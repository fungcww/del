VERSION 5.00
Object = "{00025600-0000-0000-C000-000000000046}#5.2#0"; "Crystl32.OCX"
Begin VB.Form frmMenu 
   AutoRedraw      =   -1  'True
   BorderStyle     =   0  'None
   Caption         =   "Print Card and Pin Number Letter Program"
   ClientHeight    =   1740
   ClientLeft      =   5430
   ClientTop       =   3735
   ClientWidth     =   4830
   FillStyle       =   0  'Solid
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1740
   ScaleWidth      =   4830
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton cmdGo 
      Caption         =   "Go"
      Height          =   495
      Left            =   600
      TabIndex        =   7
      Top             =   1080
      Width           =   1455
   End
   Begin VB.ComboBox cmbForR 
      Enabled         =   0   'False
      Height          =   315
      ItemData        =   "frmMenu.frx":0000
      Left            =   3240
      List            =   "frmMenu.frx":000A
      TabIndex        =   6
      Text            =   "(1st or Reprint)"
      Top             =   600
      Width           =   1455
   End
   Begin VB.ComboBox cmbJob 
      Enabled         =   0   'False
      Height          =   315
      ItemData        =   "frmMenu.frx":0024
      Left            =   1680
      List            =   "frmMenu.frx":0031
      TabIndex        =   5
      Text            =   "(Job)"
      Top             =   600
      Width           =   1455
   End
   Begin VB.ComboBox cmbCom 
      Enabled         =   0   'False
      Height          =   315
      ItemData        =   "frmMenu.frx":004E
      Left            =   120
      List            =   "frmMenu.frx":005E
      TabIndex        =   4
      Text            =   "(Company)"
      Top             =   600
      Width           =   1455
   End
   Begin Crystal.CrystalReport CRT 
      Left            =   0
      Top             =   1200
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   348160
      WindowControlBox=   -1  'True
      WindowMaxButton =   -1  'True
      WindowMinButton =   -1  'True
      PrintFileLinesPerPage=   60
   End
   Begin VB.CommandButton cmdExit 
      Caption         =   "&Exit"
      Height          =   495
      Left            =   2760
      TabIndex        =   0
      Top             =   1080
      Width           =   1455
   End
   Begin VB.Frame Frame1 
      BorderStyle     =   0  'None
      Height          =   1575
      Left            =   0
      TabIndex        =   8
      Top             =   120
      Width           =   4815
   End
   Begin VB.Label lblForR 
      Caption         =   "First or Reprint"
      Height          =   255
      Left            =   3360
      TabIndex        =   3
      Top             =   240
      Width           =   1215
   End
   Begin VB.Label lblJob 
      Caption         =   "Job"
      Height          =   255
      Left            =   1800
      TabIndex        =   2
      Top             =   240
      Width           =   1215
   End
   Begin VB.Label lblCom 
      Caption         =   "Company"
      Height          =   255
      Left            =   240
      TabIndex        =   1
      Top             =   240
      Width           =   1215
   End
   Begin VB.Menu mnuPrint 
      Caption         =   "Print &Cards"
      Begin VB.Menu mnulife 
         Caption         =   "&Life"
         Begin VB.Menu mnuCardLife 
            Caption         =   "Card (First Print)"
         End
         Begin VB.Menu mnuPLLife 
            Caption         =   "PinLetter (First Print)"
         End
         Begin VB.Menu SEP0 
            Caption         =   "-"
         End
         Begin VB.Menu mnuSurvey 
            Caption         =   "Survey"
         End
         Begin VB.Menu mnuCoupon 
            Caption         =   "Coupon"
         End
         Begin VB.Menu SEP1 
            Caption         =   "-"
            Index           =   1
         End
         Begin VB.Menu mnuCardLifeR 
            Caption         =   "Card (Reprint)"
         End
         Begin VB.Menu mnuPLLifeR 
            Caption         =   "PinLetter (Reprint)"
         End
         Begin VB.Menu SEP7 
            Caption         =   "-"
         End
         Begin VB.Menu mnuReportLife 
            Caption         =   "Report"
         End
      End
      Begin VB.Menu mnuMcu 
         Caption         =   "&Macau"
         Begin VB.Menu mnuCardMCU 
            Caption         =   "Card (First Print)"
         End
         Begin VB.Menu mnuPLMCU 
            Caption         =   "PinLetter (First Print)"
         End
         Begin VB.Menu SEP8 
            Caption         =   "-"
         End
         Begin VB.Menu mnuCardMCUR 
            Caption         =   "Card (Reprint)"
         End
         Begin VB.Menu mnuPLMCUR 
            Caption         =   "PinLetter (Reprint)"
         End
      End
      Begin VB.Menu mnuAetna 
         Caption         =   "&MPF"
         Begin VB.Menu mnuCardAetna 
            Caption         =   "Card (First Print)"
         End
         Begin VB.Menu mnuPLAetna 
            Caption         =   "PinLetter"
         End
         Begin VB.Menu mnuReportAetna 
            Caption         =   "Report"
         End
         Begin VB.Menu SEP2 
            Caption         =   "-"
            Index           =   2
         End
         Begin VB.Menu mnuCardAetnaR 
            Caption         =   "Card (Reprint)"
         End
      End
      Begin VB.Menu mnuDresd 
         Caption         =   "&Dresdner"
         Begin VB.Menu mnuCardDresd 
            Caption         =   "Welcome Letter (First Print)"
         End
         Begin VB.Menu mnuPLDresd 
            Caption         =   "PinLetter"
         End
         Begin VB.Menu mnuReportDresd 
            Caption         =   "Report"
         End
         Begin VB.Menu SEP3 
            Caption         =   "-"
            Index           =   3
         End
         Begin VB.Menu mnuCardDresdR 
            Caption         =   "Welcome Letter (Reprint)"
         End
      End
      Begin VB.Menu mnuSchrd 
         Caption         =   "&Schroder"
         Begin VB.Menu mnuCardSchrd 
            Caption         =   "Card (First Print)"
         End
         Begin VB.Menu mnuPLSchrd 
            Caption         =   "PinLetter"
         End
         Begin VB.Menu mnuReportSchrd 
            Caption         =   "Report"
         End
         Begin VB.Menu SEP4 
            Caption         =   "-"
            Index           =   4
         End
         Begin VB.Menu mnuCardSchrdR 
            Caption         =   "Card (Reprint)"
         End
      End
      Begin VB.Menu mnuMcDonald 
         Caption         =   "M&cDonald"
         Begin VB.Menu mnuCardMcDonald 
            Caption         =   "Card (First Print)"
         End
         Begin VB.Menu mnuPLMcDonald 
            Caption         =   "PinLetter"
         End
         Begin VB.Menu mnuReportMcDonald 
            Caption         =   "Report"
         End
         Begin VB.Menu SEP5 
            Caption         =   "-"
            Index           =   5
         End
         Begin VB.Menu mnuCardMcDonaldR 
            Caption         =   "Card (Reprint)"
         End
      End
      Begin VB.Menu mnuORSO 
         Caption         =   "&ORSO"
         Begin VB.Menu mnuCardORSO 
            Caption         =   "Card Letter(First Print)"
         End
         Begin VB.Menu mnuPLORSO 
            Caption         =   "PinLetter"
         End
         Begin VB.Menu mnuReportORSO 
            Caption         =   "Report"
         End
         Begin VB.Menu SEP6 
            Caption         =   "-"
         End
         Begin VB.Menu mnuCardORSOR 
            Caption         =   "Card Letter(Reprint)"
         End
      End
      Begin VB.Menu mnuGISOS 
         Caption         =   "&GI SOS Card"
         Begin VB.Menu mnuGIMedSOS 
            Caption         =   "GI Medical SOS"
         End
         Begin VB.Menu mnuGINonMedSOS 
            Caption         =   "GI NonMedical SOS"
         End
      End
   End
   Begin VB.Menu mnuJobRun 
      Caption         =   "&Report "
      Begin VB.Menu mnuCrisisMon 
         Caption         =   "Crisis Monthly - Life"
      End
      Begin VB.Menu mnuLifeMon 
         Caption         =   "SOS Monthl&y - Life"
      End
   End
