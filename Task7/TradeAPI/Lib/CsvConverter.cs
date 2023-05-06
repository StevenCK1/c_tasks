
using System.Globalization;
using TradeAPI.Models;
using TradeAPI.TradesData;

namespace TradeAPI.Lib
{
    public class CsvConverter
    {
        private Constants _constants;
        public IEnumerable<StrategyPnl> convertStrategy()
        {
            List<StrategyPnl> strategyPnlList = new List<StrategyPnl>();

            using (StreamReader reader = new StreamReader(_constants.PnlPath))
            {
                string[] headers = reader.ReadLine().Split(',');

                // Loop through the remaining lines
                while (!reader.EndOfStream)
                {
                    string[] fields = reader.ReadLine().Split(',');

                    DateTime date = DateTime.Parse(fields[0]);

                    // Loop through the remaining fields and create a list of decimal values
                    List<decimal> pnlList = new List<decimal>();
                    for (int i = 1; i < fields.Length; i++)
                    {
                        // convert integer to decimals
                        decimal pnl = decimal.Parse(fields[i], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        pnlList.Add(pnl);
                    }

                    // Create a new Pnl object for each strategy and add it to the list of StrategyPnl objects
                    for (int i = 0; i < pnlList.Count; i++)
                    {
                        StrategyPnl strategyPnl = strategyPnlList.Find(x => x.Strategy == headers[i + 1]);
                        if (strategyPnl == null)
                        {
                            strategyPnl = new StrategyPnl { Strategy = headers[i + 1], Pnls = new List<Pnl>() };
                            strategyPnlList.Add(strategyPnl);
                        }
                        strategyPnl.Pnls.Add(new Pnl { Date = date, Amount = pnlList[i] });
                    }
                }
            }

            return strategyPnlList;
        }

    }
}
