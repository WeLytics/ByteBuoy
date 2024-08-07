type EmptyMetricsStateProps = {
	pageIdOrSlug: string;
};

/**
 * Renders a component representing the empty state of metrics.
 * @returns JSX.Element
 */
export function EmptyMetricsState({pageIdOrSlug}: EmptyMetricsStateProps) {
	return (
		<div className="relative mt-6 block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2">
			<svg
				className="mx-auto h-12 w-12 text-gray-400"
				stroke="currentColor"
				fill="none"
				viewBox="0 0 48 48"
				aria-hidden="true"
			>
				<path
					strokeLinecap="round"
					strokeLinejoin="round"
					strokeWidth={2}
					d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6"
				/>
			</svg>
			<span className="mt-2 block text-sm font-semibold text-white">
				No Metrics available
			</span>

			<span className="mt-2 block text-sm text-white">
				Send metrics by using the Page Id or Slug <pre className="bg-gray-900 text-white p-1 rounded-lg inline-block"><code>{pageIdOrSlug}</code></pre>
			</span>

			<div className="mt-10 flex items-center justify-center gap-x-6">
				<a
					href="https://welytics.github.io/ByteBuoy/"
					target="_blank"
					className="rounded-md bg-indigo-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
				>
					See Documentation
				</a>
			</div>
		</div>
	);
}
