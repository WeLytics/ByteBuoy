export interface Job {
    id: number;
    description: string;
    hostname: string;
    startedDateTime: string;
    finishedDateTime: string;
    status: number;
}