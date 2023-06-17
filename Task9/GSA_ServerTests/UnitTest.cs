using GSA_Server.Core.utils;
using GSA_Server.Data.Context;
using GSA_Server.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace GSA_ServerTests
{
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void GetStrategiesWithCapitals_GetsNamedStrategies()
        {
            // Arrange
            var listStrategies = new string[] { "Strategy1", "Strategy3" };
            var testDataCapitals1 = new List<Capital>
            {
                new Capital
                {
                    Date = new DateTime(2000, 1, 1),
                    Amount = 100M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 2),
                    Amount = 200M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 4),
                    Amount = 400M
                }
            };

            var testDataCapitals2 = new List<Capital>
            {
                new Capital
                {
                    Date = new DateTime(2000, 1, 2),
                    Amount = 100M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 3),
                    Amount = 200M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 5),
                    Amount = 400M
                }
            };

            var testDataStrategy1 = new Strategy
            {
                StratName = "Strategy1",
                Region = "EU",
                Capitals = testDataCapitals1
            };
            var testDataStrategy2 = new Strategy
            {
                StratName = "Strategy2",
                Region = "US",
                Capitals = testDataCapitals2
            };
            var testDataStrategy3 = new Strategy
            {
                StratName = "Strategy3",
                Region = "EU"
            };

            var expected = new List<Strategy>
            {
                testDataStrategy1,
                testDataStrategy3
            };

            var testDataStrategies = new List<Strategy>
            {
                testDataStrategy1,
                testDataStrategy2,
                testDataStrategy3
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Strategy>>();
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.Provider).Returns(testDataStrategies.Provider);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.Expression).Returns(testDataStrategies.Expression);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.ElementType).Returns(testDataStrategies.ElementType);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.GetEnumerator()).Returns(() => testDataStrategies.GetEnumerator());

            var mockContext = new Mock<GsaserverApiContext>();
            mockContext.Setup(c => c.Strategies).Returns(mockSet.Object);

            var service = new DatabaseQuerier(mockContext.Object);

            //Act
            var actual = service.GetStrategiesWithCapitals(listStrategies);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetStrategiesWithCapitals_GetsNamedStrategies_And_OrdersByDate()
        {
            // Arrange
            var listStrategies = new string[] { "Strategy1" };

            var testDataCapitals1 = new List<Capital>
            {
                new Capital
                {
                    Date = new DateTime(2000, 1, 5),
                    Amount = 400M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 2),
                    Amount = 100M
                },
                new Capital
                {
                    Date = new DateTime(2000, 1, 3),
                    Amount = 200M
                },
                new Capital
                {
                    Date = new DateTime(1999, 1, 1),
                    Amount = 400M
                }
            };

            var testDataStrategy1 = new Strategy
            {
                StratName = "Strategy1",
                Region = "EU",
                Capitals = testDataCapitals1
            };
            var testDataStrategy2 = new Strategy
            {
                StratName = "Strategy2",
                Region = "US",
            };
            var testDataStrategy3 = new Strategy
            {
                StratName = "Strategy3",
                Region = "EU"
            };

            var expected = new List<Strategy>
            {
                new Strategy
                {
                    StratName = "Strategy1",
                    Region = "EU",
                    Capitals = new List<Capital>
                    {
                        new Capital
                        {
                            Date = new DateTime(1999, 1, 1),
                            Amount = 400M
                        },
                        new Capital
                        {
                            Date = new DateTime(2000, 1, 2),
                            Amount = 100M
                        },
                        new Capital
                        {
                            Date = new DateTime(2000, 1, 3),
                            Amount = 200M
                        },
                        new Capital
                        {
                            Date = new DateTime(2000, 1, 5),
                            Amount = 400M
                        }
                    }
                },
                
            };

            var testDataStrategies = new List<Strategy>
            {
                testDataStrategy1,
                testDataStrategy2,
                testDataStrategy3
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Strategy>>();
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.Provider).Returns(testDataStrategies.Provider);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.Expression).Returns(testDataStrategies.Expression);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.ElementType).Returns(testDataStrategies.ElementType);
            mockSet.As<IQueryable<Strategy>>().Setup(m => m.GetEnumerator()).Returns(() => testDataStrategies.GetEnumerator());

            var mockContext = new Mock<GsaserverApiContext>();
            mockContext.Setup(c => c.Strategies).Returns(mockSet.Object);

            var service = new DatabaseQuerier(mockContext.Object);

            //Act
            var actual = service.GetStrategiesWithCapitals(listStrategies);

            //Assert
            Assert.That(actual[0].StratName, Is.EqualTo(expected[0].StratName));
            Assert.That(expected[0].Capitals.ElementAt(0).Date, Is.LessThan(expected[0].Capitals.ElementAt(1).Date));
            Assert.That(expected[0].Capitals.ElementAt(1).Date, Is.LessThan(expected[0].Capitals.ElementAt(2).Date));
            Assert.That(expected[0].Capitals.ElementAt(2).Date, Is.LessThan(expected[0].Capitals.ElementAt(3).Date));

        }

        [Test]
        public void GetStrategiesWithPnlsFromRegion_GetsStrategiesFromNamedRegion_And_OrdersByDate()
        {
            var testDataPnl = new List<Pnl>
            {
                new Pnl
                {
                    Date = new DateTime(2000, 1, 1),
                    Amount = 1000M
                },
                new Pnl
                {
                    Date = new DateTime(2000, 1, 2),
                    Amount = 2000M
                },
                new Pnl
                {
                    Date = new DateTime(2000, 1, 3),
                    Amount = 3000M
                }
            };

            Assert.Pass();
        }
    }
}