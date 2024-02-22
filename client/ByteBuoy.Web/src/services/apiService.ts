import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
import { redirect } from 'react-router';
import { toast } from "react-toastify";

const API_KEY = import.meta.env.VITE_API_KEY;
const BASE_URL = import.meta.env.VITE_BACKEND_API_URI;

const api = axios.create({
    baseURL: BASE_URL,
    headers: {
        'X-API-Key': API_KEY
    },
    withCredentials: true
});

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
    skipAuthRefresh?: boolean;
}

declare module 'axios' {
    export interface AxiosRequestConfig {
      skipAuthRefresh?: boolean; // Custom property added to the AxiosRequestConfig
    }
}

// Axios response interceptor
api.interceptors.response.use(
    (response) => response, 
    async (error: AxiosError) => {
        if (error.config?.skipAuthRefresh) {
            return Promise.reject(error);
        }

        if (error.response?.status === 401) {
            console.error("Unauthorized! Redirecting to login...");
            
            redirect('/login');
        }
        return Promise.reject(error);
    }
);


interface LoginResponse {
    user: {
      id: string;
      username: string;
    };
    token: string;
  }


// Login function
export const login = async (email: string, password: string): Promise<LoginResponse> => {
    try {
        const config: CustomAxiosRequestConfig = {
            skipAuthRefresh: true, 
        };

        const response: AxiosResponse<LoginResponse> = await api.post('/login?useCookies=true', {
            email,
            password,
        }, config);

        return response.data;
    } catch (error) {
        console.error("Error logging in: ", error);
        throw error;
    }
};

export const fetchData = async <T>(endpoint: string): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.get<T>(endpoint);
        return response.data;
    } catch (error) {
        toast.error("Failed to load data!");
        console.error("Error fetching data: ", error);
        throw error;    
    }
};

export const postData = async <T, U>(endpoint: string, data: U): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.post<T>(endpoint, data);
        return response.data;
    } catch (error) {
        toast.error("Failed to send data!");
        console.error("Error posting data: ", error);
        throw error;
    }
};

export const CreatePageAsync = async <T, U>(data: U): Promise<T> => { 
    const endpoint = '/api/v1/system/initialSetup';
    return postData<T, U>(endpoint, data);
}
