import React from "react";
import {PaginationMeta} from "../types/PaginationMeta";
import {ChevronLeftIcon, ChevronRightIcon} from "@heroicons/react/20/solid";

interface PaginationProps {
	paginationMeta: PaginationMeta;
	onNavigate: (page: number) => void;
	onPrevious: () => void;
	onNext: () => void;
}

const Pagination: React.FC<PaginationProps> = ({
	paginationMeta,
	onNavigate,
	onPrevious,
	onNext,
}) => {
	// Helper function to generate page numbers with ellipses
	const getPageNumbers = () => {
		const {currentPage, totalPages} = paginationMeta;
		const delta = 2; // number of pages before and after the current page
		const range = [];

		for (
			let i = Math.max(2, currentPage - delta);
			i <= Math.min(totalPages - 1, currentPage + delta);
			i++
		) {
			range.push(i);
		}

		if (currentPage - delta > 2) {
			range.unshift("...");
		}
		if (currentPage + delta < totalPages - 1) {
			range.push("...");
		}

		range.unshift(1);
		if (totalPages !== 1) {
			range.push(totalPages);
		}

		return range;
	};

	return (
		<div className="">
			<div className="flex items-center justify-between border-t border-gray-200 bg-dark text-white px-4 py-3 sm:px-6 mt-10">
				<div className="flex flex-1 justify-between sm:hidden">
					<a
						href="#"
						onClick={() => onPrevious()}
						className="relative inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
					>
						Previous
					</a>
					<a
						href="#"
						onClick={() => onNext()}
						className="relative ml-3 inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
					>
						Next
					</a>
				</div>
				<div className="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
					<div>
						<p className="text-sm text-gray-700">
							Total of {" "} 
							<span className="font-medium">
								{paginationMeta.totalItems}
							</span>{" "}
							results
						</p>
					</div>
					<div>
						<nav
							className="isolate inline-flex -space-x-px rounded-md shadow-sm"
							aria-label="Pagination"
						>
							<a
								href="#"
								onClick={() => onPrevious()}
								className="relative cursor-pointer inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
							>
								<span className="sr-only">Previous</span>
								<ChevronLeftIcon className="h-5 w-5" aria-hidden="true" />
							</a>

                            {getPageNumbers().map((page, index) =>
                                typeof page === "number" ? (
                                    <a
                                        key={index}
                                        href="#"
                                        onClick={(e) => { e.preventDefault(); onNavigate(page); }}
                                        className={`relative cursor-pointer z-10 inline-flex items-center px-4 py-2 text-sm font-semibold text-white focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 ${
                                            page === paginationMeta.currentPage ? "bg-blue-500" : "hover:bg-blue-400"
                                        }`}
                                    >
                                        {page}
                                    </a>
                                ) : (
                                    <span key={index} className="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-white">
                                        {page}
                                    </span>
                                )
                            )}

							{/* Current: "z-10 bg-indigo-600 text-white focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600", Default: "text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:outline-offset-0" */}
							<a
								href="#"
								onClick={() => onNext()}
								className="relative cursor-pointer inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
							>
								<span className="sr-only">Next</span>
								<ChevronRightIcon
									className="h-5 w-5"
									aria-hidden="true"
								/>
							</a>
						</nav>
					</div>
				</div>
			</div>
		</div>
	);
};

export default Pagination;
