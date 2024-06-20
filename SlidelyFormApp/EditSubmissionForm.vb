Imports System.IO
Imports Newtonsoft.Json
Imports System.Windows.Forms

Public Class EditSubmissionForm
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = -1

    Public Sub New()
        InitializeComponent()
        LoadSubmissions()
        PopulateListBox()
    End Sub

    Private Sub LoadSubmissions()
        Dim json As String = File.ReadAllText("SlidelyFormApp\\db.json")
        submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
    End Sub

    Private Sub PopulateListBox()
        lstSubmissions.Items.Clear()
        For Each submission As Submission In submissions
            lstSubmissions.Items.Add(submission.Email)
        Next
    End Sub

    Private Sub lstSubmissions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSubmissions.SelectedIndexChanged
        currentIndex = lstSubmissions.SelectedIndex
        If currentIndex >= 0 Then
            Dim submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGitHubLink.Text = submission.GitHubLink
            lblStopwatchTime.Text = submission.StopwatchTime
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If currentIndex >= 0 Then
            submissions(currentIndex).Name = txtName.Text
            submissions(currentIndex).Email = txtEmail.Text
            submissions(currentIndex).Phone = txtPhone.Text
            submissions(currentIndex).GitHubLink = txtGitHubLink.Text
            submissions(currentIndex).StopwatchTime = lblStopwatchTime.Text

            File.WriteAllText("SlidelyFormApp\\db.json", JsonConvert.SerializeObject(submissions, Formatting.Indented))
            MessageBox.Show("Submission updated!")
        Else
            MessageBox.Show("Please select a submission to edit.")
        End If
    End Sub
End Class