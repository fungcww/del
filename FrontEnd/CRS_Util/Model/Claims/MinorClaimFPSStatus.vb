Namespace Model.Claims
    Public Class MinorClaimFPSStatusRow
        Public Property claimsnum As Decimal
        Public Property claimsoccur As Decimal
        Public Property policyaccountid As String
        Public Property proxytype As String
        Public Property proxyid As String
        Public Property fpslasttrialtime As DateTime
        Public Property FPSStatus As String
        Public Property failreason As String
        Public Property cntrycd As String
    End Class

    Public Class MinorClaimFPSStatus
        Public Property mcs_claim_header_fps As List(Of MinorClaimFPSStatusRow)
    End Class
End Namespace