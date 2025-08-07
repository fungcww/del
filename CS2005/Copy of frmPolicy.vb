Imports System.Threading
Imports System.Data.SqlClient

Public Class frmPolicy
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.tcPolicy.Controls.Clear()
        Me.tcPolicy.Controls.Add(Me.tabPolicySummary)
        Me.tcPolicy.Controls.Add(Me.tabCoverageDetails)
        Me.tcPolicy.Controls.Add(Me.tabDDACCD)
        Me.tcPolicy.Controls.Add(Me.tabPaymentHistory)
        Me.tcPolicy.Controls.Add(Me.tabCustomerHistory)
        Me.tcPolicy.Controls.Add(Me.tabClaimsHistory)
        Me.tcPolicy.Controls.Add(Me.tabServiceLog)
        Me.tcPolicy.Controls.Add(Me.tabFinancialInfo)
        Me.tcPolicy.Controls.Add(Me.tabAgentInfo)
        Me.tcPolicy.Controls.Add(Me.tabUnderwriting)
        'Me.tcPolicy.Controls.Add(Me.tabTransactionHistory)
        Me.tcPolicy.Controls.Add(Me.tabCouponHistory)
        Me.tcPolicy.Controls.Add(Me.tabSMS)
        'Me.tcPolicy.Controls.Add(Me.tabWebNews)
        Me.tcPolicy.Controls.Add(Me.tabAPLHistory)

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents Payh1 As CS2005.PAYH
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tcPolicy As System.Windows.Forms.TabControl
    Friend WithEvents tabPolicySummary As System.Windows.Forms.TabPage
    Friend WithEvents tabCustomerHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabClaimsHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabServiceLog As System.Windows.Forms.TabPage
    Friend WithEvents tabUnderwriting As System.Windows.Forms.TabPage
    Friend WithEvents tabCoverageDetails As System.Windows.Forms.TabPage
    Friend WithEvents tabTransactionHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabFinancialInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabPaymentHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabAgentInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabDDACCD As System.Windows.Forms.TabPage
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PolicySummary1 As CS2005.PolicySummary
    Friend WithEvents AddressSelect1 As CS2005.AddressSelect
    Friend WithEvents Coverage1 As CS2005.Coverage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents ClaimHist1 As CS2005.ClaimHist
    Friend WithEvents tabSMS As System.Windows.Forms.TabPage
    Friend WithEvents Sms1 As CS2005.SMS
    Friend WithEvents AgentInfo1 As CS2005.AgentInfo
    Friend WithEvents CustHist1 As CS2005.CustHist
    Friend WithEvents Ddaccdr1 As CS2005.DDACCDR
    Friend WithEvents UwInfo1 As CS2005.UWInfo
    Friend WithEvents tabCouponHistory As System.Windows.Forms.TabPage
    Friend WithEvents Couh1 As CS2005.COUH
    Friend WithEvents FinancialInfo1 As CS2005.FinancialInfo
    Friend WithEvents UclServiceLog1 As CS2005.uclServiceLog
    Friend WithEvents tabAPLHistory As System.Windows.Forms.TabPage
    Friend WithEvents AplHist1 As CS2005.APLHist
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPolicy))
        Me.tcPolicy = New System.Windows.Forms.TabControl
        Me.tabPolicySummary = New System.Windows.Forms.TabPage
        Me.PolicySummary1 = New CS2005.PolicySummary
        Me.AddressSelect1 = New CS2005.AddressSelect
        Me.tabDDACCD = New System.Windows.Forms.TabPage
        Me.Ddaccdr1 = New CS2005.DDACCDR
        Me.tabCustomerHistory = New System.Windows.Forms.TabPage
        Me.CustHist1 = New CS2005.CustHist
        Me.tabTransactionHistory = New System.Windows.Forms.TabPage
        Me.tabServiceLog = New System.Windows.Forms.TabPage
        Me.UclServiceLog1 = New CS2005.uclServiceLog
        Me.tabCoverageDetails = New System.Windows.Forms.TabPage
        Me.Coverage1 = New CS2005.Coverage
        Me.tabPaymentHistory = New System.Windows.Forms.TabPage
        Me.Payh1 = New CS2005.PAYH
        Me.tabUnderwriting = New System.Windows.Forms.TabPage
        Me.UwInfo1 = New CS2005.UWInfo
        Me.tabCouponHistory = New System.Windows.Forms.TabPage
        Me.Couh1 = New CS2005.COUH
        Me.tabFinancialInfo = New System.Windows.Forms.TabPage
        Me.FinancialInfo1 = New CS2005.FinancialInfo
        Me.tabAPLHistory = New System.Windows.Forms.TabPage
        Me.AplHist1 = New CS2005.APLHist
        Me.tabClaimsHistory = New System.Windows.Forms.TabPage
        Me.ClaimHist1 = New CS2005.ClaimHist
        Me.tabAgentInfo = New System.Windows.Forms.TabPage
        Me.AgentInfo1 = New CS2005.AgentInfo
        Me.tabSMS = New System.Windows.Forms.TabPage
        Me.Sms1 = New CS2005.SMS
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtPolicy = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProduct = New System.Windows.Forms.TextBox
        Me.txtChiName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.tcPolicy.SuspendLayout()
        Me.tabPolicySummary.SuspendLayout()
        Me.tabDDACCD.SuspendLayout()
        Me.tabCustomerHistory.SuspendLayout()
        Me.tabServiceLog.SuspendLayout()
        Me.tabCoverageDetails.SuspendLayout()
        Me.tabPaymentHistory.SuspendLayout()
        Me.tabUnderwriting.SuspendLayout()
        Me.tabCouponHistory.SuspendLayout()
        Me.tabFinancialInfo.SuspendLayout()
        Me.tabAPLHistory.SuspendLayout()
        Me.tabClaimsHistory.SuspendLayout()
        Me.tabAgentInfo.SuspendLayout()
        Me.tabSMS.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcPolicy
        '
        Me.tcPolicy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcPolicy.Controls.Add(Me.tabPolicySummary)
        Me.tcPolicy.Controls.Add(Me.tabCustomerHistory)
        Me.tcPolicy.Controls.Add(Me.tabDDACCD)
        Me.tcPolicy.Controls.Add(Me.tabTransactionHistory)
        Me.tcPolicy.Controls.Add(Me.tabServiceLog)
        Me.tcPolicy.Controls.Add(Me.tabCoverageDetails)
        Me.tcPolicy.Controls.Add(Me.tabPaymentHistory)
        Me.tcPolicy.Controls.Add(Me.tabUnderwriting)
        Me.tcPolicy.Controls.Add(Me.tabCouponHistory)
        Me.tcPolicy.Controls.Add(Me.tabFinancialInfo)
        Me.tcPolicy.Controls.Add(Me.tabAPLHistory)
        Me.tcPolicy.Controls.Add(Me.tabClaimsHistory)
        Me.tcPolicy.Controls.Add(Me.tabAgentInfo)
        Me.tcPolicy.Controls.Add(Me.tabSMS)
        Me.tcPolicy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcPolicy.HotTrack = True
        Me.tcPolicy.ImageList = Me.ImageList1
        Me.tcPolicy.Location = New System.Drawing.Point(0, 56)
        Me.tcPolicy.Name = "tcPolicy"
        Me.tcPolicy.SelectedIndex = 0
        Me.tcPolicy.Size = New System.Drawing.Size(764, 408)
        Me.tcPolicy.TabIndex = 1
        Me.tcPolicy.Tag = "Policy Information"
        '
        'tabPolicySummary
        '
        Me.tabPolicySummary.AutoScroll = True
        Me.tabPolicySummary.BackColor = System.Drawing.SystemColors.Control
        Me.tabPolicySummary.Controls.Add(Me.PolicySummary1)
        Me.tabPolicySummary.Controls.Add(Me.AddressSelect1)
        Me.tabPolicySummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPolicySummary.ForeColor = System.Drawing.Color.Black
        Me.tabPolicySummary.Location = New System.Drawing.Point(4, 23)
        Me.tabPolicySummary.Name = "tabPolicySummary"
        Me.tabPolicySummary.Size = New System.Drawing.Size(756, 381)
        Me.tabPolicySummary.TabIndex = 0
        Me.tabPolicySummary.Text = "Policy Summary"
        '
        'PolicySummary1
        '
        Me.PolicySummary1.Location = New System.Drawing.Point(0, 0)
        Me.PolicySummary1.Name = "PolicySummary1"
        Me.PolicySummary1.PolicyAccountID = Nothing
        Me.PolicySummary1.Size = New System.Drawing.Size(728, 304)
        Me.PolicySummary1.TabIndex = 0
        '
        'AddressSelect1
        '
        Me.AddressSelect1.Location = New System.Drawing.Point(0, 304)
        Me.AddressSelect1.Name = "AddressSelect1"
        Me.AddressSelect1.Size = New System.Drawing.Size(720, 300)
        Me.AddressSelect1.TabIndex = 4
        '
        'tabDDACCD
        '
        Me.tabDDACCD.AutoScroll = True
        Me.tabDDACCD.Controls.Add(Me.Ddaccdr1)
        Me.tabDDACCD.ImageIndex = 3
        Me.tabDDACCD.Location = New System.Drawing.Point(4, 23)
        Me.tabDDACCD.Name = "tabDDACCD"
        Me.tabDDACCD.Size = New System.Drawing.Size(756, 381)
        Me.tabDDACCD.TabIndex = 3
        Me.tabDDACCD.Text = "DDA/CCDR"
        Me.tabDDACCD.Visible = False
        '
        'Ddaccdr1
        '
        Me.Ddaccdr1.Location = New System.Drawing.Point(0, 0)
        Me.Ddaccdr1.Name = "Ddaccdr1"
        Me.Ddaccdr1.Size = New System.Drawing.Size(724, 504)
        Me.Ddaccdr1.TabIndex = 0
        '
        'tabCustomerHistory
        '
        Me.tabCustomerHistory.Controls.Add(Me.CustHist1)
        Me.tabCustomerHistory.ImageIndex = 3
        Me.tabCustomerHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabCustomerHistory.Name = "tabCustomerHistory"
        Me.tabCustomerHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabCustomerHistory.TabIndex = 6
        Me.tabCustomerHistory.Text = "Customer History"
        Me.tabCustomerHistory.Visible = False
        '
        'CustHist1
        '
        Me.CustHist1.Location = New System.Drawing.Point(0, 0)
        Me.CustHist1.Name = "CustHist1"
        Me.CustHist1.Size = New System.Drawing.Size(720, 280)
        Me.CustHist1.TabIndex = 0
        '
        'tabTransactionHistory
        '
        Me.tabTransactionHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabTransactionHistory.Name = "tabTransactionHistory"
        Me.tabTransactionHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabTransactionHistory.TabIndex = 13
        Me.tabTransactionHistory.Text = "Transaction History"
        Me.tabTransactionHistory.Visible = False
        '
        'tabServiceLog
        '
        Me.tabServiceLog.Controls.Add(Me.UclServiceLog1)
        Me.tabServiceLog.Location = New System.Drawing.Point(4, 23)
        Me.tabServiceLog.Name = "tabServiceLog"
        Me.tabServiceLog.Size = New System.Drawing.Size(756, 381)
        Me.tabServiceLog.TabIndex = 8
        Me.tabServiceLog.Text = "Service Log"
        Me.tabServiceLog.Visible = False
        '
        'UclServiceLog1
        '
        Me.UclServiceLog1.AutoScroll = True
        Me.UclServiceLog1.CustomerID = Nothing
        Me.UclServiceLog1.Location = New System.Drawing.Point(0, 0)
        Me.UclServiceLog1.Name = "UclServiceLog1"
        Me.UclServiceLog1.PolicyAccountID = Nothing
        Me.UclServiceLog1.Size = New System.Drawing.Size(716, 436)
        Me.UclServiceLog1.TabIndex = 0
        '
        'tabCoverageDetails
        '
        Me.tabCoverageDetails.AutoScroll = True
        Me.tabCoverageDetails.Controls.Add(Me.Coverage1)
        Me.tabCoverageDetails.ImageIndex = 3
        Me.tabCoverageDetails.Location = New System.Drawing.Point(4, 23)
        Me.tabCoverageDetails.Name = "tabCoverageDetails"
        Me.tabCoverageDetails.Size = New System.Drawing.Size(756, 381)
        Me.tabCoverageDetails.TabIndex = 1
        Me.tabCoverageDetails.Text = "Coverage Details"
        Me.tabCoverageDetails.Visible = False
        '
        'Coverage1
        '
        Me.Coverage1.Location = New System.Drawing.Point(0, 0)
        Me.Coverage1.Name = "Coverage1"
        Me.Coverage1.PolicyAccountID = Nothing
        Me.Coverage1.Size = New System.Drawing.Size(756, 604)
        Me.Coverage1.TabIndex = 0
        '
        'tabPaymentHistory
        '
        Me.tabPaymentHistory.AutoScroll = True
        Me.tabPaymentHistory.Controls.Add(Me.Payh1)
        Me.tabPaymentHistory.ImageIndex = 3
        Me.tabPaymentHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabPaymentHistory.Name = "tabPaymentHistory"
        Me.tabPaymentHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabPaymentHistory.TabIndex = 4
        Me.tabPaymentHistory.Text = "Payment History"
        Me.tabPaymentHistory.Visible = False
        '
        'Payh1
        '
        Me.Payh1.Location = New System.Drawing.Point(-4, 0)
        Me.Payh1.Name = "Payh1"
        Me.Payh1.Size = New System.Drawing.Size(740, 376)
        Me.Payh1.TabIndex = 0
        '
        'tabUnderwriting
        '
        Me.tabUnderwriting.Controls.Add(Me.UwInfo1)
        Me.tabUnderwriting.ImageIndex = 3
        Me.tabUnderwriting.Location = New System.Drawing.Point(4, 23)
        Me.tabUnderwriting.Name = "tabUnderwriting"
        Me.tabUnderwriting.Size = New System.Drawing.Size(756, 381)
        Me.tabUnderwriting.TabIndex = 12
        Me.tabUnderwriting.Text = "Underwriting"
        Me.tabUnderwriting.Visible = False
        '
        'UwInfo1
        '
        Me.UwInfo1.Location = New System.Drawing.Point(8, 8)
        Me.UwInfo1.Name = "UwInfo1"
        Me.UwInfo1.Size = New System.Drawing.Size(720, 252)
        Me.UwInfo1.TabIndex = 0
        '
        'tabCouponHistory
        '
        Me.tabCouponHistory.Controls.Add(Me.Couh1)
        Me.tabCouponHistory.ImageIndex = 3
        Me.tabCouponHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabCouponHistory.Name = "tabCouponHistory"
        Me.tabCouponHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabCouponHistory.TabIndex = 15
        Me.tabCouponHistory.Text = "Coupon History"
        '
        'Couh1
        '
        Me.Couh1.DateFrom = New Date(CType(0, Long))
        Me.Couh1.Location = New System.Drawing.Point(0, 0)
        Me.Couh1.Name = "Couh1"
        Me.Couh1.PolicyAccountID = Nothing
        Me.Couh1.Size = New System.Drawing.Size(720, 252)
        Me.Couh1.TabIndex = 0
        '
        'tabFinancialInfo
        '
        Me.tabFinancialInfo.Controls.Add(Me.FinancialInfo1)
        Me.tabFinancialInfo.Location = New System.Drawing.Point(4, 23)
        Me.tabFinancialInfo.Name = "tabFinancialInfo"
        Me.tabFinancialInfo.Size = New System.Drawing.Size(756, 381)
        Me.tabFinancialInfo.TabIndex = 9
        Me.tabFinancialInfo.Text = "Financial Information"
        Me.tabFinancialInfo.Visible = False
        '
        'FinancialInfo1
        '
        Me.FinancialInfo1.Location = New System.Drawing.Point(0, 0)
        Me.FinancialInfo1.Name = "FinancialInfo1"
        Me.FinancialInfo1.Size = New System.Drawing.Size(724, 328)
        Me.FinancialInfo1.TabIndex = 0
        '
        'tabAPLHistory
        '
        Me.tabAPLHistory.AutoScroll = True
        Me.tabAPLHistory.Controls.Add(Me.AplHist1)
        Me.tabAPLHistory.ImageIndex = 3
        Me.tabAPLHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabAPLHistory.Name = "tabAPLHistory"
        Me.tabAPLHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabAPLHistory.TabIndex = 16
        Me.tabAPLHistory.Text = "APL History"
        '
        'AplHist1
        '
        Me.AplHist1.DateFrom = New Date(CType(0, Long))
        Me.AplHist1.Location = New System.Drawing.Point(0, 0)
        Me.AplHist1.Name = "AplHist1"
        Me.AplHist1.PolicyAccountID = Nothing
        Me.AplHist1.Size = New System.Drawing.Size(720, 240)
        Me.AplHist1.TabIndex = 0
        '
        'tabClaimsHistory
        '
        Me.tabClaimsHistory.AutoScroll = True
        Me.tabClaimsHistory.Controls.Add(Me.ClaimHist1)
        Me.tabClaimsHistory.Location = New System.Drawing.Point(4, 23)
        Me.tabClaimsHistory.Name = "tabClaimsHistory"
        Me.tabClaimsHistory.Size = New System.Drawing.Size(756, 381)
        Me.tabClaimsHistory.TabIndex = 7
        Me.tabClaimsHistory.Text = "Claims History"
        Me.tabClaimsHistory.Visible = False
        '
        'ClaimHist1
        '
        Me.ClaimHist1.Location = New System.Drawing.Point(0, 0)
        Me.ClaimHist1.Name = "ClaimHist1"
        Me.ClaimHist1.PolicyAccountID = Nothing
        Me.ClaimHist1.Size = New System.Drawing.Size(728, 720)
        Me.ClaimHist1.TabIndex = 0
        '
        'tabAgentInfo
        '
        Me.tabAgentInfo.Controls.Add(Me.AgentInfo1)
        Me.tabAgentInfo.Location = New System.Drawing.Point(4, 23)
        Me.tabAgentInfo.Name = "tabAgentInfo"
        Me.tabAgentInfo.Size = New System.Drawing.Size(756, 381)
        Me.tabAgentInfo.TabIndex = 10
        Me.tabAgentInfo.Text = "Agent Information"
        Me.tabAgentInfo.Visible = False
        '
        'AgentInfo1
        '
        Me.AgentInfo1.Location = New System.Drawing.Point(0, 0)
        Me.AgentInfo1.Name = "AgentInfo1"
        Me.AgentInfo1.Size = New System.Drawing.Size(720, 368)
        Me.AgentInfo1.TabIndex = 0
        '
        'tabSMS
        '
        Me.tabSMS.AutoScroll = True
        Me.tabSMS.Controls.Add(Me.Sms1)
        Me.tabSMS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSMS.Location = New System.Drawing.Point(4, 23)
        Me.tabSMS.Name = "tabSMS"
        Me.tabSMS.Size = New System.Drawing.Size(756, 381)
        Me.tabSMS.TabIndex = 14
        Me.tabSMS.Text = "SMS"
        Me.tabSMS.Visible = False
        '
        'Sms1
        '
        Me.Sms1.AutoScroll = True
        Me.Sms1.Location = New System.Drawing.Point(0, 0)
        Me.Sms1.Name = "Sms1"
        Me.Sms1.PolicyAccountID = Nothing
        Me.Sms1.Size = New System.Drawing.Size(720, 368)
        Me.Sms1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(260, 28)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(188, 20)
        Me.txtStatus.TabIndex = 2
        Me.txtStatus.Text = ""
        '
        'txtPolicy
        '
        Me.txtPolicy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicy.Location = New System.Drawing.Point(108, 28)
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.ReadOnly = True
        Me.txtPolicy.TabIndex = 3
        Me.txtPolicy.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(48, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Policy No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Status"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(464, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Product"
        '
        'txtProduct
        '
        Me.txtProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtProduct.Location = New System.Drawing.Point(512, 28)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.ReadOnly = True
        Me.txtProduct.Size = New System.Drawing.Size(228, 20)
        Me.txtProduct.TabIndex = 7
        Me.txtProduct.Text = ""
        '
        'txtChiName
        '
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Location = New System.Drawing.Point(564, 4)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.Size = New System.Drawing.Size(92, 20)
        Me.txtChiName.TabIndex = 8
        Me.txtChiName.Text = ""
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Location = New System.Drawing.Point(372, 4)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(188, 20)
        Me.txtFirstName.TabIndex = 9
        Me.txtFirstName.Text = ""
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Location = New System.Drawing.Point(192, 4)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(176, 20)
        Me.txtLastName.TabIndex = 10
        Me.txtLastName.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Serving Customer"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(108, 4)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(80, 20)
        Me.txtTitle.TabIndex = 12
        Me.txtTitle.Text = ""
        '
        'frmPolicy
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(764, 461)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.txtProduct)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPolicy)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.tcPolicy)
        Me.Name = "frmPolicy"
        Me.Tag = "Policy Information"
        Me.Text = "frmPolicy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tcPolicy.ResumeLayout(False)
        Me.tabPolicySummary.ResumeLayout(False)
        Me.tabDDACCD.ResumeLayout(False)
        Me.tabCustomerHistory.ResumeLayout(False)
        Me.tabServiceLog.ResumeLayout(False)
        Me.tabCoverageDetails.ResumeLayout(False)
        Me.tabPaymentHistory.ResumeLayout(False)
        Me.tabUnderwriting.ResumeLayout(False)
        Me.tabCouponHistory.ResumeLayout(False)
        Me.tabFinancialInfo.ResumeLayout(False)
        Me.tabAPLHistory.ResumeLayout(False)
        Me.tabClaimsHistory.ResumeLayout(False)
        Me.tabAgentInfo.ResumeLayout(False)
        Me.tabSMS.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private dtPAYH, dtCOUH, dtPriv, dtAPLH As DataTable
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private strPolicy, strCustID As String
    Private WithEvents w As New clsMQWorker
    Private dtPolSum, dtPolAddr, dtCoverage, dtCustHist, dtCCDR, dtDDA, dtUWInf, dtNA As DataTable
    Private blnPOINFO As Boolean = False
    Private blnExit As Boolean
    Private strClientList, strHolderID As String
    Private datEnqFrom, datEnqTo As Date

    ' To be executed in another thread
    Delegate Sub QExecDelegate(ByVal strPolicy As String, ByVal datEnqFrom As Date, ByVal datEnqTo As Date)

    ' UpdateUI delegate executed on main thread
    Delegate Sub UpdateUIDelegate(ByVal strFunc As String)

    'Public Property PolicyNo(ByVal strTitle As String, ByVal strLastName As String, ByVal strFirstName As String, _
    '        ByVal strChiName As String, ByVal strProduct As String, ByVal strStatus As String) As String
    Public Property PolicyAccountID() As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strPolicy = Value
            Me.txtPolicy.Text = Value
            'If Not IsDBNull(strTitle) Then Me.txtTitle.Text = strTitle
            'If Not IsDBNull(strLastName) Then Me.txtLastName.Text = strLastName
            'If Not IsDBNull(strFirstName) Then Me.txtFirstName.Text = strFirstName
            'If Not IsDBNull(strChiName) Then Me.txtChiName.Text = strChiName
            'If Not IsDBNull(strProduct) Then Me.txtProduct.Text = strProduct
            'If Not IsDBNull(strStatus) Then Me.txtStatus.Text = strStatus
        End Set
    End Property

    Public Property CustomerID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            strCustID = Value
        End Set
    End Property

    Public ReadOnly Property NoRecord() As Boolean
        Get
            Return blnExit
        End Get
    End Property

    Private Sub w_Finish(ByVal strFunc As String, ByVal lngErrNo As Long, ByVal strErrMsg As String, ByVal dt As System.Data.DataTable) Handles w.Finish

        ' assgin return dt to corresponding dt object, called by worker thread
        If lngErrNo <> 0 Then
            wndMain.StatusBarPanel1.Text = strErrMsg
        Else
            Select Case strFunc
                Case cPOADDR
                    dtPolAddr = dt
                Case cCOINFO
                    dtCoverage = dt
                Case cDDA
                    dtDDA = dt
                Case cCCDR
                    dtCCDR = dt
                Case cHICL
                    dtCustHist = dt
                Case cUWINFO
                    dtUWInf = dt
                Case cPAYH
                    dtPAYH = dt
                Case cCOUHST
                    dtCOUH = dt
                Case cAPLH
                    dtAPLH = dt
            End Select

            ' update UI for corresponding tab
            Call UpdateUI(strFunc)
        End If

    End Sub

    Private Sub frmPolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Me.tcPolicy.Size = New System.Drawing.Size(764, 408)

        Dim dlgQExec As QExecDelegate = New QExecDelegate(AddressOf w.QueuedExec)

        'Dim tab As TabPage
        'For Each tab In tcPolicy.TabPages
        '    'tabPaymentHistory.ImageIndex = 3
        '    'tabCouponHistory.ImageIndex = 3
        '    If tab.Text = "Policy Summary" Then
        '    Else
        '        tab.ImageIndex = 3
        '    End If
        'Next

        dtPriv = objCS.GetPrivRS(giUPSGrp, "tcPolicy", lngErr, strErr)
        If lngErr <> 0 Or dtPriv.Rows.Count = 0 Then
            MsgBox("Error retrieving authority records")
        End If
        wndMain.StatusBarPanel1.Text = ""

        ' *** Get Policy Summary first ***
        dtPolSum = objCS.GetPolicySummary(strPolicy, lngErr, strErr)
        If lngErr = 0 Then
            blnPOINFO = True
        End If

        If Not dtPolSum Is Nothing Then

            If dtPolSum.Rows.Count > 0 Then

                'dtPolSum.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
                'dtPolSum.DefaultView.RowFilter = "PolicyRelateCode = 'O'"
                'drs = dtPolSum.DefaultView.Item(0)

                ' Get effective date and calculate the date range for enquiry function
                datEnqFrom = dtPolSum.Rows(0).Item("PaidToDate")
                If dtPolSum.Rows(0).Item("BillToDate") > datEnqFrom Then
                    datEnqFrom = dtPolSum.Rows(0).Item("BillToDate")
                End If

