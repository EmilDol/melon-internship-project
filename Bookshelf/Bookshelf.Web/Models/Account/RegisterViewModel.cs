using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "First Name must be at least 1 character.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "Last Name must be at least 1 character.")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(256, ErrorMessage = "Email cannot be longer than 256 characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(256, ErrorMessage = "Password cannot be longer than 256 characters.")]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(256, ErrorMessage = "Password cannot be longer than 256 characters.")]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
