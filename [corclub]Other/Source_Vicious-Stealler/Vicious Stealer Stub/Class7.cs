using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000019 RID: 25
[StandardModule]
internal sealed class Class7
{
	// Token: 0x0200001A RID: 26
	public class Class8
	{
		// Token: 0x0600004B RID: 75
		[DllImport("kernel32")]
		private static extern IntPtr HeapAlloc(IntPtr intptr_1, uint uint_0, uint uint_1);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32")]
		private static extern IntPtr GetProcessHeap();

		// Token: 0x0600004D RID: 77
		[DllImport("kernel32")]
		private static extern int lstrlen(IntPtr intptr_1);

		// Token: 0x0600004E RID: 78
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_open(IntPtr intptr_1, ref IntPtr intptr_2);

		// Token: 0x0600004F RID: 79
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_close(IntPtr intptr_1);

		// Token: 0x06000050 RID: 80
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_exec(IntPtr intptr_1, IntPtr intptr_2, IntPtr intptr_3, IntPtr intptr_4, ref IntPtr intptr_5);

		// Token: 0x06000051 RID: 81
		[DllImport("mozsqlite3")]
		private static extern IntPtr sqlite3_errmsg(IntPtr intptr_1);

		// Token: 0x06000052 RID: 82
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_prepare_v2(IntPtr intptr_1, IntPtr intptr_2, int int_3, ref IntPtr intptr_3, ref IntPtr intptr_4);

		// Token: 0x06000053 RID: 83
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_step(IntPtr intptr_1);

		// Token: 0x06000054 RID: 84
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_column_count(IntPtr intptr_1);

		// Token: 0x06000055 RID: 85
		[DllImport("mozsqlite3")]
		private static extern IntPtr sqlite3_column_name(IntPtr intptr_1, int int_3);

		// Token: 0x06000056 RID: 86
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_column_type(IntPtr intptr_1, int int_3);

		// Token: 0x06000057 RID: 87
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_column_int(IntPtr intptr_1, int int_3);

		// Token: 0x06000058 RID: 88
		[DllImport("mozsqlite3")]
		private static extern double sqlite3_column_double(IntPtr intptr_1, int int_3);

		// Token: 0x06000059 RID: 89
		[DllImport("mozsqlite3")]
		private static extern IntPtr sqlite3_column_text(IntPtr intptr_1, int int_3);

		// Token: 0x0600005A RID: 90
		[DllImport("mozsqlite3")]
		private static extern IntPtr sqlite3_column_blob(IntPtr intptr_1, int int_3);

		// Token: 0x0600005B RID: 91
		[DllImport("mozsqlite3")]
		private static extern IntPtr sqlite3_column_table_name(IntPtr intptr_1, int int_3);

		// Token: 0x0600005C RID: 92
		[DllImport("mozsqlite3")]
		private static extern int sqlite3_finalize(IntPtr intptr_1);

