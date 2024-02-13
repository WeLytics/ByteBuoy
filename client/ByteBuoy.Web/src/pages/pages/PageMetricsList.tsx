import React from "react";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Metric } from "../../types/Metric";
import { fetchData } from "../../services/apiService";
import TimeAgo from "../../components/TimeAgo";
import Circle from "../../components/Circle";
import PageTitle from "../../components/PageTitle";
import { Page } from "../../types/Page";
import { statuses } from "../../models/statuses";
import { RenderMetaJson } from "../../components/MetaJsonRenderer";

export default function PageMetricsList() {
	const { pageId: pageIdOrSlug } = useParams<{ pageId: string }>();
	const [metrics, setMetrics] = useState<Metric[] | null>(null);
	const [page, setPage] = useState<Page | null>(null);

	useEffect(() => {
		const loadData = async () => {
			const resultMetrics = await fetchData<Metric[]>(
				`/api/v1/pages/${pageIdOrSlug}/metrics`
			);
			setMetrics(resultMetrics);

			const resultPage = await fetchData<Page>(
				`/api/v1/pages/${pageIdOrSlug}`
			);
			setPage(resultPage);
		};

		loadData();
	}, []);

	return (
		<>
			<PageTitle title={page?.title ?? "N/A"} />
			<div className="mt-5">
				<div className="bg-gray-900 py-10">
					{/* <StatusBar statuses={data} /> */}

					<table className="mt-6 w-full whitespace-nowrap text-left">
						<colgroup>
							<col className="w-full sm:w-4/12" />
							<col className="lg:w-4/12" />
							<col className="lg:w-2/12" />
						</colgroup>
						<thead className="border-b border-white/10 text-sm leading-6 text-white">
							<tr>
								<th
									scope="col"
									className="py-2 pl-4 pr-8 font-semibold sm:pl-6 lg:pl-8"
								>
									Value
								</th>
								<th
									scope="col"
									className="hidden py-2 pl-0 pr-8 font-semibold sm:table-cell"
								>
									Meta
								</th>
								<th
									scope="col"
									className="py-2 pl-0 pr-4 text-right font-semibold sm:pr-8 sm:text-left lg:pr-20"
								>
									Date
								</th>
							</tr>
						</thead>
						<tbody className="divide-y divide-white/5">
							{metrics?.map((item) => (
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
											<Circle
												colorClass={
													statuses[item.status]
												}
											></Circle>
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
											<TimeAgo
												dateString={item.created}
											/>
										</div>
									</td>
								</tr>
							))}
						</tbody>
					</table>
				</div>
			</div>
		</>
	);
}
