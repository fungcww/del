Public Class MacauServiceBL

    Public Function SetMCUCIWSQLConn() As String
        Dim strsqlconn As String
        If gUAT Then
            'ITSR933 FG R3 Polciy Number Change Start
            'ITSR933 FG R4 EnvSetup Gary Lei Start
            If g_Connection_CIW.Contains("106") Or g_Connection_CIW.Contains("ITR") Then
                'ITSR933 FG R4 EnvSetup Gary Lei End
                strsqlconn = objSec.ConnStr(g_ProjectAlias, g_Connection_CIW.Replace("ING", "MCU"), g_UserType)
            Else
                strsqlconn = objSec.ConnStr("LAS", "MCUCIWU101", "LASUPDATE") 'Get MCUCIWU101 connectionstring
            End If
            'ITSR933 FG R3 Polciy Number Change Start
        Else
            strsqlconn = objSec.ConnStr("LAS", "MCUCIWPRD01", "LASUPDATE") 'Get MCUCIWPRD01 connectionstring
        End If
        Return strsqlconn
    End Function

    Public Function SetMCUACMSQLConn() As String
        Dim strsqlconn As String
        If gUAT Then
            'ITSR933 FG R3 Polciy Number Change Start
            'ITSR933 FG R4 EnvSetup Gary Lei Start
            If g_Connection_CIW.Contains("106") Or g_Connection_CIW.Contains("ITR") Then
                'ITSR933 FG R4 EnvSetup Gary Lei End
                strsqlconn = objSec.ConnStr(g_ProjectAlias, g_Connection_CAM.Replace("ING", "MCU"), g_UserType)
            Else
                strsqlconn = objSec.ConnStr("LAS", "MCUCAMU101", "LASUPDATE") 'Get MCUCIWU101 connectionstring
            End If
            'ITSR933 FG R3 Polciy Number Change Start
        Else
            strsqlconn = objSec.ConnStr("LAS", "MCUCAMPRD01", "LASUPDATE") 'Get MCUCIWPRD01 connectionstring
        End If
        Return strsqlconn
    End Function

End Class
