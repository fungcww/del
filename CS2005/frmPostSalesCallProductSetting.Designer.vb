<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostSalesCallProductSetting
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
        Me.components = New System.ComponentModel.Container
        Me.dgvProduct = New System.Windows.Forms.DataGridView
        Me.colProductID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanNameEng = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanNameChi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdProductCancel = New System.Windows.Forms.Button
        Me.cmdProductEdit = New System.Windows.Forms.Button
        Me.cmdProductSave = New System.Windows.Forms.Button
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.txtRiskEng = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtNonGuarBenefit = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtFees = New System.Windows.Forms.TextBox
        Me.txtObjectiveEng = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtProductTypeEng = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtGuideUrl = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCSV = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtBonus = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtCoupon = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtDividend = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtLoan = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtPremiumType = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBenefit = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPlanNameChi = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPlanNameEng = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProductID = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dgvQuestion = New System.Windows.Forms.DataGridView
        Me.cmdQuestionAdd = New System.Windows.Forms.Button
        Me.cmdQuestionRemove = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdPlanNameFilter = New System.Windows.Forms.Button
        Me.txtPlanNameFilter = New System.Windows.Forms.TextBox
        Me.cmdProductIdFilter = New System.Windows.Forms.Button
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtProductIDFilter = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.cmdProductAdd = New System.Windows.Forms.Button
        Me.cmdProductDelete = New System.Windows.Forms.Button
        Me.cmdProductCopy = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label18 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEarlySurCharge = New System.Windows.Forms.TextBox
        Me.txtEarlySurrPeriod = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.chkFees = New System.Windows.Forms.CheckBox
        Me.txtRiskChi = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtProductTypeChi = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtObjectiveChi = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.bsProduct = New System.Windows.Forms.BindingSource(Me.components)
        Me.bsQuestion = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.colQuestionNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colQuestion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colVC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvQuestion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bsQuestion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvProduct
        '
        Me.dgvProduct.AllowUserToAddRows = False
        Me.dgvProduct.AllowUserToDeleteRows = False
        Me.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduct.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colProductID, Me.colPlanNameEng, Me.colPlanNameChi})
        Me.dgvProduct.Location = New System.Drawing.Point(6, 19)
        Me.dgvProduct.MultiSelect = False
        Me.dgvProduct.Name = "dgvProduct"
        Me.dgvProduct.ReadOnly = True
        Me.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProduct.Size = New System.Drawing.Size(915, 81)
        Me.dgvProduct.TabIndex = 3
        '
        'colProductID
        '
        Me.colProductID.DataPropertyName = "cswpsd_ProductID"
        Me.colProductID.Frozen = True
        Me.colProductID.HeaderText = "Product ID"
        Me.colProductID.Name = "colProductID"
        Me.colProductID.ReadOnly = True
        '
        'colPlanNameEng
        '
        Me.colPlanNameEng.DataPropertyName = "Description"
        Me.colPlanNameEng.Frozen = True
        Me.colPlanNameEng.HeaderText = "Product Name (Eng)"
        Me.colPlanNameEng.Name = "colPlanNameEng"
        Me.colPlanNameEng.ReadOnly = True
        Me.colPlanNameEng.Width = 400
        '
        'colPlanNameChi
        '
        Me.colPlanNameChi.DataPropertyName = "ChineseDescription"
        Me.colPlanNameChi.HeaderText = "Product Name (Chi)"
        Me.colPlanNameChi.Name = "colPlanNameChi"
        Me.colPlanNameChi.ReadOnly = True
        Me.colPlanNameChi.Width = 300
        '
        'cmdProductCancel
        '
        Me.cmdProductCancel.Location = New System.Drawing.Point(927, 382)
        Me.cmdProductCancel.Name = "cmdProductCancel"
        Me.cmdProductCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductCancel.TabIndex = 3
        Me.cmdProductCancel.Text = "Cancel"
        Me.cmdProductCancel.UseVisualStyleBackColor = True
        '
        'cmdProductEdit
        '
        Me.cmdProductEdit.Location = New System.Drawing.Point(765, 382)
        Me.cmdProductEdit.Name = "cmdProductEdit"
        Me.cmdProductEdit.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductEdit.TabIndex = 1
        Me.cmdProductEdit.Text = "Edit"
        Me.cmdProductEdit.UseVisualStyleBackColor = True
        '
        'cmdProductSave
        '
        Me.cmdProductSave.Location = New System.Drawing.Point(846, 382)
        Me.cmdProductSave.Name = "cmdProductSave"
        Me.cmdProductSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductSave.TabIndex = 2
        Me.cmdProductSave.Text = "Save"
        Me.cmdProductSave.UseVisualStyleBackColor = True
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(117, 50)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(178, 21)
        Me.cboCategory.TabIndex = 3
        '
        'txtRiskEng
        '
        Me.txtRiskEng.Location = New System.Drawing.Point(31, 507)
        Me.txtRiskEng.Multiline = True
        Me.txtRiskEng.Name = "txtRiskEng"
        Me.txtRiskEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRiskEng.Size = New System.Drawing.Size(319, 90)
        Me.txtRiskEng.TabIndex = 21
        Me.txtRiskEng.WordWrap = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(28, 491)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 13)
        Me.Label20.TabIndex = 37
        Me.Label20.Text = "Product Risk (Eng)"
        '
        'txtNonGuarBenefit
        '
        Me.txtNonGuarBenefit.Location = New System.Drawing.Point(450, 113)
        Me.txtNonGuarBenefit.Name = "txtNonGuarBenefit"
        Me.txtNonGuarBenefit.Size = New System.Drawing.Size(162, 20)
        Me.txtNonGuarBenefit.TabIndex = 13
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(330, 116)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(114, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Non-guarantee Benefit"
        '
        'txtFees
        '
        Me.txtFees.Location = New System.Drawing.Point(450, 50)
        Me.txtFees.Name = "txtFees"
        Me.txtFees.Size = New System.Drawing.Size(162, 20)
        Me.txtFees.TabIndex = 8
        '
        'txtObjectiveEng
        '
        Me.txtObjectiveEng.Location = New System.Drawing.Point(31, 383)
        Me.txtObjectiveEng.Multiline = True
        Me.txtObjectiveEng.Name = "txtObjectiveEng"
        Me.txtObjectiveEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObjectiveEng.Size = New System.Drawing.Size(319, 105)
        Me.txtObjectiveEng.TabIndex = 19
        Me.txtObjectiveEng.WordWrap = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(28, 367)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 13)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "Product Objective (Eng)"
        '
        'txtProductTypeEng
        '
        Me.txtProductTypeEng.Location = New System.Drawing.Point(31, 257)
        Me.txtProductTypeEng.Multiline = True
        Me.txtProductTypeEng.Name = "txtProductTypeEng"
        Me.txtProductTypeEng.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtProductTypeEng.Size = New System.Drawing.Size(323, 107)
        Me.txtProductTypeEng.TabIndex = 17
        Me.txtProductTypeEng.WordWrap = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(32, 241)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 13)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Product Type (Eng)"
        '
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(450, 139)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(162, 20)
        Me.txtOther.TabIndex = 16
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(406, 142)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Others"
        '
        'txtGuideUrl
        '
        Me.txtGuideUrl.Location = New System.Drawing.Point(117, 191)
        Me.txtGuideUrl.Name = "txtGuideUrl"
        Me.txtGuideUrl.Size = New System.Drawing.Size(178, 20)
        Me.txtGuideUrl.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 194)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 13)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "Product Guide URL"
        '
        'txtCSV
        '
        Me.txtCSV.Location = New System.Drawing.Point(117, 139)
        Me.txtCSV.Name = "txtCSV"
        Me.txtCSV.Size = New System.Drawing.Size(178, 20)
        Me.txtCSV.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(30, 142)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Guarantee CSV"
        '
        'txtBonus
        '
        Me.txtBonus.Location = New System.Drawing.Point(450, 165)
        Me.txtBonus.Name = "txtBonus"
        Me.txtBonus.Size = New System.Drawing.Size(162, 20)
        Me.txtBonus.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(369, 168)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Special Bonus"
        '
        'txtCoupon
        '
        Me.txtCoupon.Location = New System.Drawing.Point(117, 165)
        Me.txtCoupon.Name = "txtCoupon"
        Me.txtCoupon.Size = New System.Drawing.Size(178, 20)
        Me.txtCoupon.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(67, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Coupon"
        '
        'txtDividend
        '
        Me.txtDividend.Location = New System.Drawing.Point(450, 77)
        Me.txtDividend.Name = "txtDividend"
        Me.txtDividend.Size = New System.Drawing.Size(162, 20)
        Me.txtDividend.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(313, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Dividend (Non-Guarantee)"
        '
        'txtLoan
        '
        Me.txtLoan.Location = New System.Drawing.Point(117, 113)
        Me.txtLoan.Name = "txtLoan"
        Me.txtLoan.Size = New System.Drawing.Size(178, 20)
        Me.txtLoan.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(80, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Loan"
        '
        'txtPremiumType
        '
        Me.txtPremiumType.Location = New System.Drawing.Point(117, 77)
        Me.txtPremiumType.Name = "txtPremiumType"
        Me.txtPremiumType.Size = New System.Drawing.Size(178, 20)
        Me.txtPremiumType.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(37, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Premium Type"
        '
        'txtBenefit
        '
        Me.txtBenefit.Location = New System.Drawing.Point(692, 46)
        Me.txtBenefit.Multiline = True
        Me.txtBenefit.Name = "txtBenefit"
        Me.txtBenefit.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtBenefit.Size = New System.Drawing.Size(278, 139)
        Me.txtBenefit.TabIndex = 5
        Me.txtBenefit.WordWrap = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(642, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Benefit"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(62, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Category"
        '
        'txtPlanNameChi
        '
        Me.txtPlanNameChi.Location = New System.Drawing.Point(692, 15)
        Me.txtPlanNameChi.Name = "txtPlanNameChi"
        Me.txtPlanNameChi.ReadOnly = True
        Me.txtPlanNameChi.Size = New System.Drawing.Size(278, 20)
        Me.txtPlanNameChi.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(588, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Product Name (Chi)"
        '
        'txtPlanNameEng
        '
        Me.txtPlanNameEng.Location = New System.Drawing.Point(289, 15)
        Me.txtPlanNameEng.Name = "txtPlanNameEng"
        Me.txtPlanNameEng.ReadOnly = True
        Me.txtPlanNameEng.Size = New System.Drawing.Size(290, 20)
        Me.txtPlanNameEng.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(180, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Product Name (Eng)"
        '
        'txtProductID
        '
        Me.txtProductID.Location = New System.Drawing.Point(91, 15)
        Me.txtProductID.Name = "txtProductID"
        Me.txtProductID.ReadOnly = True
        Me.txtProductID.Size = New System.Drawing.Size(69, 20)
        Me.txtProductID.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Product ID"
        '
        'dgvQuestion
        '
        Me.dgvQuestion.AllowUserToAddRows = False
        Me.dgvQuestion.AllowUserToDeleteRows = False
        Me.dgvQuestion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvQuestion.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colQuestionNo, Me.colQuestion, Me.colVC, Me.colSM, Me.Column1})
        Me.dgvQuestion.Location = New System.Drawing.Point(15, 19)
        Me.dgvQuestion.MultiSelect = False
        Me.dgvQuestion.Name = "dgvQuestion"
        Me.dgvQuestion.ReadOnly = True
        Me.dgvQuestion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvQuestion.Size = New System.Drawing.Size(898, 158)
        Me.dgvQuestion.TabIndex = 3
        '
        'cmdQuestionAdd
        '
        Me.cmdQuestionAdd.Location = New System.Drawing.Point(919, 19)
        Me.cmdQuestionAdd.Name = "cmdQuestionAdd"
        Me.cmdQuestionAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdQuestionAdd.TabIndex = 0
        Me.cmdQuestionAdd.Text = "Add"
        Me.cmdQuestionAdd.UseVisualStyleBackColor = True
        '
        'cmdQuestionRemove
        '
        Me.cmdQuestionRemove.Location = New System.Drawing.Point(919, 49)
        Me.cmdQuestionRemove.Name = "cmdQuestionRemove"
        Me.cmdQuestionRemove.Size = New System.Drawing.Size(75, 23)
        Me.cmdQuestionRemove.TabIndex = 1
        Me.cmdQuestionRemove.Text = "Remove"
        Me.cmdQuestionRemove.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdPlanNameFilter)
        Me.GroupBox2.Controls.Add(Me.txtPlanNameFilter)
        Me.GroupBox2.Controls.Add(Me.cmdProductIdFilter)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.txtProductIDFilter)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1008, 40)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Find"
        '
        'cmdPlanNameFilter
        '
        Me.cmdPlanNameFilter.Location = New System.Drawing.Point(598, 13)
        Me.cmdPlanNameFilter.Name = "cmdPlanNameFilter"
        Me.cmdPlanNameFilter.Size = New System.Drawing.Size(54, 20)
        Me.cmdPlanNameFilter.TabIndex = 3
        Me.cmdPlanNameFilter.Text = "Filter"
        Me.cmdPlanNameFilter.UseVisualStyleBackColor = True
        '
        'txtPlanNameFilter
        '
        Me.txtPlanNameFilter.Location = New System.Drawing.Point(457, 13)
        Me.txtPlanNameFilter.Name = "txtPlanNameFilter"
        Me.txtPlanNameFilter.Size = New System.Drawing.Size(135, 20)
        Me.txtPlanNameFilter.TabIndex = 2
        '
        'cmdProductIdFilter
        '
        Me.cmdProductIdFilter.Location = New System.Drawing.Point(248, 14)
        Me.cmdProductIdFilter.Name = "cmdProductIdFilter"
        Me.cmdProductIdFilter.Size = New System.Drawing.Size(54, 20)
        Me.cmdProductIdFilter.TabIndex = 1
        Me.cmdProductIdFilter.Text = "Filter"
        Me.cmdProductIdFilter.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(348, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(103, 13)
        Me.Label23.TabIndex = 16
        Me.Label23.Text = "Product Name (Eng)"
        '
        'txtProductIDFilter
        '
        Me.txtProductIDFilter.Location = New System.Drawing.Point(107, 14)
        Me.txtProductIDFilter.Name = "txtProductIDFilter"
        Me.txtProductIDFilter.Size = New System.Drawing.Size(135, 20)
        Me.txtProductIDFilter.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(43, 17)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(58, 13)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "Product ID"
        '
        'cmdProductAdd
        '
        Me.cmdProductAdd.Location = New System.Drawing.Point(927, 19)
        Me.cmdProductAdd.Name = "cmdProductAdd"
        Me.cmdProductAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductAdd.TabIndex = 0
        Me.cmdProductAdd.Text = "Add"
        Me.cmdProductAdd.UseVisualStyleBackColor = True
        '
        'cmdProductDelete
        '
        Me.cmdProductDelete.Location = New System.Drawing.Point(927, 48)
        Me.cmdProductDelete.Name = "cmdProductDelete"
        Me.cmdProductDelete.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductDelete.TabIndex = 1
        Me.cmdProductDelete.Text = "Delete"
        Me.cmdProductDelete.UseVisualStyleBackColor = True
        '
        'cmdProductCopy
        '
        Me.cmdProductCopy.Location = New System.Drawing.Point(927, 77)
        Me.cmdProductCopy.Name = "cmdProductCopy"
        Me.cmdProductCopy.Size = New System.Drawing.Size(75, 23)
        Me.cmdProductCopy.TabIndex = 2
        Me.cmdProductCopy.Text = "Copy"
        Me.cmdProductCopy.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.chkFees)
        Me.Panel1.Controls.Add(Me.txtRiskChi)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.txtProductTypeChi)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.txtObjectiveChi)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtProductID)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cboCategory)
        Me.Panel1.Controls.Add(Me.txtPlanNameEng)
        Me.Panel1.Controls.Add(Me.txtRiskEng)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.txtPlanNameChi)
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
        Me.Panel1.Controls.Add(Me.txtGuideUrl)
        Me.Panel1.Controls.Add(Me.txtBonus)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtCSV)
        Me.Panel1.Location = New System.Drawing.Point(6, 106)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(996, 270)
        Me.Panel1.TabIndex = 4
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(689, 615)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(19, 13)
        Me.Label18.TabIndex = 55
        Me.Label18.Text = "    "
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.txtEarlySurCharge)
        Me.GroupBox4.Controls.Add(Me.txtEarlySurrPeriod)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(692, 255)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(278, 94)
        Me.GroupBox4.TabIndex = 54
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Early Withdrawal/Surrender Charge (for Horizon II/Altitude II only)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Within Premium Payment Period"
        '
        'txtEarlySurCharge
        '
        Me.txtEarlySurCharge.Location = New System.Drawing.Point(179, 61)
        Me.txtEarlySurCharge.Name = "txtEarlySurCharge"
        Me.txtEarlySurCharge.Size = New System.Drawing.Size(55, 20)
        Me.txtEarlySurCharge.TabIndex = 4
        '
        'txtEarlySurrPeriod
        '
        Me.txtEarlySurrPeriod.Location = New System.Drawing.Point(179, 35)
        Me.txtEarlySurrPeriod.Name = "txtEarlySurrPeriod"
        Me.txtEarlySurrPeriod.Size = New System.Drawing.Size(55, 20)
        Me.txtEarlySurrPeriod.TabIndex = 52
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(115, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 13)
        Me.Label17.TabIndex = 48
        Me.Label17.Text = "Charge (%)"
        '
        'chkFees
        '
        Me.chkFees.AutoSize = True
        Me.chkFees.Location = New System.Drawing.Point(349, 52)
        Me.chkFees.Name = "chkFees"
        Me.chkFees.Size = New System.Drawing.Size(95, 17)
        Me.chkFees.TabIndex = 7
        Me.chkFees.Text = "Fee && Charges"
        Me.chkFees.UseVisualStyleBackColor = True
        '
        'txtRiskChi
        '
        Me.txtRiskChi.Location = New System.Drawing.Point(359, 507)
        Me.txtRiskChi.Multiline = True
        Me.txtRiskChi.Name = "txtRiskChi"
        Me.txtRiskChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRiskChi.Size = New System.Drawing.Size(319, 90)
        Me.txtRiskChi.TabIndex = 22
        Me.txtRiskChi.WordWrap = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(356, 491)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(92, 13)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "Product Risk (Chi)"
        '
        'txtProductTypeChi
        '
        Me.txtProductTypeChi.Location = New System.Drawing.Point(359, 257)
        Me.txtProductTypeChi.Multiline = True
        Me.txtProductTypeChi.Name = "txtProductTypeChi"
        Me.txtProductTypeChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtProductTypeChi.Size = New System.Drawing.Size(323, 107)
        Me.txtProductTypeChi.TabIndex = 18
        Me.txtProductTypeChi.WordWrap = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(360, 241)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(95, 13)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "Product Type (Chi)"
        '
        'txtObjectiveChi
        '
        Me.txtObjectiveChi.Location = New System.Drawing.Point(359, 383)
        Me.txtObjectiveChi.Multiline = True
        Me.txtObjectiveChi.Name = "txtObjectiveChi"
        Me.txtObjectiveChi.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtObjectiveChi.Size = New System.Drawing.Size(319, 105)
        Me.txtObjectiveChi.TabIndex = 20
        Me.txtObjectiveChi.WordWrap = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(356, 367)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(116, 13)
        Me.Label25.TabIndex = 42
        Me.Label25.Text = "Product Objective (Chi)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvQuestion)
        Me.GroupBox1.Controls.Add(Me.cmdQuestionAdd)
        Me.GroupBox1.Controls.Add(Me.cmdQuestionRemove)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 475)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1009, 183)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Questions"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dgvProduct)
        Me.GroupBox3.Controls.Add(Me.cmdProductAdd)
        Me.GroupBox3.Controls.Add(Me.cmdProductCancel)
        Me.GroupBox3.Controls.Add(Me.cmdProductSave)
        Me.GroupBox3.Controls.Add(Me.Panel1)
        Me.GroupBox3.Controls.Add(Me.cmdProductDelete)
        Me.GroupBox3.Controls.Add(Me.cmdProductEdit)
        Me.GroupBox3.Controls.Add(Me.cmdProductCopy)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 58)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1009, 411)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Products"
        '
        'colQuestionNo
        '
        Me.colQuestionNo.DataPropertyName = "cswpsq_question_no"
        Me.colQuestionNo.HeaderText = "No."
        Me.colQuestionNo.Name = "colQuestionNo"
        Me.colQuestionNo.ReadOnly = True
        Me.colQuestionNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colQuestion
        '
        Me.colQuestion.DataPropertyName = "cswpsq_description"
        Me.colQuestion.HeaderText = "Question"
        Me.colQuestion.Name = "colQuestion"
        Me.colQuestion.ReadOnly = True
        Me.colQuestion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colQuestion.Width = 400
        '
        'colVC
        '
        Me.colVC.DataPropertyName = "cswpsq_vc"
        Me.colVC.HeaderText = "VC"
        Me.colVC.Name = "colVC"
        Me.colVC.ReadOnly = True
        Me.colVC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colSM
        '
        Me.colSM.DataPropertyName = "cswpsq_sm"
        Me.colSM.HeaderText = "SM"
        Me.colSM.Name = "colSM"
        Me.colSM.ReadOnly = True
        Me.colSM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "cswpsq_no_fna"
        Me.Column1.HeaderText = "No FNA"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'frmPostSalesCallProductSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1034, 670)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "frmPostSalesCallProductSetting"
        Me.Text = "Post Sales Call Product Setting"
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvQuestion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.bsProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bsQuestion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvProduct As System.Windows.Forms.DataGridView
    Friend WithEvents txtPlanNameEng As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProductID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPlanNameChi As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPremiumType As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBenefit As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCSV As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBonus As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCoupon As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDividend As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLoan As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRiskEng As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtNonGuarBenefit As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtFees As System.Windows.Forms.TextBox
    Friend WithEvents txtObjectiveEng As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtProductTypeEng As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtGuideUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents dgvQuestion As System.Windows.Forms.DataGridView
    Friend WithEvents cmdQuestionAdd As System.Windows.Forms.Button
    Friend WithEvents cmdQuestionRemove As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdPlanNameFilter As System.Windows.Forms.Button
    Friend WithEvents txtPlanNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents cmdProductIdFilter As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtProductIDFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmdProductCancel As System.Windows.Forms.Button
    Friend WithEvents cmdProductEdit As System.Windows.Forms.Button
    Friend WithEvents cmdProductSave As System.Windows.Forms.Button
    Friend WithEvents cmdProductAdd As System.Windows.Forms.Button
    Friend WithEvents cmdProductDelete As System.Windows.Forms.Button
    Friend WithEvents cmdProductCopy As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtRiskChi As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtProductTypeChi As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtObjectiveChi As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents chkFees As System.Windows.Forms.CheckBox
    Friend WithEvents bsProduct As System.Windows.Forms.BindingSource
    Friend WithEvents colProductID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanNameEng As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanNameChi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bsQuestion As System.Windows.Forms.BindingSource
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEarlySurCharge As System.Windows.Forms.TextBox
    Friend WithEvents txtEarlySurrPeriod As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents colQuestionNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQuestion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
