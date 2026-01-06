using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIRS.Application.DIRegistration;
using MIRS.Domain.DIRegistration;
using MIRS.Domain.Models;
using MIRS.Persistence.ApplicationDbContext;
using MIRS.Persistence.DIRegistration;
using ServiceDescriptor = MIRS.Core.DI.ServiceDescriptor;
using ServiceLifetime = MIRS.Core.DI.ServiceLifetime;

namespace MIRS.Web.Extensions;

public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
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

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
        })
        .AddCookie("Cookies", options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddAuthorization();

        services
            .AddFromRegistry(DomainServiceRegistry.GetServices())
            .AddFromRegistry(ApplicationServiceRegistry.GetServices())
            .AddFromRegistry(PersistenceServiceRegistry.GetServices());

        services.AddCors(opt =>
        {
            opt.AddPolicy(
                "CorsPolicy",
                policy => policy
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