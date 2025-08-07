Namespace Model.Claims
    Public Class MinorClaimChequeInfoRow
        Public Property mcspay_policy_no As String
        Public Property mcspay_claim_no As Decimal?
        Public Property mcspay_claim_occur As Decimal?
        Public Property lcpchq_chq_id As Decimal?
        Public Property lcpchq_payment_no As Decimal?
        Public Property lcpchq_system As String
        Public Property lcpchq_cheque_no As Decimal?
        Public Property lcpchq_reference_no As Decimal?
        Public Property lcpchq_issue_date As DateTime?
        Public Property lcpchq_print_mode As String
        Public Property lcpchq_bsb As String
        Public Property lcpchq_chq_status As String
        Public Property lcpchq_remark As String
        Public Property lcpchq_reprinted As Integer?
        Public Property lcpchq_batch_no As Decimal?
        Public Property lcpchq_request_by As String
        Public Property lcpchq_approval_id As String
        Public Property lcpchq_lot_id As Decimal?
        Public Property lcpchq_lastchgdate As DateTime?
        Public Property lcpchq_lastchgby As String
        Public Property lcpchq_reprint_chq_id As Decimal?
        Public Property lcpchq_gen_print_flag As String
        Public Property lcpchq_gen_void_flag As String
        Public Property lcpcpm_payment_no As Decimal?
        Public Property lcpcpm_system As String
        Public Property lcpcpm_payment_date As DateTime?
        Public Property lcpcpm_payment_type As String
        Public Property lcpcpm_case_no As String
        Public Property lcpcpm_policy_no As String
        Public Property lcpcpm_policy_curr As String
        Public Property lcpcpm_insured_no As String
        Public Property lcpcpm_payee_name As String
        Public Property lcpcpm_holder_name As String
        Public Property lcpcpm_agent_no As String
        Public Property lcpcpm_agent_name As String
        Public Property lcpcpm_agent_location As String
        Public Property lcpcpm_agent_unit As String
        Public Property lcpcpm_amount As Decimal?
        Public Property lcpcpm_cheque_curr As String
        Public Property lcpcpm_exchg_rate As Decimal?
        Public Property lcpcpm_exception As String
        Public Property lcpcpm_remark1 As String
        Public Property lcpcpm_remark2 As String
        Public Property lcpcpm_request_by As String
        Public Property lcpcpm_approval_id As String
        Public Property lcpcpm_create_date As DateTime?
    End Class

    Public Class MinorClaimChequeInfo
        Public Property mcsvw_cheque_info As List(Of MinorClaimChequeInfoRow)
    End Class
End Namespace