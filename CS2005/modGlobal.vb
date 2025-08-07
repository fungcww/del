'********************************************************************
' Admended By: Flora Leung
' Admended Function: EnvSetup
' Added Function: GetCodeTableValue
' Date: 14 Feb 2012
' Project: Project Leo Goal 3 Capsil
'
' Admended By: Peter Lam
' Admended Function: EnvSetup
' Date: 22 Feb 2012
' Project: Project Leo Goal 1 Phase 1
'
' Admended By: Peter Lam (ITDLSW002)
' Admended Function: GetLAPolicyValue
' Date: 17 Aug 2012
' Project: Production Fix 2012/08/17

' Admended By: Sampson Siu SS20151130
' Admended Function: ShowPolicy
' Date: 30 Nov 2015
' Project: Production Fix 2015/11/30 - Check policy type (LifeAsia/Capsil) of searched policy, and find policy info by policy type

' Admended By: Kay Tsang KT20161111
' Admended Function: add S301 environment, refactor clsJSONBusinessObject to use CRS_Util object
' Date: 11 Nov 2016
' Project: Tap & Go

' Admended By: Kay Tsang KT20170210
' Admended Function: fix GI detail page missing customer ID
' Date: 10 Feb 2017
' Project: GI detail page
'
' Admended By: Steven Liu SL20191121
' Admended Function: All alert message for QDAP refund extra payment
' Date: 2019-11-21
' Project: ITSR1356 QDAP followup
'
' Admended By: Keith Tong KT20201110
' Admended Function: Remaining limit for VHIS High End Medical Plan
' Date: 2020-11-10
' Project: ITSR-1488 CRS Minor Claims History
'
' Admended By: Keith Tong KT20201123
' Admended Function: CRS pop up the error message when searching a policy
' Date: 2020-11-23
' Project: SR#00128961 CRS pop up the error message when searching a policy
'
' Admended By: Lubin ITSR-3487 CRS Macau Phase 3
' Admended Function: Change the QuoteRpu, Add PrintFlag1 parameter to bo request.
' Date: 2022-11-3
' Project: ITSR-3487 CRS Macau Phase 3
'********************************************************************
' Admended By: Claudia Lai CL20220928
' Admended Function: Add fields for CI Claim Paid Termination
' Date: 2022-09-28
' Project: ITSR-3488 EasyCover/MyCover Claim Paid Termination
'********************************************************************
' Admended By: Gavin Wu
' Admended Function: Setup CCMWS Configuration
' Date: 2023-06-07
' Project: ITSR3162 & 4101
'********************************************************************
' Admended By: Oliver Ou 222834
' Admended Function:
' 1.Add New field “EB reference no.” And “Employee code” in search customer page
' Date: 2023-08-22
' Project: CRS Enhancement(General Enhance Ph3) Point A-1
'********************************************************************
' Admended By: Oliver Ou 222834
' Admended Function:
' 1. Customer alert maintenance
' 2. Enable boardcast prompt up message under customer level + all policies under same customer (PH Or PI)
' Date: 2023-11-01
' Project: CRS Enhancement(General Enhance Ph4) Point A-10
'********************************************************************
' Amend By:     Chrysan Cheng
' Date:         12 Nov 2024
' Changes:      CRS performer slowness - Policy Summary
'********************************************************************
' Description : HNW Expansion - Integrated Customer Search
' Date		  : 12 Jan 2025
' Author	  : Chrysan Cheng
'******************************************************************
' Description : AS400 to cloud
' Date		  : 18 Apr 2025
' Author	  : Chrysan Cheng
'******************************************************************

Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting.Channels.Http
Imports System.Net
Imports System.Configuration
Imports INGLife.Interface
Imports HKL.Interface
Imports System.Text
Imports System.Xml
Imports HashidsNet
Imports System.Security.Cryptography
Imports System.IO
Imports CRS.CRS
Imports System.Reflection
Imports ADODB
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Linq



Module Module1

    '**** Load test
#If STRESS <> 0 Then
    Public f As IO.FileInfo = New IO.FileInfo("StressTest_" & Format(Now, "yyyyMMddhhmmss") & ".txt")
    Public writer As IO.StreamWriter = f.CreateText()
#End If
    Public appSettings As App.Config.AppSettings = New App.Config.AppSettings
    ' For use with UPS
    Public gsUser, gsUser400, gsCSRName, gsCSRChiName, strUPSMenuCtrl As String
    Public g_LacComp, g_LahComp, g_QmanAsur, g_WinRemoteAsurQ, g_LAReplyAsurQ, g_WinLocalAsurQ, g_LacEnv, g_LahEnv As String
    Public gLoginEnvStr As String
    Public giUPSGrp As Decimal
    Public dtPriv As DataTable
    Public dtFuncList As DataTable

    ' Global Window handle

    'oliver 2023-11-30 added for Switch Over Code from Assurance to Bermuda
    Public IsAssurance As Boolean = True
    Public wndMain As frmCS2005_Asur = New frmCS2005_Asur
    'Public wndMain As New frmCS2005

    Public wndInbox As frmInbox

    ' .NET Remoting object
#If HKL = 1 Then
    Public objSec As HKL.Interface.ISecurity
    Public objCS As HKL.Interface.ICRS
#Else
    Public objSec As INGLife.Interface.ISecurity
    Public objCS As INGLife.Interface.ICS2005
    Public wsCRS As CRSWS.CRSWS
    'Public objCS As INGLife.CS2005.CS2005

#End If

    Public objSSO As ISSOCom

    ' Global settings
    Public gSearchLimit, gWindowLimit, gEnqFrom, gFAXSLEEP, gQryTimeOut As Integer
    Public gFaxNo, gFaxSrv As String
    Public gCrmMode As String
    ''AC, Change not to use advance compilation option - start
    'Public gSQL3, gcNBR As String
    Public gSQL3, gcNBR, gcCIW, gcMCS, gcRMLife, gcPOS, gcLCP As String
    Public gcMcuCIW, gcMcuNBR, gcMcuMCS, gcMcuPOS As String
    Public gcCRSServer, gcCRSDB, gcCRSDBMcu, gcCRSDBAsur, gcEPORTALDB, gcEPORTALDBMcu, gcNBSDB, gcNBSDBMcu As String
    'AC, Change not to use advance compilation option - end

    'Public blnStop() As Boolean

    ' Database/Table name
    Public strCIWConn, strCIWMcuConn, strLIDZConn, strAsurConn, strAsurMcuConn, strCAPSILConn, strMCSConn, strUPSConn, strCTRConn, strMPFConn, strICRConn, strMJCConn As String
    Public strPOSConn As String
    Public cCAPSIL, ORDUPO, ORDUCO, ORDUET, ORDUMC, ORDUNA, ORDCNA, ORDUAG, ORDURL As String
    'Public cCAPSIL As String
    Public strLastPolicy As String
    Public strSK() As String        ' Channels user can view
    Public blnIE20 As Boolean = True
    Public strValProj, strValConn As String

    'oliver 2023-11-30 updated for Switch Over Code from Assurance to Bermuda
    'Public strCompany As String
    Public strCompany As String = "ING"

    'Store LifeAsia Component System Table
    Public dsComponentSysTable As DataSet
    Public gobjMQQueHeader As Utility.Utility.MQHeader
    Public gobjDBHeader As Utility.Utility.ComHeader
    Public gobjPOSHeader As Utility.Utility.POSHeader

    'oliver 2024-7-25 added for Com 6
    Public gobjBmuMQQueHeader As Utility.Utility.MQHeader
    Public gobjBmuDBHeader As Utility.Utility.ComHeader
    Public gobjBmuPOSHeader As Utility.Utility.POSHeader

    Public gobjMcuMQQueHeader As Utility.Utility.MQHeader
    Public gobjMcuDBHeader As Utility.Utility.ComHeader
    Public gobjMcuPOSHeader As Utility.Utility.POSHeader

    Public gobjLacDBHeader As Utility.Utility.ComHeader
    Public gobjLahDBHeader As Utility.Utility.ComHeader
    Public gobjLacMQQueHeader As Utility.Utility.MQHeader
    Public gobjLahMQQueHeader As Utility.Utility.MQHeader
    Public gobjLacPOSHeader As Utility.Utility.POSHeader
    Public gobjLahPOSHeader As Utility.Utility.POSHeader

    Public gBusDate As Date 'ES09

    Public gSysTableLastUpdate As DateTime = Now.AddHours(-24)  'ITSR933 FG R3 Performance Tuning

    ' CIC Integration
    Public giCICTn As String    ' Current CIC call in Tel. no.

    Public strLASCAM As String

    'AC, Change not to use advance compilation option - start
    Public gUAT As Boolean = False
    'AC, Change not to use advance compilation option - end

    Public objNBA As NewBusinessAdmin.NBLifeAdmin 'ITDYKT

    Public IE As SHDocVw.InternetExplorer


    Public THandle As Long

    Public Declare Function BringWindowToTop Lib "user32" (ByVal hwnd As Long) As Long

    'Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As Object, ByVal lpWindowName As Object) As Long

    'Private TripleDes As New TripleDESCryptoServiceProvider

    ' ITSR-4063 
    Public isUHNWMember As Boolean = False
    Public isUHNWMemberMcu As Boolean = False
    'oliver 2024-7-31 added for Com 6
    Public isHNWMember As Boolean = False
    '    Public Sub RPC_Setup()

    '        Dim lngErrNo As Long
    '        Dim strErr As String
    '        Dim strConfigPath As String

    '#If RPC = "DCOM" Then
    '        objCS = New INGLife.CS2005.CS2005
    '        objsec = New INGLife.Security.DbSec.soap
    '#Else

    '        ' VS will run in \bin directory
    '        strConfigPath = Application.StartupPath
    '        '#If UAT <> 0 Then
    '        '        If InStr(strConfigPath, "\bin") > 0 Then
    '        '            strConfigPath = Replace(strConfigPath, "\bin", "")
    '        '        End If
    '        '#End If
    '        strConfigPath &= "\cs2005.exe.config"
    '        ' Read from configuration files instead
    '        RemotingConfiguration.Configure(strConfigPath)

    '        'Dim channel As New HttpChannel
    '        'ChannelServices.RegisterChannel(channel)
    '        'useAuthenticatedConnectionSharing,  useDefaultCredentials="true"
    '        'Dim remoteobj As Object = Activator.GetObject(GetType(comCS2005.CS2005), "tcp://10.10.18.237:13101/comCS2005.CS2005.soap")
    '        'Dim remoteobj1 As Object = Activator.GetObject(GetType(comCS2005.clsUtility), "tcp://10.10.18.237:13101/comCS2005.clsUtility.soap")

    '        'Dim Props As IDictionary
    '        'Dim remoteobj As Object = Activator.GetObject(GetType(ICS2005), "http://10.10.4.34/CS2005Service/INGLife.CS2005.CS2005.soap")
    '        'Props = ChannelServices.GetChannelSinkProperties(remoteobj)
    '        'Props("credentials") = CredentialCache.DefaultCredentials
    '        'objCS = CType(remoteobj, ICS2005)

    '        Dim Props As IDictionary

    '        If g_Comp = "ING" Then
    '            Dim remoteobj As Object = Activator.GetObject(GetType(ICS2005), ConfigurationSettings.AppSettings.Item("ICS2005"))
    '            objCS = CType(remoteobj, ICS2005)

    '            Dim remoteobj2 As Object = Activator.GetObject(GetType(INGLife.Interface.ISecurity), ConfigurationSettings.AppSettings.Item("ISecurity"))
    '            objSec = CType(remoteobj2, INGLife.Interface.ISecurity)
    '            'strCIWConn = objSec.ConnStr(g_ProjectAlias, g_Connection_CIW, g_UserType)
    '            'strCIWConn = objSec.ConnStr("CS2005", "CIWD101", "CSUSER")
    '        Else
    '            Dim remoteobj As Object = Activator.GetObject(GetType(ICRS), ConfigurationSettings.AppSettings.Item("ICRS"))
    '            objCS = CType(remoteobj, ICRS)

    '            Dim remoteobj2 As Object = Activator.GetObject(GetType(HKL.Interface.ISecurity), ConfigurationSettings.AppSettings.Item("ISecurityHKL"))
    '            objSec = CType(remoteobj2, HKL.Interface.ISecurity)

    '        End If

    '        ' Start SSO Changes
    '        Dim remoteobj3 As Object = Activator.GetObject(GetType(ISSOCom), ConfigurationSettings.AppSettings.Item("ISSO"))
    '        objSSO = CType(remoteobj3, ISSOCom)
    '        ' End SSO Changes

    '#End If
    '        'strCIWConn = objSec.ConnStr("CS2005", "CIWD101", "CSUSER")

    '#If UAT = 0 Then
    '        strCIWConn = objSec.ConnStr(g_ProjectAlias, g_Connection_CIW, g_UserType)

    '#Else
    '        Dim password As String
    '        Dim dbserver As String
    '        If g_Env.Substring(0, 1) = "U" Then
    '            password = "ingladev"
    '            dbserver = "hksqluat1"
    '        ElseIf g_Env.Substring(0, 1) = "S" Then
    '            password = "ingladev"
    '            dbserver = "hksqlsit1"
    '        Else
    '            password = "IngLAdev"
    '            dbserver = "hksqldev1"
    '        End If

    '        strCIWConn = "server=" & dbserver & ";database=INGCIW" & g_Env & ";Network=DBMSSOCN;uid=dev_las_ing;password=" & password & ";Connect Timeout=0"
    '        strPOSConn = "server=" & dbserver & ";database=INGPOS" & g_Env & ";Network=DBMSSOCN;uid=dev_las_ing;password=" & password & ";Connect Timeout=0"
    '        strMCSConn = "server=" & dbserver & ";database=INGCIW" & g_Env & ";Network=DBMSSOCN;uid=dev_las_ing;password=" & password & ";Connect Timeout=0"

    '        'strCIWConn = "server=HKSQLDEV1;database=INGCIWSIT01;Network=DBMSSOCN;uid=dev_las_ing;password=IngLAdev;Connect Timeout=0"
    '        'strPOSConn = "server=HKSQLDEV1;database=INGPOSSIT01;Network=DBMSSOCN;uid=dev_las_ing;password=IngLAdev;Connect Timeout=0"
    '        'strMCSConn = "server=HKSQLDEV1;database=INGCIWSIT01;Network=DBMSSOCN;uid=dev_las_ing;password=IngLAdev;Connect Timeout=0"

    '        'strMPFConn = objSec.ConnStr("MPFAES", "MPFAESCON", "WEB")
    '        strMJCConn = "server=" & dbserver & ";database=INGMCS" & g_Env & ";Network=DBMSSOCN;uid=dev_las_ing;password=" & password & ";Connect Timeout=0"
    '       ' strUPSConn = "server=HKSQLUAT1;database=INGPROFILEUAT01;Network=DBMSSOCN;uid=dev_las_ing;password=ingladev;Connect Timeout=0"
    '        strUPSConn = "server=" & dbserver & ";database=INGPROFILE" & g_Env & ";Network=DBMSSOCN;uid=dev_las_ing;password=" & password & ";Connect Timeout=0"
    '#End If
    '        'strUPSConn = objSec.ConnStr(g_ProjectAlias, g_Connection_UPS, g_UserType)
    '        'MsgBox(strUPSConn)
    '        'strMPFConn = ""
    '        'strICRConn = objSec.ConnStr("CS2005", "ICR", "CRSBATCH")
    '        'strICRConn = objSec.ConnStr("LAS", "INGICRUAT02", "LASUPDATE")

    '        If g_Comp = "ING" Then
    '#If UAT = 0 Then
    '            strUPSConn = objSec.ConnStr(g_ProjectAlias, g_Connection_UPS, g_UserType)
    '            strMPFConn = objSec.ConnStr("MPFAES", "MPFAESCON", "WEB")
    '            strMCSConn = objSec.ConnStr(g_ProjectAlias, g_Connection_MCS, g_UserType)
    '            strMJCConn = objSec.ConnStr(g_ProjectAlias_MJC, g_Connection_MJC, g_UserType_MJC)
    '#End If
    '            strICRConn = objSec.ConnStr(g_ProjectAlias, g_Connection_ICR, g_UserType_ICR)

    '            ' MJC
    '            'strMJCConn = objSec.ConnStr("MJC", "MJCDB", "UPDATE")
    '            'strMJCConn = objSec.ConnStr("LAS", "INGMCSUAT02", "LASUPDATE")
    '            'strMJCConn = objSec.ConnStr(g_ProjectAlias_MJC, g_Connection_MJC, g_UserType_MJC)
    '            strCTRConn = objSec.ConnStr("CS2005", "CTR", "CSUSER")
    '        End If

    '        'strCAPSILConn = objSec.ConnStr("CS2005", "ENQ40022", "CSUSER")
    '        strCAPSILConn = objSec.ConnStr(g_Capsil_Project, g_Capsil_Connection, g_Capsil_UserType)

    '        'strCAPSILConn = "Provider=MSDASQL.1;Password=LDDPOS2201;Extended Properties=""DSN=AS400_LPAR1_C;UID=LDDPOS22;PWD=LDDPOS2201;"""







    '        objSec = Nothing

    '    End Sub

    Public Function GET_IE() As SHDocVw.InternetExplorer
        'If IE Is Nothing Then
        '    IE = CreateObject("InternetExplorer.Application")
        'End If
        Return IE
    End Function

    Public Function CREATE_IE() As SHDocVw.InternetExplorer
        If IE Is Nothing Then
            IE = CreateObject("InternetExplorer.Application")
        Else
            Try
                IE.Quit()
                IE.Stop()
                IE = Nothing
            Catch ex As Exception
                IE = Nothing
            End Try
            IE = CreateObject("InternetExplorer.Application")
        End If
        'If IE Is Nothing Then
        'IE = CreateObject("InternetExplorer.Application")

        'End If
        Return IE
    End Function

    Public Sub RPC_Setup()

        Dim lngErrNo As Long = 0
        Dim strErr As String = ""
        Dim strConfigPath As String

        ' VS will run in \bin directory
        strConfigPath = Application.StartupPath

        'oliver 2023-11-30 updatedd for Switch Over Code from Assurance to Bermuda
        'strConfigPath &= "\remote.config"
        strConfigPath &= "\cs2005.exe.config"

        ' Read from configuration files instead
        RemotingConfiguration.Configure(strConfigPath)

        Dim Props As IDictionary
        'If ConfigurationSettings.AppSettings.Item("doNetRemoting") = False Then
        '    'objCS = New INGLife.CS2005.CS2005
        '    'objSec = New INGLife.DBAccess.Database
        'Else
        If g_Comp = "ING" Or g_Comp = "LAC" Or g_Comp = "LAH" Then
            Dim remoteobj As Object = Activator.GetObject(GetType(INGLife.Interface.ICS2005), ConfigurationSettings.AppSettings.Item("ICS2005"))
            objCS = CType(remoteobj, ICS2005)
            'objCS = New INGLife.CS2005.CS2005
            Dim remoteobj2 As Object = Activator.GetObject(GetType(INGLife.Interface.ISecurity), ConfigurationSettings.AppSettings.Item("ISecurity"))
            objSec = CType(remoteobj2, INGLife.Interface.ISecurity)
        Else 'HKL
            Dim remoteobj As Object = Activator.GetObject(GetType(ICRS), ConfigurationSettings.AppSettings.Item("ICRS"))
            objCS = CType(remoteobj, ICRS)

            Dim remoteobj2 As Object = Activator.GetObject(GetType(HKL.Interface.ISecurity), ConfigurationSettings.AppSettings.Item("ISecurityHKL"))
            objSec = CType(remoteobj2, HKL.Interface.ISecurity)
        End If
        'End If


        ' Start SSO Changes
        Dim remoteobj3 As Object = Activator.GetObject(GetType(ISSOCom), ConfigurationSettings.AppSettings.Item("ISSO"))
        objSSO = CType(remoteobj3, ISSOCom)
        ' End SSO Changes


        'AC - Change to use configuration setting - start
        If gUAT = False Then
            strCIWConn = objSec.ConnStr(g_ProjectAlias, g_Connection_CIW, g_UserType)
            'strCIWMcuConn = objSec.ConnStr(g_ProjectAlias, g_Connection_McuCIW, g_UserType)
            strCIWMcuConn = objSec.ConnStr("LAS", "MCUCIWPRD01", "LASUPDATE")
            strUPSConn = objSec.ConnStr(g_ProjectAlias, g_Connection_UPS, g_UserType)
            strMPFConn = objSec.ConnStr("MPFAES", "MPFAESCON", "WEB")
            strMCSConn = objSec.ConnStr(g_ProjectAlias, g_Connection_MCS, g_UserType)
            strMJCConn = objSec.ConnStr(g_ProjectAlias_MJC, g_Connection_MJC, g_UserType_MJC)
            strICRConn = objSec.ConnStr(g_ProjectAlias, g_Connection_ICR, g_UserType_ICR)
            strCTRConn = objSec.ConnStr("CS2005", "CTR", "CSUSER")
            strCAPSILConn = objSec.ConnStr(g_Capsil_Project, g_Capsil_Connection, g_Capsil_UserType)
            strAsurConn = objSec.ConnStr(g_ProjectAlias, "ASURCON", g_UserType)
        ElseIf g_Comp = "MCU" Then

            Dim objDB As DBLogon_NET.DBLogon.DBlogonNet     'DbAccess Object
            objDB = New DBLogon_NET.DBLogon.DBlogonNet
            objDB.Project = g_ProjectAlias
            objDB.ConnectionAlias = g_Connection_CIW
            objDB.User = g_UserType
            objDB.Connect()
            strCIWConn = objDB.GetDBstring()
            strCIWMcuConn = objDB.GetDBstring()



            objDB = New DBLogon_NET.DBLogon.DBlogonNet
            objDB.Project = g_ProjectAlias
            objDB.ConnectionAlias = g_Connection_UPS
            objDB.User = g_UserType
            objDB.Connect()
            strUPSConn = objDB.GetDBstring()
        End If

        'strCIWConn = CRS_Util.DecryptString(CRS_Component.APICallHelper.GetConnectionConfig(apiConn, "HKCIWConnection"))
        If gUAT Then
            strCIWConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "INGConnectionString")
            strCIWMcuConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCUConnectionString")
            strLIDZConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "LIDZConnectionString")
            'strCIWMcuConn = objSec.ConnStr("LAS", "MCUCIWPRD01", "LASUPDATE")
            strUPSConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UPSConnectionString") 'objSec.ConnStr(g_ProjectAlias, g_Connection_UPS, g_UserType)
            strMPFConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MPFConnectionString") 'objSec.ConnStr("MPFAES", "MPFAESCON", "WEB")
            strMCSConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCSConnectionString")
            strMJCConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MJCConnectionString") 'objSec.ConnStr(g_ProjectAlias_MJC, g_Connection_MJC, g_UserType_MJC)
            strICRConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ICRConnectionString") 'objSec.ConnStr(g_ProjectAlias, g_Connection_ICR, g_UserType_ICR)
            'strCTRConn = objSec.ConnStr("CS2005", "CTR", "CSUSER")
            strCAPSILConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CAPSILConnectionString") 'objSec.ConnStr(g_Capsil_Project, g_Capsil_Connection, g_Capsil_UserType)
            strAsurConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "AsurConnectionString") 'objSec.ConnStr(g_ProjectAlias, "ASURCON", g_UserType)
            'strAsurConn = strAsurConn.Replace("DB_Test", "L7LACT_REPL")
            'strAsurConn = "max pool size=500;server=10.175.42.136;database=DB_Test;Network=DBMSSOCN;uid=FwdAssurance_ODS_UAT;password=FWD@12345"
        Else
            Try
                strCIWConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "INGConnectionString"))
            Catch ex As Exception
                strCIWConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "INGConnectionString")
            End Try

            Try
                'strCIWMcuConn = objSec.ConnStr("LAS", "MCUCIWPRD01", "LASUPDATE")
                strCIWMcuConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCUConnectionString"))
            Catch ex As Exception
                strCIWMcuConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCUConnectionString")
            End Try

            Try
                strLIDZConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "LIDZConnectionString"))
            Catch ex As Exception
                strLIDZConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "LIDZConnectionString")
            End Try

            Try
                strUPSConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UPSConnectionString")) 'objSec.ConnStr(g_ProjectAlias, g_Connection_UPS, g_UserType)
            Catch ex As Exception
                strUPSConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "UPSConnectionString")
            End Try

            Try
                strMPFConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MPFConnectionString")) 'objSec.ConnStr("MPFAES", "MPFAESCON", "WEB")
            Catch ex As Exception
                strMPFConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MPFConnectionString")
            End Try

            Try
                strMCSConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCSConnectionString"))
            Catch ex As Exception
                strMCSConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MCSConnectionString")
            End Try

            Try
                strMJCConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MJCConnectionString")) 'objSec.ConnStr(g_ProjectAlias_MJC, g_Connection_MJC, g_UserType_MJC)
            Catch ex As Exception
                strMJCConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "MJCConnectionString")
            End Try

            Try
                strICRConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ICRConnectionString")) 'objSec.ConnStr(g_ProjectAlias, g_Connection_ICR, g_UserType_ICR)
            Catch ex As Exception
                strICRConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ICRConnectionString")
            End Try

            Try
                'strCTRConn = objSec.ConnStr("CS2005", "CTR", "CSUSER")
                strCAPSILConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CAPSILConnectionString")) 'objSec.ConnStr(g_Capsil_Project, g_Capsil_Connection, g_Capsil_UserType)
            Catch ex As Exception
                strCAPSILConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CAPSILConnectionString")
            End Try

            Try
                strAsurConn = CRS_Component.DecryptString(CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "AsurConnectionString")) 'objSec.ConnStr(g_ProjectAlias, "ASURCON", g_UserType)
                'strAsurConn = strAsurConn.Replace("DB_Test", "L7LACT_REPL")
                'strAsurConn = "max pool size=500;server=10.175.42.136;database=DB_Test;Network=DBMSSOCN;uid=FwdAssurance_ODS_UAT;password=FWD@12345"
            Catch ex As Exception
                strAsurConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "AsurConnectionString")
            End Try
        End If

        objCS.Env = giEnv
        'AC - Change to use configuration setting - start

    End Sub


    Public Sub RPC_Setup2()

        Dim lngErrNo As Long
        Dim strErr As String

        'GetCustomerIDByMobile("28502286")

        '#If UAT = 0 Then
        '        giEnv = 0
        '        cCAPSIL = "EAADTA"
        '        ORDUPO = cCAPSIL & ".ORDUPO"
        '        ORDUCO = cCAPSIL & ".ORDUCO"
        '        ORDUET = cCAPSIL & ".ORDUET"
        '        'ORDUMC = cCAPSIL & ".ORDUMC"
        '        ORDUNA = cCAPSIL & ".ORDUNA"
        '        ORDCNA = cCAPSIL & ".ORDCNA"
        '        ORDURL = cCAPSIL & ".ORDURL"
        '#Else
        '        Dim f As New frmEnvSel
        '        f.ShowDialog()
        '        giEnv = f.SelEnv
        '        f.Dispose()
        '        'giEnv = 8
        '        objCS.Env = giEnv

        '        If giEnv = 0 Then
        '            cCAPSIL = "LDDCPSLDB"
        '        Else
        '            cCAPSIL = "LDDCPS" & giEnv & "DB"
        '        End If

        '        ORDUPO = cCAPSIL & ".ORDUPO"
        '        ORDUCO = cCAPSIL & ".ORDUCO"
        '        ORDUET = cCAPSIL & ".ORDUET"
        '        'ORDUMC = cCAPSIL & ".ORDUMC"
        '        ORDUNA = cCAPSIL & ".ORDUNA"
        '        ORDCNA = cCAPSIL & ".ORDCNA"
        '        ORDURL = cCAPSIL & ".ORDURL"
        '#End If

        'giEnv = 0
        'objCS.Env = giEnv

        cCAPSIL = g_Capsil_Lib
        ORDUPO = cCAPSIL & ".ORDUPO"
        ORDUCO = cCAPSIL & ".ORDUCO"
        ORDUET = cCAPSIL & ".ORDUET"
        'ORDUMC = cCAPSIL & ".ORDUMC"
        ORDUNA = cCAPSIL & ".ORDUNA"
        ORDCNA = cCAPSIL & ".ORDCNA"
        ORDURL = cCAPSIL & ".ORDURL"

        Dim dtInfo As DataTable
        Try
            'dtInfo = objCS.GetSystemInfo(lngErrNo, strErr)
            GetSystemInfo(getCompanyCode(g_Comp), dtInfo)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        If Not dtInfo Is Nothing Then
            If dtInfo.Rows.Count > 0 Then

                'If CheckNewVersion(dtInfo.Rows(0).Item("cswsi_assembly_loc")) Then
                '    MsgBox("A new version is updated to your machine, please restart the application", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Auto Update")
                '    End
                'End If

                With dtInfo.Rows(0)
                    gSearchLimit = .Item("cswsi_search_limit")
                    gWindowLimit = .Item("cswsi_window_limit")
                    gEnqFrom = .Item("cswsi_enq_from")
                    gFaxNo = .Item("cswsi_fax_no")
                    gFaxSrv = .Item("cswsi_fax_server")
                    gFAXSLEEP = .Item("cswsi_fax_sleep")
                    gQryTimeOut = .Item("cswsi_query_timeout")

                    strCIWConn &= ";Connect Timeout=" & gQryTimeOut
                    strCIWMcuConn &= ";Connect Timeout=" & gQryTimeOut
                    strLIDZConn &= ";Connect Timeout=" & gQryTimeOut

                End With
                'ReDim blnStop(gWindowLimit)

                ' Check for correct version
                '#If UAT = 0 Then
                '                If dtInfo.Rows(0).Item("cswsi_version_no") <> Application.ProductVersion Then
                '                    MsgBox("The latest version of " & gSystem & " should be " & dtInfo.Rows(0).Item("cswsi_version_no") & ", please update your system first.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
                '                    End
                '                End If
                '#End If
            Else
                MsgBox("Error reading system info.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                End
            End If
        End If

        Dim dtCSR As DataTable
        Try
            dtCSR = objCS.GetCSR(gsUser, lngErrNo, strErr)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        If Not dtCSR Is Nothing Then
            If dtCSR.Rows.Count > 0 Then
                With dtCSR.Rows(0)
                    gsUser400 = IIf(IsDBNull(.Item("csrid_400")), gsUser, .Item("csrid_400"))
                    gsCSRName = IIf(IsDBNull(.Item("Name")), "", .Item("Name"))
                    gsCSRChiName = IIf(IsDBNull(.Item("CName")), "", .Item("CName"))
                End With
            Else
                gsUser400 = gsUser
                gsCSRName = gsUser
                gsCSRChiName = gsUser
            End If
        End If

        '#If UAT <> 0 Then
        '        blnIE20 = Not CheckIE20()
        '#Else
        '        blnIE20 = true
        '#End If

        'Dim dtProjection As DataTable
        'Dim lngErr As Long, strErrMsg As String

        'Dim PO() = {"H163001793"}

        'For i As Integer = 0 To PO.Length - 1
        '    dtProjection = objCS.GetPoProjection(PO(i), 10, lngErr, strErrMsg)
        '    MsgBox(PO(i) & vbCrLf & dtProjection.Rows(0).Item(0))
        'Next

        'LifeAsia global setting
        gobjDBHeader.UserID = gsUser
        gobjDBHeader.EnvironmentUse = g_Env
        gobjDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjDBHeader.CompanyID = g_Comp '"ING"
        gobjDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjMQQueHeader.QueueManager = g_Qman
        gobjMQQueHeader.RemoteQueue = g_WinRemoteQ
        gobjMQQueHeader.ReplyToQueue = g_LAReplyQ
        gobjMQQueHeader.LocalQueue = g_WinLocalQ
        gobjMQQueHeader.UserID = gsUser
        gobjMQQueHeader.CompanyID = g_Comp
        gobjMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        ' for logging to csw_mq_log when MQ error
        gobjMQQueHeader.EnvironmentUse = g_Env
        gobjMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjMQQueHeader.ConnectionAlias = g_Comp & "CIW" & g_Env

        'LifeAsia BMU global setting
        'oliver 2024-7-31 added for Com 6
        gobjBmuDBHeader.UserID = gsUser
        gobjBmuDBHeader.EnvironmentUse = g_Env
        gobjBmuDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjBmuDBHeader.CompanyID = g_BmuComp '"ING"
        gobjBmuDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjBmuMQQueHeader.QueueManager = g_Qman
        gobjBmuMQQueHeader.RemoteQueue = g_WinRemoteQ
        gobjBmuMQQueHeader.ReplyToQueue = g_LAReplyQ
        gobjBmuMQQueHeader.LocalQueue = g_WinLocalQ
        gobjBmuMQQueHeader.UserID = gsUser
        gobjBmuMQQueHeader.CompanyID = g_BmuComp
        gobjBmuMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        ' for logging to csw_mq_log when MQ error
        gobjBmuMQQueHeader.EnvironmentUse = g_Env
        gobjBmuMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjBmuMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjBmuMQQueHeader.ConnectionAlias = g_Comp & "CIW" & g_Env

        'Mcu Global Setting'
        gobjMcuDBHeader.UserID = gsUser
        gobjMcuDBHeader.EnvironmentUse = g_McuEnv
        gobjMcuDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjMcuDBHeader.CompanyID = g_McuComp '"ING"
        gobjMcuDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjMcuMQQueHeader.QueueManager = g_Qman
        gobjMcuMQQueHeader.RemoteQueue = g_WinRemoteMcuQ
        gobjMcuMQQueHeader.ReplyToQueue = g_LAReplyMcuQ
        gobjMcuMQQueHeader.LocalQueue = g_WinLocalMcuQ
        gobjMcuMQQueHeader.UserID = gsUser
        gobjMcuMQQueHeader.CompanyID = g_McuComp
        gobjMcuMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        ' for logging to csw_mq_log when MQ error
        gobjMcuMQQueHeader.EnvironmentUse = g_McuEnv

        gobjMcuMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjMcuMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjMcuMQQueHeader.ConnectionAlias = g_McuComp & "CIW" & g_Env

        'LAC Global Setting
        gobjLacDBHeader.UserID = gsUser
        gobjLacDBHeader.EnvironmentUse = g_LacEnv
        gobjLacDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjLacDBHeader.CompanyID = g_LacComp '"LAC"
        gobjLacDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjLacMQQueHeader.QueueManager = g_QmanAsur
        gobjLacMQQueHeader.RemoteQueue = g_WinRemoteAsurQ
        gobjLacMQQueHeader.ReplyToQueue = g_LAReplyAsurQ
        gobjLacMQQueHeader.LocalQueue = g_WinLocalAsurQ
        gobjLacMQQueHeader.UserID = gsUser
        gobjLacMQQueHeader.CompanyID = "LAC"
        gobjLacMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        gobjLacMQQueHeader.EnvironmentUse = g_LacEnv
        gobjLacMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjLacMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjLacMQQueHeader.ConnectionAlias = g_LacComp & "CIW" & g_LacEnv

        'LAH Global Setting
        gobjLahDBHeader.UserID = gsUser
        gobjLahDBHeader.EnvironmentUse = g_LahEnv
        gobjLahDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjLahDBHeader.CompanyID = g_LahComp '"LAH"
        gobjLahDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjLahMQQueHeader.QueueManager = g_QmanAsur
        gobjLahMQQueHeader.RemoteQueue = g_WinRemoteAsurQ
        gobjLahMQQueHeader.ReplyToQueue = g_LAReplyAsurQ
        gobjLahMQQueHeader.LocalQueue = g_WinLocalAsurQ
        gobjLahMQQueHeader.UserID = gsUser
        gobjLahMQQueHeader.CompanyID = "LAH"
        gobjLahMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        gobjLahMQQueHeader.EnvironmentUse = g_LahEnv
        gobjLahMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjLahMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjLahMQQueHeader.ConnectionAlias = g_LahComp & "CIW" & g_LahEnv


        'ITSR933 FG R3 Policy Number Change Start
        'If My.Settings.LAReady Then
        'Get LifeAsia Component System Table
        'Dim sContType As String = ""
        'Dim dsSendData As New DataSet
        'Dim objPOS As LifeClientInterfaceComponent.clsPOS
        'Try
        '    dsComponentSysTable = New DataSet
        '    objPOS = New LifeClientInterfaceComponent.clsPOS
        '    objPOS.MQQueuesHeader = gobjMQQueHeader
        '    objPOS.DBHeader = gobjDBHeader

        '    If Not objPOS.GetComponentSysTable(dsSendData, dsComponentSysTable, sContType, strErr) Then
        '        If strErr.Trim = "" Then
        '            MsgBox("Cannot get component system table!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        '        Else
        '            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        '        End If
        '        End
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        'Finally
        '    objPOS = Nothing
        '    dsSendData = Nothing
        'End Try

        'End If
        'ITSR933 FG R3 Policy Number Change End

        'Check Application Version No.
        If Not "Y".Equals(GetUatXml("SKVC")) Then
            Try
                Dim sAppVersion = GLSystemVersion()

                If sAppVersion <> Microsoft.VisualBasic.Strings.Left(Application.ProductVersion, InStrRev(Application.ProductVersion, ".0") - 1) Then
                    MsgBox("The latest version of " & gSystem & " should be " & sAppVersion & ", please re-logon windows to update your system first.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error") 'Temp Comment
                    appSettings.Logger.logger.Error("The latest version of " & gSystem & " should be " & sAppVersion & ", please re-logon windows to update your system first.")
                    End
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            End Try
        End If
    End Sub

    Public Function GetRelationValue(ByVal drCurrent As DataRow, ByVal strRelation As String, ByVal strField As String)

        Dim dr1 As DataRow
        Try
            dr1 = drCurrent.GetParentRow(strRelation)
            Return IIf(IsDBNull(dr1.Item(strField)), "", dr1.Item(strField))
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Sub FormatDate(ByVal sender As Object, ByVal cevent As ConvertEventArgs)

        If IsDBNull(cevent.Value) Then
            cevent.Value = ""
        ElseIf CType(cevent.Value, DateTime) = #1/1/1900# Then
            cevent.Value = ""
        Else
            cevent.Value = CType(cevent.Value, DateTime).ToString(gDateFormat)
        End If

    End Sub

    Public Sub FormatCPSDate(ByVal sender As Object, ByVal cevent As ConvertEventArgs)

        Dim val As String

        If IsDBNull(cevent.Value) Then
            cevent.Value = Format(#1/1/1900#, gDateFormat)
        Else
            Try
                val = CType(cevent.Value, String)
                If val.Length() = 7 And CInt(val) <> 0 Then
                    cevent.Value = Format(DateSerial(CInt(val.Substring(0, 3)) + 1800, CInt(val.Substring(3, 2)), CInt(val.Substring(5, 2))), gDateFormat)
                Else
                    'cevent.Value = Format(#1/1/1900#, gDateFormat)
                    cevent.Value = ""
                End If

            Catch ex As Exception
                cevent.Value = Format(#1/1/1900#, gDateFormat)
                cevent.Value = ""
            End Try
        End If

    End Sub

    Public Function VBDate(ByVal strDate As String) As Date
        Dim d As String

        If strDate.Length() = 7 And CInt(strDate) <> 0 Then
            VBDate = DateSerial(CInt(strDate.Substring(0, 3)) + 1800, CInt(strDate.Substring(3, 2)), CInt(strDate.Substring(5, 2)))
        Else
            VBDate = #1/1/1900#
        End If

    End Function

    Function CPS_SUB_DATE(ByVal datFrom As Date, ByVal datTo As Date)

        Dim de_days_to_month(13) As Integer
        Dim d1, d2, m1, m2, y1, y2, ndays As Integer

        ' No. of days from month
        de_days_to_month(1) = 0
        de_days_to_month(2) = 31
        de_days_to_month(3) = 59
        de_days_to_month(4) = 90
        de_days_to_month(5) = 120
        de_days_to_month(6) = 151
        de_days_to_month(7) = 181
        de_days_to_month(8) = 212
        de_days_to_month(9) = 243
        de_days_to_month(10) = 273
        de_days_to_month(11) = 304
        de_days_to_month(12) = 334
        de_days_to_month(13) = 365

        If IsDate(datFrom) And IsDate(datTo) Then
            d1 = Microsoft.VisualBasic.DateAndTime.Day(datFrom)
            m1 = Microsoft.VisualBasic.DateAndTime.Month(datFrom)
            y1 = Microsoft.VisualBasic.DateAndTime.Year(datFrom)
            d2 = Microsoft.VisualBasic.DateAndTime.Day(datTo)
            m2 = Microsoft.VisualBasic.DateAndTime.Month(datTo)
            y2 = Microsoft.VisualBasic.DateAndTime.Year(datTo)
            ndays = (y2 - y1) * 365 + (d2 - d1) + de_days_to_month(m2) - de_days_to_month(m1)
        Else
            ndays = 0
        End If

        Return ndays

    End Function
    ''' <summary>
    ''' Get AccountStatus description by statusCode
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    ''' <returns>Status Description</returns>
    Public Function GetAcStatus(ByVal strAcSts As String, Optional companyName As String = "") As String
        companyName = If(String.IsNullOrEmpty(companyName), g_Comp, companyName)
        Return APIServiceBL.QueryScalar(Of String)(companyName, "FRM_ACCOUNT_STATUS", New Dictionary(Of String, String)() From {
            {"statusCode", strAcSts}
            }, "")
    End Function

    Public Function GetCouponOption(ByVal strOption As String) As String

        Dim strSQL, strStatus As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select CouponOptionDesc From " & gcPOS & "vw_CouponOptionCodes Where CouponOptionCode = '" & Trim(strOption) & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strStatus = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strStatus

    End Function

    Public Function GetDivOption(ByVal strOption As String) As String

        Dim strSQL, strStatus As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select DividendOptionDesc From " & gcPOS & "vw_DividendOptionCodes Where DividendOptionCode = '" & Trim(strOption) & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strStatus = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strStatus

    End Function

    Public Function GetCCDRBanker(ByVal strCardNo As String) As String

        Dim strSQL, strBanker As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select cswcct_desc from Csw_ccd_type where cswcct_code = '" & Left(strCardNo, 6) & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strBanker = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strBanker

    End Function

    Public Function GetLocation(ByVal strAgtCode As String) As String

        Dim strSQL, strLoc As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select cswagi_unit_code+cswagi_loc_code From cswvw_cam_agent_info Where cswagi_agent_code in ('" & strAgtCode & "')"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strLoc = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            '#End If
            If gUAT = False Then
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
            'AC - Change to use configuration setting - end

        Catch sqlex As SqlClient.SqlException
            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            '#End If
            If gUAT = False Then
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
            'AC - Change to use configuration setting - end

        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strLoc

    End Function


    Public Function GetMcuLocation(ByVal strAgtCode As String) As String

        Dim strSQL, strLoc As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select cswagi_unit_code+cswagi_loc_code From cswvw_cam_agent_info Where cswagi_agent_code in ('" & strAgtCode & "')"

        sqlconnect.ConnectionString = strCIWMcuConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strLoc = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            '#End If
            If gUAT = False Then
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
            'AC - Change to use configuration setting - end

        Catch sqlex As SqlClient.SqlException
            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            '#End If
            If gUAT = False Then
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
            'AC - Change to use configuration setting - end

        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strLoc

    End Function

    Public Function GetPOSPlanType(ByVal strRiderCode As String) As String

        Dim strSQL, strPlanType As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select pospt_type from cswvw_pos_Plan_Type"
        strSQL = strSQL & " Where substring('" & Trim(strRiderCode) & "', pospt_begin , pospt_end) = pospt_code"
        strSQL = strSQL & " Order by len(pospt_code) DESC"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strPlanType = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strPlanType

    End Function

    Public Function GetClientID(ByVal strCustID As String) As String

        Dim strSQL, strID As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select CNANO from ORDCNA where cnaco = '' and CNACIW = " & strCustID

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strID = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strID

    End Function

    'Public Function GetCSRName(ByVal strCSRID As String) As DataTable

    '    Dim sqlconnect As New SqlConnection
    '    Dim strSQL As String
    '    Dim sqlda As SqlDataAdapter
    '    Dim dt As New DataTable

    '    strSQL = "select CSRID, Name, CName from CSR "

    '    If strCSRID <> "" Then
    '        strSQL &= "Where CSRID = '" & Trim(strCSRID) & "'"
    '    End If

    '    sqlconnect.ConnectionString = strCIWConn

    '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)

    '    Try
    '        sqlda.Fill(dt)
    '    Catch sqlex As SqlClient.SqlException
    '        lngErrNo = sqlex.Number
    '        strErrMsg = "GetSystemInfo - " & sqlex.ToString
    '    Catch ex As Exception
    '        lngErrNo = -1
    '        strErrMsg = "GetSystemInfo - " & ex.ToString
    '    End Try

    '    Return dt


    '    Dim strSQL, strName As String
    '    Dim sqlconnect As New SqlConnection
    '    Dim sqlcmd As New SqlCommand

    '    strSQL = "Select * from CSR where CSRID = '" & strCSRID & "'"

    '    sqlconnect.ConnectionString = strCIWConn
    '    sqlconnect.Open()

    '    sqlcmd.CommandText = strSQL
    '    sqlcmd.Connection = sqlconnect

    '    Try
    '        strName = sqlcmd.ExecuteScalar()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Catch sqlex As SqlClient.SqlException
    '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Finally
    '        sqlconnect.Close()
    '    End Try

    '    sqlcmd.Dispose()
    '    sqlconnect.Dispose()

    '    Return strName

    'End Function

    'Public Function GetChiCSRName(ByVal strCSRID As String) As String

    '    Dim strSQL, strName As String
    '    Dim sqlconnect As New SqlConnection
    '    Dim sqlcmd As New SqlCommand

    '    strSQL = "Select CName from CSR where CSRID = '" & strCSRID & "'"

    '    sqlconnect.ConnectionString = strCIWConn
    '    sqlconnect.Open()

    '    sqlcmd.CommandText = strSQL
    '    sqlcmd.Connection = sqlconnect

    '    Try
    '        If Not IsDBNull(sqlcmd.ExecuteScalar) Then
    '            strName = sqlcmd.ExecuteScalar()
    '        Else
    '            strName = ""
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Catch sqlex As SqlClient.SqlException
    '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Finally
    '        sqlconnect.Close()
    '    End Try

    '    sqlcmd.Dispose()
    '    sqlconnect.Dispose()

    '    Return strName

    'End Function

    Public Function GetCCDRejectCode(ByVal strRjCode As String) As String

        Dim strSQL, strRj As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select CCDRRejectReasonDesc from CCDRRejectReasonCodes Where CCDRRejectReasonCode = '" & strRjCode & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strRj = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strRj

    End Function

    Public Function GetDDARejectCode(ByVal strRjCode As String) As String

        Dim strSQL, strRj As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select DDARejectReasonDesc From DDARejectReasonCodes Where DDARejectReasonCode = '" & strRjCode & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strRj = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strRj

    End Function

    Public Function GetAgentFromLicByAPI(ByVal strLicense As String, ByVal strType As String) As String

        Dim strAgt As String
        Dim retDs As DataSet
        Dim strComp As String = g_Comp
        If strType = "A" Then
            strComp = g_LacComp
        End If

        'strSQL = "select * from cswvw_agent_license where " & strLicense
        Try
            'strAgt = sqlcmd.ExecuteScalar()
            retDs = APIServiceBL.CallAPIBusi(strComp, "GET_AGENT_FROM_LIC", New Dictionary(Of String, String)() From {
                                   {"CRLicenseNo", " and " & strLicense}
                                })
            If Not retDs.Tables(0) Is Nothing Then
                If retDs.Tables(0).Rows.Count > 0 Then
                    strAgt = retDs.Tables(0).Rows(0).Item("camalt_agent_no")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'sqlcmd.Dispose()
        'sqlconnect.Dispose()

        Return strAgt

    End Function

    Public Function DECtoBIN(ByVal lngDecimalNumber As Decimal) As String
        On Error Resume Next
        Dim bytBit As Byte
        Dim strValue As String

        Do Until (lngDecimalNumber <= 0)
            bytBit = IIf((lngDecimalNumber / 2) - Fix(lngDecimalNumber / 2) <> 0, 1, 0)
            lngDecimalNumber = Fix(lngDecimalNumber / 2)
            strValue = CStr(bytBit) & strValue
        Loop
        DECtoBIN = Right(StrDup(95, "0") & strValue, 95)

    End Function

    Public Function OpenWindow(ByVal w As Form, ByVal p As Object) As Boolean
        Dim fs() As Form = wndMain.MdiChildren()
        If fs.Length > gWindowLimit Then
            MsgBox("You can open at most " & gWindowLimit & " windows, please close some and try again.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Window Limit")
            Return False
        Else
            w.MdiParent = p
            w.WindowState = FormWindowState.Normal
            w.Show()
            Return True
        End If
    End Function

    Public Function ShowWindow(ByVal w As Form, ByVal p As Object, ByVal strFindWndTitle As String) As Boolean
        Dim f As Form = FindWindow(p, strFindWndTitle)

        If f IsNot Nothing Then
            f.Focus()
            Return True
        Else
            If Not OpenWindow(w, p) Then
                w.Dispose()
                Return False
            Else
                Return True
            End If
        End If
    End Function

    ''' <summary>
    ''' Find the MDI children form from parent form
    ''' </summary>
    ''' <param name="p">MDI parent form</param>
    ''' <param name="strFindWndTitle">Children form title text</param>
    ''' <returns>Return the form object if found, otherwise return nothing</returns>
    Public Function FindWindow(p As Object, strFindWndTitle As String) As Form
        Dim fs() As Form = p.MdiChildren()

        For Each f As Form In fs
            If InStr(f.Text, strFindWndTitle) <> 0 Then
                Return f
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Check whether the MDI children form exists in the parent form
    ''' </summary>
    ''' <param name="p">MDI parent form</param>
    ''' <param name="strFindWndTitle">Children form title text</param>
    ''' <returns>Return true if exist, otherwise return false</returns>
    Public Function ExistWindow(p As Object, strFindWndTitle As String) As Boolean
        Return FindWindow(p, strFindWndTitle) IsNot Nothing
    End Function

    Public Function GetClientList(ByVal strCustID As String) As String()

        Dim strSQL, strID() As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim sqlReader As SqlDataReader

        strSQL = "Select CNANO from ORDCNA where cnaco = '' and CNACIW = " & strCustID

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            sqlReader = sqlcmd.ExecuteReader
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Dim i As Integer = 0
        While sqlReader.Read
            ReDim Preserve strID(i)
            strID(i) = sqlReader.GetString(0)
            i += 1
        End While

        sqlReader.Close()
        sqlconnect.Close()
        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strID

    End Function

    Public Function GetDIRemark(ByVal strTypeIns As String, ByVal strTBL As String) As String

        Dim strSQL, strRmk As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select cswtbr_desc from " & serverPrefix & "csw_tbl_rmk " &
            " Where cswtbr_ins_type = '" & strTypeIns & "'" &
            " And cswtbr_tbl_code = '" & strTBL & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strRmk = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strRmk

    End Function

    Public Function CheckIE20() As Boolean

        Dim strSQL, strFld As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select cswmfd_field_name From " & serverPrefix & "csw_mq_func_detail Where cswmfd_func = 'COUH' And cswmfd_field_name = 'ErrDesc'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strFld = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return (strFld Is Nothing OrElse strFld.Length = 0)

    End Function

    Public Function GetChannel(ByVal strChl As String) As String()

        Dim strSQL, strSK() As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim drChl As SqlDataReader
        Dim i As Integer = 0
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select cswsum_unit_code from " & serverPrefix & "csw_special_unitcode_map " &
            " Where rtrim(cswsum_desc) in (" & strChl & ")"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            drChl = sqlcmd.ExecuteReader
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        While drChl.Read
            ReDim Preserve strSK(i)
            strSK(i) = drChl.GetString(0)
            i += 1
        End While

        drChl.Close()
        sqlconnect.Close()
        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strSK

    End Function

    Public Function GetChiAddr(ByRef dtNA As DataTable) As String

        Dim strSQL, strRmk As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtSQLNA As New DataTable

        Dim strNAList As String
        If dtNA.Rows.Count > 0 Then
            With dtNA
                strNAList = "'" & .Rows(0).Item("ClientID") & "'"
                For i As Integer = 1 To .Rows.Count - 1
                    strNAList &= ",'" & .Rows(i).Item("ClientID") & "'"
                Next
            End With
        End If

        strSQL = "Select DISTINCT o.*, addr.AddrProof from ORDCNA o LEFT JOIN capsil_client_role cliRole ON cliRole.CapClientNum = o.CNANO AND cliRole.CapClientRole = 'O' and cliRole.RiderNo = '00' LEFT JOIN CustomerAddress addr ON o.CNACIW = addr.CustomerID AND o.CNAEAT = addr.AddressTypeCode    " &
            " Where cliRole.RiderNo = '00' AND  cliRole.CapClientRole = 'O' AND  o.cnaco = '' and o.CNANO IN (" & strNAList & ")"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(dtSQLNA)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        dtNA.Columns.Add("AddrProof")

        Dim dr1 As DataRow()
        For i As Integer = 0 To dtNA.Rows.Count - 1
            With dtNA.Rows(i)
                dr1 = dtSQLNA.Select("CNANO = '" & .Item("ClientID") & "'")
                If dr1.Length > 0 Then
                    Try
                        .Item("ChiName") = Trim(IIf(IsDBNull(dr1(0).Item("CNALNM")), "", dr1(0).Item("CNALNM"))) &
                                Trim(IIf(IsDBNull(dr1(0).Item("CNAFNM")), "", dr1(0).Item("CNAFNM")))
                        .Item("CoCName") = IIf(IsDBNull(dr1(0).Item("CNACNM")), "", dr1(0).Item("CNACNM"))
                        .Item("CAddressLine1") = IIf(IsDBNull(dr1(0).Item("CNAAD1")), "", dr1(0).Item("CNAAD1"))
                        .Item("CAddressLine2") = IIf(IsDBNull(dr1(0).Item("CNAAD2")), "", dr1(0).Item("CNAAD2"))
                        .Item("CAddressLine3") = IIf(IsDBNull(dr1(0).Item("CNAAD3")), "", dr1(0).Item("CNAAD3"))
                        .Item("CAddressCity") = IIf(IsDBNull(dr1(0).Item("CNAAD4")), "", dr1(0).Item("CNAAD4"))
                        .Item("AddrProof") = IIf(IsDBNull(dr1(0).Item("AddrProof")), "", dr1(0).Item("AddrProof"))
                    Catch ex As Exception
                    End Try
                    .AcceptChanges()
                End If
            End With
        Next

    End Function

    Public Function WriteLog(ByVal strLogEntry As String, ByVal writer As IO.StreamWriter)
        writer.WriteLine(strLogEntry)
        writer.Flush()
    End Function

    Public Function CheckLimit(ByVal dt As DataTable) As String
        CheckLimit = ""
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("ContFlag") = "Y" Then
                    CheckLimit = "More than 100 records returned, please change ""From Date"" to view previous data."
                End If
            End If
        End If
    End Function

    Public Function DCARStatus(ByVal strSts As String) As String

        Select Case strSts
            Case "A"
                Return "Active"
            Case "C"
                Return "Cancelled"
            Case "S"
                Return "Suppressed"
        End Select
        Return ""

    End Function

    Public Function CheckNewVersion(ByVal strLoc As String) As Boolean

        Dim strCmd As String
        Dim retval As Integer
        'System.Diagnostics.FileVersionInfo.GetVersionInfo("cs2005.exe")
        strCmd = "REN " & Application.StartupPath & "\cs2005.exe cs2005.exe." & Format(Now, "yyyyMMddHHmmss") & ".bak"
        retval = Shell(strCmd, AppWinStyle.MaximizedFocus, True)
        strCmd = "XCOPY " & strLoc & "*.* /s/e/r/y " & Application.StartupPath
        retval = Shell(strCmd, AppWinStyle.MaximizedFocus, True)
        Return IIf(retval = 0, True, False)

    End Function

    Public Function GetCustStatus(ByVal strcustID As String) As String

        Dim strSQL, strFld As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "SELECT 'Status' = Case When secsup_lock_flag <> 3 Then 'Active' Else 'Locked' End From sec_user_profile " &
                 " Where secsup_user_id='" & strcustID & "'"

        sqlconnect.ConnectionString = strCTRConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strFld = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strFld

    End Function

    Public Function GetPersonalInfoAddress(ByVal strCustID As String, ByVal strClientID As String,
                                            ByRef strErrMsg As String, ByRef blnCovSmoker As Boolean,
                                            ByRef dtPersonal As DataTable, ByRef dtAddress As DataTable,
                                            ByRef dtCIWPersonInfo As DataTable, Optional ByVal blnLoadPol As Boolean = True) As Boolean
        Dim sqlConn As New SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strSQL As String

        Dim lngErrNo As Long = 0
        Dim lngCnt As Long

        GetPersonalInfoAddress = False

        Try

            ' Temp add to handle duplicate clientid
            Dim strClient() As String

            'oliver 2024-6-6 added for Table_Relocate_Sprint13
            'strClient = GetClientList(strCustID)
            strClient = New String() {strCustID}

            If blnLoadPol Then
                'AC - Change to use configuration setting - start
                'If UAT <> 0 Then
                '    'objCS.Env = giEnv
                'End If
                'AC - Change to use configuration setting - start

                If Not strClient Is Nothing AndAlso strClient.Length > 0 Then

                    'sqldt = objCS.GetPolicyList(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    sqldt = GetPolicyListCIW(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        Exit Function
                    End If

                    For I As Integer = 1 To strClient.Length - 1
                        'dtAddress = objCS.GetPolicyList(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        dtAddress = GetPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                        If lngErrNo <> 0 Then
                            Exit Function
                        End If

                        For K As Integer = 0 To dtAddress.Rows.Count - 1
                            sqldt.Rows.Add(dtAddress.Rows(K).ItemArray)
                        Next
                    Next

                    'ds.Tables.Add(sqldt)
                Else
                    'sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    sqldt = GetPolicyListCIW(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Function
                    End If

                    ' ds.Tables.Add(sqldt)
                End If

                ' Check if in any coverage, the insured is marked as smoker
                For I As Integer = 0 To sqldt.Rows.Count - 1
                    If sqldt.Rows(I).Item("PolicyRelateCode") = "PI" And sqldt.Rows(I).Item("SMCODE") = "S" Then
                        blnCovSmoker = True
                        Exit For
                    End If
                Next
            End If

            'strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            'strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

            sqlConn.ConnectionString = strCIWConn

            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlDA.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")


            ' Use CIW
            ' Enhancement for PDPO - Add OptOutOtherFlag column
            strSQL = "Select c.CustomerID, NamePrefix, FirstName, NameSuffix, ChiLstNm+ChiFstNm as ChiName, CoName, CoCName, " &
                " DateOfBirth, Gender, MaritalStatusCode, SmokerFlag, LanguageCode, GovernmentIDCard, OptOutFlag, " &
                " EmailAddr, UseChiInd, g.AgentCode, ISNULL(g.UnitCode,'') as Agency, " &
                " AddressTypeCode, AddressLine1, AddressLine2, AddressLine3, AddressCity, " &
                " AddressPostalCode, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, EMailID, BadAddress, AddrProof, c.CountryCode, " &
                " CustomerStatusCode, CustomerTypeCode, PhoneMobile, PhonePager, PassportNumber, Occupation, BirthPlace, AgeAdmInd, c.OptOutOtherFlag " &
                " From CustomerAddress a, Customer c LEFT JOIN AgentCodes g " &
                " ON c.AgentCode = g.AgentCode " &
                " Where addresstypecode in ('R','B','I','J') and c.CustomerID = '" & strCustID.Replace("'", "''") & "' " &
                " And c.CustomerID = a.CustomerID"
            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            dtAddress = New DataTable("CustomerAddress")
            Try
                sqlDA.Fill(dtAddress)
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            'strSQL = "Select cswdgm_cust_id as CustomerID, cswdgm_optout_email as OptEmail, cswdgm_optout_call as OptCall,"
            'strSQL &= " cswdgm_rating as Rating, cswdgm_no_of_dep as DependNo, cswdgm_edu_level as EduLevel,"
            ''strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup"
            'strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup, cswdgm_optout_NPS"
            'strSQL &= " From csw_demographic"
            'strSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"
            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'dtCIWPersonInfo = New DataTable("CIWCustomer")
            'Try
            '    sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            '    sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            '    sqlDA.Fill(dtCIWPersonInfo)
            'Catch sqlex As SqlClient.SqlException
            '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'End Try

            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            dtCIWPersonInfo = GetCIWCustomer(strCustID)

            sqlConn.Dispose()

            'Use CAPSIL

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    'objCS.Env = giEnv
            '    objCS.Env = 0
            'End If
            'AC - Change to use configuration setting - end


            'dtPersonal = objCS.GetORDUNA("'" & strClientID.Replace("'", "''") & "'", lngErrNo, strErrMsg)
            If Not strClient Is Nothing AndAlso strClient.Length > 0 Then
                dtPersonal = objCS.GetORDUNA("'" & strClient(0) & "'", lngErrNo, strErrMsg)
            End If
            'Handle Error in GetChiAddr() if no record found
            If Not dtPersonal Is Nothing Then
                If dtPersonal.Rows.Count > 0 Then
                    GetChiAddr(dtPersonal)
                End If
                dtPersonal.TableName = "CustomerAddress"
            End If

            If Not dtPersonal Is Nothing AndAlso Not dtAddress Is Nothing Then
                If dtPersonal.Rows.Count > 0 AndAlso dtAddress.Rows.Count > 0 Then
                    dtPersonal.Rows(0).Item("CustomerStatusCode") = dtAddress.Rows(0).Item("CustomerStatusCode")
                    dtPersonal.Rows(0).Item("OptOutFlag") = dtAddress.Rows(0).Item("OptOutFlag")
                    dtPersonal.Rows(0).Item("MaritalStatusCode") = dtAddress.Rows(0).Item("MaritalStatusCode")
                    dtPersonal.Rows(0).Item("PhoneMobile") = dtAddress.Rows(0).Item("PhoneMobile")
                    dtPersonal.Rows(0).Item("PhonePager") = dtAddress.Rows(0).Item("PhonePager")
                    dtPersonal.Rows(0).Item("PassportNumber") = dtAddress.Rows(0).Item("PassportNumber")
                    dtPersonal.Rows(0).Item("Occupation") = dtAddress.Rows(0).Item("Occupation")
                    dtPersonal.Rows(0).Item("BirthPlace") = dtAddress.Rows(0).Item("BirthPlace")
                    Try
                        dtPersonal.Columns.Add("AgeAdmInd", Type.GetType("System.String"))
                    Catch ex As Exception
                    End Try
                    dtPersonal.Rows(0).Item("AgeAdmInd") = dtAddress.Rows(0).Item("AgeAdmInd")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column START
                    Try
                        dtPersonal.Columns.Add("OptOutOtherFlag", Type.GetType("System.String"))
                    Catch ex As Exception
                    End Try
                    dtPersonal.Rows(0).Item("OptOutOtherFlag") = dtAddress.Rows(0).Item("OptOutOtherFlag")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column END
                End If
            End If
            GetPersonalInfoAddress = True

        Catch ex As Exception
            strErrMsg = ex.ToString
        End Try

    End Function

    Public Function GetPersonalInfoAddress4Customer(ByVal strCustID As String, ByVal strClientID As String,
                                           ByRef strErrMsg As String, ByRef blnCovSmoker As Boolean,
                                           ByRef dtPersonal As DataTable, ByRef dtAddress As DataTable,
                                           ByRef dtCIWPersonInfo As DataTable, Optional ByVal blnLoadPol As Boolean = True,
                                           Optional ByVal strCompanyID As String = "ING") As Boolean
        Dim sqlConn As New SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strSQL, strConn As String

        Dim lngErrNo As Long = 0
        Dim lngCnt As Long

        GetPersonalInfoAddress4Customer = False
        'Updated at 2023-8-21 by oliver for fixed the bug which is Personal Information Not Found Under Customer Search  
        'strConn = IIf(strCompanyID.Equals("Bermuda"), strCIWConn, strLIDZConn)
        strConn = IIf(strCompanyID.Equals("ING"), strCIWConn, strLIDZConn)
        Try

            ' Temp add to handle duplicate clientid
            Dim strClient() As String

            'strClient = GetClientList(strCustID)
            strClient = New String() {strCustID}

            If blnLoadPol Then
                'AC - Change to use configuration setting - start
                'If UAT <> 0 Then
                '    'objCS.Env = giEnv
                'End If
                'AC - Change to use configuration setting - start

                If Not strClient Is Nothing AndAlso strClient.Length > 0 Then

                    sqldt = objCS.GetPolicyList(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    'sqldt = GetPolicyListCIW(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        Exit Function
                    End If

                    For I As Integer = 1 To strClient.Length - 1
                        'dtAddress = objCS.GetPolicyList(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                        'oliver 2023-12-6 updated for Switch Over Code from Assurance to Bermuda 
                        'dtAddress = GetPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        If IsAssurance Then
                            dtAddress = GetAsurPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        Else
                            dtAddress = GetPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        End If

                        If lngErrNo <> 0 Then
                            Exit Function
                        End If

                        For K As Integer = 0 To dtAddress.Rows.Count - 1
                            sqldt.Rows.Add(dtAddress.Rows(K).ItemArray)
                        Next
                    Next

                    'ds.Tables.Add(sqldt)
                Else
                    'sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    sqldt = GetPolicyListCIW(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Function
                    End If

                    ' ds.Tables.Add(sqldt)
                End If

                ' Check if in any coverage, the insured is marked as smoker
                For I As Integer = 0 To sqldt.Rows.Count - 1
                    If sqldt.Rows(I).Item("PolicyRelateCode") = "PI" And sqldt.Rows(I).Item("SMCODE") = "S" Then
                        blnCovSmoker = True
                        Exit For
                    End If
                Next
            End If

            'strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            'strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

            sqlConn.ConnectionString = strConn

            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlDA.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")


            ' Use CIW
            ' Enhancement for PDPO - Add OptOutOtherFlag column
            strSQL = "Select c.CustomerID, NamePrefix, FirstName, NameSuffix, ChiLstNm+ChiFstNm as ChiName, CoName, CoCName, " &
                " DateOfBirth, Gender, MaritalStatusCode, SmokerFlag, LanguageCode, GovernmentIDCard, OptOutFlag, " &
                " EmailAddr, UseChiInd, g.AgentCode, ISNULL(g.UnitCode,'') as Agency, " &
                " AddressTypeCode, AddressLine1, AddressLine2, AddressLine3, AddressCity, " &
                " AddressPostalCode, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, EMailID, BadAddress, AddrProof, c.CountryCode, " &
                " CustomerStatusCode, CustomerTypeCode, PhoneMobile, PhonePager, PassportNumber, Occupation, BirthPlace, AgeAdmInd, c.OptOutOtherFlag " &
                " From CustomerAddress a, Customer c LEFT JOIN AgentCodes g " &
                " ON c.AgentCode = g.AgentCode " &
                " Where addresstypecode in ('R','B','I','J') and c.CustomerID = '" & strCustID.Replace("'", "''") & "' " &
                " And c.CustomerID = a.CustomerID"
            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            dtAddress = New DataTable("CustomerAddress")
            Try
                sqlDA.Fill(dtAddress)
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            'strSQL = "Select cswdgm_cust_id as CustomerID, cswdgm_optout_email as OptEmail, cswdgm_optout_call as OptCall,"
            'strSQL &= " cswdgm_rating as Rating, cswdgm_no_of_dep as DependNo, cswdgm_edu_level as EduLevel,"
            ''strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup"
            'strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup, cswdgm_optout_NPS"
            'strSQL &= " From csw_demographic"
            'strSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"
            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'dtCIWPersonInfo = New DataTable("CIWCustomer")
            'Try
            '    sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            '    sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            '    sqlDA.Fill(dtCIWPersonInfo)
            'Catch sqlex As SqlClient.SqlException
            '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'End Try

            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            dtCIWPersonInfo = GetCIWCustomer(strCustID, strCompanyID)

            sqlConn.Dispose()

            'Use CAPSIL

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    'objCS.Env = giEnv
            '    objCS.Env = 0
            'End If
            'AC - Change to use configuration setting - end


            'dtPersonal = objCS.GetORDUNA("'" & strClientID.Replace("'", "''") & "'", lngErrNo, strErrMsg)
            If Not strClient Is Nothing AndAlso strClient.Length > 0 Then
                'dtPersonal = objCS.GetORDUNA("'" & strClient(0) & "'", lngErrNo, strErrMsg)
                'dtPersonal = objCS.GetCIWCustInfo(strClient(0))
                dtPersonal = GetCIWCustInfo(strClient(0), strConn)
            End If
            'Handle Error in GetChiAddr() if no record found
            'If Not dtPersonal Is Nothing Then
            '    If dtPersonal.Rows.Count > 0 Then
            '        GetChiAddr(dtPersonal)
            '    End If
            '    dtPersonal.TableName = "CustomerAddress"
            'End If

            If Not dtPersonal Is Nothing AndAlso Not dtAddress Is Nothing Then
                Try
                    dtPersonal.Columns.Add("AgeAdmInd", Type.GetType("System.String"))
                Catch ex As Exception
                End Try
                Try
                    dtPersonal.Columns.Add("OptOutOtherFlag", Type.GetType("System.String"))
                Catch ex As Exception
                End Try

                If dtPersonal.Rows.Count > 0 AndAlso dtAddress.Rows.Count > 0 Then
                    dtPersonal.Rows(0).Item("CustomerStatusCode") = dtAddress.Rows(0).Item("CustomerStatusCode")
                    dtPersonal.Rows(0).Item("OptOutFlag") = dtAddress.Rows(0).Item("OptOutFlag")
                    dtPersonal.Rows(0).Item("MaritalStatusCode") = dtAddress.Rows(0).Item("MaritalStatusCode")
                    dtPersonal.Rows(0).Item("PhoneMobile") = dtAddress.Rows(0).Item("PhoneMobile")
                    dtPersonal.Rows(0).Item("PhonePager") = dtAddress.Rows(0).Item("PhonePager")
                    dtPersonal.Rows(0).Item("PassportNumber") = dtAddress.Rows(0).Item("PassportNumber")
                    dtPersonal.Rows(0).Item("Occupation") = dtAddress.Rows(0).Item("Occupation")
                    dtPersonal.Rows(0).Item("BirthPlace") = dtAddress.Rows(0).Item("BirthPlace")

                    dtPersonal.Rows(0).Item("AgeAdmInd") = dtAddress.Rows(0).Item("AgeAdmInd")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column START
                    dtPersonal.Rows(0).Item("OptOutOtherFlag") = dtAddress.Rows(0).Item("OptOutOtherFlag")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column END
                End If
            End If
            GetPersonalInfoAddress4Customer = True

        Catch ex As Exception
            strErrMsg = ex.ToString
        End Try

    End Function


    Public Function GetPersonalInfoAddress4McuCustomer(ByVal strCustID As String, ByVal strClientID As String,
                                          ByRef strErrMsg As String, ByRef blnCovSmoker As Boolean,
                                          ByRef dtPersonal As DataTable, ByRef dtAddress As DataTable,
                                          ByRef dtCIWPersonInfo As DataTable, Optional ByVal blnLoadPol As Boolean = True) As Boolean
        Dim sqlConn As New SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strSQL As String

        Dim lngErrNo As Long = 0
        Dim lngCnt As Long

        GetPersonalInfoAddress4McuCustomer = False

        Try

            ' Temp add to handle duplicate clientid
            Dim strClient() As String

            'strClient = GetClientList(strCustID)
            strClient = New String() {strCustID}

            If blnLoadPol Then
                'AC - Change to use configuration setting - start
                'If UAT <> 0 Then
                '    'objCS.Env = giEnv
                'End If
                'AC - Change to use configuration setting - start

                If Not strClient Is Nothing AndAlso strClient.Length > 0 Then

                    sqldt = objCS.GetPolicyList(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    'sqldt = GetPolicyListCIW(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        Exit Function
                    End If

                    For I As Integer = 1 To strClient.Length - 1
                        'dtAddress = objCS.GetPolicyList(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        'dtAddress = GetPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        'dtAddress = wsCRS.GetPolicyListMCUCIW(getCompanyCode(g_McuComp), getEnvCode(), strClient(I), "", "", "POLST", "=", lngErrNo, strErrMsg, lngCnt, True)

                        'If lngErrNo <> 0 Then
                        '    Exit Function
                        'End If

                        If GetPolicyListByAPI(getCompanyCode(g_McuComp), "", strClient(I), "", "POLST", "=", lngErrNo, strErrMsg, lngCnt, sqldt, True) Then
                            If lngErrNo <> 0 Then
                                Exit Function
                            End If
                        End If

                        For K As Integer = 0 To dtAddress.Rows.Count - 1
                            sqldt.Rows.Add(dtAddress.Rows(K).ItemArray)
                        Next
                    Next

                    'ds.Tables.Add(sqldt)
                Else
                    'sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    'sqldt = wsCRS.GetPolicyListMCUCIW(getCompanyCode(g_McuComp), getEnvCode(), strClientID, "", "", "POLST", "=", lngErrNo, strErrMsg, lngCnt, True)

                    'If lngErrNo <> 0 Then
                    '    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    '    wndMain.Cursor = Cursors.Default
                    '    Exit Function
                    'End If
                    sqldt = New DataTable
                    If GetPolicyListByAPI(getCompanyCode(g_McuComp), "", strClientID, "", "POLST", "=", lngErrNo, strErrMsg, lngCnt, sqldt, True) Then
                        If lngErrNo <> 0 Then
                            MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                            wndMain.Cursor = Cursors.Default
                            Exit Function
                        End If
                    End If

                    ' ds.Tables.Add(sqldt)
                End If

                ' Check if in any coverage, the insured is marked as smoker
                For I As Integer = 0 To sqldt.Rows.Count - 1
                    If sqldt.Rows(I).Item("PolicyRelateCode") = "PI" And sqldt.Rows(I).Item("SMCODE") = "S" Then
                        blnCovSmoker = True
                        Exit For
                    End If
                Next
            End If

            'strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            'strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

            sqlConn.ConnectionString = strCIWMcuConn

            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlDA.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")


            ' Use CIW
            ' Enhancement for PDPO - Add OptOutOtherFlag column
            strSQL = "Select c.CustomerID, NamePrefix, FirstName, NameSuffix, ChiLstNm+ChiFstNm as ChiName, CoName, CoCName, " &
                " DateOfBirth, Gender, MaritalStatusCode, SmokerFlag, LanguageCode, GovernmentIDCard, OptOutFlag, " &
                " EmailAddr, UseChiInd, g.AgentCode, ISNULL(g.UnitCode,'') as Agency, " &
                " AddressTypeCode, AddressLine1, AddressLine2, AddressLine3, AddressCity, " &
                " AddressPostalCode, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, EMailID, BadAddress, AddrProof, c.CountryCode, " &
                " CustomerStatusCode, CustomerTypeCode, PhoneMobile, PhonePager, PassportNumber, Occupation, BirthPlace, AgeAdmInd, c.OptOutOtherFlag " &
                " From CustomerAddress a, Customer c LEFT JOIN AgentCodes g " &
                " ON c.AgentCode = g.AgentCode " &
                " Where addresstypecode in ('R','B','I','J') and c.CustomerID = '" & strCustID.Replace("'", "''") & "' " &
                " And c.CustomerID = a.CustomerID"
            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            dtAddress = New DataTable("CustomerAddress")
            Try
                sqlDA.Fill(dtAddress)
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            'strSQL = "Select cswdgm_cust_id as CustomerID, cswdgm_optout_email as OptEmail, cswdgm_optout_call as OptCall,"
            'strSQL &= " cswdgm_rating as Rating, cswdgm_no_of_dep as DependNo, cswdgm_edu_level as EduLevel,"
            ''strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup"
            ''strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup, cswdgm_optout_NPS"
            ''changed by jeff tam
            'strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup, cswdgm_optout_NPS"
            'strSQL &= " From csw_demographic"
            'strSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"
            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'dtCIWPersonInfo = New DataTable("CIWCustomer")
            'Try
            '    sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            '    sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            '    sqlDA.Fill(dtCIWPersonInfo)
            'Catch sqlex As SqlClient.SqlException
            '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            'End Try

            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            dtCIWPersonInfo = GetCIWCustomer(strCustID, g_McuComp)

            sqlConn.Dispose()

            'Use CAPSIL

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    'objCS.Env = giEnv
            '    objCS.Env = 0
            'End If
            'AC - Change to use configuration setting - end


            'dtPersonal = objCS.GetORDUNA("'" & strClientID.Replace("'", "''") & "'", lngErrNo, strErrMsg)
            If Not strClient Is Nothing AndAlso strClient.Length > 0 Then
                'dtPersonal = objCS.GetORDUNA("'" & strClient(0) & "'", lngErrNo, strErrMsg)
                'dtPersonal = objCS.GetCIWCustInfo(strClient(0))
                dtPersonal = GetCIWMcuCustInfo(strClient(0))
            End If
            'Handle Error in GetChiAddr() if no record found
            'If Not dtPersonal Is Nothing Then
            '    If dtPersonal.Rows.Count > 0 Then
            '        GetChiAddr(dtPersonal)
            '    End If
            '    dtPersonal.TableName = "CustomerAddress"
            'End If

            If Not dtPersonal Is Nothing AndAlso Not dtAddress Is Nothing Then
                Try
                    dtPersonal.Columns.Add("AgeAdmInd", Type.GetType("System.String"))
                Catch ex As Exception
                End Try
                Try
                    dtPersonal.Columns.Add("OptOutOtherFlag", Type.GetType("System.String"))
                Catch ex As Exception
                End Try

                If dtPersonal.Rows.Count > 0 AndAlso dtAddress.Rows.Count > 0 Then
                    dtPersonal.Rows(0).Item("CustomerStatusCode") = dtAddress.Rows(0).Item("CustomerStatusCode")
                    dtPersonal.Rows(0).Item("OptOutFlag") = dtAddress.Rows(0).Item("OptOutFlag")
                    dtPersonal.Rows(0).Item("MaritalStatusCode") = dtAddress.Rows(0).Item("MaritalStatusCode")
                    dtPersonal.Rows(0).Item("PhoneMobile") = dtAddress.Rows(0).Item("PhoneMobile")
                    dtPersonal.Rows(0).Item("PhonePager") = dtAddress.Rows(0).Item("PhonePager")
                    dtPersonal.Rows(0).Item("PassportNumber") = dtAddress.Rows(0).Item("PassportNumber")
                    dtPersonal.Rows(0).Item("Occupation") = dtAddress.Rows(0).Item("Occupation")
                    dtPersonal.Rows(0).Item("BirthPlace") = dtAddress.Rows(0).Item("BirthPlace")

                    dtPersonal.Rows(0).Item("AgeAdmInd") = dtAddress.Rows(0).Item("AgeAdmInd")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column START
                    dtPersonal.Rows(0).Item("OptOutOtherFlag") = dtAddress.Rows(0).Item("OptOutOtherFlag")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column END
                End If
            End If
            GetPersonalInfoAddress4McuCustomer = True

        Catch ex As Exception
            strErrMsg = ex.ToString
        End Try

    End Function

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Public Function GetCIWCustomer(ByVal customerID As String, Optional ByVal company As String = "HK") As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(company), "GET_CIW_CUSTOMER",
                        New Dictionary(Of String, String) From {
                        {"cswdgm_cust_id", customerID}
                        })

            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0).Copy
            End If

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return dt

    End Function

    Public Function CPSDate(ByVal datVal As Date, Optional ByVal intBase As Integer = 1800) As String
        Try
            Return CStr(CInt(datVal.ToString("yyyyMMdd")) - intBase * 10000)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function AddAuditTrail(ByVal strCustID As String, ByVal strAction As String, ByVal strDetail As String) As Boolean
        Dim strSQL As String
        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim intCnt As Integer

        AddAuditTrail = False
        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlCmd.Connection = sqlconnect

        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Insert " & serverPrefix & "crm_audit_trail_tab (crmcat_tran_date, crmcat_user_id, crmcat_customer_id, crmcat_action, crmcat_detail) " &
            " Select GETDATE(), '" & gsUser & "', " & strCustID & ", '" & strAction & "','" & strDetail & "'"

        sqlCmd.CommandText = strSQL

        Try
            intCnt = sqlCmd.ExecuteNonQuery()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Function
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Function
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlCmd.Dispose()
        End Try

        If intCnt > 0 Then
            AddAuditTrail = True
        End If

    End Function

    ''' <summary>
    ''' Check whether the current user has permissions for the specified <paramref name="strTabName"/>
    ''' </summary>
    ''' <returns>Return true if permission is assigned, otherwise false</returns>
    Public Function CheckIULAccess(ByVal strPolicy As String, ByVal comName As String) As Boolean
        Dim dtResult As New DataTable
        Dim retDs As DataSet

        Try
            retDs = APIServiceBL.CallAPIBusi(comName, "CHECK_IUL_ACCESS", New Dictionary(Of String, String)() From {
            {"strInPolicy", strPolicy}})
        Catch ex As Exception
            MsgBox(String.Format("Fail to get CIWPT_isIndexedUL info for: " & strPolicy & " " & ex.Message))
        End Try

        If retDs.Tables.Count = 0 Then
            MsgBox(String.Format("Fail to get CIWPT_isIndexedUL info: " & strPolicy))
            Return False
        End If
        dtResult = retDs.Tables(0).Copy
        If dtResult.Rows.Count = 0 Then
            'Throw New Exception(String.Format("No CIWPT_isIndexedUL record was found by product ID {0}", strPolicy))
            MsgBox("No CIWPT_isIndexedUL record was found by product ID " & strPolicy)
            Return False
        End If

        Return IIf(Convert.ToString(dtResult.Rows(0)("CIWPT_isIndexedUL")).Equals("Y"), True, False)
    End Function
    Public Function CheckUPSAccess(strTabName As String) As Boolean
        If dtPriv Is Nothing OrElse dtPriv.Rows.Count = 0 Then Return False

        Dim dr() As DataRow = dtPriv.Select($"upsmit_desc = '{strTabName}'")

        Return dr.Length > 0 AndAlso Mid(strUPSMenuCtrl, dr(0).Item("upsmit_seq_no"), 1) = "1"
    End Function

    ''' <summary>
    ''' Check whether the current user has permissions(new access control) for the specified <paramref name="accessFuncName"/>
    ''' </summary>
    ''' <returns>Return true if permission is assigned, otherwise false</returns>
    Public Function CheckUPSAccessFunc(accessFuncName As String) As Boolean
        Return dtFuncList IsNot Nothing AndAlso dtFuncList.Select($"AccessFunc = '{accessFuncName}'").Length > 0
    End Function

    ''' <summary>
    ''' Get VIP status
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Public Function GetVIPStatus(ByVal strCustID As String, Optional ByVal strPolicy As String = "", Optional companyName As String = "") As String

        Dim strStatus As String
        'Dim sqlconnect As New SqlConnection
        'Dim sqlcmd As New SqlCommand
        strStatus = "0"
        Try
            If strPolicy <> "" Then
                strStatus = APIServiceBL.QueryScalar(Of String)(companyName, "FRM_VIP_STATUS_POLICY", New Dictionary(Of String, String)() From {
                {"strPolicy", Trim(strPolicy)}
                }, "")
            Else
                strStatus = APIServiceBL.QueryScalar(Of String)(companyName, "FRM_VIP_STATUS_CUSTOMER", New Dictionary(Of String, String)() From {
                {"strCustID", Trim(strCustID)}
                }, "")

            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error") 'Temp Comment
            appSettings.Logger.logger.Error(ex.Message.ToString)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Return strStatus

    End Function

    Public Function GetFundAllocation(ByVal strPolicyNo As String, ByVal strEffDate As Date,
                                  ByRef objFA As DataTable, ByRef strError As String) As Boolean

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter

        GetFundAllocation = False

        strSQL = "select B.cswcfa_policy_no , B.cswcfa_coverage_no, C.ProductID, D.Description, E.ChineseDescription, B.cswcfa_eff_date, B.cswcfa_fund_code, B.cswcfa_allocation, mpfinv_chi_desc, mpfinv_chi_name " &
                 "From" &
                 "(select cswcfa_policy_no, cswcfa_coverage_no, max(cswcfa_eff_date) as MaxDate " &
                 " From csw_fund_allocation " &
                 " Where cswcfa_policy_no = '" & strPolicyNo & "' and cswcfa_eff_date <= '" & strEffDate & "' " &
                 " group by cswcfa_policy_no, cswcfa_coverage_no " &
                 ") A " &
                 "join csw_fund_allocation B on A.cswcfa_policy_no = B.cswcfa_policy_no and A.cswcfa_coverage_no= B.cswcfa_coverage_no and A.MaxDate = B.cswcfa_eff_date " &
                 "left join coverage C on B.cswcfa_policy_no = C.PolicyAccountID and B.cswcfa_coverage_no = C.Trailer " &
                 "left join product D on C.ProductID = D.ProductID " &
                 "left join " & gcNBSDB & "product_chi E on C.ProductID = E.ProductID " &
                 "left join cswvw_mpf_investment F on mpfinv_code = B.cswcfa_fund_code"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(objFA)
            GetFundAllocation = True

        Catch sqlex As SqlClient.SqlException
            strError = sqlex.ToString
        Catch ex As Exception
            strError = ex.ToString
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlda.Dispose()
        End Try

    End Function

    Public Function GetUnitTx(ByVal strPolicyNo As String, ByRef objUH As DataTable, ByRef strError As String) As Boolean

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter

        GetUnitTx = False

        strSQL = "Select cswuth_fund_code as FundCode, cswuth_co_no as Rider, cswuth_cap_tx_date as TxDate, " &
            " cswuth_cal_tr_date as TradeDate, cswuth_cal_al_date as AlloDate, cswuth_amount as Amount, cswuth_unit as Units, " &
            " cswuth_pay_type as PayMethod, cswuth_adm_chrg as AdminCharge, cswuth_amt_fnd_ccy as Amount_ccy, " &
            " case when cswuth_cap_rv_date = '19000101' then '' else '*' end as RecvFlag " &
            " From csw_unit_tran " &
            " Where cswuth_poli_no = '" & strPolicyNo & "'"
        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(objUH)
            GetUnitTx = True

        Catch sqlex As SqlClient.SqlException
            strError = sqlex.ToString
        Catch ex As Exception
            strError = ex.ToString
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlda.Dispose()
        End Try

    End Function

    Public Function GetGICustomerList(ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String,
            ByVal strCustID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False) As System.Data.DataTable

        Dim sqlconnect As New SqlConnection
        Dim sqldt As DataTable

        Dim strSEL, strSELC, strSQL, strCR, strCR1, strCR2 As String
        Dim intTmpCnt As Integer

        strSELC = "Select count(*) "
        strSEL = "Select nameprefix, firstname, namesuffix, rtrim(chilstnm)+rtrim(chifstnm) as ChiName, governmentidcard, " &
            " 0 as NABYY, 0 as NABMM, 0 as NABDD, gender, coname, cocname, customerid, space(10) as ClientID, AgentCode, " &
            " isnull(dateofbirth,'19000101') as dateofbirth "

        strSQL = " From Customer " &
            " Where companyid = 'GI' "

        If strLastName <> "" Then
            strCR1 &= " and namesuffix = '" & Replace(strLastName, "'", "''") & "'"
        End If

        If strFirstName <> "" Then
            strCR1 &= " and firstname = '" & Replace(strFirstName, "'", "''") & "'"
        End If

        If strHKID <> "" Then
            strCR2 &= " and governmentidcard = '" & Replace(strHKID, "'", "''") & "'"
        End If

        If strCustID <> "" Then
            strCR2 &= " and customerid = '" & Replace(strCustID, "'", "''") & "'"
        End If

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        If strCR1 = "" And strCR2 = "" Then
            lngErrNo = -1
            strErrMsg = "GetCustomerList - Invalid Criteria"
            Exit Function
        Else
            strSQL &= strCR1 & strCR2
        End If

        Try
            If blnCntOnly Then
                Dim sqlcmd As New SqlCommand
                sqlcmd.CommandText = strSELC & strSQL
                sqlcmd.Connection = sqlconnect
                intTmpCnt = sqlcmd.ExecuteScalar
                sqlconnect.Close()
                sqlcmd = Nothing
            End If
            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                Dim sqlda As SqlDataAdapter
                sqldt = New DataTable("CustList")
                sqlda = New SqlDataAdapter(strSEL & strSQL, sqlconnect)
                sqlda.Fill(sqldt)
                intCnt = sqldt.Rows.Count
            End If

        Catch err As SqlException
            lngErrNo = -1
            strErrMsg = "GetCustomerList - " & err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetCustomerList - " & ex.ToString
        End Try

        'Return sqldt
        GetGICustomerList = sqldt

    End Function

    'oliver 2023-12-6 added for Switch Over Code from Assurance to Bermuda 
    Public Function GetGIPolicyList(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False) As DataTable

        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer

        oledbconnect.ConnectionString = strCIWConn

        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then

                ' ES01 begin
                strSEL = "Select p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " &
                        " PolicyRelateCode, a.AccountStatusCode, AccountStatus, Space(2) as POCOCD, PolicyCurrency, " &
                        " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " &
                        " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a " &
                    " Where r.policyaccountid like " & strPolicy &
                    " And r.policyaccountid = p.policyaccountid " &
                    " And p.AccountStatusCode = a.AccountStatusCode " &
                    " And p.companyid = 'GI' " &
                    " And r.policyrelatecode = 'PH' "

                'strSEL = "Select RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                '        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                '        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                '        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                'strSQL = _
                '    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                '    " Where r.FLD0004 LIKE " & strPolicy & _
                '    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                '    " And r.FLD0004 = POPONO " & _
                '    " And POCOMP = '  ' " & _
                '    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  ' " & _
                '    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
                ' ES01 end

            End If

            ' Policy List (by customer id)
            If strCustID <> "" Then

                strSEL = "Select p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " &
                        " PolicyRelateCode, a.AccountStatusCode, Space(2) as POCOCD, PolicyCurrency, " &
                        " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " &
                        " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a  " &
                    " Where r.customerid = '" & strCustID & "' " &
                    " And r.policyaccountid = p.policyaccountid " &
                    " And p.AccountStatusCode = a.AccountStatusCode " &
                    " And p.companyid = 'GI' " &
                    " And r.policyrelatecode = 'PH' "

                'strSEL = "Select DISTINCT RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                '        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                '        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                '        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                'strSQL = _
                '    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                '    " Where r.FLD0004 = POPONO " & _
                '    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                '    " And FLD0002 = '" & strCustID & "' " & _
                '    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  '" & _
                '    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
            End If

            'If strRel <> "" Then
            '    strSQL &= " And r.FLD0006 = '" & strRel & "'"
            'Else
            '    'strSQL &= " And r.FLD0005 in ('00', '01') "
            '    strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
            'End If

            If blnCntOnly Then
                oledbconnect.Open()
                Dim cmd As SqlCommand = New SqlCommand(strSELC & strSQL, oledbconnect)
                intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If

            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & ex.ToString
            Return Nothing
        End Try

        oledbconnect = Nothing

        Return dtPOLST

    End Function

    'oliver 2023-12-6 updated for Switch Over Code from Assurance to Bermuda 
    'Public Function GetGIPolicyList(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
    Public Function GetGIPolicyListAsur(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False, Optional CompanyID As String = "ING") As DataTable

        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer

        If String.Equals(CompanyID, "ING", StringComparison.OrdinalIgnoreCase) = True Then oledbconnect.ConnectionString = strCIWConn
        If String.Equals(CompanyID, "MCU", StringComparison.OrdinalIgnoreCase) = True Then oledbconnect.ConnectionString = strCIWMcuConn
        If String.Equals(CompanyID, "LAC", StringComparison.OrdinalIgnoreCase) = True Then oledbconnect.ConnectionString = strLIDZConn
        If String.Equals(CompanyID, "LAH", StringComparison.OrdinalIgnoreCase) = True Then oledbconnect.ConnectionString = strLIDZConn

        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then

                ' ES01 begin
                strSEL = "Select DISTINCT p.CompanyID, p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " &
                        " PolicyRelateCode, a.AccountStatusCode, AccountStatus, Space(2) as POCOCD, PolicyCurrency, " &
                        " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " &
                        " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a " &
                    " Where r.policyaccountid like " & strPolicy &
                    " And r.policyaccountid = p.policyaccountid " &
                    " And p.AccountStatusCode = a.AccountStatusCode " &
                    " And p.companyid = 'GI' " &
                    " And r.policyrelatecode = 'PH' "

                'strSEL = "Select RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                '        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                '        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                '        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                'strSQL = _
                '    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                '    " Where r.FLD0004 LIKE " & strPolicy & _
                '    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                '    " And r.FLD0004 = POPONO " & _
                '    " And POCOMP = '  ' " & _
                '    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  ' " & _
                '    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
                ' ES01 end

            End If

            ' Policy List (by customer id)
            If strCustID <> "" Then

                strSEL = "Select DISTINCT p.CompanyID, p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " &
                        " PolicyRelateCode, a.AccountStatusCode, Space(2) as POCOCD, PolicyCurrency, " &
                        " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " &
                        " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a  " &
                    " Where r.customerid = '" & strCustID & "' " &
                    " And r.policyaccountid = p.policyaccountid " &
                    " And p.AccountStatusCode = a.AccountStatusCode " &
                    " And p.companyid = 'GI' " &
                    " And r.policyrelatecode = 'PH' "

                'strSEL = "Select DISTINCT RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                '        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                '        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                '        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                'strSQL = _
                '    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                '    " Where r.FLD0004 = POPONO " & _
                '    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                '    " And FLD0002 = '" & strCustID & "' " & _
                '    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  '" & _
                '    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
            End If

            'If strRel <> "" Then
            '    strSQL &= " And r.FLD0006 = '" & strRel & "'"
            'Else
            '    'strSQL &= " And r.FLD0005 in ('00', '01') "
            '    strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
            'End If

            If blnCntOnly Then
                oledbconnect.Open()
                Dim cmd As SqlCommand = New SqlCommand(strSELC & strSQL, oledbconnect)
                intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If

            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & ex.ToString
            Return Nothing
        End Try

        oledbconnect = Nothing

        Return dtPOLST

    End Function

    Public Sub GetEnvSetup(ByVal iEnvStr As String)
        gSQL3 = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gSQL3")
        CSW_USER_PRIVS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "CSW_USER_PRIVS")
        UPS_USER_GROUP_TAB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "UPS_USER_GROUP_TAB")
        UPS_USER_LIST_TAB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "UPS_USER_LIST_TAB")
        CIC_FULLNAME_MAPPING = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "CIC_FULLNAME_MAPPING")
        cSQL3 = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "cSQL3")
        cSQL3_Mcu = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "cSQL3_MCU")
        g_LAUser = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LAUser")
        g_Qman = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Qman")
        g_WinRemoteQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinRemoteQ")
        g_WinRemoteMcuQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinRemoteMcuQ")
        g_LAReplyQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LAReplyQ")
        g_LAReplyMcuQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LAReplyMcuQ")
        g_WinLocalQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinLocalQ")
        g_WinLocalMcuQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinLocalMcuQ")
        g_Env = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Env")
        g_McuEnv = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_McuEnv")
        g_Comp = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Comp")
        'oliver 2024-7-31 added for Com 6
        g_BmuComp = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_BmuComp")
        g_McuComp = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_McuComp")
        g_ProjectAlias = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_ProjectAlias")
        g_UserType = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_UserType")
        g_Connection_CIW = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_CIW")
        g_Connection_McuCIW = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_McuCIW")
        g_Connection_UPS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_UPS")
        g_Connection_ICR = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_ICR")
        g_Connection_MCS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_MCS")
        g_Connection_MJC = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_MJC")
        g_Connection_McuMJC = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_McuMJC")
        g_Connection_CAM = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_CAM")
        g_Capsil_Project = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Capsil_Project")
        g_Capsil_Connection = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Capsil_Connection")
        g_Capsil_UserType = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Capsil_UserType")
        g_Capsil_Lib = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Capsil_Lib")
        g_CAM_Database = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_CAM_Database")
        g_McuCAM_Database = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_McuCAM_Database")
        g_McuNBR_Database = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_McuNBR_Database")
        g_ProjectAlias_MJC = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_ProjectAlias_MJC")
        g_UserType_MJC = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_UserType_MJC")
        gcNBR = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcNBR")
        gcMcuNBR = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcMcuNBR")
        giEnv = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "giEnv")
        strValProj = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "strValProj")
        strValConn = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "strValConn")
        g_Connection_CNB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_Connection_CNB")
        gcMcuCIW = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcMcuCIW")
        gcCIW = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcCIW")
        gcMcuMCS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcMcuMCS")
        gcMCS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcMCS")
        gcRMLife = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcRMLife")
        gcPOS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcPOS")
        gcMcuPOS = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcMcuPOS")
        cSQL3_Asur = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "cSQL3_ASUR")
        gcLCP = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcLCP")
        gcCRSServer = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcCRSServer")
        gcCRSDB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcCRSDB")
        gcCRSDBMcu = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcCRSDBMcu")
        gcCRSDBAsur = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcCRSDBAsur")
        gcEPORTALDB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcEPORTALDB")
        gcEPORTALDBMcu = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcEPORTALDBMcu")
        gcNBSDB = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcNBSDB")
        gcNBSDBMcu = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "gcNBSDBMcu")
    End Sub

    Public Sub GetAsurEnvSetup(ByVal iEnvStr As String)
        g_LacComp = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LacComp")
        g_LahComp = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LahComp")
        g_QmanAsur = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_QmanAsur")
        g_WinRemoteAsurQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinRemoteAsurQ")
        g_LAReplyAsurQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LAReplyAsurQ")
        g_WinLocalAsurQ = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_WinLocalAsurQ")
        g_LacEnv = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LacEnv")
        g_LahEnv = CRS_Component.EnvironmentUtility.getEnvironmentSetting(iEnvStr, "g_LahEnv")
    End Sub

    <Obsolete>
    Public Function EnvSetupFromAppSettings(Optional ByVal iEnvStr As String = "")
        appSettings.ConfigFile = iEnvStr
        cSQL3 = appSettings.cSQL3
        CSW_USER_PRIVS = appSettings.CSW_USER_PRIVS
        UPS_USER_GROUP_TAB = appSettings.UPS_USER_GROUP_TAB
        UPS_USER_LIST_TAB = appSettings.UPS_USER_LIST_TAB
        CIC_FULLNAME_MAPPING = appSettings.CIC_FULLNAME_MAPPING
        g_LAUser = appSettings.g_LAUser
        g_Qman = appSettings.g_Qman
        g_WinRemoteQ = appSettings.g_WinRemoteQ
        g_WinRemoteMcuQ = appSettings.g_WinRemoteMcuQ
        g_LAReplyQ = appSettings.g_LAReplyQ
        g_LAReplyMcuQ = appSettings.g_LAReplyMcuQ
        g_WinLocalQ = appSettings.g_WinLocalQ
        g_WinLocalMcuQ = appSettings.g_WinLocalMcuQ
        g_Env = appSettings.g_Env
        g_McuEnv = appSettings.g_McuEnv
        g_Comp = appSettings.g_Comp
        g_McuComp = appSettings.g_McuComp
        g_ProjectAlias = appSettings.g_ProjectAlias
        g_UserType = appSettings.g_UserType
        g_Connection_CIW = appSettings.g_Connection_CIW
        g_Connection_McuCIW = appSettings.g_Connection_McuCIW
        g_Connection_UPS = appSettings.g_Connection_UPS
        g_Connection_MCS = appSettings.g_Connection_MCS
        g_Connection_ICR = appSettings.g_Connection_ICR
        g_UserType_ICR = appSettings.g_UserType_ICR
        g_ProjectAlias_MJC = appSettings.g_ProjectAlias_MJC
        g_Connection_MJC = appSettings.g_Connection_MJC
        g_UserType_MJC = appSettings.g_UserType_MJC
        g_Connection_CAM = appSettings.g_Connection_CAM
        g_Capsil_Project = appSettings.g_Capsil_Project
        g_Capsil_Connection = appSettings.g_Capsil_Connection
        g_Capsil_UserType = appSettings.g_Capsil_UserType
        g_Capsil_Lib = appSettings.g_Capsil_Lib
        g_CAM_Database = appSettings.g_CAM_Database
        g_McuCAM_Database = appSettings.g_McuCAM_Database
        g_McuNBR_Database = appSettings.g_McuNBR_Database
        gcNBR = appSettings.gcNBR
        giEnv = appSettings.giEnv
        strLASCAM = appSettings.strLASCAM
        strValProj = appSettings.strValProj
        strValConn = appSettings.strValConn
        g_Connection_CNB = appSettings.g_Connection_CNB
        gcCIW = appSettings.gcCIW
        gcMcuCIW = appSettings.gcMcuCIW
        gcMCS = appSettings.gcMCS
        gcMcuMCS = appSettings.gcMcuMCS
        gcRMLife = appSettings.gcRMLife
        gcPOS = appSettings.gcPOS
        gcMcuPOS = appSettings.gcMcuPOS
    End Function

    ''' <param name="istrEnv">e.g. INGU105</param>
    Public Sub EnvSetup(ByVal istrEnv As String)
        If ConfigurationManager.AppSettings("Utility") = "Y" Then
            GetEnvSetup(istrEnv)
            GetAsurEnvSetup(istrEnv)
            Return
        End If
    End Sub

    '***** LIFE ASIA ***

    ' ES0001 begin
    ''' <summary>
    ''' Get Broker VerifIication Key
    ''' Lubin 2022-11-07 TSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Public Function GetBrokerVerificationKey(ByVal strAgent As String, Optional companyName As String = "") As String
        GetBrokerVerificationKey = ""
        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        'strSQL = "Select * from " & gSQL3 & "." & g_CAM_Database & ".dbo.cam_broker_password_tab where cambpt_agent_no = '" & strAgent & "' "

        Try
            'oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
            'GetBrokerVerificationKey = APIServiceBL.QueryScalar(Of String)(companyName,
            '                                "FRM_BROKER_VERIFICATION_KEY",
            '                                New Dictionary(Of String, String) From {
            '                                {"strAgent", strAgent}
            '                                }, "", True)
            GetBrokerVerificationKey = APIServiceBL.QueryScalar(Of String)(companyName,
                                            "FRM_BROKER_VERIFICATION_KEY",
                                            New Dictionary(Of String, String) From {
                                            {IIf(IsAssurance, "strAgent", "cambpt_agent_no"), strAgent}
                                            }, "", True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "CRS - GetBrokerVerificationKey")
        End Try

    End Function
    ' ES0001 end


    Public Function GetPolicyListMCUCIW(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByVal strCri As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False) As DataTable

        'Dim oledbconnect As New OleDbConnection
        'Dim daPOLST, daAGTLST As OleDbDataAdapter
        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer

        oledbconnect.ConnectionString = strCIWMcuConn
        oledbconnect.Open()

        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then
                strSEL = "Select P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From policyaccount p, product pd, coveragedetail cd, csw_poli_rel r, csw_poli_rel r1, customer c " &
                    " Where p.policyaccountid like '@policyNo' " &
                    " and p.policyaccountid = r.policyaccountid " &
                    " and p.policyaccountid = r1.policyaccountid " &
                    " and p.policyaccountid = cd.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' " &
                    " and r1.customerid = c.customerid " &
                    " and p.productid = pd.productid and cd.trailer = 1 "

            End If

            ' Policy List (by customer id)
            '" And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' "
            If strCustID <> "" Then

                strSEL = "Select DISTINCT P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From csw_poli_rel r, policyaccount p, product pd, coveragedetail cd, customer c, csw_poli_rel r1 " &
                     " Where p.policyaccountid = r.policyaccountid  " &
                     " and p.policyaccountid = cd.policyaccountid  " &
                     " and r.customerid = '@CustomerId'" &
                     " and p.productid = pd.productid and cd.trailer = 1  " &
                     " and p.policyaccountid = r1.policyaccountid   " &
                     " and r1.policyrelatecode = 'SA' " &
                     " and r1.customerid = c.customerid  "
            End If

            If strRel <> "" Then
                strSQL &= " And r.policyrelatecode = 'PH'"
            Else
                'strSQL &= " And r.FLD0005 in ('00', '01') "
                'strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
                strSQL &= " And r.policyrelatecode in ('PH','PI')"
            End If

            If blnCntOnly Then

                'Dim cmd As OleDbCommand = New OleDbCommand(strSQL, oledbconnect)
                'Dim cmd As SqlCommand = New SqlCommand(strSELC & strSQL, oledbconnect)

                Dim sda As New SqlDataAdapter(strSELC & strSQL, oledbconnect)



                If (strPolicy <> "") Then

                    Dim tmp As String

                    tmp = strPolicy.Replace("'", "").Replace("%", "")

                    sda.SelectCommand.CommandType = CommandType.Text

                    '1. 
                    sda.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 20).Value = tmp

                    '2.
                    sda.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 20).Value = "11149531"

                    'sda.SelectCommand.Parameters("@policyNo").Value = _tmp

                    'cmd.Parameters.AddWithValue("@Cri", strCri)

                    Dim _dt = New DataTable()

                    sda.Fill(_dt)

                    Dim cnt = _dt.Rows.Count()

                    Dim _val = _dt.Rows(0).Item(0).ToString()

                End If

                'sda.SelectCommand = cmd 

                'Dim _val = cmd.ExecuteScalar()





                'If (strPolicy <> "") Then
                '    cmd.Parameters.AddWithValue("@PolicyNo", strPolicy)
                'cmd.Parameters.AddWithValue("@Cri", strCri)

                'ElseIf (strCustID <> "") Then
                'cmd.Parameters.AddWithValue("@CustomerId", strCustID)
                'End If

                'If strRel <> "" Then
                'cmd.Parameters.AddWithValue("@Rel", strRel)
                'End If

                ' intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If





            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                'daPOLST = New OleDbDataAdapter(strSQL, oledbconnect)

                Dim cmd = New SqlCommand(strSEL & strSQL, oledbconnect)

                If (strPolicy <> "") Then
                    cmd.Parameters.AddWithValue("@PolicyNo", strPolicy)
                    'cmd.Parameters.AddWithValue("@Cri", strCri)

                ElseIf (strCustID <> "") Then
                    cmd.Parameters.AddWithValue("@CustomerId", strCustID)
                End If

                If strRel <> "" Then
                    'strSQL &= " And r.FLD0006 = '" & strRel & "'"
                    'strSQL &= " And r.policyrelatecode = '" & strRel & "'"
                    cmd.Parameters.AddWithValue("@Rel", strRel)
                End If

                cmd.CommandType = CommandType.Text

                daPOLST = New SqlDataAdapter(cmd)
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & ex.ToString
            Return Nothing
        End Try

        Return dtPOLST

    End Function

    Public Function GetConnectionStringByCompanyID(ByVal CompanyID As String) As String
        'oliver 2024-7-31 added for Com 6
        If CompanyID = "BMU" Then
            CompanyID = "ING"
        End If
        Dim strTempConn As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, CompanyID & "ConnectionString")
        Dim strRtnConn As String
        Try
            strRtnConn = CRS_Component.DecryptString(strTempConn)
        Catch ex As Exception
            strRtnConn = strTempConn
        End Try

        ' 20250320, HNW Expansion - Append timeout to the connection string
        strRtnConn &= ";Connect Timeout=" & gQryTimeOut

        Return strRtnConn
        'If String.Equals(CompanyID,"ING", StringComparison.OrdinalIgnoreCase) Then return strCIWConn
        'If String.Equals(CompanyID,"MCU", StringComparison.OrdinalIgnoreCase) Then return strCIWMcuConn
        'If String.Equals(CompanyID,"LAC", StringComparison.OrdinalIgnoreCase) Then return strLIDZConn
        'If String.Equals(CompanyID,"LAH", StringComparison.OrdinalIgnoreCase) Then return strLIDZConn
    End Function

    ' oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
    'Public Function GetPolicyListCIW(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
    Public Function GetAsurPolicyListCIW(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False, Optional ByVal CompanyID As String = "ING", Optional ByVal Criteria As String = "like", Optional ByVal strCustCriteria As String = "", Optional ByVal strCompanyCode As String = "") As DataTable

        'Dim oledbconnect As New OleDbConnection
        'Dim daPOLST, daAGTLST As OleDbDataAdapter
        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer
        Dim SysEventLog As New SysEventLog.clsEventLog

        oledbconnect.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
        SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[" + CompanyID + " Connection String : " + oledbconnect.ConnectionString + "]", Now.AddYears(-100), Now.AddYears(-100), "[" + oledbconnect.ConnectionTimeout.ToString() + "]", "[Criteria : " + Criteria + "]", "")

        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then

                strSEL = "Select DISTINCT P.CompanyID, Pd.ProductTypeCode As PolicyType, P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From policyaccount p " &
                         " inner join product pd " &
                         " on p.productid = pd.productid " &
                         " inner join ( select * from coveragedetail where trailer = 1 ) cd " &
                         " on p.policyaccountid = cd.policyaccountid " &
                         " inner join ( select * from csw_poli_rel where policyrelatecode = 'PH' or policyrelatecode = 'PI')r " &
                         " on p.policyaccountid = r.policyaccountid " &
                         " inner join ( select * from csw_poli_rel where policyrelatecode = 'SA' ) r1 " &
                         " on p.policyaccountid = r1.policyaccountid " &
                         " inner join customer c " &
                         " on r1.customerid = c.customerid  " &
                         " where p.policyaccountid " &
                         Criteria & " " & strPolicy
                SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[ SQL : " + strSEL + strSQL + "]", Now.AddYears(-100), Now.AddYears(-100), "", "", "")
                'strSQL = " From policyaccount p, product pd, coveragedetail cd, csw_poli_rel r, csw_poli_rel r1, customer c " &
                '    " Where p.policyaccountid like " & strPolicy &
                '    " and p.policyaccountid = r.policyaccountid " &
                '    " and p.policyaccountid = r1.policyaccountid " &
                '    " and p.policyaccountid = cd.policyaccountid " &
                '    " and r1.policyrelatecode = 'SA' " &
                '    " and r1.customerid = c.customerid " &
                '    " and p.productid = pd.productid and cd.trailer = 1 "
                'strSEL = "Select P.CompanyID, P.PolicyAccountid, P.ProductID, r.PolicyRelateCode, " &
                '       " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                '       " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                'strSQL = " From policyaccount p, coveragedetail cd, csw_poli_rel r, csw_poli_rel r1, customer c " &
                '    " Where p.policyaccountid like " & strPolicy &
                '    " and p.policyaccountid = r.policyaccountid " &
                '    " and p.policyaccountid = r1.policyaccountid " &
                '    " and p.policyaccountid = cd.policyaccountid " &
                '    " and r1.policyrelatecode = 'SA' " &
                '    " and r1.customerid = c.customerid " &
                '    " and cd.trailer = 1 "

            End If

            ' Policy List (by customer id)
            '" And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' "
            If strCustID <> "" Then

                strSEL = "Select DISTINCT P.CompanyID, Pd.ProductTypeCode As PolicyType, P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From policyaccount p " &
                            " inner join product pd " &
                            " on p.productid = pd.productid " &
                            " inner join ( select * from coveragedetail where trailer = 1 ) cd " &
                            " on p.policyaccountid = cd.policyaccountid " &
                            " inner join ( select * from csw_poli_rel where policyrelatecode = 'PH' or policyrelatecode = 'PI')r " &
                            " on p.policyaccountid = r.policyaccountid " &
                            " inner join ( select * from csw_poli_rel where policyrelatecode = 'SA' ) r1 " &
                            " on p.policyaccountid = r1.policyaccountid " &
                            " inner join customer c " &
                            " on r1.customerid = c.customerid  "

                If strCustCriteria <> "" Then
                    strSQL = strSQL & " where r.customerid in (" & strCustCriteria & ")"
                Else
                    strSQL = strSQL & " where r.customerid = '" & strCustID & "'"
                End If

                'oliver 2024-8-6 added for Com 6
                If strCompanyCode <> "" Then
                    strSQL = strSQL & " and P.CompanyID = '" & strCompanyCode & "' "
                End If

                'strSEL = "Select DISTINCT P.CompanyID, Pd.ProductTypeCode As PolicyType, P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                '        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                '        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                'strSQL = " From csw_poli_rel r, policyaccount p, product pd, coveragedetail cd, customer c, csw_poli_rel r1 " &
                '     " Where p.policyaccountid = r.policyaccountid  " &
                '     " and p.policyaccountid = cd.policyaccountid  " &
                '     " and r.customerid = '" & strCustID & "'" &
                '     " and p.productid = pd.productid and cd.trailer = 1  " &
                '     " and p.policyaccountid = r1.policyaccountid   " &
                '     " and r1.policyrelatecode = 'SA' " &
                '     " and r1.customerid = c.customerid  "
            End If

            If strRel <> "" Then
                'strSQL &= " And r.FLD0006 = '" & strRel & "'"
                strSQL &= " And r.policyrelatecode = '" & strRel & "'"
            Else
                'strSQL &= " And r.FLD0005 in ('00', '01') "
                'strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
                strSQL &= " And r.policyrelatecode in ('PH','PI')"
            End If

            'MsgBox(strSEL & strSQL)

            If blnCntOnly Then
                oledbconnect.Open()
                'Dim cmd As OleDbCommand = New OleDbCommand(strSQL, oledbconnect)
                Dim cmd As SqlCommand = New SqlCommand(strSELC & strSQL, oledbconnect)
                intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If

            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                'daPOLST = New OleDbDataAdapter(strSQL, oledbconnect)
                Dim strMainStartTime As String = Now
                SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[Start time]", strMainStartTime, Now.AddYears(-100), "", "", "")
                oledbconnect.Open()
                Dim cmd As SqlCommand = New SqlCommand(strSEL & strSQL, oledbconnect)
                cmd.CommandTimeout = oledbconnect.ConnectionTimeout
                daPOLST = New SqlDataAdapter()
                daPOLST.SelectCommand = cmd
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                oledbconnect.Close()
                Dim strMainEndTime As String = Now
                SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[Count]", strMainEndTime, Now.AddYears(-100), dtPOLST.Rows.Count(), "", "")
                SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[End time]", strMainEndTime, Now.AddYears(-100), "", "", "")

                'Dim strMainStartTime As String = Now
                'SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[Start time]", strMainStartTime, Now.AddYears(-100), "", "", "")
                'daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
                'dtPOLST = New DataTable(strTable)
                'daPOLST.Fill(dtPOLST)
                'Dim strMainEndTime As String = Now
                'SysEventLog.WritePerLog(gsUser, "modGlobal", "GetPolicyListCIW", "[End time]", strMainEndTime, Now.AddYears(-100), "", "", "")
                'intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW sqlex - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW ex - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        'With dtPOLST.Columns
        '    .Add("PolicyEffDate", Type.GetType("System.DateTime"))
        '    .Add("PaidToDate", Type.GetType("System.DateTime"))
        'End With

        ''Dim intRow As Integer = dtPOLST.Rows.Count
        ''Dim i As Integer
        ''For i = 0 To intRow - 1
        ''    With dtPOLST.Rows(i)

        ''        Select Case Trim(dtPOLST.Rows(i).Item("FLD0006"))
        ''            Case "A"
        ''                .Item("PolicyRelateCode") = "AS"
        ''            Case "B"
        ''                .Item("PolicyRelateCode") = "BE"
        ''            Case "I"
        ''                .Item("PolicyRelateCode") = "PI"
        ''            Case "O"
        ''                .Item("PolicyRelateCode") = "PH"
        ''            Case "P"
        ''                .Item("PolicyRelateCode") = "PR"
        ''            Case "S"
        ''                .Item("PolicyRelateCode") = "SP"
        ''        End Select

        ''        Select Case Trim(dtPOLST.Rows(i).Item("POCOCD"))
        ''            Case "BA"
        ''                .Item("PolicyCurrency") = "AUD"
        ''            Case "BH", "HK"
        ''                .Item("PolicyCurrency") = "HKD"
        ''            Case "BM", "US"
        ''                .Item("PolicyCurrency") = "USD"
        ''        End Select

        ''        .Item("PolicyEffDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
        ''        .Item("PaidToDate") = DateSerial(.Item("POPAYY") + 1800, .Item("POPAMM"), .Item("POPADD"))
        ''    End With
        ''Next

        Return dtPOLST

    End Function

    ' oliver 2023-12-12 added for Switch Over Code from Assurance to Bermuda
    Public Function GetPolicyListCIW(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
            Optional ByVal blnCntOnly As Boolean = False) As DataTable

        'Dim oledbconnect As New OleDbConnection
        'Dim daPOLST, daAGTLST As OleDbDataAdapter
        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer

        oledbconnect.ConnectionString = strCIWConn

        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then
                strSEL = "Select P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From policyaccount p, product pd, coveragedetail cd, csw_poli_rel r, csw_poli_rel r1, customer c " &
                    " Where p.policyaccountid like " & strPolicy &
                    " and p.policyaccountid = r.policyaccountid " &
                    " and p.policyaccountid = r1.policyaccountid " &
                    " and p.policyaccountid = cd.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' " &
                    " and r1.customerid = c.customerid " &
                    " and p.productid = pd.productid and cd.trailer = 1 "

            End If

            ' Policy List (by customer id)
            '" And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' "
            If strCustID <> "" Then
                strSEL = "Select DISTINCT P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                        " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                        " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                strSQL = " From csw_poli_rel r, policyaccount p, product pd, coveragedetail cd, customer c, csw_poli_rel r1 " &
                     " Where p.policyaccountid = r.policyaccountid  " &
                     " and p.policyaccountid = cd.policyaccountid  " &
                     " and r.customerid = '" & strCustID & "'" &
                     " and p.productid = pd.productid and cd.trailer = 1  " &
                     " and p.policyaccountid = r1.policyaccountid   " &
                     " and r1.policyrelatecode = 'SA' " &
                     " and r1.customerid = c.customerid  "
            End If

            If strRel <> "" Then
                'strSQL &= " And r.FLD0006 = '" & strRel & "'"
                strSQL &= " And r.policyrelatecode = '" & strRel & "'"
            Else
                'strSQL &= " And r.FLD0005 in ('00', '01') "
                'strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
                strSQL &= " And r.policyrelatecode in ('PH','PI','PP')"
            End If

            'MsgBox(strSEL & strSQL)

            If blnCntOnly Then
                oledbconnect.Open()
                'Dim cmd As OleDbCommand = New OleDbCommand(strSQL, oledbconnect)
                Dim cmd As SqlCommand = New SqlCommand(strSELC & strSQL, oledbconnect)
                intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If

            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                'daPOLST = New OleDbDataAdapter(strSQL, oledbconnect)
                daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        'With dtPOLST.Columns
        '    .Add("PolicyEffDate", Type.GetType("System.DateTime"))
        '    .Add("PaidToDate", Type.GetType("System.DateTime"))
        'End With

        ''Dim intRow As Integer = dtPOLST.Rows.Count
        ''Dim i As Integer
        ''For i = 0 To intRow - 1
        ''    With dtPOLST.Rows(i)

        ''        Select Case Trim(dtPOLST.Rows(i).Item("FLD0006"))
        ''            Case "A"
        ''                .Item("PolicyRelateCode") = "AS"
        ''            Case "B"
        ''                .Item("PolicyRelateCode") = "BE"
        ''            Case "I"
        ''                .Item("PolicyRelateCode") = "PI"
        ''            Case "O"
        ''                .Item("PolicyRelateCode") = "PH"
        ''            Case "P"
        ''                .Item("PolicyRelateCode") = "PR"
        ''            Case "S"
        ''                .Item("PolicyRelateCode") = "SP"
        ''        End Select

        ''        Select Case Trim(dtPOLST.Rows(i).Item("POCOCD"))
        ''            Case "BA"
        ''                .Item("PolicyCurrency") = "AUD"
        ''            Case "BH", "HK"
        ''                .Item("PolicyCurrency") = "HKD"
        ''            Case "BM", "US"
        ''                .Item("PolicyCurrency") = "USD"
        ''        End Select

        ''        .Item("PolicyEffDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
        ''        .Item("PaidToDate") = DateSerial(.Item("POPAYY") + 1800, .Item("POPAMM"), .Item("POPADD"))
        ''    End With
        ''Next

        Return dtPOLST

    End Function

    Public Function GetPolicyListECommerce(ByVal strHKID As String, ByVal strPolicy As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer) As DataTable

        Dim oledbconnect As New SqlConnection
        Dim daPOLST As SqlDataAdapter
        Dim strSQL, strSEL As String
        Dim dtPOLST As DataTable

        Dim s_Env As String = ""

        s_Env = ConfigurationSettings.AppSettings.Item("DefaultEnvironment").ToString()

        oledbconnect.ConnectionString = strCIWConn
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        Try
            ' Policy List (by policy)
            strSEL = "SELECT p.id, p.policy_number, p.created_date, " &
                            "p.commencement_date, p.expiry_date, p.user_name, p.promo_code, " &
                            "c.full_name, c.id_number, c.mobile, c.email, " &
                            "isnull(c.opt_in1, 0) as Opt_In_FWD, isnull(c.opt_in2, 0) as Opt_In_3Pty  "

            strSQL = " FROM eCommerce..[gi_policy] as p left outer join eCommerce..[client] as c on " &
                        "p.client_id_ow = c.id " &
                        "where (1 = 1) " &
                        "and p.product_code in (select Value from " & serverPrefix & " CodeTable  where Code = 'iFWDFree')" &
                        "and p.policy_number is not null "

            If strPolicy <> "" Then
                strSQL = strSQL & "and p.policy_number = '" & strPolicy & "' "
            End If

            If strHKID <> "" Then
                strSQL = strSQL & "and p.client_id_ow in (Select id from eCommerce..[client] where id_type = 'ID' and id_number = '" & strHKID & "') "
            End If

            strSQL = strSQL & "order by p.policy_number, p.created_date"

            daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
            dtPOLST = New DataTable(strTable)
            daPOLST.Fill(dtPOLST)
            intCnt = dtPOLST.Rows.Count

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyListECommerce - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyListECommerce - " & ex.ToString
            Return Nothing
        End Try
        Return dtPOLST

    End Function

    Public Function GetPolicyInsuredInfoECommerce(ByVal strPolicy As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer) As DataTable

        Dim oledbconnect As New SqlConnection
        Dim daPOLST As SqlDataAdapter
        Dim strSQL, strSEL As String
        Dim dtPOLST As DataTable

        oledbconnect.ConnectionString = strCIWConn

        Try
            ' Policy List (by policy)
            strSEL = "SELECT gpi.id, gpi.policy_id, c.full_name, c.id_number, gpi.relationship_code, itd.item_desc "

            strSQL = "from eCommerce..gi_policy_insured as gpi " &
                     "left outer join eCommerce..client as c on gpi.client_id = c.id " &
                     "left outer join eCommerce..item_desc as itd on gpi.relationship_code = itd.item_code and itd.item_table = 'InsuredRelationshipCode' and item_lang = 'EN' " &
                     "where 1 = 1 "

            If strPolicy <> "" Then
                strSQL = strSQL & "and gpi.policy_id in (Select id from eCommerce..gi_policy where policy_number = '" & strPolicy & "')"
            End If

            strSQL = strSQL & "order by c.full_name"

            daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
            dtPOLST = New DataTable(strTable)
            daPOLST.Fill(dtPOLST)
            intCnt = dtPOLST.Rows.Count

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyInsuredInfoECommerce - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyInsuredInfoECommerce - " & ex.ToString
            Return Nothing
        End Try
        Return dtPOLST

    End Function

    Public Function GetPolicyBeneInfoECommerce(ByVal strPolicy As String, ByVal strInsuredID As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer) As DataTable

        Dim oledbconnect As New SqlConnection
        Dim daPOLST As SqlDataAdapter
        Dim strSQL, strSEL As String
        Dim dtPOLST As DataTable

        oledbconnect.ConnectionString = strCIWConn

        Try
            ' Policy List (by policy)
            strSEL = "select gpib.policy_role_id, c.full_name, Case when c.id_type = 'PP' Then c.passport else c.id_number end as id_number, gpib.relationship_code, itd.item_desc  "

            strSQL = " from eCommerce..gi_policy_insured_beneficiary as gpib " &
                     "left outer join eCommerce..client as c on gpib.client_id = c.id " &
                     "left outer join eCommerce..item_desc as itd on gpib.relationship_code = itd.item_code and itd.item_table = 'BeneRelationshipCode' and item_lang = 'EN' " &
                     "where 1 = 1 "

            If strPolicy <> "" Then
                strSQL = strSQL & "and gpib.policy_role_id in (select id from eCommerce..gi_policy_insured where policy_id in (Select id from eCommerce..gi_policy where policy_number = '" & strPolicy & "'))"
            End If

            If strInsuredID <> "" Then
                strSQL = strSQL & "and gpib.policy_role_id = '" & strInsuredID & "'"
            End If

            strSQL = strSQL & "order by c.full_name"

            daPOLST = New SqlDataAdapter(strSEL & strSQL, oledbconnect)
            dtPOLST = New DataTable(strTable)
            daPOLST.Fill(dtPOLST)
            intCnt = dtPOLST.Rows.Count

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyInsuredInfoECommerce - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyInsuredInfoECommerce - " & ex.ToString
            Return Nothing
        End Try
        Return dtPOLST

    End Function

    Public Function UpdHKLAgtDT(ByVal dtAgent As DataTable, ByVal strColName As String, ByVal strNewCol As String) As Boolean

        Dim dr As DataRow

        Try
            dtAgent.Columns.Add(strNewCol, Type.GetType("System.String"))
            For Each dr In dtAgent.Rows
                dr.Item(strNewCol) = MapHKLAgent("", Right(dr.Item(strColName), 5))
            Next

            dtAgent.AcceptChanges()
        Catch ex As Exception

        End Try

    End Function

    ' HKL001
    Public Function MapHKLAgent(ByVal strHKLAgt As String, ByVal strINGAgt As String, Optional ByRef strBankCode As String = "",
    Optional ByRef strBranchCode As String = "") As String

        Dim strSQL As String
        Dim strAgt As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        If strHKLAgt <> "" Then
            strSQL = "select camham_ING_AgentNo from " & CAM_HKL_AGENT_MAPPING &
                " where camham_HKL_Bank+camham_HKL_AgentNo = '" & Replace(strHKLAgt, "'", "''") & "'"
        End If

        If strINGAgt <> "" Then
            strSQL = "select camham_HKL_Bank+camham_HKL_AgentNo+camham_HKL_Branch from " & CAM_HKL_AGENT_MAPPING &
                " where camham_ING_AgentNo = '" & Replace(strINGAgt, "'", "''") & "'"
        End If

        If strSQL = "" Then
            sqlcmd = Nothing
            Return ""
        End If

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strAgt = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        ' Return additional information
        If strINGAgt <> "" AndAlso strAgt <> "" Then
            strBankCode = Strings.Left(strAgt, 3)
            strBranchCode = Strings.Right(strAgt, 3)
            strAgt = Strings.Left(strAgt, 9)
        End If

        Return strAgt

    End Function

    ' ITSR200706
    Public Function GetRejRsn(ByVal strPolicy As String) As String

        Dim strSQL, strFld As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        strSQL = "Select nbrws_reject_reason " &
            " From " & NBR_WNC_WORKSHEET & ", " & NBR_WNC_STATUS &
            " Where nbrws_reject_reason_cd = nbrwnc_reject_reason_cd " &
            " And nbrwnc_policy_no = '" & strPolicy & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strFld = sqlcmd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        GetRejRsn = IIf(IsDBNull(strFld) OrElse strFld Is Nothing, "", strFld)

    End Function

    ' **** ES005 begin ****
    ''Public Function GetFundTransaction(ByVal strPolicy As String, ByRef objFT As DataTable, ByRef strError As String) As Boolean

    ''    Dim strSQL As String
    ''    Dim dtTemp As DataTable

    ''    GetFundTransaction = False

    ''    ' Get completed transaction from pos_trans_queue
    ''    strSQL = "Select postq_trans_id as Trans_ID, isnull(postq_backend_sys,'') as postq_backend_sys, postq_post_on as 'Trans. Date', case when postq_trans_type = 'ULA' then 'Fund Allocation' else 'Fund Switching' end as 'Trans. Type', case when postq_status = 'CO' then 'Completed' else 'In Progress' end as Status, postq_trans_type as Source " & _
    ''        " From pos_trans_queue " & _
    ''        " Where postq_policy_no = '" & strPolicy & "'" & _
    ''        " And postq_trans_type in ('ULA','ULB') " & _
    ''        " And postq_status in ('CO','') " & _
    ''        " Order by postq_post_on "

    ''    If GetDT(strSQL, strPOSConn, objFT, strError) Then

    ''        strSQL = "Select cswfre_ref_no, cswfre_create_date, cswfre_type, cswfre_status, 'WEB' as Source " & _
    ''            " from csw_fund_request " & _
    ''            " where cswfre_policy_no = '" & strPolicy & "'"

    ''        If GetDT(strSQL, strCIWConn, dtTemp, strError) Then
    ''            Dim dr As DataRow

    ''            If Not dtTemp Is Nothing Then
    ''                For i As Integer = 0 To dtTemp.Rows.Count - 1
    ''                    dr = objFT.NewRow
    ''                    dr.Item("Trans_ID") = dtTemp.Rows(i).Item("cswfre_ref_no")
    ''                    dr.Item("Trans. Date") = dtTemp.Rows(i).Item("cswfre_create_date")
    ''                    dr.Item("Trans. Type") = dtTemp.Rows(i).Item("cswfre_type")
    ''                    dr.Item("Status") = dtTemp.Rows(i).Item("cswfre_status")
    ''                    dr.Item("Source") = dtTemp.Rows(i).Item("Source")
    ''                    dr.Item("postq_backend_sys") = "LA"
    ''                    objFT.Rows.Add(dr)
    ''                Next
    ''                objFT.AcceptChanges()
    ''            End If
    ''        Else
    ''            Exit Function
    ''        End If
    ''    Else
    ''        Exit Function
    ''    End If

    ''    GetFundTransaction = True

    ''End Function

    ''Public Function GetFundTransactionDetails(ByVal strTxType As String, ByVal strTxID As String, ByVal strLA As String, ByVal strPolicy As String, ByRef objDS As DataSet, ByRef strError As String)

    ''    Dim strSQL, strSwitchOutID, strFundListO, strFundListI As String
    ''    Dim sqlconnect As New SqlConnection
    ''    Dim sqlda As SqlDataAdapter
    ''    Dim dtTemp, dtSwitchOut, dtSwitchIn, dtFundName, dtFundAlloc As DataTable
    ''    Dim dteStartDate As Date
    ''    Dim dblAllocAmt As Double

    ''    objDS = New DataSet("FundHist")

    ''    GetFundTransactionDetails = False

    ''    If strTxType <> "ULA" And strTxType <> "ULB" Then Exit Function

    ''    If strTxType = "ULA" Then

    ''        ' -------- Fund Allocation --------
    ''        strSQL = "select * from pos_fundalloc_hist where posfh_trans_id = " & strTxID & _
    ''            " and posfh_status = 'Completed'"
    ''        If GetDT(strSQL, strPOSConn, dtTemp, strError) Then
    ''            'strSwitchOutID = dtTemp.Rows(0).Item("possm_switchout_id")
    ''            'dteStartDate = dtTemp.Rows(0).Item("possm_eff_date")
    ''        Else
    ''            MsgBox(strError)
    ''            Exit Function
    ''        End If

    ''        dtFundAlloc = New DataTable("FundAlloc")
    ''        strFundListO = ""

    ''        dtFundAlloc.Columns.Add("FundCode", GetType(System.String))
    ''        dtFundAlloc.Columns.Add("FundName", GetType(System.String))
    ''        dtFundAlloc.Columns.Add("Allocation", GetType(System.String))

    ''        For i As Integer = 1 To 10
    ''            If Trim(dtTemp.Rows(0).Item("posfh_fund" & i)) <> "" Then
    ''                Dim dr As DataRow
    ''                dr = dtFundAlloc.NewRow
    ''                dr.Item("Fundcode") = Trim(dtTemp.Rows(0).Item("posfh_fund" & i))
    ''                dr.Item("FundName") = ""
    ''                dr.Item("Allocation") = dtTemp.Rows(0).Item("posfh_percent" & i)
    ''                dtFundAlloc.Rows.Add(dr)
    ''                strFundListO = strFundListO & "'" & Trim(dtTemp.Rows(0).Item("posfh_fund" & i)) & "',"
    ''            Else
    ''                Exit For
    ''            End If
    ''        Next
    ''        dtFundAlloc.AcceptChanges()
    ''        If dtFundAlloc.Rows.Count > 0 Then strFundListO = Left(strFundListO, Len(strFundListO) - 1)

    ''        strSQL = "Select mpfinv_code, mpfinv_chi_desc, mpfinv_curr " & _
    ''            " from cswvw_mpf_investment " & _
    ''            " where mpfinv_code in (" & strFundListO & ")"
    ''        If GetDT(strSQL, strCIWConn, dtTemp, strError) Then
    ''            For Each dr As DataRow In dtFundAlloc.Rows
    ''                dtTemp.DefaultView.RowFilter = "mpfinv_code = '" & Trim(dr.Item("FundCode")) & "'"
    ''                dr.Item("FundName") = dtTemp.DefaultView.Item(0).Item("mpfinv_chi_desc")
    ''            Next
    ''            dtFundAlloc.AcceptChanges()
    ''            objDS.Tables.Add(dtFundAlloc)
    ''        Else
    ''            MsgBox(strError)
    ''            Exit Function
    ''        End If

    ''        GetFundTransactionDetails = True

    ''    Else

    ''        ' -------- Fund Switching --------
    ''        ' Find Switch out ID
    ''        strSQL = "Select * " & _
    ''            " From Pos_SwitchIn_Main Where possm_trans_id = " & strTxID & _
    ''            " and possm_status = 'Completed'"
    ''        If GetDT(strSQL, strPOSConn, dtTemp, strError) Then
    ''            strSwitchOutID = dtTemp.Rows(0).Item("possm_switchout_id")
    ''            dteStartDate = dtTemp.Rows(0).Item("possm_eff_date")
    ''            dblAllocAmt = dtTemp.Rows(0).Item("possm_booster_amt")

    ''            dtSwitchIn = New DataTable("SwitchIn")
    ''            dtSwitchIn.Columns.Add("FundCode", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("FundName", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("Allocation", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("Currency", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("UnitPrice", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("ValuDate", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("NoUnits", GetType(System.String))
    ''            dtSwitchIn.Columns.Add("Total", GetType(System.String))

    ''            For i As Integer = 1 To 10
    ''                If Trim(dtTemp.Rows(0).Item("possm_fund" & i)) <> "" Then
    ''                    Dim dr As DataRow
    ''                    dr = dtSwitchIn.NewRow
    ''                    dr.Item("Fundcode") = Trim(dtTemp.Rows(0).Item("possm_fund" & i))
    ''                    dtSwitchIn.Rows.Add(dr)
    ''                    strFundListI = strFundListI & "'" & Trim(dtTemp.Rows(0).Item("possm_fund" & i)) & "',"
    ''                Else
    ''                    Exit For
    ''                End If
    ''            Next
    ''            strFundListI = Left(strFundListI, Len(strFundListI) - 1)

    ''        Else
    ''            MsgBox(strError)
    ''            Exit Function
    ''        End If

    ''        ' Find Switch Out info
    ''        If strLA = "LA" Then
    ''            strSQL = "select pospf_fund, pospf_unit_bal, pospf_sur_unit " & _
    ''                " From pos_parsur_fundAlloc, pos_parsur_hist " & _
    ''                " Where posph_trans_id = " & strSwitchOutID & _
    ''                " And posph_trans_id = pospf_posph_id "
    ''        Else
    ''            strSQL = "select * " & _
    ''                " From pos_parsur_hist " & _
    ''                " Where posph_trans_id = " & strSwitchOutID
    ''        End If

    ''        If GetDT(strSQL, strPOSConn, dtTemp, strError) Then

    ''            dtSwitchOut = New DataTable("SwitchOut")
    ''            dtSwitchOut.Columns.Add("pospf_fund", GetType(System.String))
    ''            dtSwitchOut.Columns.Add("FundName", GetType(System.String))     'Fund Name
    ''            dtSwitchOut.Columns.Add("pospf_sur_unit", GetType(System.String))
    ''            dtSwitchOut.Columns.Add("UnitHold", GetType(System.Double))     'Unit Hold
    ''            dtSwitchOut.Columns.Add("SWOUnits", GetType(System.Double))     'Switch out units
    ''            dtSwitchOut.Columns.Add("Currency", GetType(System.String))     'Currency
    ''            dtSwitchOut.Columns.Add("UnitPrice", GetType(System.String))    'Unit Price
    ''            dtSwitchOut.Columns.Add("ValuDate", GetType(System.String))   'Valuation Date
    ''            dtSwitchOut.Columns.Add("Total", GetType(System.String))        'Total

    ''            If strLA = "LA" Then
    ''                'For Each dr As DataRow In dtSwitchOut.Rows
    ''                For Each dr As DataRow In dtTemp.Rows
    ''                    strFundListO = strFundListO & "'" & Trim(dr.Item("pospf_fund")) & "',"

    ''                    Dim dr1 As DataRow
    ''                    dr1 = dtSwitchOut.NewRow
    ''                    dr1.Item("pospf_fund") = dr.Item("pospf_fund")
    ''                    dr1.Item("pospf_sur_unit") = Math.Round(dr.Item("pospf_sur_unit"), 0, MidpointRounding.ToEven)
    ''                    dtSwitchOut.Rows.Add(dr1)
    ''                Next
    ''            Else
    ''                For i As Integer = 1 To 10
    ''                    If Trim(dtTemp.Rows(0).Item("posph_fund" & i)) <> "" Then
    ''                        Dim dr As DataRow
    ''                        dr = dtSwitchOut.NewRow
    ''                        dr.Item("pospf_fund") = Trim(dtTemp.Rows(0).Item("posph_fund" & i))
    ''                        If dr.Item("pospf_unit_bal") <> 0 Then
    ''                            dr.Item("pospf_sur_unit") = Math.Round(dtTemp.Rows(0).Item("posph_sur_unit" & i) / dtTemp.Rows(0).Item("posph_unit_bal" & i) * 100, 0, MidpointRounding.ToEven)
    ''                        Else
    ''                            dr.Item("pospf_sur_unit") = "N/A"
    ''                        End If
    ''                        dtSwitchOut.Rows.Add(dr)
    ''                        strFundListO = strFundListO & "'" & Trim(dtTemp.Rows(0).Item("posph_fund" & i)) & "',"
    ''                    Else
    ''                        Exit For
    ''                    End If
    ''                Next

    ''            End If

    ''            strFundListO = Left(strFundListO, Len(strFundListO) - 1)

    ''            ' Find fund inforamtion
    ''            strSQL = "Select f1.cswfdb_fund_code, f1.cswfdb_cal_date, f1.cswfdb_unt_hld, mpfinv_chi_desc, mpfinv_curr, mpfval_unit_price, mpfval_to_date " & _
    ''                " from csw_fund_balance f1, " & _
    ''                " (select cswfdb_fund_code, max(cswfdb_cal_date) as cswfdb_cal_date from csw_fund_balance " & _
    ''                " where cswfdb_pono = '" & strPolicy & "' and cswfdb_cal_date <= '" & Format(dteStartDate, "MM/dd/yyyy") & "' " & _
    ''                " group by cswfdb_fund_code) f2, cswvw_mpf_investment LEFT JOIN cswvw_mpf_valuation v1 " & _
    ''                " ON mpfval_invest_fund = mpfinv_code " & _
    ''                " and mpfval_to_date = (select min(mpfval_to_date) from cswvw_mpf_valuation v2 " & _
    ''                " where v2.mpfval_invest_fund = v1.mpfval_invest_fund and v2.mpfval_to_date > '" & Format(dteStartDate, "MM/dd/yyyy") & "') " & _
    ''                " where f1.cswfdb_pono = '" & strPolicy & "' " & _
    ''                " and f1.cswfdb_fund_code = f2.cswfdb_fund_code " & _
    ''                " and f1.cswfdb_cal_date = f2.cswfdb_cal_date " & _
    ''                " and f1.cswfdb_fund_code = mpfinv_code " & _
    ''                " and mpfinv_code in (" & strFundListO & ")"

    ''            If GetDT(strSQL, strCIWConn, dtTemp, strError) Then
    ''                dtFundName = dtTemp.Copy

    ''                For Each dr As DataRow In dtSwitchOut.Rows
    ''                    dtFundName.DefaultView.RowFilter = "cswfdb_fund_code = '" & Trim(dr.Item("pospf_fund")) & "'"

    ''                    dr.Item("FundName") = dtFundName.DefaultView.Item(0).Item("mpfinv_chi_desc")
    ''                    dr.Item("UnitHold") = dtFundName.DefaultView.Item(0).Item("cswfdb_unt_hld")
    ''                    If IsNumeric(dr.Item("pospf_sur_unit")) Then
    ''                        dr.Item("SWOUnits") = Math.Round(dtFundName.DefaultView.Item(0).Item("cswfdb_unt_hld") * dr.Item("pospf_sur_unit") / 100, 5, MidpointRounding.ToEven)
    ''                    Else
    ''                        dr.Item("SWOUnits") = 0
    ''                    End If

    ''                    dr.Item("Currency") = dtFundName.DefaultView.Item(0).Item("mpfinv_curr")

    ''                    If IsDBNull(dtFundName.DefaultView.Item(0).Item("mpfval_unit_price")) Then
    ''                        dr.Item("UnitPrice") = "N/A"
    ''                        dr.Item("ValuDate") = "N/A"
    ''                        dr.Item("Total") = "N/A"
    ''                    Else
    ''                        dr.Item("UnitPrice") = dtFundName.DefaultView.Item(0).Item("mpfval_unit_price")
    ''                        dr.Item("ValuDate") = Format(dtFundName.DefaultView.Item(0).Item("mpfval_to_date"), "MM/dd/yyyy")
    ''                        dr.Item("Total") = Math.Round((dr.Item("UnitHold") * dr.Item("pospf_sur_unit") / 100 * dtFundName.DefaultView.Item(0).Item("mpfval_unit_price")) - 0.005, 2)
    ''                    End If
    ''                Next

    ''                dtSwitchOut.AcceptChanges()
    ''                objDS.Tables.Add(dtSwitchOut)
    ''            Else
    ''                MsgBox(strError)
    ''                Exit Function
    ''            End If

    ''        Else
    ''            MsgBox(strError)
    ''            Exit Function
    ''        End If

    ''        ' Get Switch IN details
    ''        Dim objAES As New LifeClientInterfaceComponent.CommonControl
    ''        Dim dtRece As New DataTable
    ''        Dim dsRece As New DataSet
    ''        Dim Util As New Utility.Utility
    ''        Dim strTradeDate As String
    ''        objAES.ComHeader = gobjDBHeader

    ''        strTradeDate = ""

    ''        If strLA = "LA" Then
    ''            If Not objAES.GetUnitTranHistory(strPolicy, dsRece, strError, dteStartDate, DateAdd(DateInterval.Month, 1, dteStartDate)) Then
    ''                MsgBox(strError)
    ''                Exit Function
    ''            End If
    ''        Else
    ''            strSQL = "Select cswuth_fund_code as UTRFCD, cswuth_cal_tr_date as UTRTRD, cswuth_amount as UTRAMT, cswuth_unit as UTRUNO " & _
    ''                " from csw_unit_tran where cswuth_poli_no = '2050005282' and cswuth_pay_type = 'FSW' and cswuth_amount > 0 " & _
    ''                " and cswuth_cal_tr_date between '" & dteStartDate & "' and '" & DateAdd(DateInterval.Month, 1, dteStartDate) & "'"

    ''            If Not GetDT(strSQL, strCIWConn, dsRece.Tables(0), strError) Then
    ''                MsgBox(strError)
    ''                Exit Function
    ''            End If
    ''        End If

    ''        With dsRece.Tables(0)

    ''            ' Unit Tran record found
    ''            If .Rows.Count > 0 Then
    ''                ' Find latest switch-in records
    ''                .DefaultView.RowFilter = "UTRPAY = 'Switch in/out'" & _
    ''                    "and UTRAMT > 0 and UTRTRD >= '" & CInt(Format(dteStartDate, "yyyyMMdd") - 18000000) & "' "
    ''                .DefaultView.Sort = "UTRTRD ASC"

    ''                ' Switch-in record found
    ''                If .DefaultView.Count > 0 Then
    ''                    strTradeDate = .DefaultView.Item(0).Item("UTRTRD")
    ''                    .DefaultView.RowFilter = "UTRPAY = 'Switch in/out' and UTRAMT > 0 and UTRTRD = '" & strTradeDate & "' "

    ''                    ' Get Fund name & price
    ''                    strSQL = "Select mpfval_invest_fund, mpfinv_chi_desc, mpfval_unit_price, mpfval_to_date, mpfinv_curr " & _
    ''                        " from cswvw_mpf_investment, cswvw_mpf_valuation v1 " & _
    ''                        " where mpfinv_code = mpfval_invest_fund " & _
    ''                        " and mpfval_to_date = (select min(mpfval_to_date) from cswvw_mpf_valuation v2 " & _
    ''                        " where v2.mpfval_invest_fund = v1.mpfval_invest_fund and v2.mpfval_to_date >= '" & CInt(strTradeDate) + 18000000 & "') " & _
    ''                        " and mpfinv_code in (" & strFundListI & ")"

    ''                    If Not GetDT(strSQL, strCIWConn, dtTemp, strError) Then
    ''                        MsgBox(strError)
    ''                        Exit Function
    ''                    End If

    ''                    For i As Integer = 0 To .DefaultView.Count - 1
    ''                        dtSwitchIn.DefaultView.RowFilter = "FundCode = '" & .DefaultView.Item(i).Item("UTRFCD") & "'"
    ''                        dtTemp.DefaultView.RowFilter = "mpfval_invest_fund = '" & .DefaultView.Item(i).Item("UTRFCD") & "'"

    ''                        dtSwitchIn.DefaultView.Item(0).Item("NoUnits") = .DefaultView.Item(i).Item("UTRUNO")
    ''                        dtSwitchIn.DefaultView.Item(0).Item("Total") = .DefaultView.Item(i).Item("UTRAMT")

    ''                        dtSwitchIn.DefaultView.Item(0).Item("Currency") = dtTemp.DefaultView.Item(0).Item("mpfinv_curr")
    ''                        dtSwitchIn.DefaultView.Item(0).Item("FundName") = dtTemp.DefaultView.Item(0).Item("mpfinv_chi_desc")
    ''                        dtSwitchIn.DefaultView.Item(0).Item("UnitPrice") = dtTemp.DefaultView.Item(0).Item("mpfval_unit_price")
    ''                        dtSwitchIn.DefaultView.Item(0).Item("ValuDate") = Format(dtTemp.DefaultView.Item(0).Item("mpfval_to_date"), "MM/dd/yyyy")

    ''                        If dblAllocAmt > 0 Then
    ''                            dtSwitchIn.DefaultView.Item(0).Item("Allocation") = Math.Round(.DefaultView.Item(i).Item("UTRAMT") / dblAllocAmt * 100, 0, MidpointRounding.ToEven)
    ''                        Else
    ''                            If .DefaultView.Count = 1 Then
    ''                                dtSwitchIn.DefaultView.Item(0).Item("Allocation") = "100"
    ''                            End If
    ''                        End If
    ''                    Next
    ''                End If
    ''            End If

    ''            ' Switch-in not complete yet
    ''            If strTradeDate = "" Then
    ''                ' Get Fund name & price
    ''                strSQL = "Select mpfinv_code, mpfinv_chi_desc, mpfinv_curr " & _
    ''                    " from cswvw_mpf_investment " & _
    ''                    " where mpfinv_code in (" & strFundListI & ")"
    ''                If Not GetDT(strSQL, strCIWConn, dtTemp, strError) Then
    ''                    MsgBox(strError)
    ''                    Exit Function
    ''                End If

    ''                For Each dr As DataRow In dtSwitchIn.Rows
    ''                    dtTemp.DefaultView.RowFilter = "mpfinv_code = '" & dr.Item("FundCode") & "'"
    ''                    dr.Item("Currency") = dtTemp.DefaultView.Item(0).Item("mpfinv_curr")
    ''                    dr.Item("FundName") = dtTemp.DefaultView.Item(0).Item("mpfinv_chi_desc")
    ''                    dr.Item("NoUnits") = "N/A"
    ''                    dr.Item("Total") = "N/A"
    ''                    dr.Item("UnitPrice") = "N/A"
    ''                    dr.Item("ValuDate") = "N/A"
    ''                    dr.Item("Allocation") = "N/A"
    ''                Next
    ''            End If ' Count > 0

    ''            dtSwitchIn.AcceptChanges()
    ''            objDS.Tables.Add(dtSwitchIn)

    ''            GetFundTransactionDetails = True

    ''        End With

    ''    End If ' Transaction type

    ''End Function

    Public Function GetDT(ByVal strSQL As String, ByVal strConn As String, ByRef dtResult As DataTable, ByRef strError As String) As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter

        GetDT = False

        sqlconnect.ConnectionString = strConn
        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        'sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        'sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        sqlda.SelectCommand.CommandTimeout = 0

        dtResult = New DataTable

        Try
            sqlda.Fill(dtResult)
            GetDT = True
        Catch sqlex As SqlClient.SqlException
            strError = sqlex.ToString
        Catch ex As Exception
            strError = ex.ToString
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlda.Dispose()
        End Try

    End Function

    Public Function CheckFSPending(ByVal strPolicy As String, ByVal strErr As String) As Boolean

        Dim dsSendData As New DataSet
        Dim dsRece As New DataSet
        Dim wsCRS As New LifeClientInterfaceComponent.clsCRS
        Dim dtSendData As New DataTable
        Dim dr As DataRow = dtSendData.NewRow()

        CheckFSPending = False

        dtSendData.Columns.Add("PolicyNo")
        dr("PolicyNo") = strPolicy
        dtSendData.Rows.Add(dr)
        dsSendData.Tables.Add(dtSendData)

        Dim dtReceData As New DataTable
        dtReceData.Columns.Add("PendingFlag")
        dsRece.Tables.Add(dtReceData)

        'wsCRS.DBHeader = objDBHeader
        'wsCRS.MQQueuesHeader = objMQQueHeader

        'AC - Change to use configuration setting - start
        '#If UAT = 0 Then
        'AC - Change to use configuration setting - end
        If wsCRS.CheckPendingFSW(dsSendData, dsRece, strErr) = True Then
            If dsRece.Tables(0).Rows(0).Item("PendingFlag") = "N" Then
                CheckFSPending = True
            Else
                MsgBox("Please note that there is a pending fund trade in progress.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            End If
        End If
        'AC - Change to use configuration setting - start
        '#End If
        'AC - Change to use configuration setting - end

        'strSQL = "select postq_trans_id, postq_status " & _
        '    " from pos_trans_queue" & _
        '    " where postq_trans_type in ('ULB') " & _
        '    " and postq_status = '' " & _
        '    " and postq_policy_no = '" & strPolicy & "' "
        'If GetDT(strSQL, strPOSConn, dtDummy, strError) Then
        '    If Not dtDummy Is Nothing AndAlso dtDummy.Rows.Count > 0 Then
        '        MsgBox("Please note that there is a pending Fund Switching request.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        '    End If
        'Else
        '    CheckFSPending = True
        'End If

    End Function

    Public Function SetupHelp(ByRef dtHelpInfo As DataTable, ByVal strError As String) As Boolean

        Dim strSQL As String

        SetupHelp = False

        strSQL = "Select * from csw_system_value where cswsyv_field_name like 'Help%'"
        If GetDT(strSQL, strCIWConn, dtHelpInfo, strError) Then
            SetupHelp = True
        End If

    End Function
    ''' <summary>
    ''' Export the datatable to csv file.
    ''' Lubin 2022-11-02 Add exportCnt parmater.
    ''' </summary>
    ''' <param name="strPath">output file path</param>
    ''' <param name="dtFinal">DataTable object</param>
    ''' <returns>Is succ?</returns>
    Public Function ExportCSV(ByVal strPath As String, ByVal dtFinal As DataTable) As Boolean
        Dim exportCnt As Integer = 0
        Return ExportCSV(strPath, dtFinal, exportCnt)
    End Function
    ''' <summary>
    ''' Export the datatable to csv file.
    ''' Lubin 2022-11-02 Add exportCnt parmater.
    ''' </summary>
    ''' <param name="strPath">output file path</param>
    ''' <param name="dtFinal">DataTable object</param>
    ''' <param name="exportCnt">Export count</param>
    ''' <returns>Is succ?</returns>
    Public Function ExportCSV(ByVal strPath As String, ByVal dtFinal As DataTable, ByRef exportCnt As Integer) As Boolean

        ExportCSV = False

        If strPath <> "" AndAlso dtFinal.Rows.Count > 0 Then

            wndMain.Cursor = Cursors.WaitCursor

            'Open the CSV file.
            Dim csvStream As New IO.StreamWriter(strPath, False, System.Text.Encoding.UTF8) 'SQL2008

            'Use a string builder for efficiency.
            Dim line As New System.Text.StringBuilder

            Dim index As Integer

            'Iterate over all the columns in the table and get the column names.
            For index = 0 To dtFinal.Columns.Count - 1
                If index > 0 Then
                    'Put a comma between column names.
                    line.Append(","c)
                End If

                'Precede and follow each column name with a double quote
                'and escape each double quote with another double quote.
                line.Append(""""c)
                line.Append(dtFinal.Columns(index).ColumnName.Replace("""", """"""))
                line.Append(""""c)
            Next

            'Write the line to the file.
            csvStream.WriteLine(line.ToString())

            'Iterate over all the rows in the table.
            'For Each row As DataRow In dtFinal.Rows
            For i As Integer = 0 To dtFinal.DefaultView.Count - 1
                'Clear the line of text.
                line.Remove(0, line.Length)

                'Iterate over all the fields in the row and get the field values.
                For index = 0 To dtFinal.Columns.Count - 1
                    If index > 0 Then
                        'Put a comma between field values.
                        line.Append(","c)
                    End If

                    'Precede and follow each field value with a double quote
                    'and escape each double quote with another double quote.
                    line.Append(""""c)
                    'line.Append(row(index).ToString().Replace("""", """"""))
                    line.Append(dtFinal.DefaultView.Item(i).Item(index).ToString().Replace("""", """"""))
                    line.Append(""""c)
                Next

                'Write the line to the file.
                csvStream.WriteLine(line.ToString())
            Next

            csvStream.Flush()
            csvStream.Close()

            wndMain.Cursor = Cursors.Default

        End If

        ExportCSV = True
        exportCnt = dtFinal.DefaultView.Count
    End Function
    ''' <summary>
    ''' Get Policy Map
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Public Function GetPolicyMap(ByRef strInPolicy As String, ByRef strOutPolicy As String, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Try
            'oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
            'Dim dr As DataRow = APIServiceBL.QueryFirstRow(companyName,
            '                                "FRM_POLICY_MAP",
            '                                New Dictionary(Of String, String) From {
            '                                {"strAgent", strInPolicy}
            '                                }, False)
            Dim dr As DataRow = APIServiceBL.QueryFirstRow(companyName,
                                            "FRM_POLICY_MAP",
                                            New Dictionary(Of String, String) From {
                                            {IIf(IsAssurance, "strAgent", "strInPolicy"), strInPolicy}
                                            }, False)
            If Not IsNothing(dr) Then

                If strInPolicy = dr.Item("cswpm_capsil_policy") Then
                    strOutPolicy = dr.Item("cswpm_la_policy")
                ElseIf strInPolicy = dr.Item("cswpm_la_policy") Then
                    strOutPolicy = dr.Item("cswpm_capsil_policy")
                End If
            End If
            Return True
        Catch ex As Exception
            strErr = ex.Message
        End Try
        'strSQL = "select * from csw_policy_map " & _
        '    " where cswpm_la_policy = '" & strInPolicy & "'" & _
        '    " or cswpm_capsil_policy = '" & strInPolicy & "'"

        Return retVal
    End Function

    ''' <summary>
    ''' Get Policy Map
    ''' Human 2023-01-31 ITSR-4362 Cheque Outsourcing
    ''' </summary>
    Public Function GetChequeInfo(ByRef strInPolicy As String, ByRef dtChequeInfo As DataTable, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Dim posDB As String = gcPOS
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "CHEQUE_OUTSOURCING",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInPolicy", strInPolicy},
                                                                {"posDB", posDB}
                                                            })

            If Not IsNothing(retDs) Then
                If retDs.Tables(0).Rows.Count > 0 Then
                    dtChequeInfo = retDs.Tables(0)
                    Return True
                End If
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function
    ' **** ES005 end ****

    ''' <summary>
    ''' Get Policy Map
    ''' Human 2023-01-31 ITSR-4362 Cheque Outsourcing
    ''' </summary>
    Public Function GetAgentHistory(ByRef strInPolicy As String, ByRef dtAgentHistory As DataTable, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "AGENT_HISTORY",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInPolicy", strInPolicy}
                                                            })

            If Not IsNothing(retDs) Then
                If retDs.Tables(0).Rows.Count > 0 Then
                    dtAgentHistory = retDs.Tables(0)
                    Return True
                End If
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get Agent Info Related Table
    ''' Kenny Siu 2023-03-17 New Agent Head Title
    ''' </summary>
    Public Function GetAgentInfoRelatedTables(ByRef strInPolicy As String, ByRef agentList As String, ByRef dsAgentInfo As DataSet, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "GET_AGENT_INFO_RELATED_TABLE",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInPolicy", strInPolicy},
                                                                {"agentList", agentList}
                                                            })

            If Not IsNothing(retDs) Then
                dsAgentInfo = retDs
                Return True
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function


    ' CIC Integration
    Public Function GetCustomerIDByMobile(ByVal strMobile As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlCmd As New SqlCommand
        Dim sqlda As SqlDataAdapter
        Dim objCUST As New DataTable("CUST")
        Dim blnFound As Boolean = False
        Dim strCID As String = 0
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select CustomerID, NameSuffix, FirstName, CNANO as ClientID " &
            " From customer, ordcna " &
            " Where (phonemobile = '" & strMobile & "' or phonepager = '" & strMobile & "') " &
            " And CustomerID = CNACIW"
        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(objCUST)
            GetCustomerIDByMobile = objCUST

            If objCUST.Rows.Count > 0 Then
                blnFound = True
                strCID = objCUST.Rows(0).Item("CustomerID")
            End If

            strSQL = "Insert into " & serverPrefix & "csw_wml_site_stat (cswwss_cid,cswwss_datetime,cswwss_page_name,cswwss_duration,cswwss_done) " &
                " Select 'CTICRS',GETDATE(),'" & gsUser & "," & strMobile & "'," & strCID & ",'" & IIf(blnFound, "Y", "N") & "'"
            sqlconnect.Open()
            sqlCmd.Connection = sqlconnect
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.OkOnly, "CRS - GetCustomerIDByMobile")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "CRS - GetCustomerIDByMobile")
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlda.Dispose()
        End Try

    End Function

    ' Get elite/noble agent
    Public Function GetEliteAgent1(ByVal strAgent As String) As String

        Dim strSQL, strGrade As String
        Dim objDBL As Object
        Dim objBRK As ADODB.Recordset

        GetEliteAgent1 = "STANDARD"

        strSQL = "Select * from cam_agent_info_elite " &
            " where camaie_agent_no = '" & strAgent & "' " &
            " and camaie_effective_date <= getdate() " &
            " order by camaie_effective_date desc"

        Try
            objDBL = CreateObject("dbsecurity.database")
            If objDBL.Connect("ITOPER", "LAS", strLASCAM, "LASUPDATE") Then
                objBRK = objDBL.executestatement(strSQL)
                If Not objBRK Is Nothing Then
                    If objBRK.RecordCount > 0 Then
                        strGrade = objBRK.Fields("camaie_agent_class").Value
                        Select Case strGrade
                            Case 0
                                GetEliteAgent1 = "STANDARD"
                            Case 1
                                GetEliteAgent1 = "ELITE"
                            Case 2
                                GetEliteAgent1 = "NOBLE"
                        End Select
                    End If
                End If
            Else
                MsgBox("Execute error", MsgBoxStyle.OkOnly, "CRS - GetEliteAgent")
            End If
            objDBL.Disconnect()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.OkOnly, "CRS - GetEliteAgent")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "CRS - GetEliteAgent")
        Finally
            objDBL = Nothing
            objBRK = Nothing
        End Try

    End Function

    ' Get elite/noble agent
    ''' <summary>
    ''' Get Elite Agent
    ''' Lubin 2022-11-07 TSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Public Function GetEliteAgent(ByVal strAgent As String, Optional companyName As String = "") As String

        Dim retEliteAgent = "STANDARD"
        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Try

            Dim strGrade As String = APIServiceBL.QueryScalar(Of String)(companyName,
                                            "FRM_ELITE_AGENT",
                                            New Dictionary(Of String, String) From {
                                            {"strAgent", strAgent}
                                            }, "", True)
            Select Case strGrade
                Case "0"
                    retEliteAgent = "STANDARD"
                Case "1"
                    retEliteAgent = "ELITE"
                Case "2"
                    retEliteAgent = "NOBLE"
                Case Else
                    retEliteAgent = "STANDARD"
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "CRS - GetEliteAgent")
        End Try
        Return retEliteAgent
    End Function

    Public Function GetLAPolicyValue(ByVal strComp As String, ByVal strPolicy As String, ByVal strValType As String, ByRef dblTotal As Double, ByRef strErr As String, Optional ByVal datAsOf As Date = #1/1/1900#) As DataTable

        Dim objVAL As New LifeClientInterfaceComponent.clsPOS
        Dim objLaWrapper As Object
        Dim dsSend As New DataSet
        Dim dsRece As New DataSet
        Dim dtSend As New DataTable
        Dim dtRece As New DataTable
        Dim drSend As DataRow

        Try
            'oliver 2024-3-7 added for NUT-3813
            Dim objMQQueuesHeader As New Utility.Utility.MQHeader
            Dim objDBHeader As New Utility.Utility.ComHeader
            Dim objPOSHeader As New Utility.Utility.POSHeader
            GetMQHeader(strComp, objMQQueuesHeader, objDBHeader, objPOSHeader)
            objVAL.MQQueuesHeader = objMQQueuesHeader
            objVAL.DBHeader = objDBHeader
            objVAL.POSHeader = objPOSHeader

            Select Case strValType
                Case "P"
                    dtSend.Columns.Add("PolicyNo", GetType(System.String))
                    dtSend.Columns.Add("QuoteDate", GetType(System.DateTime))
                    'oliver 2024-3-7 added for NUT-3813
                    dtSend.Columns.Add("PrintFlag1", GetType(System.String))

                    drSend = dtSend.NewRow
                    drSend("PolicyNo") = strPolicy
                    drSend("QuoteDate") = datAsOf
                    'oliver 2024-3-7 added for NUT-3813
                    drSend("PrintFlag1") = "N"
                    dtSend.Rows.Add(drSend)

                    dsSend.Tables.Add(dtSend)
                    dsRece.Tables.Add(dtRece)

                    If objVAL.PolicyValueEnq(dsSend, dsRece, strErr) Then
                        If dsRece.Tables.Count > 0 AndAlso dsRece.Tables(0).Rows.Count > 0 Then
                            GetLAPolicyValue = dsRece.Tables(0)
                        End If
                    End If

                Case "S"
                    objLaWrapper = CreateObject("LASVB6Wrapper.POSWrapper")
                    objLaWrapper.SetPOSWrapperEnv(strComp & g_Env)

                    Call objLaWrapper.GetSurVal(gsUser, strPolicy, dblTotal, strErr, Today)

                    If strErr <> "" Then
                        MsgBox(strErr)
                    End If

                    'ITDLSW002 - REMARK START
                    'dblTotal += 1
                    'ITDLSW002 - REMARK END
                    'dtSend.Columns.Add("PolicyNo", Type.GetType("System.String"))
                    'dtSend.Columns.Add("EffDate", Type.GetType("System.DateTime"))

                    'drSend = dtSend.NewRow
                    'drSend("PolicyNo") = strPolicy
                    'drSend("EffDate") = datAsOf
                    'dtSend.Rows.Add(drSend)

                    ''dtSend.Rows.Add(drSend)
                    'dsSend.Tables.Add(dtSend)

                    'If Not objVAL.GetSurValue(dsSend, dsRece, strErr) Then
                    '    MsgBox(strErr)
                    'Else
                    '    If dsRece.Tables(0).Rows.Count > 0 Then
                    '        For i As Integer = 0 To dsRece.Tables(0).Rows.Count - 1
                    '            dblTotal = dblTotal + Val(dsRece.Tables(0).Rows(i).Item("Est_Value"))
                    '        Next
                    '    End If
                    'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            objVAL = Nothing
            objLaWrapper = Nothing
        End Try

    End Function

    Public Function GetLAPolicyValueMcu(ByVal strPolicy As String, ByVal strValType As String, ByRef dblTotal As Double, ByRef strErr As String, Optional ByVal datAsOf As Date = #1/1/1900#) As DataTable

        Dim objVAL As New LifeClientInterfaceComponent.clsPOS
        Dim objLaWrapper As Object
        Dim dsSend As New DataSet
        Dim dsRece As New DataSet
        Dim dtSend As New DataTable
        Dim dtRece As New DataTable
        Dim drSend As DataRow
        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim objDBHeaderMcu As Utility.Utility.ComHeader

        Try
            objMQQueHeaderMcu = gobjMQQueHeader
            objMQQueHeaderMcu.CompanyID = g_McuComp
            objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
            objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
            objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
            objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ

            objVAL.MQQueuesHeader = objMQQueHeaderMcu
            objDBHeaderMcu = gobjDBHeader
            objDBHeaderMcu.CompanyID = g_McuComp
            objDBHeaderMcu.EnvironmentUse = g_McuEnv
            objVAL.DBHeader = objDBHeaderMcu
            objVAL.POSHeader = gobjPOSHeader


            Select Case strValType
                Case "P"
                    dtSend.Columns.Add("PolicyNo", GetType(System.String))
                    dtSend.Columns.Add("QuoteDate", GetType(System.DateTime))

                    drSend = dtSend.NewRow
                    drSend("PolicyNo") = strPolicy
                    drSend("QuoteDate") = datAsOf
                    dtSend.Rows.Add(drSend)

                    dsSend.Tables.Add(dtSend)
                    dsRece.Tables.Add(dtRece)

                    If objVAL.PolicyValueEnq(dsSend, dsRece, strErr) Then
                        If dsRece.Tables.Count > 0 AndAlso dsRece.Tables(0).Rows.Count > 0 Then
                            GetLAPolicyValueMcu = dsRece.Tables(0)
                        End If
                    End If

                Case "S"
                    objLaWrapper = CreateObject("LASVB6Wrapper.POSWrapper")
                    objLaWrapper.SetPOSWrapperEnv(g_McuComp & g_McuEnv)

                    Call objLaWrapper.GetSurVal(gsUser, strPolicy, dblTotal, strErr, Today)

                    If strErr <> "" Then
                        MsgBox(strErr)
                    End If

                    'ITDLSW002 - REMARK START
                    'dblTotal += 1
                    'ITDLSW002 - REMARK END
                    'dtSend.Columns.Add("PolicyNo", Type.GetType("System.String"))
                    'dtSend.Columns.Add("EffDate", Type.GetType("System.DateTime"))

                    'drSend = dtSend.NewRow
                    'drSend("PolicyNo") = strPolicy
                    'drSend("EffDate") = datAsOf
                    'dtSend.Rows.Add(drSend)

                    ''dtSend.Rows.Add(drSend)
                    'dsSend.Tables.Add(dtSend)

                    'If Not objVAL.GetSurValue(dsSend, dsRece, strErr) Then
                    '    MsgBox(strErr)
                    'Else
                    '    If dsRece.Tables(0).Rows.Count > 0 Then
                    '        For i As Integer = 0 To dsRece.Tables(0).Rows.Count - 1
                    '            dblTotal = dblTotal + Val(dsRece.Tables(0).Rows(i).Item("Est_Value"))
                    '        Next
                    '    End If
                    'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            objVAL = Nothing
            objLaWrapper = Nothing
        End Try

    End Function

    'oliver 2023-12-12 added for Switch Over Code from Assurance to Bermuda
    ''' <summary>
    ''' Call QuoteRpu of AS400.
    ''' Lubin 2022-11-03 Changed/ITSR-3487 CRS Macau Phase 3
    ''' Changes:Add PrintFlag1 parameter to bo request;catch up the error message
    ''' </summary>
    ''' <param name="policyNum">Policy number</param>
    ''' <param name="issueQuoteLetter">default is false</param>
    ''' <param name="dsQuoteRpu">return result of dataset</param>
    ''' <param name="TotalCSV">return value</param>
    ''' <param name="errorMsg">errorMessage</param>
    ''' <returns>is succ</returns>
    Public Function QuoteRpu(ByVal CompanyID As String, ByVal policyNum As String, ByVal issueQuoteLetter As Boolean, ByRef dsQuoteRpu As DataSet, ByRef TotalCSV As Double, ByRef errorMsg As String) As Boolean

        Dim wspos As POSWS.POSWS

        Try
            'Check total cash value first
            Dim dt As DataTable = GetEnquiryBO()
            Dim row As DataRow = dt.NewRow()
            Dim dsSend As New DataSet
            Dim ds As New DataSet
            Dim strErr As String = ""

            Dim objCI As LifeClientInterfaceComponent.clsPOS
            objCI = New LifeClientInterfaceComponent.clsPOS()

            If CompanyID = "ING" Then
                objCI.DBHeader = gobjDBHeader
                objCI.MQQueuesHeader = gobjMQQueHeader
            Else
                objCI.DBHeader = gobjMcuDBHeader
                objCI.MQQueuesHeader = gobjMcuMQQueHeader
            End If





            row("QuoteDate") = gBusDate
            row("PolicyNo") = policyNum

            row("PrintFlag1") = "N" ' Lubin 2022-11-03 ITSR-3487:Add PrintFlag1 parameter to bo request
            dt.Rows.Add(row)
            dsSend.Tables.Add(dt)
            If Not objCI.PolicyValueEnq(dsSend, ds, strErr) Then
                Throw New Exception(strErr)
            Else
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    TotalCSV = ds.Tables(0).Rows(0).Item("TotalSurVal")
                End If
            End If

            wspos = New POSWS.POSWS

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            wspos.Url = "http://hkcomprd2/posws/posws.asmx"
            '#Else
            '            wspos.Url = "http://hkcomdev2/ingsit02posws/posws.asmx"
            '#End If
            If gUAT = False Then
                wspos.Url = "http://hkcomprd2/posws/posws.asmx"
            Else
                If CompanyID = "ING" Then wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjDBHeader, gobjMQQueHeader)
                If CompanyID = "MCU" Then wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjMcuDBHeader, gobjMcuMQQueHeader)
            End If
            'AC - Change to use configuration setting - end

            wspos.MQSOAPHeaderValue = GetPOSWSMQHeader()
            wspos.DBSOAPHeaderValue = GetPOSWSDBHeader()
            wspos.Credentials = System.Net.CredentialCache.DefaultCredentials
            wspos.Timeout = 10000000

            Return wspos.QuoteRpu(policyNum, issueQuoteLetter, dsQuoteRpu, errorMsg)
        Catch ex As Exception
            errorMsg = ex.Message
            If Not IsNothing(wspos) Then
                wspos.Dispose()
            End If
            Return False
        End Try

    End Function

    'oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
    'Public Function QuoteRpu(ByVal policyNum As String, ByVal issueQuoteLetter As Boolean, ByVal isAsur As Boolean, ByRef dsQuoteRpu As DataSet, ByRef TotalCSV As Double, ByRef errorMsg As String) As Boolean
    ''' <summary>
    ''' Call QuoteRpu of AS400.
    ''' Lubin 2022-11-03 Changed/ITSR-3487 CRS Macau Phase 3
    ''' Changes:Add PrintFlag1 parameter to bo request;catch up the error message
    ''' </summary>
    ''' <param name="policyNum">Policy number</param>
    ''' <param name="issueQuoteLetter">default is false</param>
    ''' <param name="dsQuoteRpu">return result of dataset</param>
    ''' <param name="TotalCSV">return value</param>
    ''' <param name="errorMsg">errorMessage</param>
    ''' <returns>is succ</returns>
    Public Function QuoteRpuAsur(ByVal policyNum As String, ByVal issueQuoteLetter As Boolean, ByVal isAsur As Boolean, ByRef dsQuoteRpu As DataSet, ByRef TotalCSV As Double, ByRef errorMsg As String) As Boolean

        Dim wspos As POSWS.POSWS

        Try
            'Check total cash value first
            Dim dt As DataTable = GetEnquiryBO()
            Dim row As DataRow = dt.NewRow()
            Dim dsSend As New DataSet
            Dim ds As New DataSet
            Dim strErr As String = ""

            Dim objCI As LifeClientInterfaceComponent.clsPOS
            objCI = New LifeClientInterfaceComponent.clsPOS()

            objCI.DBHeader = IIf(isAsur, gobjLacDBHeader, gobjDBHeader)
            objCI.MQQueuesHeader = IIf(isAsur, gobjLacMQQueHeader, gobjMQQueHeader)

            row("QuoteDate") = gBusDate
            row("PolicyNo") = policyNum

            row("PrintFlag1") = "N" ' Lubin 2022-11-03 ITSR-3487:Add PrintFlag1 parameter to bo request
            dt.Rows.Add(row)
            dsSend.Tables.Add(dt)
            If Not objCI.PolicyValueEnq(dsSend, ds, strErr) Then
                Throw New Exception(strErr)
            Else
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    TotalCSV = ds.Tables(0).Rows(0).Item("TotalSurVal")
                End If
            End If

            wspos = New POSWS.POSWS

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            wspos.Url = "http://hkcomprd2/posws/posws.asmx"
            '#Else
            '            wspos.Url = "http://hkcomdev2/ingsit02posws/posws.asmx"
            '#End If
            If gUAT = False Then
                wspos.Url = "http://hkcomprd2/posws/posws.asmx"
            Else
                wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjDBHeader, gobjMQQueHeader)
            End If
            'AC - Change to use configuration setting - end

            wspos.MQSOAPHeaderValue = GetPOSWSMQHeaderByComp(isAsur)
            wspos.DBSOAPHeaderValue = GetPOSWSDBHeaderByComp(isAsur)
            wspos.Credentials = System.Net.CredentialCache.DefaultCredentials
            wspos.Timeout = 10000000

            Return wspos.QuoteRpu(policyNum, issueQuoteLetter, dsQuoteRpu, errorMsg)
        Catch ex As Exception
            errorMsg = ex.Message
            If Not IsNothing(wspos) Then
                wspos.Dispose()
            End If
            Return False
        End Try

    End Function

    Private Function GetPOSWSMQHeader() As POSWS.MQSOAPHeader

        Dim objPowsMQHeader As New POSWS.MQSOAPHeader
        objPowsMQHeader.QueueManager = gobjMQQueHeader.QueueManager
        objPowsMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
        objPowsMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
        objPowsMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
        objPowsMQHeader.Timeout = 300000

        objPowsMQHeader.ProjectAlias = gobjMQQueHeader.ProjectAlias
        objPowsMQHeader.ConnectionAlias = gobjMQQueHeader.ConnectionAlias
        objPowsMQHeader.UserType = gobjMQQueHeader.UserType
        objPowsMQHeader.EnvironmentUse = gobjMQQueHeader.EnvironmentUse
        objPowsMQHeader.CompanyID = gobjMQQueHeader.CompanyID
        objPowsMQHeader.UserID = gobjMQQueHeader.UserID
        Return objPowsMQHeader

    End Function

    Private Function GetPOSWSMQHeaderByComp(ByVal isAsur As Boolean) As POSWS.MQSOAPHeader

        Dim mqHeader As New Utility.Utility.MQHeader
        mqHeader = IIf(isAsur, gobjLacMQQueHeader, gobjMQQueHeader)

        Dim objPowsMQHeader As New POSWS.MQSOAPHeader
        objPowsMQHeader.QueueManager = mqHeader.QueueManager
        objPowsMQHeader.RemoteQueue = mqHeader.RemoteQueue
        objPowsMQHeader.ReplyToQueue = mqHeader.ReplyToQueue
        objPowsMQHeader.LocalQueue = mqHeader.LocalQueue
        objPowsMQHeader.Timeout = 300000

        objPowsMQHeader.ProjectAlias = mqHeader.ProjectAlias
        objPowsMQHeader.ConnectionAlias = mqHeader.ConnectionAlias
        objPowsMQHeader.UserType = mqHeader.UserType
        objPowsMQHeader.EnvironmentUse = mqHeader.EnvironmentUse
        objPowsMQHeader.CompanyID = mqHeader.CompanyID
        objPowsMQHeader.UserID = mqHeader.UserID
        Return objPowsMQHeader

    End Function

    Private Function GetMcuPOSWSMQHeader() As POSWS.MQSOAPHeader

        Dim objPowsMQHeader As New POSWS.MQSOAPHeader
        objPowsMQHeader.QueueManager = gobjMcuMQQueHeader.QueueManager
        objPowsMQHeader.RemoteQueue = gobjMcuMQQueHeader.RemoteQueue
        objPowsMQHeader.ReplyToQueue = gobjMcuMQQueHeader.ReplyToQueue
        objPowsMQHeader.LocalQueue = gobjMcuMQQueHeader.LocalQueue
        objPowsMQHeader.Timeout = 300000

        objPowsMQHeader.ProjectAlias = gobjMcuMQQueHeader.ProjectAlias
        objPowsMQHeader.ConnectionAlias = gobjMcuMQQueHeader.ConnectionAlias
        objPowsMQHeader.UserType = gobjMcuMQQueHeader.UserType
        objPowsMQHeader.EnvironmentUse = gobjMcuMQQueHeader.EnvironmentUse
        objPowsMQHeader.CompanyID = gobjMcuMQQueHeader.CompanyID
        objPowsMQHeader.UserID = gobjMcuMQQueHeader.UserID
        Return objPowsMQHeader

    End Function

    Private Function GetPOSWSDBHeader() As POSWS.DBSOAPHeader
        'Try
        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        objPowsDBHeader.User = gobjDBHeader.UserID
        objPowsDBHeader.Project = gobjDBHeader.ProjectAlias
        objPowsDBHeader.Env = gobjDBHeader.EnvironmentUse
        objPowsDBHeader.Comp = gobjDBHeader.CompanyID
        objPowsDBHeader.ConnectionAlias = gobjDBHeader.CompanyID & "POS" & gobjDBHeader.EnvironmentUse
        objPowsDBHeader.UserType = gobjDBHeader.UserType
        Return objPowsDBHeader

    End Function

    Private Function GetPOSWSDBHeaderByComp(ByVal isAsur As Boolean) As POSWS.DBSOAPHeader
        'Try
        Dim dbHeader As New Utility.Utility.ComHeader
        dbHeader = IIf(isAsur, gobjLacDBHeader, gobjDBHeader)

        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        objPowsDBHeader.User = dbHeader.UserID
        objPowsDBHeader.Project = dbHeader.ProjectAlias
        objPowsDBHeader.Env = dbHeader.EnvironmentUse
        objPowsDBHeader.Comp = dbHeader.CompanyID
        objPowsDBHeader.ConnectionAlias = dbHeader.CompanyID & "POS" & dbHeader.EnvironmentUse
        objPowsDBHeader.UserType = dbHeader.UserType
        Return objPowsDBHeader

    End Function

    Private Function GetMcuPOSWSDBHeader() As POSWS.DBSOAPHeader
        'Try
        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        objPowsDBHeader.User = gobjMcuDBHeader.UserID
        objPowsDBHeader.Project = gobjMcuDBHeader.ProjectAlias
        objPowsDBHeader.Env = gobjMcuDBHeader.EnvironmentUse
        objPowsDBHeader.Comp = gobjMcuDBHeader.CompanyID
        objPowsDBHeader.ConnectionAlias = gobjMcuDBHeader.CompanyID & "POS" & gobjMcuDBHeader.EnvironmentUse
        objPowsDBHeader.UserType = gobjMcuDBHeader.UserType
        Return objPowsDBHeader

    End Function

    Public Function GetCCRWSMQHeader() As CCRWS.MQSOAPHeader

        Dim objCCRwsMQHeader As New CCRWS.MQSOAPHeader
        objCCRwsMQHeader.QueueManager = gobjMQQueHeader.QueueManager
        objCCRwsMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
        objCCRwsMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
        objCCRwsMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
        objCCRwsMQHeader.Timeout = 300000

        objCCRwsMQHeader.CompanyID = gobjMQQueHeader.CompanyID
        objCCRwsMQHeader.UserID = gobjMQQueHeader.UserID

        objCCRwsMQHeader.ProjectAlias = gobjDBHeader.ProjectAlias
        objCCRwsMQHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
        objCCRwsMQHeader.UserType = gobjDBHeader.UserType
        objCCRwsMQHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse

        Return objCCRwsMQHeader

    End Function

    Public Function GetCCRWSDBHeader() As CCRWS.DBSOAPHeader

        Dim objCCRwsDBHeader As New CCRWS.DBSOAPHeader
        objCCRwsDBHeader.User = gobjDBHeader.UserID
        objCCRwsDBHeader.Project = gobjDBHeader.ProjectAlias
        objCCRwsDBHeader.Env = gobjDBHeader.EnvironmentUse
        objCCRwsDBHeader.Comp = gobjDBHeader.CompanyID
        objCCRwsDBHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
        objCCRwsDBHeader.UserType = gobjDBHeader.UserType
        Return objCCRwsDBHeader

    End Function

    Private Function GetMCSWSMQHeader() As MCSWS.MQSOAPHeader

        Dim objMCSwsMQHeader As New MCSWS.MQSOAPHeader
        objMCSwsMQHeader.QueueManager = gobjMQQueHeader.QueueManager
        objMCSwsMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
        objMCSwsMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
        objMCSwsMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
        objMCSwsMQHeader.Timeout = 300000

        objMCSwsMQHeader.CompanyID = gobjMQQueHeader.CompanyID
        objMCSwsMQHeader.UserID = gobjMQQueHeader.UserID

        objMCSwsMQHeader.ProjectAlias = gobjDBHeader.ProjectAlias
        objMCSwsMQHeader.ConnectionAlias = gobjDBHeader.CompanyID & "MCS" & gobjDBHeader.EnvironmentUse
        objMCSwsMQHeader.UserType = gobjDBHeader.UserType
        objMCSwsMQHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse

        Return objMCSwsMQHeader

    End Function

    Private Function GetMCSWSDBHeader() As MCSWS.DBSOAPHeader

        Dim objMCSwsDBHeader As New MCSWS.DBSOAPHeader
        objMCSwsDBHeader.User = gobjDBHeader.UserID
        objMCSwsDBHeader.Project = gobjDBHeader.ProjectAlias
        objMCSwsDBHeader.Env = gobjDBHeader.EnvironmentUse
        objMCSwsDBHeader.Comp = gobjDBHeader.CompanyID
        objMCSwsDBHeader.ConnectionAlias = gobjDBHeader.CompanyID & "MCS" & gobjDBHeader.EnvironmentUse
        objMCSwsDBHeader.UserType = gobjDBHeader.UserType
        Return objMCSwsDBHeader

    End Function

    Public Function GetCRSWSMQHeader(Optional ByVal CompanyID As App.Config.Company = App.Config.Company.ING) As CRSWS.MQSOAPHeader

        Dim objCRSwsMQHeader As New CRSWS.MQSOAPHeader
        Select Case CompanyID
            Case App.Config.Company.ING
                objCRSwsMQHeader.QueueManager = gobjMQQueHeader.QueueManager
                objCRSwsMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
                objCRSwsMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
                objCRSwsMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
                objCRSwsMQHeader.Timeout = 300000

                objCRSwsMQHeader.CompanyID = gobjMQQueHeader.CompanyID
                objCRSwsMQHeader.UserID = gobjMQQueHeader.UserID

                objCRSwsMQHeader.ProjectAlias = gobjDBHeader.ProjectAlias
                objCRSwsMQHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
                objCRSwsMQHeader.UserType = gobjDBHeader.UserType
                objCRSwsMQHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse
            Case App.Config.Company.LAC
                objCRSwsMQHeader.QueueManager = gobjLacMQQueHeader.QueueManager
                objCRSwsMQHeader.RemoteQueue = gobjLacMQQueHeader.RemoteQueue
                objCRSwsMQHeader.ReplyToQueue = gobjLacMQQueHeader.ReplyToQueue
                objCRSwsMQHeader.LocalQueue = gobjLacMQQueHeader.LocalQueue
                objCRSwsMQHeader.Timeout = 300000

                objCRSwsMQHeader.CompanyID = gobjLacMQQueHeader.CompanyID
                objCRSwsMQHeader.UserID = gobjLacMQQueHeader.UserID

                objCRSwsMQHeader.ProjectAlias = gobjLacDBHeader.ProjectAlias
                objCRSwsMQHeader.ConnectionAlias = gobjLacDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
                objCRSwsMQHeader.UserType = gobjLacDBHeader.UserType
                objCRSwsMQHeader.EnvironmentUse = gobjLacDBHeader.EnvironmentUse
            Case App.Config.Company.LAH
                objCRSwsMQHeader.QueueManager = gobjLahMQQueHeader.QueueManager
                objCRSwsMQHeader.RemoteQueue = gobjLahMQQueHeader.RemoteQueue
                objCRSwsMQHeader.ReplyToQueue = gobjLahMQQueHeader.ReplyToQueue
                objCRSwsMQHeader.LocalQueue = gobjLahMQQueHeader.LocalQueue
                objCRSwsMQHeader.Timeout = 300000

                objCRSwsMQHeader.CompanyID = gobjLahMQQueHeader.CompanyID
                objCRSwsMQHeader.UserID = gobjLahMQQueHeader.UserID

                objCRSwsMQHeader.ProjectAlias = gobjLahDBHeader.ProjectAlias
                objCRSwsMQHeader.ConnectionAlias = gobjLahDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
                objCRSwsMQHeader.UserType = gobjLahDBHeader.UserType
                objCRSwsMQHeader.EnvironmentUse = gobjLahDBHeader.EnvironmentUse
            Case App.Config.Company.MCU
                objCRSwsMQHeader.QueueManager = gobjMcuMQQueHeader.QueueManager
                objCRSwsMQHeader.RemoteQueue = gobjMcuMQQueHeader.RemoteQueue
                objCRSwsMQHeader.ReplyToQueue = gobjMcuMQQueHeader.ReplyToQueue
                objCRSwsMQHeader.LocalQueue = gobjMcuMQQueHeader.LocalQueue
                objCRSwsMQHeader.Timeout = 300000

                objCRSwsMQHeader.CompanyID = gobjMcuMQQueHeader.CompanyID
                objCRSwsMQHeader.UserID = gobjMcuMQQueHeader.UserID

                objCRSwsMQHeader.ProjectAlias = gobjMcuDBHeader.ProjectAlias
                objCRSwsMQHeader.ConnectionAlias = gobjMcuDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
                objCRSwsMQHeader.UserType = gobjMcuDBHeader.UserType
                objCRSwsMQHeader.EnvironmentUse = gobjMcuDBHeader.EnvironmentUse


        End Select

        Return objCRSwsMQHeader

    End Function

    Public Function GetMcuCRSWSMQHeader() As CRSWS.MQSOAPHeader

        Dim objMcuCRSwsMQHeader As New CRSWS.MQSOAPHeader
        objMcuCRSwsMQHeader.QueueManager = gobjMcuMQQueHeader.QueueManager
        objMcuCRSwsMQHeader.RemoteQueue = gobjMcuMQQueHeader.RemoteQueue
        objMcuCRSwsMQHeader.ReplyToQueue = gobjMcuMQQueHeader.ReplyToQueue
        objMcuCRSwsMQHeader.LocalQueue = gobjMcuMQQueHeader.LocalQueue
        objMcuCRSwsMQHeader.Timeout = 300000

        objMcuCRSwsMQHeader.CompanyID = gobjMcuMQQueHeader.CompanyID
        objMcuCRSwsMQHeader.UserID = gobjMcuMQQueHeader.UserID

        objMcuCRSwsMQHeader.ProjectAlias = gobjMcuDBHeader.ProjectAlias
        objMcuCRSwsMQHeader.ConnectionAlias = gobjMcuDBHeader.CompanyID & "CIW" & gobjMcuDBHeader.EnvironmentUse
        objMcuCRSwsMQHeader.UserType = gobjMcuDBHeader.UserType
        objMcuCRSwsMQHeader.EnvironmentUse = gobjMcuDBHeader.EnvironmentUse

        Return objMcuCRSwsMQHeader

    End Function

    Public Function GetCRSWSDBHeader() As CRSWS.DBSOAPHeader

        Dim objCRSwsDBHeader As New CRSWS.DBSOAPHeader
        objCRSwsDBHeader.User = gobjDBHeader.UserID
        objCRSwsDBHeader.Project = gobjDBHeader.ProjectAlias
        objCRSwsDBHeader.Env = gobjDBHeader.EnvironmentUse
        objCRSwsDBHeader.Comp = gobjDBHeader.CompanyID
        objCRSwsDBHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
        objCRSwsDBHeader.UserType = gobjDBHeader.UserType
        Return objCRSwsDBHeader

    End Function

    Public Function GetMcuCRSWSDBHeader() As CRSWS.DBSOAPHeader

        Dim objMcuCRSwsDBHeader As New CRSWS.DBSOAPHeader
        objMcuCRSwsDBHeader.User = gobjMcuDBHeader.UserID
        objMcuCRSwsDBHeader.Project = gobjMcuDBHeader.ProjectAlias
        objMcuCRSwsDBHeader.Env = gobjMcuDBHeader.EnvironmentUse
        objMcuCRSwsDBHeader.Comp = gobjMcuDBHeader.CompanyID
        objMcuCRSwsDBHeader.ConnectionAlias = gobjMcuDBHeader.CompanyID & "CIW" & gobjMcuDBHeader.EnvironmentUse
        objMcuCRSwsDBHeader.UserType = gobjMcuDBHeader.UserType
        Return objMcuCRSwsDBHeader

    End Function

    Private Function GetEnquiryBO() As DataTable
        Dim bo As New BOSchema.PolicyValue()
        Dim dt As DataTable = bo.GetPolicyValueEnqSendSchema()
        Return bo.SchemaToDataTable(dt)
    End Function

    'oliver 2023-12-12 updated for Switch Over Code from Assurance to Bermuda
    Public Function AddPolicyNotes(ByVal CompanyID As String, ByVal dtPolicyNote As DataTable, ByRef ErrMsg As String)

        Dim DataAdaptor As New ComCtl.PolicyNoteDefaultAdaptor
        Dim f As New ComCtl.PolicyNoteFilter
        Dim dsData As New DataSet

        Try
            f.PolicyNo = "dummy"
            f.EntryDateFrom = Today
            f.EntryDateTo = Today
            If CompanyID = "ING" Then DataAdaptor.ComHeader = gobjDBHeader
            If CompanyID = "MCU" Then DataAdaptor.ComHeader = gobjMcuDBHeader

            dsData = DataAdaptor.GetPolicyNote(f)

            With dtPolicyNote.Rows(0)
                DataAdaptor.AddNewRecord(dsData, .Item("PolicyNo"), Val(.Item("CiwNo")), .Item("Type"),
                    .Item("subType"), .Item("UserID"), .Item("EntryDate"), .Item("FollowUpUser"), IIf(IsDBNull(.Item("FollowupDate")), Nothing, .Item("FollowupDate")), .Item("Desc"),
                    .Item("TypeDesc"), .Item("subTypeDesc"), .Item("IsHICL"), .Item("HICLText"))
                Dim ds As DataSet = dsData.GetChanges()
                DataAdaptor.SavePolicyNote(ds)
            End With

        Catch ex As Exception
            ErrMsg = ex.ToString

        End Try

    End Function

    Public Function GetNBRPolicy(ByRef strInPolicy As String, ByRef strErr As String) As Boolean
        ' ES009 begin

        Dim strSQL As String
        Dim dtResult As DataTable

        GetNBRPolicy = True

        strSQL = "select * from nbrvw_uw_worksheet " &
            " where nbruwk_new_policy_no = '" & strInPolicy & "' and len(isnull(nbruwk_uw_comments,''))>0 " '

        If GetDT(strSQL, strCIWConn, dtResult, strErr) Then
            If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                GetNBRPolicy = False
            End If
        End If
        ' ES009 end

    End Function


    Public Function GetNBRPolicyMcu(ByRef strInPolicy As String, ByRef strErr As String) As Boolean
        ' ES009 begin

        Dim strSQL As String
        Dim dtResult As DataTable

        GetNBRPolicyMcu = True

        strSQL = "select * from nbrvw_uw_worksheet " &
            " where nbruwk_new_policy_no = '" & strInPolicy & "' and len(isnull(nbruwk_uw_comments,''))>0 " '

        If GetDT(strSQL, strCIWMcuConn, dtResult, strErr) Then
            If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                GetNBRPolicyMcu = False
            End If
        End If
        ' ES009 end

    End Function

    Public Function GetBusinessDate(ByRef BusDate As Date) As Boolean
        Dim objDbSoapHeader As New NBAWS.ComSOAPHeader
        Dim objNBAWS As New NBAWS.NBAWS
        Dim strErr As String
        Dim dsMain As DataSet

        Try
            Dim cfgPath As String = System.Configuration.ConfigurationManager.AppSettings("ConfigFilePath")

            objDbSoapHeader.ProjectAlias = gobjDBHeader.ProjectAlias
            objDbSoapHeader.MachineID = gobjDBHeader.MachineID
            objDbSoapHeader.UserID = gobjDBHeader.UserID
            objDbSoapHeader.UserType = gobjDBHeader.UserType
            objDbSoapHeader.VersionNo = gobjDBHeader.VersionNo
            objDbSoapHeader.CompanyID = gobjDBHeader.CompanyID

            'oliver 2023-11-30 added for Switch Over Code from Assurance to Bermuda
            'objDbSoapHeader.EnvironmentUse = IIf(cfgPath.Contains("Configuration.T107.xml"), "T107", gobjDBHeader.EnvironmentUse)
            If IsAssurance Then
                objDbSoapHeader.EnvironmentUse = IIf(cfgPath.Contains("Configuration.T107.xml"), "T107", gobjDBHeader.EnvironmentUse)
            Else
                objDbSoapHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse
            End If

            'AC - Change not to use Advance Compilation Option
            '#If UAT = 0 Then
            '            objNBAWS.Url = "http://hkcomprd2/nbaws/nbaws.asmx"
            '#Else
            '            objNBAWS.Url = "http://hkcomdev2/ingsit02nbaws/nbaws.asmx"
            '#End If
            If gUAT = False Then
                objNBAWS.Url = "http://hkcomprd2/nbaws/nbaws.asmx"
            Else
                objNBAWS.Url = Utility.Utility.GetWebServiceURL("NBAWS", gobjDBHeader, gobjMQQueHeader)
            End If
            'AC - Change not to use Advance Compilation Option


            objNBAWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objNBAWS.ComSOAPHeaderValue = objDbSoapHeader


            If objNBAWS.GetSysBusinessDate(dsMain, strErr) = True Then
                If dsMain.Tables.Count > 0 AndAlso dsMain.Tables(0).Rows.Count > 0 Then
                    BusDate = dsMain.Tables(0).Rows(0).Item("BusinessDate")
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            objNBAWS.Dispose()

        End Try
    End Function
    ' Flora Leung, Project Leo Goal 3 Capsil, 14-Feb-2012 Start
    ''' <summary>
    ''' Get CodeTable value of key
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    ''' <returns>Value</returns>
    Public Function GetCodeTableValue(ByVal strCode As String, Optional companyName As String = "") As String
        companyName = If(String.IsNullOrEmpty(companyName), g_Comp, companyName)
        Return APIServiceBL.QueryScalar(Of String)(companyName, "FRM_CODE_TABLE_VAL", New Dictionary(Of String, String)() From {
            {"strCode", strCode}
            }, "")
    End Function
    ' Flora Leung, Project Leo Goal 3 Capsil, 14-Feb-2012 End

    ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
    ''CRS 7x24 Changes - Start
    'Public ExternalUser As Boolean = False

    Public Function MaskExternalUserData(ByVal type As MaskData, ByVal strVal As String) As String

        If strVal.Trim.Length = 0 Then
            MaskExternalUserData = String.Empty
            Exit Function
        End If
        strVal = strVal.Trim
        Select Case type

            Case MaskData.HKID
                strVal = "*" + strVal.Remove(0, 1)
                Return strVal.Remove(strVal.Length - 4, 4) + "****"

            Case MaskData.CREDIT_CARAD_NO, MaskData.BANK_ACCOUNT_NO
                Return strVal.Remove(strVal.Length - 4, 4) + "****"

        End Select

    End Function

    Public Enum MaskData
        BANK_ACCOUNT_NO = 0
        CREDIT_CARAD_NO = 1
        HKID = 2
    End Enum
    Public Function MaskDTsource(ByRef dtInput As DataTable, ByVal strColumnName As String, ByVal type As MaskData,
                    Optional ByVal strParentTableName As String = "",
                    Optional ByVal strParentColumnName As String = "",
                    Optional ByVal strChildTableName As String = "",
                    Optional ByVal strChildcolumnName As String = "") As DataTable

        For row As Integer = 0 To dtInput.Rows.Count - 1

            For col As Integer = 0 To dtInput.Columns.Count - 1
                If strColumnName.Trim.ToUpper = dtInput.Columns(col).ColumnName().Trim.ToUpper And
                       (Not String.IsNullOrEmpty(Convert.ToString(dtInput.Rows(row)(col)))) Then
                    dtInput.Rows(row)(col) = MaskExternalUserData(type, dtInput.Rows(row)(col))
                End If
            Next
        Next

        'Mask Parent DataSet
        For Each parentrel As DataRelation In dtInput.ParentRelations
            For Each dtparent As DataTable In parentrel.DataSet.Tables
                If dtparent.TableName.Trim.ToUpper = strParentTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtparent.Rows.Count - 1
                        For col As Integer = 0 To dtparent.Columns.Count - 1

                            If strParentColumnName.Trim.ToUpper = dtparent.Columns(col).ColumnName().Trim.ToUpper And
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtparent.Rows(row)(col)))) Then
                                dtparent.Rows(row)(col) = MaskExternalUserData(type, dtparent.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        'Mask Child DataSet
        For Each childrel As DataRelation In dtInput.ChildRelations
            For Each dtchild As DataTable In childrel.DataSet.Tables
                If dtchild.TableName.Trim.ToUpper = strChildTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtchild.Rows.Count - 1
                        For col As Integer = 0 To dtchild.Columns.Count - 1

                            If strChildcolumnName.Trim.ToUpper = dtchild.Columns(col).ColumnName().Trim.ToUpper And
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtchild.Rows(row)(col)))) Then
                                dtchild.Rows(row)(col) = MaskExternalUserData(type, dtchild.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        Return dtInput

    End Function

    ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
    '''' <summary>
    '''' Check CRS External User by user name 
    '''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    '''' </summary>
    'Public Function CheckCRSExternalUser(ByVal pstrUserName As String, ByRef IsExtranalUser As Boolean, Optional companyName As String = "") As Boolean

    '    Dim strTmp() As String
    '    companyName = If(String.IsNullOrEmpty(companyName), g_Comp, companyName)

    '    Try
    '        'sqlCmd = New SqlCommand("Select cswsyv_field_name,cswsyv_value,cswsyv_desc,cswsyv_value_chi From csw_system_value where cswsyv_field_name = 'CRS_EX_USR'", sqlConn)

    '        Dim dr As DataRow = APIServiceBL.QueryFirstRow(companyName, "FRM_SYSTEM_VALUE", New Dictionary(Of String, String)() From {
    '         {"fieldName", "CRS_EX_USR"}
    '         })
    '        If Not IsNothing(dr) Then
    '            If Not IsDBNull(dr("cswsyv_value")) Then
    '                strTmp = Convert.ToString(dr("cswsyv_value")).Split(",")

    '                For Each str As String In strTmp
    '                    If str.Substring(0, 3).ToUpper().Trim = pstrUserName.Substring(0, 3).ToUpper().Trim Then
    '                        IsExtranalUser = True
    '                        CheckCRSExternalUser = True
    '                        Exit For
    '                    Else
    '                        CheckCRSExternalUser = False
    '                    End If
    '                Next
    '            Else
    '                CheckCRSExternalUser = False
    '            End If
    '        End If
    '    Catch sqlEx As SqlException
    '        MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
    '    Catch ex As Exception
    '        MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
    '    End Try
    '    Return CheckCRSExternalUser
    'End Function
    ''CRS 7x24 Changes - End


    ''' <summary>
    ''' Add by ITDSCH on 2016-12-15
    ''' </summary>
    ''' <param name="creditCardNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MaskedCreditCard(ByVal creditCardNumber As String) As String
        If creditCardNumber IsNot Nothing Then
            creditCardNumber = creditCardNumber.Trim
            Dim len As Integer = creditCardNumber.Length
            If len > 4 Then
                Dim i As Integer = 0
                Dim number As String = ""
                For i = 0 To len - 5
                    number = number + "X"
                Next
                number = number + creditCardNumber.Substring(len - 4, 4)
                Return number
            End If
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Add by ITDSCH on 2016-12-15
    ''' </summary>
    ''' <param name="dtInput"></param>
    ''' <param name="strColumnName"></param>
    ''' <param name="type"></param>
    ''' <param name="strParentTableName"></param>
    ''' <param name="strParentColumnName"></param>
    ''' <param name="strChildTableName"></param>
    ''' <param name="strChildcolumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaskDTCreditCard(ByRef dtInput As DataTable, ByVal strColumnName As String, ByVal type As MaskData,
                    Optional ByVal strParentTableName As String = "",
                    Optional ByVal strParentColumnName As String = "",
                    Optional ByVal strChildTableName As String = "",
                    Optional ByVal strChildcolumnName As String = "") As DataTable

        For row As Integer = 0 To dtInput.Rows.Count - 1

            For col As Integer = 0 To dtInput.Columns.Count - 1
                If strColumnName.Trim.ToUpper = dtInput.Columns(col).ColumnName().Trim.ToUpper And
                       (Not String.IsNullOrEmpty(Convert.ToString(dtInput.Rows(row)(col)))) Then
                    dtInput.Rows(row)(col) = MaskedCreditCard(dtInput.Rows(row)(col))
                End If
            Next
        Next

        'Mask Parent DataSet
        For Each parentrel As DataRelation In dtInput.ParentRelations
            For Each dtparent As DataTable In parentrel.DataSet.Tables
                If dtparent.TableName.Trim.ToUpper = strParentTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtparent.Rows.Count - 1
                        For col As Integer = 0 To dtparent.Columns.Count - 1

                            If strParentColumnName.Trim.ToUpper = dtparent.Columns(col).ColumnName().Trim.ToUpper And
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtparent.Rows(row)(col)))) Then
                                dtparent.Rows(row)(col) = MaskedCreditCard(dtparent.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        'Mask Child DataSet
        For Each childrel As DataRelation In dtInput.ChildRelations
            For Each dtchild As DataTable In childrel.DataSet.Tables
                If dtchild.TableName.Trim.ToUpper = strChildTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtchild.Rows.Count - 1
                        For col As Integer = 0 To dtchild.Columns.Count - 1

                            If strChildcolumnName.Trim.ToUpper = dtchild.Columns(col).ColumnName().Trim.ToUpper And
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtchild.Rows(row)(col)))) Then
                                dtchild.Rows(row)(col) = MaskedCreditCard(dtchild.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        Return dtInput

    End Function


    'Send iLAS SMS to customer 2015-03-09
    Public Function SendiLASSMS(ByVal PstrUser As String, ByVal PstrCustID As String, ByVal PstrLangFlag As String, ByVal PStrPhoneMobile As String, ByVal PdtEffDate As Date,
                               ByVal PdtExpDate As Date, ByVal PstrMsgCat As String, ByVal PstrMsg As String, ByRef PstrRefNo As String,
                               ByVal PstrPolNo As String, ByRef PstrErrMsg As String, Optional ByVal bMc As Boolean = False) As Integer

        Dim objWSPos As POSWS.POSWS

        Try
            objWSPos = New POSWS.POSWS


            If gUAT = False Then
                objWSPos.Url = "http://hkcomprd2/posws/posws.asmx"
            Else
                objWSPos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjDBHeader, gobjMQQueHeader)
                'objWSPos.Url = "http://hkcomdev2/posws/posws.asmx"
            End If

            'objWSPos.MQSOAPHeaderValue = GetPOSWSMQHeader()
            'objWSPos.DBSOAPHeaderValue = GetPOSWSDBHeader()

            If bMc Then
                Dim objPowsMQHeader As New POSWS.MQSOAPHeader
                objPowsMQHeader.QueueManager = gobjMQQueHeader.QueueManager
                objPowsMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
                objPowsMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
                objPowsMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
                objPowsMQHeader.Timeout = 300000

                objPowsMQHeader.ProjectAlias = gobjMQQueHeader.ProjectAlias
                objPowsMQHeader.ConnectionAlias = gobjMQQueHeader.ConnectionAlias
                objPowsMQHeader.UserType = gobjMQQueHeader.UserType
                objPowsMQHeader.EnvironmentUse = gobjMQQueHeader.EnvironmentUse
                objPowsMQHeader.CompanyID = "MCU" 'gobjMQQueHeader.CompanyID
                objPowsMQHeader.UserID = gobjMQQueHeader.UserID

                objWSPos.MQSOAPHeaderValue = objPowsMQHeader

                Dim objPowsDBHeader As New POSWS.DBSOAPHeader
                objPowsDBHeader.User = gobjDBHeader.UserID
                objPowsDBHeader.Project = gobjDBHeader.ProjectAlias
                objPowsDBHeader.Env = gobjDBHeader.EnvironmentUse
                objPowsDBHeader.Comp = "MCU" 'gobjDBHeader.CompanyID
                objPowsDBHeader.ConnectionAlias = "MCU" & "POS" & gobjDBHeader.EnvironmentUse 'gobjDBHeader.CompanyID & "POS" & gobjDBHeader.EnvironmentUse
                objPowsDBHeader.UserType = gobjDBHeader.UserType

                objWSPos.DBSOAPHeaderValue = objPowsDBHeader
            Else
                objWSPos.MQSOAPHeaderValue = GetPOSWSMQHeader()
                objWSPos.DBSOAPHeaderValue = GetPOSWSDBHeader()
            End If

            objWSPos.Credentials = System.Net.CredentialCache.DefaultCredentials
            objWSPos.Timeout = 10000000

            Return objWSPos.SendSMS2Customer(PstrUser, PstrCustID, PstrLangFlag, PStrPhoneMobile, PdtEffDate, PdtExpDate, PstrMsgCat, PstrMsg, PstrRefNo, PstrPolNo, PstrErrMsg)

        Catch ex As Exception
            If Not IsNothing(objWSPos) Then
                objWSPos.Dispose()
            End If
        End Try
    End Function
    'Send iLAS SMS to customer end

    'ITDYKT Common Open Policy Method
    'oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda
    'Public Sub ShowPolicy(ByVal strPolicy As String, Optional ByVal CompanyID As String = "ING", Optional ByVal blnIsUHNWCustomer As Boolean = False, Optional ByVal strCustomerID As String = "", Optional ByVal strRelateCode As String = "")
    Public Sub ShowPolicy_Assurance(ByVal strPolicy As String, Optional ByVal CompanyID As String = "ING", Optional ByVal blnIsUHNWCustomer As Boolean = False, Optional ByVal strCustomerID As String = "", Optional ByVal strRelateCode As String = "")
        'Check is GI

        Dim blnGI As Boolean
        Dim lngErr As Long
        Dim strErr As String
        Dim lngCnt As Long = gSearchLimit
        Dim sqldt As DataTable = New DataTable
        Dim GICustomerID As String
        Dim GIAccountStatus As String


        blnGI = False   ' ES01
        lngErr = 0
        strErr = ""
        Dim SingleQuoteFormatted = String.Format("'{0}%'", Trim(strPolicy))

        ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda
        'sqldt = GetPolicyListCIW("", "'" & strPolicy & "%'", "PH", "POLST", lngErr, strErr, lngCnt, True, CompanyID)
        If GetPolicyListByAPI(getCompanyCode(CompanyID), SingleQuoteFormatted, "", "PH", "POLST", "like", lngErr, strErr, lngCnt, sqldt, True) Then
            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        'oliver 2024-8-6 added for Com 6
        If sqldt.Rows.Count > 0 AndAlso sqldt.Columns.Contains("CompanyID") AndAlso sqldt.Rows(0).Item("CompanyID") = "BMU" Then
            CompanyID = "BMU"
            If Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                Exit Sub
            End If
        End If

        'check VVIP policy
        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(CompanyID), "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {{"PolicyNo", Trim(strPolicy)}})
        If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
            If isUHNWMember Then
                MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            Else
                MsgBox("VVVIP -Ultra High Net Worth customer. You're not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                Exit Sub
            End If
        End If

        If lngCnt = 0 Then

            ' ES01
            ' Search GI policy directly from CIW
            lngErr = 0
            strErr = ""
            lngCnt = gSearchLimit

            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda
            'sqldt = GetGIPolicyList("", "'" & strPolicy & "%'", "", "POLST", lngErr, strErr, lngCnt, True, CompanyID)
            sqldt = GetGIPolicyListAsur("", "'" & strPolicy & "%'", "", "POLST", lngErr, strErr, lngCnt, True, CompanyID)

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If

            If lngCnt = 0 Then
                MsgBox("No Matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Show Policy(Common)")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            Else
                blnGI = True
                GICustomerID = sqldt.Rows(0)("CustomerID") & ""
                GIAccountStatus = sqldt.Rows(0)("AccountStatus") & ""
            End If

        End If

        OpenPolicySummary(strPolicy, CompanyID, blnIsUHNWCustomer, strCustomerID, strRelateCode)

    End Sub

    Public Sub OpenPolicySummary(ByVal strPolicy As String, Optional ByVal CompanyID As String = "ING", Optional ByVal blnIsUHNWCustomer As Boolean = False, Optional ByVal strCustomerID As String = "", Optional ByVal strRelateCode As String = "")
        wndMain.Cursor = Cursors.AppStarting

        ' CRS performer slowness - Use the unified portal to open policy summary screen
        Dim policyBL As New PolicySearchBL
        policyBL.ShowPolicyAsur(strPolicy, CompanyID, blnIsUHNWCustomer, strCustomerID, strRelateCode)

        wndMain.Cursor = Cursors.Default
    End Sub

    Public Function GetMQHeader(ByVal companyID As String, ByRef mqHeader As Utility.Utility.MQHeader, ByRef dbHeader As Utility.Utility.ComHeader, ByRef posdbHeader As Utility.Utility.POSHeader)
        Dim Company As Company = [Enum].Parse(GetType(Company), companyID)

        Select Case Company
            Case Company.ING
                mqHeader = gobjMQQueHeader
                dbHeader = gobjDBHeader
                posdbHeader = gobjPOSHeader
            'oliver 2024-8-6 added for Com 6
            Case Company.BMU
                mqHeader = gobjBmuMQQueHeader
                dbHeader = gobjBmuDBHeader
                posdbHeader = gobjBmuPOSHeader
            Case Company.MCU
                mqHeader = gobjMcuMQQueHeader
                dbHeader = gobjMcuDBHeader
                posdbHeader = gobjMcuPOSHeader
            Case Company.LAC
                mqHeader = gobjLacMQQueHeader
                dbHeader = gobjLacDBHeader
                posdbHeader = gobjLacPOSHeader
            Case Company.LAH
                mqHeader = gobjLahMQQueHeader
                dbHeader = gobjLahDBHeader
                posdbHeader = gobjLahPOSHeader
        End Select
    End Function


    'ITDTCO : Common Open the Policy Method for Macau
    Public Sub ShowPolicyMcu(ByVal strPolicy As String, Optional ByVal blnIsUHNWCustomer As Boolean = False, Optional ByVal strCustomerID As String = "", Optional ByVal strRelateCode As String = "")
        'Check is GI

        Dim blnGI As Boolean
        Dim lngErr As Long
        Dim strErr As String
        Dim lngCnt As Long
        Dim sqldt As DataTable
        Dim GICustomerID As String
        Dim GIAccountStatus As String

        blnGI = False   ' ES01
        lngErr = 0
        strErr = ""

        Dim SingleQuoteFormatted = String.Format("'{0}%'", Trim(strPolicy))
        sqldt = New DataTable
        If GetPolicyListByAPI(getCompanyCode(g_McuComp), SingleQuoteFormatted, "", "PH", "POLST", "like", lngErr, strErr, lngCnt, sqldt, True) Then
            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        If lngCnt = 0 Then

            ' ES01
            ' Search GI policy directly from CIW
            lngErr = 0
            strErr = ""
            lngCnt = gSearchLimit

            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda
            'sqldt = GetGIPolicyMcuList("", "'" & strPolicy & "'", "", "POLST", lngErr, strErr, lngCnt, True)
            sqldt = GetGIPolicyMcuList(g_McuComp, "", "'" & strPolicy & "'", "", "POLST", lngErr, strErr, lngCnt)

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If

            If lngCnt = 0 Then
                MsgBox("No Matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Show Policy(Common)")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            Else
                blnGI = True
                GICustomerID = sqldt.Rows(0)("CustomerID") & ""
                GIAccountStatus = sqldt.Rows(0)("AccountStatus") & ""
            End If

        End If

        wndMain.Cursor = Cursors.AppStarting

        ' CRS performer slowness - Use the unified portal to open policy summary screen
        Dim policyBL As New PolicySearchBL
        policyBL.ShowPolicyMcu(strPolicy, g_McuComp, blnIsUHNWCustomer, strCustomerID, strRelateCode)

        wndMain.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Get GI Policy information from mcu database.
    ''' Lubin 2022-12-01 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    ''' <returns>datatable result.</returns>
    Public Function GetGIPolicyMcuList(companyName As String, ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer) As DataTable

        Dim retDt As DataTable
        Try
            ' Policy List (by policy)
            If Not String.IsNullOrEmpty(strPolicy) Then
                retDt = APIServiceBL.QueryFirstTable(companyName, "FRM_SEARCH_POLICY",
                                                                New Dictionary(Of String, String)() From {
                    {"TOP_ROW", intCnt},
                    {"PolicyNo", Trim(strPolicy).TrimEnd("%")}
                 }, False)
            ElseIf Not String.IsNullOrEmpty(strCustID) Then
                retDt = APIServiceBL.QueryFirstTable(companyName, "FRM_SEARCH_POLICY_CUST",
                                                                 New Dictionary(Of String, String)() From {
                     {"TOP_ROW", intCnt},
                     {"strCustID", Trim(strCustID)}
                  }, False)
            End If
            intCnt = retDt.Rows.Count

        Catch sqlex As SqlException
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & ex.ToString
            Return Nothing
        End Try

        Return retDt

    End Function

    Public Function GetCIWCustInfo(ByVal CustID As String, ByVal strConn As String) As System.Data.DataTable

        Dim sqlConn As New SqlConnection(strConn)
        Dim sSql As String
        Dim sqldt As New DataTable("CustomerAddress")
        Dim sqlDA As New SqlDataAdapter

        Try
            sSql = "select top 1 c.nameprefix, c.firstname, c.NameSuffix, isnull(c.ChiLstNm,'') + isnull(c.ChiFstNm,'') as ChiName , c.DateOfBirth, " &
            "c.Gender,  c.MaritalStatusCode, c.SmokerFlag, c.LanguageCode , c.GovernmentIDCard , '0' as OptOutFlag , c.CoName , " &
            "c.CoCName , c.EmailAddr , c.UseChiInd, ca.AddressTypeCode, ca.AddressLine1, ca.AddressLine2, ca.AddressLine3, " &
            "ca.AddressCity, ca.AddressPostalCode, ca.PhoneNumber1, ca.PhoneNumber2, ca.FaxNumber1, ca.BadAddress, " &
            "ca.CountryCode, ca.CustomerID, c.agentcode as clientid, c.CustomerStatusCode, cs.CustomerStatus,  c.CustomerTypeCode, ct.CustomerType, c.AgentCode, " &
            "c.PassportNumber, c.PhoneMobile, c.PhonePager, c.BirthPlace, '0' as OptOutOtherFlag, " &
            "c.PhonePager, c.occupName as occupation, c.occupcode , ca.FaxNumber2 " &
            "from customer c left outer join customeraddress ca on c.CustomerID=ca.CustomerID " &
            "left outer join CustomerTypeCodes ct on c.CustomerTypeCode=ct.CustomerTypeCode " &
            "left outer join CustomerStatusCodes cs on c.CustomerStatusCode=cs.CustomerStatusCode " &
            "where c.CustomerID='" & CustID & "'"

            sqlConn.Open()
            sqlDA = New SqlDataAdapter(sSql, sqlConn)
            sqlDA.Fill(sqldt)

        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        Finally
            sqlConn.Close()
        End Try
        Return sqldt

    End Function


    Public Function GetCIWMcuCustInfo(ByVal CustID As String) As System.Data.DataTable


        Dim sqlConn As New SqlConnection(strCIWMcuConn)
        Dim sSql As String
        Dim sqldt As New DataTable("CustomerAddress")
        Dim sqlDA As New SqlDataAdapter

        Try
            sSql = "select top 1 c.nameprefix, c.firstname, c.NameSuffix, isnull(c.ChiLstNm,'') + isnull(c.ChiFstNm,'') as ChiName , c.DateOfBirth, " &
            "c.Gender,  c.MaritalStatusCode, c.SmokerFlag, c.LanguageCode , c.GovernmentIDCard , '0' as OptOutFlag , c.CoName , " &
            "c.CoCName , c.EmailAddr , c.UseChiInd, ca.AddressTypeCode, ca.AddressLine1, ca.AddressLine2, ca.AddressLine3, " &
            "ca.AddressCity, ca.AddressPostalCode, ca.PhoneNumber1, ca.PhoneNumber2, ca.FaxNumber1, ca.BadAddress, " &
            "ca.CountryCode, ca.CustomerID, c.agentcode as clientid, c.CustomerStatusCode, cs.CustomerStatus,  c.CustomerTypeCode, ct.CustomerType, c.AgentCode, " &
            "c.PassportNumber, c.PhoneMobile, c.PhonePager, c.BirthPlace, '0' as OptOutOtherFlag, " &
            "c.PhonePager, c.occupName as occupation, c.occupcode , ca.FaxNumber2 " &
            "from customer c left outer join customeraddress ca on c.CustomerID=ca.CustomerID " &
            "left outer join CustomerTypeCodes ct on c.CustomerTypeCode=ct.CustomerTypeCode " &
            "left outer join CustomerStatusCodes cs on c.CustomerStatusCode=cs.CustomerStatusCode " &
            "where c.CustomerID='" & CustID & "'"

            sqlConn.Open()
            sqlDA = New SqlDataAdapter(sSql, sqlConn)
            sqlDA.Fill(sqldt)

        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        Finally
            sqlConn.Close()
        End Try
        Return sqldt

    End Function

    ''' <summary>
    ''' Phase 3 Point A-1(CRS Enhancement)
    ''' Get CustomerList by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-8-22 Added for Add new field “EB reference no.” and “Employee code” in search customer page</br>
    ''' <br>20250112 Chrysan Cheng, HNW Expansion - Integrated Customer Search</br>
    ''' </remarks>
    ''' <param name="lastName">Represents the LastName is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="firstName">Represents the FirstName is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="hkId">Represents the HKID is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="custId">Represents the CustID is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="agentNo">Represents the AgentNo is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="plateNumber">Represents the PlateNumber is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="strType">Represents the type of company that want to query</param>
    ''' <param name="errMsg">Represents the exception detail value</param>
    ''' <param name="mobile">Represents the Mobile is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="email">Represents the Email is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="ebRefNo">Represents the EBRefNo is part of the dictionary to get the CustomerListDataSet</param>
    ''' <param name="employeeCode">Represents the EmployeeCode is part of the dictionary to get the CustomerListDataSet</param>
    ''' <returns>The returned DataTable represents result of the SearchBusiAPI</returns>
    Public Function GetCustomerListByAPI(ByVal lastName As String, ByVal firstName As String, ByVal hkId As String,
        ByVal custId As String, ByVal agentNo As String, ByVal plateNumber As String, ByVal strType As String, ByRef errMsg As String,
        Optional mobile As String = "", Optional email As String = "", Optional ebRefNo As String = "", Optional employeeCode As String = "") As DataTable

        Dim custList As New DataTable("CustList")

        Try
            Dim dict As New Dictionary(Of String, String) From {
                {"CRLastName", IIf(lastName <> "", " and " & lastName, " ")},
                {"CRFirstName", IIf(firstName <> "", " and " & firstName, " ")},
                {"CRAgentNo", IIf(agentNo <> "", " and " & agentNo, " ")},
                {"CRHKID", IIf(hkId <> "", " and " & hkId, " ")},
                {"CRCustID", IIf(custId <> "", " and cus." & custId, " ")},
                {"CREBRefNo", IIf(ebRefNo <> "", " and " & ebRefNo, " ")},
                {"CREmployeeCode", IIf(employeeCode <> "", " and " & employeeCode, " ")},
                {"CRPlateNumber", IIf(plateNumber <> "", " and cus.CustomerID in (" & "select c.CustomerID from gi_vehicle a " &
                    "inner join csw_poli_rel b on a.PolicyAccountID=b.PolicyAccountID and b.PolicyRelateCode='PH' " &
                    "inner join customer c on b.CustomerID=c.CustomerID and " & plateNumber & ")", " ")},
                {"CRMobile", IIf(mobile <> "", " and " & mobile, " ")},
                {"CREmail", IIf(email <> "", " and " & email, " ")}
            }

            ' check criteria cannot be empty
            Dim dictIsContainsValue As Boolean = False
            For Each pair As KeyValuePair(Of String, String) In dict
                If pair.Value.Contains("and") Then
                    dictIsContainsValue = True
                    Exit For
                End If
            Next

            If Not dictIsContainsValue Then
                errMsg = "GetCustomerListByAPI - Invalid Criteria"
                Return custList
            End If


            ' call corresponding busi to get data
            Dim strComp As String = If(strType = "A", g_LacComp, If(strType = "M", g_McuComp, g_Comp))
            Dim strBusi As String = If(strType = "A", "GET_ASSURANCE_CUST_LIST", "GET_CUST_LIST")

            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(strComp, strBusi, dict)

            If retDs IsNot Nothing AndAlso retDs.Tables.Count > 0 Then
                custList = retDs.Tables(0).Copy()
            End If
        Catch ex As Exception
            errMsg = "GetCustomerListByAPI - " & ex.ToString
        End Try

        Return custList
    End Function



    Public Function checkPolicyEndDate(ByVal sPolID As String) As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""

        Try
            'As told by Lung, no need to check expiry date , 20160413

            'Dim sSQL As String = "select Top 1 PolicyAccountID from PolicyAccount where " & _
            '"(PolicyAccountID like '" & sPolID & "%') and datediff(year, PolicyAccount.PolicyEndDate, getdate()) <= 2 " & _
            '"and AccountStatusCode in ('1','2','3','4','V','X','Z') order by PolicyAccount.PolicyEndDate desc "

            Dim sSQL As String = "select Top 1 PolicyAccountID from PolicyAccount where " &
            "(PolicyAccountID like '" & sPolID & "%')  " &
            "and AccountStatusCode in ('1','2','3','4','V','X','Z') order by PolicyAccount.PolicyEndDate desc "

            If GetDT(sSQL, strCIWConn, dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    checkPolicyEndDate = dtResult.Rows(0)("PolicyAccountID")
                Else
                    checkPolicyEndDate = ""
                End If
            End If

            Return checkPolicyEndDate

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Public Function GLCheckPolicyEndDate(ByVal sPolID As String) As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""

        Try
            'As told by Lung, no need to check expiry date , 20160413

            'Dim sSQL As String = "select Top 1 PolicyAccountID from PolicyAccount where " & _
            '"(PolicyAccountID = '" & sPolID & "' or PolicyAccountID = '" & "GL" & sPolID & "') and datediff(year, PolicyAccount.PolicyEndDate, getdate()) <= 2 " & _
            '" order by PolicyAccount.PolicyEndDate desc "

            Dim sSQL As String = "select Top 1 PolicyAccountID=case when left(PolicyAccountID,2)='GL' then substring(PolicyAccountID,3,LEN(policyAccountId)) else PolicyAccountID end from PolicyAccount where " &
            "(PolicyAccountID = '" & sPolID & "' or PolicyAccountID = '" & "GL" & sPolID & "') and companyid in ('GL','LTD') " &
            " order by PolicyAccount.PolicyEndDate desc "

            If GetDT(sSQL, strCIWConn, dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    GLCheckPolicyEndDate = dtResult.Rows(0)("PolicyAccountID")
                Else
                    GLCheckPolicyEndDate = ""
                End If
            End If

            Return GLCheckPolicyEndDate

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Public Function GLGetPolicyID(ByVal sPolID As String) As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""

        Try

            Dim sSQL As String = "select Top 1 PolicyAccountID from PolicyAccount where " &
            "(PolicyAccountID = '" & sPolID & "' or PolicyAccountID = '" & "GL" & sPolID & "') and companyid in ('GL','LTD') " &
            " order by PolicyAccount.PolicyEndDate desc "

            If GetDT(sSQL, strCIWConn, dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    GLGetPolicyID = dtResult.Rows(0)("PolicyAccountID")
                Else
                    GLGetPolicyID = ""
                End If
            End If

            Return GLGetPolicyID

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    'Public Enum BrowserNavConstants
    '    navBrowserBar = 20
    'End Enum

    'Public Function BlockPolicy(ByVal strPolicy As String) As Boolean

    '    'Grace request not to show this list of policy, 20160429
    '    Dim doc As XmlDocument = New XmlDocument()
    '    doc.Load(AppDomain.CurrentDomain.BaseDirectory & "\BlockPolicy.xml")

    '    'Dim sBlockList(13) As String
    '    'sBlockList(0) = "500003367"
    '    'sBlockList(1) = "500003378"
    '    'sBlockList(2) = "500003379"
    '    'sBlockList(3) = "500005178"
    '    'sBlockList(4) = "500015500"
    '    'sBlockList(5) = "500015671"
    '    'sBlockList(6) = "500017346"
    '    'sBlockList(7) = "500017395"
    '    'sBlockList(8) = "500017777"
    '    'sBlockList(9) = "500017915"
    '    'sBlockList(10) = "500017920"
    '    'sBlockList(11) = "500017990"
    '    'sBlockList(12) = "5103348"

    '    BlockPolicy = True

    '    Dim xmlNode As XmlNode = doc.SelectSingleNode("//*[@id='" & strPolicy & "']")
    '    If Not xmlNode Is Nothing Then
    '        Return False
    '    Else
    '        Return True
    '    End If


    '    'For Each value As String In sBlockList
    '    '    If value = strPolicy Then
    '    '        BlockPolicy = False
    '    '        Exit For
    '    '    End If
    '    'Next



    'End Function


    Public Function GLSystemVersion() As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""

        Try
            'oliver 2024-7-11 added for Table_Relocate_Sprint 14
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
            Dim sSQL As String = "select value from " & serverPrefix & " codetable where code='CRSVer'"

            If GetDT(sSQL, strCIWConn, dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    GLSystemVersion = dtResult.Rows(0)("value")
                Else
                    GLSystemVersion = ""
                End If
            End If

            Return GLSystemVersion

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Public Function getCustomerID(ByVal sPolID As String) As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""
        Dim sRelatedCode As String = "PH" 'KT20170323
        'Dim sCompId As String = "" 'KT20170323

        Try

            'KT20170323 start
            'Dim sSQL2 As String = "select companyid from policyaccount where policyaccountID='{0}'"

            'sSQL2 = String.Format(sSQL2, sPolID)
            'If GetDT(sSQL2, strCIWConn, dtResult, strErr) Then
            '    If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
            '        sCompId = dtResult.Rows(0)("companyid")
            '    End If
            'End If

            'If sCompId = "EB" Or sCompId = "GL" Or sCompId = "LTD" Then
            '    sRelatedCode = "PI"
            'End If
            'KT20170323 end

            Dim sSQL As String = "select Top 1 CustomerID from csw_poli_rel where policyaccountID='{0}' and policyrelatecode='{1}'"

            sSQL = String.Format(sSQL, sPolID, sRelatedCode)
            If GetDT(sSQL, strCIWConn, dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    getCustomerID = dtResult.Rows(0)("CustomerID")
                Else
                    getCustomerID = ""
                End If
            End If

            Return getCustomerID

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function


    Public Function GIDetailPage(ByVal sPolicyNo As String) As Boolean

        GIDetailPage = False

        If sPolicyNo.Trim = String.Empty Then
            MsgBox("Please enter policy no.", MsgBoxStyle.Information)
            Exit Function
        End If

        'Dim aweb As New frmWebView

        Dim customerId As String = getCustomerID(sPolicyNo.Trim) 'KT20170210
        Dim objPolicyResponse As CRS_Util.clsJSONBusinessObj.clsPolicySearchResponse = CRS_Util.clsJSONTool.CallGIPolicyWS(sPolicyNo.Trim) 'KT20161111
        Dim url As String = String.Empty
        Dim strErr As String = String.Empty
        If objPolicyResponse IsNot Nothing AndAlso objPolicyResponse.groupType <> "" Then
            url = CRS_Util.clsJSONTool.GetGIDetailPageURL(customerId, sPolicyNo.Trim, objPolicyResponse.groupType.Trim)
        End If

        Try
            If Not OpenApplicationByIE(url, strErr) Then 'Old programme code have problem after desktop upgrade to IE, change to use the Open IE function originally for chatbot
                MsgBox(strErr)
                'GIDetailPage = False
                GIDetailPage = True 'Don't blcok the service log even cannot open GI page
            Else
                GIDetailPage = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            GIDetailPage = False
        End Try

    End Function

    Public Function EBDetailPage(ByVal sPolicyNo As String) As Boolean
        Dim sURL As String = String.Empty
        EBDetailPage = False

        If sPolicyNo.Trim = String.Empty Then
            MsgBox("Please enter policy no.", MsgBoxStyle.Information)
            Exit Function
        End If



        Try
            Dim customerId As String = ""
            Dim objPolicyResponse As CRS_Util.clsJSONBusinessObj.clsEBPolicySearchResponse = CRS_Util.clsJSONTool.CallEBPolicyWS(sPolicyNo.Trim) 'KT20161111

            If objPolicyResponse IsNot Nothing AndAlso objPolicyResponse.companyID.Trim <> "" Then
                'KT20170323 start
                If objPolicyResponse.companyID.Trim = "GL" Or objPolicyResponse.companyID.Trim = "LTD" Then
                    customerId = getCustomerID("GL" + sPolicyNo.Trim)
                Else
                    customerId = getCustomerID(sPolicyNo.Trim)
                End If
                'KT20170323 end

                Dim strErr As String = String.Empty

                sURL = CRS_Util.clsJSONTool.GetEBDetailPageURL(customerId, sPolicyNo.Trim, objPolicyResponse.companyID.Trim) 'KT20161111
                If Not OpenApplicationByIE(sURL, strErr) Then 'Old programme code have problem after desktop upgrade to IE, change to use the Open IE function originally for chatbot
                    MsgBox(strErr)
                    'EBDetailPage = False
                    EBDetailPage = True 'Don't blcok the service log even cannot open EB page
                Else
                    EBDetailPage = True
                End If
            Else
                MsgBox("Policy No. Not Found")
                EBDetailPage = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            EBDetailPage = False
        End Try

    End Function

    'ITDCIC open CRM search page
    Public Function CRMSearchPage(ByVal UserID As String) As Boolean

        CRMSearchPage = False

        'If IE Is Nothing Then
        '    CREATE_IE()
        'Else
        '    Try
        '        IE.Quit()
        '        IE.Stop()
        '        IE = Nothing
        '    Catch ex As Exception
        '        IE = Nothing
        '    End Try
        '    CREATE_IE()
        'End If
        'ITDCPI original code CRM page open by IE start
        'CREATE_IE()

        ' set IE name
        'IE.PutProperty("name", "CRM Search")
        'IE.Top = 100
        'IE.Left = 100
        'IE.Height = 800
        'IE.Width = 1000
        'IE.AddressBar = False
        'IE.StatusBar = False
        'IE.ToolBar = False
        'IE.MenuBar = False
        'IE.Navigate(System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWebURL") & "?LoginID=" & UserID)
        'IE.Visible = True

        'CRMSearchPage = True

        'ITDCPI original code CRM page open by IE end
        'ITDCPI new code CRM page open by Chrome start

        Dim strErr As String = String.Empty
        Dim url As String = System.Configuration.ConfigurationSettings.AppSettings.Item("OptinWebURL") & "?LoginID=" & UserID
        If Not OpenApplicationByChrome(url, strErr) Then
            MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            CRMSearchPage = False
        Else
            CRMSearchPage = True
        End If
        'ITDCPI new code CRM page open by Chrome end

    End Function
    'ITDCIC

    Public Sub StringCarriageReturnConvertEventHandler(ByVal sender As Object, ByVal e As ConvertEventArgs)

        If Not TypeOf e.Value Is String Then
            Return
        End If

        e.Value = DirectCast(e.Value, String).Replace(vbLf, vbNewLine)

    End Sub

    Public Sub CharToBooleanConvertEventHandler(ByVal sender As Object, ByVal e As ConvertEventArgs)

        If e.Value Is Nothing Then
            e.Value = False
        End If
        If e.Value Is DBNull.Value Then
            e.Value = False
        Else
            Select Case e.Value.ToString().ToUpper()
                Case "Y"
                    e.Value = True
                Case Else
                    e.Value = False
            End Select
        End If
    End Sub

    Public Sub BooleanToCharConvertEventHandler(ByVal sender As Object, ByVal e As ConvertEventArgs)

        If e.Value Is Nothing Then
            e.Value = "N"
        End If

        If e.Value Is DBNull.Value Then
            e.Value = "N"
        Else
            If DirectCast(e.Value, Boolean) Then
                e.Value = "Y"
            Else
                e.Value = "N"
            End If
        End If
    End Sub

    Public Function GetCodeTable(ByVal strCode As String) As DataTable

        Dim strSQL As String
        Dim dt As New DataTable()
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL = String.Format("select * from " & serverPrefix & "CodeTable where Code='{0}'", strCode)

        Using daSql As New SqlDataAdapter(strSQL, strCIWConn)
            daSql.Fill(dt)
        End Using

        Return dt

    End Function

    Private Function GetDBSOAPHeader_MCU() As POSWS.DBSOAPHeader

        Dim objPowsDBHeader As New POSWS.DBSOAPHeader

        'objPowsDBHeader.User = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER")
        'objPowsDBHeader.Project = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_PROJ_ALIAS")
        'objPowsDBHeader.Env = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_ENV")
        'objPowsDBHeader.Comp = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_COMP_NAME")
        'objPowsDBHeader.ConnectionAlias = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_CONN_ALIAS")
        'objPowsDBHeader.UserType = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER_TYPE")

        Return objPowsDBHeader

    End Function

    Private Function ComHeader_MCU() As Utility.Utility.ComHeader

        Dim objDBHeaderMCU As New Utility.Utility.ComHeader
        objDBHeaderMCU.UserID = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER")
        objDBHeaderMCU.ProjectAlias = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_PROJ_ALIAS")
        objDBHeaderMCU.EnvironmentUse = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_ENV")
        objDBHeaderMCU.CompanyID = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_COMP_NAME")
        objDBHeaderMCU.ConnectionAlias = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_CONN_ALIAS")
        objDBHeaderMCU.UserType = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER_TYPE")
        Return objDBHeaderMCU

    End Function

    Private Function MQHeader_MCU() As Utility.Utility.MQHeader

        Dim objMQHeaderMCU As New Utility.Utility.MQHeader
        objMQHeaderMCU.UserID = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER")
        objMQHeaderMCU.ProjectAlias = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_PROJ_ALIAS")
        objMQHeaderMCU.EnvironmentUse = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_ENV")
        objMQHeaderMCU.CompanyID = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_COMP_NAME")
        objMQHeaderMCU.ConnectionAlias = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_CONN_ALIAS")
        objMQHeaderMCU.UserType = System.Configuration.ConfigurationSettings.AppSettings.Item("MCU_USER_TYPE")
        Return objMQHeaderMCU

    End Function

    Public Function POSWS_HK() As POSWS.POSWS
        ' CRS performer slowness
        'Dim wspos As New POSWS.POSWS

        'If gUAT Then
        '    'wspos.Url = "http://localhost:20560/POSWebService/POSWS.asmx"
        '    wspos.Url = My.Settings.CS2005_POSWS_POSWS
        'Else
        '    wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjDBHeader, gobjMQQueHeader)
        'End If
        'wspos.Credentials = System.Net.CredentialCache.DefaultCredentials
        'wspos.Timeout = -1
        'wspos.MQSOAPHeaderValue = GetPOSWSMQHeader()
        'wspos.DBSOAPHeaderValue = GetPOSWSDBHeader()
        'Return wspos

        Dim wspos As POSWS.POSWS = PosServiceInst.GetService(g_Comp, GetPOSWSDBHeader(), GetPOSWSMQHeader())

        If gUAT Then
            'wspos.Url = "http://localhost:20560/POSWebService/POSWS.asmx"
            wspos.Url = My.Settings.CS2005_POSWS_POSWS
        Else
            wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", gobjDBHeader, gobjMQQueHeader)
        End If

        Return wspos
    End Function

    Public Function POSWS_MCU() As POSWS.POSWS
        ' CRS performer slowness
        'Dim wspos As New POSWS.POSWS

        'If gUAT Then
        '    'wspos.Url = "http://localhost:20560/POSWebService/POSWS.asmx"
        '    wspos.Url = My.Settings.CS2005_POSWS_POSWS
        'Else
        '    wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", ComHeader_MCU, MQHeader_MCU)
        'End If
        'wspos.Credentials = System.Net.CredentialCache.DefaultCredentials
        'wspos.Timeout = -1
        ''wspos.DBSOAPHeaderValue = GetDBSOAPHeader_MCU()
        'wspos.MQSOAPHeaderValue = GetMcuPOSWSMQHeader()
        'wspos.DBSOAPHeaderValue = GetMcuPOSWSDBHeader()
        'Return wspos

        Dim wspos As POSWS.POSWS = PosServiceInst.GetService(g_McuComp, GetMcuPOSWSDBHeader(), GetMcuPOSWSMQHeader())

        If gUAT Then
            'wspos.Url = "http://localhost:20560/POSWebService/POSWS.asmx"
            wspos.Url = My.Settings.CS2005_POSWS_POSWS
        Else
            wspos.Url = Utility.Utility.GetWebServiceURL("POSWS", ComHeader_MCU, MQHeader_MCU)
        End If

        Return wspos
    End Function

    Public Function MCSWS_HK() As MCSWS.MCSWS

        Dim wsmcs As New MCSWS.MCSWS

        If gUAT Then
            'wspos.Url = "http://localhost:48794/MCSWS.asmx"
            wsmcs.Url = My.Settings.CS2005_MCSWS_MCSWS
        Else
            wsmcs.Url = My.Settings.CS2005_MCSWS_MCSWS 'ITSR2408 HOTFIX - 20210913 utility MCSWS return wrong url
            'wsmcs.Url = Utility.Utility.GetWebServiceURL("MCSWS", gobjDBHeader, gobjMQQueHeader)
        End If
        wsmcs.Credentials = System.Net.CredentialCache.DefaultCredentials
        wsmcs.Timeout = -1
        wsmcs.MQSOAPHeaderValue = GetMCSWSMQHeader()
        wsmcs.DBSOAPHeaderValue = GetMCSWSDBHeader()
        Return wsmcs

    End Function

    Public Function ExcecuteQuery(ByVal strSQL As String, ByVal strConn As String, ByRef dtResult As DataTable, ByRef strErr As String) As Boolean

        Try
            Using connection As SqlConnection = New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(strSQL, connection)
                Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
                adapter.Fill(dtResult)

                connection.Close()
            End Using

        Catch ex As Exception
            strErr = ex.ToString()
            Return False
        End Try

        Return True
    End Function

    Public Function ExecNonQuery(ByVal strSQL As String, ByVal strConn As String, ByRef intRowsAffected As Integer, ByRef strErr As String) As Boolean

        Try
            Using connection As SqlConnection = New SqlConnection(strConn),
                command As SqlCommand = New SqlCommand(strSQL, connection)

                If (connection.State And ConnectionState.Open) <> ConnectionState.Open Then connection.Open()

                intRowsAffected = command.ExecuteNonQuery()

                connection.Close()
            End Using

        Catch ex As Exception
            strErr = ex.ToString()
            Return False
        End Try

        Return True
    End Function

    Public Sub EscapeSqlSingleQuote(ByVal dt As DataTable)
        For Each dr As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                If col.DataType Is GetType(String) AndAlso dr(col.ColumnName) IsNot DBNull.Value Then
                    dr(col.ColumnName) = dr(col.ColumnName).ToString().Replace("'", "''")
                End If
            Next
        Next
    End Sub

    'ITDCIC 7-11 & ISC claim payout
    Public Function get7ElevenPayoutDetails(ByVal companyCode As String, ByVal strPolicyNo As String, ByVal strMobileNo As String, ByRef dtPayout As DataTable) As Boolean
        dtPayout = New DataTable
        Try
            Dim queryDict As New Dictionary(Of String, String)
            If Not String.IsNullOrEmpty(strPolicyNo) Then queryDict.Add("PolicyID", strPolicyNo)
            If Not String.IsNullOrEmpty(strMobileNo) Then queryDict.Add("CustMobile", strMobileNo)

            Dim ds As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyCode), "GET_7ELEVEN_PAYOUT_DETAILS", queryDict)

            If ds.Tables.Count > 0 Then
                dtPayout = ds.Tables(0)
                Return True
            End If

            Return False
        Catch ex As Exception
            Dim strError As String = "CRSAPI GET_7ELEVEN_PAYOUT_DETAILS Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    'ITDCIC 7-11 & ISC claim payout
    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Macau 7-11 QR Code 
    ''' Get 7ElevenTranDetails by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code </br>
    ''' </remarks>
    ''' <param name="companyCode">Represents the CompanyCode is call to the CRS_API which DataBase ConnectString</param>
    ''' <param name="strPayoutId">Represents the PayoutId is part of the dictionary to get the 7ElevenTranDetails DataSet</param>
    ''' <param name="dtTran">Represents the CRS_API return related table</param>
    ''' <returns>Represents whether the CRS_API return related 7ElevenTranDetails table is successful</returns>
    Public Function Get7ElevenTranDetails(ByVal companyCode As String, ByVal strPayoutId As String, ByRef dtTran As DataTable) As Boolean
        Dim blnSuccess = False
        dtTran = New DataTable
        Dim ds As DataSet = New DataSet()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyCode), "GET_7ELEVEN_TRAN_DETAILS",
                                     New Dictionary(Of String, String) From {
                                    {"PayoutID", IIf(Not String.IsNullOrEmpty(strPayoutId), strPayoutId, "")}
                                    })
            If ds.Tables.Count > 0 Then
                dtTran = ds.Tables(0)
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_7ELEVEN_TRANDETAILS Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

        'Dim sqlconnect As New SqlConnection

        'Try
        '    Dim strSQL As String
        '    Dim sSQL As String = "SELECT DISTINCT TransID AS 'Trans ID', ShopID AS 'Shop ID' FROM PayoutTransaction WHERE PayoutID = '{0}'"

        '    strSQL = String.Format(sSQL, strPayoutId)

        '    sqlconnect.ConnectionString = strCIWConn
        '    sqlconnect.Open()
        '    Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlDA.Fill(dtTran)
        '    sqlconnect.Close()
        '    blnSuccess = True


        'Catch ex As Exception
        '    Dim strError As String
        '    strError = ex.ToString + " occurs in modGlobal get7ElevenTranDetails"
        '    MsgBox(strError)
        '    Return False
        'End Try

        'Return blnSuccess
    End Function

    'ITDCIC 7-11 & ISC claim payout
    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Macau 7-11 QR Code 
    ''' Get 7ElevenTranHistory by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code </br>
    ''' </remarks>
    ''' <param name="companyCode">Represents the CompanyCode is to call the CRS_API which DataBase ConnectString</param>
    ''' <param name="strPayoutId">Represents the PayoutId is part of the dictionary to get the 7ElevenTranHistoryDataSet</param>
    ''' <param name="strTranId">Represents the TranId is part of the dictionary to get the 7ElevenTranHistory DataSet</param>
    ''' <param name="dtTranHIstory">Represents the CRS_API return related table</param>
    ''' <returns>Represents whether the CRS_API return related 7ElevenTranHistory table is successful</returns>
    Public Function Get7ElevenTranHistory(ByVal companyCode As String, ByVal strPayoutId As String, ByVal strTranId As String, ByRef dtTranHIstory As DataTable) As Boolean
        dtTranHIstory = New DataTable
        Try
            Dim queryDict As New Dictionary(Of String, String)
            If Not String.IsNullOrEmpty(strPayoutId) Then queryDict.Add("PayoutID", strPayoutId)
            If Not String.IsNullOrEmpty(strTranId) Then queryDict.Add("TransID", strTranId)

            Dim ds As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyCode), "GET_7ELEVEN_TRAN_HISTORY", queryDict)

            If ds.Tables.Count > 0 Then
                dtTranHIstory = ds.Tables(0)
                Return True
            End If

            Return False
        Catch ex As Exception
            Dim strError As String = "CRSAPI GET_7ELEVEN_TRAN_HISTORY Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    'ITDCIC 7-11 & ISC claim payout
    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Macau 7-11 QR Code 
    ''' Update 7ElevenRemarks by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code </br>
    ''' </remarks>
    ''' <param name="companyCode">Represents the CompanyCode is to call the CRS_API which DataBase ConnectString</param>
    ''' <param name="strPayoutId">Represents the PayoutId is part of the dictionary to update the 7ElevenRemarks</param>
    ''' <returns>Represents whether the CRS_API update 7ElevenRemarks is successful</returns>
    Public Function Save7ElevenRemarks(ByVal companyCode As String, ByVal strPayoutID As String, ByVal strRemarks As String) As Boolean
        Dim blnSuccess = False

        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(companyCode), "UPDATE_7ELEVEN_PAYOUT_REMARK_BY_PAYOUTID",
                                New Dictionary(Of String, String) From {
                                {"Remark", strRemarks},
                                {"PayoutID", strPayoutID},
                                {"UpdateBy", gsUser}
                                })
            blnSuccess = True
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            blnSuccess = False
        End Try

        Return blnSuccess
        'Dim sqlconnect As New SqlConnection
        'Dim sqlcmd As New SqlCommand


        'Dim strSQL, strStatus As String
        'Dim sSQL As String = "UPDATE payout" &
        '                     " SET Remark = '{1}', UpdateDate = GETDATE(), UpdateBy = '" & gsUser & "'" &
        '                     " WHERE PayoutID = '{0}'"


        'strSQL = String.Format(sSQL, strPayoutID, strRemarks)

        'sqlconnect.ConnectionString = strCIWConn
        'sqlconnect.Open()

        'sqlcmd.CommandText = strSQL
        'sqlcmd.Connection = sqlconnect

        'Try
        '    strStatus = sqlcmd.ExecuteScalar()
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    blnSuccess = False
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    blnSuccess = False
        'Finally
        '    sqlconnect.Close()
        'End Try

        'If strStatus Is Nothing Then
        '    blnSuccess = True
        'End If

        'Return blnSuccess
    End Function

    'KT20171207
    Public Function OpenChatbotCIC(ByVal UserID As String, ByVal FullName As String) As Boolean

        OpenChatbotCIC = False

        'If IE Is Nothing Then
        '    CREATE_IE()
        'Else
        '    Try
        '        IE.Quit()
        '        IE.Stop()
        '        IE = Nothing
        '    Catch ex As Exception
        '        IE = Nothing
        '    End Try
        '    CREATE_IE()
        'End If

        CREATE_IE()

        ' set IE name
        IE.PutProperty("name", "Chatbot CIC")
        IE.Top = 100
        IE.Left = 100
        IE.Height = 800
        IE.Width = 1000
        IE.AddressBar = False
        IE.StatusBar = False
        IE.ToolBar = False
        IE.MenuBar = False
        IE.Navigate(System.Configuration.ConfigurationSettings.AppSettings.Item("ChatbotCICWebURL") & "?LoginID=" & UserID & "&username=" & FullName)
        IE.Visible = True

        OpenChatbotCIC = True



    End Function

    Public Function checkChatbotCICUserRight(ByVal UserID As String) As Boolean
        checkChatbotCICUserRight = False
        Dim grpName As String = String.Empty
        Dim sqlconnect As New SqlConnection
        Dim dtTemp As New DataTable

        Try
            Dim strSQL As String
            Dim sSQL As String = "select ug.upsugt_usr_grp_name from " & UPS_USER_LIST_TAB & " ul (nolock) inner join " & UPS_USER_GROUP_TAB & " ug (nolock) on ul.upsult_sys_abv=ug.upsugt_sys_abv and ul.upsult_usr_grp=ug.upsugt_usr_grp_no where ug.upsugt_sys_abv='CIC' and ul.upsult_usr_id='{0}'"

            strSQL = String.Format(sSQL, UserID)

            sqlconnect.ConnectionString = strUPSConn
            sqlconnect.Open()
            Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtTemp)
            sqlconnect.Close()
            If dtTemp.Rows.Count > 0 Then
                checkChatbotCICUserRight = True
            Else
                checkChatbotCICUserRight = False
            End If


        Catch ex As Exception
            Dim strError As String
            strError = ex.ToString + " occurs in modGlobal checkChatbotCICUserRight"
            MsgBox(strError)
            Return False
        End Try

        Return checkChatbotCICUserRight

    End Function

    Public Function getFullNameFromProfileDB(ByVal UserID As String) As String
        Dim fullname As String = String.Empty
        Dim sqlconnect As New SqlConnection
        Dim dtTemp As New DataTable

        Try
            Dim strSQL As String
            Dim sSQL As String = "select ufm_full_name from " & CIC_FULLNAME_MAPPING & " where ufm_user_id='{0}'"


            strSQL = String.Format(sSQL, UserID)

            sqlconnect.ConnectionString = strUPSConn
            sqlconnect.Open()
            Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtTemp)
            sqlconnect.Close()
            fullname = dtTemp.Rows(0)("ufm_full_name")




        Catch ex As Exception
            Dim strError As String
            strError = ex.ToString + " occurs in modGlobal getFullNameFromProfileDB"
            MsgBox(strError)
            Return False
        End Try

        Return fullname

    End Function
    'KT20171207

    'ITDCPI 2019 New Chatbot start

    'new access control for Icontek Chatbot item start
    'Public Function checkUserRight(ByVal UserID As String, ByRef dtUserGroup As DataTable) As Boolean
    '    Dim allowaccess As Boolean = False
    '    Dim sqlconnect As New SqlConnection
    '    Dim strSQL As String = String.Empty
    '    Try
    '        strSQL = "Select usr.upsult_usr_grp as [AccessItem] , usgp.upsugt_usr_grp_name as [AccessGrp] "
    '        strSQL += "from " & UPS_USER_LIST_TAB & " usr "
    '        strSQL += "inner join " & UPS_USER_GROUP_TAB & " usgp on usr.upsult_usr_grp=usgp.upsugt_usr_grp_no "
    '        strSQL += "where upsult_sys_abv='CSW' "
    '        strSQL += "and usr.upsult_usr_id='" + UserID + "'"

    '        sqlconnect.ConnectionString = strUPSConn
    '        sqlconnect.Open()
    '        Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
    '        sqlDA.Fill(dtUserGroup)
    '        sqlconnect.Close()
    '        If dtUserGroup.Rows.Count > 0 Then allowaccess = True
    '    Catch ex As Exception
    '        Dim strError As String
    '        strError = ex.ToString + " occurs in modGlobal checkUserGroup"
    '        MsgBox(strError)
    '        Return False
    '    End Try
    '    Return allowaccess
    'End Function

    'Use Chrome to open new CE Assist and Admin Console
    Public Function OpenApplicationByChrome(ByVal Url As String, ByRef strErr As String) As Boolean
        Dim path As String = "C:\Program Files (x86)\Google\Chrome\Application\" 'First try to open 32-bit version Chrome
        Dim chromeexe As String = "chrome.exe"
        Dim File As System.IO.File
        Dim chromepath As String = path + chromeexe
        strErr = String.Empty
        'Dim fs As System.IO.FileStream
        Try
            If File.Exists(chromepath) Then
                Process.Start(chromepath, Url)
                Return True
            Else
                path = "C:\Program Files\Google\Chrome\Application\" 'If no 64-bit, try 64-bit version
                chromepath = path + chromeexe
                If File.Exists(chromepath) Then
                    Process.Start(chromepath, Url)
                    Return True
                Else 'If still cannot open chrome, try IE
                    Dim strIEErr As String
                    If OpenApplicationByIE(Url, strIEErr) Then
                        Return True
                    Else
                        Throw New System.Exception(strIEErr)
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function

    'Use IE to open new CE Assist and Admin Console
    Public Function OpenApplicationByIE(ByVal Url As String, ByRef strIEErr As String) As Boolean
        Dim IE As SHDocVw.InternetExplorer
        Try
            If IE Is Nothing Then
                IE = CreateObject("InternetExplorer.Application")
            Else
                Try
                    IE.Quit()
                    IE.Stop()
                    IE = Nothing
                Catch ex As Exception
                    IE = Nothing
                End Try
                IE = CreateObject("InternetExplorer.Application")
            End If
            IE.PutProperty("name", "CRS Extension")
            IE.AddressBar = False
            IE.StatusBar = False
            IE.ToolBar = False
            IE.MenuBar = False
            IE.Navigate(Url)
            IE.Visible = True
            Return True
        Catch ex As Exception
            strIEErr = ex.Message
            Return False
        End Try
    End Function

    Public Function setEndpoint(ByVal Env As String) As String
        Dim endpoint As String
        If Env = "PRD01" Then
            endpoint = ConfigurationSettings.AppSettings.Item("icontekchatbot_prd")
        Else
            endpoint = ConfigurationSettings.AppSettings.Item("icontekchatbot_uat")
        End If
        Return endpoint
    End Function

    'Access CE Assistance (344=CS_Admin1 (CSR), 345=CS_Admin2 (CS Mgr))
    Public Function ChatbotCEAssistancePage(ByVal strUserID As String) As Boolean

        ChatbotCEAssistancePage = False
        Dim strToken As String = GetHashidToken(strUserID)
        Dim strErr As String = String.Empty
        Dim url As String = setEndpoint(g_Env) & "/ce-assist/SSO?user=" + strUserID + "&token=" + strToken
        If Not OpenApplicationByChrome(url, strErr) Then
            MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            ChatbotCEAssistancePage = False
        Else
            ChatbotCEAssistancePage = True
        End If

    End Function

    'Access Admin Consloe (344=CS_Admin1 (CSR), 345=CS_Admin2 (CS Mgr))
    Public Function ChatbotAdminconsole(ByVal strUserID As String) As Boolean

        ChatbotAdminconsole = False
        Dim strToken As String = GetHashidToken(strUserID)
        Dim strErr As String = String.Empty
        Dim url As String = setEndpoint(g_Env) & "/admin/SSO?user=" + strUserID + "&token=" + strToken
        If Not OpenApplicationByChrome(url, strErr) Then
            MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            ChatbotAdminconsole = False
        Else
            ChatbotAdminconsole = True
        End If

    End Function

    'Generate Access Token
    Private Function GetHashidToken(ByVal strUserID As String) As String
        Dim strHashidToken As String = String.Empty
        Dim unixtimestamp As Long = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        Dim ids As New Hashids("md^(djjfsdj/129SGFwwi128sdERT@#$" + strUserID, 8, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
        strHashidToken = ids.EncodeLong(unixtimestamp)
        Return strHashidToken
    End Function 'ITDCPI 2019 New Chatbot end

    'SL20191121 - Start
    Public Function GetQDAPRefundExtraPaymentAlert(ByVal companyID As String, ByVal strPolicyNo As String, ByVal strCustID As String, ByVal dtBusDate As DateTime, ByVal strErr As String) As Boolean
        Dim sqlConn As New SqlConnection

        Dim dt As New DataTable
        Dim strSQL As String = String.Empty

        strSQL += "select pol.PolicyAccountid, pol.RCD from PolicyAccount pol "
        strSQL += "inner join csw_poli_rel rel on rel.PolicyAccountID = pol.PolicyAccountID and rel.PolicyRelateCode ='PH' "
        strSQL += "where pol.ProductID in ('UQA1', 'UQA2') "
        strSQL += "and CONVERT(varchar, '" & dtBusDate.ToString("yyyy-MM-dd") & "', 112) < CONVERT(varchar, dateadd(year, 1, pol.RCD), 112)  "

        If strPolicyNo.Trim <> "" Then
            strSQL += "and pol.PolicyAccountid ='" + strPolicyNo + "' "
        End If

        If strCustID.Trim <> "" Then
            strSQL += "and rel.CustomerID = '" + strCustID + "'"
        End If

        Try
            Dim da As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.ConnectionString = GetConnectionStringByCompanyID(companyID)
            sqlConn.Open()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function


    Public Function GetMcuQDAPRefundExtraPaymentAlert(ByVal strPolicyNo As String, ByVal strCustID As String, ByVal dtBusDate As DateTime, ByVal strErr As String) As Boolean
        Dim sqlConn As New SqlConnection

        Dim dt As New DataTable
        Dim strSQL As String = String.Empty

        strSQL += "select pol.PolicyAccountid, pol.RCD from PolicyAccount pol "
        strSQL += "inner join csw_poli_rel rel on rel.PolicyAccountID = pol.PolicyAccountID and rel.PolicyRelateCode ='PH' "
        strSQL += "where pol.ProductID in ('UQA1', 'UQA2') "
        strSQL += "and CONVERT(varchar, '" & dtBusDate.ToString("yyyy-MM-dd") & "', 112) < CONVERT(varchar, dateadd(year, 1, pol.RCD), 112)  "

        If strPolicyNo.Trim <> "" Then
            strSQL += "and pol.PolicyAccountid ='" + strPolicyNo + "' "
        End If

        If strCustID.Trim <> "" Then
            strSQL += "and rel.CustomerID = '" + strCustID + "'"
        End If

        Try
            sqlConn.ConnectionString = strCIWMcuConn

            Dim da As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    'SL20191121 - End

#Region "ITSR933 FG R3"
    'ITSR933 FG R3 CE Start
    Public Function GetCapsilPolicyNo(ByVal strPolicy As String) As String
        Dim ret As String = ""

        Try
            Dim objCI As LifeClientInterfaceComponent.clsPOS
            objCI = New LifeClientInterfaceComponent.clsPOS()

            objCI.DBHeader = gobjDBHeader
            objCI.MQQueuesHeader = gobjMQQueHeader

            Dim strErr As String = ""
            If objCI.GetCapsilPolicyNo(strPolicy, ret, strErr) Then
                Return ret 'ITSR933 FG R5 PNC fix dual search by Gary Lei
            End If

        Catch ex As Exception

        End Try

        Return ret
    End Function




    Public Function GetMcuCapsilPolicyNo(ByVal strPolicy As String) As String
        Dim ret As String = ""

        Try
            Dim objCI As LifeClientInterfaceComponent.clsPOS
            objCI = New LifeClientInterfaceComponent.clsPOS()

            objCI.DBHeader = gobjMcuDBHeader
            objCI.MQQueuesHeader = gobjMcuMQQueHeader

            Dim strErr As String = ""
            If objCI.GetCapsilPolicyNo(strPolicy, ret, strErr) Then
                Return ret 'ITSR933 FG R5 PNC fix dual search by Gary Lei
            End If

        Catch ex As Exception

        End Try

        Return ret
    End Function


    Public Function GetLifeAsiaPolicyNo(ByVal strPolicy As String) As String
        Dim ret As String = ""

        Try
            Dim objCI As LifeClientInterfaceComponent.clsPOS
            objCI = New LifeClientInterfaceComponent.clsPOS()

            objCI.DBHeader = gobjDBHeader
            objCI.MQQueuesHeader = gobjMQQueHeader

            Dim strErr As String = ""
            If objCI.GetLifeAsiaPolicyNo(ret, strPolicy, strErr) Then
                ret = ret.Trim
            End If

        Catch ex As Exception

        End Try

        Return ret
    End Function
    'ITSR933 FG R3 CE End
#End Region





    Private Function DecryptString128Bit(ByVal vstrStringToBeDecrypted As String,
                                    ByVal vstrDecryptionKey As String) As String

        Dim bytDataToBeDecrypted() As Byte
        Dim bytTemp() As Byte
        Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
        Dim objRijndaelManaged As New RijndaelManaged
        Dim objMemoryStream As MemoryStream
        Dim objCryptoStream As CryptoStream
        Dim bytDecryptionKey() As Byte

        Dim intLength As Integer
        Dim intRemaining As Integer
        Dim intCtr As Integer
        Dim strReturnString As String = String.Empty
        Dim achrCharacterArray() As Char
        Dim intIndex As Integer

        '   *****************************************************************
        '   ******   Convert base64 encrypted value to byte array      ******
        '   *****************************************************************

        bytDataToBeDecrypted = Convert.FromBase64String(vstrStringToBeDecrypted)

        '   ********************************************************************
        '   ******   Encryption Key must be 256 bits long (32 bytes)      ******
        '   ******   If it is longer than 32 bytes it will be truncated.  ******
        '   ******   If it is shorter than 32 bytes it will be padded     ******
        '   ******   with upper-case Xs.                                  ****** 
        '   ********************************************************************

        intLength = Len(vstrDecryptionKey)

        If intLength >= 32 Then
            vstrDecryptionKey = Strings.Left(vstrDecryptionKey, 32)
        Else
            intLength = Len(vstrDecryptionKey)
            intRemaining = 32 - intLength
            vstrDecryptionKey = vstrDecryptionKey & Strings.StrDup(intRemaining, "X")
        End If

        bytDecryptionKey = Encoding.ASCII.GetBytes(vstrDecryptionKey.ToCharArray)

        ReDim bytTemp(bytDataToBeDecrypted.Length)

        objMemoryStream = New MemoryStream(bytDataToBeDecrypted)

        '   ***********************************************************************
        '   ******  Create the decryptor and write value to it after it is   ******
        '   ******  converted into a byte array                              ******
        '   ***********************************************************************

        Try

            objCryptoStream = New CryptoStream(objMemoryStream,
               objRijndaelManaged.CreateDecryptor(bytDecryptionKey, bytIV),
               CryptoStreamMode.Read)

            objCryptoStream.Read(bytTemp, 0, bytTemp.Length)

            objCryptoStream.FlushFinalBlock()
            objMemoryStream.Close()
            objCryptoStream.Close()

        Catch

        End Try

        '   *****************************************
        '   ******   Return decypted value     ******
        '   *****************************************

        Return StripNullCharacters(Encoding.ASCII.GetString(bytTemp))

    End Function

    Private Function StripNullCharacters(ByVal vstrStringWithNulls As String) As String

        Dim intPosition As Integer
        Dim strStringWithOutNulls As String

        intPosition = 1
        strStringWithOutNulls = vstrStringWithNulls

        Do While intPosition > 0
            intPosition = InStr(intPosition, vstrStringWithNulls, vbNullChar)

            If intPosition > 0 Then

                strStringWithOutNulls = Microsoft.VisualBasic.Strings.Left(strStringWithOutNulls, intPosition - 1) &
                                  Microsoft.VisualBasic.Strings.Right(strStringWithOutNulls, Len(strStringWithOutNulls) - intPosition)
            End If

            If intPosition > strStringWithOutNulls.Length Then
                Exit Do
            End If
        Loop

        Return strStringWithOutNulls

    End Function

    ' oliver 2023-12-15 added for Switch Over Code from Assurance to Bermuda
    Public Function GetPolicyList_Asur_WorkAround(ByVal strUCID As String, ByVal strTableName As String, ByVal blnInforceOnly As Boolean, ByRef lngErr As Long, ByRef strErr As String) As DataSet
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable

        lngErr = 0
        strErr = ""
        Try
            sqlconnect.ConnectionString = strAsurConn
            sqlconnect.Open()

            dt.TableName = strTableName
            sqlDA = New SqlDataAdapter("CRS_ODS_GetPolList_SP", sqlconnect)
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlDA.SelectCommand.Parameters.Add("@ucid", SqlDbType.VarChar).Value = strUCID
            'sqlDA.SelectCommand.Parameters.Add("@AllPolicy", SqlDbType.VarChar).Value = IF( blnInforceOnly, '', 'Y')
            sqlDA.SelectCommand.Parameters.Add("@ind", SqlDbType.VarChar).Value = IIf(blnInforceOnly, "", "Y")
            sqlDA.Fill(dt)

            ds.Tables.Add(dt)


        Catch err As SqlException
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & err.ToString()
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & ex.ToString()

        Finally
            sqlconnect.Dispose()
        End Try

        Return ds

    End Function

    Public Function GetPolicyList_Asur(ByVal strUCID As String, ByVal strTableName As String, ByVal blnInforceOnly As Boolean, ByRef lngErr As Long, ByRef strErr As String) As DataSet
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable

        lngErr = 0
        strErr = ""
        Try
            sqlconnect.ConnectionString = strLIDZConn
            sqlconnect.Open()

            dt.TableName = strTableName
            sqlDA = New SqlDataAdapter("CRS_ODS_GetPolList_SP", sqlconnect)
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlDA.SelectCommand.Parameters.Add("@ucid", SqlDbType.VarChar).Value = strUCID
            'sqlDA.SelectCommand.Parameters.Add("@AllPolicy", SqlDbType.VarChar).Value = IF( blnInforceOnly, '', 'Y')
            sqlDA.SelectCommand.Parameters.Add("@ind", SqlDbType.VarChar).Value = IIf(blnInforceOnly, "", "Y")
            sqlDA.Fill(dt)

            ds.Tables.Add(dt)


        Catch err As SqlException
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & err.ToString()
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & ex.ToString()

        Finally
            sqlconnect.Dispose()
        End Try

        Return ds

    End Function

    ''' <summary>
    ''' Get assurance policy by policy number.
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-14
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Function GetPolicy_Asur(ByVal strPolicyNo As String, ByVal strTableName As String, ByRef lngErr As Long, ByRef strErr As String, Optional ByVal operatedStr As String = "") As DataSet
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable

        lngErr = 0
        strErr = ""
        Try
            sqlconnect.ConnectionString = strAsurConn
            sqlconnect.Open()

            'If Not ValidateCriteria(strPN, sboxPolicyNumber_Assurance) Then Exit Function

            dt.TableName = strTableName
            sqlDA = New SqlDataAdapter("CRS_ODS_GetPolicySummary_pol_SP", sqlconnect)
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure
            'sqlDA.SelectCommand.Parameters.Add("@pol", SqlDbType.VarChar).Value = strPolicyNo
            If (String.IsNullOrEmpty(operatedStr)) Then
                operatedStr = "CHDRNUM = '" & strPolicyNo & "'"
            Else
                operatedStr = operatedStr.Replace("PolicyAccountID", "CHDRNUM")
            End If

            sqlDA.SelectCommand.Parameters.Add("@whereclause", SqlDbType.VarChar).Value = operatedStr

            sqlDA.Fill(dt)

            ds.Tables.Add(dt)
        Catch err As SqlException
            lngErr = -1
            strErr = "GetPolicy_Asur - " & err.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPolicySummary_pol_SP", "GetPolicy_Asur", err.Message, err)
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPolicy_Asur - " & ex.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPolicySummary_pol_SP", "GetPolicy_Asur", ex.Message, ex)
        Finally
            sqlconnect.Dispose()
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Get assurance policy's roles by policy number.
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-14
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Function GetPolicyRoles_Asur(ByVal strPolicyNo As String, ByVal strTableName As String, ByRef lngErr As Long, ByRef strErr As String) As DataSet
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable

        lngErr = 0
        strErr = ""
        Try
            sqlconnect.ConnectionString = strAsurConn
            sqlconnect.Open()

            dt.TableName = strTableName
            sqlDA = New SqlDataAdapter("CRS_ODS_GetPolicySummary_cln_SP", sqlconnect)
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlDA.SelectCommand.Parameters.Add("@pol", SqlDbType.VarChar).Value = strPolicyNo
            sqlDA.Fill(dt)

            ds.Tables.Add(dt)
        Catch err As SqlException
            lngErr = -1
            strErr = "GetPolicyRoles_Asur - " & err.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPolicySummary_cln_SP", "GetPolicyRoles_Asur", err.Message, err)
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPolicyRoles_Asur - " & ex.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPolicySummary_cln_SP", "GetPolicyRoles_Asur", ex.Message, ex)
        Finally
            sqlconnect.Dispose()
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Get assurance personal detail by <paramref name="strUCID">customer ID</paramref>
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-25
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Function GetPersonalDetail_Asur(ByVal strUCID As String, ByVal strTableName As String, ByRef lngErr As Long, ByRef strErr As String) As DataSet
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable

        lngErr = 0
        strErr = ""
        Try
            sqlconnect.ConnectionString = strAsurConn
            sqlconnect.Open()

            dt.TableName = strTableName
            sqlDA = New SqlDataAdapter("CRS_ODS_GetPersonalDetail_SP", sqlconnect)
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure
            sqlDA.SelectCommand.Parameters.Add("@ucid", SqlDbType.VarChar).Value = strUCID
            sqlDA.Fill(dt)

            ds.Tables.Add(dt)
        Catch err As SqlException
            lngErr = -1
            strErr = "GetPolicyRoles_Asur - " & err.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPersonalDetail_SP", "GetPolicyRoles_Asur", err.Message, err)
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPolicyRoles_Asur - " & ex.ToString()

            LogInformation("ERROR", "CRS_ODS_GetPersonalDetail_SP", "GetPolicyRoles_Asur", ex.Message, ex)
        Finally
            sqlconnect.Dispose()
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Assign value to date time picker control.
    ''' If value is null or value = min/max date, make the date time picker displays empty
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-31
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Sub AssignValueToDateTimePicker(ByVal dtp As DateTimePicker, ByVal d As Nullable(Of Date))
        Dim isValidValue As Boolean = False
        If d Is Nothing Then
            isValidValue = False
        Else
            If d.Value = Date.MinValue Or d.Value = Date.MaxValue Or
                d.Value.Date = Date.MinValue.Date Or d.Value.Date = Date.MaxValue.Date Then
                isValidValue = False
            Else
                isValidValue = True
            End If
        End If

        If isValidValue Then
            dtp.Value = d
        Else
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = " "
            dtp.Value = Date.FromOADate(0)
            dtp.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Assign value to date time picker control.
    ''' If value is null or value = min/max date, make the date time picker displays empty
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-31
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Sub AssignValueToDateTimePicker(ByVal dtp As DateTimePicker, ByVal dr As DataRow, ByVal columnName As String)
        Dim d As DateTime
        Dim isValidValue As Boolean = False

        If dr.IsNull(columnName) Then
            isValidValue = False
        Else
            d = Convert.ToDateTime(dr(columnName))
            If d = DateTime.MinValue Or d = DateTime.MaxValue Or
                d.Date = DateTime.MinValue.Date Or d.Date = DateTime.MaxValue.Date Then
                isValidValue = False
            Else
                isValidValue = True
            End If
        End If

        If isValidValue Then
            dtp.Value = d
        Else
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = " "
            dtp.Value = Date.FromOADate(0)
            dtp.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Log information
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-31
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Public Sub LogInformation(ByVal strType As String, ByVal strRef As String, ByVal strInfoVal As String, ByVal strRtnMsg As String, ByVal ex As Exception)
        Try
            Dim exMessage As String = String.Empty
            Dim exStackTrace As String = String.Empty
            Dim SysEventLog As New SysEventLog.clsEventLog
            Dim objDBHeader As Utility.Utility.ComHeader
            objDBHeader.UserID = gsUser
            objDBHeader.EnvironmentUse = g_Env '"SIT02"
            objDBHeader.ProjectAlias = "LAS" '"LAS"
            objDBHeader.CompanyID = g_Comp '"ING"
            objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

            SysEventLog.CiwHeader = objDBHeader

            If Not ex Is Nothing Then
                exMessage = ex.Message
                exStackTrace = ex.StackTrace
            End If

            SysEventLog.ProcEventLog(strType, Now, objDBHeader.UserID, "CRS", objDBHeader.MachineID, exMessage, exStackTrace, strRef, strInfoVal, strRtnMsg, False)
        Catch e As Exception
            ' ignore exception, don't disturb main logic.
        End Try
    End Sub
    'Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

    '    Dim sha1 As New SHA1CryptoServiceProvider

    '    ' Hash the key.
    '    Dim keyBytes() As Byte =
    '        System.Text.Encoding.Unicode.GetBytes(key)
    '    Dim hash() As Byte = sha1.ComputeHash(keyBytes)

    '    ' Truncate or pad the hash.
    '    ReDim Preserve hash(length - 1)
    '    Return hash
    'End Function
    'Public Function DecryptData(ByVal encryptedtext As String) As String
    '    TripleDes.Key = TruncateHash("UNW9byPekW", TripleDes.KeySize \ 8)
    '    TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)

    '    ' Convert the encrypted text string to a byte array.
    '    Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

    '    ' Create the stream.
    '    Dim ms As New System.IO.MemoryStream
    '    ' Create the decoder to write to the stream.
    '    Dim decStream As New CryptoStream(MS, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

    '    ' Use the crypto stream to write the byte array to the stream.
    '    decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
    '    decStream.FlushFinalBlock()

    '    ' Convert the plaintext stream to a string.
    '    Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    'End Function

    Public Function GetRenewalPaymentMethodValidation(ByVal strPolicyNo As String) As Boolean
        Dim isValid As Boolean = False
        Dim objPOSWS As POSWS.POSWS = POSWS_HK()
        Dim strErr As String
        Dim dsRtn As New DataSet

        Try
            objPOSWS.LA_CheckRenewalPaymentMethodValidation(strPolicyNo, "", dsRtn, strErr)
            If dsRtn.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dsRtn.Tables(0).Rows

                    If (dr("FACTHOUS").ToString.Trim.Equals("02") Or dr("FACTHOUS").ToString.Trim.Equals("26") Or dr("FACTHOUS").ToString.Trim.Equals("27")) And dr("Yearly").ToString.Trim.Equals("Y") Then
                        isValid = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Error GetRenewalPaymentMethodValidation : " & strErr & vbNewLine & ex.Message & ex.StackTrace)
        End Try

        Return isValid
    End Function

#Region "MediSaver"

    'ITSR2408 - SL20210625
    Public Function GetMediSavingAmount(ByVal strPolicyNo As String, ByVal blnWarning As Boolean) As Decimal
        Dim objPOSWS As POSWS.POSWS = POSWS_HK()
        Dim objMCSWS As MCSWS.MCSWS = MCSWS_HK()
        Dim strErr As String
        Dim dsBeneHist As New DataSet
        Dim dsElgBal As New DataSet
        Dim decElgAmt As Decimal = -1

        'dtBenefit = Me.dsClaim.Tables("mcsvw_claim_bene_hist").Copy()
        'dtBenefit.Columns.Add("mcscbh_plan_code")
        'dtBenefit.Columns.Add("mcscbh_paid_amt")
        'dtBenefit.Columns.Add("Benefit")

        Try
            objMCSWS.getClaimBenefitHistory("", strErr, dsBeneHist, "", -1, -1, strPolicyNo.Trim, 1, "Optional", "Optional")

            If dsBeneHist.Tables(0).Rows.Count > 0 Then
                Dim dtMediSav As DataTable = Nothing
                objPOSWS.GetPosSoftCode("MediSaverPlan", dtMediSav, False, strErr)
                For Each drBen As DataRow In dsBeneHist.Tables(0).Rows
                    For Each drMSP As DataRow In dtMediSav.Rows
                        If drMSP("possc_code").ToString.Trim = drBen("mcscbh_plan_code").ToString.Trim Then
                            dsElgBal = objMCSWS.GetSavingABal(drBen("mcscbh_policy_no").ToString.Trim, "01", strErr)
                            decElgAmt = Math.Round(Convert.ToDecimal(dsElgBal.Tables(dsElgBal.Tables.Count - 1).Rows(0)("ZEGCLAMT01")), 2)
                            If decElgAmt = 0 AndAlso blnWarning Then
                                MsgBox("Policy is terminated as medical saving account reaches 100% of sum insured.")
                            End If
                            Return decElgAmt
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            MsgBox("Problem when retrieve saving amount. : " & strErr & vbNewLine & ex.Message & ex.StackTrace) 'Temp Comment Assurance
            appSettings.Logger.logger.Error(ex.Message.ToString)
        End Try

        Return 0
    End Function

    Public Function getCompanyCode(Optional ByVal strCompany As String = "") As String
        Dim company As String = IIf(strCompany = "", g_Comp, strCompany)
        If company.ToLower() = "macau" Or company.ToLower() = "mcu" Or company.ToLower() = "mc" Then
            Return "MC"
        ElseIf company.ToLower() = "lac" Then
            Return "LAC"
        ElseIf company.ToLower() = "lah" Then
            Return "LAH"
        Else
            Return "HK"
        End If
    End Function

    ''' <summary>
    ''' Get company ID code by company name
    ''' </summary>
    ''' <param name="companyName">Company name, e.g. Bermuda, Bermuda/Private, Assurance, Macau, Private</param>
    ''' <returns>Return company ID code, e.g. ING, LAC, MCU, BMU</returns>
    Public Function GetCompanyCodeByName(companyName As String) As String
        Select Case companyName?.Trim.ToUpper
            Case "BERMUDA/PRIVATE", "BERMUDA"
                Return g_Comp
            Case "ASSURANCE"
                Return g_LacComp
            Case "MACAU"
                Return g_McuComp
            Case "PRIVATE"
                Return g_BmuComp
            Case Else
                Return g_Comp
        End Select
    End Function

    Public Function getEnvCode(Optional ByVal strEnv As String = "") As String
        If gUAT Then
            If strEnv <> "" Then
                Return strEnv & g_Env
            Else
                Return "CIW" & g_Env
            End If
        Else
            If strEnv <> "" Then
                Return strEnv
            Else
                Return "Vantive"
            End If
        End If
    End Function

#End Region

    Public Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim props() As PropertyInfo = GetType(T).GetProperties()
        For Each prop As PropertyInfo In props
            If prop.GetCustomAttributes(True).Length > 0 Then
                Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                If element Is Nothing Then
                    table.Columns.Add(prop.Name, prop.PropertyType)
                Else
                    table.Columns.Add(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName), If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
                End If
            Else
                table.Columns.Add(prop.Name, prop.PropertyType)
            End If
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyInfo In props
                If prop.GetCustomAttributes(True).Length > 0 Then
                    Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                    If element Is Nothing Then
                        row(prop.Name) = prop.GetValue(item, Nothing)
                    Else
                        'If (Nullable.GetUnderlyingType(prop.PropertyType) Is GetType(DateTime)) Then
                        '    row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(item, Nothing) Is Nothing, DBNull.Value, prop.GetValue(item, Nothing))
                        'End If
                        row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(item, Nothing) Is Nothing, DBNull.Value, prop.GetValue(item, Nothing))
                    End If
                Else
                    row(prop.Name) = prop.GetValue(item, Nothing)
                End If
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

    Public Function ConvertToDataTable(Of T)(ByVal obj As T) As DataTable
        Dim table As New DataTable()
        Dim props() As PropertyInfo = GetType(T).GetProperties()
        For Each prop As PropertyInfo In props
            If prop.GetCustomAttributes(True).Length > 0 Then
                Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                If element Is Nothing Then
                    table.Columns.Add(prop.Name, prop.PropertyType)
                Else
                    table.Columns.Add(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName), If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
                End If
            Else
                table.Columns.Add(prop.Name, prop.PropertyType)
            End If
        Next
        Dim row As DataRow = table.NewRow()
        For Each prop As PropertyInfo In props
            If prop.GetCustomAttributes(True).Length > 0 Then
                Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                If element Is Nothing Then
                    row(prop.Name) = prop.GetValue(obj, Nothing)
                Else
                    row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(obj, Nothing) Is Nothing, DBNull.Value, prop.GetValue(obj, Nothing))
                End If
            Else
                row(prop.Name) = prop.GetValue(obj, Nothing)
            End If
        Next
        table.Rows.Add(row)
        Return table
    End Function
#Region "CI Claim Term"

    'ITSR3488 - CL20220928 - start
    ''' <remarks>
    ''' <br>20241119 Chrysan Cheng, CRS performer slowness - Reuse BusinessDate to reduce the number of calls</br>
    ''' </remarks>
    Public Function IsAMCClaimValid(strPolicyNo As String, businessDate As Date)
        Dim objPOSWS As POSWS.POSWS = POSWS_HK()
        Dim objMCSWS As MCSWS.MCSWS = MCSWS_HK()
        Dim strErr As String = ""
        Dim dtBusDate As Date = businessDate

        Try
            Dim flag1 As Boolean
            Dim flag2 As Boolean

            'If Not GetBusinessDate(dtBusDate) Then
            '    dtBusDate = Now()
            'End If
            If Not objMCSWS.IsAMCClaimValid(strPolicyNo, dtBusDate, flag1, flag2, strErr) Then
                If strErr <> "" Then
                    'throw exception / msg err + exit sub
                End If
                If Not flag1 Then
                    MsgBox("The benefit of Additional Medical Coverage is expired which is two (2) years after the date of the First confirmed Diagnosis of such Big 3 Disease.")
                End If
                If Not flag2 Then
                    MsgBox("The benefit of Additional Medical Coverage reached the max limit.")
                End If
            End If


        Catch ex As Exception
            MsgBox("Problem when call IsAMCClaimValid : " & strErr & vbNewLine & ex.Message & ex.StackTrace)
        End Try


    End Function


#End Region
    'ITSR3488 - CL20220928 - end

    Public Sub InsertVVIPLog(strPolicy As String, strCustNo As String, strCustRole As String, strScreenName As String, blnIsUHNWMember As Boolean,
                             Optional companyCode As String = Nothing)
        Try
            If GetTestConfig("VLogTestMode").Equals("") Then
                ' Do Insert Log to crs_vvip_access_log
                APIServiceBL.ExecAPIBusi(If(companyCode, g_Comp), "INST_VVIP_LOG_INFO",
                                New Dictionary(Of String, String) From {
                                {"system", "CRS"},
                                {"policyNo", strPolicy},
                                {"customerNo", strCustNo},
                                {"customerRole", strCustRole},
                                {"screenName", strScreenName},
                                {"action", "Enquiry"},
                                {"userID", gsUser},
                                {"vvipAuthorized", IIf(blnIsUHNWMember, "Y", "N")}
                                })
            End If
        Catch ignore As Exception
        End Try
    End Sub

    Public Function GetTestConfig(ByVal nodeName As String) As String
        Try
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load(AppDomain.CurrentDomain.BaseDirectory & "\TestConfig.xml")

            Dim xmlNode As XmlNode = doc.SelectSingleNode("Root/" & nodeName)
            If Not xmlNode Is Nothing Then
                Return xmlNode.InnerText.ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

#Region "ITSR3162 & 4101"

    ''' <summary>
    ''' CCM Web Service
    ''' </summary>
    ''' <returns></returns>
    Public Function CCMWebService() As CCMWS.CCMWS
        Dim ccmws As New CCMWS.CCMWS

        ccmws.GlobalHeaderValue = New CCMWS.GlobalHeader()
        ccmws.GlobalHeaderValue.gVal = New CCMWS.GlobalVal()
        ccmws.GlobalHeaderValue.gVal.ProjectName = "CCM"
        ccmws.GlobalHeaderValue.gVal.UserType = "CCMUPDATE"

        If "PRD01".Equals(g_Env, StringComparison.OrdinalIgnoreCase) Then
            ccmws.GlobalHeaderValue.gVal.CcmDbName = "CCM"
        Else
            ccmws.GlobalHeaderValue.gVal.CcmDbName = g_Comp & "CCMU102" '& g_Env
        End If

        Dim comHeader As New Utility.Utility.ComHeader
        Dim mqHeader As New Utility.Utility.MQHeader
        comHeader.CompanyID = g_Comp
        comHeader.EnvironmentUse = g_Env
        If Not gUAT Then
            ccmws.Url = Utility.Utility.GetWebServiceURL("CCMWS", comHeader, mqHeader)
        End If
        ccmws.Credentials = System.Net.CredentialCache.DefaultCredentials
        ccmws.Timeout = 1800000

        Return ccmws
    End Function

#End Region

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Validate Special Symbols
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-25 by oliver</br>
    ''' </remarks>
    ''' <param name="validateString">Represents a string that needs to be validated</param>
    ''' <returns></returns>
    Public Function ValidateStringIsContainsSpecialSymbols(ByVal validateString As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(validateString, "^[\u4e00-\u9fa5A-Za-z0-9()\-\+\s]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
    End Function

    Public Function GetPolicyListByAPI(ByVal strCompanyCode As String, ByVal strInPolicy As String, ByVal strCustID As String, ByVal strRelateCode As String,
                                        ByVal strTable As String, ByVal strCri As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
                                        ByRef dtPolicyList As DataTable, Optional ByVal blnCntOnly As Boolean = False, Optional ByVal strCompanyID As String = "") As Boolean

        If String.IsNullOrEmpty(strCompanyCode) Then strCompanyCode = g_Comp
        Dim retVal As Boolean = False
        Dim strPolicyRelateCode As String = String.Empty
        Dim strInputWithCri As String = String.Empty

        'dtPolicyList.TableName = "PolicyAccTbl"

        If strRelateCode <> "" Then
            strPolicyRelateCode = " 'PH' "
        Else
            strPolicyRelateCode = " 'PH', 'PI' "
        End If

        If strInPolicy <> "" Then
            strInputWithCri = " p.policyaccountid " & strCri & " " & strInPolicy & " "
        ElseIf strCustID <> "" Then
            strInputWithCri = " r.customerid " & strCri & " " & strCustID & " "
        End If

        'oliver 2024-7-31 added for Com 6
        If strCompanyID <> "" Then
            strInputWithCri &= " and p.CompanyID " & strCri & " '" & strCompanyID & "' "
        End If

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(strCompanyCode, "GET_POLICYLIST_CIW",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strPolicyRelateCode", strPolicyRelateCode},
                                                                {"strInputWithCri", strInputWithCri}
                                                            })

            If Not IsNothing(retDs) Then
                If Not IsNothing(retDs.Tables(0)) Then

                    'If blnCntOnly AndAlso retDs.Tables(0).Rows.Count > intCnt Then
                    '    intCnt = retDs.Tables(0).Rows.Count
                    '    Exit Function
                    'End If
                    dtPolicyList = retDs.Tables(0).Copy
                    dtPolicyList.TableName = strTable
                    intCnt = retDs.Tables(0).Rows.Count
                    Return True
                End If
            End If
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.Message
            Return False
        End Try
    End Function

    Public Function ConcatServerDB(ByVal server As String, ByVal db As String) As String
        Return String.Concat("[", server, "].", db)
    End Function

    ''' <summary>
    ''' Get CRS DB server prefix name string by company ID
    ''' </summary>
    ''' <param name="companyID">The company ID</param>
    ''' <returns>Return corresponding prefix name with dot</returns>
    Public Function GetCrsServerPrefixByCompanyID(companyID As String) As String
        Select Case companyID?.Trim.ToUpper
            Case "ING", "BMU"
                Return ConcatServerDB(gcCRSServer, gcCRSDB)
            Case "LAC", "LAH"
                Return ConcatServerDB(gcCRSServer, gcCRSDBAsur)
            Case "MCU"
                Return ConcatServerDB(gcCRSServer, gcCRSDBMcu)
            Case Else
                Return ConcatServerDB(gcCRSServer, gcCRSDB)
        End Select
    End Function

    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Point A-10
    ''' Check Is Exist In CustomerAlertTable By PolicyNo To Prompt
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-01 Added for CRS Enhancement(General Enhance Ph4) Point A-10</br>
    ''' </remarks>
    ''' <param name="policyNo">Represents the policyNo is parameter to get SinglePolicyRelatedCustomerTable</param>
    Public Sub CheckIsExistCustomerAlertTableByPolicyNoToPrompt(policyNo As String)

        Dim dt As DataTable = GetSinglePolicyRelatedCustomerTableByPolicyNo(policyNo)
        Dim selectConditionFormat As String = "governmentidcard='{0}' or passportnumber='{0}'"
        Try
            If dt.Rows.Count > 0 Then
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    Dim selectCondition As String = ""
                    If Not String.IsNullOrEmpty(dt.Rows(i).Item("governmentidcard").ToString()) Then
                        selectCondition = String.Format(selectConditionFormat, dt.Rows(i).Item("governmentidcard").ToString(), dt.Rows(i).Item("passportnumber").ToString())
                    ElseIf Not String.IsNullOrEmpty(dt.Rows(i).Item("passportnumber").ToString()) Then
                        selectCondition = String.Format(selectConditionFormat, dt.Rows(i).Item("passportnumber").ToString())
                    Else
                        Exit Sub
                    End If

                    Dim drArry As DataRow() = dt.Select(selectCondition)
                    If drArry.Length > 1 Then
                        dt.Rows.RemoveAt(i)
                    End If
                Next

                For Each dr As DataRow In dt.Rows
                    CheckIsExistCustomerAlertTableByCustomerIDToPrompt(dr.Item("CustomerID").ToString())
                Next
            End If
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Point A-10
    ''' Check Is Exist In CustomerAlertTable By CustomerID To Prompt
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-01 Added for CRS Enhancement(General Enhance Ph4) Point A-10</br>
    ''' </remarks>
    ''' <param name="customerID">Represents the customerID is parameter to get SingleCustomerAlertTable</param>
    Public Sub CheckIsExistCustomerAlertTableByCustomerIDToPrompt(customerID As String)
        Dim dt As DataTable = GetSingleCustomerAlertTableByCustomerID(customerID)
        If dt.Rows.Count > 0 Then

            Dim customerAlertAllMessage As StringBuilder = New StringBuilder()
            For Each dr As DataRow In dt.Rows
                Dim isPrompt As String = IIf(Not String.IsNullOrEmpty(dr.Item("cswca_prompt").ToString()), dr.Item("cswca_prompt").ToString(), "N")
                If isPrompt = "Y" Then
                    Dim customerAlertSingleMessage As String = "(" & dr.Item("row_num").ToString() & ")" & " Message Content:" & vbCrLf & dr.Item("cswca_message").ToString() & vbCrLf
                    customerAlertAllMessage.AppendLine(customerAlertSingleMessage)
                End If
            Next

            If customerAlertAllMessage.Length > 0 Then
                Dim alertTitle As String = dt.Rows.Count & " Customer Alert Messages About CustomerID: " & customerID
                MsgBox(customerAlertAllMessage.ToString(), MsgBoxStyle.Information, alertTitle)
            End If

        End If
    End Sub

    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Point A-10
    ''' Get SingleCustomerAlertTable by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-01 Added for CRS Enhancement(General Enhance Ph4) Point A-10</br>
    ''' </remarks>
    ''' <param name="customerID">Represents the customerID is part of the dictionary to get the CustomerAlertDataSet</param>
    Private Function GetSingleCustomerAlertTableByCustomerID(customerID As String) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_CUSTOMER_ALERT_BY_CUSTOMERID",
                        New Dictionary(Of String, String) From {
                        {"CustomerID", customerID}
                        })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI GET_CUSTOMER_ALERT_BY_CUSTOMERID Retrieve Error." & vbCrLf & ex.Message)
            Return dt
        End Try

    End Function

    ''' <summary>
    ''' CRS Enhancement(General Enhance Ph4)
    ''' Point A-10
    ''' Get SinglePolicyRelatedCustomerTable by using SearchBusiAPI in CRS_API
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-01 Added for CRS Enhancement(General Enhance Ph4) Point A-10</br>
    ''' </remarks>
    ''' <param name="policyNo">Represents the policyNo is part of the dictionary to get the PolicyRelatedCustomerDataSet</param>
    Private Function GetSinglePolicyRelatedCustomerTableByPolicyNo(policyNo As String) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_POLICY_RELATE_CUSTOMER_BY_POLICYNO",
                        New Dictionary(Of String, String) From {
                        {"policyaccountid", policyNo}
                        })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI GET_POLICY_RELATE_CUSTOMER_BY_POLICYNO Retrieve Error." & vbCrLf & ex.Message)
            Return dt
        End Try
    End Function

    Public Function GetAsurPolicyMappingListByAPI(ByVal strCompanyCode As String, ByVal strInPolicy As String, ByRef lngErrNo As Long,
                                                  ByRef strErrMsg As String, ByRef intCnt As Integer, ByRef dtPolicyList As DataTable) As Boolean

        If String.IsNullOrEmpty(strCompanyCode) Then strCompanyCode = g_Comp
        Dim strInputWithCri As String = String.Empty

        strInPolicy = "'" & strInPolicy.Replace("'", "") & "'"

        If strInPolicy <> "" Then
            strInputWithCri = " (cpm.cswpm_Assurance_policy = " & strInPolicy & " or cpm.cswpm_AssApp_policy = " & strInPolicy & " or cpm.cswpm_AssonePass_policy = " & strInPolicy & ") and cpm.cswpm_la_policy <> " & strInPolicy & " "
        End If

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(strCompanyCode, "GET_ASUR_POLICY_MAPPING_LIST",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInputWithCri", strInputWithCri}
                                                            })

            If Not IsNothing(retDs) Then
                If Not IsNothing(retDs.Tables(0)) Then

                    dtPolicyList = retDs.Tables(0).Copy
                    dtPolicyList.TableName = "AsurPolicyMapping"
                    intCnt = retDs.Tables(0).Rows.Count
                    Return True
                End If
            End If
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.Message
            Return False
        End Try
    End Function

    Public Sub HandleGlobalException(ex As Exception, msg As String, Optional title As String = "Error", Optional button As MessageBoxButtons = MessageBoxButtons.OK, Optional icon As MessageBoxIcon = MessageBoxIcon.Error)
        Dim errMsg As String = "An unhandled exception occurs.Please Contact IT!" & Environment.NewLine & Environment.NewLine
        errMsg += "Error: " & ex.Message.Trim() & Environment.NewLine & Environment.NewLine
        errMsg += "StackTrace: " & ex.StackTrace.Trim()
        System.Diagnostics.Debug.WriteLine(errMsg)
        MessageBox.Show(msg, title, button, icon)
    End Sub

    ''' <summary>
    ''' Get NB system info config DataSet
    ''' </summary>
    ''' <remarks>
    ''' <br>20231215 Oliver, Commented for optimized login speed</br>
    ''' <br>20241029 Chrysan Cheng, CRS performer slowness - Change to call CRS_API with cache</br>
    ''' </remarks>
    Public Sub CheckUpdateNbSystemTable(ByVal objDBHeader1 As Utility.Utility.ComHeader, ByRef dsSysTable1 As DataSet, ByRef dtLastUpdate As DateTime)
        Try
            Dim iHoursSinceLastUpdate As Integer = Math.Abs(DateDiff(DateInterval.Hour, dtLastUpdate, Now))
            If iHoursSinceLastUpdate >= 12 Then
                'Dim objCI As New LifeClientInterfaceComponent.CommonControl
                'objCI.ComHeader = objDBHeader1
                'Dim strErr As String = ""

                'dsSysTable1 = New DataSet
                'If objCI.GetNbSysInfo(dsSysTable1, strErr) Then
                '    dtLastUpdate = Now
                'ElseIf strErr.Trim = "" Then
                '    MsgBox("Cannot get component system table!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                'Else
                '    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                'End If

                dsSysTable1 = New DataSet   ' reset first

                Dim sysInfoBL As New SystemInfoAsyncBL(objDBHeader1, Nothing)   ' no need use mqHeader for this function
                dsSysTable1 = sysInfoBL.GetNbSysInfo()
                dtLastUpdate = Now
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Public Function RetrievePostSalesCallPolicyQuestion(ByRef strInPolicy As String, ByRef dtPostSalesCall As DataTable, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Dim sbSql As New Text.StringBuilder()
        Dim strAppend As String
        Dim callType As PostSalesCallType
        dtPostSalesCall = New DataTable

        If Not GetPolicyPostSalesCallType(strInPolicy, callType, strErr, companyName) Then
            Return False
        End If

        Try
            If callType = PostSalesCallType.Welcome OrElse callType = PostSalesCallType.iLAS Then
                sbSql.AppendLine(")") ' No question for non-iLAS welcome call
            ElseIf (callType And PostSalesCallType.NoFNA) = PostSalesCallType.NoFNA Then
                sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_no_fna='Y'))")
            ElseIf (callType And PostSalesCallType.VulnerableCustomer) = PostSalesCallType.VulnerableCustomer Then
                sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_vc = 'Y'))")
            ElseIf (callType And PostSalesCallType.SuitabilityMismatch) = PostSalesCallType.SuitabilityMismatch Then
                sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_sm = 'Y'))")
            End If

            strAppend = sbSql.ToString()

            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "RETRIEVE_POST_SALES_CALL_POLICY_QUESTION",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInPolicy", strInPolicy},
                                                                {"strAppend", strAppend}
                                                            })

            If Not IsNothing(retDs) Then
                dtPostSalesCall.TableName = "PolicyQuestion"
                dtPostSalesCall = retDs.Tables(0)
            End If
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI RETRIEVE_POST_SALES_CALL_POLICY_QUESTION Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function
    'Welcome Call Type
    Public Function GetCallTypeForWelcomeCall(ByRef dtResult As DataTable, ByRef strErr As String, Optional companyName As String = "") As Boolean
        Dim policyNumbers As String = String.Join(",", dtResult.AsEnumerable().Select(Function(row) row("Policy_No").ToString()))
        Dim connection As SqlConnection = New SqlConnection(IIf(companyName.Equals("MC"), strCIWMcuConn, strCIWConn))
        Dim dtWithCallType As DataTable = New DataTable
        connection.Open()
        Try
            Dim command As SqlCommand = New SqlCommand("cswsp_GetPostSalesCallList", connection)
            command.Parameters.Add("@policyNo", policyNumbers)
            command.Parameters.Add("@dateFrom", DBNull.Value)
            command.Parameters.Add("@dateTo", DBNull.Value)
            command.CommandType = CommandType.StoredProcedure

            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(dtWithCallType)

            If dtWithCallType.Rows.Count > 0 Or 1 = 1 Then
                ' Get WelcomCall
                Dim dtWelcomeCall As DataTable = dtWithCallType.Clone()
                Dim rows() As DataRow = dtWithCallType.Select("CallType = 'Welcome Call'")
                For Each row As DataRow In rows
                    dtWelcomeCall.ImportRow(row)
                Next
                'dtWithCallType = dtWelcomeCall
                Dim keys_WelcomePolicyId As New HashSet(Of String)()
                For Each row As DataRow In dtWelcomeCall.Rows
                    keys_WelcomePolicyId.Add(row("PolicyAccountID").ToString())
                Next

                'filter agent & filter bank channel - exclude these records 
                Try
                    Dim welcomeType_policies As String = String.Join(",", keys_WelcomePolicyId)
                    Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "FILTER_AGENT_BANK",
                                                New Dictionary(Of String, String)() From {
                                                {"strInPolicy", welcomeType_policies}
                                                })
                    If Not IsNothing(retDs) Then
                        Dim dt_RecordsToRemove As DataTable = retDs.Tables("filter_agent_bank")
                        If dt_RecordsToRemove.Rows.Count > 0 Then
                            For Each row As DataRow In dt_RecordsToRemove.Rows
                                Dim policyAccountID_ToRemove As String = row("PolicyAccountID").ToString()
                                If keys_WelcomePolicyId.Contains(policyAccountID_ToRemove) Then
                                    keys_WelcomePolicyId.Remove(row("PolicyAccountID").ToString())
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Dim strError As String
                    strError = "CRSAPI calling FILTER_AGENT_BANK Retrieve Error." & vbCrLf & ex.Message
                    HandleGlobalException(ex, strError)
                End Try
                '
                Dim filteredDtResult As DataTable = dtResult.Clone()
                For Each row As DataRow In dtResult.Rows
                    If keys_WelcomePolicyId.Contains(row("Policy_No").ToString()) AndAlso
                    Not {"BCOM", "CCBA", "NYCB", "ICBC", "BANK"}.Contains(row("Location").ToString()) Then
                        row("CallType") = "Welcome Call"
                        filteredDtResult.ImportRow(row)
                    End If
                Next
                dtResult = filteredDtResult
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw
        Finally
            connection.Close()
        End Try
    End Function
    '
    Public Function GetPolicyPostSalesCallType(ByRef strInPolicy As String, ByRef callType As PostSalesCallType, ByRef strErr As String, Optional companyName As String = "") As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Dim posDB As String = gcPOS

        'default
        callType = PostSalesCallType.Welcome

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "CHECK_ILAS",
                                                            New Dictionary(Of String, String)() From {
                                                            {"isLinked", "Y"},
                                                            {"strInPolicy", strInPolicy}
                                                            })
            If Not IsNothing(retDs) Then
                If retDs.Tables(0).Rows.Count > 0 Then
                    callType += PostSalesCallType.iLAS
                End If
            End If

            Dim retDs2 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "CHECK_ILAS",
                                                            New Dictionary(Of String, String)() From {
                                                            {"isLinked", "N"},
                                                            {"strInPolicy", strInPolicy}
                                                            })
            If Not IsNothing(retDs2) Then
                If retDs2.Tables(0).Rows.Count > 0 Then
                    Dim retDs3 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "CHECK_FNA_FORM_FOR_NON_ILAS",
                                                            New Dictionary(Of String, String)() From {
                                                            {"strInPolicy", strInPolicy},
                                                            {"DbStart", gcCIW}
                                                            })
                    If Not IsNothing(retDs) Then
                        If retDs.Tables(0).Rows.Count = 0 Then
                            callType += PostSalesCallType.NoFNA
                        Else
                            Dim connection As SqlConnection = New SqlConnection(IIf(companyName.Equals("MC"), strCIWMcuConn, strCIWConn))
                            Dim dt As DataTable = New DataTable
                            connection.Open()
                            Try
                                Dim command As SqlCommand = New SqlCommand("cswsp_GetPostSalesCallList", connection)
                                command.Parameters.Add("@policyNo", strInPolicy)
                                command.Parameters.Add("@dateFrom", DBNull.Value)
                                command.Parameters.Add("@dateTo", DBNull.Value)
                                command.CommandType = CommandType.StoredProcedure

                                Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
                                adapter.Fill(dt)

                                If dt.Rows.Count > 0 Then
                                    ' Check SM
                                    If dt.Rows(0)("CallType").ToString().Trim() = "Suitability Mismatch" Then
                                        callType += PostSalesCallType.SuitabilityMismatch
                                    Else
                                        ' Check VC
                                        If dt.Rows(0)("CallType").ToString().Trim() = "VC Call" Then
                                            callType += PostSalesCallType.VulnerableCustomer
                                        End If
                                    End If
                                End If

                            Catch ex As Exception
                                Console.WriteLine(ex.Message)
                                Throw
                            Finally
                                connection.Close()
                            End Try

                        End If
                    End If
                End If

                Return True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GetPolicyPostSalesCallType API Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try

    End Function

    Public Function SavePostSalesCallPolicyAnswer(ByVal dtQuestion As DataTable, ByVal userID As String, Optional companyName As String = "") As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            For Each drQuestion As DataRow In dtQuestion.Rows

                Dim sbSqlInsert As New Text.StringBuilder()
                Dim strInsertScript As String = String.Empty
                Dim strUpdateScript As String = String.Empty
                'Dim strCondition As String = String.Empty

                sbSqlInsert.AppendLine("'" & drQuestion("PolicyAccountID").ToString().Trim() & "',")
                sbSqlInsert.AppendLine("'" & drQuestion("cswpsq_question_id") & "',")
                sbSqlInsert.AppendLine("'" & drQuestion("cswpca_answer_value").ToString().Trim() & "',")
                sbSqlInsert.AppendLine("NULL,")
                sbSqlInsert.AppendLine("'" & userID & "',")
                sbSqlInsert.AppendLine("GETDATE(),")
                sbSqlInsert.AppendLine("'" & userID & "',")
                sbSqlInsert.AppendLine("GETDATE()")
                strInsertScript = sbSqlInsert.ToString()
                strUpdateScript = " cswpca_answer_value = '" & drQuestion("cswpca_answer_value").ToString().Trim() & "' "
                'strCondition = " cswpca_policy_no='" & drQuestion("PolicyAccountID").ToString().Trim() & "' and cswpca_question_id = " & drQuestion("cswpsq_question_id").ToString().Trim() & " "

                'call API SAVE_POSTSALES_CALL_POLICY_ANSWER
                APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                "SAVE_POSTSALES_CALL_POLICY_ANSWER",
                                New Dictionary(Of String, String) From {
                                {"strPolicyNo", drQuestion("PolicyAccountID").ToString().Trim()},
                                {"strQuestionID", drQuestion("cswpsq_question_id").ToString().Trim()},
                                {"strInsertScript", strInsertScript},
                                {"strUpdateScript", strUpdateScript}
                                })

            Next

            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI SAVE_POSTSALES_CALL_POLICY_ANSWER Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function RetrievePostSalesCallProductCategory(ByVal companyName As String, ByRef dtCategory As DataTable) As Boolean
        Dim blnSuccess = False
        dtCategory = New DataTable
        Dim ds As DataSet = New DataSet()
        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "RETRIEVE_POSTSALES_CALL_PRODUCT_CATEGORY",
                                     New Dictionary(Of String, String) From {})
            If ds.Tables.Count > 0 Then
                dtCategory = ds.Tables(0)
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI RETRIEVE_POSTSALES_CALL_PRODUCT_CATEGORY Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function IsPostSalesCallQuestionInUse(ByVal companyName As String, ByVal strQuestionID As String) As Boolean
        Dim blnSuccess = False

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "GET_POSTSALES_CALL_QUESTIONS_BY_ID",
                                     New Dictionary(Of String, String) From {{"strQuestionID", strQuestionID}})
            If retDs.Tables.Count > 0 Then
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_POSTSALES_CALL_QUESTIONS_BY_ID Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function RetrievePostSalesCallProductSettings(ByVal companyName As String, ByRef dsSetting As DataSet) As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "RETRIEVE_POSTSALES_CALL_PRODUCT_SETTING",
                                                            New Dictionary(Of String, String)() From {})

            If Not IsNothing(retDs) Then
                dsSetting = retDs
                Return True
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI RETRIEVE_POSTSALES_CALL_PRODUCT_SETTING Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function DeletePostSalesCallProductSetting(ByVal companyName As String, ByVal strProductID As String) As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                "DELETE_POSTSALES_CALL_PRODUCT_SETTING",
                                New Dictionary(Of String, String) From {
                                {"str_cswpsdProductID", strProductID},
                                {"str_cswpsnProductID", strProductID}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI DELETE_POSTSALES_CALL_PRODUCT_SETTING Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function CopySalesCallProductSetting(ByVal companyName As String, ByVal strUserID As String, ByVal strFromProductID As String, ByVal strToProductID As String) As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                "COPY_POSTSALES_CALL_PRODUCT_SETTING",
                                New Dictionary(Of String, String) From {
                                {"strUserID", strUserID},
                                {"strFromProductID", strFromProductID},
                                {"strToProductID", strToProductID}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI COPY_POSTSALES_CALL_PRODUCT_SETTING Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function AddPostSalesCallProductQuestion(ByVal dtQuestion As DataTable, ByVal userID As String, ByRef isExists As Boolean, Optional companyName As String = "") As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            Dim sbSqlInsert As New Text.StringBuilder()
            Dim strInsertScript As String = String.Empty
            Dim strCondition As String = String.Empty

            sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsn_ProductID").ToString().Trim() & "',")
            sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsn_question_id") & "',")
            sbSqlInsert.AppendLine(IIf(dtQuestion.Rows(0)("cswpsn_order") Is DBNull.Value, "NULL", dtQuestion.Rows(0)("cswpsn_order")) & ",")
            sbSqlInsert.AppendLine("'" & userID & "',")
            sbSqlInsert.AppendLine("GETDATE(),")
            sbSqlInsert.AppendLine("'" & userID & "',")
            sbSqlInsert.AppendLine("GETDATE()")
            strInsertScript = sbSqlInsert.ToString()
            strCondition = " cswpsn_ProductID='" & dtQuestion.Rows(0)("cswpsn_ProductID").ToString().Trim() & "' and cswpsn_question_id = " & dtQuestion.Rows(0)("cswpsn_question_id").ToString().Trim() & " "

            'call API SAVE_POSTSALES_CALL_POLICY_ANSWER
            Dim retDs As DataSet = APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                            "ADD_POSTSALES_CALL_PRODUCT_QUESTION",
                                            New Dictionary(Of String, String) From {
                                            {"strCondition", strCondition},
                                            {"strInsertScript", strInsertScript}
                                            })

            If Not IsNothing(retDs) Then
                If retDs.Tables(0).Rows(0).Item(0).ToString().Equals("1") Then
                    ' Existed
                    isExists = True
                Else
                    ' Not Existed
                    isExists = False
                End If
            End If

            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI ADD_POSTSALES_CALL_PRODUCT_QUESTION Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function RemovePostSalesCallProductQuestion(ByVal companyName As String, ByVal strProductId As String, ByVal intQuestionId As Integer) As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                "REMOVE_POSTSALES_CALL_PRODUCT_QUESTION",
                                New Dictionary(Of String, String) From {
                                {"str_cswpsnProductID", strProductId},
                                {"str_cswpsnQuestionID", intQuestionId}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI REMOVE_POSTSALES_CALL_PRODUCT_QUESTION Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function SetPostSalesCallProductQuestionOrder(ByVal companyCode As String, ByVal strProductId As String, ByVal intQuestionId As Integer, ByVal intNewOrder As Integer, ByVal strUserID As String) As Boolean

        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(companyCode), "SET_POSTSALES_CALL_PRODUCT_QUESTION_ORDER",
                                New Dictionary(Of String, String) From {
                                {"intNewOrder", intNewOrder},
                                {"strUserID", strUserID},
                                {"strProductId", strProductId},
                                {"intQuestionId", intQuestionId}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI SET_POSTSALES_CALL_PRODUCT_QUESTION_ORDER Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function UpdatePostSalesCallProductSetting(ByVal companyCode As String, ByVal dtSetting As DataTable, ByVal strUserID As String) As Boolean

        Try
            'EscapeSqlSingleQuote(dtSetting)

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyCode), "UPDATE_POSTSALES_CALL_PRODUCT_SETTING",
                                New Dictionary(Of String, String) From {
                                {"strCategory", dtSetting.Rows(0)("cswpsd_category")},
                                {"strBenefit", dtSetting.Rows(0)("cswpsd_benefit").ToString().Trim()},
                                {"strPremiumType", dtSetting.Rows(0)("cswpsd_premium_type").ToString().Trim()},
                                {"strLoan", dtSetting.Rows(0)("cswpsd_loan").ToString().Trim()},
                                {"strDividend", dtSetting.Rows(0)("cswpsd_dividend").ToString().Trim()},
                                {"strCoupon", dtSetting.Rows(0)("cswpsd_coupon").ToString().Trim()},
                                {"strSpecialBonus", dtSetting.Rows(0)("cswpsd_special_bonus").ToString().Trim()},
                                {"strCashValue", dtSetting.Rows(0)("cswpsd_cash_value").ToString().Trim()},
                                {"strGuideURL", dtSetting.Rows(0)("cswpsd_guide_url").ToString().Trim()},
                                {"strOthers", dtSetting.Rows(0)("cswpsd_others").ToString().Trim()},
                                {"strHasFees", dtSetting.Rows(0)("cswpsd_has_fees").ToString().Trim()},
                                {"strFees", dtSetting.Rows(0)("cswpsd_fees").ToString().Trim()},
                                {"strNgBenefit", dtSetting.Rows(0)("cswpsd_ng_benefit").ToString().Trim()},
                                {"strSurrCharge", IIf(dtSetting.Rows(0)("cswpsd_surr_charge") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_charge"))},
                                {"strSurrPeriod", IIf(dtSetting.Rows(0)("cswpsd_surr_period") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_period"))},
                                {"strProductTypeEng", dtSetting.Rows(0)("cswpsd_product_type_eng").ToString().Trim()},
                                {"strProductTypeChi", dtSetting.Rows(0)("cswpsd_product_type_chi").ToString().Trim()},
                                {"strProductObjectiveEng", dtSetting.Rows(0)("cswpsd_product_objective_eng").ToString().Trim()},
                                {"strProductObjectiveChi", dtSetting.Rows(0)("cswpsd_product_objective_chi").ToString().Trim()},
                                {"strRiskEng", dtSetting.Rows(0)("cswpsd_risk_eng").ToString().Trim()},
                                {"strRiskChi", dtSetting.Rows(0)("cswpsd_risk_chi").ToString().Trim()},
                                {"strUserID", strUserID},
                                {"strProductId", dtSetting.Rows(0)("cswpsd_ProductID")}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI UPDATE_POSTSALES_CALL_PRODUCT_SETTING Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function AddPostSalesCallProductSetting(ByVal companyCode As String, ByVal dtSetting As DataTable, ByVal strUserID As String) As Boolean
        For Each row As DataRow In dtSetting.Rows
            If IsDBNull(row("cswpsd_has_fees")) OrElse String.IsNullOrEmpty(row("cswpsd_has_fees").ToString()) Then
                row("cswpsd_has_fees") = "N"
            End If
        Next
        Try
            'EscapeSqlSingleQuote(dtSetting)

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyCode), "ADD_POSTSALES_CALL_PRODUCT_SETTING",
                                New Dictionary(Of String, String) From {
                                {"strProductId", dtSetting.Rows(0)("cswpsd_ProductID")},
                                {"strCategory", dtSetting.Rows(0)("cswpsd_category")},
                                {"strBenefit", dtSetting.Rows(0)("cswpsd_benefit").ToString().Trim()},
                                {"strPremiumType", dtSetting.Rows(0)("cswpsd_premium_type").ToString().Trim()},
                                {"strLoan", dtSetting.Rows(0)("cswpsd_loan").ToString().Trim()},
                                {"strDividend", dtSetting.Rows(0)("cswpsd_dividend").ToString().Trim()},
                                {"strCoupon", dtSetting.Rows(0)("cswpsd_coupon").ToString().Trim()},
                                {"strSpecialBonus", dtSetting.Rows(0)("cswpsd_special_bonus").ToString().Trim()},
                                {"strCashValue", dtSetting.Rows(0)("cswpsd_cash_value").ToString().Trim()},
                                {"strGuideURL", dtSetting.Rows(0)("cswpsd_guide_url").ToString().Trim()},
                                {"strOthers", dtSetting.Rows(0)("cswpsd_others").ToString().Trim()},
                                {"strHasFees", dtSetting.Rows(0)("cswpsd_has_fees").ToString().Trim()},
                                {"strFees", dtSetting.Rows(0)("cswpsd_fees").ToString().Trim()},
                                {"strNgBenefit", dtSetting.Rows(0)("cswpsd_ng_benefit").ToString().Trim()},
                                {"strSurrCharge", IIf(dtSetting.Rows(0)("cswpsd_surr_charge") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_charge"))},
                                {"strSurrPeriod", IIf(dtSetting.Rows(0)("cswpsd_surr_period") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_period"))},
                                {"strProductTypeEng", dtSetting.Rows(0)("cswpsd_product_type_eng").ToString().Trim()},
                                {"strProductTypeChi", dtSetting.Rows(0)("cswpsd_product_type_chi").ToString().Trim()},
                                {"strProductObjectiveEng", dtSetting.Rows(0)("cswpsd_product_objective_eng").ToString().Trim()},
                                {"strProductObjectiveChi", dtSetting.Rows(0)("cswpsd_product_objective_chi").ToString().Trim()},
                                {"strRiskEng", dtSetting.Rows(0)("cswpsd_risk_eng").ToString().Trim()},
                                {"strRiskChi", dtSetting.Rows(0)("cswpsd_risk_chi").ToString().Trim()},
                                {"strUserID", strUserID}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI ADD_POSTSALES_CALL_PRODUCT_SETTING Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function IsPostSalesCallProductSettingExists(ByVal companyName As String, ByVal strProductId As String) As Boolean
        Dim blnSuccess = False

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "GET_POSTSALES_CALL_PRODUCT_SETTING_BY_ID",
                                     New Dictionary(Of String, String) From {{"strProductId", strProductId}})
            If retDs.Tables.Count > 0 AndAlso retDs.Tables(0).Rows.Count > 0 Then
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_POSTSALES_CALL_PRODUCT_SETTING_BY_ID Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function GetCoverage(ByVal companyName As String, ByVal strPolicyNo As String, ByRef dtCov As DataTable) As Boolean
        Dim blnSuccess = False
        dtCov = New DataTable
        Dim ds As DataSet = New DataSet()
        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "GET_COVERAGE",
                                     New Dictionary(Of String, String) From {{"strPolicyNo", strPolicyNo}})
            If ds.Tables.Count > 0 Then
                dtCov = ds.Tables(0)
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_COVERAGE Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function RetrievePostSalesCallQuestions(ByVal companyName As String, ByRef dtQues As DataTable) As Boolean
        Dim blnSuccess = False
        dtQues = New DataTable
        Dim ds As DataSet = New DataSet()
        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "RETRIEVE_POSTSALES_CALL_QUESTIONS",
                                     New Dictionary(Of String, String) From {})
            If ds.Tables.Count > 0 Then
                dtQues = ds.Tables(0)
                blnSuccess = True
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI RETRIEVE_POSTSALES_CALL_QUESTIONS Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function DeletePostSalesCallQuestion(ByVal companyName As String, ByVal strQuestionID As String) As Boolean

        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

            APIServiceBL.ExecAPIBusi(getCompanyCode(companyName),
                                "DELETE_POSTSALES_CALL_QUESTION",
                                New Dictionary(Of String, String) From {
                                {"str_cswpsqQuestionID", strQuestionID}
                                })
            Return True

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI DELETE_POSTSALES_CALL_QUESTION Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function GetReportList(ByVal companyName As String, ByRef dtReportList As DataTable) As Boolean
        Dim blnSuccess = False
        dtReportList = New DataTable
        Dim ds As DataSet = New DataSet()
        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "GET_REPORT_LIST",
                                     New Dictionary(Of String, String) From {})
            If ds.Tables.Count > 0 Then
                dtReportList = ds.Tables(0)
                blnSuccess = True
            Else
                MsgBox("Fail to GetReportList : There is no report list can be retrieved. ")
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_REPORT_LIST Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function SavePostSalesCallQuestion(ByVal companyName As String, ByVal userID As String, ByVal intMode As Integer, ByRef dtQuestion As DataTable) As Boolean
        Dim blnSuccess = False

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

        Try
            Select Case intMode
                Case 1
                    Dim ds As DataSet = New DataSet()
                    Dim sbSqlInsert As New Text.StringBuilder()
                    Dim strInsertScript As String = String.Empty

                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_questionnaire_code").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_question_no").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_description").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_answer_type").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_answer_template").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_vc").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_sm").ToString().Trim() & "',")
                    sbSqlInsert.AppendLine("'" & userID & "',")
                    sbSqlInsert.AppendLine("GETDATE(),")
                    sbSqlInsert.AppendLine("'" & userID & "',")
                    sbSqlInsert.AppendLine("GETDATE(),")
                    sbSqlInsert.AppendLine("'" & dtQuestion.Rows(0)("cswpsq_no_fna").ToString().Trim() & "'")
                    strInsertScript = sbSqlInsert.ToString()

                    'call insert API
                    ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "INSERT_POSTSALES_CALL_QUESTION",
                                     New Dictionary(Of String, String) From {
                                     {"strInsertScript", strInsertScript}
                                     })

                    If ds.Tables.Count > 0 Then
                        dtQuestion.Rows(0)("cswpsq_question_id") = ds.Tables(1).Rows(0)(0)
                        blnSuccess = True
                    End If

                Case 2
                    'Dim strCondition As String = String.Empty
                    Dim sbSqlUpdate As New Text.StringBuilder()
                    Dim strUpdateScript As String = String.Empty

                    sbSqlUpdate.AppendLine(" cswpsq_questionnaire_code = '" & dtQuestion.Rows(0)("cswpsq_questionnaire_code").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_question_no = '" & dtQuestion.Rows(0)("cswpsq_question_no").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_description = '" & dtQuestion.Rows(0)("cswpsq_description").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_answer_type = '" & dtQuestion.Rows(0)("cswpsq_answer_type").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_answer_template = '" & dtQuestion.Rows(0)("cswpsq_answer_template").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_vc = '" & dtQuestion.Rows(0)("cswpsq_vc").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_sm = '" & dtQuestion.Rows(0)("cswpsq_sm").ToString().Trim() & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_update_by = '" & userID & "',")
                    sbSqlUpdate.AppendLine(" cswpsq_update_date = GETDATE(),")
                    sbSqlUpdate.AppendLine(" cswpsq_no_fna = '" & dtQuestion.Rows(0)("cswpsq_no_fna").ToString().Trim() & "' ")
                    strUpdateScript = sbSqlUpdate.ToString()
                    'strCondition = " cswpsq_question_id = '" & dtQuestion.Rows(0)("cswpsq_question_id").ToString().Trim() & "'"

                    'call update API
                    APIServiceBL.ExecAPIBusi(getCompanyCode(companyName), "UPDATE_POSTSALES_CALL_QUESTION",
                                     New Dictionary(Of String, String) From {
                                     {"strQuestionID", dtQuestion.Rows(0)("cswpsq_question_id").ToString().Trim()},
                                     {"strUpdateScript", strUpdateScript}
                                     })

                    blnSuccess = True
            End Select

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI SavePostSalesCallQuestion API Retrieve Error. intMode : " & intMode & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try

        Return blnSuccess

    End Function

    Public Function GetSystemInfo(ByVal companyName As String, ByRef dtSystemInfo As DataTable) As Boolean
        Dim blnSuccess = False
        dtSystemInfo = New DataTable
        Dim ds As DataSet = New DataSet()
        Try
            If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "GET_SYSTEM_INFO",
                                     New Dictionary(Of String, String) From {})
            If ds.Tables.Count > 0 Then
                dtSystemInfo = ds.Tables(0)
                blnSuccess = True
            Else
                MsgBox("Fail to GetSystemInfo : There is no system info can be retrieved. ")
            End If

        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_SYSTEM_INFO Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
        Return blnSuccess

    End Function

    Public Function GetPolicyInfo(ByVal companyName As String, ByVal strPolicyNo As String, ByRef dsPolicyInfo As DataSet) As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "GET_POLICY_INFO",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strPolicyNo", strPolicyNo}
                                                            })

            If Not IsNothing(retDs) Then
                dsPolicyInfo = retDs
                Return True
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_POLICY_INFO Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function RetrievePostSalesCallProducts(ByVal companyName As String, ByVal strProductID As String, ByVal strPlanName As String, ByRef dtProduct As DataTable) As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp

        Try
            Dim strCondition As String = String.Empty

            If strPlanName IsNot String.Empty Then
                strCondition = String.Format(" and a.Description like '%{0}%'", strPlanName.Trim())
            End If

            If strProductID IsNot String.Empty Then
                strCondition = String.Format(" and a.ProductID like '%{0}%'", strProductID.Trim())
            End If

            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(companyName), "RETRIEVE_POSTSALES_CALL_PRODUCTS",
                                                            New Dictionary(Of String, String)() From {{"strCondition", strCondition}})

            If Not IsNothing(retDs) Then
                dtProduct = retDs.Tables(0)
                Return True
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI RETRIEVE_POSTSALES_CALL_PRODUCTS Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function GetRptILASNotificationLetterInfo(ByVal companyName As String, ByVal strPolicyNo As String, ByRef dsRptILASNotificationLetterInfo As DataSet) As Boolean

        If String.IsNullOrEmpty(companyName) Then companyName = g_Comp
        Dim retVal As Boolean = False
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyName, "GET_RPT_ILAS_NOTIFICATION_LETTER_INFO",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strPolicyNo", strPolicyNo}
                                                            })

            If Not IsNothing(retDs) Then
                dsRptILASNotificationLetterInfo = retDs
                Return True
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI GET_RPT_ILAS_NOTIFICATION_LETTER_INFO Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
            Return False
        End Try
    End Function

    Public Function GetUatXml(tab As String) As String
        Try
            Dim xmlDoc As XmlDocument = New XmlDocument
            xmlDoc.Load(System.IO.Path.Combine(My.Application.Info.DirectoryPath, "CRSTEST.XML"))
            Dim rootNode As XmlNode = xmlDoc.DocumentElement
            Dim childNode As XmlNode = rootNode.SelectSingleNode(tab)
            Return childNode.InnerXml
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Sub InsertUatLog(msg As String)
        Try
            If GetUatXml("UatLogPath").Length > 0 Then
                System.IO.File.AppendAllText(GetUatXml("UatLogPath").Replace("yyyyMMdd", Now.ToString("yyyyMMdd")), Now.ToString("yyyyMMdd hh:mm:ss.fff") & " " & msg & vbNewLine)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub InsertLocalLog(ByVal message As String)
        Try
            System.IO.File.AppendAllText("C:\Temp\CRS_" + Now.ToString("yyyyMMdd") + ".LOG", Now.ToString("yyyyMMdd hh:mm:ss") + " : " + message + vbNewLine)
        Catch ex As Exception

        End Try
    End Sub
    'oliver 2024-7-31 added for Com 6
    Public Function CheckUserRight(ByVal rightName As String) As Boolean
        Dim dsUserGroup As DataSet = New DataSet
        Dim dtUserGroup As DataTable = New DataTable
        If isHNWMember Then
            Return True
        End If
        Try
            dsUserGroup = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_USER_RIGHT", New Dictionary(Of String, String)() From {{"strUserID", gsUser}})
            If dsUserGroup.Tables.Count > 0 Then
                dtUserGroup = dsUserGroup.Tables(0)
                For i As Integer = 0 To dtUserGroup.Rows.Count - 1
                    If dtUserGroup.Rows(i)("AccessGrp").ToString().Trim = rightName Then
                        isHNWMember = True
                        Return True
                    End If
                Next
            End If
        Catch ex As Exception
            Dim strError As String
            strError = "CRSAPI CHECK_USER_RIGHT Retrieve Error." & vbCrLf & ex.Message
            HandleGlobalException(ex, strError)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Get LCP DB name by <paramref name="companyID"/> and <paramref name="environmentUse"/>, e.g. 'INGLCPU105'
    ''' </summary>
    ''' <remarks>20250303, Copy from LAS</remarks>
    Public Function GetLcpDbName(companyID As String, environmentUse As String)
        Const HK_DB As String = "LCP"
        Const MCU_DB As String = "LCM"

        If environmentUse.Contains("PRD") Then
            Return If(companyID = "MCU", MCU_DB, HK_DB)
        Else
            If companyID = "LAC" OrElse companyID = "LAH" Then
                ' Assurance
                If environmentUse.Contains("U401") OrElse environmentUse.Contains("U402") Then
                    ' Same as Bermuda production
                    Return If(companyID = "MCU", MCU_DB, HK_DB)
                Else
                    Return "LAA" & HK_DB & environmentUse
                End If
            Else
                ' Bermuda
                Return (companyID & HK_DB & environmentUse).Replace("BMU", "ING")
            End If
        End If
    End Function

End Module


