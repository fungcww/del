<Serializable()> _
Public Class PaymentInfo
    Public Property PaymentTypeCodes As List(Of PaymentTypeCodes) = New List(Of PaymentTypeCodes)
    Public Property cswvw_cam_Agent_info As List(Of cswvw_cam_Agent_info) = New List(Of cswvw_cam_Agent_info)
    Public Property DDARejectReasonCodes As List(Of DDARejectReasonCodes) = New List(Of DDARejectReasonCodes)
    Public Property CCDRRejectReasonCodes As List(Of CCDRRejectReasonCodes) = New List(Of CCDRRejectReasonCodes)
    Public Property csw_payh_remark_code As List(Of csw_payh_remark_code) = New List(Of csw_payh_remark_code)
    Public Property agentcodes As List(Of agentcodes) = New List(Of agentcodes)
    Public Property Logo As List(Of Logo) = New List(Of Logo)
End Class
<Serializable()> _
Public Class PaymentTypeCodes
    Public Property PaymentTypeCode As System.String
    Public Property PaymentTypeDesc As System.String
    Public Property timestamp As System.Byte()
    Public Property PayTypeChDesc As System.String
End Class 'PaymentTypeCodes
<Serializable()> _
Public Class cswvw_cam_Agent_info
    Public Property cswagi_agent_code As System.String
    Public Property cswagi_unit_code As System.String
    Public Property cswagi_loc_code As System.String
    Public Property cswagi_contract_date As System.DateTime
    Public Property cswagi_date_left As System.DateTime
    Public Property cswagi_grade As System.Int32
    Public Property cswagi_desc As System.String
    Public Property cswagi_mgr_name As System.String
    Public Property cswagi_res_phone As System.String
    Public Property cswagi_mob_phone As System.String
    Public Property cswagi_bus_phone As System.String
    Public Property cswagi_fax As System.String
    Public Property cswagi_email As System.String
    Public Property cswagi_idno As System.String
End Class 'cswvw_cam_Agent_info
<Serializable()> _
Public Class DDARejectReasonCodes
    Public Property DDARejectReasonCode As System.String
    Public Property DDARejectReasonDesc As System.String
    Public Property timestamp As System.Byte()
End Class 'DDARejectReasonCodes
<Serializable()> _
Public Class CCDRRejectReasonCodes
    Public Property CCDRRejectReasonCode As System.String
    Public Property CCDRRejectReasonDesc As System.String
    Public Property timestamp As System.Byte()
End Class 'CCDRRejectReasonCodes
<Serializable()> _
Public Class csw_payh_remark_code
    Public Property cswprc_code As System.String
    Public Property cswprc_desc As System.String
End Class 'csw_payh_remark_code
<Serializable()> _
Public Class Logo
    Public Property Policy As System.String
    Public Property ING_Logo As System.Byte()
    Public Property Address As System.Byte()
    Public Property IService As System.Byte()
    Public Property ICaring As System.Byte()
End Class 'Logo
