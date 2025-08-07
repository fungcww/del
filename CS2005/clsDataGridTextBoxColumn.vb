Public Class clsDataGridTextBoxColumn

    Inherits DataGridTextBoxColumn

    Private strCmpVal As String

    'Public Sub New()
    '    blnHighlight = False
    'End Sub

    Public Sub New(ByVal strVal As String)
        strCmpVal = strVal
    End Sub

    'Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
    '    '
    '    ' Get data from the underlying record and format for display.
    '    '
    '    Dim oVal As Object = MyBase.GetColumnValueAtRow(cm, RowNum)
    '    If oVal.GetType Is GetType(DBNull) Then
    '        Return ""                         ' String to display for DBNull.
    '    Else
    '        Return objCS.VBDate(oVal)
    '    End If

    'End Function

    Protected Overloads Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal source As CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As Brush, ByVal foreBrush As Brush, ByVal alignToRight As Boolean)

        ' the idea is to conditionally set the foreBrush and/or backbrush 
        ' depending upon some crireria on the cell value 
        ' Here, we color anything that begins with a letter higher than 'F' 

        Try

            Dim o As Object
            o = Me.GetColumnValueAtRow(source, rowNum)

            If (Not (o) Is Nothing) Then
                'Dim c As Char
                'c = CType(o, String).Substring(0, 1)
                Dim c As String
                c = Trim(CType(o, String))
                If (strCmpVal = "" And c <> strCmpVal) OrElse (strCmpVal <> "" And c = strCmpVal) Then
                    'If blnHighlight Then
                    ' could be as simple as 
                    backBrush = New SolidBrush(Color.Red)
                    ' or something fancier... 
                    'backBrush = New LinearGradientBrush(bounds, Color.FromArgb(255, 200, 200), Color.FromArgb(128, 20, 20), LinearGradientMode.BackwardDiagonal)
                    foreBrush = New SolidBrush(Color.Black)
                End If
            End If
        Catch ex As Exception
            ' empty catch 
        Finally
            ' make sure the base class gets called to do the drawing with 
            ' the possibly changed brushes 
            MyBase.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight)

        End Try

    End Sub

End Class

