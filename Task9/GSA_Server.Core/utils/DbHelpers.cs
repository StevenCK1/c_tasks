using GSA_Server.Core.models;
using GSA_Server.Data.Context;

namespace GSA_Server.Core.utils
{
    public class DbHelpers
    {
        private readonly GsaserverApiContext _dbContext;
        public DbHelpers(GsaserverApiContext dbContext)
        {
           _dbContext = dbContext;
        }
        public  void SaveToDatabase(List<StrategyVM> strategies)
        {
            using (var db = _dbContext)
            {
                db.RemoveRange(db.Capitals);
                db.RemoveRange(db.Pnls);
                db.RemoveRange(db.Strategies);
                db.SaveChanges();
            }
            foreach (StrategyVM strategy in strategies)
            {
                using (var db = _dbContext)
                {
                    GSA_Server.Data.Entity.Strategy dbStrategies = new();
                    dbStrategies.StratName = strategy.StratName;
                    dbStrategies.Region = strategy.Region;

                    db.Strategies.Add(dbStrategies);
                    db.SaveChanges();

                    List<Data.Entity.Pnl> dbPnls = new();
                    List<Data.Entity.Capital> dbCapitals = new();

                    foreach (var pnl in strategy.Pnl)
                    {
                        var dbPnl = new Data.Entity.Pnl()
                        {
                            StrategyId = dbStrategies.StrategyId,
                            Date = pnl.Date,
                            Amount = pnl.Amount,
                        };

                        dbPnls.Add(dbPnl);
                    }

                    foreach (var capital in strategy.Capital)
                    {
                        var dbCapital = new Data.Entity.Capital()
                        {
                            StrategyId = dbStrategies.StrategyId,
                            Date = capital.Date,
                            Amount = capital.Amount
                        };

                        dbCapitals.Add(dbCapital);
                    }

                    dbStrategies.Capitals = dbCapitals;
                    dbStrategies.Pnls = dbPnls;
                    db.SaveChanges();
                }
            }
        }
    }
}
