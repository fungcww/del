Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports INGLife.Interface
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports CRS_Util

'******************************************************************
' Amendment   : Concurrent fund trade
' Date        : 4/3/2009
' Author      : Eric Shu (ES001)	
'******************************************************************
' Admended By: Flora Leung
' Admended Function:                     
' Added Function:   GetDDRRejectReason
' Date: 18 Jan 2012
' Project: Project Leo Goal 3
'********************************************************************

Public Class CRS
    Private objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters
    Private objCIWHeader As Utility.Utility.ComHeader       'CIWHeader includes MSSQL for CIW conn. parameters
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objPOSHeader As Utility.Utility.POSHeader
    Private objUtility As New Utility.Utility

    Private objCIW As New DBLogon_NET.DBLogon.DBlogonNet
    Private objDB As New DBLogon_NET.DBLogon.DBlogonNet

    Private ObjML As New BOSetup.Mandate_List
    Private ObjDDE As New BOSetup.Direct_Debit_Enquiry
    Private ObjSAB As New BOSetup.Sub_Account_Balance
    Private ObjSAP As New BOSetup.Sub_Account_Posting
    Private ObjPH As New BOSetup.Payment_Hist_Enq
    Private ObjTH As New BOSetup.TransHistHeaderDetail
    Private ObjTP As New BOSetup.Transaction_Posting
    'Private ObjCS As INGLife.Interface.ICS2005

    Private objFSW As New BOSetup.PendingFSW

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
#Region " POS Properties"
    Public Property POSHeader() As Utility.Utility.POSHeader
        Get
            Return objPOSHeader
        End Get
        Set(ByVal value As Utility.Utility.POSHeader)
            objPOSHeader = value
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
    Public Property CiwHeader() As Utility.Utility.ComHeader
        Get
            Return objCIWHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objCIWHeader = value
        End Set
    End Property
#End Region
#Region " DB manaul connect"
    Public WriteOnly Property DBConnection() As DBLogon_NET.DBLogon.DBlogonNet
        Set(ByVal value As DBLogon_NET.DBLogon.DBlogonNet)
            objDB = value
        End Set
    End Property
    Public ReadOnly Property DBIsConnected() As Boolean
        Get
            If Not objDB Is Nothing Then
                objDB.IsConnected()
            Else
                Return False
            End If
        End Get
    End Property
