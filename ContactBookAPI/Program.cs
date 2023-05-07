using ContactBook.API.Controllers;
using ContactBook.API.Extension;
using ContactBook.API.Extensions;
using ContactBook.Core.Utilities;
using ContactBookAPI.Extensions;
using Core.ContactBookAPI.Implementations.Services;
using Core.ContactBookAPI.Utilities;

public class Program
{
    public static async Task Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);
     

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorizationExtension();
        builder.Services.ServiceConfigurationExtension(builder.Configuration);
        builder.Services.DbExtensionMethod(builder.Configuration);
        builder.Services.AddSwaggerExtension();
        builder.Services.AddAutoMapper(typeof(ContactBookAPI_Profile));
        



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
      

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction => { setupAction.SwaggerEndpoint("/swagger/ContactsBookAPI/swagger.json", "ODUNAYOR's ContactBooK_API"); });
        }

        app.UseHttpsRedirection();


        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();
 
        app.MapControllers();
        await AdminDataSeeding.SeedAdmin(app);
        //_ = app.UseMvc();
        app.Run();
    }
}
