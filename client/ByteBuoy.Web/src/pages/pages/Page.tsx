import React, { useCallback } from "react";
import { Link, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Page } from "../../types/Page";
import { fetchData, postDataNoPayload } from "../../services/apiService";

import { Fragment } from "react";
import {
	ChevronDownIcon,
	ClockIcon,
	PencilIcon,
	ArchiveBoxXMarkIcon,
	ArrowPathIcon
} from "@heroicons/react/20/solid";
import { Menu, Transition } from "@headlessui/react";

import PageMetrics from "./PageMetrics";
import { classNames } from "../../utils/utils";
// import PageEditForm from "../../components/PageEditForm";

const PageComponent: React.FC = () => {
	const { pageId } = useParams<{ pageId: string }>();
	const [page, setPage] = useState<Page | null>(null);
	const [refreshKey, setRefreshKey] = useState(0);
	
	useEffect(() => {
		loadData();
	}, []);

	const loadData = async () => {
		const result = await fetchData<Page>(`/api/v1/pages/${pageId}`);
		setPage(result);
	};

	const purgeList= async () => {
		await postDataNoPayload(`/api/v1/pages/${pageId}/metrics/purge`);
		refreshChild();
	}

	const refreshChild = useCallback(() => {
        // Incrementing the key will cause the useEffect in ChildComponent to run
        setRefreshKey(prevKey => prevKey + 1);
    }, []);

	return (
		<>
			
			{/* {page && <PageEditForm page2={page} />} */}
			
			{/* <PageTitle title={data?.title ?? "N/A"} /> */}
			<div className="lg:flex lg:items-center lg:justify-between mt-4">
				<div className="min-w-0 flex-1">
					<h2 className="mt-2 text-left text-2xl font-bold leading-7 text-white sm:truncate sm:text-3xl sm:tracking-tight">
						{page?.title ?? "N/A"}
					</h2>
				</div>
				<div className="mt-5 flex lg:ml-4 lg:mt-0">
					<span className="hidden sm:block">
						<Link to={"list"}>
							<button
								type="button"
								className="inline-flex items-center rounded-md bg-white/10 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-white/20"
							>
								<ClockIcon
									className="-ml-0.5 mr-1.5 h-5 w-5"
									aria-hidden="true"
								/>
								History
							</button>
						</Link>
					</span>

					<span className="ml-3 hidden sm:block">
						<button
							type="button"
							className="inline-flex items-center rounded-md bg-indigo-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500"
						>
							<PencilIcon
								className="-ml-0.5 mr-1.5 h-5 w-5"
								aria-hidden="true"
							/>
							Edit
						</button>
					</span>

					<span className="ml-3 hidden sm:block">
						<button
							type="button"
							className="inline-flex items-center rounded-md bg-indigo-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500"
							onClick={refreshChild}
						>
							<ArrowPathIcon
								className="-ml-0.5 mr-1.5 h-5 w-5"
								aria-hidden="true"
							/>
							Refresh
						</button>
					</span>



					<span className="ml-3 hidden sm:block">
						<button
							type="button"
							className="inline-flex items-center rounded-md bg-indigo-500 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500"
							onClick={purgeList}
						>
							<ArchiveBoxXMarkIcon
								className="-ml-0.5 mr-1.5 h-5 w-5"
								aria-hidden="true"
							/>
							Purge
						</button>
					</span>

					{/* Dropdown */}
					<Menu as="div" className="relative ml-3 sm:hidden">
						<Menu.Button className="inline-flex items-center rounded-md bg-white/10 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-white/20">
							More
							<ChevronDownIcon
								className="-mr-1 ml-1.5 h-5 w-5"
								aria-hidden="true"
							/>
						</Menu.Button>
						<Transition
							as={Fragment}
							enter="transition ease-out duration-200"
							enterFrom="transform opacity-0 scale-95"
							enterTo="transform opacity-100 scale-100"
							leave="transition ease-in duration-75"
							leaveFrom="transform opacity-100 scale-100"
							leaveTo="transform opacity-0 scale-95"
						>
							<Menu.Items className="absolute left-0 z-10 -ml-1 mt-2 w-48 origin-top-left rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
								<Menu.Item>
									{({ active }) => (
										<a
											href="#"
											className={classNames(
												active ? "bg-gray-100" : "",
												"block px-4 py-2 text-sm text-gray-700"
											)}
										>
											Edit
										</a>
									)}
								</Menu.Item>
								<Menu.Item>
									{({ active }) => (
										<a
											href="#"
											className={classNames(
												active ? "bg-gray-100" : "",
												"block px-4 py-2 text-sm text-gray-700"
											)}
										>
											History
										</a>
									)}
								</Menu.Item>
							</Menu.Items>
						</Transition>
					</Menu>
				</div>
			</div>

			<div className="mt-5">
				<PageMetrics key={refreshKey} />
			</div>
		</>
	);
};

export default PageComponent;
