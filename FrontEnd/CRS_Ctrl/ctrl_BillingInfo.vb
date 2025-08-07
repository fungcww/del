Public Class ctrl_BillingInfo
    Public strPolicyNo As String
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private objMQQueHeader As Utility.Utility.MQHeader      'MQQueHeader includes MQ conn. parameters
    Private objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters
    Private objPOSHeader As Utility.Utility.POSHeader       'POSHeader indicates transaction id and type
    Private objUtility As Utility.Utility                   'For calling Utility object
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS     'For calling clsPOS class

    Private dsCurr As New DataSet

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

    ''' <summary>
    ''' Get or set policy header data externally
    ''' </summary>
    Public Property PolicyHeaderData As DataSet
        Get
            Return dsCurr
        End Get
        Set(value As DataSet)
            dsCurr = value
        End Set
    End Property

    Public Sub showBillingInfo()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            Dim strErr As String = ""
            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            clsPOS.DBHeader = Me.objDBHeader
            'create dataset
            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            Dim strTime As String = ""
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicyNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)

            ' CRS performer slowness - If dsCurr has been assigned externally or has been obtained, no need to perform get logic
            Dim blnGetPolicy As Boolean = True
            If dsCurr Is Nothing OrElse dsCurr.Tables.Count = 0 Then
                dsCurr = New DataSet()
                blnGetPolicy = clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr)
            End If

            FillBillInfo(dsCurr)
            SetLevyColmuns(strPolicyNo, dsCurr.Tables(0).Rows(0)("Next_bill_Amt"))

            'DL20210506 - ITSR2760 display split premium data if product is split premium - start
            ' get plancode to identify display of split premium column by dsCurr.Tables(0).Rows(0)("BasicPlan")
            Dim strIsSplitPremiumProductErr As String = ""
            If clsPOS.IsSplitPremiumProduct(dsCurr.Tables(0).Rows(0)("BasicPlan").ToString, strIsSplitPremiumProductErr) Then
                Me.lblSplitPrmOpt.Visible = True
                Me.lbl2ndPrmDueDate.Visible = True
                Me.lbl2ndPremAmt.Visible = True
                Me.txtSplitPrmOpt.Visible = True
                Me.dtp2ndPrmDueDate.Visible = True
                Me.txt2ndPrmAmt.Visible = True
                Me.lblAdminCharge.Visible = True
                Me.txtAdminCharge.Visible = True

                ' retrieve split premium data
                Dim dsSplitPremData As New DataSet
                Dim strSplitPremErr As String = ""
                If clsPOS.GetSplitPremiumDataByPolicyNo(strPolicyNo, dsSplitPremData, strSplitPremErr) Then
                    If Not dsSplitPremData Is Nothing AndAlso dsSplitPremData.Tables(0).Rows.Count > 0 Then
                        Me.txtSplitPrmOpt.Text = dsSplitPremData.Tables(0).Rows(0)("SplitPremOpt").ToString
                        Me.dtp2ndPrmDueDate.Text = dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString
                        'If dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString.Length = 8 Then
                        '    Me.dtp2ndPrmDueDate.Text = String.Format("{0}-{1}-{2}", dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString.Substring(0, 4), dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString.Substring(3, 2), dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString.Substring(6))
                        'Else
                        '    Me.dtp2ndPrmDueDate.Text = dsSplitPremData.Tables(0).Rows(0)("DueDatePrem02").ToString
                        'End If
                        Dim dbl2ndPremAmt As Double = CDbl(dsSplitPremData.Tables(0).Rows(0)("StdPrem02")) + CDbl(dsSplitPremData.Tables(0).Rows(0)("LoadPrem02"))
                        Me.txt2ndPrmAmt.Text = dbl2ndPremAmt.ToString("0.00")
                        'Me.txt2ndNetPrmAmt.Text = CDbl(dsSplitPremData.Tables(0).Rows(0)("PremTot02")).ToString("0.00")

                        If Not dsSplitPremData.Tables(0).Rows(0)("AdminChargeFlag") Is Nothing AndAlso dsSplitPremData.Tables(0).Rows(0)("AdminChargeFlag").ToString.Equals("Y") Then
                            'Format: 2nd Modal Premium ¡V Discount amount + Admin Charge - Premium Suspense amount
                            Dim dbl2ndNetPrmAmt As Double = CDbl(dsSplitPremData.Tables(0).Rows(0)("StdPrem02")) - CDbl(dsSplitPremData.Tables(0).Rows(0)("PremDisc02")) + CDbl(dsSplitPremData.Tables(0).Rows(0)("AdminChargeAmt")) - CDbl(dsCurr.Tables(0).Rows(0)("PSusp"))
                            Me.txt2ndNetPrmAmt.Text = dbl2ndNetPrmAmt.ToString("0.00")
                            Me.txtAdminCharge.Text = CDbl(dsSplitPremData.Tables(0).Rows(0)("AdminChargeAmt")).ToString("0.00")
                        Else
                            Me.txt2ndNetPrmAmt.Text = CDbl(dsSplitPremData.Tables(0).Rows(0)("PremTot02")).ToString("0.00")
                            Me.txtAdminCharge.Text = "N/A"
                        End If

                        If dsSplitPremData.Tables(0).Rows(0)("SplitPremOpt").ToString.ToUpper.Equals("Y") Then
                            Me.lbl2ndPrmDueDate.Visible = True
                            Me.lbl2ndPremAmt.Visible = True
                            Me.lbl2ndNetPremAmt.Visible = True
                            Me.dtp2ndPrmDueDate.Visible = True
                            Me.txt2ndPrmAmt.Visible = True
                            Me.txt2ndNetPrmAmt.Visible = True
                            Me.lblAdminCharge.Visible = True
                            Me.txtAdminCharge.Visible = True
                        Else
                            Me.lbl2ndPrmDueDate.Visible = False
                            Me.lbl2ndPremAmt.Visible = False
                            Me.lbl2ndNetPremAmt.Visible = False
                            Me.dtp2ndPrmDueDate.Visible = False
                            Me.txt2ndPrmAmt.Visible = False
                            Me.txt2ndNetPrmAmt.Visible = False
                            Me.lblAdminCharge.Visible = False
                            Me.txtAdminCharge.Visible = False
                        End If
                    End If
                End If
            Else
                Me.lblSplitPrmOpt.Visible = False
                Me.lbl2ndPrmDueDate.Visible = False
                Me.lbl2ndPremAmt.Visible = False
                Me.lbl2ndNetPremAmt.Visible = False
                Me.txtSplitPrmOpt.Visible = False
                Me.dtp2ndPrmDueDate.Visible = False
                Me.txt2ndPrmAmt.Visible = False
                Me.txt2ndNetPrmAmt.Visible = False
                Me.lblAdminCharge.Visible = False
                Me.txtAdminCharge.Visible = False
            End If
            'DL20210506 - end

        Catch ignore As Exception
        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_BillingInfo", "showBillingInfo", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
        End Try
    End Sub


    Public Sub FillBillInfo(ByVal dsCurr As DataSet)

        objCI.MQQueuesHeader = Me.objMQQueHeader
        objCI.DBHeader = Me.objDBHeader
        txtBillType.Text = objCI.getPayment_Meth(dsCurr.Tables(0).Rows(0)("Pay_Meth").ToString)
        txtMode.Text = objCI.getPayment_Freq(dsCurr.Tables(0).Rows(0)("Freq").ToString) 'dsCurr.Tables(0).Rows(0)("Freq").ToString
        cboCurrency.Text = dsCurr.Tables(0).Rows(0)("Bill_Ccy").ToString
        cboDrawDay.Text = CDate(dsCurr.Tables(0).Rows(0)("Next_bill_Date").ToString).Day.ToString
        dtpPTD.Text = dsCurr.Tables(0).Rows(0)("Paid_To_Date").ToString
        dtpBTD.Text = dsCurr.Tables(0).Rows(0)("Bill_To_Date").ToString
        dtpNBD.Text = dsCurr.Tables(0).Rows(0)("Next_Bill_Date").ToString
        txtModeP.Text = dsCurr.Tables(0).Rows(0)("Next_bill_Amt").ToString

        dtpRCD.Text = dsCurr.Tables(0).Rows(0)("RCD").ToString
        dtpPID.Text = dsCurr.Tables(0).Rows(0)("Policy_Iss_Date").ToString
        dtpFID.Text = dsCurr.Tables(0).Rows(0)("1st_Iss_Date").ToString
        dtpPPD.Text = dsCurr.Tables(0).Rows(0)("Proposal_Date").ToString
        dtpARD.Text = dsCurr.Tables(0).Rows(0)("Prop_Rec_Date").ToString
        dtpUWDD.Text = dsCurr.Tables(0).Rows(0)("UW_Dec_Date").ToString

        If Me.objMQQueHeader.CompanyID = "HKL" Then
            txtDispPerson.Text = "N/A"
            dtpDispDate.CustomFormat = "__/__/____"
            dtpDispDate.Text = dsCurr.Tables(0).Rows(0)("DispDate").ToString
        Else
            txtDispPerson.Text = dsCurr.Tables(0).Rows(0)("DispPerson").ToString
            dtpDispDate.CustomFormat = "MMM dd, yyyy"
            dtpDispDate.Text = dsCurr.Tables(0).Rows(0)("DispDate").ToString
        End If
        dtpCooloffDate.Text = dsCurr.Tables(0).Rows(0)("CooloffDate").ToString

        txtDividendOption.Text = dsCurr.Tables(0).Rows(0)("DividendOption").ToString
    End Sub

    Public Sub SetLevyColmuns(ByVal strPolicyNo As String, ByVal premiumAmount As Double)
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Dim strErr As String = ""
        Dim levyAmount As Double = 0

        Dim currency As String = cboCurrency.Text
        Dim modePermium As String = txtModeP.Text
        Dim paidToDate As DateTime = CDate(dtpPTD.Text)

        Dim premiumAllocatedAmountDue As String = "0.00"
        Dim premiumAmountDue As String = "0.00"
        Dim levyQuotationAmount As String = "00.0"

        Try
            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            clsPOS.DBHeader = Me.objDBHeader
            clsPOS.CiwHeader = Me.objDBHeader

            ' this levy quotation is different than the policy header one, cannot reuse
            Dim blnGetPolicy As Boolean = False
            '20171017
            'blnGetPolicy = clsPOS.GetLevyAmountOutstanding(strPolicyNo, levyAmount, strErr)
            blnGetPolicy = clsPOS.GetLevyQuotation(strPolicyNo, currency, modePermium, paidToDate, paidToDate, False, "R", "LA",
                                                   premiumAmountDue, premiumAllocatedAmountDue, levyQuotationAmount, strErr)

            txtLevyQuotation.Text = CDbl(levyQuotationAmount).ToString("0.00")

            txtTotalAmount.Text = (CDbl(levyQuotationAmount) + premiumAmount).ToString("0.00")

        Catch ex As Exception

            txtLevyQuotation.Text = "00.0"
            txtTotalAmount.Text = "00.0"

        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_BillingInfo", "SetLevyColmuns", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
        End Try
    End Sub

End Class
