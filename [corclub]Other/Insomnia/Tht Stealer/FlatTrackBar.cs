// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatTrackBar
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
  [DefaultEvent("Scroll")]
  internal class FlatTrackBar : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private int Val;
    private bool Bool;
    private Rectangle Track;
    private Rectangle Knob;
    private FlatTrackBar._Style Style_;
    private int _Minimum;
    private int _Maximum;
    private int _Value;
    private bool _ShowValue;
    private Color BaseColor;
    private Color _TrackColor;
    private Color SliderColor;
    private Color _HatchColor;

    public FlatTrackBar._Style Style
    {
      get
      {
        return this.Style_;
      }
      set
      {
        this.Style_ = value;
      }
    }

    [Category("Colors")]
    public Color TrackColor
    {
      get
      {
        return this._TrackColor;
      }
      set
      {
        this._TrackColor = value;
      }
    }

    [Category("Colors")]
    public Color HatchColor
    {
      get
      {
        return this._HatchColor;
      }
      set
      {
        this._HatchColor = value;
      }
    }

    public int Minimum
    {
      get
      {
        int num;
        return num;
      }
      set
      {
        if (value >= 0)
          ;
        this._Minimum = value;
        if (value > this._Value)
          this._Value = value;
        if (value > this._Maximum)
          this._Maximum = value;
        this.Invalidate();
      }
    }

    public int Maximum
    {
      get
      {
        return this._Maximum;
      }
      set
      {
        if (value >= 0)
          ;
        this._Maximum = value;
        if (value < this._Value)
          this._Value = value;
        if (value < this._Minimum)
          this._Minimum = value;
        this.Invalidate();
      }
    }

    public int Value
    {
      get
      {
        return this._Value;
      }
      set
      {
        if (value == this._Value)
          return;
        if (value <= this._Maximum && value >= this._Minimum || !true)
          ;
        this._Value = value;
        this.Invalidate();
        FlatTrackBar.ScrollEventHandler scrollEventHandler = this.ScrollEvent;
        if (scrollEventHandler == null)
          return;
        scrollEventHandler((object) this);
      }
    }

    public bool ShowValue
    {
      get
      {
        return this._ShowValue;
      }
      set
      {
        this._ShowValue = value;
      }
    }

    public event FlatTrackBar.ScrollEventHandler Scroll;

    [DebuggerNonUserCode]
    static FlatTrackBar()
    {
    }

    public FlatTrackBar()
    {
      FlatTrackBar.__ENCAddToList((object) this);
      this._Maximum = 10;
      this._ShowValue = false;
      this.BaseColor = Color.FromArgb(45, 47, 49);
      this._TrackColor = Helpers._FlatColor;
      this.SliderColor = Color.FromArgb(25, 27, 29);
      this._HatchColor = Color.FromArgb(23, 148, 92);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.Height = 18;
      this.BackColor = Color.FromArgb(60, 70, 73);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatTrackBar.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatTrackBar.__ENCList.Count == FlatTrackBar.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatTrackBar.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatTrackBar.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatTrackBar.__ENCList[index1] = FlatTrackBar.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatTrackBar.__ENCList.RemoveRange(index1, checked (FlatTrackBar.__ENCList.Count - index1));
          FlatTrackBar.__ENCList.Capacity = FlatTrackBar.__ENCList.Count;
        }
        FlatTrackBar.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.Val = checked ((int) Math.Round(unchecked ((double) checked (this._Value - this._Minimum) / (double) checked (this._Maximum - this._Minimum) * (double) checked (this.Width - 11))));
      this.Track = new Rectangle(this.Val, 0, 10, 20);
      this.Bool = this.Track.Contains(e.Location);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if ((!this.Bool || e.X <= -1 || e.X >= checked (this.Width + 1)) && !false)
        return;
      this.Value = checked (this._Minimum + (int) Math.Round(unchecked ((double) checked (this._Maximum - this._Minimum) * (double) e.X / (double) this.Width)));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Bool = false;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyCode == Keys.Subtract)
      {
        if (this.Value == 0)
          return;
        this.Value = checked (this.Value - 1);
      }
      else if (e.KeyCode == Keys.Add && this.Value != this._Maximum)
        this.Value = checked (this.Value + 1);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Height = 23;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      Rectangle rect1 = new Rectangle(1, 6, checked (this.W - 2), 8);
      GraphicsPath path1 = new GraphicsPath();
      GraphicsPath path2 = new GraphicsPath();
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      this.Val = checked ((int) Math.Round(unchecked ((double) checked (this._Value - this._Minimum) / (double) checked (this._Maximum - this._Minimum) * (double) checked (this.W - 10))));
      this.Track = new Rectangle(this.Val, 0, 10, 20);
      this.Knob = new Rectangle(this.Val, 4, 11, 14);
      path1.AddRectangle(rect1);
      graphics1.SetClip(path1);
      Graphics graphics2 = graphics1;
      SolidBrush solidBrush1 = new SolidBrush(this.BaseColor);
      Rectangle rectangle = new Rectangle(0, 7, this.W, 8);
      Rectangle rect2 = rectangle;
      graphics2.FillRectangle((Brush) solidBrush1, rect2);
      Graphics graphics3 = graphics1;
      SolidBrush solidBrush2 = new SolidBrush(this._TrackColor);
      rectangle = new Rectangle(0, 7, checked (this.Track.X + this.Track.Width), 8);
      Rectangle rect3 = rectangle;
      graphics3.FillRectangle((Brush) solidBrush2, rect3);
      graphics1.ResetClip();
      HatchBrush hatchBrush1 = new HatchBrush(HatchStyle.Plaid, this.HatchColor, this._TrackColor);
      Graphics graphics4 = graphics1;
      HatchBrush hatchBrush2 = hatchBrush1;
      rectangle = new Rectangle(-10, 7, checked (this.Track.X + this.Track.Width), 8);
      Rectangle rect4 = rectangle;
      graphics4.FillRectangle((Brush) hatchBrush2, rect4);
      switch (this.Style)
      {
        case FlatTrackBar._Style.Slider:
          path2.AddRectangle(this.Track);
          graphics1.FillPath((Brush) new SolidBrush(this.SliderColor), path2);
          break;
        case FlatTrackBar._Style.Knob:
          path2.AddEllipse(this.Knob);
          graphics1.FillPath((Brush) new SolidBrush(this.SliderColor), path2);
          break;
      }
      if (this.ShowValue)
      {
        Graphics graphics5 = graphics1;
        string s = Conversions.ToString(this.Value);
        Font font = new Font("Segoe UI", 8f);
        Brush white = Brushes.White;
        rectangle = new Rectangle(1, 6, this.W, this.H);
        RectangleF layoutRectangle = (RectangleF) rectangle;
        StringFormat format = new StringFormat()
        {
          Alignment = StringAlignment.Far,
          LineAlignment = StringAlignment.Far
        };
        graphics5.DrawString(s, font, white, layoutRectangle, format);
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }

    [Flags]
    public enum _Style
    {
      Slider = 0,
      Knob = 1,
    }

    public delegate void ScrollEventHandler(object sender);
  }
}
