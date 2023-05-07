using CloudinaryDotNet;
using ContactBook.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.ContactBookAPI.Utilities
{
    public  class AdminDataSeeding
    {
       
      public static async Task SeedAdmin(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { UsersRoleManager.ADMIN, UsersRoleManager.REGULAR };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
           
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UsersModel>>();
                var userName = "Pato";
                var Password = "Thattechsis@$";

                if (await userManager.FindByNameAsync(userName) == null)
                {
                    var user = new UsersModel();
                    user.UserName = userName;
                    user.Email = Password;
                    await userManager.CreateAsync(user, Password);
                    await  userManager.AddToRoleAsync(user, UsersRoleManager.ADMIN);
                }
                    

            }
        }
    }
}
