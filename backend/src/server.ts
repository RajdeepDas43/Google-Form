import express, { Request, Response, NextFunction } from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import path from 'path';

declare const __dirname: string; // Add this line to declare __dirname

const app = express();
const port = 3000;
const dbFilePath = path.join(__dirname, 'db.json');

app.use(bodyParser.json());

// Middleware for handling JSON parsing errors
app.use((err: SyntaxError & { status?: number }, req: Request, res: Response, next: NextFunction) => {
  if (err instanceof SyntaxError && err.status === 400 && 'body' in err) {
    return res.status(400).send({ message: 'Invalid JSON' });
  }
  next();
});

// Load the database
function loadDatabase() {
  try {
    const data = fs.readFileSync(dbFilePath, 'utf8');
    return JSON.parse(data);
  } catch (err) {
    console.error('Error loading database:', err);
    return { submissions: [] };
  }
}

// Save the database
function saveDatabase(db: any) {
  try {
    fs.writeFileSync(dbFilePath, JSON.stringify(db, null, 2));
  } catch (err) {
    console.error('Error saving database:', err);
  }
}

// /ping endpoint
app.get('/ping', (req: Request, res: Response) => {
  res.send(true);
});

// /submit endpoint
app.post('/submit', (req: Request, res: Response) => {
  try {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    const db = loadDatabase();
    db.submissions.push({ name, email, phone, github_link, stopwatch_time });
    saveDatabase(db);
    res.status(201).send('Submission created');
  } catch (err) {
    console.error('Error processing /submit:', err);
    res.status(500).send('Internal server error');
  }
});

// /read endpoint (continued)
// /read endpoint
app.get('/read', (req: Request, res: Response) => {
  try {
    const index = parseInt(req.query.index as string);
    if (isNaN(index)) {
      return res.status(400).send('Invalid index');
    }
    const db = loadDatabase();
    if (index >= 0 && index < db.submissions.length) {
      res.json(db.submissions[index]);
    } else {
      res.status(404).send('Submission not found');
    }
  }
  catch (err) {
    console.error('Error processing /read:', err);
    res.status(500).send('Internal server error');
  }
});

// /delete endpoint
app.delete('/submission/:index', (req: Request, res: Response) => {
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
} else {
  res.status(404).send('Submission not found');
}
} catch (err) {
console.error('Error processing /delete:', err);
res.status(500).send('Internal server error');
}
});

// /update endpoint
app.put('/submission/:index', (req: Request, res: Response) => {
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
} else {
  res.status(404).send('Submission not found');
}
} catch (err) {
console.error('Error processing /update:', err);
res.status(500).send('Internal server error');
}
});

// /search endpoint
app.get('/search', (req: Request, res: Response) => {
try {
const email = req.query.email as string;
if (!email) {
  return res.status(400).send('Email query parameter is required');
}
const db = loadDatabase();
const results = db.submissions.filter((submission: any) => submission.email === email);
if (results.length > 0) {
  res.json(results);
} else {
  res.status(404).send('No submissions found for this email');
}
} catch (err) {
console.error('Error processing /search:', err);
res.status(500).send('Internal server error');
}
});

app.listen(port, () => {
console.log(`Server running at http://localhost:${port}`);
});