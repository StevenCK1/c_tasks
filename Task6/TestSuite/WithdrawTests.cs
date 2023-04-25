using System;
using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;

namespace TestSuite
{
    public class WithdrawTests
    {
        private WithdrawMoney _withdrawMoney;
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<INotificationService> _mockNotificationService;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockNotificationService = new Mock<INotificationService>();
            _withdrawMoney = new WithdrawMoney(_mockAccountRepository.Object, _mockNotificationService.Object);
        }

        [Test]
        public void Execute_WithdrawMoneyFromAccounts()
        {
            // Arrange
            var AccountId = Guid.NewGuid();
            var amount = 100m;

            var Account = new Account
            {
                Id = AccountId,
                Balance = 1000m
            };
           

            _mockAccountRepository.Setup(x => x.GetAccountById(AccountId)).Returns(Account);

            // Act
            _withdrawMoney.Execute(AccountId, amount);

            // Assert
            _mockAccountRepository.Verify(x => x.Update(Account), Times.Once);
            Assert.AreEqual(900m, Account.Balance);
        }


        [Test]

        public void Execute_ThrowsException_WhenInsufficientFunds()
        {
            // Arrange
            var fromAccountId = Guid.NewGuid();
            var amount = 1000m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                Balance = 500m
            };

            _mockAccountRepository.Setup(x => x.GetAccountById(fromAccountId)).Returns(fromAccount);

            // Act and Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _withdrawMoney.Execute(fromAccountId, amount));
            Assert.AreEqual("Insufficient funds to make withdrawal", ex.Message);
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
            
            var amount = 501m;

            mockAccountRepository.Setup(r => r.GetAccountById(fromAccount.Id)).Returns(fromAccount);

            var withdrawMoney = new WithdrawMoney(mockAccountRepository.Object, mockNotificationService.Object);

            // Act
            withdrawMoney.Execute(fromAccount.Id, amount);

            // Assert
            mockNotificationService.Verify(s => s.NotifyFundsLow(fromAccount.User.Email), Times.Once);
        }
    }
}
