using System;

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
    }
}
