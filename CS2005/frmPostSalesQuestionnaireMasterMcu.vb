Public Class frmPostSalesQuestionnaireMasterMcu

    Public SelectedQuestion As DataRow
    Public objDBHeader As Utility.Utility.ComHeader

    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Private dtQuestions As DataTable
    Private formMode As FormModeEnum = FormModeEnum.AddNew

    Private Sub Init()
        Dim strErr As String = Nothing

        dgvQuestions.AutoGenerateColumns = False

        Dim dtQuestionnaire As DataTable = GetCodeTable("CRSQN")
        cboQuestionnaire.DataSource = dtQuestionnaire
        cboQuestionnaire.DisplayMember = "Description"
        cboQuestionnaire.ValueMember = "Value"

        colQuestionnaire.DataSource = dtQuestionnaire
        colQuestionnaire.DisplayMember = "Description"
        colQuestionnaire.ValueMember = "Value"

        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = objDBHeader

        ' retrieve data
        'If Not clsCRS.RetrievePostSalesCallQuestions(dtQuestions, strErr) Then
        Try
            If Not RetrievePostSalesCallQuestions(getCompanyCode(g_McuComp), dtQuestions) Then
                'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Add questions failed", MsgBoxStyle.Critical, "Error")
            Return
        End Try

        dtQuestions.Columns("cswpsq_questionnaire_code").DefaultValue = dtQuestionnaire.Rows(0)("Value")
        dtQuestions.Columns("cswpsq_vc").DefaultValue = "N"
        dtQuestions.Columns("cswpsq_sm").DefaultValue = "N"
        dtQuestions.Columns("cswpsq_no_fna").DefaultValue = "N"

        ' bind data
        bsQuestions.DataSource = dtQuestions
        dgvQuestions.DataSource = bsQuestions

        txtQuestionNo.DataBindings.Add("Text", bsQuestions, "cswpsq_question_no")
        txtQuestion.DataBindings.Add("Text", bsQuestions, "cswpsq_description")
        cboQuestionnaire.DataBindings.Add("SelectedValue", bsQuestions, "cswpsq_questionnaire_code")

        AddHandler txtQuestion.DataBindings("Text").Format, AddressOf StringCarriageReturnConvertEventHandler

        Dim bindingVC As New Binding("Checked", bsQuestions, "cswpsq_vc")
        AddHandler bindingVC.Format, AddressOf CharToBooleanConvertEventHandler
        AddHandler bindingVC.Parse, AddressOf BooleanToCharConvertEventHandler
        chkVC.DataBindings.Add(bindingVC)

        Dim bindingSM As New Binding("Checked", bsQuestions, "cswpsq_sm")
        AddHandler bindingSM.Format, AddressOf CharToBooleanConvertEventHandler
        AddHandler bindingSM.Parse, AddressOf BooleanToCharConvertEventHandler
        chkSM.DataBindings.Add(bindingSM)

        Dim bindingNoFNA As New Binding("Checked", bsQuestions, "cswpsq_no_fna")
        AddHandler bindingNoFNA.Format, AddressOf CharToBooleanConvertEventHandler
        AddHandler bindingNoFNA.Parse, AddressOf BooleanToCharConvertEventHandler
        chkNoFNA.DataBindings.Add(bindingNoFNA)


        If formMode = FormModeEnum.Select Then
            cmdExit.Text = "Cancel"
        End If

    End Sub

    Private Sub frmPostSalesQuestionnaireMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If clsCRS IsNot Nothing Then
            clsCRS.Dispose()
        End If
    End Sub

    Private Sub frmPostSalesQuestionnaireMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Init()

        SetUIMode(formMode)
    End Sub

    Public Sub New(ByVal mode As FormModeEnum)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        formMode = mode
    End Sub

    Private Sub SetUIMode(ByVal mode As FormModeEnum)
        formMode = mode

        Select Case formMode
            Case FormModeEnum.Enquiry
                dgvQuestions.Enabled = True
                cmdNew.Enabled = True
                cmdEdit.Enabled = True
                cmdSave.Enabled = False
                cmdCancel.Enabled = False
                cmdDelete.Enabled = True
                cmdSelect.Enabled = False

                txtQuestionNo.ReadOnly = True
                txtQuestion.ReadOnly = True
                cboQuestionnaire.Enabled = False
                chkSM.Enabled = False
                chkVC.Enabled = False
                chkNoFNA.Enabled = False

            Case FormModeEnum.AddNew, FormModeEnum.Modify
                dgvQuestions.Enabled = False
                cmdNew.Enabled = False
                cmdEdit.Enabled = False
                cmdSave.Enabled = True
                cmdCancel.Enabled = True
                cmdDelete.Enabled = False
                cmdSelect.Enabled = False

                txtQuestionNo.ReadOnly = False
                txtQuestion.ReadOnly = False
                cboQuestionnaire.Enabled = True
                chkSM.Enabled = True
                chkVC.Enabled = True
                chkNoFNA.Enabled = True

            Case FormModeEnum.Select
                dgvQuestions.Enabled = True
                cmdNew.Visible = False
                cmdEdit.Visible = False
                cmdSave.Visible = False
                cmdCancel.Visible = False
                cmdDelete.Visible = False
                cmdSelect.Enabled = True

                txtQuestionNo.ReadOnly = True
                txtQuestion.ReadOnly = True
                cboQuestionnaire.Enabled = False
                chkSM.Enabled = False
                chkVC.Enabled = False
                chkNoFNA.Enabled = False

        End Select
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        bsQuestions.AddNew()

        SetUIMode(FormModeEnum.AddNew)

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If bsQuestions.Current Is Nothing Then Return
        SetUIMode(FormModeEnum.Modify)

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        bsQuestions.CancelEdit()
        SetUIMode(FormModeEnum.Enquiry)

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim id As Integer = CType(bsQuestions.Current, DataRowView)("cswpsq_question_id")
        Dim strErr As String = Nothing

        If ValidateDelete(id) Then

            If MsgBox("Delete selected question?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm") = MsgBoxResult.Yes Then
                'If clsCRS.DeletePostSalesCallQuestion(id, strErr) = False Then
                If DeletePostSalesCallQuestion(getCompanyCode(Me.objDBHeader.CompanyID), id) = False Then
                    'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                    Return
                End If

                SetUIMode(FormModeEnum.Enquiry)
                bsQuestions.RemoveCurrent()
                MsgBox("Delete successful.", MsgBoxStyle.Information, "Message")
            End If
        End If
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        If bsQuestions.Current Is Nothing Then
            MsgBox("No question available for selection.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        Me.SelectedQuestion = CType(bsQuestions.Current, DataRowView).Row

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If ValidateInput() = False Then Return

        If Save() Then
            SetUIMode(FormModeEnum.Enquiry)
            MsgBox("Save successful.", MsgBoxStyle.Information, "Message")
        End If
    End Sub

    Private Function ValidateInput() As Boolean

        bsQuestions.EndEdit()

        If txtQuestionNo.Text.Trim() = String.Empty Then
            MsgBox("Please input Question No.", MsgBoxStyle.Exclamation, "Message")
            Return False
        End If

        If txtQuestion.Text.Trim() = String.Empty Then
            MsgBox("Please input Question.", MsgBoxStyle.Exclamation, "Message")
            Return False
        End If

        Return True
    End Function

    Private Function ValidateDelete(ByVal id As Integer) As Boolean

        If IsPostSalesCallQuestionInUse(getCompanyCode(Me.objDBHeader.CompanyID), id) Then
            'MsgBox("Cannot delete question which linked to product.", MsgBoxStyle.Exclamation, "Message")
            Return False
        End If

        Return True
    End Function

    Private Function Save() As Boolean
        Dim strErr As String = Nothing
        Dim dtData As DataTable = CType(bsQuestions.DataSource, DataTable).Clone()
        Dim dr As DataRow = CType(bsQuestions.Current, DataRowView).Row

        dr("cswpsq_answer_type") = "MC"
        dr("cswpsq_answer_template") = 1

        dtData.ImportRow(dr)

        'If clsCRS.SavePostSalesCallQuestion(formMode, dtData, strErr) = False Then
        If SavePostSalesCallQuestion(getCompanyCode(g_McuComp), Me.objDBHeader.UserID, formMode, dtData) = False Then
            'MsgBox(strErr, MsgBoxStyle.Critical, "Error")
        End If

        If formMode = FormModeEnum.AddNew Then
            dr("cswpsq_question_id") = dtData.Rows(0)("cswpsq_question_id")
        End If

        Return True
    End Function

End Class