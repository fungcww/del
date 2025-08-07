Namespace Model.Claims
    Public Class MinorClaimTotalClaimRow
        Public Property BeneType As String
        Public Property customerid As Decimal
        Public Property PolicyCurrency As String
        Public Property sum_mcscbh_paid_amt As Decimal?
        Public Property posctd_saving_bal As Decimal?
        Public Property possr_Terminal_Bonus As Decimal?
        Public Property mcsben_benefit_maxamt As Decimal?
        Public Property mcs_pay As Decimal?
        Public Property TotalRemain As Decimal?
    End Class

    Public Class MinorClaimTotalClaim
        Public Property Total_Claim As List(Of MinorClaimTotalClaimRow)
    End Class
End Namespace