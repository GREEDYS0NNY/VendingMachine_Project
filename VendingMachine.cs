﻿using System.Data.SqlClient;
using System.Globalization;
using IronXL;

namespace VendingMachine
{
    internal class VendingMachine : SuccessfulCompletionMessages, IDataAccess
    {
        private readonly string connectionString = @"Data Source=GREED;Initial Catalog=VendingMachine;Integrated Security=True;";

        public void ImportData(string fileName)
        {
            WorkBook workBook = WorkBook.Load($"{Directory.GetCurrentDirectory()}\\{fileName}");
            WorkSheet workSheet = workBook.WorkSheets.First();

            int rowsCount = workSheet.RowCount;

            RemoveAllDrinks();

            for (int i = 0; i < rowsCount; i++)
            {
                using SqlConnection connection = new(connectionString);
                connection.Open();

                string insertQuery = "INSERT INTO Drinks (Id, Name, Price) VALUES (@Id, @Name, @Price)";

                using SqlCommand command = new(insertQuery, connection);

                if (fileName.Contains(".csv"))
                {
                    string[] row = workSheet.GetRow(i).ToString().Split(',');

                    command.Parameters.AddWithValue("@Id", Convert.ToInt32(row[0]));
                    command.Parameters.AddWithValue("@Name", row[1]);
                    command.Parameters.AddWithValue("@Price", Convert.ToDecimal(row[2], CultureInfo.InvariantCulture));

                    command.ExecuteNonQuery();
                }
                else
                {
                    IronXL.Range idRows = workSheet[$"A1:A{rowsCount}"];
                    IronXL.Range nameRows = workSheet[$"B1:B{rowsCount}"];
                    IronXL.Range priceRows = workSheet[$"C1:C{rowsCount}"];

                    command.Parameters.AddWithValue("@Id", idRows.ToArray()[i].GetValue<int>());
                    command.Parameters.AddWithValue("@Name", nameRows.ToArray()[i].GetValue<string>());
                    command.Parameters.AddWithValue("@Price", priceRows.ToArray()[i].GetValue<decimal>());

                    command.ExecuteNonQuery();
                }
            }
            SuccessfullImportMessage();
        }

        public void ExportData(string fileName) 
        {
            List<Drink> drinks = GetAvailableDrinks();

            string pathToFile = $"{Directory.GetCurrentDirectory()}\\{fileName}";

            if (fileName.Contains(".csv"))
            {
                using StreamWriter writer = new(pathToFile);

                foreach (Drink drink in drinks)
                {
                    string formattedPrice = drink.Price.ToString(CultureInfo.InvariantCulture);
                    writer.WriteLine($"{drink.Id},{drink.Name},{formattedPrice}");
                }
            }
            else
            {
                WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);
                WorkSheet workSheet = workBook.DefaultWorkSheet;

                for (int i = 0; i < drinks.Count; i++)
                {
                    workSheet[$"A{i + 1}"].Value = drinks[i].Id;
                    workSheet[$"B{i + 1}"].Value = drinks[i].Name;
                    workSheet[$"C{i + 1}"].Value = drinks[i].Price;
                }
                workBook.SaveAs(pathToFile);
            }
            SuccessfullExportMessage();
        }

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

            SuccessfullDrinkAddition();
        }

        public void UpdateDrinkInfo(int updatedDrinkId, Drink drink)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string updateQuery = "UPDATE Drinks SET Id = @Id, Name = @Name, Price = @Price WHERE Id = @updatedDrinkId";

            using SqlCommand command = new(updateQuery, connection);
            command.Parameters.AddWithValue("@Id", drink.Id);
            command.Parameters.AddWithValue("@Name", drink.Name);
            command.Parameters.AddWithValue("@Price", drink.Price);
            command.Parameters.AddWithValue("@updatedDrinkId", updatedDrinkId);

            command.ExecuteNonQuery();

            SuccessfullDrinkUpdate();
        }

        public void RemoveDrink(int drinkId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteQuery = "DELETE FROM Drinks WHERE Id = @drinkId";

            using SqlCommand command = new(deleteQuery, connection);
            command.Parameters.AddWithValue("@drinkId", drinkId);

            command.ExecuteNonQuery();

            SuccessfullDrinkRemoval();
        }

        public void RemoveAllDrinks()
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteQuery = "TRUNCATE TABLE Drinks";

            using SqlCommand command = new(deleteQuery, connection);
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

            SuccessfullTransactionsRemoval();
        }

        public void ViewAvailableDrinks()
        {
            List<Drink> availableDrinks = GetAvailableDrinks();

            Console.WriteLine("Dostępne napoje:");

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
                        Name = reader["Name"].ToString(), // nigdy nie otrzyma wartości NULL, ponieważ kolumna Name w bazie danych jest oznaczona jako NOT NULL
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
                    Name = reader["Name"].ToString(), // nigdy nie otrzyma wartości NULL, ponieważ kolumna Name w bazie danych jest oznaczona jako NOT NULL
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }
            return new Drink { Id = 0, Name = "Undefined", Price = 0 };
        }
    }
}
