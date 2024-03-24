import React, { useState } from "react";
import { MetricsBucket } from "../types/MetricsBucket";
import MetricBucketIndicatorHoverPanel from "./MetricBucketIndicatorHoverPanel";
import { MetricStatus } from "../types/MetricStatus";

interface StatusIndicatorProps {
	metricsBucket: MetricsBucket;
}

const MetricBucketIndicator: React.FC<StatusIndicatorProps> = ({
	metricsBucket,
}) => {
	const [showDialog, setShowDialog] = useState(false);
	const statusColors: { [key: number]: string } = {
		0: "bg-gray-500", // No data
		1: "bg-green-500", // Success
		2: "bg-orange-500", // Warning
		3: "bg-red-500", // Error
	};

	return (
		<>
			<div
				title={metricsBucket.value}
				className={`tooltip ${
					statusColors[metricsBucket.status]
				} p-1 h-10 flex items-center justify-center text-white rounded ${ metricsBucket.status !== MetricStatus.NoData ? 'cursor-pointer' : '' }`}
				style={{
					position: "relative", // Important for absolute positioning of the dialog
					display: "inline-block",
				}}
				// onMouseEnter={() => setShowDialog(true)}
				// onMouseLeave={() => setShowDialog(false)}
				onMouseDown={() => setShowDialog(!showDialog)}
			>
				{showDialog && metricsBucket.status !== MetricStatus.NoData && (
					<MetricBucketIndicatorHoverPanel
						metricBucket={metricsBucket}
					/>
				)}
			</div>
		</>
	);
};

export default MetricBucketIndicator;
