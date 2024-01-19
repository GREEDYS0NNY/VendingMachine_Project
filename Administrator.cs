using System.Collections.Generic;

namespace VendingMachine
{
    internal class Administrator : User
    {
        private readonly VendingMachine vendingMachine;
        private readonly string PASSWORD = "1234";
        public Administrator(VendingMachine machine) : base(machine) { vendingMachine = machine; }
        
        private void AddDrink()
        {
            List<Drink> availableDrinks = vendingMachine.GetAvailableDrinks();
            List<string> drinksNames = [];

            foreach (Drink drink in availableDrinks) { drinksNames.Add(drink.Name); }

            Console.Write("Enter the name of the drink: ");

            string? name = Console.ReadLine();

            if (name == "" || name is null || drinksNames.Contains(name)) { CustomWarnings.NameWarning(); }
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

                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine($"\n{name} has been added to the inventory.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else { CustomWarnings.IdWarning(); }
                }
                else { CustomWarnings.PriceWarning(); }
            }
        }

        private void RemoveDrink()
        {
            vendingMachine.ViewAvailableDrinks();
            Console.Write("\nEnter Id of the drink to remove: ");

            if (int.TryParse(Console.ReadLine(), out int drinkId))
            {
                Drink removedDrink = vendingMachine.GetDrinkById(drinkId);

                if (removedDrink.Id != 0)
                {
                    vendingMachine.RemoveDrink(drinkId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{removedDrink.Name} has been removed from the inventory!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { CustomWarnings.IdWarning(); }
            }
            else { CustomWarnings.IdWarning(); }
        }

        private void UpdateDrinkInfo()
        {
            vendingMachine.ViewAvailableDrinks();
            Console.Write("\nGive Id of the drink to update: ");

            if (int.TryParse(Console.ReadLine(), out int updatedDrinkId))
            {
                Drink drink = vendingMachine.GetDrinkById(updatedDrinkId);
                if (drink.Id != 0)
                {
                    Console.WriteLine("\nGive new info: ");
                    Console.Write("\nEnter the name of the drink: ");

                    string? newName = Console.ReadLine();

                    if (newName == "" | newName is null) { CustomWarnings.NameWarning(); }
                    else
                    {
                        Console.Write("Enter the price of the drink: ");

                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                        {
                            Console.Write("Enter drink's Id: ");

                            if (int.TryParse(Console.ReadLine(), out int newId))
                            {
                                Drink newDrink = new() { Id = newId, Name = newName, Price = newPrice };
                                vendingMachine.UpdateDrinkInfo(updatedDrinkId, newDrink);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDrink's info has been updated succesfully!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else { CustomWarnings.IdWarning(); }
                        }
                        else { CustomWarnings.PriceWarning(); }
                    }
                }
                else { CustomWarnings.IdWarning(); }
            }
            else { CustomWarnings.IdWarning(); }
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
                Console.WriteLine("3. Update Drink Info");
                Console.WriteLine("4. View Available Drinks");
                Console.WriteLine("5. View Transactions");
                Console.WriteLine("6. Delete All Transactions");
                Console.WriteLine("7. Exit Admin menu");
                Console.Write("\nEnter your choice: ");

                _ = int.TryParse(Console.ReadLine(), out int choice);

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
                        RemoveDrink();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        UpdateDrinkInfo();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        vendingMachine.ViewAvailableDrinks();
                        Console.Write("\nEnter to main menu ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        ViewTransactions();
                        Console.Write("\nEnter to main menu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 6:
                        Console.Clear();
                        vendingMachine.DeleteAllTransactions();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("All transactions have been deleted succesfully!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nExiting Admin menu...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        CustomWarnings.AdminMenuChoiceWarning();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            else
            {
                CustomWarnings.AccessWarning();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ViewTransactions()
        {
            List<Transaction> transactions = vendingMachine.GetTransactions();

            Console.WriteLine("Transactions:");

            Console.WriteLine("\n-------------------------------------------------------------------");

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

            if (inputPassword != PASSWORD) { return false; }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAccess allowed!");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }
}
