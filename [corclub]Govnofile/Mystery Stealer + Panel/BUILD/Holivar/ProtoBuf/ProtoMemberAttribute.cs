using System;
using System.Reflection;

namespace ProtoBuf
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ProtoMemberAttribute : Attribute, IComparable, IComparable<ProtoMemberAttribute>
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000287D File Offset: 0x00000A7D
		public int CompareTo(object other)
		{
			return this.CompareTo(other as ProtoMemberAttribute);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000B51C File Offset: 0x0000971C
		public int CompareTo(ProtoMemberAttribute other)
		{
			if (other == null)
			{
				return -1;
			}
			if (this == other)
			{
				return 0;
			}
			int num = this.tag.CompareTo(other.tag);
			if (num == 0)
			{
				num = string.CompareOrdinal(this.name, other.name);
			}
			return num;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000288B File Offset: 0x00000A8B
		public ProtoMemberAttribute(int tag) : this(tag, false)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002895 File Offset: 0x00000A95
		internal ProtoMemberAttribute(int tag, bool forced)
		{
			if (tag <= 0 && !forced)
			{
				throw new ArgumentOutOfRangeException("tag");
			}
			this.tag = tag;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000028B6 File Offset: 0x00000AB6
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000028BE File Offset: 0x00000ABE
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000028C7 File Offset: 0x00000AC7
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000028CF File Offset: 0x00000ACF
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000028D8 File Offset: 0x00000AD8
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000028E0 File Offset: 0x00000AE0
		internal void Rebase(int tag)
		{
			this.tag = tag;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000028E9 File Offset: 0x00000AE9
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000028F6 File Offset: 0x00000AF6
		public bool IsRequired
		{
			get
			{
				return (this.options & MemberSerializationOptions.Required) == MemberSerializationOptions.Required;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Required;
					return;
				}
				this.options &= ~MemberSerializationOptions.Required;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002919 File Offset: 0x00000B19
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002926 File Offset: 0x00000B26
		public bool IsPacked
		{
			get
			{
				return (this.options & MemberSerializationOptions.Packed) == MemberSerializationOptions.Packed;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Packed;
					return;
				}
				this.options &= ~MemberSerializationOptions.Packed;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002958 File Offset: 0x00000B58
		public bool OverwriteList
		{
			get
			{
				return (this.options & MemberSerializationOptions.OverwriteList) == MemberSerializationOptions.OverwriteList;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.OverwriteList;
					return;
				}
				this.options &= ~MemberSerializationOptions.OverwriteList;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002989 File Offset: 0x00000B89
		public bool AsReference
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReference) == MemberSerializationOptions.AsReference;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReference;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReference;
				}
				this.options |= MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000029BC File Offset: 0x00000BBC
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000029CB File Offset: 0x00000BCB
		internal bool AsReferenceHasValue
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReferenceHasValue) == MemberSerializationOptions.AsReferenceHasValue;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReferenceHasValue;
					return;
				}
				this.options &= ~MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000029EF File Offset: 0x00000BEF
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000029FC File Offset: 0x00000BFC
		public bool DynamicType
		{
			get
			{
				return (this.options & MemberSerializationOptions.DynamicType) == MemberSerializationOptions.DynamicType;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.DynamicType;
					return;
				}
				this.options &= ~MemberSerializationOptions.DynamicType;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002A1F File Offset: 0x00000C1F
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002A27 File Offset: 0x00000C27
		public MemberSerializationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x04000094 RID: 148
		internal MemberInfo Member;

		// Token: 0x04000095 RID: 149
		internal bool TagIsPinned;

		// Token: 0x04000096 RID: 150
		private string name;

		// Token: 0x04000097 RID: 151
		private DataFormat dataFormat;

		// Token: 0x04000098 RID: 152
		private int tag;

		// Token: 0x04000099 RID: 153
		private MemberSerializationOptions options;
	}
}
