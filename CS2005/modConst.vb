Module modConst

    Public Const gSystem = "CRS"
    Public Const gUPSystem = "CSW"
    Public Const gRegEx = "(NOT)?(\s*\(*)\s*(\w+)\s*(=|<>|<|>|>=|<=|LIKE|IN)\s*(\(([^\)]*)\)|'([^']*)'|(-?\d*\.?\d+))(\s*\)*\s*)(AND|OR)?"
    Public Const gError = "~~ERROR~~"
    'Public Const gSearchLimit = 100
    'Public Const gWindowLimit = 10
    Public Const gNULLText = ""
    Public Const gDateFormat = "dd-MMM-yyyy"
    Public Const gDateTimeFormat = "dd-MMM-yyyy hh:mm tt"
    Public Const gNumFormat = "#,##0.00"
    Public Const gMfFormat = "0.0000000"
    Public Const gCCFormat = "####-####-####-####"
    Public Const gSearchLimit_const As Long = 200
    Public Const gLOCAL_HELP_PATH As String = "C:\Prodapps\LA DLL\crs_help.chm"

    ' MQ connection info.
    'Public Const cNTQMgr = "HKALSQLUAT1"
    'Public Const cNTRecvQ = "HKLIFE.TO.CAPSIL1"
    'Public Const cCAPReplyQ = "CAPSIL.TO.HKLIFE1"
    'Public Const cNTReplyQ = "HKLIFE.QUEUE1.LCL"
    'Public Const cMQBufferSize = 65535

    Public Const cPOADDR = "POADDR"
    Public Const cCOINFO = "COINFO"
    Public Const cDDA = "DDA"
    Public Const cCCDR = "CCDR"
    Public Const cPAYH = "PAYH"
    Public Const cHICL = "HICL"
    Public Const cUWINFO = "UWINFO"
    Public Const cCOUHST = "COUHST"
    Public Const cAPLH = "APLH"
    Public Const cORDUNA = "ORDUNA"
    Public Const cTRNH = "TRNH"
    Public Const cDCAR = "DCAR"


    '*** LifeAsia - Start ***
    Public CSW_USER_PRIVS As String
    Public UPS_USER_GROUP_TAB As String
    Public UPS_USER_LIST_TAB As String
    Public CAM_HKL_AGENT_MAPPING As String
    Public NBR_WNC_WORKSHEET As String
    Public NBR_WNC_STATUS As String
    Public cSQL3 As String
    Public cSQL3_Mcu As String
    Public cSQL3_Asur As String
    Public CIC_FULLNAME_MAPPING As String

    Public g_LAUser As String
    Public g_Qman As String
    Public g_WinRemoteQ As String
    Public g_WinRemoteMcuQ As String
    Public g_LAReplyQ As String
    Public g_LAReplyMcuQ As String
    Public g_WinLocalQ As String
    Public g_WinLocalMcuQ As String
    Public g_Env As String
    Public g_McuEnv As String
    Public g_Comp As String
    'oliver 2024-7-31 added for Com 6
    Public g_BmuComp As String
    Public g_McuComp As String

    Public g_ProjectAlias As String
    Public g_UserType As String

    Public g_Connection_CIW As String
    Public g_Connection_McuCIW As String
    Public g_Connection_UPS As String
    Public g_Connection_ICR As String
    Public g_Connection_MCS As String
    Public g_Connection_MJC As String
    Public g_Connection_McuMJC As String
    Public g_Connection_CAM As String
    'AC - Change to use configuration setting - start
    Public g_Connection_CNB As String
    'AC - Change to use configuration setting - end

    Public g_UserType_ICR As String
    Public g_ProjectAlias_MJC As String
    Public g_UserType_MJC As String

    Public g_Capsil_Project As String
    Public g_Capsil_Connection As String
    Public g_Capsil_UserType As String

    Public g_Capsil_Lib As String
    Public g_CAM_Database As String
    Public g_McuCAM_Database As String
    Public g_McuNBR_Database As String
    
    Public giEnv As Integer
    'Public UAT As Integer
    '*** LifeAsia - End ***

    Public Enum PrintDest
        pdPrinter = 1
        pdPreview = 2
        pdFax = 3
        pdExport = 4    ' ES005
        pdCM = 5
    End Enum

    Public Enum Company
        ING = 1
        LAC = 2
        LAH = 3
        MCU = 4
        BMU = 5
    End Enum

    

End Module
