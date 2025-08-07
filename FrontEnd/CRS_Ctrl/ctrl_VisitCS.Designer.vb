<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_VisitCS
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblCustVer = New System.Windows.Forms.Label
        Me.lblUpdDate = New System.Windows.Forms.Label
        Me.cboCsFlag = New System.Windows.Forms.ComboBox
        Me.lblUpdDateValue = New System.Windows.Forms.Label
        Me.btnChange = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblUpdUserValue = New System.Windows.Forms.Label
        Me.lblUpdUser = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCustVer
        '
        Me.lblCustVer.AutoSize = True
        Me.lblCustVer.Location = New System.Drawing.Point(6, 24)
        Me.lblCustVer.MaximumSize = New System.Drawing.Size(150, 0)
        Me.lblCustVer.Name = "lblCustVer"
        Me.lblCustVer.Size = New System.Drawing.Size(127, 26)
        Me.lblCustVer.TabIndex = 0
        Me.lblCustVer.Text = "Face to face customer's identity verification done?"
        '
        'lblUpdDate
        '
        Me.lblUpdDate.AutoSize = True
        Me.lblUpdDate.Location = New System.Drawing.Point(6, 59)
        Me.lblUpdDate.MaximumSize = New System.Drawing.Size(150, 0)
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.Size = New System.Drawing.Size(66, 13)
        Me.lblUpdDate.TabIndex = 2
        Me.lblUpdDate.Text = "Update date"
        '
        'cboCsFlag
        '
        Me.cboCsFlag.Enabled = False
        Me.cboCsFlag.FormattingEnabled = True
        Me.cboCsFlag.Items.AddRange(New Object() {"Y", "N", "N/A"})
        Me.cboCsFlag.Location = New System.Drawing.Point(150, 24)
        Me.cboCsFlag.Name = "cboCsFlag"
        Me.cboCsFlag.Size = New System.Drawing.Size(47, 21)
        Me.cboCsFlag.TabIndex = 3
        Me.cboCsFlag.Text = "N/A"
        '
        'lblUpdDateValue
        '
        Me.lblUpdDateValue.AutoSize = True
        Me.lblUpdDateValue.Location = New System.Drawing.Point(122, 59)
        Me.lblUpdDateValue.Name = "lblUpdDateValue"
        Me.lblUpdDateValue.Size = New System.Drawing.Size(75, 13)
        Me.lblUpdDateValue.TabIndex = 4
        Me.lblUpdDateValue.Text = "YYYY-MM-DD"
        '
        'btnChange
        '
        Me.btnChange.Enabled = False
        Me.btnChange.Location = New System.Drawing.Point(6, 110)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(75, 23)
        Me.btnChange.TabIndex = 5
        Me.btnChange.Text = "Change"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Enabled = False
        Me.btnUpdate.Location = New System.Drawing.Point(122, 110)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblUpdUserValue)
        Me.GroupBox1.Controls.Add(Me.lblUpdUser)
        Me.GroupBox1.Controls.Add(Me.cboCsFlag)
        Me.GroupBox1.Controls.Add(Me.lblUpdDateValue)
        Me.GroupBox1.Controls.Add(Me.lblCustVer)
        Me.GroupBox1.Controls.Add(Me.btnUpdate)
        Me.GroupBox1.Controls.Add(Me.btnChange)
        Me.GroupBox1.Controls.Add(Me.lblUpdDate)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(213, 142)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Identity Verification"
        '
        'lblUpdUserValue
        '
        Me.lblUpdUserValue.AutoSize = True
        Me.lblUpdUserValue.Location = New System.Drawing.Point(122, 85)
        Me.lblUpdUserValue.Name = "lblUpdUserValue"
        Me.lblUpdUserValue.Size = New System.Drawing.Size(53, 13)
        Me.lblUpdUserValue.TabIndex = 8
        Me.lblUpdUserValue.Text = "username"
        '
        'lblUpdUser
        '
        Me.lblUpdUser.AutoSize = True
        Me.lblUpdUser.Location = New System.Drawing.Point(5, 85)
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.Size = New System.Drawing.Size(67, 13)
        Me.lblUpdUser.TabIndex = 7
        Me.lblUpdUser.Text = "Update User"
        '
        'ctrl_VisitCS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrl_VisitCS"
        Me.Size = New System.Drawing.Size(224, 150)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustVer As System.Windows.Forms.Label
    Friend WithEvents lblUpdDate As System.Windows.Forms.Label
    Friend WithEvents cboCsFlag As System.Windows.Forms.ComboBox
    Friend WithEvents lblUpdDateValue As System.Windows.Forms.Label
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblUpdUserValue As System.Windows.Forms.Label
    Friend WithEvents lblUpdUser As System.Windows.Forms.Label

End Class
