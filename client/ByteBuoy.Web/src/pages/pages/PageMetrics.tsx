import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/apiService";
import { MetricsConsolidated } from "../../types/MetricsConsolidated";
import MetricsGroup from "../../components/MetricsGroup";
import MetricsSubGroup from "../../components/MetricsSubGroup";
import React from "react";

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
  {metrics?.metricsGroups !== undefined && metrics?.metricsGroups!.length >= 0 ? (
    metrics.metricsGroups.map((metricsGroup, index) => (
      <React.Fragment key={index}>
        <div>
          <MetricsGroup metricsGroup={metricsGroup} />
          {metricsGroup?.subGroups !== undefined && metricsGroup?.subGroups!.length >= 0
            ? metricsGroup.subGroups.map((subGroup, subIndex) => (
                <React.Fragment key={`subGroup-${index}-${subIndex}`}>
                  <div>
                    <hr></hr>
                    <MetricsSubGroup metricsSubGroup={subGroup} />
                  </div>
                </React.Fragment>
              ))
            : null}
        </div>
      </React.Fragment>
    ))
  ) : (
    <p>No metrics available</p>
  )}
</>

	);
}
