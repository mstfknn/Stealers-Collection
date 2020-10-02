// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.FlatTreeView
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Tht_Stealer
{
  internal class FlatTreeView : TreeView
  {
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    private TreeNodeStates State;
    private Color _BaseColor;
    private Color _LineColor;

    [DebuggerNonUserCode]
    static FlatTreeView()
    {
    }

    public FlatTreeView()
    {
      FlatTreeView.__ENCAddToList((object) this);
      this._BaseColor = Color.FromArgb(45, 47, 49);
      this._LineColor = Color.FromArgb(25, 27, 29);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.DoubleBuffered = true;
      this.BackColor = this._BaseColor;
      this.ForeColor = Color.White;
      this.LineColor = this._LineColor;
      this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
      List<WeakReference> list = FlatTreeView.__ENCList;
      Monitor.Enter((object) list);
      try
      {
        if (FlatTreeView.__ENCList.Count == FlatTreeView.__ENCList.Capacity)
        {
          int index1 = 0;
          int num1 = 0;
          int num2 = checked (FlatTreeView.__ENCList.Count - 1);
          int index2 = num1;
          while (index2 <= num2)
          {
            if (FlatTreeView.__ENCList[index2].IsAlive)
            {
              if (index2 != index1)
                FlatTreeView.__ENCList[index1] = FlatTreeView.__ENCList[index2];
              checked { ++index1; }
            }
            checked { ++index2; }
          }
          FlatTreeView.__ENCList.RemoveRange(index1, checked (FlatTreeView.__ENCList.Count - index1));
          FlatTreeView.__ENCList.Capacity = FlatTreeView.__ENCList.Count;
        }
        FlatTreeView.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
      }
      finally
      {
        Monitor.Exit((object) list);
      }
    }

    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
      try
      {
        Rectangle rect;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rectangle& local = @rect;
        int x = e.Bounds.Location.X;
        int y = e.Bounds.Location.Y;
        int width = e.Bounds.Width;
        Rectangle rectangle = e.Bounds;
        int height = rectangle.Height;
        // ISSUE: explicit reference operation
        ^local = new Rectangle(x, y, width, height);
        switch (this.State)
        {
          case TreeNodeStates.Default:
            e.Graphics.FillRectangle(Brushes.Red, rect);
            Graphics graphics1 = e.Graphics;
            string text1 = e.Node.Text;
            Font font1 = new Font("Segoe UI", 8f);
            Brush limeGreen = Brushes.LimeGreen;
            rectangle = new Rectangle(checked (rect.X + 2), checked (rect.Y + 2), rect.Width, rect.Height);
            RectangleF layoutRectangle1 = (RectangleF) rectangle;
            StringFormat format1 = Helpers.NearSF;
            graphics1.DrawString(text1, font1, limeGreen, layoutRectangle1, format1);
            this.Invalidate();
            break;
          case TreeNodeStates.Checked:
            e.Graphics.FillRectangle(Brushes.Green, rect);
            Graphics graphics2 = e.Graphics;
            string text2 = e.Node.Text;
            Font font2 = new Font("Segoe UI", 8f);
            Brush black1 = Brushes.Black;
            rectangle = new Rectangle(checked (rect.X + 2), checked (rect.Y + 2), rect.Width, rect.Height);
            RectangleF layoutRectangle2 = (RectangleF) rectangle;
            StringFormat format2 = Helpers.NearSF;
            graphics2.DrawString(text2, font2, black1, layoutRectangle2, format2);
            this.Invalidate();
            break;
          case TreeNodeStates.Selected:
            e.Graphics.FillRectangle(Brushes.Green, rect);
            Graphics graphics3 = e.Graphics;
            string text3 = e.Node.Text;
            Font font3 = new Font("Segoe UI", 8f);
            Brush black2 = Brushes.Black;
            rectangle = new Rectangle(checked (rect.X + 2), checked (rect.Y + 2), rect.Width, rect.Height);
            RectangleF layoutRectangle3 = (RectangleF) rectangle;
            StringFormat format3 = Helpers.NearSF;
            graphics3.DrawString(text3, font3, black2, layoutRectangle3, format3);
            this.Invalidate();
            break;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) Interaction.MsgBox((object) ex.Message, MsgBoxStyle.OkOnly, (object) null);
        ProjectData.ClearProjectError();
      }
      base.OnDrawNode(e);
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
      graphics.FillRectangle((Brush) new SolidBrush(this._BaseColor), rect);
      graphics.DrawString(this.Text, new Font("Segoe UI", 8f), Brushes.Black, (RectangleF) new Rectangle(checked (this.Bounds.X + 2), checked (this.Bounds.Y + 2), this.Bounds.Width, this.Bounds.Height), Helpers.NearSF);
      base.OnPaint(e);
      Helpers.G.Dispose();
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.DrawImageUnscaled((Image) Helpers.B, 0, 0);
      Helpers.B.Dispose();
    }
  }
}
