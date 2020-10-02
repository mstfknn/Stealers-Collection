using System;
using System.Security;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	// Token: 0x0200014A RID: 330
	internal interface ICredentialsPrompt : IDisposable
	{
		// Token: 0x06000AA7 RID: 2727
		DialogResult ShowDialog();

		// Token: 0x06000AA8 RID: 2728
		DialogResult ShowDialog(IntPtr owner);

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AA9 RID: 2729
		// (set) Token: 0x06000AAA RID: 2730
		string Username { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AAB RID: 2731
		// (set) Token: 0x06000AAC RID: 2732
		string Password { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AAD RID: 2733
		// (set) Token: 0x06000AAE RID: 2734
		SecureString SecurePassword { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AAF RID: 2735
		// (set) Token: 0x06000AB0 RID: 2736
		string Title { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AB1 RID: 2737
		// (set) Token: 0x06000AB2 RID: 2738
		string Message { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AB3 RID: 2739
		// (set) Token: 0x06000AB4 RID: 2740
		bool SaveChecked { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AB5 RID: 2741
		// (set) Token: 0x06000AB6 RID: 2742
		bool GenericCredentials { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AB7 RID: 2743
		// (set) Token: 0x06000AB8 RID: 2744
		bool ShowSaveCheckBox { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AB9 RID: 2745
		// (set) Token: 0x06000ABA RID: 2746
		int ErrorCode { get; set; }
	}
}
