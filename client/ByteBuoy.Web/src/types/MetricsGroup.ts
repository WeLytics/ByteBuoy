import { MetricsBucket as MetricsBucket } from './MetricsBucket';
import { MetricsSubGroup } from './MetricsSubGroup';
import { MetricStatus } from './MetricStatus';

export interface MetricsGroup {
    id: number;
    title: string;
    description?: string | null;
    groupBy?: string | null;
    groupStatus: MetricStatus;
    bucketValues: MetricsBucket[];
    subGroups: MetricsSubGroup[];
    metricInterval: number;
    groupByValue: boolean;  
  }
