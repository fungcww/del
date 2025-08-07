Namespace Model.Claims
    Public Class MinorClaimPendingAsurRow
        Public Property mcsply_policy_no As String
        Public Property mcspen_claim_no As Decimal?
        Public Property mcspen_claim_occur As Decimal?
        Public Property mcspen_pending_date As DateTime?
        Public Property mcspen_pending_desc As String
        Public Property mcspen_resolved_date As DateTime?
        Public Property mcspen_resolved_desc As String
        Public Property mcspen_pending_status As String
    End Class

    Public Class MinorClaimPendingAsur
        Public Property mcsvw_claim_pending As List(Of MinorClaimPendingAsurRow)
    End Class
End Namespace