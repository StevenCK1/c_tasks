using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        private BalanceMethods _balanceMethods;


        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this._balanceMethods = new BalanceMethods();

        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            var fromBalance = from.Balance - amount;

            _balanceMethods.InsufficientFunds(fromBalance, "transfer");

            _balanceMethods.NotifyWhenFundsUnder500(from, fromBalance, notificationService);

            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
            _balanceMethods.updateBalancesOfAccount("transfer", from, to, amount, accountRepository);
        }
    }
}
