VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Object = "{00025600-0000-0000-C000-000000000046}#5.2#0"; "crystl32.ocx"
Begin VB.Form frmMonthEndLife 
   Caption         =   "SOS Monthly Report "
   ClientHeight    =   2235
   ClientLeft      =   6270
   ClientTop       =   4455
   ClientWidth     =   3975
   LinkTopic       =   "Form1"
   ScaleHeight     =   2235
   ScaleWidth      =   3975
   Begin VB.CommandButton cmdPrint 
      Caption         =   "&Print"
      Height          =   375
      Left            =   480
      TabIndex        =   3
      Top             =   1560
      Width           =   1215
   End
   Begin VB.CommandButton cmdExit 
      Caption         =   "E&xit"
      Height          =   375
      Left            =   2160
      TabIndex        =   2
      Top             =   1560
      Width           =   1215
   End
   Begin MSComCtl2.DTPicker dtpGenDate 
      Height          =   285
      Left            =   1680
      TabIndex        =   0
      Top             =   720
      Width           =   1815
      _ExtentX        =   3201
      _ExtentY        =   503
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   49610755
      CurrentDate     =   36511
   End
   Begin Crystal.CrystalReport CrystalReport 
      Left            =   120
      Top             =   1080
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   348160
      Destination     =   2
      WindowControlBox=   -1  'True
      WindowMaxButton =   -1  'True
      WindowMinButton =   -1  'True
      PrintFileType   =   2
      PrintFileLinesPerPage=   60
   End
   Begin VB.Label lblGenDate 
      Caption         =   "Month End"
      Height          =   255
      Left            =   480
      TabIndex        =   1
      Top             =   720
      Width           =   975
   End
End
Attribute VB_Name = "frmMonthEndLife"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'*******************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Mar 31 2003
'*     Reference  : AY07
'*     Description: The following changes made:
'*                  - Remove calls to database table ORDCNA, which is not used
'*                    now
'*                  - Change the path of saving the SOS Monthly Report from
'*                    \\Eaant2\prodapps\ciw\report\ to
'*                    \\eaant2\prodapps\CIW\SOSRPT\
'*******************************************************************************
'*     Amended By : Eric Shu
'*     Date       : Dec 2009
'*     Reference  : ES01
'*     Description: Generate SOS file for Macau
'******************************************************************************************

Option Explicit

Private str2 As New Scripting.FileSystemObject
Private Const strTmpXls = "C:\temp\temp.xls"
'Save Excel file
' ** Change for AY07 start **
Private Const userFilePath = "\\Eaant2\ProdApps\CIW\SOSRPT\" 'PRD
'Private Const userFilePath = "\\Eaant2\prodapps\ciw\report\"     'EC06
'Private Const userFilePath = "C:\temp\"                        'EC06
' ** Change for AY07 end **
Private Const strFilename = "SOSMonthlyRpt"
Private Const strHead1 = "SOS Monthly Report"
Private Const strHead2 = "     For "
Private Const strHead3 = "Print Date: "

Private Const strSubTt1 = "ID Number"
Private Const strSubTt2 = "Policy Number"
Private Const strSubTt3 = "Customer Name"
Private Const strSubTt4 = "HKID"
Private Const strSubTt5 = "Plan Name"
Private Const strSubTt6 = "Inforce Date"
Private Const strSubTt7 = "New Business"

Private Const MaxRowPerSheet = 60000 'Excel 2003 limitation 65536 split other worksheet will rowcount > the const

Private Sub cmdExit_Click()
Unload Me
End Sub

Private Sub cmdPrint_Click()
Call genMonReport(Format(dtpGenDate, "DD/MM/YYYY"))
Call genMonReport(Format(dtpGenDate, "DD/MM/YYYY"), "MCU") ' ES01
MsgBox "This job is finsihed!!!", vbOKOnly
Unload Me
End Sub

Public Sub Form_Load()
    dtpGenDate = Format(Date, "DD/MM/YYYY")
End Sub

Public Sub genMonReport(strMonDate As String, Optional strCompany As String = "")

Dim dbstr As String
Dim strMM As String
Dim strYY As String
Dim strFromDate As String
Dim strToDate As String

Dim appExcel As New Excel.Application
Dim wbExcel As Excel.Workbook
Dim shtExcel As Excel.Worksheet
Dim cellExcel As Excel.Range

Dim X As Long       'Row
Dim Y As Integer       'Col
Dim strMonEnd As String
Dim Pstrdb As String
Dim PstrODBCname As String
Dim strIndate As String

Dim strPreCustID As String
'Dim strPrePolNo As String
Dim strPreName As String

Dim i As Integer
Dim RecIndex As Long


X = 2                  'inital 2 col

' ES01 begin
'Pstrdb = "Life"
'PstrODBCname = "Life"
If strCompany = "MCU" Then
    Pstrdb = "MCU"
    PstrODBCname = "MCU"
Else
    Pstrdb = "Life"
    PstrODBCname = "Life"
End If
' ES01 end

strMM = Mid(strMonDate, 4, 2)
strYY = Mid(strMonDate, 7, 4)
strMonEnd = Trim(strMM + "/" + strYY)

