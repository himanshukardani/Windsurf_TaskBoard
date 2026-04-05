import axios from 'axios';
import { LoginRequest, LoginResponse, SignupRequest, SignupResponse, Task, TaskCreateRequest, TaskUpdateRequest, User, DashboardStats, UserRoleUpdateRequest } from '../types';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:5014/api';

// Create axios instance
const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add token to requests
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Auth APIs
export const authAPI = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    const response = await api.post('/auth/login', data);
    return response.data;
  },

  signup: async (data: SignupRequest): Promise<SignupResponse> => {
    const response = await api.post('/auth/signup', data);
    return response.data;
  },
};

// Task APIs
export const taskAPI = {
  getMyTasks: async (status?: string): Promise<Task[]> => {
    const params = status ? { status } : {};
    const response = await api.get('/task', { params });
    return response.data;
  },

  getTaskById: async (id: number): Promise<Task> => {
    const response = await api.get(`/task/${id}`);
    return response.data;
  },

  createTask: async (data: TaskCreateRequest): Promise<Task> => {
    const response = await api.post('/task', data);
    return response.data;
  },

  updateTask: async (id: number, data: TaskUpdateRequest): Promise<Task> => {
    const response = await api.put(`/task/${id}`, data);
    return response.data;
  },

  deleteTask: async (id: number): Promise<void> => {
    await api.delete(`/task/${id}`);
  },
};

// Admin APIs
export const adminAPI = {
  getDashboard: async (): Promise<DashboardStats> => {
    const response = await api.get('/admin/dashboard');
    return response.data;
  },

  getAllTasks: async (): Promise<Task[]> => {
    const response = await api.get('/admin/tasks');
    return response.data;
  },

  deleteAnyTask: async (id: number): Promise<void> => {
    await api.delete(`/admin/tasks/${id}`);
  },

  getAllUsers: async (): Promise<User[]> => {
    const response = await api.get('/admin/users');
    return response.data;
  },

  getUserById: async (id: number): Promise<User> => {
    const response = await api.get(`/admin/users/${id}`);
    return response.data;
  },

  updateUserRole: async (data: UserRoleUpdateRequest): Promise<void> => {
    await api.put('/admin/users/role', data);
  },
};

export default api;
