// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.Helpers
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tht_Stealer
{
  [StandardModule]
  internal sealed class Helpers
  {
    internal static Color _FlatColor = Color.FromArgb(35, 168, 109);
    internal static StringFormat NearSF = new StringFormat()
    {
      Alignment = StringAlignment.Near,
      LineAlignment = StringAlignment.Near
    };
    internal static StringFormat CenterSF = new StringFormat()
    {
      Alignment = StringAlignment.Center,
      LineAlignment = StringAlignment.Center
    };
    internal static Graphics G;
    internal static Bitmap B;

    public static GraphicsPath RoundRec(Rectangle Rectangle, int Curve)
    {
      GraphicsPath graphicsPath1 = new GraphicsPath();
      int num1 = checked (Curve * 2);
      GraphicsPath graphicsPath2 = graphicsPath1;
      Rectangle rectangle = new Rectangle(Rectangle.X, Rectangle.Y, num1, num1);
      Rectangle rect1 = rectangle;
      double num2 = -180.0;
      double num3 = 90.0;
      graphicsPath2.AddArc(rect1, (float) num2, (float) num3);
      GraphicsPath graphicsPath3 = graphicsPath1;
      rectangle = new Rectangle(checked (Rectangle.Width - num1 + Rectangle.X), Rectangle.Y, num1, num1);
      Rectangle rect2 = rectangle;
      double num4 = -90.0;
      double num5 = 90.0;
      graphicsPath3.AddArc(rect2, (float) num4, (float) num5);
      GraphicsPath graphicsPath4 = graphicsPath1;
      rectangle = new Rectangle(checked (Rectangle.Width - num1 + Rectangle.X), checked (Rectangle.Height - num1 + Rectangle.Y), num1, num1);
      Rectangle rect3 = rectangle;
      double num6 = 0.0;
      double num7 = 90.0;
      graphicsPath4.AddArc(rect3, (float) num6, (float) num7);
      GraphicsPath graphicsPath5 = graphicsPath1;
      rectangle = new Rectangle(Rectangle.X, checked (Rectangle.Height - num1 + Rectangle.Y), num1, num1);
      Rectangle rect4 = rectangle;
      double num8 = 90.0;
      double num9 = 90.0;
      graphicsPath5.AddArc(rect4, (float) num8, (float) num9);
      graphicsPath1.AddLine(new Point(Rectangle.X, checked (Rectangle.Height - num1 + Rectangle.Y)), new Point(Rectangle.X, checked (Curve + Rectangle.Y)));
      return graphicsPath1;
    }

    public static GraphicsPath RoundRect(float x, float y, float w, float h, float r = 0.3f, bool TL = true, bool TR = true, bool BR = true, bool BL = true)
    {
      float num1 = Math.Min(w, h) * r;
      float num2 = x + w;
      float num3 = y + h;
      GraphicsPath graphicsPath1 = new GraphicsPath();
      GraphicsPath graphicsPath2 = graphicsPath1;
      if (TL)
        graphicsPath2.AddArc(x, y, num1, num1, 180f, 90f);
      else
        graphicsPath2.AddLine(x, y, x, y);
      if (TR)
        graphicsPath2.AddArc(num2 - num1, y, num1, num1, 270f, 90f);
      else
        graphicsPath2.AddLine(num2, y, num2, y);
      if (BR)
        graphicsPath2.AddArc(num2 - num1, num3 - num1, num1, num1, 0.0f, 90f);
      else
        graphicsPath2.AddLine(num2, num3, num2, num3);
      if (BL)
        graphicsPath2.AddArc(x, num3 - num1, num1, num1, 90f, 90f);
      else
        graphicsPath2.AddLine(x, num3, x, num3);
      graphicsPath2.CloseFigure();
      return graphicsPath1;
    }

    public static GraphicsPath DrawArrow(int x, int y, bool flip)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int num1 = 12;
      int num2 = 6;
      if (flip)
      {
        graphicsPath.AddLine(checked (x + 1), y, checked (x + num1 + 1), y);
        graphicsPath.AddLine(checked (x + num1), y, checked (x + num2), checked (y + num2 - 1));
      }
      else
      {
        graphicsPath.AddLine(x, checked (y + num2), checked (x + num1), checked (y + num2));
        graphicsPath.AddLine(checked (x + num1), checked (y + num2), checked (x + num2), y);
      }
      graphicsPath.CloseFigure();
      return graphicsPath;
    }
  }
}
