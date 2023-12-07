using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bokföringsprogram.Tests
{
    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void AddTransaction_ShouldAddTransactionToAccount()
        {
            // Arrange
            var account = new CheckingAccount();
            var transaction = new Transaction { Date = DateTime.Now, Description = "Test", Amount = 100m };
            // Act
            account.AddTransaction(transaction);
            // Assert
            Assert.That(account.Transactions.Count, Is.EqualTo(1));
            Assert.That(account.Transactions[0], Is.EqualTo(transaction));
        }
    }
    [TestFixture]
    public class FileManagerTests
    {
        [Test]
        public void LoadTransactions_ShouldLoadTransactionsFromFile()
        {
            // Arrange
            var fileManager = new FileManager();
            // Act
            var transactions = fileManager.LoadTransactions();
            // Assert
            Assert.That(transactions, Is.Not.Null);
        }
        [Test]
        public void SaveTransactions_ShouldSaveTransactionsToFile()
        {
            // Arrange
            var fileManager = new FileManager();
            var transactions = new List<Transaction>
            {
                new Transaction { Date = DateTime.Now, Description = "Test", Amount = 100m }
            };
            // Act
            fileManager.SaveTransactions(transactions);
            var loadedTransactions = fileManager.LoadTransactions();
            // Assert
            Assert.That(1, Is.EqualTo(loadedTransactions.Count));
            Assert.That("Test", Is.EqualTo(loadedTransactions[0].Description));
        }
    }
}
