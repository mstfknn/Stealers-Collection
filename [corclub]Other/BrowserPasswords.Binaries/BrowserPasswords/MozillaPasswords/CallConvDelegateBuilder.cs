using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x02000023 RID: 35
	public class CallConvDelegateBuilder
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00004240 File Offset: 0x00002440
		private static string GetNewDelegateName(MethodInfo delegateMethodInfo, CallingConvention callConv)
		{
			return delegateMethodInfo.Name + callConv.ToString();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004268 File Offset: 0x00002468
		private static Type CreateDelegate(MethodInfo delegateMethodInfo, CallingConvention callConv, string delegName)
		{
			AssemblyName assemblyName = new AssemblyName(delegName);
			string text = assemblyName.Name + "Module.dll";
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(text, text, true);
			TypeBuilder typeBuilder = moduleBuilder.DefineType(assemblyName.Name + "Delegate", TypeAttributes.Sealed | TypeAttributes.AutoClass, typeof(MulticastDelegate));
			Type returnType = delegateMethodInfo.ReturnType;
			Type[] returnTypeRequiredCustomModifiers = null;
			Type[] returnTypeOptionalCustomModifiers = null;
			switch (callConv)
			{
			case CallingConvention.Winapi:
				returnTypeOptionalCustomModifiers = new Type[]
				{
					typeof(CallConvStdcall)
				};
				break;
			case CallingConvention.Cdecl:
				returnTypeOptionalCustomModifiers = new Type[]
				{
					typeof(CallConvCdecl)
				};
				break;
			case CallingConvention.StdCall:
				returnTypeOptionalCustomModifiers = new Type[]
				{
					typeof(CallConvStdcall)
				};
				break;
			case CallingConvention.ThisCall:
				returnTypeOptionalCustomModifiers = new Type[]
				{
					typeof(CallConvThiscall)
				};
				break;
			case CallingConvention.FastCall:
				returnTypeOptionalCustomModifiers = new Type[]
				{
					typeof(CallConvFastcall)
				};
				break;
			}
			List<Type> list = new List<Type>();
			List<Type[]> list2 = new List<Type[]>();
			List<Type[]> list3 = new List<Type[]>();
			foreach (ParameterInfo parameterInfo in delegateMethodInfo.GetParameters())
			{
				list.Add(parameterInfo.ParameterType);
				list2.Add(parameterInfo.GetRequiredCustomModifiers());
				list3.Add(parameterInfo.GetOptionalCustomModifiers());
			}
			ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			});
			constructorBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("EndInvoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, CallingConventions.Standard);
			methodBuilder.SetParameters(new Type[]
			{
				typeof(IAsyncResult)
			});
			methodBuilder.SetReturnType(returnType);
			methodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			ConstructorInfo constructor = typeof(MarshalAsAttribute).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(UnmanagedType),
				typeof(VarEnum),
				typeof(Type),
				typeof(UnmanagedType),
				typeof(short),
				typeof(int),
				typeof(string),
				typeof(Type),
				typeof(string),
				typeof(int)
			}, null);
			MethodBuilder methodBuilder2 = typeBuilder.DefineMethod("Invoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, CallingConventions.Standard, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, list.ToArray(), list2.ToArray(), list3.ToArray());
			checked
			{
				foreach (ParameterInfo parameterInfo2 in delegateMethodInfo.GetParameters())
				{
					ParameterBuilder parameterBuilder = methodBuilder2.DefineParameter(parameterInfo2.Position + 1, parameterInfo2.Attributes, parameterInfo2.Name);
					foreach (MarshalAsAttribute marshalAsAttribute in parameterInfo2.GetCustomAttributes(typeof(MarshalAsAttribute), false))
					{
						parameterBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor, new object[]
						{
							marshalAsAttribute.Value,
							marshalAsAttribute.SafeArraySubType,
							marshalAsAttribute.SafeArrayUserDefinedSubType,
							marshalAsAttribute.ArraySubType,
							marshalAsAttribute.SizeParamIndex,
							marshalAsAttribute.SizeConst,
							marshalAsAttribute.MarshalType,
							marshalAsAttribute.MarshalTypeRef,
							marshalAsAttribute.MarshalCookie,
							marshalAsAttribute.IidParameterIndex
						}));
					}
				}
				methodBuilder2.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
				list.Add(typeof(AsyncCallback));
				list.Add(typeof(object));
				if (list3.Count > 0)
				{
					list3.Add(null);
					list3.Add(null);
				}
				if (list2.Count > 0)
				{
					list2.Add(null);
					list2.Add(null);
				}
				MethodBuilder methodBuilder3 = typeBuilder.DefineMethod("BeginInvoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, CallingConventions.Standard, typeof(IAsyncResult), returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, list.ToArray(), list2.ToArray(), list3.ToArray());
				foreach (ParameterInfo parameterInfo3 in delegateMethodInfo.GetParameters())
				{
					ParameterBuilder parameterBuilder2 = methodBuilder3.DefineParameter(parameterInfo3.Position + 1, parameterInfo3.Attributes, parameterInfo3.Name);
					foreach (MarshalAsAttribute marshalAsAttribute2 in parameterInfo3.GetCustomAttributes(typeof(MarshalAsAttribute), false))
					{
						parameterBuilder2.SetCustomAttribute(new CustomAttributeBuilder(constructor, new object[]
						{
							marshalAsAttribute2.Value,
							marshalAsAttribute2.SafeArraySubType,
							marshalAsAttribute2.SafeArrayUserDefinedSubType,
							marshalAsAttribute2.ArraySubType,
							marshalAsAttribute2.SizeParamIndex,
							marshalAsAttribute2.SizeConst,
							marshalAsAttribute2.MarshalType,
							marshalAsAttribute2.MarshalTypeRef,
							marshalAsAttribute2.MarshalCookie,
							marshalAsAttribute2.IidParameterIndex
						}));
					}
				}
				methodBuilder3.DefineParameter(list.Count - 1, ParameterAttributes.None, "callback");
				methodBuilder3.DefineParameter(list.Count, ParameterAttributes.None, "o");
				methodBuilder3.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
				return typeBuilder.CreateType();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000486C File Offset: 0x00002A6C
		public static MulticastDelegate CreateDelegateInstance(MethodInfo delegateMethodInfo, CallingConvention callConv, object objThis)
		{
			string newDelegateName = CallConvDelegateBuilder.GetNewDelegateName(delegateMethodInfo, callConv);
			Type type;
			if (CallConvDelegateBuilder.generated_delegates.ContainsKey(newDelegateName))
			{
				type = CallConvDelegateBuilder.generated_delegates[newDelegateName];
			}
			else
			{
				type = CallConvDelegateBuilder.CreateDelegate(delegateMethodInfo, callConv, newDelegateName);
				CallConvDelegateBuilder.generated_delegates.Add(newDelegateName, type);
			}
			return (MulticastDelegate)Activator.CreateInstance(type, new object[]
			{
				RuntimeHelpers.GetObjectValue(objThis),
				delegateMethodInfo.MethodHandle.GetFunctionPointer()
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000048EC File Offset: 0x00002AEC
		public static MulticastDelegate CreateDelegateInstance(Type methodContainer, string methodName, CallingConvention callConv, object objThis)
		{
			MethodInfo method = methodContainer.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			return CallConvDelegateBuilder.CreateDelegateInstance(method, callConv, RuntimeHelpers.GetObjectValue(objThis));
		}

		// Token: 0x04000104 RID: 260
		private static Dictionary<string, Type> generated_delegates = new Dictionary<string, Type>();
	}
}
