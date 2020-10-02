using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace WindowsApplication710
{
	// Token: 0x02000019 RID: 25
	public class SQLiteHandler
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003F70 File Offset: 0x00002170
		private int GVL(int startIndex)
		{
			checked
			{
				int result;
				if (startIndex > this.db_bytes.Length)
				{
					result = 0;
				}
				else
				{
					int num = startIndex + 8;
					int num2 = startIndex;
					for (;;)
					{
						int num3 = num2;
						int num4 = num;
						if (num3 > num4)
						{
							break;
						}
						if (num2 > this.db_bytes.Length - 1)
						{
							goto IL_58;
						}
						if ((this.db_bytes[num2] & 128) != 128)
						{
							goto IL_54;
						}
						num2++;
					}
					return startIndex + 8;
					IL_54:
					return num2;
					IL_58:
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003FD8 File Offset: 0x000021D8
		private long CVL(int startIndex, int endIndex)
		{
			checked
			{
				endIndex++;
				byte[] array = new byte[8];
				int num = endIndex - startIndex;
				bool flag = false;
				long result;
				if (num == 0 | num > 9)
				{
					result = 0L;
				}
				else if (num == 1)
				{
					array[0] = (this.db_bytes[startIndex] & 127);
					result = BitConverter.ToInt64(array, 0);
				}
				else
				{
					if (num == 9)
					{
						flag = true;
					}
					int num2 = 1;
					int num3 = 7;
					int num4 = 0;
					if (flag)
					{
						array[0] = this.db_bytes[endIndex - 1];
						endIndex--;
						num4 = 1;
					}
					for (int i = endIndex - 1; i >= startIndex; i += -1)
					{
						if (i - 1 >= startIndex)
						{
							array[num4] = (byte)(unchecked(((int)((byte)((uint)this.db_bytes[i] >> (checked(num2 - 1) & 7))) & 255 >> num2) | (int)((byte)(this.db_bytes[checked(i - 1)] << (num3 & 7)))));
							num2++;
							num4++;
							num3--;
						}
						else if (!flag)
						{
							array[num4] = (byte)((int)(unchecked((byte)((uint)this.db_bytes[i] >> (checked(num2 - 1) & 7)))) & 255 >> num2);
						}
					}
					result = BitConverter.ToInt64(array, 0);
				}
				return result;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004104 File Offset: 0x00002304
		private bool IsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004128 File Offset: 0x00002328
		private ulong ConvertToInteger(int startIndex, int Size)
		{
			checked
			{
				ulong result;
				if (Size > 8 | Size == 0)
				{
					result = 0UL;
				}
				else
				{
					ulong num = 0UL;
					int num2 = 0;
					int num3 = Size - 1;
					int num4 = num2;
					for (;;)
					{
						int num5 = num4;
						int num6 = num3;
						if (num5 > num6)
						{
							break;
						}
						num = (num << 8 | unchecked((ulong)this.db_bytes[checked(startIndex + num4)]));
						num4++;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004180 File Offset: 0x00002380
		private void ReadMasterTable(ulong Offset)
		{
			checked
			{
				if (this.db_bytes[(int)Offset] == 13)
				{
					ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num2 = 0;
					if (this.master_table_entries != null)
					{
						num2 = this.master_table_entries.Length;
						this.master_table_entries = (SQLiteHandler.sqlite_master_entry[])Utils.CopyArray((Array)this.master_table_entries, new SQLiteHandler.sqlite_master_entry[this.master_table_entries.Length + (int)num + 1]);
					}
					else
					{
						this.master_table_entries = new SQLiteHandler.sqlite_master_entry[(int)(num + 1)];
					}
					int num3 = 0;
					int num4 = (int)num;
					int num5 = num3;
					for (;;)
					{
						int num6 = num5;
						int num7 = num4;
						if (num6 > num7)
						{
							break;
						}
						ulong num8 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(num5 * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) != 0)
						{
							num8 += Offset;
						}
						int num9 = this.GVL((int)num8);
						this.CVL((int)num8, num9);
						int num10 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)));
						this.master_table_entries[num2 + num5].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)), num10);
						num8 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num10), new decimal(num8))), 1m));
						num9 = this.GVL((int)num8);
						num10 = num9;
						long value = this.CVL((int)num8, num9);
						long[] array = new long[5];
						int num11 = 0;
						do
						{
							num9 = num10 + 1;
							num10 = this.GVL(num9);
							array[num11] = this.CVL(num9, num10);
							if (array[num11] > 9L)
							{
								if (this.IsOdd(array[num11]))
								{
									array[num11] = (long)Math.Round((double)(array[num11] - 13L) / 2.0);
								}
								else
								{
									array[num11] = (long)Math.Round((double)(array[num11] - 12L) / 2.0);
								}
							}
							else
							{
								array[num11] = (long)(unchecked((ulong)this.SQLDataTypeSize[checked((int)array[num11])]));
							}
							num11++;
						}
						while (num11 <= 4);
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + num5].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + num5].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + num5].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
						}
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + num5].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + num5].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + num5].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						this.master_table_entries[num2 + num5].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + num5].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + num5].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + num5].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						num5++;
					}
				}
				else if (this.db_bytes[(int)Offset] == 5)
				{
					ushort num12 = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num13 = 0;
					int num14 = (int)num12;
					int num15 = num13;
					for (;;)
					{
						int num16 = num15;
						int num7 = num14;
						if (num16 > num7)
						{
							break;
						}
						ushort num17 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(num15 * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) == 0)
						{
							this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)num17, 4)), 1m), new decimal((int)this.page_size))));
						}
						else
						{
							this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + unchecked((ulong)num17)), 4)), 1m), new decimal((int)this.page_size))));
						}
						num15++;
					}
					this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004A48 File Offset: 0x00002C48
		private bool ReadTableFromOffset(ulong Offset)
		{
			checked
			{
				if (this.db_bytes[(int)Offset] == 13)
				{
					decimal num = decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m);
					int value = 0;
					if (this.table_entries != null)
					{
						value = this.table_entries.Length;
						this.table_entries = (SQLiteHandler.table_entry[])Utils.CopyArray((Array)this.table_entries, new SQLiteHandler.table_entry[Convert.ToInt32(decimal.Add(new decimal(this.table_entries.Length), num)) + 1]);
					}
					else
					{
						this.table_entries = new SQLiteHandler.table_entry[Convert.ToInt32(num) + 1];
					}
					decimal num2 = 0m;
					decimal limit = num;
					decimal num3 = num2;
					while (ObjectFlowControl.ForLoopControl.ForNextCheckDec(num3, limit, 1m))
					{
						ulong num4 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), decimal.Multiply(num3, 2m))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) != 0)
						{
							num4 += Offset;
						}
						int num5 = this.GVL((int)num4);
						this.CVL((int)num4, num5);
						int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
						this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
						num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
						num5 = this.GVL((int)num4);
						num6 = num5;
						long num7 = this.CVL((int)num4, num5);
						long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
						int num9 = 0;
						SQLiteHandler.record_header_field[] array;
						while (num8 < num7)
						{
							array = (SQLiteHandler.record_header_field[])Utils.CopyArray((Array)array, new SQLiteHandler.record_header_field[num9 + 1]);
							num5 = num6 + 1;
							num6 = this.GVL(num5);
							array[num9].type = this.CVL(num5, num6);
							if (array[num9].type > 9L)
							{
								if (this.IsOdd(array[num9].type))
								{
									array[num9].size = (long)Math.Round((double)(array[num9].type - 13L) / 2.0);
								}
								else
								{
									array[num9].size = (long)Math.Round((double)(array[num9].type - 12L) / 2.0);
								}
							}
							else
							{
								array[num9].size = (long)(unchecked((ulong)this.SQLDataTypeSize[checked((int)array[num9].type)]));
							}
							num8 = num8 + unchecked((long)(checked(num6 - num5))) + 1L;
							num9++;
						}
						this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content = new string[array.Length - 1 + 1];
						int num10 = 0;
						int num11 = 0;
						int num12 = array.Length - 1;
						int num13 = num11;
						for (;;)
						{
							int num14 = num13;
							int num15 = num12;
							if (num14 > num15)
							{
								break;
							}
							if (array[num13].type > 9L)
							{
								if (!this.IsOdd(array[num13].type))
								{
									if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
									{
										this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content[num13] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[num13].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
									{
										this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content[num13] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[num13].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
									{
										this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content[num13] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[num13].size);
									}
								}
								else
								{
									this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content[num13] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[num13].size);
								}
							}
							else
							{
								this.table_entries[Convert.ToInt32(decimal.Add(new decimal(value), num3))].content[num13] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[num13].size));
							}
							num10 = (int)(unchecked((long)num10) + array[num13].size);
							num13++;
						}
						num3 = decimal.Add(num3, 1m);
					}
				}
				else if (this.db_bytes[(int)Offset] == 5)
				{
					ushort num16 = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num17 = 0;
					int num18 = (int)num16;
					int num19 = num17;
					for (;;)
					{
						int num20 = num19;
						int num15 = num18;
						if (num20 > num15)
						{
							break;
						}
						ushort num21 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(num19 * 2))), 2);
						this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + unchecked((ulong)num21)), 4)), 1m), new decimal((int)this.page_size))));
						num19++;
					}
					this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
				}
				return true;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005220 File Offset: 0x00003420
		public bool ReadTable(string TableName)
		{
			int num = -1;
			int num2 = 0;
			int num3 = this.master_table_entries.Length;
			int num4 = num2;
			checked
			{
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						break;
					}
					if (this.master_table_entries[num4].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
					{
						goto IL_45;
					}
					num4++;
				}
				goto IL_47;
				IL_45:
				num = num4;
				IL_47:
				bool result;
				if (num == -1)
				{
					result = false;
				}
				else
				{
					string[] array = this.master_table_entries[num].sql_statement.Substring(this.master_table_entries[num].sql_statement.IndexOf("(") + 1).Split(new char[]
					{
						','
					});
					int num7 = 0;
					int num8 = array.Length - 1;
					int num9 = num7;
					for (;;)
					{
						int num10 = num9;
						int num6 = num8;
						if (num10 > num6)
						{
							break;
						}
						array[num9] = Strings.LTrim(array[num9]);
						int num11 = array[num9].IndexOf(" ");
						if (num11 > 0)
						{
							array[num9] = array[num9].Substring(0, num11);
						}
						if (array[num9].IndexOf("UNIQUE") == 0)
						{
							break;
						}
						this.field_names = (string[])Utils.CopyArray((Array)this.field_names, new string[num9 + 1]);
						this.field_names[num9] = array[num9];
						num9++;
					}
					result = this.ReadTableFromOffset((ulong)((this.master_table_entries[num].root_num - 1L) * (long)(unchecked((ulong)this.page_size))));
				}
				return result;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005390 File Offset: 0x00003590
		public int GetRowCount()
		{
			return this.table_entries.Length;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000053A8 File Offset: 0x000035A8
		public string GetValue(int row_num, int field)
		{
			string result;
			if (row_num >= this.table_entries.Length)
			{
				result = null;
			}
			else if (field >= this.table_entries[row_num].content.Length)
			{
				result = null;
			}
			else
			{
				result = this.table_entries[row_num].content[field];
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005400 File Offset: 0x00003600
		public string GetValue(int row_num, string field)
		{
			int num = -1;
			int num2 = 0;
			int num3 = this.field_names.Length;
			int num4 = num2;
			checked
			{
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						break;
					}
					if (this.field_names[num4].ToLower().CompareTo(field.ToLower()) == 0)
					{
						goto IL_3A;
					}
					num4++;
				}
				goto IL_3C;
				IL_3A:
				num = num4;
				IL_3C:
				string result;
				if (num == -1)
				{
					result = null;
				}
				else
				{
					result = this.GetValue(row_num, num);
				}
				return result;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005460 File Offset: 0x00003660
		public string[] GetTableNames()
		{
			int num = 0;
			int num2 = 0;
			checked
			{
				int num3 = this.master_table_entries.Length - 1;
				int num4 = num2;
				string[] array;
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						break;
					}
					if (Operators.CompareString(this.master_table_entries[num4].item_type, "table", false) == 0)
					{
						array = (string[])Utils.CopyArray((Array)array, new string[num + 1]);
						array[num] = this.master_table_entries[num4].item_name;
						num++;
					}
					num4++;
				}
				return array;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000054E4 File Offset: 0x000036E4
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public SQLiteHandler(string baseName)
		{
			this.SQLDataTypeSize = new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				6,
				8,
				8,
				0,
				0
			};
			checked
			{
				if (File.Exists(baseName))
				{
					FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
					string s = Strings.Space((int)FileSystem.LOF(1));
					FileSystem.FileGet(1, ref s, -1L, false);
					FileSystem.FileClose(new int[]
					{
						1
					});
					this.db_bytes = Encoding.Default.GetBytes(s);
					if (Encoding.Default.GetString(this.db_bytes, 0, 15).CompareTo("SQLite format 3") != 0)
					{
						throw new Exception("Not a valid SQLite 3 Database File");
					}
					if (this.db_bytes[52] != 0)
					{
						throw new Exception("Auto-vacuum capable database is not supported");
					}
					this.page_size = (ushort)this.ConvertToInteger(16, 2);
					this.encoding = this.ConvertToInteger(56, 4);
					if (decimal.Compare(new decimal(this.encoding), 0m) == 0)
					{
						this.encoding = 1UL;
					}
					this.ReadMasterTable(100UL);
				}
			}
		}

		// Token: 0x0400002F RID: 47
		private byte[] db_bytes;

		// Token: 0x04000030 RID: 48
		private ushort page_size;

		// Token: 0x04000031 RID: 49
		private ulong encoding;

		// Token: 0x04000032 RID: 50
		private SQLiteHandler.sqlite_master_entry[] master_table_entries;

		// Token: 0x04000033 RID: 51
		private byte[] SQLDataTypeSize;

		// Token: 0x04000034 RID: 52
		private SQLiteHandler.table_entry[] table_entries;

		// Token: 0x04000035 RID: 53
		private string[] field_names;

		// Token: 0x0200001A RID: 26
		private struct record_header_field
		{
			// Token: 0x04000036 RID: 54
			public long size;

			// Token: 0x04000037 RID: 55
			public long type;
		}

		// Token: 0x0200001B RID: 27
		private struct table_entry
		{
			// Token: 0x04000038 RID: 56
			public long row_id;

			// Token: 0x04000039 RID: 57
			public string[] content;
		}

		// Token: 0x0200001C RID: 28
		private struct sqlite_master_entry
		{
			// Token: 0x0400003A RID: 58
			public long row_id;

			// Token: 0x0400003B RID: 59
			public string item_type;

			// Token: 0x0400003C RID: 60
			public string item_name;

			// Token: 0x0400003D RID: 61
			public string astable_name;

			// Token: 0x0400003E RID: 62
			public long root_num;

			// Token: 0x0400003F RID: 63
			public string sql_statement;
		}
	}
}
