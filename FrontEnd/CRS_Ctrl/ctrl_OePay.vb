'********************************************************************
' Create By:  Stephen Ma
' Date:         2018-06-01
' Project:      ITSR870
' Ref:          SM20180601
' Changes:      O!epay Tab - Copy from 'TapNGo' Tab
'********************************************************************
Public Class ctrl_OePay
    Private strPolicyNo As String = ""
    Private strProductID As String = ""
    Private strMerchantID As String = ""
    Private strAccountID As String = ""
    Private strLang As String = "en"
    Private decBalance As Decimal = 0.0
    Private strBalanceCcy As String = ""
    Private strBalanceDate As String = ""
    Private strStartDate As String = ""
    Private strEndDate As String = ""
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private dsPolicyHead As New DataSet 'Policy head dataset
    Private EffDate As String = ""  'Effective date  
    Private dsView As DataSet = New DataSet("CSView")


#Region " Property Setting"
    Public Property dbHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
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

    'input
    Public Property PolicyNo() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property

    Public Property Lang() As String
        Get
            Return strLang
        End Get
        Set(ByVal value As String)
            strLang = value
        End Set
    End Property

    Public Property ProductID() As String
        Get
            Return strProductID
        End Get
        Set(ByVal value As String)
            strProductID = value
        End Set
    End Property
    Public Property AccountID() As String
        Get
            Return strAccountID
        End Get
        Set(ByVal value As String)
            strAccountID = value
        End Set
    End Property
    Public Property MerchantID() As String
        Get
            Return strMerchantID
        End Get
        Set(ByVal value As String)
            strMerchantID = value
        End Set
    End Property
    Public Property Balance() As Decimal
        Get
            Return decBalance
        End Get
        Set(ByVal value As Decimal)
            decBalance = value
        End Set
    End Property
    Public Property BalanceCCy() As String
        Get
            Return strBalanceCcy
        End Get
        Set(ByVal value As String)
            strBalanceCcy = value
        End Set
    End Property
    Public Property BalanceDate() As String
        Get
            Return strBalanceDate
        End Get
        Set(ByVal value As String)
            strBalanceDate = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return strStartDate
        End Get
        Set(ByVal value As String)
            strStartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return strEndDate
        End Get
        Set(ByVal value As String)
            strEndDate = value
        End Set
    End Property
    Private Sub ShowPolicyRecord()

        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now
        strFuncStartTime = Now
        Try
            Ctrl_POS_Scrn_Head1.policyInuse = strPolicyNo.Trim
            Ctrl_POS_Scrn_Head1.dbHeader = Me.dbHeader
            Ctrl_POS_Scrn_Head1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_Scrn_Head1.ShowPolicyRcd()


            dsPolicyHead = Ctrl_POS_Scrn_Head1.currdsInuse
            If dsPolicyHead.Tables.Count > 0 Then
                EffDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
            End If
        Catch ex As Exception
        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformatio", "ShowPolicyRecord", "", strMainStartTime, strMainEndTime, strPolicyNo.Trim, "", "")

        End Try
    End Sub

    Public Sub showOePayInfo()
        ShowPolicyRecord()
        policyCcyVal.Text = "-"
        policyBalVal.Text = "-"
        policyAsOfDateVal.Text = "-"
        StartDatePicker.Enabled = False
        EndDatePicker.Enabled = False
        SearchButton.Enabled = False
        ResetBtn.Enabled = False
        getCSView()
        strStartDate = Now.ToString("yyyy-MM-dd")
        strEndDate = Now.ToString("yyyy-MM-dd")
    End Sub

    Public Sub getCSView()
        Dim objMerchantProductResponse As CRS_Util.clsJSONBusinessObj.clsMerchantProductResponse = CRS_Util.clsJSONTool.CallGetMerchantProductInfo(Me.PolicyNo.Trim, Me.Lang)
        If objMerchantProductResponse.result = True Then
            strProductID = objMerchantProductResponse.productId
            strMerchantID = objMerchantProductResponse.merchantId
            strAccountID = objMerchantProductResponse.accountId
        End If

        Dim objMerchantTxnHistoryResp As CRS_Util.clsJSONBusinessObj.clsMerchantTxnHistoryResponse = CRS_Util.clsJSONTool.CallMerchantTxnHistory(Me.PolicyNo.Trim, Me.Lang, Me.ProductID, Me.MerchantID, Me.AccountID, Me.strStartDate, Me.strEndDate, True)

        If objMerchantTxnHistoryResp.result = True Then
            StartDatePicker.Enabled = True
            EndDatePicker.Enabled = True
            SearchButton.Enabled = True
            ResetBtn.Enabled = True
            dsView.Tables.Clear()
            Dim dt As DataTable = dsView.Tables.Add("Table1")
            dt.Columns.Add("Transaction Date", Type.GetType("System.String"))
            dt.Columns.Add("Currency", Type.GetType("System.String"))
            dt.Columns.Add("Amount", Type.GetType("System.Decimal"))
            dt.Columns.Add("Transaction Entry", Type.GetType("System.String"))

            strBalanceCcy = objMerchantTxnHistoryResp.policyBalanceCcy
            decBalance = objMerchantTxnHistoryResp.policyBalance
            strBalanceDate = objMerchantTxnHistoryResp.policyBalanceAsOfDate

            policyCcyVal.Text = strBalanceCcy
            policyBalVal.Text = decBalance
            policyAsOfDateVal.Text = strBalanceDate

            For Each CSView As CRS_Util.clsJSONBusinessObj.clsMerchantTxnHistory In objMerchantTxnHistoryResp.transactionHistory
                Dim dr As DataRow = dt.NewRow()
                dr.Item("Transaction Date") = CSView.txnDate
                dr.Item("Currency") = CSView.txnCcy
                dr.Item("Amount") = CSView.txnAmt
                dr.Item("Transaction Entry") = CSView.txnEntry
                dt.Rows.Add(dr)
            Next

            dgvCSView.DataSource = dsView.Tables("Table1")
            dgvCSView.Refresh()

            For Each c As DataGridViewColumn In dgvCSView.Columns
                If c.Index = 0 Or c.Index = 3 Then 'txndate and entry
                    dgvCSView.Columns(c.Index).Width = 200
                Else
                    dgvCSView.Columns(c.Index).Width = 100
                End If
            Next c
        End If

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartDatePicker.ValueChanged
        strStartDate = StartDatePicker.Value.ToString("yyyy-MM-dd")
    End Sub

    Private Sub EndDatePicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndDatePicker.ValueChanged
        strEndDate = EndDatePicker.Value.ToString("yyyy-MM-dd")
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        If strEndDate.CompareTo(strStartDate) = -1 Then
            MsgBox("End Date is smaller than Start Date")
        Else
            getCSView()
        End If

    End Sub

    Private Sub ResetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetBtn.Click
        strStartDate = ""
        strEndDate = ""
        SearchButton_Click(sender, e)
    End Sub
End Class
