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
import ProfilePage from "./pages/user/Profile";
import { fetchData } from "./services/apiService";

const AppWrapper = () => {
	return (
		<Router>
			<Routes>
				<Route element={<Layout />}>
					<Route path="/" element={<App />} >
					{/* Protected routes */}
						<Route index element={<Home />} />
					</Route>
					<Route path="metrics/:pageId" element={<PageComponent />} />
					<Route
						path="metrics/:pageId/list"
						element={<PageMetricsList />}
					/>
					<Route path="metrics" element={<PagesComponent />} />
					<Route path="jobs/:jobId" element={<Job />} />
					<Route path="jobs" element={<JobsComponent />} />
					<Route path="login" element={<LoginPage />} />
					<Route path="profile" element={<ProfilePage />} />
					<Route
						path="*"
						element={<NotFound />}
						errorElement={<NotFound />}
					/>
				</Route>
				<Route path="setup" element={<SetupComponent />} />
				{/* </Route> */}
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
				const response = await fetchData("/api/v1/system/isFirstRun");

				setIsFirstRun(response);
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
		}
	}, [isChecking, isFirstRun, navigate]);

	if (isChecking) return <div>Loading application...</div>;
	return <Outlet />;
};

const container = document.getElementById("root");
const root = createRoot(container!); // Non-null assertion operator
root.render(<AppWrapper />);
