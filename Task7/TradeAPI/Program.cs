using TradeAPI.Constants;
using TradeAPI.Db;
using TradeAPI.Lib;

class Program
{
    static void Main(string[] args)
    {
        CsvConverter csvConverter = new CsvConverter();
        string fullPath = Path.Combine(Constants.PnlPath);

        var strategyPnL = csvConverter.ConvertStrategy(fullPath);

        using (TradeApiContext tradeApiContext = new TradeApiContext())
        {
            DbHelpers csvConverterDb = new DbHelpers(tradeApiContext);

            csvConverterDb.PopulatePnLDb(strategyPnL);
        }

    }
}