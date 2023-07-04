using System.ComponentModel.DataAnnotations;

using Bookshelf.Infrastructure.Models.Enums;

namespace Bookshelf.Infrastructure.Models
{
    public class Request
    {
        public Request()
        {
            Categories = new List<RequestCategory>();
            Followers = new List<RequestFollow>();
            Upvoters = new List<RequestUpvote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = null!;

        public DateTime DateAdded { get; set; }

        [Required]
        public RequestStatus Status { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public int Upvotes { get; set; }

        [Required]
        public string Link { get; set; } = null!;

        [MaxLength(300)]
        public string? Motivation { get; set; } = null!;

        public ICollection<RequestCategory> Categories { get; set; } = null!;

        public ICollection<RequestFollow> Followers { get; set; } = null!;

        public ICollection<RequestUpvote> Upvoters { get; set; } = null!;
    }
}
