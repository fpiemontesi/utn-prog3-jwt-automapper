using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos;
using UserApi.Services.Auth;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _loginService.ValidateUserAsync(dto.UserName, dto.Password);

            if (token == null)
            {
                //return Forbid();
                return NotFound();
            }

            return Ok(token);
        }
    }
}
