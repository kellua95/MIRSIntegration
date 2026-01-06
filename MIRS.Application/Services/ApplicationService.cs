using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MIRS.Application.Interfaces;
using MIRS.Domain.Interfaces;

namespace MIRS.Application.Services;

public abstract class ApplicationService : IApplicationService
{
    private readonly Lazy<IServiceProvider> _serviceProvider;

    private IUnitOfWork? _unitOfWork;
    private IMapper? _mapper;
    private ILogger? _logger;

    protected ApplicationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = new Lazy<IServiceProvider>(() => serviceProvider);
    }

    protected IServiceProvider ServiceProvider => _serviceProvider.Value;

    protected IUnitOfWork UnitOfWork =>
        _unitOfWork ??= ServiceProvider.GetRequiredService<IUnitOfWork>();

    protected IMapper Mapper =>
        _mapper ??= ServiceProvider.GetRequiredService<IMapper>();

    protected ILogger Logger =>
        _logger ??= ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(GetType());
}

