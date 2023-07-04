using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Models
{
    public class RequestFollow
    {
        [ForeignKey(nameof(Requests))]
        public int RequestId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public Request Requests { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
