import { useAuthStore } from  '../store/auth';

export function useAuth() {
  const { user, userRoles, setUser, setUserRoles, logout } = useAuthStore();

  // Add any additional logic or methods you might need
  const isAuthenticated = Boolean(user);
  
  return {
    isAuthenticated,
    user,
    userRoles,
    setUser,
    setUserRoles,
    logout
  };
}
