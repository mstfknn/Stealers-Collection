using System;

namespace ProtoBuf
{
	// Token: 0x0200002E RID: 46
	public sealed class SerializationContext
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00002F79 File Offset: 0x00001179
		internal void Freeze()
		{
			this.frozen = true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002F82 File Offset: 0x00001182
		private void ThrowIfFrozen()
		{
			if (this.frozen)
			{
				throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00002F97 File Offset: 0x00001197
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00002F9F File Offset: 0x0000119F
		public object Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (this.context != value)
				{
					this.ThrowIfFrozen();
					this.context = value;
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002FB7 File Offset: 0x000011B7
		static SerializationContext()
		{
			SerializationContext.@default.Freeze();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00002FCD File Offset: 0x000011CD
		internal static SerializationContext Default
		{
			get
			{
				return SerializationContext.@default;
			}
		}

		// Token: 0x040000C9 RID: 201
		private bool frozen;

		// Token: 0x040000CA RID: 202
		private object context;

		// Token: 0x040000CB RID: 203
		private static readonly SerializationContext @default = new SerializationContext();
	}
}
