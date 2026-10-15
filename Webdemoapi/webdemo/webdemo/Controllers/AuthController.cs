using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webdemo.DTO;
using webdemo.Services;

namespace webdemo.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>Create an account and store a salted password hash.</summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var created = await _authService.RegisterAsync(request.Username, request.Password, cancellationToken);
            if (!created)
            {
                return Conflict(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Tên đăng nhập đã tồn tại."
                });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = "Đăng ký thành công." });
        }

        /// <summary>Verify credentials and issue an HttpOnly authentication cookie.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var account = await _authService.AuthenticateAsync(request.Username, request.Password, cancellationToken);
            if (account == null)
            {
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Đăng nhập không thành công.",
                    Detail = "Tên đăng nhập hoặc mật khẩu không đúng."
                });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Ok(new { username = account.Username, message = "Đăng nhập thành công." });
        }

        /// <summary>Return the current authenticated user.</summary>
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new { username = User.Identity.Name });
        }

        /// <summary>End the current authenticated session.</summary>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
    }
}
