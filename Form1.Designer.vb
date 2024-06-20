' Form1.Designer.vb
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.txtQuestionText = New System.Windows.Forms.TextBox()
        Me.txtOptions = New System.Windows.Forms.TextBox()
        Me.btnAddQuestion = New System.Windows.Forms.Button()
        Me.btnDeleteQuestion = New System.Windows.Forms.Button()
        Me.btnSaveForm = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' Setup controls here...
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        ' Add controls to form
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.cmbType)
        Me.Controls.Add(Me.txtQuestionText)
        Me.Controls.Add(Me.txtOptions)
        Me.Controls.Add(Me.btnAddQuestion)
        Me.Controls.Add(Me.btnDeleteQuestion)
        Me.Controls.Add(Me.btnSaveForm)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.txtTitle)
        Me.Name = "Form1"
        Me.Text = "Form Builder"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents txtQuestionText As System.Windows.Forms.TextBox
    Friend WithEvents txtOptions As System.Windows.Forms.TextBox
    Friend WithEvents btnAddQuestion As System.Windows.Forms.Button
    Friend WithEvents btnDeleteQuestion As System.Windows.Forms.Button
    Friend WithEvents btnSaveForm As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
End Class
