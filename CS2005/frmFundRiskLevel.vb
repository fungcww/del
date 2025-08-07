Public Class frmFundRiskLevel

    Public objDBHeader As Utility.Utility.ComHeader

    Private dtFund As DataTable
    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Private dtRiskLevel As DataTable
    Private formMode As FormModeEnum

    Private Sub frmFundRiskLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strErr As String = Nothing

        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = objDBHeader

        SetUIMode(FormModeEnum.Enquiry)

        If Not clsCRS.RetrieveInvestmentFund(dtFund, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        bsFund.DataSource = dtFund
        dgvFund.AutoGenerateColumns = False
        dgvFund.DataSource = bsFund

        dtRiskLevel = New DataTable()
        dtRiskLevel.Columns.Add("RiskCode", GetType(String))
        dtRiskLevel.Columns.Add("RiskDesc", GetType(String))
        dtRiskLevel.Rows.Add(DBNull.Value, "")
        dtRiskLevel.Rows.Add("H", "High")

        colRisk.DataSource = dtRiskLevel
        colRisk.DisplayMember = "RiskDesc"
        colRisk.ValueMember = "RiskCode"

        cboRiskLevel.DataSource = dtRiskLevel
        cboRiskLevel.DisplayMember = "RiskDesc"
        cboRiskLevel.ValueMember = "RiskCode"

        cboRiskLevel.DataBindings.Add("SelectedValue", bsFund, "mpfinv_risk_level")
        txtFundCode.DataBindings.Add("Text", bsFund, "mpfinv_code")
        txtFundName.DataBindings.Add("Text", bsFund, "mpfinv_chi_desc")

    End Sub

    Public Sub SetUIMode(ByVal mode As FormModeEnum)
        formMode = mode

        Select Case formMode
            Case FormModeEnum.Modify
                cboRiskLevel.Enabled = True

                cmdSave.Enabled = True
                cmdCancel.Enabled = True
                cmdEdit.Enabled = False
                cmdFilter.Enabled = False

                dgvFund.Enabled = False

            Case FormModeEnum.Enquiry
                cboRiskLevel.Enabled = False

                cmdSave.Enabled = False
                cmdCancel.Enabled = False
                cmdEdit.Enabled = True
                cmdFilter.Enabled = True

                dgvFund.Enabled = True
        End Select

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        SetUIMode(FormModeEnum.Modify)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        bsFund.CancelEdit()
        SetUIMode(FormModeEnum.Enquiry)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        bsFund.EndEdit()

        If Save() Then
            SetUIMode(FormModeEnum.Enquiry)
            MsgBox("Record updated.", MsgBoxStyle.Information, "Message")
        End If

    End Sub

    Private Function Save() As Boolean
        Dim dt As DataTable = dtFund.Clone()
        Dim strErr As String = Nothing

        dt.ImportRow(CType(bsFund.Current, DataRowView).Row)
        If Not clsCRS.UpdateFundRiskLevel(dt, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return False
        End If

        Return True
    End Function

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilter.Click
        If txtFundCodeFilter.Text.Trim() = String.Empty Then
            bsFund.Filter = String.Empty
        Else
            bsFund.Filter = "mpfinv_code like '%" & txtFundCodeFilter.Text.Trim() & "%'"
        End If
    End Sub
End Class