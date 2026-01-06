using MIRS.Application.DTOs;

namespace MIRS.Application.Interfaces;

public interface IUserService
{
    Task<UserDetailDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDetailDto>> GetAllUsersAsync();
    Task<UserDetailDto> CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
    Task AssignRoleAsync(int userId, string role);
}
