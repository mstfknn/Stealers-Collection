// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatContextMenuStrip
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Tht_Stealer
{
  internal class FlatContextMenuStrip : ContextMenuStrip
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();

    [DebuggerNonUserCode]
    static FlatContextMenuStrip()
    {
    }

    public FlatContextMenuStrip()
    {
      FlatContextMenuStrip.__ENCAddToList((object) this);
      this.Renderer = (ToolStripRenderer) new ToolStripProfessionalRenderer((ProfessionalColorTable) new FlatContextMenuStrip.TColorTable());
      this.ShowImageMargin = false;
      this.ForeColor = Color.White;
      this.Font = new Font("Segoe UI", 8f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatContextMenuStrip.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatContextMenuStrip.__ENCList.Count == FlatContextMenuStrip.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatContextMenuStrip.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatContextMenuStrip.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatContextMenuStrip.__ENCList[index1] = FlatContextMenuStrip.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatContextMenuStrip.__ENCList.RemoveRange(index1, checked (FlatContextMenuStrip.__ENCList.Count - index1));
          FlatContextMenuStrip.__ENCList.Capacity = FlatContextMenuStrip.__ENCList.Count;
        }
        FlatContextMenuStrip.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    }

    public class TColorTable : ProfessionalColorTable
    {
      private Color BackColor;
      private Color CheckedColor;
      private Color BorderColor;

      [Category("Colors")]
      public Color _BackColor
      {
        get
        {
          return this.BackColor;
        }
        set
        {
          this.BackColor = value;
        }
      }

      [Category("Colors")]
      public Color _CheckedColor
      {
        get
        {
          return this.CheckedColor;
        }
        set
        {
          this.CheckedColor = value;
        }
      }

      [Category("Colors")]
      public Color _BorderColor
      {
        get
        {
          return this.BorderColor;
        }
        set
        {
          this.BorderColor = value;
        }
      }

      public override Color ButtonSelectedBorder
      {
        get
        {
          return this.BackColor;
        }
      }

      public override Color CheckBackground
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color CheckPressedBackground
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color CheckSelectedBackground
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color ImageMarginGradientBegin
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color ImageMarginGradientEnd
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color ImageMarginGradientMiddle
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color MenuBorder
      {
        get
        {
          return this.BorderColor;
        }
      }

      public override Color MenuItemBorder
      {
        get
        {
          return this.BorderColor;
        }
      }

      public override Color MenuItemSelected
      {
        get
        {
          return this.CheckedColor;
        }
      }

      public override Color SeparatorDark
      {
        get
        {
          return this.BorderColor;
        }
      }

      public override Color ToolStripDropDownBackground
      {
        get
        {
          return this.BackColor;
        }
      }

      public TColorTable()
      {
        this.BackColor = Color.FromArgb(45, 47, 49);
        this.CheckedColor = Helpers._FlatColor;
        this.BorderColor = Color.FromArgb(53, 58, 60);
      }
    }
  }
}
