VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 3  'UsesTransaction
END
Attribute VB_Name = "CIWClsCCtr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'*****************************************************************************
'        Call Centre Class (CIWClsCCtr)
'*****************************************************************************
' Description :  ( CCtr )
'
' Date        : 18 May 2000
' Author      : Ricky Leung, Wong Wing Chung
' Reference   : Database Logon Module
'             : Microsoft Transaction Server Type Library
'             : Microsoft ActiveX Data Objects 2.1 Library
'*****************************************************************************
' Amended by : Anson Chan
' Date : Apr 17 2001
' Description : - Add new function
'                 MPFGetSchemeType ( Identify the MPF/ORSO Scheme )
'                 MPFGetForfeiture ( Get the Forfeiture Balance )
'                 MPFGetExchangeRate ( Get the exchange rate )
'
'               - Modify existing function
'                 MPFGetFundPrice ( retrieve from SPAN table instead of RPC,
'                                   separate from MPF/ORSO & currency is plus
' Ref : AC01
'*****************************************************************************
' Amended by : Anson Chan
' Date : Aug 24 2001
' Description : - Add new function
'                 MPFGetSecGroup ( Retrieve the User Security Group )
'                 MPFGetFaxList ( Retrieve the fax list )
'
'               - Modify existing function
'                 MPFValidCustID ( Get the security group & lookup the ORSO list table )
'
' Ref : AC02
'*****************************************************************************
' Amended by : Anson Chan
' Date : Nov 19 2001
' Description : - Add new function
'                 LIFEGetBillNo ( Get PPS Number )
'                 LIFEGetNBItem ( List out all New Business Outstanding Item )
'                 MPFChkDBScheme ( Check whether the Defined Benefit Scheme )
'
'               - Modify existing function
'                 MPFGetFaxList  ( List out all available fax )
' Ref : AC03
'*****************************************************************************
' Amended by : Eammon Chen            Ref : EC01            Date : May 02 2002
' Description : - Filter MPFListAcctByCust function when Mbr type <> 'E'
'
'*****************************************************************************
' Amended by : Eammon Chen            Ref : EC02            Date : Aug 04 2002
' Description : - Bug fix for MPFGetFundPrice
'
'*****************************************************************************
' Amended by : Eammon Chen            Ref : EC03            Date : Sep 05 2002
' Description : - No GI information (AuthPoEnqByRole and ListPoByRole)
'
'*****************************************************************************

Option Explicit
Dim hServer As Long
Dim lngrst As Long
Const MPFCONTRIBUTIONFIELDSEGMENT = 8
Const MPFUNITCONFIELDSEGMENT = 15
Const GETFUNDTOTFNUM = 7
Const GETFUNDUNITTOTFNUM = 28
Const GETBILLDETAILFNUM = 10
Const GETBILLDETAILTOTFNUM = 10
Const GETFUNDUNITCONTFNUM = 49
Const GETFUNDPERCENTFNUM = 4
Public Function ValidCustId(ByVal PstrCustID As String, ByVal Pstrpin As String, _
                            ByVal PstrGroup As String, ByVal Pstruserid As String) As Boolean
Dim dbcon As New Dblogon.Dbconnect
Dim rstCust As New ADODB.Recordset
Dim strSQL As String
On Error GoTo ErrHandler

If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then   'get connected
    strSQL = "exec secsp_lock_flag '" & PstrCustID & "','" & Pstrpin & "', 'EIC'"
    Set rstCust = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    If Trim(rstCust.Fields(0).Value) = "Login" Then
        strSQL = "declare @message char(50);exec secsp_chk_user_in_group '" & PstrCustID & "','" & PstrGroup & "',@message output ; select @message"
        Set rstCust = Nothing
        Set rstCust = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        If Trim(rstCust.Fields(0).Value) = "" Then
            ValidCustId = True
        Else
            ValidCustId = False
        End If
    Else
        ValidCustId = False
    End If
Else
    ValidCustId = False
End If
rstCust.Close
dbcon.Disconnect
Set rstCust = Nothing
Set dbcon = Nothing

Exit Function
ErrHandler:
    Set rstCust = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    ValidCustId = False
End Function
Public Function AuthPoEnq(ByVal Pdblcustid As Double, ByVal Pstrpolno As String, Pstruserid As String) As Boolean
On Error GoTo ErrHandler
AuthPoEnq = False
If AuthPoEnqByRole(Pdblcustid, Pstrpolno, "SA", Pstruserid) Then
    AuthPoEnq = True
ElseIf AuthPoEnqByRole(Pdblcustid, Pstrpolno, "PH", Pstruserid) Then
    AuthPoEnq = True
End If
Exit Function
ErrHandler:
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    AuthPoEnq = False
End Function
Public Function AuthPoEnqByRole(ByVal Pdblcustid As Double, ByVal Pstrpolno As String, PstrRole As String, Pstruserid As String) As Boolean
Dim dbcon As New Dblogon.Dbconnect
Dim rstPolr As New ADODB.Recordset
Dim strSQL As String
On Error GoTo ErrHandler
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then   'get connected
    
    '***EC03 Create - Only Life Case Exist ***
    'strSQL = "select CustomerId from csw_poli_rel where customerid = " + CStr(Pdblcustid) + " and PolicyAccountId = '" + Pstrpolno + "' and PolicyRelateCode = '" + PstrRole + "'"
    strSQL = "select a.CustomerId from csw_poli_rel a, policyaccount b where a.customerid = " + CStr(Pdblcustid) + " and a.PolicyAccountId = b.PolicyAccountId and b.companyID = 'EAA' and a.PolicyAccountId = '" + Pstrpolno + "' and a.PolicyRelateCode = '" + PstrRole + "'"
    '***EC03 End
    Set rstPolr = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    If rstPolr.RecordCount > 0 Then
        AuthPoEnqByRole = True
    Else
        AuthPoEnqByRole = False
    End If
End If
rstPolr.Close
dbcon.Disconnect
Set rstPolr = Nothing
Set dbcon = Nothing
Exit Function
ErrHandler:
    Set rstPolr = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    AuthPoEnqByRole = False
End Function
Public Function PoStatus(ByVal Pstrpolno As String, ByVal Pstruserid As String) As ADODB.Recordset
On Error GoTo ErrHandler
Dim dbcon As New Dblogon.Dbconnect
Dim strSQL As String

If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then   'get connected
    strSQL = "select AccountStatusCode, PaidToDate, ProductId, DATEDIFF(day, paidtodate, getdate()) AS DayDiff from policyaccount where policyaccountid = '" & Pstrpolno & "'"
    Set PoStatus = dbcon.ExecuteStatement(strSQL)
    If Mid(PoStatus.Fields(2).Value, 1, 4) = "_CBS" Then
        strSQL = "select 'CB' as AccountStatusCode, '01/01/1900' as PaidToDate"
    Else
        If Mid(PoStatus.Fields(2).Value, 1, 3) = "HIL" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "HRILT" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "HRILB" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "HRILD" Or _
           Mid(PoStatus.Fields(2).Value, 1, 3) = "UIL" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "URILT" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "URILB" Or _
           Mid(PoStatus.Fields(2).Value, 1, 5) = "URILD" Or _
           Mid(PoStatus.Fields(2).Value, 2, 2) = "YC" Or _
           Mid(PoStatus.Fields(2).Value, 3, 2) = "YC" Or _
           Mid(PoStatus.Fields(2).Value, 2, 2) = "DC" Or _
           Mid(PoStatus.Fields(2).Value, 3, 2) = "DC" Or _
           Mid(PoStatus.Fields(2).Value, 2, 2) = "GC" Or _
           Mid(PoStatus.Fields(2).Value, 3, 2) = "GC" Then
            strSQL = "select 'IL' as AccountStatusCode, '01/01/1900' as PaidToDate"
        Else
            If PoStatus.Fields(3).Value > 30 Then
                If PoStatus.Fields(0).Value = "1" Or PoStatus.Fields(0).Value = "2" Or _
                    PoStatus.Fields(0).Value = "3" Or PoStatus.Fields(0).Value = "4" Or _
                    PoStatus.Fields(0).Value = "V" Or PoStatus.Fields(0).Value = "X" Then
                    strSQL = "select '" & PoStatus.Fields(0).Value & "' as AccountStatusCode, '" & PoStatus.Fields(1).Value & "' as PaidToDate"
                Else
                    strSQL = "select 'S' as AccountStatusCode, '" & PoStatus.Fields(1).Value & "' as PaidToDate"
                End If
            Else
                If PoStatus.Fields(0).Value = "0" Or PoStatus.Fields(0).Value = "9" Or _
                    PoStatus.Fields(0).Value = "8" Or PoStatus.Fields(0).Value = "7" Or _
                    PoStatus.Fields(0).Value = "6" Or PoStatus.Fields(0).Value = "5" Then
                    'strSQL = "select '" & PoStatus.Fields(0).Value & "' as AccountStatusCode, NULL as PaidToDate"
                    strSQL = "select '" & PoStatus.Fields(0).Value & "' as AccountStatusCode, '01/01/1900' as PaidToDate"
                Else
                    strSQL = "select '" & PoStatus.Fields(0).Value & "' as AccountStatusCode, '" & PoStatus.Fields(1).Value & "' as PaidToDate"
                End If
            End If
        End If
    End If
    Set PoStatus = Nothing
    Set PoStatus = dbcon.ExecuteStatement(strSQL)
End If
dbcon.Disconnect
Set dbcon = Nothing
Exit Function
ErrHandler:
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    Set PoStatus = Nothing
    Set dbcon = Nothing
End Function
Public Function PoModPre(ByVal Pstrpolno As String, ByVal Pstruserid As String) As ADODB.Recordset
On Error GoTo ErrHandler
Dim dbcon As New Dblogon.Dbconnect
Dim strSQL As String
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then   'get connected
    strSQL = "select Mode, PolicyCurrency, ModalPremium from policyaccount where policyaccountid = '" + Pstrpolno + "'"
    Set PoModPre = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
End If
dbcon.Disconnect
Set dbcon = Nothing
Exit Function
ErrHandler:
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    Set PoModPre = Nothing
    Set dbcon = Nothing
End Function
Public Function PoAutoPayDay(ByVal Pstrpolno As String, ByVal Pstruserid As String) As String
On Error GoTo ErrHandler
Dim dbcon As New Dblogon.Dbconnect
Dim rstPO As New ADODB.Recordset
Dim rstDDA As New ADODB.Recordset
Dim rstCCDR As New ADODB.Recordset
Dim strSQL As String

If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then   'get connected

    strSQL = "SELECT PolicyAccountID FROM PolicyAccount WHERE PolicyAccountID = '" & Pstrpolno & "' AND ( BillingType = '4' OR BillingType = '5' )"
    Set rstPO = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    If rstPO.RecordCount > 0 Then
        strSQL = "select ddadrawdate, DDAEffectiveDate from dda where policyaccountid = '" & Pstrpolno & "' order by DDASeqNo DESC"
        Set rstDDA = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        strSQL = "select CCDRdrawdate,CCDREffectiveDate from CCDR where policyaccountid = '" + Pstrpolno + "' order by CCDRSeqNo DESC"
        If rstDDA.RecordCount <> 0 Then
            If IsNull(rstDDA.Fields(1).Value) Or Format(rstDDA.Fields(1).Value, "yyyy-mm-dd") < Format(Date, "yyyy-mm-dd") Then
                PoAutoPayDay = rstDDA.Fields("ddadrawdate").Value
                strSQL = "select CCDRdrawdate,CCDREffectiveDate from CCDR where policyaccountid = '" + Pstrpolno + "' and CCDRdrawdate > '" & rstDDA.Fields("ddadrawdate").Value & "' order by CCDRSeqNo DESC"
            End If
        End If
        Set rstCCDR = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        If rstCCDR.RecordCount <> 0 Then
            If rstCCDR.Fields(1).Value < Date Or IsNull(rstCCDR.Fields(1).Value) Then
                PoAutoPayDay = rstCCDR.Fields(0).Value
            Else
                If rstDDA.RecordCount = 0 Then
                    PoAutoPayDay = "00"
                End If
            End If
        Else
            If rstDDA.RecordCount = 0 Then
                PoAutoPayDay = "00"
            End If
        End If
    Else
        PoAutoPayDay = "00"
    End If
    
