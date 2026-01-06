using Microsoft.AspNetCore.Mvc;
using MIRS.Application.DTOs;
using MIRS.Application.Interfaces;

namespace MIRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssueService _issueService;

    public IssuesController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<IssueDto>>> GetIssues()
    {
        var issues = await _issueService.GetIssuesAsync();
        return Ok(issues);
    }

    [HttpPost]
    public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto createIssueDto)
    {
        var issue = await _issueService.CreateIssueAsync(createIssueDto);

        if (issue == null) return BadRequest("Problem creating issue");

        return Ok(issue);
    }
}
