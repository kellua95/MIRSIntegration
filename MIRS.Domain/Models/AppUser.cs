
using Microsoft.AspNetCore.Identity;
using MIRS.Domain.Enums;

namespace MIRS.Domain.Models;

public class AppUser:IdentityUser<int>
{
   public string ?  FullName { get; set; } 
   
   public bool IsActive { get; set; } 
     
   public DateTime CreatedAt { get; set; } = DateTime.Now;

   public DateTime UpdateAt { get; set; } = DateTime.Now;

   public UserRoleEnum Role { get; set; }

    public ICollection<Issue> CreatedIssues { get; set; }
    public ICollection<Issue> AssignedIssues { get; set; }

}