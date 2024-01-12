namespace VendingMachine
{
    internal class Transaction
    {
        public Drink PurchasedDrink { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
