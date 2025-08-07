Public Class frmSmsTempMgt

    Dim SMSMgt As New SMSManagementBL

    Private Sub frmSmsTempMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialize_EditPage()
    End Sub


    Private Sub btnaddmsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddmsg.Click
        Dim strUser As String = gsUser.Trim
        Dim strErr As String = String.Empty
        If txtNewEngContent.Text.Trim = String.Empty Then MessageBox.Show("Please provide English SMS Template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
        If txtNewChiContent.Text.Trim = String.Empty Then MessageBox.Show("Please provide Chinese SMS Template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
        Dim confirmationmsg As String = "Following SMS Template will be added." + vbNewLine
        confirmationmsg += "English: " + vbNewLine + txtNewEngContent.Text & vbNewLine & vbNewLine
        confirmationmsg += "Chinese: " + vbNewLine + txtNewChiContent.Text
        Dim result As Integer = MessageBox.Show(confirmationmsg, "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.Cancel Then
            Exit Sub
        Else
            If Not SMSMgt.InsertSMSTemplate(txtNewEngContent.Text, txtNewChiContent.Text, strUser, strErr) Then
                MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Else
                MessageBox.Show("New SMS Template has been added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                ResetAddPage()
            End If
        End If
    End Sub
    Private Sub ResetAddPage()
        txtNewChiContent.Text = ""
        txtNewEngContent.Text = ""
    End Sub

    Private Sub btnresetnewmsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetnewmsg.Click
        Dim result As Integer = MessageBox.Show("Template has not been created. Do you confirm cleaning the input?", "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.Cancel Then
            Exit Sub
        Else
            ResetAddPage()
            Initialize_EditPage()
        End If
    End Sub

    Private Sub Initialize_EditPage()
        txtEditUuid.Text = ""
        txtEditEngContent.Text = ""
        txtEditChiContent.Text = ""
        Dim dtSmsTemp As New DataTable
        Dim strErr As String = String.Empty
        If Not SMSMgt.GetSMSTemplate(dtSmsTemp, strErr) Then
            MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            dgvSmsTemp.DataSource = dtSmsTemp
        End If
    End Sub

    Private Sub dgvSmsTemp_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSmsTemp.SelectionChanged
        Dim selectedTempRow As DataGridViewRow = dgvSmsTemp.Rows(dgvSmsTemp.CurrentCell.RowIndex)
        txtEditUuid.Text = selectedTempRow.Cells("Uuid").Value
        txtEditEngContent.Text = selectedTempRow.Cells("EngText").Value
        txtEditChiContent.Text = selectedTempRow.Cells("ChiText").Value
    End Sub

    Private Sub btnmodifymsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodifymsg.Click
        Dim strUser As String = gsUser.Trim
        Dim strErr As String = String.Empty
        If txtEditEngContent.Text.Trim = String.Empty Then MessageBox.Show("Please provide English SMS Template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
        If txtEditChiContent.Text.Trim = String.Empty Then MessageBox.Show("Please provide Chinese SMS Template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
        Dim confirmationmsg As String = "Please confirm the new SMS Template." + vbNewLine
        confirmationmsg += "English: " + vbNewLine + txtEditEngContent.Text & vbNewLine & vbNewLine
        confirmationmsg += "Chinese: " + vbNewLine + txtEditChiContent.Text
        Dim result As Integer = MessageBox.Show(confirmationmsg, "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.Cancel Then
            Exit Sub
        Else
            If Not SMSMgt.UpdateSMSTemplate("UPDATE", txtEditEngContent.Text, txtEditChiContent.Text, strUser, txtEditUuid.Text, strErr) Then
                MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Else
                MessageBox.Show("SMS Template has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Initialize_EditPage()
            End If
        End If
    End Sub

    Private Sub btndeletemsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletemsg.Click
        Dim strUser As String = gsUser.Trim
        Dim strErr As String = String.Empty
        Dim confirmationmsg As String = "Following SMS Template will be obsoleted." + vbNewLine
        confirmationmsg += "English: " + vbNewLine + txtEditEngContent.Text & vbNewLine & vbNewLine
        confirmationmsg += "Chinese: " + vbNewLine + txtEditChiContent.Text
        Dim result As Integer = MessageBox.Show(confirmationmsg, "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.Cancel Then
            Exit Sub
        Else
            If Not SMSMgt.UpdateSMSTemplate("DELETE", txtEditEngContent.Text, txtEditChiContent.Text, strUser, txtEditUuid.Text, strErr) Then
                MessageBox.Show(strErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Else
                MessageBox.Show("SMS Template has been obsoleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Initialize_EditPage()
            End If
        End If
    End Sub
End Class
