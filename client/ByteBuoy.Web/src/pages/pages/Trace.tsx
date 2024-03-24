import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Metric } from "../../types/Metric";
import { fetchData } from "../../services/apiService";
import TimeAgo from "../../components/TimeAgo";
import Circle from "../../components/Circle";
import PageTitle from "../../components/PageTitle";
import { statuses } from "../../models/statuses";
import { RenderMetaJson } from "../../components/MetaJsonRenderer";
import SkeletonLoader from "../../components/SkeletonLoader";

export default function TraceComponent() {
	const { fileHash } = useParams<{ fileHash: string }>();
	const [metrics, setMetrics] = useState<Metric[] | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);


	useEffect(() => {
		const loadData = async () => {
			setLoading(true);
			setError(null);

			try {
				const resultMetrics = await fetchData<Metric[]>(
					`/api/v1/trace/${fileHash}`
				);
				setMetrics(resultMetrics);
				}
			catch (error) {
				console.error("Failed to fetch metrics:", error);
				setError("Failed to load metrics. Please try again later.");
			} finally {
				setLoading(false);
			}
		};

		loadData();
	}, [fileHash]);

	if (loading) {
		return <SkeletonLoader />;
	}

	if (error) {
		return <p className="text-red-500">{error}</p>;
	}

	return (
		<>
			<PageTitle title="Trace" />
			<div>SHA-256 Hash: {fileHash}</div>
			<div className="mt-5">
				<div className="py-10">
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
											{item.metaJson && RenderMetaJson(item.metaJson)}
										</div>

										{item.hashSHA256 && 
											<div className="flex gap-x-3">
												<strong>Hash (SHA256): {item.hashSHA256}</strong>
											</div>
										}
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
