using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIRS.Application.DTOs;
using MIRS.Application.Exceptions;
using MIRS.Application.Interfaces;
using MIRS.Domain.Models;

namespace MIRS.Application.Services;

public class UserService : ApplicationService, IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserService(
        IServiceProvider serviceProvider,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager) : base(serviceProvider)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UserDetailDto> GetUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null) throw new NotFoundException(nameof(AppUser), id);

        var roles = await _userManager.GetRolesAsync(user);
        var roleDtos = await GetRoleDtosAsync(roles);

        return MapToUserDetailDto(user, roleDtos);
    }

    public async Task<Pagination<UserDetailDto>> GetAllUsersAsync(PaginationParams paginationParams)
    {
        var query = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            query = query.Where(u => u.Email!.Contains(paginationParams.Search) || 
                                     u.FullName!.Contains(paginationParams.Search));
        }

        var count = await query.CountAsync();

        var users = await query
            .Skip((paginationParams.PageIndex - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        var userDetails = new List<UserDetailDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleDtos = await GetRoleDtosAsync(roles);
            userDetails.Add(MapToUserDetailDto(user, roleDtos));
        }

        return new Pagination<UserDetailDto>(
            paginationParams.PageIndex, 
            paginationParams.PageSize, 
            count, 
            userDetails);
    }

    public async Task<UserDetailDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser
        {
            Email = createUserDto.Email,
            UserName = createUserDto.Email,
            FullName = createUserDto.FullName,
            PhoneNumber = createUserDto.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.Select(e => e.Description));

        var roleResult = await _userManager.AddToRoleAsync(user, createUserDto.Role);

        if (!roleResult.Succeeded)
            throw new ValidationException(roleResult.Errors.Select(e => e.Description));

        var role = await _roleManager.FindByNameAsync(createUserDto.Role);
        var roleDtos = new List<RoleDto>();
        if (role != null) roleDtos.Add(new RoleDto { Id = role.Id, Name = role.Name! });

        return MapToUserDetailDto(user, roleDtos);
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null) throw new NotFoundException(nameof(AppUser), id);

        user.FullName = updateUserDto.FullName;
        user.PhoneNumber = updateUserDto.PhoneNumber;
        user.IsActive = updateUserDto.IsActive;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.Select(e => e.Description));
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null) throw new NotFoundException(nameof(AppUser), id);

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.Select(e => e.Description));
    }

    public async Task AssignRoleAsync(int userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null) throw new NotFoundException(nameof(AppUser), userId);

        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
            throw new ValidationException(result.Errors.Select(e => e.Description));
    }

    private async Task<ICollection<RoleDto>> GetRoleDtosAsync(IList<string> roleNames)
    {
        var roleDtos = new List<RoleDto>();
        foreach (var name in roleNames)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role != null)
            {
                roleDtos.Add(new RoleDto { Id = role.Id, Name = role.Name! });
            }
        }
        return roleDtos;
    }

    private UserDetailDto MapToUserDetailDto(AppUser user, ICollection<RoleDto> roles)
    {
        return new UserDetailDto
        {
            Id = user.Id,
            Email = user.Email!,
            FullName = user.FullName ?? "",
            PhoneNumber = user.PhoneNumber ?? "",
            IsActive = user.IsActive,
            Roles = roles
        };
    }
}
