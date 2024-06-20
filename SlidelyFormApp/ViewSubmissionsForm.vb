Imports System.IO
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Public Sub New()
        InitializeComponent()
        LoadSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub LoadSubmissions()
        Dim json As String = File.ReadAllText("SlidelyFormApp\\db.json")
        submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
    End Sub

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            lblName.Text = "Name: " & submission.Name
            lblEmail.Text = "Email: " & submission.Email
            lblPhone.Text = "Phone: " & submission.Phone
            lblGitHubLink.Text = "GitHub: " & submission.GitHubLink
            lblStopwatchTime.Text = "Stopwatch Time: " & submission.StopwatchTime
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

    ' Handle keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Left
                btnPrevious.PerformClick()
            Case Keys.Right
                btnNext.PerformClick()
            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
        Return True
    End Function
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String
End Class