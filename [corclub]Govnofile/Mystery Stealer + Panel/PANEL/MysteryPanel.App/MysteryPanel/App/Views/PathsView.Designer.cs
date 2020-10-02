namespace MysteryPanel.App.Views
{
	// Token: 0x0200003D RID: 61
	public partial class PathsView : global::MetroSet_UI.Forms.MetroSetForm, global::ReactiveUI.IViewFor<global::MysteryPanel.ViewModels.PanelViewModel>, global::ReactiveUI.IViewFor, global::ReactiveUI.IActivatable
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000388E File Offset: 0x00001A8E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00012BA4 File Offset: 0x00010DA4
		private void InitializeComponent()
		{
			this.viewerStyle = new global::MetroSet_UI.StyleManager();
			this.viewerControlBox = new global::MetroSet_UI.Controls.MetroSetControlBox();
			this.logsList = new global::System.Windows.Forms.ListBox();
			base.SuspendLayout();
			this.viewerStyle.MetroForm = this;
			this.viewerStyle.Style = global::MetroSet_UI.Design.Style.Dark;
			this.viewerStyle.ThemeAuthor = "Narwin";
			this.viewerStyle.ThemeName = "MetroDark";
			this.viewerControlBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.viewerControlBox.CloseHoverBackColor = global::System.Drawing.Color.FromArgb(183, 40, 40);
			this.viewerControlBox.CloseHoverForeColor = global::System.Drawing.Color.White;
			this.viewerControlBox.CloseNormalForeColor = global::System.Drawing.Color.Gray;
			this.viewerControlBox.CloseParent = false;
			this.viewerControlBox.DisabledForeColor = global::System.Drawing.Color.Silver;
			this.viewerControlBox.Location = new global::System.Drawing.Point(889, 6);
			this.viewerControlBox.MaximizeBox = false;
			this.viewerControlBox.MaximizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.viewerControlBox.MaximizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.viewerControlBox.MaximizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.viewerControlBox.MinimizeBox = false;
			this.viewerControlBox.MinimizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.viewerControlBox.MinimizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.viewerControlBox.MinimizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.viewerControlBox.Name = "viewerControlBox";
			this.viewerControlBox.Size = new global::System.Drawing.Size(100, 25);
			this.viewerControlBox.Style = global::MetroSet_UI.Design.Style.Dark;
			this.viewerControlBox.StyleManager = this.viewerStyle;
			this.viewerControlBox.TabIndex = 2;
			this.viewerControlBox.ThemeAuthor = "Narwin";
			this.viewerControlBox.ThemeName = "MetroDark";
			this.viewerControlBox.CloseButtonClicked += new global::System.EventHandler(this.viewerControlBox_CloseButtonClicked);
			this.logsList.BackColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.logsList.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.logsList.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.logsList.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.logsList.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.logsList.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.logsList.ItemHeight = 16;
			this.logsList.Location = new global::System.Drawing.Point(12, 70);
			this.logsList.Name = "logsList";
			this.logsList.ScrollAlwaysVisible = true;
			this.logsList.Size = new global::System.Drawing.Size(976, 368);
			this.logsList.TabIndex = 6;
			this.logsList.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.logsListView_MouseDoubleClick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(1000, 450);
			base.Controls.Add(this.logsList);
			base.Controls.Add(this.viewerControlBox);
			this.MaximumSize = new global::System.Drawing.Size(1000, 450);
			this.MinimumSize = new global::System.Drawing.Size(1000, 450);
			base.Name = "PathsView";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Design.Style.Dark;
			base.StyleManager = this.viewerStyle;
			this.Text = "LogPaths Viewer";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.PathsView_FormClosing);
			base.ResumeLayout(false);
		}

		// Token: 0x04000184 RID: 388
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000185 RID: 389
		private global::MetroSet_UI.StyleManager viewerStyle;

		// Token: 0x04000186 RID: 390
		private global::MetroSet_UI.Controls.MetroSetControlBox viewerControlBox;

		// Token: 0x04000187 RID: 391
		private global::System.Windows.Forms.ListBox logsList;
	}
}
