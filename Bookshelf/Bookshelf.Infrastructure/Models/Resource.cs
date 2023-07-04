using Bookshelf.Infrastructure.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public ResourceType Type { get; set; }

        public DateTime DateTaken { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public ResourceStatus? Status { get; set; }

        public string? FilePath { get; set; } 
    }
}
