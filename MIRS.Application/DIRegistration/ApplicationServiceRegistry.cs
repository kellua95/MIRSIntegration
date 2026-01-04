
using MIRS.Core.DI;

namespace MIRS.Application.DIRegistration;

public static class ApplicationServiceRegistry
{
    private static readonly IList<ServiceDescriptor> _services = new List<ServiceDescriptor>();

    public static IReadOnlyList<ServiceDescriptor> GetServices()
    {
        /*Add(ServiceDescriptor(
             typeof(Service),
             typeof(Implementation),
             ServiceLifetime))*/

        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}