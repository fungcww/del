Public Class frmPostSalesCallProductSelect

    Public objDBHeader As Utility.Utility.ComHeader
    Public SelectedProduct As DataRow

    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Private dtProduct As DataTable

    Public Delegate Sub ProductSelectedEventHandler(ByVal sender As Object, ByVal productInfo As DataRowView, ByRef cancel As Boolean)

    Public Event ProductSelected As ProductSelectedEventHandler

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim strErr As String = Nothing

        If txtProductID.Text.Trim() = String.Empty Then
            MsgBox("Please input product ID to search.", MsgBoxStyle.Exclamation)
            Return
        End If

        'If Not clsCRS.RetrievePostSalesCallProducts(txtProductID.Text.Trim(), String.Empty, dtProduct, strErr) Then
        If Not RetrievePostSalesCallProducts(getCompanyCode(Me.objDBHeader.CompanyID), txtProductID.Text.Trim(), String.Empty, dtProduct) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        dgvProduct.DataSource = dtProduct
    End Sub

    Private Sub frmPostSalesCallProductSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = objDBHeader

        dgvProduct.AutoGenerateColumns = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dgvProduct.CurrentRow Is Nothing Then
            MsgBox("No product selected.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim drv As DataRowView = dgvProduct.CurrentRow.DataBoundItem
        Dim blnCancel As Boolean = False

        RaiseEvent ProductSelected(Me, drv, blnCancel)

        If blnCancel = False Then
            SelectedProduct = drv.Row

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim strErr As String = Nothing

        If txtPlanName.Text.Trim() = String.Empty Then
            MsgBox("Please input product name to search.", MsgBoxStyle.Exclamation)
            Return
        End If

        'If Not clsCRS.RetrievePostSalesCallProducts(String.Empty, txtPlanName.Text.Trim(), dtProduct, strErr) Then
        If Not RetrievePostSalesCallProducts(getCompanyCode(Me.objDBHeader.CompanyID), String.Empty, txtPlanName.Text.Trim(), dtProduct) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        dgvProduct.DataSource = dtProduct
    End Sub
End Class