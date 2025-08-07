Imports CRS_Component.APICall
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmEServiceOptInOut
    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click

        Try
            CleanUp()
            Dim kongAPIKey As String = System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey")
            If Not txtID.Text Is Nothing And Not cbIDType.SelectedItem Is Nothing Then

                Dim id As String = txtID.Text
                Dim type As String = cbIDType.SelectedItem.ToString()
                Dim tempAPIUrl As String = String.Empty
                Dim api_Url As String = String.Empty
                Dim sJSON As String = String.Empty
                Dim responseObject As JObject = New JObject()

                'check if ODISD exist first
                Dim strCondition = " and ( Hkid = '" & id & "' or PassportNumber = '" & id & "' ) "
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "EBMEMBER_RECORD", New Dictionary(Of String, String)() From {
                        {"condition", strCondition}})

                If Not retDs.Tables Is Nothing Then
                    If retDs.Tables(0).Rows.Count > 0 Then
                        Dim strCustID As String = retDs.Tables(0).Rows(0).Item("CustomerID").ToString()
                        'txtName.Text = retDs.Tables(0).Rows(0).Item("Name").ToString()
                        'strCustID = "21330832"

                        tempAPIUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CommonAPIEndPoint")
                        api_Url = String.Format(tempAPIUrl, "optinws", "getOdsId")

                        Dim GetODISDAPIRequestQuery = New GetODISDAPIRequest(strCustID)

                        sJSON = JsonConvert.SerializeObject(GetODISDAPIRequestQuery)
                        responseObject = CRS_Component.APICallHelper.CallAPIWithResponse(Of JObject)(api_Url, GetODISDAPIRequestQuery, kongAPIKey)
                        Dim optOutFlag As String = responseObject.GetValue("odsId").ToString()

                        If Not String.IsNullOrEmpty(optOutFlag) Then
                            'prompt message ask user go CRM
                            MsgBox("OdsId is existed, please use CRM.")
                            Exit Sub
                        End If
                    End If
                End If

                'Dim tempAPIUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "NBAPIEndPoint")
                'Dim api_Url = "http://hkgjavaappsit01.hk.intranet:8080/optinws-test/getProspectCustomerOptOut"
                tempAPIUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CommonAPIEndPoint")
                api_Url = String.Format(tempAPIUrl, "optinws", "getProspectCustomerOptOut")

                'A123456(7)
                Dim OptInOutAPIRequestquery = New OptInOutAPIRequest(id, type)

                sJSON = JsonConvert.SerializeObject(OptInOutAPIRequestquery)
                responseObject = CRS_Component.APICallHelper.CallAPIWithResponse(Of JObject)(api_Url, OptInOutAPIRequestquery, kongAPIKey)

                Dim msg As JToken = responseObject.GetValue("msg")
                Dim item = JsonConvert.DeserializeObject(Of Message)(msg.ToString(Formatting.None))

                If Not item.IsSystemError Then
                    Dim documentNumber As String = responseObject.GetValue("documentNumber").ToString()
                    If Not String.IsNullOrEmpty(documentNumber) Then
                        Dim documentType As String = responseObject.GetValue("documentType").ToString()
                        Dim optOutFlag As String = responseObject.GetValue("optOutFlag").ToString()
                        Dim updatedBy As String = responseObject.GetValue("updatedBy").ToString()
                        Dim updatedDate As String = responseObject.GetValue("updateDate").ToString()

                        Select Case optOutFlag
                            Case "N"
                                rbOptIn.Checked = True
                            Case "Y"
                                rbOptOut.Checked = True
                            Case Else
                                rbNA.Checked = True
                        End Select

                        If Not retDs.Tables Is Nothing Then
                            If retDs.Tables(0).Rows.Count > 0 Then
                                txtName.Text = retDs.Tables(0).Rows(0).Item("Name").ToString()
                            End If
                        End If

                        txtUpdatedBy.Text = updatedBy
                        txtUpdateDate.Text = updatedDate

                        HandleRB(True)
                    Else
                        ' No Record Found
                        MsgBox("API Message : " & item.message_en, MsgBoxStyle.Information, "")
                    End If
                Else
                    ' alert API Error message
                    MsgBox("API Error : " & item.message_en, MsgBoxStyle.Critical, "Error")
                End If
            Else
                ' alert message 
                MsgBox("Please enter HKID or Passport No. and select ID Type", MsgBoxStyle.Information, "")
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Try
            'Dim api_Url = "http://hkgjavaappsit01.hk.intranet:8080/optinws-test/saveProspectCustomerOptOut"
            Dim tempAPIUrl = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "CommonAPIEndPoint")
            Dim api_Url = String.Format(tempAPIUrl, "optinws", "saveProspectCustomerOptOut")

            Dim id As String = txtID.Text
            Dim type As String = cbIDType.SelectedItem.ToString()
            Dim optOutFlag As String = IIf(rbOptOut.Checked, "Y", "N")
            'A123456(7)
            Dim query = New OptInOutSaveAPIRequest(id, type, optOutFlag, "CRS")

            Dim sJSON As String = JsonConvert.SerializeObject(query)
            'Dim responseObject As JObject = CRS_Component.APICallHelper.CallAPIWithResponse(Of JObject)(api_Url, query)
            Dim kongAPIKey As String = System.Configuration.ConfigurationSettings.AppSettings.Item("KongAPIKey")
            Dim responseObject As JObject = CRS_Component.APICallHelper.CallAPIWithResponse(Of JObject)(api_Url, query, kongAPIKey)

            Dim resultFlag As Boolean = responseObject.GetValue("result")

            If resultFlag Then
                MsgBox("Save Succuss", MsgBoxStyle.Information)
                CleanUp()
                HandleRB(False)
            Else
                Dim msg As JToken = responseObject.GetValue("msg")
                Dim item = JsonConvert.DeserializeObject(Of Message)(msg.ToString(Formatting.None))
                MsgBox("API Error : " & item.message_en, MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        CleanUp()
        HandleRB(False)
    End Sub

    Private Sub HandleRB(ByVal flag As Boolean)
        rbOptIn.Enabled = flag
        rbOptOut.Enabled = flag
        rbNA.Enabled = flag

        If flag Then
            cmdSave.Enabled = True
            cmdCancel.Enabled = True
        Else
            cmdSave.Enabled = False
            cmdCancel.Enabled = False
        End If
    End Sub

    Private Sub CleanUp()
        txtName.Text = String.Empty
        txtUpdatedBy.Text = String.Empty
        txtUpdateDate.Text = String.Empty
        rbOptIn.Checked = False
        rbOptOut.Checked = False
        rbNA.Checked = False
    End Sub

End Class