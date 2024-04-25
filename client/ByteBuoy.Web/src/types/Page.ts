import { MetricStatus } from "./MetricStatus";

export interface Page {
    id: number;
    title: string;
    slug: string;
    updated: string;
    description: string;
    pageStatus: MetricStatus;
    isPublic: boolean;  
}