<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class uclServiceLog_Asur_WorkAround
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.chkMCV = New System.Windows.Forms.CheckBox()
        Me.chkIdVerify = New System.Windows.Forms.CheckBox()
        Me.rad_SuitabilityMisMatch = New System.Windows.Forms.RadioButton()
        Me.gboPolicy = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbEB = New System.Windows.Forms.RadioButton()
        Me.rbGI = New System.Windows.Forms.RadioButton()
        Me.rbLife = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rad_ILASPostSalseCall = New System.Windows.Forms.RadioButton()
        Me.chkReminder = New System.Windows.Forms.CheckBox()
        Me.rad_VCPostSalesCall = New System.Windows.Forms.RadioButton()
        Me.rad_NVCWelcomeCall = New System.Windows.Forms.RadioButton()
        Me.lbl_PostCallCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lbl_PostCallStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbl_InforceDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkACC = New System.Windows.Forms.CheckBox()
        Me.chkFCR = New System.Windows.Forms.CheckBox()
        Me.chkPolicyAlert = New System.Windows.Forms.CheckBox()
        Me.btnSaveC = New System.Windows.Forms.Button()
        Me.txtDownloadPostSalesCall = New System.Windows.Forms.Button()
        Me.hidSender = New System.Windows.Forms.TextBox()
        Me.hidCustID = New System.Windows.Forms.TextBox()
        Me.hidPolicy = New System.Windows.Forms.TextBox()
        Me.dtReminder = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtInitial = New System.Windows.Forms.DateTimePicker()
        Me.cbReceiver = New System.Windows.Forms.ComboBox()
        Me.cbInitiator = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbReceiver = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkAES = New System.Windows.Forms.CheckBox()
        Me.dgSrvLog = New System.Windows.Forms.DataGrid()
        Me.txtReminder = New System.Windows.Forms.TextBox()
        Me.cbEventCat = New System.Windows.Forms.ComboBox()
        Me.cbEventDetail = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.grpServiceEvent = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.cbMedium = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtPolicyAlert = New System.Windows.Forms.TextBox()
        Me.btncustlogweb = New System.Windows.Forms.Button()
        Me.hidSrvEvtNo = New System.Windows.Forms.TextBox()
        Me.gboCustomer = New System.Windows.Forms.GroupBox()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gboPolicy.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpServiceEvent.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gboCustomer.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkMCV
        '
        Me.chkMCV.AutoSize = True
        Me.chkMCV.BackColor = System.Drawing.Color.Transparent
        Me.chkMCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkMCV.ForeColor = System.Drawing.Color.Red
        Me.chkMCV.Location = New System.Drawing.Point(1566, 266)
        Me.chkMCV.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMCV.Name = "chkMCV"
        Me.chkMCV.Size = New System.Drawing.Size(87, 29)
        Me.chkMCV.TabIndex = 72
        Me.chkMCV.Text = "MCV"
        Me.chkMCV.UseVisualStyleBackColor = False
        '
        'chkIdVerify
        '
        Me.chkIdVerify.Location = New System.Drawing.Point(118, 272)
        Me.chkIdVerify.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIdVerify.Name = "chkIdVerify"
        Me.chkIdVerify.Size = New System.Drawing.Size(122, 26)
        Me.chkIdVerify.TabIndex = 0
        Me.chkIdVerify.Text = "ID Verify"
        Me.chkIdVerify.UseVisualStyleBackColor = True
        '
        'rad_SuitabilityMisMatch
        '
        Me.rad_SuitabilityMisMatch.AutoCheck = False
        Me.rad_SuitabilityMisMatch.AutoSize = True
        Me.rad_SuitabilityMisMatch.Location = New System.Drawing.Point(1564, 429)
        Me.rad_SuitabilityMisMatch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_SuitabilityMisMatch.Name = "rad_SuitabilityMisMatch"
        Me.rad_SuitabilityMisMatch.Size = New System.Drawing.Size(177, 24)
        Me.rad_SuitabilityMisMatch.TabIndex = 71
        Me.rad_SuitabilityMisMatch.TabStop = True
        Me.rad_SuitabilityMisMatch.Text = "Suitability Mismatch "
        Me.rad_SuitabilityMisMatch.UseVisualStyleBackColor = True
        Me.rad_SuitabilityMisMatch.Visible = False
        '
        'gboPolicy
        '
        Me.gboPolicy.Controls.Add(Me.Label13)
        Me.gboPolicy.Controls.Add(Me.txtPolicyNo)
        Me.gboPolicy.Controls.Add(Me.lblPolicyNo)
        Me.gboPolicy.Controls.Add(Me.Panel1)
        Me.gboPolicy.Location = New System.Drawing.Point(12, 635)
        Me.gboPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboPolicy.Name = "gboPolicy"
        Me.gboPolicy.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboPolicy.Size = New System.Drawing.Size(602, 114)
        Me.gboPolicy.TabIndex = 70
        Me.gboPolicy.TabStop = False
        Me.gboPolicy.Text = "Policy"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(9, 32)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 25)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "Policy Type"
        Me.Label13.Visible = False
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.Location = New System.Drawing.Point(118, 72)
        Me.txtPolicyNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(208, 26)
        Me.txtPolicyNo.TabIndex = 45
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.Location = New System.Drawing.Point(9, 77)
        Me.lblPolicyNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(144, 25)
        Me.lblPolicyNo.TabIndex = 44
        Me.lblPolicyNo.Text = "Policy No."
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbEB)
        Me.Panel1.Controls.Add(Me.rbGI)
        Me.Panel1.Controls.Add(Me.rbLife)
        Me.Panel1.Location = New System.Drawing.Point(250, 25)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(236, 38)
        Me.Panel1.TabIndex = 43
        Me.Panel1.Visible = False
        '
        'rbEB
        '
        Me.rbEB.AutoSize = True
        Me.rbEB.Location = New System.Drawing.Point(147, 8)
        Me.rbEB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbEB.Name = "rbEB"
        Me.rbEB.Size = New System.Drawing.Size(56, 24)
        Me.rbEB.TabIndex = 16
        Me.rbEB.Text = "EB"
        Me.rbEB.UseVisualStyleBackColor = True
        '
        'rbGI
        '
        Me.rbGI.AutoSize = True
        Me.rbGI.Location = New System.Drawing.Point(82, 8)
        Me.rbGI.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbGI.Name = "rbGI"
        Me.rbGI.Size = New System.Drawing.Size(52, 24)
        Me.rbGI.TabIndex = 15
        Me.rbGI.Text = "GI"
        Me.rbGI.UseVisualStyleBackColor = True
        '
        'rbLife
        '
        Me.rbLife.AutoSize = True
        Me.rbLife.Checked = True
        Me.rbLife.Location = New System.Drawing.Point(10, 8)
        Me.rbLife.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbLife.Name = "rbLife"
        Me.rbLife.Size = New System.Drawing.Size(60, 24)
        Me.rbLife.TabIndex = 14
        Me.rbLife.TabStop = True
        Me.rbLife.Text = "Life"
        Me.rbLife.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(238, 155)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 22)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Date:"
        '
        'rad_ILASPostSalseCall
        '
        Me.rad_ILASPostSalseCall.AutoCheck = False
        Me.rad_ILASPostSalseCall.AutoSize = True
        Me.rad_ILASPostSalseCall.Location = New System.Drawing.Point(1564, 501)
        Me.rad_ILASPostSalseCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_ILASPostSalseCall.Name = "rad_ILASPostSalseCall"
        Me.rad_ILASPostSalseCall.Size = New System.Drawing.Size(181, 24)
        Me.rad_ILASPostSalseCall.TabIndex = 69
        Me.rad_ILASPostSalseCall.TabStop = True
        Me.rad_ILASPostSalseCall.Text = "ILAS Post-Sales Call"
        Me.rad_ILASPostSalseCall.UseVisualStyleBackColor = True
        Me.rad_ILASPostSalseCall.Visible = False
        '
        'chkReminder
        '
        Me.chkReminder.Location = New System.Drawing.Point(118, 152)
        Me.chkReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkReminder.Name = "chkReminder"
        Me.chkReminder.Size = New System.Drawing.Size(120, 25)
        Me.chkReminder.TabIndex = 11
        Me.chkReminder.Text = "Prompted?"
        '
        'rad_VCPostSalesCall
        '
        Me.rad_VCPostSalesCall.AutoCheck = False
        Me.rad_VCPostSalesCall.AutoSize = True
        Me.rad_VCPostSalesCall.Location = New System.Drawing.Point(1564, 477)
        Me.rad_VCPostSalesCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_VCPostSalesCall.Name = "rad_VCPostSalesCall"
        Me.rad_VCPostSalesCall.Size = New System.Drawing.Size(176, 24)
        Me.rad_VCPostSalesCall.TabIndex = 68
        Me.rad_VCPostSalesCall.TabStop = True
        Me.rad_VCPostSalesCall.Text = "Post-Sales Call - VC"
        Me.rad_VCPostSalesCall.UseVisualStyleBackColor = True
        Me.rad_VCPostSalesCall.Visible = False
        '
        'rad_NVCWelcomeCall
        '
        Me.rad_NVCWelcomeCall.AutoCheck = False
        Me.rad_NVCWelcomeCall.AutoSize = True
        Me.rad_NVCWelcomeCall.Location = New System.Drawing.Point(1564, 452)
        Me.rad_NVCWelcomeCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_NVCWelcomeCall.Name = "rad_NVCWelcomeCall"
        Me.rad_NVCWelcomeCall.Size = New System.Drawing.Size(194, 24)
        Me.rad_NVCWelcomeCall.TabIndex = 67
        Me.rad_NVCWelcomeCall.TabStop = True
        Me.rad_NVCWelcomeCall.Text = "Welcome Call - NonVC"
        Me.rad_NVCWelcomeCall.UseVisualStyleBackColor = True
        Me.rad_NVCWelcomeCall.Visible = False
        '
        'lbl_PostCallCount
        '
        Me.lbl_PostCallCount.AutoSize = True
        Me.lbl_PostCallCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallCount.Location = New System.Drawing.Point(1700, 578)
        Me.lbl_PostCallCount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PostCallCount.Name = "lbl_PostCallCount"
        Me.lbl_PostCallCount.Size = New System.Drawing.Size(85, 20)
        Me.lbl_PostCallCount.TabIndex = 66
        Me.lbl_PostCallCount.Text = "0 time(s)"
        Me.lbl_PostCallCount.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 32)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 25)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Policy Alert"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1560, 578)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(138, 20)
        Me.Label17.TabIndex = 65
        Me.Label17.Text = "Post Call Times :"
        Me.Label17.Visible = False
        '
        'lbl_PostCallStatus
        '
        Me.lbl_PostCallStatus.AutoSize = True
        Me.lbl_PostCallStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallStatus.Location = New System.Drawing.Point(1700, 555)
        Me.lbl_PostCallStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PostCallStatus.Name = "lbl_PostCallStatus"
        Me.lbl_PostCallStatus.Size = New System.Drawing.Size(113, 20)
        Me.lbl_PostCallStatus.TabIndex = 64
        Me.lbl_PostCallStatus.Text = "InCompleted"
        Me.lbl_PostCallStatus.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1560, 555)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(140, 20)
        Me.Label15.TabIndex = 63
        Me.Label15.Text = "Post Call Status :"
        Me.Label15.Visible = False
        '
        'lbl_InforceDate
        '
        Me.lbl_InforceDate.AutoSize = True
        Me.lbl_InforceDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InforceDate.Location = New System.Drawing.Point(1698, 532)
        Me.lbl_InforceDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_InforceDate.Name = "lbl_InforceDate"
        Me.lbl_InforceDate.Size = New System.Drawing.Size(113, 20)
        Me.lbl_InforceDate.TabIndex = 62
        Me.lbl_InforceDate.Text = "01 Mar 2016"
        Me.lbl_InforceDate.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1560, 532)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 20)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Inforce Date :"
        Me.Label5.Visible = False
        '
        'chkACC
        '
        Me.chkACC.BackColor = System.Drawing.Color.Transparent
        Me.chkACC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkACC.ForeColor = System.Drawing.Color.Indigo
        Me.chkACC.Location = New System.Drawing.Point(1566, 262)
        Me.chkACC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkACC.Name = "chkACC"
        Me.chkACC.Size = New System.Drawing.Size(174, 120)
        Me.chkACC.TabIndex = 60
        Me.chkACC.Text = "Suggestions/Grievances"
        Me.chkACC.UseVisualStyleBackColor = False
        '
        'chkFCR
        '
        Me.chkFCR.AutoSize = True
        Me.chkFCR.BackColor = System.Drawing.Color.Transparent
        Me.chkFCR.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkFCR.ForeColor = System.Drawing.Color.Indigo
        Me.chkFCR.Location = New System.Drawing.Point(1566, 234)
        Me.chkFCR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkFCR.Name = "chkFCR"
        Me.chkFCR.Size = New System.Drawing.Size(81, 29)
        Me.chkFCR.TabIndex = 59
        Me.chkFCR.Text = "FCR"
        Me.chkFCR.UseVisualStyleBackColor = False
        '
        'chkPolicyAlert
        '
        Me.chkPolicyAlert.Location = New System.Drawing.Point(118, 32)
        Me.chkPolicyAlert.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPolicyAlert.Name = "chkPolicyAlert"
        Me.chkPolicyAlert.Size = New System.Drawing.Size(144, 25)
        Me.chkPolicyAlert.TabIndex = 9
        Me.chkPolicyAlert.Text = "Prompted?"
        '
        'btnSaveC
        '
        Me.btnSaveC.Location = New System.Drawing.Point(1566, 163)
        Me.btnSaveC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSaveC.Name = "btnSaveC"
        Me.btnSaveC.Size = New System.Drawing.Size(96, 58)
        Me.btnSaveC.TabIndex = 58
        Me.btnSaveC.Text = "&Save && Close"
        '
        'txtDownloadPostSalesCall
        '
        Me.txtDownloadPostSalesCall.Location = New System.Drawing.Point(1566, 388)
        Me.txtDownloadPostSalesCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDownloadPostSalesCall.Name = "txtDownloadPostSalesCall"
        Me.txtDownloadPostSalesCall.Size = New System.Drawing.Size(230, 32)
        Me.txtDownloadPostSalesCall.TabIndex = 57
        Me.txtDownloadPostSalesCall.Text = "Button1"
        Me.txtDownloadPostSalesCall.Visible = False
        '
        'hidSender
        '
        Me.hidSender.Location = New System.Drawing.Point(1566, 298)
        Me.hidSender.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidSender.Name = "hidSender"
        Me.hidSender.Size = New System.Drawing.Size(100, 26)
        Me.hidSender.TabIndex = 56
        Me.hidSender.Visible = False
        '
        'hidCustID
        '
        Me.hidCustID.Location = New System.Drawing.Point(1560, 254)
        Me.hidCustID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidCustID.Name = "hidCustID"
        Me.hidCustID.Size = New System.Drawing.Size(100, 26)
        Me.hidCustID.TabIndex = 55
        Me.hidCustID.Visible = False
        '
        'hidPolicy
        '
        Me.hidPolicy.Location = New System.Drawing.Point(1566, 231)
        Me.hidPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidPolicy.Name = "hidPolicy"
        Me.hidPolicy.Size = New System.Drawing.Size(100, 26)
        Me.hidPolicy.TabIndex = 54
        Me.hidPolicy.Visible = False
        '
        'dtReminder
        '
        Me.dtReminder.Location = New System.Drawing.Point(292, 149)
        Me.dtReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtReminder.Name = "dtReminder"
        Me.dtReminder.Size = New System.Drawing.Size(160, 26)
        Me.dtReminder.TabIndex = 12
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbStatus)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.dtInitial)
        Me.GroupBox2.Controls.Add(Me.cbReceiver)
        Me.GroupBox2.Controls.Add(Me.cbInitiator)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.lbReceiver)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.chkAES)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 409)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(602, 217)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Status"
        '
        'cbStatus
        '
        Me.cbStatus.BackColor = System.Drawing.Color.White
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.Location = New System.Drawing.Point(108, 37)
        Me.cbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(160, 28)
        Me.cbStatus.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(18, 43)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 25)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Status"
        '
        'dtInitial
        '
        Me.dtInitial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInitial.Location = New System.Drawing.Point(108, 148)
        Me.dtInitial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtInitial.Name = "dtInitial"
        Me.dtInitial.Size = New System.Drawing.Size(244, 26)
        Me.dtInitial.TabIndex = 8
        '
        'cbReceiver
        '
        Me.cbReceiver.BackColor = System.Drawing.Color.White
        Me.cbReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReceiver.Enabled = False
        Me.cbReceiver.Location = New System.Drawing.Point(108, 77)
        Me.cbReceiver.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbReceiver.Name = "cbReceiver"
        Me.cbReceiver.Size = New System.Drawing.Size(486, 28)
        Me.cbReceiver.TabIndex = 6
        '
        'cbInitiator
        '
        Me.cbInitiator.BackColor = System.Drawing.Color.White
        Me.cbInitiator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInitiator.Location = New System.Drawing.Point(108, 111)
        Me.cbInitiator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbInitiator.Name = "cbInitiator"
        Me.cbInitiator.Size = New System.Drawing.Size(486, 28)
        Me.cbInitiator.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(18, 154)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 25)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Initial Date"
        '
        'lbReceiver
        '
        Me.lbReceiver.Enabled = False
        Me.lbReceiver.Location = New System.Drawing.Point(18, 80)
        Me.lbReceiver.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbReceiver.Name = "lbReceiver"
        Me.lbReceiver.Size = New System.Drawing.Size(78, 25)
        Me.lbReceiver.TabIndex = 15
        Me.lbReceiver.Text = "Reciever"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(18, 117)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 25)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Initiator"
        '
        'chkAES
        '
        Me.chkAES.Enabled = False
        Me.chkAES.Location = New System.Drawing.Point(282, 37)
        Me.chkAES.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAES.Name = "chkAES"
        Me.chkAES.Size = New System.Drawing.Size(156, 25)
        Me.chkAES.TabIndex = 5
        Me.chkAES.Text = "Transfer to AES"
        '
        'dgSrvLog
        '
        Me.dgSrvLog.AlternatingBackColor = System.Drawing.Color.White
        Me.dgSrvLog.BackColor = System.Drawing.Color.White
        Me.dgSrvLog.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgSrvLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgSrvLog.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgSrvLog.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgSrvLog.CaptionVisible = False
        Me.dgSrvLog.DataMember = ""
        Me.dgSrvLog.FlatMode = True
        Me.dgSrvLog.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgSrvLog.ForeColor = System.Drawing.Color.Black
        Me.dgSrvLog.GridLineColor = System.Drawing.Color.Wheat
        Me.dgSrvLog.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgSrvLog.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgSrvLog.HeaderForeColor = System.Drawing.Color.Black
        Me.dgSrvLog.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgSrvLog.Location = New System.Drawing.Point(9, 12)
        Me.dgSrvLog.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgSrvLog.Name = "dgSrvLog"
        Me.dgSrvLog.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgSrvLog.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgSrvLog.ReadOnly = True
        Me.dgSrvLog.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgSrvLog.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgSrvLog.Size = New System.Drawing.Size(1229, 166)
        Me.dgSrvLog.TabIndex = 51
        '
        'txtReminder
        '
        Me.txtReminder.Location = New System.Drawing.Point(16, 183)
        Me.txtReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReminder.MaxLength = 900
        Me.txtReminder.Multiline = True
        Me.txtReminder.Name = "txtReminder"
        Me.txtReminder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReminder.Size = New System.Drawing.Size(902, 72)
        Me.txtReminder.TabIndex = 13
        '
        'cbEventCat
        '
        Me.cbEventCat.BackColor = System.Drawing.Color.White
        Me.cbEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventCat.Location = New System.Drawing.Point(156, 68)
        Me.cbEventCat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventCat.Name = "cbEventCat"
        Me.cbEventCat.Size = New System.Drawing.Size(438, 28)
        Me.cbEventCat.TabIndex = 1
        '
        'cbEventDetail
        '
        Me.cbEventDetail.BackColor = System.Drawing.Color.White
        Me.cbEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventDetail.Location = New System.Drawing.Point(156, 105)
        Me.cbEventDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventDetail.Name = "cbEventDetail"
        Me.cbEventDetail.Size = New System.Drawing.Size(438, 28)
        Me.cbEventDetail.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(1566, 65)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 35)
        Me.btnSave.TabIndex = 50
        Me.btnSave.Text = "&Save"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(18, 37)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 25)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Medium"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(1566, 114)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 35)
        Me.btnCancel.TabIndex = 49
        Me.btnCancel.Text = "&Cancel"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1566, 15)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(96, 35)
        Me.btnNew.TabIndex = 47
        Me.btnNew.Text = "&New"
        '
        'grpServiceEvent
        '
        Me.grpServiceEvent.Controls.Add(Me.Label8)
        Me.grpServiceEvent.Controls.Add(Me.Label6)
        Me.grpServiceEvent.Controls.Add(Me.cbEventCat)
        Me.grpServiceEvent.Controls.Add(Me.cbEventDetail)
        Me.grpServiceEvent.Controls.Add(Me.cbEventTypeDetail)
        Me.grpServiceEvent.Controls.Add(Me.cbMedium)
        Me.grpServiceEvent.Controls.Add(Me.Label7)
        Me.grpServiceEvent.Controls.Add(Me.Label9)
        Me.grpServiceEvent.Location = New System.Drawing.Point(12, 191)
        Me.grpServiceEvent.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpServiceEvent.Name = "grpServiceEvent"
        Me.grpServiceEvent.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpServiceEvent.Size = New System.Drawing.Size(602, 209)
        Me.grpServiceEvent.TabIndex = 46
        Me.grpServiceEvent.TabStop = False
        Me.grpServiceEvent.Text = "Service Event"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(18, 111)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 25)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Event Detail"
        '
        'cbEventTypeDetail
        '
        Me.cbEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cbEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventTypeDetail.Location = New System.Drawing.Point(156, 142)
        Me.cbEventTypeDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventTypeDetail.Name = "cbEventTypeDetail"
        Me.cbEventTypeDetail.Size = New System.Drawing.Size(438, 28)
        Me.cbEventTypeDetail.TabIndex = 3
        '
        'cbMedium
        '
        Me.cbMedium.BackColor = System.Drawing.Color.White
        Me.cbMedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMedium.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbMedium.Location = New System.Drawing.Point(156, 31)
        Me.cbMedium.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbMedium.Name = "cbMedium"
        Me.cbMedium.Size = New System.Drawing.Size(438, 28)
        Me.cbMedium.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(18, 74)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(126, 31)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Event Category"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(18, 148)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 25)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Event Type Detail"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 155)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 22)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Reminder"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 275)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 25)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Notes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkIdVerify)
        Me.GroupBox1.Controls.Add(Me.dtReminder)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkReminder)
        Me.GroupBox1.Controls.Add(Me.chkPolicyAlert)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtNotes)
        Me.GroupBox1.Controls.Add(Me.txtReminder)
        Me.GroupBox1.Controls.Add(Me.txtPolicyAlert)
        Me.GroupBox1.Location = New System.Drawing.Point(622, 191)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(926, 558)
        Me.GroupBox1.TabIndex = 52
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Alert and Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(16, 300)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNotes.MaxLength = 900
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(902, 248)
        Me.txtNotes.TabIndex = 14
        '
        'txtPolicyAlert
        '
        Me.txtPolicyAlert.Location = New System.Drawing.Point(16, 60)
        Me.txtPolicyAlert.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyAlert.MaxLength = 256
        Me.txtPolicyAlert.Multiline = True
        Me.txtPolicyAlert.Name = "txtPolicyAlert"
        Me.txtPolicyAlert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPolicyAlert.Size = New System.Drawing.Size(902, 66)
        Me.txtPolicyAlert.TabIndex = 10
        '
        'btncustlogweb
        '
        Me.btncustlogweb.Location = New System.Drawing.Point(1436, 96)
        Me.btncustlogweb.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btncustlogweb.Name = "btncustlogweb"
        Me.btncustlogweb.Size = New System.Drawing.Size(112, 85)
        Me.btncustlogweb.TabIndex = 73
        Me.btncustlogweb.Text = "Show Contact History"
        Me.btncustlogweb.UseVisualStyleBackColor = True
        '
        'hidSrvEvtNo
        '
        Me.hidSrvEvtNo.Location = New System.Drawing.Point(1566, 194)
        Me.hidSrvEvtNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidSrvEvtNo.Name = "hidSrvEvtNo"
        Me.hidSrvEvtNo.Size = New System.Drawing.Size(100, 26)
        Me.hidSrvEvtNo.TabIndex = 48
        Me.hidSrvEvtNo.Visible = False
        Me.hidSrvEvtNo.WordWrap = False
        '
        'gboCustomer
        '
        Me.gboCustomer.Controls.Add(Me.txtCustomerID)
        Me.gboCustomer.Controls.Add(Me.Label14)
        Me.gboCustomer.Controls.Add(Me.txtCustomerName)
        Me.gboCustomer.Controls.Add(Me.Label16)
        Me.gboCustomer.Location = New System.Drawing.Point(12, 757)
        Me.gboCustomer.Name = "gboCustomer"
        Me.gboCustomer.Size = New System.Drawing.Size(381, 82)
        Me.gboCustomer.TabIndex = 74
        Me.gboCustomer.TabStop = False
        Me.gboCustomer.Text = "Customer"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerID.Location = New System.Drawing.Point(87, 15)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(288, 26)
        Me.txtCustomerID.TabIndex = 47
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(6, 21)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(67, 16)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "ID"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.Location = New System.Drawing.Point(87, 47)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(288, 26)
        Me.txtCustomerName.TabIndex = 45
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(6, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 16)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Name"
        '
        'uclServiceLog_Asur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.gboCustomer)
        Me.Controls.Add(Me.chkMCV)
        Me.Controls.Add(Me.rad_SuitabilityMisMatch)
        Me.Controls.Add(Me.gboPolicy)
        Me.Controls.Add(Me.rad_ILASPostSalseCall)
        Me.Controls.Add(Me.rad_VCPostSalesCall)
        Me.Controls.Add(Me.rad_NVCWelcomeCall)
        Me.Controls.Add(Me.lbl_PostCallCount)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lbl_PostCallStatus)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lbl_InforceDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkACC)
        Me.Controls.Add(Me.chkFCR)
        Me.Controls.Add(Me.btnSaveC)
        Me.Controls.Add(Me.txtDownloadPostSalesCall)
        Me.Controls.Add(Me.hidSender)
        Me.Controls.Add(Me.hidCustID)
        Me.Controls.Add(Me.hidPolicy)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.dgSrvLog)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.grpServiceEvent)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btncustlogweb)
        Me.Controls.Add(Me.hidSrvEvtNo)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "uclServiceLog_Asur"
        Me.Size = New System.Drawing.Size(2520, 1154)
        Me.gboPolicy.ResumeLayout(False)
        Me.gboPolicy.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpServiceEvent.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gboCustomer.ResumeLayout(False)
        Me.gboCustomer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkMCV As System.Windows.Forms.CheckBox
    Friend WithEvents chkIdVerify As System.Windows.Forms.CheckBox
    Friend WithEvents rad_SuitabilityMisMatch As System.Windows.Forms.RadioButton
    Friend WithEvents gboPolicy As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbEB As System.Windows.Forms.RadioButton
    Friend WithEvents rbGI As System.Windows.Forms.RadioButton
    Friend WithEvents rbLife As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rad_ILASPostSalseCall As System.Windows.Forms.RadioButton
    Friend WithEvents chkReminder As System.Windows.Forms.CheckBox
    Friend WithEvents rad_VCPostSalesCall As System.Windows.Forms.RadioButton
    Friend WithEvents rad_NVCWelcomeCall As System.Windows.Forms.RadioButton
    Friend WithEvents lbl_PostCallCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lbl_PostCallStatus As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lbl_InforceDate As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkACC As System.Windows.Forms.CheckBox
    Friend WithEvents chkFCR As System.Windows.Forms.CheckBox
    Friend WithEvents chkPolicyAlert As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveC As System.Windows.Forms.Button
    Friend WithEvents txtDownloadPostSalesCall As System.Windows.Forms.Button
    Friend WithEvents hidSender As System.Windows.Forms.TextBox
    Friend WithEvents hidCustID As System.Windows.Forms.TextBox
    Friend WithEvents hidPolicy As System.Windows.Forms.TextBox
    Friend WithEvents dtReminder As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtInitial As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbReceiver As System.Windows.Forms.ComboBox
    Friend WithEvents cbInitiator As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbReceiver As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkAES As System.Windows.Forms.CheckBox
    Friend WithEvents dgSrvLog As System.Windows.Forms.DataGrid
    Friend WithEvents txtReminder As System.Windows.Forms.TextBox
    Friend WithEvents cbEventCat As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventDetail As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents grpServiceEvent As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbEventTypeDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cbMedium As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicyAlert As System.Windows.Forms.TextBox
    Friend WithEvents btncustlogweb As System.Windows.Forms.Button
    Friend WithEvents hidSrvEvtNo As System.Windows.Forms.TextBox
    Friend WithEvents gboCustomer As GroupBox
    Friend WithEvents txtCustomerID As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents Label16 As Label
End Class
