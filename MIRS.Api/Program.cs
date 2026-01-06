using Microsoft.EntityFrameworkCore;
using MIRS.Api.Extensions;
using MIRS.Api.Middleware;
using MIRS.Application.Middleware;
using MIRS.Startup;

namespace MIRS.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddAppServices(builder.Configuration);
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        
        var app = builder.Build();
        
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<TransactionMiddleware>();
        
        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();
        await DatabaseInitializer.InitializeAsync(app.Services);
        app.Run();
    }
}