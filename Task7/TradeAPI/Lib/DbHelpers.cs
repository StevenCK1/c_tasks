using TradeAPI.Db.Entity;
using TradeAPI.Db;
using TradeAPI.Models;

namespace TradeAPI.Lib
{
    public class DbHelpers
    {
        private readonly TradeApiContext _tradeApiContext;

        public DbHelpers(TradeApiContext tradeApiContext)
        {
            _tradeApiContext = tradeApiContext;
        }
        public void PopulatePnLDb(List<StrategyPnlVM> strategyPnlList)
        {

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
