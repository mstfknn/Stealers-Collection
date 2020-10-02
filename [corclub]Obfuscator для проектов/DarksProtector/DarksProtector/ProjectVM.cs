using System.Collections.Generic;
using System.Collections.ObjectModel;
using Confuser.Core;
using Confuser.Core.Project;

namespace DarksProtector
{
    public class ProjectVM : ViewModelBase, IViewModel<ConfuserProject>, IRuleContainer
    {
        private bool modified;
        ProjectSettingVM<Packer> packer;

        public ProjectVM(ConfuserProject proj, string fileName)
        {
            this.Project = proj;
            this.FileName = fileName;

            ObservableCollection<ProjectModuleVM> modules = Utils.Wrap(proj, module => new ProjectModuleVM(this, module));
            modules.CollectionChanged += (sender, e) => this.IsModified = true;
            this.Modules = modules;

            ObservableCollection<StringItem> plugins = Utils.Wrap(proj.PluginPaths, path => new StringItem(path));
            plugins.CollectionChanged += (sender, e) => this.IsModified = true;
            this.Plugins = plugins;

            ObservableCollection<StringItem> probePaths = Utils.Wrap(proj.ProbePaths, path => new StringItem(path));
            probePaths.CollectionChanged += (sender, e) => this.IsModified = true;
            this.ProbePaths = probePaths;

            ObservableCollection<ProjectRuleVM> rules = Utils.Wrap(proj.Rules, rule => new ProjectRuleVM(this, rule));
            rules.CollectionChanged += (sender, e) => this.IsModified = true;
            this.Rules = rules;

            this.Protections = new ObservableCollection<ConfuserComponent>();
            this.Packers = new ObservableCollection<ConfuserComponent>();
        }

        public ConfuserProject Project { get; }

        public bool IsModified
        {
            get => this.modified;
            set => this.SetProperty(ref this.modified, value, "IsModified");
        }

        public string Seed
        {
            get => this.Project.Seed;
            set => this.SetProperty(this.Project.Seed != value, val => this.Project.Seed = val, value, "Seed");
        }

        public bool Debug
        {
            get => this.Project.Debug;
            set => this.SetProperty(this.Project.Debug != value, val => this.Project.Debug = val, value, "Debug");
        }

        public string BaseDirectory
        {
            get => this.Project.BaseDirectory;
            set => this.SetProperty(this.Project.BaseDirectory != value, val => this.Project.BaseDirectory = val, value, "BaseDirectory");
        }

        public string OutputDirectory
        {
            get => this.Project.OutputDirectory;
            set => this.SetProperty(this.Project.OutputDirectory != value, val => this.Project.OutputDirectory = val, value, "OutputDirectory");
        }

        public ProjectSettingVM<Packer> Packer
        {
            get
            {
                this.packer = this.Project.Packer == null ? null : new ProjectSettingVM<Packer>(this, this.Project.Packer);
                return this.packer;
            }
            set
            {
                var vm = (IViewModel<SettingItem<Packer>>)value;
                bool changed = (vm == null && this.Project.Packer != null) || (vm != null && this.Project.Packer != vm.Model);
                this.SetProperty(changed, val => this.Project.Packer = val == null ? null : val.Model, vm, "Packer");
            }
        }

        public IList<ProjectModuleVM> Modules { get; private set; }
        public IList<StringItem> Plugins { get; private set; }
        public IList<StringItem> ProbePaths { get; private set; }

        public ObservableCollection<ConfuserComponent> Protections { get; private set; }
        public ObservableCollection<ConfuserComponent> Packers { get; private set; }
        public IList<ProjectRuleVM> Rules { get; private set; }

        public string FileName { get; set; }

        ConfuserProject IViewModel<ConfuserProject>.Model => this.Project;

        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            if (property != "IsModified")
                this.IsModified = true;
        }
    }
}