using Bookshelf.Infrastructure.Models.Enums;

using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Infrastructure.Models
{
    public class Request
    {
        public Request()
        {
            Categories = new List<RequestCategory>();
            Followers = new List<RequestFollow>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
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

        public string Motivation { get; set; } = null!;

        public ICollection<RequestCategory> Categories { get; set; } = null!;

        public ICollection<RequestFollow> Followers { get; set; } = null!;
    }
}
