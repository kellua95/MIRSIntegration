
using Microsoft.AspNetCore.Identity;
using MIRS.Core.Intefaces;

namespace MIRS.Domain.Models;

public class AppUser:IdentityUser<int>, IAuditedEntity
{
   public string ?  FullName { get; set; } 
   
   public bool IsActive { get; set; } 
     
   public DateTime? CreatedAt { get; set; }
   public DateTime? UpdatedAt { get; set; }


    public ICollection<Issue> CreatedIssues { get; set; }
    public ICollection<Issue> AssignedIssues { get; set; }
   
}