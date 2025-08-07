Imports System.Threading.Tasks

''' <summary>
''' Async get data business base logic
''' </summary>
Public MustInherit Class AsyncBaseBL

    Protected objDBHeader As Utility.Utility.ComHeader
    Protected objMQHeader As Utility.Utility.MQHeader

    Public ReadOnly Property CompanyID As String
        Get
            Return objDBHeader.CompanyID.Trim.ToUpper
        End Get
    End Property

    Protected Sub New(objDBHeader As Utility.Utility.ComHeader, objMQHeader As Utility.Utility.MQHeader)
        Me.objDBHeader = objDBHeader
        Me.objMQHeader = objMQHeader
    End Sub

    Protected Function GetDataSet(busiType As String, busiId As String, queryDict As Dictionary(Of String, String)) As DataSet
        Return APIServiceBL.CallAsyncBusiAPI(CompanyID, busiType, busiId, queryDict)
    End Function

    Protected Function GetDataSetAsync(busiType As String, busiId As String, queryDict As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Task.Factory.StartNew(Function() GetDataSet(busiType, busiId, queryDict))
    End Function

    Public Shared Function WhenAll(ParamArray tasks As Task()) As Task
        Return Task.Factory.StartNew(Sub() Task.WaitAll(tasks))
    End Function

End Class
