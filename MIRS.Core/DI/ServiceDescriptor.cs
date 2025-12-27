namespace MIRS.Core.DI;

public sealed record ServiceDescriptor(
    Type Service,
    Type Implementation,
    ServiceLifetime Lifetime);