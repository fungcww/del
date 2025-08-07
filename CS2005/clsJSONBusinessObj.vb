
Public Class clsJSONBusinessObj

    Public Class msgCls

        Public message_zh As String
        Public message_en As String
        Public IsSystemError As Boolean
    End Class


    Public Class clsPolicySearch

        Public policyAccountId As String
        Public callerRequest As clsCallerRequest
        
        Public Sub New()
            policyAccountId = ""
        End Sub

    End Class

    Public Class clsCustomerSearch

        Public lastNameEn As String
        Public firstNameEn As String
        Public lastNameZh As String
        Public firstNameZh As String
        Public docId As String
        Public policyAccountId As String
        Public gender As String
        Public phoneNumber As String
        Public policyRelateCode As String
        Public callerRequest As clsCallerRequest
        Public agentCode As String
        Public agentLicense As String

        Public Sub New()
            lastNameEn = ""
            firstNameEn = ""
            lastNameZh = ""
            firstNameZh = ""
            docId = ""
            policyAccountId = ""
            gender = ""
            phoneNumber = ""
            policyRelateCode = ""
            agentCode = ""
            agentLicense = ""
        End Sub

    End Class

    Public Class clsCallerRequest
        Public sessionKey As String
        Public company As String
        Public Isvalid As Boolean
        Public msgCls As String
        Public customerId As String
        Public userId As String
        Public application As String

        Public Sub New()
            sessionKey = ""
            company = "HK"
            msgCls = Nothing
            Isvalid = False
            application = "crs"
        End Sub
    End Class


    Public Class clsAgentFamily

        Public SortKey As String
        Public AgentCode As String
        Public LastName As String
        Public FirstName As String
        Public EnglishName As String
        Public ChineseName As String
        Public BirthDay As Date
    End Class


    Public Class clsAgentFamilyList

        Public msgCls As msgCls
        Public AgentList As List(Of clsAgentFamily)

        Public Sub New()
            msgCls = New msgCls
            AgentList = New List(Of clsAgentFamily)
        End Sub
    End Class

    Public Class clsLifePolicy
        Public policyAccountId As String
        Public companyID As String
        Public customerId As String
        Public productID As String
        Public productDesc As String
        Public policyCurrency As String
        Public accountStatusCode As String
        Public accountStatus As String
        Public sumInsured As Nullable(Of Decimal)
        Public policyPremium As Nullable(Of Decimal)
        Public policyEffectiveDate As Nullable(Of Date)
        Public policyEndDate As Nullable(Of Date)
        Public paidToDate As Nullable(Of Date)
        Public billToDate As Nullable(Of Date)
        Public payModeCode As String
        Public payMode As String
        Public agentCode As String
        Public agentLicense As String
        Public smoker As String

    End Class
    

    Public Class clsGIPolicy

        Public policyAccountId As String
        Public companyID As String
        Public customerId As String
        Public productID As String
        Public productDescEn As String
        Public productDescZh As String
        Public policyCurrency As String
        Public accountStatusCode As String
        Public accountStatus As String
        Public sumInsured As Nullable(Of Decimal)
        Public policyPremium As Nullable(Of Decimal)
        Public policyEffectiveDate As Nullable(Of Date)
        Public policyEndDate As Nullable(Of Date)
        Public group As String
        Public viewType As String
        Public mainClass As String
        Public type As String

       
    End Class

    Public Class clsPensionPolicy

        Public memberType As String
        Public schemeType As String
        Public accountNumber As String
        Public joinDate As Nullable(Of Date)
        Public memberStatus As String
        Public fundBalance As Nullable(Of Decimal)
        Public fundBalanceDate As Nullable(Of Date)
        Public lastName As String
        Public firstName As String
        Public fullnameZh As String
        Public docId As String
        Public gender As String
        Public dateOfBirth As Nullable(Of Date)

    End Class
 

    Public Class clsEBPolicy

        Public policyAccountId As String
        Public companyID As String
        Public customerId As String
        Public productID As String
        Public productDesc As String
        Public productDescZh As String
        Public employeeName As String
        Public accountStatus As String
        Public policyEffectiveDate As Nullable(Of Date)
        Public policyEndDate As Nullable(Of Date)
        Public insuredName As String
        Public docId As String
        Public relateToCustomer As String
        Public relateCode As String
        Public gender As String
        Public type As String
        Public employeeCode As String
        Public planCode As String

        'Public policyCurrency As String
        'Public accountStatusCode As String

        'Public sumInsured As Nullable(Of Decimal)
        'Public policyPremium As Nullable(Of Decimal)


        'Public paidToDate As Nullable(Of Date)
        'Public billToDate As Nullable(Of Date)
        'Public payModeCode As String
        'Public payMode As String

        'Public firstName As String
        'Public lastName As String



    End Class

    Public Class clsCallResponse

        Public sessionKey As String
        Public company As String
        Public Isvalid As Boolean
        Public msgCls As msgCls
        Public customerId As String
        Public userId As String
    End Class

    Public Class clsSearchResponse

        Public lifePolicies As List(Of clsLifePolicy)
        Public giPolicies As List(Of clsGIPolicy)
        Public ebPolicies As List(Of clsEBPolicy)
        Public pensions As List(Of clsPensionPolicy)
        Public callerResponse As clsCallResponse
    End Class

    Public Class clsPolicySearchResponse

        Public policyAccountId As String
        Public groupType As String
        Public callerResponse As clsCallResponse
    End Class

    Public Class clsEBPolicySearchResponse

        Public policyAccountId As String
        Public companyID As String
        Public type As String
        Public callerResponse As clsCallResponse
    End Class

End Class




