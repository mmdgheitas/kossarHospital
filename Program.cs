using kosarHospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DB>(i => i.UseSqlServer(builder.Configuration.GetConnectionString("Hospital")));

builder.Services.AddIdentity<User, IdentityRole>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 1;
    option.Password.RequireNonAlphanumeric = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedAccount = false;
})
    .AddUserManager<UserManager<User>>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<DB>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/home/AccessDenied";
    option.Cookie.Name = "login";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromDays(1);
    option.LoginPath = "/home/login_singin";
    option.SlidingExpiration = false;
});

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromDays(7);
});

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();