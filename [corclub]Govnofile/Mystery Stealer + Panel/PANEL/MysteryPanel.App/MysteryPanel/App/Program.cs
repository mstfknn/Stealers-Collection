using System;
using System.Windows;
using System.Windows.Forms;
using MysteryPanel.App.Views;

namespace MysteryPanel.App
{
	// Token: 0x02000032 RID: 50
	public static class Program
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00008020 File Offset: 0x00006220
		[STAThread]
		public static void Main()
		{
			try
			{
				System.Windows.Forms.Application.EnableVisualStyles();
				System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
				Form mainForm = new LoginView();
				System.Windows.MessageBox.Show("Mystery Stealer CRACKED BY LOKI https://t.me/loki_chanell LOKI STEALER");
				System.Windows.Forms.Application.Run(mainForm);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString());
			}
		}
	}
}
