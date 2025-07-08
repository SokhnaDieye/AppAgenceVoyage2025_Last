using System;
using System.Net;
using System.Net.Mail;

namespace AppAgenceVoyage2025.Utils
{
    public class Mailer
    {
        public static void SendMail(string adresse, string subjet, string message)
        {
            try
            {
                //MailMessage mail = new MailMessage
                //{
                //    From = new MailAddress("mtd.voyage@jgohub.com"),
                //    Subject = subjet,
                //    Body = message,
                //    IsBodyHtml = false
                //};

                //mail.To.Add(adresse);

                //SmtpClient smtp = new SmtpClient("mail.jgohub.com", 587)
                //{
                //    EnableSsl = true, 
                //    Credentials = new NetworkCredential("mtd.voyage@jgohub.com", "Passer@123mtd"),
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false
                //};

                //smtp.Send(mail);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.jgohub.com"); // web58.lws-hosting.com
                mail.From = new MailAddress("mtd.voyage@jgohub.com"); // djigo@jgotechmaker.com
                mail.To.Add(adresse);
                mail.Subject = subjet;
                mail.IsBodyHtml = true;
                mail.Body = message;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mtd.voyage@jgohub.com", "Passer@123mtd"); // djigo@jgotechmaker.com | mouh@m@d_techmaker
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                Console.WriteLine("Email envoyé avec succès !");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"🚨 SMTP Error: {ex.StatusCode} - {ex.Message}");
                Console.WriteLine($"📌 Stack Trace: {ex.StackTrace}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ General Error: {ex.Message}");
                Console.WriteLine($"📌 Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
