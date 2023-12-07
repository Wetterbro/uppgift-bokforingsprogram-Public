using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace bokföringsprogram
{
    internal class FileManager
    {
        private readonly string _dbPath = @"C:\Users\wette\Desktop\skola\C#\bokföringsprogram\bokföringsprogram\files\data.db";

        public List<Transaction> LoadTransactions()
        {
            var transactions = new List<Transaction>();

            // Connection string for SQLite database
            string connectionString = $"Data Source={_dbPath};Version=3;";

            // Create a new SQLite connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Check if the Transactions table exists
                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Transactions'";
                using (SQLiteCommand checkTableCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var tableName = checkTableCommand.ExecuteScalar();
                    if (tableName == null || tableName.ToString() != "Transactions")
                    {
                        // The Transactions table doesn't exist
                        return transactions;
                    }
                }

                // Create a SQLite command to retrieve data
                string query = "SELECT Date, Description, Amount FROM Transactions";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Execute the command
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Read the data and add it to the transactions list
                        while (reader.Read())
                        {
                            var transaction = new Transaction
                            {
                                Date = reader.GetDateTime(0),
                                Description = reader.GetString(1),
                                Amount = reader.GetDecimal(2)
                            };
                            transactions.Add(transaction);
                        }
                    }
                }
                // Close the connection
                connection.Close();
            }
            return transactions;
        }


        public void SaveTransactions(List<Transaction> transactions)
        {
            // Connection string for SQLite database
            string connectionString = $"Data Source={_dbPath};Version=3;";

            // Create a new SQLite connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create the Transactions table if it doesn't exist
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Transactions (Date TEXT, Description TEXT, Amount REAL)";
                using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }
                
                // Clear existing data in the table
                string clearTableQuery = "DELETE FROM Transactions";
                using (SQLiteCommand clearTableCommand = new SQLiteCommand(clearTableQuery, connection))
                {
                    clearTableCommand.ExecuteNonQuery();
                    
                }

                // Create a SQLite command to insert data
                string insertDataQuery = "INSERT INTO Transactions (Date, Description, Amount) VALUES (@Date, @Description, @Amount)";
                using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Date", "");
                    command.Parameters.AddWithValue("@Description", "");
                    command.Parameters.AddWithValue("@Amount", 0.0);

                    // Insert new data into the table
                    foreach (var transaction in transactions)
                    {
                        command.Parameters["@Date"].Value = transaction.Date.ToString("yyyy-MM-dd HH:mm:ss");
                        command.Parameters["@Description"].Value = transaction.Description;
                        command.Parameters["@Amount"].Value = transaction.Amount;
                        command.ExecuteNonQuery();
                    }
                }

                // Close the connection
                connection.Close();
            }
        }

    }
}
