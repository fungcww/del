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
' Admended By: Lubin
' Admended Function: Move the Macau query logic to the server side.
' Date: 2022-11-07
' Project: ITSR-3487  CRS Macau Phase 3 ¡V CRS
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
Imports CRS_Ctrl

Public Class frmPolicyMcu
    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Inherits frmPolicyBase
    Inherits System.Windows.Forms.Form

    Public blnEditHICL As Boolean = False
    Friend WithEvents lblEPolicy As System.Windows.Forms.Label
    Friend WithEvents txtEPolicy As System.Windows.Forms.TextBox
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

        'Lubin 2022-11-07 Looks like the SMSiLAS  wasn't move all logic from frmPolicy.
        'Add it new instance, otherwise it will reported an null error.
        If IsNothing(SMSiLAS) Then SMSiLAS = New CS2005.SMSiLAS()

        ' Move initialization here to fix designer crash
        Me.SMSiLAS.PolicyAccountID = PolicyAccountID
        Me.SMSiLAS.CustomerID = CustomerID

        Me.tcPolicy.Controls.Clear()
        If CheckUPSAccess("Policy Summary") Then Me.tcPolicy.Controls.Add(Me.tabPolicySummary)
        If CheckUPSAccess("Coverage Details") Then Me.tcPolicy.Controls.Add(Me.tabCoverageDetails)
        If CheckUPSAccess("Unit Tx History") Then tcPolicy.Controls.Add(Me.tabUTRH)
        If CheckUPSAccess("Unit Tx Summary") Then tcPolicy.Controls.Add(Me.tabUTRS)
        If CheckUPSAccess("Unit Tx History") And g_Comp <> "HKL" Then tcPolicy.Controls.Add(Me.tabFundTrans) ' ES005 begin
        'SP ILAS
        Me.tcPolicy.Controls.Add(Me.tabPayOutHist)

        If CheckUPSAccess("Policy Alternation") Then tcPolicy.Controls.Add(Me.tabPolicyAlt)
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
        ' End Policy Notes
        If CheckUPSAccess("Service Log") Then
            Me.tcPolicy.Controls.Add(Me.tabServiceLog)
            Me.tcPolicy.Controls.Add(Me.tabPostSalesCall)
        End If

        If CheckUPSAccess("Agent Info") Then Me.tcPolicy.Controls.Add(Me.tabAgentInfo)
        If CheckUPSAccess("Underwriting") Then Me.tcPolicy.Controls.Add(Me.tabUnderwriting)

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
    Friend WithEvents tabPolicySummary As System.Windows.Forms.TabPage
    Friend WithEvents tabTapNGo As System.Windows.Forms.TabPage
    Friend WithEvents tabOePay As System.Windows.Forms.TabPage 'KT20180726
    Friend WithEvents tabCustomerHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabClaimsHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabServiceLog As System.Windows.Forms.TabPage
    Friend WithEvents tabUnderwriting As System.Windows.Forms.TabPage
    Friend WithEvents tabCoverageDetails As System.Windows.Forms.TabPage
    Friend WithEvents tabTransactionHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabFinancialInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabPaymentHistory As System.Windows.Forms.TabPage
    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Friend WithEvents tabAnnuityPaymentHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabDirectCreditHistory As System.Windows.Forms.TabPage
    Friend WithEvents tabCupGetTransResult As System.Windows.Forms.TabPage
    'LH1507004  Journey Annuity Day 2 Phase 2 End
    Friend WithEvents tabAgentInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabDDACCD As System.Windows.Forms.TabPage
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
    'Friend WithEvents PolicySummary1 As CS2005.PolicySummaryMcu
    Friend WithEvents AddressSelect1 As CS2005.AddressSelectMcu
    Friend WithEvents Coverage1 As CS2005.CoverageMcu
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
    Friend WithEvents tabSMS As System.Windows.Forms.TabPage
    Friend WithEvents Sms1 As CS2005.uclSMS_Asur
    Friend WithEvents AgentInfo1 As CS2005.AgentInfoMcu
    Friend WithEvents CustHist1 As CS2005.CustHist
    Friend WithEvents Ddaccdr1 As CS2005.DDACCDRMcu
    Friend WithEvents UwInfo1 As CS2005.UWInfoMcu
    Friend WithEvents tabCouponHistory As System.Windows.Forms.TabPage
    Friend WithEvents Couh1 As CS2005.COUH
    Friend WithEvents FinancialInfo1 As CS2005.FinancialInfoMcu
    Friend WithEvents UclServiceLog1 As CS2005.uclServiceLogMcu
    Friend WithEvents tabAPLHistory As System.Windows.Forms.TabPage
    Friend WithEvents AplHist1 As CS2005.APLHist
    Friend WithEvents tabCashFlow As System.Windows.Forms.TabPage
    Friend WithEvents UclCashFlow1 As CS2005.uclCashFlow
    Friend WithEvents lblAPL As System.Windows.Forms.Label
    Friend WithEvents lblLastAPL As System.Windows.Forms.Label ' Project Leo G3
    Friend WithEvents tabDCAR As System.Windows.Forms.TabPage
    Friend WithEvents Dcar1 As CS2005.DCAR
    Friend WithEvents txtCName As System.Windows.Forms.TextBox
    Friend WithEvents txtCNameChi As System.Windows.Forms.TextBox
    'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
    'Friend WithEvents TrxHist1 As CS2005.TrxHist
    Friend WithEvents tabDISC As System.Windows.Forms.TabPage
    Friend WithEvents Disc1 As CS2005.DISC
    Friend WithEvents tabUTRH As System.Windows.Forms.TabPage
    Friend WithEvents tabUTRS As System.Windows.Forms.TabPage
    Friend WithEvents UclUTRH1 As CS2005.uclUTRH
    Friend WithEvents tabPolicyAlt As System.Windows.Forms.TabPage
    Friend WithEvents UclPolicyAlt1 As CS2005.uclPolicyAltMcu
    Friend WithEvents PolicyAltPending1 As CS2005.PolicyAltPendingMcu
    Friend WithEvents cmdReturn As System.Windows.Forms.Button
    Friend WithEvents cmdPending As System.Windows.Forms.Button
    Friend WithEvents tabMClaimsHistory As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_DirectDebitEnq1 As CRS_Ctrl.ctrl_DirectDebitEnq
    Friend WithEvents Ctrl_CRSPolicyGeneral_Information1 As CRS_Ctrl.ctrl_CRSPolicyGeneral_Information
    Friend WithEvents Ctrl_TapNGo1 As CRS_Ctrl.ctrl_TapNGo
    Friend WithEvents Ctrl_OePay1 As CRS_Ctrl.ctrl_OePay 'KT20170726
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
    Friend WithEvents Ctrl_TranHist1 As CRS_Ctrl.ctrl_TranHist
    Friend WithEvents Ctrl_BillingInf1 As POSCommCtrl.Ctrl_BillingInf
    Friend WithEvents Ctrl_ChgComponent1 As POSCommCtrl.Ctrl_ChgComponent
    Friend WithEvents Ctrl_PaymentHist1 As ComCtl.PaymentHistory
    'LH1507004  Journey Annuity Day 2 Phase 2 Start
    Friend WithEvents Ctrl_AnnuityPaymentHist1 As POSCommCtrl.Ctrl_AnnuityPaymentHist
    Friend WithEvents Ctrl_DirectCreditHist1 As POSCommCtrl.Ctrl_DirectCreditHist
    Friend WithEvents Ctrl_CupGetTransResult As ComCtl.CupGetTransResult
    'LH1507004  Journey Annuity Day 2 Phase 2 End
    Friend WithEvents Ctrl_FundHolding1 As CRS_Ctrl.ctrl_FundHolding
    Friend WithEvents Ctrl_FundTranSummary1 As CRS_Ctrl.ctrl_FundTranSummary
    Friend WithEvents CashFlow1 As ComCtl.CashFlow
    Friend WithEvents Ctrl_Sel_CO1 As POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tabFundTrans As System.Windows.Forms.TabPage
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents Ctrl_FundTranHist1 As CRS_Ctrl.ctrl_FundTranHist
    Friend WithEvents tabPolicyNotes As System.Windows.Forms.TabPage
    Friend WithEvents PolicyNote1 As ComCtl.PolicyNote
    Friend WithEvents tabSubAcc As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_FinancialDtl1 As CRS_Ctrl.ctrl_FinancialDtl
    Friend WithEvents Ctrl_FinancialInfo1 As CRS_Ctrl.ctrl_FinancialInfo
    Friend WithEvents LoanHist1 As POSCommCtrl.LoanHist
    Friend WithEvents CouponHist1 As POSCommCtrl.CouponHist
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents PolicyValue1 As POSCommCtrl.PolicyValue
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tabParSur As System.Windows.Forms.TabPage
    Friend WithEvents UclParSur1 As CS2005.uclParSur
    Friend WithEvents lblDateFormatMsg As System.Windows.Forms.Label
    Friend WithEvents tabiLASSMS As System.Windows.Forms.TabPage
    Friend WithEvents SMSiLAS As CS2005.SMSiLAS
    Friend WithEvents cboNCVReminderFlag As System.Windows.Forms.ComboBox
    Friend WithEvents lblNCVReminderFlag As System.Windows.Forms.Label
    Friend WithEvents lblNCVUpdateDate As System.Windows.Forms.Label
    Friend WithEvents txtNCVUpdateDate As System.Windows.Forms.TextBox
    Friend WithEvents lblNCVUpdateUser As System.Windows.Forms.Label
    Friend WithEvents txtNCVUpdateUser As System.Windows.Forms.TextBox
    Friend WithEvents tabPostSalesCall As System.Windows.Forms.TabPage
    Friend WithEvents UclPostSalesCallQuestionnaire1 As CS2005.uclPostSalesCallQuestionnaireMcu
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tabLevyHistory As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_LevyHistory As CRS_Ctrl.ctrl_LevyHistory
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtEstatement As System.Windows.Forms.TextBox
    Friend WithEvents MClaimHist1 As CS2005.MClaimHistMcu
    Friend WithEvents lblCapsilPolNo As System.Windows.Forms.Label 'ITSR933 FG R3 Policy Number Change
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblMacauBanner As System.Windows.Forms.Label 'add by jeff for mcuau policy
    Friend WithEvents tabARWHistory As System.Windows.Forms.TabPage 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents Ctrl_AutoRegularWithdrawalHis As CRS_Ctrl.ctrl_AutoRegularWithdrawalHis 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents Ctrl_AutoRegularWithdrawal As CRS_Ctrl.ctrl_AutoRegularWithdrawal 'ITSR 1607 1792 - Auto / Regular Withdrawal (ARW)
    Friend WithEvents tabTraditionalPartialSurrQuot As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_Sel_CO2 As POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents Ctrl_POS_ParSurTradition1 As POSCommCtrl.Ctrl_POS_ParSurTradition
    'IUL
    Friend WithEvents tabIULControl As System.Windows.Forms.TabPage = New System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_POS_iULMaint1 As POSCommCtrl.Ctrl_POS_iULMaint = New POSCommCtrl.Ctrl_POS_iULMaint
    '

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPolicyMcu))
        Dim PolicyNoteDefaultAdaptor1 As ComCtl.PolicyNoteDefaultAdaptor = New ComCtl.PolicyNoteDefaultAdaptor()
        Me.tcPolicy = New System.Windows.Forms.TabControl()
        Me.tabPolicySummary = New System.Windows.Forms.TabPage()
        Me.Ctrl_CRSPolicyGeneral_Information1 = New CRS_Ctrl.ctrl_CRSPolicyGeneral_Information()
        Me.lblNCVUpdateDate = New System.Windows.Forms.Label()
        Me.txtNCVUpdateDate = New System.Windows.Forms.TextBox()
        Me.lblNCVUpdateUser = New System.Windows.Forms.Label()
        Me.txtNCVUpdateUser = New System.Windows.Forms.TextBox()
        Me.cboNCVReminderFlag = New System.Windows.Forms.ComboBox()
        Me.lblNCVReminderFlag = New System.Windows.Forms.Label()
        Me.lblPolicyNo = New System.Windows.Forms.Label()
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.PolicySummary1 = New CS2005.PolicySummaryMcu()
        Me.AddressSelect1 = New CS2005.AddressSelectMcu()
        Me.tabUTRH = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundHolding1 = New CRS_Ctrl.ctrl_FundHolding()
        Me.UclUTRH1 = New CS2005.uclUTRH()
        Me.tabUTRS = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundTranSummary1 = New CRS_Ctrl.ctrl_FundTranSummary()
        Me.tabCoverageDetails = New System.Windows.Forms.TabPage()
        Me.Ctrl_ChgComponent1 = New POSCommCtrl.Ctrl_ChgComponent()
        Me.Coverage1 = New CS2005.CoverageMcu()
        Me.tabFinancialInfo = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PolicyValue1 = New POSCommCtrl.PolicyValue()
        Me.FinancialInfo1 = New CS2005.FinancialInfoMcu()
        Me.tabMClaimsHistory = New System.Windows.Forms.TabPage()
        Me.MClaimHist1 = New CS2005.MClaimHistMcu()
        Me.tabDISC = New System.Windows.Forms.TabPage()
        Me.Disc1 = New CS2005.DISC()
        Me.tabPaymentHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_PaymentHist1 = New ComCtl.PaymentHistory()
        Me.Ctrl_BillingInf1 = New POSCommCtrl.Ctrl_BillingInf()
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
        Me.tabDCAR = New System.Windows.Forms.TabPage()
        Me.Dcar1 = New CS2005.DCAR()
        Me.tabUnderwriting = New System.Windows.Forms.TabPage()
        Me.UwInfo1 = New CS2005.UWInfoMcu()
        Me.tabSMS = New System.Windows.Forms.TabPage()
        Me.Sms1 = New CS2005.uclSMS_Asur()
        Me.tabPolicyAlt = New System.Windows.Forms.TabPage()
        Me.cmdPending = New System.Windows.Forms.Button()
        Me.cmdReturn = New System.Windows.Forms.Button()
        Me.PolicyAltPending1 = New CS2005.PolicyAltPendingMcu()
        Me.UclPolicyAlt1 = New CS2005.uclPolicyAltMcu()
        Me.tabCouponHistory = New System.Windows.Forms.TabPage()
        Me.CouponHist1 = New POSCommCtrl.CouponHist()
        Me.Couh1 = New CS2005.COUH()
        Me.tabDDACCD = New System.Windows.Forms.TabPage()
        Me.Ctrl_DirectDebitEnq1 = New CRS_Ctrl.ctrl_DirectDebitEnq()
        Me.Ddaccdr1 = New CS2005.DDACCDRMcu()
        Me.tabCashFlow = New System.Windows.Forms.TabPage()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.Ctrl_Sel_CO1 = New POSCommCtrl.Ctrl_Sel_CO()
        Me.CashFlow1 = New ComCtl.CashFlow()
        Me.UclCashFlow1 = New CS2005.uclCashFlow()
        Me.tabClaimsHistory = New System.Windows.Forms.TabPage()
        Me.ClaimHist1 = New CS2005.ClaimHist_Asur()
        Me.tabTransactionHistory = New System.Windows.Forms.TabPage()
        Me.Ctrl_TranHist1 = New CRS_Ctrl.ctrl_TranHist()
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.TrxHist1 = New CS2005.TrxHist()
        Me.tabCustomerHistory = New System.Windows.Forms.TabPage()
        Me.CustHist1 = New CS2005.CustHist()
        Me.tabServiceLog = New System.Windows.Forms.TabPage()
        Me.UclServiceLog1 = New CS2005.uclServiceLogMcu()
        Me.tabAgentInfo = New System.Windows.Forms.TabPage()
        Me.AgentInfo1 = New CS2005.AgentInfoMcu()
        Me.tabFundTrans = New System.Windows.Forms.TabPage()
        Me.Ctrl_FundTranHist1 = New CRS_Ctrl.ctrl_FundTranHist()
        Me.tabPayOutHist = New System.Windows.Forms.TabPage()
        Me.Ctrl_CashDividendPayoutHist1 = New POSCommCtrl.Ctrl_CashDividendPayoutHist()
        Me.tabPolicyNotes = New System.Windows.Forms.TabPage()
        Me.PolicyNote1 = New ComCtl.PolicyNote()
        Me.tabSubAcc = New System.Windows.Forms.TabPage()
        Me.Ctrl_FinancialDtl1 = New CRS_Ctrl.ctrl_FinancialDtl()
        Me.Ctrl_FinancialInfo1 = New CRS_Ctrl.ctrl_FinancialInfo()
        Me.tabParSur = New System.Windows.Forms.TabPage()
        Me.UclParSur1 = New CS2005.uclParSur()
        Me.tabiLASSMS = New System.Windows.Forms.TabPage()
        Me.SMSiLAS = New CS2005.SMSiLAS()
        Me.tabPostSalesCall = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.UclPostSalesCallQuestionnaire1 = New CS2005.uclPostSalesCallQuestionnaireMcu()
        Me.tabTapNGo = New System.Windows.Forms.TabPage()
        Me.Ctrl_TapNGo1 = New CRS_Ctrl.ctrl_TapNGo()
        Me.tabOePay = New System.Windows.Forms.TabPage()
        Me.Ctrl_OePay1 = New CRS_Ctrl.ctrl_OePay()
        Me.tabAsurPolicySummary = New System.Windows.Forms.TabPage()
        Me.UclPolicySummary_Asur1 = New CS2005.uclPolicySummary_Asur()
        Me.tabAsurServiceLog = New System.Windows.Forms.TabPage()
        'Lawrence 2024-12-04 for IUL
        Me.tabIULControl = New System.Windows.Forms.TabPage()
        '
        ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
        'Me.UclServiceLog2 = New CS2005.uclServiceLog_Asur()
        Me.UclServiceLog2 = New CS2005.uclServiceLog_Asur_WorkAround()

        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEstatement = New System.Windows.Forms.TextBox()
        Me.lblCapsilPolNo = New System.Windows.Forms.Label()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.lblMacauBanner = New System.Windows.Forms.Label()
        Me.lblEPolicy = New System.Windows.Forms.Label()
        Me.txtEPolicy = New System.Windows.Forms.TextBox()
        Me.notificationworker = New System.ComponentModel.BackgroundWorker()
        Me.tabTraditionalPartialSurrQuot = New System.Windows.Forms.TabPage()
        Me.Ctrl_Sel_CO2 = New POSCommCtrl.Ctrl_Sel_CO()
        Me.Ctrl_POS_ParSurTradition1 = New POSCommCtrl.Ctrl_POS_ParSurTradition()
        Me.tcPolicy.SuspendLayout()
        Me.tabPolicySummary.SuspendLayout()
        Me.tabUTRH.SuspendLayout()
        Me.tabUTRS.SuspendLayout()
        Me.tabCoverageDetails.SuspendLayout()
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
        Me.tabAgentInfo.SuspendLayout()
        Me.tabFundTrans.SuspendLayout()
        Me.tabPayOutHist.SuspendLayout()
        Me.tabPolicyNotes.SuspendLayout()
        Me.tabSubAcc.SuspendLayout()
        Me.tabParSur.SuspendLayout()
        Me.tabiLASSMS.SuspendLayout()
        Me.tabPostSalesCall.SuspendLayout()
        Me.tabTapNGo.SuspendLayout()
        Me.tabOePay.SuspendLayout()
        Me.tabAsurPolicySummary.SuspendLayout()
        Me.tabAsurServiceLog.SuspendLayout()
        Me.tabTraditionalPartialSurrQuot.SuspendLayout()
        'IUL
        Me.tabIULControl.SuspendLayout()
        '
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
        Me.tcPolicy.Controls.Add(Me.tabSubAcc)
        Me.tcPolicy.Controls.Add(Me.tabParSur)
        Me.tcPolicy.Controls.Add(Me.tabiLASSMS)
        Me.tcPolicy.Controls.Add(Me.tabPostSalesCall)
        Me.tcPolicy.Controls.Add(Me.tabTapNGo)
        Me.tcPolicy.Controls.Add(Me.tabOePay)
        Me.tcPolicy.Controls.Add(Me.tabAsurPolicySummary)
        Me.tcPolicy.Controls.Add(Me.tabAsurServiceLog)
        Me.tcPolicy.Controls.Add(Me.tabTraditionalPartialSurrQuot)
        Me.tcPolicy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcPolicy.HotTrack = True
        Me.tcPolicy.ImageList = Me.ImageList1
        Me.tcPolicy.Location = New System.Drawing.Point(11, 54)
        Me.tcPolicy.Multiline = True
        Me.tcPolicy.Name = "tcPolicy"
        Me.tcPolicy.SelectedIndex = 0
        Me.tcPolicy.Size = New System.Drawing.Size(1284, 779)
        Me.tcPolicy.TabIndex = 1
        Me.tcPolicy.TabStop = False
        Me.tcPolicy.Tag = "Policy Information"
        '
        'tabPolicySummary
        '
        Me.tabPolicySummary.AutoScroll = True
        Me.tabPolicySummary.BackColor = System.Drawing.Color.Transparent
        Me.tabPolicySummary.Controls.Add(Me.Ctrl_CRSPolicyGeneral_Information1)
        Me.tabPolicySummary.Controls.Add(Me.lblNCVUpdateDate)
        Me.tabPolicySummary.Controls.Add(Me.txtNCVUpdateDate)
        Me.tabPolicySummary.Controls.Add(Me.lblNCVUpdateUser)
        Me.tabPolicySummary.Controls.Add(Me.txtNCVUpdateUser)
        Me.tabPolicySummary.Controls.Add(Me.cboNCVReminderFlag)
        Me.tabPolicySummary.Controls.Add(Me.lblNCVReminderFlag)
        Me.tabPolicySummary.Controls.Add(Me.lblPolicyNo)
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.tabPolicySummary.Controls.Add(Me.PolicySummary1)
        Me.tabPolicySummary.Controls.Add(Me.AddressSelect1)
        Me.tabPolicySummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPolicySummary.ForeColor = System.Drawing.Color.Black
        Me.tabPolicySummary.ImageIndex = 3
        Me.tabPolicySummary.Location = New System.Drawing.Point(4, 61)
        Me.tabPolicySummary.Name = "tabPolicySummary"
        Me.tabPolicySummary.Size = New System.Drawing.Size(1276, 714)
        Me.tabPolicySummary.TabIndex = 0
        Me.tabPolicySummary.Tag = "Policy Summary"
        Me.tabPolicySummary.Text = "Policy Summary"
        Me.tabPolicySummary.UseVisualStyleBackColor = True
        '
        'Ctrl_CRSPolicyGeneral_Information1
        '
        Me.Ctrl_CRSPolicyGeneral_Information1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_CRSPolicyGeneral_Information1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_CRSPolicyGeneral_Information1.Name = "Ctrl_CRSPolicyGeneral_Information1"
        Me.Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = ""
        Me.Ctrl_CRSPolicyGeneral_Information1.Size = New System.Drawing.Size(1033, 725)
        Me.Ctrl_CRSPolicyGeneral_Information1.SystemInUse = Nothing
        Me.Ctrl_CRSPolicyGeneral_Information1.TabIndex = 0
        '
        'lblNCVUpdateDate
        '
        Me.lblNCVUpdateDate.AutoSize = True
        Me.lblNCVUpdateDate.Location = New System.Drawing.Point(631, 282)
        Me.lblNCVUpdateDate.Name = "lblNCVUpdateDate"
        Me.lblNCVUpdateDate.Size = New System.Drawing.Size(106, 13)
        Me.lblNCVUpdateDate.TabIndex = 132
        Me.lblNCVUpdateDate.Text = "Latest Update Date: "
        '
        'txtNCVUpdateDate
        '
        Me.txtNCVUpdateDate.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNCVUpdateDate.Location = New System.Drawing.Point(737, 278)
        Me.txtNCVUpdateDate.MaxLength = 10
        Me.txtNCVUpdateDate.Name = "txtNCVUpdateDate"
        Me.txtNCVUpdateDate.ReadOnly = True
        Me.txtNCVUpdateDate.Size = New System.Drawing.Size(100, 20)
        Me.txtNCVUpdateDate.TabIndex = 131
        '
        'lblNCVUpdateUser
        '
        Me.lblNCVUpdateUser.AutoSize = True
        Me.lblNCVUpdateUser.Location = New System.Drawing.Point(426, 282)
        Me.lblNCVUpdateUser.Name = "lblNCVUpdateUser"
        Me.lblNCVUpdateUser.Size = New System.Drawing.Size(93, 13)
        Me.lblNCVUpdateUser.TabIndex = 130
        Me.lblNCVUpdateUser.Text = "Last Update User:"
        '
        'txtNCVUpdateUser
        '
        Me.txtNCVUpdateUser.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNCVUpdateUser.Location = New System.Drawing.Point(520, 278)
        Me.txtNCVUpdateUser.MaxLength = 10
        Me.txtNCVUpdateUser.Name = "txtNCVUpdateUser"
        Me.txtNCVUpdateUser.ReadOnly = True
        Me.txtNCVUpdateUser.Size = New System.Drawing.Size(100, 20)
        Me.txtNCVUpdateUser.TabIndex = 129
        '
        'cboNCVReminderFlag
        '
        Me.cboNCVReminderFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNCVReminderFlag.Enabled = False
        Me.cboNCVReminderFlag.FormattingEnabled = True
        Me.cboNCVReminderFlag.Location = New System.Drawing.Point(198, 278)
        Me.cboNCVReminderFlag.Name = "cboNCVReminderFlag"
        Me.cboNCVReminderFlag.Size = New System.Drawing.Size(221, 21)
        Me.cboNCVReminderFlag.TabIndex = 128
        '
        'lblNCVReminderFlag
        '
        Me.lblNCVReminderFlag.AutoSize = True
        Me.lblNCVReminderFlag.Location = New System.Drawing.Point(15, 282)
        Me.lblNCVReminderFlag.Name = "lblNCVReminderFlag"
        Me.lblNCVReminderFlag.Size = New System.Drawing.Size(181, 13)
        Me.lblNCVReminderFlag.TabIndex = 127
        Me.lblNCVReminderFlag.Text = "Negative Cash Value Reminder Flag:"
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(55, 34)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(62, 13)
        Me.lblPolicyNo.TabIndex = 5
        Me.lblPolicyNo.Text = "StrPolicyNo"
        '
        'PolicySummary1
        '
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.PolicySummary1.Location = New System.Drawing.Point(11, 3)
        'Me.PolicySummary1.Name = "PolicySummary1"
        'Me.PolicySummary1.PolicyAccountID = Nothing
        'Me.PolicySummary1.Size = New System.Drawing.Size(728, 291)
        'Me.PolicySummary1.TabIndex = 0
        '
        'AddressSelect1
        '
        Me.AddressSelect1.Location = New System.Drawing.Point(11, 306)
        Me.AddressSelect1.Name = "AddressSelect1"
        Me.AddressSelect1.Size = New System.Drawing.Size(720, 338)
        Me.AddressSelect1.TabIndex = 4
        '
        'tabUTRH
        '
        Me.tabUTRH.AutoScroll = True
        Me.tabUTRH.Controls.Add(Me.Ctrl_FundHolding1)
        Me.tabUTRH.Controls.Add(Me.UclUTRH1)
        Me.tabUTRH.Location = New System.Drawing.Point(4, 23)
        Me.tabUTRH.Name = "tabUTRH"
        Me.tabUTRH.Size = New System.Drawing.Size(192, 73)
        Me.tabUTRH.TabIndex = 20
        Me.tabUTRH.Tag = "Unit Tx History"
        Me.tabUTRH.Text = "Unit Tx History"
        Me.tabUTRH.UseVisualStyleBackColor = True
        '
        'Ctrl_FundHolding1
        '
        Me.Ctrl_FundHolding1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_FundHolding1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FundHolding1.Name = "Ctrl_FundHolding1"
        Me.Ctrl_FundHolding1.PolicyinUse = Nothing
        Me.Ctrl_FundHolding1.Size = New System.Drawing.Size(1054, 606)
        Me.Ctrl_FundHolding1.TabIndex = 1
        '
        'UclUTRH1
        '
        Me.UclUTRH1.Location = New System.Drawing.Point(0, 0)
        Me.UclUTRH1.Name = "UclUTRH1"
        Me.UclUTRH1.Size = New System.Drawing.Size(740, 500)
        Me.UclUTRH1.TabIndex = 0
        '
        'tabUTRS
        '
        Me.tabUTRS.Controls.Add(Me.Ctrl_FundTranSummary1)
        Me.tabUTRS.Location = New System.Drawing.Point(4, 42)
        Me.tabUTRS.Name = "tabUTRS"
        Me.tabUTRS.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUTRS.Size = New System.Drawing.Size(192, 54)
        Me.tabUTRS.TabIndex = 30
        Me.tabUTRS.Tag = "Unit Tx Summary"
        Me.tabUTRS.Text = "Unit Tx Summary"
        Me.tabUTRS.UseVisualStyleBackColor = True
        '
        'Ctrl_FundTranSummary1
        '
        Me.Ctrl_FundTranSummary1.AutoSize = True
        Me.Ctrl_FundTranSummary1.BasicInsured = Nothing
        Me.Ctrl_FundTranSummary1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_FundTranSummary1.Name = "Ctrl_FundTranSummary1"
        Me.Ctrl_FundTranSummary1.PolicyinUse = Nothing
        Me.Ctrl_FundTranSummary1.Size = New System.Drawing.Size(1035, 587)
        Me.Ctrl_FundTranSummary1.TabIndex = 0
        '
        'tabCoverageDetails
        '
        Me.tabCoverageDetails.AutoScroll = True
        Me.tabCoverageDetails.Controls.Add(Me.Ctrl_ChgComponent1)
        Me.tabCoverageDetails.Controls.Add(Me.Coverage1)
        Me.tabCoverageDetails.ImageIndex = 3
        Me.tabCoverageDetails.Location = New System.Drawing.Point(4, 61)
        Me.tabCoverageDetails.Name = "tabCoverageDetails"
        Me.tabCoverageDetails.Size = New System.Drawing.Size(192, 35)
        Me.tabCoverageDetails.TabIndex = 1
        Me.tabCoverageDetails.Tag = "Coverage Details"
        Me.tabCoverageDetails.Text = "Coverage Details"
        Me.tabCoverageDetails.UseVisualStyleBackColor = True
        Me.tabCoverageDetails.Visible = False
        '
        'Ctrl_ChgComponent1
        '
        Me.Ctrl_ChgComponent1.BillTypeInUse = ""
        Me.Ctrl_ChgComponent1.CoolingOffDateInUse = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.Ctrl_ChgComponent1.CovNoInuse = ""
        Me.Ctrl_ChgComponent1.CurrdsInUse = Nothing
        Me.Ctrl_ChgComponent1.EffDateInUse = New Date(CType(0, Long))
        Me.Ctrl_ChgComponent1.FormCanUpgradeOrConverse = False
        Me.Ctrl_ChgComponent1.LifeNoInUse = ""
        Me.Ctrl_ChgComponent1.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_ChgComponent1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_ChgComponent1.ModeInUse = Utility.Utility.ModeName.Blank
        Me.Ctrl_ChgComponent1.Name = "Ctrl_ChgComponent1"
        Me.Ctrl_ChgComponent1.PModeInUse = ""
        Me.Ctrl_ChgComponent1.PolicyNoInuse = ""
        Me.Ctrl_ChgComponent1.RiderInuse = ""
        Me.Ctrl_ChgComponent1.setFundHolding = False
        Me.Ctrl_ChgComponent1.Size = New System.Drawing.Size(967, 542)
        Me.Ctrl_ChgComponent1.SystemInUse = Nothing
        Me.Ctrl_ChgComponent1.TabIndex = 0
        '
        'Coverage1
        '
        Me.Coverage1.Location = New System.Drawing.Point(0, 0)
        Me.Coverage1.Name = "Coverage1"
        Me.Coverage1.PolicyAccountID = Nothing
        Me.Coverage1.Size = New System.Drawing.Size(728, 720)
        Me.Coverage1.TabIndex = 0
        '
        'tabFinancialInfo
        '
        Me.tabFinancialInfo.AutoScroll = True
        Me.tabFinancialInfo.Controls.Add(Me.Label6)
        Me.tabFinancialInfo.Controls.Add(Me.Button1)
        Me.tabFinancialInfo.Controls.Add(Me.DateTimePicker1)
        Me.tabFinancialInfo.Controls.Add(Me.PolicyValue1)
        Me.tabFinancialInfo.Controls.Add(Me.FinancialInfo1)
        Me.tabFinancialInfo.Location = New System.Drawing.Point(4, 80)
        Me.tabFinancialInfo.Name = "tabFinancialInfo"
        Me.tabFinancialInfo.Size = New System.Drawing.Size(192, 16)
        Me.tabFinancialInfo.TabIndex = 9
        Me.tabFinancialInfo.Tag = "Financial Info"
        Me.tabFinancialInfo.Text = "Financial Information"
        Me.tabFinancialInfo.UseVisualStyleBackColor = True
        Me.tabFinancialInfo.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Effective Date"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(203, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 20)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Go"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(93, 8)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'PolicyValue1
        '
        Me.PolicyValue1.Location = New System.Drawing.Point(3, 34)
        Me.PolicyValue1.Margin = New System.Windows.Forms.Padding(4)
        Me.PolicyValue1.Name = "PolicyValue1"
        Me.PolicyValue1.RpuOriginalSI = ""
        Me.PolicyValue1.RpuPaidToDate = ""
        Me.PolicyValue1.RpuQuoteAmount = ""
        Me.PolicyValue1.RpuRefund = ""
        Me.PolicyValue1.Size = New System.Drawing.Size(1008, 404)
        Me.PolicyValue1.TabIndex = 1
        '
        'FinancialInfo1
        '
        Me.FinancialInfo1.Location = New System.Drawing.Point(0, 0)
        Me.FinancialInfo1.Name = "FinancialInfo1"
        Me.FinancialInfo1.Size = New System.Drawing.Size(724, 368)
        Me.FinancialInfo1.TabIndex = 0
        '
        'tabMClaimsHistory
        '
        Me.tabMClaimsHistory.AutoScroll = True
        Me.tabMClaimsHistory.Controls.Add(Me.MClaimHist1)
        Me.tabMClaimsHistory.Location = New System.Drawing.Point(4, 99)
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
        Me.MClaimHist1.Size = New System.Drawing.Size(728, 736)
        Me.MClaimHist1.TabIndex = 0
        '
        'tabDISC
        '
        Me.tabDISC.Controls.Add(Me.Disc1)
        Me.tabDISC.Location = New System.Drawing.Point(4, 118)
        Me.tabDISC.Name = "tabDISC"
        Me.tabDISC.Size = New System.Drawing.Size(192, 0)
        Me.tabDISC.TabIndex = 19
        Me.tabDISC.Tag = "No Claim Discount"
        Me.tabDISC.Text = "No Claim Discount"
        Me.tabDISC.UseVisualStyleBackColor = True
        '
        'Disc1
        '
        Me.Disc1.Location = New System.Drawing.Point(0, 0)
        Me.Disc1.Name = "Disc1"
        Me.Disc1.Size = New System.Drawing.Size(572, 184)
        Me.Disc1.TabIndex = 0
        '
        'tabPaymentHistory
        '
        Me.tabPaymentHistory.AutoScroll = True
        Me.tabPaymentHistory.Controls.Add(Me.Ctrl_PaymentHist1)
        Me.tabPaymentHistory.Controls.Add(Me.Ctrl_BillingInf1)
        Me.tabPaymentHistory.Controls.Add(Me.Ctrl_AutoRegularWithdrawal)
        Me.tabPaymentHistory.ImageIndex = 3
        Me.tabPaymentHistory.Location = New System.Drawing.Point(4, 137)
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
        Me.Ctrl_PaymentHist1.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_PaymentHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_PaymentHist1.Name = "Ctrl_PaymentHist1"
        Me.Ctrl_PaymentHist1.PolicyNoInUse = ""
        Me.Ctrl_PaymentHist1.Size = New System.Drawing.Size(1015, 238)
        Me.Ctrl_PaymentHist1.TabIndex = 3
        '
        'Ctrl_BillingInf1
        '
        Me.Ctrl_BillingInf1.AllowQuote = False
        Me.Ctrl_BillingInf1.AllowUpdate = False
        Me.Ctrl_BillingInf1.BillTypeInUse = ""
        Me.Ctrl_BillingInf1.BussDateNow = New Date(CType(0, Long))
        Me.Ctrl_BillingInf1.CurrdsInUse = Nothing
        Me.Ctrl_BillingInf1.EffDateInUse = New Date(CType(0, Long))
        Me.Ctrl_BillingInf1.Location = New System.Drawing.Point(7, 245)
        Me.Ctrl_BillingInf1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_BillingInf1.Name = "Ctrl_BillingInf1"
        Me.Ctrl_BillingInf1.NewDrawDay = ""
        Me.Ctrl_BillingInf1.NewManRef = ""
        Me.Ctrl_BillingInf1.NewPayor = ""
        Me.Ctrl_BillingInf1.NewPayorLAID = ""
        Me.Ctrl_BillingInf1.PModeInUse = "Enquiry"
        Me.Ctrl_BillingInf1.PolicyNoInUse = ""
        Me.Ctrl_BillingInf1.Size = New System.Drawing.Size(1024, 343)
        Me.Ctrl_BillingInf1.TabIndex = 2
        '
        'Ctrl_AutoRegularWithdrawal
        '
        Me.Ctrl_AutoRegularWithdrawal.Location = New System.Drawing.Point(1038, 254)
        Me.Ctrl_AutoRegularWithdrawal.Name = "Ctrl_AutoRegularWithdrawal"
        Me.Ctrl_AutoRegularWithdrawal.OwnerNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawal.PolicyNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawal.Size = New System.Drawing.Size(510, 300)
        Me.Ctrl_AutoRegularWithdrawal.TabIndex = 4
        '
        'tabLevyHistory
        '
        Me.tabLevyHistory.AutoScroll = True
        Me.tabLevyHistory.Controls.Add(Me.Ctrl_LevyHistory)
        Me.tabLevyHistory.Location = New System.Drawing.Point(4, 137)
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
        Me.Ctrl_LevyHistory.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_LevyHistory.Name = "Ctrl_LevyHistory"
        Me.Ctrl_LevyHistory.PolicyNoInUse = Nothing
        Me.Ctrl_LevyHistory.Size = New System.Drawing.Size(883, 778)
        Me.Ctrl_LevyHistory.TabIndex = 1
        '
        'tabARWHistory
        '
        Me.tabARWHistory.AutoScroll = True
        Me.tabARWHistory.Controls.Add(Me.Ctrl_AutoRegularWithdrawalHis)
        Me.tabARWHistory.Location = New System.Drawing.Point(4, 156)
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
        Me.Ctrl_AutoRegularWithdrawalHis.Location = New System.Drawing.Point(0, 6)
        Me.Ctrl_AutoRegularWithdrawalHis.Name = "Ctrl_AutoRegularWithdrawalHis"
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoBasic = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoDependent = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.PolicyNoInUse = Nothing
        Me.Ctrl_AutoRegularWithdrawalHis.Size = New System.Drawing.Size(1089, 406)
        Me.Ctrl_AutoRegularWithdrawalHis.TabIndex = 0
        '
        'tabAnnuityPaymentHistory
        '
        Me.tabAnnuityPaymentHistory.AutoScroll = True
        Me.tabAnnuityPaymentHistory.Controls.Add(Me.Ctrl_AnnuityPaymentHist1)
        Me.tabAnnuityPaymentHistory.ImageIndex = 3
        Me.tabAnnuityPaymentHistory.Location = New System.Drawing.Point(4, 175)
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
        Me.Ctrl_AnnuityPaymentHist1.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_AnnuityPaymentHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_AnnuityPaymentHist1.Name = "Ctrl_AnnuityPaymentHist1"
        Me.Ctrl_AnnuityPaymentHist1.PolicyNo = ""
        Me.Ctrl_AnnuityPaymentHist1.Size = New System.Drawing.Size(1015, 238)
        Me.Ctrl_AnnuityPaymentHist1.TabIndex = 21
        '
        'tabDirectCreditHistory
        '
        Me.tabDirectCreditHistory.AutoScroll = True
        Me.tabDirectCreditHistory.Controls.Add(Me.Ctrl_DirectCreditHist1)
        Me.tabDirectCreditHistory.ImageIndex = 3
        Me.tabDirectCreditHistory.Location = New System.Drawing.Point(4, 194)
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
        Me.Ctrl_DirectCreditHist1.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_DirectCreditHist1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_DirectCreditHist1.Name = "Ctrl_DirectCreditHist1"
        Me.Ctrl_DirectCreditHist1.PolicyNo = ""
        Me.Ctrl_DirectCreditHist1.Size = New System.Drawing.Size(1015, 238)
        Me.Ctrl_DirectCreditHist1.TabIndex = 21
        '
        'tabCupGetTransResult
        '
        Me.tabCupGetTransResult.AutoScroll = True
        Me.tabCupGetTransResult.Controls.Add(Me.Ctrl_CupGetTransResult)
        Me.tabCupGetTransResult.ImageIndex = 3
        Me.tabCupGetTransResult.Location = New System.Drawing.Point(4, 213)
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
        Me.Ctrl_CupGetTransResult.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_CupGetTransResult.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_CupGetTransResult.Name = "Ctrl_CupGetTransResult"
        Me.Ctrl_CupGetTransResult.policyInuse = ""
        Me.Ctrl_CupGetTransResult.Size = New System.Drawing.Size(1015, 400)
        Me.Ctrl_CupGetTransResult.TabIndex = 21
        '
        'tabAPLHistory
        '
        Me.tabAPLHistory.AutoScroll = True
        Me.tabAPLHistory.Controls.Add(Me.LoanHist1)
        Me.tabAPLHistory.Controls.Add(Me.AplHist1)
        Me.tabAPLHistory.ImageIndex = 3
        Me.tabAPLHistory.Location = New System.Drawing.Point(4, 213)
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
        Me.LoanHist1.Size = New System.Drawing.Size(1002, 529)
        Me.LoanHist1.TabIndex = 1
        '
        'AplHist1
        '
        Me.AplHist1.DateFrom = New Date(CType(0, Long))
        Me.AplHist1.Location = New System.Drawing.Point(0, 0)
        Me.AplHist1.Name = "AplHist1"
        Me.AplHist1.PolicyAccountID = Nothing
        Me.AplHist1.Size = New System.Drawing.Size(720, 352)
        Me.AplHist1.TabIndex = 0
        '
        'tabDCAR
        '
        Me.tabDCAR.AutoScroll = True
        Me.tabDCAR.Controls.Add(Me.Dcar1)
        Me.tabDCAR.ImageIndex = 3
        Me.tabDCAR.Location = New System.Drawing.Point(4, 232)
        Me.tabDCAR.Name = "tabDCAR"
        Me.tabDCAR.Size = New System.Drawing.Size(192, 0)
        Me.tabDCAR.TabIndex = 18
        Me.tabDCAR.Text = "DCAR"
        Me.tabDCAR.UseVisualStyleBackColor = True
        '
        'tabIULControl
        '
        Me.tabIULControl.AutoScroll = True
        Me.tabIULControl.Controls.Add(Me.Ctrl_POS_iULMaint1)
        Me.tabIULControl.Name = "tabIULControl"
        Me.tabIULControl.TabIndex = 18
        Me.tabIULControl.Tag = "Account/ Segment Details"
        Me.tabIULControl.Text = "Account/ Segment Details"
        Me.tabIULControl.UseVisualStyleBackColor = True
        '
        'Dcar1
        '
        Me.Dcar1.Location = New System.Drawing.Point(4, 0)
        Me.Dcar1.Name = "Dcar1"
        Me.Dcar1.Size = New System.Drawing.Size(652, 324)
        Me.Dcar1.TabIndex = 0
        '
        'tabUnderwriting
        '
        Me.tabUnderwriting.Controls.Add(Me.UwInfo1)
        Me.tabUnderwriting.Location = New System.Drawing.Point(4, 232)
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
        Me.UwInfo1.Location = New System.Drawing.Point(8, 8)
        Me.UwInfo1.Name = "UwInfo1"
        Me.UwInfo1.Size = New System.Drawing.Size(720, 523)
        Me.UwInfo1.TabIndex = 0
        '
        'tabSMS
        '
        Me.tabSMS.AutoScroll = True
        Me.tabSMS.Controls.Add(Me.Sms1)
        Me.tabSMS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSMS.Location = New System.Drawing.Point(4, 251)
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
        Me.Sms1.Location = New System.Drawing.Point(0, 0)
        Me.Sms1.Name = "Sms1"
        Me.Sms1.PolicyAccountID = Nothing
        Me.Sms1.Size = New System.Drawing.Size(800, 500)
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
        Me.tabPolicyAlt.Location = New System.Drawing.Point(4, 251)
        Me.tabPolicyAlt.Name = "tabPolicyAlt"
        Me.tabPolicyAlt.Size = New System.Drawing.Size(192, 0)
        Me.tabPolicyAlt.TabIndex = 21
        Me.tabPolicyAlt.Tag = "Policy Alternation"
        Me.tabPolicyAlt.Text = "Policy Alternation"
        Me.tabPolicyAlt.UseVisualStyleBackColor = True
        '
        'cmdPending
        '
        Me.cmdPending.Location = New System.Drawing.Point(644, 296)
        Me.cmdPending.Name = "cmdPending"
        Me.cmdPending.Size = New System.Drawing.Size(75, 23)
        Me.cmdPending.TabIndex = 99
        Me.cmdPending.Text = "Pending..."
        '
        'cmdReturn
        '
        Me.cmdReturn.Enabled = False
        Me.cmdReturn.Location = New System.Drawing.Point(644, 324)
        Me.cmdReturn.Name = "cmdReturn"
        Me.cmdReturn.Size = New System.Drawing.Size(75, 23)
        Me.cmdReturn.TabIndex = 2
        Me.cmdReturn.Text = "Return"
        Me.cmdReturn.Visible = False
        '
        'PolicyAltPending1
        '
        Me.PolicyAltPending1.Location = New System.Drawing.Point(0, 0)
        Me.PolicyAltPending1.Name = "PolicyAltPending1"
        Me.PolicyAltPending1.Size = New System.Drawing.Size(728, 360)
        Me.PolicyAltPending1.TabIndex = 1
        Me.PolicyAltPending1.Visible = False
        '
        'UclPolicyAlt1
        '
        Me.UclPolicyAlt1.Location = New System.Drawing.Point(0, 0)
        Me.UclPolicyAlt1.Name = "UclPolicyAlt1"
        Me.UclPolicyAlt1.Size = New System.Drawing.Size(728, 332)
        Me.UclPolicyAlt1.TabIndex = 0
        '
        'tabCouponHistory
        '
        Me.tabCouponHistory.Controls.Add(Me.CouponHist1)
        Me.tabCouponHistory.Controls.Add(Me.Couh1)
        Me.tabCouponHistory.ImageIndex = 3
        Me.tabCouponHistory.Location = New System.Drawing.Point(4, 270)
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
        Me.CouponHist1.Size = New System.Drawing.Size(781, 466)
        Me.CouponHist1.TabIndex = 1
        '
        'Couh1
        '
        Me.Couh1.DateFrom = New Date(CType(0, Long))
        Me.Couh1.Location = New System.Drawing.Point(0, 0)
        Me.Couh1.Name = "Couh1"
        Me.Couh1.PolicyAccountID = Nothing
        Me.Couh1.Size = New System.Drawing.Size(720, 372)
        Me.Couh1.TabIndex = 0
        '
        'tabDDACCD
        '
        Me.tabDDACCD.AutoScroll = True
        Me.tabDDACCD.Controls.Add(Me.Ctrl_DirectDebitEnq1)
        Me.tabDDACCD.Controls.Add(Me.Ddaccdr1)
        Me.tabDDACCD.ImageIndex = 3
        Me.tabDDACCD.Location = New System.Drawing.Point(4, 289)
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
        Me.Ctrl_DirectDebitEnq1.Size = New System.Drawing.Size(779, 543)
        Me.Ctrl_DirectDebitEnq1.TabIndex = 1
        '
        'Ddaccdr1
        '
        Me.Ddaccdr1.dtPriv = Nothing
        Me.Ddaccdr1.Location = New System.Drawing.Point(0, 0)
        Me.Ddaccdr1.Name = "Ddaccdr1"
        Me.Ddaccdr1.Size = New System.Drawing.Size(724, 504)
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
        Me.tabCashFlow.Location = New System.Drawing.Point(4, 289)
        Me.tabCashFlow.Name = "tabCashFlow"
        Me.tabCashFlow.Size = New System.Drawing.Size(192, 0)
        Me.tabCashFlow.TabIndex = 17
        Me.tabCashFlow.Tag = "Cash Flow"
        Me.tabCashFlow.Text = "Cash Flow"
        Me.tabCashFlow.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(917, 30)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(75, 23)
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
        Me.Ctrl_Sel_CO1.Size = New System.Drawing.Size(1024, 102)
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
        Me.CashFlow1.Location = New System.Drawing.Point(0, 99)
        Me.CashFlow1.Margin = New System.Windows.Forms.Padding(4)
        Me.CashFlow1.modeinuse = Utility.Utility.ModeName.Add
        Me.CashFlow1.Name = "CashFlow1"
        Me.CashFlow1.policynoinuse = Nothing
        Me.CashFlow1.ridernoinuse = Nothing
        Me.CashFlow1.Size = New System.Drawing.Size(656, 418)
        Me.CashFlow1.TabIndex = 1
        '
        'UclCashFlow1
        '
        Me.UclCashFlow1.Location = New System.Drawing.Point(0, 0)
        Me.UclCashFlow1.Name = "UclCashFlow1"
        Me.UclCashFlow1.Size = New System.Drawing.Size(724, 368)
        Me.UclCashFlow1.TabIndex = 0
        '
        'tabClaimsHistory
        '
        Me.tabClaimsHistory.AutoScroll = True
        Me.tabClaimsHistory.Controls.Add(Me.ClaimHist1)
        Me.tabClaimsHistory.Location = New System.Drawing.Point(4, 308)
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
        Me.ClaimHist1.Location = New System.Drawing.Point(0, 0)
        Me.ClaimHist1.Name = "ClaimHist1"
        Me.ClaimHist1.PolicyAccountID = Nothing
        Me.ClaimHist1.Size = New System.Drawing.Size(1022, 1200)
        Me.ClaimHist1.TabIndex = 0
        '
        'tabTransactionHistory
        '
        Me.tabTransactionHistory.Controls.Add(Me.Ctrl_TranHist1)
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.tabTransactionHistory.Controls.Add(Me.TrxHist1)
        Me.tabTransactionHistory.Location = New System.Drawing.Point(4, 327)
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
        Me.Ctrl_TranHist1.Size = New System.Drawing.Size(779, 489)
        Me.Ctrl_TranHist1.TabIndex = 1
        '
        'TrxHist1
        '
        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
        'Me.TrxHist1.Location = New System.Drawing.Point(0, 0)
        'Me.TrxHist1.Name = "TrxHist1"
        'Me.TrxHist1.PolicyAccountID = Nothing
        'Me.TrxHist1.Size = New System.Drawing.Size(720, 360)
        'Me.TrxHist1.TabIndex = 0
        '
        'tabCustomerHistory
        '
        Me.tabCustomerHistory.Controls.Add(Me.CustHist1)
        Me.tabCustomerHistory.ImageIndex = 3
        Me.tabCustomerHistory.Location = New System.Drawing.Point(4, 346)
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
        Me.CustHist1.Size = New System.Drawing.Size(720, 384)
        Me.CustHist1.TabIndex = 0
        '
        'tabServiceLog
        '
        Me.tabServiceLog.AutoScroll = True
        Me.tabServiceLog.Controls.Add(Me.UclServiceLog1)
        Me.tabServiceLog.Location = New System.Drawing.Point(4, 346)
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
        Me.UclServiceLog1.CustomerID = Nothing
        Me.UclServiceLog1.Location = New System.Drawing.Point(0, 0)
        Me.UclServiceLog1.Name = "UclServiceLog1"
        Me.UclServiceLog1.PolicyType = Nothing
        Me.UclServiceLog1.Size = New System.Drawing.Size(1420, 800)
        Me.UclServiceLog1.TabIndex = 0
        '
        'tabAgentInfo
        '
        Me.tabAgentInfo.Controls.Add(Me.AgentInfo1)
        Me.tabAgentInfo.Location = New System.Drawing.Point(4, 365)
        Me.tabAgentInfo.Name = "tabAgentInfo"
        Me.tabAgentInfo.Size = New System.Drawing.Size(192, 0)
        Me.tabAgentInfo.TabIndex = 10
        Me.tabAgentInfo.Tag = "Agent Info"
        Me.tabAgentInfo.Text = "Agent Information"
        Me.tabAgentInfo.UseVisualStyleBackColor = True
        Me.tabAgentInfo.Visible = False
        '
        'AgentInfo1
        '
        Me.AgentInfo1.Location = New System.Drawing.Point(0, 0)
        Me.AgentInfo1.Name = "AgentInfo1"
        Me.AgentInfo1.Size = New System.Drawing.Size(720, 428)
        Me.AgentInfo1.TabIndex = 0
        '
        'tabFundTrans
        '
        Me.tabFundTrans.Controls.Add(Me.Ctrl_FundTranHist1)
        Me.tabFundTrans.Location = New System.Drawing.Point(4, 384)
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
        Me.Ctrl_FundTranHist1.Size = New System.Drawing.Size(877, 562)
        Me.Ctrl_FundTranHist1.TabIndex = 0
        '
        'tabPayOutHist
        '
        Me.tabPayOutHist.Controls.Add(Me.Ctrl_CashDividendPayoutHist1)
        Me.tabPayOutHist.Location = New System.Drawing.Point(4, 403)
        Me.tabPayOutHist.Name = "tabPayOutHist"
        Me.tabPayOutHist.Size = New System.Drawing.Size(192, 0)
        Me.tabPayOutHist.TabIndex = 32
        Me.tabPayOutHist.Text = "Pay Out History"
        Me.tabPayOutHist.UseVisualStyleBackColor = True
        '
        'Ctrl_CashDividendPayoutHist1
        '
        Me.Ctrl_CashDividendPayoutHist1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_CashDividendPayoutHist1.Name = "Ctrl_CashDividendPayoutHist1"
        Me.Ctrl_CashDividendPayoutHist1.PolicyNo = ""
        Me.Ctrl_CashDividendPayoutHist1.Size = New System.Drawing.Size(1044, 396)
        Me.Ctrl_CashDividendPayoutHist1.TabIndex = 0
        '
        'tabPolicyNotes
        '
        Me.tabPolicyNotes.AutoScroll = True
        Me.tabPolicyNotes.Controls.Add(Me.PolicyNote1)
        Me.tabPolicyNotes.Location = New System.Drawing.Point(4, 61)
        Me.tabPolicyNotes.Name = "tabPolicyNotes"
        Me.tabPolicyNotes.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPolicyNotes.Size = New System.Drawing.Size(1276, 714)
        Me.tabPolicyNotes.TabIndex = 24
        Me.tabPolicyNotes.Text = "Policy Notes"
        Me.tabPolicyNotes.UseVisualStyleBackColor = True
        '
        'PolicyNote1
        '
        Me.PolicyNote1.AllowEdit = False
        Me.PolicyNote1.DataAdaptor = PolicyNoteDefaultAdaptor1
        Me.PolicyNote1.Location = New System.Drawing.Point(0, 1)
        Me.PolicyNote1.Margin = New System.Windows.Forms.Padding(4)
        Me.PolicyNote1.Name = "PolicyNote1"
        Me.PolicyNote1.Size = New System.Drawing.Size(815, 632)
        Me.PolicyNote1.TabIndex = 0
        '
        'tabSubAcc
        '
        Me.tabSubAcc.AutoScroll = True
        Me.tabSubAcc.Controls.Add(Me.Ctrl_FinancialDtl1)
        Me.tabSubAcc.Controls.Add(Me.Ctrl_FinancialInfo1)
        Me.tabSubAcc.Location = New System.Drawing.Point(4, 422)
        Me.tabSubAcc.Name = "tabSubAcc"
        Me.tabSubAcc.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSubAcc.Size = New System.Drawing.Size(192, 0)
        Me.tabSubAcc.TabIndex = 25
        Me.tabSubAcc.Text = "Sub-Account Balance"
        Me.tabSubAcc.UseVisualStyleBackColor = True
        '
        'Ctrl_FinancialDtl1
        '
        Me.Ctrl_FinancialDtl1.Location = New System.Drawing.Point(3, 485)
        Me.Ctrl_FinancialDtl1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Enquiry
        Me.Ctrl_FinancialDtl1.Name = "Ctrl_FinancialDtl1"
        Me.Ctrl_FinancialDtl1.PolicyNoInUse = ""
        Me.Ctrl_FinancialDtl1.Size = New System.Drawing.Size(920, 269)
        Me.Ctrl_FinancialDtl1.TabIndex = 3
        '
        'Ctrl_FinancialInfo1
        '
        Me.Ctrl_FinancialInfo1.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_FinancialInfo1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_FinancialInfo1.Name = "Ctrl_FinancialInfo1"
        Me.Ctrl_FinancialInfo1.PolicyNoInUse = ""
        Me.Ctrl_FinancialInfo1.Size = New System.Drawing.Size(779, 489)
        Me.Ctrl_FinancialInfo1.TabIndex = 2
        '
        'tabParSur
        '
        Me.tabParSur.Controls.Add(Me.UclParSur1)
        Me.tabParSur.Location = New System.Drawing.Point(4, 441)
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
        Me.UclParSur1.Location = New System.Drawing.Point(3, 6)
        Me.UclParSur1.Margin = New System.Windows.Forms.Padding(4)
        Me.UclParSur1.Name = "UclParSur1"
        Me.UclParSur1.PolicyNumber = Nothing
        Me.UclParSur1.Size = New System.Drawing.Size(1024, 508)
        Me.UclParSur1.TabIndex = 0
        '
        'tabiLASSMS
        '
        Me.tabiLASSMS.AutoScroll = True
        Me.tabiLASSMS.Controls.Add(Me.SMSiLAS)
        Me.tabiLASSMS.Location = New System.Drawing.Point(4, 441)
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
        Me.SMSiLAS.Location = New System.Drawing.Point(22, 6)
        Me.SMSiLAS.Name = "SMSiLAS"
        Me.SMSiLAS.PolicyAccountID = Nothing
        Me.SMSiLAS.Size = New System.Drawing.Size(687, 504)
        Me.SMSiLAS.TabIndex = 0
        '
        'tabPostSalesCall
        '
        Me.tabPostSalesCall.Controls.Add(Me.Label7)
        Me.tabPostSalesCall.Controls.Add(Me.UclPostSalesCallQuestionnaire1)
        Me.tabPostSalesCall.Location = New System.Drawing.Point(4, 460)
        Me.tabPostSalesCall.Name = "tabPostSalesCall"
        Me.tabPostSalesCall.Size = New System.Drawing.Size(192, 0)
        Me.tabPostSalesCall.TabIndex = 28
        Me.tabPostSalesCall.Text = "Post Sales Call"
        Me.tabPostSalesCall.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Basic Plan/Rider"
        '
        'UclPostSalesCallQuestionnaire1
        '
        Me.UclPostSalesCallQuestionnaire1.Location = New System.Drawing.Point(13, 29)
        Me.UclPostSalesCallQuestionnaire1.Name = "UclPostSalesCallQuestionnaire1"
        Me.UclPostSalesCallQuestionnaire1.PolicyAccountID = Nothing
        Me.UclPostSalesCallQuestionnaire1.Size = New System.Drawing.Size(923, 545)
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
        Me.tabTapNGo.Location = New System.Drawing.Point(4, 441)
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
        Me.Ctrl_TapNGo1.Name = "Ctrl_TapNGo1"
        Me.Ctrl_TapNGo1.PolicyNo = ""
        Me.Ctrl_TapNGo1.PolicyNoInUse = ""
        Me.Ctrl_TapNGo1.Size = New System.Drawing.Size(1033, 725)
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
        Me.tabOePay.Location = New System.Drawing.Point(4, 460)
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
        Me.Ctrl_OePay1.MerchantID = ""
        Me.Ctrl_OePay1.Name = "Ctrl_OePay1"
        Me.Ctrl_OePay1.PolicyNo = ""
        Me.Ctrl_OePay1.PolicyNoInUse = ""
        Me.Ctrl_OePay1.ProductID = ""
        Me.Ctrl_OePay1.Size = New System.Drawing.Size(1033, 725)
        Me.Ctrl_OePay1.StartDate = ""
        Me.Ctrl_OePay1.TabIndex = 0
        '
        'tabAsurPolicySummary
        '
        Me.tabAsurPolicySummary.Controls.Add(Me.UclPolicySummary_Asur1)
        Me.tabAsurPolicySummary.Location = New System.Drawing.Point(4, 479)
        Me.tabAsurPolicySummary.Name = "tabAsurPolicySummary"
        Me.tabAsurPolicySummary.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAsurPolicySummary.Size = New System.Drawing.Size(192, 0)
        Me.tabAsurPolicySummary.TabIndex = 32
        Me.tabAsurPolicySummary.Text = "Policy Summary (Assurance)"
        Me.tabAsurPolicySummary.UseVisualStyleBackColor = True
        '
        'UclPolicySummary_Asur1
        '
        Me.UclPolicySummary_Asur1.Location = New System.Drawing.Point(3, 2)
        Me.UclPolicySummary_Asur1.Name = "UclPolicySummary_Asur1"
        Me.UclPolicySummary_Asur1.PolicyNumber = Nothing
        Me.UclPolicySummary_Asur1.Size = New System.Drawing.Size(1089, 601)
        Me.UclPolicySummary_Asur1.TabIndex = 0
        '
        'tabAsurServiceLog
        '
        Me.tabAsurServiceLog.Controls.Add(Me.UclServiceLog2)
        Me.tabAsurServiceLog.Location = New System.Drawing.Point(4, 498)
        Me.tabAsurServiceLog.Name = "tabAsurServiceLog"
        Me.tabAsurServiceLog.Size = New System.Drawing.Size(192, 0)
        Me.tabAsurServiceLog.TabIndex = 33
        Me.tabAsurServiceLog.Text = "Service Log (Assurance)"
        Me.tabAsurServiceLog.UseVisualStyleBackColor = True
        '
        'UclServiceLog2
        '
        Me.UclServiceLog2.CustomerID = Nothing
        Me.UclServiceLog2.Location = New System.Drawing.Point(0, 0)
        Me.UclServiceLog2.Name = "UclServiceLog2"
        Me.UclServiceLog2.PolicyType = Nothing
        Me.UclServiceLog2.Size = New System.Drawing.Size(1750, 750)
        Me.UclServiceLog2.TabIndex = 0
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
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(235, 28)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(108, 20)
        Me.txtStatus.TabIndex = 2
        '
        'txtPolicy
        '
        Me.txtPolicy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicy.Location = New System.Drawing.Point(108, 28)
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.ReadOnly = True
        Me.txtPolicy.Size = New System.Drawing.Size(80, 20)
        Me.txtPolicy.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(48, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Policy No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(195, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Status"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(352, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Product"
        '
        'txtProduct
        '
        Me.txtProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtProduct.Location = New System.Drawing.Point(400, 28)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.ReadOnly = True
        Me.txtProduct.Size = New System.Drawing.Size(189, 20)
        Me.txtProduct.TabIndex = 7
        '
        'txtChiName
        '
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtChiName.Location = New System.Drawing.Point(564, 4)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.Size = New System.Drawing.Size(92, 23)
        Me.txtChiName.TabIndex = 8
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Location = New System.Drawing.Point(372, 4)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(188, 20)
        Me.txtFirstName.TabIndex = 9
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Location = New System.Drawing.Point(192, 4)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(176, 20)
        Me.txtLastName.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
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
        '
        'lblAPL
        '
        Me.lblAPL.AutoSize = True
        Me.lblAPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPL.ForeColor = System.Drawing.Color.Blue
        Me.lblAPL.Location = New System.Drawing.Point(664, 4)
        Me.lblAPL.Name = "lblAPL"
        Me.lblAPL.Size = New System.Drawing.Size(32, 15)
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
        Me.lblLastAPL.Location = New System.Drawing.Point(664, 4)
        Me.lblLastAPL.Name = "lblLastAPL"
        Me.lblLastAPL.Size = New System.Drawing.Size(60, 15)
        Me.lblLastAPL.TabIndex = 13
        Me.lblLastAPL.Text = "Last APL"
        Me.lblLastAPL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLastAPL.Visible = False
        '
        'txtCName
        '
        Me.txtCName.Location = New System.Drawing.Point(108, 4)
        Me.txtCName.Name = "txtCName"
        Me.txtCName.Size = New System.Drawing.Size(332, 20)
        Me.txtCName.TabIndex = 14
        Me.txtCName.Visible = False
        '
        'txtCNameChi
        '
        Me.txtCNameChi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCNameChi.Location = New System.Drawing.Point(444, 4)
        Me.txtCNameChi.Name = "txtCNameChi"
        Me.txtCNameChi.Size = New System.Drawing.Size(212, 23)
        Me.txtCNameChi.TabIndex = 15
        Me.txtCNameChi.Visible = False
        '
        'txtBillNo
        '
        Me.txtBillNo.Location = New System.Drawing.Point(639, 28)
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.Size = New System.Drawing.Size(85, 20)
        Me.txtBillNo.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(594, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Bill No."
        '
        'lblDateFormatMsg
        '
        Me.lblDateFormatMsg.AutoSize = True
        Me.lblDateFormatMsg.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblDateFormatMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDateFormatMsg.Location = New System.Drawing.Point(1040, 0)
        Me.lblDateFormatMsg.Name = "lblDateFormatMsg"
        Me.lblDateFormatMsg.Size = New System.Drawing.Size(244, 13)
        Me.lblDateFormatMsg.TabIndex = 18
        Me.lblDateFormatMsg.Text = "All short date is in format - ""mm/dd/yyyy"" "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(740, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "e-Statement"
        '
        'txtEstatement
        '
        Me.txtEstatement.Location = New System.Drawing.Point(810, 27)
        Me.txtEstatement.Name = "txtEstatement"
        Me.txtEstatement.ReadOnly = True
        Me.txtEstatement.Size = New System.Drawing.Size(29, 20)
        Me.txtEstatement.TabIndex = 19
        '
        'lblCapsilPolNo
        '
        Me.lblCapsilPolNo.AutoSize = True
        Me.lblCapsilPolNo.ForeColor = System.Drawing.Color.Green
        Me.lblCapsilPolNo.Location = New System.Drawing.Point(739, 7)
        Me.lblCapsilPolNo.Name = "lblCapsilPolNo"
        Me.lblCapsilPolNo.Size = New System.Drawing.Size(13, 13)
        Me.lblCapsilPolNo.TabIndex = 21
        Me.lblCapsilPolNo.Text = "--"
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(845, 4)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(13, 13)
        Me.lblMsg.TabIndex = 26
        Me.lblMsg.Text = ""
        '
        'lblMacauBanner
        '
        Me.lblMacauBanner.AutoSize = True
        Me.lblMacauBanner.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMacauBanner.ForeColor = System.Drawing.Color.Black
        Me.lblMacauBanner.Location = New System.Drawing.Point(760, 7)
        Me.lblMacauBanner.Name = "lblMacauBanner"
        Me.lblMacauBanner.Size = New System.Drawing.Size(45, 13)
        Me.lblMacauBanner.TabIndex = 24
        Me.lblMacauBanner.Font = New Font(lblMacauBanner.Font, FontStyle.Bold)
        Me.lblMacauBanner.Text = "Macau"


        Me.lblMacauBanner.Text = "Macau"

        '
        'lblEPolicy
        '
        Me.lblEPolicy.AutoSize = True
        Me.lblEPolicy.Location = New System.Drawing.Point(845, 32)
        Me.lblEPolicy.Name = "lblEPolicy"
        Me.lblEPolicy.Size = New System.Drawing.Size(41, 13)
        Me.lblEPolicy.TabIndex = 22
        Me.lblEPolicy.Text = "ePolicy"
        '
        'txtEPolicy
        '
        Me.txtEPolicy.Location = New System.Drawing.Point(892, 29)
        Me.txtEPolicy.Name = "txtEPolicy"
        Me.txtEPolicy.ReadOnly = True
        Me.txtEPolicy.Size = New System.Drawing.Size(46, 20)
        Me.txtEPolicy.TabIndex = 23
        '
        'notificationworker
        '
        Me.notificationworker.WorkerReportsProgress = True
        '
        'tabTraditionalPartialSurrQuot
        '
        Me.tabTraditionalPartialSurrQuot.AutoScroll = True
        Me.tabTraditionalPartialSurrQuot.Controls.Add(Me.Ctrl_Sel_CO2)
        Me.tabTraditionalPartialSurrQuot.Controls.Add(Me.Ctrl_POS_ParSurTradition1)
        Me.tabTraditionalPartialSurrQuot.ImageIndex = -1
        Me.tabTraditionalPartialSurrQuot.Location = New System.Drawing.Point(4, 61)
        Me.tabTraditionalPartialSurrQuot.Name = "tabDefaultPayoutMethodRegist"
        Me.tabTraditionalPartialSurrQuot.Size = New System.Drawing.Size(192, 35)
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
        Me.Ctrl_Sel_CO2.Location = New System.Drawing.Point(0, 4)
        Me.Ctrl_Sel_CO2.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_Sel_CO2.Name = "Ctrl_Sel_CO2"
        Me.Ctrl_Sel_CO2.PolicyNoInuse = ""
        Me.Ctrl_Sel_CO2.PStsInuse = ""
        Me.Ctrl_Sel_CO2.RCDDateInUse = ""
        Me.Ctrl_Sel_CO2.RiderInuse = ""
        Me.Ctrl_Sel_CO2.RiskStsInuse = ""
        Me.Ctrl_Sel_CO2.Size = New System.Drawing.Size(1024, 102)
        Me.Ctrl_Sel_CO2.TabIndex = 1
        '
        'Ctrl_POS_ParSurTradition1
        '
        Me.Ctrl_POS_ParSurTradition1.Location = New System.Drawing.Point(0, 106)
        Me.Ctrl_POS_ParSurTradition1.Margin = New System.Windows.Forms.Padding(4)
        Me.Ctrl_POS_ParSurTradition1.Name = "Ctrl_POS_ParSurTradition1"
        Me.Ctrl_POS_ParSurTradition1.Size = New System.Drawing.Size(1024, 700)
        Me.Ctrl_POS_ParSurTradition1.TabIndex = 3
        '
        'frmPolicyMcu
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1284, 734)
        Me.Controls.Add(Me.txtEPolicy)
        Me.Controls.Add(Me.lblEPolicy)
        Me.Controls.Add(Me.lblCapsilPolNo)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.lblMacauBanner)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtEstatement)
        Me.Controls.Add(Me.lblDateFormatMsg)
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
        Me.Name = "frmPolicyMcu"
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
        Me.tabDCAR.ResumeLayout(False)
        Me.tabUnderwriting.ResumeLayout(False)
        Me.tabSMS.ResumeLayout(False)
        Me.tabPolicyAlt.ResumeLayout(False)
        Me.tabCouponHistory.ResumeLayout(False)
        Me.tabDDACCD.ResumeLayout(False)
        Me.tabCashFlow.ResumeLayout(False)
        Me.tabClaimsHistory.ResumeLayout(False)
        Me.tabTransactionHistory.ResumeLayout(False)
        Me.tabCustomerHistory.ResumeLayout(False)
        Me.tabServiceLog.ResumeLayout(False)
        Me.tabAgentInfo.ResumeLayout(False)
        Me.tabFundTrans.ResumeLayout(False)
        Me.tabPayOutHist.ResumeLayout(False)
        Me.tabPolicyNotes.ResumeLayout(False)
        Me.tabSubAcc.ResumeLayout(False)
        Me.tabParSur.ResumeLayout(False)
        Me.tabiLASSMS.ResumeLayout(False)
        Me.tabPostSalesCall.ResumeLayout(False)
        Me.tabPostSalesCall.PerformLayout()
        Me.tabTapNGo.ResumeLayout(False)
        Me.tabOePay.ResumeLayout(False)
        Me.tabAsurPolicySummary.ResumeLayout(False)
        Me.tabAsurServiceLog.ResumeLayout(False)
        Me.tabTraditionalPartialSurrQuot.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public isAssurance As Boolean = False ' Added by Hugo Chan on 2021-05-14, "CRS - First Level of Access", declare this flag for skipping some life asia and capsil function calls
    Public sCompanyID As String = "MCU"

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
        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyMcu", "frmPolicy_Load", "ReloadScreen.Begin", funcStartTime, Now, txtPolicy.Text, "", "")
        If Me.IsHandleCreated AndAlso Not Me.Disposing AndAlso Not Me.IsDisposed Then
            ' if blank form already loaded, reload data now, otherwise wait for the main thread to load itself
            LoadForm()

            ' re-visible Tab Controls
            Me.tcPolicy.Visible = True
        End If
        SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyMcu", "frmPolicy_Load", "ReloadScreen.End", funcStartTime, Now, txtPolicy.Text, "", "")
    End Sub

    Private Sub frmPolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim funcStartTime As Date = Now

        If IsAsyncLoadForm AndAlso Me.dsPolicyHead Is Nothing Then
            ' Only display a disabled blank screen until the policy data is successfully retrieved
            Control.CheckForIllegalCrossThreadCalls = False ' disable non-main thread call check for global controls (Enable cross-thread access to control)
            Me.tcPolicy.Visible = False
            SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicyMcu", "frmPolicy_Load", "frmPolicy_Load.Show", funcStartTime, Now, txtPolicy.Text, "", "")
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

        Dim strErr As String

        'check IUL access
        If Not String.IsNullOrEmpty(strPolicy) Then
            If CheckIULAccess(strPolicy, g_McuComp) Then Me.tcPolicy.Controls.Add(Me.tabIULControl)
        End If
        '

        'check the access right
        If Not CheckUPSAccessFunc("Search Policy (Macau)") Then
            MessageBox.Show("No Permission", "No Permission", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        LoadMsgAndLabel()

        If blnIsUHNWCustomer Then
            InsertVVIPLog(strPolicy, strCustID, strRelateCode, "Policy Summary (MCU)", isUHNWMember)
        End If


        ' Added by Hugo Chan on 2021-05-14, "CRS - First Level of Access", display Assurance policy
        'If isAssurance Then
        '    ShowAssuranceTabs()
        '    LoadAssuranceInformation()
        '    Exit Sub
        'End If

        Try
            Dim dtPolList As DataTable

            ' CRS performer slowness - Reuse BusinessDate to reduce the number of calls
            Dim dtBusDate As Date = Today   ' use today if fail

            'SL20191121 - Start
            If GetBusinessDate(dtBusDate) Then
                If GetMcuQDAPRefundExtraPaymentAlert(strPolicy, "", dtBusDate, strErr) Then
                    Dim strAlertMsg As String = String.Empty
                    strAlertMsg += "Refund 50% unearned premium as surrender benefit if policy is full surrender within the first policy year"
                    MessageBox.Show(strAlertMsg, "Refund Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
            'SL20191121 - End

            If CustomerBL.GetMcuPolicyHolderId(strPolicy, strCustID, strErr) Then
                If BadAddrWarning(strCustID) Then
                    Dim strWarnMsg As String = String.Empty
                    strWarnMsg += "Customer has bad address record. Pleae remind customer to:" & vbNewLine
                    strWarnMsg += "-Submit address proof of existing address; or" & vbNewLine
                    strWarnMsg += "-Update the new address information by submitting a completed address change form with signature that matches company record."
                    MessageBox.Show(strWarnMsg, "Bad Address Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            'Me.tcPolicy.Size = New System.Drawing.Size(764, 408)

            'Dim dlgQExec As QExecDelegate = New QExecDelegate(AddressOf w.QueuedExec)

            'Dim tab As TabPage
            'For Each tab In tcPolicy.TabPages
            '    'tabPaymentHistory.ImageIndex = 3
            '    'tabCouponHistory.ImageIndex = 3
            '    If tab.Text = "Policy Summary" Then
            '    Else
            '        tab.ImageIndex = 3
            '    End If
            'Next

            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.WaitCursor

            blnCanClose = False

            ' *** Get Policy Summary first ***
            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    objCS.Env = giEnv
            'End If
            'AC - Change to use configuration setting - end

            ' Initialize policy note

            ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
            'PolicyNote1.DataAdaptor.ComHeader = gobjDBHeader
            PolicyNote1.DataAdaptor.ComHeader = gobjMcuDBHeader

#If STRESS <> 0 Then
        Call WriteLog("Start: " & Now, writer)
        Dim ts, tf As TimeSpan
        ts = New TimeSpan(Now.Ticks)
#End If

            ' **** ES005 begin ****
            ' Popup alert message if there is pending fund switching request
            ''Dim dsSendData As New DataSet
            ''Dim dsRece As New DataSet
            ''Dim wsCRS As New LifeClientInterfaceComponent.clsCRS
            ''Dim dtSendData As New DataTable
            ''Dim dr As DataRow = dtSendData.NewRow()

            ''dtSendData.Columns.Add("PolicyNo")
            ''dr("PolicyNo") = strPolicy
            ''dtSendData.Rows.Add(dr)
            ''dsSendData.Tables.Add(dtSendData)

            ''Dim dtReceData As New DataTable
            ''dtReceData.Columns.Add("PendingFlag")
            ''dsRece.Tables.Add(dtReceData)

            ''wsCRS.DBHeader = objDBHeader
            ''wsCRS.MQQueuesHeader = objMQQueHeader

            ''If wsCRS.CheckPendingFSW(dsSendData, dsRece, strErr) = True Then
            ''    If dsRece.Tables(0).Rows(0).Item("PendingFlag") = "N" Then
            ''    Else
            ''        MsgBox("Please note that there is a pending fund trade in progress.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            ''    End If
            ''Else
            ''    wndMain.StatusBarPanel1.Text = strErr
            ''End If

            'If CheckFSPending(strPolicy, strErr) Then
            '    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            'End If

            '' Check Help database, and setup help accordingly
            'Dim dtHelpInfo As DataTable
            'If SetupHelp(dtHelpInfo, strErr) Then

            '    HelpProvider1.HelpNamespace = "C:\AcsME\CRS_Help\crs_help.chm"

            '    Dim ctlToSet As Control
            '    Dim arCtrl() As String
            '    For Each dr As DataRow In dtHelpInfo.Rows
            '        arCtrl = Strings.Split(dr.Item("cswsyv_value"), ".")
            '        ctlToSet = Controls(arCtrl(0))
            '        For i As Integer = 1 To arCtrl.Length - 1
            '            ctlToSet = ctlToSet.Controls(arCtrl(i))
            '        Next
            '        HelpProvider1.SetHelpNavigator(ctlToSet, HelpNavigator.KeywordIndex)
            '        HelpProvider1.SetHelpKeyword(ctlToSet, dr.Item("cswsyv_desc"))
            '    Next
            'Else
            '    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            'End If

            ' **** ES005 end ****
            'Dim blnCanView = False ' move to class variable
            Dim strUnit As String
            Dim strEstatement As String = ""
            Dim clsCommon As New LifeClientInterfaceComponent.CommonControl()

            clsCommon.MQHeader = Me.objMQQueHeader
            clsCommon.ComHeader = Me.objDBHeader

            If Not clsCommon.GetEStatementIndicatorByPolicyNo(strPolicy, strEstatement, strErr) Then
                Throw New Exception(strErr)
            End If

            txtEstatement.Text = strEstatement

            If isLifeAsia = False Then
                'dtPolList = objCS.GetPolicySummary(strPolicy, lngErr, strErr)
                dtPolSum = objCS.GetPolicySummary(strPolicy, lngErr, strErr)
                'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
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
                'Me.tabTapNGo.ImageIndex = -1
                'Me.tabOePay.ImageIndex = -1 'KT20180726
                Me.tabCoverageDetails.ImageIndex = -1
                Me.tabPaymentHistory.ImageIndex = -1
                'LH1507004  Journey Annuity Day 2 Phase 2 Start
                Me.tabAnnuityPaymentHistory.ImageIndex = -1
                Me.tabDirectCreditHistory.ImageIndex = -1
                Me.tabCupGetTransResult.ImageIndex = -1
                'LH1507004  Journey Annuity Day 2 Phase 2 End
                'Me.tabDCAR.ImageIndex = -1
                Me.tabDDACCD.ImageIndex = -1
                Me.tabCouponHistory.ImageIndex = -1
                Me.tabCustomerHistory.ImageIndex = -1

                Dim ds As New DataSet
                getLifeAsiaInfo(strPolicy, dtBusDate, ds)

                'ITSR933 FG R3 Policy Number Change Start
                Dim strErr1 As String = ""
                Dim strCapsilPolicy As String = GetCapsilPolicyNo(strPolicy)
                If strCapsilPolicy <> "" Then
                    strCapsilPolicy = strCapsilPolicy.Trim
                    lblCapsilPolNo.Text = strCapsilPolicy
                End If
                'ITSR933 FG R3 Policy Number Change End

                'MsgBox(strCustID)

                If ds.Tables.Count > 0 Then
                    dtPolSum = ds.Tables("POLINF").Copy
                    dtNA = ds.Tables("ORDUNA").Copy

                    ' Check authority to view the channel
                    If Not strSK Is Nothing AndAlso strSK.Length <> 0 Then
                        strUnit = GetMcuLocation(Strings.Right(dtPolSum.Rows(0).Item("POAGCY"), 5))
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
                        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
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
                        'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
                        'PolicySummary1.Visible = False
                        AddressSelect1.Visible = False
                        Ctrl_CRSPolicyGeneral_Information1.dbHeader = Me.objDBHeader
                        Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
                        Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = RTrim(strPolicy)
                        Ctrl_CRSPolicyGeneral_Information1.SystemInUse = "CRS"
                        Ctrl_CRSPolicyGeneral_Information1.UHNWRightFlag = isUHNWMemberMcu
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

                        ''PL20120427 - START
                        'With Me.UclParSur1
                        '    .DBHeader = Me.objDBHeader
                        '    .MQQueuesHeader = Me.objMQQueHeader
                        '    .PolicyNumber = RTrim(strPolicy)
                        '    .InitInfo()
                        'End With
                        ''PL20120427 - END
                    End If

                    ''Tap & Go tab KT20161111
                    'strFuncStartTime = Now
                    'If Me.Ctrl_CRSPolicyGeneral_Information1.dbHeader.CompanyID = "ING" Or Me.Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader.CompanyID = "ING" Then
                    '    If isTapNGoProduct(strPolicy) Then

                    '        If CheckUPSAccess("Policy Summary") Then
                    '            Me.tcPolicy.Controls.Add(Me.tabTapNGo)
                    '            Ctrl_TapNGo1.dbHeader = Me.objDBHeader
                    '            Ctrl_TapNGo1.MQQueuesHeader = Me.objMQQueHeader
                    '            Ctrl_TapNGo1.dbHeader = Me.objDBHeader
                    '            Ctrl_TapNGo1.PolicyNoInUse = RTrim(strPolicy)
                    '            Ctrl_TapNGo1.showTapNGoInfo()
                    '        End If
                    '    End If
                    'End If
                    'strFuncEndTime = Now
                    'SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "Ctrl_TapNGo", strFuncStartTime, strFuncEndTime, "", "", "")

                    ''KT20170726
                    'strFuncStartTime = Now
                    'If Me.Ctrl_CRSPolicyGeneral_Information1.dbHeader.CompanyID = "ING" Or Me.Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader.CompanyID = "ING" Then
                    '    If isOepayProduct(strPolicy) Then
                    '        If CheckUPSAccess("Policy Summary") Then
                    '            Me.tcPolicy.Controls.Add(Me.tabOePay)
                    '            Ctrl_OePay1.dbHeader = Me.objDBHeader
                    '            Ctrl_OePay1.MQQueuesHeader = Me.objMQQueHeader
                    '            Ctrl_OePay1.dbHeader = Me.objDBHeader
                    '            Ctrl_OePay1.PolicyNoInUse = RTrim(strPolicy)
                    '            Ctrl_OePay1.showOePayInfo()
                    '        End If
                    '    End If
                    'End If
                    'strFuncEndTime = Now
                    'SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "Ctrl_OePay", strFuncStartTime, strFuncEndTime, "", "", "")

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

                    'ITSR2408 - SL20210625 - Start
                    'strFuncStartTime = Now
                    'Call GetMediSavingAmount(txtPolicy.Text, True)
                    'strFuncEndTime = Now
                    'SysEventLog.WritePerLog(gsUser, "cs2005.frmPolicy", "frmPolicy_Load", "GetMediSavingAmount", strFuncStartTime, strFuncEndTime, "", "", "")
                    'ITSR2408 - SL20210625 - 
                    notificationworker.RunWorkerAsync()

                    ' VIP flag
                    strFuncStartTime = Now
                    If GetVIPStatus(0, strPolicy, g_McuComp) = "1" Then ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
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

            'If lngErr = 0 Then
            '    dtCashV = objCS.GetPolicyVal(strPolicy, Today, lngErr, strErr)
            'End If

            If lngErr = 0 Then
                blnPOINFO = True
            End If
            'dtPolMisc = dtNA.Copy
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
                            ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
                            ' Add g_McuComp Parameter.
                            Me.txtStatus.Text = GetAcStatus(strAcSts) 'Remove the parameter, because of the config table maybe is not ready,g_McuComp
                        End If

                        ' Flora Leung, Project Leo Goal 3 Capsil, 14-Feb-2012 Start
                        Dim strSubSts As String = String.Empty
                        strSubSts = .Item("SubStatus")
                        If Not IsDBNull(strAcSts) And Not IsDBNull(strSubSts) Then
                            If strAcSts.Trim & strSubSts.Trim = "2W" Then
                                ' Me.txtStatus.Text = "Payor Waive (MCI)"
                                ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
                                ' Add g_McuComp Parameter.
                                Me.txtStatus.Text = GetCodeTableValue("CRS_2W") 'Remove the parameter, because of the config table maybe is not ready,g_McuComp
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
                    If GetVIPStatus(0, strPolicy, g_McuComp) = "1" Then ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
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

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, gSystem)
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

        sqlConn.ConnectionString = strCIWConn
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
        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
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

        sqlConn.ConnectionString = strCIWConn
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
    ''' <summary>
    ''' Prompt policy alert for the policy
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Sub PolicyAlert()
        Dim strAlertTitle As String
        Dim strCSR As String

        'ITSR933 FG R3 CE Start
        Dim strPolicyCap As String = GetCapsilPolicyNo(strPolicy)
        'ITSR933 FG R3 CE End

        ' 7/17/2006 - increase timeout period
        ' End

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "FRM_POLICY_ALERT",
                                                                  New Dictionary(Of String, String)() From {
                      {"strPolicy", Trim(strPolicy)},
                      {"strPolicyCap", If(String.IsNullOrEmpty(strPolicyCap), "unknow999", strPolicyCap)}
                   })

            For Each dr As DataRow In retDs.Tables(0).Rows
                If IsDBNull(dr.Item("Name")) Then
                    strCSR = "unknown user (" & dr.Item("MasterCSRID") & ")"
                Else
                    strCSR = dr.Item("Name")
                End If
                strAlertTitle = "Alert from " & strCSR & " on " & Format(dr.Item("EventInitialDatetime"), "dd MMM yyyy") & ". "
                MsgBox(dr.Item("AlertNotes"), MsgBoxStyle.Information, strAlertTitle)
            Next
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        End Try

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
                            If GetVIPStatus(strCustID, "", g_McuComp) = "1" Then ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
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
                            If GetVIPStatus(strCustID, "", g_McuComp) = "1" Then ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
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
                    'oliver 2024-5-3 commented for Table_Relocate_Sprint13,It will not call if life asia policy
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
                    'Me.tabTapNGo.ImageIndex = -1

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
                    'Me.tabDCAR.ImageIndex = -1
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
            AgentInfo1.srcDTPolSum(dtNA.Copy) = dtPolSum.Copy
            blnAgtInfoLoad = True
            Exit Sub
        End If

        If tcPolicy.SelectedTab.Name = "tabAgentInfo" AndAlso (strCustID Is Nothing OrElse strCustID = "") AndAlso Not blnAgtInfoLoad Then
            tcPolicy.SelectedTab = tabPolicySummary
        End If

        ' Load UW information only after ORDUNA is available
        If tcPolicy.SelectedTab.Name = "tabUnderwriting" AndAlso (Not strCustID Is Nothing) AndAlso strCustID <> "" AndAlso Not blnUWLoad Then
            'UwInfo1.srcDTUWInf(VBDate(dtPolSum.Rows(0).Item("POFELK"))) = strPolicy
            'UwInfo1.srcDTUWInf(dtPolSum.Rows(0).Item("POFELK")) = strPolicy
            UwInfo1.srcDTUWInf(Nothing) = strPolicy 'Macau Underwriting Fix
            blnUWLoad = True
            Exit Sub
        End If

        If tcPolicy.SelectedTab.Name = "tabUnderwriting" AndAlso (strCustID Is Nothing OrElse strCustID = "") AndAlso Not blnUWLoad Then
            tcPolicy.SelectedTab = tabPolicySummary
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

        sqlconnect.ConnectionString = strCIWConn
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
            AgentInfo1.srcDTPolSum(dtNA.Copy) = dtPolSum.Copy
            blnAgtInfoLoad = True
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
            UclServiceLog1.PolicyAccountID(strCustID) = strPolicy
            blnSrvLogLoad = True
        End If
    End Sub

    Private Sub tabUnderwriting_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabUnderwriting.HandleCreated
        If Not isLifeAsia Then
            If strCustID <> "" Then
                'UwInfo1.srcDTUWInf(VBDate(dtPolSum.Rows(0).Item("POFELK"))) = strPolicy
                UwInfo1.srcDTUWInf(dtPolSum.Rows(0).Item("POFELK")) = strPolicy
                blnUWLoad = True
            End If
        Else
            UwInfo1.srcDTUWInf(Nothing) = strPolicy
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



    ''' <summary>
    ''' Get the Policy information
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    ''' <remarks>
    ''' <br>20241119 Chrysan Cheng, CRS performer slowness - Reuse BusinessDate to reduce the number of calls</br>
    ''' </remarks>
    Public Sub getLifeAsiaInfo(strPolicy As String, businessDate As Date, ByRef ds As DataSet)
        Dim policyDs As DataSet ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
        Dim strSQL As String = ""
        Dim dr1 As DataRow

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

        Dim dt2 As DataTable = New DataTable("ORDUNA")
        dt2.Columns.Add("ClientID", Type.GetType("System.String"))
        dt2.Columns.Add("NamePrefix", Type.GetType("System.String"))
        dt2.Columns.Add("NameSuffix", Type.GetType("System.String"))
        dt2.Columns.Add("FirstName", Type.GetType("System.String"))
        dt2.Columns.Add("ChiName", Type.GetType("System.String"))

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

        CRS_Component.WaitWndFun.ShowMessage("Read Policy from CRSAPI Server...")
        policyDs = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_POLICY_POLICY_INFO",
                                                            New Dictionary(Of String, String)() From {
                {"PolicyNo", Trim(strPolicy)}
            })
        ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
        ' Get Effective Date
        Dim strerr As String = ""
        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.DBHeader = Me.objDBHeader
        clsPOS.MQQueuesHeader = Me.objMQQueHeader

        ' CRS performer slowness - Reuse BusinessDate to reduce the number of calls
        Dim effDate As Date = businessDate
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
        'CRS_Component.WaitWndFun.ShowMessage("Get Bussines Date from 400 MQ...") 'Lubin 2022-11-07 Add Pending message.
        'If GetBusinessDate(effDate) = False Then 'If LAS Server doesn't return the business date, then use today as Effect Date, Lubin 2022-11-07 Add Comment.
        '    effDate = Today
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
        row("QuoteDate") = effDate.Date
        row("PolicyNo") = strPolicy.Trim
        row("PrintFlag1") = "N"
        dt.Rows.Add(row)
        dsLAPolicySend.Tables.Add(dt)

        strerr = ""

        clsPOS.DBHeader = Me.objDBHeader
        clsPOS.MQQueuesHeader = Me.objMQQueHeader
        CRS_Component.WaitWndFun.ShowMessage("Get PolicyValueEnq from 400 MQ...") 'Lubin 2022-11-07 Add Pending message.
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

        For Each sqlrDr As DataRow In policyDs.Tables("policy").Rows
            If sqlrDr("policyrelatecode").ToString.Trim = "SA" Then
                dr1("POAGCY") = sqlrDr("agentcode").ToString.Trim
            ElseIf sqlrDr("policyrelatecode").ToString.Trim = "WA" Then
                dr1("POWAGT") = sqlrDr("agentcode").ToString.Trim
            End If
            dr1("unit") = sqlrDr("unitcode").ToString.Trim
        Next

        dt1.Rows.Add(dr1)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'strSQL = "select distinct customerID as ClientID from csw_poli_rel where PolicyAccountID = '" & rtrim(strpolicy) & "' and policyrelatecode in ('SA', 'WA')"
        'strSQL = "select (select '00000' + agentcode from customer where  csw_poli_rel.customerID=customer.CustomerID) as ClientID, " & _
        '"(select NamePrefix from customer where  csw_poli_rel.customerID=customer.CustomerID) as NamePrefix, " & _
        '"(select FirstName from customer where  csw_poli_rel.customerID=customer.CustomerID) as FirstName, " & _
        '"(select NameSuffix from customer where  csw_poli_rel.customerID=customer.CustomerID) as NameSuffix, " & _
        '"(select chilstNm + ' ' + ChiFstNm from customer where  csw_poli_rel.customerID=customer.CustomerID) as ChiName, policyrelatecode " & _
        '"from csw_poli_rel " & _
        '"where PolicyAccountID = '" & rtrim(strpolicy) & "' and policyrelatecode in ('SA', 'WA', 'PH', 'PI', 'BE') "


        ' Not sure about these logic, in the query it find with two codes('SA', 'WA')
        ' So the servicing agent and writing agent must be one guy, otherwise following logic will insert two rows.
        ' I think it should not be right in some scenes. Just ignore it at this time.
        'agent
        CRS_Component.WaitWndFun.ShowMessage("Get Agent info from CRSAPI Server...") 'Lubin 2022-11-07 Add Pending message.

        For Each sqlrDr As DataRow In policyDs.Tables("policy_agent").Rows
            dr1 = dt2.NewRow()

            dr1("ClientID") = "00000" & sqlrDr("agentcode")

            dr1("NamePrefix") = sqlrDr("NamePrefix")
            dr1("NameSuffix") = sqlrDr("NameSuffix")
            dr1("FirstName") = sqlrDr("FirstName")
            dr1("ChiName") = Trim(sqlrDr("chilstnm")) & Trim(sqlrDr("chifstnm"))

            dt2.Rows.Add(dr1)
        Next

        'non agent
        For Each sqlrDr As DataRow In policyDs.Tables("policy_no_agent").Rows
            dr1 = dt2.NewRow()

            dr1("ClientID") = sqlrDr("customerid")

            dr1("NamePrefix") = sqlrDr("NamePrefix")
            dr1("NameSuffix") = sqlrDr("NameSuffix")
            dr1("FirstName") = sqlrDr("FirstName")
            dr1("ChiName") = Trim(sqlrDr("chilstnm")) & Trim(sqlrDr("chifstnm"))

            dt2.Rows.Add(dr1)
        Next

        'owner

        For Each sqlrDr As DataRow In policyDs.Tables("policy_ph").Rows

            strCustID = sqlrDr("customerid")

            txtCName.Text = sqlrDr("COName")
            txtCNameChi.Text = sqlrDr("COCName")
            txtTitle.Text = sqlrDr("NamePrefix")
            txtLastName.Text = sqlrDr("NameSuffix")
            txtFirstName.Text = sqlrDr("FirstName")
            txtChiName.Text = Trim(sqlrDr("chilstnm")) & Trim(sqlrDr("chifstnm"))
        Next

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

            'Added by Human Macau Phase 3 2022-12-28
            '#If DEBUG
            'Dim f As New POSCommCtrl.Ctrl_DirectCreditHistFilter()
            'f.PolicyNo = strPolicy
            'f.FromDate = DateAdd(DateInterval.Year, -3, Today)
            'f.ToDate = Today
            ''Ctrl_DirectCreditHist1.Search(f)

            'Dim ds As New DataSet            
            'Dim strErr1 As String = ""
            'Me.LifeClientPos.DBHeader = gobjDBHeader
            'Me.LifeClientPos.MQQueuesHeader = gobjMQQueHeader
            'Dim Polices As String() = System.Configuration.ConfigurationManager.AppSettings("PolicyTestCases").Split(",")
            'For index As Integer = 0 To Polices.Length - 1 
            '    Console.WriteLine(Polices(index))
            '    ds = New DataSet
            '    strErr1 = ""
            '    If Not Me.LifeClientPos.GetDirectCreditHistory(Polices(index), f.FromDate, f.ToDate, ds, strErr1) Then                
            '        Console.WriteLine(strErr1)
            '    Else
            '        If ds.Tables.Count > 0 Then
            '            Console.WriteLine( ds.Tables(0).Rows(0)("RowCount"))
            '        End If
            'End If
            'Next
            '#End If

            Ctrl_BillingInf1.DBHeader = Me.objDBHeader
            Ctrl_BillingInf1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_BillingInf1.PolicyNoInUse = RTrim(strPolicy)
            'End Added by Human Macau Phase 3 2022-12-28
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
            Ctrl_AnnuityPaymentHist1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_AnnuityPaymentHist1.DBHeader = Me.objDBHeader
            Ctrl_AnnuityPaymentHist1.PolicyNo = RTrim(strPolicy)
            'Ctrl_AnnuityPaymentHist1.InitScreen()

            Ctrl_AnnuityPaymentHist1.DBHeader = Me.objDBHeader
            Ctrl_AnnuityPaymentHist1.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_AnnuityPaymentHist1.PolicyNo = RTrim(strPolicy)
            'Added by Human Macau Phase 3 
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
    ''' <summary>
    ''' Get BillNo of Policy
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    ''' <returns>BillNo</returns>
    Private Function GetBillNo() As String
        Dim retBillNo = ""
        Dim retDs As New DataSet
        Try
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_POLICY_BILL_NO", New Dictionary(Of String, String)() From {
           {"PolicyNo", strPolicy}
           })
            If retDs.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(retDs.Tables(0).Rows(0)("BillNo")) Then
                retBillNo = retDs.Tables(0).Rows(0)("BillNo")
            End If
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        End Try
        Return retBillNo

    End Function
    ' **** ES005 end ****
    Private Sub tabFundTrans_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabFundTrans.HandleCreated

        'If isLifeAsia Then
        ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
        'Me.Ctrl_FundTranHist1.ComHeader = gobjDBHeader
        'Me.Ctrl_FundTranHist1.MQQueuesHeader = gobjMQQueHeader
        Me.Ctrl_FundTranHist1.ComHeader = gobjMcuDBHeader
        Me.Ctrl_FundTranHist1.MQQueuesHeader = gobjMcuMQQueHeader
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

    Private Sub ClaimHist1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    'ITDCBT20150115 NegativeCashValue Start 
    Public Sub FillNegativeCashValueReminder()

        Dim dsNegativeCashValueReminder As New Data.DataSet
        Dim objPOS As LifeClientInterfaceComponent.clsPOS
        objPOS = New LifeClientInterfaceComponent.clsPOS()
        objPOS.DBHeader = objDBHeader

        ' oliver umcommented 2023-12-15 for Switch Over Code from Assurance to Bermuda 
        If Not objPOS.GetCapsilNegativeCashValueReminder(strPolicy, dsNegativeCashValueReminder, strErr) Then
            MsgBox(strErr)
            Exit Sub
        End If

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
        Dim POSDB As String = gcMcuPOS
        Try
            Dim ds As DataSet = New DataSet("ePolicy")
            Dim sqlconnect As New SqlConnection
            Dim sqlda As SqlDataAdapter

            sqlconnect.ConnectionString = strCIWConn
            Dim command = New SqlCommand("select ePolicyIndicator, PolicyId from " & POSDB & "PolicyEstatement nolock where PolicyId = @sPolicy1", sqlconnect)
            command.Parameters.AddWithValue("@sPolicy1", sPolicy)
            sqlda = New SqlDataAdapter(command)
            Try
                ds.Tables.Remove("ePolicy")
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
        'tcPolicy.TabPages.Clear()
        'tcPolicy.TabPages.Add(tabAsurPolicySummary)
        'tcPolicy.TabPages.Add(tabAsurServiceLog)
    End Sub

    ''' <summary>
    ''' Load policy information and policy's roles
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-14
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub LoadAssuranceInformation()
        'UclPolicySummary_Asur1.PolicyNumber = Me.PolicyAccountID
        'UclPolicySummary_Asur1.ProductName = Me.ProductName
        'UclPolicySummary_Asur1.LoadInformation()

        'modify by jeff
        'UclServiceLog2.resetDS()
        'UclServiceLog2.PolicyAccountID(UclServiceLog2.CustomerID) = Me.PolicyAccountID
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
    Private Sub LoadMsgAndLabel()
        Dim dsMsgApiLst As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), "MSG_API_LST", New Dictionary(Of String, String)() From {})
        Dim strMsg As String = ""
        Dim strLbl As String = ""
        Dim dsTmp As DataSet
        Dim iCount = 1
        If dsMsgApiLst.Tables.Count > 0 Then
            For Each dr As DataRow In dsMsgApiLst.Tables(0).Rows
                dsTmp = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), dr("MSG_API_LST").ToString(), New Dictionary(Of String, String)() From {
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
End Class



