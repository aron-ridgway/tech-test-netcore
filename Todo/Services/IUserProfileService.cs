using System.Threading.Tasks;

namespace Todo.Services
{
    public interface IUserProfileService
    {
        Task<string> GetUserName(string email);
    }
}