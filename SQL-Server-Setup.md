# SQL Server Setup Guide for TaskBoard

## Overview
This guide helps you set up SQL Server for the TaskBoard application. The application has been configured to use SQL Server instead of SQLite.

## Connection String Options

The application includes multiple connection string options in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true",
    "LocalDB": "Server=(localdb)\\mssqllocaldb;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true",
    "SQLExpress": "Server=.\\SQLEXPRESS;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Option 1: Using SQL Server LocalDB (Recommended for Development)

### Prerequisites
- Visual Studio 2019/2022 or SQL Server Data Tools
- SQL Server Express with LocalDB

### Setup
1. Install SQL Server Express with LocalDB from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. Choose "Express" edition and include "LocalDB"
3. Update `appsettings.json` to use the LocalDB connection:
   ```json
   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true"
   ```

## Option 2: Using SQL Server Express

### Prerequisites
- SQL Server Express installed

### Setup
1. Install SQL Server Express from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. Ensure SQL Server Express service is running
3. Update `appsettings.json` to use SQL Express:
   ```json
   "DefaultConnection": "Server=.\\SQLEXPRESS;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true"
   ```

## Option 3: Using Full SQL Server

### Prerequisites
- SQL Server Standard/Developer/Enterprise edition

### Setup
1. Install SQL Server
2. Create a database named "TaskBoardDB" or let the application create it
3. Update `appsettings.json` with your server details:
   ```json
   "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true"
   ```

## Option 4: Using SQL Server with Username/Password

If you prefer SQL authentication instead of Windows authentication:

```json
"DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskBoardDB;User Id=your_username;Password=your_password;MultipleActiveResultSets=true"
```

## Verification Steps

1. **Test Connection**: Run the application
   ```bash
   cd TaskBoard.API
   dotnet run
   ```

2. **Check Database**: The application will automatically create the database and tables:
   - Users table
   - Tasks table
   - Seed data for default admin user

3. **Verify Data**: Use SQL Server Management Studio (SSMS) to verify:
   - Database `TaskBoardDB` exists
   - Tables `Users` and `Tasks` exist
   - Default admin user is created

## Troubleshooting

### Common Issues

1. **"A network-related or instance-specific error occurred"**
   - Verify SQL Server service is running
   - Check server name in connection string
   - Ensure firewall allows SQL Server connections

2. **"Login failed for user"**
   - Verify authentication method (Windows vs SQL)
   - Check user permissions
   - Ensure database exists

3. **"Cannot open database"**
   - Database will be created automatically
   - Ensure user has permission to create databases
   - Manually create database if needed

### SQL Server Management Studio (SSMS)
Download SSMS to manage your SQL Server: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

### Database Commands
```sql
-- Check if database exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'TaskBoardDB')
    PRINT 'Database exists'
ELSE
    PRINT 'Database does not exist'

-- View tables
USE TaskBoardDB
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES

-- View seed data
SELECT * FROM Users
SELECT * FROM Tasks
```

## Switching Back to SQLite (if needed)

If you want to switch back to SQLite:

1. Install SQLite package:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
   ```

2. Update `appsettings.json`:
   ```json
   "DefaultConnection": "Data Source=TaskBoard.db"
   ```

3. Update `Program.cs`:
   ```csharp
   options.UseSqlite(connectionString)
   ```

4. Remove SQL Server package:
   ```bash
   dotnet remove package Microsoft.EntityFrameworkCore.SqlServer
   ```

## Production Considerations

For production deployment:

1. Use a dedicated SQL Server instance
2. Implement proper backup strategies
3. Use SQL authentication with strong passwords
4. Configure connection pooling
5. Monitor database performance
6. Implement proper indexing

## Connection String Parameters Explained

- `Server`: SQL Server instance name
- `Database`: Database name (will be created automatically)
- `Trusted_Connection=true`: Use Windows authentication
- `MultipleActiveResultSets=true`: Allow multiple queries simultaneously
- `User Id/Password`: SQL authentication credentials

## Next Steps

Once SQL Server is configured:

1. Run the application to create the database
2. Test login with default admin credentials:
   - Email: Admin@gmail.com
   - Password: Admin@123
3. Start the frontend application
4. Verify all functionality works as expected
