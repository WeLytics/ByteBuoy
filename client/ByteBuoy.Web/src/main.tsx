import React from 'react'
import ReactDOM from 'react-dom/client'
// import App from './App.tsx'
import './index.css'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import ErrorPage from "./error-page";
import PageComponent from './components/PageComponent';
// import App from './App';


const router = createBrowserRouter([
  {
    path: "/",
    element: <div>Home page</div>,
    errorElement: <ErrorPage />,
  },
        {path: "pages/:pageId",
        element: <PageComponent />,
        // element: <App />,
      },
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    {/* <App /> */}
    <RouterProvider router={router} />
  </React.StrictMode>,
)
