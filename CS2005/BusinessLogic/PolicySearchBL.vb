Imports System.Text
Imports System.Threading

Public Class PolicySearchBL

    Private objMQQueHeader As New Utility.Utility.MQHeader
    Private objDBHeader As New Utility.Utility.ComHeader
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private CompanyID As String = "ING"
    Private SysEventLog As New SysEventLog.clsEventLog

    Private _formReadySignal As AutoResetEvent = Nothing    ' auto event signal gun for form reloading
    Private _frmPolicyAsur As frmPolicyAsur = Nothing       ' policy summary form (Assurance Supported)
    Private _frmPolicyMcu As frmPolicyMcu = Nothing         ' policy summary form for MCU


    ''' <summary>
    ''' Get Policy on Click Open
    ''' </summary>
    Sub GetPolicy(ByVal strPolicyNo As String, ByRef strErr As String, ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal strComp As String = "")

        If String.IsNullOrEmpty(Trim(strPolicyNo)) Then
            Exit Sub
        End If
        Dim PolicySearchResult As New DataTable
        GetHeaderByCompanyID()

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader
        'ITSR933 FG R5 Policy Number Change Holy Hu Start
        'ITSR933 FG R3 Policy Number Change Start
        Dim strErr1 As String = ""
        Dim strLifeAsiaPolicy As String = ""
        'Dim strPolicyStatus As String = ""
        Dim strOldPolicy As String = ""
        strOldPolicy = strPolicyNo
        If clsPOS.IsCapsilDisconnected(strErr1) Then
            If clsPOS.GetLifeAsiaPolicyNo(strLifeAsiaPolicy, strPolicyNo, strErr1) Then
                strLifeAsiaPolicy = strLifeAsiaPolicy.Trim
                If strLifeAsiaPolicy <> "" AndAlso strLifeAsiaPolicy.Trim <> strPolicyNo.Trim Then
                    strPolicyNo = strLifeAsiaPolicy
                    MsgBox("Please note that Capsil policy " & strOldPolicy & " is converted to Life Asia policy " & strLifeAsiaPolicy)
                End If
            End If
        End If
        'ITSR933 FG R5 Policy Number Change Holy Hu End

        Dim SingleQuoteFormatted = String.Format("'{0}'", Trim(strPolicyNo))
        Dim sqldtM As New DataTable
        Dim lngErrM As Long = 0
        Dim lngCntM As Long = gSearchLimit

        Dim sqldt As New DataTable
        Dim lngErr As Long = 0
        Dim lngCnt As Long = gSearchLimit


        Dim sqldtAsur As New DataTable
        Dim lngErrAsur As Long = 0
        Dim lngCntAsur = gSearchLimit

        If strComp = "LAC" Or strComp = "" Then
            ' Handle for get Asur policy mapping list
            If GetAsurPolicyMappingListByAPI(g_LacComp, SingleQuoteFormatted, lngErrM, strErr, lngCntM, sqldtM) Then
                If lngErrM <> 0 Then
                    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If

        If sqldtM.Rows.Count > 0 Then
            ' New Policy number found for asur policy mapping
            Dim searchList As New List(Of String)
            For i As Integer = 0 To sqldtM.Rows.Count - 1
                searchList.Add(String.Format("'{0}'", Trim(sqldtM.Rows(i)("cswpm_la_policy").ToString())))
            Next
            ' add original input policy
            searchList.Add(SingleQuoteFormatted)

            For Each itm As String In searchList
                ' Bermuda case
                Dim tempSqldt As New DataTable
                Dim tempLngErr As Long = 0
                Dim tempLngCnt As Long = gSearchLimit
                If GetPolicyListByAPI(getCompanyCode(g_Comp), itm, "", "PH", "POLST", "=", tempLngErr, strErr, tempLngCnt, tempSqldt, True) Then
                    If tempLngErr <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

                If Not tempSqldt Is Nothing Then
                    sqldt.Merge(tempSqldt, False)
                End If

                ' Assurance case
                Dim tempSqldtAsur As New DataTable
                Dim tempLngErrAsur As Long = 0
                Dim tempLngCntAsur = gSearchLimit
                If GetPolicyListByAPI(g_LacComp, itm, "", "PH", "POLST", "=", tempLngErrAsur, strErr, tempLngCntAsur, tempSqldtAsur, True) Then
                    If tempLngErrAsur <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

                If Not tempSqldtAsur Is Nothing Then
                    sqldtAsur.Merge(tempSqldtAsur, False)
                End If
            Next
        Else
            ' No any New Policy number found for asur policy mapping
            If strComp = "ING" Or strComp = "BMU" Or strComp = "" Then
                'sqldt = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErr, strerr, lngCnt, isGetCountOnly, "ING")
                If GetPolicyListByAPI(getCompanyCode(g_Comp), SingleQuoteFormatted, "", "PH", "POLST", "=", lngErr, strErr, lngCnt, sqldt, True) Then
                    If lngErr <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If
            End If

            If strComp = "LAC" Or strComp = "" Then
                Dim strErrAsur = ""
                'sqldtAsur = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErrAsur, strErrAsur, lngCntAsur, isGetCountOnly, "LAC")
                If GetPolicyListByAPI(g_LacComp, SingleQuoteFormatted, "", "PH", "POLST", "=", lngErrAsur, strErr, lngCntAsur, sqldtAsur, True) Then
                    If lngErrAsur <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If
            End If
        End If

        'Dim sqldtMcu As New DataTable
        'Dim lngCntMcu = gSearchLimit
        'Dim lngErrMcu As Long = 0
        'Dim strErrMcu = ""
        'sqldtMcu = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErr, strErrMcu, lngCntMcu, isGetCountOnly, "MCU")
        'If Not sqldtMcu Is Nothing And Not sqldt Is Nothing Then sqldt.Merge(sqldtMcu, False)
        'If Not sqldtAsur Is Nothing And Not sqldt Is Nothing Then sqldt.Merge(sqldtAsur, False)
        'Count Record

        If Not sqldtAsur Is Nothing Then
            PolicySearchResult.Merge(sqldtAsur, False)
        End If

        If Not sqldt Is Nothing Then
            PolicySearchResult.Merge(sqldt, False)
        End If

        Dim TotalRecordCount As Long = PolicySearchResult.Rows.Count

        If TotalRecordCount = 0 Then
            MsgBox("No Policy Found.")
            strErr = ""
            Dim policyDs As New DataSet
            Dim policyDt As New DataTable
            SearchGIPolicy(strPolicyNo, strErr, policyDs, policyDt)
            If policyDt.Rows.Count = 0 Then
                Exit Sub
            End If
        ElseIf TotalRecordCount = 1 Then
            If PolicySearchResult.Rows(0)("CompanyID") <> "" Then
                CompanyID = Trim(PolicySearchResult.Rows(0)("CompanyID").ToString)
                strCompany = Trim(PolicySearchResult.Rows(0)("CompanyID").ToString)
            End If

            'Alex TH Lee 20240917
            'check VVIP
            If Not "Y".Equals(GetUatXml("SKCV")) Then
                Dim strFuncStartTime As String = Now
                Dim strFuncEndTime As String = Now
                Dim isUHNWPolicy As Boolean = False
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(CompanyID, "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                {"PolicyNo", PolicySearchResult.Rows(0)("PolicyAccountId").ToString.Trim}
                })
                strFuncEndTime = Now
                If Not retDs.Tables Is Nothing Then
                    If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                        isUHNWPolicy = True
                    End If
                    If Not "Y".Equals(GetUatXml("SKLV")) Then
                        SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, PolicySearchResult.Rows(0)("PolicyAccountId").ToString.Trim, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
                    End If
                End If

                Dim isUHNWMemberFlag As Boolean = False
                isUHNWMemberFlag = isUHNWMember
                If Not "Y".Equals(GetUatXml("SKLV")) Then
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, PolicySearchResult.Rows(0)("PolicyAccountId").ToString.Trim, "isUHNWMember", IIf(isUHNWMemberFlag, "1", "0"))
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmCS2005", "cmdOpen_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, PolicySearchResult.Rows(0)("PolicyAccountId").ToString.Trim, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))
                End If
                If isUHNWMemberFlag And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                ElseIf Not isUHNWMemberFlag And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                    Exit Sub
                End If
            End If

            ' check HNW(Private) (if need), 20250304 - HNW Expansion
            If CompanyID = "BMU" AndAlso Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                Exit Sub
            End If

            'oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'ShowPolicy(strPolicyNo, strerr, sender, e, CompanyID)
            strErr = ""
            ShowPolicyAsur(PolicySearchResult.Rows(0)("PolicyAccountId").ToString, CompanyID)
            Exit Sub
        ElseIf TotalRecordCount > 1 Then
            Dim frmSearchPolicyAsur As New frmSearchPolicyAsur
            frmSearchPolicyAsur.sboxPolicy.TextBoxSearchInput1.Text = strPolicyNo
            If Not OpenWindow(frmSearchPolicyAsur, wndMain) Then
                frmSearchPolicyAsur.Dispose()
            End If
            Call frmSearchPolicyAsur.cmdSearch_Click(New Object, New EventArgs, "=")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' Get policy on click open (For MCU)
    ''' </summary>
    Sub GetPolicyMcu(ByRef strPolicyNo As String)
        If String.IsNullOrEmpty(Trim(strPolicyNo)) Then
            Exit Sub
        End If

        CompanyID = "MCU"
        GetHeaderByCompanyID()

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader

        Dim strErr As String = String.Empty
        Dim strLifeAsiaPolicy As String = String.Empty

        Try
            Dim MacauServiceLogBL As New ServiceLogBL

            MacauServiceLogBL.GetLifeAsiaPolicyNoMCU(strLifeAsiaPolicy, strPolicyNo, strErr)
            If strLifeAsiaPolicy <> "" Then
                MessageBox.Show("Capsil Policy " & strPolicyNo & " update to " & strLifeAsiaPolicy)
                strPolicyNo = strLifeAsiaPolicy.Trim
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If clsPOS.IsCapsilDisconnected(strErr) Then
            If clsPOS.GetLifeAsiaPolicyNo(strLifeAsiaPolicy, strPolicyNo, strErr) Then
                strLifeAsiaPolicy = strLifeAsiaPolicy.Trim
                If strLifeAsiaPolicy <> "" AndAlso strLifeAsiaPolicy.Trim <> strPolicyNo Then
                    MsgBox("Please note that Capsil policy " & strPolicyNo & " is converted to Life Asia policy " & strLifeAsiaPolicy)
                    strPolicyNo = strLifeAsiaPolicy
                End If
            End If
        End If

        ShowPolicyMcu(strPolicyNo.Trim, CompanyID)
    End Sub

    Function SearchGIPolicy(ByVal strPolicyNo As String, ByRef strerr As String, ByRef searchPolicyDs As DataSet, ByRef searchPolicyDt As DataTable) As Boolean

        Try
            Dim companyList As Array = System.Enum.GetNames(GetType(Company))
            For Each companyItem As String In companyList
                Dim lngErr As Long = 0
                Dim lngCnt As Long = gSearchLimit
                Dim SingleQuoteFormatted = String.Format("'{0}%'", strPolicyNo)
                Dim isGetCountOnly As Boolean = False
                'oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
                'Dim sqldt As DataTable = GetGIPolicyList("", SingleQuoteFormatted, "PH", "POLST", lngErr, strerr, lngCnt, isGetCountOnly, companyItem)
                Dim sqldt As DataTable = GetGIPolicyListAsur("", SingleQuoteFormatted, "PH", "POLST", lngErr, strerr, lngCnt, isGetCountOnly, companyItem)
                If lngCnt > gSearchLimit Then
                    MsgBox("Over " & gSearchLimit & " records returned, please re-define your criteria.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    Return False
                End If
                sqldt.TableName = companyItem
                If Not sqldt Is Nothing Then
                    If sqldt.Rows.Count > 0 Then
                        searchPolicyDs.Tables.Add(sqldt)
                        searchPolicyDt.Merge(sqldt)
                    End If
                End If
            Next

            If searchPolicyDs.Tables.Count = 0 Then
                MsgBox("GIPolicy Not found")
            End If

            If searchPolicyDs.Tables.Count > 1 Then
                MsgBox("GIPolicy appear in two company")
                Return False
            End If


        Catch ex As Exception
            strerr = ex.ToString
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Show policy summary screen (Assurance Supported)
    ''' </summary>
    ''' <remarks>
    ''' <br>20231215 Oliver, Updated for Switch Over Code from Assurance to Bermuda</br>
    ''' <br>20241113 Chrysan Cheng, CRS performer slowness</br>
    ''' </remarks>
    Public Sub ShowPolicyAsur(strPolicyNo As String, Optional companyID As String = "ING", Optional blnIsUHNWCustomer As Boolean = False,
                              Optional strCustomerID As String = "", Optional strRelateCode As String = "")

        Dim funcStartTime As Date = Now

        strCompany = companyID
        Me.CompanyID = companyID
        GetHeaderByCompanyID()

        Try
            ' if policy form already open, no need to reload again
            If Not ExistWindow(wndMain, RTrim(strPolicyNo)) Then
                ' since the form initialization takes a long time, it will be initialized after asynchronously loading data
                _formReadySignal = New AutoResetEvent(False)

                ' get NB system info data first
                Call CheckUpdateNbSystemTable(objDBHeader, dsComponentSysTable, gSysTableLastUpdate) 'ITSR933 FG R3 Performance Tuning
                'w.Ctrl_ChgComponent1.SysTableInUse = dsComponentSysTable   ' CRS performer slowness - Will be set in tabCoverageDetails.HandleCreated
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyAsur", "CheckUpdateNbSystemTable", funcStartTime, Now, strPolicyNo, "", "")

                ' async to load and handle policy data
                HandleLifeAsiaPolicyAsync(RTrim(strPolicyNo))   'Life/Asia policy

                funcStartTime = Now
                ' init form object
                _frmPolicyAsur = New frmPolicyAsur With {
                    .IsAsyncLoadForm = True,    ' CRS performer slowness - Async load Form data
                    .CompanyID = companyID,
                    .objDBHeader = objDBHeader,
                    .objMQQueHeader = objMQQueHeader,
                    .PolicyAccountID = RTrim(strPolicyNo),
                    .Text = "Policy " & RTrim(strPolicyNo),
                    .blnIsUHNWCustomer = blnIsUHNWCustomer,
                    .CustomerID = If(blnIsUHNWCustomer, strCustomerID, .CustomerID),
                    .strRelateCode = If(blnIsUHNWCustomer, strRelateCode, .strRelateCode)
                }
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyAsur", "New frmPolicyAsur", funcStartTime, Now, strPolicyNo, "", "")
            End If

            ' show form / focus on old form
            If ShowWindow(_frmPolicyAsur, wndMain, RTrim(strPolicyNo)) Then
                If _frmPolicyAsur?.NoRecord Then
                    ' do nothing
                End If
            End If
        Finally
            ' set the form is ready singal
            _formReadySignal?.Set()
            SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyAsur", "", funcStartTime, Now, strPolicyNo, "", "")
        End Try
    End Sub

    ''' <summary>
    ''' Show policy summary screen for MCU
    ''' </summary>
    ''' <remarks>
    ''' <br>20241113 Chrysan Cheng, CRS performer slowness</br>
    ''' </remarks>
    Public Sub ShowPolicyMcu(strPolicyNo As String, Optional companyID As String = "MCU", Optional blnIsUHNWCustomer As Boolean = False,
                             Optional strCustomerID As String = "", Optional strRelateCode As String = "")

        Dim funcStartTime As Date = Now

        strCompany = companyID
        Me.CompanyID = companyID
        GetHeaderByCompanyID()

        Try
            ' if policy form already open, no need to reload again
            If Not ExistWindow(wndMain, RTrim(strPolicyNo)) Then
                ' since the form initialization takes a long time, it will be initialized after asynchronously loading data
                _formReadySignal = New AutoResetEvent(False)

                ' get NB system info data first
                Call CheckUpdateNbSystemTable(objDBHeader, dsComponentSysTable, gSysTableLastUpdate) 'ITSR933 FG R3 Performance Tuning
                'w.Ctrl_ChgComponent1.SysTableInUse = dsComponentSysTable   ' CRS performer slowness - Will be set in tabCoverageDetails.HandleCreated
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyMcu", "CheckUpdateNbSystemTable", funcStartTime, Now, strPolicyNo, "", "")

                ' async to load and handle policy data
                HandleLifeAsiaPolicyMcuAsync(RTrim(strPolicyNo))    'Life/Asia policy

                funcStartTime = Now
                ' init form object
                _frmPolicyMcu = New frmPolicyMcu With {
                    .IsAsyncLoadForm = True,    ' CRS performer slowness - Async load Form data
                    .sCompanyID = companyID,    ' Especially for MCU
                    .objDBHeader = objDBHeader,
                    .objMQQueHeader = objMQQueHeader,
                    .PolicyAccountID = RTrim(strPolicyNo),
                    .Text = "Policy " & RTrim(strPolicyNo),
                    .blnIsUHNWCustomer = blnIsUHNWCustomer,
                    .CustomerID = If(blnIsUHNWCustomer, strCustomerID, .CustomerID),
                    .strRelateCode = If(blnIsUHNWCustomer, strRelateCode, .strRelateCode)
                }
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyMcu", "New frmPolicyAsur", funcStartTime, Now, strPolicyNo, "", "")
            End If

            ' show form / focus on old form
            If ShowWindow(_frmPolicyMcu, wndMain, RTrim(strPolicyNo)) Then
                If _frmPolicyMcu?.NoRecord Then
                    ' do nothing
                End If
            End If
        Finally
            ' set the form is ready singal
            _formReadySignal?.Set()
            SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "ShowPolicyMcu", "", funcStartTime, Now, strPolicyNo, "", "")
        End Try
    End Sub

    Private Sub HandleLifeAsiaPolicyAsync(strPolicyNo As String)
        Dim funcStartTime As Date = Now

        ' async get policy summary page related data
        Dim psBL As New PolicySummaryAsyncBL(objDBHeader, objMQQueHeader)

        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyAsync", "PolicySummaryBL.GetWholeDataDictionaryAsync.Start", funcStartTime, Now, strPolicyNo, "", "")
        psBL.GetWholeDataDictionaryAsync(strPolicyNo,
            Sub(t, dataDict)
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyAsync", "PolicySummaryBL.GetWholeDataDictionaryAsync.End", funcStartTime, Now, strPolicyNo, "", "")

                ' get policy header data from dataDict (if have)
                Dim dsPolicyHeader As New DataSet
                Dim dsPolicyData As DataSet = Nothing
                If dataDict?.TryGetValue("dsPolicyData", dsPolicyData) Then
                    If dsPolicyData.Tables.Contains("PolicyHeader") Then
                        dsPolicyHeader.Tables.Add(dsPolicyData.Tables("PolicyHeader").Copy)
                    End If
                End If

                ' wait until the form is ready
                _formReadySignal?.WaitOne()
                ' got the signal, dispose first
                _formReadySignal?.Dispose()

                ' no need reload if Form non-Ready/closed
                If _frmPolicyAsur Is Nothing OrElse _frmPolicyAsur.Disposing OrElse _frmPolicyAsur.IsDisposed OrElse Not _frmPolicyAsur.IsHandleCreated Then Return

                ' set data and reload screen
                _frmPolicyAsur.Invoke(
                    Sub()
                        Try
                            Dim isLifeAsia As Boolean = True
                            Dim isProposal As Boolean = False

                            ' only PolicyHeader fail will be considered as error (Because other data can be fetching again later in synchronous mode)
                            If t.IsFaulted AndAlso (dsPolicyHeader.Tables.Count = 0 OrElse dsPolicyHeader.Tables(0).Rows.Count = 0) Then
                                ' failed, handle error
                                Dim sb As New StringBuilder
                                Dim aggEx As AggregateException = t.Exception.InnerException
                                For Each ex As Exception In aggEx.InnerExceptions
                                    sb.AppendLine(ex.Message)
                                Next
                                HandlePolicyError(strPolicyNo, sb.ToString, isLifeAsia, isProposal)
                            Else
                                ' succeed, init screen
                                InitPolicyForm(_frmPolicyAsur, strPolicyNo, dsPolicyHeader)

                                If t.IsFaulted Then
                                    ' other data fail, and will be fetching again later in synchronous mode, notify user
                                    MsgBox("Data acquisition exception, loading may be slow.", MsgBoxStyle.Exclamation)
                                End If
                            End If

                            _frmPolicyAsur.isLifeAsia = isLifeAsia
                            _frmPolicyAsur.isProposal = isProposal
                        Finally
                            ' finally, reload screen anyway
                            _frmPolicyAsur.ReloadScreen(dsPolicyHeader, dataDict)

                            If _frmPolicyAsur.isProposal Then
                                ' proposal policy should focus on NB form
                                If objNBA IsNot Nothing AndAlso Not objNBA.IsDisposed AndAlso objNBA.IsHandleCreated AndAlso objNBA.Visible Then
                                    objNBA.Focus()
                                End If
                            End If
                        End Try
                    End Sub
                )
            End Sub
        )

        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyAsync", "", funcStartTime, Now, strPolicyNo, "", "")
    End Sub

    Private Sub HandleLifeAsiaPolicyMcuAsync(strPolicyNo As String)
        Dim funcStartTime As Date = Now

        ' async get policy summary page related data
        Dim psBL As New PolicySummaryAsyncBL(objDBHeader, objMQQueHeader)

        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyMcuAsync", "PolicySummaryBL.GetWholeDataDictionaryAsync.Start", funcStartTime, Now, strPolicyNo, "", "")
        psBL.GetWholeDataDictionaryAsync(strPolicyNo,
            Sub(t, dataDict)
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyMcuAsync", "PolicySummaryBL.GetWholeDataDictionaryAsync.End", funcStartTime, Now, strPolicyNo, "", "")

                ' get policy header data from dataDict (if have)
                Dim dsPolicyHeader As New DataSet
                Dim dsPolicyData As DataSet = Nothing
                If dataDict?.TryGetValue("dsPolicyData", dsPolicyData) Then
                    If dsPolicyData.Tables.Contains("PolicyHeader") Then
                        dsPolicyHeader.Tables.Add(dsPolicyData.Tables("PolicyHeader").Copy)
                    End If
                End If

                ' wait until the form is ready
                _formReadySignal?.WaitOne()
                ' got the signal, dispose first
                _formReadySignal?.Dispose()

                ' no need reload if Form non-Ready/closed
                If _frmPolicyMcu Is Nothing OrElse _frmPolicyMcu.Disposing OrElse _frmPolicyMcu.IsDisposed OrElse Not _frmPolicyMcu.IsHandleCreated Then Return

                ' set data and reload screen
                _frmPolicyMcu.Invoke(
                    Sub()
                        Try
                            Dim isLifeAsia As Boolean = True
                            Dim isProposal As Boolean = False

                            ' only PolicyHeader fail will be considered as error (Because other data can be fetching again later in synchronous mode)
                            If t.IsFaulted AndAlso (dsPolicyHeader.Tables.Count = 0 OrElse dsPolicyHeader.Tables(0).Rows.Count = 0) Then
                                ' failed, handle error
                                Dim sb As New StringBuilder
                                Dim aggEx As AggregateException = t.Exception.InnerException
                                For Each ex As Exception In aggEx.InnerExceptions
                                    sb.AppendLine(ex.Message)
                                Next
                                HandlePolicyError(strPolicyNo, sb.ToString, isLifeAsia, isProposal)
                            Else
                                ' succeed, init screen
                                InitPolicyFormMcu(_frmPolicyMcu, strPolicyNo, dsPolicyHeader)

                                If t.IsFaulted Then
                                    ' other data fail, and will be fetching again later in synchronous mode, notify user
                                    MsgBox("Data acquisition exception, loading may be slow.", MsgBoxStyle.Exclamation)
                                End If
                            End If

                            _frmPolicyMcu.isLifeAsia = isLifeAsia
                            _frmPolicyMcu.isProposal = isProposal
                        Finally
                            ' finally, reload screen anyway
                            _frmPolicyMcu.ReloadScreen(dsPolicyHeader, dataDict)

                            If _frmPolicyMcu.isProposal Then
                                ' proposal policy should focus on NB form
                                If objNBA IsNot Nothing AndAlso Not objNBA.IsDisposed AndAlso objNBA.IsHandleCreated AndAlso objNBA.Visible Then
                                    objNBA.Focus()
                                End If
                            End If
                        End Try
                    End Sub
                )
            End Sub
        )

        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySearchBL", "HandleLifeAsiaPolicyMcuAsync", "", funcStartTime, Now, strPolicyNo, "", "")
    End Sub

    <Obsolete("Deprecated by CRS performer slowness - Capsil policy should be already delink")>
    Private Sub HandleCapsilPolicy(strPolicyNo As String, strErr As String, w As frmPolicyAsur,
                                   ByRef dsPolicyCurr As DataSet, ByRef isLifeAsia As Boolean, ByRef isProposal As Boolean)
        isLifeAsia = False
        isProposal = False

        Dim dsPolicySend As New DataSet
        Dim dtSendData As New DataTable
        dtSendData.Columns.Add("PolicyNo")
        dtSendData.Rows.Add(RTrim(strPolicyNo))
        dsPolicySend.Tables.Add(dtSendData)

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader

        If clsPOS.GetCapsilPolicy(dsPolicySend, dsPolicyCurr, Nothing, strErr) Then
            If dsPolicyCurr.Tables.Count > 0 AndAlso dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                InitPolicyForm(w, strPolicyNo, dsPolicyCurr)
            End If
        Else
            HandlePolicyError(strPolicyNo, strErr, isLifeAsia, isProposal)
        End If
    End Sub

    Private Sub HandlePolicyError(strPolicyNo As String, strErr As String, ByRef isLifeAsia As Boolean, ByRef isProposal As Boolean)
        If strErr.Contains("Contract not on file") Then
            isLifeAsia = False
        ElseIf strErr.Contains("Policy not in force") Then
            MsgBox("Policy not inforce. ", MsgBoxStyle.Information)
            isProposal = True

            ' show NB form
            ShowNewBusinessAdmin(strPolicyNo, strErr)
        Else
            isLifeAsia = False
            MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If
    End Sub

    Private Sub ShowNewBusinessAdmin(strPolicyNo As String, ByRef strErr As String)
        ' Check if policy can be found from NBA, if no, it is CNB policy
        Dim isCNB As Boolean = If(CompanyID = "MCU", GetNBRPolicyMcu(strPolicyNo, strErr), GetNBRPolicy(strPolicyNo, strErr))
        If isCNB Then
            Dim objCNB As New frmNewBizAdmin
            objCNB.PolicyNo(objDBHeader.CompanyID, objDBHeader.EnvironmentUse, objDBHeader.UserID) = strPolicyNo.Trim
            objCNB.Show()
            objCNB.Focus()
        Else
            If objNBA Is Nothing OrElse Not objNBA.IsHandleCreated Then
                ' init and show NBA form
                InitializeNewBusinessAdmin(strErr)
            End If

            Dim eKey As New KeyPressEventArgs(Chr(13))
            objNBA.txtPolicyNo.Text = RTrim(strPolicyNo)
            objNBA.txtPolicyNo_KeyPress(Nothing, eKey)
            objNBA.Visible = True
            objNBA.Focus()
        End If
    End Sub

    Private Sub InitializeNewBusinessAdmin(ByRef strErr As String)
        Dim dtUserAuthority As New DataTable

        Dim objCI As New LifeClientInterfaceComponent.CommonControl With {
            .ComHeader = objDBHeader
        }
        objCI.GetUserAuthority(dtUserAuthority, strErr)

        objNBA = New NewBusinessAdmin.NBLifeAdmin With {
            .ComHeaderInUse = objDBHeader,
            .dtAuthorityInUse = dtUserAuthority,
            .Text = $"New Business Administration ({objDBHeader.CompanyID}) Main Menu ({objDBHeader.EnvironmentUse})"
        }

        objNBA.Show()
    End Sub

    Private Sub InitPolicyForm(ByRef w As frmPolicyAsur, strPolicyNo As String, dsPolicyCurr As DataSet)

        w.txtCName.Text = ""
        w.txtCNameChi.Text = ""
        w.txtTitle.Text = ""
        w.txtLastName.Text = ""
        w.txtFirstName.Text = ""
        w.txtChiName.Text = ""
        w.txtPolicy.Text = ""
        w.txtStatus.Text = ""
        w.txtProduct.Text = ""

        w.txtPolicy.Text = RTrim(strPolicyNo)
        w.txtStatus.Text = dsPolicyCurr.Tables(0).Rows(0)("Risk_Sts")
        'w.txtProduct.Text = dsPolicyCurr.Tables(0).Rows(0)("Code")
        w.txtProduct.Text = dsPolicyCurr.Tables(0).Rows(0)("Desc")
    End Sub

    Private Sub InitPolicyFormMcu(ByRef w As frmPolicyMcu, strPolicyNo As String, dsPolicyCurr As DataSet)

        w.txtCName.Text = ""
        w.txtCNameChi.Text = ""
        w.txtTitle.Text = ""
        w.txtLastName.Text = ""
        w.txtFirstName.Text = ""
        w.txtChiName.Text = ""
        w.txtPolicy.Text = ""
        w.txtStatus.Text = ""
        w.txtProduct.Text = ""

        w.txtPolicy.Text = RTrim(strPolicyNo)
        w.txtStatus.Text = dsPolicyCurr.Tables(0).Rows(0)("Risk_Sts")
        'w.txtProduct.Text = dsPolicyCurr.Tables(0).Rows(0)("Code")
        w.txtProduct.Text = dsPolicyCurr.Tables(0).Rows(0)("Desc")
    End Sub

    Public Sub GetHeaderByCompanyID()
        Select Case CompanyID
            Case "ING"
                objMQQueHeader = gobjMQQueHeader
                objDBHeader = gobjDBHeader
            Case "BMU"
                objMQQueHeader = gobjBmuMQQueHeader
                objDBHeader = gobjBmuDBHeader
            Case "MCU"
                objMQQueHeader = gobjMcuMQQueHeader
                objDBHeader = gobjMcuDBHeader
            Case "LAC"
                objMQQueHeader = gobjLacMQQueHeader
                objDBHeader = gobjLacDBHeader
            Case "LAH"
                objMQQueHeader = gobjLahMQQueHeader
                objDBHeader = gobjLahDBHeader
        End Select
    End Sub

End Class
