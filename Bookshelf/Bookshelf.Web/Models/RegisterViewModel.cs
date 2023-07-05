using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
