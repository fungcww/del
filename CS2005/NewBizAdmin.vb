'********************************************************************
' Admended By: Flora Leung
' Admended Function: getWSNBWrapper
'                    ShowPolicyRecord
' Added Function: getNBRRecord
' Date: 10 Jan 2012
' Project: Project Leo Goal 3
'********************************************************************

Public Class frmNewBizAdmin

    Private dsMainInfo As New Data.DataSet
    Private dtBasicPlan As New Data.DataTable
    Private dtFundName As New Data.DataTable
    Private dtPaymentMethod As New Data.DataTable
    'Private dtFundAlloc As New Data.DataTable
    Private InternalDSCompD As DataSet
    Private InternalDSFundAlloc As DataSet

    Private objDBHeader As Utility.Utility.ComHeader
    'Protected dbCiw As DBLogon_NET.DBLogon.DBlogonNet
    Private env As String = "UAT"
    Private Company As String
    Private strPolicy As String
    Private strUserID As String

    Public WriteOnly Property PolicyNo(ByVal Comp As String, ByVal Environment As String, ByVal UserID As String) As String
        Set(ByVal value As String)
            env = Environment
            Company = Comp
            strPolicy = value
            strUserID = UserID
            txtPolicyNo.Text = strPolicy.Trim
            ShowPolicyRecord(txtPolicyNo.Text)
        End Set
    End Property

    'Private Sub txtPolicyNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNo.LostFocus
    '    If txtPolicyNo.Text.Trim = "" Then
    '        Exit Sub
    '    End If
    '    txtPolicyNo.Text = txtPolicyNo.Text.ToString.Trim.ToUpper
    '    ShowPolicyRecord(txtPolicyNo.Text.ToString.Trim.ToUpper)
    'End Sub

    Private Sub createDtCompDtl(ByRef dsMain As DataSet)
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("CompDtl")

            If Not blnmain Then
                dsMain.Tables.Add("CompDtl")
                With dsMain.Tables("CompDtl")
                    .Columns.Add("CovLifeNo", GetType(System.String))
                    .Columns.Add("CovNo", GetType(System.String))
                    .Columns.Add("CovRiderno", GetType(System.String))
                    .Columns.Add("LifeClientNo", GetType(System.String))
                    .Columns.Add("PlanCode", GetType(System.String))
                    .Columns.Add("PlanName", GetType(System.String))
                    .Columns.Add("CovMortCls", GetType(System.String))
                    .Columns.Add("CovPremCessAge", GetType(System.String))
                    '.Columns.Add("CovPremCessDate", GetType(System.String))
                    '.Columns.Add("CovMaturityAge", GetType(System.String))
                    .Columns.Add("CovSumInsured", GetType(System.String))

                    ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                    .Columns.Add("CovRiskCessAge", GetType(System.String))
                    .Columns.Add("CovRiskCessDate", GetType(System.String))
                    .Columns.Add("CovPremCessDate", GetType(System.String))
                    .Columns.Add("CovLoadPrem", GetType(System.String))
                    .Columns.Add("CovTotPremium", GetType(System.String))
                    ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

                End With
            Else
                dsMain.Tables("CompDtl").Rows.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub createDtContDtl(ByRef dsMain As DataSet)
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("ContDtl")

            If Not blnmain Then
                dsMain.Tables.Add("ContDtl")
                With dsMain.Tables("ContDtl")
                    .Columns.Add("Comm_Agent1", GetType(System.String))
                    .Columns.Add("Comm_Share1", GetType(System.String))
                    .Columns.Add("Comm_Agent2", GetType(System.String))
                    .Columns.Add("Comm_Share2", GetType(System.String))
                    .Columns.Add("Comm_Agent3", GetType(System.String))
                    .Columns.Add("Comm_Share3", GetType(System.String))
                End With
            Else
                dsMain.Tables("ContDtl").Rows.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub createDtCompFund(ByRef dsMain As DataSet)
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("CompFund")

            If Not blnmain Then
                dsMain.Tables.Add("CompFund")
                With dsMain.Tables("CompFund")
                    .Columns.Add("FundLifeNo", GetType(System.String))
                    .Columns.Add("FundCovNo", GetType(System.String))
                    .Columns.Add("FundRiderNo", GetType(System.String))
                    .Columns.Add("FundCode1", GetType(System.String))
                    .Columns.Add("FundName1", GetType(System.String))
                    .Columns.Add("FundShare1", GetType(System.String))
                    .Columns.Add("FundCode2", GetType(System.String))
                    .Columns.Add("FundName2", GetType(System.String))
                    .Columns.Add("FundShare2", GetType(System.String))
                    .Columns.Add("FundCode3", GetType(System.String))
                    .Columns.Add("FundName3", GetType(System.String))
                    .Columns.Add("FundShare3", GetType(System.String))
                    .Columns.Add("FundCode4", GetType(System.String))
                    .Columns.Add("FundName4", GetType(System.String))
                    .Columns.Add("FundShare4", GetType(System.String))
                    .Columns.Add("FundCode5", GetType(System.String))
                    .Columns.Add("FundName5", GetType(System.String))
                    .Columns.Add("FundShare5", GetType(System.String))
                    .Columns.Add("FundCode6", GetType(System.String))
                    .Columns.Add("FundName6", GetType(System.String))
                    .Columns.Add("FundShare6", GetType(System.String))
                    .Columns.Add("FundCode7", GetType(System.String))
                    .Columns.Add("FundName7", GetType(System.String))
                    .Columns.Add("FundShare7", GetType(System.String))
                    .Columns.Add("FundCode8", GetType(System.String))
                    .Columns.Add("FundName8", GetType(System.String))
                    .Columns.Add("FundShare8", GetType(System.String))
                    .Columns.Add("FundCode9", GetType(System.String))
                    .Columns.Add("FundName9", GetType(System.String))
                    .Columns.Add("FundShare9", GetType(System.String))
                    .Columns.Add("FundCode10", GetType(System.String))
                    .Columns.Add("FundName10", GetType(System.String))
                    .Columns.Add("FundShare10", GetType(System.String))
                End With
            Else
                dsMain.Tables("CompFund").Rows.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub createDtLifeDtl(ByRef dsMain As DataSet)
        Try
            Dim blnmain As Boolean = dsMain.Tables.Contains("LifeDtl")

            If Not blnmain Then
                dsMain.Tables.Add("LifeDtl")
                With dsMain.Tables("LifeDtl")
                    .Columns.Add("LifeNo", GetType(System.String))
                    .Columns.Add("LifeClientNo", GetType(System.String))
                End With
            Else
                dsMain.Tables("LifeDtl").Rows.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub ConnectCiw()
    '    If dbCiw Is Nothing Then
    '        dbCiw = New DBLogon_NET.DBLogon.DBlogonNet()
    '    End If
    '    dbCiw.Project = objDBHeader.ProjectAlias
    '    dbCiw.User = objDBHeader.UserType
    '    dbCiw.ConnectionAlias = objDBHeader.CompanyID & "CIW" & objDBHeader.EnvironmentUse
    '    If Not dbCiw.IsConnected Then
    '        If Not dbCiw.Connect() Then
    '            Throw New Exception(dbCiw.RecentErrorMessage)
    '        End If
    '    End If
    'End Sub

    'Protected Sub DisconnectCiw()
    '    If dbCiw IsNot Nothing AndAlso dbCiw.IsConnected Then
    '        dbCiw.Disconnect()
    '    End If
    'End Sub

    Private Function getDtBasicPlan() As DataTable
        Dim dt As New DataTable
        Dim strSQL As String = "select ProductID, Description from Product "
        Dim strError As String

        Try
            If GetDT(strSQL, strCIWConn, dt, strError) Then
                Return dt
            End If
            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If
        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try
    End Function

    Private Function getDtFundName() As DataTable
        Dim dt As New DataTable
        Dim strSQL As String = "select mpfinv_code, mpfinv_chi_desc from cswvw_mpf_investment "
        Dim strError As String

        Try
            If GetDT(strSQL, strCIWConn, dt, strError) Then
                Return dt
            End If

            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If

            'Return dt
        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try
    End Function

    Private Function getPaymentMethod(ByVal methodCode As String) As String
        Dim dt As New DataTable
        Dim strMethod As String = ""
        Dim strSQL As String = "select Item, Long_desc from " & gcPOS & "vw_csw_Payment_Method WHERE Item = '" & methodCode.Trim & "'"
        Dim strError As String

        Try
            If GetDT(strSQL, strCIWConn, dt, strError) Then
                If dt.Rows.Count > 0 Then
                    strMethod = dt.Rows(0).Item("Long_desc").ToString.Trim
                End If

                Return strMethod
            End If

            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If

        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try
    End Function

    Private Function getAgentName(ByVal agentCode As String) As String
        Dim dt As New DataTable
        Dim agentName As String = ""
        Dim strSQL As String = "select NameSuffix, FirstName from Customer where AgentCode = '" & agentCode.Trim & "'"
        Dim strError As String

        Try

            If GetDT(strSQL, strCIWConn, dt, strError) Then
                If dt.Rows.Count > 0 Then
                    agentName = dt.Rows(0).Item("NameSuffix").ToString.Trim & " " & dt.Rows(0).Item("FirstName").ToString.Trim
                End If

                Return agentName
            End If

            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If

        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try
    End Function

    Private Function getInsuredName(ByVal custID As String) As String
        Dim dt As New DataTable
        Dim custName As String = ""
        Dim strSQL As String = "select NameSuffix, FirstName from Customer where customerid = '" & custID.Trim & "'"
        Dim strError As String

        Try
            If GetDT(strSQL, strCIWConn, dt, strError) Then
                If dt.Rows.Count > 0 Then
                    custName = dt.Rows(0).Item("NameSuffix").ToString.Trim & " " & dt.Rows(0).Item("FirstName").ToString.Trim
                End If

                Return custName
            End If

            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If

        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try
    End Function

    Private Function getPolicyRelation(ByVal PolicyNo As String) As DataTable

        Dim dt As New DataTable
        Dim strSQL As String
        Dim strError As String

        strSQL = "Select c.customerid as [Customer ID], policyaccountrelationdesc as Role, governmentidcard as HKID, PassportNumber, Gender as Gender, DateOfBirth, rtrim(namesuffix) + ' ' + rtrim(firstname) as [Name] " & _
            " from (select CustomerID, PolicyAccountID, PolicyRelateCode from csw_poli_rel union select CustomerID, PolicyAccountID, PolicyRelateCode from csw_poli_rel_ext where policyrelatecode in ('PP')) r, customer c, PolicyAccountRelationCodes " & _
            " where policyaccountid = '" & PolicyNo & "' " & _
            " and r.customerid = c.customerid " & _
            " and r.policyrelatecode = policyaccountrelationcode "
        Try

            If GetDT(strSQL, strCIWConn, dt, strError) Then
                Return dt
            End If

            'Me.ConnectCiw()
            'If Not dbCiw.ExecQuery(strSQL, dt) Then
            '    Throw New Exception(dbCiw.RecentErrorMessage)
            'End If

        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            'DisconnectCiw()
        End Try

    End Function

    Private Function getModeDesc(ByVal mode As String) As String
        Dim strEachMode As String = ""
        If mode.Trim = "12" Then
            strEachMode = "MONTHLY"
        ElseIf mode.Trim = "06" Or mode.Trim = "6" Then
            strEachMode = "HALF-YEARLY"
        ElseIf mode.Trim = "01" Or mode.Trim = "1" Then
            strEachMode = "YEARLY"
        Else
            strEachMode = mode
        End If

        Return strEachMode

    End Function

    Private Function getFundName(ByVal code As String) As String
        Dim strFundName As String = ""
        If code.Trim <> "" Then
            If dtFundName.Rows.Count > 0 Then
                dtFundName.DefaultView.RowFilter = "mpfinv_code='" & code.Trim & "'"
                If dtFundName.DefaultView.Count > 0 Then
                    strFundName = IIf(String.IsNullOrEmpty(dtFundName.DefaultView(0)("mpfinv_chi_desc")), "", dtFundName.DefaultView(0)("mpfinv_chi_desc").ToString().Trim())
                End If
            End If
        End If
        Return strFundName
    End Function

    Private Sub setDBHeader(ByVal strEnv As String)

        Select Case env
            Case "DEV07"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "SIT07"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "UAT07"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "PRD01"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "D202"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "S202"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
            Case "U202"
                objDBHeader.UserID = strUserID
                objDBHeader.CompanyID = Company
                objDBHeader.EnvironmentUse = env
                objDBHeader.ProjectAlias = "LAS"
                objDBHeader.UserType = "LASUPDATE"
        End Select
    End Sub

    Private Function GetWSNBWrapper(ByVal strEnv As String) As CNBWrapper.NBWrapper
        Dim ws As New CNBWrapper.NBWrapper()
        Dim header As New CNBWrapper.ComSOAPHeader()
        Dim mq As New CNBWrapper.MQSOAPHeader()

        header.UserID = strUserID
        header.CompanyID = If(strEnv = "PRD01", Company, "ING")    ' Always use "ING" in UAT
        header.EnvironmentUse = strEnv
        header.ProjectAlias = "LAS"
        header.UserType = "LASUPDATE"
        mq.UserID = header.UserID
        mq.CompanyID = Company
        mq.ProjectAlias = "LAS"
        mq.UserType = "LASUPDATE"
        mq.QueueManager = g_Qman
        mq.EnvironmentUse = header.EnvironmentUse
        mq.RemoteQueue = g_WinRemoteQ
        mq.ReplyToQueue = g_LAReplyQ
        mq.LocalQueue = g_WinLocalQ

        ws.ComSOAPHeaderValue = header
        ws.MQSOAPHeaderValue = mq

        ' Specifically for PRD
        ws.Url = If(strEnv = "PRD01", "http://hkcomprd2/CNBWS/NBWrapper.asmx", Utility.Utility.GetWebServiceURL("CNBWS", gobjDBHeader, gobjMQQueHeader))

        ws.Credentials = System.Net.CredentialCache.DefaultCredentials
        ws.Timeout = 1800000
        Return ws
    End Function

    Private Sub ShowPolicyRecord(ByVal strPolicy As String)
        Try
            Dim strErr As String = ""
            Dim send As New CNBWrapper.BoSchema_NBEnquiry_Send_schema()
            Dim rece As CNBWrapper.BoSchema_NBEnquiry_Rece_GetNBPEnquirySchema_DS = Nothing
            Dim ws As CNBWrapper.NBWrapper = GetWSNBWrapper(env)
            send.PolicyNo = strPolicy

            'load pop-up message
            LoadMsg()

            Call createDtCompDtl(dsMainInfo)
            Call createDtContDtl(dsMainInfo)
            Call createDtLifeDtl(dsMainInfo)
            Call createDtCompFund(dsMainInfo)
            Call setDBHeader(env)

            'ws.Url = "http://localhost:16448/CNBWS/NBWrapper.asmx"
            'ws.Url = "http://hkcomdev2/ingsit07nbaws/NBWrapper.asmx"

            If Not ws.EnquiryProposal(send, rece, strErr) Then
                MsgBox("ShowPolicyRecord Warning: " & strErr)
                Exit Sub
            End If

            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
            If rece.CompDtl.Length = 0 And rece.LifeDtl.Length = 0 Then
                If getNBRRecord(strPolicy, strErr) Then
                    Exit Sub
                Else
                    MsgBox("ShowPolicyRecord Warning: " & strErr)
                    Exit Sub
                End If
            End If
            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

            dtBasicPlan = getDtBasicPlan().Copy
            dtFundName = getDtFundName().Copy

            For intSeqNo As Integer = 0 To rece.LifeDtl.Length - 1
                Dim dr As DataRow
                dr = dsMainInfo.Tables("LifeDtl").NewRow
                dr.Item("LifeNo") = IIf(String.IsNullOrEmpty(rece.LifeDtl(intSeqNo).LifeNo), "", rece.LifeDtl(intSeqNo).LifeNo)
                dr.Item("LifeClientNo") = IIf(String.IsNullOrEmpty(rece.LifeDtl(intSeqNo).LifeClientNo), "", rece.LifeDtl(intSeqNo).LifeClientNo)
                dsMainInfo.Tables("LifeDtl").Rows.Add(dr)
            Next

            For intSeqNo As Integer = 0 To rece.CompDtl.Length - 1
                Dim dr As DataRow
                dr = dsMainInfo.Tables("CompDtl").NewRow
                dr.Item("CovLifeNo") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovLifeNo), "", rece.CompDtl(intSeqNo).CovLifeNo)
                dr.Item("CovNo") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovNo), "", rece.CompDtl(intSeqNo).CovNo)
                dr.Item("CovRiderno") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiderno), "", rece.CompDtl(intSeqNo).CovRiderno)

                If dsMainInfo.Tables("LifeDtl").Rows.Count > 0 Then
                    dsMainInfo.Tables("LifeDtl").DefaultView.RowFilter = "LifeNo='" & rece.CompDtl(intSeqNo).CovLifeNo & "'"
                    dr.Item("LifeClientNo") = IIf(String.IsNullOrEmpty(dsMainInfo.Tables("LifeDtl").DefaultView(0)("LifeClientNo")), "", dsMainInfo.Tables("LifeDtl").DefaultView(0)("LifeClientNo").ToString().Trim())
                Else
                    dr.Item("LifeClientNo") = ""
                End If

                'dr.Item("RiderCliNo") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).RiderCliNo), "", rece.CompDtl(intSeqNo).RiderCliNo)
                dr.Item("PlanCode") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).PlanCode), "", rece.CompDtl(intSeqNo).PlanCode)
                If dtBasicPlan.Rows.Count > 0 Then
                    dtBasicPlan.DefaultView.RowFilter = "ProductID='" & IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).PlanCode), "", rece.CompDtl(intSeqNo).PlanCode) & "'"
                    dr.Item("PlanName") = IIf(String.IsNullOrEmpty(dtBasicPlan.DefaultView(0)("Description")), "", dtBasicPlan.DefaultView(0)("Description").ToString().Trim())
                Else
                    dr.Item("PlanName") = ""
                End If

                'dr.Item("NoOfLife") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).NoOfLife), "", rece.CompDtl(intSeqNo).NoOfLife)
                dr.Item("CovMortCls") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovMortCls), "", rece.CompDtl(intSeqNo).CovMortCls)
                'dr.Item("CovRiskCessAge") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiskCessAge), "", rece.CompDtl(intSeqNo).CovRiskCessAge)
                'dr.Item("CovRiskCessTerm") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiskCessTerm), "", rece.CompDtl(intSeqNo).CovRiskCessTerm)
                'dr.Item("CovRiskCessDate") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiskCessDate), "", rece.CompDtl(intSeqNo).CovRiskCessDate)
                dr.Item("CovPremCessAge") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovPremCessAge), "", rece.CompDtl(intSeqNo).CovPremCessAge)
                'dr.Item("CovPremCessTerm") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovPremCessTerm), "", rece.CompDtl(intSeqNo).CovPremCessTerm)
                'dr.Item("CovPremCessDate") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovPremCessDate), "", rece.CompDtl(intSeqNo).CovPremCessDate)

                'dr.Item("CovMaturityAge") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovMaturityAge), "", rece.CompDtl(intSeqNo).CovMaturityAge)
                'dr.Item("CovMaturityTerm") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovMaturityTerm), "", rece.CompDtl(intSeqNo).CovMaturityTerm)
                'dr.Item("CovMaturityDate") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovMaturityDate), "", rece.CompDtl(intSeqNo).CovMaturityDate)
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                dr.Item("CovSumInsured") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovSumInsured), "", Format(Decimal.Parse(rece.CompDtl(intSeqNo).CovSumInsured), "0.00"))
                'dr.Item("CovSumInsured") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovSumInsured), "", rece.CompDtl(intSeqNo).CovSumInsured)
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End
                'dr.Item("CovLoadPrem") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovLoadPrem), "", rece.CompDtl(intSeqNo).CovLoadPrem)
                'dr.Item("CovTotPremium") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovTotPremium), "", rece.CompDtl(intSeqNo).CovTotPremium)
                'dr.Item("CovUsualResidence") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovUsualResidence), "", rece.CompDtl(intSeqNo).CovUsualResidence)

                'dr.Item("AGGGRETSI") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).AGGGRETSI), "", rece.CompDtl(intSeqNo).AGGGRETSI)
                'dr.Item("ProductSubPlan") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).ProductSubPlan), "", rece.CompDtl(intSeqNo).ProductSubPlan)
                'dr.Item("ProductSubClass") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).ProductSubClass), "", rece.CompDtl(intSeqNo).ProductSubClass)
                'dr.Item("WaitingPeriod") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).WaitingPeriod), "", rece.CompDtl(intSeqNo).WaitingPeriod)
                'dr.Item("ExclusionCode1") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).ExclusionCode1), "", rece.CompDtl(intSeqNo).ExclusionCode1)
                'dr.Item("ExclusionCode2") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).ExclusionCode2), "", rece.CompDtl(intSeqNo).ExclusionCode2)
                'dr.Item("ExclusionCode3") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).ExclusionCode3), "", rece.CompDtl(intSeqNo).ExclusionCode3)
                'dr.Item("UWdecisionType") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).UWdecisionType), "", rece.CompDtl(intSeqNo).UWdecisionType)
                'dr.Item("UWresultType") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).UWresultType), "", rece.CompDtl(intSeqNo).UWresultType)

                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                dr.Item("CovRiskCessAge") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiskCessAge), "", rece.CompDtl(intSeqNo).CovRiskCessAge)
                dr.Item("CovRiskCessDate") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovRiskCessDate), "", rece.CompDtl(intSeqNo).CovRiskCessDate)
                dr.Item("CovPremCessDate") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovPremCessDate), "", rece.CompDtl(intSeqNo).CovPremCessDate)
                dr.Item("CovLoadPrem") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovLoadPrem), "", Format(Decimal.Parse(rece.CompDtl(intSeqNo).CovLoadPrem), "0.00"))
                dr.Item("CovTotPremium") = IIf(String.IsNullOrEmpty(rece.CompDtl(intSeqNo).CovTotPremium), "", Format(Decimal.Parse(rece.CompDtl(intSeqNo).CovTotPremium), "0.00"))
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

                dsMainInfo.Tables("CompDtl").Rows.Add(dr)
            Next

            For intSeqNo As Integer = 0 To rece.CompFund.Length - 1
                Dim dr As DataRow
                dr = dsMainInfo.Tables("CompFund").NewRow
                dr.Item("FundLifeNo") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCovNo), "", rece.CompFund(intSeqNo).FundCovNo)
                dr.Item("FundCovNo") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCovNo), "", rece.CompFund(intSeqNo).FundCovNo)
                dr.Item("FundRiderNo") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundRiderNo), "", rece.CompFund(intSeqNo).FundRiderNo)
                dr.Item("FundCode1") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode1), "", rece.CompFund(intSeqNo).FundCode1)
                dr.Item("FundName1") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode1), "", rece.CompFund(intSeqNo).FundCode1))
                dr.Item("FundShare1") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare1), "", rece.CompFund(intSeqNo).FundShare1)

                dr.Item("FundCode2") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode2), "", rece.CompFund(intSeqNo).FundCode2)
                dr.Item("FundName2") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode2), "", rece.CompFund(intSeqNo).FundCode2))
                dr.Item("FundShare2") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare2), "", rece.CompFund(intSeqNo).FundShare2)

                dr.Item("FundCode3") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode3), "", rece.CompFund(intSeqNo).FundCode3)
                dr.Item("FundName3") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode3), "", rece.CompFund(intSeqNo).FundCode3))
                dr.Item("FundShare3") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare3), "", rece.CompFund(intSeqNo).FundShare3)

                dr.Item("FundCode4") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode4), "", rece.CompFund(intSeqNo).FundCode4)
                dr.Item("FundName4") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode4), "", rece.CompFund(intSeqNo).FundCode4))
                dr.Item("FundShare4") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare4), "", rece.CompFund(intSeqNo).FundShare4)

                dr.Item("FundCode5") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode5), "", rece.CompFund(intSeqNo).FundCode5)
                dr.Item("FundName5") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode5), "", rece.CompFund(intSeqNo).FundCode5))
                dr.Item("FundShare5") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare5), "", rece.CompFund(intSeqNo).FundShare5)

                dr.Item("FundCode6") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode6), "", rece.CompFund(intSeqNo).FundCode6)
                dr.Item("FundName6") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode6), "", rece.CompFund(intSeqNo).FundCode6))
                dr.Item("FundShare6") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare6), "", rece.CompFund(intSeqNo).FundShare6)

                dr.Item("FundCode7") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode7), "", rece.CompFund(intSeqNo).FundCode7)
                dr.Item("FundName7") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode7), "", rece.CompFund(intSeqNo).FundCode7))
                dr.Item("FundShare7") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare7), "", rece.CompFund(intSeqNo).FundShare7)

                dr.Item("FundCode8") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode8), "", rece.CompFund(intSeqNo).FundCode8)
                dr.Item("FundName8") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode8), "", rece.CompFund(intSeqNo).FundCode8))
                dr.Item("FundShare8") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare8), "", rece.CompFund(intSeqNo).FundShare8)

                dr.Item("FundCode9") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode9), "", rece.CompFund(intSeqNo).FundCode9)
                dr.Item("FundName9") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode9), "", rece.CompFund(intSeqNo).FundCode9))
                dr.Item("FundShare9") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare9), "", rece.CompFund(intSeqNo).FundShare9)

                dr.Item("FundCode10") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode10), "", rece.CompFund(intSeqNo).FundCode10)
                dr.Item("FundName10") = getFundName(IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundCode10), "", rece.CompFund(intSeqNo).FundCode10))
                dr.Item("FundShare10") = IIf(String.IsNullOrEmpty(rece.CompFund(intSeqNo).FundShare10), "", rece.CompFund(intSeqNo).FundShare10)

                dsMainInfo.Tables("CompFund").Rows.Add(dr)
            Next

            If rece.ContDtl IsNot Nothing Then
                'Dim dr As DataRow
                'dr = dsMainInfo.Tables("ContDtl").NewRow
                'dr.Item("Comm_Agent1") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent1), "", rece.ContDtl.Comm_Agent1)
                'dr.Item("Comm_Share1") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share1), "", rece.ContDtl.Comm_Share1)
                'dr.Item("Comm_Agent2") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent2), "", rece.ContDtl.Comm_Agent2)
                'dr.Item("Comm_Share2") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share2), "", rece.ContDtl.Comm_Share2)
                'dr.Item("Comm_Agent3") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent3), "", rece.ContDtl.Comm_Agent3)
                'dr.Item("Comm_Share3") = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share3), "", rece.ContDtl.Comm_Share3)

                Dim PolicyRelationDT As DataTable
                PolicyRelationDT = getPolicyRelation(strPolicy)
                dgvRelation.Columns.Clear()
                dgvRelation.DataSource = PolicyRelationDT
                dgvRelation.AllowUserToAddRows = False
                dgvRelation.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dgvRelation.Columns(6).Width = 300

                Dim custName As String = getInsuredName(IIf(String.IsNullOrEmpty(rece.ContDtl.OwnClientNo), "", rece.ContDtl.OwnClientNo))
                Dim strPayMethod As String = getPaymentMethod(IIf(String.IsNullOrEmpty(rece.ContDtl.PayMethod), "", rece.ContDtl.PayMethod))
                Dim strMode As String = getModeDesc(IIf(String.IsNullOrEmpty(rece.ContDtl.Billing_Freq), "", rece.ContDtl.Billing_Freq))

                lblCurrency.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.ContractCur), "", rece.ContDtl.ContractCur)
                lblPolicyStatus.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.ContractSts), "", rece.ContDtl.ContractSts)
                lblMode.Text = strMode
                lblPayMethod.Text = strPayMethod
                lblOwner.Text = custName
                lblServicingAgent.Text = getAgentName(IIf(String.IsNullOrEmpty(rece.ContDtl.Servic_Agent), "", rece.ContDtl.Servic_Agent))
                lblRCD.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.RiskCommDate), "", rece.ContDtl.RiskCommDate)

                txtCustomerNo.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.OwnClientNo), "", rece.ContDtl.OwnClientNo)
                txtCustName.Text = custName
                txtPolicyCurrency.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.ContractCur), "", rece.ContDtl.ContractCur)
                txtBillingCurrency.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Billing_Cur), "", rece.ContDtl.Billing_Cur)
                txtPayMethod.Text = strPayMethod
                txtMode.Text = strMode

                txtRiskCommDate.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.RiskCommDate), "", rece.ContDtl.RiskCommDate)
                txtProposalDate.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Proposal_Dt), "", rece.ContDtl.Proposal_Dt)
                txtAppRecvDate.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Proposal_Rec_dt), "", rece.ContDtl.Proposal_Rec_dt)

                'txtAgentCode1.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent1), "", rece.ContDtl.Comm_Agent1)
                'txtAgentShare1.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share1), "", rece.ContDtl.Comm_Share1)
                'txtAgentCode2.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent2), "", rece.ContDtl.Comm_Agent2)
                'txtAgentShare2.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share2), "", rece.ContDtl.Comm_Share2)
                'txtAgentCode3.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent3), "", rece.ContDtl.Comm_Agent3)
                'txtAgentShare3.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share3), "", rece.ContDtl.Comm_Share3)

                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                If IIf(String.IsNullOrEmpty(rece.ContDtl.PayMethod), "", rece.ContDtl.PayMethod) = "C" Then
                    txtDDADay.Text = ""
                Else
                    txtDDADay.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Billing_Date.Trim.ToString.Substring(3, 2)), "", rece.ContDtl.Billing_Date.Trim.ToString.Substring(3, 2))
                End If

                If rece.ContDtl.Comm_Agent1.Trim <> "" Then
                    txtAgentCode1.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent1), "", rece.ContDtl.Comm_Agent1)
                    txtAgentName1.Text = getAgentName(IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent1), "", rece.ContDtl.Comm_Agent1))
                    txtAgentShare1.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share1), "", rece.ContDtl.Comm_Share1)
                Else
                    txtAgentCode1.Text = ""
                    txtAgentName1.Text = ""
                    txtAgentShare1.Text = ""

                End If

                If rece.ContDtl.Comm_Agent2.Trim <> "" Then
                    txtAgentCode2.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent2), "", rece.ContDtl.Comm_Agent2)
                    txtAgentName2.Text = getAgentName(IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent2), "", rece.ContDtl.Comm_Agent2))
                    txtAgentShare2.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share2), "", rece.ContDtl.Comm_Share2)
                Else
                    txtAgentCode2.Text = ""
                    txtAgentName2.Text = ""
                    txtAgentShare2.Text = ""
                End If

                If rece.ContDtl.Comm_Agent3.Trim <> "" Then
                    txtAgentCode3.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent3), "", rece.ContDtl.Comm_Agent3)
                    txtAgentName3.Text = getAgentName(IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Agent3), "", rece.ContDtl.Comm_Agent3))
                    txtAgentShare3.Text = IIf(String.IsNullOrEmpty(rece.ContDtl.Comm_Share3), "", rece.ContDtl.Comm_Share3)
                Else
                    txtAgentCode3.Text = ""
                    txtAgentName3.Text = ""
                    txtAgentShare3.Text = ""

                End If
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

            End If

            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
            If rece.LifeDtl IsNot Nothing Then
                If rece.LifeDtl.Length > 0 Then
                    Dim insuredName As String = getInsuredName(IIf(String.IsNullOrEmpty(rece.LifeDtl(0).LifeClientNo), "", rece.LifeDtl(0).LifeClientNo))
                    lblBasicInsured.Text = insuredName
                End If
            End If
            If rece.FinDtl IsNot Nothing Then
                mtbPSContCurr.Text = IIf(String.IsNullOrEmpty(rece.FinDtl.FIN_hyphen_PREMSUSP), "", Format(Decimal.Parse(rece.FinDtl.FIN_hyphen_PREMSUSP), "0.00"))
                ' mtbPSContHKD.Text = IIf(String.IsNullOrEmpty(rece.FinDtl.FIN_hyphen_PREMSUSP), "", Format(Decimal.Parse(rece.FinDtl.FIN_hyphen_PREMSUSP), "0.00"))

                Dim strExRate(2) As String
                strExRate(0) = txtPolicyCurrency.Text
                strExRate(1) = txtBillingCurrency.Text
                Dim dtExRate As DataTable = New DataTable

                If GetSysNBExRate(strExRate, dtExRate) Then
                    If dtExRate IsNot Nothing Then
                        If dtExRate.Rows.Count > 0 Then
                            mtbPSContHKD.Text = Format(dtExRate.Rows(0).Item("nbrser_exchange_rate") * Format(Decimal.Parse(mtbPSContCurr.Text), "0.00"), "0.00")
                        Else
                            mtbPSContHKD.Text = ""
                        End If
                    Else
                        mtbPSContHKD.Text = ""
                    End If
                Else
                    mtbPSContHKD.Text = ""
                End If
            End If

            Dim dblBoosterAmt As Double = 0
            Dim dblTotalPremium As Double = 0
            Dim dvwCheckBooster As DataView
            Dim dtCheckBooster As New DataTable

            If Not GetSysSinglePlanCode(dtCheckBooster) Then
                Throw New Exception("Unable to load ccs_booster_plan")
            Else
                dvwCheckBooster = dtCheckBooster.DefaultView

                If rece.CompDtl IsNot Nothing Then
                    For intSeqNo As Integer = 0 To rece.CompDtl.Length - 1
                        Dim dr As DataRow

                        dvwCheckBooster.RowFilter = String.Format("cbp_booster = 'Y' and cbp_plan_code = '{0}'", rece.CompDtl(intSeqNo).PlanCode)
                        If dvwCheckBooster.Count > 0 Then
                            dblBoosterAmt += Val(rece.CompDtl(intSeqNo).CovTotPremium)
                        Else
                            dblTotalPremium += Val(rece.CompDtl(intSeqNo).CovTotPremium)
                        End If
                    Next
                    mtbModePremium.Text = Format(dblTotalPremium, "0.00")
                Else
                    mtbModePremium.Text = ""
                End If

            End If
            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

            InitDGV()
        Catch ex As Exception
            MsgBox("ShowPolicyRecord error : " & ex.Message)
        End Try
    End Sub

    Public Sub InitDGV()
        'Header
        InternalDSCompD = Nothing
        InternalDSCompD = New DataSet
        InternalDSCompD.Tables.Add()

        DGVCompDetail.Columns.Clear()
        DGVCompDetail.DataSource = Nothing

        With InternalDSCompD.Tables(0)
            .Columns.Add("CovLifeNo", GetType(System.String))
            .Columns.Add("CovNo", GetType(System.String))
            .Columns.Add("CovRiderno", GetType(System.String))
            .Columns.Add("LifeClientNo", GetType(System.String))
            .Columns.Add("PlanCode", GetType(System.String))
            .Columns.Add("PlanName", GetType(System.String))
            .Columns.Add("CovMortCls", GetType(System.String))
            .Columns.Add("CovPremCessAge", GetType(System.String))
            .Columns.Add("CovSumInsured", GetType(System.String))
            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
            .Columns.Add("CovRiskCessAge", GetType(System.String))
            .Columns.Add("CovRiskCessDate", GetType(System.String))
            .Columns.Add("CovPremCessDate", GetType(System.String))
            .Columns.Add("CovLoadPrem", GetType(System.String))
            .Columns.Add("CovTotPremium", GetType(System.String))
            ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End
        End With

        With dsMainInfo.Tables("CompDtl")
            Dim nrow As DataRow
            For i As Integer = 0 To .DefaultView.Count - 1
                nrow = InternalDSCompD.Tables(0).NewRow
                nrow("CovLifeNo") = .DefaultView(i).Item("CovLifeNo").ToString
                nrow("CovNo") = .DefaultView(i).Item("CovNo")
                nrow("CovRiderno") = .DefaultView(i).Item("CovRiderno").ToString
                nrow("LifeClientNo") = .DefaultView(i).Item("LifeClientNo").ToString
                nrow("PlanCode") = .DefaultView(i).Item("PlanCode").ToString
                nrow("PlanName") = .DefaultView(i).Item("PlanName").ToString
                nrow("CovMortCls") = .DefaultView(i).Item("CovMortCls").ToString
                nrow("CovPremCessAge") = .DefaultView(i).Item("CovPremCessAge").ToString
                nrow("CovSumInsured") = .DefaultView(i).Item("CovSumInsured").ToString
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                nrow("CovRiskCessAge") = .DefaultView(i).Item("CovRiskCessAge").ToString
                nrow("CovRiskCessDate") = .DefaultView(i).Item("CovRiskCessDate").ToString
                nrow("CovPremCessDate") = .DefaultView(i).Item("CovPremCessDate").ToString
                nrow("CovLoadPrem") = .DefaultView(i).Item("CovLoadPrem").ToString
                nrow("CovTotPremium") = .DefaultView(i).Item("CovTotPremium").ToString
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End
                InternalDSCompD.Tables(0).Rows.Add(nrow)
            Next
            .DefaultView.RowFilter = ""
        End With

        DGVCompDetail.Columns.Clear()
        DGVCompDetail.DataSource = InternalDSCompD.Tables(0).DefaultView

        DGVCompDetail.AllowUserToAddRows = False
        DGVCompDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub DGVCompDetail_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVCompDetail.SelectionChanged
        Try

            If DGVCompDetail.SelectedRows.Count > 0 Then

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovNo").Value) Then
                    txtCovNo.Text = DGVCompDetail.SelectedRows(0).Cells("CovNo").Value
                Else
                    txtCovNo.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovLifeNo").Value) Then
                    txtLifeNo.Text = DGVCompDetail.SelectedRows(0).Cells("CovLifeNo").Value
                Else
                    txtLifeNo.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovRiderno").Value) Then
                    txtRiderNo.Text = DGVCompDetail.SelectedRows(0).Cells("CovRiderno").Value
                Else
                    txtRiderNo.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovSumInsured").Value) Then
                    txtSumInsured.Text = DGVCompDetail.SelectedRows(0).Cells("CovSumInsured").Value
                Else
                    txtSumInsured.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovMortCls").Value) Then
                    txtMortalityCls.Text = DGVCompDetail.SelectedRows(0).Cells("CovMortCls").Value
                Else
                    txtMortalityCls.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovPremCessAge").Value) Then
                    txtPremCessAge.Text = DGVCompDetail.SelectedRows(0).Cells("CovPremCessAge").Value
                Else
                    txtPremCessAge.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("PlanCode").Value) Then
                    txtPlanName.Text = DGVCompDetail.SelectedRows(0).Cells("PlanCode").Value & " - " & DGVCompDetail.SelectedRows(0).Cells("PlanName").Value
                Else
                    txtPlanName.Text = ""
                End If

                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovRiskCessAge").Value) Then
                    TextBox10.Text = DGVCompDetail.SelectedRows(0).Cells("CovRiskCessAge").Value
                Else
                    TextBox10.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovRiskCessDate").Value) Then
                    TextBox11.Text = DGVCompDetail.SelectedRows(0).Cells("CovRiskCessDate").Value
                Else
                    TextBox11.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovPremCessDate").Value) Then
                    TextBox4.Text = DGVCompDetail.SelectedRows(0).Cells("CovPremCessDate").Value
                Else
                    TextBox4.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovLoadPrem").Value) Then
                    TextBox9.Text = DGVCompDetail.SelectedRows(0).Cells("CovLoadPrem").Value
                Else
                    TextBox9.Text = ""
                End If

                If Not IsDBNull(DGVCompDetail.SelectedRows(0).Cells("CovTotPremium").Value) Then
                    txtNextPremAmt.Text = DGVCompDetail.SelectedRows(0).Cells("CovTotPremium").Value
                Else
                    txtNextPremAmt.Text = ""
                End If
                ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End

                DGVFundAlloc.Columns.Clear()
                DGVFundAlloc.DataSource = Nothing

                'Details
                InternalDSFundAlloc = Nothing
                InternalDSFundAlloc = New DataSet
                InternalDSFundAlloc.Tables.Add()

                With InternalDSFundAlloc.Tables(0)
                    .Columns.Add("FundCode", GetType(System.String))
                    .Columns.Add("FundName", GetType(System.String))
                    .Columns.Add("FundShare", GetType(System.String))
                End With

                If dsMainInfo.Tables("CompFund").Rows.Count > 0 Then
                    dsMainInfo.Tables("CompFund").DefaultView.RowFilter = "FundLifeNo='" & DGVCompDetail.SelectedRows(0).Cells("CovLifeNo").Value & "'  and FundCovNo='" & DGVCompDetail.SelectedRows(0).Cells("CovNo").Value & "' and FundRiderNo='" & DGVCompDetail.SelectedRows(0).Cells("CovRiderno").Value & "'"
                    If dsMainInfo.Tables("CompFund").DefaultView.Count > 0 Then
                        Dim nrow As DataRow
                        For i As Integer = 1 To 10
                            If dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundCode" & i).ToString.Trim <> "" Then
                                nrow = InternalDSFundAlloc.Tables(0).NewRow
                                nrow("FundCode") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundCode" & i).ToString.Trim
                                nrow("FundName") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundName" & i).ToString.Trim
                                nrow("FundShare") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundShare" & i).ToString.Trim
                                InternalDSFundAlloc.Tables(0).Rows.Add(nrow)
                            End If
                        Next
                    End If
                    dsMainInfo.Tables("CompFund").DefaultView.RowFilter = ""
                End If

                DGVFundAlloc.DataSource = InternalDSFundAlloc.Tables(0)

                'dtFundAlloc = Nothing
                'dtFundAlloc = New DataTable
                'With dtFundAlloc
                '    .Columns.Add("FundCode", GetType(System.String))
                '    .Columns.Add("FundName", GetType(System.String))
                '    .Columns.Add("FundShare", GetType(System.String))
                'End With

                'If dsMainInfo.Tables("CompFund").Rows.Count > 0 Then
                '    dsMainInfo.Tables("CompFund").DefaultView.RowFilter = "FundLifeNo='" & DGVCompDetail.SelectedRows(0).Cells("CovLifeNo").Value & "'  and FundCovNo='" & DGVCompDetail.SelectedRows(0).Cells("CovNo").Value & "' and FundRiderNo='" & DGVCompDetail.SelectedRows(0).Cells("CovRiderno").Value & "'"

                '    If dsMainInfo.Tables("CompFund").DefaultView.Count > 0 Then

                '        Dim nrow As DataRow
                '        For i As Integer = 1 To 10
                '            If dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundCode" & i).ToString.Trim <> "" Then
                '                nrow = dtFundAlloc.NewRow
                '                nrow("FundCode") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundCode" & i).ToString.Trim
                '                nrow("FundName") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundName" & i).ToString.Trim
                '                nrow("FundShare") = dsMainInfo.Tables("CompFund").DefaultView(0).Item("FundShare" & i).ToString.Trim
                '                dtFundAlloc.Rows.Add(nrow)
                '            End If
                '        Next
                '    End If
                '    dsMainInfo.Tables("CompFund").DefaultView.RowFilter = ""
                'End If
                'DGVFundAlloc.DataSource = dtFundAlloc
            End If

        Catch ex As Exception
            'Dim strError As String = ""
            'strError = "DGVCompDetail.SelectionChanged: " & vbCr & vbLf & ex.Message & vbCr & vbLf & ex.StackTrace
            'MessageBox.Show(strError, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MsgBox("ShowPolicyRecord error : " & ex.Message)
        End Try

    End Sub

    Private Sub frmNewBizAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "New Business Administration (ING) - (" & env & ")"
    End Sub

    ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 Start
    '#Region "Connection Functions"
    '    Private objCcsDB As DBLogon_NET.DBLogon.DBlogonNet
    '    Private objCCSHeader As New Utility.Utility.ComHeader
    '    Private objNbrDB As DBLogon_NET.DBLogon.DBlogonNet
    '    Private objNbrHeader As New Utility.Utility.ComHeader
    '    Private strError As String

    '    Public ReadOnly Property CcsDBIsConnected() As Boolean
    '        Get
    '            If Not objCcsDB Is Nothing Then
    '                Return objCcsDB.IsConnected()
    '            Else
    '                Return False
    '            End If
    '        End Get
    '    End Property

    '    Private Function DisconnectCCSDB() As Boolean
    '        If Not IsNothing(objCcsDB) Then
    '            If objCcsDB.IsConnected Then
    '                objCcsDB.Disconnect()
    '                objCcsDB = Nothing
    '            End If
    '        End If
    '    End Function

    '    Private Function ConnectCcsDB() As Boolean
    '        Try
    '            If Not CcsDBIsConnected Then
    '                objCcsDB = New DBLogon_NET.DBLogon.DBlogonNet
    '                objCcsDB.Project = gobjDBHeader.ProjectAlias
    '                If gobjDBHeader.EnvironmentUse = "PRD01" Then
    '                    objCcsDB.ConnectionAlias = "CCS"
    '                Else
    '                    objCcsDB.ConnectionAlias = gobjDBHeader.CompanyID + "CCS" + gobjDBHeader.EnvironmentUse
    '                End If

    '                objCcsDB.User = gobjDBHeader.UserType
    '                If objCcsDB.Connect() Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            Else
    '                Return True
    '            End If

    '        Catch ex As Exception
    '            Return False
    '            strError = ex.Message
    '        End Try

    '    End Function

    '    Public ReadOnly Property NbrDBIsConnected() As Boolean
    '        Get
    '            If Not objNbrDB Is Nothing Then
    '                Return objNbrDB.IsConnected()
    '            Else
    '                Return False
    '            End If
    '        End Get
    '    End Property

    '    Private Function DisconnectNBRDB() As Boolean
    '        If Not IsNothing(objNbrDB) Then
    '            If objNbrDB.IsConnected Then
    '                objNbrDB.Disconnect()
    '                objNbrDB = Nothing
    '            End If
    '        End If
    '    End Function

    '    Private Function ConnectNbrDB() As Boolean
    '        Try
    '            If Not NbrDBIsConnected Then
    '                objNbrDB = New DBLogon_NET.DBLogon.DBlogonNet
    '                objNbrDB.Project = gobjDBHeader.ProjectAlias
    '                If gobjDBHeader.EnvironmentUse = "PRD01" Then
    '                    objNbrDB.ConnectionAlias = "NBR"
    '                Else
    '                    objNbrDB.ConnectionAlias = gobjDBHeader.CompanyID + "NBR" + gobjDBHeader.EnvironmentUse
    '                End If

    '                objNbrDB.User = gobjDBHeader.UserType
    '                If objNbrDB.Connect() Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            Else
    '                Return True
    '            End If

    '        Catch ex As Exception
    '            Return False
    '            strError = ex.Message
    '        End Try
    '    End Function
    '#End Region

    Public Function GetSysSinglePlanCode(ByRef dt As DataTable) As Boolean
        'Function Description   : Get Single Premium Plan / Booster Code  for validation 
        'Input Parameter        : NIL
        'Processing             : Get From ccs_booster_plan 
        'Output Parameter       : dt = Single Premium Plan or booster Plan Code
        Dim strSql As String = ""
        Dim strError As String = ""
        Dim strCCSConn As String = Utility.Utility.GetDbNameByCompanyEnv_CCS(Company, env) & ".."

        strSql = "select * from " & strCCSConn & "ccs_booster_plan where cbp_booster = 'Y' or cbp_singleprem = 'Y'"

        Try
            If GetDT(strSql, GetConnectionStringByCompanyID(Company), dt, strError) Then
                dt.TableName = "SysSinglePlanCode"
                Return True
            Else
                Return False
            End If

            'If ConnectCcsDB() Then
            '    If Not objCcsDB.ExecQuery(strSql, dt) Then
            '        strError = objCcsDB.RecentErrorMessage
            '        'SysEventLog.ProcEventLog("Error", Now, CCSHeader.UserID, CCSHeader.ProjectAlias, CCSHeader.MachineID, strError, "GetSysDistChannel", "", Now, "", False)
            '        Return False
            '    Else
            '        dt.TableName = "SysSinglePlanCode"
            '        Return True
            '    End If
            'Else
            '    'SysEventLog.ProcEventLog("Error", Now, CCSHeader.UserID, CCSHeader.ProjectAlias, CCSHeader.MachineID, strError, "GetSysContStru", "", Now, "", False)
            '    Return False
            'End If
        Catch ex As Exception
            'strError &= ex.Message
            Throw New Exception("", ex)
            'SysEventLog.ProcEventLog("Error", Now, CCSHeader.UserID, CCSHeader.ProjectAlias, CCSHeader.MachineID, ex.Message, ex.StackTrace, strRef, Now, "", False)
            Return False
        Finally
            ' DisconnectCCSDB()
        End Try
    End Function

    Public Function GetSysNBExRate(ByVal strSrc() As String, ByRef dt As DataTable, Optional ByVal disConn As Boolean = False) As Boolean
        'Function Description   : Get Exchange Rate, for New Business. 
        '                       : For new business, USD--> HKD, is 8, which is different from centeralize exchange rate.
        'Input Parameter        : strSrc(0) = From Exchange Rate Code, E.g. HKD
        'Input Parameter        : strSrc(1) = to Exchange Rate Code, E.g. USD
        'Output Parameter       : dt = Insurance Type Code  Data Table

        Dim strSql As String = ""
        Dim strFrCur As String = ""
        Dim strToCur As String = ""
        Dim strFilter As String = ""
        Dim strError As String = ""

        If strSrc.Length >= 2 Then
            strFrCur = strSrc(0)
            strToCur = strSrc(1)

            If strFrCur <> "" Then
                strFilter &= " where nbrser_from_currency = '" & strFrCur & "'"
            End If
            If strToCur <> "" Then
                strFilter &= " and nbrser_to_currency = '" & strToCur & "'"
            End If
        End If

        Dim strNBRConn As String = Utility.Utility.GetDbNameByCompanyEnv_NBR(Company, env) & ".."

        strSql = "select * from " & strNBRConn & "nbr_si_exchange_rate" & strFilter

        Try
            If GetDT(strSql, GetConnectionStringByCompanyID(Company), dt, strError) Then
                dt.TableName = "SysNbExRate"
                Return True
            Else
                Return False
            End If

            'If ConnectNbrDB() Then
            '    If Not objNbrDB.ExecQuery(strSql, dt) Then
            '        strError = objNbrDB.RecentErrorMessage
            '        Return False
            '    Else
            '        dt.TableName = "SysNbExRate"
            '        Return True
            '    End If
            'End If
        Catch ex As Exception
            Throw New Exception("", ex)
            Return False
        Finally
            'If disConn Then
            '    DisconnectNBRDB()
            'End If
        End Try
    End Function

    Public Function getNBRRecord(ByVal strPolicyNo As String, ByRef strErr As String) As Boolean
        getNBRRecord = False

        Dim objCommon As New LifeClientInterfaceComponent.CommonControl
        objCommon.ComHeader = gobjDBHeader

        Dim dsNbaRece As DataSet = New DataSet
        Dim strSrc(0) As String
        Dim index As Integer = 0
        strSrc(0) = strPolicy.Trim

        If objCommon.GetPropInfo(strSrc, dsNbaRece, strErr) Then
            dtBasicPlan = getDtBasicPlan().Copy
            dtFundName = getDtFundName().Copy

            For intSeqNo As Integer = 0 To dsNbaRece.Tables("dttLIfeDtl").Rows.Count - 1
                Dim dr As DataRow
                dr = dsMainInfo.Tables("LifeDtl").NewRow
                dr.Item("LifeNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttLIfeDtl").Rows(intSeqNo).Item("life_no")), "", dsNbaRece.Tables("dttLIfeDtl").Rows(intSeqNo).Item("life_no"))
                dr.Item("LifeClientNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttLIfeDtl").Rows(intSeqNo).Item("LAClientNo_Client")), "", dsNbaRece.Tables("dttLIfeDtl").Rows(intSeqNo).Item("LAClientNo_Client"))
                dsMainInfo.Tables("LifeDtl").Rows.Add(dr)
            Next

            For intSeqNo As Integer = 0 To dsNbaRece.Tables("dttCompDtl").Rows.Count - 1
                Dim dr As DataRow
                dr = dsMainInfo.Tables("CompDtl").NewRow
                dr.Item("CovLifeNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Life_No")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Life_No"))
                dr.Item("CovNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("coverage_no")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("coverage_no"))
                dr.Item("CovRiderno") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("rider_no")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("rider_no"))

                If dsMainInfo.Tables("LifeDtl").Rows.Count > 0 Then
                    dsMainInfo.Tables("LifeDtl").DefaultView.RowFilter = "LifeNo='" & dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("life_no") & "'"
                    dr.Item("LifeClientNo") = IIf(String.IsNullOrEmpty(dsMainInfo.Tables("LifeDtl").DefaultView(0)("LifeClientNo")), "", dsMainInfo.Tables("LifeDtl").DefaultView(0)("LifeClientNo").ToString().Trim())
                Else
                    dr.Item("LifeClientNo") = ""
                End If

                dr.Item("PlanCode") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("plan_cd")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("plan_cd"))
                If dtBasicPlan.Rows.Count > 0 Then
                    dtBasicPlan.DefaultView.RowFilter = "ProductID='" & IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("plan_cd")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("plan_cd")) & "'"
                    dr.Item("PlanName") = IIf(String.IsNullOrEmpty(dtBasicPlan.DefaultView(0)("Description")), "", dtBasicPlan.DefaultView(0)("Description").ToString().Trim())
                Else
                    dr.Item("PlanName") = ""
                End If

                dr.Item("CovMortCls") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Mort_Class")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Mort_Class"))
                dr.Item("CovPremCessAge") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("prem_cess_age")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("prem_cess_age"))

                dr.Item("CovSumInsured") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("sum_insured")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("sum_insured"))

                dr.Item("CovRiskCessAge") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Bene_cess_age")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Bene_cess_age"))
                dr.Item("CovRiskCessDate") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Bene_exp_dt")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Bene_exp_dt"))
                dr.Item("CovPremCessDate") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Prem_exp_dt")), "", dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Prem_exp_dt"))
                dr.Item("CovLoadPrem") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Loaded_Premium")), "", Format(Decimal.Parse(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Loaded_Premium")), "0.00"))
                dr.Item("CovTotPremium") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Modal_Premium")), "", Format(Decimal.Parse(dsNbaRece.Tables("dttCompDtl").Rows(intSeqNo).Item("Modal_Premium")), "0.00"))

                dsMainInfo.Tables("CompDtl").Rows.Add(dr)
            Next

            Dim drCompFund As DataRow
            drCompFund = dsMainInfo.Tables("CompFund").NewRow

            drCompFund.Item("FundLifeNo") = ""
            drCompFund.Item("FundCovNo") = ""
            drCompFund.Item("FundRiderNo") = ""
            drCompFund.Item("FundCode1") = ""
            drCompFund.Item("FundName1") = ""
            drCompFund.Item("FundShare1") = ""
            drCompFund.Item("FundCode2") = ""
            drCompFund.Item("FundName2") = ""
            drCompFund.Item("FundShare2") = ""
            drCompFund.Item("FundCode3") = ""
            drCompFund.Item("FundName3") = ""
            drCompFund.Item("FundShare3") = ""
            drCompFund.Item("FundCode4") = ""
            drCompFund.Item("FundName4") = ""
            drCompFund.Item("FundShare4") = ""
            drCompFund.Item("FundCode5") = ""
            drCompFund.Item("FundName5") = ""
            drCompFund.Item("FundShare5") = ""
            drCompFund.Item("FundCode6") = ""
            drCompFund.Item("FundName6") = ""
            drCompFund.Item("FundShare6") = ""
            drCompFund.Item("FundCode7") = ""
            drCompFund.Item("FundName7") = ""
            drCompFund.Item("FundShare7") = ""
            drCompFund.Item("FundCode8") = ""
            drCompFund.Item("FundName8") = ""
            drCompFund.Item("FundShare8") = ""
            drCompFund.Item("FundCode9") = ""
            drCompFund.Item("FundName9") = ""
            drCompFund.Item("FundShare9") = ""
            drCompFund.Item("FundCode10") = ""
            drCompFund.Item("FundName10") = ""
            drCompFund.Item("FundShare10") = ""

            For intSeqNo As Integer = 0 To dsNbaRece.Tables("dttCompFund").Rows.Count - 1
                If drCompFund.Item("FundLifeNo").ToString <> dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("coverage_no").ToString Then
                    dsMainInfo.Tables("CompFund").Rows.Add(drCompFund)
                    drCompFund.Item("FundLifeNo") = ""
                    drCompFund.Item("FundCovNo") = ""
                    drCompFund.Item("FundRiderNo") = ""
                    drCompFund.Item("FundCode1") = ""
                    drCompFund.Item("FundName1") = ""
                    drCompFund.Item("FundShare1") = ""
                    drCompFund.Item("FundCode2") = ""
                    drCompFund.Item("FundName2") = ""
                    drCompFund.Item("FundShare2") = ""
                    drCompFund.Item("FundCode3") = ""
                    drCompFund.Item("FundName3") = ""
                    drCompFund.Item("FundShare3") = ""
                    drCompFund.Item("FundCode4") = ""
                    drCompFund.Item("FundName4") = ""
                    drCompFund.Item("FundShare4") = ""
                    drCompFund.Item("FundCode5") = ""
                    drCompFund.Item("FundName5") = ""
                    drCompFund.Item("FundShare5") = ""
                    drCompFund.Item("FundCode6") = ""
                    drCompFund.Item("FundName6") = ""
                    drCompFund.Item("FundShare6") = ""
                    drCompFund.Item("FundCode7") = ""
                    drCompFund.Item("FundName7") = ""
                    drCompFund.Item("FundShare7") = ""
                    drCompFund.Item("FundCode8") = ""
                    drCompFund.Item("FundName8") = ""
                    drCompFund.Item("FundShare8") = ""
                    drCompFund.Item("FundCode9") = ""
                    drCompFund.Item("FundName9") = ""
                    drCompFund.Item("FundShare9") = ""
                    drCompFund.Item("FundCode10") = ""
                    drCompFund.Item("FundName10") = ""
                    drCompFund.Item("FundShare10") = ""
                End If

                drCompFund.Item("FundLifeNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("coverage_no")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("coverage_no"))
                drCompFund.Item("FundCovNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("coverage_no")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("coverage_no"))
                ' drCompFund.Item("FundRiderNo") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("rider_no")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("rider_no"))
                drCompFund.Item("FundRiderNo") = intSeqNo

                If intSeqNo = 0 Then
                    drCompFund.Item("FundCode1") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName1") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare1") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 1 Then
                    drCompFund.Item("FundCode2") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName2") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare2") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 2 Then
                    drCompFund.Item("FundCode3") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName3") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare3") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 3 Then
                    drCompFund.Item("FundCode4") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName4") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare4") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 4 Then
                    drCompFund.Item("FundCode5") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName5") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare5") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 5 Then
                    drCompFund.Item("FundCode6") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName6") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare6") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 6 Then
                    drCompFund.Item("FundCode7") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName7") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare7") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 7 Then
                    drCompFund.Item("FundCode8") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName8") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare8") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 8 Then
                    drCompFund.Item("FundCode9") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName9") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare9") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If

                If intSeqNo = 9 Then
                    drCompFund.Item("FundCode10") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code"))
                    drCompFund.Item("FundName10") = getFundName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_code")))
                    drCompFund.Item("FundShare10") = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc")), "", dsNbaRece.Tables("dttCompFund").Rows(intSeqNo).Item("fund_alloc"))
                End If
                ' dsMainInfo.Tables("CompFund").Rows.Add(drCompFund)
            Next



            If dsNbaRece.Tables("dttContDtl") IsNot Nothing Then

                Dim PolicyRelationDT As DataTable
                PolicyRelationDT = getPolicyRelation(strPolicy)
                dgvRelation.Columns.Clear()
                dgvRelation.DataSource = PolicyRelationDT
                dgvRelation.AllowUserToAddRows = False
                dgvRelation.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dgvRelation.Columns(6).Width = 300

                If dsNbaRece.Tables("dttContDtl").Rows.Count > 0 Then
                    Dim custName As String = ""
                    Dim custNo As String = ""
                    Dim insuredName As String = ""
                    Dim insuredNo As String = ""
                    Dim agentNo As String = ""

                    If dsNbaRece.Tables("dttContcli").Rows.Count > 0 Then
                        Dim nRowInsured() As DataRow
                        nRowInsured = dsNbaRece.Tables("dttContcli").Select("role = 'I' and life_no = 1")
                        If Not nRowInsured Is Nothing Then
                            If nRowInsured.Length >= 1 Then
                                insuredName = getInsuredName(IIf(String.IsNullOrEmpty(nRowInsured(0).Item("Customer_no")), "", nRowInsured(0).Item("Customer_no")))
                                insuredNo = IIf(String.IsNullOrEmpty(nRowInsured(0).Item("Customer_no")), "", nRowInsured(0).Item("Customer_no"))
                            End If
                        End If

                        Dim nRowCustomer() As DataRow
                        nRowCustomer = dsNbaRece.Tables("dttContcli").Select("role = 'P' and life_no = 0")
                        If Not nRowCustomer Is Nothing Then
                            If nRowCustomer.Length >= 1 Then
                                custName = getInsuredName(IIf(String.IsNullOrEmpty(nRowCustomer(0).Item("Customer_no")), "", nRowCustomer(0).Item("Customer_no")))
                                custNo = IIf(String.IsNullOrEmpty(nRowCustomer(0).Item("Customer_no")), "", nRowCustomer(0).Item("Customer_no"))
                            End If
                        End If

                        Dim nRowAgent() As DataRow
                        nRowAgent = dsNbaRece.Tables("dttContcli").Select("role = 'W' and life_no = 0")
                        If Not nRowAgent Is Nothing Then
                            If nRowAgent.Length >= 1 Then
                                agentNo = IIf(String.IsNullOrEmpty(nRowAgent(0).Item("Customer_no")), "", nRowAgent(0).Item("Customer_no"))
                            End If
                        End If
                    End If



                    Dim strPayMethod As String = getPaymentMethod(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_method")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_method")))
                    Dim strMode As String = getModeDesc(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_mode")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_mode")))

                    lblCurrency.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("currency")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("currency"))
                    lblPolicyStatus.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("policy_status")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("policy_status"))
                    lblMode.Text = strMode
                    lblPayMethod.Text = strPayMethod
                    lblOwner.Text = custName
                    lblServicingAgent.Text = getAgentName(agentNo)
                    lblRCD.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("risk_com_date")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("risk_com_date"))
                    lblBasicInsured.Text = insuredName

                    txtCustomerNo.Text = custNo
                    txtCustName.Text = custName
                    txtPolicyCurrency.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("currency")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("currency"))
                    txtBillingCurrency.Text = "HKD"
                    txtPayMethod.Text = strPayMethod
                    txtMode.Text = strMode

                    txtRiskCommDate.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("risk_com_date")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("risk_com_date"))
                    txtProposalDate.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("proposal_date")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("proposal_date"))
                    txtAppRecvDate.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("appl_rec_date")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("appl_rec_date"))
                    If IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_method")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("payment_method")) = "C" Then
                        txtDDADay.Text = ""
                    Else
                        txtDDADay.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("draw_day")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("draw_day"))
                    End If
                    mtbModePremium.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("modal_premium")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("modal_premium"))
                    mtbPSContCurr.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttContDtl").Rows(0).Item("psusp")), "", dsNbaRece.Tables("dttContDtl").Rows(0).Item("psusp"))

                    Dim strExRate(2) As String
                    strExRate(0) = txtPolicyCurrency.Text
                    strExRate(1) = txtBillingCurrency.Text
                    Dim dtExRate As DataTable = New DataTable

                    If GetSysNBExRate(strExRate, dtExRate) Then
                        If dtExRate IsNot Nothing Then
                            If dtExRate.Rows.Count > 0 Then
                                mtbPSContHKD.Text = Format(dtExRate.Rows(0).Item("nbrser_exchange_rate") * Format(Decimal.Parse(mtbPSContCurr.Text), "0.00"), "0.00")
                            Else
                                mtbPSContHKD.Text = ""
                            End If
                        Else
                            mtbPSContHKD.Text = ""
                        End If
                    Else
                        mtbPSContHKD.Text = ""
                    End If
                End If

                If dsNbaRece.Tables("dttCompDtl").Rows.Count > 0 Then
                    If dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code1").Trim <> "" Then
                        txtAgentCode1.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code1")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code1"))
                        txtAgentName1.Text = getAgentName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code1")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code1")))
                        txtAgentShare1.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share1")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share1"))
                    Else
                        txtAgentCode1.Text = ""
                        txtAgentName1.Text = ""
                        txtAgentShare1.Text = ""

                    End If

                    If dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code2").Trim <> "" Then
                        txtAgentCode2.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code2")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code2"))
                        txtAgentName2.Text = getAgentName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code2")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code2")))
                        txtAgentShare2.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share2")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share2"))
                    Else
                        txtAgentCode2.Text = ""
                        txtAgentName2.Text = ""
                        txtAgentShare2.Text = ""
                    End If

                    If dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code3").Trim <> "" Then
                        txtAgentCode3.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code3")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code3"))
                        txtAgentName3.Text = getAgentName(IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code3")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_code3")))
                        txtAgentShare3.Text = IIf(String.IsNullOrEmpty(dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share3")), "", dsNbaRece.Tables("dttCompDtl").Rows(0).Item("Agent_Share3"))
                    Else
                        txtAgentCode3.Text = ""
                        txtAgentName3.Text = ""
                        txtAgentShare3.Text = ""

                    End If
                End If
            End If
        Else
            getNBRRecord = False
        End If

        InitDGV()
        getNBRRecord = True

        Return getNBRRecord
    End Function
    ' Flora Leung, Project Leo Goal 3, 10-Jan-2012 End
    Private Sub LoadMsg()
        Try
            Dim dsMsgApiLst As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_Comp), "MSG_API_LST_NON_INFORCE", New Dictionary(Of String, String)() From {})
            Dim strMsg As String = ""
            Dim strLbl As String = ""
            Dim dsTmp As DataSet
            Dim iCount = 1
            If dsMsgApiLst.Tables.Count > 0 Then
                For Each dr As DataRow In dsMsgApiLst.Tables(0).Rows
                    dsTmp = APIServiceBL.CallAPIBusi(getCompanyCode(g_Comp), dr("MSG_API_LST_NON_INFORCE").ToString(), New Dictionary(Of String, String)() From {
                                                            {"strInPolicy", strPolicy}
                                                            })
                    If dsTmp.Tables.Count > 0 Then
                        For Each dr2 As DataRow In dsTmp.Tables(0).Rows
                            If dr2("MSG").ToString.Trim.Length > 0 Then
                                strMsg = strMsg + iCount.ToString + ". " + dr2("MSG").ToString.Trim + vbNewLine
                                iCount = iCount + 1
                            End If

                        Next
                    End If
                Next
            End If

            If strMsg.Trim.Length > 0 Then
                MessageBox.Show(strMsg.Trim)
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
