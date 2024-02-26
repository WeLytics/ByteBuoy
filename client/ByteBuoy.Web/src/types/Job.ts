import { JobHistory } from "./JobHistory";

export interface Job {
    id: number;
    description: string;
    hostName: string;
    startedDateTime: string;
    finishedDateTime: string | null;
    status: number;

    jobHistory: JobHistory[] | null;
}

