import axios, { AxiosResponse } from 'axios';

const API_KEY = import.meta.env.VITE_API_KEY;
const BASE_URL = import.meta.env.VITE_BACKEND_API_URI;

// console.log('API_KEY', API_KEY);
// console.log('BASE_URL', BASE_URL);

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
