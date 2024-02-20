import React from "react";
import { useEffect, useState } from "react";

import { Job } from "../../types/Job";
import { fetchData } from "../../services/apiService";
import { ChevronRightIcon } from "@heroicons/react/24/outline";
import { classNames } from "../../utils/utils";
import TimeAgo from "../../components/TimeAgo";
import { statuses } from "../../models/statuses";
import SkeletonLoader from "../../components/SkeletonLoader";

const DEFAULT_DATE = "0001-01-01T00:00:00Z";

const isValidDate = (dateString: string) => {
	return dateString && dateString !== DEFAULT_DATE;
};

const Jobs: React.FC = () => {
	const [jobs, setData] = useState<Job[] | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const loadData = async () => {
			setLoading(true);
			setError(null);
			try {
				const result = await fetchData<Job[]>(
					`/api/v1/jobs?orderby=finishedDatetime desc`
				);
				setData(result);
			} catch (error) {
				console.error("Failed to fetch metrics:", error);
				setError("Failed to load metrics. Please try again later.");
			} finally {
				setLoading(false);
			}
		};

		loadData();
	}, []);

	if (loading) {
		return <SkeletonLoader />;
	}

	if (error) {
		return <p className="text-red-500">{error}</p>;
	}

	return (
		<>
			<div className="mx-auto max-w-lg px-4 py-12 sm:px-6 md:py-16">
				<ul role="list" className="divide-y divide-white/5">
					{jobs?.map((job) => (
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
											href={"jobs/" + job.id}
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
										{isValidDate(job.finishedDateTime) ? (
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
									<svg
										viewBox="0 0 2 2"
										className="h-0.5 w-0.5 flex-none fill-gray-300"
									>
										<circle cx={1} cy={1} r={1} />
									</svg>
									<p className="whitespace-nowrap">
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
		</>
	);
};

export default Jobs;
