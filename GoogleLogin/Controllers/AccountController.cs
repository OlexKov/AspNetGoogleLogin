using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleLogin.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;

        [HttpPost("sign-in/google")]
        public async Task<IActionResult> Login([FromBody] AuthResponse googleAccessToken) => Ok(await accountService.GoogleLoginAsync(googleAccessToken.token));
    }
}
