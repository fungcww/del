Imports System.IO
Imports System.Text
Imports System
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Runtime.Remoting.MetadataServices
Imports POSMain
Imports POSMain.CMWS

Public Class ServiceLogBL

    Dim sqlConn As New SqlConnection
    Dim MacauServiceBL As New MacauServiceBL
    Private objCRS As New CRS.CRS
    Private Const POLICY_MAIN_TYPE_BERMUDA = "Bermuda"
    Private Const POLICY_MAIN_TYPE_GI = "GI"
    Private Const POLICY_MAIN_TYPE_EB = "EB"
    Private Const POLICY_MAIN_TYPE_MACAU = "Macau"
    Private Const POLICY_MAIN_TYPE_ASSURANCE = "Assurance"
    Private Const POLICY_MAIN_TYPE_PRIVATE = "BMU"
#Region "Service Log Option"

    'oliver 2024-6-21 commented for Table_Relocate_Sprint13
    'Public Sub GetEventCategory(ByRef dtEvtCat As DataTable, ByRef strErr As String)
    '    sqlConn.ConnectionString = strCIWConn
    '    Dim strSQL As String = "select cswecc_code, cswecc_desc as [EventCat] from csw_event_category_code order by cswecc_desc"
    '    Try
    '        Dim daEvtCat As New SqlDataAdapter(strSQL, sqlConn)
    '        sqlConn.Open()
    '        daEvtCat.Fill(dtEvtCat)
    '        If (dtEvtCat Is Nothing) Then
    '            Throw New Exception("Cannot get Event Category List!")
    '        End If
    '    Catch ex As Exception
    '        strErr = ex.Message
    '    Finally
    '        sqlConn.Close()
    '    End Try
    'End Sub

    'oliver 2024-6-21 commented for Table_Relocate_Sprint13
    Public Sub GetEventCategory(ByRef dtEvtCat As DataTable, ByRef strErr As String)
        Dim ds As DataSet = New DataSet()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_EVENT_CATEGORY",
                        New Dictionary(Of String, String) From {})

            If ds.Tables.Count > 0 Then
                dtEvtCat = ds.Tables(0).Copy
            End If

        Catch ex As Exception
            strErr = ex.Message
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try

    End Sub

    Public Sub GetEventDetail(ByVal strEventCatCode As String, ByVal isMCU As Boolean, ByRef dtEvtDetail As DataTable, ByRef strErr As String)
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = "select EventTypeCode, EventTypeDesc as [Issue_Type] from ServiceEventTypeCodes "
        strSQL += "where EventCategoryCode='" + strEventCatCode + "'"
        If isMCU Then 'Hide Option such as VHIS in Macau Service Log
            strSQL += "and (HideInMCU<>'Y' or HideInMCU is null) "
        End If
        strSQL += " order by SortOrder"
        Dim i As Integer
        Try
            Dim daEvtDetail As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daEvtDetail.Fill(dtEvtDetail)
            If (dtEvtDetail Is Nothing) Then
                Throw New Exception("Cannot get Event Detail List!")
            End If

        Catch ex As Exception
            strErr = ex.Message
        Finally
            sqlConn.Close()
        End Try

    End Sub

    Public Sub GetEventTypeDetail(ByVal strEvtCatCode As String, ByVal strEvtDetailCode As String, ByRef dtEvtDetailType As DataTable, ByRef strErr As String)
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = "select csw_event_typedtl_code, csw_event_typedtl_desc as [Event_Description] from " & gcPOS & "vw_csw_event_typedtl_code "
        strSQL += "where csw_event_category_code='" + strEvtCatCode + "'"
        strSQL += " and csw_event_type_code='" + strEvtDetailCode + "'"
        strSQL += " and (Obsoleted<>'Y' or Obsoleted is null) "
        strSQL += " order by cswetd_sort_order"
        Try
            Dim daEvtTypeDetail As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daEvtTypeDetail.Fill(dtEvtDetailType)
            If (dtEvtDetailType Is Nothing) Then
                Throw New Exception("Cannot get Event Type Detail List!")
            End If
        Catch ex As Exception
            strErr = ex.Message
        Finally
            sqlConn.Close()
        End Try
    End Sub

    Public Sub GetMedium(ByRef dtMedium As DataTable, ByRef strErr As String)
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = "select EventSourceMediumCode, EventSourceMedium as [Medium]  from EventSourceMediumCodes group by EventSourceMediumCode,EventSourceMedium order by EventSourceMedium"
        Try
            Dim daMedium As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daMedium.Fill(dtMedium)
            If (dtMedium Is Nothing) Then
                Throw New Exception("Cannot get Medium!")
            End If
        Catch ex As Exception
            strErr = ex.Message
        Finally
            sqlConn.Close()
        End Try
    End Sub

    Public Sub GetInitiator(ByRef dtInitiator As DataTable, ByRef strErr As String)
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = "select EventSourceInitiatorcode, EventSourceInitiator as [initiator] from EventSourceInitiatorCodes group by EventSourceInitiatorCode,EventSourceInitiator order by EventSourceInitiator"
        Try
            Dim daInitator As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daInitator.Fill(dtInitiator)
            sqlConn.Close()
            If (dtInitiator Is Nothing) Then
                Throw New Exception("Cannot get Initator!")
            End If
        Catch ex As Exception
            strErr = ex.Message
        Finally
            sqlConn.Close()
        End Try
    End Sub

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Public Sub GetEventStatusList(ByRef dtEventStatusList As DataTable, ByVal blnVHISComplaint As Boolean, ByRef strErr As String)
        Dim ds As DataSet = New DataSet()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_EVENT_STATUS_CODE",
                        New Dictionary(Of String, String) From {
                        {"blnVHISComplaint", If(blnVHISComplaint, 1, 0)}
                        })

            If ds.Tables.Count > 0 Then
                dtEventStatusList = ds.Tables(0).Copy
            End If

        Catch ex As Exception
            strErr = ex.Message
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try

    End Sub

    'oliver 2024-5-3 commented for Table_Relocate_Sprint13
    'Public Sub GetEventStatusList(ByRef dtEventStatusList As DataTable, ByVal blnVHISComplaint As Boolean, ByRef strErr As String)
    '    sqlConn.ConnectionString = strCIWConn
    '    Dim strSQL As String = "select EventStatusCode, EventStatus as [Status] from EventStatusCodes "
    '    If blnVHISComplaint Then
    '        strSQL += "where EventStatusCode Not in ('C','H', 'P') "
    '    Else
    '        strSQL += "where EventStatusCode in ('C','H', 'P') "
    '    End If
    '    strSQL += "group by EventStatusCode,EventStatus order by EventStatusCode "
    '    Try
    '        Dim daEventStatusList As New SqlDataAdapter(strSQL, sqlConn)
    '        sqlConn.Open()
    '        daEventStatusList.Fill(dtEventStatusList)
    '        sqlConn.Close()
    '        If (dtEventStatusList Is Nothing) Then
    '            Throw New Exception("Cannot get Event Status List")
    '        End If
    '    Catch ex As Exception
    '        strErr = ex.Message
    '    Finally
    '        sqlConn.Close()
    '    End Try
    'End Sub

    Public Sub GetCsrList(ByRef dtCsr As DataTable, ByRef strErr As String)
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = "(Select '' as 'CSRID', '' as 'Name', null as 'CSRTypeCode', null as 'Description', "
        strSQL += "null as 'ProductsSpecializedIn', null as 'LicensesHeld', "
        strSQL += "null as 'LicenseEffectiveDate', null as 'CSRUnitCode', null as 'SupervisorsCSRID', null as 'timestamp', null as 'Cname', null as 'csrid_400', null as 'Active' "
        strSQL += "From " & serverPrefix & "CSR) union (Select * From " & serverPrefix & "CSR) order by Name"
        Try
            Dim daCsr As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daCsr.Fill(dtCsr)
            sqlConn.Close()
            If (dtCsr Is Nothing) Then
                Throw New Exception("Cannot get CSR list")
            End If
        Catch ex As Exception
            strErr = ex.Message
        Finally
            sqlConn.Close()
        End Try
    End Sub

#End Region

