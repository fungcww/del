Attribute VB_Name = "PrintCard"
'******************************************************************************************
'*     Amended By : Anson Chan
'*     Date       : May 15 2001
'*     Reference  : AC01
'*     Description: Adding McDonald Into Card/PIN Letter Program
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : April 17 2002
'*     Reference  : EC01
'*     Description: Changing the UserName to NTloginName
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : June 11 2002
'*     Reference  : EC02
'*     Description: Bug Fix for insert agentCode
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : June 14 2002
'*     Reference  : EC03
'*     Description: if Member = 'A', Display the current address
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : Aug 07 2002
'*     Reference  : EC04
'*     Description: Bug Fix for getCurraddress and shell to printer
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : Aug 16 2002
'*     Reference  : EC05
'*     Description: Life Reprint Pin_letter T + 1, Reprint card/Letter T + 5
'*                  Pension Reprint Pin_letter T + 1, Reprint card/Letter T + 5
'*                  Schrd/Dresd PrintLetter for ORSO and MPF
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : Sep 02 2002
'*     Reference  : EC06
'*     Description: Reset Password Lock Flag to Zero - Pension and Life Case
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : Sep 11 2002
'*     Reference  : EC07
'*     Description: Bug Fix for ER Case print Letter
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Sept 09 2002
'*     Reference  : AY01
'*     Description: Use Chinese Address if UseChiInd in CustomerAddress is 'Y' for PIN
'*                  letter and survey
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Oct 09 2002
'*     Reference  : AY02
'*     Description: Add Daily Printing Report for Customer Survey
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Oct 11 2002
'*     Reference  : AY03
'*     Description: Review for the Logic of Life Printing Jobs.  Changes Include:
'*                  - Mark the print flags as 'A' if the records cannot be printed due to
'*                    no valid address available for PIN letter, survey and coupon print
'*                    job
'*                  - Chinese name can be used by PIN letter, survey and coupon printing
'*                  - Change the logic of first printing of card and PIN letter.  Check for
'*                    prtidxletter and prtidxcard = 'P' instead of <> 'R'
'*                  - Fix the bugs in logic of printing of PIN letter.  Check for
'*                    CountryCode instead of CoName for country checking
'*                  - Change the logic of coupon printing:
'*                      - Both Chinese and English name will be used in the header of the
'*                        coupon letter.  The name inside the coupon continues to display
'*                        English name only
'*                      - Check for all types of address instead of English residential
'*                        address (type 'R') only.
'*                      - More than one coupon will be printed if customer return more
'*                        than one survey.
'*                      - The formula for calculating the expiry date change from
'*                        (Print Date) + 6 months - 1 day to
'*                        (Print Date) - 1 day + 6 months
'*                      - The marking of print flag for successful cases is done after
'*                        printing instead of before printing.  It allows records to be
'*                        reprinted without additional production run after there are
'*                        printer failure
'*                  - Change the logic of report printing for survey that only successful
'*                    cases will be displayed
'*                  - Change the logic of label printing for the reprint cards.  Validity
'*                    of the address will be checked before printing labels.
'*                  - A bug fix in the function GetAddress to avoid errors for fields not
'*                    found and null value of countrycode returned
'*                  In this review, some assumption has been made
'*                  - For a customer, its company name must be valid if it is a company or
'*                    its English first name and last name must be valid if it is not a
'*                    company.  If the "Use Chinese Indicator" is 'Y', the customer must
'*                    have a valid Chinese company name for company customers or a valid
'*                    Chinese first name and last name for other customers
'*                  - The gender of the customer will not be null
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Nov 04 2002
'*     Reference  : AY04
'*     Description: Change of Logo in Dresdner, and only Generate Customer Letter instead
'*                  of Generate Customer Card and Letter
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Nov 08 2002
'*     Reference  : AY05
'*     Description: Generate error log for errors from printing cards
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Nov 14 2002
'*     Reference  : AY06
'*     Description: Bug fix for the problem of no data in reprint reports for MPF and ORSO
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Mar 31 2003
'*     Reference  : AY07
'*     Description: The following changes made:
'*                  - Remove calls to database table ORDCNA, which is not used now
'*                  - Change the path of saving the SOS Monthly Report from
'*                    \\Eaant2\prodapps\ciw\report\ to \\eaant2\prodapps\CIW\SOSRPT\
'******************************************************************************************
'*     Amended By : Franco Hui
'*     Date       : 6 Oct 2003
'*     Reference  : FH01
'*     Description: The following changes made:
'*                  - Print the label/survey/report/coupon through sip_server to the DP90 printer
'*                  - label format changes
'******************************************************************************************
'*     Amended By : Carmen Wong
'*     Date       : 21 Jan 2004
'*     Reference  : CW02
'*     Description: handle the ' in agent name
'******************************************************************************************
'*     Amended By : Winki Chan
'*     Date       : Oct 2007
'*     Reference  : WC102007
'*     Description: Add to print GI Medical and Non-Medical SOS Card
'******************************************************************************************
'*     Amended By : Sophia Choi
'*     Date       : Apr 2008
'*     Reference  : SC042008    or Choi
'*     Description: Reorder the key to print GI Medical and Non-Medical SOS Card
'*        global variable for displaying the last record in the message box
'******************************************************************************************
'*     Amended By : To Wai Lung
'*     Date       : Jun 2008
'*     Reference  : ITDTLW001
'*     Description: Crisis report for following plan
'*         1)       Crisis Fighter
'*         2)       CCA
'*         3)       CCP
'*         4)       CrisisPro
'*         5)       CrisiSaver
'*         6)       Aexcellady
'******************************************************************************************
'*     Amended By : To Wai Lung
'*     Date       : DEC 2008
'*     Reference  : ITDTLW002
'*     Description: Fix the record overflow problem
'******************************************************************************************
'*     Amended By : To Wai Lung
'*     Date       : JUL 2010
'*     Reference  : ITDTLW003
'*     Description: Include the following product in the crisis list and set
'*                  the following plan code read from csw_system_value
'*                  1) Crisis Fighter
'*                  2) Crisis Fighter Plus
'*                  3) Crisis Ease 80
'*                  4) Crisis Ease 100
'*                  Replace the new card print name to SP55 and print name read from
'*                  csw_system_value
'******************************************************************************************
'*     Amended By : Eric Shu
'*     Date       : Oct 2010
'*     Reference  : ES001
'*     Description: CNB get dispatch date from CIW
'******************************************************************************************
'*     Amended By : To Wai Lung
'*     Date       : JUL 2010
'*     Reference  : ITDTLW004
'*     Description: Include the following product in the SOS list and set
'*                  the following plan code read from csw_system_value
'*                  1) Embrace Medical Plan
'*                  2) Embrace Medical Rider
'******************************************************************************************
'*     Amended By : To Wai Lung
'*     Date       : JUL 2011
'*     Reference  : ITDTLW005
'*     Description: PHW Revamp Print Card and PIN Letter function for Macau
'******************************************************************************************


' Function for generate data definition file for Crystal Reports
' Not used in production
Declare Function CreateFieldDefFile Lib "p2smon.dll" (lpUnk As Object, _
     ByVal fileName As String, ByVal bOverWriteExistingFile As Long) _
     As Long

Option Explicit


'Public Const PRINTER_NAME = "ImageCard IV"
Public PRINTER_NAME As String
'Public Const PRINTER_NAME = "CutePDF Writer"

'Public Const LABEL_PRINTER_NAME = "Xerox DocuPrint N24/N32/N40"
'Public Const PRINTER_NAME = "\\CARD_PC\ImageCard IV"
'Public Const LABEL_PRINTER_NAME = "\\CARD_PC\Xerox DocuPrint N24/N32/N40"
Public Const LABEL_PRINTER_NAME = "LABEL"
Public Const SURVEY_PRINTER_NAME = "SURVEY"
Public Const COUPON_PRINTER_NAME = "COUPON"
Public Const REPORT_PRINTER_NAME = "REPORT"

'ITDTLW005
Public Const MCUSECURITY_USER = "MAESUSER"
Public Const MCUSECURITY_PROJECT = "MAES"
Public Const MCUSECURITY_CONNECT = "DBLOGON"

Public Const PROJECT = "CTR"
'Public Const USER = "EICADMIN"     'EC01
Public Const LETTER_PRINT_DAY = 14
Public Const SECURITY_CONNECT = "CTRSECCON"
Public ToDate As Date
Public Fromdate As Date
'Public db As New Dblogon.Dbconnect
Public db As Object
Dim dbSECSQL As New Dblogon.Dbconnect
Private InsRptTbl As New Dblogon.Dbconnect

Public bolFlag As Boolean       'EC01
Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long      'EC01
Public rec1 As New ADODB.Recordset  'EC01
Public pBolORSOFlag As Boolean      'EC05

Public Const s400User = "PHWUPDATE"
Public Const s400Proj = "PHW"
Public Const s400Conn = "UPT400"
Public Const s400Type = "UPDATE"

Public Const s400UserM = "ITOPER"
Public Const s400ProjM = "LAS"
Public Const s400ConnM = "CPSDB"
Public Const s400TypeM = "LASUPDATE"

'display last record  SC042008 choi
Public GIMSG As String


Public Function GetPrinter(ByRef prtTemp As Printer, ByVal Printname As String) As Boolean
On Error GoTo errhandler
For Each prtTemp In Printers
    If prtTemp.DeviceName = Printname Then
        GetPrinter = True
        Exit Function
    End If
GetPrinter = False
Next
Exit Function
errhandler:
    err.Raise err.number, err.Source, err.Description, err.HelpFile, err.HelpContext
End Function
Public Function Print_Label(ByVal CustId As String, ByVal Name As String, _
                            ByVal row As Integer, ByVal col As Integer, ByVal Pstrdb As String) As Boolean
On Error Resume Next
Dim rsAdd As ADODB.Recordset
Dim i As Integer

    '** Change for AY03 start **
    ' Check for available address exists or not before print label
    Set rsAdd = GetAddress(CustId, Pstrdb)
    If Not (rsAdd Is Nothing) Then
        Printer.Font = "Arial"
        Printer.FontSize = 9
        
        Printer.CurrentX = 400 + col * 5800
        Printer.CurrentY = 100 + row * 2100
        Printer.Print Name
        For i = 0 To 3
            Printer.CurrentX = 400 + col * 5800
            Printer.CurrentY = 350 + i * 250 + row * 2100
            Printer.Print rsAdd.Fields(i).Value
        Next i
        Printer.CurrentX = 4000 + col * 5800
        Printer.CurrentY = 1250 + row * 2100
        Printer.Print CustId
        Print_Label = True
    Else
        Print_Label = False
    End If
    '** Change for AY03 end **
    Set rsAdd = Nothing
End Function
Public Function AddOne(ByRef row As Integer, ByRef col As Integer) As Boolean
col = col + 1
If col > 1 Then
    row = row + 1
    col = 0
    If row > 7 Then
        row = 0
        col = 0
        AddOne = True
        Exit Function
    End If
End If
AddOne = False
End Function
Public Sub Print_Label_Job(rsCust As ADODB.Recordset, Pstrdb As String)
Dim Name As String
Dim CustId As String
Dim prt As Printer
Dim row As Integer
Dim col As Integer
Dim result As Integer
Dim strsql As String
    
    row = 0
    col = 0
    rsCust.MoveFirst
    If GetPrinter(prt, LABEL_PRINTER_NAME) Then
        Set Printer = prt
        result = MsgBox("Please Change the Label to the Printer " & LABEL_PRINTER_NAME & "!!", vbYesNo)
        If result = vbYes Then
            While Not rsCust.EOF
                CustId = Trim(rsCust.Fields(0).Value)
                Name = GetName(CustId)
                If (Print_Label(CustId, Name, row, col, Pstrdb) = True) Then
                    If AddOne(row, col) Then
                        Printer.NewPage
                    End If
                Else
                    strsql = "insert into csw_print_errlog values ('" & CustId & "',getdate(),'Cannot print label for the reprint card.  Reason: No valid address for the customer.')"
                    Call db.ExecuteStatement(strsql)
                End If
                rsCust.MoveNext
            Wend
            Printer.EndDoc
        End If
    Else
        MsgBox "Label Printer NOT Found.", vbCritical
    End If
End Sub
Public Function Print_Job(ByVal PstrCardType As String, ByVal Pstrdb As String, _
                          ByVal PstrForR As String, ByRef PlngCard As Long) As Long
On Error GoTo errhandler
Dim rs As New ADODB.Recordset
Dim strsql As String
Dim strsql2 As String
Dim para1 As String
Dim para2 As String
Dim para4 As String
Dim paraEmail As String
Dim rs1 As New ADODB.Recordset
Dim rs2 As New ADODB.Recordset
Dim rs3 As New ADODB.Recordset
'Add WC102007
Dim PrintCardOK As Boolean
'End Add WC102007

'Connection Variable
'ITDTLW005 Start ***********************************************************************
Dim a400User As String
Dim a400Proj As String
Dim a400Conn As String
Dim a400Type As String
Dim NBUser As String
Dim NBProj As String
Dim NBdb As String
Dim CIWUser As String
Dim CIWProj As String
Dim CIWdb As String
Dim LIB As String

If Pstrdb = "MCU" Then
    a400User = s400UserM
    a400Proj = s400ProjM
    a400Conn = s400ConnM
    a400Type = s400TypeM
    NBUser = "ITOPER"
    NBProj = "LAS"
    NBdb = "MCUNBRPRD01"
    CIWUser = "ITOPER"
    CIWProj = "LAS"
    CIWdb = "MCUCIWPRD01"
    LIB = "eaadtm."
Else
    a400User = s400User
    a400Proj = s400Proj
    a400Conn = s400Conn
    a400Type = s400Type
    NBUser = "ITOPER"
    NBProj = "NBR"
    NBdb = "NBRDB"
    CIWUser = "ITOPER"
    CIWProj = "POS"
    CIWdb = "CIWDB"
    LIB = "eaadta."
End If
'ITDTLW005 End ***********************************************************************

