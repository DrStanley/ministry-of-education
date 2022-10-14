using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Identity
{
    public static class UserSeeder
    {
        public static void SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<AppUser> userManager)
        {
            AppUser user = new AppUser()
            {
                Email = "admin@emis.com",
                UserName = "admin",
                Name = "Admin",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

            if (userManager.FindByEmailAsync(user.Email).Result == null)
            {
                var result = userManager.CreateAsync(user, "Admin123.").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SchoolAdmin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "SchoolAdmin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Teacher").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Teacher"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }   
            if (!roleManager.RoleExistsAsync("Inspector").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Inspector"
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }
    }
}
