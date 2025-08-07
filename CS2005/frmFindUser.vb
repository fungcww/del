'**************************
'Date : Oct 18, 2023
'Author : Oliver Ou 222834
'Purpose : CRS Enhancement(General Enhance Ph4) Point A-4 merge "Find User Name" tool $/AES_PHW/IPD/AESPHW/FindUserName 
'**************************

Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports System.Configuration

Public Class frmFindUser

#Region "Private Members"
    'Private Shared connStr As String = "Data Source=HKSQLVS1;Initial Catalog=vantive;User ID=com_las_ing;Password=IngLA123"
    'Private Shared url As String = "http://hkgcomprd02.hk.intranet/vpaservice/vpaservice.svc/VPAResetPasswordAndSend"
    'Private Shared connStr As String = "Data Source=HKSQLUAT1;Initial Catalog=ingciwu301;User ID=com_las_ing;Password=IngLA456"
    Private Shared connStr As String = IIf(Not String.IsNullOrEmpty(strCIWConn), strCIWConn, "Data Source=HKSQLUAT1;Initial Catalog=ingciwu301;User ID=com_las_ing;Password=IngLA456")
    'Private Shared url As String = "http://10.10.18.106/VPAService/VPAService.svc/VPAResetPasswordAndSend"
    'Private Shared url As String = "http://whkeaseaaappu01/HKU105_VPAService/VPAService.svc/VPAResetPasswordAndSend"
    Private Shared url As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "VPAService")
    'Private Shared url As String = "http://localhost:43370/VPAService.svc/VPAResetPasswordAndSend"
    Private Shared customerID As String = String.Empty
#End Region

