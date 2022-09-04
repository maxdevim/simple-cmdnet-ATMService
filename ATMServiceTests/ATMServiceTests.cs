using ATMService;
using ATMService.Model;

namespace ATMServiceTests
{
    [TestClass]
    public class ATMServiceTests
    {
        [TestMethod]
        public void UpdateBalance()
        {
            BankAccount TestAccount = new BankAccount("112233", "me");
            double startBalance = 123.12;
            double withdrawAmount = 79.12;
            double expectedBalance = 44;

            TestAccount.Balance = startBalance;
            TestAccount.Withdraw(withdrawAmount);

            double endBalance = TestAccount.Balance;

            Assert.AreEqual(endBalance, expectedBalance, 0.001, "Balance incorrect");
        }
    }
}