Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Xml
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Linq

Public Class APICallHelper

    Private Const cstDateFormat As String = "yyyy-MM-dd"

    Class Response
        Public Property data As Object
        Public Property sucess As Boolean
    End Class

    Public Shared Function GetConnectionConfig(ByVal strAPIURL As String, ByVal configName As String, Optional ByVal xmlPath As String = "root/data/conn/") As String
        Try


            Dim sRtnJSON As String = ExcuteGetAPICall(strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim xmlnode As XmlNode = JsonConvert.DeserializeXmlNode(sRtnJSON, "root")

            Dim nodeList As XmlNode = xmlnode.SelectSingleNode(xmlPath & configName)

            Return nodeList.InnerText

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetConnectionConfigs(ByVal strAPIURL As String, ByVal configName As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Try

            Dim sRtnJSON As String = ExcuteGetAPICall(strAPIURL)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim xmlnode As XmlNode = JsonConvert.DeserializeXmlNode(sRtnJSON, "root")

            For Each child As XmlNode In xmlnode.ChildNodes
                If result.ContainsKey(child.Name) = False Then
                    result.Add(child.Name, child.InnerText)
                End If
            Next

            Return result

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function CallAPIWithDynamicResponse(ByVal strAPIURL As String, ByVal objQuery As Object) As DataSet
        Try
            Dim ds As DataSet = New DataSet
            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)

            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim jsonLinq As JContainer = JObject.Parse(sRtnJSON)
            ' Find the first array using Linq
            Dim srcArray = jsonLinq.Descendants().Where(Function(d) TypeOf d Is JArray).ToList
            For Each Item As JArray In srcArray
                Dim trgArray = New JArray()
                For Each row As JObject In Item.Children(Of JObject)()
                    Dim cleanRow = New JObject()
                    For Each column As JProperty In row.Properties()
                        ' Only include JValue types
                        If TypeOf column.Value Is JValue Then
                            cleanRow.Add(column.Name, column.Value)
                        Else
                            cleanRow.Add(column.Name, column.Value)
                        End If
                    Next
                    trgArray.Add(cleanRow)
                Next
                Dim dt = JsonConvert.DeserializeObject(Of DataTable)(trgArray.ToString())
                ds.Tables.Add(dt)
            Next

            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallAPIWithResponse(Of T)(ByVal strAPIURL As String, ByVal objQuery As Object) As T
        Try

            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)


            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim response = JsonConvert.DeserializeObject(Of T)(sRtnJSON, dtC)

            'Dim table = JsonConvert.DeserializeObject(Of Object)(sRtnJSON)

            Return response

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function CallAPIWithResponse(Of T)(ByVal strAPIURL As String, ByVal objQuery As Object, Optional ByVal ApiKey As String = "") As T
        Try

            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)

            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL, ApiKey)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim response = JsonConvert.DeserializeObject(Of T)(sRtnJSON, dtC)

            'Dim table = JsonConvert.DeserializeObject(Of Object)(sRtnJSON)

            Return response

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallGetAPIWithResponse(Of T)(ByVal strAPIURL As String) As T
        Try
            Dim sRtnJSON As String = ExcuteGetAPICall(strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim response = JsonConvert.DeserializeObject(Of T)(sRtnJSON, dtC)

            'Dim table = JsonConvert.DeserializeObject(Of Object)(sRtnJSON)

            Return response

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallAPIAplLoan(ByVal strAPIURL As String, ByVal objQuery As Object) As APIResponse(Of AppLoanResponse)
        Try

            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)


            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of APIResponse(Of AppLoanResponse))(sRtnJSON, dtC)

            Dim response As APIResponse(Of AppLoanResponse) = New APIResponse(Of AppLoanResponse)

            response = CType(sqlResponse, APIResponse(Of AppLoanResponse))

            'Dim table = JsonConvert.DeserializeObject(Of Object)(sRtnJSON)

            Return response

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallAPIWithPayload(ByVal strAPIURL As String, ByVal objQuery As Object) As DataTable
        Try

            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)


            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim table = JsonConvert.DeserializeObject(Of RootObject(Of DataTable))(sRtnJSON).data

            Return table

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function CallAPI(ByVal strSQL As String, ByVal strConn As String, ByVal strAPIURL As String, ByVal strPolicy As String) As DataTable
        Try

            Dim objQuery As New PolicyDetailQuery
            objQuery.PolicyAccountIds.Add(strPolicy)

            Dim sJSON As String = JsonConvert.SerializeObject(objQuery)


            Dim sRtnJSON As String = ExcuteAPICall(sJSON, strAPIURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim table = JsonConvert.DeserializeObject(Of RootObject(Of DataTable))(sRtnJSON).data

            Return table

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallCRSAPI(ByVal apiURL As String, ByVal generalQuery As GeneralQuery) As DataTable
        Try


            Dim sJSON As String = JsonConvert.SerializeObject(generalQuery)

            'giws/getGIPolicyGroupType
            Dim sRtnJSON As String = ExcuteAPICall(sJSON, apiURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim table = JsonConvert.DeserializeObject(Of RootObject(Of DataTable))(sRtnJSON).data

            Return table

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CallCRSAPI(ByVal query As String, ByVal connection As String, ByVal apiURL As String, ByVal parameters As Dictionary(Of String, Object), ByVal CredentialKey As String, Optional ByVal isEncrypted As Boolean = True) As DataTable
        Try

            Dim objSQL As New GeneralQuery
            objSQL.QueryString = query
            objSQL.ConnectionString = connection
            objSQL.Parameters = parameters
            objSQL.CredentialKey = CredentialKey
            objSQL.IsEncrypted = isEncrypted

            Dim sJSON As String = JsonConvert.SerializeObject(objSQL)

            'giws/getGIPolicyGroupType
            Dim sRtnJSON As String = ExcuteAPICall(sJSON, apiURL)

            'File.WriteAllText("c:\temp\json.txt", sRtnJSON)

            Dim dtC As New IsoDateTimeConverter()
            dtC.DateTimeFormat = cstDateFormat

            Dim sqlResponse = JsonConvert.DeserializeObject(Of Object)(sRtnJSON, dtC)

            Dim table = JsonConvert.DeserializeObject(Of RootObject(Of DataTable))(sRtnJSON).data

            Return table

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Class RootObject(Of T)
        Public Property data As T
        Public Property conn As T
    End Class

    Public Shared Function ExcuteAPICall(ByVal serializedString As String, ByVal BaseAddress As String, Optional ByVal ApiKey As String = "") As String

        Dim strsb As String = String.Empty
        'Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))

        'If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
        '    myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
        '    System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))

        'End If

        If Not String.IsNullOrEmpty(BaseAddress) And Not String.IsNullOrEmpty(serializedString) Then
            Try
                Dim Request As HttpWebRequest = WebRequest.Create(BaseAddress) '(HttpWebRequest)

                If Not String.IsNullOrEmpty(ApiKey) Then
                    Request.Headers("apiKey") = ApiKey
                    Request.Headers("Authorization") = ApiKey
                End If

                Request.Method = "POST"
                Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(serializedString)
                Request.ContentType = "application/json"
                Request.ContentLength = bt.Length
                Request.Timeout = 300000
                Request.Credentials = System.Net.CredentialCache.DefaultCredentials

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
        Return strsb
    End Function

    Public Shared Function ExcuteGetAPICall(ByVal BaseAddress As String, Optional ByVal ApiKey As String = "") As String

        Dim strsb As String = String.Empty
        'Dim myProxy As New WebProxy(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyURL"))

        'If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
        '    myProxy.Credentials = New NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyUser"), _
        '    System.Configuration.ConfigurationSettings.AppSettings.Item("ProxyPwd"))

        'End If

        If Not String.IsNullOrEmpty(BaseAddress) Then
            Try
                Dim Request As HttpWebRequest = WebRequest.Create(BaseAddress) '(HttpWebRequest)

                If Not String.IsNullOrEmpty(ApiKey) Then
                    Request.Headers("Authorization") = ApiKey
                End If

                Request.Method = "Get"

                Request.ContentType = "application/json"

                Request.Timeout = 300000
                'If System.Configuration.ConfigurationSettings.AppSettings.Item("UseProxy") = "1" Then
                '    Request.Proxy = myProxy
                'End If

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

    ''' <summary>
    ''' Convert the business api response to DataSet without data model
    ''' </summary>
    ''' <param name="resultRsp">Original response result</param>
    ''' <returns>DataSet</returns>
    Private Shared Function BusiRspToDataset(ByVal resultRsp As JObject) As DataSet
        Dim dataArrObj As JObject = resultRsp.ToObject(Of APIResponse(Of JObject)).data
        Dim ds As DataSet = dataArrObj.ToObject(Of DataSet)()
        'Dim ds As New DataSet()
        'For Each dtObj As JProperty In dataArrObj.Properties
        '    ds.Tables.Add(dtObj.ToObject(Of DataTable)())
        'Next
        Return ds
    End Function

    ''' <summary>
    ''' Covert the result, and process the blob column from base64
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="blobDef">blob column names</param>
    ''' <param name="colName">Column name</param>
    ''' <returns>true or false</returns>
    Private Shared Function IsBlobCol(blobDef As String, colName As String) As Boolean
        Return ("," & blobDef & ",").IndexOf("," & colName & ",") <> -1
    End Function
    ''' <summary>
    ''' Covert the result, and process the blob column from base64
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="ds">fill dataset</param>
    ''' <param name="blobDef">blob column names</param>
    ''' <returns>Dataset</returns>
    Private Shared Function ConvertBlobDs(ds As DataSet, blobDef As String) As DataSet
        If String.IsNullOrEmpty(blobDef) Then Return ds

        Dim destDs As DataSet = ds.Clone()
        For Each ddt As DataTable In destDs.Tables
            Dim isFoundBlob As Boolean = False
            'Not sure about the Contains is ignore case, so use the foreach.
            For Each col As DataColumn In ddt.Columns
                If IsBlobCol(blobDef, col.ColumnName) Then
                    col.DataType = GetType(System.Byte())
                    isFoundBlob = True
                End If
            Next
            If isFoundBlob Then
                For Each dr As DataRow In ds.Tables(ddt.TableName).Rows
                    Dim newDr As DataRow = ddt.NewRow
                    For Each col As DataColumn In ddt.Columns

                        If IsBlobCol(blobDef, col.ColumnName) Then
                            col.DataType = GetType(System.Byte())
                            If Not IsDBNull(dr.Item(col.ColumnName)) AndAlso Not IsNothing(dr.Item(col.ColumnName)) Then
                                newDr.Item(col.ColumnName) = Convert.FromBase64String(dr.Item(col.ColumnName))
                            End If
                        Else
                            newDr.Item(col.ColumnName) = dr.Item(col.ColumnName)
                        End If
                    Next
                    ddt.Rows.Add(newDr)
                    'ddt.ImportRow(dr)
                Next
            Else
                ddt = ds.Tables(ddt.TableName).Copy
            End If
        Next
        Return destDs
    End Function


#Region "unpackage dataset"
    Private Shared Function EtractBytesFormStream(ByVal zipStream As Stream, ByVal dataBlock As Integer) As Byte()
        Try
            Dim data As Byte() = Nothing
            Dim totalBytesRead As Integer = 0

            While True
                Array.Resize(data, totalBytesRead + dataBlock + 1)
                Dim bytesRead As Integer = zipStream.Read(data, totalBytesRead, dataBlock)

                If bytesRead = 0 Then
                    Exit While
                End If

                totalBytesRead += bytesRead
            End While

            Array.Resize(data, totalBytesRead)
            Return data
        Catch
            Return Nothing
        End Try
    End Function



    Private Shared Function Decompress(ByVal data As Byte()) As Byte()
        Try
            Dim ms As MemoryStream = New MemoryStream(data)
            Dim zipStream As Stream = Nothing
            zipStream = New GZipStream(ms, CompressionMode.Decompress)
            Dim dc_data As Byte() = Nothing
            dc_data = EtractBytesFormStream(zipStream, data.Length)
            Return dc_data
        Catch
            Return Nothing
        End Try
    End Function


    Private Shared Function StringToByteArray(serializedString As String) As Byte()
        Dim numberChars As Integer = serializedString.Length
        Dim serializedData(numberChars / 2) As Byte
        For i As Integer = 0 To numberChars - 2 Step 2
            serializedData(i / 2) = Convert.ToByte(serializedString.Substring(i, 2), 16)
        Next
        Return serializedData
    End Function

    ''' <remarks>
    ''' <br>20241224 Chrysan Cheng, Fix serialize DataSet issue - Change to use DataContractSerializer class</br>
    ''' </remarks>
    Private Shared Function GetDatasetFromByteStr(ByVal byteStr As String) As DataSet
        Dim buffer As Byte() = Decompress(StringToByteArray(byteStr))

        ' Change to use DataContractSerializer class
        'Dim ser As BinaryFormatter = New BinaryFormatter()
        'Dim ser As New XmlSerializer(GetType(DataSet))
        'Dim dss As DataSet = CType(ser.Deserialize(New MemoryStream(buffer)), DataSet)
        Dim ser As New DataContractSerializer(GetType(DataSet))
        Dim dss As DataSet = CType(ser.ReadObject(New MemoryStream(buffer)), DataSet)

        Return dss
    End Function
#End Region


    ''' <summary>
    ''' Convert the company name to a format accepted by the CRSAPI.
    ''' </summary>
    ''' <param name="companyName">Original company ID, e.g. ING</param>
    Public Shared Function ConvertCompanyNameForCRSAPI(companyName As String) As String
        Return If(companyName.ToUpper.Equals("LAC") OrElse companyName.ToUpper.Equals("LAH") OrElse companyName.ToUpper.Equals("BMU"),
                  companyName.ToUpper,
                  If(companyName.ToUpper.Equals("MCU") OrElse companyName.ToUpper.Equals("MC"), "MC", "HK"))
    End Function

    ''' <summary>
    ''' Call API business
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="apiUrlFormat">API Url</param>
    ''' <param name="companyName">Company Name</param>
    ''' <param name="busiId">Business Id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Dataset of result</returns>
    Public Shared Function CallAPIBusi(apiUrlFormat As String, companyName As String, busiId As String, queryDict As Dictionary(Of String, String)) As DataSet
        Try
            Dim finalCompanyName As String = ConvertCompanyNameForCRSAPI(companyName)

            Dim apiUrl As String = String.Format(apiUrlFormat, finalCompanyName, busiId)

            Dim responseObject As JObject = CallAPIWithResponse(Of JObject)(apiUrl, queryDict)
            Dim retStatus As JToken = responseObject.GetValue("success")

            'Dim isSucc As Boolean = retStatus.Value(Of Boolean)
            Dim isSucc As Boolean = If(retStatus IsNot Nothing, retStatus.ToObject(Of Boolean)(), False)
            If Not isSucc Then Throw New Exception(responseObject.GetValue("error").ToString)
            Dim dataRet As JObject = responseObject.GetValue("data")
            Dim ds As DataSet = GetDatasetFromByteStr(dataRet.GetValue("SearchResult").ToString())
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Call async business API
    ''' </summary>
    ''' <param name="apiUrlFormat">API Url</param>
    ''' <param name="companyName">Company Name</param>
    ''' <param name="busiType">Business Type</param>
    ''' <param name="busiId">Business ID</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Dataset of result</returns>
    Public Shared Function CallAsyncBusiAPI(apiUrlFormat As String, companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String)) As DataSet
        Dim finalCompanyName As String = ConvertCompanyNameForCRSAPI(companyName)

        Dim apiUrl As String = String.Format(apiUrlFormat, finalCompanyName, busiType, busiId)

        Dim responseObject As JObject = CallAPIWithResponse(Of JObject)(apiUrl, queryDict)

        Dim retStatus As JToken = responseObject.GetValue("success")
        'Dim isSucc As Boolean = retStatus.Value(Of Boolean)
        Dim isSucc As Boolean = If(retStatus IsNot Nothing, retStatus.ToObject(Of Boolean)(), False)

        If Not isSucc Then Throw New Exception(responseObject.GetValue("error").ToString)

        Dim dataVal As JValue = responseObject.GetValue("data")
        'Dim ds As DataSet = GetDatasetFromByteStr(dataVal.Value(Of String))
        Dim ds As DataSet = GetDatasetFromByteStr(If(dataVal IsNot Nothing, dataVal.ToObject(Of String)(), String.Empty))

        Return ds
    End Function

    ''' <summary>
    ''' Call CRS restful format api
    ''' </summary>
    ''' <param name="apiUrlFormat">API Url</param>
    ''' <param name="companyName">Company Name</param>
    ''' <param name="apiSubUrl">The sub api url string</param>
    ''' <param name="objBody">The body data model instance</param>
    ''' <returns>Return data model instance of <typeparamref name="T"/></returns>
    Public Shared Function CallCRSRestfulAPI(Of T)(apiUrlFormat As String, companyName As String, apiSubUrl As String, objBody As Object) As T
        Dim finalCompanyName As String = ConvertCompanyNameForCRSAPI(companyName)

        Dim apiUrl As String = String.Format(apiUrlFormat, apiSubUrl, finalCompanyName)

        Dim responseObject As APIResponse(Of T) = CallAPIWithResponse(Of APIResponse(Of T))(apiUrl, objBody)

        If Not responseObject.success Then Throw New Exception(responseObject.message)

        Return responseObject.data
    End Function

    ''' <summary>
    ''' Call business's search API
    ''' </summary>
    ''' <typeparam name="T">The data model class</typeparam>
    ''' <param name="apiUrlFormat">API Url</param>
    ''' <param name="companyName">Company Name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Return data model instance of <typeparamref name="T"/></returns>
    Public Shared Function CallSearchAPIWithObject(Of T)(apiUrlFormat As String, companyName As String, busiType As String, busiId As String,
                                                         queryDict As Dictionary(Of String, String)) As T
        Dim finalCompanyName As String = ConvertCompanyNameForCRSAPI(companyName)

        Dim apiUrl As String = String.Format(apiUrlFormat, busiType, busiId, finalCompanyName)

        Dim responseObject As APIResponse(Of T) = CallAPIWithResponse(Of APIResponse(Of T))(apiUrl, queryDict)

        If Not responseObject.success Then Throw New Exception(responseObject.message)

        Return responseObject.data
    End Function

    ''' <summary>
    ''' Call business's search API
    ''' </summary>
    ''' <param name="apiUrlFormat">API Url</param>
    ''' <param name="companyName">Company Name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Dataset of result</returns>
    Public Shared Function CallSearchAPI(Of T)(apiUrlFormat As String, companyName As String, busiType As String, busiId As String,
                                               queryDict As Dictionary(Of String, String)) As DataSet

        Return EnvironmentUtility.ConvertToDataSet(CallSearchAPIWithObject(Of T)(apiUrlFormat, companyName, busiType, busiId, queryDict))
    End Function


    ''' <summary>
    ''' SMS revamp (CRS Enhancement) Call Enquire SMS API to Display SMS information
    ''' Call the CallUtilityAPIWithResponse method and deserialize responseJObject to dataSet
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-6 by oliver</br>
    ''' </remarks>
    ''' <param name="apiUrlFormat">Represents the ApiUrlFormat is the request URL</param>
    ''' <param name="versionNumber">Represents the VersionNumber is one of the parameters that make up the request URL</param>
    ''' <param name="apiName">Represents the ApiName is one of the parameters that make up the request URL</param>
    ''' <param name="apiKey">Represents the APIKEY is one of the Header parameter to Request UtilityAPI</param>
    ''' <param name="tableName">Represents the TableName of the Response form</param>
    ''' <param name="queryDict">Represents the Dictionary is one of the parameter to Request UtilityAPI</param>
    ''' <returns>The returned DataSet represents the Response of Request UtilityAPI</returns>
    Public Shared Function CallUtilityAPI(apiUrlFormat As String, versionNumber As String, apiName As String, apiKey As String, tableName As String, queryDict As Dictionary(Of String, Object)) As DataSet
        Try
            Dim apiUrl As String = String.Format(apiUrlFormat, versionNumber, apiName)

            Dim responseJObject As JObject = CallUtilityAPIWithResponse(Of JObject)(apiUrl, queryDict, apiKey)
            Dim resultJToken As JToken = responseJObject.GetValue("result")

            'Dim isSuccess As Boolean = resultJToken.Value(Of Boolean)
            Dim isSuccess As Boolean = If(resultJToken IsNot Nothing, resultJToken.ToObject(Of Boolean)(), False)

            If Not isSuccess Then Throw New Exception(responseJObject.GetValue("messageEn").ToString)

            Dim dataSet As DataSet = New DataSet
            Dim responseData As JToken = responseJObject.GetValue("responseData")

            Dim dataTable As DataTable = JsonConvert.DeserializeObject(Of DataTable)(responseData.ToString())
            dataTable.TableName = tableName
            dataSet.Tables.Add(dataTable)
            Return dataSet

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' SMS revamp (CRS Enhancement) Call Send SMS API to Send SMS
    ''' Call the CallUtilityAPIWithResponse method 
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-6 by oliver</br>
    ''' </remarks>
    ''' <param name="apiUrlFormat">Represents the ApiUrlFormat is the request URL</param>
    ''' <param name="versionNumber">Represents the VersionNumber is one of the parameters that make up the request URL</param>
    ''' <param name="apiName">Represents the ApiName is one of the parameters that make up the request URL</param>
    ''' <param name="apiKey">Represents the APIKEY is one of the Header parameter to Request UtilityAPI</param>
    ''' <param name="tableName">Represents the TableName of the Response form</param>
    ''' <param name="queryDict">Represents the Dictionary is one of the parameter to Request UtilityAPI</param>
    Public Shared Sub ExcuteUtilityAPI(apiUrlFormat As String, versionNumber As String, apiName As String, apiKey As String, tableName As String, queryDict As Dictionary(Of String, Object))
        Try
            Dim apiUrl As String = String.Format(apiUrlFormat, versionNumber, apiName)

            Dim responseJObject As JObject = CallUtilityAPIWithResponse(Of JObject)(apiUrl, queryDict, apiKey)
            Dim resultJToken As JToken = responseJObject.GetValue("result")

            'Dim isSuccess As Boolean = resultJToken.Value(Of Boolean)
            Dim isSuccess As Boolean = If(resultJToken IsNot Nothing, resultJToken.ToObject(Of Boolean)(), False)

            If Not isSuccess Then Throw New Exception(responseJObject.GetValue("messageEn").ToString)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' SMS revamp (CRS Enhancement)
    ''' Call the ExcuteUtility method and deserialize the type of responseString to T
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-6 by oliver</br>
    ''' </remarks>
    ''' <typeparam name="T">Represents the JSON string being deserialized Type is T</typeparam>
    ''' <param name="apiUrl">Represents the ApiUrl is the request URL</param>
    ''' <param name="queryObject">Represents the Object is one of the parameter to Request UtilityAPI</param>
    ''' <param name="apiKey">Represents the APIKEY is one of the Header parameter to Request UtilityAPI</param>
    ''' <returns>The returned T represents the Response which was deserialized Type of Request UtilityAPI</returns>
    Public Shared Function CallUtilityAPIWithResponse(Of T)(ByVal apiUrl As String, ByVal queryObject As Object, ByVal apiKey As String) As T
        Try

            Dim queryString As String = JsonConvert.SerializeObject(queryObject)
            Dim responseString As String = UtilityAPI(apiUrl, queryString, apiKey)

            Dim dateTimeConverter As New IsoDateTimeConverter()
            dateTimeConverter.DateTimeFormat = cstDateFormat
            Dim responseJObject = JsonConvert.DeserializeObject(Of T)(responseString, dateTimeConverter)

            Return responseJObject

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' SMS revamp (CRS Enhancement)
    ''' Request API with one Header which name is ApiKey
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-6 by oliver</br>
    ''' </remarks>
    ''' <param name="apiUrl">Represents the ApiUrl is the request URL</param>
    ''' <param name="queryString">Represents the queryString is one of the parameter to Request UtilityAPI</param>
    ''' <param name="apiKey">Represents the APIKEY is one of the Header parameter to Request UtilityAPI</param>
    ''' <returns>The returned string represents the Response of Request UtilityAPI</returns>
    Public Shared Function UtilityAPI(ByVal apiUrl As String, ByVal queryString As String, Optional ByVal apiKey As String = "") As String

        Dim responseString As String = String.Empty

        If Not String.IsNullOrEmpty(apiUrl) And Not String.IsNullOrEmpty(queryString) Then
            Try
                Dim httpWebRequest As HttpWebRequest = WebRequest.Create(apiUrl)

                If Not String.IsNullOrEmpty(apiKey) Then
                    httpWebRequest.Headers("ApiKey") = apiKey
                End If

                httpWebRequest.Method = "POST"
                Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(queryString)
                httpWebRequest.ContentType = "application/json"
                httpWebRequest.ContentLength = bt.Length
                httpWebRequest.Timeout = 300000
                httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials

                Dim requestStream As Stream = httpWebRequest.GetRequestStream()
                requestStream.Write(bt, 0, bt.Length)
                requestStream.Close()
                Using httpResponse As HttpWebResponse = TryCast(httpWebRequest.GetResponse(), HttpWebResponse)

                    If httpResponse.StatusCode <> HttpStatusCode.OK Then
                        Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", httpResponse.StatusCode, httpResponse.StatusDescription))
                    End If
                    Dim responseStream As Stream = httpResponse.GetResponseStream()
                    Dim streamReader As New StreamReader(responseStream)
                    responseString = streamReader.ReadToEnd()

                End Using
            Catch ex As WebException
                Using httpResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)

                    If httpResponse.StatusCode = HttpStatusCode.BadRequest Then
                        Dim responseStream As Stream = httpResponse.GetResponseStream()
                        Dim streamReader As New StreamReader(responseStream)
                        responseString = streamReader.ReadToEnd()
                        Return responseString
                    End If

                End Using
                Throw
            Catch ex As Exception
                Throw
            End Try

        End If
        Return responseString
    End Function

End Class
