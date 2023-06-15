using GSA_Server.Core.models;
using GSA_Server.Data.Context;
using GSA_Server.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GSA_Server.Core.utils
{
    // TODO: Single responsiblity principle. Decouple the code! 
    public class DatabaseQuerier
    {
        
        private readonly GsaserverApiContext _dbContext;

        public DatabaseQuerier(GsaserverApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CumulativeCapitalVM> QueryCapitals(string[] strategyNames)
        {
            if (!strategyNames.Any()) return new List<CumulativeCapitalVM>();

            var strategies = GetStrategiesWithCapitals(strategyNames);
            if (!strategies.Any()) return new List<CumulativeCapitalVM>();

            var cumulativeStrategies = CumulateStrategyCapitals(strategies);

            return cumulativeStrategies;
        }

        public List<CumulativeCapitalVM> CumulateStrategyCapitals(List<Strategy> strategies)
        {
            var cumulativeStrategies = new List<CumulativeCapitalVM>();

            foreach (var strategy in strategies)
            {
                var cumulativeStrategy = new CumulativeCapitalVM();
                cumulativeStrategy.StratName = strategy.StratName;

                 cumulativeStrategy.Capital = new List<CapitalVM>();
                var total = 0.0M;
                foreach (var capital in strategy.Capitals)
                {
                    total = capital.Amount + total;

                    var cumulativeCapital = new CapitalVM() { Date = capital.Date, Amount = total };
                    cumulativeStrategy.Capital.Add(cumulativeCapital);
                }
                cumulativeStrategies.Add(cumulativeStrategy);
            }

             return cumulativeStrategies;
        }

        public Dictionary<DateTime, decimal> QueryPnls(string region)
        {
            if (region.IsNullOrEmpty()) throw new NullReferenceException();

            var strategies = GetStrategiesWithPnlsFromRegion(region);

            var pnlDict = CumulateStrategyPnls(strategies);

            return pnlDict;

        }

        public Dictionary<DateTime, decimal> CumulateStrategyPnls(List<Strategy> strategies)
        {
            var pnlDict = new Dictionary<DateTime, decimal>();

            DateTime firstDate = DateTime.MaxValue;
            DateTime lastDate = DateTime.MinValue;

            foreach (var strategy in strategies)
            {
                var cumulativeStrategy = new GSA_Server.Data.Entity.Strategy();
                cumulativeStrategy.StratName = strategy.StratName;

                var currentTotal = 0.0M;

                foreach (var pnl in strategy.Pnls)
                {
                    var currentDate = pnl.Date.Date; // Use only the date part without time

                    if (currentDate < firstDate)
                        firstDate = currentDate;

                    if (currentDate > lastDate)
                        lastDate = currentDate;

                    if (pnlDict.ContainsKey(currentDate))
                    {
                        pnlDict[currentDate] += pnl.Amount;
                    }
                    else
                    {
                        pnlDict[currentDate] = pnl.Amount;
                    }
                }
            }

            // Fill in missing dates with zero PnL
            for (var date = firstDate; date <= lastDate; date = date.AddDays(1))
            {
                if (!pnlDict.ContainsKey(date))
                    pnlDict[date] = 0.0M;
            }

            // Calculate cumulative totals
            decimal cumulativeTotal = 0.0M;
            foreach (var kvp in pnlDict.OrderBy(k => k.Key))
            {
                cumulativeTotal += kvp.Value;
                pnlDict[kvp.Key] = cumulativeTotal;
            }

            return pnlDict;
        }


        private List<GSA_Server.Data.Entity.Strategy> GetStrategiesWithCapitals(string[] strategyNames)
        {
            using (var db = _dbContext)
            {
                var strategies = db.Strategies.Where(x => strategyNames.Contains(x.StratName))
                    .Include(x => x.Capitals)
                    .ToList();

                foreach (var strategy in strategies)
                {
                    strategy.Capitals = strategy.Capitals.OrderBy(x => x.Date).ToList();
                }
                
                return strategies;
            }
    
        }

        private List<GSA_Server.Data.Entity.Strategy> GetStrategiesWithPnlsFromRegion(string region)
        {
            var strategies = new List<GSA_Server.Data.Entity.Strategy>();

            strategies = _dbContext.Strategies.Where(x => x.Region == region)
                .Include(x => x.Pnls)
                .ToList();
            
            return strategies;
        }
    }
}
