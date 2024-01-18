
namespace VendingMachine
{
    internal class CustomWarnings
    {
        public static void NameWarning() 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Name must be defined and must be unique. Canceling the operation...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void IdWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Id must be greater than or equal to 1 and must be unique. Canceling the operation...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PriceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Invalid price. Canceling the operation...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AdminMenuChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n❌ Invalid choice. Exiting Admin menu...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MainMenuChoiceWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Invalid choice. Try again.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AccessWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAccess denied!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
