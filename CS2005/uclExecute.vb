Imports System.Data.SqlClient
Imports System.IO


Public Class uclExecute
    Inherits System.Windows.Forms.UserControl

    Private Const pc_LockMinutes As Integer = 10

    Private dsExcution As DataSet = New DataSet("Excution")
    Private blnAdmin As Boolean
    Private blnFillCampaign, blnFillChannel As Boolean
    Private blnRefreshCtls As Boolean
    Private WithEvents bm As BindingManagerBase
    Private aryLockRecord(2) As String
    Private dtCSRF As DataTable
    Private gLock As Integer = 0

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.cmdAssign.Visible = CheckUPSAccess("Auto-Assign CSR")

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents grdResult As System.Windows.Forms.DataGrid
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCampaign As System.Windows.Forms.ComboBox
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents uclCustomer As CS2005.Personal
    Friend WithEvents uclCampaignTracking As CS2005.uclCampaignTracking
    Friend WithEvents uclAddress As CS2005.AddressSelect
    Friend WithEvents cmdLock As System.Windows.Forms.Button
    Friend WithEvents cmdUnLock As System.Windows.Forms.Button
    Friend WithEvents cmdAssign As System.Windows.Forms.Button
    Friend WithEvents cmdShow As System.Windows.Forms.Button
    Friend WithEvents cboCSRFtr As System.Windows.Forms.ComboBox
    Friend WithEvents radCSR As System.Windows.Forms.RadioButton
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblSLCnt As System.Windows.Forms.Label
    Friend WithEvents UclWebBrowser1 As CS2005.uclWebBrowser
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.uclCustomer = New CS2005.Personal
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.uclCampaignTracking = New CS2005.uclCampaignTracking
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.uclAddress = New CS2005.AddressSelect
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.UclWebBrowser1 = New CS2005.uclWebBrowser
        Me.grdResult = New System.Windows.Forms.DataGrid
        Me.cboCampaign = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.cmdLock = New System.Windows.Forms.Button
        Me.cmdUnLock = New System.Windows.Forms.Button
        Me.cmdAssign = New System.Windows.Forms.Button
        Me.cmdShow = New System.Windows.Forms.Button
        Me.cboCSRFtr = New System.Windows.Forms.ComboBox
        Me.radCSR = New System.Windows.Forms.RadioButton
        Me.radAll = New System.Windows.Forms.RadioButton
        Me.lblSLCnt = New System.Windows.Forms.Label
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(4, 184)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(700, 264)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.uclCustomer)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(692, 400)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Personal"
        '
        'uclCustomer
        '
        Me.uclCustomer.Location = New System.Drawing.Point(-4, 0)
        Me.uclCustomer.Name = "uclCustomer"
        Me.uclCustomer.Size = New System.Drawing.Size(692, 400)
        Me.uclCustomer.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.uclCampaignTracking)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(692, 238)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Campaign Tracking"
        '
        'uclCampaignTracking
        '
        Me.uclCampaignTracking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uclCampaignTracking.Location = New System.Drawing.Point(0, 0)
        Me.uclCampaignTracking.Name = "uclCampaignTracking"
        Me.uclCampaignTracking.ReadOnlyMode = True
        Me.uclCampaignTracking.Size = New System.Drawing.Size(692, 238)
        Me.uclCampaignTracking.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.uclAddress)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(692, 238)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Address"
        '
        'uclAddress
        '
        Me.uclAddress.Location = New System.Drawing.Point(0, 0)
        Me.uclAddress.Name = "uclAddress"
        Me.uclAddress.Size = New System.Drawing.Size(692, 264)
        Me.uclAddress.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.UclWebBrowser1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(692, 238)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Call Script"
        '
        'UclWebBrowser1
        '
        Me.UclWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UclWebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.UclWebBrowser1.Name = "UclWebBrowser1"
        Me.UclWebBrowser1.Size = New System.Drawing.Size(692, 238)
        Me.UclWebBrowser1.TabIndex = 0
        '
        'grdResult
        '
        Me.grdResult.AlternatingBackColor = System.Drawing.Color.White
        Me.grdResult.BackColor = System.Drawing.Color.White
        Me.grdResult.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdResult.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdResult.CaptionVisible = False
        Me.grdResult.DataMember = ""
        Me.grdResult.FlatMode = True
        Me.grdResult.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdResult.ForeColor = System.Drawing.Color.Black
        Me.grdResult.GridLineColor = System.Drawing.Color.Wheat
        Me.grdResult.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdResult.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdResult.HeaderForeColor = System.Drawing.Color.Black
        Me.grdResult.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Location = New System.Drawing.Point(4, 52)
        Me.grdResult.Name = "grdResult"
        Me.grdResult.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdResult.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdResult.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdResult.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Size = New System.Drawing.Size(696, 128)
        Me.grdResult.TabIndex = 22
        '
        'cboCampaign
        '
        Me.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCampaign.Items.AddRange(New Object() {"我愛ING安泰曼聯盃", "Refundable Hospital Income"})
        Me.cboCampaign.Location = New System.Drawing.Point(72, 4)
        Me.cboCampaign.Name = "cboCampaign"
        Me.cboCampaign.Size = New System.Drawing.Size(416, 21)
        Me.cboCampaign.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Campaign:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(500, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 16)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "Channel:"
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Location = New System.Drawing.Point(556, 4)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(96, 21)
        Me.cboChannel.TabIndex = 97
        '
        'cmdLock
        '
        Me.cmdLock.Enabled = False
        Me.cmdLock.Location = New System.Drawing.Point(548, 180)
        Me.cmdLock.Name = "cmdLock"
        Me.cmdLock.Size = New System.Drawing.Size(76, 22)
        Me.cmdLock.TabIndex = 99
        Me.cmdLock.Text = "Lock"
        '
        'cmdUnLock
        '
        Me.cmdUnLock.Enabled = False
        Me.cmdUnLock.Location = New System.Drawing.Point(624, 180)
        Me.cmdUnLock.Name = "cmdUnLock"
        Me.cmdUnLock.Size = New System.Drawing.Size(76, 22)
        Me.cmdUnLock.TabIndex = 100
        Me.cmdUnLock.Text = "Release"
        '
        'cmdAssign
        '
        Me.cmdAssign.Enabled = False
        Me.cmdAssign.Location = New System.Drawing.Point(444, 180)
        Me.cmdAssign.Name = "cmdAssign"
        Me.cmdAssign.Size = New System.Drawing.Size(104, 22)
        Me.cmdAssign.TabIndex = 101
        Me.cmdAssign.Text = "Auto-Assign CSR"
        '
        'cmdShow
        '
        Me.cmdShow.Enabled = False
        Me.cmdShow.Location = New System.Drawing.Point(328, 28)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.Size = New System.Drawing.Size(40, 20)
        Me.cmdShow.TabIndex = 121
        Me.cmdShow.Text = "Show"
        '
        'cboCSRFtr
        '
        Me.cboCSRFtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCSRFtr.Enabled = False
        Me.cboCSRFtr.Location = New System.Drawing.Point(220, 28)
        Me.cboCSRFtr.Name = "cboCSRFtr"
        Me.cboCSRFtr.Size = New System.Drawing.Size(104, 21)
        Me.cboCSRFtr.TabIndex = 120
        '
        'radCSR
        '
        Me.radCSR.Location = New System.Drawing.Point(116, 28)
        Me.radCSR.Name = "radCSR"
        Me.radCSR.Size = New System.Drawing.Size(100, 20)
        Me.radCSR.TabIndex = 119
        Me.radCSR.Text = "Specific CSRID"
        '
        'radAll
        '
        Me.radAll.Checked = True
        Me.radAll.Location = New System.Drawing.Point(8, 28)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(104, 20)
        Me.radAll.TabIndex = 118
        Me.radAll.TabStop = True
        Me.radAll.Text = "All Records"
        '
        'lblSLCnt
        '
        Me.lblSLCnt.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(192, Byte))
        Me.lblSLCnt.Location = New System.Drawing.Point(536, 32)
        Me.lblSLCnt.Name = "lblSLCnt"
        Me.lblSLCnt.Size = New System.Drawing.Size(160, 16)
        Me.lblSLCnt.TabIndex = 122
        Me.lblSLCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(656, 4)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(40, 20)
        Me.cmdLoad.TabIndex = 123
        Me.cmdLoad.Text = "Load"
        '
        'uclExecute
        '
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.lblSLCnt)
        Me.Controls.Add(Me.grdResult)
        Me.Controls.Add(Me.cmdShow)
        Me.Controls.Add(Me.cboCSRFtr)
        Me.Controls.Add(Me.radCSR)
        Me.Controls.Add(Me.radAll)
        Me.Controls.Add(Me.cmdAssign)
        Me.Controls.Add(Me.cmdUnLock)
        Me.Controls.Add(Me.cmdLock)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboChannel)
        Me.Controls.Add(Me.cboCampaign)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "uclExecute"
        Me.Size = New System.Drawing.Size(708, 452)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub RefreshExecuteGrid(ByVal strCampaignID As String, ByVal strChannelID As String, ByVal strCustomerID As String, ByVal blnRefreshControls As Boolean)
        Dim strKeys(2) As String
        Dim strErrMsg As String
        Try
            blnRefreshCtls = blnRefreshControls
            Call BindExcutionGrid(strCampaignID, strChannelID)
            'Locate the Customer
            strKeys(0) = "CampaignID = '" & strCampaignID.Replace("'", "''") & "'"
            strKeys(1) = "ChannelID = '" & strChannelID.Replace("'", "''") & "'"
            strKeys(2) = "CustomerID = '" & strCustomerID.Replace("'", "''") & "'"
            dsExcution.Tables("CampaignExcution").Rows.Find(strKeys)

            'UnLock this record
            If UnLockRecord(strCampaignID, strChannelID, strCustomerID, strErrMsg) Then
                Me.cmdLock.Enabled = True
                Me.cmdUnLock.Enabled = False
                'Me.grdResult.Enabled = True
                Me.uclCampaignTracking.EnableButtons(False)

                radAll.Enabled = True
                radCSR.Enabled = True
                cboCSRFtr.Enabled = True
                cmdShow.Enabled = True
                ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                ''CRS 7x24 Changes - Start
                'If ExternalUser Then
                '    cmdLoad.Enabled = False
                'Else
                '    cmdLoad.Enabled = True
                'End If
                ''CRS 7x24 Changes - End
                cmdLoad.Enabled = True
                aryLockRecord(0) = ""
                aryLockRecord(1) = ""
                aryLockRecord(2) = ""
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            blnRefreshCtls = True
        End Try
    End Sub

