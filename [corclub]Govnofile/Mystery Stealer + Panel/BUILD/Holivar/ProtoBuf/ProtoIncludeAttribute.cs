using System;
using System.ComponentModel;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoIncludeAttribute : Attribute
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00002834 File Offset: 0x00000A34
		public ProtoIncludeAttribute(int tag, Type knownType) : this(tag, (knownType == null) ? "" : knownType.AssemblyQualifiedName)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000B4CC File Offset: 0x000096CC
		public ProtoIncludeAttribute(int tag, string knownTypeName)
		{
			if (tag <= 0)
			{
				throw new ArgumentOutOfRangeException("tag", "Tags must be positive integers");
			}
			if (Helpers.IsNullOrEmpty(knownTypeName))
			{
				throw new ArgumentNullException("knownTypeName", "Known type cannot be blank");
			}
			this.tag = tag;
			this.knownTypeName = knownTypeName;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000284D File Offset: 0x00000A4D
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002855 File Offset: 0x00000A55
		public string KnownTypeName
		{
			get
			{
				return this.knownTypeName;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000285D File Offset: 0x00000A5D
		public Type KnownType
		{
			get
			{
				return TypeModel.ResolveKnownType(this.KnownTypeName, null, null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002874 File Offset: 0x00000A74
		[DefaultValue(DataFormat.Default)]
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.dataFormat = value;
			}
		}

		// Token: 0x04000091 RID: 145
		private readonly int tag;

		// Token: 0x04000092 RID: 146
		private readonly string knownTypeName;

		// Token: 0x04000093 RID: 147
		private DataFormat dataFormat;
	}
}
