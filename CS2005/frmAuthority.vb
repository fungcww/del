Imports System.Data.SqlClient
Imports CRS_Component

Public Class frmAuthority
    Inherits System.Windows.Forms.Form

    Private configEndPoint_Url As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ConfigEndPoint")

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBox3)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(204, 144)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(144, 104)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'CheckBox3
        '
        Me.CheckBox3.Location = New System.Drawing.Point(16, 72)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = "CheckBox3"
        '
        'CheckBox2
        '
        Me.CheckBox2.Location = New System.Drawing.Point(16, 48)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.TabIndex = 1
        Me.CheckBox2.Text = "CheckBox2"
        '
        'CheckBox1
        '
        Me.CheckBox1.Location = New System.Drawing.Point(16, 24)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "CheckBox1"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(4, 4)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(464, 132)
        Me.DataGrid1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(392, 256)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Save"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(392, 288)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cancel"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(392, 320)
        Me.Button3.Name = "Button3"
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Quit"
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(4, 140)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(192, 199)
        Me.ListBox1.TabIndex = 6
        '
        'frmAuthority
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 349)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAuthority"
        Me.Tag = "Authority Control"
        Me.Text = "Authority Control"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim objDS As DataSet = New DataSet("CS2005")
    Dim dr, dr1 As DataRow
    Dim da As SqlDataAdapter
    Dim sqlConnect As New SqlConnection
    Dim dt As DataTable

    Private Sub frmAuthority_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strSQL As String
        Dim strErrNo As Integer
        Dim strErrMsg As String

        ' Get code table from SQL
        Try
            strSQL = "select * from " & UPS_USER_GROUP_TAB & _
                " where upsugt_sys_abv = '" & gUPSystem & "'"
            'sqlConnect.ConnectionString = "packet size=4096;user id=acsowner;data source=hkalsqldev1;persist security info=True;initial catalog=profile;password=ownpapa"
            sqlConnect.ConnectionString = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "UPSConnection").DecryptString()
            da = New SqlDataAdapter(strSQL, sqlConnect)
            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough
            'da.FillSchema(objDS, SchemaType.Source, UPS_USER_GROUP_TAB)
            da.Fill(objDS, UPS_USER_GROUP_TAB)

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridColumnStyle

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "upsugt_usr_grp_no"
        cs.HeaderText = "Group No."
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "upsugt_usr_grp_name"
        cs.HeaderText = "Group Name"
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = UPS_USER_GROUP_TAB
        DataGrid1.TableStyles.Add(ts)

        DataGrid1.DataSource = objDS.Tables(UPS_USER_GROUP_TAB)
        DataGrid1.AllowDrop = False
        DataGrid1.ReadOnly = True

    End Sub

    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = DataGrid1.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            DataGrid1.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            DataGrid1.Select(hti.Row)
        End If
    End Sub

    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged

        Dim dv As New DataView
        dv.Table = objDS.Tables(CSW_USER_PRIVS)
        dv.RowFilter = "cswup_name = 'tabPaymentHistoy'"
        dv.RowStateFilter = DataViewRowState.None
        If dv.Count > 0 Then

        End If

    End Sub

End Class
