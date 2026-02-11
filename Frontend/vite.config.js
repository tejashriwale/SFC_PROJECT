// vite.config.js
import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => {
  // Load env file based on mode
  const env = loadEnv(mode, process.cwd(), '');

  return {
    define: {
      global: 'window',
    },
    base: '/',
    publicDir: 'public',
    server: {
      open: true,
      proxy: {
        '/backend': {
          target: env.VITE_API_URL || 'http://localhost:5200',
          changeOrigin: true,
          secure: false,
          rewrite: (path) => path.replace(/^\/backend/, '')
        }
      }
    }
  };
});
