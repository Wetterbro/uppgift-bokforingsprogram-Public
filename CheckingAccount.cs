namespace bokföringsprogram
{
    public class CheckingAccount : Account
    {
        public override void AddTransaction(Transaction transaction)
        {
            // Logik för att lägga till en transaktion i ett checkkonto
            Transactions.Add(transaction);
        }
    }

}
