import axios, { AxiosError, AxiosResponse } from 'axios';
import { redirect } from 'react-router';
import { PagedResponse } from '../types/PagedResponse';

const API_KEY = import.meta.env.VITE_API_KEY;
const BASE_URL = import.meta.env.VITE_BACKEND_API_URI;

export const api = axios.create({
    baseURL: BASE_URL,
    headers: {
        'X-API-Key': API_KEY
    },
    withCredentials: true
});


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

export const fetchData = async <T>(endpoint: string): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.get<T>(endpoint);
        return response.data;
    } catch (error) {
        console.error("Error fetching data: ", error);
        throw error;    
    }
};

export const fetchPagedData = async <T>(endpoint: string): Promise<PagedResponse<T>> => {
    try {
        const response: AxiosResponse<PagedResponse<T>> = await api.get<PagedResponse<T>>(endpoint);
        return response.data
    } catch (error) {
        console.error("Error fetching data: ", error);
        throw error;    
    }
};


export const postData = async <T, U>(endpoint: string, data: U): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.post<T>(endpoint, data);
        return response.data;
    } catch (error) {
        console.error("Error posting data: ", error);
        throw error;
    }
};

export const postDataNoPayload = async <T>(endpoint: string): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.post<T>(endpoint);
        return response.data;
    } catch (error) {
        console.error("Error posting data: ", error);
        throw error;
    }
};

export const patchData = async <T, U>(endpoint: string, data: U): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.patch<T>(endpoint, data);
        return response.data;
    } catch (error) {
        console.error("Error patching data: ", error);
        throw error;
    }
};

export const postInitialSetupAsync = async <T, U>(data: U): Promise<T> => { 
    const endpoint = '/api/v1/system/initialSetup';
    return postData<T, U>(endpoint, data);
}