#End Region
#Region " Connection/Disconnection"
    Private Function ConnectDB(ByRef obj As Object, ByVal strProject As String, ByVal strConnAlias As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        Try

            If Not Me.DBIsConnected Then
                obj = New DBLogon_NET.DBLogon.DBlogonNet
                obj.Project = strProject
                obj.ConnectionAlias = strConnAlias 'strComp + strProject + strEnv
                obj.User = strUser
                If obj.Connect() Then
                    Return True
                Else
                    strErr = objDB.RecentErrorMessage
                    Return False
                End If
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    Private Function DisconnectDB(ByVal obj As Object) As Boolean
        obj.Disconnect()
        obj = Nothing
        Return True
    End Function
#End Region

#Region "Direct Debit Enquiry"
    Public Function GetMandateListRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                   ByRef strErr As String) As Boolean
        Try
            Dim i As Int16 = 0
            Dim j As Int16 = 0
            'Getting mandate list 
            ObjML.MQQueuesHeader = Me.objMQQueHeader
            'Return ObjML.GetMandateList(dsSendData, dsReceData, strErr)

            'Get Related Mange by Client ID
            If ObjML.GetMandateList(dsSendData, dsReceData, strErr) = True Then
                dsReceData.Tables(1).Columns.Add("CLNTNUM")
                If dsReceData.Tables(1).Rows.Count > 0 Then
                    For i = 0 To dsReceData.Tables(1).Rows.Count - 1
                        dsReceData.Tables(1).Rows(i).Item("CLNTNUM") = dsSendData.Tables(0).Rows(0).Item(0)
                    Next
                End If


                Dim strSQL As String = ""
                Dim strLAClientNo As String = ""
                Dim dt As New DataTable


                strLAClientNo = dsSendData.Tables(0).Rows(0).Item(0)
                strSQL = "select b.clntnum from csw_laciwmap a, csw_laciwmap b where a.CLNTNUM = '" & strLAClientNo & "'"
                strSQL &= " and a.ciw_no = b.ciw_no and b.CLNTNUM <> '" & strLAClientNo & "'"

                objDBHeader.CompanyID = objMQQueHeader.CompanyID
                objDBHeader.EnvironmentUse = objMQQueHeader.EnvironmentUse
                objDBHeader.UserType = objMQQueHeader.UserType
                objDBHeader.UserID = objMQQueHeader.UserID
                objDBHeader.ProjectAlias = objMQQueHeader.ProjectAlias

                If Exec(strSQL, "CIW", dt, strErr) Then
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            Dim dsReceTmp As New DataSet
                            dsSendData.Tables(0).Rows(0).Item(0) = dt.Rows(i).Item("CLNTNUM")
                            Dim objBOSetupRel As New BOSetup.Mandate_List
                            objBOSetupRel.MQQueuesHeader = Me.MQQueuesHeader
                            If objBOSetupRel.GetMandateList(dsSendData, dsReceTmp, strErr) Then
                                dsReceTmp.Tables(1).Columns.Add("CLNTNUM")
                                If dsReceTmp.Tables(1).Rows.Count > 0 Then
                                    For j = 0 To dsReceTmp.Tables(1).Rows.Count - 1
                                        dsReceTmp.Tables(1).Rows(j).Item("CLNTNUM") = dsSendData.Tables(0).Rows(0).Item(0)
                                    Next
                                End If

                                If dsReceTmp.Tables(0).Rows.Count > 0 Then
                                    dsReceData.Tables(0).Merge(dsReceTmp.Tables(0).Copy)
                                    dsReceData.Tables(1).Merge(dsReceTmp.Tables(1).Copy)
                                End If
                            Else
                                Return False
                            End If
                        Next
                    End If
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try


    End Function

    Public Function GetDirectDebitEnqRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                    ByRef strErr As String) As Boolean
        Try
            'Getting direct debit records 
            ObjDDE.MQQueuesHeader = Me.objMQQueHeader
            Return ObjDDE.GetDirectDebitEnq(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try

    End Function


#End Region

#Region " Sub Account Balance"
    Public Function GetSubAcctBalEnqRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                      ByRef strErr As String) As Boolean
        Try
            'Getting sub account balance records 
            ObjSAB.MQQueuesHeader = Me.objMQQueHeader
            Return ObjSAB.GetSubAcctBalDetail(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function
    Public Function GetSubAcctBalPostingRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                  ByRef strErr As String) As Boolean
        Try
            'Getting sub account posting records 
            ObjSAP.MQQueuesHeader = Me.objMQQueHeader
            Return ObjSAP.GetSubAcctPostingDetail(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try

    End Function
#End Region

#Region " Transaction History"
    Public Function GetTranHistRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                         ByRef strErr As String) As Boolean
        Try
            'Getting transaction history records 
            ObjTH.MQQueuesHeader = Me.objMQQueHeader
            Return ObjTH.GetTransHistHeaderDetail(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try

    End Function

    Public Function GetTranPostingRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                  ByRef strErr As String) As Boolean
        Try
            'Getting transaction posting records 
            ObjTP.MQQueuesHeader = Me.objMQQueHeader
            Return ObjTP.GetTranPosting(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try

    End Function
#End Region

#Region " Payment History"
    Public Function GetPaymentHistRecord(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                          ByRef strErr As String) As Boolean
        Try
            'Getting payment history records 
            ObjPH.MQQueuesHeader = Me.objMQQueHeader
            Return ObjPH.GetPaymentHist(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try

    End Function
#End Region

#Region " Check Pending Fund Switching "
    Public Function CheckPendingFSW(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                                          ByRef strErr As String) As Boolean
        Try
            'Getting payment history records 
            objFSW.MQQueuesHeader = Me.objMQQueHeader
            Return objFSW.GetPendingFSW(dsSendData, dsReceData, strErr)
        Catch ex As Exception
            strErr = "ERROR:" & ex.Message
            Return False
        End Try

    End Function
#End Region

#Region "Get Fund Transaction History"

    Public Function GetFundTxHist(ByVal strPolicy As String, ByVal strTxType As String, ByVal strTxID As String, ByVal strLAFlag As String, ByRef dsReceData As Data.DataSet, _
                                          ByRef strErr As String) As Boolean

        Dim dtData As New DataTable
        Dim strSQL As String = ""
        Dim strLoadType, strLA, strFundListI, strFundListO, strPoCurr As String
        Dim lngTx As String
        Dim dteTxDate As Date
        Dim dblAllocAmt As Double
        Dim dtSwitchIn, dtSwitchOut As DataTable
        Dim dblExRate As Double
        Dim strUser As String
        Dim dblTotal As Double
        Dim intCP As Integer
        Dim strExErr As String = ""
        Dim strLATXID As String = ""
        Dim blnHasNA As Boolean = False

        GetFundTxHist = False

        intCP = 1

        ' Clear all table first
        If dsReceData.Tables.Count > 0 Then
            For i As Integer = 0 To dsReceData.Tables.Count - 1
                dsReceData.Tables.RemoveAt(0)
            Next
        End If

        intCP = 2

        ' Get Fund transaction
        If strTxType = "" And lngTx = "" Then

            strSQL = "Select postq_trans_id, postq_post_on, " & _
                " case when postq_trans_type = 'ULA' then 'Fund Allocation' " & _
                "      when postq_trans_type = 'ULB' then 'Fund Switching' " & _
                "      when postq_trans_type = 'ULS' then 'Partial Surrender' end as postq_trans_type, " & _
                " case when postq_status = 'CO' then 'Completed' " & _
                "      when postq_status = 'IN' and postq_message_type = 'L' then 'In Progress' " & _
                "      when postq_status = '' then 'Completed' end as postq_status, " & _
                " postq_backend_sys " & _
                " from pos_trans_queue " & _
                " where postq_trans_type in ('ULA','ULB','ULS') and postq_backend_sys = 'LA' " & _
                " and (postq_status = '' or postq_status = 'CO' or (postq_status = 'IN' and postq_message_type = 'L')) " & _
                " and postq_policy_no = '" & strPolicy & "'" & _
                " and postq_trans_id in (select cswth_ref_id from ciw_csw_trans_header where cswth_status = 'CO' and cswth_trans_type in ('ULA','ULS','FSW') and cswth_policy_no = '" & strPolicy & "')" & _
                " order by postq_post_on desc"

            If Exec(strSQL, "POS", dtData, strErr) Then
                If dtData.Rows.Count > 0 Then
                    dtData.TableName = "FundTran"
                    strLoadType = dtData.Rows(0).Item("postq_trans_type")
                    lngTx = dtData.Rows(0).Item("postq_trans_id")
                    strLA = dtData.Rows(0).Item("postq_backend_sys")

                    Dim dtData1 As New DataTable
                    For Each dr As DataRow In dtData.Rows
                        If dr.Item("postq_trans_type") = "Partial Surrender" Then
                            strSQL = "Select case when postq_status = 'CO' then 'Completed' " & _
                                " When postq_status = 'IN' and postq_message_type = 'L' then 'In Progress' " & _
                                " When postq_status = '' then 'Completed' end as postq_status " & _
                                " From pos_trans_queue, pos_value_history " & _
                                " Where posvh_trans_id = postq_trans_id and postq_trans_type = 'VAL' " & _
                                "  and posvh_parsurr_trans = " & dr.Item("postq_trans_id")
                            If Exec(strSQL, "POS", dtData1, strErr) Then
                                If dtData1.Rows.Count > 0 Then
                                    dr.Item("postq_status") = dtData1.Rows(0).Item("postq_status")
                                Else
                                    dr.Item("postq_status") = "In Progress"
                                End If
                            End If
                        End If
                    Next
                    dtData.AcceptChanges()
                    dsReceData.Tables.Add(dtData)
                Else
                    strErr = "No fund transaction found for policy " & strPolicy
                    Exit Function
                End If
            Else
                strErr = "Error: Get Fund Transaction" & objCIW.RecentErrorMessage & intCP
                Exit Function
            End If

        Else
            strLoadType = strTxType
            lngTx = strTxID
            strLA = strLAFlag
        End If

        intCP = 3

        Dim wsPOS As POSWS.POSWS
        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        Dim objPowsMQHeader As New POSWS.MQSOAPHeader
        Dim dsPS, dsSWInMain, dsParSurHist As New DataSet
        Dim lngSWOutID As Long
        Dim dtFundAlloc As DataTable
        Dim strTransCode As String = ""

        ' Setup header
        objPowsMQHeader.QueueManager = objMQQueHeader.QueueManager
        objPowsMQHeader.RemoteQueue = objMQQueHeader.RemoteQueue
        objPowsMQHeader.ReplyToQueue = objMQQueHeader.ReplyToQueue
        objPowsMQHeader.LocalQueue = objMQQueHeader.LocalQueue
        objPowsMQHeader.Timeout = objMQQueHeader.Timeout

        objPowsMQHeader.ProjectAlias = objMQQueHeader.ProjectAlias
        objPowsMQHeader.ConnectionAlias = objMQQueHeader.ConnectionAlias
        objPowsMQHeader.UserType = objMQQueHeader.UserType
        objPowsMQHeader.EnvironmentUse = objMQQueHeader.EnvironmentUse
        objPowsMQHeader.CompanyID = objMQQueHeader.CompanyID
        objPowsMQHeader.UserID = objMQQueHeader.UserID
        objPowsMQHeader.MachineID = objMQQueHeader.MachineID
        objPowsMQHeader.VersionNo = objMQQueHeader.VersionNo
        objPowsMQHeader.LibraryName = objMQQueHeader.LibraryName

        objPowsDBHeader.User = objDBHeader.UserID
        objPowsDBHeader.Project = objDBHeader.ProjectAlias
        objPowsDBHeader.Env = objDBHeader.EnvironmentUse
        objPowsDBHeader.Comp = objDBHeader.CompanyID
        objPowsDBHeader.ConnectionAlias = objDBHeader.ConnectionAlias
        objPowsDBHeader.UserType = objDBHeader.UserType

        objPowsDBHeader.CIWUser = objCIWHeader.UserID
        objPowsDBHeader.CIWProject = objCIWHeader.ProjectAlias
        objPowsDBHeader.CIWEnv = objCIWHeader.EnvironmentUse
        objPowsDBHeader.CIWComp = objCIWHeader.CompanyID
        objPowsDBHeader.CIWConnectionAlias = objCIWHeader.ConnectionAlias

        wsPOS = New POSWS.POSWS
        wsPOS.DBSOAPHeaderValue = objPowsDBHeader
        wsPOS.MQSOAPHeaderValue = objPowsMQHeader

        'By AC - Changed to support multiple environment
        'wsPOS.Url = My.Settings(objDBHeader.EnvironmentUse & "_CRS_POSWS_POSWS")
        wsPOS.Url = Utility.Utility.GetWebServiceURL("POSWS", objDBHeader, objMQQueHeader)
        'By AC - Changed to support multiple environment

        wsPOS.Credentials = System.Net.CredentialCache.DefaultCredentials
        wsPOS.Timeout = My.Settings.WStIMEOUT

        intCP = 4

        ' Get policy currency and exchange rate
        If objDBHeader.EnvironmentUse = "PRD01" Then
            strUser = "ITOPER"
        Else
            strUser = objDBHeader.UserID
        End If

        strPoCurr = ""

        strSQL = "Select PolicyCurrency" & _
            " from Policyaccount " & _
            " where policyaccountid = '" & strPolicy & "'"
        If Exec(strSQL, "CIW", dtData, strErr) Then
            If dtData.Rows.Count > 0 Then
                strPoCurr = dtData.Rows(0).Item("PolicyCurrency")
            Else
                strErr = "Error: Get Policy currency" & intCP
                Exit Function
            End If
        Else
            strErr = "Error: Get Policy currency" & objCIW.RecentErrorMessage & intCP
            Exit Function
        End If

        intCP = 5

        ' Get Fund Allocation
        Select Case strLoadType
            Case "Fund Allocation", "ULA"
                Try
                    If wsPOS.GetFundAllocRecord(dsSWInMain, lngTx, strErr) = False Then
                        Exit Function
                    End If

                    intCP = 6

                    If dsSWInMain.Tables.Count = 0 Then
                        Exit Function
                    End If

                    dtFundAlloc = New DataTable("FundAlloc")
                    strFundListO = ""

                    dtFundAlloc.Columns.Add("FundCode", GetType(System.String))
                    dtFundAlloc.Columns.Add("FundName", GetType(System.String))
                    dtFundAlloc.Columns.Add("Allocation", GetType(System.String))

                    For i As Integer = 1 To 10
                        With dsSWInMain.Tables(0).Rows(0)
                            If Trim(.Item("posfh_fund" & i)) <> "" Then
                                Dim dr As DataRow
                                dr = dtFundAlloc.NewRow
                                dr.Item("Fundcode") = Trim(.Item("posfh_fund" & i))
                                dr.Item("FundName") = ""
                                dr.Item("Allocation") = .Item("posfh_percent" & i)
                                dtFundAlloc.Rows.Add(dr)
                                strFundListO = strFundListO & "'" & Trim(.Item("posfh_fund" & i)) & "',"
                            Else
                                Exit For
                            End If
                        End With
                    Next
                    dtFundAlloc.AcceptChanges()
                    If dtFundAlloc.Rows.Count > 0 Then strFundListO = Left(strFundListO, Len(strFundListO) - 1)

                    strSQL = "Select mpfinv_code, mpfinv_chi_desc, mpfinv_curr " & _
                                " from cswvw_mpf_investment " & _
                                " where mpfinv_code in (" & strFundListO & ")"

                    If Exec(strSQL, "CIW", dtData, strErr) Then
                        If dtData.Rows.Count > 0 Then
                            For Each dr As DataRow In dtFundAlloc.Rows
                                dtData.DefaultView.RowFilter = "mpfinv_code = '" & Trim(dr.Item("FundCode")) & "'"
                                dr.Item("FundName") = dtData.DefaultView.Item(0).Item("mpfinv_chi_desc")
                            Next
                            dtFundAlloc.AcceptChanges()
                            dsReceData.Tables.Add(dtFundAlloc)
                            GetFundTxHist = True
                        Else
                            strErr = "Error: Get Fund name" & intCP
                            Exit Function
                        End If
                    Else
                        strErr = "Error: Get Fund name" & intCP
                        Exit Function
                    End If

                Catch ex As Exception
                    strErr = strErr & vbCrLf & ex.ToString & intCP
                Finally
                    If Not IsNothing(wsPOS) Then
                        wsPOS.Dispose()
                    End If
                End Try

            Case "Fund Switching", "ULB", "Partial Surrender", "ULS"

                ' Get LA transaction ID, if any
                strSQL = "select postq_LAPTRANNO from pos_trans_queue where postq_trans_id = " & lngTx

                Select Case strLoadType
                    Case "Fund Switching", "ULB"
                        strSQL &= " and postq_trans_type = 'ULB'"
                    Case "Partial Surrender", "ULS"
                        strSQL &= " and postq_trans_type = 'ULS'"
                End Select

                If Exec(strSQL, "POS", dtData, strErr) Then
                    If dtData.Rows.Count > 0 Then
                        strLATXID = dtData.Rows(0).Item("postq_LAPTRANNO")
                    End If
                End If

                ' Get transaction header and details (Fund Switching)
                Try
                    ' Build switch-in records
                    If strLoadType = "ULB" Or strLoadType = "Fund Switching" Then
                        If wsPOS.GetSWInMainRecord(dsSWInMain, lngTx, strErr) = False Then
                            Exit Function
                        Else
                            With dsSWInMain.Tables(0).Rows(0)
                                lngSWOutID = .Item("possm_switchout_id")
                                dteTxDate = .Item("possm_eff_date")
                                dblAllocAmt = .Item("possm_booster_amt")

                                dtSwitchIn = New DataTable("SwitchIn")
                                dtSwitchIn.Columns.Add("FundCode", GetType(System.String))
                                dtSwitchIn.Columns.Add("FundName", GetType(System.String))
                                dtSwitchIn.Columns.Add("Allocation", GetType(System.String))
                                dtSwitchIn.Columns.Add("Currency", GetType(System.String))
                                dtSwitchIn.Columns.Add("UnitPrice", GetType(System.String))
                                dtSwitchIn.Columns.Add("ValuDate", GetType(System.String))
                                dtSwitchIn.Columns.Add("NoUnits", GetType(System.String))
                                dtSwitchIn.Columns.Add("Total", GetType(System.String))
                                dtSwitchIn.Columns.Add("Total_PO", GetType(System.String))
                                dtSwitchIn.Columns.Add("EXCHR", GetType(System.String))

                                For i As Integer = 1 To 10
                                    If Trim(.Item("possm_fund" & i)) <> "" Then
                                        Dim dr As DataRow
                                        dr = dtSwitchIn.NewRow
                                        dr.Item("Fundcode") = Trim(.Item("possm_fund" & i))
                                        dr.Item("Allocation") = Trim(.Item("possm_Alloc" & i))
                                        dtSwitchIn.Rows.Add(dr)
                                        strFundListI = strFundListI & "'" & Trim(.Item("possm_fund" & i)) & "',"
                                    Else
                                        Exit For
                                    End If
                                Next
                                strFundListI = Left(strFundListI, Len(strFundListI) - 1)
                            End With
                        End If
                    End If 'switch in

                    intCP = 7

                    ' Build Switch out records
                    If strLA = "LA" Then

                        If (strLoadType = "Partial Surrender" Or strLoadType = "ULS") Then lngSWOutID = lngTx

                        ' Life/Asia transaction records
                        dsParSurHist.Tables.Clear()
                        If wsPOS.GetAllParSurrRecord(dsParSurHist, lngSWOutID, strErr) = False Then
                            Exit Function
                        End If
                        If dsParSurHist.Tables.Count = 0 Then
                            strErr = "Error: No record return from GetAllParSurrRecord" & intCP
                            Exit Function
                        Else
                            If dsParSurHist.Tables(0).Rows.Count > 0 Then
                                dteTxDate = dsParSurHist.Tables(0).Rows(0).Item("posph_eff_date")
                                dtData = dsParSurHist.Tables(0)
                            Else
                                Exit Function
                            End If
                        End If
                    Else
                        ' CAPSIL transaction records
                        Try
                            objDBHeader.ConnectionAlias = objDBHeader.CompanyID & "POS" & objDBHeader.EnvironmentUse

                            If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then
                                strSQL = "select * " & _
                                    " From pos_parsur_hist " & _
                                    " Where posph_trans_id = " & lngSWOutID

                                If objCIW.ExecQuery(strSQL, dtData) Then
                                    If dtData.Rows.Count > 0 Then
                                    Else
                                        strErr = "Error: Get CAPSIL pos_parsur_hist" & intCP
                                        Exit Function
                                    End If
                                Else
                                    strErr = "Error: Get CAPSIL pos_parsur_hist" & intCP
                                    Exit Function
                                End If
                            Else
                                strErr = objCIW.RecentErrorMessage & intCP
                                Exit Function
                            End If
                        Finally
                            DisconnectDB(objCIW)
                        End Try
                    End If 'switch out

                    dtSwitchOut = New DataTable("SwitchOut")
                    dtSwitchOut.Columns.Add("pospf_fund", GetType(System.String))
                    dtSwitchOut.Columns.Add("FundName", GetType(System.String))     'Fund Name
                    dtSwitchOut.Columns.Add("pospf_sur_unit", GetType(System.String))
                    dtSwitchOut.Columns.Add("UnitHold", GetType(System.Double))     'Unit Hold
                    dtSwitchOut.Columns.Add("SWOUnits", GetType(System.Double))     'Switch out units
                    dtSwitchOut.Columns.Add("Currency", GetType(System.String))     'Currency
                    dtSwitchOut.Columns.Add("UnitPrice", GetType(System.String))    'Unit Price
                    dtSwitchOut.Columns.Add("ValuDate", GetType(System.String))   'Valuation Date
                    dtSwitchOut.Columns.Add("Total", GetType(System.String))        'Total
                    dtSwitchOut.Columns.Add("Total_PO", GetType(System.String))
                    dtSwitchOut.Columns.Add("EXCHR", GetType(System.String))

                    intCP = 8

                    If strLA = "LA" Then
                        'For Each dr As DataRow In dtSwitchOut.Rows
                        ' Return from BO
                        For Each dr As DataRow In dtData.Rows
                            If dr.Item("pospf_sur_unit") <> 0 Then
                                strFundListO = strFundListO & "'" & Trim(dr.Item("pospf_fund")) & "',"

                                Dim dr1 As DataRow
                                dr1 = dtSwitchOut.NewRow
                                dr1.Item("pospf_fund") = dr.Item("pospf_fund")
                                dr1.Item("pospf_sur_unit") = Math.Round(dr.Item("pospf_sur_unit"), 0, MidpointRounding.ToEven)
                                dtSwitchOut.Rows.Add(dr1)
                            End If
                        Next
                    Else
                        ' Return from SQL
                        For i As Integer = 1 To 10
                            If Trim(dtData.Rows(0).Item("posph_fund" & i)) <> "" Then
                                Dim dr As DataRow
                                dr = dtSwitchOut.NewRow
                                dr.Item("pospf_fund") = Trim(dtData.Rows(0).Item("posph_fund" & i))
                                If dr.Item("pospf_unit_bal") <> 0 Then
                                    dr.Item("pospf_sur_unit") = Math.Round(dtData.Rows(0).Item("posph_sur_unit" & i) / dtData.Rows(0).Item("posph_unit_bal" & i) * 100, 0, MidpointRounding.ToEven)
                                Else
                                    dr.Item("pospf_sur_unit") = "N/A"
                                End If
                                dtSwitchOut.Rows.Add(dr)
                                strFundListO = strFundListO & "'" & Trim(dtData.Rows(0).Item("posph_fund" & i)) & "',"
                            Else
                                Exit For
                            End If
                        Next
                    End If

                    strFundListO = Left(strFundListO, Len(strFundListO) - 1)

                    intCP = 9

                    ' Find fund inforamtion
                    strSQL = "Select f1.cswfdb_fund_code, f1.cswfdb_cal_date, f1.cswfdb_unt_hld, mpfinv_chi_desc, mpfinv_curr, mpfval_unit_price, mpfval_to_date " & _
                        " from csw_fund_balance f1, " & _
                        " (select cswfdb_fund_code, max(cswfdb_cal_date) as cswfdb_cal_date from csw_fund_balance " & _
                        " where cswfdb_pono = '" & strPolicy & "' and cswfdb_cal_date <= '" & Format(dteTxDate, "MM/dd/yyyy") & "' " & _
                        " group by cswfdb_fund_code) f2, cswvw_mpf_investment LEFT JOIN cswvw_mpf_valuation v1 " & _
                        " ON mpfval_invest_fund = mpfinv_code " & _
                        " and mpfval_to_date = (select min(mpfval_to_date) from cswvw_mpf_valuation v2 " & _
                        " where v2.mpfval_invest_fund = v1.mpfval_invest_fund and v2.mpfval_to_date > '" & Format(dteTxDate, "MM/dd/yyyy") & "') " & _
                        " where f1.cswfdb_pono = '" & strPolicy & "' " & _
                        " and f1.cswfdb_fund_code = f2.cswfdb_fund_code " & _
                        " and f1.cswfdb_cal_date = f2.cswfdb_cal_date " & _
                        " and f1.cswfdb_fund_code = mpfinv_code " & _
                        " and mpfinv_code in (" & strFundListO & ")"

                    intCP = 10

                    If Exec(strSQL, "CIW", dtData, strErr) Then
                        If dtData.Rows.Count > 0 Then

                            ' Append to the switchout datatable
                            For Each dr As DataRow In dtSwitchOut.Rows
                                dtData.DefaultView.RowFilter = "cswfdb_fund_code = '" & Trim(dr.Item("pospf_fund")) & "'"

                                dr.Item("FundName") = dtData.DefaultView.Item(0).Item("mpfinv_chi_desc")
                                dr.Item("UnitHold") = dtData.DefaultView.Item(0).Item("cswfdb_unt_hld")
                                If IsNumeric(dr.Item("pospf_sur_unit")) Then
                                    dr.Item("SWOUnits") = Math.Round(dtData.DefaultView.Item(0).Item("cswfdb_unt_hld") * dr.Item("pospf_sur_unit") / 100, 5, MidpointRounding.ToEven)
                                Else
                                    dr.Item("SWOUnits") = 0
                                End If

                                dr.Item("Currency") = dtData.DefaultView.Item(0).Item("mpfinv_curr")

                                If IsDBNull(dtData.DefaultView.Item(0).Item("mpfval_unit_price")) Then
                                    dr.Item("UnitPrice") = "N/A"
                                    dr.Item("ValuDate") = "N/A"
                                    dr.Item("Total") = "N/A"
                                    dr.Item("Total_PO") = "N/A"
                                    dr.Item("EXCHR") = "N/A"
                                Else
                                    dr.Item("UnitPrice") = dtData.DefaultView.Item(0).Item("mpfval_unit_price")
                                    dr.Item("ValuDate") = Format(dtData.DefaultView.Item(0).Item("mpfval_to_date"), "MM/dd/yyyy")
                                    dr.Item("Total") = Math.Round((dr.Item("UnitHold") * dr.Item("pospf_sur_unit") / 100 * dtData.DefaultView.Item(0).Item("mpfval_unit_price")) - 0.005, 2)
                                End If

                                intCP = 11

                                Dim dsRate As DataSet

                                dsRate = New DataSet
                                If dr.Item("ValuDate") <> "N/A" Then
                                    If Trim(dr.Item("Currency")) <> strPoCurr Then
                                        Try
                                            If wsPOS.GetExchangeRate(strUser, dr.Item("Currency"), strPoCurr, dsRate, strExErr, CDate(dtData.DefaultView.Item(0).Item("mpfval_to_date")), "BOK", "") Then
                                                dblExRate = CDbl(dsRate.Tables(0).Rows(0).Item("rlfcxr_sell_rate") & "")
                                            Else
                                                strErr = strExErr
                                            End If
                                        Catch ex As Exception
                                            strErr = strErr & vbCrLf & ex.ToString
                                        End Try

                                        dr.Item("Total_PO") = Math.Round(dr.Item("Total") * dblExRate - 0.005, 2)
                                        dr.Item("EXCHR") = dblExRate
                                    Else
                                        dr.Item("Total_PO") = dr.Item("Total")
                                        dr.Item("EXCHR") = "1.00000000"
                                    End If
                                    dblTotal += dr.Item("Total_PO")
                                Else
                                    blnHasNA = True
                                End If
                            Next

                            Dim drTtl As DataRow = dtSwitchOut.NewRow
                            If blnHasNA = True Then
                                drTtl.Item("Total_PO") = "N/A"
                            Else
                                drTtl.Item("Total_PO") = dblTotal
                            End If
                            drTtl.Item("Total") = "TOTAL:"
                            dtSwitchOut.Rows.Add(drTtl)

                            dtSwitchOut.AcceptChanges()
                            dsReceData.Tables.Add(dtSwitchOut)

                        Else
                            strErr = "Error: Get Fund Price" & intCP
                            Exit Function
                        End If
                    Else
                        strErr = "Error: Get Fund Price" & intCP
                        Exit Function
                    End If

                    intCP = 12

                    ' Get unit trans history
                    If strLoadType = "ULB" Or strLoadType = "Fund Switching" Or strLoadType = "ULS" Or strLoadType = "Partial Surrender" Then
                        Dim dsUnitTrans As New DataSet
                        Dim strTradeDate As String = ""

                        If strLA = "LA" Then
                            ' Call BO to get switch in info.

                            Dim dsSend As New DataSet
                            Dim dtSendData As New DataTable
                            Dim strMsg As String = ""

                            dtSendData.Columns.Add("StartRecNo", Type.GetType("System.String"))
                            dtSendData.Columns.Add("PolicyNo", Type.GetType("System.String"))
                            dtSendData.Columns.Add("FromDate", Type.GetType("System.String"))
                            dtSendData.Columns.Add("ToDate", Type.GetType("System.String"))
                            dtSendData.Columns.Add("TransCode", Type.GetType("System.String"))

                            Dim dr As DataRow = dtSendData.NewRow()

                            dr("StartRecNo") = "00000"
                            dr("PolicyNo") = strPolicy
                            dr("FromDate") = dteTxDate
                            dr("ToDate") = Today

                            If (strLoadType = "Partial Surrender" Or strLoadType = "ULS") Then
                                strTransCode = "T510"
                            Else
                                strTransCode = "T676"
                            End If
                            dr("TransCode") = strTransCode
                            dtSendData.Rows.Add(dr)
                            dsSend.Tables.Add(dtSendData)

                            If wsPOS.GetUnitTran(dsSend, dsUnitTrans, strMsg, strErr) = False Then
                                Exit Function
                            End If

                            ''Dim wsNBA As New NBAWS.NBAWS
                            ''Dim objDbSoapHeader As New NBAWS.ComSOAPHeader

                            ' ''Set Soap Header
                            ''objDbSoapHeader.ProjectAlias = objDBHeader.ProjectAlias
                            ''objDbSoapHeader.MachineID = objDBHeader.MachineID
                            ''objDbSoapHeader.UserID = objDBHeader.UserID
                            ''objDbSoapHeader.UserType = objDBHeader.UserType
                            ''objDbSoapHeader.VersionNo = objDBHeader.VersionNo
                            ''objDbSoapHeader.CompanyID = objDBHeader.CompanyID
                            ''objDbSoapHeader.EnvironmentUse = objDBHeader.EnvironmentUse

                            ''wsNBA.Url = My.Settings(objDBHeader.EnvironmentUse & "_CRS_NBAWS_NBAWS")
                            ' ''wsNBA.Url = "http://localhost:30817/NBAWS/NBAWS.asmx"

                            ''wsNBA.Credentials = System.Net.CredentialCache.DefaultCredentials
                            ''wsNBA.ComSOAPHeaderValue = objDbSoapHeader
                            ''Dim dt As New DataTable
                            ''dsUnitTrans.Tables.Add(dt)
                            ''If wsNBA.GetUnitTranHistory(strPolicy, dsUnitTrans, strErr, dteTxDate, Today) = False Then
                            ''    Exit Function
                            ''End If

                            intCP = 13

                        Else
                            ' CAPSIL transaction, get from csw_unit_tran
                            Try
                                objDBHeader.ConnectionAlias = objDBHeader.CompanyID & "CIW" & objDBHeader.EnvironmentUse

                                If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then
                                    strSQL = "Select cswuth_fund_code as UTRFCD, cswuth_cal_tr_date as UTRTRD, cswuth_amount as UTRAMT, cswuth_unit as UTRUNO " & _
                                        " from csw_unit_tran where cswuth_poli_no = '" & strPolicy & "' and cswuth_pay_type = 'FSW' and cswuth_amount > 0 " & _
                                        " and cswuth_cal_tr_date between '" & dteTxDate & "' and '" & DateAdd(DateInterval.Month, 1, dteTxDate) & "'"

                                    If objCIW.ExecQuery(strSQL, dtData) Then
                                        If dtData.Rows.Count > 0 Then
                                        Else
                                            strErr = "Error: Get CAPSIL csw_unit_tran" & intCP
                                            Exit Function
                                        End If
                                    Else
                                        strErr = "Error: Get CAPSIL csw_unit_tran" & intCP
                                        Exit Function
                                    End If
                                Else
                                    strErr = objCIW.RecentErrorMessage & intCP
                                    Exit Function
                                End If
                            Catch ex As Exception
                                strErr = strErr & vbCrLf & ex.ToString & intCP
                            Finally
                                DisconnectDB(objCIW)
                            End Try
                        End If

                        intCP = 14

                        With dsUnitTrans.Tables(1)
                            ' Unit Tran record found
                            If .Rows.Count > 0 Then

                                ' Find switch-out records
                                ''.DefaultView.RowFilter = "UTRPAY = 'Switch in/out'" & _
                                ''    "and UTRAMT < 0 and UTRTRD >= '" & CInt(Format(dteTxDate, "yyyyMMdd") - 18000000) & "' "
                                'and CompletedInd = 'Y'
                                .DefaultView.RowFilter = "TransCode = '" & strTransCode & "' and DeemedUnit < 0 and MoniesDate >= '" & Format(dteTxDate, "yyyyMMdd") & "'"

                                ' **** ES01 begin ****
                                ' Concurrent Fund Trade, filter by transaction ID as well
                                If strLATXID <> "" Then
                                    .DefaultView.RowFilter &= " and TranNo = " & strLATXID
                                End If
                                ' **** ES01 end ****

                                '.DefaultView.Sort = "UTRTRD ASC"
                                .DefaultView.Sort = "TransDate ASC"

                                dblTotal = 0
                                strTradeDate = ""

                                If .DefaultView.Count > 0 Then

                                    'strTradeDate = .DefaultView.Item(0).Item("UTRTRD")
                                    strTradeDate = .DefaultView.Item(0).Item("MoniesDate")

                                    ' Fill switch out amount for each funds
                                    For Each dr As DataRow In dtSwitchOut.Rows
                                        If Not IsDBNull(dr.Item("pospf_fund")) Then
                                            '.DefaultView.RowFilter = "UTRPAY = 'Switch in/out' and UTRAMT < 0 and UTRTRD = '" & strTradeDate & "' and UTRFCD = '" & dr.Item("pospf_fund") & "'"
                                            'and CompletedInd = 'Y'
                                            .DefaultView.RowFilter = "TransCode = '" & strTransCode & "' and DeemedUnit  < 0 and MoniesDate = '" & strTradeDate & "' and Fund = '" & dr.Item("pospf_fund") & "'"

                                            ' **** ES01 begin ****
                                            ' Concurrent Fund Trade, filter by transaction ID as well
                                            If strLATXID <> "" Then
                                                .DefaultView.RowFilter &= " and TranNo = " & strLATXID
                                            End If
                                            ' **** ES01 end ****

                                            dr.Item("SWOUnits") = 0

                                            If dr.Item("Total") <> "N/A" Then
                                                dr.Item("Total") = 0
                                            End If

                                            For i As Integer = 0 To .DefaultView.Count - 1
                                                dr.Item("SWOUnits") += Math.Abs(.DefaultView.Item(i).Item("deemedunit"))

                                                If dr.Item("Total") <> "N/A" Then
                                                    dr.Item("Total") += Math.Abs(.DefaultView.Item(i).Item("FundAmt"))
                                                End If
                                            Next

                                            dr.Item("SWOUnits") = Math.Round(dr.Item("SWOUnits"), 6)

                                            If dr.Item("Total") <> "N/A" Then
                                                If Trim(dr.Item("Currency")) <> strPoCurr Then
                                                    dr.Item("Total_PO") = Math.Round(dr.Item("Total") * dr.Item("EXCHR") - 0.005, 2)
                                                Else
                                                    dr.Item("Total_PO") = dr.Item("Total")
                                                End If
                                                dblTotal += dr.Item("Total_PO")
                                            End If

                                            ''If .DefaultView.Count > 0 Then
                                            ''    'dr.Item("SWOUnits") = Math.Abs(.DefaultView.Item(0).Item("UTRUNO"))
                                            ''    'dr.Item("Total") = Math.Abs(.DefaultView.Item(0).Item("UTRAMT"))
                                            ''    dr.Item("SWOUnits") = Math.Abs(.DefaultView.Item(0).Item("deemedunit"))
                                            ''    dr.Item("Total") = Math.Abs(.DefaultView.Item(0).Item("FundAmt"))
                                            ''    If Trim(dr.Item("Currency")) <> strPoCurr Then
                                            ''        dr.Item("Total_PO") = Math.Round(dr.Item("Total") * dr.Item("EXCHR") - 0.005, 2)
                                            ''    Else
                                            ''        dr.Item("Total_PO") = dr.Item("Total")
                                            ''    End If
                                            ''    dblTotal += dr.Item("Total_PO")
                                            ''End If
                                        End If
                                    Next

                                    ' Update total
                                    If blnHasNA = False Then
                                        dtSwitchOut.Rows(dtSwitchOut.Rows.Count - 1).Item("Total_PO") = dblTotal
                                    End If

                                    dtSwitchOut.AcceptChanges()
                                End If

                                intCP = 15

                                If strLoadType = "ULB" Or strLoadType = "Fund Switching" Then
                                    ' Find latest switch-in records
                                    ''.DefaultView.RowFilter = "UTRPAY = 'Switch in/out'" & _
                                    ''    "and UTRAMT > 0 and UTRTRD >= '" & CInt(Format(dteTxDate, "yyyyMMdd") - 18000000) & "' "
                                    .DefaultView.RowFilter = "TransCode = '" & strTransCode & "' and DeemedUnit > 0 and MoniesDate >= '" & Format(dteTxDate, "yyyyMMdd") & "'"

                                    ' **** ES01 begin ****
                                    ' Concurrent Fund Trade, filter by transaction ID as well
                                    If strLATXID <> "" Then
                                        .DefaultView.RowFilter &= " and TranNo = " & strLATXID
                                    End If
                                    ' **** ES01 end ****

                                    '.DefaultView.Sort = "UTRTRD ASC"
                                    .DefaultView.Sort = "TransDate ASC"

                                    strTradeDate = ""
                                    ' Switch-in record found
                                    If .DefaultView.Count > 0 Then
                                        strTradeDate = .DefaultView.Item(0).Item("MoniesDate")
                                        '.DefaultView.RowFilter = "UTRPAY = 'Switch in/out' and UTRAMT > 0 and UTRTRD = '" & strTradeDate & "' "
                                        .DefaultView.RowFilter = "TransCode = '" & strTransCode & "' and DeemedUnit  > 0 and MoniesDate = '" & strTradeDate & "'"

                                        ' **** ES01 begin ****
                                        ' Concurrent Fund Trade, filter by transaction ID as well
                                        If strLATXID <> "" Then
                                            .DefaultView.RowFilter &= " and TranNo = " & strLATXID
                                        End If
                                        ' **** ES01 end ****

                                        .DefaultView.Sort = "Fund"

                                        ' Get Fund name & price
                                        strSQL = "Select mpfval_invest_fund, mpfinv_chi_desc, mpfval_unit_price, mpfval_to_date, mpfinv_curr " & _
                                            " from cswvw_mpf_investment, cswvw_mpf_valuation v1 " & _
                                            " where mpfinv_code = mpfval_invest_fund " & _
                                            " and mpfval_to_date = (select min(mpfval_to_date) from cswvw_mpf_valuation v2 " & _
                                            " where v2.mpfval_invest_fund = v1.mpfval_invest_fund and v2.mpfval_to_date > '" & strTradeDate.Trim & "') " & _
                                            " and mpfinv_code in (" & strFundListI & ")"

                                        If Exec(strSQL, "CIW", dtData, strErr) Then
                                            If dtData.Rows.Count > 0 Then
                                            Else
                                                strErr = "Error: Get Fund Price" & intCP
                                                Exit Function
                                            End If
                                        Else
                                            strErr = "Error: Get Fund Price" & intCP
                                            Exit Function
                                        End If

                                        intCP = 16

                                        For i As Integer = 0 To .DefaultView.Count - 1
                                            ''dtSwitchIn.DefaultView.RowFilter = "FundCode = '" & .DefaultView.Item(i).Item("UTRFCD") & "'"
                                            ''dtData.DefaultView.RowFilter = "mpfval_invest_fund = '" & .DefaultView.Item(i).Item("UTRFCD") & "'"

                                            ''dtSwitchIn.DefaultView.Item(0).Item("NoUnits") = .DefaultView.Item(i).Item("UTRUNO")
                                            ''dtSwitchIn.DefaultView.Item(0).Item("Total") = .DefaultView.Item(i).Item("UTRAMT")

                                            ''dtSwitchIn.DefaultView.Item(0).Item("Currency") = dtData.DefaultView.Item(0).Item("mpfinv_curr")
                                            ''dtSwitchIn.DefaultView.Item(0).Item("FundName") = dtData.DefaultView.Item(0).Item("mpfinv_chi_desc")
                                            ''dtSwitchIn.DefaultView.Item(0).Item("UnitPrice") = dtData.DefaultView.Item(0).Item("mpfval_unit_price")
                                            ''dtSwitchIn.DefaultView.Item(0).Item("ValuDate") = Format(dtData.DefaultView.Item(0).Item("mpfval_to_date"), "MM/dd/yyyy")

                                            dtSwitchIn.DefaultView.RowFilter = "FundCode = '" & .DefaultView.Item(i).Item("Fund") & "'"
                                            dtData.DefaultView.RowFilter = "mpfval_invest_fund = '" & .DefaultView.Item(i).Item("Fund") & "'"

                                            If IsDBNull(dtSwitchIn.DefaultView.Item(0).Item("NoUnits")) Then
                                                dtSwitchIn.DefaultView.Item(0).Item("NoUnits") = .DefaultView.Item(i).Item("deemedunit")
                                                dtSwitchIn.DefaultView.Item(0).Item("Total") = .DefaultView.Item(i).Item("FundAmt")
                                            Else
                                                dtSwitchIn.DefaultView.Item(0).Item("NoUnits") += .DefaultView.Item(i).Item("deemedunit")
                                                dtSwitchIn.DefaultView.Item(0).Item("Total") += .DefaultView.Item(i).Item("FundAmt")
                                            End If

                                            dtSwitchIn.DefaultView.Item(0).Item("Currency") = dtData.DefaultView.Item(0).Item("mpfinv_curr")
                                            dtSwitchIn.DefaultView.Item(0).Item("FundName") = dtData.DefaultView.Item(0).Item("mpfinv_chi_desc")
                                            dtSwitchIn.DefaultView.Item(0).Item("UnitPrice") = dtData.DefaultView.Item(0).Item("mpfval_unit_price")
                                            dtSwitchIn.DefaultView.Item(0).Item("ValuDate") = Format(dtData.DefaultView.Item(0).Item("mpfval_to_date"), "MM/dd/yyyy")

                                            'If dblAllocAmt > 0 Then
                                            '    dtSwitchIn.DefaultView.Item(0).Item("Allocation") = Math.Round(.DefaultView.Item(i).Item("UTRAMT") / dblAllocAmt * 100, 0, MidpointRounding.ToEven)
                                            'Else
                                            '    If .DefaultView.Count = 1 Then
                                            '        dtSwitchIn.DefaultView.Item(0).Item("Allocation") = "100"
                                            '    End If
                                            'End If

                                            Dim dsRate As New DataSet
                                            If dtSwitchIn.DefaultView.Item(0).Item("ValuDate") <> "N/A" Then
                                                If Trim(dtSwitchIn.DefaultView.Item(0).Item("Currency")) <> strPoCurr Then
                                                    If wsPOS.GetExchangeRate(strUser, dtSwitchIn.DefaultView.Item(0).Item("Currency"), strPoCurr, dsRate, strExErr, dtData.DefaultView.Item(0).Item("mpfval_to_date"), "BOK", "") Then
                                                        dblExRate = dsRate.Tables(0).Rows(0).Item("rlfcxr_sell_rate")
                                                    Else
                                                        strErr = strExErr
                                                    End If
                                                    dtSwitchIn.DefaultView.Item(0).Item("Total_PO") = Math.Round(dtSwitchIn.DefaultView.Item(0).Item("Total") * dblExRate - 0.005, 2)
                                                    dtSwitchIn.DefaultView.Item(0).Item("EXCHR") = dblExRate
                                                Else
                                                    dtSwitchIn.DefaultView.Item(0).Item("Total_PO") = dtSwitchIn.DefaultView.Item(0).Item("Total")
                                                    dtSwitchIn.DefaultView.Item(0).Item("EXCHR") = "1.00000000"
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                            intCP = 17

                            ' Switch-in not complete yet
                            If strTradeDate = "" AndAlso strFundListI <> "" Then
                                ' Get Fund name & price
                                strSQL = "Select mpfinv_code, mpfinv_chi_desc, mpfinv_curr " & _
                                    " from cswvw_mpf_investment " & _
                                    " where mpfinv_code in (" & strFundListI & ")"

                                If Exec(strSQL, "CIW", dtData, strErr) Then
                                    If dtData.Rows.Count > 0 Then
                                    Else
                                        strErr = "Error: Get Fund information" & intCP
                                        Exit Function
                                    End If
                                Else
                                    strErr = "Error: Get Fund information" & intCP
                                    Exit Function
                                End If

                                For Each dr As DataRow In dtSwitchIn.Rows
                                    dtData.DefaultView.RowFilter = "mpfinv_code = '" & dr.Item("FundCode") & "'"
                                    dr.Item("Currency") = dtData.DefaultView.Item(0).Item("mpfinv_curr")
                                    dr.Item("FundName") = dtData.DefaultView.Item(0).Item("mpfinv_chi_desc")
                                    dr.Item("NoUnits") = "N/A"
                                    dr.Item("Total") = "N/A"
                                    dr.Item("UnitPrice") = "N/A"
                                    dr.Item("ValuDate") = "N/A"
                                    dr.Item("Total_PO") = "N/A"
                                    dr.Item("EXCHR") = "N/A"
                                    'dr.Item("Allocation") = "N/A"
                                Next

                            End If ' Count > 0

                            intCP = 18

                            If strLoadType = "ULB" Or strLoadType = "Fund Switching" Then
                                dtSwitchIn.AcceptChanges()
                                dsReceData.Tables.Add(dtSwitchIn)
                            End If

                        End With
                    End If

                    GetFundTxHist = True

                Catch ex As Exception
                    strErr = strErr & vbCrLf & ex.ToString & intCP
                Finally
                    If Not IsNothing(wsPOS) Then
                        wsPOS.Dispose()
                    End If
                End Try
        End Select

    End Function

    Private Function Exec(ByVal strSQL As String, ByVal strDB As String, ByRef dtData As DataTable, ByRef strErr As String) As Boolean

        Exec = False

        Try
            objDBHeader.ConnectionAlias = objDBHeader.CompanyID & strDB & objDBHeader.EnvironmentUse

            If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then
                If objCIW.ExecQuery(strSQL, dtData) Then
                    Exec = True
                Else
                    strErr = "(Exec) SQL Error: " & objCIW.RecentErrorMessage
                    Exit Function
                End If
            Else
                strErr = "(Exec) Connect Error: " & objCIW.RecentErrorMessage
                Exit Function
            End If
        Catch ex As Exception
            strErr = "(Exec) Exception: " & ex.ToString
        Finally
            DisconnectDB(objCIW)
        End Try
    End Function

    Private Function Exec(ByVal cmd As SqlClient.SqlCommand, ByVal strDB As String, ByRef dtData As DataTable, ByRef strErr As String) As Boolean

        Exec = False

        Try
            objDBHeader.ConnectionAlias = objDBHeader.CompanyID & strDB & objDBHeader.EnvironmentUse

            If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then
                If objCIW.ExecQuery(cmd, dtData) Then
                    Exec = True
                Else
                    strErr = "(Exec) SQL Error: " & objCIW.RecentErrorMessage
                    Exit Function
                End If
            Else
                strErr = "(Exec) Connect Error: " & objCIW.RecentErrorMessage
                Exit Function
            End If
        Catch ex As Exception
            strErr = "(Exec) Exception: " & ex.ToString
        Finally
            DisconnectDB(objCIW)
        End Try
    End Function

    Private Function ExecNonQuery(ByVal strSQL As String, ByVal strDB As String, ByRef strErr As String) As Boolean

        Try
            objDBHeader.ConnectionAlias = objDBHeader.CompanyID & strDB & objDBHeader.EnvironmentUse

            If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then
                If objCIW.ExecNonQuery(strSQL, 0) Then
                    Return True
                Else
                    strErr = "(Exec) SQL Error: " & objCIW.RecentErrorMessage
                    Return False
                End If
            Else
                strErr = "(Exec) Connect Error: " & objCIW.RecentErrorMessage
                Return False
            End If
        Catch ex As Exception
            strErr = "(Exec) Exception: " & ex.ToString
            Return False
        Finally
            DisconnectDB(objCIW)
        End Try
    End Function

    Private Function ExecNonQuerys(ByVal strSQL() As String, ByVal strDB As String, ByRef strErr As String) As Boolean

        Try
            objDBHeader.ConnectionAlias = objDBHeader.CompanyID & strDB & objDBHeader.EnvironmentUse

            If Me.ConnectDB(objCIW, objDBHeader.ProjectAlias, objDBHeader.ConnectionAlias, objDBHeader.UserType, strErr) Then

                objCIW.TransactionBegin()

                For Each sql As String In strSQL
                    If Not objCIW.ExecNonQuery(sql, 0) Then
                        Throw New Exception(objCIW.RecentErrorMessage)
                    End If
                Next

                objCIW.TransactionCommit()
                Return True
            Else
                strErr = "(Exec) Connect Error: " & objCIW.RecentErrorMessage
                Return False
            End If
        Catch ex As Exception
            strErr = "(Exec) Exception: " & ex.ToString
            objCIW.TransactionRollback()
            Return False
        Finally
            DisconnectDB(objCIW)
        End Try

    End Function

#End Region

    'ITSR1285 - ChopardC Start

    Public Function GetPoInfo(ByVal strPolicyNo As String, ByRef dsPoInfo As Data.DataSet, ByRef strErr As String) As Boolean

        Dim strUser As String
        Dim intCP As Integer

        intCP = 1

        ' Get policy currency and exchange rate
        If objDBHeader.EnvironmentUse = "PRD01" Then
            strUser = "ITOPER"
        Else
            strUser = objDBHeader.UserID
        End If

        intCP = 2
        Try
            Dim wsPOS As POSWS.POSWS
            Dim objPowsDBHeader As New POSWS.DBSOAPHeader
            Dim objPowsMQHeader As New POSWS.MQSOAPHeader

            ' Setup header
            SetupMQHeader(objPowsMQHeader, objMQQueHeader)
            SetupDBHeader(objPowsDBHeader, objDBHeader)

            wsPOS = New POSWS.POSWS
            wsPOS.DBSOAPHeaderValue = objPowsDBHeader
            wsPOS.MQSOAPHeaderValue = objPowsMQHeader

            intCP = 3
            wsPOS.Url = Utility.Utility.GetWebServiceURL("POSWS", objDBHeader, objMQQueHeader)
            wsPOS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsPOS.Timeout = My.Settings.WStIMEOUT

            intCP = 4
            If Not wsPOS.GetPoInfo(strPolicyNo, dsPoInfo, strErr) Then
                Throw New Exception(strErr)
                Return False
            End If

            Return True

        Catch ex As Exception
            MsgBox(strErr & vbCrLf & ex.ToString & intCP)
        Finally

        End Try

    End Function

    Public Function GetIAAccount(ByVal strPolicyNo As String, ByVal effDate As DateTime, ByRef dsFundIA As Data.DataSet, ByRef strCurr As String, ByRef strErr As String) As Boolean

        Dim strUser As String
        Dim intCP As Integer

        intCP = 1

        ' Get policy currency and exchange rate
        If objDBHeader.EnvironmentUse = "PRD01" Then
            strUser = "ITOPER"
        Else
            strUser = objDBHeader.UserID
        End If

        intCP = 2
        Try
            Dim wsPOS As POSWS.POSWS
            Dim objPowsDBHeader As New POSWS.DBSOAPHeader
            Dim objPowsMQHeader As New POSWS.MQSOAPHeader

            Dim dsSendData As New DataSet
            Dim dsReceData As New DataSet
            Dim dtSendDataCompList As New DataTable
            Dim dr As DataRow

            ' Setup header
            SetupMQHeader(objPowsMQHeader, objMQQueHeader)
            SetupDBHeader(objPowsDBHeader, objDBHeader)

            wsPOS = New POSWS.POSWS
            wsPOS.DBSOAPHeaderValue = objPowsDBHeader
            wsPOS.MQSOAPHeaderValue = objPowsMQHeader

            intCP = 3
            wsPOS.Url = Utility.Utility.GetWebServiceURL("POSWS", objDBHeader, objMQQueHeader)
            wsPOS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsPOS.Timeout = My.Settings.WStIMEOUT

            dtSendDataCompList.Columns.Add("PolicyNo", Type.GetType("System.String"))
            If objDBHeader.CompanyID = "LAC" Or objDBHeader.CompanyID = "LAH" Then
                dtSendDataCompList.Columns.Add("EffDate", Type.GetType("System.DateTime"))
            End If

            ' Get component list                
            dr = dtSendDataCompList.NewRow()
            dr.Item("PolicyNo") = strPolicyNo
            If objDBHeader.CompanyID = "LAC" Or objDBHeader.CompanyID = "LAH" Then
                dr.Item("EffDate") = effDate
            End If
            dtSendDataCompList.Rows.Add(dr)

            'Set DataTable(s) to Data Set
            dsSendData.Tables.Add(dtSendDataCompList)

            intCP = 4

            If String.IsNullOrEmpty(strCurr) Then
                If Not wsPOS.GetPolicyCurrency(strPolicyNo, strCurr, strErr) Then
                    Throw New Exception(strErr)
                End If

            End If

            intCP = 5
            If Not wsPOS.GetPolicyFundDetail(dsSendData, dsReceData, strErr) Then
                Throw New Exception(strErr)
            Else
                intCP = 6

                Dim intCnt As Integer = 0
                Dim aryAct(8, 0) As String
                Dim dtLAData As New DataTable
                Dim i As Integer

                If dsReceData.Tables(0).Rows(0)(0) > 0 Then
                    Dim strFund As String = String.Empty
                    Dim strCov As String = String.Empty
                    Dim strTempFund As String = String.Empty
                    Dim strTempCov As String = String.Empty
                    Dim strTempLifeNo As String = String.Empty
                    Dim strTempCovNo As String = String.Empty
                    Dim strTempRiderNo As String = String.Empty
                    Dim strTempUnitType As String = String.Empty

                    dsReceData.Tables(1).DefaultView.RowFilter = ""
                    dsReceData.Tables(1).DefaultView.Sort = "LifeNo, CovNo, RiderNo, Fund, UnitType"
                    dtLAData = dsReceData.Tables(1).DefaultView.ToTable

                    ' Grouping unit and DeemedUnit by FundCode and Coverage
                    For i = 0 To dtLAData.Rows.Count - 1
                        strTempFund = dtLAData.Rows(i)("Fund").ToString.Trim
                        strTempLifeNo = dtLAData.Rows(i)("LifeNo").ToString.Trim
                        strTempCovNo = dtLAData.Rows(i)("CovNo").ToString.Trim
                        strTempRiderNo = dtLAData.Rows(i)("RiderNo").ToString.Trim
                        strTempCov = strTempLifeNo & strTempCovNo & strTempRiderNo

                        If strFund <> strTempFund OrElse strCov <> strTempCov Then
                            strFund = strTempFund
                            strCov = strTempCov

                            intCnt = intCnt + 1
                            ReDim Preserve aryAct(8, intCnt)
                            aryAct(1, intCnt) = strTempFund
                            aryAct(2, intCnt) = 0
                            aryAct(3, intCnt) = 0
                            aryAct(4, intCnt) = 0
                            aryAct(5, intCnt) = 0
                            aryAct(6, intCnt) = strTempLifeNo
                            aryAct(7, intCnt) = strTempCovNo
                            aryAct(8, intCnt) = strTempRiderNo
                        End If

                        strTempUnitType = dtLAData.Rows(i)("UnitType").ToString.Trim

                        If strTempUnitType = "A" Then
                            aryAct(2, intCnt) = aryAct(2, intCnt) & dtLAData.Rows(i)("Unit").ToString.Trim
                            aryAct(3, intCnt) = aryAct(3, intCnt) & dtLAData.Rows(i)("DeemedUnit").ToString.Trim
                        ElseIf strTempUnitType = "I" Then
                            aryAct(4, intCnt) = aryAct(4, intCnt) + dtLAData.Rows(i)("Unit").ToString.Trim
                            aryAct(5, intCnt) = aryAct(5, intCnt) + dtLAData.Rows(i)("DeemedUnit").ToString.Trim
                        End If
                    Next


                    ' Get csw_ia_account_balance table schema
                    intCP = 7

                    Dim dsSchema As New DataSet
                    Dim dsFundInfo As New DataSet
                    Dim dtFundInfo As New DataTable
                    Dim dtIAAcc As New DataTable
                    Dim drIAAcc As DataRow

                    If Not wsPOS.GetIAAccountTableSchema(dsSchema, strErr) Then
                        intCP = 8
                        strErr = strErr & vbCrLf & " GetIAAccountTableSchema " & intCP
                        Throw New Exception(strErr)
                    Else
                        intCP = 9

                        If dsSchema.Tables.Count > 0 Then
                            dtIAAcc = dsSchema.Tables(0).Clone()

                            For i = 1 To intCnt
                                Dim strLife, strCoverage, strRider, strFundCode, strFundCurr, strPoCurr As String
                                Dim dblExRate, dblIUnit, dblAUnit, dblDeemedIUnit, dblDeemedAUnit, dblUnitPrice As Double
                                Dim dValToDate As DateTime


                                drIAAcc = dtIAAcc.NewRow()

                                strFundCode = aryAct(1, i)
                                dblIUnit = aryAct(4, i)
                                dblDeemedIUnit = aryAct(5, i)
                                dblAUnit = aryAct(2, i)
                                dblDeemedAUnit = aryAct(3, i)

                                ' GetFundInfo
                                If Not wsPOS.GetFundInfo(strFundCode, dsFundInfo, strErr) Then
                                    intCP = 10
                                    strErr = strErr & vbCrLf & " GetFundInfo " & intCP
                                    Throw New Exception(strErr)
                                End If

                                dtFundInfo = dsFundInfo.Tables(i - 1).Copy

                                If dtFundInfo.Rows.Count = 0 Then
                                    Continue For
                                End If

                                strPoCurr = strCurr
                                strFundCurr = dtFundInfo.Rows(0)("mpfinv_curr").ToString.Trim

                                dblUnitPrice = dtFundInfo.Rows(0)("mpfval_unit_price")
                                dValToDate = CDate(dtFundInfo.Rows(0)("mpfval_to_date"))

                                intCP = 11

                                If strFundCurr <> strPoCurr Then
                                    Try
                                        Dim dsRate As New DataSet

                                        If wsPOS.GetExchangeRate(strUser, strFundCurr, strPoCurr, dsRate, strErr, Now.Date, "BOK", "") Then
                                            dblExRate = CDbl(dsRate.Tables(0).Rows(0).Item("rlfcxr_buy_rate"))
                                        Else
                                            Throw New Exception(strErr)
                                        End If
                                    Catch ex As Exception
                                        strErr = strErr & vbCrLf & ex.ToString
                                        Throw New Exception(strErr)
                                    End Try
                                Else
                                    dblExRate = 1
                                End If

                                drIAAcc("cswiab_exch_rate") = dblExRate

                                intCP = 12

                                strLife = aryAct(6, i)
                                strCoverage = aryAct(7, i)
                                strRider = aryAct(8, i)

                                drIAAcc("cswiab_life") = strLife
                                drIAAcc("cswiab_coverage") = strCoverage
                                drIAAcc("cswiab_rider") = strRider
                                drIAAcc("description") = dtFundInfo.Rows(0)("mpfinv_chi_desc").ToString.Trim
                                drIAAcc("chinesedescription") = dtFundInfo.Rows(0)("mpfinv_chi_name").ToString.Trim

                                drIAAcc("cswiab_unit_price") = dblUnitPrice

                                drIAAcc("cswiab_policy_no") = strPolicyNo
                                drIAAcc("cswiab_id") = i
                                drIAAcc("cswiab_fund_code") = strFundCode
                                drIAAcc("cswiab_fund_curr") = strFundCurr

                                drIAAcc("cswiab_val_date") = dValToDate

                                drIAAcc("cswiab_iunit") = Format(dblIUnit, "0.00000")
                                drIAAcc("cswiab_deemed_iunit") = Format(dblDeemedIUnit, "0.00000")
                                drIAAcc("cswiab_ivalue") = Format(TruncateRound(dblDeemedIUnit * dblUnitPrice, 2), "0.00")
                                drIAAcc("cswiab_ivalue_polccy") = Format(TruncateRound(TruncateRound(dblDeemedIUnit * dblUnitPrice, 2) * dblExRate, 2), "0.00")

                                drIAAcc("cswiab_aunit") = Format(dblAUnit, "0.00000")
                                drIAAcc("cswiab_deemed_aunit") = Format(dblDeemedAUnit, "0.00000")
                                drIAAcc("cswiab_avalue") = Format(TruncateRound(dblDeemedAUnit * dblUnitPrice, 2), "0.00")
                                drIAAcc("cswiab_avalue_polccy") = Format(TruncateRound(TruncateRound(dblDeemedAUnit * dblUnitPrice, 2) * dblExRate, 2), "0.00")

                                drIAAcc("cswiab_total_value") = Format(TruncateRound((dblDeemedIUnit + dblDeemedAUnit) * dblUnitPrice, 2), "0.00")

                                drIAAcc("cswiab_update_date") = Now
                                drIAAcc("cswiab_seq") = IIf((dblIUnit + dblAUnit) > 0, 1, 0)

                                dtIAAcc.Rows.Add(drIAAcc)

                            Next i

                        End If

                        If dtIAAcc.Rows.Count > 0 Then
                            dsFundIA.Tables.Add(dtIAAcc)
                            Return True
                        Else
                            Return False
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            strErr = strErr & vbCrLf & ex.ToString & intCP
            MsgBox(strErr)
            Return False
        Finally

        End Try

    End Function

    Public Function GetTotalInvestmentPreimumPaid(ByVal strPoNo As String, ByVal strFilterTranType As String) As Double

        Dim strErr As String = ""
        Dim wsPOS As POSWS.POSWS = Nothing
        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        Dim objPowsMQHeader As New POSWS.MQSOAPHeader

        Dim dsReceData As New DataSet

        Try
            ' Setup header
            SetupMQHeader(objPowsMQHeader, objMQQueHeader)
            SetupDBHeader(objPowsDBHeader, objDBHeader)

            wsPOS = New POSWS.POSWS
            wsPOS.DBSOAPHeaderValue = objPowsDBHeader
            wsPOS.MQSOAPHeaderValue = objPowsMQHeader

            wsPOS.Url = Utility.Utility.GetWebServiceURL("POSWS", objDBHeader, objMQQueHeader)
            wsPOS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsPOS.Timeout = My.Settings.WStIMEOUT

            Dim dblLAAmount As Double = 0
            Dim dblAmount As Double = 0

            'ITSR1285!VAltitude FE Enhancement: There is no Altitude policy in Capsil 

            'Check whether it is Life Asia Policy, if yes, get sub account balance
            Dim blnLAPolicy As Boolean
            blnLAPolicy = IIf(wsPOS.GetPolicyType(strPoNo, strErr) = 1, True, False) ' 1 = ING 

            If blnLAPolicy Then

                'Trailer is not used in the wrapper, will return all sub accounts
                Dim dtLAData = GetSubAcctBal(strPoNo, wsPOS)

                'Get Link Plan and Riders of the policy, also get the trailer mapping in LifeAsia
                Dim dtTrailerMap = GetTrailerMap(strPoNo, wsPOS)

                dblLAAmount = 0

                'Only filter Account Code = "LE", Account Type "LP"
                If dtLAData IsNot Nothing Then
                    Dim drLAData As DataRow() = dtLAData.Select("Acct_Code = 'LE'")
                    If drLAData IsNot Nothing And drLAData.Length > 0 Then
                        For Each dr As DataRow In drLAData
                            If dr("Acct_Type") = "LP" Or dr("Acct_Type") = "SP" Then
                                If dr("Entity") IsNot Nothing Then
                                    Dim strLife = Mid(dr("Entity").ToString, 1, 2)
                                    Dim strCoverage = Mid(dr("Entity").ToString, 3, 2)
                                    Dim strRider = Mid(dr("Entity").ToString, 5, 2)
                                    If dtTrailerMap IsNot Nothing Then
                                        Dim drTrailerMap As DataRow() = dtTrailerMap.Select("LIFE = '" & strLife & "' And COVERAGE = '" & strCoverage & "' And RIDER = '" & strRider & "'")
                                        If drTrailerMap IsNot Nothing And drTrailerMap.Length > 0 Then
                                            dblLAAmount = dblLAAmount + Math.Abs(CDec(dr("Curr_Bal")))
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                    dblAmount = dblAmount + dblLAAmount
                End If
            Else
                MsgBox("CRS.GetTotalInvestmentPreimumPaid() unable to return result with non-LA policy.")
            End If
            Return dblAmount

        Catch ex As Exception
            strErr = strErr & vbCrLf & ex.ToString
            MsgBox(strErr)
        Finally
            If wsPOS IsNot Nothing Then
                wsPOS.Dispose()
            End If
        End Try

    End Function

    Public Function GetEstimatedSurrenderAmount(ByVal strPoNo As String, ByVal dateEffectDate As Date) As Double

        Dim dblAmount As Decimal = 0
        Dim strErr As String = ""
        Dim wsPOS As POSWS.POSWS
        Dim objPowsDBHeader As New POSWS.DBSOAPHeader
        Dim objPowsMQHeader As New POSWS.MQSOAPHeader

        Try
            ' Setup header
            SetupMQHeader(objPowsMQHeader, objMQQueHeader)
            SetupDBHeader(objPowsDBHeader, objDBHeader)

            wsPOS = New POSWS.POSWS
            wsPOS.DBSOAPHeaderValue = objPowsDBHeader
            wsPOS.MQSOAPHeaderValue = objPowsMQHeader

            wsPOS.Url = Utility.Utility.GetWebServiceURL("POSWS", objDBHeader, objMQQueHeader)
            wsPOS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsPOS.Timeout = My.Settings.WStIMEOUT

            Dim blnLAPolicy As Boolean
            blnLAPolicy = IIf(wsPOS.GetPolicyType(strPoNo, strErr) = 1, True, False) ' 1 = ING 

            If blnLAPolicy Then

                Dim dsSendData As New DataSet
                Dim dtSendData As New DataTable
                Dim dsSur As New DataSet
                Dim TotalCharge As Double = 0

                dtSendData.Columns.Add("PolicyNo", Type.GetType("System.String"))
                dtSendData.Columns.Add("EffDate", Type.GetType("System.DateTime"))
                dtSendData.Columns.Add("ZFULREF", Type.GetType("System.String"))

                Dim dr As DataRow = dtSendData.NewRow()

                If dateEffectDate = #1/1/1900# Then
                    dateEffectDate = DateTime.Today
                End If

                dr("PolicyNo") = strPoNo
                dr("EffDate") = dateEffectDate
                dr("ZFULREF") = ""  ' ITSR4804-01, Work for Assurance full refund tick("Y" / "N"), default empty string here

                dtSendData.Rows.Add(dr)
                dsSendData.Tables.Add(dtSendData)

                If Not wsPOS.GetSurValue(dsSendData, dsSur, strErr) Then
                    Throw New Exception(strErr)
                Else
                    If dsSur.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To dsSur.Tables(0).Rows.Count - 1
                            dblAmount = dblAmount + Val(dsSur.Tables(0).Rows(i).Item("Est_Value"))
                            TotalCharge += Val(dsSur.Tables(0).Rows(i).Item("Act_Value"))
                        Next
                        dblAmount += TotalCharge ' Include penalty
                    End If
                End If

            End If

            Return dblAmount

        Catch ex As Exception
            strErr = strErr & vbCrLf & ex.ToString
            MsgBox(strErr)
        Finally
            If wsPOS IsNot Nothing Then
                wsPOS.Dispose()
            End If
        End Try

    End Function

#Region "get personal info addres for mcu customers"

    Public Function GetPersonalInfoAddress4McuCustomer(ByVal strCustID As String, ByVal strClientID As String,
                                             ByRef strErrMsg As String, ByRef blnCovSmoker As Boolean,
                                             ByRef dtPersonal As DataTable, ByRef dtAddress As DataTable,
                                             ByRef dtCIWPersonInfo As DataTable, Optional ByVal blnLoadPol As Boolean = True) As Boolean


        Dim sqlConn As New SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strSQL As String

        Dim lngErrNo As Long = 0
        Dim lngCnt As Long

        GetPersonalInfoAddress4McuCustomer = False

        Try

            ' Temp add to handle duplicate clientid
            Dim strClient() As String

            'strClient = GetClientList(strCustID)
            strClient = New String() {strCustID}

            If blnLoadPol Then
                'AC - Change to use configuration setting - start
                'If UAT <> 0 Then
                '    'objCS.Env = giEnv
                'End If
                'AC - Change to use configuration setting - start

                If Not strClient Is Nothing AndAlso strClient.Length > 0 Then

                    'sqldt = ObjCS.GetPolicyList(strClient(0), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    'sqldt = Me.GetPolicyListMCUCIW(strClient(0), "", "", "POLST", "=", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        Exit Function
                    End If

                    For I As Integer = 1 To strClient.Length - 1
                        'dtAddress = objCS.GetPolicyList(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        'dtAddress = GetPolicyListCIW(strClient(I), "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                        'dtAddress = Me.GetPolicyListMCUCIW(strClient(I), "", "", "POLST", "=", lngErrNo, strErrMsg, lngCnt)

                        If lngErrNo <> 0 Then
                            Exit Function
                        End If

                        For K As Integer = 0 To dtAddress.Rows.Count - 1
                            sqldt.Rows.Add(dtAddress.Rows(K).ItemArray)
                        Next
                    Next

                    'ds.Tables.Add(sqldt)
                Else
                    'sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErrNo, strErrMsg, lngCnt)
                    'sqldt = Me.GetPolicyListMCUCIW(strClientID, "", "", "POLST", "=", lngErrNo, strErrMsg, lngCnt)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        'wndMain.Cursor = Cursors.Default
                        Exit Function
                    End If

                    ' ds.Tables.Add(sqldt)
                End If

                ' Check if in any coverage, the insured is marked as smoker
                For I As Integer = 0 To sqldt.Rows.Count - 1
                    If sqldt.Rows(I).Item("PolicyRelateCode") = "PI" And sqldt.Rows(I).Item("SMCODE") = "S" Then
                        blnCovSmoker = True
                        Exit For
                    End If
                Next
            End If

            'strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            'strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

            'sqlConn.ConnectionString = strCIWMcuConn

            'sqlConn.ConnectionString = "server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30"
            sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("MC_CIWU105").ConnectionString

            'sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            'sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlDA.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")


            ' Use CIW
            ' Enhancement for PDPO - Add OptOutOtherFlag column
            strSQL = "Select c.CustomerID, NamePrefix, FirstName, NameSuffix, ChiLstNm+ChiFstNm as ChiName, CoName, CoCName, " &
                " DateOfBirth, Gender, MaritalStatusCode, SmokerFlag, LanguageCode, GovernmentIDCard, OptOutFlag, " &
                " EmailAddr, UseChiInd, g.AgentCode, ISNULL(g.UnitCode,'') as Agency, " &
                " AddressTypeCode, AddressLine1, AddressLine2, AddressLine3, AddressCity, " &
                " AddressPostalCode, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, EMailID, BadAddress, AddrProof, c.CountryCode, " &
                " CustomerStatusCode, CustomerTypeCode, PhoneMobile, PhonePager, PassportNumber, Occupation, BirthPlace, AgeAdmInd, c.OptOutOtherFlag " &
                " From CustomerAddress a, Customer c LEFT JOIN AgentCodes g " &
                " ON c.AgentCode = g.AgentCode " &
                " Where addresstypecode in ('R','B','I','J') and c.CustomerID = '" & strCustID.Replace("'", "''") & "' " &
                " And c.CustomerID = a.CustomerID"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            dtAddress = New DataTable("CustomerAddress")

            Try
                sqlDA.Fill(dtAddress)
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            strSQL = "Select cswdgm_cust_id as CustomerID, cswdgm_optout_email as OptEmail, cswdgm_optout_call as OptCall,"
            strSQL &= " cswdgm_rating as Rating, cswdgm_no_of_dep as DependNo, cswdgm_edu_level as EduLevel,"
            'strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup"
            'strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup, cswdgm_optout_NPS"
            'changed by jeff tam
            strSQL &= " cswdgm_ann_sal as PersonalIncome, cswdgm_household_income as HouseholdIncome, cswdgm_remark as Remarks, cswdgm_occupation as Occup"
            strSQL &= " From csw_demographic"
            strSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            dtCIWPersonInfo = New DataTable("CIWCustomer")

            Try
                sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
                sqlDA.Fill(dtCIWPersonInfo)
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            sqlConn.Dispose()

            'Use CAPSIL

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    'objCS.Env = giEnv
            '    objCS.Env = 0
            'End If
            'AC - Change to use configuration setting - end


            'dtPersonal = objCS.GetORDUNA("'" & strClientID.Replace("'", "''") & "'", lngErrNo, strErrMsg)
            If Not strClient Is Nothing AndAlso strClient.Length > 0 Then
                'dtPersonal = objCS.GetORDUNA("'" & strClient(0) & "'", lngErrNo, strErrMsg)
                'dtPersonal = objCS.GetCIWCustInfo(strClient(0))
                'dtPersonal = GetCIWMcuCustInfo(strClient(0))
            End If
            'Handle Error in GetChiAddr() if no record found
            'If Not dtPersonal Is Nothing Then
            '    If dtPersonal.Rows.Count > 0 Then
            '        GetChiAddr(dtPersonal)
            '    End If
            '    dtPersonal.TableName = "CustomerAddress"
            'End If

            If Not dtPersonal Is Nothing AndAlso Not dtAddress Is Nothing Then
                Try
                    dtPersonal.Columns.Add("AgeAdmInd", Type.GetType("System.String"))
                Catch ex As Exception
                End Try
                Try
                    dtPersonal.Columns.Add("OptOutOtherFlag", Type.GetType("System.String"))
                Catch ex As Exception
                End Try

                If dtPersonal.Rows.Count > 0 AndAlso dtAddress.Rows.Count > 0 Then
                    dtPersonal.Rows(0).Item("CustomerStatusCode") = dtAddress.Rows(0).Item("CustomerStatusCode")
                    dtPersonal.Rows(0).Item("OptOutFlag") = dtAddress.Rows(0).Item("OptOutFlag")
                    dtPersonal.Rows(0).Item("MaritalStatusCode") = dtAddress.Rows(0).Item("MaritalStatusCode")
                    dtPersonal.Rows(0).Item("PhoneMobile") = dtAddress.Rows(0).Item("PhoneMobile")
                    dtPersonal.Rows(0).Item("PhonePager") = dtAddress.Rows(0).Item("PhonePager")
                    dtPersonal.Rows(0).Item("PassportNumber") = dtAddress.Rows(0).Item("PassportNumber")
                    dtPersonal.Rows(0).Item("Occupation") = dtAddress.Rows(0).Item("Occupation")
                    dtPersonal.Rows(0).Item("BirthPlace") = dtAddress.Rows(0).Item("BirthPlace")

                    dtPersonal.Rows(0).Item("AgeAdmInd") = dtAddress.Rows(0).Item("AgeAdmInd")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column START
                    dtPersonal.Rows(0).Item("OptOutOtherFlag") = dtAddress.Rows(0).Item("OptOutOtherFlag")
                    ' Enhancement for PDPO - Add OptOutOtherFlag column END
                End If
            End If
            GetPersonalInfoAddress4McuCustomer = True

        Catch ex As Exception
            strErrMsg = ex.ToString
        End Try

    End Function

#End Region


#Region "get mcu customer info in CIW DB"

    Public Function GetCIWMcuCustInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal CustID As String, ByRef strErr As String) As DataTable


        'Dim sqlConn As New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30")
        Dim sSql As String
        Dim sqldt As New DataTable("CustomerAddress")
        Dim sqlDA As New SqlDataAdapter

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            Try
                sSql = "select top 1 c.nameprefix, c.firstname, c.NameSuffix, isnull(c.ChiLstNm,'') + isnull(c.ChiFstNm,'') as ChiName , c.DateOfBirth, " & _
                "c.Gender,  c.MaritalStatusCode, c.SmokerFlag, c.LanguageCode , c.GovernmentIDCard , '0' as OptOutFlag , c.CoName , " & _
                "c.CoCName , c.EmailAddr , c.UseChiInd, ca.AddressTypeCode, ca.AddressLine1, ca.AddressLine2, ca.AddressLine3, " & _
                "ca.AddressCity, ca.AddressPostalCode, ca.PhoneNumber1, ca.PhoneNumber2, ca.FaxNumber1, ca.BadAddress, " & _
                "ca.CountryCode, ca.CustomerID, c.agentcode as clientid, c.CustomerStatusCode, cs.CustomerStatus,  c.CustomerTypeCode, ct.CustomerType, c.AgentCode, " & _
                "c.PassportNumber, c.PhoneMobile, c.PhonePager, c.BirthPlace, '0' as OptOutOtherFlag, " & _
                "c.PhonePager, c.occupName as occupation, c.occupcode , ca.FaxNumber2 " & _
                "from customer c left outer join customeraddress ca on c.CustomerID=ca.CustomerID " & _
                "left outer join CustomerTypeCodes ct on c.CustomerTypeCode=ct.CustomerTypeCode " & _
                "left outer join CustomerStatusCodes cs on c.CustomerStatusCode=cs.CustomerStatusCode " & _
                "where c.CustomerID = @cusID "

                conn.Open()
                sqlDA = New SqlDataAdapter(sSql, conn)
                sqlDA.SelectCommand.CommandType = CommandType.Text

                sqlDA.SelectCommand.Parameters.Add("@cusID", SqlDbType.VarChar, 100).Value = CustID

                sqlDA.MissingSchemaAction = MissingSchemaAction.Add
                sqlDA.MissingMappingAction = MissingMappingAction.Passthrough

                sqlDA.Fill(sqldt)


            Catch sqlEx As SqlException
                strErr &= "SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message
            Catch ex As Exception
                strErr &= "Exception: " & CStr(ex.Message)
            Finally
                conn.Close()
            End Try

            Return sqldt

        End Using

    End Function

#End Region


    Public Function GetSubAcctBal(ByVal sPolicy As String, ByVal wsPOS As POSWS.POSWS) As DataTable

        Try
            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable
            Dim dsReceData As New DataSet
            Dim strErr As String = ""
            Dim strMsg As String = ""

            dtSendData.Columns.Clear()
            dtSendData.Columns.Add("PolicyNo", Type.GetType("System.String"))
            dtSendData.Columns.Add("MANDREF", Type.GetType("System.String"))
            dtSendData.Rows.Add()
            dtSendData.Rows(0)("PolicyNo") = sPolicy
            dtSendData.Rows(0)("MANDREF") = "00000"
            dsSendData.Tables.Clear()
            dsSendData.Tables.Add(dtSendData)
            dsReceData.Tables.Clear()

            If Not wsPOS.GetSubAcctBal(dsSendData, dsReceData, strMsg, strErr) Then
                Throw New Exception(strErr)
            Else
                Return dsReceData.Tables(1)
            End If
        Finally
            If wsPOS IsNot Nothing Then
                wsPOS.Dispose()
            End If
        End Try

    End Function

    Public Function GetTrailerMap(ByVal policyNumber As String, ByVal wsPOS As POSWS.POSWS) As DataTable

        Try
            Dim dsReceData As New DataSet
            Dim strErr As String = ""

            If Not wsPOS.GetTrailerMap(policyNumber, dsReceData, strErr) Then
                Throw New Exception(strErr)
            Else
                Return dsReceData.Tables(0)
            End If
        Finally
            If wsPOS IsNot Nothing Then
                wsPOS.Dispose()
            End If
        End Try

    End Function

    Public Sub SetupMQHeader(ByRef destination As POSWS.MQSOAPHeader, ByVal source As Utility.Utility.MQHeader)

        destination.QueueManager = source.QueueManager
        destination.RemoteQueue = source.RemoteQueue
        destination.ReplyToQueue = source.ReplyToQueue
        destination.LocalQueue = source.LocalQueue
        destination.Timeout = source.Timeout

        destination.ProjectAlias = source.ProjectAlias
        destination.ConnectionAlias = source.ConnectionAlias
        destination.UserType = source.UserType
        destination.EnvironmentUse = source.EnvironmentUse
        destination.CompanyID = source.CompanyID
        destination.UserID = source.UserID
        destination.MachineID = source.MachineID
        destination.VersionNo = source.VersionNo
        destination.LibraryName = source.LibraryName

    End Sub

    Public Sub SetupDBHeader(ByRef destination As POSWS.DBSOAPHeader, ByVal source As Utility.Utility.ComHeader)

        destination.User = source.UserID
        destination.Project = source.ProjectAlias
        destination.Env = source.EnvironmentUse
        destination.Comp = source.CompanyID
        destination.ConnectionAlias = source.ConnectionAlias
        destination.UserType = source.UserType

        destination.CIWUser = source.UserID
        destination.CIWProject = source.ProjectAlias
        destination.CIWEnv = source.EnvironmentUse
        destination.CIWComp = source.CompanyID
        destination.CIWConnectionAlias = source.ConnectionAlias

    End Sub

    'ITSR1285 - ChopardC END
#Region "Check user permission and user type"

    Public Function CheckUserPermission(ByVal userId As String, ByRef dtFuncList As DataTable, ByRef strErr As String) As DataTable

        Dim strSQL As String = ""
        'Dim dtFuncList As New DataTable
        Dim lstFunc As New List(Of String)
        Dim strUserId As String = ""
        Dim strCompanyId As String = ""
        Dim sqlconnect As New SqlConnection

        Dim allowaccess As Boolean = False
        'Dim sqlconnect As New SqlConnection
        'Dim strSQL As String = String.Empty

        sqlconnect.ConnectionString = ConfigurationManager.ConnectionStrings("ProfileConnStr").ConnectionString '""server=upsuatdb1.hk.azure;database=INGProfileU105;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30""

        Try
            'strSQL = "Select usr.upsult_usr_grp as [AccessItem] , usgp.upsugt_usr_grp_name as [AccessGrp] "
            'strSQL += "from ups_user_list_tab usr "
            'strSQL += "inner join ups_user_group_tab usgp on usr.upsult_usr_grp=usgp.upsugt_usr_grp_no "
            'strSQL += "where upsult_sys_abv='CSW' "
            'strSQL += "and usr.upsult_usr_id='" + userId + "'"


            strSQL = "SELECT gfperm.upsgfpt_func_name AS [AccessFunc], gfperm.upsgfpt_func_desc AS MenuBtn "
            strSQL += "FROM ups_user_list_tab usr "
            strSQL += "INNER JOIN ups_user_group_tab usgp on usr.upsult_usr_grp = usgp.upsugt_usr_grp_no "
            strSQL += "INNER JOIN ups_grp_func_permission_tab gfperm on gfperm.upsgfpt_usr_grp_no = usgp.upsugt_usr_grp_no "
            strSQL += "WHERE usgp.upsugt_sys_abv='CSW' "
            strSQL += "AND usr.upsult_usr_id='" + userId + "' "
            strSQL += "AND gfperm.upsgfpt_usr_perm_flag = 'Y'"


            'ConfigurationManager.ConnectionStrings("CRSWSConnStr").ConnectionString()
            'Dim DBConName As String = ConfigurationManager.ConnectionStrings("CRSWSConnStr").ToString

            'sqlconnect.ConnectionString = DBConName

            sqlconnect.Open()
            Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtFuncList)
            sqlconnect.Close()

            ' If Exec(strSQL, "POS", dtUserGroup, strErr) Then
            'If dtUserGroup.Rows.Count > 0 Then


            'sqlconnect.ConnectionString = strUPSConn
            'sqlconnect.Open()
            'Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            'sqlDA.Fill(dtUserGroup)
            'sqlconnect.Close()
            If dtFuncList.Rows.Count > 0 Then

                Return dtFuncList

            End If

            'allowaccess = True
            'End If
            'End If

        Catch ex As Exception
            Dim strError As String
            strError = ex.ToString + " occurs in modGlobal checkUserGroup"
            MsgBox(strError)
            Return dtFuncList
        End Try

        Return dtFuncList

    End Function
#End Region

#Region "Get Private RS"


    Public Function GetPrivRS(ByVal intGroupID As Integer, ByVal strCtrl As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        'dt.TableName = "privRSTbl"

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ProfileConnStr").ConnectionString)

            Try
                strSQL = "Select * from ups_menu_item_tab Where upsmit_sys_abv = 'CSW' "

                If intGroupID <> 0 Then
                    strSQL &= " And upsmit_class = @GroupID "
                End If

                conn.Open()

                da = New SqlDataAdapter(strSQL, conn)
                da.MissingSchemaAction = MissingSchemaAction.Add
                da.MissingMappingAction = MissingMappingAction.Passthrough

                If (intGroupID <> 0) Then

                    da.SelectCommand.Parameters.Add("@GroupID", SqlDbType.Int, 20).Value = intGroupID

                End If

                da.FillSchema(dt, SchemaType.Source)
                da.Fill(dt)

            Catch err As SqlClient.SqlException
                lngErrNo = err.Number
                strErrMsg = "GetPrivRS - " & err.ToString()
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "GetPrivRS - " & ex.ToString
                Return Nothing
            Finally
                sqlconnect.Close()
            End Try

        End Using

        Return dt

    End Function

#End Region

#Region "Get ori User Right "

    Public Function checkUserRight(ByVal UserID As String, ByRef strErr As String) As DataTable

        Dim allowaccess As Boolean = False
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String = String.Empty
        Dim dtUserGroup As DataTable = New DataTable()
        dtUserGroup.TableName = "UsrGrpTbl"

        Try
            strSQL = "Select usr.upsult_usr_grp as [AccessItem] , usgp.upsugt_usr_grp_name as [AccessGrp] "
            strSQL += "from ups_user_list_tab usr "
            strSQL += "inner join ups_user_group_tab usgp on usr.upsult_usr_grp=usgp.upsugt_usr_grp_no "
            strSQL += "where upsult_sys_abv='CSW' "
            strSQL += "and usr.upsult_usr_id='" + UserID + "'"

            sqlconnect.ConnectionString = ConfigurationManager.ConnectionStrings("ProfileConnStr").ConnectionString '"server=upsuatdb1.hk.azure;database=INGProfileU105;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30"
            sqlconnect.Open()
            Dim sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtUserGroup)
            sqlconnect.Close()

            'If dtUserGroup.Rows.Count > 0 Then
            '    allowaccess = True
            'End If

        Catch ex As Exception
            strErr = ex.ToString + " occurs in checkUserGroup"
            'MsgBox(strError)
            Return dtUserGroup
        End Try

        Return dtUserGroup

    End Function

#End Region


#Region "Get UPS Group List"

    Public Function GetUPSGroup(ByVal strSystem As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable 'Object

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim usrRightDt As DataTable = New DataTable() 'KT20150615
        usrRightDt.TableName = "UsrRightTbl"

        Try

            strSQL = "Select upsugt_usr_right " &
                " From ups_user_list_tab, ups_user_group_tab " &
                " Where upsult_sys_abv = '" & strSystem & "'" &
                " And upsult_usr_id = '" & strUser & "'" &
                " And upsult_usr_grp = upsugt_usr_grp_no"

            sqlconnect.ConnectionString = ConfigurationManager.ConnectionStrings("ProfileConnStr").ConnectionString '"server=upsuatdb1.hk.azure;database=INGProfileU105;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30"
            Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
            sqlconnect.Open()
            'KT20150615
            Using dad As New SqlDataAdapter(strSQL, sqlconnect)
                dad.Fill(usrRightDt)
            End Using

        Catch err As SqlClient.SqlException
            lngErrNo = err.Number
            strErrMsg = err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetUPSGroup - " & ex.ToString
        Finally
            sqlconnect.Close()
        End Try
        Dim cnt As Integer = usrRightDt.Rows.Count

        Return usrRightDt

    End Function

#End Region


#Region "Search Customer List for Macau"

    Public Function GetCustomerListMcu(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String,
                                       ByVal strCustID As String, ByVal strAgentNo As String, ByVal strPlateNumber As String,
                                       ByVal LastNameVal As String, ByVal FirstNameVal As String, ByVal HKIDVal As String, ByVal CustVal As String,
                                       ByVal AgentNoVal As String, ByVal PlateNumberVal As String, ByVal MobileVal As String, ByVal EmailVal As String,
                                       ByVal blnCntOnly As Boolean, ByVal strMobile As String, ByVal strEmail As String, ByRef lngErrNo As Long,
                                       ByRef strErrMsg As String, ByRef intCnt As Integer, ByRef dtCusLst As DataTable) As Boolean


        Dim sqlDA As New SqlDataAdapter
        Dim sqldt As New DataTable("CustList")
        Dim strSEL, strSELC, strSQL, strCR, strCR1, strCR2 As String
        Dim intTmpCnt As Integer

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            strSELC = "Select count(*) "

            strSQL = "SELECT NamePrefix, FirstName, NameSuffix, ISNULL(ChiLstNm,'') + ISNULL(ChiFstNm,'') as ChiName, " +
                    "GovernmentIDCard = CASE WHEN GovernmentIDCard <> '' THEN GovernmentIDCard ELSE PassportNumber END, Gender, " +
                    "CoName, CoCName, CustomerID, DateOfBirth, AgentCode, AgentCode AS clientID FROM customer WHERE 1=1 "

            If strLastName <> "" Then
                strCR1 &= " AND " & strLastName
            End If

            If strFirstName <> "" Then
                strCR1 &= " AND " & strFirstName
            End If

            If strAgentNo <> "" Then
                strCR1 &= " AND " & strAgentNo
            End If

            If strHKID <> "" Then
                strCR2 &= " AND " & strHKID
            End If

            'strCR = Replace(strCustID, "'", "")
            'strCR = strCustID
            If strCustID <> "" Then
                strCR2 &= " AND " & strCustID
            End If

            If strPlateNumber <> "" Then
                strCR2 &= " AND CustomerID IN (" & "SELECT c.CustomerID FROM gi_vehicle a " &
                "INNER JOIN csw_poli_rel b ON a.PolicyAccountID=b.PolicyAccountID AND b.PolicyRelateCode='PH' " &
                "INNER JOIN customer c ON b.CustomerID = c.CustomerID AND " & strPlateNumber & ")"
            End If

            If strMobile <> "" Then
                strCR2 &= " AND " & strMobile
            End If

            'strCR = Replace(strCustID, "'", "")
            'strCR = strCustID
            If strEmail <> "" Then
                strCR2 &= " AND " & strEmail
            End If

            'sqlconnect.ConnectionString = ConfigurationManager.ConnectionStrings("MC_CIWU105").ConnectionString
            'sqlconnect.ConnectionString = "server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30"
            'sqlconnect.Open()

            If strCR1 = "" And strCR2 = "" Then
                lngErrNo = -1
                strErrMsg = "GetCustomerList - Invalid Criteria"
                Exit Function
            Else

                strSQL &= strCR1 & strCR2

            End If

            Try
                If blnCntOnly Then


                    Dim tmpDt = New DataTable("CntTbl")
                    Dim tmpDa = New SqlDataAdapter(strSELC + strSQL, conn)

                    tmpDa.SelectCommand.CommandType = CommandType.Text

                    If strLastName <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@NameSuffix", SqlDbType.VarChar, 50).Value = LastNameVal.Replace("'", "").ToString()
                    End If

                    If strFirstName <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstNameVal.Replace("'", "").ToString()
                    End If

                    If strAgentNo <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@AgentCode", SqlDbType.VarChar, 30).Value = AgentNoVal.Replace("'", "").ToString()
                    End If

                    If strHKID <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@GovernmentIDCard", SqlDbType.VarChar, 30).Value = HKIDVal.Replace("'", "").ToString()
                        tmpDa.SelectCommand.Parameters.Add("@PassportNumber", SqlDbType.VarChar, 50).Value = HKIDVal.Replace("'", "").ToString()
                    End If

                    If strCustID <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.VarChar, 30).Value = CustVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strPlateNumber <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@RegistrationNo", SqlDbType.VarChar, 50).Value = PlateNumberVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strMobile <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@PhoneMobile", SqlDbType.VarChar, 50).Value = MobileVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strEmail <> "" Then
                        tmpDa.SelectCommand.Parameters.Add("@EmailAddr", SqlDbType.VarChar, 50).Value = EmailVal.Replace("'", "").ToString() '"11149531"
                    End If

                    tmpDa.Fill(tmpDt)

                    intTmpCnt = tmpDt.Rows.count

                    'intTmpCnt = sqlcmd.ExecuteScalar
                    'sqlconnect.Close()
                End If

                If blnCntOnly AndAlso intTmpCnt > intCnt Then
                    intCnt = intTmpCnt
                    Exit Function
                Else

                    sqldt = New DataTable("CustList")

                    sqlDA = New SqlDataAdapter(strSEL & strSQL, conn)

                    sqlDA.SelectCommand.CommandType = CommandType.Text


                    If strLastName <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@NameSuffix", SqlDbType.VarChar, 50).Value = LastNameVal.Replace("'", "").ToString()
                    End If

                    If strFirstName <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstNameVal.Replace("'", "").ToString()
                    End If

                    If strAgentNo <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@AgentCode", SqlDbType.VarChar, 30).Value = AgentNoVal.Replace("'", "").ToString()
                    End If

                    If strHKID <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@GovernmentIDCard", SqlDbType.VarChar, 30).Value = HKIDVal.Replace("'", "").ToString()
                        sqlDA.SelectCommand.Parameters.Add("@PassportNumber", SqlDbType.VarChar, 50).Value = HKIDVal.Replace("'", "").ToString()
                    End If

                    If strCustID <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.VarChar, 30).Value = CustVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strPlateNumber <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@RegistrationNo", SqlDbType.VarChar, 50).Value = PlateNumberVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strMobile <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@PhoneMobile", SqlDbType.VarChar, 50).Value = MobileVal.Replace("'", "").ToString() '"11149531"
                    End If

                    If strEmail <> "" Then
                        sqlDA.SelectCommand.Parameters.Add("@EmailAddr", SqlDbType.VarChar, 50).Value = EmailVal.Replace("'", "").ToString() '"11149531"
                    End If

                    'sqlDA.Fill(sqldt)
                    'intCnt = sqldt.Rows.Count

                    sqlDA.Fill(dtCusLst)
                    intCnt = dtCusLst.Rows.Count

                End If

            Catch err As SqlException
                lngErrNo = -1
                strErrMsg = "GetCustomerList - " & err.ToString()
                Return False
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "GetCustomerList - " & ex.ToString
                Return False
            End Try

        End Using

        Return True

    End Function


