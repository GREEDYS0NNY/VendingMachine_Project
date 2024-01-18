namespace VendingMachine
{
    internal class User
    {
        private readonly VendingMachine vendingMachine;

        public User(VendingMachine machine) { vendingMachine = machine; }

        public void InsertCoin(decimal coin)
        {
            if (coin >= 0.00m) { Console.Write($"\nInserted {coin} zł."); }
            else { Console.WriteLine("\nInvalid coin amount. Please insert a valid coin."); }
        }

        public decimal SelectDrink(int drinkId)
        {
            Drink selectedDrink = vendingMachine.GetDrinkById(drinkId);

            if (selectedDrink != null)
            {
                Transaction transaction = new()
                {
                    PurchasedDrink = selectedDrink,
                    TotalSum = selectedDrink.Price,
                    PurchaseTime = DateTime.Now
                };
                vendingMachine.AddTransaction(transaction);

                Console.WriteLine($"\nWaiting for the payment: {selectedDrink.Price} zł");
                return selectedDrink.Price;
            }
            else { Console.WriteLine("\nInvalid drink selection. Please choose a valid drink."); }
            return 0;
        }
    }
}
