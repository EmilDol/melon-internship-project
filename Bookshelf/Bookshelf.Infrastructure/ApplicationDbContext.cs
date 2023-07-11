using Bookshelf.Infrastructure.Configurations;
using Bookshelf.Infrastructure.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<RequestCategory> RequestsCategories { get; set; }

        public DbSet<RequestFollow> RequestsFollows { get; set; }

        public DbSet<ResourceCategory> ResourcesCategories { get; set; }

        public DbSet<RequestUpvote> RequestUpvotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RequestCategory>()
                .HasKey(rc => new {rc.RequestId, rc.CategoryId});

            builder.Entity<RequestFollow>()
                .HasKey(rf => new { rf.RequestId, rf.UserId });
            
            builder.Entity<ResourceCategory>()
                .HasKey(rc => new { rc.ResourceId, rc.CategoryId });
            
            builder.Entity<RequestUpvote>()
                .HasKey(ru => new { ru.RequestId, ru.UserId });

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}