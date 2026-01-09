using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Employees
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EmployeeVM> Employees { get; set; }

        public void OnGet()
        {
            Employees = _context.Users
                .Where(u => u.Role.Name == "Employee")
                .Select(e => new EmployeeVM
                {
                    Name = e.FullName,
                    IssueCount = e.AssignedIssues.Count
                }).ToList();
        }
    }

    public class EmployeeVM
    {
        public string Name { get; set; }
        public int IssueCount { get; set; }
    }
}
