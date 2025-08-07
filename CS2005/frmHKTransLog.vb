Imports System
Imports System.Data.SqlClient

Public Class frmHKTransLog

#Region "   Common Declartion  "
    Dim int_CBO_Default_Index As Integer = -1
    Dim dte_DTE_Long_Format As String = "yyyy/MM/dd hh:mm tt"
    Dim dte_DTE_Custom_Format As String = "MMMM dd, yyyy hh:mm tt"
    Dim dte_DTE_Default_Date As Date = Date.Now
    Dim dte_DTE_NULL_Date As String = "1900/01/01"
    Dim strError As String = String.Empty
    Dim strMode As String = String.Empty  'A-->Add, V--Edit
    Dim sqlConn As New SqlConnection
    Dim ServiceLogBL As New ServiceLogBL

#End Region

    Private Sub frmHKTransLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Get combo box default value
            Me.BuildUserCombo(cboUser)
            Me.Initialize_YearMonth_box()
            Me.Initialize_EventStatusList(cboSearchStatus)
            cboSearchStatus.SelectedIndex = -1
            Me.ResetDetail()
            dgvServiceLog.AutoGenerateColumns = False
            DroplistEnabled(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            strMode = "A"
            Me.ResetDetail()
            btnSubmit.Enabled = True
            btnresetinput.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dsResult As New DataSet
        Dim StrErr As String = String.Empty
        Try
            If strMode = "A" Then
                If MessageBox.Show("Do you want to continue without save service log?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            strMode = "V"

            Me.ResetDetail()

            RemoveHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged
            dgvServiceLog.DataSource = Nothing
            AddHandler dgvServiceLog.SelectionChanged, AddressOf dgvServiceLog_SelectionChanged

            Dim currIndex As Integer = 0
            If dgvServiceLog.RowCount > 0 Then
                currIndex = dgvServiceLog.CurrentRow.Index
            End If

            If Not ServiceLogBL.Get_HK_non_cust_ServiceLog(cboYear.Text.Trim, cboMonth.Text.Trim, cboUser.Text.Trim, cboSearchStatus.SelectedValue, txtsearchmobile.Text.Trim, txtsearchemail.Text.Trim, dsResult, StrErr) Then
                MessageBox.Show(StrErr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Else
                If dsResult.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("No record is found", "Hong Kong Service Log (Non-Cust)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Else
                    dgvServiceLog.DataSource = dsResult.Tables(0)
                    If dgvServiceLog.Rows.Count - 1 >= currIndex Then
                        dgvServiceLog.Rows(currIndex).Selected = True
                        dgvServiceLog.CurrentCell = dgvServiceLog.Rows(currIndex).Cells("GreetingName")
                    End If
                End If
            End If

            dgvServiceLog.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub
    Private Sub btnresetsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetsearch.Click
        If DialogResult.Yes = MessageBox.Show("Do you want to clean the data inputted?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) Then
            ResetSearch()
        End If
    End Sub

    Private Sub btnresetinput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetinput.Click
        If DialogResult.Yes = MessageBox.Show("Do you want to clean the data inputted?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) Then
            ResetDetail()
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim strValidationMessage As String
            Dim strServiceEventNumber As String
            If Not Me.Validation(strValidationMessage) Then
                MessageBox.Show(strValidationMessage, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If

            Dim dsResult As New DataSet
            Dim dtResult As New DataTable
            Dim dRow As DataRow

            Me.BuildDS(dtResult)
            dRow = dtResult.NewRow
            If strMode = "A" Then
                dRow("ServiceEventNumber") = String.Empty
            Else
                Dim RowIndex As Integer = -1
                If (Not dgvServiceLog.CurrentCell Is Nothing) Then
                    RowIndex = dgvServiceLog.CurrentCell.RowIndex
                End If
                strServiceEventNumber = dgvServiceLog.Rows(RowIndex).Cells("ServiceEventNumber").Value
                dRow("ServiceEventNumber") = strServiceEventNumber
                If dgvServiceLog.Rows(RowIndex).Cells("EventStatusCode").Value = "C" Then
                    
                End If
            End If

            dRow("CustomerID") = "0" 'Customer number always is '0' for non-customer enquiry
            dRow("MasterCSRID") = gsUser 'Log in User ID
            If cboStatus.SelectedValue = "H" Then
                dRow("SecondaryCSRID") = cboHandoffCsr.SelectedValue
            Else
                dRow("SecondaryCSRID") = String.Empty
            End If

            'New field for identifying non-customer if neccessary
            dRow("Custname") = cboNamePrefix.Text + " " + txtgreetingname.Text.Trim 'Greeting name, always neccessary
            dRow("Call_id") = txtcallid.Text.Trim 'Call ID
            dRow("PhoneMobile") = txtmobile.Text.Trim 'Mobile
            dRow("EmailAddr") = txtemail.Text.Trim 'Email

            dRow("EventSourceInitiatorCode") = cboEventSourceInd.SelectedValue
            dRow("EventSourceMediumCode") = cboMedium.SelectedValue

            dRow("EventInitialDateTime") = dteEffectiveDate.Value.ToString(dte_DTE_Long_Format)
            dRow("EventAssignDateTime") = dte_DTE_NULL_Date
            dRow("EventCompletionDateTime") = dte_DTE_NULL_Date

            dRow("EventCategoryCode") = cboEventCategory.SelectedValue
            dRow("EventTypeCode") = cboEventDetail.SelectedValue
            dRow("EventTypeDetailCode") = cboEventTypeDetail.SelectedValue


            If cboStatus.Text.Trim = "Completed" Then
                dRow("EventCloseoutDateTime") = Date.Now.ToString(dte_DTE_Long_Format)
                dRow("EventCloseoutCSRID") = gsUser 'Login User ID
            Else
                dRow("EventCloseoutDateTime") = dte_DTE_NULL_Date 'if not yet completed, closeout date ='1900/01/01'
                dRow("EventCloseoutCSRID") = String.Empty 'if not yet completed, no closeout CSR
            End If

            dRow("EventStatusCode") = cboStatus.SelectedValue

            dRow("EventNotes") = txtEventNote.Text.Trim.Replace("'", "''") 'Replace ' with "
            dRow("ReminderDate") = dte_DTE_NULL_Date
            dRow("ReminderNotes") = txtRemainder.Text.Trim.Replace("'", "''") 'Replace ' with "

            dRow("DivisionCode") = String.Empty
            dRow("UnitCode") = String.Empty
            dRow("CaseNo") = String.Empty
            dRow("UpdateUser") = gsUser 'Login User
            dRow("BirthdayAlert") = "N"
            dRow("IsTransferToAES") = "N"
            dRow("IsAppearedInAES") = "N"
            dRow("IsPolicyAlert") = "N"
            dRow("AlertNotes") = String.Empty
            If chkFCR.Checked Then
                dRow("EventCloseoutCode") = "Y"
            Else
                dRow("EventCloseoutCode") = "N"
            End If
            If chkMCV.Checked Then
                dRow("MCV") = "Y"
            Else
                dRow("MCV") = "N"
            End If
            dtResult.Rows.Add(dRow)
            dsResult.Tables.Add(dtResult)

            If strMode = "A" Then
                If Not ServiceLogBL.Insert_non_cust_ServiceLog(dsResult, strServiceEventNumber, strError) Then
                    Throw New Exception(strError)
                End If
                MessageBox.Show("Record created Successfully at " & Now.ToString("hh:mm dd MMM yyyy") & ".", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Else
                If Not ServiceLogBL.Update_non_cust_ServiceLog(dsResult, strError) Then
                    Throw New Exception(strError)
                End If
                MessageBox.Show("Record updated Successfully at " & Now.ToString("hh:mm dd MMM yyyy") & ".", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            End If
            strMode = ""
            ResetDetail()

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
                If strMode = "A" Then
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

    Private Sub cboEventCategory_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEventCategory.SelectedValueChanged
        Try
            If Not TypeOf cboEventCategory.SelectedValue Is String Then
                Exit Try
            Else
                Me.Initialize_EventDetail(cboEventCategory.SelectedValue)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub


    Private Sub cboEventDetail_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEventDetail.SelectedValueChanged
        Try
            If Not TypeOf cboEventDetail.SelectedValue Is String Then
                Exit Try
            Else
                Me.Initialize_EventTypeDetail(cboEventCategory.SelectedValue, cboEventDetail.SelectedValue)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub cboStatus_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Try
            If Not TypeOf cboStatus.SelectedValue Is String Then
                Exit Try
            Else
                If cboStatus.SelectedValue = "H" Then
                    cboHandoffCsr.Enabled = True
                Else
                    cboHandoffCsr.Enabled = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

#Region "Common Function"

    Private Sub ResetDetail()

        Try
            DroplistEnabled(True)
            txtgreetingname.Text = String.Empty
            txtemail.Text = String.Empty
            txtmobile.Text = String.Empty
            txtEventNote.Text = String.Empty
            txtRemainder.Text = String.Empty
            dteEffectiveDate.Value = Now()
            Me.Initialize_Nameprefix()
            Me.Initialize_Medium()
            Me.Initialize_EventSourceIndicator()
            Me.Initialize_EventCategory()
            Me.Initialize_HandoffCsr()
            Me.Initialize_EventStatusList(cboStatus)
            cboStatus.SelectedValue = "C"
            chkFCR.Checked = False
            chkMCV.Checked = False

            If strMode = "V" Then
                btnSubmit.Enabled = True
                btnresetinput.Enabled = True
            Else
                btnSubmit.Enabled = False
                btnresetinput.Enabled = False
            End If

            ServiceLogBL.GetSerlogPreference(cboMedium, cboEventSourceInd) 'Select Default Medium and Initiator

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub ResetSearch()
        Try
            dgvServiceLog.DataSource = Nothing
            txtsearchmobile.Text = String.Empty
            txtsearchemail.Text = String.Empty
            cboYear.Text = String.Empty
            cboMonth.Text = String.Empty
            cboSearchStatus.SelectedIndex = -1
            cboUser.Text = gsUser
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub


    Private Sub PopulatedData(ByVal dgvRow As DataGridViewRow)

        Try
            Dim dteResult As DateTime
            Dim strErr As String = String.Empty
            txtEventNote.Text = dgvRow.Cells("EventNotes").Value
            txtRemainder.Text = dgvRow.Cells("ReminderNotes").Value
            dteEffectiveDate.Value = dgvRow.Cells("EventInitialDateTime").Value

            Dim GreetingName As String = Convert.ToString(dgvRow.Cells("GreetingName").Value).Trim
            If GreetingName = "" Then
                cboNamePrefix.Text = "Mr."
                txtgreetingname.Text = String.Empty
            Else
                cboNamePrefix.Text = Mid(GreetingName, 1, 3)
                txtgreetingname.Text = Mid(GreetingName, 5, Len(GreetingName))
            End If

            If Convert.ToString(dgvRow.Cells("EventSourceInitiator").Value).Trim = "" Then
                cboEventSourceInd.Text = String.Empty
            Else
                cboEventSourceInd.Text = dgvRow.Cells("EventSourceInitiator").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventSourceMedium").Value).Trim = "" Then
                cboMedium.Text = String.Empty
            Else
                cboMedium.Text = dgvRow.Cells("EventSourceMedium").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventCategoryDesc").Value).Trim = "" Then
                cboEventCategory.Text = String.Empty
            Else
                cboEventCategory.Text = dgvRow.Cells("EventCategoryDesc").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventTypeDesc").Value).Trim = "" Then
                cboEventDetail.Text = String.Empty
            Else
                cboEventDetail.Text = dgvRow.Cells("EventTypeDesc").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventTypeDetailDesc").Value).Trim = "" Then
                cboEventTypeDetail.Text = String.Empty
            Else
                cboEventTypeDetail.Text = dgvRow.Cells("EventTypeDetailDesc").Value
            End If

            If Convert.ToString(dgvRow.Cells("EventStatus").Value).Trim = "" Then
                cboStatus.Text = String.Empty
            Else
                cboStatus.Text = (dgvRow.Cells("EventStatus")).Value
            End If

            'Allow change EventNote but not allow change dropdown list option if completed
            If dgvRow.Cells("EventStatus").Value = "Completed" Then
                DroplistEnabled(False)
            Else
                DroplistEnabled(True)
            End If

            If Convert.ToString(dgvRow.Cells("PhoneMobile").Value).Trim = "" Then
                txtmobile.Text = String.Empty
            Else
                txtmobile.Text = (dgvRow.Cells("PhoneMobile")).Value
            End If

            If Convert.ToString(dgvRow.Cells("EmailAddr").Value).Trim = "" Then
                txtemail.Text = String.Empty
            Else
                txtemail.Text = (dgvRow.Cells("EmailAddr")).Value
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

            If Convert.ToString(dgvRow.Cells("call_id").Value).Trim = "" Then
                txtcallid.Text = String.Empty
            Else
                txtcallid.Text = dgvRow.Cells("call_id").Value
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub BuildUserCombo(ByRef cboUser As ComboBox)
        Try
            cboUser.Items.Add("<ALL>")
            cboUser.Items.Add(gsUser.ToUpper)
            cboUser.SelectedIndex = 0

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

            'New variables for HK non-cust Service Log
            dtResult.Columns.Add("Custname", Type.GetType("System.String")) 'Greeting name
            dtResult.Columns.Add("Call_id", Type.GetType("System.String")) 'Call ID
            dtResult.Columns.Add("PhoneMobile", Type.GetType("System.String")) 'Mobile
            dtResult.Columns.Add("EmailAddr", Type.GetType("System.String")) 'Email

        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Function Validation(ByRef strValidationMsg As String) As Boolean
        strValidationMsg = String.Empty
        Dim passValidation As Boolean = True
        Try
            If cboEventCategory.Text = String.Empty Then
                strValidationMsg += "- Please select Event Category!" & vbNewLine
                passValidation = False
            End If

            If cboEventDetail.Text = String.Empty Then
                strValidationMsg += "- Please select Event Detail!" & vbNewLine
                passValidation = False
            End If

            If cboMedium.Text = String.Empty Then
                strValidationMsg += "- Please select Medium!" & vbNewLine
                passValidation = False
            End If

            If cboEventSourceInd.Text = String.Empty Then
                strValidationMsg += "- Please select Initator!" & vbNewLine
                passValidation = False
            End If

            If txtgreetingname.Text.Trim = String.Empty Then
                strValidationMsg += "- Please input Greeting Name!" & vbNewLine
                passValidation = False
            End If

            If txtmobile.Text.Trim = String.Empty And txtemail.Text.Trim = String.Empty And cboStatus.SelectedValue <> "C" Then
                strValidationMsg += "- Please input mobile or email if the case is not completed!" & vbNewLine
                passValidation = False
            End If

            If cboStatus.SelectedValue = "H" And cboHandoffCsr.Text.Trim = String.Empty Then
                strValidationMsg += "- Please select the receiver of this service log!" & vbNewLine
                passValidation = False
            End If

            If strMode = "V" Then
                If dgvServiceLog.Rows.Count = 0 Then
                    strValidationMsg += "- No Records Found!" & vbNewLine
                    Return False
                End If
            End If
            Return passValidation
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Function
#End Region

#Region "New UI BL"

    Private Sub Initialize_YearMonth_box()
        Dim i As Integer
        Dim j As Integer
        For i = Now.Year To 1970 Step -1
            cboYear.Items.Add(i)
        Next
        For j = 1 To 12
            cboMonth.Items.Add(j)
        Next
    End Sub

    Private Sub Initialize_Nameprefix()
        cboNamePrefix.Text = ""
        cboNamePrefix.Items.Clear()
        cboNamePrefix.Items.Add("Mr.")
        cboNamePrefix.Items.Add("Ms.")
        cboNamePrefix.SelectedIndex = "0"
    End Sub

    Private Sub Initialize_EventCategory()
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
    End Sub

    Private Sub Initialize_EventDetail(ByVal strEvtCatCode As String)
        Dim dtEvtDetail As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventDetail(strEvtCatCode, True, dtEvtDetail, strErr) 'isMCU set to true, exclude VHIS
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
    End Sub

    Private Sub Initialize_EventTypeDetail(ByVal strEvtCatCode As String, ByVal strEvtDetailCode As String)
        Dim dtEvtTypeDetail As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventTypeDetail(strEvtCatCode, strEvtDetailCode, dtEvtTypeDetail, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboEventTypeDetail.DataSource = dtEvtTypeDetail
            cboEventTypeDetail.ValueMember = "csw_event_typedtl_code"
            cboEventTypeDetail.DisplayMember = "Event_Description"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub Initialize_Medium()

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
    End Sub

    Private Sub Initialize_EventSourceIndicator()

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

    End Sub

    Private Sub Initialize_EventStatusList(ByVal cboStatuslist As ComboBox)

        Dim dtEventStatusList As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventStatusList(dtEventStatusList, False, strErr) 'VHIS Complaint will not shown, default no related status
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboStatuslist.Text = String.Empty
            cboStatuslist.DataSource = dtEventStatusList
            cboStatuslist.ValueMember = "EventStatusCode"
            cboStatuslist.DisplayMember = "Status"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

    Private Sub Initialize_HandoffCsr()
        Dim dtCsr As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetCsrList(dtCsr, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            cboHandoffCsr.DataSource = dtCsr
            cboHandoffCsr.ValueMember = "CSRID"
            cboHandoffCsr.DisplayMember = "Name"
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub DroplistEnabled(ByVal isEnabled As Boolean)
        cboEventCategory.Enabled = isEnabled
        cboEventDetail.Enabled = isEnabled
        cboEventTypeDetail.Enabled = isEnabled
        cboEventSourceInd.Enabled = isEnabled
        cboMedium.Enabled = isEnabled
        dteEffectiveDate.Enabled = isEnabled
        cboStatus.Enabled = isEnabled
        cboHandoffCsr.Enabled = isEnabled
    End Sub

#End Region

End Class