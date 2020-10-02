using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ProtoBuf
{
	// Token: 0x0200001A RID: 26
	internal sealed class Helpers
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000022E5 File Offset: 0x000004E5
		private Helpers()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000257E File Offset: 0x0000077E
		public static StringBuilder AppendLine(StringBuilder builder)
		{
			return builder.AppendLine();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002586 File Offset: 0x00000786
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002596 File Offset: 0x00000796
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message, object obj)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002596 File Offset: 0x00000796
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002598 File Offset: 0x00000798
		[Conditional("TRACE")]
		public static void TraceWriteLine(string message)
		{
			Trace.WriteLine(message);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002596 File Offset: 0x00000796
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002596 File Offset: 0x00000796
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message, params object[] args)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002596 File Offset: 0x00000796
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000B034 File Offset: 0x00009234
		public static void Sort(int[] keys, object[] values)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 1; i < keys.Length; i++)
				{
					if (keys[i - 1] > keys[i])
					{
						int num = keys[i];
						keys[i] = keys[i - 1];
						keys[i - 1] = num;
						object obj = values[i];
						values[i] = values[i - 1];
						values[i - 1] = obj;
						flag = true;
					}
				}
			}
			while (flag);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000025A0 File Offset: 0x000007A0
		public static void BlockCopy(byte[] from, int fromIndex, byte[] to, int toIndex, int count)
		{
			Buffer.BlockCopy(from, fromIndex, to, toIndex, count);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000025AD File Offset: 0x000007AD
		public static bool IsInfinity(float value)
		{
			return float.IsInfinity(value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000025B5 File Offset: 0x000007B5
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000025C0 File Offset: 0x000007C0
		internal static MethodInfo GetStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000025CB File Offset: 0x000007CB
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name, Type[] types)
		{
			if (types == null)
			{
				types = Helpers.EmptyTypes;
			}
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000025E3 File Offset: 0x000007E3
		internal static bool IsSubclassOf(Type type, Type baseClass)
		{
			return type.IsSubclassOf(baseClass);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000025EC File Offset: 0x000007EC
		public static bool IsInfinity(double value)
		{
			return double.IsInfinity(value);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000B084 File Offset: 0x00009284
		public static ProtoTypeCode GetTypeCode(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return (ProtoTypeCode)typeCode;
			}
			if (type == typeof(TimeSpan))
			{
				return ProtoTypeCode.TimeSpan;
			}
			if (type == typeof(Guid))
			{
				return ProtoTypeCode.Guid;
			}
			if (type == typeof(Uri))
			{
				return ProtoTypeCode.Uri;
			}
			if (type == typeof(byte[]))
			{
				return ProtoTypeCode.ByteArray;
			}
			if (type == typeof(Type))
			{
				return ProtoTypeCode.Type;
			}
			return ProtoTypeCode.Unknown;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000025F4 File Offset: 0x000007F4
		internal static Type GetUnderlyingType(Type type)
		{
			return Nullable.GetUnderlyingType(type);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000025FC File Offset: 0x000007FC
		internal static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002604 File Offset: 0x00000804
		internal static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000B140 File Offset: 0x00009340
		internal static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetGetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000B188 File Offset: 0x00009388
		internal static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetSetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000260C File Offset: 0x0000080C
		internal static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes, bool nonPublic)
		{
			return type.GetConstructor(nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public), null, parameterTypes, null);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002620 File Offset: 0x00000820
		internal static ConstructorInfo[] GetConstructors(Type type, bool nonPublic)
		{
			return type.GetConstructors(nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002631 File Offset: 0x00000831
		internal static PropertyInfo GetProperty(Type type, string name, bool nonPublic)
		{
			return type.GetProperty(name, nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002643 File Offset: 0x00000843
		internal static object ParseEnum(Type type, string value)
		{
			return Enum.Parse(type, value, true);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000B1D0 File Offset: 0x000093D0
		internal static MemberInfo[] GetInstanceFieldsAndProperties(Type type, bool publicOnly)
		{
			BindingFlags bindingAttr = publicOnly ? (BindingFlags.Instance | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			FieldInfo[] fields = type.GetFields(bindingAttr);
			MemberInfo[] array = new MemberInfo[fields.Length + properties.Length];
			properties.CopyTo(array, 0);
			fields.CopyTo(array, properties.Length);
			return array;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000B218 File Offset: 0x00009418
		internal static Type GetMemberType(MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				return ((FieldInfo)member).FieldType;
			}
			if (memberType != MemberTypes.Property)
			{
				return null;
			}
			return ((PropertyInfo)member).PropertyType;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000264D File Offset: 0x0000084D
		internal static bool IsAssignableFrom(Type target, Type type)
		{
			return target.IsAssignableFrom(type);
		}

		// Token: 0x04000058 RID: 88
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;
	}
}
