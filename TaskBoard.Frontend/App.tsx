import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthProvider, useAuth } from './utils/AuthContext';
import Login from './pages/Login';
import Signup from './pages/Signup';

// Create a theme
const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

// Protected Route Component
const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { user, isLoading } = useAuth();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
};

// Admin Route Component
const AdminRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { user, isAdmin, isLoading } = useAuth();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!user || !isAdmin) {
    return <Navigate to="/dashboard" replace />;
  }

  return <>{children}</>;
};

// Placeholder components for now
const Dashboard: React.FC = () => <div><h1>User Dashboard</h1><p>Task management coming soon...</p></div>;
const AdminDashboard: React.FC = () => <div><h1>Admin Dashboard</h1><p>Admin features coming soon...</p></div>;
const MyTasks: React.FC = () => <div><h1>My Tasks</h1><p>Task list coming soon...</p></div>;
const AllTasks: React.FC = () => <div><h1>All Tasks</h1><p>All tasks view coming soon...</p></div>;
const UserManagement: React.FC = () => <div><h1>User Management</h1><p>User management coming soon...</p></div>;

function AppRoutes() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/signup" element={<Signup />} />
      
      {/* User Routes */}
      <Route path="/dashboard" element={
        <ProtectedRoute>
          <Dashboard />
        </ProtectedRoute>
      } />
      <Route path="/my-tasks" element={
        <ProtectedRoute>
          <MyTasks />
        </ProtectedRoute>
      } />
      
      {/* Admin Routes */}
      <Route path="/admin/dashboard" element={
        <AdminRoute>
          <AdminDashboard />
        </AdminRoute>
      } />
      <Route path="/admin/tasks" element={
        <AdminRoute>
          <AllTasks />
        </AdminRoute>
      } />
      <Route path="/admin/users" element={
        <AdminRoute>
          <UserManagement />
        </AdminRoute>
      } />
      
      {/* Default redirect */}
      <Route path="/" element={<Navigate to="/login" replace />} />
    </Routes>
  );
}

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <Router>
          <AppRoutes />
        </Router>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App;
