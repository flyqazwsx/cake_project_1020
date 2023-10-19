using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using cake_project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace cake_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDbContext<Cart_dbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;

                    options.LoginPath = new PathString("/Cake/Login");

                    options.AccessDeniedPath = new PathString("/Cake/Login");
                });

            

            var app = builder.Build();

           
            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Cake}/{action=Index}/{id?}");

            app.Run();
        }
    }
}