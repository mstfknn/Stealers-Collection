// Decompiled with JetBrains decompiler
// Type: Tht_Stealer.IconInjector
// Assembly: Tht Stealer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4558D086-1B58-4935-8D10-FB5F8F33EB02
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace Tht_Stealer
{
  public class IconInjector
  {
    [DebuggerNonUserCode]
    public IconInjector()
    {
    }

    public static void InjectIcon(string exeFileName, string iconFileName)
    {
      IconInjector.InjectIcon(exeFileName, iconFileName, 1U, 1U);
    }

    public static void InjectIcon(string exeFileName, string iconFileName, uint iconGroupID, uint iconBaseID)
    {
      IconInjector.IconFile iconFile = IconInjector.IconFile.FromFile(iconFileName);
      IntPtr hUpdate1 = IconInjector.NativeMethods.BeginUpdateResource(exeFileName, false);
      byte[] iconGroupData = iconFile.CreateIconGroupData(iconBaseID);
      IntPtr hUpdate2 = hUpdate1;
      IntPtr num1 = new IntPtr(14L);
      IntPtr type1 = num1;
      IntPtr num2 = new IntPtr((long) iconGroupID);
      IntPtr name1 = num2;
      int num3 = 0;
      byte[] data1 = iconGroupData;
      int length1 = iconGroupData.Length;
      IconInjector.NativeMethods.UpdateResource(hUpdate2, type1, name1, (short) num3, data1, length1);
      int num4 = 0;
      int num5 = checked (iconFile.ImageCount - 1);
      int index = num4;
      while (index <= num5)
      {
        byte[] numArray = iconFile.get_ImageData(index);
        IntPtr hUpdate3 = hUpdate1;
        num2 = new IntPtr(3L);
        IntPtr type2 = num2;
        num1 = new IntPtr(checked ((long) iconBaseID + (long) index));
        IntPtr name2 = num1;
        int num6 = 0;
        byte[] data2 = numArray;
        int length2 = numArray.Length;
        IconInjector.NativeMethods.UpdateResource(hUpdate3, type2, name2, (short) num6, data2, length2);
        checked { ++index; }
      }
      IconInjector.NativeMethods.EndUpdateResource(hUpdate1, false);
    }

    [SuppressUnmanagedCodeSecurity]
    private class NativeMethods
    {
      [DebuggerNonUserCode]
      public NativeMethods()
      {
      }

      [DllImport("kernel32")]
      public static extern IntPtr BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);

      [DllImport("kernel32")]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool UpdateResource(IntPtr hUpdate, IntPtr type, IntPtr name, short language, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] data, int dataSize);

      [DllImport("kernel32")]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool EndUpdateResource(IntPtr hUpdate, [MarshalAs(UnmanagedType.Bool)] bool discard);
    }

    private struct ICONDIR
    {
      public ushort Reserved;
      public ushort Type;
      public ushort Count;
    }

    private struct ICONDIRENTRY
    {
      public byte Width;
      public byte Height;
      public byte ColorCount;
      public byte Reserved;
      public ushort Planes;
      public ushort BitCount;
      public int BytesInRes;
      public int ImageOffset;
    }

    private struct BITMAPINFOHEADER
    {
      public uint Size;
      public int Width;
      public int Height;
      public ushort Planes;
      public ushort BitCount;
      public uint Compression;
      public uint SizeImage;
      public int XPelsPerMeter;
      public int YPelsPerMeter;
      public uint ClrUsed;
      public uint ClrImportant;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    private struct GRPICONDIRENTRY
    {
      public byte Width;
      public byte Height;
      public byte ColorCount;
      public byte Reserved;
      public ushort Planes;
      public ushort BitCount;
      public int BytesInRes;
      public ushort ID;
    }

    private class IconFile
    {
      private IconInjector.ICONDIR iconDir;
      private IconInjector.ICONDIRENTRY[] iconEntry;
      private byte[][] iconImage;

      public int ImageCount
      {
        get
        {
          return (int) this.iconDir.Count;
        }
      }

      private IconFile()
      {
        this.iconDir = new IconInjector.ICONDIR();
      }

      public byte[] get_ImageData(int index)
      {
        return this.iconImage[index];
      }

      public static IconInjector.IconFile FromFile(string filename)
      {
        IconInjector.IconFile iconFile = new IconInjector.IconFile();
        byte[] numArray = File.ReadAllBytes(filename);
        GCHandle gcHandle = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
        iconFile.iconDir = (IconInjector.ICONDIR) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (IconInjector.ICONDIR));
        iconFile.iconEntry = new IconInjector.ICONDIRENTRY[checked ((int) iconFile.iconDir.Count - 1 + 1)];
        iconFile.iconImage = new byte[checked ((int) iconFile.iconDir.Count - 1 + 1)][];
        int num1 = Marshal.SizeOf((object) iconFile.iconDir);
        Type type = typeof (IconInjector.ICONDIRENTRY);
        int num2 = Marshal.SizeOf(type);
        int num3 = 0;
        int num4 = checked ((int) iconFile.iconDir.Count - 1);
        int index = num3;
        while (index <= num4)
        {
          IconInjector.ICONDIRENTRY icondirentry = (IconInjector.ICONDIRENTRY) Marshal.PtrToStructure(new IntPtr(checked (gcHandle.AddrOfPinnedObject().ToInt64() + (long) num1)), type);
          iconFile.iconEntry[index] = icondirentry;
          iconFile.iconImage[index] = new byte[checked (icondirentry.BytesInRes - 1 + 1)];
          Buffer.BlockCopy((Array) numArray, icondirentry.ImageOffset, (Array) iconFile.iconImage[index], 0, icondirentry.BytesInRes);
          checked { num1 += num2; }
          checked { ++index; }
        }
        gcHandle.Free();
        return iconFile;
      }

      public byte[] CreateIconGroupData(uint iconBaseID)
      {
        byte[] numArray = new byte[checked (Marshal.SizeOf(typeof (IconInjector.ICONDIR)) + Marshal.SizeOf(typeof (IconInjector.GRPICONDIRENTRY)) * this.ImageCount - 1 + 1)];
        GCHandle gcHandle1 = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
        Marshal.StructureToPtr((object) this.iconDir, gcHandle1.AddrOfPinnedObject(), false);
        int num1 = Marshal.SizeOf((object) this.iconDir);
        int num2 = 0;
        int num3 = checked (this.ImageCount - 1);
        int index = num2;
        while (index <= num3)
        {
          IconInjector.GRPICONDIRENTRY grpicondirentry = new IconInjector.GRPICONDIRENTRY();
          IconInjector.BITMAPINFOHEADER bitmapinfoheader = new IconInjector.BITMAPINFOHEADER();
          GCHandle gcHandle2 = GCHandle.Alloc((object) bitmapinfoheader, GCHandleType.Pinned);
          Marshal.Copy(this.get_ImageData(index), 0, gcHandle2.AddrOfPinnedObject(), Marshal.SizeOf(typeof (IconInjector.BITMAPINFOHEADER)));
          gcHandle2.Free();
          grpicondirentry.Width = this.iconEntry[index].Width;
          grpicondirentry.Height = this.iconEntry[index].Height;
          grpicondirentry.ColorCount = this.iconEntry[index].ColorCount;
          grpicondirentry.Reserved = this.iconEntry[index].Reserved;
          grpicondirentry.Planes = bitmapinfoheader.Planes;
          grpicondirentry.BitCount = bitmapinfoheader.BitCount;
          grpicondirentry.BytesInRes = this.iconEntry[index].BytesInRes;
          grpicondirentry.ID = checked ((ushort) ((long) iconBaseID + (long) index));
          Marshal.StructureToPtr((object) grpicondirentry, new IntPtr(checked (gcHandle1.AddrOfPinnedObject().ToInt64() + (long) num1)), false);
          checked { num1 += Marshal.SizeOf(typeof (IconInjector.GRPICONDIRENTRY)); }
          checked { ++index; }
        }
        gcHandle1.Free();
        return numArray;
      }
    }
  }
}
