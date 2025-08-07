''' <remarks>
''' Added by Hugo Chan on 2021-05-14
''' Project: CRS - First Level of Access
''' </remarks>
Public Class uclPolicySummary_Asur
    Private Const DATE_FORMAT As String = "yyyy-MM-dd"
    Private Const DATE_FORMAT_DISPLAY As String = "MMM dd, yyyy"

    Private Const MESSAGE_UNABLE_TO_GET_POLICY_INFORMATION As String = "Unable to get policy information."
    Private Const MESSAGE_ERROR_OCCURS_WHEN_GETTING_POLICY_INFORMATION As String = "Error occurs when loading policy information."
    Private Const MESSAGE_UNABLE_TO_GET_ROLE_INFORMATION As String = "Unable to get role information."
    Private Const MESSAGE_ERROR_OCCURS_WHEN_GETTING_ROLE_INFORMATION As String = "Error occurs when loading role information."

    Private _policyNumber As String
    Private _productName As String
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub

	Public Property PolicyNumber() As String
        Get
            Return _policyNumber
        End Get
        Set(ByVal value As String)
            _policyNumber = value
        End Set
    End Property

    Public Property ProductName() As String
        Get
            Return _productName
        End Get
        Set(ByVal value As String)
            _productName = value
        End Set
    End Property

    Private Sub uclPolicySummary_Asur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' remove "Change Insured/Keyman" tab, not necessary for Assurance in this stage.
        If TabControl1.TabPages.Count > 1 Then
            TabControl1.TabPages.RemoveAt(1)
        End If
    End Sub

    Public Sub LoadInformation()
        LoadPolicyInformation()
        LoadPolicyRoles()
    End Sub

    Public Sub LoadPolicyInformation()
        Dim ds As DataSet
        Dim dr As DataRow
        Dim lngErr As Long
        Dim strErr As String = String.Empty

        Try
            ds = GetPolicy_Asur(PolicyNumber, "policy", lngErr, strErr)
            If Not String.IsNullOrWhiteSpace(strErr) Then
                MessageBox.Show(MESSAGE_ERROR_OCCURS_WHEN_GETTING_POLICY_INFORMATION & " " & strErr)
                Exit Sub
            End If

            If ds Is Nothing Then
                MessageBox.Show(MESSAGE_UNABLE_TO_GET_POLICY_INFORMATION)
                Exit Sub
            End If

            If ds.Tables.Count = 0 Then
                MessageBox.Show(MESSAGE_UNABLE_TO_GET_POLICY_INFORMATION)
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                MessageBox.Show(MESSAGE_UNABLE_TO_GET_POLICY_INFORMATION)
                Exit Sub
            End If

            dr = ds.Tables(0).Rows(0)

            AssignValuesToScreenHeadControls(dr)
            AssignValuesToBillingInformationControls(dr)


        Catch ex As Exception
            LogInformation("Error", "", "uclPolicySummary_Asur - LoadPolicyInformation", ex.Message, ex)

            MessageBox.Show(ex.Message)
        Finally
            ' release resource
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
        End Try
    End Sub

    Public Sub LoadPolicyRoles()
        Dim ds As DataSet
        Dim lngErr As Long
        Dim strErr As String = String.Empty

        Try
            ds = GetPolicyRoles_Asur(PolicyNumber, "role", lngErr, strErr)
            If Not String.IsNullOrWhiteSpace(strErr) Then
                MessageBox.Show(MESSAGE_ERROR_OCCURS_WHEN_GETTING_ROLE_INFORMATION & " " & strErr)
                Exit Sub
            End If

            If ds Is Nothing Then
                MessageBox.Show(MESSAGE_UNABLE_TO_GET_ROLE_INFORMATION)
                Exit Sub
            End If

            If ds.Tables.Count = 0 Then
                MessageBox.Show(MESSAGE_UNABLE_TO_GET_ROLE_INFORMATION)
                Exit Sub
            End If

            With UclPolicyClient_Asur1
                .DGVRole.DataSource = ds.Tables(0)
                .DGVRole.Refresh()

                .DGVRole.Columns("Company").Visible = False
                .DGVRole.Columns("Role").Visible = False
                .DGVRole.Columns("role_desc").HeaderText = "Role"
                .DGVRole.Columns("Customer_No").HeaderText = "Customer No."
                .DGVRole.Columns("DOB").HeaderText = "Date of Birth"
                .DGVRole.Columns("relationship").HeaderText = "Relationship"
                .DGVRole.Columns("addr_proof").HeaderText = "Address Proof"
                .DGVRole.Columns("SmokeFlag").HeaderText = "Smoke Flag"
            End With
        Catch ex As Exception
            LogInformation("Error", "", "uclPolicySummary_Asur - LoadPolicyRoles", ex.Message, ex)

            MessageBox.Show(ex.Message)
        Finally
            ' release resource
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
        End Try
    End Sub

    Private Sub AssignValuesToScreenHeadControls(ByVal dr As DataRow)


        With UclPolicyScreenHead_Asur1
            .lblCapsilPolNo.Text = Convert.ToString(dr("policy"))
            .lbPOCurr.Text = Convert.ToString(dr("Currency"))
            .lbContractStatus.Text = Convert.ToString(dr("Status"))
            .lbPStatus.Text = Convert.ToString(dr("Prem_Status"))
            .lbMode.Text = Convert.ToString(dr("Mode"))
            .lbBillType.Text = Convert.ToString(dr("Bill_Type"))
            If dr.IsNull("Pay_to_Date") Then
                .lbPTD.Text = "-"
            Else
                .lbPTD.Text = Convert.ToDateTime(dr("Pay_to_Date")).ToString(DATE_FORMAT_DISPLAY)
            End If
            If dr.IsNull("RCD") Then
                .lbRCD.Text = "-"
            Else
                .lbRCD.Text = Convert.ToDateTime(dr("RCD")).ToString(DATE_FORMAT_DISPLAY)
            End If
            .lblNxLevy.Text = Convert.ToString(dr("Next_Insurance_Levy"))
            .lblNxTrm.Text = "-"
            .lbCode.Text = Convert.ToString(dr("Product"))
            .lbContractDesc.Text = ProductName
            .lbOwnNo.Text = ""
            .lbOwnName.Text = Convert.ToString(dr("Owner"))
            .lbBInsuredNo.Text = ""
            .lbBInsuredName.Text = Convert.ToString(dr("Basic_Insured"))
            .lbSAgent.Text = Convert.ToString(dr("Servicing_Agent"))
            .lbOCustomerNo.Text = "-"
        End With
    End Sub

    Private Sub AssignValuesToBillingInformationControls(ByVal dr As DataRow)
        Dim currency As String
        Dim drawDate As String
        Dim nextBillDate As Nullable(Of Date)
        Dim coolingOffDate As Nullable(Of Date)

        currency = Convert.ToString(dr("Currency"))
        drawDate = Convert.ToString(dr("Draw_Date"))

        With UclPolicyBillingInfo_Asur1
            .txtBillType.Text = Convert.ToString(dr("Bill_Type"))
            .txtMode.Text = Convert.ToString(dr("Mode"))
            .cboCurrency.Items.Clear()
            .cboCurrency.Items.Add(currency)
            .cboCurrency.SelectedIndex = 0
            .cboDrawDay.Items.Clear()
            .cboDrawDay.Items.Add(drawDate)
            .cboDrawDay.SelectedIndex = 0
            .txtLevyQuotation.Text = Convert.ToString(dr("Levy"))
            AssignValueToDateTimePicker(.dtpPTD, dr, "Pay_to_Date")
            AssignValueToDateTimePicker(.dtpBTD, dr, "Bill_to_Date")
            nextBillDate = ConvertStringToDate(dr, "Next_bill_date")
            AssignValueToDateTimePicker(.dtpNBD, nextBillDate)
            .txtModeP.Text = Convert.ToString(dr("Modal_Premium"))
            .txtTotalAmount.Text = Convert.ToString(dr("Total_Amount"))
            AssignValueToDateTimePicker(.dtpPID, dr, "Policy_Inforce_Date")
            AssignValueToDateTimePicker(.dtpFID, dr, "First_Inforce_Date")
            AssignValueToDateTimePicker(.dtpPPD, dr, "Policy_Proposal_Date")
            AssignValueToDateTimePicker(.dtpARD, dr, "Application_Recev_Date")
            AssignValueToDateTimePicker(.dtpUWDD, dr, "UW_Decision_Date")
            AssignValueToDateTimePicker(.dtpDispDate, dr, "Dispatch_Date")
            AssignValueToDateTimePicker(.dtpRCD, dr, "RCD")
            coolingOffDate = ConvertStringToDate(dr, "Cooling_Off_Date")
            AssignValueToDateTimePicker(.dtpCooloffDate, coolingOffDate)

            ' Note: No the following columns in DataSet
            '       Dispatch Person
            '       Diviend Option
            .txtDispPerson.Text = String.Empty
            .txtDividendOption.Text = String.Empty
        End With
    End Sub

    Private Function ConvertStringToDate(ByVal dr As DataRow, ByVal columnName As String) As Nullable(Of Date)
        Dim d As Nullable(Of Date) = Nothing
        Dim t As String
        Dim dateFormatter As New System.Globalization.DateTimeFormatInfo
        dateFormatter.FullDateTimePattern = DATE_FORMAT
        dateFormatter.LongDatePattern = DATE_FORMAT
        dateFormatter.ShortDatePattern = DATE_FORMAT

        If Not dr.IsNull(columnName) Then
            If Not String.IsNullOrWhiteSpace(Convert.ToString(dr(columnName))) Then
                t = Convert.ToDateTime(dr(columnName))
                DateTime.TryParse(t, dateFormatter, System.Globalization.DateTimeStyles.None, d)
            End If
        End If

        Return d
    End Function
End Class
