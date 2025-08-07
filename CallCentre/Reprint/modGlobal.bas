Attribute VB_Name = "modGlobal"
Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long
Public Const MPF = "mpf"
Public Const LIFE = "csw"
Public Const PROJECT = "CTR"
Public strUserID As String

Public Function GetNTUserName() As String
On Error Resume Next
Dim UserName As String
Dim slength As Long
Dim retval As Long

    UserName = Space(255)
    slength = 255
    retval = GetUserName(UserName, slength)
    GetNTUserName = Left(UserName, slength - 1)
End Function
Public Function GetEorR(ByVal strCustID As String, dbsec As Dblogon.Dbconnect) As String
Dim strsql As String
strsql = "select mpfpid_pin_flag from mpf_pin_details where mpfpid_cid = '" & strCustID & "'"
GetEorR = dbsec.ExecuteStatement(strsql).Fields(0).Value
End Function
Public Function ChooseConnection(ByVal Pstrdbc As String) As String
On Error GoTo errorHandler
 Select Case Pstrdbc
        Case "ING"
            ChooseConnection = "CTRMPFAETSQLCON"
        Case "DRESDNER"
            ChooseConnection = "CTRMPFDRESQLCON"
        Case "SCHRODER"
            ChooseConnection = "CTRMPFSCHSQLCON"
        Case "LIFE"
            ChooseConnection = "CTRSQLCON"
 End Select
Exit Function
errorHandler:
    MsgBox Err.Description
End Function
