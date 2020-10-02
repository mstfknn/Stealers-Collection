using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GrandSteal.Server.Data.Extensions.Nulls;
using GrandSteal.Server.Models;
using MysteryPanel.App;
using ReactiveUI;

namespace MysteryPanel.ViewModels
{
	// Token: 0x02000008 RID: 8
	public class AuthViewModel : SchedulerResolver
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002862 File Offset: 0x00000A62
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000286A File Offset: 0x00000A6A
		public PanelViewModel PanelVM
		{
			[CompilerGenerated]
			get
			{
				return this.<PanelVM>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<PanelVM>k__BackingField, value))
				{
					return;
				}
				this.<PanelVM>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.PanelVM);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000288F File Offset: 0x00000A8F
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002897 File Offset: 0x00000A97
		public IObservable<bool> CanSignIn
		{
			[CompilerGenerated]
			get
			{
				return this.<CanSignIn>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<CanSignIn>k__BackingField, value))
				{
					return;
				}
				this.<CanSignIn>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.CanSignIn);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000028C4 File Offset: 0x00000AC4
		public IDisposable LogInSubscriber
		{
			[CompilerGenerated]
			get
			{
				return this.<LogInSubscriber>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<LogInSubscriber>k__BackingField, value))
				{
					return;
				}
				this.<LogInSubscriber>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.LogInSubscriber);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000028E9 File Offset: 0x00000AE9
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000028F1 File Offset: 0x00000AF1
		public bool IsAcceptingChanging
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAcceptingChanging>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (this.<IsAcceptingChanging>k__BackingField == value)
				{
					return;
				}
				this.<IsAcceptingChanging>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.IsAcceptingChanging);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C44 File Offset: 0x00001E44
		public AuthViewModel(IScheduler scheduler)
		{
			this.<IsAcceptingChanging>k__BackingField = true;
			base..ctor(scheduler);
			this.CanSignIn = this.WhenAnyValue((AuthViewModel vm) => vm.PanelVM.Account.Login, (AuthViewModel vm) => vm.PanelVM.Account.Password, (AuthViewModel vm) => vm.IsAcceptingChanging, (string user, string pass, bool accepting) => !string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass) && user.Length >= 3 && pass.Length >= 8 && accepting).DistinctUntilChanged<bool>();
			this.SignInCommand = ReactiveCommand.Create(new Action(this.SignIn), this.CanSignIn, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public override void Load()
		{
			this.PanelVM.OnSystemLogInvoked("AuthViewModel", "Loaded");
			this.PanelVM.Account.Login = AccountSettings.Default.CachedLogin;
			this.PanelVM.Account.Password = AccountSettings.Default.CachedPassword;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002913 File Offset: 0x00000B13
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000291B File Offset: 0x00000B1B
		public ReactiveCommand<Unit, Unit> SignInCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<SignInCommand>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.<SignInCommand>k__BackingField, value))
				{
					return;
				}
				this.<SignInCommand>k__BackingField = value;
				this.<>OnPropertyChanged(<>PropertyChangedEventArgs.SignInCommand);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003E00 File Offset: 0x00002000
		private async void SignIn()
		{
			try
			{
				this.IsAcceptingChanging = false;
				UserAccount userAccount = await this.PanelVM.ServerWCF.Authorizate(this.PanelVM.Account);
				if (userAccount.IsNotNull<UserAccount>() && userAccount.Activated)
				{
					if (base.OwnerControl.Control.Visible)
					{
						this.PanelVM.OnSystemLogInvoked("AuthViewModel", "Authorized");
						base.OwnerControl.Control.Visible = false;
						this.LogInSubscriber = Observable.Interval(TimeSpan.FromSeconds(60.0)).Subscribe(delegate(long _)
						{
							this.PanelVM.Synchronization.Send(delegate(object __)
							{
								this.SignIn();
							}, null);
						});
						this.PanelVM.Account.Activated = userAccount.Activated;
						this.PanelVM.Account.ActivationDate = userAccount.ActivationDate;
						this.PanelVM.Account.ID = userAccount.ID;
						this.PanelVM.Account.Login = userAccount.Login;
						this.PanelVM.Account.Password = userAccount.Password;
						this.PanelVM.Account.RegistrationDate = userAccount.RegistrationDate;
						AccountSettings.Default.CachedLogin = userAccount.Login;
						AccountSettings.Default.CachedPassword = userAccount.Password;
						AccountSettings.Default.Save();
						this.PanelVM.Load();
					}
				}
				else if (base.OwnerControl.Control.Visible)
				{
					MessageBox.Show("Login or password in not valid");
				}
				else
				{
					MessageBox.Show("Your account is disconnected");
					Environment.Exit(0);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error occured during signing in");
			}
			finally
			{
				this.IsAcceptingChanging = true;
			}
		}
	}
}
