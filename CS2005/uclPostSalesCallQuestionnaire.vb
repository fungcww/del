Public Class uclPostSalesCallQuestionnaire

    Private policyNo As String
    Private dtCoverage As DataTable
    Private dtQuestion As DataTable
    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Private formMode As FormModeEnum
    Private questionControls As New List(Of uclQuestion)()

    Public Property PolicyAccountID() As String
        Get
            Return policyNo
        End Get
        Set(ByVal value As String)
            policyNo = value
        End Set
    End Property

    Private objDBHeader As Utility.Utility.ComHeader
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property

    Public Sub Initialize()
        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = objDBHeader

        Dim dtCategory As DataTable = Nothing
        Dim strErr As String = Nothing

        'If Not clsCRS.RetrievePostSalesCallProductCategory(dtCategory, strErr) Then
        If Not RetrievePostSalesCallProductCategory(objDBHeader.CompanyID, dtCategory) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        cboCategory.DataSource = dtCategory
        cboCategory.ValueMember = "cswcpc_category"
        cboCategory.DisplayMember = "cswcpc_category_desc"

        txtEarlySurCharge.DataBindings.Add("Text", bsProduct, "cswpsd_surr_charge")
        txtEarlySurrPeriod.DataBindings.Add("Text", bsProduct, "cswpsd_surr_period")
        txtPremiumType.DataBindings.Add("Text", bsProduct, "cswpsd_premium_type")
        txtFees.DataBindings.Add("Text", bsProduct, "cswpsd_fees")
        txtBenefit.DataBindings.Add("Text", bsProduct, "cswpsd_benefit")
        txtLoan.DataBindings.Add("Text", bsProduct, "cswpsd_loan")
        txtCSV.DataBindings.Add("Text", bsProduct, "cswpsd_cash_value")
        txtCoupon.DataBindings.Add("Text", bsProduct, "cswpsd_coupon")
        txtDividend.DataBindings.Add("Text", bsProduct, "cswpsd_dividend")
        txtNonGuarBenefit.DataBindings.Add("Text", bsProduct, "cswpsd_ng_benefit")
        txtOther.DataBindings.Add("Text", bsProduct, "cswpsd_others")
        txtBonus.DataBindings.Add("Text", bsProduct, "cswpsd_special_bonus")
        txtGuideUrl.DataBindings.Add("Text", bsProduct, "cswpsd_guide_url")
        txtProductTypeEng.DataBindings.Add("Text", bsProduct, "cswpsd_product_type_eng")
        txtProductTypeChi.DataBindings.Add("Text", bsProduct, "cswpsd_product_type_chi")
        txtObjectiveEng.DataBindings.Add("Text", bsProduct, "cswpsd_product_objective_eng")
        txtObjectiveChi.DataBindings.Add("Text", bsProduct, "cswpsd_product_objective_chi")
        txtRiskEng.DataBindings.Add("Text", bsProduct, "cswpsd_risk_eng")
        txtRiskChi.DataBindings.Add("Text", bsProduct, "cswpsd_risk_chi")

        cboCategory.DataBindings.Add("SelectedValue", bsProduct, "cswpsd_category")

        Dim bindingHasFees As New Binding("Checked", bsProduct, "cswpsd_has_fees")
        AddHandler bindingHasFees.Format, AddressOf CharToBooleanConvertEventHandler
        AddHandler bindingHasFees.Parse, AddressOf BooleanToCharConvertEventHandler
        chkFees.DataBindings.Add(bindingHasFees)

        AddHandler txtBenefit.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtProductTypeEng.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtProductTypeChi.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtObjectiveEng.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtObjectiveChi.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtRiskEng.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler
        AddHandler txtRiskChi.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler


        dgvCoverage.AutoGenerateColumns = False


    End Sub

    Public Sub RetrieveInfo()
        Dim questionCtrl As uclQuestion
        Dim strErr As String = Nothing

        'If Not clsCRS.GetCoverage(policyNo, dtCoverage, strErr) Then
        If Not GetCoverage(getCompanyCode(Me.objDBHeader.CompanyID), policyNo, dtCoverage) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        bsProduct.DataSource = dtCoverage
        dgvCoverage.DataSource = bsProduct

        'If Not clsCRS.RetrievePostSalesCallPolicyQuestion(policyNo, dtQuestion, strErr) Then
        If Not RetrievePostSalesCallPolicyQuestion(policyNo, dtQuestion, strErr, getCompanyCode(Me.objDBHeader.CompanyID)) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        dtQuestion.DefaultView.Sort = "cswpsq_question_no, cswpsq_questionnaire_code"
        questionControls.Clear()
        For Each drQuestion As DataRowView In dtQuestion.DefaultView
            questionCtrl = New uclQuestion()
            questionCtrl.QuestionID = drQuestion("cswpsq_question_id")
            questionCtrl.QuestionNo = drQuestion("cswpsq_question_no").ToString()
            questionCtrl.Question = drQuestion("cswpsq_description").ToString()
            questionCtrl.AnswerValue = drQuestion("cswpca_answer_value").ToString()

            fpnQuestion.Controls.Add(questionCtrl)
            questionControls.Add(questionCtrl)
        Next

    End Sub

    Private Function Save() As Boolean
        Dim q As uclQuestion
        Dim drQuestion As DataRow
        Dim strErr As String = Nothing

        For Each q In questionControls
            drQuestion = dtQuestion.Select("cswpsq_question_id=" & q.QuestionID)(0)
            If drQuestion Is Nothing Then
                Throw New Exception("Cannot find question in DataTable.")
            End If
            drQuestion("cswpca_answer_value") = q.AnswerValue
        Next

        'If Not clsCRS.SavePostSalesCallPolicyAnswer(dtQuestion, strErr) Then
        If Not SavePostSalesCallPolicyAnswer(dtQuestion, objDBHeader.UserID, getCompanyCode(g_Comp)) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return False
        End If

        Return True
    End Function

    Private Sub CancelAllEdit()
        Dim q As uclQuestion
        Dim drQuestion As DataRow

        For Each q In questionControls
            drQuestion = dtQuestion.Select("cswpsq_question_id=" & q.QuestionID)(0)
            If drQuestion Is Nothing Then
                Throw New Exception("Cannot find question in DataTable.")
            End If
            q.AnswerValue = drQuestion("cswpca_answer_value").ToString().Trim()
        Next
    End Sub

    Public Sub SetUIMode(ByVal mode As FormModeEnum)
        formMode = mode

        Select Case formMode
            Case FormModeEnum.Modify
                cmdSave.Enabled = True
                cmdCancel.Enabled = True

                For Each q As uclQuestion In questionControls
                    q.ReadOnly = False
                Next

            Case FormModeEnum.Enquiry
                cmdSave.Enabled = False
                cmdCancel.Enabled = False

                For Each q As uclQuestion In questionControls
                    q.ReadOnly = True
                Next

        End Select
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Save() Then
            MsgBox("Answer saved.", MsgBoxStyle.Information, "Message")
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        CancelAllEdit()
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        If txtGuideUrl.Text.Trim().StartsWith("http") Then
            Process.Start(txtGuideUrl.Text.Trim())
        End If
    End Sub

    Public Sub CheckPostSalesCallCompleted()
        Dim blnCompleted As Boolean
        Dim strErr As String = Nothing

        'If Not clsCRS.IsPostSalesCallCompleted(Me.PolicyAccountID, blnCompleted, strErr) Then
        '    MsgBox(strErr, MsgBoxStyle.Critical, "Error")
        '    Return
        'End If

        Dim strServiceEventCategory As String = "'86', '88', '93', '91'"
        Dim sbSql As New System.Text.StringBuilder()
        Dim strSQL As String = Nothing
        Dim dtServiceLog As New DataTable()

        blnCompleted = False

        ' service event log
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select top 1 * from ServiceEventDetail")
        sbSql.AppendLine("where EventCategoryCode in (" & strServiceEventCategory & ")")
        sbSql.AppendFormat("and PolicyAccountID='{0}'", Me.PolicyAccountID) : sbSql.AppendLine()
        sbSql.AppendLine("order by EventInitialDateTime desc, ServiceEventNumber desc")

        strSQL = sbSql.ToString()

        If Not ExcecuteQuery(strSQL, strCIWConn, dtServiceLog, strErr) Then
            MsgBox(strErr)
        End If

        If dtServiceLog.Rows.Count > 0 AndAlso
            dtServiceLog.Rows(0)("EventStatusCode").ToString.Trim = "C" AndAlso
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() <> "10" AndAlso
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() <> "20" Then

            blnCompleted = True
        End If

        If blnCompleted Then
            SetUIMode(FormModeEnum.Enquiry)
        Else
            SetUIMode(FormModeEnum.Modify)
        End If

    End Sub

End Class
