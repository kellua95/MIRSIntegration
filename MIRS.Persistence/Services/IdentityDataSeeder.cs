using Microsoft.AspNetCore.Identity;
using MIRS.Domain.Constants;
using MIRS.Domain.Interfaces;

namespace MIRS.Persistence.Services;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public IdentityDataSeeder(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var roles = new[]
        {
            Roles.SuperAdmin,
            Roles.Admin,
            Roles.Employee,
            Roles.Citizen
        };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }
}
