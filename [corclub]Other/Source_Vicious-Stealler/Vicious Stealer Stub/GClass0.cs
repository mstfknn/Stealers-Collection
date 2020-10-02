using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x0200000C RID: 12
public class GClass0
{
	// Token: 0x06000022 RID: 34 RVA: 0x00002560 File Offset: 0x00000760
	private int method_0(int int_0)
	{
		if (int_0 > this.byte_0.Length)
		{
			return 0;
		}
		checked
		{
			int num = int_0 + 8;
			for (int i = int_0; i <= num; i++)
			{
				if (i > this.byte_0.Length - 1)
				{
					return 0;
				}
				if ((this.byte_0[i] & 128) != 128)
				{
					return i;
				}
			}
			return int_0 + 8;
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000025B8 File Offset: 0x000007B8
	private long method_1(int int_0, int int_1)
	{
		checked
		{
			int_1++;
			byte[] array = new byte[8];
			int num = int_1 - int_0;
			bool flag = false;
			if (num == 0 | num > 9)
			{
				return 0L;
			}
			if (num == 1)
			{
				array[0] = (this.byte_0[int_0] & 127);
				return BitConverter.ToInt64(array, 0);
			}
			if (num == 9)
			{
				flag = true;
			}
			int num2 = 1;
			int num3 = 7;
			int num4 = 0;
			if (flag)
			{
				array[0] = this.byte_0[int_1 - 1];
				int_1--;
				num4 = 1;
			}
			for (int i = int_1 - 1; i >= int_0; i += -1)
			{
				if (i - 1 >= int_0)
				{
					array[num4] = (byte)(unchecked(((int)((byte)((uint)this.byte_0[i] >> (checked(num2 - 1) & 7))) & 255 >> num2) | (int)((byte)(this.byte_0[checked(i - 1)] << (num3 & 7)))));
					num2++;
					num4++;
					num3--;
				}
				else if (!flag)
				{
					array[num4] = (byte)((int)(unchecked((byte)((uint)this.byte_0[i] >> (checked(num2 - 1) & 7)))) & 255 >> num2);
				}
			}
			return BitConverter.ToInt64(array, 0);
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000026C4 File Offset: 0x000008C4
	private bool method_2(long long_0)
	{
		return (long_0 & 1L) == 1L;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000026E8 File Offset: 0x000008E8
	private ulong method_3(int int_0, int int_1)
	{
		if (int_1 > 8 | int_1 == 0)
		{
			return 0UL;
		}
		ulong num = 0UL;
		int num2 = 0;
		checked
		{
			int num3 = int_1 - 1;
			for (int i = num2; i <= num3; i++)
			{
				num = (num << 8 | unchecked((ulong)this.byte_0[checked(int_0 + i)]));
			}
			return num;
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002738 File Offset: 0x00000938
	private void method_4(ulong ulong_1)
	{
		checked
		{
			if (this.byte_0[(int)ulong_1] == 13)
			{
				ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.struct4_0 != null)
				{
					num2 = this.struct4_0.Length;
					this.struct4_0 = (GClass0.Struct4[])Utils.CopyArray((Array)this.struct4_0, new GClass0.Struct4[this.struct4_0.Length + (int)num + 1]);
				}
				else
				{
					this.struct4_0 = new GClass0.Struct4[(int)(num + 1)];
				}
				int num3 = 0;
				int num4 = (int)num;
				for (int i = num3; i <= num4; i++)
				{
					ulong num5 = this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ulong_1), 8m), new decimal(i * 2))), 2);
					if (decimal.Compare(new decimal(ulong_1), 100m) != 0)
					{
						num5 += ulong_1;
					}
					int num6 = this.method_0((int)num5);
					this.method_1((int)num5, num6);
					int num7 = this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num6), new decimal(num5))), 1m)));
					this.struct4_0[num2 + i].long_0 = this.method_1(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num6), new decimal(num5))), 1m)), num7);
					num5 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num7), new decimal(num5))), 1m));
					num6 = this.method_0((int)num5);
					num7 = num6;
					long value = this.method_1((int)num5, num6);
					long[] array = new long[5];
					int num8 = 0;
					do
					{
						num6 = num7 + 1;
						num7 = this.method_0(num6);
						array[num8] = this.method_1(num6, num7);
						if (array[num8] > 9L)
						{
							if (this.method_2(array[num8]))
							{
								array[num8] = (long)Math.Round((double)(array[num8] - 13L) / 2.0);
							}
							else
							{
								array[num8] = (long)Math.Round((double)(array[num8] - 12L) / 2.0);
							}
						}
						else
						{
							array[num8] = (long)(unchecked((ulong)this.byte_1[checked((int)array[num8])]));
						}
						num8++;
					}
					while (num8 <= 4);
					if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
					{
						this.struct4_0[num2 + i].string_0 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(value))), (int)array[0]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
					{
						this.struct4_0[num2 + i].string_0 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(value))), (int)array[0]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
					{
						this.struct4_0[num2 + i].string_0 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(new decimal(num5), new decimal(value))), (int)array[0]);
					}
					if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
					{
						this.struct4_0[num2 + i].string_1 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
					{
						this.struct4_0[num2 + i].string_1 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
					{
						this.struct4_0[num2 + i].string_1 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					this.struct4_0[num2 + i].long_1 = (long)this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
					if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
					{
						this.struct4_0[num2 + i].string_3 = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
					{
						this.struct4_0[num2 + i].string_3 = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
					else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
					{
						this.struct4_0[num2 + i].string_3 = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num5), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
				}
			}
			else if (this.byte_0[(int)ulong_1] == 5)
			{
				ushort num9 = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 3m)), 2)), 1m));
				int num10 = 0;
				int num11 = (int)num9;
				for (int j = num10; j <= num11; j++)
				{
					ushort num12 = (ushort)this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ulong_1), 12m), new decimal(j * 2))), 2);
					if (decimal.Compare(new decimal(ulong_1), 100m) == 0)
					{
						this.method_4(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_3((int)num12, 4)), 1m), new decimal((int)this.ushort_0))));
					}
					else
					{
						this.method_4(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_3((int)(ulong_1 + unchecked((ulong)num12)), 4)), 1m), new decimal((int)this.ushort_0))));
					}
				}
				this.method_4(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 8m)), 4)), 1m), new decimal((int)this.ushort_0))));
			}
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002FF0 File Offset: 0x000011F0
	private bool method_5(ulong ulong_1)
	{
		checked
		{
			if (this.byte_0[(int)ulong_1] == 13)
			{
				ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.struct3_0 != null)
				{
					num2 = this.struct3_0.Length;
					this.struct3_0 = (GClass0.Struct3[])Utils.CopyArray((Array)this.struct3_0, new GClass0.Struct3[this.struct3_0.Length + (int)num + 1]);
				}
				else
				{
					this.struct3_0 = new GClass0.Struct3[(int)(num + 1)];
				}
				int num3 = 0;
				int num4 = (int)num;
				for (int i = num3; i <= num4; i++)
				{
					ulong num5 = this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ulong_1), 8m), new decimal(i * 2))), 2);
					if (decimal.Compare(new decimal(ulong_1), 100m) != 0)
					{
						num5 += ulong_1;
					}
					int num6 = this.method_0((int)num5);
					this.method_1((int)num5, num6);
					int num7 = this.method_0(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num6), new decimal(num5))), 1m)));
					this.struct3_0[num2 + i].long_0 = this.method_1(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num6), new decimal(num5))), 1m)), num7);
					num5 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num5), decimal.Subtract(new decimal(num7), new decimal(num5))), 1m));
					num6 = this.method_0((int)num5);
					num7 = num6;
					long num8 = this.method_1((int)num5, num6);
					long num9 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num5), new decimal(num6)), 1m));
					int num10 = 0;
					GClass0.Struct2[] array;
					while (num9 < num8)
					{
						array = (GClass0.Struct2[])Utils.CopyArray((Array)array, new GClass0.Struct2[num10 + 1]);
						num6 = num7 + 1;
						num7 = this.method_0(num6);
						array[num10].long_1 = this.method_1(num6, num7);
						if (array[num10].long_1 > 9L)
						{
							if (this.method_2(array[num10].long_1))
							{
								array[num10].long_0 = (long)Math.Round((double)(array[num10].long_1 - 13L) / 2.0);
							}
							else
							{
								array[num10].long_0 = (long)Math.Round((double)(array[num10].long_1 - 12L) / 2.0);
							}
						}
						else
						{
							array[num10].long_0 = (long)(unchecked((ulong)this.byte_1[checked((int)array[num10].long_1)]));
						}
						num9 = num9 + unchecked((long)(checked(num7 - num6))) + 1L;
						num10++;
					}
					this.struct3_0[num2 + i].string_0 = new string[array.Length - 1 + 1];
					int num11 = 0;
					int num12 = 0;
					int num13 = array.Length - 1;
					for (int j = num12; j <= num13; j++)
					{
						if (array[j].long_1 > 9L)
						{
							if (!this.method_2(array[j].long_1))
							{
								if (decimal.Compare(new decimal(this.ulong_0), 1m) == 0)
								{
									this.struct3_0[num2 + i].string_0[j] = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num8)), new decimal(num11))), (int)array[j].long_0);
								}
								else if (decimal.Compare(new decimal(this.ulong_0), 2m) == 0)
								{
									this.struct3_0[num2 + i].string_0[j] = Encoding.Unicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num8)), new decimal(num11))), (int)array[j].long_0);
								}
								else if (decimal.Compare(new decimal(this.ulong_0), 3m) == 0)
								{
									this.struct3_0[num2 + i].string_0[j] = Encoding.BigEndianUnicode.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num8)), new decimal(num11))), (int)array[j].long_0);
								}
							}
							else
							{
								this.struct3_0[num2 + i].string_0[j] = Encoding.Default.GetString(this.byte_0, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num8)), new decimal(num11))), (int)array[j].long_0);
							}
						}
						else
						{
							this.struct3_0[num2 + i].string_0[j] = Conversions.ToString(this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num5), new decimal(num8)), new decimal(num11))), (int)array[j].long_0));
						}
						num11 = (int)(unchecked((long)num11) + array[j].long_0);
					}
				}
			}
			else if (this.byte_0[(int)ulong_1] == 5)
			{
				ushort num14 = Convert.ToUInt16(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 3m)), 2)), 1m));
				int num15 = 0;
				int num16 = (int)num14;
				for (int k = num15; k <= num16; k++)
				{
					ushort num17 = (ushort)this.method_3(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(ulong_1), 12m), new decimal(k * 2))), 2);
					this.method_5(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_3((int)(ulong_1 + unchecked((ulong)num17)), 4)), 1m), new decimal((int)this.ushort_0))));
				}
				this.method_5(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.method_3(Convert.ToInt32(decimal.Add(new decimal(ulong_1), 8m)), 4)), 1m), new decimal((int)this.ushort_0))));
			}
			return true;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003714 File Offset: 0x00001914
	public bool method_6(string string_1)
	{
		int num = -1;
		int num2 = 0;
		int num3 = this.struct4_0.Length;
		int i = num2;
		checked
		{
			while (i <= num3)
			{
				if (this.struct4_0[i].string_1.ToLower().CompareTo(string_1.ToLower()) == 0)
				{
					num = i;
					IL_45:
					if (num == -1)
					{
						return false;
					}
					string[] array = this.struct4_0[num].string_3.Substring(this.struct4_0[num].string_3.IndexOf("(") + 1).Split(new char[]
					{
						','
					});
					int num4 = 0;
					int num5 = array.Length - 1;
					for (i = num4; i <= num5; i++)
					{
						array[i] = Strings.LTrim(array[i]);
						int num6 = array[i].IndexOf(" ");
						if (num6 > 0)
						{
							array[i] = array[i].Substring(0, num6);
						}
						if (array[i].IndexOf("UNIQUE") == 0)
						{
							break;
						}
						this.string_0 = (string[])Utils.CopyArray((Array)this.string_0, new string[i + 1]);
						this.string_0[i] = array[i];
					}
					return this.method_5((ulong)((this.struct4_0[num].long_1 - 1L) * (long)(unchecked((ulong)this.ushort_0))));
				}
				else
				{
					i++;
				}
			}
			goto IL_45;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003860 File Offset: 0x00001A60
	public int method_7()
	{
		return this.struct3_0.Length;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003878 File Offset: 0x00001A78
	public string method_8(int int_0, int int_1)
	{
		if (int_0 >= this.struct3_0.Length)
		{
			return null;
		}
		if (int_1 >= this.struct3_0[int_0].string_0.Length)
		{
			return null;
		}
		return this.struct3_0[int_0].string_0[int_1];
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000038C0 File Offset: 0x00001AC0
	public string method_9(int int_0, string string_1)
	{
		int num = -1;
		int num2 = 0;
		int num3 = this.string_0.Length;
		int i = num2;
		checked
		{
			while (i <= num3)
			{
				if (this.string_0[i].ToLower().CompareTo(string_1.ToLower()) == 0)
				{
					num = i;
					IL_3A:
					if (num == -1)
					{
						return null;
					}
					return this.method_8(int_0, num);
				}
				else
				{
					i++;
				}
			}
			goto IL_3A;
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003918 File Offset: 0x00001B18
	public string[] method_10()
	{
		int num = 0;
		int num2 = 0;
		checked
		{
			int num3 = this.struct4_0.Length - 1;
			string[] array;
			for (int i = num2; i <= num3; i++)
			{
				if (Operators.CompareString(this.struct4_0[i].string_0, "table", false) == 0)
				{
					array = (string[])Utils.CopyArray((Array)array, new string[num + 1]);
					array[num] = this.struct4_0[i].string_1;
					num++;
				}
			}
			return array;
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003994 File Offset: 0x00001B94
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public GClass0(string baseName)
	{
		this.byte_1 = new byte[]
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
				this.byte_0 = Encoding.Default.GetBytes(s);
				if (Encoding.Default.GetString(this.byte_0, 0, 15).CompareTo("SQLite format 3") != 0)
				{
					throw new Exception("Not a valid SQLite 3 Database File");
				}
				if (this.byte_0[52] != 0)
				{
					throw new Exception("Auto-vacuum capable database is not supported");
				}
				if (decimal.Compare(new decimal(this.method_3(44, 4)), 4m) >= 0)
				{
					throw new Exception("No supported Schema layer file-format");
				}
				this.ushort_0 = (ushort)this.method_3(16, 2);
				this.ulong_0 = this.method_3(56, 4);
				if (decimal.Compare(new decimal(this.ulong_0), 0m) == 0)
				{
					this.ulong_0 = 1UL;
				}
				this.method_4(100UL);
			}
		}
	}

	// Token: 0x04000012 RID: 18
	private byte[] byte_0;

	// Token: 0x04000013 RID: 19
	private ushort ushort_0;

	// Token: 0x04000014 RID: 20
	private ulong ulong_0;

	// Token: 0x04000015 RID: 21
	private GClass0.Struct4[] struct4_0;

	// Token: 0x04000016 RID: 22
	private byte[] byte_1;

	// Token: 0x04000017 RID: 23
	private GClass0.Struct3[] struct3_0;

	// Token: 0x04000018 RID: 24
	private string[] string_0;

	// Token: 0x0200000D RID: 13
	private struct Struct2
	{
		// Token: 0x04000019 RID: 25
		public long long_0;

		// Token: 0x0400001A RID: 26
		public long long_1;
	}

	// Token: 0x0200000E RID: 14
	private struct Struct3
	{
		// Token: 0x0400001B RID: 27
		public long long_0;

		// Token: 0x0400001C RID: 28
		public string[] string_0;
	}

	// Token: 0x0200000F RID: 15
	private struct Struct4
	{
		// Token: 0x0400001D RID: 29
		public long long_0;

		// Token: 0x0400001E RID: 30
		public string string_0;

		// Token: 0x0400001F RID: 31
		public string string_1;

		// Token: 0x04000020 RID: 32
		public string string_2;

		// Token: 0x04000021 RID: 33
		public long long_1;

		// Token: 0x04000022 RID: 34
		public string string_3;
	}
}
