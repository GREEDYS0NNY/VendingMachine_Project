using System.Collections.Generic;

namespace VendingMachine
{
    internal class Administrator : User
    {
        private readonly VendingMachine vendingMachine;
        private readonly string PASSWORD = "1234"; 
        public Administrator(VendingMachine machine) : base(machine)
        {
            vendingMachine = machine;
        }
        
        private void AddDrink()
        {
            List<Drink> availableDrinks = vendingMachine.GetAvailableDrinks();
            List<string> drinksNames = [];
            foreach (Drink drink in availableDrinks) { drinksNames.Add(drink.Name); }

            Console.Write("Enter the name of the drink: ");

            string? name = Console.ReadLine();

            if (name == "" || name is null || drinksNames.Contains(name))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Name must be defined and must be unique. Canceling the operation...");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write("Enter the price of the drink: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.Write("Enter drink's Id: ");

                    _ = int.TryParse(Console.ReadLine(), out int drinkId);

                    Drink existingDrink = vendingMachine.GetDrinkById(drinkId);

                    if (existingDrink.Id != drinkId && drinkId >= 1)
                    {
                        Drink newDrink = new() { Id = drinkId, Name = name, Price = price };

                        vendingMachine.AddDrink(newDrink);

                        Console.WriteLine();
                        Console.WriteLine($"{name} has been added to the inventory.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Id must be greater than or equal to 1 and must be unique. Canceling the operation...");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid price. Canceling the operation...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private void RemoveDrink()
        {
            Console.Write("Enter Id of the drink to remove: ");

            if (int.TryParse(Console.ReadLine(), out int drinkId))
            {
                Drink removedDrink = vendingMachine.GetDrinkById(drinkId);

                if (removedDrink.Id >= 1)
                {
                    vendingMachine.RemoveDrink(drinkId);

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{removedDrink.Name} has been removed from the inventory!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid Id. Canceling the operation...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid Id. Canceling the operation...");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ManageInventory()
        {
            bool isAllowed = CheckPassword();

            if (isAllowed)
            {
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Admin menu:");
                Console.WriteLine("1. Add Drink");
                Console.WriteLine("2. Remove Drink");
                Console.WriteLine("3. View Available Drinks");
                Console.WriteLine("4. View Transactions");
                Console.WriteLine("5. Delete All Transactions");
                Console.WriteLine("6. Exit Admin menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                _ = int.TryParse(Console.ReadLine(), out int choice);

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddDrink();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        vendingMachine.ViewAvailableDrinks();
                        Console.WriteLine();
                        RemoveDrink();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        vendingMachine.ViewAvailableDrinks();
                        Console.WriteLine();
                        Console.Write("Enter to main menu ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        ViewTransactions();
                        Console.WriteLine();
                        Console.Write("Enter to main menu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        vendingMachine.DeleteAllTransactions();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("All transactions have been deleted succesfully!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Exiting Admin menu...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("❌ Invalid choice. Exiting Admin menu...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Access denied!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ViewTransactions()
        {
            List<Transaction> transactions = vendingMachine.GetTransactions();

            Console.WriteLine("Transactions:");

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"Drink Id: {transaction.PurchasedDrink.Id}; Name: {transaction.PurchasedDrink.Name}; " +
                $"Price: {transaction.TotalSum} zł; Time: {transaction.PurchaseTime};");
                Console.WriteLine("-------------------------------------------------------------------");
            }
        }

        private bool CheckPassword()
        {
            Console.Write("Give the password: ");
            string? inputPassword = Console.ReadLine();

            Console.WriteLine();

            if (inputPassword != PASSWORD) { return false; }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Access allowed!");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }
}
