using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.DTOs;

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
            var (success, error) = await _authService.RegisterAsync(request.Email, request.Password, request.FullName, request.Role);
            if (!success)
                return BadRequest(new { error });
            return Ok(new { message = "Register successful" });
        }
        /// <summary>
        /// Get user profile by userId, if userId is null then get the profile of the logged-in user
        /// </summary>
        /// <param name="userId"> nếu Id là null thi là user đang đăng nhập</param>
        /// <returns></returns>
        [HttpGet("user-profile")]
        public async Task<ActionResult<UserReadDTO>> UserProfile(Guid? userId)
        {
            var rs = await _authService.GetUserById(userId);
            
            return Ok(rs);
        }
        /// <summary>
        /// Update user information by userId
        /// </summary>
        /// <param name="userId"> nếu là null thì là user đang đăng nhập</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("/{userId}")]
        public async Task<ActionResult<UserReadDTO>> UpdateUser(Guid? userId,UserCreateUpdateDTO dto)
        {
            var rs = await _authService.UpdateUserAsync(userId,dto);

            return Ok(rs);
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
        public string Role { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
} 