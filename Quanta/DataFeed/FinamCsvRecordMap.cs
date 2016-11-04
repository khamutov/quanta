using System.Globalization;
using CsvHelper.Configuration;

namespace Quanta.DataFeed
{
    public sealed class FinamCsvRecordMap : CsvClassMap<FinamCsvRecord>
    {
        public FinamCsvRecordMap()
        {
            Map(m => m.Ticker).Name("<TICKER>");
            Map(m => m.Period).Name("<PER>");
            Map(m => m.Date).Name("<DATE>");
            Map(m => m.Time).Name("<TIME>");
            Map(m => m.OpenPrice).ConvertUsing(row => double.Parse(row.GetField<string>("<OPEN>"), CultureInfo.InvariantCulture));
            Map(m => m.ClosePrice).ConvertUsing(row => double.Parse(row.GetField<string>("<CLOSE>"), CultureInfo.InvariantCulture));
            Map(m => m.HighPrice).ConvertUsing(row => double.Parse(row.GetField<string>("<LOW>"), CultureInfo.InvariantCulture));
            Map(m => m.LowPrice).ConvertUsing(row => double.Parse(row.GetField<string>("<CLOSE>"), CultureInfo.InvariantCulture));
            Map(m => m.Volume).ConvertUsing(row => double.Parse(row.GetField<string>("<VOL>"), CultureInfo.InvariantCulture));
        }
    }
}
