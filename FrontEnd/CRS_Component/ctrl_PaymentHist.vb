Public Class ctrl_PaymentHist
    Private dsCurr As New DataSet
    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private strPolicyNo As String
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
    Private Sub FormatDgvPaymentHist(ByVal dtCust As DataTable)
        Try
            dgvPaymentHist.Columns.Clear()
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol6 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol7 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol8 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol9 As New Windows.Forms.DataGridViewTextBoxColumn

            With aCol1
                .DataPropertyName = "Payment_Date"
                .Name = "PaymentDate"
                .HeaderText = "Payment Date"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "PayTypeCode"
                .Name = "PaymentType"
                .HeaderText = "Payment Type"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol3
                .DataPropertyName = "Curr"
                .Name = "Currency"
                .HeaderText = "Currency"
                .Width = 60
                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "RecAmt"
                .Name = "ReceivedAmount"
                .HeaderText = "Received Amount"
                .Width = 130
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "TransDesc"
                .Name = "TransactionDesc"
                .HeaderText = "Transaction Description"
                .Width = 180
                .ReadOnly = False
            End With

            With aCol6
                .DataPropertyName = "SubAcctCode"
                .Name = "subAcctCode"
                .HeaderText = "Sub Acct Code"
                .Width = 110
                .ReadOnly = False
            End With

            With aCol7
                .DataPropertyName = "subAcctType"
                .Name = "subAcctType"
                .HeaderText = "Sub Acct Type"
                .Width = 110
                .ReadOnly = False
            End With

            With aCol8
                .DataPropertyName = "PayTypeDesc"
                .Name = "PayTypeDesc"
                .HeaderText = "Pay Type Desc"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol9
                .DataPropertyName = "TransCode"
                .Name = "TransCode"
                .HeaderText = "Trans Code"
                .Width = 100
                .ReadOnly = False
            End With
            With dgvPaymentHist.Columns
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
                .Add(aCol6)
                .Add(aCol7)
                .Add(aCol8)
                .Add(aCol9)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ShowPaymentHistRcd()
        Try
            Dim strErr As String = ""
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            'create send dataset
            Dim dsSendData As New DataSet
            Dim dsReceData As New DataSet

            'Prepare tables for transaction
            If PrepareSendPaymentHist(dsSendData, strErr) = False Then
                MsgBox(strErr)
            End If
            'If PrepareRecePaymentHist(dsReceData, strErr) = False Then
            '    MsgBox(strErr)
            'End If

            'Getting Transaction History
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            If clsCRS.getPaymentHist(dsSendData, dsReceData, strErr) = False Then
                MsgBox(strErr)
            End If

            If dsReceData.Tables(0).Rows.Count > 0 Then
                'dgvPaymentHist.DataSource = dsReceData
                'dgvPaymentHist.DataMember = "Table1"
                FormatDgvPaymentHist(dsReceData.Tables(0))
                dgvPaymentHist.DataSource = dsReceData.Tables(0)
                dgvPaymentHist.Columns("PayTypeDesc").Visible = False
                dgvPaymentHist.Columns("TransCode").Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrepareSendPaymentHist(ByRef dsSendData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Policy_No")
            dr("Policy_No") = strPolicyNo
            dtSendData.Columns.Add("FromDate")
            dr("FromDate") = "11/11/2005"
            dtSendData.Columns.Add("ToDate")
            dr("ToDate") = "18/12/2007"

            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function PrepareRecePaymentHist(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Payment_Date")
            dtReceData.Columns.Add("PayTypeDesc")
            dtReceData.Columns.Add("Curr")
            dtReceData.Columns.Add("RecAmt")

            dtReceData.Columns.Add("TransCode")
            dtReceData.Columns.Add("SubAcctCode")
            dtReceData.Columns.Add("SubAcctType")
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

End Class
