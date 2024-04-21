import React from "react";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom"; 

import { Job } from "../../types/Job";
import { fetchPagedData } from "../../services/apiService";
import { ChevronRightIcon } from "@heroicons/react/24/outline";
import { classNames } from "../../utils/utils";
import TimeAgo from "../../components/TimeAgo";
import { statuses } from "../../models/statuses";
import SkeletonLoader from "../../components/SkeletonLoader";
import { ServerIcon } from "@heroicons/react/20/solid";
import Pagination from "../../components/Pagination";
import { PaginationMeta } from "../../types/PaginationMeta";

const DEFAULT_DATE = "0001-01-01T00:00:00Z";

const isValidDate = (dateString: string) => {
	return dateString && dateString !== DEFAULT_DATE;
};

const Jobs: React.FC = () => {
	const [jobs, setJobs] = useState<Job[] | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);
	const [paginationMeta, setPaginationMeta] = useState<PaginationMeta>();
	const navigate = useNavigate(); 
	const { pageId: pageIdParam } = useParams(); 
	const pageId = Number(pageIdParam) || 1; 
	
	const loadDataPage = async (newPageId: number) => {	
		console.log('newPageId', newPageId);
		navigate(`/jobs/${newPageId}`, { replace: true });
	}

	const onPreviousPage = async () => {	
		console.log('onprevious');
		if (pageId > 1) {
			loadDataPage(pageId - 1);
		}
	}

	const onNextPage = async () => {	
		console.log('onNextPage');
		if (paginationMeta && pageId < paginationMeta.totalPages) {
			loadDataPage(pageId + 1);
		}
	}

	useEffect(() => {
		if (!pageIdParam || isNaN(Number(pageIdParam))) {
			navigate(`/jobs/${pageId}`, { replace: true });
		}
		const loadData = async () => {
			setLoading(true);
			setError(null);
			try {
				const result = await fetchPagedData<Job[]>(
					`/api/v1/jobs?page=${pageId}&orderby=startedDateTime desc&pageSize=10`
				);
				setJobs(result.data);

				console.log('result', result);
				setPaginationMeta(result.pagination!);
			} catch (error) {
				console.error("Failed to fetch metrics:", error);
				setError("Failed to load metrics. Please try again later.");
			} finally {
				setLoading(false);
			}
		};

		loadData();
	}, [pageId, navigate, pageIdParam]);

	if (loading) {
		return <SkeletonLoader />;
	}

	if (error) {
		return <p className="text-red-500">{error}</p>;
	}

	return (
		<>
			<div className="mx-auto max-w-3xl px-4 pt-12 sm:px-6 md:pt-16">
				<ul role="list" className="divide-y divide-white/5">
					{jobs && jobs.map((job) => (
						<li
							key={job.id}
							className="relative flex items-center space-x-4 py-4"
						>
							<div className="min-w-0 flex-auto">
								<div className="flex items-center gap-x-3">
									<div
										className={classNames(
											statuses[job.status],
											"flex-none rounded-full p-1"
										)}
									>
										<div className="h-2 w-2 rounded-full bg-current" />
									</div>
									<h2 className="min-w-0 text-sm font-semibold leading-6 text-white">
										<a
											href={"/job/" + job.id}
											className="flex gap-x-2"
										>
											<span className="truncate">
												{job.description}
											</span>
											<span className="absolute inset-0" />
										</a>
									</h2>
								</div>
								<div className="mt-3 flex items-center gap-x-2.5 text-xs leading-5 text-gray-400">
									<p className="truncate">
										{job.finishedDateTime && isValidDate(job.finishedDateTime) ? (
											<>
												<span>Finished </span>
												<TimeAgo
													dateString={
														job.finishedDateTime
													}
												/>
											</>
										) : (
											<>
												<span>Started </span>
												<TimeAgo
													dateString={
														job.startedDateTime
													}
												/>
											</>
										)}
									</p>

									<ServerIcon className="h-4 w-4 flex-none text-gray-400" />
									<p className="whitespace-nowrap pl-0">
										{job.hostName}
									</p>
								</div>
							</div>
							<ChevronRightIcon
								className="h-5 w-5 flex-none text-gray-400"
								aria-hidden="true"
							/>
						</li>
					))}
				</ul>
			</div>

			{paginationMeta && paginationMeta.totalPages > 1 && (
				<Pagination paginationMeta={paginationMeta!} onNavigate={loadDataPage!} onPrevious={onPreviousPage} onNext={onNextPage} />
			)}
		</>
	);
};

export default Jobs;
