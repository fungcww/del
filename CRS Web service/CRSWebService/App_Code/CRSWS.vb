' Admended By: Flora Leung
' Admended Function:                     
' Added Function:   GetDDRRejectReason
' Date: 18 Jan 2012
' Project: Project Leo Goal 3
'********************************************************************

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Collections.Generic
Imports CRS
'Imports CRS.ApiResponse

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CRSWS
    Inherits System.Web.Services.WebService
    Public objMQHeader As ING_SOAPHeader.MQSOAPHeader
    Public objDBHeader As ING_SOAPHeader.DBSOAPHeader
    Public objHeader As ING_SOAPHeader.ING_SOAPHeader
    Public objDBHead As Utility.Utility.ComHeader

    Private _CRSObj As CRS.CRS
    Private ReadOnly Property CRSObj() As CRS.CRS
        <System.Diagnostics.DebuggerStepThrough()> _
        Get
            If _CRSObj Is Nothing Then
                _CRSObj = New CRS.CRS()

                If objDBHeader IsNot Nothing Then
                    Dim objDBHead As New Utility.Utility.ComHeader
                    objDBHead.ProjectAlias = objDBHeader.Project
                    objDBHead.CompanyID = objDBHeader.Comp
                    objDBHead.UserID = objDBHeader.User
                    objDBHead.EnvironmentUse = objDBHeader.Env
                    objDBHead.UserType = objDBHeader.UserType
                    _CRSObj.DBHeader = objDBHead
                End If

                If objMQHeader IsNot Nothing Then
                    Dim objMQHead As New Utility.Utility.MQHeader
                    objMQHead.QueueManager = objMQHeader.QueueManager
                    objMQHead.RemoteQueue = objMQHeader.RemoteQueue
                    objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
                    objMQHead.LocalQueue = objMQHeader.LocalQueue
                    objMQHead.Timeout = objMQHeader.Timeout
                    objMQHead.UserID = objMQHeader.UserID
                    objMQHead.ProjectAlias = objMQHeader.ProjectAlias
                    objMQHead.CompanyID = objMQHeader.CompanyID
                    objMQHead.EnvironmentUse = objMQHeader.EnvironmentUse
                    objMQHead.UserType = objMQHeader.UserType

                    _CRSObj.MQQueuesHeader = objMQHead
                End If
            End If

            Return _CRSObj
        End Get
    End Property

