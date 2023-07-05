using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Bookshelf.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Requests = new List<RequestFollow>();
            Upvotes = new List<RequestUpvote>();
        }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        public ICollection<RequestFollow> Requests { get; set; }

        public ICollection<RequestUpvote> Upvotes { get; set; }
    }
}
