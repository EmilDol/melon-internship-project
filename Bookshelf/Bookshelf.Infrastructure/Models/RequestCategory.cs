﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Infrastructure.Models
{
    public class RequestCategory
    {
        [Required]
        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Request Request { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
