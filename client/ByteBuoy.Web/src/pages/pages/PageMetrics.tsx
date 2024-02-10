import React from 'react';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'

import { Metric } from '../../types/Metric';
import { fetchData } from '../../services/apiService';
import TimeAgo from '../../components/TimeAgo';
import { classNames } from '../../utils/utils';
import Circle from '../../components/Circle';


const statuses: { [key: number]: string } = {
	0: "text-green-400 bg-green-400/10", // Success
	1: "text-rose-400 bg-rose-400/10", // Warning
	2: "text-red-400 bg-red-400/10", // Error
};


export default function PageMetrics() {
  const { pageId: pageIdOrSlug } = useParams<{ pageId: string }>();
  const [data, setData] = useState<Metric[] | null>(null);

  useEffect(() => {
      const loadData = async () => {
          const result = await fetchData<Metric[]>(`/api/v1/pages/${pageIdOrSlug}/metrics`);
          setData(result);
      };

      loadData();
  }, []);


  const RenderData: React.FC<{ data: [] }> = ({ data }) => {
    if (typeof data === 'object' && data !== null) {
      return (
        <div>
          {Object.entries(data).map(([key, value]) => (
            <div key={key}>
              <strong>{key}:</strong> <RenderData data={value} />
            </div>
          ))}
        </div>
      );
    } else {
      return <span>{data}</span>;
    }
  };
  
  function RenderMetaJson(metaJson: string): React.ReactNode {
    const data = JSON.parse(metaJson);  
    return <RenderData data={data} />;
  }

  return (
    <div className="bg-gray-900 py-10">
      <table className="mt-6 w-full whitespace-nowrap text-left">
        <colgroup>
          <col className="w-full sm:w-4/12" />
          <col className="lg:w-4/12" />
          <col className="lg:w-2/12" />
        </colgroup>
        <thead className="border-b border-white/10 text-sm leading-6 text-white">
          <tr>
            <th scope="col" className="py-2 pl-4 pr-8 font-semibold sm:pl-6 lg:pl-8">
              Value
            </th>
            <th scope="col" className="hidden py-2 pl-0 pr-8 font-semibold sm:table-cell">
              Meta
            </th>
            <th scope="col" className="py-2 pl-0 pr-4 text-right font-semibold sm:pr-8 sm:text-left lg:pr-20">
              Date
            </th>
          </tr>
        </thead>
        <tbody className="divide-y divide-white/5">
          {data?.map((item) => (
            <tr key={item.id}>
              <td className="py-4 pl-4 pr-8 sm:pl-6 lg:pl-8">
                
              {/* <div
										className={classNames(
											statuses[item.status],
											"flex-none rounded-full p-1"
										)}
									>
										<div className="h-2 w-2 rounded-full bg-current" />
									</div>              */}
                <div className="flex items-center gap-x-4">
                <Circle colorClass={statuses[item.status]}></Circle>
                  {item.value} 
                  
                  {item.valueString}
                </div>
              </td>
              <td className="hidden py-4 pl-0 pr-4 sm:table-cell sm:pr-8">
                <div className="flex gap-x-3">
                    {RenderMetaJson(item.metaJson)}
                </div>
              </td>
              <td className="py-4 pl-0 pr-4 text-sm leading-6 sm:pr-8 lg:pr-20">
                <div className="flex items-center justify-end gap-x-2 sm:justify-start">
                  <TimeAgo dateString={item.created} />
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}