End If
rstDDA.Close
Set rstDDA = Nothing
rstCCDR.Close
Set rstCCDR = Nothing
dbcon.Disconnect
Set dbcon = Nothing
Exit Function
ErrHandler:
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    PoAutoPayDay = "00"
    Set rstDDA = Nothing
    Set rstCCDR = Nothing
    Set dbcon = Nothing
End Function
Public Function ChangePIN(ByVal PstrCustID As String, ByVal Pstropin As String, _
                          ByVal Pstrnpin As String, ByVal Pstrtype As String, _
                          ByVal PstrGroup As String, ByVal Pstruserid As String) As Boolean
On Error GoTo ErrHandler
Dim strSQL As String                   'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
Dim rsdbcon As New ADODB.Recordset   'Recordset for SQL connection
If Pstrtype = "C" Then
    If ValidCustId(PstrCustID, Pstropin, PstrGroup, Pstruserid) Then
        strSQL = "declare @message char(50);exec secsp_upd_password '" & PstrCustID & "','" & Pstropin & "','" & Pstrnpin & "',@message output;select @message"
        If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then
            Set rsdbcon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            If Trim(rsdbcon.Fields(0).Value) = "" Then
                ChangePIN = True
            Else
                ChangePIN = False
            End If
            rsdbcon.Close
        Else
            ChangePIN = False
        End If
        dbcon.Disconnect
    Else
        ChangePIN = False
    End If
Else
    If ChgAgentPIN(PstrCustID, Pstropin, Pstrnpin, PstrGroup, Pstruserid) Then
        ChangePIN = True
    Else
        ChangePIN = False
    End If
End If
Set rsdbcon = Nothing
Set dbcon = Nothing
Exit Function
ErrHandler:
    ChangePIN = False
    Set rsdbcon = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function ListPoByRole(ByVal Pdblcustid As Double, _
                           ByVal PstrRole As String, _
                           ByVal Pstruserid As String) As ADODB.Recordset
On Error GoTo ErrHandler
Dim strSQL As String                    'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection

'***EC03 Create - Only List Case Exists
'strSQL = "select PolicyAccountID from csw_poli_rel where " & _
'         "CustomerID =" & Pdblcustid & " and PolicyRelateCode = '" & PstrRole & "'"
strSQL = "select a.PolicyAccountID from csw_poli_rel a, policyaccount b where " & _
         "a.CustomerID =" & Pdblcustid & " and a.policyaccountID = b.policyaccountID and b.companyID = 'EAA' and a.PolicyRelateCode = '" & PstrRole & "'"
'***EC03 End
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
    Set ListPoByRole = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
Else
    GoTo ErrHandler
End If
dbcon.Disconnect
Set dbcon = Nothing
Exit Function
ErrHandler:
    Set ListPoByRole = Nothing
    dbcon.Disconnect
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function ValidPo(ByVal Pstrpolno As String, _
                        ByVal Pstruserid As String) As Boolean
On Error GoTo ErrHandler
Dim strSQL As String                    'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
Dim rsdbcon As New ADODB.Recordset   'Recordset for SQL connection
strSQL = "select PolicyAccountID from PolicyAccount where " & _
         "PolicyAccountID = '" & Pstrpolno & "'"
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
    Set rsdbcon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    If rsdbcon.RecordCount = 0 Then
        ValidPo = False
    Else
        ValidPo = True
    End If
    rsdbcon.Close
Else
    GoTo ErrHandler
End If
dbcon.Disconnect
Set rsdbcon = Nothing
Set dbcon = Nothing
Exit Function
ErrHandler:
    ValidPo = False
    Set rsdbcon = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function ValidAgent(ByVal PstrAgtcd As String, ByVal Pstrpin As String, _
                           ByVal PstrGroup As String, ByVal Pstruserid As String) As String
On Error GoTo ErrHandler
Dim strSQL As String                    'String for SQL statement
Dim objdbcon As New Dblogon.Dbconnect
Dim rsdbcon As New ADODB.Recordset   'Recordset for SQL connection
    If ValidCustId(PstrAgtcd, Pstrpin, PstrGroup, Pstruserid) Then
        If objdbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
            Set rsdbcon = Nothing
            strSQL = "select CustomerID from Customer where AgentCode='" & Mid(PstrAgtcd, 1, 5) & "'"
            Set rsdbcon = objdbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            If rsdbcon.RecordCount <> 0 Then
                ValidAgent = rsdbcon.Fields("CustomerID").Value
            Else
                ValidAgent = "0"
            End If
                rsdbcon.Close
        Else
            GoTo ErrHandler
        End If
        objdbcon.Disconnect
    Else
        ValidAgent = "0"
    End If
Set rsdbcon = Nothing
Set objdbcon = Nothing
Exit Function
ErrHandler:
    ValidAgent = "0"
    Set rsdbcon = Nothing
    Set objdbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function GetLatestExRate(ByVal PstrFromCty As String, _
                                ByVal PstrToCty As String, _
                                ByVal Pstruserid As String) As Double
On Error GoTo ErrHandler
Dim strSQL As String                    'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
Dim rsdbcon As New ADODB.Recordset   'Recordset for SQL connection
    strSQL = "select csw_exchange_rate from csw_exchange_rate" & _
             " where csw_from_curr='" & PstrFromCty & "' and csw_to_curr='" & _
             PstrToCty & "' order by csw_effective_date desc"
    If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
        Set rsdbcon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        If Not rsdbcon.EOF Then
            GetLatestExRate = rsdbcon.Fields(0).Value
        Else
            GetLatestExRate = 0
        End If
        rsdbcon.Close
        Set rsdbcon = Nothing
    Else
        GoTo ErrHandler
    End If
    dbcon.Disconnect
    Set rsdbcon = Nothing
    Set dbcon = Nothing
Exit Function
ErrHandler:
    GetLatestExRate = 0
    Set rsdbcon = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function Dummy(ByVal Pstruserid As String)
On Error GoTo ErrHandler
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
    dbcon.Disconnect
    Dummy = True
Else
    Dummy = False
End If
If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then
    dbcon.Disconnect
    Dummy = True
Else
    Dummy = False
End If
Set dbcon = Nothing
Exit Function
ErrHandler:
    Dummy = False
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function ChgAgentPIN(ByVal PstrCustID As String, ByVal Pstropin As String, _
                            ByVal Pstrnpin As String, ByVal PstrGroup As String, _
                            ByVal Pstruserid As String) As Boolean
On Error GoTo ErrHandler
Dim strSQL As String                   'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
Dim objsecdb As New Dblogon.Dbconnect
Dim rsdbcon As New ADODB.Recordset   'Recordset for SQL connection

    

    If ValidCustId(PstrCustID, Pstropin, PstrGroup, Pstruserid) Then
        strSQL = "declare @message char(50);exec secsp_upd_password '" & PstrCustID & "','" & Pstropin & "','" & Pstrnpin & "',@message output;select @message"
        
        
        If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then
            Set rsdbcon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            
            
            If Trim(rsdbcon.Fields(0).Value) = "" Then
                    If objsecdb.Connect(Pstruserid, PROJECT, CTRSQL) Then
                        strSQL = "insert csw_pwd_sync(cswpsy_cid,cswpsy_opwd,cswpsy_npwd,cswpsy_type,cswpsy_mode,cswpsy_upd_date) values('" & PstrCustID & "','" & Pstropin & "','" & Pstrnpin & "','A','U',getdate())"
                        Call objsecdb.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
                        ChgAgentPIN = True
                        objsecdb.Disconnect
                    Else
                        ChgAgentPIN = False
                    End If
            Else
                ChgAgentPIN = False
            End If
            rsdbcon.Close
        Else
            ChgAgentPIN = False
        End If
        dbcon.Disconnect
    Else
        ChgAgentPIN = False
    End If
