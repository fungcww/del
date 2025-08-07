Imports System.Threading.Tasks

''' <summary>
''' Async get data business logic for system info config
''' </summary>
Public Class SystemInfoAsyncBL
    Inherits AsyncBaseBL

    Public Const BUSI_TYPE_SYSTEM_INFO_STR As String = "SystemInfo"

    Public Sub New(objDBHeader As Utility.Utility.ComHeader, objMQHeader As Utility.Utility.MQHeader)
        MyBase.New(objDBHeader, objMQHeader)
    End Sub

    Public Function GetNbSysInfo() As DataSet
        Return GetDataSet(BUSI_TYPE_SYSTEM_INFO_STR, "QueryNBSystemInfo", New Dictionary(Of String, String))
    End Function

    Public Function GetNbSysInfoAsync(callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_SYSTEM_INFO_STR, "QueryNBSystemInfo", New Dictionary(Of String, String)).
                ContinueWith(callback)
    End Function

    Public Function GetUWSysInfoAsync(callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_SYSTEM_INFO_STR, "QueryUWSystemInfo", New Dictionary(Of String, String)).
                ContinueWith(callback)
    End Function

End Class
