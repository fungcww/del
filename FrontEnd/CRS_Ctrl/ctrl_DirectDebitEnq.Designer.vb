<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_DirectDebitEnq
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
        Me.dgvMandateList = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPayorNo = New System.Windows.Forms.TextBox
        Me.txtMandateRef = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtStatusCd = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFactorHse = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPayorName = New System.Windows.Forms.TextBox
        Me.txtStatusDesc = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpEffDate = New System.Windows.Forms.DateTimePicker
        Me.dtpSubmitDate = New System.Windows.Forms.DateTimePicker
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBankCd = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtBankLoc = New System.Windows.Forms.TextBox
        Me.txtBankName = New System.Windows.Forms.TextBox
        Me.txtBankAcct = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCurr = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtMandateAmt = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtMandateType = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMM = New System.Windows.Forms.TextBox
        Me.lblCCExpiryDate = New System.Windows.Forms.Label
        Me.txtYY = New System.Windows.Forms.TextBox
        Me.lbSlash = New System.Windows.Forms.Label
        Me.lblMMYY = New System.Windows.Forms.Label
        Me.txtInitialRejectDesc = New System.Windows.Forms.TextBox
        Me.txtInitialRejectCode = New System.Windows.Forms.TextBox
        Me.lblInitialReject = New System.Windows.Forms.Label
        Me.txtCUPPayorName = New System.Windows.Forms.TextBox
        Me.txtCUPAcctNo = New System.Windows.Forms.TextBox
        Me.lblCUPProvince = New System.Windows.Forms.Label
        Me.lblCUPBranch = New System.Windows.Forms.Label
        Me.txtCUPBranch = New System.Windows.Forms.TextBox
        Me.txtCUPCity = New System.Windows.Forms.TextBox
        Me.txtCUPProvince = New System.Windows.Forms.TextBox
        CType(Me.dgvMandateList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvMandateList
        '
        Me.dgvMandateList.AllowUserToAddRows = False
        Me.dgvMandateList.AllowUserToDeleteRows = False
        Me.dgvMandateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMandateList.Location = New System.Drawing.Point(3, 3)
        Me.dgvMandateList.Name = "dgvMandateList"
        Me.dgvMandateList.ReadOnly = True
        Me.dgvMandateList.Size = New System.Drawing.Size(773, 161)
        Me.dgvMandateList.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 178)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Payor:"
        '
        'txtPayorNo
        '
        Me.txtPayorNo.Location = New System.Drawing.Point(105, 175)
        Me.txtPayorNo.Name = "txtPayorNo"
        Me.txtPayorNo.ReadOnly = True
        Me.txtPayorNo.Size = New System.Drawing.Size(100, 20)
        Me.txtPayorNo.TabIndex = 2
        '
        'txtMandateRef
        '
        Me.txtMandateRef.Location = New System.Drawing.Point(105, 201)
        Me.txtMandateRef.Name = "txtMandateRef"
        Me.txtMandateRef.ReadOnly = True
        Me.txtMandateRef.Size = New System.Drawing.Size(456, 20)
        Me.txtMandateRef.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 204)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Mandate Ref:"
        '
        'txtStatusCd
        '
        Me.txtStatusCd.Location = New System.Drawing.Point(105, 230)
        Me.txtStatusCd.Name = "txtStatusCd"
        Me.txtStatusCd.ReadOnly = True
        Me.txtStatusCd.Size = New System.Drawing.Size(100, 20)
        Me.txtStatusCd.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 233)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Status:"
        '
        'txtFactorHse
        '
        Me.txtFactorHse.Location = New System.Drawing.Point(105, 336)
        Me.txtFactorHse.Name = "txtFactorHse"
        Me.txtFactorHse.ReadOnly = True
        Me.txtFactorHse.Size = New System.Drawing.Size(200, 20)
        Me.txtFactorHse.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 339)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Factoring house:"
        '
        'txtPayorName
        '
        Me.txtPayorName.Location = New System.Drawing.Point(211, 175)
        Me.txtPayorName.Name = "txtPayorName"
        Me.txtPayorName.ReadOnly = True
        Me.txtPayorName.Size = New System.Drawing.Size(350, 20)
        Me.txtPayorName.TabIndex = 3
        '
        'txtStatusDesc
        '
        Me.txtStatusDesc.Location = New System.Drawing.Point(211, 230)
        Me.txtStatusDesc.Name = "txtStatusDesc"
        Me.txtStatusDesc.ReadOnly = True
        Me.txtStatusDesc.Size = New System.Drawing.Size(350, 20)
        Me.txtStatusDesc.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 260)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Effective Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 286)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Submit Date:"
        '
        'dtpEffDate
        '
        Me.dtpEffDate.Enabled = False
        Me.dtpEffDate.Location = New System.Drawing.Point(105, 256)
        Me.dtpEffDate.Name = "dtpEffDate"
        Me.dtpEffDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpEffDate.TabIndex = 7
        '
        'dtpSubmitDate
        '
        Me.dtpSubmitDate.Enabled = False
        Me.dtpSubmitDate.Location = New System.Drawing.Point(105, 282)
        Me.dtpSubmitDate.Name = "dtpSubmitDate"
        Me.dtpSubmitDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpSubmitDate.TabIndex = 8
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Enabled = False
        Me.dtpEndDate.Location = New System.Drawing.Point(105, 310)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpEndDate.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 314)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "End Date:"
        '
        'txtBankCd
        '
        Me.txtBankCd.Location = New System.Drawing.Point(105, 362)
        Me.txtBankCd.Name = "txtBankCd"
        Me.txtBankCd.ReadOnly = True
        Me.txtBankCd.Size = New System.Drawing.Size(456, 20)
        Me.txtBankCd.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 365)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Bank:"
        '
        'txtBankLoc
        '
        Me.txtBankLoc.Location = New System.Drawing.Point(105, 384)
        Me.txtBankLoc.Name = "txtBankLoc"
        Me.txtBankLoc.ReadOnly = True
        Me.txtBankLoc.Size = New System.Drawing.Size(456, 20)
        Me.txtBankLoc.TabIndex = 12
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(105, 407)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.Size = New System.Drawing.Size(456, 20)
        Me.txtBankName.TabIndex = 13
        '
        'txtBankAcct
        '
        Me.txtBankAcct.Location = New System.Drawing.Point(105, 433)
        Me.txtBankAcct.Name = "txtBankAcct"
        Me.txtBankAcct.ReadOnly = True
        Me.txtBankAcct.Size = New System.Drawing.Size(456, 20)
        Me.txtBankAcct.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 436)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Bank Acct:"
        '
        'txtCurr
        '
        Me.txtCurr.Location = New System.Drawing.Point(105, 459)
        Me.txtCurr.Name = "txtCurr"
        Me.txtCurr.ReadOnly = True
        Me.txtCurr.Size = New System.Drawing.Size(100, 20)
        Me.txtCurr.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 462)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Currency:"
        '
        'txtMandateAmt
        '
        Me.txtMandateAmt.Location = New System.Drawing.Point(311, 458)
        Me.txtMandateAmt.Name = "txtMandateAmt"
        Me.txtMandateAmt.ReadOnly = True
        Me.txtMandateAmt.Size = New System.Drawing.Size(250, 20)
        Me.txtMandateAmt.TabIndex = 16
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(214, 461)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Mandate Amount:"
        '
        'txtMandateType
        '
        Me.txtMandateType.Location = New System.Drawing.Point(105, 535)
        Me.txtMandateType.Name = "txtMandateType"
        Me.txtMandateType.ReadOnly = True
        Me.txtMandateType.Size = New System.Drawing.Size(100, 20)
        Me.txtMandateType.TabIndex = 17
        Me.txtMandateType.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 538)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Mandate type:"
        Me.Label12.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(214, 538)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(166, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "1 - Fixed, 2 - Variable, 3 - Optional"
        Me.Label13.Visible = False
        '
        'txtMM
        '
        Me.txtMM.Location = New System.Drawing.Point(140, 484)
        Me.txtMM.Name = "txtMM"
        Me.txtMM.ReadOnly = True
        Me.txtMM.Size = New System.Drawing.Size(38, 20)
        Me.txtMM.TabIndex = 30
        '
        'lblCCExpiryDate
        '
        Me.lblCCExpiryDate.AutoSize = True
        Me.lblCCExpiryDate.Location = New System.Drawing.Point(15, 487)
        Me.lblCCExpiryDate.Name = "lblCCExpiryDate"
        Me.lblCCExpiryDate.Size = New System.Drawing.Size(119, 13)
        Me.lblCCExpiryDate.TabIndex = 31
        Me.lblCCExpiryDate.Text = "Credit Card Expiry Date:"
        '
        'txtYY
        '
        Me.txtYY.Location = New System.Drawing.Point(195, 484)
        Me.txtYY.Name = "txtYY"
        Me.txtYY.ReadOnly = True
        Me.txtYY.Size = New System.Drawing.Size(38, 20)
        Me.txtYY.TabIndex = 32
        '
        'lbSlash
        '
        Me.lbSlash.AutoSize = True
        Me.lbSlash.Location = New System.Drawing.Point(180, 487)
        Me.lbSlash.Name = "lbSlash"
        Me.lbSlash.Size = New System.Drawing.Size(12, 13)
        Me.lbSlash.TabIndex = 33
        Me.lbSlash.Text = "/"
        '
        'lblMMYY
        '
        Me.lblMMYY.AutoSize = True
        Me.lblMMYY.Location = New System.Drawing.Point(239, 487)
        Me.lblMMYY.Name = "lblMMYY"
        Me.lblMMYY.Size = New System.Drawing.Size(50, 13)
        Me.lblMMYY.TabIndex = 34
        Me.lblMMYY.Text = "(MM/YY)"
        '
        'txtInitialRejectDesc
        '
        Me.txtInitialRejectDesc.Location = New System.Drawing.Point(211, 510)
        Me.txtInitialRejectDesc.Name = "txtInitialRejectDesc"
        Me.txtInitialRejectDesc.ReadOnly = True
        Me.txtInitialRejectDesc.Size = New System.Drawing.Size(350, 20)
        Me.txtInitialRejectDesc.TabIndex = 37
        '
        'txtInitialRejectCode
        '
        Me.txtInitialRejectCode.Location = New System.Drawing.Point(105, 510)
        Me.txtInitialRejectCode.Name = "txtInitialRejectCode"
        Me.txtInitialRejectCode.ReadOnly = True
        Me.txtInitialRejectCode.Size = New System.Drawing.Size(100, 20)
        Me.txtInitialRejectCode.TabIndex = 36
        '
        'lblInitialReject
        '
        Me.lblInitialReject.AutoSize = True
        Me.lblInitialReject.Location = New System.Drawing.Point(15, 513)
        Me.lblInitialReject.Name = "lblInitialReject"
        Me.lblInitialReject.Size = New System.Drawing.Size(68, 13)
        Me.lblInitialReject.TabIndex = 35
        Me.lblInitialReject.Text = "Initial Reject:"
        Me.lblInitialReject.UseWaitCursor = True
        '
        'txtCUPPayorName
        '
        Me.txtCUPPayorName.Location = New System.Drawing.Point(211, 175)
        Me.txtCUPPayorName.Name = "txtCUPPayorName"
        Me.txtCUPPayorName.ReadOnly = True
        Me.txtCUPPayorName.Size = New System.Drawing.Size(350, 20)
        Me.txtCUPPayorName.TabIndex = 3
        '
        'txtCUPAcctNo
        '
        Me.txtCUPAcctNo.Location = New System.Drawing.Point(105, 433)
        Me.txtCUPAcctNo.Name = "txtCUPAcctNo"
        Me.txtCUPAcctNo.ReadOnly = True
        Me.txtCUPAcctNo.Size = New System.Drawing.Size(456, 20)
        Me.txtCUPAcctNo.TabIndex = 14
        Me.txtCUPAcctNo.Visible = False
        '
        'lblCUPProvince
        '
        Me.lblCUPProvince.AutoSize = True
        Me.lblCUPProvince.Location = New System.Drawing.Point(14, 387)
        Me.lblCUPProvince.Name = "lblCUPProvince"
        Me.lblCUPProvince.Size = New System.Drawing.Size(74, 13)
        Me.lblCUPProvince.TabIndex = 53
        Me.lblCUPProvince.Text = "Province/City:"
        Me.lblCUPProvince.Visible = False
        '
        'lblCUPBranch
        '
        Me.lblCUPBranch.AutoSize = True
        Me.lblCUPBranch.Location = New System.Drawing.Point(271, 387)
        Me.lblCUPBranch.Name = "lblCUPBranch"
        Me.lblCUPBranch.Size = New System.Drawing.Size(44, 13)
        Me.lblCUPBranch.TabIndex = 54
        Me.lblCUPBranch.Text = "Branch:"
        Me.lblCUPBranch.Visible = False
        '
        'txtCUPBranch
        '
        Me.txtCUPBranch.BackColor = System.Drawing.SystemColors.Control
        Me.txtCUPBranch.Enabled = False
        Me.txtCUPBranch.Location = New System.Drawing.Point(321, 384)
        Me.txtCUPBranch.Name = "txtCUPBranch"
        Me.txtCUPBranch.ReadOnly = True
        Me.txtCUPBranch.Size = New System.Drawing.Size(240, 20)
        Me.txtCUPBranch.TabIndex = 52
        Me.txtCUPBranch.Visible = False
        '
        'txtCUPCity
        '
        Me.txtCUPCity.BackColor = System.Drawing.SystemColors.Control
        Me.txtCUPCity.Enabled = False
        Me.txtCUPCity.Location = New System.Drawing.Point(187, 384)
        Me.txtCUPCity.Name = "txtCUPCity"
        Me.txtCUPCity.ReadOnly = True
        Me.txtCUPCity.Size = New System.Drawing.Size(76, 20)
        Me.txtCUPCity.TabIndex = 51
        Me.txtCUPCity.Visible = False
        '
        'txtCUPProvince
        '
        Me.txtCUPProvince.BackColor = System.Drawing.SystemColors.Control
        Me.txtCUPProvince.Enabled = False
        Me.txtCUPProvince.Location = New System.Drawing.Point(105, 384)
        Me.txtCUPProvince.Name = "txtCUPProvince"
        Me.txtCUPProvince.ReadOnly = True
        Me.txtCUPProvince.Size = New System.Drawing.Size(76, 20)
        Me.txtCUPProvince.TabIndex = 50
        Me.txtCUPProvince.Visible = False
        '
        'ctrl_DirectDebitEnq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblCUPProvince)
        Me.Controls.Add(Me.lblCUPBranch)
        Me.Controls.Add(Me.txtCUPBranch)
        Me.Controls.Add(Me.txtCUPCity)
        Me.Controls.Add(Me.txtCUPProvince)
        Me.Controls.Add(Me.txtInitialRejectDesc)
        Me.Controls.Add(Me.txtInitialRejectCode)
        Me.Controls.Add(Me.lblInitialReject)
        Me.Controls.Add(Me.lblMMYY)
        Me.Controls.Add(Me.lbSlash)
        Me.Controls.Add(Me.txtYY)
        Me.Controls.Add(Me.txtMM)
        Me.Controls.Add(Me.lblCCExpiryDate)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtMandateType)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtMandateAmt)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtCurr)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtBankAcct)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.txtBankLoc)
        Me.Controls.Add(Me.txtBankCd)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpSubmitDate)
        Me.Controls.Add(Me.dtpEffDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtStatusDesc)
        Me.Controls.Add(Me.txtPayorName)
        Me.Controls.Add(Me.txtFactorHse)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtStatusCd)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMandateRef)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPayorNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvMandateList)
        Me.Controls.Add(Me.txtCUPPayorName)
        Me.Controls.Add(Me.txtCUPAcctNo)
        Me.Name = "ctrl_DirectDebitEnq"
        Me.Size = New System.Drawing.Size(779, 560)
        CType(Me.dgvMandateList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvMandateList As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPayorNo As System.Windows.Forms.TextBox
    Friend WithEvents txtMandateRef As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtStatusCd As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFactorHse As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPayorName As System.Windows.Forms.TextBox
    Friend WithEvents txtStatusDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpEffDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSubmitDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBankCd As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBankLoc As System.Windows.Forms.TextBox
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents txtBankAcct As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCurr As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMandateAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtMandateType As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents lblCCExpiryDate As System.Windows.Forms.Label
    Friend WithEvents txtYY As System.Windows.Forms.TextBox
    Friend WithEvents lbSlash As System.Windows.Forms.Label
    Friend WithEvents lblMMYY As System.Windows.Forms.Label
    Friend WithEvents txtInitialRejectDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtInitialRejectCode As System.Windows.Forms.TextBox
    Friend WithEvents lblInitialReject As System.Windows.Forms.Label
    Friend WithEvents txtCUPPayorName As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPAcctNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCUPProvince As System.Windows.Forms.Label
    Friend WithEvents lblCUPBranch As System.Windows.Forms.Label
    Friend WithEvents txtCUPBranch As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPCity As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPProvince As System.Windows.Forms.TextBox

End Class
