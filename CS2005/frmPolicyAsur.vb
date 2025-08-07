'********************************************************************
' Admended By: Flora Leung
' Admended Function: getLifeAsiaInfo
' Added Function: GetEnquiryBO
' Date: 12 Jan 2012
' Project: Project Leo Goal 3
'*********************************************************************
' Admended By: Flora Leung
' Admended Function: EnvSetup
' Date: 14 Feb 2012
' Project: Project Leo Goal 3 Capsil
'********************************************************************
' Admended By: Kay Tsang KT20161111 tabTapNGo
' Admended Function: Tap & Go tab
' Date: 2016-11-11
' Project: Tap & Go
'********************************************************************
' Admended By: Steven Liu SL20191121
' Admended Function: All alert message for QDAP refund extra payment
' Date: 2019-11-21
' Project: ITSR1356 QDAP followup
'********************************************************************
' Admended By: Chopard Chan
' Admended Function: Added new ARW UI control and new ARW His tab 
' Date: 2020-09-16
' Project: ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
'********************************************************************
' Admended By: Sam Leung SL20210625
' Admended Function: Add fields for MediSaver Claim Paid Termination
' Date: 2021-06-25
' Project: ITSR-2408  MediSaver Claim Paid Termination
'********************************************************************
' Admended By: Claudia Lai CL20220928
' Admended Function: Add fields for CI Claim Paid Termination
' Date: 2022-09-28
' Project: ITSR-3488 EasyCover/MyCover Claim Paid Termination
'********************************************************************
' Admended By: Oliver Ou
' Admended Function: 
' Date: 2023-12-15
' Project: Switch Over Code from Assurance to Bermuda 
'********************************************************************
' Amend By:     Chrysan Cheng
' Date:         12 Nov 2024
' Changes:      CRS performer slowness - Policy Summary
'********************************************************************
' Amend By:     Chrysan Cheng
' Date:         17 Feb 2025
' Changes:      CRS performance 2 - PaymentHistory and MinorClaimsHistory
'********************************************************************

Imports INGLife.Interface
Imports System.Threading
Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Http

