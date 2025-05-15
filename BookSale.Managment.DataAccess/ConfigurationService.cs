using BookSale.Management.Domain.Entities;
using BookSale.Management.Domain.Setting;
using BookSale.Managment.DataAccess.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Managment.DataAccess
{
    public static class ConfigurationService
    {
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await appContext.Database.MigrateAsync();
            }
        }

        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var userDefault = configuration.GetSection("DefaultUser")?.Get<DefaultUser>() ?? new DefaultUser();
                var roleDefault = configuration.GetValue<string>("DefaultRole") ?? "SuperAdmin";

                try
                {
                    if(!await roleManager.RoleExistsAsync(roleDefault))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleDefault));
                    }

                    //Add role
                    var existUser = await userManager.FindByNameAsync(userDefault.UserName);

                    if (existUser == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = userDefault.UserName,
                            FullName = userDefault.UserName,
                            Email = userDefault.Email,
                            NormalizedEmail = userDefault.Email.ToUpper(),
                            IsActive = true,
                            AccessFailedCount = 0
                        };

                        var identityUser = await userManager.CreateAsync(user, userDefault.Password);

                        if (identityUser.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, roleDefault);
                        }
                    }                
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
