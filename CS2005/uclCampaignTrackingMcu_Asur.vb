Imports System.Data.SqlClient

Public Class uclCampaignTrackingMcu_Asur
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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

    Friend WithEvents dtAppointment As DateTimePicker
    Friend WithEvents lblAppointDate As Label
    Friend WithEvents cmdDelete As Button
    Friend WithEvents cmdAdd As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cmdSave As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cboHandoff As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtComment As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cboCallStatus As ComboBox
    Friend WithEvents grdTracking As DataGrid
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpCallDate As DateTimePicker

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dtAppointment = New System.Windows.Forms.DateTimePicker()
        Me.lblAppointDate = New System.Windows.Forms.Label()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboHandoff = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboCallStatus = New System.Windows.Forms.ComboBox()
        Me.grdTracking = New System.Windows.Forms.DataGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpCallDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.grdTracking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtAppointment
        '
        Me.dtAppointment.CustomFormat = "dddd, dd MMMM, yyyy     hh:mm:ss tt"
        Me.dtAppointment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAppointment.Location = New System.Drawing.Point(121, 210)
        Me.dtAppointment.Name = "dtAppointment"
        Me.dtAppointment.Size = New System.Drawing.Size(268, 26)
        Me.dtAppointment.TabIndex = 120
        '
        'lblAppointDate
        '
        Me.lblAppointDate.Location = New System.Drawing.Point(19, 210)
        Me.lblAppointDate.Name = "lblAppointDate"
        Me.lblAppointDate.Size = New System.Drawing.Size(96, 16)
        Me.lblAppointDate.TabIndex = 119
        Me.lblAppointDate.Text = "Appointment Date"
        Me.lblAppointDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdDelete
        '
        Me.cmdDelete.Enabled = False
        Me.cmdDelete.Location = New System.Drawing.Point(760, 283)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(75, 37)
        Me.cmdDelete.TabIndex = 118
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdAdd
        '
        Me.cmdAdd.Enabled = False
        Me.cmdAdd.Location = New System.Drawing.Point(600, 283)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 37)
        Me.cmdAdd.TabIndex = 117
        Me.cmdAdd.Text = "&Add"
        '
        'cmdEdit
        '
        Me.cmdEdit.Enabled = False
        Me.cmdEdit.Location = New System.Drawing.Point(680, 283)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(75, 37)
        Me.cmdEdit.TabIndex = 116
        Me.cmdEdit.Text = "&Edit"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(920, 283)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 37)
        Me.cmdCancel.TabIndex = 115
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(840, 283)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 37)
        Me.cmdSave.TabIndex = 114
        Me.cmdSave.Text = "Save"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(234, 180)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 20)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Handoff:"
        '
        'cboHandoff
        '
        Me.cboHandoff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHandoff.Location = New System.Drawing.Point(311, 176)
        Me.cboHandoff.Name = "cboHandoff"
        Me.cboHandoff.Size = New System.Drawing.Size(128, 28)
        Me.cboHandoff.TabIndex = 112
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 244)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 20)
        Me.Label4.TabIndex = 111
        Me.Label4.Text = "Comment:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(121, 241)
        Me.txtComment.MaxLength = 255
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(874, 36)
        Me.txtComment.TabIndex = 110
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 20)
        Me.Label1.TabIndex = 109
        Me.Label1.Text = "Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Location = New System.Drawing.Point(83, 176)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(136, 28)
        Me.cboStatus.TabIndex = 108
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(440, 149)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 20)
        Me.Label11.TabIndex = 107
        Me.Label11.Text = "Call Status:"
        '
        'cboCallStatus
        '
        Me.cboCallStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCallStatus.Location = New System.Drawing.Point(549, 146)
        Me.cboCallStatus.Name = "cboCallStatus"
        Me.cboCallStatus.Size = New System.Drawing.Size(192, 28)
        Me.cboCallStatus.TabIndex = 106
        '
        'grdTracking
        '
        Me.grdTracking.AlternatingBackColor = System.Drawing.Color.White
        Me.grdTracking.BackColor = System.Drawing.Color.White
        Me.grdTracking.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdTracking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdTracking.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTracking.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdTracking.CaptionVisible = False
        Me.grdTracking.DataMember = ""
        Me.grdTracking.FlatMode = True
        Me.grdTracking.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdTracking.ForeColor = System.Drawing.Color.Black
        Me.grdTracking.GridLineColor = System.Drawing.Color.Wheat
        Me.grdTracking.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdTracking.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdTracking.HeaderForeColor = System.Drawing.Color.Black
        Me.grdTracking.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTracking.Location = New System.Drawing.Point(3, 3)
        Me.grdTracking.Name = "grdTracking"
        Me.grdTracking.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdTracking.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdTracking.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdTracking.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTracking.Size = New System.Drawing.Size(1001, 134)
        Me.grdTracking.TabIndex = 105
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(127, 20)
        Me.Label5.TabIndex = 104
        Me.Label5.Text = "Call Date && Time"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpCallDate
        '
        Me.dtpCallDate.CustomFormat = "dddd, dd MMMM, yyyy     hh:mm:ss tt"
        Me.dtpCallDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCallDate.Location = New System.Drawing.Point(151, 144)
        Me.dtpCallDate.Name = "dtpCallDate"
        Me.dtpCallDate.Size = New System.Drawing.Size(268, 26)
        Me.dtpCallDate.TabIndex = 103
        '
        'uclCampaignTrackingMcu
        '
        Me.Controls.Add(Me.dtAppointment)
        Me.Controls.Add(Me.lblAppointDate)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboHandoff)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboCallStatus)
        Me.Controls.Add(Me.grdTracking)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpCallDate)
        Me.Name = "uclCampaignTrackingMcu"
        Me.Size = New System.Drawing.Size(1014, 332)
        CType(Me.grdTracking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strCustID, strCampID, strChannelID As String
    Private _ReadOnlyMode As Boolean
    Dim dsTracking As DataSet = New DataSet("Tracking")
    Private blnAdmin As Boolean = False
    Private blnCSUser As Boolean = False
    Private blnEditable As Boolean = False
    Private blnFillHandOff As Boolean = False
    Private blnNew As Boolean
    Dim WithEvents bm As BindingManagerBase

    Public Property CustID(ByVal strCampaignID As String, Optional ByVal strCampChannelID As String = "ALL") As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    '*** For Temp Use***
                    blnAdmin = True
                    blnCSUser = True
                    '*** For Temp Use***
                    blnNew = False
                    strCustID = Value
                    strCampID = strCampaignID
                    strChannelID = strCampChannelID
                    Call buildUI()
                End If
            Else
                'Clear All Data and disable all control
                strCustID = "-1"
                strCampID = gNULLText
                strChannelID = gNULLText
                Call buildUI()
                Call EnableButtons(False)
            End If
        End Set
    End Property

    Public Property ReadOnlyMode() As Boolean
        Get
            Return _ReadOnlyMode
        End Get
        Set(ByVal Value As Boolean)
            _ReadOnlyMode = Value
            Me.dtpCallDate.Enabled = Not Value
            Me.cboCallStatus.Enabled = Not Value
            Me.cboStatus.Enabled = Not Value
            Me.cboHandoff.Enabled = Not Value
            Me.dtAppointment.Enabled = Not Value
            Me.txtComment.Enabled = Not Value
            Me.cmdAdd.Enabled = Not Value
            Me.cmdEdit.Enabled = Not Value
            Me.cmdDelete.Enabled = Not Value
            Me.cmdSave.Enabled = Not Value
            Me.cmdCancel.Enabled = Not Value
        End Set
    End Property

    'Call By Other Form to enable buttons
    Public Sub EnableButtons(ByVal blnCanEdit As Boolean)
        blnEditable = blnCanEdit
        Me.cmdAdd.Enabled = blnAdmin And blnEditable
        If bm.Count > 0 Then
            Me.cmdEdit.Enabled = blnAdmin And blnEditable
            Me.cmdDelete.Enabled = blnAdmin And blnEditable
        Else
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
        End If
    End Sub


    Private Sub buildUI()

        Call EnableControls(False)
        Call InitButtons()

        Call FillCboHandOff()
        Call FillCboCallStatus()
        Call FillCboStatus()

        Call BindTrackingData()

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdAdd.Enabled = False
        '    cmdEdit.Enabled = False
        '    cmdDelete.Enabled = False
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        'End If
        ''CRS 7x24 Changes - End

    End Sub

#Region "Get Data"
    Private Function GetCampaignTracking(ByRef dtData As DataTable, ByRef strErrMsg As String,
                                    Optional ByVal strCustumerID As String = "ALL", Optional ByVal strCampaignID As String = "ALL",
                                    Optional ByVal strChannelID As String = "ALL", Optional ByVal datCallDateTime As Date = #1/1/1900#) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(3) As DataColumn
        Dim strSQL As String
        Dim strWhere As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        GetCampaignTracking = False

        Try
            sqlConn.ConnectionString = strCIWMcuConn

            dtData = New DataTable("Tracking")

            'Get Campaign Tracking
            strSQL = "Select crmctk_campaign_id as 'CampaignID', crmctk_call_datetime as 'CallDate',  right(convert(varchar,crmctk_call_datetime,100),7) as 'CallTime', "
            strSQL &= " crmctk_customer_id as 'CustomerID', crmctk_channel_id as 'ChannelID',"
            strSQL &= " crmctk_call_status as 'CallStatus', crmctk_handoff as 'HandOff', crmctk_comment as 'Comment', crmctk_status as 'Status',"
            strSQL &= " crmctk_appointment_date as 'AppointmentDate', crmtcs_status_desc as CallStatusDesc, crmcts_status_desc as StatusDesc"
            strSQL &= " From " & serverPrefix & "crm_campaign_tracking Inner Join " & serverPrefix & "crm_campaign_tracking_callstatus"
            strSQL &= " ON crmctk_call_status =  crmtcs_status_id"
            strSQL &= " Inner Join " & serverPrefix & "crm_campaign_tracking_status"
            strSQL &= " ON crmctk_status =  crmcts_status_id"
            strWhere = ""
            If strCustumerID <> "ALL" Then
                strWhere &= " And crmctk_customer_id = '" & strCustumerID.Trim.Replace("'", "''") & "'"
            End If
            If strCampaignID <> "ALL" Then
                strWhere &= " And crmctk_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            End If
            If strChannelID <> "ALL" Then
                strWhere &= " And crmctk_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            End If
            If datCallDateTime <> #1/1/1900# Then
                strWhere &= " And crmctk_call_datetime = '" & datCallDateTime.ToString("yyyy-MM-dd hh:mm:ss") & "'"
            End If
            If strWhere.Length > 4 Then
                strWhere = " Where" & strWhere.Substring(4)
            End If
            strSQL &= strWhere & " Order by crmctk_campaign_id, crmctk_channel_id, crmctk_customer_id"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("CampaignID")
            aryDCPrimary(1) = dtData.Columns("CallDate")
            aryDCPrimary(2) = dtData.Columns("CustomerID")
            aryDCPrimary(3) = dtData.Columns("ChannelID")
            dtData.PrimaryKey = aryDCPrimary

            GetCampaignTracking = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function GetHandoffPeople(ByRef dtData As DataTable, ByRef strErrMsg As String) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(0) As DataColumn
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        GetHandoffPeople = False

        Try
            sqlConn.ConnectionString = strCIWMcuConn

            dtData = New DataTable("Tracking")

            'Get Campaign Tracking
            strSQL = "Select distinct csrid, name from " & serverPrefix & "csr where csrid like 'CSR%' Order By name"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("csrid")
            dtData.PrimaryKey = aryDCPrimary

            GetHandoffPeople = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function GetCallStatus(ByRef dtData As DataTable, ByRef strErrMsg As String) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(0) As DataColumn
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        GetCallStatus = False

        Try
            sqlConn.ConnectionString = strCIWMcuConn

            dtData = New DataTable("Tracking")

            'Get Campaign Tracking
            strSQL = "Select crmtcs_status_id as StatusID, crmtcs_status_desc as StatusDesc from " & serverPrefix & "crm_campaign_tracking_callstatus"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("StatusID")
            dtData.PrimaryKey = aryDCPrimary

            GetCallStatus = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function GetTrackingStatus(ByRef dtData As DataTable, ByRef strErrMsg As String) As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(0) As DataColumn
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        GetTrackingStatus = False

        Try
            sqlConn.ConnectionString = strCIWMcuConn

            dtData = New DataTable("Tracking")

            'Get Campaign Tracking
            strSQL = "Select crmcts_status_id as StatusID, crmcts_status_desc as StatusDesc from " & serverPrefix & "crm_campaign_tracking_status"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("StatusID")
            dtData.PrimaryKey = aryDCPrimary

            GetTrackingStatus = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function
#End Region

#Region "FillComboBox"
    Private Sub FillCboHandOff()
        Dim dtData As DataTable
        Dim drData As DataRow
        Dim sqlDT As DataTable
        Dim sqlDV As DataView

        Dim strErrMsg As String

        Try
            If GetHandoffPeople(dtData, strErrMsg) Then
                'Add Blank Record
                sqlDT = dtData.Copy
                drData = sqlDT.NewRow
                drData.Item("csrid") = ""
                drData.Item("name") = ""
                sqlDT.Rows.InsertAt(drData, 0)
                sqlDV = sqlDT.DefaultView
                sqlDV.Sort = "name"

                blnFillHandOff = True
                Me.cboHandoff.DataSource = sqlDV
                Me.cboHandoff.DisplayMember = "name"
                Me.cboHandoff.ValueMember = "csrid"
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            blnFillHandOff = False
        End Try

    End Sub

    Private Sub FillCboCallStatus()
        Dim dtData As DataTable
        Dim sqlDV As DataView

        Dim strErrMsg As String

        If GetCallStatus(dtData, strErrMsg) Then
            sqlDV = dtData.DefaultView
            sqlDV.Sort = "StatusID"

            Me.cboCallStatus.DataSource = sqlDV
            Me.cboCallStatus.DisplayMember = "StatusDesc"
            Me.cboCallStatus.ValueMember = "StatusID"
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub

    Private Sub FillCboStatus()
        Dim dtData As DataTable
        Dim sqlDV As DataView

        Dim strErrMsg As String

        If GetTrackingStatus(dtData, strErrMsg) Then
            sqlDV = dtData.DefaultView
            sqlDV.Sort = "StatusID"

            Me.cboStatus.DataSource = sqlDV
            Me.cboStatus.DisplayMember = "StatusDesc"
            Me.cboStatus.ValueMember = "StatusID"
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub
#End Region

    Private Sub EnableControls(ByVal blnEnabled As Boolean)
        Me.dtpCallDate.Enabled = blnEnabled
        Me.cboCallStatus.Enabled = blnEnabled
        Me.cboStatus.Enabled = blnEnabled
        Me.cboHandoff.Enabled = blnEnabled And blnCSUser
        Me.txtComment.Enabled = blnEnabled
        Me.dtAppointment.Enabled = blnEnabled
    End Sub

    Private Sub InitButtons()
        Me.cmdAdd.Enabled = blnAdmin And blnEditable
        Me.cmdEdit.Enabled = blnAdmin And blnEditable
        Me.cmdDelete.Enabled = blnAdmin And blnEditable
        Me.cmdSave.Enabled = False
        Me.cmdCancel.Enabled = False
    End Sub

    Private Sub ClearTextBox()
        Me.dtpCallDate.Value = Now
        If Me.cboCallStatus.Items.Count > 0 Then
            Me.cboCallStatus.SelectedIndex = 0
        End If
        If Me.cboStatus.Items.Count > 0 Then
            Me.cboStatus.SelectedIndex = 0
        End If
        If Me.cboHandoff.Items.Count > 0 Then
            Me.cboHandoff.SelectedIndex = 0
        End If
        Me.txtComment.Text = gNULLText
        Me.dtAppointment.Value = Now
    End Sub

    Private Sub BuildGridStyle()
        Dim ts1 As New clsDataGridTableStyle
        Dim cs1 As DataGridTextBoxColumn

        cs1 = New DataGridTextBoxColumn
        cs1.Width = 80
        cs1.MappingName = "CallDate"
        cs1.HeaderText = "Call Date"
        cs1.NullText = gNULLText
        cs1.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs1)

        cs1 = New DataGridTextBoxColumn
        cs1.Width = 80
        cs1.MappingName = "CallTime"
        cs1.HeaderText = "Call Time"
        cs1.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs1)

        cs1 = New DataGridTextBoxColumn
        cs1.Width = 250
        cs1.MappingName = "CallStatusDesc"
        cs1.HeaderText = "Call Status"
        cs1.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs1)

        cs1 = New DataGridTextBoxColumn
        cs1.Width = 120
        cs1.MappingName = "StatusDesc"
        cs1.HeaderText = "Status"
        cs1.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs1)

        ts1.MappingName = "Tracking"
        grdTracking.TableStyles.Clear()
        grdTracking.TableStyles.Add(ts1)

        grdTracking.DataSource = dsTracking.Tables("Tracking")
        dsTracking.Tables("Tracking").DefaultView.Sort = "CallDate DESC, CallTime DESC"
        grdTracking.AllowDrop = False
        grdTracking.ReadOnly = True

    End Sub

    Private Sub BindTrackingData()
        Dim dtData As DataTable
        Dim strErrMsg As String

        If GetCampaignTracking(dtData, strErrMsg, strCustID, strCampID, strChannelID) Then
            If dsTracking.Tables.Contains(dtData.TableName) Then
                dsTracking.Tables(dtData.TableName).Constraints.Clear()
                dsTracking.Relations.Clear()
                dsTracking.Tables.Remove(dtData.TableName)
            End If
            dsTracking.Tables.Add(dtData)
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

        Call BuildGridStyle()

        bm = Me.BindingContext(dsTracking.Tables("Tracking"))

        If bm.Count = 0 Then
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
        End If

        Call UpdatePT()

    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow

        If bm.Count > 0 Then
            drI = CType(bm.Current, DataRowView).Row()
        End If

        If Not drI Is Nothing Then
            Me.txtComment.Text = drI.Item("Comment")
            Try
                blnFillHandOff = True
                Me.cboHandoff.SelectedValue = drI.Item("HandOff")
            Catch ex As Exception
                Throw ex
            Finally
                blnFillHandOff = False
            End Try
            Me.dtpCallDate.Value = drI.Item("CallDate")
            Me.cboCallStatus.SelectedValue = drI.Item("CallStatus")
            Me.cboStatus.SelectedValue = drI.Item("Status")
            Me.dtAppointment.Value = drI.Item("AppointmentDate")
        Else
            Call EnableControls(False)
            Call ClearTextBox()
        End If
    End Sub

    Private Function Validation() As Boolean
        Dim strErrMsg As String

        Validation = False

        'Check Input

        Validation = True

    End Function

    Private Sub SaveCampaignTracking()
        Dim sqlConn As New SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sqlTran As SqlTransaction
        Dim dtData As DataTable
        Dim strErrMsg As String

        Dim datCallDate As Date
        Dim strCallStatus As String
        Dim strStatus As String
        Dim strHandoff As String
        Dim strComment As String
        Dim datAppointment As Date

        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        datCallDate = Me.dtpCallDate.Value
        strCallStatus = Me.cboCallStatus.SelectedValue
        strStatus = IIf(Me.cboStatus.SelectedValue Is Nothing, gNULLText, Me.cboStatus.SelectedValue)
        strHandoff = IIf(Me.cboHandoff.SelectedValue Is Nothing, gNULLText, Me.cboHandoff.SelectedValue)
        strComment = Me.txtComment.Text.Trim
        datAppointment = Me.dtAppointment.Value

        If blnNew Then

            'Check Campaign Tracking is existing or not
            If GetCampaignTracking(dtData, strErrMsg, strCustID, strCampID, strChannelID, datCallDate) Then
                If dtData.Rows.Count > 0 Then
                    MsgBox("Campaign Tracking is existing.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            strSQL = "Insert into " & serverPrefix & "crm_campaign_tracking (crmctk_campaign_id, crmctk_call_datetime, crmctk_customer_id, crmctk_channel_id,"
            strSQL &= " crmctk_call_status, crmctk_status, crmctk_handoff, crmctk_comment, crmctk_appointment_date,"
            strSQL &= " crmctk_create_user, crmctk_create_date, crmctk_update_user, crmctk_update_date)"
            strSQL &= " Values ('" & strCampID.Replace("'", "''") & "', '" & datCallDate.ToString("yyyy-MM-dd hh:mm:ss") & "', '"
            strSQL &= strCustID.Replace("'", "''") & "', '" & strChannelID.Replace("'", "''") & "', '"
            strSQL &= strCallStatus.Replace("'", "''") & "', '" & strStatus.Replace("'", "''") & "', '" & strHandoff.Replace("'", "''") & "', '" & strComment.Replace("'", "''") & "', '"
            strSQL &= datAppointment.ToString("yyyy-MM-dd hh:mm:ss") & "', '" & gsUser.Replace("'", "''") & "', GetDate(), '" & gsUser.Replace("'", "''") & "', GetDate())"

        Else

            strSQL = "Update " & serverPrefix & "crm_campaign_tracking Set crmctk_comment = '" & strComment.Replace("'", "''") & "',"
            strSQL &= " crmctk_update_user = '" & gsUser.Replace("'", "''") & "',"
            strSQL &= " crmctk_update_date = GetDate()"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_call_datetime = '" & datCallDate.ToString("yyyy-MM-dd hh:mm:ss") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'"

        End If


        Try
            sqlConn.ConnectionString = strCIWMcuConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand
            sqlTran = sqlConn.BeginTransaction("AddTracking")
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTran

            'Insert/Update Campaign Tracking
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

            'Get Latest Call Status
            strSQL = "Select crmctk_status From " & serverPrefix & "crm_campaign_tracking TA Inner Join"
            strSQL &= " (Select crmctk_campaign_id, Max(crmctk_call_datetime) As crmctk_call_datetime,"
            strSQL &= " crmctk_customer_id, crmctk_channel_id From " & serverPrefix & "crm_campaign_tracking"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            strSQL &= " Group by crmctk_campaign_id, crmctk_customer_id, crmctk_channel_id) TB"
            strSQL &= " ON TA.crmctk_campaign_id = TB.crmctk_campaign_id And TA.crmctk_call_datetime = TB.crmctk_call_datetime"
            strSQL &= " AND TA.crmctk_customer_id = TB.crmctk_customer_id And TA.crmctk_channel_id = TB.crmctk_channel_id"
            sqlCmd.CommandText = strSQL
            strStatus = sqlCmd.ExecuteScalar()
            If strStatus Is Nothing Then
                strStatus = gNULLText
            End If

            'Update Campaign Sale Leads
            strSQL = "Update " & serverPrefix & "crm_campaign_sales_leads Set crmcsl_status = '" & strStatus.Replace("'", "''") & "',"
            strSQL &= " crmcsl_last_call = (Select Max(crmctk_call_datetime) From " & serverPrefix & "crm_campaign_tracking"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'),"
            strSQL &= " crmcsl_update_user = '" & gsUser.Replace("'", "''") & "',"
            strSQL &= " crmcsl_update_date = GetDate()"
            strSQL &= " Where crmcsl_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_customer_id = '" & strCustID.Replace("'", "''") & "'"
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()
            sqlTran.Commit()

            'Add Service Log
            If Me.cboHandoff.Text.Length > 0 Then
                '---Pending (No Need)
            End If

            blnNew = False
            gCrmMode = ""

            Call EnableControls(False)
            Call InitButtons()
            Call BindTrackingData()
            'Refresh Parent User Control's grid
            Try
                Call CType(Me.Parent.Parent.Parent, uclExecute).RefreshExecuteGrid(strCampID, strChannelID, strCustID, False)
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Try
                sqlTran.Rollback("AddTracking")
            Catch ex1 As Exception
                'Throw ex1
            End Try
            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            sqlCmd.Dispose()
            sqlTran.Dispose()
        End Try

    End Sub

    Private Sub DelCampaignTracking()
        Dim sqlConn As New SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sqlTran As SqlTransaction

        Dim datCallDate As Date
        Dim strStatus As String
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDBMcu)

        datCallDate = Me.dtpCallDate.Value

        Try
            sqlConn.ConnectionString = strCIWMcuConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand
            sqlTran = sqlConn.BeginTransaction("DelTracking")
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTran

            'Delete Campaign Channels
            strSQL = "Delete " & serverPrefix & "crm_campaign_tracking"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_call_datetime = '" & datCallDate.ToString("yyyy-MM-dd hh:mm:ss") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

            'Get Latest Call Status
            strSQL = "Select crmctk_status From " & serverPrefix & "crm_campaign_tracking TA Inner Join"
            strSQL &= " (Select crmctk_campaign_id, Max(crmctk_call_datetime) As crmctk_call_datetime,"
            strSQL &= " crmctk_customer_id, crmctk_channel_id From " & serverPrefix & "crm_campaign_tracking"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            strSQL &= " Group by crmctk_campaign_id, crmctk_customer_id, crmctk_channel_id) TB"
            strSQL &= " ON TA.crmctk_campaign_id = TB.crmctk_campaign_id And TA.crmctk_call_datetime = TB.crmctk_call_datetime"
            strSQL &= " AND TA.crmctk_customer_id = TB.crmctk_customer_id And TA.crmctk_channel_id = TB.crmctk_channel_id"
            sqlCmd.CommandText = strSQL
            strStatus = sqlCmd.ExecuteScalar()
            If strStatus Is Nothing Then
                strStatus = gNULLText
            End If

            'Update Campaign Sale Leads
            strSQL = "Update " & serverPrefix & "crm_campaign_sales_leads"
            strSQL &= " Set crmcsl_status = '" & strStatus.Replace("'", "''") & "',"
            strSQL &= " crmcsl_last_call = (Select Max(crmctk_call_datetime) From " & serverPrefix & "crm_campaign_tracking"
            strSQL &= " Where crmctk_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_customer_id = '" & strCustID.Replace("'", "''") & "'"
            strSQL &= " And crmctk_channel_id = '" & strChannelID.Replace("'", "''") & "'),"
            strSQL &= " crmcsl_update_user = '" & gsUser.Replace("'", "''") & "',"
            strSQL &= " crmcsl_update_date = GetDate()"
            strSQL &= " Where crmcsl_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            strSQL &= " And crmcsl_customer_id = '" & strCustID.Replace("'", "''") & "'"
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()
            sqlTran.Commit()

            Call BindTrackingData()
            'Refresh Parent User Control's grid
            Try
                Call CType(Me.Parent.Parent.Parent, uclExecute).RefreshExecuteGrid(strCampID, strChannelID, strCustID, False)
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Try
                sqlTran.Rollback("DelTracking")
            Catch ex1 As Exception
                'Throw ex1
            End Try
            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            sqlCmd.Dispose()
            sqlTran.Dispose()
        End Try

    End Sub

    Private Sub bm_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bm.CurrentChanged
        Try
            blnNew = False
            gCrmMode = ""

            Call EnableControls(False)
            Call InitButtons()
            Call UpdatePT()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub grdTracking_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTracking.MouseUp
        Try
            Dim pt = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = grdTracking.HitTest(pt)

            If hti.Type = DataGrid.HitTestType.Cell Then
                grdTracking.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                grdTracking.Select(hti.Row)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Try
            blnNew = True
            gCrmMode = "CampaignExec"

            'Enabel all controls for add new record
            Call EnableControls(True)
            Me.dtAppointment.Enabled = False
            Me.cmdAdd.Enabled = False
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
            Me.cmdSave.Enabled = True
            Me.cmdCancel.Enabled = True

            Call ClearTextBox()
            Me.dtpCallDate.Focus()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try
            blnNew = False
            gCrmMode = ""

            Call EnableControls(False)
            Me.cmdAdd.Enabled = True
            Me.cmdEdit.Enabled = (Me.bm.Count > 0)
            Me.cmdDelete.Enabled = (Me.bm.Count > 0)
            Me.cmdSave.Enabled = False
            Me.cmdCancel.Enabled = False

            Call UpdatePT()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try
            If MsgBox("Are you sure to delete this campaign tracking record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Delete Campaign Tracking") = MsgBoxResult.Yes Then
                Call DelCampaignTracking()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try
            Me.txtComment.Enabled = True
            Me.dtpCallDate.Enabled = False
            Me.cmdAdd.Enabled = False
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
            Me.cmdSave.Enabled = True
            Me.cmdCancel.Enabled = True
            Me.txtComment.Focus()

            gCrmMode = "CampaignExec"

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try
            If Validation() Then
                Call SaveCampaignTracking()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cboHandoff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboHandoff.SelectedIndexChanged
        If Not blnFillHandOff Then
            If Me.cboHandoff.Text.Trim.Length > 0 Then
                Me.dtAppointment.Enabled = True
            Else
                Me.dtAppointment.Enabled = False
                Me.dtAppointment.Value = Now
            End If
        End If
    End Sub

End Class
