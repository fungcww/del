Namespace Model.Claims
    Public Class MinorClaimBenefitRow
        Public Property mcscbh_policy_no As String
        Public Property mcscbh_claim_no As Decimal?
        Public Property mcscbh_claim_occur As Decimal?
        Public Property mcscbh_coverage As Decimal?
        Public Property Description As String
        Public Property mcsben_benefit_desc As String
        Public Property mcsben_benefit_maxamt As Decimal?
        Public Property mcscps_present_day As Decimal?
        Public Property mcscps_present_curr As String
        Public Property mcs_present As Decimal?
        Public Property mcschd_payment_curr As String
        Public Property mcscbh_paid_day As Decimal?
        Public Property mcs_pay As Decimal?
        Public Property mcscbh_booster_paid As Decimal?
    End Class

    Public Class MinorClaimBenefit
        Public Property mcsvw_claim_bene_hist As List(Of MinorClaimBenefitRow)
    End Class
End Namespace