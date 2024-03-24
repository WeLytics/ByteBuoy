import { MetricsBucket } from './MetricsBucket';
import { MetricStatus } from './MetricStatus';

export interface MetricsSubGroup {
    groupTitle: string;
    groupValue: string | null;
    status: MetricStatus;
    groupByValues: MetricsBucket[];
  }
