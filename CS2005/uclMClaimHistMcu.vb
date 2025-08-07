Imports System.Data.SqlClient

Public Class MClaimHistMcu
    Inherits System.Windows.Forms.UserControl

    Private objMQQueHeader As Utility.Utility.MQHeader      'MQQueHeader includes MQ conn. parameters
    Private objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters

    Public Property PolicyAccountID() As String
        Get
            Return m_PolicyNumber
        End Get
        Set(ByVal Value As String)
            m_PolicyNumber = Value
            If Not Value Is Nothing Then Call BuildUI()
        End Set
    End Property

#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region


    Dim sqlConnect As SqlConnection
    Dim sqlUpdConnect As SqlConnection
    Dim dsClaim As DataSet
    Dim WithEvents bm As BindingManagerBase

    Dim daClaimMaster As SqlDataAdapter         'DataApater for claim master
    'Dim daClaimStatus As SqlDataAdapter         'DataApater for claim status
    'Dim daCheque As SqlDataAdapter              'DataApater for cheque info
    'Dim daBenefit As SqlDataAdapter             'DataApater for benefit info
    Dim daPending As SqlDataAdapter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgBenefit As System.Windows.Forms.DataGrid
    Friend WithEvents btnDBSO As System.Windows.Forms.Button
    Friend WithEvents btnDBSOHistory As System.Windows.Forms.Button
    Friend WithEvents txtClaimNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents txtInsuredName As System.Windows.Forms.TextBox
    Friend WithEvents txtClaimOccur As System.Windows.Forms.TextBox
    'DataApater for pending claims

    Dim m_PolicyNumber As String

    ' This function will connect to the database, retrieve the claims history of the policy set in the PolicyNumber property
    Public Sub BuildUI()

        wndMain.Cursor = Cursors.WaitCursor
        RefreshData()
        InitUI()
        wndMain.Cursor = Cursors.Default

    End Sub

    ' This function initialize the user interface, set the styles of the datagrids
    Private Sub InitUI()

        ' Create layout of datagrid for Claim History
        Dim tsClaimHist As New clsDataGridTableStyle
        Dim cs As DataGridColumnStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "mjcchd_claim_no"
        cs.HeaderText = "Claim No."
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcchd_claim_occur"
        cs.HeaderText = "Claim Occur"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcctp_description"
        cs.HeaderText = "Claim Type"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "mjccst_status_ext_desc"
        cs.HeaderText = "Claim Status"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mjcchd_receive_date"
        cs.HeaderText = "Receive Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mjcchd_incident_date"
        cs.HeaderText = "Incident Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 60
        'cs.MappingName = "mjcchd_incident_code"
        'cs.HeaderText = "Code"
        'cs.NullText = gNULLText
        'tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mjcchd_incident_desc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mjcply_dbso_YN"
        cs.HeaderText = "DBSO"
        cs.NullText = gNULLText
        tsClaimHist.GridColumnStyles.Add(cs)


        'Map the table style to the grid
        tsClaimHist.MappingName = "mjc_claim_policy"
        dgClaimHist.TableStyles.Add(tsClaimHist)


        'Create layout of datagrid for payment Info
        Dim tsChequeInfo As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcpay_cheque_curr"
        cs.HeaderText = "Pay Currency"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcpay_cheque_amount"
        cs.HeaderText = "Pay Amount"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gNumFormat
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcpay_cheque_no"
        cs.HeaderText = "Cheque No."
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 80
        'cs.MappingName = "lcpchq_chq_status"
        'cs.HeaderText = "Cheque Status"
        'cs.NullText = gNULLText
        'tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcpay_cheque_issue"
        cs.HeaderText = "Issue Date"
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "mjcpay_payee"
        cs.HeaderText = "Payee Name"
        cs.NullText = gNULLText
        tsChequeInfo.GridColumnStyles.Add(cs)

        'Map the table style to the grid
        tsChequeInfo.MappingName = "mjc_claim_payment"
        dgChequeInfo.TableStyles.Add(tsChequeInfo)


        'Create layout of datagrid for Pending Claims Info
        Dim tsPending As New clsDataGridTableStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mjcpen_pending_date"
        cs.HeaderText = "Pending Date"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        tsPending.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "mjcpen_pending_code"
        'cs.HeaderText = "Pending Code"
        'cs.NullText = gNULLText
        'tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mjcpen_pending_desc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "mjcpen_resolved_date"
        cs.HeaderText = "Resolve Date"
        cs.NullText = gNULLText
        CType(cs, DataGridTextBoxColumn).Format = gDateFormat
        tsPending.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "mjcpen_resolved_code"
        'cs.HeaderText = "Resolve Code"
        'cs.NullText = gNULLText
        'tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "mjcpen_resolved_desc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "mjcpen_pending_status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        tsPending.GridColumnStyles.Add(cs)

        'Map the table style to the grid
        tsPending.MappingName = "mjc_claim_pending"
        dgPending.TableStyles.Add(tsPending)

        Dim tsBenefit As New clsDataGridTableStyle()
        cs = New DataGridTextBoxColumn()
        cs.Width = 160
        cs.MappingName = "mjcbtp_description"
        cs.HeaderText = "Benefit Type"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn()
        cs.Width = 90
        cs.MappingName = "mjccbd_debit"
        cs.HeaderText = "Debit"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn()
        cs.Width = 90
        cs.MappingName = "mjccbd_credit"
        cs.HeaderText = "Credit"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn()
        cs.Width = 90
        cs.MappingName = "mjccbd_benefit_from"
        cs.HeaderText = "From"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn()
        cs.Width = 90
        cs.MappingName = "mjccbd_benefit_to"
        cs.HeaderText = "To"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn()
        cs.Width = 90
        cs.MappingName = "mjccbd_remark"
        cs.HeaderText = "Remark"
        cs.NullText = gNULLText
        tsBenefit.GridColumnStyles.Add(cs)

        tsBenefit.MappingName = "mjc_claim_benefit_detail"
        dgBenefit.TableStyles.Add(tsBenefit)

    End Sub

    ' This function fill up the dataset and display the data
    Private Sub RefreshData()
        Dim CIWDB As String = gcMcuCIW
        Dim MCSDB As String = gcMcuMCS
        Dim POSDB As String = gcMcuPOS

        Dim strSQL, strClaim As String

        sqlConnect = New SqlConnection(strCIWMcuConn)
        sqlUpdConnect = New SqlConnection(strUPSConn)

        'ITSR933 FG R3 CE Start
        Dim strPolicyCap As String = GetMcuCapsilPolicyNo(m_PolicyNumber)

        strSQL = "Select *, CompanyID" &
            " from PolicyAccount pa with(nolock) " &
            " inner join " & MCSDB & "mjc_claim_policy mjcply with(nolock) on mjcply_policy_no = PolicyAccountID " &
            " inner join " & MCSDB & "mjc_claim_register mjcchd with(nolock) on mjcply_claim_no = mjcchd_claim_no And mjcply_claim_occur = mjcchd_claim_occur " &
            " inner join " & MCSDB & "mjc_claim_type mjcctp with(nolock) on mjcchd_type = mjcctp_type_id " &
            " inner join " & MCSDB & "mjc_claim_status mjccst with(nolock) on mjcchd_clm_status = mjccst_status " &
            " where mjcply_policy_no in ('" & m_PolicyNumber & "','" & strPolicyCap & "') " &
            " And mjcchd_clm_status <> 'DEL' " &
            " Order by mjcply_claim_no DESC, mjcply_claim_occur DESC;" &
            " Select * " &
            " From " & MCSDB & "mjc_claim_payment " &
            " Where mjcpay_policy_no in ('" & m_PolicyNumber & "','" & strPolicyCap & "') And isnull(mjcpay_policy_no,'') <> '' "

        daClaimMaster = New SqlDataAdapter(strSQL, sqlConnect)
        daClaimMaster.MissingSchemaAction = MissingSchemaAction.Add
        daClaimMaster.MissingMappingAction = MissingMappingAction.Passthrough
        'ITSR933 FG R3 CE End

        Try
            dsClaim = New DataSet
            daClaimMaster.TableMappings.Add("mjc_claim_policy1", "mjc_claim_payment")
            daClaimMaster.Fill(dsClaim, "mjc_claim_policy")

            dsClaim.Tables("mjc_claim_policy").Columns.Add("mjcply_dbso_YN")

            For Each drPol As DataRow In dsClaim.Tables("mjc_claim_policy").Rows

                If drPol("CompanyID").ToString().Trim() = "ING" Then
                    Dim strDBSOStatus As String = ""

                    If drPol("mjcply_approved") IsNot DBNull.Value AndAlso drPol("mjcply_approved") = True Then
                        strDBSOStatus = drPol("mjcply_dbso").ToString().Trim()
                    Else
                        ' check from BO if claim not approved
                        Dim dsDBSO As New DataSet()
                        Dim objPOSWS As POSWS.POSWS = POSWS_MCU()
                        Dim strErr As String

                        If Not objPOSWS.EnquireDBSO(m_PolicyNumber, dsDBSO, strErr) Then
                            If strErr.Contains("Contract type not allow") Then
                                drPol("mjcply_dbso_YN") = "N"
                            Else
                                MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        Else
                            strDBSOStatus = dsDBSO.Tables(0).Rows(0)("TSTATUS").ToString().Trim()
                        End If
                    End If

                    If strDBSOStatus = "M" OrElse strDBSOStatus = "I" OrElse strDBSOStatus = "C" Then
                        drPol("mjcply_dbso_YN") = "Y"
                    Else
                        drPol("mjcply_dbso_YN") = "N"
                    End If

                End If
            Next

            dsClaim.Tables("mjc_claim_policy").AcceptChanges()

        Catch sqlex As SqlException
            MsgBox("RefreshData(): " & sqlex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox("RefreshData(): " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        strClaim = ""
        For Each dr As DataRow In dsClaim.Tables("mjc_claim_policy").Rows
            If strClaim = "" Then
                strClaim = dr.Item("mjcply_claim_no")
            Else
                strClaim &= "," & dr.Item("mjcply_claim_no")
            End If
        Next


        If strClaim = "" Then
            Exit Sub
        End If

        'strSQL = "Select * " & _
        '    " From mjc_claim_pending " & _
        '    " Where mjcpen_claim_no in (" & strClaim & ") " & _
        '    " Order by mjcpen_claim_no, mjcpen_claim_occur, mjcpen_pending_seq"

        'MCS cloud migration change: mjc_claim_pending 
        strSQL = "Select * From " & MCSDB & "mjc_claim_pending " &
         "Where mjcpen_claim_no in (" & strClaim & ") " &
         "Order by mjcpen_claim_no, mjcpen_claim_occur, mjcpen_pending_seq"


        daPending = New SqlDataAdapter(strSQL, sqlConnect)
        daPending.MissingSchemaAction = MissingSchemaAction.Add
        daPending.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            daPending.Fill(dsClaim, "mjc_claim_pending")
        Catch sqlex As SqlException
            MsgBox("RefreshData(): " & sqlex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox("RefreshData(): " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        ' MJC benefit
        'strSQL = "select a.*, b.mjcbtp_description from mjc_claim_benefit_detail a inner join mjc_benefit_type b on a.mjccbd_benefit_type=b.mjcbtp_type_id where mjccbd_policy_no in ('" & m_PolicyNumber & "','" & strPolicyCap & "') And isnull(mjccbd_policy_no,'') <> '' "
        ' MCS cloud migration change:
        strSQL = "select a.*, b.mjcbtp_description from " & MCSDB & "mjc_claim_benefit_detail a" &
        " inner join " & MCSDB & "mjc_benefit_type b on a.mjccbd_benefit_type=b.mjcbtp_type_id " &
        " where mjccbd_policy_no in ('" & m_PolicyNumber & "','" & strPolicyCap & "') And isnull(mjccbd_policy_no,'') <> ''"

        Dim daBenefit As New SqlDataAdapter(strSQL, sqlConnect)
        daBenefit.MissingSchemaAction = MissingSchemaAction.Add
        daBenefit.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            daBenefit.Fill(dsClaim, "mjc_claim_benefit_detail")
        Catch sqlex As SqlException
            MsgBox("RefreshData(): " & sqlex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox("RefreshData(): " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        Try
            ' mjc_claim_payment
            Dim dcParent(1) As DataColumn
            Dim dcChild(1) As DataColumn
            dcParent(0) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_no")
            dcParent(1) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_occur")
            dcChild(0) = dsClaim.Tables("mjc_claim_payment").Columns("mjcpay_claim_no")
            dcChild(1) = dsClaim.Tables("mjc_claim_payment").Columns("mjcpay_claim_occur")
            dsClaim.Relations.Add("Master_Payment", dcParent, dcChild, False)

            ' mjc_claim_pending
            ReDim dcParent(1)
            ReDim dcChild(1)
            dcParent(0) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_no")
            dcParent(1) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_occur")
            dcChild(0) = dsClaim.Tables("mjc_claim_pending").Columns("mjcpen_claim_no")
            dcChild(1) = dsClaim.Tables("mjc_claim_pending").Columns("mjcpen_claim_occur")
            dsClaim.Relations.Add("Master_Pending", dcParent, dcChild, False)

            ' mjc_claim_benefit_detail
            ReDim dcParent(1)
            ReDim dcChild(1)
            dcParent(0) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_no")
            dcParent(1) = dsClaim.Tables("mjc_claim_policy").Columns("mjcchd_claim_occur")
            dcChild(0) = dsClaim.Tables("mjc_claim_benefit_detail").Columns("mjccbd_claim_no")
            dcChild(1) = dsClaim.Tables("mjc_claim_benefit_detail").Columns("mjccbd_claim_occur")
            dsClaim.Relations.Add("Master_Benefit", dcParent, dcChild, False)

            If dsClaim.Tables("mjc_claim_policy").Rows.Count > 0 Then BindControl()

        Catch sqlex As SqlException
            MsgBox("RefreshData(): " & sqlex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox("RefreshData(): " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    ' This function data bound all contorls on the form
    Private Sub BindControl()
        Dim b As Binding

        ClearAllBinding()

        dgClaimHist.SetDataBinding(dsClaim, "mjc_claim_policy")
        dgChequeInfo.SetDataBinding(dsClaim, "mjc_claim_policy.Master_Payment")
        dgPending.SetDataBinding(dsClaim, "mjc_claim_policy.Master_Pending")
        dgBenefit.SetDataBinding(dsClaim, "mjc_claim_policy.Master_Benefit")

        'Create data bindings of other controls
        txtClaimType.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcctp_description")
        txtClaimStatus.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjccst_status_ext_desc")

        b = New Binding("Text", dsClaim, "mjc_claim_policy.mjcchd_receive_date")
        AddHandler b.Format, AddressOf DateTimeToDateString
        AddHandler b.Parse, AddressOf DateStringToDateTime
        txtReceiveDate.DataBindings.Add(b)

        b = New Binding("Text", dsClaim, "mjc_claim_policy.mjcchd_incident_date")
        AddHandler b.Format, AddressOf DateTimeToDateString
        AddHandler b.Parse, AddressOf DateStringToDateTime
        txtIncidentDate.DataBindings.Add(b)

        txtCode.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcchd_incident_code")
        txtDesc.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcchd_incident_desc")
        txtPlaceIncd.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcchd_incident_place")
        txtComment.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcchd_comment")

        'b = New Binding("Text", dsClaim, "mjc_claim_pending.mjcpen_pending_date")
        'AddHandler b.Format, AddressOf DateTimeToDateString
        'AddHandler b.Parse, AddressOf DateStringToDateTime
        'txtPendingDate.DataBindings.Add(b)

        'b = New Binding("Text", dsClaim, "mjc_claim_pending.mjcpen_resolved_date")
        'AddHandler b.Format, AddressOf DateTimeToDateString
        'AddHandler b.Parse, AddressOf DateStringToDateTime
        'txtResolveDate.DataBindings.Add(b)

        txtPendingDesc.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.Master_Pending.mjcpen_pending_desc")
        'txtResolveDesc.DataBindings.Add("Text", dsClaim, "mjc_claim_pending.mjcpen_resolved_desc")

        txtClaimNo.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcply_claim_no")
        txtPolicyNo.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcply_policy_no")
        txtInsuredName.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcchd_insured_name")
        txtClaimOccur.DataBindings.Add("Text", dsClaim, "mjc_claim_policy.mjcply_claim_occur")

    End Sub

    ' This function clear the data binding of all controls on the form
    Private Sub ClearAllBinding()
        dgClaimHist.DataBindings.Clear()
        dgChequeInfo.DataBindings.Clear()
        dgPending.DataBindings.Clear()
        dgBenefit.DataBindings.Clear()
        txtClaimType.DataBindings.Clear()
        txtClaimStatus.DataBindings.Clear()
        txtReceiveDate.DataBindings.Clear()
        txtIncidentDate.DataBindings.Clear()
        txtCode.DataBindings.Clear()
        txtDesc.DataBindings.Clear()
        txtPlaceIncd.DataBindings.Clear()
        txtComment.DataBindings.Clear()
        'txtPendingDate.DataBindings.Clear()
        'txtPendingDesc.DataBindings.Clear()
        'txtResolveDate.DataBindings.Clear()
        'txtResolveDesc.DataBindings.Clear()
    End Sub

    ' This function update the comment field to the database
    ''' <summary>
    ''' Update comment
    ''' Lubin 2022-11-14 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Sub UpdateComment()
        'As the datagrid is bound to a view on the database, we need to do the update by executing a UPDATE SQL STATEMENT
        'directly in the mcs database
        '
        Dim tempdt As New DataTable

        Me.BindingContext(dsClaim, "mjc_claim_policy").EndCurrentEdit()
        tempdt = dsClaim.Tables("mjc_claim_policy").GetChanges(DataRowState.Modified)
        If Not (tempdt Is Nothing) Then
            If tempdt.Rows.Count > 0 Then
                For Each dr As DataRow In tempdt.Rows
                    Try
                        APIServiceBL.ExecAPIBusi(g_McuComp,
                                           "FRM_CLAIM_UPDATE_COMMENT",
                                           New Dictionary(Of String, String) From {
                                           {"mjcchdComment", dr("mjcchd_comment")},
                                           {"mjcchdClaimNo", dr("mjcchd_claim_no")},
                                           {"mjcchdClaimOccur", dr("mjcchd_claim_occur")}
                                           })
                        MsgBox("Record updated !", , "Record updated")
                    Catch ex As SqlException
                        MsgBox("Error occurs when updating the comments. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    End Try
                Next
                dsClaim.Tables("mjc_claim_policy").AcceptChanges()
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
    Friend WithEvents lblHospitalOut As System.Windows.Forms.Label
    Friend WithEvents lblReceiveDate As System.Windows.Forms.Label
    Friend WithEvents lblSettleDate As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents lblClaimStatus As System.Windows.Forms.Label
    Friend WithEvents txtClaimStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiveDate As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents lblPending As System.Windows.Forms.Label
    Friend WithEvents dgPending As System.Windows.Forms.DataGrid
    Friend WithEvents butUpdate As System.Windows.Forms.Button
    Friend WithEvents lblClaimType As System.Windows.Forms.Label
    Friend WithEvents txtClaimType As System.Windows.Forms.TextBox
    Friend WithEvents lblAccidentDate As System.Windows.Forms.Label
    Friend WithEvents dgChequeInfo As System.Windows.Forms.DataGrid
    Friend WithEvents lblChequeInfo As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPlaceIncd As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtIncidentDate As System.Windows.Forms.TextBox
    Friend WithEvents txtPendingDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgClaimHist = New System.Windows.Forms.DataGrid()
        Me.lblClaimType = New System.Windows.Forms.Label()
        Me.txtClaimType = New System.Windows.Forms.TextBox()
        Me.lblHospitalOut = New System.Windows.Forms.Label()
        Me.lblReceiveDate = New System.Windows.Forms.Label()
        Me.lblSettleDate = New System.Windows.Forms.Label()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.lblClaimStatus = New System.Windows.Forms.Label()
        Me.txtClaimStatus = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.txtPlaceIncd = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtReceiveDate = New System.Windows.Forms.TextBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.lblPending = New System.Windows.Forms.Label()
        Me.dgPending = New System.Windows.Forms.DataGrid()
        Me.butUpdate = New System.Windows.Forms.Button()
        Me.txtIncidentDate = New System.Windows.Forms.TextBox()
        Me.lblAccidentDate = New System.Windows.Forms.Label()
        Me.dgChequeInfo = New System.Windows.Forms.DataGrid()
        Me.lblChequeInfo = New System.Windows.Forms.Label()
        Me.txtPendingDesc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgBenefit = New System.Windows.Forms.DataGrid()
        Me.btnDBSO = New System.Windows.Forms.Button()
        Me.btnDBSOHistory = New System.Windows.Forms.Button()
        Me.txtClaimNo = New System.Windows.Forms.TextBox()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.txtInsuredName = New System.Windows.Forms.TextBox()
        Me.txtClaimOccur = New System.Windows.Forms.TextBox()
        CType(Me.dgClaimHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgChequeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgBenefit, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgClaimHist.Size = New System.Drawing.Size(712, 88)
        Me.dgClaimHist.TabIndex = 0
        '
        'lblClaimType
        '
        Me.lblClaimType.AutoSize = True
        Me.lblClaimType.Location = New System.Drawing.Point(28, 109)
        Me.lblClaimType.Name = "lblClaimType"
        Me.lblClaimType.Size = New System.Drawing.Size(59, 13)
        Me.lblClaimType.TabIndex = 17
        Me.lblClaimType.Text = "Claim Type"
        '
        'txtClaimType
        '
        Me.txtClaimType.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaimType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaimType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaimType.Location = New System.Drawing.Point(100, 105)
        Me.txtClaimType.Name = "txtClaimType"
        Me.txtClaimType.ReadOnly = True
        Me.txtClaimType.Size = New System.Drawing.Size(112, 20)
        Me.txtClaimType.TabIndex = 18
        '
        'lblHospitalOut
        '
        Me.lblHospitalOut.AutoSize = True
        Me.lblHospitalOut.Location = New System.Drawing.Point(268, 133)
        Me.lblHospitalOut.Name = "lblHospitalOut"
        Me.lblHospitalOut.Size = New System.Drawing.Size(32, 13)
        Me.lblHospitalOut.TabIndex = 21
        Me.lblHospitalOut.Text = "Code"
        '
        'lblReceiveDate
        '
        Me.lblReceiveDate.AutoSize = True
        Me.lblReceiveDate.Location = New System.Drawing.Point(228, 109)
        Me.lblReceiveDate.Name = "lblReceiveDate"
        Me.lblReceiveDate.Size = New System.Drawing.Size(73, 13)
        Me.lblReceiveDate.TabIndex = 22
        Me.lblReceiveDate.Text = "Receive Date"
        '
        'lblSettleDate
        '
        Me.lblSettleDate.AutoSize = True
        Me.lblSettleDate.Location = New System.Drawing.Point(8, 157)
        Me.lblSettleDate.Name = "lblSettleDate"
        Me.lblSettleDate.Size = New System.Drawing.Size(87, 13)
        Me.lblSettleDate.TabIndex = 23
        Me.lblSettleDate.Text = "Place of Incident"
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(40, 181)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(51, 13)
        Me.lblComment.TabIndex = 26
        Me.lblComment.Text = "Comment"
        '
        'lblClaimStatus
        '
        Me.lblClaimStatus.AutoSize = True
        Me.lblClaimStatus.Location = New System.Drawing.Point(400, 109)
        Me.lblClaimStatus.Name = "lblClaimStatus"
        Me.lblClaimStatus.Size = New System.Drawing.Size(65, 13)
        Me.lblClaimStatus.TabIndex = 28
        Me.lblClaimStatus.Text = "Claim Status"
        '
        'txtClaimStatus
        '
        Me.txtClaimStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaimStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaimStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaimStatus.Location = New System.Drawing.Point(472, 105)
        Me.txtClaimStatus.Name = "txtClaimStatus"
        Me.txtClaimStatus.ReadOnly = True
        Me.txtClaimStatus.Size = New System.Drawing.Size(240, 20)
        Me.txtClaimStatus.TabIndex = 29
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCode.Location = New System.Drawing.Point(304, 129)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(76, 20)
        Me.txtCode.TabIndex = 32
        '
        'txtPlaceIncd
        '
        Me.txtPlaceIncd.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlaceIncd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlaceIncd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlaceIncd.Location = New System.Drawing.Point(100, 153)
        Me.txtPlaceIncd.Name = "txtPlaceIncd"
        Me.txtPlaceIncd.ReadOnly = True
        Me.txtPlaceIncd.Size = New System.Drawing.Size(164, 20)
        Me.txtPlaceIncd.TabIndex = 33
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesc.Location = New System.Drawing.Point(384, 129)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.Size = New System.Drawing.Size(328, 44)
        Me.txtDesc.TabIndex = 35
        '
        'txtReceiveDate
        '
        Me.txtReceiveDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceiveDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceiveDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReceiveDate.Location = New System.Drawing.Point(304, 105)
        Me.txtReceiveDate.Name = "txtReceiveDate"
        Me.txtReceiveDate.ReadOnly = True
        Me.txtReceiveDate.Size = New System.Drawing.Size(76, 20)
        Me.txtReceiveDate.TabIndex = 36
        '
        'txtComment
        '
        Me.txtComment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComment.Location = New System.Drawing.Point(100, 177)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(612, 48)
        Me.txtComment.TabIndex = 37
        '
        'lblPending
        '
        Me.lblPending.Location = New System.Drawing.Point(8, 249)
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
        Me.dgPending.Location = New System.Drawing.Point(8, 265)
        Me.dgPending.Name = "dgPending"
        Me.dgPending.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgPending.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgPending.ReadOnly = True
        Me.dgPending.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgPending.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgPending.Size = New System.Drawing.Size(712, 96)
        Me.dgPending.TabIndex = 44
        '
        'butUpdate
        '
        Me.butUpdate.Location = New System.Drawing.Point(648, 233)
        Me.butUpdate.Name = "butUpdate"
        Me.butUpdate.Size = New System.Drawing.Size(64, 23)
        Me.butUpdate.TabIndex = 46
        Me.butUpdate.Text = "Update"
        '
        'txtIncidentDate
        '
        Me.txtIncidentDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncidentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncidentDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncidentDate.Location = New System.Drawing.Point(100, 129)
        Me.txtIncidentDate.Name = "txtIncidentDate"
        Me.txtIncidentDate.ReadOnly = True
        Me.txtIncidentDate.Size = New System.Drawing.Size(76, 20)
        Me.txtIncidentDate.TabIndex = 30
        '
        'lblAccidentDate
        '
        Me.lblAccidentDate.AutoSize = True
        Me.lblAccidentDate.Location = New System.Drawing.Point(24, 133)
        Me.lblAccidentDate.Name = "lblAccidentDate"
        Me.lblAccidentDate.Size = New System.Drawing.Size(71, 13)
        Me.lblAccidentDate.TabIndex = 19
        Me.lblAccidentDate.Text = "Incident Date"
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
        Me.dgChequeInfo.Location = New System.Drawing.Point(8, 437)
        Me.dgChequeInfo.Name = "dgChequeInfo"
        Me.dgChequeInfo.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgChequeInfo.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgChequeInfo.ReadOnly = True
        Me.dgChequeInfo.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgChequeInfo.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgChequeInfo.Size = New System.Drawing.Size(712, 96)
        Me.dgChequeInfo.TabIndex = 39
        '
        'lblChequeInfo
        '
        Me.lblChequeInfo.Location = New System.Drawing.Point(8, 421)
        Me.lblChequeInfo.Name = "lblChequeInfo"
        Me.lblChequeInfo.Size = New System.Drawing.Size(144, 16)
        Me.lblChequeInfo.TabIndex = 40
        Me.lblChequeInfo.Text = "Payment Info - 0 item(s)"
        '
        'txtPendingDesc
        '
        Me.txtPendingDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtPendingDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPendingDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPendingDesc.Location = New System.Drawing.Point(120, 365)
        Me.txtPendingDesc.Multiline = True
        Me.txtPendingDesc.Name = "txtPendingDesc"
        Me.txtPendingDesc.ReadOnly = True
        Me.txtPendingDesc.Size = New System.Drawing.Size(600, 44)
        Me.txtPendingDesc.TabIndex = 48
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 369)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Pending Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 545)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Benefit"
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
        Me.dgBenefit.Location = New System.Drawing.Point(8, 561)
        Me.dgBenefit.Name = "dgBenefit"
        Me.dgBenefit.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.dgBenefit.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgBenefit.ReadOnly = True
        Me.dgBenefit.SelectionBackColor = System.Drawing.Color.Wheat
        Me.dgBenefit.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgBenefit.Size = New System.Drawing.Size(712, 163)
        Me.dgBenefit.TabIndex = 49
        '
        'btnDBSO
        '
        Me.btnDBSO.Location = New System.Drawing.Point(386, 233)
        Me.btnDBSO.Name = "btnDBSO"
        Me.btnDBSO.Size = New System.Drawing.Size(79, 23)
        Me.btnDBSO.TabIndex = 51
        Me.btnDBSO.Text = "DBSO"
        '
        'btnDBSOHistory
        '
        Me.btnDBSOHistory.Location = New System.Drawing.Point(472, 233)
        Me.btnDBSOHistory.Name = "btnDBSOHistory"
        Me.btnDBSOHistory.Size = New System.Drawing.Size(137, 23)
        Me.btnDBSOHistory.TabIndex = 52
        Me.btnDBSOHistory.Text = "DBSO Payment History"
        '
        'txtClaimNo
        '
        Me.txtClaimNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaimNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaimNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaimNo.Location = New System.Drawing.Point(100, 205)
        Me.txtClaimNo.Name = "txtClaimNo"
        Me.txtClaimNo.ReadOnly = True
        Me.txtClaimNo.Size = New System.Drawing.Size(112, 20)
        Me.txtClaimNo.TabIndex = 53
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPolicyNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPolicyNo.Location = New System.Drawing.Point(100, 205)
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.ReadOnly = True
        Me.txtPolicyNo.Size = New System.Drawing.Size(112, 20)
        Me.txtPolicyNo.TabIndex = 54
        '
        'txtInsuredName
        '
        Me.txtInsuredName.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsuredName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsuredName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInsuredName.Location = New System.Drawing.Point(100, 205)
        Me.txtInsuredName.Name = "txtInsuredName"
        Me.txtInsuredName.ReadOnly = True
        Me.txtInsuredName.Size = New System.Drawing.Size(112, 20)
        Me.txtInsuredName.TabIndex = 55
        '
        'txtClaimOccur
        '
        Me.txtClaimOccur.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaimOccur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaimOccur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaimOccur.Location = New System.Drawing.Point(100, 205)
        Me.txtClaimOccur.Name = "txtClaimOccur"
        Me.txtClaimOccur.ReadOnly = True
        Me.txtClaimOccur.Size = New System.Drawing.Size(112, 20)
        Me.txtClaimOccur.TabIndex = 56
        '
        'MClaimHist
        '
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.txtClaimOccur)
        Me.Controls.Add(Me.txtInsuredName)
        Me.Controls.Add(Me.txtPolicyNo)
        Me.Controls.Add(Me.txtClaimNo)
        Me.Controls.Add(Me.btnDBSOHistory)
        Me.Controls.Add(Me.btnDBSO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgBenefit)
        Me.Controls.Add(Me.txtPendingDesc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.butUpdate)
        Me.Controls.Add(Me.dgPending)
        Me.Controls.Add(Me.lblPending)
        Me.Controls.Add(Me.lblChequeInfo)
        Me.Controls.Add(Me.dgChequeInfo)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.txtReceiveDate)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtPlaceIncd)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtIncidentDate)
        Me.Controls.Add(Me.txtClaimStatus)
        Me.Controls.Add(Me.lblClaimStatus)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.lblSettleDate)
        Me.Controls.Add(Me.lblReceiveDate)
        Me.Controls.Add(Me.lblHospitalOut)
        Me.Controls.Add(Me.lblAccidentDate)
        Me.Controls.Add(Me.lblClaimType)
        Me.Controls.Add(Me.txtClaimType)
        Me.Controls.Add(Me.dgClaimHist)
        Me.Name = "MClaimHist"
        Me.Size = New System.Drawing.Size(728, 736)
        CType(Me.dgClaimHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgChequeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgBenefit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


#End Region

    Private Sub dgChequeInfo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles dgChequeInfo.Paint
        Dim iRec As Int32

        'Show the record count of the Cheque info section
        Try
            iRec = Me.BindingContext(dsClaim, "mjc_claim_policy.Master_Payment").Count
        Catch ex As Exception
            iRec = 0
        End Try
        lblChequeInfo.Text = "Payment Info - " & iRec & " item(s)"
    End Sub

    Private Sub dgPending_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles dgPending.Paint
        Dim iRec As Int32

        'Show the record count of the Pending info section
        Try
            iRec = Me.BindingContext(dsClaim, "mjc_claim_policy.Master_Pending").Count
        Catch ex As Exception
            iRec = 0
        End Try
        lblPending.Text = "Pending - " & iRec & " item(s)"
    End Sub

    Private Sub butUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butUpdate.Click
        UpdateComment()
    End Sub

    Private Sub dgClaimHist_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgClaimHist.CurrentCellChanged

        Dim tempdt As New DataTable
        Dim cmd As New SqlCommand, dr As DataRow
        Dim sSQL As String

        'Whenever the current cell changed, check whether the comments field has been modified
        'If it does, prompt the user for saving
        Me.BindingContext(dsClaim, "mjc_claim_policy").EndCurrentEdit()
        tempdt = dsClaim.Tables("mjc_claim_policy").GetChanges(DataRowState.Modified)
        If Not (tempdt Is Nothing) Then
            If tempdt.Rows.Count > 0 Then
                If MsgBox("Do you want to save the modified data?", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then
                    UpdateComment()
                Else
                    dsClaim.Tables("mjc_claim_policy").RejectChanges()
                End If
            End If

            tempdt.Dispose()
        End If

    End Sub

    Private Sub dgClaimHist_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgClaimHist.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dgClaimHist.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            dgClaimHist.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            dgClaimHist.Select(hti.Row)
        End If
    End Sub

    Private Sub dgChequeInfo_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgChequeInfo.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dgChequeInfo.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            dgChequeInfo.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            dgChequeInfo.Select(hti.Row)
        End If
    End Sub

    Private Sub dgPending_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgPending.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dgPending.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            dgPending.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            dgPending.Select(hti.Row)
        End If
    End Sub

    Private Sub MClaimHist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        'If ExternalUser Then
        '    butUpdate.Enabled = False
        'End If
    End Sub

    Private Sub btnDBSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDBSO.Click
        Dim frm As New POSMain.frmDBSORegister(POSMain.Systems.CRS)
        frm.DBHeader = objDBHeader
        frm.MQQueuesHeader = objMQQueHeader
        frm.SetPolicyNumber = m_PolicyNumber
        frm.ShowDialog()
    End Sub

    Private Sub btnDBSOHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDBSOHistory.Click
        Dim frm As New POSMain.frmDBSOPaymentHistoryEnquiry()
        frm.DBHeader = objDBHeader
        frm.MQQueuesHeader = objMQQueHeader
        frm.policyNo = m_PolicyNumber
        frm.ShowDialog()
    End Sub

    Private Sub ClaimsAudit(ByVal ClaimNo As String, ByVal PolicyNo As String, ByVal InsuredName As String, ByVal OccurNo As String)
        Try
            Dim EventName As String = "CRS - Major Claim History - Load"
            Using wsCRS As New CRSWS.CRSWS()
                Dim response As New CRSWS.WSResponseOfObject
                'CRS_Util Enhancement disabled
                wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader)
                If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                    wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
                End If
#If DEBUG Then
                wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
#End If
                wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsCRS.Timeout = 10000000

                response = wsCRS.ClaimsAudit(g_McuComp, g_McuEnv, ClaimNo, PolicyNo, InsuredName, OccurNo, EventName)

                If response Is Nothing Or response.Success = False Then
                    MsgBox("Fail to ClaimsAudit :" + response.ErrorMsg, MsgBoxStyle.Exclamation)
                Else

                End If

            End Using
            Return
            'Dim sqlMcsConnect As SqlConnection
            'sqlMcsConnect = New SqlConnection(strMCSConn)
            'Dim cmd As New SqlCommand
            'cmd.CommandText = "exec SaveAuditTrail @Source,@PolicyNo,@ClaimNo,@InsuredName,@PolicyholderName,@OccurNo,@EventName,@UserID,@Remarks"
            'cmd.Parameters.Add("@Source", SqlDbType.NVarChar, 100)
            'cmd.Parameters.Add("@PolicyNo", SqlDbType.VarChar, 16)
            'cmd.Parameters.Add("@ClaimNo", SqlDbType.NVarChar, 20)
            'cmd.Parameters.Add("@InsuredName", SqlDbType.VarChar, 70)
            'cmd.Parameters.Add("@PolicyholderName", SqlDbType.NVarChar, 100)
            'cmd.Parameters.Add("@OccurNo", SqlDbType.NVarChar, 20)
            'cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50)
            'cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 10)
            'cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 65535)
            'cmd.Parameters("@Source").Value = "CRS"
            'cmd.Parameters("@PolicyNo").Value = PolicyNo
            'cmd.Parameters("@ClaimNo").Value = ClaimNo
            'cmd.Parameters("@InsuredName").Value = InsuredName
            'cmd.Parameters("@PolicyholderName").Value = ""
            'cmd.Parameters("@OccurNo").Value = OccurNo
            'cmd.Parameters("@EventName").Value = "CRS - Major Claim History - Load"
            'cmd.Parameters("@UserID").Value = Environment.UserName.ToUpper()
            'cmd.Parameters("@Remarks").Value = ""
            'cmd.Connection = sqlMcsConnect
            'sqlMcsConnect.Open()
            'cmd.ExecuteNonQuery()
            'sqlMcsConnect.Close()
        Catch ex As Exception
            'MsgBox("Error occurs when insert claim audit record. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub txtClaimNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClaimNo.TextChanged
        If (txtClaimNo.Text.Length > 0 And txtPolicyNo.Text.Length > 0 And txtClaimOccur.Text.Length > 0 And txtInsuredName.Text.Length > 0) Then
            ClaimsAudit(txtClaimNo.Text, txtPolicyNo.Text, txtInsuredName.Text, txtClaimOccur.Text)
        End If
    End Sub

    Private Sub txtPolicyNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPolicyNo.TextChanged
        If (txtClaimNo.Text.Length > 0 And txtPolicyNo.Text.Length > 0 And txtClaimOccur.Text.Length > 0 And txtInsuredName.Text.Length > 0) Then
            ClaimsAudit(txtClaimNo.Text, txtPolicyNo.Text, txtInsuredName.Text, txtClaimOccur.Text)
        End If
    End Sub

    Private Sub txtClaimOccur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClaimOccur.TextChanged
        If (txtClaimNo.Text.Length > 0 And txtPolicyNo.Text.Length > 0 And txtClaimOccur.Text.Length > 0 And txtInsuredName.Text.Length > 0) Then
            ClaimsAudit(txtClaimNo.Text, txtPolicyNo.Text, txtInsuredName.Text, txtClaimOccur.Text)
        End If
    End Sub

    Private Sub txtInsuredName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsuredName.TextChanged
        If (txtClaimNo.Text.Length > 0 And txtPolicyNo.Text.Length > 0 And txtClaimOccur.Text.Length > 0 And txtInsuredName.Text.Length > 0) Then
            ClaimsAudit(txtClaimNo.Text, txtPolicyNo.Text, txtInsuredName.Text, txtClaimOccur.Text)
        End If
    End Sub
End Class

