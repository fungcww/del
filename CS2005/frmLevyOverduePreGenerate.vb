Public Class frmLevyOverduePreGenerate

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.FolderBrowserDialog1.SelectedPath = txtPath.Text
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        Me.FolderBrowserDialog1.ShowDialog()
        txtPath.Text = Me.FolderBrowserDialog1.SelectedPath
    End Sub


    Private Sub frmLevyOverduePreGenerate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim nowDate As DateTime
        nowDate = DateTime.Now

        TextBox1.Text = nowDate.ToString("MM/dd/yyyy")

    End Sub

End Class