#Region "Get Data"
    Private Function GetCampaignExcution(ByRef dtData As DataTable, ByRef strErrMsg As String,
                                Optional ByVal strCampaignID As String = "ALL", Optional ByVal strChannelID As String = "ALL") As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(2) As DataColumn
        Dim strSQL As String
        Dim strWhere As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        GetCampaignExcution = False

        Try
            sqlConn.ConnectionString = strCIWConn

            dtData = New DataTable("CampaignExcution")

            'Get Campaign Channels
            strSQL = "Select crmcsl_campaign_id as CampaignID, crmcsl_channel_id as ChannelID,"
            strSQL &= " crmcsl_customer_id as CustomerID, crmcsl_status as Status, crmcsl_last_call as LastCall,"
            strSQL &= " crmcmp_campaign_name as CampaignName, crmcpt_channel_desc as ChannelName,"
            strSQL &= " rtrim(NameSuffix) + ' ' + rtrim(FirstName) as CustomerName, DateDiff(yy,DateOfBirth,GetDate()) as Age,"
            strSQL &= " DateOfBirth as Birthday, GovernmentIDCard as ClientID,"
            strSQL &= " IsNull(crmcts_status_desc,'') as StatusDesc,"
            strSQL &= " NumberOfDependents as DependNo, EduLevel as EduLevel, AnnSalary as PersonalIncome, Remarks as Remarks,"
            strSQL &= " cswdgm_optout_email as OptEmail, cswdgm_optout_call as OptCall, cswdgm_rating as Rating, crmcsl_csrid, name "
            strSQL &= " From " & serverPrefix & "crm_campaign_sales_leads Inner Join " & serverPrefix & "crm_campaign"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_campaign_id = crm_campaign.crmcmp_campaign_id"
            strSQL &= " Inner Join " & serverPrefix & "crm_campaign_channel_type"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_channel_id = crm_campaign_channel_type.crmcpt_channel_id"
            strSQL &= " Inner Join Customer"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_customer_id = Customer.CustomerID"
            'oliver 2024-5-3 added for Table_Relocate_Sprint13
            strSQL &= " Left Outer Join " & serverPrefix & " csw_demographic"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_customer_id = csw_demographic.cswdgm_cust_id"
            strSQL &= " Left Outer Join " & serverPrefix & "crm_campaign_tracking_status"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_status = crm_campaign_tracking_status.crmcts_status_id"
            strSQL &= " Left Outer Join " & serverPrefix & "csr"
            strSQL &= " ON crmcsl_csrid = csrid "

            If strCampaignID.Trim.ToUpper <> "ALL" Then
                strWhere &= " Where crmcsl_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            End If
            If strChannelID.Trim.ToUpper <> "ALL" Then
                If strWhere = "" Then
                    strWhere &= " Where"
                Else
                    strWhere &= " And"
                End If
                strWhere &= " crmcsl_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            End If
            strSQL &= strWhere & " Order by crmcsl_campaign_id, crmcsl_channel_id"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("CampaignID")
            aryDCPrimary(1) = dtData.Columns("ChannelID")
            aryDCPrimary(2) = dtData.Columns("CustomerID")
            dtData.PrimaryKey = aryDCPrimary

            GetCampaignExcution = True
            'lblSLCnt.Text = "Records: " & dtData.Rows.Count

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlDA.Dispose()
            sqlConn.Close()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function GetSaleLeadsChannel(ByRef dtData As DataTable, ByRef strErrMsg As String,
                                Optional ByVal strCampaignID As String = "ALL") As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String
        Dim strWhere As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        GetSaleLeadsChannel = False

        Try
            sqlConn.ConnectionString = strCIWConn

            dtData = New DataTable("SaleLeadsChannel")

            'Get Campaign Channels
            strSQL = "Select crmcsl_campaign_id as CampaignID, crmcsl_channel_id as ChannelID,"
            strSQL &= " crmcmp_campaign_name as CampaignName, crmcpt_channel_desc as ChannelName,"
            strSQL &= " crmcpc_call_script as ChannelCallScriptFile"
            strSQL &= " From " & serverPrefix & "crm_campaign_sales_leads Inner Join " & serverPrefix & "crm_campaign"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_campaign_id = crm_campaign.crmcmp_campaign_id"
            strSQL &= " Inner Join " & serverPrefix & "crm_campaign_channel_type"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_channel_id = crm_campaign_channel_type.crmcpt_channel_id"
            strSQL &= " Inner Join " & serverPrefix & "crm_campaign_channel"
            strSQL &= " ON crm_campaign_sales_leads.crmcsl_campaign_id = crm_campaign_channel.crmcpc_campaign_id"
            strSQL &= " AND crm_campaign_sales_leads.crmcsl_channel_id = crm_campaign_channel.crmcpc_channel_id"
            If strCampaignID.Trim.ToUpper <> "ALL" Then
                strWhere &= " Where crmcsl_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            End If
            strSQL &= strWhere & " Group by crmcsl_campaign_id, crmcsl_channel_id, crmcmp_campaign_name, crmcpt_channel_desc, crmcpc_call_script"
            strSQL &= " Order by crmcsl_campaign_id, crmcsl_channel_id"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            GetSaleLeadsChannel = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlDA.Dispose()
            sqlConn.Close()
            sqlConn.Dispose()
        End Try

    End Function
