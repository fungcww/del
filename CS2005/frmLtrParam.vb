'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR3162 & 4101     Print the letter input parameter form

Public Class frmLtrParam

    Private datFrom, datTo As Date
    Private strPolicy As String
    Public ReadOnly Property FromDate() As Date
        Get
            Return datFrom
        End Get
    End Property

    Public ReadOnly Property ToDate() As Date
        Get
            Return datTo
        End Get
    End Property

    Public ReadOnly Property PolicyNo() As String
        Get
            Return strPolicy
        End Get
    End Property

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        datFrom = CDate(txtFrom.Text)
        datTo = CDate(txtTo.Text)
        strPolicy = txtPolicy.Text
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class