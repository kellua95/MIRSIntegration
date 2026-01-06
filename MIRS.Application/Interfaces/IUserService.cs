using MIRS.Application.DTOs;

namespace MIRS.Application.Interfaces;

public interface IUserService
{
    Task<UserDetailDto> GetUserByIdAsync(int id);
    Task<Pagination<UserDetailDto>> GetAllUsersAsync(PaginationParams paginationParams);
    Task<UserDetailDto> CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
    Task AssignRoleAsync(int userId, string role);
}
