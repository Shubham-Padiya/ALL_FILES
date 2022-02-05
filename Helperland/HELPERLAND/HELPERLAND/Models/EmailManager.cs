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
        public static void SendEmail(string to , string subject , string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.IsBodyHtml = true;
            mail.To.Add(to);
            mail.From = new MailAddress("My Email");
            mail.Subject = subject;
            mail.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("My Email", "My Password");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
