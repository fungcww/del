Imports System.Net
Imports System.IO
Imports System.Configuration
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Public Class clsJSONTool

    Private Shared cstDateFormat As String = "yyyy-MM-dd"

    Public Shared Function CallWeb(ByVal sURL As String, ByVal sFileName As String) As String

        Dim strsb As String = String.Empty
        Dim sOutPutFile As String = String.Empty
        Dim stream1 As System.IO.Stream

        Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))
        myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
        System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))


        If Not String.IsNullOrEmpty(sURL) Then
            Try
                Dim Request As HttpWebRequest = WebRequest.Create(sURL) '(HttpWebRequest)

                Request.Method = "GET"
                'Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(sURL)
                'Request.ContentType = "text/html"

                'Request.ContentLength = bt.Length
                Request.Timeout = 60000
                Request.Proxy = myProxy

                'Dim st As Stream = Request.GetRequestStream()
                'st.Write(bt, 0, bt.Length)
                'st.Close()
                Using hresponse As HttpWebResponse = TryCast(Request.GetResponse(), HttpWebResponse)

                    If hresponse.StatusCode <> HttpStatusCode.OK Then
                        Throw New Exception([String].Format("Server error (HTTP {0}: {1}).", hresponse.StatusCode, hresponse.StatusDescription))
                    End If
                    stream1 = hresponse.GetResponseStream

                    'owebbrowser.DocumentStream = stream1

                    Dim sr As New StreamReader(stream1, System.Text.Encoding.UTF8)
                    strsb = sr.ReadToEnd()

                    'check temp\crs not exists then create folder
                    sOutPutFile = "c:\temp\crs\crs_" & sFileName & ".html"

                    File.WriteAllText(sOutPutFile, strsb)
                    'owebbrowser.DocumentText = strsb
                End Using
            Catch ex As Exception
                Throw ex
            End Try

        End If
        Return sOutPutFile
    End Function

    Public Shared Function ExcuteCall(ByVal serializedString As String, ByVal BaseAddress As String, ByVal ServiceCall As String, Optional ByVal ApiKey As String = "") As String

        Dim strsb As String = String.Empty
        Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))

        If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
            myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
            System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))
           
        End If

        If Not String.IsNullOrEmpty(BaseAddress) And Not String.IsNullOrEmpty(serializedString) And Not String.IsNullOrEmpty(ServiceCall) Then
            Try
                Dim Request As HttpWebRequest = WebRequest.Create(BaseAddress + ServiceCall) '(HttpWebRequest)

                If Not String.IsNullOrEmpty(ApiKey) Then
                    Request.Headers("Authorization") = ApiKey
                End If

                Request.Method = "POST"
                Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(serializedString)
                Request.ContentType = "application/json"
                Request.ContentLength = bt.Length
                Request.Timeout = 60000
                If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
                    Request.Proxy = myProxy
                End If
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
        Return strsb
    End Function

    Public Shared Function CallWSByCustID(ByVal strCustID As String) As clsJSONBusinessObj.clsSearchResponse
        Try

            Dim objCustomerSearch As New clsJSONBusinessObj.clsCustomerSearch
            objCustomerSearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objCustomerSearch.callerRequest.customerId = strCustID
            objCustomerSearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUSER")

            Dim sJSON As String = JsonConvert.SerializeObject(objCustomerSearch)


            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"), _
             System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSFuncName"), "")

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
        '"&option=crs&company=hk&userId=" & System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUSER") & _
        '"&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"
        sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL") & "gi/" & sType & "?policyId=" & sPolicyID & _
        "&option=crs&company=hk" & _
        "&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"

        'File.WriteAllText("c:\temp\sURL.txt", sURL)
        Return sURL
    End Function

    Public Shared Function GetEBDetailPageURL(ByVal sCustID As String, ByVal sPolicyID As String, ByVal sType As String) As String
        Dim sURL As String = String.Empty

        'sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL") & "eb/enquiry?policyId=" & sPolicyID & _
        '"&option=crs&type=" & sType & "&company=hk&userId=" & System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUSER") & _
        '"&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"
        sURL = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL") & "eb/enquiry?policyId=" & sPolicyID & _
        "&option=crs&type=" & sType & "&company=hk" & _
        "&customerId=" & sCustID & "&sessionKey=&lang=en&mode=gui"


        Return sURL
    End Function

    Public Shared Function CallGIPolicyWS(ByVal strPolicyID As String) As clsJSONBusinessObj.clsPolicySearchResponse
        Try

            Dim objPolicySearch As New clsJSONBusinessObj.clsPolicySearch
            objPolicySearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objPolicySearch.policyAccountId = strPolicyID
            objPolicySearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser")

            Dim sJSON As String = JsonConvert.SerializeObject(objPolicySearch)

            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"), _
             System.Configuration.ConfigurationSettings.AppSettings.Item("GIPolicyGroupWSFuncName"), "")

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
        Try

            Dim objPolicySearch As New clsJSONBusinessObj.clsPolicySearch
            objPolicySearch.callerRequest = New clsJSONBusinessObj.clsCallerRequest
            objPolicySearch.policyAccountId = strPolicyID
            objPolicySearch.callerRequest.userId = System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSUser")

            Dim sJSON As String = JsonConvert.SerializeObject(objPolicySearch)

            Dim sRtnJSON As String = clsJSONTool.ExcuteCall(sJSON, System.Configuration.ConfigurationSettings.AppSettings.Item("PolicyWSURL"), _
             System.Configuration.ConfigurationSettings.AppSettings.Item("GIEBPolicyGroupWSFuncName"), "")

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

End Class
