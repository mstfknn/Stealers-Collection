// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.RadioButton
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
  [DefaultEvent("CheckedChanged")]
  internal class RadioButton : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private MouseState State;
    private int W;
    private int H;
    private RadioButton._Options O;
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
        this.InvalidateControls();
        RadioButton.CheckedChangedEventHandler changedEventHandler = this.CheckedChangedEvent;
        if (changedEventHandler != null)
          changedEventHandler((object) this);
        this.Invalidate();
      }
    }

    [Category("Options")]
    public RadioButton._Options Options
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

    public event RadioButton.CheckedChangedEventHandler CheckedChanged;

    [DebuggerNonUserCode]
    static RadioButton()
    {
    }

    public RadioButton()
    {
      RadioButton.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._BorderColor = Helpers._FlatColor;
      this._TextColor = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.Cursor = Cursors.Hand;
      this.Size = new Size(100, 22);
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Font = new Font("Segoe UI", 10f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = RadioButton.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (RadioButton.__ENCList.Count == RadioButton.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (RadioButton.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (RadioButton.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                RadioButton.__ENCList[index1] = RadioButton.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          RadioButton.__ENCList.RemoveRange(index1, checked (RadioButton.__ENCList.Count - index1));
          RadioButton.__ENCList.Capacity = RadioButton.__ENCList.Count;
        }
        RadioButton.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnClick(EventArgs e)
    {
      if (!this._Checked)
        this.Checked = true;
      base.OnClick(e);
    }

    private void InvalidateControls()
    {
      if (!this.IsHandleCreated || !this._Checked)
        return;
      try
      {
        foreach (Control control in this.Parent.Controls)
        {
          if (control != this && control is RadioButton)
          {
            ((RadioButton) control).Checked = false;
            this.Invalidate();
          }
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.InvalidateControls();
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
      Rectangle rect1 = new Rectangle(0, 2, checked (this.Height - 5), checked (this.Height - 5));
      Rectangle rect2 = new Rectangle(4, 6, checked (this.H - 12), checked (this.H - 12));
      Graphics graphics = Helpers.G;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.Clear(this.BackColor);
      switch (this.O)
      {
        case RadioButton._Options.Style1:
          graphics.FillEllipse((Brush) new SolidBrush(this._BaseColor), rect1);
          switch (this.State)
          {
            case MouseState.Over:
              graphics.DrawEllipse(new Pen(this._BorderColor), rect1);
              break;
            case MouseState.Down:
              graphics.DrawEllipse(new Pen(this._BorderColor), rect1);
              break;
          }
          if (this.Checked)
          {
            graphics.FillEllipse((Brush) new SolidBrush(this._BorderColor), rect2);
            break;
          }
          else
            break;
        case RadioButton._Options.Style2:
          graphics.FillEllipse((Brush) new SolidBrush(this._BaseColor), rect1);
          switch (this.State)
          {
            case MouseState.Over:
              graphics.DrawEllipse(new Pen(this._BorderColor), rect1);
              graphics.FillEllipse((Brush) new SolidBrush(Color.FromArgb(118, 213, 170)), rect1);
              break;
            case MouseState.Down:
              graphics.DrawEllipse(new Pen(this._BorderColor), rect1);
              graphics.FillEllipse((Brush) new SolidBrush(Color.FromArgb(118, 213, 170)), rect1);
              break;
          }
          if (this.Checked)
            graphics.FillEllipse((Brush) new SolidBrush(this._BorderColor), rect2);
          break;
      }
      graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this._TextColor), (RectangleF) new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
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
