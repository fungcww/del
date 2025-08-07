Imports System.Xml.Serialization


<Serializable()> _
Public Class WSResponse(Of T)
    Public Property Success As Boolean

    Public Property Message As String

    Public Property ErrorMsg As String

    Public Property Data As T
End Class


