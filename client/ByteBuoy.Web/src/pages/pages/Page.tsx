import React from 'react';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'
import { Page } from '../../types/Page';
import { fetchData } from '../../services/apiService';
import PageMetrics from './PageMetrics';
import PageTitle from '../../components/PageTitle';

const PageComponent: React.FC = () => {
  const { pageId } = useParams<{ pageId: string }>();
  const [data, setData] = useState<Page | null>(null);

  useEffect(() => {
      const loadData = async () => {
          const result = await fetchData<Page>(`/api/v1/pages/${pageId}`);
          setData(result);
      };

      loadData();
  }, []);
  
  return (
    <>
      <PageTitle title={data?.title ?? "N/A"} />
      <div className="mt-5">
      <PageMetrics />
      </div>
    </>
  )
}

export default PageComponent;
