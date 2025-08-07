'********************************************************************
' Amend By:  Kay Tsang
' Date:         11 Nov 2016
' Project:      ITSR492
' Ref:          KT20161111
' Changes:      Add visit CS flag WS call
' System.Configuration.ConfigurationSettings.AppSettings in app.config of CS2005 project (CS2005.exe.config)
'********************************************************************
' Amend By:  Kay Tsang
' Date:         26 Jul 2018
' Project:      ITSR879
' Ref:          KT20180726
' Changes:      Add pending txn for merchant
'********************************************************************
Imports System.Net
Imports System.IO
Imports System.Configuration
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports System.Security.Authentication

Public Class clsJSONTool

    Private Shared cstDateFormat As String = "yyyy-MM-dd"

    'Public Shared Function CallWeb(ByVal sURL As String, ByVal sFileName As String) As String

    '    Dim strsb As String = String.Empty
    '    Dim sOutPutFile As String = String.Empty
    '    Dim stream1 As System.IO.Stream

    '    'Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))
    '    'myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
    '    'System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))


    '    If Not String.IsNullOrEmpty(sURL) Then
    '        Try
    '            Dim Request As HttpWebRequest = WebRequest.Create(sURL) '(HttpWebRequest)

    '            Request.Method = "GET"
    '            'Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(sURL)
    '            'Request.ContentType = "text/html"

    '            'Request.ContentLength = bt.Length
    '            Request.Timeout = 60000
    '            'Request.Proxy = myProxy

    '            'Dim st As Stream = Request.GetRequestStream()
    '            'st.Write(bt, 0, bt.Length)
    '            'st.Close()
    '            Using hresponse As HttpWebResponse = TryCast(Request.GetResponse(), HttpWebResponse)

    '                If hresponse.StatusCode <> HttpStatusCode.OK Then
    '                    Throw New Exception([String].Format("Server error (HTTP {0}: {1}).", hresponse.StatusCode, hresponse.StatusDescription))
    '                End If
    '                stream1 = hresponse.GetResponseStream

    '                'owebbrowser.DocumentStream = stream1

    '                Dim sr As New StreamReader(stream1, System.Text.Encoding.UTF8)
    '                strsb = sr.ReadToEnd()

    '                'check temp\crs not exists then create folder
    '                sOutPutFile = "c:\temp\crs\crs_" & sFileName & ".html"

    '                File.WriteAllText(sOutPutFile, strsb)
    '                'owebbrowser.DocumentText = strsb
    '            End Using
    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End If
    '    Return sOutPutFile
    'End Function

    Public Shared Function ExcuteCall(ByVal serializedString As String, ByVal BaseAddress As String, ByVal ServiceCall As String, Optional ByVal ApiKey As String = "") As String
        Return ExcuteCall(serializedString, BaseAddress, ServiceCall, ApiKey, False)
    End Function
    Public Shared Function ExcuteCall(ByVal serializedString As String, ByVal BaseAddress As String, ByVal ServiceCall As String, Optional ByVal ApiKey As String = "", Optional ByVal isKongAPI As Boolean = False) As String

        Dim strsb As String = String.Empty
        'Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))

        'If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
        '    myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
        '    System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))

        'End If

        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall serializedString : " + serializedString + vbNewLine)
        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall BaseAddress : " + BaseAddress + vbNewLine)
        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall ServiceCall : " + ServiceCall + vbNewLine)
        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall ApiKey : " + ApiKey + vbNewLine)
        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall isKongAPI : " + isKongAPI.ToString + vbNewLine)

        If Not String.IsNullOrEmpty(BaseAddress) And Not String.IsNullOrEmpty(serializedString) And Not String.IsNullOrEmpty(ServiceCall) Then
            Try
                'oliver 2024-3-20 added for Check GIPolicy in U301 Env
                'System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or CType(CType(&HC00, SslProtocols), SecurityProtocolType)
                Dim Request As HttpWebRequest = WebRequest.Create(BaseAddress + ServiceCall) '(HttpWebRequest)

                If Not String.IsNullOrEmpty(ApiKey) Then
                    Request.Headers(IIf(isKongAPI, "apiKey", "Authorization")) = ApiKey
                End If

                Request.Method = "POST"
                Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(serializedString)
                Request.ContentType = "application/json"
                Request.ContentLength = bt.Length
                Request.Timeout = 300000
                If (ServiceCall.Equals("getInitOptOutCam")) Then
                    Request.Timeout = 600000
                End If
                'If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
                '    Request.Proxy = myProxy
                'End If
                Dim st As Stream = Request.GetRequestStream()
                st.Write(bt, 0, bt.Length)
                st.Close()
                Using hresponse As HttpWebResponse = TryCast(Request.GetResponse(), HttpWebResponse)
                    If hresponse.StatusCode <> HttpStatusCode.OK Then
                        Throw New Exception([String].Format("Server error (HTTP {0}: {1}).", hresponse.StatusCode, hresponse.StatusDescription))
                    End If
                    Dim stream1 As Stream = hresponse.GetResponseStream()
                    Dim sr As New StreamReader(stream1)
                    strsb = sr.ReadToEnd()

                End Using
            Catch ex As Exception
                Throw ex
            End Try

        End If

        'System.IO.File.AppendAllText("C:\Temp\CRSUtil_" + Now.ToString("yyyymmdd") + ".log", Now.ToString() + " CRS_Util.clsJSONCall.ExcuteCall return : " + strsb + vbNewLine)

        Return strsb
    End Function

    Public Shared Function CallWSByCustID(ByVal strCustID As String) As clsJSONBusinessObj.clsSearchResponse
        Return CallWSByCustID(strCustID, "")
    End Function
    Public Shared Function CallWSByCustID(ByVal strCustID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsSearchResponse
        Try
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objCustomerSearch As New clsJSONBusinessObj.clsCustomerSearch
            objCustomerSearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objCustomerSearch.callerRequest.customerId = strCustID
            objCustomerSearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser")

            Dim sJSON As String = JsonConvert.SerializeObject(objCustomerSearch)


            'giws/search
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"),
             System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSFuncName"), kongAPIKey, True)

            'File.WriteAllText("c:\temp\sURL.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            'Dim SearchResponse As New clsJSONBusinessObj.clsSearchResponse

            Dim SearchResponse = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsSearchResponse)(sRtnJSON, dtC)

            Return SearchResponse

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetGIDetailPageURL(ByVal sCustID As String, ByVal sPolicyID As String, ByVal sType As String) As String
        Dim sURL As String = String.Empty

        'sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL") & "gi/" & sType & "?policyId=" & sPolicyID & _
        '"&option=crs&company=hk&userId=" & System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser") & _
        '"&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"
        'giweb/gi/groupX?policyId=.....
        sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWebURL") & "gi/" & sType & "?policyId=" & sPolicyID & _
        "&option=crs&company=hk" & _
        "&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"

        'File.WriteAllText("c:\temp\sURL.txt", sURL)
        Return sURL
    End Function

    Public Shared Function GetEBDetailPageURL(ByVal sCustID As String, ByVal sPolicyID As String, ByVal sType As String) As String
        Dim sURL As String = String.Empty

        'sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL") & "eb/enquiry?policyId=" & sPolicyID & _
        '"&option=crs&type=" & sType & "&company=hk&userId=" & System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser") & _
        '"&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"
        'giweb/eb/enquiry?policyId=.......
        sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWebURL") & "eb/enquiry?policyId=" & sPolicyID & _
        "&option=crs&type=" & sType & "&company=hk" & _
        "&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"


        Return sURL
    End Function

    Public Shared Function CallGIPolicyWS(ByVal strPolicyID As String) As clsJSONBusinessObj.clsPolicySearchResponse
        Return CallGIPolicyWS(strPolicyID, "")
    End Function
    Public Shared Function CallGIPolicyWS(ByVal strPolicyID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsPolicySearchResponse
        Try
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objPolicySearch As New clsJSONBusinessObj.clsPolicySearch
            objPolicySearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objPolicySearch.policyAccountId = strPolicyID
            objPolicySearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser")

            Dim sJSON As String = JsonConvert.SerializeObject(objPolicySearch)

            'giws/getGIPolicyGroupType
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"),
             System.Configuration.ConfigurationSettings.AppSettings.Item("GIPolicyGroupWSFuncName"), kongAPIKey, True)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            'Dim SearchResponse As New clsJSONBusinessObj.clsSearchResponse

            Dim SearchResponse = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsPolicySearchResponse)(sRtnJSON, dtC)

            Return SearchResponse

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallEBPolicyWS(ByVal strPolicyID As String) As clsJSONBusinessObj.clsEBPolicySearchResponse
        Return CallEBPolicyWS(strPolicyID, "")
    End Function
    Public Shared Function CallEBPolicyWS(ByVal strPolicyID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsEBPolicySearchResponse
        Try
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objPolicySearch As New clsJSONBusinessObj.clsPolicySearch
            objPolicySearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objPolicySearch.policyAccountId = strPolicyID
            objPolicySearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser")

            Dim sJSON As String = JsonConvert.SerializeObject(objPolicySearch)

            'giws/getEBPolicyCompanyIdAndType
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"),
             System.Configuration.ConfigurationSettings.AppSettings.Item("GIEBPolicyGroupWSFuncName"), kongAPIKey, True)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            'Dim SearchResponse As New clsJSONBusinessObj.clsSearchResponse

            Dim SearchResponse = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsEBPolicySearchResponse)(sRtnJSON, dtC)

            Return SearchResponse

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallCheckVisitCSWS(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strLang As String) As clsJSONBusinessObj.clsVisitCSFlagResponse
        Return CallCheckVisitCSWS(strPolicyID, strCustID, strLang, "")
    End Function
    'KT20161111 start
    Public Shared Function CallCheckVisitCSWS(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strLang As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsVisitCSFlagResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("VisitCSCheckWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objVisitCSReq As New clsJSONBusinessObj.clsCheckVisitCSFlagRequest
            objVisitCSReq.policyId = strPolicyID
            objVisitCSReq.customerId = strCustID
            objVisitCSReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objVisitCSReq)

            'tngws/checkVisitCSFlag
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objVisitCSResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsVisitCSFlagResponse)(sRtnJSON, dtC)

            Return objVisitCSResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallUpdateVisitCSWS(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strFlag As String, ByVal strLang As String, ByVal strUpdUser As String) As clsJSONBusinessObj.clsVisitCSFlagResponse
        Return CallUpdateVisitCSWS(strPolicyID, strCustID, strFlag, strLang, strUpdUser, "")
    End Function
    Public Shared Function CallUpdateVisitCSWS(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strFlag As String, ByVal strLang As String, ByVal strUpdUser As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsVisitCSFlagResponse
        Try

            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("VisitCSUpdateWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objVisitCSReq As New clsJSONBusinessObj.clsUpdateVisitCSFlagRequest
            objVisitCSReq.policyId = strPolicyID
            objVisitCSReq.customerId = strCustID
            objVisitCSReq.visitCSFlag = strFlag
            objVisitCSReq.lang = strLang
            objVisitCSReq.updateUser = strUpdUser

            Dim sJSON As String = JsonConvert.SerializeObject(objVisitCSReq)

            'tngws/updateVisitCSFlag
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objVisitCSResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsVisitCSFlagResponse)(sRtnJSON, dtC)

            Return objVisitCSResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallUnfreezeOTPFunction(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strLang As String) As clsJSONBusinessObj.clsUnfreezeOTPFunctionResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("UnfreezeOTPWSFuncName")

            Dim objUnfreezeReq As New clsJSONBusinessObj.clsUnfreezeOTPFunctionRequest
            objUnfreezeReq.policyId = strPolicyID
            objUnfreezeReq.customerId = strCustID
            objUnfreezeReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objUnfreezeReq)

            'tngws/unfreezeOTPFunction
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objUnfreezeResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsUnfreezeOTPFunctionResponse)(sRtnJSON, dtC)

            Return objUnfreezeResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallCheckOTPLockStatus(ByVal strPolicyID As String, ByVal strCustID As String, ByVal strLang As String) As clsJSONBusinessObj.clsCheckOTPLockStatusResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("CheckOTPLockWSFuncName")

            Dim objCheckOTPLockStatusReq As New clsJSONBusinessObj.clsCheckOTPLockStatusRequest
            objCheckOTPLockStatusReq.policyId = strPolicyID
            objCheckOTPLockStatusReq.customerId = strCustID
            objCheckOTPLockStatusReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objCheckOTPLockStatusReq)

            'tngws/checkOTPLockStatus
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objCheckOTPLockStatusResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsCheckOTPLockStatusResponse)(sRtnJSON, dtC)

            Return objCheckOTPLockStatusResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallGetCSView(ByVal strCustID As String, ByVal strLang As String) As clsJSONBusinessObj.clsGetCSViewResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("GetCSViewFuncName")

            Dim objGetCSViewReq As New clsJSONBusinessObj.clsGetCSViewRequest
            objGetCSViewReq.customerId = strCustID
            objGetCSViewReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objGetCSViewReq)

            'tngws/getTngCSView
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetCSViewResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsGetCSViewResponse)(sRtnJSON, dtC)

            Return objGetCSViewResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallGetPendingTxn(ByVal strPolicyID As String, ByVal strLang As String) As clsJSONBusinessObj.clsPendingTxnResponse
        Return CallGetPendingTxn(strPolicyID, strLang, "")
    End Function
    Public Shared Function CallGetPendingTxn(ByVal strPolicyID As String, ByVal strLang As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsPendingTxnResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TapNGoWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("PendingTxnWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objGetPendingTxnReq As New clsJSONBusinessObj.clsPendingTxnRequest
            objGetPendingTxnReq.policyId = strPolicyID
            objGetPendingTxnReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objGetPendingTxnReq)

            'tngws/getPendingTransaction
            'Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetPendingTxnResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsPendingTxnResponse)(sRtnJSON, dtC)

            Return objGetPendingTxnResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function    'KT20161111 end

    Public Shared Function CallOdsGetOdsGetOdsid(ByVal strHkid As String, ByVal strPassportno As String, ByVal strCustid As String, ByVal strCustFullname As String) As clsJSONBusinessObj.clsGetOdsidResponse
        Return CallOdsGetOdsGetOdsid(strHkid, strPassportno, strCustid, strCustFullname, "")
    End Function
    'ITDCIC New Opt-Out Option Start
    Public Shared Function CallOdsGetOdsGetOdsid(ByVal strHkid As String, ByVal strPassportno As String, ByVal strCustid As String, ByVal strCustFullname As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsGetOdsidResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("GetOdsidFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objGetOdsidReq As New clsJSONBusinessObj.clsGetOdsidRequest
            objGetOdsidReq.custHkid = strHkid
            objGetOdsidReq.custId = strCustid
            objGetOdsidReq.custFullname = strCustFullname
            objGetOdsidReq.custPassportNo = strPassportno

            Dim sJSON As String = JsonConvert.SerializeObject(objGetOdsidReq)

            'optinws/getOdsId
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetOdsidResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsGetOdsidResponse)(sRtnJSON, dtC)

            Return objGetOdsidResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallUpdateOptOutFlag(ByVal strCustid As String, ByVal strOdsid As String, ByVal strCompanyid As String, ByVal strOptout As String, ByVal strUpdateuser As String, ByVal strUpdatedatetime As String) As List(Of clsJSONBusinessObj.clsUpdateOptOutFlagResponse)
        Return CallUpdateOptOutFlag(strCustid, strOdsid, strCompanyid, strOptout, strUpdateuser, strUpdatedatetime, "")
    End Function
    Public Shared Function CallUpdateOptOutFlag(ByVal strCustid As String, ByVal strOdsid As String, ByVal strCompanyid As String, ByVal strOptout As String, ByVal strUpdateuser As String, ByVal strUpdatedatetime As String, Optional ByVal strKongAPIKey As String = "") As List(Of clsJSONBusinessObj.clsUpdateOptOutFlagResponse)
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("UpdateOptoutFlagFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objUpdateOptOutFlagReq As New clsJSONBusinessObj.clsUpdateOptOutFlagRequest
            objUpdateOptOutFlagReq.custId = strCustid
            objUpdateOptOutFlagReq.odsId = strOdsid
            objUpdateOptOutFlagReq.companyId = strCompanyid
            objUpdateOptOutFlagReq.optOut = strOptout
            objUpdateOptOutFlagReq.updateUser = strUpdateuser
            objUpdateOptOutFlagReq.updateDateTime = strUpdatedatetime

            Dim sJSON As String = JsonConvert.SerializeObject(objUpdateOptOutFlagReq)

            'optinws/updateOptOut
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objUpdateOptOutFlagResp As List(Of clsJSONBusinessObj.clsUpdateOptOutFlagResponse) = JsonConvert.DeserializeObject(Of List(Of clsJSONBusinessObj.clsUpdateOptOutFlagResponse))(sRtnJSON, dtC)

            Return objUpdateOptOutFlagResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallGetInitOptOutFlag(ByVal strCustid As String, ByVal strOdsid As String, ByVal strCompanyid As String) As List(Of clsJSONBusinessObj.clsGetInitOptOutFlagResponse)
        Return CallGetInitOptOutFlag(strCustid, strOdsid, strCompanyid, "")
    End Function
    Public Shared Function CallGetInitOptOutFlag(ByVal strCustid As String, ByVal strOdsid As String, ByVal strCompanyid As String, Optional ByVal strKongAPIKey As String = "") As List(Of clsJSONBusinessObj.clsGetInitOptOutFlagResponse)
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("GetInitOptoutFlagFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objGetInitOptOutFlagReq As New clsJSONBusinessObj.clsGetInitOptOutFlagRequest
            objGetInitOptOutFlagReq.custId = strCustid
            objGetInitOptOutFlagReq.odsId = strOdsid
            objGetInitOptOutFlagReq.companyId = strCompanyid


            Dim sJSON As String = JsonConvert.SerializeObject(objGetInitOptOutFlagReq)

            'optinws/getInitOptOut
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetInitOptOutFlagResp As List(Of clsJSONBusinessObj.clsGetInitOptOutFlagResponse) = JsonConvert.DeserializeObject(Of List(Of clsJSONBusinessObj.clsGetInitOptOutFlagResponse))(sRtnJSON, dtC)

            Return objGetInitOptOutFlagResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallGetInitOptOutCam(ByVal strCustId As List(Of String), ByVal strInOutFilter As String) As clsJSONBusinessObj.clsGetInitOptOutFlagCamResponse
        Return CallGetInitOptOutCam(strCustId, strInOutFilter, "")
    End Function
    Public Shared Function CallGetInitOptOutCam(ByVal strCustId As List(Of String), ByVal strInOutFilter As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsGetInitOptOutFlagCamResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("GetInitOptOutCamFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objGetInitOptOutCamReq As New clsJSONBusinessObj.clsGetInitOptOutFlagCamRequest
            objGetInitOptOutCamReq.custId = strCustId
            objGetInitOptOutCamReq.inOutFilter = strInOutFilter

            Dim sJSON As String = JsonConvert.SerializeObject(objGetInitOptOutCamReq)
            'optinws/getInitOptOutCam
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetInitOptOutCamResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsGetInitOptOutFlagCamResponse)(sRtnJSON, dtC)

            Return objGetInitOptOutCamResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CheckCrmUser(ByVal strUserId As String) As clsJSONBusinessObj.clsCheckCrmUserResponse
        Return CheckCrmUser(strUserId, "")
    End Function
    Public Shared Function CheckCrmUser(ByVal strUserId As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsCheckCrmUserResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("CheckCrmUserFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objCheckCrmUserReq As New clsJSONBusinessObj.clsCheckCrmUserRequest
            objCheckCrmUserReq.userId = strUserId

            Dim sJSON As String = JsonConvert.SerializeObject(objCheckCrmUserReq)
            'optinws/checkCrmUser
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objCheckCrmUserResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsCheckCrmUserResponse)(sRtnJSON, dtC)

            Return objCheckCrmUserResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'ITDCIC New Opt-Out Option End

    'ITDCIC 7-11 & ISC claim payout Start
    Public Shared Function Call7ElevenUnfreezePayout(ByVal intPayoutId As Integer, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "") As clsJSONBusinessObj.cls7ElevenUnfreezePayoutResponse
        Return Call7ElevenUnfreezePayout(intPayoutId, strUserId, strLang, strMerchantId, "")
    End Function
    Public Shared Function Call7ElevenUnfreezePayout(ByVal intPayoutId As Integer, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "", Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.cls7ElevenUnfreezePayoutResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("UnfreezePayoutWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objUnfreezePayoutReq As New clsJSONBusinessObj.cls7ElevenUnfreezePayoutRequest
            objUnfreezePayoutReq.payoutId = intPayoutId
            objUnfreezePayoutReq.userId = strUserId
            objUnfreezePayoutReq.lang = strLang
            'objUnfreezePayoutReq.merchantId = strMerchantId
            If strMerchantId.Length > 0 Then
                objUnfreezePayoutReq.merchantId = strMerchantId
            End If

            Dim sJSON As String = JsonConvert.SerializeObject(objUnfreezePayoutReq)

            'payoutws/unfreezePayout
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim obj7ElevenUnfreezePayoutResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenUnfreezePayoutResponse)(sRtnJSON, dtC)

            Return obj7ElevenUnfreezePayoutResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Call7ElevenVoidPayout(ByVal intPayoutId As Integer, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "") As clsJSONBusinessObj.cls7ElevenVoidPayoutResponse
        Return Call7ElevenVoidPayout(intPayoutId, strUserId, strLang, strMerchantId, "")
    End Function
    Public Shared Function Call7ElevenVoidPayout(ByVal intPayoutId As Integer, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "", Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.cls7ElevenVoidPayoutResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("VoidPayoutWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objVoidPayoutReq As New clsJSONBusinessObj.cls7ElevenVoidPayoutRequest
            objVoidPayoutReq.payoutId = intPayoutId
            objVoidPayoutReq.userId = strUserId
            objVoidPayoutReq.lang = strLang
            'objVoidPayoutReq.merchantId = strMerchantId
            If strMerchantId.Length > 0 Then
                objVoidPayoutReq.merchantId = strMerchantId
            End If

            Dim sJSON As String = JsonConvert.SerializeObject(objVoidPayoutReq)

            'payoutws/voidPayout
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim obj7ElevenVoidPayoutResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenVoidPayoutResponse)(sRtnJSON, dtC)

            Return obj7ElevenVoidPayoutResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Call7ElevenReqPayout(ByVal strMobile As String, ByVal strHkid As String, ByVal strPassport As String, ByVal strEmail As String,
                                                ByVal strPayoutDate As String, ByVal strPayoutType As String,
                                                ByVal strPayoutDept As String, ByVal strPayoutCurrency As String,
                                                ByVal strPayoutAmount As String, ByVal strPolicyCurrency As String,
                                                ByVal strExchangeRate As String, ByVal strCmIndex As String,
                                                ByVal strMerchantId As String, ByVal strClaimId As String,
                                                ByVal strCustomerId As String, ByVal strPolicyId As String,
                                                ByVal strLang As String, ByVal strLocation As String) As clsJSONBusinessObj.cls7ElevenReqPayoutResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL")

            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("RequestPayoutWSFuncName")

            Dim objRequestPayoutReq As New clsJSONBusinessObj.cls7ElevenReqPayoutRequest
            objRequestPayoutReq.mobile = strMobile
            Dim tblCustdetails As New Dictionary(Of String, String)
            If Not String.IsNullOrEmpty(strHkid) Then
                If strLocation = "MO" Then
                    tblCustdetails.Add("moid", strHkid)
                Else
                    tblCustdetails.Add("hkid", strHkid)
                End If
            End If
            If Not String.IsNullOrEmpty(strPassport) Then
                If strLocation = "MO" Then
                    tblCustdetails.Add("mopassport", strPassport)
                Else
                    tblCustdetails.Add("passport", strPassport)
                End If
            End If
            If Not String.IsNullOrEmpty(strEmail) Then
                tblCustdetails.Add("email", strEmail)
            End If
            objRequestPayoutReq.custDetails = tblCustdetails
            objRequestPayoutReq.payoutDate = strPayoutDate
            objRequestPayoutReq.payoutType = strPayoutType
            objRequestPayoutReq.payoutDept = strPayoutDept
            objRequestPayoutReq.payoutCurrency = strPayoutCurrency
            objRequestPayoutReq.payoutAmount = strPayoutAmount
            objRequestPayoutReq.policyCurrency = strPolicyCurrency
            objRequestPayoutReq.exchangeRate = strExchangeRate
            objRequestPayoutReq.cmIndex = strCmIndex
            objRequestPayoutReq.merchantId = strMerchantId
            objRequestPayoutReq.claimId = strClaimId
            objRequestPayoutReq.customerId = strCustomerId
            objRequestPayoutReq.policyId = strPolicyId
            objRequestPayoutReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objRequestPayoutReq)

            'payoutws/requestPayout
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim obj7ElevenReqPayoutResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenReqPayoutResponse)(sRtnJSON, dtC)

            Return obj7ElevenReqPayoutResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Call7ElevenReqPayout(ByVal strMobile As String, ByVal strHkid As String, ByVal strPassport As String, ByVal strEmail As String,
                                                ByVal strPayoutDate As String, ByVal strPayoutType As String,
                                                ByVal strPayoutDept As String, ByVal strPayoutCurrency As String,
                                                ByVal strPayoutAmount As String, ByVal strPolicyCurrency As String,
                                                ByVal strExchangeRate As String, ByVal strCmIndex As String,
                                                ByVal strMerchantId As String, ByVal strClaimId As String,
                                                ByVal strCustomerId As String, ByVal strPolicyId As String,
                                                ByVal strLang As String, ByVal strLocation As String,
                                                Optional ByVal strKongAPIKey As String = "", Optional ByVal strPayoutWSURL As String = "", Optional ByVal strRequestPayoutWSFuncName As String = "") As clsJSONBusinessObj.cls7ElevenReqPayoutResponse
        Try
            Dim url As String = IIf(Not String.IsNullOrEmpty(strPayoutWSURL), strPayoutWSURL, System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL"))

            Dim func As String = IIf(Not String.IsNullOrEmpty(strRequestPayoutWSFuncName), strRequestPayoutWSFuncName, System.Configuration.ConfigurationSettings.AppSettings.Item("RequestPayoutWSFuncName"))

            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objRequestPayoutReq As New clsJSONBusinessObj.cls7ElevenReqPayoutRequest
            objRequestPayoutReq.mobile = strMobile
            Dim tblCustdetails As New Dictionary(Of String, String)
            If Not String.IsNullOrEmpty(strHkid) Then
                If strLocation = "MO" Then
                    tblCustdetails.Add("moid", strHkid)
                Else
                    tblCustdetails.Add("hkid", strHkid)
                End If
            End If
            If Not String.IsNullOrEmpty(strPassport) Then
                If strLocation = "MO" Then
                    tblCustdetails.Add("mopassport", strPassport)
                Else
                    tblCustdetails.Add("passport", strPassport)
                End If
            End If
            If Not String.IsNullOrEmpty(strEmail) Then
                tblCustdetails.Add("email", strEmail)
            End If
            objRequestPayoutReq.custDetails = tblCustdetails
            objRequestPayoutReq.payoutDate = strPayoutDate
            objRequestPayoutReq.payoutType = strPayoutType
            objRequestPayoutReq.payoutDept = strPayoutDept
            objRequestPayoutReq.payoutCurrency = strPayoutCurrency
            objRequestPayoutReq.payoutAmount = strPayoutAmount
            objRequestPayoutReq.policyCurrency = strPolicyCurrency
            objRequestPayoutReq.exchangeRate = strExchangeRate
            objRequestPayoutReq.cmIndex = strCmIndex
            objRequestPayoutReq.merchantId = strMerchantId
            objRequestPayoutReq.claimId = strClaimId
            objRequestPayoutReq.customerId = strCustomerId
            objRequestPayoutReq.policyId = strPolicyId
            objRequestPayoutReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objRequestPayoutReq)

            'payoutws/requestPayout
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim obj7ElevenReqPayoutResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenReqPayoutResponse)(sRtnJSON, dtC)

            Return obj7ElevenReqPayoutResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Call7ElevenSrhSmsHist(ByVal intPayoutId As Integer, ByVal strLang As String, Optional ByVal strMerchantId As String = "") As clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse
        Return Call7ElevenSrhSmsHist(intPayoutId, strLang, strMerchantId, "")
    End Function
    Public Shared Function Call7ElevenSrhSmsHist(ByVal intPayoutId As Integer, ByVal strLang As String, Optional ByVal strMerchantId As String = "", Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("SrhSmsHistWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objSrhSmsHistReq As New clsJSONBusinessObj.cls7ElevenSrhSmsHistRequest
            objSrhSmsHistReq.payoutId = intPayoutId
            objSrhSmsHistReq.lang = strLang
            'objSrhSmsHistReq.merchantId = strMerchantId
            If strMerchantId.Length > 0 Then
                objSrhSmsHistReq.merchantId = strMerchantId
            End If

            Dim sJSON As String = JsonConvert.SerializeObject(objSrhSmsHistReq)

            'payoutws/searchPayoutSMSHistory
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objSrhSmsHistResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse)(sRtnJSON, dtC)

            Return objSrhSmsHistResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Call7ElevenResendSms(ByVal strSmsMessageId As String, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "") As clsJSONBusinessObj.cls7ElevenResendSmsResponse
        Return Call7ElevenResendSms(strSmsMessageId, strUserId, strLang, strMerchantId, "")
    End Function
    Public Shared Function Call7ElevenResendSms(ByVal strSmsMessageId As String, ByVal strUserId As String, ByVal strLang As String, Optional ByVal strMerchantId As String = "", Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.cls7ElevenResendSmsResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("7ElevenPayoutWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("ResendSmsWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objResendSmsReq As New clsJSONBusinessObj.cls7ElevenResendSmsRequst
            objResendSmsReq.smsMessageId = strSmsMessageId
            objResendSmsReq.userId = strUserId
            objResendSmsReq.lang = strLang
            'objResendSmsReq.merchantId = strMerchantId
            If strMerchantId.Length > 0 Then
                objResendSmsReq.merchantId = strMerchantId
            End If

            Dim sJSON As String = JsonConvert.SerializeObject(objResendSmsReq)

            'payoutws/resendSMS
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objResendSmsResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.cls7ElevenResendSmsResponse)(sRtnJSON, dtC)

            Return objResendSmsResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'ITDCIC 7-11 & ISC claim payout End

    'KT20171207
    Public Shared Function CallBackdoorRegGenKey(ByVal strFullName As String, ByVal strLocation As String) As clsJSONBusinessObj.clsBackdoorRegResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("BackdoorRegGenKeyURL")
            If strLocation = "MO" Then
                url = System.Configuration.ConfigurationSettings.AppSettings.Item("MacauBackdoorRegGenKeyURL")
            End If
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("BackdoorRegGenKeyFuncName")
            'Dim useProxy As String = System.Configuration.ConfigurationSettings.AppSettings.Item("useProxy")

            'need to disable proxy for calling internal balancer 10.11.221.31port 3000
            'If useProxy = "1" Then
            '    System.Configuration.ConfigurationSettings.AppSettings.Item("useProxy") = "0"
            'End If

            Dim objBackdoorRegReq As New clsJSONBusinessObj.clsBackdoorRegRequest
            objBackdoorRegReq.ceidentifier = strFullName

            Dim sJSON As String = JsonConvert.SerializeObject(objBackdoorRegReq)

            'registration/backdoor/genKey
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objBackdoorRegResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsBackdoorRegResponse)(sRtnJSON, dtC)

            'System.Configuration.ConfigurationSettings.AppSettings.Item("useProxy") = "1" 'make sure the proxy enabled again

            Return objBackdoorRegResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'KT20180726
    Public Shared Function CallGetMerchantPendingTxn(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String) As clsJSONBusinessObj.clsMerchantPendingTxnResponse
        Return CallGetMerchantPendingTxn(strPolicyID, strLang, strProductID, strMerchantID, "")
    End Function
    Public Shared Function CallGetMerchantPendingTxn(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsMerchantPendingTxnResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("MerchantPendingTxnWSFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objGetPendingTxnReq As New clsJSONBusinessObj.clsMerchantPendingTxnRequest
            objGetPendingTxnReq.policyId = strPolicyID
            objGetPendingTxnReq.productId = strProductID
            objGetPendingTxnReq.merchantId = strMerchantID
            objGetPendingTxnReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objGetPendingTxnReq)

            'transactionws/checkPendingTransaction
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objGetPendingTxnResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsMerchantPendingTxnResponse)(sRtnJSON, dtC)

            Return objGetPendingTxnResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallGetMerchantProductInfo(ByVal strPolicyID As String, ByVal strLang As String) As clsJSONBusinessObj.clsMerchantProductResponse
        Return CallGetMerchantProductInfo(strPolicyID, strLang, "")
    End Function
    Public Shared Function CallGetMerchantProductInfo(ByVal strPolicyID As String, ByVal strLang As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsMerchantProductResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("MerchantProductFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objMerchantProductReq As New clsJSONBusinessObj.clsMerchantProductRequest
            objMerchantProductReq.policyId = strPolicyID
            objMerchantProductReq.lang = strLang

            Dim sJSON As String = JsonConvert.SerializeObject(objMerchantProductReq)

            'transactionws/getMerchantProductInfo
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objMerchantProductResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsMerchantProductResponse)(sRtnJSON, dtC)

            Return objMerchantProductResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallValidateO2OWithdraw(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal) As clsJSONBusinessObj.clsValidateO2OResponse
        Return CallValidateO2OWithdraw(strPolicyID, strLang, strProductID, strMerchantID, strUserID, strCcy, decAmount, "")
    End Function
    Public Shared Function CallValidateO2OWithdraw(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsValidateO2OResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("ValidateO2OWithdrawFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objValidateO2OReq As New clsJSONBusinessObj.clsValidateO2ORequest
            objValidateO2OReq.policyId = strPolicyID
            objValidateO2OReq.lang = strLang
            objValidateO2OReq.amount = decAmount
            objValidateO2OReq.currency = strCcy
            objValidateO2OReq.merchantId = strMerchantID
            objValidateO2OReq.productId = strProductID
            objValidateO2OReq.userId = strUserID



            Dim sJSON As String = JsonConvert.SerializeObject(objValidateO2OReq)

            'transactionws/validateWithdraw
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objValidateO2Resp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsValidateO2OResponse)(sRtnJSON, dtC)

            Return objValidateO2Resp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallValidateO2OTopUp(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal) As clsJSONBusinessObj.clsValidateO2OResponse
        Return CallValidateO2OTopUp(strPolicyID, strLang, strProductID, strMerchantID, strUserID, strCcy, decAmount, "")
    End Function
    Public Shared Function CallValidateO2OTopUp(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsValidateO2OResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("ValidateO2OTopUpFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objValidateO2OReq As New clsJSONBusinessObj.clsValidateO2ORequest
            objValidateO2OReq.policyId = strPolicyID
            objValidateO2OReq.lang = strLang
            objValidateO2OReq.amount = decAmount
            objValidateO2OReq.currency = strCcy
            objValidateO2OReq.merchantId = strMerchantID
            objValidateO2OReq.productId = strProductID
            objValidateO2OReq.userId = strUserID



            Dim sJSON As String = JsonConvert.SerializeObject(objValidateO2OReq)

            'transactionws/validateTopUp
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objValidateO2Resp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsValidateO2OResponse)(sRtnJSON, dtC)

            Return objValidateO2Resp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallPerformO2OWithdraw(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, ByVal strTxnID As String) As clsJSONBusinessObj.clsPerformO2OResponse
        Return CallPerformO2OWithdraw(strPolicyID, strLang, strProductID, strMerchantID, strUserID, strCcy, decAmount, strTxnID, "")
    End Function
    Public Shared Function CallPerformO2OWithdraw(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, ByVal strTxnID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsPerformO2OResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("PerformO2OWithdrawFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objPerformO2OReq As New clsJSONBusinessObj.clsPerformO2ORequest
            objPerformO2OReq.policyId = strPolicyID
            objPerformO2OReq.lang = strLang
            objPerformO2OReq.amount = decAmount
            objPerformO2OReq.currency = strCcy
            objPerformO2OReq.merchantId = strMerchantID
            objPerformO2OReq.productId = strProductID
            objPerformO2OReq.userId = strUserID
            objPerformO2OReq.txnId = strTxnID


            Dim sJSON As String = JsonConvert.SerializeObject(objPerformO2OReq)

            'transactionws/o2oWithdraw
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objPerformO2OResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsPerformO2OResponse)(sRtnJSON, dtC)

            Return objPerformO2OResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallPerformO2OTopUp(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, ByVal strTxnID As String) As clsJSONBusinessObj.clsPerformO2OResponse
        Return CallPerformO2OTopUp(strPolicyID, strLang, strProductID, strMerchantID, strUserID, strCcy, decAmount, strTxnID, "")
    End Function
    Public Shared Function CallPerformO2OTopUp(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strUserID As String, ByVal strCcy As String, ByVal decAmount As Decimal, ByVal strTxnID As String, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsPerformO2OResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("PerformO2OTopUpFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objPerformO2OReq As New clsJSONBusinessObj.clsPerformO2ORequest
            objPerformO2OReq.policyId = strPolicyID
            objPerformO2OReq.lang = strLang
            objPerformO2OReq.amount = decAmount
            objPerformO2OReq.currency = strCcy
            objPerformO2OReq.merchantId = strMerchantID
            objPerformO2OReq.productId = strProductID
            objPerformO2OReq.userId = strUserID
            objPerformO2OReq.txnId = strTxnID


            Dim sJSON As String = JsonConvert.SerializeObject(objPerformO2OReq)

            'transactionws/o2oTopUp
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objPerformO2OResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsPerformO2OResponse)(sRtnJSON, dtC)

            Return objPerformO2OResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallMerchantTxnHistory(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strAccountID As String, ByVal strStartDate As String, ByVal strEndDate As String, ByVal boolInternal As Boolean) As clsJSONBusinessObj.clsMerchantTxnHistoryResponse
        Return CallMerchantTxnHistory(strPolicyID, strLang, strProductID, strMerchantID, strAccountID, strStartDate, strEndDate, boolInternal, "")
    End Function
    Public Shared Function CallMerchantTxnHistory(ByVal strPolicyID As String, ByVal strLang As String, ByVal strProductID As String, ByVal strMerchantID As String, ByVal strAccountID As String, ByVal strStartDate As String, ByVal strEndDate As String, ByVal boolInternal As Boolean, Optional ByVal strKongAPIKey As String = "") As clsJSONBusinessObj.clsMerchantTxnHistoryResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("TransactionWSURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("MerchantTransactionHistoryFuncName")
            Dim kongAPIKey As String = IIf(Not String.IsNullOrEmpty(strKongAPIKey), strKongAPIKey, System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey"))

            Dim objMerchantTxnHistoryReq As New clsJSONBusinessObj.clsMerchantTxnHistoryRequest
            objMerchantTxnHistoryReq.policyId = strPolicyID
            objMerchantTxnHistoryReq.lang = strLang
            objMerchantTxnHistoryReq.merchantId = strMerchantID
            objMerchantTxnHistoryReq.productId = strProductID
            objMerchantTxnHistoryReq.accountId = strAccountID
            objMerchantTxnHistoryReq.startDate = strStartDate
            objMerchantTxnHistoryReq.endDate = strEndDate
            objMerchantTxnHistoryReq.internal = boolInternal

            Dim sJSON As String = JsonConvert.SerializeObject(objMerchantTxnHistoryReq)

            'transactionws/getTransactionHistory
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, kongAPIKey, True)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objMerchantTxnHistoryResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsMerchantTxnHistoryResponse)(sRtnJSON, dtC)

            Return objMerchantTxnHistoryResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'AL20210201 eService PI Access
    Public Shared Function CallPiAuthGenKey(ByVal strPiCustID As String, ByVal strUsr As String) As clsJSONBusinessObj.clsPiAuthResponse
        Try
            Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("PiAuthGenKeyURL")
            Dim func As String = System.Configuration.ConfigurationSettings.AppSettings.Item("PiAuthGenKeyFuncName")

            Dim objPiAuthReq As New clsJSONBusinessObj.clsPiAuthRequest
            objPiAuthReq.customerId = strPiCustID
            objPiAuthReq.ceUser = strUsr

            Dim sJSON As String = JsonConvert.SerializeObject(objPiAuthReq)

            'registration/backdoor/genKey
            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, url, func, "")

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim objPiAuthResp = JsonConvert.DeserializeObject(Of clsJSONBusinessObj.clsPiAuthResponse)(sRtnJSON, dtC)

            Return objPiAuthResp

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    
End Class
