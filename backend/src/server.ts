import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';

const app = express();
const port = 3000;

app.use(bodyParser.json());

app.get('/ping', (req, res) => {
    res.json(true);
});

app.post('/submit', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    const newSubmission = { name, email, phone, github_link, stopwatch_time };

    fs.readFile('src/db.json', (err, data) => {
        if (err) throw err;
        const submissions = JSON.parse(data.toString());
        submissions.push(newSubmission);
        fs.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
            if (err) throw err;
            res.json({ message: 'Submission saved!' });
        });
    });
});

app.get('/read', (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    fs.readFile('src/db.json', (err, data) => {
        if (err) throw err;
        const submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            res.json(submissions[index]);
        } else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});

app.delete('/delete', (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    fs.readFile('src/db.json', (err, data) => {
        if (err) throw err;
        let submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            submissions.splice(index, 1);
            fs.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
                if (err) throw err;
                res.json({ message: 'Submission deleted!' });
            });
        } else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});

app.put('/edit', (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    fs.readFile('src/db.json', (err, data) => {
        if (err) throw err;
        let submissions = JSON.parse(data.toString());
        if (index >= 0 && index < submissions.length) {
            submissions[index] = { name, email, phone, github_link, stopwatch_time };
            fs.writeFile('src/db.json', JSON.stringify(submissions), (err) => {
                if (err) throw err;
                res.json({ message: 'Submission updated!' });
            });
        } else {
            res.status(404).json({ error: 'Submission not found' });
        }
    });
});

app.get('/search', (req, res) => {
    const email = req.query.email as string;
    fs.readFile('src/db.json', (err, data) => {
        if (err) throw err;
        const submissions = JSON.parse(data.toString());
        const results = submissions.filter((submission: any) => submission.email === email);
        res.json(results);
    });
});

app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
