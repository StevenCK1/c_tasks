
using System.Globalization;
using TradeAPI.ViewModels;

namespace TradeAPI.Lib
{
    public class CsvConverter
    {
        public List<StrategyPnlVM> ConvertStrategy(string PnLPath)
        {
            List<StrategyPnlVM> strategyPnlList = new List<StrategyPnlVM>();
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
                        StrategyPnlVM strategyPnl = GetStrategyPnl(headers, i, strategyPnlList);
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

        private StrategyPnlVM GetStrategyPnl(string[] headers, int index, List<StrategyPnlVM> strategyPnlList)
        {
            StrategyPnlVM strategyPnl = strategyPnlList.Find(x => x.StratName == headers[index + 1]);
            if (strategyPnl == null)
            {
                strategyPnl = new StrategyPnlVM { StratName = headers[index + 1], Pnl = new List<PnLVM>() };
                strategyPnlList.Add(strategyPnl);
            }
            return strategyPnl;
        }

        private void AddPnlToStrategyPnl(DateTime date, decimal pnl, StrategyPnlVM strategyPnl)
        {
            strategyPnl.Pnl.Add(new PnLVM { Date = date, Amount = pnl });
        }


    }
}
