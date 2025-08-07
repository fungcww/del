<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHKFI_Para
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtP1To = New System.Windows.Forms.DateTimePicker
        Me.dtP1From = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.rdoCODay = New System.Windows.Forms.RadioButton
        Me.rdoRL = New System.Windows.Forms.RadioButton
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoTL = New System.Windows.Forms.RadioButton
        Me.rdoUL = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "to"
        '
        'dtP1To
        '
        Me.dtP1To.CustomFormat = "MM/dd/yyyy"
        Me.dtP1To.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtP1To.Location = New System.Drawing.Point(211, 40)
        Me.dtP1To.Name = "dtP1To"
        Me.dtP1To.Size = New System.Drawing.Size(115, 20)
        Me.dtP1To.TabIndex = 7
        '
        'dtP1From
        '
        Me.dtP1From.CustomFormat = "MM/dd/yyyy"
        Me.dtP1From.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtP1From.Location = New System.Drawing.Point(59, 40)
        Me.dtP1From.Name = "dtP1From"
        Me.dtP1From.Size = New System.Drawing.Size(115, 20)
        Me.dtP1From.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Please input the period (inforce date):"
        '
        'rdoCODay
        '
        Me.rdoCODay.AutoSize = True
        Me.rdoCODay.Checked = True
        Me.rdoCODay.Location = New System.Drawing.Point(59, 110)
        Me.rdoCODay.Name = "rdoCODay"
        Me.rdoCODay.Size = New System.Drawing.Size(114, 17)
        Me.rdoCODay.TabIndex = 10
        Me.rdoCODay.TabStop = True
        Me.rdoCODay.Text = "Cooling off last day"
        Me.rdoCODay.UseVisualStyleBackColor = True
        '
        'rdoRL
        '
        Me.rdoRL.AutoSize = True
        Me.rdoRL.Location = New System.Drawing.Point(188, 110)
        Me.rdoRL.Name = "rdoRL"
        Me.rdoRL.Size = New System.Drawing.Size(75, 17)
        Me.rdoRL.TabIndex = 11
        Me.rdoRL.Text = "Risk Level"
        Me.rdoRL.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(101, 185)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(201, 185)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Sort By"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoTL)
        Me.Panel1.Controls.Add(Me.rdoUL)
        Me.Panel1.Location = New System.Drawing.Point(47, 76)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(279, 28)
        Me.Panel1.TabIndex = 15
        '
        'rdoTL
        '
        Me.rdoTL.AutoSize = True
        Me.rdoTL.Location = New System.Drawing.Point(141, 3)
        Me.rdoTL.Name = "rdoTL"
        Me.rdoTL.Size = New System.Drawing.Size(120, 17)
        Me.rdoTL.TabIndex = 1
        Me.rdoTL.TabStop = True
        Me.rdoTL.Text = "Tranditional Product"
        Me.rdoTL.UseVisualStyleBackColor = True
        '
        'rdoUL
        '
        Me.rdoUL.AutoSize = True
        Me.rdoUL.Checked = True
        Me.rdoUL.Location = New System.Drawing.Point(12, 3)
        Me.rdoUL.Name = "rdoUL"
        Me.rdoUL.Size = New System.Drawing.Size(107, 17)
        Me.rdoUL.TabIndex = 0
        Me.rdoUL.TabStop = True
        Me.rdoUL.Text = "Unit Link Product"
        Me.rdoUL.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Type"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(300, 139)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(59, 23)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(56, 142)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(238, 20)
        Me.txtPath.TabIndex = 18
        Me.txtPath.Text = "I:\Functional Log\Post Sales Call Log"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Path"
        '
        'frmHKFI_Para
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 220)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.rdoRL)
        Me.Controls.Add(Me.rdoCODay)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtP1To)
        Me.Controls.Add(Me.dtP1From)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmHKFI_Para"
        Me.Text = "ILAS Product - Post Sales Call Enquiry"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtP1To As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtP1From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdoCODay As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRL As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoTL As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUL As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
End Class
