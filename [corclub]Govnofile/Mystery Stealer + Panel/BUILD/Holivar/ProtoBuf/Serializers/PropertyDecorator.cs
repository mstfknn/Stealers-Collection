using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004E RID: 78
	internal sealed class PropertyDecorator : ProtoDecoratorBase
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000363B File Offset: 0x0000183B
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00003147 File Offset: 0x00001347
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00003643 File Offset: 0x00001843
		public PropertyDecorator(TypeModel model, Type forType, PropertyInfo property, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.property = property;
			PropertyDecorator.SanityCheck(model, property, tail, out this.readOptionsWriteValue, true, true);
			this.shadowSetter = PropertyDecorator.GetShadowSetter(model, property);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F074 File Offset: 0x0000D274
		private static void SanityCheck(TypeModel model, PropertyInfo property, IProtoSerializer tail, out bool writeValue, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			writeValue = (tail.ReturnsValue && (PropertyDecorator.GetShadowSetter(model, property) != null || (property.CanWrite && Helpers.GetSetMethod(property, nonPublic, allowInternal) != null)));
			if (!property.CanRead || Helpers.GetGetMethod(property, nonPublic, allowInternal) == null)
			{
				throw new InvalidOperationException("Cannot serialize property without a get accessor");
			}
			if (!writeValue && (!tail.RequiresOldValue || Helpers.IsValueType(tail.ExpectedType)))
			{
				throw new InvalidOperationException("Cannot apply changes to property " + property.DeclaringType.FullName + "." + property.Name);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F120 File Offset: 0x0000D320
		private static MethodInfo GetShadowSetter(TypeModel model, PropertyInfo property)
		{
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(property.ReflectedType, "Set" + property.Name, new Type[]
			{
				property.PropertyType
			});
			if (instanceMethod == null || !instanceMethod.IsPublic || instanceMethod.ReturnType != model.MapType(typeof(void)))
			{
				return null;
			}
			return instanceMethod;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00003679 File Offset: 0x00001879
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.property.GetValue(value, null);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F180 File Offset: 0x0000D380
		public override object Read(object value, ProtoReader source)
		{
			object value2 = this.Tail.RequiresOldValue ? this.property.GetValue(value, null) : null;
			object obj = this.Tail.Read(value2, source);
			if (this.readOptionsWriteValue && obj != null)
			{
				if (this.shadowSetter == null)
				{
					this.property.SetValue(value, obj, null);
				}
				else
				{
					this.shadowSetter.Invoke(value, new object[]
					{
						obj
					});
				}
			}
			return null;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		internal static bool CanWrite(TypeModel model, MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.CanWrite || PropertyDecorator.GetShadowSetter(model, propertyInfo) != null;
			}
			return member is FieldInfo;
		}

		// Token: 0x0400010E RID: 270
		private readonly PropertyInfo property;

		// Token: 0x0400010F RID: 271
		private readonly Type forType;

		// Token: 0x04000110 RID: 272
		private readonly bool readOptionsWriteValue;

		// Token: 0x04000111 RID: 273
		private readonly MethodInfo shadowSetter;
	}
}
