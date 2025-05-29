using BookSale.Management.Application.Services;
using BookSale.Management.Application;
using BookSale.Managment.DataAccess;
using BookSale.Managment.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var buiderRazor = builder.Services.AddRazorPages()
                                    .AddSessionStateTempDataProvider();

// Add services to the container.

builder.Services.RegisterDb(builder.Configuration);

builder.Services.AddDependencyInjection();

buiderRazor.Services.AddAutoMapper();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IImageService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new ImageService(env.WebRootPath); // truyền thủ công WebRootPath
});


builder.Services.AddControllersWithViews()
                    .AddSessionStateTempDataProvider();

builder.Services.AddRazorPages()
         .AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

await app.AutoMigration();

await app.SeedData(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    buiderRazor.AddRazorRuntimeCompilation();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "AdminRouting",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseSession();

app.Run();
