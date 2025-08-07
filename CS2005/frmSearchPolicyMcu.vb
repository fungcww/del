'******************************************************************
' Description : Policy Search UI for Macau customer
' Date		  : 02/28/2022
' Author	  : Jeff Tam
'******************************************************************

Imports System.Data.SqlClient

Public Class frmSearchPolicyMcu

    Inherits System.Windows.Forms.Form

    Private sqldt As DataTable
    Private ds As DataSet = New DataSet("PolicyList")
    Private bm As BindingManagerBase
    Private blnAgain As Boolean
    Private lngErr As Long = 0
    Private strErr As String = ""
    ' ES01 begin
    Private blnGI As Boolean = False
    ' ES01 end
    Private objNBA As New NewBusinessAdmin.NBLifeAdmin  ' L/A

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
    Friend WithEvents sboxPolicy As CS2005.SelectBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.grdPolicy = New System.Windows.Forms.DataGrid()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.sboxPolicy = New CS2005.SelectBox()
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(392, 48)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Quic&k Open"
        Me.Button1.Visible = False
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
        Me.Panel2.Controls.Add(Me.Button1)
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
        Me.sboxPolicy.Location = New System.Drawing.Point(8, 8)
        Me.sboxPolicy.Name = "sboxPolicy"
        Me.sboxPolicy.Size = New System.Drawing.Size(688, 32)
        Me.sboxPolicy.TabIndex = 1
        '
        'frmSearchPolicyMcu
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(768, 453)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmSearchPolicyMcu"
        Me.Text = "Search Policy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        'Dim sqlconnect As New SqlConnection
        'Dim sqlcmd As New SqlCommand
        'Dim strCNT, strSEL, strSQL, strCR, strCR1 As String
        Dim strCR As String
        Dim strSearchCondition As String
        Dim lngCnt As Long
        'Dim sqlda As SqlDataAdapter

        wndMain.StatusBarPanel1.Text = ""

        'strCNT = "Select count(*) "
        'strSEL = "select Description, policyaccountid, PolicyEffDate, PolicyCurrency "

        ''strSQL = " from policyaccount a, product p " & _
        ''    " where a.productid = p.productid "
        'strSQL = " From PolicyAccount Where 1=1 "

        strCR = sboxPolicy.Criteria

        strSearchCondition = sboxPolicy.getSelectedValue()
        '("3")


        If (strSearchCondition <> "=" And strSearchCondition <> "like") Then
            MsgBox("Please Select '=' or 'like' for search the Policy No. ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            sboxPolicy.TextBox1.Focus()
            ds.Clear()
            Exit Sub
        End If

        If strCR = gError Then
            MsgBox("Invalid Input", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
            'Else
            '    If strCR <> "" Then
            '        strCR1 &= " and " & strCR
            '    End If
        End If

        If strCR = "" Then
            MsgBox("Please enter a criteria", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            sboxPolicy.TextBox1.Focus()
            Exit Sub
            'Else
            '    strSQL &= strCR1
        End If

        'sqlconnect.ConnectionString = strCIWConn
        'sqlconnect.Open()

        'sqlcmd.CommandText = strCNT & strSQL
        'sqlcmd.Connection = sqlconnect

        '' Count the number of records return from CIW, if okay we can then call MQ
        'Try
        '    lngCnt = sqlcmd.ExecuteScalar()
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Finally
        '    sqlconnect.Close()
        'End Try

        wndMain.Cursor = Cursors.WaitCursor

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end
        Try


            'Using wsCRS As New CRSWS.CRSWS()

            '    sqldt = New DataTable()

            '    'wsCRS.DBSOAPHeaderValue = GetCRSWSDBHeader()
            '    'wsCRS.MQSOAPHeaderValue = GetCRSWSMQHeader()
            '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
            '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
            '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            '    End If
            '    'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
            '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            '    wsCRS.Timeout = 10000000


            '    blnGI = False   ' ES01
            '    lngErr = 0
            '    strErr = ""
            '    lngCnt = gSearchLimit

            '    sqldt.TableName = "PolicyAccTbl"

            '    'sqldt = objCS.GetPolicyList("", sboxPolicy.Criteria("1"), "O", "POLST", lngErr, strErr, lngCnt, True)
            '    sqldt = wsCRS.GetPolicyListMCUCIW(getCompanyCode(g_McuComp), getEnvCode(), "", sboxPolicy.Criteria("1"), "PH", "POLST", strSearchCondition, lngErr, strErr, lngCnt, True)

            ' If lngErr <> 0 Then
            '  MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            '  wndMain.Cursor = Cursors.Default
            '  Exit Sub
            ' End If

            'End Using

            If GetPolicyListByAPI(getCompanyCode(g_McuComp), sboxPolicy.Criteria("1"), "", "PH", "POLST", strSearchCondition, lngErr, strErr, lngCnt, sqldt, True) Then
                If lngErr <> 0 Then
                    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        Catch ex As Exception

            MsgBox("Error : " + ex.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")

        End Try


        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        If lngCnt = 0 Then

            ' ES01
            ' Search GI policy directly from CIW
            lngErr = 0
            strErr = ""
            lngCnt = gSearchLimit
            sqldt = GetGIPolicyMcuList(g_McuComp, "", sboxPolicy.Criteria("1"), "", "POLST", lngErr, strErr, lngCnt)

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If

            If lngCnt = 0 Then
                MsgBox("No Bermuda matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Search Policy")
            Else
                blnGI = True
            End If

            'MsgBox("No Matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Search Policy")
            'wndMain.Cursor = Cursors.Default
            'sboxPolicy.TextBox1.Focus()
            'Exit Sub
            ' ES01

        End If

        If lngCnt > gSearchLimit Then
            MsgBox("Over " & gSearchLimit & " records returned, please re-define your criteria.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            sboxPolicy.TextBox1.Focus()
            Exit Sub
        Else
            'sqlda = New SqlDataAdapter("Select ProductID, Description from Product", sqlconnect)
            'sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            'Try
            '    sqlda.Fill(ds, "Product")
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            'Catch sqlex As SqlClient.SqlException
            '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            'End Try

            '#If UAT <> 0 Then
            '            objCS.Env = giEnv
            '#End If
            '            lngErr = 0
            '            strErr = ""
            '            sqldt = objCS.GetPolicyList("", sboxPolicy.Criteria("1"), "O", "POLST", lngErr, strErr, lngCnt)
            If Not sqldt Is Nothing Then
                If ds.Tables.Contains("POLST") Then
                    '**If search again, clear existing relation
                    'ds.Tables("POLST").Constraints.Clear()
                    'ds.Relations.Remove("Product")
                    ds.Tables.Remove("POLST")
                End If
                ds.Tables.Add(sqldt)
            End If
        End If

        SearchAndCombineAssurancePolicies(sboxPolicy.Criteria("1"), sqldt)
        lngCnt = sqldt.Rows.Count

        If lngCnt = 0 Then
            MsgBox("No Matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Search Policy")
            wndMain.Cursor = Cursors.Default
            sboxPolicy.TextBox1.Focus()
            Exit Sub
        End If


        ' Added by Hugo Chan on 2021-05-28, "CRS - First Level of Access", search Assurrance and combine result into the 'sqldt'.
        'SearchAndCombineAssurancePolicies(sboxPolicy.Criteria("1"), sqldt)
        'lngCnt = sqldt.Rows.Count

        'If lngCnt = 0 Then
        '    MsgBox("No Matching Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Search Policy")
        '    wndMain.Cursor = Cursors.Default
        '    sboxPolicy.TextBox1.Focus()
        '    Exit Sub
        'End If


        'sqlcmd.Dispose()
        'sqlconnect.Dispose()

        'Dim relProduct As New Data.DataRelation("Product", ds.Tables("Product").Columns("ProductID"), _
        '    ds.Tables("Result").Columns("ProductID"), True)
        'Try
        '    ds.Relations.Add(relProduct)
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

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

        ts.MappingName = "POLST"
        grdPolicy.TableStyles.Add(ts)

        grdPolicy.DataSource = sqldt
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

        If sqldt Is Nothing OrElse sqldt.Rows.Count = 0 Then
            Exit Sub
        End If

        Try
            sqldt.Clear()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

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
        Dim isUHNWPolicy As Boolean = False
        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                {"PolicyNo", strPolicy}
                })

        If Not retDs.Tables Is Nothing Then
            If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                isUHNWPolicy = True
                InsertVVIPLog(strPolicy, "", "", "Policy Search (MCU)", isUHNWMember)
            End If
            SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicyMcu", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
        End If

        SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicyMcu", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "isUHNWMemberMcu", IIf(isUHNWMemberMcu, "1", "0"))
        SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicyMcu", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strPolicy, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))

        If isUHNWMemberMcu And isUHNWPolicy Then
            MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
        ElseIf Not isUHNWMemberMcu And isUHNWPolicy Then
            MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            Exit Sub
        End If

        ' Changed by Hugo Chan on 2021-05-28, "CRS - First Level of Access", search Assurrance and combine result into the 'sqldt'.
        'ShowPolicy_Assurance(strPolicy)
        'Dim strPolicyType As String = Convert.ToString(drI.Item("PolicyType"))
        'If "Assurance".Equals(strPolicyType, StringComparison.OrdinalIgnoreCase) Then
        '    Dim strStatus As String = Convert.ToString(drI.Item("AccountStatus"))
        '    Dim strProductName As String = Convert.ToString(drI.Item("Description"))
        '    ShowAssurancePolicy(strPolicy)
        'Else
        ShowPolicyMcu(strPolicy, isUHNWPolicy)
        'End If


        'If blnGI = False Then   ' ES01

        '    'With drI
        '    '    If strPolicy <> "" Then
        '    '        Dim w As New frmPolicy
        '    '        w.PolicyAccountID = strPolicy
        '    '        w.Text = "Policy " & strPolicy

        '    '        If ShowWindow(w, wndMain, strPolicy) Then
        '    '            If w.NoRecord Then
        '    '                w.Close()
        '    '                w.Dispose()
        '    '            End If
        '    '        End If
        '    '    End If
        '    'End With
        '    Dim w As New frmPolicy
        '    Dim dsPolicySend As New DataSet
        '    Dim dsPolicyCurr As New DataSet
        '    Dim strTime As String = ""
        '    Dim strerr As String = ""
        '    Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        '    Dim blnGetPolicy As Boolean
        '    Dim dr As DataRow
        '    Dim dtSendData As New DataTable
        '    Dim objMQQueHeader As Utility.Utility.MQHeader
        '    Dim objDBHeader As Utility.Utility.ComHeader
        '    Dim isLifeAsia As Boolean = False

        '    dtSendData.Columns.Add("PolicyNo")
        '    dr = dtSendData.NewRow
        '    dr("PolicyNo") = RTrim(strPolicy)
        '    dtSendData.Rows.Add(dr)

        '    dsPolicySend.Tables.Add(dtSendData)
        '    objMQQueHeader.UserID = gsUser
        '    objMQQueHeader.QueueManager = g_Qman '"LACSQMGR1" '"WINTEL"
        '    objMQQueHeader.RemoteQueue = g_WinRemoteQ '"LACSSIT02.TO.LA400SIT02" '"LIFEASIA.RQ1"
        '    objMQQueHeader.ReplyToQueue = g_LAReplyQ '"LA400SIT02.TO.LACSSIT02" '"WINTEL.RQ1"
        '    objMQQueHeader.LocalQueue = g_WinLocalQ  '"LACSSIT02.QUEUE1.LCL" '"WINTEL.LQ1"
        '    objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout


        '    objMQQueHeader.CompanyID = g_Comp
        '    objMQQueHeader.EnvironmentUse = g_Env
        '    objMQQueHeader.ProjectAlias = "LAS"
        '    objMQQueHeader.UserType = "LASUPDATE"

        '    objDBHeader.UserID = gsUser
        '    objDBHeader.EnvironmentUse = g_Env '"SIT02"
        '    objDBHeader.ProjectAlias = "LAS" '"LAS"
        '    objDBHeader.CompanyID = g_Comp '"ING"
        '    objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        '    clsPOS.MQQueuesHeader = objMQQueHeader
        '    clsPOS.DBHeader = objDBHeader

        '    w.objDBHeader = objDBHeader
        '    w.objMQQueHeader = objMQQueHeader

        '    isLifeAsia = False


        '    w.isProposal = False

        '    If Trim(strPolicy) <> "" Then

        '        If clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr) = True Then
        '            If dsPolicyCurr.Tables.Count > 0 Then
        '                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
        '                    isLifeAsia = True

        '                    w.txtCName.Text = ""
        '                    w.txtCNameChi.Text = ""
        '                    w.txtTitle.Text = ""
        '                    w.txtLastName.Text = ""
        '                    w.txtFirstName.Text = ""
        '                    w.txtChiName.Text = ""
        '                    w.txtPolicy.Text = ""
        '                    w.txtStatus.Text = ""
        '                    w.txtProduct.Text = ""

        '                    w.txtPolicy.Text = RTrim(strPolicy)
        '                    w.txtStatus.Text = dsPolicyCurr.Tables(0).Rows(0)("Risk_Sts")
        '                    w.txtProduct.Text = dsPolicyCurr.Tables(0).Rows(0)("Code")
        '                End If
        '            End If


        '            'Dim objDB As New Object
        '            'Dim ConnectionAlias As String = My.Settings.CompanyID.Trim + "CIW" + My.Settings.Environment
        '            'ConnectDB(objDB, My.Settings.Project, ConnectionAlias, My.Settings.UserType, strerr)
        '            'strCIWConn = objDB.getDBString 'strCIWConn
        '        Else
        '            If InStr(strerr, "Contract not on file") > 0 Then
        '                isLifeAsia = False
        '            ElseIf InStr(strerr, "Policy not in force") > 0 Then
        '                MsgBox("Policy not inforce. ", MsgBoxStyle.Information)
        '                isLifeAsia = True
        '                w.isProposal = True


        '                ' **** ES009 begin ****
        '                ' Check if policy can be found from NBA, if no, it is CNB policy
        '                If GetNBRPolicy(strPolicy, strerr) Then
        '                    Dim objCNB As New frmNewBizAdmin
        '                    objCNB.PolicyNo(g_Comp, g_Env, gsUser) = strPolicy.Trim
        '                    objCNB.Show()
        '                    objCNB.Focus()
        '                Else
        '                    ' **** ES009 end ****

        '                    Dim eKey As New System.Windows.Forms.KeyPressEventArgs(Chr(13))



        '                    If objNBA.IsHandleCreated = False Then
        '                        objNBA = Nothing
        '                        objNBA = New NewBusinessAdmin.NBLifeAdmin

        '                        Dim dtUserAuthority As New DataTable

        '                        gobjDBHeader.UserID = gsUser
        '                        gobjDBHeader.EnvironmentUse = g_Env
        '                        gobjDBHeader.ProjectAlias = "LAS" '"LAS"
        '                        gobjDBHeader.CompanyID = g_Comp '"ING"
        '                        gobjDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        '                        Dim objCI As New LifeClientInterfaceComponent.CommonControl
        '                        objCI.ComHeader = gobjDBHeader
        '                        Call objCI.GetUserAuthority(dtUserAuthority, strerr)

        '                        objNBA.ComHeaderInUse = gobjDBHeader
        '                        'temporary remark since waiting anthony update NBLifeAdmin.vb
        '                        objNBA.dtAuthorityInUse = dtUserAuthority
        '                        objNBA.Text = "New Business Administration (" & gobjDBHeader.CompanyID & ") Main Menu (" & gobjDBHeader.EnvironmentUse & ")"
        '                        objNBA.Show()
        '                        objNBA.txtPolicyNo.Text = RTrim(strPolicy)
        '                        Call objNBA.txtPolicyNo_KeyPress(Nothing, eKey)
        '                        objNBA.Visible = True
        '                        objNBA.Focus()
        '                    Else
        '                        objNBA.txtPolicyNo.Text = RTrim(strPolicy)
        '                        Call objNBA.txtPolicyNo_KeyPress(Nothing, eKey)
        '                        objNBA.Visible = True
        '                        objNBA.Focus()
        '                    End If
        '                End If 'ES009

        '                'Dim eKey As New System.Windows.Forms.KeyPressEventArgs(Chr(13))

        '                'If objNBA.IsHandleCreated = False Then
        '                '    objNBA = Nothing
        '                '    objNBA = New NewBusinessAdmin.NBLifeAdmin

        '                '    Dim dtUserAuthority As New DataTable

        '                '    gobjDBHeader.UserID = gsUser
        '                '    gobjDBHeader.EnvironmentUse = g_Env
        '                '    gobjDBHeader.ProjectAlias = "LAS" '"LAS"
        '                '    gobjDBHeader.CompanyID = g_Comp '"ING"
        '                '    gobjDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        '                '    Dim objCI As New LifeClientInterfaceComponent.CommonControl
        '                '    objCI.ComHeader = gobjDBHeader
        '                '    Call objCI.GetUserAuthority(dtUserAuthority, strerr)

        '                '    objNBA.ComHeaderInUse = gobjDBHeader
        '                '    'temporary remark since waiting anthony update NBLifeAdmin.vb
        '                '    objNBA.dtAuthorityInUse = dtUserAuthority
        '                '    objNBA.Text = "New Business Administration (" & gobjDBHeader.CompanyID & ") Main Menu (" & gobjDBHeader.EnvironmentUse & ")"
        '                '    objNBA.Show()
        '                '    objNBA.txtPolicyNo.Text = RTrim(strPolicy)
        '                '    Call objNBA.txtPolicyNo_KeyPress(sender, eKey)
        '                '    objNBA.Visible = True
        '                '    objNBA.Focus()
        '                'Else
        '                '    objNBA.txtPolicyNo.Text = RTrim(strPolicy)
        '                '    Call objNBA.txtPolicyNo_KeyPress(sender, eKey)
        '                '    objNBA.Visible = True
        '                '    objNBA.Focus()
        '                'End If
        '            Else
        '                MsgBox(strerr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '            End If
        '        End If

        '        w.isLifeAsia = isLifeAsia

        '        w.PolicyAccountID = RTrim(strPolicy)
        '        w.Text = "Policy " & RTrim(strPolicy)
        '        w.Ctrl_ChgComponent1.SysTableInUse = dsComponentSysTable

        '        If ShowWindow(w, wndMain, RTrim(strPolicy)) Then
        '            If w.NoRecord Then
        '                'start update by kit 
        '                'w.Close()
        '                'w.Dispose()

        '                'Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = strPolicyNo.Trim
        '                'Ctrl_CRSPolicyGeneral_Information1.showPolicyGeneralInfo()

        '                'end update by kit
        '            End If
        '            'txtPolicy.Text = ""
        '        End If

        '        If w.isProposal = True Then
        '            objNBA.Focus()
        '        End If
        '    End If

        'Else

        '    If strPolicy <> "" Then
        '        Dim w As New frmGIPolicy
        '        w.PolicyAccountID(Strings.Right(drI.Item("PolicyAccountid"), 3), drI.Item("CustomerID"), _
        '                drI.Item("AccountStatus")) = Strings.Left(drI.Item("PolicyAccountid"), 13)
        '        ShowWindow(w, wndMain, Trim(Strings.Left(drI.Item("PolicyAccountid"), 13)))
        '        If w.NoRecord = True Then
        '            w.Close()
        '            w.Dispose()
        '        End If
        '    End If

        'End If

        'wndMain.Cursor = Cursors.Default
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

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim strPolicy As String
        strPolicy = UCase(sboxPolicy.Criteria("2"))

        Dim w As New frmPolicyMcu
        w.PolicyAccountID = strPolicy
        w.Text = "Policy " & strPolicy

        If ShowWindow(w, wndMain, strPolicy) Then
            If w.NoRecord Then
                w.Close()
                w.Dispose()
            End If
        End If

        'Dim fs() As Form = wndMain.MdiChildren()
        'Dim f As Form
        'Dim blnWinFound As Boolean

        'For Each f In fs
        '    If InStr(f.Text, strPolicy) <> 0 Then
        '        blnWinFound = True
        '        Exit For
        '    End If
        'Next

        'If blnWinFound Then
        '    f.Focus()
        'Else
        '    Dim w As New frmPolicy
        '    w.PolicyAccountID = strPolicy
        '    w.Text = "Policy " & strPolicy
        '    If Not OpenWindow(w, wndMain) Then
        '        w.Dispose()
        '    End If
        '    If w.NoRecord Then
        '        w.Close()
        '        w.Dispose()
        '    End If
        '    'w.MdiParent = wndMain
        '    'w.Show()
        'End If

    End Sub

    Private Sub grdPolicy_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPolicy.DoubleClick

        Call Button2_Click(sender, e)

    End Sub

    Private Sub frmSearchPolicy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        sboxPolicy.TextBox1.Focus()
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
        If Not dtBermudaPolicy.Columns.Contains("PolicyType") Then
            dtBermudaPolicy.Columns.Add("PolicyType", Type.GetType("System.String"))
        End If
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