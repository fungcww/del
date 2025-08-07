Imports System.Data.SqlClient
Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu
    ''' <summary>
    ''' Report: FCR report
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub RPU_Report()

        Dim ds As New DataSet("RPUQuote")

        Dim strSQL As String

        Dim strPolicy, strErrMsg As String
        Dim lngErrNo As Long
        Dim intCnt As Integer
        Dim datFrom, datStart, datEnd, datLast As Date
        Dim dtPAYH, dtPAYH_All, dtPoList, dtORDUNA, dtPolMisc As DataTable
        Dim blnCont As Boolean = True

        Dim drs As DataRowView
        Dim strPI, strPH, strAG, strInsName_Rpt, strInsName_Disp As String
        Dim dblAmount, TotalCSV As Double
        Dim strAgtName As String
        Dim strAgPhone As String
        Dim dtRPUDetail As New DataTable("RPUDtl")
        Dim dr As DataRow

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "RPU Quotation"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Enabled = False
        frmInput.txtTo.Enabled = False
        frmInput.RadioButton1.Enabled = True
        frmInput.RadioButton2.Enabled = True
        frmInput.ShowDialog()

        blnCancel = True
        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        strPolicy = frmInput.PolicyNo

        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor
        'CRS_Component.WaitWndFun.Execute(ParentFrm, "Loading data from backend..",
                          'Sub()
                              Call QueryRpuData(ds, strSQL, strPolicy, strErrMsg, lngErrNo, dtPoList, dtORDUNA,
                                          dtPolMisc, drs, strPI, strPH, strAG, strInsName_Rpt, strInsName_Disp, dblAmount, TotalCSV,
                                          strAgtName, strAgPhone, dtRPUDetail, dr, frmInput)
                          'End Sub
            ', True)


    End Sub

    Private Sub QueryRpuData(ByRef ds As DataSet, ByRef strSQL As String, strPolicy As String, ByRef strErrMsg As String, ByRef lngErrNo As Long, ByRef dtPoList As DataTable, ByRef dtORDUNA As DataTable, ByRef dtPolMisc As DataTable, ByRef drs As DataRowView, ByRef strPI As String, ByRef strPH As String, ByRef strAG As String, ByRef strInsName_Rpt As String, ByRef strInsName_Disp As String, ByRef dblAmount As Double, ByRef TotalCSV As Double, ByRef strAgtName As String, ByRef strAgPhone As String, dtRPUDetail As DataTable, ByRef dr As DataRow, frmInput As frmPAYHRpt)
        ' RPU Quote and Projection for LifeAsia
        Dim dtResult As DataTable
        Dim isRightCompany As Boolean = False
        Dim Company = g_McuComp
        CRS_Component.WaitWndFun.ShowMessage("Verify the Policy in the CRSAPI Server...")
        If System.Configuration.ConfigurationManager.AppSettings("QueryRpuData").Equals("") = False Then Company = System.Configuration.ConfigurationManager.AppSettings("QueryRpuData")
        
        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(Company, "Report_customerDetail", New Dictionary(Of String, String)() From {
           {"PolicyNo", strPolicy}
           })
        If retDs.Tables.Count = 0 OrElse retDs.Tables(0).Rows.Count = 0 Then
            isRightCompany = False
        Else
            isRightCompany = TRUE
        End If

        If isRightCompany Then
            ' Get from CIW

            CRS_Component.WaitWndFun.ShowMessage("Call QuoteRpu from 400 Server...")
            retDs = APIServiceBL.CallAPIBusi(Company, "Report_customerDetail", New Dictionary(Of String, String)() From {
               {"PolicyNo", strPolicy}
               })
            dtResult = retDs.Tables(0)

            '11790021
            strInsName_Disp = Trim(dtResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("pi_FirstName"))
            strAgtName = Trim(dtResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("sa_FirstName"))
            strAgPhone = Trim(dtResult.Rows(0).Item("PhoneNumber"))
            strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)

            Dim dsRPU As DataSet
            If QuoteRpu(Company,strPolicy, False, dsRPU, TotalCSV, strErrMsg) = True Then
                'If (dtResult.Rows(0).Item("policycurrency") = "USD" And TotalCSV < 500) Or (dtResult.Rows(0).Item("policycurrency") = "HKD" And TotalCSV < 4000) Then
                'MsgBox("Total Cash Value is less than USD500 / HKD4000, RPU quotation is not applicable.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                'blnCancel = True
                'Exit Sub
                'Else
                dblAmount = dsRPU.Tables(0).Rows(0).Item("NewSI")
                If frmInput.RadioButton1.Checked Then
                    ds.Tables.Add(dsRPU.Tables(1).Clone)
                Else
                    ds.Tables.Add(dsRPU.Tables(1).Copy)
                End If
                ds.Tables("Table2").TableName = "RPU_PROJ"
                'End If
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                blnCancel = True
                Exit Sub
            End If

            dtRPUDetail.Columns.Add("PolicyAccountID", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("Lang", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("PH_Name", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("PH_NamePrefix", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("PH_NameSuffix", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("PI_Name", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("AG_Name", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("AG_Phone", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("AG_Loc", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("Curr", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("NewSI", Type.GetType("System.Double"))
            dtRPUDetail.Columns.Add("PaidToDate", Type.GetType("System.DateTime"))

            If frmInput.chkEng.Checked = True Then
                dr = dtRPUDetail.NewRow
                dr.Item("PolicyAccountID") = strPolicy
                dr.Item("Lang") = "E"
                dr.Item("PH_Name") = Trim(dtResult.Rows(0).Item("ph_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("ph_FirstName"))
                dr.Item("PH_NamePrefix") = Trim(dtResult.Rows(0).Item("ph_NamePrefix"))
                dr.Item("PH_NameSuffix") = Trim(dtResult.Rows(0).Item("ph_NameSuffix"))
                dr.Item("PI_Name") = Trim(dtResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("pi_FirstName"))
                dr.Item("AG_Name") = Trim(dtResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("sa_FirstName"))
                dr.Item("AG_Phone") = dtResult.Rows(0).Item("PhoneNumber")
                dr.Item("AG_Loc") = dtResult.Rows(0).Item("LocationCode")
                dr.Item("Curr") = dtResult.Rows(0).Item("policycurrency")
                dr.Item("NewSI") = dblAmount
                dr.Item("PaidToDate") = dtResult.Rows(0).Item("PaidToDate")
                dtRPUDetail.Rows.Add(dr)
            End If

            If frmInput.chkChi.Checked = True Then
                dr = dtRPUDetail.NewRow
                dr.Item("PolicyAccountID") = strPolicy
                dr.Item("Lang") = "C"
                dr.Item("PH_Name") = Trim(dtResult.Rows(0).Item("ph_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("ph_FirstName"))
                dr.Item("PH_NamePrefix") = Trim(dtResult.Rows(0).Item("ph_NamePrefix"))
                dr.Item("PH_NameSuffix") = Trim(dtResult.Rows(0).Item("ph_NameSuffix"))
                dr.Item("PI_Name") = Trim(dtResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("pi_FirstName"))
                dr.Item("AG_Name") = Trim(dtResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("sa_FirstName"))
                dr.Item("AG_Phone") = dtResult.Rows(0).Item("PhoneNumber")
                dr.Item("AG_Loc") = dtResult.Rows(0).Item("LocationCode")
                dr.Item("Curr") = dtResult.Rows(0).Item("policycurrency")
                dr.Item("NewSI") = dblAmount
                dr.Item("PaidToDate") = dtResult.Rows(0).Item("PaidToDate")
                dtRPUDetail.Rows.Add(dr)
            End If
            ds.Tables.Add(dtRPUDetail)

            CRS_Component.WaitWndFun.ShowMessage("Get Customer information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_customerDetail", New Dictionary(Of String, String)() From {
               {"PolicyNo", strPolicy}
               })



            CRS_Component.WaitWndFun.ShowMessage("Get Logo information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "LOG_POLICY", New Dictionary(Of String, String)() From {
            {"PolicyNo", strPolicy}
            })
            ds.Tables.Add(retDs.Tables(0).Copy())

            CRS_Component.WaitWndFun.ShowMessage("Get csw_policy_address information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_csw_policy_address", New Dictionary(Of String, String)() From {
           {"PolicyNo", strPolicy}
           })
            ds.Tables.Add(retDs.Tables(0).Copy())

            'Dim filename As String = "rpu_la.xsd"
            'Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
            'Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
            'ds.WriteXmlSchema(MyXmlTextWriter)
            'MyXmlTextWriter.Close()
            ' End LifeAsia

            wndMain.Cursor = Cursors.Default

            Try
                Dim tempRPT = rpt.FileName
                tempRPT = Replace(tempRPT, "rassdk://", "")
                'tempRPT = Replace(tempRPT, ".rpt", "_LA.rpt")
                rpt.Load(tempRPT)
                rpt.SetDataSource(ds)
                rpt.SetParameterValue("strCSR", gsCSRName)
                rpt.SetParameterValue("strChiCSR", gsCSRChiName)
                rpt.SetParameterValue("strSAName", strAgtName)
                rpt.SetParameterValue("strInsured", strInsName_Disp)
                rpt.SetParameterValue("strPhone", strAgPhone)
                rpt.SetParameterValue("strRPUAmount", dblAmount)
                rpt.SetParameterValue("ReportType", IIf(frmInput.RadioButton1.Checked = True, "A", "P"))
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            ' Mark Policy Notes if user print the report
            If Destination = PrintDest.pdPrinter Then
                Dim dtPolNote As New DataTable
                dtPolNote.Columns.Add("PolicyNo", Type.GetType("System.String"))
                dtPolNote.Columns.Add("CiwNo", Type.GetType("System.String"))
                dtPolNote.Columns.Add("Type", Type.GetType("System.String"))
                dtPolNote.Columns.Add("subType", Type.GetType("System.String"))
                dtPolNote.Columns.Add("UserID", Type.GetType("System.String"))
                dtPolNote.Columns.Add("EntryDate", Type.GetType("System.String"))
                dtPolNote.Columns.Add("FollowUpUser", Type.GetType("System.String"))
                dtPolNote.Columns.Add("FollowupDate", Type.GetType("System.String"))
                dtPolNote.Columns.Add("Desc", Type.GetType("System.String"))
                dtPolNote.Columns.Add("TypeDesc", Type.GetType("System.String"))
                dtPolNote.Columns.Add("subTypeDesc", Type.GetType("System.String"))
                dtPolNote.Columns.Add("IsHICL", Type.GetType("System.Boolean"))
                dtPolNote.Columns.Add("HICLText", Type.GetType("System.String"))

                dr = dtPolNote.NewRow
                dr.Item("PolicyNo") = strPolicy
                dr.Item("CiwNo") = dtResult.Rows(0).Item("CustomerID")
                dr.Item("Type") = "94"
                dr.Item("subType") = ""
                dr.Item("UserID") = gsUser
                dr.Item("EntryDate") = Today
                dr.Item("FollowUpUser") = ""
                dr.Item("FollowupDate") = Nothing
                dr.Item("Desc") = "RPU SI: " & dtResult.Rows(0).Item("policycurrency") & " " & CStr(dblAmount) & " (PTD: " & Format(dtResult.Rows(0).Item("PaidToDate"), "dd-MMM-yyyy") & ")"
                dr.Item("TypeDesc") = "RPU Quotation"
                dr.Item("subTypeDesc") = ""
                dr.Item("IsHICL") = False
                dr.Item("HICLText") = ""
                dtPolNote.Rows.Add(dr)
                AddPolicyNotes(Company, dtPolNote, strErrMsg)
            End If

            blnCancel = False

        Else
            CRS_Component.WaitWndFun.ShowMessage("Call GetPolicySummary from web Server...")
            dtPoList = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
            dtPoList.TableName = "POLINF"
            If lngErrNo = 0 Then
                ds.Tables.Add(dtPoList)
            End If
            CRS_Component.WaitWndFun.ShowMessage("Call GetPolicyMisc from web Server...")
            dtPolMisc = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
            If lngErrNo = 0 Then
                dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
                If dtPolMisc.DefaultView.Count > 0 Then
                    drs = dtPolMisc.DefaultView.Item(0)
                    strPH = drs.Item("ClientID")
                    dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
                    dtPoList.Rows(0).Item("ClientID") = drs.Item("ClientID")
                End If

                dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PI'"
                If dtPolMisc.DefaultView.Count > 0 Then
                    For i As Integer = 0 To dtPolMisc.DefaultView.Count - 1
                        drs = dtPolMisc.DefaultView.Item(i)
                        If drs.Item("COTRAI") > 1 Then
                            Exit For
                        End If
                        If strPI = "" Then
                            strPI &= drs.Item("ClientID")
                        Else
                            strPI &= "', '" & drs.Item("ClientID")
                        End If
                        If drs.Item("FLD0005") = "01" Then
                            strInsName_Rpt = drs.Item("ClientID")
                        End If
                    Next
                End If
            End If

            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY"), 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                Next
                CRS_Component.WaitWndFun.ShowMessage("Call GetORDUNA from web Server...")
                dtORDUNA = objCS.GetORDUNA("'" & strPI & "','" & strPH & "','" & "00000" & strAG & "'", lngErrNo, strErrMsg)
                GetChiAddr(dtORDUNA)
                If lngErrNo = 0 Then
                    ds.Tables.Add(dtORDUNA)
                End If
            Else
                MsgBox("Policy information not found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            Dim blnChiOnly As Boolean = False
            dtORDUNA.DefaultView.RowFilter = "ClientID = '" & strPH & "'"
            If dtORDUNA.DefaultView.Count > 0 Then
                drs = dtORDUNA.DefaultView.Item(0)
                If drs.Item("USECHIIND") = "Y" Or (drs.Item("ADDRESSLINE1") = "" And drs.Item("ADDRESSLINE2") = "" And drs.Item("ADDRESSLINE3") = "" And drs.Item("AddressCity") = "") Then
                    blnChiOnly = True
                End If
            End If

            Dim blnEng, blnChi As Boolean

            blnEng = frmInput.chkEng.Checked
            blnChi = frmInput.chkChi.Checked

            If blnEng = True AndAlso blnChiOnly Then
                If MsgBox("Only Chinese version is available, print it instead?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Error") = MsgBoxResult.No Then
                    blnCancel = True
                    Exit Sub
                Else
                    blnChi = True
                    blnEng = False
                End If
            End If

            dtORDUNA.DefaultView.RowFilter = "ClientID IN ('" & strPI & "')"
            Dim strInsName As String
            If dtORDUNA.DefaultView.Count > 0 Then
                For i As Integer = 0 To dtORDUNA.DefaultView.Count - 1
                    drs = dtORDUNA.DefaultView.Item(i)
                    If strInsName = "" Then
                        strInsName &= Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
                    Else
                        strInsName &= ", " & Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
                    End If

                    If drs.Item("ClientID") = strInsName_Rpt Then
                        strInsName_Rpt = Trim(drs.Item("NameSuffix"))
                        strInsName_Disp = Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
                    End If

                Next
            Else
                strInsName = ""
            End If

            ' Get RPU Quotation by calling CAPSIL Trigger
            Dim dtRPU As DataTable
            'Dim dblAmount As Double
            CRS_Component.WaitWndFun.ShowMessage("Call LockPolicy from web Server...")
            Call objCS.LockPolicy(strPolicy, gsUser, "", Left(SystemInformation.ComputerName, 10), "MODP", lngErrNo, strErrMsg)

            If lngErrNo = 0 Then
                CRS_Component.WaitWndFun.ShowMessage("Call GetRPUQuote from web Server...")
                dtRPU = objCS.GetRPUQuote(strPolicy, gsUser, strInsName_Rpt, dtPoList.Rows(0).Item("PolicyCurrency"),
                    dblAmount, dtPoList.Rows(0).Item("paidtodate"), lngErrNo, strErrMsg)

                If lngErrNo = 0 Then
                    If dtRPU.Rows.Count > 0 Then

                        dblAmount = dtRPU.Rows(0).Item("strRPUQuote")
                        If IsDBNull(dblAmount) Then
                            dblAmount = 0
                        End If
                    End If

                    'Call objCS.UnLockPolicy(strPolicy, gsUser, "", SystemInformation.ComputerName, "MODP", lngErrNo, strErrMsg)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    End If

                Else
                    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                End If
                CRS_Component.WaitWndFun.ShowMessage("Call UnLockPolicy from web Server...")
                Call objCS.UnLockPolicy(strPolicy, gsUser, "", Left(SystemInformation.ComputerName, 10), "MODP", lngErrNo, strErrMsg)

                If lngErrNo <> 0 Then
                    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                End If
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            End If

            If dblAmount = 0 Then
                MsgBox("RPU Amount is zero.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "RPU Quotation")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If


            CRS_Component.WaitWndFun.ShowMessage("Get agentcodes information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_agentcodes", New Dictionary(Of String, String)() From {
           {"AgentCode", strAG}
           })
            'CRS_Util.Utility.CopyTableAndRename(retDs,"agentcodes","agentcodes")
            'Public shared Function CopyTableAndRename(ds As DataSet,srcTableName As String, destTableName As string) As DataTable
            '    Dim dt As DataTable=ds.Tables(srcTableName).Copy()
            '    dt.TableName=destTableName
            '    Return dt
            'End Function
            ds.Tables.Add(retDs.Tables(0).Copy())


            CRS_Component.WaitWndFun.ShowMessage("Get cswvw_cam_Agent_info information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "Report_Agent_info", New Dictionary(Of String, String)() From {
           {"AgentCode", strAG}
           })
            ds.Tables.Add(retDs.Tables(0).Copy())

            CRS_Component.WaitWndFun.ShowMessage("Get Logo information from CRSAPI Server...")
            retDs = APIServiceBL.CallAPIBusi(g_McuComp, "LOG_POLICY", New Dictionary(Of String, String)() From {
            {"PolicyNo", strPolicy}
            })
            ds.Tables.Add(retDs.Tables(0).Copy())

            CRS_Component.WaitWndFun.ShowMessage("Init data...")
            dtRPUDetail.Columns.Add("PolicyAccountID", Type.GetType("System.String"))
            dtRPUDetail.Columns.Add("Lang", Type.GetType("System.String"))

            If blnEng Then
                dr = dtRPUDetail.NewRow
                dr.Item("PolicyAccountID") = strPolicy
                dr.Item("Lang") = "E"
                dtRPUDetail.Rows.Add(dr)
            End If

            If blnChi Then
                dr = dtRPUDetail.NewRow
                dr.Item("PolicyAccountID") = strPolicy
                dr.Item("Lang") = "C"
                dtRPUDetail.Rows.Add(dr)
            End If

            ds.Tables.Add(dtRPUDetail)

            '#If UAT <> 0 Then
            '        Dim filename As String = "RPUQuote.xsd"
            '        Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
            '        Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
            '        ds.WriteXmlSchema(MyXmlTextWriter)
            '        MyXmlTextWriter.Close()
            '#End If

            dtORDUNA.DefaultView.RowFilter = "ClientID = '" & "00000" & strAG & "'"
            If dtORDUNA.DefaultView.Count > 0 Then
                drs = dtORDUNA.DefaultView.Item(0)
                strAgtName = Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
            Else
                strAgtName = ""
            End If

            If Not ds.Tables("agentcodes") Is Nothing AndAlso ds.Tables("agentcodes").Rows.Count > 0 Then
                strAgPhone = Trim(ds.Tables("agentcodes").Rows(0).Item("PhoneNumber"))
                strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)
            Else
                strAgPhone = ""
            End If

            wndMain.Cursor = Cursors.Default

            Try
                rpt.SetDataSource(ds)
                rpt.Subreports("Address").SetDataSource(ds)
                rpt.SetParameterValue("strCSR", gsCSRName)
                rpt.SetParameterValue("strChiCSR", gsCSRChiName)
                rpt.SetParameterValue("strSAName", strAgtName)
                rpt.SetParameterValue("strInsured", strInsName_Disp)
                rpt.SetParameterValue("strPhone", strAgPhone)
                rpt.SetParameterValue("strRPUAmount", dblAmount)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            blnCancel = False
        End If
    End Sub
End Class
