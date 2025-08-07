Imports System.IO
Imports POSCommCtrl

Public Class frmSmartLegacyHistory

    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private strPolicyInUse As String

    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property

    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property

    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyInUse
        End Get
        Set(ByVal value As String)
            strPolicyInUse = value
        End Set
    End Property

    Public Sub ShowSLHRecord()
        Ctrl_POS_SmartLegacyHist.MQQueuesHeader = Me.objMQQueHeader
        Ctrl_POS_SmartLegacyHist.DBHeader = Me.objDBHeader
        Ctrl_POS_SmartLegacyHist.SetPolicyNumber = strPolicyInUse
        Ctrl_POS_SmartLegacyHist.InitSmartLegacyHistory(5)
    End Sub


    Private Sub HandleExceptionTryCatch(ByVal strErr As String)
        MsgBox(strErr, MsgBoxStyle.Critical)
    End Sub

End Class