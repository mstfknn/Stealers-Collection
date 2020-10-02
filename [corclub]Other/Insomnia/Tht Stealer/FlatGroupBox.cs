// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatGroupBox
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Tht_Stealer
{
  internal class FlatGroupBox : ContainerControl
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private bool _ShowText;
    private Color _BaseColor;

    [Category("Colors")]
    public Color BaseColor
    {
      get
      {
        return this._BaseColor;
      }
      set
      {
        this._BaseColor = value;
      }
    }

    public bool ShowText
    {
      get
      {
        return this._ShowText;
      }
      set
      {
        this._ShowText = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatGroupBox()
    {
    }

    public FlatGroupBox()
    {
      FlatGroupBox.__ENCAddToList((object) this);
      this._ShowText = true;
      this._BaseColor = Color.FromArgb(60, 70, 73);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.Transparent;
      this.Size = new Size(240, 180);
      this.Font = new Font("Segoe ui", 10f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatGroupBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatGroupBox.__ENCList.Count == FlatGroupBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatGroupBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatGroupBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatGroupBox.__ENCList[index1] = FlatGroupBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatGroupBox.__ENCList.RemoveRange(index1, checked (FlatGroupBox.__ENCList.Count - index1));
          FlatGroupBox.__ENCList.Capacity = FlatGroupBox.__ENCList.Count;
        }
        FlatGroupBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      GraphicsPath graphicsPath1 = new GraphicsPath();
      GraphicsPath graphicsPath2 = new GraphicsPath();
      GraphicsPath graphicsPath3 = new GraphicsPath();
      Rectangle Rectangle = new Rectangle(8, 8, checked (this.W - 16), checked (this.H - 16));
      Graphics graphics = Helpers.G;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.Clear(this.BackColor);
      GraphicsPath path1 = Helpers.RoundRec(Rectangle, 8);
      graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path1);
      GraphicsPath path2 = Helpers.DrawArrow(28, 2, false);
      graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path2);
      GraphicsPath path3 = Helpers.DrawArrow(28, 8, true);
      graphics.FillPath((Brush) new SolidBrush(Color.FromArgb(60, 70, 73)), path3);
      if (this.ShowText)
        graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(Helpers._FlatColor), (RectangleF) new Rectangle(16, 16, this.W, this.H), Helpers.NearSF);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
