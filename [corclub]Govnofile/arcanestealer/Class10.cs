// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using System;
using System.Reflection;

internal class Class10
{
  internal static Module module_0;

  internal static void b94Wv9qqPacbX(int typemdt)
  {
    Type type = Class10.module_0.ResolveType(33554432 + typemdt);
    foreach (FieldInfo field in type.GetFields())
    {
      MethodInfo method = (MethodInfo) Class10.module_0.ResolveMethod(field.MetadataToken + 100663296);
      field.SetValue((object) null, (object) (MulticastDelegate) Delegate.CreateDelegate(type, method));
    }
  }

  public Class10()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  static Class10()
  {
    Class11.ARXWv9qzu32dU();
    Class10.module_0 = typeof (Class10).Assembly.ManifestModule;
  }

  internal delegate void Delegate0(object o);
}
