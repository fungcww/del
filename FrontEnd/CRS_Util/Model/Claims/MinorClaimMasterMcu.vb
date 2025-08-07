Namespace Model.Claims
    Public Class MinorClaimMasterMcuRow
        Public Property mcsply_policy_no As String
        Public Property mcschd_claim_no As Decimal?
        Public Property mcschd_claim_occur As Decimal?
        Public Property mcschd_insured_name As String
        Public Property mcschd_insured_id As String
        Public Property mcschd_accident_date As DateTime?
        Public Property mcschd_hospital_indate As DateTime?
        Public Property mcschd_hospital_outdate As DateTime?
        Public Property mcschd_receive_date As DateTime?
        Public Property mcschd_consult_date As DateTime?
        Public Property mcschd_settled_date As DateTime?
        Public Property mcschd_symptom_date As DateTime?
        Public Property mcschd_comment As String
        Public Property mcschd_chq_remark1 As String
        Public Property mcschd_chq_remark2 As String
        Public Property remark As String
        Public Property mcschd_clm_status As String
        Public Property mcschd_account_no As String
        Public Property mcschd_payee_name As String
        Public Property mcschd_accident_desc As String
        Public Property mcschd_hospital_desc As String
        Public Property mcschd_operation_name As String
        Public Property mcschd_return_doc1 As String
        Public Property mcschd_return_doc2 As String
        Public Property mcschd_return_doc3 As String
        Public Property mcschd_return_doc4 As String
        Public Property impairment1 As String
        Public Property impairment2 As String
        Public Property impairment3 As String
        Public Property impairment4 As String
        Public Property mcscst_status_ext_desc As String
        Public Property mcsmpy_mobile_num As String
        Public Property mcsmpy_7ElevenPay As String
        Public Property mcscps_present_amt As Decimal?
    End Class

    Public Class MinorClaimMasterMcu
        Public Property mcsvw_claim_header_details As List(Of MinorClaimMasterMcuRow)
    End Class
End Namespace