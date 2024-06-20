' FormPreview.vbs
Imports System.Windows.Forms
Imports System.Drawing
Public Class FormPreview
    Public Sub New(title As String, questions As DataGridViewRowCollection)
        InitializeComponent()
        Me.Text = title
        Dim yPos As Integer = 10
        For Each question As DataGridViewRow In questions
            Dim lblQuestion As New Label With {
                .Text = question.Cells("QuestionText").Value.ToString(),
                .Location = New Point(10, yPos),
                .AutoSize = True
            }
            Me.Controls.Add(lblQuestion)
            yPos += 30
            If question.Cells("QuestionType").Value.ToString() = "Multiple Choice" Then
                For Each opt As String In question.Cells("Options").Value.ToString().Split(","c)
                    Dim rdoOption As New RadioButton With {
                        .Text = opt.Trim(),
                        .Location = New Point(20, yPos),
                        .AutoSize = True
                    }
                    Me.Controls.Add(rdoOption)
                    yPos += 20
                Next
            Else
                Dim txtAnswer As New TextBox With {
                    .Location = New Point(20, yPos),
                    .Width = 200
                }
                Me.Controls.Add(txtAnswer)
                yPos += 30
            End If
            yPos += 10  ' Add some spacing before the next question
        Next
    End Sub
End Class
