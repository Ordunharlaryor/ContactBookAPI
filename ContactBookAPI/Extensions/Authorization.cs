using ContactBook.Domain.Enums;
using Core.ContactBookAPI.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBook.API.Extension
{
    public static class AuthorizationExtension

    {
            public static void AddAuthorizationExtension(this IServiceCollection services)
            {
                services.AddAuthorization(options =>
                {

                    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UsersRoleManager.ADMIN)) ;
                    options.AddPolicy("RequireRegularUserRole", policy => policy.RequireRole(UsersRoleManager.REGULAR));
                    options.AddPolicy("RequireRegularUserOrAdminRole", policy =>
                    {
                        policy.RequireRole(UsersRoleManager.REGULAR);
                        policy.RequireRole(UsersRoleManager.ADMIN);
                    });
                });
            }
        }

    }
