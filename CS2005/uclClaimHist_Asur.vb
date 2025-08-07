'******************************************************************
' Amendment   : MCS Phease 2
' Date        : 7/20/2011
' Author      : Eric Shu (ES010)	
'******************************************************************
' Admended By: Keith Tong KT20201110
' Admended Function: Remaining limit for VHIS High End Medical Plan
' Date: 2020-11-10
' Project: ITSR-1488 CRS Minor Claims History
'********************************************************************
' Admended By: Keith Tong KT20201203
' Admended Function: Remaining limit for VHIS High End Medical Plan
' Date: 2020-12-03
' Project: ITSR-1488 CRS Minor Claims History
'********************************************************************
' Admended By: Sam Leung SL20210625
' Admended Function: Add fields for MediSaver Claim Paid Termination
' Date: 2021-06-25
' Project: ITSR-2408  MediSaver Claim Paid Termination
'********************************************************************
' Admended By: Phoebe Ching PC20211020
' Admended Function: Add VAT/GST display
' Date: 2021-10-20
' Project: ITSR-3067 Display VAT & GST in VHIS claims settlement
'********************************************************************
' Admended By: Dennis Lai DL20220112
' Admended Function: Cater HVS6 for display annual limit 
' Date: 2022-01-12
' Project: ITSR2959 vTheOne
'********************************************************************
' Admended By: Claudia Lai CL20220928
' Admended Function: Add fields for CI Claim Paid Termination
' Date: 2022-09-28
' Project: ITSR-3488 EasyCover/MyCover Claim Paid Termination
'********************************************************************
'********************************************************************
' Admended By: oliver ou 222834
' Admended Function: Phase 3 Point A-8(CRS Enhancement)
' Minor claims history Pending, Benefit boxes move to upper side, Enlarge the boxes And lengthen the fields For easy viewing
' Date: 2023-08-30
' Project: CRS
'********************************************************************
' Amend By:     Chrysan Cheng
' Date:         17 Feb 2025
' Changes:      CRS performance 2 - PaymentHistory and MinorClaimsHistory
'********************************************************************


