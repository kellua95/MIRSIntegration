namespace MIRS.Application.DTOs;

public class CreateIssueDto
{
    public int IssueTypeId { get; set; }
    public int GovernorateId { get; set; }
    public int MunicipalityId { get; set; }
    public string? Street { get; set; }
    public string? AddressDetails { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}
