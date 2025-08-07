VERSION 5.00
Begin VB.UserControl ReprintCard 
   ClientHeight    =   435
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   480
   Picture         =   "ReprintCard.ctx":0000
   ScaleHeight     =   435
   ScaleWidth      =   480
End
Attribute VB_Name = "ReprintCard"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Public Sub Show()
frmPrintControl.Show vbModal
End Sub

Public Property Get UserID() As String
UserID = strUserID
End Property

Public Property Let UserID(ByVal strUser As String)
strUserID = strUser
End Property

