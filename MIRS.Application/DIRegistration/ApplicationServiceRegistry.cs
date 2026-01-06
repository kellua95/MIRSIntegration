using MIRS.Application.Interfaces;
using MIRS.Application.Services;
using MIRS.Core.DI;

namespace MIRS.Application.DIRegistration;

public static class ApplicationServiceRegistry
{
    private static readonly IList<ServiceDescriptor> _services = new List<ServiceDescriptor>();

    public static IReadOnlyList<ServiceDescriptor> GetServices()
    {
        AddService(new ServiceDescriptor(
             typeof(IIssueService),
             typeof(IssueService),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(ITokenService),
             typeof(TokenService),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(IAuthService),
             typeof(AuthService),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(IUserService),
             typeof(UserService),
             ServiceLifetime.Scoped));

        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}