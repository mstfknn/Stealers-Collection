using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MetroSet_UI.Design;

namespace MysteryPanel.App
{
	// Token: 0x02000031 RID: 49
	public static class ThemeExtensions
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000034B2 File Offset: 0x000016B2
		public static void ApplyTheme(this TextBox button, Style style)
		{
			if (style == Style.Light)
			{
				button.ForeColor = ThemeExtensions.LightTextFore;
				button.BackColor = ThemeExtensions.LightTextBack;
			}
			if (style == Style.Dark)
			{
				button.ForeColor = ThemeExtensions.DarkTextFore;
				button.BackColor = ThemeExtensions.DarkTextBack;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000034E7 File Offset: 0x000016E7
		public static void ApplyTheme(this Button button)
		{
			button.ForeColor = Color.White;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000034F4 File Offset: 0x000016F4
		public static void ApplyTheme(this ListView list, Style style)
		{
			ThemeExtensions.ApplyListView(list, style);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000034FD File Offset: 0x000016FD
		public static void ApplyTheme(this ListBox list, Style style)
		{
			if (style == Style.Light)
			{
				list.ForeColor = ThemeExtensions.LightListViewFore;
				list.BackColor = ThemeExtensions.LightListBoxBack;
			}
			if (style == Style.Dark)
			{
				list.ForeColor = ThemeExtensions.DarkListViewFore;
				list.BackColor = ThemeExtensions.DarkListBoxBack;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007EC0 File Offset: 0x000060C0
		public static void ApplyTheme(this ObjectListView list, Style style)
		{
			if (style == Style.Light)
			{
				for (int i = 0; i < list.Items.Count; i++)
				{
					list.Items[i].BackColor = ThemeExtensions.LightListViewBack;
					list.Items[i].ForeColor = ThemeExtensions.LightListViewFore;
				}
			}
			if (style == Style.Dark)
			{
				for (int j = 0; j < list.Items.Count; j++)
				{
					list.Items[j].BackColor = ThemeExtensions.DarkListViewBack;
					list.Items[j].ForeColor = ThemeExtensions.DarkListViewFore;
				}
			}
			ThemeExtensions.ApplyListView(list, style);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00003532 File Offset: 0x00001732
		private static void ApplyListView(ListView list, Style style)
		{
			if (style == Style.Light)
			{
				list.ForeColor = ThemeExtensions.LightListViewFore;
				list.BackColor = ThemeExtensions.LightListViewBack;
			}
			if (style == Style.Dark)
			{
				list.ForeColor = ThemeExtensions.DarkListViewFore;
				list.BackColor = ThemeExtensions.DarkListViewBack;
			}
		}

		// Token: 0x040000E0 RID: 224
		private static readonly Color DarkTextBack = Color.FromArgb(30, 30, 30);

		// Token: 0x040000E1 RID: 225
		private static readonly Color DarkTextFore = Color.FromArgb(170, 170, 170);

		// Token: 0x040000E2 RID: 226
		private static readonly Color DarkListViewBack = Color.FromArgb(30, 30, 30);

		// Token: 0x040000E3 RID: 227
		private static readonly Color DarkListViewFore = Color.FromArgb(170, 170, 170);

		// Token: 0x040000E4 RID: 228
		private static readonly Color DarkListBoxBack = Color.FromArgb(32, 32, 32);

		// Token: 0x040000E5 RID: 229
		private static readonly Color LightListBoxBack = Color.White;

		// Token: 0x040000E6 RID: 230
		private static readonly Color LightListViewBack = Color.FromArgb(200, 200, 200);

		// Token: 0x040000E7 RID: 231
		private static readonly Color LightListViewFore = Color.Black;

		// Token: 0x040000E8 RID: 232
		private static readonly Color LightTextFore = Color.Black;

		// Token: 0x040000E9 RID: 233
		private static readonly Color LightTextBack = Color.FromArgb(238, 238, 238);
	}
}
