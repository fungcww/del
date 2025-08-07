<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckSMS
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
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblSearchRegion = New System.Windows.Forms.Label()
        Me.lblSmsContent = New System.Windows.Forms.Label()
        Me.txtSmsContent = New System.Windows.Forms.TextBox()
        Me.gpbCompany = New System.Windows.Forms.GroupBox()
        Me.rbMC = New System.Windows.Forms.RadioButton()
        Me.rbHK = New System.Windows.Forms.RadioButton()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.lblAgentID = New System.Windows.Forms.Label()
        Me.dgvSMS = New System.Windows.Forms.DataGridView()
        Me.txtAgentID = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblRefNo = New System.Windows.Forms.Label()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.pnlSearch.SuspendLayout()
        Me.gpbCompany.SuspendLayout()
        CType(Me.dgvSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.lblSearchRegion)
        Me.pnlSearch.Controls.Add(Me.lblSmsContent)
        Me.pnlSearch.Controls.Add(Me.txtSmsContent)
        Me.pnlSearch.Controls.Add(Me.gpbCompany)
        Me.pnlSearch.Controls.Add(Me.lblMessage)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnSearch)
        Me.pnlSearch.Controls.Add(Me.lblPolicyNo)
        Me.pnlSearch.Controls.Add(Me.txtPolicyNo)
        Me.pnlSearch.Controls.Add(Me.lblAgentID)
        Me.pnlSearch.Controls.Add(Me.dgvSMS)
        Me.pnlSearch.Controls.Add(Me.txtAgentID)
        Me.pnlSearch.Controls.Add(Me.btnExit)
        Me.pnlSearch.Controls.Add(Me.lblRefNo)
        Me.pnlSearch.Controls.Add(Me.txtRefNo)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(1127, 703)
        Me.pnlSearch.TabIndex = 4
        '
        'lblSearchRegion
        '
        Me.lblSearchRegion.AutoSize = True
        Me.lblSearchRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearchRegion.ForeColor = System.Drawing.Color.Red
        Me.lblSearchRegion.Location = New System.Drawing.Point(23, 107)
        Me.lblSearchRegion.Name = "lblSearchRegion"
        Me.lblSearchRegion.Size = New System.Drawing.Size(423, 32)
        Me.lblSearchRegion.TabIndex = 14
        Me.lblSearchRegion.Text = "Search Results for Hong Kong"
        '
        'lblSmsContent
        '
        Me.lblSmsContent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSmsContent.AutoSize = True
        Me.lblSmsContent.Location = New System.Drawing.Point(25, 514)
        Me.lblSmsContent.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSmsContent.Name = "lblSmsContent"
        Me.lblSmsContent.Size = New System.Drawing.Size(105, 20)
        Me.lblSmsContent.TabIndex = 13
        Me.lblSmsContent.Text = "SMS Content"
        '
        'txtSmsContent
        '
        Me.txtSmsContent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSmsContent.Location = New System.Drawing.Point(134, 511)
        Me.txtSmsContent.Multiline = True
        Me.txtSmsContent.Name = "txtSmsContent"
        Me.txtSmsContent.ReadOnly = True
        Me.txtSmsContent.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSmsContent.Size = New System.Drawing.Size(800, 106)
        Me.txtSmsContent.TabIndex = 8
        '
        'gpbCompany
        '
        Me.gpbCompany.Controls.Add(Me.rbMC)
        Me.gpbCompany.Controls.Add(Me.rbHK)
        Me.gpbCompany.Location = New System.Drawing.Point(911, 12)
        Me.gpbCompany.Name = "gpbCompany"
        Me.gpbCompany.Size = New System.Drawing.Size(200, 123)
        Me.gpbCompany.TabIndex = 11
        Me.gpbCompany.TabStop = False
        Me.gpbCompany.Text = "Company"
        '
        'rbMC
        '
        Me.rbMC.AutoSize = True
        Me.rbMC.Location = New System.Drawing.Point(6, 68)
        Me.rbMC.Name = "rbMC"
        Me.rbMC.Size = New System.Drawing.Size(58, 24)
        Me.rbMC.TabIndex = 4
        Me.rbMC.TabStop = True
        Me.rbMC.Tag = "MC"
        Me.rbMC.Text = "MC"
        Me.rbMC.UseVisualStyleBackColor = True
        '
        'rbHK
        '
        Me.rbHK.AutoSize = True
        Me.rbHK.Checked = True
        Me.rbHK.Location = New System.Drawing.Point(6, 38)
        Me.rbHK.Name = "rbHK"
        Me.rbHK.Size = New System.Drawing.Size(56, 24)
        Me.rbHK.TabIndex = 3
        Me.rbHK.TabStop = True
        Me.rbHK.Tag = "HK"
        Me.rbHK.Text = "HK"
        Me.rbHK.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblMessage.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblMessage.Location = New System.Drawing.Point(0, 670)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(1127, 33)
        Me.lblMessage.TabIndex = 10
        '
        'btnClear
        '
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Location = New System.Drawing.Point(771, 100)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(112, 35)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "&Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(640, 100)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(112, 35)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(320, 46)
        Me.lblPolicyNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(73, 20)
        Me.lblPolicyNo.TabIndex = 9
        Me.lblPolicyNo.Text = "Policy No"
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.Location = New System.Drawing.Point(401, 46)
        Me.txtPolicyNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyNo.MaxLength = 500
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(193, 26)
        Me.txtPolicyNo.TabIndex = 1
        '
        'lblAgentID
        '
        Me.lblAgentID.AutoSize = True
        Me.lblAgentID.Location = New System.Drawing.Point(13, 46)
        Me.lblAgentID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAgentID.Name = "lblAgentID"
        Me.lblAgentID.Size = New System.Drawing.Size(73, 20)
        Me.lblAgentID.TabIndex = 2
        Me.lblAgentID.Text = "Agent ID"
        '
        'dgvSMS
        '
        Me.dgvSMS.AllowUserToAddRows = False
        Me.dgvSMS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSMS.Location = New System.Drawing.Point(17, 162)
        Me.dgvSMS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvSMS.MultiSelect = False
        Me.dgvSMS.Name = "dgvSMS"
        Me.dgvSMS.ReadOnly = True
        Me.dgvSMS.RowHeadersWidth = 51
        Me.dgvSMS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSMS.Size = New System.Drawing.Size(1049, 322)
        Me.dgvSMS.TabIndex = 7
        '
        'txtAgentID
        '
        Me.txtAgentID.Location = New System.Drawing.Point(94, 43)
        Me.txtAgentID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAgentID.MaxLength = 500
        Me.txtAgentID.Name = "txtAgentID"
        Me.txtAgentID.Size = New System.Drawing.Size(193, 26)
        Me.txtAgentID.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(940, 630)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(112, 35)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblRefNo
        '
        Me.lblRefNo.AutoSize = True
        Me.lblRefNo.Location = New System.Drawing.Point(621, 49)
        Me.lblRefNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefNo.Name = "lblRefNo"
        Me.lblRefNo.Size = New System.Drawing.Size(63, 20)
        Me.lblRefNo.TabIndex = 0
        Me.lblRefNo.Text = "Ref. No"
        '
        'txtRefNo
        '
        Me.txtRefNo.Location = New System.Drawing.Point(692, 49)
        Me.txtRefNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRefNo.MaxLength = 500
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(193, 26)
        Me.txtRefNo.TabIndex = 2
        '
        'frmCheckSMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1127, 703)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "frmCheckSMS"
        Me.Text = "Check SMS"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.gpbCompany.ResumeLayout(False)
        Me.gpbCompany.PerformLayout()
        CType(Me.dgvSMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSearch As Panel
    Friend WithEvents btnClear As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents lblPolicyNo As Label
    Friend WithEvents txtPolicyNo As TextBox
    Friend WithEvents lblAgentID As Label
    Friend WithEvents dgvSMS As DataGridView
    Friend WithEvents txtAgentID As TextBox
    Friend WithEvents btnExit As Button
    Friend WithEvents lblRefNo As Label
    Friend WithEvents txtRefNo As TextBox
    Friend WithEvents lblMessage As Label
    Friend WithEvents gpbCompany As GroupBox
    Friend WithEvents rbHK As RadioButton
    Friend WithEvents lblSmsContent As Label
    Friend WithEvents txtSmsContent As TextBox
    Friend WithEvents rbMC As RadioButton
    Friend WithEvents lblSearchRegion As Label
End Class
