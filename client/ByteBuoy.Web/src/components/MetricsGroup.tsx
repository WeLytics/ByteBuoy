import React from "react";
import { MetricsGroup as MetricsGroupType } from "../types/MetricsGroup";
import StatusBar from "./MetricStatusBar";

type Props = {
	metricsGroup: MetricsGroupType;
};

const MetricsGroup: React.FC<Props> = ({ metricsGroup }) => {
	return (
		<>
			<h1>{metricsGroup.title}</h1>
			<h2>{metricsGroup.description}</h2>
			<StatusBar metricsBuckets={metricsGroup.bucketValues} />
		</>
	);
};

export default MetricsGroup;
