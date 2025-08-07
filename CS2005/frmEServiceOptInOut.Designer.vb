<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEServiceOptInOut
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbOptIn = New System.Windows.Forms.RadioButton()
        Me.rbOptOut = New System.Windows.Forms.RadioButton()
        Me.rbNA = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUpdatedBy = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbIDType = New System.Windows.Forms.ComboBox()
        Me.txtUpdateDate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(36, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "HKID / Passport No. "
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(197, 30)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(208, 26)
        Me.txtID.TabIndex = 1
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(431, 72)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(85, 31)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(36, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(99, 119)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = true
        Me.txtName.Size = New System.Drawing.Size(417, 26)
        Me.txtName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(36, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Opt In/Out"
        '
        'rbOptIn
        '
        Me.rbOptIn.AutoSize = true
        Me.rbOptIn.Enabled = false
        Me.rbOptIn.Location = New System.Drawing.Point(156, 178)
        Me.rbOptIn.Name = "rbOptIn"
        Me.rbOptIn.Size = New System.Drawing.Size(78, 24)
        Me.rbOptIn.TabIndex = 6
        Me.rbOptIn.TabStop = true
        Me.rbOptIn.Text = "Opt In"
        Me.rbOptIn.UseVisualStyleBackColor = true
        '
        'rbOptOut
        '
        Me.rbOptOut.AutoSize = true
        Me.rbOptOut.Enabled = false
        Me.rbOptOut.Location = New System.Drawing.Point(258, 178)
        Me.rbOptOut.Name = "rbOptOut"
        Me.rbOptOut.Size = New System.Drawing.Size(90, 24)
        Me.rbOptOut.TabIndex = 7
        Me.rbOptOut.TabStop = true
        Me.rbOptOut.Text = "Opt Out"
        Me.rbOptOut.UseVisualStyleBackColor = true
        '
        'rbNA
        '
        Me.rbNA.AutoSize = true
        Me.rbNA.Enabled = false
        Me.rbNA.Location = New System.Drawing.Point(371, 178)
        Me.rbNA.Name = "rbNA"
        Me.rbNA.Size = New System.Drawing.Size(60, 24)
        Me.rbNA.TabIndex = 8
        Me.rbNA.TabStop = true
        Me.rbNA.Text = "N/A"
        Me.rbNA.UseVisualStyleBackColor = true
        Me.rbNA.Visible = false
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(36, 223)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Updated by"
        '
        'txtUpdatedBy
        '
        Me.txtUpdatedBy.Location = New System.Drawing.Point(156, 220)
        Me.txtUpdatedBy.Name = "txtUpdatedBy"
        Me.txtUpdatedBy.ReadOnly = true
        Me.txtUpdatedBy.Size = New System.Drawing.Size(360, 26)
        Me.txtUpdatedBy.TabIndex = 10
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = false
        Me.cmdSave.Location = New System.Drawing.Point(40, 299)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(85, 31)
        Me.cmdSave.TabIndex = 11
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = false
        Me.cmdCancel.Location = New System.Drawing.Point(137, 299)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(85, 31)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(36, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 20)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "ID Type"
        '
        'cbIDType
        '
        Me.cbIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIDType.FormattingEnabled = true
        Me.cbIDType.Items.AddRange(New Object() {"HKID", "PASSPORT"})
        Me.cbIDType.Location = New System.Drawing.Point(197, 74)
        Me.cbIDType.Name = "cbIDType"
        Me.cbIDType.Size = New System.Drawing.Size(208, 28)
        Me.cbIDType.TabIndex = 14
        '
        'txtUpdateDate
        '
        Me.txtUpdateDate.Location = New System.Drawing.Point(156, 258)
        Me.txtUpdateDate.Name = "txtUpdateDate"
        Me.txtUpdateDate.ReadOnly = true
        Me.txtUpdateDate.Size = New System.Drawing.Size(360, 26)
        Me.txtUpdateDate.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(36, 261)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 20)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Updated Date"
        '
        'frmEServiceOptInOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9!, 20!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 350)
        Me.Controls.Add(Me.txtUpdateDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbIDType)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtUpdatedBy)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.rbNA)
        Me.Controls.Add(Me.rbOptOut)
        Me.Controls.Add(Me.rbOptIn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmEServiceOptInOut"
        Me.Text = "eService Opt In/Out"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents cmdSearch As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents rbOptIn As RadioButton
    Friend WithEvents rbOptOut As RadioButton
    Friend WithEvents rbNA As RadioButton
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUpdatedBy As TextBox
    Friend WithEvents cmdSave As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents cbIDType As ComboBox
    Friend WithEvents txtUpdateDate As TextBox
    Friend WithEvents Label6 As Label
End Class