End
Attribute VB_Name = "frmMenu"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'******************************************************************************************
'*     Amended By : Anson Chan
'*     Date       : May 15 2001
'*     Reference  : AC01
'*     Description: Adding McDonald Into Card/PIN Letter Program
'******************************************************************************************
'*     Amended By : Eammon Chen
'*     Date       : April 17 2002
'*     Reference  : EC01
'*     Description: Changing the menu format and add in new form for MonthEnd job
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Sept 9 2002
'*     Reference  : AY01
'*     Description: Use Chinese Address if UseChiInd in CustomerAddress is 'Y' for PIN
'*                  letter and survey
'******************************************************************************************
'*     Amended By : Anthony Yuen
'*     Date       : Oct 9 2002
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
'*     Description: Generate error message for errors from printing cards
'******************************************************************************************
'*     Amended By : Carmen Wong
'*     Date       : Dec 29 2003
'*     Reference  : CW01
'*     Description: Set printer information before print Reprint-Card report
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
'*     Reference  : ITDTLW0001
'*     Description: Add new form for crisis report
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
'*     Amended By : To Wai Lung
'*     Date       : JUL 2011
'*     Reference  : ITDTLW005
'*     Description: PHW Revamp Print Card and PIN Letter function for Macau
'******************************************************************************************
 

Public PstrODBCname As String


Private Sub cmdExit_Click()
    End
End Sub
Private Sub cmdGo_Click()
Dim strFR As String
Dim strLM As String
Dim strM As String

If cmbForR = "RePrint" Then
    strFR = "R"
Else
    strFR = "F"
End If

Select Case CStr(cmbCom.Text)
    'ITDTLW005 Start ********************************
    Case "MCU"
        strLM = "csw"
        strM = "MCU"
    'ITDTLW005 End ********************************
    Case "Life"
        strLM = "csw"
        strM = "Aetna LIFE"
    Case "Aetna"
        strLM = "mpf"
        strM = "Aetna MPF"
    Case "Dresd"
        strLM = "mpf"
        strM = "Dresdner MPF"
    Case "Schrd"
        strLM = "mpf"
        strM = "Schroder MPF"
    '// AC01 Added - Begin //
    Case "McDonald"
        strLM = "mpf"
        strM = "McDonald"
    '// AC01 Added - End //
    Case "ORSO"
        strLM = "mpf"
        strM = "ORSO"
    'Add WC102007  Add display message before print card
    Case "GI"
        strLM = CStr(cmbJob.Text)
        Select Case CStr(cmbJob.Text)
            Case "Non Medical SOS Card"
                'strLM = "GI"
                strM = "[[[Underwriting SOS Card]]] ----- company name at the top of the card"
            Case "Medical SOS Card"
                'strLM = "GI"
                strM = "[[[Medical SOS Card]]] ----- 'SOS' logo at the right top of the card"
        End Select
    'End Add WC102007
End Select

Select Case CStr(cmbJob.Text)
    Case "Card"
        Call PrintCard(Trim(cmbCom.Text), strFR, strM, strLM)
    Case "PinLetter"
        'Call PrintPL(strM, Trim(cmbCom.Text), strLM)
        Call PrintPL(strM, Trim(cmbCom.Text), strLM, strFR)
    Case "Report"
        Call PrtReport(Trim(cmbCom.Text))
    Case "Survey"
        Call PrtSurvey(Trim(cmbCom.Text))
    Case "Coupon"
        Call PrtCoupon(Trim(cmbCom.Text))
    'Add WC102007 - GI part to call print card
    Case "Non Medical SOS Card", "Medical SOS Card"
        Call PrintCard(Trim(cmbCom.Text), strFR, strM, strLM)
    'End Add WC102007
End Select
End Sub

Private Sub mnuCardAetna_Click()
cmbCom.Text = "Aetna"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardAetnaR_Click()
cmbCom.Text = "Aetna"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardDresd_Click()
cmbCom.Text = "Dresd"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "MPFDresd"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardDresdR_Click()
cmbCom.Text = "Dresd"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "MPFDresd"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardLife_Click()
cmbCom.Text = "Life"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "CIW"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardLifeR_Click()
cmbCom.Text = "Life"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "CIW"
Frame1.Visible = False   'EC01
End Sub

'ITDTLW005 Start ***********************************************************************
Private Sub mnuCardMCU_Click()
cmbCom.Text = "MCU"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "CIW"
Frame1.Visible = False
End Sub

Private Sub mnuCardMCUR_Click()
cmbCom.Text = "MCU"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "CIW"
Frame1.Visible = False
End Sub
'ITDTLW005 End ***********************************************************************

Private Sub mnuCardORSO_Click()
cmbCom.Text = "ORSO"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardORSOR_Click()
cmbCom.Text = "ORSO"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardSchrd_Click()
cmbCom.Text = "Schrd"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "MPFSchrd"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCardSchrdR_Click()
cmbCom.Text = "Schrd"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "MPFSchrd"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCrisisMon_Click()
    Frame1.Visible = True   'ITDTLW001
    frmMonthEndCrisis.Show vbModal       'ITDTLW001
End Sub

'Add WC102007 - Add 2 menu items
Private Sub mnuGIMedSOS_Click()
cmbCom.Text = "GI"
cmbJob.Text = "Medical SOS Card"
cmbForR.Text = "Print"
PstrODBCname = "GIShare"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuGINonMedSOS_Click()
cmbCom.Text = "GI"
cmbJob.Text = "Non Medical SOS Card"
cmbForR.Text = "Print"
PstrODBCname = "GIShare"
Frame1.Visible = False   'EC01
End Sub
'End Add WC102007

'ITDTLW005 Start ***********************************************************************
Private Sub mnuPLMCU_Click()
cmbCom.Text = "MCU"
cmbJob.Text = "PinLetter"
cmbForR.Text = "Print"
PstrODBCname = "CIW"
Frame1.Visible = False
End Sub

Private Sub mnuPLMCUR_Click()
cmbCom.Text = "MCU"
cmbJob.Text = "PinLetter"
cmbForR.Text = "RePrint"
PstrODBCname = "CIW"
Frame1.Visible = False
End Sub
'ITDTLW005 End ***********************************************************************

Private Sub mnuSurvey_Click()
cmbCom.Text = "Life"
cmbJob.Text = "Survey"
cmbForR.Text = "Print"
PstrODBCname = "CIW"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuCoupon_Click()
cmbCom.Text = "Life"
cmbJob.Text = "Coupon"
cmbForR.Text = "Print"
PstrODBCname = "CIW"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuLifeMon_Click()
Frame1.Visible = True   'EC01
frmMonthEndLife.Show vbModal       'Ec01
End Sub

