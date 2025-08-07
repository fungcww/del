Public Class frmCRS_MainScreen
    Private strFormcalled As String
    Private strPolicyNo As String
    Private strClientNo As String

    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private objCIWHeader As Utility.Utility.ComHeader
    Private objComHeader As Utility.Utility.ComHeader
    Private objPOSHeader As Utility.Utility.POSHeader
    Private objUtility As Utility.Utility

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

    Public Property formCalled()
        Get
            Return strFormcalled
        End Get
        Set(ByVal value)
            strFormcalled = value
        End Set
    End Property
    Public Property PolicyNo()
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value)
            strPolicyNo = value
        End Set
    End Property
    Public Property ClientNo()
        Get
            Return strClientNo
        End Get
        Set(ByVal value)
            strClientNo = value
        End Set
    End Property
    Private Sub frmCRS_MainScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'objMQQueHeader.QueueManager = My.Settings.Qman
        'objMQQueHeader.RemoteQueue = My.Settings.WinRemoteQ
        'objMQQueHeader.ReplyToQueue = My.Settings.LAReplyQ
        'objMQQueHeader.LocalQueue = My.Settings.WinLocalQ
        'objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        lblPolicyNo.Text = strPolicyNo.Trim
        Select Case formCalled
            Case "Direct Debit Enquiry"
                tcCRS.SelectTab("tabDirDebitEnq")
                Ctrl_DirectDebitEnq1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_DirectDebitEnq1.ClientNoInUse = strClientNo.Trim
                Ctrl_DirectDebitEnq1.PolicyNoInUse = strPolicyNo.Trim
                Ctrl_DirectDebitEnq1.ShowMandateListRcd()
            Case "Financial Information"
                tcCRS.SelectTab("tabFinInfo")
                Ctrl_FinancialInfo1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_FinancialInfo1.PolicyNoInUse = strPolicyNo.Trim
                Ctrl_FinancialInfo1.ShowSubAcctBalRcd()
                Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Enquiry
                Ctrl_FinancialDtl1.showrestatmentInfo()
            Case "Transaction History"
                tcCRS.SelectTab("tabTranHist")
                Ctrl_TranHist1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_TranHist1.PolicyNoInUse = strPolicyNo.Trim
                Ctrl_TranHist1.ShowTranHistRcd()
            Case "Policy General Information"
                tcCRS.SelectTab("tabPolicyGenInfo")
                Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = strPolicyNo.Trim

                Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()
                ' ''Ctrl_POS_PolicyClient.EffDateInuse = EffDate
                ' ''Ctrl_POS_PolicyClient.PolicyNoInuse = txtPolicyNo.Text.Trim
                ' ''Ctrl_POS_PolicyClient.DBHeader = Me.DBHeader
                ' ''Ctrl_POS_PolicyClient.MQQueuesHeader = Me.MQQueuesHeader
                ' ''Ctrl_POS_PolicyClient.sPolicyStatus = dsPolicyHead.Tables(0).Rows(0)("Risk_Sts")
                ' ''Ctrl_POS_PolicyClient.modeinuse = Ctrl_POS_Scrn_Head.ModeInuse
                ' ''Ctrl_POS_PolicyClient.showClnRelation()
            Case "Payment History"
                tcCRS.SelectTab("tabPaymentHist")
                Ctrl_PaymentHist1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_PaymentHist1.PolicyNoInUse = strPolicyNo.Trim
                Ctrl_PaymentHist1.ShowPaymentHistRcd()
                Ctrl_BillingInf1.MQQueuesHeader = Me.objMQQueHeader
                Ctrl_BillingInf1.PolicyNoInUse = strPolicyNo.Trim
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim dsPolicySend As New DataSet
                Dim dsPolicyCurr As New DataSet
                Dim strTime As String = ""
                Dim strerr As String = ""
                Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
                Dim blnGetPolicy As Boolean
                Dim dr As DataRow
                Dim dtSendData As New DataTable
                dtSendData.Columns.Add("PolicyNo")
                dr = dtSendData.NewRow
                dr("PolicyNo") = strPolicyNo.Trim
                dtSendData.Rows.Add(dr)

                dsPolicySend.Tables.Add(dtSendData)
                clsPOS.MQQueuesHeader = Me.objMQQueHeader
                blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
                If dsPolicyCurr.Tables.Count > 0 Then
                    If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                        Ctrl_BillingInf1.CurrdsInUse = dsPolicyCurr
                        Ctrl_BillingInf1.EffDateInUse = dsPolicyCurr.Tables(0).Rows(0)("Sys_Bus_Date")
                        Ctrl_BillingInf1.PolicyNoInUse = strPolicyNo.Trim
                        Ctrl_BillingInf1.PModeInUse = dsPolicyCurr.Tables(0).Rows(0)("Freq")
                        Ctrl_BillingInf1.BillTypeInUse = dsPolicyCurr.Tables(0).Rows(0)("Pay_Meth")
                        'Ctrl_BillingInf1.modeinuse = POSCommCtrl.Ctrl_BillingInf.ModeName.Enquiry
                        Ctrl_BillingInf1.initCboNewBillType()
                        Ctrl_BillingInf1.AllowQuote = True
                        Ctrl_BillingInf1.showBillingInfo()
                        'Ctrl_BillingInf1.initCboNewBillType()
                    End If
                End If
            Case "Component"
                tcCRS.SelectTab("tabComponent")
                Ctrl_ChgComponent1.MQQueuesHeader = Me.MQQueuesHeader
                Ctrl_ChgComponent1.DBHeader = Me.objDBHeader
                Ctrl_ChgComponent1.PolicyNoInuse = strPolicyNo.Trim
                Ctrl_ChgComponent1.ModeInUse = Utility.Utility.ModeName.Change
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'If Ctrl_BillingInf1.initCboNewBillType() = False Then
                '    Exit Sub
                'End If
                'If Ctrl_BillingInf1.initCboNewMode() = False Then
                '    Exit Sub
                'End If
                'Ctrl_BillingInf1.showBillingInfo()
                'Ctrl_CRSPolicyGeneral_Information1.MQQueuesHeader = Me.objMQQueHeader
                'Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = strPolicyNo.Trim
                'Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()
        End Select
    End Sub



End Class