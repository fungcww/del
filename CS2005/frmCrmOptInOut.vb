Imports System.IO
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports CRS_Component.APICall
Imports CS2005.CrmFflSearchResult
Imports CS2005.CrmLifeDetailResult
Imports CS2005.CrmLifeSearchResult
Imports CS2005.CrmProspectSearchResult
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Linq

Public Class frmCrmOptInOut
    Private sApiBaseUrl, sSearchFfl, sGetProspect, sSearchLife, sGetLifeDetail, sSearchProspect, sUpdateFfl, sUpdateLife, sUpdateProspect, sApiKey As String
    Private Sub frmCrmOptInOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'sApiBaseUrl = "https://uat-apihk.hk.intranet/customer-api/v1/optinws/hk/u105"
        sApiBaseUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "OptInOutApi")
        'sApiKey = "HdMkb9zXYurxmxiGI67PaCxfu1EJFDBe"
        sApiKey = System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey")
        'sSearchFfl = "/crm/ffl/v1/search"
        sSearchFfl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutSearchFfl")
        'sGetProspect = "/crm/prospect/v1/source"
        sGetProspect = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutGetProspect")
        'sSearchLife = "/crm/life/v1/search"
        sSearchLife = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutSearchLife")
        'sGetLifeDetail = "/crm/life/v1/detail"
        sGetLifeDetail = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutGetLifeDetail")
        'sSearchProspect = "/crm/prospect/v1/search"
        sSearchProspect = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutSearchProspect")
        'sUpdateFfl = "/crm/ffl/v1/update"
        sUpdateFfl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutUpdateFfl")
        'sUpdateLife = "/life/v2/update"
        sUpdateLife = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutUpdateLife")
        'sUpdateProspect = "/crm/prospect/v1/update"
        sUpdateProspect = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ApiFunOptInOutUpdateProspect")
        loadProspect()
        cbCom.SelectedItem = "All"
        UclCrmOptInOutDtl1.frmOptout = Me

        UclCrmOptInOutDtl1.Location = New Point(0, 0)
    End Sub

    Private Sub loadProspect()
        'tempAPIUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CommonAPIEndPoint")
        Dim responseString As String = GetApiResponse(sApiBaseUrl + sGetProspect, sApiKey)
        Dim prospectResultObj As CrmProspectResult = JsonConvert.DeserializeObject(Of CrmProspectResult)(responseString)

        cbProspect.DataSource = New BindingSource(prospectResultObj.sourceList, Nothing)
        cbProspect.DisplayMember = "Value"
        cbProspect.ValueMember = "Value"
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        searchCustomer()
    End Sub
    Public Sub searchCustomer()
        ' Check if at least one search condition is provided
        If String.IsNullOrWhiteSpace(txtEngName.Text) AndAlso
           String.IsNullOrWhiteSpace(txtChiName.Text) AndAlso
           String.IsNullOrWhiteSpace(txtHkid.Text) AndAlso
           String.IsNullOrWhiteSpace(txtEmail.Text) AndAlso
           String.IsNullOrWhiteSpace(txtPhone.Text) AndAlso
           String.IsNullOrWhiteSpace(txtCustId.Text) Then
            MessageBox.Show("Please provide at least one search condition.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim customers As New List(Of CrmCustomer)

        'search FFL
        Try
            Dim crmFflSearchRequestObj As CrmFflSearchRequest = New CrmFflSearchRequest()
            crmFflSearchRequestObj.englishFullName = txtEngName.Text.Trim
            crmFflSearchRequestObj.chineseFullName = txtChiName.Text.Trim
            crmFflSearchRequestObj.hkIdPassport = txtHkid.Text.Trim
            crmFflSearchRequestObj.email = txtEmail.Text.Trim
            crmFflSearchRequestObj.phone = txtPhone.Text.Trim
            crmFflSearchRequestObj.updateUser = txtUpdUsr.Text.Trim
            Dim crmFflSearchResultObj As CrmFflSearchResult = searchFfl(crmFflSearchRequestObj)
            If crmFflSearchResultObj.fflDetails IsNot Nothing Then
                Dim fflCustomers As List(Of CrmCustomer) = convertDisplayCustomerFfl(crmFflSearchResultObj.fflDetails)
                customers.AddRange(fflCustomers)
            End If
        Catch ex As Exception
            'MsgBox("Error in search FFL")
        End Try
        'Dim fflCustomers As List(Of CrmCustomer) = convertDisplayCustomerFfl(crmFflSearchResultObj.fflDetails)
        'customers.AddRange(fflCustomers)

        'search Life
        Try
            Dim crsLifeSearchRequestObj As CrmLifeSearchRequest = New CrmLifeSearchRequest()
            Dim lifeCustomers As List(Of CrmCustomer) = searchLifeCustomer(cbCom.SelectedItem.ToString, txtEngName.Text, txtChiName.Text, txtHkid.Text, txtEmail.Text, txtPhone.Text, txtCustId.Text)
            If lifeCustomers IsNot Nothing Then
                customers.AddRange(lifeCustomers)
            End If
        Catch ex As Exception
            'MsgBox("Error in search Life")
        End Try
        'customers.AddRange(lifeCustomers)

        'search Prospect
        Try
            Dim crmProspectSearchRequestObj As CrmProspectSearchRequest = New CrmProspectSearchRequest
            crmProspectSearchRequestObj.customerId = txtCustId.Text.Trim
            crmProspectSearchRequestObj.englishFullName = txtEngName.Text.Trim
            crmProspectSearchRequestObj.phone = txtPhone.Text.Trim
            crmProspectSearchRequestObj.email = txtEmail.Text.Trim
            crmProspectSearchRequestObj.hkId = txtHkid.Text.Trim
            crmProspectSearchRequestObj.chineseFullName = txtChiName.Text.Trim
            crmProspectSearchRequestObj.updateUser = txtUpdUsr.Text.Trim
            crmProspectSearchRequestObj.source = "" 'cbProspect.SelectedItem.ToString
            Dim crmProspectResultObj As CrmProspectSearchResult = searchProspect(crmProspectSearchRequestObj)
            If crmProspectResultObj IsNot Nothing Then
                Dim prospectCustomer As List(Of CrmCustomer) = convertDisplayCustomerProspect(crmProspectResultObj.ProspectDetails)
                customers.AddRange(prospectCustomer)
            End If
        Catch ex As Exception
            'MsgBox("Error in search Prospect")
        End Try
        'Dim prospectCustomer As List(Of CrmCustomer) = convertDisplayCustomerProspect(crmProspectResultObj.ProspectDetails)
        'customers.AddRange(prospectCustomer)

        dgvCust.DataSource = customers
    End Sub
    Private Function searchFfl(request As CrmFflSearchRequest) As CrmFflSearchResult
        Try
            Dim queryString As String = JsonConvert.SerializeObject(request)
            Dim responseString As String = UtilityAPI(sApiBaseUrl + sSearchFfl, queryString, sApiKey)
            Return JsonConvert.DeserializeObject(Of CrmFflSearchResult)(responseString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function updateFfl(request As CrmFflUpdateRequest) As CrmUpdateResult
        Dim queryString As String = JsonConvert.SerializeObject(request)
        Dim responseString As String = UtilityAPI(sApiBaseUrl + sUpdateFfl, queryString, sApiKey)
        Return JsonConvert.DeserializeObject(Of CrmUpdateResult)(responseString)
    End Function

    Private Function searchLife(request As CrmLifeSearchRequest) As CrmLifeSearchResult
        Dim queryString As String = JsonConvert.SerializeObject(request)
        Dim responseString As String = UtilityAPI(sApiBaseUrl + sSearchLife, queryString, sApiKey)
        Return JsonConvert.DeserializeObject(Of CrmLifeSearchResult)(responseString)
    End Function
    Public Function getLifeDetail(request As CrmLifeDetailRequest) As CrmLifeDetailResult
        Dim queryString As String = JsonConvert.SerializeObject(request)
        Dim responseString As String = UtilityAPI(sApiBaseUrl + sGetLifeDetail, queryString, sApiKey)
        Return JsonConvert.DeserializeObject(Of CrmLifeDetailResult)(responseString)
    End Function
    Public Function updateLife(request As CrmLifeUpdateRequest) As CrmUpdateResult
        Dim queryString As String = JsonConvert.SerializeObject(request)
        Dim responseString As String = UtilityAPI(sApiBaseUrl + sUpdateLife, queryString, sApiKey)
        Return JsonConvert.DeserializeObject(Of CrmUpdateResult)(responseString)
    End Function

    Private Sub dgvCust_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCust.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim datasource As List(Of CrmCustomer) = CType(dgvCust.DataSource, List(Of CrmCustomer))
            Dim selectedObject As CrmCustomer = datasource(e.RowIndex)
            UclCrmOptInOutDtl1.crmCustomer = selectedObject
            UclCrmOptInOutDtl1.showCustomer()
            UclCrmOptInOutDtl1.Visible = True
        End If
    End Sub

    Private Function searchProspect(request As CrmProspectSearchRequest) As CrmProspectSearchResult
        Try
            Dim queryString As String = JsonConvert.SerializeObject(request)
            Dim responseString As String = UtilityAPI(sApiBaseUrl + sSearchProspect, queryString, sApiKey)
            Return JsonConvert.DeserializeObject(Of CrmProspectSearchResult)(responseString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function updateProspect(request As CrmProspectUpdateRequest) As CrmUpdateResult
        Dim queryString As String = JsonConvert.SerializeObject(request)
        Dim responseString As String = UtilityAPI(sApiBaseUrl + sUpdateProspect, queryString, sApiKey)
        Return JsonConvert.DeserializeObject(Of CrmUpdateResult)(responseString)
    End Function

    Public Shared Function UtilityAPI(ByVal apiUrl As String, ByVal queryString As String, Optional ByVal apiKey As String = "") As String

        Dim responseString As String = String.Empty

        If Not String.IsNullOrEmpty(apiUrl) And Not String.IsNullOrEmpty(queryString) Then
            Try
                Dim httpWebRequest As HttpWebRequest = WebRequest.Create(apiUrl)

                If Not String.IsNullOrEmpty(apiKey) Then
                    httpWebRequest.Headers("ApiKey") = apiKey
                    httpWebRequest.Headers("apikey") = apiKey
                End If

                httpWebRequest.Method = "POST"
                Dim bt As Byte() = System.Text.Encoding.UTF8.GetBytes(queryString)
                httpWebRequest.ContentType = "application/json"
                httpWebRequest.ContentLength = bt.Length
                httpWebRequest.Timeout = 300000
                httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials

                Dim requestStream As Stream = httpWebRequest.GetRequestStream()
                requestStream.Write(bt, 0, bt.Length)
                requestStream.Close()
                Using httpResponse As HttpWebResponse = TryCast(httpWebRequest.GetResponse(), HttpWebResponse)

                    If httpResponse.StatusCode <> HttpStatusCode.OK And httpResponse.StatusCode <> HttpStatusCode.Created Then
                        Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", httpResponse.StatusCode, httpResponse.StatusDescription))
                    End If
                    Dim responseStream As Stream = httpResponse.GetResponseStream()
                    Dim streamReader As New StreamReader(responseStream)
                    responseString = streamReader.ReadToEnd()

                End Using
            Catch ex As WebException
                Using httpResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)

                    If httpResponse.StatusCode = HttpStatusCode.BadRequest Then
                        Dim responseStream As Stream = httpResponse.GetResponseStream()
                        Dim streamReader As New StreamReader(responseStream)
                        responseString = streamReader.ReadToEnd()
                        Return responseString
                    End If

                End Using
                Throw
            Catch ex As Exception
                Throw
            End Try

        End If
        Return responseString
    End Function

    Public Shared Function GetApiResponse(ByVal apiUrl As String, Optional ByVal apiKey As String = "") As String
        Dim responseString As String = String.Empty

        If Not String.IsNullOrEmpty(apiUrl) Then
            Try
                Dim httpWebRequest As HttpWebRequest = WebRequest.Create(apiUrl)

                If Not String.IsNullOrEmpty(apiKey) Then
                    httpWebRequest.Headers("ApiKey") = apiKey
                End If

                httpWebRequest.Method = "GET"
                httpWebRequest.ContentType = "application/json"
                httpWebRequest.Timeout = 300000
                httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials

                Using httpResponse As HttpWebResponse = TryCast(httpWebRequest.GetResponse(), HttpWebResponse)
                    If httpResponse.StatusCode <> HttpStatusCode.OK Then
                        Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", httpResponse.StatusCode, httpResponse.StatusDescription))
                    End If

                    Dim responseStream As Stream = httpResponse.GetResponseStream()
                    Dim streamReader As New StreamReader(responseStream)
                    responseString = streamReader.ReadToEnd()
                End Using
            Catch ex As WebException
                Using httpResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                    If httpResponse.StatusCode = HttpStatusCode.BadRequest Then
                        Dim responseStream As Stream = httpResponse.GetResponseStream()
                        Dim streamReader As New StreamReader(responseStream)
                        responseString = streamReader.ReadToEnd()
                        Return responseString
                    End If
                End Using
                Throw
            Catch ex As Exception
                Throw
            End Try
        End If

        Return responseString
    End Function

    Private Function convertDisplayCustomerFfl(fflDetails As List(Of Ffldetail)) As List(Of CrmCustomer)
        convertDisplayCustomerFfl = New List(Of CrmCustomer)
        Dim ffldetailObj As Ffldetail
        For Each ffldetailObj In fflDetails
            convertDisplayCustomerFfl.Add(New CrmCustomer With {
                .type = "FFL",
                .id = ffldetailObj.id,
                .customerIdentity = "",
                .companyCode = "",
                .datasource = ffldetailObj.datasource,
                .businessPartner = ffldetailObj.businessPartner,
                .productName = ffldetailObj.productName,
                .productPlanName = ffldetailObj.productPlanName,
                .policyNumber = ffldetailObj.policyNumber,
                .lastName = ffldetailObj.lastName,
                .givenName = ffldetailObj.givenName,
                .chineseName = ffldetailObj.chineseName,
                .title = ffldetailObj.title,
                .gender = ffldetailObj.gender,
                .dob = ffldetailObj.dob,
                .hkid = ffldetailObj.hkid,
                .hkidCountry = ffldetailObj.hkidCountry,
                .addressLine1 = ffldetailObj.addressLine1,
                .addressLine2 = ffldetailObj.addressLine2,
                .addressLine3 = ffldetailObj.addressLine3,
                .district = ffldetailObj.district,
                .mobileNumCountryCode = ffldetailObj.mobileNumCountryCode,
                .mobileNum = ffldetailObj.mobileNum,
                .email = ffldetailObj.email,
                .emailType = ffldetailObj.emailType,
                .optOut = ffldetailObj.optOut,
                .optOut3P = "",
                .optOut3rdPartyDate = "",
                .optOutEmail = "",
                .optOutDmail = "",
                .optOutTelemarketing = "",
                .optOutSMS = "",
                .optOutWhatsapp = "",
                .optOutStartDate = ffldetailObj.optOutStartDate,
                .updateDateTime = ffldetailObj.updateDateTime,
                .updatedBy = ffldetailObj.updatedBy,
                .updateApplication = ffldetailObj.updateApplication,
                .remark = ""
                                          })
        Next
    End Function
    Private Function convertDisplayCustomerLife(lifeSearchResults As List(Of CrmLifeSearchResult_SearchResult)) As List(Of CrmCustomer)
        convertDisplayCustomerLife = New List(Of CrmCustomer)
        Dim lifeSearchResultObj As CrmLifeSearchResult_SearchResult
        For Each lifeSearchResultObj In lifeSearchResults
            convertDisplayCustomerLife.Add(New CrmCustomer With {
                .type = "LIFE",
                .id = lifeSearchResultObj.customerId,
                .customerIdentity = lifeSearchResultObj.customerIdentity,
                .companyCode = lifeSearchResultObj.companyCode,
                .datasource = "",
                .businessPartner = "",
                .productName = "",
                .productPlanName = "",
                .policyNumber = "",
                .lastName = "",'NameSuffix
                .givenName = "",'FirstName
                .chineseName = "",'ChiLstNm + ChiFstNm
                .title = "",'NamePrefix
                .gender = "",'Gender
                .dob = "",'DateOfBirth
                .hkid = "",'GovernmentIDCard
                .hkidCountry = "",
                .addressLine1 = "",
                .addressLine2 = "",
                .addressLine3 = "",
                .district = "",
                .mobileNumCountryCode = "",
                .mobileNum = "",'PhoneMobile
                .email = "",'EmailAddr
                .emailType = "",
                .optOut = lifeSearchResultObj.optOut,
                .optOut3P = lifeSearchResultObj.optOut3P,
                .optOut3rdPartyDate = "",
                .optOutEmail = lifeSearchResultObj.optOutEmail,
                .optOutDmail = lifeSearchResultObj.optOutDmail,
                .optOutTelemarketing = lifeSearchResultObj.optOutTelemarketing,
                .optOutSMS = lifeSearchResultObj.optOutSMS,
                .optOutWhatsapp = lifeSearchResultObj.optOutWhatsapp,
                .optOutStartDate = "",
                .updateDateTime = lifeSearchResultObj.updateDateTime,
                .updatedBy = lifeSearchResultObj.updateUser,
                .updateApplication = "",
                .remark = ""
                                          })
        Next
    End Function
    Private Function convertDisplayCustomerProspect(prospectSearchResults As List(Of CrmProspectSearchResult_Prospectdetails)) As List(Of CrmCustomer)
        convertDisplayCustomerProspect = New List(Of CrmCustomer)
        Dim prospectSearchResultObj As CrmProspectSearchResult_Prospectdetails
        For Each prospectSearchResultObj In prospectSearchResults
            convertDisplayCustomerProspect.Add(New CrmCustomer With {
                .type = "Prospect",
                .id = prospectSearchResultObj.sequenceID,
                .customerIdentity = prospectSearchResultObj.customerID,
                .companyCode = "",
                .datasource = prospectSearchResultObj.source,
                .businessPartner = "",
                .productName = "",
                .productPlanName = "",
                .policyNumber = "",
                .lastName = "",
                .givenName = prospectSearchResultObj.customerName,
                .chineseName = prospectSearchResultObj.customerChiName,
                .title = "",
                .gender = "",
                .dob = "",
                .hkid = prospectSearchResultObj.hkid,
                .hkidCountry = "",
                .addressLine1 = "",
                .addressLine2 = "",
                .addressLine3 = "",
                .district = "",
                .mobileNumCountryCode = "",
                .mobileNum = prospectSearchResultObj.customerPhone,
                .email = prospectSearchResultObj.customerEmail,
                .emailType = "",
                .optOut = prospectSearchResultObj.optOut,
                .optOut3P = prospectSearchResultObj.optOut3rdParty,
                .optOut3rdPartyDate = prospectSearchResultObj.optOut3rdPartyDate,
                .optOutEmail = "",
                .optOutDmail = "",
                .optOutTelemarketing = "",
                .optOutSMS = "",
                .optOutWhatsapp = "",
                .optOutStartDate = prospectSearchResultObj.optOutDate,
                .updateDateTime = prospectSearchResultObj.updateDate,
                .updatedBy = prospectSearchResultObj.updateUser,
                .updateApplication = "",
                .remark = prospectSearchResultObj.remark
                                          })
        Next
    End Function
    Private Function getLifeCustomer(customers As List(Of CrmCustomer)) As List(Of CrmCustomer)
        Dim customerObj As CrmCustomer
        Dim dsTmp As New DataSet
        Dim sComCode As String = "HK"
        For Each customerObj In customers
            dsTmp = New DataSet
            If "Bermuda".Equals(customerObj.companyCode) Then
                sComCode = "HK"
            ElseIf "Assurance".Equals(customerObj.companyCode) Then
                sComCode = "LAC"
            End If
            dsTmp = APIServiceBL.CallAPIBusi(sComCode, "GET_CUST", New Dictionary(Of String, String)() From {{"Customerid", customerObj.id}})
            customerObj.lastName = dsTmp.Tables(0).Rows(0)("NameSuffix").ToString
            customerObj.givenName = dsTmp.Tables(0).Rows(0)("FirstName").ToString
            customerObj.chineseName = dsTmp.Tables(0).Rows(0)("ChiLstNm").ToString & " " & dsTmp.Tables(0).Rows(0)("ChiFstNm").ToString
            customerObj.title = dsTmp.Tables(0).Rows(0)("NamePrefix").ToString
            customerObj.gender = dsTmp.Tables(0).Rows(0)("Gender").ToString
            customerObj.dob = dsTmp.Tables(0).Rows(0)("DateOfBirth").ToString
            customerObj.hkid = dsTmp.Tables(0).Rows(0)("GovernmentIDCard").ToString
            customerObj.mobileNum = dsTmp.Tables(0).Rows(0)("PhoneMobile").ToString
            customerObj.email = dsTmp.Tables(0).Rows(0)("EmailAddr").ToString
        Next
        Return customers
    End Function

    Private Function searchLifeCustomer(sCom As String, sName As String, sChiName As String, sHkid As String, sEmail As String, sPhone As String, sCustomerID As String) As List(Of CrmCustomer)
        Dim customerObjs As New List(Of CrmCustomer)
        Dim dsResult, dsTmp, dsTmpHK, dsTmpAsur As New DataSet
        Dim sComCode As String = "HK"

        Dim searchParams As New Dictionary(Of String, String)()
        If Not String.IsNullOrWhiteSpace(sName) Then searchParams.Add("strName", sName)
        If Not String.IsNullOrWhiteSpace(sChiName) Then searchParams.Add("strChiName", sChiName)
        If Not String.IsNullOrWhiteSpace(sHkid) Then searchParams.Add("strHkid", sHkid)
        If Not String.IsNullOrWhiteSpace(sEmail) Then searchParams.Add("strEmail", sEmail)
        If Not String.IsNullOrWhiteSpace(sPhone) Then searchParams.Add("strPhone", sPhone)
        If Not String.IsNullOrWhiteSpace(sCustomerID) Then searchParams.Add("strCustomerID", sCustomerID)

        If "Bermuda".Equals(sCom) Then
            'dsTmp = APIServiceBL.CallAPIBusi("HK", "CRM_Search_Customer", New Dictionary(Of String, String)() From {
            '                                 {"strName", sName},
            '                                 {"strChiName", sChiName},
            '                                 {"strHkid", sHkid},
            '                                 {"strEmail", sEmail},
            '                                 {"strPhone", sPhone},
            '                                 {"strCustomerID", sCustomerID}
            '                                 })
            dsTmp = APIServiceBL.CallAPIBusi("HK", "CRM_Search_Customer", searchParams)
            dsResult = New DataSet
            dsResult.Tables.Add(dsTmp.Tables(0).Copy)
            Dim colCom As New DataColumn("companyCode")
            colCom.DefaultValue = "Bermuda"
            dsResult.Tables(0).Columns.Add(colCom)
        ElseIf "Assurance".Equals(sCom) Then
            'dsTmp = APIServiceBL.CallAPIBusi("LAC", "CRM_Search_Customer", New Dictionary(Of String, String)() From {
            '                                 {"strName", sName},
            '                                 {"strChiName", sChiName},
            '                                 {"strHkid", sHkid},
            '                                 {"strEmail", sEmail},
            '                                 {"strPhone", sPhone},
            '                                 {"strCustomerID", sCustomerID}
            '                                 })
            dsTmp = APIServiceBL.CallAPIBusi("LAC", "CRM_Search_Customer", searchParams)
            dsResult = New DataSet
            dsResult.Tables.Add(dsTmp.Tables(0).Copy)
            Dim colCom As New DataColumn("companyCode")
            colCom.DefaultValue = "Assurance"
            dsResult.Tables(0).Columns.Add(colCom)
        ElseIf "All".Equals(sCom) Then
            'dsTmp = APIServiceBL.CallAPIBusi("HK", "CRM_Search_Customer", New Dictionary(Of String, String)() From {
            '                                 {"strName", sName},
            '                                 {"strChiName", sChiName},
            '                                 {"strHkid", sHkid},
            '                                 {"strEmail", sEmail},
            '                                 {"strPhone", sPhone},
            '                                 {"strCustomerID", sCustomerID}
            '                                 })
            dsTmpHK = APIServiceBL.CallAPIBusi("HK", "CRM_Search_Customer", searchParams)
            Dim colCom As New DataColumn("companyCode")
            colCom.DefaultValue = "Bermuda"
            dsTmpHK.Tables(0).Columns.Add(colCom)
            dsResult = New DataSet
            dsResult.Tables.Add(dsTmpHK.Tables(0).Copy)
            dsTmpAsur = APIServiceBL.CallAPIBusi("LAC", "CRM_Search_Customer", New Dictionary(Of String, String)() From {
                                             {"strName", sName},
                                             {"strChiName", sChiName},
                                             {"strHkid", sHkid},
                                             {"strEmail", sEmail},
                                             {"strPhone", sPhone},
                                             {"strCustomerID", sCustomerID}
                                             })
            colCom = New DataColumn("companyCode")
            colCom.DefaultValue = "Assurance"
            dsTmpAsur.Tables(0).Columns.Add(colCom)
            dsResult.Tables(0).Merge(dsTmpAsur.Tables(0).Copy)
        End If
        Dim row As DataRow
        For Each row In dsResult.Tables(0).Rows
            Dim searchLifeRequestObj As New CrmLifeSearchRequest
            searchLifeRequestObj.customerId = row("customerid").ToString
            searchLifeRequestObj.companyCode = "LIFE"
            Dim CrmLifeSearchResultObj As CrmLifeSearchResult = searchLife(searchLifeRequestObj)
            Dim customerObj As New CrmCustomer
            customerObj.type = "LIFE"
            customerObj.id = row("customerid").ToString
            customerObj.customerIdentity = CrmLifeSearchResultObj.searchResults(0).customerIdentity
            customerObj.companyCode = row("companyCode").ToString
            customerObj.datasource = ""
            customerObj.businessPartner = ""
            customerObj.productName = ""
            customerObj.productPlanName = ""
            customerObj.policyNumber = ""
            customerObj.lastName = row("NameSuffix").ToString
            customerObj.givenName = row("FirstName").ToString
            customerObj.chineseName = row("ChiLstNm").ToString & " " & row("ChiFstNm").ToString
            customerObj.title = row("NamePrefix").ToString
            customerObj.gender = row("Gender").ToString
            customerObj.dob = row("DateOfBirth").ToString
            customerObj.hkid = row("GovernmentIDCard").ToString
            customerObj.hkidCountry = ""
            customerObj.addressLine1 = ""
            customerObj.addressLine2 = ""
            customerObj.addressLine3 = ""
            customerObj.district = ""
            customerObj.mobileNumCountryCode = ""
            customerObj.mobileNum = row("PhoneMobile").ToString
            customerObj.email = row("EmailAddr").ToString
            customerObj.emailType = ""
            customerObj.optOut = CrmLifeSearchResultObj.searchResults(0).optOut
            customerObj.optOut3P = CrmLifeSearchResultObj.searchResults(0).optOut3P
            customerObj.optOut3rdPartyDate = ""
            customerObj.optOutEmail = CrmLifeSearchResultObj.searchResults(0).optOutEmail
            customerObj.optOutDmail = CrmLifeSearchResultObj.searchResults(0).optOutDmail
            customerObj.optOutTelemarketing = CrmLifeSearchResultObj.searchResults(0).optOutTelemarketing
            customerObj.optOutSMS = CrmLifeSearchResultObj.searchResults(0).optOutSMS
            customerObj.optOutWhatsapp = CrmLifeSearchResultObj.searchResults(0).optOutWhatsapp
            customerObj.optOutStartDate = ""
            customerObj.updateDateTime = CrmLifeSearchResultObj.searchResults(0).updateDateTime
            customerObj.updatedBy = CrmLifeSearchResultObj.searchResults(0).updateUser
            customerObj.updateApplication = ""
            customerObj.remark = ""
            customerObjs.Add(customerObj)
        Next
        Return customerObjs
    End Function
End Class
Public Class CrmCustomer
    Public Property type As String '"FFL","LIFE","Prospect"
    Public Property id As String ' (FFL)		custId (Life)		sequenceID (Prospect)
    Public Property customerIdentity As String '(Life)		customerID (Prospect)	
    Public Property companyCode As String '(Life)
    Public Property datasource As String '(FFL)		source (Prospect)
    Public Property businessPartner As String '(FFL)
    Public Property productName As String '(FFL)
    Public Property productPlanName As String '(FFL)
    Public Property policyNumber As String '(FFL)
    Public Property lastName As String '(FFL)
    Public Property givenName As String '(FFL)		customerName (Prospect)
    Public Property chineseName As String '(FFL)		customerChiName (Prospect)
    Public Property title As String '(FFL)
    Public Property gender As String '(FFL)
    Public Property dob As String '(FFL)
    Public Property hkid As String '(FFL)		hkid (Prospect)
    Public Property hkidCountry As String '(FFL)
    Public Property addressLine1 As String '(FFL)
    Public Property addressLine2 As String '(FFL)
    Public Property addressLine3 As String '(FFL)
    Public Property district As String '(FFL)
    Public Property mobileNumCountryCode As String '(FFL)
    Public Property mobileNum As String '(FFL)		customerPhone (Prospect)
    Public Property email As String '(FFL)		customerEmail (Prospect)
    Public Property emailType As String '(FFL)
    Public Property optOut As String '(FFL)		optOut (Life)		optOut (Prospect)
    Public Property optOut3P As String '(Life)		optOut3rdParty (Prospect)
    Public Property optOut3rdPartyDate As String '(Prospect)
    Public Property optOutEmail As String '(Life)
    Public Property optOutDmail As String '(Life)
    Public Property optOutTelemarketing As String '(Life)
    Public Property optOutSMS As String '(Life)
    Public Property optOutWhatsapp As String '(Life)
    Public Property optOutStartDate As String '(FFL)		optOutDate (Prospect)
    Public Property updateDateTime As String '(FFL)		updateDateTime (Life)		updateDate (Prospect)
    Public Property updatedBy As String '(FFL)		updateUser (Life)		updateUser (Prospect)
    Public Property updateApplication As String '(FFL)
    Public Property remark As String '(Prospect)
    Public Property latestStatus As Lateststatus '(Life)
    Public Property history() As History '(Life)
    Public Property lifeDetail As CrmLifeDetailResult_Lifedetail
End Class

Public Class CrmFflSearchRequest
    Public Property id As String = ""
    Public Property hkIdPassport As String
    Public Property englishFullName As String
    Public Property chineseFullName As String
    Public Property phone As String
    Public Property email As String
    Public Property updateUser As String
End Class
Public Class Message
    Public Property success As Boolean
    Public Property resultCode As Integer
    Public Property description As String
    Public Property dateTime As String
End Class
Public Class CrmFflSearchResult
    Public Property message As Message
    Public Property fflDetails As List(Of Ffldetail)
    Public Class Ffldetail
        Public Property id As String
        Public Property datasource As String
        Public Property businessPartner As String
        Public Property productName As String
        Public Property productPlanName As String
        Public Property policyNumber As String
        Public Property lastName As String
        Public Property givenName As String
        Public Property chineseName As String
        Public Property title As String
        Public Property gender As String
        Public Property dob As String
        Public Property hkid As String
        Public Property hkidCountry As String
        Public Property addressLine1 As String
        Public Property addressLine2 As String
        Public Property addressLine3 As String
        Public Property district As String
        Public Property mobileNumCountryCode As String
        Public Property mobileNum As String
        Public Property email As String
        Public Property emailType As String
        Public Property optOut As String
        Public Property optOutStartDate As String
        Public Property updateDateTime As String
        Public Property updatedBy As String
        Public Property updateApplication As String
    End Class

End Class
Public Class CrmLifeSearchResult
    Public Property message As Message
    Public Property searchResults As List(Of CrmLifeSearchResult_SearchResult)
    Public Class CrmLifeSearchResult_SearchResult
        Public Property customerId As String
        Public Property customerIdentity As String
        Public Property companyCode As String
        Public Property optOut As String
        Public Property optOut3P As String
        Public Property optOutEmail As String
        Public Property optOutDmail As String
        Public Property optOutTelemarketing As String
        Public Property optOutSMS As String
        Public Property optOutWhatsapp As String
        Public Property updateUser As String
        Public Property updateDateTime As String
    End Class
End Class
Public Class CrmFflUpdateRequest
    Public Property id As Integer
    Public Property optOut As String
    Public Property updateUser As String
End Class
Public Class CrmUpdateResult
    Public Property message As Message
End Class
Public Class CrmProspectResult
    Public Property message As Message
    Public Property sourceList As Dictionary(Of String, String)

End Class
Public Class CrmLifeSearchRequest
    Public Property customerId As String
    Public Property companyCode As String
    Public Property updateUser As String
End Class
Public Class CrmLifeDetailRequest
    Public Property customerId As String
    Public Property companyCode As String
End Class
Public Class CrmLifeDetailResult
    Public Property message As Message
    Public Property lifeDetail As CrmLifeDetailResult_Lifedetail
    Public Class CrmLifeDetailResult_Lifedetail
        Public Property latestStatus As Lateststatus
        Public Property history As List(Of History)

    End Class
    Public Class Lateststatus
        Public Property custId As String
        Public Property customerIdentity As String
        Public Property companyCode As String
        Public Property optOut As String
        Public Property optOut3P As String
        Public Property optOutEmail As String
        Public Property optOutDmail As String
        Public Property optOutTelemarketing As String
        Public Property optOutSMS As String
        Public Property optOutWhatsapp As String
        Public Property updateUser As String
        Public Property updateDateTime As String
    End Class
    Public Class History
        Public Property optOut As String
        Public Property optOut3P As String
        Public Property optOutEmail As String
        Public Property optOutDmail As String
        Public Property optOutTelemarketing As String
        Public Property optOutSms As String
        Public Property optOutWhatsapp As String
        Public Property companyCode As String
        Public Property updateDateTime As String
        Public Property updateUser As String
        Public Property remark As String
        Public Property updateApplication As String
    End Class
End Class
Public Class CrmProspectSearchRequest
    Public Property sequenceId As String = ""
    Public Property customerId As String
    Public Property englishFullName As String
    Public Property phone As String
    Public Property email As String
    Public Property hkId As String
    Public Property chineseFullName As String
    Public Property source As String
    Public Property updateUser As String
End Class
Public Class CrmProspectSearchResult
    Public Property ProspectDetails As List(Of CrmProspectSearchResult_Prospectdetails)
    Public Property message As Message
    Public Class CrmProspectSearchResult_Prospectdetails
        Public Property sequenceID As String
        Public Property customerID As String
        Public Property customerName As String
        Public Property customerPhone As String
        Public Property customerEmail As String
        Public Property customerChiName As String
        Public Property optOut As String
        Public Property optOutDate As String
        Public Property optOut3rdParty As String
        Public Property optOut3rdPartyDate As String
        Public Property source As String
        Public Property updateUser As String
        Public Property updateDate As String
        Public Property remark As String
        Public Property hkid As String
    End Class

End Class
Public Class CrmProspectUpdateRequest
    Public Property sequenceId As String
    Public Property optOut As String
    Public Property optOut3P As String
    Public Property remark As String
    Public Property updateUser As String
End Class
Public Class CrmLifeUpdateRequest
    Public Property customerId As String
    Public Property companyCode As String
    Public Property optOut As String
    Public Property optOut3Party As String
    Public Property optOutEmail As String
    Public Property optOutDMail As String
    Public Property optOutTelemarketing As String
    Public Property optOutSMS As String
    Public Property optOutWhatsapp As String
    Public Property remark As String
    Public Property updateUser As String
End Class



