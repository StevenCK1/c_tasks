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
            _mockAccountRepository.Verify(x => x.Update(AccountId), Times.Once);
            Assert.AreEqual(900m, Account.Balance);
        }
    }
}
