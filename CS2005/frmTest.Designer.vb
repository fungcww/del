<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTest
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
        Me.btnTest = New System.Windows.Forms.Button()
        Me.tbEnvStr = New System.Windows.Forms.TextBox()
        Me.tbUserStr = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(118, 35)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'tbEnvStr
        '
        Me.tbEnvStr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbEnvStr.Location = New System.Drawing.Point(12, 12)
        Me.tbEnvStr.Name = "tbEnvStr"
        Me.tbEnvStr.Size = New System.Drawing.Size(100, 20)
        Me.tbEnvStr.TabIndex = 1
        Me.tbEnvStr.Text = "INGU105"
        '
        'tbUserStr
        '
        Me.tbUserStr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbUserStr.Location = New System.Drawing.Point(12, 38)
        Me.tbUserStr.Name = "tbUserStr"
        Me.tbUserStr.Size = New System.Drawing.Size(100, 20)
        Me.tbUserStr.TabIndex = 2
        Me.tbUserStr.Text = "UTPPM1"
        '
        'frmTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 152)
        Me.Controls.Add(Me.tbUserStr)
        Me.Controls.Add(Me.tbEnvStr)
        Me.Controls.Add(Me.btnTest)
        Me.Name = "frmTest"
        Me.Text = "frmTest"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnTest As Button
    Friend WithEvents tbEnvStr As TextBox
    Friend WithEvents tbUserStr As TextBox
End Class
