using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Issues
{
    public class AssignModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AssignModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IssueId { get; set; }

        [BindProperty]
        public int EmployeeId { get; set; }

        public List<SelectListItem> Employees { get; set; }

        public void OnGet(int id)
        {
            IssueId = id;

            Employees = _context.Users
                .Where(u => u.Role.Name == "Employee")
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }).ToList();
        }

        public IActionResult OnPost()
        {
            var issue = _context.Issues.Find(IssueId);
            issue.AssignedEmployeeId = EmployeeId;
            issue.StatusId = 4; // In Progress

            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
