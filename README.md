# 📦 Core Inventory ERP (Inventory Management API)

An enterprise-grade, robust backend API built with **ASP.NET Core Web API** to handle full-scale inventory, stock management, purchasing, and sales operations.

## 🚀 Technical Stack & Architecture

- **Framework:** ASP.NET Core Web API (C#)
- **Database ORM:** Entity Framework Core (EF Core)
- **Database Engine:** Microsoft SQL Server
- **Authentication:** JWT (JSON Web Tokens) with Role-based Access Control capability.
- **Validation:** FluentValidation for declarative and strongly-typed model validation.
- **Documentation:** Swagger/OpenAPI UI integration with JWT Bearer support.

## 🗄️ Database Schema & Entities

The application utilizes a fully relational SQL database mapping real-world retail and ERP interactions. Inside the system, here is the complete entity map:

1. **Products (`Products` Table)**
   - Tracks `ProductCode` (Unique), Name, UnitPrice, CurrentStock, MinimumStock, ProductImage, and Category.
2. **Categories (`Categories` Table)**
   - Organizes products logically.
3. **Users (`Users` Table)**
   - Handles system authentication. Tracks Username (Unique), Email, PasswordHash, Role, and Activity Status.
4. **Customers (`Customers` Table)**
   - Manages client data: Name, ContactEmail, Phone, and Address.
5. **Suppliers (`Suppliers` Table)**
   - Manages vendor data: Name, ContactEmail, Phone, and Address.
6. **Sales Orders & Items (`SalesOrders` / `OrderItems` Tables)**
   - Maps customer purchases, linking `CustomerId`, `TotalAmount`, and a collection of `OrderItems` directly tied back to `Products`.
7. **Purchase Orders & Items (`PurchaseOrders` / `OrderItems` Tables)**
   - Maps vendor replenishments, linking `SupplierId`, `TotalAmount`, and a collection of `OrderItems`.
8. **Stock Transactions (`StockTransactions` Table)**
   - An immutable ledger tracking 'IN' and 'OUT' movements. Tracks `TransactionType`, `TransactionDate`, and `ProductId`.

## ⚙️ Core Application Features

*   **Secure Authentication Pipeline**: Endpoints are secured using JWT Auth. Tokens are verified via properties defined in `appsettings.json`.
*   **Media & File Uploads**: Product schemas support direct image file uploads. Images are securely saved to an internal `/images` directory, enforcing dimension/size limits and `.jpg/.png` constraints.
*   **Data Integrity & Validation**: FluentValidation automatically intercepts bad payloads before they hit the Controllers, ensuring the database only receives clean data.
*   **Ledger-Based Inventory**: Every purchase or sale interacts with `StockTransactions` rather than superficially updating a number, ensuring robust audit trails for stock tracking.
*   **Cross-Origin Configuration**: CORS is pre-configured to handle safe local/remote frontend connections.

## 📡 API Endpoint Overview (Controllers)

The project follows standard RESTful principles (`GET`, `POST`, `PUT`, `DELETE`):

*   `/api/User` - Registration, Login, Token Generation.
*   `/api/Product` - CRUD for products; includes specific endpoints for multipart form-data image uploads (`[FromForm] ProductCreateDto`).
*   `/api/Categories` - Manage product classifications.
*   `/api/Customer` - Client relationship management.
*   `/api/Supplier` - Vendor management.
*   `/api/PurchaseOrder` - Creating incoming orders and replenishing stock.
*   `/api/saleOrder` - Outgoing goods handling.
*   `/api/StockTransaction` - Retrieving transaction ledgers.
*   `/api/OrderItem` - Sub-items mapping.
*   `/api/Image` - Handling image delivery/serving to frontend clients.

## 🛠️ Getting Started (Local Development)

### Prerequisites
1. .NET SDK installed.
2. Microsoft SQL Server (LocalDB or Express)

### Setup Instructions
1. **Clone the repository.**
2. **Setup Connection String:** Navigate to `appsettings.json` and ensure the connection string named `"ConnectionString"` reflects your local SQL instance.
   *(Current Default: `server=LAPTOP-OECMPEB9\\SQLEXPRESS;database=Inventory_Management`)*
3. **Database Migration:** Run the following commands in the Package Manager Console or terminal:
   ```bash
   dotnet ef database update
   ```
4. **Run the Application:** 
   ```bash
   dotnet run
   ```
5. **Access Swagger UI:** Navigate to `https://localhost:<port>/swagger` to view and test endpoints. Use the lock icon top-right to insert your `Bearer <token>`.
