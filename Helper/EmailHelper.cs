using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace HubstaffReportGenerator.Helper
{
    class EmailHelper
    {
        public static void Email(string subject, string content, string emailTo)
        {
            using (MailMessage mm = new MailMessage(Constants.SmtpUser, emailTo))
            {

                mm.IsBodyHtml = false;
                mm.Subject = subject;
                mm.Body = content;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Constants.SmtpHost;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(Constants.SmtpUser, Constants.SmtpPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Constants.SmtpPort;
                smtp.SendMailAsync(mm).Wait();
            }
        }

        public static void EmailWithAttachment(string subject, string content, string[] emailTo, Stream stream, string fileName)
        {
            using (MailMessage mm = new MailMessage())
            {
                mm.IsBodyHtml = false;
                mm.Subject = subject;
                mm.Body = content;
                mm.Attachments.Add(new Attachment(stream, fileName));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Constants.SmtpHost;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(Constants.SmtpUser, Constants.SmtpPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Constants.SmtpPort;
                foreach (var email in emailTo)
                {
                    mm.To.Add(new MailAddress(email));
                }
                mm.From = new MailAddress(Constants.SmtpUser);
                smtp.SendMailAsync(mm).Wait();
            }
        }
    }
}
