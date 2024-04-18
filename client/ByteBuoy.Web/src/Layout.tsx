import { Outlet } from "react-router-dom";
import Footer from "./components/Footer";
import { ToastContainer } from "react-toastify";
import Navbar from "./components/Navbar";

export default function Layout() {
	return (
		<>
			<div className="flex flex-col min-h-screen">
			<Navbar />
			
			<div className="flex-grow">
			<main>
				<div className="mx-auto max-w-6xl sm:px-7 md:px-6 lg:px-8 dark:text-white lg:py-3 "> 
					<Outlet /> {/* Child routes will be rendered here */}
				</div>
			</main>
			</div>

			<ToastContainer />
			<Footer />


			</div>
		</>
	);
}
