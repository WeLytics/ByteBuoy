const express = require('express');
const cors = require('cors');
const { createProxyMiddleware } = require('http-proxy-middleware');

const app = express();
const PORT = process.env.PORT || 99999;
const API_SERVICE_URL = process.env.API_SERVICE_URL || "http://invalid:5000"; 
const API_KEY = process.env.API_KEY; 
const CORS_ORIGIN = process.env.CORS_ORIGIN || 'http://localhost:3000'; 

if (!API_KEY) {
    console.error('API_KEY environment variable is not set.');
    process.exit(1);
}

// CORS Middleware
console.error('CORS_ORIGIN SET TO: ' + CORS_ORIGIN);
app.use(cors({
    origin: CORS_ORIGIN
}));

// Proxy endpoints
app.use('/proxy', createProxyMiddleware({
    target: API_SERVICE_URL,
    changeOrigin: true,
    pathRewrite: {
        [`^/proxy`]: '',
    },
    onProxyReq: (proxyReq, req, res) => {
        proxyReq.setHeader('x-api-key', API_KEY);
    },
}));

// Start the Proxy
app.listen(PORT, () => {
    console.log(`Starting Proxy at ${PORT}`);
});