Private Sub mnuPLAetna_Click()
cmbCom.Text = "Aetna"
cmbJob.Text = "PinLetter"
cmbForR.Text = ""
Frame1.Visible = False   'EC01
pBolORSOFlag = False     'EC05
End Sub

Private Sub mnuPLDresd_Click()
cmbCom.Text = "Dresd"
cmbJob.Text = "PinLetter"
cmbForR.Text = ""
Frame1.Visible = False   'EC01
pBolORSOFlag = False     'EC05
End Sub

Private Sub mnuPLLife_Click()
cmbCom.Text = "Life"
cmbJob.Text = "PinLetter"
cmbForR.Text = "Print"
Frame1.Visible = False   'EC01
pBolORSOFlag = False     'EC05
End Sub

' Freelook date enhancement
Private Sub mnuPLLifeR_Click()
cmbCom.Text = "Life"
cmbJob.Text = "PinLetter"
cmbForR.Text = "RePrint"
Frame1.Visible = False
pBolORSOFlag = False
End Sub
' End

Private Sub mnuPLORSO_Click()
cmbCom.Text = "ORSO"
cmbJob.Text = "PinLetter"
cmbForR.Text = ""
Frame1.Visible = False   'EC01
pBolORSOFlag = True      'EC05
End Sub

Private Sub mnuPLSchrd_Click()
cmbCom.Text = "Schrd"
cmbJob.Text = "PinLetter"
cmbForR.Text = ""
Frame1.Visible = False   'EC01
pBolORSOFlag = False     'EC05
End Sub

Private Sub mnuReportAetna_Click()
cmbCom.Text = "Aetna"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuReportDresd_Click()
cmbCom.Text = "Dresd"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "MPFDresd"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuReportLife_Click()
cmbCom.Text = "Life"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "CIW"
Frame1.Visible = False   'EC01
End Sub

'// AC01 Added Begin - Menu //
'Card - First Print
Private Sub mnuCardMcDonald_Click()
cmbCom.Text = "McDonald"
cmbJob.Text = "Card"
cmbForR.Text = "Print"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub
'PIN Letter
Private Sub mnuPLMcDonald_Click()
cmbCom.Text = "McDonald"
cmbJob.Text = "PinLetter"
cmbForR.Text = ""
Frame1.Visible = False   'EC01
pBolORSOFlag = False     'EC05
End Sub
'Report
Private Sub mnuReportMcDonald_Click()
cmbCom.Text = "McDonald"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub
'Card - Re-Print
Private Sub mnuCardMcDonaldR_Click()
cmbCom.Text = "McDonald"
cmbJob.Text = "Card"
cmbForR.Text = "RePrint"
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub
'// AC01 Added End - Menu //

Private Sub mnuReportORSO_Click()
cmbCom.Text = "ORSO"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "MPFAetna"
Frame1.Visible = False   'EC01
End Sub

Private Sub mnuReportSchrd_Click()
cmbCom.Text = "Schrd"
cmbJob.Text = "Report"
cmbForR.Text = ""
PstrODBCname = "MPFSchrd"
Frame1.Visible = False   'EC01
End Sub

Public Sub PrintCard(ByVal company As String, ByVal ForR As String, _
                     ByVal strMsg As String, ByVal strLorM As String)
On Error GoTo errhandler
Dim prt As Printer
Dim number As Long
Dim CardNo As Long
Dim number_orso As Long
Dim CardNo_orso As Long
'** Codes for AY04 start **
Dim bolCanPrintCard As Boolean
'** Codes for AY04 end **

    If frmMenu.cmbCom = "ORSO" Then
        MsgBox "Please Change the blank A4 paper to the " & LABEL_PRINTER_NAME & "!!", vbExclamation
        CardNo_orso = 0
        number_orso = PrtCardLetter_ORSO(strLorM, ForR, "Dresd")
        CardNo_orso = CardNo_orso + number_orso
        number_orso = PrtCardLetter_ORSO(strLorM, ForR, "Schrd")
        CardNo_orso = CardNo_orso + number_orso
        MsgBox "This job is finsihed!!! " & CardNo_orso & " card letter(s) is/are printed.", vbOKOnly
    Else
        '** Change for AY04 start **
        If company = "Dresd" Then
            bolCanPrintCard = True
        Else
            Call GetPrinterName(company) 'ITDTLW003
            bolCanPrintCard = GetPrinter(prt, PRINTER_NAME)
        End If
        
        If bolCanPrintCard Then
            If company <> "Dresd" Then
                Set Printer = prt
            'If strLorM = "csw" And ForR = "F" Then
                'MsgBox "Please Change both the " & strMsg & " Print Card to the Card Printer " & Chr(13) & "and Customer Survey to the Xerox N40 Printer!!", vbExclamation
                'MsgBox "Please Change both the " & strMsg & " Print Card to the Card Printer!!", vbExclamation
            'Else
                'MsgBox "Please Change both the " & strMsg & " Print Card to the Card Printer!!", vbExclamation
                 MsgBox "Please ensure the " & strMsg & " cards are in the card printer!!", vbExclamation
            End If
            '** Change for AY04 end **
            'MsgBox "Print Job"
            number = Print_Job(strLorM, company, ForR, CardNo)
            If (company = "Dresd" Or company = "Schrd") And number > 0 Then
                Call PrtCardLetter
            End If
            If company = "Schrd" Then
                MsgBox "This job is finsihed!!! " & CardNo & " card(s) is/are printed. " & number & " welcome letter(s) is/are printed", vbOKOnly
            '** Change for AY04 start **
            ElseIf company = "Dresd" Then
                MsgBox "This job is finsihed!!! " & number & " welcome letter(s) is/are printed", vbOKOnly
            '** Change for AY04 end **
            ElseIf company = "GI" Then
               '**  SC042008  choi
              MsgBox "This job has been finished!!! " & number & " card(s) is/are printed." & GIMSG, vbOKOnly
            Else
                MsgBox "This job is finsihed!!! " & number & " card(s) is/are printed", vbOKOnly
            End If
        Else
            MsgBox "Card Printer NOT Found.", vbCritical
        End If
    End If
