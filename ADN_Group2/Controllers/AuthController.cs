using Microsoft.AspNetCore.Mvc;
using Service.Auth;

namespace ADN_Group2.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var (success, error) = await _authService.RegisterAsync(request.Email, request.Password, request.FullName);
            if (!success)
                return BadRequest(new { error });
            return Ok(new { message = "Register successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (success, token, error) = await _authService.LoginAsync(request.Email, request.Password);
            if (!success)
                return BadRequest(new { error });
            return Ok(new { token });
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
} 