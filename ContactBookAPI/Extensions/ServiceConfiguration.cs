using AutoMapper;
using ContactBook.Core.Abstraction.Interfaces;
using ContactBook.Core.Implementations.Services;
using ContactBook.Infrastructure.Repositories;
using Core.ContactBookAPI.Implementations.Services;
using Microsoft.AspNetCore.Hosting;

namespace ContactBook.API.Extensions
{
    public static class ServiceConfiguration
    {
        public static void ServiceConfigurationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUploadService, UploadService>();




        }
    }
}
