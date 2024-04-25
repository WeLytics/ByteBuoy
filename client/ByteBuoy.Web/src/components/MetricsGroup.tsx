import React, {useState} from "react";
import {MetricsGroup as MetricsGroupType} from "../types/MetricsGroup";
import StatusBar from "./MetricStatusBar";
import {NumericToMetricIntervalMapping} from "../types/MetricInterval";
import MetricsGroupEdit from "./MetricsGroupEdit";
import {PencilIcon} from "@heroicons/react/24/outline";
import {useAuth} from "../hooks/useAuth";

type Props = {
	metricsGroup: MetricsGroupType;
	reloadList: () => void;
};

const MetricsGroup: React.FC<Props> = ({metricsGroup, reloadList}) => {
	const [isEditing, setIsEditing] = useState(false);
	const {isAdmin} = useAuth();

	const handleEdit = () => {
		setIsEditing(!isEditing);
	};

	return (
		<>
			<h1 className="font-bold">
				{metricsGroup.title} (
				{NumericToMetricIntervalMapping[metricsGroup.metricInterval]})
				{isAdmin && (
					<PencilIcon
						onClick={handleEdit}
						className="inline-block ml-3 h-5 w-5 cursor-pointer"
					/>
				)}
			</h1>
			<h2>{metricsGroup.description}</h2>

			{isEditing && isAdmin && (
				<div className="border-solid border-2 border-sky-100 p-3 rounded">
					<MetricsGroupEdit
						initialValues={metricsGroup}
						reloadList={reloadList}
					/>
				</div>
			)}

			<StatusBar metricsBuckets={metricsGroup.bucketValues} />
		</>
	);
};

export default MetricsGroup;
