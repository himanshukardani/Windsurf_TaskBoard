# TaskBoard - Task Management Application

A complete task management application with user authentication and admin features built with ASP.NET Core Web API and React.

## Features

### Authentication
- User registration and login
- JWT-based authentication
- Role-based authorization (User/Admin)
- Password hashing with BCrypt

### User Features
- Create, read, update, delete tasks
- Filter tasks by status (To Do, In Progress, Done)
- View only own tasks

### Admin Features
- Dashboard with statistics
- View all users' tasks
- Delete any task
- Manage users (view all, change roles)

## Tech Stack

### Backend
- ASP.NET Core 7.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt.Net-Next

### Frontend
- React 18
- TypeScript
- Material-UI
- React Router
- Axios

## Project Structure

```
TaskBoard2/
├── TaskBoard.API/                 # Backend API
│   ├── Controllers/               # API Controllers
│   ├── Models/                    # Database Models
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Services/                  # Business Logic
│   ├── Data/
│   │   ├── Context/              # Database Context
│   │   └── Repositories/         # Repository Pattern
│   ├── Configuration/            # App Configuration
│   └── Program.cs               # Application Entry Point
├── TaskBoard.Frontend/           # Frontend React App
│   ├── src/
│   │   ├── components/          # Reusable Components
│   │   ├── pages/               # Page Components
│   │   ├── services/            # API Services
│   │   ├── utils/               # Utility Functions
│   │   └── types/               # TypeScript Types
│   ├── public/                  # Static Files
│   └── package.json            # Dependencies
├── API-Endpoints.md             # API Documentation
└── README.md                   # This File
```

## Setup Instructions

### Prerequisites
- .NET 7.0 SDK or later
- Node.js 16+ and npm
- Visual Studio Code or Visual Studio

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd TaskBoard2/TaskBoard.API
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5014`

5. Access Swagger documentation at `http://localhost:5014/swagger`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd TaskBoard2/TaskBoard.Frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

   The frontend will be available at `http://localhost:3000`

### Default Admin User

The application includes a default admin user:

- **Email:** Admin@gmail.com
- **Password:** Admin@123
- **Role:** Admin

This user is automatically created when the database is initialized.

## Database

The application uses SQL Server for data storage. See [SQL-Server-Setup.md](./SQL-Server-Setup.md) for detailed setup instructions.

### Database Schema

**Users Table:**
- Id (Primary Key)
- Name
- Email (Unique)
- PasswordHash
- Role
- CreatedAt

**Tasks Table:**
- Id (Primary Key)
- Title
- Description
- Status
- CreatedAt
- UpdatedAt
- UserId (Foreign Key)

### Connection String Options

Multiple connection string options are available in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true",
    "LocalDB": "Server=(localdb)\\mssqllocaldb;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true",
    "SQLExpress": "Server=.\\SQLEXPRESS;Database=TaskBoardDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

Choose the appropriate connection string based on your SQL Server setup.

## API Endpoints

See [API-Endpoints.md](./API-Endpoints.md) for detailed API documentation.

## Usage

1. Start both the backend and frontend applications
2. Open `http://localhost:3000` in your browser
3. Use the default admin credentials to login
4. Create new users or manage tasks
5. Admin users can access the admin dashboard at `/admin/dashboard`

## Development Notes

### Authentication Flow
1. User logs in with email/password
2. Backend validates credentials and returns JWT token
3. Frontend stores token in localStorage
4. Token is included in all subsequent API requests
5. Backend validates token on protected routes

### Role-Based Access
- **User:** Can manage their own tasks only
- **Admin:** Can manage all tasks and users

### Security Features
- Passwords are hashed using BCrypt
- JWT tokens expire after 60 minutes
- All API endpoints are protected except login/signup
- Admin endpoints require Admin role

## Troubleshooting

### Backend Issues
- Ensure .NET 7.0 SDK is installed
- Check that port 5014 is not in use
- Verify database connection string in appsettings.json

### Frontend Issues
- Ensure Node.js 16+ is installed
- Check that port 3000 is not in use
- Verify API proxy configuration in package.json

### Common Issues
- **CORS errors:** Backend CORS is configured to allow all origins in development
- **Database errors:** Database is created automatically, ensure write permissions
- **Authentication errors:** Check JWT configuration and token expiration

## Future Enhancements

- Email notifications for task updates
- File attachments for tasks
- Task categories and tags
- User avatars
- Real-time updates with SignalR
- Mobile responsive design improvements
- Task comments and collaboration
- Advanced filtering and search
- Task templates
- Export functionality (PDF, Excel)
