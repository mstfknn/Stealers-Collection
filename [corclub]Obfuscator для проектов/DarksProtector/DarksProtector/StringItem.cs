using System;

namespace DarksProtector
{
    public class StringItem : IViewModel<string>
    {
        public StringItem(string item)
        {
            this.Item = item;
        }

        public string Item { get; private set; }

        string IViewModel<string>.Model => this.Item;

        public override string ToString()
        {
            return this.Item;
        }
    }
}