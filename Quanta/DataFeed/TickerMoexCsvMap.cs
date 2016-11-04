using CsvHelper.Configuration;

namespace Quanta.DataFeed
{
    public sealed class TickerMoexCsvMap : CsvClassMap<Ticker>
    {
        public TickerMoexCsvMap()
        {
            Map(m => m.Code).Name("SECID");
            Map(m => m.Name).Name("NAME");
            Map(m => m.ShortName).Name("SHORTNAME");
            Map(m => m.TypeName).Name("TYPENAME");
            Map(m => m.Isin).Name("ISIN");
            Map(m => m.ListLevel).Name("LISTLEVEL");
        }
    }
}
