using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Core.DTOs
{
    public class RequestAddDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Author { get; set; } = null!;

        [Required]
        public string Priority { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public string Link { get; set; } = null!;

        [MaxLength(300)]
        public string? Motivation { get; set; } = null!;

        [Required]
        public List<string> Categories { get; set; } = null!;
    }
}
