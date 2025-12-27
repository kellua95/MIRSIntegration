using MIRS.Core.DI;

namespace MIRS.Core.DIRegistration;

public class CoreServiceRegistry
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

    private IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}