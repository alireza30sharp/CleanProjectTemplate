using Infrastructure.IdentityConfigs;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigurationManager configuration = builder.Configuration;


string connection= configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DataBaseContext>(option =>
{
    option.UseSqlServer(connection, b => b.MigrationsAssembly("Persistence"));
});

//builder.Services.AddDbContext<IdentityDataBaseContext>(option =>
//{
//    option.UseSqlServer(connection, b => b.MigrationsAssembly("Persistence"));
//});
builder.Services.AddIdentityService(configuration);
builder.Services.AddAuthorization();

builder.Services.ConfigureExternalCookie(option => { 
option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.LoginPath = "/Account/login";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.SlidingExpiration = true;

});

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

app.Run();