Exit Sub
errhandler:
    MsgBox "Error during printing cards for """ & company & """." & (Chr(13) & Chr(10)) & err.Source & ": " & err.Description, vbCritical
    Resume Next
End Sub

Public Sub PrintPL(ByVal strMsg As String, ByVal company As String, _
                   ByVal strLorM As String, ByVal strFR As String)
'Public Sub PrintPL(ByVal strMsg As String, ByVal company As String, _
'                   ByVal strLorM As String)
On Error GoTo errhandler
Dim strpath As String
    If company = "ORSO" Then
        MsgBox "Please Change the Aetna Pin Letter to the Printer!!", vbExclamation
        Open App.Path & "\AetnaPINLetter.txt" For Output As #1
        Print #1, "!$AetnaPINLetter"
        Print #1, "%%PROG: C:\ctrsys\AetnaPINLetter.JDT"
        Call Print_Letter_Job(strLorM, "Dresd")
        Call Print_Letter_Job(strLorM, "Schrd")
        Print #1, "%%EOJ"
        Close (1)
        strpath = "AetnaPINLetter.bat"
    Else
        MsgBox "Please Change the " & strMsg & " Pin Letter to the Printer!!", vbExclamation
        Call Print_Letter_Job(strLorM, company, strFR)
        strpath = company & "PINLetter.bat"
    End If
    Shell (strpath)
    MsgBox "This job is finsihed!!!", vbOKOnly
Exit Sub
errhandler:
    MsgBox "Error during printing PIN letters for """ & company & """." & (Chr(13) & Chr(10)) & err.Source & err.Description, vbCritical
    Resume Next
End Sub

Public Sub PrtReport(ByVal company As String)
Dim dbstr As String
Dim strCompany As String
'** Codes for AY02 start **
Dim rsSurvey As ADODB.Recordset
'** Codes for AY02 end **
Dim prt As Printer

    If frmMenu.cmbCom = "ORSO" Then
        frmPickDate.Show vbModal
        If bolFlag = True Then     'EC01
            Call Get_Connect("Aetna")
            Call KillRptTemp(company)
            Call Get_Connect("Dresd")
            Call ADDTempReportRec(Fromdate, ToDate, company)
            Call Get_Connect("Schrd")
            Call ADDTempReportRec(Fromdate, ToDate, company)
        End If
    Else
        frmPickDate.Show vbModal
        If bolFlag = True Then     'EC01
            Call Get_Connect(company)
            Call ADDTempReportRec(Fromdate, ToDate, company)
        End If
    End If

    If bolFlag = True Then     'EC01
        If GetPrinter(prt, REPORT_PRINTER_NAME) Then
            Set Printer = prt
            Call CRT.Reset
            CRT.PrinterName = prt.DeviceName
            CRT.PrinterDriver = prt.DriverName
            CRT.PrinterPort = prt.Port
            If cmbCom.Text = "ORSO" Then
                strCompany = "Aetna"
            Else
                strCompany = cmbCom.Text
            End If
            If get_db(dbstr, Trim(strCompany)) Then
                CRT.Connect = "DSN = " & PstrODBCname & "; " & dbstr
                CRT.Destination = crptToPrinter
                CRT.Formulas(0) = "TodayDate = " & """" & Format(ToDate, "dd/mm/yyyy") & """"
                If company <> "Life" Then
                    '// AC01 Changed - Begin //
                    '//AC01-Old//CRT.Formulas(1) = "Comp = " & """" & Company & """"
                    '//AC01-Old//CRT.ReportFileName = App.Path & "\Print Report.rpt"
                    If company = "McDonald" Then
                        CRT.Formulas(1) = "Comp = " & """" & company & """"
                        CRT.ReportFileName = App.Path & "\McDonald Print Report.rpt"
                    Else
                        CRT.Formulas(1) = "Comp = " & """" & company & """"
                        CRT.ReportFileName = App.Path & "\Print Report.rpt"
                    End If
                    '// AC01 Changed - End //
                Else
                   CRT.ReportFileName = App.Path & "\Print Report Life.rpt"
                End If
                CRT.Action = 1
                
                 '// BEGIN CW01 - Setting printer information
                Set Printer = prt
                Call CRT.Reset
                CRT.PrinterName = prt.DeviceName
                CRT.PrinterDriver = prt.DriverName
                CRT.PrinterPort = prt.Port
                CRT.Connect = "DSN = " & PstrODBCname & "; " & dbstr
                CRT.Destination = crptToPrinter
                '// END CW01
                                
                CRT.Formulas(0) = "TodayDate = " & """" & Format(ToDate, "dd/mm/yyyy") & """"
                If company <> "Life" Then
                    CRT.Formulas(1) = "Comp = " & """" & company & """"
                    CRT.ReportFileName = App.Path & "\RePrint Report.rpt"
                Else
                    CRT.ReportFileName = App.Path & "\RePrint Report Life.rpt"
                End If
                CRT.Action = 1
                '** Change for AY02 start **
                'MsgBox "This job is finsihed!!!", vbOKOnly
                MsgBox "Report for card and PIN letter printed!", vbOKOnly
            Else
                'MsgBox "Fail to connect database!!!", vbOKOnly
                MsgBox "Fail to connect database.  Card and PIN letter report is not printed.", vbOKOnly
                '** Change for AY02 end **
            End If
            
            '** Codes for AY02 start **
            If frmMenu.cmbCom.Text = "Life" Then  ' Print report for survey
                Set rsSurvey = GetSurveyReportData(Fromdate, ToDate)
                '** Codes for development, not for production **
                'If CreateFieldDefFile(rsSurvey, App.Path & "\temp.ttx", True) <> 0 Then
                '    MsgBox "Field definition successfully created"
                'Else
                '    MsgBox "Failed to create field definition file"
                'End If
                '** Codes for development end **
                If Not rsSurvey.EOF Then
                    CRT.Reset
                    CRT.PrinterName = prt.DeviceName
                    CRT.PrinterDriver = prt.DriverName
                    CRT.PrinterPort = prt.Port
                    CRT.Formulas(2) = "todaydate = " & """" & Format(ToDate, "dd/mm/yyyy") & """"
                    CRT.ReportFileName = App.Path & "\Print Survey Report Life.rpt"
                    CRT.SetTablePrivateData 0, 3, rsSurvey
                    'CRT.Destination = crptToFile  ' Print to file for debug
                    'CRT.PrintFileType = crptCrystal
                    CRT.Destination = crptToPrinter
                    CRT.Action = 1
                    MsgBox "Report for survey printed!", vbOKOnly
                Else
                    MsgBox "There is no record for survey report.  Survey report is not printed.", vbOKOnly
                End If
            End If
            '** Codes for AY02 end **
        Set db = Nothing
        Else
            MsgBox "Report Printer : " & REPORT_PRINTER_NAME & " NOT Found", vbCritical
        End If
    End If      'EC01
End Sub

Public Function get_db(ByRef dbstr As String, ByVal Pstrdb As String) As Boolean
On Error GoTo errhandler
Dim a As New Dblogon.Dbconnect
Dim str As String
Dim start As Integer
Dim stop_end As Integer
Dim i As Integer

' ES01 begin
'Call a.Connect(GetNTUserName, PROJECT, ChooseConnection(Pstrdb))    'EC01
If Pstrdb = "MCU" Then
    Call a.Connect("MAESUSER", "MAES", "CIW", "AGENCY")
Else
    Call a.Connect(GetNTUserName, PROJECT, ChooseConnection(Pstrdb))
End If
' ES01 end

'Call a.Connect("itdkcc", PROJECT, ChooseConnection(Pstrdb))
'Call a.Connect(USER, PROJECT, ChooseConnection(Pstrdb))            'EC01

str = a.GetDBString
For i = 1 To Len(str)
    If Mid(str, i, 3) = "UID" Then
        start = i
    End If
    If Mid(str, i, 3) = "APP" Then
        stop_end = i
    End If
Next i
dbstr = Mid(str, start, stop_end - start - 1)
get_db = True
Set a = Nothing
Exit Function
errhandler:
    Set a = Nothing
    get_db = False
End Function
Public Sub PrtCardLetter()
Dim dbstr As String
    If get_db(dbstr, Trim(cmbCom.Text)) Then
        CRT.Reset
        CRT.Connect = "DSN = " & PstrODBCname & "; " & dbstr
        CRT.Destination = crptToPrinter
        'CRT.Destination = crptToFile  ' Codes for debug in AY04
        'CRT.PrintFileType = crptCrystal  ' Codes for debug in AY04
        CRT.Formulas(0) = "TodayDate = " & """" & Format(Date, "dd/mm/yyyy") & """"
        If Trim(cmbForR.Text) = "Print" Then
            CRT.Formulas(1) = "ForR = ""P"""
        Else
            CRT.Formulas(1) = "ForR = ""R"""
        End If
        CRT.ReportFileName = App.Path & "\Print Card Letter " & Trim(cmbCom.Text) & " EE.rpt"
        CRT.Action = 1
        CRT.Destination = crptToPrinter
        'CRT.Destination = crptToFile  ' Codes for debug in AY04
        'CRT.PrintFileType = crptCrystal  ' Codes for debug in AY04
        CRT.Formulas(0) = "TodayDate = " & """" & Format(Date, "dd/mm/yyyy") & """"
        If Trim(cmbForR.Text) = "Print" Then
            CRT.Formulas(1) = "ForR = ""P"""
        Else
            CRT.Formulas(1) = "ForR = ""R"""
        End If
        CRT.ReportFileName = App.Path & "\Print Card Letter " & Trim(cmbCom.Text) & " ER.rpt"
        CRT.Action = 1
        '** Old codes before AY04 **
        'MsgBox "This printing job is finsihed!!!", vbOKOnly
    Else
        MsgBox "Fail to connect database!!!", vbOKOnly
    End If