#Region " Direct Debit Enquiry"
    <WebMethod(Description:="GetMandateList")> _
    <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetMandateList(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objMQHead.ProjectAlias = objMQHeader.ProjectAlias
            objMQHead.CompanyID = objMQHeader.CompanyID
            objMQHead.EnvironmentUse = objMQHeader.EnvironmentUse
            objMQHead.UserType = objMQHeader.UserType

            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetMandateListRecord(dsSendData, dsReceData, strErr)
    End Function

    <WebMethod(Description:="GetDirDebitEnq")> _
<SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetDirDebitEnq(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetDirectDebitEnqRecord(dsSendData, dsReceData, strErr)

    End Function
#End Region

#Region " Transaction History"
    <WebMethod(Description:="GetTranHist")> _
    <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetTranHist(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetTranHistRecord(dsSendData, dsReceData, strErr)
    End Function

    <WebMethod(Description:="GetTranPosting")> _
<SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetTranPosting(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetTranPostingRecord(dsSendData, dsReceData, strErr)
    End Function
#End Region

#Region " Financial information"
    <WebMethod(Description:="GetSubAcctBalEnq")> _
   <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetSubAcctBalEnq(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetSubAcctBalEnqRecord(dsSendData, dsReceData, strErr)
    End Function

    <WebMethod(Description:="GetSubAcctBalPosting")> _
<SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetSubAcctBalPosting(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetSubAcctBalPostingRecord(dsSendData, dsReceData, strErr)
    End Function
#End Region

#Region " Payment History"
    <WebMethod(Description:="GetPaymentHist")> _
 <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetPaymentHist(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetPaymentHistRecord(dsSendData, dsReceData, strErr)
    End Function
#End Region

#Region " Check Pending Fund Switching "
    <WebMethod(Description:="CheckPendingFSW")> _
 <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function CheckPendingFSW(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.CheckPendingFSW(dsSendData, dsReceData, strErr)
    End Function
#End Region

#Region " Mcu policy search"
    '<WebMethod()> _
    'Public Function GetPolicyListMCUCIW(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
    '        ByVal strTable As String, ByVal strCri As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
    '        ByVal blnCntOnly As Boolean) As DataTable

    '    Dim objCRS As New CRS.CRS

    '    Dim dt As New DataTable()

    '    Return objCRS.GetPolicyListMCUCIW(strCompanyCode, strEnvCode, strCustID, strPolicy, strRel, strTable, strCri, lngErrNo, strErrMsg, intCnt, True)

    'End Function
#End Region

#Region " Mcu customer search "
    '<WebMethod(Description:="GetCustomerListMcu")> _
    'Public Function GetCustomerListMcu(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String, _
    '                                   ByVal strCustID As String, ByVal strAgentNo As String, ByVal strPlateNumber As String, _
    '                                   ByVal LastNameVal As String, ByVal FirstNameVal As String, ByVal HKIDVal As String, ByVal CustVal As String, _
    '                                   ByVal AgentNoVal As String, ByVal PlateNumberVal As String, ByVal MobileVal As String, ByVal EmailVal As String, _
    '                                   ByVal blnCntOnly As Boolean, ByVal strMobile As String, ByVal strEmail As String, ByRef lngErrNo As Long, _
    '                                   ByRef strErrMsg As String, ByRef intCnt As Integer, ByRef dtCusLst As DataTable) As Boolean


    '    Dim objCRS As New CRS.CRS



    '    Return objCRS.GetCustomerListMcu(strCompanyCode, strEnvCode, strLastName, strFirstName, strHKID, strCustID, strAgentNo, strPlateNumber, LastNameVal, _
    '                                     FirstNameVal, HKIDVal, CustVal, AgentNoVal, PlateNumberVal, MobileVal, EmailVal, blnCntOnly, strMobile, strEmail, lngErrNo, strErrMsg, intCnt, dtCusLst)

    '    'Return False

    '    'Return objCRS.getc(strCustID, strPolicy, strRel, strTable, strCri, lngErrNo, strErrMsg, intCnt, True)

    'End Function
#End Region

#Region "Get Macau policy summer Agent info tab records"

    '<WebMethod(Description:="GetMcuAgentInfoRecords")> _
    'Public Function getMcuAgentInfoList(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal g_Comp As String, ByVal serName As String, ByVal strAgtList As String, ByVal camHKLAgtMap As String, _
    '                             ByVal camDB As String, ByVal ds As DataSet, ByRef strErrMsg As String) As DataSet


    '    Dim objCRS As New CRS.CRS

    '    Dim tmpDs As New DataSet()

    '    Return objCRS.getMcuAgentInfoList(strCompanyCode, strEnvCode, g_Comp, serName, strAgtList, camHKLAgtMap, camDB, ds, strErrMsg)

    'End Function

#End Region


    '#Region "Common SQL Execution"
    '    <WebMethod()> _
    '    Public Function commonSQLExcute(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strSql As String, ByVal paramList As String) As CRS.ApiResponse

    '        Dim objCRS As New CRS.CRS
    '        Dim api As CRS.ApiResponse = New CRS.ApiResponse()
    '        Return objCRS.commonSQLExcute(strCompanyCode, strEnvCode, strSql, paramList)
    '        'Return api

    '    End Function
    '#End Region

#Region "Check Ori User Right"

    '<WebMethod()> _
    'Public Function checkUserRight(ByVal UserID As String, ByRef strErr As String) As DataTable

    '    Dim objCRS As New CRS.CRS
    '    Dim dt As DataTable = New DataTable()

    '    Return objCRS.checkUserRight(UserID, strErr)
    '    'Return dt

    'End Function

#End Region

#Region "Get Access UPS Group List"

    '<WebMethod()> _
    'Public Function GetUPSGroup(ByVal strSystem As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

    '    Dim objCRS As New CRS.CRS
    '    Dim dt As DataTable = New DataTable()

    '    Return objCRS.GetUPSGroup(strSystem, strUser, lngErrNo, strErrMsg)
    'End Function

#End Region


#Region "Get Agent Account List"

    <WebMethod()> _
    Public Function getAgentAccountList(ByVal g_Comp As String, ByVal serName As String, ByVal strAgtList As String, ByVal camHKLAgtMap As String, _
                                 ByVal camDB As String, ByRef lngErrNo As Integer, ByRef strErrMsg As String, ByRef ds As DataSet) As Boolean
        Dim objCRS As New CRS.CRS
        Return False
        'Return objCRS.getAgentInfoAgtList(g_Comp, serName, strAgtList, camHKLAgtMap, camDB, lngErrNo, strErrMsg, ds)

    End Function

#End Region

#Region "Get Load ComboBox"

    <WebMethod()> _
    Public Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As Object, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, ByVal blnAllowNull As Boolean) As Boolean

        Dim objCRS As New CRS.CRS

        Return False

    End Function

#End Region

#Region "Get Personal Infi Addres for Mcu customer webservice"
   ' <WebMethod(Description:="GetPersonalInfoAddress4McuCustomer")> _
   '<SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
   ' Public Function GetPersonalInfoAddress4McuCustomer(ByVal strCustID As String, ByVal strClientID As String, _
   '                                                   ByRef strErrMsg As String, ByRef blnCovSmoker As Boolean, _
   '                                                       ByRef dtPersonal As DataTable, ByRef dtAddress As DataTable, _
   '                                                       ByRef dtCIWPersonInfo As DataTable, ByVal blnLoadPol As Boolean) As Boolean
   '     Dim objCRS As New CRS.CRS

   '     Return objCRS.GetPersonalInfoAddress4McuCustomer(strCustID, strClientID, strErrMsg, blnCovSmoker, dtPersonal, dtAddress, dtCIWPersonInfo, blnLoadPol)

   '     'Return True


   ' End Function

#End Region


#Region "get GI Mcu policy List records"
    <WebMethod()> _
    Public Function GetGIPolicyMcuList(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            ByVal blnCntOnly As Boolean) As DataTable

        Dim objCRS As New CRS.CRS

        Dim _dt As New DataTable

        'Return objCRS.GetGIPolicyMcuList(strCompanyCode, strEnvCode, strCustID, strPolicy, strRel, strTable, lngErrNo, strErrMsg, intCnt, blnCntOnly)
        Return _dt

    End Function
#End Region



#Region "Get Fund Transaction History"
    <WebMethod(Description:="GetFundTxHist")> _
 <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
 <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetFundTxHist(ByVal strPolicy As String, ByVal strTxType As String, ByVal strTxID As String, ByVal strLAFlag As String, ByRef dsReceData As Data.DataSet, _
            ByRef strErr As String) As Boolean
        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objCRS.MQQueuesHeader = objMQHead

            Dim objDBHead As New Utility.Utility.ComHeader
            objDBHead.ProjectAlias = objDBHeader.Project
            objDBHead.CompanyID = objDBHeader.Comp
            objDBHead.UserID = objDBHeader.User
            objDBHead.EnvironmentUse = objDBHeader.Env
            objDBHead.UserType = objDBHeader.UserType
            objCRS.DBHeader = objDBHead

        End If
        Return objCRS.GetFundTxHist(strPolicy, strTxType, strTxID, strLAFlag, dsReceData, strErr)
    End Function
#End Region

    ' Flora Leung, Project Leo Goal 3, 18-Jan-2012 Start
#Region "DDA/CCDR"
    <WebMethod(Description:="GetDDRRejectReason")> _
    <SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetDDRRejectReason(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS
        If Not IsNothing(objMQHeader) Then
            Dim objMQHead As New Utility.Utility.MQHeader
            objMQHead.QueueManager = objMQHeader.QueueManager
            objMQHead.RemoteQueue = objMQHeader.RemoteQueue
            objMQHead.ReplyToQueue = objMQHeader.ReplyToQueue
            objMQHead.LocalQueue = objMQHeader.LocalQueue
            objMQHead.Timeout = objMQHeader.Timeout
            objMQHead.UserID = objMQHeader.UserID
            objMQHead.ProjectAlias = objMQHeader.ProjectAlias
            objMQHead.CompanyID = objMQHeader.CompanyID
            objMQHead.EnvironmentUse = objMQHeader.EnvironmentUse
            objMQHead.UserType = objMQHeader.UserType

            objCRS.MQQueuesHeader = objMQHead
        End If
        Return objCRS.GetDDRRejectReason(dsSendData, dsReceData, strErr)
    End Function
#End Region
    ' Flora Leung, Project Leo Goal 3, 18-Jan-2012 End

#Region "Check user permission and user type"
    '<WebMethod()> _
    'Public Function CheckUserPermission(ByVal UserID As String, ByRef dtFuncList As DataTable, ByRef strErr As String) As DataTable
    '    'Public Function CheckUserPermission(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

    '    Dim objCRS As New CRS.CRS

    '    Dim dt As New DataTable()

    '    Return objCRS.CheckUserPermission(UserID, dtFuncList, strErr)



    'End Function
#End Region

#Region "Get Private RS Records"

    <WebMethod(Description:="GetPrivRS")> _
    Public Function GetPrivRS(ByVal intGroupID As Integer, ByVal strCtrl As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim objCRS As New CRS.CRS

        Dim dt As New DataTable()

        Return objCRS.GetPrivRS(intGroupID, strCtrl, lngErrNo, strErrMsg)

    End Function

#End Region



#Region "Get Macau Major Claim History Records Section"

    <WebMethod()> _
    Public Function getMClaimPolicyRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDB As String, ByVal policyNo As String, ByVal policyCap As String, ByRef strErrMsg As String) As DataSet
        'Public Function CheckUserPermission(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMClaimPolicyRec(strCompanyCode, strEnvCode, strDB, policyNo, policyCap, strErrMsg)

    End Function


    <WebMethod()> _
    Public Function getMcuMClaimPendingRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strClaim As String, ByVal policyNo As String, ByVal policyCap As String, ByVal dsClaim As DataSet, ByRef strErrMsg As String) As DataSet
        'Public Function CheckUserPermission(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMcuMClaimPendingRec(strCompanyCode, strEnvCode, strClaim, policyNo, policyCap, dsClaim, strErrMsg)

    End Function


    <WebMethod()> _
    Public Function getMcuMClaimBenefitRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal policyCap As String, ByVal dsClaim As DataSet, ByRef strErrMsg As String) As DataSet
        'Public Function CheckUserPermission(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMcuMClaimBenefitRec(strCompanyCode, strEnvCode, policyNo, policyCap, dsClaim, strErrMsg)

    End Function


#End Region

#Region "Test web service connetion"
    <WebMethod()> _
    Public Function chkWebsConnetion(ByRef UserID As String, ByRef strErr As String) As Boolean
        'Public Function CheckUserPermission(ByRef dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, ByRef strErr As String) As Boolean
        '<SoapHeader("objMQHeader", direction:=SoapHeaderDirection.In)> _

        Dim objCRS As New CRS.CRS

        Return True

    End Function
#End Region

#Region "Get Macau DDA/CCDR status Code"

    <WebMethod()> _
    Public Function getMacauDDACCDRStatusCode(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMacauDDACCDRStatusCode(strCompanyCode, strEnvCode, strErr)

    End Function

#End Region

#Region "Get APL History Records"

    <WebMethod()> _
    Public Function getAplHistMcuRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getAplHistMcuRec(strCompanyCode, strEnvCode, strErr)

    End Function

#End Region



#Region "get MCU SMS Letter section"

    <WebMethod()> _
    Public Function getMcuSmsMsgDetail(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal policyCap As String, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMcuSmsMsgDetail(strCompanyCode, strEnvCode, policyNo, policyCap, strErr)

    End Function

    <WebMethod()> _
    Public Function getMcuSMSPolicyInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal dsSMS As DataSet, ByVal mcCamDB As String, ByVal mcNbr As String, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Dim ds As New DataSet()

        Return objCRS.getMcuSMSPolicyInfo(strCompanyCode, strEnvCode, policyNo, dsSMS, mcCamDB, mcNbr, strErr)

    End Function

#End Region


#Region "Get Macau QDAP refund extra Paymenr Alert Records"

    <WebMethod()> _
    Function getMcuQDAPRefundExtraPaymentAlert(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal custID As String, ByVal dtBusDate As DateTime, ByRef strErr As String) As DataTable

        Dim objCRS As New CRS.CRS

        Dim dt As New DataTable()

        Return objCRS.getMcuQDAPRefundExtraPaymentAlert(strCompanyCode, strEnvCode, policyNo, custID, dtBusDate, strErr)

    End Function

#End Region


#Region "get Macau Underwriting Records section"


    <WebMethod()> _
    Public Function getPendingAgtCode(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As DataTable

        Dim objCRS As New CRS.CRS

        Dim dt As New DataTable()

        Return objCRS.getPendingAgtCode(strCompanyCode, strEnvCode, policyNo, strErr)

    End Function


    <WebMethod()> _
    Public Function getMcuPolicyUW(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef ds As DataSet, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Return objCRS.getMcuPolicyUW(strCompanyCode, strEnvCode, policyNo, ds, strErr)

    End Function

    <WebMethod()> _
    Public Function getMcuUWOutstandingReq(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal gcNBR As String, ByVal ds As DataSet, ByRef strErr As String) As DataSet

        Dim objCRS As New CRS.CRS

        Return objCRS.getMcuUWOutstandingReq(strCompanyCode, strEnvCode, policyNo, gcNBR, ds, strErr)

    End Function

    <WebMethod()> _
    Public Function GetNBRPolicyMcu(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As Boolean

        Dim objCRS As New CRS.CRS
        Dim dt As DataTable = New DataTable()

        Return objCRS.GetNBRPolicyMcu(strCompanyCode, strEnvCode, policyNo, strErr)

    End Function

#End Region


#Region "Get NCB Records"

    <WebMethod(Description:="GetNCBRecords")> _
    Public Function GetNCB(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As DataTable


        Dim objCRS As New CRS.CRS

        Dim dt As New DataTable()

        Return objCRS.GetNCB(strCompanyCode, strEnvCode, policyNo, strErr)

    End Function

#End Region

#Region "Post sales call"

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePostSalesCallQuestions(ByRef dt As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePostSalesCallQuestions(dt, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function SavePostSalesCallQuestion(ByVal intMode As Integer, ByRef dtQuestion As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.SavePostSalesCallQuestion(intMode, dtQuestion, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function IsPostSalesCallQuestionInUse(ByVal intQuestionId As Integer, ByRef isUsed As Boolean, ByRef strErr As String) As Boolean
        Return CRSObj.IsPostSalesCallQuestionInUse(intQuestionId, isUsed, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function DeletePostSalesCallQuestion(ByVal intQuestionId As Integer, ByRef strErr As String) As Boolean
        Return CRSObj.DeletePostSalesCallQuestion(intQuestionId, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePostSalesCallProductSettings(ByRef dsSettings As DataSet, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePostSalesCallProductSettings(dsSettings, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePostSalesCallProductCategory(ByRef dtCategory As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePostSalesCallProductCategory(dtCategory, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function UpdatePostSalesCallProductSetting(ByVal dtSetting As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.UpdatePostSalesCallProductSetting(dtSetting, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function AddPostSalesCallProductSetting(ByVal dtSetting As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.AddPostSalesCallProductSetting(dtSetting, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function DeletePostSalesCallProductSetting(ByVal strProductId As String, ByRef strErr As String) As Boolean
        Return CRSObj.DeletePostSalesCallProductSetting(strProductId, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePostSalesCallProducts(ByVal strProductID As String, ByVal strPlanName As String, ByRef dtProduct As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePostSalesCallProducts(strProductID, strPlanName, dtProduct, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function CopySalesCallProductSetting(ByVal strFromProductID As String, ByVal strToProductID As String, ByRef strErr As String) As Boolean
        Return CRSObj.CopySalesCallProductSetting(strFromProductID, strToProductID, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function AddPostSalesCallProductQuestion(ByVal dtQuestion As DataTable, ByRef isExists As Boolean, ByRef strErr As String) As Boolean
        Return CRSObj.AddPostSalesCallProductQuestion(dtQuestion, isExists, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RemovePostSalesCallProductQuestion(ByVal strProductId As String, ByVal intQuestionId As Integer, ByRef strErr As String) As Boolean
        Return CRSObj.RemovePostSalesCallProductQuestion(strProductId, intQuestionId, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function SetPostSalesCallProductQuestionOrder(ByVal strProductId As String, ByVal intQuestionId As Integer, ByVal intNewOrder As Integer, ByRef strErr As String) As Boolean
        Return CRSObj.SetPostSalesCallProductQuestionOrder(strProductId, intQuestionId, intNewOrder, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function IsPostSalesCallProductSettingExists(ByVal strProductId As String, ByRef isExists As Boolean, ByRef strErr As String) As Boolean
        Return CRSObj.IsPostSalesCallProductSettingExists(strProductId, isExists, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetCoverage(ByVal strPolicyNo As String, ByRef dtCov As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.GetCoverage(strPolicyNo, dtCov, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePostSalesCallPolicyQuestion(ByVal strPolicyNo As String, ByRef dtQuestions As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePostSalesCallPolicyQuestion(strPolicyNo, dtQuestions, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function SavePostSalesCallPolicyAnswer(ByVal dtQuestion As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.SavePostSalesCallPolicyAnswer(dtQuestion, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function IsPostSalesCallCompleted(ByVal strPolicyNo As String, ByRef blnCompleted As Boolean, ByRef strErr As String) As Boolean
        Return CRSObj.IsPostSalesCallCompleted(strPolicyNo, blnCompleted, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrieveInvestmentFund(ByRef dtFund As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrieveInvestmentFund(dtFund, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function UpdateFundRiskLevel(ByVal dtFund As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.UpdateFundRiskLevel(dtFund, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function GetPolicyPostSalesCallType(ByVal strPolicyNo As String, ByRef callType As CRS.CRS.PostSalesCallType, ByRef strErr As String) As Boolean
        Return CRSObj.GetPolicyPostSalesCallType(strPolicyNo, callType, strErr)
    End Function

    <WebMethod()> _
    <SoapHeader("objDBHeader", direction:=SoapHeaderDirection.In)> _
    Public Function RetrievePolicyHighRiskFund(ByVal strPolicyNo As String, ByRef dtFund As DataTable, ByRef strErr As String) As Boolean
        Return CRSObj.RetrievePolicyHighRiskFund(strPolicyNo, dtFund, strErr)
    End Function

#End Region

#Region "Service Log Retrieve"
    <WebMethod(Description:="GetSerLogbyCriteria")> _
    Public Function GetSerLogbyCriteria(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByRef strErr As String, ByVal strType As String) As String
        Dim result As String
        Dim ds As New DataSet
        ds.Tables.Add(CRSObj.GetSerLogbycriteria(strCompanyCode, strEnvCode, StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, strErr, strType))

        Using ms As New IO.MemoryStream()
            ds.WriteXml(ms, System.Data.XmlWriteMode.WriteSchema)
            result = System.Text.Encoding.UTF8.GetString(ms.ToArray)
        End Using
        Return result
        'Return CRSObj.GetSerLogbycriteria(strCompanyCode, strEnvCode, StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, strErr, strType)
    End Function

    <WebMethod(Description:="GetSerLogbyCusIdOrCriteria")> _
    Public Function GetSerLogbyCusIdOrCriteria(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByVal isDescOrder As Boolean, ByRef strErr As String, ByVal strType As String) As String
        Dim result As String
        Dim ds As New DataSet
        ds.Tables.Add(CRSObj.GetSerLogbyCusIdOrCriteria(strCompanyCode, strEnvCode, StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, isDescOrder, strErr, strType))

        Using ms As New IO.MemoryStream()
            ds.WriteXml(ms, System.Data.XmlWriteMode.WriteSchema)
            result = System.Text.Encoding.UTF8.GetString(ms.ToArray)
        End Using
        Return result
        'Return CRSObj.GetSerLogbyCusIdOrCriteria(strCompanyCode, strEnvCode, StartDate, EndDate, strMediumCode, strEvtCatCode, strcustId, includeNonCust, isDescOrder, strErr, strType)
    End Function
#End Region

#Region "Policy Alternation"
    '<WebMethod(Description:="GetMarkin")> _
    'Public Function GetMarkin(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtMarkin As DataTable, ByRef strErrMsg As String) As Boolean

    '    Return CRSObj.GetMarkin(strCompanyCode, strEnvCode, strPolicy, dtMarkin, strErrMsg)
    'End Function

    '<WebMethod(Description:="GetMarkinHist")> _
    'Public Function GetMarkinHist(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtMarkinHist As DataTable, ByRef strErrMsg As String) As Boolean
    '    Return CRSObj.GetMarkinHist(strCompanyCode, strEnvCode, strPolicy, dtMarkinHist, strErrMsg)
    'End Function

    '<WebMethod(Description:="GetPendingMarkin")> _
    'Public Function GetPendingMarkin(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtPendingMarkin As DataTable, ByRef strErrMsg As String) As Boolean
    '    Return CRSObj.GetPendingMarkin(strCompanyCode, strEnvCode, strPolicy, dtPendingMarkin, strErrMsg)
    'End Function

    '<WebMethod(Description:="GetPendingMarkinHist")> _
    'Public Function GetPendingMarkinHist(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtPendingMarkinHist As DataTable, ByRef strErrMsg As String) As Boolean
    '    Return CRSObj.GetPendingMarkinHist(strCompanyCode, strEnvCode, strPolicy, dtPendingMarkinHist, strErrMsg)
    'End Function
#End Region

#Region "Macau Report List"
    <WebMethod(Description:="GetReportList")> _
    Public Function GetReportList(ByVal strCompanyCode As String, ByVal strEnvCode As String) As WSResponse(Of List(Of MCUReportList))
        Return CRSObj.GetReportList(strCompanyCode, strEnvCode)
    End Function
#End Region


#Region "ILAS Notification Letter"
    <WebMethod(Description:="LoadFundPopup")> _
    Public Function LoadFundPopup(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of List(Of RptILASFundDesc))
        Return CRSObj.LoadFundPopup(strCompanyCode, strEnvCode, strPolicy)
    End Function

    <WebMethod(Description:="GetRptILASNotificationLetterInfo")> _
    Public Function GetRptILASNotificationLetterInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByVal strCAMDatabase As String) As WSResponse(Of RptILASNotificationLetterInfo)
        Return CRSObj.GetRptILASNotificationLetterInfo(strCompanyCode, strEnvCode, strPolicy, strCAMDatabase)
    End Function
#End Region

#Region "Premium reminder call report"
    <WebMethod(Description:="GetPremCallRpt")> _
    Public Function GetPremCallRpt(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal channelCheckFlag As Boolean, ByVal strChannel As String) As WSResponse(Of List(Of PremCallRpt))
        Return CRSObj.GetPremCallRpt(strCompanyCode, strEnvCode, strDateFrom, strDateTo, channelCheckFlag, strChannel)
    End Function
#End Region

#Region "ILAS Product – Post Sale Call Report"
    <WebMethod(Description:="GetHKFIReport")> _
    Public Function GetHKFIReport(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal strType As String, ByVal strOrderType As String) As WSResponse(Of List(Of HKFIRpt))
        Return CRSObj.GetHKFIReport(strCompanyCode, strEnvCode, strDateFrom, strDateTo, strType, strOrderType)
    End Function
#End Region
#Region "Policy Letter"
    <WebMethod(Description:="GetPolicyInfo")> _
    Public Function GetPolicyInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of PolicyInfo)
        Return CRSObj.GetPolicyInfo(strCompanyCode, strEnvCode, strPolicy)
    End Function
#End Region
#Region "Payment Letter"
    '<WebMethod(Description:="GetPIPHCustomerID")> _
    'Public Function GetPIPHCustomerID(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of CustomerID)
    '    Return CRSObj.GetPIPHCustomerID(strCompanyCode, strEnvCode, strPolicy)
    'End Function

    '<WebMethod(Description:="GetCustomerAndAgentInfo")> _
    'Public Function GetCustomerAndAgentInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of List(Of CustomerInfo))
    '    Return CRSObj.GetCustomerAndAgentInfo(strCompanyCode, strEnvCode, strPolicy)
    'End Function

    '<WebMethod(Description:="GetPaymentInfo")> _
    'Public Function GetPaymentInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByVal strAG As String) As WSResponse(Of PaymentInfo)
    '    Return CRSObj.GetPaymentInfo(strCompanyCode, strEnvCode, strPolicy, strAG)
    'End Function
#End Region
#Region "Claims Audit"
    <WebMethod(Description:="Claims Audit")> _
    Public Function ClaimsAudit(ByVal CompanyID As String, ByVal Env As String, ByVal ClaimNo As String, ByVal PolicyNo As String, ByVal InsuredName As String, ByVal OccurNo As String, ByVal EventName As String) As WSResponse(Of Object)
        Return CRSObj.ClaimsAudit(CompanyID, Env, ClaimNo, PolicyNo, InsuredName, OccurNo, EventName)
    End Function
#End Region
End Class
