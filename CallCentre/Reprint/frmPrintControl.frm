VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Begin VB.Form frmPrintControl 
   Caption         =   "Card/Letter Print System"
   ClientHeight    =   6105
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3795
   LinkTopic       =   "Form1"
   ScaleHeight     =   6105
   ScaleWidth      =   3795
   StartUpPosition =   2  'CenterScreen
   Begin VB.TextBox txtStatus 
      Enabled         =   0   'False
      Height          =   375
      Left            =   1680
      Locked          =   -1  'True
      TabIndex        =   15
      Top             =   3720
      Width           =   1695
   End
   Begin VB.TextBox mebCustomerID 
      Height          =   375
      Left            =   1080
      TabIndex        =   1
      Top             =   120
      Width           =   2415
   End
   Begin VB.ComboBox cboSubType 
      Height          =   315
      Left            =   2040
      TabIndex        =   4
      Top             =   600
      Width           =   1455
   End
   Begin VB.ComboBox cboSelection 
      Height          =   315
      Left            =   1080
      TabIndex        =   3
      Top             =   600
      Width           =   855
   End
   Begin VB.CommandButton cmdBrowse 
      Caption         =   "&Browse"
      Height          =   375
      Left            =   120
      TabIndex        =   5
      Top             =   1080
      Width           =   1215
   End
   Begin VB.CommandButton cmdExit 
      Caption         =   "&Exit"
      Height          =   375
      Left            =   2400
      TabIndex        =   21
      Top             =   5400
      Width           =   1215
   End
   Begin VB.CommandButton cmdPrint 
      Caption         =   "&Print"
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   20
      Top             =   5400
      Width           =   1215
   End
   Begin VB.ComboBox cboPrintLetter 
      Height          =   315
      Left            =   1680
      TabIndex        =   19
      Top             =   4680
      Width           =   1695
   End
   Begin VB.ComboBox cboPrintCard 
      Height          =   315
      Left            =   1680
      TabIndex        =   17
      Top             =   4200
      Width           =   1695
   End
   Begin VB.Frame fraDetail 
      Height          =   3735
      Left            =   120
      TabIndex        =   23
      Top             =   1440
      Width           =   3495
      Begin VB.TextBox mebLastUpdateDate 
         Height          =   405
         Left            =   1560
         TabIndex        =   9
         Top             =   840
         Width           =   1695
      End
      Begin VB.TextBox mebFirstPrintDate 
         Height          =   375
         Left            =   1560
         TabIndex        =   7
         Top             =   360
         Width           =   1695
      End
      Begin VB.TextBox txtPrtCountLetter 
         Height          =   375
         Left            =   1560
         Locked          =   -1  'True
         TabIndex        =   13
         Top             =   1800
         Width           =   1695
      End
      Begin VB.TextBox txtPrtCountCard 
         Height          =   375
         Left            =   1560
         Locked          =   -1  'True
         TabIndex        =   11
         Top             =   1320
         Width           =   1695
      End
      Begin VB.Label Label2 
         Caption         =   "Status"
         Enabled         =   0   'False
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   2400
         Width           =   735
      End
      Begin VB.Label lblLetterPrintCount 
         Caption         =   "Letter Print Count"
         Height          =   375
         Left            =   120
         TabIndex        =   12
         Top             =   1920
         Width           =   1455
      End
      Begin VB.Label lblPrintLetter 
         Caption         =   "Print Letter ?"
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   3360
         Width           =   975
      End
      Begin VB.Label lblPrintCard 
         Caption         =   "Print Card ?"
         Height          =   255
         Left            =   120
         TabIndex        =   16
         Top             =   2880
         Width           =   975
      End
      Begin VB.Label lblCardPrintCount 
         Caption         =   "Card Print Count"
         Height          =   375
         Left            =   120
         TabIndex        =   10
         Top             =   1440
         Width           =   1575
      End
      Begin VB.Label lblLastUpdateDate 
         Caption         =   "Last Update Date"
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   960
         Width           =   1335
      End
      Begin VB.Label lblFirstPrint 
         Caption         =   "First Print Date"
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   480
         Width           =   1215
      End
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   255
      Left            =   0
      TabIndex        =   22
      Top             =   5850
      Width           =   3795
      _ExtentX        =   6694
      _ExtentY        =   450
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   3
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   2117
            MinWidth        =   2117
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   6
            Object.Width           =   2117
            MinWidth        =   2117
            TextSave        =   "9/18/02"
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   5
            Object.Width           =   1764
            MinWidth        =   1764
            TextSave        =   "7:15 PM"
         EndProperty
      EndProperty
   End
   Begin VB.Label Label1 
      Caption         =   "LIFE/MPF ?"
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   600
      Width           =   1095
   End
   Begin VB.Label lblCustomerID 
      Caption         =   "CustomerID"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   975
   End
