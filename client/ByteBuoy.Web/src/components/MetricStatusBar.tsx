import React from 'react';
import MetricBucketIndicator from './MetricBucketIndicator';
import { MetricsBucket } from '../types/MetricsBucket';


// Define a type for the StatusBar props
interface StatusBarProps {
    metricsBuckets: MetricsBucket[];
}

// Main component to display the status bar
const StatusBar: React.FC<StatusBarProps> = ({ metricsBuckets }) => {
  return (
    <div className="flex space-x-2 p-4">
      {metricsBuckets.map((metricsBucket, index) => (
        <MetricBucketIndicator key={index} metricsBucket={metricsBucket} />
      ))}
    </div>
  );
};

export default StatusBar;
