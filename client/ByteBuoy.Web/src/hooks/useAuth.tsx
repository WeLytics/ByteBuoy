import { useAuthStore } from  '../store/auth';

export function useAuth() {
  const { user, userRoles, setUser, setUserRoles, logout } = useAuthStore();

  const isAuthenticated = Boolean(user);
  const isAdmin = userRoles?.includes('admin') ?? false;
  
  return {
    isAuthenticated,
    isAdmin,
    user,
    userRoles,
    setUser,
    setUserRoles,
    logout
  };
}