Public Class frmPolicyAsur
    Inherits System.Windows.Forms.Form

    Public blnEditHICL As Boolean = False
    Friend WithEvents lblEPolicy As System.Windows.Forms.Label
    'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
    Friend WithEvents lblNBMPolicy As System.Windows.Forms.Label
    Friend WithEvents txtEPolicy As System.Windows.Forms.TextBox
    Friend WithEvents lblAnnualRenewalPrem As System.Windows.Forms.Label
    Friend WithEvents txtAnnualRenewalPrem As System.Windows.Forms.TextBox
    Friend WithEvents tabAsurPolicySummary As System.Windows.Forms.TabPage
    Friend WithEvents tabAsurServiceLog As System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents UclServiceLog2 As CS2005.uclServiceLog_Asur
    Friend WithEvents UclServiceLog2 As CS2005.uclServiceLog_Asur_WorkAround

    Friend WithEvents UclPolicySummary_Asur1 As CS2005.uclPolicySummary_Asur
    Friend WithEvents tabPayOutHist As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_CashDividendPayoutHist1 As POSCommCtrl.Ctrl_CashDividendPayoutHist
    Friend WithEvents notificationworker As System.ComponentModel.BackgroundWorker

    Dim CustomerBL As New CustomerBL
    Public CompanyID As String = "ING"

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.

        InitializeComponent()

        'dtPriv = objCS.GetPrivRS(0, gUPSystem, lngErr, strErr)
        'If lngErr <> 0 Or dtPriv.Rows.Count = 0 Then
        '    MsgBox("Error retrieving authority records")
        'End If

        'Add any initialization after the InitializeComponent() call

        ' Move initialization here to fix designer crash
        Me.SMSiLAS.PolicyAccountID = PolicyAccountID
        Me.SMSiLAS.CustomerID = CustomerID

        Me.tcPolicy.Controls.Clear()
        If CheckUPSAccess("Policy Summary") Then Me.tcPolicy.Controls.Add(Me.tabPolicySummary)
        If CheckUPSAccess("Coverage Details") Then Me.tcPolicy.Controls.Add(Me.tabCoverageDetails)
        'If CheckUPSAccess("Default Payout Method Registration") Then Me.tcPolicy.Controls.Add(Me.tabDefaultPayoutMethodRegist)
        Me.tcPolicy.Controls.Add(Me.tabDefaultPayoutMethodRegist)
        If CheckUPSAccess("Unit Tx History") Then tcPolicy.Controls.Add(Me.tabUTRH)
        'If CheckUPSAccess("Unit Tx Summary") Then tcPolicy.Controls.Add(Me.tabUTRS)
        tcPolicy.Controls.Add(Me.tabUTRS)
        If CheckUPSAccess("Unit Tx History") And g_Comp <> "HKL" Then tcPolicy.Controls.Add(Me.tabFundTrans) ' ES005 begin
        'SP ILAS
        Me.tcPolicy.Controls.Add(Me.tabPayOutHist)

        If CheckUPSAccess("Policy Alternation") Then tcPolicy.Controls.Add(Me.tabPolicyAlt)
        If CheckUPSAccess("Policy Alternation") And (strCompany = "LAC" Or strCompany = "LAH") Then tcPolicy.Controls.Add(Me.tabIWSHistory)
        If CheckUPSAccess("DDAR/CCDR") Then Me.tcPolicy.Controls.Add(Me.tabDDACCD)
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabPaymentHistory)

        'Levy
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabLevyHistory)
        ' ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabARWHistory)

        'LH1507004  Journey Annuity Day 2 Phase 2 Start
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabAnnuityPaymentHistory)
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabDirectCreditHistory)
        If CheckUPSAccess("Payment History") Then Me.tcPolicy.Controls.Add(Me.tabCupGetTransResult)
        'LH1507004  Journey Annuity Day 2 Phase 2 End
        ' Start Policy Notes
        'If CheckUPSAccess("Customer History") Then Me.tcPolicy.Controls.Add(Me.tabCustomerHistory)
        If CheckUPSAccess("Customer History") Then
            'Me.tcPolicy.Controls.Add(Me.tabCustomerHistory)
            Me.tcPolicy.Controls.Add(Me.tabPolicyNotes)
        End If

        'oliver 2024-3-5 added for ITSR-5105 EasyTake Service Phase 2
        Me.tcPolicy.Controls.Add(Me.tabDesignatedPerson)

        ' End Policy Notes
        If CheckUPSAccess("Service Log") Then
            Me.tcPolicy.Controls.Add(Me.tabServiceLog)
            Me.tcPolicy.Controls.Add(Me.tabPostSalesCall)
        End If

        If CheckUPSAccess("Agent Info") Then Me.tcPolicy.Controls.Add(Me.tabAgentInfo)
        'If CheckUPSAccess("Underwriting") Then Me.tcPolicy.Controls.Add(Me.tabUnderwriting)
        If CheckUPSAccessFunc("[Tab]Underwriting") Then Me.tcPolicy.Controls.Add(Me.tabUnderwriting)

        ' ES007 begin
        If CheckUPSAccess("Financial Info") Then
            Me.tcPolicy.Controls.Add(Me.tabFinancialInfo)
            Me.tcPolicy.Controls.Add(Me.tabSubAcc)
        End If
        ' ES007 end

        If CheckUPSAccess("Transaction History") Then tcPolicy.Controls.Add(Me.tabTransactionHistory)

        If CheckUPSAccess("DCAR") Then Me.tcPolicy.Controls.Add(Me.tabDCAR)
        If CheckUPSAccess("APL History") Then Me.tcPolicy.Controls.Add(Me.tabAPLHistory)
        If CheckUPSAccess("Coupon History") Then Me.tcPolicy.Controls.Add(Me.tabCouponHistory)
        If CheckUPSAccess("No Claim Discount") Then tcPolicy.Controls.Add(Me.tabDISC)

        If CheckUPSAccess("Claims History") Then Me.tcPolicy.Controls.Add(Me.tabClaimsHistory)
        ' Marjo Claim History
        If CheckUPSAccess("Major Claims") Then Me.tcPolicy.Controls.Add(Me.tabMClaimsHistory)
        If CheckUPSAccess("SMS") Then Me.tcPolicy.Controls.Add(Me.tabSMS)
        If CheckUPSAccess("Edit HICL") Then blnEditHICL = True

        'PL20120427 - START
        If CheckUPSAccess("Policy Summary") Then Me.tcPolicy.Controls.Add(Me.tabParSur)
        'PL20120427 - END

        'ILAS SMS Steven Liu 2015-03-10
        If CheckUPSAccess("SMS iLAS") Then Me.tcPolicy.Controls.Add(Me.tabiLASSMS)
        'ILAS SMS End

        'IUL'
        'If (strCompany = "BMU") Then Me.tcPolicy.Controls.Add(Me.tabIULControl)
        'IUL End'
        Me.tcPolicy.Controls.Add(Me.tabTraditionalPartialSurrQuot)
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tcPolicy As System.Windows.Forms.TabControl
    Friend WithEvents tabPolicySummary As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabTapNGo As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabOePay As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage 'KT20180726
    Friend WithEvents tabCustomerHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabClaimsHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabServiceLog As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabUnderwriting As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabCoverageDetails As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabDefaultPayoutMethodRegist As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabTransactionHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabFinancialInfo As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabPaymentHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Friend WithEvents tabAnnuityPaymentHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabDirectCreditHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabCupGetTransResult As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    'LH1507004  Journey Annuity Day 2 Phase 2 End
    Friend WithEvents tabAgentInfo As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabDDACCD As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    'oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents PolicySummary1 As CS2005.PolicySummary = New CS2005.PolicySummary()
    Friend WithEvents AddressSelect1 As CS2005.AddressSelect = New CS2005.AddressSelect
    Friend WithEvents Coverage1 As CS2005.Coverage = New CS2005.Coverage
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
    Friend WithEvents ClaimHist1 As CS2005.ClaimHist_Asur
    Friend WithEvents tabSMS As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents Sms1 As CS2005.uclSMS = New CS2005.uclSMS
    Friend WithEvents Sms1 As CS2005.uclSMS_Asur = New CS2005.uclSMS_Asur
    'Friend WithEvents AgentInfo1 As CS2005.AgentInfo = New CS2005.AgentInfo
    Friend WithEvents AgentInfo1 As CS2005.AgentInfo_Asur = New CS2005.AgentInfo_Asur

    Friend WithEvents CustHist1 As CS2005.CustHist = New CS2005.CustHist
    Friend WithEvents Ddaccdr1 As CS2005.DDACCDR = New CS2005.DDACCDR
    Friend WithEvents UwInfo1 As CS2005.UWInfo = New CS2005.UWInfo
    Friend WithEvents UwInfoAsur As CS2005.uclUWInfo_Asur = New CS2005.uclUWInfo_Asur
    Friend WithEvents tabCouponHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Couh1 As CS2005.COUH = New CS2005.COUH
    Friend WithEvents FinancialInfo1 As CS2005.FinancialInfo = New CS2005.FinancialInfo

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents UclServiceLog1 As CS2005.uclServiceLog = New CS2005.uclServiceLog
    Friend WithEvents UclServiceLog1 As CS2005.uclServiceLog_Asur = New CS2005.uclServiceLog_Asur

    Friend WithEvents tabAPLHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents AplHist1 As CS2005.APLHist = New CS2005.APLHist
    Friend WithEvents tabCashFlow As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents UclCashFlow1 As CS2005.uclCashFlow = New CS2005.uclCashFlow
    Friend WithEvents lblAPL As System.Windows.Forms.Label
    Friend WithEvents lblLastAPL As System.Windows.Forms.Label ' Project Leo G3
    Friend WithEvents tabDCAR As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Dcar1 As CS2005.DCAR = New CS2005.DCAR
    Friend WithEvents txtCName As System.Windows.Forms.TextBox
    Friend WithEvents txtCNameChi As System.Windows.Forms.TextBox
    'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
    'Friend WithEvents TrxHist1 As CS2005.TrxHist = New CS2005.TrxHist
    Friend WithEvents tabDISC As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents Disc1 As CS2005.DISC = New CS2005.DISC
    Friend WithEvents Disc1 As CS2005.DISC_Asur = New CS2005.DISC_Asur

    Friend WithEvents tabUTRH As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents tabUTRS As System.Windows.Forms.TabPage
    Friend WithEvents UclUTRH1 As CS2005.uclUTRH = New CS2005.uclUTRH
    Friend WithEvents tabPolicyAlt As System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents UclPolicyAlt1 As CS2005.uclPolicyAlt = New CS2005.uclPolicyAlt
    Friend WithEvents UclPolicyAlt1 As CS2005.uclPolicyAlt_Asur = New CS2005.uclPolicyAlt_Asur

    Friend WithEvents PolicyAltPending1 As CS2005.PolicyAltPending = New CS2005.PolicyAltPending
    Friend WithEvents cmdReturn As System.Windows.Forms.Button
    Friend WithEvents cmdPending As System.Windows.Forms.Button
    Friend WithEvents tabMClaimsHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_DirectDebitEnq1 As CRS_Ctrl.ctrl_DirectDebitEnq = New CRS_Ctrl.ctrl_DirectDebitEnq
    Friend WithEvents Ctrl_CRSPolicyGeneral_Information1 As CRS_Ctrl.ctrl_CRSPolicyGeneral_Information = New CRS_Ctrl.ctrl_CRSPolicyGeneral_Information
    Friend WithEvents Ctrl_TapNGo1 As CRS_Ctrl.ctrl_TapNGo = New CRS_Ctrl.ctrl_TapNGo
    Friend WithEvents Ctrl_OePay1 As CRS_Ctrl.ctrl_OePay = New CRS_Ctrl.ctrl_OePay 'KT20170726
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents Ctrl_TranHist1 As CRS_Ctrl.ctrl_TranHist = New CRS_Ctrl.ctrl_TranHist
    Friend WithEvents Ctrl_BillingInf1 As POSCommCtrl.Ctrl_BillingInf = New POSCommCtrl.Ctrl_BillingInf
    Friend WithEvents Ctrl_ChgComponent1 As POSCommCtrl.Ctrl_ChgComponent = New POSCommCtrl.Ctrl_ChgComponent
    Friend WithEvents Ctrl_DefaultPayout1 As ComCtl.DefaultPayout
    Friend WithEvents Ctrl_PaymentHist1 As ComCtl.PaymentHistory = New ComCtl.PaymentHistory
    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Friend WithEvents Ctrl_AnnuityPaymentHist1 As POSCommCtrl.Ctrl_AnnuityPaymentHist = New POSCommCtrl.Ctrl_AnnuityPaymentHist
    Friend WithEvents Ctrl_DirectCreditHist1 As POSCommCtrl.Ctrl_DirectCreditHist = New POSCommCtrl.Ctrl_DirectCreditHist
    Friend WithEvents Ctrl_CupGetTransResult As ComCtl.CupGetTransResult = New ComCtl.CupGetTransResult
    'LH1507004  Journey Annuity Day 2 Phase 2 End
    Friend WithEvents Ctrl_FundHolding1 As CRS_Ctrl.ctrl_FundHolding = New CRS_Ctrl.ctrl_FundHolding
    Friend WithEvents Ctrl_FundTranSummary1 As CRS_Ctrl.ctrl_FundTranSummary = New CRS_Ctrl.ctrl_FundTranSummary
    Friend WithEvents CashFlow1 As ComCtl.CashFlow = New ComCtl.CashFlow
    Friend WithEvents Ctrl_Sel_CO1 As POSCommCtrl.Ctrl_Sel_CO = New POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tabFundTrans As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents Ctrl_FundTranHist1 As CRS_Ctrl.ctrl_FundTranHist = New CRS_Ctrl.ctrl_FundTranHist
    Friend WithEvents tabPolicyNotes As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents PolicyNote1 As ComCtl.PolicyNote = New ComCtl.PolicyNote
    'oliver 2024-3-5 added for ITSR-5105 EasyTake Service Phase 2
    Friend WithEvents tabDesignatedPerson As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_DesignatedPerson As ComCtl.DesignatedPersonMaintenance
    Friend WithEvents tabSubAcc As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_FinancialDtl1 As CRS_Ctrl.ctrl_FinancialDtl = New CRS_Ctrl.ctrl_FinancialDtl
    Friend WithEvents Ctrl_FinancialInfo1 As CRS_Ctrl.ctrl_FinancialInfo = New CRS_Ctrl.ctrl_FinancialInfo
    Friend WithEvents LoanHist1 As POSCommCtrl.LoanHist = New POSCommCtrl.LoanHist
    Friend WithEvents lblReminderMsg1 As System.Windows.Forms.Label
    Friend WithEvents lblReminderMsg2 As System.Windows.Forms.Label
    Friend WithEvents CouponHist1 As POSCommCtrl.CouponHist = New POSCommCtrl.CouponHist
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents PolicyValue1 As POSCommCtrl.PolicyValue = New POSCommCtrl.PolicyValue
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tabParSur As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents UclParSur1 As CS2005.uclParSur = New CS2005.uclParSur
    Friend WithEvents UclParSur1 As CS2005.uclParSur_Asur = New CS2005.uclParSur_Asur

    Friend WithEvents lblDateFormatMsg As System.Windows.Forms.Label
    Friend WithEvents lblTAD2CPolicyMsg As System.Windows.Forms.Label
    Friend WithEvents tabiLASSMS As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents SMSiLAS As CS2005.SMSiLAS = New CS2005.SMSiLAS
    Friend WithEvents cboNCVReminderFlag As System.Windows.Forms.ComboBox
    Friend WithEvents lblNCVReminderFlag As System.Windows.Forms.Label
    Friend WithEvents lblNCVUpdateDate As System.Windows.Forms.Label
    Friend WithEvents txtNCVUpdateDate As System.Windows.Forms.TextBox
    Friend WithEvents lblNCVUpdateUser As System.Windows.Forms.Label
    Friend WithEvents txtNCVUpdateUser As System.Windows.Forms.TextBox
    Friend WithEvents tabPostSalesCall As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents UclPostSalesCallQuestionnaire1 As CS2005.uclPostSalesCallQuestionnaire = New CS2005.uclPostSalesCallQuestionnaire
    Friend WithEvents UclPostSalesCallQuestionnaire1 As CS2005.uclPostSalesCallQuestionnaire_Asur = New CS2005.uclPostSalesCallQuestionnaire_Asur

    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tabLevyHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_LevyHistory As CRS_Ctrl.ctrl_LevyHistory = New CRS_Ctrl.ctrl_LevyHistory
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtEstatement As System.Windows.Forms.TextBox

    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Friend WithEvents MClaimHist1 As CS2005.MClaimHist = New CS2005.MClaimHist
    Friend WithEvents MClaimHist1 As CS2005.MClaimHist_Asur = New CS2005.MClaimHist_Asur

    Friend WithEvents lblCapsilPolNo As System.Windows.Forms.Label 'ITSR933 FG R3 Policy Number Change
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents tabARWHistory As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents Ctrl_AutoRegularWithdrawalHis As CRS_Ctrl.ctrl_AutoRegularWithdrawalHis = New CRS_Ctrl.ctrl_AutoRegularWithdrawalHis 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents Ctrl_AutoRegularWithdrawal As CRS_Ctrl.ctrl_AutoRegularWithdrawal 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents lblCompanyIdBanner As System.Windows.Forms.Label = New System.Windows.Forms.Label
    Friend WithEvents tabIWSHistory As System.Windows.Forms.TabPage
    'oliver 2023-12-11 commented for Switch Over Code from Assurance to Bermuda
    Friend WithEvents Ctrl_IWSHistoryEnquiry1 As POSCommCtrl.Ctrl_IWSHistoryEnquiry
    Friend WithEvents tabTraditionalPartialSurrQuot As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_Sel_CO2 As POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents Ctrl_POS_ParSurTradition1 As POSCommCtrl.Ctrl_POS_ParSurTradition
    'IUL
    Friend WithEvents tabIULControl As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_POS_iULMaint1 As POSCommCtrl.Ctrl_POS_iULMaint = New POSCommCtrl.Ctrl_POS_iULMaint
    '
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPolicyAsur))
        Dim PolicyNoteDefaultAdaptor7 As ComCtl.PolicyNoteDefaultAdaptor = New ComCtl.PolicyNoteDefaultAdaptor()
        Me.tcPolicy = New System.Windows.Forms.TabControl()
        Me.tabPolicySummary = New System.Windows.Forms.TabPage()
        Me.lblNCVUpdateDate = New System.Windows.Forms.Label()
        Me.txtNCVUpdateDate = New System.Windows.Forms.TextBox()
        Me.lblNCVUpdateUser = New System.Windows.Forms.Label()
        Me.txtNCVUpdateUser = New System.Windows.Forms.TextBox()
        Me.cboNCVReminderFlag = New System.Windows.Forms.ComboBox()
        Me.lblNCVReminderFlag = New System.Windows.Forms.Label()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        Me.AddressSelect1 = New CS2005.AddressSelect()
        Me.tabUTRH = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundHolding1 = New CRS_Ctrl.ctrl_FundHolding()
        Me.UclUTRH1 = New CS2005.uclUTRH()
        Me.tabUTRS = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundTranSummary1 = New CRS_Ctrl.ctrl_FundTranSummary()
        Me.tabCoverageDetails = New System.Windows.Forms.TabPage()
        Me.Ctrl_ChgComponent1 = New POSCommCtrl.Ctrl_ChgComponent()
        Me.Coverage1 = New CS2005.Coverage()
        Me.tabDefaultPayoutMethodRegist = New System.Windows.Forms.TabPage()
        Me.Ctrl_DefaultPayout1 = New ComCtl.DefaultPayout()
        Me.tabFinancialInfo = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PolicyValue1 = New POSCommCtrl.PolicyValue()
        Me.FinancialInfo1 = New CS2005.FinancialInfo()
        Me.tabMClaimsHistory = New System.Windows.Forms.TabPage()
        Me.MClaimHist1 = New CS2005.MClaimHist_Asur()
        Me.tabDISC = New System.Windows.Forms.TabPage()
        Me.Disc1 = New CS2005.DISC_Asur()
        Me.tabPaymentHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_PaymentHist1 = New ComCtl.PaymentHistory()
        Me.Ctrl_AutoRegularWithdrawal = New CRS_Ctrl.ctrl_AutoRegularWithdrawal()
        Me.tabLevyHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_LevyHistory = New CRS_Ctrl.ctrl_LevyHistory()
        Me.tabARWHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_AutoRegularWithdrawalHis = New CRS_Ctrl.ctrl_AutoRegularWithdrawalHis()
        Me.tabAnnuityPaymentHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_AnnuityPaymentHist1 = New POSCommCtrl.Ctrl_AnnuityPaymentHist()
        Me.tabDirectCreditHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_DirectCreditHist1 = New POSCommCtrl.Ctrl_DirectCreditHist()
        Me.tabCupGetTransResult = New System.Windows.Forms.TabPage()
        Me.Ctrl_CupGetTransResult = New ComCtl.CupGetTransResult()
        Me.tabAPLHistory = New System.Windows.Forms.TabPage()
        Me.LoanHist1 = New POSCommCtrl.LoanHist()
        Me.AplHist1 = New CS2005.APLHist()
        Me.lblReminderMsg1 = New System.Windows.Forms.Label()
        Me.lblReminderMsg2 = New System.Windows.Forms.Label()
        Me.tabDCAR = New System.Windows.Forms.TabPage()
        Me.Dcar1 = New CS2005.DCAR()
        Me.tabUnderwriting = New System.Windows.Forms.TabPage()
        Me.UwInfo1 = New CS2005.UWInfo()
        Me.UwInfoAsur = New CS2005.uclUWInfo_Asur()
        Me.tabSMS = New System.Windows.Forms.TabPage()
        Me.Sms1 = New CS2005.uclSMS_Asur()
        Me.tabPolicyAlt = New System.Windows.Forms.TabPage()
        Me.cmdPending = New System.Windows.Forms.Button()
        Me.cmdReturn = New System.Windows.Forms.Button()
        Me.PolicyAltPending1 = New CS2005.PolicyAltPending()
        Me.UclPolicyAlt1 = New CS2005.uclPolicyAlt_Asur()
        Me.tabCouponHistory = New System.Windows.Forms.TabPage()
        Me.CouponHist1 = New POSCommCtrl.CouponHist()
        Me.Couh1 = New CS2005.COUH()
        Me.tabDDACCD = New System.Windows.Forms.TabPage()
        Me.Ctrl_DirectDebitEnq1 = New CRS_Ctrl.ctrl_DirectDebitEnq()
        Me.Ddaccdr1 = New CS2005.DDACCDR()
        Me.tabCashFlow = New System.Windows.Forms.TabPage()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.Ctrl_Sel_CO1 = New POSCommCtrl.Ctrl_Sel_CO()
        Me.CashFlow1 = New ComCtl.CashFlow()
        Me.UclCashFlow1 = New CS2005.uclCashFlow()
        Me.tabClaimsHistory = New System.Windows.Forms.TabPage()
        Me.ClaimHist1 = New CS2005.ClaimHist_Asur()
        Me.tabTransactionHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_TranHist1 = New CRS_Ctrl.ctrl_TranHist()
        Me.tabCustomerHistory = New System.Windows.Forms.TabPage()
        Me.CustHist1 = New CS2005.CustHist()
        Me.tabServiceLog = New System.Windows.Forms.TabPage()
        Me.UclServiceLog1 = New CS2005.uclServiceLog_Asur()
        Me.tabAgentInfo = New System.Windows.Forms.TabPage()
        Me.tabFundTrans = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundTranHist1 = New CRS_Ctrl.ctrl_FundTranHist()
        Me.tabPayOutHist = New System.Windows.Forms.TabPage()
        Me.Ctrl_CashDividendPayoutHist1 = New POSCommCtrl.Ctrl_CashDividendPayoutHist()
        Me.tabPolicyNotes = New System.Windows.Forms.TabPage()
        Me.PolicyNote1 = New ComCtl.PolicyNote()
        Me.tabDesignatedPerson = New System.Windows.Forms.TabPage()
        Me.Ctrl_DesignatedPerson = New ComCtl.DesignatedPersonMaintenance()
        Me.tabSubAcc = New System.Windows.Forms.TabPage()
        Me.Ctrl_FinancialDtl1 = New CRS_Ctrl.ctrl_FinancialDtl()
        Me.Ctrl_FinancialInfo1 = New CRS_Ctrl.ctrl_FinancialInfo()
        Me.tabParSur = New System.Windows.Forms.TabPage()
        Me.UclParSur1 = New CS2005.uclParSur_Asur()
        Me.tabiLASSMS = New System.Windows.Forms.TabPage()
        Me.SMSiLAS = New CS2005.SMSiLAS()
        Me.tabPostSalesCall = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.UclPostSalesCallQuestionnaire1 = New CS2005.uclPostSalesCallQuestionnaire_Asur()
        Me.tabTapNGo = New System.Windows.Forms.TabPage()
        Me.Ctrl_TapNGo1 = New CRS_Ctrl.ctrl_TapNGo()
        Me.tabOePay = New System.Windows.Forms.TabPage()
        Me.Ctrl_OePay1 = New CRS_Ctrl.ctrl_OePay()
        Me.tabAsurPolicySummary = New System.Windows.Forms.TabPage()
        Me.UclPolicySummary_Asur1 = New CS2005.uclPolicySummary_Asur()
        Me.tabAsurServiceLog = New System.Windows.Forms.TabPage()
        Me.UclServiceLog2 = New CS2005.uclServiceLog_Asur_WorkAround()
        Me.tabIWSHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_IWSHistoryEnquiry1 = New POSCommCtrl.Ctrl_IWSHistoryEnquiry()
        Me.tabTraditionalPartialSurrQuot = New System.Windows.Forms.TabPage()
        Me.Ctrl_Sel_CO2 = New POSCommCtrl.Ctrl_Sel_CO()
        Me.Ctrl_POS_ParSurTradition1 = New POSCommCtrl.Ctrl_POS_ParSurTradition()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tabIULControl = New System.Windows.Forms.TabPage()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtPolicy = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtChiName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblAPL = New System.Windows.Forms.Label()
        Me.lblLastAPL = New System.Windows.Forms.Label()
        Me.txtCName = New System.Windows.Forms.TextBox()
        Me.txtCNameChi = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.lblDateFormatMsg = New System.Windows.Forms.Label()
        Me.lblTAD2CPolicyMsg = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEstatement = New System.Windows.Forms.TextBox()
        Me.lblCapsilPolNo = New System.Windows.Forms.Label()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.lblEPolicy = New System.Windows.Forms.Label()
        Me.lblNBMPolicy = New System.Windows.Forms.Label()
        Me.txtEPolicy = New System.Windows.Forms.TextBox()
        Me.lblAnnualRenewalPrem = New System.Windows.Forms.Label()
        Me.txtAnnualRenewalPrem = New System.Windows.Forms.TextBox()
        Me.notificationworker = New System.ComponentModel.BackgroundWorker()
        Me.lblCompanyIdBanner = New System.Windows.Forms.Label()
        Me.tcPolicy.SuspendLayout()
        Me.tabPolicySummary.SuspendLayout()
        Me.tabUTRH.SuspendLayout()
        Me.tabUTRS.SuspendLayout()
        Me.tabCoverageDetails.SuspendLayout()
        Me.tabDefaultPayoutMethodRegist.SuspendLayout()
        Me.tabFinancialInfo.SuspendLayout()
        Me.tabMClaimsHistory.SuspendLayout()
        Me.tabDISC.SuspendLayout()
        Me.tabPaymentHistory.SuspendLayout()
        Me.tabLevyHistory.SuspendLayout()
        Me.tabARWHistory.SuspendLayout()
        Me.tabAnnuityPaymentHistory.SuspendLayout()
        Me.tabDirectCreditHistory.SuspendLayout()
        Me.tabCupGetTransResult.SuspendLayout()
        Me.tabAPLHistory.SuspendLayout()
        Me.tabDCAR.SuspendLayout()
        Me.tabUnderwriting.SuspendLayout()
        Me.tabSMS.SuspendLayout()
        Me.tabPolicyAlt.SuspendLayout()
        Me.tabCouponHistory.SuspendLayout()
        Me.tabDDACCD.SuspendLayout()
        Me.tabCashFlow.SuspendLayout()
        Me.tabClaimsHistory.SuspendLayout()
        Me.tabTransactionHistory.SuspendLayout()
        Me.tabCustomerHistory.SuspendLayout()
        Me.tabServiceLog.SuspendLayout()
        Me.tabFundTrans.SuspendLayout()
        Me.tabPayOutHist.SuspendLayout()
        Me.tabPolicyNotes.SuspendLayout()
        Me.tabDesignatedPerson.SuspendLayout()
        Me.tabSubAcc.SuspendLayout()
        Me.tabParSur.SuspendLayout()
        Me.tabiLASSMS.SuspendLayout()
        Me.tabPostSalesCall.SuspendLayout()
        Me.tabTapNGo.SuspendLayout()
        Me.tabOePay.SuspendLayout()
        Me.tabAsurPolicySummary.SuspendLayout()
        Me.tabAsurServiceLog.SuspendLayout()
        Me.tabIWSHistory.SuspendLayout()
        Me.tabTraditionalPartialSurrQuot.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcPolicy
        '
        Me.tcPolicy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcPolicy.Controls.Add(Me.tabPolicySummary)
        Me.tcPolicy.Controls.Add(Me.tabUTRH)
        Me.tcPolicy.Controls.Add(Me.tabUTRS)
        Me.tcPolicy.Controls.Add(Me.tabCoverageDetails)
        Me.tcPolicy.Controls.Add(Me.tabDefaultPayoutMethodRegist)
        Me.tcPolicy.Controls.Add(Me.tabFinancialInfo)
        Me.tcPolicy.Controls.Add(Me.tabMClaimsHistory)
        Me.tcPolicy.Controls.Add(Me.tabDISC)
        Me.tcPolicy.Controls.Add(Me.tabPaymentHistory)
        Me.tcPolicy.Controls.Add(Me.tabLevyHistory)
        Me.tcPolicy.Controls.Add(Me.tabARWHistory)
        Me.tcPolicy.Controls.Add(Me.tabAnnuityPaymentHistory)
        Me.tcPolicy.Controls.Add(Me.tabDirectCreditHistory)
        Me.tcPolicy.Controls.Add(Me.tabCupGetTransResult)
        Me.tcPolicy.Controls.Add(Me.tabAPLHistory)
        Me.tcPolicy.Controls.Add(Me.tabDCAR)
        Me.tcPolicy.Controls.Add(Me.tabUnderwriting)
        Me.tcPolicy.Controls.Add(Me.tabSMS)
        Me.tcPolicy.Controls.Add(Me.tabPolicyAlt)
        Me.tcPolicy.Controls.Add(Me.tabCouponHistory)
        Me.tcPolicy.Controls.Add(Me.tabDDACCD)
        Me.tcPolicy.Controls.Add(Me.tabCashFlow)
        Me.tcPolicy.Controls.Add(Me.tabClaimsHistory)
        Me.tcPolicy.Controls.Add(Me.tabTransactionHistory)
        Me.tcPolicy.Controls.Add(Me.tabCustomerHistory)
        Me.tcPolicy.Controls.Add(Me.tabServiceLog)
        Me.tcPolicy.Controls.Add(Me.tabAgentInfo)
        Me.tcPolicy.Controls.Add(Me.tabFundTrans)
        Me.tcPolicy.Controls.Add(Me.tabPayOutHist)
        Me.tcPolicy.Controls.Add(Me.tabPolicyNotes)
        Me.tcPolicy.Controls.Add(Me.tabDesignatedPerson)
        Me.tcPolicy.Controls.Add(Me.tabSubAcc)
        Me.tcPolicy.Controls.Add(Me.tabParSur)
        Me.tcPolicy.Controls.Add(Me.tabiLASSMS)
        Me.tcPolicy.Controls.Add(Me.tabPostSalesCall)
        Me.tcPolicy.Controls.Add(Me.tabTapNGo)
        Me.tcPolicy.Controls.Add(Me.tabOePay)
        Me.tcPolicy.Controls.Add(Me.tabAsurPolicySummary)
        Me.tcPolicy.Controls.Add(Me.tabAsurServiceLog)
        Me.tcPolicy.Controls.Add(Me.tabIWSHistory)
        Me.tcPolicy.Controls.Add(Me.tabTraditionalPartialSurrQuot)
        Me.tcPolicy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcPolicy.HotTrack = True
        Me.tcPolicy.ImageList = Me.ImageList1
        Me.tcPolicy.Location = New System.Drawing.Point(18, 73)
        Me.tcPolicy.Multiline = True
        Me.tcPolicy.Name = "tcPolicy"
        Me.tcPolicy.SelectedIndex = 0
        Me.tcPolicy.Size = New System.Drawing.Size(2116, 1570)
        Me.tcPolicy.TabIndex = 1
        Me.tcPolicy.TabStop = False
        Me.tcPolicy.Tag = "Policy Information"
        '
        'tabPolicySummary
        '
        Me.tabPolicySummary.BackColor = System.Drawing.Color.Transparent
        Me.tabPolicySummary.Controls.Add(Me.lblNCVUpdateDate)
        Me.tabPolicySummary.Controls.Add(Me.txtNCVUpdateDate)
        Me.tabPolicySummary.Controls.Add(Me.lblNCVUpdateUser)
        Me.tabPolicySummary.Controls.Add(Me.txtNCVUpdateUser)
        Me.tabPolicySummary.Controls.Add(Me.cboNCVReminderFlag)
        Me.tabPolicySummary.Controls.Add(Me.lblNCVReminderFlag)
        Me.tabPolicySummary.Controls.Add(Me.lblPolicyNo)
        Me.tabPolicySummary.Controls.Add(Me.AddressSelect1)
        Me.tabPolicySummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPolicySummary.ForeColor = System.Drawing.Color.Black
        Me.tabPolicySummary.ImageIndex = 3
        Me.tabPolicySummary.Location = New System.Drawing.Point(4, 104)
        Me.tabPolicySummary.Name = "tabPolicySummary"
        Me.tabPolicySummary.Size = New System.Drawing.Size(2108, 1462)
        Me.tabPolicySummary.TabIndex = 0
        Me.tabPolicySummary.Tag = "Policy Summary"
        Me.tabPolicySummary.Text = "Policy Summary"
        Me.tabPolicySummary.UseVisualStyleBackColor = True
        '
        'lblNCVUpdateDate
        '
        Me.lblNCVUpdateDate.AutoSize = True
        Me.lblNCVUpdateDate.Location = New System.Drawing.Point(1515, 618)
        Me.lblNCVUpdateDate.Name = "lblNCVUpdateDate"
        Me.lblNCVUpdateDate.Size = New System.Drawing.Size(165, 20)
        Me.lblNCVUpdateDate.TabIndex = 132
        Me.lblNCVUpdateDate.Text = "Latest Update Date: "
        '
        'txtNCVUpdateDate
        '
        Me.txtNCVUpdateDate.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNCVUpdateDate.Location = New System.Drawing.Point(524, 181)
        Me.txtNCVUpdateDate.MaxLength = 10
        Me.txtNCVUpdateDate.Name = "txtNCVUpdateDate"
        Me.txtNCVUpdateDate.ReadOnly = True
        Me.txtNCVUpdateDate.Size = New System.Drawing.Size(71, 26)
        Me.txtNCVUpdateDate.TabIndex = 131
        '
        'lblNCVUpdateUser
        '
        Me.lblNCVUpdateUser.AutoSize = True
        Me.lblNCVUpdateUser.Location = New System.Drawing.Point(1023, 618)
        Me.lblNCVUpdateUser.Name = "lblNCVUpdateUser"
        Me.lblNCVUpdateUser.Size = New System.Drawing.Size(146, 20)
        Me.lblNCVUpdateUser.TabIndex = 130
        Me.lblNCVUpdateUser.Text = "Last Update User:"
        '
        'txtNCVUpdateUser
        '
        Me.txtNCVUpdateUser.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNCVUpdateUser.Location = New System.Drawing.Point(369, 181)
        Me.txtNCVUpdateUser.MaxLength = 10
        Me.txtNCVUpdateUser.Name = "txtNCVUpdateUser"
        Me.txtNCVUpdateUser.ReadOnly = True
        Me.txtNCVUpdateUser.Size = New System.Drawing.Size(71, 26)
        Me.txtNCVUpdateUser.TabIndex = 129
        '
        'cboNCVReminderFlag
        '
        Me.cboNCVReminderFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNCVReminderFlag.Enabled = False
        Me.cboNCVReminderFlag.FormattingEnabled = True
        Me.cboNCVReminderFlag.Location = New System.Drawing.Point(141, 181)
        Me.cboNCVReminderFlag.Name = "cboNCVReminderFlag"
        Me.cboNCVReminderFlag.Size = New System.Drawing.Size(157, 28)
        Me.cboNCVReminderFlag.TabIndex = 128
        '
        'lblNCVReminderFlag
        '
        Me.lblNCVReminderFlag.AutoSize = True
        Me.lblNCVReminderFlag.Location = New System.Drawing.Point(36, 618)
        Me.lblNCVReminderFlag.Name = "lblNCVReminderFlag"
        Me.lblNCVReminderFlag.Size = New System.Drawing.Size(284, 20)
        Me.lblNCVReminderFlag.TabIndex = 127
        Me.lblNCVReminderFlag.Text = "Negative Cash Value Reminder Flag:"
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(132, 75)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(97, 20)
        Me.lblPolicyNo.TabIndex = 5
        Me.lblPolicyNo.Text = "StrPolicyNo"
        '
        'AddressSelect1
        '
        Me.AddressSelect1.Location = New System.Drawing.Point(8, 199)
        Me.AddressSelect1.Name = "AddressSelect1"
        Me.AddressSelect1.Size = New System.Drawing.Size(512, 219)
        Me.AddressSelect1.TabIndex = 4
        '
        'tabUTRH
        '
        Me.tabUTRH.AutoScroll = True
        Me.tabUTRH.Controls.Add(Me.Ctrl_FundHolding1)
        Me.tabUTRH.Controls.Add(Me.UclUTRH1)
        Me.tabUTRH.Location = New System.Drawing.Point(4, 54)
        Me.tabUTRH.Name = "tabUTRH"
        Me.tabUTRH.Size = New System.Drawing.Size(192, 42)
        Me.tabUTRH.TabIndex = 20
        Me.tabUTRH.Tag = "Unit Tx History"
        Me.tabUTRH.Text = "Unit Tx History"
        Me.tabUTRH.UseVisualStyleBackColor = True
        '
        'Ctrl_FundHolding1
        '
        Me.Ctrl_FundHolding1.Location = New System.Drawing.Point(5, 4)
        Me.Ctrl_FundHolding1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FundHolding1.Name = "Ctrl_FundHolding1"
        Me.Ctrl_FundHolding1.PolicyinUse = Nothing
        Me.Ctrl_FundHolding1.Size = New System.Drawing.Size(1686, 886)
        Me.Ctrl_FundHolding1.TabIndex = 1
        '
        'UclUTRH1
        '
        Me.UclUTRH1.Location = New System.Drawing.Point(0, 0)
        Me.UclUTRH1.Name = "UclUTRH1"
        Me.UclUTRH1.Size = New System.Drawing.Size(1184, 731)
        Me.UclUTRH1.TabIndex = 0
        '
        'tabUTRS
        '
        Me.tabUTRS.Controls.Add(Me.Ctrl_FundTranSummary1)
        Me.tabUTRS.Location = New System.Drawing.Point(4, 79)
        Me.tabUTRS.Name = "tabUTRS"
        Me.tabUTRS.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUTRS.Size = New System.Drawing.Size(192, 17)
        Me.tabUTRS.TabIndex = 30
        Me.tabUTRS.Tag = "Unit Tx Summary"
        Me.tabUTRS.Text = "Unit Tx Summary"
        Me.tabUTRS.UseVisualStyleBackColor = True
        '
        'Ctrl_FundTranSummary1
        '
        Me.Ctrl_FundTranSummary1.AutoSize = True
        Me.Ctrl_FundTranSummary1.BasicInsured = Nothing
        Me.Ctrl_FundTranSummary1.Location = New System.Drawing.Point(5, 4)
        Me.Ctrl_FundTranSummary1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_FundTranSummary1.Name = "Ctrl_FundTranSummary1"
        Me.Ctrl_FundTranSummary1.PolicyinUse = Nothing
        Me.Ctrl_FundTranSummary1.Size = New System.Drawing.Size(2749, 1102)
        Me.Ctrl_FundTranSummary1.TabIndex = 0
        '
        'tabCoverageDetails
        '
        Me.tabCoverageDetails.AutoScroll = True
        Me.tabCoverageDetails.Controls.Add(Me.Ctrl_ChgComponent1)
        Me.tabCoverageDetails.Controls.Add(Me.Coverage1)
        Me.tabCoverageDetails.ImageIndex = 3
        Me.tabCoverageDetails.Location = New System.Drawing.Point(4, 104)
        Me.tabCoverageDetails.Name = "tabCoverageDetails"
        Me.tabCoverageDetails.Size = New System.Drawing.Size(2108, 1462)
        Me.tabCoverageDetails.TabIndex = 1
        Me.tabCoverageDetails.Tag = "Coverage Details"
        Me.tabCoverageDetails.Text = "Coverage Details"
        Me.tabCoverageDetails.UseVisualStyleBackColor = True
        Me.tabCoverageDetails.Visible = False
        '
        'Ctrl_ChgComponent1
        '
        Me.Ctrl_ChgComponent1.BillTypeInUse = ""
        Me.Ctrl_ChgComponent1.CovNoInuse = ""
        Me.Ctrl_ChgComponent1.CurrdsInUse = Nothing
        Me.Ctrl_ChgComponent1.EffDateInUse = New Date(CType(0, Long))
        Me.Ctrl_ChgComponent1.LifeNoInUse = ""
        Me.Ctrl_ChgComponent1.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_ChgComponent1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_ChgComponent1.ModeInUse = Utility.Utility.ModeName.Blank
        Me.Ctrl_ChgComponent1.Name = "Ctrl_ChgComponent1"
        Me.Ctrl_ChgComponent1.PModeInUse = ""
        Me.Ctrl_ChgComponent1.PolicyNoInuse = ""
        Me.Ctrl_ChgComponent1.RiderInuse = ""
        Me.Ctrl_ChgComponent1.setFundHolding = False
        Me.Ctrl_ChgComponent1.Size = New System.Drawing.Size(1547, 792)
        Me.Ctrl_ChgComponent1.SystemInUse = Nothing
        Me.Ctrl_ChgComponent1.TabIndex = 0
        '
        'Coverage1
        '
        Me.Coverage1.Location = New System.Drawing.Point(0, 0)
        Me.Coverage1.Name = "Coverage1"
        Me.Coverage1.PolicyAccountID = Nothing
        Me.Coverage1.Size = New System.Drawing.Size(1165, 1052)
        Me.Coverage1.TabIndex = 0
        '
        'tabDefaultPayoutMethodRegist
        '
        Me.tabDefaultPayoutMethodRegist.AutoScroll = True
        Me.tabDefaultPayoutMethodRegist.Controls.Add(Me.Ctrl_DefaultPayout1)
        Me.tabDefaultPayoutMethodRegist.Location = New System.Drawing.Point(4, 129)
        Me.tabDefaultPayoutMethodRegist.Name = "tabDefaultPayoutMethodRegist"
        Me.tabDefaultPayoutMethodRegist.Size = New System.Drawing.Size(192, 0)
        Me.tabDefaultPayoutMethodRegist.TabIndex = 1
        Me.tabDefaultPayoutMethodRegist.Tag = "Default Payout Method Registration"
        Me.tabDefaultPayoutMethodRegist.Text = "Default Payout Method Registration"
        Me.tabDefaultPayoutMethodRegist.UseVisualStyleBackColor = True
        Me.tabDefaultPayoutMethodRegist.Visible = False
        '
        'Ctrl_DefaultPayout1
        '
        Me.Ctrl_DefaultPayout1.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_DefaultPayout1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_DefaultPayout1.Name = "Ctrl_DefaultPayout1"
        Me.Ctrl_DefaultPayout1.PolicyInUse = ""
        Me.Ctrl_DefaultPayout1.PolicyNoInuse = ""
        Me.Ctrl_DefaultPayout1.Size = New System.Drawing.Size(1707, 792)
        Me.Ctrl_DefaultPayout1.TabIndex = 0
        Me.Ctrl_DefaultPayout1.TabPayoutIndexInUse = "0"
        '
        'tabFinancialInfo
        '
        Me.tabFinancialInfo.AutoScroll = True
        Me.tabFinancialInfo.Controls.Add(Me.Label6)
        Me.tabFinancialInfo.Controls.Add(Me.Button1)
        Me.tabFinancialInfo.Controls.Add(Me.DateTimePicker1)
        Me.tabFinancialInfo.Controls.Add(Me.PolicyValue1)
        Me.tabFinancialInfo.Controls.Add(Me.FinancialInfo1)
        Me.tabFinancialInfo.Location = New System.Drawing.Point(4, 154)
        Me.tabFinancialInfo.Name = "tabFinancialInfo"
        Me.tabFinancialInfo.Size = New System.Drawing.Size(192, 0)
        Me.tabFinancialInfo.TabIndex = 9
        Me.tabFinancialInfo.Tag = "Financial Info"
        Me.tabFinancialInfo.Text = "Financial Information"
        Me.tabFinancialInfo.UseVisualStyleBackColor = True
        Me.tabFinancialInfo.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 20)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Effective Date"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(325, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 29)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Go"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(149, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(166, 26)
        Me.DateTimePicker1.TabIndex = 2
        '
        'PolicyValue1
        '
        Me.PolicyValue1.Location = New System.Drawing.Point(5, 50)
        Me.PolicyValue1.Margin = New System.Windows.Forms.Padding(4)
        Me.PolicyValue1.Name = "PolicyValue1"
        Me.PolicyValue1.RpuOriginalSI = ""
        Me.PolicyValue1.RpuPaidToDate = ""
        Me.PolicyValue1.RpuQuoteAmount = ""
        Me.PolicyValue1.RpuRefund = ""
        Me.PolicyValue1.Size = New System.Drawing.Size(1613, 590)
        Me.PolicyValue1.TabIndex = 1
        '
        'FinancialInfo1
        '
        Me.FinancialInfo1.Location = New System.Drawing.Point(0, 0)
        Me.FinancialInfo1.Name = "FinancialInfo1"
        Me.FinancialInfo1.Size = New System.Drawing.Size(1158, 538)
        Me.FinancialInfo1.TabIndex = 0
        '
        'tabMClaimsHistory
        '
        Me.tabMClaimsHistory.AutoScroll = True
        Me.tabMClaimsHistory.Controls.Add(Me.MClaimHist1)
        Me.tabMClaimsHistory.Location = New System.Drawing.Point(4, 179)
        Me.tabMClaimsHistory.Name = "tabMClaimsHistory"
        Me.tabMClaimsHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabMClaimsHistory.TabIndex = 22
        Me.tabMClaimsHistory.Text = "Major Claims History"
        Me.tabMClaimsHistory.UseVisualStyleBackColor = True
        '
        'MClaimHist1
        '
        Me.MClaimHist1.Location = New System.Drawing.Point(0, 0)
        Me.MClaimHist1.Name = "MClaimHist1"
        Me.MClaimHist1.PolicyAccountID = Nothing
        Me.MClaimHist1.Size = New System.Drawing.Size(1165, 1076)
        Me.MClaimHist1.TabIndex = 0
        '
        'tabDISC
        '
        Me.tabDISC.Controls.Add(Me.Disc1)
        Me.tabDISC.Location = New System.Drawing.Point(4, 204)
        Me.tabDISC.Name = "tabDISC"
        Me.tabDISC.Size = New System.Drawing.Size(192, 0)
        Me.tabDISC.TabIndex = 19
        Me.tabDISC.Tag = "No Claim Discount"
        Me.tabDISC.Text = "No Claim Discount/Bonus"
        Me.tabDISC.UseVisualStyleBackColor = True
        '
        'Disc1
        '
        Me.Disc1.CompanyID = Nothing
        Me.Disc1.Location = New System.Drawing.Point(0, 0)
        Me.Disc1.Name = "Disc1"
        Me.Disc1.Size = New System.Drawing.Size(1520, 877)
        Me.Disc1.TabIndex = 0
        '
        'tabPaymentHistory
        '
        Me.tabPaymentHistory.AutoScroll = True
        Me.tabPaymentHistory.Controls.Add(Me.Ctrl_PaymentHist1)
        Me.tabPaymentHistory.Controls.Add(Me.Ctrl_AutoRegularWithdrawal)
        Me.tabPaymentHistory.ImageIndex = 3
        Me.tabPaymentHistory.Location = New System.Drawing.Point(4, 229)
        Me.tabPaymentHistory.Name = "tabPaymentHistory"
        Me.tabPaymentHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabPaymentHistory.TabIndex = 4
        Me.tabPaymentHistory.Tag = "Payment History"
        Me.tabPaymentHistory.Text = "Payment History"
        Me.tabPaymentHistory.UseVisualStyleBackColor = True
        Me.tabPaymentHistory.Visible = False
        '
        'Ctrl_PaymentHist1
        '
        Me.Ctrl_PaymentHist1.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_PaymentHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_PaymentHist1.Name = "Ctrl_PaymentHist1"
        Me.Ctrl_PaymentHist1.PolicyNoInUse = ""
        Me.Ctrl_PaymentHist1.Size = New System.Drawing.Size(1624, 348)
        Me.Ctrl_PaymentHist1.TabIndex = 3
        '
        'Ctrl_AutoRegularWithdrawal
        '
        Me.Ctrl_AutoRegularWithdrawal.Location = New System.Drawing.Point(1661, 371)
        Me.Ctrl_AutoRegularWithdrawal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_AutoRegularWithdrawal.Name = "Ctrl_AutoRegularWithdrawal"
        Me.Ctrl_AutoRegularWithdrawal.OwnerNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawal.PolicyNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawal.Size = New System.Drawing.Size(816, 439)
        Me.Ctrl_AutoRegularWithdrawal.TabIndex = 4
        '
        'tabLevyHistory
        '
        Me.tabLevyHistory.AutoScroll = True
        Me.tabLevyHistory.Controls.Add(Me.Ctrl_LevyHistory)
        Me.tabLevyHistory.Location = New System.Drawing.Point(4, 254)
        Me.tabLevyHistory.Name = "tabLevyHistory"
        Me.tabLevyHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabLevyHistory.TabIndex = 29
        Me.tabLevyHistory.Tag = "Levy History"
        Me.tabLevyHistory.Text = "Levy History"
        Me.tabLevyHistory.UseVisualStyleBackColor = True
        Me.tabLevyHistory.Visible = False
        '
        'Ctrl_LevyHistory
        '
        Me.Ctrl_LevyHistory.AutoScroll = True
        Me.Ctrl_LevyHistory.AutoSize = True
        Me.Ctrl_LevyHistory.Location = New System.Drawing.Point(5, 4)
        Me.Ctrl_LevyHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_LevyHistory.Name = "Ctrl_LevyHistory"
        Me.Ctrl_LevyHistory.PolicyNoInUse = Nothing
        Me.Ctrl_LevyHistory.Size = New System.Drawing.Size(2246, 1750)
        Me.Ctrl_LevyHistory.TabIndex = 1
        '
        'tabARWHistory
        '
        Me.tabARWHistory.AutoScroll = True
        Me.tabARWHistory.Controls.Add(Me.Ctrl_AutoRegularWithdrawalHis)
        Me.tabARWHistory.Location = New System.Drawing.Point(4, 279)
        Me.tabARWHistory.Name = "tabARWHistory"
        Me.tabARWHistory.Padding = New System.Windows.Forms.Padding(3)
        Me.tabARWHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabARWHistory.TabIndex = 31
        Me.tabARWHistory.Tag = "Auto / Regular Withdrawal History"
        Me.tabARWHistory.Text = "Auto / Regular Withdrawal History"
        Me.tabARWHistory.UseVisualStyleBackColor = True
        Me.tabARWHistory.Visible = False
        '
        'Ctrl_AutoRegularWithdrawalHis
        '
        Me.Ctrl_AutoRegularWithdrawalHis.AutoScroll = True
        Me.Ctrl_AutoRegularWithdrawalHis.AutoSize = True
        Me.Ctrl_AutoRegularWithdrawalHis.Location = New System.Drawing.Point(0, 9)
        Me.Ctrl_AutoRegularWithdrawalHis.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_AutoRegularWithdrawalHis.Name = "Ctrl_AutoRegularWithdrawalHis"
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoBasic = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoDependent = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.Size = New System.Drawing.Size(2909, 918)
        Me.Ctrl_AutoRegularWithdrawalHis.TabIndex = 0
        '
        'tabAnnuityPaymentHistory
        '
        Me.tabAnnuityPaymentHistory.AutoScroll = True
        Me.tabAnnuityPaymentHistory.Controls.Add(Me.Ctrl_AnnuityPaymentHist1)
        Me.tabAnnuityPaymentHistory.ImageIndex = 3
        Me.tabAnnuityPaymentHistory.Location = New System.Drawing.Point(4, 304)
        Me.tabAnnuityPaymentHistory.Name = "tabAnnuityPaymentHistory"
        Me.tabAnnuityPaymentHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabAnnuityPaymentHistory.TabIndex = 22
        Me.tabAnnuityPaymentHistory.Tag = "Annuity Payment History"
        Me.tabAnnuityPaymentHistory.Text = "Annuity Payment History"
        Me.tabAnnuityPaymentHistory.UseVisualStyleBackColor = True
        Me.tabAnnuityPaymentHistory.Visible = False
        '
        'Ctrl_AnnuityPaymentHist1
        '
        Me.Ctrl_AnnuityPaymentHist1.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_AnnuityPaymentHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_AnnuityPaymentHist1.Name = "Ctrl_AnnuityPaymentHist1"
        Me.Ctrl_AnnuityPaymentHist1.PolicyNo = ""
        Me.Ctrl_AnnuityPaymentHist1.Size = New System.Drawing.Size(1624, 348)
        Me.Ctrl_AnnuityPaymentHist1.TabIndex = 21
        '
        'tabDirectCreditHistory
        '
        Me.tabDirectCreditHistory.AutoScroll = True
        Me.tabDirectCreditHistory.Controls.Add(Me.Ctrl_DirectCreditHist1)
        Me.tabDirectCreditHistory.ImageIndex = 3
        Me.tabDirectCreditHistory.Location = New System.Drawing.Point(4, 329)
        Me.tabDirectCreditHistory.Name = "tabDirectCreditHistory"
        Me.tabDirectCreditHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabDirectCreditHistory.TabIndex = 22
        Me.tabDirectCreditHistory.Tag = "Direct Credit History"
        Me.tabDirectCreditHistory.Text = "Direct Credit History"
        Me.tabDirectCreditHistory.UseVisualStyleBackColor = True
        Me.tabDirectCreditHistory.Visible = False
        '
        'Ctrl_DirectCreditHist1
        '
        Me.Ctrl_DirectCreditHist1.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_DirectCreditHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_DirectCreditHist1.Name = "Ctrl_DirectCreditHist1"
        Me.Ctrl_DirectCreditHist1.PolicyNo = ""
        Me.Ctrl_DirectCreditHist1.Size = New System.Drawing.Size(1624, 348)
        Me.Ctrl_DirectCreditHist1.TabIndex = 21
        '
        'tabCupGetTransResult
        '
        Me.tabCupGetTransResult.AutoScroll = True
        Me.tabCupGetTransResult.Controls.Add(Me.Ctrl_CupGetTransResult)
        Me.tabCupGetTransResult.ImageIndex = 3
        Me.tabCupGetTransResult.Location = New System.Drawing.Point(4, 354)
        Me.tabCupGetTransResult.Name = "tabCupGetTransResult"
        Me.tabCupGetTransResult.Size = New System.Drawing.Size(192, 0)
        Me.tabCupGetTransResult.TabIndex = 22
        Me.tabCupGetTransResult.Tag = "CUP Payment"
        Me.tabCupGetTransResult.Text = "CUP Payment"
        Me.tabCupGetTransResult.UseVisualStyleBackColor = True
        Me.tabCupGetTransResult.Visible = False
        '
        'Ctrl_CupGetTransResult
        '
        Me.Ctrl_CupGetTransResult.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_CupGetTransResult.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_CupGetTransResult.Name = "Ctrl_CupGetTransResult"
        Me.Ctrl_CupGetTransResult.policyInuse = ""
        Me.Ctrl_CupGetTransResult.Size = New System.Drawing.Size(1624, 584)
        Me.Ctrl_CupGetTransResult.TabIndex = 21
        '
        'tabAPLHistory
        '
        Me.tabAPLHistory.AutoScroll = True
        Me.tabAPLHistory.Controls.Add(Me.LoanHist1)
        Me.tabAPLHistory.Controls.Add(Me.AplHist1)
        Me.tabAPLHistory.Controls.Add(Me.lblReminderMsg1)
        Me.tabAPLHistory.Controls.Add(Me.lblReminderMsg2)
        Me.tabAPLHistory.ImageIndex = 3
        Me.tabAPLHistory.Location = New System.Drawing.Point(4, 379)
        Me.tabAPLHistory.Name = "tabAPLHistory"
        Me.tabAPLHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabAPLHistory.TabIndex = 16
        Me.tabAPLHistory.Tag = "APL History"
        Me.tabAPLHistory.Text = "APL History"
        Me.tabAPLHistory.UseVisualStyleBackColor = True
        '
        'LoanHist1
        '
        Me.LoanHist1.Location = New System.Drawing.Point(0, 0)
        Me.LoanHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.LoanHist1.Name = "LoanHist1"
        Me.LoanHist1.PolicyNo = ""
        Me.LoanHist1.Size = New System.Drawing.Size(1443, 773)
        Me.LoanHist1.TabIndex = 1
        '
        'AplHist1
        '
        Me.AplHist1.DateFrom = New Date(CType(0, Long))
        Me.AplHist1.Location = New System.Drawing.Point(0, 0)
        Me.AplHist1.Name = "AplHist1"
        Me.AplHist1.PolicyAccountID = Nothing
        Me.AplHist1.Size = New System.Drawing.Size(1152, 514)
        Me.AplHist1.TabIndex = 0
        '
        'lblReminderMsg1
        '
        Me.lblReminderMsg1.AutoSize = True
        Me.lblReminderMsg1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReminderMsg1.Location = New System.Drawing.Point(1456, 29)
        Me.lblReminderMsg1.Name = "lblReminderMsg1"
        Me.lblReminderMsg1.Size = New System.Drawing.Size(219, 20)
        Me.lblReminderMsg1.TabIndex = 2
        Me.lblReminderMsg1.Text = "* Assurance APL History"
        '
        'lblReminderMsg2
        '
        Me.lblReminderMsg2.AutoSize = True
        Me.lblReminderMsg2.Location = New System.Drawing.Point(1456, 73)
        Me.lblReminderMsg2.Name = "lblReminderMsg2"
        Me.lblReminderMsg2.Size = New System.Drawing.Size(502, 60)
        Me.lblReminderMsg2.TabIndex = 3
        Me.lblReminderMsg2.Text = "Assurance NFO/APL/APT arrangement is different from Bermuda." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to ECM" &
    "/Navigator image, CRS Policy Notes " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Type = Assurance APL/APT), Provision"
        '
        'tabDCAR
        '
        Me.tabDCAR.AutoScroll = True
        Me.tabDCAR.Controls.Add(Me.Dcar1)
        Me.tabDCAR.ImageIndex = 3
        Me.tabDCAR.Location = New System.Drawing.Point(4, 404)
        Me.tabDCAR.Name = "tabDCAR"
        Me.tabDCAR.Size = New System.Drawing.Size(192, 0)
        Me.tabDCAR.TabIndex = 18
        Me.tabDCAR.Text = "DCAR"
        Me.tabDCAR.UseVisualStyleBackColor = True
        '
        'Dcar1
        '
        Me.Dcar1.Location = New System.Drawing.Point(6, 0)
        Me.Dcar1.Name = "Dcar1"
        Me.Dcar1.Size = New System.Drawing.Size(1044, 474)
        Me.Dcar1.TabIndex = 0
        '
        'tabUnderwriting
        '
        Me.tabUnderwriting.Controls.Add(Me.UwInfo1)
        Me.tabUnderwriting.Controls.Add(Me.UwInfoAsur)
        Me.tabUnderwriting.Location = New System.Drawing.Point(4, 404)
        Me.tabUnderwriting.Name = "tabUnderwriting"
        Me.tabUnderwriting.Size = New System.Drawing.Size(192, 0)
        Me.tabUnderwriting.TabIndex = 12
        Me.tabUnderwriting.Tag = "Underwriting"
        Me.tabUnderwriting.Text = "Underwriting"
        Me.tabUnderwriting.UseVisualStyleBackColor = True
        Me.tabUnderwriting.Visible = False
        '
        'UwInfo1
        '
        Me.UwInfo1.Location = New System.Drawing.Point(13, 12)
        Me.UwInfo1.Name = "UwInfo1"
        Me.UwInfo1.Size = New System.Drawing.Size(1152, 877)
        Me.UwInfo1.TabIndex = 0
        '
        'UwInfoAsur
        '
        Me.UwInfoAsur.Location = New System.Drawing.Point(13, 12)
        Me.UwInfoAsur.Name = "UwInfoAsur"
        Me.UwInfoAsur.Size = New System.Drawing.Size(1152, 877)
        Me.UwInfoAsur.TabIndex = 1
        '
        'tabSMS
        '
        Me.tabSMS.AutoScroll = True
        Me.tabSMS.Controls.Add(Me.Sms1)
        Me.tabSMS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSMS.Location = New System.Drawing.Point(4, 429)
        Me.tabSMS.Name = "tabSMS"
        Me.tabSMS.Size = New System.Drawing.Size(192, 0)
        Me.tabSMS.TabIndex = 14
        Me.tabSMS.Tag = "SMS"
        Me.tabSMS.Text = "PSC SMS Letter"
        Me.tabSMS.UseVisualStyleBackColor = True
        Me.tabSMS.Visible = False
        '
        'Sms1
        '
        Me.Sms1.AutoScroll = True
        Me.Sms1.CompanyID = Nothing
        Me.Sms1.Location = New System.Drawing.Point(0, 0)
        Me.Sms1.Name = "Sms1"
        Me.Sms1.PolicyAccountID = Nothing
        Me.Sms1.Size = New System.Drawing.Size(1280, 731)
        Me.Sms1.TabIndex = 0
        Me.Sms1.UseDefaultPrinter = False
        '
        'tabPolicyAlt
        '
        Me.tabPolicyAlt.AutoScroll = True
        Me.tabPolicyAlt.Controls.Add(Me.cmdPending)
        Me.tabPolicyAlt.Controls.Add(Me.cmdReturn)
        Me.tabPolicyAlt.Controls.Add(Me.PolicyAltPending1)
        Me.tabPolicyAlt.Controls.Add(Me.UclPolicyAlt1)
        Me.tabPolicyAlt.Location = New System.Drawing.Point(4, 454)
        Me.tabPolicyAlt.Name = "tabPolicyAlt"
        Me.tabPolicyAlt.Size = New System.Drawing.Size(192, 0)
        Me.tabPolicyAlt.TabIndex = 21
        Me.tabPolicyAlt.Tag = "Policy Alternation"
        Me.tabPolicyAlt.Text = "Policy Alternation"
        Me.tabPolicyAlt.UseVisualStyleBackColor = True
        '
        'cmdPending
        '
        Me.cmdPending.Location = New System.Drawing.Point(1030, 433)
        Me.cmdPending.Name = "cmdPending"
        Me.cmdPending.Size = New System.Drawing.Size(120, 33)
        Me.cmdPending.TabIndex = 99
        Me.cmdPending.Text = "Pending..."
        '
        'cmdReturn
        '
        Me.cmdReturn.Enabled = False
        Me.cmdReturn.Location = New System.Drawing.Point(1030, 474)
        Me.cmdReturn.Name = "cmdReturn"
        Me.cmdReturn.Size = New System.Drawing.Size(120, 33)
        Me.cmdReturn.TabIndex = 2
        Me.cmdReturn.Text = "Return"
        Me.cmdReturn.Visible = False
        '
        'PolicyAltPending1
        '
        Me.PolicyAltPending1.Location = New System.Drawing.Point(0, 0)
        Me.PolicyAltPending1.Name = "PolicyAltPending1"
        Me.PolicyAltPending1.Size = New System.Drawing.Size(1165, 526)
        Me.PolicyAltPending1.TabIndex = 1
        Me.PolicyAltPending1.Visible = False
        '
        'UclPolicyAlt1
        '
        Me.UclPolicyAlt1.Location = New System.Drawing.Point(0, 0)
        Me.UclPolicyAlt1.Name = "UclPolicyAlt1"
        Me.UclPolicyAlt1.Size = New System.Drawing.Size(1165, 485)
        Me.UclPolicyAlt1.TabIndex = 0
        '
        'tabCouponHistory
        '
        Me.tabCouponHistory.Controls.Add(Me.CouponHist1)
        Me.tabCouponHistory.Controls.Add(Me.Couh1)
        Me.tabCouponHistory.ImageIndex = 3
        Me.tabCouponHistory.Location = New System.Drawing.Point(4, 479)
        Me.tabCouponHistory.Name = "tabCouponHistory"
        Me.tabCouponHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabCouponHistory.TabIndex = 15
        Me.tabCouponHistory.Tag = "Coupon History"
        Me.tabCouponHistory.Text = "Coupon History"
        Me.tabCouponHistory.UseVisualStyleBackColor = True
        '
        'CouponHist1
        '
        Me.CouponHist1.Location = New System.Drawing.Point(0, 0)
        Me.CouponHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.CouponHist1.Name = "CouponHist1"
        Me.CouponHist1.PolicyNo = ""
        Me.CouponHist1.Size = New System.Drawing.Size(1250, 681)
        Me.CouponHist1.TabIndex = 1
        '
        'Couh1
        '
        Me.Couh1.DateFrom = New Date(CType(0, Long))
        Me.Couh1.Location = New System.Drawing.Point(0, 0)
        Me.Couh1.Name = "Couh1"
        Me.Couh1.PolicyAccountID = Nothing
        Me.Couh1.Size = New System.Drawing.Size(1152, 544)
        Me.Couh1.TabIndex = 0
        '
        'tabDDACCD
        '
        Me.tabDDACCD.AutoScroll = True
        Me.tabDDACCD.Controls.Add(Me.Ctrl_DirectDebitEnq1)
        Me.tabDDACCD.Controls.Add(Me.Ddaccdr1)
        Me.tabDDACCD.ImageIndex = 3
        Me.tabDDACCD.Location = New System.Drawing.Point(4, 504)
        Me.tabDDACCD.Name = "tabDDACCD"
        Me.tabDDACCD.Size = New System.Drawing.Size(192, 0)
        Me.tabDDACCD.TabIndex = 3
        Me.tabDDACCD.Tag = "DDA/CCDR"
        Me.tabDDACCD.Text = "DDA/CCDR"
        Me.tabDDACCD.UseVisualStyleBackColor = True
        Me.tabDDACCD.Visible = False
        '
        'Ctrl_DirectDebitEnq1
        '
        Me.Ctrl_DirectDebitEnq1.ClientNoInUse = ""
        Me.Ctrl_DirectDebitEnq1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_DirectDebitEnq1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_DirectDebitEnq1.Name = "Ctrl_DirectDebitEnq1"
        Me.Ctrl_DirectDebitEnq1.PolicyNoInUse = ""
        Me.Ctrl_DirectDebitEnq1.Size = New System.Drawing.Size(1246, 794)
        Me.Ctrl_DirectDebitEnq1.TabIndex = 1
        '
        'Ddaccdr1
        '
        Me.Ddaccdr1.dtPriv = Nothing
        Me.Ddaccdr1.Location = New System.Drawing.Point(0, 0)
        Me.Ddaccdr1.Name = "Ddaccdr1"
        Me.Ddaccdr1.Size = New System.Drawing.Size(1158, 737)
        Me.Ddaccdr1.strUPSMenuCtrl = Nothing
        Me.Ddaccdr1.TabIndex = 0
        '
        'tabCashFlow
        '
        Me.tabCashFlow.AutoScroll = True
        Me.tabCashFlow.Controls.Add(Me.btnSelect)
        Me.tabCashFlow.Controls.Add(Me.Ctrl_Sel_CO1)
        Me.tabCashFlow.Controls.Add(Me.CashFlow1)
        Me.tabCashFlow.Controls.Add(Me.UclCashFlow1)
        Me.tabCashFlow.Location = New System.Drawing.Point(4, 529)
        Me.tabCashFlow.Name = "tabCashFlow"
        Me.tabCashFlow.Size = New System.Drawing.Size(192, 0)
        Me.tabCashFlow.TabIndex = 17
        Me.tabCashFlow.Tag = "Cash Flow"
        Me.tabCashFlow.Text = "Cash Flow"
        Me.tabCashFlow.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(1467, 44)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(120, 33)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'Ctrl_Sel_CO1
        '
        Me.Ctrl_Sel_CO1.BasicPlan = ""
        Me.Ctrl_Sel_CO1.ClientNameInuse = ""
        Me.Ctrl_Sel_CO1.ClientNoInuse = ""
        Me.Ctrl_Sel_CO1.CovCodeInuse = ""
        Me.Ctrl_Sel_CO1.CovDescInuse = ""
        Me.Ctrl_Sel_CO1.CovNoInuse = ""
        Me.Ctrl_Sel_CO1.currdsInuse = Nothing
        Me.Ctrl_Sel_CO1.LifeNoInUse = ""
        Me.Ctrl_Sel_CO1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_Sel_CO1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_Sel_CO1.Name = "Ctrl_Sel_CO1"
        Me.Ctrl_Sel_CO1.PolicyNoInuse = ""
        Me.Ctrl_Sel_CO1.PStsInuse = ""
        Me.Ctrl_Sel_CO1.RCDDateInUse = ""
        Me.Ctrl_Sel_CO1.RiderInuse = ""
        Me.Ctrl_Sel_CO1.RiskStsInuse = ""
        Me.Ctrl_Sel_CO1.Size = New System.Drawing.Size(1638, 149)
        Me.Ctrl_Sel_CO1.TabIndex = 2
        '
        'CashFlow1
        '
        Me.CashFlow1.coveragenoinuse = Nothing
        Me.CashFlow1.DetailDS = Nothing
        Me.CashFlow1.Enabled = False
        Me.CashFlow1.HeaderDS = Nothing
        Me.CashFlow1.HistFromDateinuse = "12:00:00 AM"
        Me.CashFlow1.lifenoinuse = Nothing
        Me.CashFlow1.Location = New System.Drawing.Point(0, 145)
        Me.CashFlow1.Margin = New System.Windows.Forms.Padding(4)
        Me.CashFlow1.modeinuse = Utility.Utility.ModeName.Add
        Me.CashFlow1.Name = "CashFlow1"
        Me.CashFlow1.policynoinuse = Nothing
        Me.CashFlow1.ridernoinuse = Nothing
        Me.CashFlow1.Size = New System.Drawing.Size(1050, 611)
        Me.CashFlow1.TabIndex = 1
        '
        'UclCashFlow1
        '
        Me.UclCashFlow1.Location = New System.Drawing.Point(0, 0)
        Me.UclCashFlow1.Name = "UclCashFlow1"
        Me.UclCashFlow1.Size = New System.Drawing.Size(1158, 538)
        Me.UclCashFlow1.TabIndex = 0
        '
        'tabClaimsHistory
        '
        Me.tabClaimsHistory.AutoScroll = True
        Me.tabClaimsHistory.Controls.Add(Me.ClaimHist1)
        Me.tabClaimsHistory.Location = New System.Drawing.Point(4, 554)
        Me.tabClaimsHistory.Name = "tabClaimsHistory"
        Me.tabClaimsHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabClaimsHistory.TabIndex = 7
        Me.tabClaimsHistory.Tag = "Claims History"
        Me.tabClaimsHistory.Text = "Minor Claims History"
        Me.tabClaimsHistory.UseVisualStyleBackColor = True
        Me.tabClaimsHistory.Visible = False
        '
        'ClaimHist1
        '
        Me.ClaimHist1.AutoScroll = True
        Me.ClaimHist1.AutoSize = True
        Me.ClaimHist1.Location = New System.Drawing.Point(0, 0)
        Me.ClaimHist1.Name = "ClaimHist1"
        Me.ClaimHist1.PolicyAccountID = Nothing
        Me.ClaimHist1.Size = New System.Drawing.Size(2979, 1156)
        Me.ClaimHist1.TabIndex = 0
        '
        'tabTransactionHistory
        '
        Me.tabTransactionHistory.Controls.Add(Me.Ctrl_TranHist1)
        Me.tabTransactionHistory.Location = New System.Drawing.Point(4, 579)
        Me.tabTransactionHistory.Name = "tabTransactionHistory"
        Me.tabTransactionHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabTransactionHistory.TabIndex = 13
        Me.tabTransactionHistory.Tag = "Transaction History"
        Me.tabTransactionHistory.Text = "Transaction History"
        Me.tabTransactionHistory.UseVisualStyleBackColor = True
        Me.tabTransactionHistory.Visible = False
        '
        'Ctrl_TranHist1
        '
        Me.Ctrl_TranHist1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_TranHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_TranHist1.Name = "Ctrl_TranHist1"
        Me.Ctrl_TranHist1.PolicyNoInUse = ""
        Me.Ctrl_TranHist1.Size = New System.Drawing.Size(1246, 715)
        Me.Ctrl_TranHist1.TabIndex = 1
        '
        'tabCustomerHistory
        '
        Me.tabCustomerHistory.Controls.Add(Me.CustHist1)
        Me.tabCustomerHistory.ImageIndex = 3
        Me.tabCustomerHistory.Location = New System.Drawing.Point(4, 604)
        Me.tabCustomerHistory.Name = "tabCustomerHistory"
        Me.tabCustomerHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabCustomerHistory.TabIndex = 6
        Me.tabCustomerHistory.Tag = "Customer History"
        Me.tabCustomerHistory.Text = "Customer History"
        Me.tabCustomerHistory.UseVisualStyleBackColor = True
        Me.tabCustomerHistory.Visible = False
        '
        'CustHist1
        '
        Me.CustHist1.Location = New System.Drawing.Point(0, 0)
        Me.CustHist1.Name = "CustHist1"
        Me.CustHist1.Size = New System.Drawing.Size(1152, 561)
        Me.CustHist1.TabIndex = 0
        '
        'tabServiceLog
        '
        Me.tabServiceLog.AutoScroll = True
        Me.tabServiceLog.Controls.Add(Me.UclServiceLog1)
        Me.tabServiceLog.Location = New System.Drawing.Point(4, 629)
        Me.tabServiceLog.Name = "tabServiceLog"
        Me.tabServiceLog.Size = New System.Drawing.Size(192, 0)
        Me.tabServiceLog.TabIndex = 8
        Me.tabServiceLog.Tag = "Service Log"
        Me.tabServiceLog.Text = "Service Log"
        Me.tabServiceLog.UseVisualStyleBackColor = True
        Me.tabServiceLog.Visible = False
        '
        'UclServiceLog1
        '
        Me.UclServiceLog1.CompanyID = Nothing
        Me.UclServiceLog1.CustomerID = Nothing
        Me.UclServiceLog1.IDNumber = Nothing
        Me.UclServiceLog1.IsNBMPolicy = False
        Me.UclServiceLog1.Location = New System.Drawing.Point(0, 0)
        Me.UclServiceLog1.Name = "UclServiceLog1"
        Me.UclServiceLog1.PolicyType = Nothing
        Me.UclServiceLog1.ReturnDataTableRetentionCampaignEnquiry = Nothing
        Me.UclServiceLog1.Size = New System.Drawing.Size(4000, 1535)
        Me.UclServiceLog1.TabIndex = 0
        '
        'tabAgentInfo
        '
        Me.tabAgentInfo.Location = New System.Drawing.Point(4, 654)
        Me.tabAgentInfo.Name = "tabAgentInfo"
        Me.tabAgentInfo.Size = New System.Drawing.Size(192, 0)
        Me.tabAgentInfo.TabIndex = 10
        Me.tabAgentInfo.Tag = "Agent Info"
        Me.tabAgentInfo.Text = "Agent Information"
        Me.tabAgentInfo.UseVisualStyleBackColor = True
        Me.tabAgentInfo.Visible = False
        '
        'tabFundTrans
        '
        Me.tabFundTrans.Controls.Add(Me.Ctrl_FundTranHist1)
        Me.tabFundTrans.Location = New System.Drawing.Point(4, 679)
        Me.tabFundTrans.Name = "tabFundTrans"
        Me.tabFundTrans.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFundTrans.Size = New System.Drawing.Size(192, 0)
        Me.tabFundTrans.TabIndex = 23
        Me.tabFundTrans.Text = "Fund Transaction History"
        Me.tabFundTrans.UseVisualStyleBackColor = True
        '
        'Ctrl_FundTranHist1
        '
        Me.Ctrl_FundTranHist1.AutoScroll = True
        Me.Ctrl_FundTranHist1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_FundTranHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FundTranHist1.Name = "Ctrl_FundTranHist1"
        Me.Ctrl_FundTranHist1.PolicyNoInUse = Nothing
        Me.Ctrl_FundTranHist1.Size = New System.Drawing.Size(1403, 821)
        Me.Ctrl_FundTranHist1.TabIndex = 0
        '
        'tabPayOutHist
        '
        Me.tabPayOutHist.Controls.Add(Me.Ctrl_CashDividendPayoutHist1)
        Me.tabPayOutHist.Location = New System.Drawing.Point(4, 704)
        Me.tabPayOutHist.Name = "tabPayOutHist"
        Me.tabPayOutHist.Size = New System.Drawing.Size(192, 0)
        Me.tabPayOutHist.TabIndex = 32
        Me.tabPayOutHist.Text = "Pay Out History"
        Me.tabPayOutHist.UseVisualStyleBackColor = True
        '
        'Ctrl_CashDividendPayoutHist1
        '
        Me.Ctrl_CashDividendPayoutHist1.Location = New System.Drawing.Point(5, 4)
        Me.Ctrl_CashDividendPayoutHist1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_CashDividendPayoutHist1.Name = "Ctrl_CashDividendPayoutHist1"
        Me.Ctrl_CashDividendPayoutHist1.PolicyNo = ""
        Me.Ctrl_CashDividendPayoutHist1.Size = New System.Drawing.Size(1670, 579)
        Me.Ctrl_CashDividendPayoutHist1.TabIndex = 0
        '
        'tabPolicyNotes
        '
        Me.tabPolicyNotes.AutoScroll = True
        Me.tabPolicyNotes.Controls.Add(Me.PolicyNote1)
        Me.tabPolicyNotes.Location = New System.Drawing.Point(4, 729)
        Me.tabPolicyNotes.Name = "tabPolicyNotes"
        Me.tabPolicyNotes.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPolicyNotes.Size = New System.Drawing.Size(192, 0)
        Me.tabPolicyNotes.TabIndex = 24
        Me.tabPolicyNotes.Text = "Policy Notes"
        Me.tabPolicyNotes.UseVisualStyleBackColor = True
        '
        'PolicyNote1
        '
        Me.PolicyNote1.AllowEdit = False
        Me.PolicyNote1.DataAdaptor = PolicyNoteDefaultAdaptor7
        Me.PolicyNote1.Location = New System.Drawing.Point(0, 1)
        Me.PolicyNote1.Margin = New System.Windows.Forms.Padding(4)
        Me.PolicyNote1.Name = "PolicyNote1"
        Me.PolicyNote1.Size = New System.Drawing.Size(1304, 924)
        Me.PolicyNote1.TabIndex = 0
        '
        'tabDesignatedPerson
        '
        Me.tabDesignatedPerson.AutoScroll = True
        Me.tabDesignatedPerson.Controls.Add(Me.Ctrl_DesignatedPerson)
        Me.tabDesignatedPerson.Location = New System.Drawing.Point(4, 754)
        Me.tabDesignatedPerson.Name = "tabDesignatedPerson"
        Me.tabDesignatedPerson.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDesignatedPerson.Size = New System.Drawing.Size(192, 0)
        Me.tabDesignatedPerson.TabIndex = 34
        Me.tabDesignatedPerson.Text = "Designated Person"
        Me.tabDesignatedPerson.UseVisualStyleBackColor = True
        '
        'Ctrl_DesignatedPerson
        '
        Me.Ctrl_DesignatedPerson.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_DesignatedPerson.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_DesignatedPerson.Name = "Ctrl_DesignatedPerson"
        Me.Ctrl_DesignatedPerson.PolicyInUse = ""
        Me.Ctrl_DesignatedPerson.PolicyNoInuse = ""
        Me.Ctrl_DesignatedPerson.Size = New System.Drawing.Size(1312, 918)
        Me.Ctrl_DesignatedPerson.TabIndex = 0
        '
        'tabSubAcc
        '
        Me.tabSubAcc.AutoScroll = True
        Me.tabSubAcc.Controls.Add(Me.Ctrl_FinancialDtl1)
        Me.tabSubAcc.Controls.Add(Me.Ctrl_FinancialInfo1)
        Me.tabSubAcc.Location = New System.Drawing.Point(4, 779)
        Me.tabSubAcc.Name = "tabSubAcc"
        Me.tabSubAcc.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSubAcc.Size = New System.Drawing.Size(192, 0)
        Me.tabSubAcc.TabIndex = 25
        Me.tabSubAcc.Text = "Sub-Account Balance"
        Me.tabSubAcc.UseVisualStyleBackColor = True
        '
        'Ctrl_FinancialDtl1
        '
        Me.Ctrl_FinancialDtl1.Location = New System.Drawing.Point(5, 709)
        Me.Ctrl_FinancialDtl1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Enquiry
        Me.Ctrl_FinancialDtl1.Name = "Ctrl_FinancialDtl1"
        Me.Ctrl_FinancialDtl1.PolicyNoInUse = ""
        Me.Ctrl_FinancialDtl1.Size = New System.Drawing.Size(1472, 393)
        Me.Ctrl_FinancialDtl1.TabIndex = 3
        '
        'Ctrl_FinancialInfo1
        '
        Me.Ctrl_FinancialInfo1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_FinancialInfo1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FinancialInfo1.Name = "Ctrl_FinancialInfo1"
        Me.Ctrl_FinancialInfo1.PolicyNoInUse = ""
        Me.Ctrl_FinancialInfo1.Size = New System.Drawing.Size(1246, 715)
        Me.Ctrl_FinancialInfo1.TabIndex = 2
        '
        'tabParSur
        '
        Me.tabParSur.Controls.Add(Me.UclParSur1)
        Me.tabParSur.Location = New System.Drawing.Point(4, 804)
        Me.tabParSur.Name = "tabParSur"
        Me.tabParSur.Padding = New System.Windows.Forms.Padding(3)
        Me.tabParSur.Size = New System.Drawing.Size(192, 0)
        Me.tabParSur.TabIndex = 26
        Me.tabParSur.Text = "Partial Surrender"
        Me.tabParSur.UseVisualStyleBackColor = True
        '
        'UclParSur1
        '
        Me.UclParSur1.AllowUpdate = False
        Me.UclParSur1.Location = New System.Drawing.Point(5, 9)
        Me.UclParSur1.Margin = New System.Windows.Forms.Padding(4)
        Me.UclParSur1.Name = "UclParSur1"
        Me.UclParSur1.PolicyNumber = Nothing
        Me.UclParSur1.Size = New System.Drawing.Size(1638, 742)
        Me.UclParSur1.TabIndex = 0
        '
        'tabiLASSMS
        '
        Me.tabiLASSMS.AutoScroll = True
        Me.tabiLASSMS.Controls.Add(Me.SMSiLAS)
        Me.tabiLASSMS.Location = New System.Drawing.Point(4, 829)
        Me.tabiLASSMS.Name = "tabiLASSMS"
        Me.tabiLASSMS.Padding = New System.Windows.Forms.Padding(3)
        Me.tabiLASSMS.Size = New System.Drawing.Size(192, 0)
        Me.tabiLASSMS.TabIndex = 27
        Me.tabiLASSMS.Text = "iLAS SMS"
        Me.tabiLASSMS.UseVisualStyleBackColor = True
        '
        'SMSiLAS
        '
        Me.SMSiLAS.CustomerID = Nothing
        Me.SMSiLAS.Location = New System.Drawing.Point(35, 9)
        Me.SMSiLAS.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SMSiLAS.Name = "SMSiLAS"
        Me.SMSiLAS.PolicyAccountID = Nothing
        Me.SMSiLAS.Size = New System.Drawing.Size(1099, 736)
        Me.SMSiLAS.TabIndex = 0
        '
        'tabPostSalesCall
        '
        Me.tabPostSalesCall.Controls.Add(Me.Label7)
        Me.tabPostSalesCall.Controls.Add(Me.UclPostSalesCallQuestionnaire1)
        Me.tabPostSalesCall.Location = New System.Drawing.Point(4, 854)
        Me.tabPostSalesCall.Name = "tabPostSalesCall"
        Me.tabPostSalesCall.Size = New System.Drawing.Size(192, 0)
        Me.tabPostSalesCall.TabIndex = 28
        Me.tabPostSalesCall.Text = "Post Sales Call"
        Me.tabPostSalesCall.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 20)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Basic Plan/Rider"
        '
        'UclPostSalesCallQuestionnaire1
        '
        Me.UclPostSalesCallQuestionnaire1.Location = New System.Drawing.Point(21, 42)
        Me.UclPostSalesCallQuestionnaire1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UclPostSalesCallQuestionnaire1.Name = "UclPostSalesCallQuestionnaire1"
        Me.UclPostSalesCallQuestionnaire1.PolicyAccountID = Nothing
        Me.UclPostSalesCallQuestionnaire1.Size = New System.Drawing.Size(1477, 797)
        Me.UclPostSalesCallQuestionnaire1.TabIndex = 0
        '
        'tabTapNGo
        '
        Me.tabTapNGo.AutoScroll = True
        Me.tabTapNGo.BackColor = System.Drawing.Color.Transparent
        Me.tabTapNGo.Controls.Add(Me.Ctrl_TapNGo1)
        Me.tabTapNGo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabTapNGo.ForeColor = System.Drawing.Color.Black
        Me.tabTapNGo.ImageIndex = 3
        Me.tabTapNGo.Location = New System.Drawing.Point(4, 879)
        Me.tabTapNGo.Name = "tabTapNGo"
        Me.tabTapNGo.Size = New System.Drawing.Size(192, 0)
        Me.tabTapNGo.TabIndex = 0
        Me.tabTapNGo.Tag = "Tap & Go"
        Me.tabTapNGo.Text = "Tap & Go"
        Me.tabTapNGo.UseVisualStyleBackColor = True
        '
        'Ctrl_TapNGo1
        '
        Me.Ctrl_TapNGo1.CustomerID = ""
        Me.Ctrl_TapNGo1.Lang = "en"
        Me.Ctrl_TapNGo1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_TapNGo1.LockDate = ""
        Me.Ctrl_TapNGo1.LockStatus = ""
        Me.Ctrl_TapNGo1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_TapNGo1.Name = "Ctrl_TapNGo1"
        Me.Ctrl_TapNGo1.PolicyNo = ""
        Me.Ctrl_TapNGo1.PolicyNoInUse = ""
        Me.Ctrl_TapNGo1.Size = New System.Drawing.Size(1653, 1060)
        Me.Ctrl_TapNGo1.TabIndex = 0
        '
        'tabOePay
        '
        Me.tabOePay.AutoScroll = True
        Me.tabOePay.BackColor = System.Drawing.Color.Transparent
        Me.tabOePay.Controls.Add(Me.Ctrl_OePay1)
        Me.tabOePay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabOePay.ForeColor = System.Drawing.Color.Black
        Me.tabOePay.ImageIndex = 3
        Me.tabOePay.Location = New System.Drawing.Point(4, 879)
        Me.tabOePay.Name = "tabOePay"
        Me.tabOePay.Size = New System.Drawing.Size(192, 0)
        Me.tabOePay.TabIndex = 0
        Me.tabOePay.Tag = "O!ePay"
        Me.tabOePay.Text = "O!ePay"
        Me.tabOePay.UseVisualStyleBackColor = True
        '
        'Ctrl_OePay1
        '
        Me.Ctrl_OePay1.AccountID = ""
        Me.Ctrl_OePay1.Balance = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Ctrl_OePay1.BalanceCCy = ""
        Me.Ctrl_OePay1.BalanceDate = ""
        Me.Ctrl_OePay1.EndDate = ""
        Me.Ctrl_OePay1.Lang = "en"
        Me.Ctrl_OePay1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_OePay1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_OePay1.MerchantID = ""
        Me.Ctrl_OePay1.Name = "Ctrl_OePay1"
        Me.Ctrl_OePay1.PolicyNo = ""
        Me.Ctrl_OePay1.PolicyNoInUse = ""
        Me.Ctrl_OePay1.ProductID = ""
        Me.Ctrl_OePay1.Size = New System.Drawing.Size(1653, 1060)
        Me.Ctrl_OePay1.StartDate = ""
        Me.Ctrl_OePay1.TabIndex = 0
        '
        'tabAsurPolicySummary
        '
        Me.tabAsurPolicySummary.Controls.Add(Me.UclPolicySummary_Asur1)
        Me.tabAsurPolicySummary.Location = New System.Drawing.Point(4, 904)
        Me.tabAsurPolicySummary.Name = "tabAsurPolicySummary"
        Me.tabAsurPolicySummary.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAsurPolicySummary.Size = New System.Drawing.Size(192, 0)
        Me.tabAsurPolicySummary.TabIndex = 32
        Me.tabAsurPolicySummary.Text = "Policy Summary (Assurance)"
        Me.tabAsurPolicySummary.UseVisualStyleBackColor = True
        '
        'UclPolicySummary_Asur1
        '
        Me.UclPolicySummary_Asur1.Location = New System.Drawing.Point(5, 3)
        Me.UclPolicySummary_Asur1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UclPolicySummary_Asur1.Name = "UclPolicySummary_Asur1"
        Me.UclPolicySummary_Asur1.PolicyNumber = Nothing
        Me.UclPolicySummary_Asur1.Size = New System.Drawing.Size(1742, 878)
        Me.UclPolicySummary_Asur1.TabIndex = 0
        '
        'tabAsurServiceLog
        '
        Me.tabAsurServiceLog.Controls.Add(Me.UclServiceLog2)
        Me.tabAsurServiceLog.Location = New System.Drawing.Point(4, 929)
        Me.tabAsurServiceLog.Name = "tabAsurServiceLog"
        Me.tabAsurServiceLog.Size = New System.Drawing.Size(192, 0)
        Me.tabAsurServiceLog.TabIndex = 33
        Me.tabAsurServiceLog.Text = "Service Log (Assurance)"
        Me.tabAsurServiceLog.UseVisualStyleBackColor = True
        '
        'UclServiceLog2
        '
        Me.UclServiceLog2.AutoSize = True
        Me.UclServiceLog2.CustomerID = Nothing
        Me.UclServiceLog2.Location = New System.Drawing.Point(0, 0)
        Me.UclServiceLog2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UclServiceLog2.Name = "UclServiceLog2"
        Me.UclServiceLog2.PolicyType = Nothing
        Me.UclServiceLog2.Size = New System.Drawing.Size(4000, 2192)
        Me.UclServiceLog2.TabIndex = 0
        '
        'tabIWSHistory
        '
        Me.tabIWSHistory.AutoScroll = True
        Me.tabIWSHistory.Controls.Add(Me.Ctrl_IWSHistoryEnquiry1)
        Me.tabIWSHistory.Location = New System.Drawing.Point(4, 954)
        Me.tabIWSHistory.Name = "tabIWSHistory"
        Me.tabIWSHistory.Size = New System.Drawing.Size(192, 0)
        Me.tabIWSHistory.TabIndex = 22
        Me.tabIWSHistory.Tag = "IWS History"
        Me.tabIWSHistory.Text = "IWS History"
        Me.tabIWSHistory.UseVisualStyleBackColor = True
        '
        'Ctrl_IWSHistoryEnquiry1
        '
        Me.Ctrl_IWSHistoryEnquiry1.Location = New System.Drawing.Point(0, -44)
        Me.Ctrl_IWSHistoryEnquiry1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Ctrl_IWSHistoryEnquiry1.Name = "Ctrl_IWSHistoryEnquiry1"
        Me.Ctrl_IWSHistoryEnquiry1.Size = New System.Drawing.Size(1466, 604)
        Me.Ctrl_IWSHistoryEnquiry1.TabIndex = 0
        '
        'tabTraditionalPartialSurrQuot
        '
        Me.tabTraditionalPartialSurrQuot.AutoScroll = True
        Me.tabTraditionalPartialSurrQuot.Controls.Add(Me.Ctrl_Sel_CO2)
        Me.tabTraditionalPartialSurrQuot.Controls.Add(Me.Ctrl_POS_ParSurTradition1)
        Me.tabTraditionalPartialSurrQuot.Location = New System.Drawing.Point(4, 979)
        Me.tabTraditionalPartialSurrQuot.Name = "tabTraditionalPartialSurrQuot"
        Me.tabTraditionalPartialSurrQuot.Size = New System.Drawing.Size(192, 0)
        Me.tabTraditionalPartialSurrQuot.TabIndex = 1
        Me.tabTraditionalPartialSurrQuot.Tag = "Traditional Partial Surrender Quotation"
        Me.tabTraditionalPartialSurrQuot.Text = "Traditional Partial Surrender Quotation"
        Me.tabTraditionalPartialSurrQuot.UseVisualStyleBackColor = True
        Me.tabTraditionalPartialSurrQuot.Visible = False
        '
        'Ctrl_Sel_CO2
        '
        Me.Ctrl_Sel_CO2.BasicPlan = ""
        Me.Ctrl_Sel_CO2.ClientNameInuse = ""
        Me.Ctrl_Sel_CO2.ClientNoInuse = ""
        Me.Ctrl_Sel_CO2.CovCodeInuse = ""
        Me.Ctrl_Sel_CO2.CovDescInuse = ""
        Me.Ctrl_Sel_CO2.CovNoInuse = ""
        Me.Ctrl_Sel_CO2.currdsInuse = Nothing
        Me.Ctrl_Sel_CO2.LifeNoInUse = ""
        Me.Ctrl_Sel_CO2.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_Sel_CO2.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_Sel_CO2.Name = "Ctrl_Sel_CO2"
        Me.Ctrl_Sel_CO2.PolicyNoInuse = ""
        Me.Ctrl_Sel_CO2.PStsInuse = ""
        Me.Ctrl_Sel_CO2.RCDDateInUse = ""
        Me.Ctrl_Sel_CO2.RiderInuse = ""
        Me.Ctrl_Sel_CO2.RiskStsInuse = ""
        Me.Ctrl_Sel_CO2.Size = New System.Drawing.Size(1638, 149)
        Me.Ctrl_Sel_CO2.TabIndex = 1
        '
        'Ctrl_POS_ParSurTradition1
        '
        Me.Ctrl_POS_ParSurTradition1.CoverageSelectedInUse = False
        Me.Ctrl_POS_ParSurTradition1.CurrentCoverageCodeInUse = Nothing
        Me.Ctrl_POS_ParSurTradition1.CurrentCoverageNoInUse = Nothing
        Me.Ctrl_POS_ParSurTradition1.CurrentLifeNoInUse = Nothing
        Me.Ctrl_POS_ParSurTradition1.CurrentPolicyNoInUse = Nothing
        Me.Ctrl_POS_ParSurTradition1.CurrentRiderNoInUse = Nothing
        Me.Ctrl_POS_ParSurTradition1.Location = New System.Drawing.Point(0, 155)
        Me.Ctrl_POS_ParSurTradition1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_POS_ParSurTradition1.Name = "Ctrl_POS_ParSurTradition1"
        Me.Ctrl_POS_ParSurTradition1.Size = New System.Drawing.Size(1638, 1023)
        Me.Ctrl_POS_ParSurTradition1.TabIndex = 3
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
        '
        'tabIULControl
        '
        Me.tabIULControl.AutoScroll = True
        Me.tabIULControl.Location = New System.Drawing.Point(0, 0)
        Me.tabIULControl.Name = "tabIULControl"
        Me.tabIULControl.Size = New System.Drawing.Size(200, 100)
        Me.tabIULControl.TabIndex = 18
        Me.tabIULControl.Tag = "Account/ Segment Details"
        Me.tabIULControl.Text = "Account/ Segment Details"
        Me.tabIULControl.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(376, 41)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(173, 26)
        Me.txtStatus.TabIndex = 2
        '
        'txtPolicy
        '
        Me.txtPolicy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicy.Location = New System.Drawing.Point(173, 41)
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.ReadOnly = True
        Me.txtPolicy.Size = New System.Drawing.Size(128, 26)
        Me.txtPolicy.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(77, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Policy No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Status"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(563, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Product"
        '
        'txtProduct
        '
        Me.txtProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtProduct.Location = New System.Drawing.Point(640, 41)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.ReadOnly = True
        Me.txtProduct.Size = New System.Drawing.Size(302, 26)
        Me.txtProduct.TabIndex = 7
        '
        'txtChiName
        '
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtChiName.Location = New System.Drawing.Point(902, 6)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.Size = New System.Drawing.Size(148, 30)
        Me.txtChiName.TabIndex = 8
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Location = New System.Drawing.Point(595, 6)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(301, 26)
        Me.txtFirstName.TabIndex = 9
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Location = New System.Drawing.Point(307, 6)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(282, 26)
        Me.txtLastName.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 20)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Serving Customer"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(173, 6)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(128, 26)
        Me.txtTitle.TabIndex = 12
        '
        'lblAPL
        '
        Me.lblAPL.AutoSize = True
        Me.lblAPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPL.ForeColor = System.Drawing.Color.Blue
        Me.lblAPL.Location = New System.Drawing.Point(1062, 6)
        Me.lblAPL.Name = "lblAPL"
        Me.lblAPL.Size = New System.Drawing.Size(46, 22)
        Me.lblAPL.TabIndex = 13
        Me.lblAPL.Text = "APL"
        Me.lblAPL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAPL.Visible = False
        '
        'lblLastAPL
        '
        Me.lblLastAPL.AutoSize = True
        Me.lblLastAPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLastAPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastAPL.ForeColor = System.Drawing.Color.Blue
        Me.lblLastAPL.Location = New System.Drawing.Point(1062, 6)
        Me.lblLastAPL.Name = "lblLastAPL"
        Me.lblLastAPL.Size = New System.Drawing.Size(89, 22)
        Me.lblLastAPL.TabIndex = 13
        Me.lblLastAPL.Text = "Last APL"
        Me.lblLastAPL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLastAPL.Visible = False
        '
        'txtCName
        '
        Me.txtCName.Location = New System.Drawing.Point(173, 6)
        Me.txtCName.Name = "txtCName"
        Me.txtCName.Size = New System.Drawing.Size(531, 26)
        Me.txtCName.TabIndex = 14
        Me.txtCName.Visible = False
        '
        'txtCNameChi
        '
        Me.txtCNameChi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCNameChi.Location = New System.Drawing.Point(710, 6)
        Me.txtCNameChi.Name = "txtCNameChi"
        Me.txtCNameChi.Size = New System.Drawing.Size(340, 30)
        Me.txtCNameChi.TabIndex = 15
        Me.txtCNameChi.Visible = False
        '
        'txtBillNo
        '
        Me.txtBillNo.Location = New System.Drawing.Point(1022, 41)
        Me.txtBillNo.MaxLength = 12
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.Size = New System.Drawing.Size(136, 26)
        Me.txtBillNo.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(950, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 20)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Bill No."
        '
        'lblDateFormatMsg
        '
        Me.lblDateFormatMsg.AutoSize = True
        Me.lblDateFormatMsg.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblDateFormatMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDateFormatMsg.Location = New System.Drawing.Point(1889, 0)
        Me.lblDateFormatMsg.Name = "lblDateFormatMsg"
        Me.lblDateFormatMsg.Size = New System.Drawing.Size(361, 20)
        Me.lblDateFormatMsg.TabIndex = 18
        Me.lblDateFormatMsg.Text = "All short date is in format - ""mm/dd/yyyy"" "
        '
        'lblTAD2CPolicyMsg
        '
        Me.lblTAD2CPolicyMsg.AutoSize = True
        Me.lblTAD2CPolicyMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTAD2CPolicyMsg.ForeColor = System.Drawing.Color.SkyBlue
        Me.lblTAD2CPolicyMsg.Location = New System.Drawing.Point(1904, 45)
        Me.lblTAD2CPolicyMsg.Name = "lblTAD2CPolicyMsg"
        Me.lblTAD2CPolicyMsg.Size = New System.Drawing.Size(178, 20)
        Me.lblTAD2CPolicyMsg.TabIndex = 18
        Me.lblTAD2CPolicyMsg.Text = "TA/CDC D2C Policy"
        Me.lblTAD2CPolicyMsg.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1184, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 20)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "e-Statement"
        '
        'txtEstatement
        '
        Me.txtEstatement.Location = New System.Drawing.Point(1296, 39)
        Me.txtEstatement.Name = "txtEstatement"
        Me.txtEstatement.ReadOnly = True
        Me.txtEstatement.Size = New System.Drawing.Size(46, 26)
        Me.txtEstatement.TabIndex = 19
        '
        'lblCapsilPolNo
        '
        Me.lblCapsilPolNo.AutoSize = True
        Me.lblCapsilPolNo.ForeColor = System.Drawing.Color.Green
        Me.lblCapsilPolNo.Location = New System.Drawing.Point(1182, 10)
        Me.lblCapsilPolNo.Name = "lblCapsilPolNo"
        Me.lblCapsilPolNo.Size = New System.Drawing.Size(19, 20)
        Me.lblCapsilPolNo.TabIndex = 21
        Me.lblCapsilPolNo.Text = "--"
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(1352, 6)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(0, 20)
        Me.lblMsg.TabIndex = 26
        '
        'lblEPolicy
        '
        Me.lblEPolicy.AutoSize = True
        Me.lblEPolicy.Location = New System.Drawing.Point(1352, 47)
        Me.lblEPolicy.Name = "lblEPolicy"
        Me.lblEPolicy.Size = New System.Drawing.Size(58, 20)
        Me.lblEPolicy.TabIndex = 22
        Me.lblEPolicy.Text = "ePolicy"
        '
        'lblNBMPolicy
        '
        Me.lblNBMPolicy.AutoSize = True
        Me.lblNBMPolicy.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblNBMPolicy.Location = New System.Drawing.Point(1512, 25)
        Me.lblNBMPolicy.Name = "lblNBMPolicy"
        Me.lblNBMPolicy.Size = New System.Drawing.Size(88, 20)
        Me.lblNBMPolicy.TabIndex = 26
        Me.lblNBMPolicy.Text = "NBM Policy"
        Me.lblNBMPolicy.Visible = False
        '
        'txtEPolicy
        '
        Me.txtEPolicy.Location = New System.Drawing.Point(1427, 42)
        Me.txtEPolicy.Name = "txtEPolicy"
        Me.txtEPolicy.ReadOnly = True
        Me.txtEPolicy.Size = New System.Drawing.Size(74, 26)
        Me.txtEPolicy.TabIndex = 23
        '
        'lblAnnualRenewalPrem
        '
        Me.lblAnnualRenewalPrem.AutoSize = True
        Me.lblAnnualRenewalPrem.Location = New System.Drawing.Point(1528, 47)
        Me.lblAnnualRenewalPrem.Name = "lblAnnualRenewalPrem"
        Me.lblAnnualRenewalPrem.Size = New System.Drawing.Size(109, 20)
        Me.lblAnnualRenewalPrem.TabIndex = 24
        Me.lblAnnualRenewalPrem.Text = "Annual CCDR"
        '
        'txtAnnualRenewalPrem
        '
        Me.txtAnnualRenewalPrem.Location = New System.Drawing.Point(1728, 42)
        Me.txtAnnualRenewalPrem.Name = "txtAnnualRenewalPrem"
        Me.txtAnnualRenewalPrem.ReadOnly = True
        Me.txtAnnualRenewalPrem.Size = New System.Drawing.Size(74, 26)
        Me.txtAnnualRenewalPrem.TabIndex = 25
        '
        'notificationworker
        '
        Me.notificationworker.WorkerReportsProgress = True
        '
        'lblCompanyIdBanner
        '
        Me.lblCompanyIdBanner.AutoSize = True
        Me.lblCompanyIdBanner.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCompanyIdBanner.ForeColor = System.Drawing.Color.Black
        Me.lblCompanyIdBanner.Location = New System.Drawing.Point(1248, 10)
        Me.lblCompanyIdBanner.Name = "lblCompanyIdBanner"
        Me.lblCompanyIdBanner.Size = New System.Drawing.Size(0, 20)
        Me.lblCompanyIdBanner.TabIndex = 24
        '
        'frmPolicyAsur
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
        Me.ClientSize = New System.Drawing.Size(2250, 1499)
        Me.Controls.Add(Me.txtEPolicy)
        Me.Controls.Add(Me.lblAnnualRenewalPrem)
        Me.Controls.Add(Me.txtAnnualRenewalPrem)
        Me.Controls.Add(Me.lblEPolicy)
        Me.Controls.Add(Me.lblNBMPolicy)
        Me.Controls.Add(Me.lblCapsilPolNo)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtEstatement)
        Me.Controls.Add(Me.lblDateFormatMsg)
        Me.Controls.Add(Me.lblTAD2CPolicyMsg)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBillNo)
        Me.Controls.Add(Me.txtCNameChi)
        Me.Controls.Add(Me.txtCName)
        Me.Controls.Add(Me.lblAPL)
        Me.Controls.Add(Me.lblLastAPL)
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
        Me.Controls.Add(Me.lblCompanyIdBanner)
        Me.Name = "frmPolicyAsur"
        Me.Tag = "Policy Information"
        Me.Text = "frmPolicy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tcPolicy.ResumeLayout(False)
        Me.tabPolicySummary.ResumeLayout(False)
        Me.tabPolicySummary.PerformLayout()
        Me.tabUTRH.ResumeLayout(False)
        Me.tabUTRS.ResumeLayout(False)
        Me.tabUTRS.PerformLayout()
        Me.tabCoverageDetails.ResumeLayout(False)
        Me.tabDefaultPayoutMethodRegist.ResumeLayout(False)
        Me.tabFinancialInfo.ResumeLayout(False)
        Me.tabFinancialInfo.PerformLayout()
        Me.tabMClaimsHistory.ResumeLayout(False)
        Me.tabDISC.ResumeLayout(False)
        Me.tabPaymentHistory.ResumeLayout(False)
        Me.tabLevyHistory.ResumeLayout(False)
        Me.tabLevyHistory.PerformLayout()
        Me.tabARWHistory.ResumeLayout(False)
        Me.tabARWHistory.PerformLayout()
        Me.tabAnnuityPaymentHistory.ResumeLayout(False)
        Me.tabDirectCreditHistory.ResumeLayout(False)
        Me.tabCupGetTransResult.ResumeLayout(False)
        Me.tabAPLHistory.ResumeLayout(False)
        Me.tabAPLHistory.PerformLayout()
        Me.tabDCAR.ResumeLayout(False)
        Me.tabUnderwriting.ResumeLayout(False)
        Me.tabSMS.ResumeLayout(False)
        Me.tabPolicyAlt.ResumeLayout(False)
        Me.tabCouponHistory.ResumeLayout(False)
        Me.tabDDACCD.ResumeLayout(False)
        Me.tabCashFlow.ResumeLayout(False)
        Me.tabClaimsHistory.ResumeLayout(False)
        Me.tabClaimsHistory.PerformLayout()
        Me.tabTransactionHistory.ResumeLayout(False)
        Me.tabCustomerHistory.ResumeLayout(False)
        Me.tabServiceLog.ResumeLayout(False)
        Me.tabFundTrans.ResumeLayout(False)
        Me.tabPayOutHist.ResumeLayout(False)
        Me.tabPolicyNotes.ResumeLayout(False)
        Me.tabDesignatedPerson.ResumeLayout(False)
        Me.tabSubAcc.ResumeLayout(False)
        Me.tabParSur.ResumeLayout(False)
        Me.tabiLASSMS.ResumeLayout(False)
        Me.tabPostSalesCall.ResumeLayout(False)
        Me.tabPostSalesCall.PerformLayout()
        Me.tabTapNGo.ResumeLayout(False)
        Me.tabOePay.ResumeLayout(False)
        Me.tabAsurPolicySummary.ResumeLayout(False)
        Me.tabAsurServiceLog.ResumeLayout(False)
        Me.tabAsurServiceLog.PerformLayout()
        Me.tabIWSHistory.ResumeLayout(False)
        Me.tabTraditionalPartialSurrQuot.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public isAssurance As Boolean = False ' Added by Hugo Chan on 2021-05-14, "CRS - First Level of Access", declare this flag for skipping some life asia and capsil function calls

    Public isLifeAsia As Boolean = False
    Public isProposal As Boolean = False
    Public dtCashV() As DataTable
    Private dtPAYH, dtCOUH, dtAPLH, dtTRNH As DataTable
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private strPolicy, strCustID As String
    Private WithEvents w As New clsMQWorker
    Private dtPolSum, dtPolMisc, dtPolAddr, dtCoverage(1), dtCustHist, dtCCDR, dtDDA, dtDCAR, dtUWInf, dtNA As DataTable
    Private blnPOINFO As Boolean = False
    Private blnExit, blnSrvLogLoad, blnAgtInfoLoad, blnUWLoad, blnShowSrvLog, blnCanClose As Boolean
    Private strClientList, strHolderID As String
    Private datEnqFrom, datHstFrom As Date
    Public objMQQueHeader As Utility.Utility.MQHeader
    Public objDBHeader As Utility.Utility.ComHeader
    Private bPolicySummaryLoaded As Boolean = False
    Public strRelateCode As String = ""
    Public blnIsUHNWCustomer As Boolean = False
    ' To be executed in another thread
