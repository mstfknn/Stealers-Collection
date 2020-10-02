// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatButton
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
  internal class FlatButton : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private bool _Rounded;
    private MouseState State;
    private Color _BaseColor;
    private Color _TextColor;

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
    static FlatButton()
    {
    }

    public FlatButton()
    {
      FlatButton.__ENCAddToList((object) this);
      this._Rounded = false;
      this.State = MouseState.None;
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
      List<WeakReference> list = FlatButton.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatButton.__ENCList.Count == FlatButton.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatButton.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatButton.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatButton.__ENCList[index1] = FlatButton.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatButton.__ENCList.RemoveRange(index1, checked (FlatButton.__ENCList.Count - index1));
          FlatButton.__ENCList.Capacity = FlatButton.__ENCList.Count;
        }
        FlatButton.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      GraphicsPath graphicsPath = new GraphicsPath();
      Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
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
            GraphicsPath path = Helpers.RoundRec(rectangle, 6);
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
            break;
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rectangle);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
            break;
          }
        case MouseState.Over:
          if (this.Rounded)
          {
            GraphicsPath path = Helpers.RoundRec(rectangle, 6);
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.FillPath((Brush) new SolidBrush(Color.FromArgb(20, Color.White)), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
            break;
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rectangle);
            graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(20, Color.White)), rectangle);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
            break;
          }
        case MouseState.Down:
          if (this.Rounded)
          {
            GraphicsPath path = Helpers.RoundRec(rectangle, 6);
            graphics.FillPath((Brush) new SolidBrush(this._BaseColor), path);
            graphics.FillPath((Brush) new SolidBrush(Color.FromArgb(20, Color.Black)), path);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
          }
          else
          {
            graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rectangle);
            graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(20, Color.Black)), rectangle);
            graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) rectangle, Helpers.CenterSF);
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
