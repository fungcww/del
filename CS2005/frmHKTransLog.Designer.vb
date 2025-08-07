<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHKTransLog
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblMedium = New System.Windows.Forms.Label
        Me.lblEventCategory = New System.Windows.Forms.Label
        Me.cboEventCategory = New System.Windows.Forms.ComboBox
        Me.cboEventTypeDetail = New System.Windows.Forms.ComboBox
        Me.lblEventTypeDetail = New System.Windows.Forms.Label
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.txtEventNote = New System.Windows.Forms.TextBox
        Me.lblEventNote = New System.Windows.Forms.Label
        Me.lblReminder = New System.Windows.Forms.Label
        Me.txtRemainder = New System.Windows.Forms.TextBox
        Me.lblEffectiveDate = New System.Windows.Forms.Label
        Me.dteEffectiveDate = New System.Windows.Forms.DateTimePicker
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.cboMedium = New System.Windows.Forms.ComboBox
        Me.lblEventDetail = New System.Windows.Forms.Label
        Me.cboEventDetail = New System.Windows.Forms.ComboBox
        Me.cboEventSourceInd = New System.Windows.Forms.ComboBox
        Me.dgvServiceLog = New System.Windows.Forms.DataGridView
        Me.lblYear = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboMonth = New System.Windows.Forms.ComboBox
        Me.cboUser = New System.Windows.Forms.ComboBox
        Me.lblUser = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSearchStatus = New System.Windows.Forms.Label
        Me.cboSearchStatus = New System.Windows.Forms.ComboBox
        Me.gbSerach = New System.Windows.Forms.GroupBox
        Me.btnresetsearch = New System.Windows.Forms.Button
        Me.cboYear = New System.Windows.Forms.ComboBox
        Me.txtsearchemail = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtsearchmobile = New System.Windows.Forms.TextBox
        Me.lblcontactphone = New System.Windows.Forms.Label
        Me.lblMonth = New System.Windows.Forms.Label
        Me.chkFCR = New System.Windows.Forms.CheckBox
        Me.chkMCV = New System.Windows.Forms.CheckBox
        Me.lblgreetingname = New System.Windows.Forms.Label
        Me.txtgreetingname = New System.Windows.Forms.TextBox
        Me.txtcallid = New System.Windows.Forms.TextBox
        Me.lblcallid = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboHandoffCsr = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboNamePrefix = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtemail = New System.Windows.Forms.TextBox
        Me.lblmobile = New System.Windows.Forms.Label
        Me.txtmobile = New System.Windows.Forms.TextBox
        Me.btnresetinput = New System.Windows.Forms.Button
        Me.EventInitialDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.InitiatorName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GreetingName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventStatus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCategoryDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeDetailDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventSourceInitiator = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventSourceMedium = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCloseoutCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MCV = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PhoneMobile = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EmailAddr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Call_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeDetailCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventStatusCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ServiceEventNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCompletionDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCloseoutDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventCloseoutCSRID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventSourceInitiatorCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventSourceMediumCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventNotes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReminderNotes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MasterCSRID = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvServiceLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSerach.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMedium
        '
        Me.lblMedium.AutoSize = True
        Me.lblMedium.Location = New System.Drawing.Point(454, 60)
        Me.lblMedium.Name = "lblMedium"
        Me.lblMedium.Size = New System.Drawing.Size(44, 13)
        Me.lblMedium.TabIndex = 23
        Me.lblMedium.Text = "Medium"
        '
        'lblEventCategory
        '
        Me.lblEventCategory.AutoSize = True
        Me.lblEventCategory.Location = New System.Drawing.Point(12, 60)
        Me.lblEventCategory.Name = "lblEventCategory"
        Me.lblEventCategory.Size = New System.Drawing.Size(80, 13)
        Me.lblEventCategory.TabIndex = 23
        Me.lblEventCategory.Text = "Event Category"
        '
        'cboEventCategory
        '
        Me.cboEventCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventCategory.FormattingEnabled = True
        Me.cboEventCategory.Location = New System.Drawing.Point(106, 52)
        Me.cboEventCategory.Name = "cboEventCategory"
        Me.cboEventCategory.Size = New System.Drawing.Size(300, 21)
        Me.cboEventCategory.TabIndex = 11
        '
        'cboEventTypeDetail
        '
        Me.cboEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventTypeDetail.FormattingEnabled = True
        Me.cboEventTypeDetail.Location = New System.Drawing.Point(106, 117)
        Me.cboEventTypeDetail.Name = "cboEventTypeDetail"
        Me.cboEventTypeDetail.Size = New System.Drawing.Size(300, 21)
        Me.cboEventTypeDetail.TabIndex = 13
        '
        'lblEventTypeDetail
        '
        Me.lblEventTypeDetail.AutoSize = True
        Me.lblEventTypeDetail.Location = New System.Drawing.Point(12, 125)
        Me.lblEventTypeDetail.Name = "lblEventTypeDetail"
        Me.lblEventTypeDetail.Size = New System.Drawing.Size(92, 13)
        Me.lblEventTypeDetail.TabIndex = 23
        Me.lblEventTypeDetail.Text = "Event Type Detail"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(501, 117)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(107, 21)
        Me.cboStatus.TabIndex = 15
        Me.cboStatus.Tag = ""
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(454, 125)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 23
        Me.lblStatus.Text = "Status"
        '
        'txtEventNote
        '
        Me.txtEventNote.Location = New System.Drawing.Point(10, 623)
        Me.txtEventNote.MaxLength = 900
        Me.txtEventNote.Multiline = True
        Me.txtEventNote.Name = "txtEventNote"
        Me.txtEventNote.Size = New System.Drawing.Size(275, 150)
        Me.txtEventNote.TabIndex = 18
        '
        'lblEventNote
        '
        Me.lblEventNote.AutoSize = True
        Me.lblEventNote.Location = New System.Drawing.Point(7, 607)
        Me.lblEventNote.Name = "lblEventNote"
        Me.lblEventNote.Size = New System.Drawing.Size(61, 13)
        Me.lblEventNote.TabIndex = 23
        Me.lblEventNote.Text = "Event Note"
        '
        'lblReminder
        '
        Me.lblReminder.AutoSize = True
        Me.lblReminder.Location = New System.Drawing.Point(342, 607)
        Me.lblReminder.Name = "lblReminder"
        Me.lblReminder.Size = New System.Drawing.Size(52, 13)
        Me.lblReminder.TabIndex = 23
        Me.lblReminder.Text = "Reminder"
        '
        'txtRemainder
        '
        Me.txtRemainder.Location = New System.Drawing.Point(343, 623)
        Me.txtRemainder.MaxLength = 900
        Me.txtRemainder.Multiline = True
        Me.txtRemainder.Name = "txtRemainder"
        Me.txtRemainder.Size = New System.Drawing.Size(275, 150)
        Me.txtRemainder.TabIndex = 19
        '
        'lblEffectiveDate
        '
        Me.lblEffectiveDate.AutoSize = True
        Me.lblEffectiveDate.Location = New System.Drawing.Point(13, 25)
        Me.lblEffectiveDate.Name = "lblEffectiveDate"
        Me.lblEffectiveDate.Size = New System.Drawing.Size(57, 13)
        Me.lblEffectiveDate.TabIndex = 23
        Me.lblEffectiveDate.Text = "Initial Date"
        '
        'dteEffectiveDate
        '
        Me.dteEffectiveDate.CustomFormat = "MMMM dd, yyyy hh:mm tt"
        Me.dteEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dteEffectiveDate.Location = New System.Drawing.Point(106, 19)
        Me.dteEffectiveDate.Name = "dteEffectiveDate"
        Me.dteEffectiveDate.Size = New System.Drawing.Size(300, 20)
        Me.dteEffectiveDate.TabIndex = 9
        Me.dteEffectiveDate.Value = New Date(2017, 1, 6, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(3, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(97, 85)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&Add New Service Log"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(643, 669)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(131, 104)
        Me.btnSubmit.TabIndex = 20
        Me.btnSubmit.Text = "&Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(848, 739)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 34)
        Me.btnExit.TabIndex = 21
        Me.btnExit.Text = "&Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cboMedium
        '
        Me.cboMedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedium.FormattingEnabled = True
        Me.cboMedium.Location = New System.Drawing.Point(501, 52)
        Me.cboMedium.Name = "cboMedium"
        Me.cboMedium.Size = New System.Drawing.Size(300, 21)
        Me.cboMedium.TabIndex = 10
        '
        'lblEventDetail
        '
        Me.lblEventDetail.AutoSize = True
        Me.lblEventDetail.Location = New System.Drawing.Point(12, 91)
        Me.lblEventDetail.Name = "lblEventDetail"
        Me.lblEventDetail.Size = New System.Drawing.Size(65, 13)
        Me.lblEventDetail.TabIndex = 23
        Me.lblEventDetail.Text = "Event Detail"
        '
        'cboEventDetail
        '
        Me.cboEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventDetail.FormattingEnabled = True
        Me.cboEventDetail.Location = New System.Drawing.Point(106, 83)
        Me.cboEventDetail.Name = "cboEventDetail"
        Me.cboEventDetail.Size = New System.Drawing.Size(300, 21)
        Me.cboEventDetail.TabIndex = 12
        '
        'cboEventSourceInd
        '
        Me.cboEventSourceInd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventSourceInd.FormattingEnabled = True
        Me.cboEventSourceInd.Location = New System.Drawing.Point(501, 83)
        Me.cboEventSourceInd.Name = "cboEventSourceInd"
        Me.cboEventSourceInd.Size = New System.Drawing.Size(300, 21)
        Me.cboEventSourceInd.TabIndex = 14
        '
        'dgvServiceLog
        '
        Me.dgvServiceLog.AllowUserToAddRows = False
        Me.dgvServiceLog.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvServiceLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvServiceLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvServiceLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventInitialDateTime, Me.InitiatorName, Me.GreetingName, Me.EventStatus, Me.EventCategoryDesc, Me.EventTypeDesc, Me.EventTypeDetailDesc, Me.EventSourceInitiator, Me.EventSourceMedium, Me.EventCloseoutCode, Me.MCV, Me.PhoneMobile, Me.EmailAddr, Me.Call_id, Me.EventCategoryCode, Me.EventTypeCode, Me.EventTypeDetailCode, Me.EventStatusCode, Me.ServiceEventNumber, Me.CustomerID, Me.EventCompletionDateTime, Me.EventCloseoutDateTime, Me.EventCloseoutCSRID, Me.EventSourceInitiatorCode, Me.EventSourceMediumCode, Me.EventNotes, Me.ReminderNotes, Me.LocationCode, Me.MasterCSRID})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvServiceLog.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvServiceLog.Location = New System.Drawing.Point(4, 96)
        Me.dgvServiceLog.MultiSelect = False
        Me.dgvServiceLog.Name = "dgvServiceLog"
        Me.dgvServiceLog.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvServiceLog.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvServiceLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvServiceLog.Size = New System.Drawing.Size(919, 223)
        Me.dgvServiceLog.TabIndex = 1
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Location = New System.Drawing.Point(432, 23)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(29, 13)
        Me.lblYear.TabIndex = 20
        Me.lblYear.Text = "Year"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(537, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(97, 66)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "&Search Service Log"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboMonth
        '
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.FormattingEnabled = True
        Me.cboMonth.Items.AddRange(New Object() {"", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.cboMonth.Location = New System.Drawing.Point(467, 46)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.Size = New System.Drawing.Size(52, 21)
        Me.cboMonth.TabIndex = 5
        '
        'cboUser
        '
        Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUser.FormattingEnabled = True
        Me.cboUser.Location = New System.Drawing.Point(286, 46)
        Me.cboUser.Name = "cboUser"
        Me.cboUser.Size = New System.Drawing.Size(130, 21)
        Me.cboUser.TabIndex = 3
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(251, 49)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(29, 13)
        Me.lblUser.TabIndex = 27
        Me.lblUser.Text = "User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(454, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Initiator"
        '
        'lblSearchStatus
        '
        Me.lblSearchStatus.AutoSize = True
        Me.lblSearchStatus.Location = New System.Drawing.Point(243, 22)
        Me.lblSearchStatus.Name = "lblSearchStatus"
        Me.lblSearchStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblSearchStatus.TabIndex = 30
        Me.lblSearchStatus.Text = "Status"
        '
        'cboSearchStatus
        '
        Me.cboSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchStatus.FormattingEnabled = True
        Me.cboSearchStatus.Location = New System.Drawing.Point(286, 19)
        Me.cboSearchStatus.Name = "cboSearchStatus"
        Me.cboSearchStatus.Size = New System.Drawing.Size(130, 21)
        Me.cboSearchStatus.TabIndex = 1
        '
        'gbSerach
        '
        Me.gbSerach.Controls.Add(Me.btnresetsearch)
        Me.gbSerach.Controls.Add(Me.cboYear)
        Me.gbSerach.Controls.Add(Me.txtsearchemail)
        Me.gbSerach.Controls.Add(Me.Label3)
        Me.gbSerach.Controls.Add(Me.txtsearchmobile)
        Me.gbSerach.Controls.Add(Me.lblcontactphone)
        Me.gbSerach.Controls.Add(Me.lblMonth)
        Me.gbSerach.Controls.Add(Me.lblUser)
        Me.gbSerach.Controls.Add(Me.cboUser)
        Me.gbSerach.Controls.Add(Me.lblYear)
        Me.gbSerach.Controls.Add(Me.cboMonth)
        Me.gbSerach.Controls.Add(Me.lblSearchStatus)
        Me.gbSerach.Controls.Add(Me.btnSearch)
        Me.gbSerach.Controls.Add(Me.cboSearchStatus)
        Me.gbSerach.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.gbSerach.Location = New System.Drawing.Point(106, 3)
        Me.gbSerach.Name = "gbSerach"
        Me.gbSerach.Size = New System.Drawing.Size(817, 87)
        Me.gbSerach.TabIndex = 22
        Me.gbSerach.TabStop = False
        Me.gbSerach.Text = "Search Service Log"
        '
        'btnresetsearch
        '
        Me.btnresetsearch.Location = New System.Drawing.Point(662, 15)
        Me.btnresetsearch.Name = "btnresetsearch"
        Me.btnresetsearch.Size = New System.Drawing.Size(97, 66)
        Me.btnresetsearch.TabIndex = 39
        Me.btnresetsearch.Text = "Reset Search criteria"
        Me.btnresetsearch.UseVisualStyleBackColor = True
        '
        'cboYear
        '
        Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYear.FormattingEnabled = True
        Me.cboYear.Location = New System.Drawing.Point(467, 20)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.Size = New System.Drawing.Size(52, 21)
        Me.cboYear.TabIndex = 38
        '
        'txtsearchemail
        '
        Me.txtsearchemail.Location = New System.Drawing.Point(56, 47)
        Me.txtsearchemail.MaxLength = 45
        Me.txtsearchemail.Name = "txtsearchemail"
        Me.txtsearchemail.Size = New System.Drawing.Size(135, 20)
        Me.txtsearchemail.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Email"
        '
        'txtsearchmobile
        '
        Me.txtsearchmobile.Location = New System.Drawing.Point(56, 20)
        Me.txtsearchmobile.MaxLength = 16
        Me.txtsearchmobile.Name = "txtsearchmobile"
        Me.txtsearchmobile.Size = New System.Drawing.Size(135, 20)
        Me.txtsearchmobile.TabIndex = 35
        '
        'lblcontactphone
        '
        Me.lblcontactphone.AutoSize = True
        Me.lblcontactphone.Location = New System.Drawing.Point(17, 23)
        Me.lblcontactphone.Name = "lblcontactphone"
        Me.lblcontactphone.Size = New System.Drawing.Size(38, 13)
        Me.lblcontactphone.TabIndex = 34
        Me.lblcontactphone.Text = "Mobile"
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Location = New System.Drawing.Point(424, 49)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(37, 13)
        Me.lblMonth.TabIndex = 33
        Me.lblMonth.Text = "Month"
        '
        'chkFCR
        '
        Me.chkFCR.AutoSize = True
        Me.chkFCR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFCR.ForeColor = System.Drawing.Color.Indigo
        Me.chkFCR.Location = New System.Drawing.Point(455, 19)
        Me.chkFCR.Name = "chkFCR"
        Me.chkFCR.Size = New System.Drawing.Size(57, 20)
        Me.chkFCR.TabIndex = 17
        Me.chkFCR.Text = "FCR"
        Me.chkFCR.UseVisualStyleBackColor = True
        '
        'chkMCV
        '
        Me.chkMCV.AutoSize = True
        Me.chkMCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMCV.ForeColor = System.Drawing.Color.Red
        Me.chkMCV.Location = New System.Drawing.Point(518, 19)
        Me.chkMCV.Name = "chkMCV"
        Me.chkMCV.Size = New System.Drawing.Size(59, 20)
        Me.chkMCV.TabIndex = 24
        Me.chkMCV.Text = "MCV"
        Me.chkMCV.UseVisualStyleBackColor = True
        '
        'lblgreetingname
        '
        Me.lblgreetingname.AutoSize = True
        Me.lblgreetingname.Location = New System.Drawing.Point(13, 26)
        Me.lblgreetingname.Name = "lblgreetingname"
        Me.lblgreetingname.Size = New System.Drawing.Size(78, 13)
        Me.lblgreetingname.TabIndex = 25
        Me.lblgreetingname.Text = "Greeting Name"
        '
        'txtgreetingname
        '
        Me.txtgreetingname.Location = New System.Drawing.Point(169, 19)
        Me.txtgreetingname.Name = "txtgreetingname"
        Me.txtgreetingname.Size = New System.Drawing.Size(238, 20)
        Me.txtgreetingname.TabIndex = 26
        '
        'txtcallid
        '
        Me.txtcallid.Location = New System.Drawing.Point(501, 18)
        Me.txtcallid.Name = "txtcallid"
        Me.txtcallid.Size = New System.Drawing.Size(300, 20)
        Me.txtcallid.TabIndex = 26
        '
        'lblcallid
        '
        Me.lblcallid.AutoSize = True
        Me.lblcallid.Location = New System.Drawing.Point(454, 25)
        Me.lblcallid.Name = "lblcallid"
        Me.lblcallid.Size = New System.Drawing.Size(38, 13)
        Me.lblcallid.TabIndex = 27
        Me.lblcallid.Text = "Call ID"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboHandoffCsr)
        Me.GroupBox1.Controls.Add(Me.dteEffectiveDate)
        Me.GroupBox1.Controls.Add(Me.lblcallid)
        Me.GroupBox1.Controls.Add(Me.lblEffectiveDate)
        Me.GroupBox1.Controls.Add(Me.cboEventCategory)
        Me.GroupBox1.Controls.Add(Me.txtcallid)
        Me.GroupBox1.Controls.Add(Me.lblEventCategory)
        Me.GroupBox1.Controls.Add(Me.lblEventDetail)
        Me.GroupBox1.Controls.Add(Me.cboEventDetail)
        Me.GroupBox1.Controls.Add(Me.lblEventTypeDetail)
        Me.GroupBox1.Controls.Add(Me.cboEventTypeDetail)
        Me.GroupBox1.Controls.Add(Me.cboMedium)
        Me.GroupBox1.Controls.Add(Me.lblMedium)
        Me.GroupBox1.Controls.Add(Me.cboEventSourceInd)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboStatus)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 418)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(907, 153)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Service Type"
        '
        'cboHandoffCsr
        '
        Me.cboHandoffCsr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHandoffCsr.Enabled = False
        Me.cboHandoffCsr.FormattingEnabled = True
        Me.cboHandoffCsr.Location = New System.Drawing.Point(614, 117)
        Me.cboHandoffCsr.Name = "cboHandoffCsr"
        Me.cboHandoffCsr.Size = New System.Drawing.Size(190, 21)
        Me.cboHandoffCsr.TabIndex = 28
        Me.cboHandoffCsr.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboNamePrefix)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtemail)
        Me.GroupBox2.Controls.Add(Me.lblmobile)
        Me.GroupBox2.Controls.Add(Me.txtmobile)
        Me.GroupBox2.Controls.Add(Me.lblgreetingname)
        Me.GroupBox2.Controls.Add(Me.txtgreetingname)
        Me.GroupBox2.Controls.Add(Me.chkMCV)
        Me.GroupBox2.Controls.Add(Me.chkFCR)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 325)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(908, 87)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Customer Information"
        '
        'cboNamePrefix
        '
        Me.cboNamePrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNamePrefix.FormattingEnabled = True
        Me.cboNamePrefix.Location = New System.Drawing.Point(107, 18)
        Me.cboNamePrefix.Name = "cboNamePrefix"
        Me.cboNamePrefix.Size = New System.Drawing.Size(57, 21)
        Me.cboNamePrefix.TabIndex = 31
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(454, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Email"
        '
        'txtemail
        '
        Me.txtemail.Location = New System.Drawing.Point(504, 45)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(300, 20)
        Me.txtemail.TabIndex = 30
        '
        'lblmobile
        '
        Me.lblmobile.AutoSize = True
        Me.lblmobile.Location = New System.Drawing.Point(13, 52)
        Me.lblmobile.Name = "lblmobile"
        Me.lblmobile.Size = New System.Drawing.Size(38, 13)
        Me.lblmobile.TabIndex = 27
        Me.lblmobile.Text = "Mobile"
        '
        'txtmobile
        '
        Me.txtmobile.Location = New System.Drawing.Point(107, 45)
        Me.txtmobile.Name = "txtmobile"
        Me.txtmobile.Size = New System.Drawing.Size(300, 20)
        Me.txtmobile.TabIndex = 28
        '
        'btnresetinput
        '
        Me.btnresetinput.Location = New System.Drawing.Point(848, 669)
        Me.btnresetinput.Name = "btnresetinput"
        Me.btnresetinput.Size = New System.Drawing.Size(75, 38)
        Me.btnresetinput.TabIndex = 31
        Me.btnresetinput.Text = "Reset Input"
        Me.btnresetinput.UseVisualStyleBackColor = True
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
        Me.InitiatorName.HeaderText = "CSR"
        Me.InitiatorName.Name = "InitiatorName"
        Me.InitiatorName.ReadOnly = True
        '
        'GreetingName
        '
        Me.GreetingName.DataPropertyName = "GreetingName"
        Me.GreetingName.HeaderText = "GreetingName"
        Me.GreetingName.Name = "GreetingName"
        Me.GreetingName.ReadOnly = True
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
        'EventSourceInitiator
        '
        Me.EventSourceInitiator.DataPropertyName = "EventSourceInitiator"
        Me.EventSourceInitiator.HeaderText = "Initiator"
        Me.EventSourceInitiator.Name = "EventSourceInitiator"
        Me.EventSourceInitiator.ReadOnly = True
        '
        'EventSourceMedium
        '
        Me.EventSourceMedium.DataPropertyName = "EventSourceMedium"
        Me.EventSourceMedium.HeaderText = "Medium"
        Me.EventSourceMedium.Name = "EventSourceMedium"
        Me.EventSourceMedium.ReadOnly = True
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
        'PhoneMobile
        '
        Me.PhoneMobile.DataPropertyName = "PhoneMobile"
        Me.PhoneMobile.HeaderText = "PhoneMobile"
        Me.PhoneMobile.Name = "PhoneMobile"
        Me.PhoneMobile.ReadOnly = True
        '
        'EmailAddr
        '
        Me.EmailAddr.DataPropertyName = "EmailAddr"
        Me.EmailAddr.HeaderText = "EmailAddr"
        Me.EmailAddr.Name = "EmailAddr"
        Me.EmailAddr.ReadOnly = True
        '
        'Call_id
        '
        Me.Call_id.DataPropertyName = "Call_id"
        Me.Call_id.HeaderText = "Call_id"
        Me.Call_id.Name = "Call_id"
        Me.Call_id.ReadOnly = True
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
        'frmHKTransLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 813)
        Me.Controls.Add(Me.btnresetinput)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gbSerach)
        Me.Controls.Add(Me.dgvServiceLog)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.lblReminder)
        Me.Controls.Add(Me.txtRemainder)
        Me.Controls.Add(Me.lblEventNote)
        Me.Controls.Add(Me.txtEventNote)
        Me.Name = "frmHKTransLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hong Kong Service Log (Non-Cust)"
        CType(Me.dgvServiceLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSerach.ResumeLayout(False)
        Me.gbSerach.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMedium As System.Windows.Forms.Label
    Friend WithEvents lblEventCategory As System.Windows.Forms.Label
    Friend WithEvents cboEventCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboEventTypeDetail As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventTypeDetail As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtEventNote As System.Windows.Forms.TextBox
    Friend WithEvents lblEventNote As System.Windows.Forms.Label
    Friend WithEvents lblReminder As System.Windows.Forms.Label
    Friend WithEvents txtRemainder As System.Windows.Forms.TextBox
    Friend WithEvents lblEffectiveDate As System.Windows.Forms.Label
    Friend WithEvents dteEffectiveDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cboMedium As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventDetail As System.Windows.Forms.Label
    Friend WithEvents cboEventDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cboEventSourceInd As System.Windows.Forms.ComboBox
    Friend WithEvents dgvServiceLog As System.Windows.Forms.DataGridView
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cboMonth As System.Windows.Forms.ComboBox
    Friend WithEvents cboUser As System.Windows.Forms.ComboBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSearchStatus As System.Windows.Forms.Label
    Friend WithEvents cboSearchStatus As System.Windows.Forms.ComboBox
    Friend WithEvents gbSerach As System.Windows.Forms.GroupBox
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents chkFCR As System.Windows.Forms.CheckBox
    Friend WithEvents chkMCV As System.Windows.Forms.CheckBox
    Friend WithEvents txtsearchemail As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtsearchmobile As System.Windows.Forms.TextBox
    Friend WithEvents lblcontactphone As System.Windows.Forms.Label
    Friend WithEvents lblgreetingname As System.Windows.Forms.Label
    Friend WithEvents txtgreetingname As System.Windows.Forms.TextBox
    Friend WithEvents txtcallid As System.Windows.Forms.TextBox
    Friend WithEvents lblcallid As System.Windows.Forms.Label
    Friend WithEvents cboYear As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtemail As System.Windows.Forms.TextBox
    Friend WithEvents lblmobile As System.Windows.Forms.Label
    Friend WithEvents txtmobile As System.Windows.Forms.TextBox
    Friend WithEvents cboNamePrefix As System.Windows.Forms.ComboBox
    Friend WithEvents btnresetsearch As System.Windows.Forms.Button
    Friend WithEvents cboHandoffCsr As System.Windows.Forms.ComboBox
    Friend WithEvents btnresetinput As System.Windows.Forms.Button
    Friend WithEvents EventInitialDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitiatorName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GreetingName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCategoryDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDetailDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventSourceInitiator As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventSourceMedium As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventCloseoutCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MCV As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneMobile As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailAddr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Call_id As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents LocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MasterCSRID As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
