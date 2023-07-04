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

        public ICollection<RequestFollow> Requests { get; set; }

        public ICollection<RequestUpvote> Upvotes { get; set; }
    }
}