		// Token: 0x0600005D RID: 93 RVA: 0x000020F3 File Offset: 0x000002F3
		public Class8()
		{
			this.intptr_0 = IntPtr.Zero;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002106 File Offset: 0x00000306
		public Class8(string baseName)
		{
			this.method_0(baseName);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002115 File Offset: 0x00000315
		public void method_0(string string_0)
		{
			if (Class7.Class8.sqlite3_open(this.method_7(string_0), ref this.intptr_0) != 0)
			{
				this.intptr_0 = IntPtr.Zero;
				throw new Exception("Error with opening database " + string_0 + "!");
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000214D File Offset: 0x0000034D
		public void method_1()
		{
			if (this.intptr_0 != IntPtr.Zero)
			{
				Class7.Class8.sqlite3_close(this.intptr_0);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003F38 File Offset: 0x00002138
		public ArrayList method_2()
		{
			string string_ = "SELECT name FROM sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%'UNION ALL SELECT name FROM sqlite_temp_master WHERE type IN ('table','view') ORDER BY 1";
			DataTable dataTable = this.method_4(string_);
			ArrayList arrayList = new ArrayList();
			try
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					arrayList.Add(dataRow.ItemArray[0].ToString());
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
			return arrayList;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003FC0 File Offset: 0x000021C0
		public void method_3(string string_0)
		{
			IntPtr intPtr;
			Class7.Class8.sqlite3_exec(this.intptr_0, this.method_7(string_0), IntPtr.Zero, IntPtr.Zero, ref intPtr);
			if (intPtr != IntPtr.Zero)
			{
				throw new Exception("Error with executing non-query: \"" + string_0 + "\"!" + this.method_8(Class7.Class8.sqlite3_errmsg(intPtr)));
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000401C File Offset: 0x0000221C
		public DataTable method_4(string string_0)
		{
			IntPtr intptr_;
			IntPtr intPtr;
			Class7.Class8.sqlite3_prepare_v2(this.intptr_0, this.method_7(string_0), this.method_9(this.method_7(string_0)), ref intptr_, ref intPtr);
			DataTable result = new DataTable();
			for (int num = this.method_5(intptr_, ref result); num == 100; num = this.method_6(intptr_, ref result))
			{
			}
			Class7.Class8.sqlite3_finalize(intptr_);
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004078 File Offset: 0x00002278
		private int method_5(IntPtr intptr_1, ref DataTable dataTable_0)
		{
			dataTable_0 = new DataTable("resultTable");
			int num = Class7.Class8.sqlite3_step(intptr_1);
			checked
			{
				if (num == 100)
				{
					int num2 = Class7.Class8.sqlite3_column_count(intptr_1);
					object[] array = new object[num2 - 1 + 1];
					int num3 = 0;
					int num4 = num2 - 1;
					for (int i = num3; i <= num4; i++)
					{
						string columnName = this.method_8(Class7.Class8.sqlite3_column_name(intptr_1, i));
						switch (Class7.Class8.sqlite3_column_type(intptr_1, i))
						{
						case 1:
							dataTable_0.Columns.Add(columnName, Type.GetType("System.Int32"));
							array[i] = Class7.Class8.sqlite3_column_int(intptr_1, i);
							break;
						case 2:
							dataTable_0.Columns.Add(columnName, Type.GetType("System.Single"));
							array[i] = Class7.Class8.sqlite3_column_double(intptr_1, i);
							break;
						case 3:
							dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
							array[i] = this.method_8(Class7.Class8.sqlite3_column_text(intptr_1, i));
							break;
						case 4:
							dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
							array[i] = this.method_8(Class7.Class8.sqlite3_column_blob(intptr_1, i));
							break;
						default:
							dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
							array[i] = "";
							break;
						}
					}
					dataTable_0.Rows.Add(array);
				}
				return Class7.Class8.sqlite3_step(intptr_1);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000041FC File Offset: 0x000023FC
		private int method_6(IntPtr intptr_1, ref DataTable dataTable_0)
		{
			int num = Class7.Class8.sqlite3_column_count(intptr_1);
			checked
			{
				object[] array = new object[num - 1 + 1];
				int num2 = 0;
				int num3 = num - 1;
				for (int i = num2; i <= num3; i++)
				{
					switch (Class7.Class8.sqlite3_column_type(intptr_1, i))
					{
					case 1:
						array[i] = Class7.Class8.sqlite3_column_int(intptr_1, i);
						break;
					case 2:
						array[i] = Class7.Class8.sqlite3_column_double(intptr_1, i);
						break;
					case 3:
						array[i] = this.method_8(Class7.Class8.sqlite3_column_text(intptr_1, i));
						break;
					case 4:
						array[i] = this.method_8(Class7.Class8.sqlite3_column_blob(intptr_1, i));
						break;
					default:
						array[i] = "";
						break;
					}
				}
				dataTable_0.Rows.Add(array);
				return Class7.Class8.sqlite3_step(intptr_1);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000042C4 File Offset: 0x000024C4
		private IntPtr method_7(string string_0)
		{
			if (string_0 == null)
			{
				return IntPtr.Zero;
			}
			Encoding utf = Encoding.UTF8;
			byte[] bytes = utf.GetBytes(string_0);
			uint uint_ = checked((uint)(bytes.Length + 1));
			IntPtr intPtr = Class7.Class8.HeapAlloc(Class7.Class8.GetProcessHeap(), 0u, uint_);
			Marshal.Copy(bytes, 0, intPtr, bytes.Length);
			Marshal.WriteByte(intPtr, bytes.Length, 0);
			return intPtr;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004318 File Offset: 0x00002518
		private string method_8(IntPtr intptr_1)
		{
			if (intptr_1 == IntPtr.Zero)
			{
				return null;
			}
			Encoding utf = Encoding.UTF8;
			int num = this.method_9(intptr_1);
			byte[] array = new byte[checked(num - 1 + 1)];
			Marshal.Copy(intptr_1, array, 0, num);
			return utf.GetString(array, 0, num);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004360 File Offset: 0x00002560
		private int method_9(IntPtr intptr_1)
		{
			if (intptr_1 == IntPtr.Zero)
			{
				return 0;
			}
			return Class7.Class8.lstrlen(intptr_1);
		}

		// Token: 0x0400002A RID: 42
		private const int int_0 = 0;

		// Token: 0x0400002B RID: 43
		private const int int_1 = 100;

		// Token: 0x0400002C RID: 44
		private const int int_2 = 101;

		// Token: 0x0400002D RID: 45
		private IntPtr intptr_0;

		// Token: 0x0200001B RID: 27
		public enum Enum1
		{
			// Token: 0x0400002F RID: 47
			INT = 1,
			// Token: 0x04000030 RID: 48
			FLOAT,
			// Token: 0x04000031 RID: 49
			TEXT,
			// Token: 0x04000032 RID: 50
			BLOB,
			// Token: 0x04000033 RID: 51
			NULL
		}
	}
}
