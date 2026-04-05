export interface User {
  id: number;
  name: string;
  email: string;
  role: string;
  createdAt: string;
}

export interface Task {
  id: number;
  title: string;
  description: string;
  status: string;
  createdAt: string;
  updatedAt: string;
  userId: number;
  userName: string;
  userEmail?: string;
  userRole?: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  role: string;
  name: string;
  email: string;
}

export interface SignupRequest {
  name: string;
  email: string;
  password: string;
}

export interface SignupResponse {
  message: string;
  userId: string;
}

export interface TaskCreateRequest {
  title: string;
  description: string;
  status: string;
}

export interface TaskUpdateRequest {
  title: string;
  description: string;
  status: string;
}

export interface DashboardStats {
  totalUsers: number;
  totalTasks: number;
  toDoTasks: number;
  inProgressTasks: number;
  doneTasks: number;
}

export interface UserRoleUpdateRequest {
  userId: number;
  newRole: string;
}
