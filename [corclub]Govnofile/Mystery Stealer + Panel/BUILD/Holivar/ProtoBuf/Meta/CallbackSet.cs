using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	// Token: 0x02000066 RID: 102
	public class CallbackSet
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00003C95 File Offset: 0x00001E95
		internal CallbackSet(MetaType metaType)
		{
			if (metaType == null)
			{
				throw new ArgumentNullException("metaType");
			}
			this.metaType = metaType;
		}

		// Token: 0x170000C3 RID: 195
		internal MethodInfo this[TypeModel.CallbackType callbackType]
		{
			get
			{
				switch (callbackType)
				{
				case TypeModel.CallbackType.BeforeSerialize:
					return this.beforeSerialize;
				case TypeModel.CallbackType.AfterSerialize:
					return this.afterSerialize;
				case TypeModel.CallbackType.BeforeDeserialize:
					return this.beforeDeserialize;
				case TypeModel.CallbackType.AfterDeserialize:
					return this.afterDeserialize;
				default:
					throw new ArgumentException("Callback type not supported: " + callbackType.ToString(), "callbackType");
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00010168 File Offset: 0x0000E368
		internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				if (parameterType != model.MapType(typeof(SerializationContext)) && parameterType != model.MapType(typeof(Type)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000101BC File Offset: 0x0000E3BC
		private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
		{
			this.metaType.ThrowIfFrozen();
			if (callback == null)
			{
				return callback;
			}
			if (callback.IsStatic)
			{
				throw new ArgumentException("Callbacks cannot be static", "callback");
			}
			if (callback.ReturnType != model.MapType(typeof(void)) || !CallbackSet.CheckCallbackParameters(model, callback))
			{
				throw CallbackSet.CreateInvalidCallbackSignature(callback);
			}
			return callback;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00003CB2 File Offset: 0x00001EB2
		internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
		{
			return new NotSupportedException("Invalid callback signature in " + method.DeclaringType.FullName + "." + method.Name);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00003CD9 File Offset: 0x00001ED9
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public MethodInfo BeforeSerialize
		{
			get
			{
				return this.beforeSerialize;
			}
			set
			{
				this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003CFB File Offset: 0x00001EFB
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00003D03 File Offset: 0x00001F03
		public MethodInfo BeforeDeserialize
		{
			get
			{
				return this.beforeDeserialize;
			}
			set
			{
				this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003D1D File Offset: 0x00001F1D
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00003D25 File Offset: 0x00001F25
		public MethodInfo AfterSerialize
		{
			get
			{
				return this.afterSerialize;
			}
			set
			{
				this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00003D3F File Offset: 0x00001F3F
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00003D47 File Offset: 0x00001F47
		public MethodInfo AfterDeserialize
		{
			get
			{
				return this.afterDeserialize;
			}
			set
			{
				this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003D61 File Offset: 0x00001F61
		public bool NonTrivial
		{
			get
			{
				return this.beforeSerialize != null || this.beforeDeserialize != null || this.afterSerialize != null || this.afterDeserialize != null;
			}
		}

		// Token: 0x04000140 RID: 320
		private readonly MetaType metaType;

		// Token: 0x04000141 RID: 321
		private MethodInfo beforeSerialize;

		// Token: 0x04000142 RID: 322
		private MethodInfo afterSerialize;

		// Token: 0x04000143 RID: 323
		private MethodInfo beforeDeserialize;

		// Token: 0x04000144 RID: 324
		private MethodInfo afterDeserialize;
	}
}
