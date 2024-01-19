# Vending Machine Project
Do poprawnego działania programu konieczne jest utworzenie bazy danych, 
a także określenie ścieżki do bazy danych na urządzeniu w zmiennej connectionString. 
Zmienna ta znajduje się w klasie VendingMachine.cs w 7 wierszu. 
Należy również utworzyć dwie tabele dla bazy danych. Kod do ich utworzenia znajduje się poniżej.

Tabela Drinks:

CREATE TABLE Drinks
(
	Id INT PRIMARY KEY,
	Name NVARCHAR(100) UNIQUE NOT NULL,
	Price DECIMAL(10, 2) NOT NULL
);

Tabela Transactions:

CREATE TABLE Transactions
(
	Id INT PRIMARY KEY IDENTITY,
	DrinkId INT NOT NULL,
	TotalSum DECIMAL(10, 2) NOT NULL,
	PurchaseTime DATETIME NOT NULL,
);