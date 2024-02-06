import React, { useEffect, useState } from 'react';
import ReactDOM from "react-dom/client";
// import App from './App.tsx'
import "./index.css";
import { BrowserRouter as Router, Routes, Route, Navigate, useLocation,} from 'react-router-dom';
import ErrorPage from "./components/ErrorComponent";
import PageComponent from "./components/PageComponent";
import SetupComponent from "./components/SetupComponent";
// import App from './App';

const useCheckFirstRun = () => {
  const [isFirstRun, setIsFirstRun] = useState(null);

  useEffect(() => {
    const checkFirstRun = async () => {
      // Replace with your actual backend check
      const response = await fetch('/api/check-first-run');
      const data = await response.json();
      setIsFirstRun(data.isFirstRun);
    };

    checkFirstRun();
  }, []);

  return isFirstRun;
};


const CheckFirstRun = ({ children }) => {
  const isFirstRun = useCheckFirstRun();
  const location = useLocation();

  if (isFirstRun === null) {
    // Maybe return a loading spinner while waiting for the check
    return <div>Loading...</div>;
  }

  return isFirstRun && location.pathname !== '/setup' ? <Navigate to="/setup" replace /> : children;
};


const router = createBrowserRouter([
	{
		path: "/",
		element: <div>Home page</div>,
		errorElement: <ErrorPage />,
    loader: async () => {
      const isFirstRun = await checkFirstRun();
      if (isFirstRun) {
        return { redirect: '/setup' };
      }
    }
	},
  {
		path: "/setup",
		element: <SetupComponent />
	},
	{
		path: "pages/:pageId",
		element: <PageComponent />,
	},
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
       <Router>
      {isFirstRun && <Redirect to="/setup" />}
      <Switch>
        {/* Your routes go here */}
        <Route path="/setup">
          {/* Your setup component */}
        </Route>
        {/* ... other routes ... */}
      </Switch>
    </Router>

	</React.StrictMode>
);
