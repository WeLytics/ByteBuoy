import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { postData } from "../services/apiService";
import "../App.css";
import { SubmitHandler, useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import axios from "axios";

const FormSchema = z.object({
	title: z.string().min(5).max(50),
});

interface ServerResponse {
	errors: string,
	id: string,
}


type FormSchemaType = z.infer<typeof FormSchema>;

const PageCreateForm = () => {
	const [errorServer, setErrorServer] = useState('');
	const {
		register,
		handleSubmit,
		formState: { errors, isSubmitting },
	} = useForm<FormSchemaType>({
		resolver: zodResolver(FormSchema),
	});

	const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
		try {
			setErrorServer('');
			const response = await postData<ServerResponse, FormSchemaType>('/api/v1/pages',data);
			toast.success("Instance set up successfully!");
			setTimeout(() => {
				window.location.href = `/metrics/${response.id}`;
			}, 3000); // Redirect after showing success message
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
	};

	return (
		<div className="mx-auto max-w-xl px-4 sm:px-6 sm:py-10 lg:px-8 text-left">
			<form onSubmit={handleSubmit(onSubmit)}>
				<div className="space-y-12 py-4">
					<div className="border-b border-white/10 pb-12">
						<div>
							<h2 className="text-base font-semibold leading-7 text-white">
								Page
							</h2>
							<p className="mt-1 text-sm leading-6 text-gray-400">
								Create your Monitoring Page
							</p>
							<input
								type="text"
								placeholder="Page Title"
								disabled={isSubmitting}
								{...register("title")}
								className="block w-full rounded-md border-0 bg-white/5 py-1.5 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6"
							/>
							{errors.title && (
								<p className="text-sm text-red-600 mt-1">
									{errors.title.message}
								</p>
							)}
						</div>

						<div className="mt-6 flex items-center justify-end gap-x-6">
							<button
								type="submit"
								disabled={isSubmitting}
								className="rounded-md bg-indigo-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500"
							>
								Save
							</button>
						</div>

						{errorServer && (
							<p className="text-sm text-red-600 mt-1">{errorServer}</p>
						)}
					</div>
				</div>
			</form>
			<ToastContainer
				position="top-right"
				autoClose={5000}
				hideProgressBar={false}
				newestOnTop={false}
				closeOnClick
				rtl={false}
				pauseOnFocusLoss
				draggable
				pauseOnHover
			/>
		</div>
	);
};

export default PageCreateForm;
