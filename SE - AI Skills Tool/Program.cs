using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using SE_AI_Skills_Tool.Context;
using SE_AI_Skills_Tool.Models;
using SE_AI_Skills_Tool.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
                            {
                                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                                options.Cookie.HttpOnly = true;
                                options.Cookie.IsEssential = true;
                            });

// Add services to the container.

builder.Services.AddControllersWithViews();

// var connectionStringAstDev = Environment.GetEnvironmentVariable("astdevConnectionString", EnvironmentVariableTarget.Process);
var connectionStringAstDev = builder.Configuration.GetConnectionString("astdevConnectionString");

if (!string.IsNullOrEmpty(connectionStringAstDev))
{
    builder.Services.AddDbContext<AstDevContext>(opts => opts.UseSqlServer(connectionStringAstDev));
}
else
{
    throw new Exception("Connection Strings are missing.");
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AstDevContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllerRoute(name: "default",
                       pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
