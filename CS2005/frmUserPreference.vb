Public Class frmUserPreference
    Dim ServiceLogBL As New ServiceLogBL

    Private Sub frmCRSPreference_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtuser.Text = UCase(gsUser)
        Initialize_DefaultMedium()
        Initialize_DefaultInitator()
        GetUserServiceLogPreference()
    End Sub

    Private Sub Initialize_DefaultMedium()
        Dim dtMedium As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetMedium(dtMedium, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cbodefaultmedium.SelectedItem = String.Empty
            cbodefaultmedium.Items.Clear()
            cbodefaultmedium.DataSource = dtMedium
            cbodefaultmedium.ValueMember = "EventSourceMediumCode"
            cbodefaultmedium.DisplayMember = "Medium"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub Initialize_DefaultInitator()
        Dim dtInitator As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetInitiator(dtInitator, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cbodefaultinitiator.SelectedItem = String.Empty
            cbodefaultinitiator.Items.Clear()
            cbodefaultinitiator.DataSource = dtInitator
            cbodefaultinitiator.ValueMember = "EventSourceInitiatorcode"
            cbodefaultinitiator.DisplayMember = "Initiator"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub btnchangedefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangedefault.Click
        lbllastupdatetime.Visible = False
        Dim strdefauldmedium As String = cbodefaultmedium.Text
        Dim strdefaultinitiator As String = cbodefaultinitiator.Text
        If UpdateUserServiceLogPreference(strdefauldmedium, strdefaultinitiator) Then
            MessageBox.Show("User Preference updated successfully. Your default medium is " + strdefauldmedium + " and default initiator is " + strdefaultinitiator + ".")
            lbllastupdatetime.Visible = True
            lbllastupdatetime.Text = "Last updated at " + Now.ToString("HH:MM")
        Else
            MessageBox.Show("Cannot update User Preference updated successfully.")
        End If
    End Sub

    Private Sub GetUserServiceLogPreference()
        Dim dtPreference As DataTable
        If ServiceLogBL.ReadLocalCRSConfig(True, dtPreference) Then
            If dtPreference.Rows.Count = 0 Then
                cbodefaultmedium.SelectedValue = "PC"
                cbodefaultinitiator.SelectedValue = "CTR"
            Else
                cbodefaultmedium.SelectedValue = dtPreference.Rows(0)("Medium").ToString
                cbodefaultinitiator.SelectedValue = dtPreference.Rows(0)("Initiator").ToString
            End If
        Else
            cbodefaultmedium.SelectedValue = "PC"
            cbodefaultinitiator.SelectedValue = "CTR"
        End If
    End Sub

    Private Function UpdateUserServiceLogPreference(ByRef strdefauldmedium As String, ByRef strdefaultinitiator As String) As Boolean
        Try
            If ServiceLogBL.SetLocalCRSConfig(cbodefaultmedium.SelectedValue, cbodefaultinitiator.SelectedValue) Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class