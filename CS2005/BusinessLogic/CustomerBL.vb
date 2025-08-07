Imports System.Data.SqlClient

Public Class CustomerBL

    Dim sqlConn As New SqlConnection
    Public Function GetPolicyHolderId(ByVal companyID As String,ByVal strpolnum As String, ByRef strCustomerId As String, ByRef strErr As String) As Boolean
        Dim dtph As New DataTable
        Dim sqlConn As New SqlConnection
        Dim strSQL As String = String.Empty

        sqlConn.ConnectionString = GetConnectionStringByCompanyID(companyID)
        strSQL = "select customerid from csw_poli_rel where PolicyRelateCode='PH'and policyaccountid='" & strpolnum & "'"
        Try
            Dim daph As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daph.Fill(dtph)
            strCustomerId = dtph.Rows(0)("customerid").ToString()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function


    Public Function GetMcuPolicyHolderId(ByVal strpolnum As String, ByRef strCustomerId As String, ByRef strErr As String) As Boolean
        Dim dtph As New DataTable
        Dim sqlConn As New SqlConnection
        Dim strSQL As String = String.Empty
        sqlConn.ConnectionString = strCIWMcuConn
        strSQL = "select customerid from csw_poli_rel where PolicyRelateCode='PH'and policyaccountid='" & strpolnum & "'"
        Try
            Dim daph As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daph.Fill(dtph)
            strCustomerId = dtph.Rows(0)("customerid").ToString()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetCustomerList(ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String, _
        ByVal strCustID As String, ByVal strAgentNo As String, ByVal strPlateNumber As String, _
        ByRef lngErrNo As Long, ByRef strErr As String, ByRef intCnt As Integer, ByRef dtcustlist As DataTable, _
        Optional ByVal blnCntOnly As Boolean = False) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim intTmpCnt As Integer
        Dim strSEL As String = String.Empty
        Dim strSELC As String = String.Empty
        Dim strSQL As String = String.Empty
        Dim strCR As String = String.Empty
        Dim strSQLcmd As String = String.Empty

        strSELC = "Select count(*) "
        strSQL += "select c.NamePrefix, c.FirstName, c.NameSuffix, isnull(c.ChiLstNm,'') + isnull(c.ChiFstNm,'') as ChiName, "
        strSQL += "GovernmentIDCard=case when GovernmentIDCard<>'' then c.GovernmentIDCard else c.PassportNumber end, c.Gender, "
        strSQL += "c.CoName, c.CoCName, c.CustomerID, c.DateOfBirth, c.AgentCode, c.AgentCode as clientID, aspu.UserName as [eServiceLogin] "
        strSQL += "from customer c "
        strSQL += "left join security..aspnet_Profile aspp on convert(varchar,aspp.PropertyValuesString)= convert(varchar,c.CustomerID) "
        strSQL += "left join security..aspnet_Users aspu on aspp.UserId=aspu.UserId "
        strSQL += "where c.customerid<>0 "

        If strLastName <> "" Then
            strCR += "and " + strLastName + " "
        End If

        If strFirstName <> "" Then
            strCR += "and " + strFirstName + " "
        End If

        If strAgentNo <> "" Then
            strCR += "and " + strAgentNo + " "
        End If

        If strHKID <> "" Then
            strCR += "and " + strHKID + " "
        End If

        If strCustID <> "" Then
            strCR += "and " + strCustID + " "
        End If

        If strPlateNumber <> "" Then
            strCR += " and CustomerID in (" & "select c.CustomerID from gi_vehicle a "
            strCR += "inner join csw_poli_rel b on a.PolicyAccountID=b.PolicyAccountID and b.PolicyRelateCode='PH' "
            strCR += "inner join customer c on b.CustomerID=c.CustomerID and c.CustomerID" + strPlateNumber + ") "
        End If

        If strCR = "" Then
            lngErrNo = -1
            strErr = "GetCustomerList - Invalid Criteria"
            Exit Function
        Else
            strSQL += strCR
        End If

        Try
            sqlConn.Open()
            If blnCntOnly Then
                Dim sqlcmd As New SqlCommand
                sqlcmd.CommandText = strSELC + strSQL
                sqlcmd.Connection = sqlConn
                intTmpCnt = sqlcmd.ExecuteScalar
            End If
            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                Dim dacustlist As New SqlDataAdapter(strSEL + strSQL, sqlConn)
                dacustlist.Fill(dtcustlist)
                intCnt = dtcustlist.Rows.Count
            End If
            Return True
        Catch err As SqlException
            lngErrNo = -1
            strErr = "GetCustomerList - " & err.ToString()
            Return False
        Catch ex As Exception
            lngErrNo = -1
            strErr = "GetCustomerList - " & ex.ToString
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetBadAddress(ByVal companyID As String, ByVal strCustID As String, ByRef dtBadAddr As DataTable, ByVal strErr As String) As Boolean
        sqlConn.ConnectionString = GetConnectionStringByCompanyID(companyID)
        Dim strSQL As String = "select AddressTypeCode from  customeraddress where BadAddress='Y' and customerid='" + strCustID + "'"
        Try
            Dim daBadAddr As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daBadAddr.Fill(dtBadAddr)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetMcuBadAddress(ByVal strCustID As String, ByRef dtBadAddr As DataTable, ByVal strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWMcuConn
        Dim strSQL As String = "select AddressTypeCode from  customeraddress where BadAddress='Y' and customerid='" + strCustID + "'"
        Try
            Dim daBadAddr As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daBadAddr.Fill(dtBadAddr)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function GetReturnMailMarkin(ByVal strCustID As String, ByRef dtMarkin As DataTable, ByVal strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = String.Empty
        strSQL += "select markin.Cswreg_policy_no as [PolicyNumber],markin.Cswreg_markin_date as [ReturnMailDate],markin.cswreg_rmk "
        strSQL += "from csw_req_reg markin "
        strSQL += "inner join  csw_poli_rel cr on  markin.Cswreg_policy_no=cr.PolicyAccountID and cr.PolicyRelateCode='PH' "
        strSQL += "where markin.Cswreg_txn_type='85' and cr.customerid='" + strCustID + "'"
        Try
            Dim daMarkin As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daMarkin.Fill(dtMarkin)
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function
End Class
