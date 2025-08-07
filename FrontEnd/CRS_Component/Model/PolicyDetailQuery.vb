Public Class PolicyDetailQuery
    Public Property PolicyAccountIds As List(Of String)
    Public Property CustomerId As String
    Public Property IsActive As Boolean?

    Public Sub New()
        PolicyAccountIds = New List(Of String)
    End Sub
End Class
