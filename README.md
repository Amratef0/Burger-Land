# 🍔 Burger Land

A full-featured restaurant web application built with **ASP.NET Core MVC (.NET 8)**. Customers can browse the menu by category, add items to their cart, place orders, and make table reservations — all with a secure authentication system powered by ASP.NET Core Identity. The project follows the **Repository Pattern** with clearly defined interfaces for clean and scalable architecture.

---

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/) (.NET 8) | Web framework |
| [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) v8 | ORM & database access |
| [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB / Express) | Relational database |
| [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) | Authentication & user management |
| C# / Razor Views | Backend logic & templating |
| HTML, SCSS, JavaScript | Frontend UI & styling |

---

## 📁 Project Structure

```
Burger-Land/
├── Controllers/              # MVC Controllers (Menu, Cart, Orders, Reservation...)
├── Models/                   # Entity models (Product, Order, Reservation, Category, ApplicationUser...)
├── Views/                    # Razor view templates
├── Interfaces/               # Repository & service interfaces
│   ├── IProductRepository
│   ├── ICategoryRepository
│   ├── IReservationRepository
│   ├── ICartService
│   └── IOrderService
├── Repositories/             # Concrete repository & service implementations
├── Areas/
│   └── Identity/Pages/       # ASP.NET Identity scaffolded pages (Login, Register...)
├── Migrations/               # EF Core database migrations
├── wwwroot/                  # Static files (CSS/SCSS, JS, images)
├── Properties/               # Launch settings
├── Program.cs                # App entry point & dependency injection
├── appsettings.json          # App configuration & connection strings
└── ReservationSystem.csproj  # Project file & NuGet packages
```

---

## ⚙️ Prerequisites

- **Visual Studio 2022** or later
- **.NET 8 SDK** — [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server LocalDB** or **SQL Server Express**

---

## 🛠️ Installation

```bash
# 1. Clone the repository
git clone https://github.com/Amratef0/Burger-Land.git
cd Burger-Land
```

Open the solution in **Visual Studio 2022**, then restore NuGet packages automatically on build, or run:

```bash
dotnet restore
```

---

## 🔧 Configuration

Update `appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BurgerLandDb;Trusted_Connection=True;"
  }
}
```

---

## 🗄️ Database Setup

Run migrations to create the database. In **Package Manager Console** (Visual Studio):

```powershell
Update-Database
```

Or via the .NET CLI:

```bash
dotnet ef database update
```

---

## ▶️ Running the App

```bash
dotnet run
```

Or press **F5** in Visual Studio. The app will be available at `https://localhost:5001`.

---

## 🎯 Features

**Customers**
- Browse menu items by category
- Add products to shopping cart (session-based)
- Place food orders
- Make table reservations
- Register and login with ASP.NET Core Identity

**Admin**
- Manage menu products (Create, Edit, Delete)
- Manage categories
- View and manage customer orders
- View and manage reservations
- Role-based access control

---

## 🏗️ Architecture

The project uses the **Repository & Service Pattern** to decouple business logic from data access:

```
Controller → Interface → Repository / Service → DbContext (EF Core) → SQL Server
```

**Registered services:**

| Interface | Implementation |
|---|---|
| `IProductRepository` | `ProductRepository` |
| `ICategoryRepository` | `CategoryRepository` |
| `IReservationRepository` | `ReservationRepository` |
| `ICartService` | `CartService` |
| `IOrderService` | `OrderService` |

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature`
3. Commit your changes with clear messages
4. Submit a pull request

---

## 📜 License

This project is open source under the **MIT License**.

---

## 👤 Author

**Amr Atef** — [@Amratef0](https://github.com/Amratef0)
