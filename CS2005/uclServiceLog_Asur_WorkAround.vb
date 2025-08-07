Imports System.Data.SqlClient
Imports ING

Public Class uclServiceLog_Asur_WorkAround
    Inherits System.Windows.Forms.UserControl

    Private strPolicy As String
    Private strCustID As String
    Private strNewFlag As String
    Private strPolicyType As String
    Private iPrevPosition As Integer
    Private blnIsCustLevel As Boolean = False
    Dim sqlConn As New SqlConnection
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
    Dim dsSrvLog As New DataSet
    Dim ServiceLogBL As New ServiceLogBL
    Dim bm As BindingManagerBase

    Public Delegate Sub EventSavedEventHandler(ByVal sender As Object, ByVal e As DataRow)

    Public Event EventSaved As EventSavedEventHandler

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

    'FindCustomerid() - Find customerid by policy number
    Private Function FindCustomerid(ByVal strPolicyAccID As String) As String
        Dim strTemp As String
        Dim sqlCmd As SqlCommand
        Dim sqlReader As SqlDataReader

        sqlConn.Open()
        strTemp = ""

        Try
            sqlCmd = New SqlCommand("Select Customerid From csw_poli_rel Where policyrelatecode = 'PH' and policyaccountid = '" & RTrim(strPolicyAccID) & "' ", sqlConn)
            sqlReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

            If sqlReader.Read() Then
                If Not sqlReader.IsDBNull(0) Then
                    strTemp = CStr(sqlReader.Item(0))
                End If

            End If
        Catch sqlEx As SqlException
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - FindCustomerid", sqlEx.Message, sqlEx)

            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - FindCustomerid", ex.Message, ex)

            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        Finally
            If Not sqlCmd Is Nothing Then
                sqlCmd.Dispose()
            End If
            If Not sqlReader Is Nothing Then
                sqlReader.Close()
            End If
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

        Return strTemp
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
                         "t1.* " &
                         ", PolicyAccountNo=t1.PolicyNo_Asur " &
                         ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, isnull(t8.name,'') sender_name " &
                         ", PolicyType='LIFE' " &
             "From ServiceEventDetail t1 " &
             "left join " & gcPOS & "vw_csw_event_category_code t5 on t1.EventCategoryCode=t5.cswecc_code " &
             "left join " & gcPOS & "vw_csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " &
             "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " &
             "left join " & serverPrefix & "CSR t8 on t1.MasterCSRID=t8.CSRID " &
             "left join " & serverPrefix & "EventStatusCodes as event on event.EventStatusCode = t1.EventStatusCode and event.CategoryCode= t1.EventCategoryCode and event.EventTypeCode = t1.EventTypeCode " &
             "left join " & serverPrefix & "EventStatusCodes as star on star.EventStatusCode = t1.EventStatusCode and star.CategoryCode= '*' and star.EventTypeCode = '*' "

        If strPolicy = "" Then
            strSQL += "Where t1.UCID = " & strCustID & " "
        Else
            strSQL += "Where t1.PolicyNo_Asur = '" & strPolicy & "' "
        End If
        'ITSR933 FG R3 Policy Number Change End

        strSQL += "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "
        'VHIS end - Add sort order to EventStatusCodes

        If strCustID = "" Then
            strCustID = FindCustomerid(strPolicy)
        End If
        daSrvEvtDet = New SqlDataAdapter(strSQL, sqlConn)
        daEvtCat = New SqlDataAdapter("Select * from " & gcPOS & "vw_csw_event_category_code order by cswecc_desc", sqlConn)
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
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - InitDataset", e.Message, e)

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


        ' Added by Hugo Chan on 2021-05-13, "CRS - First Level of Access", showing "Product Name" in grid control
        MapPolicyInfo(dsSrvLog.Tables("ServiceEventDetail"))

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
    End Sub

    ''' <summary>
    ''' Get policy info. from Assurance DB and assign values to the <paramref name="dt">dt</paramref> DataTable.
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-13
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub MapPolicyInfo(ByVal dt As DataTable)
        Dim lngErr As Long
        Dim strErr As String = String.Empty
        Dim dsPolicyList As DataSet = Nothing
        Dim dr As DataRow
        Dim drs As DataRow()
        Dim policyAccountNo As String
        Dim isChanged As Boolean = False
        Try
            ' Add the product name column if it is not existing
            If Not dt.Columns.Contains("ProductName") Then
                dt.Columns.Add("ProductName", Type.GetType("System.String"))
            End If

            If Not String.IsNullOrWhiteSpace(Me.CustomerID) Then
                ' get policy list by customer ID
                dsPolicyList = GetPolicyList_Asur_WorkAround(Me.CustomerID, "POLSTINF", False, lngErr, strErr)
            End If

            If String.IsNullOrWhiteSpace(strErr) Then
                If Not dsPolicyList Is Nothing Then
                    For Each dr In dt.Rows
                        policyAccountNo = Convert.ToString(dr("PolicyAccountNo"))
                        If Not String.IsNullOrWhiteSpace(policyAccountNo) Then
                            drs = dsPolicyList.Tables(0).Select(String.Format("POLICY = '{0}'", policyAccountNo.Replace("'", "''")))
                            If drs.Length > 0 Then
                                dr("ProductName") = Convert.ToString(drs(0)("PRODUCT"))
                                isChanged = True
                            End If
                        End If
                    Next


                    If isChanged Then
                        dt.AcceptChanges()
                    End If
                End If
            Else
                LogInformation("Error", "GetPolicyList_Asur", "MapPolicyInfo", strErr, Nothing)

                MsgBox(strErr)
            End If
        Catch ex As Exception
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - MapPolicyInfo", ex.Message, ex)

            MsgBox(ex.Message)
        Finally
            ' release resources
            If Not dsPolicyList Is Nothing Then
                dsPolicyList.Dispose()
                dsPolicyList = Nothing
            End If
        End Try

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
        hidPolicy.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "PolicyNo_Asur")
        hidCustID.DataBindings.Add("Text", dsSrvLog.Tables("ServiceEventDetail"), "UCID")
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
            dgSrvLog.Width = 850

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

        'getPostSalesCallInfo()

        AddHandler daSrvEvtDet.RowUpdating, AddressOf RowUpdating




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
                    LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - RowUpdating", sqlEx.Message, sqlEx)

                    MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
                Catch ex As Exception
                    LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - RowUpdating", ex.Message, ex)

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
        dr.Item("CustomerID") = System.DBNull.Value
        dr.Item("PolicyAccountID") = System.DBNull.Value
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
        dr.Item("UCID") = strCustID
        dr.Item("PolicyNo_Asur") = strPolicy

        'C001 - Start
        dr.Item("MCV") = ""
        'C001 - End
        Try
            dgSrvLog.AllowSorting = False
            dsSrvLog.Tables("ServiceEventDetail").Rows.Add(dr)

            Me.BindingContext(dsSrvLog.Tables("ServiceEventDetail")).Position = dsSrvLog.Tables("ServiceEventDetail").Rows.Count - 1
            dgSrvLog.AllowSorting = True
        Catch ex As Exception
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - btnNew_Click", ex.Message, ex)

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

    'SaveServiceLog() - Save the service log, called when CurrentCellChanged event of dgSrvLog or user click btnSave
    Private Sub SaveServiceLog(ByVal dr As DataRow)
        'SQLCommandBuilder - Create insert and update commandtext
        'Dim cmdBuilder As New SqlCommandBuilder(daSrvEvtDet)
        Dim sqlUpdCmd As SqlCommand
        Dim sqlInsCmd As SqlCommand
        Dim intNextSrvNo As Integer
        Dim strInsSQL As String
        Dim strUpdSQL As String

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
                'If rbLife.Checked Then
                dr.Item("PolicyNo_Asur") = dr.Item("PolicyAccountNo").ToString.Trim
                'ElseIf rbGI.Checked Then
                '    dr.Item("PolicyAccountID") = checkPolicyEndDate(dr.Item("PolicyAccountNo").ToString.Trim)
                'ElseIf rbEB.Checked Then
                '    If Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "12" Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "22" _
                '    Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "13" Or Microsoft.VisualBasic.Left(dr.Item("PolicyAccountNo").ToString.Trim, 2) = "23" Then
                '        dr.Item("PolicyAccountID") = GLGetPolicyID(dr.Item("PolicyAccountNo").ToString.Trim)
                '    Else
                '        dr.Item("PolicyAccountID") = checkPolicyEndDate(dr.Item("PolicyAccountNo").ToString.Trim)
                '    End If
                'End If
            End If
            'ElseIf strPolicyType = "GL" AndAlso Microsoft.VisualBasic.Left(strPolicy, 2) = "12" Or Microsoft.VisualBasic.Left(strPolicy, 2) = "22" _
            '                  Or Microsoft.VisualBasic.Left(strPolicy, 2) = "13" Or Microsoft.VisualBasic.Left(strPolicy, 2) = "23" Then
            '    dr.Item("PolicyAccountID") = strPolicyType & strPolicy
        End If
        'C001 - Start
        strInsSQL = "declare @no as numeric(15,0); " &
                    "Select @no = isnull(max(ServiceEventNumber),0) from ServiceEventDetail; " &
                    "update  csw_srv_evt_dtl_id set swID = @no+1; " &
                    "insert into ServiceEventDetail values " &
                    "((@no+1), '" & dr.Item("EventCategoryCode") & "', " &
                    "'" & dr.Item("EventTypeCode") & "', " &
                    "null, NULL, NULL, " &
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
                    "'" & dr.Item("IsIdVerify") & "','" & IIf(chkMCV.Checked, "Y", "N") & "', " & dr.Item("UCID") & ", '" & dr.Item("PolicyNo_Asur") & "'); "
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
                    "PolicyAccountID = NULL, " &
                    "PolicyNo_Asur = '" & dr.Item("PolicyNo_Asur") & "', " &
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
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - SaveServiceLog", eSQL.Message, eSQL)

            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Save data")
        Catch ex As Exception
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - SaveServiceLog", ex.Message, ex)

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

        'Refresh datagrid
        wndInbox.RefreshInbox()
        Refresh_ServiceLog()
    End Sub

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

        ' Added by Hugo Chan on 2021-05-13, "CRS - First Level of Access", showing "Product Name" in grid control
        MapPolicyInfo(dsSrvLog.Tables("ServiceEventDetail"))

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

        Me.lbl_InforceDate.Text = "N/A"
        Me.lbl_PostCallStatus.Text = "N/A"
        Me.lbl_PostCallCount.Text = "0 time(s)"

        Dim s_Sql As String = "cswsp_GetPostSalesCallList"

        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()


        daPostSalesCallInfo = New SqlDataAdapter(s_Sql, sqlConn)

        Try
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = IIf(strPolicy Is Nothing, "", strPolicy)
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = DBNull.Value
            daPostSalesCallInfo.SelectCommand.CommandType = CommandType.StoredProcedure
            daPostSalesCallInfo.Fill(dsSrvLog, "PostSalesCallInfo")

            doRefreshPostCallInfo()

        Catch e As Exception
            LogInformation("Error", "", "uclServiceLog_Asur_WorkAround - getPostSalesCallInfo", e.Message, e)

            MsgBox("Exception: " & Err.Description, MsgBoxStyle.Critical)
        Finally
            sqlConn.Close()
        End Try


    End Sub

    'added by ITDYMH 20150229 Post-Sales Call
    Private Sub doRefreshPostCallInfo()



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

            ElseIf dsSrvLog.Tables("PostSalesCallInfo").Rows(0).Item("CallType").ToString.IndexOf("ILAS") > -1 Then
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
        'If Not ServiceLogBL.GetSerLogbycriteriaNew("1900-01-01", "1900-01-01", "", "", strCustID, False, True, dsresult, strErr, "HK") Then
        If Not ServiceLogBL.GetSerLogbycriteriaNew("1900-01-01", "1900-01-01", "", "", strCustID, False, True, dsresult, strErr, "") Then
            MessageBox.Show(strErr)
            Exit Sub
        Else
            MapPolicyInfoToLogs(dsSrvLog.Tables("ServiceEventDetail"), dsresult)

            ServiceLogBL.Generatewebpage(dsresult)
        End If
    End Sub

    ''' <summary>
    ''' Get policy info. from the <paramref name="dt">dt</paramref> DataTable.
    ''' Assign policy info. to the <paramref name="dt">dsresult</paramref> DataSet.
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-13
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub MapPolicyInfoToLogs(ByVal dt As DataTable, ByVal dsresult As DataSet)
        ' basic checking
        If dt Is Nothing Then
            Return
        End If
        If dsresult Is Nothing Then
            Return
        End If
        If dsresult.Tables.Count = 0 Then
            Return
        End If

        ' prepare variables
        Dim dr As DataRow
        Dim drs As DataRow()
        Dim policyAccountNo As String
        Dim productName As String
        Dim notFound As Boolean = False
        Dim lngErr As Long = 0
        Dim strErr As String = String.Empty
        Dim dsPolicyList As DataSet = Nothing

        ' find and assign product name from dt to dsresult
        For Each dr In dsresult.Tables(0).Rows
            policyAccountNo = Convert.ToString(dr("PolicyAccountNo"))
            drs = dt.Select(String.Format("PolicyAccountNo = '{0}'", policyAccountNo.Replace("'", "''")))
            If drs.Length = 0 Then
                notFound = True
            Else
                dr("ProductName") = Convert.ToString(drs(0)("ProductName"))
            End If
        Next

        ' find and assign product name from policies of the current customer.
        If notFound Then
            Try
                dsPolicyList = GetPolicyList_Asur_WorkAround(Me.CustomerID, "policy", False, lngErr, strErr)
                If Not String.IsNullOrWhiteSpace(strErr) Then
                    LogInformation("Error", "GetPolicyList_Asur", "MapPolicyInfoToLogs", strErr, Nothing)
                Else
                    For Each dr In dsresult.Tables(0).Rows
                        policyAccountNo = Convert.ToString(dr("PolicyAccountNo"))
                        productName = Convert.ToString(dr("ProductName"))

                        If String.IsNullOrWhiteSpace(productName) Then
                            drs = dsPolicyList.Tables(0).Select(String.Format("Policy = '{0}'", policyAccountNo.Replace("'", "''")))
                            If drs.Length = 0 Then
                                notFound = True
                            Else
                                dr("ProductName") = Convert.ToString(drs(0)("Product"))
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                LogInformation("Error", "GetPolicyList_Asur", "MapPolicyInfoToLogs", ex.Message, ex)
            Finally
                If Not dsPolicyList Is Nothing Then
                    dsPolicyList.Dispose()
                End If
            End Try
        End If
    End Sub
#End Region


End Class
