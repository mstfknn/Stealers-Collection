// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using System;
using System.IO;
using System.Text;

internal class Class5
{
  private readonly ulong ulong_0;
  private object object_0;
  private readonly byte[] byte_0;
  private Class5.Struct3[] struct3_0;
  private readonly ulong ulong_1;
  private readonly byte[] byte_1;
  private Class5.Struct4[] struct4_0;

  public Class5(string string_0)
  {
    Class11.ARXWv9qzu32dU();
    this.byte_1 = new byte[10]
    {
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 3,
      (byte) 4,
      (byte) 6,
      (byte) 8,
      (byte) 8,
      (byte) 0,
      (byte) 0
    };
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.byte_0 = File.ReadAllBytes(string_0);
    this.ulong_1 = this.method_0(16, 2);
    this.ulong_0 = this.method_0(56, 4);
    this.method_5(100L);
  }

  private ulong method_0(int int_0, int int_1)
  {
    try
    {
      if (int_1 > 8 | int_1 == 0)
        return 0;
      ulong num = 0;
      for (int index = 0; index <= int_1 - 1; ++index)
        num = num << 8 | (ulong) this.byte_0[int_0 + index];
      return num;
    }
    catch
    {
      return 0;
    }
  }

  private long method_1(int int_0, int int_1)
  {
    try
    {
      ++int_1;
      byte[] numArray = new byte[8];
      int num1 = int_1 - int_0;
      bool flag = false;
      if (num1 == 0 | num1 > 9)
        return 0;
      switch (num1)
      {
        case 1:
          numArray[0] = (byte) ((uint) this.byte_0[int_0] & (uint) sbyte.MaxValue);
          return BitConverter.ToInt64(numArray, 0);
        case 9:
          flag = true;
          break;
      }
      int num2 = 1;
      int num3 = 7;
      int index1 = 0;
      if (flag)
      {
        numArray[0] = this.byte_0[int_1 - 1];
        --int_1;
        index1 = 1;
      }
      for (int index2 = int_1 - 1; index2 >= int_0; index2 += -1)
      {
        if (index2 - 1 >= int_0)
        {
          numArray[index1] = (byte) ((int) this.byte_0[index2] >> num2 - 1 & (int) byte.MaxValue >> num2 | (int) this.byte_0[index2 - 1] << num3);
          ++num2;
          ++index1;
          --num3;
        }
        else if (!flag)
          numArray[index1] = (byte) ((int) this.byte_0[index2] >> num2 - 1 & (int) byte.MaxValue >> num2);
      }
      return BitConverter.ToInt64(numArray, 0);
    }
    catch
    {
      return 0;
    }
  }

  public int method_2()
  {
    return this.struct4_0.Length;
  }

  public string method_3(int int_0, int int_1)
  {
    try
    {
      if (int_0 >= this.struct4_0.Length)
        return (string) null;
      return int_1 >= this.struct4_0[int_0].string_0.Length ? (string) null : this.struct4_0[int_0].string_0[int_1];
    }
    catch
    {
      return (string) null;
    }
  }

  private int method_4(int int_0)
  {
    try
    {
      if (int_0 > this.byte_0.Length)
        return 0;
      for (int index = int_0; index <= int_0 + 8; ++index)
      {
        if (index > this.byte_0.Length - 1)
          return 0;
        if (((int) this.byte_0[index] & 128) != 128)
          return index;
      }
      return int_0 + 8;
    }
    catch
    {
      return 0;
    }
  }

  private static bool smethod_0(long long_0)
  {
    return (long_0 & 1L) == 1L;
  }

