// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatTabControl
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
  internal class FlatTabControl : TabControl
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private int W;
    private int H;
    private Color BGColor;
    private Color _BaseColor;
    private Color _ActiveColor;

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
    public Color ActiveColor
    {
      get
      {
        return this._ActiveColor;
      }
      set
      {
        this._ActiveColor = value;
      }
    }

    [DebuggerNonUserCode]
    static FlatTabControl()
    {
    }

    public FlatTabControl()
    {
      FlatTabControl.__ENCAddToList((object) this);
      this.BGColor = Color.FromArgb(60, 70, 73);
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._ActiveColor = Helpers._FlatColor;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(60, 70, 73);
      this.Font = new Font("Segoe UI", 10f);
      this.SizeMode = TabSizeMode.Fixed;
      this.ItemSize = new Size(120, 40);
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatTabControl.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatTabControl.__ENCList.Count == FlatTabControl.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatTabControl.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatTabControl.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatTabControl.__ENCList[index1] = FlatTabControl.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatTabControl.__ENCList.RemoveRange(index1, checked (FlatTabControl.__ENCList.Count - index1));
          FlatTabControl.__ENCList.Capacity = FlatTabControl.__ENCList.Count;
        }
        FlatTabControl.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void CreateHandle()
    {
      base.CreateHandle();
      this.Alignment = TabAlignment.Top;
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
      graphics1.Clear(this._BaseColor);
      try
      {
        this.SelectedTab.BackColor = this.BGColor;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
      int num1 = 0;
      int num2 = checked (this.TabCount - 1);
      int index = num1;
      while (index <= num2)
      {
        Rectangle rectangle;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rectangle& local1 = @rectangle;
        Point location1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Point& local2 = @location1;
        Point location2 = this.GetTabRect(index).Location;
        int x1 = checked (location2.X + 2);
        Point location3 = this.GetTabRect(index).Location;
        int y1 = location3.Y;
        // ISSUE: explicit reference operation
        ^local2 = new Point(x1, y1);
        Point location4 = location1;
        Size size1 = new Size(this.GetTabRect(index).Width, this.GetTabRect(index).Height);
        Size size2 = size1;
        // ISSUE: explicit reference operation
        ^local1 = new Rectangle(location4, size2);
        Rectangle rect;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rectangle& local3 = @rect;
        Point location5 = rectangle.Location;
        size1 = new Size(rectangle.Width, rectangle.Height);
        Size size3 = size1;
        // ISSUE: explicit reference operation
        ^local3 = new Rectangle(location5, size3);
        if (index == this.SelectedIndex)
        {
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
          graphics1.FillRectangle((Brush) new SolidBrush(this._ActiveColor), rect);
          if (this.ImageList != null)
          {
            try
            {
              if (this.ImageList.Images[this.TabPages[index].ImageIndex] != null)
              {
                Graphics graphics2 = graphics1;
                Image image = this.ImageList.Images[this.TabPages[index].ImageIndex];
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Point& local4 = @location2;
                location1 = rect.Location;
                int x2 = checked (location1.X + 8);
                location3 = rect.Location;
                int y2 = checked (location3.Y + 6);
                // ISSUE: explicit reference operation
                ^local4 = new Point(x2, y2);
                Point point = location2;
                graphics2.DrawImage(image, point);
                graphics1.DrawString("      " + this.TabPages[index].Text, this.Font, Brushes.White, (RectangleF) rect, Helpers.CenterSF);
              }
              else
                graphics1.DrawString(this.TabPages[index].Text, this.Font, Brushes.White, (RectangleF) rect, Helpers.CenterSF);
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              throw new Exception(ex.Message);
            }
          }
          else
            graphics1.DrawString(this.TabPages[index].Text, this.Font, Brushes.White, (RectangleF) rect, Helpers.CenterSF);
        }
        else
        {
          graphics1.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
          if (this.ImageList != null)
          {
            try
            {
              if (this.ImageList.Images[this.TabPages[index].ImageIndex] != null)
              {
                Graphics graphics2 = graphics1;
                Image image = this.ImageList.Images[this.TabPages[index].ImageIndex];
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Point& local4 = @location2;
                location1 = rect.Location;
                int x2 = checked (location1.X + 8);
                location3 = rect.Location;
                int y2 = checked (location3.Y + 6);
                // ISSUE: explicit reference operation
                ^local4 = new Point(x2, y2);
                Point point = location2;
                graphics2.DrawImage(image, point);
                graphics1.DrawString("      " + this.TabPages[index].Text, this.Font, (Brush) new SolidBrush(Color.White), (RectangleF) rect, new StringFormat()
                {
                  LineAlignment = StringAlignment.Center,
                  Alignment = StringAlignment.Center
                });
              }
              else
                graphics1.DrawString(this.TabPages[index].Text, this.Font, (Brush) new SolidBrush(Color.White), (RectangleF) rect, new StringFormat()
                {
                  LineAlignment = StringAlignment.Center,
                  Alignment = StringAlignment.Center
                });
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              throw new Exception(ex.Message);
            }
          }
          else
            graphics1.DrawString(this.TabPages[index].Text, this.Font, (Brush) new SolidBrush(Color.White), (RectangleF) rect, new StringFormat()
            {
              LineAlignment = StringAlignment.Center,
              Alignment = StringAlignment.Center
            });
        }
        checked { ++index; }
      }
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
