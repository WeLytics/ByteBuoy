import React from 'react';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'
import { fetchData } from '../../services/apiService';
import PageTitle from '../../components/PageTitle';
import { Job as JobType } from '../../types/Job';


const Job: React.FC = () => {
  const { jobId } = useParams<{ jobId: string }>();
  const [data, setData] = useState<JobType | null>(null);

  useEffect(() => {
      const loadData = async () => {
          const result = await fetchData<JobType>(`/api/v1/jobs/${jobId}/`);
          setData(result);
      };

      loadData();
  }, []);
  
  return (
    <>
      <PageTitle title={data?.description ?? "N/A"} />
    </>
  )
}

export default Job;
