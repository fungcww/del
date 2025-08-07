Imports System.Data.SqlClient

Public Class uclGI
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grdGI As System.Windows.Forms.DataGrid
    Friend WithEvents cmdGIOpen As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdGIOpen = New System.Windows.Forms.Button
        Me.grdGI = New System.Windows.Forms.DataGrid
        CType(Me.grdGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdGIOpen
        '
        Me.cmdGIOpen.Location = New System.Drawing.Point(640, 4)
        Me.cmdGIOpen.Name = "cmdGIOpen"
        Me.cmdGIOpen.Size = New System.Drawing.Size(64, 23)
        Me.cmdGIOpen.TabIndex = 34
        Me.cmdGIOpen.Text = "Open"
        '
        'grdGI
        '
        Me.grdGI.AlternatingBackColor = System.Drawing.Color.White
        Me.grdGI.BackColor = System.Drawing.Color.White
        Me.grdGI.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdGI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdGI.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdGI.CaptionVisible = False
        Me.grdGI.DataMember = ""
        Me.grdGI.FlatMode = True
        Me.grdGI.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdGI.ForeColor = System.Drawing.Color.Black
        Me.grdGI.GridLineColor = System.Drawing.Color.Wheat
        Me.grdGI.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdGI.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdGI.HeaderForeColor = System.Drawing.Color.Black
        Me.grdGI.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.Location = New System.Drawing.Point(8, 0)
        Me.grdGI.Name = "grdGI"
        Me.grdGI.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdGI.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdGI.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdGI.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.Size = New System.Drawing.Size(624, 244)
        Me.grdGI.TabIndex = 32
        '
        'uclGI
        '
        Me.Controls.Add(Me.cmdGIOpen)
        Me.Controls.Add(Me.grdGI)
        Me.Name = "uclGI"
        Me.Size = New System.Drawing.Size(720, 296)
        CType(Me.grdGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ds As DataSet = New DataSet("GI")
    Private gdtGI As DataTable
    Private strCustID As String
    Dim WithEvents bm As BindingManagerBase

    Public Property CustID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = Value
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    Public Function resetDS()
        Me.gdtGI.Clear()
    End Function

    Public Sub buildUI()
        If gdtGI Is Nothing Then
            gdtGI = New DataTable("GI")
            gdtGI.Columns.Add("Description", Type.GetType("System.String"))
            gdtGI.Columns.Add("PolicyAccountID", Type.GetType("System.String"))
            gdtGI.Columns.Add("PolicyEffDate", Type.GetType("System.DateTime"))
            gdtGI.Columns.Add("PolicyEndDate", Type.GetType("System.DateTime"))
            gdtGI.Columns.Add("AccountStatus", Type.GetType("System.String"))
        End If

        'set the Grid Style
        If Me.grdGI.TableStyles.Count = 0 Then
            buildGridStyle()
        End If

        fillDT()
    End Sub

    Private Sub buildGridStyle()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "Description"
        'cs.HeaderText = "Product Description"
        'ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "PolicyAccountID"
        cs.HeaderText = "Policy No."
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyEffDate"
        cs.HeaderText = "Eff. Date"
        cs.Format = "dd-MMM-yyyy"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PolicyEndDate"
        cs.HeaderText = "End Date"
        cs.Format = "dd-MMM-yyyy"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AccountStatus"
        cs.HeaderText = "Status"
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "GI"
        grdGI.TableStyles.Add(ts)

        grdGI.DataSource = gdtGI
        grdGI.AllowDrop = False
        grdGI.ReadOnly = True
    End Sub

    Private Sub fillDT()
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

        'strCustID = "10502170"

        strSQL = "Select a.productid as Description, c.PolicyAccountid, c.PolicyRelateCode, a.PolicyEffDate,  a.PolicyEndDate, s.AccountStatus,  a.PolicyCurrency " & _
                 "From csw_poli_rel c, policyaccount a, AccountStatusCodes s " & _
                 "where c.policyaccountid = a.policyaccountid  and c.customerid = '" & strCustID & "'  and policyrelatecode = 'PH' and a.companyid = 'GI' " & _
                 "and a.AccountStatusCode = s.AccountStatusCode " & _
                 "order by Description "

        sqlconnect.ConnectionString = strCIWConn

        Try
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(gdtGI)

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try        

        bm = Me.BindingContext(gdtGI)

        If bm.Count > 0 Then
            cmdGIOpen.Enabled = True
        Else
            cmdGIOpen.Enabled = False
        End If

    End Sub

    Private Sub cmdGIOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGIOpen.Click
        'UpdatePT()
        Dim w As New frmGIPolicy

        If bm.Position <> -1 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
            If Not drI Is Nothing Then

                wndMain.Cursor = Cursors.WaitCursor

                w.PolicyAccountID(Strings.Right(drI.Item("PolicyAccountid"), 3), strCustID, drI.Item("AccountStatus")) = Strings.Left(drI.Item("PolicyAccountid"), 13)
                ShowWindow(w, wndMain, Trim(Strings.Left(drI.Item("PolicyAccountid"), 13)))

                wndMain.Cursor = Cursors.Default

                If w.NoRecord = True Then
                    w.Close()
                    w.Dispose()
                End If
            End If
        End If

    End Sub

    Private Sub grdGI_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdGI.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdGI.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdGI.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdGI.Select(hti.Row)
        End If
    End Sub

End Class
