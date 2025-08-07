'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR3162 & 4101     Query the letter service logic


Imports System.Configuration
Imports System.IO
Imports System.Net

Public Class LetterCommonBL
    Inherits LetterQueryBase


    ''' <summary>
    ''' Get letter common field mapping
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function GetCommonField(ByVal policyNo As String, ByVal strConn As String) As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PolicyNo", GetType(String))
        dtFields.Columns.Add("LetterDateEng", GetType(String))
        dtFields.Columns.Add("LetterDateChi", GetType(String))
        dtFields.Columns.Add("ContactAddress1", GetType(String))
        dtFields.Columns.Add("ContactAddress2", GetType(String))
        dtFields.Columns.Add("ContactAddress3", GetType(String))
        dtFields.Columns.Add("ContactAddress4", GetType(String))
        dtFields.Columns.Add("OwnerPrefixEng", GetType(String))
        dtFields.Columns.Add("OwnerPrefixChi", GetType(String))
        dtFields.Columns.Add("OwnerNameEng", GetType(String))
        dtFields.Columns.Add("OwnerNameChi", GetType(String))
        dtFields.Columns.Add("OwnerPhone", GetType(String))
        dtFields.Columns.Add("AgentNameEng", GetType(String))
        dtFields.Columns.Add("AgentNameChi", GetType(String))
        dtFields.Columns.Add("AgentPhone", GetType(String))
        dtFields.Columns.Add("AgentUnit", GetType(String))
        dtFields.Columns.Add("AgentLocation", GetType(String))

        dtFields.Rows.Add(dtFields.NewRow())

        dtFields.Rows(0)("PolicyNo") = policyNo
        dtFields.Rows(0)("LetterDateEng") = DateTime.Now.ToString("dd MMM yyyy")
        dtFields.Rows(0)("LetterDateChi") = UCase(Format(DateTime.Now, "yyyy年M月d日"))

        'get address -s
        Dim dtAddress As DataTable = GetAddressInfo(policyNo, strConn)
        dtFields.Rows(0)("ContactAddress1") = dtAddress.Rows(0)("cswpad_add1").ToString()
        dtFields.Rows(0)("ContactAddress2") = dtAddress.Rows(0)("cswpad_add2").ToString()
        dtFields.Rows(0)("ContactAddress3") = dtAddress.Rows(0)("cswpad_add3").ToString()
        dtFields.Rows(0)("ContactAddress4") = dtAddress.Rows(0)("cswpad_city").ToString()
        'get address -e

        'get owner -s
        Dim dtOwner As DataTable = CustomerInfoMapping(policyNo, "PH", strConn)
        dtFields.Rows(0)("OwnerPrefixEng") = dtOwner.Rows(0)("PrefixEng")
        dtFields.Rows(0)("OwnerPrefixChi") = dtOwner.Rows(0)("PrefixChi")
        dtFields.Rows(0)("OwnerNameEng") = dtOwner.Rows(0)("NameEng")
        dtFields.Rows(0)("OwnerNameChi") = dtOwner.Rows(0)("NameChi")
        dtFields.Rows(0)("OwnerPhone") = dtOwner.Rows(0)("PhoneNo")
        'get owner -e

        'get agent -s
        Dim dtAgent As DataTable = CustomerInfoMapping(policyNo, "SA", strConn)
        dtFields.Rows(0)("AgentNameEng") = dtAgent.Rows(0)("NameEng")
        dtFields.Rows(0)("AgentNameChi") = dtAgent.Rows(0)("NameChi")
        Dim agentCode As String = dtAgent.Rows(0)("AgentCode").ToString().Trim()
        Dim agentCodesInfo As DataTable = GetAgentCodesInfo(agentCode, strConn)
        dtFields.Rows(0)("AgentPhone") = agentCodesInfo.Rows(0)("PhoneNumber").ToString().Trim()
        dtFields.Rows(0)("AgentUnit") = agentCodesInfo.Rows(0)("UnitCode").ToString().Trim()
        dtFields.Rows(0)("AgentLocation") = agentCodesInfo.Rows(0)("LocationCode").ToString().Trim()
        'get agent -e

        Return dtFields
    End Function

    ''' <summary>
    ''' Map the customer to the datatable
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="policyRelateCode"></param>
    ''' <returns></returns>
    Public Function CustomerInfoMapping(ByVal policyNo As String, ByVal policyRelateCode As String, ByVal strConn As String) As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PrefixEng", GetType(String))
        dtFields.Columns.Add("PrefixChi", GetType(String))
        dtFields.Columns.Add("NameEng", GetType(String))
        dtFields.Columns.Add("NameChi", GetType(String))
        dtFields.Columns.Add("PhoneNo", GetType(String))
        dtFields.Columns.Add("AgentCode", GetType(String))
        dtFields.Rows.Add(dtFields.NewRow())

        Dim dtCustomer As DataTable = GetCustomerInfo(policyNo, policyRelateCode, strConn)
        Dim namePrefix As String = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("NamePrefix").ToString())
        Dim gender As String = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("Gender").ToString())
        Dim prefixChi As String = String.Empty
        Dim prefixEng As String = If(dtCustomer.Rows.Count = 0, String.Empty, GetNamePrefix(namePrefix, gender, prefixChi))
        dtFields.Rows(0)("PrefixEng") = prefixEng
        dtFields.Rows(0)("PrefixChi") = prefixChi

        Dim lastName As String = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("NameSuffix").ToString().Trim())
        Dim firstName As String = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("FirstName").ToString.Trim())
        Dim fullNameEng As String = If(String.IsNullOrEmpty(lastName), firstName, If(String.IsNullOrEmpty(firstName), lastName, lastName & " " & firstName))
        Dim fullNameChi As String = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("ChiLstNm").ToString.Trim() & dtCustomer.Rows(0)("ChiFstNm").ToString().Trim())
        If String.IsNullOrWhiteSpace(fullNameChi) Then
            fullNameChi = fullNameEng
        End If

        dtFields.Rows(0)("NameEng") = fullNameEng
        dtFields.Rows(0)("NameChi") = fullNameChi
        dtFields.Rows(0)("PhoneNo") = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("PhoneMobile").ToString().Trim())
        dtFields.Rows(0)("AgentCode") = If(dtCustomer.Rows.Count = 0, String.Empty, dtCustomer.Rows(0)("AgentCode").ToString().Trim())

        Return dtFields
    End Function

    ''' <summary>
    ''' get name prefix (eng and chi)
    ''' </summary>
    ''' <param name="namePrefix"></param>
    ''' <param name="gender"></param>
    ''' <param name="prefixChi"></param>
    ''' <returns></returns>
    Public Function GetNamePrefix(ByVal namePrefix As String, ByVal gender As String, ByRef prefixChi As String) As String
        Dim prefix As String = Nothing

        If String.IsNullOrWhiteSpace(namePrefix) Then
            If String.IsNullOrWhiteSpace(gender) Then
                prefix = "Partner"
                prefixChi = "客戶"
            Else
                If gender.Trim().ToUpper() = "M" Then
                    prefix = "MR"
                    prefixChi = "先生"
                ElseIf gender.Trim().ToUpper() = "F" Then
                    prefix = "MS"
                    prefixChi = "女士"
                Else
                    prefix = "Partner"
                    prefixChi = "客戶"
                End If
            End If
        Else
            If namePrefix.Trim().ToUpper() = "MR" Then
                prefix = "MR"
                prefixChi = "先生"
            ElseIf namePrefix.Trim().ToUpper() = "MS" Then
                prefix = "MS"
                prefixChi = "女士"
            ElseIf namePrefix.Trim().ToUpper() = "MRS" Then
                prefix = "MRS"
                prefixChi = "太太"
            ElseIf namePrefix.Trim().ToUpper() = "MISS" Then
                prefix = "MISS"
                prefixChi = "小姐"
            Else
                prefix = "Partner"
                prefixChi = "客戶"
            End If
        End If

        Return prefix
    End Function

    ''' <summary>
    ''' Map the policy status (Eng)
    ''' </summary>
    ''' <param name="accountStatusCode"></param>
    ''' <param name="strConn"></param>
    ''' <returns></returns>
    Public Function PolicyStatusEngMapping(ByVal accountStatusCode As String, ByVal strConn As String) As String
        Dim policyStatus As String = Nothing

        Select Case accountStatusCode
            Case "Z", "V", "1", "2", "3", "4"
                policyStatus = "Active"
            Case "T"
                policyStatus = "Terminate"
            Case Else
                policyStatus = GetPolicyStatus(accountStatusCode, strConn)
        End Select

        Return policyStatus
    End Function

    ''' <summary>
    ''' Map the policy status (Chi)
    ''' </summary>
    ''' <param name="accountStatusCode"></param>
    ''' <returns></returns>
    Public Function PolicyStatusChiMapping(ByVal accountStatusCode As String, ByVal strConn As String) As String
        Dim policyStatus As String = Nothing

        Select Case accountStatusCode
            Case "Z", "V", "1", "2", "3", "4"
                policyStatus = "生效"
            Case = "T"
                policyStatus = "已終止"
            Case = "0"
                policyStatus = "預備生效"
            Case = "5"
                policyStatus = "申請被拒絕"
            Case = "6"
                policyStatus = "申請未完成"
            Case = "7"
                policyStatus = "申請資料不齊全"
            Case = "8"
                policyStatus = "已收妥款項，但未核保"
            Case = "9"
                policyStatus = "已核保，但未繳交款項"
            Case = "A"
                policyStatus = "不接納，由起保日起取消"
            Case = "B"
                policyStatus = "保單失效"
            Case = "C"
                policyStatus = "保單轉換"
            Case = "D"
                policyStatus = "已終止"
            Case = "E"
                policyStatus = "終止保單"
            Case = "F"
                policyStatus = "保單期滿"
            Case = "H"
                policyStatus = "保單已過期"
            Case = "I"
                policyStatus = "年前已終止"
            Case = "L"
                policyStatus = "取消保單"
            Case = "N"
                policyStatus = "未生效"
            Case = "W"
                policyStatus = "年金"
            Case = "X"
                policyStatus = "提早停繳保費(只限基本計劃)"
            Case Else
                policyStatus = GetPolicyStatusChi(accountStatusCode, strConn)
        End Select

        Return policyStatus
    End Function

    Public Function CoverageStatusEngMapping(ByVal CoverageStatus As String)
        Dim status As String = Nothing

        Select Case CoverageStatus
            Case "Z", "1", "2", "3", "4"
                status = "Active"
            Case "T"
                status = "Terminate"
            Case Else
                status = String.Empty
        End Select

        Return status
    End Function

    Public Function CoverageStatusChiMapping(ByVal CoverageStatus As String)
        Dim status As String = Nothing

        Select Case CoverageStatus
            Case "Z", "1", "2", "3", "4"
                status = "生效"
            Case "T"
                status = "已終止"
            Case "0"
                status = "預備生效"
            Case "5"
                status = "申請被拒絕"
            Case "6"
                status = "申請未完成"
            Case "7"
                status = "申請資料不齊全"
            Case "8"
                status = "已收妥款項，但未核保"
            Case "9"
                status = "已核保，但未繳交款項"
            Case "A"
                status = "不接納，由起保日起取消"
            Case "B"
                status = "保單失效"
            Case "C"
                status = "保單轉換"
            Case "D"
                status = "已終止"
            Case "E"
                status = "終止保單"
            Case "F"
                status = "保單期滿"
            Case "H"
                status = "保單已過期"
            Case "I"
                status = "年前已終止"
            Case "L"
                status = "取消保單"
            Case "N"
                status = "未生效"
            Case "W"
                status = "年金"
            Case "V"
                status = "提早停繳保費(全份計劃)"
            Case "X"
                status = "提早停繳保費(只限基本計劃)"
            Case Else
                status = String.Empty
        End Select

        Return status
    End Function

    ''' <summary>
    ''' setup CCMWS.LetterReuqest data
    ''' </summary>
    ''' <param name="ltrCode"></param>
    ''' <param name="ltrPrintMode"></param>
    ''' <param name="countryCode"></param>
    ''' <param name="companyCode"></param>
    ''' <param name="policyNo"></param>
    ''' <returns></returns>
    Public Function SetupLetterRequest(ByVal ltrCode As String, ByVal ltrPrintMode As String, ByVal countryCode As String, ByVal companyCode As String, ByVal policyNo As String) As CCMWS.LetterRequest
        Dim letterRequest As CCMWS.LetterRequest = New CCMWS.LetterRequest()

        'required fields -s
        letterRequest.SystemId = "CRS"
        letterRequest.LetterCode = ltrCode
        letterRequest.PrintMode = ltrPrintMode
        letterRequest.Country = countryCode
        letterRequest.CompanyCode = companyCode
        'required fields -e

        letterRequest.PolicyNo = policyNo

        Return letterRequest
    End Function

    ''' <summary>
    ''' DataTable mapped to CCMWS.LetterField()
    ''' </summary>
    ''' <param name="dtField"></param>
    ''' <returns></returns>
    Public Function DataMapToLetterField(ByVal dtField As DataTable) As CCMWS.LetterField()
        Dim fieldList As CCMWS.LetterField() = New CCMWS.LetterField(dtField.Columns.Count - 1) {}

        For Each row As DataRow In dtField.Rows
            For Each column As DataColumn In dtField.Columns
                Dim letterField As New CCMWS.LetterField
                letterField.FieldName = column.ColumnName
                letterField.FieldValue = row(column).ToString()
                fieldList(dtField.Columns.IndexOf(column)) = letterField
            Next
        Next

        Return fieldList
    End Function

    ''' <summary>
    ''' DataSet mapped to CCMWS.LetterRegion()()
    ''' </summary>
    ''' <param name="dsRegion"></param>
    ''' <returns></returns>
    Public Function DataMapToLetterRegion(ByVal dsRegion As DataSet) As CCMWS.LetterRegion()()
        Dim regionSet As CCMWS.LetterRegion()() = New CCMWS.LetterRegion(dsRegion.Tables.Count - 1)() {}

        For Each dt As DataTable In dsRegion.Tables
            Dim regionList As New List(Of CCMWS.LetterRegion)
            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    Dim letterRegin As New CCMWS.LetterRegion
                    letterRegin.RowNum = dt.Rows.IndexOf(row) + 1
                    letterRegin.ColumnNum = dt.Columns.IndexOf(column) + 1
                    letterRegin.FieldName = column.ColumnName
                    letterRegin.FieldValue = row(column).ToString()
                    regionList.Add(letterRegin)
                Next
            Next
            regionSet(dsRegion.Tables.IndexOf(dt)) = regionList.ToArray()
        Next

        Return regionSet
    End Function

    ''' <summary>
    ''' Get payment mode (chi)
    ''' </summary>
    ''' <param name="paymentMode"></param>
    ''' <returns></returns>
    Public Function GetPaymentModeChi(ByVal paymentMode As String) As String
        Dim paymentModeChi As String = Nothing
        Select Case paymentMode
            Case "Annually"
                paymentModeChi = "年供"
            Case "Half-annually"
                paymentModeChi = "半年供"
            Case "Monthly"
                paymentModeChi = "月供"
            Case "Quarterly"
                paymentModeChi = "季供"
            Case Else
                paymentModeChi = paymentMode
        End Select
        Return paymentModeChi
    End Function

    ''' <summary>
    ''' Get currency (chi)
    ''' </summary>
    ''' <param name="currency"></param>
    ''' <returns></returns>
    Public Function GetCurrencyChi(ByVal currency As String) As String
        Select Case currency
            Case "HKD"
                Return "港元"
            Case "USD"
                Return "美元"
            Case "EUR"
                Return "歐元"
            Case "MOP"
                Return "澳門幣"
            Case "RMB"
                Return "人民幣"
            Case Else
                Return currency
        End Select
    End Function

    ''' <summary>
    ''' Get payment list
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <param name="fromDate"></param>
    ''' <param name="toDate"></param>
    ''' <returns></returns>
    Public Function GetPaymentList(ByVal policyNo As String, ByVal fromDate As Date, ByVal toDate As Date, Optional strCompanyID As String = "ING") As DataSet

        'oliver 2024-3-7 added for NUT-3813
        Dim objDBHeader As New Utility.Utility.ComHeader
        Dim objMQQueHeader As New Utility.Utility.MQHeader
        Dim objPOSHeader As New Utility.Utility.POSHeader
        GetMQHeader(strCompanyID, objMQQueHeader, objDBHeader, objPOSHeader)

        Dim clsCRS As New LifeClientInterfaceComponent.clsCRS
        Dim dtSendData = New DataTable
        dtSendData.Rows.Add(dtSendData.NewRow())
        dtSendData.Columns.Add("Policy_No")
        dtSendData.Columns.Add("FromDate")
        dtSendData.Columns.Add("ToDate")

        dtSendData.Rows(0)("Policy_No") = RTrim(policyNo)
        dtSendData.Rows(0)("FromDate") = fromDate.ToString("yyyy-MM-dd")
        dtSendData.Rows(0)("ToDate") = toDate.ToString("yyyy-MM-dd")

        Dim dsPolicySend As New DataSet
        Dim dsPolicyRece As New DataSet
        dsPolicySend.Tables.Add(dtSendData)
        clsCRS.DBHeader = objDBHeader
        clsCRS.MQQueuesHeader = objMQQueHeader
        Dim strError As String = ""
        Dim result As Boolean = clsCRS.getPaymentHist(dsPolicySend, dsPolicyRece, strError)
        If result And strError = "" Then
            Return dsPolicyRece
        Else
            Throw New Exception(String.Format("get payment list error: {0}", strError))
        End If
    End Function

    ''' <summary>
    ''' Get coupon option desc (chi)
    ''' </summary>
    ''' <param name="couponOption">GetPolicyAccountInfo(policyNo).Rows(0).Item("CouponOption").ToString()</param>
    ''' <returns></returns>
    Public Function GetCouponOptionDescChi(ByVal couponOption As String) As String
        Dim desc As String = Nothing

        Select Case couponOption
            Case "0"
                desc = "沒有現金票券"
            Case "1"
                desc = "現金"
            Case "2"
                desc = "積存"
            Case "3"
                desc = "不適用"
            Case Else
                desc = GetCouponOption(couponOption)
        End Select

        Return desc
    End Function

    ''' <summary>
    ''' get dividend option desc (chi)
    ''' </summary>
    ''' <param name="dividendOption">GetPolicyAccountInfo(policyNo).Rows(0).Item("DividendOption").ToString()</param>
    ''' <returns></returns>
    Public Function GetDividendOptionDescChi(ByVal dividendOption As String) As String
        Dim desc As String = Nothing

        Select Case dividendOption
            Case "0"
                desc = "不適用"
            Case "1"
                desc = "支取現金"
            Case "2"
                desc = "Reduced Premium(Excess paid in cash)"
            Case "3"
                desc = "購買繳清額外保險"
            Case "4"
                desc = "積存紅利"
            Case "5"
                desc = "一年定期"
            Case "6"
                desc = "Reduced Loan (Excess left ot accumulate)"
            Case "9"
                desc = "紅利轉移"
            Case "A"
                desc = "一年定期 (Excess paid in cash)"
            Case "B"
                desc = "一年定期 (Excess Reduce Premium)"
            Case "C"
                desc = "一年定期 (Excess buy paid-up additions)"
            Case "D"
                desc = "一年定期 (Excess left to accumulate on deposit - MR25/9)"
            Case "E"
                desc = "增值保障利益附約"
            Case "O"
                desc = "沒有紅利"
            Case "R"
                desc = "歸原紅利"
            Case Else
                desc = GetDivOption(dividendOption)
        End Select

        Return desc
    End Function

    ''' <summary>
    ''' Get cooling off date
    ''' </summary>
    ''' <param name="policyNo"></param>
    ''' <returns></returns>
    Public Function GetCoolingOffDate(ByVal policyNo As String, ByVal comp As String) As String
        Dim dsCurr As New DataSet
        Dim strErr As String = String.Empty

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = If(comp = g_Comp, gobjMQQueHeader, gobjMcuMQQueHeader)
        clsPOS.DBHeader = If(comp = g_Comp, gobjDBHeader, gobjMcuDBHeader)

        If Not clsPOS.GetContractDetail(policyNo, dsCurr, strErr) Then
            Throw New Exception(String.Format("Fail to get cooling off date, the error: {0}", strErr))
        End If

        Dim coolingOffDate As String = String.Empty
        If dsCurr.Tables.Count > 0 AndAlso dsCurr.Tables(0).Rows.Count > 0 Then
            coolingOffDate = dsCurr.Tables(0).Rows(0)("CooloffDate")
        End If

        Return coolingOffDate
    End Function

    Public Function GetContractDetail(ByVal policyNo As String, Optional strCompanyID As String = "ING") As DataTable
        Dim dsContractDtl As New DataSet
        Dim strErr As String = String.Empty

        'oliver 2024-3-7 added for NUT-3813
        Dim objDBHeader As New Utility.Utility.ComHeader
        Dim objMQQueHeader As New Utility.Utility.MQHeader
        Dim objPOSHeader As New Utility.Utility.POSHeader
        GetMQHeader(strCompanyID, objMQQueHeader, objDBHeader, objPOSHeader)

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader

        If Not clsPOS.GetContractDetail(policyNo, dsContractDtl, strErr) Then
            Throw New Exception(String.Format("Fail to get contract detail, policy no : {0}, the error: {1}", policyNo, strErr))
        End If

        If dsContractDtl.Tables.Count = 0 AndAlso dsContractDtl.Tables(0).Rows.Count = 0 Then
            Throw New Exception(String.Format("No contract detail info record was found by policy no {0}", policyNo))
        End If

        Return dsContractDtl.Tables(0)

    End Function

    Public Function GetCOSelWithCustNo(ByVal policyNo As String, Optional strCompanyID As String = "ING") As DataTable
        Dim strErr As String = String.Empty
        Dim dsSentData As New DataSet
        Dim dsReceData As New DataSet
        Dim dtCOSelWithCustNo As New DataTable

        dtCOSelWithCustNo.Columns.Add("PolicyNo")
        Dim dr As DataRow = dtCOSelWithCustNo.NewRow()
        dr("PolicyNo") = policyNo
        dtCOSelWithCustNo.Rows.Add(dr)
        dsSentData.Tables.Add(dtCOSelWithCustNo)

        'oliver 2024-3-7 added for NUT-3813
        Dim objDBHeader As New Utility.Utility.ComHeader
        Dim objMQQueHeader As New Utility.Utility.MQHeader
        Dim objPOSHeader As New Utility.Utility.POSHeader
        GetMQHeader(strCompanyID, objMQQueHeader, objDBHeader, objPOSHeader)

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader

        If Not clsPOS.GetCOSelWithCustNo(dsSentData, dsReceData, strErr) Then
            Throw New Exception(String.Format("Fail to get COSelWithCustNo, policy no : {0}, the error: {1}", policyNo, strErr))
        End If

        If dsReceData.Tables.Count = 0 AndAlso dsReceData.Tables(0).Rows.Count = 0 Then
            Throw New Exception(String.Format("No COSelWithCustNo info record was found by policy no {0}", policyNo))
        End If

        Return dsReceData.Tables(1)

    End Function

    Public Function GetPremiumRoutine(ByVal policyNo As String, ByVal paidToDate As Date, ByVal strFreq As String, Optional strCompanyID As String = "ING") As DataTable

        Dim strErr As String = String.Empty
        Dim dsSentData As New DataSet
        Dim dsReceData As New DataSet
        Dim dtPremiumRoutine As New DataTable

        dtPremiumRoutine.Columns.Add("CompanyCode")
        dtPremiumRoutine.Columns.Add("PolicyNo")
        dtPremiumRoutine.Columns.Add("Paid_to_date", Type.GetType("System.DateTime"))
        dtPremiumRoutine.Columns.Add("BillingFrequency")
        Dim dr As DataRow = dtPremiumRoutine.NewRow()
        dr("CompanyCode") = "2"
        dr("PolicyNo") = policyNo
        dr("Paid_to_date") = paidToDate
        dr("BillingFrequency") = strFreq
        dtPremiumRoutine.Rows.Add(dr)

        dsSentData.Tables.Add(dtPremiumRoutine)

        'oliver 2024-3-7 added for NUT-3813
        Dim objDBHeader As New Utility.Utility.ComHeader
        Dim objMQQueHeader As New Utility.Utility.MQHeader
        Dim objPOSHeader As New Utility.Utility.POSHeader
        GetMQHeader(strCompanyID, objMQQueHeader, objDBHeader, objPOSHeader)

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader

        If Not clsPOS.GetPremiumRoutine(dsSentData, dsReceData, strErr) Then
            Throw New Exception(String.Format("Fail to get premium routine, policy no : {0}, the error: {1}", policyNo, strErr))
        End If

        If dsReceData.Tables.Count = 0 AndAlso dsReceData.Tables(0).Rows.Count = 0 Then
            Throw New Exception(String.Format("No premium routine info record was found by policy no {0}", policyNo))
        End If

        Return dsReceData.Tables(1)

    End Function

    ''' <summary>
    ''' Write byte to file
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns></returns>
    Public Function WriteByteToFile(ByVal bytes As Byte(), ByVal fileName As String) As String
        Try
            Dim baseFolder As String = ConfigurationManager.AppSettings("PdfPrintPath").ToString()
            If Not Directory.Exists(baseFolder) Then
                Directory.CreateDirectory(baseFolder)
            End If

            Dim path As String = System.IO.Path.Combine(baseFolder, fileName)
            Dim fs As New FileStream(path, FileMode.Create, FileAccess.Write)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()

            Return path
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get money format
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="scale"></param>
    ''' <returns></returns>
    Public Function GetMoneyFormat(ByVal obj As Object, Optional ByVal scale As Integer = 2) As String
        Dim str As String
        Dim result As Decimal = 0
        Decimal.TryParse(obj.ToString(), result)

        Dim scaleStr As String = IIf(scale = 0, "0", "0.0")
        If scale > 1 Then
            For i As Integer = 2 To scale
                scaleStr = scaleStr & "0"
            Next
        End If

        str = Format(result, "##,##" & scaleStr)
        Return str
    End Function


End Class
