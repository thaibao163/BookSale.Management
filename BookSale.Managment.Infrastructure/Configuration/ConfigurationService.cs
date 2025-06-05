using BookSale.Management.Application;
using BookSale.Management.Application.Services;
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

            //save cookie user
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BookSaleMangementCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.LoginPath = "/admin/authentication/login";
                options.SlidingExpiration = true;
            });

            //số lần nhập sai và thời gian lock
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationDbContext>>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IGenreService, GenreService>();

            //services.AddTransient<IBookService, BookService>();

            //services.AddTransient<IImageService, ImageService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
