using System.Collections.Generic;
using Confuser.Core;
using Confuser.Renamer.Analyzers;
using dnlib.DotNet;

namespace Confuser.Renamer
{
    internal class AnalyzePhase : ProtectionPhase
    {

        public AnalyzePhase(NameProtection parent)
            : base(parent) { }

        public override bool ProcessAll => true;

        public override ProtectionTargets Targets => ProtectionTargets.AllDefinitions;

        public override string Name => "Name analysis";

        void ParseParameters(IDnlibDef def, ConfuserContext context, NameService service, ProtectionParameters parameters)
        {
            RenameMode? mode = parameters.GetParameter<RenameMode?>(context, def, "mode", null);
            if (mode != null)
                service.SetRenameMode(def, mode.Value);
        }

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters)
        {
            var service = (NameService)context.Registry.GetService<INameService>();
            foreach (IDnlibDef def in parameters.Targets.WithProgress(context.Logger))
            {
                this.ParseParameters(def, context, service, parameters);

                if (def is ModuleDef module)
                {
                    foreach (Resource res in module.Resources)
                        service.SetOriginalName(res, res.Name);
                }
                else
                    service.SetOriginalName(def, def.Name);

                if (def is TypeDef)
                {
                    service.GetVTables().GetVTable((TypeDef)def);
                    service.SetOriginalNamespace(def, ((TypeDef)def).Namespace);
                }
                context.CheckCancellation();
            }

            this.RegisterRenamers(context, service);
            IList<IRenamer> renamers = service.Renamers;
            foreach (IDnlibDef def in parameters.Targets.WithProgress(context.Logger))
            {
                this.Analyze(service, context, parameters, def, true);
                context.CheckCancellation();
            }
        }

        void RegisterRenamers(ConfuserContext context, NameService service)
        {
            bool wpf = false,
                 caliburn = false,
                 winforms = false,
                 json = false;

            foreach (ModuleDefMD module in context.Modules)
                foreach (AssemblyRef asmRef in module.GetAssemblyRefs())
                {
                    switch (asmRef.Name)
                    {
                        case "WindowsBase":
                        case "PresentationCore":
                        case "PresentationFramework":
                        case "System.Xaml":
                            wpf = true;
                            break;
                        case "Caliburn.Micro":
                            caliburn = true;
                            break;
                        case "System.Windows.Forms":
                            winforms = true;
                            break;
                        case "Newtonsoft.Json":
                            json = true;
                            break;
                    }
                }

            if (wpf)
            {
                var wpfAnalyzer = new WPFAnalyzer();
                //context.Logger.Debug("WPF found, enabling compatibility.");
                service.Renamers.Add(wpfAnalyzer);
                if (caliburn)
                {
                    //context.Logger.Debug("Caliburn.Micro found, enabling compatibility.");
                    service.Renamers.Add(new CaliburnAnalyzer(wpfAnalyzer));
                }
            }

            if (winforms)
            {
                var winformsAnalyzer = new WinFormsAnalyzer();
                //context.Logger.Debug("WinForms found, enabling compatibility.");
                service.Renamers.Add(winformsAnalyzer);
            }

            if (json)
            {
                var jsonAnalyzer = new JsonAnalyzer();
                //context.Logger.Debug("Newtonsoft.Json found, enabling compatibility.");
                service.Renamers.Add(jsonAnalyzer);
            }
        }

