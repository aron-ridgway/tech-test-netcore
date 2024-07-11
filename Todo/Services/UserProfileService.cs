using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<ToDoApiOptions> _options;

        public UserProfileService(IHttpClientFactory httpClientFactory, IOptions<ToDoApiOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        public async Task<string> GetUserName(string email)
        {
            var client = _httpClientFactory.CreateClient();

            var profile = new UserProfile();
            var url = $"{_options.Value.UserProfileUrl}?email={email}";

            try
            {
                // if our api is slow or down, add a cancellation token to prevent the request from hanging
                CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                CancellationToken token = cts.Token;

                var response = await client.GetAsync(url, token);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    profile = JsonSerializer.Deserialize<UserProfile>(json);
                }
            }
            catch (HttpRequestException ex)
            {
                //log there is something wrong with our internal api
            }
            catch (TaskCanceledException ex)
            {
                //log there is something wrong with our internal api
            }

            return profile.DisplayName;
        }
    }
}
