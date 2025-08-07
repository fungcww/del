Module MQMsg

    Public Structure PAYH
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strAccNo_16 As String
        Public strDate_6 As String
        Public strOption_1 As String
        Public strPaymentType_4 As String
        Public strRemarkCode_1 As String
        Public strUser_3 As String
    End Structure

    Public Structure COUH
        Public strFunc_5 As String
        Public strMonth_2 As String
        Public strYear_2 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure CCDR
        Public strFunc_5 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure DDAR
        Public strFunc_5 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure APLH
        Public strFunc_5 As String
        Public strMonth_2 As String
        Public strYear_2 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure TRNH
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strFYear_3 As String
        Public strFMth_2 As String
        Public strFDay_2 As String
        Public strTYear_3 As String
        Public strTMth_2 As String
        Public strTDay_2 As String
    End Structure

    Public Structure HICL
        Public strFunc_5 As String
        Public strType_1 As String
        Public strPolicy_10 As String
        Public strClient_10 As String
        Public strDate_7 As String
        Public strSeq_2 As String
        Public strBUsr_3 As String
        Public strCDept_3 As String
        Public strCUsr_3 As String
        Public strOption_1 As String
        Public strCode_5 As String
        Public strFDate_7 As String
        Public strFInit_3 As String
        Public strFAction_5 As String
        Public strComment1_50 As String
        Public strComment2_50 As String
        Public strComment3_50 As String
    End Structure

    Public Structure POLIC
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strRider_2 As String
    End Structure

    Public Structure POLIU
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strYear_3 As String
        Public strMonth_2 As String
        Public strDay_2 As String
    End Structure

    Public Structure POLIH
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strRider_2 As String
        Public strFDate_7 As String
        Public strTDate_7 As String
    End Structure

    Public Structure CASHV
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strDate_7 As String
        Public strRider_2 As String
    End Structure

    Public Structure POVAL
        Public strFunc_5 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure DCAR
        Public strFunc_5 As String
        Public strOption_1 As String
        Public strPolicy_10 As String
    End Structure

    Public Structure DISC
        Public strFunc_5 As String
        Public strOption_1 As String
        Public strPolicy_10 As String
        Public strCov_2 As String
    End Structure

    Public Structure UTRH
        Public strFunc_5 As String
        Public strPolicy_10 As String
        Public strDate_7 As String
        Public strPriceDate_7 As String
    End Structure

    Public Structure GIPSEA
        Public strPolicy_16 As String
        Public strRen_3 As String
        Public strClient_10 As String
        Public strType_1 As String
    End Structure

End Module
