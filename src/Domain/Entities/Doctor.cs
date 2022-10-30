using Domain.Entities.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor:Entity
    {
        public Doctor()
        {

        }
        public Doctor(string id) : base(id)
        {
        }

        public string Biography { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}