#If STRESS <> 0 Then
    Delegate Sub QExecDelegate(ByVal strPolicy As String, ByVal datEnqFrom As Date, ByVal datHstFrom As Date, ByVal strClientList As String, ByVal objWriter As IO.StreamWriter)
#Else
    Delegate Sub QExecDelegate(ByVal strPolicy As String, ByVal datEnqFrom As Date, ByVal datHstFrom As Date, ByVal strClientList As String)
#End If

    ' **** ES004 begin ****
    Private CapDate As String = ""

    ''' <summary>
    ''' Indicates whether this form will be loaded asynchronously
    ''' </summary>
    Public Property IsAsyncLoadForm As Boolean = False

    Private dsPolicyHead As DataSet = Nothing
    Private policySummaryDataDict As Dictionary(Of String, DataSet) = Nothing

    Private WithEvents frmSelCO As New frmSelectCO
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS
    Private objComHeader As Utility.Utility.ComHeader
    Private objPOSHeader As Utility.Utility.POSHeader
    Private objUtility As Utility.Utility
    Private SysEventLog As New SysEventLog.clsEventLog
    ' **** ES004 end ****

    Public blnCanView As Boolean = False

    ' UpdateUI delegate executed on main thread
    Delegate Sub UpdateUIDelegate(ByVal strFunc As String, ByVal strMsg As String)
    Dim dlgQExec As QExecDelegate = New QExecDelegate(AddressOf w.QueuedExec)

    'Public Property PolicyNo(ByVal strTitle As String, ByVal strLastName As String, ByVal strFirstName As String, _
    '        ByVal strChiName As String, ByVal strProduct As String, ByVal strStatus As String) As String
    Public Property PolicyAccountID(Optional ByVal blnShow As Boolean = False) As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strLastPolicy = Value
            strPolicy = Value
            blnShowSrvLog = blnShow
            Me.txtPolicy.Text = Value
            'If Not IsDBNull(strTitle) Then Me.txtTitle.Text = strTitle
            'If Not IsDBNull(strLastName) Then Me.txtLastName.Text = strLastName
            'If Not IsDBNull(strFirstName) Then Me.txtFirstName.Text = strFirstName
            'If Not IsDBNull(strChiName) Then Me.txtChiName.Text = strChiName
            'If Not IsDBNull(strProduct) Then Me.txtProduct.Text = strProduct
            'If Not IsDBNull(strStatus) Then Me.txtStatus.Text = strStatus

            'Alex Th Lee 20201013 [ITSR2281 - ePolicy]
            loadEPolicyFlag(Value) '
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

    Private Sub w_Finish(ByVal strFunc As String, ByVal lngErrNo As Long, ByVal strErrMsg As String, ByVal dt As System.Data.DataTable()) Handles w.Finish

        ' assgin return dt to corresponding dt object, called by worker thread
        If lngErrNo <> 0 Then

            ' VS2005 upgrade change start (move to updateUI())
            'wndMain.StatusBarPanel1.Text = strErrMsg
            'wndMain.Cursor = Cursors.Default
            ' VS2005 upgrade change end

#If STRESS <> 0 Then
            Call WriteLog("Error: " & strFunc & "-" & strErrMsg & "-" & Now, writer)
#End If
        End If

        If strFunc = "MAIN" Then Exit Sub

        Select Case strFunc
            Case cPOADDR
                dtPolAddr = dt(0)
            Case cCOINFO
                dtCoverage(0) = dt(0)
                dtCoverage(1) = dt(1)
            Case cDDA
                dtDDA = dt(0)
            Case cCCDR
                dtCCDR = dt(0)
            Case cHICL
                dtCustHist = dt(0)
            Case cUWINFO
                dtUWInf = dt(0)
            Case cPAYH
                dtPAYH = dt(0)
            Case cCOUHST
                dtCOUH = dt(0)
            Case cAPLH
                dtAPLH = dt(0)
            Case cORDUNA
                dtNA = dt(0)
            Case cTRNH
                dtTRNH = dt(0)
            Case cDCAR
                dtDCAR = dt(0)
        End Select

        ' update UI for corresponding tab
        Call UpdateUI(strFunc, strErrMsg)

    End Sub

    'oliver 2024-3-5 added for ITSR-5105 EasyTake Service Phase 2
    Private Sub CheckIsDesignatedPersonPolicy(ByVal policyNo As String)
        Try

            Dim dataTableGetMiscRole As DataTable = Nothing
            Dim wspos As POSWS.POSWS = POSWS_HK()
            dataTableGetMiscRole = wspos.getMiscRole(policyNo, "DP")
            If Not IsNothing(dataTableGetMiscRole) AndAlso dataTableGetMiscRole.Rows.Count > 0 Then
                MsgBox("Policy assigned ""Designated Person"" who could file a claim and access the living benefits on behalf of Policy Owner with valid medical certificates when Policy Owner is mentally / physically incapacitated ", MsgBoxStyle.Information, "Designated Person Policy Pop-Up Message")
            End If

        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try
    End Sub

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Sub CheckIsNBMPolicy(ByVal policyNo As String)
        Dim strErrRetentionCampaignEnquiry As String = ""
        Try
            Dim isNBMPolicy As Boolean = False
            Dim wspos As POSWS.POSWS = POSWS_HK()
            Dim dataTableRetentionCampaignEnquiry As DataTable = Nothing
            'isNBMPolicy = wspos.RetentionCampaignEnquiry(policyNo, "01", "01", "00", dataTableRetentionCampaignEnquiry, strErrRetentionCampaignEnquiry)
            isNBMPolicy = RetentionCampaignEnquiry(wspos, policyNo, dataTableRetentionCampaignEnquiry, strErrRetentionCampaignEnquiry)
            If isNBMPolicy Then
                If Not IsNothing(dataTableRetentionCampaignEnquiry) AndAlso dataTableRetentionCampaignEnquiry.Rows.Count > 0 Then
                    MsgBox(IIf(Not IsDBNull(dataTableRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails")), dataTableRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignDetails").ToString(), "NBM Retention Policy Pop-Up Message"), MsgBoxStyle.Information, "NBM Retention Policy Pop-Up Message")
                End If
                Me.lblNBMPolicy.Visible = True
                UclServiceLog1.IsNBMPolicy = True
                UclServiceLog1.ReturnDataTableRetentionCampaignEnquiry = dataTableRetentionCampaignEnquiry
            End If

        Catch ex As Exception
            HandleGlobalException(ex, ex.Message & Environment.NewLine & "RetentionCampaignEnquiry Return StrErr:" & strErrRetentionCampaignEnquiry)
        End Try
    End Sub

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Private Function RetentionCampaignEnquiry(ByVal wspos As POSWS.POSWS, ByVal policyNo As String, ByRef dataTableRetentionCampaignEnquiry As DataTable, ByRef strErrRetentionCampaignEnquiry As String) As Boolean
        Dim isRetentionCampaignEnquirySuccess As Boolean = False
        isRetentionCampaignEnquirySuccess = wspos.RetentionCampaignEnquiry(policyNo, "01", "01", "00", dataTableRetentionCampaignEnquiry, strErrRetentionCampaignEnquiry)
        If isRetentionCampaignEnquirySuccess Then
            If Not IsDBNull(dataTableRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignCode")) Then
                If Not String.IsNullOrEmpty(dataTableRetentionCampaignEnquiry.Rows(0).Item("RetentionCampaignCode").ToString) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
        Return False
    End Function

    ''' <remarks>
    ''' <br>20241113 Chrysan Cheng, CRS performer slowness</br>
    ''' </remarks>
    Private Sub RefreshPolicySummary(sender As Object, e As EventArgs)
        If dsPolicyHead Is Nothing OrElse dsPolicyHead.Tables.Count = 0 Then
            ' cache policy header data from PolicySummary page after it load data finish
            dsPolicyHead = Ctrl_CRSPolicyGeneral_Information1.dsCurrInUse
        End If

        ' re-enable PolicySummary page when load data finish
        Ctrl_CRSPolicyGeneral_Information1.Enabled = True
    End Sub

    ''' <summary>
    ''' Reload form data by async mode
    ''' </summary>
    ''' <param name="dsPolicyHeader">Policy header data</param>
    ''' <param name="dataDict">Other policy data dictionary</param>
    ''' <remarks>
    ''' <br>20241113 Chrysan Cheng, CRS performer slowness</br>
    ''' </remarks>
    Public Sub ReloadScreen(dsPolicyHeader As DataSet, dataDict As Dictionary(Of String, DataSet))
        Dim funcStartTime As Date = Now

        Me.dsPolicyHead = dsPolicyHeader
        Me.policySummaryDataDict = dataDict

        ' reload screen (if need)
        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyAsur", "frmPolicy_Load", "ReloadScreen.Begin", funcStartTime, Now, txtPolicy.Text, "", "")
        If Me.IsHandleCreated AndAlso Not Me.Disposing AndAlso Not Me.IsDisposed Then
            ' if blank form already loaded, reload data now, otherwise wait for the main thread to load itself
            LoadForm()

            ' re-visible Tab Controls
            Me.tcPolicy.Visible = True
        End If
        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyAsur", "frmPolicy_Load", "ReloadScreen.End", funcStartTime, Now, txtPolicy.Text, "", "")
    End Sub

    Private Sub frmPolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim funcStartTime As Date = Now

        If IsAsyncLoadForm AndAlso Me.dsPolicyHead Is Nothing Then
            ' Only display a disabled blank screen until the policy data is successfully retrieved
            Control.CheckForIllegalCrossThreadCalls = False ' disable non-main thread call check for global controls (Enable cross-thread access to control)
            Me.tcPolicy.Visible = False
            SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyAsur", "frmPolicy_Load", "frmPolicy_Load.Show", funcStartTime, Now, txtPolicy.Text, "", "")
        Else
            LoadForm()
        End If
    End Sub

    Private Sub LoadForm()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now

        'Oliver 2023-11-1 Added for CRS Enhancement(General Enhance Ph4) Point A - 10
        If Not String.IsNullOrEmpty(strPolicy) Then
            CheckIsExistCustomerAlertTableByPolicyNoToPrompt(strPolicy)
        End If
        'IUL check
        If Not String.IsNullOrEmpty(strPolicy) AndAlso (Not (CompanyID.Equals("LAC") Or CompanyID.Equals("LAH"))) Then
            If CheckIULAccess(strPolicy, g_Comp) Then Me.tcPolicy.Controls.Add(Me.tabIULControl)
        End If
        '

        'oliver 2024-3-5 added for ITSR-5105 EasyTake Service Phase 2
        If Not String.IsNullOrEmpty(strPolicy) Then
            CheckIsDesignatedPersonPolicy(strPolicy)
        End If

        'oliver 2023-12-25 added for ITSR5061 Retention Offer Campaign 
        If Not String.IsNullOrEmpty(strPolicy) Then
            CheckIsNBMPolicy(strPolicy)
        End If

        Dim isTAD2C As Boolean = False
        ''Check TA
        'Dim isTAPolicy As Boolean = False
        'If Not String.IsNullOrEmpty(strPolicy) Then
        '    Dim serverPrefix As String = g_CAM_Database
        '    Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "CHECK_TA_POLICY", New Dictionary(Of String, String)() From {
        '                                                    {"strInPolicy", strPolicy},
        '                                                    {"DbStart", serverPrefix}
        '                                                    })

        '    If Not retDs.Tables Is Nothing Then
        '        If retDs.Tables(0).Rows.Count > 0 Then
        '            isTAPolicy = True
        '        End If
        '    End If
        'End If

        ''Check D2C
        'Dim isD2CPolicy As Boolean = False
        'If Not String.IsNullOrEmpty(strPolicy) Then
        '    Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "CHECK_D2C_POLICY", New Dictionary(Of String, String)() From {
        '                                                    {"strInPolicy", strPolicy}
        '                                                    })

        '    If Not retDs.Tables Is Nothing Then
        '        If retDs.Tables(0).Rows.Count > 0 Then
        '            isD2CPolicy = True
        '        End If
        '    End If
        'End If

        'If isTAPolicy And isD2CPolicy Then
        '    'MsgBox("This is TA D2C policy", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
        '    Me.lblTAD2CPolicyMsg.Visible = True
        '    isTAD2C = True
        'End If

        If LoadMsgAndLabelD2C() = "Y" Then
            isTAD2C = True
        End If

        LoadMsgAndLabel()

        If (CompanyID.Equals("LAC") Or CompanyID.Equals("LAH")) Then
            txtAnnualRenewalPrem.Text = ""
        Else
            txtAnnualRenewalPrem.Text = IIf(GetRenewalPaymentMethodValidation(strPolicy), "Y", "N")
            If txtAnnualRenewalPrem.Text = "Y" Then
                MsgBox("This policy accepts the renewal yearly premium paid by credit card autopay", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            End If
        End If

        If blnIsUHNWCustomer Then
            InsertVVIPLog(strPolicy, strCustID, strRelateCode, "Policy Summary (HK)", isUHNWMember)
        End If

        If CompanyID.Equals("ING") Then
            lblCompanyIdBanner.Text = "Bermuda"
        ElseIf CompanyID.Equals("LAC") Or CompanyID.Equals("LAH") Then
            lblCompanyIdBanner.Text = "Assurance"
            'oliver 2024-7-31 added for Com 6
        ElseIf CompanyID.Equals("BMU") Then
            lblCompanyIdBanner.Text = "Private"
            lblCompanyIdBanner.BackColor = ColorTranslator.FromHtml("#33CC33")
            If Me.tcPolicy.Controls.Contains(Me.tabLevyHistory) Then Me.tcPolicy.Controls.Remove(Me.tabLevyHistory) ' ITSR4602-159, No Levy History Tab for BMU
        End If

        ' Added by Hugo Chan on 2021-05-14, "CRS - First Level of Access", display Assurance policy
        If isAssurance Then
            ShowAssuranceTabs()
            LoadAssuranceInformation()
            Exit Sub
        End If

        Try
            Dim dtPolList As DataTable

            ' CRS performer slowness - Reuse BusinessDate to reduce the number of calls
            Dim dtBusDate As Date = Today   ' use today if fail

            'SL20191121 - Start
            If GetBusinessDate(dtBusDate) Then
                If GetQDAPRefundExtraPaymentAlert(CompanyID, strPolicy, "", dtBusDate, strErr) Then
                    Dim strAlertMsg As String = String.Empty
                    strAlertMsg += "Refund 50% unearned premium as surrender benefit if policy is full surrender within the first policy year"
                    MessageBox.Show(strAlertMsg, "Refund Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
            'SL20191121 - End

            If CustomerBL.GetPolicyHolderId(CompanyID, strPolicy, strCustID, strErr) Then
                If BadAddrWarning(strCustID) Then
                    Dim strWarnMsg As String = String.Empty
                    strWarnMsg += "Customer has bad address record. Pleae remind customer to:" & vbNewLine
                    strWarnMsg += "-Submit address proof of existing address; or" & vbNewLine
                    strWarnMsg += "-Update the new address information by submitting a completed address change form with signature that matches company record."
                    MessageBox.Show(strWarnMsg, "Bad Address Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If


            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.WaitCursor

            blnCanClose = False

            ' *** Get Policy Summary first ***

            ' Initialize policy note
            PolicyNote1.DataAdaptor.ComHeader = objDBHeader

#If STRESS <> 0 Then
        Call WriteLog("Start: " & Now, writer)
        Dim ts, tf As TimeSpan
        ts = New TimeSpan(Now.Ticks)
#End If

            'Dim blnCanView = False ' move to class variable
            Dim strUnit As String
            Dim strEstatement As String = ""
            Dim clsCommon As New LifeClientInterfaceComponent.CommonControl()

            clsCommon.MQHeader = Me.objMQQueHeader
            clsCommon.ComHeader = Me.objDBHeader

            If Not clsCommon.GetEStatementIndicatorByPolicyNo(strPolicy, strEstatement, strErr) Then
                'Throw New Exception(strErr)
            End If

            txtEstatement.Text = strEstatement

            If isLifeAsia = False Then
                'dtPolList = objCS.GetPolicySummary(strPolicy, lngErr, strErr)
                dtPolSum = objCS.GetPolicySummary(strPolicy, lngErr, strErr)
                'oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
                'PolicySummary1.Visible = True
                AddressSelect1.Visible = True

                Ctrl_CRSPolicyGeneral_Information1.Visible = False

                ' **** ES005 begin ****
                txtBillNo.Visible = False
                Label5.Visible = False
                ' **** ES005 end ****
            Else

                ' Remove the icon for LA policy
                Me.tabAPLHistory.ImageIndex = -1
                Me.tabPolicySummary.ImageIndex = -1
                Me.tabTapNGo.ImageIndex = -1
                Me.tabOePay.ImageIndex = -1 'KT20180726
                Me.tabCoverageDetails.ImageIndex = -1
                Me.tabPaymentHistory.ImageIndex = -1
                'LH1507004  Journey Annuity Day 2 Phase 2 Start
                Me.tabAnnuityPaymentHistory.ImageIndex = -1
                Me.tabDirectCreditHistory.ImageIndex = -1
                Me.tabCupGetTransResult.ImageIndex = -1
                'LH1507004  Journey Annuity Day 2 Phase 2 End
                Me.tabDCAR.ImageIndex = -1
                Me.tabDDACCD.ImageIndex = -1
                Me.tabCouponHistory.ImageIndex = -1
                Me.tabCustomerHistory.ImageIndex = -1

                Dim ds As New DataSet
                getLifeAsiaInfo(strPolicy, dtBusDate, ds)

                If ds.Tables.Count > 0 Then
                    dtPolSum = ds.Tables("POLINF").Copy
                    dtNA = ds.Tables("ORDUNA").Copy

                    ' Check authority to view the channel
                    If Not strSK Is Nothing AndAlso strSK.Length <> 0 Then
                        strUnit = GetLocation(Strings.Right(dtPolSum.Rows(0).Item("POAGCY"), 5))
                        For s As Integer = 0 To strSK.Length - 1
                            If Strings.Left(strUnit, Strings.Len(strSK(s))) = strSK(s) Then
                                blnCanView = True
                            End If
                        Next
                    Else
                        blnCanView = True
                    End If

                    'Levy Hard Code
                    'blnCanView = True

                    If Not blnCanView Then
                        MsgBox("You can only view policies written by the channel you are supporting.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Policy Information")
                        blnExit = True
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    If isProposal = True Then

                        Ctrl_CRSPolicyGeneral_Information1.Visible = False
                        'oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
                        'PolicySummary1.Visible = False
                        AddressSelect1.Visible = False

                        tcPolicy.SelectTab("tabPolicySummary")
                        CheckUPSAccess("Policy Summary")
                        tcPolicy.SelectedIndex = 0
                        lblPolicyNo.Visible = False

                    ElseIf bPolicySummaryLoaded = False Then
                        lblPolicyNo.Text = RTrim(strPolicy)
                        lblPolicyNo.Visible = True
                        Ctrl_CRSPolicyGeneral_Information1.Visible = True
                        'oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
                        'PolicySummary1.Visible = False
                        AddressSelect1.Visible = False
                        Ctrl_CRSPolicyGeneral_Information1.dbHeader = Me.objDBHeader
                        Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
                        Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = RTrim(strPolicy)
                        Ctrl_CRSPolicyGeneral_Information1.SystemInUse = "CRS"
                        Ctrl_CRSPolicyGeneral_Information1.IsTAD2C = isTAD2C
                        Ctrl_CRSPolicyGeneral_Information1.UHNWRightFlag = isUHNWMember
                        Ctrl_CRSPolicyGeneral_Information1.Enabled = False  ' disabled until load data finish

                        ' add a subscription to the LoadDataCompleted event
                        AddHandler Ctrl_CRSPolicyGeneral_Information1.LoadDataCompleted, AddressOf RefreshPolicySummary

                        ' set policy data externally (if have)
                        Ctrl_CRSPolicyGeneral_Information1.SetPolicyDataForAsync(Me.dsPolicyHead, Me.policySummaryDataDict)

                        strFuncStartTime = Now
                        Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()
                        strFuncEndTime = Now
                        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo", strFuncStartTime, strFuncEndTime, txtPolicy.Text, "", "")

                        strFuncStartTime = Now
                        tcPolicy.SelectTab("tabPolicySummary")
                        CheckUPSAccess("Policy Summary")
                        tcPolicy.SelectedIndex = 0
                        strFuncEndTime = Now
                        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "CheckUPSAccess", strFuncStartTime, strFuncEndTime, "", "", "")

                        bPolicySummaryLoaded = True
                    End If

                    'Tap & Go tab KT20161111
                    strFuncStartTime = Now
                    If Me.Ctrl_CRSPolicyGeneral_Information1.dbHeader.CompanyID = "ING" Or Me.Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader.CompanyID = "ING" Then
                        If isTapNGoProduct(strPolicy) Then
                            If CheckUPSAccess("Policy Summary") Then
                                Me.tcPolicy.Controls.Add(Me.tabTapNGo)
                                Ctrl_TapNGo1.dbHeader = Me.objDBHeader
                                Ctrl_TapNGo1.MQQueuesHeader = Me.objMQQueHeader
                                Ctrl_TapNGo1.dbHeader = Me.objDBHeader
                                Ctrl_TapNGo1.PolicyNoInUse = RTrim(strPolicy)
                                Ctrl_TapNGo1.showTapNGoInfo()
                            End If
                        End If
                    End If
                    strFuncEndTime = Now
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "Ctrl_TapNGo", strFuncStartTime, strFuncEndTime, "", "", "")

                    'KT20170726
                    strFuncStartTime = Now
                    If Me.Ctrl_CRSPolicyGeneral_Information1.dbHeader.CompanyID = "ING" Or Me.Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader.CompanyID = "ING" Then
                        If isOepayProduct(strPolicy) Then
                            If CheckUPSAccess("Policy Summary") Then
                                Me.tcPolicy.Controls.Add(Me.tabOePay)
                                Ctrl_OePay1.dbHeader = Me.objDBHeader
                                Ctrl_OePay1.MQQueuesHeader = Me.objMQQueHeader
                                Ctrl_OePay1.dbHeader = Me.objDBHeader
                                Ctrl_OePay1.PolicyNoInUse = RTrim(strPolicy)
                                Ctrl_OePay1.showOePayInfo()
                            End If
                        End If
                    End If
                    strFuncEndTime = Now
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "Ctrl_OePay", strFuncStartTime, strFuncEndTime, "", "", "")

                    ' **** ES005 begin ****
                    'Prompt policy alert for LA policy
                    strFuncStartTime = Now
                    If CheckUPSAccess("Cash Flow") Then tcPolicy.Controls.Add(Me.tabCashFlow)
                    strFuncEndTime = Now
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "tabCashFlow", strFuncStartTime, strFuncEndTime, "", "", "")

                    'strFuncStartTime = Now
                    'Call PolicyAlert()
                    'strFuncEndTime = Now
                    'SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "PolicyAlert", strFuncStartTime, strFuncEndTime, "", "", "")
                    strFuncStartTime = Now
                    txtBillNo.Text = GetBillNo()
                    strFuncEndTime = Now
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "GetBillNo", strFuncStartTime, strFuncEndTime, "", "", "")

                    'ITSR3488 - CL20220928 - start
                    Call IsAMCClaimValid(txtPolicy.Text.Trim, dtBusDate)
                    'ITSR3488 - CL20220928 - end
                    notificationworker.RunWorkerAsync()

                    ' VIP flag
                    strFuncStartTime = Now
                    If GetVIPStatus(0, strPolicy) = "1" Then
                        Me.txtCName.BackColor = System.Drawing.Color.Orange
                        Me.txtCNameChi.BackColor = System.Drawing.Color.Orange
                        Me.txtTitle.BackColor = System.Drawing.Color.Orange
                        Me.txtLastName.BackColor = System.Drawing.Color.Orange
                        Me.txtFirstName.BackColor = System.Drawing.Color.Orange
                        Me.txtChiName.BackColor = System.Drawing.Color.Orange
                    End If
                    strFuncEndTime = Now
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "GetVIPStatus", strFuncStartTime, strFuncEndTime, "", "", "")
                    ' **** ES005 end ****

                    wndMain.Cursor = Cursors.Default
                    'lngErr = 0
                    'strErr = ""
                    Exit Sub
                End If

            End If
            'end update by kit
