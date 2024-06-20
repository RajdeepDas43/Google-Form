Imports System.IO
Imports Newtonsoft.Json
Imports System.Windows.Forms

Public Class MainForm
    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub btnDeleteSubmission_Click(sender As Object, e As EventArgs) Handles btnDeleteSubmission.Click
        Dim deleteForm As New DeleteSubmissionForm()
        deleteForm.Show()
    End Sub

    Private Sub btnEditSubmission_Click(sender As Object, e As EventArgs) Handles btnEditSubmission.Click
        Dim editForm As New EditSubmissionForm()
        editForm.Show()
    End Sub

    ' Handle keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.F1
                btnViewSubmissions.PerformClick()
            Case Keys.F2
                btnCreateSubmission.PerformClick()
            Case Keys.F3
                btnDeleteSubmission.PerformClick()
            Case Keys.F4
                btnEditSubmission.PerformClick()
            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
        Return True
    End Function
End Class