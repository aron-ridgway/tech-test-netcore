using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Todo.Api.Models;

namespace Todo.Api.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<GravatarOptions> _gravatarOptions;

        public UserProfileService(IHttpClientFactory httpClientFactory, IOptions<GravatarOptions> gravatarOptions)
        {
            _httpClientFactory = httpClientFactory;
            _gravatarOptions = gravatarOptions;
        }


        public async Task<GravatarProfile> GetGravatarProfileAsync(string email)
        {
            return await GetGravatarProfile(email);
        }

        private async Task<GravatarProfile> GetGravatarProfile(string email)
        {
            GravatarProfile profile = new();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var hash = ComputeSha256Hash(email.ToLower());
                var url = $"{_gravatarOptions.Value.ApiUrl}/profiles/{hash}";

                HttpResponseMessage response = await GetGravatarPolicy().ExecuteAsync(async () => await client.GetAsync(url));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    profile = JsonSerializer.Deserialize<GravatarProfile>(json);
                }
            }
            catch (Exception)
            {
                //log an error
            }

            return profile ?? new();
        }

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 instance
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash of the input string
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static AsyncRetryPolicy<HttpResponseMessage> GetGravatarPolicy()
        {
            //this should be moved to a policy factory
            return Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        //log an error
                    });
        }

    }
}
