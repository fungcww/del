Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml

Public Class encodeWSParam

    Public Function Serialize(ByVal obj As Object) As String

        Dim ser As XmlSerializer = New XmlSerializer(obj.GetType())
        Dim sb As StringBuilder = New StringBuilder()
        Dim writer As StringWriter = New StringWriter(sb)
        ser.Serialize(writer, obj)
        Return sb.ToString()
    End Function

    Public Function Deserialize(Of T)(ByVal s As String) As T

        Dim xdoc As XmlDocument = New XmlDocument()

        Try

            xdoc.LoadXml(s)
            Dim reader As XmlNodeReader = New XmlNodeReader(xdoc.DocumentElement)

            Dim ser As XmlSerializer = New XmlSerializer(GetType(T))
            Dim obj As Object = ser.Deserialize(reader)

            Return obj

        Catch

            Return CType(Nothing, T)

        End Try

    End Function

End Class
