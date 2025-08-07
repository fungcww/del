Public Class frmFinancialInfo

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Ctrl_FinancialInfo1.PolicyNoInUse = txtPolicyNo.Text.Trim
        Ctrl_FinancialInfo1.ShowSubAcctBalRcd()
    End Sub

End Class