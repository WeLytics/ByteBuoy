import React from "react";
import StatusBar from "./MetricStatusBar";
import { MetricsSubGroup as MetricsSubGroupType } from "../types/MetricsSubGroup";

type Props = {
	metricsSubGroup: MetricsSubGroupType;
};

const MetricsSubGroup: React.FC<Props> = ({ metricsSubGroup }) => {
	return (
		<>
			<h1>{metricsSubGroup.groupTitle}</h1>
			<h2>{metricsSubGroup.groupValue}</h2>
			<StatusBar metricsBuckets={metricsSubGroup.groupByValues} />
		</>
	);
};

export default MetricsSubGroup;
