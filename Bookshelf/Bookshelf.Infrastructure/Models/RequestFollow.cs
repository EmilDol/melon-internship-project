﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Infrastructure.Models
{
    public class RequestFollow
    {
        [Required]
        [ForeignKey(nameof(Requests))]
        public int RequestId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public Request Requests { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
