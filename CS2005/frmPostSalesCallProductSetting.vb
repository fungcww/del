Public Class frmPostSalesCallProductSetting

    Public objDBHeader As Utility.Utility.ComHeader

    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Private dsSetting As DataSet
    Private formMode As FormModeEnum = FormModeEnum.AddNew

    Private Sub LoadData()
        Dim strErr As String = Nothing

        ' retrieve data
        Dim tempDs As DataSet = New DataSet()
        dsSetting = New DataSet()

        'If Not clsCRS.RetrievePostSalesCallProductSettings(dsSetting, strErr) Then
        If RetrievePostSalesCallProductSettings(Me.objDBHeader.CompanyID, tempDs) Then
            For Each dt As DataTable In tempDs.Tables
                dsSetting.Tables.Add(dt.Copy)
            Next

            Try
                dsSetting.Relations.Add("productQuestionRel", dsSetting.Tables("Products").Columns("cswpsd_ProductID"), dsSetting.Tables("Questions").Columns("cswpsn_ProductID"))
            Catch ex As Exception
            End Try
            ' bind data
            bsProduct.DataSource = dsSetting
            bsProduct.DataMember = "Products"

            bsQuestion.DataSource = bsProduct
            bsQuestion.DataMember = "productQuestionRel"
            bsQuestion.Sort = "cswpsq_question_no"

        End If
    End Sub

    Private Sub Init()
        Dim strErr As String = Nothing

        dgvProduct.AutoGenerateColumns = False

        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = objDBHeader

        LoadData()

        cboCategory.DataSource = dsSetting.Tables("Category")
        cboCategory.DisplayMember = "cswcpc_category_desc"
        cboCategory.ValueMember = "cswcpc_category"

        dgvProduct.DataSource = bsProduct

        txtProductID.DataBindings.Add("Text", bsProduct, "cswpsd_ProductID")
        txtPlanNameEng.DataBindings.Add("Text", bsProduct, "Description")
        txtPlanNameChi.DataBindings.Add("Text", bsProduct, "ChineseDescription")
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

        dgvQuestion.AutoGenerateColumns = False
        dgvQuestion.DataSource = bsQuestion

        SetUIMode(FormModeEnum.Enquiry)
    End Sub

    Private Sub SetUIMode(ByVal mode As FormModeEnum)
        formMode = mode

        Select Case formMode
            Case FormModeEnum.AddNew, FormModeEnum.Modify
                dgvProduct.Enabled = False

                cmdPlanNameFilter.Enabled = False
                cmdProductIdFilter.Enabled = False
                cmdProductAdd.Enabled = False
                cmdProductDelete.Enabled = False
                cmdProductCopy.Enabled = False
                cmdProductEdit.Enabled = False
                cmdProductCancel.Enabled = True
                cmdProductSave.Enabled = True
                cmdQuestionAdd.Enabled = False
                cmdQuestionRemove.Enabled = False

                cboCategory.Enabled = True
                chkFees.Enabled = True

                txtBenefit.ReadOnly = False
                txtBonus.ReadOnly = False
                txtCoupon.ReadOnly = False
                txtCSV.ReadOnly = False
                txtDividend.ReadOnly = False
                txtFees.ReadOnly = False
                txtGuideUrl.ReadOnly = False
                txtLoan.ReadOnly = False
                txtNonGuarBenefit.ReadOnly = False
                txtObjectiveChi.ReadOnly = False
                txtObjectiveEng.ReadOnly = False
                txtOther.ReadOnly = False
                txtPremiumType.ReadOnly = False
                txtProductTypeChi.ReadOnly = False
                txtProductTypeEng.ReadOnly = False
                txtRiskChi.ReadOnly = False
                txtRiskEng.ReadOnly = False
                txtEarlySurCharge.ReadOnly = False
                txtEarlySurrPeriod.ReadOnly = False

            Case FormModeEnum.Enquiry
                dgvProduct.Enabled = True

                cmdPlanNameFilter.Enabled = True
                cmdProductIdFilter.Enabled = True
                cmdProductAdd.Enabled = True
                cmdProductDelete.Enabled = True
                cmdProductCopy.Enabled = True
                cmdProductEdit.Enabled = True
                cmdProductCancel.Enabled = False
                cmdProductSave.Enabled = False
                cmdQuestionAdd.Enabled = True
                cmdQuestionRemove.Enabled = True

                cboCategory.Enabled = False
                chkFees.Enabled = False

                txtBenefit.ReadOnly = True
                txtBonus.ReadOnly = True
                txtCoupon.ReadOnly = True
                txtCSV.ReadOnly = True
                txtDividend.ReadOnly = True
                txtFees.ReadOnly = True
                txtGuideUrl.ReadOnly = True
                txtLoan.ReadOnly = True
                txtNonGuarBenefit.ReadOnly = True
                txtObjectiveChi.ReadOnly = True
                txtObjectiveEng.ReadOnly = True
                txtOther.ReadOnly = True
                txtPremiumType.ReadOnly = True
                txtProductTypeChi.ReadOnly = True
                txtProductTypeEng.ReadOnly = True
                txtRiskChi.ReadOnly = True
                txtRiskEng.ReadOnly = True
                txtEarlySurCharge.ReadOnly = True
                txtEarlySurrPeriod.ReadOnly = True

        End Select
    End Sub

    Private Sub frmPostSalesCallProductSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Init()
    End Sub


    Private Sub cmdProductIdFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductIdFilter.Click
        If txtProductIDFilter.Text.Trim() = String.Empty Then
            bsProduct.Filter = String.Empty
        Else
            bsProduct.Filter = "cswpsd_ProductID like '%" & txtProductIDFilter.Text.Trim() & "%'"
        End If

    End Sub

    Private Sub cmdPlanNameFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlanNameFilter.Click
        If txtPlanNameFilter.Text.Trim() = String.Empty Then
            bsProduct.Filter = String.Empty
        Else
            bsProduct.Filter = "Description like '%" & txtPlanNameFilter.Text.Trim() & "%'"
        End If
    End Sub

    Private Sub cmdProductSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductSave.Click
        If ValidateInput() = False Then Return

        If Save() Then
            SetUIMode(FormModeEnum.Enquiry)
            MsgBox("Save successful.", MsgBoxStyle.Information, "Message")
        End If
    End Sub

    Private Function ValidateInput() As Boolean
        Dim drvSetting As DataRowView

        bsProduct.EndEdit()
        drvSetting = bsProduct.Current

        If drvSetting("cswpsd_category") Is DBNull.Value Then
            MsgBox("Please select category of the product.", MsgBoxStyle.Exclamation, "Message")
            Return False
        End If


        Return True
    End Function

    Private Function Save() As Boolean
        Dim strErr As String = Nothing
        Dim dtData As DataTable = CType(bsProduct.DataSource, DataSet).Tables("Products").Clone()
        Dim dr As DataRow = CType(bsProduct.Current, DataRowView).Row

        dtData.ImportRow(dr)

        Select Case formMode
            Case FormModeEnum.AddNew
                'If clsCRS.AddPostSalesCallProductSetting(dtData, strErr) = False Then
                If AddPostSalesCallProductSetting(getCompanyCode(Me.objDBHeader.CompanyID), dtData, Me.objDBHeader.UserID) = False Then
                    'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                    Return False
                End If

            Case FormModeEnum.Modify
                'If clsCRS.UpdatePostSalesCallProductSetting(dtData, strErr) = False Then
                If UpdatePostSalesCallProductSetting(getCompanyCode(Me.objDBHeader.CompanyID), dtData, Me.objDBHeader.UserID) = False Then
                    'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                    Return False
                End If

        End Select

        Return True
    End Function

    Private Sub CheckProductSettingExists(ByVal sender As Object, ByVal productInfo As DataRowView, ByRef cancel As Boolean)
        'Dim blnExists As Boolean = False
        'Dim strErr As String = Nothing
        'If clsCRS.IsPostSalesCallProductSettingExists(productInfo("ProductID").ToString().Trim(), blnExists, strErr) = False Then
        If IsPostSalesCallProductSettingExists(getCompanyCode(Me.objDBHeader.CompanyID), productInfo("ProductID").ToString().Trim()) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            MsgBox("Selected product already exists.", MsgBoxStyle.Exclamation, "Message")
            cancel = True
        End If

        'If blnExists Then
        '    MsgBox("Selected product already exists.", MsgBoxStyle.Exclamation, "Message")
        '    cancel = True
        'End If
    End Sub


    Private Sub cmdProductAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductAdd.Click
        Using frmProductSelect As New frmPostSalesCallProductSelect()
            frmProductSelect.objDBHeader = Me.objDBHeader

            AddHandler frmProductSelect.ProductSelected, AddressOf CheckProductSettingExists

            If frmProductSelect.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If

            Dim drv As DataRowView = bsProduct.AddNew()
            drv("cswpsd_ProductID") = frmProductSelect.SelectedProduct("ProductID")
            drv("Description") = frmProductSelect.SelectedProduct("Description")
            drv("ChineseDescription") = frmProductSelect.SelectedProduct("ChineseDescription")
            bsProduct.ResetCurrentItem()

            SetUIMode(FormModeEnum.AddNew)
        End Using
    End Sub

    Private Sub cmdQuestionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuestionAdd.Click
        Dim strErr As String = Nothing

        If bsProduct.Current Is Nothing Then
            MsgBox("No product to selected.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        Using frmQuestionSelect As New frmPostSalesQuestionnaireMaster(FormModeEnum.Select)
            frmQuestionSelect.objDBHeader = Me.objDBHeader

            If frmQuestionSelect.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If

            Dim drQuestion As DataRow = CType(bsProduct.DataSource, DataSet).Tables("Questions").NewRow()
            drQuestion("cswpsn_question_id") = frmQuestionSelect.SelectedQuestion("cswpsq_question_id")
            drQuestion("cswpsq_question_id") = frmQuestionSelect.SelectedQuestion("cswpsq_question_id")
            drQuestion("cswpsq_question_no") = frmQuestionSelect.SelectedQuestion("cswpsq_question_no")
            drQuestion("cswpsq_description") = frmQuestionSelect.SelectedQuestion("cswpsq_description")
            drQuestion("cswpsq_vc") = frmQuestionSelect.SelectedQuestion("cswpsq_vc")
            drQuestion("cswpsq_sm") = frmQuestionSelect.SelectedQuestion("cswpsq_sm")
            drQuestion("cswpsq_no_fna") = frmQuestionSelect.SelectedQuestion("cswpsq_no_fna")
            drQuestion("cswpsn_order") = bsQuestion.Count + 1
            drQuestion("cswpsn_ProductID") = CType(bsProduct.Current, DataRowView)("cswpsd_ProductID")

            'Dim drvQuestion As DataRowView = bsQuestion.AddNew()
            'drvQuestion("cswpsn_question_id") = frmQuestionSelect.SelectedQuestion("cswpsq_question_id")
            'drvQuestion("cswpsq_question_no") = frmQuestionSelect.SelectedQuestion("cswpsq_question_no")
            'drvQuestion("cswpsq_description") = frmQuestionSelect.SelectedQuestion("cswpsq_description")
            'drvQuestion("cswpsq_vc") = frmQuestionSelect.SelectedQuestion("cswpsq_vc")
            'drvQuestion("cswpsq_sm") = frmQuestionSelect.SelectedQuestion("cswpsq_sm")
            'drvQuestion("cswpsn_order") = bsQuestion.Count + 1
            'drvQuestion("cswpsn_ProductID") = CType(bsProduct.Current, DataRowView)("cswpsd_ProductID")
            'bsQuestion.EndEdit()
            'bsQuestion.ResetCurrentItem()

            ' update to DB
            Dim dt As DataTable = CType(bsProduct.DataSource, DataSet).Tables("Questions").Clone()
            'CType(bsProduct.Current, DataRowView)("cswpsq_question_id") = 0
            dt.Rows.Add(drQuestion.ItemArray)

            Dim blnExists As Boolean = False
            'If Not clsCRS.AddPostSalesCallProductQuestion(dt, blnExists, strErr) Then
            If Not AddPostSalesCallProductQuestion(dt, Me.objDBHeader.UserID, blnExists, getCompanyCode(Me.objDBHeader.CompanyID)) Then
                'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                Return
            End If

            If blnExists Then
                MsgBox("The selected question already exists.", MsgBoxStyle.Exclamation, "Message")
                Return
            End If

            CType(bsProduct.DataSource, DataSet).Tables("Questions").Rows.Add(drQuestion)
            'bsQuestion.ResetBindings(False)

        End Using

        MsgBox("Question added.", MsgBoxStyle.Information, "Message")

    End Sub

    Private Sub cmdQuestionRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuestionRemove.Click
        Dim strErr As String = Nothing

        If bsQuestion.Current Is Nothing Then
            MsgBox("No question to remove.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        Dim drv As DataRowView = CType(bsQuestion.Current, DataRowView)
        If MsgBox("Remove question " & drv("cswpsq_question_no").ToString().Trim() & " ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Return
        End If

        'If Not clsCRS.RemovePostSalesCallProductQuestion(drv("cswpsn_ProductID").ToString().Trim(), drv("cswpsn_question_id"), strErr) Then
        If Not RemovePostSalesCallProductQuestion(getCompanyCode(Me.objDBHeader.CompanyID), drv("cswpsn_ProductID").ToString().Trim(), drv("cswpsn_question_id")) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        bsQuestion.RemoveCurrent()

        MsgBox("Question removed.", MsgBoxStyle.Information, "Message")

    End Sub

    Private Sub cmdProductEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductEdit.Click
        If bsProduct.Current Is Nothing Then
            MsgBox("No product to edit.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        SetUIMode(FormModeEnum.Modify)
    End Sub

    Private Sub cmdProductCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductCancel.Click
        bsProduct.CancelEdit()
        bsProduct.ResetCurrentItem()

        SetUIMode(FormModeEnum.Enquiry)
    End Sub

    Private Sub cmdProductDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductDelete.Click
        Dim drv As DataRowView
        Dim strErr As String = Nothing

        If bsProduct.Current Is Nothing Then
            MsgBox("No product to delete.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        drv = CType(bsProduct.Current, DataRowView)
        If MsgBox("Delete settings of product " & drv("cswpsd_ProductID").ToString().Trim() & "?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm") <> MsgBoxResult.Yes Then
            Return
        End If

        'If Not clsCRS.DeletePostSalesCallProductSetting(drv("cswpsd_ProductID").ToString().Trim(), strErr) Then
        If Not DeletePostSalesCallProductSetting(Me.objDBHeader.CompanyID, drv("cswpsd_ProductID").ToString().Trim()) Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        bsProduct.RemoveCurrent()
        MsgBox("Product settings deleted.", MsgBoxStyle.Information, "Message")

    End Sub

    Private Sub cmdProductCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProductCopy.Click
        If bsProduct.Current Is Nothing Then
            MsgBox("No product selected to copy.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        Using frmProductSelect As New frmPostSalesCallProductSelect()
            frmProductSelect.objDBHeader = Me.objDBHeader

            AddHandler frmProductSelect.ProductSelected, AddressOf CheckProductSettingExists

            If frmProductSelect.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If

            Dim strFromProductId As String = CType(bsProduct.Current, DataRowView)("cswpsd_ProductID").ToString().Trim()
            Dim strToProductId As String = frmProductSelect.SelectedProduct("ProductID")
            Dim strErr As String = Nothing

            'If Not clsCRS.CopySalesCallProductSetting(strFromProductId, strToProductId, strErr) Then
            Try
                If Not CopySalesCallProductSetting(Me.objDBHeader.CompanyID, Me.objDBHeader.UserID, strFromProductId, strToProductId) Then
                    'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                    Return
                End If
            Catch ex As Exception
                Return
            End Try
        End Using

        LoadData()
        MsgBox("Product setting copied.", MsgBoxStyle.Information, "Message")

    End Sub

End Class