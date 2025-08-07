Public Class frmIwsClaimHist
    Public Sub LoadHist(strCompId As String, strPolicyNo As String)
        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(strCompId, "GET_IWS_CLAIM_HIST", New Dictionary(Of String, String)() From {{"strPolicyNo", strPolicyNo}})

            If Not IsNothing(retDs) Then
                If Not IsNothing(retDs.Tables(0)) Then
                    dgvClaimHist.DataSource = retDs.Tables(0)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Get IWS Claim History Error")
        End Try
    End Sub

    Private Sub dgvClaimHist_SelectionChanged(sender As Object, e As EventArgs) Handles dgvClaimHist.SelectionChanged
        Try
            txtRemark.Text = dgvClaimHist.SelectedRows.Item(0).Cells.Item("Comment").Value.ToString()
        Catch ex As Exception

        End Try
    End Sub
End Class