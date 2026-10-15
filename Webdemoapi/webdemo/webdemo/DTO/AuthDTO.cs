using System.ComponentModel.DataAnnotations;

namespace webdemo.DTO
{
    /// <summary>Credentials used to create a new account.</summary>
    public class RegisterRequest
    {
        [Required, StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]{3,50}$", ErrorMessage = "Tên đăng nhập chỉ được chứa chữ cái, số, dấu chấm, gạch dưới hoặc gạch ngang.")]
        public string Username { get; set; }

        [Required, StringLength(128, MinimumLength = 8)]
        public string Password { get; set; }
    }

    /// <summary>Credentials used to sign in to an existing account.</summary>
    public class LoginRequest
    {
        [Required, StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]{3,50}$", ErrorMessage = "Tên đăng nhập chỉ được chứa chữ cái, số, dấu chấm, gạch dưới hoặc gạch ngang.")]
        public string Username { get; set; }

        [Required, StringLength(128, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
