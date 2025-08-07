Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Data.SqlClient

Public Class ApiResponse

    Public dtResult As DataTable
    Public errMsg As String

    Public Sub ApiResponse(ByVal dtResult As DataTable, ByVal errMsg As String)
        Me.dtResult = dtResult
        Me.errMsg = errMsg
    End Sub


    Public Property DtRespResult() As DataTable
        Set(ByVal value As DataTable)
            dtResult = value
        End Set
        Get
            Return dtResult
        End Get
    End Property

    Public Property ErrorMsg() As String
        Set(ByVal value As String)
            errMsg = value
        End Set
        Get
            Return errMsg
        End Get
    End Property

End Class

<Serializable()> _
Public Class ParamClass

    Public da As SqlDataAdapter


    Public Property DaRespResult() As SqlDataAdapter
        Set(ByVal value As SqlDataAdapter)
            da = value
        End Set
        Get
            Return da
        End Get
    End Property

End Class
