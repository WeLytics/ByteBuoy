// DialogBox.tsx

import React from "react";
import { MetricsBucket } from "../types/MetricsBucket";
import TimeAgo from "./TimeAgo";
import Circle from "./Circle";
import { statuses } from "../models/statuses";
import { RenderMetaJson } from "./MetaJsonRenderer";

interface DialogBoxProps {
	metricBucket: MetricsBucket;
}

const MetricBucketIndicatorHoverPanel: React.FC<DialogBoxProps> = ({
	metricBucket,
}) => {
	return (
		<div
			className="text-left bg-white dark:bg-gray-800 dark:text-white shadow-lg rounded-lg p-4 border border-gray-200 dark:border-gray-700 w-96"
			style={{
				position: "absolute",
				top: "100%",
				padding: "10px",
				zIndex: 100,
				display: "inline-block",
				width: "500px",
			}}
		>
			{metricBucket.metrics.map((metric) => (
				<>
					<React.Fragment key={metric.id}>
						<div className="m-2 p-2">
							<div>
								<Circle colorClass={statuses[metric.status]} />
								<TimeAgo dateString={metric.created} />
							</div>
							{metric.value}
							{metric.valueString}
							{RenderMetaJson(metric.metaJson)}
						</div>
						<hr />
					</React.Fragment>
				</>
			))}
		</div>
	);
};

export default MetricBucketIndicatorHoverPanel;
