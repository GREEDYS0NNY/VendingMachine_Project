
namespace VendingMachine
{
    internal class CustomWarnings
    {
        public static void NameWarning() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Nazwa musi być określona i musi mieć unikalną wartość. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void IdWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Id musi być większy lub równy 1 i musi mieć unikalną wartość. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PriceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Nieprawidłowo podana cena. Cena musi być liczbą całkowitą lub ułamkową z dwiema cyframi po przecinku. Anulowanie operacji...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AdminMenuChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Nieprawidłowy numer operacji. Wyjście z menu administracyjnego...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MainMenuChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Nieprawidłowy numer operacji. Spróbuj ponownie.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AccessWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDostęp zabroniony!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CoinTypeWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNieprawidłowa kwota monety. Kwota musi być liczbą całkowitą, ułamkową lub dziesiętną. Wprowadź prawidłową monetę.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrinkChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNieprawidłowy wybór napoju. Wybierz prawidłowy napój.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
