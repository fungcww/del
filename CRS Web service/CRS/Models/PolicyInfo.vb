Imports System.Xml.Serialization
<Serializable()> _
Public Class PolicyInfo
    Public Property DDA As List(Of DDA) = New List(Of DDA)
    Public Property CCDR As List(Of CCDR) = New List(Of CCDR)
    Public Property BillingTypeCodes As List(Of BillingTypeCodes) = New List(Of BillingTypeCodes)
    Public Property AccountStatusCodes As List(Of AccountStatusCodes) = New List(Of AccountStatusCodes)
    Public Property ModeCodes As List(Of ModeCodes) = New List(Of ModeCodes)
    Public Property DDAStatusCodes As List(Of DDAStatusCodes) = New List(Of DDAStatusCodes)
    Public Property CCDRStatusCodes As List(Of CCDRStatusCodes) = New List(Of CCDRStatusCodes)
    Public Property PolicyAccount As List(Of PolicyAccount) = New List(Of PolicyAccount)
    Public Property csw_policy_value As List(Of csw_policy_value) = New List(Of csw_policy_value)
    Public Property agentcodes As List(Of agentcodes) = New List(Of agentcodes)
    Public Property csw_ing_logo_table As List(Of csw_ing_logo_table) = New List(Of csw_ing_logo_table)
    Public Property product_type As List(Of product_type) = New List(Of product_type)
    Public Property couponoptioncodes As List(Of couponoptioncodes) = New List(Of couponoptioncodes)
    Public Property dividendoptioncodes As List(Of dividendoptioncodes) = New List(Of dividendoptioncodes)
    Public Property Product_Chi As List(Of Product_Chi) = New List(Of Product_Chi)
    Public Property cswsp_clientinfo As List(Of cswsp_clientinfo) = New List(Of cswsp_clientinfo)
    Public Property cswsp_PolicyInfo As List(Of cswsp_PolicyInfo) = New List(Of cswsp_PolicyInfo)
    Public Property cswsp_corider2 As List(Of cswsp_corider2) = New List(Of cswsp_corider2)
End Class



<Serializable()> _
Public Class DDA
    Public Property PolicyAccountID As System.String
    Public Property DDASeqNo As System.Decimal
    Public Property DDAStatus As System.String
    Public Property DDALastStatus As System.String
    Public Property DDADrawDate As System.String
    Public Property DDASubmitDate As System.DateTime
    Public Property DDAStsChgDate As System.DateTime
    Public Property DDAEffectiveDate As System.DateTime
    Public Property DDAEndDate As System.DateTime
    Public Property DDALastMaintDate As System.DateTime
    Public Property DDAComments As System.String
    Public Property DDAOperator As System.String
    Public Property DDAFollowUpDate As System.DateTime
    Public Property DDAFollowUpOpr As System.String
    Public Property DDARemarks As System.String
    Public Property DDAPayorInfo As System.String
    Public Property DDABankAccountNo As System.String
    Public Property DDABankerName As System.String
    Public Property DDABankCode As System.String
    Public Property DDABranchCode As System.String
    Public Property timestamp() As System.Byte()
    Public Property DDASIMPIND As System.String
    Public Property DDAPAYERID As System.String
    Public Property DDAPAYERIDTYPE As System.String
    Public Property DDADEBITLIMIT As System.Decimal
    Public Property DCCLPT As System.String
    Public Property DCBKCD As System.String
    Public Property DCACNO As System.String
    Public Property DCBNK As System.String
    Public Property DCPRV As System.String
    Public Property DCCTY As System.String
    Public Property DCRBNK As System.String
    Public Property DCPYR As System.String
End Class

Public Class BillingTypeCodes
    Public Property BillingTypeCode As System.String
    Public Property BillingTypeDesc As System.String
    Public Property timestamp() As System.Byte()
End Class

<Serializable()> _
Public Class CCDR
    Public Property PolicyAccountID As System.String
    Public Property CCDRSeqNo As System.Decimal
    Public Property CCDRStatus As System.String
    Public Property CCDRLastStatus As System.String
    Public Property CCDRDrawDate As System.String
    Public Property CCDRSubmitDate As System.DateTime
    Public Property CCDRStsChgDate As System.DateTime
    Public Property CCDREffectiveDate As System.DateTime
    Public Property CCDREndDate As System.DateTime
    Public Property CCDRLastMaintDate As System.DateTime
    Public Property CCDRComments As System.String
    Public Property CCDROperator As System.String
    Public Property CCDRFollowUpDate As System.DateTime
    Public Property CCDRFollowUpOpr As System.String
    Public Property CCDRRemarks As System.String
    Public Property CCDRCardHolderName As System.String
    Public Property CCDRCardNumber As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class AccountStatusCodes
    Public Property AccountStatusCode As System.String
    Public Property AccountStatus As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class ModeCodes
    Public Property ModeCode As System.String
    Public Property ModeDesc As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class DDAStatusCodes
    Public Property DDAStatusCode As System.String
    Public Property DDAStatusDesc As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class CCDRStatusCodes
    Public Property CCDRStatusCode As System.String
    Public Property CCDRStatusDesc As System.String
    Public Property timestamp() As System.Byte()
