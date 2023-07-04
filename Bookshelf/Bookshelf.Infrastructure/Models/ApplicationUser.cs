using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Requests = new List<RequestFollow>();
        }

        public ICollection<RequestFollow> Requests { get; set; } = null!;
    }
}
