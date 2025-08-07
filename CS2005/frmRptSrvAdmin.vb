Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmRptSrvAdmin
    Inherits System.Windows.Forms.Form

    Dim dtCampaign, dtChannel As DataTable
    Dim blnCampData As Boolean = False

    Public Property LoadCampData() As Boolean
        Get
        End Get
        Set(ByVal Value As Boolean)
            blnCampData = Value
            If blnCampData = True Then
                Me.cboCampaign.Enabled = True
                Me.cboChannel.Enabled = True
                Me.txtActivity.Enabled = True
                Call loadCampaign()
            End If
        End Set
    End Property

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
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents cboCampaign As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtActivity As System.Windows.Forms.TextBox
    Friend WithEvents chkBounce As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.cboCampaign = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtActivity = New System.Windows.Forms.TextBox
        Me.chkBounce = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(182, 140)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 24)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(266, 140)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "&Cancel"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(80, 100)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(128, 20)
        Me.dtTo.TabIndex = 2
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(80, 68)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(128, 20)
        Me.dtFrom.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "From Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "To Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 16)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Channel:"
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Enabled = False
        Me.cboChannel.Location = New System.Drawing.Point(80, 36)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(128, 21)
        Me.cboChannel.TabIndex = 42
        '
        'cboCampaign
        '
        Me.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCampaign.Enabled = False
        Me.cboCampaign.Location = New System.Drawing.Point(80, 8)
        Me.cboCampaign.Name = "cboCampaign"
        Me.cboCampaign.Size = New System.Drawing.Size(420, 21)
        Me.cboCampaign.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 16)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Campaign:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(220, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 16)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Activity:"
        '
        'txtActivity
        '
        Me.txtActivity.Enabled = False
        Me.txtActivity.Location = New System.Drawing.Point(268, 36)
        Me.txtActivity.Name = "txtActivity"
        Me.txtActivity.Size = New System.Drawing.Size(232, 20)
        Me.txtActivity.TabIndex = 45
        Me.txtActivity.Text = "'RIDR','URIDR'"
        '
        'chkBounce
        '
        Me.chkBounce.Enabled = False
        Me.chkBounce.Location = New System.Drawing.Point(220, 72)
        Me.chkBounce.Name = "chkBounce"
        Me.chkBounce.Size = New System.Drawing.Size(148, 24)
        Me.chkBounce.TabIndex = 46
        Me.chkBounce.Text = "Check Bounced Email"
        '
        'frmRptSrvAdmin
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(520, 173)
        Me.Controls.Add(Me.chkBounce)
        Me.Controls.Add(Me.txtActivity)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboChannel)
        Me.Controls.Add(Me.cboCampaign)
        Me.Controls.Add(Me.dtFrom)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmRptSrvAdmin"
        Me.Text = "Report Printing"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub loadCampaign()
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If blnCampData = True Then
            dtCampaign = New DataTable
            dtChannel = New DataTable
        End If

        strSQL = "Select distinct crmcmp_campaign_id, crmcmp_campaign_name " & _
            " From  " & serverPrefix & "crm_campaign "
        LoadComboBox(dtCampaign, cboCampaign, "crmcmp_campaign_id", "crmcmp_campaign_name", strSQL)
        loadChannel(cboCampaign.SelectedValue)

    End Sub

    Private Sub loadChannel(ByVal campID As String)

        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select 'ALL' as crmcpc_channel_id, 'ALL' as crmcpt_channel_desc UNION ALL "

        strSQL &= "Select crmcpc_channel_id, crmcpt_channel_desc " & _
            " From " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_channel_type " & _
            " Where crmcpc_campaign_id = '" & campID & "' and crmcpc_channel_id = crmcpt_channel_id and crmcpc_status_id <> '03'"
        LoadComboBox(dtChannel, cboChannel, "crmcpc_channel_id", "crmcpt_channel_desc", strSQL)

    End Sub

    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As ComboBox, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean
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

End Class
