export interface JobHistory {
    id: number;
    jobId: number;
    taskNumber: number;
    taskName: string | null;
    description: string | null;
    createdDateTime: string;
    errorMessage: string | null;
}