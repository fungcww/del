'******************************************************************
' Description : Search GI customer
' Date		  : 7/30/2007
' Author	  : Eric Shu (ES01)
'******************************************************************
' Description : Switch Over Code from Assurance to Bermuda 
' Date		  : 12/15/2023
' Author	  : Oliver Ou
'******************************************************************
Imports System.Data.SqlClient

Public Class frmSearchPolicyAsur
    Inherits System.Windows.Forms.Form

    Private searchResultDt As DataTable
    Private ds As DataSet = New DataSet("PolicyList")
    Private bm As BindingManagerBase
    Private blnAgain As Boolean
    Private lngErr As Long = 0
    Private strErr As String = ""
    ' ES01 begin
    Private blnGI As Boolean = False
    ' ES01 end
    Private objNBA As New NewBusinessAdmin.NBLifeAdmin  ' L/A
    Private SysEventLog As New SysEventLog.clsEventLog
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents grdPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents sboxPolicy As CS2005.SelectBox_Asur
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdPolicy = New System.Windows.Forms.DataGrid()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.sboxPolicy = New CS2005.SelectBox_Asur()
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPolicy
        '
        Me.grdPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolicy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPolicy.BackColor = System.Drawing.Color.White
        Me.grdPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolicy.CaptionVisible = False
        Me.grdPolicy.DataMember = ""
        Me.grdPolicy.FlatMode = True
        Me.grdPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Location = New System.Drawing.Point(8, 8)
        Me.grdPolicy.Name = "grdPolicy"
        Me.grdPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Size = New System.Drawing.Size(648, 160)
        Me.grdPolicy.TabIndex = 3
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.Location = New System.Drawing.Point(664, 96)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(88, 23)
        Me.cmdSearch.TabIndex = 6
        Me.cmdSearch.Text = "&Search"
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClear.Location = New System.Drawing.Point(664, 128)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(88, 23)
        Me.cmdClear.TabIndex = 7
        Me.cmdClear.Text = "&Clear All"
        '
        'cmdOpen
        '
        Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen.Enabled = False
        Me.cmdOpen.Location = New System.Drawing.Point(664, 16)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(88, 23)
        Me.cmdOpen.TabIndex = 8
        Me.cmdOpen.Text = "&Open"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(664, 48)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(88, 23)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Cl&ose"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSearch)
        Me.Panel1.Controls.Add(Me.cmdClear)
        Me.Panel1.Controls.Add(Me.cmdOpen)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Controls.Add(Me.grdPolicy)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(768, 176)
        Me.Panel1.TabIndex = 10
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 176)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(768, 3)
        Me.Splitter1.TabIndex = 11
        Me.Splitter1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.sboxPolicy)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 179)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(768, 274)
        Me.Panel2.TabIndex = 12
        '
        'sboxPolicy
        '
        Me.sboxPolicy.DefaultOp = "like"
        Me.sboxPolicy.FieldName = "PolicyAccountID"
        Me.sboxPolicy.getSelectedValue = "like"
        Me.sboxPolicy.LabelText = "Policy No."
        Me.sboxPolicy.Location = New System.Drawing.Point(13, 12)
        Me.sboxPolicy.Name = "sboxPolicy"
        Me.sboxPolicy.Size = New System.Drawing.Size(1101, 46)
        Me.sboxPolicy.TabIndex = 1
        '
        'frmSearchPolicyAsur
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(768, 453)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmSearchPolicyAsur"
        Me.Text = "Search Policy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal criteria As String = "") Handles cmdSearch.Click

        Dim strCR As String
        Dim lngCnt As Long

        wndMain.StatusBarPanel1.Text = ""

        If Not String.IsNullOrEmpty(criteria) Then
            sboxPolicy.ComboBoxCriteria.SelectedItem = criteria
        End If
        strCR = sboxPolicy.Criteria
        'strCR1 = ""
        If strCR = gError Then
            MsgBox("Invalid Input", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        If strCR = "" Then
            MsgBox("Please enter a criteria", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            sboxPolicy.TextBoxSearchInput1.Focus()
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor

        Dim PolicySearchResult As New DataTable

        Dim SingleQuoteFormatted = sboxPolicy.Criteria("1")
        Dim sqldtM As New DataTable
        Dim lngErrM As Long = 0
        Dim lngCntM As Long = gSearchLimit

        Dim sqldt As New DataTable
        Dim lngErr As Long = 0
        lngCnt = gSearchLimit

        Dim sqldtAsur As New DataTable
        Dim lngErrAsur As Long = 0
        Dim lngCntAsur = gSearchLimit
        Dim strErrAsur As String = String.Empty

        If sboxPolicy.ComboBoxCriteria.SelectedItem = "=" Then
            ' Handle for get Asur policy mapping list
            If GetAsurPolicyMappingListByAPI(g_LacComp, SingleQuoteFormatted, lngErrM, strErr, lngCntM, sqldtM) Then
                If lngErrM <> 0 Then
                    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If

        If sqldtM.Rows.Count > 0 Then
            ' New Policy number found for asur policy mapping
            Dim searchList As New List(Of String)
            For i As Integer = 0 To sqldtM.Rows.Count - 1
                searchList.Add(String.Format("'{0}'", Trim(sqldtM.Rows(i)("cswpm_la_policy").ToString())))
            Next
            ' add original input policy
            searchList.Add(SingleQuoteFormatted)

            For Each itm As String In searchList
                ' Bermuda case
                Dim tempSqldt As New DataTable
                Dim tempLngErr As Long = 0
                Dim tempLngCnt As Long = gSearchLimit
                If GetPolicyListByAPI(getCompanyCode(g_Comp), itm, "", "PH", "POLST", "=", tempLngErr, strErr, tempLngCnt, tempSqldt, True) Then
                    If tempLngErr <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

                If Not tempSqldt Is Nothing Then
                    sqldt.Merge(tempSqldt, False)
                End If

                ' Assurance case
                Dim tempSqldtAsur As New DataTable
                Dim tempLngErrAsur As Long = 0
                Dim tempLngCntAsur = gSearchLimit
                If GetPolicyListByAPI(g_LacComp, itm, "", "PH", "POLST", "=", tempLngErrAsur, strErr, tempLngCntAsur, tempSqldtAsur, True) Then
                    If tempLngErrAsur <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

                If Not tempSqldtAsur Is Nothing Then
                    sqldtAsur.Merge(tempSqldtAsur, False)
                End If
            Next
        Else
            ' No any New Policy number found for asur policy mapping
            If GetPolicyListByAPI(getCompanyCode(g_Comp), SingleQuoteFormatted, "", "PH", "POLST", sboxPolicy.ComboBoxCriteria.SelectedItem, lngErr, strErr, lngCnt, sqldt, True) Then
                If lngErr <> 0 Then
                    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            'sqldtAsur = GetPolicyListCIW("", SingleQuoteFormatted, "PH", "POLST", lngErrAsur, strErrAsur, lngCntAsur, isGetCountOnly, "LAC")
            If GetPolicyListByAPI(g_LacComp, SingleQuoteFormatted, "", "PH", "POLST", sboxPolicy.ComboBoxCriteria.SelectedItem, lngErrAsur, strErrAsur, lngCntAsur, sqldtAsur, True) Then
                If lngErrAsur <> 0 Then
                    MsgBox(strErrAsur, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If

        Dim SearchResult As New DataTable("POLST")
        Dim totalRowCount = 0
        If Not sqldtAsur Is Nothing Then
            SearchResult.Merge(sqldtAsur, False)
            totalRowCount += sqldtAsur.Rows.Count
        End If

        If Not sqldt Is Nothing Then
            SearchResult.Merge(sqldt, False)
            totalRowCount += sqldt.Rows.Count
        End If

        searchResultDt = SearchResult

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            'Exit Sub
        End If

        If lngErrAsur <> 0 Then
            MsgBox(strErrAsur, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            'Exit Sub
        End If

        Dim sqldtGI As New DataTable
        Dim sqldtGIAsur As New DataTable
        Dim totalRowCountGI = 0
        Dim lngCntGI = gSearchLimit
        Dim lngCntGIAsur = gSearchLimit
        Dim GICustomerID As String
        Dim GIAccountStatus As String
        Dim GISearchResult As New DataTable("POLST")
        If totalRowCount = 0 Then

            ' ES01
            ' Search GI policy directly from CIW
            Dim lngErrGI = 0
            Dim strErrGI = ""

            Dim policySearchBL As New PolicySearchBL
            Dim policySearchErr As String = ""
            Dim policySearchAssurErr As String = ""

            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'sqldtGI = GetGIPolicyList("", sboxPolicy.Criteria("1"), "", "POLST", lngErrGI, strErrGI, lngCntGI, True, "ING")
            sqldtGI = GetGIPolicyListAsur("", sboxPolicy.Criteria("1"), "", "POLST", lngErrGI, strErrGI, lngCntGI, True, "ING")

            Dim lngErrGIAsur = 0
            Dim strErrGIAsur = ""

            Dim policySearchBLAsur As New PolicySearchBL
            Dim policySearchErrAsur As String = ""
            Dim policySearchAssurErrAsur As String = ""

            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'sqldtGIAsur = GetGIPolicyList("", sboxPolicy.Criteria("1"), "", "POLST", lngErrGIAsur, strErrGIAsur, lngCntGIAsur, True, "LAC")
            sqldtGIAsur = GetGIPolicyListAsur("", sboxPolicy.Criteria("1"), "", "POLST", lngErrGIAsur, strErrGIAsur, lngCntGIAsur, True, "LAC")

            If Not sqldtGIAsur Is Nothing Then
                GISearchResult.Merge(sqldtGIAsur, False)
                totalRowCountGI += sqldtGIAsur.Rows.Count
            End If
            If Not sqldtGI Is Nothing Then
                GISearchResult.Merge(sqldtGI, False)
                totalRowCountGI += sqldtGIAsur.Rows.Count
            End If

            searchResultDt = GISearchResult

            If lngErrGI <> 0 Then
                'Temp Skip
                MsgBox(strErrGI, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                ' Exit Sub
            End If

            If lngErrAsur <> 0 Then
                MsgBox(strErrGIAsur, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                'Exit Sub
            End If

            If totalRowCountGI = 0 Then
                'Temp Skip
                MsgBox("No Bermuda matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Search Policy")
            Else
                blnGI = True
                GICustomerID = GISearchResult.Rows(0)("CustomerID") & ""
                GIAccountStatus = GISearchResult.Rows(0)("AccountStatus") & ""
            End If

        End If

        If totalRowCountGI > gSearchLimit Then
            MsgBox("Over " & gSearchLimit & " records returned, please re-define your criteria.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            sboxPolicy.TextBoxSearchInput1.Focus()
            Exit Sub
        Else
            If Not GISearchResult Is Nothing Then
                If ds.Tables.Contains("POLST") Then
                    '**If search again, clear existing relation
                    ds.Tables.Remove("POLST")
                End If
                ds.Tables.Add(sqldtGI)
            End If

            If sqldtGI.Rows.Count = 1 Then
                Dim policyNo = ds.Tables("POLST").Rows(0)("PolicyAccountid")

                ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
                'ShowPolicy(policyNo, ds.Tables("POLST").Rows(0)("CompanyID"))
                'ShowPolicy_Assurance(policyNo, ds.Tables("POLST").Rows(0)("CompanyID"))
                OpenPolicySummary(policyNo, ds.Tables("POLST").Rows(0)("CompanyID"))
            End If
        End If

        If Not SearchResult Is Nothing Then
            If ds.Tables.Contains("POLST") Then
                '**If search again, clear existing relation
                ds.Tables.Remove("POLST")
            End If
            ds.Tables.Add(SearchResult)
        End If

        If SearchResult.Rows.Count = 1 Then
            Dim policyNo = SearchResult.Rows(0)("PolicyAccountid")

            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'ShowPolicy(policyNo, SearchResult.Rows(0)("CompanyID"))
            'ShowPolicy_Assurance(policyNo, SearchResult.Rows(0)("CompanyID"))
            'oliver 2024-7-31 added for Com 6
            If SearchResult.Rows(0)("CompanyID") = "BMU" AndAlso Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If

            If Not "Y".Equals(GetUatXml("SKCV")) Then
                Dim strFuncStartTime As String = Now
                Dim strFuncEndTime As String = Now
                Dim isUHNWPolicy As Boolean = False
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                    {"PolicyNo", policyNo}
                    })

                If Not retDs.Tables Is Nothing Then
                    If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                        isUHNWPolicy = True
                    End If
                    If Not "Y".Equals(GetUatXml("SKLV")) Then
                        SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, policyNo, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
                    End If
                End If

                If Not "Y".Equals(GetUatXml("SKLV")) Then
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, policyNo, "isUHNWMember", IIf(isUHNWMember, "1", "0"))
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, policyNo, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))
                End If

                If isUHNWMember And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                ElseIf Not isUHNWMember And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            OpenPolicySummary(policyNo, SearchResult.Rows(0)("CompanyID"))
        End If

        ' Default sorting sequence
        ds.Tables("POLST").DefaultView.Sort = "PolicyAccountID"
        If Not blnAgain Then grdPolicy.DataSource = ds.Tables("POLST")
        bm = Me.BindingContext(ds.Tables("POLST"))
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        If bm.Count > 0 Then
            cmdOpen.Enabled = True
            cmdOpen.Focus()
            Me.AcceptButton = cmdOpen
        Else
            cmdOpen.Enabled = False
        End If

        If Not blnAgain Then Call buildUI()

        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub buildUI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        grdPolicy.TableStyles.Clear()

        'With ds.Tables("POLST")
        '    .Columns.Add("Description", GetType(String))
        'End With

        cs = New DataGridTextBoxColumn
        cs.Width = 220
        cs.MappingName = "Description"
        cs.HeaderText = "Product Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
        'cs.Width = 220
        'cs.MappingName = "Description"
        'cs.HeaderText = "Product Description"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyAccountID"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyEffDate"
        cs.HeaderText = "Effective Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyCurrency"
        cs.HeaderText = "Currency"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)


        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CompanyID"
        cs.HeaderText = "CompanyID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyType"
        cs.HeaderText = "PolicyType"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLST"
        grdPolicy.TableStyles.Add(ts)

        grdPolicy.DataSource = searchResultDt
        grdPolicy.AllowDrop = False
        grdPolicy.ReadOnly = True

    End Sub

    Private Sub frmSearchPolicy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SelectBox1.FieldName = "PolicyAccountID"
        'SelectBox1.LabelText = "Policy No."
        'SelectBox1.InputText = ""
        wndMain.StatusBarPanel1.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click

        blnGI = False   ' ES01
        sboxPolicy.Clear()
        grdPolicy.DataSource = Nothing

        cmdOpen.Enabled = False
        Me.AcceptButton = cmdSearch
        Me.sboxPolicy.Focus()

        wndMain.StatusBarPanel1.Text = ""

        If searchResultDt Is Nothing OrElse searchResultDt.Rows.Count = 0 Then
            Exit Sub
        End If

        Try
            searchResultDt.Clear()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub grdPolicy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
        Dim strPolicy = UCase(RTrim(drI.Item("PolicyAccountID")))
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now

        If String.IsNullOrEmpty(strPolicy) Then
            Return
        End If

        wndMain.Cursor = Cursors.AppStarting

        ' ITSR-4063 
        ' Do Checking for UHNW
        If Not "Y".Equals(GetUatXml("SKCV")) Then
            Dim isUHNWPolicy As Boolean = False
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                    {"PolicyNo", strPolicy}
                    })

            If Not retDs.Tables Is Nothing Then
                If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                    isUHNWPolicy = True
                End If
                If Not "Y".Equals(GetUatXml("SKLV")) Then
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
                End If
            End If

            If Not "Y".Equals(GetUatXml("SKLV")) Then
                SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "isUHNWMember", IIf(isUHNWMember, "1", "0"))
                SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "grdPolicy_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))
            End If

            If isUHNWMember And isUHNWPolicy Then
                MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            ElseIf Not isUHNWMember And isUHNWPolicy Then
                MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                Exit Sub
            End If
        End If

        ' Changed by Hugo Chan on 2021-05-28, "CRS - First Level of Access", search Assurrance and combine result into the 'sqldt'.
        Try
            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'ShowPolicy(strPolicy, Convert.ToString(drI.Item("CompanyID")))
            'ShowPolicy_Assurance(strPolicy, Convert.ToString(drI.Item("CompanyID")))
            'oliver 2024-7-31 added for Com 6
            If Convert.ToString(drI.Item("CompanyID")) = "BMU" AndAlso Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                Exit Sub
            End If
            OpenPolicySummary(strPolicy, Convert.ToString(drI.Item("CompanyID")))
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally
            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda 
            'ShowPolicy(strPolicy, Convert.ToString(drI.Item("CompanyID")))
            'OpenPolicySummary(strPolicy, Convert.ToString(drI.Item("CompanyID")))
        End Try

        Me.AcceptButton = cmdSearch

    End Sub

    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPolicy.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPolicy.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub grdPolicy_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPolicy.DoubleClick

        Call grdPolicy_Click(sender, e)

    End Sub

    Private Sub frmSearchPolicy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        sboxPolicy.TextBoxSearchInput1.Focus()
    End Sub

    Private Sub sboxPolicy_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles sboxPolicy.Enter
        Me.AcceptButton = Me.cmdSearch
    End Sub

    ''' <summary>
    ''' Search Assurrance policies and combine result into the <paramref name="dtBermudaPolicy">Bermuda policy data table</paramref>.
    ''' </summary>
    ''' <param name="sqldt">Policies of Bermuda</param>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-28
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub SearchAndCombineAssurancePolicies(ByVal strPolicyNo As String, ByVal dtBermudaPolicy As DataTable)
        Dim dsAssurancePolicy As DataSet
        Dim lngErr As Long
        Dim strErr As String = String.Empty
        Dim drNew As DataRow

        ' Add a column for indicating records are Assurance or non-Assurance (Bermuda)
        dtBermudaPolicy.Columns.Add("PolicyType", Type.GetType("System.String"))
        If Not dtBermudaPolicy.Columns.Contains("AccountStatus") Then
            dtBermudaPolicy.Columns.Add("AccountStatus", Type.GetType("System.String"))
        End If
        If String.IsNullOrWhiteSpace(strPolicyNo) Then
            Return
        Else
            strPolicyNo = strPolicyNo.Replace("'", "").Replace("%", "")
        End If

        Dim policyCriteria = sboxPolicy.Criteria.Trim()

        dsAssurancePolicy = GetPolicy_Asur(strPolicyNo, "policy", lngErr, strErr, policyCriteria)
        If Not String.IsNullOrWhiteSpace(strErr) Then
            ' Log error
            LogInformation("ERROR", "GetPolicy_Asur", "SearchAndCombineAssurancePolicies", strErr, Nothing)
        End If
        If dsAssurancePolicy Is Nothing Then
            ' Log error
            LogInformation("ERROR", "GetPolicy_Asur", "SearchAndCombineAssurancePolicies", "Data Set is nothing", Nothing)
        Else
            If dsAssurancePolicy.Tables.Count = 0 Then
                ' Log error
                LogInformation("ERROR", "GetPolicy_Asur", "SearchAndCombineAssurancePolicies", "Data Table is nothing", Nothing)
            Else
                ' combine assurance policy result to Bermuda policy data table
                For Each dr As DataRow In dsAssurancePolicy.Tables(0).Rows
                    drNew = dtBermudaPolicy.NewRow()
                    drNew("PolicyType") = "Assurance"
                    drNew("AccountStatus") = dr("Status")
                    drNew("PolicyAccountid") = dr("policy")
                    drNew("ProductID") = dr("Product")
                    drNew("Description") = dr("Product_description")
                    drNew("PolicyRelateCode") = "PH"
                    drNew("PolicyCurrency") = dr("Currency")
                    drNew("PolicyEffDate") = dr("RCD")
                    drNew("PaidToDate") = dr("Pay_to_Date")
                    drNew("PaidMode") = dr("Mode")
                    drNew("POAGCY") = dr("Servicing_Agent")

                    dtBermudaPolicy.Rows.Add(drNew)
                Next
            End If
        End If
    End Sub

End Class

