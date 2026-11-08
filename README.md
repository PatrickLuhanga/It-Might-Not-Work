# RePurpose (It-Might-Not-Work)

An ASP.NET Core MVC web application for a Durban-based e-waste recycling marketplace. RePurpose collects discarded electronics, extracts and refurbishes usable parts, and lists them for sale through an online storefront. The app combines a public product catalogue with simple back-office CRUD screens for products, parts, and accounts.

## Project Overview

- **What it does:** Presents a marketing homepage and a browseable catalogue of repurposed electronic components (RAM, capacitors, Arduino boards, sensors, and similar parts). Staff can register products and their constituent parts, and manage a lightweight account list.
- **Who it is for:** The RePurpose team and its customers, plus students/developers maintaining the sample storefront.
- **Current maturity:** The products catalogue is a working MVC flow backed partly by Entity Framework Core and partly by hard-coded demo products. Accounts are stored in an in-memory static list, not the database.

## Tech Stack

| Layer | Technology |
| --- | --- |
| Language | C# |
| Framework | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core 9.0.5 |
| Database | SQL Server / SQL Server LocalDB |
| Views | Razor (`.cshtml`) with Bootstrap-style layout |
| Tooling | `Microsoft.VisualStudio.Web.CodeGeneration.Design` (scaffolding), EF Core Tools |

Key NuGet packages are declared in `RePurpose.csproj`:

- `Microsoft.EntityFrameworkCore.SqlServer` 9.0.5
- `Microsoft.EntityFrameworkCore.Tools` 9.0.5
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` 8.0.7

## How It Works / Architecture

```
It-Might-Not-Work/
├── Program.cs                 # App startup: DbContext, MVC, middleware, routing
├── RePurpose.csproj           # Target framework + NuGet dependencies
├── RePurpose.sln              # Visual Studio solution
├── appsettings.json           # Connection string + logging
├── Controllers/
│   ├── HomeController.cs      # Home, Privacy, About, Error
│   ├── ProductsController.cs  # Catalogue, product details, product creation
│   ├── PartsController.cs     # Full CRUD for parts
│   └── AccountsController.cs  # CRUD against an in-memory account list
├── Data/
│   └── AppDbContexts.cs       # EF Core DbContext (Products, Parts)
├── Models/
│   ├── Product.cs             # Product entity
│   ├── Part.cs                # Part entity (many-to-one Product)
│   ├── AccountsModel.cs       # Account view model (not persisted)
│   └── ProductBrowseViewModel.cs # Catalogue + card view models
├── Migrations/                # EF Core schema migration (InitialCreate)
├── Views/                     # Razor views for Home/Products/Parts/Accounts
├── Properties/launchSettings.json
└── wwwroot/                   # CSS, JS, images/favicon
```

### Request flow

1. `Program.cs` registers `AppDbContext` with SQL Server and enables MVC with a default route of `{controller=Home}/{action=Index}/{id?}`.
2. `HomeController` serves the landing page (`/`), `/Home/About`, `/Home/Privacy`, and the error page.
3. `/Products` renders the catalogue through `ProductsController.Index`. It queries the `Products` table to count rows, but the cards themselves come from a hard-coded `dummyProducts` list. `/Products/Details/{id}` also resolves against that demo list.
4. `/Products/Create` (POST) accepts a `Product` plus an uploaded image. The image is written to `wwwroot/images` and the product is saved through EF Core.
5. `/Parts` and `/Parts/{Create,Edit,Delete,Details}` are standard EF Core CRUD actions. Each `Part` belongs to one `Product`, configured in `AppDbContext.OnModelCreating`.
6. `/Accounts` is CRUD over a `static List<AccountsModel>` seeded with two demo accounts; changes are lost when the process restarts.

### Database schema

The `InitialCreate` migration creates two tables:

- **Products**: `ProductID` (identity PK), `ProductType`, `Description`, `IsWorking`, `ImagePath`.
- **Parts**: `PartID` (identity PK), `PartType`, `ProductID` (FK → Products, cascade delete), `Condition`, `EstimatedValue`.

## Prerequisites & Environment Variables

### Required tools

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (installed with Visual Studio on Windows) or another SQL Server instance
- `dotnet-ef` CLI tool (only if you need to create/apply migrations)
- Visual Studio 2022, VS Code, or the .NET CLI

### Configuration

The connection string is read from `ConnectionStrings:DefaultConnection` in `appsettings.json`:

```
Server=(localdb)\mssqllocaldb;Database=RePurposeDurbanDB;Trusted_Connection=True;
```

No secrets are committed. To override without editing the file, use standard ASP.NET Core configuration:

| Variable | Purpose | Default in repo |
| --- | --- | --- |
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | `Server=(localdb)\mssqllocaldb;Database=RePurposeDurbanDB;Trusted_Connection=True;` |
| `ASPNETCORE_ENVIRONMENT` | Selects the environment (`Development`, `Production`) | `Development` in `launchSettings.json` |

`appsettings.Development.json` only adjusts logging; it contains no connection string.

> **Undocumented setup note:** the Razor layout links to `Products/Category`, `Products/Search`, and a `Cart` controller, but those actions/controllers are not implemented in this repository. Expect 404s if those links are used.

## Getting Started

All commands are run from the repository root (the folder containing `RePurpose.csproj`).

### 1. Restore dependencies

```powershell
dotnet restore
```

### 2. Create/update the database

Ensure LocalDB is available, then apply the included migration:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef database update
```

If you use a different SQL Server instance, set the connection string first:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=YOUR_SERVER;Database=RePurposeDurbanDB;Trusted_Connection=True;TrustServerCertificate=True;"
dotnet ef database update
```

### 3. Run the app

```powershell
dotnet run
```

The launch profile exposes:

- HTTP: `http://localhost:5124`
- HTTPS: `https://localhost:7299`

Alternatively use the IIS Express profile from Visual Studio (`http://localhost:20199`, SSL `44337`).

### Useful routes

| Route | Description |
| --- | --- |
| `/` | Landing page |
| `/Home/About` | Project background |
| `/Products` | Product catalogue |
| `/Products/Details/{id}` | Demo product detail |
| `/Products/Create` | Add a product (accepts image upload) |
| `/Parts` | Parts list and CRUD |
| `/Accounts` | In-memory account CRUD |

### Build a release

```powershell
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

## Notes and Known Limitations

- The catalogue and product-details pages use hard-coded sample products rather than the database, while the product count is read from the database.
- Account data is not persisted; `AccountsController` uses a static list seeded with example credentials. Do not treat it as an authentication system.
- There is no authentication/authorization middleware wired up beyond `app.UseAuthorization()`.
- Category, search, and cart links exist in the layout but have no corresponding controller actions yet.
