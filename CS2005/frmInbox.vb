'**************************
'Date : Jun 11, 2015
'Author : Kay Tsang KT20150611
'Purpose add new combo box for searching, button for exporting pending cases
' added order by clause for custom searching
' combo box/ check box interaction behavior for pending/handoff
'**************************
'**************************
'Date : Aug 30, 2023
'Author : oliver ou 222834
'Phase 3 Point A-3(CRS Enhancement)
'Allow user To update service log And save In front page
'**************************
'**************************
'Date : Dec 15, 2023
'Author : oliver ou 222834
'Switch Over Code from Assurance to Bermuda 
'**************************
Imports System.Data.SqlClient

Public Class frmInbox
    Inherits System.Windows.Forms.Form

    Private dtInbox, dtApp, dtCSR, dtCSR2 As New DataTable 'KT20150611 added dtCSR2
    'Private ds As DataSet = New DataSet("Inbox")
    Private bm As BindingManagerBase
    Private WithEvents bmApp As BindingManagerBase
    Private lngErr As Long = 0
    Friend WithEvents cboHandOff As System.Windows.Forms.ComboBox
    Friend WithEvents cboPending As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Private strErr As String = ""
    Private wGIServiceLog As frmGIServiceLog


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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdInbox As System.Windows.Forms.DataGrid
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdApp As System.Windows.Forms.DataGrid
    Friend WithEvents uclCampaignTracking As CS2005.uclCampaignTracking
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents chkHandoff As System.Windows.Forms.CheckBox
    Friend WithEvents chkPending As System.Windows.Forms.CheckBox
    Friend WithEvents chkComp As System.Windows.Forms.CheckBox
    Friend WithEvents optCust As System.Windows.Forms.RadioButton
    Friend WithEvents optInbox As System.Windows.Forms.RadioButton
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboPending = New System.Windows.Forms.ComboBox
        Me.cboHandOff = New System.Windows.Forms.ComboBox
        Me.txtTDate = New System.Windows.Forms.DateTimePicker
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.chkHandoff = New System.Windows.Forms.CheckBox
        Me.chkPending = New System.Windows.Forms.CheckBox
        Me.chkComp = New System.Windows.Forms.CheckBox
        Me.optCust = New System.Windows.Forms.RadioButton
        Me.optInbox = New System.Windows.Forms.RadioButton
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.grdInbox = New System.Windows.Forms.DataGrid
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.uclCampaignTracking = New CS2005.uclCampaignTracking
        Me.grdApp = New System.Windows.Forms.DataGrid
        Me.cmdExport = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdInbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdApp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1035, 496)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.txtRemark)
        Me.TabPage1.Controls.Add(Me.grdInbox)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1027, 470)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Service Log"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmdExport)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.cboPending)
        Me.GroupBox1.Controls.Add(Me.cboHandOff)
        Me.GroupBox1.Controls.Add(Me.txtTDate)
        Me.GroupBox1.Controls.Add(Me.txtFDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmdRefresh)
        Me.GroupBox1.Controls.Add(Me.chkHandoff)
        Me.GroupBox1.Controls.Add(Me.chkPending)
        Me.GroupBox1.Controls.Add(Me.chkComp)
        Me.GroupBox1.Controls.Add(Me.optCust)
        Me.GroupBox1.Controls.Add(Me.optInbox)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 400)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1023, 68)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Searching"
        '
        'cboPending
        '
        Me.cboPending.FormattingEnabled = True
        Me.cboPending.Location = New System.Drawing.Point(407, 17)
        Me.cboPending.Name = "cboPending"
        Me.cboPending.Size = New System.Drawing.Size(194, 21)
        Me.cboPending.TabIndex = 17
        '
        'cboHandOff
        '
        Me.cboHandOff.FormattingEnabled = True
        Me.cboHandOff.Location = New System.Drawing.Point(692, 15)
        Me.cboHandOff.Name = "cboHandOff"
        Me.cboHandOff.Size = New System.Drawing.Size(194, 21)
        Me.cboHandOff.TabIndex = 15
        '
        'txtTDate
        '
        Me.txtTDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtTDate.Location = New System.Drawing.Point(424, 40)
        Me.txtTDate.Name = "txtTDate"
        Me.txtTDate.Size = New System.Drawing.Size(132, 20)
        Me.txtTDate.TabIndex = 12
        '
        'txtFDate
        '
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFDate.Location = New System.Drawing.Point(268, 40)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(236, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "From"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(404, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "to"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(560, 40)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(75, 23)
        Me.cmdRefresh.TabIndex = 8
        Me.cmdRefresh.Text = "Refresh"
        '
        'chkHandoff
        '
        Me.chkHandoff.Location = New System.Drawing.Point(622, 15)
        Me.chkHandoff.Name = "chkHandoff"
        Me.chkHandoff.Size = New System.Drawing.Size(76, 24)
        Me.chkHandoff.TabIndex = 4
        Me.chkHandoff.Text = "Handoff"
        '
        'chkPending
        '
        Me.chkPending.Location = New System.Drawing.Point(340, 16)
        Me.chkPending.Name = "chkPending"
        Me.chkPending.Size = New System.Drawing.Size(104, 24)
        Me.chkPending.TabIndex = 3
        Me.chkPending.Text = "Pending"
        '
        'chkComp
        '
        Me.chkComp.Location = New System.Drawing.Point(236, 16)
        Me.chkComp.Name = "chkComp"
        Me.chkComp.Size = New System.Drawing.Size(104, 24)
        Me.chkComp.TabIndex = 2
        Me.chkComp.Text = "Completed"
        '
        'optCust
        '
        Me.optCust.Location = New System.Drawing.Point(132, 16)
        Me.optCust.Name = "optCust"
        Me.optCust.Size = New System.Drawing.Size(104, 24)
        Me.optCust.TabIndex = 1
        Me.optCust.Text = "Custom"
        '
        'optInbox
        '
        Me.optInbox.Checked = True
        Me.optInbox.Location = New System.Drawing.Point(8, 16)
        Me.optInbox.Name = "optInbox"
        Me.optInbox.Size = New System.Drawing.Size(104, 24)
        Me.optInbox.TabIndex = 0
        Me.optInbox.TabStop = True
        Me.optInbox.Text = "Inbox Default"
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Location = New System.Drawing.Point(0, 336)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.Size = New System.Drawing.Size(1023, 60)
        Me.txtRemark.TabIndex = 3
        '
        'grdInbox
        '
        Me.grdInbox.AlternatingBackColor = System.Drawing.Color.White
        Me.grdInbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdInbox.BackColor = System.Drawing.Color.White
        Me.grdInbox.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdInbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdInbox.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInbox.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdInbox.CaptionVisible = False
        Me.grdInbox.DataMember = ""
        Me.grdInbox.FlatMode = True
        Me.grdInbox.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdInbox.ForeColor = System.Drawing.Color.Black
        Me.grdInbox.GridLineColor = System.Drawing.Color.Wheat
        Me.grdInbox.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdInbox.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdInbox.HeaderForeColor = System.Drawing.Color.Black
        Me.grdInbox.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInbox.Location = New System.Drawing.Point(0, 0)
        Me.grdInbox.Name = "grdInbox"
        Me.grdInbox.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdInbox.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdInbox.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdInbox.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInbox.Size = New System.Drawing.Size(1027, 332)
        Me.grdInbox.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.uclCampaignTracking)
        Me.TabPage2.Controls.Add(Me.grdApp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(1027, 470)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Appointment"
        '
        'uclCampaignTracking
        '
        Me.uclCampaignTracking.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.uclCampaignTracking.Location = New System.Drawing.Point(0, 238)
        Me.uclCampaignTracking.Name = "uclCampaignTracking"
        Me.uclCampaignTracking.ReadOnlyMode = False
        Me.uclCampaignTracking.Size = New System.Drawing.Size(1027, 232)
        Me.uclCampaignTracking.TabIndex = 3
        '
        'grdApp
        '
        Me.grdApp.AlternatingBackColor = System.Drawing.Color.White
        Me.grdApp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdApp.BackColor = System.Drawing.Color.White
        Me.grdApp.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdApp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdApp.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdApp.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdApp.CaptionVisible = False
        Me.grdApp.DataMember = ""
        Me.grdApp.FlatMode = True
        Me.grdApp.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdApp.ForeColor = System.Drawing.Color.Black
        Me.grdApp.GridLineColor = System.Drawing.Color.Wheat
        Me.grdApp.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdApp.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdApp.HeaderForeColor = System.Drawing.Color.Black
        Me.grdApp.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdApp.Location = New System.Drawing.Point(0, 0)
        Me.grdApp.Name = "grdApp"
        Me.grdApp.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdApp.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdApp.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdApp.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdApp.Size = New System.Drawing.Size(1027, 236)
        Me.grdApp.TabIndex = 2
        '
        'cmdExport
        '
        Me.cmdExport.Location = New System.Drawing.Point(679, 41)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(103, 23)
        Me.cmdExport.TabIndex = 9
        Me.cmdExport.Text = "Export Pending"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(798, 41)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(103, 23)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save Event Note"
        '
        'frmInbox
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1009, 497)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmInbox"
        Me.Text = "ITDTMS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdInbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdApp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmInbox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = UCase(gsUser)
        txtTDate.Value = Today
        txtFDate.Value = DateAdd(DateInterval.Month, -1, Today)
        dtInbox = SelectRecord()
        dtApp = SelectApp()
        Call FillInbox()
        Call UpdateReminder()
        Call FillApp()

        Dim strSQL, strError As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        'strSQL = "select csrid, case when name = '' then csrid else '(' + csrid + ') ' + name end as cname from csr order by name"
        'KT20150611 check name for null
        strSQL = "select csrid, case when name is null or name = '' then csrid else '(' + csrid + ') ' + name end as cname from " & serverPrefix & "csr order by name"
        If GetDT(strSQL, strCIWConn, dtCSR, strError) Then
            cboHandOff.DataSource = dtCSR
            cboHandOff.DisplayMember = "cname"
            cboHandOff.ValueMember = "CSRID"
            cboHandOff.SelectedIndex = -1 'KT20150611
        End If
        'KT20150611
        If GetDT(strSQL, strCIWConn, dtCSR2, strError) Then
            cboPending.DataSource = dtCSR2
            cboPending.DisplayMember = "cname"
            cboPending.ValueMember = "CSRID"
            cboPending.SelectedIndex = -1
        End If
        'KT20150611

    End Sub

    Private Function SelectRecord() As DataTable

        Dim dt As New DataTable
        Dim dtAsur As New DataTable
        dt = SelectRecord("ING")
        dtAsur = SelectRecord("LAC")
        If dtAsur.Rows.Count > 0 Then
            Dim dtCompanyIDColumn As New Data.DataColumn("CompanyID", GetType(System.String))
            dtCompanyIDColumn.DefaultValue = "Bermuda"
            dt.Columns.Add(dtCompanyIDColumn)
            Dim dtAsurCompanyIDColumn As New Data.DataColumn("CompanyID", GetType(System.String))
            dtAsurCompanyIDColumn.DefaultValue = "Assurance"
            dtAsur.Columns.Add(dtAsurCompanyIDColumn)
            dt.Merge(dtAsur)
        End If
        Return dt

    End Function


    Private Function SelectRecord(ByVal companyID As String) As DataTable

        Dim strSQL, strWhere As String
        Dim sqldt As New DataTable("ServiceLog")
        Dim sqlda As SqlDataAdapter
        Dim sqlconnect As New SqlConnection
        Dim strSecdUser As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        Dim POSDB As String = gcPOS
        If companyID = "LAC" OrElse companyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
            POSDB = Utility.Utility.GetDbNameByCompanyEnv_POS(companyID, g_LacEnv) & ".."
        ElseIf companyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If
        ' Phase 3 Point A-3(CRS Enhancement)
        ' add s.ServiceEventNumberAdd to the end of the select statement
        ' updated at 2023-8-29 by oliver, which is Allow user to update service log and save in front page 
        strSQL = "SELECT s.EventInitialDateTime as 'Initial Date', " &
                 "sc.Eventstatus as 'Status', cs.Name as 'Sender', " &
                 "c.cswecc_desc as 'Event Category', t.EventTypeDesc as 'Event Detail', " &
                 "d.csw_event_typedtl_desc as 'Event Type Detail', " &
                 "case when left(p.policyaccountid,2)='GL' then isnull(left(substring(p.PolicyAccountID,3,LEN(p.policyAccountId)),12),'') else isnull(left(p.PolicyAccountID,12),'') end as 'Policy ID', " &
                 "case when p.companyid in ('EAA','ING','BMU') then 'LIFE' when p.CompanyID in ('EB','GL','LTD') then 'EB' when p.CompanyID in ('GI') then 'GI' else p.CompanyID end as 'Policy Type', " &
                 "(Case when s.ReminderNotes is null then '' else s.ReminderNotes End) as 'Reminder Note', " &
                 "(Case when s.EventNotes is null then '' else s.EventNotes End) as 'Event Note', s.CustomerID, cus.agentcode, cus.FirstName, cus.NameSuffix ,s.ServiceEventNumber " &
                 "FROM ServiceEventDetail s with (nolock) left join " & POSDB & "vw_csw_event_category_code  c " &
                 "on s.EventCategoryCode = c.cswecc_code " &
                 "left join ServiceEventTypeCodes t " &
                 "on s.EventCategoryCode = t.EventCategoryCode and s.EventTypeCode = t.EventTypeCode " &
                 "left join " & POSDB & "vw_csw_event_typedtl_code d  " &
                 "on s.EventCategoryCode = d.csw_event_category_code and " &
                 "s.EventTypeCode = d.csw_event_type_code and " &
                 "s.EventTypeDetailCode = d.csw_event_typedtl_code " &
                 "left join " & serverPrefix & "EventStatusCodes sc " &
                 "on s.EventStatusCode = sc.EventStatusCode " &
                 "left join " & serverPrefix & " CSR cs on s.MasterCSRID = cs.CSRID " &
                 "left join PolicyAccount p on s.PolicyAccountID=p.PolicyAccountID " &
                 "left join customer cus on s.customerid = cus.CustomerID "

        If optInbox.Checked Then
            'strSQL &= "WHERE (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') or " & _
            '     "(s.SecondaryCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'H') or " & _
            '     "(s.MasterCSRID = '" & Trim(gsUser) & "' and s.Reminderdate <= convert(varchar, getdate(), 101) and s.ReminderNotes <> '' and s.Reminderdate > '19000101') " & _
            '     "order by s.EventStatusCode asc, s.EventInitialDateTime desc "

            strSQL &= "WHERE ( (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') or " &
                        "(s.SecondaryCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'H') or " &
                        "(s.MasterCSRID = '" & Trim(gsUser) & "' and s.Reminderdate <= convert(varchar, getdate(), 101) and s.ReminderNotes <> '' and s.Reminderdate > '19000101')  ) "
            strSQL &= " and s.ServiceEventNumber not in ( select ServiceEventNumber from " & serverPrefix & "ServiceEventDetailMCU_Extend ) " &
                      " order by s.EventStatusCode asc, s.EventInitialDateTime desc "

        ElseIf optCust.Checked Then
            strSQL &= "WHERE convert(varchar,s.EventInitialDateTime,112) between '" & Format(txtFDate.Value, "yyyyMMdd") & "' AND '" & Format(txtTDate.Value, "yyyyMMdd") & "'"

            'Exclude Macau Service Log
            strSQL &= " and s.ServiceEventNumber not in ( select ServiceEventNumber from " & serverPrefix & "ServiceEventDetailMCU_Extend ) "

            If chkComp.Checked Then
                strWhere = " (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'C') "
            End If

            If chkPending.Checked Then

                'KT20150611
                If cboPending.SelectedValue <> Trim(gsUser) Then
                    If strWhere = "" Then
                        strWhere = " (cs.CSRID = '" & Trim(cboPending.SelectedValue) & "' and s.EventStatusCode = 'P') "
                    Else
                        strWhere &= " or (cs.CSRID = '" & Trim(cboPending.SelectedValue) & "' and s.EventStatusCode = 'P') "
                    End If
                Else
                    If strWhere = "" Then
                        strWhere = " (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') "
                    Else
                        strWhere &= " or (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') "
                    End If
                End If
                'KT20150611

                'If strWhere = "" Then
                '    strWhere = " (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') "
                'Else
                '    strWhere &= " or (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') "
                'End If



            End If

            If chkHandoff.Checked Then
                strSecdUser = gsUser
                If cboHandOff.SelectedValue <> "" Then
                    strSecdUser = Trim(cboHandOff.SelectedValue)
                End If
                If strWhere = "" Then
                    'strWhere = " (s.SecondaryCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'H') "
                    strWhere = " (s.SecondaryCSRID = '" & Trim(strSecdUser) & "' and s.EventStatusCode = 'H') "
                Else
                    'strWhere &= " or (s.SecondaryCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'H') "
                    strWhere &= " or (s.SecondaryCSRID = '" & Trim(strSecdUser) & "' and s.EventStatusCode = 'H') "
                End If
            End If

            If strWhere <> "" Then
                strSQL &= " AND (" & strWhere & ")"
            Else
                strSQL &= " AND s.MasterCSRID = '" & Trim(gsUser) & "'"
            End If

            'KT20150611
            strSQL &= "order by s.EventStatusCode asc, s.EventInitialDateTime desc "

        End If

        sqlconnect.ConnectionString = GetConnectionStringByCompanyID(companyID)
        'sqlda = New SqlDataAdapter(strSQL, sqlconnect)        
        sqlda = New SqlDataAdapter

        Dim sqlcmd As New SqlCommand
        sqlcmd.Connection = sqlconnect
        sqlcmd.CommandTimeout = gQryTimeOut
        sqlcmd.CommandText = strSQL

        sqlda.SelectCommand = sqlcmd

        Try
            sqlda.Fill(sqldt)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ex Error")
        End Try

        Return sqldt

    End Function

    Private Sub FillInbox()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "Initial Date"
        cs.HeaderText = "Initial Date"
        'cs.Format = gDateTimeFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Sender"
        cs.HeaderText = "Sender"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Event Category"
        cs.HeaderText = "Event Category"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Event Detail"
        cs.HeaderText = "Event Detail"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "Event Type Detail"
        cs.HeaderText = "Event Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Policy ID"
        cs.HeaderText = "Policy ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "Policy Type"
        cs.HeaderText = "Policy Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "Reminder Note"
        cs.HeaderText = "Reminder Note"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "Event Note"
        cs.HeaderText = "Event Note"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CustomerID"
        cs.HeaderText = "CustomerID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "agentcode"
        cs.HeaderText = "Agent Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "FirstName"
        cs.HeaderText = "First Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "NameSuffix"
        cs.HeaderText = "Name Suffix"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "ServiceLog"
        Me.grdInbox.TableStyles.Clear()
        Me.grdInbox.TableStyles.Add(ts)

        bm = Me.BindingContext(dtInbox)
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        grdInbox.DataSource = dtInbox
        grdInbox.AllowDrop = False
        grdInbox.ReadOnly = True
        'txtRemark.DataBindings.Add("text", dtInbox, "Event Note")

    End Sub

    Private Sub FillApp()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "AppointmentDate"
        cs.HeaderText = "Appointment Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CampaignID"
        cs.HeaderText = "Campaign ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "CampaignName"
        cs.HeaderText = "Campaign Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "ChannelDesc"
        cs.HeaderText = "Channel"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "HandoffBy"
        cs.HeaderText = "Handoff By"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "HandoffDate"
        cs.HeaderText = "Handoff Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CustomerID"
        cs.HeaderText = "Customer ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "Comment"
        cs.HeaderText = "Comment"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "Appointment"
        Me.grdApp.TableStyles.Clear()
        Me.grdApp.TableStyles.Add(ts)

        grdApp.DataSource = dtApp
        dtApp.DefaultView.Sort = "AppointmentDate"
        grdApp.AllowDrop = False
        grdApp.ReadOnly = True

        bmApp = Me.BindingContext(dtApp)

        Call UpdateControls()

    End Sub

    Private Sub DataGrid1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdInbox.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdInbox.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdInbox.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdInbox.Select(hti.Row)
        End If
    End Sub

    Private Sub grdAlert_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdInbox.CurrentCellChanged
        Call UpdateReminder()
    End Sub

    Private Sub UpdateReminder()

        If bm.Count > 0 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

            If Not drI Is Nothing Then
                txtRemark.Text = drI.Item("Event Note")
            End If
        End If

    End Sub

    'grdInbox_DoubleClick() - Handles the double click event in grdInbox, open the selected Service Log's policy
    Private Sub grdInbox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdInbox.DoubleClick

        Dim strOpenPolicy, strPolicyType, strColName, strCustomerID, strClientID, strFirstName, strNameSuffix, strCompanyID As String
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now

        strColName = dtInbox.Rows(grdInbox.CurrentRowIndex).Table.Columns(grdInbox.CurrentCell.ColumnNumber).ColumnName.ToUpper()
        Dim dr As DataRow = CType(bm.Current, DataRowView).Row()
        strCustomerID = Trim(If(String.IsNullOrEmpty(dr.Item("CustomerID").ToString()), "", dr.Item("CustomerID")))
        strOpenPolicy = Trim(dr.Item("Policy ID").ToString())

        If strColName = "CUSTOMERID" OrElse String.IsNullOrEmpty(strOpenPolicy) Then
            If Not String.IsNullOrEmpty(strCustomerID) Then
                strClientID = Trim(If(strColName = "CUSTOMERID", If(String.IsNullOrEmpty(dr.Item("agentcode").ToString()), "", dr.Item("agentcode")), GetClientID(strCustomerID)))
                strFirstName = Trim(If(String.IsNullOrEmpty(dr.Item("FirstName").ToString()), "", dr.Item("FirstName")))
                strNameSuffix = Trim(If(String.IsNullOrEmpty(dr.Item("NameSuffix").ToString()), "", dr.Item("NameSuffix")))
                strCompanyID = Trim(If(dtInbox.Columns.Contains("CompanyID") AndAlso Not String.IsNullOrEmpty(dr.Item("CompanyID").ToString()), dr.Item("CompanyID"), "Bermuda"))
                Dim strName As String = Trim($"{strNameSuffix} {strFirstName}")

                ' Open customer form
                Dim bl As New CustomerSearchBL()
                bl.ShowCustomerCentric(strName, strCustomerID, strClientID, strCompanyID, Nothing, "Inbox Customer (HK)", False)
            End If
        Else
            ' Open policy form
            If Not String.IsNullOrEmpty(strOpenPolicy) Then

                ' Do Checking for UHNW
                Dim isUHNWPolicy As Boolean = False
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "VERIFY_UHNWPOLICY", New Dictionary(Of String, String)() From {
                        {"PolicyNo", strOpenPolicy}
                        })

                If Not retDs.Tables Is Nothing Then
                    If retDs.Tables(0).Rows.Count > 0 And Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
                        isUHNWPolicy = True
                        InsertVVIPLog(strOpenPolicy, "", "", "Inbox Policy (HK)", isUHNWMember)
                    End If
                    SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strOpenPolicy, "Record Count", retDs.Tables(0).Rows(0).Item("Count"))
                End If

                SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strOpenPolicy, "isUHNWMember", IIf(isUHNWMember, "1", "0"))
                SysEventLog.WritePerLog(gsUser, "CS2005.frmSearchPolicy", "Button2_Click", "VERIFY_UHNWPOLICY", strFuncStartTime, strFuncEndTime, strOpenPolicy, "isUHNWPolicy", IIf(isUHNWPolicy, "1", "0"))

                If isUHNWMember And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                ElseIf Not isUHNWMember And isUHNWPolicy Then
                    MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                    Exit Sub
                End If

                strPolicyType = LTrim(RTrim(dr.Item("Policy Type")))
                If strPolicyType = "LIFE" OrElse strPolicyType = "LAC" Or strPolicyType = "LAH" Then
                    ' oliver 2023-12-4 added for Switch Over Code from Assurance to Bermuda
                    'ShowPolicy(strOpenPolicy)
                    strCompanyID = "Bermuda"
                    If dtInbox.Columns.Contains("CompanyID") Then
                        strCompanyID = RTrim(If(String.IsNullOrEmpty(dr.Item("CompanyID").ToString()), "Bermuda", dr.Item("CompanyID")))
                    End If
                    ShowPolicy_Assurance(RTrim(strOpenPolicy), If(strCompanyID = "Bermuda", "ING", strPolicyType))
                ElseIf strPolicyType = "EB" Then
                    'strOpenPolicy = checkPolicyEndDate(strOpenPolicy)
                    If Microsoft.VisualBasic.Left(strOpenPolicy, 2) = "12" Or Microsoft.VisualBasic.Left(strOpenPolicy, 2) = "22" _
                                   Or Microsoft.VisualBasic.Left(strOpenPolicy, 2) = "13" Or Microsoft.VisualBasic.Left(strOpenPolicy, 2) = "23" Then
                        strOpenPolicy = GLCheckPolicyEndDate(strOpenPolicy)
                    Else
                        strOpenPolicy = checkPolicyEndDate(strOpenPolicy)
                    End If

                    If ShowEBDetailPage(strOpenPolicy) Then
                        Call GIServiceLog(strOpenPolicy)
                    End If
                ElseIf strPolicyType = "GI" Then
                    strOpenPolicy = checkPolicyEndDate(strOpenPolicy)
                    If ShowGIDetailPage(strOpenPolicy) Then
                        Call GIServiceLog(strOpenPolicy)
                    End If
                End If
            End If

        End If

    End Sub

    Public Sub RefreshInbox()
        'dtInbox.Clear()
        'sqlda.Fill(dtInbox)
        dtInbox = SelectRecord()
        'grdInbox.DataSource = dtInbox
        Call FillInbox()
        Call UpdateReminder()
        dtApp = SelectApp()
        'grdApp.DataSource = dtApp
        Call FillApp()

    End Sub

    Private Sub frmInbox_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call RefreshInbox()
        bm = Me.BindingContext(dtInbox)
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
        Call UpdateReminder()
    End Sub

    Private Sub optInbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optInbox.CheckedChanged
        If optInbox.Checked Then
            chkComp.Checked = False 'KT20150611
            chkComp.Enabled = False
            chkPending.Checked = False 'KT20150611
            chkPending.Enabled = False
            chkHandoff.Checked = False 'KT20150611
            chkHandoff.Enabled = False
            txtFDate.Enabled = False
            txtTDate.Enabled = False
            cmdRefresh.Enabled = False
            cboHandOff.SelectedIndex = -1 'KT20150611
            cboHandOff.Enabled = False
            cboPending.SelectedIndex = -1 'KT20150611
            cboPending.Enabled = False 'KT20150611
            cmdExport.Enabled = False 'KT20150611

            dtInbox = SelectRecord()
            Call RefreshInbox()
            bm = Me.BindingContext(dtInbox)
            wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
            Call UpdateReminder()
        End If
    End Sub

    Private Sub optCust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCust.CheckedChanged
        If optCust.Checked Then
            chkComp.Enabled = True
            chkPending.Enabled = True
            chkHandoff.Enabled = True
            txtFDate.Enabled = True
            txtTDate.Enabled = True
            cmdRefresh.Enabled = True
            cboHandOff.SelectedIndex = -1 'KT20150611
            cboHandOff.Enabled = True
            cboPending.SelectedIndex = -1 'KT20150611
            'KT20150611
            If CheckUPSAccess("Pending Case Search") Then
                cboPending.Enabled = True
            End If
            'KT20150611

        End If
    End Sub

    Private Function SelectApp() As DataTable

        Dim sqldt As New DataTable("Appointment")
        Dim sqlda As SqlDataAdapter
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim aryDCPrimary(2) As DataColumn
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select crmctk_appointment_date as 'AppointmentDate', crmcsl_campaign_id as 'CampaignID'," &
            " crmcmp_campaign_name as 'CampaignName',  crmctk_channel_id as 'ChannelID', crmcpt_channel_desc as 'ChannelDesc'," &
            " crmctk_create_user as 'HandoffID', IsNull(Name,crmctk_create_user) as 'HandoffBy', crmcsl_last_call as 'HandoffDate'," &
            " crmcsl_customer_id as 'CustomerID',  left(crmctk_comment,50) as Comment " &
            " From " & serverPrefix & "crm_campaign_sales_leads Inner Join " & serverPrefix & "crm_campaign" &
            " ON crmcsl_campaign_id = crmcmp_campaign_id Inner Join " & serverPrefix & "crm_campaign_tracking" &
            " ON crmcsl_campaign_id = crmctk_campaign_id And crmcsl_channel_id = crmctk_channel_id" &
            " And crmcsl_customer_id = crmctk_customer_id And crmcsl_last_call = crmctk_call_datetime" &
            " Inner Join " & serverPrefix & "crm_campaign_channel_type ON crmctk_channel_id = crmcpt_channel_id" &
            " Inner Join Customer ON crmcsl_customer_id = CustomerID" &
            " Left Outer Join " & serverPrefix & "CSR ON crmctk_create_user = CSRID" &
            " Where crmcsl_status = '02' " &
            " And crmctk_handoff = '" & gsUser.Trim.Replace("'", "''") & "'"

        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)

            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(sqldt)

            'Set Primary Key
            aryDCPrimary(0) = sqldt.Columns("CampaignID")
            aryDCPrimary(1) = sqldt.Columns("ChannelID")
            aryDCPrimary(2) = sqldt.Columns("CustomerID")
            sqldt.PrimaryKey = aryDCPrimary

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Return sqldt

    End Function

    Private Sub UpdateControls()
        Dim drI As DataRow

        Dim strCustomerID As String
        Dim strCampaignID As String
        Dim strChannelID As String

        If bmApp.Count > 0 Then
            drI = CType(bmApp.Current, DataRowView).Row()
        End If

        If Not drI Is Nothing Then
            strCustomerID = IIf(drI.IsNull("CustomerID"), "", drI.Item("CustomerID"))
            strCampaignID = IIf(drI.IsNull("CampaignID"), "", drI.Item("CampaignID"))
            strChannelID = IIf(drI.IsNull("ChannelID"), "", drI.Item("ChannelID"))

            Me.uclCampaignTracking.CustID(strCampaignID, strChannelID) = strCustomerID
            Me.uclCampaignTracking.EnableButtons(True)
        Else
            Me.uclCampaignTracking.CustID("") = Nothing
        End If

    End Sub

    'Public Sub RefreshExecuteGrid(ByVal strCampaignID As String, ByVal strChannelID As String, ByVal strCustomerID As String, ByVal blnRefreshControls As Boolean)
    '    Dim strKeys(2) As String
    '    Dim strErrMsg As String
    '    Try
    '        Call RefreshInbox()
    '        'Locate the Customer
    '        strKeys(0) = "CampaignID = '" & strCampaignID.Replace("'", "''") & "'"
    '        strKeys(1) = "ChannelID = '" & strChannelID.Replace("'", "''") & "'"
    '        strKeys(2) = "CustomerID = '" & strCustomerID.Replace("'", "''") & "'"
    '        dtApp.Rows.Find(strKeys)

    '    Catch ex As Exception
    '        Throw ex
    '    Finally

    '    End Try
    'End Sub

    Private Sub frmInbox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.grdApp.Height = Math.Max(100, Me.TabPage2.Height - Me.uclCampaignTracking.Height)
    End Sub

    Private Sub frmInbox_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.grdApp.Height = Math.Max(100, Me.TabPage2.Height - Me.uclCampaignTracking.Height)
    End Sub

    Private Sub grdApp_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdApp.MouseUp
        Try
            Dim pt = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = Me.grdApp.HitTest(pt)

            If hti.Type = DataGrid.HitTestType.Cell Then
                Me.grdApp.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                Me.grdApp.Select(hti.Row)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub bmApp_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bmApp.CurrentChanged
        Try
            wndMain.Cursor = Cursors.WaitCursor
            Call UpdateControls()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            wndMain.Cursor = Cursors.Default
        End Try
    End Sub

    'KT20150611
    Private Sub chkPending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPending.Click
        If chkPending.Checked Then
            cboPending.SelectedValue = Trim(gsUser) ' default value to self
            If CheckUPSAccess("Pending Case Search") Then
                cmdExport.Enabled = True
            End If
        Else
            cboPending.SelectedIndex = -1
            cmdExport.Enabled = False
        End If
    End Sub

    Private Sub cboPending_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPending.SelectedIndexChanged
        If optCust.Checked Then
            If cboPending.SelectedIndex <> -1 Then
                chkPending.Checked = True
                If CheckUPSAccess("Pending Case Search") Then
                    cmdExport.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub chkHandoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHandoff.Click
        cboHandOff.SelectedIndex = -1
    End Sub

    Private Sub cboHandOff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboHandOff.SelectedIndexChanged
        If optCust.Checked Then
            If cboHandOff.SelectedIndex <> -1 Then
                chkHandoff.Checked = True
            End If
        End If
    End Sub

    'Private Function GetUPSGroup(ByVal strSystem As String, ByVal strUser As String, ByRef lngErrNo As Long, _
    'ByRef strErrMsg As String) As Object

    '    Dim strSQL As String
    '    Dim lngGrpNo As Object
    '    Dim sqlconnect As New SqlConnection

    '    Try

    '        strSQL = "Select upsugt_usr_grp_name " & _
    '            " From " & UPS_USER_LIST_TAB & ", " & UPS_USER_GROUP_TAB & _
    '            " Where upsult_sys_abv = '" & strSystem & "'" & _
    '            " And upsult_usr_id = '" & strUser & "'" & _
    '            " And upsult_usr_grp = upsugt_usr_grp_no"

    '        sqlconnect.ConnectionString = strUPSConn
    '        Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
    '        sqlconnect.Open()
    '        lngGrpNo = sqlcmd.ExecuteScalar

    '    Catch err As SqlClient.SqlException
    '        lngErrNo = err.Number
    '        strErrMsg = err.ToString()
    '    Catch ex As Exception
    '        lngErrNo = -1
    '        strErrMsg = "GetUPSGroup - " & ex.ToString
    '    Finally
    '        sqlconnect.Close()
    '    End Try

    '    Return lngGrpNo

    'End Function

    Private Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Call ExportPending()
    End Sub

    Private Sub ExportPending()

        Dim strSQL, strWhere As String
        Dim sqldt As New DataTable
        Dim sqlda As SqlDataAdapter
        Dim sqlconnect As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "SELECT CustomerID, case when s.PolicyAccountID is null then '' else s.PolicyAccountID end as 'Policy ID', " &
                 "s.EventInitialDateTime as 'Initial Date', cs.Name as 'Sender' " &
                 "FROM ServiceEventDetail s with (nolock) inner join " & gcPOS & "vw_csw_event_category_code  c " &
                 "on s.EventCategoryCode = c.cswecc_code " &
                 "inner join ServiceEventTypeCodes t " &
                 "on s.EventCategoryCode = t.EventCategoryCode and s.EventTypeCode = t.EventTypeCode " &
                 "inner join " & gcPOS & "vw_csw_event_typedtl_code d  " &
                 "on s.EventCategoryCode = d.csw_event_category_code and " &
                 "s.EventTypeCode = d.csw_event_type_code and " &
                 "s.EventTypeDetailCode = d.csw_event_typedtl_code " &
                 "inner join " & serverPrefix & "EventStatusCodes sc " &
                 "on s.EventStatusCode = sc.EventStatusCode " &
                 "inner join " & serverPrefix & "CSR cs on s.MasterCSRID = cs.CSRID "


        If optCust.Checked Then
            strSQL &= "WHERE convert(varchar,s.EventInitialDateTime,112) between '" & Format(txtFDate.Value, "yyyyMMdd") & "' AND '" & Format(txtTDate.Value, "yyyyMMdd") & "'"

            If chkPending.Checked Then

                If cboPending.SelectedValue <> Trim(gsUser) Then
                    strWhere = " (cs.CSRID = '" & Trim(cboPending.SelectedValue) & "' and s.EventStatusCode = 'P') "
                Else
                    strWhere = " (s.MasterCSRID = '" & Trim(gsUser) & "' and s.EventStatusCode = 'P') "
                End If

            End If

            strSQL &= " AND (" & strWhere & ")"
            strSQL &= " order by s.EventStatusCode asc, s.EventInitialDateTime desc "

        End If

        sqlconnect.ConnectionString = strCIWConn
        sqlda = New SqlDataAdapter

        Dim sqlcmd As New SqlCommand
        sqlcmd.Connection = sqlconnect
        sqlcmd.CommandTimeout = gQryTimeOut
        sqlcmd.CommandText = strSQL

        sqlda.SelectCommand = sqlcmd

        Try
            sqlda.Fill(sqldt)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Dim str As New System.Text.StringBuilder

        Dim header As String = Chr(34) & "Customer ID" & Chr(34) & "," & Chr(34) & "Policy No" & Chr(34) & "," & Chr(34) & "Initial Date" & Chr(34) & "," & Chr(34) & "Initiator" & Chr(34) & vbNewLine

        str.Append(header)

        For Each row As DataRow In sqldt.Rows

            For Each field As Object In row.ItemArray
                str.Append(Chr(34) & field.ToString() & Chr(34) & ",") ' double quote content
            Next

            str.Replace(",", vbNewLine, str.Length - 1, 1)

        Next


        Try
            Dim loc As String = "C:\temp\"
            Dim filename As String = Trim(cboPending.SelectedValue) & "_pending_case.csv"
            Using sfd As New SaveFileDialog
                sfd.InitialDirectory = loc
                sfd.Title = "Save As"
                sfd.OverwritePrompt = True
                sfd.FileName = filename
                sfd.DefaultExt = ".csv"
                sfd.Filter = "CSV (*.csv)|"
                sfd.AddExtension = True
                If sfd.ShowDialog() = DialogResult.OK Then
                    My.Computer.FileSystem.WriteAllText(sfd.FileName, str.ToString, False)
                    MessageBox.Show("Pending records are exported to file " & sfd.FileName)
                End If
            End Using
            'My.Computer.FileSystem.WriteAllText(loc & filename, str.ToString, False)
            'MessageBox.Show("Pending records are exported to file " & loc & filename)
        Catch ex As Exception
            MessageBox.Show("Export error")
        End Try

    End Sub

    ''' <summary>
    ''' Phase 3 Point A-3(CRS Enhancement)
    ''' triggering event for the btnSave button 
    ''' </summary>
    ''' <remarks>
    ''' <br>Added at 2023-8-29 by oliver which is Allow user to update service log and save in front page</br>
    ''' </remarks>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If bm.Count > 0 Then
            Dim dr As DataRow = CType(bm.Current, DataRowView).Row()
            If Not dr Is Nothing Then
                Dim strCompanyID As String = "HK"
                If dtInbox.Columns.Contains("CompanyID") AndAlso Not String.IsNullOrEmpty(dr.Item("CompanyID").ToString()) Then
                    If dr.Item("CompanyID").ToString() = "Assurance" Then
                        strCompanyID = "LAC"
                    End If
                End If
                Call SaveServiceLog(dr.Item("ServiceEventNumber"), txtRemark.Text, strCompanyID)
            End If
        Else
            MsgBox("Failed to Save EventNotes,The service log has no data")
        End If
    End Sub

    ''' <summary>
    ''' Point A-3 (CRS Enhancement)
    ''' Update service log by using ExcecuteBusiAPI in CRS_API
    ''' According to serviceEventNumber Filter out a records then modify the specified eventNote field in the database
    ''' </summary>
    ''' <remarks>
    ''' <br>Added at 2023-8-29 by oliver which is  Allow user to update service log and save in front page</br>
    ''' </remarks>
    ''' <param name="serviceEventNumber">Represents ServiceEventNumber parameter that makes up a SQL statement</param>
    ''' <param name="eventNote">Represents EventNote parameter that makes up a SQL statement</param>
    Private Sub SaveServiceLog(ByVal serviceEventNumber As String, ByVal eventNote As String, ByVal strCompanyID As String)

        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(strCompanyID),
                                            "UPDATE_SERVICE_LOG_EVENTNOTE",
                                            New Dictionary(Of String, String) From {
                                            {"eventNote", IIf(eventNote <> "", eventNote, " ")},
                                            {"serviceEventNumber", serviceEventNumber}
                                            })
            MsgBox("Save Success !", , "Save Success")
        Catch ex As Exception
            MsgBox("Error occurs when updating the comments. Error: " & ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        wndMain.StatusBarPanel1.Text = "Save Complete"
        Call RefreshInbox()
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
    End Sub
    'KT20150611

    Private Function ShowGIDetailPage(ByVal sPolicyNo As String) As Boolean
        Dim aCursor = Me.Cursor
        Try
            Me.Cursor = Cursors.WaitCursor
            Return GIDetailPage(sPolicyNo)
        Finally
            Me.Cursor = aCursor
        End Try
    End Function

    Private Function ShowEBDetailPage(ByVal sPolicyNo As String) As Boolean
        Dim aCursor = Me.Cursor
        Try
            Me.Cursor = Cursors.WaitCursor
            Return EBDetailPage(sPolicyNo)
        Finally
            Me.Cursor = aCursor
        End Try
    End Function

    Private Sub GIServiceLog(ByVal sPolicyNo As String)
        If Not wGIServiceLog Is Nothing Then
            wGIServiceLog.Dispose()
        End If
        wGIServiceLog = New frmGIServiceLog

        If Microsoft.VisualBasic.Left(sPolicyNo, 2) = "12" Or Microsoft.VisualBasic.Left(sPolicyNo, 2) = "22" _
                    Or Microsoft.VisualBasic.Left(sPolicyNo, 2) = "13" Or Microsoft.VisualBasic.Left(sPolicyNo, 2) = "23" Then
            wGIServiceLog.CustomerID = getCustomerID("GL" & sPolicyNo)
        Else
            wGIServiceLog.CustomerID = getCustomerID(sPolicyNo)
        End If

        wGIServiceLog.PolicyAccountID = sPolicyNo
        wGIServiceLog.PolicyType = "GL"
        If Not OpenWindow(wGIServiceLog, wndMain) Then
            wGIServiceLog.Dispose()
        End If

    End Sub

End Class
