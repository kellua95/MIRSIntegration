using MIRS.Application.DIRegistration;
using MIRS.Domain.DIRegistration;
using MIRS.Persistence.DIRegistration;
using ServiceDescriptor = MIRS.Core.DI.ServiceDescriptor;
using ServiceLifetime = MIRS.Core.DI.ServiceLifetime;
namespace MIRS.Web.Extensions;

public static  class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // services
        //     .AddFromRegistry(DomainServiceRegistry.GetServices())
        //     .AddFromRegistry(ApplicationServiceRegistry.GetServices())
        //     .AddFromRegistry(PersistenceServiceRegistry.GetServices());

        //services.RegisterAppServicesAndRepos();
        
        return services;
    }

    public static IServiceCollection RegisterAppServicesAndRepos(this IServiceCollection services)
    {
        var servicesBucket = new Dictionary<Type, Type>();
        
        PersistenceServices.Register(servicesBucket);
        DomainServices.Register(servicesBucket);
        ApplicationServices.Register(servicesBucket);
        
        foreach (var entry in servicesBucket)
        {
            services.AddScoped(entry.Key, entry.Value);
        }
        
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