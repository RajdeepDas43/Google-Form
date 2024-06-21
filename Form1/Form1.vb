Imports System.Net.Http
Imports System.Net.Http.Json
Imports System.Diagnostics
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    ' Declare buttons with WithEvents keyword
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateSubmission As Button
    Private submissions As List(Of Submission) = New List(Of Submission)
    Private currentIndex As Integer = 0
    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhoneNum As TextBox
    Private WithEvents txtGithubLink As TextBox
    Private WithEvents lblStopwatchTime As Label
    Private WithEvents btnToggleStopwatch As Button
    Private WithEvents btnSubmit As Button
    Private WithEvents btnDelete As Button
    Private WithEvents btnEdit As Button
    Private stopwatch As New Stopwatch()
    Private stopwatchTimer As Timer

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent() ' Keep this call to the designer-generated method

        ' Add any initialization after the InitializeComponent() call.
        Me.KeyPreview = True

        ' Initialize main form buttons
        btnViewSubmissions = New Button With {
            .Text = "View Submissions (CTRL + V)",
            .Location = New Point(10, 10),
            .Size = New Size(200, 50)
        }
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click

        btnCreateSubmission = New Button With {
            .Text = "Create New Submission (CTRL + N)",
            .Location = New Point(10, 70),
            .Size = New Size(200, 50)
        }
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click

        ' Add buttons to the form
        Me.Controls.Add(btnViewSubmissions)
        Me.Controls.Add(btnCreateSubmission)

        ' Apply basic styling to the form
        Me.Text = "Slidely Form App"
        Me.BackColor = Color.LightBlue

        ' Initialize the timer
        stopwatchTimer = New Timer()
        AddHandler stopwatchTimer.Tick, AddressOf UpdateStopwatchLabel
        stopwatchTimer.Interval = 1000 ' Update the stopwatch label every second
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        ' Clear previous controls
        Me.Controls.Clear()
        InitializeComponent()

        ' Initialize viewing controls
        Dim lblName As New Label With {.Text = "Name", .Location = New Point(10, 10)}
        txtName = New TextBox With {.Location = New Point(150, 10), .Width = 200}
        Dim lblEmail As New Label With {.Text = "Email", .Location = New Point(10, 40)}
        txtEmail = New TextBox With {.Location = New Point(150, 40), .Width = 200}
        Dim lblPhoneNum As New Label With {.Text = "Phone Num", .Location = New Point(10, 70)}
        txtPhoneNum = New TextBox With {.Location = New Point(150, 70), .Width = 200}
        Dim lblGithubLink As New Label With {.Text = "Github Link For Task 2", .Location = New Point(10, 100)}
        txtGithubLink = New TextBox With {.Location = New Point(150, 100), .Width = 200}
        Dim lblStopwatchTime As New Label With {.Text = "Stopwatch time", .Location = New Point(10, 130)}
        lblStopwatchTime = New Label With {.Location = New Point(150, 130), .Width = 200}

        Dim btnPrevious As New Button With {
            .Text = "Previous (CTRL + P)",
            .Location = New Point(10, 160),
            .Size = New Size(100, 50)
        }
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click

        Dim btnNext As New Button With {
            .Text = "Next (CTRL + N)",
            .Location = New Point(120, 160),
            .Size = New Size(100, 50)
        }
        AddHandler btnNext.Click, AddressOf btnNext_Click

        btnDelete = New Button With {
            .Text = "Delete",
            .Location = New Point(230, 160),
            .Size = New Size(100, 50)
        }
        AddHandler btnDelete.Click, AddressOf btnDelete_Click

        btnEdit = New Button With {
            .Text = "Edit",
            .Location = New Point(340, 160),
            .Size = New Size(100, 50)
        }
        AddHandler btnEdit.Click, AddressOf btnEdit_Click

        ' Add controls to the form
        Me.Controls.Add(lblName)
        Me.Controls.Add(txtName)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(lblPhoneNum)
        Me.Controls.Add(txtPhoneNum)
        Me.Controls.Add(lblGithubLink)
        Me.Controls.Add(txtGithubLink)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(btnPrevious)
        Me.Controls.Add(btnNext)
        Me.Controls.Add(btnDelete)
        Me.Controls.Add(btnEdit)

        ' Load submissions from backend
        LoadSubmissions()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        ' Clear previous controls
        Me.Controls.Clear()
        InitializeComponent()

        ' Initialize creating controls
        Dim lblName As New Label With {.Text = "Name", .Location = New Point(10, 10)}
        txtName = New TextBox With {.Location = New Point(150, 10), .Width = 200}
        Dim lblEmail As New Label With {.Text = "Email", .Location = New Point(10, 40)}
        txtEmail = New TextBox With {.Location = New Point(150, 40), .Width = 200}
        Dim lblPhoneNum As New Label With {.Text = "Phone Num", .Location = New Point(10, 70)}
        txtPhoneNum = New TextBox With {.Location = New Point(150, 70), .Width = 200}
        Dim lblGithubLink As New Label With {.Text = "Github Link For Task 2", .Location = New Point(10, 100)}
        txtGithubLink = New TextBox With {.Location = New Point(150, 100), .Width = 200}
        Dim lblStopwatchTime As New Label With {.Text = "Stopwatch time", .Location = New Point(10, 130)}
        lblStopwatchTime = New Label With {.Location = New Point(150, 130), .Width = 200}

        btnToggleStopwatch = New Button With {
            .Text = "TOGGLE STOPWATCH (CTRL + T)",
            .Location = New Point(10, 160),
            .Size = New Size(200, 50)
        }
        AddHandler btnToggleStopwatch.Click, AddressOf btnToggleStopwatch_Click

        btnSubmit = New Button With {
            .Text = "SUBMIT (CTRL + S)",
            .Location = New Point(10, 220),
            .Size = New Size(200, 50)
        }
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click

        ' Add controls to the form
        Me.Controls.Add(lblName)
        Me.Controls.Add(txtName)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(lblPhoneNum)
        Me.Controls.Add(txtPhoneNum)
        Me.Controls.Add(lblGithubLink)
        Me.Controls.Add(txtGithubLink)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(btnToggleStopwatch)
        Me.Controls.Add(btnSubmit)

        ' Apply styling to the controls
        txtName.BackColor = Color.LightYellow
        txtEmail.BackColor = Color.LightYellow
        txtPhoneNum.BackColor = Color.LightYellow
        txtGithubLink.BackColor = Color.LightYellow
        lblStopwatchTime.BackColor = Color.LightYellow
    End Sub

    Private Async Sub LoadSubmissions()
        ' Load submissions from backend
        Dim client As New HttpClient()
        Try
            Dim response = Await client.GetFromJsonAsync(Of List(Of Submission))("http://localhost:3000/submissions")
            If response IsNot Nothing Then
                submissions = response
                currentIndex = 0
                DisplayCurrentSubmission()
            Else
                MessageBox.Show("No submissions found.")
            End If
        Catch ex As HttpRequestException
            MessageBox.Show("Error connecting to the server: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplayCurrentSubmission()
        ' Display the current submission details in the text boxes
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhoneNum.Text = submission.PhoneNum
            txtGithubLink.Text = submission.GithubLink
            lblStopwatchTime.Text = submission.StopwatchTime
        Else
            txtName.Text = ""
            txtEmail.Text = ""
            txtPhoneNum.Text = ""
            txtGithubLink.Text = ""
            lblStopwatchTime.Text = ""
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplayCurrentSubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplayCurrentSubmission()
        End If
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs)
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim client As New HttpClient()
            Try
                Dim response = Await client.DeleteAsync($"http://localhost:3000/submission/{currentIndex}")
                If response.IsSuccessStatusCode Then
                    submissions.RemoveAt(currentIndex)
                    If currentIndex >= submissions.Count Then
                        currentIndex -= 1
                    End If
                    DisplayCurrentSubmission()
                    MessageBox.Show("Submission deleted successfully")
                Else
                    MessageBox.Show("Error deleting submission")
                End If
            Catch ex As HttpRequestException
                MessageBox.Show("Error connecting to the server: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Error deleting submission: " & ex.Message)
            End Try
        End If
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs)
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim client As New HttpClient()
            Try
                Dim submission = New Submission With {
                    .Name = txtName.Text,
                    .Email = txtEmail.Text,
                    .PhoneNum = txtPhoneNum.Text,
                    .GithubLink = txtGithubLink.Text,
                    .StopwatchTime = lblStopwatchTime.Text
                }
                Dim response = Await client.PutAsJsonAsync($"http://localhost:3000/submission/{currentIndex}", submission)
                If response.IsSuccessStatusCode Then
                    submissions(currentIndex) = submission
                    MessageBox.Show("Submission updated successfully")
                Else
                    MessageBox.Show("Error updating submission")
                End If
            Catch ex As HttpRequestException
                MessageBox.Show("Error connecting to the server: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Error updating submission: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
            stopwatchTimer.Start()
        End If
        UpdateStopwatchLabel()
    End Sub

    Private Sub UpdateStopwatchLabel()
        If lblStopwatchTime IsNot Nothing Then
            lblStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        End If
    End Sub

    Private Sub UpdateStopwatchLabel(sender As Object, e As EventArgs)
        UpdateStopwatchLabel()
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim submission As New Submission With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .PhoneNum = txtPhoneNum.Text,
            .GithubLink = txtGithubLink.Text,
            .StopwatchTime = lblStopwatchTime.Text
        }

            Dim client As New HttpClient()
            Dim response = Await client.PostAsJsonAsync("http://localhost:3000/submit", submission)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission created successfully")
            Else
                MessageBox.Show("Error creating submission: " & response.ReasonPhrase)
            End If
        Catch ex As HttpRequestException
            MessageBox.Show("Error connecting to the server: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Error creating submission: " & ex.Message)
            ' Display detailed exception information
            MessageBox.Show("Exception details: " & ex.ToString())
        End Try
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub

    Public Class Submission
        Public Property Name As String
        Public Property Email As String
        Public Property PhoneNum As String
        Public Property GithubLink As String
        Public Property StopwatchTime As String
    End Class
End Class