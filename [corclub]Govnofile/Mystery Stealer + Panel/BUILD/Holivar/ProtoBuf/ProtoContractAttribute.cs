using System;

namespace ProtoBuf
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class ProtoContractAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002697 File Offset: 0x00000897
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000269F File Offset: 0x0000089F
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000026B0 File Offset: 0x000008B0
		public int ImplicitFirstTag
		{
			get
			{
				return this.implicitFirstTag;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ImplicitFirstTag");
				}
				this.implicitFirstTag = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000026D1 File Offset: 0x000008D1
		public bool UseProtoMembersOnly
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000026DB File Offset: 0x000008DB
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000026E5 File Offset: 0x000008E5
		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000026F8 File Offset: 0x000008F8
		public ImplicitFields ImplicitFields
		{
			get
			{
				return this.implicitFields;
			}
			set
			{
				this.implicitFields = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002701 File Offset: 0x00000901
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000270A File Offset: 0x0000090A
		public bool InferTagFromName
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value);
				this.SetFlag(2, true);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000271C File Offset: 0x0000091C
		internal bool InferTagFromNameHasValue
		{
			get
			{
				return this.HasFlag(2);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002725 File Offset: 0x00000925
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000272D File Offset: 0x0000092D
		public int DataMemberOffset
		{
			get
			{
				return this.dataMemberOffset;
			}
			set
			{
				this.dataMemberOffset = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002736 File Offset: 0x00000936
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000273F File Offset: 0x0000093F
		public bool SkipConstructor
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002753 File Offset: 0x00000953
		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000275E File Offset: 0x0000095E
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000276B File Offset: 0x0000096B
		private void SetFlag(byte flag, bool value)
		{
			if (value)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000279A File Offset: 0x0000099A
		public bool EnumPassthru
		{
			get
			{
				return this.HasFlag(64);
			}
			set
			{
				this.SetFlag(64, value);
				this.SetFlag(128, true);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000027B1 File Offset: 0x000009B1
		internal bool EnumPassthruHasValue
		{
			get
			{
				return this.HasFlag(128);
			}
		}

		// Token: 0x04000080 RID: 128
		private string name;

		// Token: 0x04000081 RID: 129
		private int implicitFirstTag;

		// Token: 0x04000082 RID: 130
		private ImplicitFields implicitFields;

		// Token: 0x04000083 RID: 131
		private int dataMemberOffset;

		// Token: 0x04000084 RID: 132
		private byte flags;

		// Token: 0x04000085 RID: 133
		private const byte OPTIONS_InferTagFromName = 1;

		// Token: 0x04000086 RID: 134
		private const byte OPTIONS_InferTagFromNameHasValue = 2;

		// Token: 0x04000087 RID: 135
		private const byte OPTIONS_UseProtoMembersOnly = 4;

		// Token: 0x04000088 RID: 136
		private const byte OPTIONS_SkipConstructor = 8;

		// Token: 0x04000089 RID: 137
		private const byte OPTIONS_IgnoreListHandling = 16;

		// Token: 0x0400008A RID: 138
		private const byte OPTIONS_AsReferenceDefault = 32;

		// Token: 0x0400008B RID: 139
		private const byte OPTIONS_EnumPassthru = 64;

		// Token: 0x0400008C RID: 140
		private const byte OPTIONS_EnumPassthruHasValue = 128;
	}
}
