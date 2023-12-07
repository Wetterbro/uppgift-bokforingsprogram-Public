namespace bokföringsprogram
{
    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; }
        public override void AddTransaction(Transaction transaction)
        {
            // Logik för att lägga till en transaktion i ett sparkonto
            Transactions.Add(transaction);
        }
    }

}
