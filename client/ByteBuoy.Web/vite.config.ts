import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 4200, // Set your desired default port
    strictPort: true // Set false, if you want Vite to try finding an available port if the default one is already in use
  }
})
