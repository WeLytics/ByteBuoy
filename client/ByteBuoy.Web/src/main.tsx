import { useEffect, useState } from "react";
import { createRoot } from "react-dom/client";
import {
	BrowserRouter as Router,
	Routes,
	Route,
	useNavigate,
	Outlet,
} from "react-router-dom";
import "./index.css";
import PageComponent from "./pages/pages/Page";
import Job from "./pages/jobs/Job";
import JobsComponent from "./pages/jobs/Jobs";
import PagesComponent from "./pages/pages/Pages";
import SetupComponent from "./pages/Setup";
import LoginPage from "./pages/user/Login";
import Home from "./pages/Home";
import Layout from "./Layout";
import PageMetricsList from "./pages/pages/PageMetricsList";
import NotFound from "./pages/pages/NotFound";

const AppWrapper = () => {
	return (
		<Router>
			<Routes>
				<Route path="/" element={<App />}>
					<Route element={<Layout />}>
						{/* Protected routes */}
						<Route index element={<Home />} />
						<Route
							path="metrics/:pageId"
							element={<PageComponent />}
						/>
						<Route
							path="metrics/:pageId/list"
							element={<PageMetricsList />}
						/>
						<Route path="metrics" element={<PagesComponent />} />
						<Route path="jobs/:jobId" element={<Job />} />
						<Route path="jobs" element={<JobsComponent />} />
						<Route path="login" element={<LoginPage />} />

						{/* <Route path="*" element={< Navigate replace to="/check-first-run" />} /> */}
						<Route
							path="*"
							element={<NotFound />}
							errorElement={<NotFound />}
						/>
					</Route>
					<Route path="setup" element={<SetupComponent />} />
				</Route>
			</Routes>
		</Router>
	);
};

const App = () => {
	const navigate = useNavigate();
	const [isChecking, setIsChecking] = useState(true);
	const [isFirstRun, setIsFirstRun] = useState<boolean | null>(null);

	useEffect(() => {
		const checkFirstRun = async () => {
			try {
				// const response = await fetch('/api/isFirstRun');
				// const data = await response.json();

				setIsFirstRun(false);
				// setIsFirstRun(data.isFirstRun);
			} catch (error) {
				console.error("Failed to check first run status:", error);
				setIsFirstRun(false); // Assume it's not the first run if there's an error
			} finally {
				setIsChecking(false);
			}
		};

		checkFirstRun();
	}, []);

	useEffect(() => {
		if (!isChecking) {
			if (isFirstRun) {
				navigate("/setup");
			}
			// else {
			//   navigate('/');
			// }
		}
	}, [isChecking, isFirstRun, navigate]);

	if (isChecking) return <div>Checking application setup...</div>;
	return <Outlet />;
};

const container = document.getElementById("root");
const root = createRoot(container!); // Non-null assertion operator
root.render(<AppWrapper />);
