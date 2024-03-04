import React, { useState } from "react";
import { MetricsGroup as MetricsGroupType } from "../types/MetricsGroup";
import StatusBar from "./MetricStatusBar";
import { NumericToMetricIntervalMapping } from "../types/MetricInterval";
import MetricsGroupEdit from "./MetricsGroupEdit";

type Props = {
	metricsGroup: MetricsGroupType;
	reloadList: () => void;
};

const MetricsGroup: React.FC<Props> = ({ metricsGroup, reloadList }) => {
	const [isEditing, setIsEditing] = useState(false);

	const handleEdit = () => {

		setIsEditing(!isEditing);
	};


	return (
		<>
			<h1>
				{metricsGroup.title} (
				{NumericToMetricIntervalMapping[metricsGroup.metricInterval]})
			</h1>
			<h2>{metricsGroup.description}</h2>

			<StatusBar metricsBuckets={metricsGroup.bucketValues} />

			<button onClick={handleEdit}>Edit</button>

			{isEditing && (
				<MetricsGroupEdit initialValues={metricsGroup} reloadList={reloadList} />
			)}
		</>
	);
};

export default MetricsGroup;
