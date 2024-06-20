' Form1.vb
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Imports System.Windows.Forms

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupDataGridView()
    End Sub

    Private Sub SetupDataGridView()
        With DataGridView1
            .Columns.Add("QuestionType", "Type")
            .Columns.Add("QuestionText", "Question")
            .Columns.Add("Options", "Options (Comma separated)")
            .Columns(0).Width = 100
            .Columns(1).Width = 250
            .Columns(2).Width = 150
        End With
    End Sub

    Private Sub btnAddQuestion_Click(sender As Object, e As EventArgs) Handles btnAddQuestion.Click
        DataGridView1.Rows.Add(cmbType.SelectedItem.ToString(), txtQuestionText.Text, txtOptions.Text)
        txtQuestionText.Clear()
        txtOptions.Clear()
    End Sub

    Private Sub btnDeleteQuestion_Click(sender As Object, e As EventArgs) Handles btnDeleteQuestion.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        End If
    End Sub

    Private Async Sub btnSaveForm_Click(sender As Object, e As EventArgs) Handles btnSaveForm.Click
        Dim form As New With {
            .Title = txtTitle.Text,
            .Questions = DataGridView1.Rows.Cast(Of DataGridViewRow)().
                         Select(Function(r) New With {
                             .Type = r.Cells("QuestionType").Value.ToString(),
                             .Text = r.Cells("QuestionText").Value.ToString(),
                             .Options = r.Cells("Options").Value.ToString().Split(","c)
                         }).ToList()
        }

        Using client As New HttpClient
            client.BaseAddress = New Uri("http://localhost:3000/")
            Dim content As New StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json")
            Dim result As HttpResponseMessage = Await client.PostAsync("forms", content)
            MessageBox.Show(Await result.Content.ReadAsStringAsync())
        End Using
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim preview As New FormPreview(txtTitle.Text, DataGridView1.Rows)
        preview.ShowDialog()
    End Sub
    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub
End Class
