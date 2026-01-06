using MIRS.Application.DTOs;

namespace MIRS.Application.Interfaces;

public interface IAuthService
{
    Task<UserDto?> LoginAsync(LoginDto loginDto);
    Task<UserDto?> RegisterAsync(RegisterDto registerDto);
}