PlngCard = 0
Print_Job = 0
    If Get_Connect(Pstrdb) Then
    
        'MsgBox "Get Data"
    
        Set rs = Get_Print_Data(PstrCardType, PstrForR)
        If UCase(Trim(PstrCardType)) = UCase(Trim("Medical SOS Card")) Or _
           UCase(Trim(PstrCardType)) = UCase(Trim("Non Medical SOS Card")) Then
            MsgBox (rs.RecordCount & " will be print out")
        End If
        
        'MsgBox rs.RecordCount
        
        If Not rs.EOF Then
            While Not rs.EOF
                If PstrCardType = "csw" Then
                    'PW0001 begin
                    ' Check Freelook date start
                    If PstrForR <> "R" Then   'not reprint
                        ' Check despatch date
                        Dim strDSQL As String
                        Dim strFLDate As String
                        Dim objDBS As Object
                        Dim objRS As ADODB.Recordset
                        Dim blnFnd As Boolean
                    
                        blnFnd = False
                        'Set objDBS = CreateObject("dblogon.dbconnect")
                        Set objDBS = CreateObject("dbsecurity.database")
                        
                        strDSQL = "Select PORGDT " & _
                        " From " & LIB & "ordupo " & _
                        " Where popono = '" & rs("cswpcr_pid") & "'" & _
                        " And pocomp = ''"
                    
                        'If objDBS.Connect(s400User, s400Proj, s400Conn, s400Type) Then
                        If objDBS.Connect(a400User, a400Proj, a400Conn, a400Type) Then
                            Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                            
                            If Not objRS Is Nothing Then
                                If objRS.RecordCount > 0 Then
                                                                    
                                    If IsNull(objRS("PORGDT")) Then blnFnd = False
                                    If Len(objRS("PORGDT")) <> 7 Then blnFnd = False
                                    
                                    strFLDate = Mid(objRS("PORGDT"), 4, 2) & "/" & Right(objRS("PORGDT"), 2) & "/" & Left(objRS("PORGDT"), 3) + 1800
                                    If IsDate(strFLDate) Then
                                        
                                        ' 1 days after despatch date
                                        If DateAdd("d", 1, CDate(strFLDate)) > Date Then
                                            GoTo NextCard
                                        End If
                                        
                                        blnFnd = True
                                    Else
                                        blnFnd = False
                                    End If
                                    
                                End If
                            End If

                            Set objRS = Nothing
                            objDBS.Disconnect
                        End If
                        
                        'MsgBox "FL=" & strFLDate

                        ' Check Life/Asia policy from nbr_policy_master
                        If blnFnd = False Then
                                                   
                            Set objDBS = CreateObject("dbsecurity.database")
                            'If objDBS.Connect("ITOPER", "NBR", "NBRDB") Then
                            If objDBS.Connect(NBUser, NBProj, NBdb) Then
                           
                                strDSQL = "select nbrpmr_dispatch_date from nbr_policy_master where nbrpmr_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                
                                If Not objRS Is Nothing Then
                                    If objRS.RecordCount > 0 Then
                                    
                                        'MsgBox strDSQL
                                        'MsgBox "DD=" & CStr(objRS("nbrpmr_dispatch_date"))
                                        
                                        'disable the check of dispatch date at Macau
                                        If Pstrdb = "MCU" Then
                                           blnFnd = True
                                        Else
                                            If IsNull(objRS("nbrpmr_dispatch_date")) Then GoTo NextCard
                                            If objRS("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCard
                                        
                                            If DateAdd("d", 1, objRS("nbrpmr_dispatch_date")) > Date Then
                                                GoTo NextCard
                                            End If
                                            
                                            blnFnd = True
                                        End If
'                                    Else
'                                        GoTo NextCard
                                    End If
'                                Else
'                                    GoTo NextCard
                                End If
                            End If
                            
                            Set objRS = Nothing
                            objDBS.Disconnect
                            
                        End If
                        ' End
                        
                        ' ES001 begin -- Check Life/Asia policy from CNB
                        If blnFnd = False Then
                                                   
                            Set objDBS = CreateObject("dbsecurity.database")
                            'If objDBS.Connect("ITOPER", "POS", "CIWDB") Then
                            If objDBS.Connect(CIWUser, CIWProj, CIWdb) Then
                           
                                strDSQL = "select convert(datetime,ciwdh_dispatch_date) as nbrpmr_dispatch_date from ciwpr_dispatch_hist where ciwdh_dispatched = 'Y' and ciwdh_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                
                                If Not objRS Is Nothing Then
                                    If objRS.RecordCount > 0 Then
                                    
                                        'MsgBox strDSQL
                                        'MsgBox "DD=" & CStr(objRS("nbrpmr_dispatch_date"))
                                    
                                        If IsNull(objRS("nbrpmr_dispatch_date")) Then GoTo NextCard
                                        If objRS("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCard
                                        
                                        If DateAdd("d", 1, objRS("nbrpmr_dispatch_date")) > Date Then
                                            GoTo NextCard
                                        End If
                                        
                                        blnFnd = True
                                        
                                    Else
                                        GoTo NextCard
                                    End If
                                Else
                                    GoTo NextCard
                                End If
                            End If
                            
                            Set objRS = Nothing
                            objDBS.Disconnect
                            
                        End If
                        ' ES001 End
                    End If
                   
                    ' Check Freelook date end
                    'PW0001 end
                    
                    para1 = Trim(rs.Fields(0).Value)
                    para2 = GetName(para1)
                    para4 = "E"
'                    Set rs3 = GetAddress(para1)
'                    If PstrForR = "F" Then
'                        paraEmail = GetEmailAddr(para1)
'                        Call frmMenu.PrtCustSurvey(para1, para2, rs3(0), rs3(1), rs3(2), rs3(3), paraEmail, rs3(5))
'                    End If
                Else
                'Add WC102007    Put Policy=para1,  Insured name=para2,   company=para4
                    If UCase(Trim(PstrCardType)) = UCase(Trim("Medical SOS Card")) Or _
                       UCase(Trim(PstrCardType)) = UCase(Trim("Non Medical SOS Card")) Then
                         'output format 3 paras only  SC042008 Choi
                       para1 = rs.Fields("PolicyNo") & "      Ref. No.: " & rs.Fields("Employee") & "-" & rs.Fields("RelCde") ' Policy No  Emp-EE
                       para2 = rs.Fields("CustName")   ' Customer Name
                       para4 = rs.Fields("CompanyName")   ' Company Name
                Else
                'End Add WC102007
                    If PstrForR = "F" Then
                       If Trim(rs.Fields(1)) = "E" Then
                          strsql = "select rtrim(mpfmbd_surname) + ' ' + rtrim(mpfmbd_given_names) as Name from mpf_member_basic_details where " _
                                  + "mpfmbd_fund_code = '" & Trim(rs.Fields(2)) & "' and mpfmbd_member_number = '" & CStr(Trim(rs.Fields(5))) & "'"
                          Set rs1 = db.ExecuteStatement(strsql)
                          para1 = Trim(rs1.Fields(0))
                          PlngCard = PlngCard + 1
                       Else
                          strsql = "select mpfrcd_description, mpfrcd_company_name from mpf_reporting_centre where mpfrcd_fund_code = '" & Trim(rs.Fields(2)) & "' " _
                                 + "and mpfrcd_employer_code = '" & Trim(rs.Fields(3)) & "' and mpfrcd_reporting_centre = '" & Trim(rs.Fields(4)) & "'"
                          Set rs1 = db.ExecuteStatement(strsql)
                          para1 = Trim(rs1.Fields(0)) + " " + GetEmpName(rs1)
                       End If
                       para2 = Trim(rs.Fields(0))
                    Else
                        strsql2 = "select mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num from mpf_pin_details where mpfpid_cid = '" & Trim(rs.Fields(0)) & "' order by mpfpid_date_time "
                        Set rs2 = db.ExecuteStatement(strsql2)
                        
                        If Trim(rs.Fields(1)) = "E" Then
                            strsql = "select rtrim(mpfmbd_surname) + ' ' + rtrim(mpfmbd_given_names) as Name from mpf_member_basic_details where " _
                                    + "mpfmbd_fund_code = '" & Trim(rs2.Fields(0)) & "' and mpfmbd_member_number = '" & CStr(Trim(rs2.Fields(3))) & "'"
                            Set rs1 = db.ExecuteStatement(strsql)
                            para1 = Trim(rs1.Fields(0))
                            PlngCard = PlngCard + 1
                        Else
                            strsql = "select mpfrcd_description, mpfrcd_company_name from mpf_reporting_centre where mpfrcd_fund_code = '" & Trim(rs2.Fields(0)) & "' " _
                                    + "and mpfrcd_employer_code = '" & Trim(rs2.Fields(1)) & "' and mpfrcd_reporting_centre = '" & Trim(rs2.Fields(2)) & "'"
                            Set rs1 = db.ExecuteStatement(strsql)
                            para1 = Trim(rs1.Fields(0)) + " " + GetEmpName(rs1)
                        End If
                        para2 = Trim(rs.Fields(0))
                    End If
                    para4 = Trim(rs.Fields(1))
                End If
                End If
                
                'MsgBox "Print Card"
                
                'Add WC102007  -- If statement change to select case. GI will pass PstrCardType to 3rd para
                'If PrintCard(para1, para2, Pstrdb, para4) Then
                Select Case UCase(Trim(Pstrdb))
                Case "GI"
                   PrintCardOK = PrintCard(para1, para2, PstrCardType, para4)
                   GIMSG = " The last one= " & para1 & "," & para2 & "," & para4  'choi
                Case Else
                   PrintCardOK = PrintCard(para1, para2, Pstrdb, para4)
                End Select
                If PrintCardOK Then
                
                    Select Case UCase(Trim(Pstrdb))
                    Case "GI"
                    If Upd_GUDB(rs.Fields("PolicyNo").Value, rs.Fields("endorsement").Value, rs.Fields("CustID")) = False Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Card Database for this user!!')"
                        Call db.ExecuteStatement(strsql)
                    End If
                    Case Else
                'End Add WC102007
                    If Upd_DB(PstrCardType, rs.Fields(0).Value) = False Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Card Database for this user!!')"
                        Call db.ExecuteStatement(strsql)
                    End If
                    End Select
                    
                    'PW0001 begin
                    Print_Job = Print_Job + 1
                    'PW0001 end
                    
                End If
                
NextCard:
                rs.MoveNext
                Set objRS = Nothing
                Set objDBS = Nothing
            Wend
            Printer.EndDoc
            'PW0001 begin
            'Print_Job = rs.RecordCount
            'PW0001 end
            If PstrCardType = "csw" And PstrForR = "R" Then
                '** Change for AY03 start **
                ' Change text in combo box when printing labels
                ' Main purpose is to able to get Chinese name
                frmMenu.cmbJob.Text = "Label"
                frmMenu.cmbForR = ""
                Call Print_Label_Job(rs, Pstrdb)
                frmMenu.cmbJob.Text = "Card"
                frmMenu.cmbForR = "RePrint"
                '** Change for AY03 end
            End If
        End If
        Call Disconnect
    End If
    Set rs = Nothing
    Set rs1 = Nothing
Exit Function
errhandler:
    'Add WC102007 -- GI will not add record to  _print_errlog
    If Pstrdb = "GI" Then
       strsql = "INSERT egs_Sos_Log values ('" & rs.Fields("Custid") & "','" & rs.Fields("PolicyNo") & "',getdate(),'Print Card Failed. " & Trim(Mid(err.Source & err.Description, 1, 100)) & "')"
        Call db.ExecuteStatement(strsql)
    Else
    'End Add WC102007
        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Print Card !!" & Left(err.Description, 75) & "')"
        Call db.ExecuteStatement(strsql)
    End If        'Add WC102007
    Resume Next
End Function
Public Function Get_Connect(ByVal Pstrdb As String) As Boolean
On Error GoTo errhandler

    Set db = Nothing
    
    'If db.Connect(USER, PROJECT, ChooseConnection(Pstrdb)) Then    'EC01
    
    'Add WC102007  - If pass in pstrdb = GI, connect to gi database
    Select Case Pstrdb
    Case "GI"
        Set db = CreateObject("dblogon.dbconnect")
        If db.Connect("ECARGOUSER", "ECARGO", "ECARGOCON") Then
            Get_Connect = True
        Else
            Get_Connect = False
        End If
    ' ES01 - Add Macau connection
    Case "MCU"
        'If db.Connect("MAESUSER", "MAES", "CIW", "AGENCY") Then
        Set db = CreateObject("dbsecurity.database")
        If db.Connect("ITOPER", "LAS", "MCUCIWPRD01", "LASUPDATE") Then
            Get_Connect = True
        Else
            Get_Connect = False
        End If
    Case Else
    'End Add WC102007
        Set db = CreateObject("dblogon.dbconnect")
        If db.Connect(GetNTUserName, PROJECT, ChooseConnection(Pstrdb)) Then     'EC01
        'If db.Connect("eicadmin", PROJECT, ChooseConnection(Pstrdb)) Then  ' For development only
        'If db.Connect("itdkcc", PROJECT, ChooseConnection(Pstrdb)) Then  ' For development only
            Get_Connect = True
        Else
            Get_Connect = False
        End If
    End Select  'Add WC102007
'    If db.Connect("ITOPER", PROJECT, ChooseConnection(Pstrdb)) Then     'EC01
      
Exit Function
errhandler:
    Set db = Nothing
    Get_Connect = False
End Function
Public Function Disconnect()
    If db.IsConnected = True Then
        Call db.Disconnect
    End If
    Set db = Nothing
End Function
Public Function Get_Print_Data(ByVal PstrCardType As String, ByVal PstrForR As String) As ADODB.Recordset
On Error GoTo errhandler
    Dim strsql As String
    If PstrCardType = "csw" Then
        If PstrForR = "F" Then
            '** Query amended for AY03 **
''            strsql = "Select distinct(cswpcr_cid),cswpcr_pid,cswpuw_infor_date " & _
''                     "From csw_print_cardletter_report , csw_policy_uw " & _
''                     "Where cswpcr_crtdate < '" & Format(Now, "yyyy/mm/dd") & "' and cswpcr_prtedcard = 'N' " & _
''                     "and cswpcr_prtidxcard = 'P' and cswpcr_pid = cswpuw_poli_id " & _
''                     "order by cswpuw_infor_date, cswpcr_pid"
'''            strsql = "Select distinct(cswpcr_cid),cswpcr_pid,cswpuw_infor_date " & _
'''                     "From csw_print_cardletter_report , csw_policy_uw " & _
'''                     "Where cswpcr_crtdate < '" & Format(Now, "yyyy/mm/dd") & "' and cswpcr_prtedcard = 'N' " & _
'''                     "and cswpcr_prtidxcard = 'P' and cswpcr_pid = cswpuw_poli_id " & _
'''                     "order by cswpcr_pid"

            strsql = "Select distinct(cswpcr_cid),cswpcr_pid, '19000101' as cswpuw_infor_date " & _
                     "From csw_print_cardletter_report " & _
                     "Where cswpcr_crtdate < '" & Format(Now, "yyyy/mm/dd") & "' and cswpcr_prtedcard = 'N' " & _
                     "and cswpcr_prtidxcard = 'P' " & _
                     "order by cswpcr_pid"
        Else
            strsql = "select cswpcr_cid From csw_print_cardletter_report " & _
                     "Where cswpcr_crtdate < '" & Format(Now, "yyyy/mm/dd") & "' and cswpcr_prtedcard = 'N' " & _
                     "and cswpcr_prtidxcard = 'R' order by cswpcr_cid"
        End If
    Else
        If PstrForR = "F" Then
            '// AC01 Changed Begin //
            '//AC01 Old//strsql = "select mpfpcr_cid, mpfpcr_cust_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num from mpf_print_cardletter_report, mpf_pin_details where mpfpcr_prtidxcard = 'P' and convert(char(10), mpfpcr_cid) = mpfpid_cid and mpfpcr_prtedcard = 'N' and mpfpcr_fund = mpfpid_fund and mpfpcr_emp = mpfpid_emp and mpfpcr_rc = mpfpid_rpc order by convert(varchar(12), mpfpcr_crtdate,103), mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num"
            If frmMenu.cmbCom = "McDonald" Then '*** Hard code with Fund Code = 013 ***
                strsql = "SELECT mpfpcr_cid, mpfpcr_cust_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details " & _
                         "WHERE mpfpid_fund = '013' " & _
                         "AND mpfpcr_prtidxcard = 'P' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND mpfpcr_fund = mpfpid_fund " & _
                         "AND mpfpcr_emp = mpfpid_emp " & _
                         "AND mpfpcr_rc = mpfpid_rpc " & _
                         "ORDER BY convert(varchar(12), mpfpcr_crtdate,103), mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_name1, mpfpid_name2"
            ElseIf frmMenu.cmbCom = "ORSO" Then
                strsql = "SELECT mpfpcr_cid, mpfpcr_cust_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details, mpf_fund_special_fields " & _
                         "WHERE mpfpid_fund NOT IN ('013','200','203','303') " & _
                         "AND mpfpcr_prtidxcard = 'P' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND mpfpcr_fund = mpfpid_fund " & _
                         "AND mpfpcr_emp = mpfpid_emp " & _
                         "AND mpfpcr_rc = mpfpid_rpc " & _
                         "AND mpfpid_fund = fund_code " & _
                         "AND table_index = 3 " & _
                         "AND special_fields = 'ING ORSO' " & _
                         "ORDER BY convert(varchar(12), mpfpcr_crtdate,103), mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num"
            'Add WC102007 - found Printed record string for GI
            'ADD SC042008 Choi - add extra fields --old:egssom_ProductGroup, endorse, egssom_PolicyNo
            ElseIf frmMenu.cmbCom = "GI" Then
            'Change WC052012 - change printing seq to 'egssom_Company,egssom_ProductGroup, endorse, egssom_PolicyNo,egssom_Subsidiary, egssom_SF1, egssom_EmployeeName,  (CASE WHEN egssom_RelSeq= 'EE' THEN 1  when egssom_RelSeq = 'SP' then 2 when egssom_RelSeq = 'CH' then 3 END)
            '                         "      order by egssom_Company,egssom_ProductGroup, endorse, egssom_PolicyNo,egssom_Subsidiary,egssom_Employee,egssom_RelSeq"
                strsql = "select egssom_ProductGroup, egssom_Endor as endorsement , egssom_PolicyNo as PolicyNo, endorse = CASE WHEN egssom_Endor= 0 THEN 0  when egssom_Endor > 0 then 1 END, " & _
                         "    egssom_InsuredName as CustName, egssom_CompanyName as companyName, egssom_InsuredCode as CustID,  egssom_Product, " & _
                         "    egssom_Company as Company, egssom_Employee as Employee, egssom_Subsidiary as Subsidiary,  egssom_RelCde as RelCde, " & _
                         "    egssom_RelSeq as RelSeq " & _
                         "    From gishare.dbo.egs_Sos_setup, gishare.dbo.egs_Sos_Member " & _
                         "    Where egssos_Product = egssom_Product " & _
                         "      and egssos_Type = '" & Trim(UCase(frmMenu.cmbJob)) & "'" & _
                         "      order by egssom_Company,egssom_ProductGroup, endorse, egssom_PolicyNo,egssom_Subsidiary, egssom_SF1, egssom_EmployeeName,  (CASE WHEN egssom_RelSeq= 'EE' THEN 1  when egssom_RelSeq = 'SP' then 2 when egssom_RelSeq = 'CH' then 3 END) "
            '            "      order by egssom_Company,egssom_ProductGroup, endorse, egssom_PolicyNo,egssom_Subsidiary,egssom_Employee,egssom_RelSeq"
            'End Add WC102007
            Else
                strsql = "SELECT mpfpcr_cid, mpfpcr_cust_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details, mpf_fund_special_fields " & _
                         "WHERE mpfpid_fund <> '013' " & _
                         "AND mpfpcr_prtidxcard = 'P' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND mpfpcr_fund = mpfpid_fund " & _
                         "AND mpfpcr_emp = mpfpid_emp " & _
                         "AND mpfpcr_rc = mpfpid_rpc " & _
                         "AND mpfpid_fund = fund_code " & _
                         "AND table_index = 3 " & _
                         "AND special_fields <> 'ING ORSO' " & _
                         "ORDER BY convert(varchar(12), mpfpcr_crtdate,103), mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num"
            End If
        Else
            '//AC01 Old//strsql = "select distinct(mpfpcr_cid), mpfpid_pin_flag, convert(varchar(12),mpfpcr_crtdate,103) as date from mpf_print_cardletter_report, mpf_pin_details where mpfpcr_prtidxcard = 'R' and mpfpcr_prtedcard = 'N' and convert(char(10), mpfpcr_cid) = mpfpid_cid order by convert(varchar(12),mpfpcr_crtdate,103), mpfpcr_cid"
            If frmMenu.cmbCom = "McDonald" Then '*** Hard code with Fund Code = 013 ***
                strsql = "SELECT distinct(mpfpcr_cid), mpfpid_pin_flag, convert(varchar(12),mpfpcr_crtdate,103) as date " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details " & _
                         "WHERE mpfpid_fund = '013' " & _
                         "AND mpfpcr_prtidxcard = 'R' " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "ORDER BY convert(varchar(12),mpfpcr_crtdate,103), mpfpcr_cid"
            ElseIf frmMenu.cmbCom = "ORSO" Then
                strsql = "SELECT distinct(mpfpcr_cid), mpfpid_pin_flag, convert(varchar(12),mpfpcr_crtdate,103) as date, mpfpcr_cust_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details, mpf_fund_special_fields  " & _
                         "WHERE mpfpid_fund NOT IN ('013','200','203','303') " & _
                         "AND mpfpcr_prtidxcard = 'R' " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "AND mpfpid_fund = fund_code " & _
                         "AND table_index = 3 " & _
                         "AND special_fields = 'ING ORSO' " & _
                         "ORDER BY convert(varchar(12),mpfpcr_crtdate,103), mpfpcr_cid"
            Else
                strsql = "SELECT distinct(mpfpcr_cid), mpfpid_pin_flag, convert(varchar(12),mpfpcr_crtdate,103) as date " & _
                         "FROM mpf_print_cardletter_report, mpf_pin_details, mpf_fund_special_fields  " & _
                         "WHERE mpfpid_fund <> '013' " & _
                         "AND mpfpcr_prtidxcard = 'R' " & _
                         "AND mpfpcr_prtedcard = 'N' " & _
                         "AND convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
                         "AND mpfpid_fund = fund_code " & _
                         "AND table_index = 3 " & _
                         "AND special_fields <> 'ING ORSO' " & _
                         "ORDER BY convert(varchar(12),mpfpcr_crtdate,103), mpfpcr_cid"
            End If
            '// AC01 Changed End //
        End If
    End If
    Set Get_Print_Data = db.ExecuteStatement(strsql)
Exit Function
errhandler:
    Set Get_Print_Data = Nothing
End Function

'For GI Medical Card / Non Medical Card, Para 4 (PstrFlag) will store Company Name
Public Function PrintCard(ByVal PstrID As String, ByVal PstrName As String, ByVal Pstrdb As String, ByVal PstrFlag As String) As Boolean
On Error GoTo errhandler
Dim colName As Collection
Dim i As Integer
'** Added AY05 start **
Dim strsql As String
'** Added AY05 end **

    Printer.Font = "Arial Narrow"
    Select Case Pstrdb
        Case "Aetna"
            Printer.FontSize = 7
            Set colName = Truncate_name(PstrID)
            For i = 1 To colName.Count
                Printer.CurrentX = 1330
                Printer.CurrentY = 2050 + i * 160
                Printer.Print colName.Item(i)
            Next i
            Printer.CurrentX = 1330
            Printer.CurrentY = 2565
            Printer.Print PstrName
        Case "Dresd"
            '** Change for AY04 start **
            '** Comment the codes which are for generate customer card
            'If PstrFlag = "E" Then
                'Printer.FontSize = 10
                'Printer.CurrentX = 145
                'Printer.CurrentY = 1700
                'Printer.Print PstrID
                'Printer.FontSize = 9
                'Printer.CurrentX = 1330
                'Printer.CurrentY = 2109
                'Printer.Print PstrName
            'Else
                PrintCard = True
                Exit Function
            'End If
            '** Change for AY04 end **
        Case "Schrd"
            If PstrFlag = "E" Then
                Printer.FontSize = 10
                Set colName = Truncate_name(PstrID)
                For i = 1 To colName.Count
                    Printer.CurrentX = 1450
                    Printer.CurrentY = 2150 + i * 190
                    Printer.Print colName.Item(i)
                Next i
                Printer.FontSize = 9
                Printer.CurrentX = 1450
                Printer.CurrentY = 2750
                Printer.Print PstrName
            Else
                PrintCard = True
                Exit Function
            End If
        Case "Life", "MCU" 'ITDTLW005 add Macau
            Printer.FontSize = 7
            Printer.CurrentX = 1050
            Printer.CurrentY = 2260
            Printer.Print PstrID
            Printer.CurrentX = 1050
            Printer.CurrentY = 2560
            Printer.Print PstrName
        Case "McDonald"
            Printer.FontSize = 7
            Set colName = Truncate_name(PstrID)
            For i = 1 To colName.Count
                Printer.CurrentX = 1330
                Printer.CurrentY = 2050 + i * 160
                Printer.Print colName.Item(i)
            Next i
            Printer.CurrentX = 1330
            Printer.CurrentY = 2565
            Printer.Print PstrName
        'Add WC102007  - Print ING Card
        Case "Medical SOS Card"
            'Print Policy no and Insured name
            Printer.Font = "Arial Narrow"
            Printer.FontSize = 9
            
            'Company Name
            Printer.CurrentX = 330
            Printer.CurrentY = 730
            Printer.Print PstrFlag
            'Policy No
            Printer.CurrentX = 1325
            Printer.CurrentY = 2260
            Printer.Print PstrID
            'Insured Name
            Set colName = Truncate_name(PstrName)
            For i = 1 To colName.Count
                Printer.CurrentX = 1325
                Printer.CurrentY = 2500 + i * 160
                Printer.Print colName.Item(i)
            Next i
        Case "Non Medical SOS Card"
            'Print Insured name Only
            Printer.Font = "Arial Narrow"
            Printer.FontSize = 9
            Set colName = Truncate_name(PstrName)
            For i = 1 To colName.Count
                Printer.CurrentX = 1325
                Printer.CurrentY = 2250 + i * 160
                Printer.Print colName.Item(i)
            Next i
        'End Add WC102007
    End Select
    
    Printer.NewPage
    PrintCard = True

Exit Function
errhandler:
    '** Added AY05 start **
    If Pstrdb = "Aetna" Then
        strsql = "INSERT csw_print_errlog values (" & PstrID & ",getdate(),'Print Card Failed. " & err.Source & err.Description & "')"
    Else
        'Add WC102007  - GI will not add log
        If Pstrdb = "GI" Then
            strsql = "INSERT egs_Sos_Log values ('" & Trim(Mid(PstrName, 1, 50)) & "','" & PstrID & "',getdate(),'Print Card Failed. " & err.Source & err.Description & "')"
        Else
        'End Add WC102007
            strsql = "INSERT mpf_print_errlog values (" & PstrID & ",getdate(),'Print Card Failed. " & err.Source & err.Description & "')"
        End If   'Add WC102007
    End If
    '** Added AY05 end **
    Call db.ExecuteStatement(strsql)
    PrintCard = False
End Function
Public Function Upd_DB(ByVal PstrCardType As String, ByVal PstrID As String) As Boolean
On Error GoTo errhandler
    Dim strsql As String
    Dim strsql1 As String
    strsql = "Update " & PstrCardType & "_print_control set " & _
             PstrCardType & "prc_card_ind = 'N', " & PstrCardType & "prc_lstprtdate = " & _
             "getdate(), " & PstrCardType & "prc_printcount_card = (" & PstrCardType & "prc_printcount_card + 1) " & _
             "where " & PstrCardType & "prc_cust_id = " & PstrID
    strsql1 = "Update " & PstrCardType & "_print_cardletter_report set " & _
              PstrCardType & "pcr_prtedcard = 'Y', " & PstrCardType & "pcr_lstprtdatecard = getdate() where " & _
              PstrCardType & "pcr_prtedcard = 'N' and " & PstrCardType & "pcr_cid = " & PstrID
    Call db.ExecuteStatement(strsql)
    Call db.ExecuteStatement(strsql1)
    Upd_DB = True
Exit Function
errhandler:
    Upd_DB = False
End Function

Public Function Upd_GUDB(ByVal strPolicy As String, ByVal strEndor As String, _
ByVal strCustID As String) As Boolean
On Error GoTo errhandler
    Dim strsql As String
    
    strsql = "delete From gishare.dbo.egs_SOS_Member_History Where egssoh_LastUpdate_Date < getdate() - 5"
    Call db.ExecuteStatement(strsql)
    
    strsql = "Insert into gishare.dbo.egs_SOS_Member_History " & _
             "    select * from gishare.dbo.egs_SOS_Member where egssom_PolicyNo = '" & strPolicy & "'" & _
             "   and egssom_Endor = '" & strEndor & "'" & _
             "   and egssom_InsuredCode = '" & strCustID & "'"
    Call db.ExecuteStatement(strsql)

    strsql = "Delete from gishare.dbo.egs_SOS_Member where egssom_PolicyNo = '" & strPolicy & "'" & _
             "   and egssom_Endor = '" & strEndor & "'" & _
             "   and egssom_InsuredCode = '" & strCustID & "'"
    Call db.ExecuteStatement(strsql)
    Upd_GUDB = True
Exit Function
errhandler:
    Upd_GUDB = False
End Function

Public Function ChooseConnection(ByVal Pstrdbc As String) As String
Select Case Pstrdbc
        Case "Aetna"
            ChooseConnection = "CTRMPFAETSQLCON"
        Case "Dresd"
            ChooseConnection = "CTRMPFDRESQLCON"
        Case "Schrd"
            ChooseConnection = "CTRMPFSCHSQLCON"
        Case "Life"
            ChooseConnection = "CTRSQLCON"
        Case "McDonald"
            ChooseConnection = "CTRMPFAETSQLCON"
        Case "ICR"
            ChooseConnection = "CTRICRSQLCON"
 End Select
End Function
Public Function GetNameRS(ByVal CustId As String) As ADODB.Recordset
    Dim strsql As String
    
    '** Change for AY03 start **
    ' Always get English name for printing cards and reports
    ' Get Chinese name for printing other things if UseChiInd ='Y'
    If frmMenu.cmbJob.Text = "Card" Or frmMenu.cmbJob.Text = "Report" Then
        strsql = "Select Case When Gender = 'C' Then CoName Else rtrim(namesuffix)+ ' ' + rtrim(firstname)End As Name, " & _
                 "gender, coname, namesuffix, firstname, pinnumber, countrycode, agentcode " & _
                 "From customer where customerid = " & CustId
    Else
        strsql = "select case when gender<>'C' and (usechiind<>'Y' or usechiind is null) then rtrim(namesuffix)+ ' ' + rtrim(firstname) " & _
                 "when gender<>'C' and usechiind='Y' then chilstnm + chifstnm " & _
                 "when gender='C' and usechiind='Y' then cocname " & _
                 "else coname end as name, gender, coname, namesuffix, firstname, pinnumber, countrycode, agentcode " & _
                 "from customer where customerid = " & CustId
    End If
    '** Change for AY03 end **
    Set GetNameRS = db.ExecuteStatement(strsql)
End Function
Public Function GetName(ByVal CustId As String) As String
On Error GoTo err:
Dim rs As ADODB.Recordset
GetName = ""
Set rs = GetNameRS(CustId)
GetName = Trim(rs.Fields(0).Value)
Set rs = Nothing
Exit Function
err:
End Function
Public Function GetEmailAddr(ByVal CustId As String) As String
Dim strsql As String
Dim rs As ADODB.Recordset

GetEmailAddr = ""
strsql = "select emailaddr from customer where customerid = " & CustId
Set rs = db.ExecuteStatement(strsql)
GetEmailAddr = Trim(rs.Fields(0).Value)
Set rs = Nothing
Exit Function
err:
End Function
Public Function GetAddress(ByVal CustId As String, ByVal Pstrdb As String) As ADODB.Recordset
Dim strsql As String
Dim strAddr As String

' **Created for AY01**
Dim strUseChiInd As String
Dim strAddrType As String
Dim rsChiInd As ADODB.Recordset
Dim rsAddrType As ADODB.Recordset
Dim strTypeCode1 As String
Dim strTypeCode2 As String
Dim strTypeCode3 As String
Dim strTypeCode4 As String

Dim usr As String
Dim Proj As String
Dim conn As String

If Pstrdb = "MCU" Then
    usr = "ITOPER"
    Proj = "LAS"
    conn = "MCUCIWPRD01"
Else
    usr = "ITOPER"
    Proj = "LAS"
    conn = "INGCIWPRD01"
End If

Dim db As Object
Set db = CreateObject("Dbsecurity.Database")
If db.Connect(usr, Proj, conn) Then     'EC01

    strsql = "INSERT csw_print_errlog values (" & CustId & ",getdate(),'Connect Sucessfully')"
    Call db.ExecuteStatement(strsql)

    strsql = "select  TOP 1 C.CSWPAD_ADD1 AS addressline1, " & _
         " C.CSWPAD_ADD2 AS addressline2, " & _
         " C.CSWPAD_ADD3 AS addressline3, " & _
         " C.CSWPAD_CITY AS addresscity, " & _
         " 'N' AS badaddress, " & _
         " CSWPAD_COUNTRY As countrycode, CHG_DATE, c.timestamp " & _
         " from csw_poli_rel a " & _
         " LEFT join csw_policy_address_log b" & _
         " on a.policyaccountid = b.CSWPAD_POLI_ID AND a.policyrelatecode = 'ph' , " & _
         " csw_policy_address C " & _
         " Where a.customerid = " & CustId & _
         "  AND a.policyaccountid = C.CSWPAD_POLI_ID" & _
         "  ORDER BY CHG_DATE DESC, c.timestamp desc"

    Set GetAddress = db.ExecuteStatement(strsql)
Else
    Set GetAddress = Nothing
End If



'strUseChiInd = "select case when UseChiInd='Y' then 'Y' else 'N' end from customer where customerID = " & CustId
'Set rsChiInd = db.ExecuteStatement(strUseChiInd)
'If rsChiInd.Fields(0).Value = "Y" Then
'    strTypeCode1 = "addresstypecode = 'I'"
'    strTypeCode2 = "addresstypecode = 'J'"
'    strTypeCode3 = "addresstypecode = 'R'"
'    strTypeCode4 = "addresstypecode = 'B'"
'Else
'    strTypeCode1 = "addresstypecode = 'R'"
'    strTypeCode2 = "addresstypecode = 'B'"
'    strTypeCode3 = "addresstypecode = 'I'"
'    strTypeCode4 = "addresstypecode = 'J'"
'End If
'
'strAddrType = "select addresstypecode from customeraddress where badaddress = 'N' and customerid = " & CustId
'Set rsAddrType = db.ExecuteStatement(strAddrType)
'If Not rsAddrType.EOF Then
'    rsAddrType.MoveFirst
'    rsAddrType.Find strTypeCode1
'    If rsAddrType.EOF Then
'        rsAddrType.MoveFirst
'        rsAddrType.Find strTypeCode2
'        If rsAddrType.EOF Then
'            rsAddrType.MoveFirst
'            rsAddrType.Find strTypeCode3
'            If rsAddrType.EOF Then
'                rsAddrType.MoveFirst
'                rsAddrType.Find strTypeCode4
'            End If
'        End If
'    End If
'End If
'
'If Not rsAddrType.EOF Then
'    '** Query modified for AY03 **
'    ' Country code is also returned
'    strsql = "select addressline1, addressline2, addressline3, addresscity, badaddress, isnull(countrycode,''), " & _
'             "case when PhoneNumber1 is null then '' else PhoneNumber1 end as PhoneNumber1 " & _
'             "from customeraddress " & _
'             "where addresstypecode = '" & rsAddrType.Fields(0).Value & "' and badaddress = 'N' and customerid = " & CustId
'    Set GetAddress = db.ExecuteStatement(strsql)
'' ** Removed for AY07 start **
''Else
'    '** Query modified for AY03 **
'    ' Add empty string to avoid errors for fields not found
'    'strsql = "select CNAAD1, CNAAD2, CNAAD3, CNAAD4, 'N', '', '' from ORDCNA where CNACIW = " & CustId
'    'Set GetAddress = db.ExecuteStatement(strsql)
'' ** Removed for AY07 end **
'End If
'' **End AY01**

'** Codes before AY01 **
'strsql = "select addressline1,addressline2,addressline3,addresscity,badaddress,case when PhoneNumber1 is null then '' else PhoneNumber1 end as PhoneNumber1 from customeraddress where addresstypecode = 'R' and badaddress = 'N' and customerid = " & CustId
'Set GetAddress = db.ExecuteStatement(strsql)
'** Codes before AY01 end **

'"***EC04 Create
'strAddr = Trim(GetAddress.Fields(0)) & Trim(GetAddress.Fields(1)) & Trim(GetAddress.Fields(2)) & Trim(GetAddress.Fields(3))
'If Len(strAddr) = 0 And Trim(GetAddress.Fields(4)) = "N" Then
'    strsql = "select CNAAD1, CNAAD2, CNAAD3, CNAAD4, 'N' from ORDCNA where CNACIW = " & CustId
'    Set GetAddress = db.ExecuteStatement(strsql)
'End If

'** Codes before AY01 **
'If GetAddress.EOF Then
'    'Checking B Index -
'    strsql = "Select addressline1,addressline2,addressline3,addresscity,badaddress,case when PhoneNumber1 is null then '' else PhoneNumber1 end as PhoneNumber1 from customeraddress where addresstypecode = 'B' and badaddress = 'N' and customerid = " & CustId
'    Set GetAddress = db.ExecuteStatement(strsql)
'
'    If GetAddress.EOF Then
'        'Check Chinese Address
'        strsql = "select CNAAD1, CNAAD2, CNAAD3, CNAAD4, 'N' from ORDCNA where CNACIW = " & CustId
'        Set GetAddress = db.ExecuteStatement(strsql)
'    End If
'***EC04 End
'End If
'** Codes before AY01 end **

End Function
Public Function GetPassword(ByVal CustId As String, ByRef PWD As String, ByVal Pstrdb As String) As Boolean
On Error GoTo err:
Dim strsql As String
PWD = ""

If Not dbSECSQL.IsConnected Then
    'Call dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT)             'EC01
    If Pstrdb = "MCU" Then
        Call dbSECSQL.Connect(MCUSECURITY_USER, MCUSECURITY_PROJECT, MCUSECURITY_CONNECT)
    Else
        Call dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT)     'EC01
    End If
End If

PWD = Generate_pwd

strsql = "exec secsp_reset_pwd '" & CustId & "', '" & PWD & "'"
Call dbSECSQL.ExecuteStatement(strsql)

'***EC06 Create
strsql = "Update sec_user_profile Set secsup_lock_flag = 0 " & _
         " Where secsup_user_id = '" & CustId & "' "
Call dbSECSQL.ExecuteStatement(strsql)
'***EC06 End
GetPassword = True
Exit Function
err:
    GetPassword = False
End Function

Public Function Print_Letter_Job(ByVal PstrCardType As String, ByVal Pstrdb As String, Optional ByVal strFR As String = "")
'Public Function Print_Letter_Job(ByVal PstrCardType As String, ByVal Pstrdb As String)
On Error GoTo errhandler
Dim rs As ADODB.Recordset
Dim strsql As String
Dim strsql1 As String
Dim strsql2 As String
Dim rs1 As ADODB.Recordset
Dim rs2 As ADODB.Recordset
Dim strID As String
Dim strName1 As String
Dim strName2 As String
Dim blnError As Boolean
Dim strAddr1 As String
Dim strAddr2 As String
Dim strAddr3 As String
Dim strAddr4 As String
Dim strContPer As String
Dim strsql4 As String
Dim rs3 As New ADODB.Recordset
Dim strBenCls As String
Dim strMemNo As String

    If Get_Connect(Pstrdb) Then
        Set rs = Get_Print_Letter_Job_Data(PstrCardType, Pstrdb, strFR)
        Select Case Pstrdb
            Case "Life"
                Open App.Path & "\" & Pstrdb & "PINLetter.txt" For Output As #1
                Open App.Path & "\" & Pstrdb & "PINLetterHK.txt" For Output As #2
                Print #1, "!$PINLetter"
                Print #1, "%%PROG: C:\ctrsys\" & Pstrdb & "PINLetter.JDT"
                Print #2, "!$PINLetter"
                Print #2, "%%PROG: C:\ctrsys\" & Pstrdb & "PINLetter.JDT"
                
                While Not rs.EOF
                        
                    ' Check Freelook date start
                    If rs("cswpcr_prtidxletter") = "P" Then
                        ' Check despatch date
                        Dim strDSQL As String
                        Dim strFLDate As String
                        Dim objDBS As Object
                        Dim objRS As ADODB.Recordset
                        Dim blnFnd As Boolean
                        
                        blnFnd = False
                        If rs("cswpcr_crtdate") <= #9/18/2007# Then
                            If rs("cswpcr_crtdate") + 14 > Date Then
                                GoTo NextCard
                            End If
                        Else
                            Set objDBS = CreateObject("dblogon.dbconnect")
                            strDSQL = "Select PORGDT " & _
                                " From eaadta.ordupo " & _
                                " Where popono = '" & rs("cswpcr_pid") & "'" & _
                                " And pocomp = '' and posts <> 'C'"
                            
                            If objDBS.Connect(s400User, s400Proj, s400Conn, s400Type) Then
                                Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                
                                If Not objRS Is Nothing Then
                                    If objRS.RecordCount > 0 Then
                                    
                                        If IsNull(objRS("PORGDT")) Then blnFnd = False
                                        If Len(objRS("PORGDT")) <> 7 Then blnFnd = False
                                        
                                        strFLDate = Mid(objRS("PORGDT"), 4, 2) & "/" & Right(objRS("PORGDT"), 2) & "/" & Left(objRS("PORGDT"), 3) + 1800
                                        If IsDate(strFLDate) Then
                                            ' 7 days after despatch date
                                            If DateAdd("d", 7, CDate(strFLDate)) > Date Then
                                                GoTo NextCard
                                            End If
                                            
                                            blnFnd = True
                                        Else
                                            blnFnd = False
                                        End If
                                    
                                    End If
                                End If
                                
                                Set objRS = Nothing
                                objDBS.Disconnect
                            End If
                            
                            If blnFnd = False Then
                                ' Check Life/Asia policy from nbr_policy_master
                                Set objDBS = CreateObject("dbsecurity.database")
                                
                                If objDBS.Connect("ITOPER", "NBR", "NBRDB") Then
                                    strDSQL = "select nbrpmr_dispatch_date from nbr_policy_master where nbrpmr_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                    Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                    
                                    If Not objRS Is Nothing Then
                                        If objRS.RecordCount > 0 Then
                                            If IsNull(objRS("nbrpmr_dispatch_date")) Then GoTo NextCard
                                            If objRS("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCard
                                            
                                            If DateAdd("d", 7, objRS("nbrpmr_dispatch_date")) > Date Then
                                                GoTo NextCard
                                            End If
                                            
                                            blnFnd = True
                                            
'                                        Else
'                                            GoTo NextCard
                                        End If
'                                    Else
'                                        GoTo NextCard
                                    End If
                                End If
                                
                                Set objRS = Nothing
                                objDBS.Disconnect
                                
                            End If
                            
                            ' ES001 begin - Get from CNB
                            If blnFnd = False Then
                                ' Check Life/Asia policy from nbr_policy_master
                                Set objDBS = CreateObject("dbsecurity.database")
                                                                                               
                                If objDBS.Connect("ITOPER", "POS", "CIWDB") Then
                                    strDSQL = "select convert(datetime,ciwdh_dispatch_date) as nbrpmr_dispatch_date from ciwpr_dispatch_hist where ciwdh_dispatched = 'Y' and ciwdh_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                    Set objRS = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                    
                                    If Not objRS Is Nothing Then
                                        If objRS.RecordCount > 0 Then
                                            If IsNull(objRS("nbrpmr_dispatch_date")) Then GoTo NextCard
                                            If objRS("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCard
                                            
                                            If DateAdd("d", 7, objRS("nbrpmr_dispatch_date")) > Date Then
                                                GoTo NextCard
                                            End If
                                            
                                            blnFnd = True
                                            
                                        Else
                                            GoTo NextCard
                                        End If
                                    Else
                                        GoTo NextCard
                                    End If
                                End If
                                
                                Set objRS = Nothing
                                objDBS.Disconnect

                            End If
                            ' ES001 - End
                        End If
                    End If
                    ' Check Freelook date end
                
                    Set rs1 = GetNameRS(rs.Fields(0).Value)
                    Set rs2 = GetAddress(rs.Fields(0).Value, Pstrdb)
                    
                    If rs2 Is Nothing Or rs2.RecordCount <= 0 Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'NO ADDRESS for this customer!!')"
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 start **
                        ' Mark the print flag to 'A'
                        strsql = "Update " & PstrCardType & "_print_cardletter_report set " & _
                                 PstrCardType & "pcr_prtedletter = 'A', " & PstrCardType & "pcr_lstprtdateletter = getdate() where " & _
                                 PstrCardType & "pcr_prtedletter = 'N' and " & PstrCardType & "pcr_cid = " & rs.Fields(0).Value
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 end **
                    ElseIf Len(Trim(rs2.Fields(0) + rs2.Fields(1) + rs2.Fields(2) + rs2.Fields(3))) = 0 And Trim(rs2.Fields(4)) = "N" Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'NO ADDRESS for this customer!!')"
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 start **
                        ' Mark the print flag to 'A'
                        strsql = "Update " & PstrCardType & "_print_cardletter_report set " & _
                                 PstrCardType & "pcr_prtedletter = 'A', " & PstrCardType & "pcr_lstprtdateletter = getdate() where " & _
                                 PstrCardType & "pcr_prtedletter = 'N' and " & PstrCardType & "pcr_cid = " & rs.Fields(0).Value
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 end **
                    Else
                        If Trim(rs2.Fields(4).Value) = "N" Then
                            If rs.Fields(1).Value = "P" Then
                                strID = Trim(rs1.Fields(1).Value)
                            End If
                            If GetPassword(rs.Fields(0).Value, strID, Pstrdb) = False Then
                                GoTo errhandler
                            End If
                            If PrintLetter(Trim(rs1.Fields(0).Value), strID, Trim(rs2.Fields(0).Value), Trim(rs2.Fields(1).Value), Trim(rs2.Fields(2).Value), Trim(rs2.Fields(3).Value), Trim(rs2.Fields(5).Value)) Then
                                If Upd_DB_Letter(PstrCardType, rs.Fields(0).Value) = False Then
                                    strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                    Call db.ExecuteStatement(strsql)
                                End If
                            End If
                        Else
                            strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'BAD ADDRESS for this customer!!')"
                            Call db.ExecuteStatement(strsql)
                        End If
                    End If
NextCard:
                    rs.MoveNext
                    Set objRS = Nothing
                    Set objDBS = Nothing
                    
                Wend
                
                Print #1, "%%EOJ"
                Print #2, "%%EOJ"
                Close (1)
                Close (2)
            'ITDTLW005 Start ***********************************************************************
            Case "MCU"
                Open App.Path & "\" & Pstrdb & "PINLetter.txt" For Output As #1
                Open App.Path & "\" & Pstrdb & "PINLetterHK.txt" For Output As #2
                Print #1, "!$PINLetter"
                Print #1, "%%PROG: C:\ctrsys\" & Pstrdb & "PINLetter.JDT"
                Print #2, "!$PINLetter"
                Print #2, "%%PROG: C:\ctrsys\" & Pstrdb & "PINLetter.JDT"
                
                While Not rs.EOF
                        
                    ' Check Freelook date start
                    If rs("cswpcr_prtidxletter") = "P" Then
                        ' Check despatch date
                        Dim strDSQLM As String
                        Dim strFLDateM As String
                        Dim objDBSM As Object
                        Dim objRSM As ADODB.Recordset
                        Dim blnFndM As Boolean
                        
                        blnFndM = False
                        If rs("cswpcr_crtdate") <= #9/18/2007# Then
                            If rs("cswpcr_crtdate") + 14 > Date Then
                                GoTo NextCardM
                            End If
                        Else
                            Set objDBSM = CreateObject("dbsecurity.database")
                            strDSQLM = "Select PORGDT " & _
                                " From eaadtm.ordupo " & _
                                " Where popono = '" & rs("cswpcr_pid") & "'" & _
                                " And pocomp = '' and posts <> 'C'"
                            
                            If objDBSM.Connect(s400UserM, s400ProjM, s400ConnM, s400TypeM) Then
                                Set objRSM = objDBSM.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)
                                
                                If Not objRSM Is Nothing Then
                                    If objRSM.RecordCount > 0 Then
                                    
                                        If IsNull(objRSM("PORGDT")) Then blnFndM = False
                                        If Len(objRSM("PORGDT")) <> 7 Then blnFndM = False
                                        
                                        strFLDateM = Mid(objRSM("PORGDT"), 4, 2) & "/" & Right(objRSM("PORGDT"), 2) & "/" & Left(objRSM("PORGDT"), 3) + 1800
                                        If IsDate(strFLDateM) Then
                                            ' 7 days after despatch date
                                            If DateAdd("d", 7, CDate(strFLDateM)) > Date Then
                                                GoTo NextCardM
                                            End If
                                            
                                            blnFnd = True
                                        Else
                                            blnFnd = False
                                        End If
                                    
                                    End If
                                End If
                                
                                Set objRSM = Nothing
                                objDBSM.Disconnect
                            End If
                            
                            If blnFndM = False Then
                                ' Check Life/Asia policy from nbr_policy_master
                                Set objDBSM = CreateObject("dbsecurity.database")
                                If objDBSM.Connect("ITOPER", "LAS", "MCUNBRPRD01") Then
                                    strDSQLM = "select nbrpmr_dispatch_date from nbr_policy_master where nbrpmr_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                    Set objRSM = objDBSM.ExecuteStatement(strDSQLM, adUseClient, adOpenStatic, adLockOptimistic)
                                    
                                    If Not objRSM Is Nothing Then
                                        If objRSM.RecordCount > 0 Then
                                            'If IsNull(objRSM("nbrpmr_dispatch_date")) Then GoTo NextCardM
                                            'If objRSM("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCardM
                                            
                                            'If DateAdd("d", 7, objRSM("nbrpmr_dispatch_date")) > Date Then
                                            '    GoTo NextCardM
                                            'End If
                                            
                                            blnFndM = True
                                            
'                                        Else
'                                            GoTo NextCard
                                        End If
'                                    Else
'                                        GoTo NextCard
                                    End If
                                End If
                                
                                Set objRSM = Nothing
                                objDBSM.Disconnect
                                
                            End If
                            
                            ' ES001 begin - Get from CNB
                            If blnFndM = False Then
                                ' Check Life/Asia policy from nbr_policy_master
                                Set objDBSM = CreateObject("dbsecurity.database")

                                If objDBSM.Connect("ITOPER", "MAES", "CIW") Then
                                    strDSQLM = "select convert(datetime,ciwdh_dispatch_date) as nbrpmr_dispatch_date from ciwpr_dispatch_hist where ciwdh_dispatched = 'Y' and ciwdh_policy_no = '" & Trim(rs("cswpcr_pid")) & "'"
                                    Set objRSM = objDBS.ExecuteStatement(strDSQL, adUseClient, adOpenStatic, adLockOptimistic)

                                    If Not objRSM Is Nothing Then
                                        If objRSM.RecordCount > 0 Then
                                            If IsNull(objRSM("nbrpmr_dispatch_date")) Then GoTo NextCardM
                                            If objRSM("nbrpmr_dispatch_date") = "01/01/1900" Then GoTo NextCardM

                                            If DateAdd("d", 7, objRSM("nbrpmr_dispatch_date")) > Date Then
                                                GoTo NextCardM
                                            End If

                                            blnFndM = True

                                        Else
                                            GoTo NextCardM
                                        End If
                                    Else
                                        GoTo NextCardM
                                    End If
                                End If

                                Set objRSM = Nothing
                                objDBSM.Disconnect

                            End If
                            ' ES001 - End
                        End If
                    End If
                    ' Check Freelook date end
                
                    Set rs1 = GetNameRS(rs.Fields(0).Value)
                    Set rs2 = GetAddress(rs.Fields(0).Value, Pstrdb)
                                       
                    If rs2 Is Nothing Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'NO ADDRESS for this customer!!')"
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 start **
                        ' Mark the print flag to 'A'
                        strsql = "Update " & PstrCardType & "_print_cardletter_report set " & _
                                 PstrCardType & "pcr_prtedletter = 'A', " & PstrCardType & "pcr_lstprtdateletter = getdate() where " & _
                                 PstrCardType & "pcr_prtedletter = 'N' and " & PstrCardType & "pcr_cid = " & rs.Fields(0).Value
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 end **
                    ElseIf Len(Trim(rs2.Fields(0) + rs2.Fields(1) + rs2.Fields(2) + rs2.Fields(3))) = 0 And Trim(rs2.Fields(4)) = "N" Then
                        strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'NO ADDRESS for this customer!!')"
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 start **
                        ' Mark the print flag to 'A'
                        strsql = "Update " & PstrCardType & "_print_cardletter_report set " & _
                                 PstrCardType & "pcr_prtedletter = 'A', " & PstrCardType & "pcr_lstprtdateletter = getdate() where " & _
                                 PstrCardType & "pcr_prtedletter = 'N' and " & PstrCardType & "pcr_cid = " & rs.Fields(0).Value
                        Call db.ExecuteStatement(strsql)
                        '** Codes for AY03 end **
                    Else
                        If Trim(rs2.Fields(4).Value) = "N" Then
                            If rs.Fields(1).Value = "P" Then
                                strID = Trim(rs1.Fields(1).Value)
                            End If
                            If GetPassword(rs.Fields(0).Value, strID, Pstrdb) = False Then
                                GoTo errhandler
                            End If
                            If PrintLetter(Trim(rs1.Fields(0).Value), strID, Trim(rs2.Fields(0).Value), Trim(rs2.Fields(1).Value), Trim(rs2.Fields(2).Value), Trim(rs2.Fields(3).Value), Trim(rs2.Fields(5).Value)) Then
                                If Upd_DB_Letter(PstrCardType, rs.Fields(0).Value) = False Then
                                    strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                    Call db.ExecuteStatement(strsql)
                                End If
                            End If
                        Else
                            strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'BAD ADDRESS for this customer!!')"
                            Call db.ExecuteStatement(strsql)
                        End If
                    End If
NextCardM:
                    rs.MoveNext
                    Set objRSM = Nothing
                    Set objDBSM = Nothing
                    
                Wend
                
                Print #1, "%%EOJ"
                Print #2, "%%EOJ"
                Close (1)
                Close (2)
                'ITDTLW005 End ***********************************************************************
            Case "Aetna"
                Open App.Path & "\" & Pstrdb & "PINLetter.txt" For Output As #1
                Print #1, "!$AetnaPINLetter"
                Print #1, "%%PROG: C:\ctrsys\" & Pstrdb & "PINLetter.JDT"
                'If Not dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT) Then          'EC01
                If Not dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT) Then  'EC01
                    GoTo errhandler
                End If
                
                While Not rs.EOF
                    Call GetLetterContent(Trim(rs.Fields(2)), Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), _
                                rs.Fields(6), strName1, strName2, strContPer, strBenCls, strMemNo, _
                                strAddr1, strAddr2, strAddr3, strAddr4, blnError)
                    
                    If blnError = False Then
                        strID = GetPwd(CStr(rs.Fields(0)), Trim(rs.Fields(1)))
                        
                        If PrintAetnaLetter(strName1, strName2, strContPer, strBenCls, strMemNo, Trim(rs.Fields(2)), strID, strAddr1, strAddr2, strAddr3, strAddr4) Then
                            If Upd_DB_Letter(PstrCardType, CStr(rs.Fields(0))) = False Then
                                strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                Call db.ExecuteStatement(strsql)
                            End If
                        End If
                    End If
                    rs.MoveNext
                Wend
                Print #1, "%%EOJ"
                Close (1)
                
            Case "Dresd"
                If frmMenu.cmbCom <> "ORSO" Then
                    Open App.Path & "\" & Pstrdb & "EEPINLetter.txt" For Output As #1
                    Open App.Path & "\" & Pstrdb & "ERPINLetter.txt" For Output As #2
                    Print #1, "!$DresdEEPINLetter"
                    Print #2, "!$DresdERPINLetter"
                    Print #1, "%%PROG: C:\ctrsys\" & Pstrdb & "EEPINLetter.JDT"
                    Print #2, "%%PROG: C:\ctrsys\" & Pstrdb & "ERPINLetter.JDT"
                End If
                
                'If Not dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT) Then              'EC01
                If Not dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT) Then      'EC01
                    GoTo errhandler
                End If
                While Not rs.EOF
                    Call GetLetterContent(Trim(rs.Fields(2)), Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), _
                                rs.Fields(6), strName1, strName2, strContPer, strBenCls, strMemNo, _
                                strAddr1, strAddr2, strAddr3, strAddr4, blnError)
                    
                    If blnError = False Then
                        strID = GetPwd(CStr(rs.Fields(0)), Trim(rs.Fields(1)))
                        
                        If PrintDresdLetter(strName1, strName2, strContPer, strBenCls, strMemNo, strID, strAddr1, strAddr2, strAddr3, strAddr4, Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), Trim(rs.Fields(2))) Then
                            If Upd_DB_Letter(PstrCardType, CStr(rs.Fields(0))) = False Then
                                strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                Call db.ExecuteStatement(strsql)
                            End If
                        End If
                    End If
                    rs.MoveNext
                Wend
                If frmMenu.cmbCom <> "ORSO" Then
                    Print #1, "%%EOJ"
                    Print #2, "%%EOJ"
                    Close (1)
                    Close (2)
                End If
            Case "Schrd"
                If frmMenu.cmbCom <> "ORSO" Then
                    Open App.Path & "\" & Pstrdb & "EEPINLetter.txt" For Output As #1
                    Open App.Path & "\" & Pstrdb & "ERPINLetter.txt" For Output As #2
                    Print #1, "!$SchrdEEPINLetter"
                    Print #2, "!$SchrdERPINLetter"
                    Print #1, "%%PROG: C:\ctrsys\" & Pstrdb & "EEPINLetter.JDT"
                    Print #2, "%%PROG: C:\ctrsys\" & Pstrdb & "ERPINLetter.JDT"
                End If
                
                'If Not dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT) Then              'EC01
                If Not dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT) Then      'EC01
                    GoTo errhandler
                End If
                While Not rs.EOF
                    Call GetLetterContent(Trim(rs.Fields(2)), Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), _
                                rs.Fields(6), strName1, strName2, strContPer, strBenCls, strMemNo, _
                                strAddr1, strAddr2, strAddr3, strAddr4, blnError)
                    
                    If blnError = False Then
                        strID = GetPwd(CStr(rs.Fields(0)), Trim(rs.Fields(1)))
                        
                        If PrintSchrdLetter(strName1, strName2, strContPer, strBenCls, strMemNo, strID, strAddr1, strAddr2, strAddr3, strAddr4, Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), Trim(rs.Fields(2))) Then
                            If Upd_DB_Letter(PstrCardType, CStr(rs.Fields(0))) = False Then
                                strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                Call db.ExecuteStatement(strsql)
                            End If
                        End If
                    End If
                    rs.MoveNext
                Wend
                
                If frmMenu.cmbCom <> "ORSO" Then
                    Print #1, "%%EOJ"
                    Print #2, "%%EOJ"
                    Close (1)
                    Close (2)
                End If
            '// AC01 Added - Begin //
            Case "McDonald"
                Open App.Path & "\" & Pstrdb & "PINLetter.txt" For Output As #1
                Print #1, "!$AetnaPINLetter"
                Print #1, "%%PROG: C:\ctrsys\AetnaPINLetter.JDT"
                'If Not dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT) Then              'EC01
                If Not dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT) Then      'EC01
                    GoTo errhandler
                End If
                
                While Not rs.EOF
                    Call GetLetterContent(Trim(rs.Fields(2)), Trim(rs.Fields(3)), Trim(rs.Fields(4)), Trim(rs.Fields(5)), _
                                rs.Fields(6), strName1, strName2, strContPer, strBenCls, strMemNo, _
                                strAddr1, strAddr2, strAddr3, strAddr4, blnError)
                    
                    If blnError = False Then
                        strID = GetPwd(CStr(rs.Fields(0)), Trim(rs.Fields(1)))
                        
                        If PrintAetnaLetter(strName1, strName2, strContPer, strBenCls, strMemNo, Trim(rs.Fields(2)), strID, strAddr1, strAddr2, strAddr3, strAddr4) Then
                            If Upd_DB_Letter(PstrCardType, CStr(rs.Fields(0))) = False Then
                                strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Update Print Letter Database for this user!!')"
                                Call db.ExecuteStatement(strsql)
                            End If
                        End If
                    End If
                    rs.MoveNext
                Wend
                
                Print #1, "%%EOJ"
                Close (1)
                '// AC01 Added - End //
        End Select
        Call Disconnect
    End If
    Set rs = Nothing
    Set rs1 = Nothing
    Set rs2 = Nothing
    Set rs3 = Nothing
    If dbSECSQL.IsConnected Then
        dbSECSQL.Disconnect
    End If
Exit Function
errhandler:
    strsql = "INSERT " & PstrCardType & "_print_errlog values (" & rs.Fields(0).Value & ",getdate(),'Cannot Print Letter!!" & Left(err.Description, 75) & "')"
    Call db.ExecuteStatement(strsql)
    Resume Next
End Function

Public Function Get_Print_Letter_Job_Data(ByVal PstrCardType As String, ByVal Pstrdb As String, ByVal strFR As String) As ADODB.Recordset
'Public Function Get_Print_Letter_Job_Data(ByVal PstrCardType As String, ByVal Pstrdb As String) As ADODB.Recordset
On Error GoTo errhandler
Dim strsql As String
Dim recSql As ADODB.Recordset
Dim CID As Long

    If PstrCardType = "csw" Then
         '***EC05 Create - Reprint Letter Only - T + 1 Or Reprint card/Letter T + 5
         'strsql = "select cswpcr_cid, cswpcr_prtidxletter " & _
         '         "from csw_print_cardletter_report " & _
         '         "where (DATEDIFF(day, cswpcr_crtdate, getdate()) >= " & LETTER_PRINT_DAY & " or (cswpcr_prtidxletter = 'R' and DATEDIFF(day, cswpcr_crtdate, getdate()) >= 5)) and " & _
         '         "cswpcr_prtedletter='N' order by convert(varchar(12),cswpcr_crtdate,103),cswpcr_pid"
         
         '** Query amended for AY03 **
' Check Freelook date start
'         strsql = "select cswpcr_cid, cswpcr_prtidxletter, cswpcr_pid, cswpcr_crtdate " & _
'                  "from csw_print_cardletter_report " & _
'                  "where ((DATEDIFF(day, cswpcr_crtdate, getdate()) >= " & LETTER_PRINT_DAY & " and cswpcr_prtidxletter = 'P')" & _
'                  " or (cswpcr_prtidxletter = 'R' and cswpcr_prtidxcard <> 'R' and DATEDIFF(day, cswpcr_crtdate, getdate()) >= 1) " & _
'                  " or (cswpcr_prtidxletter = 'R' and cswpcr_prtidxcard = 'R' and DATEDIFF(day, cswpcr_crtdate, getdate()) >= 5)) and " & _
'                  " cswpcr_prtedletter='N' order by convert(varchar(12),cswpcr_crtdate,103),cswpcr_pid"
' First print or reprint
        If strFR = "R" Then
            strsql = "select cswpcr_cid, cswpcr_prtidxletter, cswpcr_pid, cswpcr_crtdate " & _
                      "from csw_print_cardletter_report " & _
                      "where ((cswpcr_prtidxletter = 'R' and cswpcr_prtidxcard <> 'R' and DATEDIFF(day, cswpcr_crtdate, getdate()) >= 1) " & _
                      " or (cswpcr_prtidxletter = 'R' and cswpcr_prtidxcard = 'R' and DATEDIFF(day, cswpcr_crtdate, getdate()) >= 5)) and " & _
                      " cswpcr_prtedletter='N' " & _
                      " order by cswpcr_prtidxletter, cswpcr_pid, cswpcr_cid"
        Else
            strsql = "select cswpcr_cid, cswpcr_prtidxletter, cswpcr_pid, cswpcr_crtdate " & _
                      "from csw_print_cardletter_report " & _
                      "where ((DATEDIFF(day, cswpcr_crtdate, getdate()) >= " & 1 & " and cswpcr_prtidxletter = 'P')) and " & _
                      " cswpcr_prtedletter='N' " & _
                      " order by cswpcr_pid, cswpcr_cid"
        End If
' Check Freelook date end
        '***Ec05 End
    Else
        'strsql = "select mpfpcr_cid, mpfpcr_prtidxletter, mpfpid_pin_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num " & _
                "from mpf_print_cardletter_report, mpf_pin_details " & _
                "where mpfpcr_crtdate <= '" & CStr(Date - 5) & " 23:59:59' and ((mpfpcr_prtidxletter = 'P' and " & _
                "mpfpcr_fund = mpfpid_fund and mpfpcr_emp = mpfpid_emp and mpfpcr_rc = mpfpid_rpc) or " & _
                "(mpfpcr_prtidxletter = 'R' and ((mpfpid_pin_flag = 'R' and mpfpid_rpc = '000') or mpfpid_pin_flag = 'E'))) and " & _
                "mpfpcr_prtedletter = 'N' and convert(char(10), mpfpcr_cid) = mpfpid_cid " & _
        '        "order by mpfpcr_prtidxletter, convert(varchar(12), mpfpcr_crtdate,103), mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num"
 
        
        '****EC03 create
        'strsql = "select mpfpcr_cid, mpfpcr_prtidxletter, mpfpid_pin_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num, mpfpcr_crtdate, mpfpid_name1, mpfpid_name2 " & _
        '        "into #TempPrint " & _
        '        "from mpf_print_cardletter_report, mpf_pin_details " & _
        '        "where mpfpcr_crtdate <= '" & Format((Date - 5), "mm/dd/yyyy") & " 23:59:59' " & _
        '        "and ((mpfpcr_prtidxletter = 'P' and " & _
        '        "mpfpcr_fund = mpfpid_fund and mpfpcr_emp = mpfpid_emp and mpfpcr_rc = mpfpid_rpc) or " & _
        '        "(mpfpcr_prtidxletter = 'R' and ((mpfpid_pin_flag = 'R' and mpfpid_rpc = '000') or mpfpid_pin_flag = 'E'))) and " & _
        '        "mpfpcr_prtedletter = 'N' and convert(char(10), mpfpcr_cid) = mpfpid_cid "
        'Set recSql = db.ExecuteStatement(strsql)
        
        '***EC05 Create - Reprint Letter Only - T + 1 Or Reprint card/Letter T + 5
        'Insert to temp table - #tmpPrt
        strsql = "select mpfpcr_cid, mpfpcr_prtidxletter, mpfpid_pin_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num, mpfpcr_crtdate, mpfpid_name1, mpfpid_name2 " & _
                "into #TmpPrt " & _
                "from mpf_print_cardletter_report, mpf_pin_details " & _
                "where ((mpfpcr_crtdate <= '" & Format((Date - 5), "mm/dd/yyyy") & " 23:59:59' " & _
                "and mpfpcr_prtidxletter = 'P' and " & _
                "mpfpcr_fund = mpfpid_fund and mpfpcr_emp = mpfpid_emp and mpfpcr_rc = mpfpid_rpc) or " & _
                "(mpfpcr_prtidxletter = 'R' and mpfpcr_prtidxcard <> 'R' and mpfpcr_crtdate <= '" & Format((Date - 1), "mm/dd/yyyy") & " 23:59:59' and ((mpfpid_pin_flag = 'R' and mpfpid_rpc = '000') or mpfpid_pin_flag = 'E')) OR " & _
                "(mpfpcr_prtidxletter = 'R' and mpfpcr_prtidxcard = 'R' and mpfpcr_crtdate <= '" & Format((Date - 5), "mm/dd/yyyy") & " 23:59:59' and ((mpfpid_pin_flag = 'R' and mpfpid_rpc = '000') or mpfpid_pin_flag = 'E'))) AND " & _
                "mpfpcr_prtedletter = 'N' and convert(char(10), mpfpcr_cid) = mpfpid_cid "
        Set recSql = db.ExecuteStatement(strsql)
        '***EC05 End
        
        'Filter temp table #tmpPrt if member type is active, then save to #tempPrint
        Dim strTmpPrt As String
        Dim recTmpPrt As ADODB.Recordset
        
        '***EC07 Create
        '***EC03 Create
        'strTmpPrt = "select a.mpfpcr_cid, a.mpfpcr_prtidxletter, a.mpfpid_pin_flag, a.mpfpid_fund, a.mpfpid_emp, a.mpfpid_rpc, a.mpfpid_mbr_num, a.mpfpcr_crtdate, a.mpfpid_name1, a.mpfpid_name2 into #TempPrint " & _
        '            "from #TmpPrt a, mpf_member_basic_details b " & _
        '            " where b.mpfmbd_member_type='A' " & _
        '            " and a.mpfpid_fund= b.mpfmbd_fund_code and a.mpfpid_emp = b.mpfmbd_employer_code " & _
        '            " and a.mpfpid_rpc=b.mpfmbd_reporting_centre and a.mpfpid_mbr_num = b.mpfmbd_member_number " & _
        '            " order by a.mpfpcr_cid, a.mpfpid_fund, a.mpfpid_emp, a.mpfpid_rpc, a.mpfpid_mbr_num "
        'Set recTmpPrt = db.ExecuteStatement(strTmpPrt)
        '***EC03 End
        
        strTmpPrt = "select a.mpfpcr_cid, a.mpfpcr_prtidxletter, a.mpfpid_pin_flag, a.mpfpid_fund, a.mpfpid_emp, a.mpfpid_rpc, a.mpfpid_mbr_num, a.mpfpcr_crtdate, a.mpfpid_name1, a.mpfpid_name2 into #TempPrint1 " & _
                    "from #TmpPrt a, mpf_member_basic_details b " & _
                    " where b.mpfmbd_member_type IN ('A', 'P') " & _
                    " AND ((mpfpid_pin_flag = 'E' and a.mpfpid_fund= b.mpfmbd_fund_code and a.mpfpid_emp = b.mpfmbd_employer_code " & _
                    " and a.mpfpid_rpc=b.mpfmbd_reporting_centre and a.mpfpid_mbr_num = b.mpfmbd_member_number) " & _
                    " OR (mpfpid_pin_flag = 'R' and a.mpfpid_fund= b.mpfmbd_fund_code and a.mpfpid_emp = b.mpfmbd_employer_code " & _
                    " AND a.mpfpid_rpc = '000')) " & _
                    " Order by a.mpfpcr_cid, a.mpfpid_fund, a.mpfpid_emp, a.mpfpid_rpc, a.mpfpid_mbr_num "
        Set recTmpPrt = db.ExecuteStatement(strTmpPrt)
        
        strsql = "SELECT * INTO #TempPrint FROM #TempPrint1 " & _
                 "GROUP BY mpfpcr_cid, mpfpcr_prtidxletter, mpfpid_pin_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, " & _
                 "mpfpid_mbr_num , mpfpcr_crtdate, mpfpid_name1, mpfpid_name2 " & _
                 "ORDER BY mpfpcr_cid, mpfpcr_prtidxletter, mpfpid_pin_flag, mpfpid_fund, mpfpid_emp, mpfpid_rpc, " & _
                 "mpfpid_mbr_num , mpfpcr_crtdate, mpfpid_name1, mpfpid_name2 "
        Set recSql = db.ExecuteStatement(strsql)
        
        strsql = "Select * from #TempPrint where mpfpcr_prtidxletter = 'R' " _
               + "order by mpfpcr_cid, mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num "
        Set recSql = db.ExecuteStatement(strsql)
        '***EC07 End
                
        While Not recSql.EOF
            CID = recSql.Fields(0)
            recSql.MoveNext
            If Not recSql.EOF Then
                If CID = recSql.Fields(0) Then
                    strsql = "delete from #TempPrint where mpfpcr_cid = " + CStr(CID) + _
                            " and mpfpid_fund = '" + Trim(recSql.Fields(3)) + "' " _
                            + "and mpfpid_mbr_num = " + CStr(recSql.Fields(6)) + " "
                    Call db.ExecuteStatement(strsql)
                End If
            End If
        Wend
        
        If frmMenu.cmbCom = "ORSO" Then
            strsql = "select * from #TempPrint, mpf_fund_special_fields where mpfpid_fund NOT IN ('013','200','203','303') AND mpfpid_fund = fund_code AND table_index = 3 AND special_fields = 'ING ORSO' " _
                    + "order by mpfpcr_prtidxletter, convert(char(12),mpfpcr_crtdate,111), " _
                    + "mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num "
        Else
            If Pstrdb = "McDonald" Then
                strsql = "select * from #TempPrint where mpfpid_fund = '013' " _
                        + "order by mpfpcr_prtidxletter, convert(char(12),mpfpcr_crtdate,111), " _
                        + "mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_name1, mpfpid_name2 "
            Else
                strsql = "select * from #TempPrint, mpf_fund_special_fields where mpfpid_fund <> '013' AND mpfpid_fund = fund_code AND table_index = 3 AND special_fields <> 'ING ORSO' " _
                        + "order by mpfpcr_prtidxletter, convert(char(12),mpfpcr_crtdate,111), " _
                        + "mpfpid_fund, mpfpid_emp, mpfpid_rpc, mpfpid_mbr_num "
            End If
        End If
    End If
    
    Set Get_Print_Letter_Job_Data = db.ExecuteStatement(strsql)
    
    If PstrCardType <> "csw" Then
        '***EC03 Create
        strsql = "drop table #TmpPrt "
        Set recSql = db.ExecuteStatement(strsql)
        '***EC03 End
        
        strsql = "drop table #TempPrint "
        Set recSql = db.ExecuteStatement(strsql)
    End If

Exit Function
errhandler:
    Set Get_Print_Letter_Job_Data = Nothing

End Function

Public Sub GetLetterContent(ByVal Pin_flag As String, ByVal strFund As String, _
                            ByVal strEmp As String, ByVal strRC As String, _
                            ByVal intMemNo As Long, ByRef strName1 As String, _
                            ByRef strName2 As String, ByRef strContPer As String, _
                            ByRef strBenCls As String, ByRef strMemNo As String, _
                            ByRef strAddr1 As String, ByRef strAddr2 As String, _
                            ByRef strAddr3 As String, ByRef strAddr4 As String, _
                            ByRef blnError As Boolean)

Dim strsql1 As String
Dim strsql2 As String
Dim strsql3 As String
Dim rs1 As ADODB.Recordset
Dim rs2 As ADODB.Recordset
Dim rs3 As ADODB.Recordset

If Pin_flag = "E" Then
    strsql1 = "select rtrim(mpfmbd_surname) + ' ' + rtrim(mpfmbd_given_names), mpfmbd_benefit_class from mpf_member_basic_details where " & _
            "mpfmbd_fund_code = '" & strFund & "' and mpfmbd_member_number = '" & CStr(intMemNo) & "'"
    strsql2 = "select mpfmbd_address_1, mpfmbd_address_2, mpfmbd_address_3, mpfmbd_suburb from mpf_member_basic_details where mpfmbd_fund_code = '" & strFund & "' and mpfmbd_member_number = '" & CStr(intMemNo) & "'"
    strsql3 = "select mpfrcd_description, mpfrcd_company_name, mpfrcd_class_of_employer from mpf_reporting_centre where mpfrcd_fund_code = '" & strFund & "' and mpfrcd_employer_code = '" & strEmp & "' and mpfrcd_reporting_centre = '" & strRC & "'"
    Set rs1 = db.ExecuteStatement(strsql1)
    Set rs2 = db.ExecuteStatement(strsql2)
    Set rs3 = db.ExecuteStatement(strsql3)
    If rs1.EOF = True Or rs2.EOF = True Or rs3.EOF = True Then
        blnError = True
    Else
        blnError = False
    End If
    If Trim(rs3.Fields(2)) = "E" Or Trim(rs3.Fields(2)) = "O" Then
        strName1 = Trim(rs3.Fields(0))
        strName2 = GetEmpName(rs3)
        strBenCls = "Class : " + Trim(rs1.Fields(1))
    Else
        strName1 = ""
        strName2 = ""
        strBenCls = ""
    End If
    strContPer = Trim(rs1.Fields(0))
    'GK01
    'strMemNo = "(" + strFund + "-" + Left(strEmp, 1) + "-" + Right(strEmp, 6) + "-" + CStr(intMemNo) + ")"
    If Trim(CStr(intMemNo)) <> "" Then
        strMemNo = "Ref#: " + strFund + "-" + Left(strEmp, 1) + "-" + Right(strEmp, 6) + "-" + CStr(intMemNo)
    Else
        strMemNo = ""
    End If
    'GK01-END
    Call GetMemAddr(rs2, strFund, CStr(intMemNo), strAddr1, strAddr2, strAddr3, strAddr4)
Else
    strsql1 = "select mpfrcd_description, mpfrcd_company_name, mpfrcd_contact from mpf_reporting_centre where mpfrcd_fund_code = '" & strFund & "' and mpfrcd_employer_code = '" & strEmp & "' and mpfrcd_reporting_centre = '" & strRC & "'"
    strsql2 = "select mpfrcd_street_name_1, mpfrcd_street_name_2, mpfrcd_suburb from mpf_reporting_centre where mpfrcd_fund_code = '" & strFund & "' and mpfrcd_employer_code = '" & strEmp & "' and mpfrcd_reporting_centre = '" & strRC & "'"
    Set rs1 = db.ExecuteStatement(strsql1)
    Set rs2 = db.ExecuteStatement(strsql2)
    If rs1.EOF = True Or rs2.EOF = True Then
        blnError = True
    Else
        blnError = False
    End If
    strName1 = Trim(rs1.Fields(0))
    strName2 = GetEmpName(rs1)
    strContPer = GetEmpContPer(rs1)
    strBenCls = ""
    strMemNo = ""
    Call GetEmpAddr(rs2, strFund, strEmp, strRC, strAddr1, strAddr2, strAddr3, strAddr4)
End If

Set rs1 = Nothing
Set rs2 = Nothing
Set rs3 = Nothing

End Sub

Public Function PrintLetter(ByVal PstrName As String, ByVal Pstrpwd As String, _
                            ByVal PstrAdd1 As String, ByVal PstrAdd2 As String, _
                            ByVal PstrAdd3 As String, ByVal Pstradd4 As String, _
                            ByVal Addresscode As String) As Boolean
On Error GoTo errhandler
    If Addresscode <> "HK" Then
        Print #1, "Letter|";
        Print #1, Format(Date, "dd mmm yyyy") & "|";
        Print #1, PstrName; "|";
        Print #1, Pstrpwd; "|";
        Print #1, PstrAdd1; "|";
        Print #1, PstrAdd2; "|";
        Print #1, PstrAdd3; "|";
        Print #1, Pstradd4; "|"
        PrintLetter = True
    Else
        Print #2, "Letter|";
        Print #2, Format(Date, "dd mmm yyyy") & "|";
        Print #2, PstrName; "|";
        Print #2, Pstrpwd; "|";
        Print #2, PstrAdd1; "|";
        Print #2, PstrAdd2; "|";
        Print #2, PstrAdd3; "|";
        Print #2, Pstradd4; "|"
        PrintLetter = True
    End If
Exit Function
errhandler:
    PrintLetter = False
End Function
Public Function PrintAetnaLetter(ByVal PstrName1 As String, ByVal PstrName2 As String, _
                            ByVal PstrContPer As String, ByVal PstrBenCls As String, _
                            ByVal PstrMemNo As String, ByVal PstrFlag As String, _
                            ByVal Pstrpwd As String, ByVal PstrAddr1 As String, _
                            ByVal PstrAddr2 As String, ByVal PstrAddr3 As String, _
                            ByVal PstrAddr4 As String) As Boolean
Dim strName As String
Dim intlen As Integer
Dim i As Integer
Dim j As Integer
Dim strAddrName1 As String
Dim strAddrName2 As String
Dim strBodyName1 As String
Dim strBodyName2 As String
Dim strBodyName3 As String

On Error GoTo errhandler
strName = PstrName1 + " " + PstrName2
intlen = Len(strName)

If intlen > 50 Then
    For i = 50 To 1 Step -1
        If Mid(strName, i, 1) = " " Then
            strAddrName1 = Mid(strName, 1, i)
            strAddrName2 = Mid(strName, i + 1, intlen - i)
            i = 1
        End If
    Next i
Else
    strAddrName1 = strName
    strAddrName2 = ""
End If

If PstrFlag = "E" Then
    strName = PstrContPer
    intlen = Len(strName)
End If

If intlen > 30 Then         ' first check > 30 start
    For i = 30 To 1 Step -1
        If Mid(strName, i, 1) = " " Then
            strBodyName1 = Mid(strName, 1, i)
            If intlen > 60 Then         ' second check > 60 start
                For j = 60 To i + 1 Step -1
                    If Mid(strName, j, 1) = " " Then
                        strBodyName2 = Mid(strName, i + 1, j)
                        strBodyName3 = Mid(strName, j + 1, intlen - j)
                        j = i + 1
                    End If
                Next j
            Else
                strBodyName2 = Mid(strName, i + 1, intlen - i)
                strBodyName3 = ""
            End If          ' second check end
            i = 1
        End If
    Next i
Else
    strBodyName1 = strName
    strBodyName2 = ""
    strBodyName3 = ""
End If              ' first check end

    Print #1, "body|";
    Print #1, Format(Date, "dd mmmm, yyyy"); "|";
    Print #1, strBodyName1; "|"; strBodyName2; "|"; strBodyName3; "|";
    Print #1, Pstrpwd; "|"
    Print #1, "addr|";
    Print #1, strAddrName1; "|"; strAddrName2; "|"; PstrBenCls; "|"; PstrMemNo; "|"; PstrContPer; "|";
    Print #1, PstrAddr1; "|"; PstrAddr2; "|"; PstrAddr3; "|"; PstrAddr4; "|"
    PrintAetnaLetter = True
Exit Function

errhandler:
    PrintAetnaLetter = False
End Function
Public Function PrintDresdLetter(ByVal PstrName1 As String, ByVal PstrName2 As String, _
                                ByVal PstrContPer As String, ByVal PstrBenCls As String, _
                                ByVal PstrMemNo As String, _
                                ByVal Pstrpwd As String, ByVal PstrAddr1 As String, _
                                ByVal PstrAddr2 As String, ByVal PstrAddr3 As String, _
                                ByVal PstrAddr4 As String, ByVal PstrFund As String, _
                                ByVal PstrEmp As String, ByVal PstrRc As String, _
                                ByVal PstrFlag As String) As Boolean
On Error GoTo errhandler
    If PstrFlag = "E" Then  'EE
        Print #1, "body|";
        Print #1, Format(Date, "dd mmmm, yyyy"); "|";
        '***EC05 Create
        If pBolORSOFlag = True Then
            Print #1, PstrContPer; "|"; "|"; "|";
        End If
        '***EC05 End
        Print #1, Pstrpwd; "|"
        Print #1, "addr|";
        Print #1, PstrName1; "|"; PstrName2; "|"; PstrBenCls; "|"; PstrMemNo; "|"; PstrContPer; "|";
        Print #1, PstrAddr1; "|"; PstrAddr2; "|"; PstrAddr3; "|"; PstrAddr4; "|"
        PrintDresdLetter = True
    Else             'ER
        Print #2, "body|";
        Print #2, Format(Date, "dd mmmm, yyyy"); "|";
        '***EC05 Create
        'Print #2, PstrFund + PstrEmp + "-" + PstrRc + "|";
        If pBolORSOFlag = True Then
            Print #2, PstrContPer; "|"; "|"; "|";
        Else
            Print #2, PstrFund + PstrEmp + "-" + PstrRc + "|";
        End If
        '***EC05 End
        Print #2, Pstrpwd; "|"
        Print #2, "addr|";
        Print #2, PstrName1; "|"; PstrName2; "|"; PstrContPer; "|";
        Print #2, PstrAddr1; "|"; PstrAddr2; "|"; PstrAddr3; "|"; PstrAddr4; "|"
        PrintDresdLetter = True
    End If
Exit Function

errhandler:
    PrintDresdLetter = False
End Function
Public Function PrintSchrdLetter(ByVal PstrName1 As String, ByVal PstrName2 As String, _
                                ByVal PstrContPer As String, ByVal PstrBenCls As String, _
                                ByVal PstrMemNo As String, _
                                ByVal Pstrpwd As String, ByVal PstrAddr1 As String, _
                                ByVal PstrAddr2 As String, ByVal PstrAddr3 As String, _
                                ByVal PstrAddr4 As String, ByVal PstrFund As String, _
                                ByVal PstrEmp As String, ByVal PstrRc As String, _
                                ByVal PstrFlag As String) As Boolean
On Error GoTo errhandler
    If PstrFlag = "E" Then
        Print #1, "body|";
        Print #1, Format(Date, "dd mmmm, yyyy"); "|";
        '***EC05 Create
        If pBolORSOFlag = True Then
            Print #1, PstrContPer; "|"; "|"; "|";
        End If
        '***EC05 End
        Print #1, Pstrpwd; "|"
        Print #1, "addr|";
        Print #1, PstrName1; "|"; PstrName2; "|"; PstrBenCls; "|"; PstrMemNo; "|"; PstrContPer; "|";
        Print #1, PstrAddr1; "|"; PstrAddr2; "|"; PstrAddr3; "|"; PstrAddr4; "|"
        PrintSchrdLetter = True
    Else
        Print #2, "body|";
        Print #2, Format(Date, "dd mmmm, yyyy"); "|";
        '***EC05 Create
        'Print #2, PstrFund + PstrEmp + "-" + PstrRc + "|";
        If pBolORSOFlag = True Then
            Print #2, PstrContPer; "|"; "|"; "|";
        Else
            Print #2, PstrFund + PstrEmp + "-" + PstrRc + "|";
        End If
        '***EC05 End
        Print #2, Pstrpwd; "|"
        Print #2, "addr|";
        Print #2, PstrName1; "|"; PstrName2; "|"; PstrContPer; "|";
        Print #2, PstrAddr1; "|"; PstrAddr2; "|"; PstrAddr3; "|"; PstrAddr4; "|"
        PrintSchrdLetter = True
    End If
Exit Function

errhandler:
    PrintSchrdLetter = False
End Function
Public Function Upd_DB_Letter(ByVal PstrCardType As String, ByVal PstrID As String) As Boolean
On Error GoTo errhandler
    Dim strsql As String
    Dim strsql1 As String
    strsql = "Update " & PstrCardType & "_print_control set " & _
             PstrCardType & "prc_letter_ind = 'N', " & PstrCardType & "prc_lstprtdate = " & _
             "getdate(), " & PstrCardType & "prc_printcount_letter = (" & PstrCardType & "prc_printcount_letter + 1) " & _
             "where " & PstrCardType & "prc_cust_id = " & PstrID
    strsql1 = "Update " & PstrCardType & "_print_cardletter_report set " & _
              PstrCardType & "pcr_prtedletter = 'Y', " & PstrCardType & "pcr_lstprtdateletter = getdate() where " & _
              PstrCardType & "pcr_prtedletter = 'N' and " & PstrCardType & "pcr_cid = " & PstrID
    Call db.ExecuteStatement(strsql)
    Call db.ExecuteStatement(strsql1)
    Upd_DB_Letter = True
Exit Function
errhandler:
    Upd_DB_Letter = False
    err.Raise err.number, err.Source, err.Description, err.HelpFile, err.HelpContext
End Function
Public Function Generate_pwd() As String
Dim a As Long
Randomize
a = Fix(Rnd * 1000000 + 0.5)
Generate_pwd = Format(CStr(a), "000000")
End Function
Public Function Truncate_name(ByVal PstrName As String) As Collection
Dim intlen, i As Integer
Dim colres As New Collection
intlen = Len(PstrName)
If intlen > 35 Then
    For i = 35 To 1 Step -1
        If Mid(PstrName, i, 1) = " " Then
            colres.Add Mid(PstrName, 1, i)
            colres.Add Mid(PstrName, i + 1, intlen - i)
            i = 1
        End If
    Next i
Else
    colres.Add PstrName
End If
    Set Truncate_name = colres
End Function
Public Sub GetMemAddr(ByVal PrecAddr As Recordset, _
                        ByVal PFund As String, ByVal PMemNo As String, _
                        ByRef strAddr1 As String, ByRef strAddr2 As String, _
                        ByRef strAddr3 As String, ByRef strAddr4 As String)
'Dim strAddr1 As String
'Dim strAddr2 As String
'Dim strAddr3 As String
'Dim strAddr4 As String
Dim intCount As Integer
Dim strsql As String
Dim recAddr As ADODB.Recordset

intCount = 0
If IsNull(PrecAddr.Fields(0)) = False Then
    strAddr1 = Trim(PrecAddr.Fields(0))
    strAddr1 = Replace(strAddr1, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr1 = ""
End If

If IsNull(PrecAddr.Fields(1)) = False Then
    strAddr2 = Trim(PrecAddr.Fields(1))
    strAddr2 = Replace(strAddr2, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr2 = ""
End If

If IsNull(PrecAddr.Fields(2)) = False Then
    strAddr3 = Trim(PrecAddr.Fields(2))
    strAddr3 = Replace(strAddr3, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr3 = ""
End If

If IsNull(PrecAddr.Fields(3)) = False Then
    strAddr4 = Trim(PrecAddr.Fields(3))
    strAddr4 = Replace(strAddr4, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr4 = ""
End If

If intCount = 0 Then
    strsql = "select mpfmbd_postcode, mpfmbd_address_intl_6, mpfmbd_country from mpf_member_basic_details where mpfmbd_fund_code = '" & PFund & "' and mpfmbd_member_number = '" & PMemNo & "'"
    Set recAddr = db.ExecuteStatement(strsql)
    
    If IsNull(recAddr.Fields(0)) = False Then
        strAddr1 = Trim(recAddr.Fields(0))
        strAddr1 = Replace(strAddr1, Chr(39), Chr(39) & Chr(39))
    Else
        strAddr1 = ""
    End If
    
    If IsNull(recAddr.Fields(1)) = False Then
        strAddr2 = Trim(recAddr.Fields(1))
        strAddr2 = Replace(strAddr2, Chr(39), Chr(39) & Chr(39))
    Else
        strAddr2 = ""
    End If
    
    If IsNull(recAddr.Fields(2)) = False Then
        strAddr3 = Trim(recAddr.Fields(2))
        strAddr3 = Replace(strAddr3, Chr(39), Chr(39) & Chr(39))
    Else
        strAddr3 = ""
    End If

    strAddr4 = ""
End If

'strsql = "select '" & strAddr1 & "' as Addr1, '" & strAddr2 & "' as Addr2, '" & strAddr3 & "' as Addr3, '" & strAddr4 & "' as Addr4 "
'Set GetMemAddr = db.ExecuteStatement(strsql)

End Sub
Public Function GetEmpName(ByVal PrecName As Recordset) As String

Dim strName As String
Dim k As Integer

If IsNull(Trim(PrecName.Fields(1))) = False Then
    k = InStr(1, Trim(PrecName.Fields(1)), "#")
    If k <> 0 Then
        strName = Mid(Trim(PrecName.Fields(1)), 1, k - 1)
    Else
        strName = Trim(PrecName.Fields(1))
    End If
Else
    strName = ""
End If

GetEmpName = strName

End Function
Public Function GetEmpContPer(ByVal PrecName As Recordset) As String

Dim strName As String
Dim k As Integer

If IsNull(Trim(PrecName.Fields(2))) = False Then
    k = InStr(1, Trim(PrecName.Fields(2)), "#")
    If k = 1 Then
        strName = Mid(Trim(PrecName.Fields(2)), 2, Len(Trim(PrecName.Fields(2))))
    ElseIf k <> 0 Then
        strName = Mid(Trim(PrecName.Fields(2)), 1, k - 1)
    Else
        strName = Trim(PrecName.Fields(2))
    End If
Else
    strName = ""
End If

GetEmpContPer = strName

End Function
Public Function GetPwd(ByVal PstrCID As String, ByVal PstrFlag As String) As String
On Error GoTo errhandler

Dim strsql As String
Dim strsql1 As String
Dim rskey As New ADODB.Recordset
Dim rspin As New ADODB.Recordset
Dim strpkey As String
Dim strpass As String

If Not dbSECSQL.IsConnected Then
    'Call dbSECSQL.Connect(USER, PROJECT, SECURITY_CONNECT)             'EC01
    Call dbSECSQL.Connect(GetNTUserName, PROJECT, SECURITY_CONNECT)     'EC01
End If

If PstrFlag = "P" Then
    strsql = "select secsup_user_password, secsup_pkey, secsup_nkey from sec_user_profile where secsup_user_id = '" & PstrCID & "'"
    Set rskey = dbSECSQL.ExecuteStatement(strsql)
    strpass = Replace(rskey.Fields(0), Chr(34), Chr(34) & Chr(34))
    strpkey = Replace(rskey.Fields(1), Chr(34), Chr(34) & Chr(34))
    strsql1 = "declare @output char(6) exec secsp_decode " & """" & Trim(strpass) & """" & ", @output out, " & Trim(rskey.Fields(2)) & ", " & """" & Trim(strpkey) & """" & " select @output"
    Set rspin = dbSECSQL.ExecuteStatement(strsql1)
    GetPwd = Trim(rspin.Fields(0).Value)
ElseIf PstrFlag = "R" Then
    GetPwd = Generate_pwd
    strsql = "exec secsp_reset_pwd '" & PstrCID & "', '" & GetPwd & "'"
    Call dbSECSQL.ExecuteStatement(strsql)
    '***EC06 Create
    strsql = "Update sec_user_profile Set secsup_lock_flag = 0 " & _
             " Where secsup_user_id = '" & PstrCID & "' "
    Call dbSECSQL.ExecuteStatement(strsql)
    '***EC06 End
End If
Exit Function

errhandler:
    strsql = "INSERT mpf_print_errlog values (" & PstrCID & ",getdate(),'Cannot Print Letter!!" & Left(err.Description, 75) & "')"
    Call db.ExecuteStatement(strsql)
    Resume Next
End Function
Public Sub GetEmpAddr(ByVal PrecAddr As Recordset, _
                        ByVal PFund As String, ByVal PEmp As String, _
                        ByVal PRc As String, ByRef strAddr1 As String, _
                        ByRef strAddr2 As String, ByRef strAddr3 As String, _
                        ByRef strAddr4 As String)
'Dim strAddr1 As String
'Dim strAddr2 As String
'Dim strAddr3 As String
'Dim strAddr4 As String
Dim intCount As Integer
Dim strsql As String
Dim recAddr As ADODB.Recordset

intCount = 0
If IsNull(PrecAddr.Fields(0)) = False Then
    strAddr1 = Trim(PrecAddr.Fields(0))
    strAddr1 = Replace(strAddr1, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr1 = ""
End If

If IsNull(PrecAddr.Fields(1)) = False Then
    strAddr2 = Trim(PrecAddr.Fields(1))
    strAddr2 = Replace(strAddr2, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr2 = ""
End If

If IsNull(PrecAddr.Fields(2)) = False Then
    strAddr3 = Trim(PrecAddr.Fields(2))
    strAddr3 = Replace(strAddr3, Chr(39), Chr(39) & Chr(39))
    intCount = intCount + 1
Else
    strAddr3 = ""
End If

If intCount = 0 Then
    strsql = "select mpfrcd_postcode, mpfrcd_state from mpf_reporting_centre where mpfrcd_fund_code = '" & PFund & "' and mpfrcd_employer_code = '" & PEmp & "' and mpfrcd_reporting_centre = '" & PRc & "'"
    Set recAddr = db.ExecuteStatement(strsql)
    
    If IsNull(recAddr.Fields(0)) = False Then
        strAddr1 = Trim(recAddr.Fields(0))
        strAddr1 = Replace(strAddr1, Chr(39), Chr(39) & Chr(39))
    Else
        strAddr1 = ""
    End If
    
    If IsNull(recAddr.Fields(1)) = False Then
        strAddr2 = Trim(recAddr.Fields(1))
        strAddr2 = Replace(strAddr2, Chr(39), Chr(39) & Chr(39))
    Else
        strAddr2 = ""
    End If

    strAddr3 = ""
End If

strAddr4 = ""

'strsql = "select '" & strAddr1 & "' as Addr1, '" & strAddr2 & "' as Addr2, '" & strAddr3 & "' as Addr3, '" & strAddr4 & "' as Addr4 "
'Set GetEmpAddr = db.ExecuteStatement(strsql)

End Sub

Public Function GetReportData(ByVal Fromdate As Date, ByVal ToDate As Date, ByVal company As String) As ADODB.Recordset
Dim strsql As String

If company = "Life" Then

' Update SQL for despatch date enhancement
' 10/15/2007
'    strsql = "select * from csw_print_cardletter_report where " & _
'             "(cswpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "cswpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(cswpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "cswpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(cswpcr_crtdate >= '" & Format(Fromdate, "yyyy-MM-dd") & "' and " & _
'             "cswpcr_crtdate < '" & Format(ToDate, "yyyy-MM-dd") & "' and cswpcr_prtidxletter = 'N' and cswpcr_prtidxcard = 'N')"


' Printed PIN and card + despatched new policy (from nbr_dsph_log instead of cswpcr_crtdate)
' ES002 - Include LifeAsia and CNB to prevent duplicate record
'strsql = "Select r.*, d.nbrdsp_date " & _
'    " from csw_print_cardletter_report r left join nbr.dbo.nbr_dsph_log d " & _
'    " on cswpcr_pid = nbrdsp_policy_no " & _
'    " where ((cswpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' " & _
'    " and cswpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') " & _
'    " or (cswpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' " & _
'    " and cswpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') " & _
'    " or (nbrdsp_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' " & _
'    " and nbrdsp_date < '" & Format(ToDate, "yyyy-MM-dd") & "')) " & _
'    " Order by nbrdsp_date desc, cswpcr_prtedcard desc, cswpcr_pid"
strsql = "Select r.*, ISNULL(ISNULL(d.nbrdsp_date, d1.nbrpmr_dispatch_date),d2.ciwdh_dispatch_date) as nbrdsp_date " & _
    " from csw_print_cardletter_report r " & _
    "   left join nbr.dbo.nbr_dsph_log d on cswpcr_pid = nbrdsp_policy_no " & _
    "   left join nbr.dbo.nbr_policy_master d1 on cswpcr_pid = nbrpmr_policy_no " & _
    "   left join vantive.dbo.ciwpr_dispatch_hist d2 on cswpcr_pid = ciwdh_policy_no " & _
    " where ((cswpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and cswpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') " & _
    " or (cswpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and cswpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') " & _
    " or (nbrdsp_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' and nbrdsp_date < '" & Format(ToDate, "yyyy-MM-dd") & "') " & _
    " or (nbrpmr_dispatch_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' and nbrpmr_dispatch_date < '" & Format(ToDate, "yyyy-MM-dd") & "') " & _
    " or (ciwdh_dispatch_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' and ciwdh_dispatch_date < '" & Format(ToDate, "yyyy-MM-dd") & "')) " & _
    " Order by nbrdsp_date desc, cswpcr_prtedcard desc, cswpcr_pid"
' ES002 - end

'    strsql = "select * from csw_print_cardletter_report where " & _
'             "(cswpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "cswpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(cswpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "cswpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(cswpcr_pid in (select nbrdsp_policy_no from nbr.dbo.nbr_dsph_log " & _
'             "where nbrdsp_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' and " & _
'             "nbrdsp_date < '" & Format(ToDate, "yyyy-MM-dd") & "') and cswpcr_prtidxletter = 'N' and cswpcr_prtidxcard = 'N')"
' End

ElseIf company = "ORSO" Then
    strsql = "select * from mpf_print_cardletter_report, mpf_fund_special_fields, mpf_pin_details where (" & _
             "(mpfpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
             "(mpfpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
             "(mpfpcr_crtdate >= '" & Format(Fromdate + 1, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_crtdate < '" & Format(ToDate + 1, "yyyy-MM-dd") & "' and mpfpcr_prtidxletter = 'N' and mpfpcr_prtidxcard = 'N')) and " & _
             "mpfpid_cid = mpfpcr_cid and " & _
             "((mpfpcr_prtidxcard = 'P' and mpfpcr_fund = fund_code and mpfpid_fund = fund_code) or " & _
             "(mpfpcr_prtidxcard = 'R' and mpfpid_fund = fund_code) or " & _
             "(mpfpcr_prtidxletter = 'R' and mpfpid_fund = fund_code)) and " & _
             "mpfpid_fund not in ('013','200','203','303') and " & _
             "table_index = 3 and " & _
             "special_fields = 'ING ORSO' "
' Life/Asia start
ElseIf company = "LA" Then
    strsql = "Select r.*, d.nbrpmr_dispatch_date as nbrdsp_date " & _
        " from vantive..csw_print_cardletter_report r left join nbr.dbo.nbr_policy_master d " & _
        " on cswpcr_pid = nbrpmr_policy_no " & _
        " where ((nbrpmr_dispatch_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' " & _
        " and nbrpmr_dispatch_date < '" & Format(ToDate, "yyyy-MM-dd") & "')) " & _
        " Order by nbrpmr_dispatch_date desc, cswpcr_prtedcard desc, cswpcr_pid"
' End

' ES001 - CNB start
ElseIf company = "CNB" Then
    strsql = "Select r.*, convert(datetime,d.ciwdh_dispatch_date) as nbrdsp_date " & _
        " from vantive..csw_print_cardletter_report r left join vantive.dbo.ciwpr_dispatch_hist d " & _
        " on cswpcr_pid = ciwdh_policy_no " & _
        " where ((ciwdh_dispatch_date >= '" & Format(Fromdate, "yyyy-MM-dd") & "' " & _
        " and ciwdh_dispatch_date < '" & Format(ToDate, "yyyy-MM-dd") & "')) " & _
        " Order by ciwdh_dispatch_date desc, cswpcr_prtedcard desc, cswpcr_pid"
' ES001 - End
Else
    strsql = "select * from mpf_print_cardletter_report, mpf_fund_special_fields, mpf_pin_details where (" & _
             "(mpfpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
             "(mpfpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
             "(mpfpcr_crtdate >= '" & Format(Fromdate + 1, "yyyy-MM-dd") & "' and " & _
             "mpfpcr_crtdate < '" & Format(ToDate + 1, "yyyy-MM-dd") & "' and mpfpcr_prtidxletter = 'N' and mpfpcr_prtidxcard = 'N')) and " & _
             "mpfpid_cid = mpfpcr_cid and mpfpid_fund = fund_code and " & _
             "table_index = 3 and " & _
             "special_fields <> 'ING ORSO' "
'    strsql = "select * from mpf_print_cardletter_report where (" & _
'             "(mpfpcr_lstprtdateletter >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "mpfpcr_lstprtdateletter < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(mpfpcr_lstprtdatecard >= '" & Format(ToDate, "yyyy-MM-dd") & "' and " & _
'             "mpfpcr_lstprtdatecard < '" & Format(ToDate + 1, "yyyy-MM-dd") & "') or " & _
'             "(mpfpcr_crtdate >= '" & Format(Fromdate + 1, "yyyy-MM-dd") & "' and " & _
'             "mpfpcr_crtdate < '" & Format(ToDate + 1, "yyyy-MM-dd") & "' and mpfpcr_prtidxletter = 'N' and mpfpcr_prtidxcard = 'N'))"
End If

If company = "Life" Or company = "LA" Or company = "CNB" Then
    Dim objDS As Object
    Set objDS = CreateObject("dbsecurity.database")
    
    If objDS.Connect("ITOPER", "POS", "CIWDB") Then
        Set GetReportData = objDS.ExecuteStatement(strsql)
        objDS.Disconnect
        Set objDS = Nothing
    End If
Else
    Set GetReportData = db.ExecuteStatement(strsql)
End If

End Function
Public Function GetAgentsDtl(ByVal CustId As String, ByRef Agentname As String, _
                             ByRef AgentCode As String, ByRef AgentUCode As String)
On Error GoTo err:
Dim rs As ADODB.Recordset
Dim strsql As String
Agentname = ""
AgentCode = ""
AgentUCode = ""
Set rs = GetNameRS(CustId)
If Not rs.EOF Then
    Agentname = Trim(rs.Fields(0).Value)
    '***EC02 create
    'AgentCode = Trim(rs.Fields(3).Value)
    AgentCode = Trim(rs.Fields(7).Value)
    '****EC02 End
    'strsql = "select UnitCode from AgentCodes where agentcode = '" & AgentCode & "'"
    strsql = "select locationcode from AgentCodes where agentcode = '" & AgentCode & "'"
    Set rs = Nothing
    Set rs = db.ExecuteStatement(strsql)
    AgentUCode = Trim(rs.Fields(0).Value)
End If
Set rs = Nothing
Exit Function
err:
    Set rs = Nothing
End Function
Public Sub InsertReportTableLife(ByVal CustId As String, ByVal Custname As String, _
                                 ByVal PoliID As String, ByVal AgentUCode As String, _
                                 ByVal Agentname As String, ByVal AgentCode As String, _
                                 ByVal CreateDate As Date, ByVal CardIdx As String, _
                                 ByVal LetterIdx As String, ByVal Card As String, _
                                 ByVal Letter As String, ByVal CardUser As String, _
                                 ByVal LetterUser As String, ByVal LetterPrint As String, _
                                 ByVal CardPrint As String)
On Error GoTo err:
Dim strsql As String
'//CW02 modified begin
'strsql = "insert csw_print_daily_report values(" & CustId & ",'" & Replace(Custname, "'", "''") & "'," & _
'         "'" & PoliID & "','" & AgentUCode & "','" & AgentCode & "','" & Agentname & "'," & _
'         "'" & Format(CreateDate, "yyyy-MM-dd") & "','" & LetterIdx & "','" & CardIdx & "'," & _
'         "'" & Letter & "','" & Card & "','" & LetterPrint & "'," & _
'         "'" & CardPrint & "','" & LetterUser & "','" & CardUser & "')"
strsql = "insert csw_print_daily_report values(" & CustId & ",'" & Replace(Custname, "'", "''") & "'," & _
         "'" & PoliID & "','" & AgentUCode & "','" & AgentCode & "','" & Replace(Agentname, "'", "''") & "'," & _
         "'" & Format(CreateDate, "yyyy-MM-dd") & "','" & LetterIdx & "','" & CardIdx & "'," & _
         "'" & Letter & "','" & Card & "','" & LetterPrint & "'," & _
         "'" & CardPrint & "','" & LetterUser & "','" & CardUser & "')"
'//CW02 modified end
Call db.ExecuteStatement(strsql)
Exit Sub
err:
    MsgBox err.Description
End Sub
Public Function ReplaceNULL(rs As ADODB.Recordset, ByVal fieldno As Integer, _
                            ByVal FalseVal As String) As String
If IsNull(rs.Fields(fieldno).Value) Then
    ReplaceNULL = FalseVal
Else
    ReplaceNULL = Trim(rs.Fields(fieldno).Value)
End If
End Function
Public Sub ADDTempReportRec(ByVal Fromdate As Date, ByVal ToDate As Date, ByVal company As String)
On Error GoTo err:
Dim rsrdata As ADODB.Recordset
    Set rsrdata = GetReportData(Fromdate, ToDate, company)
    
' ES002 - begin
''    ' Life/Asia policy
''    Dim rsLA As ADODB.Recordset
''    Set rsLA = GetReportData(Fromdate, ToDate, "LA")
''    ' End
''
''    ' ES001 - Life/Asia policy
''    Dim rsCNB As ADODB.Recordset
''    Set rsCNB = GetReportData(Fromdate, ToDate, "CNB")
''    ' ES001 - End
' ES002 - end

    If company <> "ORSO" Then
        Call KillRptTemp(company)
    End If
    If company = "Life" Then
        Call ReportLifeJob(rsrdata)

' ES002 begin - comment below
''        Call ReportLifeJob(rsLA)    ' Life/Asia
''        Call ReportLifeJob(rsCNB)    ' ES001
' ES002 end

    Else
        Call ReportMPFJob(rsrdata, company)
    End If
    Set rsrdata = Nothing
Exit Sub
err:
    MsgBox err.Description
End Sub

Public Sub KillRptTemp(ByVal company As String)
Dim strsql As String
If company = "Life" Then
    strsql = "delete from csw_print_daily_report"
Else
    strsql = "delete from mpf_print_daily_report"
End If
Call db.ExecuteStatement(strsql)
End Sub
Public Function GetInForceDate(ByVal PolicyID As String) As String
Dim strsql As String
Dim rs As ADODB.Recordset
On Error GoTo err:
GetInForceDate = "1900-01-01"
strsql = "select cswpuw_infor_date from csw_policy_uw where cswpuw_poli_id = '" & PolicyID & "'"
Set rs = db.ExecuteStatement(strsql)
If Not rs.EOF Then
    GetInForceDate = rs.Fields(0).Value
End If
Set rs = Nothing
Exit Function
err:
    Set rs = Nothing
    GetInForceDate = "1900-01-01"
End Function
Public Sub ReportLifeJob(rsrdata As ADODB.Recordset)
On Error GoTo err:
Dim CustId, Custname As String
Dim PoliID As String
Dim AgentID As String
Dim Agentname As String
Dim AgentCode As String
Dim AgentUCode As String
Dim Card, CardUser, CardPrint As String
Dim Letter, LetterUser, LetterPrint As String
Dim CreateDate As Date
While Not rsrdata.EOF
    CustId = Trim(rsrdata.Fields(0).Value)
    Custname = GetName(CustId)
    AgentID = ReplaceNULL(rsrdata, 2, "0")
    PoliID = ReplaceNULL(rsrdata, 1, "")
    CardPrint = Format(ReplaceNULL(rsrdata, 7, ""), "yyyy-MM-dd")
    LetterPrint = Format(ReplaceNULL(rsrdata, 6, ""), "yyyy-MM-dd")
    Card = ReplaceNULL(rsrdata, 9, "")
    Letter = ReplaceNULL(rsrdata, 8, "")
    CardUser = ReplaceNULL(rsrdata, 11, "")
    LetterUser = ReplaceNULL(rsrdata, 10, "")
    Call GetAgentsDtl(AgentID, Agentname, AgentCode, AgentUCode)
    
    Dim strOrder As String
    If rsrdata("cswpcr_prtidxletter") = "P" And rsrdata("cswpcr_prtidxcard") = "P" Then
        
        strOrder = "Z"
        
        If rsrdata("cswpcr_prtedletter") = "N" And rsrdata("cswpcr_prtedcard") = "Y" Then
            strOrder = "A"
        End If
                
        If rsrdata("cswpcr_prtedletter") = "Y" And rsrdata("cswpcr_prtedcard") = "Y" Then
            strOrder = "C"
        End If
                
        Agentname = strOrder
    
    End If
    
            
    If rsrdata("cswpcr_prtidxletter") = "N" And rsrdata("cswpcr_prtidxcard") = "N" Then
        
        strOrder = "Z"
        If IsNull(rsrdata("cswpcr_prtedletter")) And IsNull(rsrdata("cswpcr_prtedcard")) Then
            strOrder = "B"
        End If
        
        Agentname = strOrder
        
    End If
    
       
    If rsrdata(5) = "R" Or rsrdata(4) = "R" Then
        CreateDate = rsrdata(3)
    Else
        'CreateDate = GetInForceDate(PoliID)
        CreateDate = rsrdata("nbrdsp_date")
    End If
    Call InsertReportTableLife(CustId, Custname, PoliID, AgentUCode, Agentname, AgentCode, CreateDate, rsrdata(5), rsrdata(4), Card, Letter, CardUser, LetterUser, LetterPrint, CardPrint)
    rsrdata.MoveNext
Wend
Exit Sub
err:
End Sub
Public Sub ReportMPFJob(rsrdata As ADODB.Recordset, ByVal company As String)
On Error GoTo err:
Dim CustId, Custname, CustType As String
Dim FundCode As String
Dim EmpCode As String
Dim Reporting As String
Dim MemberN As String
Dim Account As String
Dim Card, CardUser, CardPrint As String
Dim Letter, LetterUser, LetterPrint As String
While Not rsrdata.EOF
    CustId = Trim(rsrdata.Fields(0).Value)
    CustType = ReplaceNULL(rsrdata, 11, "E")
    If CustType = "R" Or GetMPFMemberN(CustId, FundCode, EmpCode, Reporting, MemberN) Then
        If (company = "McDonald" And FundCode = "013") Or company <> "McDonald" Then
            Custname = GetMPFName(FundCode, EmpCode, Reporting, MemberN, CustType)
            Account = FundCode & "-" & EmpCode & "-" & Reporting
            CardPrint = Format(ReplaceNULL(rsrdata, 8, ""), "yyyy-MM-dd")
            LetterPrint = Format(ReplaceNULL(rsrdata, 7, ""), "yyyy-MM-dd")
            Card = ReplaceNULL(rsrdata, 10, "")
            Letter = ReplaceNULL(rsrdata, 9, "")
            CardUser = ReplaceNULL(rsrdata, 13, "")
            LetterUser = ReplaceNULL(rsrdata, 12, "")
            Call InsertReportTableMPF(CustId, Custname, Account, MemberN, CustType, rsrdata(4), rsrdata(6), rsrdata(5), Card, Letter, CardUser, LetterUser, LetterPrint, CardPrint)
        End If
    End If
    rsrdata.MoveNext
Wend
Exit Sub
err:
End Sub
Public Function GetMPFMemberN(ByVal CustId As String, ByRef FundCode As String, _
                              ByRef EmpCode As String, ByRef Reporting As String, _
                              ByRef MemberN As String) As Boolean
On Error GoTo err:
Dim strsql As String
Dim rs As ADODB.Recordset
FundCode = ""
EmpCode = ""
Reporting = ""
MemberN = ""
GetMPFMemberN = False
strsql = "select mpfpid_mbr_num,mpfpid_fund,mpfpid_emp,mpfpid_rpc from mpf_pin_details where mpfpid_cid = '" & CustId & "'"
Set rs = db.ExecuteStatement(strsql)
If Not rs.EOF Then
    MemberN = Trim(rs.Fields(0).Value)
    FundCode = Trim(rs.Fields(1).Value)
    EmpCode = Trim(rs.Fields(2).Value)
    Reporting = Trim(rs.Fields(3).Value)
    GetMPFMemberN = True
End If
Set rs = Nothing
Exit Function
err:
    Set rs = Nothing
    GetMPFMemberN = False
End Function
Public Function GetMPFName(ByVal FundCode As String, ByVal EmpCode As String, _
                           ByVal Reporting As String, ByVal MemberN As String, _
                           ByVal CustType As String) As String
On Error GoTo err:
Dim strsql As String
Dim Name As String
Dim rs As ADODB.Recordset
Dim pos As Integer
GetMPFName = ""
If CustType = "E" Then
    strsql = "select mpfmbd_given_names,mpfmbd_surname,mpfmbd_chinese_name from mpf_member_basic_details where " & _
             "mpfmbd_fund_code = '" & FundCode & "' and mpfmbd_member_number = '" & MemberN & "' and " & _
             "mpfmbd_employer_code = '" & EmpCode & "' and mpfmbd_reporting_centre = '" & Reporting & "'"
    Set rs = db.ExecuteStatement(strsql)
    Name = Trim(rs.Fields(1).Value) & " " & Trim(rs.Fields(0).Value)
Else
    strsql = "select mpfrcd_company_name,mpfrcd_description from mpf_reporting_centre where " & _
             "mpfrcd_fund_code = '" & FundCode & "' and mpfrcd_reporting_centre = '" & Reporting & "' and " & _
             "mpfrcd_employer_code = '" & EmpCode & "'"
    Set rs = db.ExecuteStatement(strsql)
    Name = Trim(rs.Fields(1).Value) & Trim(rs.Fields(0).Value)
    pos = InStr(1, Name, "#")
    If pos = 1 Then
        Name = Right(Name, Len(Name) - 1)
    ElseIf pos <> 0 Then
        Name = Left(Name, pos - 1)
    End If
End If
GetMPFName = Name
Set rs = Nothing
Exit Function
err:
    Set rs = Nothing
End Function
Public Sub InsertReportTableMPF(ByVal CustId As String, ByVal Custname As String, _
                                ByVal Acct As String, ByVal MN As String, ByVal CustType As String, _
                                ByVal CreateDate As Date, ByVal CardIdx As String, _
                                ByVal LetterIdx As String, ByVal Card As String, _
                                ByVal Letter As String, ByVal CardUser As String, _
                                ByVal LetterUser As String, ByVal LetterPrint As String, _
                                ByVal CardPrint As String)
On Error GoTo err:
Dim strsql As String

strsql = "insert mpf_print_daily_report values(" & CustId & ",'" & Replace(Custname, "'", "''") & "'," & _
         "'" & Acct & "','" & MN & "','" & CustType & "','" & Format(CreateDate, "yyyy-MM-dd") & "','" & LetterIdx & "','" & CardIdx & "'," & _
         "'" & Letter & "','" & Card & "','" & LetterPrint & "'," & _
         "'" & CardPrint & "','" & LetterUser & "','" & CardUser & "')"
If frmMenu.cmbCom = "ORSO" Then
    If Not InsRptTbl.IsConnected Then
        Call InsRptTbl.Connect(GetNTUserName, PROJECT, ChooseConnection("Aetna"))
    End If
    Call InsRptTbl.ExecuteStatement(strsql)
Else
    Call db.ExecuteStatement(strsql)
End If
Exit Sub
err:
    MsgBox err.Description
End Sub

Public Function GetCouponData() As ADODB.Recordset
Dim strsql As String
Dim strCustID As String
Dim rsCoupon As New ADODB.Recordset
'** Codes for AY03 start **
'Dim strValidCustId As String  ' Store customerid with valid address
Dim strInvalidCustId As String  ' Store customerid with invalid address
Dim strTypeCode1 As String
Dim strTypeCode2 As String
Dim strTypeCode3 As String
Dim strTypeCode4 As String
Dim rsDetails As New ADODB.Recordset
'** Codes for AY03 end **

    '** Codes for AY03 start **
    ' Get the customerid that coupon has to be printed for
    Call Get_Connect("ICR")
    strsql = "Select icrcsv_holder_id From hkalsqlprd3.icr.dbo.icr_cust_survey Where icrcsv_cup_prt = 'N' Order By icrcsv_holder_id"
    'strsql = "Select icrcsv_holder_id From hkalsqldev1.icr.dbo.icr_cust_survey Where icrcsv_cup_prt = 'N' Order By icrcsv_holder_id"  ' For development only
    Set rsCoupon = db.ExecuteStatement(strsql, adUseClient, adOpenForwardOnly, adLockReadOnly)
    
    'strValidCustId = ""
    strInvalidCustId = ""
    Call Get_Connect("Life")
    db.ExecuteStatement ("create table #coupon (CustomerID varchar(9), CusName varchar(51), " & _
                         "AddressLine1 varchar(52), AddressLine2 varchar(52), AddressLine3 varchar(52), AddressCity varchar(52), " & _
                         "AddressTypeCode varchar(2), UseChiInd varchar(1), CouponName varchar(51) )")

    While Not rsCoupon.EOF
        ' Get the name and address for the specific customer
        strsql = "SELECT a.CustomerID, " & _
                 "case when a.gender<>'C' and (a.usechiind<>'Y' or a.usechiind is null) then quotename(rtrim(a.namesuffix)+ ' ' + rtrim(a.firstname),'''') " & _
                 "when a.gender<>'C' and a.usechiind='Y' then '''' + a.chilstnm + a.chifstnm + '''' " & _
                 "when a.gender='C' and a.usechiind='Y' then '''' + a.cocname + '''' " & _
                 "else quotename(a.coname,'''') end, " & _
                 "quotename(b.AddressLine1,'''') , quotename(b.AddressLine2,''''), quotename(b.AddressLine3,''''), quotename(b.AddressCity,''''), " & _
                 "b.AddressTypeCode, a.UseChiInd, quotename(rtrim(a.namesuffix)+ ' ' + rtrim(a.firstname),'''') " & _
                 "FROM Customer a, CustomerAddress b WHERE a.CustomerID = b.CustomerID and a.CustomerID = '" & rsCoupon(0) & "' " & _
                 "and b.AddressTypeCode in ('R','B','I','J') and b.BadAddress='N' "
        Set rsDetails = db.ExecuteStatement(strsql, adUseClient, adOpenStatic, adLockReadOnly)
        ' Get the record with suitable address
        If Not rsDetails.EOF Then
            If rsDetails(7) = "Y" Then
                strTypeCode1 = "addresstypecode = 'I'"
                strTypeCode2 = "addresstypecode = 'J'"
                strTypeCode3 = "addresstypecode = 'R'"
                strTypeCode4 = "addresstypecode = 'B'"
            Else
                strTypeCode1 = "addresstypecode = 'R'"
                strTypeCode2 = "addresstypecode = 'B'"
                strTypeCode3 = "addresstypecode = 'I'"
                strTypeCode4 = "addresstypecode = 'J'"
            End If
            rsDetails.MoveFirst
            rsDetails.Find strTypeCode1
            If rsDetails.EOF Then
                rsDetails.MoveFirst
                rsDetails.Find strTypeCode2
                If rsDetails.EOF Then
                    rsDetails.MoveFirst
                    rsDetails.Find strTypeCode3
                    If rsDetails.EOF Then
                        rsDetails.MoveFirst
                        rsDetails.Find strTypeCode4
                    End If
                End If
            End If
            If Not rsDetails.EOF Then
                ' Store it into a temp table for later selection
                strsql = "INSERT INTO #coupon (CustomerID,CusName,AddressLine1,AddressLine2,AddressLine3,AddressCity," & _
                         "AddressTypeCode,UseChiInd,CouponName) VALUES ('" & rsDetails(0) & "'," & rsDetails(1) & "," & _
                         rsDetails(2) & "," & rsDetails(3) & "," & rsDetails(4) & "," & rsDetails(5) & _
                         ",'" & rsDetails(6) & "','" & rsDetails(7) & "'," & rsDetails(8) & ")"
                db.ExecuteStatement (strsql)
                'strValidCustId = strValidCustId & "'" & rsCoupon(0) & "',"
            Else
                strInvalidCustId = strInvalidCustId & "'" & rsCoupon(0) & "',"
            End If
        Else
            strInvalidCustId = strInvalidCustId & "'" & rsCoupon(0) & "',"
        End If
        rsCoupon.MoveNext
    Wend
    
    ' Return the result
    Set GetCouponData = db.ExecuteStatement("select * from #coupon", adUseClient, adOpenStatic, adLockBatchOptimistic)
    db.ExecuteStatement ("drop table #coupon")
    ' Update the coupon-printing flags
    Call Get_Connect("ICR")
    'If strValidCustId <> "" Then
    '    strsql = "Update icr..icr_cust_survey Set icrcsv_cup_prt = 'Y' Where icrcsv_holder_id IN (" & Mid(strValidCustId, 1, Len(strValidCustId) - 1) & ") "
    '    db.ExecuteStatement (strsql)
    'End If
    If strInvalidCustId <> "" Then
        strsql = "Update icr..icr_cust_survey Set icrcsv_cup_prt = 'A' Where icrcsv_holder_id IN (" & Mid(strInvalidCustId, 1, Len(strInvalidCustId) - 1) & ") "
        db.ExecuteStatement (strsql)
    End If
    '** Codes for AY03 end **

    '** Codes Before AY03 start **
    'Call Get_Connect("ICR")
    'strsql = "Select Distinct icrcsv_holder_id From hkalsqlprd3.icr.dbo.icr_cust_survey Where icrcsv_cup_prt = 'N' "
    'Set rsCoupon = db.ExecuteStatement(strsql)
    'strCustID = ""
    
    'While Not rsCoupon.EOF
    '    strCustID = strCustID + CStr(rsCoupon(0)) + ","
    '    rsCoupon.MoveNext
    'Wend
    'If strCustID <> "" Then
    '    strCustID = Mid(strCustID, 1, Len(strCustID) - 1)
        
    '    Call Get_Connect("Life")
    '    strsql = "Select Distinct a.CustomerID, a.NameSuffix + ' ' + a.FirstName, b.AddressLine1, b.AddressLine2, b.AddressLine3, b.AddressCity From Customer a, CustomerAddress b Where a.CustomerID = b.CustomerID and a.CustomerID in (" & strCustID & ") and b.AddressTypeCode = 'R' and b.BadAddress <> 'Y' "
    '    Set GetCouponData = db.ExecuteStatement(strsql)
        
    '    Call Get_Connect("ICR")
    '    strsql = "Update icr..icr_cust_survey Set icrcsv_cup_prt = 'Y' Where icrcsv_holder_id IN (" & strCustID & ") "
    '    Set rsCoupon = db.ExecuteStatement(strsql)
    'Else
    '    Set GetCouponData = db.ExecuteStatement("select 'Nothing' as A")
    'End If
    '** Codes Before AY03 end **

End Function
'***EC01 Create
Public Function GetNTUserName() As String
On Error Resume Next
Dim UserName As String
Dim slength As Long
Dim retval As Long

    UserName = Space(255)
    slength = 255
    retval = GetUserName(UserName, slength)
    GetNTUserName = Left(UserName, slength - 1)
    
    ' for UAT - use ITOPER
    GetNTUserName = "ITOPER"
    'GetNTUserName = "EICADMIN"
    ' End

End Function

Public Sub getPrintOut(pstrMM As String, pstrYY As String, Optional strCompany As String)
Dim str1 As String

'str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'     + " Where a.policyAccountID = b.policyAccountID" _
'     + " and a.productID=b.productID" _
'     + " and a.trailer = b.trailer" _
'     + " and b.policyAccountID = c.policyAccountID" _
'     + " and c.productID=d.productID" _
'     + " and c.companyID=d.companyID" _
'     + " and e.customerID=b.customerID" _
'     + " and e.PolicyAccountID=b.PolicyAccountID" _
'     + " and f.customerID = e.customerID" _
'     + " and c.accountstatuscode in('1','2','3','4','V')" _
'     + " and e.policyRelateCode ='PH'" _
'     + " and e.customerID is not null" _
'     + " and (a.productID like '%I368%' or a.productID like '%I152%' or a.productID like '%H01%' or a.productID like '%RH01%' or a.productID like '%AM%' or a.productID like '%RAM%')" _
'     + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
'     + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

' Medipro 100
'str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'     + " Where a.policyAccountID = b.policyAccountID" _
'     + " and a.productID=b.productID" _
'     + " and a.trailer = b.trailer" _
'     + " and b.policyAccountID = c.policyAccountID" _
'     + " and a.CoverageStatus in('1','2','3','4','V')" _
'     + " and ((a.productID like '%RSOS%') or (a.productID like '%H01%') or (a.productID like '%RH01%') or (a.productID like '%AM%') or (a.productID like '%RAM%'))" _
'     + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
'     + " and a.ProductID=d.ProductID" _
'     + " and e.customerID=b.customerID" _
'     + " and e.PolicyAccountID=b.PolicyAccountID" _
'     + " and e.policyRelateCode IN ('PH', 'PI')" _
'     + " and f.customerID = e.customerID" _
'     + " and e.customerID is not null" _
'     + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

' ES01 begin
    Dim planstr As String
    
    If GetSOSPlan(strCompany, planstr) Then
        If strCompany = "MCU" Then
'            str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.Description, " _
'            + " case when rtrim(f.chilstnm)+rtrim(f.chifstnm)='' then f.namesuffix + ' ' + f.firstName else rtrim(f.chilstnm)+rtrim(f.chifstnm) end as contact, " _
'            + " f.GovernmentIDCard " _
'            + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'            + " Where a.policyAccountID = b.policyAccountID" _
'            + " and a.productID=b.productID" _
'            + " and a.trailer = b.trailer" _
'            + " and b.policyAccountID = c.policyAccountID" _
'            + " and a.CoverageStatus in('1','2','3','4','V')" _
'            + " and (a.productid like '_RHC%' or a.productid like '_RHCB%' or a.productid like '_RHI%' or a.productid like '_I368%' or a.productid like '_I152%' or a.productid like '_RAM%' or a.productid like '_AM%' or a.productid in ('HPC2' , 'HCB2', 'AM11' , 'AMB1') ) " _
'            + " and a.ProductID=d.ProductID" _
'            + " and e.customerID=b.customerID" _
'            + " and e.PolicyAccountID=b.PolicyAccountID" _
'            + " and e.policyRelateCode IN ('PH', 'PI')" _
'            + " and f.customerID = e.customerID" _
'            + " and e.customerID is not null" _
'            + " Order by e.customerID ASC, a.exhibitinforcedate ASC "
            str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.Description, " _
            + " case when rtrim(f.chilstnm)+rtrim(f.chifstnm)='' then f.namesuffix + ' ' + f.firstName else rtrim(f.chilstnm)+rtrim(f.chifstnm) end as contact, " _
            + " f.GovernmentIDCard " _
            + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
            + " Where a.policyAccountID = b.policyAccountID" _
            + " and a.productID=b.productID" _
            + " and a.trailer = b.trailer" _
            + " and b.policyAccountID = c.policyAccountID" _
            + " and a.CoverageStatus in('1','2','3','4','V')" _
            + " and " & planstr _
            + " and a.ProductID=d.ProductID" _
            + " and e.customerID=b.customerID" _
            + " and e.PolicyAccountID=b.PolicyAccountID" _
            + " and e.policyRelateCode IN ('PH', 'PI')" _
            + " and f.customerID = e.customerID" _
            + " and e.customerID is not null" _
            + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

            Set rec1 = db.ExecuteStatement(str1)
        Else
    ' ES01 end
'            str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'            + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'            + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'            + " Where a.policyAccountID = b.policyAccountID" _
'            + " and a.productID=b.productID" _
'            + " and a.trailer = b.trailer" _
'            + " and b.policyAccountID = c.policyAccountID" _
'            + " and a.CoverageStatus in('1','2','3','4','V')" _
'            + " and ((a.productID like '%RSOS%') or (a.productID like '%H01%') or (a.productID like '%RH01%') or (a.productID like '%AM%') or (a.productID like '%RAM%') or (a.productid like '_H07%') or (a.productid like '_RH07%')) " _
'            + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
'            + " and a.ProductID=d.ProductID" _
'            + " and e.customerID=b.customerID" _
'            + " and e.PolicyAccountID=b.PolicyAccountID" _
'            + " and e.policyRelateCode IN ('PH', 'PI')" _
'            + " and f.customerID = e.customerID" _
'            + " and e.customerID is not null" _
'            + " Order by e.customerID ASC, a.exhibitinforcedate ASC "
            str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
            + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
            + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
            + " Where a.policyAccountID = b.policyAccountID" _
            + " and a.productID=b.productID" _
            + " and a.trailer = b.trailer" _
            + " and b.policyAccountID = c.policyAccountID" _
            + " and a.CoverageStatus in('1','2','3','4','V')" _
            + " and " & planstr _
            + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
            + " and a.ProductID=d.ProductID" _
            + " and e.customerID=b.customerID" _
            + " and e.PolicyAccountID=b.PolicyAccountID" _
            + " and e.policyRelateCode IN ('PH', 'PI')" _
            + " and f.customerID = e.customerID" _
            + " and e.customerID is not null" _
            + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

            Set rec1 = db.ExecuteStatement(str1)
        End If
    Else
        MsgBox ("Cannot get the plan code!")
    End If

End Sub

'ITDTLW001 new function
Public Sub getPrintOutCrisis(pstrDD As String, pstrMM As String, pstrYY As String)
Dim str1 As String
Dim StrDate As Date
Dim EndDate As Date
Dim ReportDate As String

ReportDate = CDate(pstrMM & "/" & pstrDD & "/" & pstrYY)
'StrDate = CDate(CStr(DatePart("m", ReportDate) - 1) & "/01/" & CStr(DatePart("yyyy", ReportDate)))
'EndDate = DateAdd("d", -1, CDate(CStr(DatePart("m", ReportDate)) & "/01/" & CStr(DatePart("yyyy", ReportDate))))


'str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'     + " Where a.policyAccountID = b.policyAccountID" _
'     + " and a.productID=b.productID" _
'     + " and a.trailer = b.trailer" _
'     + " and b.policyAccountID = c.policyAccountID" _
'     + " and c.productID=d.productID" _
'     + " and c.companyID=d.companyID" _
'     + " and e.customerID=b.customerID" _
'     + " and e.PolicyAccountID=b.PolicyAccountID" _
'     + " and f.customerID = e.customerID" _
'     + " and c.accountstatuscode in('1','2','3','4','V')" _
'     + " and e.policyRelateCode ='PH'" _
'     + " and e.customerID is not null" _
'     + " and (a.productID like '%I368%' or a.productID like '%I152%' or a.productID like '%H01%' or a.productID like '%RH01%' or a.productID like '%AM%' or a.productID like '%RAM%')" _
'     + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
'     + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

' Medipro 100
'str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'     + " Where a.policyAccountID = b.policyAccountID" _
'     + " and a.productID=b.productID" _
'     + " and a.trailer = b.trailer" _
'     + " and b.policyAccountID = c.policyAccountID" _
'     + " and a.CoverageStatus in('1','2','3','4','V')" _
'     + " and ((a.productID like '%RSOS%') or (a.productID like '%H01%') or (a.productID like '%RH01%') or (a.productID like '%AM%') or (a.productID like '%RAM%'))" _
'     + " and month(a.ExhibitInforceDate) = '" + Format(pstrMM, "00") + "'" _
'     + " and a.ProductID=d.ProductID" _
'     + " and e.customerID=b.customerID" _
'     + " and e.PolicyAccountID=b.PolicyAccountID" _
'     + " and e.policyRelateCode IN ('PH', 'PI')" _
'     + " and f.customerID = e.customerID" _
'     + " and e.customerID is not null" _
'     + " Order by e.customerID ASC, a.exhibitinforcedate ASC "
'ITDTLW001
'str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description," _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard" _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f" _
'     + " Where a.policyAccountID = b.policyAccountID" _
'     + " and a.productID=b.productID" _
'     + " and a.trailer = b.trailer" _
'     + " and b.policyAccountID = c.policyAccountID" _
'     + " and a.CoverageStatus in('1','2','3','4','V')" _
'     + " and (" _
'     + " (a.productid like '_CM%') or (a.productid like '_CS%') or" _
'     + " (a.productid like '%CPD%') or (a.productid like '%RCP%') or " _
'     + " (a.productid like '%CCA%') or (a.productid like '%CCP%') or " _
'     + " (a.productid like '_CI%') or (a.productid like '%AE%'))" _
'     + " and a.ExhibitInforceDate <= '" & ReportDate & "'" _
'     + " and a.ProductID=d.ProductID" _
'     + " and e.customerID=b.customerID" _
'     + " and e.PolicyAccountID=b.PolicyAccountID" _
'     + " and e.policyRelateCode IN ('PH', 'PI')" _
'     + " and f.customerID = e.customerID" _
'     + " and e.customerID is not null" _
'     + " Order by e.customerID ASC, a.exhibitinforcedate ASC "

'ITDTLW002
'     str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description, " _
'     + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard " _
'     + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f " _
'     + " Where a.policyAccountID = b.policyAccountID " _
'     + " and a.productID=b.productID " _
'     + " and a.trailer = b.trailer " _
'     + " and b.policyAccountID = c.policyAccountID " _
'     + " and a.CoverageStatus in('1','2','3','4','V') " _
'     + " and ( " _
'     + " (a.productid like '_CM%') or (a.productid like '_CS%') or " _
'     + " (a.productid like '%CPD%') or (a.productid like '%RCP%') or " _
'     + " (a.productid like '%CCA%') or (a.productid like '%CCP%') or " _
'     + " (a.productid like '_CI%') or (a.productid like '%AE%')) " _
'     + " and a.ExhibitInforceDate <= '" & Format(ReportDate, "yyyy-MM-dd") & "'" _
'     + " and a.ProductID=d.ProductID " _
'     + " and e.customerID=b.customerID " _
'     + " and e.PolicyAccountID=b.PolicyAccountID " _
'     + " and e.policyRelateCode IN ('PI') " _
'     + " and f.customerID = e.customerID " _
'     + " and e.customerID is not null " _
'     + " Order by a.exhibitinforcedate, a.policyaccountid, e.customerID "
     
'ITDTLW003
     Dim planstr As String
     If GetCrisisPlan(planstr) Then
          str1 = "Select e.customerID, a.policyAccountID, a.productID, a.exhibitinforcedate, d.description, " _
            + " f.namesuffix + ' ' + f.firstName as contact, f.GovernmentIDCard " _
            + " from coverage a, coverageDetail b, policyAccount c, product d, csw_poli_rel e, customer f " _
            + " Where a.policyAccountID = b.policyAccountID " _
            + " and a.productID=b.productID " _
            + " and a.trailer = b.trailer " _
            + " and b.policyAccountID = c.policyAccountID " _
            + " and a.CoverageStatus in('1','2','3','4','V') " _
            + " and a.productid in (" & planstr & ") " _
            + " and a.ExhibitInforceDate <= '" & Format(ReportDate, "yyyy-MM-dd") & "'" _
            + " and a.ProductID=d.ProductID " _
            + " and e.customerID=b.customerID " _
            + " and e.PolicyAccountID=b.PolicyAccountID " _
            + " and e.policyRelateCode IN ('PI') " _
            + " and f.customerID = e.customerID " _
            + " and e.customerID is not null " _
            + " Order by a.exhibitinforcedate, a.policyaccountid, e.customerID "
     Else
        MsgBox ("Cannot get the plan code!")
     End If
Set rec1 = db.ExecuteStatement(str1)

End Sub


'** Codes for AY02 start **
Public Function GetSurveyReportData(ByVal DateFrom As Date, ByVal DateTo As Date) As ADODB.Recordset
    Dim strsql As String
    ' First, select relevant data (Inforce Date, Customer ID, Policy ID, Agent Code, Print Flag and Print Date)
    '   with criteria print date is this day and survey has been printed
    ' Second, get agent unit code
    ' Third, get name of agent
    ' Finally, get name of customer
    ' Date range get from the report date for the card / PIN letter report.  Inforce date is not checked as
    '   inforce date is not necessary to be one day before print date (e.g. To get benifits)
    strsql = "select distinct e.infordate, e.custid, e.polid, isnull(e.agcode,''), e.prn, e.prndate, isnull(e.unitcode,''), isnull(e.agname,''), case when f.gender='C' then f.coname else rtrim(f.namesuffix)+' '+rtrim(f.firstname) end as cusname from " & _
               "(select c.infordate, c.custid, c.polid, c.agcode, c.prn, c.prndate, c.unitcode, case when d.gender='C' then d.coname else rtrim(d.namesuffix)+' '+rtrim(d.firstname) end as agname from " & _
                 "(select a.infordate, a.custid, a.polid, a.agcode, a.prn, a.prndate, b.unitcode as unitcode from " & _
                   "(select distinct cswpuw_infor_date as infordate, cswppp_cid as custid, cswppp_pid as polid, cswppp_agentcode as agcode, cswppp_fstprn as prn, cswppp_prn_date as prndate " & _
                   "from csw_prn_per_pol inner join csw_policy_uw on cswppp_pid = cswpuw_poli_id " & _
                   "where (cswppp_prn_date >= '" & Format(DateFrom + 1, "yyyy-MM-dd") & "' and cswppp_prn_date < '" & Format(DateTo + 1, "yyyy-MM-dd") & "') " & _
                   "and csw_prn_per_pol.cswppp_fstprn='Y') " & _
                 "a left outer join agentcodes b on a.agcode=b.agentcode) " & _
               "c left outer join customer d on c.agcode=d.agentcode) " & _
             "e left outer join customer f on e.custid=f.customerid order by polid"
    Set GetSurveyReportData = db.ExecuteStatement(strsql)
End Function

'ITDTLW003 start
Public Function GetCrisisPlan(ByRef planstr As String) As Boolean
    Dim rsplan As ADODB.Recordset
    Dim strsql As String
    
    strsql = "select * from csw_system_value where cswsyv_field_name like 'CRISIS_PN%'"
    Set rsplan = db.ExecuteStatement(strsql)
    
    If rsplan.RecordCount > 0 Then
        While Not rsplan.EOF
            planstr = planstr & rsplan.Fields("cswsyv_value").Value
            rsplan.MoveNext
        Wend
        GetCrisisPlan = True
    Else
        GetCrisisPlan = False
    End If
End Function

'ITDTLW004 start
Public Function GetSOSPlan(ByVal comp As String, ByRef planstr As String) As Boolean
    Dim rsplan As ADODB.Recordset
    Dim strsql As String
    
    If comp <> "MCU" Then
        strsql = "select * from csw_system_value where cswsyv_field_name like 'SOS_PN%'"
    Else
        strsql = "select * from csw_system_value where cswsyv_field_name like 'MSOS_PN%'"
    End If
    Set rsplan = db.ExecuteStatement(strsql)
    
    If rsplan.RecordCount > 0 Then
        While Not rsplan.EOF
            planstr = planstr & rsplan.Fields("cswsyv_value").Value
            rsplan.MoveNext
        Wend
        GetSOSPlan = True
    Else
        GetSOSPlan = False
    End If
End Function

Public Function GetPrinterName(ByVal company As String) As Boolean
    Dim rsPrinter As ADODB.Recordset
    Dim strsql As String
    
    If Get_Connect("Life") Then 'Hard code to use Life connection (Printer share with GI)
        strsql = "select * from csw_system_value where cswsyv_field_name = 'CARDPTR'"
        Set rsPrinter = db.ExecuteStatement(strsql)
    
        PRINTER_NAME = rsPrinter.Fields("cswsyv_value").Value
    End If
End Function
'ITDTLW003 End

