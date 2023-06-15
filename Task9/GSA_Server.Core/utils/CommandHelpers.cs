using GSA_Server.Data.Context;

namespace GSA_Server.Core.utils
{
    public class CommandHelpers
    {

        DatabaseQuerier _databaseQuerier = new DatabaseQuerier(new GsaserverApiContext());

        public CommandHelpers(DatabaseQuerier databaseQuerier)
        {
            _databaseQuerier = databaseQuerier;
        }

        public List<string> ProcessCommands(string command)
        {
            var commandParts = command.Split(' ');
            var results = new List<string>();

            if (commandParts[0] == "capital")
            {
                var strategies = commandParts.Skip(1).ToArray();

               var capitalResults = ProcessCapital(strategies);
               results.AddRange(capitalResults);

            }
            else if (commandParts[0] == "cumulative-pnl")
            {
                var region = commandParts[1];

               var pnlResults = ProcessCumulativePnL(region);
                results.AddRange(pnlResults);
            }
            else
            {
                results.Add("Invalid command.");
            }

            return results;
        }

        public List<string> ProcessCapital(string[] strategies)
        {

            var dbResponse = _databaseQuerier.QueryCapitals(strategies);
            var results = new List<string>();

            for (int i = 0; i < dbResponse[0].Capital.Count(); i++)
            {

                foreach (var response in dbResponse)
                {
                    var capital = response.Capital.ElementAt(i);

                    results.Add($"strategy: {response.StratName}, date: {capital.Date.ToString("yyyy-MM-dd")}, capital: {capital.Amount.ToString("0")}");
                }
            }

            return results;
        }

        public List<string> ProcessCumulativePnL(string region)
        {

            var dbResponse = _databaseQuerier.QueryPnls(region);
            var results = new List<string>();

            var keys = dbResponse.Keys;
            keys.OrderBy(x => x.Date).ToList();

            foreach (var key in keys)
            {
                results.Add($"date: {key.ToString("yyyy-MM-dd")} cululativePnl:{dbResponse[key]}");
            }

            return results;
        }
    }
}
