using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Areas.Identity.Data;
using Saydalia_Online.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Saydalia_Online.Repositories;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Interfaces.InterfaceServices;
using Saydalia_Online.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Configuration;

namespace Saydalia_Online
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");


            builder.Services.AddDbContext<SaydaliaOnlineContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);

            //builder.Services.AddDbContextPool<SaydaliaOnlineContext>(options => options.UseSqlServer(connectionString));
            #region DI
            builder.Services.AddScoped<SaydaliaOnlineContext>();
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepositoryt>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IPayService, PaypalService>();
            #endregion

            builder.Services.AddDefaultIdentity<Saydalia_Online_AuthUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SaydaliaOnlineContext>();



            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            var app = builder.Build();


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                //Seeding Data 
                var userManager = services.GetRequiredService<UserManager<Saydalia_Online_AuthUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await ApplicationIdentityDbContextSeeding.SeedUsersAsync(userManager, roleManager);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }


            #region MW
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
            #endregion

            app.Run();
        }
    }
}
