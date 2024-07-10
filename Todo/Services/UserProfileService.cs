using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
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

            try
            {
                var response = await client.GetAsync($"{_options.Value.UserProfileUrl}?email={email}");

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

            return profile.DisplayName;
        }
    }
}
