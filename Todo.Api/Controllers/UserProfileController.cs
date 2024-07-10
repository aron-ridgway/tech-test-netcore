using Microsoft.AspNetCore.Mvc;
using Todo.Api.Services;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        [ResponseCache(Duration = 1800)]
        public async Task<IActionResult> Get(string email)
        {
            var profile = await _userProfileService.GetGravatarProfileAsync(email);
            return Ok(profile);
        }
    }
}
