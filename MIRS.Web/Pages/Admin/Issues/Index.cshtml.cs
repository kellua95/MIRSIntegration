using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Issues
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<IssueViewModel> Issues { get; set; }

        public void OnGet()
        {
            Issues = _context.Issues
                .Select(i => new IssueViewModel
                {
                    Id = i.Id,
                    IssueType = i.IssueType.Name,
                    Status = i.Status.Name,
                    Municipality = i.Municipality.Name,
                    EmployeeName = i.AssignedEmployee != null
                        ? i.AssignedEmployee.FullName
                        : null
                }).ToList();
        }
    }

    public class IssueViewModel
    {
        public int Id { get; set; }
        public string IssueType { get; set; }
        public string Status { get; set; }
        public string Municipality { get; set; }
        public string EmployeeName { get; set; }
    }
}
