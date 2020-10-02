using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using Confuser.Core.Project;

namespace DarksProtector
{
    public class ProjectModuleVM : ViewModelBase, IViewModel<ProjectModule>, IRuleContainer
    {
        private readonly ProjectVM parent;
        private string asmName = "Unknown", simpleName;
        bool isSelected;

        public ProjectModuleVM(ProjectVM parent, ProjectModule module)
        {
            this.parent = parent;
            this.Module = module;

            ObservableCollection<ProjectRuleVM> rules = Utils.Wrap(module.Rules, rule => new ProjectRuleVM(parent, rule));
            rules.CollectionChanged += (sender, e) => parent.IsModified = true;
            this.Rules = rules;

            if (module.Path != null)
            {
                this.SimpleName = System.IO.Path.GetFileName(module.Path);
                this.LoadAssemblyName();
            }
        }

        public bool IsSelected
        {
            get => this.isSelected;
            set => this.SetProperty(ref this.isSelected, value, "IsSelected");
        }

        public ProjectModule Module { get; }

        public string Path
        {
            get => this.Module.Path;
            set
            {
                if (this.SetProperty(this.Module.Path != value, val => this.Module.Path = val, value, "Path"))
                {
                    this.parent.IsModified = true;
                    this.SimpleName = System.IO.Path.GetFileName(this.Module.Path);
                    this.LoadAssemblyName();
                }
            }
        }

        public string SimpleName
        {
            get => this.simpleName;
            private set => this.SetProperty(ref this.simpleName, value, "SimpleName");
        }

        public string AssemblyName
        {
            get => this.asmName;
            private set => this.SetProperty(ref this.asmName, value, "AssemblyName");
        }

        public string SNKeyPath
        {
            get => this.Module.SNKeyPath;
            set
            {
                if (this.SetProperty(this.Module.SNKeyPath != value, val => this.Module.SNKeyPath = val, value, "SNKeyPath"))
                    this.parent.IsModified = true;
            }
        }

        public string SNKeyPassword
        {
            get => this.Module.SNKeyPassword;
            set
            {
                if (this.SetProperty(this.Module.SNKeyPassword != value, val => this.Module.SNKeyPassword = val, value, "SNKeyPassword"))
                    this.parent.IsModified = true;
            }
        }

        public IList<ProjectRuleVM> Rules { get; private set; }

        ProjectModule IViewModel<ProjectModule>.Model => this.Module;

        void LoadAssemblyName()
        {
            this.AssemblyName = "Loading...";
            ThreadPool.QueueUserWorkItem(_ => {
                try
                {
                    string path = System.IO.Path.Combine(this.parent.BaseDirectory, this.Path);
                    if (!string.IsNullOrEmpty(this.parent.FileName))
                        path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.parent.FileName), path);
                    var name = System.Reflection.AssemblyName.GetAssemblyName(path);
                    this.AssemblyName = name.FullName;
                }
                catch
                {
                    this.AssemblyName = "Unknown";
                }
            });
        }
    }
}