        internal void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, IDnlibDef def, bool runAnalyzer)
        {
            if (def is TypeDef)
                this.Analyze(service, context, parameters, (TypeDef)def);
            else if (def is MethodDef)
                this.Analyze(service, context, parameters, (MethodDef)def);
            else if (def is FieldDef)
                this.Analyze(service, context, parameters, (FieldDef)def);
            else if (def is PropertyDef)
                this.Analyze(service, context, parameters, (PropertyDef)def);
            else if (def is EventDef)
                this.Analyze(service, context, parameters, (EventDef)def);
            else if (def is ModuleDef)
            {
                string pass = parameters.GetParameter<string>(context, def, "password", null);
                if (pass != null)
                    service.reversibleRenamer = new ReversibleRenamer(pass);
                service.SetCanRename(def, false);
            }

            if (!runAnalyzer || parameters.GetParameter(context, def, "forceRen", false))
                return;

            foreach (IRenamer renamer in service.Renamers)
                renamer.Analyze(context, service, parameters, def);
        }

        static bool IsVisibleOutside(ConfuserContext context, ProtectionParameters parameters, IMemberDef def)
        {
            if (!(def is TypeDef type))
                type = def.DeclaringType;

            bool? renPublic = parameters.GetParameter<bool?>(context, def, "renPublic", null);
            return renPublic == null ? type.IsVisibleOutside() : type.IsVisibleOutside(false) && !renPublic.Value;
        }

        private void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, TypeDef type)
        {
            if (!IsVisibleOutside(context, parameters, type))
            {
                if (type.IsRuntimeSpecialName || type.IsGlobalModuleType)
                {
                    service.SetCanRename(type, false);
                }
            }
            else
            {
                service.SetCanRename(type, false);
            }

            if (parameters.GetParameter(context, type, "forceRen", false))
                return;

            if (type.InheritsFromCorlib("System.Attribute"))
            {
                service.ReduceRenameMode(type, RenameMode.ASCII);
            }

            if (type.InheritsFrom("System.Configuration.SettingsBase"))
            {
                service.SetCanRename(type, false);
            }
        }

        void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, MethodDef method)
        {
            if (!IsVisibleOutside(context, parameters, method.DeclaringType) ||
                !method.IsFamily && !method.IsFamilyOrAssembly && !method.IsPublic ||
!IsVisibleOutside(context, parameters, method))
            {
                if (!method.IsRuntimeSpecialName)
                {
                    if (!parameters.GetParameter(context, method, "forceRen", false))
                    {
                        if (!method.DeclaringType.IsComImport() || method.HasAttribute("System.Runtime.InteropServices.DispIdAttribute"))
                        {
                            if (method.DeclaringType.IsDelegate())
                                service.SetCanRename(method, false);
                        }
                        else service.SetCanRename(method, false);
                    }
                    else return;
                }
                else service.SetCanRename(method, false);
            }
            else service.SetCanRename(method, false);
        }

        void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, FieldDef field)
        {
            if (!IsVisibleOutside(context, parameters, field.DeclaringType) ||
                !field.IsFamily && !field.IsFamilyOrAssembly && !field.IsPublic ||
!IsVisibleOutside(context, parameters, field))
            {
                if (!field.IsRuntimeSpecialName)
                {
                    if (parameters.GetParameter(context, field, "forceRen", false))
                        return;

                    else if (field.DeclaringType.IsSerializable && !field.IsNotSerialized)
                        service.SetCanRename(field, false);

                    else if (field.IsLiteral && field.DeclaringType.IsEnum &&
                        !parameters.GetParameter(context, field, "renEnum", false))
                        service.SetCanRename(field, false);
                }
                else service.SetCanRename(field, false);
            }
            else service.SetCanRename(field, false);
        }

        void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, PropertyDef property)
        {
            if (IsVisibleOutside(context, parameters, property.DeclaringType) &&
                property.IsPublic() &&
                IsVisibleOutside(context, parameters, property))
                service.SetCanRename(property, false);

            else if (property.IsRuntimeSpecialName)
                service.SetCanRename(property, false);

            else if (parameters.GetParameter(context, property, "forceRen", false))
                return;

            else if (property.DeclaringType.Implements("System.ComponentModel.INotifyPropertyChanged"))
                service.SetCanRename(property, false);

            else if (property.DeclaringType.Name.String.Contains("AnonymousType"))
                service.SetCanRename(property, false);
        }

        void Analyze(NameService service, ConfuserContext context, ProtectionParameters parameters, EventDef evt)
        {
            if (IsVisibleOutside(context, parameters, evt.DeclaringType) &&
                evt.IsPublic() &&
                IsVisibleOutside(context, parameters, evt))
                service.SetCanRename(evt, false);

            else if (evt.IsRuntimeSpecialName)
                service.SetCanRename(evt, false);
        }
    }
}