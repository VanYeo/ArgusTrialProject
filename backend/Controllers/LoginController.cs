using backend.DTOs.Login;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var (isAuthenticated, token) = await loginService.LoginAsync(loginRequestDto.Email, loginRequestDto.Password);

            if (isAuthenticated && token != null)
            {
                return Ok(new { token });
            }

            return BadRequest("Invalid email or password");
        }
    }
}