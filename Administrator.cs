namespace VendingMachine
{
    internal class Administrator : CustomWarnings
    {
        private readonly VendingMachine vendingMachine;
        private readonly string PASSWORD = "1234";
        public Administrator(VendingMachine machine) { vendingMachine = machine; }
        
        private void ImportData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Przed importowaniem danych cały asortyment napojów zostanie wyczyszczony!");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\nCzy chcesz kontynuować operację? Tak/Nie: ");

            string? choice = Console.ReadLine();

            if (choice is not null)
            {
                switch (choice.ToLower())
                {
                    case "tak":
                        Console.Write("\nUkaż nazwę pliku wraz z rozszerzeniem (np. file.xlsx/xls/csv): ");
                        string? fileName = Console.ReadLine();

                        if (fileName is not null)
                        {
                            try
                            {
                                vendingMachine.ImportData(fileName);
                            }
                            catch (Exception)
                            {
                                FailedImportExportWarning();
                            }
                        }
                        else { NullWarning(); }
                        break;
                    case "nie":
                        Console.WriteLine("\nNaciśnij Enter, aby wyjść do menu administracyjnego.");
                        break;
                    default:
                        ChoiceWarning();
                        break;
                }
            }
            else { NullWarning(); }
        }

        private void ExportData()
        {
            Console.Write("Podaj nazwę pliku (np. file.xlsx/xls/csv): ");

            string? fileName = Console.ReadLine();

            if (fileName is not null)
            {
                try
                {
                    vendingMachine.ExportData(fileName);
                }
                catch (Exception)
                {
                    FailedImportExportWarning();
                }
            }
            else { NullWarning(); }
        }

        private void AddDrink()
        {
            List<Drink> availableDrinks = vendingMachine.GetAvailableDrinks();
            List<string> drinksNames = [];

            foreach (Drink drink in availableDrinks) { drinksNames.Add(drink.Name); }

            Console.Write("Wprowadź typ napoju (Water, Juice, Soda): ");

            string? drinkType = Console.ReadLine();

            Console.Write("Wprowadź nazwę napoju: ");

            string? name = Console.ReadLine();

            if (name == "" || name is null || drinksNames.Contains(name)) { NameWarning(); }
            else
            {
                Console.Write("Wprowadź cenę napoju: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.Write("Wprowadź Id napoju: ");

                    _ = int.TryParse(Console.ReadLine(), out int drinkId);

                    Drink existingDrink = vendingMachine.GetDrinkById(drinkId);

                    if (existingDrink.Id != drinkId && drinkId >= 1)
                    {
                        if (drinkType is not null)
                        {
                            switch (drinkType.ToLower())
                            {
                                case "water":
                                    Water water = new() { Id = drinkId, Name = name, Price = price };
                                    vendingMachine.AddDrink(water);
                                    break;
                                case "juice":
                                    Juice juice = new() { Id = drinkId, Name = name, Price = price };
                                    vendingMachine.AddDrink(juice);
                                    break;
                                case "soda":
                                    Soda soda = new() { Id = drinkId, Name = name, Price = price };
                                    vendingMachine.AddDrink(soda);
                                    break;
                                default:
                                    WrongDrinkType();
                                    break;
                            }
                        }
                        else { NullWarning(); }
                    }
                    else { IdWarning(); }
                }
                else { PriceWarning(); }
            }
        }

        private void RemoveDrink()
        {
            vendingMachine.ViewAvailableDrinks();
            Console.Write("\nWprowadź Id napoju, który chcesz usunąć: ");

            if (int.TryParse(Console.ReadLine(), out int drinkId))
            {
                Drink removedDrink = vendingMachine.GetDrinkById(drinkId);

                if (removedDrink.Id != 0) { vendingMachine.RemoveDrink(drinkId); }
                else { IdWarning(); }
            }
            else { IdWarning(); }
        }

        private void UpdateDrinkInfo()
        {
            vendingMachine.ViewAvailableDrinks();
            Console.Write("\nWprowadź Id napoju, dla którego chcesz zmienić informacje: ");

            if (int.TryParse(Console.ReadLine(), out int updatedDrinkId))
            {
                Drink drink = vendingMachine.GetDrinkById(updatedDrinkId);
                if (drink.Id != 0)
                {
                    Console.WriteLine("\nWprowadzanie nowych informacji: ");
                    Console.Write("\nWprowadź nową nazwę napoju: ");

                    string? newName = Console.ReadLine();

                    if (newName is not null | newName != "") 
                    {
                        Console.Write("Wprowadź nową cenę napoju: ");

                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                        {
                            Console.Write("Wprowadź nowy Id napoju: ");

                            if (int.TryParse(Console.ReadLine(), out int newId))
                            {
                                try
                                {
                                    Drink newDrink = new() { Id = newId, Name = newName, Price = newPrice };
                                    vendingMachine.UpdateDrinkInfo(updatedDrinkId, newDrink);
                                }
                                catch (Exception)
                                {
                                    NameWarning();
                                }
                            }
                            else { IdWarning(); }
                        }
                        else { PriceWarning(); }
                    }
                    else { NameWarning(); }
                }
                else { IdWarning(); }
            }
            else { IdWarning(); }
        }

        public void AccessToAdminMenu()
        {
            bool isAllowed = CheckPassword();

            if (isAllowed) { ManageInventory(); }
            else { Console.ReadKey(); Console.Clear(); }
        }

        private void ManageInventory()
        {
            Console.Clear();

            Console.WriteLine("Menu administracyjne:");
            Console.WriteLine("1. Dodać napój");
            Console.WriteLine("2. Usunąć napój");
            Console.WriteLine("3. Aktualizacja informacji o napoju");
            Console.WriteLine("4. Wyświetlić listę dostępnych napojów");
            Console.WriteLine("5. Wyświetlić listę transakcji");
            Console.WriteLine("6. Usunięcie wszystkich transakcji");
            Console.WriteLine("7. Importowanie danych");
            Console.WriteLine("8. Eksportowanie danych");
            Console.WriteLine("9. Wyjdź z menu administracyjnego");
            Console.Write("\nWybierz operację: ");

            _ = int.TryParse(Console.ReadLine(), out int choice);

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AddDrink();
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 2:
                    Console.Clear();
                    RemoveDrink();
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 3:
                    Console.Clear();
                    UpdateDrinkInfo();
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 4:
                    Console.Clear();
                    vendingMachine.ViewAvailableDrinks();
                    Console.Write("\nNaciśnij Enter, aby wyjść do menu administracyjnego.");
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 5:
                    Console.Clear();
                    ViewTransactions();
                    Console.Write("\nNaciśnij Enter, aby wyjść do menu administracyjnego.");
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 6:
                    Console.Clear();
                    vendingMachine.DeleteAllTransactions();
                    Console.Write("\nNaciśnij Enter, aby wyjść do menu administracyjnego.");
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 7:
                    Console.Clear();
                    ImportData();
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 8:
                    Console.Clear();
                    ExportData();
                    Console.ReadKey();
                    Console.Clear();
                    ManageInventory();
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nWyjście z menu administracyjnego...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    Console.Clear();
                    break;
                default:
                    AdminMenuChoiceWarning();
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }

        private void ViewTransactions()
        {
            List<Transaction> transactions = vendingMachine.GetTransactions();

            Console.WriteLine("Lista transakcji:");

            Console.WriteLine("\n-------------------------------------------------------------------");

            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"Id napoju: {transaction.PurchasedDrink.Id}; Nazwa: {transaction.PurchasedDrink.Name}; " +
                $"Cena: {transaction.TotalSum} zł; Czas: {transaction.PurchaseTime};");
                Console.WriteLine("-------------------------------------------------------------------");
            }
        }

        private bool CheckPassword()
        {
            Console.Write("Wprowadź hasło: ");
            string? inputPassword = Console.ReadLine();

            if (inputPassword != PASSWORD) 
            {
                AccessWarning();
                return false; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nDostęp przyznany!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return true;
            }
        }
    }
}
