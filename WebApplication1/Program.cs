using biznis.Interfaces.Repository;
using biznis.Interfaces.Services;
using biznis.Repository;
using biznis.Services;
using ClassLibrary1;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services
  .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
      options.LoginPath = "/Home/Login";
      options.AccessDeniedPath = "/Home/Forbidden";
  });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var db = new AppDbContext())
{
    db.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

    var adminEmail = "admin@admin.com";

    // You need a method to check existence by email.
    // E.g. IUserService.GetByEmailAsync or IUserRepository.GetByEmailAsync.
    var existing = await scope.ServiceProvider
        .GetRequiredService<IUserRepository>()
        .GetByEmailAsync(adminEmail);

    if (existing == null)
    {
        await userService.CreateAdminAsync("admin", adminEmail, "123");
    }
}

app.Run();
