using System.ComponentModel.DataAnnotations;
using Bookshelf.Core.DTOs.Categories;

namespace Bookshelf.Core.DTOs.Requests
{
    public class RequestAddDTO
    {
        public RequestAddDTO()
        {
            Categories = new();
            CategoryIds = new();
        }

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

        public List<CategoryDTO> Categories { get; set; } = null!;

        public List<int> CategoryIds { get; set; } = null!;
    }
}
