using MIRS.Core.DI;
using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Services;

namespace MIRS.Domain.DIRegistration;

public static class DomainServiceRegistry
{
    private static readonly IList<ServiceDescriptor> _services=new List<ServiceDescriptor>();
    
    public static IReadOnlyList<ServiceDescriptor> GetServices()
    {
        /*Add(ServiceDescriptor(
             typeof(Service),
             typeof(Implementation),
             ServiceLifetime))*/
        
        _services.Add(new ServiceDescriptor(typeof(ITestManager), typeof(TestManager), ServiceLifetime.Scoped));
        
        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}