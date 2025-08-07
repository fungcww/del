'C001 - Add MCV Indicator in HK & Macau Service Log Screen - Gopu Kalaimani.

Imports System
Imports System.Data.SqlClient

Public Class frmMCUTransLog

#Region "   Common Declartion  "
    Dim str_TXT_Default_Text As String = String.Empty
    Dim int_CBO_Default_Index As Integer = -1
    Dim dte_DTE_Long_Format As String = "yyyy/MM/dd hh:mm tt"
    Dim dte_DTE_Custom_Format As String = "MMMM dd, yyyy hh:mm tt"
    Dim dte_DTE_Default_Date As Date = Date.Now
    Dim dte_DTE_NULL_Date As String = "1900/01/01"

    Dim strError As String = String.Empty
    Dim strMode As String = String.Empty  'A-->Add, V--Edit
    Dim strDivisionNo As String = String.Empty
    Dim strUnitCode As String = String.Empty

    Dim str_Medium_Default_Value As String = str_TXT_Default_Text
    Dim str_EventCategory_Default_Value As String = str_TXT_Default_Text
    Dim str_EventDetail_Default_Value As String = str_TXT_Default_Text
    Dim str_EventTypeDetail_Default_Value As String = str_TXT_Default_Text
    Dim str_EventSourceInd_Default_Value As String = str_TXT_Default_Text
    Dim str_Status_Default_Value As String = str_TXT_Default_Text

    Dim ServiceLogBL As New ServiceLogBL 'ITDCPI New ServiceLogBL replacing POSWS

