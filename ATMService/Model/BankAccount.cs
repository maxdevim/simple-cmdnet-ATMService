using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMService.Model
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public double Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

        public BankAccount(string accountNumber, string owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0;
            Transactions = new List<Transaction>();
        }

        public void Withdraw(double amount)
        {
            if(amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount. Try again.");
                Console.ResetColor();
                return;
            }
            else
            {
                Balance -= amount;
                Transactions.Add(new Transaction(-amount));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} has been withdrawn from your account", amount);
                Console.WriteLine("Your new balance amount: {0}", Balance);
                Console.ResetColor();
                return;
            }
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount. Try again.");
                Console.ResetColor();
                return;
            }
            else
            {
                Balance += amount;
                Transactions.Add(new Transaction(amount));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} has been deposited to your account", amount);
                Console.WriteLine("Your new balance amount: {0}", Balance);
                Console.ResetColor();
                return;
            }
        }

        public void Transfer(BankAccount bankAccount, double amount)
        {
            if(amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount. Try again");
                Console.ResetColor();
                return;
            }
            else
            {
                Balance -= amount;
                bankAccount.Balance += amount;
                Transactions.Add(new Transaction(bankAccount, -amount));
                bankAccount.Transactions.Add(new Transaction(this ,+amount));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} has been transfered to {1}", amount, bankAccount.Owner);
                Console.WriteLine("Your new balance amount: {0}", Balance);
                Console.ResetColor();
            }
        }

        public void PrintStatement()
        {
            Console.WriteLine("Statement of {0}, owner: {1}", AccountNumber, Owner);
            foreach (var transaction in Transactions)
            {
                Console.ResetColor();
                Console.WriteLine("Transaction: {0}", transaction.Type.ToString());
                Console.WriteLine("Amount: {0}", transaction.Amount.ToString());
                Console.WriteLine("Recipient: {0}\n", transaction.Recipient);
            }
        }
    }
}
