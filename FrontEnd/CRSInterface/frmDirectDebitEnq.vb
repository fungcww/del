Public Class frmDirectDebitEnq
    Private ctrlDirDebitEnq As CRS_Ctrl.ctrl_DirectDebitEnq

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Ctrl_DirectDebitEnq1.ClientNoInUse = txtClientNo.Text.Trim
        Ctrl_DirectDebitEnq1.ShowMandateListRcd()
    End Sub
End Class
