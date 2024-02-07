import React, { useEffect, useState } from 'react';
import { createRoot } from 'react-dom/client';
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
  useNavigate,
  Outlet,
} from 'react-router-dom';
import './index.css';
import PageComponent from './components/PageComponent';
import JobComponent from './components/JobComponent';
import JobsComponent from './components/JobsComponent';
import PagesComponent from './components/PagesComponent';
import ErrorComponent from './components/ErrorComponent';


const SetupPage = () => (
  <div>Setup Page - follow instructions to set up your application.</div>
);

const HomePage = () => (
  <div>Welcome to the application!</div>
);

const AppWrapper = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<App />}>
          {/* Protected routes */}
          <Route index element={<HomePage />} />
          <Route path="setup" element={<SetupPage />} />
          <Route path="pages/:pageId" element={<PageComponent />} />
          <Route path="pages" element={<PagesComponent />} />
          <Route path="jobs/:jobId" element={<JobComponent />} />
          <Route path="jobs" element={<JobsComponent />} />

          {/* <Route path="*" element={< Navigate replace to="/check-first-run" />} /> */}
          <Route path="*" errorElement={<ErrorComponent />} />
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
        console.error('Failed to check first run status:', error);
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
        navigate('/setup');
      } 
      // else {
      //   navigate('/');
      // }
    }
  }, [isChecking, isFirstRun, navigate]);

  if (isChecking) return <div>Checking application setup...</div>;
  return <Outlet />;
};

const container = document.getElementById('root');
const root = createRoot(container!); // Non-null assertion operator
root.render(<AppWrapper />);
