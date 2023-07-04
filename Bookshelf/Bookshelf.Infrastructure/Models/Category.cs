using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Infrastructure.Models
{
    public class Category
    {
        public Category()
        {
            Requests = new List<RequestCategory>();
            Resources = new List<ResourceCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<RequestCategory> Requests { get; set; } = null!;

        public ICollection<ResourceCategory> Resources { get; set; } = null!;
    }
}
