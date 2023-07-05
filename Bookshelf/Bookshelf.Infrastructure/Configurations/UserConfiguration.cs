using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bookshelf.Infrastructure.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Spiridon",
                NormalizedUserName = "SPIRIDON",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "Spiridon",
                LastName = "Stamatov"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "admin123!");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "ivan",
                NormalizedUserName = "IVAN",
                Email = "guest@gmail.com",
                NormalizedEmail = "GUEST@GMAIL.COM",
                FirstName = "Ivan",
                LastName = "Georgiev"
            };

            user.PasswordHash =
                hasher.HashPassword(user, "guest1123!");

            users.Add(user);


            builder.HasData(users);
        }
    }
}
