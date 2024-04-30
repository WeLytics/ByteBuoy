import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/apiService";
import { MetricsConsolidated } from "../../types/MetricsConsolidated";
import MetricsGroup from "../../components/MetricsGroup";
import MetricsSubGroup from "../../components/MetricsSubGroup";
import React from "react";
import SkeletonLoader from "../../components/SkeletonLoader";
import { EmptyMetricsState } from "../../components/EmptyMetricState";

export default function PageMetrics() {
	const { pageId: pageIdOrSlug } = useParams<{ pageId: string }>();
	const [metrics, setMetrics] = useState<MetricsConsolidated | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);

	const loadData = async () => {
		setLoading(true);
		setError(null);
		try {
			const result = await fetchData<MetricsConsolidated>(
				`/api/v1/pages/${pageIdOrSlug}/metrics/consolidated`
			);
			setMetrics(result);
		} catch (error) {
			console.error("Failed to fetch metrics:", error);
			setError("Failed to load metrics. Please try again later.");
		} finally {
			setLoading(false);
		}
	};

	useEffect(() => {
		loadData();
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, []);

	if (loading) {
		return <SkeletonLoader />;
	}

	if (error) {
		return <p className="text-red-500">{error}</p>;
	}

	return (
		<>
			{metrics?.metricsGroups !== undefined &&
				metrics?.metricsGroups!.length >= 0 &&
				metrics.metricsGroups.map((metricsGroup, index) => (
					<React.Fragment key={index}>
						<div className="text-left">
							<MetricsGroup metricsGroup={metricsGroup} reloadList={loadData} />
							{metrics?.metricsGroups &&
							metrics.metricsGroups.length > 0
								? metricsGroup.subGroups.map(
										(subGroup, subIndex) => (
											<React.Fragment
												key={`subGroup-${index}-${subIndex}`}
											>
												<div>
													<hr />
													<MetricsSubGroup
														metricsSubGroup={
															subGroup
														}
													/>
												</div>
											</React.Fragment>
										)): null}
						</div>
					</React.Fragment>
				))}

			{metrics?.metricsGroups !== undefined &&
				metrics?.metricsGroups.length === 0 && 
				<EmptyMetricsState pageIdOrSlug={pageIdOrSlug!} />}
		</>
	);
}
