import React from "react";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/apiService";
import PageTitle from "../../components/PageTitle";
import { Job as JobType } from "../../types/Job";
import { classNames } from "../../utils/utils";
import TimeAgo from "../../components/TimeAgo";
import { JobDetail } from "../../types/JobDetails";
import Circle from "../../components/Circle";

const statuses: { [key: number]: string } = {
	0: "text-gray-500 bg-gray-100/10", // Running
	1: "text-green-400 bg-green-400/10", // Success
	2: "text-rose-400 bg-rose-400/10", // Warning
	3: "text-red-400 bg-red-400/10", // Error
};

const Job: React.FC = () => {
	const { jobId } = useParams<{ jobId: string }>();
	const [jobDetails, setJobDetails] = useState<JobDetail[]>([]);
	const [job, setJob] = useState<JobType | null>(null);
	let detailCount = 0;

	const addJobDetail = (newDetail: JobDetail) => {
		setJobDetails((currentDetails) => [...currentDetails, newDetail]);
		newDetail.id = detailCount++;
		console.log(newDetail);
	};

	useEffect(() => {
		const loadData = async () => {
			const result = await fetchData<JobType>(`/api/v1/jobs/${jobId}/`);
			// const details: JobDetail[] = [];
			if (result === null) return;

			addJobDetail({
				description: "Job started",
				date: result.startedDateTime,
			});

			if (result?.finishedDateTime !== null) {
				addJobDetail({
					description: "Job finished",
					date: result.finishedDateTime,
				});
			}

			// setJobDetails(details);
			setJob(result);
		};

		loadData();
	}, []);

	return (
		<>
      {/* <Circle color={"red-400"} size={30} />   */}

			{job?.status !== undefined && (
        <Circle colorClass={classNames(
          statuses[job!.status]
        )}
         />
				// <div
				// 	className={classNames(
				// 		statuses[job!.status],
				// 		"flex-none rounded-full p-1"
				// 	)}
				// >
				// 	<div className="h-2 w-2 rounded-full bg-current" />
				// </div>
			)}
			<PageTitle title={ job !== null ?  <Circle colorClass={statuses[job!.status]} />
          &&   job.description + "asdf" : "N/A"} />


			<div>
				<p>Job ID: {jobId}</p>
				<p>Job Description: {job?.description ?? "N/A"}</p>
				<p>Job Status: {job?.status ?? "N/A"}</p>
			</div>

			<div className="mx-auto max-w-lg px-4 py-12 sm:px-6 md:py-16">
				<ul role="list" className="space-y-6">
					{jobDetails.map((jobDetail, jobIdx) => (
						<li
							key={jobDetail.id}
							className="relative flex gap-x-4"
						>
							<div
								className={classNames(
									jobIdx === jobDetails.length - 1
										? "h-6"
										: "-bottom-6",
									"absolute left-0 top-0 flex w-6 justify-center"
								)}
							>
								<div className="w-px bg-gray-200" />
							</div>

							<div className="relative flex h-6 w-6 flex-none items-center justify-center dark:bg-gray">
								<div className="h-1.5 w-1.5 rounded-full bg-gray-100 ring-1 ring-gray-300" />
							</div>
							<p className="flex-auto py-0.5 text-xs leading-5 dark:text-white-500 text-gray-500">
								<span className="font-medium dark:text-white text-gray-900">
									{jobDetail.description}
								</span>
							</p>
							<div className="flex-none py-0.5 text-xs leading-5 text-gray-500">
								<TimeAgo dateString={jobDetail.date} />
							</div>
						</li>
					))}
				</ul>
			</div>
		</>
	);
};

export default Job;
