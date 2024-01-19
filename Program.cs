namespace VendingMachine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            VendingMachine vendingMachine = new();
            User user = new(vendingMachine);
            Administrator administrator = new(vendingMachine);

            while (true)
            {
                Console.WriteLine("Menu główne:");
                Console.WriteLine("1. Kupić napój");
                Console.WriteLine("2. Menu administracyjne");
                Console.WriteLine("3. Wyjdź");
                Console.Write("\nWybierz operację: ");

                _ = int.TryParse(Console.ReadLine(), out int choice);

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Console.Clear();

                        vendingMachine.ViewAvailableDrinks();
                        user.BuyDrink();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nDziękujemy za zakup!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        administrator.AccessToAdminMenu();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Udane wyjście!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;
                    default:
                        CustomWarnings.MainMenuChoiceWarning();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}