import React from "react";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/apiService";
import { Job as JobType } from "../../types/Job";
import { classNames } from "../../utils/utils";
import TimeAgo from "../../components/TimeAgo";
import { JobDetail } from "../../types/JobDetails";
import Circle from "../../components/Circle";
import { statuses } from "../../models/statuses";
import { CheckCircleIcon } from "@heroicons/react/20/solid";

const Job: React.FC = () => {
	const { jobId } = useParams<{ jobId: string }>();
	const [jobDetails, setJobDetails] = useState<JobDetail[]>([]);
	const [job, setJob] = useState<JobType | null>(null);
	let detailCount = 0;

	const addJobDetail = (newDetail: JobDetail) => {
		setJobDetails((currentDetails) => [...currentDetails, newDetail]);
		newDetail.id = detailCount++;
	};

	const loadData = async () => {
		const result = await fetchData<JobType>(`/api/v1/jobs/${jobId}/`);
		if (result === null) 
			return;

		addJobDetail({
			description: "Job started",
			date: result.startedDateTime,
			isFinished: false,
		});

		if (result.jobHistory) {
			for (const history of result.jobHistory) {
				addJobDetail({
					description: history.taskName ?? '',
					date: history.createdDateTime,
					isFinished: false,
				});
			}
		}
		else {
			console.log("No job history found")	
			console.log(result as JobType);
		}


		if (result?.finishedDateTime !== null) {
			addJobDetail({
				description: "Job finished",
				date: result.finishedDateTime,
				isFinished: true
			});
		}
		else {
			addJobDetail({
				description: "Job NOT finished yet",
				date: '',
				isFinished: false,
			});
		}

		// setJobDetails(details);
		setJob(result);
	};
	
	useEffect(() => {
		loadData();
	}, []);

	return (
		<>
			<div>
				<h1 className="text-3xl font-bold leading-tight tracking-tight text-gray-900 dark:text-white">
					{job && <Circle colorClass={statuses[job.status]} />}{" "}
					{job?.description ?? "N/A "}
				</h1>
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
								{jobIdx < jobDetails.length - 1 && (
									<div className="w-px bg-gray-200" />
								)}
							</div>

							<div className="relative flex h-6 w-6 flex-none items-center justify-center">
								{jobIdx == jobDetails.length - 1 && jobDetail.isFinished ? (
									<CheckCircleIcon
										className="h-6 w-6 text-green-600"
										aria-hidden="true"
									/>
								) : (
									<div className="h-1.5 w-1.5 rounded-full bg-gray-100 ring-1 ring-gray-300" />
								)}
							</div>
							<p className="flex-auto py-0.5 text-xs leading-5 dark:text-white-500 text-gray-500 text-left">
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
