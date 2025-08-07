Attribute VB_Name = "CtrRpcGlobal"
#If Win32 Then
Declare Function spanRpcDisconnect Lib "span32.dll" (ByVal hServer As Long) As Long
#Else
Declare Function spanRpcDisconnect Lib "spanrpc.dll" (ByVal hServer As Long) As Long
#End If

#If Win32 Then
Declare Function spanRpcConnectLogon Lib "span32.dll" (hServer As Long, ByVal source As String, ByVal username As String, ByVal password As String) As Long
#Else
Declare Function spanRpcConnectLogon Lib "spanrpc.dll" (hServer As Long, ByVal source As String, ByVal username As String, ByVal password As String) As Long
#End If

#If Win32 Then
Declare Function superRpcBillDetails2 Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paOption As String, ByVal paNbrOptions As Integer, ByVal paPayFreq As String, ByVal paDetails As String) As Long
#Else
Declare Function superRpcBillDetails2 Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paOption As String, ByVal paNbrOptions As Integer, ByVal paPayFreq As String, ByVal paDetails As String) As Long
#End If

#If Win32 Then
Declare Function superRpcInvestmentValue Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paDetails As String) As Long
#Else
Declare Function superRpcInvestmentValue Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paDetails As String) As Long
#End If

#If Win32 Then
Declare Function superRpcUnitPriceDetails Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paDetails As String) As Long
#Else
Declare Function superRpcUnitPriceDetails Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paDetails As String) As Long
#End If

#If Win32 Then
Declare Function superRpcPeriodConts Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Double, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paStartDate As String, ByVal paEndDate As String, ByVal paPeriodOptions As String) As Long
#Else
Declare Function superRpcPeriodConts Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Double, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paStartDate As String, ByVal paEndDate As String, ByVal paPeriodOptions As String) As Long
#End If

#If Win32 Then
Declare Function superRpcInvestmentValueUnits Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paDetails As String) As Long
#Else
Declare Function superRpcInvestmentValueUnits Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paDetails As String) As Long
#End If

#If Win32 Then
Declare Function superRpcInvestments Lib "suprpc32.dll" (ByVal hServer As Long, ByVal returnValue As String, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paUnitPriceDate As String) As Long
#Else
Declare Function superRpcInvestments Lib "suprpc.dll" (ByVal hServer As Long, ByVal returnValue As String, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paEffectiveDate As String, ByVal paUnitPriceDate As String) As Long
#End If

'Added AC01 - Begin
#If Win32 Then
Declare Function superRpcMemberDetails Lib "suprpc32.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paDetails As String) As Long
#Else
Declare Function superRpcMemberDetails Lib "suprpc.dll" (ByVal hServer As Long, returnValue As Integer, ByVal paFundCode As String, ByVal paMemberNumber As Long, ByVal paDetails As String) As Long
#End If
'Added AC01 - End
