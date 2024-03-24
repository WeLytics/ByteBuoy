// import { yupResolver } from '@hookform/resolvers/yup';
// import { Dispatch, SetStateAction, useEffect } from 'react';
// import { FormProvider, useForm } from 'react-hook-form';
// // import { useTranslation } from 'react-i18next';

// export type FormWrapperProps = {
// 	onSubmit(data: unknown): void;
// 	validationSchema: unknown;
// 	children: JSX.Element | JSX.Element[];
// 	shouldFormReset?: boolean;
// 	// backendErrors?: FieldValidationError;
// 	setShoulFormReset?: Dispatch<SetStateAction<boolean>>;
// };

// const FormWrapper = ({
// 	validationSchema,
// 	onSubmit,
// 	children,
// 	shouldFormReset = false,
// 	// backendErrors,
// 	setShoulFormReset,
// }: FormWrapperProps) => {
// 	const formMethods = useForm({
// 		resolver: yupResolver(validationSchema),
// 	});
// 	// const { t } = useTranslation(['main']);
// 	const { reset, setError } = formMethods;

// 	useEffect(() => {
// 		if (shouldFormReset) {
// 			reset();
// 			if (setShoulFormReset) {
// 				setShoulFormReset(false);
// 			}
// 		}
// 	}, [shouldFormReset]);

// 	useEffect(() => {
// 		backendErrors?.fieldErrors.forEach((err)  => {
// 			setError(err.field!, {
// 				message:  t(err.i18nKey as any),
// 			});
// 		});
// 	}, [backendErrors]);

// 	return (
// 		<>
// 			<FormProvider {...formMethods}>
// 				<form
// 					className="w-full"
// 					noValidate={true}
// 					onSubmit={formMethods.handleSubmit(onSubmit)}
// 				>
// 					{children}
// 				</form>
// 			</FormProvider>
// 		</>
// 	);
// };

// export default FormWrapper;
