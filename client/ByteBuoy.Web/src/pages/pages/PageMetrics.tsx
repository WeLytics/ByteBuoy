import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/apiService";
import { MetricsConsolidated } from "../../types/MetricsConsolidated";
import MetricsGroup from "../../components/MetricsGroup";
import MetricsSubGroup from "../../components/MetricsSubGroup";

export default function PageMetrics() {
	const { pageId: pageIdOrSlug } = useParams<{ pageId: string }>();
	const [metrics, setMetrics] = useState<MetricsConsolidated | null>(null);

	useEffect(() => {
		const loadData = async () => {
			const result = await fetchData<MetricsConsolidated>(
				`/api/v1/pages/${pageIdOrSlug}/metrics/consolidated`
			);
			setMetrics(result);
		};

		loadData();
	}, [pageIdOrSlug]);

	return (
		<>
			{metrics?.metricsGroups !== undefined &&
			metrics?.metricsGroups!.length >= 0 ? (
				metrics.metricsGroups.map((metricsGroup, index) => (
					<>
						<MetricsGroup key={index} metricsGroup={metricsGroup} />
						{metricsGroup?.subGroups !== undefined &&
						metricsGroup?.subGroups!.length >= 0
							? metricsGroup.subGroups.map((subGroup, index) => (
									<>
                    <hr></hr>
										<MetricsSubGroup
											key={index}
											metricsSubGroup={subGroup}
										/>
									</>
                ))
							: null}
					</>
				))
			) : (
				<p>No metrics available</p>
			)}
		</>
	);
}
