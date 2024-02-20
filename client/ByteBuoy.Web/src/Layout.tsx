import { Outlet } from "react-router-dom";
import Header from "./components/Nav";
import Footer from "./components/Footer";
import { ToastContainer } from "react-toastify";

export default function Layout() {
	return (
		<>
			<Header />
			<main>
				<div className="mx-auto max-w-7xl sm:px-6 lg:px-8 dark:text-white">
					<Outlet /> {/* Child routes will be rendered here */}
				</div>
			</main>

			<ToastContainer />
			<Footer />
		</>
	);
}
