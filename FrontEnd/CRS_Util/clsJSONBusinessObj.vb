'********************************************************************
' Amend By:  Kay Tsang
' Date:         11 Nov 2016
' Project:      ITSR492
' Ref:          KT20161111
' Changes:      Add visit CS flag objects
'********************************************************************
' Amend By:  Kay Tsang
' Date:         26 Jul 2018
' Project:      ITSR879
' Ref:          KT20180726
' Changes:      Add pending txn for merchant
'********************************************************************

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

    'KT20161111 start
    Public Class clsUnfreezeOTPFunctionRequest
        Public policyId As String
        Public customerId As String
        Public lang As String
    End Class

    Public Class clsUnfreezeOTPFunctionResponse
        Public policyId As String
        Public customerId As String
        Public result As Boolean
        Public msg As MessageClsTNG
    End Class

    Public Class clsGetCSViewRequest
        Public customerId As String
        Public lang As String
    End Class

    Public Class clsGetCSViewResponse
        Public csvList As List(Of CSView)
        Public msg As MessageClsTNG
    End Class

    Public Class CSView
        Public policyId As String
        Public customerId As String
        Public paired As String
        Public allowToLinkupWithdraw As String
        Public lastLinkupRequestId As String
        Public lastLinkupResponse As String
        Public lastTxnTransId As String
        Public lastTxnStatus As String
        Public lastTxnRequestId As String
        Public lastTxnResponse As String
        Public dailyStartPrincipal As Double
        Public dailyCurrentPrincipal As Double
    End Class

    Public Class clsCheckOTPLockStatusRequest
        Public policyId As String
        Public customerId As String
        Public lang As String
    End Class

    Public Class clsCheckOTPLockStatusResponse
        Public policyId As String
        Public customerId As String
        Public lockDate As String
        Public msg As MessageClsTNG
    End Class

    Public Class clsCheckVisitCSFlagRequest
        Public policyId As String
        Public customerId As String
        Public lang As String
    End Class

    Public Class clsUpdateVisitCSFlagRequest
        Public policyId As String
        Public customerId As String
        Public visitCSFlag As String
        Public lang As String
        Public updateUser As String
    End Class

    Public Class clsVisitCSFlagResponse
        Public policyId As String
        Public customerId As String
        Public visitCSFlag As String
        Public updateDate As String
        Public updateUser As String
        Public msg As MessageClsTNG
    End Class

    Public Class clsPendingTxnRequest
        Public policyId As String
        Public lang As String
    End Class

    Public Class clsPendingTxnResponse
        Public policyId As String
        Public pendingTxnExist As Boolean
        Public pendingTxnAmount As Nullable(Of Decimal)
        Public msg As MessageClsTNG
    End Class

    Public Class MessageClsTNG
        Public message_zh As String
        Public message_en As String
        Public resultCode As String
        Public refCode As String
    End Class
    'KT20161111 end
    'ITDCIC New Opt-Out Option Start
    Public Class clsGetOdsidRequest
        Public custHkid As String
        Public custPassportNo As String
        Public custId As String
        Public custFullname As String
    End Class

    Public Class clsGetOdsidResponse
        Public custId As String
        Public odsId As String
        Public custHkid As String
        Public custPassportNo As String
        Public custFullname As String
        Public msg As MessageClsTNG
    End Class

    Public Class clsUpdateOptOutFlagRequest
        Public custId As String
        Public odsId As String
        Public companyId As String
        Public optOut As String
        Public updateUser As String
        Public updateDateTime As String
    End Class
    Public Class clsUpdateOptOutFlagResponse
        Public custId As String
        Public odsId As String
        Public companyId As String
        Public optOut As String
        Public thirdPartyOptOut As String
        Public updateDateTime As String
        Public updateUser As String
        Public updateSuccess As Boolean
        Public needUpdate As Boolean
        Public msg As MessageClsTNG
    End Class
    Public Class clsGetInitOptOutFlagRequest
        Public custId As String
        Public odsId As String
        Public companyId As String
    End Class
    Public Class clsGetInitOptOutFlagResponse
        Public custId As String
        Public odsId As String
        Public companyId As String
        Public initOptOut As String
        Public initThirdPartyOptOut As String
        Public updateUser As String
        Public updateDateTime As String
        Public msg As MessageClsTNG
    End Class
    Public Class clsCamNewOptOut
        Public custId As String
        Public odsId As String
        Public optOut As String
    End Class
    Public Class clsGetInitOptOutFlagCamRequest
        Public custId As List(Of String)
        Public inOutFilter As String
    End Class
    Public Class clsGetInitOptOutFlagCamResponse
        Public optOutRecord As List(Of clsCamNewOptOut)
        Public msg As MessageClsTNG
    End Class
    Public Class clsCheckCrmUserRequest
        Public userId As String
    End Class
    Public Class clsCheckCrmUserResponse
        Public validUser As Boolean
        Public msg As MessageClsTNG
        Public admin As Boolean
    End Class
    'ITDCIC New Opt-Out Option End

    'ITDCIC 7-11 & ISC claim payout Start
    Public Class cls7ElevenUnfreezePayoutRequest
        Public payoutId As Integer
        Public userId As String
        Public lang As String
        Public merchantId As String
    End Class

    Public Class cls7ElevenUnfreezePayoutResponse
        Public result As Boolean
        Public message As String
    End Class

    Public Class cls7ElevenVoidPayoutRequest
        Public payoutId As Integer
        Public userId As String
        Public lang As String
        Public merchantId As String
    End Class

    Public Class cls7ElevenVoidPayoutResponse
        Public result As Boolean
        Public message As String
    End Class

    Public Class cls7ElevenReqPayoutRequest
        Public mobile As String
        Public custDetails As Dictionary(Of String, String)
        Public payoutDate As String
        Public payoutType As String
        Public payoutDept As String
        Public payoutCurrency As String
        Public payoutAmount As String
        Public policyCurrency As String
        Public exchangeRate As String
        Public cmIndex As String
        Public merchantId As String
        Public claimId As String
        Public customerId As String
        Public policyId As String
        Public lang As String
    End Class

    Public Class cls7ElevenReqPayoutResponse
        Public result As Boolean
        Public message As String
    End Class

    Public Class cls7ElevenSrhSmsHistRequest
        Public payoutId As Integer
        Public lang As String
        Public merchantId As String
    End Class

    Public Class cls7ElevenSrhSmsHistResult
        Public smsMessageId As String
        Public sendDate As String
        Public mobile As String
        Public content As String
        Public payoutId As String
        Public seq As String
        Public resendSMS As Boolean
        Public status As String
    End Class

    Public Class cls7ElevenSrhSmsHistResponse
        Public result As String
        Public message As String
        Public policyId As String
        Public smsHistory As List(Of cls7ElevenSrhSmsHistResult)
    End Class

    Public Class cls7ElevenResendSmsRequst
        Public smsMessageId As String
        Public userId As String
        Public lang As String
        Public merchantId As String
    End Class

    Public Class cls7ElevenResendSmsResponse
        Public result As String
        Public message As String
        Public smsMessageId As String
        Public sendDate As String
    End Class

    'ITDCIC 7-11 & ISC claim payout End

    'KT20171207
    Public Class clsBackdoorRegRequest
        Public ceidentifier As String
    End Class

    Public Class clsBackdoorRegResponseData
        Public unlocKey As String
    End Class

    Public Class clsBackdoorRegResponseError
        Public code As String
        Public message As String
    End Class

    Public Class clsBackdoorRegResponse
        Public status As String
        Public data As clsBackdoorRegResponseData
        Public [error] As clsBackdoorRegResponseError
    End Class

    'KT20180726
    Public Class clsMerchantPendingTxnRequest
        Public policyId As String
        Public productId As String
        Public merchantId As String
        Public lang As String
    End Class
    Public Class clsMerchantPendingTxnResponse
        Public result As Boolean
        Public hasPending As Boolean
        Public message As String
    End Class
    Public Class clsMerchantProductRequest
        Public policyId As String
        Public lang As String
    End Class

    Public Class clsMerchantProductResponse
        Public result As Boolean
        Public productId As String
        Public merchantId As String
        Public accountId As String
        Public message As String
    End Class
    Public Class clsValidateO2ORequest
        Public policyId As String
        Public productId As String
        Public merchantId As String
        Public currency As String
        Public amount As Nullable(Of Decimal)
        Public userId As String
        Public lang As String
    End Class
    Public Class clsValidateO2OResponse
        Public result As Boolean
        Public message As String
    End Class
    Public Class clsPerformO2ORequest
        Public policyId As String
        Public productId As String
        Public merchantId As String
        Public currency As String
        Public amount As Nullable(Of Decimal)
        Public userId As String
        Public txnId As String
        Public lang As String
    End Class
    Public Class clsPerformO2OResponse
        Public result As Boolean
        Public message As String
        Public currency As String
        Public amount As Nullable(Of Decimal)
    End Class
    Public Class clsMerchantTxnHistoryRequest
        Public merchantId As String
        Public policyId As String
        Public productId As String
        Public accountId As String
        Public startDate As String
        Public endDate As String
        Public lang As String
        Public internal As Boolean
    End Class
    Public Class clsMerchantTxnHistoryResponse
        Public policyBalance As Nullable(Of Decimal)
        Public policyBalanceAsOfDate As String
        Public policyBalanceCcy As String
        Public transactionHistory As List(Of clsMerchantTxnHistory)
        Public result As Boolean
        Public message As String
    End Class
    Public Class clsMerchantTxnHistory
        Public txnAmt As Nullable(Of Decimal)
        Public txnDate As String
        Public txnCcy As String
        Public txnEntry As String
    End Class
    'AL20210201 eService PI Access
    Public Class clsPiAuthRequest
        Public customerId As String
        Public ceUser As String
    End Class
    Public Class clsPiAuthResponseData
        Public token As String
        Public unlocKey As String
        Public expiry As String
    End Class

    Public Class clsPiAuthResponseError
        Public code As String
        Public message As String
    End Class

    Public Class clsPiAuthResponse
        Public status As String
        Public data As clsPiAuthResponseData
        Public [error] As clsPiAuthResponseError
    End Class
End Class




