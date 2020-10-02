// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatNumeric
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
  internal class FlatNumeric : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private MouseState State;
    private int x;
    private int y;
    private long _Value;
    private long _Min;
    private long _Max;
    private bool Bool;
    private Color _BaseColor;
    private Color _ButtonColor;

    public long Value
    {
      get
      {
        return this._Value;
      }
      set
      {
        if (value <= this._Max & value >= this._Min)
          this._Value = value;
        this.Invalidate();
      }
    }

    public long Maximum
    {
      get
      {
        return this._Max;
      }
      set
      {
        if (value > this._Min)
          this._Max = value;
        if (this._Value > this._Max)
          this._Value = this._Max;
        this.Invalidate();
      }
    }

    public long Minimum
    {
      get
      {
        return this._Min;
      }
      set
      {
        if (value < this._Max)
          this._Min = value;
        if (this._Value < this._Min)
          this._Value = this.Minimum;
        this.Invalidate();
      }
    }

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
    public Color ButtonColor
    {
      get
      {
        return this._ButtonColor;
      }
      set
      {
        this._ButtonColor = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatNumeric()
    {
    }

    public FlatNumeric()
    {
      FlatNumeric.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._ButtonColor = Helpers._FlatColor;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.Font = new Font("Segoe UI", 10f);
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.ForeColor = Color.White;
      this._Min = 0L;
      this._Max = 9999999L;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatNumeric.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatNumeric.__ENCList.Count == FlatNumeric.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatNumeric.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatNumeric.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatNumeric.__ENCList[index1] = FlatNumeric.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatNumeric.__ENCList.RemoveRange(index1, checked (FlatNumeric.__ENCList.Count - index1));
          FlatNumeric.__ENCList.Capacity = FlatNumeric.__ENCList.Count;
        }
        FlatNumeric.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.x = e.Location.X;
      this.y = e.Location.Y;
      this.Invalidate();
      if (e.X < checked (this.Width - 23))
        this.Cursor = Cursors.IBeam;
      else
        this.Cursor = Cursors.Hand;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.x > checked (this.Width - 21) && this.x < checked (this.Width - 3))
      {
        if (this.y < 15)
        {
          if (checked (this.Value + 1L) <= this._Max)
            this._Value = checked (this._Value + 1L);
        }
        else if (checked (this.Value - 1L) >= this._Min)
          this._Value = checked (this._Value - 1L);
      }
      else
      {
        this.Bool = !this.Bool;
        this.Focus();
      }
      this.Invalidate();
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      try
      {
        if (this.Bool)
          this._Value = Conversions.ToLong(Conversions.ToString(this._Value) + e.KeyChar.ToString());
        if (this._Value > this._Max)
          this._Value = this._Max;
        this.Invalidate();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyCode != Keys.Back)
        return;
      this.Value = 0L;
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Height = 30;
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
      graphics1.Clear(this.BackColor);
      graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect1);
      Graphics graphics2 = graphics1;
      SolidBrush solidBrush = new SolidBrush(this._ButtonColor);
      Rectangle rectangle = new Rectangle(checked (this.Width - 24), 0, 24, this.H);
      Rectangle rect2 = rectangle;
      graphics2.FillRectangle((Brush) solidBrush, rect2);
      Graphics graphics3 = graphics1;
      string s1 = "+";
      Font font1 = new Font("Segoe UI", 12f);
      Brush white1 = Brushes.White;
      Point point1 = new Point(checked (this.Width - 12), 8);
      PointF point2 = (PointF) point1;
      StringFormat format1 = Helpers.CenterSF;
      graphics3.DrawString(s1, font1, white1, point2, format1);
      Graphics graphics4 = graphics1;
      string s2 = "-";
      Font font2 = new Font("Segoe UI", 10f, FontStyle.Bold);
      Brush white2 = Brushes.White;
      point1 = new Point(checked (this.Width - 12), 22);
      PointF point3 = (PointF) point1;
      StringFormat format2 = Helpers.CenterSF;
      graphics4.DrawString(s2, font2, white2, point3, format2);
      Graphics graphics5 = graphics1;
      string s3 = Conversions.ToString(this.Value);
      Font font3 = this.Font;
      Brush white3 = Brushes.White;
      rectangle = new Rectangle(5, 1, this.W, this.H);
      RectangleF layoutRectangle = (RectangleF) rectangle;
      StringFormat format3 = new StringFormat()
      {
        LineAlignment = StringAlignment.Center
      };
      graphics5.DrawString(s3, font3, white3, layoutRectangle, format3);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
