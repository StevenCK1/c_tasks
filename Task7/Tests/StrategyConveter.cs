using TradeAPI.Lib;
using TradeAPI.Models;

namespace Tests
{
    public class Tests
    {
        private CsvConverter _csvConverter;

        [SetUp]
        public void SetUp()
        {
            _csvConverter = new CsvConverter();
        }
       

        [Test]
        public void ConvertStrategy_ShouldReturnCorrectData_WhenFileHasData()
        {
            // Arrange
            string filePath = "pnl.csv";
            string strategy1 = "Strategy1";
            string strategy2 = "Strategy2";
            DateTime date1 = new DateTime(2010, 1, 1);
            DateTime date2 = new DateTime(2010, 1, 4);

            List<StrategyPnl> expectedData = new List<StrategyPnl>
    {
        new StrategyPnl
        {
            Strategy = strategy1,
            Pnls = new List<Pnl>
            {
                new Pnl { Date = date1, Amount = 95045 },
                new Pnl { Date = date2, Amount = -140135 }
            }
        },
        new StrategyPnl
        {
            Strategy = strategy2,
            Pnls = new List<Pnl>
            {
                new Pnl { Date = date1, Amount = 501273 },
                new Pnl { Date = date2, Amount = 369071 }
            }
        }
    };

            // Act
            var actualData = _csvConverter.ConvertStrategy(filePath);

            // Assert
           
        }
    }
}