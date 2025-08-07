Public Class clsCSDataGrid

    Inherits System.Windows.Forms.DataGrid

    Public Sub New()
        MyBase.New()

        Me.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BackgroundColor = System.Drawing.Color.Ivory
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.CaptionForeColor = System.Drawing.Color.Lavender
        Me.CaptionVisible = False
        Me.DataMember = ""
        Me.FlatMode = True
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Location = New System.Drawing.Point(8, 8)
        Me.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.ParentRowsForeColor = System.Drawing.Color.Black

    End Sub

End Class
