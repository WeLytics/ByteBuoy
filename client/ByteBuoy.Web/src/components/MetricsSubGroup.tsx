import React from "react";
import StatusBar from "./MetricStatusBar";
import { MetricsSubGroup as MetricsSubGroupType } from "../types/MetricsSubGroup";

type Props = {
	metricsSubGroup: MetricsSubGroupType;
};

const MetricsSubGroup: React.FC<Props> = ({ metricsSubGroup }) => {
	return (
		<>
			<div className="border-b border-white-200 pb-5 pt-5">
				<h3 className="text-base font-semibold leading-6 text-white-900">
					{metricsSubGroup.groupTitle}
				</h3>
				<p className="mt-2 max-w-4xl text-sm text-gray-200">
					{metricsSubGroup.groupValue}
				</p>
				<StatusBar metricsBuckets={metricsSubGroup.groupByValues} />
			</div>
		</>
	);
};

export default MetricsSubGroup;
