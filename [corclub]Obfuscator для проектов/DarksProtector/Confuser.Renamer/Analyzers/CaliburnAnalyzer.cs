using System;
using Confuser.Core;
using Confuser.Renamer.BAML;
using dnlib.DotNet;

namespace Confuser.Renamer.Analyzers {
	internal class CaliburnAnalyzer : IRenamer {
		public CaliburnAnalyzer(WPFAnalyzer wpfAnalyzer) {
			wpfAnalyzer.AnalyzeBAMLElement += AnalyzeBAMLElement;
		}

		public void Analyze(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
			var type = def as TypeDef;
			if (type == null || type.DeclaringType != null)
				return;
			if (type.Name.Contains("ViewModel")) {
				string viewNs = type.Namespace.Replace("ViewModels", "Views");
				string viewName = type.Name.Replace("PageViewModel", "Page").Replace("ViewModel", "View");
				TypeDef view = type.Module.Find(viewNs + "." + viewName, true);
				if (view != null) {
					service.SetCanRename(type, false);
					service.SetCanRename(view, false);
				}

				// Test for Multi-view
				string multiViewNs = type.Namespace + "." + type.Name.Replace("ViewModel", "");
				foreach (TypeDef t in type.Module.Types)
					if (t.Namespace == multiViewNs) {
						service.SetCanRename(type, false);
						service.SetCanRename(t, false);
					}
			}
		}

		void AnalyzeBAMLElement(BAMLAnalyzer analyzer, BamlElement elem) {
			foreach (BamlRecord rec in elem.Body) {
				var prop = rec as PropertyWithConverterRecord;
				if (prop == null)
					continue;

                Tuple<IDnlibDef, AttributeInfoRecord, TypeDef> attr = analyzer.ResolveAttribute(prop.AttributeId);
				string attrName = null;
				if (attr.Item2 != null)
					attrName = attr.Item2.Name;
				else if (attr.Item1 != null)
					attrName = attr.Item1.Name;

				if (attrName == "Attach")
					AnalyzeMessageAttach(analyzer, attr, prop.Value);

				if (attrName == "Name")
					AnalyzeAutoBind(analyzer, attr, prop.Value);

				if (attrName == "MethodName")
					AnalyzeActionMessage(analyzer, attr, prop.Value);
			}
		}

		void AnalyzeMessageAttach(BAMLAnalyzer analyzer, Tuple<IDnlibDef, AttributeInfoRecord, TypeDef> attr, string value) {
			if (attr.Item2 == null)
				return;
            TypeDef attrDeclType = analyzer.ResolveType(attr.Item2.OwnerTypeId);
			if (attrDeclType.FullName != "Caliburn.Micro.Message")
				return;

			foreach (string msg in value.Split(';')) {
				string msgStr = msg.Contains("=") ? msg.Split('=')[1].Trim('[', ']', ' ') : msg.Trim('[', ']', ' ');
                if (msgStr.StartsWith("Action"))
					msgStr = msgStr.Substring(6);
				int parenIndex = msgStr.IndexOf('(');
				if (parenIndex != -1)
					msgStr = msgStr.Substring(0, parenIndex);

				string actName = msgStr.Trim();
				foreach (MethodDef method in analyzer.LookupMethod(actName))
					analyzer.NameService.SetCanRename(method, false);
			}
		}

		void AnalyzeAutoBind(BAMLAnalyzer analyzer, Tuple<IDnlibDef, AttributeInfoRecord, TypeDef> attr, string value) {
			if (!(attr.Item1 is PropertyDef) || ((PropertyDef)attr.Item1).DeclaringType.FullName != "System.Windows.FrameworkElement")
				return;

			foreach (MethodDef method in analyzer.LookupMethod(value))
				analyzer.NameService.SetCanRename(method, false);
			foreach (PropertyDef method in analyzer.LookupProperty(value))
				analyzer.NameService.SetCanRename(method, false);
		}

		void AnalyzeActionMessage(BAMLAnalyzer analyzer, Tuple<IDnlibDef, AttributeInfoRecord, TypeDef> attr, string value) {
			if (attr.Item2 == null)
				return;
            TypeDef attrDeclType = analyzer.ResolveType(attr.Item2.OwnerTypeId);
			if (attrDeclType.FullName != "Caliburn.Micro.ActionMessage")
				return;

			foreach (MethodDef method in analyzer.LookupMethod(value))
				analyzer.NameService.SetCanRename(method, false);
		}


		public void PreRename(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
			//
		}

		public void PostRename(ConfuserContext context, INameService service, ProtectionParameters parameters, IDnlibDef def) {
			//
		}
	}
}