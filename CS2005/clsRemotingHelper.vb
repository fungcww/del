Imports System
Imports System.Collections
Imports System.Runtime.Remoting

Public Class RemotingHelper

    Private Shared _isInit As Boolean
    Private Shared _wellKnownTypes As IDictionary

    Public Shared Function GetObject(ByVal type As Type) As Object
        If Not _isInit Then InitTypeCache()

        Dim entr As WellKnownClientTypeEntry = DirectCast(_wellKnownTypes(type), WellKnownClientTypeEntry)

        If (entr Is Nothing) Then
            Throw New RemotingException("Type not found!")
        End If

        Return Activator.GetObject(entr.ObjectType, entr.ObjectUrl)
    End Function

    Public Shared Sub InitTypeCache()
        _isInit = True
        _wellKnownTypes = New Hashtable

        Dim entr As WellKnownClientTypeEntry

        For Each entr In RemotingConfiguration.GetRegisteredWellKnownClientTypes()
            If (entr.ObjectType Is Nothing) Then
                Throw New RemotingException("A configured type could not " & "be found. Please check spelling")
            End If
            _wellKnownTypes.Add(entr.ObjectType, entr)
        Next
    End Sub

End Class
