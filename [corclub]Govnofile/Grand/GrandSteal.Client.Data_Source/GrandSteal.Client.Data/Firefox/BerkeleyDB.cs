using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000013 RID: 19
	public class BerkeleyDB
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000023AC File Offset: 0x000005AC
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000023B4 File Offset: 0x000005B4
		public string Version { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000023BD File Offset: 0x000005BD
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000023C5 File Offset: 0x000005C5
		public List<KeyValuePair<string, string>> Keys { get; private set; }

		// Token: 0x0600006C RID: 108 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public BerkeleyDB(string FileName)
		{
			List<byte> list = new List<byte>();
			this.Keys = new List<KeyValuePair<string, string>>();
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(FileName)))
			{
				int i = 0;
				int num = (int)binaryReader.BaseStream.Length;
				while (i < num)
				{
					list.Add(binaryReader.ReadByte());
					i++;
				}
			}
			string value = BitConverter.ToString(this.Extract(list.ToArray(), 0, 4, false)).Replace("-", "");
			string text = BitConverter.ToString(this.Extract(list.ToArray(), 4, 4, false)).Replace("-", "");
			int num2 = BitConverter.ToInt32(this.Extract(list.ToArray(), 12, 4, true), 0);
			if (!string.IsNullOrEmpty(value))
			{
				this.Version = "Berkelet DB";
				if (text.Equals("00000002"))
				{
					this.Version += " 1.85 (Hash, version 2, native byte-order)";
				}
				int num3 = int.Parse(BitConverter.ToString(this.Extract(list.ToArray(), 56, 4, false)).Replace("-", ""));
				int num4 = 1;
				while (this.Keys.Count < num3)
				{
					string[] array = new string[(num3 - this.Keys.Count) * 2];
					for (int j = 0; j < (num3 - this.Keys.Count) * 2; j++)
					{
						array[j] = BitConverter.ToString(this.Extract(list.ToArray(), num2 * num4 + 2 + j * 2, 2, true)).Replace("-", "");
					}
					Array.Sort<string>(array);
					for (int k = 0; k < array.Length; k += 2)
					{
						int num5 = Convert.ToInt32(array[k], 16) + num2 * num4;
						int num6 = Convert.ToInt32(array[k + 1], 16) + num2 * num4;
						int num7 = (k + 2 >= array.Length) ? (num2 + num2 * num4) : (Convert.ToInt32(array[k + 2], 16) + num2 * num4);
						string @string = Encoding.ASCII.GetString(this.Extract(list.ToArray(), num6, num7 - num6, false));
						string value2 = BitConverter.ToString(this.Extract(list.ToArray(), num5, num6 - num5, false));
						if (!string.IsNullOrEmpty(@string))
						{
							this.Keys.Add(new KeyValuePair<string, string>(@string, value2));
						}
					}
					num4++;
				}
				return;
			}
			this.Version = "Unknow database format";
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004D38 File Offset: 0x00002F38
		private byte[] Extract(byte[] source, int start, int length, bool littleEndian)
		{
			byte[] array = new byte[length];
			int num = 0;
			for (int i = start; i < start + length; i++)
			{
				array[num] = source[i];
				num++;
			}
			if (littleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000023CE File Offset: 0x000005CE
		private byte[] ConvertToLittleEndian(byte[] source)
		{
			Array.Reverse(source);
			return source;
		}
	}
}
