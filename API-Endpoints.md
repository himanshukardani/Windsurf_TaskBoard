# TaskBoard API Endpoints Documentation

## Base URL
`http://localhost:5014/api`

## Authentication Endpoints

### POST /auth/login
Login user and return JWT token

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "jwt_token_here",
  "role": "User",
  "name": "John Doe",
  "email": "user@example.com"
}
```

### POST /auth/signup
Register new user

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "message": "User created successfully",
  "userId": "1"
}
```

## Task Endpoints (User)

### GET /task
Get current user's tasks (supports optional status filter)

**Query Parameters:**
- `status` (optional): "To Do", "In Progress", "Done"

**Headers:**
- `Authorization: Bearer {token}`

**Response:**
```json
[
  {
    "id": 1,
    "title": "Task title",
    "description": "Task description",
    "status": "To Do",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z",
    "userId": 1,
    "userName": "John Doe"
  }
]
```

### GET /task/{id}
Get specific task by ID

**Headers:**
- `Authorization: Bearer {token}`

### POST /task
Create new task

**Headers:**
- `Authorization: Bearer {token}`

**Request Body:**
```json
{
  "title": "New Task",
  "description": "Task description",
  "status": "To Do"
}
```

### PUT /task/{id}
Update existing task

**Headers:**
- `Authorization: Bearer {token}`

**Request Body:**
```json
{
  "title": "Updated Task",
  "description": "Updated description",
  "status": "In Progress"
}
```

### DELETE /task/{id}
Delete task

**Headers:**
- `Authorization: Bearer {token}`

## Admin Endpoints (Admin Role Required)

### GET /admin/dashboard
Get dashboard statistics

**Headers:**
- `Authorization: Bearer {token}`

**Response:**
```json
{
  "totalUsers": 10,
  "totalTasks": 25,
  "toDoTasks": 10,
  "inProgressTasks": 8,
  "doneTasks": 7
}
```

### GET /admin/tasks
Get all tasks from all users

**Headers:**
- `Authorization: Bearer {token}`

**Response:**
```json
[
  {
    "id": 1,
    "title": "Task title",
    "description": "Task description",
    "status": "To Do",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z",
    "userId": 1,
    "userName": "John Doe",
    "userEmail": "user@example.com",
    "userRole": "User"
  }
]
```

### DELETE /admin/tasks/{id}
Delete any task (admin only)

**Headers:**
- `Authorization: Bearer {token}`

### GET /admin/users
Get all users

**Headers:**
- `Authorization: Bearer {token}`

**Response:**
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "user@example.com",
    "role": "User",
    "createdAt": "2024-01-01T00:00:00Z"
  }
]
```

### GET /admin/users/{id}
Get specific user by ID

**Headers:**
- `Authorization: Bearer {token}`

### PUT /admin/users/role
Update user role

**Headers:**
- `Authorization: Bearer {token}`

**Request Body:**
```json
{
  "userId": 2,
  "newRole": "Admin"
}
```

## Error Responses

All endpoints return appropriate error responses:

**400 Bad Request:**
```json
{
  "message": "Invalid input data"
}
```

**401 Unauthorized:**
```json
{
  "message": "Invalid or missing token"
}
```

**403 Forbidden:**
```json
{
  "message": "Access denied"
}
```

**404 Not Found:**
```json
{
  "message": "Resource not found"
}
```

**500 Internal Server Error:**
```json
{
  "message": "An error occurred",
  "error": "Detailed error message"
}
```

## Default Admin User

Email: `Admin@gmail.com`  
Password: `Admin@123`  
Role: `Admin`
