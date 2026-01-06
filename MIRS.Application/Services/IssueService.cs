using AutoMapper;
using MIRS.Application.DTOs;
using MIRS.Application.Interfaces;
using MIRS.Domain.Interfaces;
using MIRS.Domain.Models;
using MIRS.Domain.Enums;

namespace MIRS.Application.Services;

public class IssueService : ApplicationService, IIssueService
{
    public IssueService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<IssueDto> CreateIssueAsync(CreateIssueDto createIssueDto)
    {
        var issue = Mapper.Map<Issue>(createIssueDto);
        issue.Status = IssueStatusEnum.Pending; // Default status

        await UnitOfWork.Repository<Issue>().AddAsync(issue);
        
        return Mapper.Map<IssueDto>(issue);
    }

    public async Task<IReadOnlyList<IssueDto>> GetIssuesAsync()
    {
        var issues = await UnitOfWork.Repository<Issue>().GetListBySpecAsync(null);
        return Mapper.Map<IReadOnlyList<IssueDto>>(issues);
    }
}
