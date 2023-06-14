using GSA_Server.Core.models;

namespace GSA_Server.Core.utils
{
    public class StrategyReader
    {
        public List<StrategyVM> _strategies;
        public IMyFileReader _fileReader;


        public StrategyReader(IMyFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        public List<StrategyVM> Execute()
        {
            _strategies = InitialiseStrategies(_strategies);
            var pnls = ReadPnls().ToDictionary(x => x.StratName);
            var capitals = ReadCapitals().ToDictionary(x => x.StratName);

            foreach(var strategy in _strategies)
            {
                strategy.Pnl = pnls[strategy.StratName].Pnl;
                strategy.Capital = capitals[strategy.StratName].Capital;
            }

            return _strategies;
        }

        public List<StrategyVM> ReadPnls()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "files/pnl.csv");

            var lines = _fileReader.ReadAllLines(filePath);
            var strategiesWithNames = GetStrategyNames(lines);
            return ReadData<PnlVM>(lines, strategiesWithNames, (amount, date) => new PnlVM() { Amount=amount, Date= date}, strategy => strategy.Pnl);
        }

        public List<StrategyVM> ReadCapitals()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "files/capital.csv");

            var lines = _fileReader.ReadAllLines(filePath);
            var strategiesWithNames = GetStrategyNames(lines);
            return ReadData<CapitalVM>(lines, strategiesWithNames, (amount, date) => new CapitalVM() { Amount = amount, Date = date }, strategy => strategy.Capital);

        }
        public List<StrategyVM> GetStrategyNames(string[] lines)
        {
            if (lines[0].Split(",").First() != "Date") throw new Exception("Invalid headers, Date must exist at the first column");
            var headers = lines[0].Split(",").Skip(1).ToArray();
            var strategies = CreateStrategies(headers);
            return strategies;
        }

        private List<StrategyVM> CreateStrategies(string[] headers)
        {
            var strategies = new List<StrategyVM>();
            foreach (var line in headers)
            {
                strategies.Add(new StrategyVM() { StratName = line });
            }

            return strategies;
        }

        public List<StrategyVM> ReadData<T>(string[] lines, List<StrategyVM> strategies, Func<decimal, DateTime, T> CreateFunction, Func<StrategyVM, ICollection<T>> GoToList )
        {
            var stratNames = lines[0]
                .Split(",")
                .Skip(1)
                .ToArray();

            var body = lines
                .Skip(1)
                .Select(row => row.Split(","))
                .ToArray();

            for (int i = 0; i < body.Length; i++)
            {             
                var row = body[i];
                var date = DateTime.Parse(row[0]);
                var amounts = row.Skip(1).ToArray();

                for(int j = 0; j < amounts.Length; j++)
                {
                    var stratName = stratNames[j];
                    var amount = Decimal.Parse(amounts[j]);
                    var current = CreateFunction(amount, date);
                    var currentStrategy = strategies.Where(x => x.StratName == stratName).First();
                    GoToList(currentStrategy).Add(current);
                }
            }

            return strategies;
        }

        public List<StrategyVM> InitialiseStrategies(List<StrategyVM> strategies)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "files/properties.csv");
            var lines = _fileReader.ReadAllLines(filePath);
            return ParseProperties(lines);
        }

        public List<StrategyVM> ParseProperties(string[] lines)
        {
            if (lines[0].Split(",")[0] != "StratName" || lines[0].Split(",")[1] != "Region") throw new Exception("Invalid header names in properties.csv");
            var result = new List<StrategyVM>();
            var body = lines.Skip(1).ToArray();
            foreach (var row in body)
            {
                var rows = row.Split(',').ToArray();
                if (rows.Length != 2 ) {
                    throw new Exception("Did not receive the correct number of rows");
                }
                var strategy = new StrategyVM() {
                    StratName = rows[0],
                    Region = rows[1]
                };
                result.Add(strategy);
            }
            return result;
        }
    }
}
