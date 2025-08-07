<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTranHist
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTranHist))
        Me.Ctrl_TranHist1 = New CRS_Ctrl.ctrl_TranHist
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPolicyNo = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Ctrl_TranHist1
        '
        Me.Ctrl_TranHist1.Location = New System.Drawing.Point(6, 35)
        Me.Ctrl_TranHist1.Name = "Ctrl_TranHist1"
        Me.Ctrl_TranHist1.PolicyNoInUse = ""
        Me.Ctrl_TranHist1.Size = New System.Drawing.Size(779, 489)
        Me.Ctrl_TranHist1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(170, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 22)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Policy No."
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.Location = New System.Drawing.Point(64, 12)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(100, 20)
        Me.txtPolicyNo.TabIndex = 4
        '
        'frmTranHist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 536)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPolicyNo)
        Me.Controls.Add(Me.Ctrl_TranHist1)
        Me.Name = "frmTranHist"
        Me.Text = "Tran Hist"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ctrl_TranHist1 As CRS_Ctrl.ctrl_TranHist
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
End Class
