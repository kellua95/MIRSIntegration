using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MIRS.Api.Errors;
using MIRS.Application.DIRegistration;
using MIRS.Domain.DIRegistration;
using MIRS.Persistence.DIRegistration;
using MIRS.Core.DI;
using MIRS.Domain.Models;
using MIRS.Persistence.ApplicationDbContext;
using ServiceDescriptor = MIRS.Core.DI.ServiceDescriptor;
using ServiceLifetime = MIRS.Core.DI.ServiceLifetime;

namespace MIRS.Api.Extensions;

public static  class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection") ?? "Data Source=mirs.db";

        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlite(connectionString)
        );
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"] ?? "super_secret_key_that_is_at_least_32_chars_long")),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization();

        services
            .AddFromRegistry(DomainServiceRegistry.GetServices())
            .AddFromRegistry(ApplicationServiceRegistry.GetServices())
            .AddFromRegistry(PersistenceServiceRegistry.GetServices());
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = new ApiValidationErrorResponse(errors);

                return new BadRequestObjectResult(response);
            };
        });
        
        services.AddCors(opt =>
        {
            opt.AddPolicy(
                "CorsPolicy",
                policy=>policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
        });
        return services;
    }
    
    public static IServiceCollection AddFromRegistry(
        this IServiceCollection services,
        IEnumerable<ServiceDescriptor> descriptors)
    {
        foreach (var d in descriptors)
        {
            switch (d.Lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.Service, d.Implementation);
                    break;

                case ServiceLifetime.Scoped:
                    services.AddScoped(d.Service, d.Implementation);
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(d.Service, d.Implementation);
                    break;
            }
        }

        return services;
    }
}