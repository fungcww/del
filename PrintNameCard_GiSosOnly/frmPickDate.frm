VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Begin VB.Form frmPickDate 
   Caption         =   "Date"
   ClientHeight    =   1710
   ClientLeft      =   5940
   ClientTop       =   3465
   ClientWidth     =   3120
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   ScaleHeight     =   1710
   ScaleWidth      =   3120
   Begin MSComCtl2.DTPicker DTPToDate 
      Height          =   375
      Left            =   1320
      TabIndex        =   5
      Top             =   600
      Width           =   1695
      _ExtentX        =   2990
      _ExtentY        =   661
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   27983875
      CurrentDate     =   36969
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "Cancel"
      Height          =   375
      Left            =   1680
      TabIndex        =   3
      Top             =   1200
      Width           =   1335
   End
   Begin VB.CommandButton cmdEnter 
      Caption         =   "Enter"
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   1200
      Width           =   1335
   End
   Begin MSComCtl2.DTPicker DTPFromdate 
      Height          =   375
      Left            =   1320
      TabIndex        =   0
      Top             =   120
      Width           =   1695
      _ExtentX        =   2990
      _ExtentY        =   661
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   27983875
      CurrentDate     =   36846
   End
   Begin VB.Label lblToDate 
      Caption         =   "To Date"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   600
      Width           =   975
   End
   Begin VB.Label lblTo 
      Caption         =   "From Date"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   1095
   End
End
Attribute VB_Name = "frmPickDate"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub cmdCancel_Click()
Unload frmPickDate
bolFlag = False     'EC01
End Sub

Private Sub cmdEnter_Click()
frmPickDate.Enabled = False
Fromdate = Format(DTPFromdate, "yyyy-MM-dd")
ToDate = Format(DTPToDate, "yyyy-MM-dd")
Unload frmPickDate
bolFlag = True     'EC01
End Sub

Private Sub Form_Load()
    frmPickDate.Caption = frmMenu.cmbCom.Text & " Printing Date"
    DTPFromdate.Value = Now - 1
    DTPToDate.Value = Now
End Sub
