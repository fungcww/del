<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackdoorReg
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
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.btnGenerateKey = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtGeneratedKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.hkLocation = New System.Windows.Forms.RadioButton()
        Me.moLocation = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(12, 18)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(130, 13)
        Me.lblPolicyNo.TabIndex = 0
        Me.lblPolicyNo.Text = "HKID/Passport/EB Ref Id"
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Location = New System.Drawing.Point(12, 93)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(78, 13)
        Me.lblMobileNo.TabIndex = 1
        Me.lblMobileNo.Text = "Generated Key"
        '
        'txtFullName
        '
        Me.txtFullName.Location = New System.Drawing.Point(148, 15)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(341, 20)
        Me.txtFullName.TabIndex = 2
        '
        'btnGenerateKey
        '
        Me.btnGenerateKey.Location = New System.Drawing.Point(515, 15)
        Me.btnGenerateKey.Name = "btnGenerateKey"
        Me.btnGenerateKey.Size = New System.Drawing.Size(129, 20)
        Me.btnGenerateKey.TabIndex = 7
        Me.btnGenerateKey.Text = "Generate Key"
        Me.btnGenerateKey.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(569, 63)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 20)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'txtGeneratedKey
        '
        Me.txtGeneratedKey.Location = New System.Drawing.Point(148, 86)
        Me.txtGeneratedKey.Name = "txtGeneratedKey"
        Me.txtGeneratedKey.ReadOnly = True
        Me.txtGeneratedKey.Size = New System.Drawing.Size(341, 20)
        Me.txtGeneratedKey.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Location"
        '
        'hkLocation
        '
        Me.hkLocation.AutoSize = True
        Me.hkLocation.Checked = True
        Me.hkLocation.Location = New System.Drawing.Point(148, 50)
        Me.hkLocation.Name = "hkLocation"
        Me.hkLocation.Size = New System.Drawing.Size(79, 17)
        Me.hkLocation.TabIndex = 16
        Me.hkLocation.TabStop = True
        Me.hkLocation.Text = "Hong Kong"
        Me.hkLocation.UseVisualStyleBackColor = True
        '
        'moLocation
        '
        Me.moLocation.AutoSize = True
        Me.moLocation.Location = New System.Drawing.Point(293, 50)
        Me.moLocation.Name = "moLocation"
        Me.moLocation.Size = New System.Drawing.Size(58, 17)
        Me.moLocation.TabIndex = 17
        Me.moLocation.Text = "Macau"
        Me.moLocation.UseVisualStyleBackColor = True
        '
        'frmBackdoorReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 175)
        Me.Controls.Add(Me.moLocation)
        Me.Controls.Add(Me.hkLocation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtGeneratedKey)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnGenerateKey)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.lblPolicyNo)
        Me.Name = "frmBackdoorReg"
        Me.Text = "Backdoor Registration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateKey As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtGeneratedKey As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents hkLocation As System.Windows.Forms.RadioButton
    Friend WithEvents moLocation As System.Windows.Forms.RadioButton
End Class
