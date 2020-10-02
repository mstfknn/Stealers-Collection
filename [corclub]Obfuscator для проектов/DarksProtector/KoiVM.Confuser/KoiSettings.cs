namespace KoiVM.Confuser
{
    internal class KoiSettings : SimpleSettings
    {
        public bool NoUI
        {
            get
            {
                if (bool.TryParse(this.GetValue("noUI", "false"), out bool value))
                    return value;
                this.SetValue("noUI", "false");
                return false;
            }
            set => this.SetValue("noUI", value.ToString().ToLowerInvariant());
        }

        public bool NoCheck
        {
            get
            {
                if (bool.TryParse(this.GetValue("noCheck", "false"), out bool value))
                    return value;
                this.SetValue("noCheck", "false");
                return false;
            }
            set => this.SetValue("noCheck", value.ToString().ToLowerInvariant());
        }

        public string Version
        {
            get
            {
                string value = this.GetValue("ver", "");
                return string.IsNullOrEmpty(value) ? null : value;
            }
            set => this.SetValue("ver", value);
        }

        public string KoiID
        {
            get
            {
                string value = this.GetValue("id", "");
                return string.IsNullOrEmpty(value) ? null : value;
            }
            set => this.SetValue("id", value);
        }
    }
}