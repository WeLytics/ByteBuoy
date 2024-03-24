import { Metric } from "./Metric";
import { MetricStatus } from "./MetricStatus";

export interface MetricsBucket {
    start: string | Date;
    end: string | Date;
    value: string;
    status: MetricStatus;
    metrics: Metric[];
}
