Public Class ctrl_TranHist
    Private strPolicyNo As String = ""

    Private dsCurr As New DataSet
    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Dim dsReceData As New DataSet

#Region " DBLogon Properties"
    Public Property dbHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region
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
#Region " Property Setting"
    Public Property dsCurrInUse() As DataSet
        Get
            Return dsCurr
        End Get
        Set(ByVal value As DataSet)
            dsCurr = value
        End Set
    End Property
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
#End Region

    Private Sub FormatDgvTranHist(ByVal dtCust As DataTable)
        Try
            dgvTranHist.Columns.Clear()
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol6 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol7 As New Windows.Forms.DataGridViewTextBoxColumn

            With aCol1
                .DataPropertyName = "Tran_No"
                .Name = "Tran_No"
                .HeaderText = "Transaction No."
                .Width = 80
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "Date"
                .Name = "Date"
                .HeaderText = "Transaction Date"
                .Width = 80
                .ReadOnly = False
            End With

            With aCol7
                .DataPropertyName = "Date"
                .Name = "Date"
                .HeaderText = "Effective Date"
                .Width = 80
                .ReadOnly = False
            End With


            With aCol3
                .DataPropertyName = "Code"
                .Name = "Code"
                .HeaderText = "TranCode"
                .Width = 80
                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "Description"
                .Name = "Description"
                .HeaderText = "Tran Description"
                .Width = 250
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "Reversal"
                .Name = "Reversal"
                .HeaderText = "Reversal"
                .Width = 80
                .ReadOnly = False
            End With

            With aCol6
                .DataPropertyName = "RefNo"
                .Name = "RefNo"
                .HeaderText = "RefNo"
                .Width = 50
                .ReadOnly = False
                .Visible = False
            End With

            With dgvTranHist.Columns
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol7)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
                .Add(aCol6)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FormatDgvTranPost(ByVal dtCust As DataTable)
        Try
            dgvTranPost.Columns.Clear()
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol6 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol7 As New Windows.Forms.DataGridViewTextBoxColumn

            With aCol1
                .DataPropertyName = "GLCode"
                .Name = "GLCode"
                .HeaderText = "GL Code"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "Acct_Code"
                .Name = "Acct_Code"
                .HeaderText = "Sub Account Code"
                .Width = 95
                .ReadOnly = False
            End With

            With aCol3
                .DataPropertyName = "Acct_Type"
                .Name = "Acct_Type"
                .HeaderText = "Sub Account Type"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "Ori_Amt"
                .Name = "Ori_Amt"
                .HeaderText = "Original Amount"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "Ori_Curr"
                .Name = "Ori_Curr"
                .HeaderText = "Currency"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol6
                .DataPropertyName = "Acct_Amt"
                .Name = "Acct_Amt"
                .HeaderText = "Accounting Amount"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol7
                .DataPropertyName = "Acct_Curr"
                .Name = "Acct_Curr"
                .HeaderText = "Accounting Currency"
                .Width = 100
                .ReadOnly = False
            End With
            With dgvTranPost.Columns
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
                .Add(aCol6)
                .Add(aCol7)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrepareSendTranHist(ByRef dsSendData As DataSet, ByRef strErr As String, Optional ByVal strMandateRef As String = "") As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Policy_No")
            dtSendData.Columns.Add("MandateRef")
            dr("Policy_No") = strPolicyNo
            dr("MandateRef") = strMandateRef
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceTranHist(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Tran_No")
            dtReceData.Columns.Add("Date")
            dtReceData.Columns.Add("Code")
            dtReceData.Columns.Add("Description")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareSendTranPost(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            Dim dr As DataRow = dtReceData.NewRow()
            dtReceData.Columns.Add("Policy_No")
            dtReceData.Columns.Add("Tran_No")

            dr("PolicyNo") = strPolicyNo
            dr("Tran_No") = dgvTranHist.Item("Tran_No", dgvTranHist.SelectedRows(0).Index).ToString
            dtReceData.Rows.Add(dr)
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceTranPost(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("GLCode")
            dtReceData.Columns.Add("Acct_Code")
            dtReceData.Columns.Add("Acct_Type")
            dtReceData.Columns.Add("Ori_Amt")
            dtReceData.Columns.Add("Ori_Curr")
            dtReceData.Columns.Add("Acct_Amt")
            dtReceData.Columns.Add("Acct_Curr")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub ShowTranHistRcd(Optional ByVal strNextMandateRef As String = "")
        Try
            Dim strErr As String = ""
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            'create send dataset
            Dim dsSendData As New DataSet
            '            Dim dsReceData As New DataSet

            'Prepare tables for transaction
            If PrepareSendTranHist(dsSendData, strErr, strNextMandateRef) = False Then
                MsgBox(strErr)
                Exit Sub
            End If

            While dsReceData.Tables.Count > 0
                dsReceData.Tables.RemoveAt(0)
            End While
            If PrepareReceTranHist(dsReceData, strErr) = False Then
                MsgBox(strErr)
                Exit Sub
            End If

            'Getting Transaction History
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            If clsCRS.getTranHist(dsSendData, dsReceData, strErr) = False Then
                MsgBox(strErr)
                Exit Sub
            End If

            If dsReceData.Tables(1).Rows.Count > 0 Then
                'dgvTranHist.DataSource = dsReceData
                'dgvTranHist.DataMember = "Table1"
                'dgvTranHist.Columns("Tran_No").DisplayIndex = 0
                'dgvTranHist.Columns("Date").DisplayIndex = 1
                'dgvTranHist.Columns("Code").DisplayIndex = 2
                'dgvTranHist.Columns("Description").DisplayIndex = 3
                'dgvTranHist.Columns("RefNo").Visible = False
                'dgvTranHist.Columns("Code").Visible = False
                cmdNext.Enabled = True
            Else
                cmdNext.Enabled = False
            End If

            dgvTranHist.DataSource = dsReceData.Tables(1)
            FormatDgvTranHist(dsReceData.Tables(1))

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvTranHist_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTranHist.CellContentClick
        'Try
        '    If PolicyNoInUse.Trim <> "" And dgvTranHist.Item("Tran_No", dgvTranHist.SelectedRows(0).Index).ToString <> "" Then

        '    End If
        '    Dim strErr As String = ""
        '    clsCRS.MQQueuesHeader = Me.objMQQueHeader
        '    'Create send dataset
        '    Dim dsSendData As New DataSet
        '    Dim dsReceData As New DataSet

        '    'Prepare tables for transaction
        '    If PrepareSendTranPost(dsSendData, strErr) = False Then
        '        MsgBox(strErr)
        '    End If
        '    If PrepareReceTranPost(dsReceData, strErr) = False Then
        '        MsgBox(strErr)
        '    End If

        '    'Direct Debit Enquiry
        '    clsCRS.MQQueuesHeader = Me.objMQQueHeader
        '    If clsCRS.getTranPosting(dsSendData, dsReceData, strErr) = False Then
        '        MsgBox(strErr)
        '    End If

        '    If dsReceData.Tables(2).Rows.Count > 0 Then
        '        'dgvTranPost.DataSource = dsReceData
        '        'dgvTranPost.DataMember = "Table1"
        '        FormatDgvTranPost(dsReceData.Tables(2))
        '        dgvTranPost.DataSource = dsReceData.Tables(2)
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub dgvTranHist_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTranHist.SelectionChanged
        Try
            '' ''If PolicyNoInUse.Trim <> "" And dgvTranHist.Item("Tran_No", dgvTranHist.SelectedRows(0).Index).ToString <> "" Then
            Dim cm As CurrencyManager
            cm = CType(Me.BindingContext(dgvTranHist.DataSource), CurrencyManager)
            '' ''End If
            '' ''Dim strErr As String = ""
            '' ''clsCRS.MQQueuesHeader = Me.objMQQueHeader
            ' '' ''Create send dataset
            '' ''Dim dsSendData As New DataSet
            '' ''Dim dsReceData As New DataSet

            ' '' ''Prepare tables for transaction
            '' ''If PrepareSendTranPost(dsSendData, strErr) = False Then
            '' ''    MsgBox(strErr)
            '' ''End If
            '' ''If PrepareReceTranPost(dsReceData, strErr) = False Then
            '' ''    MsgBox(strErr)
            '' ''End If

            ' '' ''Direct Debit Enquiry
            '' ''clsCRS.MQQueuesHeader = Me.objMQQueHeader
            '' ''If clsCRS.getTranPosting(dsSendData, dsReceData, strErr) = False Then
            '' ''    MsgBox(strErr)
            '' ''End If
            Dim dsRecedetail As New DataSet
            Dim strErr As String = ""
            If dsReceData.Tables.Count > 2 Then
                dsReceData.Tables(2).DefaultView.RowFilter = "refno1='" & cm.Current.row.Item("refno") & "'"
                If PrepareSubAcctDetailAssignment(dsReceData.Tables(2).DefaultView, dsRecedetail, strErr) Then
                    '            If dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'").Length <> 0 Then
                    'dsRecedetail.Tables.Add(dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'")(0).Table.Copy)
                    If dsRecedetail.Tables(0).Rows.Count > 0 Then
                        'dgvTranPost.DataSource = dsReceData
                        'dgvTranPost.DataMember = "Table1"
                        FormatDgvTranPost(dsRecedetail.Tables(0))

                        dgvTranPost.DataSource = dsRecedetail.Tables(0) 'dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'")(0).Table
                        dgvTranPost.Columns("RefNo1").Visible = False
                        dgvTranPost.Columns("GLCode").DisplayIndex = 0
                        dgvTranPost.Columns("Acct_Code").DisplayIndex = 1
                        dgvTranPost.Columns("Acct_Type").DisplayIndex = 2
                        dgvTranPost.Columns("Ori_Amt").DisplayIndex = 3
                        dgvTranPost.Columns("Ori_Curr").DisplayIndex = 4
                        dgvTranPost.Columns("Acct_Amt").DisplayIndex = 5
                        dgvTranPost.Columns("Acct_Curr").DisplayIndex = 6
                    End If
                Else
                    dgvTranPost.DataSource = Nothing
                    dgvTranPost.Refresh()
                    dgvTranPost.Update()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrepareReceSubAcctDtl(ByRef dtCADetail As Data.DataTable, ByRef strErr As String) As Boolean
        'For i As Integer = 1 To 10
        dtCADetail.Columns.Add("RefNo1", GetType(System.String))
        dtCADetail.Columns.Add("Acct_Amt", GetType(System.Decimal))
        dtCADetail.Columns.Add("Acct_Curr", GetType(System.String))
        dtCADetail.Columns.Add("GLCode", GetType(System.String))
        dtCADetail.Columns.Add("Ori_Amt", GetType(System.Decimal))
        dtCADetail.Columns.Add("Ori_Curr", GetType(System.String))
        dtCADetail.Columns.Add("Acct_Code", GetType(System.String))
        dtCADetail.Columns.Add("Acct_Type", GetType(System.String))
        'dtCADetail.Columns.Add("posca_Status" & CStr(i), GetType(System.String))
        'Next
        Return True
    End Function

    Private Function PrepareSubAcctDetailAssignment(ByVal dttContCli As Data.DataView, ByRef dsCADetail As Data.DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtCADetail As New DataTable

            'Init contract assignment table
            If PrepareReceSubAcctDtl(dtCADetail, strErr) = False Then
                Return False
            End If


            'dr("posca_trans_id") = objPOSHeader.TransID
            For i As Integer = 0 To dttContCli.Count - 1
                Dim dr As DataRow = dtCADetail.NewRow
                dr("RefNo1") = dttContCli(i)("RefNo1")
                dr("Acct_Amt") = dttContCli(i)("Acct_Amt")
                dr("Acct_Curr") = dttContCli(i)("Acct_Curr")
                dr("GLCode") = dttContCli(i)("GLCode")
                dr("Ori_Amt") = dttContCli(i)("Ori_Amt")
                dr("Ori_Curr") = dttContCli(i)("Ori_Curr")
                dr("Acct_Code") = dttContCli(i)("Acct_Code")
                dr("Acct_Type") = dttContCli(i)("Acct_Type")
                dtCADetail.Rows.Add(dr)
            Next


            dsCADetail.Tables.Add(dtCADetail)
            If dttContCli.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Private Sub cmdTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTop.Click
        Call ShowTranHistRcd()
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            Dim strMandateRef As String = ""
            Dim strMaxMandateRef As String = ""
            Dim intMandateRef As Integer = 0
            Dim dtTemp As New DataTable

            If Not (dsReceData Is Nothing) Then
                If dsReceData.Tables.Count > 1 Then
                    If Not (dsReceData.Tables(1) Is Nothing) Then
                        If dsReceData.Tables(1).Rows.Count > 0 Then
                            dtTemp = dsReceData.Tables(1).Copy
                            dtTemp.DefaultView.Sort = "RefNo DESC"
                            strMaxMandateRef = dtTemp.DefaultView(0).Item("RefNo").ToString.Trim
                            intMandateRef = CInt(strMaxMandateRef)
                            intMandateRef = intMandateRef + 1
                            strMaxMandateRef = ("00000" & intMandateRef).Trim
                            strMaxMandateRef = strMaxMandateRef.Substring(strMaxMandateRef.Length - 5, 5)
                            Call ShowTranHistRcd(strMaxMandateRef)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class
