import React, { useEffect, useState } from "react";
import { Page } from "../types/Page";
import { toast } from "react-toastify";
import FormField from "./FormField";
import { FieldError, FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { number, object, string, z } from "zod";

interface Props {
	page2: Page;
}

const schema = z.object({
	title: string().optional(),
	description: string().optional(),
});

const PageEditForm: React.FC<Props> = ({ page2 }) => {
	const [page, setPage] = useState<Page>(page2);
	const methods = useForm();
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm({
		resolver: zodResolver(schema),
	});


	const onSubmit: SubmitHandler<Inputs> = (data) => console.log(data)

	// const [errorText, setError] = useState<string>("");


	if (page === undefined) return <p>No Page...</p>;

	// function handleChange(event: ChangeEvent<HTMLInputElement>): void {
	// 	throw new Error("Function not implemented.");
	// }

	return (
		<form
			onSubmit={handleSubmit(onSubmit)}
			className="space-y-4 text-white"
		>
			<FormProvider {...methods}>
				<div className="text-black">
					<input {...register("title")} />
					{errors.name?.message && (
						<p>{errors.name?.message as string}</p>
					)}

					<input {...register("description")} />
					{errors.name?.message && (
						<p>{errors.description?.message as string}</p>
					)}

					<FormField
						type="text"
						name="title"
						// value={page.title}
						placeholder="Title"
						register={register}
						error={errors.title as FieldError}
						valueAsNumber={false}
						// onChange={handleChange}
					/>

					{/* 
				<label htmlFor="title" className="block text-sm font-medium leading-6 text-white text-left">
					Title
				</label>
				<input
					type="text"
					id="title"
					name="title"
					value={page.title || ""}
					onChange={handleChange}
					className="block w-full rounded-md border-0 bg-white/5 py-1.5 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6"
					// className={`border ${
					// 	errors.title ? "border-red-500" : "border-gray-300"
					//} rounded-md p-2 w-full`}
				/>

				<label htmlFor="description" className="block text-sm font-medium leading-6 text-white text-left">
					Description
				</label>
				<input
					type="text"
					id="description"
					name="description"
					value={page.description || ""}
					onChange={handleChange}
					className="block w-full rounded-md border-0 bg-white/5 py-1.5 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6"
					// className={`border ${
					// 	errors.title ? "border-red-500" : "border-gray-300"
					//} rounded-md p-2 w-full`}
				/> */}

					{/* {errors.title && (
					<p className="text-red-500 text-sm">{errors.title}</p>
				)} */}
				</div>

				<button
					type="submit"
					className="bg-blue-500 text-white py-2 px-4 rounded-md"
				>
					Submit
				</button>

				{JSON.stringify(page)}
			</FormProvider>
		</form>
	);
};

export default PageEditForm;
