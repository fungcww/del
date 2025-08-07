Public Class ctrl_FundTranSummary

    Private clsCRS As New CRS.CRS    
    Private SysEventLog As New SysEventLog.clsEventLog
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private frmSelCO As New POSMain.frmSelectCO
    Private strPolicyNo, strBasicInsured As String
    Private datPolicyEffDate As DateTime


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
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property

    Public Property BasicInsured() As String
        Get
            Return strBasicInsured
        End Get
        Set(ByVal value As String)
            strBasicInsured = value
        End Set
    End Property

    Private Sub ctrl_FundTranSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FundSummaryLoad()

    End Sub

    Private Sub FundSummaryLoad()

        Dim strErr As String = ""
        Dim iCP As Integer

        Try
            iCP = 1

            Dim bShowAllFund As Boolean = True
            Dim strCurr As String = ""
            Dim dtIAAccount As New DataTable
            Dim dtCoverage As New DataTable

            If frmSelCO.SelectMode = False Then

                If Not GetFundIAAccount("GetPolicyFund", dtIAAccount, strCurr, strErr) Then
                    Throw New Exception(iCP & " - GetFundIAAccount Err: " & strErr)
                Else
                    If Not GetSelCo(strPolicyNo, dtCoverage) Then
                        Throw New Exception("GetSelCo")
                    End If

                    FundSummary1.CoverageDT = dtCoverage
                    FundSummary1.FundIAAccountDT = dtIAAccount
                    FundSummary1.CoverageCurrency = strCurr
                    FundSummary1.ShowAllFund = bShowAllFund

                    iCP = 2
                    FundSummary1.TotalInvestmentPreimumPaid = clsCRS.GetTotalInvestmentPreimumPaid(strPolicyNo, "'CARD','CASH','CHQ','PAC','PPS','TRF','VAL'")

                    iCP = 3
                    FundSummary1.EstimatedSurrenderAmount = clsCRS.GetEstimatedSurrenderAmount(strPolicyNo, DateTime.Today)

                    iCP = 4
                    FundSummary1.LoadFundSummary()

                    frmSelCO.SelectMode = False
                End If
            End If

        Catch ex As Exception

            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message & " " & iCP, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)

        End Try

    End Sub

    Public Function GetSelCo(ByVal strPolicyNo As String, ByRef dtSelCo As DataTable) As Boolean

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS

        Try
            Dim strErr As String = ""
            clsPOS.MQQueuesHeader = Me.objMQQueHeader

            'create dataset
            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable
            Dim dsReceData As New DataSet
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicyNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)

            If clsPOS.GetCOSelRecord(dsSendData, dsReceData, strErr) Then
                If dsReceData.Tables(1).Rows.Count > 0 Then
                    dtSelCo = dsReceData.Tables(1).Copy
                Else
                    Return False
                End If
            Else
                If Trim(strErr) <> "" Then MsgBox(strErr)
                strErr = ""
                Return False
            End If

            Return True

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try


    End Function

    Private Sub btnClearCov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        frmSelCO.SelectMode = False

        Ctrl_Sel_CO_UTRS.currdsInuse = Nothing
        Ctrl_Sel_CO_UTRS.BasicPlan = Nothing
        Ctrl_Sel_CO_UTRS.InitScreen()

        FundSummary1.Life = Nothing
        FundSummary1.Coverage = Nothing
        FundSummary1.Rider = Nothing

        FundSummaryLoad()

    End Sub

    Private Sub btnSelectCov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectCov.Click

        Dim iCP As Integer = 0
        Dim strLife, strCov, strRider, strCovCurr As String
        Dim dblOverallTotalFundValue As Double = 0
        Dim bShowAllFund As Boolean = False
        Dim strErr As String = ""

        Try
            frmSelCO.policyInuse = strPolicyNo
            frmSelCO.MQQueuesHeader = Me.objMQQueHeader
            frmSelCO.ShowDialog()

            If frmSelCO.SelectMode = True Then

                Dim dsSelCO As New DataSet
                dsSelCO = frmSelCO.CurrdsInuse
                Ctrl_Sel_CO_UTRS.currdsInuse = dsSelCO
                Ctrl_Sel_CO_UTRS.BasicPlan = frmSelCO.BasicPlan

                'Show COSel control inform.
                If ShowCOSelData(dsSelCO) = False Then
                    bShowAllFund = True
                    Exit Sub
                Else
                    strCovCurr = dsSelCO.Tables(0).DefaultView(0)("Cov_Cur")
                    strLife = dsSelCO.Tables(1).DefaultView(0)("Life")
                    strCov = dsSelCO.Tables(1).DefaultView(0)("Cov")
                    strRider = dsSelCO.Tables(1).DefaultView(0)("Rider")
                    bShowAllFund = False

                End If

                Dim dtIAAccount As New DataTable

                iCP = 1

                If Not GetFundIAAccount("GetPolicyFund", dtIAAccount, strCovCurr, strErr) Then
                    Throw New Exception(iCP & " - GetFundIAAccount Err: " & strErr)
                Else
                    FundSummary1.CoverageDT = dsSelCO.Tables(1)
                    FundSummary1.FundIAAccountDT = dtIAAccount
                    FundSummary1.CoverageCurrency = strCovCurr
                    FundSummary1.Life = strLife
                    FundSummary1.Coverage = strCov
                    FundSummary1.Rider = strRider
                    FundSummary1.ShowAllFund = bShowAllFund

                    iCP = 2
                    FundSummary1.TotalInvestmentPreimumPaid = clsCRS.GetTotalInvestmentPreimumPaid(strPolicyNo, "'CARD','CASH','CHQ','PAC','PPS','TRF','VAL'")

                    iCP = 3
                    FundSummary1.EstimatedSurrenderAmount = clsCRS.GetEstimatedSurrenderAmount(strPolicyNo, DateTime.Today)

                    iCP = 4
                    FundSummary1.LoadFundSummary()

                End If

            End If

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message & " " & iCP, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function ShowCOSelData(ByVal currds As DataSet) As Boolean
        Try
            'Show COSel control inform.
            Ctrl_Sel_CO_UTRS.PolicyNoInuse = strPolicyNo
            Ctrl_Sel_CO_UTRS.LifeNoInUse = currds.Tables(1).DefaultView(0)("Life")
            Ctrl_Sel_CO_UTRS.ClientNoInuse = currds.Tables(1).DefaultView(0)("ClientNo")
            Ctrl_Sel_CO_UTRS.ClientNameInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO_UTRS.RiderInuse = currds.Tables(1).DefaultView(0)("Rider")
            Ctrl_Sel_CO_UTRS.CovCodeInuse = currds.Tables(1).DefaultView(0)("Cov_Code")
            Ctrl_Sel_CO_UTRS.CovDescInuse = currds.Tables(1).DefaultView(0)("Cov_Desc")
            Ctrl_Sel_CO_UTRS.CovNoInuse = currds.Tables(1).DefaultView(0)("Cov")
            Ctrl_Sel_CO_UTRS.RiskStsInuse = currds.Tables(1).DefaultView(0)("Risk_Sts")
            Ctrl_Sel_CO_UTRS.PStsInuse = currds.Tables(1).DefaultView(0)("Prem_Sts")
            Ctrl_Sel_CO_UTRS.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_Sel_CO_UTRS.ShowCORcd()
            Return True
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Function

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

    Public Function GetFundIAAccount(ByVal strDataName As String, ByRef dtFundIA As DataTable, ByRef strCurr As String, ByRef strErr As String) As Boolean
        Dim blnIsValid As Boolean = False

        Dim dsPoInfo As DataSet = New DataSet
        Try

            clsCRS.MQQueuesHeader = Me.MQQueuesHeader
            clsCRS.DBHeader = Me.DBHeader

            blnIsValid = clsCRS.GetPoInfo(strPolicyNo, dsPoInfo, strErr)


            If Not dsPoInfo Is Nothing Then
                If dsPoInfo.Tables.Count > 0 Then
                    If dsPoInfo.Tables(0).Rows.Count > 0 Then
                        Dim drPoInfo As DataRow = dsPoInfo.Tables(0).Rows(0)
                        'Dim strPolCurr As String = drPoInfo("PolicyCurrency")
                        If Not IsNothing(drPoInfo("policyeffdate")) Then
                            datPolicyEffDate = drPoInfo("policyeffdate")

                        End If
                        Dim dsFundIA As New DataSet
                        Dim strIAProd As String = drPoInfo("ProductID")
                        ' Potential problem with the code below as the ProductID should come from a soft-code table.
                        If strIAProd.Substring(0, 2) = "IW" Or strIAProd.Substring(0, 2) = "VI" Or strIAProd.Substring(0, 2) = "WI" Or strIAProd.Substring(0, 2) = "VH" Or strIAProd.Substring(0, 2) = "VU" _
                                Or strIAProd.Substring(0, 2) = "HU" Or strIAProd.Substring(0, 2) = "UU" Or strIAProd.Substring(0, 2) = "XH" Or strIAProd.Substring(0, 2) = "XU" _
                                Or strIAProd.Substring(0, 2) = "KU" Then

                            blnIsValid = clsCRS.GetIAAccount(strPolicyNo, datPolicyEffDate, dsFundIA, strCurr, strErr)

                            If (blnIsValid) Then
                                If (dsFundIA IsNot Nothing AndAlso _
                                   dsFundIA.Tables.Count > 0) Then
                                    dtFundIA = dsFundIA.Tables(0)
                                    dtFundIA.DefaultView.Sort = "cswiab_coverage, cswiab_seq DESC, cswiab_fund_code"
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return blnIsValid
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try

    End Function

End Class