End Sub
Public Function PrtCardLetter_ORSO(ByVal PstrCardType As String, ByVal PstrForR As String, ByVal Pstrdbc As String) As Integer
Dim dbstr As String
Dim rs As New ADODB.Recordset
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

    Call get_db(dbstr, Trim(Pstrdbc))
    Call Get_Connect(Pstrdbc)
    Set rs = Get_Print_Data(PstrCardType, PstrForR)
    PrtCardLetter_ORSO = rs.RecordCount
    While Not rs.EOF
        CRT.Reset
        CRT.Connect = "DSN = " & "Mpf" & Pstrdbc & "; " & dbstr
        CRT.Destination = crptToPrinter
        CRT.ReportFileName = App.Path & "\Cardletter.rpt"
        Call GetLetterContent(Trim(rs.Fields("mpfpcr_cust_flag")), _
                              Trim(rs.Fields("mpfpid_fund")), _
                              Trim(rs.Fields("mpfpid_emp")), _
                              Trim(rs.Fields("mpfpid_rpc")), _
                              rs.Fields("mpfpid_mbr_num"), _
                              strName1, strName2, strContPer, strBenCls, strMemNo, _
                              strAddr1, strAddr2, strAddr3, strAddr4, blnError)
        If rs.Fields("mpfpcr_cust_flag") = "R" Then
            CRT.Formulas(0) = "TYPE = " & """" & "ER" & """"
            CRT.Formulas(1) = "PrintDate = " & """" & Format(Date, "dd/mm/yyyy") & """"
            CRT.Formulas(2) = "AccountNo = " & """" & Trim(rs.Fields("mpfpid_fund")) & Trim(rs.Fields("mpfpid_emp")) & "-" & Trim(rs.Fields("mpfpid_rpc")) & """"
            CRT.Formulas(3) = "ContactName = " & """" & Trim(strContPer) & """"
            CRT.Formulas(4) = "CompanyName = " & """" & Trim(strName1 + " " + strName2) & """"
            CRT.Formulas(5) = "Address1 = " & """" & strAddr1 & """"
            CRT.Formulas(6) = "Address2 = " & """" & strAddr2 & """"
            CRT.Formulas(7) = "Address3 = " & """" & strAddr3 & """"
            CRT.Formulas(8) = "Address4 = " & """" & strAddr4 & """"
            CRT.Formulas(9) = "AccessNo = " & """" & Trim(rs.Fields("mpfpcr_cid")) & """"
        Else
            CRT.Formulas(0) = "TYPE = " & """" & "EE" & """"
            CRT.Formulas(1) = "PrintDate = " & """" & Format(Date, "dd/mm/yyyy") & """"
            CRT.Formulas(2) = "MemberNo = " & """" & Trim(rs.Fields("mpfpid_mbr_num")) & """"
            CRT.Formulas(3) = "MemberName = " & """" & Trim(strContPer) & """"
            CRT.Formulas(4) = "CompanyName = " & """" & Trim(strName1 + " " + strName2) & """"
            CRT.Formulas(5) = "Address1 = " & """" & strAddr1 & """"
            CRT.Formulas(6) = "Address2 = " & """" & strAddr2 & """"
            CRT.Formulas(7) = "Address3 = " & """" & strAddr3 & """"
            CRT.Formulas(8) = "Address4 = " & """" & strAddr4 & """"
            CRT.Formulas(9) = "AccessNo = " & """" & Trim(rs.Fields("mpfpcr_cid")) & """"
        End If
        CRT.Action = 1
        Call Upd_DB("mpf", rs.Fields("mpfpcr_cid"))
        rs.MoveNext
    Wend
End Function

' This function is not used now.  Observed in AY03
Public Sub PrtCustSurvey(ByVal strCustID As String, ByVal strName As String, ByVal strAddr1 As String, ByVal strAddr2 As String, ByVal strAddr3 As String, ByVal strAddr4 As String, ByVal strEmail As String, ByVal strTel As String)
Dim dbstr As String
    If get_db(dbstr, Trim(cmbCom.Text)) Then
        CRT.Reset
        CRT.Connect = "DSN = " & PstrODBCname & "; " & dbstr
        CRT.Destination = crptToPrinter
        CRT.Formulas(0) = "PHID = " & """" & Mid(Trim(strCustID), 1, 1) + " " + Mid(Trim(strCustID), 2, 1) + " " + Mid(Trim(strCustID), 3, 1) + " " + Mid(Trim(strCustID), 4, 1) + " " + Mid(Trim(strCustID), 5, 1) + " " + Mid(Trim(strCustID), 6, 1) + " " + Mid(Trim(strCustID), 7, 1) + " " + Mid(Trim(strCustID), 8, 1) & """"
        CRT.Formulas(1) = "PHName = " & """" & Trim(strName) & """"
        CRT.Formulas(2) = "Address1 = " & """" & Trim(strAddr1) & """"
        CRT.Formulas(3) = "Address2 = " & """" & Trim(strAddr2) & """"
        CRT.Formulas(4) = "Address3 = " & """" & Trim(strAddr3) & """"
        CRT.Formulas(5) = "Address4 = " & """" & Trim(strAddr4) & """"
        CRT.Formulas(6) = "Email = " & """" & Trim(strEmail) & """"
        CRT.Formulas(7) = "Telephone = " & """" & Trim(strTel) & """"
        CRT.ReportFileName = App.Path & "\CustSurvey.rpt"
        CRT.Action = 1
    Else
        MsgBox "Fail to connect database!!!", vbOKOnly
    End If
End Sub

Private Sub PrtSurvey(ByVal company As String)
Dim dbstr As String
Dim intSurvey  As Integer
Dim rs As New ADODB.Recordset
Dim strsql As String

'** Codes for AY01 start **
Dim rs2 As New ADODB.Recordset
Dim strsql1 As String
Dim strsql2 As String
Dim strsql3 As String
Dim strsql4 As String
Dim strsql5 As String
Dim intLastCusId As Long
Dim strLastPolId As String
'** Codes for AY01 end **
'** Codes for AY03 start **
Dim rs3 As New ADODB.Recordset
'** Codes for AY03 end **
Dim prt As Printer

    intSurvey = 0
    Call Get_Connect("Life")
    If cmbForR = "Print" Then
        If GetPrinter(prt, SURVEY_PRINTER_NAME) Then
            Set Printer = prt
    
            'strsql = "Select count(distinct cswppp_cid) From csw_prn_per_pol where cswppp_fstprn = 'N' and cswppp_agentcode Not Like '8%' And cswppp_create_date <= Convert(Datetime,Convert(Char(12),Getdate(),107)+ ' 00:00:00')"
