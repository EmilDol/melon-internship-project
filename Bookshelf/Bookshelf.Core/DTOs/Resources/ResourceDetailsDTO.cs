using System.ComponentModel.DataAnnotations;

using Bookshelf.Core.DTOs.Categories;

namespace Bookshelf.Core.DTOs.Resources
{
    public class ResourceDetailsDTO
    {
        public ResourceDetailsDTO()
        {
            Categories = new List<CategoryDTO>();    
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Author { get; set; } = null!;

        public ICollection<CategoryDTO> Categories { get; set; } = null!;

        public string? Type { get; set; }

        public string DateTaken { get; set; }

        public string ExpectedReturnDate { get; set; }

        public string? Status { get; set; }

        public string? FilePath { get; set; }
    }
}
