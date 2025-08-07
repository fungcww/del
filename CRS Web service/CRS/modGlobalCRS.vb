''********************************************************************
'' Amend By:     Lawrence Fung
'' Date:         04 Feb 2025
'' Changes:      CRS TABLE RELOCATION(PolicyEstatement)
''********************************************************************

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



Module Module1

    '    '**** Load test
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
    'Public wndMain As frmCS2005_Asur = New frmCS2005_Asur
    'Public wndMain As New frmCS2005

    'Public wndInbox As frmInbox

    ' .NET Remoting object
#If HKL = 1 Then
        Public objSec As HKL.Interface.ISecurity
        Public objCS As HKL.Interface.ICRS
#Else
    'Public objSec As INGLife.Interface.ISecurity
    Public objCS As INGLife.Interface.ICS2005
    'Public wsCRS As CRSWS.CRSWS
    'Public objCS As INGLife.CS2005.CS2005

#End If

    'Public objSSO As ISSOCom

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

    'AC, Change not to use advance compilation option - start
    Public gUAT As Boolean = False
End Module