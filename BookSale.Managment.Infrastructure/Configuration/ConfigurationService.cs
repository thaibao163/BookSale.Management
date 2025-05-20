using BookSale.Management.Application;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using BookSale.Managment.DataAccess.Data;
using BookSale.Managment.DataAccess.Repository;
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

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationDbContext>>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IGenreService, GenreService>();

            services.AddTransient<IBookService, BookService>();
        }
    }
}
