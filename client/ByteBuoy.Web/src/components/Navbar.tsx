import { Fragment } from "react";
import { Disclosure, Menu, Transition } from "@headlessui/react";
import { Bars3Icon, BellIcon, XMarkIcon } from "@heroicons/react/24/outline";
import { NavLink } from "react-router-dom";
import { classNames } from "../utils/utils";
import { postData } from "../services/apiService";
import { useAuth } from "../hooks/useAuth";


export default function Navbar() {
	const { isAuthenticated, logout } = useAuth();

	const navigation = [
		{ name: "Metrics", href: "metrics", current: true },
		...(isAuthenticated ? [{ name: "Jobs Runs", href: "jobs", current: false }] : []),
	];

	async function handleLogout() {
		logout();
		await postData("/logout", null);
		window.location.href = "/login";
	}

	return (
		<Disclosure as="nav" className="">
			{({ open }) => (
				<>
					<div className="mx-auto max-w-6xl px-2 sm:px-6 lg:px-8">
						<div className="relative flex h-16 items-center justify-between">
							<div className="absolute inset-y-0 left-0 flex items-center sm:hidden">
								{/* Mobile menu button*/}
								<Disclosure.Button className="relative inline-flex items-center justify-center rounded-md p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white">
									<span className="absolute -inset-0.5" />
									<span className="sr-only">
										Open main menu
									</span>
									{open ? (
										<XMarkIcon
											className="block h-6 w-6"
											aria-hidden="true"
										/>
									) : (
										<Bars3Icon
											className="block h-6 w-6"
											aria-hidden="true"
										/>
									)}
								</Disclosure.Button>
							</div>
							<div className="flex flex-1 items-center justify-center sm:items-stretch sm:justify-start">
								<div className="flex flex-shrink-0 items-center">
									<a href="/">
										<img
											className="h-8 w-auto"
											src="/logo.png"
											alt="ByteBuoy - File & Artefact Monitoring"
										/>
									</a>
								</div>
								<div className="hidden sm:ml-6 sm:block">
									<div className="flex space-x-4">
										{navigation.map((item) => (
											<NavLink
												to={item.href}
												key={item.name}
												className={({ isActive }) =>
													classNames(
														isActive
															? "bg-gray-900 text-white"
															: "text-gray-300 hover:bg-gray-700 hover:text-white",
														"rounded-md px-3 py-2 text-sm font-medium"
													)
												}
												aria-current={
													item.current
														? "page"
														: undefined
												}
											>
												{item.name}
											</NavLink>
										))}
									</div>
								</div>
							</div>
							<div className="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0">
								{isAuthenticated && (
									<button
										type="button"
										className="relative rounded-full bg-gray-800 p-1 text-gray-400 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
									>
										<span className="absolute -inset-1.5" />
										<span className="sr-only">
											View notifications
										</span>
										<BellIcon
											className="h-6 w-6"
											aria-hidden="true"
										/>
									</button>
								)}

								{!isAuthenticated && (
									<a
										href="/login"
										className="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
									>
										Login
									</a>

								)}
								
								{/* Profile dropdown */}
								<Menu as="div" className="relative ml-3">
									{isAuthenticated && ( 
										<div>
											<Menu.Button className="relative flex rounded-full bg-gray-800 text-sm focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
												<span className="absolute -inset-1.5" />
												<span className="sr-only">
													Open user menu
												</span>
												<span className="inline-block h-6 w-6 overflow-hidden rounded-full bg-gray-100">
													<svg
														className="h-full w-full text-gray-300"
														fill="currentColor"
														viewBox="0 0 24 24"
													>
														<path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
													</svg>
												</span>
											</Menu.Button>
										</div>
									)}
									<Transition
										as={Fragment}
										enter="transition ease-out duration-100"
										enterFrom="transform opacity-0 scale-95"
										enterTo="transform opacity-100 scale-100"
										leave="transition ease-in duration-75"
										leaveFrom="transform opacity-100 scale-100"
										leaveTo="transform opacity-0 scale-95"
									>
										<Menu.Items className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
											{!isAuthenticated && (
												<Menu.Item>
													{({ active }) => (
														<a
															href="/login"
															className={classNames(
																active
																	? "bg-gray-100"
																	: "",
																"block px-4 py-2 text-sm text-gray-700"
															)}
														>
															Login
														</a>
													)}
												</Menu.Item>
											)}
											{isAuthenticated && (
												<Menu.Item>
													{({ active }) => (
														<a
															href="/profile"
															className={classNames(
																active
																	? "bg-gray-100"
																	: "",
																"block px-4 py-2 text-sm text-gray-700"
															)}
														>
															Your Profile
														</a>
													)}
												</Menu.Item>
											)}
											{isAuthenticated && (
												<Menu.Item>
													{({ active }) => (
														<a
															href="#"
															onClick={() => {
																handleLogout();	
															}}
															className={classNames(
																active
																	? "bg-gray-100"
																	: "",
																"block px-4 py-2 text-sm text-gray-700"
															)}
														>
															Logout
														</a>
													)}
												</Menu.Item>
											)}
										</Menu.Items>
									</Transition>
								</Menu>
							</div>
						</div>
					</div>

					<Disclosure.Panel className="sm:hidden">
						<div className="space-y-1 px-2 pb-3 pt-2">
							{navigation.map((item) => (
								<NavLink
									to={item.href}
									key={item.name}
									className={({ isActive }) =>
										classNames(
											isActive
												? "bg-gray-900 text-white"
												: "text-gray-300 hover:bg-gray-700 hover:text-white",
												"block rounded-md px-3 py-2 text-base font-medium"
										)
									}
									aria-current={
										item.current
											? "page"
											: undefined
									}
								>
									{item.name}
								</NavLink>
							))}
						</div>
					</Disclosure.Panel>
				</>
			)}
		</Disclosure>
	);
}
