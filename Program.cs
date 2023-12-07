namespace bokföringsprogram
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var account = new CheckingAccount();
            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Visa transaktioner
                        ShowTransactions(account);
                        break;
                    case "2":
                        // Lägg till transaktion
                        AddTransaction(account);
                        break;
                    case "3":
                        // Ta bort transaktion
                        RemoveTransaction(account);
                        break;
                    case "4":

                        return; // Avsluta programmet
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }

        public static void ShowMainMenu()
        {
            Console.WriteLine("Välkommen till SimpleAccounting!");
            Console.WriteLine("1. Visa alla transaktioner");
            Console.WriteLine("2. Lägg till en ny transaktion");
            Console.WriteLine("3. Ta bort en transaktion");
            Console.WriteLine("4. Avsluta");
        }

        public static void ShowTransactions(CheckingAccount account)
        {
            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine($"{transaction.Date} - {transaction.Description} - {transaction.Amount}");
            }
        }
        public static void AddTransaction(CheckingAccount account)
        {
            Console.WriteLine("Ange beskrivning:");
            var description = Console.ReadLine();
            Console.WriteLine("Ange belopp:");
            var amount = decimal.Parse(Console.ReadLine());
            var transaction = new Transaction { Date = DateTime.Now, Description = description, Amount = amount };
            account.AddTransaction(transaction);
            account.Save();
        }
        public static void RemoveTransaction(CheckingAccount account)
        {
            Console.WriteLine("Ange transaktionsindex att ta bort:");
            var index = int.Parse(Console.ReadLine());
            if (index >= 0 && index < account.Transactions.Count)
            {
                account.Transactions.RemoveAt(index);
                account.Save();
            }
            else
            {
                Console.WriteLine("Ogiltigt index.");
            }
        }
    }
}


