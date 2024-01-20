namespace VendingMachine
{
    internal class SuccessfulCompletionMessages
    {
        public static void SuccessfullImportMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nDane zostały pomyślnie zaimportowane do bazy danych!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessfullExportMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nDane zostały pomyślnie deportowane do pliku!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessfullDrinkAddition()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nNapój został dodany do asortymentu!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessfullDrinkUpdate()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nInformacje o napoju zostały pomyślnie zaktualizowane!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessfullDrinkRemoval()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nNapój został usunięty z asortymentu!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessfullTransactionsRemoval()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Wszystkie transakcje zostały pomyślnie usunięte!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
