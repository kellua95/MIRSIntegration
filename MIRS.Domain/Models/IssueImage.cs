using MIRS.Core.BaseModels;
using MIRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class IssueImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public int UploadedByUserId { get; set; }
         public AppUser User { get; set; }
        public ImageTypeEnum ImageType { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }


    }
}
