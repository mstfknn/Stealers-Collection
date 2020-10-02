using System;

namespace ProtoBuf.Meta
{
	// Token: 0x02000071 RID: 113
	public class TypeFormatEventArgs : EventArgs
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000445F File Offset: 0x0000265F
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00004467 File Offset: 0x00002667
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					if (this.typeFixed)
					{
						throw new InvalidOperationException("The type is fixed and cannot be changed");
					}
					this.type = value;
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000448C File Offset: 0x0000268C
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00004494 File Offset: 0x00002694
		public string FormattedName
		{
			get
			{
				return this.formattedName;
			}
			set
			{
				if (this.formattedName != value)
				{
					if (!this.typeFixed)
					{
						throw new InvalidOperationException("The formatted-name is fixed and cannot be changed");
					}
					this.formattedName = value;
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000044BE File Offset: 0x000026BE
		internal TypeFormatEventArgs(string formattedName)
		{
			if (Helpers.IsNullOrEmpty(formattedName))
			{
				throw new ArgumentNullException("formattedName");
			}
			this.formattedName = formattedName;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000044E0 File Offset: 0x000026E0
		internal TypeFormatEventArgs(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			this.typeFixed = true;
		}

		// Token: 0x0400017A RID: 378
		private Type type;

		// Token: 0x0400017B RID: 379
		private string formattedName;

		// Token: 0x0400017C RID: 380
		private readonly bool typeFixed;
	}
}
