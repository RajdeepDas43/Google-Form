"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const fs_1 = __importDefault(require("fs"));
const app = (0, express_1.default)();
const port = 3000;
app.use(body_parser_1.default.json());
app.get('/ping', (req, res) => {
    res.json(true);
});
app.post('/submit', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    const newSubmission = { name, email, phone, github_link, stopwatch_time };
    fs_1.default.readFile('src/db.json', (err, data) => {
        if (err)
            throw err;
        const submissions = JSON.parse(data.toString());
        submissions.push(newSubmission);
        fs_1.default.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
            if (err)
                throw err;
            res.json({ message: 'Submission saved!' });
        });
    });
});
app.get('/read', (req, res) => {
    const index = parseInt(req.query.index, 10);
    fs_1.default.readFile('src/db.json', (err, data) => {
        if (err)
            throw err;
        const submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            res.json(submissions[index]);
        }
        else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});
app.delete('/delete', (req, res) => {
    const index = parseInt(req.query.index, 10);
    fs_1.default.readFile('src/db.json', (err, data) => {
        if (err)
            throw err;
        let submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            submissions.splice(index, 1);
            fs_1.default.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
                if (err)
                    throw err;
                res.json({ message: 'Submission deleted!' });
            });
        }
        else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});
app.put('/edit', (req, res) => {
    const index = parseInt(req.query.index, 10);
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    fs_1.default.readFile('src/db.json', (err, data) => {
        if (err)
            throw err;
        let submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            submissions[index] = { name, email, phone, github_link, stopwatch_time };
            fs_1.default.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
                if (err)
                    throw err;
                res.json({ message: 'Submission updated!' });
            });
        }
        else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});
app.get('/search', (req, res) => {
    const email = req.query.email;
    fs_1.default.readFile('src/db.json', (err, data) => {
        if (err)
            throw err;
        const submissions = JSON.parse(data.toString());
        const results = submissions.filter((submission) => submission.email === email);
        res.json(results);
    });
});
app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
