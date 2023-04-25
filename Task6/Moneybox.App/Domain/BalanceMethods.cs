using System;
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
    }
}
