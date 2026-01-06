namespace MIRS.Application.DTOs;

public class UpdateUserDto
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
}