'            strsql = "SELECT DISTINCT cswppp_cid INTO #Survey " & _
'                     "FROM csw_prn_per_pol a, csw_print_cardletter_report b, customer c, customeraddress d " & _
'                     "WHERE a.cswppp_pid = b.cswpcr_pid " & _
'                     "AND a.cswppp_cid = b.cswpcr_cid " & _
'                     "AND a.cswppp_cid = c.customerid " & _
'                     "AND a.cswppp_cid = d.customerid " & _
'                     "AND a.cswppp_fstprn = 'N' " & _
'                     "AND a.cswppp_agentcode NOT LIKE '8%' " & _
'                     "AND b.cswpcr_prtidxcard = 'P' " & _
'                     "AND b.cswpcr_lstprtdatecard >= '" & Format(Now, "YYYY-MM-DD 00:00:00") & "' " & _
'                     "AND b.cswpcr_lstprtdatecard <= '" & Format(Now, "YYYY-MM-DD 23:59:59") & "' " & _
'                     "AND (d.addresstypecode = 'R' or d.addresstypecode = 'I') " & _
'                     "AND d.BadAddress = 'N' "
            ' **Codes Before AY01**
            'strsql = "SELECT DISTINCT cswppp_cid INTO #Survey " & _
                     "FROM csw_prn_per_pol a, csw_print_cardletter_report b, customer c, customeraddress d " & _
                     "WHERE a.cswppp_pid = b.cswpcr_pid " & _
                     "AND a.cswppp_cid = b.cswpcr_cid " & _
                     "AND a.cswppp_cid = c.customerid " & _
                     "AND a.cswppp_cid = d.customerid " & _
                     "AND a.cswppp_fstprn = 'N' " & _
                     "AND a.cswppp_agentcode NOT LIKE '8%' " & _
                     "AND b.cswpcr_prtidxcard = 'P' " & _
                     "AND b.cswpcr_lstprtdatecard <= '" & Format(Now, "YYYY-MM-DD 23:59:59") & "' " & _
                     "AND (d.addresstypecode = 'R' or d.addresstypecode = 'I' " & _
                     "or d.addresstypecode = 'B' or d.addresstypecode = 'J') " & _
                     "AND d.BadAddress = 'N' "
            'Set rs = db.ExecuteStatement(strsql)
            'strsql = "SELECT COUNT(*) FROM #Survey "
            'Set rs = db.ExecuteStatement(strsql)
            'If Not rs.EOF And Not IsNull(rs(0)) Then
            '    intSurvey = rs(0)
            'Else
            '    intSurvey = 0
            'End If
            ' **Codes before AY01 End**
            
            '** Codes for AY01 start **
            '** Codes before AY03 start **
            'strsql1 = "(SELECT DISTINCT " & _
                      "csw_prn_per_pol.cswppp_cid, csw_prn_per_pol.cswppp_pid, csw_prn_per_pol.cswppp_agentcode, csw_prn_per_pol.cswppp_fstprn, " & _
                      "Customer.CustomerID, Customer.FirstName, Customer.NameSuffix, Customer.EmailAddr, " & _
                      "Customer.ChiFstNm, Customer.ChiLstNm, Customer.CoName, Customer.CoCName, Customer.Gender, " & _
                      "CustomerAddress.AddressTypeCode, CustomerAddress.AddressLine1, CustomerAddress.AddressLine2, CustomerAddress.AddressLine3, CustomerAddress.AddressCity, CustomerAddress.PhoneNumber1, CustomerAddress.BadAddress, " & _
                      "AddressOrder.CodeOrder, Customer.UseChiInd, csw_policy_uw.cswpuw_infor_date " & _
                      "From " & _
                      "(((csw_prn_per_pol INNER JOIN CustomerAddress ON " & _
                      "csw_prn_per_pol.cswppp_cid = CustomerAddress.CustomerID) " & _
                      "INNER JOIN csw_policy_uw ON " & _
                      "csw_prn_per_pol.cswppp_pid = csw_policy_uw.cswpuw_poli_id) " & _
                      "INNER JOIN Customer ON " & _
                      "csw_prn_per_pol.cswppp_cid = Customer.CustomerID) " & _
                      "INNER JOIN ( " & _
                      "SELECT 'I' AS Type, 1 AS CodeOrder UNION " & _
                      "SELECT 'J' AS Type, 2 AS CodeOrder UNION " & _
                      "SELECT 'R' AS Type, 3 AS CodeOrder UNION " & _
                      "SELECT 'B' AS Type, 4 AS CodeOrder) AddressOrder ON AddressOrder.type= CustomerAddress.AddressTypeCode "
            'strsql2 = "Where " & _
                      "(CustomerAddress.AddressTypeCode = 'R' OR " & _
                      "CustomerAddress.AddressTypeCode = 'I' OR " & _
                      "CustomerAddress.AddressTypeCode = 'B' OR " & _
                      "CustomerAddress.AddressTypeCode = 'J') AND " & _
                      "CustomerAddress.BadAddress = 'N' AND " & _
                      "csw_prn_per_pol.cswppp_agentcode NOT LIKE '8%' AND "
            'strsql3 = "csw_prn_per_pol.cswppp_fstprn = 'N' AND " & _
                      "DATEDIFF(day,csw_policy_uw.cswpuw_infor_date,'" & Format(Now, "YYYY-MM-DD 23:59:59") & "') > 0 AND " & _
                      "Customer.UseChiInd = 'Y') " & _
                      "Union " & _
                      "(SELECT DISTINCT " & _
                      "csw_prn_per_pol.cswppp_cid, csw_prn_per_pol.cswppp_pid, csw_prn_per_pol.cswppp_agentcode, csw_prn_per_pol.cswppp_fstprn, " & _
                      "Customer.CustomerID, Customer.FirstName, Customer.NameSuffix, Customer.EmailAddr, " & _
                      "Customer.ChiFstNm, Customer.ChiLstNm, Customer.CoName, Customer.CoCName, Customer.Gender, " & _
                      "CustomerAddress.AddressTypeCode, CustomerAddress.AddressLine1, CustomerAddress.AddressLine2, CustomerAddress.AddressLine3, CustomerAddress.AddressCity, CustomerAddress.PhoneNumber1, CustomerAddress.BadAddress, " & _
                      "AddressOrder.CodeOrder, Customer.UseChiInd, csw_policy_uw.cswpuw_infor_date "
            'strsql4 = "From " & _
                      "(((csw_prn_per_pol INNER JOIN CustomerAddress ON " & _
                      "csw_prn_per_pol.cswppp_cid = CustomerAddress.CustomerID) " & _
                      "INNER JOIN csw_policy_uw ON " & _
                      "csw_prn_per_pol.cswppp_pid = csw_policy_uw.cswpuw_poli_id) " & _
                      "INNER JOIN Customer ON " & _
                      "csw_prn_per_pol.cswppp_cid = Customer.CustomerID) " & _
                      "INNER JOIN ( " & _
                      "SELECT 'R' AS Type, 1 AS CodeOrder UNION " & _
                      "SELECT 'B' AS Type, 2 AS CodeOrder UNION " & _
                      "SELECT 'I' AS Type, 3 AS CodeOrder UNION " & _
                      "SELECT 'J' AS Type, 4 AS CodeOrder) AddressOrder ON AddressOrder.type= CustomerAddress.AddressTypeCode " & _
                      "Where " & _
                      "(CustomerAddress.AddressTypeCode = 'R' OR "
            'strsql5 = "CustomerAddress.AddressTypeCode = 'I' OR " & _
                      "CustomerAddress.AddressTypeCode = 'B' OR " & _
                      "CustomerAddress.AddressTypeCode = 'J') AND " & _
                      "CustomerAddress.BadAddress = 'N' AND " & _
                      "csw_prn_per_pol.cswppp_agentcode NOT LIKE '8%' AND " & _
                      "csw_prn_per_pol.cswppp_fstprn = 'N' AND " & _
                      "DATEDIFF(day,csw_policy_uw.cswpuw_infor_date,'" & Format(Now, "YYYY-MM-DD 23:59:59") & "') > 0 AND " & _
                      "(Customer.UseChiInd <> 'Y' OR Customer.UseChiInd is null)) " & _
                      "Order By " & _
                      "csw_policy_uw.cswpuw_infor_date ASC, " & _
                      "csw_prn_per_pol.cswppp_pid ASC, " & _
                      "csw_prn_per_pol.cswppp_cid ASC, " & _
                      "AddressOrder.CodeOrder ASC"
            'strsql = strsql1 & strsql2 & strsql3 & strsql4 & strsql5
            '** Codes before AY03 end **
            '** Codes for AY03 start **
            strsql = "select pp.cswppp_cid, pp.cswppp_pid, cu.emailaddr, pu.cswpuw_infor_date, cu.usechiind, " & _
                     "case when cu.gender='C' and cu.usechiind='Y' then rtrim(cu.cocname) " & _
                     "when cu.gender='C' and (cu.usechiind<>'Y' or cu.usechiind is null) then rtrim(cu.coname) " & _
                     "when cu.gender<>'C' and cu.usechiind='Y' then rtrim(cu.chilstnm) + rtrim(cu.chifstnm) " & _
                     "else rtrim(cu.namesuffix) + ' ' + rtrim(cu.firstname) end as cusname, " & _
                     "case when ca.gender='C' then rtrim(ca.coname) " & _
                     "else rtrim(ca.namesuffix) + ' ' + rtrim(ca.firstname) end as agname, " & _
                     "ac.locationcode into #tempsurvey " & _
                     "from csw_prn_per_pol pp, customer cu, csw_policy_uw pu, csw_poli_rel pr, customer ca, agentcodes ac " & _
                     "Where pp.cswppp_cid = cu.customerid " & _
                     "and pp.cswppp_pid = pu.cswpuw_poli_id " & _
                     "and pp.cswppp_pid = pr.policyaccountid " & _
                     "and pr.policyrelatecode='SA' " & _
                     "and pr.customerid = ca.customerid " & _
                     "and ca.agentcode = ac.agentcode " & _
                     "and pp.cswppp_agentcode not like '8%' " & _
                     "and pp.cswppp_fstprn = 'N' " & _
                     "and datediff(day,pu.cswpuw_infor_date,getdate())>0"
            Set rs3 = db.ExecuteStatement(strsql)
            
