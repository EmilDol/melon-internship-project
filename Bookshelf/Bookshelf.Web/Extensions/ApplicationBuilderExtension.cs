using Bookshelf.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Bookshelf.Web.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("BaseUser"))
                {
                    IdentityRole userRole = new IdentityRole("BaseUser");
                    await roleManager.CreateAsync(userRole);
                }

                ApplicationUser admin = await userManager.FindByNameAsync("Spiridon");
                await userManager.AddToRoleAsync(admin, "Admin");

                ApplicationUser user = await userManager.FindByNameAsync("ivan");
                await userManager.AddToRoleAsync(user, "BaseUser");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
