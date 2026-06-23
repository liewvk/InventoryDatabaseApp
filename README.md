# InventoryDatabaseApp

**InventoryDatabaseApp** is a beginner-friendly C# Windows Forms application built with Visual Studio 2026. The app demonstrates how to create a simple inventory management system using C#, Windows Forms, SQL Server LocalDB, and `Microsoft.Data.SqlClient`.

The application allows users to add, update, delete, search, and display product records stored in a local SQL Server database.

---

## Features

* Add new product records
* Update existing product details
* Delete selected products
* Search products by name or category
* Display product records in a DataGridView
* Calculate stock value using `UnitPrice × Quantity`
* Validate product name, category, unit price, and quantity
* Connect to SQL Server LocalDB using C#
* Beginner-friendly Windows Forms layout

---

## Technologies Used

* C#
* .NET 10
* Windows Forms
* Visual Studio 2026
* SQL Server LocalDB
* Microsoft.Data.SqlClient
* DataGridView
* SQL CRUD operations

---

## Project Structure

```text
InventoryDatabaseApp
│
├── Form1.cs
├── Form1.Designer.cs
├── Program.cs
├── InventoryDatabaseApp.csproj
└── README.md
```

---

## Database Used

This project uses a SQL Server LocalDB database named:

```text
InventoryDB
```

The main table is:

```text
Products
```

---

## Products Table Script

Create the `Products` table using this SQL script:

```sql
CREATE TABLE Products
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL
);
```

---

## Sample Data

You may insert sample product records using this SQL script:

```sql
INSERT INTO Products (ProductName, Category, UnitPrice, Quantity)
VALUES
('Laptop', 'Electronics', 3500.00, 5),
('Mouse', 'Accessories', 45.00, 20),
('Keyboard', 'Accessories', 120.00, 10),
('Monitor', 'Electronics', 650.00, 8);
```

To view all records:

```sql
SELECT * FROM Products;
```

To view products with calculated stock value:

```sql
SELECT
    ProductId,
    ProductName,
    Category,
    UnitPrice,
    Quantity,
    (UnitPrice * Quantity) AS StockValue
FROM Products;
```

---

## Connection String

The application connects to SQL Server LocalDB using this connection string:

```csharp
private string connectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryDB;Integrated Security=True;TrustServerCertificate=True;";
```

Make sure the database name in your LocalDB instance is exactly:

```text
InventoryDB
```

---

## Required NuGet Package

Install the following NuGet package:

```text
Microsoft.Data.SqlClient
```

In Visual Studio:

1. Right-click the project.
2. Select **Manage NuGet Packages**.
3. Go to **Browse**.
4. Search for `Microsoft.Data.SqlClient`.
5. Click **Install**.

Then add this line at the top of `Form1.cs`:

```csharp
using Microsoft.Data.SqlClient;
```

Also include:

```csharp
using System.Data;
```

for `DataTable`.

---

## How to Run the Project

1. Open the project in **Visual Studio 2026**.
2. Make sure SQL Server LocalDB is installed.
3. Open **SQL Server Object Explorer**.
4. Connect to:

```text
(localdb)\MSSQLLocalDB
```

5. Create the database:

```text
InventoryDB
```

6. Create the `Products` table using the SQL script above.
7. Install the `Microsoft.Data.SqlClient` NuGet package.
8. Build the solution.
9. Run the application.
10. Add, update, delete, search, and refresh product records.

---

## Main Form Controls

The form uses the following controls:

| Control      | Name             | Purpose                 |
| ------------ | ---------------- | ----------------------- |
| TextBox      | `txtProductName` | Product name            |
| TextBox      | `txtCategory`    | Product category        |
| TextBox      | `txtUnitPrice`   | Unit price              |
| TextBox      | `txtQuantity`    | Product quantity        |
| TextBox      | `txtSearch`      | Search text             |
| Button       | `btnAdd`         | Add product             |
| Button       | `btnUpdate`      | Update product          |
| Button       | `btnDelete`      | Delete product          |
| Button       | `btnSearch`      | Search products         |
| Button       | `btnRefresh`     | Refresh product list    |
| Button       | `btnClear`       | Clear input fields      |
| DataGridView | `dgvProducts`    | Display product records |

---

## Validation Rules

The app checks that:

* Product name is not empty
* Category is not empty
* Unit price is a valid number
* Unit price is greater than 0
* Quantity is a valid whole number
* Quantity is 0 or greater

If the input is invalid, the app displays:

```text
Please enter valid product details.
```

---

## Important C# Syntax Note

Some project types may use C# 7.3 by default. In that case, the newer C# 8 `using` declaration syntax may not work.

Avoid this syntax if your project shows an error:

```csharp
using SqlConnection connection = new SqlConnection(connectionString);
```

Use this older block-style syntax instead:

```csharp
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    // Database code here
}
```

This style works in more project settings and is easier for beginners to understand.

---

## Troubleshooting

### 1. `Microsoft.Data.SqlClient` is not recognized

Install the NuGet package:

```text
Microsoft.Data.SqlClient
```

Then rebuild the solution.

---

### 2. `System.Data` appears more than once

If Visual Studio shows:

```text
The using directive for 'System.Data' appeared previously
```

delete the duplicate line. Keep only one:

```csharp
using System.Data;
```

---

### 3. TextBox values appear empty

Check that `InitializeComponent();` is called only once in the constructor.

Correct:

```csharp
public Form1()
{
    InitializeComponent();
    LoadProducts();
}
```

Incorrect:

```csharp
public Form1()
{
    InitializeComponent();
    InitializeComponent();
    LoadProducts();
}
```

Calling `InitializeComponent()` twice can reset or recreate controls and cause confusing behavior.

---

### 4. Database connection fails

Check that:

* LocalDB is installed
* `InventoryDB` exists
* `Products` table exists
* The connection string uses the correct database name
* `Microsoft.Data.SqlClient` is installed

---

### 5. Product records do not appear

Check the `LoadProducts()` SQL query:

```sql
SELECT ProductId, ProductName, Category, UnitPrice, Quantity,
(UnitPrice * Quantity) AS StockValue
FROM Products;
```

Also make sure `LoadProducts();` is called after adding, updating, deleting, or refreshing records.

---

## Learning Purpose

This project is designed for students and beginners learning:

* C# Windows Forms programming
* SQL Server LocalDB
* Database connection strings
* SQL `INSERT`, `SELECT`, `UPDATE`, and `DELETE`
* DataGridView binding
* Input validation
* Practical Visual Studio development workflow

---

## Author

Created as part of a Visual Studio 2026 C# hands-on learning project.

Author: **Dr.Liew Voon Kiong**

---

## License

This project is for educational and learning purposes. You may modify and extend it for your own practice projects.
