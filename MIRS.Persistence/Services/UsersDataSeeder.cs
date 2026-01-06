using Microsoft.AspNetCore.Identity;
using MIRS.Domain.Constants;
using MIRS.Domain.Interfaces;
using MIRS.Domain.Models;

namespace MIRS.Persistence.Services;

public class UsersDataSeeder : IDataSeeder
{
    private readonly UserManager<AppUser> _userManager;

    public UsersDataSeeder(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var superAdminEmail = "superadmin@mirs.com";
        var superAdmin = await _userManager.FindByEmailAsync(superAdminEmail);

        if (superAdmin == null)
        {
            superAdmin = new AppUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                FullName = "Super Admin",
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(superAdmin, "Pas$word1");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(superAdmin, Roles.SuperAdmin);
            }
        }
    }
}
