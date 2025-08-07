'C001 - Add MCV Indicator in HK & Macau Service Log Screen - Gopu Kalaimani.

Imports System.Data.SqlClient
Imports System.Globalization

Public Class uclServiceLog
    Inherits System.Windows.Forms.UserControl

    Private strPolicy As String
    Private strCustID As String
    Private strNewFlag As String
    Private strPolicyType As String
    Private iPrevPosition As Integer
    Private blnIsCustLevel As Boolean = False

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private blnIsNBMPolicy As Boolean = False
    Private blnIsEnableNBMPolicyPanel As Boolean = False
    Private rejectReasonDropDownListDataTable As DataTable
    Private dataTableReturnRetentionCampaignEnquiry As DataTable

    Dim sqlConn As New SqlConnection
    Dim sqlConn2 As New SqlConnection 'AL20210201 eService PI Access
    Dim da1 As SqlDataAdapter
    Dim dt1 As New DataTable
    Dim daSrvEvtDet As SqlDataAdapter
    Dim daEvtCat As SqlDataAdapter
    Dim daEvtType As SqlDataAdapter
    Dim daEvtTypeDet As SqlDataAdapter
    Dim daMedium As SqlDataAdapter
    Dim daStatus As SqlDataAdapter
    Dim daCsr As SqlDataAdapter
    Dim daInitiator As SqlDataAdapter
    Dim daCustomer As SqlDataAdapter
    Dim daPostSalesCallInfo As SqlDataAdapter   'added by ITDYMH 20150229 Post-Sales Call
    Dim daTmp As SqlDataAdapter 'AL20210201 eService PI Access
    Dim dsSrvLog As New DataSet
    Dim dsTmp As New DataSet 'AL20210201 eService PI Access
    Dim sPiEmail As String = "" 'AL20210201 eService PI Access
    Dim sPiID As String = "" 'AL20210201 eService PI Access
    Dim ServiceLogBL As New ServiceLogBL
    Friend WithEvents chkFCR As System.Windows.Forms.CheckBox
    Friend WithEvents chkACC As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbl_InforceDate As System.Windows.Forms.Label
    Friend WithEvents lbl_PostCallStatus As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lbl_PostCallCount As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents rad_NVCWelcomeCall As System.Windows.Forms.RadioButton
    Friend WithEvents rad_VCPostSalesCall As System.Windows.Forms.RadioButton
    Friend WithEvents rad_ILASPostSalseCall As System.Windows.Forms.RadioButton
    Friend WithEvents SaveFileDialogDownlaod As System.Windows.Forms.SaveFileDialog
    Friend WithEvents gboPolicy As System.Windows.Forms.GroupBox
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbEB As System.Windows.Forms.RadioButton
    Friend WithEvents rbGI As System.Windows.Forms.RadioButton
    Friend WithEvents rbLife As System.Windows.Forms.RadioButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents rad_SuitabilityMisMatch As System.Windows.Forms.RadioButton
    Friend WithEvents chkIdVerify As System.Windows.Forms.CheckBox
    Friend WithEvents chkMCV As System.Windows.Forms.CheckBox
    Friend WithEvents btncustlogweb As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents gboCustomer As GroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtCustomerID As TextBox
    Friend WithEvents gbPiEservicesAuth As GroupBox
    Friend WithEvents btnDisablePI As Button
    Friend WithEvents dgvAuthPi As DataGridView
    Friend WithEvents btnAuthToPi As Button
    Friend WithEvents gbNBM As GroupBox
    Friend WithEvents txtCampaignDetails As TextBox
    Friend WithEvents lblCampaignDetails As Label
    Friend WithEvents txtFinishDate As TextBox
    Friend WithEvents lblFinishDate As Label
    Friend WithEvents txtNetPremium As TextBox
    Friend WithEvents lblNetPremium As Label
    Friend WithEvents lblReason As Label
    Friend WithEvents cbRejectReason As ComboBox
    Friend WithEvents txtEffectiveDate As TextBox
    Friend WithEvents lblEffectiveDate As Label
    Friend WithEvents palYesNo As Panel
    Friend WithEvents rbNo As RadioButton
    Friend WithEvents rbYes As RadioButton
    Dim bm As BindingManagerBase

    Public Delegate Sub EventSavedEventHandler(ByVal sender As Object, ByVal e As DataRow)

    Public Event EventSaved As EventSavedEventHandler

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dgSrvLog As System.Windows.Forms.DataGrid
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents grpServiceEvent As System.Windows.Forms.GroupBox
    Friend WithEvents dtInitial As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkAES As System.Windows.Forms.CheckBox
    Friend WithEvents lbReceiver As System.Windows.Forms.Label
    Friend WithEvents cbReceiver As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbInitiator As System.Windows.Forms.ComboBox
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventCat As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventTypeDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cbMedium As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents hidSrvEvtNo As System.Windows.Forms.TextBox
    Friend WithEvents hidPolicy As System.Windows.Forms.TextBox
    Friend WithEvents hidCustID As System.Windows.Forms.TextBox
    Friend WithEvents hidSender As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicyAlert As System.Windows.Forms.TextBox
    Friend WithEvents txtReminder As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents chkPolicyAlert As System.Windows.Forms.CheckBox
    Friend WithEvents dtReminder As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkReminder As System.Windows.Forms.CheckBox
    Friend WithEvents txtDownloadPostSalesCall As System.Windows.Forms.Button
    Friend WithEvents btnSaveC As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgSrvLog = New System.Windows.Forms.DataGrid()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.grpServiceEvent = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbEventCat = New System.Windows.Forms.ComboBox()
        Me.cbEventDetail = New System.Windows.Forms.ComboBox()
        Me.cbEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.cbMedium = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtInitial = New System.Windows.Forms.DateTimePicker()
        Me.chkAES = New System.Windows.Forms.CheckBox()
        Me.lbReceiver = New System.Windows.Forms.Label()
        Me.cbReceiver = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbInitiator = New System.Windows.Forms.ComboBox()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.hidSrvEvtNo = New System.Windows.Forms.TextBox()
        Me.dtReminder = New System.Windows.Forms.DateTimePicker()
        Me.chkPolicyAlert = New System.Windows.Forms.CheckBox()
        Me.txtPolicyAlert = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkIdVerify = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkReminder = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtReminder = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.hidPolicy = New System.Windows.Forms.TextBox()
        Me.hidCustID = New System.Windows.Forms.TextBox()
        Me.hidSender = New System.Windows.Forms.TextBox()
        Me.txtDownloadPostSalesCall = New System.Windows.Forms.Button()
        Me.btnSaveC = New System.Windows.Forms.Button()
        Me.chkFCR = New System.Windows.Forms.CheckBox()
        Me.chkACC = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl_InforceDate = New System.Windows.Forms.Label()
        Me.lbl_PostCallStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbl_PostCallCount = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.rad_NVCWelcomeCall = New System.Windows.Forms.RadioButton()
        Me.rad_VCPostSalesCall = New System.Windows.Forms.RadioButton()
        Me.rad_ILASPostSalseCall = New System.Windows.Forms.RadioButton()
        Me.gboPolicy = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbEB = New System.Windows.Forms.RadioButton()
        Me.rbGI = New System.Windows.Forms.RadioButton()
        Me.rbLife = New System.Windows.Forms.RadioButton()
        Me.rad_SuitabilityMisMatch = New System.Windows.Forms.RadioButton()
        Me.chkMCV = New System.Windows.Forms.CheckBox()
        Me.btncustlogweb = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.gboCustomer = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gbPiEservicesAuth = New System.Windows.Forms.GroupBox()
        Me.btnDisablePI = New System.Windows.Forms.Button()
        Me.dgvAuthPi = New System.Windows.Forms.DataGridView()
        Me.btnAuthToPi = New System.Windows.Forms.Button()
        Me.gbNBM = New System.Windows.Forms.GroupBox()
        Me.txtCampaignDetails = New System.Windows.Forms.TextBox()
        Me.lblCampaignDetails = New System.Windows.Forms.Label()
        Me.txtFinishDate = New System.Windows.Forms.TextBox()
        Me.lblFinishDate = New System.Windows.Forms.Label()
        Me.txtNetPremium = New System.Windows.Forms.TextBox()
        Me.lblNetPremium = New System.Windows.Forms.Label()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.cbRejectReason = New System.Windows.Forms.ComboBox()
        Me.txtEffectiveDate = New System.Windows.Forms.TextBox()
        Me.lblEffectiveDate = New System.Windows.Forms.Label()
        Me.palYesNo = New System.Windows.Forms.Panel()
        Me.rbNo = New System.Windows.Forms.RadioButton()
        Me.rbYes = New System.Windows.Forms.RadioButton()
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpServiceEvent.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gboPolicy.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gboCustomer.SuspendLayout()
        Me.gbPiEservicesAuth.SuspendLayout()
        CType(Me.dgvAuthPi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbNBM.SuspendLayout()
        Me.palYesNo.SuspendLayout()
        Me.gboCustomer.SuspendLayout()
        Me.SuspendLayout()
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
        Me.dgSrvLog.Location = New System.Drawing.Point(6, 8)
        Me.dgSrvLog.MaximumSize = New System.Drawing.Size(820, 108)
        Me.dgSrvLog.Name = "dgSrvLog"
        Me.dgSrvLog.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgSrvLog.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgSrvLog.ReadOnly = True
        Me.dgSrvLog.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgSrvLog.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgSrvLog.Size = New System.Drawing.Size(803, 108)
        Me.dgSrvLog.TabIndex = 19
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(1132, 47)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 23)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(1132, 79)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "&Cancel"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1132, 15)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(64, 23)
        Me.btnNew.TabIndex = 15
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
        Me.grpServiceEvent.Location = New System.Drawing.Point(8, 124)
        Me.grpServiceEvent.Name = "grpServiceEvent"
        Me.grpServiceEvent.Size = New System.Drawing.Size(381, 136)
        Me.grpServiceEvent.TabIndex = 14
        Me.grpServiceEvent.TabStop = False
        Me.grpServiceEvent.Text = "Service Event"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(12, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Event Detail"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Medium"
        '
        'cbEventCat
        '
        Me.cbEventCat.BackColor = System.Drawing.Color.White
        Me.cbEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventCat.Location = New System.Drawing.Point(104, 44)
        Me.cbEventCat.Name = "cbEventCat"
        Me.cbEventCat.Size = New System.Drawing.Size(263, 28)
        Me.cbEventCat.TabIndex = 1
        '
        'cbEventDetail
        '
        Me.cbEventDetail.BackColor = System.Drawing.Color.White
        Me.cbEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventDetail.Location = New System.Drawing.Point(104, 68)
        Me.cbEventDetail.Name = "cbEventDetail"
        Me.cbEventDetail.Size = New System.Drawing.Size(263, 28)
        Me.cbEventDetail.TabIndex = 2
        '
        'cbEventTypeDetail
        '
        Me.cbEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cbEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventTypeDetail.Location = New System.Drawing.Point(104, 92)
        Me.cbEventTypeDetail.Name = "cbEventTypeDetail"
        Me.cbEventTypeDetail.Size = New System.Drawing.Size(263, 28)
        Me.cbEventTypeDetail.TabIndex = 3
        '
        'cbMedium
        '
        Me.cbMedium.BackColor = System.Drawing.Color.White
        Me.cbMedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMedium.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbMedium.Location = New System.Drawing.Point(104, 20)
        Me.cbMedium.Name = "cbMedium"
        Me.cbMedium.Size = New System.Drawing.Size(263, 28)
        Me.cbMedium.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 20)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Event Category"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 16)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Event Type Detail"
        '
        'dtInitial
        '
        Me.dtInitial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInitial.Location = New System.Drawing.Point(72, 96)
        Me.dtInitial.Name = "dtInitial"
        Me.dtInitial.Size = New System.Drawing.Size(164, 26)
        Me.dtInitial.TabIndex = 8
        '
        'chkAES
        '
        Me.chkAES.Enabled = False
        Me.chkAES.Location = New System.Drawing.Point(250, 24)
        Me.chkAES.Name = "chkAES"
        Me.chkAES.Size = New System.Drawing.Size(104, 16)
        Me.chkAES.TabIndex = 5
        Me.chkAES.Text = "Transfer to AES"
        '
        'lbReceiver
        '
        Me.lbReceiver.Enabled = False
        Me.lbReceiver.Location = New System.Drawing.Point(12, 52)
        Me.lbReceiver.Name = "lbReceiver"
        Me.lbReceiver.Size = New System.Drawing.Size(52, 16)
        Me.lbReceiver.TabIndex = 15
        Me.lbReceiver.Text = "Reciever"
        '
        'cbReceiver
        '
        Me.cbReceiver.BackColor = System.Drawing.Color.White
        Me.cbReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReceiver.Enabled = False
        Me.cbReceiver.Location = New System.Drawing.Point(72, 50)
        Me.cbReceiver.Name = "cbReceiver"
        Me.cbReceiver.Size = New System.Drawing.Size(295, 28)
        Me.cbReceiver.TabIndex = 6
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(12, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 16)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Initial Date"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(12, 76)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 16)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Initiator"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 16)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Status"
        '
        'cbInitiator
        '
        Me.cbInitiator.BackColor = System.Drawing.Color.White
        Me.cbInitiator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInitiator.Location = New System.Drawing.Point(72, 72)
        Me.cbInitiator.Name = "cbInitiator"
        Me.cbInitiator.Size = New System.Drawing.Size(295, 28)
        Me.cbInitiator.TabIndex = 7
        '
        'cbStatus
        '
        Me.cbStatus.BackColor = System.Drawing.Color.White
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.Location = New System.Drawing.Point(72, 24)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(172, 28)
        Me.cbStatus.TabIndex = 4
        '
        'hidSrvEvtNo
        '
        Me.hidSrvEvtNo.Location = New System.Drawing.Point(1132, 131)
        Me.hidSrvEvtNo.Name = "hidSrvEvtNo"
        Me.hidSrvEvtNo.Size = New System.Drawing.Size(68, 26)
        Me.hidSrvEvtNo.TabIndex = 16
        Me.hidSrvEvtNo.Visible = False
        Me.hidSrvEvtNo.WordWrap = False
        '
        'dtReminder
        '
        Me.dtReminder.Location = New System.Drawing.Point(196, 97)
        Me.dtReminder.Name = "dtReminder"
        Me.dtReminder.Size = New System.Drawing.Size(108, 26)
        Me.dtReminder.TabIndex = 12
        '
        'chkPolicyAlert
        '
        Me.chkPolicyAlert.Location = New System.Drawing.Point(80, 21)
        Me.chkPolicyAlert.Name = "chkPolicyAlert"
        Me.chkPolicyAlert.Size = New System.Drawing.Size(96, 16)
        Me.chkPolicyAlert.TabIndex = 9
        Me.chkPolicyAlert.Text = "Prompted?"
        '
        'txtPolicyAlert
        '
        Me.txtPolicyAlert.Location = New System.Drawing.Point(12, 39)
        Me.txtPolicyAlert.MaxLength = 256
        Me.txtPolicyAlert.Multiline = True
        Me.txtPolicyAlert.Name = "txtPolicyAlert"
        Me.txtPolicyAlert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPolicyAlert.Size = New System.Drawing.Size(704, 44)
        Me.txtPolicyAlert.TabIndex = 10
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
        Me.GroupBox1.Location = New System.Drawing.Point(395, 124)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(722, 362)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Alert and Notes"
        '
        'chkIdVerify
        '
        Me.chkIdVerify.Location = New System.Drawing.Point(80, 177)
        Me.chkIdVerify.Name = "chkIdVerify"
        Me.chkIdVerify.Size = New System.Drawing.Size(81, 17)
        Me.chkIdVerify.TabIndex = 0
        Me.chkIdVerify.Text = "ID Verify"
        Me.chkIdVerify.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(160, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Date:"
        '
        'chkReminder
        '
        Me.chkReminder.Location = New System.Drawing.Point(80, 99)
        Me.chkReminder.Name = "chkReminder"
        Me.chkReminder.Size = New System.Drawing.Size(80, 16)
        Me.chkReminder.TabIndex = 11
        Me.chkReminder.Text = "Prompted?"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Policy Alert"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 14)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Reminder"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 179)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 16)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(12, 195)
        Me.txtNotes.MaxLength = 900
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(704, 160)
        Me.txtNotes.TabIndex = 14
        '
        'txtReminder
        '
        Me.txtReminder.Location = New System.Drawing.Point(12, 119)
        Me.txtReminder.MaxLength = 900
        Me.txtReminder.Multiline = True
        Me.txtReminder.Name = "txtReminder"
        Me.txtReminder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReminder.Size = New System.Drawing.Size(704, 48)
        Me.txtReminder.TabIndex = 13
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
        Me.GroupBox2.Location = New System.Drawing.Point(8, 266)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(381, 141)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Status"
        '
        'hidPolicy
        '
        Me.hidPolicy.Location = New System.Drawing.Point(1132, 155)
        Me.hidPolicy.Name = "hidPolicy"
        Me.hidPolicy.Size = New System.Drawing.Size(68, 26)
        Me.hidPolicy.TabIndex = 23
        Me.hidPolicy.Visible = False
        '
        'hidCustID
        '
        Me.hidCustID.Location = New System.Drawing.Point(1128, 170)
        Me.hidCustID.Name = "hidCustID"
        Me.hidCustID.Size = New System.Drawing.Size(68, 26)
        Me.hidCustID.TabIndex = 24
        Me.hidCustID.Visible = False
        '
        'hidSender
        '
        Me.hidSender.Location = New System.Drawing.Point(1132, 199)
        Me.hidSender.Name = "hidSender"
        Me.hidSender.Size = New System.Drawing.Size(68, 26)
        Me.hidSender.TabIndex = 25
        Me.hidSender.Visible = False
        '
        'txtDownloadPostSalesCall
        '
        Me.txtDownloadPostSalesCall.Location = New System.Drawing.Point(1132, 257)
        Me.txtDownloadPostSalesCall.Name = "txtDownloadPostSalesCall"
        Me.txtDownloadPostSalesCall.Size = New System.Drawing.Size(153, 21)
        Me.txtDownloadPostSalesCall.TabIndex = 26
        Me.txtDownloadPostSalesCall.Text = "Button1"
        Me.txtDownloadPostSalesCall.Visible = False
        '
        'btnSaveC
        '
        Me.btnSaveC.Location = New System.Drawing.Point(1132, 111)
        Me.btnSaveC.Name = "btnSaveC"
        Me.btnSaveC.Size = New System.Drawing.Size(64, 38)
        Me.btnSaveC.TabIndex = 27
        Me.btnSaveC.Text = "&Save && Close"
        '
        'chkFCR
        '
        Me.chkFCR.AutoSize = True
        Me.chkFCR.BackColor = System.Drawing.Color.Transparent
        Me.chkFCR.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkFCR.ForeColor = System.Drawing.Color.Indigo
        Me.chkFCR.Location = New System.Drawing.Point(1132, 157)
        Me.chkFCR.Name = "chkFCR"
        Me.chkFCR.Size = New System.Drawing.Size(81, 29)
        Me.chkFCR.TabIndex = 30
        Me.chkFCR.Text = "FCR"
        Me.chkFCR.UseVisualStyleBackColor = False
        '
        'chkACC
        '
        Me.chkACC.BackColor = System.Drawing.Color.Transparent
        Me.chkACC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkACC.ForeColor = System.Drawing.Color.Indigo
        Me.chkACC.Location = New System.Drawing.Point(1132, 173)
        Me.chkACC.Name = "chkACC"
        Me.chkACC.Size = New System.Drawing.Size(116, 78)
        Me.chkACC.TabIndex = 31
        Me.chkACC.Text = "Suggestions/Grievances"
        Me.chkACC.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1128, 351)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 20)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Inforce Date :"
        '
        'lbl_InforceDate
        '
        Me.lbl_InforceDate.AutoSize = True
        Me.lbl_InforceDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InforceDate.Location = New System.Drawing.Point(1220, 351)
        Me.lbl_InforceDate.Name = "lbl_InforceDate"
        Me.lbl_InforceDate.Size = New System.Drawing.Size(113, 20)
        Me.lbl_InforceDate.TabIndex = 34
        Me.lbl_InforceDate.Text = "01 Mar 2016"
        '
        'lbl_PostCallStatus
        '
        Me.lbl_PostCallStatus.AutoSize = True
        Me.lbl_PostCallStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallStatus.Location = New System.Drawing.Point(1221, 366)
        Me.lbl_PostCallStatus.Name = "lbl_PostCallStatus"
        Me.lbl_PostCallStatus.Size = New System.Drawing.Size(113, 20)
        Me.lbl_PostCallStatus.TabIndex = 36
        Me.lbl_PostCallStatus.Text = "InCompleted"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1128, 366)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(140, 20)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Post Call Status :"
        '
        'lbl_PostCallCount
        '
        Me.lbl_PostCallCount.AutoSize = True
        Me.lbl_PostCallCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallCount.Location = New System.Drawing.Point(1221, 381)
        Me.lbl_PostCallCount.Name = "lbl_PostCallCount"
        Me.lbl_PostCallCount.Size = New System.Drawing.Size(85, 20)
        Me.lbl_PostCallCount.TabIndex = 38
        Me.lbl_PostCallCount.Text = "0 time(s)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1128, 381)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(138, 20)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Post Call Times :"
        '
        'rad_NVCWelcomeCall
        '
        Me.rad_NVCWelcomeCall.AutoCheck = False
        Me.rad_NVCWelcomeCall.AutoSize = True
        Me.rad_NVCWelcomeCall.Location = New System.Drawing.Point(1130, 299)
        Me.rad_NVCWelcomeCall.Name = "rad_NVCWelcomeCall"
        Me.rad_NVCWelcomeCall.Size = New System.Drawing.Size(194, 24)
        Me.rad_NVCWelcomeCall.TabIndex = 39
        Me.rad_NVCWelcomeCall.TabStop = True
        Me.rad_NVCWelcomeCall.Text = "Welcome Call - NonVC"
        Me.rad_NVCWelcomeCall.UseVisualStyleBackColor = True
        '
        'rad_VCPostSalesCall
        '
        Me.rad_VCPostSalesCall.AutoCheck = False
        Me.rad_VCPostSalesCall.AutoSize = True
        Me.rad_VCPostSalesCall.Location = New System.Drawing.Point(1130, 315)
        Me.rad_VCPostSalesCall.Name = "rad_VCPostSalesCall"
        Me.rad_VCPostSalesCall.Size = New System.Drawing.Size(176, 24)
        Me.rad_VCPostSalesCall.TabIndex = 40
        Me.rad_VCPostSalesCall.TabStop = True
        Me.rad_VCPostSalesCall.Text = "Post-Sales Call - VC"
        Me.rad_VCPostSalesCall.UseVisualStyleBackColor = True
        '
        'rad_ILASPostSalseCall
        '
        Me.rad_ILASPostSalseCall.AutoCheck = False
        Me.rad_ILASPostSalseCall.AutoSize = True
        Me.rad_ILASPostSalseCall.Location = New System.Drawing.Point(1130, 331)
        Me.rad_ILASPostSalseCall.Name = "rad_ILASPostSalseCall"
        Me.rad_ILASPostSalseCall.Size = New System.Drawing.Size(181, 24)
        Me.rad_ILASPostSalseCall.TabIndex = 41
        Me.rad_ILASPostSalseCall.TabStop = True
        Me.rad_ILASPostSalseCall.Text = "ILAS Post-Sales Call"
        Me.rad_ILASPostSalseCall.UseVisualStyleBackColor = True
        '
        'gboPolicy
        '
        Me.gboPolicy.Controls.Add(Me.Label13)
        Me.gboPolicy.Controls.Add(Me.txtPolicyNo)
        Me.gboPolicy.Controls.Add(Me.lblPolicyNo)
        Me.gboPolicy.Controls.Add(Me.Panel1)
        Me.gboPolicy.Location = New System.Drawing.Point(8, 413)
        Me.gboPolicy.Name = "gboPolicy"
        Me.gboPolicy.Size = New System.Drawing.Size(381, 74)
        Me.gboPolicy.TabIndex = 42
        Me.gboPolicy.TabStop = False
        Me.gboPolicy.Text = "Policy"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(6, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 16)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "Policy Type"
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.Location = New System.Drawing.Point(79, 47)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(140, 26)
        Me.txtPolicyNo.TabIndex = 45
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.Location = New System.Drawing.Point(6, 50)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(96, 16)
        Me.lblPolicyNo.TabIndex = 44
        Me.lblPolicyNo.Text = "Policy No."
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbEB)
        Me.Panel1.Controls.Add(Me.rbGI)
        Me.Panel1.Controls.Add(Me.rbLife)
        Me.Panel1.Location = New System.Drawing.Point(147, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(157, 25)
        Me.Panel1.TabIndex = 43
        '
        'rbEB
        '
        Me.rbEB.AutoSize = True
        Me.rbEB.Location = New System.Drawing.Point(98, 5)
        Me.rbEB.Name = "rbEB"
        Me.rbEB.Size = New System.Drawing.Size(56, 24)
        Me.rbEB.TabIndex = 16
        Me.rbEB.Text = "EB"
        Me.rbEB.UseVisualStyleBackColor = True
        '
        'rbGI
        '
        Me.rbGI.AutoSize = True
        Me.rbGI.Location = New System.Drawing.Point(55, 5)
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
        Me.rbLife.Location = New System.Drawing.Point(7, 5)
        Me.rbLife.Name = "rbLife"
        Me.rbLife.Size = New System.Drawing.Size(60, 24)
        Me.rbLife.TabIndex = 14
        Me.rbLife.TabStop = True
        Me.rbLife.Text = "Life"
        Me.rbLife.UseVisualStyleBackColor = True
        '
        'rad_SuitabilityMisMatch
        '
        Me.rad_SuitabilityMisMatch.AutoCheck = False
        Me.rad_SuitabilityMisMatch.AutoSize = True
        Me.rad_SuitabilityMisMatch.Location = New System.Drawing.Point(1130, 284)
        Me.rad_SuitabilityMisMatch.Name = "rad_SuitabilityMisMatch"
        Me.rad_SuitabilityMisMatch.Size = New System.Drawing.Size(177, 24)
        Me.rad_SuitabilityMisMatch.TabIndex = 43
        Me.rad_SuitabilityMisMatch.TabStop = True
        Me.rad_SuitabilityMisMatch.Text = "Suitability Mismatch "
        Me.rad_SuitabilityMisMatch.UseVisualStyleBackColor = True
        '
        'chkMCV
        '
        Me.chkMCV.AutoSize = True
        Me.chkMCV.BackColor = System.Drawing.Color.Transparent
        Me.chkMCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkMCV.ForeColor = System.Drawing.Color.Red
        Me.chkMCV.Location = New System.Drawing.Point(1132, 178)
        Me.chkMCV.Name = "chkMCV"
        Me.chkMCV.Size = New System.Drawing.Size(87, 29)
        Me.chkMCV.TabIndex = 44
        Me.chkMCV.Text = "MCV"
        Me.chkMCV.UseVisualStyleBackColor = False
        '
        'btncustlogweb
        '
        Me.btncustlogweb.Location = New System.Drawing.Point(1039, 56)
        Me.btncustlogweb.Name = "btncustlogweb"
        Me.btncustlogweb.Size = New System.Drawing.Size(75, 55)
        Me.btncustlogweb.TabIndex = 45
        Me.btncustlogweb.Text = "Show Contact History"
        Me.btncustlogweb.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'gboCustomer
        '
        Me.gboCustomer.Controls.Add(Me.txtCustomerID)
        Me.gboCustomer.Controls.Add(Me.Label14)
        Me.gboCustomer.Controls.Add(Me.txtCustomerName)
        Me.gboCustomer.Controls.Add(Me.Label16)
        Me.gboCustomer.Location = New System.Drawing.Point(6, 498)
        Me.gboCustomer.Name = "gboCustomer"
        Me.gboCustomer.Size = New System.Drawing.Size(381, 82)
        Me.gboCustomer.TabIndex = 47
        Me.gboCustomer.TabStop = False
        Me.gboCustomer.Text = "Customer"
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
        'gbPiEservicesAuth
        '
        Me.gbPiEservicesAuth.Controls.Add(Me.btnDisablePI)
        Me.gbPiEservicesAuth.Controls.Add(Me.dgvAuthPi)
        Me.gbPiEservicesAuth.Controls.Add(Me.btnAuthToPi)
        Me.gbPiEservicesAuth.Location = New System.Drawing.Point(395, 498)
        Me.gbPiEservicesAuth.Name = "gbPiEservicesAuth"
        Me.gbPiEservicesAuth.Size = New System.Drawing.Size(566, 82)
        Me.gbPiEservicesAuth.TabIndex = 48
        Me.gbPiEservicesAuth.TabStop = False
        Me.gbPiEservicesAuth.Text = "PI eServices Auth"
        '
        'btnDisablePI
        '
        Me.btnDisablePI.Enabled = False
        Me.btnDisablePI.Location = New System.Drawing.Point(460, 24)
        Me.btnDisablePI.Name = "btnDisablePI"
        Me.btnDisablePI.Size = New System.Drawing.Size(100, 23)
        Me.btnDisablePI.TabIndex = 5
        Me.btnDisablePI.Text = "Disable"
        Me.btnDisablePI.UseVisualStyleBackColor = True
        '
        'dgvAuthPi
        '
        Me.dgvAuthPi.AllowUserToAddRows = False
        Me.dgvAuthPi.AllowUserToDeleteRows = False
        Me.dgvAuthPi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvAuthPi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAuthPi.Location = New System.Drawing.Point(12, 24)
        Me.dgvAuthPi.MultiSelect = False
        Me.dgvAuthPi.Name = "dgvAuthPi"
        Me.dgvAuthPi.ReadOnly = True
        Me.dgvAuthPi.RowHeadersWidth = 62
        Me.dgvAuthPi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAuthPi.Size = New System.Drawing.Size(442, 50)
        Me.dgvAuthPi.TabIndex = 4
        '
        'btnAuthToPi
        '
        Me.btnAuthToPi.Enabled = False
        Me.btnAuthToPi.Location = New System.Drawing.Point(460, 51)
        Me.btnAuthToPi.Name = "btnAuthToPi"
        Me.btnAuthToPi.Size = New System.Drawing.Size(100, 23)
        Me.btnAuthToPi.TabIndex = 3
        Me.btnAuthToPi.Text = "Auth to PI"
        Me.btnAuthToPi.UseVisualStyleBackColor = True
        '
        'gbNBM
        '
        Me.gbNBM.Controls.Add(Me.txtCampaignDetails)
        Me.gbNBM.Controls.Add(Me.lblCampaignDetails)
        Me.gbNBM.Controls.Add(Me.txtFinishDate)
        Me.gbNBM.Controls.Add(Me.lblFinishDate)
        Me.gbNBM.Controls.Add(Me.txtNetPremium)
        Me.gbNBM.Controls.Add(Me.lblNetPremium)
        Me.gbNBM.Controls.Add(Me.lblReason)
        Me.gbNBM.Controls.Add(Me.cbRejectReason)
        Me.gbNBM.Controls.Add(Me.txtEffectiveDate)
        Me.gbNBM.Controls.Add(Me.lblEffectiveDate)
        Me.gbNBM.Controls.Add(Me.palYesNo)
        Me.gbNBM.Enabled = False
        Me.gbNBM.Location = New System.Drawing.Point(395, 586)
        Me.gbNBM.Name = "gbNBM"
        Me.gbNBM.Size = New System.Drawing.Size(722, 196)
        Me.gbNBM.TabIndex = 51
        Me.gbNBM.TabStop = False
        Me.gbNBM.Text = "NBM Retention Offer"
        '
        'txtCampaignDetails
        '
        Me.txtCampaignDetails.BackColor = System.Drawing.SystemColors.Window
        Me.txtCampaignDetails.Location = New System.Drawing.Point(12, 138)
        Me.txtCampaignDetails.Multiline = True
        Me.txtCampaignDetails.Name = "txtCampaignDetails"
        Me.txtCampaignDetails.ReadOnly = True
        Me.txtCampaignDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCampaignDetails.Size = New System.Drawing.Size(705, 49)
        Me.txtCampaignDetails.TabIndex = 51
        '
        'lblCampaignDetails
        '
        Me.lblCampaignDetails.Location = New System.Drawing.Point(12, 119)
        Me.lblCampaignDetails.Name = "lblCampaignDetails"
        Me.lblCampaignDetails.Size = New System.Drawing.Size(170, 16)
        Me.lblCampaignDetails.TabIndex = 50
        Me.lblCampaignDetails.Text = "Retention Campaign Details:"
        '
        'txtFinishDate
        '
        Me.txtFinishDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtFinishDate.Location = New System.Drawing.Point(128, 91)
        Me.txtFinishDate.Name = "txtFinishDate"
        Me.txtFinishDate.ReadOnly = True
        Me.txtFinishDate.Size = New System.Drawing.Size(112, 20)
        Me.txtFinishDate.TabIndex = 49
        '
        'lblFinishDate
        '
        Me.lblFinishDate.Location = New System.Drawing.Point(12, 94)
        Me.lblFinishDate.Name = "lblFinishDate"
        Me.lblFinishDate.Size = New System.Drawing.Size(116, 16)
        Me.lblFinishDate.TabIndex = 48
        Me.lblFinishDate.Text = "Campaign Finish Date:"
        '
        'txtNetPremium
        '
        Me.txtNetPremium.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetPremium.Location = New System.Drawing.Point(463, 60)
        Me.txtNetPremium.Name = "txtNetPremium"
        Me.txtNetPremium.ReadOnly = True
        Me.txtNetPremium.Size = New System.Drawing.Size(136, 20)
        Me.txtNetPremium.TabIndex = 47
        '
        'lblNetPremium
        '
        Me.lblNetPremium.Location = New System.Drawing.Point(217, 63)
        Me.lblNetPremium.Name = "lblNetPremium"
        Me.lblNetPremium.Size = New System.Drawing.Size(254, 16)
        Me.lblNetPremium.TabIndex = 46
        Me.lblNetPremium.Text = "Net Premium of Next Billing Under New Campaign:"
        '
        'lblReason
        '
        Me.lblReason.Location = New System.Drawing.Point(119, 27)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(80, 16)
        Me.lblReason.TabIndex = 12
        Me.lblReason.Text = "Reject Reason"
        '
        'cbRejectReason
        '
        Me.cbRejectReason.BackColor = System.Drawing.Color.White
        Me.cbRejectReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRejectReason.Enabled = False
        Me.cbRejectReason.Location = New System.Drawing.Point(202, 23)
        Me.cbRejectReason.Name = "cbRejectReason"
        Me.cbRejectReason.Size = New System.Drawing.Size(200, 21)
        Me.cbRejectReason.TabIndex = 11
        '
        'txtEffectiveDate
        '
        Me.txtEffectiveDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEffectiveDate.Location = New System.Drawing.Point(95, 60)
        Me.txtEffectiveDate.Name = "txtEffectiveDate"
        Me.txtEffectiveDate.ReadOnly = True
        Me.txtEffectiveDate.Size = New System.Drawing.Size(112, 20)
        Me.txtEffectiveDate.TabIndex = 45
        '
        'lblEffectiveDate
        '
        Me.lblEffectiveDate.Location = New System.Drawing.Point(12, 63)
        Me.lblEffectiveDate.Name = "lblEffectiveDate"
        Me.lblEffectiveDate.Size = New System.Drawing.Size(96, 16)
        Me.lblEffectiveDate.TabIndex = 44
        Me.lblEffectiveDate.Text = "Effective Date: "
        '
        'palYesNo
        '
        Me.palYesNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.palYesNo.Controls.Add(Me.rbNo)
        Me.palYesNo.Controls.Add(Me.rbYes)
        Me.palYesNo.Location = New System.Drawing.Point(12, 20)
        Me.palYesNo.Name = "palYesNo"
        Me.palYesNo.Size = New System.Drawing.Size(99, 25)
        Me.palYesNo.TabIndex = 43
        '
        'rbNo
        '
        Me.rbNo.AutoSize = True
        Me.rbNo.Location = New System.Drawing.Point(55, 5)
        Me.rbNo.Name = "rbNo"
        Me.rbNo.Size = New System.Drawing.Size(39, 17)
        Me.rbNo.TabIndex = 15
        Me.rbNo.Text = "No"
        Me.rbNo.UseVisualStyleBackColor = True
        '
        'rbYes
        '
        Me.rbYes.AutoSize = True
        Me.rbYes.Location = New System.Drawing.Point(7, 5)
        Me.rbYes.Name = "rbYes"
        Me.rbYes.Size = New System.Drawing.Size(43, 17)
        Me.rbYes.TabIndex = 14
        Me.rbYes.TabStop = True
        Me.rbYes.Text = "Yes"
        Me.rbYes.UseVisualStyleBackColor = True
        '
        'uclServiceLog
        '
        Me.Controls.Add(Me.gbPiEservicesAuth)
        Me.Controls.Add(Me.gbNBM)
        Me.Controls.Add(Me.gboCustomer)
        Me.Controls.Add(Me.btncustlogweb)
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
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSrvLog)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.grpServiceEvent)
        Me.Controls.Add(Me.hidSrvEvtNo)
        Me.Name = "uclServiceLog"
        Me.Size = New System.Drawing.Size(1346, 792)
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpServiceEvent.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.gboPolicy.ResumeLayout(False)
        Me.gboPolicy.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gboCustomer.ResumeLayout(False)
        Me.gboCustomer.PerformLayout()
        Me.gbPiEservicesAuth.ResumeLayout(False)
        CType(Me.dgvAuthPi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbNBM.ResumeLayout(False)
        Me.gbNBM.PerformLayout()
        Me.palYesNo.ResumeLayout(False)
        Me.palYesNo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public ReadOnly Property PendingSave() As Boolean
        Get
            If Not dsSrvLog.Tables("ServiceEventDetail") Is Nothing Then
                Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()
                Return dsSrvLog.HasChanges
            Else
                Return False
            End If
        End Get
    End Property

    'PolicyAccountID() - Get or set the variable strPolicy (Policy Number)
    Public Property PolicyAccountID(ByVal strCID As String) As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                strCustID = strCID
                strPolicy = Value
                BuildUI()
            End If
        End Set
    End Property

    'CustomerID() - Get or set the variable strCustID (Customer ID)
    Public Property CustomerID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                strCustID = Value
                BuildUI()
                'hidCustID.Text = strCustID
            End If
        End Set
    End Property

    'PolicyType() - Get or set the variable strPolicyType (Policy Type)
    Public Property PolicyType() As String
        Get
            Return strPolicyType
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                strPolicyType = Value

            End If
        End Set
    End Property

    Public WriteOnly Property IsCustLevel() As Boolean
        Set(ByVal Value As Boolean)
            blnIsCustLevel = Value
        End Set
    End Property

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Public Property IsNBMPolicy() As Boolean
        Get
            Return blnIsNBMPolicy
        End Get
        Set(ByVal Value As Boolean)
            blnIsNBMPolicy = Value
        End Set
    End Property

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Public Property ReturnDataTableRetentionCampaignEnquiry() As DataTable
        Get
            Return dataTableReturnRetentionCampaignEnquiry
        End Get
        Set(ByVal Value As DataTable)
            dataTableReturnRetentionCampaignEnquiry = Value
        End Set
    End Property

    Public Function resetDS()
        dsSrvLog.Clear()
        dsSrvLog = New DataSet
        Me.dgSrvLog.TableStyles.Clear()

        'Clear All bindings
        chkAES.DataBindings.Clear()
        hidSrvEvtNo.DataBindings.Clear()
        hidPolicy.DataBindings.Clear()
        hidCustID.DataBindings.Clear()
        hidSender.DataBindings.Clear()
        cbMedium.DataBindings.Clear()
        cbStatus.DataBindings.Clear()
        cbInitiator.DataBindings.Clear()
        cbEventCat.DataBindings.Clear()
        cbEventDetail.DataBindings.Clear()
        cbEventTypeDetail.DataBindings.Clear()
        cbReceiver.DataBindings.Clear()
        dtInitial.DataBindings.Clear()
        txtPolicyAlert.DataBindings.Clear()
        txtReminder.DataBindings.Clear()
        txtNotes.DataBindings.Clear()
        chkPolicyAlert.DataBindings.Clear()
        dtReminder.DataBindings.Clear()
        txtPolicyNo.DataBindings.Clear()
        chkIdVerify.DataBindings.Clear()
    End Function

    'BuildUI() - Display the Service Log History and initialize form for user input
    Private Sub BuildUI()
        InitDataset()
        InitForm()
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub uclServiceLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckEnableNBMPanel()
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub CheckEnableNBMPanel()
        Dim isEnableNBMPanel As Boolean = False
        Try
            isEnableNBMPanel = CheckIsEnableNBMPanel()
            If isEnableNBMPanel Then
                InitNBMPolicyContent()
                InitNBMPolicyRejectReasonDropDownList()
            End If
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
        blnIsEnableNBMPolicyPanel = isEnableNBMPanel
        Me.gbNBM.Enabled = blnIsEnableNBMPolicyPanel
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Function CheckIsEnableNBMPanel() As Boolean
        Dim IsInsertRetentionOfferCampaignRecord As Boolean = CheckNBMPolicyIsInsertRetentionOfferCampaignRecord()
        If Not IsInsertRetentionOfferCampaignRecord Then
            If cbEventTypeDetail.Text = "Policy Surrender" Then
                Return blnIsNBMPolicy
            End If
        End If
        Return False
    End Function

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub InitNBMPolicyContent()
        If dataTableReturnRetentionCampaignEnquiry IsNot Nothing AndAlso dataTableReturnRetentionCampaignEnquiry.Rows.Count > 0 Then
            Me.txtEffectiveDate.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("EffectiveDate")), ParseDateTimeFormat(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("EffectiveDate").ToString()), "")
            Me.txtFinishDate.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CampaignFinishDate")), ParseDateTimeFormat(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CampaignFinishDate").ToString()), "")
            Me.txtNetPremium.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("NetPremiumofNextBillingUnderNewCampaign")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("NetPremiumofNextBillingUnderNewCampaign").ToString(), "")
            Me.txtCampaignDetails.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails").ToString(), "")
        End If
    End Sub

    'oliver 2024-02-28 added for ITSR5061 Retention Offer Campaign 
    Private Function ParseDateTimeFormat(ByVal dateString As String) As String
        Dim dateResult As DateTime
        If DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, dateResult) Then
            Return dateResult.ToString("dd-MMM-yyyy")
        ElseIf DateTime.TryParseExact(dateString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dateResult) Then
            Return dateResult.ToString("yyyyMMdd")
        Else
            Return dateString
        End If
    End Function

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub InitNBMPolicyRejectReasonDropDownList()

        If rejectReasonDropDownListDataTable IsNot Nothing AndAlso rejectReasonDropDownListDataTable.Rows.Count > 0 Then
            SetRejectReasonDropDownListDataTable(rejectReasonDropDownListDataTable)
            Exit Sub
        End If

        Dim rejectReasonDropDownListDataSet As DataSet = APIServiceBL.CallAPIBusi(g_Comp,
                                        "GET_DROPDOWNLIST_BY_TYPE",
                                        New Dictionary(Of String, String) From {
                                        {"gds_type", "RetentionCampaignRejectReason"}
                                        })
        If rejectReasonDropDownListDataSet.Tables.Count > 0 Then
            rejectReasonDropDownListDataTable = rejectReasonDropDownListDataSet.Tables(0).Copy
            SetRejectReasonDropDownListDataTable(rejectReasonDropDownListDataTable)
        End If

    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub SetRejectReasonDropDownListDataTable(ByVal dataTable As DataTable)
        Me.cbRejectReason.DataSource = dataTable
        Me.cbRejectReason.DisplayMember = "gds_value"
        Me.cbRejectReason.ValueMember = "gds_index"
        Me.cbRejectReason.SelectedIndex = 0
        Me.cbRejectReason.Enabled = False
    End Sub

    'oliver 2024-3-1 added for ITSR5061 Retention Offer Campaign 
    Private Function CheckNBMPolicyIsInsertRetentionOfferCampaignRecord() As Boolean
        Try

            If dsSrvLog.Tables("ServiceEventDetail").Rows.Count > 0 Then
                bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
                Dim dr As DataRow = CType(bm.Current, DataRowView).Row()

                Dim searchRetentionOfferCampaignRecordDataSet As DataSet = APIServiceBL.CallAPIBusi(g_Comp,
                                "SEARCH_RETENTION_OFFER_CAMPAIGN_RECORD",
                                New Dictionary(Of String, String) From {
                                {"roc_service_event_number", If(Not String.IsNullOrEmpty(dr.Item("ServiceEventNumber").ToString), dr.Item("ServiceEventNumber").ToString, 0)}
                                })
                Dim searchRetentionOfferCampaignRecordDataTable As New DataTable
                If searchRetentionOfferCampaignRecordDataSet.Tables.Count > 0 AndAlso searchRetentionOfferCampaignRecordDataSet.Tables(0).Rows.Count > 0 Then
                    searchRetentionOfferCampaignRecordDataTable = searchRetentionOfferCampaignRecordDataSet.Tables(0)
                    CheckIsSwitchNBMPolicyIsAcceptOfferAndRejectReasonDropDownItem(searchRetentionOfferCampaignRecordDataTable)
                    Return True
                Else
                    CheckIsSwitchNBMPolicyIsAcceptOfferAndRejectReasonDropDownItem(searchRetentionOfferCampaignRecordDataTable)
                End If

            End If
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try

        Return False

    End Function

    'oliver 2024-3-1 added for ITSR5061 Retention Offer Campaign 
    Private Sub CheckIsSwitchNBMPolicyIsAcceptOfferAndRejectReasonDropDownItem(ByVal searchRetentionOfferCampaignRecordDataTable As DataTable)
        Try
            Me.rbYes.Checked = False
            Me.rbNo.Checked = False
            Me.cbRejectReason.DataSource = Nothing

            If searchRetentionOfferCampaignRecordDataTable.Rows.Count > 0 Then
                If Not IsDBNull(searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_is_accept_offer")) Then
                    If Not String.IsNullOrEmpty(searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_is_accept_offer").ToString) Then
                        Dim isAcceptOffer As String = searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_is_accept_offer").ToString
                        If isAcceptOffer = "N" Then
                            Me.rbNo.Checked = True
                        ElseIf isAcceptOffer = "Y" Then
                            Me.rbYes.Checked = True
                        End If
                    End If
                End If

                If Not IsDBNull(searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_reject_reason_index")) Then
                    If Not String.IsNullOrEmpty(searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_reject_reason_index").ToString) Then
                        InitNBMPolicyRejectReasonDropDownList()
                        Dim rejectReasonIndex As String = searchRetentionOfferCampaignRecordDataTable.Rows(0).Item("roc_reject_reason_index").ToString
                        Me.cbRejectReason.SelectedValue = rejectReasonIndex
                    End If
                End If
            End If

        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
    End Sub


    'FindCustomerid() - Find customerid by policy number
    Private Function FindCustomerid(ByVal strPolicyAccID As String) As String
        Dim strTemp As String

        sqlConn.Open()
        strTemp = ""
        Dim sqlCmd As New SqlCommand("Select Customerid From csw_poli_rel Where policyrelatecode = 'PH' and policyaccountid = '" & RTrim(strPolicyAccID) & "' ", sqlConn)
        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
        If sqlReader.Read() Then
            Try
                If Not sqlReader.IsDBNull(0) Then
                    strTemp = CStr(sqlReader.Item(0))
                End If
            Catch sqlEx As SqlException
                MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
            Catch ex As Exception
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Finally
                sqlReader.Close()
                sqlConn.Close()
            End Try
        End If
        Return strTemp
        sqlCmd.Dispose()

    End Function

    'InitDataset() - Fill the dataset and add relations on the tables
    Private Sub InitDataset()
        Dim dcParent(1) As DataColumn
        Dim dcChild(1) As DataColumn
        Dim dr As DataRow
        Dim drParent As DataRow
        Dim temp As String
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'Make connection to SQL server
        'sqlConn.ConnectionString = strCIWConn
        sqlConn.ConnectionString = strCIWConn
        'sqlConn.ConnectionString = "packet size=4096;user id=vantiveowner;data source=hksqldev1;persist security info=True;initial catalog=vantive;password=ownerdev"
        'strPolicy = "U9807970"
        'strCustID = "10352182"

        'Run SQL to retrieve data
        'daSrvEvtDet = New SqlDataAdapter("select * from ServiceEventDetail where PolicyAccountID = @policyid  order by EventInitialDateTime desc, EventStatusCode asc", sqlConn)
        'Place the pending case at top of the datagrid

        'VHIS start - Add sort order to EventStatusCodes
        'If strPolicy = "" Then
        '    strSQL = "Select (Case when t1.EventStatusCode='C' then '3' " & _
        '                     " when t1.EventStatusCode='P' then '1' " & _
        '                     " else '2' end) as 'Status', t1.*, ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end " & _
        '                     ", PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end " & _
        '                     ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, isnull(t8.name,'') sender_name " & _
        '                     ", PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end " & _
        '         "From ServiceEventDetail t1 " & _
        '         "left join PolicyAccount t2 on t1.PolicyAccountID=t2.PolicyAccountID " & _
        '         "left join PRODUCT t3 on t2.ProductID=t3.ProductiD " & _
        '         "left join GI_PRODUCT t4 on t2.ProductID=t4.ProductiD " & _
        '         "left join csw_event_category_code t5 on t1.EventCategoryCode=t5.cswecc_code " & _
        '         "left join csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " & _
        '         "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " & _
        '         "left join CSR t8 on t1.MasterCSRID=t8.CSRID " & _
        '         "Where t1.Customerid = " & strCustID & " " & _
        '    "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "
        'Else
        '    strSQL = "Select (Case when t1.EventStatusCode='C' then '3' " & _
        '                    " when t1.EventStatusCode='P' then '1' " & _
        '                    " else '2' end) as 'Status', t1.*,  ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end " & _
        '                    ", PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end " & _
        '                    ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, isnull(t8.name,'') sender_name " & _
        '                    ", PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end " & _
        '            "From ServiceEventDetail t1 " & _
        '            "left join PolicyAccount t2 on t1.PolicyAccountID=t2.PolicyAccountID " & _
        '            "left join PRODUCT t3 on t2.ProductID=t3.ProductiD " & _
        '            "left join GI_PRODUCT t4 on t2.ProductID=t4.ProductiD " & _
        '            "left join csw_event_category_code t5 on t1.EventCategoryCode=t5.cswecc_code " & _
        '            "left join csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " & _
        '            "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " & _
        '            "left join CSR t8 on t1.MasterCSRID=t8.CSRID " & _
        '            "Where t1.PolicyAccountID = '" & strPolicy & "' or  t1.PolicyAccountID = 'GL" & strPolicy & "' " & _
        '    "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "
        'End If

        'ITSR933 FG R3 Policy Number Change Start
        strSQL = "Select (Case when event.sort_order is null then star.sort_order else event.sort_order end) as 'Status', " &
                         "(Case when event.EventStatus is null then star.EventStatus else event.EventStatus end) as 'EventStatus', " &
                         "t1.*, ProductName=case when t2.companyid in ('EAA','ING','BMU') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end " &
                         ", PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end " &
                         ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, isnull(t8.name,'') sender_name " &
                         ", PolicyType=case when t2.companyid in ('EAA','ING','BMU') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end " &
             "From ServiceEventDetail t1 " &
             "left join " & serverPrefix & "ServiceEventDetailMCU_Extend e on t1.ServiceEventNumber = e.ServiceEventNumber " &
             "left join PolicyAccount t2 on t1.PolicyAccountID=t2.PolicyAccountID " &
             "left join PRODUCT t3 on t2.ProductID=t3.ProductiD " &
             "left join GI_PRODUCT t4 on t2.ProductID=t4.ProductiD " &
             "left join " & gcPOS & "vw_csw_event_category_code  t5 on t1.EventCategoryCode=t5.cswecc_code " &
             "left join " & gcPOS & "vw_csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " &
             "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " &
             "left join " & serverPrefix & "CSR t8 on t1.MasterCSRID=t8.CSRID " &
             "left join " & serverPrefix & "EventStatusCodes as event on event.EventStatusCode = t1.EventStatusCode and event.CategoryCode= t1.EventCategoryCode and event.EventTypeCode = t1.EventTypeCode " &
             "left join " & serverPrefix & "EventStatusCodes as star on star.EventStatusCode = t1.EventStatusCode and star.CategoryCode= '*' and star.EventTypeCode = '*' " '& _
        '"left join csw_policy_map map on t1.PolicyAccountID=cswpm_capsil_policy "

        If strPolicy = "" Then
            strSQL += "Where t1.Customerid = " & strCustID & " "
        Else
            'strSQL += "Where t1.PolicyAccountID = '" & strPolicy & "' or  t1.PolicyAccountID = 'GL" & strPolicy & "' " & " or cswpm_la_policy = '" & strPolicy & "'"
            strSQL += "left join (select cswpm_capsil_policy from csw_policy_map where cswpm_la_policy = '" & strPolicy & "') a on t1.PolicyAccountID=cswpm_capsil_policy Where (t1.PolicyAccountID = '" & strPolicy & "' or  t1.PolicyAccountID = 'GL" & strPolicy & "') "
        End If
        'ITSR933 FG R3 Policy Number Change End

        strSQL += "and e.ServiceEventNumber is null "
        strSQL += "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "
        'VHIS end - Add sort order to EventStatusCodes

        If strCustID = "" Then
            strCustID = FindCustomerid(strPolicy)
        End If
        daSrvEvtDet = New SqlDataAdapter(strSQL, sqlConn)
        daEvtCat = New SqlDataAdapter("Select * from " & gcPOS & "vw_csw_event_category_code  order by cswecc_desc", sqlConn)
        daEvtType = New SqlDataAdapter("Select * from ServiceEventTypeCodes order by SortOrder", sqlConn)
        'ITDCPI Complaint / Grievance option update start
        'daEvtTypeDet = New SqlDataAdapter("Select * from csw_event_typedtl_code order by cswetd_sort_order", sqlConn) 'Original setting
        daEvtTypeDet = New SqlDataAdapter("Select * from " & gcPOS & "vw_csw_event_typedtl_code where (Obsoleted<>'Y' or Obsoleted is null) order by cswetd_sort_order", sqlConn) 'New Setting read Obsoleted flag
        'ITDCPI Complaint / Grievance option update end
        daMedium = New SqlDataAdapter("Select * from EventSourceMediumCodes order by case when EventSourceMedium in ('Store','V-Chat') then 2 else 1 end, EventSourceMedium", sqlConn)
        daStatus = New SqlDataAdapter("Select * from " & serverPrefix & "EventStatusCodes", sqlConn)
        strSQL = "(Select '' as 'CSRID', '' as 'Name', null as 'CSRTypeCode', null as 'Description', " &
                 "null as 'ProductsSpecializedIn', null as 'LicensesHeld', " &
                 "null as 'LicenseEffectiveDate', null as 'CSRUnitCode', null as 'SupervisorsCSRID', null as 'timestamp', null as 'Cname', null as 'csrid_400', null as 'Active' " &
                 "From " & serverPrefix & "CSR) union (Select * From " & serverPrefix & "CSR) order by Name"
        daCsr = New SqlDataAdapter(strSQL, sqlConn)
        daInitiator = New SqlDataAdapter("Select * from EventSourceInitiatorCodes order by EventSourceInitiator", sqlConn)
        daCustomer = New SqlDataAdapter("Select * from customer where customerid = '" & strCustID & "' ", sqlConn)

        Try
            daSrvEvtDet.Fill(dsSrvLog, "ServiceEventDetail")
            daEvtCat.Fill(dsSrvLog, "csw_event_category_code")
            daEvtType.Fill(dsSrvLog, "ServiceEventTypeCodes")
            daEvtTypeDet.Fill(dsSrvLog, "csw_event_typedtl_code")
            daMedium.Fill(dsSrvLog, "EventSourceMediumCodes")
            daStatus.Fill(dsSrvLog, "EventStatusCodes")
            daCsr.Fill(dsSrvLog, "Csr")
            daInitiator.Fill(dsSrvLog, "EventSourceInitiatorCodes")
            daCustomer.Fill(dsSrvLog, "Customer")

        Catch e As Exception
            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
        End Try

        'Add relations to the datatables in dataset
        'dsSrvLog.Relations.Add("EvtCat_SrvEvt", dsSrvLog.Tables("csw_event_category_code").Columns("cswecc_code"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode"), False)
        dsSrvLog.Relations.Add("EvtMed_SrvEvt", dsSrvLog.Tables("EventSourceMediumCodes").Columns("EventSourceMediumCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventSourceMediumCode"), False)

        'VHIS delete
        'dsSrvLog.Relations.Add("EvtStat_SrvEvt", dsSrvLog.Tables("EventStatusCodes").Columns("EventStatusCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventStatusCode"), False)

        'dsSrvLog.Relations.Add("Csr_SrvEvt_Master", dsSrvLog.Tables("Csr").Columns("CSRID"), dsSrvLog.Tables("ServiceEventDetail").Columns("MasterCSRID"), False)
        dsSrvLog.Relations.Add("Csr_SrvEvt_Secondary", dsSrvLog.Tables("Csr").Columns("CSRID"), dsSrvLog.Tables("ServiceEventDetail").Columns("SecondaryCSRID"), False)
        dsSrvLog.Relations.Add("EvtInit_SrvEvt", dsSrvLog.Tables("EventSourceInitiatorCodes").Columns("EventSourceInitiatorCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventSourceInitiatorCode"), False)

        'VHIS delete - multiple to one
        'dcParent(0) = dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventCategoryCode")
        'dcChild(0) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode")
        'dcParent(1) = dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventTypeCode")
        'dcChild(1) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeCode")
        'dsSrvLog.Relations.Add("EvtType_SrvEvt", dcParent, dcChild, False)

        ReDim dcParent(2)
        ReDim dcChild(2)
        dcParent(0) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_category_code")
        dcChild(0) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode")
        dcParent(1) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_type_code")
        dcChild(1) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeCode")
        dcParent(2) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_typedtl_code")
        dcChild(2) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeDetailCode")
        dsSrvLog.Relations.Add("EvtTypeDet_SrvEvt", dcParent, dcChild, False)

        'Add dummy field
        With dsSrvLog.Tables("ServiceEventDetail")
            '.Columns.Add("cswecc_desc", GetType(String))
            '.Columns.Add("EventTypeDesc", GetType(String))
            '.Columns.Add("csw_event_typedtl_desc", GetType(String))
            '.Columns.Add("sender_name", GetType(String))

            'VHIS delete
            '.Columns.Add("EventStatus", GetType(String))

            .Columns.Add("EventSourceMedium", GetType(String))
        End With

        'Create layout of datagrid
        Dim tsSrvLog As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        'A hidden field that contains the ServiceEventNumber
        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "ServiceEventNumber"
        cs.HeaderText = "Service Event Number"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "EventInitialDateTime"
        cs.HeaderText = "Initial Date"
        cs.Format = gDateTimeFormat
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("Csr_SrvEvt_Master", dsSrvLog.Tables("Csr").Columns("Name"))
        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "sender_name"
        'cs.MappingName = "MasterCSRID"
        cs.HeaderText = "Sender"
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)

        'VHIS modify
        'cs = New JoinTextBoxColumn("EvtStat_SrvEvt", dsSrvLog.Tables("EventStatusCodes").Columns("EventStatus"))
        cs = New DataGridTextBoxColumn
        cs.Width = 80
        'cs.MappingName = "EventStatus"
        'VHIS modify
        'cs.MappingName = "EventStatusCode"
        cs.MappingName = "EventStatus"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("EvtCat_SrvEvt", dsSrvLog.Tables("csw_event_category_code").Columns("cswecc_desc"))
        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "cswecc_desc"
        'cs.MappingName = "EventCategoryCode"
        cs.HeaderText = "Event Category"
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("EvtType_SrvEvt", dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventTypeDesc"))
        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EventTypeDesc"
        'cs.MappingName = "EventTypeCode"
        cs.HeaderText = "Event Type"
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("EvtTypeDet_SrvEvt", dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_typedtl_desc"))
        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "csw_event_typedtl_desc"
        'cs.MappingName = "EventTypeDetailCode"
        cs.HeaderText = "Event Type Detail"
        cs.NullText = gNULLText
        tsSrvLog.GridColumnStyles.Add(cs)


        If strPolicy = "" Then
            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "PolicyAccountNo"
            cs.HeaderText = "Policy No"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 150
            cs.MappingName = "ProductName"
            cs.HeaderText = "Product Name"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

        End If

        cs = New JoinTextBoxColumn("EvtMed_SrvEvt", dsSrvLog.Tables("EventSourceMediumCodes").Columns("EventSourceMedium"))
        cs.Width = 0
        cs.MappingName = "EventSourceMedium"
        cs.HeaderText = "Event Medium"
        tsSrvLog.GridColumnStyles.Add(cs)

        'Map the table style to the grid
        tsSrvLog.MappingName = "ServiceEventDetail"
        dgSrvLog.TableStyles.Add(tsSrvLog)

        dgSrvLog.DataSource = dsSrvLog.Tables("ServiceEventDetail")
        'dgSrvLog.SetDataBinding(dsSrvLog, "ServiceEventDetail")
        'dgSrvLog.SetDataBinding(dsSrvLog.Tables("ServiceEventDetail"), "")
        'dgSrvLog.DataSource = dsSrvLog
        'dgSrvLog.DataMember = "ServiceEventDetail"
        'da1 = New SqlDataAdapter("select * from ServiceEventDetail where PolicyAccountID = '" & strPolicy & "'", sqlConn)
        'da1.Fill(dt1)
        'dg1.DataSource = dt1

        'AddHandler dsSrvLog.Tables("ServiceEventDetail").ColumnChanging, AddressOf ColumnChanging
        'AddHandler dsSrvLog.Tables("ServiceEventDetail").RowChanging, AddressOf RowChanging

        'oliver 2024-4-24 udpated for vantive table relocate to CRSDB
        Dim dtPiAuth As DataTable = New DataTable
        If strPolicy IsNot Nothing AndAlso strPolicy.Length > 0 Then
            dtPiAuth = getPiAuth(strPolicy)
        End If
        If dtPiAuth.Rows.Count > 0 Then
            dtPiAuth.Columns("CustomerID").ColumnName = "PI ID"
            dtPiAuth.Columns("EmailAddr").ColumnName = "Email"
            dtPiAuth.Columns("LastUpdateDate").ColumnName = "Auth Date"
            dtPiAuth.Columns("Enable").ColumnName = "Auth"
            dgvAuthPi.DataSource = dtPiAuth
            dgvAuthPi.Columns("Auth Date").DefaultCellStyle.Format = "MM/dd/yyyy"
        End If

    End Sub

    'Private Sub ColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    '    btnCancel.Enabled = True
    '    btnSave.Enabled = True
    'End Sub

    'Private Sub RowChanging(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs)
    '    MsgBox("Row Changing")
    'End Sub

    'InitForm() - Fill the contents of controls in the form
    Private Sub InitForm()
        Dim b As Binding

        'Fill the Medium Combo Box
        cbMedium.DataSource = dsSrvLog.Tables("EventSourceMediumCodes")
        cbMedium.DisplayMember = "EventSourceMedium"
        cbMedium.ValueMember = "EventSourceMediumCode"
        'Fill the Event Category, Event Detail and Event Type Detail Combo Box
        cbEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
        cbEventCat.DisplayMember = "cswecc_desc"
        cbEventCat.ValueMember = "cswecc_code"
        cbEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes")
        cbEventDetail.DisplayMember = "EventTypeDesc"
        cbEventDetail.ValueMember = "EventTypeCode"
        cbEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code")
        cbEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cbEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        'Fill the Status Combo Box
        cbStatus.DataSource = dsSrvLog.Tables("EventStatusCodes")
        cbStatus.DisplayMember = "EventStatus"
        cbStatus.ValueMember = "EventStatusCode"
        'Fill the Receiver Comb Box, but it will only be enabled in Handoff cases
        cbReceiver.DataSource = dsSrvLog.Tables("csr")
        cbReceiver.DisplayMember = "Name"
        cbReceiver.ValueMember = "CSRID"

        'Transfer to AES
        b = New Binding("Checked", dsSrvLog.Tables("ServiceEventDetail"), "IsTransferToAES")
        AddHandler b.Format, AddressOf YNtoTF
        AddHandler b.Parse, AddressOf TFtoYN
        chkAES.DataBindings.Add(b)

        'Fill the Initiator and Initial Date Combo Box
        cbInitiator.DataSource = dsSrvLog.Tables("EventSourceInitiatorCodes")
        cbInitiator.DisplayMember = "EventSourceInitiator"
        cbInitiator.ValueMember = "EventSourceInitiatorCode"
        dtInitial.Format = DateTimePickerFormat.Custom
        dtInitial.CustomFormat = gDateTimeFormat

        ''Enable/Disable Buttons
        'btnNew.Enabled = True
        'If dsSrvLog.Tables("ServiceEventDetail").Rows.Count = 0 Then
        '    btnSave.Enabled = False
        '    btnCancel.Enabled = False
        'End If

        'Add databindings to hidden text fields
        hidSrvEvtNo.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "ServiceEventNumber")
        hidPolicy.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "PolicyAccountID")
        hidCustID.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "CustomerID")
        hidSender.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "MasterCSRID")

        'Bind Comboboxes to ServiceEventDetail
        cbMedium.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventSourceMediumCode")
        cbStatus.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventStatusCode")
        cbInitiator.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventSourceInitiatorCode")
        cbEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventCategoryCode")
        cbEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventTypeCode")
        cbEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "EventTypeDetailCode")
        cbReceiver.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "SecondaryCSRID")
        'dtInitial.DataBindings.Add("Value", dsSrvLog.Tables("ServiceEventDetail"), "EventInitialDateTime")
        b = New Binding("Value", dsSrvLog.Tables("ServiceEventDetail"), "EventInitialDateTime")
        AddHandler b.Format, AddressOf DTFormatter
        AddHandler b.Parse, AddressOf DTParser
        dtInitial.DataBindings.Add(b)

        'Alert text
        txtPolicyAlert.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "AlertNotes")
        txtReminder.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "ReminderNotes")
        txtNotes.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "EventNotes")
        b = New Binding("Checked", dsSrvLog.Tables("ServiceEventDetail"), "IsPolicyAlert")
        AddHandler b.Format, AddressOf YNtoTF
        AddHandler b.Parse, AddressOf TFtoYN
        chkPolicyAlert.DataBindings.Add(b)

        'ID Verify
        b = New Binding("Checked", dsSrvLog.Tables("ServiceEventDetail"), "IsIdVerify")
        AddHandler b.Format, AddressOf YNtoTF
        AddHandler b.Parse, AddressOf TFtoYN
        chkIdVerify.DataBindings.Add(b)

        'Allow user to input policy ID in customer service log 
        If strPolicy <> "" Then
            gboPolicy.Visible = False
            dgSrvLog.Width = 800
        Else
            gboPolicy.Visible = True
            txtPolicyNo.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "PolicyAccountNo")
            dgSrvLog.Width = 887

        End If

        'Display Customer groupbox when entry point from customer search
        If blnIsCustLevel Then
            gboCustomer.Visible = True
            txtCustomerID.Text = strCustID
            txtCustomerName.Text = dsSrvLog.Tables("Customer").Rows(0).Item("NameSuffix").ToString() & " " & dsSrvLog.Tables("Customer").Rows(0).Item("FirstName").ToString()
        Else
            gboCustomer.Visible = False
        End If

        b = New Binding("Value", dsSrvLog.Tables("ServiceEventDetail"), "ReminderDate")
        AddHandler b.Format, AddressOf DTFormatter
        AddHandler b.Parse, AddressOf DTParser
        dtReminder.DataBindings.Add(b)
        dtReminder.Format = DateTimePickerFormat.Custom
        dtReminder.CustomFormat = gDateFormat
        'Check status
        CheckStatus()
        strNewFlag = "N"


        'Me.lbl_InforceDate.Text = ""
        Me.lbl_PostCallStatus.Text = "loading.."
        'Me.lbl_PostCallCount.Text = ""

        'Me.rad_NVCWelcomeCall.Checked = False
        'Me.rad_VCPostSalesCall.Checked = False
        'Me.rad_ILASPostSalseCall.Checked = False
        'Me.rad_SuitabilityMisMatch.Checked = False
        Me.lbl_InforceDate.Visible = False
        'Me.lbl_PostCallStatus.Visible = False
        Me.lbl_PostCallCount.Visible = False

        Me.rad_NVCWelcomeCall.Visible = False
        Me.rad_VCPostSalesCall.Visible = False
        Me.rad_ILASPostSalseCall.Visible = False
        Me.rad_SuitabilityMisMatch.Visible = False

        Me.Label5.Visible = False
        Me.Label15.Visible = False
        Me.Label17.Visible = False

        BackgroundWorker1.RunWorkerAsync()
        'getPostSalesCallInfo()

        AddHandler daSrvEvtDet.RowUpdating, AddressOf RowUpdating

        If Mid(strUPSMenuCtrl, 94, 1) = "1" Then
            btnDisablePI.Enabled = True
            btnAuthToPi.Enabled = True
        Else
            btnDisablePI.Enabled = False
            btnAuthToPi.Enabled = False
        End If
    End Sub

    'Concurrency checking before save - 
    'Compare the last update date (UpdateDateTime field) of disconnected dataset (dsSrvLog) and SQL server,
    'If SQL server date is later than dataset date, that means someone else has updated the record between 
    'loading and saving, concurrency error occurs
    Private Sub RowUpdating(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlRowUpdatingEventArgs)
        Dim tempDt As DataTable
        Dim strsql As String
        Dim strSrvEventNo As String
        Dim dtSQLUpd As DateTime
        Dim dtDsUpd As DateTime
        Dim strPrompt As String

        'sqlConn.ConnectionString = strCIWConn
        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()

        tempDt = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Modified)
        If Not IsNothing(tempDt) Then
            strSrvEventNo = tempDt.Rows(0).Item("ServiceEventNumber")
            strsql = "Select UpdateDatetime from ServiceEventDetail where ServiceEventNumber = " & strSrvEventNo
            Dim sqlCmd As New SqlCommand(strsql, sqlConn)
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            If sqlReader.Read() Then
                Try
                    If Not sqlReader.IsDBNull(0) Then
                        dtSQLUpd = sqlReader.GetDateTime(0)
                    End If
                Catch sqlEx As SqlException
                    MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
                Catch ex As Exception
                    MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
                Finally
                    sqlReader.Close()
                    sqlConn.Close()
                End Try
            End If
            dtDsUpd = tempDt.Rows(0).Item("updatedatetime")
            If DateTime.Compare(dtSQLUpd, dtDsUpd) Then
                strPrompt = "The record is updated by another user on " & dtSQLUpd.ToLongDateString & " " & dtSQLUpd.ToLongTimeString &
                            "Please reload the page again"
                MsgBox(strPrompt, MsgBoxStyle.Critical, "Concurrency Error")
            End If
        End If

    End Sub

