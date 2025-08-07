''' <remarks>
''' Added by Hugo Chan on 2021-05-14
''' Project: CRS - First Level of Access
''' </remarks>
Public Class uclPolicyClient_Asur
    Private Const DATE_FORMAT_DISPLAY As String = "MMM dd, yyyy"

    Private Sub DGVRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVRole.Click
        LoadDataToControls()
    End Sub

    Private Sub DGVRole_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVRole.SelectionChanged
        LoadDataToControls()
    End Sub

    Private Sub LoadDataToControls()
        Dim gridRow As DataGridViewRow

        ClearControlsValues()

        If DGVRole.SelectedRows.Count > 0 Then
            gridRow = DGVRole.SelectedRows(0)

            SetControlsValues(gridRow)
        End If
    End Sub

    Private Sub SetControlsValues(ByVal gridRow As DataGridViewRow)
        Dim roleDescription As String
        Dim relation As String
        Dim addressProof As String

        roleDescription = Convert.ToString(gridRow.Cells("role_desc").Value)
        relation = Convert.ToString(gridRow.Cells("Relation").Value)
        addressProof = Convert.ToString(gridRow.Cells("addr_proof").Value)

        cboRole.Items.Add(roleDescription)
        cboRole.SelectedIndex = 0
        txtCustomerNo.Text = Convert.ToString(gridRow.Cells("Customer_No").Value)
        txtHKID.Text = Convert.ToString(gridRow.Cells("HKID").Value)
        txtSex.Text = Convert.ToString(gridRow.Cells("Gender").Value)
        If Not gridRow.Cells("DOB").Value Is Nothing Then
            If Not String.IsNullOrWhiteSpace(Convert.ToString(gridRow.Cells("DOB").Value)) Then
                Try
                    txtDOB.Text = Convert.ToDateTime(gridRow.Cells("DOB").Value).ToString(DATE_FORMAT_DISPLAY)
                Catch ex As Exception
                    ' catch error for in-case string value is incorrect format
                End Try
            End If
        End If
        cboRelation.Items.Add(relation)
        cboRelation.SelectedIndex = 0
        txtTrustee.Text = Convert.ToString(gridRow.Cells("Trustee").Value)
        txtBeneRemark.Text = Convert.ToString(gridRow.Cells("BeneRemark").Value)
        txtCustomerName.Text = Convert.ToString(gridRow.Cells("Name").Value)
        txtAddress.Text = Convert.ToString(gridRow.Cells("Address").Value)
        txtShare.Text = Convert.ToString(gridRow.Cells("Share").Value)
        chkShare.Checked = False
        cboAddrProof.Items.Add(addressProof)
        cboAddrProof.SelectedIndex = 0

        txtLAClientNo_Client.Text = String.Empty
        txtSmokerFlag.Text = String.Empty
        txtOldCustNo.Text = String.Empty
        txtLAClientNo_Alt.Text = String.Empty
        txtLAClientNo.Text = String.Empty
    End Sub

    Private Sub ClearControlsValues()
        cboRole.Items.Clear()
        txtCustomerNo.Text = String.Empty
        txtHKID.Text = String.Empty
        txtSex.Text = String.Empty
        txtDOB.Text = String.Empty
        cboRelation.Items.Clear()
        txtTrustee.Text = String.Empty
        txtBeneRemark.Text = String.Empty
        txtCustomerName.Text = String.Empty
        txtAddress.Text = String.Empty
        txtShare.Text = String.Empty
        chkShare.Checked = False
        cboAddrProof.Items.Clear()

        txtLAClientNo_Client.Text = String.Empty
        txtSmokerFlag.Text = String.Empty
        txtOldCustNo.Text = String.Empty
        txtLAClientNo_Alt.Text = String.Empty
        txtLAClientNo.Text = String.Empty
    End Sub
End Class
