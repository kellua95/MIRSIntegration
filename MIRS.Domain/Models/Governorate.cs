using MIRS.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Models
{
    public class Governorate : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Municipality> Municipalities { get; set; }
    }
}