#Region " Combo Box Events "

    Private Sub cbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStatus.SelectedIndexChanged
        If cbStatus.SelectedValue Is Nothing Then
            Exit Sub
        End If
        If TypeOf (cbStatus.SelectedValue) Is String Then
            If cbStatus.SelectedValue = "C" Or cbStatus.SelectedValue = "P" Then
                cbReceiver.SelectedValue = ""
                cbReceiver.Enabled = False
                lbReceiver.Enabled = False
                cbReceiver.BackColor = System.Drawing.Color.LightGray
            Else
                cbReceiver.Enabled = True
                lbReceiver.Enabled = True
                cbReceiver.BackColor = System.Drawing.Color.White
            End If
        End If
    End Sub

    Private Sub cbEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventCat.SelectedIndexChanged
        If TypeOf (cbEventCat.SelectedValue) Is String Then
            SetType()
            SetTypeDetail()
        End If
    End Sub

    Private Sub cbEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventDetail.SelectedIndexChanged
        If TypeOf (cbEventCat.SelectedValue) Is String And TypeOf (cbEventDetail.SelectedValue) Is String Then
            SetTypeDetail()
            'VHIS start
            SetStatus()
            'VHIS end
        End If
    End Sub

    Private Sub SetTypeDetail()
        Dim strCat As String
        Dim strType As String
        strCat = cbEventCat.SelectedValue.ToString
        If Not cbEventDetail.SelectedValue Is Nothing Then
            strType = cbEventDetail.SelectedValue.ToString
        End If
        dsSrvLog.Tables("csw_event_typedtl_code").DefaultView.RowFilter = "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'"
        dsSrvLog.Tables("csw_event_typedtl_code").DefaultView.Sort = "cswetd_sort_order"
        If cbEventTypeDetail.Items.Count > 0 Then
            cbEventTypeDetail.SelectedIndex = -1
            cbEventTypeDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub SetType()
        Dim strCat As String
        strCat = cbEventCat.SelectedValue.ToString
        dsSrvLog.Tables("ServiceEventTypeCodes").DefaultView.RowFilter = "EventCategoryCode = '" & strCat & "'"
        dsSrvLog.Tables("ServiceEventTypeCodes").DefaultView.Sort = "SortOrder"
        If cbEventDetail.Items.Count > 0 Then
            cbEventDetail.SelectedIndex = -1
            cbEventDetail.SelectedIndex = 0
        End If
    End Sub

    'VHIS start - Add SetStatus
    Private Sub SetStatus()
        Dim strCat As String
        Dim strType As String
        strCat = cbEventCat.SelectedValue.ToString
        If Not cbEventDetail.SelectedValue Is Nothing Then
            strType = cbEventDetail.SelectedValue.ToString
        End If

        Dim dtEventStatus As DataTable
        dtEventStatus = dsSrvLog.Tables("EventStatusCodes").Copy()
        dtEventStatus.DefaultView.RowFilter = "CategoryCode = '" & strCat & "' and EventTypeCode = '" & strType & "'"

        If dtEventStatus.DefaultView.Count = 0 Then
            dtEventStatus.DefaultView.RowFilter = "CategoryCode = '*' and EventTypeCode = '*'"
            If dtEventStatus.DefaultView.Count > 0 Then
                dsSrvLog.Tables("EventStatusCodes").DefaultView.RowFilter = "CategoryCode = '*' and EventTypeCode = '*'"
            Else
                ' no filter
            End If
        Else
            dsSrvLog.Tables("EventStatusCodes").DefaultView.RowFilter = "CategoryCode = '" & strCat & "' and EventTypeCode = '" & strType & "'"
        End If

        dsSrvLog.Tables("EventStatusCodes").DefaultView.Sort = "sort_order"
    End Sub
    'VHIS end - Add SetStatus

    Private Sub dtReminder_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtReminder.ValueChanged
        If DateTime.Equals(dtReminder.Value, #1/1/1800#) Then
            chkReminder.Checked = False
            dtReminder.Enabled = False
        Else
            chkReminder.Checked = True
            dtReminder.Enabled = True
        End If
    End Sub

#End Region

#Region " Button Click Events "

    'btnNew_Click() - Create a new row in datatable and bind the controls to it
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim dr As DataRow

        'Check if there is any unsaved data        
        strNewFlag = "Y"
        dr = dsSrvLog.Tables("ServiceEventDetail").NewRow()
        dr.Item("EventSourceMediumCode") = "PC"
        dr.Item("EventCategoryCode") = "20"
        dr.Item("EventTypeCode") = "15"
        dr.Item("EventTypeDetailCode") = "10"
        dr.Item("CustomerID") = strCustID
        dr.Item("PolicyAccountID") = strPolicy
        dr.Item("MasterCSRID") = gsUser
        dr.Item("EventSourceInitiatorCode") = "CTR"
        dr.Item("EventStatusCode") = "C"
        dr.Item("EventInitialDateTime") = DateTime.Now
        dr.Item("EventNotes") = " "
        dr.Item("ReminderNotes") = " "
        dr.Item("AlertNotes") = " "
        dr.Item("ReminderDate") = #1/1/1800#
        dr.Item("EventAssignDateTime") = System.DBNull.Value
        dr.Item("EventCompletionDateTime") = System.DBNull.Value
        dr.Item("EventCloseoutDateTime") = System.DBNull.Value
        dr.Item("EventCloseoutCode") = ""       ' FCR

        'C001 - Start
        dr.Item("MCV") = ""
        'C001 - End
        Try
            dgSrvLog.AllowSorting = False
            dsSrvLog.Tables("ServiceEventDetail").Rows.Add(dr)

            Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).Position = dsSrvLog.Tables("ServiceEventDetail").Rows.Count - 1
            dgSrvLog.AllowSorting = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Add Error")
        End Try

        'Me.BindingContext(dsSrvLog, "ServiceEventDetail").Position = dsSrvLog.Tables("ServiceEventDetail").Rows.Count - 1
        cbMedium.Enabled = True
        cbEventCat.Enabled = True
        cbEventDetail.Enabled = True
        cbEventTypeDetail.Enabled = True
        cbInitiator.Enabled = True
        cbStatus.Enabled = True
        dtInitial.Enabled = True
        dtReminder.Enabled = False
        chkReminder.Checked = False
        chkReminder.Enabled = True
        txtReminder.Enabled = True
        chkPolicyAlert.Enabled = True
        chkPolicyAlert.Checked = False
        txtPolicyAlert.Enabled = True
        txtNotes.Enabled = True
        chkIdVerify.Enabled = True
        chkIdVerify.Checked = False

        btnNew.Enabled = False
        btnSave.Enabled = True
        btnSaveC.Enabled = True
        btnCancel.Enabled = True

        chkFCR.Enabled = True       'FCR
        chkACC.Enabled = True       ' Agent call customer hotline

        txtPolicyNo.Enabled = True

        rbLife.Checked = True
        'C001 - Start
        chkMCV.Enabled = True
        'C001 - End

        'ITDCPI Default Medium and initiator start
        ServiceLogBL.GetSerlogPreference(cbMedium, cbInitiator)
        'ITDCPI Default Medium and initiator end
        cbStatus.SelectedValue = "C"
        'dr.Item("EventStatusCode") = "C"

        'default enable for new Service Log
        chkFCR.Checked = True
    End Sub

    'btnCancel_Click() - Cancel the modifications since last acceptchanges() 
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelServiceLog()
    End Sub

    'btnSave_Click() - Synchronize the SQL table with modifications in dataset
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        'Post Sales Call - Start
        If cbEventCat.Text.Trim = "Courtesy Call - Non-vulnerable customer" AndAlso Me.rad_NVCWelcomeCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text = "Courtesy Call - Vulnerable customer" AndAlso Me.rad_VCPostSalesCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text = "ILAS - Post-Sales Call" AndAlso Me.rad_ILASPostSalseCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text.Trim = "ILAS - Pre-Approval Call" AndAlso (Me.rad_NVCWelcomeCall.Checked = True Or rad_VCPostSalesCall.Checked = True Or Me.rad_ILASPostSalseCall.Checked = True Or Me.rad_SuitabilityMisMatch.Checked = True) Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text = "Courtesy Call ¡V Suitability mismatch" AndAlso Me.rad_SuitabilityMisMatch.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If
        'Post Sales Call - End


        'Save the current row
        bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
        'iPrevPosition = bm.Position
        Dim dr As DataRow = CType(bm.Current, DataRowView).Row()
        SaveServiceLog(dr)
        'bm.Position = iPrevPosition
    End Sub