' ITSR0606140 - Use policy address for survey printing
''            strsql1 = "(select #tempsurvey.cswppp_cid, #tempsurvey.cswppp_pid, #tempsurvey.cswpuw_infor_date, #tempsurvey.usechiind, #tempsurvey.cusname, #tempsurvey.emailaddr, " & _
''                      "customeraddress.addressline1, customeraddress.addressline2, customeraddress.addressline3, customeraddress.addresscity, " & _
''                      "customeraddress.phonenumber1, customeraddress.addresstypecode , addressorder.codeorder, " & _
''                      "#tempsurvey.agname, #tempsurvey.locationcode " & _
''                      "from ((#tempsurvey " & _
''                      "inner join customeraddress on #tempsurvey.cswppp_cid=customeraddress.customerid) " & _
''                      "inner join (select 'I' as type, 1 as codeorder union " & _
''                      "select 'J' as type, 2 as codeorder union " & _
''                      "select 'R' as type, 3 as codeorder union " & _
''                      "select 'B' as type, 4 as codeorder) addressorder on addressorder.type= customeraddress.addresstypecode) " & _
''                      "where customeraddress.addresstypecode in ('R','B','I','J') " & _
''                      "and customeraddress.badaddress='N' " & _
''                      "and #tempsurvey.usechiind='Y') "
''            strsql2 = "Union " & _
''                      "(select #tempsurvey.cswppp_cid, #tempsurvey.cswppp_pid, #tempsurvey.cswpuw_infor_date, #tempsurvey.usechiind, #tempsurvey.cusname, #tempsurvey.emailaddr, " & _
''                      "customeraddress.addressline1, customeraddress.addressline2, customeraddress.addressline3, customeraddress.addresscity, " & _
''                      "customeraddress.phonenumber1, customeraddress.addresstypecode , addressorder.codeorder, " & _
''                      "#tempsurvey.agname, #tempsurvey.locationcode " & _
''                      "from ((#tempsurvey " & _
''                      "inner join customeraddress on #tempsurvey.cswppp_cid=customeraddress.customerid) " & _
''                      "inner join (select 'R' as type, 1 as codeorder union " & _
''                      "select 'B' as type, 2 as codeorder union " & _
''                      "select 'I' as type, 3 as codeorder union " & _
''                      "select 'J' as type, 4 as codeorder) addressorder on addressorder.type= customeraddress.addresstypecode) " & _
''                      "where customeraddress.addresstypecode in ('R','B','I','J') " & _
''                      "and customeraddress.badaddress='N' " & _
''                      "and (#tempsurvey.usechiind<>'Y' or #tempsurvey.usechiind is null)) " & _
''                      "order by #tempsurvey.cswppp_pid, #tempsurvey.cswppp_cid, addressorder.codeorder, #tempsurvey.agname "
''
''            strsql = strsql1 + strsql2
            
            strsql = "select #tempsurvey.cswppp_cid, #tempsurvey.cswppp_pid, #tempsurvey.cswpuw_infor_date, #tempsurvey.usechiind, #tempsurvey.cusname, #tempsurvey.emailaddr, " & _
                " pa.cswpad_add1 as addressline1, pa.cswpad_add2 as addressline2, pa.cswpad_add3 as addressline3, pa.cswpad_city as addresscity, " & _
                " pa.cswpad_tel1 as phonenumber1, pa.cswpad_addr_type as addresstypecode , 1 as codeorder, " & _
                " #tempsurvey.agname, #tempsurvey.locationcode " & _
                " from #tempsurvey, csw_policy_address pa " & _
                " where #tempsurvey.cswppp_pid=pa.cswpad_poli_id " & _
                " order by #tempsurvey.cswppp_pid, #tempsurvey.cswppp_cid, #tempsurvey.agname "
