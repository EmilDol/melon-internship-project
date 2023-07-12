using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Bookshelf.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Follows = new List<RequestFollow>();
            Upvotes = new List<RequestUpvote>();
            //Requests = new List<Request>();
            Resources = new List<Resource>();
        }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        public ICollection<RequestFollow> Follows { get; set; }

        public ICollection<RequestUpvote> Upvotes { get; set; }

        //public ICollection<Request> Requests { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
