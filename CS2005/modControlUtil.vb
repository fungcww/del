Imports System.Globalization

Module modControlUtil
    Public Sub setDtPicker(ByVal Sender As Object, ByVal cevent As ConvertEventArgs)
        Dim c As New DateTimePicker

        c = CType(Sender.Control, DateTimePicker)
        If TypeOf (cevent.Value) Is DateTime Then
            c.Format = DateTimePickerFormat.Custom
            c.CustomFormat = "dd MMMM yyyy"
        Else
            c.Format = DateTimePickerFormat.Custom
            c.CustomFormat = " "
        End If
    End Sub

    'TFtoYN() - Convert from Boolean value (True/False) to String (Y/N) 
    Public Sub TFtoYN(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        If TypeOf (cevent.Value) Is String Then
            Exit Sub
        End If
        If cevent.Value = True Then
            cevent.Value = "Y"
        Else
            cevent.Value = "N"
        End If
    End Sub

    'YNtoTF() - Convert from String (Y/N) to Boolean value (True/False)
    Public Sub YNtoTF(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        If TypeOf (cevent.Value) Is Boolean Then
            Exit Sub
        End If
        If IsDBNull(cevent.Value) Then
            cevent.Value = False
        ElseIf cevent.Value = "Y" Then
            cevent.Value = True
        Else
            cevent.Value = False
        End If
    End Sub

    'DTFormatter() - Detect if the date is null, if yes, set it the datetimepicker as 1/1/1800
    Public Sub DTFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        If Not e.DesiredType Is GetType(DateTime) Then
            Return
        End If
        If e.Value.GetType Is GetType(System.DBNull) Then
            e.Value = CType("1/1/1800", System.DateTime)
        End If
    End Sub

    Public Sub DTParser(ByVal sender As Object, ByVal e As ConvertEventArgs)
        If Not e.DesiredType Is GetType(DateTime) Then
            Return
        End If
        If Not e.Value.GetType Is GetType(DateTime) Then
            Return
        End If
        Dim value As String = CType(e.Value, String)
        If value.Equals("1/1/1800") Then
            e.Value = System.DBNull.Value
        End If
    End Sub

    Public Function FormatSQLDate(ByVal dtIn As Object, ByVal strFormat As String) As String
        If IsDBNull(dtIn) Then
            FormatSQLDate = "null"
        Else
            FormatSQLDate = "'" & Format(dtIn, strFormat) & "'"
        End If
    End Function

    Public Function ConvertFormatSQLDate(ByVal dtIn As Object, ByVal strFormat As String) As String
        If IsDBNull(dtIn) Then
            ConvertFormatSQLDate = "null"
        Else
            'ConvertFormatSQLDate = "'" & Convert.ToDateTime(dtIn).ToString(strFormat, CultureInfo.InvariantCulture) & "'"

            ' strFormat parameter is not used
            ' should include second, millisecond in SQL for timestamp
            ConvertFormatSQLDate = "'" & CType(dtIn, DateTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture) & "'"
        End If
    End Function

    ''' <summary>
    ''' Customer Level Search Issue
    ''' Date Convert to SQLFormat Without Quotes
    ''' </summary>
    ''' <remarks>
    ''' <br>added at 2023-9-25 by oliver</br>
    ''' </remarks>
    ''' <param name="dtIn">Represents a date that needs to be converted to SQLFormat</param>
    ''' <returns></returns>
    Public Function ConvertFormatSQLDateWithoutQuotes(ByVal dtIn As Object) As String
        If IsDBNull(dtIn) Then
            ConvertFormatSQLDateWithoutQuotes = " "
        Else
            ConvertFormatSQLDateWithoutQuotes = CType(dtIn, DateTime).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture)
        End If
    End Function

End Module

