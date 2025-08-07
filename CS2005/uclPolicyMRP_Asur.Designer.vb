<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclPolicyMRP_Asur
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblMaxAllowedPrem = New System.Windows.Forms.Label()
        Me.gbMRP = New System.Windows.Forms.GroupBox()
        Me.lblOSMinReqPrem = New System.Windows.Forms.Label()
        Me.lblMinReqPrem = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbMRP.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Max Allow Prem :"
        '
        'lblMaxAllowedPrem
        '
        Me.lblMaxAllowedPrem.ForeColor = System.Drawing.Color.Black
        Me.lblMaxAllowedPrem.Location = New System.Drawing.Point(97, 26)
        Me.lblMaxAllowedPrem.Name = "lblMaxAllowedPrem"
        Me.lblMaxAllowedPrem.Size = New System.Drawing.Size(88, 13)
        Me.lblMaxAllowedPrem.TabIndex = 29
        Me.lblMaxAllowedPrem.Text = "333,333,333.33"
        Me.lblMaxAllowedPrem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbMRP
        '
        Me.gbMRP.Controls.Add(Me.Label3)
        Me.gbMRP.Controls.Add(Me.lblMaxAllowedPrem)
        Me.gbMRP.Controls.Add(Me.lblOSMinReqPrem)
        Me.gbMRP.Controls.Add(Me.lblMinReqPrem)
        Me.gbMRP.Controls.Add(Me.Label2)
        Me.gbMRP.Controls.Add(Me.Label1)
        Me.gbMRP.Location = New System.Drawing.Point(3, 5)
        Me.gbMRP.Name = "gbMRP"
        Me.gbMRP.Size = New System.Drawing.Size(191, 115)
        Me.gbMRP.TabIndex = 1
        Me.gbMRP.TabStop = False
        Me.gbMRP.Text = "Minimum Required Premium (MRP)"
        '
        'lblOSMinReqPrem
        '
        Me.lblOSMinReqPrem.ForeColor = System.Drawing.Color.Black
        Me.lblOSMinReqPrem.Location = New System.Drawing.Point(97, 85)
        Me.lblOSMinReqPrem.Name = "lblOSMinReqPrem"
        Me.lblOSMinReqPrem.Size = New System.Drawing.Size(88, 13)
        Me.lblOSMinReqPrem.TabIndex = 28
        Me.lblOSMinReqPrem.Text = "222,222,222.22"
        Me.lblOSMinReqPrem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMinReqPrem
        '
        Me.lblMinReqPrem.ForeColor = System.Drawing.Color.Black
        Me.lblMinReqPrem.Location = New System.Drawing.Point(97, 56)
        Me.lblMinReqPrem.Name = "lblMinReqPrem"
        Me.lblMinReqPrem.Size = New System.Drawing.Size(88, 13)
        Me.lblMinReqPrem.TabIndex = 3
        Me.lblMinReqPrem.Text = "111,111,111.11"
        Me.lblMinReqPrem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "OS. MRP :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Min Req Prem :"
        '
        'uclPolicyMRP_Asur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbMRP)
        Me.Name = "uclPolicyMRP_Asur"
        Me.Size = New System.Drawing.Size(197, 124)
        Me.gbMRP.ResumeLayout(False)
        Me.gbMRP.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lblMaxAllowedPrem As System.Windows.Forms.Label
    Friend WithEvents gbMRP As System.Windows.Forms.GroupBox
    Private WithEvents lblOSMinReqPrem As System.Windows.Forms.Label
    Private WithEvents lblMinReqPrem As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label

End Class
