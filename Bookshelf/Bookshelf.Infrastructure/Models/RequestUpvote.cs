using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Infrastructure.Models
{
    public class RequestUpvote
    {
        [Required]
        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public Request Request { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
    }
}
