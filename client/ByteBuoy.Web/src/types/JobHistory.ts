// import { MetricStatus } from "./MetricStatus";

export interface JobHistory {
    id: number;
    jobId: number;
    taskNumber: number;
    taskName: string | null;
    description: string | null;
    // status: MetricStatus;
    createdDateTime: string;
    errorMessage: string | null;
}