import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { fetchData } from '../../services/apiService';
import { MetricsConsolidated } from '../../types/MetricsConsolidated';
import MetricsGroup from '../../components/MetricsGroup';

export default function PageMetrics() {
  const { pageId: pageIdOrSlug } = useParams<{ pageId: string; }>();
  const [ metrics, setMetrics] = useState<MetricsConsolidated | null>(null);

  useEffect(() => {
    const loadData = async () => {
      const result = await fetchData<MetricsConsolidated>(`/api/v1/pages/${pageIdOrSlug}/metrics/consolidated`);
      setMetrics(result);
      console.log(result)
    };

    loadData();
  }, [pageIdOrSlug]);



  return (
    <>
    {metrics?.metricsGroups !== undefined && metrics?.metricsGroups!.length >= 0 ? (
      metrics.metricsGroups.map((group, index) => (
        <MetricsGroup key={index} metricsGroup={group} />
      ))
    ) : (
      <p>No metrics available</p>
    )}
    </>
  );
}
