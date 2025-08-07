Public Class frmPendingHistMcu
    Inherits System.Windows.Forms.Form

    Dim strPolicy As String
    Dim dtPOLALT As DataTable

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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents grdLog As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdClose = New System.Windows.Forms.Button
        Me.grdLog = New System.Windows.Forms.DataGrid
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(636, 268)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.TabIndex = 74
        Me.cmdClose.Text = "Close"
        '
        'grdLog
        '
        Me.grdLog.AlternatingBackColor = System.Drawing.Color.White
        Me.grdLog.BackColor = System.Drawing.Color.White
        Me.grdLog.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdLog.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLog.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdLog.CaptionVisible = False
        Me.grdLog.DataMember = ""
        Me.grdLog.FlatMode = True
        Me.grdLog.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdLog.ForeColor = System.Drawing.Color.Black
        Me.grdLog.GridLineColor = System.Drawing.Color.Wheat
        Me.grdLog.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdLog.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdLog.HeaderForeColor = System.Drawing.Color.Black
        Me.grdLog.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLog.Location = New System.Drawing.Point(4, 4)
        Me.grdLog.Name = "grdLog"
        Me.grdLog.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdLog.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdLog.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdLog.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLog.Size = New System.Drawing.Size(712, 256)
        Me.grdLog.TabIndex = 73
        '
        'frmPendingHist
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(720, 297)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.grdLog)
        Me.Name = "frmPendingHist"
        Me.Text = "Pending Activity Log"
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID() As String
        Set(ByVal Value As String)
            strPolicy = Value
        End Set
    End Property

    Private Sub frmMarkinHist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strErrMsg As String = ""
        'Dim lngErrNo As Long
        Dim dtPOLALT As New DataTable
        dtPOLALT.TableName = "POLALT"

        wndMain.Cursor = Cursors.WaitCursor

        Try
            'Using wsCRS As New CRSWS.CRSWS()
                'wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
                'If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                '    wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
                'End If
                ''wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
                'wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
                'wsCRS.Timeout = 10000000

                'If Not wsCRS.GetPendingMarkinHist(getCompanyCode(g_McuComp), getEnvCode("POS"), strPolicy, dtPOLALT, strErrMsg) Then
                '    MsgBox("System Error." & vbCrLf & strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                '    Exit Sub
                'End If
                Dim retDs1 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), "GET_PENDING_MARK_IN_HIST", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
                If Not IsNothing(retDs1) Then
	                If retDs1.Tables(0).Rows.Count > 0 Then
		                dtPOLALT = retDs1.Tables(0)
	                End If
                End If

                wndMain.Cursor = Cursors.Default

                If Not dtPOLALT Is Nothing Then
                    If dtPOLALT.Rows.Count > 0 Then
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If

            'End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End Try

        'dtPOLALT = objCS.GetPendingMarkinHist(strPolicy, lngErrNo, strErrMsg)
        'dtPOLALT.TableName = "POLALT"

        'wndMain.Cursor = Cursors.Default

        'If lngErrNo = 0 Then
        '    If Not dtPOLALT Is Nothing Then
        '        If dtPOLALT.Rows.Count > 0 Then
        '        Else
        '            Exit Sub
        '        End If
        '    Else
        '        Exit Sub
        '    End If
        'Else
        '    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '    Exit Sub
        'End If

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "pospl_log_id"
        cs.HeaderText = "Log ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "pospl_action_code"
        cs.HeaderText = "Action"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "pospl_markin_id"
        cs.HeaderText = "Markin ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "pospl_pending_date"
        cs.HeaderText = "Pending Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "pospl_pending_code"
        cs.HeaderText = "Pending Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "pospl_pending_desc"
        cs.HeaderText = "Pending Reason"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "pospl_internal_remarks"
        cs.HeaderText = "Internal Remark"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "pospl_resolve_desc"
        cs.HeaderText = "Resolve Desc."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "pospl_deadline"
        cs.HeaderText = "Deadline"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "pospl_resolve_code"
        cs.HeaderText = "Resolve Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "pospl_first_rem_date"
        cs.HeaderText = "1st Remind Date"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "pospl_final_rem_date"
        cs.HeaderText = "Final Remind Date"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLALT"
        grdLog.TableStyles.Add(ts)

        grdLog.DataSource = dtPOLALT
        grdLog.AllowDrop = False
        grdLog.ReadOnly = True

    End Sub

    Private Sub grdLog_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdLog.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdLog.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdLog.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdLog.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

End Class
