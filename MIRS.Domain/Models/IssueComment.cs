using MIRS.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class IssueComment : BaseEntity
    {
        public string Comment { get; set; }
        public int CreatedByEmployeeId { get; set; }    
        public AppUser User { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public bool IsVisibleToUser { get; set; }
    }
}
