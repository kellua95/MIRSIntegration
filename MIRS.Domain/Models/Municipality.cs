using MIRS.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class Municipality : BaseEntity<int>
    {
        public string Name { get; set; }
        public int GovernorateId { get; set; }

        public Governorate Governorate { get; set; }
    }
}
