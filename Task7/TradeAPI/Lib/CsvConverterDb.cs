using TradeAPI.Db.Entity;
using TradeAPI.Db;


namespace TradeAPI.Lib
{
    public class CsvConverterDb
    {
        private readonly TradeApiContext _tradeApiContext;
        private readonly CsvConverter _csvConverter;

        public CsvConverterDb(TradeApiContext tradeApiContext, CsvConverter csvConverter)
        {
            _tradeApiContext = tradeApiContext;
            _csvConverter = csvConverter;
        }
        public void ConvertAndPopulateDatabase(string PnLPath)
        {
            var strategyPnlList = _csvConverter.ConvertStrategy(PnLPath);

            foreach (var strategyPnl in strategyPnlList)
            {
                // Create a new Strategy entity
                var strategyEntity = new StrategyPnL
                {
                    Strategy = strategyPnl.Strategy
                };

                // Add the Strategy entity to the DbContext
                _tradeApiContext.Add(strategyEntity);

                // Save changes to generate Strategy's ID
                _tradeApiContext.SaveChanges();

                foreach (var pnl in strategyPnl.Pnls)
                {
                    // Create a new PnL entity
                    var pnlEntity = new PnL
                    {
                        Date = pnl.Date,
                        Amount = pnl.Amount,
                        Idstrategy = strategyEntity.Idstrategy // Set the foreign key
                    };

                    // Add the PnL entity to the DbContext
                    _tradeApiContext.PnLs.Add(pnlEntity);
                }
            }

            // Save all changes to the database
            _tradeApiContext.SaveChanges();
        }
    }
}