#End Region


#Region "Common SQL execution function"

    Public Function commonSQLExcute(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strSql As String, ByVal paramList As String) As ApiResponse 'List(Of ApiResponse)

        Dim _api As New ApiResponse
        Dim _paramClass As New ParamClass
        Dim _encodeWSParam As New encodeWSParam

        Dim dtResult As DataTable = New DataTable
        dtResult.TableName = "ResultTbl"
        '_api.DtRespResult.TableName = "ResultTbl"
        '_api.errMsg = ""

        'Dim htResult As Hashtable = New Hashtable
        'htResult("ResultTbl") = dtResult
        'htResult("ErrMsg") = ""

        'dtResult.TableName = "ResultTbl"

        '_encodeWSParam.Deserialize(Of _paramClass(paramList))
        'Dim formatter As IFormatter = New BinaryFormatter()
        'Dim obj As ParamClass = formatter.Deserialize(_paramClass) '(ParamClass)formatter.Deserialize(stream);  

        If (strSql <> String.Empty) Then
            Try
                Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

                    cnn.Open()

                    Using da As New SqlDataAdapter(strSql, cnn)

                        Dim tmpDbType As SqlDbType

                        'If (dtParam.Rows.Count > 0) Then

                        '    For Each sqlParam As SqlParameter In dtParam.Rows
                        '        da.SelectCommand.Parameters.Add(sqlParam)
                        '    Next

                        'End If

                        'For Each item As SqlParameter In paramLst
                        'da.SelectCommand.Parameters.Add(paramLst)
                        'Next

                        'For Each item As DictionaryEntry In paramLst

                        '    If (item.Value.GetType.Equals(GetType(Integer))) Then
                        '        tmpDbType = SqlDbType.Int
                        '    ElseIf (item.Value.GetType.Equals(GetType(String))) Then
                        '        tmpDbType = SqlDbType.VarChar
                        '    ElseIf (item.Value.GetType.Equals(GetType(Double))) Then
                        '        tmpDbType = SqlDbType.Float
                        '    End If

                        '    da.SelectCommand.Parameters.Add(item.Key, tmpDbType).Value = item.Value
                        'Next

                        da.Fill(dtResult)

                        _api.DtRespResult = dtResult

                    End Using

                End Using

            Catch ex As Exception
                ' = ex.ToString
                _api.ErrorMsg = ex.ToString
            End Try

        End If

        Return _api

    End Function

