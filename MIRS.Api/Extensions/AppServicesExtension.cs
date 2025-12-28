using Microsoft.EntityFrameworkCore;
using MIRS.Application.DIRegistration;
using MIRS.Domain.DIRegistration;
using MIRS.Persistence.DIRegistration;
using MIRS.Core.DI;
using MIRS.Persistence.ApplicationContext;
using ServiceDescriptor = MIRS.Core.DI.ServiceDescriptor;
using ServiceLifetime = MIRS.Core.DI.ServiceLifetime;

namespace MIRS.Api.Extensions;

public static  class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration config,WebApplicationBuilder builder)
    {
        var dbPath = Path.Combine(
            builder.Environment.ContentRootPath,
            "mirs.db"
        );

        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlite($"Data Source={dbPath}")
        );
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services
            .AddFromRegistry(DomainServiceRegistry.GetServices())
            .AddFromRegistry(ApplicationServiceRegistry.GetServices())
            .AddFromRegistry(PersistenceServiceRegistry.GetServices());
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