import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    strictPort: true,
    host: true, // bind 0.0.0.0 so the dev server is reachable from outside its Docker container
    watch: {
      // Bind-mounted files on Windows/macOS don't emit native filesystem
      // events inside the Linux container, so Vite's watcher never fires.
      // Polling works everywhere; only enabled in Docker (see VITE_DOCKER)
      // since it's more CPU-intensive than native watching.
      usePolling: process.env.VITE_DOCKER === 'true',
    },
  },
})
