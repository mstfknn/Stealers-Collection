// Decompiled with JetBrains decompiler
// Type: Diger
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;

[StandardModule]
internal sealed class Diger
{
  public static object ReadKey(ref string hKey)
  {
    // ISSUE: unable to decompile the method.
  }

  public static string Hex2Ascii(string Text)
  {
    object Left = (object) null;
    object Counter;
    object LoopForResult;
    object CounterResult;
    if (ObjectFlowControl.ForLoopControl.ForLoopInitObj(Counter, (object) 1, (object) Strings.Len(Text), (object) 1, ref LoopForResult, ref CounterResult))
    {
      do
      {
        object Right = (object) Strings.Mid(Text, Conversions.ToInteger(CounterResult), 2);
        Left = Operators.ConcatenateObject(Left, (object) Strings.Chr(checked ((int) Math.Round(Conversion.Val(Operators.ConcatenateObject((object) "&h", Right))))));
        CounterResult = Operators.AddObject(CounterResult, (object) 1);
      }
      while (ObjectFlowControl.ForLoopControl.ForNextCheckObj(CounterResult, LoopForResult, ref CounterResult));
    }
    return Conversions.ToString(Left);
  }

  public static void dotovu()
  {
    // ISSUE: unable to decompile the method.
  }
}
