'**************************
'Date : Jun 11, 2015
'Author : Kay Tsang KT20150611
'Purpose disable frmInbox pending check box by default
'**************************
'**************************
'Date : Nov 04, 2015
'Author : Sampson Siu SS20151104
'Purpose : Check policy type (LifeAsia/Capsil) of searched policy, and find policy info by policy type
'**************************
'**************************
'Date : Dec 01, 2015
'Author : Harry Tan HT20151201
'Purpose : Single Customer View Project
'**************************
'**************************
'Date : Dec 15, 2023
'Author : Oliver Ou
'Purpose : Switch Over Code from Assurance to Bermuda 
'**************************
' Amend By:     Chrysan Cheng
' Date:         12 Nov 2024
' Changes:      CRS performer slowness - Policy Summary
'********************************************************************
' Description : HNW Expansion - Integrated Customer Search
' Date		  : 12 Jan 2025
' Author	  : Chrysan Cheng
'******************************************************************

Imports System.Configuration
Imports System.IO

Public Class frmCS2005_Asur
    Inherits System.Windows.Forms.Form
    Private objNBA As New NewBusinessAdmin.NBLifeAdmin
    'Private WithEvents objCICServer As clsCIC
    Friend WithEvents mnuiFWDFreePolicy As System.Windows.Forms.MenuItem
    Friend WithEvents PanelPolicyQuickSearch As System.Windows.Forms.Panel
    Friend WithEvents rbEB As System.Windows.Forms.RadioButton
    Friend WithEvents rbGI As System.Windows.Forms.RadioButton
    Friend WithEvents rbLife As System.Windows.Forms.RadioButton
    Friend WithEvents rbMcuLife As System.Windows.Forms.RadioButton
    Friend WithEvents mnuMacauServiceLog As System.Windows.Forms.MenuItem

    Private strPolicyNo As String
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallProduct As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallQuestion As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallFundRisk As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallList As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCrmOptInOut As System.Windows.Forms.MenuItem
    Friend WithEvents mum7ElevenPayout As System.Windows.Forms.MenuItem
    Friend WithEvents mnuChatbotCIC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBackdoorReg As System.Windows.Forms.MenuItem
    Friend WithEvents mnuIcontekCeAssistance As System.Windows.Forms.MenuItem
    Friend WithEvents mnuIcontekChatbotAdmin As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHKNonCustLog As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCrsPreference As System.Windows.Forms.MenuItem
    Friend WithEvents mnuServicelogRetrieve As System.Windows.Forms.MenuItem

    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Friend WithEvents mnuNBMServicelogRetrieve As System.Windows.Forms.MenuItem

    Friend WithEvents mnuSMSTempMgt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCoBrowsing As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgentAssist As System.Windows.Forms.MenuItem
    Friend WithEvents rbAssurance As System.Windows.Forms.RadioButton
    'Delegate Sub UpdateUIDelegate(ByVal strCustID As String, ByVal strName As String, ByVal strClientID As String)
    Dim bMO As Boolean = False
    Friend WithEvents mnuUtilityMac As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSearchPolicyMac As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCampMgtMcu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCSRMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAlertMcu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPolProjMcu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPrintReportMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReprintCardMcu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallProductMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallQuestionMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPostSalesCallFundRiskMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuServicelogRetrieveMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSMSTempMgtMcu As System.Windows.Forms.MenuItem

    Private wGIServiceLog As frmGIServiceLog
    Friend WithEvents mnuGenPostSalesCallListMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGeneralQuery As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPolicyMappingMcu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgentAssistMacau As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEServiceOptInOut As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLCP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFindUserName As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSearchCustAssur As MenuItem
    Friend WithEvents mnuSearchPolicyAssur As MenuItem
    Friend WithEvents tbHiddenButton As ToolBarButton
    Friend WithEvents rbBermuda As RadioButton

    'oliver 2024-7-25 added for Com 6
    Friend WithEvents rbHNW As RadioButton
    Friend WithEvents mnuImportMisc As MenuItem
    Friend WithEvents mnuCSPolicyList As MenuItem
    Friend WithEvents mnuCampaignManagement As MenuItem
    Friend WithEvents mnuCSRMaintenance As MenuItem
    Friend WithEvents mnuCustomerAlertMaintenance As MenuItem
    Friend WithEvents mnuPolicyValueProjection As MenuItem
    Friend WithEvents mnuPrintFaxReports As MenuItem
    Friend WithEvents mnuReprintCustomerCardPIN As MenuItem
    Friend WithEvents mnuPostSalesCallListPri As MenuItem
    Friend WithEvents mnuTransactionLog As MenuItem
    Friend WithEvents mnuExchangeRate As MenuItem
    Friend WithEvents mnuVPOQuotation As MenuItem
    Friend WithEvents mnuServiceFee As MenuItem
    Friend WithEvents mnuDMPSServiceFee As MenuItem
    Friend WithEvents mnuLCP_LAC As MenuItem
    Friend WithEvents mnuLCP_LAH As MenuItem
    Friend WithEvents mnuHelp As MenuItem
    Friend WithEvents mnuSearchCSKnowledgeBase As MenuItem
    Friend WithEvents mnuCheckUpdateofCSKB As MenuItem
    Friend WithEvents mnuTransactionLogBer As MenuItem
    Friend WithEvents mnuTransactionLogLac As MenuItem
    Friend WithEvents mnuTransactionLogLah As MenuItem
    Friend WithEvents mnuCheckSMS As MenuItem
    Friend WithEvents mnuTransactionLogPri As MenuItem

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUtility As System.Windows.Forms.MenuItem
    Friend WithEvents mnuWindow As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBarPanel4 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel5 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents tbSearchCust As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbSearchPolicy As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents LabelPolicyNo As System.Windows.Forms.Label
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPRTS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuIRTS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUVAL As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBANK As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelBatch As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPolicyMapping As System.Windows.Forms.MenuItem
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCS2005_Asur))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuImportMisc = New System.Windows.Forms.MenuItem()
        Me.mnuDelBatch = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.mnuCSPolicyList = New System.Windows.Forms.MenuItem()
        Me.mnuCrsPreference = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.mnuUtility = New System.Windows.Forms.MenuItem()
        Me.mnuSearchPolicyAssur = New System.Windows.Forms.MenuItem()
        Me.mnuSearchCustAssur = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuCampaignManagement = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.mnuCSRMaintenance = New System.Windows.Forms.MenuItem()
        Me.mnuCustomerAlertMaintenance = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.mnuPolicyValueProjection = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuPrintFaxReports = New System.Windows.Forms.MenuItem()
        Me.mnuReprintCustomerCardPIN = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallProduct = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallQuestion = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallFundRisk = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallList = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallListPri = New System.Windows.Forms.MenuItem()
        Me.mnuServicelogRetrieve = New System.Windows.Forms.MenuItem()
        Me.mnuNBMServicelogRetrieve = New System.Windows.Forms.MenuItem()
        Me.mnuSMSTempMgt = New System.Windows.Forms.MenuItem()
        Me.mnuUtilityMac = New System.Windows.Forms.MenuItem()
        Me.mnuSearchPolicyMac = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.mnuCampMgtMcu = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.mnuCSRMcu = New System.Windows.Forms.MenuItem()
        Me.mnuAlertMcu = New System.Windows.Forms.MenuItem()
        Me.MenuItem26 = New System.Windows.Forms.MenuItem()
        Me.mnuPolProjMcu = New System.Windows.Forms.MenuItem()
        Me.MenuItem28 = New System.Windows.Forms.MenuItem()
        Me.mnuPrintReportMcu = New System.Windows.Forms.MenuItem()
        Me.mnuReprintCardMcu = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallProductMcu = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallQuestionMcu = New System.Windows.Forms.MenuItem()
        Me.mnuPostSalesCallFundRiskMcu = New System.Windows.Forms.MenuItem()
        Me.mnuGenPostSalesCallListMcu = New System.Windows.Forms.MenuItem()
        Me.mnuServicelogRetrieveMcu = New System.Windows.Forms.MenuItem()
        Me.mnuSMSTempMgtMcu = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuPRTS = New System.Windows.Forms.MenuItem()
        Me.mnuIRTS = New System.Windows.Forms.MenuItem()
        Me.mnuUVAL = New System.Windows.Forms.MenuItem()
        Me.mnuBANK = New System.Windows.Forms.MenuItem()
        Me.mnuPolicyMapping = New System.Windows.Forms.MenuItem()
        Me.mnuPolicyMappingMcu = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionLog = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionLogBer = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionLogLac = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionLogLah = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionLogPri = New System.Windows.Forms.MenuItem()
        Me.mnuExchangeRate = New System.Windows.Forms.MenuItem()
        Me.mnuVPOQuotation = New System.Windows.Forms.MenuItem()
        Me.mnuServiceFee = New System.Windows.Forms.MenuItem()
        Me.mnuDMPSServiceFee = New System.Windows.Forms.MenuItem()
        Me.mnuiFWDFreePolicy = New System.Windows.Forms.MenuItem()
        Me.mnuMacauServiceLog = New System.Windows.Forms.MenuItem()
        Me.mnuHKNonCustLog = New System.Windows.Forms.MenuItem()
        Me.mnuCrmOptInOut = New System.Windows.Forms.MenuItem()
        Me.mum7ElevenPayout = New System.Windows.Forms.MenuItem()
        Me.mnuChatbotCIC = New System.Windows.Forms.MenuItem()
        Me.mnuBackdoorReg = New System.Windows.Forms.MenuItem()
        Me.mnuIcontekCeAssistance = New System.Windows.Forms.MenuItem()
        Me.mnuIcontekChatbotAdmin = New System.Windows.Forms.MenuItem()
        Me.mnuCoBrowsing = New System.Windows.Forms.MenuItem()
        Me.mnuAgentAssist = New System.Windows.Forms.MenuItem()
        Me.mnuAgentAssistMacau = New System.Windows.Forms.MenuItem()
        Me.mnuEServiceOptInOut = New System.Windows.Forms.MenuItem()
        Me.mnuLCP = New System.Windows.Forms.MenuItem()
        Me.mnuLCP_LAC = New System.Windows.Forms.MenuItem()
        Me.mnuLCP_LAH = New System.Windows.Forms.MenuItem()
        Me.mnuFindUserName = New System.Windows.Forms.MenuItem()
        Me.mnuCheckSMS = New System.Windows.Forms.MenuItem()
        Me.mnuWindow = New System.Windows.Forms.MenuItem()
        Me.mnuHelp = New System.Windows.Forms.MenuItem()
        Me.mnuSearchCSKnowledgeBase = New System.Windows.Forms.MenuItem()
        Me.mnuCheckUpdateofCSKB = New System.Windows.Forms.MenuItem()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbSearchCust = New System.Windows.Forms.ToolBarButton()
        Me.tbSearchPolicy = New System.Windows.Forms.ToolBarButton()
        Me.tbPrint = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.tbRefresh = New System.Windows.Forms.ToolBarButton()
        Me.tbHiddenButton = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel4 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel5 = New System.Windows.Forms.StatusBarPanel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtPolicy = New System.Windows.Forms.TextBox()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.LabelPolicyNo = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.PanelPolicyQuickSearch = New System.Windows.Forms.Panel()
        Me.rbLife = New System.Windows.Forms.RadioButton()
        Me.rbBermuda = New System.Windows.Forms.RadioButton()
        Me.rbHNW = New System.Windows.Forms.RadioButton()
        Me.rbGI = New System.Windows.Forms.RadioButton()
        Me.rbEB = New System.Windows.Forms.RadioButton()
        Me.rbAssurance = New System.Windows.Forms.RadioButton()
        Me.rbMcuLife = New System.Windows.Forms.RadioButton()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPolicyQuickSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuUtility, Me.mnuUtilityMac, Me.MenuItem1, Me.mnuWindow, Me.mnuHelp})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuImportMisc, Me.mnuDelBatch, Me.MenuItem8, Me.mnuCSPolicyList, Me.mnuCrsPreference, Me.MenuItem5, Me.mnuExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuImportMisc
        '
        Me.mnuImportMisc.Index = 0
        Me.mnuImportMisc.Text = "Import Misc."
        '
        'mnuDelBatch
        '
        Me.mnuDelBatch.Index = 1
        Me.mnuDelBatch.Text = "Delete Batch Import"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 2
        Me.MenuItem8.Text = "-"
        '
        'mnuCSPolicyList
        '
        Me.mnuCSPolicyList.Index = 3
        Me.mnuCSPolicyList.Text = "CS Policy List"
        '
        'mnuCrsPreference
        '
        Me.mnuCrsPreference.Enabled = False
        Me.mnuCrsPreference.Index = 4
        Me.mnuCrsPreference.Text = "CRS Preference"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 5
        Me.MenuItem5.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 6
        Me.mnuExit.Text = "Exit"
        '
        'mnuUtility
        '
        Me.mnuUtility.Index = 1
        Me.mnuUtility.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSearchPolicyAssur, Me.mnuSearchCustAssur, Me.MenuItem3, Me.mnuCampaignManagement, Me.MenuItem4, Me.mnuCSRMaintenance, Me.mnuCustomerAlertMaintenance, Me.MenuItem7, Me.mnuPolicyValueProjection, Me.MenuItem6, Me.mnuPrintFaxReports, Me.mnuReprintCustomerCardPIN, Me.MenuItem17, Me.mnuPostSalesCallProduct, Me.mnuPostSalesCallQuestion, Me.mnuPostSalesCallFundRisk, Me.mnuPostSalesCallList, Me.mnuPostSalesCallListPri, Me.mnuServicelogRetrieve, Me.mnuNBMServicelogRetrieve, Me.mnuSMSTempMgt})
        Me.mnuUtility.Text = "&Utility"
        '
        'mnuSearchPolicyAssur
        '
        Me.mnuSearchPolicyAssur.Index = 0
        Me.mnuSearchPolicyAssur.Text = "Search Policy"
        '
        'mnuSearchCustAssur
        '
        Me.mnuSearchCustAssur.Index = 1
        Me.mnuSearchCustAssur.Text = "Search Customer"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuCampaignManagement
        '
        Me.mnuCampaignManagement.Index = 3
        Me.mnuCampaignManagement.Text = "Campaign Management"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 4
        Me.MenuItem4.Text = "-"
        '
        'mnuCSRMaintenance
        '
        Me.mnuCSRMaintenance.Index = 5
        Me.mnuCSRMaintenance.Text = "CSR Maintenance"
        '
        'mnuCustomerAlertMaintenance
        '
        Me.mnuCustomerAlertMaintenance.Index = 6
        Me.mnuCustomerAlertMaintenance.Text = "Customer Alert Maintenance"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 7
        Me.MenuItem7.Text = "-"
        '
        'mnuPolicyValueProjection
        '
        Me.mnuPolicyValueProjection.Index = 8
        Me.mnuPolicyValueProjection.Text = "Policy Value Projection"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 9
        Me.MenuItem6.Text = "-"
        '
        'mnuPrintFaxReports
        '
        Me.mnuPrintFaxReports.Index = 10
        Me.mnuPrintFaxReports.Text = "Print/Fax Reports"
        '
        'mnuReprintCustomerCardPIN
        '
        Me.mnuReprintCustomerCardPIN.Enabled = False
        Me.mnuReprintCustomerCardPIN.Index = 11
        Me.mnuReprintCustomerCardPIN.Text = "Reprint Customer Card / PIN"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 12
        Me.MenuItem17.Text = "-"
        '
        'mnuPostSalesCallProduct
        '
        Me.mnuPostSalesCallProduct.Index = 13
        Me.mnuPostSalesCallProduct.Text = "Post Sales Call Product Setting"
        '
        'mnuPostSalesCallQuestion
        '
        Me.mnuPostSalesCallQuestion.Index = 14
        Me.mnuPostSalesCallQuestion.Text = "Post Sales Call Question Maintenance"
        '
        'mnuPostSalesCallFundRisk
        '
        Me.mnuPostSalesCallFundRisk.Index = 15
        Me.mnuPostSalesCallFundRisk.Text = "Fund Risk Level Maintenance"
        '
        'mnuPostSalesCallList
        '
        Me.mnuPostSalesCallList.Index = 16
        Me.mnuPostSalesCallList.Text = "Generate Post-Sales Call List(HK)"
        '
        'mnuPostSalesCallListPri
        '
        Me.mnuPostSalesCallListPri.Index = 17
        Me.mnuPostSalesCallListPri.Text = "Generate Post-Sales Call List(Private)"
        '
        'mnuServicelogRetrieve
        '
        Me.mnuServicelogRetrieve.Index = 18
        Me.mnuServicelogRetrieve.Text = "Service Log Retrieve"
        '
        'mnuNBMServicelogRetrieve
        '
        Me.mnuNBMServicelogRetrieve.Index = 19
        Me.mnuNBMServicelogRetrieve.Text = "Service Log Retrieve(NBM)"
        '
        'mnuSMSTempMgt
        '
        Me.mnuSMSTempMgt.Enabled = False
        Me.mnuSMSTempMgt.Index = 20
        Me.mnuSMSTempMgt.Text = "SMS Template Management"
        '
        'mnuUtilityMac
        '
        Me.mnuUtilityMac.Index = 2
        Me.mnuUtilityMac.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSearchPolicyMac, Me.MenuItem21, Me.mnuCampMgtMcu, Me.MenuItem23, Me.mnuCSRMcu, Me.mnuAlertMcu, Me.MenuItem26, Me.mnuPolProjMcu, Me.MenuItem28, Me.mnuPrintReportMcu, Me.mnuReprintCardMcu, Me.MenuItem31, Me.mnuPostSalesCallProductMcu, Me.mnuPostSalesCallQuestionMcu, Me.mnuPostSalesCallFundRiskMcu, Me.mnuGenPostSalesCallListMcu, Me.mnuServicelogRetrieveMcu, Me.mnuSMSTempMgtMcu})
        Me.mnuUtilityMac.Text = "&Utility(Macau)"
        '
        'mnuSearchPolicyMac
        '
        Me.mnuSearchPolicyMac.Index = 0
        Me.mnuSearchPolicyMac.Text = "Search Policy(Macau)"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 1
        Me.MenuItem21.Text = "-"
        '
        'mnuCampMgtMcu
        '
        Me.mnuCampMgtMcu.Index = 2
        Me.mnuCampMgtMcu.Text = "Campaign Management(Macau)"
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 3
        Me.MenuItem23.Text = "-"
        '
        'mnuCSRMcu
        '
        Me.mnuCSRMcu.Index = 4
        Me.mnuCSRMcu.Text = "CSR Maintenance(Macau)"
        '
        'mnuAlertMcu
        '
        Me.mnuAlertMcu.Index = 5
        Me.mnuAlertMcu.Text = "Customer Alert Maintenance(Macau)"
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 6
        Me.MenuItem26.Text = "-"
        '
        'mnuPolProjMcu
        '
        Me.mnuPolProjMcu.Index = 7
        Me.mnuPolProjMcu.Text = "Policy Value Projection(Macau)"
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 8
        Me.MenuItem28.Text = "-"
        '
        'mnuPrintReportMcu
        '
        Me.mnuPrintReportMcu.Index = 9
        Me.mnuPrintReportMcu.Text = "Print/Fax Report(Macau)"
        '
        'mnuReprintCardMcu
        '
        Me.mnuReprintCardMcu.Enabled = False
        Me.mnuReprintCardMcu.Index = 10
        Me.mnuReprintCardMcu.Text = "Reprint Customer Card / PIN"
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = 11
        Me.MenuItem31.Text = "-"
        '
        'mnuPostSalesCallProductMcu
        '
        Me.mnuPostSalesCallProductMcu.Index = 12
        Me.mnuPostSalesCallProductMcu.Text = "Post Sales Call Product Setting"
        '
        'mnuPostSalesCallQuestionMcu
        '
        Me.mnuPostSalesCallQuestionMcu.Index = 13
        Me.mnuPostSalesCallQuestionMcu.Text = "Post Sales Call Question Maintenance"
        '
        'mnuPostSalesCallFundRiskMcu
        '
        Me.mnuPostSalesCallFundRiskMcu.Index = 14
        Me.mnuPostSalesCallFundRiskMcu.Text = "Fund Risk Level Maintenance"
        '
        'mnuGenPostSalesCallListMcu
        '
        Me.mnuGenPostSalesCallListMcu.Index = 15
        Me.mnuGenPostSalesCallListMcu.Text = "Generate Post-Sales Call List(Macau)"
        '
        'mnuServicelogRetrieveMcu
        '
        Me.mnuServicelogRetrieveMcu.Index = 16
        Me.mnuServicelogRetrieveMcu.Text = "Service Log Retrieve(Macau)"
        '
        'mnuSMSTempMgtMcu
        '
        Me.mnuSMSTempMgtMcu.Enabled = False
        Me.mnuSMSTempMgtMcu.Index = 17
        Me.mnuSMSTempMgtMcu.Text = "SMS Template Management"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 3
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPRTS, Me.mnuIRTS, Me.mnuUVAL, Me.mnuBANK, Me.mnuPolicyMapping, Me.mnuPolicyMappingMcu, Me.mnuTransactionLog, Me.mnuExchangeRate, Me.mnuVPOQuotation, Me.mnuServiceFee, Me.mnuDMPSServiceFee, Me.mnuiFWDFreePolicy, Me.mnuMacauServiceLog, Me.mnuHKNonCustLog, Me.mnuCrmOptInOut, Me.mum7ElevenPayout, Me.mnuChatbotCIC, Me.mnuBackdoorReg, Me.mnuIcontekCeAssistance, Me.mnuIcontekChatbotAdmin, Me.mnuCoBrowsing, Me.mnuAgentAssist, Me.mnuAgentAssistMacau, Me.mnuEServiceOptInOut, Me.mnuLCP, Me.mnuLCP_LAC, Me.mnuLCP_LAH, Me.mnuFindUserName, Me.mnuCheckSMS})
        Me.MenuItem1.Text = "Enquiry"
        '
        'mnuPRTS
        '
        Me.mnuPRTS.Index = 0
        Me.mnuPRTS.Text = "Premium Rates"
        '
        'mnuIRTS
        '
        Me.mnuIRTS.Index = 1
        Me.mnuIRTS.Text = "Interest Rates"
        '
        'mnuUVAL
        '
        Me.mnuUVAL.Index = 2
        Me.mnuUVAL.Text = "Unit Values"
        '
        'mnuBANK
        '
        Me.mnuBANK.Index = 3
        Me.mnuBANK.Text = "Bank / Branch"
        '
        'mnuPolicyMapping
        '
        Me.mnuPolicyMapping.Index = 4
        Me.mnuPolicyMapping.Text = "Policy Mapping"
        '
        'mnuPolicyMappingMcu
        '
        Me.mnuPolicyMappingMcu.Index = 5
        Me.mnuPolicyMappingMcu.Text = "Policy Mapping(Macau)"
        '
        'mnuTransactionLog
        '
        Me.mnuTransactionLog.Index = 6
        Me.mnuTransactionLog.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuTransactionLogBer, Me.mnuTransactionLogLac, Me.mnuTransactionLogLah, Me.mnuTransactionLogPri})
        Me.mnuTransactionLog.Text = "Transaction Log"
        '
        'mnuTransactionLogBer
        '
        Me.mnuTransactionLogBer.Index = 0
        Me.mnuTransactionLogBer.Text = "Bermuda"
        '
        'mnuTransactionLogLac
        '
        Me.mnuTransactionLogLac.Index = 1
        Me.mnuTransactionLogLac.Text = "LAC"
        '
        'mnuTransactionLogLah
        '
        Me.mnuTransactionLogLah.Index = 2
        Me.mnuTransactionLogLah.Text = "LAH"
        '
        'mnuTransactionLogPri
        '
        Me.mnuTransactionLogPri.Index = 3
        Me.mnuTransactionLogPri.Text = "Private"
        '
        'mnuExchangeRate
        '
        Me.mnuExchangeRate.Index = 7
        Me.mnuExchangeRate.Text = "Exchange Rate"
        '
        'mnuVPOQuotation
        '
        Me.mnuVPOQuotation.Index = 8
        Me.mnuVPOQuotation.Text = "VPO Quotation"
        '
        'mnuServiceFee
        '
        Me.mnuServiceFee.Index = 9
        Me.mnuServiceFee.Text = "Service Fee"
        '
        'mnuDMPSServiceFee
        '
        Me.mnuDMPSServiceFee.Index = 10
        Me.mnuDMPSServiceFee.Text = "DPMS Service Fee"
        '
        'mnuiFWDFreePolicy
        '
        Me.mnuiFWDFreePolicy.Index = 11
        Me.mnuiFWDFreePolicy.Text = "iFWD Free Policy"
        '
        'mnuMacauServiceLog
        '
        Me.mnuMacauServiceLog.Index = 12
        Me.mnuMacauServiceLog.Text = "Macau Service Log"
        '
        'mnuHKNonCustLog
        '
        Me.mnuHKNonCustLog.Enabled = False
        Me.mnuHKNonCustLog.Index = 13
        Me.mnuHKNonCustLog.Text = "HK Service Log (Non-Cust)"
        '
        'mnuCrmOptInOut
        '
        Me.mnuCrmOptInOut.Index = 14
        Me.mnuCrmOptInOut.Text = "CRM opt in/out"
        '
        'mum7ElevenPayout
        '
        Me.mum7ElevenPayout.Index = 15
        Me.mum7ElevenPayout.Text = "7-Eleven Payout"
        '
        'mnuChatbotCIC
        '
        Me.mnuChatbotCIC.Index = 16
        Me.mnuChatbotCIC.Text = "Chatbot CIC"
        '
        'mnuBackdoorReg
        '
        Me.mnuBackdoorReg.Enabled = False
        Me.mnuBackdoorReg.Index = 17
        Me.mnuBackdoorReg.Text = "Backdoor Registration"
        '
        'mnuIcontekCeAssistance
        '
        Me.mnuIcontekCeAssistance.Enabled = False
        Me.mnuIcontekCeAssistance.Index = 18
        Me.mnuIcontekCeAssistance.Text = "CE Assistance"
        '
        'mnuIcontekChatbotAdmin
        '
        Me.mnuIcontekChatbotAdmin.Enabled = False
        Me.mnuIcontekChatbotAdmin.Index = 19
        Me.mnuIcontekChatbotAdmin.Text = "Icontek Chatbot Admin"
        '
        'mnuCoBrowsing
        '
        Me.mnuCoBrowsing.Enabled = False
        Me.mnuCoBrowsing.Index = 20
        Me.mnuCoBrowsing.Text = "CoBrowsing"
        '
        'mnuAgentAssist
        '
        Me.mnuAgentAssist.Index = 21
        Me.mnuAgentAssist.Text = "Agent Assist"
        '
        'mnuAgentAssistMacau
        '
        Me.mnuAgentAssistMacau.Index = 22
        Me.mnuAgentAssistMacau.Text = "Agent Assist Macau"
        '
        'mnuEServiceOptInOut
        '
        Me.mnuEServiceOptInOut.Index = 23
        Me.mnuEServiceOptInOut.Text = "eService Opt In/Out"
        '
        'mnuLCP
        '
        Me.mnuLCP.Index = 24
        Me.mnuLCP.Text = "LCP"
        '
        'mnuLCP_LAC
        '
        Me.mnuLCP_LAC.Index = 25
        Me.mnuLCP_LAC.Text = "LCP (LAC)"
        '
        'mnuLCP_LAH
        '
        Me.mnuLCP_LAH.Index = 26
        Me.mnuLCP_LAH.Text = "LCP (LAH)"
        '
        'mnuFindUserName
        '
        Me.mnuFindUserName.Index = 27
        Me.mnuFindUserName.Text = "Find User Name Tool"
        '
        'mnuCheckSMS
        '
        Me.mnuCheckSMS.Index = 28
        Me.mnuCheckSMS.Text = "Check SMS"
        '
        'mnuWindow
        '
        Me.mnuWindow.Index = 4
        Me.mnuWindow.MdiList = True
        Me.mnuWindow.Text = "&Window"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 5
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSearchCSKnowledgeBase, Me.mnuCheckUpdateofCSKB})
        Me.mnuHelp.Text = "Help"
        '
        'mnuSearchCSKnowledgeBase
        '
        Me.mnuSearchCSKnowledgeBase.Index = 0
        Me.mnuSearchCSKnowledgeBase.Text = "Search CS Knowledge Base"
        '
        'mnuCheckUpdateofCSKB
        '
        Me.mnuCheckUpdateofCSKB.Index = 1
        Me.mnuCheckUpdateofCSKB.Text = "Check Update of CS KB"
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbSearchCust, Me.tbSearchPolicy, Me.tbPrint, Me.ToolBarButton1, Me.tbRefresh, Me.tbHiddenButton})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(40, 40)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Margin = New System.Windows.Forms.Padding(0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(1063, 28)
        Me.ToolBar1.TabIndex = 1
        '
        'tbSearchCust
        '
        Me.tbSearchCust.ImageIndex = 5
        Me.tbSearchCust.Name = "tbSearchCust"
        Me.tbSearchCust.Tag = "SEARCHC"
        Me.tbSearchCust.ToolTipText = "Search Customer"
        '
        'tbSearchPolicy
        '
        Me.tbSearchPolicy.ImageIndex = 4
        Me.tbSearchPolicy.Name = "tbSearchPolicy"
        Me.tbSearchPolicy.Tag = "SEARCHP"
        Me.tbSearchPolicy.ToolTipText = "Search Policy"
        '
        'tbPrint
        '
        Me.tbPrint.ImageIndex = 6
        Me.tbPrint.Name = "tbPrint"
        Me.tbPrint.Tag = "TB_PRINT"
        Me.tbPrint.ToolTipText = "Print Report"
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tbRefresh
        '
        Me.tbRefresh.ImageIndex = 8
        Me.tbRefresh.Name = "tbRefresh"
        Me.tbRefresh.Tag = "TB_REFRESH"
        Me.tbRefresh.ToolTipText = "Update Inbox"
        '
        'tbHiddenButton
        '
        Me.tbHiddenButton.ImageIndex = 13
        Me.tbHiddenButton.Name = "tbHiddenButton"
        Me.tbHiddenButton.Tag = "HiddenButton"
        Me.tbHiddenButton.ToolTipText = "HiddenButton"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "pc_64x64.ico")
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 628)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2, Me.StatusBarPanel3, Me.StatusBarPanel4, Me.StatusBarPanel5})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1063, 22)
        Me.StatusBar1.TabIndex = 3
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.ToolTipText = "Hello"
        Me.StatusBarPanel1.Width = 748
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Width = 90
        '
        'StatusBarPanel4
        '
        Me.StatusBarPanel4.Name = "StatusBarPanel4"
        Me.StatusBarPanel4.Width = 50
        '
        'StatusBarPanel5
        '
        Me.StatusBarPanel5.Name = "StatusBarPanel5"
        Me.StatusBarPanel5.Width = 50
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(160, 24)
        Me.ProgressBar1.TabIndex = 5
        Me.ProgressBar1.Visible = False
        '
        'txtPolicy
        '
        Me.txtPolicy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPolicy.BackColor = System.Drawing.SystemColors.Info
        Me.txtPolicy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPolicy.Location = New System.Drawing.Point(253, 4)
        Me.txtPolicy.MaxLength = 16
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.Size = New System.Drawing.Size(195, 26)
        Me.txtPolicy.TabIndex = 1
        '
        'cmdOpen
        '
        Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdOpen.Location = New System.Drawing.Point(1272, 4)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(70, 30)
        Me.cmdOpen.TabIndex = 8
        Me.cmdOpen.Text = "Open"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'LabelPolicyNo
        '
        Me.LabelPolicyNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPolicyNo.AutoSize = True
        Me.LabelPolicyNo.Location = New System.Drawing.Point(165, 4)
        Me.LabelPolicyNo.Name = "LabelPolicyNo"
        Me.LabelPolicyNo.Size = New System.Drawing.Size(77, 20)
        Me.LabelPolicyNo.TabIndex = 9
        Me.LabelPolicyNo.Text = "Policy No."
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "C:\AcsME\CRS_Help\crs_help.chm"
        '
        'PanelPolicyQuickSearch
        '
        Me.PanelPolicyQuickSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.ProgressBar1)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.LabelPolicyNo)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.cmdOpen)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.txtPolicy)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbLife)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbBermuda)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbHNW)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbGI)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbEB)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbAssurance)
        Me.PanelPolicyQuickSearch.Controls.Add(Me.rbMcuLife)
        Me.PanelPolicyQuickSearch.Location = New System.Drawing.Point(-304, 4)
        Me.PanelPolicyQuickSearch.Name = "PanelPolicyQuickSearch"
        Me.PanelPolicyQuickSearch.Size = New System.Drawing.Size(1348, 37)
        Me.PanelPolicyQuickSearch.TabIndex = 17
        '
        'rbLife
        '
        Me.rbLife.AutoSize = True
        Me.rbLife.Checked = True
        Me.rbLife.Location = New System.Drawing.Point(496, 4)
        Me.rbLife.Name = "rbLife"
        Me.rbLife.Size = New System.Drawing.Size(60, 24)
        Me.rbLife.TabIndex = 14
        Me.rbLife.TabStop = True
        Me.rbLife.Text = "Life"
        Me.rbLife.UseVisualStyleBackColor = True
        '
        'rbBermuda
        '
        Me.rbBermuda.AutoSize = True
        Me.rbBermuda.Location = New System.Drawing.Point(955, 4)
        Me.rbBermuda.Name = "rbBermuda"
        Me.rbBermuda.Size = New System.Drawing.Size(99, 24)
        Me.rbBermuda.TabIndex = 19
        Me.rbBermuda.Text = "Bermuda" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rbBermuda.UseVisualStyleBackColor = True
        '
        'rbHNW
        '
        Me.rbHNW.AutoSize = True
        Me.rbHNW.Location = New System.Drawing.Point(1067, 4)
        Me.rbHNW.Name = "rbHNW"
        Me.rbHNW.Size = New System.Drawing.Size(82, 24)
        Me.rbHNW.TabIndex = 19
        Me.rbHNW.Text = "Private" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rbHNW.UseVisualStyleBackColor = True
        '
        'rbGI
        '
        Me.rbGI.AutoSize = True
        Me.rbGI.Location = New System.Drawing.Point(626, 4)
        Me.rbGI.Name = "rbGI"
        Me.rbGI.Size = New System.Drawing.Size(52, 24)
        Me.rbGI.TabIndex = 15
        Me.rbGI.Text = "GI"
        Me.rbGI.UseVisualStyleBackColor = True
        '
        'rbEB
        '
        Me.rbEB.AutoSize = True
        Me.rbEB.Location = New System.Drawing.Point(563, 3)
        Me.rbEB.Name = "rbEB"
        Me.rbEB.Size = New System.Drawing.Size(56, 24)
        Me.rbEB.TabIndex = 16
        Me.rbEB.Text = "EB"
        Me.rbEB.UseVisualStyleBackColor = True
        '
        'rbAssurance
        '
        Me.rbAssurance.AutoSize = True
        Me.rbAssurance.BackColor = System.Drawing.Color.White
        Me.rbAssurance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAssurance.Location = New System.Drawing.Point(683, 7)
        Me.rbAssurance.Name = "rbAssurance"
        Me.rbAssurance.Size = New System.Drawing.Size(135, 24)
        Me.rbAssurance.TabIndex = 17
        Me.rbAssurance.Text = "Assurance  "
        Me.rbAssurance.UseVisualStyleBackColor = False
        '
        'rbMcuLife
        '
        Me.rbMcuLife.AutoSize = True
        Me.rbMcuLife.BackColor = System.Drawing.Color.White
        Me.rbMcuLife.Location = New System.Drawing.Point(830, 4)
        Me.rbMcuLife.Name = "rbMcuLife"
        Me.rbMcuLife.Size = New System.Drawing.Size(112, 24)
        Me.rbMcuLife.TabIndex = 18
        Me.rbMcuLife.Text = "Macau Life"
        Me.rbMcuLife.UseVisualStyleBackColor = False
        '
        'frmCS2005_Asur
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
        Me.ClientSize = New System.Drawing.Size(1063, 650)
        Me.Controls.Add(Me.PanelPolicyQuickSearch)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.ToolBar1)
        Me.IsMdiContainer = True
        Me.Menu = Me.MainMenu1
        Me.Name = "frmCS2005_Asur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPolicyQuickSearch.ResumeLayout(False)
        Me.PanelPolicyQuickSearch.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    'Private Sub frmCS2005_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
    '    End
    'End Sub

    'Private Sub frmCS2005_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

    '    'StatusBar1.Controls.Add(ProgressBar1)
    '    'Me.StatusBarPanel3.Style = StatusBarPanelStyle.OwnerDraw

    '    'ProgressBar1.Value = 0

    'End Sub

    Private Sub Reposition(ByVal sender As Object,
         ByVal sbdevent As System.Windows.Forms.StatusBarDrawItemEventArgs) _
         Handles StatusBar1.DrawItem
        ProgressBar1.Location = New Point(sbdevent.Bounds.X,
           sbdevent.Bounds.Y)
        ProgressBar1.Size = New Size(sbdevent.Bounds.Width,
           sbdevent.Bounds.Height)
        ProgressBar1.Show()
    End Sub


    Private Sub frmCS2005_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CRS performer slowness - Since the first new WebClient takes a long time, async new it in advance
        PrepareWebClientAsync()

        wndInbox = New frmInbox
        wndInbox.MdiParent = Me
        wndInbox.Show()

        Dim lngErr As Long
        Dim strErr As String

        Me.Text = gSystem

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    Me.Text = gSystem & " - " & cCAPSIL
        'End If
        If gUAT = True Then
            Me.Text = gSystem & " - " & cCAPSIL & " - " & gLoginEnvStr
        End If
        'AC - Change to use configuration setting - end

        Using wsCRS As New CRSWS.CRSWS()

            'wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader)	
            wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsCRS.Timeout = 10000000

            dtPriv = wsCRS.GetPrivRS(0, gUPSystem, lngErr, strErr)

        End Using


        Try

            Dim retDs1 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_USER_PERMISSION", New Dictionary(Of String, String)() From {{"strUserID", gsUser}})
            If retDs1 IsNot Nothing AndAlso retDs1.Tables.Count > 0 AndAlso retDs1.Tables(0).Rows.Count > 0 Then
                dtFuncList = retDs1.Tables(0).Copy()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Information)
        End Try



        If Mid(strUPSMenuCtrl, 2, 1) = "0" Then
            'mnuSearchPolicy.Enabled = False
            mnuSearchPolicyAssur.Enabled = False
            tbSearchPolicy.Enabled = False
        End If
        If Not (CheckUPSAccessFunc("Search Customer (Bermuda)") OrElse CheckUPSAccessFunc("Search Customer (Assurance)") OrElse CheckUPSAccessFunc("Search Customer (Macau)")) Then
            'Me.mnuSearchCust.Enabled = False
            Me.mnuSearchCustAssur.Enabled = False
            tbSearchCust.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 4, 1) = "0" Then
            Me.mnuPrintFaxReports.Enabled = False
            tbPrint.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 47, 1) = "0" Then
            'Me.mnuCSR.Enabled = False
            Me.mnuCSRMaintenance.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 48, 1) = "0" Then
            'Me.mnuAlert.Enabled = False
            Me.mnuCustomerAlertMaintenance.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 49, 1) = "0" Then
            'Me.mnuPolProj.Enabled = False
            Me.mnuPolicyValueProjection.Enabled = False
        End If
        'If Mid(strUPSMenuCtrl, 50, 1) = "0" Then
        '    Me.mnuReprintCard.Enabled = False
        'End If
        If Mid(strUPSMenuCtrl, 51, 1) = "0" Then
            Me.mnuPRTS.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 52, 1) = "0" Then
            Me.mnuIRTS.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 53, 1) = "0" Then
            Me.mnuUVAL.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 54, 1) = "0" Then
            Me.mnuBANK.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 65, 1) = "0" Then
            'Me.mnuImport.Enabled = False
            Me.mnuImportMisc.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 66, 1) = "0" Then
            Me.mnuDelBatch.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 67, 1) = "0" Then
            'Me.mnuCSList.Enabled = False
            Me.mnuCSPolicyList.Enabled = False
        End If
        If Mid(strUPSMenuCtrl, 68, 1) = "0" Then
            'Me.mnuCRM.Enabled = False
            Me.mnuCampaignManagement.Enabled = False
        End If

        If Mid(strUPSMenuCtrl, 82, 1) = "0" Then
            Me.mnuPolicyMapping.Enabled = False
        End If

        If Mid(strUPSMenuCtrl, 84, 1) = "0" Then
            Me.mnuiFWDFreePolicy.Enabled = False
        End If

        'KT20150611
        If Mid(strUPSMenuCtrl, 85, 1) = "0" Then
            wndInbox.cboPending.Enabled = False
        End If
        'KT20150611

        'If Mid(strUPSMenuCtrl, 86, 1) = "0" Then
        '    mnuPostSalesCallList.Enabled = False
        '    mnuPostSalesCallFundRisk.Enabled = False
        '    mnuPostSalesCallProduct.Enabled = False
        '    mnuPostSalesCallQuestion.Enabled = False
        'End If

        'ITDCIC 7-11 & ISC claim payout - 7Eleven User List
        'If Mid(strUPSMenuCtrl, 89, 1) = "0" Then
        '    mum7ElevenPayout.Enabled = False
        'End If

        If g_Comp = "HKL" Then
            Me.mnuPolicyMapping.Enabled = False
        End If

        'Macau Service Log
        Me.mnuMacauServiceLog.Enabled = False
        If g_Comp.ToUpper() = "ING" Then
            If Not Mid(strUPSMenuCtrl, 87, 1) = "0" Then
                Me.mnuMacauServiceLog.Enabled = True
            End If
        ElseIf g_Comp.ToUpper() = "MCU" Then
            Dim blnResult As Boolean = False
            Boolean.TryParse(ConfigurationManager.AppSettings.Item(String.Format("MCUUSR_{0}", gsUser.ToUpper.Trim())), blnResult)
            Me.mnuMacauServiceLog.Enabled = blnResult
        End If



        objNBA = Nothing
        objNBA = New NewBusinessAdmin.NBLifeAdmin

        Dim tempDBHeader As Utility.Utility.ComHeader = gobjDBHeader

        'oliver 2023-12-13 commented for optimized login speed
        'Call CheckUpdateNbSystemTable(tempDBHeader, dsComponentSysTable, gSysTableLastUpdate) 'ITSR933 FG R3 Performance Tuning

        Dim dtUserAuthority As New DataTable
        Dim objCI As New LifeClientInterfaceComponent.CommonControl With {
            .ComHeader = tempDBHeader
        }
        Call objCI.GetUserAuthority(dtUserAuthority, strErr)
        ' only schema, no data(Authority) required (To avoid error in NBLifeAdmin.vb)
        dtUserAuthority.Clear()

        objNBA.ComHeaderInUse = tempDBHeader
        objNBA.dtAuthorityInUse = dtUserAuthority
        objNBA.Text = "New Business Administration (" & gobjDBHeader.CompanyID & ") Main Menu (" & gobjDBHeader.EnvironmentUse & ")"
        objNBA.Show()
        objNBA.Visible = False



        HelpProvider1.HelpNamespace = gLOCAL_HELP_PATH
        HelpProvider1.SetShowHelp(Me, True)
        ' ES005 begin
        CheckKBUpdate()
        ' ES005 end


        'CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    'mnuPrintReport.Enabled = False
        '    'mnuReprintCard.Enabled = False
        '    mnuReprintCustomerCardPIN.Enabled = False
        'End If
        'CRS 7x24 Changes - End


        Try
            'ITDCIC New Opt-out Option Start
            mnuCrmOptInOut.Visible = False
            mnuCrmOptInOut.Enabled = False
            Dim objCheckCrmUserResponse As CRS_Util.clsJSONBusinessObj.clsCheckCrmUserResponse
            objCheckCrmUserResponse = CRS_Util.clsJSONTool.CheckCrmUser(gsUser)
            If Not objCheckCrmUserResponse Is Nothing AndAlso objCheckCrmUserResponse.validUser Then
                mnuCrmOptInOut.Visible = True
                mnuCrmOptInOut.Enabled = True
            Else
                mnuCrmOptInOut.Visible = False
                mnuCrmOptInOut.Enabled = False
            End If
            'ITDCIC New Opt-out Option End
        Catch
            MessageBox.Show("Connot connect to check CRM Opt-out.")
        End Try

        'KT20171207
        mnuChatbotCIC.Visible = False
        mnuChatbotCIC.Enabled = False
        If checkChatbotCICUserRight(gsUser) Then
            mnuChatbotCIC.Visible = True
            mnuChatbotCIC.Enabled = True
        Else
            mnuChatbotCIC.Visible = False
            mnuChatbotCIC.Enabled = False
        End If
        'KT20171207



        'ITDCPI new access control for Icontek Chatbot item start
        Dim dtUserGroup As New DataTable("UserGroup")
        Dim i As Integer

        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_USER_RIGHT", New Dictionary(Of String, String)() From {{"strUserID", gsUser}})
        If Not IsNothing(retDs) Then
            If retDs.Tables(0).Rows.Count > 0 Then
                dtUserGroup = retDs.Tables(0)
            End If
        End If

        If dtUserGroup.Rows.Count > 0 Then
            For i = 0 To dtUserGroup.Rows.Count - 1 '308=CS_Admin (ISC CSR), 344=CS_Admin1 (Call Center CSR), 345=CS_Admin2 (CS Mgr)
                If dtUserGroup.Rows(i)("AccessItem") = "344" Or dtUserGroup.Rows(i)("AccessItem") = "345" Then
                    mnuIcontekCeAssistance.Enabled = True
                    mnuIcontekChatbotAdmin.Enabled = True
                End If
            Next
        End If

        'ITDCPI Enable function exclusively for CS Manager (345=CS_Admin2) start
        For i = 0 To dtUserGroup.Rows.Count - 1 '308=CS_Admin (ISC CSR), 344=CS_Admin1 (Call Center CSR), 345=CS_Admin2 (CS Mgr)
            If dtUserGroup.Rows(i)("AccessItem") = "345" Then
                mnuServicelogRetrieve.Enabled = True
                mnuNBMServicelogRetrieve.Enabled = True
                mnuSMSTempMgt.Enabled = True
            End If
        Next

        'ITDCPI Enable function exclusively for CS Manager (345=CS_Admin2) end
        For i = 0 To dtUserGroup.Rows.Count - 1
            If dtUserGroup.Rows(i)("AccessGrp").ToString().Trim = "MO_711" Then
                mum7ElevenPayout.Enabled = True
                bMO = True

                mnuFile.Enabled = False
                mnuUtility.Enabled = False
                mnuPRTS.Enabled = False
                mnuIRTS.Enabled = False
                mnuUVAL.Enabled = False
                mnuBANK.Enabled = False
                mnuPolicyMapping.Enabled = False
                'MenuItem11.Enabled = False
                mnuTransactionLog.Enabled = False
                'MenuItem12.Enabled = False
                mnuExchangeRate.Enabled = False
                'MenuItem14.Enabled = False
                mnuVPOQuotation.Enabled = False
                'MenuItem15.Enabled = False
                mnuServiceFee.Enabled = False
                'MenuItem16.Enabled = False
                mnuDMPSServiceFee.Enabled = False
                mnuiFWDFreePolicy.Enabled = False
                mnuMacauServiceLog.Enabled = False
                mnuHKNonCustLog.Enabled = False
                mnuCrmOptInOut.Enabled = False
                mnuChatbotCIC.Enabled = False
                mnuBackdoorReg.Enabled = False
                mnuIcontekCeAssistance.Enabled = False
                mnuIcontekChatbotAdmin.Enabled = False
                mnuCoBrowsing.Enabled = False
                mnuAgentAssist.Enabled = False
                cmdOpen.Enabled = False
                ToolBar1.Enabled = False
                tbSearchCust.Enabled = False
                tbSearchPolicy.Enabled = False
                tbPrint.Enabled = False
                ToolBarButton1.Enabled = False
                tbRefresh.Enabled = False
            End If
        Next

        For i = 0 To dtUserGroup.Rows.Count - 1
            If dtUserGroup.Rows(i)("AccessGrp").ToString().Trim = "CS_ADMIN_1" Then
                mnuAgentAssist.Enabled = True
                mnuHKNonCustLog.Enabled = True
                mnuBackdoorReg.Enabled = True
                mnuCoBrowsing.Enabled = True
                mnuCrsPreference.Enabled = True
                mnuSMSTempMgt.Enabled = True
            End If
            If dtUserGroup.Rows(i)("AccessGrp").ToString().Trim = "CS_ADMIN_2" Then
                mnuAgentAssist.Enabled = True
                mnuHKNonCustLog.Enabled = True
                mnuBackdoorReg.Enabled = True
                mnuCoBrowsing.Enabled = True
                mnuCrsPreference.Enabled = True
            End If
        Next
    End Sub


    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick

        If e.Button.Tag = "TB_REFRESH" Then
            wndInbox.RefreshInbox()
        End If

        If e.Button.Tag = "TB_PRINT" Then
            Dim w As New frmReport_Asur
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        End If

        If e.Button.Tag = "SEARCHP" Then
            ' CRS performer slowness - Use the unified portal to open policy summary screen
            'Dim w As New frmSearchPolicy
            Dim w As New frmSearchPolicyAsur
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        End If

        If e.Button.Tag = "SEARCHC" Then
            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda  
            'Dim w As New frmSearchCust
            Dim w As New frmSearchCustAsur
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        End If

    End Sub

    Private Sub mnuPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintFaxReports.Click
        Dim w As New frmReport_Asur
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub StatusBar1_PanelClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.StatusBarPanelClickEventArgs) Handles StatusBar1.PanelClick
        e.StatusBarPanel.ToolTipText = e.StatusBarPanel.Text
    End Sub

    Private Sub frmCS2005_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        If MsgBox("Please click OK to confirm exit CRS", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "CRS") <> MsgBoxResult.Ok Then
            Return
        End If

        wndInbox.Dispose()
