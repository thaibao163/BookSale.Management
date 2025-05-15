using BookSale.Management.Domain.Entities;
using BookSale.Managment.DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Managment.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                                    .AddEntityFrameworkStores<ApplicationDbContext>()
                                    .AddDefaultTokenProviders();
        }
    }
}
