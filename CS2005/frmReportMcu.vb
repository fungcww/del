'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR4101            Add new logic for Post Sales Call follow up letter

Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Threading
Imports System.IO


Public Class frmReportMcu
    Inherits System.Windows.Forms.Form

    Private sqldt As New DataTable
    Private ds As DataSet = New DataSet("Report")
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private rpt As ReportDocument
    Private dr() As DataRow
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private WithEvents cmdExport As System.Windows.Forms.Button
    Private objRptLogic As New clsReportLogicMcu

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'Lubin 2022-10-25 Add ParentFrm property to the report.
        objRptLogic.ParentFrm= Me

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lstReport As System.Windows.Forms.ListBox
    Private WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdFax As System.Windows.Forms.Button
    Friend WithEvents cmdPreview As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdFax = New System.Windows.Forms.Button
        Me.lstReport = New System.Windows.Forms.ListBox
        Me.cmdPreview = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cmdExport = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(137, 394)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
        Me.cmdPrint.TabIndex = 0
        Me.cmdPrint.Text = "Print"
        '
        'cmdFax
        '
        Me.cmdFax.Enabled = False
        Me.cmdFax.Location = New System.Drawing.Point(137, 338)
        Me.cmdFax.Name = "cmdFax"
        Me.cmdFax.Size = New System.Drawing.Size(75, 23)
        Me.cmdFax.TabIndex = 1
        Me.cmdFax.Text = "Fax"
        '
        'lstReport
        '
        Me.lstReport.Location = New System.Drawing.Point(4, 8)
        Me.lstReport.Name = "lstReport"
        Me.lstReport.Size = New System.Drawing.Size(208, 316)
        Me.lstReport.TabIndex = 2
        '
        'cmdPreview
        '
        Me.cmdPreview.Location = New System.Drawing.Point(137, 366)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.Size = New System.Drawing.Size(75, 23)
        Me.cmdPreview.TabIndex = 4
        Me.cmdPreview.Text = "Preview"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 338)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "* Print Only"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.EnableDrillDown = False
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(218, 8)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(518, 437)
        Me.CrystalReportViewer1.TabIndex = 6
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'cmdExport
        '
        Me.cmdExport.Location = New System.Drawing.Point(137, 423)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(75, 23)
        Me.cmdExport.TabIndex = 8
        Me.cmdExport.Text = "Export"
        '
        'frmReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(736, 457)
        Me.Controls.Add(Me.cmdExport)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.lstReport)
        Me.Controls.Add(Me.cmdFax)
        Me.Controls.Add(Me.cmdPrint)
        Me.Name = "frmReport"
        Me.Text = "Reports"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sqlconnect As New SqlConnection
        Dim strSQL, strRptList As String
        Dim sqlda As SqlDataAdapter

        'strRptList = ""

        'ILAS Notification Letter Enhancement
        'strRptList = Strings.Left(strRptList, Strings.Len(strRptList) - 1)
        'strRptList &= "65, 66, 68, 67, 69, 70,71,72"

        'levy
        '20180322 Add Report
        '20180627 Levy Overdue Pre List

        '20210601 ITSR933 R7 APL Loan Letter
        'strRptList &= "65, 66, 68, 67, 69, 70,71,72,73,74, 75, 76, 77"
        'strRptList &= "65, 66, 68, 67, 69, 70,71,72,73,74, 75, 76, 77, 78"
        'strSQL = "Select * from csw_report_list " & _
        '    " Where cswrel_grp_no > 1 " & _
        '    " And cswrel_rpt_no in (" & strRptList & ")" & _
        '    " order by cswrel_file_type, cswrel_rpt_no"

        'strSQL = "Select cswrel_rpt_no, cswrel_disp_name, cswrel_file_name, cswrel_file_type, cswrel_file_path from csw_report_list " & _
        '    " Where cswrel_grp_no > 1 " & _
        '    " order by cswrel_file_type, cswrel_rpt_no"

        'sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
        'sqlconnect.ConnectionString = strCIWMcuConn

        'sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        'sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        'sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        'Try
        '    sqlda.Fill(ds, "csw_report_list")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'Using wsCRS As New CRSWS.CRSWS()
        '    Dim response As New CRSWS.WSResponseOfListOfMCUReportList
        '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
        '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
        '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
        '    End If

        '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    wsCRS.Timeout = 10000000

        '    response = wsCRS.GetReportList(getCompanyCode(g_McuComp), getEnvCode())

        '    If response Is Nothing Or response.Success = False Then
        '        MsgBox("Fail to GetReportList :" + response.ErrorMsg, MsgBoxStyle.Exclamation)
        '    Else
        '        Dim dt As DataTable = ConvertToDataTable(Of CRSWS.MCUReportList)(response.Data)
        '        dt.TableName = "csw_report_list"
        '        ds.Tables.Add(dt)
        '    End If

        'End Using
        Dim dtReportList As DataTable = New DataTable
        If GetReportList(getCompanyCode(g_McuComp), dtReportList) Then
            ds.Tables.Add(dtReportList.Copy())
        End If

        lstReport.DataSource = ds.Tables("csw_report_list")
        lstReport.DisplayMember = "cswrel_disp_name"
        lstReport.ValueMember = "cswrel_rpt_no"
        lstReport_Click(lstReport, New EventArgs())

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdPrint.Enabled = False
        'End If
        ''CRS 7x24 Changes - End
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        wndMain.Cursor = Cursors.WaitCursor
        ' 3b
        objRptLogic.Destination = PrintDest.pdPrinter
        'ILAS Notification Letter Enhancement
        objRptLogic.ReportHeader = lstReport.Text
       

        If LoadReport() AndAlso PrepareData() Then
            '001s
            If String.IsNullOrWhiteSpace(objRptLogic.outputFilePath) Then
                'objRptLogic.CR_Rpt = rpt
                'CallByName(objRptLogic, dr(0).Item("cswrel_file_name"), CallType.Method)
                If Strings.Left(lstReport.Text, 1) <> "*" Then
                    Call PrintReport(rpt, PrintDest.pdPrinter)
                End If
            Else
                Call PrintLetter(PrintDest.pdPrinter)
            End If
            '001e
        End If
        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub cmdFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFax.Click

        If Strings.Left(lstReport.Text, 1) = "*" Then
            MsgBox("Fax function is not available for this report.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor
        Call PrintReport(rpt, PrintDest.pdFax)
        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

        If Strings.Left(lstReport.Text, 1) = "*" Then
            MsgBox("Preview function is not available for this report.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        ' 3b
        objRptLogic.Destination = PrintDest.pdPreview
        'ILAS Notification Letter Enhancement
        objRptLogic.ReportHeader = lstReport.Text

        wndMain.Cursor = Cursors.WaitCursor
        If LoadReport() AndAlso PrepareData() Then
            '001s
            If String.IsNullOrWhiteSpace(objRptLogic.outputFilePath) Then
                Call PrintReport(rpt, PrintDest.pdPreview)
            Else
                Call PrintLetter(PrintDest.pdPreview)
            End If
            '001e
        End If
        wndMain.Cursor = Cursors.Default
        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    CrystalReportViewer1.ShowExportButton = False
        '    CrystalReportViewer1.ShowPrintButton = False
        'Else
        '    CrystalReportViewer1.ShowExportButton = True
        '    CrystalReportViewer1.ShowPrintButton = True
        'End If
        ''CRS 7x24 Changes - End
        CrystalReportViewer1.ShowExportButton = True
        CrystalReportViewer1.ShowPrintButton = True
    End Sub

    Private Function PrepareData() As Boolean

        objRptLogic.CR_Rpt = rpt
        Try
            CallByName(objRptLogic, dr(0).Item("cswrel_file_name"), CallType.Method)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Reports")
        End Try

        Return Not objRptLogic.CancelPrint

    End Function

    Private Function LoadReport() As Boolean

        If lstReport.SelectedIndex < 0 Then
            Return False
        End If

        dr = ds.Tables("csw_report_list").Select("cswrel_rpt_no = " & lstReport.SelectedValue)
        If dr.Length > 0 Then
            Select Case dr(0).Item("cswrel_file_type")
                Case "C"        ' Crystal Report
                    Dim strPath As String

                    strPath = Application.StartupPath
                    'AC - Change to use configuration setting - start
                    'If UAT <> 0 Then
                    '    If InStr(strPath, "\bin") > 0 Then
                    '        strPath = Replace(strPath, "\bin", "")
                    '    End If
                    'End If
                    'AC - Change to use configuration setting - end
                    strPath &= "\Report\" & dr(0).Item("cswrel_file_path")
                    rpt = New ReportDocument
                    rpt.Load(strPath)

                Case "A"        ' CAPSIL function
                Case "P"        ' PDF
                Case "W"        ' Word
                Case "X"        ' Excel/Export
            End Select
        End If

        Return True

    End Function

    Private Sub PrintReport(ByVal rpt As ReportDocument, ByVal strDest As PrintDest)
        Dim needUploadToCM As Boolean = False
        Dim rptName As String

        Select Case strDest

            Case PrintDest.pdPrinter
                ' start, end = 0 to print all
                'rpt.PrintOptions.PrinterName = dr(0).Item("cswrel_ptr_name")
                'rpt.PrintOptions.PrinterName = "\\hkalvdrdev1\HPLJ4100IT1F"
                Try
                    'ITSR933 FG R7 Start
                    rptName = dr(0).Item("cswrel_file_name")
                    needUploadToCM = CheckNeedUploadToCM(rptName)
                    If needUploadToCM Then
                        CallCommonPrintFunction()
                        'ITSR933 FG R7 End
                    Else
                        PrintDialog1.Document = PrintDocument1
                        PrintDialog1.ShowDialog(Me)
                        rpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                        rpt.PrintToPrinter(1, True, 0, 0)
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Reports")
                    Exit Sub
                End Try
                MsgBox("Report Printed Successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Reports")

            Case PrintDest.pdPreview
                Try
                    CrystalReportViewer1.ReportSource = rpt
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Reports")
                End Try

            Case PrintDest.pdFax
                Dim f As New frmFax
                f.FaxDoc(dr(0).Item("cswrel_disp_name")) = dr(0).Item("cswrel_file_path")
                f.ShowDialog()

            Case PrintDest.pdExport

        End Select

    End Sub

    Public Sub ExportReport(ByVal rpt As ReportDocument, ByVal strDest As String, ByVal strFormat As ExportFormatType)

        ' Export
        Dim crExportOptions As ExportOptions
        Dim crDestOptions As New DiskFileDestinationOptions

        crDestOptions.DiskFileName = strDest

        crExportOptions = rpt.ExportOptions
        crExportOptions.DestinationOptions = crDestOptions
        crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
        crExportOptions.ExportFormatType = strFormat

        rpt.Export()

    End Sub

    Private Sub lstReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstReport.Click

        dr = ds.Tables("csw_report_list").Select("cswrel_rpt_no = " & lstReport.SelectedValue)
        If dr.Length > 0 Then
            Select Case dr(0).Item("cswrel_file_type")
                Case "C"        ' Crystal Report
                    cmdFax.Enabled = False
                    cmdPrint.Enabled = True
                    cmdPreview.Enabled = True
                    cmdExport.Enabled = False
                Case "A"        ' CAPSIL function
                    cmdFax.Enabled = False
                    cmdPrint.Enabled = True
                    cmdPreview.Enabled = False
                    cmdExport.Enabled = False
                Case "P", "W"   ' Word / PDF
                    cmdFax.Enabled = True
                    cmdPrint.Enabled = False
                    cmdPreview.Enabled = False
                    cmdExport.Enabled = False
                    '001s
                    Dim rptNo As String = lstReport.SelectedValue.ToString
                    If rptNo = "79" Then
                        cmdFax.Enabled = False
                        cmdPreview.Enabled = True
                        cmdPrint.Enabled = True
                        cmdExport.Enabled = True
                    End If
                    '001e
                    ' **** ES005 begin ****
                Case "E"        ' Export
                    cmdFax.Enabled = False
                    cmdPrint.Enabled = True
                    cmdPreview.Enabled = False
                    cmdExport.Enabled = False
                    ' **** ES005 end ****
                Case "X"        ' Export
                    cmdFax.Enabled = False
                    cmdPrint.Enabled = False
                    cmdPreview.Enabled = False
                    cmdExport.Enabled = True
            End Select
        End If

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdPrint.Enabled = False
        'End If
        ''CRS 7x24 Changes - End

    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click

        wndMain.Cursor = Cursors.WaitCursor
        '01s
        Dim rptNo As String = lstReport.SelectedValue.ToString
        If rptNo = "79" Then
            objRptLogic.Destination = PrintDest.pdExport
        Else
            ' 3b
            objRptLogic.Destination = PrintDest.pdPrinter
        End If
        '01e
        'ILAS Notification Letter Enhancement
        objRptLogic.ReportHeader = lstReport.Text

        If LoadReport() AndAlso PrepareData() Then
            'objRptLogic.CR_Rpt = rpt
            'CallByName(objRptLogic, dr(0).Item("cswrel_file_name"), CallType.Method)
            'If Strings.Left(lstReport.Text, 1) <> "*" Then
            '    Call PrintReport(rpt, PrintDest.pdPrinter)
            'End If

            '01s
            If Not String.IsNullOrWhiteSpace(objRptLogic.outputFilePath) Then
                Call PrintLetter(PrintDest.pdExport)
            End If
            '01e

        End If
        wndMain.Cursor = Cursors.Default

    End Sub

    Private Function CheckNeedUploadToCM(ByVal rptName As String) As Boolean
        If rptName = "AplLoanLetter" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CallCommonPrintFunction()
        PrintDialog1.Document = PrintDocument1
        PrintDialog1.ShowDialog(Me)

        'Common Printing Module
        Dim strErrMsg As String = ""
        Dim listPrintPolicies As List(Of String) = New List(Of String)
        Dim printed As List(Of String) = New List(Of String)
        Dim suppressed As List(Of String) = New List(Of String)
        Dim skipped As List(Of String) = New List(Of String)
        Dim uploaded2CM As List(Of String) = New List(Of String)
        Dim uploaded2CMIndex As List(Of String) = New List(Of String)
        Dim strPolicy1 As String = ""
        If objRptLogic IsNot Nothing Then
            strPolicy1 = objRptLogic.PolicyNo
        End If
        If String.IsNullOrEmpty(strPolicy1) Then Exit Sub
        listPrintPolicies.AddRange(strPolicy1.Replace(" ", "").Split("/"))

        Dim commonPrinting As PosUtils.Crystal.CommonPrinting = New PosUtils.Crystal.CommonPrinting(PrintDialog1.PrinterSettings.PrinterName, "APL Policy Loan Record", "", listPrintPolicies.ToArray(), Now, 0, g_Comp, g_Env)
        If Not (commonPrinting.PrintRpt(rpt, 1, printed, suppressed, skipped, uploaded2CM, uploaded2CMIndex, strErrMsg)) Then
            Throw New Exception(strErrMsg)
        End If
    End Sub

    '001s
    Private Sub PrintLetter(ByVal printType As PrintDest)
        Try
            Dim ltrPrint As LetterPrintBL = New LetterPrintBL
            Dim printerName As String = String.Empty
            Dim printStr As String = String.Empty
            Dim printResult As Boolean = False
            Select Case printType
                Case PrintDest.pdPreview
                    printStr = "Preview"
                    printResult = ltrPrint.SaveFileTo(objRptLogic.outputFilePath)
                Case PrintDest.pdPrinter
                    PrintDialog1.Document = PrintDocument1
                    PrintDialog1.ShowDialog(Me)
                    printerName = PrintDialog1.PrinterSettings.PrinterName
                    printStr = "Printed"
                    printResult = ltrPrint.PrintFile(objRptLogic.outputFilePath, printerName)
                Case PrintDest.pdExport
                    printStr = "Export"
                    printResult = ltrPrint.SaveFileTo(objRptLogic.outputFilePath)
            End Select

            If printResult Then
                MsgBox(String.Format("Letter {0} Successfully.", printStr), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Letter")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Letter")
            Exit Sub
        Finally
            objRptLogic.outputFilePath = String.Empty
        End Try
    End Sub
    '001e

End Class
