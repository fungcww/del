Public Interface ICS2005

    WriteOnly Property BufferSize() As Integer
    Property Env() As Integer
    Function GetPaymentHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetCouponHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
        ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    ''Function MQMsg2DT(ByVal strFunc As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    Function GetPolicyList(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            Optional ByVal blnCntOnly As Boolean = False) As DataTable
    Function GetCustomerList(ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String, _
            ByVal strCustID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            Optional ByVal blnCntOnly As Boolean = False) As DataTable

    'POLIC
    Function GetCoverage(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable()
    'POLIU
    Function GetULHistory(ByVal strPolicy As String, ByVal datEnq As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable()
    'POLIH
    Function GetCFHistory(ByVal strPolicy As String, ByVal strRider As String, ByVal datFrom As Date, ByVal datTo As Date, _
        ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    'POVAL
    Function GetPolicySummary(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetPolicyMisc(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    'CASHV
    Function GetPolicyVal(ByVal strPolicy As String, ByVal datVal As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable()

    Function GetPolicyAddress(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    'Function GetClientHistory(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetClientHistory(ByVal strPolicy As String, ByVal strType As String, ByVal strClientID As String, _
        ByVal datEnq As Date, ByVal strSeq As String, ByVal strBrUser As String, ByVal strCurDept As String, _
        ByVal strCurUsrID As String, ByVal strOption As String, ByVal strCode As String, ByVal datFollow As Date, _
        ByVal strFollowInit As String, ByVal strFollowAction As String, ByVal strComment1 As String, _
        ByVal strComment2 As String, ByVal strComment3 As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    Function GetDDA(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetCCDR(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetUWInfo(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetClientID(ByVal strCustID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As String
    Function GetModeChangeInfo(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    ''Function VBDate(ByVal strDate As String) As Date
    ''Function CPSDate(ByVal datVal As Date, Optional ByVal intBase As Integer = 1800) As String
    ''Function MQStruToString(ByVal objStru As Object) As String
    Function GetUPSGroup(ByVal strSystem As String, ByVal strUser As String, ByRef lngErrNo As Long, _
        ByRef strErrMsg As String) As Long
    Function GetPrivRS(ByVal intGroupID As Integer, ByVal strCtrl As String, ByRef lngErrNo As Long, _
            ByRef strErrMsg As String, Optional ByVal strType As String = "") As DataTable
    Function GetSystemInfo(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetORDUNA(ByVal strNANO As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetAPLHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetTrnHistory(ByVal strPolicy As String, ByVal datFrom As Date, ByVal datTo As Date, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    Function PrintPolicyStsRpt(ByVal strPolicy As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean
    Function PrintSurrenderRpt(ByVal strPolicy As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean

    Function GetPRTS(ByVal strPlan As String, ByVal strRS As String, ByVal strSmoke As String, _
        ByVal strPar As String, ByVal strSex As String, ByVal strTBL1 As String, ByVal strTBL2 As String, _
        ByVal intAge As Integer, ByVal intDur As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetUVAL(ByVal strPlan As String, ByVal strRS As String, ByVal strSmoke As String, _
        ByVal strPar As String, ByVal strSex As String, ByVal strTBL1 As String, ByVal strTBL2 As String, _
        ByVal intAge As Integer, ByVal strRecType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetIRTS(ByVal strPlan As String, ByVal strRS As String, ByVal strEffDate As Date, _
        ByVal intMTS As Integer, ByVal intDays As Integer, ByVal intMaxAmt As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetBANK(ByVal strBankCode As String, ByVal strBranchCode As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetDCAR(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetDISC(ByVal strPolicy As String, ByVal strCov As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetUTRH(ByVal strPolicy As String, ByVal datEnq As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable()

    Function LockPolicy(ByVal strPolicy As String, ByVal strUser As String, ByVal strFunc As String, _
        ByVal strTermID As String, ByVal strTransType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean
    Function UnLockPolicy(ByVal strPolicy As String, ByVal strUser As String, ByVal strFunc As String, _
        ByVal strTermID As String, ByVal strTransType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean
    Function GetRPUQuote(ByVal strPolicy As String, ByVal strUser As String, ByVal strLastName As String, ByVal strCurCode As String, _
        ByRef dblAmount As Double, ByVal strEffDate As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    Function GetCSR(ByVal strCSRID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function UpdateCSR(ByVal strCSRID As String, ByVal strCSRName As String, ByVal strCSRCName As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean

    Function GetMarkin(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetMarkinHist(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetPendingMarkin(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetPendingMarkinHist(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function GetPoProjection(ByVal strPolicy As String, ByVal intYear As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    Function ExecuteScript(ByVal strScript As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable
    Function UpdateData(ByVal strScript As String, ByVal strUType As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As String

    Function GetGIPSEAPolicy(ByVal strPolicy As String, ByVal strReCnt As String, ByVal strClient As String, _
        ByVal strType As String, _
        ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable()

End Interface

