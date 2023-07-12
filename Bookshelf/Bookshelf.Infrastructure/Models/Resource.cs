using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bookshelf.Infrastructure.Models.Enums;

namespace Bookshelf.Infrastructure.Models
{
    public class Resource
    {
        public Resource()
        {
            Categories = new List<ResourceCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Author { get; set; } = null!;

        public ICollection<ResourceCategory> Categories { get; set; } = null!;

        public ResourceType Type { get; set; }

        public DateTime? DateTaken { get; set; }

        public DateTime? ExpectedReturnDate { get; set; }

        public ResourceStatus? Status { get; set; }

        public string? FilePath { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
