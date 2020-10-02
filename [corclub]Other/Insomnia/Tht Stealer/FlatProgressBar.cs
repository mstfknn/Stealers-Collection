// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatProgressBar
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using Microsoft.VisualBasic.CompilerServices;
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
  internal class FlatProgressBar : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private int _Value;
    private int _Maximum;
    private Color _BaseColor;
    private Color _ProgressColor;
    private Color _DarkerProgress;

    [Category("Control")]
    public int Maximum
    {
      get
      {
        return this._Maximum;
      }
      set
      {
        if (value < this._Value)
          this._Value = value;
        this._Maximum = value;
        this.Invalidate();
      }
    }

    [Category("Control")]
    public int Value
    {
      get
      {
        if (this._Value == 0)
          return 0;
        else
          return this._Value;
      }
      set
      {
        if (value > this._Maximum)
        {
          value = this._Maximum;
          this.Invalidate();
        }
        this._Value = value;
        this.Invalidate();
      }
    }

    [Category("Colors")]
    public Color ProgressColor
    {
      get
      {
        return this._ProgressColor;
      }
      set
      {
        this._ProgressColor = value;
      }
    }

    [Category("Colors")]
    public Color DarkerProgress
    {
      get
      {
        return this._DarkerProgress;
      }
      set
      {
        this._DarkerProgress = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatProgressBar()
    {
    }

    public FlatProgressBar()
    {
      FlatProgressBar.__ENCAddToList((object) this);
      this._Value = 0;
      this._Maximum = 100;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._ProgressColor = Helpers._FlatColor;
      this._DarkerProgress = Color.FromArgb(23, 148, 92);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Height = 42;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatProgressBar.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatProgressBar.__ENCList.Count == FlatProgressBar.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatProgressBar.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatProgressBar.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatProgressBar.__ENCList[index1] = FlatProgressBar.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatProgressBar.__ENCList.RemoveRange(index1, checked (FlatProgressBar.__ENCList.Count - index1));
          FlatProgressBar.__ENCList.Capacity = FlatProgressBar.__ENCList.Count;
        }
        FlatProgressBar.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Height = 42;
    }

    protected override void CreateHandle()
    {
      base.CreateHandle();
      this.Height = 42;
    }

    public void Increment(int Amount)
    {
      this.Value = checked (this.Value + Amount);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      Rectangle rect1 = new Rectangle(0, 24, this.W, this.H);
      GraphicsPath path1 = new GraphicsPath();
      GraphicsPath graphicsPath1 = new GraphicsPath();
      GraphicsPath graphicsPath2 = new GraphicsPath();
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      int num = checked ((int) Math.Round(unchecked ((double) this._Value / (double) this._Maximum * (double) this.Width)));
      switch (this.Value)
      {
        case 0:
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect1);
          graphics1.FillRectangle((Brush) new SolidBrush(this._ProgressColor), new Rectangle(0, 24, checked (num - 1), checked (this.H - 1)));
          break;
        case 100:
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect1);
          graphics1.FillRectangle((Brush) new SolidBrush(this._ProgressColor), new Rectangle(0, 24, checked (num - 1), checked (this.H - 1)));
          break;
        default:
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect1);
          GraphicsPath graphicsPath3 = path1;
          Rectangle rectangle = new Rectangle(0, 24, checked (num - 1), checked (this.H - 1));
          Rectangle rect2 = rectangle;
          graphicsPath3.AddRectangle(rect2);
          graphics1.FillPath((Brush) new SolidBrush(this._ProgressColor), path1);
          HatchBrush hatchBrush1 = new HatchBrush(HatchStyle.Plaid, this._DarkerProgress, this._ProgressColor);
          Graphics graphics2 = graphics1;
          HatchBrush hatchBrush2 = hatchBrush1;
          rectangle = new Rectangle(0, 24, checked (num - 1), checked (this.H - 1));
          Rectangle rect3 = rectangle;
          graphics2.FillRectangle((Brush) hatchBrush2, rect3);
          GraphicsPath path2 = Helpers.RoundRec(new Rectangle(checked (num - 18), 0, 34, 16), 4);
          graphics1.FillPath((Brush) new SolidBrush(this._BaseColor), path2);
          GraphicsPath path3 = Helpers.DrawArrow(checked (num - 9), 16, true);
          graphics1.FillPath((Brush) new SolidBrush(this._BaseColor), path3);
          Graphics graphics3 = graphics1;
          string s = Conversions.ToString(this.Value);
          Font font = new Font("Segoe UI", 10f);
          SolidBrush solidBrush = new SolidBrush(this._ProgressColor);
          rectangle = new Rectangle(checked (num - 11), -2, this.W, this.H);
          RectangleF layoutRectangle = (RectangleF) rectangle;
          StringFormat format = Helpers.NearSF;
          graphics3.DrawString(s, font, (Brush) solidBrush, layoutRectangle, format);
          break;
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
