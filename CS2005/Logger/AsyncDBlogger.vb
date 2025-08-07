Imports System.Collections.Concurrent
Imports System.Net
Imports System.Threading

''' <summary>
''' CRS APP Async Logger.
''' </summary>
''' <remarks>
''' <br>20250124 Vincent Yip, Created.</br>
''' <br>20250422 Chrysan Cheng, Improve CRS APP Log function.</br>
''' </remarks>
Public Class AsyncDbLogger
    Private Shared ReadOnly _instance As New AsyncDbLogger()

    Private ReadOnly _logQueue As New ConcurrentQueue(Of AsyncDbLoggerItem)()
    Private ReadOnly _cts As New CancellationTokenSource()
    Private ReadOnly _logThread As Thread
    Private ReadOnly _clientIP As String

    Private Sub New()
        ' Capture the client's IP address once startup
        _clientIP = GetClientIpAddress()

        ' Start the logging thread
        _logThread = New Thread(AddressOf ProcessLogQueue) With {
            .IsBackground = True
        }

        _logThread.Start(_cts.Token)
    End Sub

    ''' <summary>
    ''' Get AsyncDbLogger Singleton Instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As AsyncDbLogger
        Get
            Return _instance
        End Get
    End Property

#Region "Static Log Function"

    ''' <summary>
    ''' Log an "INFO" message
    ''' </summary>
    ''' <param name="CalUserId">The user ID.</param>
    ''' <param name="CalFunction">The function/action name you want to log.</param>
    ''' <param name="CalDetails">The log details(if any).</param>
    ''' <param name="CalTime">The log time(default now).</param>
    Public Shared Sub LogInfo(CalUserId As String, CalFunction As String, Optional CalDetails As String = "", Optional CalTime As Date = Nothing)
        LogMessage("INFO", CalUserId, CalFunction, CalDetails, CalTime:=CalTime)
    End Sub

    ''' <summary>
    ''' Log an "ERROR" message
    ''' </summary>
    ''' <param name="CalUserId">The user ID.</param>
    ''' <param name="CalFunction">The function/action name you want to log.</param>
    ''' <param name="CalDetails">The log details(if any).</param>
    ''' <param name="CalErrorMessage">The error message(if any).</param>
    ''' <param name="CalTime">The log time(default now).</param>
    Public Shared Sub LogError(CalUserId As String, CalFunction As String, Optional CalDetails As String = "", Optional CalErrorMessage As String = "",
                               Optional CalTime As Date = Nothing)
        LogMessage("ERROR", CalUserId, CalFunction, CalDetails, CalErrorMessage, CalTime)
    End Sub

    ''' <summary>
    ''' Log a message
    ''' </summary>
    ''' <param name="CalType">The call type, e.g. [INFO,ERROR].</param>
    ''' <param name="CalUserId">The user ID.</param>
    ''' <param name="CalFunction">The function/action name you want to log.</param>
    ''' <param name="CalDetails">The log details(if any).</param>
    ''' <param name="CalErrorMessage">The error message(if any).</param>
    ''' <param name="CalTime">The log time(default now).</param>
    Public Shared Sub LogMessage(CalType As String, CalUserId As String, CalFunction As String, Optional CalDetails As String = "",
                                 Optional CalErrorMessage As String = "", Optional CalTime As Date = Nothing)
        Instance.LogMessage(CalType, CalUserId, CalFunction, If(CalTime = Nothing, Now, CalTime), CalDetails, CalErrorMessage, If(g_Comp, "ING"))
    End Sub

#End Region

    ''' <summary>
    ''' Add a log to queue
    ''' </summary>
    ''' <param name="CalType">The call type, e.g. [INFO,ERROR].</param>
    ''' <param name="CalUserId">The user ID.</param>
    ''' <param name="CalFunction">The function/action name you want to log.</param>
    ''' <param name="CalTime">The log time.</param>
    ''' <param name="CalDetails">The log details(if any).</param>
    ''' <param name="CalErrorMessage">The error message(if any).</param>
    ''' <param name="CalCompany">The company current using, e.g. [ING,MCU,LAC,LAH,BMU]</param>
    Public Sub LogMessage(CalType As String, CalUserId As String, CalFunction As String, CalTime As Date, CalDetails As String, CalErrorMessage As String, CalCompany As String)
        Dim message As New AsyncDbLoggerItem With {
            .CalType = CalType,
            .CalUserId = CalUserId,
            .CalFunction = CalFunction,
            .CalTime = CalTime,
            .CalDetails = CalDetails,
            .CalErrorMessage = CalErrorMessage,
            .CalIP = _clientIP,
            .CalCompany = CalCompany
        }

        _logQueue.Enqueue(message)
    End Sub

    ''' <summary>
    ''' Stop the logger background thread manually.
    ''' </summary>
    Public Sub StopLogging()
        Try
            ' Signal the logging thread to stop
            _cts.Cancel()
            _logThread.Join() ' Wait for the logging thread to finish
        Catch ignore As Exception
        End Try
    End Sub

    Private Sub ProcessLogQueue(token As Object)
        Dim cancellationToken As CancellationToken = CType(token, CancellationToken)
        While Not cancellationToken.IsCancellationRequested
            Dim logItem As AsyncDbLoggerItem = Nothing
            If _logQueue.TryDequeue(logItem) Then
                SendLogToWebService(logItem)
            End If
            ' TODO: Change to in-time
            Thread.Sleep(1000) ' Adjust sleep time as needed
        End While
    End Sub

    Private Sub SendLogToWebService(logItem As AsyncDbLoggerItem)
        Try
            Dim result As Boolean = APIServiceBL.CallCRSRestfulAPI(Of Boolean)(logItem.CalCompany, "Async/InsertLog", logItem)

            ' Handle the response if needed
            Console.WriteLine($"Response from server: {result}")
        Catch ex As Exception
            ' Handle any exceptions that occur during the logging process
            Console.WriteLine($"Error logging message: {ex.Message}")
        End Try
    End Sub

    Private Function GetClientIpAddress() As String
        Try
            Dim hostName As String = Dns.GetHostName()
            ' TODO: Get correct IPV4's IP
            Dim ipAddress As String = Dns.GetHostEntry(hostName).AddressList(0).ToString()
            Return ipAddress
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

End Class
