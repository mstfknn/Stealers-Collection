// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatColorPalette
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
  internal class FlatColorPalette : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private Color _Red;
    private Color _Cyan;
    private Color _Blue;
    private Color _LimeGreen;
    private Color _Orange;
    private Color _Purple;
    private Color _Black;
    private Color _Gray;
    private Color _White;

    [Category("Colors")]
    public Color Red
    {
      get
      {
        return this._Red;
      }
      set
      {
        this._Red = value;
      }
    }

    [Category("Colors")]
    public Color Cyan
    {
      get
      {
        return this._Cyan;
      }
      set
      {
        this._Cyan = value;
      }
    }

    [Category("Colors")]
    public Color Blue
    {
      get
      {
        return this._Blue;
      }
      set
      {
        this._Blue = value;
      }
    }

    [Category("Colors")]
    public Color LimeGreen
    {
      get
      {
        return this._LimeGreen;
      }
      set
      {
        this._LimeGreen = value;
      }
    }

    [Category("Colors")]
    public Color Orange
    {
      get
      {
        return this._Orange;
      }
      set
      {
        this._Orange = value;
      }
    }

    [Category("Colors")]
    public Color Purple
    {
      get
      {
        return this._Purple;
      }
      set
      {
        this._Purple = value;
      }
    }

    [Category("Colors")]
    public Color Black
    {
      get
      {
        return this._Black;
      }
      set
      {
        this._Black = value;
      }
    }

    [Category("Colors")]
    public Color Gray
    {
      get
      {
        return this._Gray;
      }
      set
      {
        this._Gray = value;
      }
    }

    [Category("Colors")]
    public Color White
    {
      get
      {
        return this._White;
      }
      set
      {
        this._White = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatColorPalette()
    {
    }

    public FlatColorPalette()
    {
      FlatColorPalette.__ENCAddToList((object) this);
      this._Red = Color.FromArgb(220, 85, 96);
      this._Cyan = Color.FromArgb(10, 154, 157);
      this._Blue = Color.FromArgb(0, 128, (int) byte.MaxValue);
      this._LimeGreen = Color.FromArgb(35, 168, 109);
      this._Orange = Color.FromArgb(253, 181, 63);
      this._Purple = Color.FromArgb(155, 88, 181);
      this._Black = Color.FromArgb(45, 47, 49);
      this._Gray = Color.FromArgb(63, 70, 73);
      this._White = Color.FromArgb(243, 243, 243);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Size = new Size(160, 80);
      this.Font = new Font("Segoe UI", 12f);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatColorPalette.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatColorPalette.__ENCList.Count == FlatColorPalette.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatColorPalette.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatColorPalette.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatColorPalette.__ENCList[index1] = FlatColorPalette.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatColorPalette.__ENCList.RemoveRange(index1, checked (FlatColorPalette.__ENCList.Count - index1));
          FlatColorPalette.__ENCList.Capacity = FlatColorPalette.__ENCList.Count;
        }
        FlatColorPalette.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Width = 180;
      this.Height = 80;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      this.W = checked (this.Width - 1);
      this.H = checked (this.Height - 1);
      Graphics graphics1 = Helpers.G;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      graphics1.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics1.Clear(this.BackColor);
      Graphics graphics2 = graphics1;
      SolidBrush solidBrush1 = new SolidBrush(this._Red);
      Rectangle rectangle = new Rectangle(0, 0, 20, 40);
      Rectangle rect1 = rectangle;
      graphics2.FillRectangle((Brush) solidBrush1, rect1);
      Graphics graphics3 = graphics1;
      SolidBrush solidBrush2 = new SolidBrush(this._Cyan);
      rectangle = new Rectangle(20, 0, 20, 40);
      Rectangle rect2 = rectangle;
      graphics3.FillRectangle((Brush) solidBrush2, rect2);
      Graphics graphics4 = graphics1;
      SolidBrush solidBrush3 = new SolidBrush(this._Blue);
      rectangle = new Rectangle(40, 0, 20, 40);
      Rectangle rect3 = rectangle;
      graphics4.FillRectangle((Brush) solidBrush3, rect3);
      Graphics graphics5 = graphics1;
      SolidBrush solidBrush4 = new SolidBrush(this._LimeGreen);
      rectangle = new Rectangle(60, 0, 20, 40);
      Rectangle rect4 = rectangle;
      graphics5.FillRectangle((Brush) solidBrush4, rect4);
      Graphics graphics6 = graphics1;
      SolidBrush solidBrush5 = new SolidBrush(this._Orange);
      rectangle = new Rectangle(80, 0, 20, 40);
      Rectangle rect5 = rectangle;
      graphics6.FillRectangle((Brush) solidBrush5, rect5);
      Graphics graphics7 = graphics1;
      SolidBrush solidBrush6 = new SolidBrush(this._Purple);
      rectangle = new Rectangle(100, 0, 20, 40);
      Rectangle rect6 = rectangle;
      graphics7.FillRectangle((Brush) solidBrush6, rect6);
      Graphics graphics8 = graphics1;
      SolidBrush solidBrush7 = new SolidBrush(this._Black);
      rectangle = new Rectangle(120, 0, 20, 40);
      Rectangle rect7 = rectangle;
      graphics8.FillRectangle((Brush) solidBrush7, rect7);
      Graphics graphics9 = graphics1;
      SolidBrush solidBrush8 = new SolidBrush(this._Gray);
      rectangle = new Rectangle(140, 0, 20, 40);
      Rectangle rect8 = rectangle;
      graphics9.FillRectangle((Brush) solidBrush8, rect8);
      Graphics graphics10 = graphics1;
      SolidBrush solidBrush9 = new SolidBrush(this._White);
      rectangle = new Rectangle(160, 0, 20, 40);
      Rectangle rect9 = rectangle;
      graphics10.FillRectangle((Brush) solidBrush9, rect9);
      Graphics graphics11 = graphics1;
      string s = "Color Palette";
      Font font = this.Font;
      SolidBrush solidBrush10 = new SolidBrush(this._White);
      rectangle = new Rectangle(0, 22, this.W, this.H);
      RectangleF layoutRectangle = (RectangleF) rectangle;
      StringFormat format = Helpers.CenterSF;
      graphics11.DrawString(s, font, (Brush) solidBrush10, layoutRectangle, format);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
