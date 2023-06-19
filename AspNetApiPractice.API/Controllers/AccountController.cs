using AspNetApiPractice.Services.User;
using AspNetApiPractice.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiPractice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            await _userService.CreateUser(command);
            return Ok();
        }
    }
}
