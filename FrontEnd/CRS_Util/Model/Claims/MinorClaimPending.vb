Namespace Model.Claims
    Public Class MinorClaimPendingRow
        Public Property mcsply_policy_no As String
        Public Property mcspen_claim_no As Decimal?
        Public Property mcspen_claim_occur As Decimal?
        Public Property mcspen_pending_date As DateTime?
        Public Property mcspen_pending_desc As String
        Public Property mcspen_resolved_date As DateTime?
        Public Property mcspen_resolved_desc As String
        Public Property mcspen_pending_status As String
        Public Property mcspen_doc_submit As String
        Public Property mcspen_doc_submit_date As DateTime?
    End Class

    Public Class MinorClaimPending
        Public Property mcsvw_claim_pending As List(Of MinorClaimPendingRow)
    End Class
End Namespace