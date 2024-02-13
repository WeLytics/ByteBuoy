import { MetricsBucket as MetricsBucket } from './MetricsBucket';
import { MetricStatus } from './MetricStatus';

export interface MetricsGroup {
    id: number;
    title: string;
    description?: string | null;
    groupBy?: string | null;
    // groupValue?: string | null;
    // MetricInterval?: MetricInterval | null;
    groupStatus: MetricStatus;
    bucketValues: MetricsBucket[];
  }
