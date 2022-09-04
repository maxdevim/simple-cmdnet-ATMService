using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMService.Model
{
    public enum TransactionType
    {
        Withdrawal,
        Deposit,
        Transfer,
        Receive
    }
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }


        public Transaction(BankAccount bankAccount, double amount)
        {
            if(amount < 0)
            {
                Type = TransactionType.Transfer;
            }
            else
            {
                Type = TransactionType.Receive;
            }

            Recipient = bankAccount.Owner; 
            Amount = amount;
        }

        public Transaction(double amount)
        {
            if (amount < 0)
            {
                Type = TransactionType.Withdrawal;
            }
            else
            {
                Type = TransactionType.Deposit;
            }

            Recipient = "-";
            Amount = amount;
        }
    }
}
