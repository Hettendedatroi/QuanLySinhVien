using System.ComponentModel.DataAnnotations;

namespace webdemo.Models
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string NormalizedUsername { get; set; }

        [Required, StringLength(500)]
        public string PasswordHash { get; set; }
    }
}