End
Attribute VB_Name = "frmPrintControl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************
'Amend By : Eammon Chen - EC01         Date : 04June2002
'Amendment: Add in new function for display the Status in
'           sec_user_group
'
'**********************************************************

Dim dbsec As New Dblogon.Dbconnect
Dim strLINE As String

Private Sub cboPrintCard_click()
If cboPrintCard.Text = "YES" Then
    cboPrintLetter.Text = "YES"
    cboPrintLetter.Locked = True
Else
    cboPrintLetter.Locked = False
End If
End Sub
Private Sub cboSelection_Click()
If cboSelection.Text = "MPF" Then
    cboSubType.Visible = True
    cboSubType.Text = "Please Select"
Else
    cboSubType.Visible = False
End If
End Sub

Private Sub cmdPrint_Click()
On Error GoTo errorHandler:
Dim strsql As String
Dim strsql1 As String
Dim strsql2 As String
Dim strsql3 As String
Dim cardidx As String
Dim letteridx As String
Dim EorR As String
Dim rs As New ADODB.Recordset
Dim rs1 As New ADODB.Recordset
Dim UserName As String
'UserName = GetNTUserName()
UserName = strUserID
    If cboPrintLetter.Text = "YES" Or cboPrintCard = "YES" Then
        strsql = "Update " & strLINE & "_print_control set "
        If cboPrintCard.Text = "YES" Then
            strsql = strsql & strLINE & "prc_card_ind = 'Y', "
            cardidx = "R"
            cardprted = "N"
        End If
        If cboPrintCard.Text = "NO" Then
            strsql = strsql & strLINE & "prc_card_ind = 'N', "
            cardidx = "N"
        End If
        If cboPrintLetter.Text = "YES" Then
            strsql = strsql & strLINE & "prc_letter_ind = 'Y', "
            letteridx = "R"
        End If
        If cboPrintLetter.Text = "NO" Then
            strsql = strsql & strLINE & "prc_letter_ind = 'N', "
            letteridx = "N"
        End If
        strsql = Left(Trim(strsql), Len(Trim(strsql)) - 1)
        strsql = strsql & " where " & strLINE & "prc_cust_id = " & mebCustomerID
        'Life
        If strLINE = LIFE Then
            strsql2 = "select cswpcr_prtedletter ,cswpcr_prtedcard from csw_print_cardletter_report where " & _
                     "(cswpcr_prtidxletter = 'R' or cswpcr_prtidxcard = 'R') and " & _
                     "cswpcr_cid = " & mebCustomerID & " and " & _
                     "(cswpcr_prtedletter = 'N' or cswpcr_prtedcard = 'N')"
            Set rs = dbsec.ExecuteStatement(strsql2)
            If rs.RecordCount = 0 Then
                strsql1 = "INSERT csw_print_cardletter_report (cswpcr_cid,cswpcr_crtdate,cswpcr_prtidxletter,cswpcr_prtidxcard,"
                If cboPrintLetter.Text = "YES" And cboPrintCard = "YES" Then
                    strsql1 = strsql1 & "cswpcr_prtedletter,cswpcr_prtedcard,cswpcr_user_letter, cswpcr_user_card"
                    strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",getdate(),'" & letteridx & "','" & cardidx & "','"
                    strsql1 = strsql1 & "N','N','" & UserName & "','" & UserName
                Else
                    If cboPrintCard = "YES" Then
                        strsql1 = strsql1 & "cswpcr_prtedcard, cswpcr_user_card"
                        strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",getdate(),'" & letteridx & "','" & cardidx & "','"
                        strsql1 = strsql1 & "N','" & UserName
                    End If
                    If cboPrintLetter.Text = "YES" Then
                        strsql1 = strsql1 & "cswpcr_prtedletter, cswpcr_user_letter"
                        strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",getdate(),'" & letteridx & "','" & cardidx & "','"
                        strsql1 = strsql1 & "N','" & UserName
                    End If
                End If
                strsql1 = strsql1 & "')"
            Else
                If rs.Fields(0).Value = "N" Then
                    If cboPrintCard.Text = "YES" Then
                        strsql1 = "update csw_print_cardletter_report " & _
                                  "set cswpcr_prtedcard = 'N', cswpcr_prtidxcard = 'R', cswpcr_user_card ='" & UserName & "' where " & _
                                  "cswpcr_prtidxletter = 'R' and " & _
                                  "cswpcr_cid = " & mebCustomerID & " and cswpcr_prtedletter = 'N'"
                    End If
                End If
                If rs.Fields(1).Value = "N" Then
                    If cboPrintLetter.Text = "YES" Then
                        strsql1 = "update csw_print_cardletter_report " & _
                                  "set cswpcr_prtedletter = 'N', cswpcr_prtidxletter = 'R', cswpcr_user_card ='" & UserName & "' where " & _
                                  "cswpcr_prtidxcard = 'R' and " & _
                                  "cswpcr_cid = " & mebCustomerID & " and cswpcr_prtedcard = 'N'"
                    End If
                End If
            End If
        Else
        'MPF
            EorR = GetEorR(mebCustomerID, dbsec)
            strsql2 = "select mpfpcr_prtedletter ,mpfpcr_prtedcard from mpf_print_cardletter_report where " & _
                     "(mpfpcr_prtidxletter = 'R' or mpfpcr_prtidxcard = 'R') and " & _
                     "mpfpcr_cid = " & mebCustomerID & " and " & _
                     "(mpfpcr_prtedletter = 'N' or mpfpcr_prtedcard = 'N')"
            Set rs = dbsec.ExecuteStatement(strsql2)
            If rs.RecordCount = 0 Then
                strsql1 = "INSERT mpf_print_cardletter_report (mpfpcr_cid,mpfpcr_cust_flag,mpfpcr_crtdate,mpfpcr_prtidxletter,mpfpcr_prtidxcard,"
                If cboPrintLetter.Text = "YES" And cboPrintCard = "YES" Then
                    strsql1 = strsql1 & "mpfpcr_prtedletter,mpfpcr_prtedcard,mpfpcr_user_letter, mpfpcr_user_card"
                    strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",'" & EorR & "',getdate(),'" & letteridx & "','" & cardidx & "','"
                    strsql1 = strsql1 & "N','N','" & UserName & "','" & UserName
                Else
                    If cboPrintCard = "YES" Then
                        strsql1 = strsql1 & "mpfpcr_prtedcard, mpfpcr_user_card"
                        strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",'" & EorR & "',getdate(),'" & letteridx & "','" & cardidx & "','"
                        strsql1 = strsql1 & "N','" & UserName
                    End If
                    If cboPrintLetter.Text = "YES" Then
                        strsql1 = strsql1 & "mpfpcr_prtedletter, mpfpcr_user_letter"
                        strsql1 = strsql1 & ") values(" & mebCustomerID.Text & ",'" & EorR & "',getdate(),'" & letteridx & "','" & cardidx & "','"
                        strsql1 = strsql1 & "N','" & UserName
                    End If
                End If
                strsql1 = strsql1 & "')"
            Else
                If rs.Fields(0).Value = "N" Then
                    If cboPrintCard.Text = "YES" Then
                        strsql1 = "update mpf_print_cardletter_report " & _
                                  "set mpfpcr_prtedcard = 'N', mpfpcr_prtidxcard = 'R', mpfpcr_user_card ='" & UserName & "' where " & _
                                  "mpfpcr_prtidxletter = 'R' and " & _
                                  "mpfpcr_cid = " & mebCustomerID & " and mpfpcr_prtedletter = 'N'"
                    End If
                End If
                If rs.Fields(1).Value = "N" Then
                    If cboPrintLetter.Text = "YES" Then
                        strsql1 = "update mpf_print_cardletter_report " & _
                                  "set mpfpcr_prtedletter = 'N', mpfpcr_prtidxletter = 'R', mpfpcr_user_card ='" & UserName & "' where " & _
                                  "mpfpcr_prtidxcard = 'R' and " & _
                                  "mpfpcr_cid = " & mebCustomerID & " and mpfpcr_prtedcard = 'N'"
                    End If
                End If
            End If
        End If
        Call dbsec.ExecuteStatement(strsql)
        Call dbsec.ExecuteStatement(strsql1)
    Else
        MsgBox "No Request will be submitted!!", vbExclamation
    End If
    Call Initialization
    Set rs = Nothing
    Set rs1 = Nothing
