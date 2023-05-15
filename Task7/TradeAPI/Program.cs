using TradeAPI.Db;
using TradeAPI.Lib;
using TradeAPI.TradesData;

class Program
{
    static void Main(string[] args)
    {
        // To refactor code so that db operation does not need to have dependency injection of csvConverter

        CsvConverter csvConverter = new CsvConverter();
        string fullPath = Path.Combine(Constants.PnlPath);

        using (TradeApiContext tradeApiContext = new TradeApiContext())
        {
            CsvConverterDb csvConverterDb = new CsvConverterDb(tradeApiContext, csvConverter);

            csvConverterDb.ConvertAndPopulateDatabase(fullPath);
        }

    }
}