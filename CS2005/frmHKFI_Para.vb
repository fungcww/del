Public Class frmHKFI_Para

    Private Sub frmHKFI_Para_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dtP1To.Value = DateAdd(DateInterval.Day, -1, Today)
        Me.dtP1From.Value = DateAdd(DateInterval.Day, -1, Today)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.FolderBrowserDialog1.SelectedPath = txtPath.Text
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        Me.FolderBrowserDialog1.ShowDialog()
        txtPath.Text = Me.FolderBrowserDialog1.SelectedPath
    End Sub
End Class