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
    <div className="flex space-x-0.5 p-1 md:p-4 md:space-x-1">
      {metricsBuckets.map((metricsBucket, index) => (
        <MetricBucketIndicator key={index} metricsBucket={metricsBucket} />
      ))}
    </div>
  );
};

export default StatusBar;
