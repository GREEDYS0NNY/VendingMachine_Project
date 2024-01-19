﻿using System.Collections.Generic;

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

            Console.Write("Wprowadź nazwę napoju: ");

            string? name = Console.ReadLine();

            if (name == "" || name is null || drinksNames.Contains(name)) { CustomWarnings.NameWarning(); }
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
                        Drink newDrink = new() { Id = drinkId, Name = name, Price = price };

                        vendingMachine.AddDrink(newDrink);

                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine($"\nNapój {name} został dodany do asortymentu.");
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
            Console.Write("\nWprowadź Id napoju, który chcesz usunąć: ");

            if (int.TryParse(Console.ReadLine(), out int drinkId))
            {
                Drink removedDrink = vendingMachine.GetDrinkById(drinkId);

                if (removedDrink.Id != 0)
                {
                    vendingMachine.RemoveDrink(drinkId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nNapój {removedDrink.Name} został usunięty z asortymentu!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { CustomWarnings.IdWarning(); }
            }
            else { CustomWarnings.IdWarning(); }
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
                                Drink newDrink = new() { Id = newId, Name = newName, Price = newPrice };
                                vendingMachine.UpdateDrinkInfo(updatedDrinkId, newDrink);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nInformacje o napoju zostały pomyślnie zaktualizowane!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else { CustomWarnings.IdWarning(); }
                        }
                        else { CustomWarnings.PriceWarning(); }
                    }
                    else { CustomWarnings.NameWarning(); }
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

                Console.WriteLine("Menu administracyjne:");
                Console.WriteLine("1. Dodać napój");
                Console.WriteLine("2. Usunąć napój");
                Console.WriteLine("3. Aktualizacja informacji o napoju");
                Console.WriteLine("4. Wyświetlić listę dostępnych napojów");
                Console.WriteLine("5. Wyświetlić listę transakcji");
                Console.WriteLine("6. Usunięcie wszystkich transakcji");
                Console.WriteLine("7. Wyjdź z menu administracyjnego");
                Console.Write("\nWybierz operację: ");

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
                        Console.Write("\nNaciśnij Enter, aby wyjść do menu głównego");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        ViewTransactions();
                        Console.Write("\nNaciśnij Enter, aby wyjść do menu głównego");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 6:
                        Console.Clear();
                        vendingMachine.DeleteAllTransactions();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Wszystkie transakcje zostały pomyślnie usunięte!");
                        Console.Write("\nWyjście z menu administracyjnego...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nWyjście z menu administracyjnego...");
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
                Console.ReadKey();
                Console.Clear();
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
                CustomWarnings.AccessWarning();
                return false; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDostęp przyznany!");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }
}
