const express = require('express');
const app = express();
const port = 5000;

// Middleware to log all requests
app.use((req, res, next) => {
    console.log(`Received request for ${req.method} ${req.path}`);
    next(); // Continue to the next middleware or route handler
});

// Catch-all route handler for any type of request
app.all('*', (req, res) => {
    res.send({
        message: `You requested ${req.method} ${req.path}`,
        headers: req.headers
    });
});

app.listen(port, () => {
    console.log(`Server listening at http://localhost:${port}`);
});
