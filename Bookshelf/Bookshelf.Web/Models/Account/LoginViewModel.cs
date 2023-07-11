using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
