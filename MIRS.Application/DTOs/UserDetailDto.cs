namespace MIRS.Application.DTOs;

public class UserDetailDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
}
