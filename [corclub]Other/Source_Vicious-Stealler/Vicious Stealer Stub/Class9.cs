using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200001D RID: 29
[StandardModule]
internal sealed class Class9
{
	// Token: 0x06000076 RID: 118 RVA: 0x00005078 File Offset: 0x00003278
	public static string smethod_0()
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", false);
		byte[] sourceArray = (byte[])registryKey.GetValue("DigitalProductID");
		byte[] array = new byte[15];
		Array.Copy(sourceArray, 52, array, 0, 15);
		string text = "BCDFGHJKMPQRTVWXY2346789";
		string text2 = "";
		int num = 0;
		checked
		{
			do
			{
				short num2 = 0;
				int num3 = 14;
				do
				{
					num2 = (num2 * 256 ^ (short)array[num3]);
					array[num3] = (byte)Math.Round(Conversion.Int((double)num2 / 24.0));
					num2 %= 24;
					num3 += -1;
				}
				while (num3 >= 0);
				text2 = text.Substring((int)num2, 1) + text2;
				num++;
			}
			while (num <= 24);
			int num4 = 4;
			do
			{
				text2 = text2.Insert(num4 * 5, "-");
				num4 += -1;
			}
			while (num4 >= 1);
			return text2;
		}
	}
}
