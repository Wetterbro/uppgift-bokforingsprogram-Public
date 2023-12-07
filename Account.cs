using System.Security.Cryptography.X509Certificates;

namespace bokföringsprogram
{
    public abstract class Account
    {
        private FileManager _fileManager = new FileManager();
        public List<Transaction> Transactions { get; set; }
        public Account()
        {
            Transactions = _fileManager.LoadTransactions();
        }
        public abstract void AddTransaction(Transaction transaction);
        public void Save()
        {
            _fileManager.SaveTransactions(Transactions);
        }

    }
}