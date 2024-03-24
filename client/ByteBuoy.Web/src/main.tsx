import {useEffect, useState} from "react";
import {createRoot} from "react-dom/client";
import {useNavigate, Outlet} from "react-router-dom";
import "./index.css";
import {fetchData} from "./services/apiService";
import {AppWrapper} from "./AppWrapper";

export const App = () => {
	const navigate = useNavigate();
	const [isChecking, setIsChecking] = useState(true);
	const [isFirstRun, setIsFirstRun] = useState<boolean | null>(null);

	useEffect(() => {
		const checkFirstRun = async () => {
			try {
				const response = await fetchData<boolean>("/api/v1/system/isFirstRun");

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
