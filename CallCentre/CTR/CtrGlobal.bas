Attribute VB_Name = "CtrGlobal"
'****************************************************************************************
' Amended by : Carmen Wong
' Date : Aug 14 2001
' Description : - Delete the function "ChooseConnection"
'                 All occurrence of ChooseConnection will be replaced by FindConnection
'                 Add option for connection alias of HR
'****************************************************************************************
' Amended by : Anson Chan
' Date : Aug 24 2001
' Description : - Add new function
'                 FindGroupConnection ( Find the DB connection for individual security group )
'
' Ref : AC02
'****************************************************************************************

Public Const CTRSEC = "CTRSECCON"
Public Const PROJECT = "CTR"
Public Const CTRSQL = "CTRSQLCON"

'****** AC02 START ******
Public Function FindGroupConnection(ByVal PstrGroup As String) As String
Dim i As Integer

If InStr(1, UCase(PstrGroup), "CTR_MPF") Then
    FindGroupConnection = "CTRMPFAETSQLCON"
ElseIf InStr(1, UCase(PstrGroup), "CTR_DRESD") Then
    FindGroupConnection = "CTRMPFDRESQLCON"
ElseIf InStr(1, UCase(PstrGroup), "CTR_SCHRD") Then
    FindGroupConnection = "CTRMPFSCHSQLCON"
ElseIf InStr(1, UCase(PstrGroup), "CTR_HR") Then
    FindGroupConnection = "CTRMPFHRSQLCON"
End If

End Function
'****** AC02 END ******

Public Function FindConnection(ByVal Pstrdbc As String) As String
Dim i As Integer

If InStr(1, UCase(Pstrdbc), "AETNA") Then
    FindConnection = "CTRMPFAETSQLCON"
ElseIf InStr(1, UCase(Pstrdbc), "DRESD") Then
    FindConnection = "CTRMPFDRESQLCON"
ElseIf InStr(1, UCase(Pstrdbc), "SCHRD") Then
    FindConnection = "CTRMPFSCHSQLCON"
ElseIf InStr(1, UCase(Pstrdbc), "HR") Then
    FindConnection = "CTRMPFHRSQLCON"
End If

End Function
Public Function StringCheck(ByVal istring) As String
    Dim i As Integer
    For i = 1 To Len(istring)
        If Mid(istring, i, 1) = "'" Then
            StringCheck = StringCheck & Mid(istring, i, 1) & "'"
        Else
            StringCheck = StringCheck & Mid(istring, i, 1)
        End If
    Next
End Function

Public Function MPFSplit(ByVal varData As Variant, ByVal intFieldNumber As Integer, _
                         ByRef intNoOFRecord As Integer) As Collection()
Dim i, j As Integer
intNoOFRecord = (UBound(varData) + 1) / intFieldNumber
ReDim colRecord(intNoOFRecord) As New Collection
    For i = 0 To intNoOFRecord - 1
        For j = 0 To intFieldNumber - 1
            colRecord(i).Add (varData(j + intFieldNumber * i))
        Next j
    Next i
    MPFSplit = colRecord
End Function

'Added AC01 - Begin
'Calculate the complete year of service
Function ComDateDiff(ByVal strDate1 As String, ByVal strDate2 As String) As Integer
Dim intDate1, intDate2, j As Integer

If Len(strDate1) > 0 And Len(strDate2) > 0 Then
    intDate2 = Format(Format(strDate2, "dd/MM/yyyy"), "yyyymmdd")
    strDate1 = Format(DateAdd("yyyy", 1, Format(strDate1, "dd/MM/yyyy")), "dd/mm/yyyy")
    intDate1 = Format(DateAdd("d", -1, Format(strDate1, "dd/MM/yyyy")), "yyyymmdd")
Else
    'date string incomplete, return 0
    ComDateDiff = 0
    Exit Function
End If

j = 0
Do While intDate2 >= intDate1
    strDate1 = Format(DateAdd("yyyy", 1, Format(strDate1, "dd/MM/yyyy")), "dd/mm/yyyy")
    intDate1 = Format(DateAdd("d", -1, Format(strDate1, "dd/MM/yyyy")), "yyyymmdd")
    ComDateDiff = ComDateDiff + 1
    j = j + 1
Loop
ComDateDiff = j
End Function
'Added AC01 - End
