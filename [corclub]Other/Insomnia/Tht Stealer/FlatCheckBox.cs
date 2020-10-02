// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatCheckBox
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
  [DefaultEvent("CheckedChanged")]
  internal class FlatCheckBox : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private MouseState State;
    private FlatCheckBox._Options O;
    private bool _Checked;
    private Color _BaseColor;
    private Color _BorderColor;
    private Color _TextColor;

    public bool Checked
    {
      get
      {
        return this._Checked;
      }
      set
      {
        this._Checked = value;
        this.Invalidate();
      }
    }

    [Category("Options")]
    public FlatCheckBox._Options Options
    {
      get
      {
        return this.O;
      }
      set
      {
        this.O = value;
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
    public Color BorderColor
    {
      get
      {
        return this._BorderColor;
      }
      set
      {
        this._BorderColor = value;
      }
    }

    public event FlatCheckBox.CheckedChangedEventHandler CheckedChanged;

    [DebuggerNonUserCode]
    static FlatCheckBox()
    {
    }

    public FlatCheckBox()
    {
      FlatCheckBox.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._BorderColor = Helpers._FlatColor;
      this._TextColor = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Cursor = Cursors.Hand;
      this.Font = new Font("Segoe UI", 10f);
      this.Size = new Size(112, 22);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatCheckBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatCheckBox.__ENCList.Count == FlatCheckBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatCheckBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatCheckBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatCheckBox.__ENCList[index1] = FlatCheckBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatCheckBox.__ENCList.RemoveRange(index1, checked (FlatCheckBox.__ENCList.Count - index1));
          FlatCheckBox.__ENCList.Capacity = FlatCheckBox.__ENCList.Count;
        }
        FlatCheckBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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

    protected override void OnClick(EventArgs e)
    {
      this._Checked = !this._Checked;
      FlatCheckBox.CheckedChangedEventHandler changedEventHandler = this.CheckedChangedEvent;
      if (changedEventHandler != null)
        changedEventHandler((object) this);
      base.OnClick(e);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Height = 22;
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
      Rectangle rect = new Rectangle(0, 2, checked (this.Height - 5), checked (this.Height - 5));
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      switch (this.O)
      {
        case FlatCheckBox._Options.Style1:
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
          switch (this.State)
          {
            case MouseState.Over:
              graphics1.DrawRectangle(new Pen(this._BorderColor), rect);
              break;
            case MouseState.Down:
              graphics1.DrawRectangle(new Pen(this._BorderColor), rect);
              break;
          }
          Rectangle rectangle1;
          if (this.Checked)
          {
            Graphics graphics2 = graphics1;
            string s = "ü";
            Font font = new Font("Wingdings", 18f);
            SolidBrush solidBrush = new SolidBrush(this._BorderColor);
            rectangle1 = new Rectangle(5, 7, checked (this.H - 9), checked (this.H - 9));
            RectangleF layoutRectangle = (RectangleF) rectangle1;
            StringFormat format = Helpers.CenterSF;
            graphics2.DrawString(s, font, (Brush) solidBrush, layoutRectangle, format);
          }
          if (!this.Enabled)
          {
            graphics1.FillRectangle((Brush) new SolidBrush(Color.FromArgb(54, 58, 61)), rect);
            Graphics graphics2 = graphics1;
            string text = this.Text;
            Font font = this.Font;
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(140, 142, 143));
            rectangle1 = new Rectangle(20, 2, this.W, this.H);
            RectangleF layoutRectangle = (RectangleF) rectangle1;
            StringFormat format = Helpers.NearSF;
            graphics2.DrawString(text, font, (Brush) solidBrush, layoutRectangle, format);
          }
          Graphics graphics3 = graphics1;
          string text1 = this.Text;
          Font font1 = this.Font;
          SolidBrush solidBrush1 = new SolidBrush(this._TextColor);
          rectangle1 = new Rectangle(20, 2, this.W, this.H);
          RectangleF layoutRectangle1 = (RectangleF) rectangle1;
          StringFormat format1 = Helpers.NearSF;
          graphics3.DrawString(text1, font1, (Brush) solidBrush1, layoutRectangle1, format1);
          break;
        case FlatCheckBox._Options.Style2:
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
          switch (this.State)
          {
            case MouseState.Over:
              graphics1.DrawRectangle(new Pen(this._BorderColor), rect);
              graphics1.FillRectangle((Brush) new SolidBrush(Color.FromArgb(118, 213, 170)), rect);
              break;
            case MouseState.Down:
              graphics1.DrawRectangle(new Pen(this._BorderColor), rect);
              graphics1.FillRectangle((Brush) new SolidBrush(Color.FromArgb(118, 213, 170)), rect);
              break;
          }
          Rectangle rectangle2;
          if (this.Checked)
          {
            Graphics graphics2 = graphics1;
            string s = "ü";
            Font font2 = new Font("Wingdings", 18f);
            SolidBrush solidBrush2 = new SolidBrush(this._BorderColor);
            rectangle2 = new Rectangle(5, 7, checked (this.H - 9), checked (this.H - 9));
            RectangleF layoutRectangle2 = (RectangleF) rectangle2;
            StringFormat format2 = Helpers.CenterSF;
            graphics2.DrawString(s, font2, (Brush) solidBrush2, layoutRectangle2, format2);
          }
          if (!this.Enabled)
          {
            graphics1.FillRectangle((Brush) new SolidBrush(Color.FromArgb(54, 58, 61)), rect);
            Graphics graphics2 = graphics1;
            string text2 = this.Text;
            Font font2 = this.Font;
            SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(48, 119, 91));
            rectangle2 = new Rectangle(20, 2, this.W, this.H);
            RectangleF layoutRectangle2 = (RectangleF) rectangle2;
            StringFormat format2 = Helpers.NearSF;
            graphics2.DrawString(text2, font2, (Brush) solidBrush2, layoutRectangle2, format2);
          }
          Graphics graphics4 = graphics1;
          string text3 = this.Text;
          Font font3 = this.Font;
          SolidBrush solidBrush3 = new SolidBrush(this._TextColor);
          rectangle2 = new Rectangle(20, 2, this.W, this.H);
          RectangleF layoutRectangle3 = (RectangleF) rectangle2;
          StringFormat format3 = Helpers.NearSF;
          graphics4.DrawString(text3, font3, (Brush) solidBrush3, layoutRectangle3, format3);
          break;
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }

    public delegate void CheckedChangedEventHandler(object sender);

    [Flags]
    public enum _Options
    {
      Style1 = 0,
      Style2 = 1,
    }
  }
}
