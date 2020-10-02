using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroSet_UI;
using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Forms;
using MysteryPanel.ViewModels;
using ReactiveUI;

namespace MysteryPanel.App.Views
{
	// Token: 0x0200003D RID: 61
	public partial class PathsView : MetroSetForm, IViewFor<PanelViewModel>, IViewFor, IActivatable
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00003858 File Offset: 0x00001A58
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00003860 File Offset: 0x00001A60
		public PanelViewModel ViewModel { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00003869 File Offset: 0x00001A69
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00003871 File Offset: 0x00001A71
		object IViewFor.ViewModel
		{
			get
			{
				return this.ViewModel;
			}
			set
			{
				this.ViewModel = (value as PanelViewModel);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00012988 File Offset: 0x00010B88
		public PathsView(PanelViewModel mainViewModel)
		{
			this.InitializeComponent();
			this.ViewModel = mainViewModel;
			this.WhenAnyValue((PathsView view) => view.ViewModel.ShowingPaths).Subscribe(delegate(bool visible)
			{
				base.Visible = visible;
			});
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentThemeStyle, (PathsView view) => view.viewerStyle.Style, null, null, null);
			this.Events().Closing.Subscribe(delegate(CancelEventArgs _)
			{
				this.ViewModel.ChangeShowing();
			});
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentPaths, (PathsView view) => view.logsList.DataSource, null, null, null);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00012B4C File Offset: 0x00010D4C
		private void logsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && this.logsList.SelectedIndex != -1)
			{
				string text = this.logsList.Items[this.logsList.SelectedIndex].ToString();
				if (Directory.Exists(text))
				{
					Process.Start(text);
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000387F File Offset: 0x00001A7F
		private void PathsView_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00003881 File Offset: 0x00001A81
		private void viewerControlBox_CloseButtonClicked(object sender, EventArgs e)
		{
			this.ViewModel.ChangeShowing();
		}
	}
}