Exit Sub
errorHandler:
    Set rs = Nothing
    Set rs1 = Nothing
    MsgBox Err.Description
End Sub

Private Sub Form_Load()
    mebCustomerID.Text = ""
    cboPrintCard.AddItem "YES"
    cboPrintCard.AddItem "NO"
    cboPrintLetter.AddItem "YES"
    cboPrintLetter.AddItem "NO"
    cboSelection.AddItem "LIFE"
    cboSelection.AddItem "MPF"
    cboSubType.AddItem "ING"
    'cboSubType.AddItem "ALHK"
    cboSubType.AddItem "DRESDNER"
    'cboSubType.AddItem "HR"
    cboSubType.AddItem "SCHRODER"
    cboSubType.Visible = False
    cboSelection.Text = "LIFE"
    Call Init
    'StatusBar1.Panels(1).Text = GetNTUserName()
    StatusBar1.Panels(1).Text = strUserID
    
End Sub
Private Sub cmdBrowse_Click()
On Error GoTo errorHandler:
    Call Init
    Call Update_SQL
    Call DisplayStatus   'EC01 Create
Exit Sub
errorHandler:
    MsgBox Err.Description
End Sub

Private Sub cmdExit_Click()
    Unload Me
End Sub

Public Function Get_Connect(ByVal PstrType As String)
On Error GoTo errorHandler
    Call Disconnect
    'Call dbsec.Connect(GetNTUserName(),PROJECT,ChooseConnection(PstrType))
    Call dbsec.Connect(strUserID, PROJECT, ChooseConnection(PstrType))
