using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Core.DTOs.Resources
{
    public class ResourceGetDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Author { get; set; } = null!;

        public string? Type { get; set; }

        public string? Status { get; set; }
    }
}
