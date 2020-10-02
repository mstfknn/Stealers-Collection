using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

// Token: 0x02000002 RID: 2
[GeneratedCode("MyTemplate", "8.0.0.0")]
[EditorBrowsable(EditorBrowsableState.Never)]
internal class Form0 : WindowsFormsApplicationBase
{
	// Token: 0x06000004 RID: 4 RVA: 0x000021E4 File Offset: 0x000003E4
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerHidden]
	[STAThread]
	[MethodImpl(MethodImplOptions.NoOptimization)]
	internal static void Main(string[] args)
	{
		try
		{
			Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering);
		}
		finally
		{
		}
		Class1.Form0_0.Run(args);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002052 File Offset: 0x00000252
	[DebuggerStepThrough]
	public Form0() : base(AuthenticationMode.Windows)
	{
		this.IsSingleInstance = false;
		this.EnableVisualStyles = true;
		this.SaveMySettingsOnExit = true;
		this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002077 File Offset: 0x00000277
	[DebuggerStepThrough]
	protected override void OnCreateMainForm()
	{
		this.MainForm = Class1.MyForms_0.Form1_0;
	}
}
