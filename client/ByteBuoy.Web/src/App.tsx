import { useEffect, useState } from 'react'
import './App.css'

import { Metric } from './types/Metric';
import { fetchData } from './services/apiService';

function App() {
  const [count, setCount] = useState(0)
  const [data, setData] = useState<Metric[] | null>(null);

  useEffect(() => {
      const loadData = async () => {
          const result = await fetchData<Metric[]>('/api/metrics');
          setData(result);
      };

      loadData();
  }, []);
  
  return (
    <>
      <h1>ByteBuoy ⛵</h1>

      {data ? <pre>{JSON.stringify(data, null, 2)}</pre> : <p>Loading...</p>}
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
      </div>
    </>
  )
}

export default App
