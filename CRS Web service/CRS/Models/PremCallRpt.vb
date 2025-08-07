Imports System.Xml.Serialization

<Serializable()>
Public Class PremCallRpt
    Public Property policyaccountid As String

    Public Property productid As String

    Public Property description As String

    Public Property paidtodate As Date

    Public Property mode As String

    Public Property aplindicator As String

    Public Property modalpremium As Decimal

    Public Property posamt As Decimal

    Public Property billingtype As String

    Public Property nameprefix As String

    Public Property namesuffix As String

    Public Property firstname As String

    Public Property phonemobile As String

    Public Property phonepager As String

    Public Property cswpad_tel1 As String

    Public Property cswpad_tel2 As String

    Public Property cswpad_add1 As String

    Public Property cswpad_add2 As String

    Public Property cswpad_add3 As String

    Public Property cswpad_city As String

    Public Property unitcode As String

    Public Property agentcode As String

    Public Property locationcode As String

    Public Property nameprefix1 As String

    Public Property namesuffix1 As String

    Public Property firstname1 As String

    <XmlElement(ElementName:="Is Prem. Holiday")> _
    Public Property is_prem_holiday As String

End Class
