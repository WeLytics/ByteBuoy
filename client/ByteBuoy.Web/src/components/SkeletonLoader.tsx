export default function SkeletonLoader() {
	return (
		<div className="p-8 max-w-sm w-full mx-auto">
			<div className="animate-bounce flex space-x-4">
				{/* <div className="rounded-full bg-slate-700 h-10 w-10"></div> */}
				<div className="flex-1 space-y-6 py-1">
					{/* <div className="h-2 bg-slate-700 rounded"></div> */}
					<div className="space-y-3">
						<div className="grid grid-cols-3 gap-4">
							<div className="h-2 bg-slate-700 rounded col-span-3"></div>
							<div className="h-2 bg-slate-700 rounded col-span-3"></div>
							<div className="h-2 bg-slate-700 rounded col-span-3"></div>
						</div>
						<div className="h-2 bg-slate-700 rounded"></div>
					</div>
				</div>
			</div>
		</div>
	);
}