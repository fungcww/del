<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclPostSalesCallQuestionnaireMcu
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
        Me.components = New System.ComponentModel.Container
        Me.fpnQuestion = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.dgvCoverage = New System.Windows.Forms.DataGridView
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkFees = New System.Windows.Forms.CheckBox
        Me.txtRiskChi = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtProductTypeChi = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtObjectiveChi = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.txtRiskEng = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtNonGuarBenefit = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtFees = New System.Windows.Forms.TextBox
        Me.txtBenefit = New System.Windows.Forms.TextBox
        Me.txtProductTypeEng = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtGuideUrl = New System.Windows.Forms.TextBox
        Me.txtPremiumType = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtObjectiveEng = New System.Windows.Forms.TextBox
        Me.txtLoan = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtDividend = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.txtCoupon = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtBonus = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCSV = New System.Windows.Forms.TextBox
        Me.bsProduct = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtEarlySurrPeriod = New System.Windows.Forms.TextBox
        Me.txtEarlySurCharge = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.dgvCoverage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fpnQuestion
        '
        Me.fpnQuestion.AutoScroll = True
        Me.fpnQuestion.BackColor = System.Drawing.SystemColors.Window
        Me.fpnQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.fpnQuestion.Location = New System.Drawing.Point(3, 281)
        Me.fpnQuestion.Name = "fpnQuestion"
        Me.fpnQuestion.Size = New System.Drawing.Size(815, 221)
        Me.fpnQuestion.TabIndex = 0
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(662, 508)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(743, 508)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'dgvCoverage
        '
        Me.dgvCoverage.AllowUserToAddRows = False
        Me.dgvCoverage.AllowUserToDeleteRows = False
        Me.dgvCoverage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCoverage.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column1, Me.Column2, Me.Column3})
        Me.dgvCoverage.Location = New System.Drawing.Point(3, 0)
        Me.dgvCoverage.Name = "dgvCoverage"
        Me.dgvCoverage.ReadOnly = True
        Me.dgvCoverage.Size = New System.Drawing.Size(815, 69)
        Me.dgvCoverage.TabIndex = 4
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "Trailer"
        Me.Column4.HeaderText = "Trailer"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "ProductID"
        Me.Column1.HeaderText = "Product ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "Description"
        Me.Column2.HeaderText = "Product Name (Eng)"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 300
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "ChineseDescription"
        Me.Column3.HeaderText = "Product Name (Chi)"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 250
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 265)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Questions"
        '
        'chkFees
        '
        Me.chkFees.AutoSize = True
        Me.chkFees.Enabled = False
        Me.chkFees.Location = New System.Drawing.Point(27, 168)
        Me.chkFees.Name = "chkFees"
        Me.chkFees.Size = New System.Drawing.Size(95, 17)
        Me.chkFees.TabIndex = 7
        Me.chkFees.Text = "Fee && Charges"
        Me.chkFees.UseVisualStyleBackColor = True
        '
        'txtRiskChi
        '
        Me.txtRiskChi.Location = New System.Drawing.Point(441, 494)
        Me.txtRiskChi.Multiline = True
        Me.txtRiskChi.Name = "txtRiskChi"
        Me.txtRiskChi.ReadOnly = True
        Me.txtRiskChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRiskChi.Size = New System.Drawing.Size(319, 89)
        Me.txtRiskChi.TabIndex = 22
        Me.txtRiskChi.WordWrap = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(438, 478)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(92, 13)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "Product Risk (Chi)"
        '
        'txtProductTypeChi
        '
        Me.txtProductTypeChi.Location = New System.Drawing.Point(441, 278)
        Me.txtProductTypeChi.Multiline = True
        Me.txtProductTypeChi.Name = "txtProductTypeChi"
        Me.txtProductTypeChi.ReadOnly = True
        Me.txtProductTypeChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtProductTypeChi.Size = New System.Drawing.Size(323, 89)
        Me.txtProductTypeChi.TabIndex = 18
        Me.txtProductTypeChi.WordWrap = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(442, 262)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(95, 13)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "Product Type (Chi)"
        '
        'txtObjectiveChi
        '
        Me.txtObjectiveChi.Location = New System.Drawing.Point(441, 386)
        Me.txtObjectiveChi.Multiline = True
        Me.txtObjectiveChi.Name = "txtObjectiveChi"
        Me.txtObjectiveChi.ReadOnly = True
        Me.txtObjectiveChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObjectiveChi.Size = New System.Drawing.Size(319, 89)
        Me.txtObjectiveChi.TabIndex = 20
        Me.txtObjectiveChi.WordWrap = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(438, 370)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(116, 13)
        Me.Label25.TabIndex = 42
        Me.Label25.Text = "Product Objective (Chi)"
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Enabled = False
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(128, 4)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(207, 21)
        Me.cboCategory.TabIndex = 3
        '
        'txtRiskEng
        '
        Me.txtRiskEng.Location = New System.Drawing.Point(55, 494)
        Me.txtRiskEng.Multiline = True
        Me.txtRiskEng.Name = "txtRiskEng"
        Me.txtRiskEng.ReadOnly = True
        Me.txtRiskEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRiskEng.Size = New System.Drawing.Size(319, 89)
        Me.txtRiskEng.TabIndex = 21
        Me.txtRiskEng.WordWrap = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(52, 478)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 13)
        Me.Label20.TabIndex = 37
        Me.Label20.Text = "Product Risk (Eng)"
        '
        'txtNonGuarBenefit
        '
        Me.txtNonGuarBenefit.Location = New System.Drawing.Point(516, 135)
        Me.txtNonGuarBenefit.Name = "txtNonGuarBenefit"
        Me.txtNonGuarBenefit.ReadOnly = True
        Me.txtNonGuarBenefit.Size = New System.Drawing.Size(264, 20)
        Me.txtNonGuarBenefit.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(73, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Category"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(396, 138)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(114, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Non-guarantee Benefit"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(470, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Benefit"
        '
        'txtFees
        '
        Me.txtFees.Location = New System.Drawing.Point(128, 166)
        Me.txtFees.Name = "txtFees"
        Me.txtFees.ReadOnly = True
        Me.txtFees.Size = New System.Drawing.Size(207, 20)
        Me.txtFees.TabIndex = 8
        '
        'txtBenefit
        '
        Me.txtBenefit.Location = New System.Drawing.Point(516, 4)
        Me.txtBenefit.Multiline = True
        Me.txtBenefit.Name = "txtBenefit"
        Me.txtBenefit.ReadOnly = True
        Me.txtBenefit.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtBenefit.Size = New System.Drawing.Size(264, 99)
        Me.txtBenefit.TabIndex = 5
        Me.txtBenefit.WordWrap = False
        '
        'txtProductTypeEng
        '
        Me.txtProductTypeEng.Location = New System.Drawing.Point(55, 278)
        Me.txtProductTypeEng.Multiline = True
        Me.txtProductTypeEng.Name = "txtProductTypeEng"
        Me.txtProductTypeEng.ReadOnly = True
        Me.txtProductTypeEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtProductTypeEng.Size = New System.Drawing.Size(323, 89)
        Me.txtProductTypeEng.TabIndex = 17
        Me.txtProductTypeEng.WordWrap = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(48, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Premium Type"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(56, 262)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 13)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Product Type (Eng)"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.cmdBrowse)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtGuideUrl)
        Me.Panel1.Controls.Add(Me.chkFees)
        Me.Panel1.Controls.Add(Me.txtRiskChi)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.txtProductTypeChi)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.txtObjectiveChi)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.cboCategory)
        Me.Panel1.Controls.Add(Me.txtRiskEng)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.txtNonGuarBenefit)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtFees)
        Me.Panel1.Controls.Add(Me.txtBenefit)
        Me.Panel1.Controls.Add(Me.txtProductTypeEng)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txtPremiumType)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtObjectiveEng)
        Me.Panel1.Controls.Add(Me.txtLoan)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtDividend)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtOther)
        Me.Panel1.Controls.Add(Me.txtCoupon)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtBonus)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtCSV)
        Me.Panel1.Location = New System.Drawing.Point(3, 75)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(815, 183)
        Me.Panel1.TabIndex = 42
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(728, 184)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(52, 23)
        Me.cmdBrowse.TabIndex = 51
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(418, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Product Guide Url"
        '
        'txtGuideUrl
        '
        Me.txtGuideUrl.Location = New System.Drawing.Point(516, 187)
        Me.txtGuideUrl.Name = "txtGuideUrl"
        Me.txtGuideUrl.ReadOnly = True
        Me.txtGuideUrl.Size = New System.Drawing.Size(206, 20)
        Me.txtGuideUrl.TabIndex = 49
        '
        'txtPremiumType
        '
        Me.txtPremiumType.Location = New System.Drawing.Point(128, 36)
        Me.txtPremiumType.Name = "txtPremiumType"
        Me.txtPremiumType.ReadOnly = True
        Me.txtPremiumType.Size = New System.Drawing.Size(207, 20)
        Me.txtPremiumType.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(91, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Loan"
        '
        'txtObjectiveEng
        '
        Me.txtObjectiveEng.Location = New System.Drawing.Point(55, 386)
        Me.txtObjectiveEng.Multiline = True
        Me.txtObjectiveEng.Name = "txtObjectiveEng"
        Me.txtObjectiveEng.ReadOnly = True
        Me.txtObjectiveEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObjectiveEng.Size = New System.Drawing.Size(319, 89)
        Me.txtObjectiveEng.TabIndex = 19
        Me.txtObjectiveEng.WordWrap = False
        '
        'txtLoan
        '
        Me.txtLoan.Location = New System.Drawing.Point(128, 62)
        Me.txtLoan.Name = "txtLoan"
        Me.txtLoan.ReadOnly = True
        Me.txtLoan.Size = New System.Drawing.Size(207, 20)
        Me.txtLoan.TabIndex = 9
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(52, 370)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 13)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "Product Objective (Eng)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(379, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Dividend (Non-Guarantee)"
        '
        'txtDividend
        '
        Me.txtDividend.Location = New System.Drawing.Point(516, 109)
        Me.txtDividend.Name = "txtDividend"
        Me.txtDividend.ReadOnly = True
        Me.txtDividend.Size = New System.Drawing.Size(264, 20)
        Me.txtDividend.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(78, 117)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Coupon"
        '
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(516, 161)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.ReadOnly = True
        Me.txtOther.Size = New System.Drawing.Size(264, 20)
        Me.txtOther.TabIndex = 16
        '
        'txtCoupon
        '
        Me.txtCoupon.Location = New System.Drawing.Point(128, 114)
        Me.txtCoupon.Name = "txtCoupon"
        Me.txtCoupon.ReadOnly = True
        Me.txtCoupon.Size = New System.Drawing.Size(207, 20)
        Me.txtCoupon.TabIndex = 15
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(472, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Others"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(47, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Special Bonus"
        '
        'txtBonus
        '
        Me.txtBonus.Location = New System.Drawing.Point(128, 140)
        Me.txtBonus.Name = "txtBonus"
        Me.txtBonus.ReadOnly = True
        Me.txtBonus.Size = New System.Drawing.Size(207, 20)
        Me.txtBonus.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(41, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Guarantee CSV"
        '
        'txtCSV
        '
        Me.txtCSV.Location = New System.Drawing.Point(128, 88)
        Me.txtCSV.Name = "txtCSV"
        Me.txtCSV.ReadOnly = True
        Me.txtCSV.Size = New System.Drawing.Size(207, 20)
        Me.txtCSV.TabIndex = 12
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(210, 27)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(58, 13)
        Me.Label28.TabIndex = 48
        Me.Label28.Text = "Charge (%)"
        '
        'txtEarlySurrPeriod
        '
        Me.txtEarlySurrPeriod.Location = New System.Drawing.Point(30, 43)
        Me.txtEarlySurrPeriod.Name = "txtEarlySurrPeriod"
        Me.txtEarlySurrPeriod.ReadOnly = True
        Me.txtEarlySurrPeriod.Size = New System.Drawing.Size(100, 20)
        Me.txtEarlySurrPeriod.TabIndex = 52
        '
        'txtEarlySurCharge
        '
        Me.txtEarlySurCharge.Location = New System.Drawing.Point(213, 43)
        Me.txtEarlySurCharge.Name = "txtEarlySurCharge"
        Me.txtEarlySurCharge.ReadOnly = True
        Me.txtEarlySurCharge.Size = New System.Drawing.Size(55, 20)
        Me.txtEarlySurCharge.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Within Premium Payment Period"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtEarlySurCharge)
        Me.GroupBox1.Controls.Add(Me.txtEarlySurrPeriod)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Location = New System.Drawing.Point(55, 600)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(350, 80)
        Me.GroupBox1.TabIndex = 53
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Early Withdrawal/Surrender Charge (for Horizon II/Altitude II only)"
        '
        'uclPostSalesCallQuestionnaire
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvCoverage)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.fpnQuestion)
        Me.Name = "uclPostSalesCallQuestionnaire"
        Me.Size = New System.Drawing.Size(823, 536)
        CType(Me.dgvCoverage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fpnQuestion As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents dgvCoverage As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkFees As System.Windows.Forms.CheckBox
    Friend WithEvents txtRiskChi As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtProductTypeChi As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtObjectiveChi As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents txtRiskEng As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtNonGuarBenefit As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFees As System.Windows.Forms.TextBox
    Friend WithEvents txtBenefit As System.Windows.Forms.TextBox
    Friend WithEvents txtProductTypeEng As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPremiumType As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtObjectiveEng As System.Windows.Forms.TextBox
    Friend WithEvents txtLoan As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDividend As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents txtCoupon As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtBonus As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCSV As System.Windows.Forms.TextBox
    Friend WithEvents bsProduct As System.Windows.Forms.BindingSource
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtGuideUrl As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEarlySurCharge As System.Windows.Forms.TextBox
    Friend WithEvents txtEarlySurrPeriod As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label

End Class
