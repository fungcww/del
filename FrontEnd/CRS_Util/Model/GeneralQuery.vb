Public Class GeneralQuery
    Public Property QueryString As String
    Public Property ConnectionString As String
    Public Property Parameters As Dictionary(Of String, Object)
    Public Property CredentialKey As String
    Public Property IsEncrypted As Boolean

    Public Sub New()
        QueryString = ""
        ConnectionString = ""
        CredentialKey = ""
        Parameters = New Dictionary(Of String, Object)()
        IsEncrypted = True
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="query">sql string</param>
    ''' <param name="connection">connection string</param>
    ''' <param name="credential">encrypted credential</param>
    ''' <param name="params">param dictionary used in sql</param>
    ''' <param name="encrypted">defined if connection string is encrypted</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal query As String, ByVal connection As String, ByVal credential As String, ByVal params As Dictionary(Of String, Object), ByVal encrypted As Boolean)
        QueryString = query
        ConnectionString = connection
        CredentialKey = credential
        Parameters = params
        IsEncrypted = encrypted
    End Sub
End Class

