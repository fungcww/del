Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class APICall

    Public Class OptInOutAPIRequest
        <JsonProperty(PropertyName:="documentNumber")>
        Property documentNumber As String
        <JsonProperty(PropertyName:="documentType")>
        Property documentType As String

        Public Sub New(documentNumber As String, documentType As String)
            Me.documentNumber = documentNumber
            Me.documentType = documentType
        End Sub
    End Class

    Public Class Message
        <JsonProperty(PropertyName:="IsSystemError")>
        Property IsSystemError As Boolean
        <JsonProperty(PropertyName:="message_en")>
        Property message_en As String
        <JsonProperty(PropertyName:="message_zh")>
        Property message_zh As String
    End Class

    Public Class OptInOutSaveAPIRequest
        <JsonProperty(PropertyName:="documentNumber")>
        Property documentNumber As String
        <JsonProperty(PropertyName:="documentType")>
        Property documentType As String
        <JsonProperty(PropertyName:="optOutFlag")>
        Property optOutFlag As String
        <JsonProperty(PropertyName:="updatedBy")>
        Property updatedBy As String
        Public Sub New(documentNumber As String, documentType As String, optOutFlag As String, updatedBy As String)
            Me.documentNumber = documentNumber
            Me.documentType = documentType
            Me.optOutFlag = optOutFlag
            Me.updatedBy = updatedBy
        End Sub
    End Class

    Public Class GetODISDAPIRequest
        <JsonProperty(PropertyName:="custId")>
        Property custId As String

        Public Sub New(custId As String)
            Me.custId = custId
        End Sub
    End Class

End Class