#If STRESS <> 0 Then
        tf = New TimeSpan(Now.Ticks)
        tf = tf.Subtract(ts)
        Call WriteLog("POLI-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If

            If lngErr = 0 Then
                dtPolSum.TableName = "POLINF"
                dtPolMisc = objCS.GetPolicyMisc(strPolicy, lngErr, strErr)

                If lngErr = 0 Then
                    dtPolMisc.TableName = "POLMISC"
                Else
                    MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                End If
            Else
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If



            If lngErr = 0 Then
                blnPOINFO = True
            End If

            If Not dtPolSum Is Nothing AndAlso Not dtPolMisc Is Nothing Then

                With dtPolSum.Columns
                    .Add("PAGCO1", Type.GetType("System.String"))
                    .Add("PAGCO2", Type.GetType("System.String"))
                    .Add("PAGCO3", Type.GetType("System.String"))
                    .Add("POFR15", Type.GetType("System.String"))
                    .Add("ProductID", Type.GetType("System.String"))
                    .Add("POIYY", Type.GetType("System.Decimal"))
                    .Add("PolicyEffDate", Type.GetType("System.DateTime"))
                End With

                If dtPolSum.Rows.Count > 0 And dtPolMisc.Rows.Count > 0 Then

                    dtPolSum.Rows(0).Item("PAGCO1") = dtPolMisc.Rows(0).Item("PAGCO1")
                    dtPolSum.Rows(0).Item("PAGCO2") = dtPolMisc.Rows(0).Item("PAGCO2")
                    dtPolSum.Rows(0).Item("PAGCO3") = dtPolMisc.Rows(0).Item("PAGCO3")
                    dtPolSum.Rows(0).Item("POFR15") = dtPolMisc.Rows(0).Item("POFR15")
                    dtPolSum.Rows(0).Item("ProductID") = dtPolMisc.Rows(0).Item("ProductID")
                    dtPolSum.Rows(0).Item("POIYY") = dtPolMisc.Rows(0).Item("POIYY")
                    dtPolSum.Rows(0).Item("PolicyEffDate") = dtPolMisc.Rows(0).Item("PolicyEffDate")

                    ' Check if user can view this channel
                    If Not strSK Is Nothing AndAlso strSK.Length <> 0 Then
                        strFuncStartTime = Now
                        strUnit = GetLocation(Strings.Right(dtPolSum.Rows(0).Item("POAGCY"), 5))
                        strFuncEndTime = Now
                        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "CheckChannel", strFuncStartTime, strFuncEndTime, "", "", "")
                        For s As Integer = 0 To strSK.Length - 1
                            If Strings.Left(strUnit, Strings.Len(strSK(s))) = strSK(s) Then
                                blnCanView = True
                            End If
                        Next
                    Else
                        blnCanView = True
                    End If

                    'Levy Hard Code
                    'blnCanView = True

                    If Not blnCanView Then
                        MsgBox("You can only view policies written by the channel you are supporting.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Policy Information")
                        blnExit = True
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    If dtPolSum.Rows(0).Item("BillingType") = "9" Then
                        lblAPL.Visible = False
                        lblLastAPL.Visible = True
                    Else
                        lblLastAPL.Visible = False
                    End If

                    If dtPolSum.Rows(0).Item("POAPOP") = "S" Then
                        lblAPL.Visible = True
                        lblLastAPL.Visible = False
                    Else
                        lblAPL.Visible = False
                    End If


                    'dtPolSum.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
                    'dtPolSum.DefaultView.RowFilter = "PolicyRelateCode = 'O'"
                    'drs = dtPolSum.DefaultView.Item(0)

                    ' Get effective date and calculate the date range for enquiry function
                    'datEnqFrom = dtPolSum.Rows(0).Item("PaidToDate")
                    'If dtPolSum.Rows(0).Item("BillToDate") > datEnqFrom Then
                    '    datEnqFrom = dtPolSum.Rows(0).Item("BillToDate")
                    'End If

                    'AC - Change to use configuration setting - start
                    'If UAT <> 0 Then
                    '    datEnqFrom = #1/1/2007#
                    'Else
                    '    datEnqFrom = Today
                    'End If
                    datEnqFrom = Today
                    'AC - Change to use configuration setting - start

                    'datHstFrom = dtPolSum.Rows(0).Item("PolicyEffDate")
                    datHstFrom = dtPolMisc.Rows(0).Item("PolicyEffDate")

                    '#If UAT = 1 Then
                    '                datEnqFrom = #12/31/2005#
                    '#End If

                    ' Get Client No. list
                    Dim i, j As Integer

                    'strClientList = "'-'"
                    With dtPolSum.Rows(0)
                        strClientList = "'00000" & Strings.Right(.Item("POAGCY"), 5) & "'"
                        strClientList &= ",'00000" & Strings.Right(.Item("POPAGT"), 5) & "'"
                        strClientList &= ",'00000" & Strings.Right(.Item("POWAGT"), 5) & "'"
                    End With
                    With dtPolMisc.Rows(0)
                        strClientList &= ",'00000" & Strings.Right(.Item("PAGCO1"), 5) & "'"
                        strClientList &= ",'00000" & Strings.Right(.Item("PAGCO2"), 5) & "'"
                        strClientList &= ",'00000" & Strings.Right(.Item("PAGCO3"), 5) & "'"
                    End With

                    'For i = 0 To dtPolSum.Rows.Count - 1
                    '    With dtPolSum.Rows(i)
                    '        strClientList &= ",'" & .Item("ClientID") & "'"
                    '        If .Item("PolicyRelateCode") = "PH" Then
                    '            strHolderID = .Item("ClientID")
                    '        End If
                    '        If .Item("FLD0005") > "01" Then
                    '            .Delete()
                    '        End If
                    '    End With
                    'Next
                    Dim strCheck As String
                    'Dim intMinCO As Integer

                    'intMinCO = dtPolMisc.Rows(0).Item("COTRAI")

                    For i = 0 To dtPolMisc.Rows.Count - 1
                        With dtPolMisc.Rows(i)
                            If .Item("COTRAI") = 1 Then
                                strClientList &= ",'" & .Item("ClientID") & "'"
                                If .Item("PolicyRelateCode") = "PH" Then
                                    strHolderID = .Item("ClientID")
                                End If
                            Else
                                'Exit For
                            End If
                            If .Item("PolicyRelateCode") = "PI" Then
                                If InStr(strCheck, .Item("ClientID")) > 0 Then
                                    dtPolMisc.Rows(i).Delete()
                                Else
                                    strCheck &= .Item("ClientID") & ","
                                End If
                            End If
                        End With
                    Next
                    dtPolMisc.AcceptChanges()

                    ' run worker thread and free the UI
