using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000004 RID: 4
[StandardModule]
[HideModuleName]
[GeneratedCode("MyTemplate", "8.0.0.0")]
internal sealed class Class1
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002091 File Offset: 0x00000291
	static Class1()
	{
		// Note: this type is marked as 'beforefieldinit'.
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000009 RID: 9 RVA: 0x0000221C File Offset: 0x0000041C
	[HelpKeyword("My.Computer")]
	internal static Class0 Class0_0
	{
		[DebuggerHidden]
		get
		{
			return Class1.class2_0.Prop_0;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000A RID: 10 RVA: 0x00002234 File Offset: 0x00000434
	[HelpKeyword("My.Application")]
	internal static Form0 Form0_0
	{
		[DebuggerHidden]
		get
		{
			return Class1.class2_1.Prop_0;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000B RID: 11 RVA: 0x0000224C File Offset: 0x0000044C
	[HelpKeyword("My.User")]
	internal static User User_0
	{
		[DebuggerHidden]
		get
		{
			return Class1.class2_2.Prop_0;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000C RID: 12 RVA: 0x00002264 File Offset: 0x00000464
	[HelpKeyword("My.Forms")]
	internal static Class1.MyForms MyForms_0
	{
		[DebuggerHidden]
		get
		{
			return Class1.class2_3.Prop_0;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600000D RID: 13 RVA: 0x0000227C File Offset: 0x0000047C
	[HelpKeyword("My.WebServices")]
	internal static Class1.MyWebServices MyWebServices_0
	{
		[DebuggerHidden]
		get
		{
			return Class1.class2_4.Prop_0;
		}
	}

	// Token: 0x04000001 RID: 1
	private static readonly Class1.Class2<Class0> class2_0 = new Class1.Class2<Class0>();

	// Token: 0x04000002 RID: 2
	private static readonly Class1.Class2<Form0> class2_1 = new Class1.Class2<Form0>();

	// Token: 0x04000003 RID: 3
	private static readonly Class1.Class2<User> class2_2 = new Class1.Class2<User>();

	// Token: 0x04000004 RID: 4
	private static Class1.Class2<Class1.MyForms> class2_3 = new Class1.Class2<Class1.MyForms>();

	// Token: 0x04000005 RID: 5
	private static readonly Class1.Class2<Class1.MyWebServices> class2_4 = new Class1.Class2<Class1.MyWebServices>();

	// Token: 0x02000005 RID: 5
	[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class MyForms
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002294 File Offset: 0x00000494
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020C5 File Offset: 0x000002C5
		public Form1 Form1_0
		{
			get
			{
				this.form1_0 = Class1.MyForms.smethod_0<Form1>(this.form1_0);
				return this.form1_0;
			}
			set
			{
				if (value == this.form1_0)
				{
					return;
				}
				if (value != null)
				{
					throw new ArgumentException("Property can only be set to Nothing");
				}
				this.method_0<Form1>(ref this.form1_0);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B8 File Offset: 0x000004B8
		[DebuggerHidden]
		private static T smethod_0<T>(T gparam_0) where T : Form, new()
		{
			if (gparam_0 != null && !gparam_0.IsDisposed)
			{
				return gparam_0;
			}
			if (Class1.MyForms.hashtable_0 != null)
			{
				if (Class1.MyForms.hashtable_0.ContainsKey(typeof(T)))
				{
					throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
				}
			}
			else
			{
				Class1.MyForms.hashtable_0 = new Hashtable();
			}
			Class1.MyForms.hashtable_0.Add(typeof(T), null);
			T result;
			try
			{
				result = Activator.CreateInstance<T>();
			}
			catch (TargetInvocationException ex) when (ex.InnerException != null)
			{
				string resourceString = Utils.GetResourceString("WinForms_SeeInnerException", new string[]
				{
					ex.InnerException.Message
				});
				throw new InvalidOperationException(resourceString, ex.InnerException);
			}
			finally
			{
				Class1.MyForms.hashtable_0.Remove(typeof(T));
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C0 File Offset: 0x000005C0
		[DebuggerHidden]
		private void method_0<T>(ref T gparam_0) where T : Form
		{
			gparam_0.Dispose();
			gparam_0 = default(T);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020EB File Offset: 0x000002EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public MyForms()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023E8 File Offset: 0x000005E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object o)
		{
			return this.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002404 File Offset: 0x00000604
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002418 File Offset: 0x00000618
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal Type method_1()
		{
			return typeof(Class1.MyForms);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002430 File Offset: 0x00000630
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return this.ToString();
		}

		// Token: 0x04000006 RID: 6
		public Form1 form1_0;

		// Token: 0x04000007 RID: 7
		[ThreadStatic]
		private static Hashtable hashtable_0;
	}

	// Token: 0x02000006 RID: 6
	[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class MyWebServices
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023E8 File Offset: 0x000005E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override bool Equals(object o)
		{
			return this.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002404 File Offset: 0x00000604
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002444 File Offset: 0x00000644
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		internal Type method_0()
		{
			return typeof(Class1.MyWebServices);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002430 File Offset: 0x00000630
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override string ToString()
		{
			return this.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000245C File Offset: 0x0000065C
		[DebuggerHidden]
		private static T smethod_0<T>(T gparam_0) where T : new()
		{
			if (gparam_0 == null)
			{
				return Activator.CreateInstance<T>();
			}
			return gparam_0;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002478 File Offset: 0x00000678
		[DebuggerHidden]
		private void method_1<T>(ref T gparam_0)
		{
			gparam_0 = default(T);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020EB File Offset: 0x000002EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public MyWebServices()
		{
		}
	}

	// Token: 0x02000007 RID: 7
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ComVisible(false)]
	internal sealed class Class2<T> where T : new()
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002494 File Offset: 0x00000694
		internal T Prop_0
		{
			[DebuggerHidden]
			get
			{
				if (Class1.Class2<T>.gparam_0 == null)
				{
					Class1.Class2<T>.gparam_0 = Activator.CreateInstance<T>();
				}
				return Class1.Class2<T>.gparam_0;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000020EB File Offset: 0x000002EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public Class2()
		{
		}

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		[ThreadStatic]
		private static T gparam_0;
	}
}