#Region "Service Log Record"
    Public Function Get_New_ServiceEventNumber(ByRef strSerEvtNumber As String, ByRef strErr As String) As Boolean
        Dim dtserevtnum As New DataTable
        Dim strSQLserevtnum As String = "Select MAX(ServiceEventNumber)+1 as ServiceEventNumber from ServiceEventDetail "
        strErr = String.Empty
        Try
            Dim daserevtnum As New SqlDataAdapter(strSQLserevtnum, sqlConn)
            sqlConn.Open()
            daserevtnum.Fill(dtserevtnum)
            strSerEvtNumber = dtserevtnum.Rows(0)("ServiceEventNumber")
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function Get_HK_non_cust_ServiceLog(ByVal strYear As String, ByVal strMonth As String, ByVal strUser As String, ByVal strStatus As String, ByVal strGivenMobile As String, ByVal strGivenEmail As String, ByRef dsResult As DataSet, ByRef strErr As String) As Boolean

        If strUser.Trim.ToUpper() = "<ALL>" Then
            strUser = ""
        End If

        If strStatus = String.Empty Then
            strStatus = ""
        End If

        If strYear.Trim = "" Or strYear.Trim = "0000" Then
            strYear = -1
        Else
            strYear = strYear.Trim
        End If

        If strMonth.Trim = "" Then
            strMonth = -1
        Else
            strMonth = strMonth.Trim
        End If

        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        sqlConn.ConnectionString = strCIWConn
        Dim dtSerLog As New DataTable
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        strSQL = " select a.ServiceEventNumber,ISNULL(a.EventCategoryCode,'') as EventCategoryCode, ISNULL(a.EventTypeCode,'') as EventTypeCode,ISNULL(CustomerID,0) as CustomerID, "
        strSQL += " ISNULL(MasterCSRID,'') as MasterCSRID,ISNULL(EventInitialDateTime,'1900/01/01') as EventInitialDateTime, "
        strSQL += " ISNULL(EventCompletionDateTime,'1900/01/01') as EventCompletionDateTime,(case when ISNULL(EventCloseoutCode,'') = 'Y' then 'Yes' else 'No' end)  as EventCloseoutCode, "
        strSQL += " ISNULL(EventCloseoutDateTime,'1900/01/01') as EventCloseoutDateTime,ISNULL(EventCloseoutCSRID,'') as EventCloseoutCSRID, "
        strSQL += " ISNULL(a.EventStatusCode,'') as EventStatusCode,ISNULL(a.EventSourceInitiatorCode,'') as EventSourceInitiatorCode,ISNULL(a.EventSourceMediumCode,'') as EventSourceMediumCode, "
        strSQL += " ISNULL(EventNotes,'') as EventNotes,ISNULL(ReminderNotes,'') as ReminderNotes,ISNULL(EventTypeDetailCode,'') as EventTypeDetailCode, "
        strSQL += " ISNULL(h.EventSourceInitiator, '') as EventSourceInitiator, ISNULL(i.EventSourceMedium,'') as EventSourceMedium, "
        strSQL += " CAST('' as varchar(5)) as LocationCode,ISNULL(b.Name,'') as InitiatorName, "
        strSQL += " ISNULL(cswecc_desc,'') as EventCategoryDesc, "
        strSQL += " ISNULL(EventTypeDesc,'') as EventTypeDesc,ISNULL(csw_event_typedtl_desc,'') as EventTypeDetailDesc,ISNULL(g.EventStatus ,'') as EventStatus "
        strSQL += " ,(case when ISNULL(MCV,'') = 'Y' then 'Yes' else 'No' end)  as MCV "
        strSQL += " ,c.Call_id , c.Custname as [GreetingName], c.Phonemobile,c.EmailAddr "
        strSQL += " from ServiceEventDetail a "
        strSQL += " inner join " & serverPrefix & "ServiceEventDetailHK_Extend c on c.ServiceEventNumber=a.ServiceEventNumber "
        strSQL += " Left Join " & serverPrefix & "csr b on b.CSRID = a.MasterCSRID "
        strSQL += " Left Join " & gcPOS & "vw_csw_event_category_code d on a.EventCategoryCode = d.cswecc_code "
        strSQL += " Left Join ServiceEventTypeCodes e on a.EventTypeCode = e.EventTypeCode and d.cswecc_code = e.EventCategoryCode "
        strSQL += " Left Join " & gcPOS & "vw_csw_event_typedtl_code f on d.cswecc_code = f.csw_event_category_code and e.EventTypeCode = f.csw_event_type_code and a.EventTypeDetailCode = f.csw_event_typedtl_code "
        strSQL += " Left Join " & serverPrefix & "EventStatusCodes g on a.EventStatusCode = g.EventStatusCode  "
        strSQL += " Left Join EventSourceInitiatorCodes h on a.EventSourceInitiatorCode=h.EventSourceInitiatorCode "
        strSQL += " Left Join EventSourceMediumCodes i on a.EventSourceMediumCode=i.EventSourceMediumCode "
        strSQL += " Where (-1 = '" + strYear + "' or YEAR(EventInitialDateTime) = '" + strYear + "')"
        strSQL += " and (-1 = '" + strMonth + "' or Month(EventInitialDateTime) = '" + strMonth + "' ) "
        strSQL += " and ('" + strUser + "' = '' or MasterCSRID = '" + strUser + "')"
        strSQL += " and ( '' = '" + strStatus + "' or a.EventStatusCode ='" + strStatus + "' ) "
        If strGivenMobile <> "" Then strSQL += " and c.PhoneMobile like '%" + strGivenMobile + "%' "
        If strGivenEmail <> "" Then strSQL += " and c.EmailAddr like'%" + strGivenEmail + "%' "

        strSQL += " order by EventInitialDateTime desc "
        Try
            Dim daSerlog As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daSerlog.Fill(dtSerLog)
            dsResult.Tables.Add(dtSerLog)
            If (dsResult Is Nothing) Then
                strErr = "Cannot get Service Log Detail!"
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            strErr = ex.Message
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function Insert_non_cust_ServiceLog(ByVal dsResult As DataSet, ByRef strServiceEventNumber As String, ByRef strErr As String) As Boolean

        sqlConn.ConnectionString = strCIWConn
        Dim strSQL_1 As String = String.Empty
        Dim strSQL_2 As String = String.Empty
        strErr = String.Empty

        'Get ServiceEventNo
        If Not Get_New_ServiceEventNumber(strServiceEventNumber, strErr) Then
            MessageBox.Show(strErr) : Exit Function
        End If
        'C001 - Start
        strSQL_1 = " Insert Into ServiceEventDetail (ServiceEventNumber,EventCategoryCode,EventTypeCode,EventTypeActionCode,CustomerID,PolicyAccountID, "
        strSQL_1 += " MasterCSRID,SecondaryCSRID,EventInitialDateTime,EventAssignDateTime,EventCompletionDateTime,EventCloseoutCode,EventCloseoutDateTime, "
        strSQL_1 += " EventCloseoutCSRID,EventStatusCode,EventSourceInitiatorCode,EventSourceMediumCode,EventNotes,ReminderDate,ReminderNotes,EventTypeDetailCode, "
        strSQL_1 += " agentcode,divisioncode,unitcode,caseno,createdatetime,updateuser,updatedatetime,BirthdayAlert,IsTransferToAES,IsAppearedInAES,IsPolicyAlert,AlertNotes,MCV) "
        strSQL_1 += " values ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}', "
        strSQL_1 += " '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}',{25},'{26}',{27},'{28}','{29}','{30}','{31}','{32}','{33}')"
        strSQL_1 = String.Format(strSQL_1, strServiceEventNumber,
                                 dsResult.Tables(0).Rows(0)("EventCategoryCode"),
                                 dsResult.Tables(0).Rows(0)("EventTypeCode"),
                                 dsResult.Tables(0).Rows(0)("EventTypeActionCode"),
                                 dsResult.Tables(0).Rows(0)("CustomerID"),
                                 dsResult.Tables(0).Rows(0)("PolicyAccountID"),
                                 dsResult.Tables(0).Rows(0)("MasterCSRID"),
                                 dsResult.Tables(0).Rows(0)("SecondaryCSRID"),
                                 dsResult.Tables(0).Rows(0)("EventInitialDateTime"),
                                 dsResult.Tables(0).Rows(0)("EventAssignDateTime"),
                                 dsResult.Tables(0).Rows(0)("EventCompletionDateTime"),
                                 dsResult.Tables(0).Rows(0)("EventCloseoutCode"),
                                 dsResult.Tables(0).Rows(0)("EventCloseoutDateTime"),
                                 dsResult.Tables(0).Rows(0)("EventCloseoutCSRID"),
                                 dsResult.Tables(0).Rows(0)("EventStatusCode"),
                                 dsResult.Tables(0).Rows(0)("EventSourceInitiatorCode"),
                                 dsResult.Tables(0).Rows(0)("EventSourceMediumCode"),
                                 dsResult.Tables(0).Rows(0)("EventNotes"),
                                 dsResult.Tables(0).Rows(0)("ReminderDate"),
                                 dsResult.Tables(0).Rows(0)("ReminderNotes"),
                                 dsResult.Tables(0).Rows(0)("EventTypeDetailCode"),
                                 dsResult.Tables(0).Rows(0)("agentcode"),
                                 dsResult.Tables(0).Rows(0)("divisioncode"),
                                 dsResult.Tables(0).Rows(0)("unitcode"),
                                 dsResult.Tables(0).Rows(0)("caseno"),
                                 "GETDATE()",
                                 dsResult.Tables(0).Rows(0)("updateuser"),
                                 "GETDATE()",
                                 dsResult.Tables(0).Rows(0)("BirthdayAlert"),
                                 dsResult.Tables(0).Rows(0)("IsTransferToAES"),
                                 dsResult.Tables(0).Rows(0)("IsAppearedInAES"),
                                 dsResult.Tables(0).Rows(0)("IsPolicyAlert"),
                                 dsResult.Tables(0).Rows(0)("AlertNotes"),
                                 dsResult.Tables(0).Rows(0)("MCV"))
        'oliver 2024-5-3 added for Table_Relocate_Sprint13
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL_2 = "Insert Into " & serverPrefix & "ServiceEventDetailHK_Extend (ServiceEventNumber,Custname,Call_id,PhoneMobile,EmailAddr ) values('{0}','{1}','{2}','{3}','{4}')"
        strSQL_2 = String.Format(strSQL_2, strServiceEventNumber,
        dsResult.Tables(0).Rows(0)("Custname"),
        dsResult.Tables(0).Rows(0)("Call_id"),
        dsResult.Tables(0).Rows(0)("PhoneMobile"),
        dsResult.Tables(0).Rows(0)("EmailAddr"))

        Try
            Dim sqlCommand_1 As New SqlCommand(strSQL_1, sqlConn)
            Dim sqlCommand_2 As New SqlCommand(strSQL_2, sqlConn)
            sqlConn.Open()
            sqlCommand_1.ExecuteNonQuery()
            sqlCommand_2.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function Update_non_cust_ServiceLog(ByVal dsResult As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL_1 As String = String.Empty
        Dim strSQL_2 As String = String.Empty
        strErr = String.Empty
        strSQL_1 = " Update ServiceEventDetail set EventCategoryCode = '{2}', EventTypeCode= '{3}', EventTypeActionCode= '{4}', EventCompletionDateTime= '{5}',EventCloseoutCSRID = '{6}', "
        strSQL_1 += " EventStatusCode= '{7}', EventSourceInitiatorCode= '{8}', EventSourceMediumCode= '{9}', EventNotes= '{10}', ReminderNotes= '{11}',EventTypeDetailCode= '{12}',updateuser= '{13}', updatedatetime = GETDATE(), "
        strSQL_1 += " EventInitialDateTime = '{14}',EventCloseoutDateTime = '{15}',EventCloseoutCode = '{16}',MCV= '{17}' "
        strSQL_1 += " Where ServiceEventNumber = '{0}'"
        strSQL_1 = String.Format(strSQL_1, dsResult.Tables(0).Rows(0)("ServiceEventNumber"),
                                        dsResult.Tables(0).Rows(0)("PolicyAccountID"),
                                        dsResult.Tables(0).Rows(0)("EventCategoryCode"),
                                        dsResult.Tables(0).Rows(0)("EventTypeCode"),
                                        dsResult.Tables(0).Rows(0)("EventTypeActionCode"),
                                        dsResult.Tables(0).Rows(0)("EventCompletionDateTime"),
                                        dsResult.Tables(0).Rows(0)("EventCloseoutCSRID"),
                                        dsResult.Tables(0).Rows(0)("EventStatusCode"),
                                        dsResult.Tables(0).Rows(0)("EventSourceInitiatorCode"),
                                        dsResult.Tables(0).Rows(0)("EventSourceMediumCode"),
                                        dsResult.Tables(0).Rows(0)("EventNotes"),
                                        dsResult.Tables(0).Rows(0)("ReminderNotes"),
                                        dsResult.Tables(0).Rows(0)("EventTypeDetailCode"),
                                        dsResult.Tables(0).Rows(0)("updateuser"),
                                        dsResult.Tables(0).Rows(0)("EventInitialDateTime"),
                                        dsResult.Tables(0).Rows(0)("EventCloseoutDateTime"),
                                        dsResult.Tables(0).Rows(0)("EventCloseoutCode"),
                                        dsResult.Tables(0).Rows(0)("MCV"))
        'oliver 2024-5-3 added for Table_Relocate_Sprint13
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL_2 = " Update " & serverPrefix & "ServiceEventDetailHK_Extend set Custname='{1}',Call_id='{2}',PhoneMobile='{3}',EmailAddr='{4}' where ServiceEventNumber='{0}'"
        strSQL_2 = String.Format(strSQL_2, dsResult.Tables(0).Rows(0)("ServiceEventNumber"),
                dsResult.Tables(0).Rows(0)("Custname"),
                dsResult.Tables(0).Rows(0)("Call_id"),
                dsResult.Tables(0).Rows(0)("PhoneMobile"),
                dsResult.Tables(0).Rows(0)("EmailAddr"))
        Try
            Dim sqlCommand_1 As New SqlCommand(strSQL_1, sqlConn)
            'oliver 2024-05-24 fix bug for Table_Relocate_Sprint13
            Dim sqlCommand_2 As New SqlCommand(strSQL_2, sqlConn)
            sqlConn.Open()
            sqlCommand_1.ExecuteNonQuery()
            sqlCommand_2.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try

    End Function
#End Region

#Region "Macau Service Log Record"

    'ITSR933 FR R3 Policy Number Change Start
    Public Function Get_MCU_ServiceLog(ByVal strYear As Integer, ByVal strMonth As Integer, ByVal strUser As String, ByVal strPolicy As String, ByVal strStatus As String, ByVal strNeedFollowUp As String, ByRef dsResult As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        'oliver updated 2024-6-27 for Assurance Production Version hot fix
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Dim dtSerLog As New DataTable
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        strSQL = " select a.ServiceEventNumber,ISNULL(a.EventCategoryCode,'') as EventCategoryCode, ISNULL(a.EventTypeCode,'') as EventTypeCode,ISNULL(CustomerID,0) as CustomerID, " &
                     " 	ISNULL(a.PolicyAccountID,'') as PolicyAccountID,ISNULL(MasterCSRID,'') as MasterCSRID,ISNULL(EventInitialDateTime,'1900/01/01') as EventInitialDateTime, " &
                     "  ISNULL(EventCompletionDateTime,'1900/01/01') as EventCompletionDateTime,(case when ISNULL(EventCloseoutCode,'') = 'Y' then 'Yes' else 'No' end)  as EventCloseoutCode, " &
                     "  ISNULL(EventCloseoutDateTime,'1900/01/01') as EventCloseoutDateTime,ISNULL(EventCloseoutCSRID,'') as EventCloseoutCSRID, " &
                     "  ISNULL(a.EventStatusCode,'') as EventStatusCode,ISNULL(EventSourceInitiatorCode,'') as EventSourceInitiatorCode,ISNULL(EventSourceMediumCode,'') as EventSourceMediumCode, " &
                     "  ISNULL(EventNotes,'') as EventNotes,ISNULL(ReminderNotes,'') as ReminderNotes,ISNULL(EventTypeDetailCode,'') as EventTypeDetailCode, " &
                     "  ISNULL(AgentCode,'') as AgentCode,ISNULL(DivisionCode,'') as DivisionCode,ISNULL(UnitCode,'') as UnitCode, " &
                     "  CAST('' as varchar(21)) as ProductID,CAST('' as varchar(25)) as NameSuffix,CAST('' as varchar(25)) as FirstName, " &
                     "  CAST('' as varchar(1)) as AccountStatusCode,CAST('' as varchar(5)) as LocationCode,ISNULL(b.Name,'') as InitiatorName, " &
                     "  (case when FollowUpByMacau = '1' then 'Yes' else 'No' end) as FollowUpByMacau,ISNULL(cswecc_desc,'') as EventCategoryDesc, " &
                     "  ISNULL(EventTypeDesc,'') as EventTypeDesc,ISNULL(csw_event_typedtl_desc,'') as EventTypeDetailDesc,ISNULL(g.EventStatus ,'') as EventStatus " &
                     "  ,(case when ISNULL(MCV,'') = 'Y' then 'Yes' else 'No' end)  as MCV " &
                     "  ,'' as PolicyAccountID2" &
                     " from " & serverPrefix & "ServiceEventDetailMCU_Extend  c ,ServiceEventDetail a " &
                     " Left Join " & serverPrefix & "csr b on b.CSRID = a.MasterCSRID " &
                     " Left Join " & gcPOS & "vw_csw_event_category_code d on a.EventCategoryCode = d.cswecc_code " &
                     " Left Join ServiceEventTypeCodes e on a.EventTypeCode = e.EventTypeCode and d.cswecc_code = e.EventCategoryCode " &
                     " Left Join " & gcPOS & "vw_csw_event_typedtl_code f on d.cswecc_code = f.csw_event_category_code and e.EventTypeCode = f.csw_event_type_code and a.EventTypeDetailCode = f.csw_event_typedtl_code " &
                     " Left Join " & serverPrefix & "EventStatusCodes g on a.EventStatusCode = g.EventStatusCode  " &
                     " Where a.ServiceEventNumber = c.ServiceEventNumber and a.PolicyAccountID = c.PolicyAccountID " &
                     " and (-1 = {0} or YEAR(EventInitialDateTime) = {0}) and (-1 = {1} or Month(EventInitialDateTime) = {1} ) and ('{2}' = '' or MasterCSRID = '{2}')  and ('' = '{3}' or a.PolicyAccountID = '{3}') " &
                     " and ( '' = '{4}' or a.EventStatusCode ='{4}' ) " &
                     " and ( '' = '{5}' or c.FollowUpByMacau ='{5}' ) " &
                     " order by updatedatetime asc "
        strSQL = String.Format(strSQL, strYear, strMonth, strUser, strPolicy, strStatus, strNeedFollowUp)
        Try
            Dim daSerlog As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daSerlog.Fill(dtSerLog)
            UpdateConversionPolicyDisplayMCU(dtSerLog)
            dsResult.Tables.Add(dtSerLog)
            If (dsResult Is Nothing) Then
                strErr = "Cannot get Service Log Detail!"
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function
    Public Sub UpdateConversionPolicyDisplayMCU(ByRef dtSerLog As DataTable)
        If dtSerLog IsNot Nothing AndAlso dtSerLog.Rows.Count > 0 AndAlso dtSerLog.Columns.Contains("PolicyAccountID") AndAlso dtSerLog.Columns.Contains("PolicyAccountID2") Then
            For Each row As DataRow In dtSerLog.Rows
                If row("PolicyAccountID") IsNot DBNull.Value AndAlso row("PolicyAccountID") <> "" Then
                    Dim strPolicy As String = row("PolicyAccountID")
                    Dim strPolicyDisplay As String = strPolicy
                    Dim strErr As String = ""

                    'oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
                    'Dim strCapsilPolicy As String = ""
                    'GetCapsilPolicyNoMCU(strPolicy, strCapsilPolicy, strErr)
                    'If strCapsilPolicy <> "" Then
                    '    strPolicyDisplay = strPolicy & " (" & strCapsilPolicy & ")"
                    'End If
                    If Not IsAssurance Then
                        Dim strCapsilPolicy As String = ""
                        GetCapsilPolicyNoMCU(strPolicy, strCapsilPolicy, strErr)
                        If strCapsilPolicy <> "" Then
                            strPolicyDisplay = strPolicy & " (" & strCapsilPolicy & ")"
                        End If
                    End If

                    Dim strLifeAsiaPolicy As String = ""
                    GetLifeAsiaPolicyNoMCU(strLifeAsiaPolicy, strPolicy, strErr)
                    If strLifeAsiaPolicy <> "" Then
                        strPolicyDisplay = strLifeAsiaPolicy & " (" & strPolicy & ")"
                    End If

                    row("PolicyAccountID2") = strPolicyDisplay
                End If
            Next
        End If
    End Sub
    'oliver 2023-12-12 uncommented for Switch Over Code from Assurance to Bermuda
    Public Function GetCapsilPolicyNoMCU(ByVal strLifeAsiaPolicy As String, ByRef strCapsilPolicy As String, ByRef strErr As String) As Boolean
        Dim sqlConnMCU As New SqlConnection
        sqlConnMCU.ConnectionString = MacauServiceBL.SetMCUCIWSQLConn() 'Serach Policy in MCU, use new connetcionstring
        Dim strSQL As String = String.Empty
        Dim dt As New DataTable

        strSQL = "select cswpm_capsil_policy "
        strSQL = strSQL & " from csw_policy_map "
        strSQL = strSQL & " where cswpm_la_policy = '" & Trim(strLifeAsiaPolicy) & "'"

        Try
            Dim da As New SqlDataAdapter(strSQL, sqlConnMCU)
            sqlConnMCU.Open()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                strCapsilPolicy = dt.DefaultView(0).Item(0).ToString.TrimEnd
            Else
                strCapsilPolicy = ""
            End If
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConnMCU.Close()
        End Try
    End Function
    Public Function GetLifeAsiaPolicyNoMCU(ByRef strLifeAsiaPolicy As String, ByVal strCapsilPolicy As String, ByRef strErr As String) As Boolean
        Dim sqlConnMCU As New SqlConnection
        sqlConnMCU.ConnectionString = MacauServiceBL.SetMCUCIWSQLConn() 'Serach Policy in MCU, use new connetcionstring
        Dim strSQL As String = String.Empty
        Dim dt As New DataTable

        strSQL = "select cswpm_la_policy "
        strSQL = strSQL & " from csw_policy_map "
        strSQL = strSQL & " where cswpm_capsil_policy  = '" & RTrim(strCapsilPolicy) & "'"

        Try
            Dim da As New SqlDataAdapter(strSQL, sqlConnMCU)
            sqlConnMCU.Open()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                strLifeAsiaPolicy = dt.DefaultView(0).Item(0).ToString.TrimEnd
            Else
                strLifeAsiaPolicy = ""
            End If
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConnMCU.Close()
        End Try
    End Function
    'ITSR933 FR R3 Policy Number Change End
    Public Function GetBasicPlan(ByRef dsBasicPlan As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = MacauServiceBL.SetMCUCIWSQLConn() 'Serach Policy in MCU, use new connetcionstring
        Dim strSQL As String = String.Empty
        Dim dtBasicPlan As New DataTable
        strSQL = " Select ProductID,[Description] as ProductName from PRODUCT group by ProductID,[Description] Order by ProductID "
        Try
            Dim daBasicPlan As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daBasicPlan.Fill(dtBasicPlan)
            dsBasicPlan.Tables.Add(dtBasicPlan)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetAccountStatus(ByRef dsAccountStatus As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = MacauServiceBL.SetMCUCIWSQLConn() 'Serach Policy in MCU, use new connetcionstring
        Dim strSQL As String = String.Empty
        Dim dtAccountStatus As New DataTable
        strSQL = " select AccountStatusCode,AccountStatus from AccountStatusCodes group by AccountStatusCode,AccountStatus order by AccountStatusCode "
        Try
            Dim daAccountStatus As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daAccountStatus.Fill(dtAccountStatus)
            dsAccountStatus.Tables.Add(dtAccountStatus)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetAgentLocation(ByRef dsAgentLocation As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = MacauServiceBL.SetMCUACMSQLConn() 'Serach Policy in MCU, use new connetcionstring
        Dim strSQL As String = String.Empty
        Dim dtAgentLocation As New DataTable
        strSQL = " select camglm_loc_code,camglm_loc_desc from cam_group_location_master group by camglm_loc_code,camglm_loc_desc order by camglm_loc_code "
        Try
            Dim daAgentLocation As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daAgentLocation.Fill(dtAgentLocation)
            dsAgentLocation.Tables.Add(dtAgentLocation)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function SearchPolicy_MCU(ByVal strPolicy As String, ByRef dsPolicy As DataSet, ByRef strErr As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim dtpolicy As New DataTable
        sqlConn.ConnectionString = MacauServiceBL.SetMCUCIWSQLConn() 'Serach Policy in MCU, use new connetcionstring
        strSQL = " Select b.CompanyID,a.PolicyRelateCode,a.PolicyAccountID,b.ProductID,a.CustomerID,RTRIM(LTRIM(c.NameSuffix)) NameSuffix ,RTRIM(LTRIM(c.FirstName)) as FirstName, " &
                 " c.AgentCode,d.LocationCode,b.AccountStatusCode,d.UnitCode from csw_poli_rel a " &
                 " Left Join PolicyAccount b on a.PolicyAccountID = b.PolicyAccountID " &
                 " Left Join Customer c on a.CustomerID  = c.CustomerID  " &
                 " Left Join AgentCodes d on c.AgentCode = d.AgentCode  " &
                 " where a.PolicyRelateCode ='SA'  and a.PolicyAccountID = '{0}' "
        strSQL = String.Format(strSQL, strPolicy)

        Try
            Dim dapolicy As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            dapolicy.Fill(dtpolicy)
            dsPolicy.Tables.Add(dtpolicy)
            If (dsPolicy Is Nothing) Then
                strErr = "Cannot get Policy from Macau Database!"
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function Save_MCU_ServiceLog(ByVal strMode As String, ByVal dsResult As DataSet, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim strServiceEventNumber As String = String.Empty
        Dim strSQL_1 As String = String.Empty
        Dim strSQL_2 As String = String.Empty
        If strMode.ToUpper = "A" Then 'Create new record
            Dim dtResult As New DataTable
            'Get ServiceEventNo
            If Not Get_New_ServiceEventNumber(strServiceEventNumber, strErr) Then
                MessageBox.Show(strErr) : Exit Function
            End If
            strSQL_1 = " Insert Into ServiceEventDetail (ServiceEventNumber,EventCategoryCode,EventTypeCode,EventTypeActionCode,CustomerID,PolicyAccountID, " &
                     "      MasterCSRID,SecondaryCSRID,EventInitialDateTime,EventAssignDateTime,EventCompletionDateTime,EventCloseoutCode,EventCloseoutDateTime, " &
                     "      EventCloseoutCSRID,EventStatusCode,EventSourceInitiatorCode,EventSourceMediumCode,EventNotes,ReminderDate,ReminderNotes,EventTypeDetailCode, " &
                     "      agentcode,divisioncode,unitcode,caseno,createdatetime,updateuser,updatedatetime,BirthdayAlert,IsTransferToAES,IsAppearedInAES,IsPolicyAlert,AlertNotes,MCV) " &
                     " values ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}', " &
                      "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}',{25},'{26}',{27},'{28}','{29}','{30}','{31}','{32}','{33}') "
            strSQL_1 = String.Format(strSQL_1, strServiceEventNumber,
                                     dsResult.Tables(0).Rows(0)("EventCategoryCode"),
                                     dsResult.Tables(0).Rows(0)("EventTypeCode"),
                                     dsResult.Tables(0).Rows(0)("EventTypeActionCode"),
                                     dsResult.Tables(0).Rows(0)("CustomerID"),
                                     dsResult.Tables(0).Rows(0)("PolicyAccountID"),
                                     dsResult.Tables(0).Rows(0)("MasterCSRID"),
                                     dsResult.Tables(0).Rows(0)("SecondaryCSRID"),
                                     dsResult.Tables(0).Rows(0)("EventInitialDateTime"),
                                     dsResult.Tables(0).Rows(0)("EventAssignDateTime"),
                                     dsResult.Tables(0).Rows(0)("EventCompletionDateTime"),
                                     dsResult.Tables(0).Rows(0)("EventCloseoutCode"),
                                     dsResult.Tables(0).Rows(0)("EventCloseoutDateTime"),
                                     dsResult.Tables(0).Rows(0)("EventCloseoutCSRID"),
                                     dsResult.Tables(0).Rows(0)("EventStatusCode"),
                                     dsResult.Tables(0).Rows(0)("EventSourceInitiatorCode"),
                                     dsResult.Tables(0).Rows(0)("EventSourceMediumCode"),
                                     dsResult.Tables(0).Rows(0)("EventNotes"),
                                     dsResult.Tables(0).Rows(0)("ReminderDate"),
                                     dsResult.Tables(0).Rows(0)("ReminderNotes"),
                                     dsResult.Tables(0).Rows(0)("EventTypeDetailCode"),
                                     dsResult.Tables(0).Rows(0)("agentcode"),
                                     dsResult.Tables(0).Rows(0)("divisioncode"),
                                     dsResult.Tables(0).Rows(0)("unitcode"),
                                     dsResult.Tables(0).Rows(0)("caseno"),
                                     "GETDATE()",
                                     dsResult.Tables(0).Rows(0)("updateuser"),
                                     "GETDATE()",
                                     dsResult.Tables(0).Rows(0)("BirthdayAlert"),
                                     dsResult.Tables(0).Rows(0)("IsTransferToAES"),
                                     dsResult.Tables(0).Rows(0)("IsAppearedInAES"),
                                     dsResult.Tables(0).Rows(0)("IsPolicyAlert"),
                                     dsResult.Tables(0).Rows(0)("AlertNotes"),
                                     dsResult.Tables(0).Rows(0)("MCV"))
            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
            strSQL_2 = "Insert Into " & serverPrefix & "ServiceEventDetailMCU_Extend (ServiceEventNumber,PolicyAccountID,FollowUpByMacau )values ('{0}','{1}',{2})"
            strSQL_2 = String.Format(strSQL_2, strServiceEventNumber, dsResult.Tables(0).Rows(0)("PolicyAccountID"), dsResult.Tables(0).Rows(0)("FollowUpByMacau"))
        Else 'Update Existing record 
            strSQL_1 = " Update ServiceEventDetail set EventCategoryCode = '{2}', EventTypeCode= '{3}', EventTypeActionCode= '{4}', EventCompletionDateTime= '{5}',EventCloseoutCSRID = '{6}', " &
                     "      EventStatusCode= '{7}', EventSourceInitiatorCode= '{8}', EventSourceMediumCode= '{9}', EventNotes= '{10}', ReminderNotes= '{11}',EventTypeDetailCode= '{12}',updateuser= '{13}', updatedatetime = GETDATE(), " &
                     "      EventInitialDateTime = '{14}',EventCloseoutDateTime = '{15}',EventCloseoutCode = '{16}',MCV= '{17}' " &
                     " Where ServiceEventNumber = '{0}' and PolicyAccountID = '{1}' "
            strSQL_1 = String.Format(strSQL_1, dsResult.Tables(0).Rows(0)("ServiceEventNumber"),
                                            dsResult.Tables(0).Rows(0)("PolicyAccountID"),
                                            dsResult.Tables(0).Rows(0)("EventCategoryCode"),
                                            dsResult.Tables(0).Rows(0)("EventTypeCode"),
                                            dsResult.Tables(0).Rows(0)("EventTypeActionCode"),
                                            dsResult.Tables(0).Rows(0)("EventCompletionDateTime"),
                                            dsResult.Tables(0).Rows(0)("EventCloseoutCSRID"),
                                            dsResult.Tables(0).Rows(0)("EventStatusCode"),
                                            dsResult.Tables(0).Rows(0)("EventSourceInitiatorCode"),
                                            dsResult.Tables(0).Rows(0)("EventSourceMediumCode"),
                                            dsResult.Tables(0).Rows(0)("EventNotes"),
                                            dsResult.Tables(0).Rows(0)("ReminderNotes"),
                                            dsResult.Tables(0).Rows(0)("EventTypeDetailCode"),
                                            dsResult.Tables(0).Rows(0)("updateuser"),
                                            dsResult.Tables(0).Rows(0)("EventInitialDateTime"),
                                            dsResult.Tables(0).Rows(0)("EventCloseoutDateTime"),
                                            dsResult.Tables(0).Rows(0)("EventCloseoutCode"),
                                            dsResult.Tables(0).Rows(0)("MCV"))
            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
            strSQL_2 = "Update " & serverPrefix & "ServiceEventDetailMCU_Extend set FollowUpByMacau = {2} where ServiceEventNumber = '{0}' and PolicyAccountID = '{1}'"
            strSQL_2 = String.Format(strSQL_2, dsResult.Tables(0).Rows(0)("ServiceEventNumber"), dsResult.Tables(0).Rows(0)("PolicyAccountID"), dsResult.Tables(0).Rows(0)("FollowUpByMacau"))
        End If

        Try
            sqlConn.Open()
            Dim sqlCommand_1 As New SqlCommand(strSQL_1, sqlConn)
            Dim sqlCommand_2 As New SqlCommand(strSQL_2, sqlConn)
            sqlCommand_1.ExecuteNonQuery()
            sqlCommand_2.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

#End Region

#Region "Retrive Service Log Record"

#Region "Use API"
    ''' <summary>
    ''' Get SingleSerLogsDataSet according to policyMainType by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-8-10 fixed the bug which is S4-CE-CRS- Service Log Retrieve miss the 'All' radio button</br>
    ''' </remarks>
    ''' <param name="policyMainType">Get SingleSerLogsDataSet according to policyMainType which include Bermuda,GI,EB,Macau,Assurance </param>
    ''' <param name="startDate">Represents the start date of get the SingleSerLogsDataSet</param>
    ''' <param name="endDate">Represents the end date of get the SingleSerLogsDataSet</param>
    ''' <param name="mediumCode">Represents the filter which is EventSourceMediumCode of get the SingleSerLogsDataSet</param>
    ''' <param name="evtCatCode">Represents the filter which is EventCategoryCode of get the SingleSerLogsDataSet</param>
    ''' <param name="custId">Represents the filter which is customerid of get the SingleSerLogsDataSet</param>
    ''' <param name="includeNonCust">Represents whether to include the customerid is empty of get the SingleSerLogsDataSet</param>
    ''' <param name="singleSerLogDataSet">Returns the singleSerLogDataSet by passing in the byref of the parameter</param>
    ''' <param name="strErr">Returns the strErr by passing in the byref of the parameter</param>
    ''' <returns>The return bool value represents whether the SingleSerLogsDataSet was got</returns>
    Public Function GetSingleSerLogDataSetByAPI(ByVal policyMainType As String, ByVal startDate As Date, ByVal endDate As Date, ByVal mediumCode As String, ByVal evtCatCode As String, ByVal custId As String, ByVal includeNonCust As Boolean, ByRef singleSerLogDataSet As DataSet, ByRef strErr As String, Optional ByVal isNBM As Boolean = False) As Boolean

        If IsDBNull(startDate) Then
            strErr = "Cannot search without StartDate."
            Return False
        End If

        Try

            Dim serLogDictonary = GetSerLogDictonary(policyMainType, startDate, endDate, mediumCode, evtCatCode, custId, includeNonCust)
            Dim companyName As String = ""
            Dim busiId As String = ""
            SetCompanyNameAndBusiId(policyMainType, companyName, busiId, isNBM)

            If companyName = String.Empty OrElse busiId = String.Empty Then
                strErr = "The policymaintype entered did not find the corresponding companyName and busiId."
                Return False
            End If

            singleSerLogDataSet = APIServiceBL.CallAPIBusi(companyName, busiId, serLogDictonary)
            Return True

        Catch ex As Exception
            strErr = ex.Message
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get allleSerLogsDataSet by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-8-10 fixed the bug which is S4-CE-CRS- Service Log Retrieve miss the 'All' radio button</br>
    ''' </remarks>
    ''' <param name="startDate">Represents the start date of get the allSerLogDataSet</param>
    ''' <param name="endDate">Represents the end date of get the allSerLogDataSet</param>
    ''' <param name="mediumCode">Represents the filter which is EventSourceMediumCode of get the allSerLogDataSet</param>
    ''' <param name="evtCatCode">Represents the filter which is EventCategoryCode of get the allSerLogDataSet</param>
    ''' <param name="custId">Represents the filter which is customerid of get the allSerLogDataSet</param>
    ''' <param name="includeNonCust">Represents whether to include the customerid is empty of get the allSerLogDataSet</param>
    ''' <param name="allSerLogDataSet">Returns the allSerLogDataSet by passing in the byref of the parameter</param>
    ''' <param name="strErr">Returns the strErr by passing in the byref of the parameter</param>
    ''' <returns>The return bool value represents whether the allSerLogDataSet was got</returns>
    Public Function GetAllSerLogDataSetByAPI(ByVal startDate As Date, ByVal endDate As Date, ByVal mediumCode As String, ByVal evtCatCode As String, ByVal custId As String, ByVal includeNonCust As Boolean, ByRef allSerLogDataSet As DataSet, ByRef strErr As String) As Boolean

        If IsDBNull(startDate) Then
            strErr = "Cannot search without StartDate."
            Return False
        End If

        Dim allSerLogDataTable As New DataTable()
        Dim allPolicyMainTypeList As String() = {POLICY_MAIN_TYPE_BERMUDA, POLICY_MAIN_TYPE_GI, POLICY_MAIN_TYPE_EB, POLICY_MAIN_TYPE_MACAU, POLICY_MAIN_TYPE_ASSURANCE, POLICY_MAIN_TYPE_PRIVATE}
        Dim i As Integer

        Try

            For i = 0 To allPolicyMainTypeList.Length - 1
                Dim serLogDictonary = GetSerLogDictonary(allPolicyMainTypeList(i), startDate, endDate, mediumCode, evtCatCode, custId, includeNonCust)

                Dim companyName As String = ""
                Dim busiId As String = ""
                SetCompanyNameAndBusiId(allPolicyMainTypeList(i), companyName, busiId)

                If companyName = String.Empty OrElse busiId = String.Empty Then
                    strErr = "The policymaintype entered did not find the corresponding companyName and busiId."
                    Return False
                End If

                Dim singleSerLogDataSet As DataSet = APIServiceBL.CallAPIBusi(companyName, busiId, serLogDictonary)
                If i = 0 Then
                    If Not IsNothing(singleSerLogDataSet) Then
                        allSerLogDataTable = singleSerLogDataSet.Tables(0)
                        For Each tcolumn As DataColumn In allSerLogDataTable.Columns
                            If tcolumn.DataType = GetType(String) Then
                                tcolumn.MaxLength = -1
                            End If
                        Next
                    End If
                    Continue For
                End If
                If Not IsNothing(singleSerLogDataSet) Then
                    allSerLogDataTable.Merge(singleSerLogDataSet.Tables(0))
                End If

            Next

            allSerLogDataSet.Tables.Add(allSerLogDataTable.Copy)
            Return True

        Catch ex As Exception
            strErr = ex.Message
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get SerLogDictionary as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-8-10 fied the bug which is S4-CE-CRS- Service Log Retrieve miss the 'All' radio button</br>
    ''' </remarks>
    ''' <param name="policyMainType">Get SerLogDictonary according to policyMainType which include Bermuda,GI,EB,Macau,Assurance </param>
    ''' <param name="startDate">Represents the start date of parameters in the SerLogDictionary</param>
    ''' <param name="endDate">Represents the end date of parameters in the SerLogDictionary</param>
    ''' <param name="mediumCode">Represents the filter which is EventSourceMediumCode of parameters in the SerLogDictionary</param>
    ''' <param name="evtCatCode">Represents the filter which is EventCategoryCode of parameters in the SerLogDictionary</param>
    ''' <param name="custId">Represents the filter which is customerid of parameters in the SerLogDictionary</param>
    ''' <param name="includeNonCust">Represents whether to include the customerid is empty of parameters in the SerLogDictionary</param>
    ''' <returns>The returned SerLogDictionary represents the parameters of the request SearchBusiAPI </returns>
    Private Function GetSerLogDictonary(ByVal policyMainType As String, ByVal startDate As Date, ByVal endDate As Date, ByVal mediumCode As String, ByVal evtCatCode As String, ByVal custId As String, ByVal includeNonCust As Boolean) As Dictionary(Of String, String)
        Dim serLogDictonary As New Dictionary(Of String, String)
        serLogDictonary.Add("PolicyMainType", policyMainType)
        serLogDictonary.Add("StartDate", IIf(Not IsDBNull(startDate), startDate.ToString("yyyy-MM-dd"), " "))
        serLogDictonary.Add("EndDate", IIf(Not IsDBNull(endDate), endDate.ToString("yyyy-MM-dd"), " "))
        serLogDictonary.Add("MediumCode", IIf(mediumCode <> String.Empty, mediumCode, " "))
        serLogDictonary.Add("EvtCatCode", IIf(evtCatCode <> String.Empty, evtCatCode, " "))

        'when includeNonCust is false,present IncludeNonCust = "0"
        'when includeNonCust is true,present IncludeNonCust = "1"
        If custId.Trim <> String.Empty Then
            serLogDictonary.Add("CustId", custId)
            serLogDictonary.Add("IncludeNonCust", "1")
        Else
            If Not includeNonCust Then
                serLogDictonary.Add("CustId", "0")
                serLogDictonary.Add("IncludeNonCust", IIf(includeNonCust, "1", "0"))
            Else
                serLogDictonary.Add("CustId", "0")
                serLogDictonary.Add("IncludeNonCust", IIf(includeNonCust, "1", "0"))
            End If
        End If

        Return serLogDictonary

    End Function

    ''' <summary>
    ''' Set CompanyName and BusiId as a parameter to request the SearchBusiAPI 
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-8-10 fixed the bug which is S4-CE-CRS- Service Log Retrieve miss the 'All' radio button</br>
    ''' </remarks>
    ''' <param name="policyMainType"></param>
    ''' <param name="companyName"></param>
    ''' <param name="busiId"></param>
    Private Sub SetCompanyNameAndBusiId(ByVal policyMainType As String, ByRef companyName As String, ByRef busiId As String, Optional ByVal isNBM As Boolean = False)
        If policyMainType = POLICY_MAIN_TYPE_BERMUDA OrElse policyMainType = POLICY_MAIN_TYPE_GI OrElse policyMainType = POLICY_MAIN_TYPE_EB Then
            companyName = "HK"
            If isNBM Then
                busiId = "GET_NBM_HK_SERLOG_BY_CRITERIA"
            Else
                busiId = "GET_HK_SERLOG_BY_CRITERIA"
            End If
        ElseIf policyMainType = POLICY_MAIN_TYPE_MACAU Then
            companyName = "HK"
            If isNBM Then
                busiId = "GET_NBM_MC_SERLOG_BY_CRITERIA"
            Else
                busiId = "GET_MC_SERLOG_BY_CRITERIA"
            End If
        ElseIf policyMainType = POLICY_MAIN_TYPE_ASSURANCE Then
            companyName = "LAC"
            busiId = "GET_ASUR_SERLOG_BY_CRITERIA"
        Else
            companyName = "HK"
            If isNBM Then
                busiId = "GET_NBM_HK_SERLOG_BY_CRITERIA"
            Else
                busiId = "GET_HK_SERLOG_BY_CRITERIA"
            End If
        End If
    End Sub

#End Region

#Region "Use local logic"

    'Public Function GetSerLogbycriteriaNew(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByRef dsResult As DataSet, ByRef strErr As String, ByVal type As String) As Boolean

    '    Dim strXML As String = ""

    '    If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
    '        strErr = "Cannot Search without Start Date or CustomerID."
    '        Return False
    '    End If


    '    Try
    '        Using wsCRS As New CRSWS.CRSWS()
    '            wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
    '            'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
    '            wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
    '            wsCRS.Timeout = 10000000


    '            'If Not wsCRS.GetDDAFORM(gsUser, g_Comp.Trim() & g_Env.Trim(), strPolicyNo, dt, strErr) Then
    '            '    Throw New Exception("Failed to get DDAForm record." & vbCrLf & strErr)
    '            'End If

    '            strXML = wsCRS.GetSerLogbyCriteria(getCompanyCode(), getEnvCode(), StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, strErr, type)

    '            If Not String.IsNullOrEmpty(strErr) Then
    '                Return False
    '            Else
    '                'dsResult.Tables.Add(dtSerLog)
    '                dsResult.ReadXml(New StringReader(strXML))
    '                If (dsResult Is Nothing) Then
    '                    strErr = "Cannot get Service Log Detail!"
    '                    Return False
    '                Else
    '                    If dsResult.Tables.Count = 0 Then
    '                        dsResult.Tables.Add(New DataTable())
    '                    End If
    '                    Return True
    '                End If
    '            End If

    '        End Using

    '    Catch ex As Exception
    '        strErr = ex.Message
    '        'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
    '        Return False
    '    Finally
    '        sqlConn.Close()
    '    End Try

    'End Function


    'Public Function GetSerLogbycriteria(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByRef dsResult As DataSet, ByRef strErr As String) As Boolean

    '    sqlConn.ConnectionString = strCIWConn
    '    Dim dtSerLog As New DataTable
    '    If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
    '        strErr = "Cannot Search without Start Date or CustomerID."
    '        Return False
    '    End If
    '    Dim strSQL As String = String.Empty
    '    strErr = String.Empty
    '    strSQL = "Select distinct t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator],t1.customerid, "
    '    strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
    '    strSQL += "PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end, "
    '    strSQL += "PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end, "
    '    strSQL += "ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end, "
    '    strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes, "
    '    strSQL += "t12.FirstName+' '+t12.NameSuffix as [Service Agent],t13.agentCode As [Agent Code], t13.LocationCode "
    '    strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV "
    '    strSQL += "From ServiceEventDetail t1 "
    '    strSQL += "left join PolicyAccount (nolock) t2 on t1.PolicyAccountID=t2.PolicyAccountID "
    '    strSQL += "left join PRODUCT (nolock) t3 on t2.ProductID=t3.ProductiD "
    '    strSQL += "left join GI_PRODUCT (nolock) t4 on t2.ProductID=t4.ProductiD "
    '    strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
    '    strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
    '    strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
    '    strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
    '    strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
    '    strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
    '    strSQL += "left join csw_poli_rel (nolock) t11 on t1.policyaccountid=t11.PolicyAccountID and t11.PolicyRelateCode='SA' "
    '    strSQL += "left join customer (nolock) t12 on t11.CustomerID=t12.CustomerID "
    '    strSQL += "left join AgentCodes (nolock) t13 on t13.agentcode=t12.AgentCode "
    '    strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
    '    strSQL += "where t1.EventSourceInitiatorCode<>'' "
    '    If Not IsDBNull(StartDate) Then
    '        strSQL += "and t1.EventInitialDateTime >='" + StartDate.ToString("yyyy-MM-dd") + "'"
    '    End If

    '    If Not IsDBNull(EndDate) Then
    '        strSQL += "and t1.EventInitialDateTime <='" + EndDate.ToString("yyyy-MM-dd") + "'"
    '    End If

    '    If strMediumCode <> String.Empty Then
    '        strSQL += "and t1.EventSourceMediumCode='" + strMediumCode + "' "
    '    End If

    '    If strEvtCatCode <> String.Empty Then
    '        strSQL += "and t1.EventCategoryCode='" + strEvtCatCode + "' "
    '    End If

    '    If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
    '        strSQL += "and t1.customerid='" + strcustId + "' "
    '    Else
    '        If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
    '            strSQL += "and t1.customerid<>'0' "
    '        End If
    '    End If

    '    strSQL += "Order by t1.EventInitialDateTime desc "

    '    Try
    '        Dim daSerlog As New SqlDataAdapter(strSQL, sqlConn)
    '        sqlConn.Open()
    '        daSerlog.Fill(dtSerLog)
    '        dsResult.Tables.Add(dtSerLog)
    '        If (dsResult Is Nothing) Then
    '            strErr = "Cannot get Service Log Detail!"
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        strErr = ex.Message
    '        'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
    '        Return False
    '    Finally
    '        sqlConn.Close()
    '    End Try

    'End Function


    Function ExportSerLogCsv(ByVal dsresultinput As DataSet) As Byte()

        Dim dtresultinput As DataTable = dsresultinput.Tables(0)
        Dim sb As StringBuilder = New StringBuilder()
        Dim intClmn As Integer = dtresultinput.Columns.Count

        Dim i As Integer = 0
        For i = 0 To intClmn - 1 Step i + 1
            sb.Append("""" + dtresultinput.Columns(i).ColumnName.ToString() + """")
            If i = intClmn - 1 Then
                sb.Append(" ")
            Else
                sb.Append(",")
            End If
        Next
        sb.Append(vbNewLine)

        Dim row As DataRow
        For Each row In dtresultinput.Rows

            Dim ir As Integer = 0
            For ir = 0 To intClmn - 1 Step ir + 1
                sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                If ir = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        Next

        Return System.Text.Encoding.UTF8.GetBytes(sb.ToString)

    End Function

#End Region
#End Region

#Region "Service Log User Preference"
    Public Sub GetSerlogPreference(ByVal cboMedium As ComboBox, ByVal cboInitiator As ComboBox) 'One function to config two combobox in service log
        Dim dtPreference As New DataTable
        Dim PreferMediumCode As String = String.Empty
        Dim PreferInitiatorCode As String = String.Empty
        If ReadLocalCRSConfig(False, dtPreference) Then
            If dtPreference.Rows.Count <> 0 Then
                PreferMediumCode = dtPreference.Rows(0)("Medium").ToString
                PreferInitiatorCode = dtPreference.Rows(0)("Initiator").ToString
            End If
        Else
            PreferMediumCode = "PC"
            PreferInitiatorCode = "CTR"
        End If
        cboMedium.SelectedValue = PreferMediumCode
        cboInitiator.SelectedValue = PreferInitiatorCode
    End Sub

    Public Function ReadLocalCRSConfig(ByVal Setup As Boolean, ByRef dtPreference As DataTable) As Boolean
        Dim path As String = ("C:\Users\" + gsUser + "\CRS_Preference.json")
        Dim PreferMedium As String = String.Empty
        Dim PreferInitiator As String = String.Empty
        Dim PreferenceJson As String
        dtPreference = New DataTable
        dtPreference.Columns.Add("Medium")
        dtPreference.Columns.Add("Initiator")
        dtPreference.Rows.Add()
        Try
            If Not File.Exists(path) Then
                Return False
            Else
                PreferenceJson = My.Computer.FileSystem.ReadAllText(path, System.Text.Encoding.UTF8)
                Dim JsonObj As Preference_JSON_result
                JsonObj = JsonConvert.DeserializeObject(Of Preference_JSON_result)(PreferenceJson)
                If JsonObj.Medium Is Nothing Then
                    dtPreference.Rows(0)("Medium") = "PC"
                Else
                    dtPreference.Rows(0)("Medium") = JsonObj.Medium
                End If
                If JsonObj.Initiator Is Nothing Then
                    dtPreference.Rows(0)("Initiator") = "CTR"
                Else
                    dtPreference.Rows(0)("Initiator") = JsonObj.Initiator
                End If
                dtPreference.AcceptChanges()
            End If
            Return True
        Catch ex As Exception
            If Setup Then MessageBox.Show("Cannot get user preference. Please update your preference.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) 'Setup=True when Customer is opening CRS Preference Win Form, otherwise is false and no error message
            dtPreference.Rows(0)("Medium") = "PC"
            dtPreference.Rows(0)("Initiator") = "CTR"
            dtPreference.AcceptChanges()
            Return False
        End Try
    End Function

    Public Function SetLocalCRSConfig(ByVal mediumCode As String, ByVal initiatorCode As String) As Boolean
        Dim path As String = ("C:\Users\" + gsUser + "\CRS_Preference.json")
        Dim PreferMedium As String = mediumCode
        Dim PreferInitiator As String = initiatorCode
        Dim PreferenceJson As String = "{medium:'" + PreferMedium + "',Initiator:'" + PreferInitiator + "'}"
        ' Create or overwrite the file.
        Try
            If Not File.Exists(path) Then
                Dim fs As FileStream = File.Create(path)
                ' Add text to the file.
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(PreferenceJson)
                fs.Write(info, 0, info.Length)
                fs.Close()
            Else
                Dim fs As FileStream
                File.Delete(path)
                fs = File.Create(path)
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(PreferenceJson)
                fs.Write(info, 0, info.Length)
                fs.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Retrieve Service Log for special purpose"
    Public Function GetSerLogbycriteriaNew(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByVal isDescOrder As Boolean, ByRef dsResult As DataSet, ByRef strErr As String, ByVal type As String) As Boolean
        Dim strXML As String = ""
        'Dim dtSerLog As New DataTable

        If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
            strErr = "Cannot Search without Start Date or CustomerID."
            Return False
        End If


        Try
            'Using wsCRS As New CRSWS.CRSWS()
            '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
            '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
            '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            '    End If
            '    'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
            '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            '    wsCRS.Timeout = 10000000


            '    'If Not wsCRS.GetDDAFORM(gsUser, g_Comp.Trim() & g_Env.Trim(), strPolicyNo, dt, strErr) Then
            '    '    Throw New Exception("Failed to get DDAForm record." & vbCrLf & strErr)
            '    'End If

            '    strXML = wsCRS.GetSerLogbyCusIdOrCriteria(getCompanyCode(), getEnvCode(), StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, isDescOrder, strErr, type)

            '    If Not String.IsNullOrEmpty(strErr) Then
            '        Return False
            '    Else
            '        dsResult.ReadXml(New StringReader(strXML))
            '        If (dsResult Is Nothing) Then
            '            strErr = "Cannot get Service Log Detail!"
            '            Return False
            '        Else
            '            Return True
            '        End If
            '    End If

            'End Using
            Dim strCondition = String.Empty
            If strcustId.Trim <> String.Empty Then
                strCondition += " and t1.customerid= '" & strcustId & "'"
            Else
                If Not IsDBNull(StartDate) Then
                    strCondition += " and t1.EventInitialDateTime >= '" & StartDate.ToString("yyyy-MM-dd") & "' "
                End If
                If Not IsDBNull(EndDate) Then
                    strCondition += " and t1.EventInitialDateTime <= '" & EndDate.ToString("yyyy-MM-dd") & "' "
                End If
                If strMediumCode <> String.Empty Then
                    strCondition += " and t1.EventSourceMediumCode = '" & strMediumCode & "' "
                End If
                If strEvtCatCode <> String.Empty Then
                    strCondition += " and t1.EventCategoryCode = '" & strEvtCatCode & "' "
                End If
                If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
                    strCondition += " and t1.customerid <> '0' "
                End If
            End If

            If Not String.IsNullOrEmpty(type) Then
                If type = "Bermuda" Or type = "Assurance" Or type = "Macau" Or type = "GI" Or type = "EB" Then
                    'For frmServiceLogRetreive
                    strCondition += "and (('" & type & "' = 'Macau' and mcu.ServiceEventNumber is not null) or ('" & type & "' in ('Bermuda', 'Assurance', 'GI', 'EB') and mcu.ServiceEventNumber is null)) "
                    strCondition += "and ( ('" & type & "' = 'Macau') "
                    strCondition += "or ('" & type & "' = 'Assurance' and isnull(t1.PolicyNo_Asur, '') <> '') "
                    strCondition += "or ('" & type & "' = 'EB' and t2.companyid in ('EB','GL','LTD')) "
                    strCondition += "or ('" & type & "' = 'GI' and t2.CompanyID = 'GI') "
                    strCondition += "or ('" & type & "' = 'Bermuda' and (t2.CompanyID is null or t2.CompanyID not in ('EB','GL','LTD', 'GI')) and isnull(t1.PolicyNo_Asur, '') = '')) "
                ElseIf type = "HK" Or type = "MC" Then
                    'For uclServiceLog & uclServiceLogMcu
                    strCondition += " and (('" & type & "' = 'MC' and mcu.ServiceEventNumber is not null) or ('" & type & "' = 'HK' and mcu.ServiceEventNumber is null)) "
                End If

            End If

            If isDescOrder Then
                strCondition += " Order by t1.EventInitialDateTime desc "
            Else
                strCondition += " Order by t1.EventInitialDateTime asc "
            End If

            Dim companycode As String = ""
            If type = "Assurance" Then
                companycode = "LAC"
            ElseIf type = "Macau" OrElse type = "MC" Then
                companycode = "MC"
            End If
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companycode), IIf(companycode = "LAC", "GET_SERVICELOG_BY_CUSTID_OR_CRITERIA_ASUR", "GET_SERVICELOG_BY_CUSTID_OR_CRITERIA"), New Dictionary(Of String, String)() From {
                    {"condition", strCondition}})

            If Not retDs.Tables Is Nothing Then
                If retDs.Tables(0).Rows.Count > 0 Then
                    Using ms As New IO.MemoryStream()
                        retDs.WriteXml(ms, System.Data.XmlWriteMode.WriteSchema)
                        strXML = System.Text.Encoding.UTF8.GetString(ms.ToArray)
                    End Using
                End If
            End If

            If String.IsNullOrEmpty(strXML) Then
                strErr = "Cannot get Service Log Detail!"
                Return False
            Else
                dsResult.ReadXml(New StringReader(strXML))
                If (dsResult Is Nothing) Then
                    strErr = "Cannot get Service Log Detail!"
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            strErr = ex.Message
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            Return False
        Finally
            sqlConn.Close()
        End Try

    End Function


    'Public Function GetSerLogbycriteria(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByVal isDescOrder As Boolean, ByRef dsResult As DataSet, ByRef strErr As String) As Boolean

    '    sqlConn.ConnectionString = strCIWConn

    '    Dim dtSerLog As New DataTable
    '    If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
    '        strErr = "Cannot Search without Start Date or CustomerID."
    '        Return False
    '    End If
    '    Dim strSQL As String = String.Empty
    '    strErr = String.Empty
    '    strSQL = "Select distinct t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator],t1.customerid, "
    '    strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
    '    strSQL += "PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end, "
    '    strSQL += "PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end, "
    '    strSQL += "ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end, "
    '    strSQL += "t15.Accountstatus as [Policy status], t2.RCD, "
    '    strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes,t1.alertnotes, "
    '    strSQL += "t12.FirstName+' '+t12.NameSuffix as [Service Agent],t13.agentCode As [Agent Code], t13.LocationCode, "
    '    strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV, "
    '    strSQL += "t1.caseno as [Suggestions/Grievances],t16.eStatement ,t1.ServiceEventNumber "
    '    strSQL += "From ServiceEventDetail t1 "
    '    strSQL += "left join PolicyAccount (nolock) t2 on t1.PolicyAccountID=t2.PolicyAccountID "
    '    strSQL += "left join PRODUCT (nolock) t3 on t2.ProductID=t3.ProductiD "
    '    strSQL += "left join GI_PRODUCT (nolock) t4 on t2.ProductID=t4.ProductiD "
    '    strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
    '    strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
    '    strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
    '    strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
    '    strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
    '    strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
    '    strSQL += "left join csw_poli_rel (nolock) t11 on t1.policyaccountid=t11.PolicyAccountID and t11.PolicyRelateCode='SA' "
    '    strSQL += "left join customer (nolock) t12 on t11.CustomerID=t12.CustomerID "
    '    strSQL += "left join AgentCodes (nolock) t13 on t13.agentcode=t12.AgentCode "
    '    strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
    '    strSQL += "left join AccountStatusCodes (nolock) t15 on t15.accountstatuscode=t2.AccountstatusCode "
    '    strSQL += "left join PolicyEstatement (nolock) t16 on t1.policyaccountid=t16.PolicyId "
    '    strSQL += "where t1.EventSourceInitiatorCode<>'' "
    '    If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
    '        strSQL += "and t1.customerid='" + strcustId + "' "
    '    Else
    '        If Not IsDBNull(StartDate) Then
    '            strSQL += "and t1.EventInitialDateTime >='" + StartDate.ToString("yyyy-MM-dd") + "'"
    '        End If
    '        If Not IsDBNull(EndDate) Then
    '            strSQL += "and t1.EventInitialDateTime <='" + EndDate.ToString("yyyy-MM-dd") + "'"
    '        End If
    '        If strMediumCode <> String.Empty Then
    '            strSQL += "and t1.EventSourceMediumCode='" + strMediumCode + "' "
    '        End If
    '        If strEvtCatCode <> String.Empty Then
    '            strSQL += "and t1.EventCategoryCode='" + strEvtCatCode + "' "
    '        End If
    '        If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
    '            strSQL += "and t1.customerid<>'0' "
    '        End If
    '    End If
    '    If isDescOrder Then
    '        strSQL += "Order by t1.EventInitialDateTime desc "
    '    Else
    '        strSQL += "Order by t1.EventInitialDateTime asc "
    '    End If

    '    Try
    '        Dim daSerlog As New SqlDataAdapter(strSQL, sqlConn)
    '        sqlConn.Open()
    '        daSerlog.Fill(dtSerLog)
    '        dsResult.Tables.Add(dtSerLog)
    '        If (dsResult Is Nothing) Then
    '            strErr = "Cannot get Service Log Detail!"
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        strErr = ex.Message
    '        'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
    '        Return False
    '    Finally
    '        sqlConn.Close()
    '    End Try

    'End Function

    ''' <summary>
    ''' Get service logs for Assurance.
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-13
    ''' Project: CRS - First Level of Access
    ''' Query is copied from the 'GetSerLogbycriteria' method.
    ''' </remarks>
    'Public Function GetSerLogbycriteria_Asur(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByVal isDescOrder As Boolean, ByRef dsResult As DataSet, ByRef strErr As String) As Boolean

    '    sqlConn.ConnectionString = strCIWConn

    '    Dim dtSerLog As New DataTable
    '    If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
    '        strErr = "Cannot Search without Start Date or CustomerID."
    '        Return False
    '    End If
    '    Dim strSQL As String = String.Empty
    '    strErr = String.Empty
    '    strSQL = "Select distinct t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator], "
    '    strSQL += "customerid = t1.UCID, "
    '    strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
    '    strSQL += "PolicyAccountNo=t1.PolicyNo_Asur, "
    '    strSQL += "PolicyType='Assurance', "
    '    strSQL += "ProductName='', "
    '    strSQL += "[Policy status] = '', RCD = '', "
    '    strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes,t1.alertnotes, "
    '    strSQL += "[Service Agent] = '',[Agent Code] = '', LocationCode = '', "
    '    strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV, "
    '    strSQL += "t1.caseno as [Suggestions/Grievances], "
    '    strSQL += "eStatement = '', "
    '    strSQL += "t1.ServiceEventNumber "
    '    strSQL += "From ServiceEventDetail t1 "
    '    strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
    '    strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
    '    strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
    '    strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
    '    strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
    '    strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
    '    strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
    '    strSQL += "where t1.EventSourceInitiatorCode<>'' "
    '    If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
    '        strSQL += "and t1.UCID='" + strcustId + "' "
    '    Else
    '        If Not IsDBNull(StartDate) Then
    '            strSQL += "and t1.EventInitialDateTime >='" + StartDate.ToString("yyyy-MM-dd") + "'"
    '        End If
    '        If Not IsDBNull(EndDate) Then
    '            strSQL += "and t1.EventInitialDateTime <='" + EndDate.ToString("yyyy-MM-dd") + "'"
    '        End If
    '        If strMediumCode <> String.Empty Then
    '            strSQL += "and t1.EventSourceMediumCode='" + strMediumCode + "' "
    '        End If
    '        If strEvtCatCode <> String.Empty Then
    '            strSQL += "and t1.EventCategoryCode='" + strEvtCatCode + "' "
    '        End If
    '        If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer
    '            strSQL += "and t1.UCID is not null "
    '        End If
    '    End If
    '    If isDescOrder Then
    '        strSQL += "Order by t1.EventInitialDateTime desc "
    '    Else
    '        strSQL += "Order by t1.EventInitialDateTime asc "
    '    End If

    '    Try
    '        Dim daSerlog As New SqlDataAdapter(strSQL, sqlConn)
    '        sqlConn.Open()
    '        daSerlog.Fill(dtSerLog)
    '        dsResult.Tables.Add(dtSerLog)
    '        If (dsResult Is Nothing) Then
    '            strErr = "Cannot get Service Log Detail!"
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        strErr = ex.Message
    '        'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
    '        Return False
    '    Finally
    '        sqlConn.Close()
    '    End Try

    'End Function

#End Region

#Region "Genterate CSV Spreadsheet for MIS"
    Public Sub GenerateCsv(ByVal dataname As String, ByVal ds As DataSet)
        Dim dt As DataTable = ds.Tables(0)
        Dim generationdattime As String = Now().ToString("yyyyMMdd_HHmm")
        Dim Path As String = "C:\Temp\" + dataname + "Report_" + generationdattime + ".csv"
        Try
            Dim myData As String = csvBytesWriter(dt)
            If Not File.Exists(Path) Then
                Dim fs As FileStream = File.Create(Path)
                fs.Close()
            Else
                Dim fs As FileStream
                File.Delete(Path)
                fs = File.Create(Path)
                fs.Close()
            End If
            Dim streamwriter As New System.IO.StreamWriter(Path, True, Encoding.UTF8)
            streamwriter.Write(myData)
            streamwriter.Dispose()
            MessageBox.Show("You may get your report at " & vbNewLine & Path, "Service Log Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function csvBytesWriter(ByVal dt As DataTable) As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim c As Integer = dt.Columns.Count
        Dim i As Integer = 0
        Dim ir As Integer
        For i = 0 To c - 1
            sb.Append("""" + dt.Columns(i).ColumnName.ToString() + """")
            If i = c - 1 Then
                sb.Append(" ")
                sb.Append(vbNewLine)
            Else
                sb.Append(",")
            End If
        Next
        For Each dr As DataRow In dt.Rows
            For ir = 0 To c - 1
                sb.Append("""" + dr(ir).ToString().Replace("""", """""") + """")
                If ir = c - 1 Then
                    sb.Append(" ")
                    sb.Append(vbNewLine)
                Else
                    sb.Append(",")
                End If

            Next
        Next
        Return sb.ToString()
    End Function
#End Region

#Region "Generate HTML Page for single page review"
    Public Function internalstylesheet() As String
        Dim stylesheet As String = "<style>"
        stylesheet += ".nav_container{display:flex; flex-direction: column}" + vbNewLine
        stylesheet += ".nav_items{padding: 10px;display:flex;flex-direction: row;align-content: center}" + vbNewLine
        stylesheet += ".nav_left{padding: 10px;flex-grow: 1;border: 1px solid black; border-right: 0; width:30vh;}" + vbNewLine
        stylesheet += ".nav_right{padding: 10px;flex-grow: 1;border: 1px solid black;width:50vh;}" + vbNewLine
        stylesheet += ".specialcare{color: red;font-weight: bold;}" + vbNewLine
        stylesheet += "p{display:inline; align-content: flex-start;padding-right: 5vh }" + vbNewLine
        stylesheet += "@media screen and (max-width: 800px) {" + vbNewLine
        stylesheet += ".nav_container{display:flex; flex-direction: column}" + vbNewLine
        stylesheet += ".nav_items{padding: 10px;flex-wrap: wrap; display:flex;flex-direction: column}" + vbNewLine
        stylesheet += ".nav_left{padding: 10px;flex-grow: 1;border: 1px solid black; border-bottom: 0; width:60vh;}" + vbNewLine
        stylesheet += ".nav_right{padding: 10px;flex-grow: 1;border: 1px solid black;width:60vh;}" + vbNewLine
        stylesheet += "p{display:inline; align-content: flex-start;padding-right: 20vh }" + vbNewLine
        stylesheet += "}</style>"
        Return stylesheet
    End Function

    Public Function GenerateWebPagehtml(ByVal dt As DataTable) As String
        Dim i As Integer
        Dim pagehtml As String = String.Empty
        pagehtml += "<html><head><title>Service Log Record</title>"
        pagehtml += internalstylesheet() + "</head><body>"
        Dim logitems As String = String.Empty
        Dim logleftcell As String
        Dim logrightcell As String
        Dim strevtdetail As String
        For i = 0 To dt.Rows.Count - 1
            'Generate left cell which contains call time, medium, initiator and event type
            logleftcell = String.Empty
            logleftcell = "<div class=nav_left>"
            logleftcell += "Status: " & dt.Rows(i)("EventStatus") & "<br/>" 'Event Status
            logleftcell += "Handle by: " & dt.Rows(i)("CSR") & "<br/>" 'CSR name
            logleftcell += dt.Rows(i)("EventInitialDateTime") & "<br/>" 'call time
            logleftcell += dt.Rows(i)("Medium") & " | " & dt.Rows(i)("Initiator") & "<br/>" 'medium, initiator
            'if Event Type is "Complaint","Grievance" or "Complaint - VHIS", case will be monitored by COmplaint Team, the webpage will also remind CSR to pay attention
            strevtdetail = dt.Rows(i)("Event Category") & " > " & dt.Rows(i)("Event Type") & " > " & dt.Rows(i)("Event Type Detail")
            If dt.Rows(i)("Event Type") = "Complaint" Or dt.Rows(i)("Event Type") = "Grievance" Or dt.Rows(i)("Event Type") = "Complaint - VHIS" Then
                strevtdetail = "<div class=specialcare>" & strevtdetail & "</div>"
            End If
            logleftcell += strevtdetail
            logleftcell += "</div>"
            'Generate left cell which contains call time, medium, initiator and event type
            logrightcell = String.Empty
            logrightcell = "<div class=nav_right>"
            logrightcell += "Policy Number: " & dt.Rows(i)("PolicyAccountNo") & "<br/>" & dt.Rows(i)("ProductName") & "(" & dt.Rows(i)("PolicyType") & ")<br/><br/>" 'Policy, product and product type
            logrightcell += "EventNotes:<br/><p>" & dt.Rows(i)("EventNotes") & "</p><br/><br/>" 'EventNotes
            logrightcell += "ReminderNotes:<br/><p>" & dt.Rows(i)("ReminderNotes") & "</p><br/><br/>" 'ReminderNotes
            logrightcell += "</div>"
            logitems += "<div class=nav_items>" & logleftcell & logrightcell & "</div>"
        Next
        pagehtml += "<div class=nav_container>" & logitems & "</div>"
        pagehtml += "</body></html>"
        Return pagehtml
    End Function

    Public Sub Generatewebpage(ByVal ds As DataSet)
        Dim dt As DataTable = ds.Tables(0)
        Dim strErr As String = String.Empty
        Dim Path As String = "C:\Temp\servicelog.html"
        Try
            Dim pagehtml As String = GenerateWebPagehtml(dt)
            If Not File.Exists(Path) Then
                Dim fs As FileStream = File.Create(Path)
                fs.Close()
            Else
                Dim fs As FileStream
                File.Delete(Path)
                fs = File.Create(Path)
                fs.Close()
            End If
            Dim streamwriter As New System.IO.StreamWriter(Path, True, Encoding.UTF8)
            streamwriter.Write(pagehtml)
            streamwriter.Dispose()
            OpenApplicationByChrome(Path, strErr)
            'MessageBox.Show("You may get your report at " & vbNewLine & Path, "Service Log Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class

Public Class Preference_JSON_result
    Public Medium As String
    Public Initiator As String
End Class
