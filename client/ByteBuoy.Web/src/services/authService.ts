import {AxiosRequestConfig, AxiosResponse} from "axios";
import {useAuthStore} from "../store/auth";
import {api} from "./apiService";
import {User} from "../types/User";
import {UserRole} from "../types/UserRole";

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
	skipAuthRefresh?: boolean;
}

interface LoginResponse {
	user: User;
	userRoles: string[];
	isAuthorized: boolean;
	message: string;
}

// Login function
export const login = async (email: string, password: string): Promise<LoginResponse> => {
	try {
		const config: CustomAxiosRequestConfig = {
			skipAuthRefresh: true,
		};

		const response: AxiosResponse<LoginResponse> = await api.post(
			"/api/v1/auth/login",
			{
				email,
				password,
			},
			config
		);

		if (
			response.data.user &&
			response.data.userRoles &&
			response.data.isAuthorized
		) {
			const user = response.data.user;
			const userRoles = mapUserRoles(response.data.userRoles);
			useAuthStore.getState().setUser(user);
			useAuthStore.getState().setUserRoles(userRoles);
		}

		return response.data;
	} catch (error) {
		console.error("Error logging in: ", error);
		throw error;
	}
};

function isValidRole(role: string): role is UserRole {
	return ["admin", "user", "guest"].includes(role);
}

// Map and validate string array to UserRole array
function mapUserRoles(roles: string[]): UserRole[] {
	return roles.map((role) => {
		if (isValidRole(role)) {
			return role; 
		} else {
			throw new Error(`Invalid role: ${role}`);
		}
	});
}
