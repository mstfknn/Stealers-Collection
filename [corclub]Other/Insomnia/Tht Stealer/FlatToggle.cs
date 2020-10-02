// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatToggle
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
  internal class FlatToggle : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private FlatToggle._Options O;
    private bool _Checked;
    private MouseState State;
    private Color BaseColor;
    private Color BaseColorRed;
    private Color BGColor;
    private Color ToggleColor;
    private Color TextColor;

    [Category("Options")]
    public FlatToggle._Options Options
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

    [Category("Options")]
    public bool Checked
    {
      get
      {
        return this._Checked;
      }
      set
      {
        this._Checked = value;
      }
    }

    public event FlatToggle.CheckedChangedEventHandler CheckedChanged;

    [DebuggerNonUserCode]
    static FlatToggle()
    {
    }

    public FlatToggle()
    {
      FlatToggle.__ENCAddToList((object) this);
      this._Checked = false;
      this.State = MouseState.None;
      this.BaseColor = Helpers._FlatColor;
      this.BaseColorRed = Color.FromArgb(220, 85, 96);
      this.BGColor = Color.FromArgb(84, 85, 86);
      this.ToggleColor = Color.FromArgb(45, 47, 49);
      this.TextColor = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.Transparent;
      Size size = new Size(44, checked (this.Height + 1));
      this.Size = size;
      this.Cursor = Cursors.Hand;
      this.Font = new Font("Segoe UI", 10f);
      size = new Size(76, 33);
      this.Size = size;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatToggle.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatToggle.__ENCList.Count == FlatToggle.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatToggle.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatToggle.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatToggle.__ENCList[index1] = FlatToggle.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatToggle.__ENCList.RemoveRange(index1, checked (FlatToggle.__ENCList.Count - index1));
          FlatToggle.__ENCList.Capacity = FlatToggle.__ENCList.Count;
        }
        FlatToggle.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Width = 76;
      this.Height = 33;
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

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this._Checked = !this._Checked;
      FlatToggle.CheckedChangedEventHandler changedEventHandler = this.CheckedChangedEvent;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      GraphicsPath graphicsPath = new GraphicsPath();
      GraphicsPath path1 = new GraphicsPath();
      Rectangle Rectangle1 = new Rectangle(0, 0, this.W, this.H);
      Rectangle rectangle1 = new Rectangle(this.W / 2, 0, 38, this.H);
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      switch (this.O)
      {
        case FlatToggle._Options.Style1:
          GraphicsPath path2 = Helpers.RoundRec(Rectangle1, 6);
          GraphicsPath path3 = Helpers.RoundRec(rectangle1, 6);
          graphics1.FillPath((Brush) new SolidBrush(this.BGColor), path2);
          graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path3);
          Graphics graphics2 = graphics1;
          string s1 = "OFF";
          Font font1 = this.Font;
          SolidBrush solidBrush1 = new SolidBrush(this.BGColor);
          Rectangle Rectangle2 = new Rectangle(19, 1, this.W, this.H);
          RectangleF layoutRectangle1 = (RectangleF) Rectangle2;
          StringFormat format1 = Helpers.CenterSF;
          graphics2.DrawString(s1, font1, (Brush) solidBrush1, layoutRectangle1, format1);
          if (this.Checked)
          {
            GraphicsPath path4 = Helpers.RoundRec(Rectangle1, 6);
            Rectangle2 = new Rectangle(this.W / 2, 0, 38, this.H);
            GraphicsPath path5 = Helpers.RoundRec(Rectangle2, 6);
            graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path4);
            graphics1.FillPath((Brush) new SolidBrush(this.BaseColor), path5);
            Graphics graphics3 = graphics1;
            string s2 = "ON";
            Font font2 = this.Font;
            SolidBrush solidBrush2 = new SolidBrush(this.BaseColor);
            Rectangle2 = new Rectangle(8, 7, this.W, this.H);
            RectangleF layoutRectangle2 = (RectangleF) Rectangle2;
            StringFormat format2 = Helpers.NearSF;
            graphics3.DrawString(s2, font2, (Brush) solidBrush2, layoutRectangle2, format2);
            break;
          }
          else
            break;
        case FlatToggle._Options.Style2:
          GraphicsPath path6 = Helpers.RoundRec(Rectangle1, 6);
          rectangle1 = new Rectangle(4, 4, 36, checked (this.H - 8));
          GraphicsPath path7 = Helpers.RoundRec(rectangle1, 4);
          graphics1.FillPath((Brush) new SolidBrush(this.BaseColorRed), path6);
          graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path7);
          graphics1.DrawLine(new Pen(this.BGColor), 18, 20, 18, 12);
          graphics1.DrawLine(new Pen(this.BGColor), 22, 20, 22, 12);
          graphics1.DrawLine(new Pen(this.BGColor), 26, 20, 26, 12);
          Graphics graphics4 = graphics1;
          string s3 = "r";
          Font font3 = new Font("Marlett", 8f);
          SolidBrush solidBrush3 = new SolidBrush(this.TextColor);
          Rectangle rectangle2 = new Rectangle(19, 2, this.Width, this.Height);
          RectangleF layoutRectangle3 = (RectangleF) rectangle2;
          StringFormat format3 = Helpers.CenterSF;
          graphics4.DrawString(s3, font3, (Brush) solidBrush3, layoutRectangle3, format3);
          if (this.Checked)
          {
            GraphicsPath path4 = Helpers.RoundRec(Rectangle1, 6);
            rectangle1 = new Rectangle(checked (unchecked (this.W / 2) - 2), 4, 36, checked (this.H - 8));
            GraphicsPath path5 = Helpers.RoundRec(rectangle1, 4);
            graphics1.FillPath((Brush) new SolidBrush(this.BaseColor), path4);
            graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path5);
            graphics1.DrawLine(new Pen(this.BGColor), checked (unchecked (this.W / 2) + 12), 20, checked (unchecked (this.W / 2) + 12), 12);
            graphics1.DrawLine(new Pen(this.BGColor), checked (unchecked (this.W / 2) + 16), 20, checked (unchecked (this.W / 2) + 16), 12);
            graphics1.DrawLine(new Pen(this.BGColor), checked (unchecked (this.W / 2) + 20), 20, checked (unchecked (this.W / 2) + 20), 12);
            Graphics graphics3 = graphics1;
            string s2 = "ü";
            Font font2 = new Font("Wingdings", 14f);
            SolidBrush solidBrush2 = new SolidBrush(this.TextColor);
            rectangle2 = new Rectangle(8, 7, this.Width, this.Height);
            RectangleF layoutRectangle2 = (RectangleF) rectangle2;
            StringFormat format2 = Helpers.NearSF;
            graphics3.DrawString(s2, font2, (Brush) solidBrush2, layoutRectangle2, format2);
            break;
          }
          else
            break;
        case FlatToggle._Options.Style3:
          GraphicsPath path8 = Helpers.RoundRec(Rectangle1, 16);
          rectangle1 = new Rectangle(checked (this.W - 28), 4, 22, checked (this.H - 8));
          path1.AddEllipse(rectangle1);
          graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path8);
          graphics1.FillPath((Brush) new SolidBrush(this.BaseColorRed), path1);
          Graphics graphics5 = graphics1;
          string s4 = "OFF";
          Font font4 = this.Font;
          SolidBrush solidBrush4 = new SolidBrush(this.BaseColorRed);
          Rectangle rectangle3 = new Rectangle(-12, 2, this.W, this.H);
          RectangleF layoutRectangle4 = (RectangleF) rectangle3;
          StringFormat format4 = Helpers.CenterSF;
          graphics5.DrawString(s4, font4, (Brush) solidBrush4, layoutRectangle4, format4);
          if (this.Checked)
          {
            GraphicsPath path4 = Helpers.RoundRec(Rectangle1, 16);
            rectangle1 = new Rectangle(6, 4, 22, checked (this.H - 8));
            path1.Reset();
            path1.AddEllipse(rectangle1);
            graphics1.FillPath((Brush) new SolidBrush(this.ToggleColor), path4);
            graphics1.FillPath((Brush) new SolidBrush(this.BaseColor), path1);
            Graphics graphics3 = graphics1;
            string s2 = "ON";
            Font font2 = this.Font;
            SolidBrush solidBrush2 = new SolidBrush(this.BaseColor);
            rectangle3 = new Rectangle(12, 2, this.W, this.H);
            RectangleF layoutRectangle2 = (RectangleF) rectangle3;
            StringFormat format2 = Helpers.CenterSF;
            graphics3.DrawString(s2, font2, (Brush) solidBrush2, layoutRectangle2, format2);
            break;
          }
          else
            break;
        case FlatToggle._Options.Style4:
          if (!this.Checked)
            break;
          else
            break;
        case FlatToggle._Options.Style5:
          if (!this.Checked)
            ;
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
      Style3 = 2,
      Style4 = Style3 | Style2,
      Style5 = 4,
    }
  }
}
