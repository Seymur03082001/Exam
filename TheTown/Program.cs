using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheTown.DAL;
using TheTown.Models;

namespace TheTown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Lockout.AllowedForNewUsers = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name:"areas",
                pattern: "{area:exists}/{controller=position}/{action=index}/{id?}"
                );

            app.MapControllerRoute(
                "default","{Controller=Home}/{Action=Index}/{id?}"
                );
            app.Run();
        }
    }
}