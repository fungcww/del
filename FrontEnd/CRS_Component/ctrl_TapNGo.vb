'********************************************************************
' Create By:  Kay Tsang
' Date:         11 Nov 2016
' Project:      ITSR492
' Ref:          KT20161111
' Changes:      Tap & Go tab
'********************************************************************
Public Class ctrl_TapNGo
    Private strPolicyNo As String = ""
    Private strCustomerID As String = ""
    Private strLang As String = "en"
    Private strLockStatus As String = ""
    Private strLockDate As String = ""
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

    Public Property CustomerID() As String
        Get
            Return strCustomerID
        End Get
        Set(ByVal value As String)
            strCustomerID = value
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

    'output
    Public Property LockStatus() As String
        Get
            Return strLockStatus
        End Get
        Set(ByVal value As String)
            strLockStatus = value
        End Set
    End Property

    Public Property LockDate() As String
        Get
            Return strLockDate
        End Get
        Set(ByVal value As String)
            strLockDate = value
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

    Public Sub showTapNGoInfo()
        ShowPolicyRecord()
        checkOTPLockStatus()
        getCSView()
    End Sub

    Public Sub checkOTPLockStatus()
        strCustomerID = Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse
        Dim objCheckOTPLockStatusResponse As CRS_Util.clsJSONBusinessObj.clsCheckOTPLockStatusResponse = CRS_Util.clsJSONTool.CallCheckOTPLockStatus(Me.PolicyNo.Trim, Me.CustomerID.Trim, Me.Lang)
        If objCheckOTPLockStatusResponse.msg.resultCode = "0" Then 'success
            Dim dt As String = IIf(objCheckOTPLockStatusResponse.lockDate Is Nothing, Nothing, objCheckOTPLockStatusResponse.lockDate)
            If Not dt Is Nothing Then
                strLockDate = Format(CDate(dt), "yyyy-MM-dd")
                strLockStatus = "LOCKED"
            Else
                strLockDate = ""
                strLockStatus = "NOT LOCKED"
            End If
        Else
            strLockDate = Me.LockDate
            strLockStatus = "N/A"
        End If
        RefreshDisplay()
    End Sub

    Public Sub RefreshDisplay()
        Me.lblLockDate.Text = Me.LockDate
        Me.lblLockStatus.Text = Me.LockStatus
        Me.btnUnlock.Enabled = IIf(Me.LockStatus = "LOCKED", True, False) 'enable if status = LOCKED
    End Sub

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        Dim boolResult = False
        Dim objUnfreezeOTPFunctionResponse As CRS_Util.clsJSONBusinessObj.clsUnfreezeOTPFunctionResponse = CRS_Util.clsJSONTool.CallUnfreezeOTPFunction(Me.PolicyNo.Trim, Me.CustomerID.Trim, Me.Lang)
        If objUnfreezeOTPFunctionResponse.msg.resultCode = "0" Then ''success
            boolResult = True
        Else
            boolResult = False
        End If
        checkOTPLockStatus()
    End Sub

    Public Sub getCSView()
        strCustomerID = Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse
        Dim objGetCSViewResponse As CRS_Util.clsJSONBusinessObj.clsGetCSViewResponse = CRS_Util.clsJSONTool.CallGetCSView(Me.CustomerID.Trim, Me.Lang)
        Dim dt As DataTable = dsView.Tables.Add("Table1")
        dt.Columns.Add("Policy ID", Type.GetType("System.String"))
        dt.Columns.Add("Customer ID", Type.GetType("System.String"))
        dt.Columns.Add("Paired", Type.GetType("System.String"))
        dt.Columns.Add("Allow to Linkup/ Withdraw", Type.GetType("System.String"))
        dt.Columns.Add("Last Linkup Request ID", Type.GetType("System.String"))
        dt.Columns.Add("Last Linkup Error", Type.GetType("System.String"))
        dt.Columns.Add("Last Txn Trans ID", Type.GetType("System.String"))
        dt.Columns.Add("Last Txn Status", Type.GetType("System.String"))
        dt.Columns.Add("Last Txn Request ID", Type.GetType("System.String"))
        dt.Columns.Add("Last Txn Error", Type.GetType("System.String"))
        dt.Columns.Add("Policy Principal", Type.GetType("System.Double"))
        dt.Columns.Add("Current Principal", Type.GetType("System.Double"))
        If objGetCSViewResponse.msg.resultCode = "0" Then 'success
            For Each CSView As CRS_Util.clsJSONBusinessObj.CSView In objGetCSViewResponse.csvList
                Dim dr As DataRow = dt.NewRow()
                dr.Item("Policy ID") = CSView.policyId
                dr.Item("Customer ID") = CSView.customerId
                dr.Item("Paired") = CSView.paired
                dr.Item("Allow to Linkup/ Withdraw") = CSView.allowToLinkupWithdraw
                dr.Item("Last Linkup Request ID") = IIf(CSView.lastLinkupRequestId = "-1", "", CSView.lastLinkupRequestId)
                dr.Item("Last Linkup Error") = IIf(Not CSView.lastLinkupResponse Is Nothing, CSView.lastLinkupResponse, "")
                dr.Item("Last Txn Trans ID") = IIf(CSView.lastTxnTransId = "-1", "", CSView.lastTxnTransId)
                Select Case CSView.lastTxnStatus
                    Case "PE"
                        CSView.lastTxnStatus = "PENDING"
                    Case "EX"
                        CSView.lastTxnStatus = "EXECUTING"
                    Case "CA"
                        CSView.lastTxnStatus = "CANCEL"
                    Case "CO"
                        CSView.lastTxnStatus = "COMPLETED"
                    Case "IN"
                        CSView.lastTxnStatus = "INCOMPLETE"
                End Select



                dr.Item("Last Txn Status") = CSView.lastTxnStatus
                dr.Item("Last Txn Request ID") = IIf(CSView.lastTxnRequestId = "-1", "", CSView.lastTxnRequestId)
                dr.Item("Last Txn Error") = IIf(Not CSView.lastTxnResponse Is Nothing, CSView.lastTxnResponse, "")
                dr.Item("Policy Principal") = CSView.dailyStartPrincipal
                dr.Item("Current Principal") = CSView.dailyCurrentPrincipal
                dt.Rows.Add(dr)
            Next
        End If

        dgvCSView.DataSource = dsView.Tables("Table1")
        dgvCSView.Refresh()

        For Each c As DataGridViewColumn In dgvCSView.Columns
            If c.Index = 2 Then 'paired
                dgvCSView.Columns(c.Index).Width = 50
            ElseIf c.Index = 5 Or c.Index = 9 Then 'error
                dgvCSView.Columns(c.Index).Width = 300
            Else
                dgvCSView.Columns(c.Index).Width = 100
            End If
        Next c

    End Sub
End Class
