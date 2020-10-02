// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatLabel
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Tht_Stealer
{
  internal class FlatLabel : Label
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();

    [DebuggerNonUserCode]
    static FlatLabel()
    {
    }

    public FlatLabel()
    {
      FlatLabel.__ENCAddToList((object) this);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.Font = new Font("Segoe UI", 8f);
      this.ForeColor = Color.White;
      this.BackColor = Color.Transparent;
      this.Text = this.Text;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatLabel.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatLabel.__ENCList.Count == FlatLabel.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatLabel.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatLabel.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatLabel.__ENCList[index1] = FlatLabel.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatLabel.__ENCList.RemoveRange(index1, checked (FlatLabel.__ENCList.Count - index1));
          FlatLabel.__ENCList.Capacity = FlatLabel.__ENCList.Count;
        }
        FlatLabel.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.Invalidate();
    }
  }
}
