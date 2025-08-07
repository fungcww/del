'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR3162 & 4101     Add new logic for Payment Letter & Policy Letter & Post Sales Call follow up letter
'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'002    15 Dec 2023     Oliver Ou                       Switch Over Code from Assurance to Bermuda 
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Microsoft.Office.Interop
Imports CRS_Component

Public Class clsReportLogic
    Inherits LetterCommonBL '001

    Private rpt As ReportDocument
    Private strDest As PrintDest   '3b
    Private blnCancel As Boolean
    '001s
    Public outputFilePath As String
    Public Const LINEBREAK As String = "--lineBreak--"
    '001e
    Public objDBHeader As Utility.Utility.ComHeader

    Private configEndPoint_Url As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ConfigEndPoint")

    Private _policyNo As String
    Public Property PolicyNo() As String
        Get
            Return _policyNo
        End Get
        Set(ByVal value As String)
            _policyNo = value
        End Set
    End Property


    Public ReadOnly Property CancelPrint() As Boolean
        Get
            Return blnCancel
        End Get
    End Property

    Public WriteOnly Property CR_Rpt()
        Set(ByVal Value)
            rpt = Value
        End Set
    End Property

    Public Property Destination()
        Get
            Return strDest
        End Get
        Set(ByVal value)
            strDest = value
        End Set
    End Property

    Public Sub RPU_Report()

        Dim ds As New DataSet("RPUQuote")
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

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

        ' RPU Quote and Projection for LifeAsia
        Dim dtResult As DataTable
        Dim LifeAsia As Boolean = False
        strSQL = "Select CompanyID from PolicyAccount where PolicyAccountid = '" & strPolicy & "'"
        If GetDT(strSQL, strCIWConn, dtResult, strErrMsg) Then
            If dtResult.Rows.Count > 0 Then
                If dtResult.Rows(0).Item("CompanyID") = "ING" Or dtResult.Rows(0).Item("CompanyID") = "BMU" Then
                    LifeAsia = True
                End If
            End If
        End If

        ' Get from CIW
        If LifeAsia Then
            strSQL = "Select cph.nameprefix as ph_nameprefix, cph.firstname as ph_firstname, cph.namesuffix as ph_namesuffix, " &
                " cpi.firstname as pi_firstname, cpi.namesuffix as pi_namesuffix, policycurrency, paidtodate, " &
                " csa.firstname as sa_firstname, csa.namesuffix as sa_namesuffix, a.phonenumber, a.locationcode, cph.customerid " &
                " from csw_poli_rel rph, customer cph, coveragedetail rpi, customer cpi, csw_poli_rel rsa, customer csa, agentcodes a, policyaccount p " &
                " where rph.policyaccountid = '" & strPolicy & "' " &
                " and rph.policyaccountid = rpi.policyaccountid and rph.policyaccountid = p.policyaccountid " &
                " and rph.policyrelatecode = 'PH' and rph.customerid = cph.customerid " &
                " and rpi.customerid = cpi.customerid and rpi.trailer=1" &
                " and rph.policyaccountid = rsa.policyaccountid " &
                " and rsa.policyrelatecode = 'SA' and rsa.customerid = csa.customerid and csa.agentcode = a.agentcode"

            '11790021
            If GetDT(strSQL, strCIWConn, dtResult, strErrMsg) Then
                strInsName_Disp = Trim(dtResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("pi_FirstName"))
                strAgtName = Trim(dtResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtResult.Rows(0).Item("sa_FirstName"))
                strAgPhone = Trim(dtResult.Rows(0).Item("PhoneNumber"))
                strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)

                Dim dsRPU As DataSet
                If QuoteRpu("ING", strPolicy, False, dsRPU, TotalCSV, strErrMsg) = True Then
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

            strSQL = "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
                "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "
            strSQL &= "; SELECT * from csw_policy_address where cswpad_poli_id = '" & strPolicy & "'"

            sqlconnect.ConnectionString = strCIWConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.TableMappings.Add("Logo1", "csw_policy_address")
            Try
                sqlda.Fill(ds, "Logo")
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

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
                tempRPT = Replace(tempRPT, ".rpt", "_LA.rpt")
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
                AddPolicyNotes("ING", dtPolNote, strErrMsg)
            End If

            blnCancel = False

        Else
            dtPoList = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
            dtPoList.TableName = "POLINF"
            If lngErrNo = 0 Then
                ds.Tables.Add(dtPoList)
            End If

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

            Call objCS.LockPolicy(strPolicy, gsUser, "", Left(SystemInformation.ComputerName, 10), "MODP", lngErrNo, strErrMsg)

            If lngErrNo = 0 Then

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

            strSQL = "select PhoneNumber from agentcodes where AgentCode = '" & strAG & "'; "
            strSQL &= "select * from cswvw_cam_Agent_info where cswagi_agent_code = '" & strAG & "'; "
            sqlconnect.ConnectionString = strCIWConn


            strSQL &= "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
                "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
                ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.TableMappings.Add("agentcodes1", "cswvw_cam_Agent_info")

            sqlda.TableMappings.Add("agentcodes2", "Logo")


            Try
                sqlda.Fill(ds, "agentcodes")
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

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

    Public Sub Projection_Rpt(ByVal policyNo As String, ByVal scrType As String, ByRef dtRpt As DataTable, ByVal sumIns As Double, ByVal chiName As String, ByRef Lang As String)
        'oliver 2024-5-3 added comment for Table_Relocate_Sprint13,it will not call because obsolate function
        Dim ds As New DataSet("dsProjection")
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

        Dim strPolicy, strErrMsg As String
        Dim lngErrNo As Long

        Dim dtPoList, dtORDUNA, dtPolMisc As DataTable
        Dim strLang As String = "E"

        strPolicy = policyNo
        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If

        'Policy Summary
        dtPoList = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
        dtPoList.TableName = "POLINF"
        If lngErrNo = 0 Then
            '1st table in ds
            ds.Tables.Add(dtPoList)
        End If

        Dim drs As DataRowView
        Dim strPI, strPH, strAG As String   'Policy Insured, Holder, Agent
        Dim strPolName As String

        'Policy Miscellaneous
        dtPolMisc = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
        strPolName = dtPolMisc.Rows(0).Item("Description")
        If lngErrNo = 0 Then
            'Policy Holder
            dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
            If dtPolMisc.DefaultView.Count > 0 Then
                drs = dtPolMisc.DefaultView.Item(0)
                strPH = drs.Item("ClientID")
                dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
                dtPoList.Rows(0).Item("ClientID") = drs.Item("ClientID")
            End If

            'Policy Insured
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
                Next
            End If
        End If

        If dtPoList.Rows.Count > 0 Then
            'Agent
            strAG = Right(dtPoList.Rows(0).Item("POAGCY"), 5)
            For i As Integer = 0 To dtPoList.Rows.Count - 1
                dtPoList.Rows(i).Item("POAGCY") = strAG
            Next

            'Get the Info for the Insured, Holder and the Agent
            dtORDUNA = objCS.GetORDUNA("'" & strPI & "','" & strPH & "','" & "00000" & strAG & "'", lngErrNo, strErrMsg)
            'Set the Info for the Insured, Holder and the Agent
            GetChiAddr(dtORDUNA)
            If lngErrNo = 0 Then
                '2nd table in ds
                ds.Tables.Add(dtORDUNA)
            End If
        Else
            MsgBox("Policy information not found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim blnChiOnly As Boolean = False
        'Check if the Holder prefer Chinese
        dtORDUNA.DefaultView.RowFilter = "ClientID = '" & strPH & "'"
        If dtORDUNA.DefaultView.Count > 0 Then
            drs = dtORDUNA.DefaultView.Item(0)
            If drs.Item("USECHIIND") = "Y" Or (drs.Item("ADDRESSLINE1") = "" And drs.Item("ADDRESSLINE2") = "" And drs.Item("ADDRESSLINE3") = "" And drs.Item("AddressCity") = "") Then
                blnChiOnly = True
                strLang = "C"
            End If
        End If

        'GetProjection for report
        Dim dtProj As DataTable
        Dim drProj As DataRow
        Dim intColCnt As Integer = dtRpt.Columns.Count
        Dim ar() As Object

        dtProj = dtRpt.Clone

        dtProj.Columns.Add("Lang", Type.GetType("System.String"))
        dtProj.Columns.Add("PolicyAccountID", Type.GetType("System.String"))

        For i As Integer = 0 To dtRpt.Rows.Count - 1

            ar = dtRpt.Rows(i).ItemArray
            ReDim Preserve ar(intColCnt + 1)
            If Not strLang = "C" Then
                ar(intColCnt) = "E"
                ar(intColCnt + 1) = strPolicy
                dtProj.Rows.Add(ar)
            End If
            ar(intColCnt) = "C"
            ar(intColCnt + 1) = strPolicy
            dtProj.Rows.Add(ar)
        Next

        ds.Tables.Add(dtProj)

        'Get Agent's Info, e.g. loc_code
        strSQL = "select * from cswvw_cam_Agent_info where cswagi_agent_code = '" & strAG & "'; "
        strSQL &= "select PhoneNumber from agentcodes where AgentCode = '" & strAG & "';"
        strSQL &= "select * from csw_currency_codes; "

        strSQL &= "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
        "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        sqlda.TableMappings.Add("cswvw_cam_Agent_info1", "agentcodes")
        sqlda.TableMappings.Add("cswvw_cam_Agent_info2", "csw_currency_codes")

        sqlda.TableMappings.Add("cswvw_cam_Agent_info3", "Logo")

        Try
            sqlda.Fill(ds, "cswvw_cam_Agent_info")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'Set Signature (Login User Name)
        'Dim strCSRName, strCSRCName As String
        'If strLang <> "C" Then
        '    strCSRName = gsCSRName
        'Else
        '    strCSRName = gsCSRChiName
        'End If

        'Set Agent Name
        dtORDUNA.DefaultView.RowFilter = "ClientID = '" & "00000" & strAG & "'"
        Dim strAgtName As String
        If dtORDUNA.DefaultView.Count > 0 Then
            drs = dtORDUNA.DefaultView.Item(0)
            strAgtName = Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
        Else
            strAgtName = ""
        End If

        'Set Insured Name
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
            Next
        Else
            strInsName = ""
        End If

        'Set Agent Phone
        Dim strAgPhone As String
        If Not ds.Tables("agentcodes") Is Nothing AndAlso ds.Tables("agentcodes").Rows.Count > 0 Then
            strAgPhone = Trim(ds.Tables("agentcodes").Rows(0).Item("PhoneNumber"))
            strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)
        Else
            strAgPhone = ""
        End If

        '#If UAT <> 0 Then
        '        Try
        '            Dim filename As String = "projection1.xsd"
        '            Dim myFileStream As New System.IO.FileStream(filename, IO.FileMode.Create)
        '            Dim myXMLTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        '            ds.WriteXmlSchema(myXMLTextWriter)
        '            myXMLTextWriter.Close()
        '        Catch ex As Exception
        '            MsgBox(ex.ToString)
        '        End Try
        '#End If

        'Set the Report Parameters
        Lang = strLang
        Try
            rpt.SetDataSource(ds)
            rpt.Subreports("Address").SetDataSource(ds)
            rpt.SetParameterValue("strSAName", strAgtName)
            rpt.SetParameterValue("strInsured", strInsName)
            rpt.SetParameterValue("strPhone", strAgPhone)
            rpt.SetParameterValue("strLangC", strLang)

            rpt.SetParameterValue("strCSR", gsCSRName)
            rpt.SetParameterValue("strCSRC", gsCSRChiName)

            rpt.SetParameterValue("strScrType", scrType)
            rpt.SetParameterValue("strSumIns", sumIns)

            rpt.SetParameterValue("strCPolName", chiName)
            rpt.SetParameterValue("strPolName", strPolName)

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        blnCancel = False

    End Sub

    Public Sub Payment_Rpt()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
        Dim ds1 As New DataSet
        ds1.ReadXmlSchema(My.Application.Info.DirectoryPath & "\payment.xsd") '"C:\View\BCH_LACRS_UAT\E_TEAM\CS2005\INGLife.LifeAsia\CS2005\payment.xsd")
        Dim IsLARec As Boolean = False
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end

        Dim ds As New DataSet("dsPaymentHistory")
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

        Dim strPolicy, strErrMsg As String
        Dim lngErrNo As Long
        Dim intCnt As Integer
        Dim datFrom, datStart, datEnd, datLast As Date
        Dim dtPAYH, dtPAYH_All, dtPoList, dtORDUNA, dtPolMisc As DataTable
        Dim blnCont As Boolean = True
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "Payment History Letter"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Text = DateSerial(Year(Today), 1, 1)
        frmInput.txtTo.Text = Today
        frmInput.RadioButton1.Enabled = False
        frmInput.RadioButton2.Enabled = False
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

        If frmInput.chkChi.Checked = False AndAlso frmInput.chkEng.Checked = False Then
            MsgBox("Please choose Eng/Chi version of letter.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If
        'strPolicy = "2046300010"
        'datFrom = #1/1/2006#

        wndMain.Cursor = Cursors.WaitCursor

        '#If UAT <> 0 Then
        '        strPolicy = "U9611330"
        '#End If

        Dim drs As DataRowView
        Dim strPI, strPH, strAG As String


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
        Dim dsPolicySend As New DataSet
        Dim dsPolicyCurr As New DataSet
        Dim objMQQueHeader As Utility.Utility.MQHeader
        Dim strTime As String = ""
        Dim strerr As String = ""
        Dim blnGetPolicy As Boolean
        Dim dr As DataRow
        Dim dtSendData As New DataTable
        Dim objDBHeader As Utility.Utility.ComHeader

        dtSendData.Columns.Add("PolicyNo")
        dr = dtSendData.NewRow
        dr("PolicyNo") = RTrim(strPolicy)
        dtSendData.Rows.Add(dr)

        dsPolicySend.Tables.Add(dtSendData)
        objMQQueHeader.UserID = gsUser
        objMQQueHeader.QueueManager = g_Qman '"LACSQMGR1" '"WINTEL"
        objMQQueHeader.RemoteQueue = g_WinRemoteQ '"LACSSIT02.TO.LA400SIT02" '"LIFEASIA.RQ1"
        objMQQueHeader.ReplyToQueue = g_LAReplyQ '"LA400SIT02.TO.LACSSIT02" '"WINTEL.RQ1"
        objMQQueHeader.LocalQueue = g_WinLocalQ  '"LACSSIT02.QUEUE1.LCL" '"WINTEL.LQ1"
        objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout

        objDBHeader.UserID = gsUser
        objDBHeader.EnvironmentUse = g_Env '"SIT02"
        objDBHeader.ProjectAlias = "LAS" '"LAS"
        objDBHeader.CompanyID = g_Comp '"ING"
        objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        If My.Settings.LAReady = True Then
            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            clsPOS.MQQueuesHeader = objMQQueHeader
            clsPOS.DBHeader = objDBHeader

            dsPolicyCurr.Tables.Clear()

            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    ds1.Tables(0).Columns.Remove("ClientID")
                    Dim dr1 As DataRow
                    dr1 = ds1.Tables(0).NewRow()
                    For i As Integer = 0 To ds1.Tables(0).Columns.Count - 1
                        Select Case ds1.Tables(0).Columns(i).ToString
                            Case "PolicyAccountID"
                                dr1(i) = RTrim(strPolicy)
                            Case "PaidToDate"
                                dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("Paid_To_Date")
                            Case "POAGCY"
                                dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("S_Agent_No")
                            Case Else
                                Select Case ds1.Tables(0).Columns(i).DataType.ToString
                                    Case "System.String"
                                        dr1(i) = ""
                                    Case "System.Decimal", "System.Int16"
                                        dr1(i) = 0
                                    Case "System.DateTime"
                                    Case Else
                                        dr1(i) = ""
                                End Select
                        End Select
                    Next
                    IsLARec = True
                    ds1.Tables(0).Rows.Add(dr1)
                    dtPoList = ds1.Tables(0).Copy
                    ds.Tables.Add(dtPoList)
                End If
            End If

        End If

        If IsLARec Then
            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY").ToString.Trim, 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                Next
            End If
            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()
            ''''''''''''''''''''''
            strSQL = "select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('PI')"
            Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
            Dim sqlrdr As SqlDataReader = sqlcmd.ExecuteReader()
            If sqlrdr.Read Then
                strPI = sqlrdr("customerID")
            End If
            sqlrdr.Close()
            sqlcmd.Dispose()
            ''''''''''''''''''''''

            strSQL = "select customerID from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('PH')"
            sqlcmd = New SqlCommand(strSQL, sqlconnect)
            sqlrdr = sqlcmd.ExecuteReader()
            If sqlrdr.Read Then
                dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
                dtPoList.Rows(0).Item("ClientID") = sqlrdr("customerID")
                strPH = sqlrdr("customerID")
            End If
            sqlrdr.Close()
            sqlconnect.Close()

            'strSQL = "select AgentCode from Customer where customerid in (select customerid from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('SA'))"
            'If sqlrdr.Read Then
            '    strPI = sqlrdr("customerID")
            'End If
            'sqlrdr.Close()
            'sqlcmd.Dispose()

            'dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
            'If dtPolMisc.DefaultView.Count > 0 Then
            ' drs = dtPolMisc.DefaultView.Item(0)
            'strPH = drs.Item("ClientID")
            'dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
            'dtPoList.Rows(0).Item("ClientID") = drs.Item("ClientID")
            'End If
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

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.Fill(ds1, "ORDUNA")
            ds.Tables.Add(ds1.Tables("ORDUNA").Copy)
            dtORDUNA = ds1.Tables("ORDUNA").Copy
            sqlda.Dispose()
            sqlconnect.Close()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
            Dim clsCRS As New LifeClientInterfaceComponent.clsCRS
            dtSendData = New DataTable
            dsPolicySend.Tables.RemoveAt(0)
            dsPolicyCurr.Tables.RemoveAt(0)
            dtSendData.Columns.Add("Policy_No")
            'dr = dtSendData.NewRow
            dtSendData.Columns.Add("FromDate")
            'dr = dtSendData.NewRow
            'dtSendData.Rows.Add(dr)
            dtSendData.Columns.Add("ToDate")
            dr = dtSendData.NewRow
            dtSendData.Rows.Add(dr)
            'dtSendData.Rows(0)("FromDate") = datFrom
            'dtSendData.Rows(0)("ToDate") = datEnd
            dtSendData.Rows(0)("ToDate") = datFrom
            dtSendData.Rows(0)("FromDate") = datEnd
            dtSendData.Rows(0)("Policy_No") = RTrim(strPolicy)
            'dtSendData.Rows.Add(dr)
            dsPolicySend.Tables.Add(dtSendData)
            clsCRS.DBHeader = objDBHeader   ' Fix UAT wrong URL problem
            clsCRS.MQQueuesHeader = objMQQueHeader
            blnGetPolicy = clsCRS.getPaymentHist(dsPolicySend, dsPolicyCurr, strerr)
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    ds1.Tables(2).Columns.Remove("Lang")
                    Dim dr1 As DataRow
                    For j As Integer = 0 To dsPolicyCurr.Tables(1).Rows.Count - 1
                        dr1 = ds1.Tables(2).NewRow()
                        For i As Integer = 0 To ds1.Tables(2).Columns.Count - 1
                            Select Case ds1.Tables(2).Columns(i).ToString
                                Case "PaymentType"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("PayTypeCode")
                                    'dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("PayTypeDesc")
                                    '                       Case "ClientID"
                                    '                            dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("Life_No")
                                Case "Date"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("Payment_Date")
                                Case "Currency"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("Curr")
                                Case "ReceivedAmount"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("RecAmt")
                                Case "PolicyAccountID"
                                    dr1(i) = RTrim(strPolicy)
                                Case Else
                                    Select Case ds1.Tables(2).Columns(i).DataType.ToString
                                        Case "System.String"
                                            dr1(i) = ""
                                        Case "System.Decimal", "System.Int16"
                                            dr1(i) = 0
                                        Case "System.DateTime"
                                        Case Else
                                            dr1(i) = ""
                                    End Select
                            End Select
                        Next
                        ds1.Tables(2).Rows.Add(dr1)
                    Next
                    dtPAYH = ds1.Tables(2).Copy
                    'dtPAYH_All = dtPAYH.Clone 
                    lngErrNo = 0
                    Dim blnEng, blnChi As Boolean

                    blnEng = frmInput.chkEng.Checked
                    blnChi = frmInput.chkChi.Checked
                    '                    ds.Tables.Add(ds1.Tables(2).Copy)
                    If lngErrNo = 0 Then

                        If dtPAYH_All Is Nothing Then
                            dtPAYH_All = dtPAYH.Clone
                            dtPAYH_All.Columns.Add("Lang", System.Type.GetType("System.String"))
                        End If

                        Dim intColCnt As Integer = dtPAYH.Columns.Count
                        Dim ar() As Object
                        Dim i As Integer
                        intCnt = dtPAYH.Rows.Count - 1
                        If dtPAYH.Rows.Count > 0 Then
                            If dtPAYH.Rows(0).Item("ContFlag") = "Y" Then
                                datFrom = dtPAYH.Rows(intCnt).Item("Date")
                                datLast = datFrom
                                blnCont = True
                            Else
                                datLast = #1/1/1900#
                                blnCont = False
                            End If
                        Else
                            datLast = #1/1/1900#
                            blnCont = False
                        End If
                        For i = 0 To intCnt
                            If dtPAYH.Rows(i).Item("Date") <> datLast And dtPAYH.Rows(i).Item("Date") >= datEnd Then
                                ar = dtPAYH.Rows(i).ItemArray
                                ReDim Preserve ar(intColCnt)
                                'If Not blnChiOnly Then
                                If blnEng Then
                                    ar(intColCnt) = "E"
                                    dtPAYH_All.Rows.Add(ar)
                                End If
                                If blnChi Then
                                    ar(intColCnt) = "C"
                                    dtPAYH_All.Rows.Add(ar)
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

        Else

            dtPoList = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
            If Not dtPoList Is Nothing Then
                dtPoList.TableName = "POLINF"
                If lngErrNo = 0 Then
                    ds.Tables.Add(dtPoList)
                End If
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
            dtPolMisc = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
            ' ''If lngErrNo <> 0 Then
            ' ''    Dim dt1 As New DataTable
            ' ''    Dim dr1 As DataRow
            ' ''    Dim dc1 As DataColumn
            ' ''    dc1 = New DataColumn()
            ' ''    dc1.ColumnName = "PolicyRelateCode"
            ' ''    dc1.DataType = System.Type.GetType("System.String")
            ' ''    dt1.Columns.Add(dc1)
            ' ''    dc1 = New DataColumn()
            ' ''    dc1.ColumnName = "ClientID"
            ' ''    dc1.DataType = System.Type.GetType("System.String")
            ' ''    dt1.Columns.Add(dc1)
            ' ''    dr1 = dt1.NewRow()
            ' ''    For i As Integer = 0 To dt1.Columns.Count - 1
            ' ''        Select Case dt1.Columns(i).ToString
            ' ''            Case "PolicyRelateCode"
            ' ''                dr1(i) = "PH"
            ' ''            Case "ClientID"
            ' ''                dr1(i) = "50001527"
            ' ''            Case Else
            ' ''                Select Case dt1.Columns(i).DataType.ToString
            ' ''                    Case "System.String"
            ' ''                        dr1(i) = ""
            ' ''                    Case "System.Decimal", "System.Int16"
            ' ''                        dr1(i) = 0
            ' ''                    Case "System.DateTime"
            ' ''                    Case Else
            ' ''                        dr1(i) = ""
            ' ''                End Select
            ' ''        End Select
            ' ''    Next

            ' ''    dt1.Rows.Add(dr1)
            ' ''    dtPolMisc = dt1.Copy
            ' ''    lngErrNo = 0
            ' ''    strErrMsg = ""
            ' ''    'ds.Tables.Add(ds1.Tables(1).Copy)
            ' ''End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
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
                    Next
                End If
            End If

            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY"), 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                    'If dtPoList.Rows(i).Item("PolicyRelateCode") = "PI" Then
                    '    strPI = dtPoList.Rows(i).Item("ClientID")
                    'End If
                    'If dtPoList.Rows(i).Item("PolicyRelateCode") = "PH" Then
                    '    strPH = dtPoList.Rows(i).Item("ClientID")
                    'End If
                Next

                dtORDUNA = objCS.GetORDUNA("'" & strPI & "','" & strPH & "','" & "00000" & strAG & "'", lngErrNo, strErrMsg)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
                ' ''If lngErrNo <> 0 Then
                ' ''    Dim dr1 As DataRow
                ' ''    dr1 = ds1.Tables(1).NewRow()
                ' ''    For i As Integer = 0 To ds1.Tables(1).Columns.Count - 1
                ' ''        Select Case ds1.Tables(1).Columns(i).ToString
                ' ''            Case "NamePrefix"
                ' ''                dr1(i) = "TestingPrefix"
                ' ''            Case "Namesuffix"
                ' ''                dr1(i) = "TestingSuffix"
                ' ''            Case "ClientID"
                ' ''                dr1(i) = "50001527"
                ' ''            Case Else
                ' ''                Select Case ds1.Tables(1).Columns(i).DataType.ToString
                ' ''                    Case "System.String"
                ' ''                        dr1(i) = ""
                ' ''                    Case "System.Decimal", "System.Int16"
                ' ''                        dr1(i) = 0
                ' ''                    Case "System.DateTime"
                ' ''                    Case Else
                ' ''                        dr1(i) = ""
                ' ''                End Select
                ' ''        End Select
                ' ''    Next
                ' ''    ds1.Tables(1).Rows.Add(dr1)
                ' ''    dtPoList = ds1.Tables(1).Copy
                ' ''    ds.Tables.Add(ds1.Tables(1).Copy)
                ' ''End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
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

            While blnCont
                dtPAYH = objCS.GetPaymentHistory(strPolicy, datFrom, lngErrNo, strErrMsg)

                If lngErrNo = 0 Then

                    If dtPAYH_All Is Nothing Then
                        dtPAYH_All = dtPAYH.Clone
                        dtPAYH_All.Columns.Add("Lang", System.Type.GetType("System.String"))
                    End If

                    Dim intColCnt As Integer = dtPAYH.Columns.Count
                    Dim ar() As Object
                    Dim i As Integer
                    intCnt = dtPAYH.Rows.Count - 1

                    If dtPAYH.Rows(0).Item("ContFlag") = "Y" Then
                        datFrom = dtPAYH.Rows(intCnt).Item("Date")
                        datLast = datFrom
                        blnCont = True
                    Else
                        datLast = #1/1/1900#
                        blnCont = False
                    End If

                    For i = 0 To intCnt
                        If dtPAYH.Rows(i).Item("Date") <> datLast And dtPAYH.Rows(i).Item("Date") >= datEnd Then
                            ar = dtPAYH.Rows(i).ItemArray
                            ReDim Preserve ar(intColCnt)
                            'If Not blnChiOnly Then
                            If blnEng Then
                                ar(intColCnt) = "E"
                                dtPAYH_All.Rows.Add(ar)
                            End If
                            If blnChi Then
                                ar(intColCnt) = "C"
                                dtPAYH_All.Rows.Add(ar)
                            End If
                        End If
                    Next
                Else
                    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End While
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''LA rec modify
        If dtPAYH_All.Rows.Count = 0 Then
            MsgBox("No Payment History record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            ds.Tables.Add(dtPAYH_All)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        strSQL = "select * from " & gcPOS & "vw_PaymentTypeCodes "
        strSQL &= "select * from cswvw_cam_Agent_info where cswagi_agent_code = '" & strAG & "'; "
        strSQL &= "select * from DDARejectReasonCodes; "
        strSQL &= "select * from CCDRRejectReasonCodes; "
        strSQL &= "select * from " & serverPrefix & "csw_payh_remark_code; "
        strSQL &= "select PhoneNumber from agentcodes where AgentCode = '" & strAG & "';"

        strSQL &= "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
        "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
        ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        'If IsLARec Then
        '    Dim objDB As New Object
        '    Dim ConnectionAlias As String = g_Comp + "CIW" + g_Env
        '    ConnectDB(objDB, g_ProjectAlias, ConnectionAlias, g_UserType, strerr)
        '    sqlconnect.ConnectionString = objDB.getDBString 'strCIWConn
        'Else
        sqlconnect.ConnectionString = strCIWConn
        'End If

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        sqlda.TableMappings.Add("PaymentTypeCodes1", "cswvw_cam_Agent_info")
        sqlda.TableMappings.Add("PaymentTypeCodes2", "DDARejectReasonCodes")
        sqlda.TableMappings.Add("PaymentTypeCodes3", "CCDRRejectReasonCodes")
        sqlda.TableMappings.Add("PaymentTypeCodes4", "csw_payh_remark_code")
        sqlda.TableMappings.Add("PaymentTypeCodes5", "agentcodes")

        sqlda.TableMappings.Add("PaymentTypeCodes6", "Logo")

        Try
            sqlda.Fill(ds, "PaymentTypeCodes")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        '#If UAT <> 0 Then
        '        Dim filename As String = "payment1.xsd"
        '        Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
        '        Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        '        ds.WriteXmlSchema(MyXmlTextWriter)
        '        MyXmlTextWriter.Close()
        '#End If

        'MsgBox(ds.Tables("POLINF").Rows.Count)
        'MsgBox(dtPAYH.Rows.Count)
        'MsgBox(dtORDUNA.Rows.Count)
        'MsgBox(dtORDUNA.Rows(0)("ClientID"))
        'MsgBox(dtORDUNA.Rows(1)("ClientID"))

        dtORDUNA.DefaultView.RowFilter = "ClientID = '" & "00000" & strAG & "'"
        Dim strAgtName As String
        If dtORDUNA.DefaultView.Count > 0 Then
            drs = dtORDUNA.DefaultView.Item(0)
            strAgtName = Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
        Else
            strAgtName = ""
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
            Next
        Else
            strInsName = ""
        End If

        Dim strAgPhone As String
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
            rpt.SetParameterValue("strInsured", strInsName)
            rpt.SetParameterValue("strStart", CDate(frmInput.FromDate))
            rpt.SetParameterValue("strEnd", CDate(frmInput.ToDate))
            rpt.SetParameterValue("strPhone", strAgPhone)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        blnCancel = False

    End Sub

    'Claim_rpt() - Prepare data for Claim Breakdown Report
    Public Sub Claim_rpt()
        Dim strSQL As String
        Dim intClaim As Integer
        Dim daClaimRpt As SqlDataAdapter
        Dim sqlRd As SqlDataReader
        Dim sqlCmd As SqlCommand
        Dim sqlConn As New SqlConnection
        Dim frmParam As New frmRptClaims
        Dim dsClaimRpt As New DataSet
        Dim blnIsLastRpt As Boolean
        Dim strClaimNo As String
        Dim strClaimOccur As String

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strClaimNo = frmParam.txtClaimNo.Text
            strClaimOccur = frmParam.txtClaimOccur.Text
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        If strClaimNo = "" Or strClaimOccur = "" Then
            MsgBox("Please input both Claim No. and Claim Occur.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
            Exit Sub
        End If

        'sqlConn.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        sqlConn.ConnectionString = strCIWConn
        'sqlConn.ConnectionString = "workstation id=CNG31501PY;user id=vantiveowner;data source=hksqldev1;persist security info=True;initial catalog=vantive;password=ownerdev"
        strSQL = "SELECT mcschd_clm_status, mcschd_converted, mcsmsl_message_no " &
                         "FROM " & cSQL3 & ".mcs.dbo.mcs_claim_header_details " &
                         "LEFT OUTER JOIN " & cSQL3 & ".mcs.dbo.mcs_message_log " &
                         "ON mcschd_claim_no=mcsmsl_claim_no and mcschd_claim_occur=mcsmsl_claim_occur and mcsmsl_message_no='F402' " &
                         "WHERE mcschd_claim_no=" & strClaimNo &
                         "and mcschd_claim_occur=" & strClaimOccur
        sqlConn.Open()
        sqlCmd = New SqlCommand(strSQL, sqlConn)
        Try
            sqlRd = sqlCmd.ExecuteReader()
        Catch eSQL As SqlException
            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
        End Try
        If sqlRd.Read() Then
            If IsDBNull(sqlRd.Item("mcschd_clm_status")) Then
                MsgBox("Claim Breakdown Charges report with the claim no " & strClaimNo & " and claim occur " & strClaimOccur & " cannot be generated as criteria not match.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                Exit Sub
            ElseIf (sqlRd.Item("mcschd_clm_status") <> "WFW" And sqlRd.Item("mcschd_clm_status") <> "FIN") Or
                    (sqlRd.Item("mcschd_converted") <> 0) Or (Not IsDBNull(sqlRd.Item("mcsmsl_message_no")) AndAlso sqlRd.Item("mcsmsl_message_no") = "F402") Then
                MsgBox("Claim Breakdown Charges report with the claim no " & strClaimNo & " and claim occur " & strClaimOccur & " cannot be generated as criteria not match.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                Exit Sub
            End If
        Else
            MsgBox("Cannot find the claim information with the claim no " & strClaimNo & " and claim occur " & strClaimOccur, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
            Exit Sub
        End If
        sqlRd.Close()

        'Determine blnIsLastRpt
        strSQL = "SELECT max(mcschd_claim_occur) from " & cSQL3 & ".mcs.dbo.mcs_claim_header_details " &
                 "WHERE mcschd_converted=0 and mcschd_clm_status in ('FIN','WFW') " &
                 "and mcschd_claim_no=" + strClaimNo
        sqlCmd.CommandText = strSQL
        Try
            sqlRd = sqlCmd.ExecuteReader()
        Catch eSQL As SqlException
            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
        End Try
        If sqlRd.Read() Then
            If CStr(sqlRd.Item(0)) = strClaimOccur Then
                blnIsLastRpt = True
            Else
                blnIsLastRpt = False
                MsgBox("Remain balance will not be displayed as " + strClaimOccur + " is not the latest claim occur for claim no " + strClaimNo + ".", MsgBoxStyle.Information, "Claim Breakdown Charges")
                Exit Sub
            End If
        End If
        sqlRd.Close()

        'Find product name
        strSQL = "SELECT * FROM Product "
        daClaimRpt = New SqlDataAdapter(strSQL, sqlConn)
        daClaimRpt.Fill(dsClaimRpt, "Product")

        'Preparing report data
        strSQL = "SELECT * FROM mcsvw_claim_breakdown_rpt " &
                 "WHERE mcscbh_claim_no=" & strClaimNo & " and mcscbh_claim_occur=" & strClaimOccur & " " &
                 "ORDER BY mcsply_policy_no, mcscpc_plan_type, mcscpc_plan_code, mcscpc_coverage, mcscbh_benefit_code "
        daClaimRpt = New SqlDataAdapter(strSQL, sqlConn)
        daClaimRpt.Fill(dsClaimRpt, "mcsvw_claim_breakdown_rpt")

        'Set parameter information
        rpt.SetDataSource(dsClaimRpt)
        rpt.SetParameterValue("ClaimNo", strClaimNo)
        rpt.SetParameterValue("ClaimOccur", strClaimOccur)
        rpt.SetParameterValue("UserName", gsUser)
        rpt.SetParameterValue("IsLastRpt", blnIsLastRpt)

        daClaimRpt.Dispose()
        sqlCmd.Dispose()
        sqlConn.Close()
        sqlConn.Dispose()

        blnCancel = False

    End Sub

    'Medium_Rpt() - Prepare data for Service Nature Medium Report
    Public Sub Service_Nature_Medium_Rpt()
        Dim strSQL As String
        Dim daSrvLog As SqlDataAdapter
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim dsSrvLog As New DataSet
        Dim sqlConn As New SqlConnection

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        'sqlConn.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        sqlConn.ConnectionString = strCIWConn
        strSQL = "SELECT EventCategoryCode, EventTypeCode, EventTypeDetailCode, EventSourceMediumCode, " &
                 "count(ServiceEventNumber) as 'Count' " &
                 "FROM ServiceEventDetail " &
                 "WHERE left(convert(char,EventInitialDateTime,120),10) >= '" & strFromDate & "' " &
                 "and left(convert(char,EventInitialDateTime,120),10) <= '" & strToDate & "' " &
                 "GROUP BY EventCategoryCode, EventTypeCode, EventTypeDetailCode, EventSourceMediumCode "
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        Try
            daSrvLog.Fill(dsSrvLog, "csw_service_log_count")
        Catch eSQL As SqlException
            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Service Event Category")
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Service Event Category")
        End Try

        strSQL = "SELECT * FROM " & gcPOS & "vw_csw_event_category_code "
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "csw_event_category_code")

        strSQL = "SELECT * FROM ServiceEventTypeCodes"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "ServiceEventTypeCodes")

        strSQL = "SELECT * FROM " & gcPOS & "vw_csw_event_typedtl_code"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "csw_event_typedtl_code")

        strSQL = "SELECT * FROM EventSourceMediumCodes"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "EventSourceMediumCodes")

        Try
            rpt.SetDataSource(dsSrvLog)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        daSrvLog.Dispose()
        dsSrvLog.Dispose()
        sqlConn.Close()
        sqlConn.Dispose()

        blnCancel = False

    End Sub

    'Service_Medium_Initiator_rpt() - Prepare data for Service Medium Initiator report
    Public Sub Service_Medium_Initiator_rpt()
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL As String
        Dim dsSrvLog As New DataSet
        Dim daSrvLog As SqlDataAdapter
        Dim sqlConn As New SqlConnection

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If
        'sqlConn.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()
        strSQL = "SELECT EventSourceInitiatorCode, EventSourceMediumCode, " &
                 "count(ServiceEventNumber) as 'Count' " &
                 "FROM ServiceEventDetail " &
                 "WHERE left(convert(char,EventInitialDateTime,120),10) >= '" & strFromDate & "' and left(convert(char,EventInitialDateTime,120),10) <= '" & strToDate & "' " &
                 "GROUP BY EventSourceInitiatorCode, EventSourceMediumCode "
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        Try
            daSrvLog.Fill(dsSrvLog, "csw_service_medium_count")
        Catch eSQL As SqlException
            MsgBox("SQL Exception: " & eSQL.Message, MsgBoxStyle.Exclamation, "Service Event Source Medium")
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Service Event Source Medium")
        End Try

        strSQL = "SELECT * FROM EventSourceMediumCodes"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "EventSourceMediumCodes")
        strSQL = "select * from EventSourceInitiatorCodes"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "EventSourceInitiatorCodes")

        Try
            rpt.SetDataSource(dsSrvLog)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        daSrvLog.Dispose()
        dsSrvLog.Dispose()
        sqlConn.Close()
        sqlConn.Dispose()

        blnCancel = False

    End Sub

    'Service_Medium_CSR() - Prepare data for Service Medium by Initiator report
    Public Sub Service_Medium_CSR_rpt()
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL As String
        Dim dsSrvLog As New DataSet
        Dim daSrvLog As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'sqlConn.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        sqlConn.ConnectionString = strCIWConn

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        strSQL = "SELECT * FROM " & serverPrefix & "CSR"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "CSR")

        strSQL = "SELECT * FROM EventSourceMediumCodes"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "EventSourceMediumCodes")

        strSQL = "SELECT MasterCSRID, EventSourceMediumCode, count(ServiceEventNumber) as 'Count' " &
                 "FROM ServiceEventDetail " &
                 "WHERE left(convert(char,EventInitialDateTime,120),10) >= '" & strFromDate & "' and left(convert(char,EventInitialDateTime,120),10) <= '" & strToDate & "' " &
                 "GROUP BY MasterCSRID, EventSourceMediumCode"
        daSrvLog = New SqlDataAdapter(strSQL, sqlConn)
        daSrvLog.Fill(dsSrvLog, "csw_csr_medium_count")

        Try
            rpt.SetDataSource(dsSrvLog)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Sub

    'Campaign_Call_rpt() - Prepare data for Campaign Activity Outbound Call Report
    Public Sub Campaign_Call_rpt()
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL As String
        Dim dsCampCall As New DataSet
        Dim daCampCall As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        blnCancel = True

        frmParam.dtFrom.Value = DateAdd(DateInterval.Month, -1, Today)
        frmParam.dtTo.Value = Today

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        strSQL = "select  crmcmp_campaign_id, crmcmp_campaign_name, crmcpc_channel_id, crmcpc_description, " &
                 "crmcmp_start_date, crmcmp_end_date, crmcpo_owner_desc, crmcps_status_desc, count(*) as call_made, sum(case when crmctk_call_status = 'N' then 1 else 0 end) as success_call " &
                 "from " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_owner, " & serverPrefix & "crm_campaign_status, " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_tracking " &
                 "where crmcmp_campaign_id = crmctk_campaign_id and crmcmp_campaign_id = crmcpc_campaign_id " &
                 "and crmctk_channel_id = crmcpc_channel_id and crmcmp_owner_id = crmcpo_owner_id and crmcmp_status_id = crmcps_status_id " &
                 "and crmctk_channel_id = '05' " &
                 "and crmcmp_start_date >= '" & strFromDate & "' and crmcmp_start_date <= '" & strToDate & "'" &
                 "group by crmcmp_campaign_id, crmcmp_campaign_name, crmcpc_channel_id, crmcpc_description, crmcmp_start_date, crmcmp_end_date, crmcpo_owner_desc, crmcps_status_desc"

        strSQL &= " Select crmctk_campaign_id, crmtcs_status_desc, count(*) as cnt " &
            " From " & serverPrefix & "crm_campaign_tracking p, " & serverPrefix & "crm_campaign_tracking_callstatus, " & serverPrefix & "crm_campaign " &
            " Where crmctk_channel_id = '05' " &
            " And crmcmp_campaign_id = crmctk_campaign_id " &
            " And crmcmp_start_date >= '" & strFromDate & "' And crmcmp_start_date <= '" & strToDate & "' " &
            " And crmctk_call_status = crmtcs_status_id " &
            " And crmctk_call_datetime = " &
            " (Select max(crmctk_call_datetime) from " & serverPrefix & "crm_campaign_tracking " &
            "  Where crmctk_campaign_id = p.crmctk_campaign_id " &
            "  And crmctk_channel_id = p.crmctk_channel_id " &
            "  And crmctk_customer_id = p.crmctk_customer_id " &
            " ) " &
            " Group By crmctk_campaign_id, crmtcs_status_desc "

        daCampCall = New SqlDataAdapter(strSQL, sqlConn)
        daCampCall.TableMappings.Add("crm_campaign", "CampaignCallCount")
        daCampCall.TableMappings.Add("crm_campaign1", "CampaignStatusCount")
        daCampCall.Fill(dsCampCall, "CampaignCallCount")

        'Dim filename As String = "call1.xsd"
        'Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
        'Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        'dsCampCall.WriteXmlSchema(MyXmlTextWriter)
        'MyXmlTextWriter.Close()

        Try
            rpt.SetDataSource(dsCampCall)
            rpt.Subreports("CNT").SetDataSource(dsCampCall)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False
    End Sub

    'Campaign_EDM_rpt() - Prepare data for Campaign Activity Email Report
    Public Sub Campaign_EDM_rpt()

        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL As String
        Dim dsCampEDM As New DataSet
        Dim daCampEDM As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim blnChkBounce As Boolean
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        blnCancel = True

        frmParam.dtFrom.Value = DateAdd(DateInterval.Month, -1, Today)
        frmParam.dtTo.Value = Today
        frmParam.chkBounce.Enabled = True

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            blnChkBounce = frmParam.chkBounce.Checked
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        If blnChkBounce Then checkMailReceipt()

        strSQL = "select  crmcmp_campaign_id, crmcmp_campaign_name, crmcpc_channel_id, crmcpc_description, " &
                 "crmcmp_start_date, crmcmp_end_date, crmcpo_owner_desc, crmcps_status_desc, sum(case when crmcsl_mail_sent = 'Y' then 1 else 0 end) as mail_sent,  sum(case when crmcsl_mail_received = 'Y' then 1 else 0 end) as mail_received " &
                 "from " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_owner, " & serverPrefix & "crm_campaign_status, " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_sales_leads " &
                 "where crmcmp_campaign_id = crmcsl_campaign_id and crmcmp_campaign_id = crmcpc_campaign_id " &
                 "and crmcsl_channel_id = crmcpc_channel_id " &
                 "and crmcmp_owner_id = crmcpo_owner_id and crmcmp_status_id = crmcps_status_id " &
                 "and crmcsl_channel_id = '04' " &
                 "and crmcmp_start_date >= '" & strFromDate & "' and crmcmp_start_date <= '" & strToDate & "' " &
                 "group by crmcmp_campaign_id, crmcmp_campaign_name, crmcpc_channel_id, crmcpc_description, crmcmp_start_date, crmcmp_end_date, crmcpo_owner_desc, crmcps_status_desc "

        daCampEDM = New SqlDataAdapter(strSQL, sqlConn)
        daCampEDM.Fill(dsCampEDM, "CampaignEDMCount")

        Try
            rpt.SetDataSource(dsCampEDM)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False
    End Sub

    Private Sub checkMailReceipt()
        Dim mOutLookApp As Outlook.Application
        Dim mNameSpace As Outlook.NameSpace
        Dim mailcount As Integer

        mOutLookApp = New Outlook.Application
        mNameSpace = mOutLookApp.GetNamespace("MAPI")
        mNameSpace.Logon(, , False, True)

        Dim oInbox As Outlook.MAPIFolder
        Dim oitems As Outlook.Items
        Dim oReport As Outlook.ReportItem

        oInbox = mNameSpace.PickFolder
        oitems = oInbox.Items
        mailcount = oitems.Count

        'oInbox = mNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox)
        'oitems = oInbox.Items
        oitems.Sort("From", True)
        'mailcount = oitems.Count

        'Dim myRecipient As Outlook.Recipient
        'myRecipient = mNameSpace.CreateRecipient("Kim Buhler")
        'myRecipient.Resolve()
        'If myRecipient.Resolved Then
        '    olMAPI.GetSharedDefaultFolder(myRecipient, olFolderCalendar)
        'End If

        Dim i As Integer
        i = 1

        Dim str, findString As String
        Dim findID As String
        Dim inPost, idPost As Integer
        Dim strSub, strCust As String

        findString = "Undeliverable:"
        findID = " (Ref#: "

        While i <= mailcount
            'For i As Integer = 1 To mailcount
            inPost = 0
            idPost = 0

            str = oitems.Item(i).subject
            inPost = InStr(str, findString, CompareMethod.Text)
            idPost = InStr(str, findID, CompareMethod.Text)

            If inPost > 0 AndAlso idPost > 0 Then
                'strSub = str.Substring(inPost + findString.Length, idPost - inPost - findString.Length)
                strCust = str.Substring(idPost + findID.Length - 1)
                strCust = strCust.Substring(0, strCust.Length - 1)

                If updateMailReceived(strSub, strCust) > 0 Then
                    'delete corresponding mail
                    'oitems.Item(i).Delete()
                    mailcount = mailcount - 1
                End If

            End If

            i = i + 1
            'Next
        End While

    End Sub

    'Update the status of mail received flag in database
    Private Function updateMailReceived(ByVal inSubject As String, ByVal inCust As String) As Integer

        Dim sqlconnect As New SqlConnection
        Dim strMap, strExec As String
        'Dim sqlApt As SqlDataAdapter
        Dim sqlCmd As New SqlCommand
        'sqlCmd = New SqlCommand
        'Dim strCamp As String
        'Dim tmpDt As New DataTable
        Dim numUpdate As Integer
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        numUpdate = 0

        'strMap = "select crmcmp_campaign_id from crm_campaign where crmcmp_campaign_name = '" & inSubject & "'"

        'Try
        '    sqlconnect.ConnectionString = strCIWConn
        '    sqlApt = New SqlDataAdapter(strMap, sqlconnect)
        '    sqlApt.Fill(tmpDt)

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'If tmpDt.Rows.Count > 0 Then
        '    strCamp = Trim(tmpDt.Rows(0).Item(0))

        ' ==== Borrow the field for setting bounced mail ====
        strExec = "update " & serverPrefix & "crm_campaign_sales_leads set crmcsl_mail_received = 'Y' " &
                  "where crmcsl_seq = '" & inCust & "' "

        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlCmd.CommandText = strExec
            sqlCmd.Connection = sqlconnect
            If sqlconnect.State <> ConnectionState.Open Then
                sqlconnect.Open()
            End If

            numUpdate = sqlCmd.ExecuteNonQuery()

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try
        'Else
        '    numUpdate = 0
        'End If

        sqlconnect = Nothing
        sqlCmd = Nothing

        Return numUpdate

    End Function

    Public Sub POLE_POLSTS()

        Dim strPolicy, strErrMsg, strCapsilDt, strProdID As String
        Dim dtPolInf As DataTable
        Dim lngErrNo As Long

        Dim connect As New OdbcConnection
        Dim Cmd As OdbcCommand

        Dim strSQL As String

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "POLE - Policy Status Report"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Enabled = False
        frmInput.txtTo.Enabled = False
        frmInput.chkChi.Enabled = False
        frmInput.chkEng.Enabled = False
        frmInput.ShowDialog()

        blnCancel = True
        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        strPolicy = frmInput.PolicyNo

        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Policy Status Report")
            Exit Sub
        End If

        'If strProdID = "" Then
        '    MsgBox("Product Code is blank!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Surrender Report")
        '    Exit Sub
        'End If

        wndMain.Cursor = Cursors.WaitCursor

        '#If UAT <> 0 Then
        '        objCS.Env = giEnv
        '#End If

        '        If Mid(strProdID, 2, 1) = "D" Then
        '            dtPolInf = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
        '        Else
        '            dtPolInf = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
        '        End If

        '        If lngErrNo <> -1 AndAlso Not dtPolInf Is Nothing AndAlso dtPolInf.Rows.Count > 0 Then
        '            strProdID = dtPolInf.Rows(0).Item("ProductID")
        '        Else
        '            MsgBox("Policy " & strPolicy & " not found.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Policy Status Report")
        '            Exit Sub
        '        End If

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end


        If objCS.PrintPolicyStsRpt(strPolicy.PadRight(10, " "), gsUser400, lngErrNo, strErrMsg) Then
            blnCancel = False
            MsgBox("Report printed successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Policy Status Report")
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Policy Status Report")
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    Public Sub POLE_SURR()

        Dim strPolicy, strErrMsg, strCapsilDt, strProdID As String
        Dim lngErrNo As Long
        Dim dtPolInf As DataTable
        Dim connect As New OdbcConnection
        Dim Cmd As OdbcCommand

        Dim strSQL As String

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "POLE - Surrender Report"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Enabled = False
        frmInput.txtTo.Enabled = False
        frmInput.chkChi.Enabled = False
        frmInput.chkEng.Enabled = False
        frmInput.ShowDialog()

        blnCancel = True
        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        strPolicy = frmInput.PolicyNo

        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end


        'dtPolInf = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
        dtPolInf = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
        If lngErrNo <> -1 AndAlso Not dtPolInf Is Nothing AndAlso dtPolInf.Rows.Count > 0 Then
            strProdID = dtPolInf.Rows(0).Item("ProductID")
        Else
            MsgBox("Policy " & strPolicy & " not found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
            Exit Sub
        End If

        If strProdID = "" Then
            MsgBox("Product Code is blank!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
            Exit Sub
        End If

        If Mid(strProdID, 2, 1) = "D" Then
            MsgBox("Cannot Print Surrender Letter for DI Policy.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
            Exit Sub
        End If

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end

        If objCS.PrintSurrenderRpt(strPolicy.PadRight(10, " "), gsUser400, lngErrNo, strErrMsg) Then
            blnCancel = False
            MsgBox("Report printed successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Surrender Report")
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    Public Sub Campaign_NB_Rider_Rpt()
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strCampaign, strChannel, strActivity As String
        Dim dsCampCall As New DataSet
        Dim daCampCall As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        blnCancel = True
        frmParam.LoadCampData = True

        frmParam.dtFrom.Value = DateAdd(DateInterval.Month, -1, Today)
        frmParam.dtTo.Value = Today

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            strCampaign = frmParam.cboCampaign.SelectedValue
            strChannel = frmParam.cboChannel.SelectedValue
            strActivity = frmParam.txtActivity.Text
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        ' +/- Rider
        strSQL = "Select crmcsl_campaign_id, crmcmp_campaign_name, crmcsl_channel_id, crmcpt_channel_desc, " &
            " c.CustomerID, Activity, h.PolicyAccountID, Date, h.Remarks, convert(numeric(12,0), 0) as SI, " &
            " rtrim(c.NamePrefix) + ' ' + rtrim(c.NameSuffix) + ' ' + rtrim(c.FirstName) as PH_Name, " &
            " rtrim(ca.NamePrefix) + ' ' + rtrim(ca.NameSuffix) + ' ' + rtrim(ca.FirstName) as SA_Name, " &
            " a.AgentCode, a.LocationCode, a.UnitCode " &
            " From " & serverPrefix & "crm_campaign_sales_leads, " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel_type, " & serverPrefix & " clienthistory h, customer c, " &
            " csw_poli_rel r, customer ca, agentcodes a " &
            " Where crmcsl_campaign_id = '" & strCampaign & "' " &
            " And crmcsl_campaign_id = crmcmp_campaign_id " &
            " And crmcsl_channel_id = crmcpt_channel_id " &
            " And crmcsl_customer_id = h.CustomerID " &
            " And h.CustomerID = c.CustomerID " &
            " And Activity in (" & strActivity & ") " &
            " And h.PolicyAccountID = r.PolicyAccountID " &
            " And r.PolicyRelateCode = 'SA' " &
            " And r.CustomerID = ca.CustomerID " &
            " And ca.AgentCode = a.AgentCode " &
            " And h.Remarks like '%ADD%' " &
            " And date between '" & strFromDate & "' AND '" & strToDate & "'"

        If strChannel <> "ALL" Then
            strSQL &= " And crmcsl_channel_id = '" & strChannel & "' "
        End If

        ' New Business
        strSQL &= " UNION ALL " &
            " Select crmcsl_campaign_id, crmcmp_campaign_name, crmcsl_channel_id, crmcpt_channel_desc, " &
            " r.CustomerID, 'NB', P.policyaccountid, CommencementDate, pd.Description, p.SumInsured, " &
            " rtrim(c.NamePrefix) + ' ' + rtrim(c.NameSuffix) + ' ' + rtrim(c.FirstName) as PH_Name, " &
            " rtrim(ca.NamePrefix) + ' ' + rtrim(ca.NameSuffix) + ' ' + rtrim(ca.FirstName) as SA_Name, " &
            " a.AgentCode, a.LocationCode, a.UnitCode " &
            " From " & serverPrefix & "crm_campaign_sales_leads, " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel_type, policyaccount p, csw_poli_rel r, product pd, customer c, " &
            " csw_poli_rel ra, customer ca, agentcodes a " &
            " Where crmcsl_campaign_id = '" & strCampaign & "' " &
            " And crmcsl_campaign_id = crmcmp_campaign_id " &
            " And crmcsl_channel_id = crmcpt_channel_id " &
            " And crmcsl_customer_id = r.customerid " &
            " And r.CustomerID = c.CustomerID " &
            " And r.policyrelatecode = 'PH' " &
            " And r.policyaccountid = p.policyaccountid " &
            " And p.productid = pd.productid " &
            " And p.PolicyAccountID = ra.PolicyAccountID " &
            " And ra.PolicyRelateCode = 'SA' " &
            " And ra.CustomerID = ca.CustomerID " &
            " And ca.AgentCode = a.AgentCode " &
            " And p.CommencementDate between '" & strFromDate & "' AND '" & strToDate & "'"

        If strChannel <> "ALL" Then
            strSQL &= " And crmcsl_channel_id = '" & strChannel & "' "
        End If

        Try
            daCampCall = New SqlDataAdapter(strSQL, sqlConn)
            daCampCall.Fill(dsCampCall, "NBR")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'Dim filename As String = "nbr.xsd"
        'Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
        'Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        'dsCampCall.WriteXmlSchema(MyXmlTextWriter)
        'MyXmlTextWriter.Close()

        Try
            rpt.SetDataSource(dsCampCall)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False
    End Sub

    Public Sub ParentsClub_rpt()

        Dim frmParam As New frmRptMiscType
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strActivity As String
        Dim blnAll As Boolean
        Dim dsMisc As New DataSet
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            'strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            'strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            strFromDate = Format(Today, "yyyy-MM-dd")
            strToDate = Format(Today, "yyyy-MM-dd")
            strActivity = frmParam.txtAct.Text
            'blnAll = frmParam.chkALL.Checked
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        strSQL =
            " Select CustomerID, rtrim(c.NamePrefix) + ' ' + rtrim(c.NameSuffix) + ' ' + rtrim(c.FirstName) as Name, " &
            "        SUM(case when cswmif_status = 'E' then 1 else 0 end) as Enrolled, " &
            "        SUM(case when cswmif_status = 'W' then 1 else 0 end) as Withdrew, " &
            "        SUM(case when cswmif_status = 'P' then 1 else 0 end) as Participated " &
            " From " & serverPrefix & "csw_misc_info, customer c " &
            " Where cswmif_customer_id = CustomerID "

        If InStr(strActivity, "%") > 0 Then
            strSQL &= " And cswmif_type LIKE '" & strActivity & "'"
        Else
            strSQL &= " And cswmif_type IN (" & strActivity & ") "
        End If

        'If blnAll = False Then
        '    strSQL &= " And cswmif_start_date BETWEEN '" & strFromDate & "' AND '" & strToDate & "'"
        'End If

        strSQL &= " Group By CustomerID, rtrim(c.NamePrefix) + ' ' + rtrim(c.NameSuffix) + ' ' + rtrim(c.FirstName)"

        Try
            sqlda = New SqlDataAdapter(strSQL, sqlConn)
            sqlda.Fill(dsMisc, "ParentsClub")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'Dim filename As String = "misc.xsd"
        'Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
        'Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        'dsMisc.WriteXmlSchema(MyXmlTextWriter)
        'MyXmlTextWriter.Close()

        Try
            rpt.SetDataSource(dsMisc)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Sub
    'For LifeAsia's Connection
    Private Function ConnectDB(ByRef obj As Object, ByVal strProject As String, ByVal strConnAlias As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        Try

            If Not obj Is Nothing Then
                obj = New DBLogon_NET.DBLogon.DBlogonNet
                obj.Project = strProject
                obj.ConnectionAlias = strConnAlias 'strComp + strProject + strEnv
                obj.User = strUser
                If obj.Connect() Then
                    Return True
                Else
                    strErr = obj.RecentErrorMessage
                    Return False
                End If
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    ' **** ES005 begin ****
    Public Sub PremCallRpt()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then

            If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
                If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            'Sky 20180109 add isPremHoliday column
            'strSQL = "Select p.policyaccountid, p.productid, description, p.paidtodate, p.mode, p.aplindicator, p.modalpremium, p.posamt, p.billingtype, cph.nameprefix, cph.namesuffix, cph.firstname, " & _
            '    " cph.phonemobile, cph.phonepager, cswpad_tel1, cswpad_tel2, cswpad_add1, cswpad_add2, cswpad_add3, cswpad_city, " & _
            '    " a.unitcode, a.agentcode, a.locationcode, csa.nameprefix, csa.namesuffix, csa.firstname " & _
            '    " from policyaccount p, csw_poli_rel rph, customer cph, csw_poli_rel rsa, customer csa, agentcodes a, csw_policy_address pa, product pd " & _
            '    " where p.policyaccountid = rph.policyaccountid " & _
            '    " and p.productid = pd.productid " & _
            '    " and rph.policyrelatecode = 'PH' " & _
            '    " and rph.customerid = cph.customerid " & _
            '    " and p.policyaccountid = rsa.policyaccountid " & _
            '    " and rsa.policyrelatecode = 'SA' " & _
            '    " and rSA.customerid = csa.customerid " & _
            '    " and csa.agentcode = a.agentcode " & _
            '    " and p.policyaccountid = cswpad_poli_id " & _
            '    " and (p.accountstatuscode in ('1') or p.aplindicator = 'Y') " & _
            '    " and p.companyid in ('EAA','ING') " & _
            '    " and p.paidtodate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' "
            strSQL = "Select p.policyaccountid, p.productid, description, p.paidtodate, p.mode, p.aplindicator, p.modalpremium, p.posamt, p.billingtype, cph.nameprefix, cph.namesuffix, cph.firstname, " &
                " cph.phonemobile, cph.phonepager, cswpad_tel1, cswpad_tel2, cswpad_add1, cswpad_add2, cswpad_add3, cswpad_city, " &
                " a.unitcode, a.agentcode, a.locationcode, csa.nameprefix, csa.namesuffix, csa.firstname , case when p.premiumstatus = 'PH' then 'Y' else 'N' end as [Is Prem. Holiday] " &
                " from policyaccount p, csw_poli_rel rph, customer cph, csw_poli_rel rsa, customer csa, agentcodes a, csw_policy_address pa, product pd " &
                " where p.policyaccountid = rph.policyaccountid " &
                " and p.productid = pd.productid " &
                " and rph.policyrelatecode = 'PH' " &
                " and rph.customerid = cph.customerid " &
                " and p.policyaccountid = rsa.policyaccountid " &
                " and rsa.policyrelatecode = 'SA' " &
                " and rSA.customerid = csa.customerid " &
                " and csa.agentcode = a.agentcode " &
                " and p.policyaccountid = cswpad_poli_id " &
                " and (p.accountstatuscode in ('1') or p.aplindicator = 'Y') " &
                " and p.companyid in ('EAA','ING') " &
                " and p.paidtodate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' "
            'Sky 20180109  end

            ' For specific bank channel
            If frmParam.CheckBox1.Checked Then
                strSQL &= " and a.locationcode in (" & Trim(frmParam.TextBox1.Text) & ") "
            End If

            strSQL &= " order by p.paidtodate, a.unitcode, a.agentcode "

            wndMain.Cursor = Cursors.WaitCursor

            If GetDT(strSQL, strCIWConn, dtResult, strError) Then
                If frmParam.CheckBox1.Checked Then
                    If Not ExportCSV(frmParam.txtPath.Text & "\PremCallRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                    End If
                Else
                    ' All
                    If Not ExportCSV(frmParam.txtPath.Text & "\PremCallRpt_all_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                    End If
                    ' Agency only
                    dtResult.DefaultView.RowFilter = "unitcode >= '00800' and unitcode <= '69999'"
                    If Not ExportCSV(frmParam.txtPath.Text & "\PremCallRpt_agency_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                    End If

                End If

                MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

            Else
                MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If

    End Sub
    ' **** ES005 end ****

    Public Function HKFI_Report()

        Dim strSQL, strError, strType As String
        Dim dtResult As DataTable
        Dim frmParam As New frmHKFI_Para

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then

            If Format(frmParam.dtP1From.Value, "MM/dd/yyyy") > Format(frmParam.dtP1To.Value, "MM/dd/yyyy") Then
                MsgBox("Invalid date range!", MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                wndMain.Cursor = Cursors.Default
                Exit Function
            End If

            If frmParam.rdoUL.Checked = True Then

                strType = "UL_"

                strSQL = "Select DISTINCT 'UL' as RptType, cswpof_Policy as Policy_No, description as Plan_Name, RTRIM(cswpof_Value) as Risk_Level, exhibitinforcedate as Inforce_Date, " &
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " &
                    "   where cswpof_policy = f.cswpof_policy " &
                    "   and cswpof_form_code = 'FNA' " &
                    "   and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " &
                    " c.customerid, "
                strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " &
                    " where r1.policyaccountid = p1.policyaccountid " &
                    " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" &
                    " and r1.policyrelatecode = 'PH' " &
                    " and r1.customerid = c.customerid " &
                    " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p1.accountstatuscode not in ('6','7','8','9','0') " &
                    " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                strSQL &= "c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " &
                    " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location , pr.PolicyReplacementInd AS Policy_Replacement, prd.NameOfInsurer AS Internal_Replacement, prd.CompanyName AS Company_Name " &
                    " From csw_policy_form f, product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " &
                    " ON p.policyaccountid = cswpuw_poli_id " &
                    " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " &
                    " LEFT JOIN " & gcNBSDB & "policy_replacement_detail prd on prd.PolicyNumber = p.PolicyAccountID" & 'Welcome Call List'
                    " LEFT JOIN " & gcNBSDB & "policy_replacement pr on prd.PolicyReplacementId = pr.ID" &
                    " where cswpof_Form_Code = 'FNA' " &
                    " and cswpof_Item_name = 'SUITABILITY' " &
                    " and RTRIM(cswpof_Value) in ('A','B','C') " &
                    " and cswpof_policy = p.policyaccountid " &
                    " and p.productid = pd.productid " &
                    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and cswpof_policy = r.policyaccountid " &
                    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " &
                    " and r.policyaccountid = r1.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " &
                    " and c1.agentcode = a.agentcode " &
                    " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " &
                    " and accountstatuscode in ('1','2','3','4','V') " &
                    " and p.companyid in ('ING','EAA') "
            Else

                strType = "TL_"

                'strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, space(1) as Risk_Level, exhibitinforcedate as Inforce_Date, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, " & _
                '    " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, c1.nameprefix as sa_nameprefix, c1.namesuffix as sa_namesuffix, c1.firstname as sa_firstname, a.locationcode, space(1) as Vulnerability " & _
                '    " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
                '    " ON p.policyaccountid = cswpuw_poli_id " & _
                '    " Where p.productid = pd.productid " & _
                '    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
                '    " and p.policyaccountid = r.policyaccountid " & _
                '    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
                '    " and r.policyaccountid = r1.policyaccountid " & _
                '    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
                '    " and c1.agentcode = a.agentcode " & _
                '    " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " & _
                '    " and accountstatuscode in ('1','2','3','4','V') " & _
                '    " and p.companyid in ('ING','EAA') "

                strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, RTRIM(ISNULL(cswpof_Value,'')) as Risk_Level, exhibitinforcedate as Inforce_Date, " &
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " &
                    " where cswpof_policy = p.policyaccountid " &
                    " and cswpof_form_code = 'FNA' " &
                    " and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " &
                    " c.customerid, "
                strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " &
                    " where(r1.policyaccountid = p1.policyaccountid) " &
                    " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" &
                    " and r1.policyrelatecode = 'PH' " &
                    " and r1.customerid = c.customerid " &
                    " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p1.accountstatuscode not in ('6','7','8','9','0') " &
                    " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                strSQL &= " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " &
                    " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location , pr.PolicyReplacementInd AS Policy_Replacement, prd.NameOfInsurer AS Internal_Replacement, prd.CompanyName AS Company_Name " &
                    " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " &
                    " ON p.policyaccountid = cswpuw_poli_id " &
                    " LEFT JOIN csw_policy_form " &
                    " LEFT JOIN " & gcNBSDB & "policy_replacement_detail prd on prd.PolicyNumber = p.PolicyAccountID" & 'Welcome Call List'
                    " LEFT JOIN " & gcNBSDB & "policy_replacement pr on prd.PolicyReplacementId = pr.ID" &
                    " ON p.policyaccountid = cswpof_policy " &
                    " and cswpof_Form_Code = 'FNA' " &
                    " and cswpof_Item_name = 'SUITABILITY' " &
                    " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " &
                    " Where(cv.productid = pd.productid) " &
                    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p.policyaccountid = r.policyaccountid " &
                    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " &
                    " and r.policyaccountid = r1.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " &
                    " and c1.agentcode = a.agentcode " &
                    " and cv.policyaccountid = p.policyaccountid " &
                    " and (cv.trailer = 1 or (cv.productid like '_RFUR1' and cv.coveragestatus in ('1','2','3','4','V'))) " &
                    " and accountstatuscode in ('1','2','3','4','V') " &
                    " and p.companyid in ('ING','EAA') "
            End If

            If frmParam.rdoCODay.Checked Then
                strSQL = strSQL & " order by cswpuw_flook_dline "
            End If

            If frmParam.rdoRL.Checked Then
                'strSQL = strSQL & " order by Class "
            End If

            wndMain.Cursor = Cursors.WaitCursor

            If GetDT(strSQL, strCIWConn, dtResult, strError) Then
                AddReplacementStatus(dtResult)
                If Not ExportCSV(frmParam.txtPath.Text & "\PostSaleCall_" & strType & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                End If
                MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            Else
                MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Function
        End If

    End Function
    Public Function HKFI_Report_MCU()

        Dim strSQL, strError, strType As String
        Dim dtResult As DataTable
        Dim frmParam As New frmHKFI_Para

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then

            If Format(frmParam.dtP1From.Value, "MM/dd/yyyy") > Format(frmParam.dtP1To.Value, "MM/dd/yyyy") Then
                MsgBox("Invalid date range!", MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                wndMain.Cursor = Cursors.Default
                Exit Function
            End If

            If frmParam.rdoUL.Checked = True Then

                strType = "UL_"

                strSQL = "Select DISTINCT 'UL' as RptType, cswpof_Policy as Policy_No, description as Plan_Name, RTRIM(cswpof_Value) as Risk_Level, exhibitinforcedate as Inforce_Date, " &
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " &
                    "   where cswpof_policy = f.cswpof_policy " &
                    "   and cswpof_form_code = 'FNA' " &
                    "   and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " &
                    " c.customerid, "
                strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " &
                    " where r1.policyaccountid = p1.policyaccountid " &
                    " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" &
                    " and r1.policyrelatecode = 'PH' " &
                    " and r1.customerid = c.customerid " &
                    " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p1.accountstatuscode not in ('6','7','8','9','0') " &
                    " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                strSQL &= "c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " &
                    " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location,  pr.PolicyReplacementInd AS Policy_Replacement, prd.NameOfInsurer AS Internal_Replacement, prd.CompanyName AS Internal_Replacement " & 'Welcome Call'
                    " From csw_policy_form f, product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " &
                    " ON p.policyaccountid = cswpuw_poli_id " &
                    " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " &
                    " LEFT JOIN " & gcNBSDBMcu & "policy_replacement_detail prd on prd.PolicyNumber = p.PolicyAccountID" &
                    " LEFT JOIN " & gcNBSDBMcu & "policy_replacement pr on prd.PolicyReplacementId = pr.ID" &
                    " where cswpof_Form_Code = 'FNA' " &
                    " and cswpof_Item_name = 'SUITABILITY' " &
                    " and RTRIM(cswpof_Value) in ('A','B','C') " &
                    " and cswpof_policy = p.policyaccountid " &
                    " and p.productid = pd.productid " &
                    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and cswpof_policy = r.policyaccountid " &
                    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " &
                    " and r.policyaccountid = r1.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " &
                    " and c1.agentcode = a.agentcode " &
                    " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " &
                    " and accountstatuscode in ('1','2','3','4','V') " &
                    " and p.companyid in ('ING','EAA') "
            Else

                strType = "TL_"

                'strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, space(1) as Risk_Level, exhibitinforcedate as Inforce_Date, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, " & _
                '    " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, c1.nameprefix as sa_nameprefix, c1.namesuffix as sa_namesuffix, c1.firstname as sa_firstname, a.locationcode, space(1) as Vulnerability " & _
                '    " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
                '    " ON p.policyaccountid = cswpuw_poli_id " & _
                '    " Where p.productid = pd.productid " & _
                '    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
                '    " and p.policyaccountid = r.policyaccountid " & _
                '    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
                '    " and r.policyaccountid = r1.policyaccountid " & _
                '    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
                '    " and c1.agentcode = a.agentcode " & _
                '    " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " & _
                '    " and accountstatuscode in ('1','2','3','4','V') " & _
                '    " and p.companyid in ('ING','EAA') "

                strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, RTRIM(ISNULL(cswpof_Value,'')) as Risk_Level, exhibitinforcedate as Inforce_Date, " &
                    " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " &
                    " where cswpof_policy = p.policyaccountid " &
                    " and cswpof_form_code = 'FNA' " &
                    " and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " &
                    " c.customerid, "
                strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " &
                    " where(r1.policyaccountid = p1.policyaccountid) " &
                    " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" &
                    " and r1.policyrelatecode = 'PH' " &
                    " and r1.customerid = c.customerid " &
                    " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p1.accountstatuscode not in ('6','7','8','9','0') " &
                    " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
                strSQL &= " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " &
                    " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location ,  pr.PolicyReplacementInd AS Policy_Replacement, prd.NameOfInsurer AS Internal_Replacement, prd.CompanyName AS Internal_Replacement " &
                    " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " &
                    " ON p.policyaccountid = cswpuw_poli_id " &
                    " LEFT JOIN csw_policy_form " &
                    " ON p.policyaccountid = cswpof_policy " &
                    " and cswpof_Form_Code = 'FNA' " &
                    " and cswpof_Item_name = 'SUITABILITY' " &
                    " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " & 'Welcome Call List'
                    " LEFT JOIN " & gcNBSDBMcu & "policy_replacement_detail prd on prd.PolicyNumber = p.PolicyAccountID" &
                    " LEFT JOIN " & gcNBSDBMcu & "policy_replacement pr on prd.PolicyReplacementId = pr.ID" &
                    " Where(cv.productid = pd.productid) " &
                    " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " &
                    " and p.policyaccountid = r.policyaccountid " &
                    " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " &
                    " and r.policyaccountid = r1.policyaccountid " &
                    " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " &
                    " and c1.agentcode = a.agentcode " &
                    " and cv.policyaccountid = p.policyaccountid " &
                    " and (cv.trailer = 1 or (cv.productid like '_RFUR1' and cv.coveragestatus in ('1','2','3','4','V'))) " &
                    " and accountstatuscode in ('1','2','3','4','V') " &
                    " and p.companyid in ('ING','EAA') "
            End If

            If frmParam.rdoCODay.Checked Then
                strSQL = strSQL & " order by cswpuw_flook_dline "
            End If

            If frmParam.rdoRL.Checked Then
                'strSQL = strSQL & " order by Class "
            End If

            wndMain.Cursor = Cursors.WaitCursor

            If GetDT(strSQL, strCIWMcuConn, dtResult, strError) Then
                AddReplacementStatus(dtResult)
                If Not ExportCSV(frmParam.txtPath.Text & "\PostSaleCall_" & strType & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                End If
                MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            Else
                MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Function
        End If

    End Function
    Public Function AddReplacementStatus(ByRef dtResult As DataTable) As DataTable
        Try
            For Each row As DataRow In dtResult.Rows
                Dim strErr = ""
                If row("Internal_Replacement") IsNot DBNull.Value Then
                    If (row("Internal_Replacement").ToString() = "FWD") Then
                        row("Internal_Replacement") = "Internal"
                    Else
                        If row("Company_Name") IsNot DBNull.Value Then
                            If (row("Company_Name").ToString().Contains("FWD") Or
                                row("Company_Name").ToString().Contains("ING") Or
                                row("Company_Name").ToString().Contains("Metlife") Or
                                row("Company_Name").ToString().Contains("Assurance") Or
                                row("Company_Name").ToString().Contains("富衞")
                                ) Then
                                row("Internal_Replacement") = "Internal"
                            Else
                                row("Internal_Replacement") = "External"
                            End If
                        End If
                    End If
                End If
                If row("Policy_Replacement") IsNot DBNull.Value Then
                    Select Case row("Policy_Replacement").ToString()
                        Case "N"
                            row("Policy_Replacement") = "No"
                            row("Internal_Replacement") = ""
                        Case "ND"
                            row("Policy_Replacement") = "Not Determined"
                        Case "Y"
                            row("Policy_Replacement") = "Yes"
                    End Select
                End If
            Next
        Catch ex As Exception
        End Try
        Return dtResult
    End Function
    Public Function CTI_CRS_REPORT()

        Dim frmParam As New frmPAYHRpt
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strActivity As String
        Dim blnAll As Boolean
        Dim dsMisc As New DataSet
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        frmParam.Text = "CTI-CRS Statistic Report"
        frmParam.Label1.Visible = False
        frmParam.txtPolicy.Visible = False
        frmParam.txtFrom.Enabled = True
        frmParam.txtTo.Enabled = True
        frmParam.chkChi.Visible = False
        frmParam.chkEng.Visible = False

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.txtFrom.Value, "yyyyMMdd")
            strToDate = Format(frmParam.txtTo.Value, "yyyyMMdd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        strSQL = "Select COUNT(*) as TTL_Cnt, " &
            " SUM(case when cswwss_done = 'Y' then 1 else 0 end) as Success, SUM(case when cswwss_done = 'N' then 1 else 0 end) as Fail " &
            " From " & serverPrefix & "csw_wml_site_stat " &
            " Where cswwss_cid = 'CTICRS' " &
            " And convert(char,cswwss_datetime,112) between '" & strFromDate & "' and '" & strToDate & "'"

        Try
            sqlda = New SqlDataAdapter(strSQL, sqlConn)
            sqlda.Fill(dsMisc, "CTICRS")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Try
            rpt.SetDataSource(dsMisc)
            rpt.SetParameterValue("FromDate", Format(frmParam.txtFrom.Value, "dd-MMM-yyyy"))
            rpt.SetParameterValue("ToDate", Format(frmParam.txtTo.Value, "dd-MMM-yyyy"))
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Function

    Public Function ACC_REPORT()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt
        Dim strFromDate As String
        Dim strToDate As String

        blnCancel = True

        frmParam.CheckBox1.Visible = False
        frmParam.TextBox1.Visible = False
        'frmParam.Text = "Agent Call to Customer Hotline Report"
        frmParam.Text = "Suggestions/Grievances Report"
        frmParam.txtPath.Text = "I:\Functional Log"

        frmParam.dtP1From.Format = DateTimePickerFormat.Custom
        frmParam.dtP1From.CustomFormat = "MM/dd/yyyy HH:mm:ss"
        frmParam.dtP1To.Format = DateTimePickerFormat.Custom
        frmParam.dtP1To.CustomFormat = "MM/dd/yyyy HH:mm:ss"

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtP1From.Value, "MM/dd/yyyy HH:mm:ss")
            strToDate = Format(frmParam.dtP1To.Value, "MM/dd/yyyy HH:mm:ss")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
            If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Function
            End If
        End If

        strSQL = "Select EventInitialDateTime, cu.namesuffix + ' ' + cu.firstname as SA_Name, a.locationcode, a.unitcode, a.agentcode, " &
            " s.PolicyAccountID, cph.namesuffix + ' ' + cph.firstname as PH_Name, cswecc_desc, eventtypedesc, csw_event_typedtl_desc " &
            " From serviceeventdetail s, " & gcPOS & "vw_csw_event_category_code  c, ServiceEventTypeCodes t, " & gcPOS & "vw_csw_event_typedtl_code d, " &
            " csw_poli_rel r, customer cu, agentcodes a, customer cph " &
            " Where EventInitialDateTime between '" & strFromDate & "' and '" & strToDate & "'" &
            " and s.EventCategoryCode = c.cswecc_code" &
            " and s.EventCategoryCode = t.eventcategorycode and s.EventTypeCode = t.eventtypecode" &
            " and s.EventCategoryCode = d.csw_event_category_code and s.EventTypeCode = d.csw_event_type_code and s.EventTypeDetailcode = d.csw_event_typedtl_code" &
            " and s.policyaccountid = r.policyaccountid" &
            " and r.policyrelatecode = 'SA'" &
            " and r.customerid = cu.customerid" &
            " and cu.agentcode = a.agentcode" &
            " and s.customerid = cph.customerid" &
            " and caseno = 'Y' " &
            " order by EventInitialDateTime"

        wndMain.Cursor = Cursors.WaitCursor

        If GetDT(strSQL, strCIWConn, dtResult, strError) Then
            If Not ExportCSV(frmParam.txtPath.Text & "\ACCRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
            End If

            MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

        Else
            MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
        End If

        frmParam.Dispose()
        wndMain.Cursor = Cursors.Default

        'Try
        '    sqlda = New SqlDataAdapter(strSQL, sqlConn)
        '    sqlda.Fill(dsMisc, "ACC")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'Try
        '    rpt.SetDataSource(dsMisc)
        '    rpt.SetParameterValue("FromDate", Format(frmParam.txtFrom.Value, "MM/dd/yyyy HH:mm:ss"))
        '    rpt.SetParameterValue("ToDate", Format(frmParam.txtTo.Value, "MM/dd/yyyy HH:mm:ss"))
        'Catch e As Exception
        '    MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        'End Try

        'blnCancel = False

    End Function

    Public Sub FCR_Report()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt

        blnCancel = True

        frmParam.CheckBox1.Visible = False
        frmParam.TextBox1.Visible = False
        frmParam.Text = "FCR Report"
        frmParam.txtPath.Text = "I:\Functional Log"

        If frmParam.ShowDialog() = DialogResult.OK Then

            If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
                If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            strSQL = "Select eventinitialdatetime, s.policyaccountid, " &
                " c.nameprefix, c.namesuffix, c.firstname, c.phonemobile, c.phonepager, " &
                " c1.nameprefix, c1.namesuffix, c1.firstname, c1.phonemobile, c1.phonepager, " &
                " eventsourceinitiator, mastercsrid, eventcloseoutcode " &
                " From serviceeventdetail s, csw_poli_rel r, customer c, customer c1, EventSourceInitiatorCodes e " &
                " Where eventsourcemediumcode = 'PC' " &
                " and eventinitialdatetime >= '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and eventinitialdatetime < '" & Format(DateAdd(DateInterval.Day, 1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' " &
                " and s.policyaccountid = r.policyaccountid " &
                " and r.policyrelatecode = 'SA' " &
                " and r.customerid = c.customerid " &
                " and s.customerid = c1.customerid" &
                " and s.EventSourceInitiatorCode = e.EventSourceInitiatorCode" &
                " order by eventinitialdatetime"

            wndMain.Cursor = Cursors.WaitCursor

            If GetDT(strSQL, strCIWConn, dtResult, strError) Then
                If Not ExportCSV(frmParam.txtPath.Text & "\FCRRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                End If

                MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

            Else
                MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If

    End Sub


    '20171103 Levy
    Public Function LevyLetterDetail()

        Dim frmParam As New frmPAYHRpt
        Dim strFromDate As String
        Dim strToDate As String
        Dim strPolicy As String
        Dim dsMisc As New DataSet

        'oliver 2024-5-3 commented for Table_Relocate_Sprint13
        'Dim strSQL, strActivity, strPolicy As String
        'Dim blnAll As Boolean
        'Dim sqlda As SqlDataAdapter
        'Dim sqlConn As New SqlConnection
        'Dim strCIWConn1 As String

        'AC - Change to use configuration setting - start
        '#If UAT = 1 Then
        '        strCIWConn1 = strCIWConn
        '#Else
        '        strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        '#End If
        'strCIWConn1 = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "HKReportConnection").DecryptString()
        'If gUAT Then
        '    strCIWConn1 = strCIWConn
        'Else
        '    strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        'End If
        'AC - Change to use configuration setting - end


        'sqlConn.ConnectionString = strCIWConn1

        blnCancel = True
        frmParam.Text = "Levy Letter"
        frmParam.txtPolicy.Text = strLastPolicy
        frmParam.txtFrom.Enabled = True
        frmParam.txtTo.Enabled = True
        frmParam.chkChi.Enabled = True
        frmParam.chkEng.Enabled = True

        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            strFromDate = Format(frmParam.txtFrom.Value, "yyyyMMdd")
            strToDate = Format(frmParam.txtTo.Value, "yyyyMMdd")

            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        'oliver 2024-5-3 commented for Table_Relocate_Sprint13
        '' Policy Info
        'strSQL = "Select * from DDA where policyaccountid = '" & strPolicy & "'; "
        'strSQL &= "Select * from CCDR where policyaccountid = '" & strPolicy & "'; "
        'strSQL &= "Select bt.* from PolicyAccount P left join BillingTypeCodes bt " &
        '    " ON P.billingtype = billingtypecode " &
        '    " Where policyaccountid = '" & strPolicy & "'; "

        'strSQL &= "Select ac.* from PolicyAccount P left join AccountStatusCodes ac " &
        '    " ON p.accountstatuscode = ac.accountstatuscode " &
        '    " Where policyaccountid = '" & strPolicy & "'; "

        'strSQL &= "Select m.* from PolicyAccount P left join ModeCodes m " &
        '    " ON p.mode = m.modecode " &
        '    " Where policyaccountid = '" & strPolicy & "'; "

        'strSQL &= "Select ds.* from DDA d left join DDAStatusCodes ds " &
        '    " ON d.ddastatus = ds.ddastatuscode " &
        '    " Where policyaccountid = '" & strPolicy & "'; "

        'strSQL &= "Select cs.* from CCDR c left join CCDRStatusCodes cs " &
        '    " ON c.ccdrstatus = cs.ccdrstatuscode " &
        '    " Where policyaccountid = '" & strPolicy & "'; "

        'strSQL &= "Select * from PolicyAccount Where PolicyAccountid = '" & strPolicy & "'"

        '' Policy Value
        'strSQL &= "Select * from csw_policy_value where cswval_TPOLID = 'xxx'"

        'strSQL &= "Select PhoneNumber, NameSuffix + ' ' + FirstName as AgName from agentcodes a, csw_poli_rel r, customer c " &
        '    " Where r.policyaccountid = '" & strPolicy & "'" &
        '    " and r.customerid = c.customerid " &
        '    " and r.policyrelatecode = 'SA' " &
        '    " and c.agentcode = a.agentcode "

        'strSQL &= "SELECT ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
        '    "FROM (select * from csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
        '    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
        '    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
        '    ", (select * from csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        'strSQL &= "Select p.companyid, pt.productid, ProductType, ProductPolValueFunc, PrintValueReport" &
        '    " From product_type pt Inner Join policyaccount p ON p.productid = pt.productid" &
        '    " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

        'strSQL &= "Select c.* from policyaccount P left join couponoptioncodes C " &
        '    " ON p.couponoption = c.couponoptioncode " &
        '    " Where policyaccountid = '" & strPolicy & "'"

        'strSQL &= "Select c.* from policyaccount P left join dividendoptioncodes C " &
        '    " ON p.dividendoption = c.dividendoptioncode " &
        '    " Where policyaccountid = '" & strPolicy & "'"

        'strSQL &= "Select pt.productid, pt.ChineseDescription" &
        '    " From " & gcNBSDB & "product_chi pt Inner Join policyaccount p ON p.productid = pt.productid" &
        '    " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"


        'strSQL &= " select CONVERT(char(10), CONVERT(datetime, CAST(BTDATE as CHAR(8)),101), 126) as BillingDate,    " & _
        '" Convert(varchar(25), CAST(ORIGAMT as money), 1) as PremiumAmount,  " & _
        '" ZORIGLVY as BilledLevyAmount ,  " & _
        '" ZLVYSTL as SettledLevyAmount , " & _
        '" ZLVYOS as OutstandingLevyAmount,  " & _
        '" 'N' as Authority " & _
        '  " from levy_billing_record as LevyHistory" & _
        '  " where POLICYNUM =  '" & Replace(strPolicy, "'", "''") & "'" & _
        '  " AND BTDATE between '" & strFromDate & "' and '" & strToDate & "'" & _
        '  " AND VALIDFLAG = 1 "

        'Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        'If frmParam.chkEng.Checked = True Then
        '    strSQL &= " select CONVERT(char(10), CONVERT(datetime, CAST(DTEEFF as CHAR(8)),101), 126) as BillingDate,    " &
        '                       " Convert(varchar(25), CAST(ORIGAMT as money), 1) as PremiumAmount, " &
        '                       " ZORIGLVY as BilledLevyAmount ,  " &
        '                       " ISNULL( " &
        '                       " 	CONVERT(char(10), CONVERT(datetime, CAST(  " &
        '                       " 		(select MAX(TRANDATE) from levy_settlement where 1=1 " &
        '                       " 		and POLICYNUM = '" & Replace(strPolicy, "'", "''") & "' " &
        '                       " 		and exists( " &
        '                       " 		select seqNo from levy_billing_record where levy_billing_record.POLICYNUM = '" & Replace(strPolicy, "'", "''") & "' " &
        '                       " 		and levy_billing_record.TRANNO = LevyHistory.TRANNO  and levy_billing_record.SEQNO = levy_settlement.SEQNO group by seqNo) " &
        '                       " 		) " &
        '                       " 	as CHAR(8)),101), 126) " &
        '                       " 	,'') " &
        '                       " as SettledDate , " &
        '                       " ZLVYSTL as SettledLevyAmount , " &
        '                       " ZLVYOS as OutstandingLevyAmount, " &
        '                       " (Select EngDescription from " & serverPrefix & "levy_desc_mapping where RTRIM(LTRIM(LevyHistory.FUPREMK)) = RTRIM(LTRIM(TransactionDescription))) as Remarks, " &
        '                       " 'N' as Authority " &
        '            " from levy_billing_record as LevyHistory" &
        '            " where POLICYNUM =  '" & Replace(strPolicy, "'", "''") & "'" &
        '            " AND BTDATE between '" & strFromDate & "' and '" & strToDate & "'" &
        '            " AND VALIDFLAG = 1 "
        'Else
        '    strSQL &= " select CONVERT(char(10), CONVERT(datetime, CAST(DTEEFF as CHAR(8)),101), 126) as BillingDate,    " &
        '                                   " Convert(varchar(25), CAST(ORIGAMT as money), 1) as PremiumAmount, " &
        '                                   " ZORIGLVY as BilledLevyAmount ,  " &
        '                                   " ISNULL( " &
        '                                   " 	CONVERT(char(10), CONVERT(datetime, CAST(  " &
        '                                   " 		(select MAX(TRANDATE) from levy_settlement where 1=1 " &
        '                                   " 		and POLICYNUM = '" & Replace(strPolicy, "'", "''") & "' " &
        '                                   " 		and exists( " &
        '                                   " 		select seqNo from levy_billing_record where levy_billing_record.POLICYNUM = '" & Replace(strPolicy, "'", "''") & "' " &
        '                                   " 		and levy_billing_record.TRANNO = LevyHistory.TRANNO  and levy_billing_record.SEQNO = levy_settlement.SEQNO group by seqNo) " &
        '                                   " 		) " &
        '                                   " 	as CHAR(8)),101), 126) " &
        '                                   " 	,'') " &
        '                                   " as SettledDate , " &
        '                                   " ZLVYSTL as SettledLevyAmount , " &
        '                                   " ZLVYOS as OutstandingLevyAmount, " &
        '                                   " (Select ChiDescription from " & serverPrefix & "levy_desc_mapping where RTRIM(LTRIM(LevyHistory.FUPREMK)) = RTRIM(LTRIM(TransactionDescription))) as Remarks, " &
        '                                   " 'N' as Authority " &
        '                        " from levy_billing_record as LevyHistory" &
        '                        " where POLICYNUM =  '" & Replace(strPolicy, "'", "''") & "'" &
        '                        " AND BTDATE between '" & strFromDate & "' and '" & strToDate & "'" &
        '                        " AND VALIDFLAG = 1 "
        'End If



        'strSQL &= "Select cph.nameprefix as ph_nameprefix, cph.firstname as ph_firstname, cph.namesuffix as ph_namesuffix, " &
        '      " cpi.firstname as pi_firstname, cpi.namesuffix as pi_namesuffix, policycurrency, paidtodate, " &
        '      " csa.firstname as sa_firstname, csa.namesuffix as sa_namesuffix, a.phonenumber, a.locationcode, cph.customerid, " &
        '      " cph.ChiFstNm as ChiFstNm,  cph.ChiLstNm as ChiLstNm, " &
        '      " csa.ChiFstNm as csa_ChiFstNm,  csa.ChiLstNm as csa_ChiLstNm " &
        '      " from csw_poli_rel rph, customer cph, coveragedetail rpi, customer cpi, csw_poli_rel rsa, customer csa, agentcodes a, policyaccount p " &
        '      " where rph.policyaccountid = '" & strPolicy & "' " &
        '      " and rph.policyaccountid = rpi.policyaccountid and rph.policyaccountid = p.policyaccountid " &
        '      " and rph.policyrelatecode = 'PH' and rph.customerid = cph.customerid " &
        '      " and rpi.customerid = cpi.customerid and rpi.trailer=1" &
        '      " and rph.policyaccountid = rsa.policyaccountid " &
        '      " and rsa.policyrelatecode = 'SA' and rsa.customerid = csa.customerid and csa.agentcode = a.agentcode"


        ' Call SP
        'Dim connection As SqlConnection = New SqlConnection(strCIWConn1)
        'Dim dtClientInfo As DataTable = New DataTable
        'Dim dtPolicyInfo As DataTable = New DataTable
        'Dim dtCoRider As DataTable = New DataTable
        'connection.Open()
        'Try
        '    Dim command As SqlCommand = New SqlCommand("cswsp_clientinfo", connection)
        '    command.Parameters.Add("@PolicyAccountID", strPolicy)
        '    command.CommandType = CommandType.StoredProcedure
        '    Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
        '    adapter.Fill(dtClientInfo)

        '    command = New SqlCommand("cswsp_PolicyInfo", connection)
        '    command.Parameters.Add("@PolicyAccountID", strPolicy)
        '    command.CommandType = CommandType.StoredProcedure

        '    adapter = New SqlDataAdapter(command)
        '    adapter.Fill(dtPolicyInfo)

        '    command = New SqlCommand("cswsp_corider2", connection)
        '    command.Parameters.Add("@PolicyAccountID", strPolicy)
        '    command.CommandType = CommandType.StoredProcedure

        '    adapter = New SqlDataAdapter(command)
        '    adapter.Fill(dtCoRider)

        'Catch ex As Exception
        '    Console.WriteLine(ex.Message)
        '    Throw
        'Finally
        '    connection.Close()
        'End Try

        'Try
        '    sqlda = New SqlDataAdapter(strSQL, sqlConn)
        '    sqlda.TableMappings.Add("DDA1", "CCDR")
        '    sqlda.TableMappings.Add("DDA2", "BillingTypeCodes")
        '    sqlda.TableMappings.Add("DDA3", "AccountStatusCodes")
        '    sqlda.TableMappings.Add("DDA4", "ModeCodes")
        '    sqlda.TableMappings.Add("DDA5", "DDAStatusCodes")
        '    sqlda.TableMappings.Add("DDA6", "CCDRStatusCodes")
        '    sqlda.TableMappings.Add("DDA7", "PolicyAccount")
        '    sqlda.TableMappings.Add("DDA8", "csw_policy_value")
        '    sqlda.TableMappings.Add("DDA9", "agentcodes")
        '    sqlda.TableMappings.Add("DDA10", "csw_ing_logo_table")
        '    sqlda.TableMappings.Add("DDA11", "product_type")
        '    sqlda.TableMappings.Add("DDA12", "couponoptioncodes")
        '    sqlda.TableMappings.Add("DDA13", "dividendoptioncodes")
        '    sqlda.TableMappings.Add("DDA14", "Product_Chi")
        '    sqlda.TableMappings.Add("DDA15", "LevyHistory")
        '    sqlda.TableMappings.Add("DDA16", "InsuredInformation")
        '    sqlda.Fill(dsMisc, "DDA")

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'If dsMisc.Tables("PolicyAccount").Rows.Count = 0 Then
        '    MsgBox("Policy not found - " & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '    Exit Function
        'End If

        'If dsMisc.Tables("product_type").Rows.Count > 0 Then
        '    If IsDBNull(dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport")) Then
        '        MsgBox("Policy Letter is not available for this product yet." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '        Exit Function
        '    ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "A" Then
        '        For Each dr As DataRow In dtPolicyInfo.Rows
        '            If Mid(dr.Item("ProductID"), 2, 4) = "RE15" OrElse Mid(dr.Item("ProductID"), 2, 4) = "RE20" Then
        '                MsgBox("Policy Letter is not available, HRE15/HRE20/URE15/URE20 is attached." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '                Exit Function
        '            End If
        '        Next
        '    ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "Q" Then
        '        MsgBox("Policy value quotation may be needed." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '    End If
        'End If

        'oliver 2024-5-3 added for Table_Relocate_Sprint13
        Dim dtClientInfo As DataTable = New DataTable
        Dim dtPolicyInfo As DataTable = New DataTable
        Dim dtCoRider As DataTable = New DataTable
        Dim cswSPInfoDataSet As DataSet = GetCSWSPInfo(strPolicy)
        dtClientInfo = cswSPInfoDataSet.Tables(0)
        dtPolicyInfo = cswSPInfoDataSet.Tables(1)
        dtCoRider = cswSPInfoDataSet.Tables(2)
        dsMisc = GetLevyLetterDetail(strPolicy, If(frmParam.chkEng.Checked, "Eng", "Chi"), strFromDate, strToDate)

        Try
            rpt.Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Database.Tables("csw_ing_logo_table").SetDataSource(dsMisc.Tables("csw_ing_logo_table"))
            rpt.Subreports("ClientInfo.rpt - 01").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)
            rpt.Subreports("levyHistory").Database.Tables("LevyHistory").SetDataSource(dsMisc.Tables("LevyHistory"))
            rpt.Subreports("levyHistoryChinese").Database.Tables("LevyHistory").SetDataSource(dsMisc.Tables("LevyHistory"))

            rpt.SetParameterValue("PolicyID", strPolicy)

            If frmParam.chkChi.Checked = True Then
                rpt.SetParameterValue("Lang", "chi")
            End If

            If frmParam.chkEng.Checked = True Then
                rpt.SetParameterValue("Lang", "eng")
            End If

            Dim insuredEngName As String = ""
            Dim insuredChiName As String = ""
            Dim locationCode As String = ""
            Dim locationPhone As String = ""
            Dim dateFrom As String = ""
            Dim dateTo As String = ""
            Dim agentNameChi As String = ""
            Dim engDearTitle As String = ""
            Dim chineseDateYear As String = ""


            If frmParam.chkEng.Checked = True Then
                dateFrom = Format(frmParam.txtFrom.Value, "MMM-dd-yyyy")
                dateTo = Format(frmParam.txtTo.Value, "MMM-dd-yyyy")
            Else
                dateFrom = Format(frmParam.txtFrom.Value, "dd-MM-yyyy")
                dateTo = Format(frmParam.txtTo.Value, "dd-MM-yyyy")
            End If

            insuredEngName = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("pi_NameSuffix")) & " " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("pi_FirstName"))
            insuredChiName = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ChiLstNm")) & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ChiFstNm"))
            locationCode = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("locationcode"))
            agentNameChi = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("csa_ChiLstNm")) & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("csa_ChiFstNm"))
            engDearTitle = "Dear " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ph_nameprefix")) & " " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ph_namesuffix"))

            chineseDateYear = Now().Year & "¦~" & Now().Month & "¤ë" & Now().Day & "¤é"


            rpt.SetParameterValue("UFVal", "-99")
            rpt.SetParameterValue("TotalPremiumPaid", "0.00")
            rpt.SetParameterValue("ULink", "N")
            rpt.SetParameterValue("ReqID", "-1")
            rpt.SetParameterValue("AgentName", dsMisc.Tables("agentcodes").Rows(0).Item("AgName"))
            rpt.SetParameterValue("AgentPhone", Left(dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"), 4) & " " & Right(dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"), 4))
            rpt.SetParameterValue("LifeInsuredEngName", insuredEngName)
            rpt.SetParameterValue("LifeInsuredChiName", insuredChiName)
            rpt.SetParameterValue("Locationcode", locationCode)
            rpt.SetParameterValue("DateFrom", dateFrom)
            rpt.SetParameterValue("DateTo", dateTo)
            rpt.SetParameterValue("AgentNameChi", agentNameChi)
            rpt.SetParameterValue("EngDearTitle", engDearTitle)
            rpt.SetParameterValue("ChineseDateYear", chineseDateYear)
            rpt.SetParameterValue("strCSR", gsCSRName)
            rpt.SetParameterValue("strChiCSR", gsCSRChiName)

        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Function

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Public Function GetCSWSPInfo(ByVal strPolicy As String) As DataSet
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_CSWSP_INFO",
                        New Dictionary(Of String, String) From {
                        {"strPolicy", strPolicy}
                        })

            If ds.Tables.Count > 0 Then
                Return ds.Copy
            End If

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return ds
    End Function

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Public Function GetLevyLetterDetail(ByVal strPolicy As String, ByVal isEng As String, ByVal strFromDate As String, ByVal strToDate As String) As DataSet
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_LEVY_LETTER_DETAIL",
                        New Dictionary(Of String, String) From {
                        {"strPolicy", strPolicy},
                        {"isEng", isEng},
                        {"strFromDate", strFromDate},
                        {"strToDate", strToDate}
                        })

            If ds.Tables.Count > 0 Then
                Return ds.Copy
            End If

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return ds
    End Function

    '20171103 Levy
    Public Function LevyOutstandingLetterDetail()

        Dim frmParam As New frmPAYHRpt
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strActivity, strPolicy As String
        Dim blnAll As Boolean
        Dim dsMisc As New DataSet
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim strCIWConn1 As String
        'oliver 2024-7-5 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'AC - Change to use configuration setting - start
        '#If UAT = 1 Then
        '        strCIWConn1 = strCIWConn
        '#Else
        '        strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        '#End If
        strCIWConn1 = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "HKReportConnection").DecryptString()
        'If gUAT Then
        '    strCIWConn1 = strCIWConn
        'Else
        '    strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        'End If
        'AC - Change to use configuration setting - end


        sqlConn.ConnectionString = strCIWConn1

        blnCancel = True
        frmParam.Text = "Levy Payment on Insurance Premium"
        frmParam.txtPolicy.Text = strLastPolicy

        frmParam.txtFrom.Enabled = False
        frmParam.txtTo.Enabled = False
        frmParam.chkChi.Enabled = False
        frmParam.chkEng.Enabled = False

        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            'strFromDate = Format(frmParam.txtFrom.Value, "yyyyMMdd")
            'strToDate = Format(frmParam.txtTo.Value, "yyyyMMdd")

            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        '' Policy Info
        strSQL = "Select * from DDA where policyaccountid = '" & strPolicy & "'; "
        strSQL &= "Select * from CCDR where policyaccountid = '" & strPolicy & "'; "
        strSQL &= "Select bt.* from PolicyAccount P left join " & gcPOS & "vw_billingtypecodes bt " &
            " ON P.billingtype = billingtypecode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select ac.* from PolicyAccount P left join AccountStatusCodes ac " &
            " ON p.accountstatuscode = ac.accountstatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select m.* from PolicyAccount P left join ModeCodes m " &
            " ON p.mode = m.modecode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select ds.* from DDA d left join " & serverPrefix & " DDAStatusCodes ds " &
            " ON d.ddastatus = ds.ddastatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select cs.* from CCDR c left join " & serverPrefix & " CCDRStatusCodes cs " &
            " ON c.ccdrstatus = cs.ccdrstatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select * from PolicyAccount Where PolicyAccountid = '" & strPolicy & "'"

        '' Policy Value
        strSQL &= $"Select * from {serverPrefix}csw_policy_value where cswval_TPOLID = 'xxx'"

        strSQL &= "Select PhoneNumber, NameSuffix + ' ' + FirstName as AgName from agentcodes a, csw_poli_rel r, customer c " &
            " Where r.policyaccountid = '" & strPolicy & "'" &
            " and r.customerid = c.customerid " &
            " and r.policyrelatecode = 'SA' " &
            " and c.agentcode = a.agentcode "

        strSQL &= "SELECT ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
            "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        strSQL &= "Select p.companyid, pt.productid, ProductType, ProductPolValueFunc, PrintValueReport" &
            " From product_type pt Inner Join policyaccount p ON p.productid = pt.productid" &
            " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

        strSQL &= "Select c.* from policyaccount P left join " & gcPOS & "vw_CouponOptionCodes C " &
            " ON p.couponoption = c.couponoptioncode " &
            " Where policyaccountid = '" & strPolicy & "'"

        strSQL &= "Select c.* from policyaccount P left join " & gcPOS & "vw_DividendOptionCodes C " &
            " ON p.dividendoption = c.dividendoptioncode " &
            " Where policyaccountid = '" & strPolicy & "'"

        strSQL &= "Select pt.productid, pt.ChineseDescription" &
            " From " & gcNBSDB & "product_chi pt Inner Join policyaccount p ON p.productid = pt.productid" &
            " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

        '20180627 ZLVYSTL = 0 is not correct, no data found for partial payment. Talk with susan change to use ZLVYOS > 0 
        strSQL &= " Select Convert(datetime, LTRIM(RTRIM(CoverageStart)), 112) as CoverageStart, " &
                    " Convert(datetime, LTRIM(RTRIM(CoverageEnd)), 112) as CoverageEnd, LevyOutstanding " &
                    " from ( " &
                    " select DISTINCT (SELECT min(DTEEFF) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum AND a.ZLVYOS > 0) as CoverageStart,    " &
                    " (SELECT max(BTDATE) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum) as CoverageEnd, " &
                    " (SELECT SUM(ZLVYOS) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum) as LevyOutstanding  " &
                    " from levy_billing_record b " &
                    " where VALIDFLAG = 1  AND ZLVYOS > 0 AND POLICYNUM = '" & Replace(strPolicy, "'", "''") & "'" &
                    " ) outputTable "

        'strSQL &= " Select Convert(datetime, LTRIM(RTRIM(CoverageStart)), 112) as CoverageStart, " & _
        '            " Convert(datetime, LTRIM(RTRIM(CoverageEnd)), 112) as CoverageEnd, LevyOutstanding " & _
        '            " from ( " & _
        '            " select DISTINCT (SELECT min(DTEEFF) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum AND a.ZLVYOS > 0) as CoverageStart,    " & _
        '            " (SELECT max(BTDATE) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum) as CoverageEnd, " & _
        '            " (SELECT SUM(ZLVYOS) from levy_billing_record a where a.VALIDFLAG = 1 AND a.POLICYNUM = b.policynum) as LevyOutstanding  " & _
        '            " from levy_billing_record b " & _
        '            " where VALIDFLAG = 1 AND ZLVYSTL = 0 AND POLICYNUM = '" & Replace(strPolicy, "'", "''") & "'" & _
        '            " ) outputTable "

        strSQL &= "Select cph.nameprefix as ph_nameprefix, cph.firstname as ph_firstname, cph.namesuffix as ph_namesuffix, " &
              " cpi.firstname as pi_firstname, cpi.namesuffix as pi_namesuffix, policycurrency, paidtodate, " &
              " csa.firstname as sa_firstname, csa.namesuffix as sa_namesuffix, a.phonenumber, a.locationcode, cph.customerid, " &
              " cph.ChiFstNm as ChiFstNm,  cph.ChiLstNm as ChiLstNm, " &
              " csa.ChiFstNm as csa_ChiFstNm,  csa.ChiLstNm as csa_ChiLstNm " &
              " from csw_poli_rel rph, customer cph, coveragedetail rpi, customer cpi, csw_poli_rel rsa, customer csa, agentcodes a, policyaccount p " &
              " where rph.policyaccountid = '" & strPolicy & "' " &
              " and rph.policyaccountid = rpi.policyaccountid and rph.policyaccountid = p.policyaccountid " &
              " and rph.policyrelatecode = 'PH' and rph.customerid = cph.customerid " &
              " and rpi.customerid = cpi.customerid and rpi.trailer=1" &
              " and rph.policyaccountid = rsa.policyaccountid " &
              " and rsa.policyrelatecode = 'SA' and rsa.customerid = csa.customerid and csa.agentcode = a.agentcode"


        ' Call SP
        Dim connection As SqlConnection = New SqlConnection(strCIWConn1)
        Dim dtClientInfo As DataTable = New DataTable
        Dim dtPolicyInfo As DataTable = New DataTable
        Dim dtCoRider As DataTable = New DataTable
        connection.Open()
        Try
            Dim command As SqlCommand = New SqlCommand("cswsp_clientinfo", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(dtClientInfo)

            command = New SqlCommand("cswsp_PolicyInfo", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure

            adapter = New SqlDataAdapter(command)
            adapter.Fill(dtPolicyInfo)

            command = New SqlCommand("cswsp_corider2", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure

            adapter = New SqlDataAdapter(command)
            adapter.Fill(dtCoRider)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw
        Finally
            connection.Close()
        End Try

        Try
            sqlda = New SqlDataAdapter(strSQL, sqlConn)
            sqlda.TableMappings.Add("DDA1", "CCDR")
            sqlda.TableMappings.Add("DDA2", "BillingTypeCodes")
            sqlda.TableMappings.Add("DDA3", "AccountStatusCodes")
            sqlda.TableMappings.Add("DDA4", "ModeCodes")
            sqlda.TableMappings.Add("DDA5", "DDAStatusCodes")
            sqlda.TableMappings.Add("DDA6", "CCDRStatusCodes")
            sqlda.TableMappings.Add("DDA7", "PolicyAccount")
            sqlda.TableMappings.Add("DDA8", "csw_policy_value")
            sqlda.TableMappings.Add("DDA9", "agentcodes")
            sqlda.TableMappings.Add("DDA10", "csw_ing_logo_table")
            sqlda.TableMappings.Add("DDA11", "product_type")
            sqlda.TableMappings.Add("DDA12", "couponoptioncodes")
            sqlda.TableMappings.Add("DDA13", "dividendoptioncodes")
            sqlda.TableMappings.Add("DDA14", "Product_Chi")
            sqlda.TableMappings.Add("DDA15", "LevyOutstanding")
            sqlda.TableMappings.Add("DDA16", "InsuredInformation")
            sqlda.Fill(dsMisc, "DDA")

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'If dsMisc.Tables("PolicyAccount").Rows.Count = 0 Then
        '    MsgBox("Policy not found - " & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '    Exit Function
        'End If

        'If dsMisc.Tables("product_type").Rows.Count > 0 Then
        '    If IsDBNull(dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport")) Then
        '        MsgBox("Policy Letter is not available for this product yet." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '        Exit Function
        '    ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "A" Then
        '        For Each dr As DataRow In dtPolicyInfo.Rows
        '            If Mid(dr.Item("ProductID"), 2, 4) = "RE15" OrElse Mid(dr.Item("ProductID"), 2, 4) = "RE20" Then
        '                MsgBox("Policy Letter is not available, HRE15/HRE20/URE15/URE20 is attached." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '                Exit Function
        '            End If
        '        Next
        '    ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "Q" Then
        '        MsgBox("Policy value quotation may be needed." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
        '    End If
        'End If

        Try
            rpt.Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Database.Tables("csw_ing_logo_table").SetDataSource(dsMisc.Tables("csw_ing_logo_table"))
            rpt.Subreports("ClientInfo.rpt - 01").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)

            rpt.SetParameterValue("PolicyID", strPolicy)

            If frmParam.chkChi.Checked = True Then
                rpt.SetParameterValue("Lang", "chi")
            End If

            If frmParam.chkEng.Checked = True Then
                rpt.SetParameterValue("Lang", "eng")
            End If

            Dim insuredEngName As String = ""
            Dim insuredChiName As String = ""
            Dim locationCode As String = ""
            Dim locationPhone As String = ""
            Dim dateFrom As String = ""
            Dim dateTo As String = ""
            Dim agentNameChi As String = ""
            Dim engDearTitle As String = ""
            Dim chineseDateYear As String = ""
            Dim converingPeriod As String = ""
            Dim outstandingAmount As String = ""
            Dim coverageStart As String = ""
            Dim coverageEnd As String = ""


            If frmParam.chkEng.Checked = True Then
                dateFrom = Format(frmParam.txtFrom.Value, "MMM-dd-yyyy")
                dateTo = Format(frmParam.txtTo.Value, "MMM-dd-yyyy")
            Else
                dateFrom = Format(frmParam.txtFrom.Value, "dd-MM-yyyy")
                dateTo = Format(frmParam.txtTo.Value, "dd-MM-yyyy")
            End If

            insuredEngName = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("pi_NameSuffix")) & " " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("pi_FirstName"))
            insuredChiName = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ChiLstNm")) & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ChiFstNm"))
            locationCode = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("locationcode"))
            agentNameChi = Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("csa_ChiLstNm")) & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("csa_ChiFstNm"))
            engDearTitle = "Dear " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ph_nameprefix")) & " " & Trim(dsMisc.Tables("InsuredInformation").Rows(0).Item("ph_namesuffix"))

            chineseDateYear = Now().Year & "¦~" & Now().Month & "¤ë" & Now().Day & "¤é"


            coverageStart = CDate(dsMisc.Tables("LevyOutstanding").Rows(0).Item("CoverageStart")).Year & "/" & CDate(dsMisc.Tables("LevyOutstanding").Rows(0).Item("CoverageStart")).Month & "/" & "1"
            coverageEnd = CDate(dsMisc.Tables("LevyOutstanding").Rows(0).Item("CoverageEnd")).Year & "/" & CDate(dsMisc.Tables("LevyOutstanding").Rows(0).Item("CoverageEnd")).Month & "/" & LastDayForMonth(CDate(dsMisc.Tables("LevyOutstanding").Rows(0).Item("CoverageEnd")).Month)
            converingPeriod = coverageStart & " - " & coverageEnd

            '20180305 Requst by Ada to add eng if chinese name is empty
            If (agentNameChi.Trim() = "") Then
                agentNameChi = dsMisc.Tables("agentcodes").Rows(0).Item("AgName")
            End If

            outstandingAmount = dsMisc.Tables("PolicyAccount").Rows(0).Item("PolicyCurrency").ToString() & " " & CDbl(dsMisc.Tables("LevyOutstanding").Rows(0).Item("LevyOutstanding")).ToString("0.00")

            rpt.SetParameterValue("UFVal", "-99")
            rpt.SetParameterValue("TotalPremiumPaid", "0.00")
            rpt.SetParameterValue("ULink", "N")
            rpt.SetParameterValue("ReqID", "-1")
            rpt.SetParameterValue("AgentName", dsMisc.Tables("agentcodes").Rows(0).Item("AgName"))
            rpt.SetParameterValue("AgentPhone", Left(dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"), 4) & " " & Right(dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"), 4))
            rpt.SetParameterValue("LifeInsuredEngName", insuredEngName)
            rpt.SetParameterValue("LifeInsuredChiName", insuredChiName)
            rpt.SetParameterValue("Locationcode", locationCode)
            rpt.SetParameterValue("DateFrom", dateFrom)
            rpt.SetParameterValue("DateTo", dateTo)
            rpt.SetParameterValue("AgentNameChi", agentNameChi)
            rpt.SetParameterValue("EngDearTitle", engDearTitle)
            rpt.SetParameterValue("ChineseDateYear", chineseDateYear)
            rpt.SetParameterValue("strCSR", gsCSRName)
            rpt.SetParameterValue("strChiCSR", gsCSRChiName)
            rpt.SetParameterValue("SubjectName", "Levy Payment on Insurance Premium")
            rpt.SetParameterValue("ConveringPeriod", converingPeriod)
            rpt.SetParameterValue("OutstandingAmount", outstandingAmount)

        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Function

    Private Function LastDayForMonth(ByVal month As Integer) As Integer

        Select Case month
            Case 1, 3, 5, 7, 8, 10, 12
                Return 31
            Case 4, 6, 9, 11
                Return 30
            Case 2
                Return 28
        End Select

    End Function


    '20180627 frmDisplayTheLevyOverdue

    Public Sub LevyOverduePreFollowUpExcel1And2()

        Dim frmParam As New frmLevyOverduePreGenerate
        Dim ds As New DataSet
        Dim strErr As String = ""

        blnCancel = True

        If frmParam.ShowDialog() = DialogResult.OK Then

            If (frmParam.TextBox1.Text = "") Then
                Throw New Exception("Cut Off day is empty. Please input. Thanks ")
            End If

            Using wsLEVY As New LEVYWS.LEVYWS()
                wsLEVY.DBSOAPHeaderValue = GetLEVYWSDBHeader()
                wsLEVY.MQSOAPHeaderValue = GetLEVYWSMQHeader()
                wsLEVY.Url = Utility.Utility.GetWebServiceURL("LEVYWS", gobjDBHeader, gobjMQQueHeader)
                wsLEVY.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsLEVY.Timeout = 10000000

                If Not wsLEVY.GetLevyOverDueList(g_Comp.Trim(), g_Env.Trim(), DateTime.ParseExact(frmParam.TextBox1.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), ds, strErr) Then
                    Throw New Exception("Failed to get LevyOverDueList record." & vbCrLf & strErr)
                End If
            End Using

            If Not ExportCSV(frmParam.txtPath.Text & "\LevyOverdueList_Pre1And2_" & Format(DateTime.Now, "MMddyyyyhhmm") & ".csv", ds.Tables(0)) Then
            End If

            MsgBox("The Excel is generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If


    End Sub


    Public Sub LevyOverduePreFollowUpExcel3()

        Dim frmParam As New frmLevyOverduePreGenerate
        Dim ds As New DataSet
        Dim strErr As String = ""

        blnCancel = True

        If frmParam.ShowDialog() = DialogResult.OK Then

            If (frmParam.TextBox1.Text = "") Then
                Throw New Exception("Cut Off day is empty. Please input. Thanks ")
            End If

            Using wsLEVY As New LEVYWS.LEVYWS()
                wsLEVY.DBSOAPHeaderValue = GetLEVYWSDBHeader()
                wsLEVY.MQSOAPHeaderValue = GetLEVYWSMQHeader()
                wsLEVY.Url = Utility.Utility.GetWebServiceURL("LEVYWS", gobjDBHeader, gobjMQQueHeader)
                wsLEVY.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsLEVY.Timeout = 10000000

                If Not wsLEVY.GetLevyOverLetterRound3PrintLists(g_Comp.Trim(), g_Env.Trim(), g_LAUser, DateTime.ParseExact(frmParam.TextBox1.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), ds, strErr) Then
                    Throw New Exception("Failed to get LevyOverDueList record." & vbCrLf & strErr)
                End If
            End Using

            If Not ExportCSV(frmParam.txtPath.Text & "\PreLevyOverdueList_Pre3" & Format(DateTime.Now, "MMddyyyyhhmm") & ".csv", ds.Tables(0)) Then
            End If

            MsgBox("The Excel is generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If


    End Sub

    Public Function GetLEVYWSMQHeader() As LEVYWS.MQSOAPHeader

        Dim objLEVYWSMQHeader As New LEVYWS.MQSOAPHeader
        objLEVYWSMQHeader.QueueManager = gobjMQQueHeader.QueueManager
        objLEVYWSMQHeader.RemoteQueue = gobjMQQueHeader.RemoteQueue
        objLEVYWSMQHeader.ReplyToQueue = gobjMQQueHeader.ReplyToQueue
        objLEVYWSMQHeader.LocalQueue = gobjMQQueHeader.LocalQueue
        objLEVYWSMQHeader.Timeout = 300000

        objLEVYWSMQHeader.CompanyID = gobjMQQueHeader.CompanyID
        objLEVYWSMQHeader.UserID = gobjMQQueHeader.UserID

        objLEVYWSMQHeader.ProjectAlias = gobjDBHeader.ProjectAlias
        objLEVYWSMQHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
        objLEVYWSMQHeader.UserType = gobjDBHeader.UserType
        objLEVYWSMQHeader.EnvironmentUse = gobjDBHeader.EnvironmentUse

        Return objLEVYWSMQHeader

    End Function

    Public Function GetLEVYWSDBHeader() As LEVYWS.DBSOAPHeader

        Dim objLEVYWSDBHeader As New LEVYWS.DBSOAPHeader
        objLEVYWSDBHeader.User = gobjDBHeader.UserID
        objLEVYWSDBHeader.Project = gobjDBHeader.ProjectAlias
        objLEVYWSDBHeader.Env = gobjDBHeader.EnvironmentUse
        objLEVYWSDBHeader.Comp = gobjDBHeader.CompanyID
        objLEVYWSDBHeader.ConnectionAlias = gobjDBHeader.CompanyID & "CIW" & gobjDBHeader.EnvironmentUse
        objLEVYWSDBHeader.UserType = gobjDBHeader.UserType
        Return objLEVYWSDBHeader

    End Function

    '20180322 frmLevyOverdueFollowUpRpt Excel file should be exported SucessFullList
    Public Sub LevyOverdueFollowUpRpt()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmLevyOverdueFollowUpRpt
        Dim strSQLWhereCase As String = ""
        Dim strSQLOrderbyCase As String = " order by policyNumber  "
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection

        blnCancel = True


        'SQL for get dropDownList
        Dim runningDateList As New DataSet
        Dim strSQLRunningDate As String = ""
        Dim strCIWConn1 As String
        Dim dsMisc As New DataSet

        strCIWConn1 = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "HKReportConnection").DecryptString()
        'If gUAT Then
        '    strCIWConn1 = strCIWConn
        'Else
        '    strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        'End If

        Dim connection As SqlConnection = New SqlConnection(strCIWConn1)

        strSQLRunningDate = "select top 12 replace(convert(varchar,RunningDate,111), '/','-')  from levy_overdue_list_Log group by RunningDate order by RunningDate desc "

        sqlConn.ConnectionString = strCIWConn1

        Try
            sqlda = New SqlDataAdapter(strSQLRunningDate, sqlConn)
            sqlda.TableMappings.Add("DDA", "LevyList")
            sqlda.Fill(dsMisc, "DDA")

            frmParam.runningDateList = dsMisc

        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try
        ''''''

        If frmParam.ShowDialog() = DialogResult.OK Then

            strSQL = "SELECT policyNumber, totalOS, CustomerID, PhoneMobile, EmailAddr, " &
                " NameSuffix, FirstName,  Gender, UseChiInd,  ChiLstNm, ChiFstNm,  " &
                " cswpad_add1, cswpad_add2, cswpad_add3, PolicyCurrency, IsSuccess, RunningDate, levy_Overdue_Follow_Up_type.OverdueFollowUpTypeName, " &
                " PaidToDate, BillToDate, AccountStatus, PDFAmount, APLAmount, PremiumSuspense, LevySuspense   " &
                " from levy_Overdue_list_log " &
                " inner join levy_Overdue_Follow_Up_type on levy_Overdue_list_log.sendingType = levy_Overdue_Follow_Up_type.OverdueFollowUpTypeId " &
                " where 1=1  " &
                " and replace(convert(varchar,RunningDate,111), '/','-')  = '" & frmParam.cboRunningDate.SelectedValue & "'"
            '" and PaidToDate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' "

            wndMain.Cursor = Cursors.WaitCursor

            If frmParam.CheckBox1.Checked Then

                strSQLWhereCase = " and sendingType = 1 and IsSuccess = 1 "

                If GetDT(strSQL & strSQLWhereCase & strSQLOrderbyCase, strCIWConn, dtResult, strError) Then
                    If Not ExportCSV(frmParam.txtPath.Text & "\LevyOverdueList_SMS_Success_" & Format(DateTime.Now, "MMddyyyyhhmm") & ".csv", dtResult) Then
                    End If
                Else
                    MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                End If

            End If

            If frmParam.CheckBox2.Checked Then

                strSQLWhereCase = " and sendingType = 4 and IsSuccess = 1 "

                If GetDT(strSQL & strSQLWhereCase & strSQLOrderbyCase, strCIWConn, dtResult, strError) Then
                    If Not ExportCSV(frmParam.txtPath.Text & "\LevyOverdueList_Letter_Success" & Format(DateTime.Now, "MMddyyyyhhmm") & ".csv", dtResult) Then
                    End If
                Else
                    MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                End If

            End If

            If frmParam.CheckBox3.Checked Then

                strSQLWhereCase = " and sendingType = 1 and IsSuccess = 0  "

                If GetDT(strSQL & strSQLWhereCase & strSQLOrderbyCase, strCIWConn, dtResult, strError) Then
                    If Not ExportCSV(frmParam.txtPath.Text & "\LevyOverdueList_SMS_Fail" & Format(DateTime.Now, "MMddyyyyhhmm") & ".csv", dtResult) Then
                    End If
                Else
                    MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                End If

            End If


            MsgBox("The excel is generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If

    End Sub

    Private Function GetLevySuspense(ByVal policyNo As String)

        Dim levySuspense As Double = 0
        Dim strErr As String = ""

        objCI.MQQueuesHeader = GetMQQueHeader()
        objCI.DBHeader = GetMComHeader()
        objCI.CiwHeader = objCI.DBHeader
        objCI.GetLevyAmountSuspense(policyNo, levySuspense, strErr)
        If (strErr <> "") Then
            levySuspense = 0
        End If

        Return levySuspense
    End Function

    Private Function GetLevyAmountQuotation(ByVal policyNumber As String,
                                                    ByVal currency As String,
                                                    ByVal premiumAmount As Double, ByVal paidToDate As DateTime) As Double

        Dim strErr As String = ""
        Dim premiumAllocatedAmountDue As String = "0.00"
        Dim premiumAmountDue As String = "0.00"
        Dim levyQuotationAmount As String = "00.0"

        objCI.MQQueuesHeader = GetMQQueHeader()
        objCI.DBHeader = GetMComHeader()
        objCI.CiwHeader = objCI.DBHeader
        objCI.GetLevyQuotation(policyNumber, currency, premiumAmount, paidToDate, paidToDate, False, "R", "CAP", premiumAllocatedAmountDue, premiumAmountDue, levyQuotationAmount, strErr)

        If (strErr <> "") Then
            Return 0
        End If

        Return CDbl(levyQuotationAmount)

    End Function

    'Private Function GetLevyAmountOutstanding(ByVal policyNo As String)

    '    Dim levyAmountOutstanding As Double = 0
    '    Dim strErr As String = ""
    '    objCI.MQQueuesHeader = GetMQQueHeader()
    '    objCI.DBHeader = GetMComHeader()
    '    objCI.CiwHeader = objCI.DBHeader
    '    objCI.GetLevyAmountOutstanding(policyNo, levyAmountOutstanding, strErr)

    '    If (strErr <> "") Then
    '        levyAmountOutstanding = 0
    '    End If

    '    Return levyAmountOutstanding
    'End Function

    'levy
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS

    Private Function GetMQQueHeader() As Utility.Utility.MQHeader
        Dim objMQQueHeader As Utility.Utility.MQHeader
        objMQQueHeader.UserID = gsUser
        objMQQueHeader.QueueManager = g_Qman '"LACSQMGR1" '"WINTEL"
        objMQQueHeader.RemoteQueue = g_WinRemoteQ '"LACSSIT02.TO.LA400SIT02" '"LIFEASIA.RQ1"
        objMQQueHeader.ReplyToQueue = g_LAReplyQ '"LA400SIT02.TO.LACSSIT02" '"WINTEL.RQ1"
        objMQQueHeader.LocalQueue = g_WinLocalQ  '"LACSSIT02.QUEUE1.LCL" '"WINTEL.LQ1"
        objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        Return objMQQueHeader
    End Function

    Private Function GetMComHeader() As Utility.Utility.ComHeader
        Dim objDBHeader As Utility.Utility.ComHeader
        objDBHeader.UserID = gsUser
        objDBHeader.EnvironmentUse = g_Env '"SIT02"
        objDBHeader.ProjectAlias = "LAS" '"LAS"
        objDBHeader.CompanyID = g_Comp '"ING"
        objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"
        Return objDBHeader
    End Function

    ' ES007 begin
    Public Function LA_PolicyDetail()

        Dim frmParam As New frmPAYHRpt
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strActivity, strPolicy As String
        Dim blnAll As Boolean
        Dim dsMisc As New DataSet
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim strCIWConn1 As String
        'oliver 2024-7-5 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'AC - Change to use configuration setting - start
        '#If UAT = 1 Then
        '        strCIWConn1 = strCIWConn
        '#Else
        '        strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        '#End If
        strCIWConn1 = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "HKReportConnection").DecryptString()
        'If gUAT Then
        '    strCIWConn1 = strCIWConn
        'Else
        '    strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        'End If
        'AC - Change to use configuration setting - end


        sqlConn.ConnectionString = strCIWConn1

        blnCancel = True
        frmParam.Text = "Policy Status Letter"
        frmParam.txtPolicy.Text = strLastPolicy
        frmParam.txtFrom.Enabled = False
        frmParam.txtTo.Enabled = False
        frmParam.chkChi.Enabled = True
        frmParam.chkEng.Enabled = True

        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        '' Policy Info
        strSQL = "Select * from DDA where policyaccountid = '" & strPolicy & "'; "
        strSQL &= "Select * from CCDR where policyaccountid = '" & strPolicy & "'; "
        strSQL &= "Select bt.* from PolicyAccount P left join " & gcPOS & "vw_billingtypecodes bt " &
            " ON P.billingtype = billingtypecode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select ac.* from PolicyAccount P left join AccountStatusCodes ac " &
            " ON p.accountstatuscode = ac.accountstatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select m.* from PolicyAccount P left join ModeCodes m " &
            " ON p.mode = m.modecode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select ds.* from DDA d left join " & serverPrefix & " DDAStatusCodes ds " &
            " ON d.ddastatus = ds.ddastatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select cs.* from CCDR c left join " & serverPrefix & " CCDRStatusCodes cs " &
            " ON c.ccdrstatus = cs.ccdrstatuscode " &
            " Where policyaccountid = '" & strPolicy & "'; "

        strSQL &= "Select * from PolicyAccount Where PolicyAccountid = '" & strPolicy & "'"

        '' Policy Value
        strSQL &= $"Select * from {serverPrefix}csw_policy_value where cswval_TPOLID = 'xxx'"

        strSQL &= "Select PhoneNumber, NameSuffix + ' ' + FirstName as AgName from agentcodes a, csw_poli_rel r, customer c " &
            " Where r.policyaccountid = '" & strPolicy & "'" &
            " and r.customerid = c.customerid " &
            " and r.policyrelatecode = 'SA' " &
            " and c.agentcode = a.agentcode "

        strSQL &= "SELECT ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
            "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
            ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        strSQL &= "Select p.companyid, pt.productid, ProductType, ProductPolValueFunc, PrintValueReport" &
            " From product_type pt Inner Join policyaccount p ON p.productid = pt.productid" &
            " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

        strSQL &= "Select c.* from policyaccount P left join " & gcPOS & "vw_CouponOptionCodes C " &
            " ON p.couponoption = c.couponoptioncode " &
            " Where policyaccountid = '" & strPolicy & "'"

        strSQL &= "Select c.* from policyaccount P left join " & gcPOS & "vw_DividendOptionCodes C " &
            " ON p.dividendoption = c.dividendoptioncode " &
            " Where policyaccountid = '" & strPolicy & "'"

        strSQL &= "Select pt.productid, pt.ChineseDescription" &
            " From " & gcNBSDB & "product_chi pt Inner Join policyaccount p ON p.productid = pt.productid" &
            " Where p.policyaccountid = '" & Replace(strPolicy, "'", "''") & "'"

        ' Call SP
        Dim connection As SqlConnection = New SqlConnection(strCIWConn1)
        Dim dtClientInfo As DataTable = New DataTable
        Dim dtPolicyInfo As DataTable = New DataTable
        Dim dtCoRider As DataTable = New DataTable
        connection.Open()
        Try
            Dim command As SqlCommand = New SqlCommand("cswsp_clientinfo", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(dtClientInfo)

            command = New SqlCommand("cswsp_PolicyInfo", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure

            adapter = New SqlDataAdapter(command)
            adapter.Fill(dtPolicyInfo)

            command = New SqlCommand("cswsp_corider2", connection)
            command.Parameters.Add("@PolicyAccountID", strPolicy)
            command.CommandType = CommandType.StoredProcedure

            adapter = New SqlDataAdapter(command)
            adapter.Fill(dtCoRider)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw
        Finally
            connection.Close()
        End Try

        Try
            sqlda = New SqlDataAdapter(strSQL, sqlConn)
            sqlda.TableMappings.Add("DDA1", "CCDR")
            sqlda.TableMappings.Add("DDA2", "BillingTypeCodes")
            sqlda.TableMappings.Add("DDA3", "AccountStatusCodes")
            sqlda.TableMappings.Add("DDA4", "ModeCodes")
            sqlda.TableMappings.Add("DDA5", "DDAStatusCodes")
            sqlda.TableMappings.Add("DDA6", "CCDRStatusCodes")
            sqlda.TableMappings.Add("DDA7", "PolicyAccount")
            sqlda.TableMappings.Add("DDA8", "csw_policy_value")
            sqlda.TableMappings.Add("DDA9", "agentcodes")
            sqlda.TableMappings.Add("DDA10", "csw_ing_logo_table")
            sqlda.TableMappings.Add("DDA11", "product_type")
            sqlda.TableMappings.Add("DDA12", "couponoptioncodes")
            sqlda.TableMappings.Add("DDA13", "dividendoptioncodes")
            sqlda.TableMappings.Add("DDA14", "Product_Chi")
            sqlda.Fill(dsMisc, "DDA")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        If dsMisc.Tables("PolicyAccount").Rows.Count = 0 Then
            MsgBox("Policy not found - " & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
            Exit Function
        End If

        If dsMisc.Tables("product_type").Rows.Count > 0 Then
            If IsDBNull(dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport")) Then
                MsgBox("Policy Letter is not available for this product yet." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
                Exit Function
            ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "A" Then
                For Each dr As DataRow In dtPolicyInfo.Rows
                    If Mid(dr.Item("ProductID"), 2, 4) = "RE15" OrElse Mid(dr.Item("ProductID"), 2, 4) = "RE20" Then
                        MsgBox("Policy Letter is not available, HRE15/HRE20/URE15/URE20 is attached." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
                        Exit Function
                    End If
                Next
            ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "Q" Then
                MsgBox("Policy value quotation may be needed." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
            End If
        End If

        If dsMisc.Tables("PolicyAccount").Rows(0).Item("CompanyID") = "EAA" Then
            Dim objDB As Object
            Dim objRS As ADODB.Recordset
            objDB = CreateObject("Dbsecurity.Database")
            Call objDB.Connect(gsUser, strValProj, strValConn)

            'AC - Change advance compilation option to configuration file - start
            '#If UAT = 1 Then
            '            strSQL = "call CSDCIWSBP.policyval('" & strPolicy & "')"
            '#Else
            '            strSQL = "call CIWLIB.policyval('" & strPolicy & "')"
            '#End If
            If gUAT Then
                strSQL = "call CSDCIWSBP.policyval('" & strPolicy & "')"
            Else
                strSQL = "call CIWLIB.policyval('" & strPolicy & "')"
            End If
            'AC - Change advance compilation option to configuration file - end


            objRS = objDB.executestatement(strSQL)

            If objRS.RecordCount > 0 Then
                Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                drVal("cswval_id") = -1
                For i As Integer = 0 To objRS.Fields.Count - 1
                    If objRS.Fields(i).Type = ADODB.DataTypeEnum.adChar Or objRS.Fields(i).Type = ADODB.DataTypeEnum.adVarChar Then
                        drVal.Item("cswval_" & objRS.Fields(i).Name) = RTrim(objRS.Fields(i).Value)
                    Else
                        drVal.Item("cswval_" & objRS.Fields(i).Name) = objRS.Fields(i).Value
                    End If
                Next
                drVal("cswval_TPOLID") = strPolicy
                drVal("cswval_PUASITotal") = dsMisc.Tables("PolicyAccount").Rows(0).Item("AdditionDeathCvr")
                dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
            End If
        Else
            ' Call Web service to get value
            Dim dtVAL As DataTable
            Dim strErr As String
            Dim dblTotal As Double

            dtVAL = GetLAPolicyValue(g_Comp, strPolicy, dsMisc.Tables("product_type").Rows(0).Item("ProductPolValueFunc"), dblTotal, strErr)
            If strErr = "" Then
                If dtVAL IsNot Nothing AndAlso dtVAL.Rows.Count > 0 Then
                    With dtVAL
                        Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                        drVal("cswval_id") = -1
                        drVal("cswval_TFLOID") = "Y"
                        drVal("cswval_TPOLID") = strPolicy
                        drVal("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        drVal("cswval_TPADDT") = DBNull.Value
                        drVal("cswval_TCSHVL") = .Rows(0).Item("TotalSurVal")
                        drVal("cswval_TBSCSV") = .Rows(0).Item("BaseCashValue")
                        drVal("cswval_TVLPUA") = .Rows(0).Item("PUACashValue")
                        drVal("cswval_TDIVDP") = .Rows(0).Item("DivOnDeposit")
                        drVal("cswval_TDEPIN") = .Rows(0).Item("DivDepositInt")
                        drVal("cswval_TPDF") = .Rows(0).Item("PDFAmount")
                        drVal("cswval_TPDFIN") = .Rows(0).Item("PDFInt")
                        drVal("cswval_TPRMRD") = .Rows(0).Item("PremRefund")
                        drVal("cswval_TLONAT") = .Rows(0).Item("Loan")
                        drVal("cswval_TLONIT") = .Rows(0).Item("LoanInt")
                        drVal("cswval_TAPLAT") = .Rows(0).Item("APL")
                        drVal("cswval_TAPLIT") = .Rows(0).Item("APLInt")
                        drVal("cswval_TMAXLN") = DBNull.Value
                        drVal("cswval_TDSCLN") = DBNull.Value
                        drVal("cswval_TBSELN") = DBNull.Value
                        drVal("cswval_TDSCFR") = DBNull.Value
                        drVal("cswval_TINRRB") = DBNull.Value
                        drVal("cswval_TRDCHV") = DBNull.Value
                        drVal("cswval_TCOUDP") = .Rows(0).Item("Coupon")
                        drVal("cswval_TCOUIT") = .Rows(0).Item("CouponInt")
                        drVal("cswval_TERRFG") = DBNull.Value
                        drVal("cswval_TOSPRM") = DBNull.Value
                        drVal("cswval_TREAMT") = DBNull.Value
                        drVal("cswval_DivYear") = .Rows(0).Item("DivYear")
                        drVal("cswval_CouYear") = .Rows(0).Item("CouponYear")
                        drVal("cswval_DivDeclare") = .Rows(0).Item("DivDeclare")
                        drVal("cswval_PremSusp") = .Rows(0).Item("PremSuspense")
                        drVal("cswval_PremRefund") = DBNull.Value
                        drVal("cswval_PUASITotal") = .Rows(0).Item("PaidUpAddition")
                        drVal("cswval_PUASICurrent") = .Rows(0).Item("CurrentPaidUp")
                        drVal("cswval_CouOpt") = .Rows(0).Item("CouponOpt")
                        drVal("cswval_DivDepositInt") = DBNull.Value
                        drVal("cswval_DivOpt") = .Rows(0).Item("DivOpt")
                        drVal("cswval_MiscSusp") = .Rows(0).Item("MiscSuspense")
                        drVal("cswval_CouDelcare") = .Rows(0).Item("TotalCouponDeclare")
                        dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
                    End With
                Else
                    With dtVAL
                        Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                        drVal("cswval_id") = -1
                        drVal("cswval_TFLOID") = "Y"
                        drVal("cswval_TPOLID") = strPolicy
                        drVal("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        drVal("cswval_TCSHVL") = dblTotal
                        dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
                    End With
                End If
            Else
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
        End If

        'For Each dt As DataTable In dsMisc.Tables
        '    MsgBox(dt.TableName & "=" & dt.Rows.Count)
        'Next

        Try
            rpt.Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Database.Tables("csw_ing_logo_table").SetDataSource(dsMisc.Tables("csw_ing_logo_table"))
            rpt.Subreports("ClientInfo.rpt").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)
            rpt.Subreports("ClientInfo.rpt - 01").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CCDR").SetDataSource(dsMisc.Tables("CCDR"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DDA").SetDataSource(dsMisc.Tables("DDA"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("BillingTypeCodes").SetDataSource(dsMisc.Tables("BillingTypeCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("ModeCodes").SetDataSource(dsMisc.Tables("ModeCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("AccountStatusCodes").SetDataSource(dsMisc.Tables("AccountStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CCDRStatusCodes").SetDataSource(dsMisc.Tables("CCDRStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DDAStatusCodes").SetDataSource(dsMisc.Tables("DDAStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DividendOptionCodes").SetDataSource(dsMisc.Tables("DividendOptionCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CouponOptionCodes").SetDataSource(dsMisc.Tables("CouponOptionCodes"))

            rpt.Subreports("PolicyInfo.rpt - 01 - 02").Database.Tables("cswsp_policyinfo;1").SetDataSource(dtPolicyInfo)

            rpt.Subreports("PolicyInfo.rpt - 01 - 01 - 01").Database.Tables("cswsp_corider2;1").SetDataSource(dtCoRider)
            rpt.Subreports("PolicyInfo.rpt - 01 - 01 - 01").Database.Tables("Product_Chi").SetDataSource(dsMisc.Tables("Product_Chi"))

            rpt.Subreports("PolicyValue.rpt").Database.Tables("csw_policy_value").SetDataSource(dsMisc.Tables("csw_policy_value"))
            rpt.Subreports("PolicyValue.rpt").Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            'rpt.Subreports("PolicyValue.rpt").Database.Tables("CouponOptionCodes").SetDataSource(dsMisc.Tables("csw_policy_value"))
            'rpt.Subreports("PolicyValue.rpt").Database.Tables("DividendOptionCodes").SetDataSource(dsMisc.Tables("csw_policy_value"))

            rpt.SetParameterValue("PolicyID", strPolicy)

            If frmParam.chkChi.Checked = True Then
                rpt.SetParameterValue("Lang", "chi")
            End If

            If frmParam.chkEng.Checked = True Then
                rpt.SetParameterValue("Lang", "eng")
            End If

            rpt.SetParameterValue("UFVal", "-99")
            rpt.SetParameterValue("TotalPremiumPaid", "0.00")
            rpt.SetParameterValue("ULink", "N")
            rpt.SetParameterValue("ReqID", "-1")
            rpt.SetParameterValue("AgentName", dsMisc.Tables("agentcodes").Rows(0).Item("AgName"))
            rpt.SetParameterValue("AgentPhone", dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"))

            '20171103 Levy
            Dim LevyInsurance As Double = 0
            Dim LevySuspense As Double = 0
            ' Dim LevyTotalModalPremium As Double = 0
            Dim premium As Double = 0
            Dim policyCurrency As String = ""
            Dim paidToDate As Date = New Date

            Try

                LevySuspense = GetLevySuspense(strPolicy)
                ' LevyInsurance = GetLevyAmountOutstanding(strPolicy)
                premium = dsMisc.Tables("PolicyAccount").Rows(0)("ModalPremium")
                policyCurrency = dsMisc.Tables("PolicyAccount").Rows(0)("PolicyCurrency")
                paidToDate = dsMisc.Tables("PolicyAccount").Rows(0)("PaidToDate")
                LevyInsurance = GetLevyAmountQuotation(strPolicy, policyCurrency, premium, paidToDate)
                'LevyTotalModalPremium =
            Catch ex As Exception
                'do nothing
            End Try

            Try
                rpt.SetParameterValue("LevyInsurance", LevyInsurance.ToString("0.00"))
                rpt.SetParameterValue("LevySuspense", LevySuspense.ToString("0.00"))
                rpt.SetParameterValue("LevyTotalModalPremium", String.Format("{0:n}", premium + LevyInsurance))
            Catch ex As Exception
                'do nothing
            End Try

        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Function
    ' ES007 end

#Region "ILAS Notification Letter Enhancement"

    Dim strRptHeader As String = String.Empty
    Public Property ReportHeader() As String
        Set(ByVal value As String)
            strRptHeader = value
        End Set
        Get
            Return strRptHeader
        End Get
    End Property


    Public Function ILAS_NotificationLetter()

        Dim objMQQueHeader As Utility.Utility.MQHeader
        Dim objDBHeader As Utility.Utility.ComHeader

        Dim frmParam As New frmPAYHRpt
        Dim strCIWConn1 As String = strCIWConn
        Dim strSQL As String = String.Empty
        Dim strPolicy As String = String.Empty
        Dim strErr As String = String.Empty

        Dim dtGeneralInfo As New DataTable

        blnCancel = True
        frmParam.Text = ReportHeader
        frmParam.txtPolicy.Text = strLastPolicy
        frmParam.txtFrom.Enabled = False
        frmParam.txtTo.Enabled = False
        frmParam.chkChi.Visible = False
        frmParam.chkEng.Visible = False


        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        'Load High Risk Fund Option Window
        Dim dtFundcoll As New DataTable
        If Not Me.LoadFundPopup(strPolicy, strCIWConn1, dtFundcoll, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
        End If
        dtFundcoll.TableName = "rpt_ILAS_Fund_Desc"

        Dim iMasterPlan As New System.Collections.Generic.List(Of String)
        Dim iKnowUSinglePremiumPlan As New System.Collections.Generic.List(Of String)
        Dim iKnowURegularPlan As New System.Collections.Generic.List(Of String)
        Dim HorizonPlan As New System.Collections.Generic.List(Of String)

        Dim HorizonUpfrontCharge As New System.Collections.Generic.List(Of String)
        Dim iKnowSPUpfrontCharge As New System.Collections.Generic.List(Of String)

        '1. Get iMaster Plan
        Dim dtTmp As New DataTable
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_IMAS','CRS_IWEA','CRS_VINT')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, iMasterPlan)


        '2. iKnowU Single Premium Plan
        dtTmp.Clear()
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_IKUSP')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, iKnowUSinglePremiumPlan)

        '3. iKnowU Reqular Premium Plan
        dtTmp.Clear()
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_IKU')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, iKnowURegularPlan)

        '4. Horizon plan
        dtTmp.Clear()
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_HORI')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, HorizonPlan)

        '5. Get Horizon Upfront Charge Value
        dtTmp.Clear()
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_HUF')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, HorizonUpfrontCharge)

        '6. Get i.Know Single Premium Upfrom Charge Value 
        dtTmp.Clear()
        strSQL = "select Value  from " & serverPrefix & " CodeTable where code in ('CRS_iKSPUF')"
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        Me.TableToList(dtTmp, iKnowSPUpfrontCharge)

        strSQL = "Select Pa.PolicyAccountID, Product.ProductID, Product.[Description] As ProductDescription, Product_Chi.ChineseDescription,pa.PolicyCurrency, " &
                    " Isnull(cswpad_add1,'') as Address1,Isnull(cswpad_add2,'') as Address2,Isnull(cswpad_add3,'') as Address3,Isnull(cswpad_city,'') As City," &
                    " Isnull(poliAddress.cswpad_tel1,'') as TelePhone1,IsNull(poliAddress.cswpad_tel2,'') as TelePhone2," &
                    " Isnull(customer.FirstName,'') as FirstName, Isnull(customer.NameSuffix,'') as LastName, Isnull(customer.ChiFstNm,'') as ChiFstNm, " &
                    " Isnull(customer.ChiLstNm,'') as ChiLstNm,Isnull(customer.PhoneMobile,'') as PhoneMobile, " &
                    " Isnull(rrt.Camrrt_loc_code,'') as LocationCode , Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') as AgentLastName, '' as AgentFirstName, " &
                    " case when len(Isnull(Agent.camaib_chi_name,''))>0 then Isnull(Agent.camaib_chi_name,'') else Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') end as AgentChiName, pa.Mode,M.ModeDesc, SPACE(15) as CooloffDate,pa.ModalPremium, Cov.ModalPremium as OneOffModalPremium, " &
                    " DATEDIFF(YEAR, cov.IssueDate, cov.PREMIUMCESSATIONDATE) as Period  from PolicyAccount as PA " &
                    " Inner Join  csw_poli_rel as poliRel on PA.PolicyAccountID = poliRel.PolicyAccountID and poliRel.PolicyRelateCode ='PH' " &
                    " Inner Join Coverage as Cov on pa.PolicyAccountID = cov.PolicyAccountID  and Cov.Trailer = '1' " &
                    " Left Join csw_policy_address as poliAddress on poliRel.PolicyAccountID = poliAddress.cswpad_poli_id " &
                    " Left Join customer on poliRel.CustomerID = customer.CustomerID   " &
                    " Left Join csw_poli_rel as PoliRelWA on pa.PolicyAccountID = PoliRelWA.PolicyAccountID and PoliRelWA.PolicyRelateCode= 'WA' " &
                    " Left Join customer as CustWA on PoliRelWA.CustomerID = custwa.CustomerID  " &
                    " Left Join Product on PA.ProductID = product.ProductID  " &
                    " Left Join " & gcNBSDB & "Product_Chi on Pa.ProductID  =Product_Chi.ProductID  " &
                    " Left Join ModeCodes M on pa.Mode = M.ModeCode " &
                    " Left Join  {1}.dbo.cam_agent_info_basic as Agent  on CustWA.AgentCode = agent.camaib_agent_no " &
                    " Inner Join {1}.dbo.cam_agent_info_dirmgr as  aid on Agent.camaib_agent_no = aid.camaid_agent_no " &
                    " Left Join {1}.dbo.cam_rdbu_rel_tab as rrt on aid.camaid_sort_key = rrt.Camrrt_agency_code and aid.camaid_section_no = rrt.Camrrt_section_no " &
                    " where PA.PolicyAccountID ='{0}' "


        strSQL = String.Format(strSQL, strPolicy, g_CAM_Database)
        Me.ExcecuteSQL(strSQL, strCIWConn1, dtGeneralInfo)

        If dtGeneralInfo.Rows.Count > 0 Then
            'Get cooling off Date 
            Dim strBasicPlan As String = String.Empty

            If Not String.IsNullOrEmpty(dtGeneralInfo.Rows(0)("ProductID")) Then
                strBasicPlan = Convert.ToString(dtGeneralInfo.Rows(0)("ProductID")).Trim
            End If

            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicy
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)

            Dim dsCurr As New DataSet
            Dim strTime As String = ""

            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            clsPOS.MQQueuesHeader = gobjMQQueHeader
            clsPOS.DBHeader = gobjDBHeader
            If Not clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr) Then
                MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            End If
            strErr = String.Empty

            If dsCurr.Tables.Count > 0 Then
                If dsCurr.Tables(0).Rows.Count > 0 Then
                    For Each drGeneralInfo As DataRow In dtGeneralInfo.Rows
                        drGeneralInfo("CooloffDate") = dsCurr.Tables(0).Rows(0)("CooloffDate")
                        If String.IsNullOrEmpty(strBasicPlan) Then
                            strBasicPlan = drGeneralInfo("ProductID")
                        End If
                    Next
                End If
            End If
            dtGeneralInfo.TableName = "rpt_ILAS_Notifiation_Letter"

            'Get Plan type 
            iKnowUReg = False
            iKnowUSP = False
            Horizon = False
            iMaster = False
            Me.GetPlanType(strBasicPlan, iMasterPlan, iKnowURegularPlan, iKnowUSinglePremiumPlan, HorizonPlan)

            'Get Suitability Option
            Dim strSuitabilityOption As String = String.Empty
            If Not Me.GetSuitability(strPolicy, strCIWConn1, strSuitabilityOption, strErr) Then
                MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            End If

            'Get Company Logo 
            Dim dtCompanyLogo As New DataTable
            If Not Me.GetCompanyLogo(dtCompanyLogo, strCIWConn1, strErr) Then
                MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            End If
            dtCompanyLogo.TableName = "Company_Logo"

            rpt.Database.Tables("rpt_ILAS_Notifiation_Letter").SetDataSource(dtGeneralInfo)
            rpt.Subreports("rpt_ILAS_Fund_Desc").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
            rpt.Subreports("rpt_ILAS_Fund_Desc_Chi").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
            rpt.Subreports("Company_Logo").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
            rpt.Subreports("ING_Address").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
            rpt.Subreports("ING_Phone").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)

            Dim type As Boolean = False
            If ReportHeader.Contains("(uncertain replies)") Then
                type = True 'UnCertain replies
            Else
                type = False
            End If

            rpt.SetParameterValue("iKnowUReg", iKnowUReg)
            rpt.SetParameterValue("iKnowUSP", iKnowUSP)
            rpt.SetParameterValue("Horizon", Horizon)
            rpt.SetParameterValue("iMaster", iMaster)
            rpt.SetParameterValue("DocTypeUC", type)
            rpt.SetParameterValue("SuitabilityOption", strSuitabilityOption)
            If iKnowSPUpfrontCharge.Count > 0 Then
                rpt.SetParameterValue("iKnowSPUpfrontCharge", iKnowSPUpfrontCharge(0))
            Else
                rpt.SetParameterValue("iKnowSPUpfrontCharge", String.Empty)
            End If
            If HorizonUpfrontCharge.Count > 0 Then
                rpt.SetParameterValue("HorizonUpfrontCharge", HorizonUpfrontCharge(0))
            Else
                rpt.SetParameterValue("HorizonUpfrontCharge", String.Empty)
            End If


            blnCancel = False
        Else
            MsgBox("Can't find the detail for given policy. Policy No - " + strPolicy, MsgBoxStyle.Exclamation, ReportHeader)
        End If

    End Function

    Private Function ExcecuteSQL(ByVal strSQL As String, ByVal strConn As String, ByRef dtResult As DataTable) As Boolean
        ExcecuteSQL = False

        Dim connection As SqlConnection = New SqlConnection(strConn)
        Dim command As SqlCommand = New SqlCommand(strSQL, connection)
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)

        adapter.Fill(dtResult)

        ExcecuteSQL = True
    End Function

    Private Function ExcecuteSql(ByVal command As SqlCommand, ByRef dtResult As DataTable) As Boolean
        ExcecuteSql = False

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)

        adapter.Fill(dtResult)

        ExcecuteSql = True
    End Function

    Private Sub TableToList(ByVal dtTable As DataTable, ByRef destList As System.Collections.Generic.List(Of String))

        For Each tmpRow As DataRow In dtTable.Rows
            destList.Add(tmpRow(0).ToString())
        Next

    End Sub

    Private iMaster As Boolean = False
    Private iKnowUReg As Boolean = False
    Private iKnowUSP As Boolean = False
    Private Horizon As Boolean = False

    Private Sub GetPlanType(ByVal strBasicPlan As String,
                                ByVal lstiMaster As System.Collections.Generic.List(Of String),
                                ByRef lstiKnowU As System.Collections.Generic.List(Of String),
                                ByRef lstiKnowUSP As System.Collections.Generic.List(Of String),
                                ByRef lsthorizon As System.Collections.Generic.List(Of String))

        If lstiMaster.Contains(strBasicPlan) Then
            iMaster = True
            Exit Sub
        ElseIf lstiKnowU.Contains(strBasicPlan) Then
            iKnowUReg = True
            Exit Sub
        ElseIf lstiKnowUSP.Contains(strBasicPlan) Then
            iKnowUSP = True
            Exit Sub
        ElseIf lsthorizon.Contains(strBasicPlan) Then
            Horizon = True
            Exit Sub
        End If

    End Sub

    Private Function LoadFundPopup(ByVal strPolicyNo As String, ByVal strConn As String, ByRef dtFundcoll As DataTable, ByRef strMsg As String) As Boolean
        LoadFundPopup = False
        Dim objFundCollPopup As New frmHighRiskFund()

        Try

            Dim strSQL As String = "select distinct  fa.cswcfa_fund_code as FundCode ,FundDesc.mpfinv_chi_desc as FundDescription,FundDesc.mpfinv_chi_name as FundChiDesc from csw_fund_allocation fa " &
                                    "Inner Join (Select  cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no,Max(cswcfa_eff_date) as Eff_Date from csw_fund_allocation where cswcfa_policy_no = '{0}' " &
                                    "Group   By cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no) as DistFund On  " &
                                    "fa.cswcfa_policy_no = DistFund.cswcfa_policy_no And fa.cswcfa_fund_code = DistFund.cswcfa_fund_code " &
                                    "and fa.cswcfa_coverage_no = DistFund.cswcfa_coverage_no and fa.cswcfa_eff_date = DistFund.Eff_Date " &
                                    "Inner Join cswvw_mpf_investment as FundDesc  On  fa.cswcfa_fund_code = FundDesc.mpfinv_code " &
                                    "where fa.cswcfa_policy_no = '{0}'  "

            strSQL = String.Format(strSQL, strPolicyNo)
            Me.ExcecuteSQL(strSQL, strConn, dtFundcoll)

            objFundCollPopup.FundColl = dtFundcoll
            If dtFundcoll.Rows.Count = 0 Then
                strMsg = "There is no fund exist for given Policy No : " + strPolicyNo
            Else
                objFundCollPopup.ShowDialog()
                dtFundcoll.Clear()
                dtFundcoll = objFundCollPopup.FundColl
                LoadFundPopup = True
            End If

        Catch ex As Exception
            strMsg = ex.Message
        Finally
            objFundCollPopup = Nothing
        End Try

    End Function

    Private Function GetSuitability(ByVal strPolicy As String, ByVal strConn As String, ByRef strResult As String, ByRef strMsg As String) As Boolean
        GetSuitability = False
        Dim strSQL As String = "select a.ext_ref as policynumber, a.suitability, case when a.suitability is null or a.suitability = '91004' then 'C' else b.code_label end code_label, a.risk_profile, a.risk_marks from CNB_Staging..cnbods_indiv_agmt a left join CNB_Staging..CNBODS_code b on a.suitability = b.code_id " &
                                " where a.ext_ref = '{0}' "
        strSQL = String.Format(strSQL, strPolicy)
        Dim dtTmp As New DataTable
        If Not Me.ExcecuteSQL(strSQL, strConn, dtTmp) Then
            strMsg = "Cannot get Policy No. suitability Value"
        Else
            If dtTmp.Rows.Count > 0 Then
                strResult = dtTmp.Rows(0)("code_label")
            End If
            GetSuitability = True
        End If

    End Function

    Private Function GetCompanyLogo(ByRef dtLogo As DataTable, ByVal strCIWCon As String, ByRef strMsg As String) As Boolean
        GetCompanyLogo = False

        Dim strSQL As String = "SELECT top 1 ING_Logo = a.cswilt_Logo, ING_CompanyAddr = b.cswilt_Logo , ING_Phone = c.cswilt_Logo, CareCompany = d.cswilt_Logo " &
                                " FROM " & gcPOS & "vw_csw_ing_logo_table as a " &
                                " Left Join " & gcPOS & "vw_csw_ing_logo_table b on RTRIM(UPPER(b.cswilt_LogoDesc)) = UPPER ('Footer ING Address') " &
                                " Left Join " & gcPOS & "vw_csw_ing_logo_table c On RTRIM(UPPER(c.cswilt_LogoDesc)) = UPPER ('Footer ING Interactive Service') " &
                                " Left Join " & gcPOS & "vw_csw_ing_logo_table d On RTRIM(UPPER(d.cswilt_LogoDesc)) = UPPER ('Footer Caring Company') " &
                                " where RTRIM(UPPER(a.cswilt_LogoDesc)) = UPPER('ING Logo') "

        If Not Me.ExcecuteSQL(strSQL, strCIWCon, dtLogo) Then
            strMsg = "Cannot get Company Logo"
        Else
            GetCompanyLogo = True
        End If

    End Function

#End Region

    Public Sub PostSalesCallVC()
        Dim clsCRS As LifeClientInterfaceComponent.clsCRS
        Dim strSQL As String = String.Empty
        Dim strErr As String = String.Empty
        Dim dtPolicy As New DataTable("policy")
        Dim dtServiceLog As New DataTable("serviceLog")
        Dim dsLetter As New DataSet("dsReport")
        Dim sbSql As System.Text.StringBuilder
        'Dim blnAgentResponse As Boolean
        Dim datCoolOff As DateTime

        Dim strServiceEventCategory As String = "'86', '88', '93', '91'"
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        blnCancel = True

        ' service event log
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select top 1 * from ServiceEventDetail")
        sbSql.AppendLine("where EventCategoryCode in (" & strServiceEventCategory & ")")
        sbSql.AppendFormat("and PolicyAccountID='{0}'", PolicyNo) : sbSql.AppendLine()
        sbSql.AppendLine("and EventStatusCode='C'")
        sbSql.AppendLine("order by EventInitialDateTime desc, ServiceEventNumber desc")

        strSQL = sbSql.ToString()
        ExcecuteSQL(strSQL, strCIWConn, dtServiceLog)

        If dtServiceLog.Rows.Count = 0 Then
            MsgBox("Courtesy Call record not found.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If Not (dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() = "30" OrElse
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() = "50" OrElse
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() = "60") Then

            MsgBox("Event detail not match to print this letter.", MsgBoxStyle.Exclamation, "Error")
            Return

        End If

        strSQL = "Select Pa.PolicyAccountID, Product.ProductID, Product.[Description] As ProductDescription, Product.ChineseDescription,pa.PolicyCurrency, " &
                    " Isnull(cswpad_add1,'') as Address1,Isnull(cswpad_add2,'') as Address2,Isnull(cswpad_add3,'') as Address3,Isnull(cswpad_city,'') As City," &
                    " Isnull(poliAddress.cswpad_tel1,'') as TelePhone1,IsNull(poliAddress.cswpad_tel2,'') as TelePhone2," &
                    " Isnull(customer.FirstName,'') as FirstName, Isnull(customer.NameSuffix,'') as LastName, Isnull(customer.ChiFstNm,'') as ChiFstNm, " &
                    " Isnull(customer.ChiLstNm,'') as ChiLstNm, case RTRIM(Isnull(customer.PhoneMobile,'')) when '' then customer.PhonePager else customer.PhoneMobile end as PhoneMobile, " &
                    " Isnull(rrt.Camrrt_loc_code,'') as LocationCode , Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') as AgentFullName, '' as AgentDesc, '' as AgentDescChi, " &
                    " case when len(Isnull(Agent.camaib_chi_name,''))>0 then Isnull(Agent.camaib_chi_name,'') else Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') end as AgentChiName, pa.Mode,M.ModeDesc, pa.ModalPremium, Cov.ModalPremium as OneOffModalPremium, " &
                    " DATEDIFF(YEAR, cov.IssueDate, cov.PREMIUMCESSATIONDATE) as Period, setting.cswpsd_category, setting.cswpsd_has_fees, aid.camaid_channel_code, " &
                    " isnull(Product.cbp_booster,'') as cbp_booster, isnull(Product.cbp_singleprem,'') as cbp_singleprem, Pa.CompanyID " &
                    " from PolicyAccount as PA " &
                    " Inner Join  csw_poli_rel as poliRel on PA.PolicyAccountID = poliRel.PolicyAccountID and poliRel.PolicyRelateCode ='PH' " &
                    " Inner Join Coverage as Cov on pa.PolicyAccountID = cov.PolicyAccountID  and Cov.Trailer = '1' " &
                    " Left Join csw_policy_address as poliAddress on poliRel.PolicyAccountID = poliAddress.cswpad_poli_id " &
                    " Left Join customer on poliRel.CustomerID = customer.CustomerID   " &
                    " Left Join csw_poli_rel as PoliRelWA on pa.PolicyAccountID = PoliRelWA.PolicyAccountID and PoliRelWA.PolicyRelateCode= 'WA' " &
                    " Left Join customer as CustWA on PoliRelWA.CustomerID = custwa.CustomerID  " &
                    " Left Join cswvw_Product product on PA.ProductID = product.ProductID  " &
                    " Left Join ModeCodes M on pa.Mode = M.ModeCode " &
                    " Left Join  {1}.dbo.cam_agent_info_basic as Agent  on CustWA.AgentCode = agent.camaib_agent_no " &
                    " Inner Join {1}.dbo.cam_agent_info_dirmgr as  aid on Agent.camaib_agent_no = aid.camaid_agent_no " &
                    " Left Join {1}.dbo.cam_rdbu_rel_tab as rrt on aid.camaid_sort_key = rrt.Camrrt_agency_code and aid.camaid_section_no = rrt.Camrrt_section_no " &
                    " left join " & serverPrefix & "csw_post_sales_call_product_setting setting on Pa.ProductID=setting.cswpsd_ProductID " &
                    " where PA.PolicyAccountID ='{0}' "


        strSQL = String.Format(strSQL, PolicyNo, g_CAM_Database)
        Me.ExcecuteSQL(strSQL, strCIWConn, dtPolicy)

        If dtPolicy.Rows.Count = 0 Then
            MsgBox("Policy information not found.", MsgBoxStyle.Critical, "Error")
            Return
        End If

        If dtPolicy.Rows(0)("CompanyID").ToString().Trim() <> "ING" OrElse dtPolicy.Rows(0)("CompanyID").ToString().Trim() <> "BMU" Then
            MsgBox("This function only support LifeAsia policy.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If dtPolicy.Rows(0)("PhoneMobile").ToString().Trim() = String.Empty Then
            MsgBox("Policy owner mobile number is empty.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        'dtPolicy.Columns.Remove("CompanyID")

        dtPolicy.Columns.Add("EventTypeCode", GetType(String))
        dtPolicy.Columns.Add("AgentResponse", GetType(Boolean))
        dtPolicy.Columns.Add("CooloffDate", GetType(DateTime))
        dtPolicy.Columns.Add("isSM", GetType(Boolean))
        dtPolicy.Columns.Add("isVC", GetType(Boolean))

        Using frm As New frmILASPostSalesLetterParm(False)

            If dtPolicy.Rows(0)("camaid_channel_code") = "ING" Then
                frm.txtAgent.Text = "our insurance agent " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªí" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim()
            ElseIf dtPolicy.Rows(0)("camaid_channel_code") = "BKC" OrElse dtPolicy.Rows(0)("camaid_channel_code") = "BKD" OrElse dtPolicy.Rows(0)("camaid_channel_code") = "BKE" Then
                frm.txtAgent.Text = "our insurance agent xxx of " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªíxxx (" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim() & ")"
            Else
                frm.txtAgent.Text = "our insurance agent " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim() & " of " & dtPolicy.Rows(0)("camaid_channel_code").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªí" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim() & " (" & dtPolicy.Rows(0)("camaid_channel_code").ToString().Trim() & ")"
            End If

            If frm.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            dtPolicy.Rows(0)("AgentResponse") = frm.chkAgentResponse.Checked
            dtPolicy.Rows(0)("AgentDesc") = frm.txtAgent.Text.Trim()
            dtPolicy.Rows(0)("AgentDescChi") = frm.txtAgentChi.Text.Trim()
            datCoolOff = frm.dtCoolOff.Value

        End Using

        If dtServiceLog.Rows.Count > 0 Then
            dtPolicy.Rows(0)("EventTypeCode") = dtServiceLog.Rows(0)("EventTypeCode")
        End If

        ' get cooling off date
        Dim dsSendData As New DataSet
        Dim dtSendData As New DataTable
        Dim dr As DataRow = dtSendData.NewRow()
        dtSendData.Columns.Add("PolicyNo")
        dr("PolicyNo") = Me.PolicyNo
        dtSendData.Rows.Add(dr)
        dsSendData.Tables.Add(dtSendData)

        Dim dsCurr As New DataSet
        'Dim strTime As String = ""

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = gobjMQQueHeader
        clsPOS.DBHeader = gobjDBHeader
        If Not clsPOS.GetContractDetail(Me.PolicyNo, dsCurr, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            Return
        End If
        strErr = String.Empty

        If dsCurr.Tables.Count > 0 AndAlso dsCurr.Tables(0).Rows.Count > 0 Then
            'If dsCurr.Tables(0).Rows(0)("CooloffDate") Is DBNull.Value OrElse CType(dsCurr.Tables(0).Rows(0)("CooloffDate"), DateTime) = #1/1/1900# Then
            '    MsgBox("Cooling off date is empty.", MsgBoxStyle.Critical, "Error")
            '    Return
            'End If

            dtPolicy.Rows(0)("CooloffDate") = dsCurr.Tables(0).Rows(0)("CooloffDate")
            'Else
            '    MsgBox("Failed to get cooling off date.", MsgBoxStyle.Critical, "Error")
            '    Return
        End If

        If dtPolicy.Rows(0)("CooloffDate") Is DBNull.Value OrElse dtPolicy.Rows(0)("CooloffDate") = #1/1/1900# Then
            dtPolicy.Rows(0)("CooloffDate") = datCoolOff
        End If

        ' get suitability mismatch and vc indicator
        Dim callType As LifeClientInterfaceComponent.CRSWS.PostSalesCallType
        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = gobjDBHeader
        'If Not clsCRS.GetPolicyPostSalesCallType(Me.PolicyNo, callType, strErr) Then
        If Not GetPolicyPostSalesCallType(Me.PolicyNo, callType, strErr, getCompanyCode(g_Comp)) Then
            MsgBox("FNA form not found.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        If ((callType And LifeClientInterfaceComponent.CRSWS.PostSalesCallType.VulnerableCustomer) = LifeClientInterfaceComponent.CRSWS.PostSalesCallType.VulnerableCustomer) OrElse
            ((callType And LifeClientInterfaceComponent.CRSWS.PostSalesCallType.NoFNA) = LifeClientInterfaceComponent.CRSWS.PostSalesCallType.NoFNA) Then

            dtPolicy.Rows(0)("isVC") = True
        Else
            dtPolicy.Rows(0)("isVC") = False
        End If

        If (callType And LifeClientInterfaceComponent.CRSWS.PostSalesCallType.SuitabilityMismatch) = LifeClientInterfaceComponent.CRSWS.PostSalesCallType.SuitabilityMismatch Then
            dtPolicy.Rows(0)("isSM") = True
        Else
            dtPolicy.Rows(0)("isSM") = False
        End If

        ' get coverage
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select cov.*, s.*, prod_cat.cswcpc_category_desc, prod_cat.cswcpc_category_desc_chi,")
        sbSql.AppendLine("DATEDIFF(YY, cov.IssueDate, cov.PREMIUMCESSATIONDATE) + case when SUBSTRING(CONVERT(char(8), cov.PREMIUMCESSATIONDATE, 112), 5,4) > SUBSTRING(CONVERT(char(8), cov.IssueDate, 112), 5,4) then 1 else 0 end as PaymentTerm,")
        sbSql.AppendLine("p.Description, p.ChineseDescription, isnull(p.cbp_booster,' ') as cbp_booster, isnull(p.cbp_singleprem,' ') as cbp_singleprem ")
        sbSql.AppendLine("from Coverage cov ")
        sbSql.AppendLine("left outer join " & serverPrefix & "csw_post_sales_call_product_setting s on cov.ProductID=s.cswpsd_ProductID ")
        sbSql.AppendLine("left outer join " & serverPrefix & "csw_post_sales_call_product_category prod_cat on s.cswpsd_category=prod_cat.cswcpc_category")
        sbSql.AppendLine("inner join cswvw_Product p on cov.ProductID=p.ProductID")
        sbSql.AppendFormat("where PolicyAccountID='{0}'", Me.PolicyNo) : sbSql.AppendLine()
        sbSql.AppendLine("order by cov.Trailer")

        strSQL = sbSql.ToString()
        Dim dtCov As New DataTable("coverage")
        Me.ExcecuteSQL(strSQL, strCIWConn, dtCov)

        Dim drBasicPlan As DataRow() = dtCov.Select("Trailer=1")
        If drBasicPlan.Length = 0 Then
            MsgBox("Cannot find basic plan.", MsgBoxStyle.Critical, "Error")
            Return
        End If

        ' get logo
        Dim dtCompanyLogo As New DataTable
        If Not Me.GetCompanyLogo(dtCompanyLogo, strCIWConn, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            Return
        End If
        dtCompanyLogo.TableName = "Company_Logo"

        dsLetter.Tables.Add(dtPolicy)
        dsLetter.Tables.Add(dtCov)
        dsLetter.Tables.Add(dtCompanyLogo)

        'dsLetter.WriteXmlSchema("C:\PostSalesCallVC.xsd")

        rpt.SetDataSource(dsLetter)

        blnCancel = False

    End Sub

    Public Sub PostSalesCalliLAS()
        Dim clsCRS As LifeClientInterfaceComponent.clsCRS
        Dim strSQL As String = String.Empty
        Dim strErr As String = String.Empty
        Dim dtPolicy As New DataTable("policy")
        Dim dtServiceLog As New DataTable("serviceLog")
        Dim dsLetter As New DataSet("dsReport")
        Dim sbSql As System.Text.StringBuilder
        Dim blnAgentResponse As Boolean
        Dim datCoolOff As DateTime
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim strServiceEventCategory As String = "'86', '88', '93', '91'"

        blnCancel = True

        ' service event log
        sbSql = New System.Text.StringBuilder()
        sbSql.AppendLine("select top 1 * from ServiceEventDetail")
        sbSql.AppendLine("where EventCategoryCode in (" & strServiceEventCategory & ")")
        sbSql.AppendFormat("and PolicyAccountID='{0}'", PolicyNo) : sbSql.AppendLine()
        sbSql.AppendLine("and EventStatusCode='C'")
        sbSql.AppendLine("order by EventInitialDateTime desc, ServiceEventNumber desc")

        strSQL = sbSql.ToString()
        ExcecuteSQL(strSQL, strCIWConn, dtServiceLog)

        If dtServiceLog.Rows.Count = 0 Then
            MsgBox("Courtesy Call record not found.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If Not (dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() = "30" OrElse
            dtServiceLog.Rows(0)("EventTypeCode").ToString().Trim() = "50") Then

            MsgBox("Event detail not match to print this letter.", MsgBoxStyle.Exclamation, "Error")
            Return

        End If

        strSQL = "Select Pa.PolicyAccountID, Product.ProductID, Product.[Description] As ProductDescription, Product.ChineseDescription,pa.PolicyCurrency, " &
                    " Isnull(cswpad_add1,'') as Address1,Isnull(cswpad_add2,'') as Address2,Isnull(cswpad_add3,'') as Address3,Isnull(cswpad_city,'') As City," &
                    " Isnull(poliAddress.cswpad_tel1,'') as TelePhone1,IsNull(poliAddress.cswpad_tel2,'') as TelePhone2," &
                    " Isnull(customer.FirstName,'') as FirstName, Isnull(customer.NameSuffix,'') as LastName, Isnull(customer.ChiFstNm,'') as ChiFstNm, " &
                    " Isnull(customer.ChiLstNm,'') as ChiLstNm, case RTRIM(Isnull(customer.PhoneMobile,'')) when '' then customer.PhonePager else customer.PhoneMobile end as PhoneMobile, " &
                    " Isnull(rrt.Camrrt_loc_code,'') as LocationCode , Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') as AgentFullName, '' as AgentDesc, '' as AgentDescChi, " &
                    " case when len(Isnull(Agent.camaib_chi_name,''))>0 then Isnull(Agent.camaib_chi_name,'') else Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') end as AgentChiName, pa.Mode,M.ModeDesc, pa.ModalPremium, Cov.ModalPremium as OneOffModalPremium, " &
                    " DATEDIFF(YEAR, cov.IssueDate, cov.PREMIUMCESSATIONDATE) as Period, " &
                    " setting.cswpsd_surr_charge, setting.cswpsd_surr_period, aid.camaid_channel_code, " &
                    " isnull(Product.cbp_booster,'') as cbp_booster, isnull(Product.cbp_singleprem,'') as cbp_singleprem, PA.CompanyID " &
                    " from PolicyAccount as PA " &
                    " Inner Join  csw_poli_rel as poliRel on PA.PolicyAccountID = poliRel.PolicyAccountID and poliRel.PolicyRelateCode ='PH' " &
                    " Inner Join Coverage as Cov on pa.PolicyAccountID = cov.PolicyAccountID  and Cov.Trailer = '1' " &
                    " Left Join csw_policy_address as poliAddress on poliRel.PolicyAccountID = poliAddress.cswpad_poli_id " &
                    " Left Join customer on poliRel.CustomerID = customer.CustomerID   " &
                    " Left Join csw_poli_rel as PoliRelWA on pa.PolicyAccountID = PoliRelWA.PolicyAccountID and PoliRelWA.PolicyRelateCode= 'WA' " &
                    " Left Join customer as CustWA on PoliRelWA.CustomerID = custwa.CustomerID  " &
                    " Left Join cswvw_Product as Product on PA.ProductID = product.ProductID  " &
                    " Left Join ModeCodes M on pa.Mode = M.ModeCode " &
                    " Left Join  {1}.dbo.cam_agent_info_basic as Agent  on CustWA.AgentCode = agent.camaib_agent_no " &
                    " Inner Join {1}.dbo.cam_agent_info_dirmgr as  aid on Agent.camaib_agent_no = aid.camaid_agent_no " &
                    " Left Join {1}.dbo.cam_rdbu_rel_tab as rrt on aid.camaid_sort_key = rrt.Camrrt_agency_code and aid.camaid_section_no = rrt.Camrrt_section_no " &
                    " left join " & serverPrefix & "csw_post_sales_call_product_setting setting on Pa.ProductID=setting.cswpsd_ProductID " &
                    " where PA.PolicyAccountID ='{0}' "


        strSQL = String.Format(strSQL, PolicyNo, g_CAM_Database)
        Me.ExcecuteSQL(strSQL, strCIWConn, dtPolicy)

        If dtPolicy.Rows.Count = 0 Then
            MsgBox("Policy information not found.", MsgBoxStyle.Critical, "Error")
            Return
        End If

        If dtPolicy.Rows(0)("CompanyID").ToString().Trim() <> "ING" OrElse dtPolicy.Rows(0)("CompanyID").ToString().Trim() <> "BMU" Then
            MsgBox("This function only support LifeAsia policy.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        If dtPolicy.Rows(0)("PhoneMobile").ToString().Trim() = String.Empty Then
            MsgBox("Policy owner mobile number is empty.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        dtPolicy.Columns.Add("EventTypeCode", GetType(String))
        dtPolicy.Columns.Add("AgentResponse", GetType(Boolean))
        dtPolicy.Columns.Add("CooloffDate", GetType(DateTime))
        dtPolicy.Columns.Add("SuitabilityChoice", GetType(String))
        dtPolicy.Columns.Add("Remuneration", GetType(Decimal))

        Using frm As New frmILASPostSalesLetterParm(True)

            If dtPolicy.Rows(0)("camaid_channel_code") = "ING" Then
                frm.txtAgent.Text = "our insurance agent " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªí" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim()
            ElseIf dtPolicy.Rows(0)("camaid_channel_code") = "BKC" OrElse dtPolicy.Rows(0)("camaid_channel_code") = "BKD" OrElse dtPolicy.Rows(0)("camaid_channel_code") = "BKE" Then
                frm.txtAgent.Text = "our insurance agent xxx of " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªíxxx (" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim() & ")"
            Else
                frm.txtAgent.Text = "our insurance agent " & dtPolicy.Rows(0)("AgentFullName").ToString().Trim() & " of " & dtPolicy.Rows(0)("camaid_channel_code").ToString().Trim()
                frm.txtAgentChi.Text = "¾P°â¥Nªí" & dtPolicy.Rows(0)("AgentChiName").ToString().Trim() & " (" & dtPolicy.Rows(0)("camaid_channel_code").ToString().Trim() & ")"
            End If

            If frm.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            dtPolicy.Rows(0)("AgentResponse") = frm.chkAgentResponse.Checked
            dtPolicy.Rows(0)("AgentDesc") = frm.txtAgent.Text.Trim()
            dtPolicy.Rows(0)("AgentDescChi") = frm.txtAgentChi.Text.Trim()
            dtPolicy.Rows(0)("SuitabilityChoice") = IIf(frm.rdoChoiceA.Checked, "A", IIf(frm.rdoChoiceB.Checked, "B", ""))
            dtPolicy.Rows(0)("Remuneration") = Decimal.Parse(frm.txtSalesRemuneration.Text.Trim())
            datCoolOff = frm.dtCoolOff.Value

        End Using


        If dtServiceLog.Rows.Count > 0 Then
            dtPolicy.Rows(0)("EventTypeCode") = dtServiceLog.Rows(0)("EventTypeCode")
        End If

        ' get cooling off date
        Dim dsSendData As New DataSet
        Dim dtSendData As New DataTable
        Dim dr As DataRow = dtSendData.NewRow()
        dtSendData.Columns.Add("PolicyNo")
        dr("PolicyNo") = Me.PolicyNo
        dtSendData.Rows.Add(dr)
        dsSendData.Tables.Add(dtSendData)

        Dim dsCurr As New DataSet
        Dim strTime As String = ""

        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        clsPOS.MQQueuesHeader = gobjMQQueHeader
        clsPOS.DBHeader = gobjDBHeader
        If Not clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            Return
        End If
        strErr = String.Empty

        If dsCurr.Tables.Count > 0 AndAlso dsCurr.Tables(0).Rows.Count > 0 Then
            'If dsCurr.Tables(0).Rows(0)("CooloffDate") Is DBNull.Value OrElse CType(dsCurr.Tables(0).Rows(0)("CooloffDate"), DateTime) = #1/1/1900# Then
            '    MsgBox("Cooling off date is empty.", MsgBoxStyle.Critical, "Error")
            '    Return
            'End If

            dtPolicy.Rows(0)("CooloffDate") = dsCurr.Tables(0).Rows(0)("CooloffDate")
            'Else
            '    MsgBox("Failed to get cooling off date.", MsgBoxStyle.Critical, "Error")
            '    Return
        End If

        If dtPolicy.Rows(0)("CooloffDate") Is DBNull.Value OrElse dtPolicy.Rows(0)("CooloffDate") = #1/1/1900# Then
            dtPolicy.Rows(0)("CooloffDate") = datCoolOff
        End If


        Dim dtHighRiskFund As DataTable = Nothing
        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = gobjDBHeader
        If Not clsCRS.RetrievePolicyHighRiskFund(Me.PolicyNo, dtHighRiskFund, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            Return
        End If

        ' get logo
        Dim dtCompanyLogo As New DataTable
        If Not Me.GetCompanyLogo(dtCompanyLogo, strCIWConn, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            Return
        End If
        dtCompanyLogo.TableName = "Company_Logo"

        dsLetter.Tables.Add(dtPolicy)
        dsLetter.Tables.Add(dtHighRiskFund)
        dsLetter.Tables.Add(dtCompanyLogo)

        'dsLetter.WriteXmlSchema("C:\PostSalesCalliLAS.xsd")

        rpt.SetDataSource(dsLetter)

        blnCancel = False
    End Sub

    Public Sub PostSalesCallLetter()
        Dim clsCRS As LifeClientInterfaceComponent.clsCRS
        Dim callType As LifeClientInterfaceComponent.CRSWS.PostSalesCallType
        Dim strErr As String = ""

        clsCRS = New LifeClientInterfaceComponent.clsCRS()
        clsCRS.DBHeader = gobjDBHeader
        'If Not clsCRS.GetPolicyPostSalesCallType(Me.PolicyNo, callType, strErr) Then
        If Not GetPolicyPostSalesCallType(Me.PolicyNo, callType, strErr, getCompanyCode(g_Comp)) Then
            MsgBox("FNA form not found.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        If (callType And LifeClientInterfaceComponent.CRSWS.PostSalesCallType.iLAS) = LifeClientInterfaceComponent.CRSWS.PostSalesCallType.iLAS Then
            rpt.Load(System.IO.Path.Combine(My.Application.Info.DirectoryPath, "report\PostSalesCalliLAS.rpt"))
            PostSalesCalliLAS()
        Else
            rpt.Load(System.IO.Path.Combine(My.Application.Info.DirectoryPath, "report\PostSalesCallVC.rpt"))
            PostSalesCallVC()
        End If

    End Sub

    Public Function GetInsuredId() As Integer
        ' upload to CM
        Dim strSql As String = "select * from CoverageDetail where PolicyAccountID='" & Me.PolicyNo & "' and Trailer=1"
        Dim dt As New DataTable()

        Me.ExcecuteSQL(strSql, strCIWConn, dt)

        If dt.Rows.Count = 0 Then
            Throw New Exception("CoverageDetail not found.")
        End If

        Return dt.Rows(0)("CustomerID")

    End Function

    Public Function GetMucInsuredId() As Integer
        ' upload to CM
        Dim strSql As String = "select * from CoverageDetail where PolicyAccountID='" & Me.PolicyNo & "' and Trailer=1"
        Dim dt As New DataTable()

        Me.ExcecuteSQL(strSql, strCIWMcuConn, dt)

        If dt.Rows.Count = 0 Then
            Throw New Exception("CoverageDetail not found.")
        End If

        Return dt.Rows(0)("CustomerID")

    End Function

#Region "Post-sales call list"

    Public Sub PostSalesCallList(ByVal printFromDate As DateTime, ByVal printToDate As DateTime, ByVal strFileName As String, Optional ByVal bMC As Boolean = False)
        Dim strSQL As String = String.Empty
        Dim strErr As String = String.Empty
        Dim dtCallList As New DataTable("Call List")
        Dim datInforceDate As DateTime

        blnCancel = True

        Dim strConn As String = strCIWConn
        Dim MacauServiceBL As New MacauServiceBL
        If bMC Then
            strConn = MacauServiceBL.SetMCUCIWSQLConn()
        End If

        Using cnn As New SqlConnection(strConn), cmd As New SqlCommand("cswsp_GetPostSalesCallList", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@policyNo", SqlDbType.VarChar, 16).Value = DBNull.Value
            cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = printFromDate.Date
            cmd.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = printToDate.Date.AddDays(1).AddMilliseconds(-1)
            cmd.CommandTimeout = 30 * 60    ' 30 minute

            cnn.Open()

            ExcecuteSql(cmd, dtCallList)

            cnn.Close()

        End Using

        'strSQL = String.Format("select * from CSWVW_POSTSALES_CALL where ExhibitInforceDate >= '{0}' and ExhibitInforceDate < dateadd(dd, 1, convert(datetime, '{1}', 112))", _
        '            printFromDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), _
        '            printToDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture))

        'ExcecuteSQL(strSQL, strCIWConn, dtCallList)

        ' fill in inforce date for LifeAsia policy
        If Not bMC Then
            For Each dr As DataRow In dtCallList.Rows
                If dr("CompanyID").ToString().Trim() = "ING" Then
                    If Not EnquireLifeAsiaInforceDate(dr("PolicyAccountID").ToString().TrimEnd(), dr("Trailer"), datInforceDate, strErr) Then
                        ' If (Not EnquireLifeAsiaInforceDate(dr("PolicyAccountID").ToString().TrimEnd(), dr("Trailer"), datInforceDate, strErr)) And (Not bMC) Then
                        MsgBox(strErr, MsgBoxStyle.Critical, "Error")
                        Return
                    End If

                    dr("ExhibitInforceDate") = datInforceDate
                End If
            Next
        End If

        dtCallList.Columns.Remove("CompanyID")
        dtCallList.Columns.Add("FNA", GetType(String))
        dtCallList.Columns.Add("Valid VC/SM", GetType(String))
        dtCallList.Columns.Add("Remarks", GetType(String))

        ' export to Excel
        Dim clsCommon As New LifeClientInterfaceComponent.CommonControl()
        Dim dsExcel As New DataSet()

        dsExcel.Tables.Add(dtCallList)

        If System.IO.File.Exists(strFileName) Then
            Try
                System.IO.File.Delete(strFileName)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                Return
            End Try
        End If

        clsCommon.ExportFileInExcel(strFileName, dsExcel, strErr, True)

    End Sub

    Public Function EnquireLifeAsiaInforceDate(ByVal strPolicyNo As String, ByVal intTrailer As Integer, ByRef inforceDate As DateTime, ByRef strErr As String) As Boolean
        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS()
        Dim dsCompDetail As New DataSet()
        Dim strLifeNo, strCoverageNo, strRiderNo As String
        Dim dtCNVPPF As New DataTable()
        Dim strSQL As String = String.Format("select * from CSW_CNVPPF where CHDRNUM='{0}' and TRAILER={1}", strPolicyNo, intTrailer)

        clsPOS.MQQueuesHeader = gobjMQQueHeader
        clsPOS.DBHeader = gobjDBHeader

        ExcecuteSQL(strSQL, strCIWConn, dtCNVPPF)

        If dtCNVPPF.Rows.Count = 0 Then
            strErr = "No record in CSW_CNVPPF: " & strPolicyNo & "-" & intTrailer
            Return False
        End If

        strLifeNo = dtCNVPPF.Rows(0)("LIFE")
        strCoverageNo = dtCNVPPF.Rows(0)("COVERAGE")
        strRiderNo = dtCNVPPF.Rows(0)("RIDER")

        If Not clsPOS.GetComponentDetail(strPolicyNo, strLifeNo, strCoverageNo, strRiderNo, dsCompDetail, strErr) Then
            MsgBox("Failed to get component detail, policy " & strPolicyNo & ", Life " & strLifeNo & ", Coverage " & strCoverageNo & ", Rider " & strRiderNo & vbNewLine & strErr, MsgBoxStyle.Critical, "Error")
            Return False
        End If

        If dsCompDetail IsNot Nothing AndAlso dsCompDetail.Tables.Count > 0 AndAlso dsCompDetail.Tables(0).Rows.Count > 0 Then
            inforceDate = dsCompDetail.Tables(0).Rows(0)("FirstInforceDate")
        Else
            strErr = "Failed to get component details, returned dataset is empty."
        End If

        Return True
    End Function


#End Region

#Region "SMS Effective Repor"
    Public Function SMS_Effective_Rpt()
        Dim strSQL1, strSQL2, strFolderPath, strFilePath, strError As String
        Dim dtResult1, dtResult2 As DataTable
        Dim ds As DataSet = New DataSet()
        Dim frmParam As New frmPremCallRpt

        blnCancel = True

        frmParam.CheckBox1.Visible = False
        frmParam.TextBox1.Visible = False
        frmParam.Text = "SMS Effective Report"
        frmParam.Label1.Text = "Please input the SMS delivery period:"
        frmParam.txtPath.Text = "C:\Reports"

        ''set to last month
        'frmParam.dtP1From.Value = DateAdd(DateInterval.Day, -frmParam.dtP1From.Value.Day + 1, frmParam.dtP1From.Value)
        'frmParam.dtP1From.Value = DateAdd(DateInterval.Month, -1, frmParam.dtP1From.Value)        
        'frmParam.dtP1To.Value = DateAdd(DateInterval.Day, -frmParam.dtP1To.Value.Day, frmParam.dtP1To.Value)
        frmParam.dtP1From.Value = DateAdd(DateInterval.Day, -8, Date.Today())
        frmParam.dtP1To.Value = DateAdd(DateInterval.Day, -8, Date.Today())

        If frmParam.ShowDialog() = DialogResult.OK Then

            'frmParam.dtP1From.Value = DateAdd(DateInterval.Day, -frmParam.dtP1From.Value.Day + 1, frmParam.dtP1From.Value)
            'frmParam.dtP1To.Value = DateAdd(DateInterval.Month, 1, frmParam.dtP1To.Value)
            'frmParam.dtP1To.Value = DateAdd(DateInterval.Day, -frmParam.dtP1To.Value.Day, frmParam.dtP1To.Value)


            If DateDiff(DateInterval.Day, frmParam.dtP1To.Value, Date.Today()) <= 7 Then
                'frmParam.dtP1To.Value = DateAdd(DateInterval.Day, -7, Date.Today())
                MsgBox("Please select SMS delivery period before " + DateAdd(DateInterval.Day, -7, Date.Today()).ToString("yyyy-MM-dd"), MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                Exit Function
            End If

            ' Validation
            If DateDiff(DateInterval.Day, frmParam.dtP1From.Value, frmParam.dtP1To.Value) < 0 Then
                MsgBox("Invalid Date range", MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                Exit Function
            End If

            If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, Date.Today()) > 37 Then
                MsgBox("Support 3 years back-date report generation only", MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                Exit Function
            End If



            wndMain.Cursor = Cursors.WaitCursor

            ' Prepare Data
            strSQL1 = "with temp as " &
                    "( " &
                    "	select " &
                    "	case when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=7 then 'Payment in 7 days' else 'No payment in 7 days' end 'DayDiff', " &
                    "	rptSMSEff_SMSType SMStype, " &
                    "	Count(*) cnt " &
                    "	from " & gcPOS & "vw_rpt_SMS_Effective " &
                    "	where rptSMSEff_SMSDate >= '" & Format(frmParam.dtP1From.Value, "yyyy-MM-dd") & "' and rptSMSEff_SMSDate <= '" & Format(frmParam.dtP1To.Value, "yyyy-MM-dd") & "' " &
                    "   and Day(rptSMSEff_RptDate) <= 10 " &
                    "	Group by case when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=7 then 'Payment in 7 days' else 'No payment in 7 days' end, rptSMSEff_SMSType " &
                    ") " &
                    " " &
                    "select DayDiff 'Filter last notification', sum([1st SMS]) '1st SMS', sum([2nd SMS]) '2nd SMS', sum([APL]) 'APL' " &
                    "from ( " &
                    "	select DayDiff, isnull([PremNote7Day],0) '1st SMS', isnull([PremRmdr30Day],0) '2nd SMS', isnull([APL],0) 'APL' " &
                    "	from temp " &
                    "	pivot ( " &
                    "		sum(cnt) for SMSType in ([PremNote7Day],[PremRmdr30Day],[APL]) " &
                    "	) pot " &
                    "	union " &
                    "	select 'Payment in 7 days', 0, 0, 0 " &
                    "	union " &
                    "	select 'No payment in 7 days', 0, 0, 0 " &
                    ") SMS_Eff_Rpt " &
                    "group by DayDiff " &
                    "order by case when DayDiff like 'No payment in % days' then 2 else 1 end, DayDiff; "


            strSQL2 &= "with temp as " &
                    "( " &
                    "	select " &
                    "	case when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=7 then 'Payment in 7 days' when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=14 then 'Payment in 8-14 days' else 'No payment in 14 days' end 'DayDiff', " &
                    "	rptSMSEff_SMSType SMStype, " &
                    "	Count(*) cnt " &
                    "	from " & gcPOS & "vw_rpt_SMS_Effective " &
                    "	where rptSMSEff_SMSDate >= '" & Format(frmParam.dtP1From.Value, "yyyy-MM-dd") & "' and rptSMSEff_SMSDate <= '" & Format(frmParam.dtP1To.Value, "yyyy-MM-dd") & "' " &
                    "    and Day(rptSMSEff_RptDate) > 10 " &
                    "	Group by case when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=7 then 'Payment in 7 days' when datediff(day,rptSMSEff_SMSDate,isnull(rptSMSEff_PaymentDate,'2999-12-31'))<=14 then 'Payment in 8-14 days' else 'No payment in 14 days' end, rptSMSEff_SMSType " &
                    ") " &
                    " " &
                    "select DayDiff 'Filter last notification', sum([1st SMS]) '1st SMS', sum([2nd SMS]) '2nd SMS', sum([APL]) 'APL' " &
                    "from ( " &
                    "	select DayDiff, isnull([PremNote7Day],0) '1st SMS', isnull([PremRmdr30Day],0) '2nd SMS', isnull([APL],0) 'APL' " &
                    "	from temp " &
                    "	pivot ( " &
                    "		sum(cnt) for SMSType in ([PremNote7Day],[PremRmdr30Day],[APL]) " &
                    "	) pot " &
                    "	union " &
                    "	select 'Payment in 7 days', 0, 0, 0 " &
                    "	union " &
                    "	select 'Payment in 8-14 days', 0, 0, 0 " &
                    "	union " &
                    "	select 'No payment in 14 days', 0, 0, 0 " &
                    ") SMS_Eff_Rpt " &
                    "group by DayDiff " &
                    "order by case when DayDiff like 'No payment in % days' then 2 else 1 end, DayDiff; "


            If GetDT(strSQL1, strCIWConn, dtResult1, strError) AndAlso GetDT(strSQL2, strCIWConn, dtResult2, strError) Then
                dtResult1.TableName = "Generated on 8th"
                dtResult2.TableName = "Generated on 15th"


                'prepare Excel param
                Dim clsCommon As New LifeClientInterfaceComponent.CommonControl()
                Dim dsExcel As New DataSet()


                dsExcel.Tables.Add(dtResult1)
                dsExcel.Tables.Add(dtResult2)


                strFolderPath = frmParam.txtPath.Text
                strFilePath = strFolderPath & "\Payment_SMS_Eff_Rpt_" & Format(Today, "yyyyMMdd") & ".xlsx"

                'prepare folder for exportation
                Try
                    If Not System.IO.Directory.Exists(strFolderPath) Then
                        System.IO.Directory.CreateDirectory(strFolderPath)
                    End If
                    If System.IO.File.Exists(strFilePath) Then
                        System.IO.File.Delete(strFilePath)
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Function
                End Try

                'export to excel
                If Not SMS_Effective_Rpt_Export_Excel(strFilePath, dsExcel, frmParam.dtP1From.Value, frmParam.dtP1To.Value, strError, True) Then
                    MsgBox(strError, MsgBoxStyle.Critical, "Error")
                End If



                '' export to CSV
                'If Not ExportCSV(frmParam.txtPath.Text & "\FCRRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                'End If

                'MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)


            Else
                MsgBox(strError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default


        Else
            frmParam.Dispose()
            Exit Function
        End If



    End Function

    Private Function SMS_Effective_Rpt_Export_Excel(ByVal strPath As String, ByRef dsExport As DataSet, ByVal dtFrom As Date, ByVal dtTo As Date, ByRef strError As String, Optional ByVal IsShowExcel As Boolean = False) As Boolean

        Dim xlApp As New Excel.Application
        'xlApp.SheetsInNewWorkbook = 1
        'xlApp.DisplayAlerts = False 'KT20150819 default is true
        Dim xlWBook As Excel.Workbook '= xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)
        Dim xlWSheet As Excel.Worksheet '= CType(xlWBook.Worksheets(1), Excel.Worksheet)
        Dim objRange As Excel.Range
        Dim dtCurrent As DataTable


        Try
            xlWBook = xlApp.Workbooks.Open("c:\prodapps\la dll\Reports\Payment_SMS_Eff_Rpt.xlsx")
            xlApp.Visible = True

            dtCurrent = dsExport.Tables(0).Copy
            xlWSheet = xlWBook.Worksheets(1)
            xlWSheet.Activate()

            xlApp.Cells(2, 2).Value = Format(dtFrom, "yyyy-MM-dd")
            xlApp.Cells(2, 3).Value = Format(dtTo, "yyyy-MM-dd")

            'Payment in 7 days
            xlApp.Cells(9, 2).Value = dtCurrent.Rows(0).Item(1)
            xlApp.Cells(9, 4).Value = dtCurrent.Rows(0).Item(2)
            xlApp.Cells(9, 6).Value = dtCurrent.Rows(0).Item(3)

            'No Payment in 7 days
            xlApp.Cells(10, 2).Value = dtCurrent.Rows(1).Item(1)
            xlApp.Cells(10, 4).Value = dtCurrent.Rows(1).Item(2)
            xlApp.Cells(10, 6).Value = dtCurrent.Rows(1).Item(3)


            xlWSheet = xlWBook.Worksheets(2)
            xlWSheet.Activate()
            dtCurrent = dsExport.Tables(1).Copy

            xlApp.Cells(2, 2).Value = Format(dtFrom, "yyyy-MM-dd")
            xlApp.Cells(2, 3).Value = Format(dtTo, "yyyy-MM-dd")

            'Payment in 7 days
            xlApp.Cells(9, 2).Value = dtCurrent.Rows(0).Item(1)
            xlApp.Cells(9, 4).Value = dtCurrent.Rows(0).Item(2)
            xlApp.Cells(9, 6).Value = dtCurrent.Rows(0).Item(3)

            'Payment in 14 days
            xlApp.Cells(10, 2).Value = dtCurrent.Rows(1).Item(1)
            xlApp.Cells(10, 4).Value = dtCurrent.Rows(1).Item(2)
            xlApp.Cells(10, 6).Value = dtCurrent.Rows(1).Item(3)

            'No Payment in 14 days
            xlApp.Cells(11, 2).Value = dtCurrent.Rows(2).Item(1)
            xlApp.Cells(11, 4).Value = dtCurrent.Rows(2).Item(2)
            xlApp.Cells(11, 6).Value = dtCurrent.Rows(2).Item(3)

            'For i As Integer = 0 To dsExport.Tables.Count - 1

            '    dtCurrent = dsExport.Tables(i).Copy
            '    xlWSheet.Name = dtCurrent.TableName

            '    For j As Integer = 0 To dtCurrent.Columns.Count - 1
            '        xlApp.Cells(1, j + 1).Value = dtCurrent.Columns(j).Caption
            '        xlApp.Cells(1, j + 1).Interior.ColorIndex = 33
            '        ' 2010/02/03 Added by Edward
            '        If blnAutoPercent Then
            '            If dtCurrent.Columns(j).Caption.Contains("%") Then
            '                xlApp.Cells(1, j + 1).EntireColumn.style = "Percent"
            '            End If
            '        End If
            '        ' 2010/02/03
            '    Next

            '    objRange = xlWSheet.Cells
            '    WriteData(dtCurrent, objRange)
            '    xlWSheet.Range("A1", "Z1").EntireColumn.AutoFit()
            '    'xlWSheet.Range("A2").CopyFromRecordset(dtCurrent)
            '    xlWSheet.Cells(1, 1).CurrentRegion.EntireColumn.AutoFit()

            '    If i < dsExport.Tables.Count - 1 Then
            '        xlWSheet = xlWBook.Worksheets.Add(After:=xlWBook.Sheets(xlApp.ActiveWorkbook.Sheets.Count))
            '    End If
            'Next

            'KT20150819 prevent popup asking to replace file and eventually timeout
            'If File.Exists(strPath) Then
            '    File.Delete(strPath)
            'End If
            'KT20150819
            xlWSheet.Calculate()
            xlWSheet = xlWBook.Worksheets(1)
            xlWSheet.Activate()
            xlWSheet.SaveAs(strPath)

            If IsShowExcel Then
                xlApp.Visible = True
            End If

            Return True

        Catch ex As Exception
            strError = ex.ToString

        Finally
            Try
                CloseAndReleaseExcelObject(IsShowExcel, xlApp, xlWSheet, xlWBook)

                'END - 2012/01/06
            Catch ex As Exception

            End Try
        End Try
    End Function

    Public Sub CloseAndReleaseExcelObject(ByVal IsShowExcel As Boolean, ByRef xlApp As Excel.Application, ByRef xlSheet As Excel.Worksheet, ByRef xlBook As Excel.Workbook)
        If Not IsShowExcel Then
            xlBook.Close()
            xlApp.Quit()
            xlApp.SheetsInNewWorkbook = 3
        End If

        ' Release the objects.
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)
        xlSheet = Nothing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook)
        xlBook = Nothing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
        xlApp = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

#End Region

#Region "APL Loan Letter"

    'this function is cloned from payment report
    Public Sub AplLoanLetter()
        'ds is the dataset finally will be passed into Crystal Report
        'ds1 is the dataset that read from .xsd schema, an intermediate dataset
        Dim ds As New DataSet("dsLoanHistory")
        Dim ds1 As New DataSet
        Dim dr As DataRow
        ds1.ReadXmlSchema(My.Application.Info.DirectoryPath & "\aplloanletter.xsd") '"C:\tfs\Life_Common\IPD\LIB"
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim strContractCurrency As String
        Dim sqlda As SqlDataAdapter
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
        If My.Settings.LAReady = True Then
            clsPOS.MQQueuesHeader = GetMQQueHeader()
            clsPOS.DBHeader = GetMComHeader()

            dsPolicyCurr.Tables.Clear()

            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            'MsgBox(If(dsPolicyCurr Is Nothing, "1. dsPolicyCurr Is Nothing", "1. dsPolicyCurr Is Not Nothing"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
            'MsgBox(If(dsPolicyCurr.Tables Is Nothing, "1. dsPolicyCurr.Tables Is Nothing", "1. dsPolicyCurr.Tables Is Not Nothing"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
            'MsgBox("1. dsPolicyCurr.Tables.Count : " & dsPolicyCurr.Tables.Count, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")

            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    'MsgBox("1. dsPolicyCurr.Tables(0).Rows.Count : " & dsPolicyCurr.Tables(0).Rows.Count, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
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

        If Not dtPoList Is Nothing Then
            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY").ToString.Trim, 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                Next
            End If
        End If

        'Prepare input for loan and APL history
        dtSendData = New DataTable
        dsPolicySend.Tables.RemoveAt(0)
        If dsPolicyCurr.Tables.Count > 0 Then
            dsPolicyCurr.Tables.RemoveAt(0)
        End If
        dtSendData.Columns.Add("Policy_No")
        dtSendData.Columns.Add("FromDate")
        dtSendData.Columns.Add("ToDate")
        dr = dtSendData.NewRow
        dtSendData.Rows.Add(dr)
        dtSendData.Rows(0)("Policy_No") = RTrim(strPolicy)
        dtSendData.Rows(0)("FromDate") = datFrom
        dtSendData.Rows(0)("ToDate") = datEnd
        dsPolicySend.Tables.Add(dtSendData)

        'MsgBox("Before GetLoanAndAPLHistory", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        'Call LifeAsia BO APLTRN to get loan and APL history
        blnGetPolicy = clsPOS.GetLoanAndAPLHistory(RTrim(strPolicy), datEnd, datFrom, dsPolicyCurr, strerr)
        'MsgBox(If(dsPolicyCurr Is Nothing, "2. dsPolicyCurr Is Nothing", "2. dsPolicyCurr Is Not Nothing"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        'MsgBox(If(dsPolicyCurr.Tables Is Nothing, "2. dsPolicyCurr.Tables Is Nothing", "2. dsPolicyCurr.Tables Is Not Nothing"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        'MsgBox("2. dsPolicyCurr.Tables.Count : " & dsPolicyCurr.Tables.Count, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        If dsPolicyCurr.Tables.Count > 0 Then
            'MsgBox("2. dsPolicyCurr.Tables(0).Rows.Count : " & dsPolicyCurr.Tables(0).Rows.Count, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
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

        If dtLoan_All Is Nothing Then
            MsgBox("No APL / Loan History record found - 1", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        If dtLoan_All.Rows.Count = 0 Then
            MsgBox("No APL / Loan History record found - 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            ds.Tables.Add(dtLoan_All)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        strSQL = "select * from cswvw_cam_Agent_info where cswagi_agent_code = '" & strAG & "'; "

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(ds, "cswvw_cam_Agent_info")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        strSQL = "SELECT '" & strPolicy & "' as Policy, ING_Logo = a.cswilt_Logo, Address = b.cswilt_Logo, IService = c.cswilt_Logo, ICaring = d.cswilt_Logo " &
               "FROM (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'ING Logo BW') a " &
               ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Address') b " &
               ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer ING Interactive Service') c " &
               ", (select * from " & gcPOS & "vw_csw_ing_logo_table where cswilt_LogoDesc = 'Footer Caring Company') d "

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        'put the strsql into the dataset as a specified name, if more than 1 table, the second will be name as "LOGO2"
        Try
            sqlda.Fill(ds, "LOGO")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

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

        strSQL = "Select cph.nameprefix as ph_nameprefix, cph.firstname as ph_firstname, cph.namesuffix as ph_namesuffix, " &
                " cpi.firstname as pi_firstname, cpi.namesuffix as pi_namesuffix, policycurrency, paidtodate, " &
                " csa.firstname as sa_firstname, csa.namesuffix as sa_namesuffix, a.phonenumber, a.locationcode, cph.customerid " &
                " from csw_poli_rel rph, customer cph, coveragedetail rpi, customer cpi, csw_poli_rel rsa, customer csa, agentcodes a, policyaccount p " &
                " where rph.policyaccountid = '" & strPolicy & "' " &
                " and rph.policyaccountid = rpi.policyaccountid and rph.policyaccountid = p.policyaccountid " &
                " and rph.policyrelatecode = 'PH' and rph.customerid = cph.customerid " &
                " and rpi.customerid = cpi.customerid and rpi.trailer=1" &
                " and rph.policyaccountid = rsa.policyaccountid " &
                " and rsa.policyrelatecode = 'SA' and rsa.customerid = csa.customerid and csa.agentcode = a.agentcode"

        If GetDT(strSQL, strCIWConn, dtSQLResult, strErrMsg) Then
            strInsName_Disp = Trim(dtSQLResult.Rows(0).Item("pi_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("pi_FirstName"))
            strAgtName = Trim(dtSQLResult.Rows(0).Item("sa_NameSuffix")) & " " & Trim(dtSQLResult.Rows(0).Item("sa_FirstName"))
            strAgPhone = Trim(dtSQLResult.Rows(0).Item("PhoneNumber"))
            strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)
        End If

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

        strSQL = "SELECT cswpad_poli_id, cswpad_addr_type, cswpad_add1, cswpad_add2, cswpad_add3, cswpad_city, " &
                "cswpad_tel1, cswpad_tel2,	cswpad_fax1, cswpad_fax2, timestamp, cswpad_country, cswpad_post_code " &
                "from csw_policy_address where cswpad_poli_id = '" & strPolicy & "'"

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(ds, "csw_policy_address")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        sqlda.Dispose()
        sqlconnect.Close()

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

#Region "001"

    Public Sub PSCFollowLetter()
        Try

            PrintLetter("PSC follow letter")

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Public Sub PaymentLetter()
        Try

            PrintLetter("Payment letter")

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Public Sub PolicyLetter()
        Try

            PrintLetter("Policy letter")

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    'oliver 2024-3-7 added for NUT-3813
    Private Function GetConnectionStringForPrintLetter(ByVal policyNo As String, ByRef strCompanyID As String) As String
        Dim SingleQuoteFormatted = String.Format("'{0}'", Trim(policyNo))
        Dim sqldt As New DataTable
        Dim lngCnt = gSearchLimit
        Dim strErr As String = ""
        Dim lngErr As Long = 0
        Dim isGetCountOnly As Boolean = False
        'sqldt = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErr, strErr, lngCnt, isGetCountOnly, "ING", "=")
        GetPolicyListByAPI("ING", SingleQuoteFormatted, "", "PH", "POLST", "=", lngErr, strErr, lngCnt, sqldt, True)
        'Asurrance Ph3 Union Search result in datatable
        Dim sqldtAsur As New DataTable
        Dim lngCntAsur = gSearchLimit
        Dim strErrAsur As String = ""
        Dim lngErrAsur As Long = 0
        'sqldtAsur = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErrAsur, strErrAsur, lngCntAsur, isGetCountOnly, "LAC", "=")
        GetPolicyListByAPI("LAC", SingleQuoteFormatted, "", "PH", "POLST", "=", lngErrAsur, strErrAsur, lngCntAsur, sqldtAsur, True)

        Dim isBermuda As Boolean = False
        Dim isAssurance As Boolean = False

        If Not sqldt Is Nothing Then
            If sqldt.Rows.Count > 0 Then
                isBermuda = True
            End If
            'MsgBox("sqldt : " & sqldt.Rows.Count , MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        End If

        If Not sqldtAsur Is Nothing Then
            If sqldtAsur.Rows.Count > 0 Then
                isAssurance = True
            End If
            'MsgBox("sqldtAsur : " & sqldtAsur.Rows.Count , MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Checking")
        End If

        If isBermuda Then
            If Not IsDBNull(sqldt.Rows(0)("CompanyID")) AndAlso Not String.IsNullOrEmpty(sqldt.Rows(0)("CompanyID").ToString) Then
                strCompanyID = sqldt.Rows(0)("CompanyID").ToString
                Return GetConnectionStringByCompanyID(sqldt.Rows(0)("CompanyID").ToString)
            End If
        End If

        If isAssurance Then
            If Not IsDBNull(sqldtAsur.Rows(0)("CompanyID")) AndAlso Not String.IsNullOrEmpty(sqldtAsur.Rows(0)("CompanyID").ToString) Then
                strCompanyID = sqldtAsur.Rows(0)("CompanyID").ToString
                Return GetConnectionStringByCompanyID(sqldtAsur.Rows(0)("CompanyID").ToString)
            End If
        End If

        strCompanyID = "ING"
        Return GetConnectionStringByCompanyID("ING")
    End Function

    Public Sub PrintLetter(ByVal letterType As String)
        blnCancel = True

        '1.Pop-up frmLtrParam box
        Dim frmInput As frmLtrParam = GetfrmLtrParam(letterType)
        Select Case letterType
            Case "PSC follow letter"
                frmInput.grpPrint.Enabled = False
            Case "Payment letter"
                frmInput.txtFrom.Enabled = True
                frmInput.txtTo.Enabled = True
            Case "Policy letter"

        End Select
        If Destination = PrintDest.pdExport Then
            frmInput.grpExport.Enabled = True
        End If
        frmInput.ShowDialog()
        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        '2.get data
        Dim dsRegion As New DataSet
        Dim dtField As New DataTable
        Dim policyNo As String = frmInput.PolicyNo
        If String.IsNullOrWhiteSpace(policyNo) Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        'oliver 2024-3-7 added for NUT-3813
        Dim strCompanyID As String = ""
        Dim strCIWConn As String = GetConnectionStringForPrintLetter(policyNo, strCompanyID)

        Dim ltrCode As String = String.Empty
        Select Case letterType
            Case "PSC follow letter"
                'oliver 2024-3-13 updated for NUT-3813
                dtField = GetPSCFollowLetterData(policyNo, dsRegion, strCIWConn)
                ltrCode = "PSCFollowHK"
            Case "Payment letter"
                Dim fromDate As Date = frmInput.FromDate
                Dim toDate As Date = frmInput.ToDate
                'oliver 2024-3-13 updated for NUT-3813
                dtField = GetPaymentLetterData(policyNo, frmInput.radChi.Checked, fromDate, toDate, dsRegion, strCIWConn, strCompanyID)
                ltrCode = If(frmInput.radChi.Checked, "PaymentChi", "PaymentEng")
            Case "Policy letter"
                'oliver 2024-3-13 updated for NUT-3813
                dtField = GetPolicyLetterData(policyNo, dsRegion, frmInput.radChi.Checked, strCIWConn)
                ltrCode = If(frmInput.radChi.Checked, "PolicyChi", "PolicyEng")
        End Select

        '3.prepare data to be inserted into the CCM DB
        Dim request As CCMWS.LetterRequest = SetupLetterRequest(ltrCode, "O", "HK", strCompanyID, policyNo)
        Dim fieldList As CCMWS.LetterField() = DataMapToLetterField(dtField)
        Dim regionSet As CCMWS.LetterRegion()() = DataMapToLetterRegion(dsRegion)

        '4.insert data to CCMS DB
        Dim insertResult As CCMWS.BaseResponseOfLetterInfo = CCMWebService.InsertLetterData(request, fieldList, regionSet)
        If insertResult.IsSuccess = False Then
            MsgBox(String.Format("Insert data to CCMS DB error: {0}", insertResult.ErrorMsg), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        '5.generate report and upolad it to CM/ECM
        Dim generateInput As CCMWS.LetterGenerateInput = New CCMWS.LetterGenerateInput()
        generateInput.LtrRequestId = insertResult.Value.LtrRequestId
        generateInput.DocType = CCMWS.DocTypeEnum.LFCEDoc
        generateInput.CMEvn = g_Comp
        generateInput.IsUploadFile = If(Destination = PrintDest.pdPrinter, True, False)
        generateInput.FileFormat = If(frmInput.radPdf.Checked = True, CCMWS.FileFormat.pdf, If(frmInput.radWord.Checked = True, CCMWS.FileFormat.docx, CCMWS.FileFormat.xlsx))
        Dim generateResult As CCMWS.BaseResponseOfLetterGenerateInfo = CCMWebService.GenerateFile(generateInput)

        If generateResult.IsSuccess = False Then
            MsgBox(String.Format("Generate report error: {0}", generateResult.ErrorMsg), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        outputFilePath = WriteByteToFile(generateResult.Value.fileByte, generateResult.Value.outputFileName)

        blnCancel = False

    End Sub

    'oliver 2024-3-13 updated for NUT-3813
    Private Function GetPSCFollowLetterData(ByVal policyNo As String, ByRef dsRegion As DataSet, ByVal strCIWConn As String) As DataTable

        Dim dtFields As DataTable = GetPSCFollowLetterFields()
        Dim dtCommon As DataTable = GetCommonField(policyNo, strCIWConn)
        dtFields.Rows.Add(dtFields.NewRow())
        dtFields.Rows(0)("PolicyNo") = policyNo
        dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateEng").ToString()
        dtFields.Rows(0)("ContactAddress1") = dtCommon.Rows(0)("ContactAddress1").ToString()
        dtFields.Rows(0)("ContactAddress2") = dtCommon.Rows(0)("ContactAddress2").ToString()
        dtFields.Rows(0)("ContactAddress3") = dtCommon.Rows(0)("ContactAddress3").ToString()
        dtFields.Rows(0)("ContactAddress4") = dtCommon.Rows(0)("ContactAddress4").ToString()
        dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
        dtFields.Rows(0)("AgentNameEng") = dtCommon.Rows(0)("AgentNameEng").ToString()
        dtFields.Rows(0)("AgentNameChi") = dtCommon.Rows(0)("AgentNameChi").ToString()
        dtFields.Rows(0)("AgentLocation") = dtCommon.Rows(0)("AgentLocation").ToString()

        'get cooling off date -s
        Dim dtContractDtl As DataTable = GetContractDetail(policyNo, g_Comp)
        Dim coolingOffDate As String = dtContractDtl.Rows(0)("CooloffDate")
        If Not String.IsNullOrWhiteSpace(coolingOffDate) Then
            dtFields.Rows(0)("CoolingOffDateEng") = DateTime.ParseExact(coolingOffDate, "MM/dd/yyyy", Nothing).ToString("dd MMM yyyy")
            dtFields.Rows(0)("CoolingOffDateChi") = DateTime.ParseExact(coolingOffDate, "MM/dd/yyyy", Nothing).ToString("yyyy年M月d日")
        End If
        'get cooling off date -e

        'get plan name -s
        Dim dtPolicyAccount As DataTable = GetPolicyAccountInfo(policyNo, strCIWConn)
        Dim productId As String = dtPolicyAccount.Rows(0)("ProductID").ToString.Trim()
        dtFields.Rows(0)("PlanNameEng") = GetPlanName(productId, strCIWConn)
        dtFields.Rows(0)("PlanNameChi") = GetPlanNameChi(productId, strCIWConn)
        'get plan name -e

        'get payment mode -s
        Dim mode As String = dtPolicyAccount.Rows(0)("Mode").ToString().Trim()
        Dim paymentMode As String = GetPaymentMode(mode, strCIWConn)
        dtFields.Rows(0)("PayModeEng") = paymentMode
        dtFields.Rows(0)("PayModeChi") = GetPaymentModeChi(paymentMode)
        'get payment mode -e

        'get event detail -s
        'oliver updated 2024-6-27 for Assurance Production Version hot fix
        Dim dtServiceLog As DataTable = GetServiceLogInfo(policyNo, strCIWConn)
        Dim medium As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("EventSourceMedium").ToString().Trim())
        Dim eventCategory As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("cswecc_desc").ToString().Trim())
        Dim eventDetail As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("EventTypeDesc").ToString().Trim())
        'Dim eventTypeDetail As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("csw_event_typedtl_desc").ToString().Trim())
        'get event detail -e


        'setup PSC_content -s
        Dim pscContentEng As String = String.Empty
        Dim pscContentChi As String = String.Empty
        '(Event Category: Courtesy Call - Vulnerable customer; Event Detail: Uncertain) or (Event Category: Courtesy Call - Suitability mismatch)
        If ("Courtesy Call - Vulnerable customer".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) And "Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase)) Or
            "Courtesy Call – Suitability mismatch".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) Then

            pscContentEng = "As required by the Insurance Authority (""IA"") to protect your rights, we are required to conduct a post-sale call with you after the issuance of the Policy. " &
                "As such, we have contacted you at your provided contact numbers """ & dtCommon.Rows(0)("OwnerPhone").ToString() & """ on the policy application form. " &
                "During the call, you indicated that you have queries on some of the matters related to the policy. " &
                "We have therefore informed our insurance agent/the broker (the ""Sales Representative"") to follow up with you for clarification." & vbCrLf & vbCrLf &
                "A Policy contract will be/has been delivered to you by our insurance agent/the broker (""the Sales Representative""). " &
                "Please read it carefully once you have received it because it contains important information and facts which you should know."

            pscContentChi = "根據保險業監管局的指引，為保障您的權益，我們需要於上述保單繕發後，致電給您進行售後電話跟進服務。" &
                "據此，我們較早前按照您於保單申請書上所提供的聯絡電話：" & dtCommon.Rows(0)("OwnerPhone").ToString() & " 與您聯絡。" &
                "對話當中，我們理解您對上述保單有疑問，我們遂要求我們的銷售代表/經紀(""理財顧問"")與您聯絡，以釐清您的疑問並作出解釋。" & vbCrLf & vbCrLf &
                "我們的銷售代表/經紀(""理財顧問"")將會/已經把您的保單合約送交給您。" &
                "請您收到後小心細閱有關文件的內容，因為當中列明了您必須知道的重要資料及概要。"
        Else
            Dim eventDetailEng As String = "."
            Dim eventDetailChi As String = "。"
            '<<CRS service log, medium=Letter, and, Event detail=Unwilling>>
            '<<CRS service log, medium=Letter, and, Event detail=Uncertain>> 
            If "Letter".Equals(medium, StringComparison.OrdinalIgnoreCase) And "Unwilling".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Then
                eventDetailEng = " on occasions but in vain."
                eventDetailChi = "，惟未能成功與您聯繫。"
            ElseIf "Letter".Equals(medium, StringComparison.OrdinalIgnoreCase) And "Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Then
                eventDetailEng = ". During the call, you indicated that you have queries on some of the matters related to the policy. " &
                    "We have therefore informed our insurance agent/the broker (the ""Sales Representative"") to follow up with you for clarification."
                eventDetailChi = "。對話當中，我們理解您對上述保單有疑問，我們遂要求我們的銷售代表/經紀(""理財顧問"")與您聯絡，以釐清您的疑問並作出解釋。"
            End If

            pscContentEng = "As required by the Insurance Authority (""IA"") to protect your rights, we are required to conduct a post-sale call with you after the issuance of the Policy. " &
                "As such, we have tried to contacted you at your provided contact numbers """ & dtCommon.Rows(0)("OwnerPhone").ToString() & """ on the policy application form" & eventDetailEng & "" & vbCrLf & vbCrLf &
                "A Policy contract will be/has been delivered to you by our insurance agent/the broker (the ""Sales Representative""). " &
                "Please read it carefully once you have received it because it contains important information and facts which you should know."

            pscContentChi = "根據保險業監管局的指引，為保障您的權益，我們需要於上述保單繕發後，致電給您進行售後電話跟進服務。" &
                "據此，我們較早前按照您於保單申請書上所提供的聯絡電話：""" & dtCommon.Rows(0)("OwnerPhone").ToString() & """ 嘗試與您聯絡" & eventDetailChi & "" & vbCrLf & vbCrLf &
                "我們的銷售代表/經紀(""理財顧問"")將會/已經把您的保單合約送交給您。" &
                "請您收到後小心細閱有關文件的內容，因為當中列明了您必須知道的重要資料及概要。"
        End If
        dtFields.Rows(0)("PSCContentEng") = pscContentEng
        dtFields.Rows(0)("PSCContentChi") = pscContentChi
        'setup PSC_content -e

        'setup Medical_plan Elite_term_plan Ex_policy_term -s
        Dim dtProductType As DataTable = GetCIWPRProductTypeInfo(productId, strCIWConn)
        Dim isMedical As String = dtProductType.Rows(0)("CIWPT_IsMedical").ToString().Trim()
        Dim isTerm As String = dtProductType.Rows(0)("CIWPT_IsTerm").ToString().Trim()
        Dim isSinglePremium As String = dtProductType.Rows(0)("CIWPT_IsSinglePremium").ToString().Trim()
        If isMedical = "Y" Then
            dtFields.Rows(0)("Rmk_MedicalPlanEng") = LINEBREAK & "*It is the first year premium payable. Premium rates for each renewal are determined based on the age of the Insured at the next birthday, are not guaranteed are subject to change." & LINEBREAK
            dtFields.Rows(0)("Rmk_MedicalPlanChi") = LINEBREAK & "*此資料只顯示首年保費金額，續期保費率依被保人之下次生日年齡釐定，續期保費率並非保證不變且可能有所更改。" & LINEBREAK
        End If
        If isTerm = "Y" Then
            dtFields.Rows(0)("Rmk_EliteTermPlanEng") = LINEBREAK & "*It is the premium payable for the first 20 policy years. Staring from the 21st policy year, the premium rate will be determined annually at the sole discretion of the Company based on the age of the next birthday of the Insured at the time of renewal." & LINEBREAK
            dtFields.Rows(0)("Rmk_EliteTermPlanChi") = LINEBREAK & "*此資料只顯示首二十個保單年度應繳保費之金額，於第二十一個保單年度開始，每年續期保費將會按照受保人於續期時之下次生日年齡，並由本公司全權酌情訂定。" & LINEBREAK
        End If
        If isSinglePremium = "N" Then
            dtFields.Rows(0)("Rmk_ExPolicyTermEng") = LINEBREAK & "If you do not intend or are unable to pay the premium for the whole Policy term, you should not acquire the Policy as you may suffer a loss if you terminate the Policy early or cease paying premium early." & LINEBREAK
            dtFields.Rows(0)("Rmk_ExPolicyTermChi") = LINEBREAK & "假如您在投保期間不打算或無法支付全期保費，您便不應購買此保單。因為提早終止此保單或提前停止繳付保費，可能令您蒙受損失。" & LINEBREAK
        End If
        'setup Medical_plan Elite_term_plan Ex_policy_term -e

        'setup Sui_title Sui_content -s
        If "Courtesy Call – Suitability mismatch".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) Then
            dtFields.Rows(0)("SuiTitleEng") = "Suitability Mismatch"
            dtFields.Rows(0)("SuiTitleChi") = "不合適配對"
            dtFields.Rows(0)("SuiContentEng") = "It is your intention and desire to proceed with the application despite the product objective(s) of the product(s) selected by you as per below table may not be suitable for your disclosed current needs as indicated in the Financial Needs and Investor Profile Analysis form."
            dtFields.Rows(0)("SuiContentChi") = "經管根據「財務需要及投資取向分析」，您所選擇的以下產品可能並不合適您的選購目標，您仍然選擇投保上述保單。"
        Else
            dtFields.Rows(0)("SuiTitleEng") = "Suitability"
            dtFields.Rows(0)("SuiTitleChi") = "合適配對"
            dtFields.Rows(0)("SuiContentEng") = "Product objective(s) of the product(s) selected by you as per below table are suitable for you based on your disclosed current needs as indicated in the Financial Needs and Investor Profile Analysis form."
            dtFields.Rows(0)("SuiContentChi") = "根據「財務需要及投資取向分析」，您所選擇以下的產品既適合您亦可達至您的選購目標。"
        End If
        'setup Sui_title Sui_content -s

        'setup NGB_content -s
        Dim ngbTitleEng As String = String.Empty
        Dim ngbTitleChi As String = String.Empty
        Dim ngbContentEng As String = String.Empty
        Dim ngbContentChi As String = String.Empty
        '(Event Category: Courtesy Call - Vulnerable customer and Event Detail: Uncertain or Unreachable or Unwilling) or Product type: Life(participating)/Universal life/Crisis(participating)
        'Life(participating) : CIWPT_IsLife
        'Universal life : CIWPT_IsUniversalLife
        'Crisis(participating) : CIWPT_IsCrisis
        Dim isLife As String = dtProductType.Rows(0)("CIWPT_IsLife").ToString().Trim()
        Dim isUniversalLife As String = dtProductType.Rows(0)("CIWPT_IsUniversalLife").ToString().Trim()
        Dim isCrisis As String = dtProductType.Rows(0)("CIWPT_IsCrisis").ToString().Trim()
        Dim productType As String = dtFields.Rows(0)("PlanNameEng")
        If ("Courtesy Call - Vulnerable customer".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) And
            ("Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Or
            "Unreachable".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Or
            "Unwilling".Equals(eventDetail, StringComparison.OrdinalIgnoreCase))) And
            (isLife = "Y" Or isUniversalLife = "Y" Or isCrisis = "Y") Then

            ngbTitleEng = LINEBREAK & "Non-Guaranteed Benefit"
            ngbContentEng = LINEBREAK & "The projected non-guaranteed benefits included in the illustration are based on the Company’s dividend / bonus (if any) scales determined under current assumed investment return and are not guaranteed. " &
                            "The actual amount payable may change at any time with the values being higher or lower than those illustrated. " &
                            "As another example, the possible potential impact of a change in the company’s current assumed investment return on the Total Surrender Benefit and the Total Death Benefit in illustration. Under some circumstances, the non-guaranteed benefits may be zero." & LINEBREAK

            ngbTitleChi = LINEBREAK & "非保證利益"
            ngbContentChi = LINEBREAK & "預計的非保證金額乃根據本公司現時假設投資回報而計算，該金額並非保證。實際獲發之金額或會比所示者較高或較低。" &
                            "建議書內說明因本公司現時假設的投資回報轉變而對退保價值及身故權益可能造成的影響。在某些情況下，非保證金額可能為零。" & LINEBREAK
        End If
        dtFields.Rows(0)("Rmk_NGBTitleEng") = ngbTitleEng
        dtFields.Rows(0)("Rmk_NGBTitleChi") = ngbTitleChi
        dtFields.Rows(0)("Rmk_NGBContentEng") = ngbContentEng
        dtFields.Rows(0)("Rmk_NGBContentChi") = ngbContentChi
        'setup NGB_content -e

        'setup Fee_charge -s
        Dim feeTitleEng As String = String.Empty
        Dim feeTitleChi As String = String.Empty
        Dim feeChargeEng As String = String.Empty
        Dim feeChargeChi As String = String.Empty
        'Product type:  Basic Plus / Basic Plus ll / Basic Plus Junior / Basic Plus Junior ll / i.Ulife Plus / Flexi-Growth / FlexiGrowth Single premium / Easy Plus / For Your Interest
        If productType.StartsWith("Basic Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("i.Ulife Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Flexi-Growth ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Easy Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("For Your Interest ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Policy Fee, Management Charge, Cost of insurance and Withdrawal/Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保單費用，行政管理費，人壽保險費及提款/退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If

        'Product type: Global fortune / Noble fortune / Regal fortune / Universal fortune / Glorious fortune product series
        If productType.StartsWith("Global fortune", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Noble fortune", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Regal fortune ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Universal fortune ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Glorious fortune ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Premium Charge, Administration Charge, Cost of insurance and Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保費費用，行政費用，人壽保險費及退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If

        'Product type: i.Ulife Select / Flexi-Growth Premier product series
        If productType.StartsWith("i.Ulife Select ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Flexi-Growth Premier ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Policy Fee, Administration Charge and Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保費費用，行政費用及提款退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If

        dtFields.Rows(0)("Rmk_FeeTitleEng") = feeTitleEng
        dtFields.Rows(0)("Rmk_FeeTitleChi") = feeTitleChi
        dtFields.Rows(0)("Rmk_FeeChargeEng") = feeChargeEng
        dtFields.Rows(0)("Rmk_FeeChargeChi") = feeChargeChi
        'setup Fee_charge -e

        'region -s
        Dim dtTbl As DataTable = New DataTable("RegionTbl")
        dtTbl.Columns.Add("Tbl_PlanNameEng", GetType(String))
        dtTbl.Columns.Add("Tbl_PlanNameChi", GetType(String))
        dtTbl.Columns.Add("Tbl_CurrencyEng", GetType(String))
        dtTbl.Columns.Add("Tbl_CurrencyChi", GetType(String))
        dtTbl.Columns.Add("Tbl_AmountEng", GetType(String))
        dtTbl.Columns.Add("Tbl_AmountChi", GetType(String))
        dtTbl.Columns.Add("Tbl_PolicyTermEng", GetType(String))
        dtTbl.Columns.Add("Tbl_PolicyTermChi", GetType(String))

        Dim dtObj As DataTable = New DataTable("RegionObj")
        dtObj.Columns.Add("Tsm_PlanNameEng", GetType(String))
        dtObj.Columns.Add("Tsm_PlanNameChi", GetType(String))
        dtObj.Columns.Add("Tsm_ObjectiveEng", GetType(String))
        dtObj.Columns.Add("Tsm_ObjectiveChi", GetType(String))

        Dim dtRisk As DataTable = New DataTable("RegionRisk")
        dtRisk.Columns.Add("Tpr_PlanNameEng", GetType(String))
        dtRisk.Columns.Add("Tpr_PlanNameChi", GetType(String))
        dtRisk.Columns.Add("Tpr_RisksEng", GetType(String))
        dtRisk.Columns.Add("Tpr_RisksChi", GetType(String))

        'LAS logic s
        Dim dtCoverage As DataTable = GetCOSelWithCustNo(policyNo, g_Comp)
        Dim paidToDate As Date = Date.ParseExact(dtContractDtl.Rows(0).Item("Paid_To_Date"), "MM/dd/yyyy", Globalization.CultureInfo.InvariantCulture)
        Dim strFreq As String = Format(Val(dtContractDtl.Rows(0).Item("Freq")), "00")
        Dim dtPremiumRoutine As DataTable = GetPremiumRoutine(policyNo, paidToDate, strFreq, g_Comp)

        For Each dr As DataRow In dtCoverage.Rows
            dtPremiumRoutine.DefaultView.RowFilter = "LIFE = '" & dr("Life") & "' and COVERAGE = '" & dr("Cov") & "' and RIDER = '" & dr("Rider") & "' "

            If dtPremiumRoutine.DefaultView.Count > 0 Then

                Dim booster As Decimal = Val(dtPremiumRoutine.DefaultView(0).Item("ZBINSTPREM"))
                Dim inst As Decimal = Val(dtPremiumRoutine.DefaultView(0).Item("INSTPREM"))
                Dim modalPremium As Decimal
                If inst > 0 Then
                    modalPremium = inst
                Else
                    modalPremium = booster
                End If

                If booster <> 0.00 Then

                    Dim tblRow As DataRow = dtTbl.NewRow()

                    productId = dr("Cov_Code").ToString().Trim()
                    dtProductType = GetCIWPRProductTypeInfo(productId, strCIWMcuConn)
                    isMedical = dtProductType.Rows(0)("CIWPT_IsMedical").ToString().Trim()
                    Dim tblPlanNameEng As String = GetPlanName(productId, strCIWConn)
                    Dim tblPlanNameChi As String = GetPlanNameChi(productId, strCIWConn)

                    tblRow("Tbl_PlanNameEng") = tblPlanNameEng
                    tblRow("Tbl_PlanNameChi") = tblPlanNameChi
                    Dim policyCurrency As String = dtPolicyAccount.Rows(0)("PolicyCurrency").ToString().Trim()
                    tblRow("Tbl_CurrencyEng") = policyCurrency
                    tblRow("Tbl_CurrencyChi") = GetCurrencyChi(policyCurrency)
                    tblRow("Tbl_AmountEng") = GetMoneyFormat(modalPremium) & If(isMedical = "Y", "*", "")
                    tblRow("Tbl_AmountChi") = GetMoneyFormat(modalPremium) & If(isMedical = "Y", "*", "")

                    Dim policyTerm As String = dtPolicyAccount.Rows(0)("PolicyTermDate").ToString().Trim()
                    If Not String.IsNullOrWhiteSpace(policyTerm) Then
                        tblRow("Tbl_PolicyTermEng") = CDate(policyTerm).ToString("dd MMM yyyy") & " years / One-Off Premium"
                        tblRow("Tbl_PolicyTermChi") = CDate(policyTerm).ToString("yyyy年M月d日") & " 年 / 一次過繳付保費"
                    End If
                    dtTbl.Rows.Add(tblRow)

                    Dim dtPSCPS As DataTable = GetPostSalesCallProductSetting(productId, strCIWConn)
                    Dim objectiveEng As String = String.Empty
                    Dim objectiveChi As String = String.Empty
                    Dim riskEng As String = String.Empty
                    Dim riskChi As String = String.Empty
                    If dtPSCPS.Rows.Count > 0 Then
                        objectiveEng = dtPSCPS.Rows(0)("cswpsd_product_objective_eng").ToString().Trim()
                        objectiveChi = dtPSCPS.Rows(0)("cswpsd_product_objective_chi").ToString().Trim()
                        riskEng = dtPSCPS.Rows(0)("cswpsd_risk_eng").ToString().Trim()
                        riskChi = dtPSCPS.Rows(0)("cswpsd_risk_chi").ToString().Trim()
                    End If

                    Dim objRow As DataRow = dtObj.NewRow()
                    objRow("Tsm_PlanNameEng") = tblPlanNameEng
                    objRow("Tsm_PlanNameChi") = tblPlanNameChi
                    objRow("Tsm_ObjectiveEng") = objectiveEng
                    objRow("Tsm_ObjectiveChi") = objectiveChi
                    dtObj.Rows.Add(objRow)

                    Dim riskRow As DataRow = dtRisk.NewRow()
                    riskRow("Tpr_PlanNameEng") = tblPlanNameEng
                    riskRow("Tpr_PlanNameChi") = tblPlanNameChi
                    riskRow("Tpr_RisksEng") = riskEng
                    riskRow("Tpr_RisksChi") = riskChi
                    dtRisk.Rows.Add(riskRow)
                End If

            End If
        Next
        'LAS logic e

        dsRegion.Tables.Add(dtTbl)
        dsRegion.Tables.Add(dtObj)
        dsRegion.Tables.Add(dtRisk)
        'region -e

        Return dtFields
    End Function

    'oliver 2024-3-13 updated for NUT-3813
    Private Function GetPaymentLetterData(ByVal policyNo As String, ByVal isChi As Boolean, ByVal fromDate As Date, ByVal toDate As Date, ByRef dsRegion As DataSet, ByVal strCIWConn As String, ByVal strCompanyID As String) As DataTable
        Dim dtFields As DataTable = GetPaymentLetterFields()
        Dim dtCommon As DataTable = GetCommonField(policyNo, strCIWConn)
        dtFields.Rows.Add(dtFields.NewRow())
        dtFields.Rows(0)("PolicyNo") = policyNo
        dtFields.Rows(0)("ContactAddress1") = dtCommon.Rows(0)("ContactAddress1").ToString()
        dtFields.Rows(0)("ContactAddress2") = dtCommon.Rows(0)("ContactAddress2").ToString()
        dtFields.Rows(0)("ContactAddress3") = dtCommon.Rows(0)("ContactAddress3").ToString()
        dtFields.Rows(0)("ContactAddress4") = dtCommon.Rows(0)("ContactAddress4").ToString()
        dtFields.Rows(0)("AgentPhone") = dtCommon.Rows(0)("AgentPhone").ToString()

        Dim dtInsured As DataTable = CustomerInfoMapping(policyNo, "PI", strCIWConn)
        Dim dtPolicyAccount As DataTable = GetPolicyAccountInfo(policyNo, strCIWConn)
        Dim paymentMode As String = GetPaymentMode(dtPolicyAccount.Rows(0)("Mode").ToString().Trim(), strCIWConn)
        Dim accountStatusCode As String = dtPolicyAccount.Rows(0)("AccountStatusCode").ToString().Trim()
        Dim policyCurrency As String = dtPolicyAccount.Rows(0)("PolicyCurrency").ToString().Trim()
        Dim paidToDate As String = dtPolicyAccount.Rows(0)("PaidToDate").ToString().Trim()

        If isChi Then
            dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerNameChi").ToString() & dtCommon.Rows(0)("OwnerPrefixChi").ToString()
            dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateChi").ToString()
            dtFields.Rows(0)("DearName") = dtCommon.Rows(0)("OwnerNameChi").ToString() & dtCommon.Rows(0)("OwnerPrefixChi").ToString()
            dtFields.Rows(0)("AgentName") = dtCommon.Rows(0)("AgentNameChi").ToString()
            dtFields.Rows(0)("InsuredName") = dtInsured.Rows(0)("NameChi")
            dtFields.Rows(0)("PayMode") = GetPaymentModeChi(paymentMode)
            dtFields.Rows(0)("PolicyStatus") = PolicyStatusChiMapping(accountStatusCode, strCIWConn)
            dtFields.Rows(0)("PolicyCurr") = GetCurrencyChi(policyCurrency)
            If Not String.IsNullOrWhiteSpace(paidToDate) Then
                dtFields.Rows(0)("PaidToDate") = CDate(paidToDate).ToString("yyyy年M月d日")
            End If
            dtFields.Rows(0)("FromDate") = fromDate.ToString("yyyy年M月d日")
            dtFields.Rows(0)("ToDate") = toDate.ToString("yyyy年M月d日")
        Else
            dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
            dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateEng").ToString()
            dtFields.Rows(0)("DearName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
            dtFields.Rows(0)("AgentName") = dtCommon.Rows(0)("AgentNameEng").ToString()
            dtFields.Rows(0)("InsuredName") = dtInsured.Rows(0)("NameEng")
            dtFields.Rows(0)("PayMode") = paymentMode
            dtFields.Rows(0)("PolicyStatus") = PolicyStatusEngMapping(accountStatusCode, strCIWConn)
            dtFields.Rows(0)("PolicyCurr") = policyCurrency
            If Not String.IsNullOrWhiteSpace(paidToDate) Then
                dtFields.Rows(0)("PaidToDate") = CDate(paidToDate).ToString("dd MMM yyyy")
            End If
            dtFields.Rows(0)("FromDate") = fromDate.ToString("dd MMM yyyy")
            dtFields.Rows(0)("ToDate") = toDate.ToString("dd MMM yyyy")
        End If

        'get payment details -s
        Dim dtTbl As DataTable = New DataTable("RegionTbl")
        dtTbl.Columns.Add("Tbl_ReceiptDate", GetType(String))
        dtTbl.Columns.Add("Tbl_Currency", GetType(String))
        dtTbl.Columns.Add("Tbl_Amount", GetType(String))
        dtTbl.Columns.Add("Tbl_Method", GetType(String))

        Dim dsPayment As DataSet = GetPaymentList(policyNo, fromDate, toDate, strCompanyID)
        'get the second table data
        Dim dtPayment As DataTable = dsPayment.Tables(1)
        For Each dr As DataRow In dtPayment.Rows
            Dim paymentDateStr As String = dr("Payment_Date").ToString().Trim()
            Dim payTypeCode As String = dr("PayTypeCode").ToString().Trim()
            Dim payTypeDesc As String = dr("PayTypeDesc").ToString().Trim()

            If Not String.IsNullOrWhiteSpace(paymentDateStr) And Not String.IsNullOrWhiteSpace(payTypeCode) Then
                Dim paymentDate As Date = DateTime.ParseExact(paymentDateStr, "MM/dd/yyyy", Nothing)
                If paymentDate >= fromDate Then
                    Dim newRow As DataRow = dtTbl.NewRow()
                    Dim currency As String = dr("Curr").ToString().Trim()
                    Dim paymentAmount As String = dr("RecAmt").ToString().Trim()

                    newRow("Tbl_ReceiptDate") = If(isChi, CDate(paymentDate).ToString("yyyy年M月d日"), CDate(paymentDate).ToString("dd MMM yyyy"))
                    newRow("Tbl_Currency") = If(isChi, GetCurrencyChi(currency), currency)
                    newRow("Tbl_Amount") = GetMoneyFormat(paymentAmount)
                    'if isChi = True or payTypeDesc is null/empty, get PaymentTypeCodes by payTypeCode
                    If isChi AndAlso Not String.IsNullOrWhiteSpace(payTypeDesc) Then
                        Dim dtPaymentMethod As DataTable = GetPaymentTypeCodesInfo(payTypeCode, strCIWConn)
                        Dim paymentMethodEng As String = dtPaymentMethod.Rows(0)("PaymentTypeDesc").ToString().Trim()
                        Dim paymentMethodChi As String = dtPaymentMethod.Rows(0)("PayTypeChDesc").ToString().Trim()
                        payTypeDesc = If(isChi, paymentMethodChi, paymentMethodEng)
                    End If
                    'if trans desc is receipt cancellation, then payTypeDesc change to receipt cancellation
                    Dim transDesc As String = dr("TransDesc").ToString().Trim()
                    If transDesc.Equals("Receipt Cancellation", StringComparison.OrdinalIgnoreCase) Then
                        payTypeDesc = If(isChi, "入款取消", "Receipt Cancellation")
                    End If
                    newRow("Tbl_Method") = payTypeDesc
                    dtTbl.Rows.Add(newRow)
                End If
            End If
        Next
        dsRegion.Tables.Add(dtTbl)
        'get payment details -e

        Return dtFields
    End Function

    'oliver 2024-3-13 updated for NUT-3813
    Private Function GetPolicyLetterData(ByVal policyNo As String, ByRef dsRegion As DataSet, ByVal isChi As Boolean, ByVal strCIWConn As String) As DataTable
        'verify data -s
        Dim dtPolicyAccount As DataTable = GetPolicyAccountInfo(policyNo, strCIWConn)
        If dtPolicyAccount.Rows.Count = 0 Then
            Throw New Exception(String.Format("Policy not found. -The Policy No. : {0}", policyNo))
        End If

        Dim productId As String = dtPolicyAccount.Rows(0)("ProductID").ToString.Trim()
        Dim dtProductType As DataTable = GetProductTypeInfo(productId, strCIWConn)
        'Dim dtCoverage As DataTable = GetCoverageInfo(policyNo, strCIWConn)
        Dim dtCoverage As DataTable = GetCoverageInfoBySP(policyNo, strCIWConn)
        If dtProductType.Rows.Count > 0 Then
            If IsDBNull(dtProductType.Rows(0)("PrintValueReport")) Then
                Throw New Exception(String.Format("Policy Letter is not available for this product yet. -The Policy No. : {0}", policyNo))
            ElseIf dtProductType.Rows(0)("PrintValueReport") = "A" Then
                For Each dr As DataRow In dtCoverage.Rows
                    If Mid(dr("ProductID"), 2, 4) = "RE15" OrElse Mid(dr("ProductID"), 2, 4) = "RE20" Then
                        Throw New Exception(String.Format("Policy Letter is not available, HRE15/HRE20/URE15/URE20 is attached. -The Policy No. : {0}", policyNo))
                    End If
                Next
            ElseIf dtProductType.Rows(0)("PrintValueReport") = "Q" Then
                Throw New Exception(String.Format("Policy value quotation may be needed. -The Policy No. : {0}", policyNo))
            End If
        End If
        'verify data -e

        Dim dtInsured As DataTable = CustomerInfoMapping(policyNo, "PI", strCIWConn)
        Dim dtBeneficiary As DataTable = CustomerInfoMapping(policyNo, "BE", strCIWConn)
        Dim accountStatusCode As String = dtPolicyAccount.Rows(0)("AccountStatusCode").ToString().Trim()
        Dim policyCurrency As String = dtPolicyAccount.Rows(0)("PolicyCurrency").ToString().Trim()
        Dim paymentMode As String = GetPaymentMode(dtPolicyAccount.Rows(0)("Mode").ToString().Trim(), strCIWConn)
        Dim issueDate As String = dtPolicyAccount.Rows(0)("PolicyEffDate").ToString().Trim()
        Dim paidToDate As String = dtPolicyAccount.Rows(0)("PaidToDate").ToString().Trim()

        Dim dtFields As DataTable = GetPolicyLetterFields()
        Dim dtCommon As DataTable = GetCommonField(policyNo, strCIWConn)
        dtFields.Rows.Add(dtFields.NewRow())
        'setup field value -s
        dtFields.Rows(0)("PolicyNo") = policyNo
        dtFields.Rows(0)("ContactAddress1") = dtCommon.Rows(0)("ContactAddress1").ToString()
        dtFields.Rows(0)("ContactAddress2") = dtCommon.Rows(0)("ContactAddress2").ToString()
        dtFields.Rows(0)("ContactAddress3") = dtCommon.Rows(0)("ContactAddress3").ToString()
        dtFields.Rows(0)("ContactAddress4") = dtCommon.Rows(0)("ContactAddress4").ToString()
        dtFields.Rows(0)("AgentPhone") = dtCommon.Rows(0)("AgentPhone").ToString()
        If isChi Then
            dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerNameChi").ToString() & dtCommon.Rows(0)("OwnerPrefixChi").ToString()
            dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateChi").ToString()
            dtFields.Rows(0)("PrintDate") = dtCommon.Rows(0)("LetterDateChi").ToString()
            dtFields.Rows(0)("DearName") = dtCommon.Rows(0)("OwnerNameChi").ToString() & dtCommon.Rows(0)("OwnerPrefixChi").ToString()
            dtFields.Rows(0)("AgentName") = dtCommon.Rows(0)("AgentNameChi").ToString()
            dtFields.Rows(0)("InsuredName") = dtInsured.Rows(0)("NameChi")
            dtFields.Rows(0)("BenefitName") = dtBeneficiary.Rows(0)("NameChi")
            dtFields.Rows(0)("PolicyStatus") = PolicyStatusChiMapping(accountStatusCode, strCIWConn)
            dtFields.Rows(0)("PolicyCurr") = GetCurrencyChi(policyCurrency)
            dtFields.Rows(0)("PayMode") = GetPaymentModeChi(paymentMode)
            If Not String.IsNullOrWhiteSpace(issueDate) Then
                dtFields.Rows(0)("IssueDate") = CDate(issueDate).ToString("yyyy年M月d日")
            End If
            If Not String.IsNullOrWhiteSpace(paidToDate) Then
                dtFields.Rows(0)("PaidToDate") = CDate(paidToDate).ToString("yyyy年M月d日")
            End If

        Else
            dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
            dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateEng").ToString()
            dtFields.Rows(0)("PrintDate") = dtCommon.Rows(0)("LetterDateEng").ToString()
            dtFields.Rows(0)("DearName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
            dtFields.Rows(0)("AgentName") = dtCommon.Rows(0)("AgentNameEng").ToString()
            dtFields.Rows(0)("InsuredName") = dtInsured.Rows(0)("NameEng")
            dtFields.Rows(0)("BenefitName") = dtBeneficiary.Rows(0)("NameEng")
            dtFields.Rows(0)("PolicyStatus") = PolicyStatusEngMapping(accountStatusCode, strCIWConn)
            dtFields.Rows(0)("PolicyCurr") = policyCurrency
            dtFields.Rows(0)("PayMode") = paymentMode
            If Not String.IsNullOrWhiteSpace(issueDate) Then
                dtFields.Rows(0)("IssueDate") = CDate(issueDate).ToString("dd MMM yyyy")
            End If
            If Not String.IsNullOrWhiteSpace(paidToDate) Then
                dtFields.Rows(0)("PaidToDate") = CDate(paidToDate).ToString("dd MMM yyyy")
            End If

        End If
        'setup field value -e

        'get policy information -s
        Dim modalPremium As Decimal
        If productId = "HDC  U" Or productId = "HGC  U" Or productId = "HYC  U" Or productId = "UDC  U" Or productId = "UGC  U" Or productId = "UYC  U" Or productId = "HIL  U" Or productId = "UIL  U" Then
            modalPremium = If(IsDBNull(dtPolicyAccount.Rows(0)("POSAmt")), 0, dtPolicyAccount.Rows(0)("POSAmt"))
        ElseIf Left(productId, 4) = "HIUL" Or Left(productId, 4) = "UIUL" Or Left(productId, 4) = "HIUI" Or Left(productId, 4) = "UIUI" Or Left(productId, 4) = "HIUS" Or Left(productId, 4) = "UIUS" Then
            modalPremium = If(IsDBNull(dtPolicyAccount.Rows(0)("POSAmt")), 0, dtPolicyAccount.Rows(0)("POSAmt"))
        Else
            modalPremium = If(IsDBNull(dtPolicyAccount.Rows(0)("ModalPremium")), 0, dtPolicyAccount.Rows(0)("ModalPremium"))
        End If

        Dim premium As Double = 0
        premium = If(IsDBNull(dtPolicyAccount.Rows(0)("ModalPremium")), 0, dtPolicyAccount.Rows(0)("ModalPremium"))
        Dim paidDate As Date = New Date
        paidDate = dtPolicyAccount.Rows(0)("PaidToDate")
        Dim LevyInsurance As Double = 0
        LevyInsurance = GetLevyAmountQuotation(policyNo, policyCurrency, premium, paidDate)

        Dim acNo As String = Nothing
        Dim acName As String = Nothing
        Dim bankName As String = Nothing
        Dim acStatusCode As String = Nothing
        Dim drawDate As String = Nothing
        Dim billingType As String = dtPolicyAccount.Rows(0)("BillingType").ToString().Trim()
        If billingType = "4" Then
            Dim dtDDA As DataTable = GetDDAInfo(policyNo, strCIWConn)
            Dim ddaBankAccountNo As String = dtDDA.Rows(0)("DDABankAccountNo").ToString().Trim()
            Dim ddaBankCode As String = dtDDA.Rows(0)("DDABankCode").ToString().Trim()
            Dim ddaBranckCode As String = dtDDA.Rows(0)("DDABranchCode").ToString().Trim()
            If ddaBankAccountNo.Length > 4 Then
                acNo = ddaBankCode & "-" & ddaBranckCode & "-" & Left(ddaBankAccountNo, ddaBankAccountNo.Length - 4) & "****"
            Else
                acNo = ddaBankCode & "-" & ddaBranckCode & "-****"
            End If
            acName = dtDDA.Rows(0)("DDAPayorInfo").ToString().Trim()
            bankName = dtDDA.Rows(0)("DDABankerName").ToString().Trim()
            acStatusCode = dtDDA.Rows(0)("DDAStatus").ToString().Trim()
            drawDate = dtDDA.Rows(0)("DDADrawDate").ToString().Trim()
        ElseIf billingType = "5" Then
            Dim dtCCDR As DataTable = GetCCDRInfo(policyNo, strCIWConn)
            Dim ccdrCardNumber As String = dtCCDR.Rows(0)("CCDRCardNumber").ToString().Trim()
            acNo = Left(ccdrCardNumber, ccdrCardNumber.Length - 6) & "******"
            acName = dtCCDR.Rows(0)("CCDRCardHolderName").ToString().Trim()
            acStatusCode = dtCCDR.Rows(0)("CCDRStatus").ToString().Trim()
            drawDate = dtCCDR.Rows(0)("CCDRDrawDate").ToString().Trim()
        Else
            drawDate = "N/A"
        End If

        Dim acStatus As String = Nothing
        Dim strActive As String = If(isChi, "生效", "Active")
        Dim strInactive As String = If(isChi, "無效", "Inactive")
        If Right(acStatusCode, 1) = "C" Or Right(acStatusCode, 1) = "S" Or acStatusCode = "99" Then
            acStatus = strInactive
        ElseIf acStatusCode <> "" Then
            acStatus = strActive
        End If


        Dim paymentMethod As String = If(isChi, GetBillingTypeDescChi(billingType, strCIWConn), GetBillingTypeDesc(billingType, strCIWConn))
        Dim couponOption As String = dtPolicyAccount.Rows(0)("CouponOption").ToString().Trim()
        Dim couponOptionDesc As String = If(isChi, GetCouponOptionDescChi(couponOption), GetCouponOption(couponOption))
        Dim dividendOption As String = dtPolicyAccount.Rows(0)("DividendOption").ToString().Trim()
        Dim dividendOptionDesc As String = If(isChi, GetDividendOptionDescChi(dividendOption), GetDivOption(dividendOption))
        'get policy information -e

        dtFields.Rows(0)("ModalPrem") = GetMoneyFormat(modalPremium)
        dtFields.Rows(0)("PremSuspense") = GetMoneyFormat(dtPolicyAccount.Rows(0)("PremiumSuspense"))
        dtFields.Rows(0)("Levy") = GetMoneyFormat(LevyInsurance)
        dtFields.Rows(0)("LevySuspense") = GetMoneyFormat(GetLevySuspense(policyNo))
        dtFields.Rows(0)("TMPWithLevy") = GetMoneyFormat(premium + LevyInsurance)
        dtFields.Rows(0)("AccountNo") = acNo
        dtFields.Rows(0)("BankName") = bankName
        dtFields.Rows(0)("AccountName") = acName
        dtFields.Rows(0)("AccountStatus") = acStatus
        dtFields.Rows(0)("BillNumber") = dtPolicyAccount.Rows(0)("BillNo").ToString().Trim()
        dtFields.Rows(0)("PayMethod") = paymentMethod
        dtFields.Rows(0)("DrawDate") = drawDate
        dtFields.Rows(0)("CouponOption") = couponOptionDesc
        dtFields.Rows(0)("DividendOption") = dividendOptionDesc

        'get policy value -s
        Dim companyId As String = dtPolicyAccount.Rows(0)("CompanyID").ToString().Trim()
        Dim additionDeathCvr As String = dtPolicyAccount.Rows(0)("AdditionDeathCvr").ToString().Trim()
        Dim productPolValueFunc As String = dtProductType.Rows(0)("ProductPolValueFunc").ToString().Trim()
        'oliver 2024-3-13 updated for NUT-3813
        Dim dtPolicyValue As DataTable = GetPolicyLetterValue(companyId, policyNo, additionDeathCvr, productPolValueFunc, strCIWConn)

        Dim paidUpAddition As Decimal
        If dividendOption <> "R" And dividendOption <> "E" Then
            paidUpAddition = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_PUASITotal")), 0, dtPolicyValue.Rows(0)("cswval_PUASITotal"))
        End If
        Dim paidUpAdditionCashValue As Decimal
        If dividendOption <> "R" And dividendOption <> "E" Then
            paidUpAdditionCashValue = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TVLPUA")), 0, dtPolicyValue.Rows(0)("cswval_TVLPUA"))
        End If
        Dim reversionaryBonusValue As Decimal
        If dividendOption = "R" Then
            reversionaryBonusValue = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_PUASITotal")), 0, dtPolicyValue.Rows(0)("cswval_PUASITotal"))
        End If
        Dim reversionaryBonusCashValue As Decimal
        If dividendOption = "R" Then
            reversionaryBonusCashValue = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TVLPUA")), 0, dtPolicyValue.Rows(0)("cswval_TVLPUA"))
        End If

        Dim couponOnDeposit As Decimal
        Dim cswvalTCOUDP As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TCOUDP")), 0, dtPolicyValue.Rows(0)("cswval_TCOUDP"))
        Dim cswvalTCOUIT As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TCOUIT")), 0, dtPolicyValue.Rows(0)("cswval_TCOUIT"))
        couponOnDeposit = cswvalTCOUDP - cswvalTCOUIT

        Dim outstandingPolicyLoanBalance As Decimal
        Dim cswvalTLONAT As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TLONAT")), 0, dtPolicyValue.Rows(0)("cswval_TLONAT"))
        Dim cswvalTLONIT As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TLONIT")), 0, dtPolicyValue.Rows(0)("cswval_TLONIT"))
        Dim cswvalTAPLAT As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TAPLAT")), 0, dtPolicyValue.Rows(0)("cswval_TAPLAT"))
        Dim cswvalTAPLIT As Decimal = If(IsDBNull(dtPolicyValue.Rows(0)("cswval_TAPLIT")), 0, dtPolicyValue.Rows(0)("cswval_TAPLIT"))
        outstandingPolicyLoanBalance = cswvalTLONAT + cswvalTLONIT + cswvalTAPLAT + cswvalTAPLIT
        'get policy value -e

        dtFields.Rows(0)("PolicyValDate") = If(isChi, UCase(Format(DateTime.Now, "yyyy年M月d日")), DateTime.Now.ToString("dd MMM yyyy"))
        dtFields.Rows(0)("TotalPolVal") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TCSHVL"))
        dtFields.Rows(0)("BasicCashVal") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TBSCSV"))
        dtFields.Rows(0)("AMLAmount") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TMAXLN"))
        dtFields.Rows(0)("DividendDep") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TDIVDP"))
        dtFields.Rows(0)("DividendInt") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TDEPIN"))
        dtFields.Rows(0)("PUAVal") = GetMoneyFormat(paidUpAddition)
        dtFields.Rows(0)("PUACashVal") = GetMoneyFormat(paidUpAdditionCashValue)
        dtFields.Rows(0)("RBVal") = GetMoneyFormat(reversionaryBonusValue)
        dtFields.Rows(0)("RBCashVal") = GetMoneyFormat(reversionaryBonusCashValue)
        dtFields.Rows(0)("CouponDep") = GetMoneyFormat(couponOnDeposit)
        dtFields.Rows(0)("CouponInt") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TCOUIT"))
        dtFields.Rows(0)("PDFAmount") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TPDF"))
        dtFields.Rows(0)("PDFInt") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TPDFIN"))
        dtFields.Rows(0)("PolicyLoanDate") = If(isChi, UCase(Format(DateTime.Now, "yyyy年M月d日")), DateTime.Now.ToString("dd MMM yyyy"))
        dtFields.Rows(0)("OPLBalance") = GetMoneyFormat(outstandingPolicyLoanBalance)
        dtFields.Rows(0)("APLAmount") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TAPLAT"))
        dtFields.Rows(0)("APLInt") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TAPLIT"))
        dtFields.Rows(0)("PolicyLoan") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TLONAT"))
        dtFields.Rows(0)("PLInt") = GetMoneyFormat(dtPolicyValue.Rows(0)("cswval_TLONIT"))

        'get coverage -s
        Dim dtTbl As DataTable = New DataTable("RegionTbl")
        dtTbl.Columns.Add("Tbl_PlanName", GetType(String))
        dtTbl.Columns.Add("Tbl_SubPlanName", GetType(String))
        dtTbl.Columns.Add("Tbl_InsuredName", GetType(String))
        dtTbl.Columns.Add("Tbl_IssueDate", GetType(String))
        dtTbl.Columns.Add("Tbl_ExpiryDate", GetType(String))
        dtTbl.Columns.Add("Tbl_PolicyStatus", GetType(String))
        dtTbl.Columns.Add("Tbl_SumInsured", GetType(String))
        dtTbl.Columns.Add("Tbl_Premium", GetType(String))

        'filter the coverage
        dtCoverage = FilterCoverage(dtCoverage)

        For Each dr As DataRow In dtCoverage.Rows

            Dim newRow As DataRow = dtTbl.NewRow()

            Dim tblProductId As String = dr("ProductID").ToString().Trim()

            Dim planName As String = GetDescription(isChi, tblProductId, dr("ChineseDescription").ToString(), dr("Description").ToString()).Trim()
            Dim subPlanName As String = GetSubPlan(isChi, productId, dr("TBL1").ToString(), dr("TBL2").ToString(), dr("Trailer").ToString()).Trim()
            'Dim description As String = If(String.IsNullOrWhiteSpace(subPlanName), planName, planName + Environment.NewLine & "   " & subPlanName)

            newRow("Tbl_PlanName") = planName
            newRow("Tbl_SubPlanName") = subPlanName

            Dim fullNameChi As String = dr("ChiLstNm").ToString.Trim() & dr("ChiFstNm").ToString().Trim()
            Dim fullNameEng As String = dr("NameSuffix").ToString.Trim() & " " & dr("FirstName").ToString.Trim()
            If String.IsNullOrWhiteSpace(fullNameChi) Then
                fullNameChi = fullNameEng
            End If

            newRow("Tbl_InsuredName") = If(isChi, fullNameChi, fullNameEng)

            Dim issueD As String = dr("IssueDate").ToString().Trim()
            If Not String.IsNullOrWhiteSpace(issueD) Then
                newRow("Tbl_IssueDate") = If(isChi, CDate(issueD).ToString("yyyy年M月d日"), CDate(issueD).ToString("dd MMM yyyy"))
            End If

            Dim expiryDate As String = dr("ExpiryDate").ToString().Trim()
            If Not String.IsNullOrWhiteSpace(issueDate) Then
                newRow("Tbl_ExpiryDate") = If(isChi, CDate(expiryDate).ToString("yyyy年M月d日"), CDate(expiryDate).ToString("dd MMM yyyy"))
            End If
            Dim coverageStatus As String = If(isChi, CoverageStatusChiMapping(dr("CoverageStatus").ToString.Trim()), CoverageStatusEngMapping(dr("CoverageStatus").ToString.Trim()))
            coverageStatus = If(String.IsNullOrWhiteSpace(coverageStatus), dr("AccountStatus").ToString.Trim(), coverageStatus)

            newRow("Tbl_PolicyStatus") = coverageStatus

            Dim productType As String = dr("ProductType").ToString()
            Dim sumInsured As String = String.Empty

            If productType = "R" OrElse productType = "B" OrElse productType = "T" OrElse productType = "L" Then
                sumInsured = "N/A"
            Else
                sumInsured = GetMoneyFormat(dr("SumInsured"))
            End If

            newRow("Tbl_SumInsured") = sumInsured
            newRow("Tbl_Premium") = GetMoneyFormat(dr("ModalPremium"))
            dtTbl.Rows.Add(newRow)

        Next
        dsRegion.Tables.Add(dtTbl)
        'get coverage -e

        Return dtFields
    End Function

    Private Function FilterCoverage(ByVal dtCoverage As DataTable) As DataTable

        dtCoverage = dtCoverage.AsEnumerable().Where(Function(c) Not ((c.Field(Of String)("CoverageStatus") = "C" AndAlso
                                                         (c.Field(Of String)("ProductID") = "HRRAA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRAC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRAH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRBA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRBC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRBH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRSA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRSC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRRSH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBAA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBAC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBAH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBBA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBBC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBBH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBSA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBSC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRBSH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTAA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTAC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTAH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTBA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTBC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTBH1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTSA1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTSC1" OrElse
                                                         c.Field(Of String)("ProductID") = "HRTSH1")) OrElse
                                                         c.Field(Of String)("ProductType") = "D")).CopyToDataTable()

        Return dtCoverage
    End Function

    Private Function GetDescription(ByVal isChi As Boolean, ByVal productID As String, ByVal descChi As String, ByVal descEng As String) As String
        Dim desc As String = String.Empty
        Dim showNote As String = String.Empty
        Dim showOcc As String = String.Empty
        Dim strOccupation As String = String.Empty
        Dim strNote As String = String.Empty

        If isChi Then
            strOccupation = "職業類別 ："
            strNote = "(註)"
        Else
            strOccupation = "Occupation Class : "
            strNote = "(Noted)"
        End If

        '***** Show Occupation *****
        If Mid(productID, 2, 1) = "D" AndAlso Mid(productID, 2, 2) <> "DC" Then

            If Mid(productID, 2, 2) = "DA" Then

                If Mid(productID, 4, 1) = "1" Then
                    showOcc = strOccupation & "A1"
                ElseIf Mid(productID, 4, 1) = "2" Then
                    showOcc = strOccupation & "A2"
                ElseIf Mid(productID, 4, 1) = "A" Then
                    showOcc = strOccupation & "AA"
                ElseIf Mid(productID, 4, 1) = "C" Then
                    showOcc = strOccupation & "A1+"
                End If
            Else    'DB, DF, DO, DL

                If Mid(productID, 5, 1) = "1" Then
                    showOcc = strOccupation & "A1"
                ElseIf Mid(productID, 5, 1) = "2" Then
                    showOcc = strOccupation & "A2"
                ElseIf Mid(productID, 5, 1) = "A" Then
                    showOcc = strOccupation & "AA"
                ElseIf Mid(productID, 5, 1) = "C" Then
                    showOcc = strOccupation & "A1+"
                End If

            End If

        End If

        If Mid(productID, 2, 3) = "RAI" OrElse Mid(productID, 2, 3) = "RDF" OrElse Mid(productID, 2, 3) = "RFD" OrElse Mid(productID, 2, 3) = "RHI" OrElse Mid(productID, 2, 3) = "RHP" OrElse Mid(productID, 2, 3) = "RPA" OrElse Mid(productID, 2, 3) = "RHS" Then
            showOcc = strOccupation & Mid(productID, 5, 1)
        End If

        If Mid(productID, 2, 3) = "RFH" Or Mid(productID, 2, 4) = "RFAI" Or Mid(productID, 2, 4) = "RFA " Then
            showNote = strNote
        End If

        If Trim(showOcc) <> "" Then
            showOcc = "(" & showOcc & ")"
        End If

        If isChi Then
            If String.IsNullOrWhiteSpace(descChi) Then
                desc = productID & " " & showOcc & " " & showNote
            Else
                desc = descChi & " " & showOcc & " " & showNote
            End If

        Else
            If String.IsNullOrWhiteSpace(descEng) Then
                desc = productID & " " & showOcc & " " & showNote
            Else
                desc = descEng & " " & showOcc & " " & showNote
            End If
        End If

        Return desc

    End Function

    Private Function GetSubPlan(ByVal isChi As Boolean, ByVal productID As String, ByVal tbl1 As String, ByVal tbl2 As String, ByVal trailer As Integer) As String
        Dim showSubPlan As String = String.Empty
        Dim strEconomy As String = String.Empty
        Dim strPremier As String = String.Empty
        Dim strSuperior As String = String.Empty
        Dim strStandard As String = String.Empty
        Dim strMBB As String = String.Empty
        Dim strPlan As String = String.Empty
        Dim strWaitingPeriod As String = String.Empty
        Dim strBenefitPeriod As String = String.Empty
        Dim strToAge As String = String.Empty
        Dim strYearsOld As String = String.Empty
        Dim strYears As String = String.Empty

        If isChi Then
            strEconomy = "經濟計劃"
            strPremier = "優等計劃"
            strSuperior = "特等計劃"
            strStandard = "標準計劃"
            strMBB = " + 增值保障"
            strPlan = "計劃"
            strWaitingPeriod = "等候期 (日) : "
            strBenefitPeriod = "權益保障期 : "
            strToAge = "至"
            strYearsOld = "歲"
            strYears = "年"
        Else
            strEconomy = "Economy"
            strPremier = "Premier"
            strSuperior = "Superior"
            strStandard = "Standard"
            strMBB = " + Booster"
            strPlan = "Plan"
            strWaitingPeriod = "Waiting Period (Days) : "
            strBenefitPeriod = "Benefit Period : "
            strToAge = "To Age "
            strYearsOld = ""
            strYears = "Years"
        End If

        '***** Show Sub Plan *****
        'Add medipro for life/asia i.ulife -- 'MP11','MP21'
        If Mid(productID, 2, 2) = "AM" OrElse Mid(productID, 2, 3) = "H01" OrElse
            Mid(productID, 2, 4) = "RH01" OrElse Mid(productID, 2, 3) = "RHC" OrElse
            Mid(productID, 2, 3) = "RAM" OrElse Mid(productID, 2, 3) = "H07" OrElse
            Mid(productID, 2, 4) = "RH07" OrElse productID = "MP11" OrElse productID = "MP21" Then

            If Mid(tbl1, 2, 1) = "E" Then
                showSubPlan = strEconomy
            ElseIf Mid(tbl1, 2, 1) = "P" Then
                showSubPlan = strPremier
            ElseIf Mid(tbl1, 2, 1) = "S" Then
                showSubPlan = strSuperior
            ElseIf Mid(tbl1, 2, 1) = "W" Then
                showSubPlan = strStandard
            End If
            If tbl2 = "001" Then
                showSubPlan = showSubPlan & strMBB
            End If
        End If

        If Mid(productID, 2, 3) = "RFA" OrElse Mid(productID, 2, 3) = "RAI" OrElse
            Mid(productID, 2, 3) = "RHP" OrElse Mid(productID, 2, 3) = "RHI" OrElse
            Mid(productID, 2, 3) = "RHS" OrElse Mid(productID, 2, 4) = "RFHP" OrElse
            Mid(productID, 2, 4) = "RFHI" OrElse Mid(productID, 2, 4) = "RFHS" OrElse Mid(productID, 2, 3) = "RPA" Then

            showSubPlan = strPlan & " " & Mid(tbl1, 2, 1)

        End If

        If Mid(productID, 2, 1) = "D" And Mid(productID, 2, 2) <> "DC" And trailer = 1 Then

            showSubPlan = strWaitingPeriod & tbl2 & "   " & strBenefitPeriod
            If tbl1.Length > 10 Then
                showSubPlan = showSubPlan & strToAge & "65" & strYearsOld
            Else
                showSubPlan = showSubPlan & tbl1 & " " & strYears
            End If
        End If

        Dim strTbl1 As String = String.Empty
        strTbl1 = tbl1.Trim()

        If (productID = "HEM1" Or productID = "UEM1" Or productID = "HER1" Or productID = "UER1") And isChi Then

            If strTbl1 = "HA" Then showSubPlan = "標準計劃 / HKD 0"
            If strTbl1 = "HD" Then showSubPlan = "特等計劃 / HKD 0"
            If strTbl1 = "HG" Then showSubPlan = "優等計劃 / HKD 0"
            If strTbl1 = "HJ" Then showSubPlan = "標準計劃 / HKD 0"
            If strTbl1 = "HM" Then showSubPlan = "特等計劃 / HKD 0"
            If strTbl1 = "HP" Then showSubPlan = "優等計劃 / HKD 0"
            If strTbl1 = "HB" Then showSubPlan = "標準計劃 / HKD 40,000"
            If strTbl1 = "HE" Then showSubPlan = "特等計劃 / HKD 40,000"
            If strTbl1 = "HH" Then showSubPlan = "優等計劃 / HKD 40,000"
            If strTbl1 = "HK" Then showSubPlan = "標準計劃 / HKD 40,000"
            If strTbl1 = "HN" Then showSubPlan = "特等計劃 / HKD 40,000"
            If strTbl1 = "HQ" Then showSubPlan = "優等計劃 / HKD 40,000"
            If strTbl1 = "HC" Then showSubPlan = "標準計劃 / HKD 80,000"
            If strTbl1 = "HF" Then showSubPlan = "特等計劃 / HKD 80,000"
            If strTbl1 = "HI" Then showSubPlan = "優等計劃 / HKD 80,000"
            If strTbl1 = "HL" Then showSubPlan = "標準計劃 / HKD 80,000"
            If strTbl1 = "HO" Then showSubPlan = "特等計劃 / HKD 80,000"
            If strTbl1 = "HR" Then showSubPlan = "優等計劃 / HKD 80,000"
            If strTbl1 = "U1" Then showSubPlan = "標準計劃 / USD 0"
            If strTbl1 = "U4" Then showSubPlan = "特等計劃 / USD 0"
            If strTbl1 = "U7" Then showSubPlan = "優等計劃 / USD 0"
            If strTbl1 = "US" Then showSubPlan = "標準計劃 / USD 0"
            If strTbl1 = "UV" Then showSubPlan = "特等計劃 / USD 0"
            If strTbl1 = "UY" Then showSubPlan = "優等計劃 / USD 0"
            If strTbl1 = "U0" Then showSubPlan = "優等計劃 / USD 10,000"
            If strTbl1 = "U3" Then showSubPlan = "標準計劃 / USD 10,000"
            If strTbl1 = "U6" Then showSubPlan = "特等計劃 / USD 10,000"
            If strTbl1 = "U9" Then showSubPlan = "優等計劃 / USD 10,000"
            If strTbl1 = "UU" Then showSubPlan = "標準計劃 / USD 10,000"
            If strTbl1 = "UX" Then showSubPlan = "特等計劃 / USD 10,000"
            If strTbl1 = "U2" Then showSubPlan = "標準計劃 / USD 5,000"
            If strTbl1 = "U5" Then showSubPlan = "特等計劃 / USD 5,000"
            If strTbl1 = "U8" Then showSubPlan = "優等計劃 / USD 5,000"
            If strTbl1 = "UT" Then showSubPlan = "標準計劃 / USD 5,000"
            If strTbl1 = "UW" Then showSubPlan = "特等計劃 / USD 5,000"
            If strTbl1 = "UZ" Then showSubPlan = "優等計劃 / USD 5,000"

        End If

        If (productID = "HEM1" Or productID = "UEM1" Or productID = "HER1" Or productID = "UER1") And Not isChi Then

            If strTbl1 = "HA" Then showSubPlan = "Standard / HKD 0"
            If strTbl1 = "HD" Then showSubPlan = "Superior / HKD 0"
            If strTbl1 = "HG" Then showSubPlan = "Premier / HKD 0"
            If strTbl1 = "HJ" Then showSubPlan = "Standard / HKD 0"
            If strTbl1 = "HM" Then showSubPlan = "Superior / HKD 0"
            If strTbl1 = "HP" Then showSubPlan = "Premier / HKD 0"
            If strTbl1 = "HB" Then showSubPlan = "Standard / HKD 40,000"
            If strTbl1 = "HE" Then showSubPlan = "Superior / HKD 40,000"
            If strTbl1 = "HH" Then showSubPlan = "Premier / HKD 40,000"
            If strTbl1 = "HK" Then showSubPlan = "Standard / HKD 40,000"
            If strTbl1 = "HN" Then showSubPlan = "Superior / HKD 40,000"
            If strTbl1 = "HQ" Then showSubPlan = "Premier / HKD 40,000"
            If strTbl1 = "HC" Then showSubPlan = "Standard / HKD 80,000"
            If strTbl1 = "HF" Then showSubPlan = "Superior / HKD 80,000"
            If strTbl1 = "HI" Then showSubPlan = "Premier / HKD 80,000"
            If strTbl1 = "HL" Then showSubPlan = "Standard / HKD 80,000"
            If strTbl1 = "HO" Then showSubPlan = "Superior / HKD 80,000"
            If strTbl1 = "HR" Then showSubPlan = "Premier / HKD 80,000"
            If strTbl1 = "U1" Then showSubPlan = "Standard / USD 0"
            If strTbl1 = "U4" Then showSubPlan = "Superior / USD 0"
            If strTbl1 = "U7" Then showSubPlan = "Premier / USD 0"
            If strTbl1 = "US" Then showSubPlan = "Standard / USD 0"
            If strTbl1 = "UV" Then showSubPlan = "Superior / USD 0"
            If strTbl1 = "UY" Then showSubPlan = "Premier / USD 0"
            If strTbl1 = "U0" Then showSubPlan = "Premier / USD 10,000"
            If strTbl1 = "U3" Then showSubPlan = "Standard / USD 10,000"
            If strTbl1 = "U6" Then showSubPlan = "Superior / USD 10,000"
            If strTbl1 = "U9" Then showSubPlan = "Premier / USD 10,000"
            If strTbl1 = "UU" Then showSubPlan = "Standard / USD 10,000"
            If strTbl1 = "UX" Then showSubPlan = "Superior / USD 10,000"
            If strTbl1 = "U2" Then showSubPlan = "Standard / USD 5,000"
            If strTbl1 = "U5" Then showSubPlan = "Superior / USD 5,000"
            If strTbl1 = "U8" Then showSubPlan = "Premier / USD 5,000"
            If strTbl1 = "UT" Then showSubPlan = "Standard / USD 5,000"
            If strTbl1 = "UW" Then showSubPlan = "Superior / USD 5,000"
            If strTbl1 = "UZ" Then showSubPlan = "Premier / USD 5,000"
        End If

        Return showSubPlan
    End Function

    'oliver 2024-3-13 updated for NUT-3813
    Private Function GetPolicyLetterValue(ByVal companyId As String, ByVal policyNo As String, ByVal additionDeathCvr As String, ByVal productPolValueFunc As String, ByVal strCIWConn As String) As DataTable
        Dim dtPolicyValue As DataTable = GetPolicyValue(strCIWConn, companyId)
        If companyId = "EAA" Then
            Dim objDB As Object = CreateObject("Dbsecurity.Database")
            Call objDB.Connect(gsUser, strValProj, strValConn)

            Dim strSql As String = Nothing
            If gUAT Then
                strSql = "call CSDCIWSBP.policyval('" & policyNo & "')"
            Else
                strSql = "call CIWLIB.policyval('" & policyNo & "')"
            End If
            Dim objRS As ADODB.Recordset = objDB.executestatement(strSql)

            If objRS.RecordCount > 0 Then
                Dim dr As DataRow = dtPolicyValue.NewRow
                dr("cswval_id") = -1
                For i As Integer = 0 To objRS.Fields.Count - 1
                    If objRS.Fields(i).Type = ADODB.DataTypeEnum.adChar Or objRS.Fields(i).Type = ADODB.DataTypeEnum.adVarChar Then
                        dr.Item("cswval_" & objRS.Fields(i).Name) = RTrim(objRS.Fields(i).Value)
                    Else
                        dr.Item("cswval_" & objRS.Fields(i).Name) = objRS.Fields(i).Value
                    End If
                Next
                dr("cswval_TPOLID") = policyNo
                dr("cswval_PUASITotal") = additionDeathCvr
                dtPolicyValue.Rows.Add(dr)
            End If
        Else
            ' Call Web service to get value
            Dim strErr As String = ""
            Dim dblTotal As Double

            Dim dtVAL As DataTable = GetLAPolicyValue(g_Comp, policyNo, productPolValueFunc, dblTotal, strErr)
            If strErr = "" Then
                If dtVAL IsNot Nothing AndAlso dtVAL.Rows.Count > 0 Then
                    With dtVAL
                        Dim dr As DataRow = dtPolicyValue.NewRow
                        dr("cswval_id") = -1
                        dr("cswval_TFLOID") = "Y"
                        dr("cswval_TPOLID") = policyNo
                        dr("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        dr("cswval_TPADDT") = DBNull.Value
                        dr("cswval_TCSHVL") = .Rows(0).Item("TotalSurVal")
                        dr("cswval_TBSCSV") = .Rows(0).Item("BaseCashValue")
                        dr("cswval_TVLPUA") = .Rows(0).Item("PUACashValue")
                        dr("cswval_TDIVDP") = .Rows(0).Item("DivOnDeposit")
                        dr("cswval_TDEPIN") = .Rows(0).Item("DivDepositInt")
                        dr("cswval_TPDF") = .Rows(0).Item("PDFAmount")
                        dr("cswval_TPDFIN") = .Rows(0).Item("PDFInt")
                        dr("cswval_TPRMRD") = .Rows(0).Item("PremRefund")
                        dr("cswval_TLONAT") = .Rows(0).Item("Loan")
                        dr("cswval_TLONIT") = .Rows(0).Item("LoanInt")
                        dr("cswval_TAPLAT") = .Rows(0).Item("APL")
                        dr("cswval_TAPLIT") = .Rows(0).Item("APLInt")
                        dr("cswval_TMAXLN") = DBNull.Value
                        dr("cswval_TDSCLN") = DBNull.Value
                        dr("cswval_TBSELN") = DBNull.Value
                        dr("cswval_TDSCFR") = DBNull.Value
                        dr("cswval_TINRRB") = DBNull.Value
                        dr("cswval_TRDCHV") = DBNull.Value
                        dr("cswval_TCOUDP") = .Rows(0).Item("Coupon")
                        dr("cswval_TCOUIT") = .Rows(0).Item("CouponInt")
                        dr("cswval_TERRFG") = DBNull.Value
                        dr("cswval_TOSPRM") = DBNull.Value
                        dr("cswval_TREAMT") = DBNull.Value
                        dr("cswval_DivYear") = .Rows(0).Item("DivYear")
                        dr("cswval_CouYear") = .Rows(0).Item("CouponYear")
                        dr("cswval_DivDeclare") = .Rows(0).Item("DivDeclare")
                        dr("cswval_PremSusp") = .Rows(0).Item("PremSuspense")
                        dr("cswval_PremRefund") = DBNull.Value
                        dr("cswval_PUASITotal") = .Rows(0).Item("PaidUpAddition")
                        dr("cswval_PUASICurrent") = .Rows(0).Item("CurrentPaidUp")
                        dr("cswval_CouOpt") = .Rows(0).Item("CouponOpt")
                        dr("cswval_DivDepositInt") = DBNull.Value
                        dr("cswval_DivOpt") = .Rows(0).Item("DivOpt")
                        dr("cswval_MiscSusp") = .Rows(0).Item("MiscSuspense")
                        dr("cswval_CouDelcare") = .Rows(0).Item("TotalCouponDeclare")
                        dtPolicyValue.Rows.Add(dr)
                    End With
                Else
                    With dtVAL
                        Dim dr As DataRow = dtPolicyValue.NewRow
                        dr("cswval_id") = -1
                        dr("cswval_TFLOID") = "Y"
                        dr("cswval_TPOLID") = policyNo
                        dr("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        dr("cswval_TCSHVL") = dblTotal
                        dtPolicyValue.Rows.Add(dr)
                    End With
                End If
            Else
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Return dtPolicyValue
    End Function

    Private Function GetfrmLtrParam(ByVal titleTxt As String) As frmLtrParam
        Dim frmInput As New frmLtrParam
        frmInput.Text = titleTxt
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Text = DateSerial(Year(Today), 1, 1)
        frmInput.txtTo.Text = Today
        frmInput.txtFrom.Enabled = False
        frmInput.txtTo.Enabled = False
        frmInput.grpExport.Enabled = False
        Return frmInput
    End Function

#Region "letter fields"
    Private Function GetPSCFollowLetterFields() As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PolicyNo", GetType(String))
        dtFields.Columns.Add("LetterDate", GetType(String))
        dtFields.Columns.Add("ContactAddress1", GetType(String))
        dtFields.Columns.Add("ContactAddress2", GetType(String))
        dtFields.Columns.Add("ContactAddress3", GetType(String))
        dtFields.Columns.Add("ContactAddress4", GetType(String))
        dtFields.Columns.Add("ContactFullName", GetType(String))
        dtFields.Columns.Add("AgentNameEng", GetType(String))
        dtFields.Columns.Add("AgentNameChi", GetType(String))
        dtFields.Columns.Add("AgentLocation", GetType(String))
        dtFields.Columns.Add("CoolingOffDateEng", GetType(String))
        dtFields.Columns.Add("CoolingOffDateChi", GetType(String))
        dtFields.Columns.Add("PlanNameEng", GetType(String))
        dtFields.Columns.Add("PlanNameChi", GetType(String))
        dtFields.Columns.Add("PayModeEng", GetType(String))
        dtFields.Columns.Add("PayModeChi", GetType(String))
        dtFields.Columns.Add("PSCContentEng", GetType(String))
        dtFields.Columns.Add("PSCContentChi", GetType(String))
        dtFields.Columns.Add("Rmk_MedicalPlanEng", GetType(String))
        dtFields.Columns.Add("Rmk_MedicalPlanChi", GetType(String))
        dtFields.Columns.Add("Rmk_EliteTermPlanEng", GetType(String))
        dtFields.Columns.Add("Rmk_EliteTermPlanChi", GetType(String))
        dtFields.Columns.Add("Rmk_ExPolicyTermEng", GetType(String))
        dtFields.Columns.Add("Rmk_ExPolicyTermChi", GetType(String))
        dtFields.Columns.Add("SuiTitleEng", GetType(String))
        dtFields.Columns.Add("SuiTitleChi", GetType(String))
        dtFields.Columns.Add("SuiContentEng", GetType(String))
        dtFields.Columns.Add("SuiContentChi", GetType(String))
        dtFields.Columns.Add("Rmk_NGBTitleEng", GetType(String))
        dtFields.Columns.Add("Rmk_NGBTitleChi", GetType(String))
        dtFields.Columns.Add("Rmk_NGBContentEng", GetType(String))
        dtFields.Columns.Add("Rmk_NGBContentChi", GetType(String))
        dtFields.Columns.Add("Rmk_FeeTitleEng", GetType(String))
        dtFields.Columns.Add("Rmk_FeeTitleChi", GetType(String))
        dtFields.Columns.Add("Rmk_FeeChargeEng", GetType(String))
        dtFields.Columns.Add("Rmk_FeeChargeChi", GetType(String))
        Return dtFields
    End Function
    Private Function GetPaymentLetterFields() As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PolicyNo", GetType(String))
        dtFields.Columns.Add("LetterDate", GetType(String))
        dtFields.Columns.Add("ContactFullName", GetType(String))
        dtFields.Columns.Add("ContactAddress1", GetType(String))
        dtFields.Columns.Add("ContactAddress2", GetType(String))
        dtFields.Columns.Add("ContactAddress3", GetType(String))
        dtFields.Columns.Add("ContactAddress4", GetType(String))
        dtFields.Columns.Add("DearName", GetType(String))
        dtFields.Columns.Add("AgentName", GetType(String))
        dtFields.Columns.Add("AgentPhone", GetType(String))
        dtFields.Columns.Add("InsuredName", GetType(String))
        dtFields.Columns.Add("PayMode", GetType(String))
        dtFields.Columns.Add("PolicyStatus", GetType(String))
        dtFields.Columns.Add("PolicyCurr", GetType(String))
        dtFields.Columns.Add("PaidToDate", GetType(String))
        dtFields.Columns.Add("FromDate", GetType(String))
        dtFields.Columns.Add("ToDate", GetType(String))
        Return dtFields
    End Function
    Private Function GetPolicyLetterFields() As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PolicyNo", GetType(String))
        dtFields.Columns.Add("PrintDate", GetType(String))
        dtFields.Columns.Add("LetterDate", GetType(String))
        dtFields.Columns.Add("ContactFullName", GetType(String))
        dtFields.Columns.Add("ContactAddress1", GetType(String))
        dtFields.Columns.Add("ContactAddress2", GetType(String))
        dtFields.Columns.Add("ContactAddress3", GetType(String))
        dtFields.Columns.Add("ContactAddress4", GetType(String))
        dtFields.Columns.Add("DearName", GetType(String))
        dtFields.Columns.Add("AgentName", GetType(String))
        dtFields.Columns.Add("AgentPhone", GetType(String))
        dtFields.Columns.Add("InsuredName", GetType(String))
        dtFields.Columns.Add("BenefitName", GetType(String))
        dtFields.Columns.Add("PolicyStatus", GetType(String))
        dtFields.Columns.Add("PolicyCurr", GetType(String))
        dtFields.Columns.Add("PayMode", GetType(String))
        dtFields.Columns.Add("IssueDate", GetType(String))
        dtFields.Columns.Add("PaidToDate", GetType(String))
        dtFields.Columns.Add("ModalPrem", GetType(String))
        dtFields.Columns.Add("PremSuspense", GetType(String))
        dtFields.Columns.Add("Levy", GetType(String))
        dtFields.Columns.Add("LevySuspense", GetType(String))
        dtFields.Columns.Add("TMPWithLevy", GetType(String))
        dtFields.Columns.Add("AccountNo", GetType(String))
        dtFields.Columns.Add("BankName", GetType(String))
        dtFields.Columns.Add("AccountName", GetType(String))
        dtFields.Columns.Add("AccountStatus", GetType(String))
        dtFields.Columns.Add("PayMethod", GetType(String))
        dtFields.Columns.Add("BillNumber", GetType(String))
        dtFields.Columns.Add("DrawDate", GetType(String))
        dtFields.Columns.Add("CouponOption", GetType(String))
        dtFields.Columns.Add("DividendOption", GetType(String))
        dtFields.Columns.Add("PolicyValDate", GetType(String))
        dtFields.Columns.Add("PolicyLoanDate", GetType(String))
        dtFields.Columns.Add("TotalPolVal", GetType(String))
        dtFields.Columns.Add("BasicCashVal", GetType(String))
        dtFields.Columns.Add("AMLAmount", GetType(String))
        dtFields.Columns.Add("DividendDep", GetType(String))
        dtFields.Columns.Add("DividendInt", GetType(String))
        dtFields.Columns.Add("PUAVal", GetType(String))
        dtFields.Columns.Add("PUACashVal", GetType(String))
        dtFields.Columns.Add("RBVal", GetType(String))
        dtFields.Columns.Add("RBCashVal", GetType(String))
        dtFields.Columns.Add("CouponDep", GetType(String))
        dtFields.Columns.Add("CouponInt", GetType(String))
        dtFields.Columns.Add("PDFAmount", GetType(String))
        dtFields.Columns.Add("PDFInt", GetType(String))
        dtFields.Columns.Add("OPLBalance", GetType(String))
        dtFields.Columns.Add("APLAmount", GetType(String))
        dtFields.Columns.Add("APLInt", GetType(String))
        dtFields.Columns.Add("PolicyLoan", GetType(String))
        dtFields.Columns.Add("PLInt", GetType(String))
        Return dtFields
    End Function
#End Region

#End Region

End Class

