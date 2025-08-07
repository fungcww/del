<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMCUTransLog
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblMedium = New System.Windows.Forms.Label()
        Me.lblEventCategory = New System.Windows.Forms.Label()
        Me.cboEventCategory = New System.Windows.Forms.ComboBox()
        Me.cboEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.lblEventTypeDetail = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblPolicy = New System.Windows.Forms.Label()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.txtEventNote = New System.Windows.Forms.TextBox()
        Me.lblEventNote = New System.Windows.Forms.Label()
        Me.lblReminder = New System.Windows.Forms.Label()
        Me.txtRemainder = New System.Windows.Forms.TextBox()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lblBasicPlan = New System.Windows.Forms.Label()
        Me.lblAccountStatus = New System.Windows.Forms.Label()
        Me.cboAccountStatus = New System.Windows.Forms.ComboBox()
        Me.lblEffectiveDate = New System.Windows.Forms.Label()
        Me.dteEffectiveDate = New System.Windows.Forms.DateTimePicker()
        Me.lblServiceAgent = New System.Windows.Forms.Label()
        Me.txtSACode = New System.Windows.Forms.TextBox()
        Me.txtAgentName = New System.Windows.Forms.TextBox()
        Me.lblServicingLocation = New System.Windows.Forms.Label()
        Me.cboServicingLocationCode = New System.Windows.Forms.ComboBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.cboBasicPlan = New System.Windows.Forms.ComboBox()
        Me.cboMedium = New System.Windows.Forms.ComboBox()
        Me.lblEventDetail = New System.Windows.Forms.Label()
        Me.cboEventDetail = New System.Windows.Forms.ComboBox()
        Me.cboEventSourceInd = New System.Windows.Forms.ComboBox()
        Me.dgvServiceLog = New System.Windows.Forms.DataGridView()
        Me.EventInitialDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitiatorName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCategoryDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventTypeDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventTypeDetailDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PolicyAccountID2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FollowUpByMacau = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCloseoutCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MCV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventTypeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventTypeDetailCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventStatusCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ServiceEventNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCompletionDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCloseoutDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventCloseoutCSRID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventSourceInitiatorCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventSourceMediumCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventNotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReminderNotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AgentCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DivisionCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameSuffix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccountStatusCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MasterCSRID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PolicyAccountID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.mtxtYear = New System.Windows.Forms.MaskedTextBox()
        Me.btnView = New System.Windows.Forms.Button()
        Me.chkFollowUp = New System.Windows.Forms.CheckBox()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.cboUser = New System.Windows.Forms.ComboBox()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSearchByPolicy = New System.Windows.Forms.Label()
        Me.txtSearchByPolicy = New System.Windows.Forms.TextBox()
        Me.lblSearchStatus = New System.Windows.Forms.Label()
        Me.cboSearchStatus = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboSearchNeedFollowUp = New System.Windows.Forms.ComboBox()
        Me.gbSerach = New System.Windows.Forms.GroupBox()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.chkFCR = New System.Windows.Forms.CheckBox()
        Me.chkMCV = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnSmsSearch = New System.Windows.Forms.Button()
        Me.txtSmsPolicy = New System.Windows.Forms.TextBox()
        Me.lblSmsPolicy = New System.Windows.Forms.Label()
        Me.UclSMS1 = New CS2005.uclSMS()
        CType(Me.dgvServiceLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSerach.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMedium
        '
        Me.lblMedium.AutoSize = True
        Me.lblMedium.Location = New System.Drawing.Point(16, 191)
        Me.lblMedium.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMedium.Name = "lblMedium"
        Me.lblMedium.Size = New System.Drawing.Size(65, 20)
        Me.lblMedium.TabIndex = 23
        Me.lblMedium.Text = "Medium"
        '
        'lblEventCategory
        '
        Me.lblEventCategory.AutoSize = True
        Me.lblEventCategory.Location = New System.Drawing.Point(20, 232)
        Me.lblEventCategory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEventCategory.Name = "lblEventCategory"
        Me.lblEventCategory.Size = New System.Drawing.Size(118, 20)
        Me.lblEventCategory.TabIndex = 23
        Me.lblEventCategory.Text = "Event Category"
        '
        'cboEventCategory
        '
        Me.cboEventCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventCategory.FormattingEnabled = True
        Me.cboEventCategory.Location = New System.Drawing.Point(200, 228)
        Me.cboEventCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboEventCategory.Name = "cboEventCategory"
        Me.cboEventCategory.Size = New System.Drawing.Size(498, 28)
        Me.cboEventCategory.TabIndex = 11
        '
        'cboEventTypeDetail
        '
        Me.cboEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventTypeDetail.FormattingEnabled = True
        Me.cboEventTypeDetail.Location = New System.Drawing.Point(892, 186)
        Me.cboEventTypeDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboEventTypeDetail.Name = "cboEventTypeDetail"
        Me.cboEventTypeDetail.Size = New System.Drawing.Size(499, 28)
        Me.cboEventTypeDetail.TabIndex = 13
        '
        'lblEventTypeDetail
        '
        Me.lblEventTypeDetail.AutoSize = True
        Me.lblEventTypeDetail.Location = New System.Drawing.Point(712, 191)
        Me.lblEventTypeDetail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEventTypeDetail.Name = "lblEventTypeDetail"
        Me.lblEventTypeDetail.Size = New System.Drawing.Size(133, 20)
        Me.lblEventTypeDetail.TabIndex = 23
        Me.lblEventTypeDetail.Text = "Event Type Detail"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(891, 266)
        Me.cboStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(499, 28)
        Me.cboStatus.TabIndex = 15
        Me.cboStatus.Tag = ""
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(712, 271)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(56, 20)
        Me.lblStatus.TabIndex = 23
        Me.lblStatus.Text = "Status"
        '
        'lblPolicy
        '
        Me.lblPolicy.AutoSize = True
        Me.lblPolicy.Location = New System.Drawing.Point(20, 22)
        Me.lblPolicy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPolicy.Name = "lblPolicy"
        Me.lblPolicy.Size = New System.Drawing.Size(73, 20)
        Me.lblPolicy.TabIndex = 23
        Me.lblPolicy.Text = "Policy No"
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.Location = New System.Drawing.Point(204, 17)
        Me.txtPolicyNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyNo.MaxLength = 16
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.ReadOnly = True
        Me.txtPolicyNo.Size = New System.Drawing.Size(498, 26)
        Me.txtPolicyNo.TabIndex = 2
        '
        'txtEventNote
        '
        Me.txtEventNote.Location = New System.Drawing.Point(21, 340)
        Me.txtEventNote.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEventNote.MaxLength = 900
        Me.txtEventNote.Multiline = True
        Me.txtEventNote.Name = "txtEventNote"
        Me.txtEventNote.Size = New System.Drawing.Size(1368, 172)
        Me.txtEventNote.TabIndex = 18
        '
        'lblEventNote
        '
        Me.lblEventNote.AutoSize = True
        Me.lblEventNote.Location = New System.Drawing.Point(20, 315)
        Me.lblEventNote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEventNote.Name = "lblEventNote"
        Me.lblEventNote.Size = New System.Drawing.Size(88, 20)
        Me.lblEventNote.TabIndex = 23
        Me.lblEventNote.Text = "Event Note"
        '
        'lblReminder
        '
        Me.lblReminder.AutoSize = True
        Me.lblReminder.Location = New System.Drawing.Point(16, 518)
        Me.lblReminder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReminder.Name = "lblReminder"
        Me.lblReminder.Size = New System.Drawing.Size(78, 20)
        Me.lblReminder.TabIndex = 23
        Me.lblReminder.Text = "Reminder"
        '
        'txtRemainder
        '
        Me.txtRemainder.Location = New System.Drawing.Point(21, 543)
        Me.txtRemainder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemainder.MaxLength = 900
        Me.txtRemainder.Multiline = True
        Me.txtRemainder.Name = "txtRemainder"
        Me.txtRemainder.Size = New System.Drawing.Size(1368, 162)
        Me.txtRemainder.TabIndex = 19
        '
        'txtCustomerID
        '
        Me.txtCustomerID.Location = New System.Drawing.Point(891, 17)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustomerID.MaxLength = 16
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.ReadOnly = True
        Me.txtCustomerID.Size = New System.Drawing.Size(499, 26)
        Me.txtCustomerID.TabIndex = 5
        Me.txtCustomerID.TabStop = False
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(712, 22)
        Me.lblCustomer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(99, 20)
        Me.lblCustomer.TabIndex = 23
        Me.lblCustomer.Text = "Customer ID"
        '
        'lblBasicPlan
        '
        Me.lblBasicPlan.AutoSize = True
        Me.lblBasicPlan.Location = New System.Drawing.Point(16, 60)
        Me.lblBasicPlan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBasicPlan.Name = "lblBasicPlan"
        Me.lblBasicPlan.Size = New System.Drawing.Size(83, 20)
        Me.lblBasicPlan.TabIndex = 23
        Me.lblBasicPlan.Text = "Basic Plan"
        '
        'lblAccountStatus
        '
        Me.lblAccountStatus.AutoSize = True
        Me.lblAccountStatus.Location = New System.Drawing.Point(20, 106)
        Me.lblAccountStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccountStatus.Name = "lblAccountStatus"
        Me.lblAccountStatus.Size = New System.Drawing.Size(119, 20)
        Me.lblAccountStatus.TabIndex = 23
        Me.lblAccountStatus.Text = "Account Status"
        '
        'cboAccountStatus
        '
        Me.cboAccountStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboAccountStatus.FormattingEnabled = True
        Me.cboAccountStatus.Location = New System.Drawing.Point(204, 97)
        Me.cboAccountStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboAccountStatus.Name = "cboAccountStatus"
        Me.cboAccountStatus.Size = New System.Drawing.Size(498, 30)
        Me.cboAccountStatus.TabIndex = 4
        Me.cboAccountStatus.TabStop = False
        '
        'lblEffectiveDate
        '
        Me.lblEffectiveDate.AutoSize = True
        Me.lblEffectiveDate.Location = New System.Drawing.Point(20, 151)
        Me.lblEffectiveDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEffectiveDate.Name = "lblEffectiveDate"
        Me.lblEffectiveDate.Size = New System.Drawing.Size(85, 20)
        Me.lblEffectiveDate.TabIndex = 23
        Me.lblEffectiveDate.Text = "Initial Date"
        '
        'dteEffectiveDate
        '
        Me.dteEffectiveDate.CustomFormat = "MMMM dd, yyyy hh:mm tt"
        Me.dteEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dteEffectiveDate.Location = New System.Drawing.Point(204, 142)
        Me.dteEffectiveDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dteEffectiveDate.Name = "dteEffectiveDate"
        Me.dteEffectiveDate.Size = New System.Drawing.Size(494, 26)
        Me.dteEffectiveDate.TabIndex = 9
        Me.dteEffectiveDate.Value = New Date(2017, 1, 6, 0, 0, 0, 0)
        '
        'lblServiceAgent
        '
        Me.lblServiceAgent.AutoSize = True
        Me.lblServiceAgent.Location = New System.Drawing.Point(712, 62)
        Me.lblServiceAgent.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblServiceAgent.Name = "lblServiceAgent"
        Me.lblServiceAgent.Size = New System.Drawing.Size(162, 20)
        Me.lblServiceAgent.TabIndex = 23
        Me.lblServiceAgent.Text = "Servicing Agent Code"
        '
        'txtSACode
        '
        Me.txtSACode.Location = New System.Drawing.Point(891, 55)
        Me.txtSACode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSACode.MaxLength = 5
        Me.txtSACode.Name = "txtSACode"
        Me.txtSACode.ReadOnly = True
        Me.txtSACode.Size = New System.Drawing.Size(79, 26)
        Me.txtSACode.TabIndex = 6
        Me.txtSACode.TabStop = False
        '
        'txtAgentName
        '
        Me.txtAgentName.Location = New System.Drawing.Point(972, 55)
        Me.txtAgentName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAgentName.MaxLength = 5
        Me.txtAgentName.Name = "txtAgentName"
        Me.txtAgentName.ReadOnly = True
        Me.txtAgentName.Size = New System.Drawing.Size(418, 26)
        Me.txtAgentName.TabIndex = 7
        Me.txtAgentName.TabStop = False
        '
        'lblServicingLocation
        '
        Me.lblServicingLocation.AutoSize = True
        Me.lblServicingLocation.Location = New System.Drawing.Point(712, 106)
        Me.lblServicingLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblServicingLocation.Name = "lblServicingLocation"
        Me.lblServicingLocation.Size = New System.Drawing.Size(138, 20)
        Me.lblServicingLocation.TabIndex = 23
        Me.lblServicingLocation.Text = "Servicing Location"
        '
        'cboServicingLocationCode
        '
        Me.cboServicingLocationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboServicingLocationCode.FormattingEnabled = True
        Me.cboServicingLocationCode.Location = New System.Drawing.Point(891, 97)
        Me.cboServicingLocationCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboServicingLocationCode.Name = "cboServicingLocationCode"
        Me.cboServicingLocationCode.Size = New System.Drawing.Size(499, 30)
        Me.cboServicingLocationCode.TabIndex = 8
        Me.cboServicingLocationCode.TabStop = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(4, 6)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(146, 131)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&Add New Service Log"
        Me.btnNew.UseVisualStyleBackColor = True
        Me.btnNew.Visible = False
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(1152, 717)
        Me.btnSubmit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(112, 35)
        Me.btnSubmit.TabIndex = 20
        Me.btnSubmit.Text = "&Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        Me.btnSubmit.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(1274, 717)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(112, 35)
        Me.btnExit.TabIndex = 21
        Me.btnExit.Text = "&Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cboBasicPlan
        '
        Me.cboBasicPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboBasicPlan.FormattingEnabled = True
        Me.cboBasicPlan.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cboBasicPlan.Location = New System.Drawing.Point(204, 55)
        Me.cboBasicPlan.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboBasicPlan.Name = "cboBasicPlan"
        Me.cboBasicPlan.Size = New System.Drawing.Size(498, 30)
        Me.cboBasicPlan.TabIndex = 3
        Me.cboBasicPlan.TabStop = False
        '
        'cboMedium
        '
        Me.cboMedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedium.FormattingEnabled = True
        Me.cboMedium.Location = New System.Drawing.Point(201, 186)
        Me.cboMedium.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboMedium.Name = "cboMedium"
        Me.cboMedium.Size = New System.Drawing.Size(498, 28)
        Me.cboMedium.TabIndex = 10
        '
        'lblEventDetail
        '
        Me.lblEventDetail.AutoSize = True
        Me.lblEventDetail.Location = New System.Drawing.Point(20, 271)
        Me.lblEventDetail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEventDetail.Name = "lblEventDetail"
        Me.lblEventDetail.Size = New System.Drawing.Size(95, 20)
        Me.lblEventDetail.TabIndex = 23
        Me.lblEventDetail.Text = "Event Detail"
        '
        'cboEventDetail
        '
        Me.cboEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventDetail.FormattingEnabled = True
        Me.cboEventDetail.Location = New System.Drawing.Point(200, 266)
        Me.cboEventDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboEventDetail.Name = "cboEventDetail"
        Me.cboEventDetail.Size = New System.Drawing.Size(498, 28)
        Me.cboEventDetail.TabIndex = 12
        '
        'cboEventSourceInd
        '
        Me.cboEventSourceInd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventSourceInd.FormattingEnabled = True
        Me.cboEventSourceInd.Location = New System.Drawing.Point(892, 228)
        Me.cboEventSourceInd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboEventSourceInd.Name = "cboEventSourceInd"
        Me.cboEventSourceInd.Size = New System.Drawing.Size(499, 28)
        Me.cboEventSourceInd.TabIndex = 14
        '
        'dgvServiceLog
        '
        Me.dgvServiceLog.AllowUserToAddRows = False
        Me.dgvServiceLog.AllowUserToDeleteRows = False
        Me.dgvServiceLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvServiceLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventInitialDateTime, Me.InitiatorName, Me.EventStatus, Me.EventCategoryDesc, Me.EventTypeDesc, Me.EventTypeDetailDesc, Me.PolicyAccountID2, Me.FollowUpByMacau, Me.EventCloseoutCode, Me.MCV, Me.EventCategoryCode, Me.EventTypeCode, Me.EventTypeDetailCode, Me.EventStatusCode, Me.ServiceEventNumber, Me.CustomerID, Me.EventCompletionDateTime, Me.EventCloseoutDateTime, Me.EventCloseoutCSRID, Me.EventSourceInitiatorCode, Me.EventSourceMediumCode, Me.EventNotes, Me.ReminderNotes, Me.AgentCode, Me.DivisionCode, Me.UnitCode, Me.ProductID, Me.NameSuffix, Me.FirstName, Me.AccountStatusCode, Me.LocationCode, Me.MasterCSRID, Me.PolicyAccountID})
        Me.dgvServiceLog.Location = New System.Drawing.Point(4, 148)
        Me.dgvServiceLog.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvServiceLog.MultiSelect = False
        Me.dgvServiceLog.Name = "dgvServiceLog"
        Me.dgvServiceLog.ReadOnly = True
        Me.dgvServiceLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvServiceLog.Size = New System.Drawing.Size(1378, 343)
        Me.dgvServiceLog.TabIndex = 1
        '
        'EventInitialDateTime
        '
        Me.EventInitialDateTime.DataPropertyName = "EventInitialDateTime"
        DataGridViewCellStyle2.Format = "yyyy/MM/dd hh:mm tt"
        DataGridViewCellStyle2.NullValue = "1900/01/01"
        Me.EventInitialDateTime.DefaultCellStyle = DataGridViewCellStyle2
        Me.EventInitialDateTime.HeaderText = "Initial Date"
        Me.EventInitialDateTime.Name = "EventInitialDateTime"
        Me.EventInitialDateTime.ReadOnly = True
        Me.EventInitialDateTime.Width = 115
        '
        'InitiatorName
        '
        Me.InitiatorName.DataPropertyName = "InitiatorName"
        Me.InitiatorName.HeaderText = "Sender"
        Me.InitiatorName.Name = "InitiatorName"
        Me.InitiatorName.ReadOnly = True
        '
        'EventStatus
        '
        Me.EventStatus.DataPropertyName = "EventStatus"
        Me.EventStatus.HeaderText = "Status"
        Me.EventStatus.Name = "EventStatus"
        Me.EventStatus.ReadOnly = True
        Me.EventStatus.Width = 75
        '
        'EventCategoryDesc
        '
        Me.EventCategoryDesc.DataPropertyName = "EventCategoryDesc"
        Me.EventCategoryDesc.HeaderText = "Event Category"
        Me.EventCategoryDesc.Name = "EventCategoryDesc"
        Me.EventCategoryDesc.ReadOnly = True
        Me.EventCategoryDesc.Width = 150
        '
        'EventTypeDesc
        '
        Me.EventTypeDesc.DataPropertyName = "EventTypeDesc"
        Me.EventTypeDesc.HeaderText = "Event Type"
        Me.EventTypeDesc.Name = "EventTypeDesc"
        Me.EventTypeDesc.ReadOnly = True
        Me.EventTypeDesc.Width = 170
        '
        'EventTypeDetailDesc
        '
        Me.EventTypeDetailDesc.DataPropertyName = "EventTypeDetailDesc"
        Me.EventTypeDetailDesc.HeaderText = "Event Type Detail"
        Me.EventTypeDetailDesc.Name = "EventTypeDetailDesc"
        Me.EventTypeDetailDesc.ReadOnly = True
        Me.EventTypeDetailDesc.Width = 170
        '
        'PolicyAccountID2
        '
        Me.PolicyAccountID2.DataPropertyName = "PolicyAccountID2"
        Me.PolicyAccountID2.HeaderText = "Policy No"
        Me.PolicyAccountID2.Name = "PolicyAccountID2"
        Me.PolicyAccountID2.ReadOnly = True
        Me.PolicyAccountID2.Width = 160
        '
        'FollowUpByMacau
        '
        Me.FollowUpByMacau.DataPropertyName = "FollowUpByMacau"
        Me.FollowUpByMacau.HeaderText = "Need Follow Up"
        Me.FollowUpByMacau.Name = "FollowUpByMacau"
        Me.FollowUpByMacau.ReadOnly = True
        Me.FollowUpByMacau.Width = 45
        '
        'EventCloseoutCode
        '
        Me.EventCloseoutCode.DataPropertyName = "EventCloseoutCode"
        Me.EventCloseoutCode.HeaderText = "FCR"
        Me.EventCloseoutCode.Name = "EventCloseoutCode"
        Me.EventCloseoutCode.ReadOnly = True
        Me.EventCloseoutCode.Width = 45
        '
        'MCV
        '
        Me.MCV.DataPropertyName = "MCV"
        Me.MCV.HeaderText = "MCV"
        Me.MCV.Name = "MCV"
        Me.MCV.ReadOnly = True
        Me.MCV.Width = 45
        '
        'EventCategoryCode
        '
        Me.EventCategoryCode.DataPropertyName = "EventCategoryCode"
        Me.EventCategoryCode.HeaderText = "EventCategoryCode"
        Me.EventCategoryCode.Name = "EventCategoryCode"
        Me.EventCategoryCode.ReadOnly = True
        Me.EventCategoryCode.Visible = False
        Me.EventCategoryCode.Width = 125
        '
        'EventTypeCode
        '
        Me.EventTypeCode.DataPropertyName = "EventTypeCode"
        Me.EventTypeCode.HeaderText = "EventTypeCode"
        Me.EventTypeCode.Name = "EventTypeCode"
        Me.EventTypeCode.ReadOnly = True
        Me.EventTypeCode.Visible = False
        Me.EventTypeCode.Width = 125
        '
        'EventTypeDetailCode
        '
        Me.EventTypeDetailCode.DataPropertyName = "EventTypeDetailCode"
        Me.EventTypeDetailCode.HeaderText = "EventTypeDetailCode"
        Me.EventTypeDetailCode.Name = "EventTypeDetailCode"
        Me.EventTypeDetailCode.ReadOnly = True
        Me.EventTypeDetailCode.Visible = False
        Me.EventTypeDetailCode.Width = 125
        '
        'EventStatusCode
        '
        Me.EventStatusCode.DataPropertyName = "EventStatusCode"
        Me.EventStatusCode.HeaderText = "Status"
        Me.EventStatusCode.Name = "EventStatusCode"
        Me.EventStatusCode.ReadOnly = True
        Me.EventStatusCode.Visible = False
        '
        'ServiceEventNumber
        '
        Me.ServiceEventNumber.DataPropertyName = "ServiceEventNumber"
        Me.ServiceEventNumber.HeaderText = "ServiceEventNumber"
        Me.ServiceEventNumber.Name = "ServiceEventNumber"
        Me.ServiceEventNumber.ReadOnly = True
        Me.ServiceEventNumber.Visible = False
        '
        'CustomerID
        '
        Me.CustomerID.DataPropertyName = "CustomerID"
        Me.CustomerID.HeaderText = "CustomerID"
        Me.CustomerID.Name = "CustomerID"
        Me.CustomerID.ReadOnly = True
        Me.CustomerID.Visible = False
        '
        'EventCompletionDateTime
        '
        Me.EventCompletionDateTime.DataPropertyName = "EventCompletionDateTime"
        Me.EventCompletionDateTime.HeaderText = "EventCompletionDateTime"
        Me.EventCompletionDateTime.Name = "EventCompletionDateTime"
        Me.EventCompletionDateTime.ReadOnly = True
        Me.EventCompletionDateTime.Visible = False
        '
        'EventCloseoutDateTime
        '
        Me.EventCloseoutDateTime.DataPropertyName = "EventCloseoutDateTime"
        Me.EventCloseoutDateTime.HeaderText = "EventCloseoutDateTime"
        Me.EventCloseoutDateTime.Name = "EventCloseoutDateTime"
        Me.EventCloseoutDateTime.ReadOnly = True
        Me.EventCloseoutDateTime.Visible = False
        '
        'EventCloseoutCSRID
        '
        Me.EventCloseoutCSRID.DataPropertyName = "EventCloseoutCSRID"
        Me.EventCloseoutCSRID.HeaderText = "EventCloseoutCSRID"
        Me.EventCloseoutCSRID.Name = "EventCloseoutCSRID"
        Me.EventCloseoutCSRID.ReadOnly = True
        Me.EventCloseoutCSRID.Visible = False
        '
        'EventSourceInitiatorCode
        '
        Me.EventSourceInitiatorCode.DataPropertyName = "EventSourceInitiatorCode"
        Me.EventSourceInitiatorCode.HeaderText = "EventSourceInitiatorCode"
        Me.EventSourceInitiatorCode.Name = "EventSourceInitiatorCode"
        Me.EventSourceInitiatorCode.ReadOnly = True
        Me.EventSourceInitiatorCode.Visible = False
        '
        'EventSourceMediumCode
        '
        Me.EventSourceMediumCode.DataPropertyName = "EventSourceMediumCode"
        Me.EventSourceMediumCode.HeaderText = "EventSourceMediumCode"
        Me.EventSourceMediumCode.Name = "EventSourceMediumCode"
        Me.EventSourceMediumCode.ReadOnly = True
        Me.EventSourceMediumCode.Visible = False
        '
        'EventNotes
        '
        Me.EventNotes.DataPropertyName = "EventNotes"
        Me.EventNotes.HeaderText = "EventNotes"
        Me.EventNotes.Name = "EventNotes"
        Me.EventNotes.ReadOnly = True
        Me.EventNotes.Visible = False
        '
        'ReminderNotes
        '
        Me.ReminderNotes.DataPropertyName = "ReminderNotes"
        Me.ReminderNotes.HeaderText = "ReminderNotes"
        Me.ReminderNotes.Name = "ReminderNotes"
        Me.ReminderNotes.ReadOnly = True
        Me.ReminderNotes.Visible = False
        '
        'AgentCode
        '
        Me.AgentCode.DataPropertyName = "AgentCode"
        Me.AgentCode.HeaderText = "AgentCode"
        Me.AgentCode.Name = "AgentCode"
        Me.AgentCode.ReadOnly = True
        Me.AgentCode.Visible = False
        '
        'DivisionCode
        '
        Me.DivisionCode.DataPropertyName = "DivisionCode"
        Me.DivisionCode.HeaderText = "DivisionCode"
        Me.DivisionCode.Name = "DivisionCode"
        Me.DivisionCode.ReadOnly = True
        Me.DivisionCode.Visible = False
        '
        'UnitCode
        '
        Me.UnitCode.DataPropertyName = "UnitCode"
        Me.UnitCode.HeaderText = "UnitCode"
        Me.UnitCode.Name = "UnitCode"
        Me.UnitCode.ReadOnly = True
        Me.UnitCode.Visible = False
        '
        'ProductID
        '
        Me.ProductID.DataPropertyName = "ProductID"
        Me.ProductID.HeaderText = "ProductID"
        Me.ProductID.Name = "ProductID"
        Me.ProductID.ReadOnly = True
        Me.ProductID.Visible = False
        '
        'NameSuffix
        '
        Me.NameSuffix.DataPropertyName = "NameSuffix"
        Me.NameSuffix.HeaderText = "NameSuffix"
        Me.NameSuffix.Name = "NameSuffix"
        Me.NameSuffix.ReadOnly = True
        Me.NameSuffix.Visible = False
        '
        'FirstName
        '
        Me.FirstName.DataPropertyName = "FirstName"
        Me.FirstName.HeaderText = "FirstName"
        Me.FirstName.Name = "FirstName"
        Me.FirstName.ReadOnly = True
        Me.FirstName.Visible = False
        '
        'AccountStatusCode
        '
        Me.AccountStatusCode.DataPropertyName = "AccountStatusCode"
        Me.AccountStatusCode.HeaderText = "AccountStatusCode"
        Me.AccountStatusCode.Name = "AccountStatusCode"
        Me.AccountStatusCode.ReadOnly = True
        Me.AccountStatusCode.Visible = False
        '
        'LocationCode
        '
        Me.LocationCode.DataPropertyName = "LocationCode"
        Me.LocationCode.HeaderText = "LocationCode"
        Me.LocationCode.Name = "LocationCode"
        Me.LocationCode.ReadOnly = True
        Me.LocationCode.Visible = False
        '
        'MasterCSRID
        '
        Me.MasterCSRID.DataPropertyName = "MasterCSRID"
        Me.MasterCSRID.HeaderText = "MasterCSRID"
        Me.MasterCSRID.Name = "MasterCSRID"
        Me.MasterCSRID.ReadOnly = True
        Me.MasterCSRID.Visible = False
        Me.MasterCSRID.Width = 75
        '
        'PolicyAccountID
        '
        Me.PolicyAccountID.DataPropertyName = "PolicyAccountID"
        Me.PolicyAccountID.HeaderText = "Policy No"
        Me.PolicyAccountID.Name = "PolicyAccountID"
        Me.PolicyAccountID.ReadOnly = True
        Me.PolicyAccountID.Width = 80
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Location = New System.Drawing.Point(669, 43)
        Me.lblYear.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(43, 20)
        Me.lblYear.TabIndex = 20
        Me.lblYear.Text = "Year"
        '
        'mtxtYear
        '
        Me.mtxtYear.Location = New System.Drawing.Point(722, 38)
        Me.mtxtYear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.mtxtYear.Mask = "0000"
        Me.mtxtYear.Name = "mtxtYear"
        Me.mtxtYear.Size = New System.Drawing.Size(76, 26)
        Me.mtxtYear.TabIndex = 4
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(831, 20)
        Me.btnView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(146, 102)
        Me.btnView.TabIndex = 6
        Me.btnView.Text = "&Search Service Log"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'chkFollowUp
        '
        Me.chkFollowUp.AutoSize = True
        Me.chkFollowUp.Location = New System.Drawing.Point(892, 306)
        Me.chkFollowUp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkFollowUp.Name = "chkFollowUp"
        Me.chkFollowUp.Size = New System.Drawing.Size(174, 24)
        Me.chkFollowUp.TabIndex = 16
        Me.chkFollowUp.Text = "Follow up by Macau"
        Me.chkFollowUp.UseVisualStyleBackColor = True
        '
        'cboMonth
        '
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.FormattingEnabled = True
        Me.cboMonth.Items.AddRange(New Object() {"", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.cboMonth.Location = New System.Drawing.Point(722, 78)
        Me.cboMonth.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.Size = New System.Drawing.Size(76, 28)
        Me.cboMonth.TabIndex = 5
        '
        'cboUser
        '
        Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUser.FormattingEnabled = True
        Me.cboUser.Location = New System.Drawing.Point(446, 78)
        Me.cboUser.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboUser.Name = "cboUser"
        Me.cboUser.Size = New System.Drawing.Size(193, 28)
        Me.cboUser.TabIndex = 3
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(393, 83)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(45, 20)
        Me.lblUser.TabIndex = 27
        Me.lblUser.Text = "User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(712, 232)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 20)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Initiator"
        '
        'lblSearchByPolicy
        '
        Me.lblSearchByPolicy.AutoSize = True
        Me.lblSearchByPolicy.Location = New System.Drawing.Point(10, 43)
        Me.lblSearchByPolicy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearchByPolicy.Name = "lblSearchByPolicy"
        Me.lblSearchByPolicy.Size = New System.Drawing.Size(80, 20)
        Me.lblSearchByPolicy.TabIndex = 29
        Me.lblSearchByPolicy.Text = "Policy No"
        '
        'txtSearchByPolicy
        '
        Me.txtSearchByPolicy.Location = New System.Drawing.Point(98, 38)
        Me.txtSearchByPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearchByPolicy.MaxLength = 16
        Me.txtSearchByPolicy.Name = "txtSearchByPolicy"
        Me.txtSearchByPolicy.Size = New System.Drawing.Size(193, 26)
        Me.txtSearchByPolicy.TabIndex = 0
        '
        'lblSearchStatus
        '
        Me.lblSearchStatus.AutoSize = True
        Me.lblSearchStatus.Location = New System.Drawing.Point(33, 83)
        Me.lblSearchStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearchStatus.Name = "lblSearchStatus"
        Me.lblSearchStatus.Size = New System.Drawing.Size(57, 20)
        Me.lblSearchStatus.TabIndex = 30
        Me.lblSearchStatus.Text = "Status"
        '
        'cboSearchStatus
        '
        Me.cboSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchStatus.FormattingEnabled = True
        Me.cboSearchStatus.Location = New System.Drawing.Point(98, 78)
        Me.cboSearchStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboSearchStatus.Name = "cboSearchStatus"
        Me.cboSearchStatus.Size = New System.Drawing.Size(193, 28)
        Me.cboSearchStatus.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 43)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 20)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Need Follow Up"
        '
        'cboSearchNeedFollowUp
        '
        Me.cboSearchNeedFollowUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchNeedFollowUp.FormattingEnabled = True
        Me.cboSearchNeedFollowUp.Location = New System.Drawing.Point(446, 38)
        Me.cboSearchNeedFollowUp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboSearchNeedFollowUp.Name = "cboSearchNeedFollowUp"
        Me.cboSearchNeedFollowUp.Size = New System.Drawing.Size(193, 28)
        Me.cboSearchNeedFollowUp.TabIndex = 2
        '
        'gbSerach
        '
        Me.gbSerach.Controls.Add(Me.lblMonth)
        Me.gbSerach.Controls.Add(Me.txtSearchByPolicy)
        Me.gbSerach.Controls.Add(Me.cboSearchNeedFollowUp)
        Me.gbSerach.Controls.Add(Me.lblUser)
        Me.gbSerach.Controls.Add(Me.lblSearchByPolicy)
        Me.gbSerach.Controls.Add(Me.cboUser)
        Me.gbSerach.Controls.Add(Me.lblYear)
        Me.gbSerach.Controls.Add(Me.cboMonth)
        Me.gbSerach.Controls.Add(Me.Label2)
        Me.gbSerach.Controls.Add(Me.lblSearchStatus)
        Me.gbSerach.Controls.Add(Me.btnView)
        Me.gbSerach.Controls.Add(Me.cboSearchStatus)
        Me.gbSerach.Controls.Add(Me.mtxtYear)
        Me.gbSerach.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.gbSerach.Location = New System.Drawing.Point(399, 5)
        Me.gbSerach.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbSerach.Name = "gbSerach"
        Me.gbSerach.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbSerach.Size = New System.Drawing.Size(986, 134)
        Me.gbSerach.TabIndex = 22
        Me.gbSerach.TabStop = False
        Me.gbSerach.Text = "Search Service Log"
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Location = New System.Drawing.Point(652, 83)
        Me.lblMonth.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(55, 20)
        Me.lblMonth.TabIndex = 33
        Me.lblMonth.Text = "Month"
        '
        'chkFCR
        '
        Me.chkFCR.AutoSize = True
        Me.chkFCR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFCR.ForeColor = System.Drawing.Color.Indigo
        Me.chkFCR.Location = New System.Drawing.Point(1080, 305)
        Me.chkFCR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkFCR.Name = "chkFCR"
        Me.chkFCR.Size = New System.Drawing.Size(81, 29)
        Me.chkFCR.TabIndex = 17
        Me.chkFCR.Text = "FCR"
        Me.chkFCR.UseVisualStyleBackColor = True
        '
        'chkMCV
        '
        Me.chkMCV.AutoSize = True
        Me.chkMCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMCV.ForeColor = System.Drawing.Color.Red
        Me.chkMCV.Location = New System.Drawing.Point(1174, 306)
        Me.chkMCV.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMCV.Name = "chkMCV"
        Me.chkMCV.Size = New System.Drawing.Size(87, 29)
        Me.chkMCV.TabIndex = 24
        Me.chkMCV.Text = "MCV"
        Me.chkMCV.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPolicy)
        Me.Panel1.Controls.Add(Me.chkMCV)
        Me.Panel1.Controls.Add(Me.cboMedium)
        Me.Panel1.Controls.Add(Me.chkFCR)
        Me.Panel1.Controls.Add(Me.lblMedium)
        Me.Panel1.Controls.Add(Me.lblEventCategory)
        Me.Panel1.Controls.Add(Me.cboEventCategory)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblEventDetail)
        Me.Panel1.Controls.Add(Me.chkFollowUp)
        Me.Panel1.Controls.Add(Me.cboEventDetail)
        Me.Panel1.Controls.Add(Me.lblEventTypeDetail)
        Me.Panel1.Controls.Add(Me.cboBasicPlan)
        Me.Panel1.Controls.Add(Me.cboEventTypeDetail)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.cboEventSourceInd)
        Me.Panel1.Controls.Add(Me.btnSubmit)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.cboStatus)
        Me.Panel1.Controls.Add(Me.cboServicingLocationCode)
        Me.Panel1.Controls.Add(Me.txtPolicyNo)
        Me.Panel1.Controls.Add(Me.lblServicingLocation)
        Me.Panel1.Controls.Add(Me.txtEventNote)
        Me.Panel1.Controls.Add(Me.txtAgentName)
        Me.Panel1.Controls.Add(Me.lblEventNote)
        Me.Panel1.Controls.Add(Me.txtSACode)
        Me.Panel1.Controls.Add(Me.txtRemainder)
        Me.Panel1.Controls.Add(Me.lblServiceAgent)
        Me.Panel1.Controls.Add(Me.lblReminder)
        Me.Panel1.Controls.Add(Me.dteEffectiveDate)
        Me.Panel1.Controls.Add(Me.lblCustomer)
        Me.Panel1.Controls.Add(Me.lblEffectiveDate)
        Me.Panel1.Controls.Add(Me.txtCustomerID)
        Me.Panel1.Controls.Add(Me.cboAccountStatus)
        Me.Panel1.Controls.Add(Me.lblBasicPlan)
        Me.Panel1.Controls.Add(Me.lblAccountStatus)
        Me.Panel1.Location = New System.Drawing.Point(9, 9)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1416, 772)
        Me.Panel1.TabIndex = 25
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(4, 500)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1444, 828)
        Me.TabControl1.TabIndex = 26
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Size = New System.Drawing.Size(1436, 795)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Service Log"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnSmsSearch)
        Me.TabPage2.Controls.Add(Me.txtSmsPolicy)
        Me.TabPage2.Controls.Add(Me.lblSmsPolicy)
        Me.TabPage2.Controls.Add(Me.UclSMS1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Size = New System.Drawing.Size(1436, 795)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SMS Letter"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnSmsSearch
        '
        Me.btnSmsSearch.Location = New System.Drawing.Point(306, 8)
        Me.btnSmsSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSmsSearch.Name = "btnSmsSearch"
        Me.btnSmsSearch.Size = New System.Drawing.Size(112, 35)
        Me.btnSmsSearch.TabIndex = 3
        Me.btnSmsSearch.Text = "Search"
        Me.btnSmsSearch.UseVisualStyleBackColor = True
        '
        'txtSmsPolicy
        '
        Me.txtSmsPolicy.Location = New System.Drawing.Point(70, 9)
        Me.txtSmsPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSmsPolicy.Name = "txtSmsPolicy"
        Me.txtSmsPolicy.Size = New System.Drawing.Size(224, 26)
        Me.txtSmsPolicy.TabIndex = 2
        '
        'lblSmsPolicy
        '
        Me.lblSmsPolicy.AutoSize = True
        Me.lblSmsPolicy.Location = New System.Drawing.Point(9, 15)
        Me.lblSmsPolicy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSmsPolicy.Name = "lblSmsPolicy"
        Me.lblSmsPolicy.Size = New System.Drawing.Size(49, 20)
        Me.lblSmsPolicy.TabIndex = 1
        Me.lblSmsPolicy.Text = "Policy"
        '
        'UclSMS1
        '
        Me.UclSMS1.AutoScroll = True
        Me.UclSMS1.Location = New System.Drawing.Point(14, 51)
        Me.UclSMS1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UclSMS1.Name = "UclSMS1"
        Me.UclSMS1.PolicyAccountID = Nothing
        Me.UclSMS1.Size = New System.Drawing.Size(1089, 709)
        Me.UclSMS1.TabIndex = 0
        Me.UclSMS1.UseDefaultPrinter = False
        '
        'frmMCUTransLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1463, 1050)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.gbSerach)
        Me.Controls.Add(Me.dgvServiceLog)
        Me.Controls.Add(Me.btnNew)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMCUTransLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Macau Service Log"
        CType(Me.dgvServiceLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSerach.ResumeLayout(False)
        Me.gbSerach.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMedium As System.Windows.Forms.Label
    Friend WithEvents lblEventCategory As System.Windows.Forms.Label
    Friend WithEvents cboEventCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboEventTypeDetail As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventTypeDetail As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblPolicy As System.Windows.Forms.Label
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents txtEventNote As System.Windows.Forms.TextBox
    Friend WithEvents lblEventNote As System.Windows.Forms.Label
    Friend WithEvents lblReminder As System.Windows.Forms.Label
    Friend WithEvents txtRemainder As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblBasicPlan As System.Windows.Forms.Label
    Friend WithEvents lblAccountStatus As System.Windows.Forms.Label
    Friend WithEvents cboAccountStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblEffectiveDate As System.Windows.Forms.Label
    Friend WithEvents dteEffectiveDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblServiceAgent As System.Windows.Forms.Label
    Friend WithEvents txtSACode As System.Windows.Forms.TextBox
    Friend WithEvents txtAgentName As System.Windows.Forms.TextBox
    Friend WithEvents lblServicingLocation As System.Windows.Forms.Label
    Friend WithEvents cboServicingLocationCode As System.Windows.Forms.ComboBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cboBasicPlan As System.Windows.Forms.ComboBox
    Friend WithEvents cboMedium As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventDetail As System.Windows.Forms.Label
    Friend WithEvents cboEventDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cboEventSourceInd As System.Windows.Forms.ComboBox
    Friend WithEvents dgvServiceLog As System.Windows.Forms.DataGridView
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents mtxtYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents chkFollowUp As System.Windows.Forms.CheckBox
    Friend WithEvents cboMonth As System.Windows.Forms.ComboBox
    Friend WithEvents cboUser As System.Windows.Forms.ComboBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSearchByPolicy As System.Windows.Forms.Label
    Friend WithEvents txtSearchByPolicy As System.Windows.Forms.TextBox
    Friend WithEvents lblSearchStatus As System.Windows.Forms.Label
    Friend WithEvents cboSearchStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboSearchNeedFollowUp As System.Windows.Forms.ComboBox
    Friend WithEvents gbSerach As System.Windows.Forms.GroupBox
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents chkFCR As System.Windows.Forms.CheckBox
    Friend WithEvents chkMCV As System.Windows.Forms.CheckBox
    Friend WithEvents EventInitialDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitiatorName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCategoryDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDetailDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PolicyAccountID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PolicyAccountID2 As System.Windows.Forms.DataGridViewTextBoxColumn 'ITSR933 FG R3 Policy Numbmer Change
    Friend WithEvents FollowUpByMacau As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCloseoutCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MCV As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDetailCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventStatusCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServiceEventNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCompletionDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCloseoutDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCloseoutCSRID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventSourceInitiatorCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventSourceMediumCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventNotes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReminderNotes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgentCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DivisionCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProductID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameSuffix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccountStatusCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MasterCSRID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents UclSMS1 As CS2005.uclSMS
    Friend WithEvents btnSmsSearch As System.Windows.Forms.Button
    Friend WithEvents txtSmsPolicy As System.Windows.Forms.TextBox
    Friend WithEvents lblSmsPolicy As System.Windows.Forms.Label
End Class
