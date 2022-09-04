using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMService.Model
{
    public class BankCard
    {
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Pin { get; set; } = null;

        public BankCard(BankAccount bankAccount)
        {
            AccountNumber = bankAccount.AccountNumber;
            Owner = bankAccount.Owner;
            ExpirationDate = DateTime.Now.AddYears(3);
        }
    }
}
