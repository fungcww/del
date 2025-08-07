<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_AutoRegularWithdrawalHis
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
        Me.Ctrl_AutoRegWithdrawalHis = New POSCommCtrl.Ctrl_AutoRegWithdrawalHis()
        Me.SuspendLayout()
        '
        'Ctrl_AutoRegWithdrawalHis
        '
        Me.Ctrl_AutoRegWithdrawalHis.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_AutoRegWithdrawalHis.Name = "Ctrl_AutoRegWithdrawalHis"
        Me.Ctrl_AutoRegWithdrawalHis.PolicyNoBasic = Nothing
        Me.Ctrl_AutoRegWithdrawalHis.PolicyNoDependent = Nothing
        Me.Ctrl_AutoRegWithdrawalHis.Size = New System.Drawing.Size(1080, 400)
        Me.Ctrl_AutoRegWithdrawalHis.TabIndex = 0
        '
        'ctrl_AutoRegularWithdrawalHis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Ctrl_AutoRegWithdrawalHis)
        Me.Name = "ctrl_AutoRegularWithdrawalHis"
        Me.Size = New System.Drawing.Size(1080, 400)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Ctrl_AutoRegWithdrawalHis As POSCommCtrl.Ctrl_AutoRegWithdrawalHis

End Class
