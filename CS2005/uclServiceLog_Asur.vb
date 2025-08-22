'C001 - Add MCV Indicator in HK & Macau Service Log Screen - Gopu Kalaimani.

Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text

Public Class uclServiceLog_Asur
    Inherits System.Windows.Forms.UserControl
    Implements ISummaryTabBase

    Private Property _CompanyID As String Implements ISummaryTabBase._CompanyID

    Public Property CompanyID As String Implements ISummaryTabBase.CompanyID
        Get
            Return _CompanyID
        End Get
        Set(value As String)
            If Not value Is Nothing Then
                _CompanyID = value
            End If
        End Set
    End Property

    Private strPolicy As String
    Private strCustID As String
    Private strNewFlag As String
    Private strPolicyType As String
    Private strIDNumber As String 'added at 2023-9-13 by oliver for Customer Level Search Issue
    Private blnIsAgent As Boolean 'added at 2023-9-13 by oliver for Customer Level Search Issue
    Private blnIsParallelMode As Boolean  'added at 2023-9-13 by oliver for Customer Level Search Issue
    Private blnIsBothComp As Boolean  'added at 2023-9-13 by oliver for Customer Level Search Issue
    Private iPrevPosition As Integer
    Private blnIsWithOutIWS As Boolean
    Private blnIsNewMode As Boolean
    Private blnIsCustLevel As Boolean = False

    'oliver 2024-8-6 added for Com 6
    Private blnIsHnwPolicy As Boolean = False
    'Service log enhancement
    ' Track which enquiry tabs have been visited to preserve data
    Private bln1stEnquiryVisited As Boolean = False
    Private bln2ndEnquiryVisited As Boolean = False
    Private bln3rdEnquiryVisited As Boolean = False
    '
    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private blnIsNBMPolicy As Boolean = False
    Private blnIsEnableNBMPolicyPanel As Boolean = False
    Private rejectReasonDropDownListDataTable As DataTable
    Private dataTableReturnRetentionCampaignEnquiry As DataTable

    Dim sqlConn As New SqlConnection
    Dim sqlConn2 As New SqlConnection 'AL20210201 eService PI Access
    Dim da1 As SqlDataAdapter
    Dim dt1 As New DataTable
    'Dim daSrvEvtDet As SqlDataAdapter 'updated at 2023-9-15 by oliver for Customer Level Search Issue
    Private Const COMPANY_NAME_BERMUDA = "ING" 'added at 2023-9-15 by oliver for Customer Level Search Issue
    Private Const COMPANY_NAME_ASSURANCE = "LAC" 'added at 2023-9-15 by oliver for Customer Level Search Issue
    Private Const COMPANY_NAME_ASSURANCE2 = "LAH" 'added at 2023-9-15 by oliver for Customer Level Search Issue
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
    Dim daEvtCatWithOutIWS As SqlDataAdapter
    Dim daEvtTypeWithOutIWS As SqlDataAdapter
    Dim daEvtTypeDetWithOutIWS As SqlDataAdapter
    Dim daMediumWithOutIWS As SqlDataAdapter
    Dim daInitiatorWithOutIWS As SqlDataAdapter
    Dim dsSrvLog As New DataSet
    Dim dsTmp As New DataSet 'AL20210201 eService PI Access
    Dim ServiceLogBL As New ServiceLogBL
    Friend WithEvents SaveFileDialogDownlaod As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents gbPiEservicesAuth As GroupBox
    Friend WithEvents btnDisablePI As Button
    Friend WithEvents dgvAuthPi As DataGridView
    Friend WithEvents btnAuthToPi As Button
    Friend WithEvents gboCustomer As GroupBox
    Friend WithEvents txtCustomerID As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents chkMCV As CheckBox
    Friend WithEvents rad_SuitabilityMisMatch As RadioButton
    Friend WithEvents gboPolicy As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtPolicyNo As TextBox
    Friend WithEvents lblPolicyNo As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rbEB As RadioButton
    Friend WithEvents rbGI As RadioButton
    Friend WithEvents rbBer As RadioButton
    Friend WithEvents rad_ILASPostSalseCall As RadioButton
    Friend WithEvents rad_VCPostSalesCall As RadioButton
    Friend WithEvents rad_NVCWelcomeCall As RadioButton
    Friend WithEvents lbl_PostCallCount As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lbl_PostCallStatus As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lbl_InforceDate As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents chkACC As CheckBox
    Friend WithEvents chkFCR As CheckBox
    Friend WithEvents btnSaveC As Button
    Friend WithEvents txtDownloadPostSalesCall As Button
    Friend WithEvents hidSender As TextBox
    Friend WithEvents hidCustID As TextBox
    Friend WithEvents hidPolicy As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cbStatus As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents dtInitial As DateTimePicker
    Friend WithEvents cbReceiver As ComboBox
    Friend WithEvents cbInitiator As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents lbReceiver As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents chkAES As CheckBox
    Friend WithEvents dgSrvLog As DataGrid
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents grpServiceEvent As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cbEventCat As ComboBox
    Friend WithEvents cbEventDetail As ComboBox
    Friend WithEvents cbEventTypeDetail As ComboBox
    Friend WithEvents cbMedium As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkIdVerify As CheckBox
    Friend WithEvents dtReminder As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents chkReminder As CheckBox
    Friend WithEvents chkPolicyAlert As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents txtReminder As TextBox
    Friend WithEvents txtPolicyAlert As TextBox
    Friend WithEvents btncustlogweb As Button
    Friend WithEvents hidSrvEvtNo As TextBox
    Friend WithEvents rbAsur As RadioButton
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
    ' Service Log enhancement
    Friend WithEvents tabEnquiry As TabControl
    Friend WithEvents tab1stEnquiry As TabPage
    Friend WithEvents tab2ndEnquiry As TabPage
    Friend WithEvents tab3rdEnquiry As TabPage
    Friend WithEvents cb1stEventCat As ComboBox
    Friend WithEvents cb1stEventDetail As ComboBox
    Friend WithEvents cb1stEventTypeDetail As ComboBox
    Friend WithEvents cb2ndEventCat As ComboBox
    Friend WithEvents cb2ndEventDetail As ComboBox
    Friend WithEvents cb2ndEventTypeDetail As ComboBox
    Friend WithEvents cb3rdEventCat As ComboBox
    Friend WithEvents cb3rdEventDetail As ComboBox
    Friend WithEvents cb3rdEventTypeDetail As ComboBox
    Friend WithEvents txt1stReason As TextBox
    Friend WithEvents txt1stAlternative As TextBox
    Friend WithEvents txt2ndReason As TextBox
    Friend WithEvents txt2ndAlternative As TextBox
    Friend WithEvents txt3rdReason As TextBox
    Friend WithEvents txt3rdAlternative As TextBox
    Friend WithEvents lbl1stEventCat As Label
    Friend WithEvents lbl1stEventDetail As Label
    Friend WithEvents lbl1stEventTypeDetail As Label
    Friend WithEvents lbl1stReason As Label
    Friend WithEvents lbl1stAlternative As Label
    Friend WithEvents lbl2ndEventCat As Label
    Friend WithEvents lbl2ndEventDetail As Label
    Friend WithEvents lbl2ndEventTypeDetail As Label
    Friend WithEvents lbl2ndReason As Label
    Friend WithEvents lbl2ndAlternative As Label
    Friend WithEvents lbl3rdEventCat As Label
    Friend WithEvents lbl3rdEventDetail As Label
    Friend WithEvents lbl3rdEventTypeDetail As Label
    Friend WithEvents lbl3rdReason As Label
    Friend WithEvents lbl3rdAlternative As Label
    '
    Dim bm As BindingManagerBase

    Public Delegate Sub EventSavedEventHandler(ByVal sender As Object, ByVal e As DataRow)

    Public Event EventSaved As EventSavedEventHandler

