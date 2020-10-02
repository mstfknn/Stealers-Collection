using System;
using System.ComponentModel;
using System.Drawing;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Windows.Forms;
using MetroSet_UI;
using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Forms;
using MysteryPanel.ViewModels;
using ReactiveUI;

namespace MysteryPanel.App.Views
{
	// Token: 0x02000035 RID: 53
	public partial class LoginView : MetroSetForm, IViewFor<AuthViewModel>, IViewFor, IActivatable
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000035BF File Offset: 0x000017BF
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000035C7 File Offset: 0x000017C7
		public AuthViewModel ViewModel { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000035D0 File Offset: 0x000017D0
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000035D8 File Offset: 0x000017D8
		object IViewFor.ViewModel
		{
			get
			{
				return this.ViewModel;
			}
			set
			{
				this.ViewModel = (value as AuthViewModel);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000806C File Offset: 0x0000626C
		public LoginView()
		{
			this.InitializeComponent();
			IntPtr handle = base.Handle;
			this.MainViewWindow = new MainView();
			this.ViewModel = new AuthViewModel(new ControlScheduler(this))
			{
				PanelVM = this.MainViewWindow.ViewModel
			};
			this.ViewModel.Load();
			this.Bind(this.ViewModel, (AuthViewModel vm) => vm.PanelVM.Account.Login, (LoginView view) => view.loginTb.Text, null, null, null);
			this.Bind(this.ViewModel, (AuthViewModel vm) => vm.PanelVM.Account.Password, (LoginView view) => view.passwordTb.Text, null, null, null);
			this.BindCommand(this.ViewModel, (AuthViewModel vm) => vm.SignInCommand, (LoginView view) => view.signInBtn, null);
			this.Bind(this.ViewModel, (AuthViewModel vm) => vm.IsAcceptingChanging, (LoginView view) => view.loginTb.Enabled, null, null, null);
			this.Bind(this.ViewModel, (AuthViewModel vm) => vm.IsAcceptingChanging, (LoginView view) => view.passwordTb.Enabled, null, null, null);
			this.Bind(this.ViewModel, (AuthViewModel vm) => vm.PanelVM.CurrentThemeStyle, (LoginView view) => view.loginStyle.Style, null, null, null);
			this.WhenAnyValue((LoginView view) => view.ViewModel.PanelVM.CurrentThemeStyle).Subscribe(delegate(Style current)
			{
				this.loginTb.ApplyTheme(current);
				this.passwordTb.ApplyTheme(current);
				this.signInBtn.ApplyTheme();
			});
		}

		// Token: 0x040000ED RID: 237
		public MainView MainViewWindow;
	}
}
