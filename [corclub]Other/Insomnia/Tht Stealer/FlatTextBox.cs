// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatTextBox
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
  [DefaultEvent("TextChanged")]
  internal class FlatTextBox : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private MouseState State;
    [AccessedThroughProperty("TB")]
    private TextBox _TB;
    private HorizontalAlignment _TextAlign;
    private int _MaxLength;
    private bool _ReadOnly;
    private bool _UseSystemPasswordChar;
    private bool _Multiline;
    private Color _BaseColor;
    private Color _TextColor;
    private Color _BorderColor;

    private virtual TextBox TB
    {
      [DebuggerNonUserCode] get
      {
        return this._TB;
      }
      [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
      {
        this._TB = value;
      }
    }

    [Category("Options")]
    public HorizontalAlignment TextAlign
    {
      get
      {
        return this._TextAlign;
      }
      set
      {
        this._TextAlign = value;
        if (this.TB == null)
          return;
        this.TB.TextAlign = value;
      }
    }

    [Category("Options")]
    public int MaxLength
    {
      get
      {
        return this._MaxLength;
      }
      set
      {
        this._MaxLength = value;
        if (this.TB == null)
          return;
        this.TB.MaxLength = value;
      }
    }

    [Category("Options")]
    public bool ReadOnly
    {
      get
      {
        return this._ReadOnly;
      }
      set
      {
        this._ReadOnly = value;
        if (this.TB == null)
          return;
        this.TB.ReadOnly = value;
      }
    }

    [Category("Options")]
    public bool UseSystemPasswordChar
    {
      get
      {
        return this._UseSystemPasswordChar;
      }
      set
      {
        this._UseSystemPasswordChar = value;
        if (this.TB == null)
          return;
        this.TB.UseSystemPasswordChar = value;
      }
    }

    [Category("Options")]
    public bool Multiline
    {
      get
      {
        return this._Multiline;
      }
      set
      {
        this._Multiline = value;
        if (this.TB == null)
          return;
        this.TB.Multiline = value;
        if (value)
          this.TB.Height = checked (this.Height - 11);
        else
          this.Height = checked (this.TB.Height + 11);
      }
    }

    [Category("Options")]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        if (this.TB == null)
          return;
        this.TB.Text = value;
      }
    }

    [Category("Options")]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        if (this.TB == null)
          return;
        this.TB.Font = value;
        this.TB.Location = new Point(3, 5);
        this.TB.Width = checked (this.Width - 6);
        if (!this._Multiline)
          this.Height = checked (this.TB.Height + 11);
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

    public override Color ForeColor
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
    static FlatTextBox()
    {
    }

    public FlatTextBox()
    {
      FlatTextBox.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._TextAlign = HorizontalAlignment.Left;
      this._MaxLength = (int) short.MaxValue;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._TextColor = Color.FromArgb(192, 192, 192);
      this._BorderColor = Helpers._FlatColor;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.Transparent;
      this.TB = new TextBox();
      this.TB.Font = new Font("Segoe UI", 10f);
      this.TB.Text = this.Text;
      this.TB.BackColor = this._BaseColor;
      this.TB.ForeColor = this._TextColor;
      this.TB.MaxLength = this._MaxLength;
      this.TB.Multiline = this._Multiline;
      this.TB.ReadOnly = this._ReadOnly;
      this.TB.UseSystemPasswordChar = this._UseSystemPasswordChar;
      this.TB.BorderStyle = BorderStyle.None;
      this.TB.Location = new Point(5, 5);
      this.TB.Width = checked (this.Width - 10);
      this.TB.Cursor = Cursors.IBeam;
      if (this._Multiline)
        this.TB.Height = checked (this.Height - 11);
      else
        this.Height = checked (this.TB.Height + 11);
      this.TB.TextChanged += new EventHandler(this.OnBaseTextChanged);
      this.TB.KeyDown += new KeyEventHandler(this.OnBaseKeyDown);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatTextBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatTextBox.__ENCList.Count == FlatTextBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatTextBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatTextBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatTextBox.__ENCList[index1] = FlatTextBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatTextBox.__ENCList.RemoveRange(index1, checked (FlatTextBox.__ENCList.Count - index1));
          FlatTextBox.__ENCList.Capacity = FlatTextBox.__ENCList.Count;
        }
        FlatTextBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      if (this.Controls.Contains((Control) this.TB))
        return;
      this.Controls.Add((Control) this.TB);
    }

    private void OnBaseTextChanged(object s, EventArgs e)
    {
      this.Text = this.TB.Text;
    }

    private void OnBaseKeyDown(object s, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.A)
      {
        this.TB.SelectAll();
        e.SuppressKeyPress = true;
      }
      if ((!e.Control || e.KeyCode != Keys.C) && !false)
        return;
      this.TB.Copy();
      e.SuppressKeyPress = true;
    }

    protected override void OnResize(EventArgs e)
    {
      this.TB.Location = new Point(5, 5);
      this.TB.Width = checked (this.Width - 10);
      if (this._Multiline)
        this.TB.Height = checked (this.Height - 11);
      else
        this.Height = checked (this.TB.Height + 11);
      base.OnResize(e);
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
      this.TB.Focus();
      this.Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.State = MouseState.Over;
      this.TB.Focus();
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
      Rectangle rect = new Rectangle(0, 0, this.W, this.H);
      Graphics graphics = Helpers.G;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.Clear(this.BackColor);
      this.TB.BackColor = this._BaseColor;
      this.TB.ForeColor = this._TextColor;
      graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
