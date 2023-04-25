using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        private BalanceMethods _balanceMethods;


        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this._balanceMethods = new BalanceMethods();

        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);

            var fromBalance = from.Balance - amount;

            _balanceMethods.InsufficientFunds(fromBalance, "withdrawal");

            _balanceMethods.NotifyWhenFundsUnder500(from, fromBalance, notificationService);

            _balanceMethods.updateBalancesOfAccount(null, from, null, amount, accountRepository);
        }
    }
}
