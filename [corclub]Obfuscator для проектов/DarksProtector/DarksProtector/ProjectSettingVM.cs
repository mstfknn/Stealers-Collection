using Confuser.Core.Project;

namespace DarksProtector
{
    public class ProjectSettingVM<T> : ViewModelBase, IViewModel<SettingItem<T>>
    {
        readonly ProjectVM parent;
        readonly SettingItem<T> setting;

        public ProjectSettingVM(ProjectVM parent, SettingItem<T> setting)
        {
            this.parent = parent;
            this.setting = setting;
        }

        public string Id
        {
            get => this.setting.Id;
            set
            {
                if (this.SetProperty(this.setting.Id != value, val => this.setting.Id = val, value, "Id"))
                    this.parent.IsModified = true;
            }
        }

        public SettingItemAction Action
        {
            get => this.setting.Action;
            set
            {
                if (this.SetProperty(this.setting.Action != value, val => this.setting.Action = val, value, "Action"))
                    this.parent.IsModified = true;
            }
        }

        SettingItem<T> IViewModel<SettingItem<T>>.Model => this.setting;
    }
}