<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_AutoRegularWithdrawal
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
        Me.Ctrl_AutoRegWithdrawal = New POSCommCtrl.Ctrl_AutoRegWithdrawal()
        Me.SuspendLayout()
        '
        'Ctrl_AutoRegWithdrawal
        '
        Me.Ctrl_AutoRegWithdrawal.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_AutoRegWithdrawal.Name = "Ctrl_AutoRegWithdrawal"
        Me.Ctrl_AutoRegWithdrawal.Size = New System.Drawing.Size(510, 300)
        Me.Ctrl_AutoRegWithdrawal.TabIndex = 0
        '
        'ctrl_AutoRegularWithdrawal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Ctrl_AutoRegWithdrawal)
        Me.Name = "ctrl_AutoRegularWithdrawal"
        Me.Size = New System.Drawing.Size(510, 300)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ctrl_AutoRegWithdrawal As POSCommCtrl.Ctrl_AutoRegWithdrawal

End Class
