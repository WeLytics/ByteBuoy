/** @type {import('tailwindcss').Config} */
// const defaultTheme = require('tailwindcss/defaultTheme')

export default {
  darkMode: 'selector',
  // darkMode: ['class', '[data-mode="dark"]'],
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      // fontFamily: {
      //   sans: ['Inter var', ...defaultTheme.fontFamily.sans],
      // },
    },
  },
  plugins: [],
}

