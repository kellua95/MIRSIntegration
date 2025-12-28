using MIRS.Core.DI;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Persistence.Repositories;

namespace MIRS.Persistence.DIRegistration;

public static class PersistenceServiceRegistry
{
    private static readonly IList<ServiceDescriptor> _services = new List<ServiceDescriptor>();

    public static IReadOnlyList<ServiceDescriptor> GetServices()
    {
        /*Add(ServiceDescriptor(
             typeof(Service),
             typeof(Implementation),
             ServiceLifetime))*/
        
        _services.Add(
            new ServiceDescriptor(
                typeof(ITestRepo),
                typeof(TestRepo),
                ServiceLifetime.Scoped));

        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}