End Class
Public Class PolicyAccount
    Public Property PolicyAccountID As System.String
    Public Property CompanyID As System.String
    Public Property ProductID As System.String
    Public Property PolicyEffDate As System.DateTime
    Public Property PolicyEndDate As System.DateTime
    Public Property TermReasonCode As System.String
    Public Property PolicyCurrency As System.String
    Public Property CommencementDate As System.DateTime
    Public Property AccountStatusCode As System.String
    Public Property SumInsured As System.Decimal
    Public Property PremiumDueDate As System.DateTime
    Public Property Takeover As System.String
    Public Property Upgrade As System.String
    Public Property SurrenderValue As System.Decimal
    Public Property BasicCV As System.Decimal
    Public Property DivsOnDep As System.Decimal
    Public Property CouponOnDep As System.Decimal
    Public Property PDFAmount As System.Decimal
    Public Property LoanAmount As System.Decimal
    Public Property APLAmount As System.Decimal
    Public Property PremiumRefund As System.Decimal
    Public Property MiscSuspense As System.Decimal
    Public Property DividendOption As System.String
    Public Property AdditionDeathCvr As System.Decimal
    Public Property CVofPUARB As System.Decimal
    Public Property DividendInterest As System.Decimal
    Public Property CouponInterest As System.Decimal
    Public Property PDFInterest As System.Decimal
    Public Property LoanInterest As System.Decimal
    Public Property APLInterest As System.Decimal
    Public Property OSPremium As System.Decimal
    Public Property PremiumSuspense As System.Decimal
    Public Property CouponOption As System.String
    Public Property ReinstatAmt As System.Decimal
    Public Property BillingType As System.String
    Public Property Mode As System.String
    Public Property ModalPremium As System.Decimal
    Public Property LastModalPremium As System.Decimal
    Public Property RCD As System.DateTime
    Public Property PaidToDate As System.DateTime
    Public Property BillToDate As System.DateTime
    Public Property NoticeRecord As System.String
    Public Property DueAmount As System.Decimal
    Public Property NotYetDueAmount As System.Decimal
    Public Property PolicyTermDate As System.DateTime
    Public Property TotalDeclareValue As System.Decimal
    Public Property CurrDeclVal As System.Decimal
    Public Property MaxmiumLoan As System.Decimal
    Public Property PolicyPremium As System.Decimal
    Public Property PolicyValueEffDate As System.DateTime
    Public Property timestamp() As System.Byte()
    Public Property POSAmt As System.Decimal
    Public Property Disbursement As System.Decimal
    Public Property APLStartDate As System.DateTime
    Public Property APLIndicator As System.String
    Public Property BillNo As System.String
    Public Property PolicyOldSts As System.String
    Public Property StsChgDate As System.DateTime
    Public Property curmodefactor As System.Decimal
    Public Property PREMIUMSTATUS As System.String
End Class
<Serializable()> _
Public Class csw_policy_value
    Public Property cswval_id As System.Int32
    Public Property cswval_TFLOID As System.String
    Public Property cswval_TPOLID As System.String
    Public Property cswval_TASADT As System.String
    Public Property cswval_TPADDT As System.String
    Public Property cswval_TCSHVL As System.Decimal
    Public Property cswval_TBSCSV As System.Decimal
    Public Property cswval_TVLPUA As System.Decimal
    Public Property cswval_TDIVDP As System.Decimal
    Public Property cswval_TDEPIN As System.Decimal
    Public Property cswval_TPDF As System.Decimal
    Public Property cswval_TPDFIN As System.Decimal
    Public Property cswval_TPRMRD As System.Decimal
    Public Property cswval_TLONAT As System.Decimal
    Public Property cswval_TLONIT As System.Decimal
    Public Property cswval_TAPLAT As System.Decimal
    Public Property cswval_TAPLIT As System.Decimal
    Public Property cswval_TMAXLN As System.Decimal
    Public Property cswval_TDSCLN As System.Decimal
    Public Property cswval_TBSELN As System.Decimal
    Public Property cswval_TDSCFR As System.Decimal
    Public Property cswval_TINRRB As System.Decimal
    Public Property cswval_TRDCHV As System.Decimal
    Public Property cswval_TCOUDP As System.Decimal
    Public Property cswval_TCOUIT As System.Decimal
    Public Property cswval_TERRFG As System.String
    Public Property cswval_TOSPRM As System.Decimal
    Public Property cswval_TREAMT As System.Decimal
    Public Property cswval_DivYear As System.String
    Public Property cswval_CouYear As System.String
    Public Property cswval_DivDeclare As System.Decimal
    Public Property cswval_PremSusp As System.Decimal
    Public Property cswval_PremRefund As System.Decimal
    Public Property cswval_PUASITotal As System.Decimal
    Public Property cswval_PUASICurrent As System.Decimal
    Public Property cswval_CouOpt As System.String
    Public Property cswval_DivDepositInt As System.Decimal
    Public Property cswval_DivOpt As System.String
    Public Property cswval_MiscSusp As System.Decimal
    Public Property cswval_CouDelcare As System.Decimal