#Region "UI Event Handlers"
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If String.IsNullOrEmpty(txtHKID.Text) AndAlso String.IsNullOrEmpty(txtUsername.Text) AndAlso String.IsNullOrEmpty(txtCustomerID.Text) AndAlso
            MessageBox.Show("No search criteria specified. This may take quite a bit time because of high volume of records." + vbCrLf + vbCrLf + "Are you sure that you want to proceed ?", "Confirm: Search", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        Else
            Cursor = Cursors.WaitCursor
            btnSearch.BeginInvoke(searchAsync)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtHKID.Text = String.Empty
        txtHKID.Focus()
        txtUsername.Text = String.Empty
        txtCustomerID.Text = String.Empty
        dgvUsers.DataSource = Nothing
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
    End Sub

    Private Sub btnGotoResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGotoResetPassword.Click
        Me.Text = "Reset Password"
        pnlSearch.Visible = False
        pnlUpdateEmail.Visible = True
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = False
        If dgvUsers.DataSource IsNot Nothing AndAlso dgvUsers.Rows.Count >= 1 AndAlso
            dgvUsers.SelectedRows.Count > 0 AndAlso Not IsDBNull(dgvUsers.SelectedRows(0).Cells(0).Value) AndAlso
            Not String.IsNullOrEmpty(dgvUsers.SelectedRows(0).Cells(0).Value) Then
            txtUsername2.BackColor = System.Drawing.SystemColors.Info
            txtUsername2.ReadOnly = True
            txtUsername2.Text = IIf(IsDBNull(dgvUsers.SelectedRows(0).Cells(0).Value), String.Empty, dgvUsers.SelectedRows(0).Cells(0).Value)
            txtEmail.Text = IIf(IsDBNull(dgvUsers.SelectedRows(0).Cells(6).Value), String.Empty, dgvUsers.SelectedRows(0).Cells(6).Value)
            customerID = IIf(IsDBNull(dgvUsers.SelectedRows(0).Cells(1).Value), String.Empty, dgvUsers.SelectedRows(0).Cells(1).Value)
            txtEmail.Focus()
        Else
            txtUsername2.BackColor = System.Drawing.SystemColors.Window
            txtUsername2.ReadOnly = False
            txtUsername2.Text = String.Empty
            txtUsername2.Focus()
            txtEmail.Text = String.Empty
        End If
    End Sub

    Private Sub frmFindUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pnlSearch.Parent = Me
        pnlLinkCustomerId.Parent = Me
        pnlUpdateEmail.Parent = Me
        pnlChangeUsername.Parent = Me
        pnlSearch.Visible = True
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = False
        dgvUsers.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        txtHKID.Focus()
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Text = "Find Username"
        pnlSearch.Visible = True
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = False
        dgvUsers.Focus()
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
        'freeze ui inputs
        txtEmail.ReadOnly = False
        btnResetPassword.Enabled = True
        btnClearInput.Enabled = True
    End Sub

    'Update the email id of the specified user, reset the password and send email
    Private Sub btnResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPassword.Click
        Dim frmEmail As String = ConfigurationManager.AppSettings.Item("frmEmail")
        Dim usrName As String = txtUsername2.Text
        Dim emailID As String = txtEmail.Text
        Dim custID As String = txtCustomerID.Text
        Dim fullName As String = String.Empty
        If dgvUsers.SelectedRows.Count > 0 AndAlso Not IsDBNull(dgvUsers.SelectedRows(0).Cells(5).Value) Then
            fullName = dgvUsers.SelectedRows(0).Cells(5).Value
        End If
        If String.IsNullOrEmpty(usrName) Then
            txtUsername2.Focus()
            lblMessage.Text = "Error: Username missing"
            Return
        End If
        'if user existence not validated yet, validate it before the update
        If Not txtUsername2.ReadOnly AndAlso Not String.IsNullOrEmpty(usrName) Then
            Dim dt As DataTable = dbSearchUser(String.Empty, usrName, String.Empty)
            If Not (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblMessage.Text = "Error: Specified (" + usrName + ") user does not exist"
                Return
            Else
                customerID = dt.Rows(0)("Customer ID")
                fullName = dt.Rows(0)("Full name")
            End If
        End If

        If String.IsNullOrEmpty(frmEmail) Then
            btnBack.Focus()
            lblMessage.Text = "Error: From email not configured"
            Return
        ElseIf String.IsNullOrEmpty(customerID) Then
            btnBack.Focus()
            lblMessage.Text = "Error: Invalid user. CustomerID is missing."
            Return
        ElseIf String.IsNullOrEmpty(emailID) Then
            txtEmail.Focus()
            lblMessage.Text = "Error: email ID missing to which new password to be sent"
            Return
        ElseIf Not IsValidEmailID(emailID) Then
            txtEmail.Focus()
            lblMessage.Text = "Error: Invalid email entered to which new password to be sent"
            Return
        End If

        Dim msg As String = "Please click 'OK' to reset the password of " + vbCrLf
        msg += "the below user and send it to the email id '" + emailID + "'" + vbCrLf
        msg += "Otherwise click 'CANCEL'." + vbCrLf
        msg += "-----------------------------------" + vbCrLf
        msg += "Username:" + usrName + vbCrLf
        msg += "CustomerID:" + customerID + vbCrLf
        msg += "FullName:" + fullName + vbCrLf
        msg += "-----------------------------------"
        If MessageBox.Show(msg, "Confirm: Update email and reset password", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
            Return
        Else
            'freeze ui inputs
            txtEmail.ReadOnly = True
            btnResetPassword.Enabled = False
            btnClearInput.Enabled = False
            btnBack.Focus()
            Try
                Dim reqBody As String = String.Empty
                reqBody = String.Format("{{""userName"": ""{0}"",""fullName"": ""{1}"",""frmEmail"": ""{2}"",""toEmail"": ""{3}"", ""supportUser"": ""{4}""}}", usrName, fullName, frmEmail, emailID, Environment.UserName)
                Dim inputJson As Byte() = Encoding.UTF8.GetBytes(reqBody)
                Dim request As WebRequest = WebRequest.Create(String.Format(url, "VPAResetPasswordAndSend"))
                request.ContentLength = inputJson.Length
                request.Method = "POST"
                request.ContentType = "application/json"
                request.Credentials = CredentialCache.DefaultCredentials

                Dim inputStream As Stream = request.GetRequestStream()
                inputStream.Write(inputJson, 0, inputJson.Length)
                inputStream.Close()

                Dim response As WebResponse = request.GetResponse()
                Dim outputStream As Stream = response.GetResponseStream()
                Dim sr As StreamReader = New StreamReader(outputStream)
                Dim outputJson As String = sr.ReadToEnd()
                response.Close()
                If outputJson.Contains("""status"":""SUCCESS""") Then
                    lblMessage.Text = "Password reset and sent to the specified email successfully"
                Else
                    lblMessage.Text = "Error: Password reset failed."
                End If

            Catch ex As Exception
                lblMessage.Text = "Error: Password reset failed."
                HandleGlobalException(ex, "Error: Password reset failed." & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtCustomerID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            lblMessage.Text = "CustomerID accepts only numbers !"
        Else
            lblMessage.Text = String.Empty
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        lblMessage.Text = String.Empty
    End Sub


    Private Sub btnGoLinkCustomerId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoLinkCustomerId.Click
        txtCustomerID.Visible = False
        Dim comboSource As New Dictionary(Of String, String)()
        If dgvUsers.DataSource IsNot Nothing AndAlso dgvUsers.Rows.Count >= 1 AndAlso
            dgvUsers.SelectedRows.Count > 0 Then
            Dim username As String = IIf(IsDBNull(dgvUsers.SelectedRows(0).Cells(0).Value), String.Empty, dgvUsers.SelectedRows(0).Cells(0).Value)
            If (Not String.IsNullOrEmpty(username)) Then
                txtUsername3.Text = username
            Else
                txtUsername3.Text = String.Empty
            End If
            Dim custId As String
            Dim custIdAndCompanyName As String
            For Each row As DataGridViewRow In dgvUsers.Rows
                custId = Convert.ToString(row.Cells(1).Value)
                custIdAndCompanyName = custId + " - " + Convert.ToString(row.Cells(7).Value)
                If Not comboSource.ContainsKey(custIdAndCompanyName) Then
                    'comboSource.Add(custId, custId)
                    comboSource.Add(custIdAndCompanyName, custIdAndCompanyName)
                End If
            Next
        Else
            txtUsername3.Text = String.Empty
            txtCustomerId3.Visible = True
            lblCustomerId3.Visible = True
        End If
        comboSource.Add("Others", "Others")
        cbxCustomerId.DataSource = New BindingSource(comboSource, Nothing)
        cbxCustomerId.DisplayMember = "Value"
        cbxCustomerId.ValueMember = "Key"
        cbxCustomerId.SelectedIndex = 0
        txtUsername3.Focus()
        Me.Text = "Link Customer ID"
        pnlSearch.Visible = False
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = True
        pnlChangeUsername.Visible = False
        lblMessage.Text = String.Empty
    End Sub


    Private Sub btnClearInput3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInput3.Click
        txtCustomerId3.Text = String.Empty
        txtUsername3.Text = String.Empty
    End Sub

    Private Sub btnBack3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack3.Click
        Me.Text = "Find Username"
        pnlSearch.Visible = True
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = False
        dgvUsers.Focus()
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
        'freeze ui inputs
        txtEmail.ReadOnly = False
        btnResetPassword.Enabled = True
        btnClearInput.Enabled = True
        txtCustomerID.Visible = True
    End Sub


    'Private Function IsValidCustomerCompanyName(ByVal currentUsername As String, ByVal targetCustomerCompanyName As String) As Boolean
    '    Dim isValid As Boolean = False
    '    Try
    '        If Not String.IsNullOrWhiteSpace(targetCustomerCompanyName) Then

    '            Dim currentCompanyName As String = String.Empty
    '            Dim dt As DataTable = dbSearchUserByUsername(currentUsername.Trim())

    '            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '                currentCompanyName = dt.Rows(0)("Company Name")

    '                If Not String.IsNullOrWhiteSpace(currentCompanyName) AndAlso currentCompanyName.Equals(targetCustomerCompanyName, StringComparison.OrdinalIgnoreCase) Then
    '                    isValid = True
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblMessage.Text = "IsValidCustomerCompanyName Error." + ex.Message
    '        HandleGlobalException(ex, "IsValidCustomerCompanyName Error." & vbCrLf & ex.Message)
    '    End Try

    '    Return isValid
    'End Function

    Private Sub btnLinkCustomerId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkCustomerId.Click

        Dim errMsg As String = String.Empty

        'validation
        Dim username As String = txtUsername3.Text.Trim().ToLower()
        If String.IsNullOrEmpty(username) Then
            errMsg = "Error: username cannot be empty"
            lblMessage.Text = errMsg
            MessageBox.Show(errMsg, "Username cannot be empty", MessageBoxButtons.OK, MessageBoxIcon.None)
            Return
        End If

        Dim expectedCustomerId As String
        If txtCustomerId3.Visible Then
            expectedCustomerId = txtCustomerId3.Text.Trim()
        Else
            expectedCustomerId = cbxCustomerId.SelectedValue.Split("-")(0).Trim()
        End If

        If String.IsNullOrEmpty(expectedCustomerId) Then
            errMsg = "Error: customer ID cannot be empty"
            lblMessage.Text = errMsg
            MessageBox.Show(errMsg, "Customer ID cannot be empty", MessageBoxButtons.OK, MessageBoxIcon.None)
            Return
        End If

        Dim userProfile As DataTable = dbSearchProfile(username)
        If userProfile.Rows.Count <= 0 Then
            errMsg = "Error: username not found"
            lblMessage.Text = errMsg
            MessageBox.Show(errMsg, "Username not found", MessageBoxButtons.OK, MessageBoxIcon.None)
            Return
        End If


        ' Check if the company name matches
        'If (Not IsValidCustomerCompanyName(txtUsername3.Text, CBCustCom.SelectedItem.ToString())) Then
        '    errMsg = "Error: Cannot link the username to the customer ID because the company names are different"
        '    lblMessage.Text = errMsg
        '    MessageBox.Show(errMsg, "Company Name Not Matches", MessageBoxButtons.OK, MessageBoxIcon.None)
        '    Return
        'End If

        Dim expectedCustomerRecord As DataTable = New DataTable
        expectedCustomerRecord = dbSearchCustomer(expectedCustomerId, CBCustCom.SelectedItem.ToString())
        'If CBCustCom.SelectedIndex = 0 Then
        '    expectedCustomerRecord = dbSearchCustomer(expectedCustomerId)
        'ElseIf CBCustCom.SelectedIndex = 1 Then
        '    expectedCustomerRecord = dbSearchCustomer(expectedCustomerId)
        'End If

        If expectedCustomerRecord.Rows.Count <= 0 Then
            errMsg = "Error: customer not found"
            lblMessage.Text = errMsg
            MessageBox.Show(errMsg, "Customer not found", MessageBoxButtons.OK, MessageBoxIcon.None)
            Return
        Else
            'the customer ID returned from the profile may not be empty or not equal to the expected customer ID
            'however, we don't cater it. 
        End If

        'prompt detail for confirmation
        Dim msg As String = "Are you sure to link username, '" + username + "', for below customer?" + Environment.NewLine + Environment.NewLine
        msg += "-----------------------------------" + Environment.NewLine
        msg += "CustomerID: " + expectedCustomerId + Environment.NewLine
        msg += "FullName: " + Convert.ToString(expectedCustomerRecord.Rows(0)("NameSuffix")) + " " + Convert.ToString(expectedCustomerRecord.Rows(0)("FirstName")) + Environment.NewLine
        msg += "Document ID: " + Convert.ToString(expectedCustomerRecord.Rows(0)("GovernmentIDCard")) + Environment.NewLine
        msg += "Passport Number: " + Convert.ToString(expectedCustomerRecord.Rows(0)("PassportNumber")) + Environment.NewLine
        msg += "Company Name: " + Convert.ToString(expectedCustomerRecord.Rows(0)("CompanyName")) + Environment.NewLine
        msg += "-----------------------------------"
        If MessageBox.Show(msg, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        If Not dbUpdateUserProfile(username, expectedCustomerId, expectedCustomerRecord.Rows(0)("CompanyName").Trim()) Then
            Return
        End If

        lblMessage.Text = "Done"

        'take log
        Dim oldCustomerId As String = If(IsDBNull(userProfile.Rows(0)("CustomerID")), "None", Convert.ToString(userProfile.Rows(0)("CustomerID")))
        Dim info As String = String.Format("Username,{0} , is linked to customer ID, {1}. Old Customer ID: {2}", username, expectedCustomerId, oldCustomerId)
        dbAuditLog("INFO", "", "", "", info)



        MessageBox.Show(info)

        btnSearch.BeginInvoke(searchAsync)

        Return

    End Sub


    Private Sub btnChangeUsername_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeUsername.Click
        'validation
        Dim oldUsername As String = txtOldUsername.Text.Trim().ToLower()
        If String.IsNullOrEmpty(oldUsername) Then
            lblMessage.Text = "Error: old username cannot be empty"
            Return
        End If

        Dim newUsername As String = txtNewUsername.Text.Trim()
        If String.IsNullOrEmpty(newUsername) Then
            lblMessage.Text = "Error: new username cannot be empty"
            Return
        End If

        Dim dt As DataTable = dbSearchUsername(oldUsername)
        If dt.Rows.Count <= 0 Then
            lblMessage.Text = "Error: old username is not found"
            Return
        End If

        If (newUsername.Equals(oldUsername)) Then
            lblMessage.Text = "Error: new username cannot be the same"
        End If

        Dim dt2 As DataTable = dbSearchUsername(newUsername)
        If dt2.Rows.Count > 0 Then
            lblMessage.Text = "Error: new username has already been used"
            Return
        End If

        'keep thing simple, don't use regular expression
        If (Not newUsername.Contains("@")) Then
            If (MessageBox.Show("New username is not likely an email, do you still proceed?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No) Then
                Return
            End If
        End If

        If Not dbUpdateUsername(oldUsername, newUsername) Then
            Return
        End If

        'take log
        Dim info As String = String.Format("Username, {0}, is changed to {1}.", oldUsername, newUsername)
        dbAuditLog("INFO", "", "", "", info)

        lblMessage.Text = "Done"
        MessageBox.Show(info)

        Return
    End Sub

    Private Sub btnClear4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear4.Click
        txtNewUsername.Text = String.Empty
        If Not txtOldUsername.ReadOnly Then
            txtOldUsername.Text = String.Empty
            txtOldUsername.Focus()
        Else
            txtNewUsername.Focus()
        End If

    End Sub

    Private Sub btnBack4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack4.Click
        Me.Text = "Find Username"
        pnlSearch.Visible = True
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = False
        dgvUsers.Focus()
        Cursor = Cursors.Default
        lblMessage.Text = String.Empty
        'freeze ui inputs
        txtEmail.ReadOnly = False
        btnResetPassword.Enabled = True
        btnClearInput.Enabled = True
    End Sub

    Private Sub btnGoChangeUsername_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoChangeUsername.Click
        Me.Text = "Change Username"
        pnlSearch.Visible = False
        pnlUpdateEmail.Visible = False
        pnlLinkCustomerId.Visible = False
        pnlChangeUsername.Visible = True
        txtOldUsername.Text = String.Empty
        txtOldUsername.ReadOnly = False
        txtOldUsername.BackColor = System.Drawing.SystemColors.Window
        txtOldUsername.Focus()
        If dgvUsers.DataSource IsNot Nothing AndAlso dgvUsers.Rows.Count >= 1 AndAlso
            dgvUsers.SelectedRows.Count > 0 Then
            Dim username As String = IIf(IsDBNull(dgvUsers.SelectedRows(0).Cells(0).Value), String.Empty, dgvUsers.SelectedRows(0).Cells(0).Value)
            If (Not String.IsNullOrEmpty(username)) Then
                txtOldUsername.Text = username
                txtOldUsername.ReadOnly = True
                txtOldUsername.BackColor = System.Drawing.SystemColors.Info
            End If
        End If
    End Sub

    'Private Sub cbxCustomerId_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxCustomerId.SelectionChangeCommitted
    Private Sub cbxCustomerId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxCustomerId.SelectedIndexChanged
        'Dim customerId As String = CType(cbxCustomerId.SelectedValue, String)
        Dim selectedPair As KeyValuePair(Of String, String) = CType(cbxCustomerId.SelectedItem, KeyValuePair(Of String, String))
        Dim customerCompany As String = selectedPair.Value

        If customerCompany.Equals("Others") Then
            txtCustomerId3.Text = String.Empty
            txtCustomerId3.Visible = True
            lblCustomerId3.Visible = True
            CBCustCom.SelectedIndex = 0
            CBCustCom.Enabled = True
        Else
            Dim customerId As String = customerCompany.Split("-")(0).Trim()
            Dim customerCompanyName As String = customerCompany.Split("-")(1).Trim()
            txtCustomerId3.Visible = False
            lblCustomerId3.Visible = False
            CBCustCom.Enabled = False
            ' Update CBCustCom based on the selected customerId
            For Each item As Object In CBCustCom.Items
                If item.ToString().Contains(customerCompanyName) Then
                    CBCustCom.SelectedItem = item
                    Exit For
                End If
            Next
        End If
    End Sub


#End Region


#Region "DB Actions/ Operations"
    Dim dbSearchUserByUsername As Func(Of String, DataTable) =
        Function(usrName) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()
            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_USER_NAME_RELATED_USER_PROFILE_BY_USERNAME", New Dictionary(Of String, String)() From {
                                              {"UserName", IIf(Not String.IsNullOrEmpty(usrName), usrName, " ")}}
                                              )
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_USER_NAME_RELATED_USER_PROFILE_BY_USERNAME Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_USER_NAME_RELATED_USER_PROFILE_BY_USERNAME Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt
        End Function

    Dim dbSearchUser As Func(Of String, String, String, DataTable) =
        Function(hkID, usrName, custID) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()
            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_USER_NAME_RELATED_USER_PROFILE", New Dictionary(Of String, String)() From {
                                              {"CustomerID", IIf(Not String.IsNullOrEmpty(custID), custID, "0")},
                                              {"HKID", IIf(Not String.IsNullOrEmpty(hkID), hkID & "%", " ")},
                                              {"UserName", IIf(Not String.IsNullOrEmpty(usrName), usrName & "%", " ")}}
                                              )
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_USER_NAME_RELATED_USER_PROFILE Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_USER_NAME_RELATED_USER_PROFILE Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt

            'Dim sql As String = " SELECT DISTINCT u.username as [Login ID], c.CustomerID as [Customer ID], c.GovernmentIDCard as [ID card], c.PassportNumber as [Passport ID], c.ExternalPartyCode as [Reference no.], c.fullname as [Full name], c.EmailAddr as [Email Address] "
            'sql = sql & " FROM ( "
            'sql = sql & " select distinct CustomerID, '' AS GovernmentIDCard, '' AS PassportNumber, ExternalPartyCode, ClientName1 as fullname ,EBEMail as EmailAddr from PolicyCustomerInfo c  "
            'sql = sql & " UNION ALL  "
            'sql = sql & " select distinct CustomerID, GovernmentIDCard, PassportNumber, '' AS ExternalPartyCode, NameSuffix + ' '+ FirstName as fullname , EmailAddr  from Customer  "
            'sql = sql & " ) AS c "
            'sql = sql & " LEFT JOIN security..aspnet_Profile p ON convert(varchar,c.CustomerID)=convert(varchar,p.PropertyValuesString)  "
            'sql = sql & " LEFT JOIN security..aspnet_Users u ON p.UserId=u.UserId  "
            'sql = sql & " WHERE 1=1 "
            'If Not String.IsNullOrEmpty(custID) Then
            '    sql = sql & " AND c.CustomerID = " & custID & " "
            'End If
            'If Not String.IsNullOrEmpty(hkID) Then
            '    sql = sql & " AND (c.GovernmentIDCard LIKE '" & hkID & "%' OR c.PassportNumber LIKE '" & hkID & "%' OR c.ExternalPartyCode LIKE '" & hkID & "%') "
            'End If
            'If Not String.IsNullOrEmpty(usrName) Then
            '    sql = sql & " AND u.UserName LIKE '" & usrName & "%'"
            'End If
            'sql = sql & " ORDER BY u.UserName "
            'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
            'Try
            '    conn.Open()
            '    Dim command As New SqlClient.SqlCommand(sql, conn)
            '    Dim dr As SqlClient.SqlDataReader = command.ExecuteReader()
            '    dt.Load(dr)
            'Catch ex As Exception
            '    lblMessage.Text = "DB Retrieve Error." + ex.Message
            'Finally
            '    conn.Close()
            '    Cursor = Cursors.Default
            'End Try
            'Return dt
        End Function

    Dim dbUpdateEmailID As Func(Of String, Boolean) =
        Function(emailID) As Boolean
            Dim isSuccess As Boolean = False
            If Not (String.IsNullOrEmpty(emailID) Or String.IsNullOrEmpty(customerID)) Then
                Dim sql As String = "update customer set EmailAddr='" + emailID + "' where CustomerID='" + customerID + "'"
                Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
                Try
                    conn.Open()
                    Dim command As New SqlClient.SqlCommand(sql, conn)
                    isSuccess = (command.ExecuteNonQuery() > 0)
                Catch ex As Exception
                    lblMessage.Text = "DB update error." + ex.Message
                    HandleGlobalException(ex, "DB update error." & vbCrLf & ex.Message)
                Finally
                    conn.Close()
                    Cursor = Cursors.Default
                End Try
            ElseIf String.IsNullOrEmpty(customerID) Then
                lblMessage.Text = "Error: Relevant CustomerID missing"
            End If
            Return isSuccess
        End Function

    Dim dbAuditLog As Action(Of String, String, String, String, String) =
        Sub(alType, alErr, alErrDetail, alReference, alInfo)
            Try
                APIServiceBL.ExecAPIBusi(getCompanyCode(),
                                "INSERT_APP_EVENT_LOG",
                                New Dictionary(Of String, String) From {
                                {"AL_Type", alType},
                                {"AL_Datetime", DateAndTime.Now.ToString()},
                                {"AL_Userid", Environment.UserName},
                                {"AL_Machine", Environment.MachineName},
                                {"AL_Err_Msg", alErr},
                                {"AL_StackTrace", IIf(Not String.IsNullOrEmpty(alErrDetail), alErrDetail, " ")},
                                {"AL_Reference", IIf(Not String.IsNullOrEmpty(alReference), alReference, " ")},
                                {"AL_Info_Value", IIf(Not String.IsNullOrEmpty(alInfo), alInfo, " ")},
                                {"AL_System", "VPA"}
                                })
            Catch ex As Exception
                lblMessage.Text = "CRSAPI INSERT_APP_EVENT_LOG insert error: Audit log failed. " + ex.Message
                HandleGlobalException(ex, "CRSAPI INSERT_APP_EVENT_LOG insert error: Audit log failed. " & vbCrLf & ex.Message)
            End Try

            'Dim sql As String = "INSERT INTO App_Event_Log(AL_Type, AL_Datetime, AL_Userid, AL_Machine, AL_Err_Msg, AL_StackTrace, AL_Reference,AL_Info_Value, AL_System)VALUES("
            'sql += "'" + alType + "', '" + DateAndTime.Now.ToString() + "', '" + Environment.UserName + "', '" + Environment.MachineName + "', "
            'sql += "'" + alErr + "', '" + alErrDetail + "', '" + alReference + "','" + alInfo + "', 'VPA') "
            'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
            'Try
            '    conn.Open()
            '    Dim command As New SqlClient.SqlCommand(sql, conn)
            '    Dim affectedRows = command.ExecuteNonQuery()
            'Catch ex As Exception
            '    lblMessage.Text = "Erro: Audit log failed. " + ex.Message
            'Finally
            '    conn.Close()
            'End Try
        End Sub

    Dim dbSearchProfile As Func(Of String, DataTable) =
        Function(userName) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()
            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_USER_PROFILE_BY_USERNAME", New Dictionary(Of String, String)() From {
                                              {"LoweredUserName", userName}
                                              })
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_USER_PROFILE_BY_USERNAME Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_USER_PROFILE_BY_USERNAME Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt
            'Dim sql As String = "select " &
            '                        "c.CustomerID " &
            '                        ",c.NameSuffix " &
            '                        ",c.FirstName " &
            '                        ",c.GovernmentIDCard " &
            '                        ",c.PassportNumber " &
            '                    "from security..aspnet_Users au " &
            '                        "inner join security..aspnet_Profile ap " &
            '                            "on au.UserId = ap.UserId " &
            '                        "left join customer c " &
            '                            "on CONVERT(varchar(20), c.customerID) = convert(varchar(20), ap.PropertyValuesString) " &
            '                    "where au.LoweredUserName = '" & userName & "' "

            'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
            'Try
            '    conn.Open()
            '    Dim command As New SqlClient.SqlCommand(sql, conn)
            '    Dim dr As SqlClient.SqlDataReader = command.ExecuteReader()
            '    dt.Load(dr)
            'Catch ex As Exception
            '    lblMessage.Text = "DB Retrieve Error." + ex.Message
            'Finally
            '    conn.Close()
            '    Cursor = Cursors.Default
            'End Try
            'Return dt
        End Function

    Dim dbSearchUsername As Func(Of String, DataTable) =
        Function(userName) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()
            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_USER_BY_USERNAME_FROM_ASPNET_USERS", New Dictionary(Of String, String)() From {
                                              {"LoweredUserName", userName}
                                              })
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_USER_BY_USERNAME_FROM_ASPNET_USERS Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_USER_BY_USERNAME_FROM_ASPNET_USERS Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt
            'Dim dt As DataTable = New DataTable()
            'Dim sql As String = "select * " &
            '                    "from security..aspnet_Users au " &
            '                    "where au.LoweredUserName = '" & userName & "' "

            'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
            'Try
            '    conn.Open()
            '    Dim command As New SqlClient.SqlCommand(sql, conn)
            '    Dim dr As SqlClient.SqlDataReader = command.ExecuteReader()
            '    dt.Load(dr)
            'Catch ex As Exception
            '    lblMessage.Text = "DB Retrieve Error." + ex.Message
            'Finally
            '    conn.Close()
            '    Cursor = Cursors.Default
            'End Try
            'Return dt
        End Function

    Dim dbSearchCustomer As Func(Of String, String, DataTable) =
        Function(customerId, companyName) As DataTable
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable()
            Try
                ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_CUSTOMER_CUSTOMER_ID", New Dictionary(Of String, String)() From {
                                              {"CustomerID", customerId},
                                              {"CompanyName", companyName}
                                              })
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                lblMessage.Text = "CRSAPI GET_CUSTOMER_CUSTOMER_ID Retrieve Error." + ex.Message
                HandleGlobalException(ex, "CRSAPI GET_CUSTOMER_CUSTOMER_ID Retrieve Error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
            Return dt

            'Dim dt As DataTable = New DataTable()
            'Dim sql As String = "select " &
            '                        "c.CustomerID " &
            '                        ",c.NameSuffix " &
            '                        ",c.FirstName " &
            '                        ",c.GovernmentIDCard " &
            '                        ",c.PassportNumber " &
            '                    "from customer c " &
            '                    "where c.CustomerID = " & customerId & " "

            'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
            'Try
            '    conn.Open()
            '    Dim command As New SqlClient.SqlCommand(sql, conn)
            '    Dim dr As SqlClient.SqlDataReader = command.ExecuteReader()
            '    dt.Load(dr)
            'Catch ex As Exception
            '    lblMessage.Text = "DB Retrieve Error." + ex.Message
            'Finally
            '    conn.Close()
            '    Cursor = Cursors.Default
            'End Try
            'Return dt
        End Function

    Dim dbUpdateUserProfile As Func(Of String, String, String, Boolean) =
    Function(username, customerId, companyCode) As Boolean
        Dim isSuccess As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(),
                                "UPDATE_USER_PROFILE_TO_LINK_CUSTOMER_ID",
                                New Dictionary(Of String, String) From {
                                {"CustomerID", customerId},
                                {"LoweredUserName", username},
                                {"mbrso_userid", username},
                                {"CompanyCode", companyCode}
                                })
            isSuccess = True
        Catch ex As Exception
            lblMessage.Text = "CRSAPI UPDATE_USER_PROFILE_TO_LINK_CUSTOMER_ID error. " + ex.Message
            HandleGlobalException(ex, "CRSAPI UPDATE_USER_PROFILE_TO_LINK_CUSTOMER_ID error. " & vbCrLf & ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
        Return isSuccess

        'Dim sql As String = "update ap " +
        '                        "set ap.propertyNames = cast('CustomerID:S:0:8:' as ntext), " +
        '                     "ap.propertyValuesString = cast('" + customerId + "' as ntext) " +
        '                    "from security..aspnet_Profile ap " +
        '                     "inner join security..aspnet_Users au " +
        '                      "on ap.UserId = au.UserId " +
        '                    "where au.LoweredUserName = '" + username + "' "

        'Dim sql2 As String = "delete from security..member_specialoffer where mbrso_userid='" + username + "' "

        'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
        'Try
        '    conn.Open()
        '    Dim command As New SqlClient.SqlCommand(sql, conn)
        '    Dim command2 As New SqlClient.SqlCommand(sql2, conn)

        '    command2.ExecuteNonQuery()
        '    isSuccess = (command.ExecuteNonQuery() > 0)
        'Catch ex As Exception
        '    lblMessage.Text = "DB update error. " + ex.Message
        'Finally
        '    conn.Close()
        '    Cursor = Cursors.Default
        'End Try
        'Return isSuccess
    End Function

    Dim dbUpdateUsername As Func(Of String, String, Boolean) =
    Function(oldUsername, newUsername) As Boolean
        Dim isSuccess As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(),
                                "UPDATE_USER_NAME_TO_CHANGE_USER_NAME",
                                New Dictionary(Of String, String) From {
                                {"NewLoweredUserName", newUsername},
                                {"OldLoweredUserName", oldUsername}
                                })
            isSuccess = True
        Catch ex As Exception
            lblMessage.Text = "CRSAPI UPDATE_USER_NAME_TO_CHANGE_USER_NAME update error. " + ex.Message
            HandleGlobalException(ex, "CRSAPI UPDATE_USER_NAME_TO_CHANGE_USER_NAME update error. " & vbCrLf & ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
        Return isSuccess

        'Dim sql As String = "update au " +
        '                        "set au.LoweredUserName ='" + newUsername + "' " +
        '                        ",au.UserName ='" + newUsername + "' " +
        '                    "from security..aspnet_Users au " +
        '                    "where au.LoweredUserName = '" + oldUsername + "' "

        'Dim sql2 As String = "update am " +
        '                         "set am.Email = '" + newUsername + "' " +
        '                         ", am.LoweredEmail = '" + newUsername + "' " +
        '                      "from security..aspnet_membership am " +
        '                         "inner join security..aspnet_users au " +
        '                             "on au.UserId = am.UserId " +
        '                      "where au.LoweredUsername = '" + newUsername + "' "  'username has already changed through sql1

        'Dim sql3 As String = "update security..member_specialoffer set mbrso_userid='" + newUsername + "' where mbrso_userid='" + oldUsername + "' "

        'Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(connStr)
        'Try
        '    conn.Open()
        '    Dim command As New SqlClient.SqlCommand(sql, conn)
        '    Dim command2 As New SqlClient.SqlCommand(sql2, conn)
        '    Dim command3 As New SqlClient.SqlCommand(sql3, conn)

        '    command3.ExecuteNonQuery()
        '    isSuccess = (command.ExecuteNonQuery() > 0 And command2.ExecuteNonQuery() > 0)
        'Catch ex As Exception
        '    lblMessage.Text = "DB update error. " + ex.Message
        'Finally
        '    conn.Close()
        '    Cursor = Cursors.Default
        'End Try
        'Return isSuccess
    End Function

#End Region

#Region "Private Utilities"
    Dim searchAsync =
        Sub()
            Dim hkid As String = txtHKID.Text
            Dim usrName As String = txtUsername.Text
            Dim custID As String = txtCustomerID.Text
            Try
                If Not (String.IsNullOrEmpty(custID) Or IsNumeric(custID)) Then
                    lblMessage.Text = "CustomerID accepts only numbers !"
                    Return
                End If
                lblMessage.Text = "Searching user..."
                dgvUsers.DataSource = Nothing
                Dim dt As DataTable = dbSearchUser(hkid, usrName, custID)
                dgvUsers.DataSource = dt
                dgvUsers.Columns(0).Width = 200
                dgvUsers.Focus()
                lblMessage.Text = "Done."
                wndMain.StatusBarPanel1.Text = dt.Rows.Count & " records selected"
            Catch ex As Exception
                lblMessage.Text = "search error." + ex.Message
                HandleGlobalException(ex, "search error." & vbCrLf & ex.Message)
            Finally
                Cursor = Cursors.Default
            End Try
        End Sub


    Private Function GeneratePassword(ByVal strCharacters As String, ByVal iPasswordLength As Integer) As String
        Dim iCharacterCount As Integer = strCharacters.Length
        Dim i As Integer
        Dim aPassword As List(Of Char) = New List(Of Char)

        For i = 0 To iPasswordLength - 1
            Dim rdn As New Random(Guid.NewGuid().GetHashCode())
            Dim iIndex As Integer = rdn.Next(0, iCharacterCount)
            aPassword.Add(strCharacters(iIndex))
        Next
        Return New String(aPassword.ToArray())
    End Function

    Public Function SendMail(ByVal strFmMail As String, ByVal strToMail As String, ByVal strSubject As String,
                             ByVal strBody As String, ByVal strSMTPIP As String, ByVal blnIsHtml As Boolean,
                             Optional ByVal strAttachment As String = vbNullString) As Boolean
        Try
            Dim message As New System.Net.Mail.MailMessage(strFmMail, strToMail, strSubject, strBody)
            Dim client As New System.Net.Mail.SmtpClient(strSMTPIP)

            If Not strAttachment Is Nothing Then
                If strAttachment.Length > 0 Then
                    Dim data = New System.Net.Mail.Attachment(strAttachment)
                    message.Attachments.Add(data)
                    data = Nothing
                End If
            End If
            message.IsBodyHtml = blnIsHtml
            client.Credentials = System.Net.CredentialCache.DefaultCredentials
            client.Send(message)
            message = Nothing
            client = Nothing
            Return True
        Catch ex As Exception
            lblMessage.Text = "Error: Send email failed. " + ex.Message
            HandleGlobalException(ex, "Error: Send email failed. " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Protected Overridable Function PrepareForgotIDPWDEmailBody(ByVal strInsuredName As String, ByVal strPassword As String) As String
        Dim strResult As String = String.Empty

        strResult &= "<table>"
        'strResult &= "<tr><td>Header</td></tr>"
        'or use app.config's settings, Dim strDateFormat As String = ConfigurationManager.AppSettings("DateTimeFormat")
        strResult &= "<tr><td>Date : " & Now.ToString("dd MMM yyy HH:mm:ss") & "</td></tr>"
        strResult &= "<tr><td></td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"

        strResult &= "<tr><td>Dear Customer : (" & strInsuredName & ")</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>Your login password has been reset. The new login password is the following:</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>Password : " & strPassword & "</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td><b><u>FWD Online Interactive Services:</u></b></td></tr>"
        strResult &= "<tr><td>You can access your policy details and acquire updated insurance information through the interactive service on FWD web site (www.fwd.com.hk).  By simply entering your Customer Number and PIN, you can submit your online enquiries and access the e-policy service. For details, please refer to the leaflet of ""Interactive Service"".</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>For maximum protection of your account information, kindly take note of the followings:</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>1.  Please keep your PIN confidential and do not disclose it to others. Keep this letter in a safe place, or destroy it after memorizing your assigned PIN.</td></tr>"
        strResult &= "<tr><td>2.  If this letter is not sealed, or intact, please notify us immediately.</td></tr>"
        strResult &= "<tr><td>3.  If you wish to change your PIN, please change it through our online interactive services.</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>If you have any questions, please feel free to call our Personal Customer Service Hotline on 3123-3123.</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>Customer Service Department - FWD Life Insurance Co. (Bermuda) Ltd.</td></tr>"

        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"

        strResult &= "<tr><td>親愛的客戶 (" & strInsuredName & ")</td></tr>"
        strResult &= "<tr><td>閣下登入密碼已重設，新登入密碼如下︰</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>登入密碼 : " & strPassword & "</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td><b><u>富衛網上客戶互動服務:</u></b></td></tr>"
        strResult &= "<tr><td>閣下可登上富衛 網頁 (www.fwd.com.hk)&nbsp;，透過「客戶互動服務」網頁以了解您的保單詳情，及獲得最新最快的保險資訊。閣下只需輸入客戶號碼及個人密碼便可於網上查詢及使用網上保單服務，詳情請參考「客戶互動服務」簡介。</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>為避免閣下之個人賬戶資料外洩，請留意以下事項：</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>1.   請將個人密碼保密，勿向其他人透露；並請於緊記個人密碼後，把此信函存放於安全地方或撕毀。</td></tr>"
        strResult &= "<tr><td>2.   如此信函不完整或未有密封，請與我們聯絡。</td></tr>"
        strResult &= "<tr><td>3.   如有需要，閣下可透過網上客戶互動服務更改個人密碼。</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>若閣下有任何疑問，歡迎致電本公司專人客戶服務熱線：3123-3123。</td></tr>"
        strResult &= "<tr><td>&nbsp;</td></tr>"
        strResult &= "<tr><td>客戶服務部 - 富衛人壽保險(百慕達)有限公司</td></tr>"

        strResult &= "<tr><td>&nbsp;</td></tr>"
        'strResult &= "<tr><td>Footer</td></tr>"
        strResult &= "</table>"
        Return strResult
    End Function


    Private Function IsValidEmailID(ByVal emailid As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim address As MailAddress = New MailAddress(emailid)
            result = True
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            result = False
        End Try
        Return result
    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles LCustCom.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCustCom.SelectedIndexChanged

    End Sub
#End Region





End Class
