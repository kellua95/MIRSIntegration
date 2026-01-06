using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MIRS.Domain.Interfaces.DomainServices;

namespace MIRS.Domain.Services;

public abstract class DomainService : IDomainService
{
    private readonly Lazy<IServiceProvider> _serviceProvider;
    private ILogger? _logger;

    protected DomainService(IServiceProvider serviceProvider)
    {
        _serviceProvider = new Lazy<IServiceProvider>(() => serviceProvider);
    }

    protected IServiceProvider ServiceProvider => _serviceProvider.Value;

    protected ILogger Logger =>
        _logger ??= ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(GetType());
}
