export interface JobDetail {
    id?: number;
    taskName: string;
    description: string[] | null;
    errorText: string | null;  
    date: string;
    isFinished: boolean;
}