import React from 'react';
// import { useParams } from 'react-router-dom';
// import { useEffect, useState } from 'react'

// import { Metric } from '../types/Metric';
// import { fetchData } from '../services/apiService';


const SetupComponent: React.FC = () => {
  // const { pageId } = useParams<{ pageId: string }>();
  // const [data, setData] = useState<Metric[] | null>(null);

  // useEffect(() => {
  //     const loadData = async () => {
  //         const result = await fetchData<Metric[]>(`/api/v1/pages/${pageId}/metrics`);
  //         setData(result);
  //     };

  //     loadData();
  // }, []);
  
  return (
    <>
      <h1>SETUP FirstRun ⛵</h1>

      {/* {data ? <pre>{JSON.stringify(data, null, 2)}</pre> : <p>Loading...</p>} */}
    </>
  )
}

export default SetupComponent;