#End Region

#Region "Get GetUPSGroup"

    'Public Function GetUPSGroup(ByVal strUser As String, ByRef lngErrNo As Long, _
    'ByRef strErrMsg As String) As DataTable

    '    Dim strSQL As String
    '    Dim sqlconnect As New SqlConnection
    '    Dim usrRightDt As New DataTable 'KT20150615

    '    Try

    '        strSQL = "Select upsugt_usr_right " & _
    '            " From ups_user_list_tab, ups_user_group_tab " & _
    '            " Where upsult_sys_abv = 'CSW' " & _
    '            " And upsult_usr_id = '" & strUser & "'" & _
    '            " And upsult_usr_grp = upsugt_usr_grp_no"

    '        sqlconnect.ConnectionString = ConfigurationManager.ConnectionStrings("ProfileConnStr").ConnectionString '"server=upsuatdb1.hk.azure;database=INGProfileU105;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30"
    '        Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
    '        sqlconnect.Open()

    '        'KT20150615
    '        Using dad As New SqlDataAdapter(strSQL, sqlconnect)
    '            dad.Fill(usrRightDt)
    '        End Using

    '    Catch err As SqlClient.SqlException
    '        lngErrNo = err.Number
    '        strErrMsg = err.ToString()
    '    Catch ex As Exception
    '        lngErrNo = -1
    '        strErrMsg = "GetUPSGroup - " & ex.ToString
    '    Finally
    '        sqlconnect.Close()
    '    End Try

    '    Return usrRightDt

    'End Function

#End Region


#Region "Search policy list for Macau "

    Public Function GetPolicyListMCUCIW(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String,
               ByVal strTable As String, ByVal strCri As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer,
               Optional ByVal blnCntOnly As Boolean = False) As DataTable

        Dim daPOLST, daAGTLST As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As New DataTable
        Dim intTmpCnt As Integer

        dtPOLST.TableName = "PolicyAccTbl"

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            conn.Open()

            strSELC = "Select count(*) "

            Try
                ' Policy List (by policy)
                If strPolicy <> "" Then
                    strSEL = "Select P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                            " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                            " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                    strSQL = " From policyaccount p, product pd, coveragedetail cd, csw_poli_rel r, csw_poli_rel r1, customer c " &
                        " Where p.policyaccountid {0} @policyNo " &
                        " and p.policyaccountid = r.policyaccountid " &
                        " and p.policyaccountid = r1.policyaccountid " &
                        " and p.policyaccountid = cd.policyaccountid " &
                        " and r1.policyrelatecode = 'SA' " &
                        " and r1.customerid = c.customerid " &
                        " and p.productid = pd.productid and cd.trailer = 1 "

                End If

                ' Policy List (by customer id)
                '" And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' "
                If strCustID <> "" Then

                    strSEL = "Select DISTINCT P.PolicyAccountid, P.ProductID, Pd.Description, r.PolicyRelateCode, " &
                            " AccountStatusCode, PolicyCurrency, PolicyEffDate, PaidToDate, " &
                            " Mode AS PaidMode, AgentCode as POAGCY, cd.SmokerFlag as SMCODE, P.ModalPremium "

                    strSQL = " From csw_poli_rel r, policyaccount p, product pd, coveragedetail cd, customer c, csw_poli_rel r1 " &
                         " Where p.policyaccountid = r.policyaccountid  " &
                         " and p.policyaccountid = cd.policyaccountid  " &
                         " and r.customerid = @customerId" &
                         " and p.productid = pd.productid and cd.trailer = 1  " &
                         " and p.policyaccountid = r1.policyaccountid   " &
                         " and r1.policyrelatecode = 'SA' " &
                         " and r1.customerid = c.customerid  "
                End If

                If strRel <> "" Then
                    strSQL &= " And r.policyrelatecode = 'PH'"
                Else
                    strSQL &= " And r.policyrelatecode in ('PH','PI')"
                End If

                If blnCntOnly Then

                    strSQL = strSQL.Replace("{0}", strCri)

                    Dim sda As New SqlDataAdapter(strSELC & strSQL, conn)
                    sda.SelectCommand.CommandType = CommandType.Text

                    If (strPolicy <> "") Then

                        sda.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 30).Value = strPolicy.Replace("'", "").ToString() '"11149531"

                        Dim _dt = New DataTable()

                        sda.Fill(_dt)

                        intTmpCnt = _dt.Rows(0).Item(0).ToString()

                    End If
                End If


                If blnCntOnly AndAlso intTmpCnt > intCnt Then
                    intCnt = intTmpCnt
                    Exit Function
                Else
                    Dim cmd = New SqlCommand(strSEL & strSQL, conn)

                    daPOLST = New SqlDataAdapter(strSEL & strSQL, conn)
                    daPOLST.SelectCommand.CommandType = CommandType.Text

                    If (strPolicy <> "") Then
                        daPOLST.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 30).Value = strPolicy.Replace("'", "").ToString()
                    ElseIf (strCustID <> "") Then
                        daPOLST.SelectCommand.Parameters.Add("@customerId", SqlDbType.VarChar, 30).Value = strCustID.Replace("'", "").ToString()
                    End If

                    If strRel <> "" Then
                        cmd.Parameters.AddWithValue("@Rel", strRel)
                    End If

                    cmd.CommandType = CommandType.Text

                    dtPOLST = New DataTable(strTable)
                    daPOLST.Fill(dtPOLST)
                    intCnt = dtPOLST.Rows.Count

                End If

                conn.Close()

            Catch sqlex As SqlException
                lngErrNo = -1
                strErrMsg = "GetPolicyListCIW - " & sqlex.ToString
                Return Nothing
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "GetPolicyListCIW - " & ex.ToString
                Return Nothing
            End Try

        End Using

        Return dtPOLST

    End Function

    Public Function GetGIPolicyMcuList(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            Optional ByVal blnCntOnly As Boolean = False) As DataTable


        Dim oledbconnect As New SqlConnection
        Dim daPOLST, daAGTLST, da As SqlDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim dt As DataTable = New DataTable()
        Dim intTmpCnt As Integer



        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            strSELC = "Select count(*) "

            Try

                If strPolicy <> "" Then ' Policy List (by policy)

                    ' ES01 begin
                    strSEL = "Select p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " & _
                            " PolicyRelateCode, a.AccountStatusCode, AccountStatus, Space(2) as POCOCD, PolicyCurrency, " & _
                            " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " & _
                            " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                    strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a " & _
                        " Where r.policyaccountid like @PolicyNo " & _
                        " And r.policyaccountid = p.policyaccountid " & _
                        " And p.AccountStatusCode = a.AccountStatusCode " & _
                        " And p.companyid = 'GI' " & _
                        " And r.policyrelatecode = 'PH' "


                ElseIf strCustID <> "" Then ' Policy List (by customer id)

                    strSEL = "Select p.PolicyAccountid, ProductID, '<GI Policy>' as Description, " & _
                            " PolicyRelateCode, a.AccountStatusCode, Space(2) as POCOCD, PolicyCurrency, " & _
                            " 0 as POIMM, 0 as POIDD, 0 as POIYY, Mode as PaidMode, space(5) as POAGCY, space(5) as POPAGT, space(5) as POWAGT, " & _
                            " space(1) as SMCODE, 0 as POPAYY, 0 as POPAMM, 0 as POPADD, ModalPremium, PolicyEffDate, PaidToDate, r.CustomerID "

                    strSQL = " From PolicyAccount p, csw_poli_rel r, accountstatuscodes a  " & _
                        " Where r.customerid = @CusId " & _
                        " And r.policyaccountid = p.policyaccountid " & _
                        " And p.AccountStatusCode = a.AccountStatusCode " & _
                        " And p.companyid = 'GI' " & _
                        " And r.policyrelatecode = 'PH' "

                End If


                If blnCntOnly Then

                    'oledbconnect.Open()
                    conn.Open()

                    da = New SqlDataAdapter(strSELC & strSQL, conn)
                    da.SelectCommand.CommandType = CommandType.Text

                    If (strPolicy <> "") Then
                        da.SelectCommand.Parameters.Add("@PolicyNo", SqlDbType.VarChar, 100).Value = strPolicy
                    ElseIf (strCustID <> "") Then
                        da.SelectCommand.Parameters.Add("@CusId", SqlDbType.VarChar, 100).Value = strCustID
                    End If


                    da.MissingSchemaAction = MissingSchemaAction.Add
                    da.MissingMappingAction = MissingMappingAction.Passthrough

                    da.Fill(dt)

                    If (dt IsNot Nothing) Then

                        intTmpCnt = dt.Rows.Count

                    End If

                    conn.Close()
                End If

                If blnCntOnly AndAlso intTmpCnt > intCnt Then
                    intCnt = intTmpCnt
                    Exit Function
                Else
                    daPOLST = New SqlDataAdapter(strSEL & strSQL, conn)
                    dtPOLST = New DataTable(strTable)
                    daPOLST.Fill(dtPOLST)
                    intCnt = dtPOLST.Rows.Count
                End If

            Catch sqlex As SqlException
                lngErrNo = -1
                strErrMsg = "GetPolicyList - " & sqlex.ToString
                Return Nothing
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "GetPolicyList - " & ex.ToString
                Return Nothing
            End Try
        End Using

        'oledbconnect.ConnectionString = strCIWMcuConn

        Return dtPOLST

    End Function

#End Region




#Region "get Macau Underwriting Records"

    Public Function getPendingAgtCode(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As DataTable

        Dim strSQL As String = ""
        Dim da As SqlDataAdapter
        Dim dt As DataTable = New DataTable()

        dt.TableName = "agtCodeTbl"

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode) 'New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30") 'CreateSqlConnection(strCompanyCode, strEnvCode)

            strSQL = "SELECT agentcode FROM csw_poli_rel r, customer c " & _
            " WHERE r.policyrelatecode = 'WA'" & _
            " AND r.customerid = c.customerid " & _
            " AND r.policyaccountid = @policyNo "


            da = New SqlDataAdapter(strSQL, conn)
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo

            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough

            da.Fill(dt)

        End Using

        Return dt

    End Function


    'method for underwriting select csw_policy_uw table record
    Public Function getMcuPolicyUW(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef ds As DataSet, ByRef strErr As String) As DataSet

        Dim strSQL As String = ""
        Dim da As SqlDataAdapter

        Try

            Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)  'New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30") 'CreateSqlConnection(strCompanyCode, strEnvCode)

                strSQL = "Select * from csw_policy_uw " & _
                        " Where cswpuw_poli_id = @policyNo "

                da = New SqlDataAdapter(strSQL, conn)
                da.SelectCommand.CommandType = CommandType.Text

                da.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo

                da.MissingSchemaAction = MissingSchemaAction.Add
                da.MissingMappingAction = MissingMappingAction.Passthrough

                da.Fill(ds, "CswUWInfo")

            End Using

        Catch sqlex As SqlClient.SqlException
            strErr = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErr = ex.ToString
        End Try

        Return ds

    End Function

    'method for under writing select nbr outstanding req
    Public Function getMcuUWOutstandingReq(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal gcNBR As String, ByVal ds As DataSet, ByRef strErr As String) As DataSet

        Dim strSQL As String = ""
        Dim da As SqlDataAdapter

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode) 'New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30") 'CreateSqlConnection(strCompanyCode, strEnvCode)

            strSQL = "Select * from nbrvw_outstanding_req; "

            strSQL &= "Select * from nbrvw_uw_advice " & _
                " Where nbruad_policy_no = @policyNo1 "

            strSQL &= "Select * from csw_policy_uw " & _
                " Where cswpuw_poli_id = @policyNo2 "

            strSQL &= "Select * from " & gcNBR & "nbr_uw_worksheet " & _
                            " Where nbruwk_new_policy_no = @policyNo3 "

            da = New SqlDataAdapter(strSQL, conn)
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.Add("@policyNo1", SqlDbType.VarChar, 100).Value = policyNo
            da.SelectCommand.Parameters.Add("@policyNo2", SqlDbType.VarChar, 100).Value = policyNo
            da.SelectCommand.Parameters.Add("@policyNo3", SqlDbType.VarChar, 100).Value = policyNo

            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough

            da.TableMappings.Add("nbrvw_outstanding_req1", "UWInfo")
            da.TableMappings.Add("nbrvw_outstanding_req2", "CswUWInfo")

            da.TableMappings.Add("nbrvw_outstanding_req3", "UWSheet")

            da.Fill(ds, "nbrvw_outstanding_req")

        End Using

        Return ds

    End Function


    Public Function GetNBRPolicyMcu(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As Boolean
        ' ES009 begin

        Dim strSQL As String
        Dim dtResult As DataTable = New DataTable()
        Dim da As SqlDataAdapter

        dtResult.TableName = "resTbl"

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode) 'New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30") 'CreateSqlConnection(strCompanyCode, strEnvCode)

            GetNBRPolicyMcu = True


            strSQL = "select * from nbrvw_uw_worksheet " & _
                " where nbruwk_new_policy_no = @policyNo and len(isnull(nbruwk_uw_comments,''))>0 " '


            da = New SqlDataAdapter(strSQL, conn)
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo

            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough

            da.Fill(dtResult)

            If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                GetNBRPolicyMcu = False
            End If


        End Using

    End Function


#End Region


#Region "Get Major Claim History Records"

    Public Function getMClaimPolicyRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDB As String, ByVal policyNo As String, ByVal policyCap As String, ByRef strErrMsg As String) As DataSet

        Dim strSQL As String = ""
        Dim strClaim As String = ""
        Dim daClaimMaster As SqlDataAdapter
        Dim dsClaim As DataSet = New DataSet

        Dim MCSDB As String = ConfigurationManager.AppSettings("HKMCSDB")
        Dim connStr = ConfigurationManager.ConnectionStrings("HKCIWConnectionString").ConnectionString
        If (strCompanyCode.Contains("MC")) Then
            MCSDB = ConfigurationManager.AppSettings("MCMCSDB")
            connStr = ConfigurationManager.ConnectionStrings("MCCIWConnectionString").ConnectionString
        End If


        Using conn As SqlConnection = New SqlConnection(connStr)

            Try

                'strSQL = "Select *, CompanyID" & _
                '" From mjc_claim_policy, mjc_claim_register, mjc_claim_type, mjc_claim_status,  " & strDB & "PolicyAccount" & _
                '    " Where mjcply_policy_no in (@policyNum1, @policyCap1) " & _
                '    " And isnull(mjcply_policy_no, '') <> '' " & _
                '    " And mjcply_claim_no = mjcchd_claim_no " & _
                '    " And mjcply_claim_occur = mjcchd_claim_occur " & _
                '    " And mjcchd_type = mjcctp_type_id " & _
                '    " And mjcchd_clm_status = mjccst_status " & _
                '    " And mjcchd_clm_status <> 'DEL' " & _
                '    " And mjcply_policy_no = PolicyAccountID " & _
                '    " Order by mjcply_claim_no DESC, mjcply_claim_occur DESC; " & _
                '    " Select * " & _
                '    " From mjc_claim_payment " & _
                '    " Where mjcpay_policy_no in (@policyNum2, @policyCap2) And isnull(mjcpay_policy_no,'') <> '' "

                ' MCS cloud migration change
                strSQL = "Select *, CompanyID" & _
                    " from PolicyAccount pa with(nolock) " & _
                    " inner join " & MCSDB & "mjc_claim_policy mjcply with(nolock) on mjcply_policy_no = PolicyAccountID " & _
                    " inner join " & MCSDB & "mjc_claim_register mjcchd with(nolock) on mjcply_claim_no = mjcchd_claim_no And mjcply_claim_occur = mjcchd_claim_occur " & _
                    " inner join " & MCSDB & "mjc_claim_type mjcctp with(nolock) on mjcchd_type = mjcctp_type_id " & _
                    " inner join " & MCSDB & "mjc_claim_status mjccst with(nolock) on mjcchd_clm_status = mjccst_status " & _
                    " where mjcply_policy_no in (@policyNum1, @policyCap1) " & _
                    " And mjcchd_clm_status <> 'DEL' " & _
                    " Order by mjcply_claim_no DESC, mjcply_claim_occur DESC;" & _
                    " Select * " & _
                    " From " & MCSDB & "mjc_claim_payment " & _
                    " Where mjcpay_policy_no in (@policyNum2, @policyCap2) And isnull(mjcpay_policy_no,'') <> '' "

                daClaimMaster = New SqlDataAdapter(strSQL, conn)
                daClaimMaster.SelectCommand.CommandType = CommandType.Text

                daClaimMaster.SelectCommand.Parameters.Add("@policyNum1", SqlDbType.VarChar, 100).Value = policyNo
                daClaimMaster.SelectCommand.Parameters.Add("@policyCap1", SqlDbType.VarChar, 100).Value = policyCap
                daClaimMaster.SelectCommand.Parameters.Add("@policyNum2", SqlDbType.VarChar, 100).Value = policyNo
                daClaimMaster.SelectCommand.Parameters.Add("@policyCap2", SqlDbType.VarChar, 100).Value = policyCap

                daClaimMaster.MissingSchemaAction = MissingSchemaAction.Add
                daClaimMaster.MissingMappingAction = MissingMappingAction.Passthrough

                daClaimMaster.TableMappings.Add("mjc_claim_policy1", "mjc_claim_payment")
                daClaimMaster.Fill(dsClaim, "mjc_claim_policy")


            Catch sqlex As SqlException
                strErrMsg = "RefreshData(): " & sqlex.Message
            Catch ex As Exception
                strErrMsg = "RefreshData(): " & ex.Message
            End Try

            Return dsClaim

        End Using

    End Function


    Public Function getMcuMClaimPendingRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strClaim As String, ByVal policyNo As String, ByVal policyCap As String, ByVal dsClaim As DataSet, ByRef strErrMsg As String) As DataSet

        Dim strSQL As String = ""
        Dim daPending As SqlDataAdapter
        Dim dsPending As DataSet = New DataSet
        Dim daBenefit As SqlDataAdapter
        Dim dsBenefit As DataSet = New DataSet
        Dim MCSDB As String = ConfigurationManager.AppSettings("HKMCSDB")
        Dim connStr = ConfigurationManager.ConnectionStrings("HKCIWConnectionString").ConnectionString
        If (strCompanyCode.Contains("MC")) Then
            MCSDB = ConfigurationManager.AppSettings("MCMCSDB")
            connStr = ConfigurationManager.ConnectionStrings("MCCIWConnectionString").ConnectionString
        End If
        Using conn As SqlConnection = New SqlConnection(connStr)

            Try

                'strSQL = "Select * " & _
                '" From mjc_claim_pending " & _
                '" Where mjcpen_claim_no in (" + strClaim + ") " & _
                '" Order by mjcpen_claim_no, mjcpen_claim_occur, mjcpen_pending_seq"
                strSQL = "Select * From " & MCSDB & "mjc_claim_pending" & _
                " Where mjcpen_claim_no in (" & strClaim & ")" & _
                " Order by mjcpen_claim_no, mjcpen_claim_occur, mjcpen_pending_seq"

                daPending = New SqlDataAdapter(strSQL, conn)
                daPending.MissingSchemaAction = MissingSchemaAction.Add
                daPending.MissingMappingAction = MissingMappingAction.Passthrough

                'daPending.SelectCommand.Parameters.Add("@ClaimLst", SqlDbType.Int, 100).Value = strClaim

                daPending.Fill(dsClaim, "mjc_claim_pending")



                'get Claim Benefit
                'strSQL = "SELECT a.*, b.mjcbtp_description " & _
                '    " FROM mjc_claim_benefit_detail a " & _
                '    " INNER JOIN mjc_benefit_type b ON a.mjccbd_benefit_type = b.mjcbtp_type_id " & _
                '    " WHERE mjccbd_policy_no IN (@policyNo, @policyCap) " & _
                '    " AND ISNULL(mjccbd_policy_no,'') <> '' "
                strSQL = "select a.*, b.mjcbtp_description from " & MCSDB & "mjc_claim_benefit_detail a" & _
                " inner join " & MCSDB & "mjc_benefit_type b on a.mjccbd_benefit_type=b.mjcbtp_type_id " & _
                " where mjccbd_policy_no in (@policyNo, @policyCap) And isnull(mjccbd_policy_no,'') <> ''"

                daBenefit = New SqlDataAdapter(strSQL, conn)
                daBenefit.MissingSchemaAction = MissingSchemaAction.Add
                daBenefit.MissingMappingAction = MissingMappingAction.Passthrough

                daBenefit.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo
                daBenefit.SelectCommand.Parameters.Add("@policyCap", SqlDbType.VarChar, 100).Value = policyCap

                daBenefit.MissingSchemaAction = MissingSchemaAction.Add
                daBenefit.MissingMappingAction = MissingMappingAction.Passthrough

                daBenefit.Fill(dsClaim, "mjc_claim_benefit_detail")


            Catch sqlex As SqlException
                strErrMsg = "RefreshData(): " & sqlex.Message
            Catch ex As Exception
                strErrMsg = "RefreshData(): " & ex.Message
            End Try

            Return dsClaim

        End Using

    End Function


    Public Function getMcuMClaimBenefitRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal policyCap As String, ByVal dsClaim As DataSet, ByRef strErrMsg As String) As DataSet

        Dim strSQL As String = ""
        Dim daBenefit As SqlDataAdapter
        Dim dsBenefit As DataSet = New DataSet
        Dim MCSDB As String = ConfigurationManager.AppSettings("HKMCSDB")
        Dim connStr = ConfigurationManager.ConnectionStrings("HKCIWConnectionString").ConnectionString
        If (strCompanyCode.Contains("MC")) Then
            MCSDB = ConfigurationManager.AppSettings("MCMCSDB")
            connStr = ConfigurationManager.ConnectionStrings("MCCIWConnectionString").ConnectionString
        End If
        Using conn As SqlConnection = New SqlConnection(connStr)
            Try

                'strSQL = "SELECT a.*, b.mjcbtp_description " & _
                '    " FROM mjc_claim_benefit_detail a " & _
                '    " INNER JOIN mjc_benefit_type b ON a.mjccbd_benefit_type = b.mjcbtp_type_id " & _
                '    " WHERE mjccbd_policy_no IN (@policyNo, @policyCap) " & _
                '    " AND ISNULL(mjccbd_policy_no,'') <> '' "
                ' MCS cloud migration change:
                strSQL = "select a.*, b.mjcbtp_description from " & MCSDB & "mjc_claim_benefit_detail a" & _
                " inner join " & MCSDB & "mjc_benefit_type b on a.mjccbd_benefit_type=b.mjcbtp_type_id " & _
                " where mjccbd_policy_no in (@policyNo, @policyCap) And isnull(mjccbd_policy_no,'') <> ''"

                daBenefit = New SqlDataAdapter(strSQL, conn)
                daBenefit.MissingSchemaAction = MissingSchemaAction.Add
                daBenefit.MissingMappingAction = MissingMappingAction.Passthrough

                daBenefit.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo
                daBenefit.SelectCommand.Parameters.Add("@policyCap", SqlDbType.VarChar, 100).Value = policyCap

                daBenefit.MissingSchemaAction = MissingSchemaAction.Add
                daBenefit.MissingMappingAction = MissingMappingAction.Passthrough

                daBenefit.Fill(dsClaim, "mjc_claim_benefit_detail")

            Catch sqlex As SqlException
                strErrMsg = "RefreshData(): " & sqlex.Message
            Catch ex As Exception
                strErrMsg = "RefreshData(): " & ex.Message
            End Try

            Return dsClaim

        End Using

    End Function



#End Region


#Region "Get Location Code"


    Public Function GetLocation(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strAgtCode As String, ByRef strErr As String) As String

        Dim strSQL As String = ""
        Dim strLoc As String = ""
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable = New DataTable
        dt.TableName = "resultTbl"


        strSQL = "Select cswagi_unit_code + cswagi_loc_code AS locCode From cswvw_cam_agent_info Where cswagi_agent_code in (@AgentCode)"

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            conn.Open()

            Try

                da = New SqlDataAdapter(strSQL, conn)
                da.SelectCommand.CommandType = CommandType.Text

                da.SelectCommand.Parameters.Add("@AgentCode", SqlDbType.VarChar, 100).Value = strAgtCode

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey
                da.MissingMappingAction = MissingMappingAction.Passthrough
                da.Fill(dt)

                'daSMSmsg.Fill(dsSMS, "sms_message")
                'daSMSstatus.Fill(dsSMS, "sms_msg_status")

                'dsSMS.Relations.Add("RelSMS", dsSMS.Tables("sms_msg_status").Columns("smsmsg_msg_status"), dsSMS.Tables("sms_message").Columns("smsmsg_msg_status"), True)
                If (dt.Rows.Count > 0) Then
                    strLoc = dt.Rows(0).Item(0).ToString()
                End If



            Catch sqlex As SqlException
                strErr = sqlex.Message
            Catch ex As Exception
                strErr = ex.ToString
            Finally
                conn.Close()
            End Try

            conn.Dispose()

        End Using

        Return strLoc

    End Function

#End Region

