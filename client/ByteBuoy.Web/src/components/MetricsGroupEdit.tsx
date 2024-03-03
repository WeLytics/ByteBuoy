import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { MetricsGroup } from '../types/MetricsGroup';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from 'react-toastify';
import axios from 'axios';
import { patchData } from '../services/apiService';
import { useParams } from 'react-router-dom';

// Define the schema for the form validation using Zod
const schema = z.object({
    title: z.string().min(3, 'Name is too short').max(100, 'Name is too long'),
    description: z.string().nonempty('Description is required'),
    groupBy: z.string().nullable()
});


interface ServerResponse {
	errors: string,
}


type MetricsGroupEditProps = {
    initialValues: MetricsGroup;
    reloadList: () => void;
};

const MetricsGroupEdit: React.FC<MetricsGroupEditProps> = ({
    initialValues,
    reloadList,
}) => {
    const [errorServer, setErrorServer] = useState('');
    const { pageId: pageIdOrSlug } = useParams<{ pageId: string }>();

    const { register, handleSubmit, formState: { errors, isSubmitting }} = useForm<MetricsGroup>({
        defaultValues: initialValues,
        resolver: zodResolver(schema),
    });

    const handleFormSubmit = async (data: MetricsGroup) => {
        console.log("data", data);  
        try {
			setErrorServer('');
            const response = await patchData<ServerResponse, MetricsGroup>(`/api/v1/pages/${pageIdOrSlug}/metrics/groups/${initialValues.id}`, data);
			
            if (!response.errors)
                toast.success("Metrics Group updated");
		} catch (error) {
			if (axios.isAxiosError(error)) {
				const serverError = error.response?.data.errors;	
				toast.error("Failed to finish setup. Please try again: " + serverError);
				setErrorServer(serverError);
			}
			else {
				toast.error("Failed to finish setup. Please try again.");
			}
		} 
        reloadList();
    };

    return (
        <form onSubmit={handleSubmit(handleFormSubmit)} className="dark:bg-gray-800">
            <div className="mb-4">
                <label htmlFor="title" className="block text-gray-700 dark:text-white">Title:</label>
                <input
                    {...register('title')}
                    disabled={isSubmitting}
                    className="border border-gray-300 dark:border-gray-700 rounded-md px-3 py-2 mt-1 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 dark:bg-gray-700 dark:text-white"
                />
                {errors.title && <span className="text-red-500">{errors.title.message}</span>}
            </div>
            <div className="mb-4">
                <label htmlFor="description" className="block text-gray-700 dark:text-white">Description:</label>
                <input
                    {...register('description')}
                    disabled={isSubmitting}
                    className="border border-gray-300 dark:border-gray-700 rounded-md px-3 py-2 mt-1 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 dark:bg-gray-700 dark:text-white"
                />
                {errors.description && <span className="text-red-500">{errors.description.message}</span>}
            </div>
            <div className="mb-4">
                <label htmlFor="groupBy" className="block text-gray-700 dark:text-white">Group By:</label>
                <input
                    {...register('groupBy')}
                    disabled={isSubmitting}
                    className="border border-gray-300 dark:border-gray-700 rounded-md px-3 py-2 mt-1 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 dark:bg-gray-700 dark:text-white"
                />
                {errors.groupBy && <span className="text-red-500">{errors.groupBy.message}</span>}
            </div>

            <button type="submit" disabled={isSubmitting}  className="bg-indigo-500 text-white px-4 py-2 rounded-md hover:bg-indigo-600 focus:outline-none focus:bg-indigo-600">Save</button>
            {errorServer && (
							<p className="text-sm text-red-600 mt-1">{errorServer}</p>
						)}
        </form>
    );
};

export default MetricsGroupEdit;
