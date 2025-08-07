Imports System.Xml.Serialization

<Serializable()> _
Public Class RptILASNotificationLetterInfo
    Public Property RptILASNotificationLetterList As List(Of RptILASNotificationLetter) = New List(Of RptILASNotificationLetter)

    Public Property iMasterPlan As List(Of String) = New List(Of String)

    Public Property iKnowUSinglePremiumPlan As List(Of String) = New List(Of String)

    Public Property iKnowURegularPlan As List(Of String) = New List(Of String)

    Public Property HorizonPlan As List(Of String) = New List(Of String)

    Public Property iKnowSPUpfrontCharge As String = String.Empty

    Public Property HorizonUpfrontCharge As String = String.Empty

    Public Property SuitabilityOption As String = String.Empty

    Public Property CompanyLogo As RptCompanyLogo
End Class
