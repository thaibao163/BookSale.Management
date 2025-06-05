using BookSale.Management.Application.Services;
using BookSale.Management.Application;
using BookSale.Managment.DataAccess;
using BookSale.Managment.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DI, DB, AutoMapper, v.v...
builder.Services.RegisterDb(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IImageService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new ImageService(env.WebRootPath);
});

var mvcBuilder = builder.Services.AddControllersWithViews()
                                 .AddSessionStateTempDataProvider();

builder.Services.AddRazorPages()
                .AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

#if DEBUG
mvcBuilder.AddRazorRuntimeCompilation();
#endif

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var config = app.Configuration;

    await app.AutoMigration();           
    await app.SeedData(config);         
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapAreaControllerRoute(
    name: "AdminRouting",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

