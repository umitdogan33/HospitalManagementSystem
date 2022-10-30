using Application.Common.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Email
{
    public class EmailHelper : IEmailHelper
    {
        public IConfiguration Configuration { get; }
        public EmailOptions Options;
        public EmailHelper(IConfiguration configuration)
        {
            Configuration=configuration;
           Options = Configuration.GetSection("EmailOptions").Get<EmailOptions>();
        }

        public void SendEmail(string to,string subject, string body)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailMessage newMail = new MailMessage();
            SmtpClient client = new SmtpClient(Options.Host);
            newMail.From = new MailAddress(Options.From, Options.DisplayName);

            newMail.To.Add(to);

            newMail.Subject = subject;

            newMail.IsBodyHtml = true; newMail.Body = $"<p>Registration was successful. Your password :</p><h1>{body}</h1> <h4>please change your password after login</h4>";
            client.EnableSsl = true;
            client.Port = Options.Port;
            client.Credentials = new NetworkCredential(Options.From, Options.Password, Options.Domain);
            client.UseDefaultCredentials = false;
            client.Send(newMail);


        }
    }
}
