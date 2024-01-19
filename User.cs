namespace VendingMachine
{
    internal class User
    {
        private readonly VendingMachine vendingMachine;

        public User(VendingMachine machine) { vendingMachine = machine; }

        public void InsertCoin(decimal coin)
        {
            if (coin >= 0.00m) { Console.Write($"\nWstawiono {coin} zł."); }
            else { CustomWarnings.CoinTypeWarning(); }
        }
        
        public void BuyDrink()
        {
            Console.Write("\nPodaj Id napoju: ");
            _ = int.TryParse(Console.ReadLine(), out int drinkId);

            Drink selectedDrink = vendingMachine.GetDrinkById(drinkId);

            if (selectedDrink.Id != 0)
            {
                decimal drinkPrice = selectedDrink.Price;
                decimal totalSum = 0;

                Console.WriteLine($"\nOczekiwanie na płatność: {drinkPrice} zł");

                while (totalSum < drinkPrice)
                {
                    Console.Write("\nWstaw monetę: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal coin) & coin > 0)
                    {
                        InsertCoin(coin);
                        totalSum += coin;
                        Console.Write($" Wprowadzona kwota: {totalSum} zł.\n");
                    }
                    else { InsertCoin(-1); }
                }
                if (totalSum - drinkPrice != 0.00m) { Console.WriteLine($"\nWeź swoją resztę: {totalSum - drinkPrice} zł."); }

                Transaction transaction = new()
                {
                    PurchasedDrink = selectedDrink,
                    TotalSum = selectedDrink.Price,
                    PurchaseTime = DateTime.Now
                };
                vendingMachine.AddTransaction(transaction);
            }
            else 
            { 
                CustomWarnings.DrinkChoiceWarning();
                BuyDrink();
            }
        }
    }
}
