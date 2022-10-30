using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient:Entity
    {
        public Patient()
        {

        }
        public Patient(string userId)
        {
            UserId=userId;
        }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
