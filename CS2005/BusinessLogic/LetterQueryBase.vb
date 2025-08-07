'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR3162 & 4101     Query related data
'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'002    15 Dec 2023     Oliver Ou                       Switch Over Code from Assurance to Bermuda 
Imports System.Data.SqlClient

Public Class LetterQueryBase

    ''' <summary>
    ''' Get address info
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetAddressInfo(ByVal policyNo As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from csw_policy_address where cswpad_poli_id='{0}' and cswpad_addr_type = 'C'"
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get address info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No address info record was found by policy no {0}", policyNo))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get customer info
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="policyRelateCode">PH:Policy Holder; PI:Policy Insured; SA:Servicing agent; BE:Beneficiary</param>
    ''' <returns></returns>
    Public Function GetCustomerInfo(ByVal policyNo As String, ByVal policyRelateCode As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        'oliver 2024-3-7 added for NUT-3813
        Dim strSql As String = "select c.* from Customer c " &
            "left join csw_poli_rel r on r.CustomerID = c.CustomerID " &
            "left join PolicyAccount p on p.PolicyAccountID = r.PolicyAccountID " &
            "where r.PolicyAccountID = '{0}' and r.PolicyRelateCode = '{1}' and p.CompanyID in ('ING', 'BMU', 'EAA', 'LAC', 'LAH')"
        strSql = String.Format(strSql, policyNo, policyRelateCode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get customer info: {0}", strErr))
        End If

        'If dtResult.Rows.Count = 0 Then
        '    Throw New Exception(String.Format("No customer info record was found by policy no {0}", policyNo))
        'End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get agent code info
    ''' </summary>
    ''' <param name="agentCode">GetCustomerInfo(policyNo, 'SA').Rows(0).Item("AgentCode").ToString()</param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetAgentCodesInfo(ByVal agentCode As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from AgentCodes where AgentCode = '{0}'"
        strSql = String.Format(strSql, agentCode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get agent info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No agent info record was found by agent code {0}", agentCode))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get plan name (Eng)
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPlanName(ByVal productId As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select Description from Product where ProductID = '{0}'"
        strSql = String.Format(strSql, productId)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get plan name: {0}", strErr))
        End If

        Dim desc As String = String.Empty
        If dtResult.Rows.Count > 0 Then
            desc = dtResult.Rows(0)(0).ToString().Trim()
        End If

        Return desc
    End Function

    ''' <summary>
    ''' Get plan name (Chi)
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPlanNameChi(ByVal productId As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select ChineseDescription from " & gcNBSDB & "Product_chi where ProductID = '{0}'"
        strSql = String.Format(strSql, productId)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get plan name: {0}", strErr))
        End If

        Dim desc As String = String.Empty
        If dtResult.Rows.Count > 0 Then
            desc = dtResult.Rows(0)(0).ToString().Trim()
        End If

        Return desc
    End Function

    ''' <summary>
    ''' Get payment mode (Eng)
    ''' </summary>
    ''' <param name="mode">GetPolicyAccountInfo(policyNo).Rows(0).Item("Mode").ToString()</param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPaymentMode(ByVal mode As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select ModeDesc from ModeCodes where ModeCode = '{0}'"
        strSql = String.Format(strSql, mode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get mode desc: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No mode desc was found by Mode {0}", mode))
        End If
        Dim desc As String = dtResult.Rows(0)(0).ToString().Trim()

        Return desc
    End Function

    ''' <summary>
    ''' Get policy status (Eng)
    ''' </summary>
    ''' <param name="accountStatusCode">GetPolicyAccountInfo(policyNo).Rows(0).Item("AccountStatusCode").ToString()</param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPolicyStatus(ByVal accountStatusCode As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select AccountStatus from AccountStatusCodes where AccountStatusCode = '{0}'"
        strSql = String.Format(strSql, accountStatusCode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get account status: {0}", strErr))
        End If

        Dim desc As String = String.Empty
        If dtResult.Rows.Count = 0 Then
            'Throw New Exception(String.Format("No account status was found by AccountStatusCode {0}", accountStatusCode))
        Else
            desc = dtResult.Rows(0)(0).ToString().Trim()
        End If

        Return desc
    End Function

    ''' <summary>
    ''' Get policy status (Chi)
    ''' </summary>
    ''' <param name="accountStatusCode">GetPolicyAccountInfo(policyNo).Rows(0).Item("AccountStatusCode").ToString()</param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPolicyStatusChi(ByVal accountStatusCode As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select AccountStatus_chi from AccountStatusCodes_chi where AccountStatusCode = '{0}'"
        strSql = String.Format(strSql, accountStatusCode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get account status: {0}", strErr))
        End If

        Dim desc As String = String.Empty
        If dtResult.Rows.Count = 0 Then
            'Throw New Exception(String.Format("No account status was found by AccountStatusCode {0}", accountStatusCode))
        Else
            desc = dtResult.Rows(0)(0).ToString().Trim()
        End If

        Return desc
    End Function

    ''' <summary>
    ''' Get coverage info
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <param name="trailer"></param>
    ''' <returns></returns>
    Public Function GetCoverageInfo(ByVal policyNo As String, ByVal strConn As String, Optional ByVal trailer As Integer = -1) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from Coverage where PolicyAccountID = '{0}'"
        strSql = String.Format(strSql, policyNo)

        If trailer <> -1 Then
            strSql = strSql & " and Trailer = " & trailer
        End If
        strSql = strSql & " order by trailer asc"

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get coverage info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No coverage info was found by policy no {0}", policyNo))
        End If

        Return dtResult
    End Function

    Public Function GetCoverageInfoBySP(ByVal policyNo As String, ByVal strConn As String) As DataTable

        Dim connection As SqlConnection = New SqlConnection(strConn)
        Dim dtPolicyInfo As DataTable = New DataTable
        connection.Open()
        Try
            Dim command As SqlCommand = New SqlCommand("cswsp_PolicyInfo", connection)
            command.Parameters.Add("@PolicyAccountID", policyNo)
            command.CommandType = CommandType.StoredProcedure

            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(dtPolicyInfo)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw
        Finally
            connection.Close()
        End Try

        Return dtPolicyInfo

    End Function

    ''' <summary>
    ''' Get PolicyAccount info
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPolicyAccountInfo(ByVal policyNo As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from PolicyAccount where PolicyAccountID = '{0}'"
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get policy account info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No policy account info was found by policy no {0}", policyNo))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get service log info
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <returns></returns>
    Public Function GetServiceLogInfo(ByVal policyNo As String, ByVal strConn As String, Optional strCompanyID As String = "ING") As DataTable
        Dim strErr As String = Nothing
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        If strCompanyID = "LAC" OrElse strCompanyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf strCompanyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If

        Dim strSql As String = Nothing
        strSql = "select (Case when event.sort_order is null then star.sort_order else event.sort_order end) as 'Status', " &
                         "(Case when event.EventStatus is null then star.EventStatus else event.EventStatus end) as 'EventStatus', " &
                         "t1.*, ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end " &
                         ", PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end " &
                         ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, t9.EventSourceMedium, isnull(t8.name,'') sender_name " &
                         ", PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end " &
             "From ServiceEventDetail t1 " &
             "left join ServiceEventDetailMCU_Extend e on t1.ServiceEventNumber = e.ServiceEventNumber " &
             "left join PolicyAccount t2 on t1.PolicyAccountID=t2.PolicyAccountID " &
             "left join PRODUCT t3 on t2.ProductID=t3.ProductiD " &
             "left join GI_PRODUCT t4 on t2.ProductID=t4.ProductiD " &
             "left join " & gcPOS & "vw_csw_event_category_code t5 on t1.EventCategoryCode=t5.cswecc_code " &
             "left join " & gcPOS & "vw_csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " &
             "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " &
             "left join " & serverPrefix & "CSR t8 on t1.MasterCSRID=t8.CSRID " &
             "left join EventStatusCodes as event on event.EventStatusCode = t1.EventStatusCode and event.CategoryCode= t1.EventCategoryCode and event.EventTypeCode = t1.EventTypeCode " &
             "left join EventStatusCodes as star on star.EventStatusCode = t1.EventStatusCode and star.CategoryCode= '*' and star.EventTypeCode = '*' " &
             "left join EventSourceMediumCodes t9 on t9.EventSourceMediumCode = t1.EventSourceMediumCode " &
             "left join (select cswpm_capsil_policy from csw_policy_map where cswpm_la_policy = '{0}') a on t1.PolicyAccountID=cswpm_capsil_policy Where (t1.PolicyAccountID = '{0}' or  t1.PolicyAccountID = 'GL{0}') " &
             "and e.ServiceEventNumber is null " &
             "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "

        '"left join (select cswpm_capsil_policy from csw_policy_map where cswpm_la_policy = '{0}') a on t1.PolicyAccountID=cswpm_capsil_policy Where (t1.PolicyAccountID = '{0}' or  t1.PolicyAccountID = 'GL{0}') " &
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to service log info: {0}", strErr))
        End If

        'If dtResult Is Nothing Or dtResult.Rows.Count = 0 Then
        '    Throw New Exception(String.Format("No service log record was found by policy no {0}", policyNo))
        'End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get service log info (MCU)
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <returns></returns>
    Public Function GetServiceLogInfo_MCU(ByVal policyNo As String) As DataTable
        Dim strErr As String = Nothing
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        Dim strSql As String = Nothing
        strSql = "Select (Case when event.sort_order is null then star.sort_order else event.sort_order end) as 'Status', " &
                         "(Case when event.EventStatus is null then star.EventStatus else event.EventStatus end) as 'EventStatus', " &
                         "t1.*, ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end " &
                         ", PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end " &
                         ", t5.cswecc_desc, t6.csw_event_typedtl_desc, t7.EventTypeDesc, t9.EventSourceMedium, isnull(t8.name,'') sender_name " &
                         ", PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end " &
                         ", e.FollowUpByMacau " &
             "From ServiceEventDetail t1 " &
             "left join ServiceEventDetailMCU_Extend e on t1.ServiceEventNumber = e.ServiceEventNumber " &
             "left join PolicyAccount t2 on t1.PolicyAccountID=t2.PolicyAccountID " &
             "left join PRODUCT t3 on t2.ProductID=t3.ProductiD " &
             "left join GI_PRODUCT t4 on t2.ProductID=t4.ProductiD " &
             "left join " & gcPOS & "vw_csw_event_category_code t5 on t1.EventCategoryCode=t5.cswecc_code " &
             "left join " & gcPOS & "vw_csw_event_typedtl_code t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code  " &
             "left join ServiceEventTypeCodes t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode " &
             "left join " & serverPrefix & "CSR t8 on t1.MasterCSRID=t8.CSRID " &
             "left join EventStatusCodes as event on event.EventStatusCode = t1.EventStatusCode and event.CategoryCode= t1.EventCategoryCode and event.EventTypeCode = t1.EventTypeCode " &
             "left join EventStatusCodes as star on star.EventStatusCode = t1.EventStatusCode and star.CategoryCode= '*' and star.EventTypeCode = '*' " &
             "left join EventSourceMediumCodes t9 on t9.EventSourceMediumCode = t1.EventSourceMediumCode " &
             "left join (select cswpm_capsil_policy from csw_policy_map where cswpm_la_policy = '{0}') a on t1.PolicyAccountID=cswpm_capsil_policy Where (t1.PolicyAccountID = '{0}' or  t1.PolicyAccountID = 'GL{0}') " &
             "and e.ServiceEventNumber is null " &
             "Order by Status asc, t1.EventInitialDateTime desc, t1.EventStatusCode asc "
        '"left join (select cswpm_capsil_policy from csw_policy_map where cswpm_la_policy = '{0}') a on t1.PolicyAccountID=cswpm_capsil_policy Where (t1.PolicyAccountID = '{0}' or  t1.PolicyAccountID = 'GL{0}') " &
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strCIWConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to service log info: {0}", strErr))
        End If

        'If dtResult Is Nothing Or dtResult.Rows.Count = 0 Then
        '    Throw New Exception(String.Format("No service log record was found by policy no {0}", policyNo))
        'End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get post sales call product setting data
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPostSalesCallProductSetting(ByVal productId As String, ByVal strConn As String, Optional strCompanyID As String = "ING") As DataTable
        Dim strErr As String = Nothing
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        Dim serverPrefix As String
        If strCompanyID = "LAC" OrElse strCompanyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf strCompanyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If

        Dim strSql As String = "select a.*, b.Description, c.ChineseDescription from " & serverPrefix & "csw_post_sales_call_product_setting a inner join Product b on a.cswpsd_ProductID=b.ProductID left outer join " & gcNBSDB & "Product_Chi c on a.cswpsd_ProductID=c.ProductID where a.cswpsd_ProductID = '{0}'"
        strSql = String.Format(strSql, productId)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get post sales call product setting data: {0}", strErr))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get CIWPRProductType info
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetCIWPRProductTypeInfo(ByVal productId As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from " & gcNBSDB & "ciwpr_product_type where CIWPT_ProductID = '{0}'"
        strSql = String.Format(strSql, productId)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get ciwpr_product_type info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No ciwpr_product_type info record was found by product ID {0}", productId))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get product type info
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetProductTypeInfo(ByVal productId As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from " & gcNBSDB & "Product_type where ProductID = '{0}'"
        strSql = String.Format(strSql, productId)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get product type info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No product type info record was found by product ID {0}", productId))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get payment type code
    ''' </summary>
    ''' <param name="payTypeCode"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetPaymentTypeCodesInfo(ByVal payTypeCode As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from " & gcPOS & "vw_PaymentTypeCodes where PaymentTypeCode = '{0}'"
        strSql = String.Format(strSql, payTypeCode)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get payment type code info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No payment type code info record was found by payment type code {0}", payTypeCode))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get billing type desc (Eng)
    ''' </summary>
    ''' <param name="billingType">GetPolicyAccountInfo(policyNo).Rows(0).Item("BillingType").ToString()</param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetBillingTypeDesc(ByVal billingType As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select BillingTypeDesc from " & gcPOS & "vw_billingtypecodes  where BillingTypeCode = '{0}'"
        strSql = String.Format(strSql, billingType)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get billing type desc: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No billing type desc record was found by billing type {0}", billingType))
        End If
        Dim desc As String = dtResult.Rows(0)(0).ToString().Trim()

        Return desc
    End Function

    ''' <summary>
    ''' Get billing type desc (Chi)
    ''' </summary>
    ''' <param name="billingType">GetPolicyAccountInfo(policyNo).Rows(0).Item("BillingType").ToString()</param>
    ''' <returns></returns>
    Public Function GetBillingTypeDescChi(ByVal billingType As String, ByVal strConn As String) As String
        Dim strErr As String = Nothing

        Dim strSql As String = "select BillingTypeDesc_chi from BillingTypeCodes_chi where BillingTypeCode = '{0}'"
        strSql = String.Format(strSql, billingType)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get billing type desc: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No billing type desc (chi) record was found by billing type {0}", billingType))
        End If
        Dim desc As String = dtResult.Rows(0)(0).ToString().Trim()

        Return desc
    End Function

    ''' <summary>
    ''' Gets the data structure of the Policy Value
    ''' <paramref name="strConn"/>
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPolicyValue(strConn As String, Optional companyID As String = "ING") As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = $"select * from {GetCrsServerPrefixByCompanyID(companyID)}csw_policy_value where cswval_TPOLID = 'xxx'"

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get csw_policy_value info: {0}", strErr))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' Get DDA data
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetDDAInfo(ByVal policyNo As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from DDA where PolicyAccountID = '{0}'"
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get DDA info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No DDA info record was found by policy no {0}", policyNo))
        End If

        Return dtResult
    End Function

    ''' <summary>
    ''' get CCDR data
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetCCDRInfo(ByVal policyNo As String, ByVal strConn As String) As DataTable
        Dim strErr As String = Nothing

        Dim strSql As String = "select * from CCDR where PolicyAccountID = '{0}'"
        strSql = String.Format(strSql, policyNo)

        Dim dtResult As New DataTable
        If Not ExcecuteQuery(strSql, strConn, dtResult, strErr) Then
            Throw New Exception(String.Format("Fail to get CCRD info: {0}", strErr))
        End If

        If dtResult.Rows.Count = 0 Then
            Throw New Exception(String.Format("No CCRD info record was found by policy no {0}", policyNo))
        End If

        Return dtResult
    End Function

End Class