' End of change

            Set rs = db.ExecuteStatement(strsql)
            '** Codes for development, not for production **
            'If CreateFieldDefFile(rs, App.Path & "\CustSurvey.ttx", True) <> 0 Then
            '    MsgBox "Field definition successfully created"
            'Else
            '    MsgBox "Failed to create field definition file"
            'End If
            '** Codes for development end **
            If Not rs.EOF Then
                    
                CRT.Reset
                CRT.PrinterName = prt.DeviceName
                CRT.PrinterDriver = prt.DriverName
                CRT.PrinterPort = prt.Port
                CRT.ReportFileName = App.Path & "\CustSurvey.rpt"
                CRT.SetTablePrivateData 0, 3, rs
                'CRT.Destination = crptToFile
                'CRT.PrintFileType = crptCrystal
                CRT.Destination = crptToPrinter
                'CRT.Destination = crptToWindow
                CRT.Action = 1
    
                '** Codes before AY01 **
                ''strsql = "Update csw_prn_per_pol set cswppp_fstprn = 'Y', cswppp_prn_date = GetDate(), cswppp_update_date = GetDate(), cswppp_update_user = '" & Trim(GetNTUserName) & "' where cswppp_fstprn = 'N' and cswppp_agentcode Not Like '8%' and cswppp_create_date <= Convert(Datetime,Convert(Char(12),Getdate(),107)+ ' 00:00:00')"
                'strsql = "UPDATE csw_prn_per_pol " & _
                         "SET cswppp_fstprn = 'Y', " & _
                         "cswppp_prn_date = GetDate(), " & _
                         "cswppp_update_date = GetDate(), " & _
                         "cswppp_update_user = '" & Trim(GetNTUserName) & "' " & _
                         "WHERE cswppp_fstprn = 'N' " & _
                         "AND cswppp_agentcode NOT LIKE '8%' " & _
                         "AND cswppp_cid IN (SELECT * FROM #Survey) "
                'Set rs = db.ExecuteStatement(strsql)
                '** Codes Before AY01 end **
                '** Codes for AY01 **
                rs.MoveFirst
                intLastCusId = -1
                strLastPolId = ""
                intSurvey = 0
                While Not rs.EOF
                    If (strLastPolId <> rs.Fields(1).Value Or intLastCusId <> rs.Fields(0).Value) Then
                        strsql = "UPDATE csw_prn_per_pol " & _
                                 "SET cswppp_fstprn = 'Y', " & _
                                 "cswppp_prn_date = GetDate(), " & _
                                 "cswppp_update_date = GetDate(), " & _
                                 "cswppp_update_user = '" & Trim(GetNTUserName) & "' " & _
                                 "WHERE cswppp_fstprn = 'N' " & _
                                 "AND cswppp_agentcode NOT LIKE '8%' " & _
                                 "AND cswppp_cid=" & rs.Fields(0).Value & " " & _
                                 "AND cswppp_pid='" & rs.Fields(1).Value & "'"
                        Set rs2 = db.ExecuteStatement(strsql)
                        intLastCusId = rs.Fields(0).Value
                        strLastPolId = rs.Fields(1).Value
                        intSurvey = intSurvey + 1
                    End If
                    rs.MoveNext
                Wend
                '** Codes for AY01 end **
            End If
            '** Codes for AY03 start **
            ' Assume all records have errors in their address if they are not selected and marked as printed
            ' after inner join with customeraddress table
            strsql = "update csw_prn_per_pol set cswppp_fstprn='A', " & _
                     "cswppp_prn_date = GetDate(), " & _
                     "cswppp_update_date = GetDate(), " & _
                     "cswppp_update_user = '" & Trim(GetNTUserName) & "' " & _
                     "from csw_prn_per_pol,#tempsurvey " & _
                     "where csw_prn_per_pol.cswppp_cid=#tempsurvey.cswppp_cid " & _
                     "and csw_prn_per_pol.cswppp_fstprn='N' "
            Set rs = db.ExecuteStatement(strsql)
            strsql = "drop table #tempsurvey"
            Set rs = db.ExecuteStatement(strsql)
            '** Codes for AY03 end **
            '** Codes before AY01 **
            'strsql = "Drop Table #Survey "
            'Set rs = db.ExecuteStatement(strsql)
            '** Codes before AY01 end **
        Else
            MsgBox "Survey Printer : " & SURVEY_PRINTER_NAME & " NOT Found", vbCritical
        End If
    End If
    MsgBox "This printing job is finsihed! " & intSurvey & " Survey(s) is/are printed!!!", vbOKOnly

End Sub

Private Sub PrtCoupon(ByVal company As String)
Dim dbstr As String
Dim rs As New ADODB.Recordset
Dim intCoupon As Integer
'** Codes for AY03 start **
Dim strsql As String
'** Codes for AY03 end **
Dim prt As Printer
    
    'If get_db(dbstr, Trim(cmbCom.Text)) Then
        'Call Get_Connect("ICR")
    If GetPrinter(prt, COUPON_PRINTER_NAME) Then
        Set Printer = prt
        
        intCoupon = 0
        Set rs = GetCouponData
        While Not rs.EOF
            If rs(0) <> "Nothing" Then
                CRT.Reset
                CRT.PrinterName = prt.DeviceName
                CRT.PrinterDriver = prt.DriverName
                CRT.PrinterPort = prt.Port
                CRT.Connect = "DSN = " & PstrODBCname & "; " & dbstr
                CRT.Destination = crptToPrinter
                'CRT.Destination = crptToWindow
                'CRT.PrintFileType = crptCrystal  ' Print to file for debug only
                'CRT.Destination = crptToFile
                CRT.Formulas(0) = "Name = " & """" & Trim(rs(1)) & """"
                CRT.Formulas(1) = "Address1 = " & """" & Trim(rs(2)) & """"
                CRT.Formulas(2) = "Address2 = " & """" & Trim(rs(3)) & """"
                CRT.Formulas(3) = "Address3 = " & """" & Trim(rs(4)) & """"
                CRT.Formulas(4) = "Address4 = " & """" & Trim(rs(5)) & """"
                CRT.Formulas(5) = "CDate = " & """" & Format(Date, "YYYYMMDD") & """"
                '** Change for AY03 start **
                If DatePart("d", Date) = 1 Then
                    CRT.Formulas(6) = "CouponDate = " & """" & Format(DateAdd("d", -1, DateAdd("m", 6, Date)), "MMMM DD, YYYY") & """"
                Else
                    CRT.Formulas(6) = "CouponDate = " & """" & Format(DateAdd("m", 6, DateAdd("d", -1, Date)), "MMMM DD, YYYY") & """"
                End If
                CRT.Formulas(7) = "CouponName = " & """" & Trim(rs(8)) & """"
                '** Change for AY03 end **
                CRT.ReportFileName = App.Path & "\CustCoupon.rpt"
                CRT.Action = 1
                intCoupon = intCoupon + 1
            End If
            
            '** Codes for AY03 start **
            Call Get_Connect("ICR")
            strsql = "Update icr..icr_cust_survey Set icrcsv_cup_prt = 'Y' Where icrcsv_holder_id='" & rs(0) & "'"
            db.ExecuteStatement (strsql)
            '** Codes for AY03 end **
            
            rs.MoveNext
        Wend
        
        MsgBox "This printing job is finsihed! " & intCoupon & " Coupon(s) is/are printed!!!", vbOKOnly
    Else
        MsgBox "Coupon Printer : " & COUPON_PRINTER_NAME & " NOT Found", vbCritical
    End If
    
    'Else
    '    MsgBox "Fail to connect database!!!", vbOKOnly
    'End If
End Sub


