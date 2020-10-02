using System;
using System.Net;
using System.Net.Mail;

namespace ISeeYou
{
    class SendMail
    {
        private void SendEmail(string mailsTo, string login, string pathAttach)
        {
            try
            {
                string mailTo = mailsTo;
                string password = "[Pass]";
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(mailTo);
                    mailMessage.To.Add(new MailAddress(mailsTo));
                    mailMessage.Subject = "[Title]";
                    mailMessage.Body = "Otchet";
                    mailMessage.Attachments.Add(new Attachment(pathAttach));
                    new SmtpClient
                    {
                        Host = "[smtp]",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(mailTo.Split(new char[]
						{
							'@'
						})[0], password),
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    }.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mail.Send: " + ex.Message);
            }
        }
    }
}
