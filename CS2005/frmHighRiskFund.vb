Public Class frmHighRiskFund
    Inherits System.Windows.Forms.Form

    Private _dtTmp As DataTable
    Public Property FundColl() As DataTable
        Get
            Return _dtTmp
        End Get
        Set(ByVal value As DataTable)
            _dtTmp = value
        End Set
    End Property


    Private Sub frmFund_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dgvFund.DataSource = FundColl()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim dtSelectedFund As DataTable
        dtSelectedFund = _dtTmp.Clone()
        For cnt As Integer = 0 To dgvFund.Rows.Count - 1
            If dgvFund.Rows(cnt).Cells("ChkFund").Value Then
                Dim dr As DataRow = dtSelectedFund.NewRow
                dr("FundCode") = dgvFund.Rows(cnt).Cells("FundCode").Value
                dr("FundDescription") = dgvFund.Rows(cnt).Cells("FundDescription").Value
                dr("FundChiDesc") = dgvFund.Rows(cnt).Cells("FundChiDesc").Value
                dtSelectedFund.Rows.Add(dr)
            End If
        Next
        Me.FundColl() = dtSelectedFund
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        FundColl = _dtTmp.Clone()
        Me.Close()
    End Sub
End Class