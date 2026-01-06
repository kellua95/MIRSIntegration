using MIRS.Application.DTOs;

namespace MIRS.Application.Interfaces;

public interface IIssueService
{
    Task<IssueDto> CreateIssueAsync(CreateIssueDto createIssueDto);
    Task<IReadOnlyList<IssueDto>> GetIssuesAsync();
}
