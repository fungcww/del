Public Interface ISecurity
    Function ConnStr(ByVal strProj As String, ByVal strConnType As String, Optional ByVal strUserType As String = "") As String
End Interface