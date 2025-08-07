<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserPreference
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lbllastupdatetime = New System.Windows.Forms.Label
        Me.cbodefaultmedium = New System.Windows.Forms.ComboBox
        Me.btnchangedefault = New System.Windows.Forms.Button
        Me.lbluser = New System.Windows.Forms.Label
        Me.cbodefaultinitiator = New System.Windows.Forms.ComboBox
        Me.txtuser = New System.Windows.Forms.TextBox
        Me.lbldefaultinitiator = New System.Windows.Forms.Label
        Me.lbldefaultmedium = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbllastupdatetime)
        Me.GroupBox1.Controls.Add(Me.cbodefaultmedium)
        Me.GroupBox1.Controls.Add(Me.btnchangedefault)
        Me.GroupBox1.Controls.Add(Me.lbluser)
        Me.GroupBox1.Controls.Add(Me.cbodefaultinitiator)
        Me.GroupBox1.Controls.Add(Me.txtuser)
        Me.GroupBox1.Controls.Add(Me.lbldefaultinitiator)
        Me.GroupBox1.Controls.Add(Me.lbldefaultmedium)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(349, 179)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Service Log"
        '
        'lbllastupdatetime
        '
        Me.lbllastupdatetime.AutoSize = True
        Me.lbllastupdatetime.Location = New System.Drawing.Point(18, 123)
        Me.lbllastupdatetime.Name = "lbllastupdatetime"
        Me.lbllastupdatetime.Size = New System.Drawing.Size(0, 13)
        Me.lbllastupdatetime.TabIndex = 7
        Me.lbllastupdatetime.Visible = False
        '
        'cbodefaultmedium
        '
        Me.cbodefaultmedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodefaultmedium.FormattingEnabled = True
        Me.cbodefaultmedium.Location = New System.Drawing.Point(121, 50)
        Me.cbodefaultmedium.Name = "cbodefaultmedium"
        Me.cbodefaultmedium.Size = New System.Drawing.Size(192, 21)
        Me.cbodefaultmedium.TabIndex = 3
        '
        'btnchangedefault
        '
        Me.btnchangedefault.Location = New System.Drawing.Point(217, 123)
        Me.btnchangedefault.Name = "btnchangedefault"
        Me.btnchangedefault.Size = New System.Drawing.Size(95, 30)
        Me.btnchangedefault.TabIndex = 6
        Me.btnchangedefault.Text = "Submit"
        Me.btnchangedefault.UseVisualStyleBackColor = True
        '
        'lbluser
        '
        Me.lbluser.AutoSize = True
        Me.lbluser.Location = New System.Drawing.Point(18, 22)
        Me.lbluser.Name = "lbluser"
        Me.lbluser.Size = New System.Drawing.Size(29, 13)
        Me.lbluser.TabIndex = 0
        Me.lbluser.Text = "User"
        '
        'cbodefaultinitiator
        '
        Me.cbodefaultinitiator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodefaultinitiator.FormattingEnabled = True
        Me.cbodefaultinitiator.Location = New System.Drawing.Point(121, 86)
        Me.cbodefaultinitiator.Name = "cbodefaultinitiator"
        Me.cbodefaultinitiator.Size = New System.Drawing.Size(192, 21)
        Me.cbodefaultinitiator.TabIndex = 5
        '
        'txtuser
        '
        Me.txtuser.Enabled = False
        Me.txtuser.Location = New System.Drawing.Point(119, 15)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(194, 20)
        Me.txtuser.TabIndex = 1
        '
        'lbldefaultinitiator
        '
        Me.lbldefaultinitiator.AutoSize = True
        Me.lbldefaultinitiator.Location = New System.Drawing.Point(16, 94)
        Me.lbldefaultinitiator.Name = "lbldefaultinitiator"
        Me.lbldefaultinitiator.Size = New System.Drawing.Size(78, 13)
        Me.lbldefaultinitiator.TabIndex = 4
        Me.lbldefaultinitiator.Text = "Default Initiator"
        '
        'lbldefaultmedium
        '
        Me.lbldefaultmedium.AutoSize = True
        Me.lbldefaultmedium.Location = New System.Drawing.Point(18, 58)
        Me.lbldefaultmedium.Name = "lbldefaultmedium"
        Me.lbldefaultmedium.Size = New System.Drawing.Size(81, 13)
        Me.lbldefaultmedium.TabIndex = 2
        Me.lbldefaultmedium.Text = "Default Medium"
        '
        'frmUserPreference
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 209)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmUserPreference"
        Me.Text = "User Preference"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbodefaultmedium As System.Windows.Forms.ComboBox
    Friend WithEvents btnchangedefault As System.Windows.Forms.Button
    Friend WithEvents lbluser As System.Windows.Forms.Label
    Friend WithEvents cbodefaultinitiator As System.Windows.Forms.ComboBox
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Friend WithEvents lbldefaultinitiator As System.Windows.Forms.Label
    Friend WithEvents lbldefaultmedium As System.Windows.Forms.Label
    Friend WithEvents lbllastupdatetime As System.Windows.Forms.Label
End Class
