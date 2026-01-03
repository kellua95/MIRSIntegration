using MIRS.Api.Extensions;
using MIRS.Api.Middleware;

namespace MIRS.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddAppServices(builder.Configuration,builder);
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        
        var app = builder.Build();
        
        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}