using MIRS.Domain.Enums;

namespace MIRS.Application.DTOs;

public class IssueDto
{
    public int Id { get; set; }
    public int IssueTypeId { get; set; }
    public int GovernorateId { get; set; }
    public int MunicipalityId { get; set; }
    public string? Street { get; set; }
    public string? AddressDetails { get; set; }
    public string? Description { get; set; }
    public IssueStatusEnum Status { get; set; }
    public int UserId { get; set; }
}
