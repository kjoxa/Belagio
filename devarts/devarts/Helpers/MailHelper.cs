using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace devarts.Helpers
{
    public class MailSender
    {
        public static string recipient = WebConfigurationManager.AppSettings["contactEmail"];
        public static string from = WebConfigurationManager.AppSettings["sendNewsletterEmail"];
        public static string fromTitle = WebConfigurationManager.AppSettings["sendNewsletterEmailTitle"];
        public static string pass = WebConfigurationManager.AppSettings["sendNewsletterEmailPassword"];
        public static string smptHost = WebConfigurationManager.AppSettings["sendNewsletterEmailHost"];
        public static int port = Convert.ToInt32(WebConfigurationManager.AppSettings["sendNewsletterEmailPort"]);

        /*
             <smtp from="contact@sangueazzurro.pl">
             <network host="smtp.webio.pl" port="587" userName="contact@sangueazzurro.pl" password="sangueazzurro1!" enableSsl="true" />
             </smtp>
        */
        
        [ValidateInput(false)]
        public bool SendSingleMail(string to, string subject, string body, HttpPostedFileBase fileUploader)
        {
            using (MailMessage mail = new MailMessage(from, to))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                if (fileUploader != null)
                {
                    string fileName = Path.GetFileName(fileUploader.FileName);
                    mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = smptHost;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from, pass);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = port;

                try
                {
                    smtp.Send(mail);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        [ValidateInput(false)]
        public bool SendNewsletter(List<string> allSubscribers, string subject, string body, HttpPostedFileBase fileUploader)
        {
            

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from, fromTitle, Encoding.UTF8);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                foreach (string adres in allSubscribers)
                {
                    mail.Bcc.Add(new MailAddress(adres));
                }

                if (fileUploader != null)
                {
                    string fileName = Path.GetFileName(fileUploader.FileName);
                    mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = smptHost;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from, pass);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = port;

                try
                {
                    smtp.Send(mail);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }

    public class MailHelper
    {
        private static SmtpClient _smtpClient;

        static MailHelper()
        {
            _smtpClient = new SmtpClient();
        }

        /// <summary>
        /// Wysłanie e-maila do pojedynczego odbiorcy.
        /// </summary>
        /// <param name="departmentId">Identyfikator wydziału</param>
        /// <param name="recipientAddress">Adres e-mail odbiorcy.</param>
        /// <param name="recipient">Nazwa odbiorcy.</param>
        /// <param name="subject">Temat e-maila.</param>
        /// <param name="news">Treść e-maila.</param>
        public static void SendEmail(string recipientAddress, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                string recipient = "kontakt@spanishwaterdog.pl";

                mail.From = new MailAddress(recipient, "Formularz kontaktowy - wiadomość od " + recipientAddress, Encoding.UTF8);
                mail.To.Add(new MailAddress(recipient, recipientAddress, Encoding.UTF8));
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                _smtpClient.Send(mail);
            }
        }

        public static void SendToRecepientEmail(string recipientAddress, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                string recipient = "kontakt@spanishwaterdog.pl";

                mail.From = new MailAddress(recipient, "Potwierdzenie wysłania wiadomości", Encoding.UTF8);
                mail.To.Add(new MailAddress(recipientAddress, recipient, Encoding.UTF8));
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                _smtpClient.Send(mail);
            }
        }

        public static void SendEmailReservation(string recipientAddress, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                string sender = "kontakt@spanishwaterdog.pl";

                mail.From = new MailAddress(sender, "Rezerwacja od " + recipientAddress, Encoding.UTF8);
                mail.To.Add(new MailAddress(sender, recipientAddress, Encoding.UTF8));
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                _smtpClient.Send(mail);
            }
        }

        public static void SendToRecepientEmailReservation(string recipientAddress, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                string sender = "kontakt@spanishwaterdog.pl";

                mail.From = new MailAddress(sender, "Potwierdzenie wysłania rezerwacji", Encoding.UTF8);
                mail.To.Add(new MailAddress(recipientAddress, sender, Encoding.UTF8));
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                _smtpClient.Send(mail);
            }
        }

        // wysłanie maila z okna modalnego z listy rezerwacji
        public static void SendEmailFromReservationList(string recipientAddress, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                string sender = "kontakt@spanishwaterdog.pl";

                mail.From = new MailAddress(sender, "Informacja o dostępności szczenięcia", Encoding.UTF8);
                mail.To.Add(new MailAddress(recipientAddress, sender, Encoding.UTF8));
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                _smtpClient.Send(mail);
            }
        }
    }
}