Exit Function
errorHandler:
    MsgBox Err.Description
End Function

Public Function Disconnect()
On Error GoTo errorHandler
    If dbsec.Isconnected = True Then
        dbsec.Disconnect
    End If
Exit Function
errorHandler:
    MsgBox Err.Description
End Function

Public Sub Update_SQL()
On Error GoTo errorHandler:
Dim adoCSWRst As New ADODB.Recordset
Dim strsql As String
If cboSelection.Text = "LIFE" Then
    Call Get_Connect(cboSelection.Text)
    strLINE = LIFE
Else
    Call Get_Connect(cboSubType.Text)
    strLINE = MPF
End If
    If Trim(mebCustomerID.Text) <> "" Then
        strsql = "SELECT * FROM " & strLINE & "_print_control where " & strLINE & "prc_cust_id = " & mebCustomerID.Text
        Set adoCSWRst = dbsec.ExecuteStatement(strsql)
        mebFirstPrintDate.Text = Format(adoCSWRst.Fields(3).Value, "dd/mm/yyyy")
        mebLastUpdateDate.Text = Format(adoCSWRst.Fields(4).Value, "dd/mm/yyyy")
        txtPrtCountCard.Text = adoCSWRst.Fields(5).Value
        txtPrtCountLetter.Text = adoCSWRst.Fields(6).Value
        If adoCSWRst.Fields(1).Value = "Y" Then
            cboPrintCard.Text = "YES"
            cboPrintCard.Locked = True
        Else
            cboPrintCard.Text = "NO"
        End If
        If adoCSWRst.Fields(2).Value = "Y" Then
            cboPrintLetter.Text = "YES"
            cboPrintLetter.Locked = True
        Else
            cboPrintLetter.Text = "NO"
        End If
        If cboPrintLetter.Text = "NO" Or cboPrintCard.Text = "NO" Then
            cmdPrint.Enabled = True
        End If
    End If
Exit Sub
errorHandler:
    If Err.Number = 3021 Then
        MsgBox "No Such Customer!!", vbExclamation
    Else
        MsgBox Err.Description
    End If
End Sub

'***EC01 Create
Public Sub DisplayStatus()

Dim strsql As String
Dim rec1 As New ADODB.Recordset
Dim DBSecCon As New Dblogon.Dbconnect

'Security Con for checking Status in sec_user_profile
If Not DBSecCon.Connect(strUserID, PROJECT, "CTRSECCON") Then
    MsgBox "DBLogon Connection Broken! Please contact IT for assistance.", vbOKOnly + vbCritical, "Error"
End If
   
strsql = "SELECT 'Status' = Case When secsup_lock_flag <> 3 Then 'Active' Else 'Locked' End From sec_user_profile " & _
         " Where secsup_user_id='" & mebCustomerID.Text & "'"
Set rec1 = DBSecCon.ExecuteStatement(strsql, adUseClient, adOpenStatic, adLockOptimistic)

If Not rec1.EOF Then
    txtStatus.Text = Trim(rec1.Fields(0))
End If

End Sub
'***EC01 End

Public Sub Initialization()
'    mebCustomerID.Mask = ""
    mebCustomerID.Text = ""
'    mebCustomerID.Mask = "########"
'    mebFirstPrintDate.Mask = ""
    mebFirstPrintDate.Text = ""
'    mebFirstPrintDate.Mask = "##/##/####"
'    mebLastUpdateDate.Mask = ""
    mebLastUpdateDate.Text = ""
'    mebLastUpdateDate.Mask = "##/##/####"
    txtPrtCountCard = ""
    txtPrtCountLetter = ""
    cboPrintCard.Text = ""
    cboPrintLetter.Text = ""
    cmdPrint.Enabled = False
    cboPrintLetter.Locked = False
    cboPrintCard.Locked = False
    frmPrintControl.mebCustomerID.SetFocus
End Sub

Public Sub Init()
    lblFirstPrint.Enabled = False
    mebFirstPrintDate.Enabled = False
    lblLastUpdateDate.Enabled = False
    mebLastUpdateDate.Enabled = False
    lblCardPrintCount.Enabled = False
    txtPrtCountCard.Enabled = False
    lblLetterPrintCount.Enabled = False
    txtPrtCountLetter.Enabled = False
    cboPrintCard.Locked = False
    cboPrintLetter.Locked = False
End Sub

Private Sub Text1_Change()

End Sub