Public Class ClaimHist_Asur
    Inherits System.Windows.Forms.UserControl

    Private dsClaim As DataSet
    Private m_PolicyNumber As String
    Private m_PolicyNumber2 As String       ' ES005
    Private lastClaimHistRowIndex As Integer = -1   ' the last selected ClaimHist row index

    ''' <summary>
    ''' DBLogon Properties
    ''' </summary>
    Public Property DBHeader As Utility.Utility.ComHeader

    ''' <summary>
    ''' MQ Properties
    ''' </summary>
    Public Property MQQueuesHeader As Utility.Utility.MQHeader

    ''' <summary>
    ''' Company ID Code
    ''' </summary>
    Public ReadOnly Property CompanyID As String
        Get
            Return DBHeader.CompanyID.Trim.ToUpper
        End Get
    End Property

    Public Property PolicyAccountID As String
        Get
            Return m_PolicyNumber
        End Get
        Set(ByVal Value As String)
            m_PolicyNumber = Value
        End Set
    End Property

#Region " Class Data and Types Declaration"

    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSurgical As System.Windows.Forms.TextBox
    Friend WithEvents txtImpairment1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtImpairment2 As System.Windows.Forms.TextBox
    Friend WithEvents txtImpairment3 As System.Windows.Forms.TextBox
    Friend WithEvents txtImpairment4 As System.Windows.Forms.TextBox
    Friend WithEvents chkDoc1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoc2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoc3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoc4 As System.Windows.Forms.CheckBox
    Friend WithEvents dgTtlClaim As System.Windows.Forms.DataGrid
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPayDet As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLifeLimit As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnualLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLifeLimit As System.Windows.Forms.Label
    Friend WithEvents lblAnnualLimit As System.Windows.Forms.Label
    Friend WithEvents lblAnnDeductible As System.Windows.Forms.Label
    Friend WithEvents txtAnnDeductible As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chk7ElevenPay As System.Windows.Forms.CheckBox
    Friend WithEvents txt7ElevenMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents lbl7ElevenMobileNo As System.Windows.Forms.Label
    Friend WithEvents chkfps As System.Windows.Forms.CheckBox
    Friend WithEvents btnfpsdetail As System.Windows.Forms.Button
    Friend WithEvents gpbclaimsnum As System.Windows.Forms.GroupBox
    Friend WithEvents txtclaimsoccur As System.Windows.Forms.TextBox
    Friend WithEvents txtclaimsnum As System.Windows.Forms.TextBox
    Friend WithEvents lblclaimsoccur As System.Windows.Forms.Label
    Friend WithEvents lblclaimsnum As System.Windows.Forms.Label
    Friend WithEvents txtpolicyno As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnDeductibleBal As System.Windows.Forms.TextBox
    Friend WithEvents lblAnnDeductibleBal As System.Windows.Forms.Label
    Friend WithEvents txtLifeRemainLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLifeRemainLimit As System.Windows.Forms.Label
    Friend WithEvents txtLifeUsedLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLifeUsedLimit As System.Windows.Forms.Label
    Friend WithEvents txtAnnualRemainLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblAnnualRemainLimit As System.Windows.Forms.Label
    Friend WithEvents lblVATGST As System.Windows.Forms.Label       'PC20211020
    Friend WithEvents btnIwsClaimHist As Button
    Friend WithEvents lblRcsRegNo As Label
    Friend WithEvents txtRcsRegNo As TextBox
    Friend WithEvents txtVATGST As System.Windows.Forms.TextBox     'PC20211020

#End Region

#Region " User Code "

    ''' <summary>
    ''' Async load data and show UI
    ''' </summary>
    Public Sub ShowUIAsync()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Dim strErr As String = String.Empty

        ' ES005, get mapping policy first (if any)
        GetPolicyMap(m_PolicyNumber, m_PolicyNumber2, strErr, CompanyID)
        SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "ShowUIAsync", "GetPolicyMap", mainStartTime, Now, m_PolicyNumber,
            (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))

        mainStartTime = Now
        If Not String.IsNullOrEmpty(m_PolicyNumber) Then
            ' disable controls before async loading is complete
            Me.Enabled = False

            ' async get data and show UI when complete
            GetDataAsync(AddressOf BuildUI)
        End If
        SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "ShowUIAsync", "GetDataAsync", mainStartTime, Now, m_PolicyNumber,
            (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
    End Sub

    ''' <summary>
    ''' Async get data and run a <paramref name="callback"/> when complete
    ''' </summary>
    ''' <param name="callback">An action to run in UI thread when get all data completes</param>
    Public Sub GetDataAsync(callback As Action)
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        If String.IsNullOrEmpty(m_PolicyNumber) Then m_PolicyNumber = " "
        If String.IsNullOrEmpty(m_PolicyNumber2) Then m_PolicyNumber2 = " "

        ' async get minor claims related data
        Dim claimsBL As New ClaimsBL(DBHeader, MQQueuesHeader)

        SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "GetDataAsync", "GetAllMinorClaimsDataAsync.Start", mainStartTime, Now, m_PolicyNumber,
            (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))

        claimsBL.GetAllMinorClaimsDataAsync(m_PolicyNumber, m_PolicyNumber2,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "GetDataAsync", "GetAllMinorClaimsDataAsync.End", mainStartTime, Now, m_PolicyNumber,
                    (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))

                ' no need further action if control non-Ready/closed
                If Me.Disposing OrElse Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

                ' the context here must be a worker thread, must invoke in main thread
                Me.Invoke(
                    Sub()
                        If Not t.IsFaulted Then
                            ' success, store data result
                            dsClaim = t.Result
                            ' Important!!! To reset the DataState manually so that the changed can be retrieved later
                            dsClaim.AcceptChanges()
                        Else
                            ' fail, pop up error msg
                            MsgBox($"GetDataAsync: {t.Exception.InnerException.Message}", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        End If

                        ' run callback anyway (if any)
                        callback?.Invoke()
                    End Sub
                )
            End Sub
        )
    End Sub

    ' This function will connect to the database, retrieve the claims history of the policy set in the PolicyNumber property
    Public Sub BuildUI()
        wndMain.Cursor = Cursors.WaitCursor
        'initialize the user interface, set the styles of the datagrids
        InitUI()

        ' set availability according to company
        If CompanyID = g_LacComp OrElse CompanyID = g_LahComp Then
            GroupBox1.Visible = False
            btnIwsClaimHist.Enabled = True
            lblRcsRegNo.Visible = False
            txtRcsRegNo.Visible = False
        ElseIf CompanyID = g_McuComp Then
            btnIwsClaimHist.Visible = False
            lblRcsRegNo.Visible = False
            txtRcsRegNo.Visible = False
        End If

        ' re-enable controls
        Me.Enabled = True

        ' refresh UI
        RefreshData()
        wndMain.Cursor = Cursors.Default
    End Sub

    ' This function initialize the user interface, set the styles of the datagrids
    Private Sub InitUI()

        ' Create layout of datagrid for Claim History
        Dim tsClaimHist As New clsDataGridTableStyle
        Dim cs As DataGridColumnStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcschd_claim_no"
        cs.HeaderText = "Claim No."
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "mcschd_claim_occur"
        cs.HeaderText = "Claim Occur"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "mcschd_insured_name"
        cs.HeaderText = "Insured Name"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcschd_accident_date"
        cs.HeaderText = "Accident Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcschd_hospital_indate"
        cs.HeaderText = "Hospital In Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcschd_hospital_outdate"
        cs.HeaderText = "Hospital Out Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        ' Add New fields 7/3/2006
        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mcschd_account_no"
        cs.HeaderText = "Cr. A/C No."
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mcschd_payee_name"
        cs.HeaderText = "Cr. Payee"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)
        ' End Add New

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "mcschd_chq_remark1"
        cs.HeaderText = "Remark1"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "mcschd_chq_remark2"
        cs.HeaderText = "Remark2"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        If CompanyID <> g_McuComp AndAlso CompanyID <> g_LacComp AndAlso CompanyID <> g_LahComp Then
            cs = New DataGridTextBoxColumn
            cs.Width = 200
            cs.MappingName = "rcsreg_case_no"
            cs.HeaderText = "Claim Case No."
            cs.NullText = gNULLText
            tsClaimHist.GridColumnStyles.Add(cs)
        End If

        'Map the table style to the grid
        tsClaimHist.MappingName = "mcsvw_claim_header_details"
        dgClaimHist.TableStyles.Add(tsClaimHist)

        'Create layout of datagrid for Cheque Info
        Dim tsChequeInfo As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "lcpcpm_cheque_curr"
        cs.HeaderText = "Pay Currency"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "lcpcpm_amount"
        cs.HeaderText = "Pay Amount"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "lcpchq_print_mode"
        cs.HeaderText = "Cheque Mode"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "lcpchq_cheque_no"
        cs.HeaderText = "Cheque No."
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "lcpchq_chq_status"
        cs.HeaderText = "Cheque Status"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "lcpchq_issue_date"
        cs.HeaderText = "Issue Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 170
        cs.MappingName = "lcpcpm_payee_name"
        cs.HeaderText = "Payee Name"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        'Map the table style to the grid
        tsChequeInfo.MappingName = "mcsvw_cheque_info"
        dgChequeInfo.TableStyles.Add(tsChequeInfo)

        'Create layout of datagrid for Benefit Info
        Dim tsBenefit As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 55
        cs.MappingName = "mcscbh_coverage"
        cs.HeaderText = "Coverage"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 280
        cs.MappingName = "Description"
        cs.HeaderText = "Plan"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mcsben_benefit_desc"
        cs.HeaderText = "Benefit"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        'If CompanyID = g_McuComp Then
        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "mcsben_benefit_maxamt"
        cs.HeaderText = "Max Benefit"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsBenefit.GridColumnStyles.Add(cs)
        'End If

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "mcscps_present_day"
        cs.HeaderText = "No of Day/Class"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "mcscps_present_curr"
        cs.HeaderText = "Presented Currency"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 125
        cs.MappingName = "mcs_present"
        cs.HeaderText = "Presented Amount"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcschd_payment_curr"
        cs.HeaderText = "Pay Currency"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "mcscbh_paid_day"
        cs.HeaderText = "Pay Date"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mcs_pay"
        cs.HeaderText = "Pay Amount"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsBenefit.GridColumnStyles.Add(cs)

        If CompanyID = g_McuComp Then
            cs = New DataGridTextBoxColumn
            cs.Width = 70
            cs.MappingName = "mcscbh_booster_paid"
            cs.HeaderText = "Booster Pay"
            cs.NullText = gNULLText
            tsBenefit.GridColumnStyles.Add(cs)
        End If

        'Map the table style to the grid
        tsBenefit.MappingName = "mcsvw_claim_bene_hist"
        dgBenefit.TableStyles.Add(tsBenefit)

        'Create layout of datagrid for Pending Claims Info
        Dim tsPending As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcspen_pending_date"
        cs.HeaderText = "Pending Date"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 320
        cs.MappingName = "mcspen_pending_desc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mcspen_resolved_date"
        cs.HeaderText = "Resolve Date"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "mcspen_resolved_desc"
        cs.HeaderText = "Resolve Description"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "mcspen_pending_status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        If CompanyID <> g_McuComp AndAlso CompanyID <> g_LacComp AndAlso CompanyID <> g_LahComp Then
            cs = New DataGridTextBoxColumn
            cs.Width = 130
            cs.MappingName = "mcspen_doc_submit"
            cs.HeaderText = "Doc Submit"
            cs.NullText = gNULLText
            tsPending.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 130
            cs.MappingName = "mcspen_doc_submit_date"
            cs.HeaderText = "Doc Submit Date"
            cs.NullText = gNULLText
            tsPending.GridColumnStyles.Add(cs)
        End If

        'Map the table style to the grid
        tsPending.MappingName = "mcsvw_claim_pending"
        dgPending.TableStyles.Add(tsPending)

        ' **** ES010 begin ****
        Dim tsTotalClaim As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "BeneType"
        cs.HeaderText = "Benefit"
        cs.NullText = gNULLText
        tsTotalClaim.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "CustomerID"
        cs.HeaderText = "CustomerID"
        cs.NullText = gNULLText
        tsTotalClaim.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "PolicyCurrency"
        cs.HeaderText = "Currency"
        cs.NullText = gNULLText
        tsTotalClaim.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "sum_mcscbh_paid_amt"
        cs.HeaderText = "Total Amount"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsTotalClaim.GridColumnStyles.Add(cs)

        'ITSR2408 - SL20210625 - Start
        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "posctd_saving_bal"
        cs.HeaderText = "Saving Account Balance"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsTotalClaim.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "possr_Terminal_Bonus"
        cs.HeaderText = "Terminal Bonus"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsTotalClaim.GridColumnStyles.Add(cs)
        'ITSR2408 - SL20210625 - End

        'ITSR3488 - CL20220928 - start
        If CompanyID <> g_McuComp Then
            cs = New DataGridTextBoxColumn
            cs.Width = 180
            cs.MappingName = "mcsben_benefit_maxamt"
            cs.HeaderText = "Max Benefit Amount"
            cs.NullText = gNULLText
            CType(cs, DataGridTextBoxColumn).Format = gNumFormat
            tsTotalClaim.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 180
            cs.MappingName = "mcs_pay"
            cs.HeaderText = "Total Claim Amount"
            cs.NullText = gNULLText
            CType(cs, DataGridTextBoxColumn).Format = gNumFormat
            tsTotalClaim.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 200
            cs.MappingName = "TotalRemain"
            cs.HeaderText = "Total Remaining Amount"
            cs.NullText = gNULLText
            CType(cs, DataGridTextBoxColumn).Format = gNumFormat
            tsTotalClaim.GridColumnStyles.Add(cs)
        End If
        'ITSR3488 - CL20220928 - end

        'Map the table style to the grid
        tsTotalClaim.MappingName = "Total_Claim"
        dgTtlClaim.TableStyles.Add(tsTotalClaim)

    End Sub

    ' This function fill up the dataset and display the data
    Private Sub RefreshData()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            If dsClaim Is Nothing OrElse dsClaim.Tables.Count = 0 Then Return

            ' show TheOneClaimBalance related data (if any)
            If dsClaim.Tables("TheOneClaimBalance").Rows.Count > 0 Then

                Me.GroupBox1.Text = HandleRemainHeader(dsClaim.Tables("TheOneClaimBalance").Rows(0).Item("ProductID").ToString())

                Me.txtAnnDeductible.Text = "0"
                Me.txtAnnDeductibleBal.Text = "0"
                Me.txtAnnualLimit.Text = "0"
                Me.txtAnnualRemainLimit.Text = "0"
                Me.txtLifeLimit.Text = "0"
                Me.txtLifeUsedLimit.Text = "0"
                Me.txtLifeRemainLimit.Text = "0"

                'KT20201203 - Start
                dsClaim.Tables("TheOneClaimBalance").DefaultView.RowFilter = "mcsben_benefit_code LIKE 'TOD%'"
                If dsClaim.Tables("TheOneClaimBalance").DefaultView.Count > 0 Then

                    If Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")) _
                        And Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) Then

                        Me.txtAnnDeductible.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n")

                        dsClaim.Tables("TheOneClaimBalance").DefaultView.RowFilter = "mcsben_benefit_code LIKE 'TOD%' AND " & "mcscrb_start_date <= '" & Today & "' AND mcscrb_end_date >='" & Today & "'"
                        If dsClaim.Tables("TheOneClaimBalance").DefaultView.Count > 0 Then
                            Me.txtAnnDeductibleBal.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")), "n")
                        Else
                            Me.txtAnnDeductibleBal.Text = Me.txtAnnDeductible.Text
                        End If

                    ElseIf Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) Then
                        'Me.txtAnnDeductible.Text = dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")
                        Me.txtAnnDeductible.Text = IIf(IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), 0, Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n"))
                        'Me.txtAnnDeductibleBal.Text = dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")
                        Me.txtAnnDeductibleBal.Text = IIf(IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), 0, Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n"))
                    End If
                End If

                dsClaim.Tables("TheOneClaimBalance").DefaultView.RowFilter = "mcsben_benefit_code LIKE 'AL%'"
                If dsClaim.Tables("TheOneClaimBalance").DefaultView.Count > 0 Then

                    If Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) _
                        And Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")) Then
                        Me.txtAnnualLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n")

                        dsClaim.Tables("TheOneClaimBalance").DefaultView.RowFilter = "mcsben_benefit_code LIKE 'AL%' AND " & "mcscrb_start_date <= '" & Today & "' AND mcscrb_end_date >='" & Today & "'"
                        If dsClaim.Tables("TheOneClaimBalance").DefaultView.Count > 0 Then
                            Me.txtAnnualRemainLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")), "n")
                        Else
                            Me.txtAnnualRemainLimit.Text = Me.txtAnnualLimit.Text
                        End If

                    ElseIf Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) Then
                        Me.txtAnnualLimit.Text = IIf(IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), 0, Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n"))
                        Me.txtAnnualRemainLimit.Text = IIf(IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), 0, Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n"))
                    End If
                End If
                'KT20201203 - End

                dsClaim.Tables("TheOneClaimBalance").DefaultView.RowFilter = "mcsben_benefit_code LIKE 'LTL%'"
                If dsClaim.Tables("TheOneClaimBalance").DefaultView.Count > 0 Then

                    If Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) _
                        And Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")) Then
                        Me.txtLifeUsedLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt") _
                            - dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")), "n")

                        Me.txtLifeLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n")
                        Me.txtLifeRemainLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcscrb_remain_amt")), "n")

                    ElseIf Not IsDBNull(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")) Then
                        Me.txtLifeLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n")
                        Me.txtLifeUsedLimit.Text = Format(0, "n")
                        Me.txtLifeRemainLimit.Text = Format(CDec(dsClaim.Tables("TheOneClaimBalance").DefaultView(0).Item("mcsben_benefit_maxamt")), "n")
                    End If
                End If

            End If
            'KT20201110 - End


            ' remove existing relations first (if any)
            dsClaim.Relations.Clear()

            ' then add new relations to the tables in dataset
            Dim dcParent(2) As DataColumn
            Dim dcChild(2) As DataColumn

            ' mcsvw_claim_header_details - mcsvw_cheque_info
            dcParent(0) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcsply_policy_no")
            dcParent(1) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_no")
            dcParent(2) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_occur")
            dcChild(0) = dsClaim.Tables("mcsvw_cheque_info").Columns("mcspay_policy_no")
            dcChild(1) = dsClaim.Tables("mcsvw_cheque_info").Columns("mcspay_claim_no")
            dcChild(2) = dsClaim.Tables("mcsvw_cheque_info").Columns("mcspay_claim_occur")
            dsClaim.Relations.Add("Master_Cheque", dcParent, dcChild, False)

            ' mcsvw_claim_header_details - mcsvw_claim_bene_hist
            ReDim dcParent(2)
            ReDim dcChild(2)
            dcParent(0) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcsply_policy_no")
            dcParent(1) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_no")
            dcParent(2) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_occur")
            dcChild(0) = dsClaim.Tables("mcsvw_claim_bene_hist").Columns("mcscbh_policy_no")
            dcChild(1) = dsClaim.Tables("mcsvw_claim_bene_hist").Columns("mcscbh_claim_no")
            dcChild(2) = dsClaim.Tables("mcsvw_claim_bene_hist").Columns("mcscbh_claim_occur")
            dsClaim.Relations.Add("Master_Benefit", dcParent, dcChild, False)

            ' mcsvw_claim_header_details - mcsvw_claim_pending
            ReDim dcParent(2)
            ReDim dcChild(2)
            dcParent(0) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcsply_policy_no")
            dcParent(1) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_no")
            dcParent(2) = dsClaim.Tables("mcsvw_claim_header_details").Columns("mcschd_claim_occur")
            dcChild(0) = dsClaim.Tables("mcsvw_claim_pending").Columns("mcsply_policy_no")
            dcChild(1) = dsClaim.Tables("mcsvw_claim_pending").Columns("mcspen_claim_no")
            dcChild(2) = dsClaim.Tables("mcsvw_claim_pending").Columns("mcspen_claim_occur")
            dsClaim.Relations.Add("Master_Pending", dcParent, dcChild, False)

            ' bind control to show data
            If dsClaim.Tables("mcsvw_claim_header_details").Rows.Count > 0 Then BindControl()

        Catch ex As Exception
            MsgBox("RefreshData(): " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            appSettings.Logger.logger.Error(ex.Message)
        Finally
            SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "RefreshData", "", mainStartTime, Now, m_PolicyNumber,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    ' This function data bound all contorls on the form
    Private Sub BindControl()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Try
            Dim b As Binding

            ClearAllBinding()

            dgClaimHist.SetDataBinding(dsClaim, "mcsvw_claim_header_details")
            dgChequeInfo.SetDataBinding(dsClaim, "mcsvw_claim_header_details.Master_Cheque")
            dgBenefit.SetDataBinding(dsClaim, "mcsvw_claim_header_details.Master_Benefit")
            dgPending.SetDataBinding(dsClaim, "mcsvw_claim_header_details.Master_Pending")

            'Create data bindings of other controls
            txtInsured.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_insured_name")
            Call UpdateSts()

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_accident_date")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtAccidentDate.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_hospital_indate")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtHospitalIn.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_hospital_outdate")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtHospitalOut.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_receive_date")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtReceiveDate.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_consult_date")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtConsultDate.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_settled_date")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtSettleDate.DataBindings.Add(b)

            b = New Binding("Text", dsClaim, "mcsvw_claim_header_details.mcschd_symptom_date")
            AddHandler b.Format, AddressOf DateTimeToDateString
            AddHandler b.Parse, AddressOf DateStringToDateTime
            txtSymptomDate.DataBindings.Add(b)

            txtComment.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_comment")
            txtRemark.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.remark")

            ' Add New fields on 7/3/2006
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            'If ExternalUser Then
            '    MaskDTsource(dsClaim.Tables("mcsvw_claim_header_details"), "mcschd_account_no", MaskData.BANK_ACCOUNT_NO)
            'End If
            txtCrAcNo.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_account_no")
            txtCrAcNm.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_payee_name")
            txtAccDesc.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_accident_desc")
            txtIllDesc.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_hospital_desc")
            ' End Add New

            ' **** ES010 begin ****
            txtImpairment1.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.impairment1")
            txtImpairment2.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.impairment2")
            txtImpairment3.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.impairment3")
            txtImpairment4.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.impairment4")

            'ITDCIC 7-11 & ISC claim payout
            txt7ElevenMobileNo.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcsmpy_mobile_num")
            chk7ElevenPay.DataBindings.Add("Checked", dsClaim, "mcsvw_claim_header_details.mcsmpy_7ElevenPay", False, DataSourceUpdateMode.OnPropertyChanged)

            dgTtlClaim.SetDataBinding(dsClaim, "Total_Claim")   'ES010
            'dgTtlPAClaim.SetDataBinding(dsClaim, "Total_PA_Claim")   'ES010

            txtpolicyno.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcsply_policy_no")

            'ITDCPI FPS Payment
            txtclaimsnum.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_claim_no")
            txtclaimsoccur.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcschd_claim_occur")
            If CompanyID <> g_McuComp AndAlso CompanyID <> g_LacComp AndAlso CompanyID <> g_LahComp Then
                txtRcsRegNo.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.rcsreg_case_no")
            End If

            ' **** ES010 end ****
            'ITDCPI FPS Payout

            'PC20211020
            txtVATGST.DataBindings.Add("Text", dsClaim, "mcsvw_claim_header_details.mcscps_present_amt")
            'PC20211020

            ' select the first row by default
            dgClaimHist.CurrentCell = New DataGridCell(0, 1)

        Finally
            SysEventLog.WritePerLog(gsUser, "CS2005.ClaimHist_Asur", "RefreshData", "BindControl", mainStartTime, Now, m_PolicyNumber,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    ' This function clear the data binding of all controls on the form
    Private Sub ClearAllBinding()
        dgClaimHist.DataBindings.Clear()
        dgChequeInfo.DataBindings.Clear()
        dgBenefit.DataBindings.Clear()
        dgPending.DataBindings.Clear()
        txtInsured.DataBindings.Clear()
        txtClaimStatus.DataBindings.Clear()
        txtAccidentDate.DataBindings.Clear()
        txtHospitalIn.DataBindings.Clear()
        txtHospitalOut.DataBindings.Clear()
        txtReceiveDate.DataBindings.Clear()
        txtConsultDate.DataBindings.Clear()
        txtSettleDate.DataBindings.Clear()
        txtSymptomDate.DataBindings.Clear()
        txtComment.DataBindings.Clear()
        txtRemark.DataBindings.Clear()
        ' Add New fields on 7/3/2006
        txtCrAcNo.DataBindings.Clear()
        txtCrAcNm.DataBindings.Clear()
        ' End Add New

        ' **** ES010 begin ****
        txtImpairment1.DataBindings.Clear()
        txtImpairment2.DataBindings.Clear()
        txtImpairment3.DataBindings.Clear()
        txtImpairment4.DataBindings.Clear()

        dgTtlClaim.DataBindings.Clear()
        'dgTtlPAClaim.DataBindings.Clear()
        ' **** ES010 end ****

        'ITDCIC 7-11 & ISC claim payout
        chk7ElevenPay.DataBindings.Clear()
        txt7ElevenMobileNo.DataBindings.Clear()

        'ITDCPI FPS Payout
        txtclaimsnum.DataBindings.Clear()
        txtclaimsoccur.DataBindings.Clear()

        ''PC20211020 Start
        txtVATGST.DataBindings.Clear()
        ''PC20211020 End

    End Sub

    ' This function update the comment field to the database
    Private Sub UpdateComment()
        'As the datagrid is bound to a view on the database, we need to do the update by executing a UPDATE SQL STATEMENT
        'directly in the mcs database
        Me.BindingContext(dsClaim, "mcsvw_claim_header_details").EndCurrentEdit()
        Dim tempdt As DataTable = dsClaim.Tables("mcsvw_claim_header_details").GetChanges(DataRowState.Modified)
        If tempdt IsNot Nothing Then
            If tempdt.Rows.Count > 0 Then
                For Each dr As DataRow In tempdt.Rows
                    Try
                        APIServiceBL.ExecAPIBusi(getCompanyCode(CompanyID), "MinorClaimMaster_Update", New Dictionary(Of String, String) From {
                            {"mcschd_comment", dr("mcschd_comment").ToString},
                            {"mcschd_claim_no", dr("mcschd_claim_no").ToString},
                            {"mcschd_claim_occur", dr("mcschd_claim_occur").ToString}
                        })

                        MsgBox("Record updated !", , "Record updated")
                    Catch ex As Exception
                        MsgBox("Error occurs when updating the comments. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    End Try
                Next

                dsClaim.Tables("mcsvw_claim_header_details").AcceptChanges()
            End If

            tempdt.Dispose()
        End If
    End Sub

    ' This function convert a datetime value into a formated string, it is used as handler of the FORMAT function of a bound control
    Private Sub DateTimeToDateString(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        ' The method converts only to string type. Test this using the DesiredType.
        If Not cevent.DesiredType Is GetType(String) Then
            Exit Sub
        End If

        ' Use the ToString method to format the value as short date ("d").
        Try
            cevent.Value = CType(cevent.Value, DateTime).ToString(gDateFormat)
        Catch ex As Exception
            cevent.Value = ""
        End Try

    End Sub

    ' This function convert a string into datetime value, it is used as handler of the PARSE function of a bound control
    Private Sub DateStringToDateTime(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        ' The method converts back to decimal type only.
        If Not cevent.DesiredType Is GetType(Decimal) Then
            Exit Sub
        End If

        ' Converts the string back to datetime
        cevent.Value = CType(cevent.Value, DateTime)
    End Sub

#End Region

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
    Friend WithEvents dgClaimHist As System.Windows.Forms.DataGrid
    Friend WithEvents lblInsured As System.Windows.Forms.Label
    Friend WithEvents txtInsured As System.Windows.Forms.TextBox
    Friend WithEvents lblAccidentDate As System.Windows.Forms.Label
    Friend WithEvents lblHospitalIn As System.Windows.Forms.Label
    Friend WithEvents lblHospitalOut As System.Windows.Forms.Label
    Friend WithEvents lblReceiveDate As System.Windows.Forms.Label
    Friend WithEvents lblSettleDate As System.Windows.Forms.Label
    Friend WithEvents lblSymptomDate As System.Windows.Forms.Label
    Friend WithEvents lblConsultDate As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblClaimStatus As System.Windows.Forms.Label
    Friend WithEvents txtClaimStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtAccidentDate As System.Windows.Forms.TextBox
    Friend WithEvents txtHospitalIn As System.Windows.Forms.TextBox
    Friend WithEvents txtHospitalOut As System.Windows.Forms.TextBox
    Friend WithEvents txtSettleDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSymptomDate As System.Windows.Forms.TextBox
    Friend WithEvents txtConsultDate As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiveDate As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents dgChequeInfo As System.Windows.Forms.DataGrid
    Friend WithEvents lblChequeInfo As System.Windows.Forms.Label
    Friend WithEvents lblBenefit As System.Windows.Forms.Label
    Friend WithEvents dgBenefit As System.Windows.Forms.DataGrid
    Friend WithEvents lblPending As System.Windows.Forms.Label
    Friend WithEvents dgPending As System.Windows.Forms.DataGrid
    Friend WithEvents butUpdate As System.Windows.Forms.Button
    Friend WithEvents txtCrAcNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCrAcNm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAccDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIllDesc As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgClaimHist = New System.Windows.Forms.DataGrid()
        Me.lblInsured = New System.Windows.Forms.Label()
        Me.txtInsured = New System.Windows.Forms.TextBox()
        Me.lblAccidentDate = New System.Windows.Forms.Label()
        Me.lblHospitalIn = New System.Windows.Forms.Label()
        Me.lblHospitalOut = New System.Windows.Forms.Label()
        Me.lblReceiveDate = New System.Windows.Forms.Label()
        Me.lblSettleDate = New System.Windows.Forms.Label()
        Me.lblSymptomDate = New System.Windows.Forms.Label()
        Me.lblConsultDate = New System.Windows.Forms.Label()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblClaimStatus = New System.Windows.Forms.Label()
        Me.txtClaimStatus = New System.Windows.Forms.TextBox()
        Me.txtAccidentDate = New System.Windows.Forms.TextBox()
        Me.txtHospitalIn = New System.Windows.Forms.TextBox()
        Me.txtHospitalOut = New System.Windows.Forms.TextBox()
        Me.txtSettleDate = New System.Windows.Forms.TextBox()
        Me.txtSymptomDate = New System.Windows.Forms.TextBox()
        Me.txtConsultDate = New System.Windows.Forms.TextBox()
        Me.txtReceiveDate = New System.Windows.Forms.TextBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.dgChequeInfo = New System.Windows.Forms.DataGrid()
        Me.lblChequeInfo = New System.Windows.Forms.Label()
        Me.lblBenefit = New System.Windows.Forms.Label()
        Me.dgBenefit = New System.Windows.Forms.DataGrid()
        Me.lblPending = New System.Windows.Forms.Label()
        Me.dgPending = New System.Windows.Forms.DataGrid()
        Me.butUpdate = New System.Windows.Forms.Button()
        Me.txtCrAcNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCrAcNm = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAccDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIllDesc = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSurgical = New System.Windows.Forms.TextBox()
        Me.txtImpairment1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtImpairment2 = New System.Windows.Forms.TextBox()
        Me.txtImpairment3 = New System.Windows.Forms.TextBox()
        Me.txtImpairment4 = New System.Windows.Forms.TextBox()
        Me.chkDoc1 = New System.Windows.Forms.CheckBox()
        Me.chkDoc2 = New System.Windows.Forms.CheckBox()
        Me.chkDoc3 = New System.Windows.Forms.CheckBox()
        Me.chkDoc4 = New System.Windows.Forms.CheckBox()
        Me.dgTtlClaim = New System.Windows.Forms.DataGrid()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPayDet = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtLifeLimit = New System.Windows.Forms.TextBox()
        Me.txtAnnualLimit = New System.Windows.Forms.TextBox()
        Me.lblLifeLimit = New System.Windows.Forms.Label()
        Me.lblAnnualLimit = New System.Windows.Forms.Label()
        Me.lblAnnDeductible = New System.Windows.Forms.Label()
        Me.txtAnnDeductible = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLifeRemainLimit = New System.Windows.Forms.TextBox()
        Me.lblLifeRemainLimit = New System.Windows.Forms.Label()
        Me.txtLifeUsedLimit = New System.Windows.Forms.TextBox()
        Me.lblLifeUsedLimit = New System.Windows.Forms.Label()
        Me.txtAnnualRemainLimit = New System.Windows.Forms.TextBox()
        Me.lblAnnualRemainLimit = New System.Windows.Forms.Label()
        Me.txtAnnDeductibleBal = New System.Windows.Forms.TextBox()
        Me.lblAnnDeductibleBal = New System.Windows.Forms.Label()
        Me.chk7ElevenPay = New System.Windows.Forms.CheckBox()
        Me.txt7ElevenMobileNo = New System.Windows.Forms.TextBox()
        Me.lbl7ElevenMobileNo = New System.Windows.Forms.Label()
        Me.chkfps = New System.Windows.Forms.CheckBox()
        Me.btnfpsdetail = New System.Windows.Forms.Button()
        Me.gpbclaimsnum = New System.Windows.Forms.GroupBox()
        Me.lblRcsRegNo = New System.Windows.Forms.Label()
        Me.txtRcsRegNo = New System.Windows.Forms.TextBox()
        Me.txtclaimsoccur = New System.Windows.Forms.TextBox()
        Me.txtclaimsnum = New System.Windows.Forms.TextBox()
        Me.lblclaimsoccur = New System.Windows.Forms.Label()
        Me.lblclaimsnum = New System.Windows.Forms.Label()
        Me.txtpolicyno = New System.Windows.Forms.TextBox()
        Me.lblVATGST = New System.Windows.Forms.Label()
        Me.txtVATGST = New System.Windows.Forms.TextBox()
        Me.btnIwsClaimHist = New System.Windows.Forms.Button()
        CType(Me.dgClaimHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgChequeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgBenefit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgTtlClaim, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.gpbclaimsnum.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgClaimHist
        '
        Me.dgClaimHist.AllowNavigation = False
        Me.dgClaimHist.AllowSorting = False
        Me.dgClaimHist.AlternatingBackColor = System.Drawing.Color.White
        Me.dgClaimHist.BackColor = System.Drawing.Color.White
        Me.dgClaimHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgClaimHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgClaimHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgClaimHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgClaimHist.CaptionVisible = False
        Me.dgClaimHist.DataMember = ""
        Me.dgClaimHist.FlatMode = True
        Me.dgClaimHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgClaimHist.ForeColor = System.Drawing.Color.Black
        Me.dgClaimHist.GridLineColor = System.Drawing.Color.Wheat
        Me.dgClaimHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgClaimHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgClaimHist.HeaderForeColor = System.Drawing.Color.Black
        Me.dgClaimHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgClaimHist.Location = New System.Drawing.Point(8, 8)
        Me.dgClaimHist.Name = "dgClaimHist"
        Me.dgClaimHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgClaimHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgClaimHist.ParentRowsVisible = False
        Me.dgClaimHist.PreferredRowHeight = 10
        Me.dgClaimHist.ReadOnly = True
        Me.dgClaimHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgClaimHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgClaimHist.Size = New System.Drawing.Size(940, 120)
        Me.dgClaimHist.TabIndex = 0
        '
        'lblInsured
        '
        Me.lblInsured.Location = New System.Drawing.Point(8, 136)
        Me.lblInsured.Name = "lblInsured"
        Me.lblInsured.Size = New System.Drawing.Size(80, 16)
        Me.lblInsured.TabIndex = 17
        Me.lblInsured.Text = "Insured Name"
        '
        'txtInsured
        '
        Me.txtInsured.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsured.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsured.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInsured.Location = New System.Drawing.Point(96, 132)
        Me.txtInsured.Name = "txtInsured"
        Me.txtInsured.ReadOnly = True
        Me.txtInsured.Size = New System.Drawing.Size(256, 26)
        Me.txtInsured.TabIndex = 18
        '
        'lblAccidentDate
        '
        Me.lblAccidentDate.Location = New System.Drawing.Point(8, 160)
        Me.lblAccidentDate.Name = "lblAccidentDate"
        Me.lblAccidentDate.Size = New System.Drawing.Size(80, 16)
        Me.lblAccidentDate.TabIndex = 19
        Me.lblAccidentDate.Text = "Accident Date"
        '
        'lblHospitalIn
        '
        Me.lblHospitalIn.Location = New System.Drawing.Point(8, 184)
        Me.lblHospitalIn.Name = "lblHospitalIn"
        Me.lblHospitalIn.Size = New System.Drawing.Size(88, 16)
        Me.lblHospitalIn.TabIndex = 20
        Me.lblHospitalIn.Text = "Hospital In Date"
        '
        'lblHospitalOut
        '
        Me.lblHospitalOut.Location = New System.Drawing.Point(173, 184)
        Me.lblHospitalOut.Name = "lblHospitalOut"
        Me.lblHospitalOut.Size = New System.Drawing.Size(104, 16)
        Me.lblHospitalOut.TabIndex = 21
        Me.lblHospitalOut.Text = "Hospital Out Date"
        '
        'lblReceiveDate
        '
        Me.lblReceiveDate.Location = New System.Drawing.Point(8, 208)
        Me.lblReceiveDate.Name = "lblReceiveDate"
        Me.lblReceiveDate.Size = New System.Drawing.Size(104, 16)
        Me.lblReceiveDate.TabIndex = 22
        Me.lblReceiveDate.Text = "Receive Date"
        '
        'lblSettleDate
        '
        Me.lblSettleDate.Location = New System.Drawing.Point(8, 232)
        Me.lblSettleDate.Name = "lblSettleDate"
        Me.lblSettleDate.Size = New System.Drawing.Size(104, 16)
        Me.lblSettleDate.TabIndex = 23
        Me.lblSettleDate.Text = "Settlement Date"
        '
        'lblSymptomDate
        '
        Me.lblSymptomDate.Location = New System.Drawing.Point(174, 232)
        Me.lblSymptomDate.Name = "lblSymptomDate"
        Me.lblSymptomDate.Size = New System.Drawing.Size(104, 16)
        Me.lblSymptomDate.TabIndex = 24
        Me.lblSymptomDate.Text = "Symptom Date"
        '
        'lblConsultDate
        '
        Me.lblConsultDate.Location = New System.Drawing.Point(173, 208)
        Me.lblConsultDate.Name = "lblConsultDate"
        Me.lblConsultDate.Size = New System.Drawing.Size(104, 16)
        Me.lblConsultDate.TabIndex = 25
        Me.lblConsultDate.Text = "Consultation Date"
        '
        'lblComment
        '
        Me.lblComment.Location = New System.Drawing.Point(8, 288)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(104, 16)
        Me.lblComment.TabIndex = 26
        Me.lblComment.Text = "Comment"
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(364, 131)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(120, 16)
        Me.lblRemark.TabIndex = 27
        Me.lblRemark.Text = "Remark"
        '
        'lblClaimStatus
        '
        Me.lblClaimStatus.Location = New System.Drawing.Point(173, 160)
        Me.lblClaimStatus.Name = "lblClaimStatus"
        Me.lblClaimStatus.Size = New System.Drawing.Size(80, 16)
        Me.lblClaimStatus.TabIndex = 28
        Me.lblClaimStatus.Text = "Claim Status"
        '
        'txtClaimStatus
        '
        Me.txtClaimStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaimStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaimStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaimStatus.Location = New System.Drawing.Point(237, 156)
        Me.txtClaimStatus.Name = "txtClaimStatus"
        Me.txtClaimStatus.ReadOnly = True
        Me.txtClaimStatus.Size = New System.Drawing.Size(117, 26)
        Me.txtClaimStatus.TabIndex = 29
        '
        'txtAccidentDate
        '
        Me.txtAccidentDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccidentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccidentDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccidentDate.Location = New System.Drawing.Point(96, 156)
        Me.txtAccidentDate.Name = "txtAccidentDate"
        Me.txtAccidentDate.ReadOnly = True
        Me.txtAccidentDate.Size = New System.Drawing.Size(76, 26)
        Me.txtAccidentDate.TabIndex = 30
        '
        'txtHospitalIn
        '
        Me.txtHospitalIn.BackColor = System.Drawing.SystemColors.Window
        Me.txtHospitalIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHospitalIn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHospitalIn.Location = New System.Drawing.Point(96, 180)
        Me.txtHospitalIn.Name = "txtHospitalIn"
        Me.txtHospitalIn.ReadOnly = True
        Me.txtHospitalIn.Size = New System.Drawing.Size(76, 26)
        Me.txtHospitalIn.TabIndex = 31
        '
        'txtHospitalOut
        '
        Me.txtHospitalOut.BackColor = System.Drawing.SystemColors.Window
        Me.txtHospitalOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHospitalOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHospitalOut.Location = New System.Drawing.Point(276, 180)
        Me.txtHospitalOut.Name = "txtHospitalOut"
        Me.txtHospitalOut.ReadOnly = True
        Me.txtHospitalOut.Size = New System.Drawing.Size(76, 26)
        Me.txtHospitalOut.TabIndex = 32
        '
        'txtSettleDate
        '
        Me.txtSettleDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSettleDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSettleDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSettleDate.Location = New System.Drawing.Point(96, 228)
        Me.txtSettleDate.Name = "txtSettleDate"
        Me.txtSettleDate.ReadOnly = True
        Me.txtSettleDate.Size = New System.Drawing.Size(76, 26)
        Me.txtSettleDate.TabIndex = 33
        '
        'txtSymptomDate
        '
        Me.txtSymptomDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSymptomDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSymptomDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSymptomDate.Location = New System.Drawing.Point(276, 228)
        Me.txtSymptomDate.Name = "txtSymptomDate"
        Me.txtSymptomDate.ReadOnly = True
        Me.txtSymptomDate.Size = New System.Drawing.Size(76, 26)
        Me.txtSymptomDate.TabIndex = 34
        '
        'txtConsultDate
        '
        Me.txtConsultDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtConsultDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsultDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConsultDate.Location = New System.Drawing.Point(276, 204)
        Me.txtConsultDate.Name = "txtConsultDate"
        Me.txtConsultDate.ReadOnly = True
        Me.txtConsultDate.Size = New System.Drawing.Size(76, 26)
        Me.txtConsultDate.TabIndex = 35
        '
        'txtReceiveDate
        '
        Me.txtReceiveDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceiveDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceiveDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReceiveDate.Location = New System.Drawing.Point(96, 204)
        Me.txtReceiveDate.Name = "txtReceiveDate"
        Me.txtReceiveDate.ReadOnly = True
        Me.txtReceiveDate.Size = New System.Drawing.Size(76, 26)
        Me.txtReceiveDate.TabIndex = 36
        '
        'txtComment
        '
        Me.txtComment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComment.Location = New System.Drawing.Point(96, 284)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(388, 64)
        Me.txtComment.TabIndex = 37
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemark.Location = New System.Drawing.Point(360, 152)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = True
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(360, 72)
        Me.txtRemark.TabIndex = 38
        '
        'dgChequeInfo
        '
        Me.dgChequeInfo.AllowNavigation = False
        Me.dgChequeInfo.AlternatingBackColor = System.Drawing.Color.White
        Me.dgChequeInfo.BackColor = System.Drawing.Color.White
        Me.dgChequeInfo.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgChequeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgChequeInfo.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgChequeInfo.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgChequeInfo.CaptionVisible = False
        Me.dgChequeInfo.DataMember = ""
        Me.dgChequeInfo.FlatMode = True
        Me.dgChequeInfo.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgChequeInfo.ForeColor = System.Drawing.Color.Black
        Me.dgChequeInfo.GridLineColor = System.Drawing.Color.Wheat
        Me.dgChequeInfo.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgChequeInfo.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgChequeInfo.HeaderForeColor = System.Drawing.Color.Black
        Me.dgChequeInfo.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgChequeInfo.Location = New System.Drawing.Point(985, 455)
        Me.dgChequeInfo.Name = "dgChequeInfo"
        Me.dgChequeInfo.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgChequeInfo.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgChequeInfo.ReadOnly = True
        Me.dgChequeInfo.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgChequeInfo.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgChequeInfo.Size = New System.Drawing.Size(874, 122)
        Me.dgChequeInfo.TabIndex = 39
        '
        'lblChequeInfo
        '
        Me.lblChequeInfo.Location = New System.Drawing.Point(981, 431)
        Me.lblChequeInfo.Name = "lblChequeInfo"
        Me.lblChequeInfo.Size = New System.Drawing.Size(134, 21)
        Me.lblChequeInfo.TabIndex = 40
        Me.lblChequeInfo.Text = "Cheque Info - 0 item(s)"
        '
        'lblBenefit
        '
        Me.lblBenefit.Location = New System.Drawing.Point(10, 591)
        Me.lblBenefit.Name = "lblBenefit"
        Me.lblBenefit.Size = New System.Drawing.Size(144, 49)
        Me.lblBenefit.TabIndex = 41
        Me.lblBenefit.Text = "Benefit - 5 item(s)"
        '
        'dgBenefit
        '
        Me.dgBenefit.AllowNavigation = False
        Me.dgBenefit.AlternatingBackColor = System.Drawing.Color.White
        Me.dgBenefit.BackColor = System.Drawing.Color.White
        Me.dgBenefit.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgBenefit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgBenefit.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgBenefit.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgBenefit.CaptionVisible = False
        Me.dgBenefit.DataMember = ""
        Me.dgBenefit.FlatMode = True
        Me.dgBenefit.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgBenefit.ForeColor = System.Drawing.Color.Black
        Me.dgBenefit.GridLineColor = System.Drawing.Color.Wheat
        Me.dgBenefit.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgBenefit.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgBenefit.HeaderForeColor = System.Drawing.Color.Black
        Me.dgBenefit.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgBenefit.Location = New System.Drawing.Point(12, 614)
        Me.dgBenefit.Name = "dgBenefit"
        Me.dgBenefit.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgBenefit.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgBenefit.ReadOnly = True
        Me.dgBenefit.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgBenefit.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgBenefit.Size = New System.Drawing.Size(936, 122)
        Me.dgBenefit.TabIndex = 42
        '
        'lblPending
        '
        Me.lblPending.Location = New System.Drawing.Point(5, 433)
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(144, 16)
        Me.lblPending.TabIndex = 43
        Me.lblPending.Text = "Pending - 0 item(s)"
        '
        'dgPending
        '
        Me.dgPending.AllowNavigation = False
        Me.dgPending.AlternatingBackColor = System.Drawing.Color.White
        Me.dgPending.BackColor = System.Drawing.Color.White
        Me.dgPending.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgPending.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgPending.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgPending.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgPending.CaptionVisible = False
        Me.dgPending.DataMember = ""
        Me.dgPending.FlatMode = True
        Me.dgPending.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgPending.ForeColor = System.Drawing.Color.Black
        Me.dgPending.GridLineColor = System.Drawing.Color.Wheat
        Me.dgPending.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgPending.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgPending.HeaderForeColor = System.Drawing.Color.Black
        Me.dgPending.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgPending.Location = New System.Drawing.Point(10, 455)
        Me.dgPending.Name = "dgPending"
        Me.dgPending.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgPending.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgPending.ReadOnly = True
        Me.dgPending.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgPending.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgPending.Size = New System.Drawing.Size(940, 122)
        Me.dgPending.TabIndex = 44
        '
        'butUpdate
        '
        Me.butUpdate.Location = New System.Drawing.Point(490, 288)
        Me.butUpdate.Name = "butUpdate"
        Me.butUpdate.Size = New System.Drawing.Size(64, 27)
        Me.butUpdate.TabIndex = 46
        Me.butUpdate.Text = "Update"
        '
        'txtCrAcNo
        '
        Me.txtCrAcNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCrAcNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCrAcNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCrAcNo.Location = New System.Drawing.Point(432, 234)
        Me.txtCrAcNo.Name = "txtCrAcNo"
        Me.txtCrAcNo.ReadOnly = True
        Me.txtCrAcNo.Size = New System.Drawing.Size(116, 26)
        Me.txtCrAcNo.TabIndex = 48
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(364, 238)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Cr. A/C No."
        '
        'txtCrAcNm
        '
        Me.txtCrAcNm.BackColor = System.Drawing.SystemColors.Window
        Me.txtCrAcNm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCrAcNm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCrAcNm.Location = New System.Drawing.Point(600, 234)
        Me.txtCrAcNm.Name = "txtCrAcNm"
        Me.txtCrAcNm.ReadOnly = True
        Me.txtCrAcNm.Size = New System.Drawing.Size(120, 26)
        Me.txtCrAcNm.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(556, 238)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Name"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 258)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Accident Desc."
        '
        'txtAccDesc
        '
        Me.txtAccDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccDesc.Location = New System.Drawing.Point(96, 254)
        Me.txtAccDesc.Name = "txtAccDesc"
        Me.txtAccDesc.ReadOnly = True
        Me.txtAccDesc.Size = New System.Drawing.Size(248, 26)
        Me.txtAccDesc.TabIndex = 52
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(364, 262)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Illness Desc."
        '
        'txtIllDesc
        '
        Me.txtIllDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtIllDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIllDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIllDesc.Location = New System.Drawing.Point(452, 258)
        Me.txtIllDesc.Name = "txtIllDesc"
        Me.txtIllDesc.ReadOnly = True
        Me.txtIllDesc.Size = New System.Drawing.Size(268, 26)
        Me.txtIllDesc.TabIndex = 54
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 358)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 16)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "Surgical Name"
        '
        'txtSurgical
        '
        Me.txtSurgical.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurgical.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurgical.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSurgical.Location = New System.Drawing.Point(96, 354)
        Me.txtSurgical.Name = "txtSurgical"
        Me.txtSurgical.ReadOnly = True
        Me.txtSurgical.Size = New System.Drawing.Size(248, 26)
        Me.txtSurgical.TabIndex = 59
        '
        'txtImpairment1
        '
        Me.txtImpairment1.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpairment1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpairment1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpairment1.Location = New System.Drawing.Point(96, 379)
        Me.txtImpairment1.Name = "txtImpairment1"
        Me.txtImpairment1.ReadOnly = True
        Me.txtImpairment1.Size = New System.Drawing.Size(338, 26)
        Me.txtImpairment1.TabIndex = 61
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 383)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 20)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Impairment Code"
        '
        'txtImpairment2
        '
        Me.txtImpairment2.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpairment2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpairment2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpairment2.Location = New System.Drawing.Point(96, 402)
        Me.txtImpairment2.Name = "txtImpairment2"
        Me.txtImpairment2.ReadOnly = True
        Me.txtImpairment2.Size = New System.Drawing.Size(338, 26)
        Me.txtImpairment2.TabIndex = 62
        '
        'txtImpairment3
        '
        Me.txtImpairment3.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpairment3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpairment3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpairment3.Location = New System.Drawing.Point(440, 379)
        Me.txtImpairment3.Name = "txtImpairment3"
        Me.txtImpairment3.ReadOnly = True
        Me.txtImpairment3.Size = New System.Drawing.Size(338, 26)
        Me.txtImpairment3.TabIndex = 63
        '
        'txtImpairment4
        '
        Me.txtImpairment4.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpairment4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpairment4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpairment4.Location = New System.Drawing.Point(440, 402)
        Me.txtImpairment4.Name = "txtImpairment4"
        Me.txtImpairment4.ReadOnly = True
        Me.txtImpairment4.Size = New System.Drawing.Size(338, 26)
        Me.txtImpairment4.TabIndex = 64
        '
        'chkDoc1
        '
        Me.chkDoc1.AutoSize = True
        Me.chkDoc1.Enabled = False
        Me.chkDoc1.Location = New System.Drawing.Point(491, 332)
        Me.chkDoc1.Name = "chkDoc1"
        Me.chkDoc1.Size = New System.Drawing.Size(192, 24)
        Me.chkDoc1.TabIndex = 65
        Me.chkDoc1.Text = "Direct Mailing to Client"
        Me.chkDoc1.UseVisualStyleBackColor = True
        '
        'chkDoc2
        '
        Me.chkDoc2.AutoSize = True
        Me.chkDoc2.Enabled = False
        Me.chkDoc2.Location = New System.Drawing.Point(630, 332)
        Me.chkDoc2.Name = "chkDoc2"
        Me.chkDoc2.Size = New System.Drawing.Size(195, 24)
        Me.chkDoc2.TabIndex = 66
        Me.chkDoc2.Text = "Cheque Delivers to CS"
        Me.chkDoc2.UseVisualStyleBackColor = True
        '
        'chkDoc3
        '
        Me.chkDoc3.AutoSize = True
        Me.chkDoc3.Enabled = False
        Me.chkDoc3.Location = New System.Drawing.Point(491, 354)
        Me.chkDoc3.Name = "chkDoc3"
        Me.chkDoc3.Size = New System.Drawing.Size(166, 24)
        Me.chkDoc3.TabIndex = 67
        Me.chkDoc3.Text = "Manual Statement"
        Me.chkDoc3.UseVisualStyleBackColor = True
        '
        'chkDoc4
        '
        Me.chkDoc4.AutoSize = True
        Me.chkDoc4.Enabled = False
        Me.chkDoc4.Location = New System.Drawing.Point(630, 354)
        Me.chkDoc4.Name = "chkDoc4"
        Me.chkDoc4.Size = New System.Drawing.Size(131, 24)
        Me.chkDoc4.TabIndex = 68
        Me.chkDoc4.Text = "Valid Referral"
        Me.chkDoc4.UseVisualStyleBackColor = True
        '
        'dgTtlClaim
        '
        Me.dgTtlClaim.AllowNavigation = False
        Me.dgTtlClaim.AlternatingBackColor = System.Drawing.Color.White
        Me.dgTtlClaim.BackColor = System.Drawing.Color.White
        Me.dgTtlClaim.BackgroundColor = System.Drawing.Color.Ivory
        Me.dgTtlClaim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgTtlClaim.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgTtlClaim.CaptionForeColor = System.Drawing.Color.Lavender
        Me.dgTtlClaim.CaptionVisible = False
        Me.dgTtlClaim.DataMember = ""
        Me.dgTtlClaim.FlatMode = True
        Me.dgTtlClaim.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgTtlClaim.ForeColor = System.Drawing.Color.Black
        Me.dgTtlClaim.GridLineColor = System.Drawing.Color.Wheat
        Me.dgTtlClaim.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.dgTtlClaim.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgTtlClaim.HeaderForeColor = System.Drawing.Color.Black
        Me.dgTtlClaim.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgTtlClaim.Location = New System.Drawing.Point(985, 614)
        Me.dgTtlClaim.Name = "dgTtlClaim"
        Me.dgTtlClaim.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgTtlClaim.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgTtlClaim.ReadOnly = True
        Me.dgTtlClaim.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgTtlClaim.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgTtlClaim.Size = New System.Drawing.Size(874, 122)
        Me.dgTtlClaim.TabIndex = 69
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(981, 590)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(269, 16)
        Me.Label7.TabIndex = 71
        Me.Label7.Text = "Total Accumulated Claim Payable"
        '
        'txtPayDet
        '
        Me.txtPayDet.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayDet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayDet.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPayDet.Location = New System.Drawing.Point(138, 762)
        Me.txtPayDet.Name = "txtPayDet"
        Me.txtPayDet.ReadOnly = True
        Me.txtPayDet.Size = New System.Drawing.Size(688, 26)
        Me.txtPayDet.TabIndex = 74
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 765)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(116, 20)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "Payment Detail"
        '
        'txtLifeLimit
        '
        Me.txtLifeLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtLifeLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLifeLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLifeLimit.Location = New System.Drawing.Point(139, 119)
        Me.txtLifeLimit.Name = "txtLifeLimit"
        Me.txtLifeLimit.ReadOnly = True
        Me.txtLifeLimit.Size = New System.Drawing.Size(113, 26)
        Me.txtLifeLimit.TabIndex = 80
        '
        'txtAnnualLimit
        '
        Me.txtAnnualLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnualLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnualLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnualLimit.Location = New System.Drawing.Point(139, 69)
        Me.txtAnnualLimit.Name = "txtAnnualLimit"
        Me.txtAnnualLimit.ReadOnly = True
        Me.txtAnnualLimit.Size = New System.Drawing.Size(113, 26)
        Me.txtAnnualLimit.TabIndex = 79
        '
        'lblLifeLimit
        '
        Me.lblLifeLimit.Location = New System.Drawing.Point(4, 123)
        Me.lblLifeLimit.Name = "lblLifeLimit"
        Me.lblLifeLimit.Size = New System.Drawing.Size(88, 16)
        Me.lblLifeLimit.TabIndex = 78
        Me.lblLifeLimit.Text = "Life Limit"
        '
        'lblAnnualLimit
        '
        Me.lblAnnualLimit.Location = New System.Drawing.Point(4, 73)
        Me.lblAnnualLimit.Name = "lblAnnualLimit"
        Me.lblAnnualLimit.Size = New System.Drawing.Size(88, 17)
        Me.lblAnnualLimit.TabIndex = 77
        Me.lblAnnualLimit.Text = "Annual Limit"
        '
        'lblAnnDeductible
        '
        Me.lblAnnDeductible.Location = New System.Drawing.Point(4, 25)
        Me.lblAnnDeductible.Name = "lblAnnDeductible"
        Me.lblAnnDeductible.Size = New System.Drawing.Size(158, 19)
        Me.lblAnnDeductible.TabIndex = 75
        Me.lblAnnDeductible.Text = "Annual Deductible"
        '
        'txtAnnDeductible
        '
        Me.txtAnnDeductible.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnDeductible.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnDeductible.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnDeductible.Location = New System.Drawing.Point(139, 21)
        Me.txtAnnDeductible.Name = "txtAnnDeductible"
        Me.txtAnnDeductible.ReadOnly = True
        Me.txtAnnDeductible.Size = New System.Drawing.Size(113, 26)
        Me.txtAnnDeductible.TabIndex = 76
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLifeRemainLimit)
        Me.GroupBox1.Controls.Add(Me.lblLifeRemainLimit)
        Me.GroupBox1.Controls.Add(Me.txtLifeUsedLimit)
        Me.GroupBox1.Controls.Add(Me.lblLifeUsedLimit)
        Me.GroupBox1.Controls.Add(Me.txtAnnualRemainLimit)
        Me.GroupBox1.Controls.Add(Me.lblAnnualRemainLimit)
        Me.GroupBox1.Controls.Add(Me.txtAnnDeductibleBal)
        Me.GroupBox1.Controls.Add(Me.lblAnnDeductibleBal)
        Me.GroupBox1.Controls.Add(Me.txtLifeLimit)
        Me.GroupBox1.Controls.Add(Me.txtAnnDeductible)
        Me.GroupBox1.Controls.Add(Me.txtAnnualLimit)
        Me.GroupBox1.Controls.Add(Me.lblAnnDeductible)
        Me.GroupBox1.Controls.Add(Me.lblLifeLimit)
        Me.GroupBox1.Controls.Add(Me.lblAnnualLimit)
        Me.GroupBox1.Location = New System.Drawing.Point(968, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(258, 193)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "TheOneMedical Remaining Limit"
        '
        'txtLifeRemainLimit
        '
        Me.txtLifeRemainLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtLifeRemainLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLifeRemainLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLifeRemainLimit.Location = New System.Drawing.Point(139, 165)
        Me.txtLifeRemainLimit.Name = "txtLifeRemainLimit"
        Me.txtLifeRemainLimit.ReadOnly = True
        Me.txtLifeRemainLimit.Size = New System.Drawing.Size(113, 26)
        Me.txtLifeRemainLimit.TabIndex = 88
        '
        'lblLifeRemainLimit
        '
        Me.lblLifeRemainLimit.Location = New System.Drawing.Point(4, 169)
        Me.lblLifeRemainLimit.Name = "lblLifeRemainLimit"
        Me.lblLifeRemainLimit.Size = New System.Drawing.Size(129, 16)
        Me.lblLifeRemainLimit.TabIndex = 87
        Me.lblLifeRemainLimit.Text = "Life Remaining Limit"
        '
        'txtLifeUsedLimit
        '
        Me.txtLifeUsedLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtLifeUsedLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLifeUsedLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLifeUsedLimit.Location = New System.Drawing.Point(139, 143)
        Me.txtLifeUsedLimit.Name = "txtLifeUsedLimit"
        Me.txtLifeUsedLimit.ReadOnly = True
        Me.txtLifeUsedLimit.Size = New System.Drawing.Size(113, 26)
        Me.txtLifeUsedLimit.TabIndex = 86
        '
        'lblLifeUsedLimit
        '
        Me.lblLifeUsedLimit.Location = New System.Drawing.Point(4, 147)
        Me.lblLifeUsedLimit.Name = "lblLifeUsedLimit"
        Me.lblLifeUsedLimit.Size = New System.Drawing.Size(88, 16)
        Me.lblLifeUsedLimit.TabIndex = 85
        Me.lblLifeUsedLimit.Text = "Life Used Limit"
        '
        'txtAnnualRemainLimit
        '
        Me.txtAnnualRemainLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnualRemainLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnualRemainLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnualRemainLimit.Location = New System.Drawing.Point(139, 94)
        Me.txtAnnualRemainLimit.Name = "txtAnnualRemainLimit"
        Me.txtAnnualRemainLimit.ReadOnly = True
        Me.txtAnnualRemainLimit.Size = New System.Drawing.Size(113, 26)
        Me.txtAnnualRemainLimit.TabIndex = 84
        '
        'lblAnnualRemainLimit
        '
        Me.lblAnnualRemainLimit.Location = New System.Drawing.Point(4, 98)
        Me.lblAnnualRemainLimit.Name = "lblAnnualRemainLimit"
        Me.lblAnnualRemainLimit.Size = New System.Drawing.Size(129, 19)
        Me.lblAnnualRemainLimit.TabIndex = 83
        Me.lblAnnualRemainLimit.Text = "Annual Remaining Limit"
        '
        'txtAnnDeductibleBal
        '
        Me.txtAnnDeductibleBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnDeductibleBal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnDeductibleBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnDeductibleBal.Location = New System.Drawing.Point(139, 44)
        Me.txtAnnDeductibleBal.Name = "txtAnnDeductibleBal"
        Me.txtAnnDeductibleBal.ReadOnly = True
        Me.txtAnnDeductibleBal.Size = New System.Drawing.Size(113, 26)
        Me.txtAnnDeductibleBal.TabIndex = 82
        '
        'lblAnnDeductibleBal
        '
        Me.lblAnnDeductibleBal.Location = New System.Drawing.Point(4, 48)
        Me.lblAnnDeductibleBal.Name = "lblAnnDeductibleBal"
        Me.lblAnnDeductibleBal.Size = New System.Drawing.Size(158, 19)
        Me.lblAnnDeductibleBal.TabIndex = 81
        Me.lblAnnDeductibleBal.Text = "Annual Deductible Balance"
        '
        'chk7ElevenPay
        '
        Me.chk7ElevenPay.AutoCheck = False
        Me.chk7ElevenPay.AutoSize = True
        Me.chk7ElevenPay.Location = New System.Drawing.Point(770, 332)
        Me.chk7ElevenPay.Name = "chk7ElevenPay"
        Me.chk7ElevenPay.Size = New System.Drawing.Size(97, 24)
        Me.chk7ElevenPay.TabIndex = 86
        Me.chk7ElevenPay.Text = "7-Eleven"
        Me.chk7ElevenPay.UseVisualStyleBackColor = True
        '
        'txt7ElevenMobileNo
        '
        Me.txt7ElevenMobileNo.BackColor = System.Drawing.SystemColors.Window
        Me.txt7ElevenMobileNo.Location = New System.Drawing.Point(832, 351)
        Me.txt7ElevenMobileNo.Name = "txt7ElevenMobileNo"
        Me.txt7ElevenMobileNo.ReadOnly = True
        Me.txt7ElevenMobileNo.Size = New System.Drawing.Size(110, 26)
        Me.txt7ElevenMobileNo.TabIndex = 88
        '
        'lbl7ElevenMobileNo
        '
        Me.lbl7ElevenMobileNo.AutoSize = True
        Me.lbl7ElevenMobileNo.Location = New System.Drawing.Point(768, 356)
        Me.lbl7ElevenMobileNo.Name = "lbl7ElevenMobileNo"
        Me.lbl7ElevenMobileNo.Size = New System.Drawing.Size(83, 20)
        Me.lbl7ElevenMobileNo.TabIndex = 87
        Me.lbl7ElevenMobileNo.Text = "Mobile No."
        '
        'chkfps
        '
        Me.chkfps.AutoSize = True
        Me.chkfps.Enabled = False
        Me.chkfps.Location = New System.Drawing.Point(770, 308)
        Me.chkfps.Name = "chkfps"
        Me.chkfps.Size = New System.Drawing.Size(147, 24)
        Me.chkfps.TabIndex = 89
        Me.chkfps.Text = "Faster Payment"
        Me.chkfps.UseVisualStyleBackColor = True
        '
        'btnfpsdetail
        '
        Me.btnfpsdetail.Enabled = False
        Me.btnfpsdetail.Location = New System.Drawing.Point(875, 308)
        Me.btnfpsdetail.Name = "btnfpsdetail"
        Me.btnfpsdetail.Size = New System.Drawing.Size(75, 37)
        Me.btnfpsdetail.TabIndex = 90
        Me.btnfpsdetail.Text = "FPS Detail"
        Me.btnfpsdetail.UseVisualStyleBackColor = True
        '
        'gpbclaimsnum
        '
        Me.gpbclaimsnum.Controls.Add(Me.lblRcsRegNo)
        Me.gpbclaimsnum.Controls.Add(Me.txtRcsRegNo)
        Me.gpbclaimsnum.Controls.Add(Me.txtclaimsoccur)
        Me.gpbclaimsnum.Controls.Add(Me.txtclaimsnum)
        Me.gpbclaimsnum.Controls.Add(Me.lblclaimsoccur)
        Me.gpbclaimsnum.Controls.Add(Me.lblclaimsnum)
        Me.gpbclaimsnum.Controls.Add(Me.txtpolicyno)
        Me.gpbclaimsnum.Location = New System.Drawing.Point(968, 207)
        Me.gpbclaimsnum.Name = "gpbclaimsnum"
        Me.gpbclaimsnum.Size = New System.Drawing.Size(258, 92)
        Me.gpbclaimsnum.TabIndex = 91
        Me.gpbclaimsnum.TabStop = False
        Me.gpbclaimsnum.Text = "Claims Number"
        '
        'lblRcsRegNo
        '
        Me.lblRcsRegNo.AutoSize = True
        Me.lblRcsRegNo.Location = New System.Drawing.Point(7, 68)
        Me.lblRcsRegNo.Name = "lblRcsRegNo"
        Me.lblRcsRegNo.Size = New System.Drawing.Size(117, 20)
        Me.lblRcsRegNo.TabIndex = 94
        Me.lblRcsRegNo.Text = "Claim Case No."
        '
        'txtRcsRegNo
        '
        Me.txtRcsRegNo.Enabled = False
        Me.txtRcsRegNo.Location = New System.Drawing.Point(90, 65)
        Me.txtRcsRegNo.Name = "txtRcsRegNo"
        Me.txtRcsRegNo.Size = New System.Drawing.Size(100, 26)
        Me.txtRcsRegNo.TabIndex = 93
        '
        'txtclaimsoccur
        '
        Me.txtclaimsoccur.Enabled = False
        Me.txtclaimsoccur.Location = New System.Drawing.Point(90, 39)
        Me.txtclaimsoccur.Name = "txtclaimsoccur"
        Me.txtclaimsoccur.Size = New System.Drawing.Size(100, 26)
        Me.txtclaimsoccur.TabIndex = 3
        '
        'txtclaimsnum
        '
        Me.txtclaimsnum.Enabled = False
        Me.txtclaimsnum.Location = New System.Drawing.Point(90, 13)
        Me.txtclaimsnum.Name = "txtclaimsnum"
        Me.txtclaimsnum.Size = New System.Drawing.Size(100, 26)
        Me.txtclaimsnum.TabIndex = 2
        '
        'lblclaimsoccur
        '
        Me.lblclaimsoccur.AutoSize = True
        Me.lblclaimsoccur.Location = New System.Drawing.Point(6, 42)
        Me.lblclaimsoccur.Name = "lblclaimsoccur"
        Me.lblclaimsoccur.Size = New System.Drawing.Size(102, 20)
        Me.lblclaimsoccur.TabIndex = 1
        Me.lblclaimsoccur.Text = "Claims Occur"
        '
        'lblclaimsnum
        '
        Me.lblclaimsnum.AutoSize = True
        Me.lblclaimsnum.Location = New System.Drawing.Point(7, 16)
        Me.lblclaimsnum.Name = "lblclaimsnum"
        Me.lblclaimsnum.Size = New System.Drawing.Size(116, 20)
        Me.lblclaimsnum.TabIndex = 0
        Me.lblclaimsnum.Text = "Claims Number"
        '
        'txtpolicyno
        '
        Me.txtpolicyno.Location = New System.Drawing.Point(90, 13)
        Me.txtpolicyno.Name = "txtpolicyno"
        Me.txtpolicyno.Size = New System.Drawing.Size(100, 26)
        Me.txtpolicyno.TabIndex = 92
        '
        'lblVATGST
        '
        Me.lblVATGST.AutoSize = True
        Me.lblVATGST.Location = New System.Drawing.Point(785, 383)
        Me.lblVATGST.Name = "lblVATGST"
        Me.lblVATGST.Size = New System.Drawing.Size(77, 20)
        Me.lblVATGST.TabIndex = 93
        Me.lblVATGST.Text = "VAT/GST"
        '
        'txtVATGST
        '
        Me.txtVATGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtVATGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVATGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVATGST.Location = New System.Drawing.Point(845, 379)
        Me.txtVATGST.Name = "txtVATGST"
        Me.txtVATGST.ReadOnly = True
        Me.txtVATGST.Size = New System.Drawing.Size(70, 26)
        Me.txtVATGST.TabIndex = 92
        '
        'btnIwsClaimHist
        '
        Me.btnIwsClaimHist.Enabled = False
        Me.btnIwsClaimHist.Location = New System.Drawing.Point(560, 288)
        Me.btnIwsClaimHist.Name = "btnIwsClaimHist"
        Me.btnIwsClaimHist.Size = New System.Drawing.Size(123, 27)
        Me.btnIwsClaimHist.TabIndex = 95
        Me.btnIwsClaimHist.Text = "IWS Claim Hist"
        '
        'ClaimHist_Asur
        '
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.Controls.Add(Me.btnIwsClaimHist)
        Me.Controls.Add(Me.gpbclaimsnum)
        Me.Controls.Add(Me.btnfpsdetail)
        Me.Controls.Add(Me.chkfps)
        Me.Controls.Add(Me.txt7ElevenMobileNo)
        Me.Controls.Add(Me.lbl7ElevenMobileNo)
        Me.Controls.Add(Me.chk7ElevenPay)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtPayDet)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dgTtlClaim)
        Me.Controls.Add(Me.chkDoc4)
        Me.Controls.Add(Me.chkDoc3)
        Me.Controls.Add(Me.chkDoc2)
        Me.Controls.Add(Me.chkDoc1)
        Me.Controls.Add(Me.txtImpairment4)
        Me.Controls.Add(Me.txtImpairment3)
        Me.Controls.Add(Me.txtImpairment2)
        Me.Controls.Add(Me.txtImpairment1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtSurgical)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtIllDesc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtAccDesc)
        Me.Controls.Add(Me.txtCrAcNm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCrAcNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.butUpdate)
        Me.Controls.Add(Me.dgPending)
        Me.Controls.Add(Me.lblPending)
        Me.Controls.Add(Me.dgBenefit)
        Me.Controls.Add(Me.lblBenefit)
        Me.Controls.Add(Me.lblChequeInfo)
        Me.Controls.Add(Me.dgChequeInfo)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.txtReceiveDate)
        Me.Controls.Add(Me.txtConsultDate)
        Me.Controls.Add(Me.txtSymptomDate)
        Me.Controls.Add(Me.txtSettleDate)
        Me.Controls.Add(Me.txtHospitalOut)
        Me.Controls.Add(Me.txtHospitalIn)
        Me.Controls.Add(Me.txtAccidentDate)
        Me.Controls.Add(Me.txtClaimStatus)
        Me.Controls.Add(Me.lblClaimStatus)
        Me.Controls.Add(Me.lblRemark)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.lblConsultDate)
        Me.Controls.Add(Me.lblSymptomDate)
        Me.Controls.Add(Me.lblSettleDate)
        Me.Controls.Add(Me.lblReceiveDate)
        Me.Controls.Add(Me.lblHospitalOut)
        Me.Controls.Add(Me.lblHospitalIn)
        Me.Controls.Add(Me.lblAccidentDate)
        Me.Controls.Add(Me.lblInsured)
        Me.Controls.Add(Me.txtInsured)
        Me.Controls.Add(Me.dgClaimHist)
        Me.Controls.Add(Me.lblVATGST)
        Me.Controls.Add(Me.txtVATGST)
        Me.Name = "ClaimHist_Asur"
        Me.Size = New System.Drawing.Size(4508, 2366)
        CType(Me.dgClaimHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgChequeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgBenefit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgTtlClaim, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gpbclaimsnum.ResumeLayout(False)
        Me.gpbclaimsnum.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub DataGrid_Paint(sender As Object, e As PaintEventArgs) Handles dgChequeInfo.Paint, dgBenefit.Paint, dgPending.Paint
        Dim dg As DataGrid = CType(sender, DataGrid)
        Dim iRec As Integer

        'Show the record count of the DataGrid section
        Try
            iRec = Me.BindingContext(dg.DataSource, dg.DataMember).Count
        Catch ex As Exception
            iRec = 0
        End Try

        Select Case dg.Name
            Case "dgChequeInfo"
                lblChequeInfo.Text = "Cheque Info - " & iRec & " item(s)"
            Case "dgBenefit"
                lblBenefit.Text = "Benefit - " & iRec & " item(s)"
            Case "dgPending"
                lblPending.Text = "Pending - " & iRec & " item(s)"
        End Select
    End Sub

    Private Sub butUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butUpdate.Click
        UpdateComment()
    End Sub

    Private Sub dgClaimHist_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgClaimHist.CurrentCellChanged
        If lastClaimHistRowIndex <> dgClaimHist.CurrentRowIndex Then
            lastClaimHistRowIndex = dgClaimHist.CurrentRowIndex

            'Whenever the current cell changed, check whether the comments field has been modified
            'If it does, prompt the user for saving
            Me.BindingContext(dsClaim, "mcsvw_claim_header_details").EndCurrentEdit()
            Dim tempdt As DataTable = dsClaim.Tables("mcsvw_claim_header_details").GetChanges(DataRowState.Modified)
            If tempdt IsNot Nothing Then
                If tempdt.Rows.Count > 0 Then
                    If MsgBox("Do you want to save the modified data?", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
                        UpdateComment()
                    Else
                        dsClaim.Tables("mcsvw_claim_header_details").RejectChanges()
                    End If
                End If

                tempdt.Dispose()
            End If

            ' update claim status
            UpdateSts()

            'ITDCPI FPS Payout check FPS status after loading the claims data
            CheckFPSRecord(txtclaimsnum.Text, txtclaimsoccur.Text)

            ' save minor claim audit trail
            If txtclaimsnum.Text.Length > 0 AndAlso txtpolicyno.Text.Length > 0 AndAlso txtInsured.Text.Length > 0 AndAlso txtclaimsoccur.Text.Length > 0 Then
                ClaimsAudit(txtclaimsnum.Text, txtpolicyno.Text, txtInsured.Text, txtclaimsoccur.Text)
            End If
        End If
    End Sub

    Private Sub DataGrid_MouseUp(sender As Object, e As MouseEventArgs) Handles dgClaimHist.MouseUp, dgChequeInfo.MouseUp, dgBenefit.MouseUp, dgPending.MouseUp, dgTtlClaim.MouseUp
        Dim dg As DataGrid = CType(sender, DataGrid)
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dg.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            dg.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            dg.Select(hti.Row)
        End If
    End Sub

    Private Sub UpdateSts()
        Dim dr As DataRow = CType(Me.BindingContext(dsClaim, "mcsvw_claim_header_details").Current, DataRowView).Row

        txtClaimStatus.Text = dr.Item("mcscst_status_ext_desc")

        chkDoc1.Checked = IIf(dr.Item("mcschd_return_doc1") = "MAL", True, False)
        chkDoc2.Checked = IIf(dr.Item("mcschd_return_doc2") = "CHQ", True, False)
        chkDoc3.Checked = IIf(dr.Item("mcschd_return_doc3") = "MAN", True, False)
        chkDoc4.Checked = IIf(dr.Item("mcschd_return_doc4") = "REF", True, False)
    End Sub

#Region "FPS Payout"
    Dim fpspolnum As String = "N/A"
    Dim fpsclaimsnum As String = "N/A"
    Dim fpsproxytype As String = "N/A"
    Dim fpsproxyid As String = "N/A"
    Dim fpslasttrialtime As String = "N/A"
    Dim fpsstatus As String = "N/A"
    Dim fpsfailreason As String = "N/A"

    Private Sub btnfpsdetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfpsdetail.Click
        Dim FPSMessage As String = String.Empty
        FPSMessage += "Policy:" + vbNewLine + PolicyAccountID + vbNewLine + vbNewLine
        FPSMessage += "Claims Number:" + vbNewLine + fpsclaimsnum + vbNewLine + vbNewLine
        FPSMessage += "Proxy Type:" + vbNewLine + fpsproxytype + vbNewLine + vbNewLine
        FPSMessage += "Proxy ID:" + vbNewLine + fpsproxyid + vbNewLine + vbNewLine
        FPSMessage += "FPS Last Try Time:" + vbNewLine + fpslasttrialtime + vbNewLine + vbNewLine
        FPSMessage += "Payout Status:" + vbNewLine + fpsstatus + vbNewLine + vbNewLine
        FPSMessage += "Fail Reason:" + vbNewLine + fpsfailreason + vbNewLine + vbNewLine
        MessageBox.Show(FPSMessage, "FPS Detail", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CheckFPSRecord(ByVal strClaimsNum As String, ByVal strClaimsOccur As String)

        Dim dtResult As New DataTable
        Dim strErr As String = String.Empty
        Try
            If CompanyID <> g_McuComp AndAlso CompanyID <> g_LacComp AndAlso CompanyID <> g_LahComp Then    ' FPS not available for MCU,Assurance here
                Dim claimsBL As New ClaimsBL(DBHeader, MQQueuesHeader)
                claimsBL.GetFPSStatus(strClaimsNum, strClaimsOccur, dtResult, strErr)
                If strErr <> String.Empty Then
                    MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
                End If
            End If

            If dtResult.Rows.Count = 0 Then
                chkfps.Checked = False
                btnfpsdetail.Enabled = False : Exit Sub
            Else
                chkfps.Checked = True
                btnfpsdetail.Enabled = True
                fpspolnum = HandleDBNull(dtResult.Rows(0)("policyaccountid"))
                fpsclaimsnum = HandleDBNull(dtResult.Rows(0)("claimsnum")) + "(claims occur: " + HandleDBNull(dtResult.Rows(0)("claimsoccur")) + ")"
                fpsproxytype = HandleDBNull(dtResult.Rows(0)("proxytype"))
                Dim cntrycd As String = HandleDBNull(dtResult.Rows(0)("cntrycd"))
                If cntrycd.Length > 0 Then
                    fpsproxyid = "+" + HandleDBNull(dtResult.Rows(0)("cntrycd")) + " - " + HandleDBNull(dtResult.Rows(0)("proxyid"))
                Else
                    fpsproxyid = HandleDBNull(dtResult.Rows(0)("proxyid"))
                End If
                fpslasttrialtime = HandleDBNull(dtResult.Rows(0)("fpslasttrialtime"))
                fpsstatus = HandleDBNull(dtResult.Rows(0)("FPSStatus"))
                fpsfailreason = HandleDBNull(dtResult.Rows(0)("failreason"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Function HandleDBNull(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return String.Empty
        Else
            Return obj.ToString()
        End If
    End Function
#End Region

    Private Sub ClaimsAudit(ByVal ClaimNo As String, ByVal PolicyNo As String, ByVal InsuredName As String, ByVal OccurNo As String)
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(CompanyID), "MinorClaimAuditTrail_Save", New Dictionary(Of String, String) From {
                {"Source", "CRS"},
                {"PolicyNo", PolicyNo},
                {"ClaimNo", ClaimNo},
                {"InsuredName", InsuredName},
                {"PolicyholderName", String.Empty},
                {"OccurNo", OccurNo},
                {"EventName", "CRS - Minor Claim History - Load"},
                {"UserID", Environment.UserName.ToUpper()},
                {"Remarks", String.Empty}
            })
        Catch ex As Exception
            'MsgBox("Error occurs when insert claim audit record. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            'MsgBox($"Fail to ClaimsAudit: {ex.Message}", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Function HandleRemainHeader(ByVal productCode As String) As String
        Dim rtn As String = "TheOneMedical Remaining Limit"

        Select Case productCode
            Case "HVS4"
                rtn = "vPrime Remaining Limit"
            Case "HVS6"
                rtn = "vTheOne Remaining Limit"
            Case "HVS7"
                rtn = "vBooster Remaining Limit"
            Case "HVS8"
                rtn = "vPrime Signature Remaining Limit"
        End Select

        Return rtn
    End Function

    Private Sub btnIwsClaimHist_Click(sender As Object, e As EventArgs) Handles btnIwsClaimHist.Click
        Dim iwsClaimHist As New frmIwsClaimHist
        iwsClaimHist.LoadHist(CompanyID, m_PolicyNumber)
        iwsClaimHist.ShowDialog()
    End Sub

End Class
