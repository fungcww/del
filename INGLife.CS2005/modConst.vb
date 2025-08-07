Module modConst

    Public Const gSystem = "CRS"

    ' Shared variable should not be used in Component Service

    ' MQ connection info.
    'Public cNTQMgr As String
    'Public cNTRecvQ As String
    'Public cCAPReplyQ As String
    'Public cNTReplyQ As String
    'Public cMQBufferSize As Integer

    'Public cGINTRecvQ As String
    'Public cGICAPReplyQ As String
    'Public cGINTReplyQ As String

    ' CAPSIL Function
    Public Const cPAYH = "PAYH"
    Public Const cCOUH = "COUH"
    Public Const cTRNH = "TRNH"
    Public Const cAPLH = "APLH"
    Public Const cCCDR = "CCDR"
    Public Const cDDAR = "DDAR"
    Public Const cHICL = "HICL"
    Public Const cPOLIC = "POLIC"
    Public Const cPOLIU = "POLIU"
    Public Const cPOLIH = "POLIH"
    Public Const cPOVAL = "POVAL"
    Public Const cCASHV = "CASHV"
    Public Const cDCAR = "DCAR"
    Public Const cDISC = "DISC"
    Public Const cUTRH = "UTRH"


    ' **** SQL 2005 begin ****
    'Public cUPS As String = "hksqlvs1.profile.dbo."
    'Public cMCS As String = "hksqlvs1."
    'Public cPOS As String = "hksqlvs1."
    '' **** SQL 2005 end ****
    'Public cPOLE As String = "EAAMOD."
    'Public cPROJ As String = "EAAMOD"
    'Public cCIW As String = "vantive.dbo."

    'Public scQMgr As String = "HKCAPSIL3"
    'Public scRecvQ As String = "HKLIFE3.TO.CAPSIL1"
    'Public scReplyQ As String = "CAPSIL.TO.HKLIFE3"
    'Public scNTReplyQ As String = "HKLIFE3.QUEUE1.LCL"


    ' SQL Table alias
    'Public UPS_USER_GROUP_TAB As String = cUPS & "ups_user_group_tab"
    'Public UPS_USER_LIST_TAB As String = cUPS & "ups_user_list_tab"
    'Public UPS_MENU_ITEM_TAB As String = cUPS & "ups_menu_item_tab"
    'Public CSW_MQ_FUNC_HEADER As String = cCIW & "csw_mq_func_header"
    'Public CSW_MQ_FUNC_DETAIL As String = cCIW & "csw_mq_func_detail"

    ' Server name
#If UAT = 0 Then
    Public Const cUPS = "profile.dbo."
    Public Const cPOS = "pos.dbo."
    Public Const cCIW = "vantive.dbo."

    Public Const cPOLE = "EAAMOD."
    Public Const cPROJ = "EAAMOD"

    'Public Const scQMgr = "HKCAPSIL3"
    'Public Const scRecvQ = "HKLIFE3.TO.CAPSIL1"
    'Public Const scReplyQ = "CAPSIL.TO.HKLIFE3"
    'Public Const scNTReplyQ = "HKLIFE3.QUEUE1.LCL"
#Else
    Public Const cUPS = "ingprofileS202.dbo."
    Public Const cPOS = "ingposS202.dbo."
    Public Const cCIW = "ingciwS202.dbo."

    Public Const cPOLE = "LDDCPS85DB."
    Public Const cPROJ = "LDDCPSLMP"

    'Public Const scQMgr = "HKCAPSIL3"
    'Public Const scRecvQ = "HKLIFE3.TO.CAPSIL1"
    'Public Const scReplyQ = "CAPSIL.TO.HKLIFE3"
    'Public Const scNTReplyQ = "HKLIFE3.QUEUE1.LCL"
#End If

    ' SQL Table alias
    Public Const UPS_USER_GROUP_TAB = cUPS & "ups_user_group_tab"
    Public Const UPS_USER_LIST_TAB = cUPS & "ups_user_list_tab"
    Public Const UPS_MENU_ITEM_TAB = cUPS & "ups_menu_item_tab"
    Public Const CSW_MQ_FUNC_HEADER = cCIW & "csw_mq_func_header"
    Public Const CSW_MQ_FUNC_DETAIL = cCIW & "csw_mq_func_detail"

End Module
