using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIRS.Domain.Interfaces;
using MIRS.Domain.Models;

namespace MIRS.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public List<Issue> Issues { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return RedirectToPage("/Welcome");
        }

        var issuesList = await _unitOfWork.Repository<Issue>().GetListBySpecAsync(null);
        Issues = issuesList.ToList();

        return Page();
    }
}