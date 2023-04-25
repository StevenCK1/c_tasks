using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;

namespace TestSuite
{
    public class TransferMoneyTests
    {
        private TransferMoney _transferMoney;
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<INotificationService> _mockNotificationService;
       // private BalanceMethods _balanceMethods;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockNotificationService = new Mock<INotificationService>();
            _transferMoney = new TransferMoney(_mockAccountRepository.Object, _mockNotificationService.Object);
        }

        [Test]
        public void Execute_TransfersMoneyBetweenAccounts()
        {
            // Arrange
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            var amount = 100m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                Balance = 1000m
            };
            var toAccount = new Account
            {
                Id = toAccountId,
                Balance = 1000m
            };

            _mockAccountRepository.Setup(x => x.GetAccountById(fromAccountId)).Returns(fromAccount);
            _mockAccountRepository.Setup(x => x.GetAccountById(toAccountId)).Returns(toAccount);

            // Act
            _transferMoney.Execute(fromAccountId, toAccountId, amount);

            // Assert
            _mockAccountRepository.Verify(x => x.Update(fromAccount), Times.Once);
            _mockAccountRepository.Verify(x => x.Update(toAccount), Times.Once);
            Assert.AreEqual(900m, fromAccount.Balance);
            Assert.AreEqual(1100m, toAccount.Balance);
        }

        [Test]

        public void Execute_ThrowsException_WhenInsufficientFunds()
        {
            // Arrange
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            var amount = 1000m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                Balance = 500m
            };
            var toAccount = new Account
            {
                Id = toAccountId,
                Balance = 500m
            };

            _mockAccountRepository.Setup(x => x.GetAccountById(fromAccountId)).Returns(fromAccount);
            _mockAccountRepository.Setup(x => x.GetAccountById(toAccountId)).Returns(toAccount);

            // Act and Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _transferMoney.Execute(fromAccountId, toAccountId, amount));
            Assert.AreEqual("Insufficient funds to make transfer", ex.Message);
        }

        [Test]
        public void Execute_NotifiesUser_WhenLowFunds()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockNotificationService = new Mock<INotificationService>();
            var fromAccount = new Account
            {
                Id = Guid.NewGuid(),
                Balance = 1000m,
                PaidIn = 0m,
                Withdrawn = 0m,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@test.com",
                    Name = "Test User"
                }
            };
            var toAccount = new Account
            {
                Id = Guid.NewGuid(),
                Balance = 0m,
                PaidIn = 0m,
                Withdrawn = 0m,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test2@test.com",
                    Name = "Test User 2"
                }
            };
            var amount = 501m;

            mockAccountRepository.Setup(r => r.GetAccountById(fromAccount.Id)).Returns(fromAccount);
            mockAccountRepository.Setup(r => r.GetAccountById(toAccount.Id)).Returns(toAccount);

            var transferMoney = new TransferMoney(mockAccountRepository.Object, mockNotificationService.Object);

            // Act
            transferMoney.Execute(fromAccount.Id, toAccount.Id, amount);

            // Assert
            mockNotificationService.Verify(s => s.NotifyFundsLow(fromAccount.User.Email), Times.Once);
        }
    }
}