// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatMax
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
  internal class FlatMax : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private MouseState State;
    private int x;
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

    [DebuggerNonUserCode]
    static FlatMax()
    {
    }

    public FlatMax()
    {
      FlatMax.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._TextColor = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.White;
      this.Size = new Size(18, 18);
      this.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.Font = new Font("Marlett", 12f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatMax.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatMax.__ENCList.Count == FlatMax.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatMax.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatMax.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatMax.__ENCList[index1] = FlatMax.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatMax.__ENCList.RemoveRange(index1, checked (FlatMax.__ENCList.Count - index1));
          FlatMax.__ENCList.Capacity = FlatMax.__ENCList.Count;
        }
        FlatMax.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.State = MouseState.Over;
      this.Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.State = MouseState.Down;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.State = MouseState.None;
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.State = MouseState.Over;
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.x = e.X;
      this.Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      switch (this.FindForm().WindowState)
      {
        case FormWindowState.Normal:
          this.FindForm().WindowState = FormWindowState.Maximized;
          break;
        case FormWindowState.Maximized:
          this.FindForm().WindowState = FormWindowState.Normal;
          break;
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Size = new Size(18, 18);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Bitmap bitmap = new Bitmap(this.Width, this.Height);
      Graphics graphics1 = Graphics.FromImage((Image) bitmap);
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      Graphics graphics2 = graphics1;
      graphics2.SmoothingMode = SmoothingMode.HighQuality;
      graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics2.Clear(this.BackColor);
      graphics2.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
      if (this.FindForm().WindowState == FormWindowState.Maximized)
        graphics2.DrawString("1", this.Font, (Brush) new SolidBrush(this.TextColor), (RectangleF) new Rectangle(1, 1, this.Width, this.Height), Helpers.CenterSF);
      else if (this.FindForm().WindowState == FormWindowState.Normal)
        graphics2.DrawString("2", this.Font, (Brush) new SolidBrush(this.TextColor), (RectangleF) new Rectangle(1, 1, this.Width, this.Height), Helpers.CenterSF);
      switch (this.State)
      {
        case MouseState.Over:
          graphics2.FillRectangle((Brush) new SolidBrush(Color.FromArgb(30, Color.White)), rect);
          break;
        case MouseState.Down:
          graphics2.FillRectangle((Brush) new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
          break;
      }
      base.OnPaint(e);
      graphics1.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) bitmap, 0, 0);
      bitmap.Dispose();
    }
  }
}
