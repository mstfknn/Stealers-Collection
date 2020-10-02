// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatStatusBar
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
  internal class FlatStatusBar : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private bool _ShowTimeDate;
    private Color _BaseColor;
    private Color _TextColor;
    private Color _RectColor;

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

    [Category("Colors")]
    public Color TextColor
    {
      get
      {
        return this._TextColor;
      }
      set
      {
        this._TextColor = value;
      }
    }

    [Category("Colors")]
    public Color RectColor
    {
      get
      {
        return this._RectColor;
      }
      set
      {
        this._RectColor = value;
      }
    }

    public bool ShowTimeDate
    {
      get
      {
        return this._ShowTimeDate;
      }
      set
      {
        this._ShowTimeDate = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatStatusBar()
    {
    }

    public FlatStatusBar()
    {
      FlatStatusBar.__ENCAddToList((object) this);
      this._ShowTimeDate = false;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._TextColor = Color.White;
      this._RectColor = Helpers._FlatColor;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.Font = new Font("Segoe UI", 8f);
      this.ForeColor = Color.White;
      this.Size = new Size(this.Width, 20);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatStatusBar.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatStatusBar.__ENCList.Count == FlatStatusBar.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatStatusBar.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatStatusBar.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatStatusBar.__ENCList[index1] = FlatStatusBar.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatStatusBar.__ENCList.RemoveRange(index1, checked (FlatStatusBar.__ENCList.Count - index1));
          FlatStatusBar.__ENCList.Capacity = FlatStatusBar.__ENCList.Count;
        }
        FlatStatusBar.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void CreateHandle()
    {
      base.CreateHandle();
      this.Dock = DockStyle.Bottom;
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.Invalidate();
    }

    public string GetTimeDate()
    {
      return Conversions.ToString(DateTime.Now.Date) + " " + Conversions.ToString(DateTime.Now.Hour) + ":" + Conversions.ToString(DateTime.Now.Minute);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = this.Width;
      this.H = this.Height;
      Rectangle rect1 = new Rectangle(0, 0, this.W, this.H);
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BaseColor);
      graphics1.FillRectangle((Brush) new SolidBrush(this.BaseColor), rect1);
      Graphics graphics2 = graphics1;
      string text = this.Text;
      Font font1 = this.Font;
      Brush white = Brushes.White;
      Rectangle rectangle = new Rectangle(10, 4, this.W, this.H);
      RectangleF layoutRectangle1 = (RectangleF) rectangle;
      StringFormat format1 = Helpers.NearSF;
      graphics2.DrawString(text, font1, white, layoutRectangle1, format1);
      Graphics graphics3 = graphics1;
      SolidBrush solidBrush1 = new SolidBrush(this._RectColor);
      rectangle = new Rectangle(4, 4, 4, 14);
      Rectangle rect2 = rectangle;
      graphics3.FillRectangle((Brush) solidBrush1, rect2);
      if (this.ShowTimeDate)
      {
        Graphics graphics4 = graphics1;
        string timeDate = this.GetTimeDate();
        Font font2 = this.Font;
        SolidBrush solidBrush2 = new SolidBrush(this._TextColor);
        rectangle = new Rectangle(-4, 2, this.W, this.H);
        RectangleF layoutRectangle2 = (RectangleF) rectangle;
        StringFormat format2 = new StringFormat()
        {
          Alignment = StringAlignment.Far,
          LineAlignment = StringAlignment.Center
        };
        graphics4.DrawString(timeDate, font2, (Brush) solidBrush2, layoutRectangle2, format2);
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
