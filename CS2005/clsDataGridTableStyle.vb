Public Class clsDataGridTableStyle
    Inherits DataGridTableStyle

    ' Custom DataGridTableStyle, define global styles for DataGrid here
    ' blnAllowSorting - if user can click the columnheader to sort, default is disabled
    Public Sub New(Optional ByVal blnAllowSorting As Boolean = False)

        MyBase.New()

        Me.AlternatingBackColor = System.Drawing.Color.White
        Me.BackColor = System.Drawing.Color.White
        Me.ForeColor = System.Drawing.Color.Black
        Me.GridLineColor = System.Drawing.Color.Wheat
        Me.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.HeaderForeColor = System.Drawing.Color.Black
        Me.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.SelectionBackColor = System.Drawing.Color.Wheat
        Me.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.AllowSorting = blnAllowSorting

    End Sub

End Class