#End Region

    'CheckStatus() - Enable/disable the input controls according to the status of service event
    Private Sub CheckStatus()
        Dim strStatus As String
        Dim strAppAes As String

        If dsSrvLog.Tables("ServiceEventDetail").Rows.Count > 0 Then
            'strStatus = dsSrvLog.Tables("ServiceEventDetail").Rows(dgSrvLog.CurrentRowIndex).Item("EventStatusCode")
            bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
            Dim dr As DataRow = CType(bm.Current, DataRowView).Row()
            strStatus = dr.Item("EventStatusCode")

            'Enable/disable comboboxes
            If strStatus = "C" Then
                'Record is non-editable when status is "Completed"
                cbMedium.Enabled = False
                cbEventCat.Enabled = False
                cbEventDetail.Enabled = False
                cbEventTypeDetail.Enabled = False
                cbStatus.Enabled = False
                cbInitiator.Enabled = False
                dtInitial.Enabled = False
                'chkFCR.Enabled = False  'FCR
                chkFCR.Enabled = True  'FCR
                chkACC.Enabled = True  'ACC
                'C001 - Start
                chkMCV.Enabled = True
                'C001 - End
            Else
                cbMedium.Enabled = True
                cbEventCat.Enabled = True
                cbEventDetail.Enabled = True
                cbEventTypeDetail.Enabled = True
                cbStatus.Enabled = True
                cbInitiator.Enabled = True
                dtInitial.Enabled = True
            End If

            'Status=Handoff - enable receiver
            If strStatus = "C" Or strStatus = "P" Then
                cbReceiver.Enabled = False
                lbReceiver.Enabled = False
                cbReceiver.BackColor = System.Drawing.Color.LightGray
            Else
                cbReceiver.Enabled = True
                lbReceiver.Enabled = True
                cbReceiver.BackColor = System.Drawing.Color.White
            End If

            'Enable/disable chkAes
            If IsDBNull(dr.Item("IsAppearedInAES")) Then
                chkAES.Enabled = False
            ElseIf dr.Item("IsAppearedInAES") = "Y" Then
                chkAES.Enabled = True
            Else
                chkAES.Enabled = False
            End If

            btnNew.Enabled = True
            btnCancel.Enabled = True
            btnSave.Enabled = True
            btnSaveC.Enabled = True

            ' **** FCR start ****
            'chkFCR.Enabled = False
            chkFCR.Enabled = True
            If Not IsDBNull(dr.Item("EventCloseoutCode")) AndAlso dr.Item("EventCloseoutCode") = "Y" Then
                chkFCR.Checked = True
            Else
                chkFCR.Checked = False
            End If
            ' **** FCR end ****

            ' **** ACC start ****
            chkACC.Enabled = True
            If Not IsDBNull(dr.Item("caseno")) AndAlso dr.Item("caseno") = "Y" Then
                chkACC.Checked = True
            Else
                chkACC.Checked = False
            End If
            ' **** ACC end ****

            If IsDBNull(dr.Item("PolicyType")) Then
                rbLife.Checked = True
            ElseIf dr.Item("PolicyType") = "LIFE" Then
                rbLife.Checked = True
            ElseIf dr.Item("PolicyType") = "EB" Then
                rbEB.Checked = True
            ElseIf dr.Item("PolicyType") = "GI" Then
                rbGI.Checked = True
            End If
            'C001 - Start
            chkMCV.Enabled = True
            If (Not IsDBNull(dr.Item("MCV"))) AndAlso dr.Item("MCV") = "Y" Then
                chkMCV.Checked = True
            Else
                chkMCV.Checked = False
            End If
            'C001 - End
        Else
            cbMedium.SelectedValue = ""
            cbMedium.Enabled = False
            cbEventCat.Enabled = False
            cbEventDetail.Enabled = False
            cbEventTypeDetail.Enabled = False
            cbStatus.Enabled = False
            cbInitiator.Enabled = False
            dtInitial.Enabled = False
            cbReceiver.Enabled = False
            lbReceiver.Enabled = False
            cbReceiver.BackColor = System.Drawing.Color.LightGray
            txtPolicyAlert.Enabled = False
            chkPolicyAlert.Checked = False
            chkPolicyAlert.Enabled = False
            chkReminder.Checked = False
            chkReminder.Enabled = False
            dtReminder.Enabled = False
            txtReminder.Enabled = False
            txtNotes.Enabled = False
            chkIdVerify.Checked = False
            chkIdVerify.Enabled = False

            btnNew.Enabled = True
            btnSave.Enabled = False
            btnSaveC.Enabled = False
            btnCancel.Enabled = False
            chkFCR.Enabled = False      ' FCR
            chkACC.Enabled = False      ' ACC

            txtPolicyNo.Enabled = False

            rbLife.Checked = True
            'C001 - Start
            chkMCV.Enabled = False
            'C001 - End
        End If

    End Sub

    'CheckTransferAES() - Check whether the case should be transferred to AES
    'Called while the first save of the service log
    Private Function CheckTransferAES() As Boolean
        Dim drv As DataRowView
        Dim dr As DataRow
        Dim strInitAES As String
        Dim strTypeDtlAES As String
        Dim strMediumAES As String
        Dim strStatusAES As String

        drv = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).Current
        dr = drv.Row.GetParentRow("EvtInit_SrvEvt")
        strInitAES = dr.Item("IsDefaultTransferAES").ToString
        dr = drv.Row.GetParentRow("EvtTypeDet_SrvEvt")
        'Check whether EventTypeDetail is blank
        If IsNothing(dr) Then
            strTypeDtlAES = "N"
        Else
            strTypeDtlAES = dr.Item("cswetd_def_tran_AES").ToString
        End If
        dr = drv.Row.GetParentRow("EvtMed_SrvEvt")
        strMediumAES = dr.Item("IsDefaultTransferAES").ToString

        'VHIS start - modify strStatusAES
        'dr = drv.Row.GetParentRow("EvtStat_SrvEvt")
        'strStatusAES = dr.Item("IsDefaultTransferAES").ToString

        Dim strCat As String
        Dim strType As String
        strCat = cbEventCat.SelectedValue.ToString
        If Not cbEventDetail.SelectedValue Is Nothing Then
            strType = cbEventDetail.SelectedValue.ToString()
        End If
        Dim drArray1 As DataRow()
        Dim drEventTypeDetail As DataRow
        drArray1 = dsSrvLog.Tables("EventStatusCodes").Select("EventStatusCode='" + cbStatus.SelectedValue + "' and CategoryCode = '" & strCat & "' and EventTypeCode = '" & strType & "'")
        If drArray1.Length() = 1 Then
            drEventTypeDetail = drArray1(0)
            strStatusAES = drEventTypeDetail("IsDefaultTransferAES")
        Else
            Dim drArray2 As DataRow()
            drArray2 = dsSrvLog.Tables("EventStatusCodes").Select("EventStatusCode='" + cbStatus.SelectedValue + "'")
            drEventTypeDetail = drArray2(0)
            strStatusAES = drEventTypeDetail("IsDefaultTransferAES")
        End If
        'VHIS end - modify strStatusAES

        'CheckTransferAES = true if ALL the 3 fields are "Y"
        If strInitAES = "Y" And strTypeDtlAES = "Y" And strMediumAES = "Y" And strStatusAES = "Y" Then
            CheckTransferAES = True
        Else
            CheckTransferAES = False
        End If
    End Function

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Sub CheckNBMPolicyRadioButton(ByVal policyNo As String, ByVal serviceEventNumber As String, ByVal customerId As String)
        Try

            Dim isAcceptOffer As String = IIf(Me.rbYes.Checked, "Y", "N")
            Dim rejectReasonIndex As String = IIf(Me.cbRejectReason.Enabled, CStr(Me.cbRejectReason.SelectedValue), "0")

            If Me.rbYes.Checked Then

                Dim effectiveDate As String = IIf(Not String.IsNullOrEmpty(Me.txtEffectiveDate.Text), ParseDateTimeFormat(Me.txtEffectiveDate.Text), "")
                Dim newCampaignCode As String = ""
                Dim originalCampaignCode As String = ""
                If dataTableReturnRetentionCampaignEnquiry IsNot Nothing AndAlso dataTableReturnRetentionCampaignEnquiry.Rows.Count > 0 Then
                    newCampaignCode = IIf(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignCode")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignCode").ToString(), "")
                    originalCampaignCode = IIf(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CurrentCampaignCode")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CurrentCampaignCode").ToString(), "")
                End If
                Dim description As String = "Campaign Code: " & newCampaignCode & ", Effective Date: " & effectiveDate & ", Discount description: " & Me.txtCampaignDetails.Text
                SaveNBMPolicy(policyNo, effectiveDate, customerId, serviceEventNumber, isAcceptOffer, rejectReasonIndex, newCampaignCode, description, originalCampaignCode)

            ElseIf Me.rbNo.Checked Then
                InsertRetentionOfferCampaignRecord(serviceEventNumber, isAcceptOffer, rejectReasonIndex)
            End If

        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try

    End Sub

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Sub SaveNBMPolicy(ByVal policyNo As String, ByVal effectiveDate As String, ByVal customerId As String, ByVal serviceEventNumber As String, ByVal isAcceptOffer As String, ByVal rejectReasonIndex As String, ByVal newCampaignCode As String, ByVal description As String, ByVal originalCampaignCode As String)
        Try
            Dim wspos As POSWS.POSWS = POSWS_HK()
            Dim transStatus As String = "ERROR"
            Dim isUpdatedRetentionCampaign As Boolean = RetentionCampaignUpdate(wspos, policyNo, If(Not String.IsNullOrEmpty(effectiveDate), CDec(effectiveDate), 0))
            If isUpdatedRetentionCampaign Then
                Dim isInsertRetentionOfferCampaignRecord As Boolean = InsertRetentionOfferCampaignRecord(serviceEventNumber, isAcceptOffer, rejectReasonIndex)
                If Not isInsertRetentionOfferCampaignRecord Then
                    description += ", Error: CRSAPI INSERT_RETENTION_OFFER_CAMPAIGN_RECORD Return False"
                Else
                    transStatus = "COMPLETED"
                End If
            Else
                description += ", Error:  RetentionCampaignUpdate WebService Return False"
            End If

            Dim isSavePolicyNotes As Boolean = SavePolicyNotes(wspos, policyNo, customerId, "RTC", "", Nothing, gsUser, Date.Now, DateTime.MinValue, "", description, Nothing)
            If Not isSavePolicyNotes Then
                description += ", Error:  SavePolicyNote WebService Return False"
            End If

            Dim effectiveDateResult As DateTime = #1/1/1900#
            If Not String.IsNullOrEmpty(effectiveDate) Then
                If DateTime.TryParseExact(effectiveDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, effectiveDateResult) Then
                End If
            End If
            SaveTransLogForRetention(wspos, policyNo, transStatus, serviceEventNumber, "Premium Discount – Retention Offer", effectiveDateResult, newCampaignCode, description, originalCampaignCode)
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
    End Sub

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function RetentionCampaignUpdate(ByVal wspos As POSWS.POSWS, ByVal policyNo As String, ByVal effectiveDate As Decimal) As Boolean
        Dim isUpdated As Boolean = False
        Dim strErrRetentionCampaignUpdate As String = ""

        Try
            isUpdated = wspos.RetentionCampaignUpdate(policyNo, "01", "01", "00", effectiveDate, strErrRetentionCampaignUpdate)
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message & Environment.NewLine & "RetentionCampaignUpdate Return StrErr:" & strErrRetentionCampaignUpdate)
        End Try

        Return isUpdated
    End Function

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function InsertRetentionOfferCampaignRecord(ByVal serviceEventNumber As String, ByVal isAcceptOffer As String, ByVal rejectReasonIndex As String) As Boolean
        Dim isInserted As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "INSERT_RETENTION_OFFER_CAMPAIGN_RECORD",
                                New Dictionary(Of String, String) From {
                                {"roc_service_event_number", serviceEventNumber},
                                {"roc_is_accept_offer", isAcceptOffer},
                                {"roc_reject_reason_index", rejectReasonIndex},
                                {"roc_create_user", gsUser}
                                })
            isInserted = True
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
        Return isInserted
    End Function

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function SaveTransLogForRetention(ByVal wspos As POSWS.POSWS, ByVal policyNo As String, ByVal transStatus As String, ByVal transNo As Long, ByVal type As String, ByVal effectiveDate As Date, ByVal newCampaignCode As String, ByVal description As String, ByVal originalCampaignCode As String) As Boolean
        Dim isSaved As Boolean = False
        Dim strErrSaveTransLogForRetention As String = ""

        Try
            isSaved = wspos.SaveTransLogForRetention(policyNo, Date.Now, gsUser, transStatus, transNo, type, Date.Now, effectiveDate, newCampaignCode, description, originalCampaignCode, strErrSaveTransLogForRetention)
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message & Environment.NewLine & "SaveTransLogForRetention Return StrErr:" & strErrSaveTransLogForRetention)
        End Try

        Return isSaved
    End Function

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function SavePolicyNotes(ByVal wspos As POSWS.POSWS, ByVal policyNo As String, ByVal customerId As String, ByVal type As String, ByVal subtype As String, ByVal hiclCode As String, ByVal userID As String, ByVal entryDate As DateTime, ByVal followupDate As DateTime, ByVal followupUserId As String, ByVal description As String, ByVal seqNo As String) As Boolean
        Dim dsPolicyNotes As New DataSet
        Dim dtPolicyNotes As DataTable = InitPolicyNotesDataTable(policyNo, customerId, type, subtype, hiclCode, userID, entryDate, followupDate, followupUserId, description, seqNo)
        dsPolicyNotes.Tables.Add(dtPolicyNotes)

        Dim isSaved As Boolean = False
        Dim strErrSavePolicyNote As String = ""
        Try
            isSaved = wspos.SavePolicyNote(dsPolicyNotes, strErrSavePolicyNote)
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message & Environment.NewLine & "SavePolicyNotes Return StrErr:" & strErrSavePolicyNote)
        End Try

        Return isSaved
    End Function

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function InitPolicyNotesDataTable(ByVal policyNo As String, ByVal customerId As String, ByVal type As String, ByVal subtype As String, ByVal hiclCode As String, ByVal userID As String, ByVal entryDate As DateTime, ByVal followupDate As DateTime, ByVal followupUserId As String, ByVal description As String, ByVal seqNo As String) As DataTable
        Dim dtPolicyNotes As New DataTable
        dtPolicyNotes.Columns.Add("Ciwpn_ID", System.Type.GetType("System.Int32"))
        dtPolicyNotes.Columns.Add("Ciwpn_policy_no", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Ciw_no", System.Type.GetType("System.Int32"))
        dtPolicyNotes.Columns.Add("Ciwpn_Type", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Sub_Type", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_HICL_Code", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_UserID", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Entry_Date", System.Type.GetType("System.DateTime"))
        dtPolicyNotes.Columns.Add("Ciwpn_Followup_Date", System.Type.GetType("System.DateTime"))
        dtPolicyNotes.Columns.Add("Ciwpn_Followup_Userid", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Description", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Seq_No", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Create_User", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Create_Date", System.Type.GetType("System.DateTime"))
        dtPolicyNotes.Columns.Add("Ciwpn_Upd_User", System.Type.GetType("System.String"))
        dtPolicyNotes.Columns.Add("Ciwpn_Upd_Date", System.Type.GetType("System.DateTime"))

        Dim dr As DataRow = dtPolicyNotes.NewRow
        dr.Item("Ciwpn_ID") = 0
        dr.Item("Ciwpn_policy_no") = policyNo
        dr.Item("Ciwpn_Ciw_no") = customerId
        dr.Item("Ciwpn_Type") = type
        dr.Item("Ciwpn_Sub_Type") = subtype
        dr.Item("Ciwpn_HICL_Code") = If(Not IsNothing(hiclCode), hiclCode, DBNull.Value)
        dr.Item("Ciwpn_UserID") = userID
        dr.Item("Ciwpn_Entry_Date") = entryDate
        dr.Item("Ciwpn_Followup_Date") = If(followupDate = DateTime.MinValue, DBNull.Value, followupDate)
        dr.Item("Ciwpn_Followup_Userid") = followupUserId
        dr.Item("Ciwpn_Description") = description
        dr.Item("Ciwpn_Seq_No") = If(Not IsNothing(seqNo), seqNo, DBNull.Value)
        dr.Item("Ciwpn_Create_User") = userID
        dr.Item("Ciwpn_Create_Date") = Date.Now
        dr.Item("Ciwpn_Upd_User") = userID
        dr.Item("Ciwpn_Upd_Date") = Date.Now
        dtPolicyNotes.Rows.Add(dr)

        Return dtPolicyNotes

    End Function

    'SaveServiceLog() - Save the service log, called when CurrentCellChanged event of dgSrvLog or user click btnSave
    Private Sub SaveServiceLog(ByVal dr As DataRow)

        'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
        Dim maxServiceEventNumber As String = ""
        If blnIsEnableNBMPolicyPanel Then
            If Not (Me.rbYes.Checked OrElse Me.rbNo.Checked) Then
                MsgBox("Please Select the NBM Retention Offer Yes or No of this Service Log.", MsgBoxStyle.Critical, "NBM Retention Offer")
                Exit Sub
            End If
            If Me.rbYes.Checked Then
                Dim msgBoxResult As MsgBoxResult = MsgBox("NBM Retention Offer Selected Yes , Please Confirm!", MsgBoxStyle.OkCancel, "NBM Retention Offer")
                If msgBoxResult = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If
            maxServiceEventNumber = GetMaxServiceEventNumber()
        End If

        'SQLCommandBuilder -
        'Create insert and update commandtext
        'Dim cmdBuilder As New SqlCommandBuilder(daSrvEvtDet)
        Dim sqlUpdCmd As SqlCommand
        Dim sqlInsCmd As SqlCommand
        Dim intNextSrvNo As Integer
        Dim strInsSQL As String
        Dim strUpdSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
        'Dim dr As DataRow = CType(bm.Current, DataRowView).Row()

        If IsDBNull(dr.Item("IsAppearedInAES")) OrElse Trim(dr.Item("IsAppearedInAES")) = "" Then
            If CheckTransferAES() Then
                dr.Item("IsAppearedInAES") = "Y"
                chkAES.Enabled = True
                chkAES.Checked = True
            Else
                chkAES.Enabled = False
                chkAES.Checked = False
            End If
        Else
            chkAES.Enabled = True
        End If

        'Check Receiver if Status = Handoff
        If cbStatus.SelectedValue = "H" And Trim(cbReceiver.SelectedValue) = "" Then
            MsgBox("Please select the receiver of this service log.", MsgBoxStyle.Critical, "Service Log")
            cbReceiver.Focus()
            Exit Sub
        End If

        'Check if Reminder later than current date
        If (Not (DateTime.Equals(#1/1/1800#, dtReminder.Value))) And DateTime.Compare(dtReminder.Value, Date.Today) < 0 Then
            MsgBox("Reminder date should be equal or later than today.", MsgBoxStyle.Critical, "Service Log")
            dtReminder.Focus()
            Exit Sub
        End If

        ''Clear Reciever when status is not Handoff
        If cbStatus.SelectedValue <> "H" Then
            dr.Item("SecondaryCSRID") = System.DBNull.Value
        End If

        'Fill in AssignDate or CompleteDate
        If cbStatus.SelectedValue = "H" Then
            dr.Item("EventAssignDateTime") = Format(DateTime.Now, gDateTimeFormat)
        ElseIf cbStatus.SelectedValue = "C" Then
            dr.Item("EventCompletionDateTime") = Format(DateTime.Now, gDateTimeFormat)
            dr.Item("EventCloseoutDateTime") = Format(DateTime.Now, gDateTimeFormat)
        End If

        ''If hidSrvEvtNo.Text = "" Then
        ''    'Retrieve next service event number for new records
        ''    intNextSrvNo = FindNextServiceEventNo()
        ''    If intNextSrvNo = 0 Then
        ''        MsgBox("Invalid Service Event Number", MsgBoxStyle.Exclamation, "CS2005")
        ''        Exit Sub
        ''    Else
        ''        'hidSrvEvtNo.Text = CStr(intNextSrvNo)
        ''        dr.Item("ServiceEventNumber") = intNextSrvNo
        ''    End If
        ''End If

        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()

        'Find the next ServiceEventNumber and update csw_srv_evt_dtl_id table
        ' *** FCR start ****
        ''strInsSQL = "declare @no as numeric(15,0); " & _
        ''            "Select @no = max(ServiceEventNumber) from ServiceEventDetail; " & _
        ''            "update  csw_srv_evt_dtl_id set swID = @no+1; " & _
        ''            "insert into ServiceEventDetail values " & _
        ''            "((@no+1), '" & dr.Item("EventCategoryCode") & "', " & _
        ''            "'" & dr.Item("EventTypeCode") & "', " & _
        ''            "null, " & dr.Item("CustomerID") & ", '" & dr.Item("PolicyAccountID") & "', " & _
        ''            "'" & gsUser & "', '" & dr.Item("SecondaryCSRID") & "', " & _
        ''            FormatSQLDate(dr.Item("EventInitialDateTime"), gDateTimeFormat) & ", " & _
        ''            FormatSQLDate(dr.Item("EventAssignDateTime"), gDateTimeFormat) & ", " & _
        ''            FormatSQLDate(dr.Item("EventCompletionDateTime"), gDateTimeFormat) & ", null, " & _
        ''            FormatSQLDate(dr.Item("EventCloseoutDateTime"), gDateTimeFormat) & ", null, " & _
        ''            "'" & dr.Item("EventStatusCode") & "', '" & dr.Item("EventSourceInitiatorCode") & "', " & _
        ''            "'" & dr.Item("EventSourceMediumCode") & "', '" & Strings.Replace(dr.Item("EventNotes"), "'", "''") & "', " & _
        ''            FormatSQLDate(dr.Item("ReminderDate"), gDateFormat) & ", " & _
        ''            "'" & Strings.Replace(dr.Item("ReminderNotes"), "'", "''") & "', null, '" & dr.Item("EventTypeDetailCode") & "', " & _
        ''            "null, null, null, null, getdate(), '" & gsUser & "', " & _
        ''            "getdate(), ' ', '" & dr.Item("isTransferToAes") & "', '" & dr.Item("IsAppearedInAES") & "', " & _
        ''            "'" & dr.Item("IsPolicyAlert") & "', '" & Replace(dr.Item("AlertNotes"), "'", "''") & "'); "

        ''strUpdSQL = "Update ServiceEventDetail set " & _
        ''            "EventCategoryCode = '" & dr.Item("EventCategoryCode") & "', " & _
        ''            "EventTypeCode = '" & dr.Item("EventTypeCode") & "', " & _
        ''            "SecondaryCSRID = '" & dr.Item("SecondaryCSRID") & "', " & _
        ''            "EventInitialDateTime = " & FormatSQLDate(dr.Item("EventInitialDateTime"), gDateTimeFormat) & ", " & _
        ''            "EventAssignDateTime = " & FormatSQLDate(dr.Item("EventAssignDateTime"), gDateTimeFormat) & ", " & _
        ''            "EventCompletionDateTime = " & FormatSQLDate(dr.Item("EventCompletionDateTime"), gDateTimeFormat) & " , " & _
        ''            "EventCloseoutDateTime = " & FormatSQLDate(dr.Item("EventCloseoutDateTime"), gDateTimeFormat) & ", " & _
        ''            "EventStatusCode = '" & dr.Item("EventStatusCode") & "', " & _
        ''            "EventSourceInitiatorCode = '" & dr.Item("EventSourceInitiatorCode") & "', " & _
        ''            "EventSourceMediumCode = '" & dr.Item("EventSourceMediumCode") & "', " & _
        ''            "EventNotes = '" & Replace(dr.Item("EventNotes"), "'", "''") & "', " & _
        ''            "ReminderDate = " & FormatSQLDate(dr.Item("ReminderDate"), gDateFormat) & ", " & _
        ''            "ReminderNotes = '" & Replace(dr.Item("ReminderNotes"), "'", "''") & "', " & _
        ''            "EventTypeDetailCode = '" & dr.Item("EventTypeDetailCode") & "', " & _
        ''            "Updateuser = '" & gsUser & "', updatedatetime = getdate(), " & _
        ''            "BirthdayAlert = ' ', IsTransferToAES = '" & dr.Item("isTransferToAes") & "', " & _

        ''            "IsAppearedInAES = '" & dr.Item("IsAppearedInAES") & "', " & _
        ''            "IsPolicyAlert = '" & dr.Item("IsPolicyAlert") & "', " & _
        ''            "AlertNotes = '" & Replace(dr.Item("AlertNotes"), "'", "''") & "' " & _
        ''            "Where ServiceEventNumber = " & dr.Item("ServiceEventNumber")
        'SQL2008
        If strPolicy = "" Then
            If Not IsDBNull(dr.Item("PolicyAccountNO")) Then
                If rbLife.Checked Then
                    dr.Item("PolicyAccountID") = dr.Item("PolicyAccountNo").ToString.Trim
                ElseIf rbGI.Checked Then
                    dr.Item("PolicyAccountID") = checkPolicyEndDate(dr.Item("PolicyAccountNo").ToString.Trim)
                ElseIf rbEB.Checked Then
                    If Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "12" Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "22" _
                    Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "13" Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "23" Then
                        dr.Item("PolicyAccountID") = GLGetPolicyID(dr.Item("PolicyAccountNo").ToString.Trim)
                    Else
                        dr.Item("PolicyAccountID") = checkPolicyEndDate(dr.Item("PolicyAccountNo").ToString.Trim)
                    End If
                End If
            End If
        ElseIf strPolicyType = "GL" AndAlso Microsoft.VisualBasic.Left(strPolicy, 2) = "12" Or Microsoft.VisualBasic.Left(strPolicy, 2) = "22" _
                          Or Microsoft.VisualBasic.Left(strPolicy, 2) = "13" Or Microsoft.VisualBasic.Left(strPolicy, 2) = "23" Then
            dr.Item("PolicyAccountID") = strPolicyType & strPolicy
        End If
        'C001 - Start
        strInsSQL = "declare @no as numeric(15,0); " &
                    "Select @no = max(ServiceEventNumber) from ServiceEventDetail; " &
                    "update " & serverPrefix & "csw_srv_evt_dtl_id set swID = @no+1; " &
                    "insert into ServiceEventDetail values " &
                    "((@no+1), '" & dr.Item("EventCategoryCode") & "', " &
                    "'" & dr.Item("EventTypeCode") & "', " &
                    "null, " & dr.Item("CustomerID") & ", '" & dr.Item("PolicyAccountID") & "', " &
                    "'" & gsUser & "', '" & dr.Item("SecondaryCSRID") & "', " &
                    ConvertFormatSQLDate(dr.Item("EventInitialDateTime"), gDateTimeFormat) & ", " &
                    ConvertFormatSQLDate(dr.Item("EventAssignDateTime"), gDateTimeFormat) & ", " &
                    ConvertFormatSQLDate(dr.Item("EventCompletionDateTime"), gDateTimeFormat) & ",'" & IIf(chkFCR.Checked, "Y", "N") & "', " &
                    ConvertFormatSQLDate(dr.Item("EventCloseoutDateTime"), gDateTimeFormat) & ", null, " &
                    "'" & dr.Item("EventStatusCode") & "', '" & dr.Item("EventSourceInitiatorCode") & "', " &
                    "'" & dr.Item("EventSourceMediumCode") & "', N'" & Strings.Replace(dr.Item("EventNotes"), "'", "''") & "', " &
                    FormatSQLDate(dr.Item("ReminderDate"), gDateFormat) & ", " &
                    "N'" & Strings.Replace(dr.Item("ReminderNotes"), "'", "''") & "', null, '" & dr.Item("EventTypeDetailCode") & "', " &
                    "null, null, null, '" & IIf(chkACC.Checked, "Y", "N") & "', getdate(), '" & gsUser & "', " &
                    "getdate(), ' ', '" & dr.Item("isTransferToAes") & "', '" & dr.Item("IsAppearedInAES") & "', " &
                    "'" & dr.Item("IsPolicyAlert") & "', N'" & Replace(dr.Item("AlertNotes"), "'", "''") & "', " &
                    "'" & dr.Item("IsIdVerify") & "','" & IIf(chkMCV.Checked, "Y", "N") & "', null, null); "
        'C001 - End

        'SQL2008
        'C001 - Start
        strUpdSQL = "Update ServiceEventDetail set " &
                    "EventCategoryCode = '" & dr.Item("EventCategoryCode") & "', " &
                    "EventTypeCode = '" & dr.Item("EventTypeCode") & "', " &
                    "SecondaryCSRID = '" & dr.Item("SecondaryCSRID") & "', " &
                    "EventInitialDateTime = " & ConvertFormatSQLDate(dr.Item("EventInitialDateTime"), gDateTimeFormat) & ", " &
                    "EventAssignDateTime = " & ConvertFormatSQLDate(dr.Item("EventAssignDateTime"), gDateTimeFormat) & ", " &
                    "EventCompletionDateTime = " & ConvertFormatSQLDate(dr.Item("EventCompletionDateTime"), gDateTimeFormat) & " , " &
                    "EventCloseoutDateTime = " & ConvertFormatSQLDate(dr.Item("EventCloseoutDateTime"), gDateTimeFormat) & ", " &
                    "EventStatusCode = '" & dr.Item("EventStatusCode") & "', " &
                    "EventSourceInitiatorCode = '" & dr.Item("EventSourceInitiatorCode") & "', " &
                    "EventSourceMediumCode = '" & dr.Item("EventSourceMediumCode") & "', " &
                    "EventNotes = N'" & Replace(dr.Item("EventNotes"), "'", "''") & "', " &
                    "ReminderDate = " & FormatSQLDate(dr.Item("ReminderDate"), gDateFormat) & ", " &
                    "ReminderNotes = N'" & Replace(dr.Item("ReminderNotes"), "'", "''") & "', " &
                    "EventTypeDetailCode = '" & dr.Item("EventTypeDetailCode") & "', " &
                    "Updateuser = '" & gsUser & "', updatedatetime = getdate(), " &
                    "BirthdayAlert = ' ', IsTransferToAES = '" & dr.Item("isTransferToAes") & "', " &
                    "IsAppearedInAES = '" & dr.Item("IsAppearedInAES") & "', " &
                    "IsPolicyAlert = '" & dr.Item("IsPolicyAlert") & "', " &
                    "AlertNotes = N'" & Replace(dr.Item("AlertNotes"), "'", "''") & "', " &
                    "EventCloseoutCode = '" & IIf(chkFCR.Checked, "Y", "N") & "', " &
                    "caseno = '" & IIf(chkACC.Checked, "Y", "N") & "', " &
                    "PolicyAccountID = '" & dr.Item("PolicyAccountID") & "', " &
                    "IsIdVerify = '" & dr.Item("IsIdVerify") & "' " &
                    ", MCV =  '" & IIf(chkMCV.Checked, "Y", "N") & "' " &
                    "Where ServiceEventNumber = " & dr.Item("ServiceEventNumber")
        'C001 - End
        ' *** FCR end ****

        sqlInsCmd = New SqlCommand(strInsSQL, sqlConn)
        sqlInsCmd.CommandTimeout = gQryTimeOut
        daSrvEvtDet.InsertCommand = sqlInsCmd

        sqlUpdCmd = New SqlCommand(strUpdSQL, sqlConn)
        sqlUpdCmd.CommandTimeout = gQryTimeOut
        daSrvEvtDet.UpdateCommand = sqlUpdCmd

        Try
            'Update the new record to dataset
            'daSrvEvtDet.Update(dsSrvLog, "ServiceEventDetail")
            daSrvEvtDet.Update(dsSrvLog.Tables("ServiceEventDetail"))

            'Update method have done AcceptChanges already
            'dsSrvLog.Tables("ServiceEventDetail").AcceptChanges()
        Catch eSQL As SqlException
            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Save data")
        Catch ex As Exception
            MsgBox("Exception: " & ex.Message, MsgBoxStyle.Exclamation, "Save Data")
        End Try
        'btnNew.Enabled = True
        'btnSave.Enabled = False
        'btnCancel.Enabled = False

        'Post a message on the status bar of main window
        wndMain.StatusBarPanel1.Text = "Save Complete"

        RaiseEvent EventSaved(Me, dr)

        'Enable/Disable fields - done in Refresh_ServiceLog()
        'CheckStatus()

        'oliver 2024-3-6 added for ITSR5061 Retention Offer Campaign 
        If blnIsEnableNBMPolicyPanel Then
            CheckSaveNBMPolicy(dr, maxServiceEventNumber)
        End If

        'Refresh datagrid
        wndInbox.RefreshInbox()
        Refresh_ServiceLog()
    End Sub

    'oliver 2024-3-7 added for ITSR5061 Retention Offer Campaign 
    Private Sub CheckSaveNBMPolicy(ByVal dr As DataRow, ByVal maxServiceEventNumber As String)
        Try
            Dim policyAccountID As String = ""
            Dim serviceEventNumber As String = ""
            Dim customerID As String = ""
            If Not IsDBNull(dr.Item("PolicyAccountID")) AndAlso Not String.IsNullOrEmpty(dr.Item("PolicyAccountID")) Then
                policyAccountID = dr.Item("PolicyAccountID").ToString()
            End If
            If Not IsDBNull(dr.Item("CustomerID")) AndAlso Not String.IsNullOrEmpty(dr.Item("CustomerID")) Then
                customerID = dr.Item("CustomerID").ToString()
            End If
            If Not IsDBNull(dr.Item("ServiceEventNumber")) AndAlso Not String.IsNullOrEmpty(dr.Item("ServiceEventNumber")) Then
                serviceEventNumber = dr.Item("ServiceEventNumber").ToString()
            Else
                serviceEventNumber = (CInt(maxServiceEventNumber) + 1).ToString
            End If
            If Not String.IsNullOrEmpty(serviceEventNumber) Then
                CheckNBMPolicyRadioButton(policyAccountID, serviceEventNumber, customerID)
            End If
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
    End Sub

    Private Function GetMaxServiceEventNumber() As String
        Dim searchMaxServiceEventNumberDataSet As DataSet = APIServiceBL.CallAPIBusi(g_Comp,
            "SEARCH_MAX_SERVICE_EVENT_NUMBER",
             New Dictionary(Of String, String)() From {})
        If searchMaxServiceEventNumberDataSet.Tables.Count > 0 Then
            Dim searchMaxServiceEventNumberDataTable As DataTable = searchMaxServiceEventNumberDataSet.Tables(0)
            If searchMaxServiceEventNumberDataTable.Rows.Count > 0 Then
                If Not IsDBNull(searchMaxServiceEventNumberDataTable.Rows(0).Item("ServiceEventNumber")) AndAlso Not String.IsNullOrEmpty(searchMaxServiceEventNumberDataTable.Rows(0).Item("ServiceEventNumber").ToString) Then
                    Return searchMaxServiceEventNumberDataTable.Rows(0).Item("ServiceEventNumber").ToString
                End If
            End If
        End If
        Return ""
    End Function

    Private Sub CancelServiceLog()
        'dsSrvLog.Tables("ServiceEventDetail").RejectChanges()
        'Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).CancelCurrentEdit()
        'btnNew.Enabled = True
        Refresh_ServiceLog()
    End Sub

    Private Sub dgSrvLog_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgSrvLog.CurrentCellChanged
        Dim tempDt As New DataTable
        Dim blnSave As Boolean
        Dim dr As DataRow

        'Check save
        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()
        blnSave = False
        If dsSrvLog.HasChanges Then
            If strNewFlag = "Y" Then
                strNewFlag = "N"
                blnSave = False
            Else
                tempDt = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Modified)
                If IsNothing(tempDt) Then
                    tempDt = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Added)
                    If Not (IsNothing(tempDt)) Then
                        blnSave = True
                    End If
                Else
                    blnSave = True
                End If

                If blnSave = True Then
                    'If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
                    For Each dr In tempDt.Rows
                            SaveServiceLog(dr)
                        Next
                    'Else
                    '    'dsSrvLog.Tables("ServiceEventDetail").RejectChanges()
                    '    CancelServiceLog()
                    'End If
                End If
            End If
        End If
        tempDt.Dispose()

        CheckStatus()

        'oliver 2024-3-1 add ITSR5061 Retention Offer Campaign 
        CheckEnableNBMPanel()
    End Sub

    Private Sub chkReminder_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReminder.CheckedChanged
        If chkReminder.Checked Then
            dtReminder.Enabled = True
            If dtReminder.Value = #1/1/1800# Then
                dtReminder.Value = DateTime.Today
            End If
        Else
            dtReminder.Value = #1/1/1800#
            dtReminder.Enabled = False
        End If
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDownloadPostSalesCall.Click
    '    Refresh_ServiceLog()
    'End Sub

    Public Sub Refresh_ServiceLog()
        Dim b As Binding
        'Remover bindings of datetimepicker before clearing the ServiceEventDetail table
        dtInitial.DataBindings.Remove(dtInitial.DataBindings.Item("Value"))
        dtInitial.Value = #1/1/1800#
        dtReminder.DataBindings.Remove(dtReminder.DataBindings.Item("Value"))
        dtReminder.Value = #1/1/1800#

        dsSrvLog.Tables("ServiceEventDetail").Clear()
        daSrvEvtDet.Fill(dsSrvLog, "ServiceEventDetail")
        dgSrvLog.DataSource = dsSrvLog.Tables("ServiceEventDetail")

        'Rebind the bindings of datetimepicker
        b = New Binding("Value", dsSrvLog.Tables("ServiceEventDetail"), "EventInitialDateTime")
        AddHandler b.Format, AddressOf DTFormatter
        AddHandler b.Parse, AddressOf DTParser
        dtInitial.DataBindings.Add(b)
        b = New Binding("Value", dsSrvLog.Tables("ServiceEventDetail"), "ReminderDate")
        AddHandler b.Format, AddressOf DTFormatter
        AddHandler b.Parse, AddressOf DTParser
        dtReminder.DataBindings.Add(b)

        CheckStatus()

        CheckEnableNBMPanel()
    End Sub


    Private Sub btnSaveC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveC.Click
        'Save the current row
        bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
        Dim dr As DataRow = CType(bm.Current, DataRowView).Row()

        'Post Sales Call - Start
        If cbEventCat.Text.Trim = "Courtesy Call - Non-vulnerable customer" AndAlso Me.rad_NVCWelcomeCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text.Trim = "Courtesy Call ¡V Suitability mismatch" AndAlso Me.rad_SuitabilityMisMatch.Checked <> True Then
            MsgBox("Event Catergory not match suitability mismatch status")
            Exit Sub
        End If


        If cbEventCat.Text = "Courtesy Call - Vulnerable customer" AndAlso Me.rad_VCPostSalesCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text = "ILAS - Post-Sales Call" AndAlso Me.rad_ILASPostSalseCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If
        'Post Sales Call - End


        SaveServiceLog(dr)
        'Close Window
        CType(Me.Parent.Parent.Parent, Form).Close()
    End Sub


    'added by ITDYMH 20150229 Post-Sales Call
    Private Sub getPostSalesCallInfo()

        'Me.lbl_InforceDate.Text = "N/A"
        'Me.lbl_PostCallStatus.Text = "N/A"
        'Me.lbl_PostCallCount.Text = "0 time(s)"

        Dim s_Sql As String = "cswsp_GetPostSalesCallList"

        If Not sqlConn.State = ConnectionState.Open Then
            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
        End If

        daPostSalesCallInfo = New SqlDataAdapter(s_Sql, sqlConn)

        Try
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = IIf(strPolicy Is Nothing, "", strPolicy)
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.CommandType = CommandType.StoredProcedure
            daPostSalesCallInfo.Fill(dsSrvLog, "PostSalesCallInfo")

            'doRefreshPostCallInfo()

        Catch e As Exception
            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
        Finally
            sqlConn.Close()
        End Try


    End Sub

    'added by ITDYMH 20150229 Post-Sales Call
    Private Sub doRefreshPostCallInfo()

        Me.lbl_InforceDate.Text = "N/A"
        Me.lbl_PostCallStatus.Text = "N/A"
        Me.lbl_PostCallCount.Text = "0 time(s)"

        If dsSrvLog.Tables("PostSalesCallInfo") IsNot Nothing AndAlso dsSrvLog.Tables("PostSalesCallInfo").Rows.Count > 0 Then

            Dim i_Age As String
            Try
                i_Age = Convert.ToInt32(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("Age").ToString())
            Catch ex As Exception
                i_Age = 0
            End Try


            Dim i_EduLevelValue As Integer
            Try
                i_EduLevelValue = Convert.ToInt32(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("EduLevelValue").ToString())
            Catch ex As Exception
                i_EduLevelValue = 0
            End Try

            Dim b_IsILAS As Boolean
            Try
                b_IsILAS = IIf(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("IsILAS").ToString().Equals("Y"), True, False)
            Catch ex As Exception
                b_IsILAS = False
            End Try

            Dim s_HaveRegIncome As String
            s_HaveRegIncome = dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("HaveRegIncome").ToString()


            'set default
            Dim i_CallCount As Integer
            i_CallCount = 0
            Me.rad_NVCWelcomeCall.Checked = False
            Me.rad_VCPostSalesCall.Checked = False
            Me.rad_ILASPostSalseCall.Checked = False
            Me.rad_SuitabilityMisMatch.Checked = False

            If dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallType") = "Welcome Call" Then
                Me.rad_NVCWelcomeCall.AutoCheck = True
                Me.rad_NVCWelcomeCall.Checked = True
                Me.rad_NVCWelcomeCall.AutoCheck = False
                If Not IsDBNull(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_NVC_WelcomeCall")) Then
                    i_CallCount = i_CallCount + dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_NVC_WelcomeCall")
                End If
            ElseIf dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallType") = "Suitability Mismatch" Then
                Me.rad_SuitabilityMisMatch.AutoCheck = True
                Me.rad_SuitabilityMisMatch.Checked = True
                Me.rad_SuitabilityMisMatch.AutoCheck = False
                If Not IsDBNull(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_SuitabilityMisMatch")) Then
                    i_CallCount = i_CallCount + dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_SuitabilityMisMatch")
                End If
            ElseIf dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallType") = "VC Call" Then
                Me.rad_VCPostSalesCall.AutoCheck = True
                Me.rad_VCPostSalesCall.Checked = True
                Me.rad_VCPostSalesCall.AutoCheck = False
                If Not IsDBNull(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_VC_PostSaleCall")) Then
                    i_CallCount = i_CallCount + dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_VC_PostSaleCall")
                End If

            ElseIf dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallType").ToString().IndexOf("ILAS") > -1 Then
                Me.rad_ILASPostSalseCall.AutoCheck = True
                Me.rad_ILASPostSalseCall.Checked = True
                Me.rad_ILASPostSalseCall.AutoCheck = False
                If Not IsDBNull(dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_ILAS_PostSaleCall")) Then
                    i_CallCount = i_CallCount + dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("NoOfDiffDays_ILAS_PostSaleCall")
                End If
            End If

            Me.lbl_PostCallStatus.Text = dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallStatus").ToString()
            Me.lbl_PostCallCount.Text = i_CallCount.ToString() & " time(s)"

            Dim s_InforeDate As String
            Dim d_InforeDate As Date
            s_InforeDate = dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("ExhibitInforceDate").ToString()
            Try
                d_InforeDate = Convert.ToDateTime(s_InforeDate)
                Me.lbl_InforceDate.Text = d_InforeDate.ToString("dd-MMM-yyyy")
            Catch ex As Exception
                Me.lbl_InforceDate.Text = ""
            End Try

        End If
    End Sub

    'Private Sub txtDownloadPostSalesCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDownloadPostSalesCall.Click
    '    Dim strFileName As String = ""
    '    SaveFileDialogDownlaod.FileName = "CUPCreTrans" & Now.ToString.Format("yyyyMMdd") & ".xls"

    '    If SaveFileDialogDownlaod.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If
    '    If SaveFileDialogDownlaod.FileName = "" Then
    '        MsgBox("please input a file name")
    '        Exit Sub
    '    Else
    '        strFileName = SaveFileDialogDownlaod.FileName
    '    End If



    '    Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()
    '    xlWorkBook = xlApp.Workbooks.Add(misValue)
    '    xlWorkBook.Application.DisplayAlerts = False
    '    worksheet = xlWorkBook.Worksheets("Sheet1")
    '    Dim columnsCount As Integer = DataGridViewTransRecord.Columns.Count
    '    worksheet.Cells.NumberFormat = "@"
    '    For Each column As DataGridViewColumn In DataGridViewTransRecord.Columns
    '        worksheet.Cells(1, column.Index + 1).Value = column.Name
    '    Next
    '    'Export Header Name End


    '    'Export Each Row Start
    '    For i As Integer = 0 To DataGridViewTransRecord.Rows.Count - 2
    '        Dim columnIndex As Integer = 0
    '        Do Until columnIndex = columnsCount
    '            worksheet.Cells(i + 2, columnIndex + 1).Value = DataGridViewTransRecord.Item(columnIndex, i).Value.ToString
    '            columnIndex += 1
    '        Loop
    '    Next
    '    worksheet.Rows.Item(1).EntireColumn.AutoFit()
    '    xlWorkBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
    '     Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)


    '    'xlWorkBook.SaveAs(APP.Path & "\CUPCreTrans" & DateTimePickerStart.Value.ToString("ddMMMyyyy") & DateTimePickerEnd.Value.ToString("ddMMMyyyy") + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
    '    ' Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
    '    xlWorkBook.Close(True, misValue, misValue)
    '    xlApp.Quit()

    '    releaseObject(worksheet)
    '    releaseObject(xlWorkBook)
    '    releaseObject(xlApp)
    '    MsgBox("downloaded")

    'End Sub

    Private Sub rad_NVCWelcomeCall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_NVCWelcomeCall.CheckedChanged

    End Sub

    Private Sub rad_SuitabilityMisMatch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_SuitabilityMisMatch.CheckedChanged

    End Sub

    Private Sub lbl_InforceDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_InforceDate.Click

    End Sub

#Region "New Service Log function"
    Private Sub btncustlogweb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncustlogweb.Click
        Dim dsresult As New DataSet
        Dim strErr As String = String.Empty
        If Not ServiceLogBL.GetSerLogbycriteriaNew("1900-01-01", "1900-01-01", "", "", strCustID, False, True, dsresult, strErr, "HK") Then
            MessageBox.Show(strErr)
            Exit Sub
        Else
            ServiceLogBL.Generatewebpage(dsresult)
        End If
    End Sub
#End Region


    Private Function isInForce() As Boolean
        Dim bReturn As Boolean = False
        If Not sqlConn2.State = ConnectionState.Open Then
            sqlConn2.ConnectionString = strCIWConn
            sqlConn2.Open()
        End If
        daTmp = New SqlDataAdapter("select AccountStatusCode from PolicyAccount nolock where PolicyAccountID = @policyNo", sqlConn2)
        daTmp.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = IIf(strPolicy Is Nothing, "", strPolicy)
        dsTmp = New DataSet
        daTmp.Fill(dsTmp)
        If dsTmp.Tables(0).Rows.Count > 0 Then
            If "1,2,3,4,V,X,Z".Contains(dsTmp.Tables(0).Rows(0)(0)) Then
                bReturn = True
            Else
                bReturn = False
            End If
        Else
            bReturn = False
        End If
        sqlConn2.Close()
        Return bReturn
    End Function
    Private Function isCooling() As Boolean
        Dim bReturn As Boolean = False
        If Not sqlConn2.State = ConnectionState.Open Then
            sqlConn2.ConnectionString = strCIWConn
            sqlConn2.Open()
        End If
        daTmp = New SqlDataAdapter("select iif(cswpuw_flook_dline > '1900-01-01 00:00:00',1,0) from csw_policy_uw nolock where cswpuw_poli_id = @policyNo", sqlConn2)
        daTmp.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = IIf(strPolicy Is Nothing, "", strPolicy)
        dsTmp = New DataSet
        daTmp.Fill(dsTmp)
        If dsTmp.Tables(0).Rows.Count > 0 Then
            If dsTmp.Tables(0).Rows(0)(0) > 0 Then
                bReturn = True
            Else
                bReturn = False
            End If
        Else
            bReturn = False
        End If
        sqlConn2.Close()
        Return bReturn
    End Function

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function getPiAuth(ByVal strPolicy As String) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_PI_AUTH",
                        New Dictionary(Of String, String) From {
                        {"strPolicyNo", strPolicy}
                        })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return dt

    End Function

    '   Private Function getPiAuth() As DataTable
    '       Try
    '           If strPolicy.Length > 0 Then
    '               Dim dsReturn As New DataSet
    '               If Not sqlConn2.State = ConnectionState.Open Then
    '                   sqlConn2.ConnectionString = strCIWConn
    '                   sqlConn2.Open()
    '               End If
    '               'daTmp = New SqlDataAdapter("sp_crs_get_auth_policy_pi", sqlConn2)
    '               Dim strSql = "select csw_poli_rel.CustomerID, Customer.EmailAddr, crs_auth_policy_pi.LastUpdateDate, crs_auth_policy_pi.[Enable] from csw_poli_rel with (nolock)" &
    '" inner join Customer with (nolock) on Customer.CustomerID = csw_poli_rel.CustomerID" &
    '" left outer join crs_auth_policy_pi with (nolock) on csw_poli_rel.PolicyAccountID = crs_auth_policy_pi.PolicyAccountID and csw_poli_rel.CustomerID = crs_auth_policy_pi.PICustID" &
    '" where (csw_poli_rel.PolicyAccountID = @PolicyAccountID)" &
    '" and csw_poli_rel.PolicyRelateCode = 'PI'"
    '               daTmp = New SqlDataAdapter(strSql, sqlConn2)
    '               daTmp.SelectCommand.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
    '               'daTmp.SelectCommand.Parameters.Add("@PICustID", SqlDbType.VarChar, 20).Value = ""
    '               'daTmp.SelectCommand.Parameters.Add("@Enable", SqlDbType.Char, 1).Value = ""
    '               'daTmp.SelectCommand.CommandType = CommandType.StoredProcedure
    '               dsReturn = New DataSet
    '               daTmp.Fill(dsReturn)
    '               sqlConn2.Close()
    '               Return dsReturn.Tables(0)
    '           End If
    '       Catch ex As Exception

    '       End Try
    '   End Function

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function UpdatePiAuth(ByVal policyAccountID As String, ByVal piCustID As String, ByVal enable As String, ByVal usr As String) As Boolean
        Dim isUpdate As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "UPDATE_PI_AUTH",
                                New Dictionary(Of String, String) From {
                                {"PolicyAccountID", Trim(policyAccountID)},
                                {"PICustID", Trim(piCustID)},
                                {"Enable", enable},
                                {"usr", usr}
                                })
            isUpdate = True
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when updating the comments. Error:  " & ex.Message)
        End Try
        Return isUpdate
    End Function

    'Private Function updatePiAuth() As Boolean
    '    Try
    '        Dim dsReturn As New DataSet
    '        If Not sqlConn2.State = ConnectionState.Open Then
    '            sqlConn2.ConnectionString = strCIWConn
    '            sqlConn2.Open()
    '        End If
    '        Dim cmd = New SqlClient.SqlCommand("sp_crs_update_auth_policy_pi", sqlConn2)
    '        cmd.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
    '        cmd.Parameters.Add("@PICustID", SqlDbType.VarChar, 20).Value = sPiID
    '        cmd.Parameters.Add("@Enable", SqlDbType.Char, 1).Value = "Y"
    '        cmd.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = gsUser
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.ExecuteNonQuery()
    '        sqlConn2.Close()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString)
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try
    '    Return True
    'End Function

    Private Function getPiAuthOtp() As System.Collections.Generic.Dictionary(Of String, String)
        Dim returnObj As New System.Collections.Generic.Dictionary(Of String, String)
        Dim objclsPiAuthResponse As CRS_Util.clsJSONBusinessObj.clsPiAuthResponse
        objclsPiAuthResponse = CRS_Util.clsJSONTool.CallPiAuthGenKey(sPiID, gsUser)

        If Not objclsPiAuthResponse Is Nothing Then
            If objclsPiAuthResponse.status.Equals("error") Then
                MessageBox.Show("Fail to gen OTP with following error:" & objclsPiAuthResponse.error.message)
            End If
            If objclsPiAuthResponse.status.Equals("success") Then
                returnObj.Add("otp", objclsPiAuthResponse.data.unlocKey)
                returnObj.Add("expiry", objclsPiAuthResponse.data.expiry.Substring(0, 10))
                returnObj.Add("expiryY", objclsPiAuthResponse.data.expiry.Substring(0, 4))
                returnObj.Add("expiryM", objclsPiAuthResponse.data.expiry.Substring(5, 2))
                returnObj.Add("expiryD", objclsPiAuthResponse.data.expiry.Substring(8, 2))
                Dim sPhNamePrefix As String = ""
                Dim sPhFirstName As String = ""
                Dim sPhNameSuffix As String = ""
                Dim sPhChiFstNm As String = ""
                Dim sPiNamePrefix As String = ""
                Dim sPiFirstName As String = ""
                Dim sPiNameSuffix As String = ""
                Dim sPiChiFstNm As String = ""
                Dim sProduct As String = ""
                Dim sProductChi As String = ""

                Dim dsReturn As New DataSet
                If Not sqlConn2.State = ConnectionState.Open Then
                    sqlConn2.ConnectionString = strCIWConn
                    sqlConn2.Open()
                End If
                daTmp = New SqlDataAdapter("select NamePrefix, FirstName, NameSuffix, ChiFstNm from Customer with (nolock) inner join csw_poli_rel with (nolock) on Customer.CustomerID = csw_poli_rel.CustomerID where csw_poli_rel.PolicyRelateCode = @Type and csw_poli_rel.PolicyAccountID = @PolicyAccountID", sqlConn2)
                daTmp.SelectCommand.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
                daTmp.SelectCommand.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = "PH"
                dsReturn = New DataSet
                daTmp.Fill(dsReturn)
                If dsReturn.Tables(0).Rows.Count > 0 Then
                    sPhNamePrefix = dsReturn.Tables(0).Rows(0)("NamePrefix").ToString
                    sPhFirstName = dsReturn.Tables(0).Rows(0)("FirstName").ToString
                    sPhNameSuffix = dsReturn.Tables(0).Rows(0)("NameSuffix").ToString
                    sPhChiFstNm = dsReturn.Tables(0).Rows(0)("ChiFstNm").ToString
                End If

                daTmp.SelectCommand.Parameters("@Type").Value = "PI"
                dsReturn = New DataSet
                daTmp.Fill(dsReturn)
                If dsReturn.Tables(0).Rows.Count > 0 Then
                    sPiNamePrefix = dsReturn.Tables(0).Rows(0)("NamePrefix").ToString
                    sPiFirstName = dsReturn.Tables(0).Rows(0)("FirstName").ToString
                    sPiNameSuffix = dsReturn.Tables(0).Rows(0)("NameSuffix").ToString
                    sPiChiFstNm = dsReturn.Tables(0).Rows(0)("ChiFstNm").ToString
                End If

                daTmp = New SqlDataAdapter("select [Product].[Description], [Product_Chi].ChineseDescription from [PolicyAccount] with (nolock) inner join [Product] with (nolock) on [PolicyAccount].ProductID = [Product].ProductID inner join " & gcNBSDB & "[Product_Chi] with (nolock) on [PolicyAccount].ProductID = [Product_Chi].ProductID where [PolicyAccount].PolicyAccountID = @PolicyAccountID", sqlConn2)
                daTmp.SelectCommand.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
                dsReturn = New DataSet
                daTmp.Fill(dsReturn)
                If dsReturn.Tables(0).Rows.Count > 0 Then
                    sProduct = dsReturn.Tables(0).Rows(0)("Description").ToString
                    sProductChi = dsReturn.Tables(0).Rows(0)("ChineseDescription").ToString
                Else
                    daTmp = New SqlDataAdapter("select ProductName, ProductNameCHI from [PolicyAccount] with (nolock) inner join GI_Product with (nolock) on [PolicyAccount].ProductID = GI_Product.ProductID where [PolicyAccount].PolicyAccountID = @PolicyAccountID", sqlConn2)
                    daTmp.SelectCommand.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
                    dsReturn = New DataSet
                    daTmp.Fill(dsReturn)
                    If dsReturn.Tables(0).Rows.Count > 0 Then
                        sProduct = dsReturn.Tables(0).Rows(0)("ProductName").ToString
                        sProductChi = dsReturn.Tables(0).Rows(0)("ProductNameCHI").ToString
                    End If
                End If

                sqlConn2.Close()

                returnObj.Add("PH_NamePrefix", sPhNamePrefix)
                returnObj.Add("PH_FirstName", sPhFirstName)
                returnObj.Add("PH_NameSuffix", sPhNameSuffix)
                returnObj.Add("PH_ChiFstNm", sPhChiFstNm)
                returnObj.Add("PI_NamePrefix", sPiNamePrefix)
                returnObj.Add("PI_FirstName", sPiFirstName)
                returnObj.Add("PI_NameSuffix", sPiNameSuffix)
                returnObj.Add("PI_ChiFstNm", sPiChiFstNm)
                returnObj.Add("Product", sProduct)
                returnObj.Add("ProductChi", sProductChi)
                Return returnObj
            End If
        Else
            MessageBox.Show("Fail to gen OTP")
        End If
        Return returnObj
    End Function

    Private Function sendPiMail(ByVal dOtp As System.Collections.Generic.Dictionary(Of String, String), ByVal toEmail As String) As Boolean
        Try
            Dim strToMail As String = toEmail
            Dim strSubject As String = "FWD eServices registration request <policy no. " & strPolicy & ">¡u´I½ÃeServices¡v½ã¤á¥Ó½Ð <«O³æ¸¹½X " & strPolicy & ">"
            Dim strFrMail As String = "cs.hk@fwd.com"
            Dim strAttachmentPath As String = ""
            Dim strContent As String = ""
            strContent &= System.IO.File.ReadAllText(".\CETPEP.bin")

            strContent = strContent.Replace("<OTP_Image>", "<img border=0 width=89 height=95 id=""Picture_x0020_2"" src=""cid:image2103.jpg@01D19732.1B190AC0"" alt=""cid:3613433F-A0B7-4806-A8B0-41A61C5E1292"">")
            strContent = strContent.Replace("<OTP_Policy>", strPolicy)
            strContent = strContent.Replace("<OTP_PH_FirstName>", dOtp("PH_FirstName"))
            strContent = strContent.Replace("<OTP_PH_NameSuffix>", dOtp("PH_NameSuffix"))
            strContent = strContent.Replace("<OTP_PI_FirstName>", dOtp("PI_FirstName"))
            strContent = strContent.Replace("<OTP_PI_NameSuffix>", dOtp("PI_NameSuffix"))
            strContent = strContent.Replace("<OTP_Product>", dOtp("Product"))
            strContent = strContent.Replace("<OTP_ProductChi>", dOtp("ProductChi"))
            strContent = strContent.Replace("<OTP_Token>", dOtp("otp"))
            strContent = strContent.Replace("<OTP_Expiry>", dOtp("expiry"))
            strContent = strContent.Replace("<OTP_ExpiryY>", dOtp("expiryY"))
            strContent = strContent.Replace("<OTP_ExpiryM>", dOtp("expiryM"))
            strContent = strContent.Replace("<OTP_ExpiryD>", dOtp("expiryD"))

            Dim mailMsg As New CommonWS.SerializableMailMessage()

            Dim mailAttachment As New CommonWS.SerializableAttachment()
            Dim fromAddr As New CommonWS.SerializableMailAddress()
            Dim toAddr(0) As CommonWS.SerializableMailAddress
            'Dim bcc(0) As CommonWS.SerializableMailAddress
            Dim cc(2) As CommonWS.SerializableMailAddress

            Dim sCc(2) As String
            'sCc = DecryptData(My.Settings.CS2005_SPCCAdd).Split(";")
            sCc = My.Settings.CS2005_SPCCAdd.Split(";")

            fromAddr.Address = strFrMail
            toAddr(0) = New CommonWS.SerializableMailAddress()
            toAddr(0).Address = strToMail
            cc(0) = New CommonWS.SerializableMailAddress()
            cc(0).Address = sCc(0)
            cc(1) = New CommonWS.SerializableMailAddress()
            cc(1).Address = sCc(1)
            cc(2) = New CommonWS.SerializableMailAddress()
            cc(2).Address = sCc(2)
            mailMsg.From = fromAddr
            mailMsg.To = toAddr
            mailMsg.Subject = strSubject
            mailMsg.Body = strContent
            'mailMsg.Bcc = bcc
            mailMsg.CC = cc
            mailMsg.IsBodyHtml = True
            mailAttachment.ContentStream = System.IO.File.ReadAllBytes("c:\prodapps\La dll\image2103.jpg")
            mailAttachment.ContentId = "image2103.jpg@01D19732.1B190AC0"
            Dim attachments(0) As CommonWS.SerializableAttachment
            attachments(0) = mailAttachment
            mailMsg.Attachments = attachments

            Using ws As CommonWS.Service = GetCommonWS()
                ws.SendExternalMail(mailMsg)
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function sendBrokerMail(ByVal dOtp As System.Collections.Generic.Dictionary(Of String, String), ByVal toEmail As String) As Boolean
        Try
            Dim strToMail As String = toEmail
            Dim strSubject As String = "FWD eServices registration request <policy no. " & strPolicy & ">¡u´I½ÃeServices¡v½ã¤á¥Ó½Ð <«O³æ¸¹½X " & strPolicy & ">"
            Dim strFrMail As String = "cs.hk@fwd.com"
            Dim strAttachmentPath As String = ""
            Dim strContent As String = ""
            strContent &= System.IO.File.ReadAllText(".\CETPEB.bin")

            strContent = strContent.Replace("<OTP_Image>", "<img border=0 width=89 height=95 id=""Picture_x0020_2"" src=""cid:image2103.jpg@01D19732.1B190AC0"" alt=""cid:3613433F-A0B7-4806-A8B0-41A61C5E1292"">")
            strContent = strContent.Replace("<OTP_Policy>", strPolicy)
            strContent = strContent.Replace("<OTP_PH_FirstName>", dOtp("PH_FirstName"))
            strContent = strContent.Replace("<OTP_PH_NameSuffix>", dOtp("PH_NameSuffix"))
            strContent = strContent.Replace("<OTP_PI_FirstName>", dOtp("PI_FirstName"))
            strContent = strContent.Replace("<OTP_PI_NameSuffix>", dOtp("PI_NameSuffix"))
            strContent = strContent.Replace("<OTP_Product>", dOtp("Product"))
            strContent = strContent.Replace("<OTP_ProductChi>", dOtp("ProductChi"))
            strContent = strContent.Replace("<OTP_Token>", dOtp("otp"))
            strContent = strContent.Replace("<OTP_Expiry>", dOtp("expiry"))
            strContent = strContent.Replace("<OTP_ExpiryY>", dOtp("expiryY"))
            strContent = strContent.Replace("<OTP_ExpiryM>", dOtp("expiryM"))
            strContent = strContent.Replace("<OTP_ExpiryD>", dOtp("expiryD"))

            Dim mailMsg As New CommonWS.SerializableMailMessage()

            Dim mailAttachment As New CommonWS.SerializableAttachment()
            Dim fromAddr As New CommonWS.SerializableMailAddress()
            Dim toAddr(0) As CommonWS.SerializableMailAddress
            'Dim bcc(0) As CommonWS.SerializableMailAddress
            Dim cc(2) As CommonWS.SerializableMailAddress

            Dim sCc(2) As String
            'sCc = DecryptData(My.Settings.CS2005_SPCCAdd).Split(";")
            sCc = My.Settings.CS2005_SPCCAdd.Split(";")

            fromAddr.Address = strFrMail
            toAddr(0) = New CommonWS.SerializableMailAddress()
            toAddr(0).Address = strToMail
            'bcc(0) = New CommonWS.SerializableMailAddress()
            'bcc(0).Address = "cs.hk@fwd.com"
            cc(0) = New CommonWS.SerializableMailAddress()
            cc(0).Address = sCc(0)
            cc(1) = New CommonWS.SerializableMailAddress()
            cc(1).Address = sCc(1)
            cc(2) = New CommonWS.SerializableMailAddress()
            cc(2).Address = sCc(2)
            mailMsg.From = fromAddr
            mailMsg.To = toAddr
            mailMsg.Subject = strSubject
            mailMsg.Body = strContent
            'mailMsg.Bcc = bcc
            mailMsg.CC = cc
            mailMsg.IsBodyHtml = True
            mailAttachment.ContentStream = System.IO.File.ReadAllBytes("c:\prodapps\La dll\image2103.jpg")
            mailAttachment.ContentId = "image2103.jpg@01D19732.1B190AC0"
            Dim attachments(0) As CommonWS.SerializableAttachment
            attachments(0) = mailAttachment
            mailMsg.Attachments = attachments

            Using ws As CommonWS.Service = GetCommonWS()
                ws.SendExternalMail(mailMsg)
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function GetCommonWS() As CommonWS.Service
        Dim ws As New CommonWS.Service()
        Dim header As New CommonWS.DBSOAPHeader()
        Dim strUrl As String = ""

        Dim DbHeader As New Utility.Utility.ComHeader
        DbHeader.UserID = gsUser
        DbHeader.CompanyID = g_Comp
        DbHeader.EnvironmentUse = g_Env
        DbHeader.ProjectAlias = "CRS"

        If gUAT = False Then
            DbHeader.ConnectionAlias = "INGCIWPRD01"
        Else
            DbHeader.ConnectionAlias = g_Connection_CIW
        End If

        DbHeader.UserType = "UPDATE"
        DbHeader.UseLocalWS = "N"

        strUrl = Utility.Utility.GetWebServiceURL("COMMONWS", DbHeader, gobjMQQueHeader)
        If strUrl.Trim.Length = 0 Then
            Throw New Exception("UseLocalWS:" & DbHeader.UseLocalWS & ";EnvironmentUse:" & DbHeader.EnvironmentUse)
        Else
            ws.Url = strUrl
        End If
        ws.Credentials = System.Net.CredentialCache.DefaultCredentials

        header.Project = DbHeader.ProjectAlias
        header.ConnectionAlias = DbHeader.ConnectionAlias
        header.UserType = DbHeader.UserType
        header.Comp = DbHeader.CompanyID
        header.Env = DbHeader.EnvironmentUse
        header.User = DbHeader.UserID

        'the default time out value is 2 minutes, change if needed
        ws.Timeout = 180000
        ws.DBSOAPHeaderValue = header
        Return ws
    End Function

    Private Sub btnAuthToPi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthToPi.Click
        Dim bDo As Boolean = True
        Dim bPiMail As Boolean = False
        Dim bBrokerMail As Boolean = False
        Dim SysEventLog As New SysEventLog.clsEventLog
        If Not dgvAuthPi.SelectedRows.Count = 1 Then
            MessageBox.Show("Please select one PI user for Auth")
            Exit Sub
        End If

        If dgvAuthPi.SelectedRows(0).Cells("Auth Date").Value.ToString.Length > 0 Then
            Dim result As Windows.Forms.DialogResult = MessageBox.Show("Auth PI user already, do you want to re-gen Auth OTP?", "Question", MessageBoxButtons.YesNo)
            If result = Windows.Forms.DialogResult.Yes Then
                bDo = True
            Else
                bDo = False
            End If
        End If
        If bDo Then
            If isInForce() Then
                If isCooling() Then
                    sPiID = dgvAuthPi.SelectedRows(0).Cells("PI ID").Value.ToString
                    sPiEmail = dgvAuthPi.SelectedRows(0).Cells("Email").Value.ToString
                    If sPiEmail.Trim.Length > 0 Then
                        'oliver 2024-4-24 updated for Table_Relocate_Sprint13
                        If UpdatePiAuth(strPolicy, sPiID, "Y", gsUser) Then
                            Dim dOtp As System.Collections.Generic.Dictionary(Of String, String)
                            dOtp = getPiAuthOtp()
                            If dOtp("otp").Trim.Length > 0 Then

                                System.IO.File.Delete("C:\temp\OTP_TMP.txt")
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "PH_FirstName : " + dOtp("PH_FirstName") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "PH_NameSuffix : " + dOtp("PH_NameSuffix") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "PI_FirstName : " + dOtp("PI_FirstName") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "PI_NameSuffix : " + dOtp("PI_NameSuffix") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "Product : " + dOtp("Product") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "ProductChi : " + dOtp("ProductChi") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "otp : " + dOtp("otp") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "expiry : " + dOtp("expiry") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "expiryY : " + dOtp("expiryY") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "expiryM : " + dOtp("expiryM") + Environment.NewLine)
                                System.IO.File.AppendAllText("C:\temp\OTP_TMP.txt", "expiryD : " + dOtp("expiryD") + Environment.NewLine)

                                'bPiMail = sendPiMail(dOtp, DecryptData(My.Settings.CS2005_SPAdd))
                                bPiMail = sendPiMail(dOtp, My.Settings.CS2005_SPAdd)
                                'bBrokerMail = sendBrokerMail(dOtp, DecryptData(My.Settings.CS2005_SPAdd))
                                bBrokerMail = sendBrokerMail(dOtp, My.Settings.CS2005_SPAdd)
                                If bPiMail And bBrokerMail Then
                                    MessageBox.Show("PI Auth done")
                                Else
                                    Dim sErrMsg As String = ""
                                    If Not bPiMail Then
                                        sErrMsg = sErrMsg & "Fail send mail to PI" & vbNewLine
                                    End If
                                    If Not bBrokerMail Then
                                        sErrMsg = sErrMsg & "Fail send mail to Broker"
                                    End If
                                    MessageBox.Show(sErrMsg.Trim)
                                End If
                            Else
                                MessageBox.Show("Create OTP fail, please try again later")
                            End If
                        Else
                            MessageBox.Show("Update Auth record fail, please try again later")
                        End If
                    Else
                        MessageBox.Show("PI Email address not valid")
                    End If
                Else
                    MessageBox.Show("Policy is not ready yet")
                End If
            Else
                MessageBox.Show("It is not InForce policy")
            End If
        End If
    End Sub

    Private Sub dgSrvLog_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles dgSrvLog.Navigate

    End Sub
    Private Sub btnDisablePI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisablePI.Click
        Try
            If Not dgvAuthPi.SelectedRows.Count = 1 Then
                MessageBox.Show("Please select one PI user for Disable")
                Exit Sub
            End If

            Dim sPiID = dgvAuthPi.SelectedRows(0).Cells("PI ID").Value.ToString
            'oliver 2024-4-24 updated for Table_Relocate_Sprint13
            If Not UpdatePiAuth(strPolicy, sPiID, "N", gsUser) Then
                MessageBox.Show("Disable fail")
                Exit Sub
            End If
            'If Not sqlConn2.State = ConnectionState.Open Then
            '    sqlConn2.ConnectionString = strCIWConn
            '    sqlConn2.Open()
            'End If
            'Dim cmd = New SqlClient.SqlCommand("sp_crs_update_auth_policy_pi", sqlConn2)
            'cmd.Parameters.Add("@PolicyAccountID", SqlDbType.VarChar, 20).Value = IIf(strPolicy Is Nothing, "", strPolicy)
            'cmd.Parameters.Add("@PICustID", SqlDbType.VarChar, 20).Value = sPiID
            'cmd.Parameters.Add("@Enable", SqlDbType.Char, 1).Value = "N"
            'cmd.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = gsUser
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.ExecuteNonQuery()
            'sqlConn2.Close()

            MessageBox.Show("Disable success")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        getPostSalesCallInfo()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        doRefreshPostCallInfo()
        Me.lbl_InforceDate.Visible = True
        Me.lbl_PostCallStatus.Visible = True
        Me.lbl_PostCallCount.Visible = True

        Me.rad_NVCWelcomeCall.Visible = True
        Me.rad_VCPostSalesCall.Visible = True
        Me.rad_ILASPostSalseCall.Visible = True
        Me.rad_SuitabilityMisMatch.Visible = True

        Me.Label5.Visible = True
        Me.Label15.Visible = True
        Me.Label17.Visible = True
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub cbEventTypeDetail_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbEventTypeDetail.SelectionChangeCommitted
        CheckEnableNBMPanel()
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub rbNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbNo.CheckedChanged
        If rbNo.Checked = True Then
            Me.cbRejectReason.Enabled = True
            Me.txtEffectiveDate.Text = "N/A"
            Me.txtFinishDate.Text = "N/A"
            Me.txtNetPremium.Text = "N/A"
            Me.txtCampaignDetails.Text = ""
        End If

    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub rbYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbYes.CheckedChanged
        If rbYes.Checked = True Then
            Me.cbRejectReason.Enabled = False
            If dataTableReturnRetentionCampaignEnquiry IsNot Nothing AndAlso dataTableReturnRetentionCampaignEnquiry.Rows.Count > 0 Then
                Me.txtEffectiveDate.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("EffectiveDate")), ParseDateTimeFormat(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("EffectiveDate").ToString()), "")
                Me.txtFinishDate.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CampaignFinishDate")), ParseDateTimeFormat(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("CampaignFinishDate").ToString()), "")
                Me.txtNetPremium.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("NetPremiumofNextBillingUnderNewCampaign")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("NetPremiumofNextBillingUnderNewCampaign").ToString(), "")
                Me.txtCampaignDetails.Text = If(Not IsDBNull(dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails")), dataTableReturnRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails").ToString(), "")
            End If
        End If
    End Sub

End Class

