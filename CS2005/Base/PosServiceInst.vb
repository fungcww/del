Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports Newtonsoft.Json

''' <summary>
''' Get common POSWS instance
''' </summary>
''' <remarks>
''' <br>20241126 Lubin, CRS performer slowness</br>
''' </remarks>
Public Class PosServiceInst
    Private Const DEFAULT_COMPANY_ID = "ING"
    Private Shared ReadOnly _instDict As New Dictionary(Of String, POSWS.POSWS)
    Private Shared ReadOnly _lockObject As New Object()

    Private Function ComputeSHA256Hash(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim builder As New StringBuilder()
            For Each b As Byte In hash
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function

    Private Shared Function CaclKeyOfParams(companyId As String, dbHeader As POSWS.DBSOAPHeader, mqHeader As POSWS.MQSOAPHeader) As String
        Return $"{companyId},dbHeader={JsonConvert.SerializeObject(dbHeader)},mqHeader={JsonConvert.SerializeObject(mqHeader)}"
    End Function

    Private Shared Function GetServiceByPara(companyId As String, Optional dbHeader As POSWS.DBSOAPHeader = Nothing, Optional mqHeader As POSWS.MQSOAPHeader = Nothing) As POSWS.POSWS
        Dim wsPOS As POSWS.POSWS = Nothing
        Monitor.Enter(_lockObject)
        Try
            Console.WriteLine("Thread-safe code execution")

            Dim dictKey As String = CaclKeyOfParams(companyId, dbHeader, mqHeader)

            ' return existing instance by key (if any)
            If _instDict.ContainsKey(dictKey) Then Return _instDict(dictKey)

            ' instance not exist, new one
            wsPOS = InitService(dbHeader, mqHeader)

            _instDict(dictKey) = wsPOS
        Finally
            Monitor.Exit(_lockObject)
        End Try
        Return wsPOS
    End Function

    Private Shared Function InitService(dbHeader As POSWS.DBSOAPHeader, mqHeader As POSWS.MQSOAPHeader) As POSWS.POSWS
        Return New POSWS.POSWS With {
                .DBSOAPHeaderValue = dbHeader,
                .MQSOAPHeaderValue = mqHeader,
                .Credentials = Net.CredentialCache.DefaultCredentials,
                .Timeout = -1
            }
    End Function

    ''' <summary>
    ''' Get POSWS Instance by parameter or default
    ''' </summary>
    Public Shared Function GetService(Optional companyId As String = "", Optional dbHeader As POSWS.DBSOAPHeader = Nothing, Optional mqHeader As POSWS.MQSOAPHeader = Nothing) As POSWS.POSWS
        If String.IsNullOrEmpty(companyId) Then
            companyId = DEFAULT_COMPANY_ID
        End If
        Return GetServiceByPara(companyId, dbHeader, mqHeader)
    End Function

End Class
