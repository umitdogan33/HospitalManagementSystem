using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Examination:Entity
    {
        public int UserId { get; set; }

        public string FilePath { get; set; }
    }
}