#Region " Windows Form Designer generated code "
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.gbPiEservicesAuth = New System.Windows.Forms.GroupBox()
        Me.btnDisablePI = New System.Windows.Forms.Button()
        Me.dgvAuthPi = New System.Windows.Forms.DataGridView()
        Me.btnAuthToPi = New System.Windows.Forms.Button()
        Me.gboCustomer = New System.Windows.Forms.GroupBox()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkMCV = New System.Windows.Forms.CheckBox()
        Me.rad_SuitabilityMisMatch = New System.Windows.Forms.RadioButton()
        Me.gboPolicy = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbAsur = New System.Windows.Forms.RadioButton()
        Me.rbEB = New System.Windows.Forms.RadioButton()
        Me.rbGI = New System.Windows.Forms.RadioButton()
        Me.rbBer = New System.Windows.Forms.RadioButton()
        Me.rad_ILASPostSalseCall = New System.Windows.Forms.RadioButton()
        Me.rad_VCPostSalesCall = New System.Windows.Forms.RadioButton()
        Me.rad_NVCWelcomeCall = New System.Windows.Forms.RadioButton()
        Me.lbl_PostCallCount = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lbl_PostCallStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbl_InforceDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkACC = New System.Windows.Forms.CheckBox()
        Me.chkFCR = New System.Windows.Forms.CheckBox()
        Me.btnSaveC = New System.Windows.Forms.Button()
        Me.txtDownloadPostSalesCall = New System.Windows.Forms.Button()
        Me.hidSender = New System.Windows.Forms.TextBox()
        Me.hidCustID = New System.Windows.Forms.TextBox()
        Me.hidPolicy = New System.Windows.Forms.TextBox()
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkIdVerify = New System.Windows.Forms.CheckBox()
        Me.dtReminder = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkReminder = New System.Windows.Forms.CheckBox()
        Me.chkPolicyAlert = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtReminder = New System.Windows.Forms.TextBox()
        Me.txtPolicyAlert = New System.Windows.Forms.TextBox()
        Me.btncustlogweb = New System.Windows.Forms.Button()
        Me.hidSrvEvtNo = New System.Windows.Forms.TextBox()
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
        Me.gbPiEservicesAuth.SuspendLayout()
        CType(Me.dgvAuthPi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboCustomer.SuspendLayout()
        Me.gboPolicy.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpServiceEvent.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbNBM.SuspendLayout()
        Me.palYesNo.SuspendLayout()
        Me.SuspendLayout()
        '
        'Service Log enhancement
        Me.tabEnquiry = New System.Windows.Forms.TabControl()
        Me.tab1stEnquiry = New System.Windows.Forms.TabPage()
        Me.tab2ndEnquiry = New System.Windows.Forms.TabPage()
        Me.tab3rdEnquiry = New System.Windows.Forms.TabPage()
        Me.cb1stEventCat = New System.Windows.Forms.ComboBox()
        Me.cb1stEventDetail = New System.Windows.Forms.ComboBox()
        Me.cb1stEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.cb2ndEventCat = New System.Windows.Forms.ComboBox()
        Me.cb2ndEventDetail = New System.Windows.Forms.ComboBox()
        Me.cb2ndEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.cb3rdEventCat = New System.Windows.Forms.ComboBox()
        Me.cb3rdEventDetail = New System.Windows.Forms.ComboBox()
        Me.cb3rdEventTypeDetail = New System.Windows.Forms.ComboBox()
        Me.txt1stReason = New System.Windows.Forms.TextBox()
        Me.txt1stAlternative = New System.Windows.Forms.TextBox()
        Me.txt2ndReason = New System.Windows.Forms.TextBox()
        Me.txt2ndAlternative = New System.Windows.Forms.TextBox()
        Me.txt3rdReason = New System.Windows.Forms.TextBox()
        Me.txt3rdAlternative = New System.Windows.Forms.TextBox()
        Me.lbl1stEventCat = New System.Windows.Forms.Label()
        Me.lbl1stEventDetail = New System.Windows.Forms.Label()
        Me.lbl1stEventTypeDetail = New System.Windows.Forms.Label()
        Me.lbl1stReason = New System.Windows.Forms.Label()
        Me.lbl1stAlternative = New System.Windows.Forms.Label()
        Me.lbl2ndEventCat = New System.Windows.Forms.Label()
        Me.lbl2ndEventDetail = New System.Windows.Forms.Label()
        Me.lbl2ndEventTypeDetail = New System.Windows.Forms.Label()
        Me.lbl2ndReason = New System.Windows.Forms.Label()
        Me.lbl2ndAlternative = New System.Windows.Forms.Label()
        Me.lbl3rdEventCat = New System.Windows.Forms.Label()
        Me.lbl3rdEventDetail = New System.Windows.Forms.Label()
        Me.lbl3rdEventTypeDetail = New System.Windows.Forms.Label()
        Me.lbl3rdReason = New System.Windows.Forms.Label()
        Me.lbl3rdAlternative = New System.Windows.Forms.Label()
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'gbPiEservicesAuth
        '
        Me.gbPiEservicesAuth.Controls.Add(Me.btnDisablePI)
        Me.gbPiEservicesAuth.Controls.Add(Me.dgvAuthPi)
        Me.gbPiEservicesAuth.Controls.Add(Me.btnAuthToPi)
        Me.gbPiEservicesAuth.Location = New System.Drawing.Point(574, 668)
        Me.gbPiEservicesAuth.Name = "gbPiEservicesAuth"
        Me.gbPiEservicesAuth.Size = New System.Drawing.Size(839, 97)
        Me.gbPiEservicesAuth.TabIndex = 105
        Me.gbPiEservicesAuth.TabStop = False
        Me.gbPiEservicesAuth.Text = "PI eServices Auth"
        '
        'btnDisablePI
        '
        Me.btnDisablePI.Enabled = False
        Me.btnDisablePI.Location = New System.Drawing.Point(727, 25)
        Me.btnDisablePI.Name = "btnDisablePI"
        Me.btnDisablePI.Size = New System.Drawing.Size(100, 30)
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
        Me.dgvAuthPi.Size = New System.Drawing.Size(707, 67)
        Me.dgvAuthPi.TabIndex = 4
        '
        'btnAuthToPi
        '
        Me.btnAuthToPi.Enabled = False
        Me.btnAuthToPi.Location = New System.Drawing.Point(727, 61)
        Me.btnAuthToPi.Name = "btnAuthToPi"
        Me.btnAuthToPi.Size = New System.Drawing.Size(100, 30)
        Me.btnAuthToPi.TabIndex = 3
        Me.btnAuthToPi.Text = "Auth to PI"
        Me.btnAuthToPi.UseVisualStyleBackColor = True
        '
        'gboCustomer
        '
        Me.gboCustomer.Controls.Add(Me.txtCustomerID)
        Me.gboCustomer.Controls.Add(Me.Label14)
        Me.gboCustomer.Controls.Add(Me.txtCustomerName)
        Me.gboCustomer.Controls.Add(Me.Label16)
        Me.gboCustomer.Location = New System.Drawing.Point(7, 668)
        Me.gboCustomer.Name = "gboCustomer"
        Me.gboCustomer.Size = New System.Drawing.Size(559, 82)
        Me.gboCustomer.TabIndex = 104
        Me.gboCustomer.TabStop = False
        Me.gboCustomer.Text = "Customer"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerID.Location = New System.Drawing.Point(87, 15)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(464, 20)
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
        Me.txtCustomerName.Size = New System.Drawing.Size(464, 20)
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
        'chkMCV
        '
        Me.chkMCV.AutoSize = True
        Me.chkMCV.BackColor = System.Drawing.Color.Transparent
        Me.chkMCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkMCV.ForeColor = System.Drawing.Color.Red
        Me.chkMCV.Location = New System.Drawing.Point(1421, 256)
        Me.chkMCV.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMCV.Name = "chkMCV"
        Me.chkMCV.Size = New System.Drawing.Size(59, 21)
        Me.chkMCV.TabIndex = 102
        Me.chkMCV.Text = "MCV"
        Me.chkMCV.UseVisualStyleBackColor = False
        '
        'rad_SuitabilityMisMatch
        '
        Me.rad_SuitabilityMisMatch.AutoCheck = False
        Me.rad_SuitabilityMisMatch.AutoSize = True
        Me.rad_SuitabilityMisMatch.Location = New System.Drawing.Point(1419, 419)
        Me.rad_SuitabilityMisMatch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_SuitabilityMisMatch.Name = "rad_SuitabilityMisMatch"
        Me.rad_SuitabilityMisMatch.Size = New System.Drawing.Size(120, 17)
        Me.rad_SuitabilityMisMatch.TabIndex = 101
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
        Me.gboPolicy.Location = New System.Drawing.Point(7, 546)
        Me.gboPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboPolicy.Name = "gboPolicy"
        Me.gboPolicy.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboPolicy.Size = New System.Drawing.Size(559, 114)
        Me.gboPolicy.TabIndex = 100
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
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.Location = New System.Drawing.Point(118, 72)
        Me.txtPolicyNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(208, 20)
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
        Me.Panel1.Controls.Add(Me.rbAsur)
        Me.Panel1.Controls.Add(Me.rbEB)
        Me.Panel1.Controls.Add(Me.rbGI)
        Me.Panel1.Controls.Add(Me.rbBer)
        Me.Panel1.Location = New System.Drawing.Point(117, 19)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(357, 38)
        Me.Panel1.TabIndex = 43
        '
        'rbAsur
        '
        Me.rbAsur.AutoSize = True
        Me.rbAsur.Location = New System.Drawing.Point(111, 8)
        Me.rbAsur.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbAsur.Name = "rbAsur"
        Me.rbAsur.Size = New System.Drawing.Size(75, 17)
        Me.rbAsur.TabIndex = 17
        Me.rbAsur.Text = "Assurance"
        Me.rbAsur.UseVisualStyleBackColor = True
        '
        'rbEB
        '
        Me.rbEB.AutoSize = True
        Me.rbEB.Location = New System.Drawing.Point(289, 8)
        Me.rbEB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbEB.Name = "rbEB"
        Me.rbEB.Size = New System.Drawing.Size(39, 17)
        Me.rbEB.TabIndex = 16
        Me.rbEB.Text = "EB"
        Me.rbEB.UseVisualStyleBackColor = True
        '
        'rbGI
        '
        Me.rbGI.AutoSize = True
        Me.rbGI.Location = New System.Drawing.Point(229, 8)
        Me.rbGI.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbGI.Name = "rbGI"
        Me.rbGI.Size = New System.Drawing.Size(36, 17)
        Me.rbGI.TabIndex = 15
        Me.rbGI.Text = "GI"
        Me.rbGI.UseVisualStyleBackColor = True
        '
        'rbBer
        '
        Me.rbBer.AutoSize = True
        Me.rbBer.Checked = True
        Me.rbBer.Location = New System.Drawing.Point(4, 8)
        Me.rbBer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbBer.Name = "rbBer"
        Me.rbBer.Size = New System.Drawing.Size(67, 17)
        Me.rbBer.TabIndex = 14
        Me.rbBer.TabStop = True
        Me.rbBer.Text = "Bermuda"
        Me.rbBer.UseVisualStyleBackColor = True
        '
        'rad_ILASPostSalseCall
        '
        Me.rad_ILASPostSalseCall.AutoCheck = False
        Me.rad_ILASPostSalseCall.AutoSize = True
        Me.rad_ILASPostSalseCall.Location = New System.Drawing.Point(1419, 491)
        Me.rad_ILASPostSalseCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_ILASPostSalseCall.Name = "rad_ILASPostSalseCall"
        Me.rad_ILASPostSalseCall.Size = New System.Drawing.Size(121, 17)
        Me.rad_ILASPostSalseCall.TabIndex = 99
        Me.rad_ILASPostSalseCall.TabStop = True
        Me.rad_ILASPostSalseCall.Text = "ILAS Post-Sales Call"
        Me.rad_ILASPostSalseCall.UseVisualStyleBackColor = True
        Me.rad_ILASPostSalseCall.Visible = False
        '
        'rad_VCPostSalesCall
        '
        Me.rad_VCPostSalesCall.AutoCheck = False
        Me.rad_VCPostSalesCall.AutoSize = True
        Me.rad_VCPostSalesCall.Location = New System.Drawing.Point(1419, 467)
        Me.rad_VCPostSalesCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_VCPostSalesCall.Name = "rad_VCPostSalesCall"
        Me.rad_VCPostSalesCall.Size = New System.Drawing.Size(118, 17)
        Me.rad_VCPostSalesCall.TabIndex = 98
        Me.rad_VCPostSalesCall.TabStop = True
        Me.rad_VCPostSalesCall.Text = "Post-Sales Call - VC"
        Me.rad_VCPostSalesCall.UseVisualStyleBackColor = True
        Me.rad_VCPostSalesCall.Visible = False
        '
        'rad_NVCWelcomeCall
        '
        Me.rad_NVCWelcomeCall.AutoCheck = False
        Me.rad_NVCWelcomeCall.AutoSize = True
        Me.rad_NVCWelcomeCall.Location = New System.Drawing.Point(1419, 442)
        Me.rad_NVCWelcomeCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rad_NVCWelcomeCall.Name = "rad_NVCWelcomeCall"
        Me.rad_NVCWelcomeCall.Size = New System.Drawing.Size(133, 17)
        Me.rad_NVCWelcomeCall.TabIndex = 97
        Me.rad_NVCWelcomeCall.TabStop = True
        Me.rad_NVCWelcomeCall.Text = "Welcome Call - NonVC"
        Me.rad_NVCWelcomeCall.UseVisualStyleBackColor = True
        Me.rad_NVCWelcomeCall.Visible = False
        '
        'lbl_PostCallCount
        '
        Me.lbl_PostCallCount.AutoSize = True
        Me.lbl_PostCallCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallCount.Location = New System.Drawing.Point(1555, 568)
        Me.lbl_PostCallCount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PostCallCount.Name = "lbl_PostCallCount"
        Me.lbl_PostCallCount.Size = New System.Drawing.Size(55, 13)
        Me.lbl_PostCallCount.TabIndex = 96
        Me.lbl_PostCallCount.Text = "0 time(s)"
        Me.lbl_PostCallCount.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1415, 568)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 13)
        Me.Label17.TabIndex = 95
        Me.Label17.Text = "Post Call Times :"
        Me.Label17.Visible = False
        '
        'lbl_PostCallStatus
        '
        Me.lbl_PostCallStatus.AutoSize = True
        Me.lbl_PostCallStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PostCallStatus.Location = New System.Drawing.Point(1555, 545)
        Me.lbl_PostCallStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PostCallStatus.Name = "lbl_PostCallStatus"
        Me.lbl_PostCallStatus.Size = New System.Drawing.Size(77, 13)
        Me.lbl_PostCallStatus.TabIndex = 94
        Me.lbl_PostCallStatus.Text = "InCompleted"
        Me.lbl_PostCallStatus.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1415, 545)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 93
        Me.Label15.Text = "Post Call Status :"
        Me.Label15.Visible = False
        '
        'lbl_InforceDate
        '
        Me.lbl_InforceDate.AutoSize = True
        Me.lbl_InforceDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InforceDate.Location = New System.Drawing.Point(1553, 522)
        Me.lbl_InforceDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_InforceDate.Name = "lbl_InforceDate"
        Me.lbl_InforceDate.Size = New System.Drawing.Size(78, 13)
        Me.lbl_InforceDate.TabIndex = 92
        Me.lbl_InforceDate.Text = "01 Mar 2016"
        Me.lbl_InforceDate.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1415, 522)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "Inforce Date :"
        Me.Label5.Visible = False
        '
        'chkACC
        '
        Me.chkACC.BackColor = System.Drawing.Color.Transparent
        Me.chkACC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkACC.ForeColor = System.Drawing.Color.Indigo
        Me.chkACC.Location = New System.Drawing.Point(1421, 248)
        Me.chkACC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkACC.Name = "chkACC"
        Me.chkACC.Size = New System.Drawing.Size(189, 120)
        Me.chkACC.TabIndex = 90
        Me.chkACC.Text = "Suggestions/Grievances"
        Me.chkACC.UseVisualStyleBackColor = False
        '
        'chkFCR
        '
        Me.chkFCR.AutoSize = True
        Me.chkFCR.BackColor = System.Drawing.Color.Transparent
        Me.chkFCR.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkFCR.ForeColor = System.Drawing.Color.Indigo
        Me.chkFCR.Location = New System.Drawing.Point(1421, 224)
        Me.chkFCR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkFCR.Name = "chkFCR"
        Me.chkFCR.Size = New System.Drawing.Size(57, 21)
        Me.chkFCR.TabIndex = 89
        Me.chkFCR.Text = "FCR"
        Me.chkFCR.UseVisualStyleBackColor = False
        '
        'btnSaveC
        '
        Me.btnSaveC.Location = New System.Drawing.Point(1421, 153)
        Me.btnSaveC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSaveC.Name = "btnSaveC"
        Me.btnSaveC.Size = New System.Drawing.Size(96, 58)
        Me.btnSaveC.TabIndex = 88
        Me.btnSaveC.Text = "&Save && Close"
        '
        'txtDownloadPostSalesCall
        '
        Me.txtDownloadPostSalesCall.Location = New System.Drawing.Point(1421, 378)
        Me.txtDownloadPostSalesCall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDownloadPostSalesCall.Name = "txtDownloadPostSalesCall"
        Me.txtDownloadPostSalesCall.Size = New System.Drawing.Size(230, 32)
        Me.txtDownloadPostSalesCall.TabIndex = 87
        Me.txtDownloadPostSalesCall.Text = "Button1"
        Me.txtDownloadPostSalesCall.Visible = False
        '
        'hidSender
        '
        Me.hidSender.Location = New System.Drawing.Point(1421, 288)
        Me.hidSender.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidSender.Name = "hidSender"
        Me.hidSender.Size = New System.Drawing.Size(100, 20)
        Me.hidSender.TabIndex = 86
        Me.hidSender.Visible = False
        '
        'hidCustID
        '
        Me.hidCustID.Location = New System.Drawing.Point(1415, 244)
        Me.hidCustID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidCustID.Name = "hidCustID"
        Me.hidCustID.Size = New System.Drawing.Size(100, 20)
        Me.hidCustID.TabIndex = 85
        Me.hidCustID.Visible = False
        '
        'hidPolicy
        '
        Me.hidPolicy.Location = New System.Drawing.Point(1421, 221)
        Me.hidPolicy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidPolicy.Name = "hidPolicy"
        Me.hidPolicy.Size = New System.Drawing.Size(100, 20)
        Me.hidPolicy.TabIndex = 84
        Me.hidPolicy.Visible = False
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
        'Me.GroupBox2.Location = New System.Drawing.Point(7, 368)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 418)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(559, 172)
        Me.GroupBox2.TabIndex = 83
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Status"
        '
        'cbStatus
        '
        Me.cbStatus.BackColor = System.Drawing.Color.White
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.Location = New System.Drawing.Point(108, 22)
        Me.cbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(279, 21)
        Me.cbStatus.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(18, 28)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 25)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Status"
        '
        'dtInitial
        '
        Me.dtInitial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInitial.Location = New System.Drawing.Point(108, 133)
        Me.dtInitial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtInitial.Name = "dtInitial"
        Me.dtInitial.Size = New System.Drawing.Size(244, 20)
        Me.dtInitial.TabIndex = 8
        '
        'cbReceiver
        '
        Me.cbReceiver.BackColor = System.Drawing.Color.White
        Me.cbReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReceiver.Enabled = False
        Me.cbReceiver.Location = New System.Drawing.Point(108, 58)
        Me.cbReceiver.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbReceiver.Name = "cbReceiver"
        Me.cbReceiver.Size = New System.Drawing.Size(443, 21)
        Me.cbReceiver.TabIndex = 6
        '
        'cbInitiator
        '
        Me.cbInitiator.BackColor = System.Drawing.Color.White
        Me.cbInitiator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInitiator.Location = New System.Drawing.Point(108, 94)
        Me.cbInitiator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbInitiator.Name = "cbInitiator"
        Me.cbInitiator.Size = New System.Drawing.Size(443, 21)
        Me.cbInitiator.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(18, 139)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 25)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Initial Date"
        '
        'lbReceiver
        '
        Me.lbReceiver.Enabled = False
        Me.lbReceiver.Location = New System.Drawing.Point(18, 61)
        Me.lbReceiver.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbReceiver.Name = "lbReceiver"
        Me.lbReceiver.Size = New System.Drawing.Size(78, 25)
        Me.lbReceiver.TabIndex = 15
        Me.lbReceiver.Text = "Reciever"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(18, 100)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 25)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Initiator"
        '
        'chkAES
        '
        Me.chkAES.Enabled = False
        Me.chkAES.Location = New System.Drawing.Point(395, 22)
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
        Me.dgSrvLog.Location = New System.Drawing.Point(4, 5)
        Me.dgSrvLog.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgSrvLog.Name = "dgSrvLog"
        Me.dgSrvLog.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgSrvLog.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgSrvLog.ReadOnly = True
        Me.dgSrvLog.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgSrvLog.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgSrvLog.Size = New System.Drawing.Size(1289, 166)
        Me.dgSrvLog.TabIndex = 81
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(1421, 55)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 35)
        Me.btnSave.TabIndex = 80
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(1421, 104)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 35)
        Me.btnCancel.TabIndex = 79
        Me.btnCancel.Text = "&Cancel"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1421, 5)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(96, 35)
        Me.btnNew.TabIndex = 77
        Me.btnNew.Text = "&New"
        '
        'grpServiceEvent
        '
        'Service Log enhancement
        Me.grpServiceEvent.Controls.Add(Me.tabEnquiry)
        '
        'Me.grpServiceEvent.Controls.Add(Me.Label8)
        Me.grpServiceEvent.Controls.Add(Me.Label6)
        'Me.grpServiceEvent.Controls.Add(Me.cbEventCat)
        'Me.grpServiceEvent.Controls.Add(Me.cbEventDetail)
        'Me.grpServiceEvent.Controls.Add(Me.cbEventTypeDetail)
        Me.grpServiceEvent.Controls.Add(Me.cbMedium)
        'Me.grpServiceEvent.Controls.Add(Me.Label7)
        'Me.grpServiceEvent.Controls.Add(Me.Label9)
        Me.grpServiceEvent.Location = New System.Drawing.Point(7, 184)
        Me.grpServiceEvent.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpServiceEvent.Name = "grpServiceEvent"
        Me.grpServiceEvent.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        'Me.grpServiceEvent.Size = New System.Drawing.Size(559, 180)
        Me.grpServiceEvent.Size = New System.Drawing.Size(559, 350)
        Me.grpServiceEvent.TabIndex = 76
        Me.grpServiceEvent.TabStop = False
        Me.grpServiceEvent.Text = "Service Event"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(18, 106)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 25)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Event Detail"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(18, 32)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 25)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Medium"
        '
        'cbEventCat
        '
        Me.cbEventCat.BackColor = System.Drawing.Color.White
        Me.cbEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventCat.Location = New System.Drawing.Point(156, 63)
        Me.cbEventCat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventCat.Name = "cbEventCat"
        Me.cbEventCat.Size = New System.Drawing.Size(395, 21)
        Me.cbEventCat.TabIndex = 1
        '
        'cbEventDetail
        '
        Me.cbEventDetail.BackColor = System.Drawing.Color.White
        Me.cbEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventDetail.Location = New System.Drawing.Point(156, 100)
        Me.cbEventDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventDetail.Name = "cbEventDetail"
        Me.cbEventDetail.Size = New System.Drawing.Size(395, 21)
        Me.cbEventDetail.TabIndex = 2
        '
        'cbEventTypeDetail
        '
        Me.cbEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cbEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEventTypeDetail.Location = New System.Drawing.Point(156, 137)
        Me.cbEventTypeDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbEventTypeDetail.Name = "cbEventTypeDetail"
        Me.cbEventTypeDetail.Size = New System.Drawing.Size(395, 21)
        Me.cbEventTypeDetail.TabIndex = 3
        '
        'cbMedium
        '
        Me.cbMedium.BackColor = System.Drawing.Color.White
        Me.cbMedium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMedium.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbMedium.Location = New System.Drawing.Point(156, 26)
        Me.cbMedium.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbMedium.Name = "cbMedium"
        Me.cbMedium.Size = New System.Drawing.Size(395, 21)
        Me.cbMedium.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(18, 69)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(126, 31)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Event Category"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(18, 143)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 25)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Event Type Detail"
        '
        'Service log enhancement
        'tabEnquiry
        '
        Me.tabEnquiry.Controls.Add(Me.tab1stEnquiry)
        Me.tabEnquiry.Controls.Add(Me.tab2ndEnquiry)
        Me.tabEnquiry.Controls.Add(Me.tab3rdEnquiry)
        Me.tabEnquiry.Location = New System.Drawing.Point(18, 60)
        Me.tabEnquiry.Name = "tabEnquiry"
        Me.tabEnquiry.SelectedIndex = 0
        'Me.tabEnquiry.Size = New System.Drawing.Size(533, 220)
        Me.tabEnquiry.Size = New System.Drawing.Size(533, 270)
        Me.tabEnquiry.TabIndex = 11
        '
        'tab1stEnquiry
        '
        Me.tab1stEnquiry.Controls.Add(Me.lbl1stEventCat)
        Me.tab1stEnquiry.Controls.Add(Me.cb1stEventCat)
        Me.tab1stEnquiry.Controls.Add(Me.lbl1stEventDetail)
        Me.tab1stEnquiry.Controls.Add(Me.cb1stEventDetail)
        Me.tab1stEnquiry.Controls.Add(Me.lbl1stEventTypeDetail)
        Me.tab1stEnquiry.Controls.Add(Me.cb1stEventTypeDetail)
        Me.tab1stEnquiry.Controls.Add(Me.lbl1stReason)
        Me.tab1stEnquiry.Controls.Add(Me.txt1stReason)
        Me.tab1stEnquiry.Controls.Add(Me.lbl1stAlternative)
        Me.tab1stEnquiry.Controls.Add(Me.txt1stAlternative)
        Me.tab1stEnquiry.Location = New System.Drawing.Point(4, 22)
        Me.tab1stEnquiry.Name = "tab1stEnquiry"
        Me.tab1stEnquiry.Padding = New System.Windows.Forms.Padding(3)
        'Me.tab1stEnquiry.Size = New System.Drawing.Size(525, 194)
        Me.tab1stEnquiry.Size = New System.Drawing.Size(525, 244)
        Me.tab1stEnquiry.TabIndex = 0
        Me.tab1stEnquiry.Text = "1st Enquiry"
        Me.tab1stEnquiry.UseVisualStyleBackColor = True
        '
        'lbl1stEventCat
        '
        Me.lbl1stEventCat.Location = New System.Drawing.Point(6, 16)
        Me.lbl1stEventCat.Name = "lbl1stEventCat"
        Me.lbl1stEventCat.Size = New System.Drawing.Size(100, 23)
        Me.lbl1stEventCat.TabIndex = 0
        Me.lbl1stEventCat.Text = "Event Category"
        '
        'cb1stEventCat
        '
        Me.cb1stEventCat.BackColor = System.Drawing.Color.White
        Me.cb1stEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb1stEventCat.Location = New System.Drawing.Point(120, 13)
        Me.cb1stEventCat.Name = "cb1stEventCat"
        Me.cb1stEventCat.Size = New System.Drawing.Size(395, 21)
        Me.cb1stEventCat.TabIndex = 1
        '
        'lbl1stEventDetail
        '
        Me.lbl1stEventDetail.Location = New System.Drawing.Point(6, 43)
        Me.lbl1stEventDetail.Name = "lbl1stEventDetail"
        Me.lbl1stEventDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl1stEventDetail.TabIndex = 2
        Me.lbl1stEventDetail.Text = "Event Detail"
        '
        'cb1stEventDetail
        '
        Me.cb1stEventDetail.BackColor = System.Drawing.Color.White
        Me.cb1stEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb1stEventDetail.Location = New System.Drawing.Point(120, 40)
        Me.cb1stEventDetail.Name = "cb1stEventDetail"
        Me.cb1stEventDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb1stEventDetail.TabIndex = 3
        '
        'lbl1stEventTypeDetail
        '
        Me.lbl1stEventTypeDetail.Location = New System.Drawing.Point(6, 70)
        Me.lbl1stEventTypeDetail.Name = "lbl1stEventTypeDetail"
        Me.lbl1stEventTypeDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl1stEventTypeDetail.TabIndex = 4
        Me.lbl1stEventTypeDetail.Text = "Event Type Detail"
        '
        'cb1stEventTypeDetail
        '
        Me.cb1stEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cb1stEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb1stEventTypeDetail.Location = New System.Drawing.Point(120, 67)
        Me.cb1stEventTypeDetail.Name = "cb1stEventTypeDetail"
        Me.cb1stEventTypeDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb1stEventTypeDetail.TabIndex = 5
        '
        'lbl1stReason
        '
        Me.lbl1stReason.Location = New System.Drawing.Point(6, 97)
        Me.lbl1stReason.Name = "lbl1stReason"
        Me.lbl1stReason.Size = New System.Drawing.Size(100, 23)
        Me.lbl1stReason.TabIndex = 6
        Me.lbl1stReason.Text = "Reason"
        '
        'txt1stReason
        '
        Me.txt1stReason.Location = New System.Drawing.Point(120, 94)
        Me.txt1stReason.Name = "txt1stReason"
        Me.txt1stReason.Size = New System.Drawing.Size(395, 20)
        Me.txt1stReason.TabIndex = 7
        '
        'lbl1stAlternative
        '
        Me.lbl1stAlternative.Location = New System.Drawing.Point(6, 124)
        Me.lbl1stAlternative.Name = "lbl1stAlternative"
        Me.lbl1stAlternative.Size = New System.Drawing.Size(100, 23)
        Me.lbl1stAlternative.TabIndex = 8
        Me.lbl1stAlternative.Text = "Alternative"
        '
        'txt1stAlternative
        '
        Me.txt1stAlternative.Location = New System.Drawing.Point(120, 121)
        Me.txt1stAlternative.Name = "txt1stAlternative"
        Me.txt1stAlternative.Size = New System.Drawing.Size(395, 20)
        Me.txt1stAlternative.TabIndex = 9
        '
        'tab2ndEnquiry
        '
        Me.tab2ndEnquiry.Controls.Add(Me.lbl2ndEventCat)
        Me.tab2ndEnquiry.Controls.Add(Me.cb2ndEventCat)
        Me.tab2ndEnquiry.Controls.Add(Me.lbl2ndEventDetail)
        Me.tab2ndEnquiry.Controls.Add(Me.cb2ndEventDetail)
        Me.tab2ndEnquiry.Controls.Add(Me.lbl2ndEventTypeDetail)
        Me.tab2ndEnquiry.Controls.Add(Me.cb2ndEventTypeDetail)
        Me.tab2ndEnquiry.Controls.Add(Me.lbl2ndReason)
        Me.tab2ndEnquiry.Controls.Add(Me.txt2ndReason)
        Me.tab2ndEnquiry.Controls.Add(Me.lbl2ndAlternative)
        Me.tab2ndEnquiry.Controls.Add(Me.txt2ndAlternative)
        Me.tab2ndEnquiry.Location = New System.Drawing.Point(4, 22)
        Me.tab2ndEnquiry.Name = "tab2ndEnquiry"
        Me.tab2ndEnquiry.Padding = New System.Windows.Forms.Padding(3)
        'Me.tab2ndEnquiry.Size = New System.Drawing.Size(525, 194)
        Me.tab2ndEnquiry.Size = New System.Drawing.Size(525, 244)
        Me.tab2ndEnquiry.TabIndex = 1
        Me.tab2ndEnquiry.Text = "2nd Enquiry"
        Me.tab2ndEnquiry.UseVisualStyleBackColor = True
        '
        'lbl2ndEventCat
        '
        Me.lbl2ndEventCat.Location = New System.Drawing.Point(6, 16)
        Me.lbl2ndEventCat.Name = "lbl2ndEventCat"
        Me.lbl2ndEventCat.Size = New System.Drawing.Size(100, 23)
        Me.lbl2ndEventCat.TabIndex = 0
        Me.lbl2ndEventCat.Text = "Event Category"
        '
        'cb2ndEventCat
        '
        Me.cb2ndEventCat.BackColor = System.Drawing.Color.White
        Me.cb2ndEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb2ndEventCat.Location = New System.Drawing.Point(120, 13)
        Me.cb2ndEventCat.Name = "cb2ndEventCat"
        Me.cb2ndEventCat.Size = New System.Drawing.Size(395, 21)
        Me.cb2ndEventCat.TabIndex = 1
        '
        'lbl2ndEventDetail
        '
        Me.lbl2ndEventDetail.Location = New System.Drawing.Point(6, 43)
        Me.lbl2ndEventDetail.Name = "lbl2ndEventDetail"
        Me.lbl2ndEventDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl2ndEventDetail.TabIndex = 2
        Me.lbl2ndEventDetail.Text = "Event Detail"
        '
        'cb2ndEventDetail
        '
        Me.cb2ndEventDetail.BackColor = System.Drawing.Color.White
        Me.cb2ndEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb2ndEventDetail.Location = New System.Drawing.Point(120, 40)
        Me.cb2ndEventDetail.Name = "cb2ndEventDetail"
        Me.cb2ndEventDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb2ndEventDetail.TabIndex = 3
        '
        'lbl2ndEventTypeDetail
        '
        Me.lbl2ndEventTypeDetail.Location = New System.Drawing.Point(6, 70)
        Me.lbl2ndEventTypeDetail.Name = "lbl2ndEventTypeDetail"
        Me.lbl2ndEventTypeDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl2ndEventTypeDetail.TabIndex = 4
        Me.lbl2ndEventTypeDetail.Text = "Event Type Detail"
        '
        'cb2ndEventTypeDetail
        '
        Me.cb2ndEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cb2ndEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb2ndEventTypeDetail.Location = New System.Drawing.Point(120, 67)
        Me.cb2ndEventTypeDetail.Name = "cb2ndEventTypeDetail"
        Me.cb2ndEventTypeDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb2ndEventTypeDetail.TabIndex = 5
        '
        'lbl2ndReason
        '
        Me.lbl2ndReason.Location = New System.Drawing.Point(6, 97)
        Me.lbl2ndReason.Name = "lbl2ndReason"
        Me.lbl2ndReason.Size = New System.Drawing.Size(100, 23)
        Me.lbl2ndReason.TabIndex = 6
        Me.lbl2ndReason.Text = "Reason"
        '
        'txt2ndReason
        '
        Me.txt2ndReason.Location = New System.Drawing.Point(120, 94)
        Me.txt2ndReason.Name = "txt2ndReason"
        Me.txt2ndReason.Size = New System.Drawing.Size(395, 20)
        Me.txt2ndReason.TabIndex = 7
        '
        'lbl2ndAlternative
        '
        Me.lbl2ndAlternative.Location = New System.Drawing.Point(6, 124)
        Me.lbl2ndAlternative.Name = "lbl2ndAlternative"
        Me.lbl2ndAlternative.Size = New System.Drawing.Size(100, 23)
        Me.lbl2ndAlternative.TabIndex = 8
        Me.lbl2ndAlternative.Text = "Alternative"
        '
        'txt2ndAlternative
        '
        Me.txt2ndAlternative.Location = New System.Drawing.Point(120, 121)
        Me.txt2ndAlternative.Name = "txt2ndAlternative"
        Me.txt2ndAlternative.Size = New System.Drawing.Size(395, 20)
        Me.txt2ndAlternative.TabIndex = 9
        '
        'tab3rdEnquiry
        '
        Me.tab3rdEnquiry.Controls.Add(Me.lbl3rdEventCat)
        Me.tab3rdEnquiry.Controls.Add(Me.cb3rdEventCat)
        Me.tab3rdEnquiry.Controls.Add(Me.lbl3rdEventDetail)
        Me.tab3rdEnquiry.Controls.Add(Me.cb3rdEventDetail)
        Me.tab3rdEnquiry.Controls.Add(Me.lbl3rdEventTypeDetail)
        Me.tab3rdEnquiry.Controls.Add(Me.cb3rdEventTypeDetail)
        Me.tab3rdEnquiry.Controls.Add(Me.lbl3rdReason)
        Me.tab3rdEnquiry.Controls.Add(Me.txt3rdReason)
        Me.tab3rdEnquiry.Controls.Add(Me.lbl3rdAlternative)
        Me.tab3rdEnquiry.Controls.Add(Me.txt3rdAlternative)
        Me.tab3rdEnquiry.Location = New System.Drawing.Point(4, 22)
        Me.tab3rdEnquiry.Name = "tab3rdEnquiry"
        Me.tab3rdEnquiry.Padding = New System.Windows.Forms.Padding(3)
        'Me.tab3rdEnquiry.Size = New System.Drawing.Size(525, 194)
        Me.tab3rdEnquiry.Size = New System.Drawing.Size(525, 244)
        Me.tab3rdEnquiry.TabIndex = 2
        Me.tab3rdEnquiry.Text = "3rd Enquiry"
        Me.tab3rdEnquiry.UseVisualStyleBackColor = True
        '
        'lbl3rdEventCat
        '
        Me.lbl3rdEventCat.Location = New System.Drawing.Point(6, 16)
        Me.lbl3rdEventCat.Name = "lbl3rdEventCat"
        Me.lbl3rdEventCat.Size = New System.Drawing.Size(100, 23)
        Me.lbl3rdEventCat.TabIndex = 0
        Me.lbl3rdEventCat.Text = "Event Category"
        '
        'cb3rdEventCat
        '
        Me.cb3rdEventCat.BackColor = System.Drawing.Color.White
        Me.cb3rdEventCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb3rdEventCat.Location = New System.Drawing.Point(120, 13)
        Me.cb3rdEventCat.Name = "cb3rdEventCat"
        Me.cb3rdEventCat.Size = New System.Drawing.Size(395, 21)
        Me.cb3rdEventCat.TabIndex = 1
        '
        'lbl3rdEventDetail
        '
        Me.lbl3rdEventDetail.Location = New System.Drawing.Point(6, 43)
        Me.lbl3rdEventDetail.Name = "lbl3rdEventDetail"
        Me.lbl3rdEventDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl3rdEventDetail.TabIndex = 2
        Me.lbl3rdEventDetail.Text = "Event Detail"
        '
        'cb3rdEventDetail
        '
        Me.cb3rdEventDetail.BackColor = System.Drawing.Color.White
        Me.cb3rdEventDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb3rdEventDetail.Location = New System.Drawing.Point(120, 40)
        Me.cb3rdEventDetail.Name = "cb3rdEventDetail"
        Me.cb3rdEventDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb3rdEventDetail.TabIndex = 3
        '
        'lbl3rdEventTypeDetail
        '
        Me.lbl3rdEventTypeDetail.Location = New System.Drawing.Point(6, 70)
        Me.lbl3rdEventTypeDetail.Name = "lbl3rdEventTypeDetail"
        Me.lbl3rdEventTypeDetail.Size = New System.Drawing.Size(100, 23)
        Me.lbl3rdEventTypeDetail.TabIndex = 4
        Me.lbl3rdEventTypeDetail.Text = "Event Type Detail"
        '
        'cb3rdEventTypeDetail
        '
        Me.cb3rdEventTypeDetail.BackColor = System.Drawing.Color.White
        Me.cb3rdEventTypeDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb3rdEventTypeDetail.Location = New System.Drawing.Point(120, 67)
        Me.cb3rdEventTypeDetail.Name = "cb3rdEventTypeDetail"
        Me.cb3rdEventTypeDetail.Size = New System.Drawing.Size(395, 21)
        Me.cb3rdEventTypeDetail.TabIndex = 5
        '
        'lbl3rdReason
        '
        Me.lbl3rdReason.Location = New System.Drawing.Point(6, 97)
        Me.lbl3rdReason.Name = "lbl3rdReason"
        Me.lbl3rdReason.Size = New System.Drawing.Size(100, 23)
        Me.lbl3rdReason.TabIndex = 6
        Me.lbl3rdReason.Text = "Reason"
        '
        'txt3rdReason
        '
        Me.txt3rdReason.Location = New System.Drawing.Point(120, 94)
        Me.txt3rdReason.Name = "txt3rdReason"
        Me.txt3rdReason.Size = New System.Drawing.Size(395, 20)
        Me.txt3rdReason.TabIndex = 7
        '
        'lbl3rdAlternative
        '
        Me.lbl3rdAlternative.Location = New System.Drawing.Point(6, 124)
        Me.lbl3rdAlternative.Name = "lbl3rdAlternative"
        Me.lbl3rdAlternative.Size = New System.Drawing.Size(100, 23)
        Me.lbl3rdAlternative.TabIndex = 8
        Me.lbl3rdAlternative.Text = "Alternative"
        '
        'txt3rdAlternative
        '
        Me.txt3rdAlternative.Location = New System.Drawing.Point(120, 121)
        Me.txt3rdAlternative.Name = "txt3rdAlternative"
        Me.txt3rdAlternative.Size = New System.Drawing.Size(395, 20)
        Me.txt3rdAlternative.TabIndex = 9
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(574, 184)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        'Me.GroupBox1.Size = New System.Drawing.Size(837, 476)
        Me.GroupBox1.Size = New System.Drawing.Size(837, 526)
        Me.GroupBox1.TabIndex = 82
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Alert and Notes"
        '
        'chkIdVerify
        '
        Me.chkIdVerify.Location = New System.Drawing.Point(120, 239)
        Me.chkIdVerify.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIdVerify.Name = "chkIdVerify"
        Me.chkIdVerify.Size = New System.Drawing.Size(122, 26)
        Me.chkIdVerify.TabIndex = 0
        Me.chkIdVerify.Text = "ID Verify"
        Me.chkIdVerify.UseVisualStyleBackColor = True
        '
        'dtReminder
        '
        Me.dtReminder.Location = New System.Drawing.Point(294, 122)
        Me.dtReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtReminder.Name = "dtReminder"
        Me.dtReminder.Size = New System.Drawing.Size(160, 20)
        Me.dtReminder.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(240, 128)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 22)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Date:"
        '
        'chkReminder
        '
        Me.chkReminder.Location = New System.Drawing.Point(120, 125)
        Me.chkReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkReminder.Name = "chkReminder"
        Me.chkReminder.Size = New System.Drawing.Size(120, 25)
        Me.chkReminder.TabIndex = 11
        Me.chkReminder.Text = "Prompted?"
        '
        'chkPolicyAlert
        '
        Me.chkPolicyAlert.Location = New System.Drawing.Point(120, 21)
        Me.chkPolicyAlert.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPolicyAlert.Name = "chkPolicyAlert"
        Me.chkPolicyAlert.Size = New System.Drawing.Size(144, 25)
        Me.chkPolicyAlert.TabIndex = 9
        Me.chkPolicyAlert.Text = "Prompted?"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 21)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 25)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Policy Alert"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 128)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 22)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Reminder"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 242)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 25)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(18, 267)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNotes.MaxLength = 900
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(811, 197)
        Me.txtNotes.TabIndex = 14
        '
        'txtReminder
        '
        Me.txtReminder.Location = New System.Drawing.Point(18, 156)
        Me.txtReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReminder.MaxLength = 900
        Me.txtReminder.Multiline = True
        Me.txtReminder.Name = "txtReminder"
        Me.txtReminder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReminder.Size = New System.Drawing.Size(811, 72)
        Me.txtReminder.TabIndex = 13
        '
        'txtPolicyAlert
        '
        Me.txtPolicyAlert.Location = New System.Drawing.Point(18, 49)
        Me.txtPolicyAlert.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPolicyAlert.MaxLength = 256
        Me.txtPolicyAlert.Multiline = True
        Me.txtPolicyAlert.Name = "txtPolicyAlert"
        Me.txtPolicyAlert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPolicyAlert.Size = New System.Drawing.Size(811, 66)
        Me.txtPolicyAlert.TabIndex = 10
        '
        'btncustlogweb
        '
        Me.btncustlogweb.Location = New System.Drawing.Point(1301, 86)
        Me.btncustlogweb.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btncustlogweb.Name = "btncustlogweb"
        Me.btncustlogweb.Size = New System.Drawing.Size(112, 85)
        Me.btncustlogweb.TabIndex = 103
        Me.btncustlogweb.Text = "Show Contact History"
        Me.btncustlogweb.UseVisualStyleBackColor = True
        '
        'hidSrvEvtNo
        '
        Me.hidSrvEvtNo.Location = New System.Drawing.Point(1421, 184)
        Me.hidSrvEvtNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.hidSrvEvtNo.Name = "hidSrvEvtNo"
        Me.hidSrvEvtNo.Size = New System.Drawing.Size(100, 20)
        Me.hidSrvEvtNo.TabIndex = 78
        Me.hidSrvEvtNo.Visible = False
        Me.hidSrvEvtNo.WordWrap = False
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
        Me.gbNBM.Location = New System.Drawing.Point(574, 771)
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
        'uclServiceLog_Asur
        '
        Me.Controls.Add(Me.gbPiEservicesAuth)
        Me.Controls.Add(Me.gbNBM)
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
        Me.Name = "uclServiceLog_Asur"
        Me.Size = New System.Drawing.Size(2500, 1050)
        Me.gbPiEservicesAuth.ResumeLayout(False)
        CType(Me.dgvAuthPi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboCustomer.ResumeLayout(False)
        Me.gboCustomer.PerformLayout()
        Me.gboPolicy.ResumeLayout(False)
        Me.gboPolicy.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpServiceEvent.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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

    'added at 2023-9-13 by oliver for Customer Level Search Issue
    Public Property IDNumber() As String
        Get
            Return strIDNumber
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                strIDNumber = Value
            End If
        End Set
    End Property

    Public WriteOnly Property IsAgent() As Boolean
        Set(ByVal Value As Boolean)
            blnIsAgent = Value
        End Set
    End Property

    'oliver 2024-8-6 added for Com 6
    Public WriteOnly Property IsHnwPolicy() As Boolean
        Set(ByVal Value As Boolean)
            blnIsHnwPolicy = Value
        End Set
    End Property

    Public WriteOnly Property IsParallelMode() As Boolean
        Set(ByVal Value As Boolean)
            blnIsParallelMode = Value
        End Set
    End Property
    Public WriteOnly Property IsBothComp() As Boolean
        Set(ByVal Value As Boolean)
            blnIsBothComp = Value
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
        txtCustomerID.DataBindings.Clear()
        chkIdVerify.DataBindings.Clear()
        'Service Log enhanement

        cb1stEventCat.DataBindings.Clear()
        cb1stEventDetail.DataBindings.Clear()
        cb1stEventTypeDetail.DataBindings.Clear()
        txt1stReason.DataBindings.Clear()
        txt1stAlternative.DataBindings.Clear()

        cb2ndEventCat.DataBindings.Clear()
        cb2ndEventDetail.DataBindings.Clear()
        cb2ndEventTypeDetail.DataBindings.Clear()
        txt2ndReason.DataBindings.Clear()
        txt2ndAlternative.DataBindings.Clear()

        cb3rdEventCat.DataBindings.Clear()
        cb3rdEventDetail.DataBindings.Clear()
        cb3rdEventTypeDetail.DataBindings.Clear()
        txt3rdReason.DataBindings.Clear()
        txt3rdAlternative.DataBindings.Clear()
        '
    End Function

    'BuildUI() - Display the Service Log History and initialize form for user input
    Private Sub BuildUI()
        InitDataset()
        InitForm()
    End Sub

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Private Sub uclServiceLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Service log enhancement
        ' Initialize enquiry tab visited flags
        bln1stEnquiryVisited = False
        bln2ndEnquiryVisited = False
        bln3rdEnquiryVisited = False
        '
        CheckEnableNBMPanel()
        'Service log enhancement
        ' Since 1st Enquiry is the default selected tab, mark it as visited
        bln1stEnquiryVisited = True

        ' Manage enquiry tab controls on form load
        ManageEnquiryTabControls()
        '
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

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToGetSerLogByIdCard(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_HK_SERLOG_BY_IDCARD"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_ASSUR_SERLOG_BY_IDCARD"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_ASSUR_SERLOG_BY_IDCARD"
        Else
            companyName = "HK"
            busiId = "GET_HK_SERLOG_BY_IDCARD"
        End If
    End Sub

    'oliver 2024-8-6 added for Com 6
    Private Sub SetCompanyNameAndBusiIdToGetBMUSerLogByIdCard(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_HK_BMU_SERLOG_BY_IDCARD"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_ASSUR_SERLOG_BY_IDCARD"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_ASSUR_SERLOG_BY_IDCARD"
        Else
            companyName = "HK"
            busiId = "GET_HK_BMU_SERLOG_BY_IDCARD"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToGetSerLogByPolicyNo(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_HK_SERLOG_BY_POLICYNO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_ASSUR_SERLOG_BY_POLICYNO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_ASSUR_SERLOG_BY_POLICYNO"
        Else
            companyName = "HK"
            busiId = "GET_HK_SERLOG_BY_POLICYNO"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToInsertSerLog(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "INSERT_HK_SERLOG_INFO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "INSERT_ASSUR_SERLOG_INFO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "INSERT_ASSUR_SERLOG_INFO"
        Else
            companyName = "HK"
            busiId = "INSERT_HK_SERLOG_INFO"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move UpdateServiceLog API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToUpdateSerLog(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "UPDATED_HK_SERLOG_INFO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "UPDATED_ASSUR_SERLOG_INFO"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "UPDATED_ASSUR_SERLOG_INFO"
        Else
            companyName = "HK"
            busiId = "UPDATED_HK_SERLOG_INFO"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move InitPartialcb API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToGetSerLogRelatedTable(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_SERLOG_INFO_RELATED_TABLE"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_SERLOG_INFO_RELATED_TABLE"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_SERLOG_INFO_RELATED_TABLE"
        Else
            companyName = "HK"
            busiId = "GET_SERLOG_INFO_RELATED_TABLE"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move InitPartialcb API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToGetSerLogCustomerTable(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_SERLOG_INFO_CUSTOMER_BY_CUSTOMERID"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_SERLOG_INFO_CUSTOMER_BY_CUSTOMERID"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_SERLOG_INFO_CUSTOMER_BY_CUSTOMERID"
        Else
            companyName = "HK"
            busiId = "GET_SERLOG_INFO_CUSTOMER_BY_CUSTOMERID"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Maintain data consistency When Updating ServiceLog, check whether it Is latter than the updatetime.Or Not cancel update And refresh display</br>
    ''' </remarks>
    ''' <param name="companyType">Represents the company type to get different company name and busiId</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Sub SetCompanyNameAndBusiIdToGetSerLogUpdateTime(ByVal companyType As String, ByRef companyName As String, ByRef busiId As String)
        If companyType = COMPANY_NAME_BERMUDA Then
            companyName = "HK"
            busiId = "GET_SERLOG_UPDATETIME"
        ElseIf companyType = COMPANY_NAME_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_SERLOG_UPDATETIME"
        ElseIf companyType = COMPANY_NAME_ASSURANCE2 Then
            companyName = "LAH"
            busiId = "GET_SERLOG_UPDATETIME"
        Else
            companyName = "HK"
            busiId = "GET_SERLOG_UPDATETIME"
        End If
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Get ServiceLog of Bermuda and Assurance by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    Private Sub GetParallelModeServiceLogByAPI()
        Try

            Dim serviceEventDetailDataTable As New DataTable()
            Dim companyNameList As String() = {COMPANY_NAME_BERMUDA, COMPANY_NAME_ASSURANCE}
            Dim i As Integer
            Dim companyName As String = ""
            Dim busiId As String = ""
            Dim dataSet As DataSet
            For i = 0 To companyNameList.Length - 1
                'oliver 2024-8-6 updated for Com 6
                If blnIsHnwPolicy AndAlso Not isHNWMember Then
                    SetCompanyNameAndBusiIdToGetBMUSerLogByIdCard(companyNameList(i), companyName, busiId)
                    dataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"IsHKID", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), "1", "0")}, {"IDCard", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), strIDNumber, " ")}, {"CustomerID", strCustID}, {"IsAgentCode", IIf(blnIsAgent, "1", "0")}, {"IsHNW", IIf(isHNWMember, "1", "0")}})
                Else
                    SetCompanyNameAndBusiIdToGetSerLogByIdCard(companyNameList(i), companyName, busiId)
                    dataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"IsHKID", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), "1", "0")}, {"IDCard", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), strIDNumber, " ")}, {"CustomerID", strCustID}, {"IsAgentCode", IIf(blnIsAgent, "1", "0")}})
                End If
                If Not IsNothing(dataSet) AndAlso dataSet.Tables.Count > 0 Then
                    If i = 0 Then
                        If Not IsNothing(dataSet) Then
                            serviceEventDetailDataTable = dataSet.Tables(0)
                        End If
                    End If
                    serviceEventDetailDataTable.Merge(dataSet.Tables(0))
                End If
            Next

            If dsSrvLog.Tables.Contains("ServiceEventDetail") Then
                dsSrvLog.Tables("ServiceEventDetail").Constraints.Clear()
                dsSrvLog.Relations.Clear()
                dsSrvLog.Tables.Remove("ServiceEventDetail")
                dsSrvLog.Tables.Add(serviceEventDetailDataTable.Copy)
                AddServiceEventDetailRelations()
            Else
                dsSrvLog.Tables.Add(serviceEventDetailDataTable.Copy)
            End If

            If strCustID = "" Then
                strCustID = FindCustomerid(strPolicy)
            End If

        Catch e As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Get ServiceLog by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    Private Sub GetSingleModeServiceLogByAPI()
        Try
            Dim serviceEventDetailDataTable As New DataTable()
            Dim companyName As String = ""
            Dim busiId As String = ""
            Dim dataSet As DataSet
            If strPolicy = "" Then
                'oliver 2024-8-6 updated for Com 6
                SetCompanyNameAndBusiIdToGetBMUSerLogByIdCard(strCompany, companyName, busiId)
                dataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"IsHKID", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), "1", "0")}, {"IDCard", If(Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX"), strIDNumber, " ")}, {"CustomerID", strCustID}, {"IsAgentCode", IIf(blnIsAgent, "1", "0")}, {"IsHNW", IIf(blnIsHnwPolicy AndAlso isHNWMember, "1", "0")}})
            Else
                SetCompanyNameAndBusiIdToGetSerLogByPolicyNo(strCompany, companyName, busiId)
                dataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"PolicyNo", strPolicy}, {"GLPolicyNo", "GL" & strPolicy}})
            End If

            If Not IsNothing(dataSet) Then
                serviceEventDetailDataTable = dataSet.Tables(0)
            End If
            If dsSrvLog.Tables.Contains("ServiceEventDetail") Then
                dsSrvLog.Tables("ServiceEventDetail").Constraints.Clear()
                dsSrvLog.Relations.Clear()
                dsSrvLog.Tables.Remove("ServiceEventDetail")
                dsSrvLog.Tables.Add(serviceEventDetailDataTable.Copy)
                AddServiceEventDetailRelations()
            Else
                dsSrvLog.Tables.Add(serviceEventDetailDataTable.Copy)
            End If

            If strCustID = "" Then
                strCustID = FindCustomerid(strPolicy)
            End If

        Catch e As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Get ServiceLog Related Table by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move InitPartialcb API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    Private Sub GetServiceLogRelatedTableByAPI()
        Try
            Dim companyName As String = ""
            Dim busiId As String = ""

            If blnIsBothComp Then
                SetCompanyNameAndBusiIdToGetSerLogRelatedTable(COMPANY_NAME_BERMUDA, companyName, busiId)
            Else
                SetCompanyNameAndBusiIdToGetSerLogRelatedTable(strCompany, companyName, busiId)
            End If

            Dim dataSet As DataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String))

            For Each dt As DataTable In dataSet.Tables
                dsSrvLog.Tables.Add(dt.Copy)
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Get ServiceLog Related Table by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move InitPartialcb API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    Private Sub GetServiceLogCustomerTableByAPI()
        Try
            Dim companyName As String = ""
            Dim busiId As String = ""

            SetCompanyNameAndBusiIdToGetSerLogCustomerTable(strCompany, companyName, busiId)
            Dim dataSet As DataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"CustomerID", strCustID}})
            If Not IsNothing(dataSet) Then
                dsSrvLog.Tables.Add(dataSet.Tables(0).Copy)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Add Service EventDetail Relations
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which display all Service log for Merge Bermuda and Assurance</br>
    ''' </remarks>
    Private Sub AddServiceEventDetailRelations()
        Try
            If Not dsSrvLog.Relations.Contains("EvtMed_SrvEvt") Then
                dsSrvLog.Relations.Add("EvtMed_SrvEvt", dsSrvLog.Tables("EventSourceMediumCodes").Columns("EventSourceMediumCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventSourceMediumCode"), False)
            End If
            If Not dsSrvLog.Relations.Contains("Csr_SrvEvt_Secondary") Then
                dsSrvLog.Relations.Add("Csr_SrvEvt_Secondary", dsSrvLog.Tables("Csr").Columns("CSRID"), dsSrvLog.Tables("ServiceEventDetail").Columns("SecondaryCSRID"), False)
            End If
            If Not dsSrvLog.Relations.Contains("EvtInit_SrvEvt") Then
                dsSrvLog.Relations.Add("EvtInit_SrvEvt", dsSrvLog.Tables("EventSourceInitiatorCodes").Columns("EventSourceInitiatorCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventSourceInitiatorCode"), False)
            End If

            If Not dsSrvLog.Relations.Contains("EvtTypeDet_SrvEvt") Then
                Dim dcParent(2) As DataColumn
                Dim dcChild(2) As DataColumn
                dcParent(0) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_category_code")
                dcChild(0) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode")
                dcParent(1) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_type_code")
                dcChild(1) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeCode")
                dcParent(2) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_typedtl_code")
                dcChild(2) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeDetailCode")
                dsSrvLog.Relations.Add("EvtTypeDet_SrvEvt", dcParent, dcChild, False)
            End If

            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("EventSourceMedium") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("EventSourceMedium", GetType(String))
            End If
            'Service log enhancement
            ' Add new columns for enquiry tabs
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("1stEventCategoryCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("1stEventCategoryCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("1stEventTypeCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("1stEventTypeCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("1stEventTypeDetailCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("1stEventTypeDetailCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("1stReason") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("1stReason", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("1stAlternative") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("1stAlternative", GetType(String))
            End If

            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("2ndEventCategoryCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("2ndEventCategoryCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("2ndEventTypeCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("2ndEventTypeCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("2ndEventTypeDetailCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("2ndEventTypeDetailCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("2ndReason") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("2ndReason", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("2ndAlternative") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("2ndAlternative", GetType(String))
            End If

            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("3rdEventCategoryCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("3rdEventCategoryCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("3rdEventTypeCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("3rdEventTypeCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("3rdEventTypeDetailCode") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("3rdEventTypeDetailCode", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("3rdReason") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("3rdReason", GetType(String))
            End If
            If Not dsSrvLog.Tables("ServiceEventDetail").Columns.Contains("3rdAlternative") Then
                dsSrvLog.Tables("ServiceEventDetail").Columns.Add("3rdAlternative", GetType(String))
            End If
            '
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'InitDataset() - Fill the dataset and add relations on the tables
    Private Sub InitDataset()

        'added at 2023-9-18 by oliver for Customer Level Search Issue
        Try
            If blnIsParallelMode AndAlso blnIsBothComp AndAlso Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX") Then
                GetParallelModeServiceLogByAPI()
            Else
                GetSingleModeServiceLogByAPI()
            End If

            'added at 2023-9-18 by oliver for Customer Level Search Issue
            GetServiceLogRelatedTableByAPI()
            GetServiceLogCustomerTableByAPI()

        Catch ex As Exception
            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
            Exit Sub
        End Try

        Try
            'added at 2023-9-18 by oliver for Customer Level Search Issue
            AddServiceEventDetailRelations()

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

            'Service log enhancement - Add new enquiry columns to data grid
            ' 1st Enquiry columns
            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "1stEventCategoryCode"
            cs.HeaderText = "Event Category 1"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "1stEventTypeCode"
            cs.HeaderText = "Event Type 1"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 120
            cs.MappingName = "1stEventTypeDetailCode"
            cs.HeaderText = "Event Type Detail 1"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "1stReason"
            cs.HeaderText = "Event Type Reason 1"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "1stAlternative"
            cs.HeaderText = "Alternative 1"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            ' 2nd Enquiry columns
            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "2ndEventCategoryCode"
            cs.HeaderText = "Event Category 2"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "2ndEventTypeCode"
            cs.HeaderText = "Event Type 2"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 120
            cs.MappingName = "2ndEventTypeDetailCode"
            cs.HeaderText = "Event Type Detail 2"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "2ndReason"
            cs.HeaderText = "Event Type Reason 2"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "2ndAlternative"
            cs.HeaderText = "Alternative 2"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            ' 3rd Enquiry columns
            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "3rdEventCategoryCode"
            cs.HeaderText = "Event Category 3"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "3rdEventTypeCode"
            cs.HeaderText = "Event Type 3"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 120
            cs.MappingName = "3rdEventTypeDetailCode"
            cs.HeaderText = "Event Type Detail 3"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "3rdReason"
            cs.HeaderText = "Event Type Reason 3"
            cs.NullText = gNULLText
            tsSrvLog.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "3rdAlternative"
            cs.HeaderText = "Alternative 3"
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

            'added at 2023-9-18 by oliver for Customer Level Search Issue
            If blnIsParallelMode Then
                cs = New DataGridTextBoxColumn
                cs.Width = 80
                cs.MappingName = "CompanyID"
                cs.HeaderText = "CompanyID"
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

            'oliver 2024-4-24 udpated for Table_Relocate_Sprint13
            Dim dtPiAuth As New DataTable
            If Not String.IsNullOrEmpty(strPolicy) Then
                dtPiAuth = GetPiAuth(strPolicy)
            End If
            If dtPiAuth.Rows.Count > 0 Then
                dtPiAuth.Columns("CustomerID").ColumnName = "PI ID"
                dtPiAuth.Columns("EmailAddr").ColumnName = "Email"
                dtPiAuth.Columns("SA_EmailAddr").ColumnName = "Broker Email"
                dtPiAuth.Columns("LastUpdateDate").ColumnName = "Auth Date"
                dtPiAuth.Columns("Enable").ColumnName = "Auth"
                dgvAuthPi.DataSource = dtPiAuth
                dgvAuthPi.Columns("Auth Date").DefaultCellStyle.Format = "MM/dd/yyyy"
            End If

        Catch ex As Exception
            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
        End Try

    End Sub

    'InitForm() - Fill the contents of controls in the form
    Private Sub InitPartialcb()
        cbInitiator.DataSource = dsSrvLog.Tables("EventSourceInitiatorCodes")
        cbInitiator.DisplayMember = "EventSourceInitiator"
        cbInitiator.ValueMember = "EventSourceInitiatorCode"
        dtInitial.Format = DateTimePickerFormat.Custom
        dtInitial.CustomFormat = gDateTimeFormat

        cbMedium.DataSource = dsSrvLog.Tables("EventSourceMediumCodes")
        cbMedium.DisplayMember = "EventSourceMedium"
        cbMedium.ValueMember = "EventSourceMediumCode"

        cbEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
        cbEventCat.DisplayMember = "cswecc_desc"
        cbEventCat.ValueMember = "cswecc_code"

        cbEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes")
        cbEventDetail.DisplayMember = "EventTypeDesc"
        cbEventDetail.ValueMember = "EventTypeCode"

        cbEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code")
        cbEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cbEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        'Service Log enhancement
        ' Set data sources for new enquiry tab controls
        cb1stEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code"))
        cb1stEventCat.DisplayMember = "cswecc_desc"
        cb1stEventCat.ValueMember = "cswecc_code"

        cb1stEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes"))
        cb1stEventDetail.DisplayMember = "EventTypeDesc"
        cb1stEventDetail.ValueMember = "EventTypeCode"

        cb1stEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code"))
        cb1stEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb1stEventTypeDetail.ValueMember = "csw_event_typedtl_code"

        cb2ndEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code"))
        cb2ndEventCat.DisplayMember = "cswecc_desc"
        cb2ndEventCat.ValueMember = "cswecc_code"

        cb2ndEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes"))
        cb2ndEventDetail.DisplayMember = "EventTypeDesc"
        cb2ndEventDetail.ValueMember = "EventTypeCode"

        cb2ndEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code"))
        cb2ndEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb2ndEventTypeDetail.ValueMember = "csw_event_typedtl_code"

        cb3rdEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code"))
        cb3rdEventCat.DisplayMember = "cswecc_desc"
        cb3rdEventCat.ValueMember = "cswecc_code"

        cb3rdEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes"))
        cb3rdEventDetail.DisplayMember = "EventTypeDesc"
        cb3rdEventDetail.ValueMember = "EventTypeCode"

        cb3rdEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code"))
        cb3rdEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb3rdEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        '
    End Sub

    Private Sub InitPartialcbWithOutIWS()
        blnIsWithOutIWS = True

        cbInitiator.DataSource = dsSrvLog.Tables("EventSourceInitiatorCodes_wo_iws")
        cbInitiator.DisplayMember = "EventSourceInitiator"
        cbInitiator.ValueMember = "EventSourceInitiatorCode"

        cbMedium.DataSource = dsSrvLog.Tables("EventSourceMediumCodes_wo_iws")
        cbMedium.DisplayMember = "EventSourceMedium"
        cbMedium.ValueMember = "EventSourceMediumCode"

        cbEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code_wo_iws")
        cbEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cbEventTypeDetail.ValueMember = "csw_event_typedtl_code"

        cbEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes_wo_iws")
        cbEventDetail.DisplayMember = "EventTypeDesc"
        cbEventDetail.ValueMember = "EventTypeCode"

        cbEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code_wo_iws")
        cbEventCat.DisplayMember = "cswecc_desc"
        cbEventCat.ValueMember = "cswecc_code"
        blnIsWithOutIWS = False
        'Service log enhancement
        ' Set data sources for new enquiry tab controls (without IWS)
        'cb1stEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code_wo_iws")
        cb1stEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code_wo_iws"))
        cb1stEventCat.DisplayMember = "cswecc_desc"
        cb1stEventCat.ValueMember = "cswecc_code"

        cb1stEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes_wo_iws"))
        cb1stEventDetail.DisplayMember = "EventTypeDesc"
        cb1stEventDetail.ValueMember = "EventTypeCode"

        cb1stEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code_wo_iws"))
        cb1stEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb1stEventTypeDetail.ValueMember = "csw_event_typedtl_code"

        cb2ndEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code_wo_iws"))
        cb2ndEventCat.DisplayMember = "cswecc_desc"
        cb2ndEventCat.ValueMember = "cswecc_code"

        cb2ndEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes_wo_iws"))
        cb2ndEventDetail.DisplayMember = "EventTypeDesc"
        cb2ndEventDetail.ValueMember = "EventTypeCode"

        cb2ndEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code_wo_iws"))
        cb2ndEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb2ndEventTypeDetail.ValueMember = "csw_event_typedtl_code"

        cb3rdEventCat.DataSource = New DataView(dsSrvLog.Tables("csw_event_category_code_wo_iws"))
        cb3rdEventCat.DisplayMember = "cswecc_desc"
        cb3rdEventCat.ValueMember = "cswecc_code"

        cb3rdEventDetail.DataSource = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes_wo_iws"))
        cb3rdEventDetail.DisplayMember = "EventTypeDesc"
        cb3rdEventDetail.ValueMember = "EventTypeCode"

        cb3rdEventTypeDetail.DataSource = New DataView(dsSrvLog.Tables("csw_event_typedtl_code_wo_iws"))
        cb3rdEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb3rdEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        '
    End Sub

    Private Sub InitForm()
        Dim b As Binding

        'Fill the Medium Combo Box
        InitPartialcb()

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
        
        'Service log enhancement
        ' Ensure new columns exist before binding
        AddServiceEventDetailRelations()
        
        ' Bind new enquiry tab controls with error handling
        Try
            ' Check if required data tables exist
            If dsSrvLog.Tables.Contains("csw_event_category_code") Then
                cb1stEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
                cb1stEventCat.DisplayMember = "cswecc_desc"
                cb1stEventCat.ValueMember = "cswecc_code"
                cb1stEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventCategoryCode")
            End If

            If dsSrvLog.Tables.Contains("ServiceEventTypeCodes") Then
                cb1stEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes")
                cb1stEventDetail.DisplayMember = "EventTypeDesc"
                cb1stEventDetail.ValueMember = "EventTypeCode"
                cb1stEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventTypeCode")
            End If

            If dsSrvLog.Tables.Contains("csw_event_typedtl_code") Then
                cb1stEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code")
                cb1stEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
                cb1stEventTypeDetail.ValueMember = "csw_event_typedtl_code"
                cb1stEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventTypeDetailCode")
            End If

            txt1stReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "1stReason")
            txt1stAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "1stAlternative")

            ' 2nd Enquiry
            If dsSrvLog.Tables.Contains("csw_event_category_code") Then
                cb2ndEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
                cb2ndEventCat.DisplayMember = "cswecc_desc"
                cb2ndEventCat.ValueMember = "cswecc_code"
                cb2ndEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventCategoryCode")
            End If

            If dsSrvLog.Tables.Contains("ServiceEventTypeCodes") Then
                cb2ndEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes")
                cb2ndEventDetail.DisplayMember = "EventTypeDesc"
                cb2ndEventDetail.ValueMember = "EventTypeCode"
                cb2ndEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventTypeCode")
            End If

            If dsSrvLog.Tables.Contains("csw_event_typedtl_code") Then
                cb2ndEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code")
                cb2ndEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
                cb2ndEventTypeDetail.ValueMember = "csw_event_typedtl_code"
                cb2ndEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventTypeDetailCode")
            End If

            txt2ndReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "2ndReason")
            txt2ndAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "2ndAlternative")

            ' 3rd Enquiry
            If dsSrvLog.Tables.Contains("csw_event_category_code") Then
                cb3rdEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
                cb3rdEventCat.DisplayMember = "cswecc_desc"
                cb3rdEventCat.ValueMember = "cswecc_code"
                cb3rdEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventCategoryCode")
            End If

            If dsSrvLog.Tables.Contains("ServiceEventTypeCodes") Then
                cb3rdEventDetail.DataSource = dsSrvLog.Tables("ServiceEventTypeCodes")
                cb3rdEventDetail.DisplayMember = "EventTypeDesc"
                cb3rdEventDetail.ValueMember = "EventTypeCode"
                cb3rdEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventTypeCode")
            End If

            If dsSrvLog.Tables.Contains("csw_event_typedtl_code") Then
                cb3rdEventTypeDetail.DataSource = dsSrvLog.Tables("csw_event_typedtl_code")
                cb3rdEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
                cb3rdEventTypeDetail.ValueMember = "csw_event_typedtl_code"
                cb3rdEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventTypeDetailCode")
            End If

            txt3rdReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "3rdReason")
            txt3rdAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "3rdAlternative")
        Catch ex As Exception
            ' Log error or handle gracefully - enquiry fields may not be available for all records
            System.Diagnostics.Debug.WriteLine("Error binding enquiry fields: " & ex.Message)
        End Try
        '
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
            dgSrvLog.Width = 850
        Else
            gboPolicy.Visible = True
            txtPolicyNo.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "PolicyAccountNo")
            dgSrvLog.Width = 900

        End If

        'Display Customer groupbox when entry point from customer search
        'updated at 2023-9-18 by oliver for Customer Level Search Issue
        If blnIsCustLevel Then
            gboCustomer.Visible = True
            txtCustomerID.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "CustomerID")
            If dsSrvLog.Tables("Customer").Rows.Count > 0 Then
                txtCustomerName.Text = dsSrvLog.Tables("Customer").Rows(0).Item("NameSuffix").ToString() & " " & dsSrvLog.Tables("Customer").Rows(0).Item("FirstName").ToString()
            End If
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


        'getPostSalesCallInfo()

        'AddHandler daSrvEvtDet.RowUpdating, AddressOf RowUpdating

        If Mid(strUPSMenuCtrl, 94, 1) = "1" Then
            btnDisablePI.Enabled = True
            btnAuthToPi.Enabled = True
        Else
            btnDisablePI.Enabled = False
            btnAuthToPi.Enabled = False
        End If

        BackgroundWorker1.RunWorkerAsync()


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
        
        ' Service log enhancement - Manage enquiry tab controls when status changes
        ManageEnquiryTabControls()
    End Sub

    Private Sub cbEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventCat.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cbEventCat.SelectedValue) Is String Then
            SetType()
            SetTypeDetail()
        End If
    End Sub

    Private Sub cbEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventDetail.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

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

        Dim tableName As String
        If blnIsNewMode Then
            tableName = "csw_event_typedtl_code_wo_iws"
        Else
            tableName = "csw_event_typedtl_code"
        End If

        dsSrvLog.Tables(tableName).DefaultView.RowFilter = "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'"
        dsSrvLog.Tables(tableName).DefaultView.Sort = "cswetd_sort_order"
        If cbEventTypeDetail.Items.Count > 0 Then
            cbEventTypeDetail.SelectedIndex = -1
            cbEventTypeDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub SetType()
        Dim strCat As String
        strCat = cbEventCat.SelectedValue.ToString
        Dim tableName As String
        If blnIsNewMode Then
            tableName = "ServiceEventTypeCodes_wo_iws"
        Else
            tableName = "ServiceEventTypeCodes"
        End If

        dsSrvLog.Tables(tableName).DefaultView.RowFilter = "EventCategoryCode = '" & strCat & "'"
        dsSrvLog.Tables(tableName).DefaultView.Sort = "SortOrder"
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
    'Service log enhancement
    ' Manage enquiry tab controls based on mode and status
    Private Sub ManageEnquiryTabControls()
        ' Determine if enquiry controls should be enabled
        ' Enable if in new mode OR if Status is "Pending" (EventStatusCode = "P")
        Dim enableEnquiryControls As Boolean = blnIsNewMode
        
        ' If not in new mode, check if Status is "Pending"
        If Not blnIsNewMode AndAlso cbStatus.SelectedValue IsNot Nothing Then
            If TypeOf (cbStatus.SelectedValue) Is String AndAlso cbStatus.SelectedValue.ToString() = "P" Then
                enableEnquiryControls = True
            End If
        End If
        
        ' 1st Enquiry controls
        cb1stEventCat.Enabled = enableEnquiryControls
        cb1stEventDetail.Enabled = enableEnquiryControls
        cb1stEventTypeDetail.Enabled = enableEnquiryControls
        txt1stReason.Enabled = enableEnquiryControls
        txt1stAlternative.Enabled = enableEnquiryControls
        
        ' 2nd Enquiry controls
        cb2ndEventCat.Enabled = enableEnquiryControls
        cb2ndEventDetail.Enabled = enableEnquiryControls
        cb2ndEventTypeDetail.Enabled = enableEnquiryControls
        txt2ndReason.Enabled = enableEnquiryControls
        txt2ndAlternative.Enabled = enableEnquiryControls
        
        ' 3rd Enquiry controls
        cb3rdEventCat.Enabled = enableEnquiryControls
        cb3rdEventDetail.Enabled = enableEnquiryControls
        cb3rdEventTypeDetail.Enabled = enableEnquiryControls
        txt3rdReason.Enabled = enableEnquiryControls
        txt3rdAlternative.Enabled = enableEnquiryControls
        
        ' Set visual appearance for disabled state
        If Not enableEnquiryControls Then
            cb1stEventCat.BackColor = System.Drawing.Color.LightGray
            cb1stEventDetail.BackColor = System.Drawing.Color.LightGray
            cb1stEventTypeDetail.BackColor = System.Drawing.Color.LightGray
            txt1stReason.BackColor = System.Drawing.Color.LightGray
            txt1stAlternative.BackColor = System.Drawing.Color.LightGray
            
            cb2ndEventCat.BackColor = System.Drawing.Color.LightGray
            cb2ndEventDetail.BackColor = System.Drawing.Color.LightGray
            cb2ndEventTypeDetail.BackColor = System.Drawing.Color.LightGray
            txt2ndReason.BackColor = System.Drawing.Color.LightGray
            txt2ndAlternative.BackColor = System.Drawing.Color.LightGray
            
            cb3rdEventCat.BackColor = System.Drawing.Color.LightGray
            cb3rdEventDetail.BackColor = System.Drawing.Color.LightGray
            cb3rdEventTypeDetail.BackColor = System.Drawing.Color.LightGray
            txt3rdReason.BackColor = System.Drawing.Color.LightGray
            txt3rdAlternative.BackColor = System.Drawing.Color.LightGray
        Else
            cb1stEventCat.BackColor = System.Drawing.Color.White
            cb1stEventDetail.BackColor = System.Drawing.Color.White
            cb1stEventTypeDetail.BackColor = System.Drawing.Color.White
            txt1stReason.BackColor = System.Drawing.Color.White
            txt1stAlternative.BackColor = System.Drawing.Color.White
            
            cb2ndEventCat.BackColor = System.Drawing.Color.White
            cb2ndEventDetail.BackColor = System.Drawing.Color.White
            cb2ndEventTypeDetail.BackColor = System.Drawing.Color.White
            txt2ndReason.BackColor = System.Drawing.Color.White
            txt2ndAlternative.BackColor = System.Drawing.Color.White
            
            cb3rdEventCat.BackColor = System.Drawing.Color.White
            cb3rdEventDetail.BackColor = System.Drawing.Color.White
            cb3rdEventTypeDetail.BackColor = System.Drawing.Color.White
            txt3rdReason.BackColor = System.Drawing.Color.White
            txt3rdAlternative.BackColor = System.Drawing.Color.White
        End If
    End Sub

    ' New enquiry tab cascade functionality
    Private Sub cb1stEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb1stEventCat.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb1stEventCat.SelectedValue) Is String Then
            Set1stType()
            Set1stTypeDetail()
        End If
    End Sub
    'Service Log enhancement
    Private Sub tabEnquiry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabEnquiry.SelectedIndexChanged
        ' Handle tab switching to clear/preserve data appropriately
        Select Case tabEnquiry.SelectedIndex
            Case 0 ' 1st Enquiry
                If Not bln1stEnquiryVisited Then
                    ' First time visiting 1st Enquiry - clear all fields
                    Clear1stEnquiryFields()
                    bln1stEnquiryVisited = True
                End If
            Case 1 ' 2nd Enquiry
                If Not bln2ndEnquiryVisited Then
                    ' First time visiting 2nd Enquiry - clear all fields
                    Clear2ndEnquiryFields()
                    bln2ndEnquiryVisited = True
                End If
            Case 2 ' 3rd Enquiry
                If Not bln3rdEnquiryVisited Then
                    ' First time visiting 3rd Enquiry - clear all fields
                    Clear3rdEnquiryFields()
                    bln3rdEnquiryVisited = True
                End If
        End Select
    End Sub
    ' Helper functions to clear enquiry fields
    Private Sub Clear1stEnquiryFields()
        ' Temporarily remove data bindings to clear fields
        cb1stEventCat.DataBindings.Clear()
        cb1stEventDetail.DataBindings.Clear()
        cb1stEventTypeDetail.DataBindings.Clear()
        txt1stReason.DataBindings.Clear()
        txt1stAlternative.DataBindings.Clear()

        ' Clear the fields
        cb1stEventCat.SelectedIndex = -1
        cb1stEventDetail.SelectedIndex = -1
        cb1stEventTypeDetail.SelectedIndex = -1
        txt1stReason.Text = ""
        txt1stAlternative.Text = ""

        ' Re-establish data bindings only if in new mode
        If blnIsNewMode Then
            cb1stEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventCategoryCode")
            cb1stEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventTypeCode")
            cb1stEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "1stEventTypeDetailCode")
            txt1stReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "1stReason")
            txt1stAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "1stAlternative")
        End If
    End Sub

    Private Sub Clear2ndEnquiryFields()
        ' Temporarily remove data bindings to clear fields
        cb2ndEventCat.DataBindings.Clear()
        cb2ndEventDetail.DataBindings.Clear()
        cb2ndEventTypeDetail.DataBindings.Clear()
        txt2ndReason.DataBindings.Clear()
        txt2ndAlternative.DataBindings.Clear()

        ' Clear the fields
        cb2ndEventCat.SelectedIndex = -1
        cb2ndEventDetail.SelectedIndex = -1
        cb2ndEventTypeDetail.SelectedIndex = -1
        txt2ndReason.Text = ""
        txt2ndAlternative.Text = ""

        ' Re-establish data bindings only if in new mode
        If blnIsNewMode Then
            cb2ndEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventCategoryCode")
            cb2ndEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventTypeCode")
            cb2ndEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "2ndEventTypeDetailCode")
            txt2ndReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "2ndReason")
            txt2ndAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "2ndAlternative")
        End If
    End Sub

    Private Sub Clear3rdEnquiryFields()
        ' Temporarily remove data bindings to clear fields
        cb3rdEventCat.DataBindings.Clear()
        cb3rdEventDetail.DataBindings.Clear()
        cb3rdEventTypeDetail.DataBindings.Clear()
        txt3rdReason.DataBindings.Clear()
        txt3rdAlternative.DataBindings.Clear()

        ' Clear the fields
        cb3rdEventCat.SelectedIndex = -1
        cb3rdEventDetail.SelectedIndex = -1
        cb3rdEventTypeDetail.SelectedIndex = -1
        txt3rdReason.Text = ""
        txt3rdAlternative.Text = ""

        ' Re-establish data bindings only if in new mode
        If blnIsNewMode Then
            cb3rdEventCat.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventCategoryCode")
            cb3rdEventDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventTypeCode")
            cb3rdEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog.Tables("ServiceEventDetail"), "3rdEventTypeDetailCode")
            txt3rdReason.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "3rdReason")
            txt3rdAlternative.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "3rdAlternative")
        End If
    End Sub
    '
    Private Sub cb1stEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb1stEventDetail.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb1stEventCat.SelectedValue) Is String And TypeOf (cb1stEventDetail.SelectedValue) Is String Then
            Set1stTypeDetail()
        End If
    End Sub

    Private Sub cb2ndEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb2ndEventCat.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb2ndEventCat.SelectedValue) Is String Then
            Set2ndType()
            Set2ndTypeDetail()
        End If
    End Sub

    Private Sub cb2ndEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb2ndEventDetail.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb2ndEventCat.SelectedValue) Is String And TypeOf (cb2ndEventDetail.SelectedValue) Is String Then
            Set2ndTypeDetail()
        End If
    End Sub

    Private Sub cb3rdEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb3rdEventCat.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb3rdEventCat.SelectedValue) Is String Then
            Set3rdType()
            Set3rdTypeDetail()
        End If
    End Sub

    Private Sub cb3rdEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb3rdEventDetail.SelectedIndexChanged
        If blnIsWithOutIWS Then
            Exit Sub
        End If

        If TypeOf (cb3rdEventCat.SelectedValue) Is String And TypeOf (cb3rdEventDetail.SelectedValue) Is String Then
            Set3rdTypeDetail()
        End If
    End Sub

    Private Sub Set1stType()
        Dim strCat As String
        strCat = cb1stEventCat.SelectedValue.ToString
        Dim tableName As String
        If blnIsNewMode Then
            tableName = "ServiceEventTypeCodes_wo_iws"
        Else
            tableName = "ServiceEventTypeCodes"
        End If

        ' Create a separate DataView for 1st enquiry to avoid affecting other tabs
        Dim dv1st As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv1st.RowFilter = "EventCategoryCode = '" & strCat & "'"
        dv1st.Sort = "SortOrder"
        cb1stEventDetail.DataSource = dv1st
        cb1stEventDetail.DisplayMember = "EventTypeDesc"
        cb1stEventDetail.ValueMember = "EventTypeCode"
        
        If cb1stEventDetail.Items.Count > 0 Then
            cb1stEventDetail.SelectedIndex = -1
            cb1stEventDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub Set1stTypeDetail()
        Dim strCat As String
        Dim strType As String
        strCat = cb1stEventCat.SelectedValue.ToString
        If Not cb1stEventDetail.SelectedValue Is Nothing Then
            strType = cb1stEventDetail.SelectedValue.ToString
        End If

        Dim tableName As String
        If blnIsNewMode Then
            tableName = "csw_event_typedtl_code_wo_iws"
        Else
            tableName = "csw_event_typedtl_code"
        End If

        ' Create a separate DataView for 1st enquiry to avoid affecting other tabs
        Dim dv1st As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv1st.RowFilter = "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'"
        dv1st.Sort = "cswetd_sort_order"
        cb1stEventTypeDetail.DataSource = dv1st
        cb1stEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb1stEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        
        If cb1stEventTypeDetail.Items.Count > 0 Then
            cb1stEventTypeDetail.SelectedIndex = -1
            cb1stEventTypeDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub Set2ndType()
        Dim strCat As String
        strCat = cb2ndEventCat.SelectedValue.ToString
        Dim tableName As String
        If blnIsNewMode Then
            tableName = "ServiceEventTypeCodes_wo_iws"
        Else
            tableName = "ServiceEventTypeCodes"
        End If

        ' Create a separate DataView for 2nd enquiry to avoid affecting other tabs
        Dim dv2nd As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv2nd.RowFilter = "EventCategoryCode = '" & strCat & "'"
        dv2nd.Sort = "SortOrder"
        cb2ndEventDetail.DataSource = dv2nd
        cb2ndEventDetail.DisplayMember = "EventTypeDesc"
        cb2ndEventDetail.ValueMember = "EventTypeCode"
        
        If cb2ndEventDetail.Items.Count > 0 Then
            cb2ndEventDetail.SelectedIndex = -1
            cb2ndEventDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub Set2ndTypeDetail()
        Dim strCat As String
        Dim strType As String
        strCat = cb2ndEventCat.SelectedValue.ToString
        If Not cb2ndEventDetail.SelectedValue Is Nothing Then
            strType = cb2ndEventDetail.SelectedValue.ToString
        End If

        Dim tableName As String
        If blnIsNewMode Then
            tableName = "csw_event_typedtl_code_wo_iws"
        Else
            tableName = "csw_event_typedtl_code"
        End If

        ' Create a separate DataView for 2nd enquiry to avoid affecting other tabs
        Dim dv2nd As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv2nd.RowFilter = "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'"
        dv2nd.Sort = "cswetd_sort_order"
        cb2ndEventTypeDetail.DataSource = dv2nd
        cb2ndEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb2ndEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        
        If cb2ndEventTypeDetail.Items.Count > 0 Then
            cb2ndEventTypeDetail.SelectedIndex = -1
            cb2ndEventTypeDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub Set3rdType()
        Dim strCat As String
        strCat = cb3rdEventCat.SelectedValue.ToString
        Dim tableName As String
        If blnIsNewMode Then
            tableName = "ServiceEventTypeCodes_wo_iws"
        Else
            tableName = "ServiceEventTypeCodes"
        End If

        ' Create a separate DataView for 3rd enquiry to avoid affecting other tabs
        Dim dv3rd As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv3rd.RowFilter = "EventCategoryCode = '" & strCat & "'"
        dv3rd.Sort = "SortOrder"
        cb3rdEventDetail.DataSource = dv3rd
        cb3rdEventDetail.DisplayMember = "EventTypeDesc"
        cb3rdEventDetail.ValueMember = "EventTypeCode"
        
        If cb3rdEventDetail.Items.Count > 0 Then
            cb3rdEventDetail.SelectedIndex = -1
            cb3rdEventDetail.SelectedIndex = 0
        End If
    End Sub

    Private Sub Set3rdTypeDetail()
        Dim strCat As String
        Dim strType As String
        strCat = cb3rdEventCat.SelectedValue.ToString
        If Not cb3rdEventDetail.SelectedValue Is Nothing Then
            strType = cb3rdEventDetail.SelectedValue.ToString
        End If

        Dim tableName As String
        If blnIsNewMode Then
            tableName = "csw_event_typedtl_code_wo_iws"
        Else
            tableName = "csw_event_typedtl_code"
        End If

        ' Create a separate DataView for 3rd enquiry to avoid affecting other tabs
        Dim dv3rd As DataView = New DataView(dsSrvLog.Tables(tableName))
        dv3rd.RowFilter = "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'"
        dv3rd.Sort = "cswetd_sort_order"
        cb3rdEventTypeDetail.DataSource = dv3rd
        cb3rdEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cb3rdEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        
        If cb3rdEventTypeDetail.Items.Count > 0 Then
            cb3rdEventTypeDetail.SelectedIndex = -1
            cb3rdEventTypeDetail.SelectedIndex = 0
        End If
    End Sub
    '
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
        blnIsNewMode = True
        dr = dsSrvLog.Tables("ServiceEventDetail").NewRow()
        dr.Item("EventSourceMediumCode") = "PC"
        dr.Item("EventCategoryCode") = "20"
        dr.Item("EventTypeCode") = "15"
        dr.Item("EventTypeDetailCode") = "10"
        dr.Item("CustomerID") = IIf(Not String.IsNullOrEmpty(txtCustomerID.Text), txtCustomerID.Text, strCustID)
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
        'added at 2023-9-18 by oliver for Customer Level Search Issue
        dr.Item("ServiceEventNumber") = 0
        dr.Item("ProductName") = ""
        dr.Item("sender_name") = ""
        dr.Item("PolicyType") = ""
        dr.Item("timestamp") = 0
        'C001 - Start
        dr.Item("MCV") = ""
        'C001 - End

        Try
            dr.Item("companyID") = _CompanyID
        Catch ex As Exception

        End Try

        Try
            InitPartialcbWithOutIWS()
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

        'rbBer.Checked = True
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
        
        ' Service log enhancement - Manage enquiry tab controls for new record
        ManageEnquiryTabControls()
    End Sub

    'btnCancel_Click() - Cancel the modifications since last acceptchanges() 
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        blnIsNewMode = False
        CancelServiceLog()
        
        ' Service log enhancement - Manage enquiry tab controls after cancel
        ManageEnquiryTabControls()
    End Sub

    'btnSave_Click() - Synchronize the SQL table with modifications in dataset
    'updated by oliver for Customer Level Search Issue
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

        If cbEventCat.Text = "Courtesy Call - Suitability mismatch" AndAlso Me.rad_SuitabilityMisMatch.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If
        'Post Sales Call - End
        'Checking for serviceLog
        If bm IsNot Nothing Then
            Dim dr_check As DataRow = CType(bm.Current, DataRowView).Row()
            Dim comId As String
            Dim logNotFoundAsur As Boolean = False
            Dim logNotFoundBer As Boolean = False
            Dim policyAccountNo As String = dr_check("PolicyAccountNo").ToString()
            If (rbAsur.Checked) Then
                comId = "LAC"
                If Not CheckIsPolicyExist(policyAccountNo, strPolicy, comId) Then
                    logNotFoundAsur = True
                End If
            End If
            If (rbBer.Checked) Then
                comId = "ING"
                If Not CheckIsPolicyExist(policyAccountNo, strPolicy, comId) Then
                    logNotFoundBer = True
                End If
            End If
            If (comId = "LAC" AndAlso logNotFoundAsur) Or (comId = "ING" AndAlso logNotFoundBer) Then
                Dim result As DialogResult = MessageBox.Show("Policy NOT INFORCED or Exist. Continue to save?",
                                                             "Confirmation",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question)

                ' Handle the user's choice
                If result = DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If
        'Save the current row
        'bm = Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail"))
        'iPrevPosition = bm.Position

        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()
        If dsSrvLog.HasChanges Then
            'If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
            Dim dr As DataRow
            Dim insertedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Added)
            If Not IsNothing(insertedDataTable) Then
                For Each dr In insertedDataTable.Rows
                    InsertServiceLog(dr)
                Next
            End If
            Dim updatedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Modified)
            If Not IsNothing(updatedDataTable) Then
                For Each dr In updatedDataTable.Rows
                    UpdateServiceLog(dr)
                Next
            End If
            'End If
        Else
            MsgBox("There is no insert or update data to save!")
        End If

        'SaveServiceLog(dr)
        'bm.Position = iPrevPosition
    End Sub

#End Region
    'Service Log checking
    Private Function CheckIsPolicyExist(policyNo As String, strPolicy As String, companyId As String) As Boolean
        Dim retDs As DataSet = New DataSet
        If String.IsNullOrEmpty(policyNo) Then
            policyNo = " "
        End If
        If Not String.IsNullOrEmpty(strPolicy) Then
            policyNo = strPolicy
        End If
        Try
            retDs = APIServiceBL.CallAPIBusi(companyId, "CHECK_POLICY_EXIST", New Dictionary(Of String, String)() From {
            {"strInPolicy", policyNo}})
            If retDs.Tables.Count > 0 AndAlso retDs.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI CHECK_POLICY_EXIST Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
        End Try
        Return False
    End Function
    '
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
                rbBer.Checked = True
            ElseIf dr.Item("PolicyType") = "LIFEB" Then
                rbBer.Checked = True
            ElseIf dr.Item("PolicyType") = "LIFEA" Then
                rbAsur.Checked = True
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

            rbBer.Checked = True
            'C001 - Start
            chkMCV.Enabled = False
            'C001 - End
        End If
        
        ' Service log enhancement - Manage enquiry tab controls
        ManageEnquiryTabControls()

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

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Insert ServiceLog to Bermuda or Assurance by using ExecuteBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move InsertServiceLog API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="dr">This datarow is ready to be inserted</param>
    Private Sub InsertServiceLog(ByVal dr As DataRow)

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

        ValidateServiceLogRow(dr)

        Dim companyName As String = ""
        Dim busiId As String = ""
        If gboPolicy.Visible Then
            If rbBer.Checked Or rbEB.Checked Then
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            ElseIf rbAsur.Checked Then
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_ASSURANCE, companyName, busiId)
            End If
        Else
            If strCompany = COMPANY_NAME_BERMUDA Then
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            ElseIf strCompany = COMPANY_NAME_ASSURANCE OrElse strCompany = COMPANY_NAME_ASSURANCE2 Then
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_ASSURANCE, companyName, busiId)
            Else
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            End If
        End If

        Try
            APIServiceBL.ExecAPIBusi(companyName, busiId,
                                New Dictionary(Of String, String) From {
                                {"EventCategoryCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventCategoryCode").ToString()), dr.Item("EventCategoryCode").ToString(), " ")},
                                {"EventTypeCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventTypeCode").ToString()), dr.Item("EventTypeCode").ToString(), " ")},
                                {"CustomerID", IIf(Not String.IsNullOrEmpty(txtCustomerID.Text), txtCustomerID.Text, strCustID)},
                                {"PolicyAccountID", dr.Item("PolicyAccountID").ToString},
                                {"gsUser", IIf(Not String.IsNullOrEmpty(gsUser), gsUser, " ")},
                                {"SecondaryCSRID", IIf(Not String.IsNullOrEmpty(dr.Item("SecondaryCSRID").ToString()), dr.Item("SecondaryCSRID").ToString(), " ")},
                                {"EventInitialDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventInitialDateTime"))},
                                {"EventAssignDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventAssignDateTime"))},
                                {"EventCompletionDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventCompletionDateTime"))},
                                {"chkFCR", IIf(chkFCR.Checked, "Y", "N")},
                                {"EventCloseoutDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventCloseoutDateTime"))},
                                {"EventStatusCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventStatusCode").ToString()), dr.Item("EventStatusCode").ToString(), " ")},
                                {"EventSourceInitiatorCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventSourceInitiatorCode").ToString()), dr.Item("EventSourceInitiatorCode").ToString(), " ")},
                                {"EventSourceMediumCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventSourceMediumCode").ToString()), dr.Item("EventSourceMediumCode").ToString(), " ")},
                                {"EventNotes", IIf(Not String.IsNullOrEmpty(dr.Item("EventNotes").ToString()), dr.Item("EventNotes").ToString(), " ")},
                                {"ReminderDate", ConvertFormatSQLDateWithoutQuotes(dr.Item("ReminderDate"))},
                                {"ReminderNotes", IIf(Not String.IsNullOrEmpty(dr.Item("ReminderNotes").ToString()), dr.Item("ReminderNotes").ToString(), " ")},
                                {"EventTypeDetailCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventTypeDetailCode").ToString()), dr.Item("EventTypeDetailCode").ToString(), " ")},
                                {"chkACC", IIf(chkACC.Checked, "Y", "N")},
                                {"isTransferToAes", IIf(Not String.IsNullOrEmpty(dr.Item("isTransferToAes").ToString()), dr.Item("isTransferToAes").ToString(), " ")},
                                {"IsAppearedInAES", IIf(Not String.IsNullOrEmpty(dr.Item("IsAppearedInAES").ToString()), dr.Item("IsAppearedInAES").ToString(), " ")},
                                {"IsPolicyAlert", IIf(Not String.IsNullOrEmpty(dr.Item("IsPolicyAlert").ToString()), dr.Item("IsPolicyAlert").ToString(), " ")},
                                {"AlertNotes", IIf(Not String.IsNullOrEmpty(dr.Item("AlertNotes").ToString()), dr.Item("AlertNotes").ToString(), " ")},
                                {"IsIdVerify", IIf(Not String.IsNullOrEmpty(dr.Item("IsIdVerify").ToString()), dr.Item("IsIdVerify").ToString(), " ")},
                                {"chkMCV", IIf(chkMCV.Checked, "Y", "N")}
                                })
            'MsgBox("Insert Data Success !", , "Insert Data Success")
        Catch ex As Exception
            MsgBox("Error occurs when inserting the comments. Error:  " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        wndMain.StatusBarPanel1.Text = "Insert Complete"
        RaiseEvent EventSaved(Me, dr)

        'oliver 2024-3-6 added for ITSR5061 Retention Offer Campaign 
        If blnIsEnableNBMPolicyPanel Then
            CheckSaveNBMPolicy(dr, maxServiceEventNumber)
        End If

        wndInbox.RefreshInbox()
        blnIsNewMode = False
        Refresh_ServiceLog()

    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Update ServiceLog to Bermuda or Assurance by using ExecuteBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move UpdateServiceLog API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="dr">This datarow is ready to be updated</param>
    Private Sub UpdateServiceLog(ByVal dr As DataRow)

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

        ValidateServiceLogRow(dr)

        If Not ValidateServiceLogUpdateTime(dr) Then
            Refresh_ServiceLog()
            Exit Sub
        End If

        Dim companyName As String = ""
        Dim busiId As String = ""
        If blnIsParallelMode Then
            If dr.Item("CompanyID") = "Bermuda" OrElse dr.Item("CompanyID") = "Private" Then
                SetCompanyNameAndBusiIdToUpdateSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            ElseIf dr.Item("CompanyID") = "Assurance" Then
                SetCompanyNameAndBusiIdToUpdateSerLog(COMPANY_NAME_ASSURANCE, companyName, busiId)
            Else
                SetCompanyNameAndBusiIdToUpdateSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            End If
        Else
            If strCompany = COMPANY_NAME_BERMUDA Then
                SetCompanyNameAndBusiIdToUpdateSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            ElseIf strCompany = COMPANY_NAME_ASSURANCE OrElse strCompany = COMPANY_NAME_ASSURANCE2 Then
                SetCompanyNameAndBusiIdToUpdateSerLog(COMPANY_NAME_ASSURANCE, companyName, busiId)
            Else
                SetCompanyNameAndBusiIdToInsertSerLog(COMPANY_NAME_BERMUDA, companyName, busiId)
            End If
        End If

        Try
            APIServiceBL.ExecAPIBusi(companyName, busiId,
                                New Dictionary(Of String, String) From {
                                {"EventCategoryCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventCategoryCode").ToString()), dr.Item("EventCategoryCode").ToString(), " ")},
                                {"EventTypeCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventTypeCode").ToString()), dr.Item("EventTypeCode").ToString(), " ")},
                                {"SecondaryCSRID", IIf(Not String.IsNullOrEmpty(dr.Item("SecondaryCSRID").ToString()), dr.Item("SecondaryCSRID").ToString(), " ")},
                                {"EventInitialDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventInitialDateTime"))},
                                {"EventAssignDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventAssignDateTime"))},
                                {"EventCompletionDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventCompletionDateTime"))},
                                {"EventCloseoutDateTime", ConvertFormatSQLDateWithoutQuotes(dr.Item("EventCloseoutDateTime"))},
                                {"EventStatusCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventStatusCode").ToString()), dr.Item("EventStatusCode").ToString(), " ")},
                                {"EventSourceInitiatorCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventSourceInitiatorCode").ToString()), dr.Item("EventSourceInitiatorCode").ToString(), " ")},
                                {"EventSourceMediumCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventSourceMediumCode").ToString()), dr.Item("EventSourceMediumCode").ToString(), " ")},
                                {"EventNotes", IIf(Not String.IsNullOrEmpty(dr.Item("EventNotes").ToString()), dr.Item("EventNotes").ToString(), " ")},
                                {"ReminderDate", ConvertFormatSQLDateWithoutQuotes(dr.Item("ReminderDate"))},
                                {"ReminderNotes", IIf(Not String.IsNullOrEmpty(dr.Item("ReminderNotes").ToString()), dr.Item("ReminderNotes").ToString(), " ")},
                                {"EventTypeDetailCode", IIf(Not String.IsNullOrEmpty(dr.Item("EventTypeDetailCode").ToString()), dr.Item("EventTypeDetailCode").ToString(), " ")},
                                {"gsUser", IIf(Not String.IsNullOrEmpty(gsUser), gsUser, " ")},
                                {"isTransferToAes", IIf(Not String.IsNullOrEmpty(dr.Item("isTransferToAes").ToString()), dr.Item("isTransferToAes").ToString(), " ")},
                                {"IsAppearedInAES", IIf(Not String.IsNullOrEmpty(dr.Item("IsAppearedInAES").ToString()), dr.Item("IsAppearedInAES").ToString(), " ")},
                                {"IsPolicyAlert", IIf(Not String.IsNullOrEmpty(dr.Item("IsPolicyAlert").ToString()), dr.Item("IsPolicyAlert").ToString(), " ")},
                                {"AlertNotes", IIf(Not String.IsNullOrEmpty(dr.Item("AlertNotes").ToString()), dr.Item("AlertNotes").ToString(), " ")},
                                {"chkFCR", IIf(chkFCR.Checked, "Y", "N")},
                                {"chkACC", IIf(chkACC.Checked, "Y", "N")},
                                {"PolicyAccountID", IIf(Not String.IsNullOrEmpty(dr.Item("PolicyAccountID").ToString()), dr.Item("PolicyAccountID").ToString(), " ")},
                                {"IsIdVerify", IIf(Not String.IsNullOrEmpty(dr.Item("IsIdVerify").ToString()), dr.Item("IsIdVerify").ToString(), " ")},
                                {"chkMCV", IIf(chkMCV.Checked, "Y", "N")},
                                {"ServiceEventNumber", IIf(Not String.IsNullOrEmpty(dr.Item("ServiceEventNumber").ToString()), dr.Item("ServiceEventNumber").ToString(), " ")}
                                })
            MsgBox("Update Data Success !", , "Update Data Success")
        Catch ex As Exception
            MsgBox("Error occurs when updating the comments. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        wndMain.StatusBarPanel1.Text = "Update Complete"
        RaiseEvent EventSaved(Me, dr)

        'oliver 2024-3-6 added for ITSR5061 Retention Offer Campaign 
        If blnIsEnableNBMPolicyPanel Then
            CheckSaveNBMPolicy(dr, maxServiceEventNumber)
        End If

        wndInbox.RefreshInbox()
        blnIsNewMode = False
        Refresh_ServiceLog()

    End Sub

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Validate ServiceLog Row
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move UpdateServiceLog and InsertServiceLog API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="dr">This datarow is ready to be validated</param>
    Private Sub ValidateServiceLogRow(ByVal dr As DataRow)
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

        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()

        If strPolicy = "" Then
            If Not IsDBNull(dr.Item("PolicyAccountNO")) Then
                If rbBer.Checked Or rbAsur.Checked Then
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

    End Sub


    ''' <summary>
    ''' Customer Level Search Issue
    ''' Validate ServiceLog UpdateTime
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-22 by oliver which Maintain data consistency When Updating ServiceLog, check whether it Is latter than the updatetime.Or Not cancel update And refresh display</br>
    ''' </remarks>
    ''' <param name="dr">This datarow is ready to be validated</param>
    Private Function ValidateServiceLogUpdateTime(ByVal dr As DataRow) As Boolean
        Dim companyName As String = ""
        Dim busiId As String = ""
        If blnIsParallelMode Then
            If dr.Item("CompanyID") = "Bermuda" OrElse dr.Item("CompanyID") = "Private" Then
                SetCompanyNameAndBusiIdToGetSerLogUpdateTime(COMPANY_NAME_BERMUDA, companyName, busiId)
            ElseIf dr.Item("CompanyID") = "Assurance" Then
                SetCompanyNameAndBusiIdToGetSerLogUpdateTime(COMPANY_NAME_ASSURANCE, companyName, busiId)
            Else
                SetCompanyNameAndBusiIdToGetSerLogUpdateTime(COMPANY_NAME_BERMUDA, companyName, busiId)
            End If
        Else
            SetCompanyNameAndBusiIdToGetSerLogUpdateTime(strCompany, companyName, busiId)
        End If

        Dim lastUpdateTimeActual As DateTime = GetServiceLogUpdateTime(dr.Item("ServiceEventNumber"), companyName, busiId)
        Dim lastUpdateTime As DateTime = dr.Item("updatedatetime")
        Dim dt01 = DateTime.Compare(lastUpdateTimeActual, lastUpdateTime)
        If DateTime.Compare(lastUpdateTimeActual, lastUpdateTime) Then
            MsgBox("The record is updated by another user on " & vbCrLf & ConvertFormatSQLDateWithoutQuotes(lastUpdateTimeActual) & vbCrLf & "Please wait and then click the row again when the page refreshed", MsgBoxStyle.Exclamation, "Concurrency Error")
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Get ServiceLog UpdateTime for Bermuda or Assurance by using ExecuteBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-18 by oliver which Move UpdateServiceLog API From CRSFrontEnd To CRSAPI</br>
    ''' </remarks>
    ''' <param name="serviceEventNumber">Represents the filter which is ServiceEventNumber of get the ServiceLog UpdateTime</param>
    ''' <param name="companyName">Represents the company name to access different databases</param>
    ''' <param name="busiId">Represents the busiId to access different API</param>
    Private Function GetServiceLogUpdateTime(ByVal serviceEventNumber As String, ByVal companyName As String, ByVal busiId As String) As DateTime
        Try
            Dim dataSet As DataSet = APIServiceBL.CallAPIBusi(companyName, busiId, New Dictionary(Of String, String)() From {{"ServiceEventNumber", serviceEventNumber}})
            Dim lastUpdateTimeActual As DateTime
            If Not IsNothing(dataSet) Then
                lastUpdateTimeActual = CType(dataSet.Tables(0).Rows(0).Item("UpdateDatetime"), DateTime)
            End If
            Return lastUpdateTimeActual
        Catch ex As Exception
            Throw
        End Try
    End Function

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
        Refresh_ServiceLog()
    End Sub

    'updated at 2023-9-22 by oliver for Customer Level Search Issue
    Private Sub dgSrvLog_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgSrvLog.CurrentCellChanged

        'Check save
        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()
        'blnSave = False
        If dsSrvLog.HasChanges Then
            If strNewFlag = "Y" Then
                strNewFlag = "N"
            Else
                'If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
                Dim dr As DataRow
                Dim insertedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Added)
                If Not IsNothing(insertedDataTable) Then
                    For Each dr In insertedDataTable.Rows
                        InsertServiceLog(dr)
                    Next
                End If
                Dim updatedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Modified)
                If Not IsNothing(updatedDataTable) Then
                    For Each dr In updatedDataTable.Rows
                        UpdateServiceLog(dr)
                    Next
                End If
                'End If

                Exit Sub
            End If
        End If

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

    'updated at 2023-9-22 by oliver for Customer Level Search Issue
    Public Sub Refresh_ServiceLog()
        'Service Log enhancement
        ' Reset enquiry tab visited flags when refreshing service log
        bln1stEnquiryVisited = False
        bln2ndEnquiryVisited = False
        bln3rdEnquiryVisited = False
        '
        dsSrvLog.Tables("ServiceEventDetail").Clear()
        If blnIsParallelMode AndAlso blnIsBothComp AndAlso Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX") Then
            GetParallelModeServiceLogByAPI()
        Else
            GetSingleModeServiceLogByAPI()
        End If

        dgSrvLog.DataSource = dsSrvLog.Tables("ServiceEventDetail")
        chkAES.DataBindings.Remove(chkAES.DataBindings.Item("Checked"))
        hidSrvEvtNo.DataBindings.Remove(hidSrvEvtNo.DataBindings.Item("Text"))
        hidPolicy.DataBindings.Remove(hidPolicy.DataBindings.Item("Text"))
        hidCustID.DataBindings.Remove(hidCustID.DataBindings.Item("Text"))
        hidSender.DataBindings.Remove(hidSender.DataBindings.Item("Text"))

        cbMedium.DataBindings.Remove(cbMedium.DataBindings.Item("SelectedValue"))
        cbStatus.DataBindings.Remove(cbStatus.DataBindings.Item("SelectedValue"))
        cbInitiator.DataBindings.Remove(cbInitiator.DataBindings.Item("SelectedValue"))
        cbEventCat.DataBindings.Remove(cbEventCat.DataBindings.Item("SelectedValue"))
        cbEventDetail.DataBindings.Remove(cbEventDetail.DataBindings.Item("SelectedValue"))
        cbEventTypeDetail.DataBindings.Remove(cbEventTypeDetail.DataBindings.Item("SelectedValue"))
        cbReceiver.DataBindings.Remove(cbReceiver.DataBindings.Item("SelectedValue"))
        
        'Service Log enhancement
        ' Remove data bindings for new enquiry controls with error handling
        Try
            cb1stEventCat.DataBindings.Remove(cb1stEventCat.DataBindings.Item("SelectedValue"))
            cb1stEventDetail.DataBindings.Remove(cb1stEventDetail.DataBindings.Item("SelectedValue"))
            cb1stEventTypeDetail.DataBindings.Remove(cb1stEventTypeDetail.DataBindings.Item("SelectedValue"))
            txt1stReason.DataBindings.Remove(txt1stReason.DataBindings.Item("Text"))
            txt1stAlternative.DataBindings.Remove(txt1stAlternative.DataBindings.Item("Text"))

            cb2ndEventCat.DataBindings.Remove(cb2ndEventCat.DataBindings.Item("SelectedValue"))
            cb2ndEventDetail.DataBindings.Remove(cb2ndEventDetail.DataBindings.Item("SelectedValue"))
            cb2ndEventTypeDetail.DataBindings.Remove(cb2ndEventTypeDetail.DataBindings.Item("SelectedValue"))
            txt2ndReason.DataBindings.Remove(txt2ndReason.DataBindings.Item("Text"))
            txt2ndAlternative.DataBindings.Remove(txt2ndAlternative.DataBindings.Item("Text"))

            cb3rdEventCat.DataBindings.Remove(cb3rdEventCat.DataBindings.Item("SelectedValue"))
            cb3rdEventDetail.DataBindings.Remove(cb3rdEventDetail.DataBindings.Item("SelectedValue"))
            cb3rdEventTypeDetail.DataBindings.Remove(cb3rdEventTypeDetail.DataBindings.Item("SelectedValue"))
            txt3rdReason.DataBindings.Remove(txt3rdReason.DataBindings.Item("Text"))
            txt3rdAlternative.DataBindings.Remove(txt3rdAlternative.DataBindings.Item("Text"))
        Catch ex As Exception
            ' Handle case where bindings don't exist yet
            System.Diagnostics.Debug.WriteLine("Error removing enquiry bindings: " & ex.Message)
        End Try
        '
        dtInitial.DataBindings.Remove(dtInitial.DataBindings.Item("Value"))

        txtPolicyAlert.DataBindings.Remove(txtPolicyAlert.DataBindings.Item("Text"))
        txtReminder.DataBindings.Remove(txtReminder.DataBindings.Item("Text"))
        txtNotes.DataBindings.Remove(txtNotes.DataBindings.Item("Text"))
        chkPolicyAlert.DataBindings.Remove(chkPolicyAlert.DataBindings.Item("Checked"))

        chkIdVerify.DataBindings.Remove(chkIdVerify.DataBindings.Item("Checked"))

        If strPolicy = "" Then
            txtPolicyNo.DataBindings.Remove(txtPolicyNo.DataBindings.Item("Text"))
        End If

        If blnIsCustLevel Then
            txtCustomerID.DataBindings.Remove(txtCustomerID.DataBindings.Item("Text"))
        End If

        dtReminder.DataBindings.Remove(dtReminder.DataBindings.Item("Value"))
        InitForm()
        CheckEnableNBMPanel()
        
        ' Service log enhancement - Manage enquiry tab controls after refresh
        ManageEnquiryTabControls()
        
        ' Reset tab selection to 1st Enquiry when switching records
        If tabEnquiry.TabPages.Count > 0 Then
            tabEnquiry.SelectedIndex = 0
        End If

    End Sub

    'updated by oliver for Customer Level Search Issue
    Private Sub btnSaveC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveC.Click

        'Post Sales Call - Start
        If cbEventCat.Text.Trim = "Courtesy Call - Non-vulnerable customer" AndAlso Me.rad_NVCWelcomeCall.Checked <> True Then
            MsgBox("Event Catergory not match Post Sales Call Status")
            Exit Sub
        End If

        If cbEventCat.Text.Trim = "Courtesy Call - Suitability mismatch" AndAlso Me.rad_SuitabilityMisMatch.Checked <> True Then
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

        Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).EndCurrentEdit()
        'SaveServiceLog(dr)
        If dsSrvLog.HasChanges Then
            'If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
            Dim dr As DataRow
            Dim insertedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Added)
            If Not IsNothing(insertedDataTable) Then
                For Each dr In insertedDataTable.Rows
                    InsertServiceLog(dr)
                Next
            End If
            Dim updatedDataTable = dsSrvLog.Tables("ServiceEventDetail").GetChanges(DataRowState.Modified)
            If Not IsNothing(updatedDataTable) Then
                For Each dr In updatedDataTable.Rows
                    UpdateServiceLog(dr)
                Next
            End If
            'End If

        Else
            MsgBox("There is no insert or update data to save!")
        End If
        'Close Window
        CType(Me.Parent.Parent.Parent, Form).Close()
    End Sub


    'added by ITDYMH 20150229 Post-Sales Call
    Private Sub getPostSalesCallInfo()

        Dim s_Sql As String = "cswsp_GetPostSalesCallList"

        If Not sqlConn.State = ConnectionState.Open Then
            sqlConn.ConnectionString = GetConnectionStringByCompanyID(strCompany)
            sqlConn.Open()
        End If

        daPostSalesCallInfo = New SqlDataAdapter(s_Sql, sqlConn)
        daPostSalesCallInfo.SelectCommand.CommandTimeout = sqlConn.ConnectionTimeout

        Try
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = IIf(strPolicy Is Nothing, "", strPolicy)
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.CommandType = CommandType.StoredProcedure
            daPostSalesCallInfo.Fill(dsSrvLog, "PostSalesCallInfo")

            'doRefreshPostCallInfo()

        Catch e As Exception
            'Temp Comment
            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
            appSettings.Logger.logger.Error(e.Message.ToString)
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
        Dim type As String = "HK"
        If (strCompany = "LAC" Or strCompany = "LAH") Then
            type = "Assurance"
        End If
        If Not ServiceLogBL.GetSerLogbycriteriaNew("1900-01-01", "1900-01-01", "", "", strCustID, False, True, dsresult, strErr, type) Then
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
            sqlConn2.ConnectionString = GetConnectionStringByCompanyID(strCompany)
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
            sqlConn2.ConnectionString = GetConnectionStringByCompanyID(strCompany)
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
    Private Function GetPiAuth(ByVal strPolicy As String) As DataTable
        Try
            Dim ds As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(strCompany), "GET_PI_AUTH", New Dictionary(Of String, String) From {
                {"strPolicyNo", strPolicy}
            })
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0).Copy()
            End If
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try

        Return New DataTable()
    End Function

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function UpdatePiAuth(ByVal policyAccountID As String, ByVal piCustID As String, ByVal enable As String, ByVal usr As String) As Boolean
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(strCompany), "UPDATE_PI_AUTH",
                                New Dictionary(Of String, String) From {
                                {"PolicyAccountID", Trim(policyAccountID)},
                                {"PICustID", Trim(piCustID)},
                                {"Enable", enable},
                                {"usr", usr}
                                })
            Return True
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when updating the comments. Error:  " & ex.Message)
        End Try
        Return False
    End Function

    Private Function GetPiAuthOtp(customerID As String) As Dictionary(Of String, String)
        Dim returnObj As New Dictionary(Of String, String)

        Try
            Dim objclsPiAuthResponse As CRS_Util.clsJSONBusinessObj.clsPiAuthResponse = CRS_Util.clsJSONTool.CallPiAuthGenKey(customerID, gsUser)
            If objclsPiAuthResponse IsNot Nothing Then
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
                        sqlConn2.ConnectionString = GetConnectionStringByCompanyID(strCompany)
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

                    daTmp = New SqlDataAdapter($"select [Product].[Description], [Product_Chi].ChineseDescription from [PolicyAccount] with (nolock) inner join [Product] with (nolock) on [PolicyAccount].ProductID = [Product].ProductID inner join {gcNBSDB}[Product_Chi] with (nolock) on [PolicyAccount].ProductID = [Product_Chi].ProductID where [PolicyAccount].PolicyAccountID = @PolicyAccountID", sqlConn2)
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
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try

        Return returnObj
    End Function

    Private Function SendPiMail(dOtp As Dictionary(Of String, String), toEmail As String, ccEmail As String) As Boolean
        Try
            Dim strToMail As String = toEmail
            Dim strCcMailArry As String() = If(ccEmail, String.Empty).Split({";"}, StringSplitOptions.RemoveEmptyEntries)
            Dim strSubject As String = "FWD eServices registration request <policy no. " & strPolicy & ">「富衛eServices」賬戶申請 <保單號碼 " & strPolicy & ">"
            Dim strFrMail As String = "cs.hk@fwd.com"
            Dim strAttachmentPath As String = ""
            Dim strContent As String = IO.File.ReadAllText(".\CETPEP.bin")

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
            Dim bccAddr As New List(Of CommonWS.SerializableMailAddress)(strCcMailArry.Length)

            fromAddr.Address = strFrMail

            toAddr(0) = New CommonWS.SerializableMailAddress With {
                .Address = strToMail
            }

            For Each strCCMail As String In strCcMailArry
                bccAddr.Add(New CommonWS.SerializableMailAddress With {
                    .Address = strCCMail
                })
            Next

            mailMsg.From = fromAddr
            mailMsg.To = toAddr
            mailMsg.Subject = strSubject
            mailMsg.Body = strContent
            mailMsg.Bcc = bccAddr.ToArray()
            mailMsg.IsBodyHtml = True
            mailAttachment.ContentStream = IO.File.ReadAllBytes("c:\prodapps\La dll\image2103.jpg")
            mailAttachment.ContentId = "image2103.jpg@01D19732.1B190AC0"
            Dim attachments(0) As CommonWS.SerializableAttachment
            attachments(0) = mailAttachment
            mailMsg.Attachments = attachments

            Using ws As CommonWS.Service = GetCommonWS()
                ws.SendExternalMail(mailMsg)
            End Using
        Catch ex As Exception
            AsyncDbLogger.LogError(gsUser, "Service Log", "SendPiMail Error", ex.ToString())
            Return False
        End Try
        Return True
    End Function

    Private Function SendBrokerMail(dOtp As Dictionary(Of String, String), toEmail As String, ccEmail As String) As Boolean
        Try
            Dim strToMail As String = toEmail
            Dim strCcMailArry As String() = If(ccEmail, String.Empty).Split({";"}, StringSplitOptions.RemoveEmptyEntries)
            Dim strSubject As String = "FWD eServices registration request <policy no. " & strPolicy & ">「富衛eServices」賬戶申請 <保單號碼 " & strPolicy & ">"
            Dim strFrMail As String = "cs.hk@fwd.com"
            Dim strAttachmentPath As String = ""
            Dim strContent As String = IO.File.ReadAllText(".\CETPEB.bin")

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
            Dim ccAddr As New List(Of CommonWS.SerializableMailAddress)(strCcMailArry.Length)

            fromAddr.Address = strFrMail

            toAddr(0) = New CommonWS.SerializableMailAddress With {
                .Address = strToMail
            }

            For Each strCCMail As String In strCcMailArry
                ccAddr.Add(New CommonWS.SerializableMailAddress With {
                    .Address = strCCMail
                })
            Next

            mailMsg.From = fromAddr
            mailMsg.To = toAddr
            mailMsg.Subject = strSubject
            mailMsg.Body = strContent
            mailMsg.CC = ccAddr.ToArray()
            mailMsg.IsBodyHtml = True
            mailAttachment.ContentStream = IO.File.ReadAllBytes("c:\prodapps\La dll\image2103.jpg")
            mailAttachment.ContentId = "image2103.jpg@01D19732.1B190AC0"
            Dim attachments(0) As CommonWS.SerializableAttachment
            attachments(0) = mailAttachment
            mailMsg.Attachments = attachments

            Using ws As CommonWS.Service = GetCommonWS()
                ws.SendExternalMail(mailMsg)
            End Using
        Catch ex As Exception
            AsyncDbLogger.LogError(gsUser, "Service Log", "SendBrokerMail Error", ex.ToString())
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
        If Not dgvAuthPi.SelectedRows.Count = 1 Then
            MessageBox.Show("Please select one PI user for Auth")
            Exit Sub
        End If

        If dgvAuthPi.SelectedRows(0).Cells("Auth Date").Value.ToString.Length > 0 Then
            bDo = MessageBox.Show("Auth PI user already, do you want to re-gen Auth OTP?", "Question", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes
        End If

        If bDo Then
            If isInForce() Then
                If isCooling() Then
                    Dim sPiID As String = dgvAuthPi.SelectedRows(0).Cells("PI ID").Value.ToString
                    Dim sPiEmail As String = dgvAuthPi.SelectedRows(0).Cells("Email").Value.ToString.Trim
                    Dim sPiBccEmail As String = GetCodeTableValue("SPBCCAdd")
                    Dim sBrokerEmail As String = dgvAuthPi.SelectedRows(0).Cells("Broker Email").Value.ToString.Trim
                    Dim sBrokerCcEmail As String = GetCodeTableValue("SPCCAdd")

                    If sPiEmail.Length > 0 AndAlso sBrokerEmail.Length > 0 Then
                        'oliver 2024-4-24 updated for Table_Relocate_Sprint13
                        If UpdatePiAuth(strPolicy, sPiID, "Y", gsUser) Then
                            Dim dOtp As Dictionary(Of String, String) = GetPiAuthOtp(sPiID)
                            If dOtp.ContainsKey("otp") AndAlso dOtp("otp").Trim.Length > 0 Then
                                LogOTPInfo(dOtp, sPiEmail, sPiBccEmail, sBrokerEmail, sBrokerCcEmail)

                                bPiMail = SendPiMail(dOtp, sPiEmail, sPiBccEmail)
                                bBrokerMail = SendBrokerMail(dOtp, sBrokerEmail, sBrokerCcEmail)

                                If bPiMail And bBrokerMail Then
                                    MessageBox.Show("PI Auth done")
                                Else
                                    Dim sErrMsg As String = ""
                                    If Not bPiMail Then
                                        sErrMsg &= "Fail send mail to PI" & vbNewLine
                                    End If
                                    If Not bBrokerMail Then
                                        sErrMsg &= "Fail send mail to Broker"
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
                        MessageBox.Show("PI / Broker Email address not valid")
                    End If
                Else
                    MessageBox.Show("Policy is not ready yet")
                End If
            Else
                MessageBox.Show("It is not InForce policy")
            End If
        End If
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
            '    sqlConn2.ConnectionString = GetConnectionStringByCompanyID(strCompany)
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

    Private Sub LogOTPInfo(otpDict As Dictionary(Of String, String), toEmail As String, ccEmail As String, brokerEmail As String, brokerCcEmail As String)
        Dim sb As New StringBuilder()
        sb.AppendLine("Send Auth Email:")
        sb.AppendLine("PH_FirstName : " + otpDict("PH_FirstName"))
        sb.AppendLine("PH_NameSuffix : " + otpDict("PH_NameSuffix"))
        sb.AppendLine("PI_FirstName : " + otpDict("PI_FirstName"))
        sb.AppendLine("PI_NameSuffix : " + otpDict("PI_NameSuffix"))
        sb.AppendLine("Product : " + otpDict("Product"))
        sb.AppendLine("ProductChi : " + otpDict("ProductChi"))
        sb.AppendLine("otp : " + otpDict("otp"))
        sb.AppendLine("expiry : " + otpDict("expiry"))
        sb.AppendLine("expiryY : " + otpDict("expiryY"))
        sb.AppendLine("expiryM : " + otpDict("expiryM"))
        sb.AppendLine("expiryD : " + otpDict("expiryD"))
        sb.AppendLine("ToMail : " + toEmail)
        sb.AppendLine("BccMail : " + ccEmail)
        sb.AppendLine("BrokerMail : " + brokerEmail)
        sb.AppendLine("BrokerCcMail : " + brokerCcEmail)

        AsyncDbLogger.LogInfo(gsUser, "Service Log", sb.ToString())
    End Sub

End Class

