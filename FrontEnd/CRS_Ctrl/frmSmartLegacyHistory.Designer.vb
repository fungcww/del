<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSmartLegacyHistory
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Ctrl_POS_SmartLegacyHist = New POSCommCtrl.Ctrl_POS_SmartLegacyHist()
        Me.SuspendLayout()
        '
        'Ctrl_POS_SmartLegacyHist
        '
        Me.Ctrl_POS_SmartLegacyHist.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_POS_SmartLegacyHist.Name = "Ctrl_POS_SmartLegacyHist"
        Me.Ctrl_POS_SmartLegacyHist.SetBeneRelation = Nothing
        Me.Ctrl_POS_SmartLegacyHist.SetContInsuredRelation = Nothing
        Me.Ctrl_POS_SmartLegacyHist.SetPolicyNumber = Nothing
        Me.Ctrl_POS_SmartLegacyHist.Size = New System.Drawing.Size(1037, 534)
        Me.Ctrl_POS_SmartLegacyHist.TabIndex = 0
        '
        'frmSmartLegacyHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1047, 546)
        Me.Controls.Add(Me.Ctrl_POS_SmartLegacyHist)
        Me.Name = "frmSmartLegacyHistory"
        Me.Text = "Smart Legacy History(COM 2 Bermuda)"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ctrl_POS_SmartLegacyHist As POSCommCtrl.Ctrl_POS_SmartLegacyHist
End Class
