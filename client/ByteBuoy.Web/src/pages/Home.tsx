import React from "react";
import PagesList from "./pages/PagesList";

const Home: React.FC = () => {
	return (
		<div className="px-6 py-24 sm:py-32 lg:px-8">
			<div className="mx-auto max-w-2xl text-center">
				<h2 className="text-4xl font-bold tracking-tight text-white sm:text-6xl">
					ByteBuoy
				</h2>
				<p className="mt-6 text-lg leading-8 text-gray-300">
                    Open Source File & Artefact Monitoring
				</p>
			</div>

            <PagesList />
		</div>
	);
};

export default Home;
