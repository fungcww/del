Imports System.Data.SqlClient

Public Class ServiceLog
    Inherits System.Windows.Forms.UserControl

    Dim strPolicy As String

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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dgSrvLog As System.Windows.Forms.DataGrid
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpServiceEvent As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents lbReceiver As System.Windows.Forms.Label
    Friend WithEvents cbReceiver As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbInitiator As System.Windows.Forms.ComboBox
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventTypeDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventDetail As System.Windows.Forms.ComboBox
    Friend WithEvents cbEventCat As System.Windows.Forms.ComboBox
    Friend WithEvents cbMedium As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents sqlConn As New SqlConnection
    'Friend WithEvents daSrvEvtDet As New SqlDataAdapter("select * from ServiceEventDetail where PolicyAccountID = @policyid", sqlConn)
    'Friend WithEvents daSrvEvtDet As New SqlDataAdapter("select * from ServiceEventDetail where PolicyAccountID", sqlConn)
    Friend WithEvents daSrvEvtDet As SqlDataAdapter
    Friend WithEvents daEvtCat As New SqlDataAdapter("Select * from csw_event_category_code order by cswecc_sort_order", sqlConn)
    Friend WithEvents daEvtType As New SqlDataAdapter("Select * from ServiceEventTypeCodes order by SortOrder", sqlConn)
    Friend WithEvents daEvtTypeDet As New SqlDataAdapter("Select * from csw_event_typedtl_code order by cswetd_sort_order", sqlConn)
    Friend WithEvents daMedium As New SqlDataAdapter("Select * from EventSourceMediumCodes", sqlConn)
    Friend WithEvents daStatus As New SqlDataAdapter("Select * from EventStatusCodes", sqlConn)
    Friend WithEvents daCsr As New SqlDataAdapter("Select CSRID, Name from Csr order by Name", sqlConn)
    Friend WithEvents dsSrvLog As New DataSet

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgSrvLog = New System.Windows.Forms.DataGrid
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.btnNew = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.grpServiceEvent = New System.Windows.Forms.GroupBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.lbReceiver = New System.Windows.Forms.Label
        Me.cbReceiver = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.cbInitiator = New System.Windows.Forms.ComboBox
        Me.cbStatus = New System.Windows.Forms.ComboBox
        Me.cbEventTypeDetail = New System.Windows.Forms.ComboBox
        Me.cbEventDetail = New System.Windows.Forms.ComboBox
        Me.cbEventCat = New System.Windows.Forms.ComboBox
        Me.cbMedium = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpServiceEvent.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgSrvLog
        '
        Me.dgSrvLog.DataMember = ""
        Me.dgSrvLog.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgSrvLog.Location = New System.Drawing.Point(16, 8)
        Me.dgSrvLog.Name = "dgSrvLog"
        Me.dgSrvLog.Size = New System.Drawing.Size(608, 148)
        Me.dgSrvLog.TabIndex = 19
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(632, 40)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(632, 72)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "&Cancel"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox2)
        Me.GroupBox4.Location = New System.Drawing.Point(360, 312)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(352, 96)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Notes"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(16, 28)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(328, 52)
        Me.TextBox2.TabIndex = 0
        Me.TextBox2.Text = ""
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(632, 8)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 15
        Me.btnNew.Text = "&New"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.RadioButton3)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Location = New System.Drawing.Point(408, 168)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(304, 132)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Reminder"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(16, 64)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(160, 56)
        Me.TextBox1.TabIndex = 6
        Me.TextBox1.Text = ""
        '
        'RadioButton3
        '
        Me.RadioButton3.Location = New System.Drawing.Point(80, 20)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(80, 20)
        Me.RadioButton3.TabIndex = 5
        Me.RadioButton3.Text = "Reminder"
        '
        'RadioButton2
        '
        Me.RadioButton2.Location = New System.Drawing.Point(16, 20)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(60, 20)
        Me.RadioButton2.TabIndex = 4
        Me.RadioButton2.Text = "Policy"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(168, 20)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(68, 20)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.Text = "Birthday"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(168, 40)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(128, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'grpServiceEvent
        '
        Me.grpServiceEvent.Controls.Add(Me.TextBox3)
        Me.grpServiceEvent.Controls.Add(Me.lbReceiver)
        Me.grpServiceEvent.Controls.Add(Me.cbReceiver)
        Me.grpServiceEvent.Controls.Add(Me.Label12)
        Me.grpServiceEvent.Controls.Add(Me.Label11)
        Me.grpServiceEvent.Controls.Add(Me.Label10)
        Me.grpServiceEvent.Controls.Add(Me.Label8)
        Me.grpServiceEvent.Controls.Add(Me.Label6)
        Me.grpServiceEvent.Controls.Add(Me.DateTimePicker2)
        Me.grpServiceEvent.Controls.Add(Me.cbInitiator)
        Me.grpServiceEvent.Controls.Add(Me.cbStatus)
        Me.grpServiceEvent.Controls.Add(Me.cbEventTypeDetail)
        Me.grpServiceEvent.Controls.Add(Me.cbEventDetail)
        Me.grpServiceEvent.Controls.Add(Me.cbEventCat)
        Me.grpServiceEvent.Controls.Add(Me.cbMedium)
        Me.grpServiceEvent.Controls.Add(Me.Label7)
        Me.grpServiceEvent.Controls.Add(Me.Label9)
        Me.grpServiceEvent.Location = New System.Drawing.Point(8, 168)
        Me.grpServiceEvent.Name = "grpServiceEvent"
        Me.grpServiceEvent.Size = New System.Drawing.Size(332, 260)
        Me.grpServiceEvent.TabIndex = 14
        Me.grpServiceEvent.TabStop = False
        Me.grpServiceEvent.Text = "Service Event"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(248, 24)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(68, 20)
        Me.TextBox3.TabIndex = 16
        Me.TextBox3.Text = "TextBox3"
        '
        'lbReceiver
        '
        Me.lbReceiver.Location = New System.Drawing.Point(12, 168)
        Me.lbReceiver.Name = "lbReceiver"
        Me.lbReceiver.Size = New System.Drawing.Size(84, 16)
        Me.lbReceiver.TabIndex = 15
        Me.lbReceiver.Text = "Reciever"
        Me.lbReceiver.Visible = False
        '
        'cbReceiver
        '
        Me.cbReceiver.Location = New System.Drawing.Point(104, 164)
        Me.cbReceiver.Name = "cbReceiver"
        Me.cbReceiver.Size = New System.Drawing.Size(140, 21)
        Me.cbReceiver.TabIndex = 14
        Me.cbReceiver.Visible = False
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(12, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(84, 16)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Initiator Date"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(12, 204)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 16)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Initiator"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 144)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Status"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(12, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Event Detail"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Medium"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(104, 224)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(212, 20)
        Me.DateTimePicker2.TabIndex = 6
        '
        'cbInitiator
        '
        Me.cbInitiator.Location = New System.Drawing.Point(104, 200)
        Me.cbInitiator.Name = "cbInitiator"
        Me.cbInitiator.Size = New System.Drawing.Size(212, 21)
        Me.cbInitiator.TabIndex = 5
        '
        'cbStatus
        '
        Me.cbStatus.Location = New System.Drawing.Point(104, 140)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(140, 21)
        Me.cbStatus.TabIndex = 4
        '
        'cbEventTypeDetail
        '
        Me.cbEventTypeDetail.Location = New System.Drawing.Point(104, 104)
        Me.cbEventTypeDetail.Name = "cbEventTypeDetail"
        Me.cbEventTypeDetail.Size = New System.Drawing.Size(212, 21)
        Me.cbEventTypeDetail.TabIndex = 3
        '
        'cbEventDetail
        '
        Me.cbEventDetail.Location = New System.Drawing.Point(104, 80)
        Me.cbEventDetail.Name = "cbEventDetail"
        Me.cbEventDetail.Size = New System.Drawing.Size(212, 21)
        Me.cbEventDetail.TabIndex = 2
        '
        'cbEventCat
        '
        Me.cbEventCat.Location = New System.Drawing.Point(104, 56)
        Me.cbEventCat.Name = "cbEventCat"
        Me.cbEventCat.Size = New System.Drawing.Size(212, 21)
        Me.cbEventCat.TabIndex = 1
        '
        'cbMedium
        '
        Me.cbMedium.Location = New System.Drawing.Point(104, 24)
        Me.cbMedium.Name = "cbMedium"
        Me.cbMedium.Size = New System.Drawing.Size(140, 21)
        Me.cbMedium.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 20)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Event Category"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 16)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Event Type Detail"
        '
        'ServiceLog
        '
        Me.Controls.Add(Me.dgSrvLog)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.grpServiceEvent)
        Me.Name = "ServiceLog"
        Me.Size = New System.Drawing.Size(720, 440)
        CType(Me.dgSrvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.grpServiceEvent.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' This function returns the connection string from the complus
    Private Function getConnStr() As String
        Dim complus As New comCS2005.clsUtility
        'TODO: Testing connection str
        'Return "workstation id=CNG31501PY;user id=vantiveuser;data source=hksqluat1;persist security info=True;initial catalog=vantive;password=holy321"
        Return complus.ConnStr("CSW", "CS2005")
    End Function

    Private Sub frmPolicyMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String

        'TODO: Display the dialog which receive userid and policyid from user
        daSrvEvtDet = New SqlDataAdapter("select * from ServiceEventDetail where PolicyAccountID = '" & strPolicy & "'", sqlConn)
        'daSrvEvtDet.SelectCommand.Parameters.Add("@policyid", SqlDbType.VarChar, 16)
        'daSrvEvtDet.SelectCommand.Parameters("@policyid").Value = strPolicy

        InitDataset()
        If dsSrvLog.Tables("ServiceEventDetail").Rows.Count <> 0 Then
            initForm()
            TextBox3.DataBindings.Add("Text", dsSrvLog, "ServiceEventDetail.ServiceEventNumber")
            cbMedium.DataBindings.Add("SelectedValue", dsSrvLog, "ServiceEventDetail.EventSourceMediumCode")
            cbEventCat.DataBindings.Add("SelectedValue", dsSrvLog, "ServiceEventDetail.EventCategoryCode")
            cbEventDetail.DataBindings.Add("SelectedValue", dsSrvLog, "ServiceEventDetail.EventTypeCode")
            cbEventTypeDetail.DataBindings.Add("SelectedValue", dsSrvLog, "ServiceEventDetail.EventTypeDetailCode")
            cbStatus.DataBindings.Add("SelectedValue", dsSrvLog, "ServiceEventDetail.EventStatusCode")

            dsSrvLog.Tables("ServiceEventDetail").Rows(dgSrvLog.CurrentRowIndex).BeginEdit()
            AddHandler dsSrvLog.Tables("ServiceEventDetail").RowChanged, New DataRowChangeEventHandler(AddressOf OnRowChanged)
            'MsgBox("cancel edit")
            'dsSrvLog.Tables("ServiceEventDetail").Rows(dgSrvLog.CurrentRowIndex).CancelEdit()

        End If


    End Sub

    Private Shared Sub OnRowChanged(ByVal sender As Object, ByVal args As DataRowChangeEventArgs)
        'Dim actionstr As String
        ''actionstr = System.Enum.GetName(args.Action.GetType(), args.Action)

        ''MsgBox(actionstr)
        'If MsgBox("Do you want to save the modified data?", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then

        'Else
        '    'args.Row.CancelEdit()
        '    args.Row.RejectChanges()
        'End If



    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click






    End Sub

    Private Sub cbEventCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventCat.SelectedIndexChanged
        Dim dvEvtDet As DataView
        Dim strCat As String

        strCat = cbEventCat.SelectedValue.ToString
        dvEvtDet = New DataView(dsSrvLog.Tables("ServiceEventTypeCodes"), "EventCategoryCode = '" & strCat & "'", "SortOrder", DataViewRowState.CurrentRows)
        cbEventDetail.DataSource = dvEvtDet
        cbEventDetail.DisplayMember = "EventTypeDesc"
        cbEventDetail.ValueMember = "EventTypeCode"
        'cbEventDetail_SelectedIndexChanged(Me, e)

    End Sub

    Private Sub cbEventDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEventDetail.SelectedIndexChanged
        Dim dvEvtTypeDet As DataView
        Dim strCat As String
        Dim strType As String


        strCat = cbEventCat.SelectedValue.ToString
        strType = cbEventDetail.SelectedValue.ToString
        dvEvtTypeDet = New DataView(dsSrvLog.Tables("csw_event_typedtl_code"), "csw_event_category_code = '" & strCat & "' and csw_event_type_code = '" & strType & "'", "cswetd_sort_order", DataViewRowState.CurrentRows)
        cbEventTypeDetail.DataSource = dvEvtTypeDet
        cbEventTypeDetail.DisplayMember = "csw_event_typedtl_desc"
        cbEventTypeDetail.ValueMember = "csw_event_typedtl_code"

    End Sub

    Private Sub cbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStatus.SelectedIndexChanged
        If cbStatus.SelectedValue.ToString = "H" Then
            cbReceiver.DataSource = dsSrvLog.Tables("csr")
            cbReceiver.DisplayMember = "Name"
            cbReceiver.ValueMember = "CSRID"
            lbReceiver.Visible = True
            cbReceiver.Visible = True
        Else
            cbReceiver.Text = ""
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim dr As DataRow = dsSrvLog.Tables("ServiceEventDetail").NewRow
        Dim cmdBuilder As New SqlCommandBuilder(daSrvEvtDet)

        'Update the new record to dataset
        daSrvEvtDet.Update(dsSrvLog, "ServiceEventDetail")
        dsSrvLog.Tables("ServiceEventDetail").AcceptChanges()
        MsgBox("Service Log saved successfully", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)

    End Sub

    Private Sub InitDataset()
        Dim dcParent(1) As DataColumn
        Dim dcChild(1) As DataColumn

        'Make connection to SQL server
        sqlConn.ConnectionString = getConnStr()
        'Fill the datatables in dataset
        daSrvEvtDet.Fill(dsSrvLog, "ServiceEventDetail")
        daEvtCat.Fill(dsSrvLog, "csw_event_category_code")
        daEvtType.Fill(dsSrvLog, "ServiceEventTypeCodes")
        daEvtTypeDet.Fill(dsSrvLog, "csw_event_typedtl_code")
        daMedium.Fill(dsSrvLog, "EventSourceMediumCodes")
        daStatus.Fill(dsSrvLog, "EventStatusCodes")
        daCsr.Fill(dsSrvLog, "Csr")

        'Add relations to the datatables in dataset
        dsSrvLog.Relations.Add("EvtCat_SrvEvt", dsSrvLog.Tables("csw_event_category_code").Columns("cswecc_code"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode"), False)
        dsSrvLog.Relations.Add("EvtMed_SrvEvt", dsSrvLog.Tables("EventSourceMediumCodes").Columns("EventSourceMediumCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventSourceMediumCode"))
        dsSrvLog.Relations.Add("EvtStat_SrvEvt", dsSrvLog.Tables("EventStatusCodes").Columns("EventStatusCode"), dsSrvLog.Tables("ServiceEventDetail").Columns("EventStatusCode"))
        dsSrvLog.Relations.Add("Csr_SrvEvt_Master", dsSrvLog.Tables("Csr").Columns("CSRID"), dsSrvLog.Tables("ServiceEventDetail").Columns("MasterCSRID"))
        dsSrvLog.Relations.Add("Csr_SrvEvt_Secondary", dsSrvLog.Tables("Csr").Columns("CSRID"), dsSrvLog.Tables("ServiceEventDetail").Columns("SecondaryCSRID"), False)

        dcParent(0) = dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventCategoryCode")
        dcChild(0) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode")
        dcParent(1) = dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventTypeCode")
        dcChild(1) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeCode")
        dsSrvLog.Relations.Add("EvtType_SrvEvt", dcParent, dcChild, False)

        ReDim dcParent(2)
        ReDim dcChild(2)
        dcParent(0) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_category_code")
        dcChild(0) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventCategoryCode")
        dcParent(1) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_type_code")
        dcChild(1) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeCode")
        dcParent(2) = dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_typedtl_code")
        dcChild(2) = dsSrvLog.Tables("ServiceEventDetail").Columns("EventTypeDetailCode")
        dsSrvLog.Relations.Add("EvtTypeDet_SrvEvt", dcParent, dcChild, False)

        'Add dummy field
        With dsSrvLog.Tables("ServiceEventDetail")
            .Columns.Add("cswecc_desc", GetType(String))
            .Columns.Add("EventTypeDesc", GetType(String))
            .Columns.Add("csw_event_typedtl_desc", GetType(String))
            .Columns.Add("sender_name", GetType(String))
            .Columns.Add("EventStatus", GetType(String))
            .Columns.Add("EventSourceMedium", GetType(String))
        End With

        'Create layout of datagrid
        Dim tsSrvLog As New DataGridTableStyle
        Dim cs As DataGridColumnStyle

        'A hidden field that contains the ServiceEventNumber
        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "ServiceEventNumber"
        cs.HeaderText = "Service Event Number"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EventInitialDateTime"
        cs.HeaderText = "Initial Date"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("Csr_SrvEvt_Master", dsSrvLog.Tables("Csr").Columns("Name"))
        cs.Width = 80
        cs.MappingName = "sender_name"
        cs.HeaderText = "Sender"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("EvtStat_SrvEvt", dsSrvLog.Tables("EventStatusCodes").Columns("EventStatus"))
        cs.Width = 80
        cs.MappingName = "EventStatus"
        cs.HeaderText = "Status"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("EvtCat_SrvEvt", dsSrvLog.Tables("csw_event_category_code").Columns("cswecc_desc"))
        cs.Width = 80
        cs.MappingName = "cswecc_desc"
        cs.HeaderText = "Event Category"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("EvtType_SrvEvt", dsSrvLog.Tables("ServiceEventTypeCodes").Columns("EventTypeDesc"))
        cs.Width = 80
        cs.MappingName = "EventTypeDesc"
        cs.HeaderText = "Event Type"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("EvtTypeDet_SrvEvt", dsSrvLog.Tables("csw_event_typedtl_code").Columns("csw_event_typedtl_desc"))
        cs.Width = 120
        cs.MappingName = "csw_event_typedtl_desc"
        cs.HeaderText = "Event Type Detail"
        tsSrvLog.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("EvtMed_SrvEvt", dsSrvLog.Tables("EventSourceMediumCodes").Columns("EventSourceMedium"))
        cs.Width = 0
        cs.MappingName = "EventSourceMedium"
        cs.HeaderText = "Event Medium"
        tsSrvLog.GridColumnStyles.Add(cs)

        'Map the table style to the grid
        tsSrvLog.MappingName = "ServiceEventDetail"
        dgSrvLog.TableStyles.Add(tsSrvLog)

        dgSrvLog.DataSource = dsSrvLog()
        dgSrvLog.DataMember = "ServiceEventDetail"
    End Sub

    Private Sub initForm()
        'Medium Combo Box
        cbMedium.DataSource = dsSrvLog.Tables("EventSourceMediumCodes")
        cbMedium.DisplayMember = "EventSourceMedium"
        cbMedium.ValueMember = "EventSourceMediumCode"

        'Event Category Combo Box
        cbEventCat.DataSource = dsSrvLog.Tables("csw_event_category_code")
        cbEventCat.DisplayMember = "cswecc_desc"
        cbEventCat.ValueMember = "cswecc_code"

        'Status
        cbStatus.DataSource = dsSrvLog.Tables("EventStatusCodes")
        cbStatus.DisplayMember = "EventStatus"
        cbStatus.ValueMember = "EventStatusCode"
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        dsSrvLog.Tables("ServiceEventDetail").Rows(dgSrvLog.CurrentRowIndex).CancelEdit()
    End Sub

    Public Property PolicyAccountID()
        Get
            PolicyAccountID = strPolicy
        End Get
        Set(ByVal Value)
            strPolicy = Value
        End Set
    End Property
End Class
