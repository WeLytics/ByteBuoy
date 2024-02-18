import { XCircleIcon, CheckCircleIcon, InformationCircleIcon } from '@heroicons/react/20/solid';

type NoticicationProps = {
    isError: boolean;
    isSuccess: boolean;
    title: string;
    text: string;
};

export default function Notification({ isError, isSuccess, title, text }: NoticicationProps) {
    // const iconClass = isError ? "text-red-400" : isSuccess ? "text-green-400" : "text-blue-400";
    const icon = isError ? <XCircleIcon className="h-5 w-5" aria-hidden="true" /> : isSuccess ? <CheckCircleIcon className="h-5 w-5" aria-hidden="true" /> : <InformationCircleIcon className="h-5 w-5" aria-hidden="true" />;

    return (
        <div className={`rounded-md ${isError ? 'bg-red-50' : isSuccess ? 'bg-green-50' : 'bg-blue-50'} p-4`}>
            <div className="flex">
                <div className="flex-shrink-0">
                    {icon}
                </div>
                <div className="ml-3">
                    <h3 className={`text-sm font-medium ${isError ? 'text-red-800' : isSuccess ? 'text-green-800' : 'text-blue-800'}`}>{title}</h3>
                    <div className={`mt-2 text-sm ${isError ? 'text-red-700' : isSuccess ? 'text-green-700' : 'text-blue-700'}`}>
                        {text}
                    </div>
                </div>
            </div>
        </div>
    );
}