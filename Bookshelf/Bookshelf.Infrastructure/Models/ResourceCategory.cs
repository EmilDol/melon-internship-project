using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Infrastructure.Models
{
    public class ResourceCategory
    {
        [Required]
        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Resource Resource { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