Call Get_Connect(Pstrdb)
'If frmMenu.get_db(dbstr, PstrODBCname) Then  ' bypass server 2012 DBLogon error -  20180430(Clara)
 If True Then
    'ES01 begin
    'Call getPrintOut(strMM, strYY)
    Call getPrintOut(strMM, strYY, strCompany)
    'ES01 end
    
    If Not rec1.EOF Then
    
       str2.CreateTextFile (strTmpXls)
       
       'Excel 2003 limitation handle begin
       'Set wbExcel = appExcel.Workbooks.Open(strTmpXls)         'Template Format for Excel File
       Set wbExcel = appExcel.Workbooks.Add
       
       For i = 4 To Int(rec1.RecordCount / MaxRowPerSheet) + 1
            wbExcel.Worksheets.Add
       Next
       'Excel 2003 limitation handle begin
       
       Set shtExcel = wbExcel.Worksheets(1)
       shtExcel.Name = "1 ~ " & MaxRowPerSheet 'Excel 2003 limitation handle
       Set cellExcel = shtExcel.Cells
       
       cellExcel(4, 3) = strHead1
       cellExcel(5, 3) = strHead2 + strMonEnd
       cellExcel(5, 5) = strHead3 + Format(Date, "DD MMM YYYY")
       
       cellExcel(8, 1) = strSubTt1
       cellExcel(8, 2) = strSubTt2
       cellExcel(8, 3) = strSubTt3
       cellExcel(8, 4) = strSubTt4
       cellExcel(8, 5) = strSubTt5
       cellExcel(8, 6) = strSubTt6
       cellExcel(8, 7) = strSubTt7
       
       X = 9
             
       While Not rec1.EOF
            RecIndex = RecIndex + 1 'Excel 2003 limitation handle
            
            Dim strCurCustID As String
            Dim strCurPolNo As String
            Dim strCurName As String       'EC04
            
            strCurCustID = Trim(rec1!customerID)
            strCurPolNo = Trim(rec1!policyAccountID)
            strCurName = Trim(rec1!contact) 'EC04
            
            'Excel 2003 limitation handle begin
            If RecIndex Mod MaxRowPerSheet >= 1 And RecIndex > MaxRowPerSheet Then
                   Set shtExcel = wbExcel.Worksheets(Int(RecIndex / MaxRowPerSheet) + 1)
                   Set cellExcel = shtExcel.Cells
                   If RecIndex Mod MaxRowPerSheet = 1 Then
                      shtExcel.Name = RecIndex & " ~ " & RecIndex + MaxRowPerSheet - 1
                   End If
                    cellExcel(4, 3) = strHead1
                    cellExcel(5, 3) = strHead2 + strMonEnd
                    cellExcel(5, 5) = strHead3 + Format(Date, "DD MMM YYYY")

                    cellExcel(8, 1) = strSubTt1
                    cellExcel(8, 2) = strSubTt2
                    cellExcel(8, 3) = strSubTt3
                    cellExcel(8, 4) = strSubTt4
                    cellExcel(8, 5) = strSubTt5
                    cellExcel(8, 6) = strSubTt6
                    cellExcel(8, 7) = strSubTt7
                    
                    X = RecIndex Mod MaxRowPerSheet + 8
            End If
            'Excel 2003 limitation handle End
            
            'If strCurCustID <> strPreCustID And strCurPolNo <> strPrePolNo Then
            'If strCurCustID <> strPreCustID And strCurName <> strPreName Then
                For Y = 1 To 7
                '// Fill your Data to Excel here (starts with row 2)
                '// Row 1 must be the Merge Fields Name
                Select Case Y
                    Case 1
                        cellExcel(X, Y) = Trim(rec1!customerID)
                    Case 2
                        cellExcel(X, Y) = Trim(rec1!policyAccountID)
                    Case 3
                        cellExcel(X, Y) = Trim(rec1!contact)
                    Case 4
                        cellExcel(X, Y) = Trim(rec1!GovernmentIDCard)
                    Case 5
                        cellExcel(X, Y) = Trim(rec1!Description)
                    Case 6
                        Dim str1 As String
                        str1 = Format(rec1!exhibitinforcedate, "MMM DD YYYY")
                        cellExcel(X, Y) = str1
                    Case 7
                        strIndate = ""
                        strIndate = Left(Format(Trim(rec1!exhibitinforcedate), "MM/DD/YYYY"), 3) + Right(Format(Trim(rec1!exhibitinforcedate), "MM/DD/YYYY"), 4)
                        If strIndate = strMonEnd Then
                            cellExcel(X, Y) = "Y"
                        Else
                            cellExcel(X, Y) = "N"
                        End If
                    End Select
                Next Y
                X = X + 1
            'End If
            strPreCustID = strCurCustID
            'strPrePolNo = strCurPolNo
            strPreName = strCurName
            rec1.MoveNext
        Wend
        
        wbExcel.Application.DisplayAlerts = False
        
        ' ES01 begin
        If strCompany = "MCU" Then
            wbExcel.SaveAs userFilePath + strFilename + strCompany + strMM + strYY + ".xls"   'Save As
        Else
        ' ES01 end
        
            wbExcel.SaveAs userFilePath + strFilename + strMM + strYY + ".xls"   'Save As
        End If
        
        wbExcel.Application.DisplayAlerts = True
        wbExcel.Close False
        appExcel.Quit
        Kill (strTmpXls)
    Else
        MsgBox "No record exist!", vbOKOnly
    End If
    
    'Call Crystal Report *********************************************
    'CrystalReport.Reset
    'CrystalReport.Connect = "DSN = " & "" & "; " & dbstr
    'CrystalReport.Destination = crptToPrinter '- DayendSet
    'CrystalReport.ReportFileName = App.Path + "\SOSMonthlyReport.rpt"
    'CrystalReport.ParameterFields(0) = "MM;" + strMM + ";True"
    'CrystalReport.ParameterFields(1) = "YY;" + strYY + ";True"
    'CrystalReport.WindowShowPrintSetupBtn = True
    'CrystalReport.Action = 1
    '*****************************************************************
    'MsgBox "This job is finsihed!!!", vbOKOnly
    'Unload Me '- DayendSet
Else
    MsgBox "Fail to connect database!!!", vbOKOnly
End If

End Sub
