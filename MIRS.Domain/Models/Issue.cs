using MIRS.Core.BaseModels;
using MIRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class Issue : BaseEntity
    {
        public int IssueTypeId { get; set; }
        public IssueTypeEnum Type { get; set; }
        public int GovernorateId { get; set; }
        public int MunicipalityId { get; set; }
        public string ? Street { get; set; }
        public string ? AddressDetails { get; set; }
        public string ? Description { get; set; }
        public IssueStatusEnum Status { get; set; }
        public int CreatedByUserId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public ICollection<IssueImage> ? Images { get; set; }
        public ICollection<IssueComment> ? Comments { get; set; }
        public Governorate Governorate { get; set; }
        public Municipality Municipality { get; set; }
        public AppUser CreatedByUser { get; set; }
        public AppUser AssignedEmployee { get; set; }

    }
}
