using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Utilities
{
    public interface IEmailHelper
    {
        void SendEmail(string to, string subject, string body);
    }
}
