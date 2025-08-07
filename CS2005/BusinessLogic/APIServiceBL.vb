Imports System.Threading.Tasks

''' <summary>
''' API Serive BLL.
''' </summary>
''' <remarks>
''' <br>20221101 Lubin Zheng, Created/Move from HK Report logics to Macau.</br>
''' <br>20250310 Chrysan Cheng, CRS performance tuning - Add async function</br>
''' </remarks>
Public Class APIServiceBL

    ''' <summary>
    ''' Call business api
    ''' Lubin 2022-11-01 Created/Move from HK Report logics to Macau.
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Dataset</returns>
    Public Shared Function CallAPIBusi(companyName As String, busiId As String, queryDict As Dictionary(Of String, String), Optional endPoint As String = "") As DataSet
        Dim apiBaseUrl As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")
        If Not String.IsNullOrEmpty(endPoint) Then apiBaseUrl = endPoint
        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/report/Business/{0}/{1}"

        Return CRS_Component.APICallHelper.CallAPIBusi(apiUrlFormat, companyName, busiId, queryDict)
    End Function

    ''' <summary>
    ''' Call business api in async
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Task</returns>
    Public Shared Function CallAPIBusiAsync(companyName As String, busiId As String, queryDict As Dictionary(Of String, String), Optional endPoint As String = "") As Task(Of DataSet)
        Return Task.Factory.StartNew(Function() CallAPIBusi(companyName, busiId, queryDict, endPoint))
    End Function

    ''' <summary>
    ''' Execute business api
    ''' Lubin 2022-11-01 Created/Move from HK Report logics to Macau.
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Dataset</returns>
    Public Shared Function ExecAPIBusi(companyName As String, busiId As String, queryDict As Dictionary(Of String, String)) As DataSet
        Dim apiBaseUrl As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")
        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/report/Execute/{0}/{1}"

        Return CRS_Component.APICallHelper.CallAPIBusi(apiUrlFormat, companyName, busiId, queryDict)
    End Function

    ''' <summary>
    ''' Execute business api in async
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Task</returns>
    Public Shared Function ExecAPIBusiAsync(companyName As String, busiId As String, queryDict As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Task.Factory.StartNew(Function() ExecAPIBusi(companyName, busiId, queryDict))
    End Function

    ''' <summary>
    ''' Call "Async/Business" api
    ''' </summary>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Async api business type (API business Class)</param>
    ''' <param name="busiId">Async api business function name (Function name of API business Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="endPoint">User-defined api base URL</param>
    ''' <returns>Dataset</returns>
    Public Shared Function CallAsyncBusiAPI(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String),
                                            Optional endPoint As String = "") As DataSet
        Dim apiBaseUrl As String = endPoint
        If String.IsNullOrEmpty(apiBaseUrl) Then apiBaseUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/Async/Business/{0}/{1}/{2}"

        Return CRS_Component.APICallHelper.CallAsyncBusiAPI(apiUrlFormat, companyName, busiType, busiId, queryDict)
    End Function

    ''' <summary>
    ''' Call CRS restful format api
    ''' </summary>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="apiSubUrl">The sub api url string</param>
    ''' <param name="objBody">The body data model instance</param>
    ''' <param name="endPoint">User-defined api base URL</param>
    ''' <returns>Return data model instance of <typeparamref name="T"/></returns>
    Public Shared Function CallCRSRestfulAPI(Of T)(companyName As String, apiSubUrl As String, objBody As Object, Optional endPoint As String = "") As T
        Dim apiBaseUrl As String = endPoint
        If String.IsNullOrEmpty(apiBaseUrl) Then apiBaseUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/{0}/{1}"

        Return CRS_Component.APICallHelper.CallCRSRestfulAPI(Of T)(apiUrlFormat, companyName, apiSubUrl, objBody)
    End Function

    ''' <summary>
    ''' Call "[Business]/search" api
    ''' </summary>
    ''' <typeparam name="T">The model class</typeparam>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="endPoint">User-defined api base URL</param>
    ''' <returns>Return model instance of <typeparamref name="T"/></returns>
    Public Shared Function CallSearchAPIWithObject(Of T)(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String),
                                               Optional endPoint As String = "") As T
        Dim apiBaseUrl As String = endPoint
        If String.IsNullOrEmpty(apiBaseUrl) Then apiBaseUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/{0}/{1}/{2}"

        Return CRS_Component.APICallHelper.CallSearchAPIWithObject(Of T)(apiUrlFormat, companyName, busiType, busiId, queryDict)
    End Function

    ''' <summary>
    ''' Call "[Business]/search" api
    ''' </summary>
    ''' <typeparam name="T">The model class</typeparam>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="endPoint">User-defined api base URL</param>
    ''' <returns>Dataset</returns>
    Public Shared Function CallSearchAPI(Of T)(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String),
                                               Optional endPoint As String = "") As DataSet
        Dim apiBaseUrl As String = endPoint
        If String.IsNullOrEmpty(apiBaseUrl) Then apiBaseUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "APIEndPoint")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl &= "/"
        Dim apiUrlFormat As String = apiBaseUrl & "api/{0}/{1}/{2}"

        Return CRS_Component.APICallHelper.CallSearchAPI(Of T)(apiUrlFormat, companyName, busiType, busiId, queryDict)
    End Function

    ''' <summary>
    ''' Call "[Business]/search" api in async
    ''' </summary>
    ''' <typeparam name="T">The model class</typeparam>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="endPoint">User-defined api base URL</param>
    ''' <returns>Task</returns>
    Public Shared Function CallSearchAPIAsync(Of T)(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String),
                                                    Optional endPoint As String = "") As Task(Of DataSet)
        Return Task.Factory.StartNew(Function() CallSearchAPI(Of T)(companyName, busiType, busiId, queryDict, endPoint))
    End Function

    ''' <summary>
    ''' Call "[Business]/search" api return first table
    ''' </summary>
    ''' <typeparam name="T">The model class</typeparam>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>DataTable</returns>
    Public Shared Function QueryFirstTableWithSearchAPI(Of T)(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String)) As DataTable
        Return CallSearchAPI(Of T)(companyName, busiType, busiId, queryDict).Tables(0).Copy
    End Function

    ''' <summary>
    ''' Call "[Business]/search" api return first table in async
    ''' </summary>
    ''' <typeparam name="T">The model class</typeparam>
    ''' <param name="companyName">Company name/ID</param>
    ''' <param name="busiType">Search api business type (API controller Class)</param>
    ''' <param name="busiId">Search api business function name (API function of API controller Class)</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Task</returns>
    Public Shared Function QueryFirstTableWithSearchAPIAsync(Of T)(companyName As String, busiType As String, busiId As String, queryDict As Dictionary(Of String, String)) As Task(Of DataTable)
        Return Task.Factory.StartNew(Function() QueryFirstTableWithSearchAPI(Of T)(companyName, busiType, busiId, queryDict))
    End Function

    ''' <summary>
    ''' Call business api return Scalar value
    ''' Lubin 2022-11-01 Created/Move from HK Report logics to Macau.
    ''' </summary>
    ''' <typeparam name="T">return type</typeparam>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="defaultVal">Default value</param>
    ''' <param name="isPopErr">if true error messagebox ,else throw the message </param>
    ''' <returns>value</returns>
    Public Shared Function QueryScalar(Of T)(companyName As String, busiId As String,
                                             queryDict As Dictionary(Of String, String), defaultVal As T, Optional isPopErr As Boolean = True) As T
        Dim retVal As T = defaultVal
        Dim retDs As New DataSet
        Try
            retDs = CallAPIBusi(companyName, busiId, queryDict)
            If retDs.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(retDs.Tables(0).Rows(0)(0)) Then
                retVal = retDs.Tables(0).Rows(0)(0)
            End If
        Catch ex As Exception
            If isPopErr Then
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Else
                Throw
            End If
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Call business api return first row
    ''' Lubin 2022-11-01 Created/Move from HK Report logics to Macau.
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="isPopErr">if true error messagebox ,else throw the message </param>
    ''' <returns>value</returns>
    Public Shared Function QueryFirstRow(companyName As String, busiId As String,
                                            queryDict As Dictionary(Of String, String), Optional isPopErr As Boolean = True) As DataRow
        Dim retVal As DataRow
        Dim retDs As New DataSet
        Try
            retDs = CallAPIBusi(companyName, busiId, queryDict)
            If retDs.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(retDs.Tables(0).Rows(0)(0)) Then
                retVal = retDs.Tables(0).Rows(0)
            End If
        Catch ex As Exception
            If isPopErr Then
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Else
                Throw
            End If
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Call business api return first Table
    ''' Lubin 2022-11-01 Created/Move from HK Report logics to Macau.
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <param name="isPopErr">if true error messagebox ,else throw the message </param>
    ''' <returns>value</returns>
    Public Shared Function QueryFirstTable(companyName As String, busiId As String, queryDict As Dictionary(Of String, String),
                                           Optional isPopErr As Boolean = False) As DataTable
        Dim retVal As New DataTable
        Try
            Dim retDs As DataSet = CallAPIBusi(companyName, busiId, queryDict)
            retVal = retDs.Tables(0).Copy
        Catch ex As Exception When isPopErr
            MsgBox("Exception: " & ex.Message, MsgBoxStyle.Exclamation, gSystem)
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Call business api return first table in async
    ''' </summary>
    ''' <param name="companyName">Company name</param>
    ''' <param name="busiId">Business id</param>
    ''' <param name="queryDict">Query Dictionary</param>
    ''' <returns>Task</returns>
    Public Shared Function QueryFirstTableAsync(companyName As String, busiId As String, queryDict As Dictionary(Of String, String)) As Task(Of DataTable)
        Return Task.Factory.StartNew(Function() QueryFirstTable(companyName, busiId, queryDict))
    End Function

    ''' <summary>
    ''' SMS revamp (CRS Enhancement) Call Enquire SMS API to Display SMS information
    ''' Call UtilityAPI SMS
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-7 by oliver</br>
    ''' </remarks>
    ''' <param name="versionNumber">Represents the VersionNumber is one of the parameters that make up the request URL</param>
    ''' <param name="apiName">Represents the ApiName is one of the parameters that make up the request URL</param>
    ''' <param name="tableName">Represents the TableName of the Response form</param>
    ''' <param name="queryDict">Represents the Dictionary is one of the parameter to Request UtilityAPI SMS</param>
    ''' <returns>The returned DataSet represents the Response of Request UtilityAPI SMS</returns>
    Public Shared Function CallUtilityAPISMS(versionNumber As String, apiName As String, tableName As String, queryDict As Dictionary(Of String, Object)) As DataSet
        Dim apiBaseUrl As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UtilityApiEndPoint")
        Dim apiKey As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UtilityApiKey")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl = apiBaseUrl + "/"
        Dim apiUrlFormat As String = apiBaseUrl + "{0}/sms/{1}"

        Return CRS_Component.APICallHelper.CallUtilityAPI(apiUrlFormat, versionNumber, apiName, apiKey, tableName, queryDict)
    End Function

    ''' <summary>
    ''' SMS revamp (CRS Enhancement) Call Send SMS API to Send SMS
    ''' Excute UtilityAPI SMS
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-7 by oliver</br>
    ''' </remarks>
    ''' <param name="versionNumber">Represents the VersionNumber is one of the parameters that make up the request URL</param>
    ''' <param name="apiName">Represents the ApiName is one of the parameters that make up the request URL</param>
    ''' <param name="tableName">Represents the TableName of the Response form</param>
    ''' <param name="queryDict">Represents the Dictionary is one of the parameter to Request UtilityAPI SMS</param>
    Public Shared Sub ExcuteUtilityAPISMS(versionNumber As String, apiName As String, tableName As String, queryDict As Dictionary(Of String, Object))
        Dim apiBaseUrl As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UtilityApiEndPoint")
        Dim apiKey As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UtilityApiKey")

        If Not apiBaseUrl.EndsWith("/") Then apiBaseUrl = apiBaseUrl + "/"
        Dim apiUrlFormat As String = apiBaseUrl + "{0}/sms/{1}"

        CRS_Component.APICallHelper.ExcuteUtilityAPI(apiUrlFormat, versionNumber, apiName, apiKey, tableName, queryDict)
    End Sub

End Class
