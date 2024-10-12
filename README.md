# Saydalia Online

## Overview
**Saydalia Online** is a digital pharmacy that makes it easy to get the medicines and health products you need from home. With a wide range of medicines, vitamins, and wellness items, you can browse, compare, and order your products quickly. Our easy-to-use platform helps you find the right medicines, read product details, check prices, and see alternative choices. You can place an order in just a few steps, pick your delivery option, and have your medicines delivered safely to your door.

## Features
### Roles and Capabilities
| **Feature**               | **User** | **Admin** | **Pharmacist** |
|---------------------------|----------|-----------|----------------|
| Browse Medicines           | ✓        | ✓         | ✓              |
| Place Orders               | ✓        | ✗         | ✗              |
| Manage Medicine Categories | ✗        | ✓         | ✓ (except delete) |
| Manage Roles               | ✗        | ✓         | ✗              |
| View Orders                | ✓        | ✓         | ✓              |
| Cancel Orders              | ✗        | ✓         | ✗              |
| PayPal Payment Integration | ✓        | ✗         | ✗              |
| Password Reset via Email   | ✓        | ✗         | ✗              |

## Technical Details
- **Framework**: ASP.NET MVC
- **Architecture**: 3-Tier Architecture (Presentation, Application, Data)
- **Design Patterns**: Repository Pattern for clean, maintainable code
- **Payment Integration**: PayPal for secure payment processing
- **Email Service**: SMTP integration for password recovery

## Installation

### Prerequisites
- Visual Studio 2022 or later
- .NET SDK (version 6.0 or higher)
- SQL Server (2019 or higher)

### Steps to Run Locally
1. **Clone the repository**  
   ```bash
   git clone https://github.com/iHebaMohammed/saydalia-online.git
   cd saydalia-online
2. Set up the Database
Open SQL Server Management Studio (SSMS).
Restore the database using the provided .bak file (e.g., MedTechSaydaliaOnline.bak):
Right-click on Databases > Restore Database... > Select the .bak file > Restore.

3. Configure Connection String
In the appsettings.json file, update the connection string to point to your local SQL Server instance:
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=MedTechSaydaliaOnline;Trusted_Connection=True;"
}

4. Install Dependencies
Open the project in Visual Studio.
In the Solution Explorer, right-click the solution and select Restore NuGet Packages.

5. Run the Application
Press F5 or click Run in Visual Studio to start the application.

### Usage
---
Admin: Can manage medicine categories, roles, and orders.
Pharmacist: Can manage medicine categories but cannot delete roles or view other users.
User: Can browse medicines, add items to the cart, and make orders using PayPal integration.

### Database Setup
---
To set up the database, you can restore the backup file MedTechSaydaliaOnline.bak to your SQL Server:

1. Open SQL Server Management Studio (SSMS).
2. Right-click on Databases > Restore Database... > Select the .bak file.
3. Configure the destination database name and file paths as needed.
4. Click OK to complete the restore.

### Technologies Used
---
ASP.NET Core MVC: Web application framework
Entity Framework Core: ORM for database access
SQL Server: Database management system
PayPal API: Payment integration
SMTP: Email service for password reset
Repository Pattern: For clean and maintainable code
---
### Demo


https://github.com/user-attachments/assets/378c4a71-62b2-416c-b5a6-b9e35c45f31e


---

### Future Enhancements
---
1. Multi-language Support: Adding support for more languages to cater to a global audience.
2. Inventory Forecasting: Use AI/ML algorithms to predict medicine stock needs.
3. Mobile App: Build a mobile app version of the platform for greater accessibility.
