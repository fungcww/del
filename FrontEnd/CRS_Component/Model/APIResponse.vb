
<Serializable()>
Public Class APIResponse(Of T)
    Public success As Boolean
    Public data As T
    Public message As String
    Public [error] As String
End Class
<Serializable>
Public Class AppLoanResponse
    Public companyLogo As List(Of RptCompanyLogo)
    Public agentInfo As List(Of AgentInfo)
    Public address As List(Of Address)
    Public policyAddress As List(Of PolicyAddress)
End Class

<Serializable()>
Public Class Address
    Public Property ph_nameprefix As String
    Public Property ph_firstname As String
    Public Property ph_namesuffix As String
    Public Property pi_firstname As String
    Public Property pi_namesuffix As String
    Public Property policycurrency As String
    Public Property paidtodate As DateTime
    Public Property sa_firstname As String
    Public Property sa_namesuffix As String
    Public Property phonenumber As String
    Public Property locationcode As String
    Public Property customerid As String
End Class

<Serializable()>
Public Class PolicyAddress
    Public Property ClientID As String
    Public Property NamePrefix As String
    Public Property NameSuffix As String
    Public Property FirstName As String
    Public Property gender As String
    Public Property usechiInd As String
    Public Property ChiName As String
    Public Property addressLine1 As String
    Public Property addressLine2 As String
    Public Property addressLine3 As String
    Public Property addressCity As String
    Public Property CaddressLine1 As String
    Public Property CaddressLine2 As String
    Public Property CaddressLine3 As String
    Public Property CaddressCity As String
End Class


<Serializable()>
Public Class AgentInfo
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
<Serializable>
Public Class RptCompanyLogo

    Public Property ING_Logo As Byte()

    Public Property ING_CompanyAddr As Byte()

    Public Property ING_Phone As Byte()

    Public Property CareCompany As Byte()

End Class
