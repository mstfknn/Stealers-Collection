﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using Confuser.Core;
using Confuser.Core.Services;
using Confuser.Renamer.BAML;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.IO;

namespace Confuser.Renamer.Analyzers
{
    internal class WPFAnalyzer : IRenamer {
		static readonly object BAMLKey = new object();

		static readonly Regex ResourceNamePattern = new Regex("^.*\\.g\\.resources$");
		internal static readonly Regex UriPattern = new Regex("(?:;COMPONENT|APPLICATION\\:,,,)(/.+\\.[BX]AML)$");
		BAMLAnalyzer analyzer;

		internal Dictionary<string, List<IBAMLReference>> bamlRefs = new Dictionary<string, List<IBAMLReference>>(StringComparer.OrdinalIgnoreCase);
		public event Action<BAMLAnalyzer, BamlElement> AnalyzeBAMLElement;

		public void Analyze(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
            if (def is MethodDef method)
            {
                if (!method.HasBody)
                    return;
                AnalyzeMethod(context, service, method);
            }

            if (def is ModuleDefMD module)
            {
                AnalyzeResources(context, service, module);
            }
        }

		public void PreRename(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
            if (!(def is ModuleDefMD module) || !parameters.GetParameter<bool>(context, def, "renXaml", true))
                return;

            Dictionary<string, Dictionary<string, BamlDocument>> wpfResInfo = context.Annotations.Get<Dictionary<string, Dictionary<string, BamlDocument>>>(module, BAMLKey);
			if (wpfResInfo == null)
				return;

			foreach (Dictionary<string, BamlDocument> res in wpfResInfo.Values)
				foreach (BamlDocument doc in res.Values) {
                    if (bamlRefs.TryGetValue(doc.DocumentName, out List<IBAMLReference> references))
                    {
                        string newName = doc.DocumentName.ToUpperInvariant();

                        #region old code

                        //if (newName.EndsWith(".BAML"))
                        //    newName = service.RandomName(RenameMode.Letters).ToLowerInvariant() + ".baml";
                        //else if (newName.EndsWith(".XAML"))
                        //    newName = service.RandomName(RenameMode.Letters).ToLowerInvariant() + ".xaml";

                        #endregion

                        #region Niks patch fix

                        /*
                         * Nik's patch for maintaining relative paths. If the xaml file is referenced in this manner
                         * "/some.namespace;component/somefolder/somecontrol.xaml"
                         * then we want to keep the relative path and namespace intact. We should be obfuscating it like this - /some.namespace;component/somefolder/asjdjh2398498dswk.xaml
                        * */

                        string[] completePath = newName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                        string newShinyName = string.Empty;
                        for (int i = 0; i <= completePath.Length - 2; i++)
                        {
                            newShinyName += completePath[i].ToLowerInvariant() + "/";
                        }
                        if (newName.EndsWith(".BAML"))
                            newName = newShinyName + service.RandomName() + ".baml";
                        else if (newName.EndsWith(".XAML"))
                            newName = newShinyName + service.RandomName() + ".xaml";

                        context.Logger.Log(String.Format("Preserving virtual paths. Replaced {0} with {1}", doc.DocumentName, newName));

                        #endregion

                        bool renameOk = true;
                        foreach (IBAMLReference bamlRef in references)
                            if (!bamlRef.CanRename(doc.DocumentName, newName))
                            {
                                renameOk = false;
                                break;
                            }

                        if (renameOk)
                        {
                            foreach (IBAMLReference bamlRef in references)
                                bamlRef.Rename(doc.DocumentName, newName);
                            doc.DocumentName = newName;
                        }
                    }
                }
		}

