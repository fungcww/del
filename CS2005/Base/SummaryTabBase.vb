Public Class SummaryTabBase
    Inherits System.Windows.Forms.UserControl
    Implements ISummaryTabBase

    Private Property _CompanyID As String Implements ISummaryTabBase._CompanyID

    Public Property CompanyID As String Implements ISummaryTabBase.CompanyID
        Get
            Return _CompanyID
        End Get
        Set(value As String)
            If Not value Is Nothing Then
                _CompanyID = value
            End If
        End Set
    End Property
End Class

Interface ISummaryTabBase

    Property _CompanyID As String
    Property CompanyID() As String

End Interface


