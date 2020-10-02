using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace ns2
{
	internal sealed class Class8
	{
		[DllImport("kernel32")]
		private static extern IntPtr HeapAlloc(IntPtr intptr_1, uint uint_0, uint uint_1);

		[DllImport("kernel32")]
		private static extern IntPtr GetProcessHeap();

		[DllImport("kernel32")]
		private static extern int lstrlen(IntPtr intptr_1);

		[DllImport("sqlite3")]
		private static extern int sqlite3_open(IntPtr intptr_1, ref IntPtr intptr_2);

		[DllImport("sqlite3")]
		private static extern int sqlite3_prepare_v2(IntPtr intptr_1, IntPtr intptr_2, int int_0, ref IntPtr intptr_3, ref IntPtr intptr_4);

		[DllImport("sqlite3")]
		private static extern int sqlite3_step(IntPtr intptr_1);

		[DllImport("sqlite3")]
		private static extern int sqlite3_column_count(IntPtr intptr_1);

		[DllImport("sqlite3")]
		private static extern IntPtr sqlite3_column_name(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern int sqlite3_column_type(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern int sqlite3_column_int(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern double sqlite3_column_double(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern IntPtr sqlite3_column_text(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern IntPtr sqlite3_column_blob(IntPtr intptr_1, int int_0);

		[DllImport("sqlite3")]
		private static extern int sqlite3_finalize(IntPtr intptr_1);

		public Class8(string string_0)
		{
			this.method_0(string_0);
		}

		public void method_0(string string_0)
		{
			if (Class8.sqlite3_open(this.method_4(string_0), ref this.intptr_0) != 0)
			{
				this.intptr_0 = IntPtr.Zero;
				throw new Exception("Error with opening database " + string_0 + "!");
			}
		}

		public DataTable method_1(string string_0)
		{
			IntPtr intptr_;
			IntPtr intPtr;
			Class8.sqlite3_prepare_v2(this.intptr_0, this.method_4(string_0), this.method_6(this.method_4(string_0)), ref intptr_, ref intPtr);
			DataTable result = new DataTable();
			for (int num = this.method_2(intptr_, ref result); num == 100; num = this.method_3(intptr_, ref result))
			{
			}
			Class8.sqlite3_finalize(intptr_);
			return result;
		}

		private int method_2(IntPtr intptr_1, ref DataTable dataTable_0)
		{
			dataTable_0 = new DataTable("resultTable");
			int num = Class8.sqlite3_step(intptr_1);
			if (num == 100)
			{
				int num2 = Class8.sqlite3_column_count(intptr_1);
				object[] array = new object[num2];
				for (int i = 0; i < num2; i++)
				{
					string columnName = this.method_5(Class8.sqlite3_column_name(intptr_1, i));
					switch (Class8.sqlite3_column_type(intptr_1, i))
					{
					case 1:
						dataTable_0.Columns.Add(columnName, Type.GetType("System.Int32"));
						array[i] = Class8.sqlite3_column_int(intptr_1, i);
						break;
					case 2:
						dataTable_0.Columns.Add(columnName, Type.GetType("System.Single"));
						array[i] = Class8.sqlite3_column_double(intptr_1, i);
						break;
					case 3:
						dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
						array[i] = this.method_5(Class8.sqlite3_column_text(intptr_1, i));
						break;
					case 4:
						dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
						array[i] = this.method_5(Class8.sqlite3_column_blob(intptr_1, i));
						break;
					default:
						dataTable_0.Columns.Add(columnName, Type.GetType("System.String"));
						array[i] = "";
						break;
					}
				}
				dataTable_0.Rows.Add(array);
			}
			return Class8.sqlite3_step(intptr_1);
		}

		private int method_3(IntPtr intptr_1, ref DataTable dataTable_0)
		{
			int num = Class8.sqlite3_column_count(intptr_1);
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				switch (Class8.sqlite3_column_type(intptr_1, i))
				{
				case 1:
					array[i] = Class8.sqlite3_column_int(intptr_1, i);
					break;
				case 2:
					array[i] = Class8.sqlite3_column_double(intptr_1, i);
					break;
				case 3:
					array[i] = this.method_5(Class8.sqlite3_column_text(intptr_1, i));
					break;
				case 4:
					array[i] = this.method_5(Class8.sqlite3_column_blob(intptr_1, i));
					break;
				default:
					array[i] = "";
					break;
				}
			}
			dataTable_0.Rows.Add(array);
			return Class8.sqlite3_step(intptr_1);
		}

		private IntPtr method_4(string string_0)
		{
			if (string_0 == null)
			{
				return IntPtr.Zero;
			}
			Encoding utf = Encoding.UTF8;
			byte[] bytes = utf.GetBytes(string_0);
			int uint_ = bytes.Length + 1;
			IntPtr intPtr = Class8.HeapAlloc(Class8.GetProcessHeap(), 0u, (uint)uint_);
			Marshal.Copy(bytes, 0, intPtr, bytes.Length);
			Marshal.WriteByte(intPtr, bytes.Length, 0);
			return intPtr;
		}

		private string method_5(IntPtr intptr_1)
		{
			if (intptr_1 == IntPtr.Zero)
			{
				return null;
			}
			Encoding utf = Encoding.UTF8;
			int num = this.method_6(intptr_1);
			byte[] array = new byte[num];
			Marshal.Copy(intptr_1, array, 0, num);
			return utf.GetString(array, 0, num);
		}

		private int method_6(IntPtr intptr_1)
		{
			if (intptr_1 == IntPtr.Zero)
			{
				return 0;
			}
			return Class8.lstrlen(intptr_1);
		}

		private IntPtr intptr_0;
	}
}
