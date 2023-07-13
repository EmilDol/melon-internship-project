using System.ComponentModel.DataAnnotations;
using Bookshelf.Core.DTOs.Categories;

namespace Bookshelf.Core.DTOs.Resources
{
    public class ResourceEditDTO
    {
        public ResourceEditDTO()
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

        public ICollection<int> CategoryIds { get; set; }
    }
}
