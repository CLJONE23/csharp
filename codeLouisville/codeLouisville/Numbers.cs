using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace codeLouisville
{
    public class Numbers
    {
        public DateTime Date { get; set; }
        public string WinningNumbers { get; set; }
        public string MegaBall { get; set; }
        public string Multiplier { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Date: ")
                .Append(Date)
                .Append(", Winning Numbers: ")
                .Append(WinningNumbers)
                .Append(", MegaBall: ")
                .Append(MegaBall);
            return sb.ToString();
        }

    }
}
