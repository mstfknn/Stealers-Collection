// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatListBox
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using Microsoft.VisualBasic;
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
  internal class FlatListBox : Control
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    [AccessedThroughProperty("ListBx")]
    private ListBox _ListBx;
    private string[] _items;
    private Color BaseColor;
    private Color _SelectedColor;

    private virtual ListBox ListBx
    {
      [DebuggerNonUserCode] get
      {
        return this._ListBx;
      }
      [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
      {
        DrawItemEventHandler itemEventHandler = new DrawItemEventHandler(this.Drawitem);
        if (this._ListBx != null)
          this._ListBx.DrawItem -= itemEventHandler;
        this._ListBx = value;
        if (this._ListBx == null)
          return;
        this._ListBx.DrawItem += itemEventHandler;
      }
    }

    [Category("Options")]
    public string[] items
    {
      get
      {
        return this._items;
      }
      set
      {
        this._items = value;
        this.ListBx.Items.Clear();
        this.ListBx.Items.AddRange((object[]) value);
        this.Invalidate();
      }
    }

    [Category("Colors")]
    public Color SelectedColor
    {
      get
      {
        return this._SelectedColor;
      }
      set
      {
        this._SelectedColor = value;
      }
    }

    public string SelectedItem
    {
      get
      {
        return Conversions.ToString(this.ListBx.SelectedItem);
      }
    }

    public int SelectedIndex
    {
      get
      {
        return this.ListBx.SelectedIndex;
      }
    }

    [DebuggerNonUserCode]
    static FlatListBox()
    {
    }

    public FlatListBox()
    {
      FlatListBox.__ENCAddToList((object) this);
      this.ListBx = new ListBox();
      this._items = new string[1]
      {
        ""
      };
      this.BaseColor = Color.FromArgb(45, 47, 49);
      this._SelectedColor = Helpers._FlatColor;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.ListBx.DrawMode = DrawMode.OwnerDrawFixed;
      this.ListBx.ScrollAlwaysVisible = false;
      this.ListBx.HorizontalScrollbar = false;
      this.ListBx.BorderStyle = BorderStyle.None;
      this.ListBx.BackColor = this.BaseColor;
      this.ListBx.ForeColor = Color.White;
      this.ListBx.Location = new Point(3, 3);
      this.ListBx.Font = new Font("Segoe UI", 8f);
      this.ListBx.ItemHeight = 20;
      this.ListBx.Items.Clear();
      this.ListBx.IntegralHeight = false;
      this.Size = new Size(131, 101);
      this.BackColor = this.BaseColor;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatListBox.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatListBox.__ENCList.Count == FlatListBox.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatListBox.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatListBox.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatListBox.__ENCList[index1] = FlatListBox.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatListBox.__ENCList.RemoveRange(index1, checked (FlatListBox.__ENCList.Count - index1));
          FlatListBox.__ENCList.Capacity = FlatListBox.__ENCList.Count;
        }
        FlatListBox.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    public void Clear()
    {
      this.ListBx.Items.Clear();
    }

    public void ClearSelected()
    {
      int index = checked (this.ListBx.SelectedItems.Count - 1);
      while (index >= 0)
      {
        this.ListBx.Items.Remove(RuntimeHelpers.GetObjectValue(this.ListBx.SelectedItems[index]));
        checked { index += -1; }
      }
    }

    public void Drawitem(object sender, DrawItemEventArgs e)
    {
      if (e.Index < 0)
        return;
      e.DrawBackground();
      e.DrawFocusRectangle();
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      if (Strings.InStr(e.State.ToString(), "Selected,", CompareMethod.Binary) > 0)
      {
        Graphics graphics1 = e.Graphics;
        SolidBrush solidBrush = new SolidBrush(this._SelectedColor);
        Rectangle bounds1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rectangle& local = @bounds1;
        Rectangle bounds2 = e.Bounds;
        int x = bounds2.X;
        int y = e.Bounds.Y;
        int width = e.Bounds.Width;
        int height = e.Bounds.Height;
        // ISSUE: explicit reference operation
        ^local = new Rectangle(x, y, width, height);
        Rectangle rect = bounds1;
        graphics1.FillRectangle((Brush) solidBrush, rect);
        Graphics graphics2 = e.Graphics;
        string s = " " + this.ListBx.Items[e.Index].ToString();
        Font font = new Font("Segoe UI", 8f);
        Brush white = Brushes.White;
        bounds2 = e.Bounds;
        double num1 = (double) bounds2.X;
        bounds1 = e.Bounds;
        double num2 = (double) checked (bounds1.Y + 2);
        graphics2.DrawString(s, font, white, (float) num1, (float) num2);
      }
      else
      {
        Graphics graphics1 = e.Graphics;
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(51, 53, 55));
        Rectangle bounds1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rectangle& local = @bounds1;
        Rectangle bounds2 = e.Bounds;
        int x = bounds2.X;
        int y = e.Bounds.Y;
        int width = e.Bounds.Width;
        int height = e.Bounds.Height;
        // ISSUE: explicit reference operation
        ^local = new Rectangle(x, y, width, height);
        Rectangle rect = bounds1;
        graphics1.FillRectangle((Brush) solidBrush, rect);
        Graphics graphics2 = e.Graphics;
        string s = " " + this.ListBx.Items[e.Index].ToString();
        Font font = new Font("Segoe UI", 8f);
        Brush white = Brushes.White;
        bounds1 = e.Bounds;
        double num1 = (double) bounds1.X;
        bounds2 = e.Bounds;
        double num2 = (double) checked (bounds2.Y + 2);
        graphics2.DrawString(s, font, white, (float) num1, (float) num2);
      }
      e.Graphics.Dispose();
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      if (this.Controls.Contains((Control) this.ListBx))
        return;
      this.Controls.Add((Control) this.ListBx);
    }

    public void AddRange(object[] items)
    {
      this.ListBx.Items.Remove((object) "");
      this.ListBx.Items.AddRange(items);
    }

    public void AddItem(object item)
    {
      this.ListBx.Items.Remove((object) "");
      this.ListBx.Items.Add(RuntimeHelpers.GetObjectValue(item));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Helpers.B = new Bitmap(this.Width, this.Height);
      Helpers.G = Graphics.FromImage((Image) Helpers.B);
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      Graphics graphics = Helpers.G;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.Clear(this.BackColor);
      this.ListBx.Size = new Size(checked (this.Width - 6), checked (this.Height - 2));
      graphics.FillRectangle((Brush) new SolidBrush(this.BaseColor), rect);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
