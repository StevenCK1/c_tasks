using TradeAPI.Lib;
using TradeAPI.ViewModels;

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

            List<StrategyPnlVM> expectedData = new List<StrategyPnlVM>
    {
        new StrategyPnlVM
        {
            StratName = "Strategy1",
            Pnl = new List<PnLVM>
            {
                new PnLVM { Date = date1, Amount = 95045 },
                new PnLVM { Date = date2, Amount = -140135 }
            }
        },
        new StrategyPnlVM
        {
            StratName = "Strategy2",
            Pnl = new List<PnLVM>
            {
                new PnLVM { Date = date1, Amount = 501273 },
                new PnLVM { Date = date2, Amount = 369071 }
            }
        }
    };

            // Act
            var actualData = _csvConverter.ConvertStrategy(fullPath);

            // Assert
            for (int i = 0; i < expectedData.Count; i++)
            {
                for (int j = 0; j < expectedData[i].Pnl.Count ; j++) {
                
                 Assert.That(actualData[i].StratName, Is.EqualTo(expectedData[i].StratName));
                 Assert.That(actualData[i].Pnl[j].Date, Is.EqualTo(expectedData[i].Pnl[j].Date));
                 Assert.That(actualData[i].Pnl[j].Amount, Is.EqualTo(expectedData[i].Pnl[j].Amount));

                }
            }

        }
    }
}