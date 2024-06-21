# Slidely Form App

This is a Windows desktop application built with Visual Basic. The application allows users to create, view, edit, and delete form submissions. It also includes a stopwatch feature that can be paused and resumed without resetting.

## Features

- **Create New Submission:** Allows users to enter details such as Name, Email, Phone Number, GitHub repo link, and stopwatch time.
- **View Submissions:** Users can view all submissions one by one.
- **Edit Submission:** Users can edit the details of an existing submission.
- **Delete Submission:** Users can delete an existing submission.
- **Stopwatch:** A stopwatch feature that can be paused and resumed without resetting.
- **Keyboard Shortcuts:** Provides convenient keyboard shortcuts for quick operations.

## How to Use

### Prerequisites

- Visual Studio with Visual Basic support installed.
- .NET Framework installed on your Windows machine.

### Running the Application

1. **Clone the Repository:**
   ```sh
   git clone https://github.com/RajdeepDas43/Google-Form.git
   cd Google-Form
   ```

2. **Open the Project in Visual Studio:**
   - Launch Visual Studio.
   - Open the solution file `SlidelyFormApp.sln`.

3. **Build and Run the Application:**
   - Press `F5` or select `Start Debugging` from the `Debug` menu.

### Operating the Application

#### Main Window

When you run the application, the main window will have two buttons:

1. **View Submissions (CTRL + V):**
   - Click this button or press `CTRL + V` to open the "View Submissions" window.

2. **Create New Submission (CTRL + N):**
   - Click this button or press `CTRL + N` to open the "Create New Submission" window.

#### Create New Submission

In the "Create New Submission" window, you can:

- **Enter Details:**
  - Fill in the fields for Name, Email, Phone Number, and GitHub repo link.
  - The stopwatch will show the elapsed time.

- **Toggle Stopwatch (CTRL + T):**
  - Click the "TOGGLE STOPWATCH" button or press `CTRL + T` to start or pause the stopwatch.

- **Submit (CTRL + S):**
  - Click the "SUBMIT" button or press `CTRL + S` to submit the details to the backend server.

#### View Submissions

In the "View Submissions" window, you can:

- **Navigate Through Submissions:**
  - Click the "Previous (CTRL + P)" button or press `CTRL + P` to view the previous submission.
  - Click the "Next (CTRL + N)" button or press `CTRL + N` to view the next submission.

- **Edit Submission:**
  - Modify the details in the text fields and click the "Edit" button to update the submission.

- **Delete Submission:**
  - Click the "Delete" button to remove the current submission.

### Backend Integration

The application is connected to a backend server for storing and retrieving submissions. Ensure the backend server is running locally on `http://localhost:3000` before using the application.

### Error Handling

The application includes detailed error handling to manage connectivity issues and provide user feedback. If an error occurs during an operation, a message box will display the error details.

### Development Notes

- The stopwatch feature is implemented using a `Stopwatch` object and a `Timer` to update the elapsed time every second.
- The application uses asynchronous HTTP requests to communicate with the backend server.

### Keyboard Shortcuts

- **View Submissions:** `CTRL + V`
- **Create New Submission:** `CTRL + N`
- **Toggle Stopwatch:** `CTRL + T`
- **Submit Form:** `CTRL + S`
- **Previous Submission:** `CTRL + P`
- **Next Submission:** `CTRL + N`

### License

This project is licensed under the MIT License.