#If UAT = 1 Then
                datEnqFrom = #12/31/2005#
#End If

                ' Get Client No. list
                Dim i, j As Integer

                strClientList = "'-'"
                For i = 0 To dtPolSum.Rows.Count - 1
                    With dtPolSum.Rows(i)
                        strClientList &= ",'" & .Item("ClientID") & "'"
                        If .Item("PolicyRelateCode") = "PH" Then
                            strHolderID = .Item("ClientID")
                        End If
                    End With
                Next

                ' run worker thread and free the UI
                dlgQExec.BeginInvoke(strPolicy, datEnqFrom, datEnqTo, AddressOf CallBack, dlgQExec)

                Dim drs As DataRowView
                dtNA = objCS.GetORDUNA(strClientList, lngErr, strErr)
                'dtPolAddr = dt1

                If lngErr = 0 Then
                    dtNA.DefaultView.RowFilter = "ClientID = '" & strHolderID & "'"
                    drs = dtNA.DefaultView.Item(0)
                    strCustID = drs.Item("CustomerID")
                    Me.CustomerID = strCustID
                Else
                    MsgBox(strErr)
                    Exit Sub
                End If
                dtPolAddr = dtNA.Copy
                dtPolAddr.DefaultView.RowFilter = "ClientID = '" & strHolderID & "'"
                dtPolAddr.TableName = "CustomerAddress"
                AddressSelect1.srcDTAddr = dtPolAddr

                'With drs
                '    If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")
                '    If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                '    If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                '    If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")
                '    If Not IsDBNull(.Item("Description")) Then Me.txtProduct.Text = .Item("Description")
                '    If Not IsDBNull(.Item("AccountStatusCode")) Then Me.txtStatus.Text = GetAcStatus(.Item("AccountStatusCode"))
                'End With
                With drs
                    If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")
                    If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                    If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                    If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")
                End With
                With dtPolSum.Rows(0)
                    If Not IsDBNull(.Item("Description")) Then Me.txtProduct.Text = .Item("Description")
                    If Not IsDBNull(.Item("AccountStatusCode")) Then Me.txtStatus.Text = GetAcStatus(.Item("AccountStatusCode"))
                End With
                ' Other black box, process in turn
                PolicySummary1.PolicyAccountID = strPolicy
                PolicySummary1.srcDTPolSum(dtNA.Copy) = dtPolSum
                'PolicySummary1.srcDTPolSum = dtPolSum
                'AxWebBrowser1.Navigate("http://www.inginsurance.hk.asia.intranet/cs/cs.htm")
            Else
                MsgBox("Policy " & strPolicy & " not found, please input again.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly, "Policy Information")
                blnExit = True
            End If

        End If

        'PolicySummary1.srcDTPolSum = dtPolSum
        'AgentInfo1.srcDTPolSum = dtPolSum.Copy
        'AddressSelect1.srcDTAddr = dtPolAddr
        'Coverage1.srcDTCov = dtCoverage
        'ClaimHist1.PolicyAccountID = "U9608120"
        'Sms1.PolicyAccountID = "01850160"
        'CustHist1.srcDTCustHist = dtCustHist
        'Ddaccdr1.srcDTDDACCDR(dtCCDR) = dtDDA
        'UwInfo1.srcDTUWInf = dtUWInf
        'FinancialInfo1.PolicyAccountID(dtPolSum) = strPolicy

    End Sub

    Private Sub CallBack(ByVal ar As IAsyncResult)

        ' Retrieve the delegate.
        Dim dlgt As QExecDelegate = CType(ar.AsyncState, QExecDelegate)

        ' Call EndInvoke to retrieve the results.
        dlgt.EndInvoke(ar)

    End Sub

    Public Sub UpdateUI(ByVal strFunc As String)

        ' If call from worker thread, switch to UI thread
        If Me.InvokeRequired Then
            Dim args() As Object = {strFunc}
            Dim dlgUI As UpdateUIDelegate = New UpdateUIDelegate(AddressOf UpdateUI)
            Me.Invoke(dlgUI, args)
        Else
            ' Wait till POINFO finish
            While blnPOINFO = False
                Application.DoEvents()
            End While

            ' update UI
            Select Case strFunc
                Case cPOADDR
                    'AddressSelect1.srcDTAddr = dtPolAddr
                Case cCOINFO
                    Coverage1.srcDTCov(dtPolSum.Rows(0).Item("CurModeFactor")) = dtCoverage
                    Me.tabCoverageDetails.ImageIndex = -1
                Case cDDA
                    Ddaccdr1.srcDTDDACCDR(dtCCDR) = dtDDA
                    Me.tabDDACCD.ImageIndex = -1
                Case cCCDR
                    ' wait for dda
                Case cHICL
                    CustHist1.srcDTCustHist(dtNA.Copy) = dtCustHist
                    Me.tabCustomerHistory.ImageIndex = -1
                Case cUWINFO
                    UwInfo1.srcDTUWInf = dtUWInf
                    Me.tabUnderwriting.ImageIndex = -1
                Case cPAYH
                    Me.Payh1.PolicyAccountID(datEnqFrom, dtPAYH, dtPolSum.Copy) = strPolicy
                    'Me.Payh1.DateFrom = datEnqFrom
                    'Me.Payh1.srcDT(dtPolSum.Copy) = dtPAYH
                    tabPaymentHistory.ImageIndex = -1
                Case cCOUHST
                    Me.Couh1.srcDT = dtCOUH
                    tabCouponHistory.ImageIndex = -1
                Case cAPLH
                    Me.AplHist1.PolicyAccountID = strPolicy
                    Me.AplHist1.srcDT = dtAPLH
                    tabAPLHistory.ImageIndex = -1
            End Select
            'wndMain.ProgressBar1.Value += 30
            'If wndMain.ProgressBar1.Value = 90 Then
            '    wndMain.ProgressBar1.Value = 100
            '    Timer1.Enabled = True
            'End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcPolicy.SelectedIndexChanged

        If Not dtPriv Is Nothing Then
            Dim dr() As DataRow = dtPriv.Select("cswup_name = '" & tcPolicy.SelectedTab.Name & "'")

            If dr.Length > 0 Then
                If InStr(dr(0).Item("cswup_access"), "R") = 0 Then
                    tcPolicy.SelectedTab.Visible = False
                    MsgBox("You don't have right to view this page")
                    tcPolicy.SelectedTab = Me.tabPolicySummary
                End If
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        wndMain.ProgressBar1.Value = 0
        Timer1.Enabled = False
    End Sub

    Private Function GetPolicyAlert(ByVal strID As String, ByVal strType As String) As String

        Dim strSQL, strMsg As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select cswcpa_message from csw_customer_policy_alert " & _
            " Where cswcpa_type = '" & Trim(strType) & "' " & _
            " And cswcpa_id = '" & Trim(strID) & "' " & _
            " And cswcpa_valid = 'T'"

        sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        ' Count the number of records return from CIW, if okay we can then call MQ
        Try
            strMsg = sqlcmd.ExecuteScalar()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        Return strMsg

    End Function

    Private Sub tabClaimsHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabClaimsHistory.HandleCreated
        ClaimHist1.PolicyAccountID = "U9608120"
        Me.tabClaimsHistory.ImageIndex = -1
    End Sub

    Private Sub tabAgentInfo_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabAgentInfo.HandleCreated
        AgentInfo1.srcDTPolSum = dtPolSum.Copy
        Me.tabAgentInfo.ImageIndex = -1
    End Sub

    Private Sub tabSMS_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabSMS.HandleCreated
        Sms1.PolicyAccountID = "01850160"
        Me.tabSMS.ImageIndex = -1
    End Sub

    Private Sub tabServiceLog_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabServiceLog.HandleCreated
        UclServiceLog1.PolicyAccountID = strPolicy
        UclServiceLog1.CustomerID = strCustID
        Me.tabSMS.ImageIndex = -1
    End Sub

    Private Sub tabFinancialInfo_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabFinancialInfo.HandleCreated
        FinancialInfo1.PolicyAccountID(dtPolSum) = strPolicy
        Me.tabFinancialInfo.ImageIndex = -1
    End Sub

    Private Sub frmPolicy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        ' Check if any policy alert set
        Dim strMessage As String = GetPolicyAlert(strPolicy, "P")
        If strMessage <> "" Then
            txtPolicy.BackColor = System.Drawing.Color.Orange
            txtStatus.BackColor = System.Drawing.Color.Orange
            txtProduct.BackColor = System.Drawing.Color.Orange
            wndMain.StatusBarPanel1.Text = "Policy Alert: " & strMessage
            MsgBox(strMessage, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Policy Alert")
        End If

        strMessage = GetPolicyAlert(strCustID, "C")
        dtPolSum.DefaultView.RowFilter = ""
        If strMessage <> "" Then
            txtTitle.BackColor = System.Drawing.Color.Orange
            txtLastName.BackColor = System.Drawing.Color.Orange
            txtFirstName.BackColor = System.Drawing.Color.Orange
            txtChiName.BackColor = System.Drawing.Color.Orange
            If wndMain.StatusBarPanel1.Text <> "" Then
                wndMain.StatusBarPanel1.Text &= " / "
            End If
            wndMain.StatusBarPanel1.Text = "Customer Alert: " & strMessage
            MsgBox(strMessage, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Customer Alert")
        End If

    End Sub

End Class
