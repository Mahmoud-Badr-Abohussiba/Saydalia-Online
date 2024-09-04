using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Data;
using Saydalia_Online.Areas.Identity.Data;
using Saydalia_Online.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Saydalia_Online
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Saydalia_OnlineContextConnection") ?? throw new InvalidOperationException("Connection string 'Saydalia_OnlineContextConnection' not found.");

            //builder.Services.AddDbContext<SaydaliaOnlineContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<Saydalia_OnlineContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Saydalia_Online_AuthUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Saydalia_OnlineContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
