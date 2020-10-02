namespace NoFile
{
    using System;
    using System.Runtime.CompilerServices;

    public class CardData
    {
        public override string ToString()
        {
            return string.Format("{0}\t{1}/{2}\t{3}\t{4}\r\n******************************\r\n", new object[] { this.Name, this.Exp_m, this.Exp_y, this.Number, this.Billing });
        }

        public string Billing { get; set; }

        public string Exp_m { get; set; }

        public string Exp_y { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
    }
}

