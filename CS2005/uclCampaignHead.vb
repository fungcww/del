Imports System.Data.SqlClient

Public Class uclCampaignHead

    Inherits System.Windows.Forms.UserControl

    Dim dtCampaign As DataTable
    Dim strCurCampaign As String
    Dim strCurChannel As String

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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents cboCampaign As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.cboCampaign = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(528, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 16)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Channel:"
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Items.AddRange(New Object() {"Email", "Outbound Call"})
        Me.cboChannel.Location = New System.Drawing.Point(584, 4)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(96, 21)
        Me.cboChannel.TabIndex = 42
        '
        'cboCampaign
        '
        Me.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCampaign.Items.AddRange(New Object() {"我愛ING安泰曼聯盃", "Refundable Hospital Income"})
        Me.cboCampaign.Location = New System.Drawing.Point(68, 4)
        Me.cboCampaign.Name = "cboCampaign"
        Me.cboCampaign.Size = New System.Drawing.Size(448, 21)
        Me.cboCampaign.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Campaign:"
        '
        'uclCampaignHead
        '
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboChannel)
        Me.Controls.Add(Me.cboCampaign)
        Me.Controls.Add(Me.Label3)
        Me.Name = "uclCampaignHead"
        Me.Size = New System.Drawing.Size(684, 28)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub uclCampaignHead_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim lngErrNo As Long
        Dim strErrMsg As String

        dtCampaign = GetActiveCampaign(lngErrNo, strErrMsg)

        If dtCampaign.Rows.Count > 0 Then
            For i As Integer = 0 To dtCampaign.Rows.Count - 1
                cboCampaign.Items.Add(Trim(dtCampaign.Rows(i).Item("crmcmp_campaign_id")) & " - " & dtCampaign.Rows(i).Item("crmcmp_campaign_name"))
            Next
        End If

    End Sub

    Private Sub cboCampaign_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCampaign.SelectedValueChanged

        strCurCampaign = Microsoft.VisualBasic.Strings.Left(cboCampaign.SelectedText, 15)

        If dtCampaign.Rows.Count > 0 Then
            For i As Integer = 0 To dtCampaign.Rows.Count - 1
                If dtCampaign.Rows(i).Item("crmcmp_campaign_id") = strCurCampaign Then
                    cboChannel.Items.Add(dtCampaign.Rows(i).Item("crmcpt_cnahhel_desc"))
                End If
            Next
        End If

    End Sub

    Private Function GetActiveCampaign(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtCampaign As DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select * " & _
                " From " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_channel_type " & _
                " Where crmcmp_status_id <> 'C' " & _
                " And crmcmp_campaign_id = crmcpc_campaign_id " & _
                " And crmcpc_channel_id = crmcpt_channel_id "

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(dtCampaign)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = -1
            strErrMsg = sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.ToString
        End Try

        Return dtCampaign

    End Function

End Class
