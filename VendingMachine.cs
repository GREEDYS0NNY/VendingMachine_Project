using System.Data.SqlClient;

namespace VendingMachine
{
    internal class VendingMachine
    {
        private readonly string connectionString = @"Data Source=GREED;Initial Catalog=VendingMachine;Integrated Security=True;";

        public void AddDrink(Drink drink)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertQuery = "INSERT INTO Drinks (Id, Name, Price) VALUES (@Id, @Name, @Price)";

            using SqlCommand command = new(insertQuery, connection);
            command.Parameters.AddWithValue("@Id", drink.Id);
            command.Parameters.AddWithValue("@Name", drink.Name);
            command.Parameters.AddWithValue("@Price", drink.Price);

            command.ExecuteNonQuery();
        }

        public void RemoveDrink(int drinkId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteQuery = "DELETE FROM Drinks WHERE Id = @drinkId";

            using SqlCommand command = new(deleteQuery, connection);
            command.Parameters.AddWithValue("@drinkId", drinkId);

            command.ExecuteNonQuery();
        }

        public void AddTransaction(Transaction transaction)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertQuery = "INSERT INTO Transactions (DrinkId, TotalSum, PurchaseTime) VALUES (@DrinkId, @TotalSum, @PurchaseTime)";

            using SqlCommand command = new(insertQuery, connection);
            command.Parameters.AddWithValue("@DrinkId", transaction.PurchasedDrink.Id);
            command.Parameters.AddWithValue("@TotalSum", transaction.TotalSum);
            command.Parameters.AddWithValue("@PurchaseTime", transaction.PurchaseTime);

            command.ExecuteNonQuery();
        }

        public void DeleteAllTransactions()
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteQuery = "TRUNCATE TABLE Transactions";

            using SqlCommand command = new(deleteQuery, connection);
            command.ExecuteNonQuery();
        }

        public void ViewAvailableDrinks()
        {
            List<Drink> availableDrinks = GetAvailableDrinks();

            Console.WriteLine("Available Drinks:");

            for (int i = 0; i < availableDrinks.Count; i++)
            {
                Console.WriteLine($"Id {availableDrinks[i].Id}: {availableDrinks[i].Name} - {availableDrinks[i].Price} zł");
            }
        }

        public List<Drink> GetAvailableDrinks()
        {
            List<Drink> availableDrinks = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Drinks";

                using SqlCommand command = new(selectQuery, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Drink drink = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                    availableDrinks.Add(drink);
                }
            }
            return availableDrinks;
        }

        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Transactions";

                using SqlCommand command = new(selectQuery, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Transaction transaction = new()
                    {
                        PurchasedDrink = GetDrinkById(Convert.ToInt32(reader["DrinkId"])),
                        TotalSum = Convert.ToDecimal(reader["TotalSum"]),
                        PurchaseTime = Convert.ToDateTime(reader["PurchaseTime"])
                    };
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        public Drink GetDrinkById(int drinkId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string selectQuery = "SELECT * FROM Drinks WHERE Id = @drinkId";

            using SqlCommand command = new(selectQuery, connection);
            command.Parameters.AddWithValue("@drinkId", drinkId);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Drink
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }
            return new Drink { Id = 0, Name = "Undefined", Price = 0 };
        }
    }
}
