"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const fs_1 = __importDefault(require("fs"));
const path_1 = __importDefault(require("path"));
const app = (0, express_1.default)();
const port = 3000;
const dbFilePath = path_1.default.join(__dirname, 'db.json');
app.use(body_parser_1.default.json());
// Middleware for handling JSON parsing errors
app.use((err, req, res, next) => {
    if (err instanceof SyntaxError && err.status === 400 && 'body' in err) {
        return res.status(400).send({ message: 'Invalid JSON' });
    }
    next();
});
// Load the database
function loadDatabase() {
    try {
        const data = fs_1.default.readFileSync(dbFilePath, 'utf8');
        return JSON.parse(data);
    }
    catch (err) {
        console.error('Error loading database:', err);
        return { submissions: [] };
    }
}
// Save the database
function saveDatabase(db) {
    try {
        fs_1.default.writeFileSync(dbFilePath, JSON.stringify(db, null, 2));
    }
    catch (err) {
        console.error('Error saving database:', err);
    }
}
// /ping endpoint
app.get('/ping', (req, res) => {
    res.send(true);
});
// /submit endpoint
app.post('/submit', (req, res) => {
    try {
        const { name, email, phone, github_link, stopwatch_time } = req.body;
        const db = loadDatabase();
        db.submissions.push({ name, email, phone, github_link, stopwatch_time });
        saveDatabase(db);
        res.status(201).send('Submission created');
    }
    catch (err) {
        console.error('Error processing /submit:', err);
        res.status(500).send('Internal server error');
    }
});
// /read endpoint (continued)
// /read endpoint
app.get('/read', (req, res) => {
    try {
        const index = parseInt(req.query.index);
        if (isNaN(index)) {
            return res.status(400).send('Invalid index');
        }
        const db = loadDatabase();
        if (index >= 0 && index < db.submissions.length) {
            res.json(db.submissions[index]);
        }
        else {
            res.status(404).send('Submission not found');
        }
    }
    catch (err) {
        console.error('Error processing /read:', err);
        res.status(500).send('Internal server error');
    }
});
// /delete endpoint
app.delete('/submission/:index', (req, res) => {
    try {
        const index = parseInt(req.params.index);
        if (isNaN(index)) {
            return res.status(400).send('Invalid index');
        }
        const db = loadDatabase();
        if (index >= 0 && index < db.submissions.length) {
            db.submissions.splice(index, 1);
            saveDatabase(db);
            res.status(200).send('Submission deleted');
        }
        else {
            res.status(404).send('Submission not found');
        }
    }
    catch (err) {
        console.error('Error processing /delete:', err);
        res.status(500).send('Internal server error');
    }
});
// /update endpoint
app.put('/submission/:index', (req, res) => {
    try {
        const index = parseInt(req.params.index);
        const { name, email, phone, github_link, stopwatch_time } = req.body;
        if (isNaN(index)) {
            return res.status(400).send('Invalid index');
        }
        const db = loadDatabase();
        if (index >= 0 && index < db.submissions.length) {
            db.submissions[index] = { name, email, phone, github_link, stopwatch_time };
            saveDatabase(db);
            res.status(200).send('Submission updated');
        }
        else {
            res.status(404).send('Submission not found');
        }
    }
    catch (err) {
        console.error('Error processing /update:', err);
        res.status(500).send('Internal server error');
    }
});
// /search endpoint
app.get('/search', (req, res) => {
    try {
        const email = req.query.email;
        if (!email) {
            return res.status(400).send('Email query parameter is required');
        }
        const db = loadDatabase();
        const results = db.submissions.filter((submission) => submission.email === email);
        if (results.length > 0) {
            res.json(results);
        }
        else {
            res.status(404).send('No submissions found for this email');
        }
    }
    catch (err) {
        console.error('Error processing /search:', err);
        res.status(500).send('Internal server error');
    }
});
app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
