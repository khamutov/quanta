namespace Quanta.DataFeed
{
    public class TickerMoexDownloader
    {
        private const string url =
            @"http://www.moex.com/iss/apps/rms/rates.csv?iss.only=object&limit=unlimited&sort_column=SECID&sort_order=ASC&security_types=common_share,preferred_share&board_groups=stock_tplus,stock_ndm_tplus&index=&listname=1,2,3&collateral=0&currencyid=&lang=ru";
    }
}
