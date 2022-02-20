using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HELPERLAND.Models
{
    public class EmailManager
    {
        public static void SendEmail(List<string> toList , string subject , string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            foreach (var mailTo in toList)
            {
                mail.To.Add(mailTo);
            }
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("Gmail");
            mail.Subject = subject;
            mail.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("Gmail", "Password");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