  private void method_5(long long_0)
  {
    try
    {
      switch (this.byte_0[(int) (IntPtr) long_0])
      {
        case 5:
          ushort num1 = (ushort) (this.method_0((int) long_0 + 3, 2) - 1UL);
          for (int index = 0; index <= (int) num1; ++index)
          {
            ushort num2 = (ushort) this.method_0((int) long_0 + 12 + index * 2, 2);
            if (long_0 == 100L)
              this.method_5(((long) this.method_0((int) num2, 4) - 1L) * (long) this.ulong_1);
            else
              this.method_5(((long) this.method_0((int) (long_0 + (long) num2), 4) - 1L) * (long) this.ulong_1);
          }
          this.method_5(((long) this.method_0((int) long_0 + 8, 4) - 1L) * (long) this.ulong_1);
          break;
        case 13:
          ulong num3 = this.method_0((int) long_0 + 3, 2) - 1UL;
          int num4 = 0;
          if (this.struct3_0 != null)
          {
            num4 = this.struct3_0.Length;
            Array.Resize<Class5.Struct3>(ref this.struct3_0, this.struct3_0.Length + (int) num3 + 1);
          }
          else
            this.struct3_0 = new Class5.Struct3[checked ((ulong) unchecked ((long) num3 + 1L))];
          for (ulong index1 = 0; index1 <= num3; ++index1)
          {
            ulong num2 = this.method_0((int) long_0 + 8 + (int) index1 * 2, 2);
            if (long_0 != 100L)
              num2 += (ulong) long_0;
            int int_1_1 = this.method_4((int) num2);
            this.method_1((int) num2, int_1_1);
            int int_1_2 = this.method_4((int) ((double) num2 + ((double) int_1_1 - (double) num2) + 1.0));
            this.method_1((int) ((double) num2 + ((double) int_1_1 - (double) num2) + 1.0), int_1_2);
            ulong num5 = (ulong) ((double) num2 + ((double) int_1_2 - (double) num2 + 1.0));
            int int_1_3 = this.method_4((int) num5);
            int int_1_4 = int_1_3;
            long num6 = this.method_1((int) num5, int_1_3);
            long[] numArray = new long[5];
            for (int index2 = 0; index2 <= 4; ++index2)
            {
              int int_0 = int_1_4 + 1;
              int_1_4 = this.method_4(int_0);
              numArray[index2] = this.method_1(int_0, int_1_4);
              numArray[index2] = numArray[index2] <= 9L ? (long) this.byte_1[(int) (IntPtr) numArray[index2]] : (!Class5.smethod_0(numArray[index2]) ? (numArray[index2] - 12L) / 2L : (numArray[index2] - 13L) / 2L);
            }
            if (this.ulong_0 == 1UL)
              this.struct3_0[num4 + (int) index1].string_0 = Encoding.Default.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0]), (int) numArray[1]);
            else if (this.ulong_0 == 2UL)
              this.struct3_0[num4 + (int) index1].string_0 = Encoding.Unicode.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0]), (int) numArray[1]);
            else if (this.ulong_0 == 3UL)
              this.struct3_0[num4 + (int) index1].string_0 = Encoding.BigEndianUnicode.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0]), (int) numArray[1]);
            this.struct3_0[num4 + (int) index1].long_0 = (long) this.method_0((int) ((long) num5 + num6 + numArray[0] + numArray[1] + numArray[2]), (int) numArray[3]);
            if (this.ulong_0 == 1UL)
              this.struct3_0[num4 + (int) index1].string_1 = Encoding.Default.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int) numArray[4]);
            else if (this.ulong_0 == 2UL)
              this.struct3_0[num4 + (int) index1].string_1 = Encoding.Unicode.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int) numArray[4]);
            else if (this.ulong_0 == 3UL)
              this.struct3_0[num4 + (int) index1].string_1 = Encoding.BigEndianUnicode.GetString(this.byte_0, (int) ((long) num5 + num6 + numArray[0] + numArray[1] + numArray[2] + numArray[3]), (int) numArray[4]);
          }
          break;
      }
    }
    catch
    {
    }
  }

  public bool method_6(string string_0)
  {
    try
    {
      int index1 = -1;
      for (int index2 = 0; index2 <= this.struct3_0.Length; ++index2)
      {
        if (string.Compare(this.struct3_0[index2].string_0.ToLower(), string_0.ToLower(), StringComparison.Ordinal) == 0)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        return false;
      char[] chArray = new char[1]{ ',' };
      string[] strArray = this.struct3_0[index1].string_1.Substring(this.struct3_0[index1].string_1.IndexOf("(", StringComparison.Ordinal) + 1).Split(chArray);
      for (int index2 = 0; index2 <= strArray.Length - 1; ++index2)
      {
        strArray[index2] = strArray[index2].TrimStart();
        int length = strArray[index2].IndexOf(' ');
        if (length > 0)
          strArray[index2] = strArray[index2].Substring(0, length);
        if (strArray[index2].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
        {
          // ISSUE: cast to a reference type
          Array.Resize<string>((string[]&) ref this.object_0, index2 + 1);
          this.object_0[index2] = (object) strArray[index2];
        }
      }
      return this.method_7((ulong) (this.struct3_0[index1].long_0 - 1L) * this.ulong_1);
    }
    catch
    {
      return false;
    }
  }

  private bool method_7(ulong ulong_2)
  {
    try
    {
      if (this.byte_0[(int) (IntPtr) ((long) ulong_2)] == (byte) 13)
      {
        ushort num1 = (ushort) (this.method_0((int) ulong_2 + 3, 2) - 1UL);
        int num2 = 0;
        if (this.struct4_0 != null)
        {
          num2 = this.struct4_0.Length;
          Array.Resize<Class5.Struct4>(ref this.struct4_0, this.struct4_0.Length + (int) num1 + 1);
        }
        else
          this.struct4_0 = new Class5.Struct4[(int) num1 + 1];
        ushort num3 = num1;
        ushort num4 = 0;
        for (ushort index1 = 0; (int) index1 <= (int) num1; ++index1)
        {
          if ((int) num3 == (int) num1)
            ++num4;
          if (num4 > (ushort) 100)
            return false;
          ulong num5 = this.method_0((int) ulong_2 + 8 + (int) index1 * 2, 2);
          if (ulong_2 != 100UL)
            num5 += ulong_2;
          int int_1_1 = this.method_4((int) num5);
          this.method_1((int) num5, int_1_1);
          int int_1_2 = this.method_4((int) ((double) num5 + ((double) int_1_1 - (double) num5) + 1.0));
          this.method_1((int) ((double) num5 + ((double) int_1_1 - (double) num5) + 1.0), int_1_2);
          ulong num6 = (ulong) ((double) num5 + ((double) int_1_2 - (double) num5 + 1.0));
          int int_1_3 = this.method_4((int) num6);
          int int_1_4 = int_1_3;
          long num7 = this.method_1((int) num6, int_1_3);
          Class5.Struct2[] array = (Class5.Struct2[]) null;
          long num8 = (long) num6 - (long) int_1_3 + 1L;
          int index2 = 0;
          while (num8 < num7)
          {
            Array.Resize<Class5.Struct2>(ref array, index2 + 1);
            int int_0 = int_1_4 + 1;
            int_1_4 = this.method_4(int_0);
            array[index2].long_1 = this.method_1(int_0, int_1_4);
            array[index2].long_0 = array[index2].long_1 <= 9L ? (long) this.byte_1[(int) (IntPtr) array[index2].long_1] : (!Class5.smethod_0(array[index2].long_1) ? (array[index2].long_1 - 12L) / 2L : (array[index2].long_1 - 13L) / 2L);
            num8 = num8 + (long) (int_1_4 - int_0) + 1L;
            ++index2;
          }
          if (array != null)
          {
            this.struct4_0[num2 + (int) index1].string_0 = new string[array.Length];
            int num9 = 0;
            for (int index3 = 0; index3 <= array.Length - 1; ++index3)
            {
              if (array[index3].long_1 > 9L)
              {
                if (!Class5.smethod_0(array[index3].long_1))
                {
                  if (this.ulong_0 == 1UL)
                    this.struct4_0[num2 + (int) index1].string_0[index3] = Encoding.Default.GetString(this.byte_0, (int) ((long) num6 + num7) + num9, (int) array[index3].long_0);
                  else if (this.ulong_0 == 2UL)
                    this.struct4_0[num2 + (int) index1].string_0[index3] = Encoding.Unicode.GetString(this.byte_0, (int) ((long) num6 + num7) + num9, (int) array[index3].long_0);
                  else if (this.ulong_0 == 3UL)
                    this.struct4_0[num2 + (int) index1].string_0[index3] = Encoding.BigEndianUnicode.GetString(this.byte_0, (int) ((long) num6 + num7) + num9, (int) array[index3].long_0);
                }
                else
                  this.struct4_0[num2 + (int) index1].string_0[index3] = Encoding.Default.GetString(this.byte_0, (int) ((long) num6 + num7) + num9, (int) array[index3].long_0);
              }
              else
                this.struct4_0[num2 + (int) index1].string_0[index3] = Convert.ToString(this.method_0((int) ((long) num6 + num7) + num9, (int) array[index3].long_0));
              num9 += (int) array[index3].long_0;
            }
          }
        }
      }
      else if (this.byte_0[(int) (IntPtr) ((long) ulong_2)] == (byte) 5)
      {
        ushort num1 = (ushort) (this.method_0((int) ((long) ulong_2 + 3L), 2) - 1UL);
        for (ushort index = 0; (int) index <= (int) num1; ++index)
        {
          ushort num2 = (ushort) this.method_0((int) ulong_2 + 12 + (int) index * 2, 2);
          this.method_7((this.method_0((int) ((long) ulong_2 + (long) num2), 4) - 1UL) * this.ulong_1);
        }
        this.method_7((this.method_0((int) ((long) ulong_2 + 8L), 4) - 1UL) * this.ulong_1);
      }
      return true;
    }
    catch
    {
      return false;
    }
  }

  private struct Struct2
  {
    public long long_0;
    public long long_1;
  }

  private struct Struct3
  {
    public string string_0;
    public long long_0;
    public string string_1;
  }

  private struct Struct4
  {
    public string[] string_0;
  }
}
