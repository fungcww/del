Public Class frmTranHist

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Ctrl_TranHist1.PolicyNoInUse = txtPolicyNo.Text.Trim
        Ctrl_TranHist1.ShowTranHistRcd()
    End Sub
End Class