#End Region

#Region "Fill Combox"
    Private Sub FillCboCampaign()
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDT As New DataTable("TableCampaign")
        Dim sqlDV As DataView
        Dim strSQL As String
        Dim I As Integer
        Dim strCampID As String
        Dim strCampName As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Try
            sqlConn.ConnectionString = strCIWConn

            strSQL = "Select distinct(crmcsl_campaign_id) as CampaignID, crmcmp_campaign_name as CampaignName"
            strSQL &= " from " & serverPrefix & "crm_campaign_sales_leads Inner Join " & serverPrefix & "crm_campaign"
            strSQL &= " ON crmcsl_campaign_id = crmcmp_campaign_id "
            strSQL &= " Where crmcmp_status_id <> '03'"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(sqlDT)

            sqlDV = sqlDT.DefaultView
            sqlDV.Sort = "CampaignName"

            Try
                blnFillCampaign = True
                Me.cboCampaign.DataSource = sqlDV
                Me.cboCampaign.DisplayMember = "CampaignName"
                Me.cboCampaign.ValueMember = "CampaignID"
                If Me.cboCampaign.Items.Count > 0 Then Me.cboCampaign.SelectedIndex = 0
            Catch ex As Exception
                Throw ex
            Finally
                blnFillCampaign = False
            End Try

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlDA.Dispose()
            sqlDT.Dispose()
            sqlConn.Dispose()
        End Try

    End Sub

    Private Sub FillCboChannel(ByVal strCampaignID As String)
        Dim dtData As DataTable
        Dim strErrMsg As String
        Dim strSQL As String
        Dim I As Integer
        Dim strChannelID As String
        Dim strChannelName As String

        If strCampaignID Is Nothing Then
            Exit Sub
        End If

        If GetSaleLeadsChannel(dtData, strErrMsg, strCampaignID) Then
            If dsExcution.Tables.Contains(dtData.TableName) Then
                dsExcution.Tables(dtData.TableName).Constraints.Clear()
                dsExcution.Relations.Clear()
                dsExcution.Tables.Remove(dtData.TableName)
            End If
            dsExcution.Tables.Add(dtData)
            Try
                blnFillChannel = True
                Me.cboChannel.DataSource = dtData.DefaultView
                Me.cboChannel.DisplayMember = "ChannelName"
                Me.cboChannel.ValueMember = "ChannelID"
                If Me.cboChannel.Items.Count > 0 Then Me.cboChannel.SelectedIndex = 0
            Catch ex As Exception
                Throw ex
            Finally
                blnFillChannel = False
            End Try
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub
#End Region

    Public Sub InitControl()
        Try
            blnAdmin = True
            blnRefreshCtls = True
            Call buildUI()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub buildUI()
        Dim strCampID As String
        Dim strChannelID As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Call EnableControls(False)

        Call FillCboCampaign()
        strCampID = IIf(Me.cboCampaign.SelectedValue Is Nothing, "", Me.cboCampaign.SelectedValue)

        Call FillCboChannel(strCampID)
        strChannelID = IIf(Me.cboChannel.SelectedValue Is Nothing, "", Me.cboChannel.SelectedValue)

        'Call BindExcutionGrid(strCampID, strChannelID)

        Dim strSQL As String

        strSQL = "Select distinct csrid, name From " & serverPrefix & "CSR where active = 'Y' order by name"
        LoadComboBox(dtCSRF, cboCSRFtr, "csrid", "name", strSQL)

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdLoad.Enabled = False
        '    cmdAssign.Enabled = False
        '    cmdLock.Enabled = False
        '    cmdUnLock.Enabled = False
        '    cmdShow.Enabled = False
        'End If
        ''CRS 7x24 Changes - End

    End Sub

    Private Sub BindExcutionGrid(ByVal strCampaignID As String, ByVal strChannelID As String, Optional ByVal blnLoadScript As Boolean = False)

        Dim dtData As DataTable
        Dim strErrMsg As String

        If GetCampaignExcution(dtData, strErrMsg, strCampaignID, strChannelID) Then
            If dsExcution.Tables.Contains(dtData.TableName) Then
                dsExcution.Tables(dtData.TableName).Constraints.Clear()
                dsExcution.Relations.Clear()
                dsExcution.Tables.Remove(dtData.TableName)
            End If
            dsExcution.Tables.Add(dtData)
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CustomerID"
        cs.HeaderText = "CustomerID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "CustomerName"
        cs.HeaderText = "Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Age"
        cs.HeaderText = "Age"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Birthday"
        cs.HeaderText = "Date Of Birth"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "StatusDesc"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "LastCall"
        cs.HeaderText = "Last Call"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Name"
        cs.HeaderText = "CSR Name"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CampaignExcution"
        Me.grdResult.TableStyles.Clear()
        Me.grdResult.TableStyles.Add(ts)

        grdResult.DataSource = dsExcution.Tables("CampaignExcution")
        dsExcution.Tables("CampaignExcution").DefaultView.Sort = "StatusDesc Desc, LastCall Desc"
        grdResult.AllowDrop = False
        grdResult.ReadOnly = True

        bm = Me.BindingContext(dsExcution.Tables("CampaignExcution"))

        If gLock < bm.Count AndAlso gLock >= 0 Then bm.Position = gLock

        If radAll.Checked Then
            dsExcution.Tables("CampaignExcution").DefaultView.RowFilter = ""
            lblSLCnt.Text = "Records: " & bm.Count
        Else
            dsExcution.Tables("CampaignExcution").DefaultView.RowFilter = "crmcsl_csrid = '" & cboCSRFtr.SelectedValue & "'"
            lblSLCnt.Text = "Records: " & dsExcution.Tables("CampaignExcution").DefaultView.Count
        End If


        If bm.Count = 0 Then
            EnableControls(False)
        End If

        Call UpdateControls(True)

    End Sub

    Private Sub UpdateControls(Optional ByVal blnLoadScript As Boolean = False)

        Dim dtPersonal, dtAddress, dtCIWPersonalInfo As DataTable
        Dim drI As DataRow
        Dim lngErrNo As Long
        Dim strErrMsg As String

        Dim strCustomerID As String
        Dim strClientID As String
        Dim strCampaignID As String
        Dim strChannelID As String
        Dim blnCovSmoker As Boolean

        If Not blnRefreshCtls Then
            Exit Sub
        End If

        If bm.Count > 0 Then
            drI = CType(bm.Current, DataRowView).Row()
        End If

        If Not drI Is Nothing Then
            'Enable Lock and Unlock button
            Me.cmdLock.Enabled = True
            Me.cmdUnLock.Enabled = False
            'Me.grdResult.Enabled = True

            strCustomerID = IIf(drI.IsNull("CustomerID"), "", drI.Item("CustomerID"))
            strClientID = IIf(drI.IsNull("ClientID"), "", drI.Item("ClientID"))
            strCampaignID = Me.cboCampaign.SelectedValue
            strChannelID = Me.cboChannel.SelectedValue

            If Not GetPersonalInfoAddress(strCustomerID, strClientID, strErrMsg, blnCovSmoker, dtPersonal, dtAddress, dtCIWPersonalInfo) Then
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            'Fill Personal Control
            'oliver 2024-6-6 added for Table_Relocate_Sprint13
            If dtPersonal IsNot Nothing And dtCIWPersonalInfo IsNot Nothing Then
                Me.uclCustomer.CustID(strClientID, dtPersonal, dtCIWPersonalInfo, blnCovSmoker) = strCustomerID
            End If

            'Fill Address Control
            Call Me.uclAddress.resetDS()
            Me.uclAddress.srcDTAddr(False) = dtAddress

            'Fill Campaign Tracking
            Me.uclCampaignTracking.CustID(strCampaignID, strChannelID) = strCustomerID
            Me.uclCampaignTracking.EnableButtons(False)

            'Fill Call Script
            If blnLoadScript Then Call FillCallScript(strCampaignID, strChannelID)
        Else
            'Enable Lock and Unlock button
            Me.cmdLock.Enabled = False
            Me.cmdUnLock.Enabled = False

            'Fill Personal Control
            Call Me.uclCustomer.ClearTextBox()

            'Fill Address Control
            Call Me.uclAddress.resetDS()
            Me.uclAddress.srcDTAddr(False) = dtAddress

            'Fill Campaign Tracking
            Me.uclCampaignTracking.CustID("") = Nothing

            'Fill Call Script
            Call FillCallScript("", "")
        End If

    End Sub

    Private Sub FillCallScript(ByVal strCampaignID As String, ByVal strChannelID As String)
        Dim dtData As DataTable
        Dim drData() As DataRow
        'Dim objFS As FileStream
        Dim strCallScriptFile As String
        'Dim b(1024) As Byte
        'Dim temp As System.Text.UTF8Encoding = New System.Text.UTF8Encoding(True)

        Try
            strCallScriptFile = ""
            'Me.rtbCallScript.Clear()
            dtData = dsExcution.Tables("SaleLeadsChannel").Copy

            drData = dtData.Select("CampaignID = '" & strCampaignID.Replace("'", "''") & "' And ChannelID = '" & strChannelID.Replace("'", "''") & "'")
            If drData.GetUpperBound(0) >= 0 Then
                strCallScriptFile = UCase(drData(0).Item("ChannelCallScriptFile"))
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            dtData.Dispose()
        End Try

        If strCallScriptFile.Trim.Length > 0 Then
            If Dir(strCallScriptFile.Trim).Length > 0 Then
                'objFS = File.OpenRead(strCallScriptFile)

                'Do While objFS.Read(b, 0, b.Length) > 0
                '    Me.rtbCallScript.Text &= temp.GetString(b)
                'Loop

                'objFS.Close()
                'UclWebBrowser1.webBrowser.Navigate(strCallScriptFile)
                If InStr(strCallScriptFile, ".DOC") Then
                    ViewDocInWebbrowser(strCallScriptFile)
                Else
                    UclWebBrowser1.webBrowser.Navigate(strCallScriptFile)
                End If
                Exit Sub
            End If
        End If

        UclWebBrowser1.webBrowser.Navigate("")

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim DocViewer As New Threading.Thread(AddressOf ViewDocInWebbrowser)
    '    DocViewer.Start()
    'End Sub

    Private Sub ViewDocInWebbrowser(ByVal strDocLoc As String)
        'Dim od As New OpenFileDialog
        'With od
        '.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        '.Filter = "Word Documents|*.DOC"
        'If .ShowDialog = DialogResult.OK Then
        Dim typeWord As Type = Type.GetTypeFromProgID("Word.Application")
        Dim WordApp As Object = Activator.CreateInstance(typeWord)

        Dim htmlFormat As Integer = 8
        Dim Docpath As Object() = {strDocLoc} '/// path to a valid .Doc file.
        Dim HtmPath As Object() = {Application.StartupPath & "WordDoc.HTML", htmlFormat} '/// temp path to hold the html version of the .Doc file.

        Dim WordDocs As Object = typeWord.InvokeMember("Documents", Reflection.BindingFlags.GetProperty, Nothing, WordApp, Nothing)
        Dim doc As Object = WordDocs.GetType.InvokeMember("Open", Reflection.BindingFlags.InvokeMethod, Nothing, WordDocs, Docpath)
        doc.GetType.InvokeMember("SaveAs", Reflection.BindingFlags.InvokeMethod, Nothing, doc, HtmPath)

        WordApp.quit() '/// close the instance of Word down.

        UclWebBrowser1.webBrowser.Navigate(HtmPath(0)) '/// load the Word Document in to the webbrowser.
        'End If
        'End With
    End Sub

    Private Function LockRecord(ByVal strCampaignID As String, ByVal strChannelID As String, ByVal strCustomerID As String, _
                                                ByRef strErrMsg As String) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlCmd As SqlCommand
        Dim strSQL As String
        Dim strLockUser As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        LockRecord = False

        Try
            'Get Campaign Channels
            strSQL = "Update " & serverPrefix & "crm_campaign_sales_leads"
            strSQL &= " Set crmcsl_lock_user = '" & gsUser.Replace("'", "''") & "',"
            strSQL &= " crmcsl_lock_date = GetDate()"
            strSQL &= " Where crmcsl_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_customer_id = '" & strCustomerID.Trim.Replace("'", "''") & "'"
            strSQL &= " And (crmcsl_lock_user is Null Or crmcsl_lock_user = ''"
            strSQL &= " Or crmcsl_lock_date is Null Or DateDiff(n,crmcsl_lock_date,GetDate()) > " & pc_LockMinutes.ToString & ")"

            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand()
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

            'Get Lock User to check
            strSQL = "Select IsNull(crmcsl_lock_user,'') From " & serverPrefix & "crm_campaign_sales_leads"
            strSQL &= " Where crmcsl_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_customer_id = '" & strCustomerID.Trim.Replace("'", "''") & "'"

            sqlCmd.CommandText = strSQL
            strLockUser = sqlCmd.ExecuteScalar()

            If strLockUser.Trim.ToUpper <> gsUser.Trim.ToUpper Then
                strErrMsg = "This record is locked by " & strLockUser
                Exit Try
            End If

            LockRecord = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlCmd.Dispose()
            sqlConn.Close()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function UnLockRecord(ByVal strCampaignID As String, ByVal strChannelID As String, ByVal strCustomerID As String, _
                                                ByRef strErrMsg As String) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlCmd As SqlCommand
        Dim strSQL As String
        Dim intUnLock As Integer
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        UnLockRecord = False

        Try
            If strCampaignID.Length = 0 Or strChannelID.Length = 0 Or strCustomerID.Length = 0 Then
                UnLockRecord = True
                Exit Try
            End If

            'Get Campaign Channels
            strSQL = "Update " & serverPrefix & "crm_campaign_sales_leads"
            strSQL &= " Set crmcsl_lock_user = ''"
            strSQL &= " Where crmcsl_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_customer_id = '" & strCustomerID.Trim.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_lock_user = '" & gsUser.Replace("'", "''") & "'"

            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand()
            sqlCmd.CommandText = strSQL
            intUnLock = sqlCmd.ExecuteNonQuery()


            If intUnLock = 0 Then
                strErrMsg = "This record cannot be unlocked."
                Exit Try
            End If

            UnLockRecord = True            

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            If Not sqlCmd Is Nothing Then sqlCmd.Dispose()
            sqlConn.Close()
            sqlConn.Dispose()
        End Try

    End Function

    Private Sub EnableControls(ByVal blnEnabled As Boolean)
        'Me.uclCustomer.Enabled = blnEnabled
        'Me.uclAddress.Enabled = blnEnabled
        'Me.uclCampaignTracking.Enabled = blnEnabled
    End Sub

    Private Sub cboChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChannel.SelectedIndexChanged
        Dim strCampID As String
        Dim strChannelID As String

        Try
            If Not blnFillChannel Then
                wndMain.Cursor = Cursors.WaitCursor
                strCampID = Me.cboCampaign.SelectedValue
                strChannelID = Me.cboChannel.SelectedValue
                'Call BindExcutionGrid(strCampID, strChannelID)
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            wndMain.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cboCampaign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCampaign.SelectedIndexChanged
        Dim strCampID As String
        Dim strChannelID As String

        Try
            If Not blnFillCampaign Then
                wndMain.Cursor = Cursors.WaitCursor
                strCampID = Me.cboCampaign.SelectedValue
                Call FillCboChannel(strCampID)
                strChannelID = Me.cboChannel.SelectedValue
                'Call BindExcutionGrid(strCampID, strChannelID)
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            wndMain.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdResult_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdResult.MouseUp
        Try
            Dim pt = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = grdResult.HitTest(pt)

            If hti.Type = DataGrid.HitTestType.Cell Then
                grdResult.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                grdResult.Select(hti.Row)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub bm_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bm.CurrentChanged

        Dim strErrMsg As String

        If cmdLock.Enabled = False Then
            Exit Sub
        End If

        Try
            wndMain.Cursor = Cursors.WaitCursor
            'Unlock Previous locked Record first
            If Not aryLockRecord(0) Is Nothing Then
                If UnLockRecord(aryLockRecord(0), aryLockRecord(1), aryLockRecord(2), strErrMsg) Then
                    Me.cmdLock.Enabled = True
                    Me.cmdUnLock.Enabled = False
                    aryLockRecord(0) = ""
                    aryLockRecord(1) = ""
                    aryLockRecord(2) = ""
                Else
                    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                End If
            End If
            Call UpdateControls()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            wndMain.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
        Dim drI As DataRow
        Dim strCampaignID As String
        Dim strChannelID As String
        Dim strCustomerID As String
        Dim strErrMsg As String
        Try
            If bm.Count <= 0 Then
                Exit Try
            End If

            drI = CType(bm.Current, DataRowView).Row()
            strCustomerID = IIf(drI.IsNull("CustomerID"), "", drI.Item("CustomerID"))
            strCampaignID = IIf(drI.IsNull("CampaignID"), "", drI.Item("CampaignID"))
            strChannelID = IIf(drI.IsNull("ChannelID"), "", drI.Item("ChannelID"))

            If LockRecord(strCampaignID, strChannelID, strCustomerID, strErrMsg) Then
                Me.cmdLock.Enabled = False
                Me.cmdUnLock.Enabled = True
                'Me.grdResult.Enabled = False

                radAll.Enabled = False
                radCSR.Enabled = False
                cboCSRFtr.Enabled = False
                cmdShow.Enabled = False
                cmdLoad.Enabled = False

                gLock = bm.Position

                Me.uclCampaignTracking.EnableButtons(True)
                'Save Locked Record's Primary Key for unlock purpose
                aryLockRecord(0) = strCampaignID
                aryLockRecord(1) = strChannelID
                aryLockRecord(2) = strCustomerID                
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub cmdUnLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnLock.Click
        Dim drI As DataRow
        Dim strCampaignID As String
        Dim strChannelID As String
        Dim strCustomerID As String
        Dim strErrMsg As String

        Try
            If bm.Count <= 0 Then
                Exit Try
            End If

            drI = CType(bm.Current, DataRowView).Row()
            strCustomerID = IIf(drI.IsNull("CustomerID"), "", drI.Item("CustomerID"))
            strCampaignID = IIf(drI.IsNull("CampaignID"), "", drI.Item("CampaignID"))
            strChannelID = IIf(drI.IsNull("ChannelID"), "", drI.Item("ChannelID"))

            If UnLockRecord(strCampaignID, strChannelID, strCustomerID, strErrMsg) Then
                Me.cmdLock.Enabled = True
                Me.cmdUnLock.Enabled = False
                'Me.grdResult.Enabled = True

                radAll.Enabled = True
                radCSR.Enabled = True
                cboCSRFtr.Enabled = True
                cmdShow.Enabled = True

                ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                ''CRS 7x24 Changes - Start
                'If ExternalUser Then
                '    cmdLoad.Enabled = False
                'Else
                '    cmdLoad.Enabled = True
                'End If
                ''CRS 7x24 Changes - End
                cmdLoad.Enabled = True

                Me.uclCampaignTracking.EnableButtons(False)
                aryLockRecord(0) = ""
                aryLockRecord(1) = ""
                aryLockRecord(2) = ""
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub cmdAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAssign.Click
        Dim f As New frmAssignCase
        f.Campaign(Me.cboChannel.SelectedValue) = Me.cboCampaign.SelectedValue
        f.ShowDialog()

        wndMain.Cursor = Cursors.WaitCursor
        Call BindExcutionGrid(Me.cboCampaign.SelectedValue, Me.cboChannel.SelectedValue)
        wndMain.Cursor = Cursors.Default

        f.Dispose()

    End Sub

    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As Object, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dt)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
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

    Private Sub radAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radAll.Click
        cboCSRFtr.Enabled = False
        cmdShow.Enabled = False
        dsExcution.Tables("CampaignExcution").DefaultView.RowFilter = ""
        lblSLCnt.Text = "Records: " & dsExcution.Tables("CampaignExcution").DefaultView.Count
    End Sub

    Private Sub radCSR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radCSR.Click
        cboCSRFtr.Enabled = True
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        dsExcution.Tables("CampaignExcution").DefaultView.RowFilter = "crmcsl_csrid = '" & cboCSRFtr.SelectedValue & "'"
        lblSLCnt.Text = "Records: " & dsExcution.Tables("CampaignExcution").DefaultView.Count
    End Sub

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Dim strCampID As String
        Dim strChannelID As String

        wndMain.Cursor = Cursors.WaitCursor

        strCampID = Trim(IIf(Me.cboCampaign.SelectedValue Is Nothing, "", Me.cboCampaign.SelectedValue))
        strChannelID = Trim(IIf(Me.cboChannel.SelectedValue Is Nothing, "", Me.cboChannel.SelectedValue))

        If strCampID = "" Or strChannelID = "" Then
            MsgBox("Invalid Campaign / Channel, please check", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        End If

        Call BindExcutionGrid(strCampID, strChannelID)
        wndMain.Cursor = Cursors.Default

        If bm.Count > 0 Then
            Me.uclCustomer.Enabled = True
            cmdAssign.Enabled = True
            radAll.Enabled = True
            radCSR.Enabled = True
            cboCSRFtr.Enabled = True
            cmdShow.Enabled = True
        Else
            Me.uclCustomer.Enabled = False
            cmdAssign.Enabled = False
            radAll.Enabled = False
            radCSR.Enabled = False
            cboCSRFtr.Enabled = False
            cmdShow.Enabled = False
        End If

    End Sub

    Private Sub uclExecute_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TabPage3.Show()
        Me.TabPage1.Show()
        Me.uclCustomer.Enabled = False
        radAll.Enabled = False
        radCSR.Enabled = False
        cboCSRFtr.Enabled = False
        cmdShow.Enabled = False
        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        'If ExternalUser Then
        '    cmdLoad.Enabled = False
        'End If
        gLock = -1
    End Sub

    Private Sub uclExecute_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        'UclWebBrowser1.webBrowser = Nothing
    End Sub

End Class
