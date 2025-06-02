using ProjectPlanner;
using ProjectPlanner.IMethod;
using ProjectPlanner.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new ConnectionStringProvider(builder.Configuration.GetConnectionString("conn").ToString()));
builder.Services.AddScoped<IAccount, AccountRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

// Add session services
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(1); // session timeout here
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
