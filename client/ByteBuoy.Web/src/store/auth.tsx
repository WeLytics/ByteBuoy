import {create} from "zustand";
import {persist} from "zustand/middleware";
import {User} from "../types/User";
import {UserRole} from "../types/UserRole";

type AuthState = {
	user: User | null;
	userRoles: UserRole[] | null;
	setUser: (user: User) => void;
	setUserRoles: (roles: UserRole[]) => void;
	logout: () => void;
};

export const useAuthStore = create<AuthState>()(
	persist(
		(set) => ({
			user: null,
			userRoles: null,
			setUser: (user) => set({user}),
			setUserRoles: (userRoles) => set({userRoles}),
			logout: () => set({user: null, userRoles: null}),
		}),
		{
			name: "user",
		}
	)
);
