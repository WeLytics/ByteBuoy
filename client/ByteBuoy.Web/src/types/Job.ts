export interface Job {
    id: number;
    description: string;
    hostName: string;
    startedDateTime: string;
    finishedDateTime: string;
    status: number;
}