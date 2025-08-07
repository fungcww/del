Public Class ctrl_FinancialInfo
    Private strPolicyNo As String = ""

    Private dsCurr As New DataSet
    Dim dsReceData As New DataSet
    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
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

    Private Sub FormatDgvSubAcctBal(ByVal dtCust As DataTable)
        Try
            dgvSubAcctBal.Columns.Clear()
            Dim aCol0 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol6 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol7 As New Windows.Forms.DataGridViewTextBoxColumn

            With aCol0
                .DataPropertyName = "RefNo"
                .Name = "RefNo"
                .HeaderText = "RefNo"
                .Width = 100
                .Visible = False
                .ReadOnly = False
            End With

            With aCol1
                .DataPropertyName = "Entity"
                .Name = "Entity"
                .HeaderText = "Entity"
                .Width = 70
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "subAccountCode"
                .Name = "PolicyCompLev"
                .HeaderText = "Sub Account Code"
                '.HeaderText = "Account Code"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol3
                .DataPropertyName = "SubAccountCodeDesc"
                .Name = "Acct_Code_Desc"
                .HeaderText = "Sub Account Code Desc"
                .Width = 120

                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "subAccountType"
                .Name = "SubAcctType"
                .HeaderText = "Sub Account Type"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "SubAccountTypeDesc"
                .Name = "Acct_Type_Desc"
                .HeaderText = "Sub Account Type Desc"
                .Width = 100
                .Visible = True
                .ReadOnly = False
            End With

            With aCol6
                .DataPropertyName = "OriginalCurr"
                .Name = "SubAcctCurr"
                .HeaderText = "Sub Account Currency"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol7
                .DataPropertyName = "CurrBalance"
                .Name = "subAcctBal"
                .HeaderText = "Sub Account Balance"
                .Width = 100
                .ReadOnly = False
            End With

            With dgvSubAcctBal.Columns
                .Add(aCol0)
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
    Private Sub FormatDgvSubAcctPosting(ByVal dtCust As DataTable)
        Try
            dgvSubAcctPosting.Columns.Clear()
            Dim aCol0 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn

            With aCol0
                .DataPropertyName = "RefNo1"
                .Name = "RefNo1"
                .HeaderText = "RefNo1"
                .Width = 100
                .ReadOnly = False
            End With
            With aCol1
                .DataPropertyName = "EffectiveDate"
                .Name = "EffDate"
                .HeaderText = "Effective Date"
                .Width = 120
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "TranNo"
                .Name = "TranNo"
                .HeaderText = "Transaction No"
                .Width = 120
                .ReadOnly = False
            End With

            With aCol3
                .DataPropertyName = "GLCode"
                .Name = "GLCode"
                .HeaderText = "G/L Code"
                .Width = 130
                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "OriginalAmount"
                .Name = "OriginalAmount"
                .HeaderText = "Original Amount"
                .Width = 120
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "AccAmount"
                .Name = "AccountAmount"
                .HeaderText = "Account Amount"
                .Width = 120
                .ReadOnly = False
                .Visible = False    ' ES001 - Remove this column for i.ulife/Dual Currency project
            End With

            With dgvSubAcctPosting.Columns
                .Add(aCol0)
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrepareSendSubAcctBal(ByRef dsSendData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicyNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceSubAcctBal(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Entity")
            dtReceData.Columns.Add("Acct_Code")
            'dtReceData.Columns.Add("Acct_Code_Desc")
            dtReceData.Columns.Add("Acct_Type")
            'dtReceData.Columns.Add("Acct_Type_Desc")
            dtReceData.Columns.Add("Curr_Bal")
            dtReceData.Columns.Add("Curr")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareSendSubAcctPost(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            Dim dr As DataRow = dtReceData.NewRow()
            dtReceData.Columns.Add("PolicyNo")
            dtReceData.Columns.Add("Entity")
            dr("PolicyNo") = strPolicyNo
            dr("Entity") = dgvSubAcctBal.Item("Entity", dgvSubAcctBal.SelectedRows(0).Index).ToString
            dtReceData.Rows.Add(dr)

            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceSubAcctPost(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Eff_Date")
            dtReceData.Columns.Add("Trans_No")
            dtReceData.Columns.Add("GLCode")
            dtReceData.Columns.Add("Ori_Amt")
            dtReceData.Columns.Add("Acct_Amt")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub ShowSubAcctBalRcd()
        Try
            If dsReceData.Tables.Count > 0 Then
                Dim iCount As Integer = dsReceData.Tables.Count
                For i As Integer = 0 To iCount - 1
                    dsReceData.Tables.RemoveAt(0)
                Next
            End If
            Dim strErr As String = ""
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            'create send dataset
            Dim dsSendData As New DataSet
            '            Dim dsReceData As New DataSet

            'Prepare tables for transaction
            If PrepareSendSubAcctBal(dsSendData, strErr) = False Then
                MsgBox(strErr)
            End If
            If PrepareReceSubAcctBal(dsReceData, strErr) = False Then
                MsgBox(strErr)
            End If

            'Getting Mandate List
            'clsCRS.MQQueuesHeader = Me.objMQQueHeader
            'If clsCRS.getMandateList(dsSendData, dsReceData, strErr) = False Then
            ' MsgBox(strErr)
            'End If

            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            If clsPOS.GetSubAccountBalance(dsSendData, dsReceData, strErr) = False Then
                MsgBox(strErr)
            End If

            If dsReceData.Tables(1).Rows.Count > 0 Then
                'dgvSubAcctBal.DataSource = dsReceData
                'dgvSubAcctBal.DataMember = "Table1"
                dgvSubAcctBal.DataSource = Nothing
                dgvSubAcctBal.DataSource = dsReceData.Tables(1)
                FormatDgvSubAcctBal(dsReceData.Tables(1))

                'dgvSubAcctBal.Refresh()
                'dgvSubAcctBal.DataSource = dsReceData.Tables(1)
                dgvSubAcctBal.Columns("RefNo").Visible = False
                dgvSubAcctBal.Columns("Entity").DisplayIndex = 0
                dgvSubAcctBal.Columns("PolicyCompLev").DisplayIndex = 1
                dgvSubAcctBal.Columns("Acct_Code_Desc").DisplayIndex = 2
                dgvSubAcctBal.Columns("SubAcctType").DisplayIndex = 3
                dgvSubAcctBal.Columns("Acct_Type_Desc").DisplayIndex = 4
                dgvSubAcctBal.Columns("SubAcctCurr").DisplayIndex = 5
                dgvSubAcctBal.Columns("subAcctBal").DisplayIndex = 6
                dgvSubAcctBal.Refresh()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvSubAcctBal_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSubAcctBal.CellContentClick
        'Try
        '    If PolicyNoInUse.Trim <> "" And dgvSubAcctBal.Item("Entity", dgvSubAcctBal.SelectedRows(0).Index).ToString <> "" Then

        '        'Create send dataset
        '        Dim dsSendData As New DataSet
        '        '                Dim dsReceData As New DataSet
        '        Dim strErr As String = ""

        '        clsCRS.MQQueuesHeader = Me.objMQQueHeader

        '        'Prepare tables for transaction
        '        If PrepareSendSubAcctPost(dsSendData, strErr) = False Then
        '            MsgBox(strErr)
        '        End If
        '        If PrepareReceSubAcctPost(dsReceData, strErr) = False Then
        '            MsgBox(strErr)
        '        End If

        '        'Sub Account Posting
        '        clsCRS.MQQueuesHeader = Me.objMQQueHeader
        '        If clsCRS.getSubAcctPosting(dsSendData, dsReceData, strErr) = False Then
        '            MsgBox(strErr)
        '        End If

        '        If dsReceData.Tables(0).Rows.Count > 0 Then
        '            dgvSubAcctPosting.DataSource = dsReceData
        '            dgvSubAcctPosting.DataMember = "Table1"
        '            FormatDgvSubAcctPosting(dsReceData.Tables(0))
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub dgvSubAcctBal_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSubAcctBal.SelectionChanged
        Try
            '' ''If PolicyNoInUse.Trim <> "" And dgvTranHist.Item("Tran_No", dgvTranHist.SelectedRows(0).Index).ToString <> "" Then
            Dim cm As CurrencyManager
            cm = CType(Me.BindingContext(dgvSubAcctBal.DataSource), CurrencyManager)
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
            Dim strerr As String = ""
            dsReceData.Tables(2).DefaultView.RowFilter = "refno1='" & cm.Current.row.Item("refno") & "'"
            If PrepareSubAcctDetailAssignment(dsReceData.Tables(2).DefaultView, dsRecedetail, strerr) Then
                'If dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'").Length <> 0 Then

                '                dsRecedetail.Tables.Add(dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'")(0).Table.Copy)
                If dsRecedetail.Tables(0).Rows.Count > 0 Then
                    'dgvTranPost.DataSource = dsReceData
                    'dgvTranPost.DataMember = "Table1"
                    FormatDgvSubAcctPosting(dsRecedetail.Tables(0))

                    dgvSubAcctPosting.DataSource = dsRecedetail.Tables(0) 'dsReceData.Tables(2).Select("refno1='" & cm.Current.row.Item("refno") & "'")(0).Table
                    dgvSubAcctPosting.Columns("RefNo1").Visible = False
                    dgvSubAcctPosting.Columns("EffDate").DisplayIndex = 0
                    dgvSubAcctPosting.Columns("TranNo").DisplayIndex = 1
                    dgvSubAcctPosting.Columns("GLCode").DisplayIndex = 2
                    dgvSubAcctPosting.Columns("OriginalAmount").DisplayIndex = 3
                    dgvSubAcctPosting.Columns("AccountAmount").DisplayIndex = 4
                    'dgvSubAcctPosting.Columns("Acct_Amt").DisplayIndex = 5
                    'dgvSubAcctPosting.Columns("Acct_Curr").DisplayIndex = 6
                End If
            Else
                dgvSubAcctPosting.DataSource = Nothing
                dgvSubAcctPosting.Refresh()
                dgvSubAcctPosting.Update()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function PrepareReceSubAcctDtl(ByRef dtCADetail As Data.DataTable, ByRef strErr As String) As Boolean
        'For i As Integer = 1 To 10
        dtCADetail.Columns.Add("RefNo1", GetType(System.String))
        dtCADetail.Columns.Add("AccAmount", GetType(System.Decimal))
        dtCADetail.Columns.Add("EffectiveDate", GetType(System.String))
        dtCADetail.Columns.Add("GLCode", GetType(System.String))
        dtCADetail.Columns.Add("OriginalAmount", GetType(System.Decimal))
        dtCADetail.Columns.Add("TranNo", GetType(System.String))
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
                dr("AccAmount") = dttContCli(i)("AccAmount")
                dr("EffectiveDate") = dttContCli(i)("EffectiveDate")
                dr("GLCode") = dttContCli(i)("GLCode")
                dr("OriginalAmount") = dttContCli(i)("OriginalAmount")
                dr("TranNo") = dttContCli(i)("TranNo")
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
End Class

