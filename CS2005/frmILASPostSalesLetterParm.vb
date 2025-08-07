Public Class frmILASPostSalesLetterParm

    Private blnShowILAS As Boolean

    Public Sub New(ByVal showILAS As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        blnShowILAS = showILAS
    End Sub

    Private Sub frmILASPostSalesLetterParm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not blnShowILAS Then
            GroupBox1.Visible = False
            GroupBox2.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If blnShowILAS Then
            If Decimal.TryParse(txtSalesRemuneration.Text.Trim(), 0) = False Then
                MsgBox("Sales remuneration must be numeric.", MsgBoxStyle.Exclamation, "Message")
                Return
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class