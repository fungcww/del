
Public Class ctrl_FundHolding
    Private SysEventLog As New SysEventLog.clsEventLog
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private frmSelCO As New POSMain.frmSelectCO
    Dim dsHeader As New DataSet
    Dim dsDetail As New DataSet
    Private sPolicy As String
    'Private objCI As New LifeClientInterfaceComponent.clsPOS      'For calling clsPOS class in LifeClientInterfaceComponent

    Dim dsPolicyHead As New DataSet

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
    Public Property PolicyinUse() As String
        Get
            Return sPolicy
        End Get
        Set(ByVal value As String)
            sPolicy = value
        End Set
    End Property
    Private Function getFundHold(ByRef dsMain As DataSet, ByVal sPolicy As String, ByVal LifeNo As String, ByVal CovNo As String, ByVal RiderNo As String) As Boolean
        Dim objPOS As New LifeClientInterfaceComponent.clsPOS
        Dim dsSendData As New DataSet
        Dim dtSendData As New DataTable
        Dim dsReceData As New DataSet
        Dim dsReceData1 As New DataSet
        Dim strErr As String = ""
        Dim dr As DataRow

        Dim strFundCode As String = ""
        Dim strFromCurrency As String = ""      'Fund Currency
        Dim strToCurrency As String = ""        'Policy Currency
        Dim dblExRate As Double = 0
        Dim strUnitPriceDate As String = ""
        Dim dblUnitPriceAmt As Double = 0
        Dim dblAmtInFundCurr As Double = 0
        Dim dblAmtInPolCurr As Double = 0
        Dim dblUnitBal As Double = 0

        Dim dblAmtInPolCurr_Total As Double = 0

        'dtSendData.Columns.Clear()
        'dtSendData.Columns.Add("PolicyNo")
        'dtSendData.Rows.Add()
        'dtSendData.Rows(0)("PolicyNo") = sPolicy

        'dsSendData.Tables.Clear()
        'dsSendData.Tables.Add(dtSendData)
        'objPOS.MQQueuesHeader = Me.MQQueuesHeader
        'objPOS.DBHeader = Me.DBHeader
        'If objPOS.GetCOSelWithCustNo(dsSendData, dsReceData, strErr) Then

        '    For i As Integer = 0 To dsReceData.Tables(1).Rows.Count - 1
        '        If dsReceData.Tables(1).Rows(i)("Cov").ToString.Trim <> "" And dsReceData.Tables(1).Rows(i)("life").ToString.Trim <> "" And _
        '        dsReceData.Tables(1).Rows(i)("Rider").ToString.Trim <> "" And dsReceData.Tables(1).Rows(i)("Cov_code").ToString.Trim <> "" Then
        '            dr = dsMain.Tables("dttCompDtl").NewRow
        '            dr("Life_No") = dsReceData.Tables(1).Rows(i)("Life")
        '            dr("coverage_no") = dsReceData.Tables(1).Rows(i)("Cov")
        '            dr("rider_no") = dsReceData.Tables(1).Rows(i)("Rider")
        '            dr("plan_cd") = dsReceData.Tables(1).Rows(i)("Cov_code")
        '            dsMain.Tables("dttCompDtl").Rows.Add(dr)
        '        End If
        '    Next
        'End If


        objPOS.MQQueuesHeader = Me.MQQueuesHeader
        objPOS.DBHeader = Me.DBHeader

        Dim dsFundList As New Data.DataSet

        'get fund currency from reporting module
        If Not objPOS.GetAvailableFundList(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), dsPolicyHead.Tables(0).Rows(0)("Curr"), dsFundList, strErr) Then
            If strErr.Trim = "" Then
                MsgBox("Cannot get fund list table!", MsgBoxStyle.Critical)
            Else
                MsgBox(strErr, MsgBoxStyle.Critical)
            End If
            strErr = ""
            Exit Function
        Else
            If dsFundList.Tables.Count < 2 Then
                MsgBox("Incorrect fund list table", MsgBoxStyle.Critical)
                Exit Function
            End If
        End If

        dtSendData.Columns.Clear()
        dtSendData.Columns.Add("PolicyNo")
        dtSendData.Columns.Add("Coverage")
        dtSendData.Columns.Add("Rider")
        dtSendData.Columns.Add("LifeNo")
        dtSendData.Columns.Add("From_Date")
        dtSendData.Columns.Add("To_Date")
        dtSendData.Columns.Add("ManRef")

        dtSendData.Rows.Add()
        dtSendData.Rows(0)("PolicyNo") = sPolicy
        dtSendData.Rows(0)("Coverage") = "01"
        dtSendData.Rows(0)("Rider") = "00"
        dtSendData.Rows(0)("From_Date") = Format(dtpFrDate.Value, "yyyy/MM/dd") '"2007/01/01" '"2007/09/25"
        dtSendData.Rows(0)("To_Date") = Format(dtpToDate.Value, "yyyy/MM/dd") '"2008/01/01" '"2007/10/27"
        dtSendData.Rows(0)("ManRef") = "00001"

        dsSendData.Tables.Clear()
        dsSendData.Tables.Add(dtSendData)

        dsReceData1.Tables.Clear()
        'For i As Integer = 0 To dsMain.Tables("dttCompDtl").Rows.Count - 1

        dtSendData.Rows(0)("LifeNo") = LifeNo 'Format(dsMain.Tables("dttCompDtl").Rows(i)("Life_No"), "00")
        dtSendData.Rows(0)("Coverage") = CovNo 'Format(dsMain.Tables("dttCompDtl").Rows(i)("coverage_no"), "00")
        dtSendData.Rows(0)("Rider") = RiderNo 'Format(dsMain.Tables("dttCompDtl").Rows(i)("rider_no"), "00")

        If objPOS.GetFundHolding(dsSendData, dsReceData1, strErr) Then 'BO not ready
            If dsReceData1.Tables.Count > 0 Then
                If dsReceData1.Tables("Header").Rows.Count > 0 Then
                    For j As Integer = 0 To dsReceData1.Tables("Header").Rows.Count - 1
                        dr = dsMain.Tables("dttCompFunHold").NewRow

                        dr("company_cd") = "  "
                        dr("policy_no") = sPolicy
                        dr("coverage_no") = CovNo 'dsMain.Tables("dttCompDtl").Rows(i)("coverage_no")
                        dr("life_no") = LifeNo 'dsMain.Tables("dttCompDtl").Rows(i)("life_no")
                        dr("virtual_fund") = dsReceData1.Tables("Header").Rows(j)("Fund")
                        dr("fund_type") = dsReceData1.Tables("Header").Rows(j)("Fund_Type")
                        dr("Unit Balance") = dsReceData1.Tables("Header").Rows(j)("Unit_Balance")
                        dr("Deem Unit Balance") = dsReceData1.Tables("Header").Rows(j)("Deemed_Unit_Bal")
                        dr("Ref") = dsReceData1.Tables("Header").Rows(j)("RefNo")
                        dr("rider_no") = RiderNo 'dsMain.Tables("dttCompDtl").Rows(i)("rider_no") 'ITSR933 FG R5.5 JIRA Gary Lei

                        strFundCode = dsReceData1.Tables("Header").Rows(j)("Fund").ToString.Trim
                        dblUnitBal = dsReceData1.Tables("Header").Rows(j)("Deemed_Unit_Bal")

                        dsFundList.Tables(0).DefaultView.RowFilter = "mpfinv_code = '" & strFundCode & "'"
                        If dsFundList.Tables(0).DefaultView.Count > 0 Then
                            strFromCurrency = dsFundList.Tables(0).DefaultView(0).Item("mpfinv_curr").ToString.Trim
                            strUnitPriceDate = dsFundList.Tables(0).DefaultView(0).Item("UnitPriceDate")
                            dblUnitPriceAmt = dsFundList.Tables(0).DefaultView(0).Item("UnitPriceAmt")

                            dblAmtInFundCurr = dblUnitPriceAmt * dblUnitBal

                            dsFundList.Tables(1).DefaultView.RowFilter = "from_ccy = '" & strFromCurrency & "'"
                            If dsFundList.Tables(0).DefaultView.Count > 0 Then
                                dblExRate = Val(dsFundList.Tables(1).DefaultView(0).Item("buy_rate"))
                                dblAmtInPolCurr = dblAmtInFundCurr * dblExRate
                            Else
                                MsgBox("No exchange rate for currency " & strFromCurrency)
                                Exit Function
                            End If
                        Else
                            MsgBox("Fund " & strFundCode & " not found in RM")
                            Exit Function
                        End If

                        dr("UnitPriceDate") = strUnitPriceDate
                        dr("FundCurr") = strFromCurrency
                        dr("UnitPriceAmt") = dblUnitPriceAmt
                        dr("AmtInFundCurr") = Format(dblAmtInFundCurr, "0.00")
                        dr("AmtInPolCurr") = Format(dblAmtInPolCurr, "0.00")
                        dr("ExRate") = Format(dblExRate, "0.00000000")

                        dblAmtInPolCurr_Total = dblAmtInPolCurr_Total + dr("AmtInPolCurr")

                        dsMain.Tables("dttCompFunHold").Rows.Add(dr)
                    Next

                    'add summary amount
                    dr = dsMain.Tables("dttCompFunHold").NewRow

                    dr("company_cd") = "  "
                    dr("policy_no") = sPolicy
                    dr("coverage_no") = CovNo 'dsMain.Tables("dttCompDtl").Rows(i)("coverage_no")
                    dr("life_no") = LifeNo 'dsMain.Tables("dttCompDtl").Rows(i)("life_no")
                    dr("virtual_fund") = "TOT"
                    dr("fund_type") = ""
                    dr("Unit Balance") = 0
                    dr("Deem Unit Balance") = 0
                    dr("Ref") = ""

                    dr("UnitPriceDate") = ""
                    dr("FundCurr") = ""
                    dr("UnitPriceAmt") = 0
                    dr("AmtInFundCurr") = 0
                    dr("AmtInPolCurr") = dblAmtInPolCurr_Total
                    dr("ExRate") = 0
                    dr("rider_no") = RiderNo 'dsMain.Tables("dttCompDtl").Rows(i)("rider_no") 'ITSR933 FG R5.5 JIRA Gary Lei

                    dsMain.Tables("dttCompFunHold").Rows.Add(dr)

                End If
                If dsReceData1.Tables("Detail").Rows.Count > 0 Then
                    For j As Integer = 0 To dsReceData1.Tables("Detail").Rows.Count - 1
                        dr = dsMain.Tables("dttCompFunHoldDtl").NewRow

                        dr("company_cd") = "  "
                        dr("policy_no") = sPolicy
                        dr("coverage_no") = CovNo 'dsMain.Tables("dttCompDtl").Rows(i)("coverage_no")
                        dr("life_no") = LifeNo 'dsMain.Tables("dttCompDtl").Rows(i)("life_no")
                        dr("virtual_fund") = ""
                        dr("Real_unit") = dsReceData1.Tables("Detail").Rows(j)("Real_Units")
                        dr("Deem unit") = dsReceData1.Tables("Detail").Rows(j)("Deemed_Units")
                        dr("Monies Date") = dsReceData1.Tables("Detail").Rows(j)("Monies_Dat")
                        dr("Fund Amount") = dsReceData1.Tables("Detail").Rows(j)("Fund_Amt")
                        dr("PriceType") = dsReceData1.Tables("Detail").Rows(j)("Now_Def")
                        dr("Completed") = dsReceData1.Tables("Detail").Rows(j)("Complete")
                        dr("Ref") = dsReceData1.Tables("Detail").Rows(j)("RefNo1")

                        dr("Price_Used") = dsReceData1.Tables("Detail").Rows(j)("Price_Used")
                        dr("Price_Date") = dsReceData1.Tables("Detail").Rows(j)("Price_Date")
                        dr("Fund_Amt_POCurr") = dsReceData1.Tables("Detail").Rows(j)("Contract_Amt")
                        dr("Fund_Curr") = dsReceData1.Tables("Detail").Rows(j)("FundCurr")
                        dr("Fund_Rate") = dsReceData1.Tables("Detail").Rows(j)("Fund_Rate")
                        dr("Trans_Desc") = dsReceData1.Tables("Detail").Rows(j)("GLCode")
                        dr("Surr_Perc") = dsReceData1.Tables("Detail").Rows(j)("Surr_Perc")
                        dr("Unit_type") = dsReceData1.Tables("Detail").Rows(j)("Unit_Type")
                        dr("Trans_Code") = dsReceData1.Tables("Detail").Rows(j)("TransCode")
                        dr("Switch_Ind") = dsReceData1.Tables("Detail").Rows(j)("Switch_Ind")
                        dr("rider_no") = RiderNo 'dsMain.Tables("dttCompDtl").Rows(i)("rider_no") 'ITSR933 FG R5.5 JIRA Gary Lei

                        dsMain.Tables("dttCompFunHoldDtl").Rows.Add(dr)
                    Next
                Else
                End If
            End If
        Else
            MsgBox(strErr, MsgBoxStyle.Information)
        End If
        'Next
    End Function


    Public Sub ShowPolicyRecord()
        'Dim dsMain As New DataSet
        Ctrl_POS_Scrn_Head.policyInuse = sPolicy 'txtPolicyNo.Text
        Ctrl_POS_Scrn_Head.MQQueuesHeader = Me.objMQQueHeader
        Ctrl_POS_Scrn_Head.dbHeader = Me.objDBHeader
        Ctrl_POS_Scrn_Head.ShowPolicyRcd()

        'Call createdttCompDtl(dsMain)
        'Call createdttCompFunHold(dsMain)
        'Call createdttCompFunHoldDtl(dsMain)

        'getFundHold(dsMain, txtPolicyNo.Text)
        'If dsHeader.Tables.Count > 0 Then
        '    dsHeader.Tables.Clear()
        'End If

        'If dsDetail.Tables.Count > 0 Then
        '    dsDetail.Tables.Clear()
        'End If
        'dsHeader.Tables.Add(dsMain.Tables("dttCompFunHold").Copy)
        'dsDetail.Tables.Add(dsMain.Tables("dttCompFunHoldDtl").Copy)
    End Sub
    Private Sub createdttCompFunHold(ByRef dsMain As DataSet)
        'Create component Fund Holding table
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("dttCompFunHold")

            If Not blnmain Then
                dsMain.Tables.Add("dttCompFunHold")
                With dsMain.Tables("dttCompFunHold")
                    .Columns.Add("company_cd", GetType(System.String))
                    .Columns("company_cd").MaxLength = 3
                    .Columns.Add("policy_no", GetType(System.String))
                    .Columns("policy_no").MaxLength = 10
                    .Columns.Add("coverage_no", GetType(System.Int16))
                    .Columns.Add("life_no", GetType(System.Int16))
                    .Columns.Add("virtual_fund", GetType(System.String))
                    .Columns("virtual_fund").MaxLength = 4
                    .Columns.Add("fund_type", GetType(System.String))
                    .Columns("fund_type").MaxLength = 1
                    .Columns.Add("Unit Balance", GetType(System.Decimal))
                    .Columns.Add("Deem Unit Balance", GetType(System.Decimal))
                    .Columns.Add("Ref", GetType(System.String))

                    .Columns.Add("UnitPriceDate", GetType(System.String))
                    .Columns.Add("FundCurr", GetType(System.String))
                    .Columns.Add("UnitPriceAmt", GetType(System.Decimal))
                    .Columns.Add("AmtInFundCurr", GetType(System.Decimal))
                    .Columns.Add("AmtInPolCurr", GetType(System.Decimal))
                    .Columns.Add("ExRate", GetType(System.Decimal))
                    .Columns.Add("rider_no", GetType(System.Int16)) 'ITSR933 FG R5.5 JIRA Gary Lei
                End With
            Else
                dsMain.Tables("dttCompFunHold").Rows.Clear()
            End If
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub createdttCompFunHoldDtl(ByRef dsMain As DataSet)
        'Create fund holding detail table
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("dttCompFunHoldDtl")

            If Not blnmain Then
                dsMain.Tables.Add("dttCompFunHoldDtl")
                With dsMain.Tables("dttCompFunHoldDtl")
                    .Columns.Add("company_cd", GetType(System.String))
                    .Columns("company_cd").MaxLength = 3
                    .Columns.Add("policy_no", GetType(System.String))
                    .Columns("policy_no").MaxLength = 10
                    .Columns.Add("life_no", GetType(System.Int16))
                    .Columns.Add("coverage_no", GetType(System.Int16))
                    .Columns.Add("virtual_fund", GetType(System.String))
                    .Columns("virtual_fund").MaxLength = 4
                    .Columns.Add("Real_unit", GetType(System.Decimal))
                    .Columns.Add("Deem unit", GetType(System.Decimal))
                    .Columns.Add("Monies Date", GetType(System.DateTime))
                    .Columns.Add("Fund Amount", GetType(System.Decimal))
                    .Columns.Add("PriceType", GetType(System.String))
                    .Columns("PriceType").MaxLength = 1
                    .Columns.Add("Completed", GetType(System.String))
                    .Columns("Completed").MaxLength = 1
                    .Columns.Add("Ref", GetType(System.String))

                    .Columns.Add("Price_Used", GetType(System.Decimal))
                    .Columns.Add("Price_Date", GetType(System.DateTime))
                    .Columns.Add("Fund_Amt_POCurr", GetType(System.Decimal))
                    .Columns.Add("Fund_Curr", GetType(System.String))
                    .Columns.Add("Fund_Rate", GetType(System.Decimal))
                    .Columns.Add("Trans_Desc", GetType(System.String))
                    .Columns.Add("Surr_Perc", GetType(System.Decimal))
                    .Columns.Add("Unit_type", GetType(System.String))
                    .Columns.Add("Trans_Code", GetType(System.String))
                    .Columns.Add("Switch_Ind", GetType(System.String))
                    .Columns.Add("rider_no", GetType(System.Int16)) 'ITSR933 FG R5.5 JIRA Gary Lei


                End With
            Else
                dsMain.Tables("dttCompFunHoldDtl").Rows.Clear()
            End If
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        'ITSR933 FG R5.5 JIRA Gary Lei Start
        Dim isHasFundRider As Boolean = False
        Dim objPOS As New LifeClientInterfaceComponent.clsPOS
        objPOS.MQQueuesHeader = Me.MQQueuesHeader
        objPOS.DBHeader = Me.DBHeader
        Dim strErr1 As String = ""
        If objPOS.HasFundRider(sPolicy, strErr1) Then
            frmSelCO.IsCanSelectRiderMode = True
            isHasFundRider = True
        End If
        'ITSR933 FG R5.5 JIRA Gary Lei End

        frmSelCO.policyInuse = sPolicy 'txtPolicyNo.Text.Trim
        frmSelCO.MQQueuesHeader = Me.objMQQueHeader
        frmSelCO.ShowDialog()

        If frmSelCO.SelectMode = True Then

            Dim dsSelCO As New DataSet
            dsSelCO = frmSelCO.CurrdsInuse
            Ctrl_Sel_CO.currdsInuse = dsSelCO
            Ctrl_Sel_CO.BasicPlan = frmSelCO.BasicPlan

            'Show COSel control inform.
            If ShowCOSelData(dsSelCO) = False Then
                Exit Sub
            End If

            'ITSR933 FG R5.5 JIRA Gary Lei Start
            If isHasFundRider AndAlso dsSelCO.Tables(1).DefaultView(0)("Cov").ToString.Trim = "" Then
                MsgBox("Invalid selection!!", MsgBoxStyle.Information)
                Exit Sub
            End If
            'ITSR933 FG R5.5 JIRA Gary Lei End

            Dim dsMain As New DataSet

            Call createdttCompFunHold(dsMain)
            Call createdttCompFunHoldDtl(dsMain)

            getFundHold(dsMain, sPolicy, dsSelCO.Tables(1).DefaultView(0)("Life"), dsSelCO.Tables(1).DefaultView(0)("Cov"), dsSelCO.Tables(1).DefaultView(0)("Rider"))
            Dim dsHeader As New DataSet
            Dim dsDetail As New DataSet
            dsHeader.Tables.Add(dsMain.Tables("dttCompFunHold").Copy)
            dsDetail.Tables.Add(dsMain.Tables("dttCompFunHoldDtl").Copy)


            FundHoldCom1.HeaderDS = dsHeader
            FundHoldCom1.DetailDS = dsDetail
            FundHoldCom1.lifenoinuse = dsSelCO.Tables(1).DefaultView(0)("Life")
            FundHoldCom1.coveragenoinuse = dsSelCO.Tables(1).DefaultView(0)("Cov")
            FundHoldCom1.modeinuse = Utility.Utility.ModeName.Blank


        End If


    End Sub

    Private Function ShowCOSelData(ByVal currds As DataSet) As Boolean
        Try
            'Show COSel control inform.
            Ctrl_Sel_CO.PolicyNoInuse = sPolicy 'txtPolicyNo.Text
            Ctrl_Sel_CO.LifeNoInUse = currds.Tables(1).DefaultView(0)("Life")
            Ctrl_Sel_CO.ClientNoInuse = currds.Tables(1).DefaultView(0)("ClientNo")
            Ctrl_Sel_CO.ClientNameInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO.RiderInuse = currds.Tables(1).DefaultView(0)("Rider")
            Ctrl_Sel_CO.CovCodeInuse = currds.Tables(1).DefaultView(0)("Cov_Code")
            Ctrl_Sel_CO.CovDescInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO.CovNoInuse = currds.Tables(1).DefaultView(0)("Cov")
            Ctrl_Sel_CO.RiskStsInuse = currds.Tables(1).DefaultView(0)("Risk_Sts")
            Ctrl_Sel_CO.PStsInuse = currds.Tables(1).DefaultView(0)("Prem_Sts")
            Ctrl_Sel_CO.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_Sel_CO.ShowCORcd()
            Return True
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub ctrl_FundHolding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpFrDate.Text = DateAdd(DateInterval.Month, -6, Now)
        dtpToDate.Text = DateAdd(DateInterval.Day, 1, Now)
        Try
            If sPolicy = "" Then 'txtPolicyNo.Text.Trim = "" Then
            Else
                lblPolicy.Text = sPolicy
                ShowPolicyRecord()
                dsPolicyHead = Ctrl_POS_Scrn_Head.currdsInuse
            End If


        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
