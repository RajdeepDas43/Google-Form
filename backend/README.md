# Backend Server

This is the backend server for the Slidely Form App, built using TypeScript and Express.

## Features

- Save Submissions
- Retrieve Saved Submissions
- Delete Submissions
- Edit Submissions
- Search Submissions by Email

## Setup and Run

### Prerequisites

- Node.js
- npm

### Steps to Run

1. Clone the repository.
2. Navigate to the `backend` directory.
3. Run `npm install` to install dependencies.
4. Run `npm run build` to compile the TypeScript code.
5. Run `npm start` to start the server.

## Endpoints

- **GET /ping** - Always returns `true`.
- **POST /submit** - Accepts `name`, `email`, `phone`, `github_link`, and `stopwatch_time` as parameters and saves the submission.
- **GET /read** - Accepts an `index` query parameter and returns the corresponding submission.
- **DELETE /delete** - Accepts an `index` query parameter and deletes the corresponding submission.
- **PUT /edit** - Accepts an `index` query parameter and updates the corresponding submission with the provided data.
- **GET /search** - Accepts an `email` query parameter and returns all submissions matching the provided email.

## License

This project is licensed under the MIT License.
