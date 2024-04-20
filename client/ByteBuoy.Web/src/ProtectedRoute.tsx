import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from './hooks/useAuth';

interface ProtectedRouteProps {
  allowedRoles: string[];
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ allowedRoles }) => {
  const { isAuthenticated, userRoles } = useAuth();  // Assuming useAuth is a custom hook for auth state

  const isAuthorized = isAuthenticated && userRoles?.some(role => allowedRoles.includes(role));

  return isAuthorized ? <Outlet /> : <Navigate to="/login" />;
};

export default ProtectedRoute;
