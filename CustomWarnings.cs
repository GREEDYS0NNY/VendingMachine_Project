﻿
namespace VendingMachine
{
    internal class CustomWarnings
    {
        public static void NameWarning() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nazwa musi być określona i musi mieć unikalną wartość. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void IdWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Id musi być większy lub równy 1 i musi mieć unikalną wartość. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PriceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nieprawidłowo podana cena. Cena musi być liczbą całkowitą lub ułamkową z dwiema cyframi po przecinku. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AdminMenuChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nieprawidłowy numer operacji. Wyjście z menu administracyjnego...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("❌ Nieprawidłowy numer operacji. Spróbuj ponownie.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AccessWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Dostęp zabroniony!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CoinTypeWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nieprawidłowa kwota monety. Kwota musi być liczbą całkowitą, ułamkową lub dziesiętną. Wprowadź prawidłową monetę.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrinkChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nieprawidłowy wybór napoju. Wybierz prawidłowy napój.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void FailedImportExportWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Wystąpił błąd podczas importowania/deportowania danych! Sprawdź, czy wprowadziłeś poprawną nazwę pliku.");
            Console.WriteLine($"Lub upewnij się, że dane w pliku są prawidłowe.");
            Console.Write("\nAnulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void NullWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Pole nie może zawierać wartości NULL!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WrongDrinkType()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Niewłaściwy rodzaj napoju! Napój musi być jednym z następujących rodzajów: Water, Juice, Soda.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WrongCoinDenomination()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Nieprawidłowy nominał monety!");
            Console.WriteLine("Moneta musi odpowiadać następującym nominałom: {0,01; 0,02; 0,05; 0,10; 0,20; 0,50; 1; 2; 5} zł.");
            Console.WriteLine("Grosze muszą zawierać dwie cyfry po przecinku.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
