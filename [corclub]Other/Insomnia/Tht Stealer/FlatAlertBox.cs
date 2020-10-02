// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatAlertBox
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
  internal class FlatAlertBox : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private FlatAlertBox._Kind K;
    private string _Text;
    private MouseState State;
    private int X;
    [AccessedThroughProperty("T")]
    private System.Windows.Forms.Timer _T;
    private Color SuccessColor;
    private Color SuccessText;
    private Color ErrorColor;
    private Color ErrorText;
    private Color InfoColor;
    private Color InfoText;

    private virtual System.Windows.Forms.Timer T
    {
      [DebuggerNonUserCode] get
      {
        return this._T;
      }
      [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.T_Tick);
        if (this._T != null)
          this._T.Tick -= eventHandler;
        this._T = value;
        if (this._T == null)
          return;
        this._T.Tick += eventHandler;
      }
    }

    [Category("Options")]
    public FlatAlertBox._Kind kind
    {
      get
      {
        return this.K;
      }
      set
      {
        this.K = value;
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
        if (this._Text == null)
          return;
        this._Text = value;
      }
    }

    [Category("Options")]
    public new bool Visible
    {
      get
      {
        return !base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatAlertBox()
    {
    }

    public FlatAlertBox()
    {
      FlatAlertBox.__ENCAddToList((object) this);
      this.State = MouseState.None;
      this.SuccessColor = Color.FromArgb(60, 85, 79);
      this.SuccessText = Color.FromArgb(35, 169, 110);
      this.ErrorColor = Color.FromArgb(87, 71, 71);
      this.ErrorText = Color.FromArgb(254, 142, 122);
      this.InfoColor = Color.FromArgb(70, 91, 94);
      this.InfoText = Color.FromArgb(97, 185, 186);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Size = new Size(576, 42);
      this.Location = new Point(10, 61);
      this.Font = new Font("Segoe UI", 10f);
      this.Cursor = Cursors.Hand;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatAlertBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatAlertBox.__ENCList.Count == FlatAlertBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatAlertBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatAlertBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatAlertBox.__ENCList[index1] = FlatAlertBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatAlertBox.__ENCList.RemoveRange(index1, checked (FlatAlertBox.__ENCList.Count - index1));
          FlatAlertBox.__ENCList.Capacity = FlatAlertBox.__ENCList.Count;
        }
        FlatAlertBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
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
      this.Height = 42;
    }

    public void ShowControl(FlatAlertBox._Kind Kind, string Str, int Interval)
    {
      this.K = Kind;
      this.Text = Str;
      this.Visible = true;
      this.T = new System.Windows.Forms.Timer();
      this.T.Interval = Interval;
      this.T.Enabled = true;
    }

    private void T_Tick(object sender, EventArgs e)
    {
      this.Visible = false;
      this.T.Enabled = false;
      this.T.Dispose();
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
      this.X = e.X;
      this.Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.Visible = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      Rectangle rect1 = new Rectangle(0, 0, this.W, this.H);
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      switch (this.K)
      {
        case FlatAlertBox._Kind.Success:
          graphics1.FillRectangle((Brush) new SolidBrush(this.SuccessColor), rect1);
          Graphics graphics2 = graphics1;
          SolidBrush solidBrush1 = new SolidBrush(this.SuccessText);
          Rectangle rectangle1 = new Rectangle(8, 9, 24, 24);
          Rectangle rect2 = rectangle1;
          graphics2.FillEllipse((Brush) solidBrush1, rect2);
          Graphics graphics3 = graphics1;
          SolidBrush solidBrush2 = new SolidBrush(this.SuccessColor);
          rectangle1 = new Rectangle(10, 11, 20, 20);
          Rectangle rect3 = rectangle1;
          graphics3.FillEllipse((Brush) solidBrush2, rect3);
          Graphics graphics4 = graphics1;
          string s1 = "ü";
          Font font1 = new Font("Wingdings", 22f);
          SolidBrush solidBrush3 = new SolidBrush(this.SuccessText);
          rectangle1 = new Rectangle(7, 7, this.W, this.H);
          RectangleF layoutRectangle1 = (RectangleF) rectangle1;
          StringFormat format1 = Helpers.NearSF;
          graphics4.DrawString(s1, font1, (Brush) solidBrush3, layoutRectangle1, format1);
          Graphics graphics5 = graphics1;
          string text1 = this.Text;
          Font font2 = this.Font;
          SolidBrush solidBrush4 = new SolidBrush(this.SuccessText);
          rectangle1 = new Rectangle(48, 12, this.W, this.H);
          RectangleF layoutRectangle2 = (RectangleF) rectangle1;
          StringFormat format2 = Helpers.NearSF;
          graphics5.DrawString(text1, font2, (Brush) solidBrush4, layoutRectangle2, format2);
          Graphics graphics6 = graphics1;
          SolidBrush solidBrush5 = new SolidBrush(Color.FromArgb(35, Color.Black));
          rectangle1 = new Rectangle(checked (this.W - 30), checked (this.H - 29), 17, 17);
          Rectangle rect4 = rectangle1;
          graphics6.FillEllipse((Brush) solidBrush5, rect4);
          Graphics graphics7 = graphics1;
          string s2 = "r";
          Font font3 = new Font("Marlett", 8f);
          SolidBrush solidBrush6 = new SolidBrush(this.SuccessColor);
          rectangle1 = new Rectangle(checked (this.W - 28), 16, this.W, this.H);
          RectangleF layoutRectangle3 = (RectangleF) rectangle1;
          StringFormat format3 = Helpers.NearSF;
          graphics7.DrawString(s2, font3, (Brush) solidBrush6, layoutRectangle3, format3);
          if (this.State == MouseState.Over)
          {
            Graphics graphics8 = graphics1;
            string s3 = "r";
            Font font4 = new Font("Marlett", 8f);
            SolidBrush solidBrush7 = new SolidBrush(Color.FromArgb(25, Color.White));
            rectangle1 = new Rectangle(checked (this.W - 28), 16, this.W, this.H);
            RectangleF layoutRectangle4 = (RectangleF) rectangle1;
            StringFormat format4 = Helpers.NearSF;
            graphics8.DrawString(s3, font4, (Brush) solidBrush7, layoutRectangle4, format4);
            break;
          }
          else
            break;
        case FlatAlertBox._Kind.Error:
          graphics1.FillRectangle((Brush) new SolidBrush(this.ErrorColor), rect1);
          Graphics graphics9 = graphics1;
          SolidBrush solidBrush8 = new SolidBrush(this.ErrorText);
          Rectangle rectangle2 = new Rectangle(8, 9, 24, 24);
          Rectangle rect5 = rectangle2;
          graphics9.FillEllipse((Brush) solidBrush8, rect5);
          Graphics graphics10 = graphics1;
          SolidBrush solidBrush9 = new SolidBrush(this.ErrorColor);
          rectangle2 = new Rectangle(10, 11, 20, 20);
          Rectangle rect6 = rectangle2;
          graphics10.FillEllipse((Brush) solidBrush9, rect6);
          Graphics graphics11 = graphics1;
          string s4 = "r";
          Font font5 = new Font("Marlett", 16f);
          SolidBrush solidBrush10 = new SolidBrush(this.ErrorText);
          rectangle2 = new Rectangle(6, 11, this.W, this.H);
          RectangleF layoutRectangle5 = (RectangleF) rectangle2;
          StringFormat format5 = Helpers.NearSF;
          graphics11.DrawString(s4, font5, (Brush) solidBrush10, layoutRectangle5, format5);
          Graphics graphics12 = graphics1;
          string text2 = this.Text;
          Font font6 = this.Font;
          SolidBrush solidBrush11 = new SolidBrush(this.ErrorText);
          rectangle2 = new Rectangle(48, 12, this.W, this.H);
          RectangleF layoutRectangle6 = (RectangleF) rectangle2;
          StringFormat format6 = Helpers.NearSF;
          graphics12.DrawString(text2, font6, (Brush) solidBrush11, layoutRectangle6, format6);
          Graphics graphics13 = graphics1;
          SolidBrush solidBrush12 = new SolidBrush(Color.FromArgb(35, Color.Black));
          rectangle2 = new Rectangle(checked (this.W - 32), checked (this.H - 29), 17, 17);
          Rectangle rect7 = rectangle2;
          graphics13.FillEllipse((Brush) solidBrush12, rect7);
          Graphics graphics14 = graphics1;
          string s5 = "r";
          Font font7 = new Font("Marlett", 8f);
          SolidBrush solidBrush13 = new SolidBrush(this.ErrorColor);
          rectangle2 = new Rectangle(checked (this.W - 30), 17, this.W, this.H);
          RectangleF layoutRectangle7 = (RectangleF) rectangle2;
          StringFormat format7 = Helpers.NearSF;
          graphics14.DrawString(s5, font7, (Brush) solidBrush13, layoutRectangle7, format7);
          if (this.State == MouseState.Over)
          {
            Graphics graphics8 = graphics1;
            string s3 = "r";
            Font font4 = new Font("Marlett", 8f);
            SolidBrush solidBrush7 = new SolidBrush(Color.FromArgb(25, Color.White));
            rectangle2 = new Rectangle(checked (this.W - 30), 15, this.W, this.H);
            RectangleF layoutRectangle4 = (RectangleF) rectangle2;
            StringFormat format4 = Helpers.NearSF;
            graphics8.DrawString(s3, font4, (Brush) solidBrush7, layoutRectangle4, format4);
            break;
          }
          else
            break;
        case FlatAlertBox._Kind.Info:
          graphics1.FillRectangle((Brush) new SolidBrush(this.InfoColor), rect1);
          Graphics graphics15 = graphics1;
          SolidBrush solidBrush14 = new SolidBrush(this.InfoText);
          Rectangle rectangle3 = new Rectangle(8, 9, 24, 24);
          Rectangle rect8 = rectangle3;
          graphics15.FillEllipse((Brush) solidBrush14, rect8);
          Graphics graphics16 = graphics1;
          SolidBrush solidBrush15 = new SolidBrush(this.InfoColor);
          rectangle3 = new Rectangle(10, 11, 20, 20);
          Rectangle rect9 = rectangle3;
          graphics16.FillEllipse((Brush) solidBrush15, rect9);
          Graphics graphics17 = graphics1;
          string s6 = "¡";
          Font font8 = new Font("Segoe UI", 20f, FontStyle.Bold);
          SolidBrush solidBrush16 = new SolidBrush(this.InfoText);
          rectangle3 = new Rectangle(12, -4, this.W, this.H);
          RectangleF layoutRectangle8 = (RectangleF) rectangle3;
          StringFormat format8 = Helpers.NearSF;
          graphics17.DrawString(s6, font8, (Brush) solidBrush16, layoutRectangle8, format8);
          Graphics graphics18 = graphics1;
          string text3 = this.Text;
          Font font9 = this.Font;
          SolidBrush solidBrush17 = new SolidBrush(this.InfoText);
          rectangle3 = new Rectangle(48, 12, this.W, this.H);
          RectangleF layoutRectangle9 = (RectangleF) rectangle3;
          StringFormat format9 = Helpers.NearSF;
          graphics18.DrawString(text3, font9, (Brush) solidBrush17, layoutRectangle9, format9);
          Graphics graphics19 = graphics1;
          SolidBrush solidBrush18 = new SolidBrush(Color.FromArgb(35, Color.Black));
          rectangle3 = new Rectangle(checked (this.W - 32), checked (this.H - 29), 17, 17);
          Rectangle rect10 = rectangle3;
          graphics19.FillEllipse((Brush) solidBrush18, rect10);
          Graphics graphics20 = graphics1;
          string s7 = "r";
          Font font10 = new Font("Marlett", 8f);
          SolidBrush solidBrush19 = new SolidBrush(this.InfoColor);
          rectangle3 = new Rectangle(checked (this.W - 30), 17, this.W, this.H);
          RectangleF layoutRectangle10 = (RectangleF) rectangle3;
          StringFormat format10 = Helpers.NearSF;
          graphics20.DrawString(s7, font10, (Brush) solidBrush19, layoutRectangle10, format10);
          if (this.State == MouseState.Over)
          {
            Graphics graphics8 = graphics1;
            string s3 = "r";
            Font font4 = new Font("Marlett", 8f);
            SolidBrush solidBrush7 = new SolidBrush(Color.FromArgb(25, Color.White));
            rectangle3 = new Rectangle(checked (this.W - 30), 17, this.W, this.H);
            RectangleF layoutRectangle4 = (RectangleF) rectangle3;
            StringFormat format4 = Helpers.NearSF;
            graphics8.DrawString(s3, font4, (Brush) solidBrush7, layoutRectangle4, format4);
          }
          break;
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }

    [Flags]
    public enum _Kind
    {
      Success = 0,
      Error = 1,
      Info = 2,
    }
  }
}
