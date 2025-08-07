<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_BillingInfo
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtAdminCharge = New System.Windows.Forms.TextBox()
        Me.lblAdminCharge = New System.Windows.Forms.Label()
        Me.txt2ndNetPrmAmt = New System.Windows.Forms.TextBox()
        Me.lbl2ndNetPremAmt = New System.Windows.Forms.Label()
        Me.txt2ndPrmAmt = New System.Windows.Forms.TextBox()
        Me.lbl2ndPremAmt = New System.Windows.Forms.Label()
        Me.dtp2ndPrmDueDate = New System.Windows.Forms.DateTimePicker()
        Me.lbl2ndPrmDueDate = New System.Windows.Forms.Label()
        Me.txtSplitPrmOpt = New System.Windows.Forms.TextBox()
        Me.lblSplitPrmOpt = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtLevyQuotation = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboDrawDay = New System.Windows.Forms.ComboBox()
        Me.cboCurrency = New System.Windows.Forms.ComboBox()
        Me.dtpNBD = New System.Windows.Forms.DateTimePicker()
        Me.dtpPTD = New System.Windows.Forms.DateTimePicker()
        Me.dtpBTD = New System.Windows.Forms.DateTimePicker()
        Me.txtModeP = New System.Windows.Forms.TextBox()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.txtBillType = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpUWDD = New System.Windows.Forms.DateTimePicker()
        Me.dtpFID = New System.Windows.Forms.DateTimePicker()
        Me.dtpRCD = New System.Windows.Forms.DateTimePicker()
        Me.dtpPID = New System.Windows.Forms.DateTimePicker()
        Me.dtpPPD = New System.Windows.Forms.DateTimePicker()
        Me.dtpARD = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDividendOption = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpCooloffDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDispDate = New System.Windows.Forms.DateTimePicker()
        Me.txtDispPerson = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAdminCharge)
        Me.GroupBox1.Controls.Add(Me.lblAdminCharge)
        Me.GroupBox1.Controls.Add(Me.txt2ndNetPrmAmt)
        Me.GroupBox1.Controls.Add(Me.lbl2ndNetPremAmt)
        Me.GroupBox1.Controls.Add(Me.txt2ndPrmAmt)
        Me.GroupBox1.Controls.Add(Me.lbl2ndPremAmt)
        Me.GroupBox1.Controls.Add(Me.dtp2ndPrmDueDate)
        Me.GroupBox1.Controls.Add(Me.lbl2ndPrmDueDate)
        Me.GroupBox1.Controls.Add(Me.txtSplitPrmOpt)
        Me.GroupBox1.Controls.Add(Me.lblSplitPrmOpt)
        Me.GroupBox1.Controls.Add(Me.txtTotalAmount)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtLevyQuotation)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cboDrawDay)
        Me.GroupBox1.Controls.Add(Me.cboCurrency)
        Me.GroupBox1.Controls.Add(Me.dtpNBD)
        Me.GroupBox1.Controls.Add(Me.dtpPTD)
        Me.GroupBox1.Controls.Add(Me.dtpBTD)
        Me.GroupBox1.Controls.Add(Me.txtModeP)
        Me.GroupBox1.Controls.Add(Me.txtMode)
        Me.GroupBox1.Controls.Add(Me.txtBillType)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1008, 105)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Billing"
        '
        'txtAdminCharge
        '
        Me.txtAdminCharge.Location = New System.Drawing.Point(896, 79)
        Me.txtAdminCharge.MaxLength = 500
        Me.txtAdminCharge.Name = "txtAdminCharge"
        Me.txtAdminCharge.ReadOnly = True
        Me.txtAdminCharge.Size = New System.Drawing.Size(100, 20)
        Me.txtAdminCharge.TabIndex = 23
        Me.txtAdminCharge.Visible = False
        '
        'lblAdminCharge
        '
        Me.lblAdminCharge.AutoSize = True
        Me.lblAdminCharge.Location = New System.Drawing.Point(813, 83)
        Me.lblAdminCharge.Name = "lblAdminCharge"
        Me.lblAdminCharge.Size = New System.Drawing.Size(73, 13)
        Me.lblAdminCharge.TabIndex = 22
        Me.lblAdminCharge.Text = "Admin Charge"
        Me.lblAdminCharge.Visible = False
        '
        'txt2ndNetPrmAmt
        '
        Me.txt2ndNetPrmAmt.Location = New System.Drawing.Point(703, 79)
        Me.txt2ndNetPrmAmt.MaxLength = 500
        Me.txt2ndNetPrmAmt.Name = "txt2ndNetPrmAmt"
        Me.txt2ndNetPrmAmt.ReadOnly = True
        Me.txt2ndNetPrmAmt.Size = New System.Drawing.Size(100, 20)
        Me.txt2ndNetPrmAmt.TabIndex = 21
        Me.txt2ndNetPrmAmt.Text = "0.00"
        Me.txt2ndNetPrmAmt.Visible = False
        '
        'lbl2ndNetPremAmt
        '
        Me.lbl2ndNetPremAmt.AutoSize = True
        Me.lbl2ndNetPremAmt.Location = New System.Drawing.Point(579, 83)
        Me.lbl2ndNetPremAmt.Name = "lbl2ndNetPremAmt"
        Me.lbl2ndNetPremAmt.Size = New System.Drawing.Size(114, 13)
        Me.lbl2ndNetPremAmt.TabIndex = 20
        Me.lbl2ndNetPremAmt.Text = "2nd Net Prem. Amount"
        Me.lbl2ndNetPremAmt.Visible = False
        '
        'txt2ndPrmAmt
        '
        Me.txt2ndPrmAmt.Location = New System.Drawing.Point(469, 79)
        Me.txt2ndPrmAmt.MaxLength = 500
        Me.txt2ndPrmAmt.Name = "txt2ndPrmAmt"
        Me.txt2ndPrmAmt.ReadOnly = True
        Me.txt2ndPrmAmt.Size = New System.Drawing.Size(100, 20)
        Me.txt2ndPrmAmt.TabIndex = 19
        Me.txt2ndPrmAmt.Text = "0.00"
        Me.txt2ndPrmAmt.Visible = False
        '
        'lbl2ndPremAmt
        '
        Me.lbl2ndPremAmt.AutoSize = True
        Me.lbl2ndPremAmt.Location = New System.Drawing.Point(365, 83)
        Me.lbl2ndPremAmt.Name = "lbl2ndPremAmt"
        Me.lbl2ndPremAmt.Size = New System.Drawing.Size(94, 13)
        Me.lbl2ndPremAmt.TabIndex = 18
        Me.lbl2ndPremAmt.Text = "2nd Prem. Amount"
        Me.lbl2ndPremAmt.Visible = False
        '
        'dtp2ndPrmDueDate
        '
        Me.dtp2ndPrmDueDate.CustomFormat = "MMM dd, yyyy"
        Me.dtp2ndPrmDueDate.Enabled = False
        Me.dtp2ndPrmDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp2ndPrmDueDate.Location = New System.Drawing.Point(247, 79)
        Me.dtp2ndPrmDueDate.Name = "dtp2ndPrmDueDate"
        Me.dtp2ndPrmDueDate.Size = New System.Drawing.Size(108, 20)
        Me.dtp2ndPrmDueDate.TabIndex = 17
        Me.dtp2ndPrmDueDate.Visible = False
        '
        'lbl2ndPrmDueDate
        '
        Me.lbl2ndPrmDueDate.AutoSize = True
        Me.lbl2ndPrmDueDate.Location = New System.Drawing.Point(133, 83)
        Me.lbl2ndPrmDueDate.Name = "lbl2ndPrmDueDate"
        Me.lbl2ndPrmDueDate.Size = New System.Drawing.Size(104, 13)
        Me.lbl2ndPrmDueDate.TabIndex = 16
        Me.lbl2ndPrmDueDate.Text = "2nd Prem. Due Date"
        Me.lbl2ndPrmDueDate.Visible = False
        '
        'txtSplitPrmOpt
        '
        Me.txtSplitPrmOpt.Location = New System.Drawing.Point(103, 79)
        Me.txtSplitPrmOpt.MaxLength = 500
        Me.txtSplitPrmOpt.Name = "txtSplitPrmOpt"
        Me.txtSplitPrmOpt.ReadOnly = True
        Me.txtSplitPrmOpt.Size = New System.Drawing.Size(20, 20)
        Me.txtSplitPrmOpt.TabIndex = 15
        Me.txtSplitPrmOpt.Visible = False
        '
        'lblSplitPrmOpt
        '
        Me.lblSplitPrmOpt.AutoSize = True
        Me.lblSplitPrmOpt.Location = New System.Drawing.Point(2, 83)
        Me.lblSplitPrmOpt.Name = "lblSplitPrmOpt"
        Me.lblSplitPrmOpt.Size = New System.Drawing.Size(91, 13)
        Me.lblSplitPrmOpt.TabIndex = 14
        Me.lblSplitPrmOpt.Text = "Split Prem. Option"
        Me.lblSplitPrmOpt.Visible = False
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Location = New System.Drawing.Point(909, 46)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.ReadOnly = True
        Me.txtTotalAmount.Size = New System.Drawing.Size(93, 20)
        Me.txtTotalAmount.TabIndex = 13
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(838, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 13)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "Total Amount:"
        '
        'txtLevyQuotation
        '
        Me.txtLevyQuotation.Location = New System.Drawing.Point(909, 19)
        Me.txtLevyQuotation.Name = "txtLevyQuotation"
        Me.txtLevyQuotation.ReadOnly = True
        Me.txtLevyQuotation.Size = New System.Drawing.Size(93, 20)
        Me.txtLevyQuotation.TabIndex = 11
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(840, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(33, 13)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "Levy:"
        '
        'cboDrawDay
        '
        Me.cboDrawDay.Enabled = False
        Me.cboDrawDay.FormattingEnabled = True
        Me.cboDrawDay.Location = New System.Drawing.Point(724, 17)
        Me.cboDrawDay.Name = "cboDrawDay"
        Me.cboDrawDay.Size = New System.Drawing.Size(113, 21)
        Me.cboDrawDay.TabIndex = 9
        '
        'cboCurrency
        '
        Me.cboCurrency.Enabled = False
        Me.cboCurrency.FormattingEnabled = True
        Me.cboCurrency.Location = New System.Drawing.Point(505, 16)
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.Size = New System.Drawing.Size(136, 21)
        Me.cboCurrency.TabIndex = 4
        '
        'dtpNBD
        '
        Me.dtpNBD.CustomFormat = "MMM dd, yyyy"
        Me.dtpNBD.Enabled = False
        Me.dtpNBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNBD.Location = New System.Drawing.Point(506, 49)
        Me.dtpNBD.Name = "dtpNBD"
        Me.dtpNBD.Size = New System.Drawing.Size(136, 20)
        Me.dtpNBD.TabIndex = 7
        '
        'dtpPTD
        '
        Me.dtpPTD.CustomFormat = "MMM dd, yyyy"
        Me.dtpPTD.Enabled = False
        Me.dtpPTD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPTD.Location = New System.Drawing.Point(70, 49)
        Me.dtpPTD.Name = "dtpPTD"
        Me.dtpPTD.Size = New System.Drawing.Size(162, 20)
        Me.dtpPTD.TabIndex = 5
        '
        'dtpBTD
        '
        Me.dtpBTD.CustomFormat = "MMM dd, yyyy"
        Me.dtpBTD.Enabled = False
        Me.dtpBTD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBTD.Location = New System.Drawing.Point(296, 49)
        Me.dtpBTD.Name = "dtpBTD"
        Me.dtpBTD.Size = New System.Drawing.Size(136, 20)
        Me.dtpBTD.TabIndex = 6
        '
        'txtModeP
        '
        Me.txtModeP.Location = New System.Drawing.Point(724, 49)
        Me.txtModeP.Name = "txtModeP"
        Me.txtModeP.ReadOnly = True
        Me.txtModeP.Size = New System.Drawing.Size(113, 20)
        Me.txtModeP.TabIndex = 8
        '
        'txtMode
        '
        Me.txtMode.Location = New System.Drawing.Point(296, 19)
        Me.txtMode.Name = "txtMode"
        Me.txtMode.ReadOnly = True
        Me.txtMode.Size = New System.Drawing.Size(136, 20)
        Me.txtMode.TabIndex = 2
        '
        'txtBillType
        '
        Me.txtBillType.Location = New System.Drawing.Point(69, 22)
        Me.txtBillType.Name = "txtBillType"
        Me.txtBillType.ReadOnly = True
        Me.txtBillType.Size = New System.Drawing.Size(162, 20)
        Me.txtBillType.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(643, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Mode Premium:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(433, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Next Bill Date:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(234, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Bill To Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(434, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Currency:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(643, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Draw Day:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(234, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Mode:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Pay To Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bill Type:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpUWDD)
        Me.GroupBox2.Controls.Add(Me.dtpFID)
        Me.GroupBox2.Controls.Add(Me.dtpRCD)
        Me.GroupBox2.Controls.Add(Me.dtpPID)
        Me.GroupBox2.Controls.Add(Me.dtpPPD)
        Me.GroupBox2.Controls.Add(Me.dtpARD)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1008, 85)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Policy Date Information"
        '
        'dtpUWDD
        '
        Me.dtpUWDD.CustomFormat = "MMM dd, yyyy"
        Me.dtpUWDD.Enabled = False
        Me.dtpUWDD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUWDD.Location = New System.Drawing.Point(643, 52)
        Me.dtpUWDD.Name = "dtpUWDD"
        Me.dtpUWDD.Size = New System.Drawing.Size(136, 20)
        Me.dtpUWDD.TabIndex = 6
        '
        'dtpFID
        '
        Me.dtpFID.CustomFormat = "MMM dd, yyyy"
        Me.dtpFID.Enabled = False
        Me.dtpFID.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFID.Location = New System.Drawing.Point(645, 23)
        Me.dtpFID.Name = "dtpFID"
        Me.dtpFID.Size = New System.Drawing.Size(136, 20)
        Me.dtpFID.TabIndex = 3
        '
        'dtpRCD
        '
        Me.dtpRCD.CustomFormat = "MMM dd, yyyy"
        Me.dtpRCD.Enabled = False
        Me.dtpRCD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRCD.Location = New System.Drawing.Point(112, 18)
        Me.dtpRCD.Name = "dtpRCD"
        Me.dtpRCD.Size = New System.Drawing.Size(136, 20)
        Me.dtpRCD.TabIndex = 1
        '
        'dtpPID
        '
        Me.dtpPID.CustomFormat = "MMM dd, yyyy"
        Me.dtpPID.Enabled = False
        Me.dtpPID.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPID.Location = New System.Drawing.Point(388, 22)
        Me.dtpPID.Name = "dtpPID"
        Me.dtpPID.Size = New System.Drawing.Size(136, 20)
        Me.dtpPID.TabIndex = 2
        '
        'dtpPPD
        '
        Me.dtpPPD.CustomFormat = "MMM dd, yyyy"
        Me.dtpPPD.Enabled = False
        Me.dtpPPD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPPD.Location = New System.Drawing.Point(112, 49)
        Me.dtpPPD.Name = "dtpPPD"
        Me.dtpPPD.Size = New System.Drawing.Size(136, 20)
        Me.dtpPPD.TabIndex = 4
        '
        'dtpARD
        '
        Me.dtpARD.CustomFormat = "MMM dd, yyyy"
        Me.dtpARD.Enabled = False
        Me.dtpARD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpARD.Location = New System.Drawing.Point(388, 49)
        Me.dtpARD.Name = "dtpARD"
        Me.dtpARD.Size = New System.Drawing.Size(136, 20)
        Me.dtpARD.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(536, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "U/W Decision Date:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(259, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Application Recev Date:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(536, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "First Inforce Date:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(259, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Policy Inforce Date:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 52)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(108, 13)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Policy Proposal Date:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 25)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Rick Comm. Date:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDividendOption)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.dtpCooloffDate)
        Me.GroupBox3.Controls.Add(Me.dtpDispDate)
        Me.GroupBox3.Controls.Add(Me.txtDispPerson)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 205)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1008, 46)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Policy Option and Information"
        '
        'txtDividendOption
        '
        Me.txtDividendOption.Location = New System.Drawing.Point(887, 16)
        Me.txtDividendOption.Name = "txtDividendOption"
        Me.txtDividendOption.ReadOnly = True
        Me.txtDividendOption.Size = New System.Drawing.Size(115, 20)
        Me.txtDividendOption.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(801, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 13)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "Diviend Option:"
        '
        'dtpCooloffDate
        '
        Me.dtpCooloffDate.CustomFormat = "MMM dd, yyyy"
        Me.dtpCooloffDate.Enabled = False
        Me.dtpCooloffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCooloffDate.Location = New System.Drawing.Point(646, 19)
        Me.dtpCooloffDate.Name = "dtpCooloffDate"
        Me.dtpCooloffDate.Size = New System.Drawing.Size(136, 20)
        Me.dtpCooloffDate.TabIndex = 6
        '
        'dtpDispDate
        '
        Me.dtpDispDate.CustomFormat = "MMM dd, yyyy"
        Me.dtpDispDate.Enabled = False
        Me.dtpDispDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDispDate.Location = New System.Drawing.Point(90, 21)
        Me.dtpDispDate.Name = "dtpDispDate"
        Me.dtpDispDate.Size = New System.Drawing.Size(162, 20)
        Me.dtpDispDate.TabIndex = 5
        '
        'txtDispPerson
        '
        Me.txtDispPerson.Location = New System.Drawing.Point(386, 19)
        Me.txtDispPerson.Name = "txtDispPerson"
        Me.txtDispPerson.ReadOnly = True
        Me.txtDispPerson.Size = New System.Drawing.Size(136, 20)
        Me.txtDispPerson.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(552, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 13)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Cooling Off Date:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(292, 23)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Dispatch Person:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Dispatch Date:"
        '
        'ctrl_BillingInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrl_BillingInfo"
        Me.Size = New System.Drawing.Size(1024, 272)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpPTD As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpBTD As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtModeP As System.Windows.Forms.TextBox
    Friend WithEvents txtMode As System.Windows.Forms.TextBox
    Friend WithEvents txtBillType As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents dtpNBD As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpPID As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPPD As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpARD As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDispPerson As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpFID As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRCD As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUWDD As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboDrawDay As System.Windows.Forms.ComboBox
    Friend WithEvents dtpDispDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCooloffDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDividendOption As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtLevyQuotation As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblSplitPrmOpt As System.Windows.Forms.Label
    Friend WithEvents txtSplitPrmOpt As System.Windows.Forms.TextBox
    Friend WithEvents txt2ndPrmAmt As System.Windows.Forms.TextBox
    Friend WithEvents lbl2ndPremAmt As System.Windows.Forms.Label
    Friend WithEvents dtp2ndPrmDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl2ndPrmDueDate As System.Windows.Forms.Label
    Friend WithEvents txt2ndNetPrmAmt As System.Windows.Forms.TextBox
    Friend WithEvents lbl2ndNetPremAmt As System.Windows.Forms.Label
    Friend WithEvents txtAdminCharge As System.Windows.Forms.TextBox
    Friend WithEvents lblAdminCharge As System.Windows.Forms.Label

End Class