#End Region

    Private Sub frmMCUTransLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Get combo box default value
            str_Medium_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("MEDIUM_DEFAULT")
            str_EventCategory_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("EVENTCATEGORY_DEFAULT")
            str_EventDetail_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("EVENTDETAIL_DEFAULT")
            str_EventTypeDetail_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("EVENTTYPEDETAIL_DEFAULT")
            str_EventSourceInd_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("EVENTSOURCEIND_DEFAULT")
            str_Status_Default_Value = Configuration.ConfigurationSettings.AppSettings.Item("STATUS_DEFAULT")

            Me.BuildUserCombo(cboUser)
            Me.BuildFollowUpCombo(cboSearchNeedFollowUp)
            Me.GetSearchStatusList()
            Me.Reset()
            txtPolicyNo.ReadOnly = True
            dgvServiceLog.AutoGenerateColumns = False
            ServiceLogBL.GetSerlogPreference(cboMedium, cboEventSourceInd)
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            strMode = "A"
            Me.Reset()
            txtPolicyNo.ReadOnly = False
            txtPolicyNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

        Try
            'If Not Date.TryParse(String.Format("{0}/{1}/01", mtxtYear.Text, cboMonth.Text), dteServiceDate) Then
            '    MessageBox.Show("Please Input Valid Year and Month.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            '    Exit Sub
            'End If

            If strMode = "A" And txtPolicyNo.Text.Trim.Length > 0 Then
                If MessageBox.Show("Do you want to continue without save service log?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            strMode = "V"

            Me.Reset()

            txtPolicyNo.ReadOnly = True
            RemoveHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
            dgvServiceLog.DataSource = Nothing
            AddHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged

            Me.Get_MCU_ServiceLog(mtxtYear.Text.Trim, cboMonth.Text.Trim, cboUser.Text.Trim, txtSearchByPolicy.Text.Trim, cboSearchStatus.SelectedValue, cboSearchNeedFollowUp.SelectedValue)

            dgvServiceLog.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Try

            Dim strValidationMessage As String
            If Not Me.Validation(strValidationMessage) Then
                MessageBox.Show(strValidationMessage, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If

            Dim dsResult As New DataSet
            Dim dtResult As New DataTable
            Dim dRow As DataRow

            Me.BuildDS(dtResult)
            If strMode = "A" Then
                'Add New Record
                dRow = dtResult.NewRow
                dRow("ServiceEventNumber") = str_TXT_Default_Text

                If (cboEventCategory.SelectedIndex = -1 OrElse cboEventCategory.SelectedValue Is Nothing) Then
                    dRow("EventCategoryCode") = str_TXT_Default_Text
                Else
                    dRow("EventCategoryCode") = cboEventCategory.SelectedValue.ToString().Trim
                End If

                If (cboEventDetail.SelectedIndex = -1 OrElse cboEventDetail.SelectedValue Is Nothing) Then
                    dRow("EventTypeCode") = str_TXT_Default_Text
                Else
                    dRow("EventTypeCode") = cboEventDetail.SelectedValue.ToString().Trim
                End If

                dRow("EventTypeActionCode") = ""
                If txtCustomerID.Text.Trim.Length = 0 Then
                    dRow("CustomerID") = "0"
                Else
                    dRow("CustomerID") = txtCustomerID.Text.Trim
                End If

                dRow("PolicyAccountID") = txtPolicyNo.Text.Trim
                dRow("MasterCSRID") = gsUser 'Log in User ID
                dRow("SecondaryCSRID") = str_TXT_Default_Text
                'dRow("EventInitialDateTime") = dteEffectiveDate.Value.Add(Date.Now.TimeOfDay).ToString(dte_DTE_Long_Format)

                'dteEffectiveDate.Value = New DateTime(dteEffectiveDate.Value.Year, dteEffectiveDate.Value.Month, dteEffectiveDate.Value.Day).Add(Date.Now.TimeOfDay)
                dRow("EventInitialDateTime") = dteEffectiveDate.Value.ToString(dte_DTE_Long_Format)
                dRow("EventAssignDateTime") = dte_DTE_NULL_Date
                'If cboStatus.SelectedValue.ToString().Trim = "H" Then
                '    dRow("EventCompletionDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                'Else
                dRow("EventCompletionDateTime") = dte_DTE_NULL_Date
                'End If
                'dRow("EventCloseoutCode") = str_TXT_Default_Text
                If cboStatus.SelectedValue.ToString().Trim = "C" Then
                    dRow("EventCloseoutDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                    dRow("EventCloseoutCSRID") = gsUser 'Login User ID
                    'dRow("EventInitialDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                Else
                    dRow("EventCloseoutDateTime") = dte_DTE_NULL_Date
                    dRow("EventCloseoutCSRID") = str_TXT_Default_Text
                End If

                If (cboStatus.SelectedIndex = -1 OrElse cboStatus.SelectedValue Is Nothing) Then
                    dRow("EventStatusCode") = str_TXT_Default_Text
                Else
                    dRow("EventStatusCode") = cboStatus.SelectedValue.ToString.Trim()
                End If

                If (cboEventSourceInd.SelectedIndex = -1 OrElse cboEventSourceInd.SelectedValue Is Nothing) Then
                    dRow("EventSourceInitiatorCode") = str_TXT_Default_Text
                Else
                    dRow("EventSourceInitiatorCode") = cboEventSourceInd.SelectedValue.ToString.Trim()
                End If

                If (cboMedium.SelectedIndex = -1 OrElse cboMedium.SelectedValue Is Nothing) Then
                    dRow("EventSourceMediumCode") = str_TXT_Default_Text
                Else
                    dRow("EventSourceMediumCode") = cboMedium.SelectedValue.ToString.Trim()
                End If

                dRow("EventNotes") = txtEventNote.Text.Trim.Trim.Replace("'", "''") 'Replace ' with "
                dRow("ReminderDate") = dte_DTE_NULL_Date
                dRow("ReminderNotes") = txtRemainder.Text.Trim.Trim.Replace("'", "''") 'Replace ' with "

                If (cboEventTypeDetail.SelectedIndex = -1 OrElse cboEventTypeDetail.SelectedValue Is Nothing) Then
                    dRow("EventTypeDetailCode") = str_TXT_Default_Text
                Else
                    dRow("EventTypeDetailCode") = cboEventTypeDetail.SelectedValue.ToString.Trim
                End If

                dRow("AgentCode") = txtSACode.Text.Trim
                dRow("DivisionCode") = strDivisionNo
                dRow("UnitCode") = strUnitCode
                dRow("CaseNo") = str_TXT_Default_Text
                dRow("UpdateUser") = gsUser 'Login User
                dRow("BirthdayAlert") = "0"
                dRow("IsTransferToAES") = "0"
                dRow("IsAppearedInAES") = "0"
                dRow("IsPolicyAlert") = "0"
                dRow("AlertNotes") = str_TXT_Default_Text
                If chkFollowUp.Checked Then
                    dRow("FollowUpByMacau") = "1"
                Else
                    dRow("FollowUpByMacau") = "0"
                End If
                If chkFCR.Checked Then
                    dRow("EventCloseoutCode") = "Y"
                Else
                    dRow("EventCloseoutCode") = "N"
                End If
                'C001 - Start
                If chkMCV.Checked Then
                    dRow("MCV") = "Y"
                Else
                    dRow("MCV") = "N"
                End If
                'C001 - End
            Else
                'Update existing Record


                Dim RowIndex As Integer = -1

                If (Not dgvServiceLog.CurrentCell Is Nothing) Then
                    RowIndex = dgvServiceLog.CurrentCell.RowIndex
                End If

                dRow = dtResult.NewRow
                If RowIndex >= 0 Then
                    dRow("ServiceEventNumber") = dgvServiceLog.Rows(RowIndex).Cells("ServiceEventNumber").Value

                    If (cboEventCategory.SelectedIndex = -1 OrElse cboEventCategory.SelectedValue Is Nothing) Then
                        dRow("EventCategoryCode") = str_TXT_Default_Text
                    Else
                        dRow("EventCategoryCode") = cboEventCategory.SelectedValue.ToString().Trim
                    End If

                    If (cboEventDetail.SelectedIndex = -1 OrElse cboEventDetail.SelectedValue Is Nothing) Then
                        dRow("EventTypeCode") = str_TXT_Default_Text
                    Else
                        dRow("EventTypeCode") = cboEventDetail.SelectedValue.ToString().Trim
                    End If

                    dRow("EventTypeActionCode") = str_TXT_Default_Text
                    dRow("CustomerID") = dgvServiceLog.Rows(RowIndex).Cells("CustomerID").Value
                    dRow("PolicyAccountID") = dgvServiceLog.Rows(RowIndex).Cells("PolicyAccountID").Value
                    dRow("MasterCSRID") = gsUser 'Log in User ID
                    dRow("SecondaryCSRID") = str_TXT_Default_Text

                    'dteEffectiveDate.Value = New DateTime(dteEffectiveDate.Value.Year, dteEffectiveDate.Value.Month, dteEffectiveDate.Value.Day).Add(Date.Now.TimeOfDay)

                    dRow("EventInitialDateTime") = dteEffectiveDate.Value.ToString(dte_DTE_Long_Format) 'dgvServiceLog.Rows(RowIndex).Cells("EventInitialDateTime").Value
                    dRow("EventAssignDateTime") = dte_DTE_Default_Date
                    'If cboStatus.SelectedValue.ToString().Trim = "H" Then
                    '    dRow("EventCompletionDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                    'Else
                    dRow("EventCompletionDateTime") = dgvServiceLog.Rows(RowIndex).Cells("EventCompletionDateTime").Value
                    'End If
                    'dRow("EventCloseoutCode") = str_TXT_Default_Text
                    If cboStatus.SelectedValue.ToString().Trim = "C" Then
                        dRow("EventCloseoutDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                        dRow("EventCloseoutCSRID") = gsUser 'Login User ID
                        'dRow("EventInitialDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                    Else
                        dRow("EventCloseoutDateTime") = dgvServiceLog.Rows(RowIndex).Cells("EventCloseoutDateTime").Value
                        dRow("EventCloseoutCSRID") = str_TXT_Default_Text
                    End If

                    If (cboStatus.SelectedIndex = -1 OrElse cboStatus.SelectedValue Is Nothing) Then
                        dRow("EventStatusCode") = str_TXT_Default_Text
                    Else
                        dRow("EventStatusCode") = cboStatus.SelectedValue.ToString.Trim()
                    End If

                    If (cboEventSourceInd.SelectedIndex = -1 OrElse cboEventSourceInd.SelectedValue Is Nothing) Then
                        dRow("EventSourceInitiatorCode") = str_TXT_Default_Text
                    Else
                        dRow("EventSourceInitiatorCode") = cboEventSourceInd.SelectedValue.ToString.Trim()
                    End If

                    If (cboMedium.SelectedIndex = -1 OrElse cboMedium.SelectedValue Is Nothing) Then
                        dRow("EventSourceMediumCode") = str_TXT_Default_Text
                    Else
                        dRow("EventSourceMediumCode") = cboMedium.SelectedValue.ToString.Trim()
                    End If

                    dRow("EventNotes") = txtEventNote.Text.Trim.Replace("'", "''") 'Replace ' with "
                    dRow("ReminderDate") = dte_DTE_NULL_Date
                    dRow("ReminderNotes") = txtRemainder.Text.Trim.Replace("'", "''") 'Replace ' with "

                    If (cboEventTypeDetail.SelectedIndex = -1 OrElse cboEventTypeDetail.SelectedValue Is Nothing) Then
                        dRow("EventTypeDetailCode") = str_TXT_Default_Text
                    Else
                        dRow("EventTypeDetailCode") = cboEventTypeDetail.SelectedValue.ToString.Trim
                    End If

                    dRow("AgentCode") = dgvServiceLog.Rows(RowIndex).Cells("AgentCode").Value
                    dRow("DivisionCode") = dgvServiceLog.Rows(RowIndex).Cells("DivisionCode").Value
                    dRow("UnitCode") = dgvServiceLog.Rows(RowIndex).Cells("UnitCode").Value
                    dRow("CaseNo") = str_TXT_Default_Text
                    dRow("UpdateUser") = gsUser 'Login User
                    dRow("BirthdayAlert") = "0"
                    dRow("IsTransferToAES") = "0"
                    dRow("IsAppearedInAES") = "0"
                    dRow("IsPolicyAlert") = "0"
                    dRow("AlertNotes") = str_TXT_Default_Text
                    If chkFollowUp.Checked Then
                        dRow("FollowUpByMacau") = "1"
                    Else
                        dRow("FollowUpByMacau") = "0"
                    End If
                    If chkFCR.Checked Then
                        dRow("EventCloseoutCode") = "Y"
                    Else
                        dRow("EventCloseoutCode") = "N"
                    End If
                    'C001 - Start
                    If chkMCV.Checked Then
                        dRow("MCV") = "Y"
                    Else
                        dRow("MCV") = "N"
                    End If
                    'C001 - End
                End If
            End If
            dtResult.Rows.Add(dRow)
            dsResult.Tables.Add(dtResult)

            'ITDCPI Use BL in CS2005 project instead of POSWS start
            'If Not POSWS_HK().Save_MCU_ServiceLog(strMode, dsResult, strError) Then
            '    Throw New Exception(strError)
            'End If
            If Not ServiceLogBL.Save_MCU_ServiceLog(strMode, dsResult, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS start

            'txtPolicyNo.ReadOnly = True
            If strMode.Trim.ToUpper = "A" Then
                Me.Reset()
                txtPolicyNo.Focus()
            Else
                Me.Get_MCU_ServiceLog(mtxtYear.Text.Trim, cboMonth.Text.Trim, cboUser.Text.Trim, txtSearchByPolicy.Text.Trim, cboSearchStatus.SelectedValue, cboSearchNeedFollowUp.SelectedValue)
                strMode = "V"
            End If

            MessageBox.Show("Record Saved Successfully!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Try

            If DialogResult.Yes = MessageBox.Show("Do you want to close this window?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) Then
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub dgvServiceLog_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvServiceLog.SelectionChanged

        Try

            If (Not dgvServiceLog.CurrentCell Is Nothing) AndAlso dgvServiceLog.CurrentCell.RowIndex >= 0 Then
                If strMode = "A" And txtPolicyNo.Text.Trim <> "" And Convert.ToString(dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex).Cells("PolicyAccountID").Value).Trim <> txtPolicyNo.Text.Trim Then
                    If MessageBox.Show("Do you want to continue without save service log?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                strMode = "V"

                Me.PopulatedData(dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub dgvServiceLog_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvServiceLog.KeyDown

        Try
            If dgvServiceLog.RowCount > 0 AndAlso (Not dgvServiceLog.CurrentCell Is Nothing) Then
                If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
                    dgvServiceLog.CurrentCell = dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex).Cells("MCV")
                End If

                If e.Shift = True AndAlso (e.KeyCode = Keys.Tab) AndAlso (dgvServiceLog.CurrentCell.RowIndex > 0) Then
                    RemoveHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
                    dgvServiceLog.CurrentCell = dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex - 1).Cells("MCV")
                    AddHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged

                    Me.PopulatedData(dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex))

                    e.Handled = True
                ElseIf e.Shift = False AndAlso (e.KeyCode = Keys.Tab) AndAlso (dgvServiceLog.CurrentCell.RowIndex < dgvServiceLog.Rows.Count - 1) Then
                    RemoveHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
                    dgvServiceLog.CurrentCell = dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex + 1).Cells("MCV")
                    AddHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged

                    Me.PopulatedData(dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex))

                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub txtPolicyNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPolicyNo.Leave

        Try
            If strMode = "A" AndAlso txtPolicyNo.Text.Trim <> "" Then

                Me.SearchPolicy_MCU(txtPolicyNo.Text.Trim)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub cboEventCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEventCategory.SelectedIndexChanged

        Try
            If cboEventCategory.SelectedIndex > -1 Then

                Me.GetEventDetail(cboEventCategory.SelectedValue.ToString().Trim)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub cboEventDetail_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEventDetail.SelectedIndexChanged

        Try
            If cboEventCategory.SelectedIndex > -1 AndAlso cboEventDetail.SelectedIndex > -1 Then

                Me.GetEventTypeDetail(cboEventCategory.SelectedValue.ToString().Trim, cboEventDetail.SelectedValue.ToString().Trim)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub cboBasicPlan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboBasicPlan.KeyDown
        Me.SupressKey(e)
    End Sub

    Private Sub cboAccountStatus_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboAccountStatus.KeyDown
        Me.SupressKey(e)
    End Sub

    Private Sub cboServicingLocationCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboServicingLocationCode.KeyDown
        Me.SupressKey(e)
    End Sub

#Region "Common Function"

    Private Sub Reset()

        Try

            'mtxtYear.Text = Date.Now.Year.ToString("0000")
            'mtxtMonth.Text = Date.Now.Month.ToString("00")
            txtPolicyNo.Text = str_TXT_Default_Text
            txtCustomerID.Text = str_TXT_Default_Text
            txtSACode.Text = str_TXT_Default_Text
            txtAgentName.Text = str_TXT_Default_Text
            'txtInitiator.Text = str_TXT_Default_Text
            txtEventNote.Text = str_TXT_Default_Text
            txtRemainder.Text = str_TXT_Default_Text

            Me.GetBasicPlan()
            Me.GetAccountStatus()
            Me.GetAgentLocation()

            cboBasicPlan.SelectedIndex = int_CBO_Default_Index
            cboAccountStatus.SelectedIndex = int_CBO_Default_Index
            cboServicingLocationCode.SelectedIndex = int_CBO_Default_Index

            'dteEffectiveDate.Value = dte_DTE_Default_Date.ToString(dte_DTE_Long_Format)
            dteEffectiveDate.Value = dte_DTE_Default_Date.ToString(dte_DTE_Custom_Format)

            Me.GetMedium()

            'Event Cateogry
            RemoveHandler cboEventCategory.SelectedIndexChanged, AddressOf cboEventCategory_SelectedIndexChanged
            Me.GetEventCategory()
            AddHandler cboEventCategory.SelectedIndexChanged, AddressOf cboEventCategory_SelectedIndexChanged

            'Event Detail
            RemoveHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged
            cboEventDetail.DataSource = Nothing
            Me.GetEventDetail(cboEventCategory.SelectedValue)
            cboEventDetail.SelectedValue = str_EventDetail_Default_Value
            AddHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged

            cboEventTypeDetail.DataSource = Nothing

            Me.GetEventTypeDetail(cboEventCategory.SelectedValue, cboEventDetail.SelectedValue)
            cboEventTypeDetail.SelectedValue = str_EventTypeDetail_Default_Value

            Me.GetEventSourceIndicator()

            Me.GetStatusList()

            chkFollowUp.Checked = False
            chkFCR.Checked = False
            'Me.GetInitiator()

            'C001 - Start
            chkMCV.Checked = False
            'C001 - End

            If strMode.Trim = "" Then
                btnSubmit.Enabled = False
            Else
                btnSubmit.Enabled = True
            End If

            If strMode <> "V" Then
                'mtxtYear.Text = Date.Now.Year.ToString("0000")
                mtxtYear.Text = str_TXT_Default_Text
                txtSearchByPolicy.Text = str_TXT_Default_Text
                'Me.SelectMonth(cboMonth)
            End If

            ServiceLogBL.GetSerlogPreference(cboMedium, cboEventSourceInd) 'ITDCPI Select Default Medium and Initiator

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub GetMedium()

        'Dim dsMedium As New DataSet
        'Try
        '    If Not POSWS_HK().GetMediumList(dsMedium, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsMedium Is Nothing) Then
        '        Throw New Exception("Cannot get Medium List!")
        '    End If

        '    cboMedium.ValueMember = "EventSourceMediumCode"
        '    cboMedium.DisplayMember = "EventSourceMedium"
        '    cboMedium.DataSource = dsMedium.Tables(0)
        '    cboMedium.SelectedValue = str_Medium_Default_Value

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtMedium As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetMedium(dtMedium, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboMedium.DataSource = dtMedium
            cboMedium.ValueMember = "EventSourceMediumCode"
            cboMedium.DisplayMember = "Medium"

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS end

    End Sub

    Private Sub GetEventCategory()

        'Dim dsEventCategory As New DataSet
        'Try
        '    If Not POSWS_HK().GetEventCategory(dsEventCategory, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsEventCategory Is Nothing) Then
        '        Throw New Exception("Cannot get Event Category List!")
        '    End If

        '    RemoveHandler cboEventCategory.SelectedIndexChanged, AddressOf cboEventCategory_SelectedIndexChanged
        '    cboEventCategory.ValueMember = "cswecc_code"
        '    cboEventCategory.DisplayMember = "cswecc_desc"
        '    cboEventCategory.DataSource = dsEventCategory.Tables(0)
        '    cboEventCategory.SelectedValue = str_EventCategory_Default_Value
        '    AddHandler cboEventCategory.SelectedIndexChanged, AddressOf cboEventCategory_SelectedIndexChanged

        '    If cboEventDetail.Items.Count > 0 Then
        '        RemoveHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged
        '        cboEventDetail.DataSource = Nothing
        '        AddHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged
        '    End If

        '    cboEventTypeDetail.DataSource = Nothing

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtEvtCat As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventCategory(dtEvtCat, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboEventCategory.DataSource = dtEvtCat
            cboEventCategory.ValueMember = "cswecc_code"
            cboEventCategory.DisplayMember = "EventCat"
            cboEventCategory.SelectedValue = "10" 'Default at "Company"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS end

    End Sub

    Private Sub GetEventDetail(ByVal strCategoryCode As String)

        'Dim dsEventDetail As New DataSet
        'Try

        '    If Not POSWS_HK().GetEventDetail(strCategoryCode, dsEventDetail, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsEventDetail Is Nothing) Then
        '        Throw New Exception("Cannot get Event Category List!")
        '    End If

        '    RemoveHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged
        '    cboEventDetail.ValueMember = "EventTypeCode"
        '    cboEventDetail.DisplayMember = "EventTypeDesc"
        '    cboEventDetail.DataSource = dsEventDetail.Tables(0)

        '    cboEventDetail.SelectedIndex = int_CBO_Default_Index
        '    AddHandler cboEventDetail.SelectedIndexChanged, AddressOf cboEventDetail_SelectedIndexChanged

        '    If cboEventTypeDetail.Items.Count > 0 Then
        '        cboEventTypeDetail.DataSource = Nothing
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtEvtDetail As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventDetail(strCategoryCode, True, dtEvtDetail, strErr) 'isMCU=True, don't show HK only option
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboEventDetail.DataSource = dtEvtDetail
            cboEventDetail.ValueMember = "EventTypeCode"
            cboEventDetail.DisplayMember = "Issue_Type"
            cboEventDetail.SelectedIndex = "0"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS start

    End Sub

    Private Sub GetEventTypeDetail(ByVal strCategoryCode As String, ByVal strEventTypeCode As String)

        'Dim dsEventTypeDetail As New DataSet
        'Try

        '    If Not POSWS_HK().GetEventTypeDetail(strCategoryCode, strEventTypeCode, dsEventTypeDetail, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsEventTypeDetail Is Nothing) Then
        '        Throw New Exception("Cannot get Event Category List!")
        '    End If

        '    cboEventTypeDetail.ValueMember = "csw_event_typedtl_code"
        '    cboEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        '    cboEventTypeDetail.DataSource = dsEventTypeDetail.Tables(0)
        '    cboEventTypeDetail.SelectedIndex = int_CBO_Default_Index

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtEvtTypeDetail As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventTypeDetail(strCategoryCode, strEventTypeCode, dtEvtTypeDetail, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboEventTypeDetail.DataSource = dtEvtTypeDetail
            cboEventTypeDetail.ValueMember = "csw_event_typedtl_code"
            cboEventTypeDetail.DisplayMember = "Event_Description"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS end

    End Sub

    Private Sub GetEventSourceIndicator()

        'Dim dsEventSourceIndicator As New DataSet
        'Try
        '    If Not POSWS_HK().GetEventSourceIndicator(dsEventSourceIndicator, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsEventSourceIndicator Is Nothing) Then
        '        Throw New Exception("Cannot get Event Category List!")
        '    End If

        '    cboEventSourceInd.ValueMember = "EventSourceInitiatorCode"
        '    cboEventSourceInd.DisplayMember = "EventSourceInitiator"
        '    cboEventSourceInd.DataSource = dsEventSourceIndicator.Tables(0)
        '    cboEventSourceInd.SelectedValue = str_EventSourceInd_Default_Value

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtEventSourceIndicator As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetInitiator(dtEventSourceIndicator, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboEventSourceInd.Text = String.Empty
            cboEventSourceInd.DataSource = dtEventSourceIndicator
            cboEventSourceInd.ValueMember = "EventSourceInitiatorcode"
            cboEventSourceInd.DisplayMember = "initiator"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS end

    End Sub

    Private Sub GetStatusList()

        'Dim dsStatusList As New DataSet
        'Try

        '    If Not POSWS_HK().GetStatusList(dsStatusList, strError) Then
        '        Throw New Exception(strError)
        '    End If

        '    If (dsStatusList Is Nothing) Then
        '        Throw New Exception("Cannot get Event Category List!")
        '    End If

        '    cboStatus.ValueMember = "EventStatusCode"
        '    cboStatus.DisplayMember = "EventStatus"
        '    cboStatus.DataSource = dsStatusList.Tables(0)
        '    cboStatus.SelectedValue = str_Status_Default_Value

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        'End Try

        'ITDCPI Use BL in CS2005 project instead of POSWS start
        Dim dtEventStatusList As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventStatusList(dtEventStatusList, False, strErr) 'VHIS Complaint will not shown, default no related status
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboStatus.Text = String.Empty
            cboStatus.DataSource = dtEventStatusList
            cboStatus.ValueMember = "EventStatusCode"
            cboStatus.DisplayMember = "Status"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
        'ITDCPI Use BL in CS2005 project instead of POSWS end

    End Sub

    'Private Sub GetInitiator()

    '    Dim dsAccountStatus As New DataSet
    '    Try
    '        If Not POSWS_HK().GetInitiator(gsUser, txtInitiator.Text, strError) Then
    '            Throw New Exception(strError)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '    End Try

    'End Sub

    Private Sub Get_MCU_ServiceLog(ByVal strYear As String, ByVal strMonth As String, ByVal strUser As String, ByVal strPolicyNo As String, ByVal strStatus As String, ByVal strNFU As String)

        Try
            Dim dsResult As New DataSet
            If strUser.Trim.ToUpper() = "<ALL>" Then
                strUser = ""
            End If

            If strStatus.Trim.ToUpper() = "-1" Then
                strStatus = ""
            End If

            If strNFU.Trim.ToUpper() = "-1" Then
                strNFU = ""
            End If

            If mtxtYear.Text.Trim = "" Or mtxtYear.Text.Trim = "0000" Then
                strYear = -1
            Else
                strYear = mtxtYear.Text.Trim
            End If

            If cboMonth.Text.Trim = "" Then
                strMonth = -1
            Else
                strMonth = cboMonth.Text.Trim
            End If

            Dim currIndex As Integer = 0
            If dgvServiceLog.RowCount > 0 Then
                currIndex = dgvServiceLog.CurrentRow.Index
            End If


            'If Not POSWS_HK().Get_MCU_ServiceLog(strYear, strMonth, strUser, strPolicyNo, strStatus, strNFU, dsResult, strError) Then
            'Throw New Exception(strError)
            'End If
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            If Not ServiceLogBL.Get_MCU_ServiceLog(strYear, strMonth, strUser, strPolicyNo, strStatus, strNFU, dsResult, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS end

            If (dsResult Is Nothing) Then
                Throw New Exception("Cannot get Service Log Detail!")
            End If

            'ITSR933 FG R3 Policy Number Change Start
            dgvServiceLog.Columns("PolicyAccountID").Visible = False

            Dim dtResult As New DataTable
            If dsResult.Tables.Count > 0 AndAlso dsResult.Tables(0).Rows.Count > 0 Then
                dtResult.Merge(dsResult.Tables(0).Copy)
            End If

            'search Capsil Conversion mapped policy and add to result

            'ITSR933 CAPSIL conversion https://jira.hk.intranet/browse/CM-4214 bug fixing, enhance the old policy number change logic 20211103 Jacky So
            'If user enter LifeAsia policy number, then it already gets the service log in the above block
            'If user enter CAPSIL policy number, which differs from LifeAsia number, it will retrieve the service log using the new LifeAsia number

            Dim strPolicyNo2 As String = strPolicyNo
            If strPolicyNo2 <> "" Then

                'Dim strPolicyCap As String = ""
                'If ServiceLogBL.GetCapsilPolicyNoMCU(strPolicyNo2, strPolicyCap, strError) Then
                '    If strPolicyCap <> "" Then
                '        dsResult = New DataSet
                '        If ServiceLogBL.Get_MCU_ServiceLog(strYear, strMonth, strUser, strPolicyCap, strStatus, strNFU, dsResult, strError) Then
                '            If dsResult.Tables.Count > 0 AndAlso dsResult.Tables(0).Rows.Count > 0 Then
                '                dtResult.Merge(dsResult.Tables(0).Copy)
                '            End If
                '        End If
                '    End If
                'End If

                Dim strPolicyLA As String = ""
                If ServiceLogBL.GetLifeAsiaPolicyNoMCU(strPolicyLA, strPolicyNo2, strError) Then
                    If strPolicyLA <> "" And (strPolicyLA <> strPolicyNo2) Then
                        dsResult = New DataSet
                        If ServiceLogBL.Get_MCU_ServiceLog(strYear, strMonth, strUser, strPolicyLA, strStatus, strNFU, dsResult, strError) Then
                            If dsResult.Tables.Count > 0 AndAlso dsResult.Tables(0).Rows.Count > 0 Then
                                dtResult.Merge(dsResult.Tables(0).Copy)
                            End If
                        End If
                    End If
                End If

            End If

            If dtResult.Rows.Count > 0 Then
                For Each row As DataRow In dtResult.Rows

                    'Get Policy info from MCU DB
                    Dim dsPolicy As New DataSet
                    'ITDCPI Use BL in CS2005 project instead of POSWS start
                    'If Not POSWS_MCU().SearchPolicy_MCU(row("PolicyAccountID"), dsPolicy, strError) Then
                    '    Throw New Exception(strError)
                    'End If
                    If Not ServiceLogBL.SearchPolicy_MCU(row("PolicyAccountID"), dsPolicy, strError) Then
                        Throw New Exception(strError)
                    End If
                    'ITDCPI Use BL in CS2005 project instead of POSWS end

                    If dsPolicy.Tables.Count > 0 AndAlso dsPolicy.Tables(0).Rows.Count > 0 Then
                        'Replace policy info to relevant fields
                        row("ProductID") = dsPolicy.Tables(0).Rows(0)("ProductID")
                        row("NameSuffix") = dsPolicy.Tables(0).Rows(0)("NameSuffix")
                        row("FirstName") = dsPolicy.Tables(0).Rows(0)("FirstName")
                        row("AccountStatusCode") = dsPolicy.Tables(0).Rows(0)("AccountStatusCode")
                        row("LocationCode") = dsPolicy.Tables(0).Rows(0)("LocationCode")
                        row.AcceptChanges()
                    End If
                Next
            End If


            RemoveHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
            dgvServiceLog.DataSource = dtResult
            If dgvServiceLog.Rows.Count - 1 >= currIndex Then
                dgvServiceLog.Rows(currIndex).Selected = True
                dgvServiceLog.CurrentCell = dgvServiceLog.Rows(currIndex).Cells("MCV")
            End If
            AddHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
            'ITSR933 FG R3 Policy Number Change End

            If strMode = "V" AndAlso (Not dgvServiceLog.CurrentCell Is Nothing) AndAlso dgvServiceLog.CurrentCell.RowIndex >= 0 Then
                Me.PopulatedData(dgvServiceLog.Rows(dgvServiceLog.CurrentCell.RowIndex))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

#Region "   Macau DB Data "

    Private Sub GetBasicPlan()

        Dim dsBasicPlan As New DataSet
        Try
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            'If Not POSWS_MCU().GetBasicPlan(dsBasicPlan, strError) Then
            '    Throw New Exception(strError)
            'End If
            If Not ServiceLogBL.GetBasicPlan(dsBasicPlan, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS end
            If (dsBasicPlan Is Nothing) Then
                Throw New Exception("Cannot get Event Category List!")
            End If

            cboBasicPlan.ValueMember = "ProductID"
            cboBasicPlan.DisplayMember = "ProductName"
            cboBasicPlan.DataSource = dsBasicPlan.Tables(0)

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub GetAccountStatus()

        Dim dsAccountStatus As New DataSet
        Try
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            'If Not POSWS_MCU().GetAccountStatus(dsAccountStatus, strError) Then
            '    Throw New Exception(strError)
            'End If
            If Not ServiceLogBL.GetAccountStatus(dsAccountStatus, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS end
            If (dsAccountStatus Is Nothing) Then
                Throw New Exception("Cannot get Account Status!")
            End If

            cboAccountStatus.ValueMember = "AccountStatusCode"
            cboAccountStatus.DisplayMember = "AccountStatus"
            cboAccountStatus.DataSource = dsAccountStatus.Tables(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub GetAgentLocation()

        Dim dsAgentLocation As New DataSet
        Try
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            'If Not POSWS_MCU().GetAgentLocation(dsAgentLocation, strError) Then
            '    Throw New Exception(strError)
            'End If
            If Not ServiceLogBL.GetAgentLocation(dsAgentLocation, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS end
            If (dsAgentLocation Is Nothing) Then
                Throw New Exception("Cannot get Agent Location!")
            End If

            cboServicingLocationCode.ValueMember = "camglm_loc_code"
            cboServicingLocationCode.DisplayMember = "camglm_loc_desc"
            cboServicingLocationCode.DataSource = dsAgentLocation.Tables(0)

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub SearchPolicy_MCU(ByVal strPolicyNo As String)

        Dim dsPolicy As New DataSet
        Try
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            'If Not POSWS_MCU().SearchPolicy_MCU(strPolicyNo, dsPolicy, strError) Then
            '    Throw New Exception(strError)
            'End If
            If Not ServiceLogBL.SearchPolicy_MCU(strPolicyNo, dsPolicy, strError) Then
                Throw New Exception(strError)
            End If
            'ITDCPI Use BL in CS2005 project instead of POSWS end

            If (dsPolicy Is Nothing) Then
                Throw New Exception("Cannot get Policy Detail!")
            End If

            If dsPolicy.Tables(0).Rows.Count > 0 Then
                txtCustomerID.Text = dsPolicy.Tables(0).Rows(0)("CustomerID")
                txtSACode.Text = dsPolicy.Tables(0).Rows(0)("AgentCode")
                txtAgentName.Text = dsPolicy.Tables(0).Rows(0)("NameSuffix") + " " + dsPolicy.Tables(0).Rows(0)("FirstName")
                cboBasicPlan.SelectedValue = dsPolicy.Tables(0).Rows(0)("ProductID")
                cboAccountStatus.SelectedValue = dsPolicy.Tables(0).Rows(0)("AccountStatusCode")
                cboServicingLocationCode.SelectedValue = dsPolicy.Tables(0).Rows(0)("LocationCode")
                strUnitCode = dsPolicy.Tables(0).Rows(0)("UnitCode")
                strDivisionNo = Convert.ToString(dsPolicy.Tables(0).Rows(0)("UnitCode")).Substring(0, 2)
            Else
                txtCustomerID.Text = str_TXT_Default_Text
                txtSACode.Text = str_TXT_Default_Text
                txtAgentName.Text = str_TXT_Default_Text
                cboBasicPlan.SelectedValue = int_CBO_Default_Index
                cboAccountStatus.SelectedValue = int_CBO_Default_Index
                cboServicingLocationCode.SelectedValue = int_CBO_Default_Index
                strUnitCode = str_TXT_Default_Text
                strDivisionNo = str_TXT_Default_Text

                MessageBox.Show("Cannot Find Given Policy Details.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

#End Region

    Private Sub PopulatedData(ByVal dgvRow As DataGridViewRow)

        Try
            Dim dteResult As DateTime

            txtPolicyNo.Text = dgvRow.Cells("PolicyAccountID").Value
            If Convert.ToString(dgvRow.Cells("CustomerID").Value) = "0" Then
                txtCustomerID.Text = str_TXT_Default_Text
            Else
                txtCustomerID.Text = dgvRow.Cells("CustomerID").Value
            End If
            txtSACode.Text = dgvRow.Cells("AgentCode").Value
            txtAgentName.Text = dgvRow.Cells("NameSuffix").Value + " " + dgvRow.Cells("FirstName").Value

            'txtInitiator.Text = dgvRow.Cells("InitiatorName").Value
            txtEventNote.Text = dgvRow.Cells("EventNotes").Value
            txtRemainder.Text = dgvRow.Cells("ReminderNotes").Value

            'If dgvRow.Cells("EventStatusCode").Value.ToString().ToUpper = "H" Then
            '    dteEffectiveDate.Value = dgvRow.Cells("EventCompletionDateTime").Value
            'If dgvRow.Cells("EventStatusCode").Value.ToString().ToUpper = "C" Then
            'dteEffectiveDate.Value = dgvRow.Cells("EventCloseoutDateTime").Value
            'txtInitiator.Text = dgvRow.Cells("CloseInitiatorName").Value
            'Else
            dteEffectiveDate.Value = dgvRow.Cells("EventInitialDateTime").Value
            'End If

            If Convert.ToString(dgvRow.Cells("ProductID").Value).Trim = "" Then
                cboBasicPlan.SelectedValue = int_CBO_Default_Index
            Else
                cboBasicPlan.SelectedValue = dgvRow.Cells("ProductID").Value
            End If

            If Convert.ToString(dgvRow.Cells("AccountStatusCode").Value).Trim = "" Then
                cboAccountStatus.SelectedValue = int_CBO_Default_Index
            Else
                cboAccountStatus.SelectedValue = dgvRow.Cells("AccountStatusCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("LocationCode").Value).Trim = "" Then
                cboServicingLocationCode.SelectedValue = int_CBO_Default_Index
            Else
                cboServicingLocationCode.SelectedValue = dgvRow.Cells("LocationCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventSourceMediumCode").Value).Trim = "" Then
                cboMedium.SelectedValue = int_CBO_Default_Index
            Else
                cboMedium.SelectedValue = dgvRow.Cells("EventSourceMediumCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventCategoryCode").Value).Trim = "" Then
                cboEventCategory.SelectedValue = int_CBO_Default_Index
            Else
                cboEventCategory.SelectedValue = dgvRow.Cells("EventCategoryCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventTypeCode").Value).Trim = "" Then
                cboEventDetail.SelectedValue = int_CBO_Default_Index
            Else
                cboEventDetail.SelectedValue = dgvRow.Cells("EventTypeCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventTypeDetailCode").Value).Trim = "" Then
                cboEventTypeDetail.SelectedValue = int_CBO_Default_Index
            Else
                cboEventTypeDetail.SelectedValue = dgvRow.Cells("EventTypeDetailCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventSourceInitiatorCode").Value).Trim = "" Then
                cboEventSourceInd.SelectedValue = int_CBO_Default_Index
            Else
                cboEventSourceInd.SelectedValue = dgvRow.Cells("EventSourceInitiatorCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventStatusCode").Value).Trim = "" Then
                cboStatus.SelectedValue = int_CBO_Default_Index
            Else
                cboStatus.SelectedValue = dgvRow.Cells("EventStatusCode").Value
            End If

            If Convert.ToString(dgvRow.Cells("FollowUpByMacau").Value).Trim.ToUpper = "YES" Then
                chkFollowUp.Checked = True
            Else
                chkFollowUp.Checked = False
            End If

            If Convert.ToString(dgvRow.Cells("EventCloseoutCode").Value).Trim.ToUpper = "YES" Then
                chkFCR.Checked = True
            Else
                chkFCR.Checked = False
            End If

            'C001 - Start
            If Convert.ToString(dgvRow.Cells("MCV").Value).Trim.ToUpper = "YES" Then
                chkMCV.Checked = True
            Else
                chkMCV.Checked = False
            End If
            'C001 - End


            strUnitCode = dgvRow.Cells("UnitCode").Value.ToString.Trim
            strDivisionNo = dgvRow.Cells("DivisionCode").Value.ToString().Trim

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    'Private Sub SelectMonth(ByRef cboMonth As ComboBox)

    '    Try
    '        For intIndex As Integer = 0 To cboMonth.Items.Count - 1
    '            If Date.Now.Month = Convert.ToString(cboMonth.Items(intIndex)) Then
    '                cboMonth.SelectedIndex = intIndex
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '    End Try

    'End Sub

    Private Sub BuildUserCombo(ByRef cboUser As ComboBox)

        Try

            cboUser.Items.Add("<ALL>")
            cboUser.Items.Add(gsUser.ToUpper)
            cboUser.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub BuildFollowUpCombo(ByRef cboSearchNFU As ComboBox)

        Try

            Dim dtFollowUp As New DataTable
            dtFollowUp.Columns.Add("Display", Type.GetType("System.String"))
            dtFollowUp.Columns.Add("Value", Type.GetType("System.Int32"))

            For index As Integer = 0 To 2
                Dim dr As DataRow = dtFollowUp.NewRow()
                Dim strDisplay As String = ""
                Dim intValue As Integer = -1

                If index = 0 Then
                    strDisplay = "<ALL>"
                    intValue = -1
                ElseIf index = 1 Then
                    strDisplay = "No"
                    intValue = 0
                ElseIf index = 2 Then
                    strDisplay = "Yes"
                    intValue = 1
                End If
                dr("Display") = strDisplay
                dr("Value") = intValue
                dtFollowUp.Rows.Add(dr)
            Next

            cboSearchNFU.DisplayMember = "Display"
            cboSearchNFU.ValueMember = "Value"
            cboSearchNFU.DataSource = dtFollowUp
            cboSearchNFU.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub GetSearchStatusList()

        Dim dtStatusList As New DataTable
        Try

            'If Not POSWS_HK().GetStatusList(dsStatusList, strError) Then
            '    Throw New Exception(strError)
            'End If
            'ITDCPI Use BL in CS2005 project instead of POSWS start
            ServiceLogBL.GetEventStatusList(dtStatusList, False, strError) 'VHIS Complaint will not shown, default no related status
            'ITDCPI Use BL in CS2005 project instead of POSWS start

            If (dtStatusList Is Nothing) Then
                Throw New Exception("Cannot get Event Category List!")
            End If

            Dim dr As DataRow = dtStatusList.NewRow
            dr("EventStatusCode") = "-1"
            dr("Status") = "<ALL)"
            dtStatusList.Rows.InsertAt(dr, 0)

            cboSearchStatus.ValueMember = "EventStatusCode"
            cboSearchStatus.DisplayMember = "Status"
            cboSearchStatus.DataSource = dtStatusList
            cboSearchStatus.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub BuildDS(ByRef dtResult As DataTable)
        Try

            dtResult.Columns.Add("ServiceEventNumber", Type.GetType("System.String"))
            dtResult.Columns.Add("EventCategoryCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventTypeCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventTypeActionCode", Type.GetType("System.String"))
            dtResult.Columns.Add("CustomerID", Type.GetType("System.String"))
            dtResult.Columns.Add("PolicyAccountID", Type.GetType("System.String"))
            dtResult.Columns.Add("MasterCSRID", Type.GetType("System.String"))
            dtResult.Columns.Add("SecondaryCSRID", Type.GetType("System.String"))
            dtResult.Columns.Add("EventInitialDateTime", Type.GetType("System.DateTime"))
            dtResult.Columns.Add("EventAssignDateTime", Type.GetType("System.DateTime"))
            dtResult.Columns.Add("EventCompletionDateTime", Type.GetType("System.DateTime"))
            dtResult.Columns.Add("EventCloseoutCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventCloseoutDateTime", Type.GetType("System.DateTime"))
            dtResult.Columns.Add("EventCloseoutCSRID", Type.GetType("System.String"))
            dtResult.Columns.Add("EventStatusCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventSourceInitiatorCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventSourceMediumCode", Type.GetType("System.String"))
            dtResult.Columns.Add("EventNotes", Type.GetType("System.String"))
            dtResult.Columns.Add("ReminderDate", Type.GetType("System.DateTime"))
            dtResult.Columns.Add("ReminderNotes", Type.GetType("System.String"))
            dtResult.Columns.Add("EventTypeDetailCode", Type.GetType("System.String"))
            dtResult.Columns.Add("AgentCode", Type.GetType("System.String"))
            dtResult.Columns.Add("DivisionCode", Type.GetType("System.String"))
            dtResult.Columns.Add("UnitCode", Type.GetType("System.String"))
            dtResult.Columns.Add("CaseNo", Type.GetType("System.String"))

            dtResult.Columns.Add("UpdateUser", Type.GetType("System.String"))
            dtResult.Columns.Add("BirthdayAlert", Type.GetType("System.String"))
            dtResult.Columns.Add("IsTransferToAES", Type.GetType("System.String"))
            dtResult.Columns.Add("IsAppearedInAES", Type.GetType("System.String"))
            dtResult.Columns.Add("IsPolicyAlert", Type.GetType("System.String"))
            dtResult.Columns.Add("AlertNotes", Type.GetType("System.String"))
            dtResult.Columns.Add("FollowUpByMacau", Type.GetType("System.String"))
            dtResult.Columns.Add("MCV", Type.GetType("System.String"))
            dtResult.TableName = "MCUServiceLog"

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Function Validation(ByRef strValidationMsg As String) As Boolean

        Try
            If strMode = "V" Then
                If dgvServiceLog.Rows.Count = 0 Then
                    strValidationMsg = "No Records Found!."
                    Return False
                End If
            End If

            'If txtPolicyNo.Text.Trim.Length = 0 Then
            '    strValidationMsg = "Please Enter Policy No."
            '    Return False
            'End If

            'If txtCustomerID.Text.Trim.Length = 0 Then
            '    strValidationMsg = "Please Enter Valid Policy No."
            '    Return False
            'End If

            'If cboEventSourceInd.SelectedIndex = -1 Then
            '    strValidationMsg = "Please Select Source Indicator."
            '    Return False
            'End If

            'If cboStatus.SelectedIndex = -1 Then
            '    strValidationMsg = "Please Select Status of this service."
            '    Return False
            'End If

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Function

    Private Sub SupressKey(ByRef eventargs As System.Windows.Forms.KeyEventArgs)

        Try
            If Not (eventargs.KeyCode = Keys.Tab Or _
                       eventargs.KeyCode = Keys.Enter Or _
                       eventargs.KeyCode = Keys.Escape Or _
                       eventargs.KeyCode = Keys.Home Or _
                       eventargs.KeyCode = Keys.End Or _
                       eventargs.KeyCode = Keys.Left Or _
                       eventargs.KeyCode = Keys.Right) Then
                eventargs.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub
#End Region

    Private Sub btnSmsSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmsSearch.Click
        UclSMS1.bMc = True
        UclSMS1.PolicyAccountID = txtSmsPolicy.Text.Trim
    End Sub
End Class
