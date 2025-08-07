Public Class uclCrmOptInOutDtl
    Public crmCustomer As CrmCustomer
    Public frmOptout As frmCrmOptInOut
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Visible = False
        frmOptout.searchCustomer()
    End Sub

    Public Sub showCustomer()
        txtOdsid.Text = ""
        txtProspectSource.Text = crmCustomer.datasource
        txtEngName.Text = crmCustomer.givenName & " " & crmCustomer.lastName
        txtChiName.Text = crmCustomer.chineseName
        txtGender.Text = crmCustomer.gender
        txtIdtype.Text = ""
        txtHkid.Text = crmCustomer.hkid
        txtNationality.Text = ""
        txtDob.Text = crmCustomer.dob
        txtMaritalStatus.Text = ""
        txtEmail.Text = crmCustomer.email
        txtBusinessPhone.Text = ""
        txtMobile.Text = crmCustomer.mobileNum
        txtHomePhone.Text = ""
        txtAddress.Text = crmCustomer.addressLine1 & vbLf & crmCustomer.addressLine2 & vbLf & crmCustomer.addressLine3
        txtCompany.Text = crmCustomer.companyCode

        rbDmOptNa.Checked = True
        If crmCustomer.optOut.Equals("Y") Then
            rbDmOptout.Checked = True
        ElseIf crmCustomer.optOut.Equals("N") Then
            rbDmOptin.Checked = True
        End If

        rb3pOptNa.Checked = True
        If crmCustomer.optOut3P.Equals("Y") Then
            rb3pOptout.Checked = True
        ElseIf crmCustomer.optOut3P.Equals("N") Then
            rb3pOptin.Checked = True
        End If

        txtRemark.Text = crmCustomer.remark
        txtUpdUsr.Text = crmCustomer.updatedBy

        If "LIFE".Equals(crmCustomer.type) Then
            Try
                If crmCustomer.lifeDetail Is Nothing Then
                    crmCustomer.lifeDetail = frmOptout.getLifeDetail(New CrmLifeDetailRequest With {.customerId = crmCustomer.id, .companyCode = "LIFE"}).lifeDetail
                    dgvOptoutHist.DataSource = crmCustomer.lifeDetail.history
                    dgvOptoutHist.Columns("optOutEmail").Visible = False
                    dgvOptoutHist.Columns("optOutDmail").Visible = False
                    dgvOptoutHist.Columns("optOutTelemarketing").Visible = False
                    dgvOptoutHist.Columns("optOutSms").Visible = False
                    dgvOptoutHist.Columns("optOutWhatsapp").Visible = False
                    If crmCustomer.lifeDetail.history.Count > 0 Then
                        txtRemark.Text = crmCustomer.lifeDetail.history(0).remark
                    End If
                End If
            Catch ex As Exception
            End Try
        End If

        If "FFL".Equals(crmCustomer.type) Then
            gbLife3p.Enabled = False
        Else
            gbLife3p.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        showCustomer()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If "FFL".Equals(crmCustomer.type) Then
            Dim crmFflUpdateRequestObj As CrmFflUpdateRequest = New CrmFflUpdateRequest
            crmFflUpdateRequestObj.id = crmCustomer.id
            If rbDmOptin.Checked Then
                crmFflUpdateRequestObj.optOut = "N"
            ElseIf rbDmOptout.Checked Then
                crmFflUpdateRequestObj.optOut = "Y"
            ElseIf rbDmOptNa.Checked Then
                crmFflUpdateRequestObj.optOut = ""
            End If
            crmFflUpdateRequestObj.updateUser = gsUser
            Dim updateResult As CrmUpdateResult = frmOptout.updateFfl(crmFflUpdateRequestObj)
            If updateResult.message.success Then
                MessageBox.Show(updateResult.message.description)
            Else
                MessageBox.Show(updateResult.message.description)
            End If
        ElseIf "LIFE".Equals(crmCustomer.type) Then
            Dim crmLifeUpdateRequestObj As CrmLifeUpdateRequest = New CrmLifeUpdateRequest
            crmLifeUpdateRequestObj.customerId = crmCustomer.id
            crmLifeUpdateRequestObj.companyCode = "LIFE"
            If rbDmOptin.Checked Then
                crmLifeUpdateRequestObj.optOut = "N"
                crmLifeUpdateRequestObj.optOutEmail = "N"
                crmLifeUpdateRequestObj.optOutDMail = "N"
                crmLifeUpdateRequestObj.optOutTelemarketing = "N"
                crmLifeUpdateRequestObj.optOutSMS = "N"
                crmLifeUpdateRequestObj.optOutWhatsapp = "N"
            ElseIf rbDmOptout.Checked Then
                crmLifeUpdateRequestObj.optOut = "Y"
                crmLifeUpdateRequestObj.optOutEmail = "Y"
                crmLifeUpdateRequestObj.optOutDMail = "Y"
                crmLifeUpdateRequestObj.optOutTelemarketing = "Y"
                crmLifeUpdateRequestObj.optOutSMS = "Y"
                crmLifeUpdateRequestObj.optOutWhatsapp = "Y"
            ElseIf rbDmOptNa.Checked Then
                crmLifeUpdateRequestObj.optOut = ""
                crmLifeUpdateRequestObj.optOutEmail = ""
                crmLifeUpdateRequestObj.optOutDMail = ""
                crmLifeUpdateRequestObj.optOutTelemarketing = ""
                crmLifeUpdateRequestObj.optOutSMS = ""
                crmLifeUpdateRequestObj.optOutWhatsapp = ""
            End If
            If rb3pOptin.Checked Then
                crmLifeUpdateRequestObj.optOut3Party = "N"
            ElseIf rb3pOptout.Checked Then
                crmLifeUpdateRequestObj.optOut3Party = "Y"
            ElseIf rb3pOptNa.Checked Then
                crmLifeUpdateRequestObj.optOut3Party = ""
            End If
            crmLifeUpdateRequestObj.remark = txtRemark.Text.Trim
            crmLifeUpdateRequestObj.updateUser = gsUser
            Dim updateResult As CrmUpdateResult = frmOptout.updateLife(crmLifeUpdateRequestObj)
            If updateResult.message.success Then
                MessageBox.Show(updateResult.message.description)
            Else
                MessageBox.Show(updateResult.message.description)
            End If
        ElseIf "Prospect".Equals(crmCustomer.type) Then
            Dim crmProspectUpdateRequestObj As CrmProspectUpdateRequest = New CrmProspectUpdateRequest
            crmProspectUpdateRequestObj.sequenceId = crmCustomer.id
            If rbDmOptin.Checked Then
                crmProspectUpdateRequestObj.optOut = "N"
            ElseIf rbDmOptout.Checked Then
                crmProspectUpdateRequestObj.optOut = "Y"
            ElseIf rbDmOptNa.Checked Then
                crmProspectUpdateRequestObj.optOut = ""
            End If
            If rb3pOptin.Checked Then
                crmProspectUpdateRequestObj.optOut3P = "N"
            ElseIf rb3pOptout.Checked Then
                crmProspectUpdateRequestObj.optOut3P = "Y"
            ElseIf rb3pOptNa.Checked Then
                crmProspectUpdateRequestObj.optOut3P = ""
            End If
            crmProspectUpdateRequestObj.remark = txtRemark.Text.Trim
            crmProspectUpdateRequestObj.updateUser = gsUser
            Dim updateResult As CrmUpdateResult = frmOptout.updateProspect(crmProspectUpdateRequestObj)
            If updateResult.message.success Then
                MessageBox.Show(updateResult.message.description)
            Else
                MessageBox.Show(updateResult.message.description)
            End If
        End If
    End Sub
End Class
