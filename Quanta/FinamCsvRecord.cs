using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanta
{
    public class FinamCsvRecord
    {
        public string Ticker { get; set; }
        public string Period { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double Volume { get; set; }
    }
}
