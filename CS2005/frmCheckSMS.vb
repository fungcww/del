'**************************
'Date : Apr 15, 2025
'Author : KevinHo
'Purpose : Check SMS Info 
'**************************

Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports System.Configuration

Public Class frmCheckSMS


#Region "UI Event Handlers"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If String.IsNullOrEmpty(txtAgentID.Text) AndAlso String.IsNullOrEmpty(txtPolicyNo.Text) AndAlso String.IsNullOrEmpty(txtRefNo.Text) Then
            MessageBox.Show("Please specify at least one search criteria.", "Error: Search", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            Cursor = Cursors.WaitCursor
            txtSmsContent.Text = String.Empty
            btnSearch.BeginInvoke(searchAsync)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtAgentID.Text = String.Empty
        txtAgentID.Focus()
        txtPolicyNo.Text = String.Empty
        txtRefNo.Text = String.Empty
        dgvSMS.DataSource = Nothing
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
        rbHK.Checked = True
        txtSmsContent.Text = String.Empty
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles rbMC.CheckedChanged, rbHK.CheckedChanged
        Dim selectedRadioButton As RadioButton = CType(sender, RadioButton)
        If selectedRadioButton.Tag IsNot Nothing Then
            If selectedRadioButton.Checked Then
                Select Case selectedRadioButton.Tag.ToString()
                    Case "HK"
                        lblSearchRegion.Text = "Search Results for Hong Kong"
                    Case "MC"
                        lblSearchRegion.Text = "Search Results for Macau"
                End Select
            End If
        End If
    End Sub

    Private Sub dgvSMS_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSMS.SelectionChanged
        ' Ensure a row is selected
        If dgvSMS.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgvSMS.SelectedRows(0)

            If selectedRow.Cells(6) IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells(6).Value) Then
                txtSmsContent.Text = selectedRow.Cells(6).Value.ToString()
            Else
                txtSmsContent.Text = String.Empty
            End If
        End If
    End Sub

#End Region



#Region "Private Utilities"

    Dim searchAsync =
    Sub()
        Dim strAgentId As String = txtAgentID.Text
        Dim strPolicyNo As String = txtPolicyNo.Text
        Dim strRefNo As String = txtRefNo.Text
        Dim strCompany As String = "HK"

        Try

            If rbHK.Checked Then
                strCompany = "HK"
            Else
                strCompany = "MC"
            End If

            lblMessage.Text = "Searching ..."
            dgvSMS.DataSource = Nothing
            Dim dt As DataTable = dbSearchSMS(strAgentId, strPolicyNo, strRefNo, strCompany)
            dgvSMS.DataSource = dt
            dgvSMS.Columns(0).Width = 200
            dgvSMS.Focus()
            lblMessage.Text = "Done."
            wndMain.StatusBarPanel1.Text = dt.Rows.Count & " records selected"
        Catch ex As Exception
            lblMessage.Text = "search error." + ex.Message
            HandleGlobalException(ex, "search error." & vbCrLf & ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "DB Actions/ Operations"
    Dim dbSearchSMS As Func(Of String, String, String, String, DataTable) =
        Function(strAgentId, strPolicyNo, strRefNo, strCompany) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()

            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(strCompany), "GET_SMS_INFO", New Dictionary(Of String, String)() From {
                                              {"agentId", IIf(Not String.IsNullOrEmpty(strAgentId), strAgentId, " ")},
                                              {"policyNos", IIf(Not String.IsNullOrEmpty(strPolicyNo), "%" & strPolicyNo & "%", " ")},
                                              {"referenceNo", IIf(Not String.IsNullOrEmpty(strRefNo), strRefNo, "0")}
                                              }
                                              )
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_SMS_INFO Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_SMS_INFO Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt

        End Function

#End Region



End Class