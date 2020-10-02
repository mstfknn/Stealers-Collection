#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace KoiVM.Confuser
{
    internal class SimpleSettings
    {
        private readonly SortedDictionary<string, string> values;
        private bool changed;

        public SimpleSettings()
        {
            this.values = new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            string cfgFile = Path.Combine(KoiInfo.KoiDirectory, "koi.cfg");
            if(File.Exists(cfgFile))
                using(var rdr = new StreamReader(File.OpenRead(cfgFile)))
                {
                    string line;
                    int lineNum = 1;
                    while((line = rdr.ReadLine()) != null)
                    {
                        if(string.IsNullOrEmpty(line))
                            continue;
                        int i = line.IndexOf(":");
                        if(i == -1) throw new ArgumentException("Invalid settings.");
                        string val = line.Substring(i + 1);

                        this.values.Add(line.Substring(0, i),
                            val.Equals("null", StringComparison.InvariantCultureIgnoreCase) ? null : val);
                        lineNum++;
                    }
                }
        }

        public string GetValue(string key, string def)
        {
            if (!this.values.TryGetValue(key, out string ret))
            {
                if (def == null) throw new ArgumentException($"'{key}' does not exist in settings.");
                ret = this.values[key] = def;
                this.changed = true;
            }
            return ret;
        }

        public T GetValue<T>(string key, string def)
        {
            if (!this.values.TryGetValue(key, out string ret))
            {
                if (def == null) throw new ArgumentException($"'{key}' does not exist in settings.");
                ret = this.values[key] = def;
                this.changed = true;
            }
            return (T) Convert.ChangeType(ret, typeof(T));
        }

        public void SetValue(string key, string val)
        {
            if(!this.values.ContainsKey(key) || this.values[key] != val)
            {
                this.values[key] = val;
                this.changed = true;
            }
        }

        public void Save()
        {
            if(!this.changed) return;
            string cfgFile = Path.Combine(KoiInfo.KoiDirectory, "koi.cfg");

            this.changed = false;
        }
    }
}