Set rsdbcon = Nothing
Set dbcon = Nothing
Set objsecdb = Nothing
Exit Function
ErrHandler:
    ChgAgentPIN = False
    Set rsdbcon = Nothing
    Set dbcon = Nothing
    Set objsecdb = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFValidCustID(ByVal PstrCustID As String, ByVal Pstrdbc As String, _
                               ByVal Pstrpin As String, ByRef PstrGroup As String, _
                               ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As Boolean
On Error GoTo errorhandler:
    Pblnrtncode = True
    Dim dbcon As New Dblogon.Dbconnect
    Dim dbORSOcon As New Dblogon.Dbconnect
    Dim rs As New ADODB.Recordset
    Dim strSQL As String
    Dim strGroup As String
    
    Pblnrtncode = False
    MPFValidCustID = False
    
    If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then   'get connected
        strSQL = "declare @message char(50);exec secsp_lock_flag '" & PstrCustID & "','" & Pstrpin & "', 'EIC'"
        Set rs = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        If Trim(rs.Fields(0).Value) = "Login" Then
            '****** AC02 START ******
            'Check with the security group
            strSQL = "Select secsug_group_id from sec_user_group where secsug_user_id = '" & Trim(PstrCustID) & "' "
            Set rs = Nothing
            Set rs = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            If rs.EOF Then
                GoTo errorhandler
            Else
                strGroup = Trim(rs(0).Value)
                If dbORSOcon.Connect(Pstruserid, PROJECT, FindGroupConnection(strGroup)) Then
                    'Look up the account
                    strSQL = "Select distinct mpfpid_fund From mpf_pin_details Where mpfpid_cid = '" & PstrCustID & "' "
                    Set rs = dbORSOcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
                    
                    If rs.EOF Then
                        GoTo errorhandler
                    Else
                        'Look up the special field (Fund Level)
                        strSQL = "Select special_fields from mpf_fund_special_fields where fund_code = '" & Trim(rs(0)) & "' and table_index = '3'"
                        Set rs = dbORSOcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
                        
                        If rs.EOF Then
                            GoTo errorhandler
                        Else
                            If InStr(1, UCase(PstrGroup), "CTR_MPF") And _
                            UCase(Trim(rs(0))) = "ING ORSO" And _
                            PstrGroup = strGroup Then
                                Pblnrtncode = True
                                MPFValidCustID = True
                            ElseIf InStr(1, UCase(PstrGroup), "CTR_MPF") And _
                            UCase(Trim(rs(0))) <> "ING ORSO" And _
                            PstrGroup = strGroup Then
                                Pblnrtncode = True
                                MPFValidCustID = True
                            ElseIf InStr(1, UCase(PstrGroup), "CTR_MPF") And _
                            UCase(Trim(rs(0))) = "ING ORSO" And _
                            PstrGroup <> strGroup Then
                                PstrGroup = strGroup
                                Pblnrtncode = True
                                MPFValidCustID = True
                            ElseIf Not InStr(1, UCase(PstrGroup), "CTR_MPF") And _
                            UCase(Trim(rs(0))) <> "ING ORSO" And _
                            PstrGroup = strGroup Then
                                Pblnrtncode = True
                                MPFValidCustID = True
                            End If
                        End If
                    End If
                Else
                    GoTo errorhandler
                End If
            End If
'AC02            strSQL = "declare @message char(50);exec secsp_chk_user_in_group '" & PstrCustID & "','" & PstrGroup & "',@message output ; select @message"
'AC02            Set rs = Nothing
'AC02            Set rs = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
'AC02            If Trim(rs.Fields(0).Value) = "" Then
'AC02                MPFValidCustID = True
'AC02            Else
'AC02                MPFValidCustID = False
'AC02            End If
            '****** AC02 END ******
        Else
            MPFValidCustID = False
        End If
    Else
        GoTo errorhandler
    End If
    Set rs = Nothing
    Set dbcon = Nothing
end_fun:
    Exit Function
errorhandler:
    Pblnrtncode = False
    MPFValidCustID = False
    Set rs = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function MPFChangePIN(ByVal PstrCustID As String, ByVal Pstrdbc As String, _
                             ByVal Pstroldpin As String, ByVal Pstrnewpin As String, _
                             ByVal PstrGroup As String, _
                             ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As Boolean

On Error GoTo errorhandler:

    Pblnrtncode = True
    Dim dbcon As New Dblogon.Dbconnect
    Dim dbcon1 As New Dblogon.Dbconnect
    Dim rs As New ADODB.Recordset
    Dim rs1 As New ADODB.Recordset
    Dim strSQL As String
    Dim Pblnresult As Boolean
    
    If MPFValidCustID(PstrCustID, Pstrdbc, Pstroldpin, PstrGroup, Pstruserid, Pblnresult) Then
        strSQL = "declare @message char(50);exec secsp_upd_password '" & PstrCustID & "','" & Pstroldpin & "','" & Pstrnewpin & "',@message output;select @message"
        If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then
            Set rs = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            If Trim(rs.Fields(0).Value) = "" Then
                MPFChangePIN = True
            Else
                MPFChangePIN = False
            End If
            rs.Close
        Else
            GoTo errorhandler
        End If
        dbcon.Disconnect
    Else
        MPFChangePIN = False
    End If
    Set rs = Nothing
    Set rs1 = Nothing
    Set dbcon = Nothing
    Set dbcon1 = Nothing
end_fun:
    Exit Function
errorhandler:
    MPFChangePIN = False
    Pblnrtncode = False
    Set rs = Nothing
    Set rs1 = Nothing
    Set dbcon = Nothing
    Set dbcon1 = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFListAcctByCust(ByVal PstrCustID As String, ByVal Pstrdbc As String, _
                                  ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:
    Pblnrtncode = True
    Dim dbcon As New Dblogon.Dbconnect
    Dim strSQL As String
    
    '***EC01 Create - Check the Mbr Type <> Exit
    'strSQL = "select (rtrim(ltrim(mpfpid_fund))+rtrim(ltrim(mpfpid_emp))+rtrim(ltrim(mpfpid_rpc))) as mpfacctno, mpfpid_cid, mpfpid_dbcode, mpfpid_fund, mpfpid_emp, mpfpid_rpc, " & _
    '         "mpfpid_pin_flag, mpfpid_sqec_no, mpfpid_name1, mpfpid_name2, mpfpid_Payroll_nbr, mpfpid_mbr_num, mpfpid_mbr_dob, mpfpid_mbr_sex, " & _
    '         "mpfpid_mbr_type, mpfpid_date_time, mpfpid_exit_days from mpf_pin_details where mpfpid_cid='" & PstrCustID & "'"
    strSQL = "select (rtrim(ltrim(mpfpid_fund))+rtrim(ltrim(mpfpid_emp))+rtrim(ltrim(mpfpid_rpc))) as mpfacctno, mpfpid_cid, mpfpid_dbcode, mpfpid_fund, mpfpid_emp, mpfpid_rpc, " & _
             "mpfpid_pin_flag, mpfpid_sqec_no, mpfpid_name1, mpfpid_name2, mpfpid_Payroll_nbr, mpfpid_mbr_num, mpfpid_mbr_dob, mpfpid_mbr_sex, " & _
             "mpfpid_mbr_type, mpfpid_date_time, mpfpid_exit_days from mpf_pin_details where mpfpid_cid='" & PstrCustID & "' and mpfpid_mbr_type <> 'E' "
    '***EC01 End
    
    If dbcon.Connect(Pstruserid, PROJECT, FindConnection(Pstrdbc)) Then
        Set MPFListAcctByCust = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    Else
        Pblnrtncode = False
        Set MPFListAcctByCust = Nothing
    End If
    Set dbcon = Nothing
end_fun:
    Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFListAcctByCust = Nothing
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    Resume end_fun
End Function

Public Function MPFGetFundPrice(ByVal PstrODBC As String, _
                                ByVal PstrODBCloginID As String, _
                                ByVal PstrODBCpwd As String, _
                                ByVal Pstruserid As String, _
                                ByVal PstrFundCode As String, _
                                ByVal PstrEmpCode As String, _
                                ByVal PstrRC As String, _
                                ByVal PstrClass As String, _
                                ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:

'****** AC01 BEGIN ******
Pblnrtncode = True
'Dim strDetails As String
'Dim returnValue As Integer
'Dim varResult As Variant
'Dim varRecord() As New Collection
'Dim hServer As Long
'Dim lngrst As Long
'Dim intNoOFRecord As Integer
'Dim i, j As Integer
Dim strSQL As String
Dim strLink As String
Dim dbcon As New Dblogon.Dbconnect
'Dim strsql1 As String
Dim rs As New ADODB.Recordset
Dim rsLink As New ADODB.Recordset
Dim rsLink2 As New ADODB.Recordset

'// Get information from SQL instead of RPC & SPAN tables, plus Currency included
Dim Mpfcon As DAO.Connection
Dim Mpfws As DAO.Workspace
Dim Mbdset As DAO.Recordset
Dim strInvestCode As String
        
'***EC02 Create
Dim tempInvestCode As String
Dim strListInvestCd As String
Dim rs1 As New ADODB.Recordset
Dim intCount As Integer
Dim i As Integer
intCount = 0
i = 1
'***EC02 End

        Set Mpfws = CreateWorkspace("", PstrODBCloginID, "", dbUseODBC)
        Set Mpfcon = Mpfws.OpenConnection(Trim(PstrODBC), dbDriverNoPrompt, True, "ODBC;UID=" + UCase(PstrODBCloginID) + ";PWD=" + PstrODBCpwd)
        Mpfcon.QueryTimeout = 3000
     
        '***EC02 Create
        strInvestCode = ""
        If dbcon.Connect(Pstruserid, PROJECT, FindConnection(UCase(PstrODBC))) Then
            strLink = "SELECT mpfinv_code ,mpfinv_class FROM mpf_investment WHERE mpfinv_class = '" & Trim(PstrClass) & "' "
            Set rs = dbcon.ExecuteStatement(strLink)
            Set rs1 = dbcon.ExecuteStatement(strLink)
            
            If Not rs1.EOF Then
                tempInvestCode = ""
                intCount = rs1.RecordCount
                While Not rs1.EOF
                   tempInvestCode = Trim(rs1!mpfinv_code)
                   If intCount = i Then
                        strListInvestCd = strListInvestCd + "'" + tempInvestCode + "'"
                   Else
                       strListInvestCd = strListInvestCd + "'" + tempInvestCode + "'" + ","
                   End If
                   i = i + 1
                   rs1.MoveNext
                Wend
            End If
       End If
        
       If Not rs.EOF Then
            If InStr(1, UCase(PstrODBC), "DRESD") > 0 Or InStr(1, UCase(PstrODBC), "SCHRD") > 0 Then
                strLink = "SELECT b.investment_code, b.description, a.effective_date, a.sell_price, c.currency_code " & _
                          "FROM span_super.unit_price_details a, span_super.investment_details b, span_super.currency_definition c  " & _
                          "WHERE a.investment_no = b.investment_no " & _
                          "AND b.currency_no = c.currency_no " & _
                          "AND b.investment_code IN (" & strListInvestCd & ") " & _
                          "ORDER BY b.investment_code, a.effective_to DESC "
                          Set Mbdset = Mpfcon.OpenRecordset(strLink, dbOpenDynamic)
            Else
                strLink = "SELECT b.investment_code, b.description, a.effective_date, a.sell_price, c.currency_code " & _
                          "FROM span_super.unit_price_details a, span_super.investment_details b, span_super.currency_definition c  " & _
                          "WHERE a.investment_no = b.investment_no " & _
                          "AND b.currency_no = c.currency_no " & _
                          "AND b.investment_code IN (" & strListInvestCd & ") " & _
                          "ORDER BY b.investment_code, a.effective_to DESC "
                          Set Mbdset = Mpfcon.OpenRecordset(strLink, dbOpenDynamic)
            End If
       End If
       '***EC02 End
        
       '***EC02 Create
       'If InStr(1, UCase(PstrODBC), "DRESD") > 0 Or InStr(1, UCase(PstrODBC), "SCHRD") > 0 Then
       '         strLink = "SELECT b.investment_code, b.description, a.effective_to, a.sell_price, c.currency_code " & _
       '                   "FROM span_super.unit_price_details a, span_super.investment_details b, span_super.currency_definition c  " & _
       '                   "WHERE a.investment_no = b.investment_no " & _
       '                   "AND b.currency_no = c.currency_no " & _
       '                   "ORDER BY b.investment_code, a.effective_to DESC "
       '     Else
       '         strLink = "SELECT b.investment_code, b.description, a.effective_date, a.sell_price, c.currency_code " & _
       '                   "FROM span_super.unit_price_details a, span_super.investment_details b, span_super.currency_definition c  " & _
       '                   "WHERE a.investment_no = b.investment_no " & _
       '                   "AND b.currency_no = c.currency_no " & _
       '                   "ORDER BY b.investment_code, a.effective_to DESC "
       ' End If
       ' Set Mbdset = Mpfcon.OpenRecordset(strLink, dbOpenDynamic)
        
       ' If Not Mbdset.EOF Then
       '     strInvestCode = ""
       '     If dbcon.Connect(Pstruserid, PROJECT, FindConnection(UCase(PstrODBC))) Then
       '         strLink = "SELECT mpfinv_code ,mpfinv_class FROM mpf_investment WHERE mpfinv_class = '" & Trim(PstrClass) & "' "
       '        Set rs = dbcon.ExecuteStatement(strLink)
       '     End If
       ' End If
       '***EC01 End

        strSQL = "SELECT "
        While Not Mbdset.EOF
            If strInvestCode <> Trim(Mbdset(0).Value) Then
                While Not rs.EOF
                    If Trim(rs(0).Value) = Trim(Mbdset(0).Value) Then
                        If Trim(rs(1).Value) = "O" Then
                            'strLink = "SELECT * " & _
                                     "FROM mpf_investment_lnk " & _
                                     "WHERE mpfifl_invest_code = '" & Trim(rs(0).Value) & "' "
                            strLink = "SELECT * FROM mpf_employer_fund_Link " & _
                                      "WHERE mpfefl_fund_code = '" & Trim(PstrFundCode) & "' " & _
                                      "AND mpfefl_emp_code = '" & Trim(PstrEmpCode) & "' " & _
                                      "AND mpfefl_rc = '" & Trim(PstrRC) & "' " & _
                                      "AND mpfefl_invest_code = '" & Trim(rs(0).Value) & "' "
                            Set rsLink = dbcon.ExecuteStatement(strLink)
                            If Not rsLink.EOF Then
                                strLink = "SELECT Max(mpfefl_effective_date) " & _
                                          "FROM mpf_employer_fund_link " & _
                                          "WHERE mpfefl_invest_code = '" & Trim(rs(0).Value) & "' " & _
                                          "AND mpfefl_effective_date <= getdate() "
                                Set rsLink = dbcon.ExecuteStatement(strLink)
                                strLink = "SELECT * " & _
                                          "FROM mpf_employer_fund_link " & _
                                          "WHERE mpfefl_invest_code = '" & Trim(rs(0).Value) & "' " & _
                                          "AND mpfefl_effective_date = '" & Format(rsLink(0), "MM/DD/YYYY") & "' "
                                If Not rsLink.EOF Then
                                    strSQL = strSQL & "'" & Trim(Mbdset(0).Value) & "' as 'Investment Fund Code', " & _
                                                    "'" & Replace(Trim(Mbdset(1).Value), "'", "''") & "' as 'Investment Fund Name', " & _
                                                    "'" & Format(Mbdset(2).Value, "DD/MM/YYYY") & "' as 'As of Date', " & _
                                                    "'" & Trim(Mbdset(3).Value) & "' as 'Investment Fund Price', " & _
                                                    "'" & Trim(Mbdset(4).Value) & "' as 'Currency', " & _
                                                    "'" & Trim(rs(1).Value) & "' as 'Class' union select "
                                End If
                            End If
                        Else
                            strSQL = strSQL & "'" & Trim(Mbdset(0).Value) & "' as 'Investment Fund Code', " & _
                                            "'" & Replace(Trim(Mbdset(1).Value), "'", "''") & "' as 'Investment Fund Name', " & _
                                            "'" & Format(Mbdset(2).Value, "DD/MM/YYYY") & "' as 'As of Date', " & _
                                            "'" & Trim(Mbdset(3).Value) & "' as 'Investment Fund Price', " & _
                                            "'" & Trim(Mbdset(4).Value) & "' as 'Currency', " & _
                                            "'" & Trim(rs(1).Value) & "' as 'Class' union select "
                        End If
                        rs.MoveLast
                    End If
                    rs.MoveNext
                Wend
                rs.MoveFirst
            End If
            strInvestCode = Trim(Mbdset(0).Value)
            Mbdset.MoveNext
        Wend
        
        '// Interim Rate
        If Trim(PstrClass) = "O" Then
            strInvestCode = ""
            strLink = "SELECT code, rate " & _
                      "FROM span_super.interest_rate " & _
                      "WHERE code IN ('INTRCG','INTRDA') " & _
                      "ORDER BY code, effective_date DESC "
            Set Mbdset = Mpfcon.OpenRecordset(strLink, dbOpenDynamic)
            While Not Mbdset.EOF
                If strInvestCode <> Trim(Mbdset(0).Value) Then
                    strSQL = strSQL & "'" & Trim(Mbdset(0).Value) & "' as 'Investment Fund Code', " & _
                                    "'' as 'Investment Fund Name', " & _
                                    "'" & Format(Date, "DD/MM/YYYY") & "' as 'As of Date', " & _
                                    "'" & Trim(Mbdset(1).Value * 100) & "' as 'Investment Fund Price', " & _
                                    "'' as 'Currency', " & _
                                    "'R' as 'Class' union select "
                    strInvestCode = Trim(Mbdset(0).Value)
                End If
                Mbdset.MoveNext
            Wend
        End If
        
        If strSQL <> "select " Then
            strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by 'Investment Fund Code'"
            Set MPFGetFundPrice = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        Else
            Set MPFGetFundPrice = Nothing
        End If
        
        dbcon.Disconnect

'    strDetails = Space$(9999)
'    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
'    lngrst = superRpcUnitPriceDetails(hServer, returnValue, strDetails)
'    strDetails = Trim(strDetails)
'    If lngrst = 0 And strDetails <> "" Then
'        If dbcon.Connect(Pstruserid, PROJECT, FindConnection(PstrODBC)) Then
'            varResult = Split(strDetails, ",")
'            varRecord = MPFSplit(varResult, GETFUNDTOTFNUM, intNoOFRecord)
'            strsql1 = "select mpfinv_code ,mpfinv_class from mpf_investment"
'            Set rs = dbcon.ExecuteStatement(strsql1)
'            strSQL = "select "
'            For i = 0 To intNoOFRecord - 1
'                rs.MoveFirst
'                While Not rs.EOF
'                    If Trim(CStr(varRecord(i).Item(2))) = Trim(rs.Fields(0).Value) And Trim(rs.Fields(1).Value) = "M" Then
'                        strSQL = strSQL & "'" & CStr(varRecord(i).Item(2)) & "' as 'Investment Fund Code', " & _
'                                          "'" & CStr(varRecord(i).Item(3)) & "' as 'Investment Fund Name', " & _
'                                          "'" & CStr(varRecord(i).Item(5)) & "' as 'As of Date', " & _
'                                          "'" & Format(CStr(varRecord(i).Item(7)), ".00") & "' as 'Investment Fund Price' union select "
'                    End If
'                    rs.MoveNext
'                Wend
'            Next i
'
'            If strSQL <> "select " Then
'                strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by 'Investment Fund Code'"
'                Set MPFGetFundPrice = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
'            Else
'                Set MPFGetFundPrice = Nothing
'            End If
'        Else
'            Set MPFGetFundPrice = Nothing
'        End If
'    Else
'        Set MPFGetFundPrice = Nothing
'    End If
'    lngrst = spanRpcDisconnect(hServer)
'    Set dbcon = Nothing
    '****** AC01 END ******
    
Exit Function
errorhandler:
    Set MPFGetFundPrice = Nothing
    Pblnrtncode = False
    Set dbcon = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function MPFGetFundUnit(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                               ByVal PstrDate As String, ByVal PstrODBC As String, _
                               ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                               ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:
Pblnrtncode = True
Dim strDetails As String
Dim returnValue As Integer
Dim varResult As Variant
Dim varRecord() As New Collection
Dim hServer As Long
Dim lngrst As Long
Dim intNoOFRecord As Integer
Dim i, j As Integer
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
    strDetails = Space$(9999)
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    lngrst = superRpcInvestmentValue(hServer, returnValue, PstrFundCode, Plngmbrno, PstrDate, strDetails)
    strDetails = Trim(strDetails)
    If lngrst = 0 And strDetails <> "" Then
        varResult = Split(strDetails, ",")
        varRecord = MPFSplit(varResult, GETFUNDUNITTOTFNUM, intNoOFRecord)
        
        strSQL = "select "
        For i = 0 To intNoOFRecord - 1
            'If CStr(varRecord(i).Item(2)) = "YES" Then
                strSQL = strSQL & "'" & CStr(varRecord(i).Item(1)) & "' as 'Investment Fund Code', " & _
                                  "'" & CStr(varRecord(i).Item(4)) & "' as 'Investment Fund Unit' union select "
            'End If
        Next i
        If strSQL <> "select " Then
            strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by 'Investment Fund Code'"
            If dbcon.Connect(Pstruserid, PROJECT, FindConnection("AETNA")) Then
                Set MPFGetFundUnit = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            Else
                Set MPFGetFundUnit = Nothing
            End If
        Else
            Set MPFGetFundUnit = Nothing
        End If
    Else
        Set MPFGetFundUnit = Nothing
    End If
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFGetFundUnit = Nothing
    Set dbcon = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFTotAssetBal(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                               ByVal PstrDate As String, ByVal PstrODBC As String, _
                               ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                               ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As Double

On Error GoTo errorhandler:
Pblnrtncode = True
Dim rscon As New ADODB.Recordset
Dim rscon1 As New ADODB.Recordset
Dim intRtn As Integer
Dim hServer As Long
Dim lngrst As Long
Dim lngRtn As Long
Dim intFetch As Integer
Dim strBalance As String
Dim varBalance As Variant
Dim dblExchangeRate As Double
Dim blnReturn As Boolean

    'Initial
    Pblnrtncode = True
    MPFTotAssetBal = 0

    'RPC Connection
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    'Get the total asset balance
    'Call The RPC "superRpcInvestmentValueUnits" for getting investment information
    '*********************************************************************************
    'RPC Name : superRpcInvestmentValueUnits
    'Parm In  : ServerHandler, ReturnFlag, FundCode, MemberNumber, EffectiveDate
    'Parm Out : Output String
    'For n = 0,1,2.......
    '*********************************************************************************
    'n      : Investment Code
    'n + 1  : Unitised
    'n + 2  : Total Asset Value
    'n + 3  : Total Number of Unit
    '*********************************************************************************
    'n + 4  : = 'R' i.e Regular resultset
    'n + 5  : Employer MC - Value
    'n + 6  : Employer MC - No. Of Units
    'n + 7  : Employee MC - Value
    'n + 8  : Employee MC - No. Of Units
    'n + 9  : Employer VC - Value
    'n + 10 : Employer VC - No. Of Units
    'n + 11 : Employee VC - Value
    'n + 12 : Employee VC - No. Of Units
    'n + 13 : Employer PS - Value
    'n + 14 : Employer PS - No. Of Units
    'n + 15 : Employee PS - Value
    'n + 16 : Employee PS - No. Of Units
    'n + 17 : Employer Forgone&Adjustment - Value
    'n + 18 : Employer Forgone&Adjustment - No. Of Units
    '*********************************************************************************
    'n + 19 : = 'I' i.e Irregular resultset
    'n + 20 : Employer MC - Value
    'n + 21 : Employer MC - No. Of Units
    'n + 22 : Employee MC - Value
    'n + 23 : Employee MC - No. Of Units
    'n + 24 : Employer VC - Value
    'n + 25 : Employer VC - No. Of Units
    'n + 26 : Employee VC - Value
    'n + 27 : Employee VC - No. Of Units
    'n + 28 : Employer PS - Value
    'n + 29 : Employer PS - No. Of Units
    'n + 30 : Employee PS - Value
    'n + 31 : Employee PS - No. Of Units
    'n + 32 : Employee Forgone&Adjustment - Value
    'n + 33 : Employee Forgone&Adjustment - No. Of Units
    '*********************************************************************************
    'n + 34 : = 'T' i.e Transfer resultset
    'n + 35 : Employer MC - Value
    'n + 36 : Employer MC - No. Of Units
    'n + 37 : Employee MC - Value
    'n + 38 : Employee MC - No. Of Units
    'n + 39 : Employer VC - Value
    'n + 40 : Employer VC - No. Of Units
    'n + 41 : Employee VC - Value
    'n + 42 : Employee VC - No. Of Units
    'n + 43 : Employer PS - Value
    'n + 44 : Employer PS - No. Of Units
    'n + 45 : Employee PS - Value
    'n + 46 : Employee PS - No. Of Units
    '*********************************************************************************
    strBalance = Space(99999)
    lngRtn = superRpcInvestmentValueUnits(hServer, intRtn, PstrFundCode, Plngmbrno, Format(Date, "DDMMYYYY"), strBalance)
    If Len(Trim(strBalance)) > 1 Then
        varBalance = Split(Trim(strBalance), ",")
        intFetch = 0
        While intFetch <= UBound(varBalance)
            dblExchangeRate = MPFGetExchangeRate(PstrODBC, PstrODBCloginID, PstrODBCpwd, Date, Trim(varBalance(intFetch)), blnReturn)
            MPFTotAssetBal = MPFTotAssetBal + _
                             Val(varBalance(intFetch + 2)) * dblExchangeRate
            intFetch = intFetch + 49
        Wend
        MPFTotAssetBal = Format(MPFTotAssetBal, ".00")
    Else
        MPFTotAssetBal = 0
    End If
'    Set rscon = MPFGetFundUnit(PstrFundCode, Plngmbrno, PstrDate, PstrODBC, PstrODBCloginID, PstrODBCpwd, Pstruserid, Pblnrtncode)
'    Set rscon1 = MPFGetFundPrice(PstrODBC, PstrODBCloginID, PstrODBCpwd, Pstruserid, Pblnrtncode)
'    While Not rscon.EOF
'        While Not rscon1.EOF
'            If Trim(rscon.Fields(0).Value) = Trim(rscon1.Fields(0).Value) Then
'                MPFTotAssetBal = MPFTotAssetBal + (CDbl(rscon.Fields(1).Value) * CDbl(rscon1.Fields(3).Value))
'            End If
'            rscon1.MoveNext
'        Wend
'        rscon1.MoveFirst
'        rscon.MoveNext
'    Wend
'    MPFTotAssetBal = Format(MPFTotAssetBal, ".00")
    Set rscon = Nothing
    lngRtn = spanRpcDisconnect(hServer)
    Exit Function
errorhandler:
    Pblnrtncode = False
    MPFTotAssetBal = 0
    Set rscon = Nothing
    lngRtn = spanRpcDisconnect(hServer)
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFGetBillDetails(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                                  ByVal PstrOption As String, ByVal PstrODBC As String, _
                                  ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                                  ByVal PintNbrOptions As Integer, ByVal Pstruserid As String, _
                                  ByRef Pblnrtncode As Boolean) As ADODB.Recordset

On Error GoTo errorhandler:
Pblnrtncode = True
Dim strDetails As String
Dim strFrequent As String
Dim returnValue As Integer
Dim varResult As Variant
Dim varRecord() As New Collection
Dim hServer As Long
Dim lngrst As Long
Dim intNoOFRecord As Integer
Dim i, j As Integer
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect

    strDetails = Space$(4999)
    strFrequent = Space$(10)
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    lngrst = superRpcBillDetails2(hServer, returnValue, PstrFundCode, Plngmbrno, PstrOption, PintNbrOptions, strFrequent, strDetails)
    strDetails = Trim(strDetails)
    If lngrst = 0 And strDetails <> "" Then
        varResult = Split(strDetails, ",")
        varRecord = MPFSplit(varResult, GETBILLDETAILTOTFNUM, intNoOFRecord)
        strSQL = "select "
        For i = 0 To intNoOFRecord - 1
            strSQL = strSQL & "'" & Trim(CStr(varRecord(i).Item(4))) & "' as Employer_MC, " & _
                              "'" & Trim(CStr(varRecord(i).Item(5))) & "' as Employee_MC, " & _
                              "'" & Trim(CStr(varRecord(i).Item(6))) & "' as Employer_VC, " & _
                              "'" & Trim(CStr(varRecord(i).Item(7))) & "' as Employee_VC, " & _
                              "'" & Trim(CStr(varRecord(i).Item(8))) & "' as Employer_PS, " & _
                              "'" & Trim(CStr(varRecord(i).Item(9))) & "' as Employee_PS, " & _
                              "'" & Trim(CStr(varRecord(i).Item(2))) & "' as Close_Date, " & _
                              "'" & Trim(CStr(varRecord(i).Item(1))) & "' as Start_Date, " & _
                              "'" & Trim(CStr(varRecord(i).Item(3))) & "' as Salary1, " & _
                              "'" & Trim(CStr(CDbl(varRecord(i).Item(10)))) & "' as Interest, " & _
                              "'" & Left(strFrequent, Len(Trim(strFrequent)) - 1) & "' as Pay_Frequency, " & _
                              "convert(datetime,'" & Trim(CStr(varRecord(i).Item(2))) & "',103) as CD " & _
                              "union select "
        Next i
        If strSQL <> "select " Then
            strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by CD desc;"
            If dbcon.Connect(Pstruserid, PROJECT, FindConnection("AETNA")) Then
                Set MPFGetBillDetails = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            Else
                Set MPFGetBillDetails = Nothing
            End If
        Else
            Set MPFGetBillDetails = Nothing
        End If
    Else
        Set MPFGetBillDetails = Nothing
    End If
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFGetBillDetails = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
    Set dbcon = Nothing
End Function

Public Function AddServiceLog(ByVal Pdblcustid As String, ByVal Pstrpolicyno As String, _
                              ByVal Pstrtranid As String, ByVal Pstruserid As String) As Boolean
On Error GoTo errorhandler
Dim strSQL As String               'String for SQL statement
Dim dbcon As New Dblogon.Dbconnect 'Dblogon for SQL connection
Dim rscon As New ADODB.Recordset
Dim rscon1 As New ADODB.Recordset
strSQL = "exec cswsp_geteventid"
If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
    Set rscon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    strSQL = "select csw_eventcategorycode,csw_eventtypecode,csw_eventtypedetailcode,csw_eventinitiator from csw_ivrtrans where csw_code='" & Pstrtranid & "'"
    Set rscon1 = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    strSQL = "insert ServiceEventDetail(ServiceEventNumber,EventCategoryCode,EventTypeCode,CustomerID,PolicyAccountID,EventInitialDateTime,EventTypeDetailCode,MasterCSRD,EventCompletionDatetime,EventCloseoutDatetime,EventStatusCode,EventSourceMediumCode,EventSourceInitiatorCode,createdatetime,updateuser,updatedatetime) " & _
             "values(" & rscon.Fields(0).Value & ",'" & rscon1.Fields(0).Value & "','" & rscon1.Fields(1).Value & "'," & Pdblcustid & ",'" & Pstrpolicyno & "', getdate(),'" & rscon1.Fields(2).Value & "','IVRS', getdate(), getdate(), 'C', 'PC', '" & rscon1.Fields(3).Value & "', getdate(),'" & Pstruserid & "', getdate())"
    Call dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
    AddServiceLog = True
Else
    GoTo errorhandler
End If
dbcon.Disconnect
Set rscon = Nothing
Set rscon1 = Nothing
Set dbcon = Nothing
Exit Function
errorhandler:
    AddServiceLog = False
    Set dbcon = Nothing
    Set rscon = Nothing
    Set rscon1 = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function MPFEmployerCon(ByVal PstrEmpCode As String, ByVal PstrFundCode As String, _
                               ByVal PstrReportCtr As String, ByVal PstrConType As String, _
                               ByVal PintNbrTran As Integer, ByVal Pstruserid As String, _
                               ByVal Pstrdbc As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo ErrHandler
Dim dbcon As New Dblogon.Dbconnect
Dim rscon As New ADODB.Recordset
Dim rscon2 As New ADODB.Recordset
Dim strSQL As String
Dim i As Integer
Dim tmpDate As String
Dim tmpCon(6) As Double

    Pblnrtncode = True
    If dbcon.Connect(Pstruserid, PROJECT, FindConnection(Pstrdbc)) Then
        If PstrConType = "M" Then
            'strSQL = "select distinct(mpfemc_cont_date) from mpf_employer_cont " & _
                     "where mpfemc_emp_code ='" + PstrEmpcode + "' and mpfemc_fund_code = '" & PstrFundCode & "' and mpfemc_reporting_centre = '" & PstrReportCtr & _
                     "' and datediff(month,mpfemc_cont_date, getDate()) < " & PintNbrTran & " order by mpfemc_cont_date desc"
            strSQL = "SELECT distinct(mpfemc_cont_date) " & _
                     "FROM mpf_employer_cont " & _
                     "WHERE mpfemc_emp_code ='" & PstrEmpCode & "' " & _
                     "AND mpfemc_fund_code = '" & PstrFundCode & "' " & _
                     "AND mpfemc_reporting_centre = '" & PstrReportCtr & "' " & _
                     "AND datediff(month,mpfemc_cont_date, getDate()) < '" & PintNbrTran & "' " & _
                     "AND mpfemc_batch_number IS NOT NULL " & _
                     "ORDER BY mpfemc_cont_date desc"
            Set rscon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            rscon.MoveLast
            'strsql = "select substring(convert(varchar(20),mpfemc_cont_date,103),4,7) as ContDate, mpfemc_index1 as Employer_MC, mpfemc_index2 as Employee_MC, mpfemc_index3 as Employer_VC, " & _
                     "mpfemc_index4 as Employee_VC, mpfemc_index5 as Employer_PS, mpfemc_index6 as Employee_PS from mpf_employer_cont " & _
                     "where mpfemc_emp_code ='" + PstrEmpcode + "' and mpfemc_fund_code = '" & PstrFundCode & "' and mpfemc_reporting_centre = '" & PstrReportCtr & _
                     "' and datediff(month,mpfemc_cont_date,'" & rscon.Fields(0).Value & "') < " & PintNbrTran & " group by substring(convert(varchar(20),mpfemc_cont_date,103),4,7)"
            strSQL = "select substring(convert(varchar(20),mpfemc_cont_date,103),4,7) as ContDate, sum(mpfemc_index1) as Employer_MC, sum(mpfemc_index2) as Employee_MC, sum(mpfemc_index3) as Employer_VC, " & _
                     "sum(mpfemc_index4) as Employee_VC, sum(mpfemc_index5) as Employer_PS, sum(mpfemc_index6) as Employee_PS from mpf_employer_cont " & _
                     "where mpfemc_emp_code ='" + PstrEmpCode + "' and mpfemc_fund_code = '" & PstrFundCode & "' and mpfemc_reporting_centre = '" & PstrReportCtr & _
                     "' and datediff(month,mpfemc_cont_date,'" & Format(rscon.Fields(0).Value, "MM/DD/YYYY") & "') <= " & PintNbrTran & " and mpfemc_batch_number IS NOT NULL group by mpfemc_cont_date order by mpfemc_cont_date desc"
            Set rscon2 = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            tmpDate = Trim(rscon2.Fields(0).Value)
            strSQL = "select "
            While Not rscon2.EOF
                If tmpDate <> rscon2.Fields(0).Value Then
                        strSQL = strSQL & "'" & tmpCon(1) & "' as Employer_MC, " & _
                                          "'" & tmpCon(2) & "' as Employee_MC, " & _
                                          "'" & tmpCon(3) & "' as Employer_VC, " & _
                                          "'" & tmpCon(4) & "' as Employee_VC, " & _
                                          "'" & tmpCon(5) & "' as Employer_PS, " & _
                                          "'" & tmpCon(6) & "' as Employee_PS, " & _
                                          "'" & "00/" & tmpDate & "' as ContDate union select "
                        tmpDate = Trim(rscon2.Fields(0).Value)
                        For i = 1 To 6
                            tmpCon(i) = 0
                        Next i
                End If
                For i = 1 To 6
                    tmpCon(i) = tmpCon(i) + CDbl(rscon2.Fields(i).Value)
                Next i
                rscon2.MoveNext
            Wend
            strSQL = strSQL & "'" & tmpCon(1) & "' as Employer_MC, " & _
                              "'" & tmpCon(2) & "' as Employee_MC, " & _
                              "'" & tmpCon(3) & "' as Employer_VC, " & _
                              "'" & tmpCon(4) & "' as Employee_VC, " & _
                              "'" & tmpCon(5) & "' as Employer_PS, " & _
                              "'" & tmpCon(6) & "' as Employee_PS, " & _
                              "'" & "00/" & tmpDate & "' order by ContDate"
            If strSQL <> "select " Then
                Set MPFEmployerCon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            Else
                Set MPFEmployerCon = Nothing
            End If
        End If
        
        If PstrConType = "C" Then
            strSQL = "select distinct(mpfemc_cont_date) from mpf_employer_cont " & _
                     "where mpfemc_emp_code ='" + PstrEmpCode + "' and mpfemc_fund_code = '" & PstrFundCode & "' and mpfemc_reporting_centre = '" & PstrReportCtr & _
                     "' and mpfemc_batch_number IS NOT NULL order by mpfemc_cont_date desc"
            Set rscon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            For i = 1 To rscon.RecordCount
                If i = PintNbrTran Then
                    i = rscon.RecordCount
                Else
                    If Not rscon.EOF Then
                        rscon.MoveNext
                    End If
                End If
            Next i
            If rscon.EOF Then
                rscon.MoveLast
            End If
            strSQL = "select sum(mpfemc_index1) as Employer_MC, sum(mpfemc_index2) as Employee_MC, sum(mpfemc_index3) as Employer_VC, " & _
                     "sum(mpfemc_index4) as Employee_VC, sum(mpfemc_index5) as Employer_PS, sum(mpfemc_index6) as Employee_PS, convert(varchar(10),mpfemc_cont_date,103) as ContDate from mpf_employer_cont " & _
                     "where mpfemc_emp_code ='" + PstrEmpCode + "' and mpfemc_fund_code = '" & PstrFundCode & "' and mpfemc_reporting_centre = '" & PstrReportCtr & _
                     "' and mpfemc_cont_date >= '" & Format(rscon.Fields(0).Value, "MM/DD/YYYY") & "' and mpfemc_batch_number IS NOT NULL group by mpfemc_cont_date order by mpfemc_cont_date desc"
            Set MPFEmployerCon = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        End If
    Else
        GoTo ErrHandler
    End If
    Set dbcon = Nothing
    Set rscon = Nothing
    Set rscon2 = Nothing
    Exit Function
ErrHandler:
    Set MPFEmployerCon = Nothing
    Set dbcon = Nothing
    Set rscon = Nothing
    Set rscon2 = Nothing
    Pblnrtncode = False
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFgetContribution(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                                   ByVal PstrDate As String, ByVal PstrODBC As String, _
                                   ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                                   ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:
Pblnrtncode = True
Dim hServer As Long
Dim lngrst As Long
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim strODBC As String
Dim Mpfcon As DAO.Connection
Dim Mpfws As DAO.Workspace
Dim Mbdset As DAO.Recordset
Dim strStartDate As String
Dim strPeriod As String
Dim MCER, MCEE, VCER, VCEE As Double
Dim intCheck As Integer
Dim dblDetails As Double
Dim i As Integer

strODBC = "ODBC;UID=" + UCase(PstrODBCloginID) + ";PWD=" + PstrODBCpwd
Set Mpfws = CreateWorkspace("", PstrODBCloginID, "", dbUseODBC)
Set Mpfcon = Mpfws.OpenConnection(Trim(PstrODBC), dbDriverNoPrompt, True, strODBC)
Mpfcon.QueryTimeout = 3000

strSQL = "select date_joined_fund from span_super.member_basic_details " _
        + "where fund_code = '" + PstrFundCode + "' and member_number = " + CStr(Plngmbrno)
Set Mbdset = Mpfcon.OpenRecordset(strSQL, dbOpenDynamic)
strStartDate = Format(Mbdset.Fields(0), "ddmmyyyy")

lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    
MCER = 0
MCEE = 0
VCER = 0
VCEE = 0
intCheck = 0

    lngrst = superRpcPeriodConts(hServer, dblDetails, PstrFundCode, Plngmbrno, strStartDate, PstrDate, "R1,I1,T1")
    If lngrst = 0 Then
        intCheck = intCheck + 1
        MCER = dblDetails
    End If
    
    lngrst = superRpcPeriodConts(hServer, dblDetails, PstrFundCode, Plngmbrno, strStartDate, PstrDate, "R2,I2,T2")
    If lngrst = 0 Then
        intCheck = intCheck + 1
        MCEE = dblDetails
    End If
    
    lngrst = superRpcPeriodConts(hServer, dblDetails, PstrFundCode, Plngmbrno, strStartDate, PstrDate, "R3,I3,T3")
    If lngrst = 0 Then
        intCheck = intCheck + 1
        VCER = dblDetails
    End If
    
    lngrst = superRpcPeriodConts(hServer, dblDetails, PstrFundCode, Plngmbrno, strStartDate, PstrDate, "R4,I4,T4")
    If lngrst = 0 Then
        intCheck = intCheck + 1
        VCEE = dblDetails
    End If

If intCheck = 4 Then
    strSQL = "select " & MCER & " as 'ER Mandatory Contribution', " & MCEE & " as 'EE Mandatory Contribution', " & VCER & " as 'ER Voluntary Contribution', " & VCEE & " as 'EE Voluntary Contribution'"
    If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
        Set MPFgetContribution = dbcon.ExecuteStatement(strSQL)
    Else
        Set MPFgetContribution = Nothing
    End If
Else
    Set MPFgetContribution = Nothing
End If
lngrst = spanRpcDisconnect(hServer)
Set dbcon = Nothing

end_fun:
    Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFgetContribution = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFGetFundUnitCont(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                                   ByVal PstrDate As String, ByVal PstrODBC As String, _
                                   ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                                   ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:
Pblnrtncode = True
Dim strDetails As String
Dim returnValue As Integer
Dim varResult As Variant
Dim varRecord() As New Collection
Dim hServer As Long
Dim lngrst As Long
Dim intNoOFRecord As Integer
Dim i, j, k As Integer
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim MCER, MCEE, VCER, VCEE, PSER, PSEE As Double
    strDetails = Space$(9999)
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    lngrst = superRpcInvestmentValueUnits(hServer, returnValue, PstrFundCode, Plngmbrno, PstrDate, strDetails)
    strDetails = Trim(strDetails)
    If lngrst = 0 And strDetails <> "" Then
        varResult = Split(strDetails, ",")
        varRecord = MPFSplit(varResult, GETFUNDUNITCONTFNUM, intNoOFRecord)
        
        strSQL = "select "
        For i = 0 To intNoOFRecord - 1
            MCER = 0
            MCEE = 0
            VCER = 0
            VCEE = 0
            PSER = 0
            PSEE = 0
            If CStr(varRecord(i).Item(2)) = "YES" Then
                For k = 5 To GETFUNDUNITCONTFNUM Step MPFUNITCONFIELDSEGMENT
                    MCER = MCER + CDbl(varRecord(i).Item(k + 2))
                    MCEE = MCEE + CDbl(varRecord(i).Item(k + 4))
                    VCER = VCER + CDbl(varRecord(i).Item(k + 6))
                    VCEE = VCEE + CDbl(varRecord(i).Item(k + 8))
                    PSER = PSER + CDbl(varRecord(i).Item(k + 10))
                    PSEE = PSEE + CDbl(varRecord(i).Item(k + 12))
                Next k
                strSQL = strSQL & "'" & CStr(varRecord(i).Item(1)) & "' as 'Investment Fund Code', " & _
                                  "'" & MCER & "' as 'MC ER (Unit)', " & _
                                  "'" & MCEE & "' as 'MC EE (Unit)', " & _
                                  "'" & VCER & "' as 'VC ER (Unit)', " & _
                                  "'" & VCEE & "' as 'VC EE (Unit)', " & _
                                  "'" & PSER & "' as 'PS ER (Unit)', " & _
                                  "'" & PSEE & "' as 'PS EE (Unit)' union select "
            End If
        Next i
        If strSQL <> "select " Then
            strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by 'Investment Fund Code'"
            If dbcon.Connect(Pstruserid, PROJECT, FindConnection("AETNA")) Then
                Set MPFGetFundUnitCont = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            Else
                Set MPFGetFundUnitCont = Nothing
            End If
            
        Else
            Set MPFGetFundUnitCont = Nothing
        End If
    Else
        Set MPFGetFundUnitCont = Nothing
    End If
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFGetFundUnitCont = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFGetFundDetails(ByVal PstrFundCode As String, ByVal Plngmbrno As Long, _
                                  ByVal PstrDate As String, ByVal PstrODBC As String, _
                                  ByVal PstrODBCloginID As String, ByVal PstrODBCpwd As String, _
                                  ByVal Pstruserid As String, ByRef Pblnrtncode As Boolean) As ADODB.Recordset
On Error GoTo errorhandler:
Pblnrtncode = True
Dim strDetails As String
Dim returnValue As String
Dim varResult As Variant
Dim varRecord() As New Collection
Dim hServer As Long
Dim lngrst As Long
Dim intNoOFRecord As Integer
Dim i, j As Integer
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rs As New ADODB.Recordset

    strDetails = Space$(99)
    returnValue = Space$(2999)
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
    lngrst = superRpcInvestments(hServer, returnValue, PstrFundCode, Plngmbrno, PstrDate, strDetails)
    returnValue = Trim(returnValue)
    If lngrst = 0 And returnValue <> "" Then
        varResult = Split(returnValue, ",")
        varRecord = MPFSplit(varResult, GETFUNDPERCENTFNUM, intNoOFRecord)
        Set rs = MPFGetFundUnitCont(PstrFundCode, Plngmbrno, PstrDate, PstrODBC, PstrODBCloginID, PstrODBCpwd, Pstruserid, Pblnrtncode)
        strSQL = "select "
        For i = 0 To intNoOFRecord - 1
            rs.MoveFirst
            While Not rs.EOF
                If Trim(CStr(varRecord(i).Item(1))) = Trim(rs.Fields(0).Value) Then
                    strSQL = strSQL & "'" & CStr(varRecord(i).Item(1)) & "' as 'Investment Fund Code', " & _
                                      "'" & Trim(rs.Fields(1).Value) & "' as 'MC ER (Unit)', " & _
                                      "'" & Trim(rs.Fields(2).Value) & "' as 'MC EE (Unit)', " & _
                                      "'" & Trim(rs.Fields(3).Value) & "' as 'VC ER (Unit)', " & _
                                      "'" & Trim(rs.Fields(4).Value) & "' as 'VC EE (Unit)', " & _
                                      "'" & CStr(varRecord(i).Item(2)) & "' as 'Investment Fund Percent' union select "
                End If
                rs.MoveNext
            Wend
        Next i
        If strSQL <> "select " Then
            strSQL = Left(strSQL, Len(strSQL) - Len("union select ")) & " order by 'Investment Fund Code'"
            If dbcon.Connect(Pstruserid, PROJECT, FindConnection("AETNA")) Then
                Set MPFGetFundDetails = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            Else
                Set MPFGetFundDetails = Nothing
            End If
            
        Else
            Set MPFGetFundDetails = Nothing
        End If
    Else
        Set MPFGetFundDetails = Nothing
    End If
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
    Set rs = Nothing
Exit Function
errorhandler:
    Pblnrtncode = False
    Set MPFGetFundDetails = Nothing
    lngrst = spanRpcDisconnect(hServer)
    Set dbcon = Nothing
    Set rs = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
'****** AC01 BEGIN ******
Public Function MPFGetClass(ByVal Pstruserid As String, _
                            ByVal PstrFundCode As String, _
                            ByVal PstrDB As String, _
                            ByRef PblnReturn As Boolean) As String
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rstERSpecialFld As New ADODB.Recordset

    'Initial
    MPFGetClass = "-1"
    PblnReturn = True
    
    If dbcon.Connect(Pstruserid, PROJECT, FindConnection(Trim(PstrDB))) Then
        'Identify MPF/ORSO Fund Code
        strSQL = "SELECT DISTINCT mpfesf_special_fields " & _
                 "FROM   mpf_employer_special_fields " & _
                 "WHERE  mpfesf_fund_code = '" & Trim(PstrFundCode) & "' " & _
                 "AND    mpfesf_table_index in (10,12) "
        Set rstERSpecialFld = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        
        While Not rstERSpecialFld.EOF
            If Not IsNull(rstERSpecialFld(0).Value) Then
                '//O-ORSO, P-Preserved, E-MPF Employer, S-MPF Self-Emp
                Select Case Trim(rstERSpecialFld(0).Value)
                    Case "O"
                        MPFGetClass = "O"
                    Case "S"
                        MPFGetClass = "M"
                    Case "E"
                        MPFGetClass = "M"
                End Select
            End If
            rstERSpecialFld.MoveNext
        Wend
    End If
    
    If dbcon.Isconnected Then
        dbcon.Disconnect
    End If

Exit Function
errorhandler:
    PblnReturn = False
    MPFGetClass = "-1"
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFGetForfeiture(ByVal Pstruserid As String, _
                                 ByVal PstrFundCode As String, _
                                 ByVal PstrERCode As String, _
                                 ByVal PstrRC As String, _
                                 ByVal PstrDB As String, _
                                 ByRef PblnReturn As Boolean) As Double
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rstForfeiture As New ADODB.Recordset

    'Initial
    MPFGetForfeiture = -1
    PblnReturn = True
    
    If IsNull(PstrFundCode) = False And _
       IsNull(PstrERCode) = False And _
       IsNull(PstrRC) = False And _
       IsNumeric(PstrFundCode) = True And _
       IsNumeric(PstrERCode) = True And _
       IsNumeric(PstrRC) = True Then
        If dbcon.Connect(Pstruserid, PROJECT, FindConnection(Trim(PstrDB))) Then
            'Get The Forfeiture Balance
            strSQL = "SELECT SUM(mpffcf_amount) " & _
                     "FROM mpf_fund_cash_flow " & _
                     "WHERE mpffcf_fund_code = '" & Trim(PstrFundCode) & "' " & _
                     "AND mpffcf_employer_code = '" + Trim(PstrERCode) + "::" + Trim(PstrRC) + "' " & _
                     "AND mpffcf_trans_type <> 'REC' " & _
                     "AND mpffcf_trans_type <> 'PBP'"
            Set rstForfeiture = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            
            If Not rstForfeiture.EOF Then
                If Not IsNull(rstForfeiture(0).Value) Then
                    MPFGetForfeiture = Format(rstForfeiture(0).Value, ".00")
                Else
                    MPFGetForfeiture = 0
                End If
            End If
        End If
    End If
    If dbcon.Isconnected Then
        dbcon.Disconnect
    End If

Exit Function
errorhandler:
    MPFGetForfeiture = -1
    PblnReturn = False
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
Public Function MPFGetVesting(ByVal PstrODBC As String, _
                              ByVal PstrODBCloginID As String, _
                              ByVal PstrODBCpwd As String, _
                              ByVal Pstruserid As String, _
                              ByVal PstrFundCode As String, _
                              ByVal PstrMemberNo As String, _
                              ByVal PstrRC As String, _
                              ByVal PstrDB As String, _
                              ByRef PblnReturn As Boolean) As Double
On Error GoTo errorhandler:
Dim strSQL As String
Dim strBalance As String
Dim strTableNo As String
Dim strNoOfYear As String
Dim DJF As Date
Dim DJC As Date
Dim dblRate As Double
Dim dblERVested As Double
Dim dblTotalERVested As Double
Dim intFetch As Integer
Dim lngRtn As Integer
Dim intRtn As Integer
Dim hServer As Long
Dim lngrst As Long
Dim strMemberType As String
Dim varBalance As Variant
Dim dbcon As New Dblogon.Dbconnect
Dim rstVesting As New ADODB.Recordset
Dim rstEEList As New ADODB.Recordset
Dim dblExchangeRate As Double
Dim blnReturn As Boolean

    'Initial
    MPFGetVesting = -1
    PblnReturn = True
    strTableNo = ""
    strNoOfYear = ""
    strMemberType = ""
    
    'RPC Connection
    lngrst = spanRpcConnectLogon(hServer, PstrODBC, PstrODBCloginID, PstrODBCpwd)
      
    If IsNull(PstrFundCode) = False And _
       IsNull(PstrFundCode) = False Then 'And _
       IsNumeric(PstrMemberNo) = True And _
       IsNumeric(PstrMemberNo) = True Then
       
        'Checking Active/Exit & RC
        If dbcon.Connect(Pstruserid, PROJECT, FindConnection(Trim(PstrDB))) Then
            'strSQL = "SELECT * " & _
                     "FROM mpf_member_basic_details " & _
                     "WHERE mpfmbd_fund_code = '" & Trim(PstrFundCode) & "' " & _
                     "AND mpfmbd_member_number = '" & Trim(Mid(PstrMemberNo, InStr(1, PstrMemberNo, ":") + 1)) & "' " & _
                     "AND mpfmbd_reporting_centre = '" & Trim(Mid(PstrMemberNo, 1, InStr(1, PstrMemberNo, ":") - 1)) & "' "
            strSQL = "SELECT * " & _
                     "FROM mpf_member_basic_details " & _
                     "WHERE mpfmbd_fund_code = '" & Trim(PstrFundCode) & "' " & _
                     "AND mpfmbd_member_number = '" & Trim(PstrMemberNo) & "' " & _
                     "AND mpfmbd_reporting_centre = '" & Trim(PstrRC) & "' "
            Set rstVesting = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            
            If rstVesting.EOF Then
                MPFGetVesting = -1
                PblnReturn = False
                lngrst = spanRpcDisconnect(hServer)
                Exit Function
            End If
            
            'strSQL = "SELECT mpfmbd_member_type " & _
                     "FROM mpf_member_basic_details " & _
                     "WHERE mpfmbd_fund_code = '" & Trim(PstrFundCode) & "' " & _
                     "AND   mpfmbd_member_number = '" & Trim(Mid(PstrMemberNo, InStr(1, PstrMemberNo, ":") + 1)) & "' "
            strSQL = "SELECT mpfmbd_member_type " & _
                     "FROM mpf_member_basic_details " & _
                     "WHERE mpfmbd_fund_code = '" & Trim(PstrFundCode) & "' " & _
                     "AND   mpfmbd_member_number = '" & Trim(PstrMemberNo) & "' "
            Set rstVesting = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        Else
            MPFGetVesting = -1
            PblnReturn = False
            lngrst = spanRpcDisconnect(hServer)
        End If
        
        If Trim(rstVesting(0)) = "A" Then
            'Get The Table Index
            'strSQL = "SELECT * " & _
                     "FROM  mpf_member_special_fields " & _
                     "WHERE mpfmsf_fund_code = '" & PstrFundCode & "' " & _
                     "AND   mpfmsf_member_number = '" & Trim(Mid(PstrMemberNo, InStr(1, PstrMemberNo, ":") + 1)) & "' " & _
                     "AND   mpfmsf_table_index IN (1,6)"
            strSQL = "SELECT * " & _
                     "FROM  mpf_member_special_fields " & _
                     "WHERE mpfmsf_fund_code = '" & PstrFundCode & "' " & _
                     "AND   mpfmsf_member_number = '" & Trim(PstrMemberNo) & "' " & _
                     "AND   mpfmsf_table_index IN (1,6)"
            Set rstVesting = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            
            While Not rstVesting.EOF
                If Trim(rstVesting.Fields("mpfmsf_table_index").Value) = "1" And Not IsNull(rstVesting.Fields("mpfmsf_special_fields").Value) Then
                    'Get the DateJoinedFund & DateJoinedCompany
                    strBalance = Space(2000)
                    'lngRtn = superRpcMemberDetails(hServer, intRtn, PstrFundCode, Val(Trim(Mid(PstrMemberNo, InStr(1, PstrMemberNo, ":") + 1))), strBalance)
                    lngRtn = superRpcMemberDetails(hServer, intRtn, PstrFundCode, Val(PstrMemberNo), strBalance)
                    varBalance = Split(strBalance, ",")
                    Select Case Mid(varBalance(2), 4, 2)
                        Case "01"
                            DJC = CDate(Left(varBalance(2), 2) + " Jan " + Mid(varBalance(2), 7, 4))
                        Case "02"
                            DJC = CDate(Left(varBalance(2), 2) + " Feb " + Mid(varBalance(2), 7, 4))
                        Case "03"
                            DJC = CDate(Left(varBalance(2), 2) + " Mar " + Mid(varBalance(2), 7, 4))
                        Case "04"
                            DJC = CDate(Left(varBalance(2), 2) + " Apr " + Mid(varBalance(2), 7, 4))
                        Case "05"
                            DJC = CDate(Left(varBalance(2), 2) + " May " + Mid(varBalance(2), 7, 4))
                        Case "06"
                            DJC = CDate(Left(varBalance(2), 2) + " Jun " + Mid(varBalance(2), 7, 4))
                        Case "07"
                            DJC = CDate(Left(varBalance(2), 2) + " Jul " + Mid(varBalance(2), 7, 4))
                        Case "08"
                            DJC = CDate(Left(varBalance(2), 2) + " Aug " + Mid(varBalance(2), 7, 4))
                        Case "09"
                            DJC = CDate(Left(varBalance(2), 2) + " Sep " + Mid(varBalance(2), 7, 4))
                        Case "10"
                            DJC = CDate(Left(varBalance(2), 2) + " Oct " + Mid(varBalance(2), 7, 4))
                        Case "11"
                            DJC = CDate(Left(varBalance(2), 2) + " Nov " + Mid(varBalance(2), 7, 4))
                        Case "12"
                            DJC = CDate(Left(varBalance(2), 2) + " Dec " + Mid(varBalance(2), 7, 4))
                    End Select
                    Select Case Mid(varBalance(3), 4, 2)
                        Case "01"
                            DJF = CDate(Left(varBalance(3), 2) + " Jan " + Mid(varBalance(3), 7, 4))
                        Case "02"
                            DJF = CDate(Left(varBalance(3), 2) + " Feb " + Mid(varBalance(3), 7, 4))
                        Case "03"
                            DJF = CDate(Left(varBalance(3), 2) + " Mar " + Mid(varBalance(3), 7, 4))
                        Case "04"
                            DJF = CDate(Left(varBalance(3), 2) + " Apr " + Mid(varBalance(3), 7, 4))
                        Case "05"
                            DJF = CDate(Left(varBalance(3), 2) + " May " + Mid(varBalance(3), 7, 4))
                        Case "06"
                            DJF = CDate(Left(varBalance(3), 2) + " Jun " + Mid(varBalance(3), 7, 4))
                        Case "07"
                            DJF = CDate(Left(varBalance(3), 2) + " Jul " + Mid(varBalance(3), 7, 4))
                        Case "08"
                            DJF = CDate(Left(varBalance(3), 2) + " Aug " + Mid(varBalance(3), 7, 4))
                        Case "09"
                            DJF = CDate(Left(varBalance(3), 2) + " Sep " + Mid(varBalance(3), 7, 4))
                        Case "10"
                            DJF = CDate(Left(varBalance(3), 2) + " Oct " + Mid(varBalance(3), 7, 4))
                        Case "11"
                            DJF = CDate(Left(varBalance(3), 2) + " Nov " + Mid(varBalance(3), 7, 4))
                        Case "12"
                            DJF = CDate(Left(varBalance(3), 2) + " Dec " + Mid(varBalance(3), 7, 4))
                    End Select

                    If Trim(rstVesting.Fields("mpfmsf_special_fields").Value) = "DJS" Then
                        strNoOfYear = ComDateDiff(Format(DJC, "DD MM YYYY"), Format(Date, "DD MM YYYY"))
                    ElseIf Trim(rstVesting.Fields("mpfmsf_special_fields").Value) = "DJF" Then
                        strNoOfYear = ComDateDiff(Format(DJF, "DD MM YYYY"), Format(Date, "DD MM YYYY"))
                    ElseIf Trim(rstVesting.Fields("mpfmsf_special_fields").Value) = "IMM" Then
                        strNoOfYear = 99
                    End If
                ElseIf Trim(rstVesting.Fields("mpfmsf_table_index").Value) = "6" Then
                    strTableNo = Trim(rstVesting.Fields("mpfmsf_special_fields").Value)
                End If
                rstVesting.MoveNext
            Wend
            
            'Get The Rate
            strSQL = "SELECT * " & _
                     "FROM  mpf_factor_table_values " & _
                     "WHERE mpfftv_table_number = " & strTableNo & " " & _
                     "ORDER BY mpfftv_table_index DESC "
            Set rstVesting = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
            
            While Not rstVesting.EOF
                If Val(rstVesting.Fields("mpfftv_entry").Value) <= Val(strNoOfYear) Then
                    dblRate = Val(rstVesting.Fields("mpfftv_factor_value").Value)
                    rstVesting.MoveLast
                End If
                rstVesting.MoveNext
            Wend
            
            'Get the ER Vesting Balance
            'Call The RPC "superRpcInvestmentValueUnits" for getting investment information
            '*********************************************************************************
            'RPC Name : superRpcInvestmentValueUnits
            'Parm In  : ServerHandler, ReturnFlag, FundCode, MemberNumber, EffectiveDate
            'Parm Out : Output String
            'For n = 0,1,2.......
            '*********************************************************************************
            'n      : Investment Code
            'n + 1  : Unitised
            'n + 2  : Total Asset Value
            'n + 3  : Total Number of Unit
            '*********************************************************************************
            'n + 4  : = 'R' i.e Regular resultset
            'n + 5  : Employer MC - Value
            'n + 6  : Employer MC - No. Of Units
            'n + 7  : Employee MC - Value
            'n + 8  : Employee MC - No. Of Units
            'n + 9  : Employer VC - Value
            'n + 10 : Employer VC - No. Of Units
            'n + 11 : Employee VC - Value
            'n + 12 : Employee VC - No. Of Units
            'n + 13 : Employer PS - Value
            'n + 14 : Employer PS - No. Of Units
            'n + 15 : Employee PS - Value
            'n + 16 : Employee PS - No. Of Units
            'n + 17 : Employer Forgone&Adjustment - Value
            'n + 18 : Employer Forgone&Adjustment - No. Of Units
            '*********************************************************************************
            'n + 19 : = 'I' i.e Irregular resultset
            'n + 20 : Employer MC - Value
            'n + 21 : Employer MC - No. Of Units
            'n + 22 : Employee MC - Value
            'n + 23 : Employee MC - No. Of Units
            'n + 24 : Employer VC - Value
            'n + 25 : Employer VC - No. Of Units
            'n + 26 : Employee VC - Value
            'n + 27 : Employee VC - No. Of Units
            'n + 28 : Employer PS - Value
            'n + 29 : Employer PS - No. Of Units
            'n + 30 : Employee PS - Value
            'n + 31 : Employee PS - No. Of Units
            'n + 32 : Employee Forgone&Adjustment - Value
            'n + 33 : Employee Forgone&Adjustment - No. Of Units
            '*********************************************************************************
            'n + 34 : = 'T' i.e Transfer resultset
            'n + 35 : Employer MC - Value
            'n + 36 : Employer MC - No. Of Units
            'n + 37 : Employee MC - Value
            'n + 38 : Employee MC - No. Of Units
            'n + 39 : Employer VC - Value
            'n + 40 : Employer VC - No. Of Units
            'n + 41 : Employee VC - Value
            'n + 42 : Employee VC - No. Of Units
            'n + 43 : Employer PS - Value
            'n + 44 : Employer PS - No. Of Units
            'n + 45 : Employee PS - Value
            'n + 46 : Employee PS - No. Of Units
            '*********************************************************************************
            strBalance = Space(8192)
            'lngRtn = superRpcInvestmentValueUnits(hServer, intRtn, PstrFundCode, Val(Trim(Mid(PstrMemberNo, InStr(1, PstrMemberNo, ":") + 1))), Format(Date, "DDMMYYYY"), strBalance)
            lngRtn = superRpcInvestmentValueUnits(hServer, intRtn, PstrFundCode, Val(PstrMemberNo), Format(Date, "DDMMYYYY"), strBalance)
            If Len(Trim(strBalance)) > 1 Then
                varBalance = Split(strBalance, ",")
                
                intFetch = 0
                dblERVested = 0
                While intFetch <= UBound(varBalance)
                    dblExchangeRate = MPFGetExchangeRate(PstrODBC, PstrODBCloginID, PstrODBCpwd, Date, Trim(varBalance(intFetch)), blnReturn)
                    dblERVested = dblERVested + _
                                  Val(varBalance(intFetch + 9)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 13)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 17)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 24)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 28)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 39)) * dblExchangeRate + _
                                  Val(varBalance(intFetch + 43)) * dblExchangeRate
                    intFetch = intFetch + 49
                Wend
                
                MPFGetVesting = Format(dblERVested * dblRate, ".00")
                PblnReturn = True
                lngrst = spanRpcDisconnect(hServer)
            Else
                MPFGetVesting = -1
                PblnReturn = False
                lngrst = spanRpcDisconnect(hServer)
            End If
        Else
            MPFGetVesting = -1
            PblnReturn = False
            lngrst = spanRpcDisconnect(hServer)
        End If
    Else
        MPFGetVesting = -1
        PblnReturn = False
        lngrst = spanRpcDisconnect(hServer)
    End If
    
    If dbcon.Isconnected Then
        dbcon.Disconnect
    End If
    
Exit Function
errorhandler:
    lngrst = spanRpcDisconnect(hServer)
    MPFGetVesting = -1
    PblnReturn = False
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function MPFGetExchangeRate(ByVal PstrODBC As String, _
                                   ByVal PstrODBCloginID As String, _
                                   ByVal PstrODBCpwd As String, _
                                   ByVal PEnquiryDate As Date, _
                                   ByVal PstrInvestCode As String, _
                                   ByRef PblnReturn As Boolean) As Double
                                
On Error GoTo errorhandler:
Dim strSQL As String
Dim strODBC As String
Dim Mpfcon As DAO.Connection
Dim Mpfws As DAO.Workspace
Dim MbdInvCurrency As DAO.Recordset
Dim MbdHKD As DAO.Recordset
Dim MbdRate As DAO.Recordset

    'Initial
    PblnReturn = True
    
    strODBC = "ODBC;UID=" + UCase(PstrODBCloginID) + ";PWD=" + PstrODBCpwd
    Set Mpfws = CreateWorkspace("", PstrODBCloginID, "", dbUseODBC)
    Set Mpfcon = Mpfws.OpenConnection(Trim(PstrODBC), dbDriverNoPrompt, True, strODBC)
    Mpfcon.QueryTimeout = 3000
    
    'Get the investment currency
    strSQL = "SELECT currency_no " & _
             "FROM span_super.investment_details " & _
             "WHERE investment_code = '" + Trim(PstrInvestCode) + "' "
    Set MbdInvCurrency = Mpfcon.OpenRecordset(strSQL, dbOpenDynamic)
    
    'Get the currency no of HKD
    strSQL = "SELECT currency_no " & _
             "FROM span_super.currency_definition " & _
             "WHERE currency_code = 'HKD' "
    Set MbdHKD = Mpfcon.OpenRecordset(strSQL, dbOpenDynamic)
    
    'Check whether is HKD or not
    If Trim(MbdInvCurrency.Fields(0)) <> Trim(MbdHKD.Fields(0)) Then
        'Not a HKD
        strSQL = "SELECT buy_rate " & _
                 "FROM span_super.currency_exchange_rate " & _
                 "WHERE currency_no1 = '" & Trim(MbdInvCurrency.Fields(0)) & "' " & _
                 "AND currency_no2 = '" & Trim(MbdHKD.Fields(0)) & "' " & _
                 "AND effective_date <= '" & Format(PEnquiryDate, "YYYY-MM-DD") & "' " & _
                 "ORDER BY effective_date DESC "
        Set MbdRate = Mpfcon.OpenRecordset(strSQL, dbOpenDynamic)
        MPFGetExchangeRate = MbdRate.Fields(0)
    Else
        'HKD
        MPFGetExchangeRate = 1
    End If

Exit Function
errorhandler:
    MPFGetExchangeRate = 1
    PblnReturn = False
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
'****** AC01 END ******

'****** AC02 BEGIN ******
Public Function MPFGetSecGroup(ByVal PstrCustID As String, _
                              ByVal Pstruserid As String, _
                              ByRef PblnReturn As Boolean) As String
                                
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rs As New ADODB.Recordset
    
    'Initial
    PblnReturn = False
    MPFGetSecGroup = ""

    'Get connected
    If dbcon.Connect(Pstruserid, PROJECT, CTRSEC) Then
        'Check with the security group
        strSQL = "Select secsug_group_id from sec_user_group where secsug_user_id = '" & Trim(PstrCustID) & "' "
        Set rs = dbcon.ExecuteStatement(strSQL, adUseClient, adOpenStatic, adLockOptimistic)
        If Not rs.EOF And Not IsNull(rs(0).Value) Then
            PblnReturn = True
            MPFGetSecGroup = Trim(rs(0).Value)
        End If
    End If
    
Exit Function
errorhandler:
    PblnReturn = False
    MPFGetSecGroup = ""
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function MPFGetFaxList(ByVal PstrODBC As String, _
                              ByVal PstrFundCode As String, _
                              ByVal PstrEmpCode As String, _
                              ByVal PstrRC As String, _
                              ByVal PstrEEER As String, _
                              ByVal PstrLang As String, _
                              ByVal Pstruserid As String, _
                              ByRef PblnReturn As Boolean) As ADODB.Recordset
                                
On Error GoTo errorhandler:
Dim strSQL As String
Dim strNumOfFax As String
Dim dbcon As New Dblogon.Dbconnect
Dim rs As New ADODB.Recordset
    
    'Initial
    PblnReturn = False
    Set MPFGetFaxList = Nothing

    'Get connected
    If dbcon.Connect(Pstruserid, PROJECT, FindConnection(PstrODBC)) Then
        strSQL = "SELECT max(mpffil_index) " & _
                 "FROM   mpf_file_list " & _
                 "WHERE ((mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_level = 'F') " & _
                 "OR     (mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_emp_code = '" & Trim(PstrEmpCode) & "' AND mpffil_level = 'E') " & _
                 "OR     (mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_emp_code = '" & Trim(PstrEmpCode) & "' AND mpffil_rc = '" & Trim(PstrRC) & "' AND mpffil_level = 'R')) " & _
                 "AND    mpffil_EE_ER = '" & Trim(PstrEEER) & "' " & _
                 "AND    mpffil_lang = '" & Trim(PstrLang) & "' " & _
                 "AND    mpffil_type = 'CTR' "
        Set rs = dbcon.ExecuteStatement(strSQL)
        If Not rs.EOF And Not IsNull(rs(0).Value) Then
            strNumOfFax = Trim(Str(rs(0).Value))

            strSQL = "SELECT '" & strNumOfFax & "', RTRIM(LTRIM(CONVERT(Varchar(2),mpffil_index))), mpffil_filename, mpffil_filepath, mpffil_voice " & _
                     "FROM   mpf_file_list " & _
                     "WHERE ((mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_level = 'F') " & _
                     "OR     (mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_emp_code = '" & Trim(PstrEmpCode) & "' AND mpffil_level = 'E') " & _
                     "OR     (mpffil_fund_code = '" & Trim(PstrFundCode) & "' AND mpffil_emp_code = '" & Trim(PstrEmpCode) & "' AND mpffil_rc = '" & Trim(PstrRC) & "' AND mpffil_level = 'R')) " & _
                     "AND    mpffil_EE_ER = '" & Trim(PstrEEER) & "' " & _
                     "AND    mpffil_lang = '" & Trim(PstrLang) & "' " & _
                     "AND    mpffil_type = 'CTR' " & _
                     "ORDER BY mpffil_index "
            Set MPFGetFaxList = dbcon.ExecuteStatement(strSQL)
            PblnReturn = True
        End If
    End If

Exit Function
errorhandler:
    PblnReturn = False
    Set MPFGetFaxList = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
'****** AC02 END ******

'****** AC03 BEGIN ******
Public Function MPFIsDBScheme(ByVal PstrODBC As String, _
                               ByVal PstrFundCode As String, _
                               ByVal PstrEmpCode As String, _
                               ByVal Pstruserid As String, _
                               ByRef PblnReturn As Boolean) As String
                                
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rs As New ADODB.Recordset
    
    'Initial
    PblnReturn = False
    MPFIsDBScheme = ""

    'Get connected
    If dbcon.Connect(Pstruserid, PROJECT, FindConnection(PstrODBC)) Then
        'mpf_employer_special_fields
        strSQL = "SELECT mpfesf_special_fields " & _
                 "FROM mpf_employer_special_fields " & _
                 "WHERE mpfesf_fund_code = '" & Trim(PstrFundCode) & "' " & _
                 "AND mpfesf_employer_code = '" & Trim(PstrEmpCode) & "' " & _
                 "AND mpfesf_table_index = 13 "
        Set rs = dbcon.ExecuteStatement(strSQL)
        If Not rs.EOF Then
            If Not IsNull(rs(0)) Then
                If Trim(rs(0)) = "Y" Then
                    MPFIsDBScheme = "YES"
                    PblnReturn = True
                Else
                    MPFIsDBScheme = "NO"
                    PblnReturn = True
                End If
            Else
                MPFIsDBScheme = "NULL"
                PblnReturn = True
            End If
        Else
            MPFIsDBScheme = "NOT FOUND"
            PblnReturn = True
        End If
    End If

Exit Function
errorhandler:
    PblnReturn = False
    MPFIsDBScheme = ""
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function

Public Function LIFEGetBillNo(ByVal Pstruserid As String, ByVal strPolicyNo As String) As String
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect
Dim rs As New ADODB.Recordset

    'Initial
    LIFEGetBillNo = ""
    
    'Get connected
    If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
        strSQL = "SELECT BillNo " & _
                 "FROM PolicyAccount " & _
                 "WHERE PolicyAccountID = '" & Trim(strPolicyNo) & "' " & _
                 "AND BillNo Is Not Null "
        Set rs = dbcon.ExecuteStatement(strSQL)
        If Not rs.EOF Then
            LIFEGetBillNo = Trim(rs(0))
        End If
    End If
    
Exit Function
errorhandler:
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function


Public Function LIFEGetNBItem(ByVal Pstruserid As String, ByVal strPolicyNo As String) As ADODB.Recordset
On Error GoTo errorhandler:
Dim strSQL As String
Dim dbcon As New Dblogon.Dbconnect

    'Initial
    Set LIFEGetNBItem = Nothing
    
    'Get connected
    If dbcon.Connect(Pstruserid, PROJECT, CTRSQL) Then
        strSQL = "SELECT DISTINCT nbruad_outstanding_code " & _
                 "FROM nbrvw_uw_advice " & _
                 "WHERE nbruad_policy_no = '" & Trim(strPolicyNo) & "' "
        Set LIFEGetNBItem = dbcon.ExecuteStatement(strSQL)
    End If
    
Exit Function
errorhandler:
    Set LIFEGetNBItem = Nothing
    Err.Raise Err.Number, Err.source, Err.Description, Err.HelpFile, Err.HelpContext
End Function
'****** AC03 END ******
