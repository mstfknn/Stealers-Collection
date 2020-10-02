// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FormSkin
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
  internal class FormSkin : ContainerControl
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private bool Cap;
    private bool _HeaderMaximize;
    private Point MousePoint;
    private object MoveHeight;
    private Color _HeaderColor;
    private Color _BaseColor;
    private Color _BorderColor;
    private Color TextColor;
    private Color _HeaderLight;
    private Color _BaseLight;
    public Color TextLight;

    [Category("Colors")]
    public Color HeaderColor
    {
      get
      {
        return this._HeaderColor;
      }
      set
      {
        this._HeaderColor = value;
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

    [Category("Colors")]
    public Color FlatColor
    {
      get
      {
        return Helpers._FlatColor;
      }
      set
      {
        Helpers._FlatColor = value;
      }
    }

    [Category("Options")]
    public bool HeaderMaximize
    {
      get
      {
        return this._HeaderMaximize;
      }
      set
      {
        this._HeaderMaximize = value;
      }
    }

    [DebuggerNonUserCode]
    static FormSkin()
    {
    }

    public FormSkin()
    {
      this.MouseDoubleClick += new MouseEventHandler(this.FormSkin_MouseDoubleClick);
      FormSkin.__ENCAddToList((object) this);
      this.Cap = false;
      this._HeaderMaximize = false;
      this.MousePoint = new Point(0, 0);
      this.MoveHeight = (object) 50;
      this._HeaderColor = Color.FromArgb(45, 47, 49);
      this._BaseColor = Color.FromArgb(60, 70, 73);
      this._BorderColor = Color.FromArgb(53, 58, 60);
      this.TextColor = Color.FromArgb(234, 234, 234);
      this._HeaderLight = Color.FromArgb(171, 171, 172);
      this._BaseLight = Color.FromArgb(196, 199, 200);
      this.TextLight = Color.FromArgb(45, 47, 49);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.White;
      this.Font = new Font("Segoe UI", 12f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FormSkin.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FormSkin.__ENCList.Count == FormSkin.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FormSkin.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FormSkin.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FormSkin.__ENCList[index1] = FormSkin.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FormSkin.__ENCList.RemoveRange(index1, checked (FormSkin.__ENCList.Count - index1));
          FormSkin.__ENCList.Capacity = FormSkin.__ENCList.Count;
        }
        FormSkin.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!(e.Button == MouseButtons.Left & new Rectangle(0, 0, this.Width, Conversions.ToInteger(this.MoveHeight)).Contains(e.Location)))
        return;
      this.Cap = true;
      this.MousePoint = e.Location;
    }

    private void FormSkin_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (!this.HeaderMaximize || !(e.Button == MouseButtons.Left & new Rectangle(0, 0, this.Width, Conversions.ToInteger(this.MoveHeight)).Contains(e.Location)))
        return;
      if (this.FindForm().WindowState == FormWindowState.Normal)
      {
        this.FindForm().WindowState = FormWindowState.Maximized;
        this.FindForm().Refresh();
      }
      else if (this.FindForm().WindowState == FormWindowState.Maximized)
      {
        this.FindForm().WindowState = FormWindowState.Normal;
        this.FindForm().Refresh();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Cap = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Cap)
        return;
      this.Parent.Location = Control.MousePosition - (Size) this.MousePoint;
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.ParentForm.FormBorderStyle = FormBorderStyle.None;
      this.ParentForm.AllowTransparency = false;
      this.ParentForm.TransparencyKey = Color.Fuchsia;
      this.ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
      this.Dock = DockStyle.Fill;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = this.Width;
      this.H = this.Height;
      Rectangle rect1 = new Rectangle(0, 0, this.W, this.H);
      Rectangle rect2 = new Rectangle(0, 0, this.W, 50);
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect1);
      graphics1.FillRectangle((Brush) new SolidBrush(this._HeaderColor), rect2);
      Graphics graphics2 = graphics1;
      SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(243, 243, 243));
      Rectangle rectangle = new Rectangle(8, 16, 4, 18);
      Rectangle rect3 = rectangle;
      graphics2.FillRectangle((Brush) solidBrush1, rect3);
      graphics1.FillRectangle((Brush) new SolidBrush(Helpers._FlatColor), 16, 16, 4, 18);
      Graphics graphics3 = graphics1;
      string text = this.Text;
      Font font = this.Font;
      SolidBrush solidBrush2 = new SolidBrush(this.TextColor);
      rectangle = new Rectangle(26, 15, this.W, this.H);
      RectangleF layoutRectangle = (RectangleF) rectangle;
      StringFormat format = Helpers.NearSF;
      graphics3.DrawString(text, font, (Brush) solidBrush2, layoutRectangle, format);
      graphics1.DrawRectangle(new Pen(this._BorderColor), rect1);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
