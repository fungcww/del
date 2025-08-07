Public Class frmLevyOverdueFollowUpRpt

    Public runningDateList As DataSet

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.FolderBrowserDialog1.SelectedPath = txtPath.Text
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        Me.FolderBrowserDialog1.ShowDialog()
        txtPath.Text = Me.FolderBrowserDialog1.SelectedPath
    End Sub


    Private Sub frmLevyOverdueFollowUpRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim nowMonth As Integer = DateTime.Now().Month
        Dim nowYear As Integer = DateTime.Now().Year

        'Me.dtP1From.Value = New Date(nowYear, nowMonth, 1)
        'Me.dtP1To.Value = New Date(nowYear, nowMonth, 2)

        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True


        If (Not runningDateList Is Nothing AndAlso runningDateList.Tables.Count > 0 AndAlso runningDateList.Tables(0).Rows.Count > 0) Then

            Me.cboRunningDate.DataSource = runningDateList.Tables(0)
            Me.cboRunningDate.DisplayMember = runningDateList.Tables(0).Columns(0).ToString
            Me.cboRunningDate.ValueMember = runningDateList.Tables(0).Columns(0).ToString

        End If


    End Sub

End Class