#Region "Get Macau SMS Letter section"

    Public Function getMcuSmsMsgDetail(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal policyCap As String, ByRef strErr As String) As DataSet

        Dim strSQL As String = ""
        Dim dsSMS As DataSet = New DataSet
        Dim daSMS As SqlDataAdapter


        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode) 'New SqlConnection("server=hksqlUAT1;database=MCUCIWU101;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30") '

            strSQL = "Select smsmsg_req_date, smsmsg_eff_date, smsmsg_exp_date, smsmsg_msg_status, smsmsg_sent_date, smsmsg_int_refno, smsmsg_mob_no, smsmsg_msg_content " & _
                " From sms_message where smsmsg_poli_no in (@policyNo1 , @policyCap1) and smsmsg_poli_no <> '' " & _
                " Union all " & _
                " Select smsmsd_create_date, smsmsd_eff_date, smsmsd_exp_date, smsmsd_msg_status, smsmsd_eff_date, '', smsmsd_mob_no, smsmsd_msg_content " & _
                " From sms_message_details where smsmsd_poli_no in (@policyNo2 , @policyCap2) and smsmsd_poli_no <> '' and smsmsd_category not in ('iLASSMS')" & _
                " order by smsmsg_req_date"
            'Excluding iLAS SMS End

            strSQL = strSQL & "; Select * from sms_msg_status"

            'Add relations to the datatables in dataset
            Try

                daSMS = New SqlDataAdapter(strSQL, conn)
                daSMS.SelectCommand.CommandType = CommandType.Text

                daSMS.SelectCommand.Parameters.Add("@policyNo1", SqlDbType.VarChar, 100).Value = policyNo
                daSMS.SelectCommand.Parameters.Add("@policyCap1", SqlDbType.VarChar, 100).Value = policyCap
                daSMS.SelectCommand.Parameters.Add("@policyNo2", SqlDbType.VarChar, 100).Value = policyNo
                daSMS.SelectCommand.Parameters.Add("@policyCap2", SqlDbType.VarChar, 100).Value = policyCap

                daSMS.MissingSchemaAction = MissingSchemaAction.AddWithKey
                daSMS.MissingMappingAction = MissingMappingAction.Passthrough
                daSMS.TableMappings.Add("sms_message1", "sms_msg_status")
                daSMS.Fill(dsSMS, "sms_message")

                'daSMSmsg.Fill(dsSMS, "sms_message")
                'daSMSstatus.Fill(dsSMS, "sms_msg_status")

                dsSMS.Relations.Add("RelSMS", dsSMS.Tables("sms_msg_status").Columns("smsmsg_msg_status"), dsSMS.Tables("sms_message").Columns("smsmsg_msg_status"), True)

            Catch sqlex As SqlException
                strErr = sqlex.Message
            Catch ex As Exception
                strErr = ex.ToString
            End Try

        End Using

        Return dsSMS


    End Function


    Public Function getMcuSMSPolicyInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal dsSMS As DataSet, ByVal mcCamDB As String, ByVal mcNbr As String, ByRef strErr As String) As DataSet

        Dim strSQL As String = ""
        'Dim ds As DataSet = New DataSet
        Dim da As SqlDataAdapter

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            strSQL = "select "
            strSQL += "DATEDIFF(hour,g.DateOfBirth,n.BOUND_DATE)/8766 as Age, "
            strSQL += "o.lookup_desc as EduLevel, "
            strSQL += "CASE p.question_answer "
            strSQL += "  WHEN '1' THEN 'Yes' "
            strSQL += "  WHEN '2' THEN 'No' "
            strSQL += "  END   "
            strSQL += "as HaveRegIncome, "
            strSQL += "q.LastCallBy, q.LastCallTime, "
            strSQL += "q.NoOfDiffDays_NVC_WelcomeCall, q.NoOfDiffDays_VC_PostSaleCall, q.NoOfDiffDays_ILAS_PostSaleCall, "
            strSQL += "isnull(r.CallStatus, 'Incompleted') as CallStatus, "
            strSQL += "t.CIWPT_IsLinked as IsILAS, "
            'strSQL += "a.PolicyAccountID, b.Description, c.ChineseDescription, j.submit_date,  "
            strSQL += "a.PolicyAccountID, b.Description, c.ChineseDescription,  "
            strSQL += "d.ExhibitInforceDate, e.CustomerID as PH_CustomerID,  "
            'ITDCPI SMS number adding area code start 
            'strSQL += "g.PhoneMobile as PH_PhoneMobile,g.EmailAddr as PH_EmailAddress, g.gender as PH_Gender," 'Original code
            strSQL += "g.PhoneMobile as PH_PhoneMobile, g.PhoneMobileareacode as PH_AreaCode, g.EmailAddr as PH_EmailAddress, g.gender as PH_Gender,"
            'ITDCPI SMS number adding area code end
            strSQL += "g.NamePrefix as Salutation, "
            strSQL += "g.NameSuffix + ' ' + g.FirstName as PH_Name_Eng,  "
            strSQL += "g.NameSuffix  as PH_LastName_Eng,  "
            strSQL += "g.ChiLstNm  as PH_LastName_Chi,  "
            strSQL += "g.ChiLstNm + g.ChiFstNm as PH_Name_Chi, "
            strSQL += "g.usechiInd as PH_UseChi,"
            strSQL += "Case  "
            strSQL += "when (g.GovernmentIDCard  <> '' or aa.CountryCode = 'HK') then 'HK'  "
            strSQL += "when aa.CountryCode = 'PR' then 'PRC' "
            strSQL += "else 'Others' "
            strSQL += "end as Region, "
            strSQL += "v.cswpad_add1, v.cswpad_add2, v.cswpad_add3, v.cswpad_city, aa.AddrProof, "
            strSQL += "h.NameSuffix + ' ' + h.FirstName as PI_Name_Eng,  "
            strSQL += "h.ChiLstNm + h.ChiFstNm as PI_Name_Chi, "
            strSQL += "m.camaid_sort_key as BranchRegion, "
            strSQL += "u.LocationCode, "
            strSQL += "l.AgentCode as AgentCode, "
            strSQL += "l.ChiLstNm + l.ChiFstNm  as SA_Name_Chi, "
            strSQL += "l.NameSuffix + ' ' + l.FirstName  as SA_Name_Eng, "
            strSQL += "y.ChiLstNm + y.ChiFstNm  as WA_Name_Chi, "
            strSQL += "y.NameSuffix + ' ' + y.FirstName  as WA_Name_Eng, "
            strSQL += "n.BOUND_DATE as AppSignDate, "
            strSQL += "a.PolicyCurrency, a.Mode as PayMode, a.ModalPremium,  "
            strSQL += "CASE n.SUITABILITY  "
            strSQL += "  WHEN '91001' THEN 'A' "
            strSQL += "  WHEN '91002' THEN 'B' "
            strSQL += "  WHEN '91003' THEN 'C' "
            strSQL += "  ELSE null "
            strSQL += "  END "
            strSQL += "as Suitability,  "
            strSQL += "i.cswpuw_flook_dline as CoolOff_ExpDate, "
            strSQL += "s.AccountStatus, "
            strSQL += "d.PREMIUMCESSATIONDATE as PremExpDate, "
            strSQL += "d.ExpiryDate as BeneExpDate, "
            strSQL += "d.SumInsured,  "
            strSQL += "z.BoosterAmount "
            strSQL += "from PolicyAccount as a  "
            strSQL += "Left outer join Product as b on a.ProductID = b.ProductID  "
            strSQL += "Left outer join Product_Chi as c on a.ProductID = c.ProductID "
            strSQL += "Inner join Coverage as d on a.PolicyAccountID = d.PolicyAccountID and a.ProductID = d.ProductID and d.Trailer = 1 "
            strSQL += "left outer join (select CustomerID, PolicyAccountID from csw_poli_rel where PolicyRelateCode = 'PH') as e on a.PolicyAccountID = e.PolicyAccountID "
            strSQL += "left outer join (select CustomerID, PolicyAccountID from csw_poli_rel where PolicyRelateCode = 'PI') as f on a.PolicyAccountID = f.PolicyAccountID "
            strSQL += "left outer join Customer as g on e.CustomerID = g.CustomerID "
            strSQL += "left outer join Customer as h on f.CustomerID = h.CustomerID "
            strSQL += "left outer join csw_policy_uw as i on i.cswpuw_poli_id = a.PolicyAccountID "
            '--strSQL += "left outer join (select CR_Policy, MIN(CR_SUBDATE) as submit_date from " & GC & "CCS_Rider group by CR_POLICY) as j on j.CR_POLICY = a.PolicyAccountID  "
            strSQL += "left outer join (select CustomerID, PolicyAccountID from csw_poli_rel where PolicyRelateCode = 'SA') as k on a.PolicyAccountID = k.PolicyAccountID "
            strSQL += "left outer join Customer as l on l.CustomerID = k.CustomerID "
            strSQL += "left outer join " & mcCamDB & ".dbo.cam_agent_info_dirmgr as m on m.camaid_agent_no = l.AgentCode "
            strSQL += "left outer join INDIV_AGMT as n on n.EXT_REF = a.PolicyAccountID "
            strSQL += "left outer join ( "
            strSQL += "select policy_no, a.form_id, c.lookup_desc from " & mcNbr & "..fna_form as a  "
            strSQL += "left outer join (select policy_no, MAX(cast(form_id as int)) as MaxFormID from " & mcNbr & "..fna_policy where status = 'A' group by policy_no) as b on a.form_id = b.MaxFormID "
            strSQL += "left outer join " & mcNbr & "..fna_lookup c on a.education_level = c.lookup_value and c.lookup_id = '0000000014' "
            strSQL += "where a.status = 'A' ) as o on o.policy_no = a.PolicyAccountID "
            strSQL += "left outer join ( "
            strSQL += "select policy_no, a.form_id, a.question_answer from " & mcNbr & "..fna_form_detail as a  "
            strSQL += "left outer join (select policy_no, MAX(cast(form_id as int)) as MaxFormID from " & mcNbr & "..fna_policy where status = 'A' group by policy_no) as b on a.form_id = b.MaxFormID "
            strSQL += "left outer join " & mcNbr & "..fna_lookup c on a.question_answer = c.lookup_value and c.lookup_id = '0000000001' "
            strSQL += "where a.status = 'A' and a.question_id='0000004002') as p on p.policy_no = a.PolicyAccountID "
            strSQL += "left outer join ( "
            strSQL += "select PolicyAccountID, LastCallBy, LastCallTime, "
            strSQL += "SUM(NoOfDiffDays_NVC_WelcomeCall) as NoOfDiffDays_NVC_WelcomeCall,  "
            strSQL += "SUM(NoOfDiffDays_VC_PostSaleCall) as NoOfDiffDays_VC_PostSaleCall,  "
            strSQL += "SUM(NoOfDiffDays_ILAS_PostSaleCall) as NoOfDiffDays_ILAS_PostSaleCall "
            strSQL += "from "
            strSQL += "(  "
            strSQL += "select a.PolicyAccountID,  "
            strSQL += "b.updateuser as LastCallBy, b.updatedatetime as LastCallTime, "
            strSQL += "a.EventIniDate as NoOfDiffDays_NVC_WelcomeCall,  "
            strSQL += "0 as NoOfDiffDays_VC_PostSaleCall, "
            strSQL += "0 as NoOfDiffDays_ILAS_PostSaleCall "
            strSQL += "from ( "
            strSQL += "select PolicyAccountID, count(distinct CONVERT(date, EventInitialDateTime, 120)) as EventIniDate,  "
            strSQL += "MAX(ServiceEventNumber) as MaxServiceNo "
            strSQL += "from ServiceEventDetail where EventCategoryCode = '86' and PolicyAccountID <> '' "
            strSQL += "group by PolicyAccountID) as a inner join  "
            strSQL += "ServiceEventDetail as b on a.MaxServiceNo = b.ServiceEventNumber "
            strSQL += "union all "
            strSQL += "select a.PolicyAccountID,  "
            strSQL += "b.updateuser as LastCallBy, b.updatedatetime as LastCallTime,   "
            strSQL += "0 as NoOfDiffDays_NVC_WelcomeCall,  "
            strSQL += "a.EventIniDate as NoOfDiffDays_VC_PostSaleCall, "
            strSQL += "0 as NoOfDiffDays_ILAS_PostSaleCall "
            strSQL += "from ( "
            strSQL += "select PolicyAccountID, count(distinct CONVERT(date, EventInitialDateTime, 120)) as EventIniDate,  "
            strSQL += "MAX(ServiceEventNumber) as MaxServiceNo "
            strSQL += "from ServiceEventDetail where EventCategoryCode = '88' and PolicyAccountID <> '' "
            strSQL += "group by PolicyAccountID) as a inner join  "
            strSQL += "ServiceEventDetail as b on a.MaxServiceNo = b.ServiceEventNumber "
            strSQL += "union all "
            strSQL += "select a.PolicyAccountID,  "
            strSQL += "b.updateuser as LastCallBy, b.updatedatetime as LastCallTime,  "
            strSQL += "0 as NoOfDiffDays_NVC_WelcomeCall,  "
            strSQL += "0 as NoOfDiffDays_VC_PostSaleCall, "
            strSQL += "a.EventIniDate as NoOfDiffDays_ILAS_PostSaleCall "
            strSQL += "from ( "
            strSQL += "select PolicyAccountID, count(distinct CONVERT(date, EventInitialDateTime, 120)) as EventIniDate,  "
            strSQL += "MAX(ServiceEventNumber) as MaxServiceNo "
            strSQL += "from ServiceEventDetail where EventCategoryCode = '91' and PolicyAccountID <> '' "
            strSQL += "group by PolicyAccountID) as a inner join  "
            strSQL += "ServiceEventDetail as b on a.MaxServiceNo = b.ServiceEventNumber "
            strSQL += ") as c group by PolicyAccountID, LastCallBy, LastCallTime "
            strSQL += ") as q on q.PolicyAccountID = a.PolicyAccountID "
            strSQL += "left outer join ( "
            strSQL += "select PolicyAccountID, EventCategoryCode, Case when COUNT(*) > 0 then 'Completed' else 'Incompleted' end as CallStatus from ServiceEventDetail "
            strSQL += "where EventCategoryCode in ('86', '88', '91') and EventTypeCode  in ('40', '50', '60') and  PolicyAccountID <> '' "
            strSQL += "group by PolicyAccountID, EventCategoryCode "
            strSQL += ") as r on r.PolicyAccountID = a.PolicyAccountID "
            strSQL += "left outer join AccountStatusCodes as s on s.AccountStatusCode = a.AccountStatusCode "
            strSQL += "left join ciwpr_product_type as t on t.CIWPT_ProductID = a.ProductID "
            strSQL += "inner join AgentCodes as u on l.AgentCode = u.AgentCode "
            strSQL += "left outer join csw_policy_address as v on v.cswpad_poli_id = a.PolicyAccountID "
            strSQL += "left outer join  "
            strSQL += "(select a.PolicyAccountID, MIN(b.CustomerID) as WritingAgentID from PolicyAccount a left join csw_poli_rel b on  "
            strSQL += "a.PolicyAccountID  = b.PolicyAccountID "
            strSQL += "and b.PolicyRelateCode = 'WA' group by a.PolicyAccountID) as x on x.PolicyAccountID = a.PolicyAccountID "
            strSQL += "left outer join Customer as y on y.CustomerID = x.WritingAgentID "
            strSQL += "left outer join ( "
            strSQL += "select PolicyAccountID, ModalPremium as BoosterAmount from CIWPR_Product_Type   "
            strSQL += "inner join Coverage on substring(CIWPT_ProductID, 1, 4)  = coverage.ProductID  "
            strSQL += "where CIWPT_IsBooster = 'Y' "
            strSQL += "and CoverageStatus in ('1', '2', '3', '4', 'v', 'x')) as z on z.PolicyAccountID = a.PolicyAccountID "
            strSQL += "left outer join ( "
            strSQL += "SELECT cliRole.PolicyAccountId, addr.AddrProof, addr.CountryCode FROM la_client_role cliRole  "
            strSQL += "INNER JOIN csw_LACIWMAP laMap ON cliRole.LAClientNum = laMap.CLNTNUM  "
            strSQL += "INNER JOIN CustomerAddress addr ON laMap.CIW_NO = addr.CustomerID AND laMap.Addr_Type = addr.AddressTypeCode  "
            strSQL += "WHERE cliRole.LAClientRole = 'DA' "
            strSQL += "union all "
            strSQL += "SELECT  cliRole.PolicyAccountId, addr.AddrProof, addr.CountryCode FROM capsil_client_role cliRole "
            strSQL += "INNER JOIN ORDCNA capMap ON cliRole.CapClientNum = capMap.CNANO  "
            strSQL += "INNER JOIN CustomerAddress addr ON capMap.CNACIW = addr.CustomerID AND capMap.CNAEAT = addr.AddressTypeCode "
            strSQL += "WHERE cliRole.RiderNo = '00' AND cliRole.CapClientRole = 'O') as aa on aa.PolicyAccountId = a.PolicyAccountID "
            strSQL += "where a.PolicyAccountID = @policyNo "

            Try

                da = New SqlDataAdapter(strSQL, conn)
                da.SelectCommand.CommandType = CommandType.Text

                da.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey
                da.MissingMappingAction = MissingMappingAction.Passthrough
                da.Fill(dsSMS, "PolicyInfo")


            Catch sqlex As SqlException
                strErr = sqlex.Message
            Catch ex As Exception
                strErr = ex.ToString
            End Try

        End Using

        Return dsSMS

    End Function


#End Region

    Public Function GetNCB(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByRef strErr As String) As DataTable

        Dim strSQL As String = ""
        Dim strLoc As String = ""
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable = New DataTable
        dt.TableName = "resultTbl"


        strSQL = "SELECT EffectiveDate, PolicyCurr, DivAmount, Percentage FROM Ciw_la_auto_NCB (NOLOCK) WHERE PolicyNo = @strPolicy"

        Using conn As SqlConnection = New SqlConnection("") 'getSettingConnectionStr(strCompanyCode, strEnvCode)

            conn.Open()

            Try

                da = New SqlDataAdapter(strSQL, conn)
                da.SelectCommand.CommandType = CommandType.Text

                da.SelectCommand.Parameters.Add("@strPolicy", SqlDbType.VarChar, 100).Value = policyNo

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey
                da.MissingMappingAction = MissingMappingAction.Passthrough
                da.Fill(dt)


            Catch sqlex As SqlException
                strErr = sqlex.Message
            Catch ex As Exception
                strErr = ex.ToString
            Finally
                conn.Close()
            End Try

            conn.Dispose()

        End Using

        Return dt


    End Function

    ' Flora Leung, Project Leo Goal 3, 18-Jan-2012 Start
#Region "DDA/CCDR"
    Public Function GetDDRRejectReason(ByVal dsSendData As Data.DataSet, ByRef dsReceData As Data.DataSet, _
                               ByRef strErr As String) As Boolean
        Try
            Dim i As Int16 = 0
            Dim j As Int16 = 0

            ' Get DDR Reject Reason by Policy No and Mandate Ref

            Dim strSQL As String = ""
            Dim dt As New DataTable
            Dim strPolicyNo As String = ""
            Dim strMandateRef As String = ""

            strPolicyNo = dsSendData.Tables(0).Rows(0).Item(0)
            strMandateRef = dsSendData.Tables(0).Rows(0).Item(1)

            strSQL = "select top 1 POSRDD_Rej_Code "
            strSQL = strSQL + "from POS_Reject_DDA_Detail "
            strSQL = strSQL + "where POSRDD_Policy_No = '" & strPolicyNo & "' and POSRDD_Mandate_Ref = '" & strMandateRef & "' "
            strSQL = strSQL + "order by POSRDD_Batch_ID desc, POSRDD_DDA_Ref desc "

            objDBHeader.CompanyID = objMQQueHeader.CompanyID
            objDBHeader.EnvironmentUse = objMQQueHeader.EnvironmentUse
            objDBHeader.UserType = objMQQueHeader.UserType
            objDBHeader.UserID = objMQQueHeader.UserID
            objDBHeader.ProjectAlias = objMQQueHeader.ProjectAlias

            dsReceData = New DataSet

            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Rej_Code")
            dtReceData.Columns.Add("Rej_Code_Desc")
            dtReceData.TableName = "Reject_Code"
            dsReceData.Tables.Add(dtReceData)

            If Exec(strSQL, "POS", dt, strErr) Then
                If dt.Rows.Count > 0 Then

                    Dim dr As DataRow = dsReceData.Tables(0).NewRow()
                    Dim strRejCode As String = ""
                    Dim dtRej As New DataTable

                    strRejCode = dt.Rows(0).Item(0).ToString
                    dr.Item("Rej_Code") = dt.Rows(0).Item(0)

                    Dim strRejSQL As String = ""
                    strRejSQL = "select DDARejectReasonDesc from DDARejectReasonCodes where DDARejectReasonCode = '" & strRejCode & "'"

                    If Exec(strRejSQL, "CIW", dtRej, strErr) Then
                        If dtRej.Rows.Count > 0 Then
                            dr.Item("Rej_Code_Desc") = dtRej.Rows(0).Item(0)
                        Else
                            dr.Item("Rej_Code_Desc") = ""
                        End If
                    Else
                        dr.Item("Rej_Code_Desc") = ""
                        Return False
                    End If

                    dsReceData.Tables(0).Rows.Add(dr)
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            strErr = ex.Message
            Return False
        End Try
    End Function
#End Region
    ' Flora Leung, Project Leo Goal 3, 18-Jan-2012 End

