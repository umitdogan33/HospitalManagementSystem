using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailOptions
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
    }
}
