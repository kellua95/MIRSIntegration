using Microsoft.AspNetCore.Identity;
using MIRS.Application.DTOs;
using MIRS.Application.Exceptions;
using MIRS.Application.Interfaces;
using MIRS.Domain.Constants;
using MIRS.Domain.Models;

namespace MIRS.Application.Services;

public class AuthService : ApplicationService, IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        IServiceProvider serviceProvider,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole<int>> roleManager,
        ITokenService tokenService) : base(serviceProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<UserDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) 
            throw new AuthenticationFailedException("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) 
            throw new AuthenticationFailedException("Invalid email or password.");

        if (!user.IsActive)
            throw new ForbiddenAccessException("Your account is deactivated.");

        var roles = await _userManager.GetRolesAsync(user);
        var tokenData = _tokenService.CreateToken(user, roles);

        var roleDtos = new List<RoleDto>();
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                roleDtos.Add(new RoleDto { Id = role.Id, Name = role.Name! });
            }
        }

        return new UserDto
        {
            Email = user.Email!,
            FullName = user.FullName ?? "",
            Token = tokenData.Token,
            ExpiresAt = tokenData.ExpiresAt,
            Roles = roleDtos
        };
    }

    public async Task<UserDto?> RegisterAsync(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            FullName = registerDto.FullName,
            PhoneNumber = registerDto.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) 
            throw new ValidationException(result.Errors.Select(e => e.Description));

        // Default role for new registrations
        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Citizen);

        if (!roleResult.Succeeded) 
            throw new ValidationException(roleResult.Errors.Select(e => e.Description));

        var roles = await _userManager.GetRolesAsync(user);
        var tokenData = _tokenService.CreateToken(user, roles);

        var roleDtos = new List<RoleDto>();
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                roleDtos.Add(new RoleDto { Id = role.Id, Name = role.Name! });
            }
        }

        return new UserDto
        {
            Email = user.Email,
            FullName = user.FullName,
            Token = tokenData.Token,
            ExpiresAt = tokenData.ExpiresAt,
            Roles = roleDtos
        };
    }
}
