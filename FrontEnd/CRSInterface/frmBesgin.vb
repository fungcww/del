Public Class frmBesgin

    Public PROJECT As String
    Public CIWSQL As String
    Public CIWSEC As String
    Public SQLDB As String
    Public CiwLibName As String
    Public CiwLibProg As String
    Public LibName As String
    Public CTR As String
    Public AGT As String
    Public CamServer As String

    Private objMQHead As New Utility.Utility.MQHeader
    Private objPOSHead As New Utility.Utility.POSHeader

    Private Sub frmBegin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MQ
        'objMQHead.QueueManager = My.Settings.Qman
        'objMQHead.RemoteQueue = My.Settings.WinRemoteQ
        'objMQHead.ReplyToQueue = My.Settings.LAReplyQ
        'objMQHead.LocalQueue = My.Settings.WinLocalQ
        'objMQHead.Timeout = 90000000 'My.Settings.Timeout

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            frmCRS_MainScreen.formCalled = ComboBox1.Text
            frmCRS_MainScreen.MQQueuesHeader = Me.objMQHead
            Select Case ComboBox1.Text
                Case "Direct Debit Enquiry"
                    frmCRS_MainScreen.ClientNo = txtClientNo.Text.Trim
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Direct Debit Enquiry"
                Case "Financial Information"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Financial Information"
                Case "Transaction History"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Transaction History"
                Case "Policy General Information"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Policy General Information"
                Case "Financial Information"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Financial Information"
                Case "Payment History"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Payment History"
                Case "Component"
                    frmCRS_MainScreen.PolicyNo = txtPolicyNo.Text.Trim
                    frmCRS_MainScreen.formCalled = "Component"
            End Select

            frmCRS_MainScreen.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub
End Class