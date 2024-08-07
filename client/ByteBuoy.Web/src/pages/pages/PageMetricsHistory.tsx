import {NavLink, useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Metric} from "../../types/Metric";
import {fetchData, fetchPagedData} from "../../services/apiService";
import TimeAgo from "../../components/TimeAgo";
import Circle from "../../components/Circle";
import PageTitle from "../../components/PageTitle";
import {Page} from "../../types/Page";
import {statuses} from "../../models/statuses";
import {RenderMetaJson} from "../../components/MetaJsonRenderer";
import SkeletonLoader from "../../components/SkeletonLoader";
import {MagnifyingGlassCircleIcon} from "@heroicons/react/24/outline";
import {EmptyMetricsState} from "../../components/EmptyMetricState";
import Pagination from "../../components/Pagination";
import { PaginationMeta } from "../../types/PaginationMeta";

function truncateString(input: string): string {
	const maxLength = 20;
	if (input.length <= maxLength) 
		return input;
	
	return `${input.substring(0, maxLength)}…`;
}

export default function PageMetricsList() {
	const { pageId, pageNr } = useParams<{ pageId: string; pageNr?: string }>();
    const pageNumber = pageNr ? parseInt(pageNr) : 1;

	const [metrics, setMetrics] = useState<Metric[] | null>(null);
	const [page, setPage] = useState<Page | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);
	const [paginationMeta, setPaginationMeta] = useState<PaginationMeta>();
	const navigate = useNavigate(); 
	
	const loadDataPage = async (newPageId: number) => {	
		navigate(`/metrics/${pageId}/history/${newPageId}`, { replace: true });
	}

	const onPreviousPage = async () => {	
		if (pageNumber > 1) {
			loadDataPage(pageNumber - 1);
		}
	}

	const onNextPage = async () => {	
		if (paginationMeta && pageNumber < paginationMeta.totalPages) {
			loadDataPage(pageNumber + 1);
		}
	}
	
	useEffect(() => {
		const loadData = async () => {
			setLoading(true);
			setError(null);
	
			try {
				const resultMetrics = await fetchPagedData<Metric[]>(
					`/api/v1/pages/${pageId}/metrics?page=${pageNumber}&pageSize=10`
				);
				setMetrics(resultMetrics.data);
				setPaginationMeta(resultMetrics.pagination!);
	
				const resultPage = await fetchData<Page>(`/api/v1/pages/${pageId}`);
				setPage(resultPage);
			} catch (error) {
				console.error("Failed to fetch metrics:", error);
				setError("Failed to load metrics. Please try again later.");
			} finally {
				setLoading(false);
			}
		};
	
		loadData();
	}, [pageId, pageNumber]); 
	

	if (loading) {
		return <SkeletonLoader />;
	}

	if (error) {
		return <p className="text-red-500">{error}</p>;
	}

	return (
		<>
			<PageTitle title={page?.title ?? "N/A"} />

			{metrics && metrics.length > 0 && (
				<div className="mx-auto max-w-6xl sm:px-6 lg:px-8 dark:text-white lg:py-3 overflow-x-auto">
					<div className="py-10">
						<table className="mt-6 whitespace-nowrap text-left table-auto">
							<colgroup>
								<col className="w-full sm:w-1/12" />
								<col className="lg:w-5/12" />
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
													colorClass={statuses[item.status]}
												></Circle>
												{item.value}
												{item.valueString}
											</div>
										</td>
										<td className="hidden py-4 pl-0 pr-4 sm:table-cell sm:pr-8">
											<div className="flex gap-x-3">
												{item.metaJson &&
													RenderMetaJson(item.metaJson)}
											</div>

											{item.hashSHA256 && (
												<div className="flex gap-x-3">
													<NavLink
														to={"/trace/" + item.hashSHA256}
													>
														<strong>
															Hash (SHA256):{" "}
															{truncateString(
																item.hashSHA256
															)}{" "}
															<MagnifyingGlassCircleIcon className="inline-block h-6 w-6"></MagnifyingGlassCircleIcon>
														</strong>{" "}
													</NavLink>
												</div>
											)}
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
				</div>
			)}
			{metrics === undefined ||
				(metrics && metrics.length === 0 && <EmptyMetricsState pageIdOrSlug={pageId!} />)}

			{paginationMeta && paginationMeta.totalPages > 1 && (
				<Pagination paginationMeta={paginationMeta!} onNavigate={loadDataPage!} onPrevious={onPreviousPage} onNext={onNextPage} />
			)}

		</>
	);
}
