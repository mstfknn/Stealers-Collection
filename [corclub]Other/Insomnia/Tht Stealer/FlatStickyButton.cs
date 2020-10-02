// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatStickyButton
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using System;
using System.Collections;
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
  internal class FlatStickyButton : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private MouseState State;
    private bool _Rounded;
    private Color _BaseColor;
    private Color _TextColor;

    private Rectangle Rect
    {
      get
      {
        return new Rectangle(this.Left, this.Top, this.Width, this.Height);
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

    [Category("Options")]
    public bool Rounded
    {
      get
      {
        return this._Rounded;
      }
      set
      {
        this._Rounded = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatStickyButton()
    {
    }

    public FlatStickyButton()
    {
      FlatStickyButton.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._Rounded = false;
      this._BaseColor = Helpers._FlatColor;
      this._TextColor = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.Size = new Size(106, 32);
      this.BackColor = Color.Transparent;
      this.Font = new Font("Segoe UI", 12f);
      this.Cursor = Cursors.Hand;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatStickyButton.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatStickyButton.__ENCList.Count == FlatStickyButton.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatStickyButton.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatStickyButton.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatStickyButton.__ENCList[index1] = FlatStickyButton.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatStickyButton.__ENCList.RemoveRange(index1, checked (FlatStickyButton.__ENCList.Count - index1));
          FlatStickyButton.__ENCList.Capacity = FlatStickyButton.__ENCList.Count;
        }
        FlatStickyButton.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.State = MouseState.Down;
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.State = MouseState.Over;
      this.Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.State = MouseState.Over;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.State = MouseState.None;
      this.Invalidate();
    }

    private bool[] GetConnectedSides()
    {
      bool[] flagArray = new bool[4]
      {
        false,
        false,
        false,
        false
      };
      try
      {
        foreach (Control control in this.Parent.Controls)
        {
          if (control is FlatStickyButton && !(control == this | !this.Rect.IntersectsWith(this.Rect)))
          {
            double a = Math.Atan2((double) checked (this.Left - control.Left), (double) checked (this.Top - control.Top)) * 2.0 / Math.PI;
            if ((double) (checked ((long) Math.Round(a)) / 1L) == a)
              flagArray[checked ((int) Math.Round(unchecked (a + 1.0)))] = true;
          }
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      return flagArray;
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = this.Width;
      this.H = this.Height;
      GraphicsPath graphicsPath1 = new GraphicsPath();
      bool[] connectedSides = this.GetConnectedSides();
      GraphicsPath graphicsPath2 = Helpers.RoundRect(0.0f, 0.0f, (float) this.W, (float) this.H, 0.3f, !(connectedSides[2] | connectedSides[1]), !(connectedSides[1] | connectedSides[0]), !(connectedSides[3] | connectedSides[0]), !(connectedSides[3] | connectedSides[2]));
      Rectangle rect = new Rectangle(0, 0, this.W, this.H);
      Graphics graphics = Helpers.G;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.Clear(this.BackColor);
      switch (this.State)
      {
        case MouseState.None:
          if (this.Rounded)
          {
            GraphicsPath path = graphicsPath2;
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
            break;
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
            break;
          }
        case MouseState.Over:
          if (this.Rounded)
          {
            GraphicsPath path = graphicsPath2;
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.FillPath((Brush) new SolidBrush(Color.FromArgb(20, Color.White)), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
            break;
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
            graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(20, Color.White)), rect);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
            break;
          }
        case MouseState.Down:
          if (this.Rounded)
          {
            GraphicsPath path = graphicsPath2;
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.FillPath((Brush) new SolidBrush(Color.FromArgb(20, Color.Black)), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
            graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(20, Color.Black)), rect);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rect, Helpers.CenterSF);
          }
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
