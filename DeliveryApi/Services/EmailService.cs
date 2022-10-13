using System.Text;
using System.Net.Mail;
using System.Net;
using System;

namespace DeliveryApi.Services
{
    public class EmailService
    {
        public static void EnviarEmail(string subject, string name, string email, string message)
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string hostEmail = "felipe.id12@gmail.com";
            string hostPassword = "eseabedoqqidllvl";
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 465; // Porta 25, 465 ou 587
            msg.From = new MailAddress(hostEmail);
            msg.To.Add(new MailAddress(email));
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = message;
            msg.IsBodyHtml = true;
            msg.BodyEncoding = Encoding.UTF8;
            smtp.EnableSsl = true;
            smtp.Timeout = 10000;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(hostEmail, hostPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.SendAsync(msg, msg);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no envio de email - " + ex.Message);
            }
        }
    }
}
