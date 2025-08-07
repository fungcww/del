
Imports System.Configuration

Public Class frm7ElevenPayout
    Dim dtPayoutDetail As New DataTable
    Dim dtTranDetail As New DataTable
    Dim dtTranHistory As New DataTable
    Dim dtSmsDetail As New DataTable
    Dim rcdUnsaveRemark As New List(Of Integer)
    Dim strPolicyNo As String
    Dim strMobileNo As String
    Dim strUserId As String = gsUser
    Dim intPayoutRow As Integer = -1
    Public strMerchantId As String

    'Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code 
    Dim blnIsMacau As Boolean = False
    Dim mcuCompanyName As String = IIf(Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("MCU_COMP_NAME")), ConfigurationManager.AppSettings.Item("MCU_COMP_NAME"), "mcu")

    ''' <summary>
    ''' Judge whether the MACAU button is clicked from mum7ElevenPayoutMcu
    ''' </summary>
    ''' <remarks>
    ''' <br>Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4)</br>
    ''' </remarks>
    Public WriteOnly Property IsMacau() As Boolean
        Set(ByVal Value As Boolean)
            blnIsMacau = Value
        End Set
    End Property

    Private Sub frm7ElevenPayout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialize()
        If blnIsMacau Then
            Me.Text = "7-Eleven QR code maintenance(Macau)"
        End If
    End Sub
    Private Sub Initialize()
        dtPayoutDetail = New DataTable
        dtTranDetail = New DataTable
        dtTranHistory = New DataTable
        dtSmsDetail = New DataTable

        btnDisableQrCode.Enabled = False
        btnUnlockQrCode.Enabled = False

        gvPayoutDetail.DataSource = dtPayoutDetail
        gvPayoutDetail.ReadOnly = True
        gvPayoutDetail.AllowUserToAddRows = False

        gvTransactions.DataSource = dtTranDetail
        gvTransactions.ReadOnly = True
        gvTransactions.AllowUserToAddRows = False

        gvTranHistory.DataSource = dtTranHistory
        gvTranHistory.ReadOnly = True
        gvTranHistory.AllowUserToAddRows = False

        gvSmsDetails.DataSource = dtSmsDetail
        gvSmsDetails.ReadOnly = True
        gvSmsDetails.AllowUserToAddRows = False

        'txtSmsSdDate.ReadOnly = True
        'txtSmsMobileNo.ReadOnly = True
        txtSmsMessage.ReadOnly = True
        'txtSmsMessage1.ReadOnly = True

        'txtSmsSdDate.BackColor = SystemColors.Control
        'txtSmsMobileNo.BackColor = SystemColors.Control
        txtSmsMessage.BackColor = SystemColors.Control
        'txtSmsMessage1.BackColor = SystemColors.Control


        txtRemarks.Text = String.Empty
        txtRemarks.Enabled = False

        txtPolicyNo.Text = ""
        txtMobileNo.Text = ""

        'Oliver 2023-11-02 Updated for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code 
        txtMobileNo.MaxLength = 11
        'txtSmsSdDate.Text = ""
        'txtSmsMobileNo.Text = ""
        txtSmsMessage.Text = ""
        'txtSmsMessage1.Text = ""

        btnSaveRemarks.Enabled = False

        'Panel1.Visible = False

        rcdUnsaveRemark = New List(Of Integer)

        btnResend.Enabled = False

        gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gvTranHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gvSmsDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub


    Private Function GetPayoutDetail(ByVal strPolicyId As String, ByVal strMobileNo As String) As Boolean
        Dim blnResult As Boolean = False
        Dim dtPayoutDetailShow As New DataTable

        dtPayoutDetail = New DataTable
        'Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code 
        get7ElevenPayoutDetails(IIf(blnIsMacau, mcuCompanyName, ""), strPolicyId, strMobileNo, dtPayoutDetail)

        If dtPayoutDetail.Rows.Count > 0 Then
            dtPayoutDetail.DefaultView.Sort = "PayoutID"
            dtPayoutDetailShow = dtPayoutDetail.Copy()
            dtPayoutDetailShow.Columns.Remove("Remark")
            dtPayoutDetailShow.Columns.Remove("Remark_Temp")
            dtPayoutDetailShow.Columns.Remove("SMSMessageID")
            gvPayoutDetail.DataSource = dtPayoutDetailShow
            gvPayoutDetail.AutoResizeColumns()
            blnResult = True
        Else
            MsgBox("No record is found in payout detail. ")
            RcdNotFound("PayoutDetail")
        End If
        Return blnResult
    End Function

    Private Function GetTranDetail(ByVal strPayoutId As String) As Boolean
        Dim blnResult As Boolean = False

        dtTranDetail = New DataTable
        'Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code  
        Get7ElevenTranDetails(IIf(blnIsMacau, mcuCompanyName, ""), strPayoutId, dtTranDetail)

        If dtTranDetail.Rows.Count > 0 Then
            dtTranDetail.DefaultView.Sort = "Trans ID"
            gvTransactions.DataSource = dtTranDetail
            gvTransactions.AutoResizeColumns()
            gvTransactions.Rows(0).Selected = True
            blnResult = True
        Else
            RcdNotFound("TranDetail")
        End If
        Return blnResult
    End Function


    Private Function GetTranHistory(ByVal strPayoutId As String, ByVal strTranId As String) As Boolean
        Dim blnResult As Boolean = False
        Dim dtTranHistoryShow As New DataTable

        dtTranHistory = New DataTable
        'Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code 
        Get7ElevenTranHistory(IIf(blnIsMacau, mcuCompanyName, ""), strPayoutId, strTranId, dtTranHistory)

        If dtTranHistory.Rows.Count > 0 Then
            dtTranHistory.DefaultView.Sort = "Trans ID, Seq No."
            dtTranHistoryShow = dtTranHistory.Copy()
            dtTranHistoryShow.Columns.Remove("SMSSendDate")
            dtTranHistoryShow.Columns.Remove("MobileNo")
            dtTranHistoryShow.Columns.Remove("SMSMessageID")
            dtTranHistoryShow.Columns.Remove("SMSMessageContent")
            dtTranHistoryShow.Columns.Remove("PayoutID")
            gvTranHistory.DataSource = dtTranHistoryShow
            gvTranHistory.AutoResizeColumns()
            gvTranHistory.Rows(0).Selected = True
            blnResult = True
        Else
            RcdNotFound("TranHistory")
        End If
        Return blnResult
    End Function

    Private Sub ResettxtRemarks(ByVal intSelectRecord As Integer)
        If Not IsDBNull(dtPayoutDetail.Rows(intSelectRecord)("Remark_Temp")) OrElse Not String.IsNullOrEmpty(dtPayoutDetail.Rows(intSelectRecord)("Remark_Temp")) Then
            txtRemarks.Text = dtPayoutDetail.Rows(intSelectRecord)("Remark_Temp")
        ElseIf Not IsDBNull(dtPayoutDetail.Rows(intSelectRecord)("Remark")) OrElse Not String.IsNullOrEmpty(dtPayoutDetail.Rows(intSelectRecord)("Remark")) Then
            txtRemarks.Text = dtPayoutDetail.Rows(intSelectRecord)("Remark")
        Else
            txtRemarks.Text = String.Empty
        End If
        btnSaveRemarks.Enabled = False
    End Sub

    Private Sub ResetgbSmsDetails(ByVal intSelectRecord As Integer)
        'txtSmsSdDate.BackColor = Color.White
        'txtSmsMobileNo.BackColor = Color.White
        'txtSmsMessage1.BackColor = Color.White
        'txtSmsSdDate.Text = IIf(IsDBNull(dtTranHistory.Rows(intSelectRecord)("SMSSendDate")), "", dtTranHistory.Rows(intSelectRecord)("SMSSendDate"))
        'txtSmsMobileNo.Text = IIf(IsDBNull(dtTranHistory.Rows(intSelectRecord)("MobileNo")), "", dtTranHistory.Rows(intSelectRecord)("MobileNo"))
        'txtSmsMessage1.Text = IIf(IsDBNull(dtTranHistory.Rows(intSelectRecord)("SMSMessageContent")), "", dtTranHistory.Rows(intSelectRecord)("SMSMessageContent"))
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strPolicyNo = Trim(txtPolicyNo.Text)
        strMobileNo = Trim(txtMobileNo.Text)

        If String.IsNullOrEmpty(strPolicyNo) And String.IsNullOrEmpty(strMobileNo) Then
            MsgBox("Please enter Policy No. or Phone No. as search parameters. ", MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        SearchPayout(strPolicyNo, strMobileNo, 0)
    End Sub

    Private Sub gvPayoutDetail_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvPayoutDetail.SelectionChanged
        Try
            If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then 'If want to change by selecting rows instead of cell, change to selectedCells
                Dim intSelectRow As Integer
                If intPayoutRow = -1 Or gvPayoutDetail.Rows.Count <= 1 Then
                    intSelectRow = gvPayoutDetail.CurrentCell.RowIndex
                Else
                    intSelectRow = intPayoutRow
                End If
                Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
                ResettxtRemarks(intSelectRow)
                EnablebtnSaveRemark()
                ResetQrCodeBtn(gvPayoutDetail.Rows(intSelectRow).Cells("Status").Value.ToString)
                If GetTranDetail(strPayoutID) Then
                    If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
                        If GetTranDetail(strPayoutID) Then
                            'gvTransactions.Rows(0).Selected = True
                            Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
                            Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
                            If GetTranHistory(strPayoutID, strTranId) Then
                                If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
                                    'gvTranHistory.Rows(0).Selected = True
                                    Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
                                    'ResetgbSmsDetails(intSelectRow3)
                                    txtRemarks.Enabled = True
                                End If
                            End If
                        End If
                    End If
                End If
                GetSmsDetails(strPayoutID)
                'Dim strLang As String = "en"
                'Dim obj7ElevenSrhSmsHistResponse As CRS_Util.clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse
                'obj7ElevenSrhSmsHistResponse = CRS_Util.clsJSONTool.Call7ElevenSrhSmsHist(Convert.ToInt64(strPayoutID), strLang)
                'If Not obj7ElevenSrhSmsHistResponse Is Nothing AndAlso obj7ElevenSrhSmsHistResponse.message.Contains("Request success") Then
                '    InitDtSmsDetail()
                '    AddDtSmsDetailRow(obj7ElevenSrhSmsHistResponse)
                '    SetGvSmsDetail()
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub gvTransactions_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvTransactions.SelectionChanged
        Try
            If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
                Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
                Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
                If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
                    Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
                    Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
                    If GetTranHistory(strPayoutID, strTranId) Then
                        If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
                            'gvTranHistory.Rows(0).Selected = True
                            Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
                            'ResetgbSmsDetails(intSelectRow3)
                            txtRemarks.Enabled = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub gvTranHistory_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvTranHistory.SelectionChanged
        Try
            If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
                If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
                    Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
                    Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
                    Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
                    Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
                    If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
                        Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
                        'ResetgbSmsDetails(intSelectRow3)
                        txtRemarks.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub gvSmsDetails_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvSmsDetails.SelectionChanged
        Try
            If gvSmsDetails.Rows.Count > 0 Then
                Dim intSelectRow As Integer = gvSmsDetails.CurrentCell.RowIndex
                txtSmsMessage.BackColor = Color.White
                txtSmsMessage.Text = dtSmsDetail.Rows(intSelectRow)("Content")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Initialize()
        strPolicyNo = ""
        strMobileNo = ""
    End Sub

    Private Sub RcdNotFound(ByVal strSearch As String)
        If strSearch.Equals(Trim("PayoutDetail")) Then
            Initialize()
            txtPolicyNo.Text = strPolicyNo
            txtMobileNo.Text = strMobileNo
        ElseIf strSearch.Equals(Trim("TranDetail")) Then
            txtRemarks.Enabled = True
            dtTranDetail = New DataTable
            dtTranHistory = New DataTable
            gvTransactions.DataSource = dtTranDetail
            gvTranHistory.DataSource = dtTranHistory
            txtSmsMessage.BackColor = SystemColors.Control
        ElseIf strSearch.Equals(Trim("TranHistory")) Then
            txtRemarks.Enabled = True
            dtTranHistory = New DataTable
            gvTranHistory.DataSource = dtTranHistory
            txtSmsMessage.Text = ""
        End If
    End Sub

    Private Sub ResetQrCodeBtn(ByVal strStatus As String)
        Dim strFreeze As String = "Lock"
        Dim strReady As String = "ReadyForPayout"

        If strReady.ToUpper.Equals(strStatus.ToUpper) Then
            btnDisableQrCode.Enabled = True
            btnUnlockQrCode.Enabled = False
            btnResend.Enabled = True
        ElseIf strFreeze.ToUpper.Equals(strStatus.ToUpper) Then
            btnDisableQrCode.Enabled = False
            btnUnlockQrCode.Enabled = True
            btnResend.Enabled = False
        Else
            btnDisableQrCode.Enabled = False
            btnUnlockQrCode.Enabled = False
            btnResend.Enabled = False
        End If

    End Sub

    Private Sub txtRemarks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemarks.TextChanged
        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex

            If IsDBNull(dtPayoutDetail.Rows(intSelectRow)("Remark_Temp")) OrElse Not dtPayoutDetail.Rows(intSelectRow)("Remark_Temp").Equals(txtRemarks.Text) Then
                dtPayoutDetail.Rows(intSelectRow)("Remark_Temp") = txtRemarks.Text
                If rcdUnsaveRemark.Count <= 0 OrElse Not rcdUnsaveRemark.Contains(intSelectRow) Then
                    rcdUnsaveRemark.Add(intSelectRow)
                End If
            End If
        End If
        btnSaveRemarks.Enabled = True
    End Sub

    Private Sub btnSaveRemarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRemarks.Click
        Dim strRemarks As String = txtRemarks.Text.ToString
        If IsDBNull(strRemarks) Then
            MsgBox("Empty input in remarks. Please try again. ")
        Else
            Me.Enabled = False
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
            'Oliver 2023-11-02 Added for CRS Enhancement(General Enhance Ph4) Macau 7-11 QR Code 
            If Not Save7ElevenRemarks(IIf(blnIsMacau, mcuCompanyName, ""), strPayoutID, strRemarks) Then
                MsgBox("Save error. Please try again. ")
            Else
                btnSaveRemarks.Enabled = False
                dtPayoutDetail.Rows(intSelectRow)("Remark") = dtPayoutDetail.Rows(intSelectRow)("Remark_Temp")
                rcdUnsaveRemark.Remove(intSelectRow)
            End If
            Me.Enabled = True
        End If
    End Sub

    Private Sub txtMobileNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobileNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If btnSaveRemarks.Enabled Or rcdUnsaveRemark.Count > 0 Then
            If Not MsgBox("Remarks content ia changed. Exit anyway? ", MsgBoxStyle.YesNo) = DialogResult.No Then
                Me.Close()
            Else
                gvPayoutDetail.ClearSelection()
                'gvPayoutDetail.Rows(rcdUnsaveRemark(0)).Cells(0).Selected = True
                gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                gvPayoutDetail.CurrentCell = gvPayoutDetail(0, rcdUnsaveRemark(0))
                'gvPayoutDetail.Rows(rcdUnsaveRemark(0)).Selected = True
                'gvPayoutDetail_SelectionChanged(sender, e)
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub EnablebtnSaveRemark()
        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
            If (Not (IsDBNull(dtPayoutDetail.Rows(intSelectRow)("Remark_Temp")) Or String.IsNullOrEmpty(dtPayoutDetail.Rows(intSelectRow)("Remark_Temp"))) And IsDBNull(dtPayoutDetail.Rows(intSelectRow)("Remark"))) Or _
               (Not IsDBNull(dtPayoutDetail.Rows(intSelectRow)("Remark_Temp")) And Not IsDBNull(dtPayoutDetail.Rows(intSelectRow)("Remark")) And Not dtPayoutDetail.Rows(intSelectRow)("Remark").Equals(dtPayoutDetail.Rows(intSelectRow)("Remark_Temp"))) Then
                btnSaveRemarks.Enabled = True
            End If
        End If
    End Sub

    Private Sub SearchPayout(ByVal strPolicyNo As String, ByVal strMobileNo As String, ByVal intSelectRow As Integer)
        Initialize()
        txtPolicyNo.Text = strPolicyNo
        txtMobileNo.Text = strMobileNo

        Me.Enabled = False

        If GetPayoutDetail(strPolicyNo, strMobileNo) Then
            gvPayoutDetail.CurrentCell = gvPayoutDetail.Rows(intSelectRow).Cells(0)
            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
            ResettxtRemarks(intSelectRow)
            If GetTranDetail(strPayoutID) Then
                Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
                Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
                If GetTranHistory(strPayoutID, strTranId) Then
                    Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
                    'ResetgbSmsDetails(intSelectRow3)
                    txtRemarks.Enabled = True
                    rcdUnsaveRemark = New List(Of Integer)
                End If
            End If
            GetSmsDetails(strPayoutID)
        End If
        Me.Enabled = True
    End Sub

    Private Sub btnDisableQrCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableQrCode.Click
        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString

            Dim strLang As String = "en"

            Dim obj7ElevenVoidPayoutResponse As CRS_Util.clsJSONBusinessObj.cls7ElevenVoidPayoutResponse
            obj7ElevenVoidPayoutResponse = CRS_Util.clsJSONTool.Call7ElevenVoidPayout(strPayoutID, strUserId, strLang, strMerchantId)

            If Not obj7ElevenVoidPayoutResponse Is Nothing AndAlso obj7ElevenVoidPayoutResponse.message.Contains("Request success") Then
                intPayoutRow = intSelectRow
                SearchPayout(strPolicyNo, strMobileNo, intSelectRow)
                gvPayoutDetail.CurrentCell = gvPayoutDetail(0, intSelectRow)
                gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                gvPayoutDetail.Rows(intSelectRow).Selected = True
                MsgBox("QR code of payout " & strPayoutID & " is disbled. ", MsgBoxStyle.OkOnly)
            Else
                MsgBox("QR code of payout " & strPayoutID & " cannot be disbled. Please try again. ", MsgBoxStyle.OkOnly)
            End If
            intPayoutRow = -1
        End If
    End Sub

    Private Sub btnUnlockQrCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockQrCode.Click
        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString

            Dim strLang As String = "en"

            Dim obj7ElevenUnFreezePayoutResponse As CRS_Util.clsJSONBusinessObj.cls7ElevenUnfreezePayoutResponse
            obj7ElevenUnFreezePayoutResponse = CRS_Util.clsJSONTool.Call7ElevenUnfreezePayout(Convert.ToInt64(strPayoutID), strUserId, strLang, strMerchantId)

            If Not obj7ElevenUnFreezePayoutResponse Is Nothing AndAlso obj7ElevenUnFreezePayoutResponse.message.Contains("Request success") Then
                intPayoutRow = intSelectRow
                SearchPayout(strPolicyNo, strMobileNo, intSelectRow)
                gvPayoutDetail.CurrentCell = gvPayoutDetail(0, intSelectRow)
                gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                gvPayoutDetail.Rows(intSelectRow).Selected = True
                MsgBox("QR code of payout " & strPayoutID & " is unlocked. ", MsgBoxStyle.OkOnly)
            Else
                MsgBox("QR code of payout " & strPayoutID & " cannot be unlocked. Please try again. ", MsgBoxStyle.OkOnly)
            End If
            intPayoutRow = -1
        End If
    End Sub

    Private Sub btnResend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResend.Click
        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
            Dim intSmsRow As Integer = gvSmsDetails.CurrentCell.RowIndex
            Dim strMsgId As String = dtSmsDetail.Rows(intSmsRow)("MessageId")
            Dim obj7ElevenResendSmsResponse As CRS_Util.clsJSONBusinessObj.cls7ElevenResendSmsResponse
            obj7ElevenResendSmsResponse = CRS_Util.clsJSONTool.Call7ElevenResendSms(strMsgId, gsUser, "en", strMerchantId)
            If Not obj7ElevenResendSmsResponse Is Nothing AndAlso obj7ElevenResendSmsResponse.message.Contains("Request success") Then
                intPayoutRow = intSelectRow
                SearchPayout(strPolicyNo, strMobileNo, intSelectRow)
                gvPayoutDetail.CurrentCell = gvPayoutDetail(0, intSelectRow)
                gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                gvPayoutDetail.Rows(intSelectRow).Selected = True
                MsgBox("SMS of payout " & strPayoutID & " is resent. ", MsgBoxStyle.OkOnly)
            Else
                MsgBox("SMS of payout " & strPayoutID & " cannot be resent. Please try again. ", MsgBoxStyle.OkOnly)
            End If
            intPayoutRow = -1
        End If
    End Sub

    Private Sub InitDtSmsDetail()
        dtSmsDetail = New DataTable
        dtSmsDetail.Columns.Add("Seq Num")
        dtSmsDetail.Columns.Add("SMS SendDate")
        dtSmsDetail.Columns.Add("Mobile Number")
        dtSmsDetail.Columns.Add("Status")
        dtSmsDetail.Columns.Add("Content")
        dtSmsDetail.Columns.Add("MessageId")
    End Sub

    Private Sub AddDtSmsDetailRow(ByVal Response As CRS_Util.clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse)
        For i As Integer = 0 To Response.smsHistory.Count - 1
            Dim seqNo As String = Response.smsHistory(i).seq
            Dim sdDate As String = Response.smsHistory(i).sendDate
            'Dim sdDate_C As DateTime = DateTime.ParseExact(sdDate, "dd/MM/yyyy HH:mm tt", Nothing)
            Dim mobile As String = Response.smsHistory(i).mobile
            Dim status As String = Response.smsHistory(i).status
            Dim content As String = Response.smsHistory(i).content
            Dim msgId As String = Response.smsHistory(i).smsMessageId
            dtSmsDetail.Rows.Add(seqNo, sdDate, mobile, status, content, msgId)
        Next
    End Sub

    Private Sub SetGvSmsDetail()
        Dim dtSmsDetailShow As New DataTable
        dtSmsDetailShow = dtSmsDetail.Copy()
        dtSmsDetailShow.Columns.Remove("Content")
        dtSmsDetailShow.Columns.Remove("MessageId")
        gvSmsDetails.DataSource = dtSmsDetailShow
        gvSmsDetails.AutoResizeColumns()
        gvSmsDetails.Rows(0).Selected = True
    End Sub

    Private Sub GetSmsDetails(ByVal strPayoutId As String)
        Dim strLang As String = "en"
        Dim obj7ElevenSrhSmsHistResponse As CRS_Util.clsJSONBusinessObj.cls7ElevenSrhSmsHistResponse
        obj7ElevenSrhSmsHistResponse = CRS_Util.clsJSONTool.Call7ElevenSrhSmsHist(Convert.ToInt64(strPayoutId), strLang, strMerchantId)
        If Not obj7ElevenSrhSmsHistResponse Is Nothing AndAlso obj7ElevenSrhSmsHistResponse.message.Contains("Request success") Then
            InitDtSmsDetail()
            AddDtSmsDetailRow(obj7ElevenSrhSmsHistResponse)
            SetGvSmsDetail()
        Else
            Dim dtSmsDetailShow As New DataTable
            gvSmsDetails.DataSource = dtSmsDetailShow
            txtSmsMessage.Text = ""
        End If
    End Sub

    'Private Sub gvPayoutDetail_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gvPayoutDetail.MouseDown
    '    Try
    '        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then 'If want to change by selecting rows instead of cell, change to selectedCells
    '            Dim intSelectRow As Integer = sender.HitTest(e.X, e.Y).RowIndex
    '            If intSelectRow = -1 Then
    '                Exit Try
    '            End If
    '            gvPayoutDetail.ClearSelection()
    '            gvPayoutDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '            gvPayoutDetail.Rows(intSelectRow).Selected = True
    '            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
    '            ResettxtRemarks(intSelectRow)
    '            EnablebtnSaveRemark()
    '            ResetQrCodeBtn(gvPayoutDetail.Rows(intSelectRow).Cells("Status").Value.ToString)
    '            If GetTranDetail(strPayoutID) Then
    '                If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
    '                    If GetTranDetail(strPayoutID) Then
    '                        Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
    '                        Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
    '                        If GetTranHistory(strPayoutID, strTranId) Then
    '                            If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
    '                                Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
    '                                'ResetgbSmsDetails(intSelectRow3)
    '                                txtRemarks.Enabled = True
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            GetSmsDetails(strPayoutID)
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub gvTransactions_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gvTransactions.MouseDown
    '    Try
    '        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
    '            Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
    '            Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
    '            If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
    '                'Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
    '                Dim intSelectRow1 As Integer = sender.HitTest(e.X, e.Y).RowIndex
    '                If intSelectRow1 = -1 Or intSelectRow1 > gvTransactions.Rows.Count - 1 Then
    '                    Exit Try
    '                End If
    '                gvTransactions.ClearSelection()
    '                gvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '                gvTransactions.Rows(intSelectRow1).Selected = True
    '                Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
    '                If GetTranHistory(strPayoutID, strTranId) Then
    '                    If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
    '                        Dim intSelectRow3 As Integer = gvTranHistory.CurrentCell.RowIndex
    '                        'ResetgbSmsDetails(intSelectRow3)
    '                        txtRemarks.Enabled = True
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '    End Try
    'End Sub

    'Private Sub gvTranHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gvTranHistory.MouseDown
    '    Try
    '        If (Not gvPayoutDetail.CurrentCell Is Nothing) AndAlso gvPayoutDetail.CurrentCell.RowIndex >= 0 AndAlso gvPayoutDetail.SelectedRows.Count > 0 Then
    '            If (Not gvTransactions.CurrentCell Is Nothing) AndAlso gvTransactions.CurrentCell.RowIndex >= 0 AndAlso gvTransactions.SelectedRows.Count > 0 Then
    '                Dim intSelectRow As Integer = gvPayoutDetail.CurrentCell.RowIndex
    '                Dim strPayoutID As String = gvPayoutDetail.Rows(intSelectRow).Cells("PayoutID").Value.ToString
    '                Dim intSelectRow1 As Integer = gvTransactions.CurrentCell.RowIndex
    '                Dim strTranId As String = gvTransactions.Rows(intSelectRow1).Cells("Trans ID").Value.ToString
    '                If (Not gvTranHistory.CurrentCell Is Nothing) AndAlso gvTranHistory.CurrentCell.RowIndex >= 0 AndAlso gvTranHistory.SelectedRows.Count > 0 Then
    '                    Dim intSelectRow3 As Integer = sender.HitTest(e.X, e.Y).RowIndex
    '                    If intSelectRow3 = -1 Or intSelectRow3 > gvTranHistory.Rows.Count - 1 Then
    '                        Exit Try
    '                    End If
    '                    gvTranHistory.ClearSelection()
    '                    gvTranHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '                    gvTranHistory.Rows(intSelectRow3).Selected = True
    '                    'ResetgbSmsDetails(intSelectRow3)
    '                    txtRemarks.Enabled = True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '    End Try
    'End Sub
End Class