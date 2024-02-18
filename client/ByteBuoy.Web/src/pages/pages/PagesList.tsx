import { useEffect, useState } from "react";
import { Page } from "../../types/Page";
import { fetchData } from "../../services/apiService";
import { ChevronRightIcon } from "@heroicons/react/24/outline";
import TimeAgo from "../../components/TimeAgo";
import Circle from "../../components/Circle";
import { statuses } from "../../models/statuses";

export default function PagesList() {
	const [pages, setData] = useState<Page[] | null>(null);

	useEffect(() => {
		const loadData = async () => {
			const result = await fetchData<Page[]>(`/api/v1/pages`);
			setData(result);
		};

		loadData();
	}, []);

	return (
		<div className="mx-auto max-w-lg px-4 py-12 sm:px-6 md:py-16">
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
										href={"metrics/" + page.id}
										className="flex gap-x-2"
									>
										<span className="truncate">
											{page.title}
										</span>
										<span className="absolute inset-0" />
									</a>
								</h2>
							</div>
							<div className="mt-3 flex items-center gap-x-2.5 text-xs leading-5 text-gray-400">
								<p className="truncate">{page.description}</p>
								<svg
									viewBox="0 0 2 2"
									className="h-0.5 w-0.5 flex-none fill-gray-300"
								>
									<circle cx={1} cy={1} r={1} />
								</svg>
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
