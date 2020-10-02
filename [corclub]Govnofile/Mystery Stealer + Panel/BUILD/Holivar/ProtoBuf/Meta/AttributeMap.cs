using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	// Token: 0x0200005E RID: 94
	internal abstract class AttributeMap
	{
		// Token: 0x060002BE RID: 702
		public abstract bool TryGet(string key, bool publicOnly, out object value);

		// Token: 0x060002BF RID: 703 RVA: 0x00003A82 File Offset: 0x00001C82
		public bool TryGet(string key, out object value)
		{
			return this.TryGet(key, true, out value);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002C0 RID: 704
		public abstract Type AttributeType { get; }

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public static AttributeMap[] Create(TypeModel model, Type type, bool inherit)
		{
			object[] customAttributes = type.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public static AttributeMap[] Create(TypeModel model, MemberInfo member, bool inherit)
		{
			object[] customAttributes = member.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public static AttributeMap[] Create(TypeModel model, Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(false);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002C4 RID: 708
		public abstract object Target { get; }

		// Token: 0x0200005F RID: 95
		private sealed class ReflectionAttributeMap : AttributeMap
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003A8D File Offset: 0x00001C8D
			public override object Target
			{
				get
				{
					return this.attribute;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003A95 File Offset: 0x00001C95
			public override Type AttributeType
			{
				get
				{
					return this.attribute.GetType();
				}
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
			public override bool TryGet(string key, bool publicOnly, out object value)
			{
				MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(this.attribute.GetType(), publicOnly);
				int i = 0;
				while (i < instanceFieldsAndProperties.Length)
				{
					MemberInfo memberInfo = instanceFieldsAndProperties[i];
					if (string.Equals(memberInfo.Name, key, StringComparison.OrdinalIgnoreCase))
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null)
						{
							value = propertyInfo.GetValue(this.attribute, null);
							return true;
						}
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							value = fieldInfo.GetValue(this.attribute);
							return true;
						}
						throw new NotSupportedException(memberInfo.GetType().Name);
					}
					else
					{
						i++;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x00003AA2 File Offset: 0x00001CA2
			public ReflectionAttributeMap(Attribute attribute)
			{
				this.attribute = attribute;
			}

			// Token: 0x04000137 RID: 311
			private readonly Attribute attribute;
		}
	}
}
