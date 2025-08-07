Public Class ctrl_LevyHistory

    Public strPolicyNo As String
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private objMQQueHeader As Utility.Utility.MQHeader      'MQQueHeader includes MQ conn. parameters
    Private objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters
    Private objPOSHeader As Utility.Utility.POSHeader       'POSHeader indicates transaction id and type
    Private objUtility As Utility.Utility                   'For calling Utility object
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS     'For calling clsPOS class

    Private dsCurr As New DataSet


    Public zlvysysForLevyOverdue As String
    Public ccdateForLevyOverdue As String
    Public tranNoForLevyOverdue As String

#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region


    Public Property PolicyNoInUse()
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value)
            strPolicyNo = value
        End Set
    End Property

    Public Sub ShowLevyHistory()

        Dim dsLevyHistory As New DataSet
        Dim strErr As String = ""

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader
        clsPOS.CiwHeader = objDBHeader
        clsPOS.GetLevyHistoryHeader(strPolicyNo, dsLevyHistory, strErr)

        If (strErr <> "") Then
            MsgBox(strErr)
            Exit Sub
        End If

        dgvLevyHeader.DataSource = dsLevyHistory.Tables(0)
        dgvLevyHeader.Refresh()

    End Sub

    Private Sub DgvLevyHeader_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles dgvLevyHeader.SelectionChanged

        Dim zLvySys As String = ""
        Dim ccDate As String = ""
        Dim dsLevyBillingRecord As New DataSet

        ' Dim dsLevyReceviceRecord As New DataSet
        Dim strErr As String = ""
        Dim ccDateToDate As DateTime = DateTime.Now()

        Try
            If (dgvLevyHeader.SelectedRows.Count <= 0) Then
                Exit Sub
            End If

            If (IsDBNull(dgvLevyHeader.SelectedRows(0).Cells(0).Value) OrElse dgvLevyHeader.SelectedRows(0).Cells(0).Value Is Nothing OrElse dgvLevyHeader.SelectedRows(0).Cells(0).Value = "") Then
                Exit Sub
            End If

            zLvySys = dgvLevyHeader.SelectedRows(0).Cells(0).Value.ToString
            ccDate = dgvLevyHeader.SelectedRows(0).Cells(2).Value.ToString
            'ccDateToDate = DateTime.ParseExact(ccDate, "yyyyMMdd", Globalization.CultureInfo.InvariantCulture)

            clsPOS.GetLevyHistoryBillingRecord(strPolicyNo, zLvySys, ccDate, dsLevyBillingRecord, strErr)
            'clsPOS.GetLevyHistorySettlementRecord(strPolicyNo, ccDate, dsLevyReceviceRecord, strErr)

            If ((Not dsLevyBillingRecord Is Nothing) AndAlso dsLevyBillingRecord.Tables.Count > 0) Then
                dgvLevyBillingRecord.DataSource = dsLevyBillingRecord.Tables(0)
                dgvLevyBillingRecord.Refresh()
            End If



            'If ((Not dsLevyReceviceRecord Is Nothing) AndAlso dsLevyReceviceRecord.Tables.Count > 0) Then
            '    dgvLevyReceviceRecord.DataSource = dsLevyReceviceRecord.Tables(0)
            '    dgvLevyReceviceRecord.Refresh()
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dgvLevyBillingRecord_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles dgvLevyBillingRecord.SelectionChanged

        Dim tranNo As String = ""
        Dim strErr As String = ""
        Dim dsLevyReceviceRecord As New DataSet



        Try
            If (dgvLevyBillingRecord.SelectedRows.Count <= 0) Then
                Exit Sub
            End If

            If (IsDBNull(dgvLevyBillingRecord.SelectedRows(0).Cells(0).Value) OrElse dgvLevyBillingRecord.SelectedRows(0).Cells(0).Value Is Nothing) Then
                Exit Sub
            End If

            tranNo = dgvLevyBillingRecord.SelectedRows(0).Cells(0).Value.ToString

            clsPOS.GetLevyHistorySettlementRecord(strPolicyNo, tranNo, dsLevyReceviceRecord, strErr)

            If ((Not dsLevyReceviceRecord Is Nothing) AndAlso dsLevyReceviceRecord.Tables.Count > 0) Then
                dgvLevyReceviceRecord.DataSource = dsLevyReceviceRecord.Tables(0)
                dgvLevyReceviceRecord.Refresh()
            End If

            zlvysysForLevyOverdue = dgvLevyBillingRecord.SelectedRows(0).Cells(12).Value.ToString
            ccdateForLevyOverdue = dgvLevyBillingRecord.SelectedRows(0).Cells(11).Value.ToString
            tranNoForLevyOverdue = tranNo

            GetLevyOverDueFollowUpRecord(zlvysysForLevyOverdue, ccdateForLevyOverdue, tranNoForLevyOverdue)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub GetLevyOverDueFollowUpRecord(ByVal ZLVYSYS As String, _
                                                    ByVal CCDATE As String, _
                                                    ByVal TRANNO As String)

        Dim dsLevyOutstandingRecord As New DataSet
        Dim strErr As String = ""

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader
        clsPOS.CiwHeader = objDBHeader
        clsPOS.GetLevyOverdueFollowUp(ZLVYSYS, strPolicyNo, CCDATE, TRANNO, dsLevyOutstandingRecord, strErr)

        If ((Not dsLevyOutstandingRecord Is Nothing) AndAlso dsLevyOutstandingRecord.Tables.Count > 0) Then
            dgvLevyOutstandingRecord.DataSource = dsLevyOutstandingRecord.Tables(0)
            dgvLevyOutstandingRecord.Columns(1).Visible = False
            dgvLevyOutstandingRecord.Columns(6).Visible = False
            dgvLevyOutstandingRecord.Columns(4).Width = 200
            dgvLevyOutstandingRecord.Refresh()
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim frmLevyOutstandingForm As New frmLevyOutstandingForm

        frmLevyOutstandingForm.objMQQueHeader = objMQQueHeader
        frmLevyOutstandingForm.objDBHeader = objDBHeader
        frmLevyOutstandingForm.isNew = True
        frmLevyOutstandingForm.policyNumber = strPolicyNo

        frmLevyOutstandingForm.ccdateForLevyOverdue = ccdateForLevyOverdue
        frmLevyOutstandingForm.tranNoForLevyOverdue = tranNoForLevyOverdue
        frmLevyOutstandingForm.zlvysysForLevyOverdue = zlvysysForLevyOverdue

        frmLevyOutstandingForm.ShowDialog()
        GetLevyOverDueFollowUpRecord(zlvysysForLevyOverdue, ccdateForLevyOverdue, tranNoForLevyOverdue)
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim frmLevyOutstandingForm As New frmLevyOutstandingForm

        If (dgvLevyOutstandingRecord.SelectedRows.Count <= 0) Then
            MsgBox("Please select the record")
            Exit Sub
        End If

        If (IsDBNull(dgvLevyOutstandingRecord.SelectedRows(0).Cells(0).Value) OrElse dgvLevyOutstandingRecord.SelectedRows(0).Cells(0).Value Is Nothing) Then
            MsgBox("Please select the record")
            Exit Sub
        End If

        frmLevyOutstandingForm.objMQQueHeader = objMQQueHeader
        frmLevyOutstandingForm.objDBHeader = objDBHeader
        frmLevyOutstandingForm.isNew = False

        frmLevyOutstandingForm.policyNumber = strPolicyNo
        frmLevyOutstandingForm.means = dgvLevyOutstandingRecord.SelectedRows(0).Cells(1).Value.ToString()
        frmLevyOutstandingForm.status = dgvLevyOutstandingRecord.SelectedRows(0).Cells(5).Value.ToString()
        frmLevyOutstandingForm.count = dgvLevyOutstandingRecord.SelectedRows(0).Cells(3).Value.ToString()
        frmLevyOutstandingForm.remarks = dgvLevyOutstandingRecord.SelectedRows(0).Cells(4).Value.ToString()
        frmLevyOutstandingForm.overdueId = dgvLevyOutstandingRecord.SelectedRows(0).Cells(6).Value.ToString()

        frmLevyOutstandingForm.lbOverdueId.Text = dgvLevyOutstandingRecord.SelectedRows(0).Cells(6).Value.ToString()
        frmLevyOutstandingForm.ccdateForLevyOverdue = ccdateForLevyOverdue
        frmLevyOutstandingForm.tranNoForLevyOverdue = tranNoForLevyOverdue
        frmLevyOutstandingForm.zlvysysForLevyOverdue = zlvysysForLevyOverdue

        frmLevyOutstandingForm.ShowDialog()
        GetLevyOverDueFollowUpRecord(zlvysysForLevyOverdue, ccdateForLevyOverdue, tranNoForLevyOverdue)

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'GetLevyOverDueFollowUpRecord()

    End Sub
End Class
