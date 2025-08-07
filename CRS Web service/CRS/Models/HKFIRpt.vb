Imports System.Xml.Serialization

<Serializable()>
Public Class HKFIRpt
    Public Property RptType As String

    Public Property Policy_No As String

    Public Property Plan_Name As String

    Public Property Risk_Level As String

    Public Property Inforce_Date As Date

    Public Property Vulnerability As String

    Public Property Cooling_Off_Date As Nullable(Of Date)

    Public Property Delivery_Date As Nullable(Of Date)

    Public Property mode As String

    Public Property modalpremium As Decimal

    Public Property customerid As Decimal

    Public Property Courtesy_Call_Made As Integer

    Public Property PH_Nameprefix As String

    Public Property PH_Namesuffix As String

    Public Property PH_Firstname As String

    Public Property Tel1 As String

    Public Property Tel2 As String

    Public Property Address1 As String

    Public Property Address2 As String

    Public Property Address3 As String

    Public Property Address4 As String

    Public Property SA_Nameprefix As String

    Public Property SA_Namesuffix As String

    Public Property SA_Firstname As String

    Public Property Location As String
End Class