End Class
<Serializable()> _
Public Class agentcodes
    Public Property PhoneNumber As System.String
    Public Property AgName As System.String
End Class
<Serializable()> _
Public Class csw_ing_logo_table
    Public Property ING_Logo As System.Byte()
    Public Property Address As System.Byte()
    Public Property IService As System.Byte()
    Public Property ICaring As System.Byte()
End Class
<Serializable()> _
Public Class product_type
    Public Property companyid As System.String
    Public Property productid As System.String
    Public Property ProductType As System.String
    Public Property ProductPolValueFunc As System.String
    Public Property PrintValueReport As System.String
End Class
<Serializable()> _
Public Class couponoptioncodes
    Public Property CouponOptionCode As System.String
    Public Property CouponOptionDesc As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class dividendoptioncodes
    Public Property DividendOptionCode As System.String
    Public Property DividendOptionDesc As System.String
    Public Property timestamp() As System.Byte()
End Class
<Serializable()> _
Public Class Product_Chi
    Public Property productid As System.String
    Public Property ChineseDescription As System.String
End Class
<Serializable()> _
Public Class cswsp_clientinfo
    Public Property FirstName As System.String
    Public Property NameSuffix As System.String
    Public Property ChiFstNm As System.String
    Public Property DateOfBirth As System.DateTime
    Public Property Gender As System.String
    Public Property PhoneMobile As System.String
    Public Property PhonePager As System.String
    Public Property SmokerFlag As System.String
    Public Property ChiLstNm As System.String
    Public Property EmailAddr As System.String
    Public Property AgentCode As System.String
    Public Property UnitCode As System.String
    Public Property AgentPhone As System.String
    Public Property CustomerID As System.Decimal
    Public Property PolicyAccountID As System.String
    Public Property PolicyRelateCode As System.String
    Public Property cswpad_add1 As System.String
    Public Property cswpad_add2 As System.String
    Public Property cswpad_add3 As System.String
    Public Property cswpad_city As System.String
    Public Property cswpad_tel1 As System.String
    Public Property cswpad_tel2 As System.String
    Public Property cswpad_fax1 As System.String
    Public Property cswpad_fax2 As System.String
    Public Property Trailer As System.Decimal
    Public Property InsuredCustID As System.Decimal
    Public Property optoutflag As System.String
    Public Property optoutotherflag As System.String
End Class

<Serializable()> _
Public Class cswsp_PolicyInfo
    Public Property PolicyAccountID As System.String
    Public Property ProductType As System.String
    Public Property ProductID As System.String
    Public Property Trailer As System.Decimal
    Public Property CoverageStatus As System.String
    Public Property IssueDate As System.DateTime
    Public Property SumInsured As System.Decimal
    Public Property ExpiryDate As System.DateTime
    Public Property ModalPremium As System.Decimal
    Public Property TBL1 As System.String
    Public Property TBL2 As System.String
    Public Property AccountStatus As System.String
    Public Property Description As System.String
    Public Property ChineseDescription As System.String
    Public Property FirstName As System.String
    Public Property NameSuffix As System.String
    Public Property ChiFstNm As System.String
    Public Property ChiLstNm As System.String
End Class

<Serializable()> _
Public Class cswsp_corider2
    Public Property Productid As System.String
    Public Property Description As System.String
    Public Property Trailer As System.Int32
    Public Property CoverageStatus As System.String
    Public Property AccountStatus As System.String
    Public Property IssueDate As System.DateTime
    Public Property ExpiryDate As System.DateTime
    Public Property ModalPremium As System.Decimal
    Public Property customerid As System.Int32
    Public Property Name As System.String
    Public Property SumInsured As System.Int32
    Public Property CName As System.String
    Public Property CDescription As System.String
    Public Property PolicyAccountId As System.String
End Class
