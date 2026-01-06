namespace MIRS.Application.DTOs;

public class UserDto
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
}