		public void PostRename(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
            if (!(def is ModuleDefMD module))
                return;

            Dictionary<string, Dictionary<string, BamlDocument>> wpfResInfo = context.Annotations.Get<Dictionary<string, Dictionary<string, BamlDocument>>>(module, BAMLKey);
			if (wpfResInfo == null)
				return;

			foreach (EmbeddedResource res in module.Resources.OfType<EmbeddedResource>()) {

                if (!wpfResInfo.TryGetValue(res.Name, out Dictionary<string, BamlDocument> resInfo))
                    continue;

                var stream = new MemoryStream();
				var writer = new ResourceWriter(stream);

				res.Data.Position = 0;
				var reader = new ResourceReader(new ImageStream(res.Data));
				IDictionaryEnumerator enumerator = reader.GetEnumerator();
				while (enumerator.MoveNext()) {
                    string name = (string)enumerator.Key;
                    reader.GetResourceData(name, out string typeName, out byte[] data);

                    if (resInfo.TryGetValue(name, out BamlDocument document))
                    {
                        var docStream = new MemoryStream
                        {
                            Position = 4
                        };
                        BamlWriter.WriteDocument(document, docStream);
                        docStream.Position = 0;
                        docStream.Write(BitConverter.GetBytes((int)docStream.Length - 4), 0, 4);
                        data = docStream.ToArray();
                        name = document.DocumentName;
                    }

                    writer.AddResourceData(name, typeName, data);
				}
				writer.Generate();
				res.Data = MemoryImageStream.Create(stream.ToArray());
			}
		}

		void AnalyzeMethod(ConfuserContext context, INameService service, MethodDef method) {
			var dpRegInstrs = new List<Tuple<bool, Instruction>>();
			var routedEvtRegInstrs = new List<Instruction>();
			for (int i = 0; i < method.Body.Instructions.Count; i++) {
				Instruction instr = method.Body.Instructions[i];
				if ((instr.OpCode.Code == Code.Call || instr.OpCode.Code == Code.Callvirt)) {
					var regMethod = (IMethod)instr.Operand;

					if (regMethod.DeclaringType.FullName == "System.Windows.DependencyProperty" &&
					    regMethod.Name.String.StartsWith("Register")) {
						dpRegInstrs.Add(Tuple.Create(regMethod.Name.String.StartsWith("RegisterAttached"), instr));
					}
					else if (regMethod.DeclaringType.FullName == "System.Windows.EventManager" &&
					         regMethod.Name.String == "RegisterRoutedEvent") {
						routedEvtRegInstrs.Add(instr);
					}
				}
				else if (instr.OpCode.Code == Code.Newobj) {
					var methodRef = (IMethod)instr.Operand;

					if (methodRef.DeclaringType.FullName == "System.Windows.Data.PropertyGroupDescription" &&
					    methodRef.Name == ".ctor" && i - 1 >= 0 && method.Body.Instructions[i - 1].OpCode.Code == Code.Ldstr) {
						foreach (PropertyDef property in analyzer.LookupProperty((string)method.Body.Instructions[i - 1].Operand))
							service.SetCanRename(property, false);
					}
				}
				else if (instr.OpCode == OpCodes.Ldstr) {
                    string operand = ((string)instr.Operand).ToUpperInvariant();
					if (operand.EndsWith(".BAML") || operand.EndsWith(".XAML")) {
                        Match match = UriPattern.Match(operand);
						if (match.Success)
							operand = match.Groups[1].Value;
						else if (operand.Contains("/"))
							context.Logger.LogFormat("WARN: Fail to extract XAML name from '{0}'.", instr.Operand);

						var reference = new BAMLStringReference(instr);
						operand = operand.TrimStart('/');
                        string baml = operand.Substring(0, operand.Length - 5) + ".BAML";
                        string xaml = operand.Substring(0, operand.Length - 5) + ".XAML";
						bamlRefs.AddListEntry(baml, reference);
						bamlRefs.AddListEntry(xaml, reference);
					}
				}
			}

			if (dpRegInstrs.Count == 0)
				return;

            ITraceService traceSrv = context.Registry.GetService<ITraceService>();
			MethodTrace trace = traceSrv.Trace(method);

			bool erred = false;
			foreach (Tuple<bool, Instruction> instrInfo in dpRegInstrs) {
				int[] args = trace.TraceArguments(instrInfo.Item2);
				if (args == null) {
					if (!erred)
						context.Logger.LogFormat("WARN: Failed to extract dependency property name in '{0}'.", method.FullName);
					erred = true;
					continue;
				}
				Instruction ldstr = method.Body.Instructions[args[0]];
				if (ldstr.OpCode.Code != Code.Ldstr) {
					if (!erred)
						context.Logger.LogFormat("WARN: Failed to extract dependency property name in '{0}'.", method.FullName);
					erred = true;
					continue;
				}

                string name = (string)ldstr.Operand;
				TypeDef declType = method.DeclaringType;
				bool found = false;
				if (instrInfo.Item1) // Attached DP
				{
					MethodDef accessor;
					if ((accessor = declType.FindMethod("Get" + name)) != null && accessor.IsStatic) {
						service.SetCanRename(accessor, false);
						found = true;
					}
					if ((accessor = declType.FindMethod("Set" + name)) != null && accessor.IsStatic) {
						service.SetCanRename(accessor, false);
						found = true;
					}
				}

				// Normal DP
				// Find CLR property for attached DP as well, because it seems attached DP can be use as normal DP as well.
				PropertyDef property = null;
				if ((property = declType.FindProperty(name)) != null) {
					service.SetCanRename(property, false);

					found = true;
					if (property.GetMethod != null)
						service.SetCanRename(property.GetMethod, false);

					if (property.SetMethod != null)
						service.SetCanRename(property.SetMethod, false);

					if (property.HasOtherMethods) {
						foreach (MethodDef accessor in property.OtherMethods)
							service.SetCanRename(accessor, false);
					}
				}
				if (!found) {
					if (instrInfo.Item1)
						context.Logger.LogFormat("WARN: Failed to find the accessors of attached dependency property '{0}' in type '{1}'.",
						                          name, declType.FullName);
					else
						context.Logger.LogFormat("WARN: Failed to find the CLR property of normal dependency property '{0}' in type '{1}'.",
						                          name, declType.FullName);
				}
			}

			erred = false;
			foreach (Instruction instr in routedEvtRegInstrs) {
				int[] args = trace.TraceArguments(instr);
				if (args == null) {
					if (!erred)
						context.Logger.LogFormat("WARN: Failed to extract routed event name in '{0}'.", method.FullName);
					erred = true;
					continue;
				}
				Instruction ldstr = method.Body.Instructions[args[0]];
				if (ldstr.OpCode.Code != Code.Ldstr) {
					if (!erred)
						context.Logger.LogFormat("WARN: Failed to extract routed event name in '{0}'.", method.FullName);
					erred = true;
					continue;
				}

                string name = (string)ldstr.Operand;
				TypeDef declType = method.DeclaringType;

				EventDef eventDef = null;
				if ((eventDef = declType.FindEvent(name)) == null) {
					context.Logger.LogFormat("WARN: Failed to find the CLR event of routed event '{0}' in type '{1}'.",
					                          name, declType.FullName);
					continue;
				}
				service.SetCanRename(eventDef, false);

				if (eventDef.AddMethod != null)
					service.SetCanRename(eventDef.AddMethod, false);

				if (eventDef.RemoveMethod != null)
					service.SetCanRename(eventDef.RemoveMethod, false);

				if (eventDef.InvokeMethod != null)
					service.SetCanRename(eventDef.InvokeMethod, false);

				if (eventDef.HasOtherMethods) {
					foreach (MethodDef accessor in eventDef.OtherMethods)
						service.SetCanRename(accessor, false);
				}
			}
		}

