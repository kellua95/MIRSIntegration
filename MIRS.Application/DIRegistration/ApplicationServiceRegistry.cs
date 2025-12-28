using MIRS.Application.Interfaces;
using MIRS.Application.Services;
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
        _services.Add(new ServiceDescriptor(typeof(ITestAppService), typeof(TestAppService), ServiceLifetime.Scoped));

        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}