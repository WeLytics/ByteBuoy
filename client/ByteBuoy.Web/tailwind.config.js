/** @type {import('tailwindcss').Config} */
import formsPlugin from '@tailwindcss/forms'

export default {
	darkMode: "selector",
	// darkMode: ['class', '[data-mode="dark"]'],
	content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
	theme: {
		extend: {
			// fontFamily: {
			//   sans: ['Inter var', ...defaultTheme.fontFamily.sans],
			// },
		},
	},
	plugins: [
		formsPlugin
	],
};