#If STRESS <> 0 Then
                dlgQExec.BeginInvoke(strPolicy, datEnqFrom, datHstFrom, strClientList, writer, AddressOf CallBack, dlgQExec)
#Else
                    dlgQExec.BeginInvoke(strPolicy, datEnqFrom, datHstFrom, strClientList, AddressOf CallBack, dlgQExec)
#End If
                    '''''Dim drs As DataRowView
                    '''''dtNA = objCS.GetORDUNA(strClientList, lngErr, strErr)

                    '''''If lngErr = 0 Then
                    '''''    dtNA.DefaultView.RowFilter = "ClientID = '" & strHolderID & "'"
                    '''''    drs = dtNA.DefaultView.Item(0)
                    '''''    strCustID = drs.Item("CustomerID")
                    '''''    Me.CustomerID = strCustID
                    '''''Else
                    '''''    MsgBox(strErr)
                    '''''    Exit Sub
                    '''''End If
                    '''''dtPolAddr = dtNA.Copy
                    '''''dtPolAddr.DefaultView.RowFilter = "ClientID = '" & strHolderID & "'"
                    '''''dtPolAddr.TableName = "CustomerAddress"
                    '''''AddressSelect1.srcDTAddr = dtPolAddr

                    ''''''With drs
                    ''''''    If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")
                    ''''''    If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                    ''''''    If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                    ''''''    If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")
                    ''''''    If Not IsDBNull(.Item("Description")) Then Me.txtProduct.Text = .Item("Description")
                    ''''''    If Not IsDBNull(.Item("AccountStatusCode")) Then Me.txtStatus.Text = GetAcStatus(.Item("AccountStatusCode"))
                    ''''''End With
                    '''''With drs
                    '''''    If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")
                    '''''    If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                    '''''    If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                    '''''    If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")
                    '''''End With
                    With dtPolMisc.Rows(0)
                        If Not IsDBNull(.Item("Description")) Then Me.txtProduct.Text = .Item("Description")
                    End With
                    With dtPolSum.Rows(0)
                        ' for VPO policy, check if it is basic or whole
                        Dim strAcSts As String
                        strAcSts = .Item("AccountStatusCode")
                        If Not IsDBNull(strAcSts) Then
                            If strAcSts = "V" Then
                                For i = 0 To dtPolMisc.Rows.Count - 1
                                    If dtPolMisc.Rows(i).Item("CStatus") <> "V" Then
                                        strAcSts = "X"
                                        Exit For
                                    End If
                                Next
                            End If
                            Me.txtStatus.Text = GetAcStatus(strAcSts)
                        End If

                        ' Flora Leung, Project Leo Goal 3 Capsil, 14-Feb-2012 Start
                        Dim strSubSts As String = String.Empty
                        strSubSts = .Item("SubStatus")
                        If Not IsDBNull(strAcSts) And Not IsDBNull(strSubSts) Then
                            If strAcSts.Trim & strSubSts.Trim = "2W" Then
                                ' Me.txtStatus.Text = "Payor Waive (MCI)"
                                Me.txtStatus.Text = GetCodeTableValue("CRS_2W")
                            End If
                        End If
                        ' Flora Leung, Project Leo Goal 3 Capsil, 14-Feb-2012 End
                    End With
                    '''''' Other black box, process in turn
                    '''''PolicySummary1.PolicyAccountID = strPolicy
                    '''''PolicySummary1.srcDTPolSum(dtNA.Copy) = dtPolSum

                    'Prompt policy alert
                    'Call PolicyAlert()

                    'ITSR2408 - SL20210625 - Start
                    'Call GetMediSavingAmount(txtPolicy.Text, True)
                    'ITSR2408 - SL20210625 - End
                    notificationworker.RunWorkerAsync()

                    ' VIP flag
                    If GetVIPStatus(0, strPolicy) = "1" Then
                        Me.txtCName.BackColor = System.Drawing.Color.Orange
                        Me.txtCNameChi.BackColor = System.Drawing.Color.Orange
                        Me.txtTitle.BackColor = System.Drawing.Color.Orange
                        Me.txtLastName.BackColor = System.Drawing.Color.Orange
                        Me.txtFirstName.BackColor = System.Drawing.Color.Orange
                        Me.txtChiName.BackColor = System.Drawing.Color.Orange
                    End If

                Else
                    MsgBox("Policy " & strPolicy & " not found, please input again.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Policy Information")
                    blnExit = True
                    wndMain.Cursor = Cursors.Default
                End If
            Else
                MsgBox("Policy " & strPolicy & " not found, please input again.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Policy Information")
                blnExit = True
                wndMain.Cursor = Cursors.Default
            End If


        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "", strMainStartTime, strMainEndTime, strPolicy, "", "")
            '20171207 Levy Project
            wndMain.Cursor = Cursors.Default
        End Try

    End Sub

    Private Function isOepayProduct(ByVal strPolicyId As String) As Boolean
        Dim isOepay As Boolean = False
        Dim policyProduct As String = String.Empty
        Dim sqlConn As New SqlConnection
        Dim strSQL As String

        sqlConn.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlConn.Open()
        strSQL = "select productid from policyaccount where policyaccountid='" + strPolicyId + "'"
        Dim sqlCmd = New SqlCommand(strSQL, sqlConn)
        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            If sqlReader.Read() Then
                If Not IsDBNull(sqlReader.Item("ProductId")) Then
                    policyProduct = sqlReader.Item("ProductId").ToString.Trim
                End If
            End If
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()


        If policyProduct = "MIS1" Then
            isOepay = True
        End If


        Return isOepay


    End Function

    Private Function isTapNGoProduct(ByVal strPolicyId As String) As Boolean
        Dim isTNG As Boolean = False
        Dim prodId() As String
        Dim tngSupportedProductId As String = String.Empty
        Dim policyProduct As String = String.Empty
        Dim sqlConn As New SqlConnection
        Dim strSQL As String
        sqlConn.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlConn.Open()
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        If CompanyID = "LAC" OrElse CompanyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf CompanyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If
        strSQL = "select value from " & serverPrefix & " CodeTable where Code = 'TNG_PRODID'"
        Dim sqlCmd As New SqlCommand(strSQL, sqlConn)
        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            If sqlReader.Read() Then
                tngSupportedProductId = sqlReader.Item("value")
            End If
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()

        prodId = tngSupportedProductId.ToString.Split(",")

        sqlConn.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlConn.Open()
        strSQL = "select productid from policyaccount where policyaccountid='" + strPolicyId + "'"
        sqlCmd = New SqlCommand(strSQL, sqlConn)
        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            If sqlReader.Read() Then
                If Not IsDBNull(sqlReader.Item("ProductId")) Then
                    policyProduct = sqlReader.Item("ProductId").ToString.Trim
                End If
            End If
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()


        For Each product As String In prodId
            If policyProduct = product.Trim Then
                isTNG = True
                Exit For
            End If
        Next


        Return isTNG


    End Function

    'PolicyAlert() - Prompt policy alert for the policy
    Private Sub PolicyAlert()
        Dim strAlertTitle As String
        Dim strSQL As String
        Dim sqlConn As New SqlConnection
        Dim dtBDay As DateTime
        Dim strCSR As String

        'ITSR933 FG R3 CE Start
        'Dim strPolicyCap As String = GetCapsilPolicyNo(strPolicy)
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        sqlConn.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        If CompanyID = "LAC" OrElse CompanyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf CompanyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If

        sqlConn.Open()
        strSQL = "Select s.AlertNotes, s.EventInitialDatetime, s.MasterCSRID, cs.Name " &
                 "From ServiceEventDetail s left outer join " & serverPrefix & "CSR cs " &
                 "on s.MasterCSRID = cs.CSRID " &
                 "Where s.policyaccountid in ('" & RTrim(strPolicy) & "') and s.policyaccountid <> '' and s.IsPolicyAlert = 'Y' " &
                 "Order by s.EventInitialDatetime desc "
        Dim sqlCmd As New SqlCommand(strSQL, sqlConn)
        'ITSR933 FG R3 CE End

        ' 7/17/2006 - increase timeout period
        sqlCmd.CommandTimeout = gQryTimeOut
        ' End

        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            While sqlReader.Read()
                If IsDBNull(sqlReader.Item("Name")) Then
                    strCSR = "unknown user (" & sqlReader.Item("MasterCSRID") & ")"
                Else
                    strCSR = sqlReader.Item("Name")
                End If
                strAlertTitle = "Alert from " & strCSR & " on " & Format(sqlReader.Item("EventInitialDatetime"), "dd MMM yyyy") & ". "
                MsgBox(sqlReader.Item("AlertNotes"), MsgBoxStyle.Information, strAlertTitle)
            End While
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()

    End Sub

    Private Sub CallBack(ByVal ar As IAsyncResult)

        ' Retrieve the delegate.
        Dim dlgt As QExecDelegate = CType(ar.AsyncState, QExecDelegate)

        ' Call EndInvoke to retrieve the results.
        dlgt.EndInvoke(ar)
        blnCanClose = True

#If STRESS <> 0 Then
        Call WriteLog("Finish: " & Now, writer)
        'writer.Close()
#End If

    End Sub

    Public Sub UpdateUI(ByVal strFunc As String, ByVal strMsg As String)

        ' If call from worker thread, switch to UI thread

        If Me.InvokeRequired Then
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' ''If isLifeAsia Then
            '' ''    Ctrl_CRSPolicyGeneral_Information1.Visible = True
            '' ''    PolicySummary1.Visible = False
            '' ''    AddressSelect1.Visible = False
            '' ''    Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
            '' ''    Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = rtrim(strpolicy)
            '' ''    Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()
            '' ''Else
            '' ''    Ctrl_CRSPolicyGeneral_Information1.Visible = False
            '' ''    PolicySummary1.Visible = True
            '' ''    AddressSelect1.Visible = True
            '' ''End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim args() As Object = {strFunc, strMsg}
            Dim dlgUI As UpdateUIDelegate = New UpdateUIDelegate(AddressOf UpdateUI)
            Me.Invoke(dlgUI, args)
        Else
            ' Wait till POINFO finish
            While blnPOINFO = False
                Application.DoEvents()
            End While

            ' update UI

            ' VS2005 upgrade add - start
            If strMsg <> "" Then
                wndMain.StatusBarPanel1.Text = strMsg
            End If
            wndMain.Cursor = Cursors.Default
            ' VS2005 update add - end

            Select Case strFunc
                Case cORDUNA

                    '''' Temp solution to duplicate NA record problem!
                    '''' Add DISTINCT to COM to solve the problem later
                    '''Dim strCheck As String
                    '''For k As Integer = 0 To dtNA.Rows.Count - 1
                    '''    If InStr(strCheck, dtNA.Rows(k).Item("ClientID")) > 0 Then
                    '''        dtNA.Rows(k).Delete()
                    '''    Else
                    '''        strCheck &= dtNA.Rows(k).Item("ClientID") & ","
                    '''    End If
                    '''Next
                    '''dtNA.AcceptChanges()
                    ' Get Chi Info from CIW temp.
                    GetChiAddr(dtNA)

                    Dim drs As DataRowView
                    dtPolAddr = dtNA.Copy
                    dtPolAddr.DefaultView.RowFilter = "ClientID = '" & strHolderID & "'"
                    If dtPolAddr.DefaultView.Count = 0 Then
                        MsgBox("Customer Information not found - " & strHolderID, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        Exit Sub
                    End If
                    drs = dtPolAddr.DefaultView.Item(0)
                    strCustID = drs.Item("CustomerID")
                    Me.CustomerID = strCustID
                    dtPolAddr.TableName = "CustomerAddress"

                    Dim i As Integer
                    Dim blnBadAddr As Boolean = False

                    For i = 0 To dtPolAddr.Rows.Count - 1


                        'MsgBox(dtPolAddr.Rows(i).Item("clientid") & "-" & dtPolAddr.Rows(i).Item("badaddress"))

                        dtPolAddr.Rows(i).Item("AddressTypeCode") = "C"

                        ' Add 11/29/2006 - Display popup message if marked bad address
                        If dtPolAddr.Rows(i).Item("ClientID") = strHolderID Then
                            If dtPolAddr.Rows(i).Item("BadAddress") = "Y" Then blnBadAddr = True
                        End If
                        ' End Add
                    Next
                    AddressSelect1.srcDTAddr(True) = dtPolAddr

                    ' Add 11/29/2006 - Display popup message if marked bad address
                    If blnBadAddr = True Then
                        MsgBox("Bad address marked for policy " & strPolicy & ", please check with customer.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gSystem)
                    End If
                    ' End Add

                    txtCName.Visible = False
                    txtCNameChi.Visible = False
                    txtTitle.Visible = False
                    txtLastName.Visible = False
                    txtFirstName.Visible = False
                    txtChiName.Visible = False

                    With drs
                        If Not IsDBNull(.Item("Gender")) AndAlso .Item("Gender") = "C" Then
                            txtCName.Visible = True
                            txtCNameChi.Visible = True
                            If Not IsDBNull(.Item("CoName")) Then Me.txtCName.Text = .Item("CoName")
                            If Not IsDBNull(.Item("CoCName")) Then Me.txtCNameChi.Text = .Item("CoCName")

                            ' Add 11/29/2006 - VIP flag
                            If GetVIPStatus(strCustID) = "1" Then
                                Me.txtCName.BackColor = System.Drawing.Color.Orange
                                Me.txtCNameChi.BackColor = System.Drawing.Color.Orange
                            End If
                            ' End Add

                        Else
                            txtTitle.Visible = True
                            txtLastName.Visible = True
                            txtFirstName.Visible = True
                            txtChiName.Visible = True
                            If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")
                            If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                            If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                            If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")

                            ' Add 11/29/2006 - VIP flag
                            If GetVIPStatus(strCustID) = "1" Then
                                Me.txtTitle.BackColor = System.Drawing.Color.Orange
                                Me.txtLastName.BackColor = System.Drawing.Color.Orange
                                Me.txtFirstName.BackColor = System.Drawing.Color.Orange
                                Me.txtChiName.BackColor = System.Drawing.Color.Orange
                            End If
                            ' End Add

                        End If
                    End With

#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    lblPolicyNo.Visible = False
                    Ctrl_CRSPolicyGeneral_Information1.Visible = False
                    'oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
                    'PolicySummary1.Visible = True
                    AddressSelect1.Visible = True

                    '20171205 Levy
                    'PolicySummary1.MQQueuesHeader = Me.objMQQueHeader
                    'PolicySummary1.DBHeader = Me.objDBHeader

                    'PolicySummary1.PolicyAccountID = strPolicy
                    'PolicySummary1.srcDTPolSum(dtNA.Copy, dtPolMisc.Copy) = dtPolSum

                    FillNegativeCashValueReminder() 'ITDCBT20150115 NegativeCashValue

#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("POLI-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
                    Me.tabPolicySummary.ImageIndex = -1
                    Me.tabTapNGo.ImageIndex = -1

                    ' Jump to Service log page
                    ' Check if user has authority to view the page first!
                    If CheckUPSAccess("Service Log") Then
                        If blnShowSrvLog Then tcPolicy.SelectedTab = tabServiceLog
                    End If


                Case cPOADDR
                    'AddressSelect1.srcDTAddr = dtPolAddr


                Case cCOINFO
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    Coverage1.Visible = True
                    Ctrl_ChgComponent1.Visible = False
                    dtCoverage(0).TableName = "Coverage"
                    dtCoverage(1).TableName = "POLIA"
                    If Not dtCoverage Is Nothing AndAlso dtCoverage(0).Rows.Count > 0 Then
                        Coverage1.PolicyAccountID = strPolicy
                        Coverage1.srcDTCov(dtPolSum.Rows(0).Item("CurModeFactor"), dtNA.Copy, dtPolMisc.Copy, dtCoverage(1)) = dtCoverage(0)
                        If CheckUPSAccess("Cash Flow") Then Me.tcPolicy.Controls.Add(Me.tabCashFlow)
                    End If
                    Me.tabCoverageDetails.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("POLIC-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If


                Case cDCAR
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If Not dtDCAR Is Nothing AndAlso dtDCAR.Rows.Count > 0 Then
                        Dcar1.srcDCAR = dtDCAR
                    End If
                    Me.tabDCAR.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("DCAR-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If


                Case cDDA
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If (Not dtDDA Is Nothing AndAlso dtDDA.Rows.Count > 0) Or (Not dtCCDR Is Nothing AndAlso dtCCDR.Rows.Count > 0) Then
                        'Add by ITDSCH on 2016-12-15 Begin
                        Ddaccdr1.dtPriv = dtPriv
                        Ddaccdr1.strUPSMenuCtrl = strUPSMenuCtrl
                        'Add by ITDSCH on 2016-12-15 End
                        Ddaccdr1.srcDTDDACCDR(dtCCDR) = dtDDA
                    End If
                    Me.tabDDACCD.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("DDAR-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
                Case cCCDR
                    ' wait for dda


                Case cHICL
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If Not dtCustHist Is Nothing Then
                        'AndAlso dtCustHist.Rows.Count > 0 Then
                        dtCustHist.TableName = "CustHist"
                        CustHist1.srcDTCustHist(dtNA.Copy, strPolicy, blnEditHICL) = dtCustHist
                    End If
                    Me.tabCustomerHistory.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("HICL-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If


                Case cUWINFO
                    'If Not dtUWInf Is Nothing AndAlso dtUWInf.Rows.Count > 0 Then
                    '    UwInfo1.srcDTUWInf(VBDate(dtPolSum.Rows(0).Item("POFELK"))) = dtUWInf
                    'End If
                    'Me.tabUnderwriting.ImageIndex = -1


                Case cPAYH
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    'If Not dtPAYH Is Nothing AndAlso dtPAYH.Rows.Count > 0 Then
                    'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
                    'Payh1.Visible = True
                    Ctrl_PaymentHist1.Visible = False
                    Ctrl_BillingInf1.Visible = False
                    '20171205 Levy
                    'Me.Payh1.MQQueuesHeader = Me.objMQQueHeader
                    'Me.Payh1.DBHeader = Me.objDBHeader

                    'Me.Payh1.PolicyAccountID(datEnqFrom, dtPAYH, dtPolSum.Copy) = strPolicy
                    'End If
                    'Me.Payh1.DateFrom = datEnqFrom
                    'Me.Payh1.srcDT(dtPolSum.Copy) = dtPAYH
                    tabPaymentHistory.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("PAYH-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If


                Case cCOUHST
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If Not dtCOUH Is Nothing AndAlso dtCOUH.Rows.Count > 0 Then
                        Me.Couh1.srcDT = dtCOUH
                    End If
                    tabCouponHistory.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("COUH-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If


                Case cAPLH
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If Not dtAPLH Is Nothing AndAlso dtAPLH.Rows.Count > 0 Then
                        Me.AplHist1.PolicyAccountID = strPolicy
                        Me.AplHist1.srcDT = dtAPLH
                    End If
                    tabAPLHistory.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("APLH-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
                    wndMain.Cursor = Cursors.Default

                    '#If UAT <> 0 Then
                    '                    Dim dtTRNH As New DataTable("TRNH")
                    '                    dtTRNH.Columns.Add("PolicyAccountID", Type.GetType("System.String"))
                    '                    dtTRNH.Columns.Add("Date", Type.GetType("System.DateTime"))
                    '                    dtTRNH.Columns.Add("Currency", Type.GetType("System.String"))
                    '                    dtTRNH.Columns.Add("Amount", Type.GetType("System.Decimal"))
                    '                    dtTRNH.Columns.Add("Account", Type.GetType("System.Decimal"))
                    '                    dtTRNH.Columns.Add("Description", Type.GetType("System.String"))
                    '                    dtTRNH.Columns.Add("Screen", Type.GetType("System.String"))
                    '                    dtTRNH.Columns.Add("User", Type.GetType("System.String"))

                    '                    Dim dr As DataRow
                    '                    dr = dtTRNH.NewRow
                    '                    dr.Item("PolicyAccountID") = "H9915383"
                    '                    dr.Item("Date") = #1/20/2006#
                    '                    dr.Item("Currency") = "BH"
                    '                    dr.Item("Amount") = 38900.0
                    '                    dr.Item("Account") = "11925006"
                    '                    dr.Item("Description") = "FM 1046310026       "
                    '                    dr.Item("Screen") = "PPAY"
                    '                    dr.Item("User") = "YHC"
                    '                    dtTRNH.Rows.Add(dr)

                    '                    dr = dtTRNH.NewRow
                    '                    dr.Item("PolicyAccountID") = "H9915383"
                    '                    dr.Item("Date") = #1/20/2006#
                    '                    dr.Item("Currency") = "BH"
                    '                    dr.Item("Amount") = -38900.0
                    '                    dr.Item("Account") = "24150006"
                    '                    dr.Item("Description") = "POL  ACCT - PREM SUS"
                    '                    dr.Item("Screen") = "PPAY"
                    '                    dr.Item("User") = "YHC"
                    '                    dtTRNH.Rows.Add(dr)

                    '                    dr = dtTRNH.NewRow
                    '                    dr.Item("PolicyAccountID") = "H9915383"
                    '                    dr.Item("Date") = #1/20/2006#
                    '                    dr.Item("Currency") = "BH"
                    '                    dr.Item("Amount") = -303.74
                    '                    dr.Item("Account") = "31002006"
                    '                    dr.Item("Description") = "PREM ACCT - MODE PRM"
                    '                    dr.Item("Screen") = "PPAY"
                    '                    dr.Item("User") = "YHC"
                    '                    dtTRNH.Rows.Add(dr)
                    '                    Me.TrxHist1.PolicyAccountID = "H9915383"
                    '                    Me.TrxHist1.srcDT = dtTRNH
                    '#End If
                Case cTRNH
#If STRESS <> 0 Then
                    Dim ts As TimeSpan = New TimeSpan(Now.Ticks)
#End If
                    If Not dtTRNH Is Nothing AndAlso dtTRNH.Rows.Count > 0 Then
                        Ctrl_TranHist1.Visible = False
                        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
                        'TrxHist1.Visible = True
                        'Me.TrxHist1.PolicyAccountID = strPolicy
                        'Me.TrxHist1.DateFrom(datEnqFrom) = datHstFrom
                        'Me.TrxHist1.srcDT = dtTRNH
                    End If
                    Me.tabTransactionHistory.ImageIndex = -1
#If STRESS <> 0 Then
                    Dim tf As TimeSpan = New TimeSpan(Now.Ticks)
                    tf = tf.Subtract(ts)
                    Call WriteLog("TRNH-2: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
                    wndMain.Cursor = Cursors.Default
            End Select
            'wndMain.ProgressBar1.Value += 30
            'If wndMain.ProgressBar1.Value = 90 Then
            '    wndMain.ProgressBar1.Value = 100
            '    Timer1.Enabled = True
            'End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcPolicy.SelectedIndexChanged
        If tcPolicy.SelectedTab Is Nothing Then
            Exit Sub
        End If

        If tcPolicy.SelectedTab.Name = "tabPolicySummary" Then
        End If

        ' Load service log only after ORDUNA is available
        If tcPolicy.SelectedTab.Name = "tabServiceLog" AndAlso (Not strCustID Is Nothing) AndAlso strCustID <> "" AndAlso Not blnSrvLogLoad Then
            UclServiceLog1.PolicyAccountID(strCustID) = strPolicy
            'UclServiceLog1.CustomerID = strCustID
            blnSrvLogLoad = True
            Exit Sub
        End If

        If tcPolicy.SelectedTab.Name = "tabServiceLog" AndAlso (strCustID Is Nothing OrElse strCustID = "") AndAlso Not blnSrvLogLoad Then
            tcPolicy.SelectedTab = tabPolicySummary
        End If

        ' Load agent information only after ORDUNA is available
        If tcPolicy.SelectedTab.Name = "tabAgentInfo" AndAlso (Not strCustID Is Nothing) AndAlso strCustID <> "" AndAlso Not blnAgtInfoLoad Then
            Try
                AgentInfo1.srcDTPolSum(dtNA.Copy) = dtPolSum.Copy
                blnAgtInfoLoad = True
                Exit Sub
            Catch ex As Exception
            End Try
        End If

        If tcPolicy.SelectedTab.Name = "tabAgentInfo" AndAlso (strCustID Is Nothing OrElse strCustID = "") AndAlso Not blnAgtInfoLoad Then
            tcPolicy.SelectedTab = tabPolicySummary
        End If

        ' Load UW information only after ORDUNA is available
        If tcPolicy.SelectedTab.Name = "tabUnderwriting" AndAlso (Not strCustID Is Nothing) AndAlso strCustID <> "" AndAlso Not blnUWLoad Then
            'UwInfo1.srcDTUWInf(VBDate(dtPolSum.Rows(0).Item("POFELK"))) = strPolicy
            'oliver 2024-7-31 added for Com 6
            If strCompany = "ING" OrElse strCompany = "BMU" Then
                UwInfo1.Visible = True
                UwInfoAsur.Visible = False
                UwInfo1.srcDTUWInf(dtPolSum.Rows(0).Item("POFELK")) = strPolicy
            Else
                UwInfo1.Visible = False
                UwInfoAsur.Visible = True
                UwInfoAsur.srcDTUWInf() = strPolicy
            End If

            blnUWLoad = True
            Exit Sub
        End If

        If tcPolicy.SelectedTab.Name = "tabUnderwriting" AndAlso (strCustID Is Nothing OrElse strCustID = "") AndAlso Not blnUWLoad Then
            tcPolicy.SelectedTab = tabPolicySummary
        End If

        If tcPolicy.SelectedTab.Name = "tabParSur" Then
            Me.UclParSur1.refreshTabPage()
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

        strSQL = "Select cswcpa_message from csw_customer_policy_alert " &
            " Where cswcpa_type = '" & Trim(strType) & "' " &
            " And cswcpa_id = '" & Trim(strID) & "' " &
            " And cswcpa_valid = 'T'"

        sqlconnect.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect
        sqlcmd.CommandTimeout = gQryTimeOut

        ' Count the number of records return from CIW, if okay we can then call MQ
        Try
            strMsg = sqlcmd.ExecuteScalar()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
            sqlcmd.Dispose()
            sqlconnect.Dispose()

        End Try

        Return strMsg

    End Function
    'IUL
    Private Sub tabIULControl_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabIULControl.HandleCreated
        Ctrl_POS_iULMaint1.DBHeader = Me.objDBHeader
        Ctrl_POS_iULMaint1.MQQueuesHeader = Me.objMQQueHeader
        Ctrl_POS_iULMaint1.POSHeader = Me.objPOSHeader
        Ctrl_POS_iULMaint1.SetPolicyNumber = RTrim(strPolicy)
        Ctrl_POS_iULMaint1.SetSystemDate = DateTime.Now.ToString("yyyy-MM-dd")
        If Ctrl_POS_iULMaint1.ValidateAllowAccess(strPolicy) Then
            Ctrl_POS_iULMaint1.Init()
        End If
    End Sub
    Private Sub tabClaimsHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabClaimsHistory.HandleCreated
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Try
            ClaimHist1.DBHeader = Me.objDBHeader
            ClaimHist1.MQQueuesHeader = Me.objMQQueHeader
            ClaimHist1.PolicyAccountID = strPolicy
            ClaimHist1.ShowUIAsync()
        Finally
            SysEventLog.WritePerLog(gsUser, "CS2005.frmPolicy", "tabClaimsHistory_HandleCreated", "", mainStartTime, Now, txtPolicy.Text,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    Private Sub tabAgentInfo_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabAgentInfo.HandleCreated
        If strCustID <> "" Then
            Try
                AgentInfo1.srcDTPolSum(dtNA.Copy) = dtPolSum.Copy
                blnAgtInfoLoad = True
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub tabSMS_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabSMS.HandleCreated
        Sms1.CompanyID = strCompany
        Sms1.PolicyAccountID = strPolicy
        Sms1.objDbHeader = Me.objDBHeader
    End Sub

    Private Sub tabiLASSMS_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabiLASSMS.HandleCreated
        SMSiLAS.PolicyAccountID = strPolicy
    End Sub

    Private Sub tabServiceLog_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabServiceLog.HandleCreated
        If strCustID <> "" Then
            UclServiceLog1.CompanyID = CompanyID
            UclServiceLog1.PolicyAccountID(strCustID) = strPolicy
            blnSrvLogLoad = True
        End If
    End Sub

    Private Sub tabUnderwriting_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabUnderwriting.HandleCreated
        If Not isLifeAsia Then
            If strCustID <> "" Then
                'UwInfo1.srcDTUWInf(VBDate(dtPolSum.Rows(0).Item("POFELK"))) = strPolicy
                'oliver 2024-7-31 added for Com 6
                If strCompany = "ING" OrElse strCompany = "BMU" Then
                    UwInfo1.Visible = True
                    UwInfoAsur.Visible = False
                    UwInfo1.srcDTUWInf(dtPolSum.Rows(0).Item("POFELK")) = strPolicy
                Else
                    UwInfo1.Visible = False
                    UwInfoAsur.Visible = True
                    UwInfoAsur.srcDTUWInf() = strPolicy
                End If
                blnUWLoad = True
            End If
        Else
            'oliver 2024-7-31 added for Com 6
            If strCompany = "ING" OrElse strCompany = "BMU" Then
                UwInfo1.Visible = True
                UwInfoAsur.Visible = False
                UwInfo1.srcDTUWInf(Nothing) = strPolicy
            Else
                UwInfo1.Visible = False
                UwInfoAsur.Visible = True
                UwInfoAsur.srcDTUWInf() = strPolicy
            End If
            blnUWLoad = True
            'MsgBox("No this tab in this policy No.")
            'tcPolicy.SelectTab("tabAgentInfo")
        End If
    End Sub

    ' **** ES007 begin ****
    Private Sub tabSubAcc_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabSubAcc.HandleCreated

        If isLifeAsia Then
            If isProposal = True Then
            Else
                Ctrl_FinancialInfo1.Visible = True
                Ctrl_FinancialInfo1.DBHeader = Me.objDBHeader
                Ctrl_FinancialInfo1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_FinancialInfo1.PolicyNoInUse = RTrim(strPolicy)
                Ctrl_FinancialInfo1.ShowSubAcctBalRcd()
                Ctrl_FinancialDtl1.DBHeader = Me.objDBHeader
                Ctrl_FinancialDtl1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_FinancialDtl1.PolicyNoInUse = RTrim(strPolicy)
                Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Search
            End If
        End If

    End Sub
    ' **** ES007 end ****

    Private Sub tabCashFlow_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCashFlow.HandleCreated

        ' **** ES004 begin ****
        If isLifeAsia Then
            'Setup parameter
            objCI.MQQueuesHeader = Me.objMQQueHeader
            objCI.DBHeader = Me.objDBHeader
            objCI.POSHeader = Me.objPOSHeader

            Me.CashFlow1.Visible = True
            Me.Ctrl_Sel_CO1.Visible = True
            Me.UclCashFlow1.Visible = False
            Me.btnSelect.Visible = True
        Else
            Me.CashFlow1.Visible = False
            Me.Ctrl_Sel_CO1.Visible = False
            Me.UclCashFlow1.Visible = True
            Me.btnSelect.Visible = False
            Me.UclCashFlow1.PolicyAccountID(dtPolMisc, dtCoverage(0), datHstFrom) = strPolicy
        End If
        'Me.UclCashFlow1.PolicyAccountID(dtPolMisc, dtCoverage(0), datHstFrom) = strPolicy
        ' **** ES004 end ****

    End Sub

    Private Sub frmPolicy_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        'blnStop(0) = True
        'While Not blnCanClose
        '    Application.DoEvents()
        'End While
        wndMain.StatusBarPanel1.Text = ""
        dlgQExec = Nothing
    End Sub

    Private Sub frmPolicy_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.UclServiceLog1.PendingSave = True Then
            MsgBox("There are unsaved service log records, please save it first.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gSystem)
            e.Cancel = True
        Else
            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub tabDISC_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDISC.HandleCreated
        Me.Disc1.CompanyID = CompanyID
        Me.Disc1.PolicyAccountID = strPolicy
    End Sub

    Private Sub tabUTRH_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabUTRH.HandleCreated
        If isLifeAsia Then
            If isProposal = True Then
                Me.UclUTRH1.Visible = False
                Ctrl_FundHolding1.Visible = False
            Else
                Me.UclUTRH1.Visible = False
                Ctrl_FundHolding1.Visible = True
                Ctrl_FundHolding1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_FundHolding1.DBHeader = Me.objDBHeader
                Ctrl_FundHolding1.PolicyinUse = RTrim(strPolicy)
                Me.UclUTRH1.Visible = False

            End If
        Else
            Ctrl_FundHolding1.Visible = False
            Me.UclUTRH1.Visible = True
            Me.UclUTRH1.PolicyAccountID(datHstFrom, dtPolSum.Rows(0).Item("ProductID")) = strPolicy
        End If

    End Sub

    Private Sub tabUTRS_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabUTRS.HandleCreated
        If isLifeAsia Then
            Ctrl_FundTranSummary1.Visible = True
            Ctrl_FundTranSummary1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_FundTranSummary1.DBHeader = Me.objDBHeader
            Ctrl_FundTranSummary1.PolicyinUse = RTrim(strPolicy)
            Ctrl_FundTranSummary1.BasicInsured = RTrim(strCustID)
        End If
    End Sub

    Private Sub tabPolicyAlt_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPolicyAlt.HandleCreated
        Me.UclPolicyAlt1.PolicyAccountID = strPolicy
        Me.PolicyAltPending1.PolicyAccountID = strPolicy
    End Sub

    Private Sub tabIWSHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabIWSHistory.HandleCreated
        'oliver 2023-12-8 commented for Switch Over Code from Assurance to Bermuda
        Me.Ctrl_IWSHistoryEnquiry1.Visible = True
        Me.Ctrl_IWSHistoryEnquiry1.DBHeader = objDBHeader
        Me.Ctrl_IWSHistoryEnquiry1.preSelectedPolicyNo = strPolicy
    End Sub

    Public Sub IWSCaseDetails_Show(ByVal selectedPolicyNo As String, ByVal selectedIWSCaseNo As String, ByVal selectedIWSStatus As String)
        Dim IWSCaseDetails As POSMain.frmIWSCaseDetails = New POSMain.frmIWSCaseDetails
        IWSCaseDetails.DBHeader = objDBHeader
        IWSCaseDetails.PolicyNo = selectedPolicyNo
        IWSCaseDetails.IWSCaseNo = selectedIWSCaseNo
        IWSCaseDetails.IWSStatus = selectedIWSStatus
        IWSCaseDetails.isCRS = True
        IWSCaseDetails.Show()
        IWSCaseDetails.Activate()
    End Sub

    Private Sub cmdReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReturn.Click
        Me.cmdPending.Visible = True
        Me.cmdPending.Enabled = True
        Me.cmdReturn.Visible = False
        Me.cmdReturn.Enabled = False
        Me.PolicyAltPending1.Visible = False
        Me.UclPolicyAlt1.Visible = True
    End Sub

    Private Sub cmdPending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPending.Click
        Me.cmdPending.Visible = False
        Me.cmdPending.Enabled = False
        Me.cmdReturn.Visible = True
        Me.cmdReturn.Enabled = True
        Me.PolicyAltPending1.Visible = True
        Me.UclPolicyAlt1.Visible = False
    End Sub

    Private Sub tabMClaimsHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMClaimsHistory.HandleCreated
        Me.MClaimHist1.MQQueuesHeader = Me.objMQQueHeader
        Me.MClaimHist1.DBHeader = Me.objDBHeader
        Me.MClaimHist1.PolicyAccountID = strPolicy
    End Sub

    Private Sub btnLAOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim strName, strCustID, strClientID As String

        'With CType(bm.Current, DataRowView)
        '    strCustID = .Item("CustomerID")
        '    strCustID = GetRelationValue(.Row, "CustRel", "CustomerID")
        '    strClientID = .Row.Item("ClientID")
        '    strName = Trim(GetRelationValue(.Row, "CustRel", "NameSuffix")) & " " & Trim(GetRelationValue(.Row, "CustRel", "FirstName")) & " (" & strCustID & ")"
        'End With

        'Dim w As New frmCustomer
        'w.CustID(strClientID) = strCustID
        'w.Text = "Customer " & strName

        'ShowWindow(w, wndMain, strCustID)
    End Sub

    ''' <remarks>
    ''' <br>20241119 Chrysan Cheng, CRS performer slowness - Reuse BusinessDate to reduce the number of calls</br>
    ''' </remarks>
    Public Sub getLifeAsiaInfo(strPolicy As String, businessDate As Date, ByRef ds As DataSet)
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String = ""
        Dim dr1 As DataRow
        sqlconnect.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlconnect.Open()
        'strSQL = "select '" & strPolicy & "' as policyAccountID, '' as unit, " & _
        '    "isnull((select agentcode from customer where customerid = (select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('SA'))), '') as POAGCY, " & _
        '    "isnull((select agentcode from customer where customerid = (select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('WA'))), '') as POWAGT," & _
        '    "'' as POPAGT, '' as PAGCO1, '' as PAGCO2, '' as PAGCO3"

        'strSQL = "select  agentcode, customer.customerid, nameprefix, firstname, namesuffix, "
        'strSQL = strSQL & " chifstnm, chilstnm, CoName, CoCName, policyrelatecode from csw_poli_rel, customer "
        'strSQL = strSQL & " where policyaccountid = '" & RTrim(strPolicy) & "' and customer.customerid = csw_poli_rel.customerid and policyrelatecode in ('SA', 'WA')"
        strSQL = "select  c.agentcode, a.unitcode, c.customerid, nameprefix, firstname, namesuffix, " &
            " ISNULL(chifstnm, '') as [chifstnm], ISNULL(chilstnm, '') as [chilstnm], CoName, CoCName, policyrelatecode from csw_poli_rel, customer c, agentcodes a " &
            " where policyaccountid = '" & RTrim(strPolicy) & "' and c.customerid = csw_poli_rel.customerid and policyrelatecode in ('SA', 'WA') " &
            " and c.agentcode = a.agentcode"

        Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
        Dim sqlrdr As SqlDataReader = sqlcmd.ExecuteReader()
        Dim dt1 As DataTable = New DataTable("POLINF")

        dt1.Columns.Add("policyAccountID", Type.GetType("System.String"))
        dt1.Columns.Add("unit", Type.GetType("System.String"))
        dt1.Columns.Add("POAGCY", Type.GetType("System.String"))
        dt1.Columns.Add("POWAGT", Type.GetType("System.String"))
        dt1.Columns.Add("POPAGT", Type.GetType("System.String"))
        dt1.Columns.Add("PAGCO1", Type.GetType("System.String"))
        dt1.Columns.Add("PAGCO2", Type.GetType("System.String"))
        dt1.Columns.Add("PAGCO3", Type.GetType("System.String"))
        ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
        dt1.Columns.Add("POAPOP", Type.GetType("System.String"))
        ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End
        dr1 = dt1.NewRow()
        dr1("policyAccountID") = RTrim(strPolicy)
        dr1("unit") = ""
        dr1("POAGCY") = ""
        dr1("POWAGT") = ""
        dr1("POPAGT") = ""
        dr1("PAGCO1") = ""
        dr1("PAGCO2") = ""
        dr1("PAGCO3") = ""

        ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
        ' Get Effective Date
        Dim strerr As String = ""
        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.DBHeader = Me.objDBHeader
        clsPOS.MQQueuesHeader = Me.objMQQueHeader

        ' CRS performer slowness - Reuse BusinessDate to reduce the number of calls
        Dim EffDate As Date = businessDate
        'Dim dsEffDateSend As New DataSet
        'Dim dsEffDateCurr As New DataSet
        'Dim strTime As String = ""
        'Dim blnGetEffDate As Boolean
        'Dim drEffDate As DataRow
        'Dim dtEffDateSendData As New DataTable
        'dtEffDateSendData.Columns.Add("PolicyNo")
        'drEffDate = dtEffDateSendData.NewRow
        'drEffDate("PolicyNo") = RTrim(strPolicy)
        'dtEffDateSendData.Rows.Add(drEffDate)
        'dsEffDateSend.Tables.Add(dtEffDateSendData)
        'If GetBusinessDate(EffDate) = False Then
        '    EffDate = Today
        'End If
        'blnGetEffDate = clsPOS.GetPolicy(dsEffDateSend, dsEffDateCurr, strTime, strerr)
        'If dsEffDateCurr.Tables.Count > 0 Then
        '    If dsEffDateCurr.Tables(0).Rows.Count > 0 Then
        '        EffDate = dsEffDateCurr.Tables(0).Rows(0)("Sys_Bus_Date")
        '    Else
        '        EffDate = #1/1/1900#
        '    End If
        'Else
        '    EffDate = #1/1/1900#
        'End If

        Dim blnGetLAPolicy As Boolean = False
        Dim dsLAPolicySend As New DataSet
        Dim dsLAPolicyCurr As New DataSet

        Dim dt As DataTable = Me.GetEnquiryBO()
        Dim row As DataRow = dt.NewRow()
        row("QuoteDate") = EffDate.Date
        row("PolicyNo") = strPolicy.Trim
        row("PrintFlag1") = "N"
        dt.Rows.Add(row)
        dsLAPolicySend.Tables.Add(dt)

        strerr = ""

        clsPOS.DBHeader = Me.objDBHeader
        clsPOS.MQQueuesHeader = Me.objMQQueHeader
        blnGetLAPolicy = clsPOS.PolicyValueEnq(dsLAPolicySend, dsLAPolicyCurr, strerr)
        If blnGetLAPolicy = True Then
            If dsLAPolicyCurr.Tables.Count > 0 Then
                If dsLAPolicyCurr.Tables(0).Rows.Count > 0 Then
                    dr1("POAPOP") = dsLAPolicyCurr.Tables(0).Rows(0)("APLStatusInd")
                    If dsLAPolicyCurr.Tables(0).Rows(0)("APLStatusInd").ToString.Trim = "S" Then
                        lblAPL.Visible = True
                        lblLastAPL.Visible = False
                    Else
                        If dsLAPolicyCurr.Tables(0).Rows(0)("APLStatusInd").ToString.Trim = "L" Then
                            lblAPL.Visible = False
                            lblLastAPL.Visible = True
                        Else
                            lblAPL.Visible = False
                            lblLastAPL.Visible = False
                        End If
                    End If
                Else
                    dr1("POAPOP") = ""
                    lblAPL.Visible = False
                    lblLastAPL.Visible = False
                End If
            Else
                dr1("POAPOP") = ""
            End If
        Else
            dr1("POAPOP") = ""
        End If
        ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End

        While sqlrdr.Read
            If sqlrdr("policyrelatecode").ToString.Trim = "SA" Then
                dr1("POAGCY") = sqlrdr("agentcode").ToString.Trim
            ElseIf sqlrdr("policyrelatecode").ToString.Trim = "WA" Then
                dr1("POWAGT") = sqlrdr("agentcode").ToString.Trim
            End If
            dr1("unit") = sqlrdr("unitcode").ToString.Trim
        End While

        dt1.Rows.Add(dr1)
        sqlrdr.Close()
        sqlcmd.Dispose()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'strSQL = "select distinct customerID as ClientID from csw_poli_rel where PolicyAccountID = '" & rtrim(strpolicy) & "' and policyrelatecode in ('SA', 'WA')"
        'strSQL = "select (select '00000' + agentcode from customer where  csw_poli_rel.customerID=customer.CustomerID) as ClientID, " & _
        '"(select NamePrefix from customer where  csw_poli_rel.customerID=customer.CustomerID) as NamePrefix, " & _
        '"(select FirstName from customer where  csw_poli_rel.customerID=customer.CustomerID) as FirstName, " & _
        '"(select NameSuffix from customer where  csw_poli_rel.customerID=customer.CustomerID) as NameSuffix, " & _
        '"(select chilstNm + ' ' + ChiFstNm from customer where  csw_poli_rel.customerID=customer.CustomerID) as ChiName, policyrelatecode " & _
        '"from csw_poli_rel " & _
        '"where PolicyAccountID = '" & rtrim(strpolicy) & "' and policyrelatecode in ('SA', 'WA', 'PH', 'PI', 'BE') "

        Dim dt2 As DataTable = New DataTable("ORDUNA")
        dt2.Columns.Add("ClientID", Type.GetType("System.String"))
        dt2.Columns.Add("NamePrefix", Type.GetType("System.String"))
        dt2.Columns.Add("NameSuffix", Type.GetType("System.String"))
        dt2.Columns.Add("FirstName", Type.GetType("System.String"))
        dt2.Columns.Add("ChiName", Type.GetType("System.String"))

        'agent
        strSQL = "select  distinct agentcode, customer.customerid, nameprefix, firstname, namesuffix, "
        strSQL = strSQL & " ISNULL(chifstnm, '') as [chifstnm], ISNULL(chilstnm, '') as [chilstnm], CoName, CoCName from csw_poli_rel, customer "
        strSQL = strSQL & " where policyaccountid = '" & RTrim(strPolicy) & "' and customer.customerid = csw_poli_rel.customerid and policyrelatecode in ('SA', 'WA')"

        sqlcmd = New SqlCommand(strSQL, sqlconnect)
        sqlrdr = sqlcmd.ExecuteReader()

        While sqlrdr.Read
            dr1 = dt2.NewRow()

            dr1("ClientID") = "00000" & sqlrdr("agentcode")

            dr1("NamePrefix") = sqlrdr("NamePrefix")
            dr1("NameSuffix") = sqlrdr("NameSuffix")
            dr1("FirstName") = sqlrdr("FirstName")
            Try
                dr1("ChiName") = Trim(sqlrdr("chilstnm")) & Trim(sqlrdr("chifstnm"))
            Catch ex As Exception
                dr1("ChiName") = ""
            End Try
            dt2.Rows.Add(dr1)
        End While

        sqlrdr.Close()
        sqlcmd.Dispose()


        'non agent
        strSQL = "select  distinct agentcode, customer.customerid, nameprefix, firstname, namesuffix, "
        strSQL = strSQL & " ISNULL(chifstnm, '') as [chifstnm], ISNULL(chilstnm, '') as [chilstnm], CoName, CoCName from csw_poli_rel, customer "
        strSQL = strSQL & " where policyaccountid = '" & RTrim(strPolicy) & "' and customer.customerid = csw_poli_rel.customerid and policyrelatecode not in ('SA', 'WA')"

        sqlcmd = New SqlCommand(strSQL, sqlconnect)
        sqlrdr = sqlcmd.ExecuteReader()

        While sqlrdr.Read
            dr1 = dt2.NewRow()
            dr1("ClientID") = sqlrdr("customerid")

            dr1("NamePrefix") = sqlrdr("NamePrefix")
            dr1("NameSuffix") = sqlrdr("NameSuffix")
            dr1("FirstName") = sqlrdr("FirstName")
            dr1("ChiName") = Trim(sqlrdr("chilstnm")) & Trim(sqlrdr("chifstnm"))

            dt2.Rows.Add(dr1)
        End While

        sqlrdr.Close()
        sqlcmd.Dispose()


        'owner
        strSQL = "select  distinct agentcode, customer.customerid, nameprefix, firstname, namesuffix, "
        strSQL = strSQL & " ISNULL(chifstnm, '') as [chifstnm], ISNULL(chilstnm, '') as [chilstnm], CoName, CoCName from csw_poli_rel, customer "
        strSQL = strSQL & " where policyaccountid = '" & RTrim(strPolicy) & "' and customer.customerid = csw_poli_rel.customerid and policyrelatecode  in ('PH')"

        sqlcmd = New SqlCommand(strSQL, sqlconnect)
        sqlrdr = sqlcmd.ExecuteReader()

        While sqlrdr.Read

            strCustID = sqlrdr("customerid")

            txtCName.Text = sqlrdr("COName")
            txtCNameChi.Text = sqlrdr("COCName")
            txtTitle.Text = sqlrdr("NamePrefix")
            txtLastName.Text = sqlrdr("NameSuffix")
            txtFirstName.Text = sqlrdr("FirstName")
            txtChiName.Text = Trim(sqlrdr("chilstnm")) & Trim(sqlrdr("chifstnm"))

        End While


        sqlrdr.Close()
        sqlcmd.Dispose()
        sqlconnect.Close()
        ds.Tables.Add(dt1)
        ds.Tables.Add(dt2)
    End Sub

    Private Sub tabDDACCD_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDDACCD.HandleCreated
        If isLifeAsia Then
            If isProposal = True Then
                Ddaccdr1.Visible = False
                Ctrl_DirectDebitEnq1.Visible = False
            Else
                Ddaccdr1.Visible = False
                Ctrl_DirectDebitEnq1.Visible = True
                Ctrl_DirectDebitEnq1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_DirectDebitEnq1.DBHeader = Me.objDBHeader
                Ctrl_DirectDebitEnq1.PolicyNoInUse = RTrim(strPolicy)

                'Update by ITDSCH on 2016-12-14 Begin
                'Ctrl_DirectDebitEnq1.ShowMandateListRcd()
                Ctrl_DirectDebitEnq1.ShowMandateListRcd(dtPriv, strUPSMenuCtrl)
                'Update by ITDSCH on 2016-12-14 End
            End If
        Else
            Ddaccdr1.Visible = True
            Ctrl_DirectDebitEnq1.Visible = False
        End If
    End Sub

    Private Function ConnectDB(ByRef obj As Object, ByVal strProject As String, ByVal strConnAlias As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        Try

            If Not obj Is Nothing Then
                obj = New DBLogon_NET.DBLogon.DBlogonNet
                obj.Project = strProject
                obj.ConnectionAlias = strConnAlias 'strComp + strProject + strEnv
                obj.User = strUser
                If obj.Connect() Then
                    Return True
                Else
                    strErr = obj.RecentErrorMessage
                    Return False
                End If
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Private Sub tabPolicySummary_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPolicySummary.Enter

    End Sub



    Private Sub tabPolicySummary_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPolicySummary.HandleCreated
        'If isLifeAsia Then
        '    lblPolicyNo.Text = rtrim(strpolicy)
        '    lblPolicyNo.Visible = True
        '    Ctrl_CRSPolicyGeneral_Information1.Visible = True
        '    PolicySummary1.Visible = False
        '    AddressSelect1.Visible = False
        '    Ctrl_CRSPolicyGeneral_Information1.dbHeader = Me.objDBHeader
        '    Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
        '    Ctrl_CRSPolicyGeneral_Information1.dbHeader = Me.objDBHeader
        '    Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = rtrim(strpolicy)
        '    Ctrl_CRSPolicyGeneral_Information1.SystemInUse = "CRS"
        '    Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()
        'Else
        '    Ctrl_CRSPolicyGeneral_Information1.Visible = False
        '    PolicySummary1.Visible = True
        '    AddressSelect1.Visible = True
        'End If

    End Sub

    Private Sub tabTransactionHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabTransactionHistory.HandleCreated
        If isLifeAsia Then
            If isProposal = True Then
                Ctrl_TranHist1.Visible = False
                'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
                'TrxHist1.Visible = False
            Else

                Using objCRSWS As New CRSWS.CRSWS

                    'objCRSWS.DBSOAPHeaderValue = GetCRSWSDBHeader()
                    'objCRSWS.MQSOAPHeaderValue = GetCRSWSMQHeader()
                    objCRSWS.Url = Utility.Utility.GetWebServiceURL("CRSWS", objDBHeader, objMQQueHeader) 'CRS_Component.EnvironmentUtility.getEnvironmentSetting(System.Configuration.ConfigurationManager.AppSettings("WebServiceEnv"))
                    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                        objCRSWS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr)
                    End If

                    Dim strNextMandateRef As String = ""
                    objCRSWS.Credentials = System.Net.CredentialCache.DefaultCredentials
                    objCRSWS.Timeout = 10000000
                    Dim dsSendData As New DataSet
                    Dim dsReceData As New DataSet

                    'Prepare tables for transaction
                    If PrepareSendTranHist(dsSendData, strErr, strNextMandateRef) = False Then
                        MsgBox(strErr)
                        Exit Sub
                    End If

                    While dsReceData.Tables.Count > 0
                        dsReceData.Tables.RemoveAt(0)
                    End While
                    If PrepareReceTranHist(dsReceData, strErr) = False Then
                        MsgBox(strErr)
                        Exit Sub
                    End If

                    objCRSWS.MQSOAPHeaderValue = GetCRSWSMQHeader(2)
                    Dim res = objCRSWS.GetTranHist(dsSendData, dsReceData, strErr)
                    appSettings.Logger.logger.Debug("GetTranHist: " & res)
                End Using

                Ctrl_TranHist1.Visible = True
                'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
                'TrxHist1.Visible = False
                Ctrl_TranHist1.dbHeader = Me.objDBHeader
                Ctrl_TranHist1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_TranHist1.PolicyNoInUse = RTrim(strPolicy)
                Ctrl_TranHist1.ShowTranHistRcd()

            End If
        Else
            Ctrl_TranHist1.Visible = False
            'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
            'TrxHist1.Visible = True
        End If
    End Sub

    Private Sub tabPaymentHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPaymentHistory.HandleCreated
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Dim funStartTime As Date = Now

        Try
            If isLifeAsia Then
                Control.CheckForIllegalCrossThreadCalls = False ' disable non-main thread call check for global controls (Enable cross-thread access to control)

                ' PaymentHistory, load control synchronously (Because it is non-time-consuming)
                Ctrl_PaymentHist1.Visible = True
                Ctrl_PaymentHist1.MQheaderInUse = Me.objMQQueHeader
                Ctrl_PaymentHist1.ComHeaderInUse = Me.objDBHeader
                Ctrl_PaymentHist1.PolicyNoInUse = RTrim(strPolicy)
                Ctrl_PaymentHist1.InitScreen()
                SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabPaymentHistory_HandleCreated", "Ctrl_PaymentHist1.InitScreen", funStartTime, Now, txtPolicy.Text,
                            (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))

                ' BillingInfo, assign key fields first (in case needed for Load event)
                Ctrl_BillingInf1.Enabled = False    ' disable before loading is complete
                Ctrl_BillingInf1.Visible = True
                Ctrl_BillingInf1.DBHeader = Me.objDBHeader
                Ctrl_BillingInf1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_BillingInf1.PolicyNoInUse = RTrim(strPolicy)
                ThreadPool.QueueUserWorkItem(AddressOf ShowBillingInfo) ' async load, controls will be across threads accessed!

                ' AutoRegularWithdrawal, can load control synchronously (Because async loading is already implemented inside the control)
                Ctrl_AutoRegularWithdrawal.Visible = True
                Ctrl_AutoRegularWithdrawal.DBHeader = Me.objDBHeader
                Ctrl_AutoRegularWithdrawal.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_AutoRegularWithdrawal.PolicyNoInUse = RTrim(strPolicy)
                Dim dsPolicyHeader As DataSet = dsPolicyHead
                If dsPolicyHeader IsNot Nothing AndAlso dsPolicyHeader.Tables.Count > 0 AndAlso dsPolicyHeader.Tables(0).Rows.Count > 0 Then
                    funStartTime = Now
                    Ctrl_AutoRegularWithdrawal.OwnerNoInUse = dsPolicyHeader.Tables(0).Rows(0)("Owner_No").ToString.Trim
                    Ctrl_AutoRegularWithdrawal.ShowARWRecord()
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabPaymentHistory_HandleCreated", "Ctrl_AutoRegularWithdrawal.ShowARWRecord", funStartTime, Now, txtPolicy.Text,
                        (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))
                End If

            Else
                Ctrl_BillingInf1.Visible = False
                Ctrl_PaymentHist1.Visible = False
                Ctrl_AutoRegularWithdrawal.Visible = False
            End If
        Finally
            SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabPaymentHistory_HandleCreated", "", mainStartTime, Now, txtPolicy.Text,
                            (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    Private Sub ShowBillingInfo()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Dim funStartTime As Date = Now
        Dim dsPolicyHeader As DataSet = dsPolicyHead

        Try
            If dsPolicyHeader IsNot Nothing AndAlso dsPolicyHeader.Tables.Count > 0 AndAlso dsPolicyHeader.Tables(0).Rows.Count > 0 Then
                If Ctrl_BillingInf1.initCboNewBillType() AndAlso Ctrl_BillingInf1.initCboNewMode() Then
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "ShowBillingInfo", "Ctrl_BillingInf1.initCbo", funStartTime, Now, txtPolicy.Text,
                        (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))

                    Ctrl_BillingInf1.CurrdsInUse = dsPolicyHeader.Copy()
                    Ctrl_BillingInf1.EffDateInUse = dsPolicyHeader.Tables(0).Rows(0)("Sys_Bus_Date")
                    Ctrl_BillingInf1.PolicyNoInUse = RTrim(strPolicy)
                    Ctrl_BillingInf1.PModeInUse = dsPolicyHeader.Tables(0).Rows(0)("Freq")
                    Ctrl_BillingInf1.BillTypeInUse = dsPolicyHeader.Tables(0).Rows(0)("Pay_Meth")
                    Ctrl_BillingInf1.AllowQuote = True

                    funStartTime = Now
                    Ctrl_BillingInf1.showBillingInfo()
                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "ShowBillingInfo", "Ctrl_BillingInf1.showBillingInfo", funStartTime, Now, txtPolicy.Text,
                        (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))
                End If
            End If
        Catch ignore As Exception
        Finally
            ' finally, re-enable the control if Form is not closed
            If Not (Me.Disposing OrElse Me.IsDisposed OrElse Ctrl_BillingInf1.Disposing OrElse Ctrl_BillingInf1.IsDisposed) Then
                Ctrl_BillingInf1.Enabled = True
            End If
            SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "ShowBillingInfo", "", mainStartTime, Now, txtPolicy.Text,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub



    Private Sub tabLevyHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabLevyHistory.HandleCreated

        'LevyHistory
        If strPolicy.Trim() = "" Then
            Exit Sub
        End If

        Me.Ctrl_LevyHistory.Visible = True
        Me.Ctrl_LevyHistory.MQQueuesHeader = Me.objMQQueHeader
        Me.Ctrl_LevyHistory.DBHeader = Me.objDBHeader
        Me.Ctrl_LevyHistory.PolicyNoInUse = RTrim(strPolicy)
        Me.Ctrl_LevyHistory.ShowLevyHistory()


    End Sub

    Private Sub tabARWHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabARWHistory.HandleCreated

        'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
        If strPolicy.Trim() = "" Then
            Exit Sub
        End If

        Me.Ctrl_AutoRegularWithdrawalHis.Visible = True
        Me.Ctrl_AutoRegularWithdrawalHis.MQQueuesHeader = Me.objMQQueHeader
        Me.Ctrl_AutoRegularWithdrawalHis.DBHeader = Me.objDBHeader
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoInUse = RTrim(strPolicy)

    End Sub

    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Private Sub tabDirectCreditHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDirectCreditHistory.HandleCreated
        If isLifeAsia Then
            Ctrl_DirectCreditHist1.Visible = True
            'Ctrl_DirectCreditHist1.MQQueuesHeader = Me.objMQQueHeader
            'Ctrl_DirectCreditHist1.DBHeader = Me.objDBHeader
            'Ctrl_DirectCreditHist1.PolicyNo = RTrim(strPolicy)
            'Ctrl_DirectCreditHist1.InitScreen()

            Ctrl_DirectCreditHist1.DBHeader = Me.objDBHeader
            Ctrl_DirectCreditHist1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_DirectCreditHist1.PolicyNo = RTrim(strPolicy)

            Ctrl_BillingInf1.DBHeader = Me.objDBHeader
            Ctrl_BillingInf1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_BillingInf1.PolicyNoInUse = RTrim(strPolicy)

            If Ctrl_BillingInf1.initCboNewMode() = False Then
                Exit Sub
            End If
            Dim dsPolicySend As New DataSet
            Dim dsPolicyCurr As New DataSet
            Dim strTime As String = ""
            Dim strerr As String = ""
            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            Dim blnGetPolicy As Boolean
            Dim dr As DataRow
            Dim dtSendData As New DataTable
            dtSendData.Columns.Add("PolicyNo")
            dr = dtSendData.NewRow
            dr("PolicyNo") = RTrim(strPolicy)
            dtSendData.Rows.Add(dr)

            dsPolicySend.Tables.Add(dtSendData)
            clsPOS.DBHeader = Me.objDBHeader
            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                End If
            End If
        Else
            Ctrl_DirectCreditHist1.Visible = False
        End If

    End Sub

    Private Sub tabAnnuityPaymentHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabAnnuityPaymentHistory.HandleCreated
        If isLifeAsia Then
            Ctrl_AnnuityPaymentHist1.Visible = True
            'Ctrl_AnnuityPaymentHist1.MQQueuesHeader = Me.objMQQueHeader
            'Ctrl_AnnuityPaymentHist1.DBHeader = Me.objDBHeader
            'Ctrl_AnnuityPaymentHist1.PolicyNo = RTrim(strPolicy)
            'Ctrl_AnnuityPaymentHist1.InitScreen()

            Ctrl_AnnuityPaymentHist1.DBHeader = Me.objDBHeader
            Ctrl_AnnuityPaymentHist1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_AnnuityPaymentHist1.PolicyNo = RTrim(strPolicy)

            Ctrl_BillingInf1.DBHeader = Me.objDBHeader
            Ctrl_BillingInf1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_BillingInf1.PolicyNoInUse = RTrim(strPolicy)

            If Ctrl_BillingInf1.initCboNewMode() = False Then
                Exit Sub
            End If
            Dim dsPolicySend As New DataSet
            Dim dsPolicyCurr As New DataSet
            Dim strTime As String = ""
            Dim strerr As String = ""
            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            Dim blnGetPolicy As Boolean
            Dim dr As DataRow
            Dim dtSendData As New DataTable
            dtSendData.Columns.Add("PolicyNo")
            dr = dtSendData.NewRow
            dr("PolicyNo") = RTrim(strPolicy)
            dtSendData.Rows.Add(dr)

            dsPolicySend.Tables.Add(dtSendData)
            clsPOS.DBHeader = Me.objDBHeader
            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                End If
            End If
        Else
            Ctrl_AnnuityPaymentHist1.Visible = False
        End If

    End Sub

    Private Sub tabCupGetTransResult_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCupGetTransResult.HandleCreated
        If isLifeAsia Then
            Ctrl_CupGetTransResult.Visible = True
            Ctrl_CupGetTransResult.ComHeader = Me.objDBHeader
            'Ctrl_CupGetTransResult.InitScreen()
        Else
        End If

    End Sub
    'LH1507004  Journey Annuity Day 2 Phase 2 End

    ''' <remarks>
    ''' <br>20241106 Chrysan Cheng, CRS performer slowness</br>
    ''' </remarks>
    Private Sub tabCoverageDetails_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCoverageDetails.HandleCreated
        Dim SysEventLog As New SysEventLog.clsEventLog With {.CiwHeader = Me.objDBHeader}
        Dim mainStartTime As Date = Now
        Dim funStartTime As Date = Now

        If isLifeAsia Then
            If isProposal = True Then
                Ctrl_ChgComponent1.Visible = False
                Coverage1.Visible = False
            Else
                Coverage1.Visible = False

                SysEventLog.ProcEventLog("INFO", Now, Me.objDBHeader.UserID, "CRS", Strings.Right(Environment.MachineName, 15), "", "cs2005.frmPolicy.tabCoverageDetails_HandleCreated",
                                         "dsComponentSysTable", $"Policy:{txtPolicy.Text}, TableCount:{dsComponentSysTable?.Tables.Count}", "", False)
                ' set system config table externally first (if any)
                If dsComponentSysTable IsNot Nothing AndAlso dsComponentSysTable.Tables.Count > 0 Then
                    Ctrl_ChgComponent1.SysTableInUse = dsComponentSysTable.Copy
                Else
                    ' system config data fail, and will be fetching again later inside the control, notify user
                    MsgBox("Data acquisition exception, loading may be slow.", MsgBoxStyle.Exclamation)
                End If

                Ctrl_ChgComponent1.DBHeader = Me.objDBHeader
                Ctrl_ChgComponent1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_ChgComponent1.SystemInUse = gSystem
                Ctrl_ChgComponent1.ModeInUse = Utility.Utility.ModeName.Blank

                ' Only display a disabled blank control until the coverage data is successfully retrieved
                Ctrl_ChgComponent1.Enabled = False
                Ctrl_ChgComponent1.Visible = True


                ' async to get coverage data
                Dim bl As New CoverageDetailsAsyncBL(Me.objDBHeader, Me.objMQQueHeader)

                SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabCoverageDetails_HandleCreated", "CoverageDetailsAsyncBL.GetWholeDataDictionaryAsync.Start", funStartTime, Now, txtPolicy.Text, "", "")
                bl.GetWholeDataDictionaryAsync(strPolicy, dsPolicyHead,
                    Sub(t, dataDict)
                        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabCoverageDetails_HandleCreated", "CoverageDetailsAsyncBL.GetWholeDataDictionaryAsync.End", funStartTime, Now, txtPolicy.Text, "", "")

                        If Me.Disposing OrElse Me.IsDisposed OrElse Ctrl_ChgComponent1.Disposing OrElse Ctrl_ChgComponent1.IsDisposed Then Return   ' no need reload if Form closed

                        ' set data and reload control
                        Ctrl_ChgComponent1.Invoke(
                            Sub()
                                Try
                                    If dsPolicyHead IsNot Nothing AndAlso dsPolicyHead.Tables.Count > 0 Then
                                        ' set policy header data externally
                                        Ctrl_ChgComponent1.PolicyInfo = dsPolicyHead.Tables(0).Copy
                                    End If

                                    ' set coverage data externally
                                    Ctrl_ChgComponent1.SetDataForAsync(dataDict)

                                    If t.IsFaulted OrElse dsPolicyHead Is Nothing OrElse dsPolicyHead.Tables.Count = 0 Then
                                        ' some data fail, and will be fetching again later in synchronous mode, notify user
                                        MsgBox("Data acquisition exception, loading may be slow.", MsgBoxStyle.Exclamation)
                                    End If

                                    funStartTime = Now
                                    ' set policyNo and refresh info
                                    Ctrl_ChgComponent1.PolicyNoInuse = RTrim(strPolicy)
                                    SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabCoverageDetails_HandleCreated", "Ctrl_ChgComponent1.PolicyNoInuse", funStartTime, Now, txtPolicy.Text, "", "")
                                Finally
                                    ' finally, re-enable the control anyway
                                    Ctrl_ChgComponent1.Enabled = True
                                End Try
                            End Sub
                        )
                    End Sub
                )
            End If
        Else
            Coverage1.Visible = True
            Ctrl_ChgComponent1.Visible = False
        End If

        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "tabCoverageDetails_HandleCreated", "", mainStartTime, Now, txtPolicy.Text, "", "")
    End Sub

    Private Sub tabDefaultPayoutMethodRegist_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDefaultPayoutMethodRegist.HandleCreated

        If Len(txtPolicy.Text) = 8 Then
            Ctrl_DefaultPayout1.PolicyInUse = txtPolicy.Text
        End If

        Ctrl_DefaultPayout1.DBHeader = Me.objDBHeader
        Ctrl_DefaultPayout1.MQQueuesHeader = Me.objMQQueHeader
        Ctrl_DefaultPayout1.isCRS = True
        Ctrl_DefaultPayout1.ShowPolicyRcd()

    End Sub

    ' **** ES004 begin ****
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        frmSelCO.policyInuse = RTrim(strPolicy)
        frmSelCO.MQQueuesHeader = Me.objMQQueHeader
        frmSelCO.ShowDialog()

        If frmSelCO.SelectMode = True Then
            Dim dsSelCO As New DataSet
            dsSelCO = frmSelCO.CurrdsInuse
            Ctrl_Sel_CO1.currdsInuse = dsSelCO
            Ctrl_Sel_CO1.BasicPlan = frmSelCO.BasicPlan

            'Show COSel control inform.
            If ShowCOSelData(Ctrl_Sel_CO1, dsSelCO) = False Then
                Exit Sub
            End If

            Dim dsMain As New DataSet

            CashFlow1.MQHeaderInUse = Me.objMQQueHeader
            CashFlow1.DBHeaderInUse = Me.objDBHeader
            CashFlow1.policynoinuse = RTrim(strPolicy)
            CashFlow1.lifenoinuse = dsSelCO.Tables(1).DefaultView(0)("Life")
            CashFlow1.coveragenoinuse = dsSelCO.Tables(1).DefaultView(0)("Cov")
            CashFlow1.ridernoinuse = dsSelCO.Tables(1).DefaultView(0)("Rider")
            CashFlow1.modeinuse = Utility.Utility.ModeName.Blank
            CashFlow1.Enabled = True

        End If

    End Sub

    Private Function ShowCOSelData(ByVal Ctrl_Sel_CO As POSCommCtrl.Ctrl_Sel_CO, ByVal currds As DataSet) As Boolean
        Try
            'Show COSel control inform.
            Ctrl_Sel_CO.PolicyNoInuse = RTrim(strPolicy)
            Ctrl_Sel_CO.LifeNoInUse = currds.Tables(1).DefaultView(0)("Life")
            Ctrl_Sel_CO.ClientNoInuse = currds.Tables(1).DefaultView(0)("ClientNo")
            Ctrl_Sel_CO.ClientNameInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO.RiderInuse = currds.Tables(1).DefaultView(0)("Rider")
            Ctrl_Sel_CO.CovCodeInuse = currds.Tables(1).DefaultView(0)("Cov_Code")
            Ctrl_Sel_CO.CovDescInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO.CovNoInuse = currds.Tables(1).DefaultView(0)("Cov")
            Ctrl_Sel_CO.RiskStsInuse = currds.Tables(1).DefaultView(0)("Risk_Sts")
            Ctrl_Sel_CO.PStsInuse = currds.Tables(1).DefaultView(0)("Prem_Sts")
            Ctrl_Sel_CO.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_Sel_CO.ShowCORcd()
            Return True
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Function
    ' **** ES004 end ****

    ' **** ES005 begin ****
    Private Function GetBillNo() As String

        Dim strSQL As String
        Dim sqlConn As New SqlConnection

        GetBillNo = ""

        sqlConn.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        sqlConn.Open()
        strSQL = "Select BillNo " &
                 "From PolicyAccount " &
                 "Where policyaccountid = '" & RTrim(strPolicy) & "'"
        Dim sqlCmd As New SqlCommand(strSQL, sqlConn)
        sqlCmd.CommandTimeout = gQryTimeOut

        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            While sqlReader.Read()
                If IsDBNull(sqlReader.Item("BillNo")) Then
                    GetBillNo = ""
                Else
                    GetBillNo = sqlReader.Item("BillNo")
                End If
            End While
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()

    End Function
    ' **** ES005 end ****
    Private Sub tabFundTrans_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabFundTrans.HandleCreated

        'If isLifeAsia Then
        Me.Ctrl_FundTranHist1.ComHeader = objDBHeader
        Me.Ctrl_FundTranHist1.MQQueuesHeader = objMQQueHeader
        Me.Ctrl_FundTranHist1.PolicyNoInUse = strPolicy
        'End If
    End Sub

    ' **** ES006 begin ****
    Private Sub tabCustomerHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCustomerHistory.HandleCreated
        If isLifeAsia And g_Comp <> "HKL" Then
            CustHist1.srcDTCustHist(dtNA.Copy, strPolicy, False, True) = Nothing
        End If
    End Sub
    ' **** ES006 end ****

    Private Sub tabPolicyNotes_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPolicyNotes.HandleCreated
        Dim f As New ComCtl.PolicyNoteFilter()
        f.PolicyNo = strPolicy
        f.EntryDateFrom = DateAdd(DateInterval.Year, -3, Today)
        f.EntryDateTo = Today
        PolicyNote1.SearchNote(f)
        PolicyNote1.AllowEdit = True
    End Sub

    'oliver 2024-3-5 added for ITSR-5105 EasyTake Service Phase 2
    Private Sub tabDesignatedPerson_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDesignatedPerson.HandleCreated
        If Len(txtPolicy.Text) = 8 Then
            Ctrl_DesignatedPerson.PolicyInUse = txtPolicy.Text
        End If

        Ctrl_DesignatedPerson.MQQueuesHeader = Me.objMQQueHeader
        Ctrl_DesignatedPerson.DBHeader = Me.objDBHeader
        Ctrl_DesignatedPerson.isCRS = True
        Ctrl_DesignatedPerson.ShowPolicyRcd()
    End Sub


    Private Sub tabCouponHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCouponHistory.HandleCreated
        ' **** ES008 begin ****
        If isLifeAsia Then
            Me.CouponHist1.Visible = True
            Me.CouponHist1.PolicyNo = strPolicy
            CouponHist1.DBHeader = objDBHeader
            CouponHist1.MQQueuesHeader = objMQQueHeader
            Me.Couh1.Visible = False
        Else
            Me.CouponHist1.Visible = False
            Me.Couh1.Visible = True
        End If
        ' **** ES008 end ****
    End Sub

    Private Sub LoanHist1_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoanHist1.HandleCreated
        '' **** ES008 begin ****
        'If isLifeAsia Then
        '    Me.LoanHist1.Visible = True
        '    Me.LoanHist1.PolicyNo = strPolicy
        '    LoanHist1.DBHeader = objDBHeader
        '    LoanHist1.MQQueuesHeader = objMQQueHeader
        '    Me.AplHist1.Visible = False
        'Else
        '    Me.LoanHist1.Visible = False
        '    Me.AplHist1.Visible = True
        'End If
        '' **** ES008 end ****
    End Sub

    Private Sub tabFinancialInfo_HandleCreated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabFinancialInfo.HandleCreated
        ' ES007 begin
        'If isLifeAsia Then
        '    If isProposal = True Then
        '        FinancialInfo1.Visible = False
        '        Ctrl_FinancialInfo1.Visible = False
        '    Else
        '        tabFinancialInfo.AutoScroll = True
        '        FinancialInfo1.Visible = False
        '        Ctrl_FinancialInfo1.Visible = True


        '        Ctrl_FinancialInfo1.DBHeader = Me.objDBHeader
        '        Ctrl_FinancialInfo1.MQQueuesHeader = Me.objMQQueHeader
        '        Ctrl_FinancialInfo1.PolicyNoInUse = RTrim(strPolicy)
        '        Ctrl_FinancialInfo1.ShowSubAcctBalRcd()
        '        Ctrl_FinancialDtl1.DBHeader = Me.objDBHeader
        '        Ctrl_FinancialDtl1.MQQueuesHeader = Me.objMQQueHeader
        '        Ctrl_FinancialDtl1.PolicyNoInUse = RTrim(strPolicy)
        '        Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Search
        '        'Ctrl_FinancialDtl1.showrestatmentInfo()

        '    End If
        'Else
        '    FinancialInfo1.Visible = True
        '    Ctrl_FinancialInfo1.Visible = False
        '    Ctrl_FinancialDtl1.Visible = False
        '    FinancialInfo1.PolicyAccountID(dtPolSum, dtPolMisc) = strPolicy
        'End If
        ' **** ES008 begin ****
        'FinancialInfo1.Visible = True
        'FinancialInfo1.isLA = isLifeAsia
        'FinancialInfo1.PolicyAccountID(dtPolSum, dtPolMisc) = strPolicy
        ' ES007 end

        ' **** ES008 begin ****
        If isLifeAsia Then
            Me.PolicyValue1.Visible = True
            Me.DateTimePicker1.Value = gBusDate
            PolicyValue1.DBHeader = objDBHeader

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            'objMQQueHeader.UserID = "LAOPER"
            '#End If
            If gUAT = False Then
                objMQQueHeader.UserID = "LAOPER"
            End If
            'AC - Change to use configuration setting - end

            PolicyValue1.MQQueuesHeader = objMQQueHeader
            Me.Controller.Enquiry(Me.DateTimePicker1.Value, strPolicy, Me.PolicyValue1)
            Me.FinancialInfo1.Visible = False
        Else
            '20171205 Levy
            FinancialInfo1.MQQueuesHeader = Me.objMQQueHeader
            FinancialInfo1.DBHeader = Me.objDBHeader

            Label6.Visible = False
            DateTimePicker1.Visible = False
            Button1.Visible = False
            Me.PolicyValue1.Visible = False
            Me.FinancialInfo1.Visible = True
            FinancialInfo1.isLA = isLifeAsia
            FinancialInfo1.PolicyAccountID(dtPolSum, dtPolMisc) = strPolicy
        End If
        ' **** ES008 end ****

    End Sub

    Private con As POSMain.PolicyValueController
    Private LifeClientPos As New LifeClientInterfaceComponent.clsPOS

    Protected ReadOnly Property Controller() As POSMain.PolicyValueController
        Get
            If con Is Nothing Then
                LifeClientPos.DBHeader = objDBHeader
                LifeClientPos.MQQueuesHeader = objMQQueHeader
                con = New POSMain.PolicyValueController(LifeClientPos)
            End If
            Return con
        End Get
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Controller.Enquiry(Me.DateTimePicker1.Value, strPolicy, Me.PolicyValue1)
    End Sub

    Private Sub tabAPLHistory_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabAPLHistory.HandleCreated

        If strCompany.Equals("ING") OrElse strCompany = "BMU" Then
            lblReminderMsg1.Visible = False
            lblReminderMsg2.Visible = False
        End If

        ' **** ES008 begin ****
        If isLifeAsia Then
            Me.LoanHist1.Visible = True
            Me.LoanHist1.PolicyNo = strPolicy
            LoanHist1.DBHeader = objDBHeader
            LoanHist1.MQQueuesHeader = objMQQueHeader
            Me.AplHist1.Visible = False
        Else
            Me.LoanHist1.Visible = False
            Me.AplHist1.Visible = True
        End If
        ' **** ES008 end ****
    End Sub

    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
    Protected Function GetEnquiryBO() As DataTable
        Dim bo As New BOSchema.PolicyValue()
        Dim dt As DataTable = bo.GetPolicyValueEnqSendSchema()
        Return bo.SchemaToDataTable(dt)
    End Function
    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End

    Private Sub tabParSur_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabParSur.HandleCreated
        'PL20120427 - START
        With Me.UclParSur1
            .DBHeader = Me.objDBHeader
            .MQQueuesHeader = Me.objMQQueHeader
            .PolicyNumber = RTrim(strPolicy)
            .InitInfo()
        End With
        'PL20120427 - END
    End Sub

    Private Sub tabParSur_HandleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabParSur.MouseClick
        Me.UclParSur1.refreshTabPage()
    End Sub

    Private Sub ClaimHist1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    'ITDCBT20150115 NegativeCashValue Start 
    Public Sub FillNegativeCashValueReminder()

        Dim dsNegativeCashValueReminder As New Data.DataSet
        Dim objPOS As LifeClientInterfaceComponent.clsPOS
        objPOS = New LifeClientInterfaceComponent.clsPOS()
        objPOS.DBHeader = objDBHeader

        'If Not objPOS.GetCapsilNegativeCashValueReminder(strPolicy, dsNegativeCashValueReminder, strErr) Then
        '    MsgBox(strErr)
        '    Exit Sub
        'End If

        If dsNegativeCashValueReminder.Tables.Count > 0 And dsNegativeCashValueReminder.Tables(0).Rows.Count > 0 Then 'if  exist
            Me.cboNCVReminderFlag.Items.Add("P: Loan amount > 95% CSV")
            Me.cboNCVReminderFlag.Items.Add("R: Loan amount < 95% CSV")
            Me.cboNCVReminderFlag.Items.Add("E: Exception case")

            For Each item As Object In cboNCVReminderFlag.Items
                If item.ToString().Substring(0, 1) = dsNegativeCashValueReminder.Tables(0).Rows(0).Item("R3PRTI").ToString() Then
                    cboNCVReminderFlag.SelectedItem = item
                    Exit For
                End If
            Next

            txtNCVUpdateUser.Text = dsNegativeCashValueReminder.Tables(0).Rows(0).Item("R3UPDU").ToString().ToUpper()
            If Trim(dsNegativeCashValueReminder.Tables(0).Rows(0).Item("R3UPDD").ToString()) <> "" And Len(dsNegativeCashValueReminder.Tables(0).Rows(0).Item("R3UPDD").ToString()) = 8 Then
                txtNCVUpdateDate.Text = dsNegativeCashValueReminder.Tables(0).Rows(0).Item("R3UPDD").ToString().Insert(6, "/").Insert(4, "/")
            End If

            'If bAllowUpdate Then
            '    Me.cboNCVReminderFlag.Enabled = True
            '    Me.cmdSubmit.Enabled = True
            'End If
        End If
    End Sub
    'ITDCBT20150115 NegativeCashValue End

    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Private Sub frmPolicy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Ctrl_CupGetTransResult.policyInuse = Me.strPolicy
    End Sub
    'LH1507004  Journey Annuity Day 2 Phase 2 End

    Private Sub tabPostSalesCall_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPostSalesCall.HandleCreated

        If Not blnCanView Then
            UclPostSalesCallQuestionnaire1.SetUIMode(FormModeEnum.Enquiry)
            Return
        End If

        UclPostSalesCallQuestionnaire1.DBHeader = Me.objDBHeader
        UclPostSalesCallQuestionnaire1.PolicyAccountID = strPolicy
        UclPostSalesCallQuestionnaire1.Initialize()
        UclPostSalesCallQuestionnaire1.RetrieveInfo()
        UclPostSalesCallQuestionnaire1.CheckPostSalesCallCompleted()

        RemoveHandler UclServiceLog1.EventSaved, AddressOf LockPostSalesCallQueationnaire
        AddHandler UclServiceLog1.EventSaved, AddressOf LockPostSalesCallQueationnaire
    End Sub

    Private Sub LockPostSalesCallQueationnaire(ByVal sender As Object, ByVal e As DataRow)
        If e("EventCategoryCode").ToString().Trim() = "86" OrElse
            e("EventCategoryCode").ToString().Trim() = "88" OrElse
            e("EventCategoryCode").ToString().Trim() = "93" OrElse
            e("EventCategoryCode").ToString().Trim() = "91" Then

            UclPostSalesCallQuestionnaire1.CheckPostSalesCallCompleted()
        End If
    End Sub

    'ITDCPI prompt message for bad address start
    Private Function BadAddrWarning(ByVal strCustID As String) As Boolean
        Dim strErr As String = String.Empty
        Dim dtBadAddr As New DataTable
        Dim dtMarkin As New DataTable
        'Get Badd Address Record
        If Not CustomerBL.GetBadAddress(CompanyID, strCustID, dtBadAddr, strErr) Then
            MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Function
        End If
        'If any address is bad address, return true for popup in UI
        If dtBadAddr.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function 'ITDCPI prompt message for bad address end

    'Alex Th Lee 20201013 [ITSR2281 - ePolicy]
    Private Sub loadEPolicyFlag(ByVal sPolicy As String)
        Dim POSDB As String = gcPOS
        Try
            Dim ds As DataSet = New DataSet("ePolicy")
            Dim sqlconnect As New SqlConnection
            Dim sqlda As SqlDataAdapter

            sqlconnect.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
            Dim command = New SqlCommand("select ePolicyIndicator, PolicyId from " & POSDB & "PolicyEstatement nolock where PolicyId = @sPolicy1", sqlconnect)
            command.Parameters.AddWithValue("@sPolicy1", sPolicy)
            sqlda = New SqlDataAdapter(command)
            Try
                If ds.Tables.Contains("ePolicy") Then ds.Tables.Remove("ePolicy")
            Catch ex As Exception

            End Try
            sqlda.Fill(ds, "ePolicy")

            If ds.Tables("ePolicy").Rows.Count > 0 Then
                If ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("E") Then
                    txtEPolicy.Text = "Y"
                ElseIf ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("S") Then
                    txtEPolicy.Text = "N (Slim)"
                ElseIf ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("F") Then
                    txtEPolicy.Text = "N (Full)"
                Else
                    txtEPolicy.Text = "N/A"
                End If
            Else
                txtEPolicy.Text = "N/A"
            End If
        Catch ex As Exception
            txtEPolicy.Text = "N/A"
        End Try
    End Sub
    Private Sub tabPayOutHist_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPayOutHist.HandleCreated
        Try
            Ctrl_CashDividendPayoutHist1.Visible = True
            Ctrl_CashDividendPayoutHist1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_CashDividendPayoutHist1.DBHeader = Me.objDBHeader
            Ctrl_CashDividendPayoutHist1.PolicyNo = strPolicy.Trim
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Hide all tabs, only show "Policy Summary (Assurance)" and "Service Log (Assurance)"
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-14
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub ShowAssuranceTabs()
        ' To hide the tab, you must remove the TabPage control from the TabControl.TabPages collection.
        ' Reference: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.tabpage?redirectedfrom=MSDN&view=net-5.0
        tcPolicy.TabPages.Clear()
        tcPolicy.TabPages.Add(tabAsurPolicySummary)
        tcPolicy.TabPages.Add(tabAsurServiceLog)
    End Sub

    ''' <summary>
    ''' Load policy information and policy's roles
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-14
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub LoadAssuranceInformation()
        UclPolicySummary_Asur1.PolicyNumber = Me.PolicyAccountID
        UclPolicySummary_Asur1.ProductName = Me.ProductName
        UclPolicySummary_Asur1.LoadInformation()

        UclServiceLog2.resetDS()
        UclServiceLog2.PolicyAccountID(UclServiceLog2.CustomerID) = Me.PolicyAccountID
    End Sub

    Private Sub notificationworker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles notificationworker.DoWork
        Call PolicyAlert()
        'ITSR2408 - SL20210625 - Start
        Call GetMediSavingAmount(txtPolicy.Text, True)
        'ITSR2408 - SL20210625 - 
    End Sub

    Private Sub notificationworker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles notificationworker.RunWorkerCompleted

    End Sub

    Private Sub tabTraditionalPartialSurrQuot_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabTraditionalPartialSurrQuot.HandleCreated

        frmSelCO.policyInuse = RTrim(strPolicy)
        frmSelCO.MQQueuesHeader = Me.objMQQueHeader
        frmSelCO.ShowDialog()

        If frmSelCO.SelectMode = True Then
            Dim dsSelCO As New DataSet
            dsSelCO = frmSelCO.CurrdsInuse
            Ctrl_Sel_CO2.currdsInuse = dsSelCO
            Ctrl_Sel_CO2.BasicPlan = frmSelCO.BasicPlan

            'Show COSel control inform.
            If ShowCOSelData(Ctrl_Sel_CO2, dsSelCO) = False Then
                Exit Sub
            End If

            Ctrl_POS_ParSurTradition1.InitGrid()

            Ctrl_POS_ParSurTradition1.DBHeader = Me.objDBHeader
            Ctrl_POS_ParSurTradition1.MQQueuesHeader = Me.objMQQueHeader

            Ctrl_POS_ParSurTradition1.CoverageSelectedInUse = True
            Ctrl_POS_ParSurTradition1.CurrentPolicyNoInUse = RTrim(strPolicy)
            Ctrl_POS_ParSurTradition1.CurrentLifeNoInUse = dsSelCO.Tables(1).DefaultView(0)("Life")
            Ctrl_POS_ParSurTradition1.CurrentCoverageNoInUse = dsSelCO.Tables(1).DefaultView(0)("Cov")
            Ctrl_POS_ParSurTradition1.CurrentRiderNoInUse = dsSelCO.Tables(1).DefaultView(0)("Rider")
            Ctrl_POS_ParSurTradition1.CurrentCoverageCodeInUse = dsSelCO.Tables(1).DefaultView(0)("Cov_code")

        End If

        'Ctrl_POS_ParSurTradition1.DBHeader = Me.objDBHeader
        'Ctrl_POS_ParSurTradition1.MQQueuesHeader = Me.objMQQueHeader

        'Ctrl_POS_ParSurTradition1.Visible = True
        'btnSelect2.Visible = True

    End Sub

    Private Function PrepareSendTranHist(ByRef dsSendData As DataSet, ByRef strErr As String, Optional ByVal strMandateRef As String = "") As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Policy_No")
            dtSendData.Columns.Add("MandateRef")
            dr("Policy_No") = txtPolicy.Text
            dr("MandateRef") = strMandateRef
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceTranHist(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Tran_No")
            dtReceData.Columns.Add("Date")
            dtReceData.Columns.Add("Code")
            dtReceData.Columns.Add("Description")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Sub LoadMsgAndLabel()
        Dim dsMsgApiLst As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_Comp), "MSG_API_LST", New Dictionary(Of String, String)() From {})
        Dim strMsg As String = ""
        Dim strLbl As String = ""
        Dim dsTmp As DataSet
        Dim iCount = 1
        If dsMsgApiLst.Tables.Count > 0 Then
            For Each dr As DataRow In dsMsgApiLst.Tables(0).Rows
                dsTmp = APIServiceBL.CallAPIBusi(getCompanyCode(g_Comp), dr("MSG_API_LST").ToString(), New Dictionary(Of String, String)() From {
                                                            {"strInPolicy", strPolicy}
                                                            })
                If dsTmp.Tables.Count > 0 Then
                    For Each dr2 As DataRow In dsTmp.Tables(0).Rows
                        If dr2("MSG").ToString.Trim.Length > 0 Then
                            strMsg = strMsg + iCount.ToString + ". " + dr2("MSG").ToString.Trim + vbNewLine
                            iCount = iCount + 1
                        End If
                        If dr2("HEADER").ToString.Trim.Length > 0 Then
                            If strLbl.Length > 0 Then
                                strLbl = strLbl + ", " + dr2("HEADER").ToString.Trim
                            Else
                                strLbl = dr2("HEADER").ToString.Trim
                            End If
                        End If
                    Next
                End If
            Next
        End If
        If strLbl.Trim.Length > 0 Then
            lblMsg.Text = strLbl.Trim
        End If
        If strMsg.Trim.Length > 0 Then
            MessageBox.Show(strMsg.Trim)
        End If
    End Sub

    Private Function LoadMsgAndLabelD2C() As String
        Try
            Dim dsMsg As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_Comp), "MSG_API_D2C", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
            If dsMsg.Tables(0).Rows.Count > 0 Then
                Me.lblTAD2CPolicyMsg.Visible = True
                Me.lblTAD2CPolicyMsg.Text = dsMsg.Tables(0).Rows(0)("lbl")
                MessageBox.Show(dsMsg.Tables(0).Rows(0)("msg"))
                Return dsMsg.Tables(0).Rows(0)("F2F")
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

#Region "Initialize TabPage"
    Private Sub AddTabPageControl(ByRef tabPage As TabPage, ByRef tabpageControl As Control)
        tabPage.SuspendLayout()
        tabpageControl.SuspendLayout()
        Dim searchControl As Boolean = tabPage.Controls.ContainsKey(tabpageControl.Name)
        If searchControl = True Then
            tabPage.Controls.Remove(tabPage.Controls(tabpageControl.Name))
            tabPage.Controls.Add(tabpageControl)
        Else
            'MsgBox(tabpageControl.Name & " not found.")
            tabPage.Controls.Add(tabpageControl)

        End If
        tabPage.ResumeLayout()
        tabpageControl.ResumeLayout()
    End Sub
#End Region
End Class



