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
                Console.WriteLine("Main menu:");
                Console.WriteLine("1. Buy a drink");
                Console.WriteLine("2. Admin menu");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Select operation: ");

                _ = int.TryParse(Console.ReadLine(), out int choice);

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Console.Clear();

                        vendingMachine.ViewAvailableDrinks();

                        Console.WriteLine();

                        Console.Write("Choose a drink: ");
                        _ = int.TryParse(Console.ReadLine(), out int drinkId);

                        decimal drinkPrice = user.SelectDrink(drinkId);
                        decimal totalSum = 0;

                        while (totalSum < drinkPrice)
                        {
                            Console.WriteLine();

                            Console.Write("Insert coin: ");
                            _ = int.TryParse(Console.ReadLine(), out int coin);

                            Console.WriteLine();

                            user.InsertCoin(coin);
                            totalSum += coin;
                            Console.Write($" Total amount: {totalSum} zł.");

                            if (totalSum > drinkPrice)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine($"Take your change: {totalSum - drinkPrice} zł.");
                                break;
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thanks for the purchase!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();

                        administrator.ManageInventory();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Succesfully exited!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid choice. Try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}