

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Saydalia_Online.Areas.Identity.Data
{
    public static class ApplicationIdentityDbContextSeeding
    {
        public static async Task SeedUsersAsync(UserManager<Saydalia_Online_AuthUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Pharmacist"))
            {
                await roleManager.CreateAsync(new IdentityRole("Pharmacist"));
            }

            var pharmistUsers = await userManager.GetUsersInRoleAsync("Pharmacist");

            if (!pharmistUsers.Any())
            {
                var user = new Saydalia_Online_AuthUser()
                {
                    name = "Pharmacist",
                    Email = "pharmacist@saydalia.com",
                    UserName = "pharmacist@saydalia.com",
                    PhoneNumber = "+201155700300",
                    ImagePath = "",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Pharmacist");
            }



            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var Admins = await userManager.GetUsersInRoleAsync("Admin");

            if (!pharmistUsers.Any())
            {
                var admin = new Saydalia_Online_AuthUser()
                {
                    name = "Admin",
                    Email = "admin@saydalia.com",
                    UserName = "admin@saydalia.com",
                    PhoneNumber = "+201553698187",
                    ImagePath = "",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

    }
}
