import React from 'react';
import { useEffect, useState } from 'react'
// import './App.css'

import { Metric } from '../types/Metric';
import { fetchData } from '../services/apiService';


const PagesComponent: React.FC = () => {
  const [data, setData] = useState<Metric[] | null>(null);

  useEffect(() => {
      const loadData = async () => {
          const result = await fetchData<Metric[]>(`/api/v1/pages`);
          setData(result);
      };

      loadData();
  }, []);
  
  return (
    <>
      <h1>ByteBuoy ⛵</h1>

      {data ? <pre>{JSON.stringify(data, null, 2)}</pre> : <p>Loading...</p>}
    </>
  )
}

export default PagesComponent;
