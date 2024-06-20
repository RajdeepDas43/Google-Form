Imports System.IO
Imports Newtonsoft.Json
Imports System.Windows.Forms

Public Class CreateSubmissionForm
    Private stopwatchTime As TimeSpan
    Private stopwatchRunning As Boolean = False

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If stopwatchRunning Then
            Timer1.Stop()
            stopwatchRunning = False
            btnStartStop.Text = "Start"
        Else
            Timer1.Start()
            stopwatchRunning = True
            btnStartStop.Text = "Stop"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1))
        lblStopwatchTime.Text = stopwatchTime.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim newSubmission As New Submission With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GitHubLink = txtGitHubLink.Text,
            .StopwatchTime = lblStopwatchTime.Text
        }

        Dim json As String = File.ReadAllText("SlidelyFormApp\\db.json")
        Dim submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
        submissions.Add(newSubmission)

        File.WriteAllText("SlidelyFormApp\\db.json", JsonConvert.SerializeObject(submissions, Formatting.Indented))

        MessageBox.Show("Submission saved!")
        Me.Close()
    End Sub

    ' Handle keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Control + Keys.S Then
            btnSubmit.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class