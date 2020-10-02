// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatComboBox
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
  internal class FlatComboBox : ComboBox
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private int _StartIndex;
    private int x;
    private int y;
    private MouseState State;
    private Color _BaseColor;
    private Color _BGColor;
    private Color _HoverColor;

    [Category("Colors")]
    public Color HoverColor
    {
      get
      {
        return this._HoverColor;
      }
      set
      {
        this._HoverColor = value;
      }
    }

    private int StartIndex
    {
      get
      {
        return this._StartIndex;
      }
      set
      {
        this._StartIndex = value;
        try
        {
          this.SelectedIndex = value;
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
        this.Invalidate();
      }
    }

    [DebuggerNonUserCode]
    static FlatComboBox()
    {
    }

    public FlatComboBox()
    {
      this.DrawItem += new DrawItemEventHandler(this.DrawItem_);
      FlatComboBox.__ENCAddToList((object) this);
      this._StartIndex = 0;
      this.State = MouseState.None;
      this._BaseColor = Color.FromArgb(25, 27, 29);
      this._BGColor = Color.FromArgb(45, 47, 49);
      this._HoverColor = Color.FromArgb(35, 168, 109);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.BackColor = Color.FromArgb(45, 45, 48);
      this.ForeColor = Color.White;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.Cursor = Cursors.Hand;
      this.StartIndex = 0;
      this.ItemHeight = 18;
      this.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatComboBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatComboBox.__ENCList.Count == FlatComboBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatComboBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatComboBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatComboBox.__ENCList[index1] = FlatComboBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatComboBox.__ENCList.RemoveRange(index1, checked (FlatComboBox.__ENCList.Count - index1));
          FlatComboBox.__ENCList.Capacity = FlatComboBox.__ENCList.Count;
        }
        FlatComboBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.x = e.Location.X;
      this.y = e.Location.Y;
      this.Invalidate();
      if (e.X < checked (this.Width - 41))
        this.Cursor = Cursors.IBeam;
      else
        this.Cursor = Cursors.Hand;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      this.Invalidate();
      if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
        return;
      this.Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.Invalidate();
    }

    public void DrawItem_(object sender, DrawItemEventArgs e)
    {
      if (e.Index < 0)
        return;
      e.DrawBackground();
      e.DrawFocusRectangle();
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        e.Graphics.FillRectangle((Brush) new SolidBrush(this._HoverColor), e.Bounds);
      else
        e.Graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), e.Bounds);
      e.Graphics.DrawString(this.GetItemText(RuntimeHelpers.GetObjectValue(this.Items[e.Index])), new Font("Segoe UI", 8f), Brushes.White, (RectangleF) new Rectangle(checked (e.Bounds.X + 2), checked (e.Bounds.Y + 2), e.Bounds.Width, e.Bounds.Height));
      e.Graphics.Dispose();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Height = 18;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = this.Width;
      this.H = this.Height;
      Rectangle rect1 = new Rectangle(0, 0, this.W, this.H);
      Rectangle rect2 = new Rectangle(checked (this.W - 40), 0, this.W, this.H);
      GraphicsPath path = new GraphicsPath();
      GraphicsPath graphicsPath = new GraphicsPath();
      Graphics graphics = Helpers.G;
      graphics.Clear(Color.FromArgb(45, 45, 48));
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.FillRectangle((Brush) new SolidBrush(this._BGColor), rect1);
      path.Reset();
      path.AddRectangle(rect2);
      graphics.SetClip(path);
      graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect2);
      graphics.ResetClip();
      graphics.DrawLine(Pens.White, checked (this.W - 10), 6, checked (this.W - 30), 6);
      graphics.DrawLine(Pens.White, checked (this.W - 10), 12, checked (this.W - 30), 12);
      graphics.DrawLine(Pens.White, checked (this.W - 10), 18, checked (this.W - 30), 18);
      graphics.DrawString(this.Text, this.Font, Brushes.White, (PointF) new Point(4, 6), Helpers.NearSF);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