#Region "Post sales call"

    Public Function RetrievePostSalesCallQuestions(ByRef dt As DataTable, ByRef strErr As String) As Boolean
        Dim strSQL As String = "select * from csw_post_sales_call_questionnaire"
        If Exec(strSQL, "CIW", dt, strErr) = False Then
            Return False
        End If

        dt.TableName = "csw_post_sales_call_questionnaire"

        Return True
    End Function

    Public Function SavePostSalesCallQuestion(ByVal intMode As Integer, ByRef dtQuestion As DataTable, ByRef strErr As String) As Boolean
        Dim strSqlInsert As String = "insert into csw_post_sales_call_questionnaire (cswpsq_questionnaire_code,cswpsq_question_no,cswpsq_description,cswpsq_answer_type,cswpsq_answer_template,cswpsq_vc,cswpsq_sm,cswpsq_create_by,cswpsq_create_date,cswpsq_update_by,cswpsq_update_date, cswpsq_no_fna) values ('{0}','{1}',N'{2}','{3}',{4},'{5}','{6}','{7}',GETDATE(),'{8}',GETDATE(), '{9}');select SCOPE_IDENTITY() as id"
        Dim strSqlUpdate As String = "update csw_post_sales_call_questionnaire set cswpsq_questionnaire_code='{0}', cswpsq_question_no='{1}', cswpsq_description=N'{2}',cswpsq_answer_type='{3}',cswpsq_answer_template={4},cswpsq_vc='{5}',cswpsq_sm='{6}',cswpsq_update_by='{7}',cswpsq_update_date=GETDATE(),cswpsq_no_fna='{9}' where cswpsq_question_id={8}"
        Dim strSql As String = Nothing
        Dim dtID As DataTable = Nothing

        Select Case intMode
            Case 1
                strSql = String.Format(strSqlInsert, dtQuestion.Rows(0)("cswpsq_questionnaire_code"), dtQuestion.Rows(0)("cswpsq_question_no"), _
                            dtQuestion.Rows(0)("cswpsq_description"), dtQuestion.Rows(0)("cswpsq_answer_type"), dtQuestion.Rows(0)("cswpsq_answer_template"), _
                            dtQuestion.Rows(0)("cswpsq_vc"), dtQuestion.Rows(0)("cswpsq_sm"), objDBHeader.UserID, objDBHeader.UserID, dtQuestion.Rows(0)("cswpsq_no_fna"))

                If Exec(strSql, "CIW", dtID, strErr) = False Then
                    Return False
                End If

                dtQuestion.Rows(0)("cswpsq_question_id") = dtID.Rows(0)(0)

            Case 2
                strSql = String.Format(strSqlUpdate, dtQuestion.Rows(0)("cswpsq_questionnaire_code"), dtQuestion.Rows(0)("cswpsq_question_no"), _
                        dtQuestion.Rows(0)("cswpsq_description"), dtQuestion.Rows(0)("cswpsq_answer_type"), dtQuestion.Rows(0)("cswpsq_answer_template"), _
                        dtQuestion.Rows(0)("cswpsq_vc"), dtQuestion.Rows(0)("cswpsq_sm"), objDBHeader.UserID, dtQuestion.Rows(0)("cswpsq_question_id"), dtQuestion.Rows(0)("cswpsq_no_fna"))

                If ExecNonQuery(strSql, "CIW", strErr) = False Then
                    Return False
                End If
        End Select

        Return True
    End Function

    Public Function IsPostSalesCallQuestionInUse(ByVal intQuestionId As Integer, ByRef isUsed As Boolean, ByRef strErr As String) As Boolean
        Dim strSql As String = String.Format("select top 1 * from csw_post_sales_call_product_questions where cswpsn_question_id={0}", intQuestionId)
        Dim dt As DataTable = Nothing

        If Exec(strSql, "CIW", dt, strErr) = False Then
            Return False
        End If

        If dt.Rows.Count > 0 Then
            isUsed = True
        Else
            isUsed = False
        End If

        Return True
    End Function

    Public Function DeletePostSalesCallQuestion(ByVal intQuestionId As Integer, ByRef strErr As String) As Boolean
        Dim strSql As String = String.Format("delete csw_post_sales_call_questionnaire where cswpsq_question_id={0}", intQuestionId)
        Dim dt As DataTable = Nothing

        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function RetrievePostSalesCallProductSettings(ByRef dsSettings As DataSet, ByRef strErr As String) As Boolean
        Dim strSql As String
        Dim dtQuestions As DataTable = Nothing
        Dim dtProduct As DataTable = Nothing
        Dim dtCategory As DataTable = Nothing

        strSql = "select * from csw_post_sales_call_product_questions a inner join csw_post_sales_call_questionnaire b on a.cswpsn_question_id=b.cswpsq_question_id"
        If Exec(strSql, "CIW", dtQuestions, strErr) = False Then
            Return False
        End If
        dtQuestions.TableName = "Questions"

        strSql = "select a.*, b.Description, c.ChineseDescription from csw_post_sales_call_product_setting a inner join Product b on a.cswpsd_ProductID=b.ProductID left outer join Product_Chi c on a.cswpsd_ProductID=c.ProductID"
        If Exec(strSql, "CIW", dtProduct, strErr) = False Then
            Return False
        End If
        dtProduct.TableName = "Products"

        If Not RetrievePostSalesCallProductCategory(dtCategory, strErr) Then
            Return False
        End If

        dsSettings = New DataSet()
        dsSettings.Tables.Add(dtProduct)
        dsSettings.Tables.Add(dtQuestions)
        dsSettings.Tables.Add(dtCategory)

        Return True
    End Function

    Public Function RetrievePostSalesCallProductCategory(ByRef dtCategory As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = "select * from csw_post_sales_call_product_category"
        If Exec(strSql, "CIW", dtCategory, strErr) = False Then
            Return False
        End If
        dtCategory.TableName = "Category"

        Return True
    End Function

    Public Function UpdatePostSalesCallProductSetting(ByVal dtSetting As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String = Nothing
        Dim sbSql As New System.Text.StringBuilder()

        EscapeSqlSingleQuote(dtSetting)

        sbSql.Append("update csw_post_sales_call_product_setting set ")
        sbSql.AppendFormat(" cswpsd_category={0},", dtSetting.Rows(0)("cswpsd_category"))
        sbSql.AppendFormat(" cswpsd_benefit=N'{0}',", dtSetting.Rows(0)("cswpsd_benefit").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_premium_type=N'{0}',", dtSetting.Rows(0)("cswpsd_premium_type").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_loan=N'{0}',", dtSetting.Rows(0)("cswpsd_loan").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_dividend=N'{0}',", dtSetting.Rows(0)("cswpsd_dividend").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_coupon=N'{0}',", dtSetting.Rows(0)("cswpsd_coupon").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_special_bonus=N'{0}',", dtSetting.Rows(0)("cswpsd_special_bonus").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_cash_value=N'{0}',", dtSetting.Rows(0)("cswpsd_cash_value").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_guide_url='{0}',", dtSetting.Rows(0)("cswpsd_guide_url").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_others=N'{0}',", dtSetting.Rows(0)("cswpsd_others").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_has_fees=N'{0}',", dtSetting.Rows(0)("cswpsd_has_fees").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_fees=N'{0}',", dtSetting.Rows(0)("cswpsd_fees").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_ng_benefit=N'{0}',", dtSetting.Rows(0)("cswpsd_ng_benefit").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_surr_charge={0},", IIf(dtSetting.Rows(0)("cswpsd_surr_charge") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_charge")))
        sbSql.AppendFormat(" cswpsd_surr_period={0},", IIf(dtSetting.Rows(0)("cswpsd_surr_period") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_period")))
        sbSql.AppendFormat(" cswpsd_product_type_eng='{0}',", dtSetting.Rows(0)("cswpsd_product_type_eng").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_product_type_chi=N'{0}',", dtSetting.Rows(0)("cswpsd_product_type_chi").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_product_objective_eng='{0}',", dtSetting.Rows(0)("cswpsd_product_objective_eng").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_product_objective_chi=N'{0}',", dtSetting.Rows(0)("cswpsd_product_objective_chi").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_risk_eng='{0}',", dtSetting.Rows(0)("cswpsd_risk_eng").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_risk_chi=N'{0}',", dtSetting.Rows(0)("cswpsd_risk_chi").ToString().Trim())
        sbSql.AppendFormat(" cswpsd_update_by='{0}',", objDBHeader.UserID)
        sbSql.Append(" cswpsd_update_date=getdate()")
        sbSql.AppendFormat(" where cswpsd_ProductID='{0}'", dtSetting.Rows(0)("cswpsd_ProductID"))

        strSql = sbSql.ToString()
        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function DeletePostSalesCallProductSetting(ByVal strProductId As String, ByRef strErr As String) As Boolean
        Dim strSql(1) As String

        strSql(0) = String.Format("delete csw_post_sales_call_product_setting where cswpsd_ProductID='{0}'", strProductId)
        strSql(1) = String.Format("delete csw_post_sales_call_product_questions where cswpsn_ProductID='{0}'", strProductId)

        If ExecNonQuerys(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function AddPostSalesCallProductSetting(ByVal dtSetting As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String = Nothing
        Dim sbSql As New System.Text.StringBuilder()

        EscapeSqlSingleQuote(dtSetting)

        sbSql.Append("insert into csw_post_sales_call_product_setting (")
        sbSql.Append(" cswpsd_ProductID,")
        sbSql.Append(" cswpsd_category,")
        sbSql.Append(" cswpsd_benefit,")
        sbSql.Append(" cswpsd_premium_type,")
        sbSql.Append(" cswpsd_loan,")
        sbSql.Append(" cswpsd_dividend,")
        sbSql.Append(" cswpsd_coupon,")
        sbSql.Append(" cswpsd_special_bonus,")
        sbSql.Append(" cswpsd_cash_value,")
        sbSql.Append(" cswpsd_guide_url,")
        sbSql.Append(" cswpsd_others,")
        sbSql.Append(" cswpsd_has_fees,")
        sbSql.Append(" cswpsd_fees,")
        sbSql.Append(" cswpsd_ng_benefit,")
        sbSql.Append(" cswpsd_surr_charge,")
        sbSql.Append(" cswpsd_surr_period,")
        sbSql.Append(" cswpsd_product_type_eng,")
        sbSql.Append(" cswpsd_product_type_chi,")
        sbSql.Append(" cswpsd_product_objective_eng,")
        sbSql.Append(" cswpsd_product_objective_chi,")
        sbSql.Append(" cswpsd_risk_eng,")
        sbSql.Append(" cswpsd_risk_chi,")
        sbSql.Append(" cswpsd_create_by,")
        sbSql.Append(" cswpsd_create_date,")
        sbSql.Append(" cswpsd_update_by,")
        sbSql.Append(" cswpsd_update_date")
        sbSql.Append(") values (")
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_ProductID"))
        sbSql.AppendFormat("{0},", dtSetting.Rows(0)("cswpsd_category"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_benefit"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_premium_type"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_loan"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_dividend"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_coupon"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_special_bonus"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_cash_value"))
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_guide_url"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_others"))
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_has_fees"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_fees"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_ng_benefit"))
        sbSql.AppendFormat("{0},", IIf(dtSetting.Rows(0)("cswpsd_surr_charge") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_charge")))
        sbSql.AppendFormat("{0},", IIf(dtSetting.Rows(0)("cswpsd_surr_period") Is DBNull.Value, "NULL", dtSetting.Rows(0)("cswpsd_surr_period")))
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_product_type_eng"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_product_type_chi"))
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_product_objective_eng"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_product_objective_chi"))
        sbSql.AppendFormat("'{0}',", dtSetting.Rows(0)("cswpsd_risk_eng"))
        sbSql.AppendFormat("N'{0}',", dtSetting.Rows(0)("cswpsd_risk_chi"))
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID)
        sbSql.Append("getdate(),")
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID)
        sbSql.Append("getdate()")
        sbSql.Append(")")

        strSql = sbSql.ToString()
        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function RetrievePostSalesCallProducts(ByVal strProductID As String, ByVal strPlanName As String, ByRef dtProduct As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = "select * from Product a left outer join Product_Chi b on a.ProductID=b.ProductID where a.CompanyID='EAA'"
        If strPlanName IsNot String.Empty Then
            strSql &= String.Format(" and a.Description like '%{0}%'", strPlanName.Trim())
        End If

        If strProductID IsNot String.Empty Then
            strSql &= String.Format(" and a.ProductID like '%{0}%'", strProductID.Trim())
        End If

        If Exec(strSql, "CIW", dtProduct, strErr) = False Then
            Return False
        End If
        dtProduct.TableName = "Product"

        Return True
    End Function

    Public Function CopySalesCallProductSetting(ByVal strFromProductID As String, ByVal strToProductID As String, ByRef strErr As String) As Boolean
        Dim strSql(1) As String
        Dim sbSql As New Text.StringBuilder()

        sbSql.Append("insert into csw_post_sales_call_product_setting (")
        sbSql.Append(" cswpsd_ProductID,")
        sbSql.Append(" cswpsd_category,")
        sbSql.Append(" cswpsd_benefit,")
        sbSql.Append(" cswpsd_premium_type,")
        sbSql.Append(" cswpsd_loan,")
        sbSql.Append(" cswpsd_dividend,")
        sbSql.Append(" cswpsd_coupon,")
        sbSql.Append(" cswpsd_special_bonus,")
        sbSql.Append(" cswpsd_cash_value,")
        sbSql.Append(" cswpsd_guide_url,")
        sbSql.Append(" cswpsd_others,")
        sbSql.Append(" cswpsd_has_fees,")
        sbSql.Append(" cswpsd_fees,")
        sbSql.Append(" cswpsd_ng_benefit,")
        sbSql.Append(" cswpsd_surr_charge,")
        sbSql.Append(" cswpsd_surr_period,")
        sbSql.Append(" cswpsd_product_type_eng,")
        sbSql.Append(" cswpsd_product_type_chi,")
        sbSql.Append(" cswpsd_product_objective_eng,")
        sbSql.Append(" cswpsd_product_objective_chi,")
        sbSql.Append(" cswpsd_risk_eng,")
        sbSql.Append(" cswpsd_risk_chi,")
        sbSql.Append(" cswpsd_create_by,")
        sbSql.Append(" cswpsd_create_date,")
        sbSql.Append(" cswpsd_update_by,")
        sbSql.Append(" cswpsd_update_date")
        sbSql.Append(") select")
        sbSql.AppendFormat(" '{0}',", strToProductID)
        sbSql.Append(" cswpsd_category,")
        sbSql.Append(" cswpsd_benefit,")
        sbSql.Append(" cswpsd_premium_type,")
        sbSql.Append(" cswpsd_loan,")
        sbSql.Append(" cswpsd_dividend,")
        sbSql.Append(" cswpsd_coupon,")
        sbSql.Append(" cswpsd_special_bonus,")
        sbSql.Append(" cswpsd_cash_value,")
        sbSql.Append(" cswpsd_guide_url,")
        sbSql.Append(" cswpsd_others,")
        sbSql.Append(" cswpsd_has_fees,")
        sbSql.Append(" cswpsd_fees,")
        sbSql.Append(" cswpsd_ng_benefit,")
        sbSql.Append(" cswpsd_surr_charge,")
        sbSql.Append(" cswpsd_surr_period,")
        sbSql.Append(" cswpsd_product_type_eng,")
        sbSql.Append(" cswpsd_product_type_chi,")
        sbSql.Append(" cswpsd_product_objective_eng,")
        sbSql.Append(" cswpsd_product_objective_chi,")
        sbSql.Append(" cswpsd_risk_eng,")
        sbSql.Append(" cswpsd_risk_chi,")
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID)
        sbSql.Append("getdate(),")
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID)
        sbSql.Append("getdate()")
        sbSql.AppendFormat(" from csw_post_sales_call_product_setting where cswpsd_ProductID='{0}'", strFromProductID)

        strSql(0) = sbSql.ToString()

        sbSql = New Text.StringBuilder()
        sbSql.AppendLine("insert into csw_post_sales_call_product_questions (")
        sbSql.AppendLine("cswpsn_ProductID,")
        sbSql.AppendLine("cswpsn_question_id,")
        sbSql.AppendLine("cswpsn_order")
        sbSql.AppendLine(") select ")
        sbSql.AppendFormat("'{0}',", strToProductID) : sbSql.AppendLine()
        sbSql.AppendLine("cswpsn_question_id,")
        sbSql.AppendLine("cswpsn_order")
        sbSql.AppendFormat(" from csw_post_sales_call_product_questions where cswpsn_ProductID='{0}'", strFromProductID)

        strSql(1) = sbSql.ToString()

        If ExecNonQuerys(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function AddPostSalesCallProductQuestion(ByVal dtQuestion As DataTable, ByRef isExists As Boolean, ByRef strErr As String) As Boolean
        Dim strSql As String = Nothing
        Dim sbSql As New Text.StringBuilder()
        Dim dtCount As DataTable = Nothing

        isExists = False

        strSql = String.Format("select * from csw_post_sales_call_product_questions where cswpsn_ProductID='{0}' and cswpsn_question_id={1}", dtQuestion.Rows(0)("cswpsn_ProductID"), dtQuestion.Rows(0)("cswpsn_question_id"))
        If Not Exec(strSql, "CIW", dtCount, strErr) Then
            Return False
        End If

        If dtCount.Rows.Count > 0 Then
            isExists = True
            Return True
        End If

        sbSql.AppendLine("insert into csw_post_sales_call_product_questions (")
        sbSql.AppendLine("cswpsn_ProductID,")
        sbSql.AppendLine("cswpsn_question_id,")
        sbSql.AppendLine("cswpsn_order,")
        sbSql.AppendLine("cswpsn_create_by,")
        sbSql.AppendLine("cswpsn_create_date,")
        sbSql.AppendLine("cswpsn_update_by,")
        sbSql.AppendLine("cswpsn_update_date")
        sbSql.AppendLine(") values (")
        sbSql.AppendFormat("'{0}',", dtQuestion.Rows(0)("cswpsn_ProductID")) : sbSql.AppendLine()
        sbSql.AppendFormat("{0},", dtQuestion.Rows(0)("cswpsn_question_id")) : sbSql.AppendLine()
        sbSql.AppendFormat("{0},", IIf(dtQuestion.Rows(0)("cswpsn_order") Is DBNull.Value, "NULL", dtQuestion.Rows(0)("cswpsn_order"))) : sbSql.AppendLine()
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID) : sbSql.AppendLine()
        sbSql.AppendLine("getdate(),")
        sbSql.AppendFormat("'{0}',", objDBHeader.UserID) : sbSql.AppendLine()
        sbSql.AppendLine("getdate()")
        sbSql.Append(")")

        strSql = sbSql.ToString()
        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function RemovePostSalesCallProductQuestion(ByVal strProductId As String, ByVal intQuestionId As Integer, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = String.Format("delete csw_post_sales_call_product_questions where cswpsn_ProductID='{0}' and cswpsn_question_id={1}", strProductId, intQuestionId)
        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function SetPostSalesCallProductQuestionOrder(ByVal strProductId As String, ByVal intQuestionId As Integer, ByVal intNewOrder As Integer, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = String.Format("update csw_post_sales_call_product_questions set cswpsn_order={2}, cswpsn_update_by='{3}', cswpsn_update_date=getdate() where cswpsn_ProductID='{0}' and cswpsn_question_id={1}", strProductId, intQuestionId, intNewOrder, objDBHeader.UserID)
        If ExecNonQuery(strSql, "CIW", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function IsPostSalesCallProductSettingExists(ByVal strProductId As String, ByRef isExists As Boolean, ByRef strErr As String) As Boolean
        Dim strSql As String
        Dim dt As DataTable = Nothing

        strSql = String.Format("select * from csw_post_sales_call_product_setting where cswpsd_ProductID='{0}'", strProductId)
        If Exec(strSql, "CIW", dt, strErr) = False Then
            Return False
        End If

        If dt.Rows.Count > 0 Then
            isExists = True
        Else
            isExists = False
        End If

        Return True
    End Function

    Public Function GetCoverage(ByVal strPolicyNo As String, ByRef dtCov As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String
        Dim sbSql As New Text.StringBuilder()

        sbSql.AppendLine("select cov.*, s.*, p.Description, chi.ChineseDescription")
        sbSql.AppendLine("from Coverage cov ")
        sbSql.AppendLine("inner join csw_post_sales_call_product_setting s on cov.ProductID=s.cswpsd_ProductID ")
        sbSql.AppendLine("inner join Product p on cov.ProductID=p.ProductID")
        sbSql.AppendLine("left outer join Product_Chi chi on p.ProductID=chi.ProductID")
        sbSql.AppendFormat("where PolicyAccountID='{0}'", strPolicyNo) : sbSql.AppendLine()
        sbSql.AppendLine("order by cov.Trailer")

        strSql = sbSql.ToString()
        If Exec(strSql, "CIW", dtCov, strErr) = False Then
            Return False
        End If

        dtCov.TableName = "Coverage"

        Return True
    End Function

    Public Function RetrievePostSalesCallPolicyQuestion(ByVal strPolicyNo As String, ByRef dtQuestions As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String
        Dim sbSql As New Text.StringBuilder()
        Dim callType As PostSalesCallType

        If Not GetPolicyPostSalesCallType(strPolicyNo, callType, strErr) Then
            Return False
        End If

        sbSql.AppendLine("select distinct a.PolicyAccountID, c.cswpsq_questionnaire_code, c.cswpsq_question_id, c.cswpsq_question_no, c.cswpsq_description, ans.cswpca_answer_value")
        sbSql.AppendLine("from Coverage a ")
        sbSql.AppendLine("inner join csw_post_sales_call_product_questions b on a.ProductID=b.cswpsn_ProductID and a.CoverageStatus in ('1','2','3','4','V','X')")
        sbSql.AppendLine("inner join csw_post_sales_call_questionnaire c on b.cswpsn_question_id=c.cswpsq_question_id")
        sbSql.AppendLine("left outer join csw_post_sales_call_answer ans on ans.cswpca_policy_no=a.PolicyAccountID and ans.cswpca_question_id=c.cswpsq_question_id")
        sbSql.AppendFormat("where a.PolicyAccountID='{0}' and (cswpsq_questionnaire_code='ILASCALL' ", strPolicyNo) : sbSql.AppendLine()

        If callType = PostSalesCallType.Welcome OrElse callType = PostSalesCallType.iLAS Then
            sbSql.AppendLine(")") ' No question for non-iLAS welcome call

        ElseIf (callType And PostSalesCallType.NoFNA) = PostSalesCallType.NoFNA Then
            sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_no_fna='Y'))")
        ElseIf (callType And PostSalesCallType.VulnerableCustomer) = PostSalesCallType.VulnerableCustomer Then
            sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_vc = 'Y'))")
        ElseIf (callType And PostSalesCallType.SuitabilityMismatch) = PostSalesCallType.SuitabilityMismatch Then
            sbSql.AppendLine("or (cswpsq_questionnaire_code='NONILAS' and c.cswpsq_sm = 'Y'))")
        End If

        strSql = sbSql.ToString()

        If Exec(strSql, "CIW", dtQuestions, strErr) = False Then
            Return False
        End If

        dtQuestions.TableName = "PolicyQuestion"

        Return True

    End Function

    ''' <summary>
    ''' Check the post-sales call type of policy.
    ''' Checking sequence:
    ''' 1. iLAS
    ''' 2. VC (No FNA)
    ''' 3. SM
    ''' 4. VC
    ''' 
    ''' Possibile combination:
    ''' Welcome = Non-iLAS coverage only
    ''' VC/SM/No FNA = Non-iLAS coverage only, and need post sales call
    ''' iLAS = iLAS coverage only
    ''' iLAS + VC/SM/No FNA = iLAS + non-iLAS coverage
    ''' </summary>
    ''' <param name="strPolicyNo"></param>
    ''' <param name="callType"></param>
    ''' <param name="strErr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPolicyPostSalesCallType(ByVal strPolicyNo As String, ByRef callType As PostSalesCallType, ByRef strErr As String) As Boolean
        Dim sbSql As Text.StringBuilder
        Dim strSql As String
        Dim dt As DataTable = Nothing
        Dim strCiwDb As String

        ' default
        callType = PostSalesCallType.Welcome

        ' check iLAS call
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select * from Coverage a inner join CIWPR_Product_Type b on a.ProductID = b.CIWPT_ProductID")
        sbSql.AppendLine("where b.CIWPT_IsLinked='Y' and a.CoverageStatus in ('1','2','3','4','V','X')")
        sbSql.AppendFormat("and a.PolicyAccountID='{0}'", strPolicyNo)
        strSql = sbSql.ToString()
        If Exec(strSql, "CIW", dt, strErr) = False Then
            Return False
        End If

        If dt.Rows.Count >= 1 Then
            callType += PostSalesCallType.iLAS
        End If

        ' check if non-iLAS coverage attached
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select * from Coverage a inner join CIWPR_Product_Type b on a.ProductID = b.CIWPT_ProductID")
        sbSql.AppendLine("where b.CIWPT_IsLinked='N' and a.CoverageStatus in ('1','2','3','4','V','X')")
        sbSql.AppendFormat("and a.PolicyAccountID='{0}'", strPolicyNo)
        strSql = sbSql.ToString()
        If Exec(strSql, "CIW", dt, strErr) = False Then
            Return False
        End If

        'ITSR933 FG R4 EnvSetup Gary Lei Start
        If DBHeader.EnvironmentUse.StartsWith("PRD") Or DBHeader.EnvironmentUse.Contains("106") Or DBHeader.EnvironmentUse.Contains("ITR") Then
            'ITSR933 FG R4 EnvSetup Gary Lei End
            strCiwDb = "POS"
        Else
            strCiwDb = DBHeader.CompanyID & "POS" & DBHeader.EnvironmentUse
        End If

        If dt.Rows.Count > 0 Then
            ' check if no fna form for non-iLAS
            strSql = String.Format("select top 1 '1' from fna_policy a inner join fna_form b on a.form_id=b.form_id where policy_no='{0}' and a.status='A' and b.status='A' " &
                                    "union all select top 1 '2' from " & strCiwDb & "..csw_policy_admin_fna where cswpaf_policy_no='{0}'", strPolicyNo)
            If Exec(strSql, "NBR", dt, strErr) = False Then
                Return False
            End If

            If dt.Rows.Count = 0 Then
                callType += PostSalesCallType.NoFNA
            Else
                ' check VC/SM
                'If objDBHeader.EnvironmentUse.StartsWith("PRD") Then
                '    If objDBHeader.CompanyID = "ING" Then
                '        strNBR = "NBR"
                '    ElseIf objDBHeader.CompanyID = "MCU" Then
                '        strNBR = "NBM"
                '    Else
                '        strErr = "Unsupported company"
                '        Return False
                '    End If

                'Else
                '    strNBR = objDBHeader.CompanyID & "NBR" & objDBHeader.EnvironmentUse
                'End If
                'sbSql = New System.Text.StringBuilder()
                'sbSql.AppendLine("select rel.PolicyAccountID, ph.DateOfBirth, agmt.bound_date,")
                'sbSql.AppendLine("case when (DATEDIFF(hour,PH.DateOfBirth,agmt.BOUND_DATE)/8766 > 65 or fna_dtl.question_answer<>'1' or o.lookup_desc = 'Primary or below' or o.lookup_desc is null or fna.BankVC = 'Y') and PH.Gender <> 'C' then 'Y' else 'N' end as isVC,")
                'sbSql.AppendLine("fna.SuitabilitymisMatch")
                'sbSql.AppendLine("from csw_poli_rel rel")
                'sbSql.AppendLine("inner join Customer PH on rel.CustomerID=PH.CustomerID and rel.PolicyRelateCode='PH'")
                'sbSql.AppendLine("inner join INDIV_AGMT agmt on rel.PolicyAccountID=EXT_REF")
                'sbSql.AppendLine("left outer join (")
                'sbSql.AppendLine("	select policy_no, a.form_id, a.IsFE as FNAExempted, a.IsBVC as BankVC , a.IsSM as SuitabilityMismatch, a.education_level")
                'sbSql.AppendLine("	from " & strNBR & ".dbo.fna_form a")
                'sbSql.AppendLine("	inner join (select policy_no, MAX(cast(form_id as int)) as MaxFormID from " & strNBR & ".dbo.fna_policy where status = 'A' group by policy_no) as b on a.form_id = b.MaxFormID      ")
                'sbSql.AppendLine("	where a.status = 'A'")
                'sbSql.AppendLine(") fna on rel.PolicyAccountID=fna.policy_no")
                'sbSql.AppendLine("left outer join " & strNBR & ".dbo.fna_form_detail fna_dtl on fna.form_id=fna_dtl.form_id and fna_dtl.question_id='0000004002' and fna_dtl.status='A'")
                'sbSql.AppendLine("left outer join " & strNBR & ".dbo.fna_lookup o on fna.education_level = o.lookup_value and o.lookup_id = '0000000014'")
                'sbSql.AppendFormat("where rel.PolicyAccountID='{0}'", strPolicyNo)

                'strSql = sbSql.ToString()
                Using cmd As New SqlClient.SqlCommand("cswsp_GetPostSalesCallList")
                    cmd.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = strPolicyNo
                    cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = DBNull.Value
                    cmd.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = DBNull.Value
                    cmd.CommandType = CommandType.StoredProcedure
                    If Exec(cmd, "CIW", dt, strErr) = False Then
                        Return False
                    End If
                End Using

                If dt.Rows.Count > 0 Then
                    ' Check SM
                    If dt.Rows(0)("CallType").ToString().Trim() = "Suitability Mismatch" Then
                        callType += PostSalesCallType.SuitabilityMismatch
                    Else
                        ' Check VC
                        If dt.Rows(0)("CallType").ToString().Trim() = "VC Call" Then
                            callType += PostSalesCallType.VulnerableCustomer
                        End If
                    End If
                End If

            End If
        End If

        Return True
    End Function

    Public Function SavePostSalesCallPolicyAnswer(ByVal dtQuestion As DataTable, ByRef strErr As String) As Boolean
        Dim sbSql As Text.StringBuilder
        Dim strSql As String
        Dim dtTmp As DataTable = Nothing

        For Each drQuestion As DataRow In dtQuestion.Rows
            ' check if the answer record exists
            strSql = String.Format("select * from csw_post_sales_call_answer where cswpca_policy_no='{0}' and cswpca_question_id={1}", drQuestion("PolicyAccountID").ToString().Trim(), drQuestion("cswpsq_question_id"))
            If Not Exec(strSql, "CIW", dtTmp, strErr) Then
                Return False
            End If

            If dtTmp.Rows.Count = 0 Then
                ' insert record
                sbSql = New Text.StringBuilder()
                sbSql.AppendLine("insert into csw_post_sales_call_answer (")
                sbSql.AppendLine("cswpca_policy_no,")
                sbSql.AppendLine("cswpca_question_id,")
                sbSql.AppendLine("cswpca_answer_value,")
                sbSql.AppendLine("cswpca_answer_text,")
                sbSql.AppendLine("cswpca_create_by,")
                sbSql.AppendLine("cswpca_create_date,")
                sbSql.AppendLine("cswpca_update_by,")
                sbSql.AppendLine("cswpca_update_date")
                sbSql.AppendLine(") values (")
                sbSql.AppendFormat("'{0}',", drQuestion("PolicyAccountID").ToString().Trim()) : sbSql.AppendLine()
                sbSql.AppendFormat("{0},", drQuestion("cswpsq_question_id")) : sbSql.AppendLine()
                sbSql.AppendFormat("'{0}',", drQuestion("cswpca_answer_value").ToString().Trim()) : sbSql.AppendLine()
                sbSql.AppendLine("NULL,")
                sbSql.AppendFormat("'{0}',", objDBHeader.UserID) : sbSql.AppendLine()
                sbSql.AppendLine("GETDATE(),")
                sbSql.AppendFormat("'{0}',", objDBHeader.UserID) : sbSql.AppendLine()
                sbSql.AppendLine("GETDATE())")

                strSql = sbSql.ToString()
            Else
                ' update record
                strSql = String.Format("update csw_post_sales_call_answer set cswpca_answer_value='{2}' where cswpca_policy_no='{0}' and cswpca_question_id={1}", drQuestion("PolicyAccountID").ToString().Trim(), drQuestion("cswpsq_question_id"), drQuestion("cswpca_answer_value").ToString().Trim())
            End If

            If Not ExecNonQuery(strSql, "CIW", strErr) Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Function IsPostSalesCallCompleted(ByVal strPolicyNo As String, ByRef blnCompleted As Boolean, ByRef strErr As String) As Boolean
        Dim strServiceEventCategory As String = "'86', '88', '93', '91'"
        Dim sbSql As New System.Text.StringBuilder()
        Dim strSQL As String = Nothing
        Dim dtServiceLog As New DataTable()

        blnCompleted = False

        ' service event log
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select top 1 * from ServiceEventDetail")
        sbSql.AppendLine("where EventCategoryCode in (" & strServiceEventCategory & ")")
        sbSql.AppendFormat("and PolicyAccountID='{0}'", strPolicyNo) : sbSql.AppendLine()
        sbSql.AppendLine("order by EventInitialDateTime desc, ServiceEventNumber desc")

        strSQL = sbSql.ToString()
        If Not Exec(strSQL, "CIW", dtServiceLog, strErr) Then
            Return False
        End If

        If dtServiceLog.Rows.Count = 0 Then
            Return True
        End If

        If dtServiceLog.Rows(0)("EventStatusCode").ToString.Trim = "C" AndAlso _
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() <> "10" AndAlso _
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() <> "20" Then

            blnCompleted = True
            Return True
        End If

        Return True
    End Function

    Public Function RetrieveInvestmentFund(ByRef dtFund As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = "select * from mpf_investment"

        If Exec(strSql, "RMLIFE", dtFund, strErr) = False Then
            Return False
        End If

        dtFund.TableName = "mpf_investment"

        Return True
    End Function

    Public Function UpdateFundRiskLevel(ByVal dtFund As DataTable, ByRef strErr As String) As Boolean
        Dim strSql As String

        strSql = String.Format("update mpf_investment set mpfinv_risk_level={1}, mpfinv_last_upd_datetime=GETDATE(), mpfinv_last_upd_user='{2}' where mpfinv_code='{0}'", dtFund.Rows(0)("mpfinv_code"), IIf(dtFund.Rows(0)("mpfinv_risk_level") Is DBNull.Value, "NULL", "'" & dtFund.Rows(0)("mpfinv_risk_level").ToString().Trim() & "'"), objDBHeader.UserID)

        If ExecNonQuery(strSql, "RMLIFE", strErr) = False Then
            Return False
        End If

        Return True
    End Function

    Public Function RetrievePolicyHighRiskFund(ByVal strPolicyNo As String, ByRef dtFund As DataTable, ByRef strErr As String) As Boolean
        Dim sbSql As New Text.StringBuilder()
        Dim strSql As String
        Dim strRMLIFE As String

        'ITSR933 FG R4 EnvSetup Gary Lei Start
        If objDBHeader.EnvironmentUse.StartsWith("PRD") Or objDBHeader.EnvironmentUse.Contains("106") Or DBHeader.EnvironmentUse.Contains("ITR") Then
            'ITSR933 FG R4 EnvSetup Gary Lei End
            If objDBHeader.CompanyID = "ING" Then
                strRMLIFE = "RMLIFE"
            ElseIf objDBHeader.CompanyID = "MCU" Then
                strRMLIFE = "MRMLIFE"
            Else
                strErr = "Unsupported company"
                Return False
            End If
        Else
            strRMLIFE = objDBHeader.CompanyID & "RMLIFE" & objDBHeader.EnvironmentUse
        End If

        sbSql.AppendLine("select distinct inv.mpfinv_code, inv.mpfinv_chi_desc, inv.mpfinv_desc")
        sbSql.AppendLine("from ciwvw_fund_allocation a inner join " & strRMLIFE & "..mpf_investment inv on a.cswcfa_fund_code=inv.mpfinv_code")
        sbSql.AppendFormat("where a.cswcfa_policy_no='{0}' and inv.mpfinv_risk_level='H'", strPolicyNo)

        strSql = sbSql.ToString()
        If Exec(strSql, "CIW", dtFund, strErr) = False Then
            Return False
        End If

        dtFund.TableName = "mpf_investment"

        Return True
    End Function

    <Flags()> _
    Public Enum PostSalesCallType As Integer
        Welcome = 0
        iLAS = 1
        VulnerableCustomer = 2
        SuitabilityMismatch = 4
        NoFNA = 8
    End Enum

#End Region

#Region "Get Macau QDAP refund extra Paymenr Alert Records"

    Function getMcuQDAPRefundExtraPaymentAlert(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal policyNo As String, ByVal custID As String, ByVal dtBusDate As DateTime, ByRef strErr As String) As DataTable

        Dim strSQL As String = ""
        Dim dsRefuExtPay As DataSet = New DataSet
        Dim daRefuExtPay As SqlDataAdapter

        Dim resultDt As DataTable = New DataTable()
        resultDt.TableName = "qdapRefuExtrPayTbl"

        Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            strSQL += "SELECT pol.PolicyAccountid, pol.RCD from PolicyAccount pol "
            strSQL += "INNER JOIN csw_poli_rel rel ON rel.PolicyAccountID = pol.PolicyAccountID AND rel.PolicyRelateCode ='PH' "
            strSQL += "WHERE pol.ProductID IN ('UQA1', 'UQA2') "
            strSQL += "AND CONVERT(VARCHAR, @dtBusDate, 112) < CONVERT(VARCHAR, DATEADD(YEAR, 1, pol.RCD), 112) "

            If policyNo.Trim <> "" Then
                strSQL += "AND pol.PolicyAccountid = @policyNo"
            End If

            If custID.Trim <> "" Then
                strSQL += "AND rel.CustomerID = @custId"
            End If

            'Add relations to the datatables in dataset
            Try

                daRefuExtPay = New SqlDataAdapter(strSQL, conn)
                daRefuExtPay.SelectCommand.CommandType = CommandType.Text

                daRefuExtPay.SelectCommand.Parameters.Add("@dtBusDate", SqlDbType.VarChar, 100).Value = dtBusDate.ToString("yyyy-MM-dd")
                daRefuExtPay.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 100).Value = policyNo
                daRefuExtPay.SelectCommand.Parameters.Add("@custId", SqlDbType.VarChar, 100).Value = custID

                daRefuExtPay.MissingSchemaAction = MissingSchemaAction.AddWithKey
                daRefuExtPay.MissingMappingAction = MissingMappingAction.Passthrough

                daRefuExtPay.Fill(resultDt)

            Catch sqlex As SqlException
                strErr = sqlex.Message
            Catch ex As Exception
                strErr = ex.ToString
            End Try

        End Using

        Return resultDt

    End Function

#End Region



#Region "Service Log Retrieve"

    'For special retrieval
    Public Function GetSerLogbyCusIdOrCriteria(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByVal isDescOrder As Boolean, ByRef strErr As String, ByVal strType As String) As DataTable

        Dim dtSerLog As New DataTable
        dtSerLog.TableName = "SerLog"

        Dim paramDict As New Dictionary(Of String, String)
        If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
            strErr = "Cannot Search without Start Date or CustomerID."
            Return dtSerLog
        End If
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        strSQL = "Select distinct t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator],t1.customerid, "
        strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
        strSQL += "PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end, "
        strSQL += "PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end, "
        strSQL += "ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end, "
        strSQL += "t15.Accountstatus as [Policy status], t2.RCD, "
        strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes,t1.alertnotes, "
        strSQL += "t12.FirstName+' '+t12.NameSuffix as [Service Agent],t13.agentCode As [Agent Code], t13.LocationCode, "
        strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV, "
        strSQL += "t1.caseno as [Suggestions/Grievances],t16.eStatement ,t1.ServiceEventNumber "
        strSQL += "From ServiceEventDetail t1 "
        strSQL += "left join ServiceEventDetailMCU_Extend (nolock) mcu on t1.ServiceEventNumber = mcu.ServiceEventNumber "
        strSQL += "left join PolicyAccount (nolock) t2 on t1.PolicyAccountID=t2.PolicyAccountID "
        strSQL += "left join PRODUCT (nolock) t3 on t2.ProductID=t3.ProductiD "
        strSQL += "left join GI_PRODUCT (nolock) t4 on t2.ProductID=t4.ProductiD "
        strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
        strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
        strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
        strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
        strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
        strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
        strSQL += "left join csw_poli_rel (nolock) t11 on t1.policyaccountid=t11.PolicyAccountID and t11.PolicyRelateCode='SA' "
        strSQL += "left join customer (nolock) t12 on t11.CustomerID=t12.CustomerID "
        strSQL += "left join AgentCodes (nolock) t13 on t13.agentcode=t12.AgentCode "
        strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
        strSQL += "left join AccountStatusCodes (nolock) t15 on t15.accountstatuscode=t2.AccountstatusCode "
        strSQL += "left join " & gcPOS & "PolicyEstatement (nolock) t16 on t1.policyaccountid=t16.PolicyId "
        strSQL += "where t1.EventSourceInitiatorCode<>'' "
        If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
            strSQL += "and t1.customerid= @custId "
            paramDict.Add("@custId", strcustId)
        Else
            If Not IsDBNull(StartDate) Then
                strSQL += "and t1.EventInitialDateTime >= @startDate "
                paramDict.Add("@startDate", StartDate.ToString("yyyy-MM-dd"))
            End If
            If Not IsDBNull(EndDate) Then
                strSQL += "and t1.EventInitialDateTime <= @endDate "
                paramDict.Add("@endDate", EndDate.ToString("yyyy-MM-dd"))
            End If
            If strMediumCode <> String.Empty Then
                strSQL += "and t1.EventSourceMediumCode= @mediumCode "
                paramDict.Add("@mediumCode", strMediumCode)
            End If
            If strEvtCatCode <> String.Empty Then
                strSQL += "and t1.EventCategoryCode= @evtCatCode "
                paramDict.Add("@evtCatCode", strEvtCatCode)
            End If
            If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
                strSQL += "and t1.customerid<>'0' "
            End If
        End If

        strSQL += "and ((@isMcu = 'MC' and mcu.ServiceEventNumber is not null) or (@isMcu = 'HK' and mcu.ServiceEventNumber is null)) "
        paramDict.Add("@isMcu", strType)

        If isDescOrder Then
            strSQL += "Order by t1.EventInitialDateTime desc "
        Else
            strSQL += "Order by t1.EventInitialDateTime asc "
        End If

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                'Using cnn As New SqlConnection(objDBHeader.ConnectionAlias)

                cnn.Open()
                Using daSerlog As New SqlDataAdapter(strSQL, cnn)
                    For Each pair As KeyValuePair(Of String, String) In paramDict
                        daSerlog.SelectCommand.Parameters.Add(pair.Key, SqlDbType.VarChar).Value = pair.Value
                    Next
                    daSerlog.Fill(dtSerLog)
                    Return dtSerLog
                    'dsResult.Tables.Add(dtSerLog)
                    'If (dsResult Is Nothing) Then
                    '    strErr = "Cannot get Service Log Detail!"
                    '    Return False
                    'Else
                    '    Return True
                    'End If
                End Using
            End Using
        Catch ex As Exception
            strErr = ex.Message
            Return dtSerLog
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            'Return False
        End Try
    End Function

#End Region

#Region "Service Log Retrieve"

    'For special retrieval
    Public Function GetSerLogbycriteria(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByRef strErr As String, ByVal strType As String) As DataTable
        Dim dtSerLog As New DataTable
        dtSerLog.TableName = "SerLog"

        Dim paramDict As New Dictionary(Of String, String)
        If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
            strErr = "Cannot Search without Start Date or CustomerID."
            Return dtSerLog
        End If
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        strSQL = "Select distinct @poicyMainType as [PolicyMainType], t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator],t1.customerid, "
        strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
        strSQL += "PolicyAccountNo=case when @poicyMainType = 'Assurance' then PolicyNo_Asur else case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end end, "
        strSQL += "PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end, "
        strSQL += "ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end, "
        strSQL += "t15.Accountstatus as [Policy status], t2.RCD, "
        strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes,t1.alertnotes, "
        strSQL += "t12.FirstName+' '+t12.NameSuffix as [Service Agent],t13.agentCode As [Agent Code], t13.LocationCode, "
        strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV, "
        strSQL += "t1.caseno as [Suggestions/Grievances],t16.eStatement ,t1.ServiceEventNumber "
        strSQL += "From ServiceEventDetail t1 "
        strSQL += "left join ServiceEventDetailMCU_Extend (nolock) mcu on t1.ServiceEventNumber = mcu.ServiceEventNumber "
        strSQL += "left join ServiceEventDetailHK_Extend (nolock) hk on t1.ServiceEventNumber = hk.ServiceEventNumber "
        strSQL += "left join PolicyAccount (nolock) t2 on t1.PolicyAccountID=t2.PolicyAccountID "
        strSQL += "left join PRODUCT (nolock) t3 on t2.ProductID=t3.ProductiD "
        strSQL += "left join GI_PRODUCT (nolock) t4 on t2.ProductID=t4.ProductiD "
        strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
        strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
        strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
        strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
        strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
        strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
        strSQL += "left join csw_poli_rel (nolock) t11 on t1.policyaccountid=t11.PolicyAccountID and t11.PolicyRelateCode='SA' "
        strSQL += "left join customer (nolock) t12 on t11.CustomerID=t12.CustomerID "
        strSQL += "left join AgentCodes (nolock) t13 on t13.agentcode=t12.AgentCode "
        strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
        strSQL += "left join AccountStatusCodes (nolock) t15 on t15.accountstatuscode=t2.AccountstatusCode "
        strSQL += "left join " & gcPOS & "PolicyEstatement (nolock) t16 on t1.policyaccountid=t16.PolicyId "
        strSQL += "where t1.EventSourceInitiatorCode<>'' "
        If Not IsDBNull(StartDate) Then
            strSQL += "and t1.EventInitialDateTime >= @startDate "
            paramDict.Add("@startDate", StartDate.ToString("yyyy-MM-dd"))
        End If

        If Not IsDBNull(EndDate) Then
            strSQL += "and t1.EventInitialDateTime <= @endDate "
            paramDict.Add("@endDate", EndDate.ToString("yyyy-MM-dd"))
        End If

        If strMediumCode <> String.Empty Then
            strSQL += "and t1.EventSourceMediumCode= @mediumCode "
            paramDict.Add("@mediumCode", strMediumCode)
        End If

        If strEvtCatCode <> String.Empty Then
            strSQL += "and t1.EventCategoryCode= @evtCatCode "
            paramDict.Add("@evtCatCode", strEvtCatCode)
        End If

        If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
            strSQL += "and t1.customerid= @custId "
            paramDict.Add("@custId", strcustId)
        Else
            If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
                strSQL += "and t1.customerid<>'0' "
            End If
        End If

        strSQL += "and ((@poicyMainType = 'Macau' and mcu.ServiceEventNumber is not null) or (@poicyMainType in ('Bermuda', 'Assurance', 'GI', 'EB') and mcu.ServiceEventNumber is null)) "
        strSQL += "and ( (@poicyMainType = 'Macau') "
        strSQL += "or (@poicyMainType = 'Assurance' and isnull(t1.PolicyNo_Asur, '') <> '') "
        strSQL += "or (@poicyMainType = 'EB' and t2.companyid in ('EB','GL','LTD')) "
        strSQL += "or (@poicyMainType = 'GI' and t2.CompanyID = 'GI') "
        strSQL += "or (@poicyMainType = 'Bermuda' and (t2.CompanyID is null or t2.CompanyID not in ('EB','GL','LTD', 'GI')) and isnull(t1.PolicyNo_Asur, '') = '')) "
        paramDict.Add("@poicyMainType", strType)

        strSQL += "Order by t1.EventInitialDateTime desc "

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                'Using cnn As New SqlConnection(objDBHeader.ConnectionAlias)

                cnn.Open()
                Using daSerlog As New SqlDataAdapter(strSQL, cnn)
                    For Each pair As KeyValuePair(Of String, String) In paramDict
                        daSerlog.SelectCommand.Parameters.Add(pair.Key, SqlDbType.VarChar).Value = pair.Value
                    Next
                    daSerlog.Fill(dtSerLog)
                    Return dtSerLog
                    'dsResult.Tables.Add(dtSerLog)
                    'If (dsResult Is Nothing) Then
                    '    strErr = "Cannot get Service Log Detail!"
                    '    Return False
                    'Else
                    '    Return True
                    'End If
                End Using
            End Using
        Catch ex As Exception
            strErr = ex.Message
            Return dtSerLog
            'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
            'Return False
        End Try
    End Function
#End Region



#Region "Policy Alternation"
    Public Function GetMarkin(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtMarkin As DataTable, ByRef strErrMsg As String) As Boolean

        Dim strSQL As String

        strSQL = "Select convert(integer,Cswreg_markin_id) as 'Markin_ID', Cswreg_policy_no as 'Policy_no', Cswttc_desc as 'Type',  " &
            " Cswreg_markin_date as 'Markin_Date', Cswreg_markout_date as 'Markout_Date', Cswreg_system as 'System', " &
            " Cswtsc_desc as 'Status', Cswreg_rmk as 'Remark', Cswreg_upd_usr as 'User_ID' " &
            " From csw_req_reg, csw_txn_type_code, csw_txn_status_code " &
            " Where Cswreg_txn_type = Cswttc_txn_type " &
            " And Cswreg_status = Cswtsc_txn_status " &
            " and Cswreg_policy_no = '" & strPolicy & "' " &
            " Order by Cswreg_markin_id desc "

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Using sqlda As New SqlDataAdapter(strSQL, cnn)
                    sqlda.Fill(dtMarkin)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            strErrMsg = "GetMarkin - " & ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Function GetMarkinHist(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtMarkinHist As DataTable, ByRef strErrMsg As String) As Boolean

        Dim strSQL As String

        strSQL = "Select Cswregl_log_id as 'Log_id', Cswregl_action as 'Action_Code', convert(integer,Cswregl_markin_id) as 'Markin_ID', " &
            " Cswregl_policy_no as 'Policy_no', Cswttc_desc as 'Type', Cswregl_markin_date as 'Markin_Date', " &
            " Cswregl_markout_date as 'Markout_Date', Cswregl_system as 'System', Cswregl_status as 'Status', " &
            " Cswregl_rmk as 'Remark', Cswregl_crt_usr as 'Create_User', Cswregl_crt_date as 'Create_date', " &
            " Cswregl_upd_usr as 'Last_Update_User', Cswregl_upd_date as 'Last_Update_Date' " &
            " From csw_req_reg_log, csw_txn_type_code " &
            " Where Cswregl_txn_type = Cswttc_txn_type " &
            " And Cswregl_policy_no = '" & strPolicy & "'"

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Using sqlda As New SqlDataAdapter(strSQL, cnn)
                    sqlda.Fill(dtMarkinHist)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            strErrMsg = "GetMarkin - " & ex.ToString
            Return False
        End Try

    End Function

    Public Function GetPendingMarkin(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtPendingMarkin As DataTable, ByRef strErrMsg As String) As Boolean

        Dim strSQL As String

        strSQL = "Select posp_markin_id, posp_policy_no, posp_indate, posp_pending_date, posp_seqno, posp_pending_code, " &
            " posp_pending_desc, posp_resolve_code, posp_resolve_desc, posp_resolve_indicator, posp_resolve_date, " &
            " posp_deadline, posp_remark, posp_internal_remarks, posp_first_rem_date, posp_final_rem_date " &
            " From pos_pending " &
            " Where posp_policy_no = '" & strPolicy & "' " &
            " Order By posp_markin_id desc, posp_seqno desc"

        'sqlconnect.ConnectionString = strPOSConn
        'sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Using sqlda As New SqlDataAdapter(strSQL, cnn)
                    sqlda.Fill(dtPendingMarkin)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            strErrMsg = "GetMarkin - " & ex.ToString
            Return False
        End Try
    End Function

    Public Function GetPendingMarkinHist(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByRef dtPendingMarkinHist As DataTable, ByRef strErrMsg As String) As Boolean

        Dim strSQL As String

        strSQL = "Select * from pos_pending_log " &
            " Where pospl_policy_no='" & strPolicy & "'"

        'sqlconnect.ConnectionString = strPOSConn
        'sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
            cnn.Open()
            Using sqlda As New SqlDataAdapter(strSQL, cnn)
                Try
                    sqlda.Fill(dtPendingMarkinHist)
                    Return True
                Catch sqlex As SqlClient.SqlException
                    strErrMsg = "GetMarkin - " & sqlex.ToString
                    Return False
                Catch ex As Exception
                    strErrMsg = "GetMarkin - " & ex.ToString
                    Return False
                Finally
                    cnn.Close()
                End Try
            End Using
        End Using

    End Function
#End Region

    Public Function GetSerLogbycriteria(ByVal StartDate As Date, ByVal EndDate As Date, ByVal strMediumCode As String, ByVal strEvtCatCode As String, ByVal strcustId As String, ByVal includeNonCust As Boolean, ByRef strErr As String, ByVal strType As String) As DataTable
        Dim dtSerLog As New DataTable
        dtSerLog.TableName = "SerLog"

        Dim paramDict As New Dictionary(Of String, String)
        If IsDBNull(StartDate) And strcustId.Trim = String.Empty Then 'Not allow search without criteria
            strErr = "Cannot Search without Start Date or CustomerID."
            Return dtSerLog
        End If
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        strSQL = "Select distinct t1.EventInitialDateTime, t14.EventStatus,t9.EventSourceMedium as [Medium],t10.EventSourceInitiator as [Initiator],t1.customerid, "
        strSQL += "t5.cswecc_desc as [Event Category], t7.EventTypeDesc as [Event Type], t6.csw_event_typedtl_desc as [Event Type Detail], "
        strSQL += "PolicyAccountNo=case when left(t1.policyaccountid,2)='GL' then left(substring(t1.PolicyAccountID,3,LEN(t1.policyAccountId)),12) else left(t1.PolicyAccountID,12) end, "
        strSQL += "PolicyType=case when t2.companyid in ('EAA','ING') then 'LIFE' when t2.companyid in ('GI') then 'GI' when t2.companyid in ('EB','GL','LTD') then 'EB' else 'LIFE' end, "
        strSQL += "ProductName=case when t2.companyid in ('EAA','ING') then isnull(t3.Description,'') else isnull(t4.ProductName,'') end, "
        strSQL += "t15.Accountstatus as [Policy status], t2.RCD, "
        strSQL += "isnull(t8.name,'') as [CSR], t1.EventNotes,t1.ReminderNotes,t1.alertnotes, "
        strSQL += "t12.FirstName+' '+t12.NameSuffix as [Service Agent],t13.agentCode As [Agent Code], t13.LocationCode, "
        strSQL += "t1.EventCloseoutCode as [FCR] ,t1.MCV, "
        strSQL += "t1.caseno as [Suggestions/Grievances],t16.eStatement ,t1.ServiceEventNumber "
        strSQL += "From ServiceEventDetail t1 "
        strSQL += "left join ServiceEventDetailMCU_Extend (nolock) mcu on t1.ServiceEventNumber = mcu.ServiceEventNumber "
        strSQL += "left join ServiceEventDetailHK_Extend (nolock) hk on t1.ServiceEventNumber = hk.ServiceEventNumber "
        strSQL += "left join PolicyAccount (nolock) t2 on t1.PolicyAccountID=t2.PolicyAccountID "
        strSQL += "left join PRODUCT (nolock) t3 on t2.ProductID=t3.ProductiD "
        strSQL += "left join GI_PRODUCT (nolock) t4 on t2.ProductID=t4.ProductiD "
        strSQL += "left join csw_event_category_code (nolock) t5 on t1.EventCategoryCode=t5.cswecc_code "
        strSQL += "left join csw_event_typedtl_code (nolock) t6 on t1.EventCategoryCode=t6.csw_event_category_code and t1.EventTypeCode=t6.csw_event_type_code and t1.EventTypeDetailCode=t6.csw_event_typedtl_code "
        strSQL += "left join ServiceEventTypeCodes (nolock) t7 on t1.EventCategoryCode=t7.EventCategoryCode and t1.EventTypeCode=t7.EventTypeCode "
        strSQL += "left join CSR (nolock) t8 on t1.MasterCSRID=t8.CSRID "
        strSQL += "left join EventSourceMediumCodes (nolock) t9 on t1.EventSourceMediumCode=t9.EventSourceMediumCode "
        strSQL += "left join EventSourceInitiatorCodes (nolock) t10 on t1.EventSourceInitiatorCode=t10.EventSourceInitiatorCode "
        strSQL += "left join csw_poli_rel (nolock) t11 on t1.policyaccountid=t11.PolicyAccountID and t11.PolicyRelateCode='SA' "
        strSQL += "left join customer (nolock) t12 on t11.CustomerID=t12.CustomerID "
        strSQL += "left join AgentCodes (nolock) t13 on t13.agentcode=t12.AgentCode "
        strSQL += "left join EventStatusCodes (nolock) t14 on t1.EventStatusCode=t14.EventStatusCode "
        strSQL += "left join AccountStatusCodes (nolock) t15 on t15.accountstatuscode=t2.AccountstatusCode "
        strSQL += "left join " & gcPOS & "PolicyEstatement (nolock) t16 on t1.policyaccountid=t16.PolicyId "
        strSQL += "where t1.EventSourceInitiatorCode<>'' "
        If Not IsDBNull(StartDate) Then
            strSQL += "and t1.EventInitialDateTime >= @startDate "
            paramDict.Add("@startDate", StartDate.ToString("yyyy-MM-dd"))
        End If

        If Not IsDBNull(EndDate) Then
            strSQL += "and t1.EventInitialDateTime <= @endDate "
            paramDict.Add("@endDate", EndDate.ToString("yyyy-MM-dd"))
        End If

        If strMediumCode <> String.Empty Then
            strSQL += "and t1.EventSourceMediumCode= @mediumCode "
            paramDict.Add("@mediumCode", strMediumCode)
        End If

        If strEvtCatCode <> String.Empty Then
            strSQL += "and t1.EventCategoryCode= @evtCatCode "
            paramDict.Add("@evtCatCode", strEvtCatCode)
        End If

        If strcustId.Trim <> String.Empty Then 'Check history via Cust Service-log, will submit CustomerID 
            strSQL += "and t1.customerid= @custId "
            paramDict.Add("@custId", strcustId)
        Else
            If Not includeNonCust Then 'Check history via Retrive page thus will not submit CustomerID, and not select include Non-customer (CustomerID=0)  
                strSQL += "and t1.customerid<>'0' "
            End If
        End If

        strSQL += "and ((@isMcu = 'MC' and mcu.ServiceEventNumber is not null) or (@isMcu = 'HK' and mcu.ServiceEventNumber is null)) "
        paramDict.Add("@isMcu", strType)

        strSQL += "Order by t1.EventInitialDateTime desc "

        Using cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("CRSWSConnStr").ConnectionString)
            'Using cnn As New SqlConnection(objDBHeader.ConnectionAlias)

            cnn.Open()
            Using daSerlog As New SqlDataAdapter(strSQL, cnn)
                Try
                    For Each pair As KeyValuePair(Of String, String) In paramDict
                        daSerlog.SelectCommand.Parameters.Add(pair.Key, SqlDbType.VarChar).Value = pair.Value
                    Next
                    daSerlog.Fill(dtSerLog)
                    Return dtSerLog
                    'dsResult.Tables.Add(dtSerLog)
                    'If (dsResult Is Nothing) Then
                    '    strErr = "Cannot get Service Log Detail!"
                    '    Return False
                    'Else
                    '    Return True
                    'End If
                Catch ex As Exception
                    strErr = ex.Message
                    Return dtSerLog
                    'Me.AddAppEventLog("Error", "CRS", "Get_HK_ServiceLog: " & strErr, ex.StackTrace, String.Format("User: {2}", strUserID), ex.InnerException.Message, "")
                    'Return False
                Finally
                    cnn.Close()
                End Try
            End Using
        End Using
    End Function


#Region "Get Macau policy summer Agent info tab records"

    Public Function getMcuAgentInfoList(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal g_Comp As String, ByVal serName As String, ByVal strAgtList As String, ByVal camHKLAgtMap As String,
                                 ByVal camDB As String, ByVal ds As DataSet, ByRef strErrMsg As String) As DataSet

        Dim strSQL As String = ""
        Dim strPolicy As String = ""


        If strAgtList.Trim().Equals("") Then
            Return ds
        End If

        Try

            Using conn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode) 'SqlConnection(ConfigurationManager.ConnectionStrings("MC_CIWU105").ConnectionString)
                'Using conn As New SqlConnection("server=hksqlUAT2;database=MCUCIWU105;Network=DBMSSOCN;uid=com_las_ing;password=IngLA456;Connect Timeout=30")

                conn.Open()

                'strSQL = "Select * from AgentStatusCodes s, AgentCodes a " & _
                '        "LEFT JOIN cswvw_agent_info ON AgentCode = cswagi_agent_code " & _
                '        "Where AgentCode in (" & strAgtList & ") and a.AgentStatusCode = s.AgentStatusCode; "
                'strSQL &= "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
                'strSQL &= "Select * from cswvw_agent_license Where camalt_agent_no in (" & strAgtList & ")"
                'strSQL = "Select * From cswvw_cam_agent_info Where cswagi_agent_code in (" & strAgtList & "); "

                If g_Comp = "HKL" Then
                    strSQL = "select camaid_agent_no as cswagi_agent_code, camaid_sort_key as cswagi_unit_code, Camrrt_loc_code as cswagi_loc_code, " &
                            " camaid_date_join as cswagi_contract_date, camaid_date_left as cswagi_date_left, camaid_grade as cswagi_grade, " &
                            " camgpr_desc as cswagi_desc, bm.camaib_disp_name as cswagi_mgr_name, camaia_res_phone as cswagi_res_phone, " &
                            " camaia_mob_phone as cswagi_mob_phone, camaia_bus_phone as cswagi_bus_phone, camaia_fax as cswagi_fax, " &
                            " camaia_email as cswagi_email, ba.camaib_idno as cswagi_idno " &
                            ", camham_HKL_Bank+camham_HKL_AgentNo as camham_HKL_AgentNo, camham_HKL_Branch, camham_HKL_Bank " &
                            " from " & serName & ".hklcam.dbo.cam_agent_info_dirmgr, " & serName & ".hklcam.dbo.cam_grade_pkg_ref, " &
                            serName & ".hklcam.dbo.cam_agent_info_package, " & serName & ".hklcam.dbo.cam_Agent_info_address, " &
                            serName & ".hklcam.dbo.cam_rdbu_rel_tab, " & serName & ".hklcam.dbo.cam_agent_info_basic ba, " & serName & ".hklcam.dbo.cam_agent_info_basic bm " &
                            ", " & camHKLAgtMap &
                            " Where camaid_Agent_no = camaip_agent_no " &
                            " and char(camaip_per_pkg) = camgpr_per_pkg " &
                            " and camaid_grade = camgpr_grade_no " &
                            " and camaid_Agent_no = camaia_agent_no " &
                            " and camaid_sort_key = camrrt_agency_code " &
                            " and camrrt_section_no = '00000' " &
                            " and bm.camaib_agent_no = case when camaid_grade between 4 and 12 then camrrt_dir_agtno else camrrt_agent_no end " &
                            " and camaid_agent_no = ba.camaib_agent_no " &
                            " and camaid_Agent_no = camham_ING_AgentNo " &
                            " and camaid_agent_no in (" & strAgtList & "); "
                    '" and camaid_agent_no in (" & strAgtList & "); "
                Else
                    strSQL = "select camaid_agent_no as cswagi_agent_code, camaid_sort_key as cswagi_unit_code, Camrrt_loc_code as cswagi_loc_code, " &
                            " camaid_date_join as cswagi_contract_date, camaid_date_left as cswagi_date_left, camaid_grade as cswagi_grade, " &
                            " camgpr_desc as cswagi_desc, bm.camaib_disp_name as cswagi_mgr_name, camaia_res_phone as cswagi_res_phone, " &
                            " camaia_mob_phone as cswagi_mob_phone, camaia_bus_phone as cswagi_bus_phone, camaia_fax as cswagi_fax, " &
                            " camaia_email as cswagi_email, ba.camaib_idno as cswagi_idno " &
                            " from " & serName & "." & camDB & ".dbo.cam_agent_info_dirmgr, " & serName & "." & camDB & ".dbo.cam_grade_pkg_ref, " &
                            serName & "." & camDB & ".dbo.cam_agent_info_package, " & serName & "." & camDB & ".dbo.cam_Agent_info_address, " &
                            serName & "." & camDB & ".dbo.cam_rdbu_rel_tab, " & serName & "." & camDB & ".dbo.cam_agent_info_basic ba, " & serName & "." & camDB & ".dbo.cam_agent_info_basic bm " &
                            " Where camaid_Agent_no = camaip_agent_no " &
                            " and char(camaip_per_pkg) = camgpr_per_pkg " &
                            " and camaid_grade = camgpr_grade_no " &
                            " and camaid_Agent_no = camaia_agent_no " &
                            " and camaid_sort_key = camrrt_agency_code " &
                            " and camrrt_section_no = '00000' " &
                            " and bm.camaib_agent_no = case when camaid_grade between 4 and 12 then camrrt_dir_agtno else camrrt_agent_no end " &
                            " and camaid_agent_no = ba.camaib_agent_no " &
                            " and camaid_agent_no in (" & strAgtList & "); "
                End If

                strSQL &= "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
                strSQL &= "Select * from cswvw_agent_license Where camalt_agent_no in (" & strAgtList & "); "

                ' Get commission info.
                strSQL &= "Select AgentCode, CommPayType, CommAcctNo, PhoneNumber from AgentCodes where AgentCode in (" & strAgtList & "); "

                ' Load servicing CSR
                strPolicy = ds.Tables("POLINF").Rows(0).Item("PolicyAccountID")

                strSQL &= "Select * from csw_cs_policy_list where cswcpl_policy_no = @policyNo "

                Dim sqlda = New SqlDataAdapter(strSQL, conn)
                sqlda.SelectCommand.CommandType = CommandType.Text

                sqlda.SelectCommand.Parameters.Add("@AgtList1", SqlDbType.VarChar, 30).Value = strAgtList.Replace("'", "").ToString()
                sqlda.SelectCommand.Parameters.Add("@AgtList2", SqlDbType.VarChar, 30).Value = strAgtList.Replace("'", "").ToString()
                sqlda.SelectCommand.Parameters.Add("@AgtList3", SqlDbType.VarChar, 30).Value = strAgtList.Replace("'", "").ToString()
                sqlda.SelectCommand.Parameters.Add("@policyNo", SqlDbType.VarChar, 30).Value = strPolicy.Replace("'", "").ToString()


                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough

                'sqlda.TableMappings.Add("AgentStatusCodes1", "PolicyAccountRelationCodes")
                'sqlda.TableMappings.Add("AgentStatusCodes2", "cswvw_agent_license")
                sqlda.TableMappings.Add("cswvw_agent_info1", "PolicyAccountRelationCodes")
                sqlda.TableMappings.Add("cswvw_agent_info2", "cswvw_agent_license")
                sqlda.TableMappings.Add("cswvw_agent_info3", "AgentCodes")
                sqlda.TableMappings.Add("cswvw_agent_info4", "csw_cs_policy_list")
                sqlda.Fill(ds, "cswvw_agent_info")

            End Using

        Catch sqlex As SqlException
            'lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & sqlex.ToString
            Return ds
        Catch ex As Exception
            'lngErrNo = -1
            strErrMsg = "GetPolicyListCIW - " & ex.ToString
            Return ds
        Finally

        End Try

        Return ds

    End Function


    Public Function UpdHKLAgtDT(ByVal dtAgent As DataTable, ByVal strColName As String, ByVal strNewCol As String) As Boolean

        Dim dr As DataRow

        Try
            dtAgent.Columns.Add(strNewCol, Type.GetType("System.String"))
            For Each dr In dtAgent.Rows
                dr.Item(strNewCol) = "" 'MapHKLAgent("", Right(dr.Item(strColName), 5))
            Next

            dtAgent.AcceptChanges()
        Catch ex As Exception

        End Try

    End Function


    Public Function MapHKLAgent(ByVal strHKLAgt As String, ByVal strINGAgt As String, ByRef strBankCode As String, ByRef strBranchCode As String, ByVal camHKLAgentMapping As String) As String

        Dim strSQL As String
        Dim strAgt As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        If strHKLAgt <> "" Then
            ' strSQL = "select camham_ING_AgentNo from " & camHKLAgentMapping & _
            ' where camham_HKL_Bank+camham_HKL_AgentNo = '" & Replace(strHKLAgt, "'", "''") & "'"
            strSQL = "select camham_ING_AgentNo AS AgentStr from " & camHKLAgentMapping & _
                "where camham_HKL_Bank+camham_HKL_AgentNo = @HKLAgt  "
        End If

        If strINGAgt <> "" Then
            'strSQL = "select camham_HKL_Bank + camham_HKL_AgentNo + camham_HKL_Branch AS AgentStr from " & camHKLAgentMapping & _
            '    " where camham_ING_AgentNo = '" & Replace(strINGAgt, "'", "''") & "'"

            strSQL = "select camham_HKL_Bank + camham_HKL_AgentNo + camham_HKL_Branch AS AgentStr from " & camHKLAgentMapping & _
                " where camham_ING_AgentNo = @INGAgt "
        End If

        If strSQL = "" Then
            Return ""
        End If

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("MC_CIWU105").ConnectionString)

            'sqlconnect.ConnectionString = conn
            conn.Open()

            Using daMapAgt As New SqlDataAdapter(strSQL, conn)

                'New SqlDataAdapter(strSQL, cnn)

                'sqlcmd.CommandText = strSQL
                'sqlcmd.Connection = conn

                Dim _dt = New DataTable()

                Try

                    'strAgt = sqlcmd.ExecuteScalar()

                    daMapAgt.Fill(_dt)

                Catch sqlex As SqlClient.SqlException
                    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

                Finally

                    'sqlconnect.Close()
                    conn.Close()
                End Try

                'sqlcmd.Dispose()
                'sqlconnect.Dispose()
                daMapAgt.Dispose()

            End Using

        End Using

        ' Return additional information
        If strINGAgt <> "" AndAlso strAgt <> "" Then
            strBankCode = Strings.Left(strAgt, 3)
            strBranchCode = Strings.Right(strAgt, 3)
            strAgt = Strings.Left(strAgt, 9)
        End If

        Return strAgt

    End Function

#End Region



    'Method for MCU Agent Infomation
#Region "Get Load ComboBox"

    Public Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As Object, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean

        'Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("MC_CIWU105").ConnectionString)

                conn.Open()

                'sqlconnect.ConnectionString = strCIWMcuConn
                sqlda = New SqlDataAdapter(strSQL, conn)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.Fill(dt)
                blnLoad = True

                conn.Close()

            End Using

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Function
            blnLoad = False
        End Try

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If

    End Function

#End Region

#Region "Get DDA/CCDR status Code"

    Public Function getMacauDDACCDRStatusCode(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByRef strErr As String) As DataSet

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String = ""
        Dim sqlda As SqlDataAdapter = New SqlDataAdapter
        Dim ds As DataSet = New DataSet("DDACCDR")

        Using con As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            Try
                strSQL = "Select * from CCDRStatusCodes; "
                strSQL &= "Select * from DDAStatusCodes"

                'sqlconnect.ConnectionString = con

                sqlda = New SqlDataAdapter(strSQL, con)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.TableMappings.Add("CCDRStatusCodes1", "DDAStatusCodes")
                sqlda.Fill(ds, "CCDRStatusCodes")

            Catch sqlex As SqlClient.SqlException
                strErr = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                strErr = ex.ToString
            End Try

            Return ds

            'sqlconnect.Dispose()

        End Using

    End Function

#End Region



#Region "Get APL History Records details"

    Public Function getAplHistMcuRec(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByRef strErr As String) As DataSet

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String = ""
        Dim sqlda As SqlDataAdapter = New SqlDataAdapter
        Dim ds As DataSet = New DataSet("APLHistory")

        Using con As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)

            Try
                strSQL = "Select cswmc_capsil_ModeCode, cswmc_ModeDesc from csw_mode_codes; "
                strSQL &= "Select BillingTypeCode, BillingTypeDesc from BillingTypeCodes"

                'sqlconnect.ConnectionString = con

                sqlda = New SqlDataAdapter(strSQL, con)
                sqlda.MissingSchemaAction = MissingSchemaAction.Add
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.TableMappings.Add("csw_mode_codes1", "BillingTypeCodes")
                sqlda.Fill(ds, "csw_mode_codes")

            Catch sqlex As SqlClient.SqlException
                strErr = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                strErr = ex.ToString
            End Try

            Return ds

            'sqlconnect.Dispose()

        End Using

    End Function

#End Region



#Region "pass SQL statment return result data table"

    Public Function GetDT(ByVal strSQL As String, ByVal strConn As String, ByRef dtResult As DataTable, ByRef strError As String) As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter

        GetDT = False

        sqlconnect.ConnectionString = strConn
        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.SelectCommand.CommandTimeout = 0

        dtResult = New DataTable

        Try
            sqlda.Fill(dtResult)
            GetDT = True
        Catch sqlex As SqlClient.SqlException
            strError = sqlex.ToString
        Catch ex As Exception
            strError = ex.ToString
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlda.Dispose()
        End Try

    End Function

#End Region


#Region "Macau Report List"
    Public Function GetReportList(ByVal strCompanyCode As String, ByVal strEnvCode As String) As WSResponse(Of List(Of MCUReportList))
        Dim response As New WSResponse(Of List(Of MCUReportList))
        Dim dt As New DataTable

        response.Data = New List(Of MCUReportList)

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Dim strSQL As String = "Select cswrel_rpt_no, cswrel_disp_name, cswrel_file_name, cswrel_file_type, cswrel_file_path from csw_report_list " & _
                                        " Where cswrel_grp_no > 1 " & _
                                        " order by cswrel_file_type, cswrel_rpt_no"

                Using da As New SqlDataAdapter(strSQL, cnn)
                    da.Fill(dt)
                End Using

                If dt Is Nothing Or dt.Rows.Count = 0 Then
                    response.Success = False

                    response.ErrorMsg = "There is no report list can be retrieved. "
                Else
                    response.Success = True
                    response.Data = ConvertDataTable(Of MCUReportList)(dt)
                End If
            End Using
        Catch sqlex As SqlClient.SqlException
            response.Success = False
            response.ErrorMsg = "GetReportList SQL Error - " & sqlex.ToString
        Catch ex As Exception
            response.Success = False
            response.ErrorMsg = "GetReportList - " & ex.ToString
        End Try

        Return response

    End Function

#End Region

#Region "ILAS Notification Letter"
    Public Function LoadFundPopup(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of List(Of RptILASFundDesc))

        Dim response As New WSResponse(Of List(Of RptILASFundDesc))
        Dim dt As New DataTable

        response.Data = New List(Of RptILASFundDesc)

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Dim strSQL As String = "select distinct  fa.cswcfa_fund_code as FundCode ,FundDesc.mpfinv_chi_desc as FundDescription,FundDesc.mpfinv_chi_name as FundChiDesc from csw_fund_allocation fa " & _
                                    "Inner Join (Select  cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no,Max(cswcfa_eff_date) as Eff_Date from csw_fund_allocation where cswcfa_policy_no = @PolicyNo " & _
                                    "Group   By cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no) as DistFund On  " & _
                                    "fa.cswcfa_policy_no = DistFund.cswcfa_policy_no And fa.cswcfa_fund_code = DistFund.cswcfa_fund_code " & _
                                    "and fa.cswcfa_coverage_no = DistFund.cswcfa_coverage_no and fa.cswcfa_eff_date = DistFund.Eff_Date " & _
                                    "Inner Join cswvw_mpf_investment as FundDesc  On  fa.cswcfa_fund_code = FundDesc.mpfinv_code " & _
                                    "where fa.cswcfa_policy_no = @PolicyNo  "

                Using da As New SqlDataAdapter(strSQL, cnn)
                    da.SelectCommand.Parameters.Add("@PolicyNo", SqlDbType.VarChar).Value = strPolicy
                    da.Fill(dt)
                End Using

                'response.Message = strSQL
                If dt Is Nothing Or dt.Rows.Count = 0 Then
                    response.Success = False

                    response.ErrorMsg = "There is no fund exist for given Policy No : " & strPolicy
                Else
                    response.Success = True
                    response.Data = ConvertDataTable(Of RptILASFundDesc)(dt)
                End If
            End Using

        Catch sqlex As SqlClient.SqlException
            response.Success = False
            response.ErrorMsg = "LoadFundPopup SQL Error - " & sqlex.ToString
        Catch ex As Exception
            response.Success = False
            response.ErrorMsg = "LoadFundPopup - " & ex.ToString
        End Try

        Return response

    End Function

    Public Function GetRptILASNotificationLetterInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByVal strCAMDatabase As String) As WSResponse(Of RptILASNotificationLetterInfo)

        Dim response As New WSResponse(Of RptILASNotificationLetterInfo)
        Dim dtGeneralInfo As New DataTable
        Dim strSQL As String = String.Empty

        response.Data = New RptILASNotificationLetterInfo

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()

                Dim strRptILASNotificationLetter As String = "Select Pa.PolicyAccountID, Product.ProductID, Product.[Description] As ProductDescription, Product_Chi.ChineseDescription,pa.PolicyCurrency, " & _
                        " Isnull(cswpad_add1,'') as Address1,Isnull(cswpad_add2,'') as Address2,Isnull(cswpad_add3,'') as Address3,Isnull(cswpad_city,'') As City," & _
                        " Isnull(poliAddress.cswpad_tel1,'') as TelePhone1,IsNull(poliAddress.cswpad_tel2,'') as TelePhone2," & _
                        " Isnull(customer.FirstName,'') as FirstName, Isnull(customer.NameSuffix,'') as LastName, Isnull(customer.ChiFstNm,'') as ChiFstNm, " & _
                        " Isnull(customer.ChiLstNm,'') as ChiLstNm,Isnull(customer.PhoneMobile,'') as PhoneMobile, " & _
                        " Isnull(rrt.Camrrt_loc_code,'') as LocationCode , Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') as AgentLastName, '' as AgentFirstName, " & _
                        " case when len(Isnull(Agent.camaib_chi_name,''))>0 then Isnull(Agent.camaib_chi_name,'') else Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') end as AgentChiName, pa.Mode,M.ModeDesc, SPACE(15) as CooloffDate,pa.ModalPremium, Cov.ModalPremium as OneOffModalPremium, " & _
                        " DATEDIFF(YEAR, cov.IssueDate, cov.PREMIUMCESSATIONDATE) as Period  from PolicyAccount as PA " & _
                        " Inner Join  csw_poli_rel as poliRel on PA.PolicyAccountID = poliRel.PolicyAccountID and poliRel.PolicyRelateCode ='PH' " & _
                        " Inner Join Coverage as Cov on pa.PolicyAccountID = cov.PolicyAccountID  and Cov.Trailer = '1' " & _
                        " Left Join csw_policy_address as poliAddress on poliRel.PolicyAccountID = poliAddress.cswpad_poli_id " & _
                        " Left Join customer on poliRel.CustomerID = customer.CustomerID   " & _
                        " Left Join csw_poli_rel as PoliRelWA on pa.PolicyAccountID = PoliRelWA.PolicyAccountID and PoliRelWA.PolicyRelateCode= 'WA' " & _
                        " Left Join customer as CustWA on PoliRelWA.CustomerID = custwa.CustomerID  " & _
                        " Left Join Product on PA.ProductID = product.ProductID  " & _
                        " Left Join Product_Chi on Pa.ProductID  =Product_Chi.ProductID  " & _
                        " Left Join ModeCodes M on pa.Mode = M.ModeCode " & _
                        " Left Join  {0}.dbo.cam_agent_info_basic as Agent  on CustWA.AgentCode = agent.camaib_agent_no " & _
                        " Inner Join {0}.dbo.cam_agent_info_dirmgr as  aid on Agent.camaib_agent_no = aid.camaid_agent_no " & _
                        " Left Join {0}.dbo.cam_rdbu_rel_tab as rrt on aid.camaid_sort_key = rrt.Camrrt_agency_code and aid.camaid_section_no = rrt.Camrrt_section_no " & _
                        " where PA.PolicyAccountID = @PolicyNo "
                strRptILASNotificationLetter = String.Format(strRptILASNotificationLetter, strCAMDatabase)
                Using daGeneralInfo As New SqlDataAdapter(strRptILASNotificationLetter, cnn)
                    daGeneralInfo.SelectCommand.Parameters.Add("@PolicyNo", SqlDbType.VarChar).Value = strPolicy
                    daGeneralInfo.Fill(dtGeneralInfo)
                End Using

                If dtGeneralInfo Is Nothing Or dtGeneralInfo.Rows.Count = 0 Then
                    response.Success = False
                    response.ErrorMsg = "There is no data exist for given Policy No : " & strPolicy
                Else
                    response.Success = True
                    response.Data.RptILASNotificationLetterList = ConvertDataTable(Of RptILASNotificationLetter)(dtGeneralInfo)

                    '1. Get iMaster Plan
                    Dim dtIMasterPlan As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_IMAS','CRS_IWEA','CRS_VINT')"
                    Using daIMasterPlan As New SqlDataAdapter(strSQL, cnn)
                        daIMasterPlan.Fill(dtIMasterPlan)
                    End Using
                    If dtIMasterPlan IsNot Nothing And dtIMasterPlan.Rows.Count > 0 Then
                        Me.TableToList(dtIMasterPlan, response.Data.iMasterPlan)
                    End If


                    '2. iKnowU Single Premium Plan
                    Dim dtIKnowUSinglePlan As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_IKUSP')"
                    Using daIKnowUSinglePlan As New SqlDataAdapter(strSQL, cnn)
                        daIKnowUSinglePlan.Fill(dtIKnowUSinglePlan)
                    End Using
                    If dtIKnowUSinglePlan IsNot Nothing And dtIKnowUSinglePlan.Rows.Count > 0 Then
                        Me.TableToList(dtIKnowUSinglePlan, response.Data.iKnowUSinglePremiumPlan)
                    End If

                    '3. iKnowU Reqular Premium Plan
                    Dim dtIKnowURegularPlan As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_IKU')"
                    Using daIKnowURegularPlan As New SqlDataAdapter(strSQL, cnn)
                        daIKnowURegularPlan.Fill(dtIKnowURegularPlan)
                    End Using
                    If dtIKnowURegularPlan IsNot Nothing And dtIKnowURegularPlan.Rows.Count > 0 Then
                        Me.TableToList(dtIKnowURegularPlan, response.Data.iKnowURegularPlan)
                    End If

                    '4. Horizon plan
                    Dim dtHorizonPlan As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_HORI')"
                    Using daHorizonPlan As New SqlDataAdapter(strSQL, cnn)
                        daHorizonPlan.Fill(dtHorizonPlan)
                    End Using
                    If dtHorizonPlan IsNot Nothing And dtHorizonPlan.Rows.Count > 0 Then
                        Me.TableToList(dtHorizonPlan, response.Data.HorizonPlan)
                    End If

                    '5. Get Horizon Upfront Charge Value
                    Dim dtHorizonUpfrontCharge As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_HUF')"
                    Using daHorizonUpfrontCharge As New SqlDataAdapter(strSQL, cnn)
                        daHorizonUpfrontCharge.Fill(dtHorizonUpfrontCharge)
                    End Using
                    If dtHorizonUpfrontCharge IsNot Nothing And dtHorizonUpfrontCharge.Rows.Count > 0 Then
                        response.Data.HorizonUpfrontCharge = Convert.ToString(dtHorizonUpfrontCharge.Rows(0).Item(0))
                    End If

                    '6. Get i.Know Single Premium Upfrom Charge Value 
                    Dim dtIKnowSPUpfrontCharge As New DataTable
                    strSQL = "select Value  from CodeTable where code in ('CRS_iKSPUF')"
                    Using daIKnowSPUpfrontCharge As New SqlDataAdapter(strSQL, cnn)
                        daIKnowSPUpfrontCharge.Fill(dtIKnowSPUpfrontCharge)
                    End Using
                    If dtIKnowSPUpfrontCharge IsNot Nothing And dtIKnowSPUpfrontCharge.Rows.Count > 0 Then
                        response.Data.iKnowSPUpfrontCharge = Convert.ToString(dtIKnowSPUpfrontCharge.Rows(0).Item(0))
                    End If

                    'Get Suitability Option
                    'Dim dtSuitability As New DataTable
                    'strSQL = "select a.ext_ref as policynumber, a.suitability, case when a.suitability is null or a.suitability = '91004' then 'C' else b.code_label end code_label, a.risk_profile, a.risk_marks from CNB_Staging..cnbods_indiv_agmt a left join CNB_Staging..CNBODS_code b on a.suitability = b.code_id " & _
                    '            " where a.ext_ref = @PolicyNo "
                    'Using daSuitability As New SqlDataAdapter(strSQL, cnn)
                    '    daSuitability.SelectCommand.Parameters.Add("@PolicyNo", SqlDbType.VarChar).Value = strPolicy
                    '    daSuitability.Fill(dtSuitability)
                    'End Using
                    'If dtSuitability IsNot Nothing And dtSuitability.Rows.Count > 0 Then
                    '    response.Data.SuitabilityOption = dtSuitability.Rows(0)("code_label")
                    'End If

                    Dim dtLogo As New DataTable
                    strSQL = "SELECT top 1 ING_Logo = a.cswilt_Logo, ING_CompanyAddr = b.cswilt_Logo , ING_Phone = c.cswilt_Logo, CareCompany = d.cswilt_Logo " & _
                            " FROM csw_ing_logo_table as a " & _
                            " Left Join csw_ing_logo_table b on RTRIM(UPPER(b.cswilt_LogoDesc)) = UPPER ('Footer ING Address') " & _
                            " Left Join csw_ing_logo_table c On RTRIM(UPPER(c.cswilt_LogoDesc)) = UPPER ('Footer ING Interactive Service') " & _
                            " Left Join csw_ing_logo_table d On RTRIM(UPPER(d.cswilt_LogoDesc)) = UPPER ('Footer Caring Company') " & _
                            " where RTRIM(UPPER(a.cswilt_LogoDesc)) = UPPER('ING Logo') "
                    Using daLogo As New SqlDataAdapter(strSQL, cnn)
                        daLogo.Fill(dtLogo)
                    End Using
                    If dtLogo IsNot Nothing And dtLogo.Rows.Count > 0 Then
                        response.Data.CompanyLogo = GetItem(Of RptCompanyLogo)(dtLogo.Rows(0))
                    End If
                End If
            End Using
        Catch sqlex As SqlClient.SqlException
            response.Success = False
            response.ErrorMsg = "GetRptILASNotificationLetterInfo SQL Error - " & sqlex.ToString
        Catch ex As Exception
            response.Success = False
            response.ErrorMsg = "GetRptILASNotificationLetterInfo - " & ex.ToString
        End Try


        Return response

    End Function

#End Region

#Region "Premium reminder call report"
    Public Function GetPremCallRpt(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal channelCheckFlag As Boolean, ByVal strChannel As String) As WSResponse(Of List(Of PremCallRpt))

        Dim response As New WSResponse(Of List(Of PremCallRpt))
        Dim dt As New DataTable
        Dim paramDict As New Dictionary(Of String, String)

        response.Data = New List(Of PremCallRpt)

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Dim strSQL As String = "Select p.policyaccountid, p.productid, description, p.paidtodate, p.mode, p.aplindicator, p.modalpremium, p.posamt, p.billingtype, cph.nameprefix, cph.namesuffix, cph.firstname, " & _
                                        " cph.phonemobile, cph.phonepager, cswpad_tel1, cswpad_tel2, cswpad_add1, cswpad_add2, cswpad_add3, cswpad_city, " & _
                                        " a.unitcode, a.agentcode, a.locationcode, csa.nameprefix nameprefix1, csa.namesuffix namesuffix1, csa.firstname firstname1, case when p.premiumstatus = 'PH' then 'Y' else 'N' end is_prem_holiday " & _
                                        " from policyaccount p, csw_poli_rel rph, customer cph, csw_poli_rel rsa, customer csa, agentcodes a, csw_policy_address pa, product pd " & _
                                        " where p.policyaccountid = rph.policyaccountid " & _
                                        " and p.productid = pd.productid " & _
                                        " and rph.policyrelatecode = 'PH' " & _
                                        " and rph.customerid = cph.customerid " & _
                                        " and p.policyaccountid = rsa.policyaccountid " & _
                                        " and rsa.policyrelatecode = 'SA' " & _
                                        " and rSA.customerid = csa.customerid " & _
                                        " and csa.agentcode = a.agentcode " & _
                                        " and p.policyaccountid = cswpad_poli_id " & _
                                        " and (p.accountstatuscode in ('1') or p.aplindicator = 'Y') " & _
                                        " and p.companyid in ('EAA','ING') " & _
                                        " and p.paidtodate between @DateFrom and @DateTo "

                ' For specific bank channel
                If channelCheckFlag Then
                    strSQL &= " and a.locationcode in ({0}) "
                    Dim channels As String() = strChannel.Replace("'", "").Split(New Char() {","c})
                    Dim channel As String
                    For Each channel In channels
                        Dim parameterName As String = "@p" & paramDict.Count
                        paramDict.Add(parameterName, channel.Trim())
                    Next
                    Dim keys As String() = New String(paramDict.Keys.Count - 1) {}
                    paramDict.Keys.CopyTo(keys, 0)
                    strSQL = String.Format(strSQL, String.Join(", ", keys))
                End If

                strSQL &= " order by p.paidtodate, a.unitcode, a.agentcode, p.policyaccountid "

                Using da As New SqlDataAdapter(strSQL, cnn)
                    da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.VarChar).Value = strDateFrom
                    da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.VarChar).Value = strDateTo
                    da.SelectCommand.Parameters.Add("@LoationCodeList", SqlDbType.VarChar).Value = strChannel
                    For Each pair As KeyValuePair(Of String, String) In paramDict
                        da.SelectCommand.Parameters.Add(pair.Key, SqlDbType.VarChar).Value = pair.Value
                    Next
                    da.Fill(dt)
                    response.Message = da.SelectCommand.CommandText
                End Using

                response.Success = True
                response.Data = ConvertDataTable(Of PremCallRpt)(dt)
            End Using

        Catch sqlex As SqlClient.SqlException
            response.Success = False
            response.ErrorMsg = "GetPremCallRpt SQL Error - " & sqlex.ToString
        Catch ex As Exception
            response.Success = False
            response.ErrorMsg = "GetPremCallRpt - " & ex.ToString
        End Try

        Return response

    End Function
#End Region

#Region "ILAS Product – Post Sale Call Report"
    Public Function GetHKFIReport(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal strType As String, ByVal strOrderType As String) As WSResponse(Of List(Of HKFIRpt))

        Dim response As New WSResponse(Of List(Of HKFIRpt))
        Dim dt As New DataTable

        response.Data = New List(Of HKFIRpt)

        Try
            Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
                cnn.Open()
                Dim strSQL As String = String.Empty

                If strType = "UL_" Then
                    strSQL = "Select DISTINCT 'UL' as RptType, cswpof_Policy as Policy_No, description as Plan_Name, RTRIM(cswpof_Value) as Risk_Level, exhibitinforcedate as Inforce_Date, " & _
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " & _
                    "   where cswpof_policy = f.cswpof_policy " & _
                    "   and cswpof_form_code = 'FNA' " & _
                    "   and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " & _
                    " c.customerid, "
                    strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " & _
                        " where r1.policyaccountid = p1.policyaccountid " & _
                        " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" & _
                        " and r1.policyrelatecode = 'PH' " & _
                        " and r1.customerid = c.customerid " & _
                        " and cv.exhibitinforcedate between dateadd(month, -1, @DateTo) and @DateTo " & _
                        " and p1.accountstatuscode not in ('6','7','8','9','0') " & _
                        " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                    strSQL &= "c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " & _
                        " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location " & _
                        " From csw_policy_form f, product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
                        " ON p.policyaccountid = cswpuw_poli_id " & _
                        " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " & _
                        " where cswpof_Form_Code = 'FNA' " & _
                        " and cswpof_Item_name = 'SUITABILITY' " & _
                        " and RTRIM(cswpof_Value) in ('A','B','C') " & _
                        " and cswpof_policy = p.policyaccountid " & _
                        " and p.productid = pd.productid " & _
                        " and exhibitinforcedate between @DateFrom and @DateTo " & _
                        " and cswpof_policy = r.policyaccountid " & _
                        " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
                        " and r.policyaccountid = r1.policyaccountid " & _
                        " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
                        " and c1.agentcode = a.agentcode " & _
                        " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " & _
                        " and accountstatuscode in ('1','2','3','4','V') " & _
                        " and p.companyid in ('ING','EAA') "
                Else
                    strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, RTRIM(ISNULL(cswpof_Value,'')) as Risk_Level, exhibitinforcedate as Inforce_Date, " & _
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " & _
                    " where cswpof_policy = p.policyaccountid " & _
                    " and cswpof_form_code = 'FNA' " & _
                    " and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " & _
                    " c.customerid, "
                    strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " & _
                        " where(r1.policyaccountid = p1.policyaccountid) " & _
                        " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" & _
                        " and r1.policyrelatecode = 'PH' " & _
                        " and r1.customerid = c.customerid " & _
                        " and cv.exhibitinforcedate between dateadd(month, -1, @DateTo) and @DateTo " & _
                        " and p1.accountstatuscode not in ('6','7','8','9','0') " & _
                        " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                    strSQL &= " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " & _
                        " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location " & _
                        " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
                        " ON p.policyaccountid = cswpuw_poli_id " & _
                        " LEFT JOIN csw_policy_form " & _
                        " ON p.policyaccountid = cswpof_policy " & _
                        " and cswpof_Form_Code = 'FNA' " & _
                        " and cswpof_Item_name = 'SUITABILITY' " & _
                        " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " & _
                        " Where(cv.productid = pd.productid) " & _
                        " and exhibitinforcedate between @DateFrom and @DateTo " & _
                        " and p.policyaccountid = r.policyaccountid " & _
                        " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
                        " and r.policyaccountid = r1.policyaccountid " & _
                        " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
                        " and c1.agentcode = a.agentcode " & _
                        " and cv.policyaccountid = p.policyaccountid " & _
                        " and (cv.trailer = 1 or (cv.productid like '_RFUR1' and cv.coveragestatus in ('1','2','3','4','V'))) " & _
                        " and accountstatuscode in ('1','2','3','4','V') " & _
                        " and p.companyid in ('ING','EAA') "
                End If

                If strOrderType = "CODay" Then
                    strSQL = strSQL & " order by cswpuw_flook_dline "
                Else
                    strSQL = strSQL & " order by Risk_Level "
                End If

                Using da As New SqlDataAdapter(strSQL, cnn)
                    da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.VarChar).Value = strDateFrom
                    da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.VarChar).Value = strDateTo

                    da.Fill(dt)
                    response.Message = da.SelectCommand.CommandText
                End Using

                response.Success = True
                response.Data = ConvertDataTable(Of HKFIRpt)(dt)
            End Using

        Catch sqlex As SqlClient.SqlException
            response.Success = False
            response.ErrorMsg = "GetHKFIReport SQL Error - " & sqlex.ToString
        Catch ex As Exception
            response.Success = False
            response.ErrorMsg = "GetHKFIReport - " & ex.ToString
        End Try

        Return response

    End Function
#End Region

#Region "Payment Letter"
    Public Function GetPIPHCustomerID(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of CustomerID)
        Dim response As New WSResponse(Of CustomerID)
        Dim customerID As New CustomerID
        Dim dsMisc As New DataSet
        response.Data = New CustomerID
        response.Success = True
        Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
            Try
                cnn.Open()
                Dim strSQL As String = ""
                ''''''''''''''''''''''
                strSQL = "select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('PI')"
                Dim sqlcmd As New SqlCommand(strSQL, cnn)
                Dim sqlrdr As SqlDataReader = sqlcmd.ExecuteReader()
                If sqlrdr.Read Then
                    response.Data.PI = sqlrdr("customerID")
                End If
                sqlrdr.Close()
                sqlcmd.Dispose()
                ''''''''''''''''''''''

                strSQL = "select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('PH')"
                sqlcmd = New SqlCommand(strSQL, cnn)
                sqlrdr = sqlcmd.ExecuteReader()
                If sqlrdr.Read Then
                    response.Data.PH = sqlrdr("customerID")
                End If
                sqlrdr.Close()

            Catch sqlex As SqlClient.SqlException
                response.Success = False
                response.ErrorMsg = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                response.Success = False
                response.ErrorMsg = ex.Message.ToString
            Finally
                cnn.Close()
            End Try
        End Using
        Return response
    End Function

    Public Function GetCustomerAndAgentInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of List(Of CustomerInfo))
        Dim response As New WSResponse(Of List(Of CustomerInfo))
        Dim customerInfo As New CustomerInfo
        Dim ds As New DataSet
        Dim dt As New DataTable
        response.Data = New List(Of CustomerInfo)
        response.Success = True
        Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
            Try
                cnn.Open()
                Dim strSQL As String = ""
                strSQL = "select convert(varchar(10), Customerid) as ClientID, NamePrefix, NameSuffix, FirstName,gender, usechiInd, chiLstNm + ChiFstNm as ChiName, " &
                "(select cswpad_add1 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as addressLine1," &
                "(select cswpad_add2 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as addressLine2," &
                "(select cswpad_add3 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as addressLine3," &
                "(select cswpad_city from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as addressCity," &
                "(select cswpad_add1 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as CaddressLine1," &
                "(select cswpad_add2 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as CaddressLine2," &
                "(select cswpad_add3 from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as CaddressLine3," &
                "(select cswpad_city from csw_policy_address where cswpad_poli_id = '" & strPolicy & "') as CaddressCity " &
                "from customer where customerID in (select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('PH', 'PI'))"

                strSQL = strSQL & " Union select '00000' + agentcode as ClientID, NamePrefix, NameSuffix, FirstName,gender, usechiInd, chiLstNm + ChiFstNm as ChiName, " &
                    "'' as addressLine1," &
                    "'' as addressLine2," &
                    "'' as addressLine3," &
                    "'' as addressCity," &
                    "'' as CaddressLine1," &
                    "'' as CaddressLine2," &
                    "'' as CaddressLine3," &
                    "'' as CaddressCity " &
                    "from customer where customerID in (select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('SA'))"


                '            sqlconnect.ConnectionString = strCIWConn
                Dim sqlda As SqlDataAdapter
                sqlda = New SqlDataAdapter(strSQL, cnn)
                sqlda.Fill(ds, "ORDUNA")
                dt.TableName = "ORDUNA"
                dt = ds.Tables("ORDUNA").Copy
                response.Data = ConvertDataTable(Of CustomerInfo)(dt)
                sqlda.Dispose()

            Catch sqlex As SqlClient.SqlException
                response.Success = False
                response.ErrorMsg = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                response.Success = False
                response.ErrorMsg = ex.Message.ToString
            Finally
                cnn.Close()
            End Try
        End Using
        Return response
    End Function

    Public Function GetPaymentInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String, ByVal strAG As String) As WSResponse(Of PaymentInfo)
        Dim response As New WSResponse(Of PaymentInfo)
        Dim policyinfo As New PaymentInfo
        Dim ds As New DataSet
        response.Data = New PaymentInfo
        response.Success = True
        Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
            Try
                cnn.Open()
                Dim strSQL As String = ""
                strSQL = "select * from PaymentTypeCodes "
                strSQL &= "select * from cswvw_cam_Agent_info where cswagi_agent_code = '" & strAG & "'; "
                strSQL &= "select * from DDARejectReasonCodes; "
                strSQL &= "select * from CCDRRejectReasonCodes; "
                strSQL &= "select * from csw_payh_remark_code; "
                strSQL &= "select PhoneNumber from agentcodes where AgentCode = '" & strAG & "';"

                strSQL &= "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
                "FROM (select * from csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
                ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
                ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
                ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "


                Dim sqlda As SqlDataAdapter
                sqlda = New SqlDataAdapter(strSQL, cnn)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.TableMappings.Add("PaymentTypeCodes1", "cswvw_cam_Agent_info")
                sqlda.TableMappings.Add("PaymentTypeCodes2", "DDARejectReasonCodes")
                sqlda.TableMappings.Add("PaymentTypeCodes3", "CCDRRejectReasonCodes")
                sqlda.TableMappings.Add("PaymentTypeCodes4", "csw_payh_remark_code")
                sqlda.TableMappings.Add("PaymentTypeCodes5", "agentcodes")
                sqlda.TableMappings.Add("PaymentTypeCodes6", "Logo")
                sqlda.Fill(ds, "PaymentTypeCodes")
                sqlda.Dispose()
                response.Data.PaymentTypeCodes = ConvertDataTable(Of PaymentTypeCodes)(ds.Tables("PaymentTypeCodes"))
                response.Data.cswvw_cam_Agent_info = ConvertDataTable(Of cswvw_cam_Agent_info)(ds.Tables("cswvw_cam_Agent_info"))
                response.Data.DDARejectReasonCodes = ConvertDataTable(Of DDARejectReasonCodes)(ds.Tables("DDARejectReasonCodes"))
                response.Data.CCDRRejectReasonCodes = ConvertDataTable(Of CCDRRejectReasonCodes)(ds.Tables("CCDRRejectReasonCodes"))
                response.Data.csw_payh_remark_code = ConvertDataTable(Of csw_payh_remark_code)(ds.Tables("csw_payh_remark_code"))
                response.Data.agentcodes = ConvertDataTable(Of agentcodes)(ds.Tables("agentcodes"))
                response.Data.Logo = ConvertDataTable(Of Logo)(ds.Tables("Logo"))
            Catch sqlex As SqlClient.SqlException
                response.Success = False
                response.ErrorMsg = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                response.Success = False
                response.ErrorMsg = ex.Message.ToString
            Finally
                cnn.Close()
            End Try
        End Using
        Return response
    End Function
#End Region

#Region "Policy Letter"
    Public Function GetPolicyInfo(ByVal strCompanyCode As String, ByVal strEnvCode As String, ByVal strPolicy As String) As WSResponse(Of PolicyInfo)
        Dim response As New WSResponse(Of PolicyInfo)
        Dim policyinfo As New PolicyInfo
        Dim dtClientInfo As DataTable = New DataTable
        Dim dtPolicyInfo As DataTable = New DataTable
        Dim dtCoRider As DataTable = New DataTable
        Dim dsMisc As New DataSet
        response.Success = True
        Using cnn As SqlConnection = CreateSqlConnection(strCompanyCode, strEnvCode)
            Try

                cnn.Open()
                Dim strSQL As String = ""
                strSQL = "Select * from DDA where policyaccountid = '" & strPolicy & "'; "
                strSQL &= "Select * from CCDR where policyaccountid = '" & strPolicy & "'; "
                strSQL &= "Select bt.* from PolicyAccount P left join BillingTypeCodes bt " & _
                    " ON P.billingtype = billingtypecode " & _
                    " Where policyaccountid = '" & strPolicy & "'; "

                strSQL &= "Select ac.* from PolicyAccount P left join AccountStatusCodes ac " & _
                    " ON p.accountstatuscode = ac.accountstatuscode " & _
                    " Where policyaccountid = '" & strPolicy & "'; "

                strSQL &= "Select m.* from PolicyAccount P left join ModeCodes m " & _
                    " ON p.mode = m.modecode " & _
                    " Where policyaccountid = '" & strPolicy & "'; "

                strSQL &= "Select ds.* from DDA d left join DDAStatusCodes ds " & _
                    " ON d.ddastatus = ds.ddastatuscode " & _
                    " Where policyaccountid = '" & strPolicy & "'; "

                strSQL &= "Select cs.* from CCDR c left join CCDRStatusCodes cs " & _
                    " ON c.ccdrstatus = cs.ccdrstatuscode " & _
                    " Where policyaccountid = '" & strPolicy & "'; "

                strSQL &= "Select * from PolicyAccount Where PolicyAccountid = '" & strPolicy & "'"

                '' Policy Value
                strSQL &= "Select * from csw_policy_value where cswval_TPOLID = 'xxx'"

                strSQL &= "Select PhoneNumber, NameSuffix + ' ' + FirstName as AgName from agentcodes a, csw_poli_rel r, customer c " & _
                    " Where r.policyaccountid = '" & strPolicy & "'" & _
                    " and r.customerid = c.customerid " & _
                    " and r.policyrelatecode = 'SA' " & _
                    " and c.agentcode = a.agentcode "

                strSQL &= "SELECT ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " & _
                    "FROM (select * from csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " & _
                    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " & _
                    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " & _
                    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

                strSQL &= "Select p.companyid, pt.productid, ProductType, ProductPolValueFunc, PrintValueReport" & _
                    " From product_type pt Inner Join policyaccount p ON p.productid = pt.productid" & _
                    " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

                strSQL &= "Select c.* from policyaccount P left join couponoptioncodes C " & _
                    " ON p.couponoption = c.couponoptioncode " & _
                    " Where policyaccountid = '" & strPolicy & "'"

                strSQL &= "Select c.* from policyaccount P left join dividendoptioncodes C " & _
                    " ON p.dividendoption = c.dividendoptioncode " & _
                    " Where policyaccountid = '" & strPolicy & "'"

                strSQL &= "Select pt.productid, pt.ChineseDescription" & _
                    " From product_chi pt Inner Join policyaccount p ON p.productid = pt.productid" & _
                    " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"
                Using da As New SqlDataAdapter(strSQL, cnn)
                    da.TableMappings.Add("DDA1", "CCDR")
                    da.TableMappings.Add("DDA2", "BillingTypeCodes")
                    da.TableMappings.Add("DDA3", "AccountStatusCodes")
                    da.TableMappings.Add("DDA4", "ModeCodes")
                    da.TableMappings.Add("DDA5", "DDAStatusCodes")
                    da.TableMappings.Add("DDA6", "CCDRStatusCodes")
                    da.TableMappings.Add("DDA7", "PolicyAccount")
                    da.TableMappings.Add("DDA8", "csw_policy_value")
                    da.TableMappings.Add("DDA9", "agentcodes")
                    da.TableMappings.Add("DDA10", "csw_ing_logo_table")
                    da.TableMappings.Add("DDA11", "product_type")
                    da.TableMappings.Add("DDA12", "couponoptioncodes")
                    da.TableMappings.Add("DDA13", "dividendoptioncodes")
                    da.TableMappings.Add("DDA14", "Product_Chi")
                    da.Fill(dsMisc, "DDA")
                End Using
                response.Data = policyinfo

                response.Data.DDA = ConvertDataTable(Of DDA)(dsMisc.Tables("DDA"))

                response.Data.CCDR = ConvertDataTable(Of CCDR)(dsMisc.Tables("CCDR"))

                response.Data.BillingTypeCodes = ConvertDataTable(Of BillingTypeCodes)(dsMisc.Tables("BillingTypeCodes"))

                response.Data.AccountStatusCodes = ConvertDataTable(Of AccountStatusCodes)(dsMisc.Tables("AccountStatusCodes"))

                response.Data.ModeCodes = ConvertDataTable(Of ModeCodes)(dsMisc.Tables("ModeCodes"))

                response.Data.DDAStatusCodes = ConvertDataTable(Of DDAStatusCodes)(dsMisc.Tables("DDAStatusCodes"))

                response.Data.CCDRStatusCodes = ConvertDataTable(Of CCDRStatusCodes)(dsMisc.Tables("CCDRStatusCodes"))

                response.Data.PolicyAccount = ConvertDataTable(Of PolicyAccount)(dsMisc.Tables("PolicyAccount"))

                response.Data.csw_policy_value = ConvertDataTable(Of csw_policy_value)(dsMisc.Tables("csw_policy_value"))

                response.Data.agentcodes = ConvertDataTable(Of agentcodes)(dsMisc.Tables("agentcodes"))

                response.Data.csw_ing_logo_table = ConvertDataTable(Of csw_ing_logo_table)(dsMisc.Tables("csw_ing_logo_table"))

                response.Data.product_type = ConvertDataTable(Of product_type)(dsMisc.Tables("product_type"))

                response.Data.couponoptioncodes = ConvertDataTable(Of couponoptioncodes)(dsMisc.Tables("couponoptioncodes"))

                response.Data.dividendoptioncodes = ConvertDataTable(Of dividendoptioncodes)(dsMisc.Tables("dividendoptioncodes"))

                response.Data.Product_Chi = ConvertDataTable(Of Product_Chi)(dsMisc.Tables("Product_Chi"))

                Dim command As SqlCommand = New SqlCommand("cswsp_clientinfo", cnn)
                command.Parameters.AddWithValue("@PolicyAccountID", strPolicy)
                command.CommandType = CommandType.StoredProcedure
                Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
                adapter.Fill(dtClientInfo)

                command = New SqlCommand("cswsp_PolicyInfo", cnn)
                command.Parameters.AddWithValue("@PolicyAccountID", strPolicy)
                command.CommandType = CommandType.StoredProcedure

                adapter = New SqlDataAdapter(command)
                adapter.Fill(dtPolicyInfo)

                command = New SqlCommand("cswsp_corider2", cnn)
                command.Parameters.AddWithValue("@PolicyAccountID", strPolicy)
                command.CommandType = CommandType.StoredProcedure

                adapter = New SqlDataAdapter(command)
                adapter.Fill(dtCoRider)

                response.Data.cswsp_clientinfo = ConvertDataTable(Of cswsp_clientinfo)(dtClientInfo)

                response.Data.cswsp_PolicyInfo = ConvertDataTable(Of cswsp_PolicyInfo)(dtPolicyInfo)

                response.Data.cswsp_corider2 = ConvertDataTable(Of cswsp_corider2)(dtCoRider)


            Catch sqlex As SqlClient.SqlException
                response.Success = False
                response.ErrorMsg = sqlex.Number & sqlex.ToString
            Catch ex As Exception
                response.Success = False
                response.ErrorMsg = ex.Message.ToString
            Finally
                cnn.Close()
            End Try
        End Using


        Return response
    End Function
#End Region
#Region "Claims"
    Public Function ClaimsAudit(ByVal CompanyID As String, ByVal Env As String, ByVal ClaimNo As String, ByVal PolicyNo As String, ByVal InsuredName As String, ByVal OccurNo As String, ByVal EventName As String) As WSResponse(Of Object)
        Dim result As New WSResponse(Of Object)
        Try
            Dim sqlMcsConnect As SqlConnection
            Dim connStr = ConfigurationManager.ConnectionStrings("HKMCSConnectionString").ConnectionString
            If (CompanyID.Contains("MC")) Then
                connStr = ConfigurationManager.ConnectionStrings("MCMCSConnectionString").ConnectionString
            End If
            sqlMcsConnect = New SqlConnection(connStr)
            Dim cmd As New SqlCommand
            cmd.CommandText = "exec SaveAuditTrail @Source,@PolicyNo,@ClaimNo,@InsuredName,@PolicyholderName,@OccurNo,@EventName,@UserID,@Remarks"
            cmd.Parameters.Add("@Source", SqlDbType.NVarChar, 100)
            cmd.Parameters.Add("@PolicyNo", SqlDbType.VarChar, 16)
            cmd.Parameters.Add("@ClaimNo", SqlDbType.NVarChar, 20)
            cmd.Parameters.Add("@InsuredName", SqlDbType.VarChar, 70)
            cmd.Parameters.Add("@PolicyholderName", SqlDbType.NVarChar, 100)
            cmd.Parameters.Add("@OccurNo", SqlDbType.NVarChar, 20)
            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50)
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 10)
            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 65535)
            cmd.Parameters("@Source").Value = "CRS"
            cmd.Parameters("@PolicyNo").Value = PolicyNo
            cmd.Parameters("@ClaimNo").Value = ClaimNo
            cmd.Parameters("@InsuredName").Value = InsuredName
            cmd.Parameters("@PolicyholderName").Value = ""
            cmd.Parameters("@OccurNo").Value = OccurNo
            cmd.Parameters("@EventName").Value = EventName 'Parameter
            cmd.Parameters("@UserID").Value = Environment.UserName.ToUpper()
            cmd.Parameters("@Remarks").Value = ""
            cmd.Connection = sqlMcsConnect
            sqlMcsConnect.Open()
            cmd.ExecuteNonQuery()
            sqlMcsConnect.Close()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Message = ex.Message
            'MsgBox("Error occurs when insert claim audit record. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
        Return result
    End Function
#End Region

    Private Function CreateSqlConnection(ByVal strCompanyCode As String, ByVal strEnvCode As String) As SqlConnection
        Dim strConnectionConfig As String = strCompanyCode & "_" & strEnvCode
        Dim sqlconnect As New SqlConnection(ConfigurationManager.ConnectionStrings(strConnectionConfig).ConnectionString)

        Return sqlconnect
    End Function



    'Private Function getSettingConnectionStr(ByVal strCompany As String, ByVal strEnvCode As String) As SqlConnection

    '    Dim common As New Common
    '    Dim sqlConn As SqlConnection = New SqlConnection()
    '    Dim connStr As String = My.Settings.HK_NCBU105.ToString() '.(strCompany & "_" & strEnvCode) '.Properties(strCompany & "_" & strEnvCode).Attributes.Values.ToString() '.PropertyValues(strCompany & "_" & strEnvCode).ToString()  'Properties.Settings.Default[ComID + "_" + DB].ToString();

    '    If (connStr <> "") Then

    '        Dim tmpStr = common.Decrypt(connStr)

    '        sqlConn = New SqlConnection(tmpStr)

    '        Return sqlConn

    '    End If

    '    Return sqlConn

    'End Function

    Private Sub EscapeSqlSingleQuote(ByVal dt As DataTable)
        For Each dr As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                If col.DataType Is GetType(String) AndAlso dr(col.ColumnName) IsNot DBNull.Value Then
                    dr(col.ColumnName) = dr(col.ColumnName).ToString().Replace("'", "''")
                End If
            Next
        Next
    End Sub

    ' Custom Function
    Private Function TruncateRound(ByVal d As Decimal, ByVal decimals As Byte) As Decimal

        Dim r As Decimal
        r = Math.Round(d, decimals)

        If (d > 0 And r > d) Then

            Return r - New Decimal(1, 0, 0, False, decimals)

        ElseIf (d < 0 And r < d) Then

            Return r + New Decimal(1, 0, 0, False, decimals)

        End If

        Return r

    End Function


    Private Function ConvertDataTable(Of T)(ByVal dt As DataTable) As IList(Of T)
        Dim data As List(Of T) = New List(Of T)()

        For Each row As DataRow In dt.Rows
            Dim item As T = GetItem(Of T)(row)
            data.Add(item)
        Next row

        Return data
    End Function

    Private Function GetItem(Of T)(ByVal dr As DataRow) As T
        Dim temp As Type = GetType(T)
        Dim obj As T = Activator.CreateInstance(Of T)()

        For Each column As DataColumn In dr.Table.Columns

            For Each pro As PropertyInfo In temp.GetProperties()

                If pro.Name.ToLower() = column.ColumnName.ToLower() AndAlso Not IsDBNull(dr(column.ColumnName)) Then
                    Dim val = If((pro.PropertyType.Name = "String"), Convert.ToString(dr(column.ColumnName)), dr(column.ColumnName))
                    pro.SetValue(obj, val, Nothing)
                    Exit For
                End If
            Next
        Next

        Return obj
    End Function

    'Private Function GetItem(Of T)(ByVal dr As DataRow) As T
    '    Dim temp As Type = GetType(T)
    '    Dim obj As T = Activator.CreateInstance(Of T)()

    '    For Each column As DataColumn In dr.Table.Columns

    '        For Each field As FieldInfo In temp.GetFields()

    '            If field.Name.ToLower() = column.ColumnName.ToLower() AndAlso Not IsDBNull(dr(column.ColumnName)) Then
    '                Dim val = If((field.FieldType.Name = "String"), Convert.ToString(dr(column.ColumnName)), dr(column.ColumnName))
    '                field.SetValue(obj, val)
    '                Exit For
    '            End If
    '        Next
    '    Next

    '    Return obj
    'End Function

    Private Sub TableToList(ByVal dtTable As DataTable, ByRef destList As List(Of String))

        For Each tmpRow As DataRow In dtTable.Rows
            destList.Add(tmpRow(0).ToString())
        Next

    End Sub

    Public Sub New()

    End Sub
End Class
