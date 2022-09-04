using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ATMService.Model
{
    public class ATM
    {
        Random rnd = new Random();
        public List<BankAccount> BankAccounts = new List<BankAccount>();
        public List<BankCard> BankCards = new List<BankCard>();
        public BankAccount LoggedBankAccount = null;

        public void Initialization()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ATMService version 1.0.0 by maxdevim ready.\n" +
                "~~~~~~~~~~");
            Console.ResetColor();
            Console.WriteLine("Please insert a valid bank card (by account number) to access desired bank account.\n");
        }

        public string ScanInput()
        {
            Console.WriteLine("(Enter one of the following keys to proceed)\n");
            Console.WriteLine("n. Create a new bank account");
            for (int i = 0; i < BankAccounts.Count; i++)
            {
                Console.WriteLine("{0}. Insert bank card of {1}.", i, BankCards[i].Owner);
            }
            Console.WriteLine("\nEnter any other key to insert a random RFID card.\n");
            return Console.ReadLine();
        }

        public void CreateBankAccount()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are about to create a new bank account.");
            Console.Write("Please enter the name of the owner: ");
            Console.ResetColor();
            var input = Console.ReadLine();
            var randomNumber = rnd.Next(100000, 999999).ToString();

            for (int i = 0; i < BankAccounts.Count; i++)
            {
                if (randomNumber == BankAccounts[i].AccountNumber)
                {
                    randomNumber = rnd.Next(100000, 999999).ToString();
                    i = -1;
                }
            }

            BankAccounts.Add(new BankAccount(randomNumber.ToString(), input));
            BankCards.Add(new BankCard(BankAccounts[BankAccounts.Count - 1]));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNew bank account for {0} with account number {1} has been created.\n" +
                "The corresponding bank card has already been delivered to you and is ready to use.\n", input, randomNumber);
            Console.ResetColor();
        }

        public BankAccount SearchBankAccount(string input)
        {
            var isNumeric = int.TryParse(input, out int number);
            if (isNumeric && number >= 0 && number < BankCards.Count)
            {
                foreach (var account in BankAccounts)
                {
                    if (BankCards[number].AccountNumber == account.AccountNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Matching bank account has been identified.");
                        Console.ResetColor();
                        return account;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unauthorized RFID card has been inserted into the machine. Please try a different bank card.\n");
            Console.ResetColor();
            return null;
        }

        public void BankCardValidation(string number, BankAccount account)
        {
            bool isNumeric = int.TryParse(number, out int intNumber);
            if (BankCards[intNumber].Pin == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome! I can see that you are visiting me for the first time!");
                do
                {
                    Console.Write("Please enter a 5-digit number as your new pin to keep your bank account secure: ");
                    var input = Console.ReadLine();
                    isNumeric = int.TryParse(input, out int pin);

                    if (isNumeric && pin > 9999 && pin <= 99999)
                    {
                        BankCards[intNumber].Pin = input;
                        LoggedBankAccount = account;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSomething is wrong with your provided pin...\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                } while (BankCards[intNumber].Pin == null);
            }
            else
            {
                do
                {
                    Console.Write("Please enter your personal 5-digit pin to gain access to your bank account: ");
                    var input = Console.ReadLine();
                    if (input != BankCards[intNumber].Pin)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: WRONG PIN ENTERED");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("SUCCESS: CORRECT PIN HAS BEEN ENTERED\n\n\n");
                        LoggedBankAccount = account;
                    }
                } while (LoggedBankAccount == null);
            }
            Console.ResetColor();
        }

        public string LoggedIn()
        {
            Console.WriteLine("\n~~~~~~~~~~\nWelcome {0}!\nWhat would you like to do?", LoggedBankAccount.Owner);
            Console.WriteLine("Your current balance amounts: {0}\n", LoggedBankAccount.Balance);
            Console.WriteLine("(Enter one of the following keys to proceed)\n");
            Console.WriteLine("w. Withdraw");
            Console.WriteLine("d. Deposit");
            Console.WriteLine("t. Transfer");
            Console.WriteLine("s. Statement\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("To delete your bank account type 'delete' without special characters.\n");
            Console.ResetColor();
            Console.WriteLine("Any other input will dispense your bank card and log you out of the system.");
            return Console.ReadLine();
        }

        public void Withdraw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are about to WITHDRAW money from your bank account.");
            Console.Write("How much money would you like to WITHDRAW?: ");
            Console.ResetColor();
            var input = Console.ReadLine();
            bool isNumeric = double.TryParse(input, out double number);
            if (!isNumeric)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NOT A VALID NUMBER. PROCESS WILL BE STOPPED");
                Console.ResetColor();
                
            }
            else
            {
                LoggedBankAccount.Withdraw(number);
                
            }
        }

        public void Deposit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are about to DEPOSIT money to your bank account.");
            Console.Write("How much money would you like to DEPOSIT?: ");
            Console.ResetColor();
            var input = Console.ReadLine();
            bool isNumeric = double.TryParse(input, out double number);
            if (!isNumeric)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NOT A VALID NUMBER. PROCESS WILL BE STOPPED");
                Console.ResetColor();
            }
            else
            {
                LoggedBankAccount.Deposit(number);
            }
        }

        public void Transfer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are about to TRANSFER money from your bank account.");
            Console.Write("How much money would you like to TRANSFER?: ");
            Console.ResetColor();
            var input = Console.ReadLine();
            bool isNumeric = double.TryParse(input, out double number);
            if (!isNumeric)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NOT A VALID NUMBER. PROCESS WILL BE STOPPED");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter a valid bank account number to transfer the money to: ");
                Console.ResetColor();
                input = Console.ReadLine();
                foreach (var account in BankAccounts)
                {
                    if (account.AccountNumber == input)
                    {
                        LoggedBankAccount.Transfer(account, number);
                        return;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No valid account with that bank account number has been registered.");
                Console.ResetColor();
            }
        }

        public void PrintStatement()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are about to print a STATEMENT.\n");
            Console.ResetColor();

            LoggedBankAccount.PrintStatement();
        }

        public void Logout()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are being logged out of the system.\n\n");
            Console.ResetColor();
            LoggedBankAccount = null;
        }
    }
}
