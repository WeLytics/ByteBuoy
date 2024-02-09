import axios, { AxiosResponse } from 'axios';

const API_KEY = import.meta.env.VITE_API_KEY;
const BASE_URL = import.meta.env.VITE_BACKEND_API_URI;

const api = axios.create({
    baseURL: BASE_URL,
    headers: {
        'X-API-Key': API_KEY
    }
});

export const fetchData = async <T>(endpoint: string): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await api.get<T>(endpoint);
        return response.data;
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

export const CreatePageAsync = async <T, U>(data: U): Promise<T> => { 
    const endpoint = '/api/v1/pages';
    return postData<T, U>(endpoint, data);
}
