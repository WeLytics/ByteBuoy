import {useEffect, useState} from "react";
import {Page} from "../../types/Page";
import {fetchData} from "../../services/apiService";
import {ChevronRightIcon, PlusIcon} from "@heroicons/react/24/outline";
import TimeAgo from "../../components/TimeAgo";
import Circle from "../../components/Circle";
import {statuses} from "../../models/statuses";
import SkeletonLoader from "../../components/SkeletonLoader";
import PageCreateForm from "../../components/PageCreateForm";

export default function PagesList() {
	const [pages, setData] = useState<Page[] | null>(null);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);
	const [isEditing, setIsEditing] = useState(false);

	const handleEdit = () => {
		setIsEditing(!isEditing);
	};


	useEffect(() => {
		const loadData = async () => {
			setLoading(true);
			setError(null);
			try {
				const result = await fetchData<Page[]>(`/api/v1/pages`);
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
		<div className="mx-auto max-w-3xl px-4 pt-12 sm:px-6 md:pt-16">
			<div className="mt-6 flex justify-end">
				<button
					type="button"

					onClick={handleEdit}
					className="inline-flex rounded-md px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
				>
					<PlusIcon className="-ml-0.5 mr-1.5 h-5 w-5" aria-hidden="true" />
					New Metric Project
				</button>
			</div>

			{isEditing && <PageCreateForm />}

			<ul role="list" className="divide-y divide-white/5">
				{pages?.map((page) => (
					<li
						key={page.id}
						className="relative flex items-center space-x-4 py-4"
					>
						<div className="min-w-0 flex-auto">
							<div className="flex items-center gap-x-3">
								<Circle colorClass={statuses[page.pageStatus]} />
								<h2 className="min-w-0 text-sm font-semibold leading-6 text-white">
									<a
										href={"metrics/" + page.slug}
										className="flex gap-x-2"
									>
										<span className="truncate">{page.title}</span>
										<span className="absolute inset-0" />
									</a>
								</h2>
							</div>
							<div className="mt-3 flex items-center gap-x-2.5 text-xs leading-5 text-gray-400">
								<p className="truncate">{page.description}</p>

								{page.updated && page.description && (
									<svg
										viewBox="0 0 2 2"
										className="h-0.5 w-0.5 flex-none fill-gray-300"
									>
										<circle cx={1} cy={1} r={1} />
									</svg>
								)}
								{<TimeAgo dateString={page.updated} />}
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
	);
}
