import {
	FieldError,
	FieldValues,
	Path,
	UseFormRegister,
} from "react-hook-form";

// Generischer Typ für Feldnamen, um Flexibilität zu erhöhen
export type FormFieldProps<
	TFormValues extends FieldValues,
	TFieldName extends keyof TFormValues
> = {
	type: string;
	placeholder: string;
	name: TFieldName;
	register: UseFormRegister<TFormValues>;
	error?: FieldError;
	valueAsNumber?: boolean;
};

const FormField = <
	TFormValues extends FieldValues,
	TFieldName extends keyof TFormValues
>({
	type,
	placeholder,
	name,
	register,
	error,
	valueAsNumber,
}: FormFieldProps<TFormValues, TFieldName>) => {
	// const inputRef = useRef();
	// const { setValue } = useFormContext(); // Assuming your form is wrapped in a FormProvider
	// const inputRef = useRef<HTMLInputElement>(null);

	// useEffect(() => {
	//     // Set the initial value if provided
	//     if (value !== undefined) {
	//         console.log("Setting initial value", name, value);
	//         setValue(name as string, value, { shouldValidate: true });
	//     }
	// }, [value, setValue, name]);

	return (
		<>
			<input
				// ref={(e) => {
					// register(name).ref(e);
					// inputRef.current = e;
				// }}
			/>

			<input
				type={type}
				placeholder={placeholder}
				// ref={inputRef}

				{...register(name as unknown as Path<TFormValues>, {
					valueAsNumber,
				})}
			/>
			{error && <span className="error-message">{error.message}</span>}
		</>
	);
};

export default FormField;