		void AnalyzeResources(ConfuserContext context, INameService service, ModuleDefMD module) {
			if (analyzer == null) {
				analyzer = new BAMLAnalyzer(context, service);
				analyzer.AnalyzeElement += AnalyzeBAMLElement;
			}

			var wpfResInfo = new Dictionary<string, Dictionary<string, BamlDocument>>();

			foreach (EmbeddedResource res in module.Resources.OfType<EmbeddedResource>()) {
				Match match = ResourceNamePattern.Match(res.Name);
				if (!match.Success)
					continue;

				var resInfo = new Dictionary<string, BamlDocument>();

				res.Data.Position = 0;
				var reader = new ResourceReader(new ImageStream(res.Data));
				IDictionaryEnumerator enumerator = reader.GetEnumerator();
				while (enumerator.MoveNext()) {
                    string name = (string)enumerator.Key;
					if (!name.EndsWith(".baml"))
						continue;
                    reader.GetResourceData(name, out string typeName, out byte[] data);
                    BamlDocument document = analyzer.Analyze(module, name, data);
					document.DocumentName = name;
					resInfo.Add(name, document);
				}

				if (resInfo.Count > 0)
					wpfResInfo.Add(res.Name, resInfo);
			}
			if (wpfResInfo.Count > 0)
				context.Annotations.Set(module, BAMLKey, wpfResInfo);
		}
	}
}