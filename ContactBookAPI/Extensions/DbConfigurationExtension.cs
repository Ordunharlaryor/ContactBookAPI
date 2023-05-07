using ContactBook.Domain.Models;
using ContactBook.Infrastructure.ContactBookContext;
using Core.ContactBookAPI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBookAPI.Extensions
{
    public static class DbConfigurationExtension
    {
        public static void DbExtensionMethod(this IServiceCollection services, IConfiguration Config)

        {
            services.AddDbContextPool<ContactBook_DbContext>(options => options.UseSqlServer(Config.GetConnectionString("Conn")));

            // Configure Identity
            var Builder = services.AddIdentity<UsersModel, IdentityRole>(options =>
             {
                 options.Password.RequiredLength = 8;
                 options.Password.RequireDigit = false;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireLowercase = true;
                 options.User.RequireUniqueEmail = true;
                 options.SignIn.RequireConfirmedEmail = false;

             });
        

            Builder = new IdentityBuilder(Builder.UserType, typeof(IdentityRole), services);
            Builder.AddEntityFrameworkStores<ContactBook_DbContext>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddSignInManager<SignInManager<UsersModel>>()
            .AddDefaultTokenProviders();

        }
    }
}