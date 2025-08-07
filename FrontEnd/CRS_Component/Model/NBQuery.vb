Imports Newtonsoft.Json

Public Class NBAPIQuery
    <JsonProperty(PropertyName:="corelationId")>
    Property corelationId As String
    <JsonProperty(PropertyName:="data")>
    Property data As Dictionary(Of String, String)
    <JsonProperty(PropertyName:="location")>
    Property location As String
    <JsonProperty(PropertyName:="userId")>
    Property userId As String

    Public Sub New(corelationId As String, data As Dictionary(Of String, String), location As String, userId As String)
        Me.corelationId = corelationId
        Me.data = data
        Me.location = location
        Me.userId = userId
    End Sub

End Class

Public Class NBResult
    <JsonProperty(PropertyName:="policyNumber")>
    Property policyNumber As String
    <JsonProperty(PropertyName:="remarkUser")>
    Property remarkUser As String
    <JsonProperty(PropertyName:="description")>
    Property description As String
    <JsonProperty(PropertyName:="date")>
    Property strDate As String

End Class