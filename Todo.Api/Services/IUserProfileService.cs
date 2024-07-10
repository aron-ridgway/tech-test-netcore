using Todo.Api.Models;

namespace Todo.Api.Services
{
    public interface IUserProfileService
    {
        Task<GravatarProfile> GetGravatarProfileAsync(string email);
    }
}
