using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace NoFile
{
    public class CardData
    {
        public string Name { get; set; }

        public string Exp_m { get; set; }

        public string Exp_y { get; set; }

        public string Number { get; set; }

        public string Billing { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}/{2}\t{3}\t{4}\r\n******************************\r\n", (object)this.Name, (object)this.Exp_m, (object)this.Exp_y, (object)this.Number, (object)this.Billing);
        }
    }
}

