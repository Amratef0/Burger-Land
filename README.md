# 🍔 Burger Land

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-8-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB%20%2F%20Express-CC2927?logo=microsoftsqlserver&logoColor=white)
![Identity](https://img.shields.io/badge/ASP.NET%20Core-Identity-512BD4)
![License](https://img.shields.io/badge/License-MIT-green)

A full-featured restaurant web application built with **ASP.NET Core MVC (.NET 8)**. Customers browse the menu by category, add items to their cart, place orders, and book tables, all behind a secure authentication system powered by **ASP.NET Core Identity**. Admins manage the menu, orders, and reservations from role-protected pages.

The project follows the **Repository & Service patterns** with clearly defined interfaces and Dependency Injection for a clean, scalable architecture.

---

## 📌 Table of Contents

- [Tech Stack](#-tech-stack)
- [Features](#-features)
- [Architecture](#️-architecture)
- [Project Structure](#-project-structure)
- [Getting Started](#️-getting-started)
- [Contributing](#-contributing)

---

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/) (.NET 8) | Web framework |
| [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) v8 (Code First) | ORM & database access |
| [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB / Express) | Relational database |
| [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) | Authentication & user management |
| C# / Razor Views | Backend logic & templating |
| HTML, SCSS, JavaScript | Frontend UI & styling |

---

## 🎯 Features

### 🍟 Customers
- Browse menu items by category
- Session-based shopping cart
- Place food orders
- Make table reservations
- Register and log in with ASP.NET Core Identity

### 🛠️ Admin
- Manage menu products (create, edit, delete)
- Manage categories
- View and manage customer orders
- View and manage table reservations
- Role-based access control (Admin/User)

---

## 🏗️ Architecture

The project uses the **Repository & Service pattern** to decouple business logic from data access. Dependencies are registered through ASP.NET Core's built-in dependency injection in `Program.cs`.

```
Controller → Interface → Repository / Service → DbContext (EF Core) → SQL Server
```

**Registered services**

| Interface | Implementation |
|---|---|
| `IProductRepository` | `ProductRepository` |
| `ICategoryRepository` | `CategoryRepository` |
| `IReservationRepository` | `ReservationRepository` |
| `ICartService` | `CartService` |
| `IOrderService` | `OrderService` |

---

## 📁 Project Structure

```
Burger-Land/
├── Controllers/              # MVC controllers (Menu, Cart, Orders, Reservation, ...)
├── Models/                   # Entity models (Product, Order, Reservation, Category, ApplicationUser, ...)
├── Views/                    # Razor view templates
├── Interfaces/               # Repository & service interfaces
│   ├── IProductRepository
│   ├── ICategoryRepository
│   ├── IReservationRepository
│   ├── ICartService
│   └── IOrderService
├── Repositories/             # Concrete repository & service implementations
├── Areas/
│   └── Identity/Pages/       # Scaffolded ASP.NET Identity pages (Login, Register, ...)
├── Migrations/               # EF Core database migrations
├── wwwroot/                  # Static files (CSS/SCSS, JS, images)
├── Properties/               # Launch settings
├── Program.cs                # App entry point & dependency injection
├── appsettings.json          # App configuration & connection strings
└── ReservationSystem.csproj  # Project file & NuGet packages
```

---

## ⚙️ Getting Started

### Prerequisites

- **Visual Studio 2022** or later (or any editor with the .NET CLI)
- **.NET 8 SDK** — [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server LocalDB** or **SQL Server Express**

### 1. Clone and restore

```bash
git clone https://github.com/Amratef0/Burger-Land.git
cd Burger-Land
dotnet restore
```

### 2. Configure the connection string

Update `appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BurgerLandDb;Trusted_Connection=True;"
  }
}
```

### 3. Create the database

In **Package Manager Console** (Visual Studio):

```powershell
Update-Database
```

Or with the .NET CLI:

```bash
dotnet ef database update
```

### 4. Run the app

```bash
dotnet run
```

Or press **F5** in Visual Studio. The app is available at `https://localhost:5001`.

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
