using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace codeLouisville
{
    public class NumbersMap : ClassMap<Numbers>
    {

        public NumbersMap()
        {
            Map(x => x.Date).Name("Draw Date");
            Map(x => x.WinningNumbers).Name("Winning Numbers");
            Map(x => x.MegaBall).Name("Mega Ball");
            Map(x => x.Multiplier).Name("Multiplier");
        }

    }
}
