<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclQuestion
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblQuestionNo = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.rdoAns1 = New System.Windows.Forms.RadioButton
        Me.rdoAns2 = New System.Windows.Forms.RadioButton
        Me.rdoAns3 = New System.Windows.Forms.RadioButton
        Me.txtQuestion = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'lblQuestionNo
        '
        Me.lblQuestionNo.AutoSize = True
        Me.lblQuestionNo.Location = New System.Drawing.Point(3, 10)
        Me.lblQuestionNo.Name = "lblQuestionNo"
        Me.lblQuestionNo.Size = New System.Drawing.Size(24, 13)
        Me.lblQuestionNo.TabIndex = 0
        Me.lblQuestionNo.Text = "Q1."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Answer:"
        '
        'rdoAns1
        '
        Me.rdoAns1.AutoSize = True
        Me.rdoAns1.Location = New System.Drawing.Point(80, 106)
        Me.rdoAns1.Name = "rdoAns1"
        Me.rdoAns1.Size = New System.Drawing.Size(32, 17)
        Me.rdoAns1.TabIndex = 3
        Me.rdoAns1.Text = "Y"
        Me.rdoAns1.UseVisualStyleBackColor = True
        '
        'rdoAns2
        '
        Me.rdoAns2.AutoSize = True
        Me.rdoAns2.Location = New System.Drawing.Point(133, 106)
        Me.rdoAns2.Name = "rdoAns2"
        Me.rdoAns2.Size = New System.Drawing.Size(33, 17)
        Me.rdoAns2.TabIndex = 4
        Me.rdoAns2.Text = "U"
        Me.rdoAns2.UseVisualStyleBackColor = True
        '
        'rdoAns3
        '
        Me.rdoAns3.AutoSize = True
        Me.rdoAns3.Location = New System.Drawing.Point(188, 106)
        Me.rdoAns3.Name = "rdoAns3"
        Me.rdoAns3.Size = New System.Drawing.Size(68, 17)
        Me.rdoAns3.TabIndex = 5
        Me.rdoAns3.Text = "Negative"
        Me.rdoAns3.UseVisualStyleBackColor = True
        '
        'txtQuestion
        '
        Me.txtQuestion.BackColor = System.Drawing.SystemColors.Window
        Me.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuestion.Location = New System.Drawing.Point(79, 3)
        Me.txtQuestion.Multiline = True
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.ReadOnly = True
        Me.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtQuestion.Size = New System.Drawing.Size(686, 97)
        Me.txtQuestion.TabIndex = 1
        '
        'uclQuestion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.txtQuestion)
        Me.Controls.Add(Me.rdoAns3)
        Me.Controls.Add(Me.rdoAns2)
        Me.Controls.Add(Me.rdoAns1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblQuestionNo)
        Me.Name = "uclQuestion"
        Me.Size = New System.Drawing.Size(768, 141)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblQuestionNo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdoAns1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAns2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAns3 As System.Windows.Forms.RadioButton
    Friend WithEvents txtQuestion As System.Windows.Forms.TextBox

End Class
