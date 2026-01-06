using MIRS.Core.BaseModels;
using MIRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class IssueType : BaseEntity
    {
        public IssueTypeEnum Type { get; set; }
        public bool IsActive { get; set; }

    }
}
