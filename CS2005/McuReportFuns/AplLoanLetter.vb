Imports System.Data.SqlClient
Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu

#Region "APL Loan Letter"

    'this function is cloned from payment report
    ''' <summary>
    ''' Report: APL/Loan History Letter
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub AplLoanLetter()
        'ds is the dataset finally will be passed into Crystal Report
        'ds1 is the dataset that read from .xsd schema, an intermediate dataset
        Dim ds As New DataSet("dsLoanHistory")
        Dim ds1 As New DataSet
        Dim dr As DataRow
        ds1.ReadXmlSchema(My.Application.Info.DirectoryPath & "\aplloanletter.xsd") '"C:\tfs\Life_Common\IPD\LIB"

        Dim strContractCurrency As String

        Dim strPolicy, strErrMsg As String
        Dim lngErrNo As Long
        Dim intCnt As Integer
        Dim datFrom, datStart, datEnd, datLast As Date
        Dim dtLoan, dtLoan_All, dtPoList, dtPolMisc As DataTable
        Dim blnCont As Boolean = True

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "APL/Loan History Letter"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Text = DateSerial(Year(Today), 1, 1)
        frmInput.txtTo.Text = Today
        frmInput.RadioButton1.Enabled = False
        frmInput.RadioButton2.Enabled = False
        frmInput.chkChi.Checked = False
        frmInput.chkEng.Checked = True
        frmInput.chkChi.Enabled = True
        frmInput.chkEng.Enabled = True
        frmInput.ShowDialog()
        blnCancel = True

        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        strPolicy = frmInput.PolicyNo
        datFrom = frmInput.ToDate
        datStart = datFrom
        datEnd = frmInput.FromDate

        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If
        CRS_Component.WaitWndFun.Execute(ParentFrm, "Try to read Policy infomation from 400.",
                           Sub()
                               Try
                                   PorcssReport(ds, ds1, dr, strContractCurrency, strPolicy, strErrMsg, lngErrNo, intCnt, datFrom, datEnd,
                                               datLast, dtLoan, dtLoan_All, dtPoList, blnCont, frmInput)
                               Catch ex As Exception
                                   'MsgBox(ex.ToString + "\" + ex.StackTrace, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Reports")
                                   Throw
                               End Try
                           End Sub, True)
    End Sub

    Private Sub PorcssReport(ds As DataSet, ds1 As DataSet, ByRef dr As DataRow, ByRef strContractCurrency As String, strPolicy As String, strErrMsg As String, ByRef lngErrNo As Long, ByRef intCnt As Integer, datFrom As Date, datEnd As Date, ByRef datLast As Date, ByRef dtLoan As DataTable, ByRef dtLoan_All As DataTable, ByRef dtPoList As DataTable, ByRef blnCont As Boolean, frmInput As frmPAYHRpt)
        wndMain.Cursor = Cursors.WaitCursor

        Dim drs As DataRowView
        Dim strPI, strPH, strAG As String

        'Get policy information

        Dim dsPolicySend As New DataSet
        Dim dsPolicyCurr As New DataSet
        Dim strTime As String = ""
        Dim strerr As String = ""
        Dim blnGetPolicy As Boolean
        Dim dtSendData As New DataTable

        dtSendData.Columns.Add("PolicyNo")
        dr = dtSendData.NewRow
        dr("PolicyNo") = RTrim(strPolicy)
        Me.PolicyNo = strPolicy
        dtSendData.Rows.Add(dr)

        dsPolicySend.Tables.Add(dtSendData)

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim objDBHeaderMcu As Utility.Utility.ComHeader

        objMQQueHeaderMcu = gobjMQQueHeader
        objMQQueHeaderMcu.CompanyID = g_McuComp
        objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
        objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
        objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
        objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ
        objDBHeaderMcu = gobjDBHeader
        objDBHeaderMcu.CompanyID = g_McuComp
        objDBHeaderMcu.EnvironmentUse = g_McuEnv
        If My.Settings.LAReady = True Then
            clsPOS.MQQueuesHeader = objMQQueHeaderMcu
            clsPOS.DBHeader = objDBHeaderMcu

            dsPolicyCurr.Tables.Clear()

            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    dr = ds1.Tables("POLINF").NewRow()
                    For i As Integer = 0 To ds1.Tables("POLINF").Columns.Count - 1
                        Select Case ds1.Tables("POLINF").Columns(i).ToString
                            Case "PolicyAccountID"
                                dr(i) = RTrim(strPolicy)
                            Case "PaidToDate"
                                dr(i) = dsPolicyCurr.Tables(0).Rows(0)("Paid_To_Date")
                            Case "POAGCY"
                                dr(i) = dsPolicyCurr.Tables(0).Rows(0)("S_Agent_No")
                            Case Else
                                Select Case ds1.Tables("POLINF").Columns(i).DataType.ToString
                                    Case "System.String"
                                        dr(i) = ""
                                    Case "System.Decimal", "System.Int16"
                                        dr(i) = 0
                                    Case "System.DateTime"
                                    Case Else
                                        dr(i) = ""
                                End Select
                        End Select
                    Next
                    ds1.Tables("POLINF").Rows.Add(dr)
                    dtPoList = ds1.Tables("POLINF").Copy
                    ds.Tables.Add(dtPoList)
                    strContractCurrency = dsPolicyCurr.Tables(0).Rows(0)("Curr")
                End If
            End If
        End If

        If dtPoList.Rows.Count > 0 Then
            strAG = Right(dtPoList.Rows(0).Item("POAGCY").ToString.Trim, 5)
            For i As Integer = 0 To dtPoList.Rows.Count - 1
                dtPoList.Rows(i).Item("POAGCY") = strAG
            Next
        End If

        'Prepare input for loan and APL history
        dtSendData = New DataTable
        dsPolicySend.Tables.RemoveAt(0)
        dsPolicyCurr.Tables.RemoveAt(0)
        dtSendData.Columns.Add("Policy_No")
        dtSendData.Columns.Add("FromDate")
        dtSendData.Columns.Add("ToDate")
        dr = dtSendData.NewRow
        dtSendData.Rows.Add(dr)
        dtSendData.Rows(0)("Policy_No") = RTrim(strPolicy)
        dtSendData.Rows(0)("FromDate") = datFrom
        dtSendData.Rows(0)("ToDate") = datEnd
        dsPolicySend.Tables.Add(dtSendData)
        CRS_Component.WaitWndFun.ShowMessage("Call LifeAsia BO APLTRN to get loan and APL history...")
        'Call LifeAsia BO APLTRN to get loan and APL history
        blnGetPolicy = clsPOS.GetLoanAndAPLHistory(RTrim(strPolicy), datEnd, datFrom, dsPolicyCurr, strerr)

        If dsPolicyCurr.Tables.Count > 0 Then
            If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                'Remove the language column first, and then add it back later with Language indicator. Copied from payment history letter.
                ds1.Tables("LOAN").Columns.Remove("Lang")
                For j As Integer = 0 To dsPolicyCurr.Tables(1).Rows.Count - 1
                    dr = ds1.Tables("LOAN").NewRow()
                    For i As Integer = 0 To ds1.Tables("LOAN").Columns.Count - 1
                        Select Case ds1.Tables("LOAN").Columns(i).ToString
                            Case "PolicyAccountID"
                                dr(i) = RTrim(strPolicy)
                            Case "TransDate"
                                dr(i) = dsPolicyCurr.Tables(1).Rows(j)("TransDate")
                            Case "Currency"
                                dr(i) = strContractCurrency
                            Case "LoanType"
                                dr(i) = dsPolicyCurr.Tables(1).Rows(j)("LoanType")
                            Case "LoanAmount"
                                If dsPolicyCurr.Tables(1).Rows(j)("LoanSubType") = "CAP" Then
                                    dr(i) = dsPolicyCurr.Tables(1).Rows(j)("Interest")
                                Else
                                    dr(i) = dsPolicyCurr.Tables(1).Rows(j)("Amount")
                                End If
                            Case "Activity"
                                dr(i) = RTrim(dsPolicyCurr.Tables(1).Rows(j)("LoanSubType"))
                            Case Else
                                Select Case ds1.Tables("LOAN").Columns(i).DataType.ToString
                                    Case "System.String"
                                        dr(i) = ""
                                    Case "System.Decimal", "System.Int16"
                                        dr(i) = 0
                                    Case "System.DateTime"
                                    Case Else
                                        dr(i) = ""
                                End Select
                        End Select
                    Next
                    ds1.Tables("LOAN").Rows.Add(dr)
                Next
                dtLoan = ds1.Tables("LOAN").Copy
                lngErrNo = 0

                Dim blnEng, blnChi As Boolean
                blnEng = frmInput.chkEng.Checked
                blnChi = frmInput.chkChi.Checked
                If lngErrNo = 0 Then
                    'dtLoan contains the loan record returned from LifeAsia BO
                    'dtLoan_All contains the record with language indicator
                    'so there will be 2 sets of dtLoan in dtLoan_ALL if both Chi and Eng are checked
                    If dtLoan_All Is Nothing Then
                        dtLoan_All = dtLoan.Clone
                        dtLoan_All.Columns.Add("Lang", System.Type.GetType("System.String"))
                    End If

                    Dim intColCnt As Integer = dtLoan.Columns.Count
                    Dim ar() As Object
                    Dim i As Integer
                    intCnt = dtLoan.Rows.Count - 1
                    If dtLoan.Rows.Count > 0 Then
                        datLast = #1/1/1900#
                        blnCont = False
                    End If
                    For i = 0 To intCnt
                        If dtLoan.Rows(i).Item("TransDate") <> datLast And dtLoan.Rows(i).Item("TransDate") >= datEnd Then
                            ar = dtLoan.Rows(i).ItemArray
                            ReDim Preserve ar(intColCnt)
                            'Append row with LOAN.Lang = E for English
                            If blnEng Then
                                ar(intColCnt) = "E"
                                dtLoan_All.Rows.Add(ar)
                            End If
                            'Append row with LOAN.Lang = C for Chinese
                            If blnChi Then
                                ar(intColCnt) = "C"
                                dtLoan_All.Rows.Add(ar)
                            End If
                        End If
                    Next
                Else
                    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''LA rec modify
        If dtLoan_All.Rows.Count = 0 Then
            MsgBox("No APL / Loan History record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            ds.Tables.Add(dtLoan_All)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        CRS_Component.WaitWndFun.ShowMessage("Get Agent information from CRSAPI Server...")

        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "Report_Agent_info", New Dictionary(Of String, String)() From {
        {"AgentCode", strAG}
        })
        'strSQL = "select * from cswvw_cam_Agent_info where cswagi_agent_code = @strAG; "

        ds.Tables.Add(retDs.Tables(0).Copy())

        retDs = APIServiceBL.CallAPIBusi(g_McuComp, "LOG_POLICY", New Dictionary(Of String, String)() From {
        {"PolicyNo", strPolicy}
        })
        ds.Tables.Add(retDs.Tables(0).Copy())


        'Get total loan amount by calling policy value enquiry BO
        'Get BO schema
        Dim bo As New BOSchema.PolicyValue()
        Dim dt As DataTable = bo.GetPolicyValueEnqSendSchema()
        'Change the schema to table format
        dt = bo.SchemaToDataTable(dt)
        Dim row As DataRow = dt.NewRow()
        row("QuoteDate") = Now.Date
        row("PolicyNo") = RTrim(strPolicy)
        row("PrintFlag1") = "N"
        dt.Rows.Add(row)
        'send the input string as a dataset dsSend
        Dim dsSend As New DataSet
        Dim dsPolicyValue As New DataSet
        dsSend.Tables.Add(dt)
        If Not clsPOS.PolicyValueEnq(dsSend, dsPolicyValue, strerr) Then
            Throw New Exception(strerr)
            Exit Sub
        End If

        'Retrieve value from BO
        Dim aplAmount As Decimal
        Dim aplIntAmount As Decimal
        Dim loanAmount As Decimal
        Dim loanIntAmount As Decimal
        Dim totalLoanAmount As Decimal
        aplAmount = dsPolicyValue.Tables(0).Rows(0)("APL")
        aplIntAmount = dsPolicyValue.Tables(0).Rows(0)("APLInt")
        loanAmount = dsPolicyValue.Tables(0).Rows(0)("Loan")
        loanIntAmount = dsPolicyValue.Tables(0).Rows(0)("LoanInt")
        totalLoanAmount = aplAmount + aplIntAmount + loanAmount + loanIntAmount

        'Get address
        Dim dtPolicyDetail As DataTable
        Dim dtSQLResult As DataTable
        Dim strInsName_Rpt, strInsName_Disp As String
        Dim strAgtName As String
        Dim strAgPhone As String
        CRS_Component.WaitWndFun.ShowMessage("Get Customer information from CRSAPI Server...")
        retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_customerDetail", New Dictionary(Of String, String)() From {
       {"PolicyNo", strPolicy}
       })
        dtSQLResult = retDs.Tables(0)
        'read from result.
        strInsName_Disp = Trim(dtSQLResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("pi_FirstName"))
        strAgtName = Trim(dtSQLResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("sa_FirstName"))
        strAgPhone = Trim(dtSQLResult.Rows(0).Item("PhoneNumber"))
        strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)

        If frmInput.chkEng.Checked = True Then
            dr = ds1.Tables("PolicyDetail").NewRow()
            dr.Item("PolicyAccountID") = strPolicy
            dr.Item("Lang") = "E"
            dr.Item("PH_Name") = Trim(dtSQLResult.Rows(0).Item("ph_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("ph_FirstName"))
            dr.Item("PH_NamePrefix") = Trim(dtSQLResult.Rows(0).Item("ph_NamePrefix"))
            dr.Item("PH_NameSuffix") = Trim(dtSQLResult.Rows(0).Item("ph_NameSuffix"))
            dr.Item("PI_Name") = Trim(dtSQLResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("pi_FirstName"))
            dr.Item("AG_Name") = Trim(dtSQLResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("sa_FirstName"))
            dr.Item("AG_Phone") = dtSQLResult.Rows(0).Item("PhoneNumber")
            dr.Item("AG_Loc") = dtSQLResult.Rows(0).Item("LocationCode")
            dr.Item("Curr") = dtSQLResult.Rows(0).Item("policycurrency")
            dr.Item("PaidToDate") = dtSQLResult.Rows(0).Item("PaidToDate")
            ds1.Tables("PolicyDetail").Rows.Add(dr)
        End If

        If frmInput.chkChi.Checked = True Then
            dr = ds1.Tables("PolicyDetail").NewRow()
            dr.Item("PolicyAccountID") = strPolicy
            dr.Item("Lang") = "C"
            dr.Item("PH_Name") = Trim(dtSQLResult.Rows(0).Item("ph_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("ph_FirstName"))
            dr.Item("PH_NamePrefix") = Trim(dtSQLResult.Rows(0).Item("ph_NamePrefix"))
            dr.Item("PH_NameSuffix") = Trim(dtSQLResult.Rows(0).Item("ph_NameSuffix"))
            dr.Item("PI_Name") = Trim(dtSQLResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("pi_FirstName"))
            dr.Item("AG_Name") = Trim(dtSQLResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("sa_FirstName"))
            dr.Item("AG_Phone") = dtSQLResult.Rows(0).Item("PhoneNumber")
            dr.Item("AG_Loc") = dtSQLResult.Rows(0).Item("LocationCode")
            dr.Item("Curr") = dtSQLResult.Rows(0).Item("policycurrency")
            dr.Item("PaidToDate") = dtSQLResult.Rows(0).Item("PaidToDate")
            ds1.Tables("PolicyDetail").Rows.Add(dr)
        End If

        dtPolicyDetail = ds1.Tables("PolicyDetail").Copy
        ds.Tables.Add(dtPolicyDetail)
        CRS_Component.WaitWndFun.ShowMessage("Get Policy information from CRSAPI Server...")
        retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_csw_policy_address", New Dictionary(Of String, String)() From {
       {"PolicyNo", strPolicy}
       })
        ds.Tables.Add(retDs.Tables(0).Copy())


        wndMain.Cursor = Cursors.Default

        Try
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("strCSR", gsCSRName)
            rpt.SetParameterValue("strChiCSR", gsCSRChiName)
            rpt.SetParameterValue("strSAName", strAgtName)
            rpt.SetParameterValue("strInsured", strInsName_Disp)
            rpt.SetParameterValue("strStart", CDate(frmInput.FromDate))
            rpt.SetParameterValue("strEnd", CDate(frmInput.ToDate))
            rpt.SetParameterValue("strPhone", strAgPhone)
            rpt.SetParameterValue("strTotalLoanAmount", totalLoanAmount)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        blnCancel = False
    End Sub


#End Region
End Class
