Imports System.IO
Imports Newtonsoft.Json
Imports System.Windows.Forms

Public Class DeleteSubmissionForm
    Private submissions As List(Of Submission)

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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedIndex = lstSubmissions.SelectedIndex
        If selectedIndex >= 0 Then
            submissions.RemoveAt(selectedIndex)
            File.WriteAllText("SlidelyFormApp\\db.json", JsonConvert.SerializeObject(submissions, Formatting.Indented))
            MessageBox.Show("Submission deleted!")
            PopulateListBox()
        Else
            MessageBox.Show("Please select a submission to delete.")
        End If
    End Sub
End Class