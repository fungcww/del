Namespace Model.Claims
    Public Class MinorClaimBalanceRow
        Public Property MCSCRB_BALANCE_ID As Decimal?
        Public Property mcschd_claim_no As Decimal?
        Public Property mcscrb_policy_no As String
        Public Property RCD As DateTime?
        Public Property Trailer As Decimal?
        Public Property ProductID As String
        Public Property TBL1 As String
        Public Property mcsben_benefit_code As String
        Public Property mcschd_hospital_indate As DateTime?
        Public Property mcsben_benefit_maxamt As Decimal?
        Public Property mcscrb_remain_amt As Decimal?
        Public Property mcscrb_start_date As DateTime?
        Public Property mcscrb_end_date As DateTime?
    End Class

    Public Class MinorClaimBalance
        Public Property TheOneClaimBalance As List(Of MinorClaimBalanceRow)
    End Class
End Namespace