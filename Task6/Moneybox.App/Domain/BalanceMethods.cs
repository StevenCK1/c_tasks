using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Domain
{
    public class BalanceMethods
    {
        public void InsufficientFunds(decimal balance, string BalanceOperation)
        {
            if (balance < 0m)
            {
                throw new InvalidOperationException($"Insufficient funds to make {BalanceOperation}");
            }

        }

        public void NotifyWhenFundsUnder500(Account account, decimal balance, INotificationService notificationService)
        {
            if (balance < 500m)
            {
                notificationService.NotifyFundsLow(account.User.Email);
            }
        }

        public void updateBalancesOfAccount(string type, Account from, Account to, decimal amount, IAccountRepository accountRepository)
        {
            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;
            accountRepository.Update(from);
            
            if (type == "transfer")
            {
                to.Balance = to.Balance + amount;
                to.PaidIn = to.PaidIn + amount;


                accountRepository.Update(to);
            }
        }
    }
}
