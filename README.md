<h1 align="center">BizCon - Warehouse Management System</h1>
<p align="center">
Business-oriented WPF (.NET Framework 4.8) sample app showcasing MVVM with Entity Framework 6: sales reporting, payroll cost summary, and inventory search.
</p>

## Overview
MVVMFirma demonstrates a production-like MVVM structure on WPF with a SQL Server backend. It includes:
- User login, main window navigation
- Sales Report (turnover by product and date range)
- Payroll Cost Summary (department/position/employee filters)
- Inventory Search (product/site details)

## Features
- MVVM-first structure (View, ViewModel, Business Logic layers)
- Entity Framework 6 (Database First) with `BizConDbEntities`
- Modular business logic for lists, calculations, and lookups
- Ready-to-run demo login and simple session service

## Tech Stack
- WPF, .NET Framework 4.8
- Entity Framework 6.5.1
- SQL Server / SQL Express / LocalDB
- Visual Studio 2019/2022

## Architecture (High Level)
- Data context: `BizConDbEntities` (EF6) reads connection from `app.config`.
- Business Logic: `Models/BusinessLogic` (e.g., `RevenueB`, `PayrollB`, `InventoryB`, `ProductsB`, `DepartmentB`).
- ViewModels: orchestrate UI state and trigger business logic.
- Views (XAML): data-bound to ViewModels with `BaseCommand` actions.

## Key Modules
### Sales Report
- Purpose: compute product turnover for a selected date range.
- View / ViewModel: `Views/SalesReportView.xaml`, `ViewModels/SaleReportViewModel.cs`.
- Important fields: `FromDate`, `ToDate`, `ProductId`, `Revenue`.
- Command: `CalculateCommand` calls `RevenueB.ProductRevenueForPeriod(ProductId, FromDate, ToDate)`.

### Payroll Cost Summary
- Purpose: compute total payroll cost for a period with cascading filters.
- View / ViewModel: `Views/PayrollCostSummaryView.xaml`, `ViewModels/PayrollCostSummaryViewModel.cs`.
- Important fields: `FromDate`, `ToDate`, `DepartmentId`, `PositionId`, `EmployeeId`, `PayrollCost`, `IsPositionEnabled`.
- Command: `CalculateCommand` calls `PayrollB.ProductRevenueForPeriod(DepartmentId, PositionId, EmployeeId, FromDate, ToDate)`.
- Behavior: changing `DepartmentId` resets `PositionId`/`EmployeeId`; changing `PositionId` resets `EmployeeId`. Informational messages on no data/zero result.

### Inventory Search
- Purpose: fetch batch/location details for a product at a site.
- View / ViewModel: `Views/InventorySearchView.xaml`, `ViewModels/InventorySearchViewModel.cs`.
- Inputs: `ProductId`, `SiteId`. Outputs: `BatchNumber`, `ClientId`, `SkuNumber`, `AisleNumber`, `LocationNumber`, `Quantity`.
- Command: `SearchInventoryCommand` triggers `LoadInventoryData` via `InventoryB.GetInventoryDetails(ProductId, SiteId)`. Validates `ProductId`.

## Prerequisites
- Windows with .NET Framework 4.8 Developer Pack
- Visual Studio 2019 or 2022
- SQL Server (Express/LocalDB) with access to a `BizConDb` database

## Getting Started
1. Clone or download the repository.
2. Open the solution/project in Visual Studio.
3. Restore NuGet packages (Entity Framework 6).
4. Configure the database connection:
   - Edit `app.config` connection string `BizConDbEntities`
   - Default points to `TomKacPcAcer\SQLEXPRESS` and database `BizConDb`. Change to your server, e.g.:
     ```
     data source=(localdb)\MSSQLLocalDB; initial catalog=BizConDb; integrated security=True;
     ```
5. Build and Run.

## Default Login (Demo)
- Username: `TKaczmarek`
- Password: `Haslo123`
- Implementation: fallback demo login is handled in the login ViewModel.
  - File: `ViewModels/LoginWindowViewModel.cs` (`ExecuteLogin`)
  - If you prefer DB-only auth, remove/guard the hardcoded branch.

## Usage Notes
- DB authentication compares plain text to `UserAccount.PasswordHash` (demo). For production, replace with proper hashing/verification.
- If you use DB login instead of the demo fallback, ensure there is an active `UserAccount` row with matching credentials and `IsActive = true`.

## Configuration
- Connection string: `MVVMFirma/app.config` (`BizConDbEntities`).
- Images and resources: `MVVMFirma/Resources/`.
- Business logic: `MVVMFirma/Models/BusinessLogic/`.

## Folder Highlights
- `Views/` – XAML views
- `ViewModels/` – presentation logic (commands, state)
- `Models/` – EF entities, context, business logic helpers

## Security
This project is for educational/demo purposes:
- Demo login and plain-text comparison are intentional simplifications.
- Do not use as-is in production; implement hashed passwords and remove fallback credentials.

