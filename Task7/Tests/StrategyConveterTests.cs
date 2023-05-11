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
            string relativePath = @".\TradesData\pnl.csv";
            string fullPath = Path.Combine(relativePath);
            DateTime date1 = new DateTime(2010, 1, 1);
            DateTime date2 = new DateTime(2010, 1, 4);

            List<StrategyPnl> expectedData = new List<StrategyPnl>
    {
        new StrategyPnl
        {
            Strategy = "Strategy1",
            Pnls = new List<Pnl>
            {
                new Pnl { Date = date1, Amount = 95045 },
                new Pnl { Date = date2, Amount = -140135 }
            }
        },
        new StrategyPnl
        {
            Strategy = "Strategy2",
            Pnls = new List<Pnl>
            {
                new Pnl { Date = date1, Amount = 501273 },
                new Pnl { Date = date2, Amount = 369071 }
            }
        }
    };

            // Act
            var actualData = _csvConverter.ConvertStrategy(fullPath);

            // Assert
            for (int i = 0; i < expectedData.Count; i++)
            {
                for (int j = 0; j < expectedData[i].Pnls.Count ; j++) {
                
                 Assert.That(actualData[i].Strategy, Is.EqualTo(expectedData[i].Strategy));
                 Assert.That(actualData[i].Pnls[j].Date, Is.EqualTo(expectedData[i].Pnls[j].Date));
                 Assert.That(actualData[i].Pnls[j].Amount, Is.EqualTo(expectedData[i].Pnls[j].Amount));

                }
            }

        }
    }
}