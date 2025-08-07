<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm7ElevenPayout
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
        Me.lblPolicyNo = New System.Windows.Forms.Label
        Me.lblMobileNo = New System.Windows.Forms.Label
        Me.txtPolicyNo = New System.Windows.Forms.TextBox
        Me.txtMobileNo = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnDisableQrCode = New System.Windows.Forms.Button
        Me.btnUnlockQrCode = New System.Windows.Forms.Button
        Me.gbPayoutDetail = New System.Windows.Forms.GroupBox
        Me.btnSaveRemarks = New System.Windows.Forms.Button
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.lblRemarks = New System.Windows.Forms.Label
        Me.gvPayoutDetail = New System.Windows.Forms.DataGridView
        Me.gbTransactions = New System.Windows.Forms.GroupBox
        Me.gvTransactions = New System.Windows.Forms.DataGridView
        Me.gbTranHistory = New System.Windows.Forms.GroupBox
        Me.gvTranHistory = New System.Windows.Forms.DataGridView
        Me.gbSmsDetails = New System.Windows.Forms.GroupBox
        Me.btnResend = New System.Windows.Forms.Button
        Me.gvSmsDetails = New System.Windows.Forms.DataGridView
        Me.txtSmsMessage = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.gbPayoutDetail.SuspendLayout()
        CType(Me.gvPayoutDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTransactions.SuspendLayout()
        CType(Me.gvTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTranHistory.SuspendLayout()
        CType(Me.gvTranHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSmsDetails.SuspendLayout()
        CType(Me.gvSmsDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(12, 18)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(55, 13)
        Me.lblPolicyNo.TabIndex = 0
        Me.lblPolicyNo.Text = "Policy No."
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Location = New System.Drawing.Point(12, 45)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(58, 13)
        Me.lblMobileNo.TabIndex = 1
        Me.lblMobileNo.Text = "Mobile No."
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.Location = New System.Drawing.Point(73, 15)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(122, 20)
        Me.txtPolicyNo.TabIndex = 2
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Location = New System.Drawing.Point(73, 42)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(122, 20)
        Me.txtMobileNo.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(220, 43)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 20)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(301, 43)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 20)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDisableQrCode
        '
        Me.btnDisableQrCode.Location = New System.Drawing.Point(382, 42)
        Me.btnDisableQrCode.Name = "btnDisableQrCode"
        Me.btnDisableQrCode.Size = New System.Drawing.Size(129, 20)
        Me.btnDisableQrCode.TabIndex = 6
        Me.btnDisableQrCode.Text = "Disable QR Code"
        Me.btnDisableQrCode.UseVisualStyleBackColor = True
        '
        'btnUnlockQrCode
        '
        Me.btnUnlockQrCode.Location = New System.Drawing.Point(517, 42)
        Me.btnUnlockQrCode.Name = "btnUnlockQrCode"
        Me.btnUnlockQrCode.Size = New System.Drawing.Size(129, 20)
        Me.btnUnlockQrCode.TabIndex = 7
        Me.btnUnlockQrCode.Text = "Unlock QR Code"
        Me.btnUnlockQrCode.UseVisualStyleBackColor = True
        '
        'gbPayoutDetail
        '
        Me.gbPayoutDetail.Controls.Add(Me.btnSaveRemarks)
        Me.gbPayoutDetail.Controls.Add(Me.txtRemarks)
        Me.gbPayoutDetail.Controls.Add(Me.lblRemarks)
        Me.gbPayoutDetail.Controls.Add(Me.gvPayoutDetail)
        Me.gbPayoutDetail.Location = New System.Drawing.Point(15, 83)
        Me.gbPayoutDetail.Name = "gbPayoutDetail"
        Me.gbPayoutDetail.Size = New System.Drawing.Size(918, 229)
        Me.gbPayoutDetail.TabIndex = 8
        Me.gbPayoutDetail.TabStop = False
        Me.gbPayoutDetail.Text = "Payout Detail"
        '
        'btnSaveRemarks
        '
        Me.btnSaveRemarks.Location = New System.Drawing.Point(531, 203)
        Me.btnSaveRemarks.Name = "btnSaveRemarks"
        Me.btnSaveRemarks.Size = New System.Drawing.Size(129, 20)
        Me.btnSaveRemarks.TabIndex = 3
        Me.btnSaveRemarks.Text = "Save Remarks"
        Me.btnSaveRemarks.UseVisualStyleBackColor = True
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(61, 159)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(464, 64)
        Me.txtRemarks.TabIndex = 2
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(6, 162)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 1
        Me.lblRemarks.Text = "Remarks"
        '
        'gvPayoutDetail
        '
        Me.gvPayoutDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvPayoutDetail.Location = New System.Drawing.Point(7, 20)
        Me.gvPayoutDetail.Name = "gvPayoutDetail"
        Me.gvPayoutDetail.ReadOnly = True
        Me.gvPayoutDetail.Size = New System.Drawing.Size(905, 135)
        Me.gvPayoutDetail.TabIndex = 0
        '
        'gbTransactions
        '
        Me.gbTransactions.Controls.Add(Me.gvTransactions)
        Me.gbTransactions.Location = New System.Drawing.Point(15, 317)
        Me.gbTransactions.Name = "gbTransactions"
        Me.gbTransactions.Size = New System.Drawing.Size(235, 151)
        Me.gbTransactions.TabIndex = 9
        Me.gbTransactions.TabStop = False
        Me.gbTransactions.Text = "Transactions"
        '
        'gvTransactions
        '
        Me.gvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvTransactions.Location = New System.Drawing.Point(7, 20)
        Me.gvTransactions.Name = "gvTransactions"
        Me.gvTransactions.ReadOnly = True
        Me.gvTransactions.Size = New System.Drawing.Size(222, 125)
        Me.gvTransactions.TabIndex = 0
        '
        'gbTranHistory
        '
        Me.gbTranHistory.Controls.Add(Me.gvTranHistory)
        Me.gbTranHistory.Location = New System.Drawing.Point(257, 318)
        Me.gbTranHistory.Name = "gbTranHistory"
        Me.gbTranHistory.Size = New System.Drawing.Size(676, 150)
        Me.gbTranHistory.TabIndex = 10
        Me.gbTranHistory.TabStop = False
        Me.gbTranHistory.Text = "Transaction History"
        '
        'gvTranHistory
        '
        Me.gvTranHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvTranHistory.Location = New System.Drawing.Point(6, 19)
        Me.gvTranHistory.Name = "gvTranHistory"
        Me.gvTranHistory.ReadOnly = True
        Me.gvTranHistory.Size = New System.Drawing.Size(663, 124)
        Me.gvTranHistory.TabIndex = 0
        '
        'gbSmsDetails
        '
        Me.gbSmsDetails.Controls.Add(Me.btnResend)
        Me.gbSmsDetails.Controls.Add(Me.gvSmsDetails)
        Me.gbSmsDetails.Controls.Add(Me.txtSmsMessage)
        Me.gbSmsDetails.Location = New System.Drawing.Point(15, 475)
        Me.gbSmsDetails.Name = "gbSmsDetails"
        Me.gbSmsDetails.Size = New System.Drawing.Size(815, 184)
        Me.gbSmsDetails.TabIndex = 11
        Me.gbSmsDetails.TabStop = False
        Me.gbSmsDetails.Text = "SMS Details"
        '
        'btnResend
        '
        Me.btnResend.Location = New System.Drawing.Point(527, 153)
        Me.btnResend.Name = "btnResend"
        Me.btnResend.Size = New System.Drawing.Size(129, 20)
        Me.btnResend.TabIndex = 13
        Me.btnResend.Text = "Resend SMS"
        Me.btnResend.UseVisualStyleBackColor = True
        '
        'gvSmsDetails
        '
        Me.gvSmsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvSmsDetails.Location = New System.Drawing.Point(9, 19)
        Me.gvSmsDetails.Name = "gvSmsDetails"
        Me.gvSmsDetails.Size = New System.Drawing.Size(512, 90)
        Me.gvSmsDetails.TabIndex = 6
        '
        'txtSmsMessage
        '
        Me.txtSmsMessage.Location = New System.Drawing.Point(9, 115)
        Me.txtSmsMessage.Multiline = True
        Me.txtSmsMessage.Name = "txtSmsMessage"
        Me.txtSmsMessage.Size = New System.Drawing.Size(512, 58)
        Me.txtSmsMessage.TabIndex = 5
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(851, 639)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 20)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frm7ElevenPayout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 666)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.gbSmsDetails)
        Me.Controls.Add(Me.gbTranHistory)
        Me.Controls.Add(Me.gbTransactions)
        Me.Controls.Add(Me.gbPayoutDetail)
        Me.Controls.Add(Me.btnUnlockQrCode)
        Me.Controls.Add(Me.btnDisableQrCode)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtMobileNo)
        Me.Controls.Add(Me.txtPolicyNo)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.lblPolicyNo)
        Me.Name = "frm7ElevenPayout"
        Me.Text = "7-Eleven QR code maintenance"
        Me.gbPayoutDetail.ResumeLayout(False)
        Me.gbPayoutDetail.PerformLayout()
        CType(Me.gvPayoutDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTransactions.ResumeLayout(False)
        CType(Me.gvTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTranHistory.ResumeLayout(False)
        CType(Me.gvTranHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSmsDetails.ResumeLayout(False)
        Me.gbSmsDetails.PerformLayout()
        CType(Me.gvSmsDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDisableQrCode As System.Windows.Forms.Button
    Friend WithEvents btnUnlockQrCode As System.Windows.Forms.Button
    Friend WithEvents gbPayoutDetail As System.Windows.Forms.GroupBox
    Friend WithEvents gbTransactions As System.Windows.Forms.GroupBox
    Friend WithEvents gbTranHistory As System.Windows.Forms.GroupBox
    Friend WithEvents gbSmsDetails As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents gvPayoutDetail As System.Windows.Forms.DataGridView
    Friend WithEvents btnSaveRemarks As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents gvTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents gvTranHistory As System.Windows.Forms.DataGridView
    Friend WithEvents txtSmsMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnResend As System.Windows.Forms.Button
    Friend WithEvents gvSmsDetails As System.Windows.Forms.DataGridView
End Class