#If STRESS <> 0 Then
        writer.Close()	
#End If
        'HT20151201	
        Try
            If Not IE Is Nothing Then
                IE.Quit()
            End If
            End
        Catch ex As Exception
            End
        End Try
        'HT20151201	
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now
        ' ' ITSR-4063 	
        ' Do Checking for UHNW	
        If String.IsNullOrEmpty(txtPolicy.Text.Trim) Then
            Return
        End If
        If rbMcuLife.Checked Then
            Dim isUHNWPolicy As Boolean = False
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(IIf(rbMcuLife.Checked = True, g_McuComp, g_Comp), "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                    {"PolicyNo", txtPolicy.Text.Trim}
                    })
            If Not retDs.Tables Is Nothing Then
                If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                    isUHNWPolicy = True
                End If
                SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, txtPolicy.Text.Trim, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
            End If

            Dim isUHNWMemberFlag As Boolean = False
            isUHNWMemberFlag = IIf(rbMcuLife.Checked = True, isUHNWMemberMcu, isUHNWMember)
            SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, txtPolicy.Text.Trim, IIf(rbMcuLife.Checked = True, "sUHNWMemberMcu", "isUHNWMember"), IIf(isUHNWMemberFlag, "1", "0"))
            SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, txtPolicy.Text.Trim, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))
            If isUHNWMemberFlag And isUHNWPolicy Then
                MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            ElseIf Not isUHNWMemberFlag And isUHNWPolicy Then
                MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                Exit Sub
            End If
        End If

        'HT20151201
        If rbGI.Checked Then

            Dim sPolicyNo As String = checkPolicyEndDate(txtPolicy.Text.Trim)
            If sPolicyNo <> "" Then
                If ShowGIDetailPage(sPolicyNo) Then
                    Call GIServiceLog(sPolicyNo)
                End If
            Else
                MsgBox("Policy No. Expired")
            End If

            Exit Sub

        ElseIf rbEB.Checked Then

            'If Not BlockPolicy(Microsoft.VisualBasic.Left(txtPolicy.Text, 12).Trim) Then
            '    MsgBox("FWD staff / special handle policy ¡V please contact EB.", MsgBoxStyle.Information)
            '    Exit Sub
            'End If

            Dim sPolicyNo As String
            'GL and LTD policy
            If Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "12" Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "22" _
                Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "13" Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "23" Then
                sPolicyNo = GLCheckPolicyEndDate(txtPolicy.Text.Trim)
            Else
                sPolicyNo = checkPolicyEndDate(txtPolicy.Text.Trim)
            End If

            If sPolicyNo <> "" Then
                If ShowEBDetailPage(sPolicyNo) Then
                    Call GIServiceLog(sPolicyNo)
                End If
            Else
                MsgBox("Policy No. Expired")
            End If

            Exit Sub

        End If

        ' start - Added by Hugo Chan on 2021-05-25, CRS - First Level of Access
        If rbAssurance.Checked Then
            strPolicyNo = ""
            strPolicyNo = RTrim(txtPolicy.Text)
            Dim strerr As String = ""
            Dim policyBL As New PolicySearchBL
            policyBL.GetPolicy(txtPolicy.Text, strerr, sender, e, "LAC")
            'SearchAssurancePolicy(txtPolicy.Text)
            Exit Sub
        End If
        ' end - Added by Hugo Chan on 2021-05-25

        If rbBermuda.Checked Then
            strPolicyNo = ""
            strPolicyNo = RTrim(txtPolicy.Text)
            Dim strerr As String = ""
            Dim policyBL As New PolicySearchBL
            policyBL.GetPolicy(txtPolicy.Text, strerr, sender, e, "ING")
            Exit Sub
        End If

        'oliver 2024-7-31 added for Com 6
        If rbHNW.Checked Then
            If Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                Exit Sub
            End If
            strPolicyNo = ""
            strPolicyNo = RTrim(txtPolicy.Text)
            Dim strerr As String = ""
            Dim policyBL As New PolicySearchBL
            policyBL.GetPolicy(txtPolicy.Text, strerr, sender, e, "BMU")
            Exit Sub
        End If

        Try
            If rbMcuLife.Checked Then
                strPolicyNo = RTrim(txtPolicy.Text)
                Dim policyBL As New PolicySearchBL
                policyBL.GetPolicyMcu(strPolicyNo)
                txtPolicy.Text = strPolicyNo
            ElseIf rbLife.Checked = True Then
                strPolicyNo = ""
                strPolicyNo = RTrim(txtPolicy.Text)
                Dim strerr As String = ""
                Dim policyBL As New PolicySearchBL
                policyBL.GetPolicy(txtPolicy.Text, strerr, sender, e)
            End If 'End ElseIf rbLife.Checked





        Catch ex As Exception
            MsgBox(ex.Message)
            appSettings.Logger.logger.Error(ex.Message.ToString)
        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "", strMainStartTime, strMainEndTime, strPolicyNo, "", "")

        End Try


    End Sub

    Private Sub mnuPRTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPRTS.Click

        Dim w As New frmPRTS
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuIRTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIRTS.Click

        Dim w As New frmIRTS
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuUVAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUVAL.Click

        Dim w As New frmUVAL
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuBANK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBANK.Click

        Dim w As New frmBANK
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuReprintCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReprintCustomerCardPIN.Click

        Dim w As New frmReprintCard
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuPolProj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPolicyValueProjection.Click

        Dim w As New frmPolicyProj
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerAlertMaintenance.Click

        Dim w As New frmAlert
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuCSR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCSRMaintenance.Click

        Dim w As New frmCSR
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuCRM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCampaignManagement.Click
        ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda  
        'Dim w As New frmCRM
        Dim w As New frmCRM_Asur
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportMisc.Click

        Dim w As New frmMiscType
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuDelBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelBatch.Click

        Dim w As New frmDelMiscBatch
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuCSList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCSPolicyList.Click

        Dim w As New frmCSPolicy
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub mnuPolicyMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPolicyMapping.Click


        'New Policy Mapping From form LAS for Assurance
        Dim w As New POSMain.frmPolicyMapping_MultipleCompany

        Dim objDBHeader As Utility.Utility.ComHeader

        objDBHeader.UserID = gsUser
        objDBHeader.EnvironmentUse = g_LacEnv
        objDBHeader.ProjectAlias = "LAS" '"LAS"
        objDBHeader.CompanyID = g_LacComp '"ING"
        objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"
        w.DBHeader = objDBHeader
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearchCSKnowledgeBase.Click
        Help.ShowHelp(Me, HelpProvider1.HelpNamespace)
    End Sub

    'Private Sub objCICServer_CallIn(ByVal strCustID As String, ByVal strName As String, ByVal strClientID As String) Handles objCICServer.CallIn

    '    If Me.InvokeRequired Then
    '        Dim args() As Object = {strCustID, strName, strClientID}
    '        Dim dlgUI As UpdateUIDelegate = New UpdateUIDelegate(AddressOf objCICServer_CallIn)
    '        Me.Invoke(dlgUI, args)
    '    Else

    '        MsgBox("The caller¡¦s mobile number matches with the policy owner¡¦s number", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
    '        ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda  
    '        'Dim w As New frmCustomer
    '        Dim w As New frmCustomer_Asur
    '        w.IsGI = False
    '        w.CustID(strClientID) = strCustID
    '        w.Text = "Customer " & strName

    '        If ShowWindow(w, wndMain, RTrim(w.Text)) Then
    '        Else
    '            w.Dispose()
    '        End If
    '    End If

    'End Sub

    Private Sub MenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCheckUpdateofCSKB.Click
        CheckKBUpdate(True)
    End Sub

    Private Sub CheckKBUpdate(Optional ByVal blnMsg As Boolean = False)
        Dim strHelpPath As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "HelpFilePath")

        Try
            ' check remote file
            If File.Exists(strHelpPath) Then
                Dim remoteFile As New FileInfo(strHelpPath)

                ' check local file
                If File.Exists(gLOCAL_HELP_PATH) Then
                    Dim localFile As New FileInfo(gLOCAL_HELP_PATH)

                    If remoteFile.LastWriteTime > localFile.LastWriteTime Then
                        File.Copy(strHelpPath, gLOCAL_HELP_PATH, True)
                        If blnMsg Then
                            MsgBox("There is a new version of CS Knowledge Base file copied to your PC.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        End If
                    Else
                        If blnMsg Then
                            MsgBox("No new CS Knowledge Base update available.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        End If
                    End If
                Else
                    File.Copy(strHelpPath, gLOCAL_HELP_PATH, True)
                    If blnMsg Then
                        MsgBox("There is a new version of CS Knowledge Base file copied to your PC.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub MenuItem12_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExchangeRate.Click
        Dim fExRate As New frmExchgRate
        fExRate.Show()
    End Sub

    ' Flora Leung, Project Leo Goal 3, 29-May-2012, Start
    Private Sub MenuItem14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPOQuotation.Click
        Dim fVPOQuotation As New POSMain.frmVPOQuotation
        fVPOQuotation.DBHeader = gobjDBHeader
        gobjMQQueHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse
        fVPOQuotation.MQHeader = gobjMQQueHeader
        fVPOQuotation.POSHeader = gobjPOSHeader
        fVPOQuotation.Show()

        Dim objPOSHead As Utility.Utility.POSHeader
        objPOSHead.TransID = "0" '*********************************
        objPOSHead.TranType = "VPO" '*********************************

        If Me.Text.Contains("Policy") Then
            fVPOQuotation.SetPolicyNumber = strPolicyNo
        Else
            fVPOQuotation.SetPolicyNumber = ""
        End If
        fVPOQuotation.DBHeader = gobjDBHeader
        fVPOQuotation.MQHeader = gobjMQQueHeader
        fVPOQuotation.POSHeader = gobjPOSHeader

        fVPOQuotation.SetUpdateFlag = True

        fVPOQuotation.Show()

    End Sub
    ' Flora Leung, Project Leo Goal 3, 29-May-2012, End

    Private Sub MenuItem15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuServiceFee.Click
        Dim fServiceFee As New POSMain.frmServiceFee
        fServiceFee.DBHeader = gobjDBHeader
        gobjMQQueHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse
        If Me.Text.Contains("Policy") Then
            fServiceFee.SetPolicyNumber = strPolicyNo
        Else
            fServiceFee.SetPolicyNumber = ""
        End If
        fServiceFee.DBHeader = gobjDBHeader
        fServiceFee.MQHeader = gobjMQQueHeader
        fServiceFee.POSHeader = gobjPOSHeader

        fServiceFee.Controls("btnChange").Enabled = False
        fServiceFee.Controls("btnConfirm").Enabled = False
        fServiceFee.Show()

    End Sub

    Private Sub MenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDMPSServiceFee.Click
        Dim fServiceFee As New POSMain.frmServiceFee
        fServiceFee.DBHeader = gobjDBHeader
        gobjMQQueHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse
        If Me.Text.Contains("Policy") Then
            fServiceFee.SetPolicyNumber = strPolicyNo
        Else
            fServiceFee.SetPolicyNumber = ""
        End If
        fServiceFee.DBHeader = gobjDBHeader
        fServiceFee.MQHeader = gobjMQQueHeader
        fServiceFee.POSHeader = gobjPOSHeader

        fServiceFee.IsDpms = True

        fServiceFee.Controls("btnChange").Enabled = False
        fServiceFee.Controls("btnConfirm").Enabled = False
        fServiceFee.Show()
    End Sub

    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiFWDFreePolicy.Click
        Dim w As New frmiFWDFreePolicyEnq
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If

    End Sub

    Private Sub GIServiceLog(ByVal sPolicyNo As String)
        If Not wGIServiceLog Is Nothing Then
            wGIServiceLog.Dispose()
        End If
        wGIServiceLog = New frmGIServiceLog

        If Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "12" Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "22" _
                    Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "13" Or Microsoft.VisualBasic.Left(txtPolicy.Text.Trim, 2) = "23" Then
            wGIServiceLog.CustomerID = getCustomerID("GL" & sPolicyNo)
        Else
            wGIServiceLog.CustomerID = getCustomerID(sPolicyNo)
        End If

        wGIServiceLog.PolicyAccountID = sPolicyNo
        wGIServiceLog.PolicyType = "GL"
        If Not OpenWindow(wGIServiceLog, Me) Then
            wGIServiceLog.Dispose()
        End If

    End Sub

    Private Function ShowGIDetailPage(ByVal sPolicyNo As String) As Boolean
        Dim aCursor = Me.Cursor
        Try
            Me.Cursor = Cursors.WaitCursor
            ShowGIDetailPage = GIDetailPage(sPolicyNo)
        Finally
            Me.Cursor = aCursor
        End Try
    End Function

    Private Function ShowEBDetailPage(ByVal sPolicyNo As String) As Boolean
        Dim aCursor = Me.Cursor
        Try
            Me.Cursor = Cursors.WaitCursor
            ShowEBDetailPage = EBDetailPage(sPolicyNo)
        Finally
            Me.Cursor = aCursor
        End Try
    End Function

    Private Sub mnuPostSalesCallQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallQuestion.Click
        Dim frmQuestionMaster As New frmPostSalesQuestionnaireMaster(FormModeEnum.Enquiry)

        frmQuestionMaster.objDBHeader = gobjDBHeader
        frmQuestionMaster.MdiParent = Me
        frmQuestionMaster.Show()
    End Sub

    Private Sub mnuPostSalesCallProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallProduct.Click
        Dim frmProductMaster As New frmPostSalesCallProductSetting()

        frmProductMaster.objDBHeader = gobjDBHeader
        frmProductMaster.MdiParent = Me
        frmProductMaster.Show()
    End Sub

    Private Sub MenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallFundRisk.Click
        Dim frmFundRisk As New frmFundRiskLevel()
        frmFundRisk.objDBHeader = gobjDBHeader

        frmFundRisk.MdiParent = Me
        frmFundRisk.Show()
    End Sub

    Private Sub mnuPostSalesCallList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallList.Click
        Using frmCallList As New frmPostSalesCallList()
            frmCallList.sCompId = "HK"
            frmCallList.objDBHeader = gobjDBHeader
            frmCallList.ShowDialog()

        End Using
    End Sub

    Private Sub mnuMacauServiceLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMacauServiceLog.Click
        Dim w As New frmMCUTransLog
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuCrmOptInOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCrmOptInOut.Click
        'CRMSearchPage(gobjDBHeader.UserID)
        Dim frmCrmOptinout As New frmCrmOptInOut
        frmCrmOptinout.Show()
    End Sub

    Private Sub mum7ElevenPayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mum7ElevenPayout.Click
        Dim w As New frm7ElevenPayout
        If gUAT Then
            If bMO Then
                w.strMerchantId = "202000001" 'UAT Macau
            Else
                w.strMerchantId = "201000001" 'UAT HK
            End If
        Else
            If bMO Then
                w.strMerchantId = "302000001" 'Prod Macau
            Else
                w.strMerchantId = "301000001" 'Prod HK
            End If
        End If
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    'KT20171207
    Private Sub mnuChatbotCIC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChatbotCIC.Click
        Dim fullname As String = getFullNameFromProfileDB(gobjDBHeader.UserID)
        OpenChatbotCIC(gobjDBHeader.UserID, fullname)
    End Sub

    Private Sub mnuBackdoorReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBackdoorReg.Click
        Dim w As New frmBackdoorReg
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    'ITDCPI open CE Assistance for 2019 New Chatbot start
    Private Sub mnuIcontekCeAssistance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIcontekCeAssistance.Click
        ChatbotCEAssistancePage(gsUser.ToString.Trim)
    End Sub
    'ITDCPI open CE Assistance for 2019 New Chatbot end

    'ITDCPI open Admin console for 2019 New Chatbot start
    Private Sub mnuIcontekChatbotAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIcontekChatbotAdmin.Click
        ChatbotAdminconsole(gsUser.ToString.Trim)
    End Sub
    'ITDCPI open Admin console for 2019 New Chatbot end

    'ITDCPI open HK Non-Customer Service Log start
    Private Sub mnuHKNonCustLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHKNonCustLog.Click
        Dim w As New frmHKTransLog
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub
    'ITDCPI open HK Non-Customer Service Log end

    'ITDCPI open CRS Preference start
    Private Sub mnuCrsPreference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCrsPreference.Click
        Dim w As New frmUserPreference
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub
    'ITDCPI open CRS Preference end

    'ITDCPI open Service Log Retrieve start
    Private Sub mnuServicelogRetrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServicelogRetrieve.Click
        Dim w As New frmServiceLogRetrieve
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub
    'ITDCPI open Service Log Retrieve end

    'ITDCPI open NBM Service Log Retrieve start
    Private Sub mnuNBMServicelogRetrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNBMServicelogRetrieve.Click
        Dim w As New frmServiceLogRetrieve
        w.IsNBM = True
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub
    'ITDCPI open NBM Service Log Retrieve end

    Private Sub mnuSMSTempMgt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSMSTempMgt.Click
        Dim w As New frmSmsTempMgt
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    'ITSR933 FG R3 Performance Tuning Start

    'Private Sub CheckUpdateNbSystemTable(ByVal objDBHeader1 As Utility.Utility.ComHeader, ByRef dsSysTable1 As DataSet, ByRef dtLastUpdate As DateTime)
    '    Try
    '        Dim iHoursSinceLastUpdate As Integer = Math.Abs(DateDiff(DateInterval.Hour, dtLastUpdate, Now))
    '        If iHoursSinceLastUpdate >= 12 Then
    '            Dim objCI As New LifeClientInterfaceComponent.CommonControl
    '            objCI.ComHeader = objDBHeader1
    '            Dim strErr As String = ""

    '            dsSysTable1 = New DataSet

    '            If objCI.GetNbSysInfo(dsSysTable1, strErr) Then
    '                dtLastUpdate = Now
    '            ElseIf strErr.Trim = "" Then
    '                MsgBox("Cannot get component system table!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
    '            Else
    '                MsgBox("CheckUpdateNbSystemTable: " & strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error") 'Temp Comment, TODO: UTCE04/UTCE05 MQ INGT106 issue
    '                appSettings.Logger.logger.Error(strErr)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
    '    End Try
    'End Sub
    'ITSR933 FG R3 Performance Tuning End

    'CoBrowsing
    Private Sub mnuCoBrowsing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCoBrowsing.Click
        Try
            Dim tsNow As Long = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim strNow As String = tsNow.ToString

            Dim strHashidToken As String = String.Empty
            Const strSalt = "R63ucFwNi2Wl11EkVe4s"
            Dim ids As New HashidsNet.Hashids(strSalt + Environment.UserName.ToUpper(), 8, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
            strHashidToken = ids.EncodeLong(Long.Parse(strNow))

            Dim strUrl As String = My.Settings.CS2005_CoBrowsUrl + "&user=" + Environment.UserName.ToUpper() + "&token=" + strHashidToken

            Process.Start("Chrome", strUrl)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub mnuAgentAssist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAgentAssist.Click
        Try
            Dim frmAgentInput As New frmAgentCodeInput
            If frmAgentInput.ShowDialog() = DialogResult.OK Then
                Dim txtTmp As String = Environment.UserName.ToUpper().Trim & frmAgentInput.txtAgentCode.Text.Trim & Date.Now.ToString("yyyyMMddHH") & "FWD@2020"
                Dim strUrl As String = My.Settings.CS2005_AAUrl & frmAgentInput.txtAgentCode.Text.Trim & "-" & Environment.UserName.ToUpper().Trim & "/" & Hash(txtTmp)
                Process.Start("Chrome", strUrl)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function Hash(ByVal content As String) As String
        Dim Sha256 As New Security.Cryptography.SHA256CryptoServiceProvider
        Dim ByteString() As Byte = System.Text.Encoding.ASCII.GetBytes(content)
        ByteString = Sha256.ComputeHash(ByteString)

        Dim ReturnString As String = Nothing
        For Each bt As Byte In ByteString
            ReturnString &= bt.ToString("x2")
        Next
        Return ReturnString
    End Function

    Private Sub mnuSearchPolicyMac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearchPolicyMac.Click
        Dim w As New frmSearchPolicyMcu
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuPostSalesCallListMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenPostSalesCallListMcu.Click
        Using frmCallList As New frmPostSalesCallList()

            frmCallList.objDBHeader = gobjDBHeader
            frmCallList.ShowDialog()

        End Using
    End Sub

    Private Sub mnuServicelogRetrieveMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServicelogRetrieveMcu.Click
        Dim w As New frmServiceLogRetrieve
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuPrintReportMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintReportMcu.Click
        Dim w As New frmReportMcu
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuPostSalesCallProductMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallProductMcu.Click

        Dim frmProductMaster As New frmPostSalesCallProductSettingMcu()

        frmProductMaster.objDBHeader = gobjMcuDBHeader
        frmProductMaster.MdiParent = Me
        frmProductMaster.Show()

    End Sub

    Private Sub mnuPostSalesCallQuestionMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostSalesCallQuestionMcu.Click

        Dim frmQuestionMaster As New frmPostSalesQuestionnaireMasterMcu(FormModeEnum.Enquiry)

        frmQuestionMaster.objDBHeader = gobjMcuDBHeader
        frmQuestionMaster.MdiParent = Me
        frmQuestionMaster.Show()

    End Sub

    Private Sub mnuAgentAssistMacau_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAgentAssistMacau.Click
        Try
            Dim frmAgentInput As New frmAgentCodeInput
            If frmAgentInput.ShowDialog() = DialogResult.OK Then
                Dim txtTmp As String = Environment.UserName.ToUpper().Trim & frmAgentInput.txtAgentCode.Text.Trim & Date.Now.ToString("yyyyMMddHH") & "FWD@2020" 'FWD@2021 for UAT 	
                Dim strUrl As String = My.Settings.CS2005_AAUrl_MC & frmAgentInput.txtAgentCode.Text.Trim & "-" & Environment.UserName.ToUpper().Trim & "/" & Hash(txtTmp)
                Process.Start("Chrome", strUrl)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub mnuGeneralQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGeneralQuery.Click
        Dim frmGeneralQueryDataGrid As New frmGeneralQueryDataGrid()
        frmGeneralQueryDataGrid.MdiParent = Me
        frmGeneralQueryDataGrid.Show()
    End Sub

    Private Sub mnuEServiceOptInOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEServiceOptInOut.Click
        Try
            'MsgBox("mnuEServiceOptInOut_Click", MsgBoxStyle.Information)	
            Dim w As New frmEServiceOptInOut
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub mnuLCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLCP.Click
        Try
            Dim lcp As New LCPClient.frmLcpMain()
            lcp.ShowLogonWindows = False
            LCPClient.LcpWebServiceClient.UserSystem = "ALL"
            LCPClient.LcpWebServiceClient.CompanyID = gobjMQQueHeader.CompanyID
            LCPClient.LcpWebServiceClient.EnvironmentUse = gobjMQQueHeader.EnvironmentUse
            LCPClient.LcpWebServiceClient.UserID = gobjMQQueHeader.UserID
            LCPClient.LcpWebServiceClient.UserType = gobjMQQueHeader.UserType
            LCPClient.LcpWebServiceClient.Project = gobjMQQueHeader.ProjectAlias
            lcp.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub mnuLCPLac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLCP_LAC.Click
        Try
            Dim lcp As New LCPClient.frmLcpMain()
            lcp.ShowLogonWindows = False
            LCPClient.LcpWebServiceClient.UserSystem = "ALL"
            LCPClient.LcpWebServiceClient.CompanyID = gobjLacMQQueHeader.CompanyID
            LCPClient.LcpWebServiceClient.EnvironmentUse = gobjLacMQQueHeader.EnvironmentUse
            LCPClient.LcpWebServiceClient.UserID = gobjLacMQQueHeader.UserID
            LCPClient.LcpWebServiceClient.UserType = gobjLacMQQueHeader.UserType
            LCPClient.LcpWebServiceClient.Project = gobjLacMQQueHeader.ProjectAlias
            lcp.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub mnuLCPLah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLCP_LAH.Click
        Try
            Dim lcp As New LCPClient.frmLcpMain()
            lcp.ShowLogonWindows = False
            LCPClient.LcpWebServiceClient.UserSystem = "ALL"
            LCPClient.LcpWebServiceClient.CompanyID = gobjLahMQQueHeader.CompanyID
            LCPClient.LcpWebServiceClient.EnvironmentUse = gobjLahMQQueHeader.EnvironmentUse
            LCPClient.LcpWebServiceClient.UserID = gobjLahMQQueHeader.UserID
            LCPClient.LcpWebServiceClient.UserType = gobjLahMQQueHeader.UserType
            LCPClient.LcpWebServiceClient.Project = gobjLahMQQueHeader.ProjectAlias
            lcp.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Click the mnuFindUserName button to jump to the frmFindUser page
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-01 Updated for CRS Enhancement(General Enhance Ph4)Point A-4 </br>
    ''' </remarks>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuFindUserName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFindUserName.Click
        Try
            Dim w As New frmFindUser
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub mnuPolicyMappingMcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPolicyMappingMcu.Click
        Dim w As New frmPolicyMapping
        w.sCompanyID = "MCU"
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuSearchPolicyAssur_Click(sender As Object, e As EventArgs) Handles mnuSearchPolicyAssur.Click
        Dim w As New frmSearchPolicyAsur
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub

    Private Sub mnuPostSalesCallListPri_Click(sender As Object, e As EventArgs) Handles mnuPostSalesCallListPri.Click
        Using frmCallList As New frmPostSalesCallList()
            frmCallList.sCompId = "BMU"
            frmCallList.objDBHeader = gobjDBHeader
            frmCallList.ShowDialog()
        End Using
    End Sub

    Private Sub mnuTransactionLogBer_Click(sender As Object, e As EventArgs) Handles mnuTransactionLogBer.Click
        Dim fTransLog As New POSMain.frmTransLog
        fTransLog.DBHeader = gobjDBHeader
        fTransLog.MQQueuesHeader = gobjMQQueHeader
        fTransLog.POSHeader = gobjPOSHeader
        fTransLog.Show()
    End Sub

    Private Sub mnuTransactionLogLac_Click_1(sender As Object, e As EventArgs) Handles mnuTransactionLogLac.Click
        Dim fTransLog As New POSMain.frmTransLog

        fTransLog.DBHeader = gobjLacDBHeader
        fTransLog.MQQueuesHeader = gobjLacMQQueHeader
        fTransLog.POSHeader = gobjLacPOSHeader
        fTransLog.Show()
    End Sub

    Private Sub mnuTransactionLogLah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransactionLogLah.Click
        Dim fTransLog As New POSMain.frmTransLog

        fTransLog.DBHeader = gobjLahDBHeader
        fTransLog.MQQueuesHeader = gobjLahMQQueHeader
        fTransLog.POSHeader = gobjLahPOSHeader
        fTransLog.Show()
    End Sub

    Private Sub mnuSearchCustAssur_Click(sender As Object, e As EventArgs) Handles mnuSearchCustAssur.Click
        Dim w As New frmSearchCustAsur
        If Not OpenWindow(w, Me) Then
            w.Dispose()
        End If
    End Sub


    Private Sub mnuTransactionLogPri_Click(sender As Object, e As EventArgs) Handles mnuTransactionLogPri.Click
        Dim fTransLog As New POSMain.frmTransLog

        fTransLog.DBHeader = gobjBmuDBHeader
        fTransLog.MQQueuesHeader = gobjBmuMQQueHeader
        fTransLog.POSHeader = gobjBmuPOSHeader
        fTransLog.Show()
    End Sub

    Private Sub PrepareWebClientAsync()
        ' Since the first new WebClient takes a long time, async new it in advance
        Threading.ThreadPool.QueueUserWorkItem(
            Sub()
                Dim SysEventLog As New SysEventLog.clsEventLog
                Dim mainStartTime As Date = Now
                ' Just new POSWS client, do thing
                POSWS_HK()
                POSWS_MCU()
                SysEventLog.WritePerLog(gsUser, "cs2005.frmCS2005_Asur", "PrepareWebClientAsync", "New POSWS", mainStartTime, Now, "", "", "")
            End Sub
        )
    End Sub

    Private Sub mnuCheckSMS_Click(sender As Object, e As EventArgs) Handles mnuCheckSMS.Click
        Try
            Dim w As New frmCheckSMS
            If Not OpenWindow(w, Me) Then
                w.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

