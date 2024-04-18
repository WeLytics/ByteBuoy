import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import PageComponent from "./pages/pages/PageMetric";
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
import TraceComponent from "./pages/pages/Trace";
import {App} from "./main";

export const AppWrapper = () => {
	return (
		<Router>
			<Routes>
				<Route element={<Layout />}>
					<Route path="/" element={<App />}>
						{/* Protected routes */}
						<Route index element={<Home />} />
					</Route>
					<Route path="metrics/:pageId" element={<PageComponent />} />
					<Route path="trace/:fileHash" element={<TraceComponent />} />
					<Route path="metrics/:pageId/list" element={<PageMetricsList />} />
					<Route path="metrics" element={<PagesComponent />} />
					<Route path="job/:jobId" element={<Job />} />
					<Route path="jobs/:pageId?" element={<JobsComponent />} />
					<Route path="login" element={<LoginPage />} />
					<Route path="profile" element={<ProfilePage />} />
					<Route path="*" element={<NotFound />} errorElement={<NotFound />} />
				</Route>
				<Route path="setup" element={<SetupComponent />} />
				{/* </Route> */}
			</Routes>
		</Router>
	);
};
