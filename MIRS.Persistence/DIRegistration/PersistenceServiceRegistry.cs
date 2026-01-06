using MIRS.Core.DI;
using MIRS.Domain.Interfaces;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Persistence.Repositories;
using MIRS.Persistence.Services;

namespace MIRS.Persistence.DIRegistration;

public static class PersistenceServiceRegistry
{
    private static readonly IList<ServiceDescriptor> _services = new List<ServiceDescriptor>();

    public static IReadOnlyList<ServiceDescriptor> GetServices()
    {
        AddService(new ServiceDescriptor(
             typeof(IUnitOfWork),
             typeof(UnitOfWork),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(IGenericRepository<>),
             typeof(GenericRepository<>),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(IDataSeeder),
             typeof(IdentityDataSeeder),
             ServiceLifetime.Scoped));

        AddService(new ServiceDescriptor(
             typeof(IDataSeeder),
             typeof(UsersDataSeeder),
             ServiceLifetime.Scoped));

        return _services.AsReadOnly();
    }

    private static IList<ServiceDescriptor> AddService(ServiceDescriptor serviceDescriptor)
    {
        _services.Add(serviceDescriptor);
        return _services;
    }
}