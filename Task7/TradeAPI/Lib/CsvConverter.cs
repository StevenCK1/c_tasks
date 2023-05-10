
using System.Globalization;
using TradeAPI.Models;
using TradeAPI.TradesData;

namespace TradeAPI.Lib
{
    public class CsvConverter
    {
        public List<StrategyPnl> ConvertStrategy(string PnLPath)
        {
            List<StrategyPnl> strategyPnlList = new List<StrategyPnl>();
            using (StreamReader reader = new StreamReader(PnLPath))
            {
                string[] headers = reader.ReadLine().Split(',');

                while (!reader.EndOfStream)
                {
                    string[] fields = reader.ReadLine().Split(',');
                    DateTime date = DateTime.Parse(fields[0]);
                    List<decimal> pnlList = ParsePnlList(fields);

                    for (int i = 0; i < pnlList.Count; i++)
                    {
                        StrategyPnl strategyPnl = GetStrategyPnl(headers, i, strategyPnlList);
                        AddPnlToStrategyPnl(date, pnlList[i], strategyPnl);
                    }
                }
            }

            return strategyPnlList;
        }

        private List<decimal> ParsePnlList(string[] fields)
        {
            List<decimal> pnlList = new List<decimal>();
            for (int i = 1; i < fields.Length; i++)
            {
                decimal pnl = decimal.Parse(fields[i], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                pnlList.Add(pnl);
            }
            return pnlList;
        }

        private StrategyPnl GetStrategyPnl(string[] headers, int index, List<StrategyPnl> strategyPnlList)
        {
            StrategyPnl strategyPnl = strategyPnlList.Find(x => x.Strategy == headers[index + 1]);
            if (strategyPnl == null)
            {
                strategyPnl = new StrategyPnl { Strategy = headers[index + 1], Pnls = new List<Pnl>() };
                strategyPnlList.Add(strategyPnl);
            }
            return strategyPnl;
        }

        private void AddPnlToStrategyPnl(DateTime date, decimal pnl, StrategyPnl strategyPnl)
        {
            strategyPnl.Pnls.Add(new Pnl { Date = date, Amount = pnl });
        }


    }
}
